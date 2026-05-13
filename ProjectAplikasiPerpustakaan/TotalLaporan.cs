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

                    string query = "SELECT * FROM vw_TotalLaporan";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblPeriode.Text = reader["periode_tampil"].ToString();
                            lblTotalKunjungan.Text =
                                Convert.ToInt32(reader["total_kunjungan"]).ToString("N0") + " orang";

                            lblPeminjaman.Text =
                                Convert.ToInt32(reader["total_peminjaman"]).ToString("N0") + " buku";

                            lblPengembalian.Text =
                                Convert.ToInt32(reader["total_pengembalian"]).ToString("N0") + " buku";

                            lblDenda.Text =
                                "Rp " + Convert.ToDecimal(reader["total_denda"]).ToString("N0");
                        }
                        else
                        {
                            ResetLabels();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan:\n" + ex.Message);
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

                    using (SqlCommand cmd = new SqlCommand("sp_SimpanLaporan", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@id_user", 1);
                        // nanti ganti pakai id user login

                        cmd.ExecuteNonQuery();

                        MessageBox.Show(
                            "Laporan berhasil disimpan!",
                            "Sukses",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Gagal menyimpan laporan:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}