using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class TotalLaporan : Form
    {
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        public TotalLaporan()
        {
            InitializeComponent();
        }

        private void Laporan_Load(object sender, EventArgs e)
        {
            LoadTotalLaporan();
        }

        // ================== LOAD TOTAL LAPORAN ==================
        private void LoadTotalLaporan()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            FORMAT(GETDATE(), 'MMMM yyyy') AS periode,
                            
                            -- Total Kunjungan dihitung dari PEMINJAMAN yang statusnya SELESAI
                            (SELECT COUNT(*) FROM PEMINJAMAN 
                             WHERE status = 'selesai') AS total_kunjungan,
                            
                            -- Total Peminjaman keseluruhan
                            (SELECT COUNT(*) FROM PEMINJAMAN) AS total_peminjaman,
                            
                            -- Total Pengembalian
                            (SELECT COUNT(*) FROM PENGEMBALIAN) AS total_pengembalian,
                            
                            -- Total Denda
                            (SELECT ISNULL(SUM(denda), 0) FROM PENGEMBALIAN) AS total_denda;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                lblPeriode.Text = reader["periode"].ToString();
                                lblTotalKunjungan.Text = Convert.ToInt32(reader["total_kunjungan"]).ToString("N0") + " orang";
                                lblPeminjaman.Text = Convert.ToInt32(reader["total_peminjaman"]).ToString("N0") + " buku";
                                lblPengembalian.Text = Convert.ToInt32(reader["total_pengembalian"]).ToString("N0") + " buku";
                                lblDenda.Text = "Rp " + Convert.ToDecimal(reader["total_denda"]).ToString("N0");
                            }
                            else
                            {
                                ResetLabels();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat memuat data:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetLabels();
            }
        }

        private void ResetLabels()
        {
            lblPeriode.Text = "-";
            lblTotalKunjungan.Text = "0 orang";
            lblPeminjaman.Text = "0 buku";
            lblPengembalian.Text = "0 buku";
            lblDenda.Text = "Rp 0";
        }


        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSimpanLaporan_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Ambil data laporan
                    string querySelect = @"
                SELECT
                    FORMAT(GETDATE(), 'yyyy-MM') AS periode,
                    (SELECT COUNT(*) FROM PEMINJAMAN WHERE status = 'selesai') AS total_kunjungan,
                    (SELECT COUNT(*) FROM PEMINJAMAN) AS total_peminjaman,
                    (SELECT COUNT(*) FROM PENGEMBALIAN) AS total_pengembalian,
                    (SELECT ISNULL(SUM(denda), 0) FROM PENGEMBALIAN) AS total_denda;";

                    string periode = "";
                    int totalKunjungan = 0, totalPeminjaman = 0, totalPengembalian = 0;
                    decimal totalDenda = 0;

                    using (SqlCommand cmdSelect = new SqlCommand(querySelect, conn))
                    using (SqlDataReader reader = cmdSelect.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            periode = reader["periode"].ToString();
                            totalKunjungan = Convert.ToInt32(reader["total_kunjungan"]);
                            totalPeminjaman = Convert.ToInt32(reader["total_peminjaman"]);
                            totalPengembalian = Convert.ToInt32(reader["total_pengembalian"]);
                            totalDenda = Convert.ToDecimal(reader["total_denda"]);
                        }
                    }

                    // Cek duplikat periode
                    string checkQuery = "SELECT COUNT(*) FROM LAPORAN WHERE periode = @periode";
                    using (SqlCommand cmdCheck = new SqlCommand(checkQuery, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@periode", periode);
                        int existing = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        if (existing > 0)
                        {
                            var dr = MessageBox.Show($"Laporan untuk periode {periode} sudah ada.\n\nGanti dengan data baru?",
                                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dr == DialogResult.No) return;
                        }
                    }

                    // INSERT dengan id_user
                    string insertQuery = @"
                INSERT INTO LAPORAN 
                    (id_user, periode, total_kunjungan, total_peminjaman, 
                     total_pengembalian, total_denda)
                VALUES 
                    (@id_user, @periode, @total_kunjungan, @total_peminjaman, 
                     @total_pengembalian, @total_denda)";

                    using (SqlCommand cmdInsert = new SqlCommand(insertQuery, conn))
                    {
                        // 🔥 GANTI DENGAN ID_USER YANG SEDANG LOGIN
                        cmdInsert.Parameters.AddWithValue("@id_user", 1);   // ← Ubah ini!

                        cmdInsert.Parameters.AddWithValue("@periode", periode);
                        cmdInsert.Parameters.AddWithValue("@total_kunjungan", totalKunjungan);
                        cmdInsert.Parameters.AddWithValue("@total_peminjaman", totalPeminjaman);
                        cmdInsert.Parameters.AddWithValue("@total_pengembalian", totalPengembalian);
                        cmdInsert.Parameters.AddWithValue("@total_denda", totalDenda);

                        int result = cmdInsert.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show($"Laporan periode {periode} berhasil disimpan!",
                                "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyimpan laporan:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}