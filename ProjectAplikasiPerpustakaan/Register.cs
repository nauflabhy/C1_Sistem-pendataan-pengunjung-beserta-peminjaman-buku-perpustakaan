using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class Register : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        public Register()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        private void Register_Load(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = '●';
            txtConfirmPassword.PasswordChar = '●';
            btnRegister.Enabled = false;

            // === Pembatasan Input ===

            // Username: huruf + angka (biasanya tidak pakai spasi)
            txtUsername.KeyPress += (s, ev) =>
            {
                if (!char.IsLetterOrDigit(ev.KeyChar) && ev.KeyChar != '\b')
                    ev.Handled = true;
            };

            // Nama Lengkap: huruf, angka, spasi
            txtNamaLengkap.KeyPress += AllowOnlyAlphanumeric_KeyPress;

            // No HP: hanya angka
            txtNoHp.KeyPress += AllowOnlyNumbers_KeyPress;

            // Email: huruf, angka, @ . _ - (email butuh simbol tertentu)
            txtEmail.KeyPress += (s, ev) =>
            {
                if (!char.IsLetterOrDigit(ev.KeyChar) &&
                    "@._-".IndexOf(ev.KeyChar) == -1 &&
                    ev.KeyChar != '\b')
                {
                    ev.Handled = true;
                }
            };
        }

        // Hanya huruf dan angka (Alphanumeric) + spasi opsional
        private void AllowOnlyAlphanumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Izinkan huruf, angka, spasi, dan tombol Backspace/Delete
            if (!char.IsLetterOrDigit(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar) &&
                e.KeyChar != '\b')
            {
                e.Handled = true; // Blokir karakter
            }
        }

        // Hanya angka (untuk No HP)
        private void AllowOnlyNumbers_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void EnableRegisterButton()
        {
            btnRegister.Enabled = !string.IsNullOrWhiteSpace(txtUsername.Text) &&
                                  !string.IsNullOrWhiteSpace(txtPassword.Text) &&
                                  !string.IsNullOrWhiteSpace(txtConfirmPassword.Text) &&
                                  !string.IsNullOrWhiteSpace(txtNamaLengkap.Text) &&
                                  !string.IsNullOrWhiteSpace(txtNoHp.Text) &&
                                  !string.IsNullOrWhiteSpace(txtEmail.Text); // tambahkan field lain jika ada
        }

        private void txtUsername_TextChanged(object sender, EventArgs e) => EnableRegisterButton();
        private void txtPassword_TextChanged(object sender, EventArgs e) => EnableRegisterButton();
        private void txtConfirmPassword_TextChanged(object sender, EventArgs e) => EnableRegisterButton();
        private void txtNamaLengkap_TextChanged(object sender, EventArgs e) => EnableRegisterButton(); // jika ada field nama

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();
            string namaLengkap = txtNamaLengkap.Text.Trim();
            string noHp = txtNoHp.Text.Trim();
            string email = txtEmail.Text.Trim();
            string role = "pengunjung";

            // ===== VALIDASI DULU SEBELUM MENYENTUH DATABASE =====
            if (string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(confirmPassword) ||
                string.IsNullOrEmpty(namaLengkap) ||
                string.IsNullOrEmpty(noHp) ||
                string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Username, Password, Nama Lengkap, Email, dan No HP wajib diisi!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Password dan Konfirmasi Password tidak cocok!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtConfirmPassword.Clear();
                txtConfirmPassword.Focus();
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password minimal 6 karakter!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ===== BARU MASUK DATABASE SETELAH VALIDASI LOLOS =====
            SqlTransaction transaction = null;

            try
            {
                conn.Open();
                transaction = conn.BeginTransaction(); // mulai transaction

                // Cek username sudah ada
                string checkQuery = "SELECT COUNT(*) FROM Pengguna WHERE username = @username";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn, transaction))
                {
                    checkCmd.Parameters.AddWithValue("@username", username);
                    int existing = (int)checkCmd.ExecuteScalar();
                    if (existing > 0)
                    {
                        transaction.Rollback(); // batalkan
                        MessageBox.Show("Username sudah digunakan! Silakan pilih username lain.",
                            "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Step 1: INSERT ke Pengguna
                string insertPengguna = @"
    INSERT INTO Pengguna (username, nama_lengkap, email, no_hp, password, role)
    VALUES (@username, @nama_lengkap, @email, @no_hp, @password, @role);
    SELECT SCOPE_IDENTITY();";

                int idUserBaru = 0;
                using (SqlCommand cmd = new SqlCommand(insertPengguna, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@nama_lengkap", namaLengkap);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@no_hp", noHp);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@role", role);
                    idUserBaru = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // Step 2: INSERT ke PENGUNJUNG
                if (idUserBaru > 0)
                {
                    string insertPengunjung = @"
                INSERT INTO PENGUNJUNG (id_user, nama_lengkap, no_hp, email)
                VALUES (@id_user, @nama_lengkap, @no_hp, @email)";

                    using (SqlCommand cmd2 = new SqlCommand(insertPengunjung, conn, transaction))
                    {
                        cmd2.Parameters.AddWithValue("@id_user", idUserBaru);
                        cmd2.Parameters.AddWithValue("@nama_lengkap", namaLengkap);
                        cmd2.Parameters.AddWithValue("@no_hp", noHp);
                        cmd2.Parameters.AddWithValue("@email", email);
                        cmd2.ExecuteNonQuery();
                    }
                }

                // Semua berhasil → COMMIT
                transaction.Commit();

                MessageBox.Show("Registrasi berhasil!\nSilakan login dengan akun baru Anda.",
                    "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoginMenu loginForm = new LoginMenu();
                loginForm.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                // Ada error → ROLLBACK semua, tidak ada data tersimpan
                transaction?.Rollback();
                MessageBox.Show("Terjadi kesalahan saat registrasi:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }


        // Opsional: Tombol Cancel / Kembali ke Login
        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoginMenu loginForm = new LoginMenu();
            loginForm.Show();
            this.Close();
        }
        

        private void txtUsername_TextChanged_1(object sender, EventArgs e)
        {
            EnableRegisterButton();
        }

        private void txtPassword_TextChanged_1(object sender, EventArgs e)
        {
            EnableRegisterButton();
        }

        private void txtConfirmPassword_TextChanged_1(object sender, EventArgs e)
        {
            EnableRegisterButton();
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Kembali ke halaman Login?\nPerubahan data akan dibuang.",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                LoginMenu loginForm = new LoginMenu();
                loginForm.Show();
                this.Close();
            }
        }
    }
}
