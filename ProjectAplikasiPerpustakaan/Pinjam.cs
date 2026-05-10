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
        private readonly int? idUser;

        // Constructor dengan parameter (dipanggil dari form CariBuku)
        public Pinjam(int idBuku, string kodeBuku, string judulBuku, int? idUser = null)
        {
            InitializeComponent();
            this.idBuku = idBuku;
            this.kodeBuku = kodeBuku;
            this.judulBuku = judulBuku;
            this.idUser = idUser;
        }

        private void Pinjam_Load(object sender, EventArgs e)
        {
            // Tampilkan data buku di Label
            lblKodeBuku.Text = kodeBuku ?? "Kode tidak tersedia";
            lblJudulBuku.Text = judulBuku ?? "Judul tidak tersedia";

            // ============== BATASI INPUT PADA SETIAP TEXTBOX ==============
            txtNIK.KeyPress += AllowOnlyNumbers_KeyPress;
            txtNIK.MaxLength = 20;

            txtNamaLengkap.KeyPress += AllowOnlyAlphanumeric_KeyPress;
            txtNamaLengkap.MaxLength = 100;

            txtNoHp.KeyPress += AllowOnlyNumbers_KeyPress;
            txtNoHp.MaxLength = 15;

            txtEmail.KeyPress += AllowEmailCharacters_KeyPress;
            txtEmail.MaxLength = 80;

            txtPerguruan.KeyPress += AllowOnlyAlphanumeric_KeyPress;
            txtPerguruan.MaxLength = 100;
        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // ================== PEMBATAS INPUT ==================
        private void AllowOnlyAlphanumeric_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar) &&
                e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void AllowOnlyNumbers_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void AllowEmailCharacters_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) &&
                "@._-".IndexOf(e.KeyChar) == -1 &&
                e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void btnAjukanPeminjaman_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNIK.Text) ||
        string.IsNullOrWhiteSpace(txtNamaLengkap.Text))
            {
                MessageBox.Show("NIK dan Nama Lengkap wajib diisi!",
                                "Validasi Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    int idPengunjung;

                    // === 1. Cek apakah pengunjung sudah ada berdasarkan NIK ===
                    string queryCek = "SELECT id_pengunjung FROM PENGUNJUNG WHERE nik = @nik";
                    using (SqlCommand cmdCek = new SqlCommand(queryCek, conn))
                    {
                        cmdCek.Parameters.AddWithValue("@nik", txtNIK.Text.Trim());
                        object result = cmdCek.ExecuteScalar();

                        if (result != null)
                        {
                            idPengunjung = Convert.ToInt32(result);
                        }
                        else
                        {
                            // === 2. Insert pengunjung baru ===
                            string queryInsertPengunjung = @"
                            INSERT INTO PENGUNJUNG 
                            (id_user, nik, nama_lengkap, no_hp, email, perguruan)
                            VALUES 
                            (@id_user, @nik, @nama, @nohp, @email, @perguruan);
                            SELECT SCOPE_IDENTITY();";

                            using (SqlCommand cmdInsert = new SqlCommand(queryInsertPengunjung, conn))
                            {
                                // ✅ Tambahkan parameter id_user
                                cmdInsert.Parameters.AddWithValue("@id_user", (object)this.idUser ?? DBNull.Value);

                                cmdInsert.Parameters.AddWithValue("@nik", txtNIK.Text.Trim());
                                cmdInsert.Parameters.AddWithValue("@nama", txtNamaLengkap.Text.Trim());
                                cmdInsert.Parameters.AddWithValue("@nohp", txtNoHp.Text.Trim());
                                cmdInsert.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                                cmdInsert.Parameters.AddWithValue("@perguruan", txtPerguruan.Text.Trim());

                                idPengunjung = Convert.ToInt32(cmdInsert.ExecuteScalar());
                            }
                        }
                    }

                    // === 3. Insert ke tabel PEMINJAMAN ===
                    string queryPinjam = @"
                INSERT INTO PEMINJAMAN 
                (id_pengunjung, id_buku, tanggal_ajuan, status)
                VALUES 
                (@id_pengunjung, @id_buku, GETDATE(), 'menunggu')";

                    using (SqlCommand cmdPinjam = new SqlCommand(queryPinjam, conn))
                    {
                        cmdPinjam.Parameters.AddWithValue("@id_pengunjung", idPengunjung);
                        cmdPinjam.Parameters.AddWithValue("@id_buku", this.idBuku);

                        int rowsAffected = cmdPinjam.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("✅ Peminjaman berhasil diajukan!\nStatus: Menunggu Persetujuan Admin.",
                                            "Berhasil", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Gagal menyimpan peminjaman.", "Gagal",
                                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat mengajukan peminjaman:\n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}