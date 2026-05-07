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
    public partial class Pinjam : Form
    {
        private readonly string connectionString =
       "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        private readonly int idBuku;
        private readonly string kodeBuku;
        private readonly string judulBuku;
        private readonly string namaPengguna;
        private readonly string rolePengguna;

        private int idPengunjung = 0;

        public Pinjam(int idBuku, string kodeBuku, string judulBuku, string namaPengguna, string rolePengguna)
        {
            InitializeComponent();

            this.idBuku = idBuku;
            this.kodeBuku = kodeBuku;
            this.judulBuku = judulBuku;
            this.namaPengguna = namaPengguna;
            this.rolePengguna = rolePengguna;

        }

        private void Pinjam_Load(object sender, EventArgs e)
        {
            lblKodeBuku.Text = kodeBuku;
            lblJudul.Text = judulBuku;

            // Ambil data pengunjung
            AmbilDataPengunjung();

            // ============== BATASI INPUT PADA SETIAP TEXTBOX ==============

            // NIK → Hanya Angka
            txtNIK.KeyPress += AllowOnlyNumbers_KeyPress;
            txtNIK.MaxLength = 20; // sesuaikan kebutuhan

            // Nama Lengkap → Huruf, Angka, Spasi
            txtNamaLengkap.KeyPress += AllowOnlyAlphanumeric_KeyPress;
            txtNamaLengkap.MaxLength = 100;

            // No HP → Hanya Angka
            txtNoHp.KeyPress += AllowOnlyNumbers_KeyPress;
            txtNoHp.MaxLength = 15;

            // Email → Huruf, Angka, @ . _ -
            txtEmail.KeyPress += AllowEmailCharacters_KeyPress;
            txtEmail.MaxLength = 80;

            // Perguruan (Universitas/Sekolah) → Huruf, Angka, Spasi
            txtPerguruan.KeyPress += AllowOnlyAlphanumeric_KeyPress;
            txtPerguruan.MaxLength = 100;
        }

        private void AmbilDataPengunjung()
        {
            string query = @"
                SELECT p.id_pengunjung, p.nik, p.nama_lengkap, p.no_hp, p.email
                FROM PENGUNJUNG p
                JOIN Pengguna u ON p.id_user = u.id_user
                WHERE u.username = @username";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@username", namaPengguna);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        idPengunjung = Convert.ToInt32(reader["id_pengunjung"]);
                        txtNIK.Text = reader["nik"].ToString();
                        txtNamaLengkap.Text = reader["nama_lengkap"].ToString();
                        txtNoHp.Text = reader["no_hp"].ToString();
                        txtEmail.Text = reader["email"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Data pengunjung tidak ditemukan.", "Info",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mengambil data: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtNIK_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNamaLengkap_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNoHp_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPeguruan_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAjukanPeminjaman_Click(object sender, EventArgs e)
        {
            if (idPengunjung == 0)
            {
                MessageBox.Show("Data pengunjung tidak valid. Silakan login ulang.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi field wajib di form Pinjam
            if (string.IsNullOrWhiteSpace(txtNamaLengkap.Text) ||
                string.IsNullOrWhiteSpace(txtNoHp.Text))
            {
                MessageBox.Show("Nama Lengkap dan No HP wajib diisi!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // NIK, Email, Perguruan opsional — tidak divalidasi

            DialogResult konfirmasi = MessageBox.Show(
                $"Ajukan peminjaman buku:\n\"{judulBuku}\"?\n\nPastikan data Anda sudah benar.",
                "Konfirmasi Peminjaman",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi != DialogResult.Yes) return;

            if (!CekStokBuku()) return;
            if (SudahMeminjamBuku()) return;

            UpdateDataPengunjung(); // simpan perubahan profil dulu
            AjukanPeminjaman();
        }

        private void UpdateDataPengunjung()
        {
            string query = @"
        UPDATE PENGUNJUNG
        SET nama_lengkap = @nama_lengkap,
            no_hp        = @no_hp,
            nik          = @nik,
            email        = @email,
            perguruan    = @perguruan
        WHERE id_pengunjung = @id_pengunjung";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@nama_lengkap", txtNamaLengkap.Text.Trim());
                cmd.Parameters.AddWithValue("@no_hp", txtNoHp.Text.Trim());

                // Opsional — simpan NULL jika kosong
                cmd.Parameters.AddWithValue("@nik",
                    string.IsNullOrWhiteSpace(txtNIK.Text)
                    ? (object)DBNull.Value : txtNIK.Text.Trim());

                cmd.Parameters.AddWithValue("@email",
                    string.IsNullOrWhiteSpace(txtEmail.Text)
                    ? (object)DBNull.Value : txtEmail.Text.Trim());

                cmd.Parameters.AddWithValue("@perguruan",
                    string.IsNullOrWhiteSpace(txtPerguruan.Text)
                    ? (object)DBNull.Value : txtPerguruan.Text.Trim());

                cmd.Parameters.AddWithValue("@id_pengunjung", idPengunjung);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal update profil: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool SudahMeminjamBuku()
        {
            // Cek jika ada peminjaman dengan status menunggu/disetujui/dipinjam untuk buku yang sama
            string query = @"
                SELECT COUNT(*) FROM PEMINJAMAN
                WHERE id_pengunjung = @id_pengunjung
                  AND id_buku       = @id_buku
                  AND status IN ('menunggu', 'disetujui', 'dipinjam')";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id_pengunjung", idPengunjung);
                cmd.Parameters.AddWithValue("@id_buku", idBuku);
                try
                {
                    conn.Open();
                    int jumlah = Convert.ToInt32(cmd.ExecuteScalar());
                    if (jumlah > 0)
                    {
                        MessageBox.Show("Anda sudah memiliki peminjaman aktif untuk buku ini.", "Peringatan",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal validasi peminjaman: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return true; // anggap gagal = blok dulu
                }
            }
        }

        private void AjukanPeminjaman()
        {
            string query = @"
        INSERT INTO PEMINJAMAN 
            (id_pengunjung, id_buku, tanggal_ajuan, status)
        VALUES 
            (@id_pengunjung, @id_buku, GETDATE(), 'menunggu')";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id_pengunjung", idPengunjung);
                cmd.Parameters.AddWithValue("@id_buku", idBuku);

                try
                {
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Pengajuan peminjaman berhasil dikirim!\nStatus: Menunggu persetujuan admin.",
                            "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        CariBuku formCari = new CariBuku(namaPengguna, rolePengguna);
                        formCari.Show();
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal mengajukan peminjaman:\n" + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool CekStokBuku()
        {
            string query = "SELECT stok_tersedia FROM BUKU WHERE id_buku = @id_buku";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@id_buku", idBuku);
                try
                {
                    conn.Open();
                    int stok = Convert.ToInt32(cmd.ExecuteScalar());
                    if (stok <= 0)
                    {
                        MessageBox.Show("Maaf, stok buku ini sudah habis.", "Stok Habis",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal cek stok: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            CariBuku formCariBuku = new CariBuku(namaPengguna, rolePengguna);
            formCariBuku.Show();
            this.Close();
        }

        // ================== PEMBATAS INPUT ==================

        // Huruf + Angka + Spasi (untuk Nama, Perguruan)
        private void AllowOnlyAlphanumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar) &&
                e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        // Hanya Angka (untuk No HP dan NIK)
        private void AllowOnlyNumbers_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        // Khusus Email (boleh @ . _ -)
        private void AllowEmailCharacters_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) &&
                "@._-".IndexOf(e.KeyChar) == -1 &&
                e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }
    }
}
