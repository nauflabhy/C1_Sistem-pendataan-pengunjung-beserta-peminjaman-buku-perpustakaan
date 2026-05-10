using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class FormPengembalian : Form
    {
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        private int idPeminjaman;
        private int idUser;
        private string namaAdmin;
        private string roleAdmin;

        public FormPengembalian(
            int idPeminjaman,
            string kodeBuku,
            string judulBuku,
            DateTime tanggalAjuan,
            int idUser)
        {
            InitializeComponent();

            this.idPeminjaman = idPeminjaman;
            this.idUser = idUser;

            // tampilkan data ke label
            lblKodeBuku.Text = kodeBuku;
            lblJudulBuku.Text = judulBuku;
            lblTanggalAjuan.Text =
                tanggalAjuan.ToString("dd MMM yyyy");

            lblTanggalPengembalian.Text =
                DateTime.Now.ToString("dd MMM yyyy");

            // isi pilihan kondisi buku
            cmbKondisiBuku.Items.Add("baik");
            cmbKondisiBuku.Items.Add("rusak ringan");
            cmbKondisiBuku.Items.Add("rusak berat");
            cmbKondisiBuku.Items.Add("hilang");

            // default pilihan
            cmbKondisiBuku.SelectedIndex = 0;
        }

        private void btnKembalikan_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // =========================
                    // CEK APAKAH SUDAH DIKEMBALIKAN
                    // =========================
                    string checkQuery = @"
            SELECT COUNT(*)
            FROM PENGEMBALIAN
            WHERE id_peminjaman = @id_peminjaman";

                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@id_peminjaman", idPeminjaman);

                        int jumlah = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (jumlah > 0)
                        {
                            MessageBox.Show(
                                "Buku ini sudah pernah dikembalikan.",
                                "Informasi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                            return;
                        }
                    }

                    // =========================
                    // INSERT PENGEMBALIAN
                    // =========================
                    string queryInsert = @"
            INSERT INTO PENGEMBALIAN
            (
                id_peminjaman,
                tanggal_kembali,
                kondisi_buku,
                denda,
                catatan
            )
            VALUES
            (
                @id_peminjaman,
                @tanggal_kembali,
                @kondisi_buku,
                @denda,
                @catatan
            )";

                    decimal denda = 0;

                    switch (cmbKondisiBuku.Text.ToLower())
                    {
                        case "rusak ringan":
                            denda = 10000;
                            break;

                        case "rusak berat":
                            denda = 50000;
                            break;

                        case "hilang":
                            denda = 100000;
                            break;
                    }

                    using (SqlCommand cmd = new SqlCommand(queryInsert, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_peminjaman", idPeminjaman);
                        cmd.Parameters.AddWithValue("@tanggal_kembali", DateTime.Now);
                        cmd.Parameters.AddWithValue(
                            "@kondisi_buku",
                            cmbKondisiBuku.Text
                        );
                        cmd.Parameters.AddWithValue("@denda", denda);
                        cmd.Parameters.AddWithValue("@catatan", "Buku telah dikembalikan");

                        cmd.ExecuteNonQuery();
                    }

                    // =========================
                    // UPDATE STATUS
                    // =========================

                    string queryUpdate = @"
                    UPDATE PEMINJAMAN
                    SET status = 'selesai'
                    WHERE id_peminjaman = @id_peminjaman";

                    using (SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("@id_peminjaman", idPeminjaman);
                        cmdUpdate.ExecuteNonQuery();

                        if (cmbKondisiBuku.Text.ToLower() != "hilang")
                        {
                            string queryStok = @"
                            UPDATE BUKU
                            SET stok_tersedia = stok_tersedia + 1
                            WHERE id_buku = (
                                SELECT id_buku
                                FROM PEMINJAMAN
                                WHERE id_peminjaman = @id_peminjaman
                            )";

                            using (SqlCommand cmdStok = new SqlCommand(queryStok, conn))
                            {
                                cmdStok.Parameters.AddWithValue("@id_peminjaman", idPeminjaman);
                                cmdStok.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageBox.Show(
                        "Buku berhasil dikembalikan!",
                        "Sukses",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    // buka laporan
                    CetakLaporan formLaporan = new CetakLaporan();
                    formLaporan.Show();

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Terjadi kesalahan:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnBatal_Click(object sender, EventArgs e)
        {
            // kembali ke form daftar pengajuan
            btnKembali formPengajuan = new btnKembali(idUser, namaAdmin, roleAdmin);

            formPengajuan.Show();

            this.Close();
        }

        private void FormPengembalian_Load(object sender, EventArgs e)
        {
        }
    }
}