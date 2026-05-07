using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProjectAplikasiPerpustakaan
{
    public partial class LoginMenu : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        public LoginMenu()
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Optional: Atur properti awal textbox dan button
            txtUsername.Clear();
            textPassword.Clear();
            textPassword.PasswordChar = '●';     // Ubah menjadi bulat (password mask)
            btnLogin.Enabled = false;            // Nonaktifkan tombol login sampai ada input
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            // Aktifkan tombol login jika username dan password sudah diisi
            CekTombolLogin();
        }

        private void textPassword_TextChanged(object sender, EventArgs e)
        {
            CekTombolLogin();
        }

        // Fungsi untuk mengaktifkan/menonaktifkan tombol login
        private void CekTombolLogin()
        {
            btnLogin.Enabled = !string.IsNullOrWhiteSpace(txtUsername.Text) &&
                               !string.IsNullOrWhiteSpace(textPassword.Text);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = textPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username dan Password tidak boleh kosong!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                conn.Open();
                string query = @"SELECT username, role FROM Pengguna 
                         WHERE username = @username AND password = @password";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string namaUser = reader["username"].ToString();
                            string role = reader["role"].ToString();

                            MessageBox.Show($"Login berhasil!\nSelamat datang, {namaUser}",
                                "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            if (role == "admin")
                            {
                                // Masuk sebagai Admin
                                Admin formAdmin = new Admin(namaUser, role);
                                formAdmin.Show();
                            }
                            else
                            {
                                // Masuk sebagai Pengunjung / User biasa
                                CariBuku formCari = new CariBuku(namaUser, role);
                                formCari.Show();
                            }

                            this.Hide();

                            this.Hide();        // Sembunyikan form login
                                                // this.Close();    // JANGAN pakai Close() di sini, nanti aplikasi mati
                        }
                        else
                        {
                            MessageBox.Show("Username atau Password salah!",
                                "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            textPassword.Clear();
                            textPassword.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}