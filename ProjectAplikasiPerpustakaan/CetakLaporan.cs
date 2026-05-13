using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace ProjectAplikasiPerpustakaan
{
    public partial class CetakLaporan : Form
    {
        private readonly string connectionString =
        "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";
        private DataTable dtLaporan;
        private BindingSource bsLaporan = new BindingSource();
        public CetakLaporan()
        {
            InitializeComponent();
        }
        private void CetakLaporan_Load(object sender, EventArgs e)
        {
            bindingNavigator1.BindingSource = bsLaporan; 

            bsLaporan.PositionChanged += bsLaporan_PositionChanged;

            LoadDataLaporan();
        }


        private void bsLaporan_PositionChanged(object sender, EventArgs e)
        {
            if (dgvLaporan.Rows.Count > 0)
            {
                dgvLaporan.ClearSelection();
                dgvLaporan.Rows[bsLaporan.Position].Selected = true;
            }
        }

        // ================== LOAD DATA PENGEMBALIAN ==================
        private void LoadDataLaporan()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM vw_LaporanPeminjaman ORDER BY id_peminjaman DESC";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        dtLaporan = new DataTable();
                        adapter.Fill(dtLaporan);
                        bsLaporan.DataSource = dtLaporan;
                        dgvLaporan.DataSource = bsLaporan;

                        if (dgvLaporan.Columns.Count > 0)
                        {
                            if (dgvLaporan.Columns["kode_buku"] != null)
                                dgvLaporan.Columns["kode_buku"].Visible = false;

                            dgvLaporan.Columns["nama_pengunjung"].HeaderText = "Nama Pengunjung";
                            dgvLaporan.Columns["judul_buku"].HeaderText = "Judul Buku";
                            dgvLaporan.Columns["tanggal_pinjam"].HeaderText = "Tanggal Pinjam";
                            dgvLaporan.Columns["tanggal_kembali"].HeaderText = "Tanggal Kembali";
                            dgvLaporan.Columns["kondisi_buku"].HeaderText = "Kondisi Buku";
                            dgvLaporan.Columns["denda"].HeaderText = "Denda (Rp)";
                            dgvLaporan.Columns["status"].HeaderText = "Status";
                            dgvLaporan.Columns["catatan"].HeaderText = "Catatan";

                            dgvLaporan.Columns["tanggal_pinjam"].DefaultCellStyle.Format = "dd MMM yyyy";
                            dgvLaporan.Columns["tanggal_kembali"].DefaultCellStyle.Format = "dd MMM yyyy";

                            dgvLaporan.Columns["denda"].DefaultCellStyle.Format = "N0";
                            dgvLaporan.Columns["denda"].DefaultCellStyle.Alignment =
                                DataGridViewContentAlignment.MiddleRight;

                            dgvLaporan.AutoSizeColumnsMode =
                                DataGridViewAutoSizeColumnsMode.AllCells;

                            dgvLaporan.Columns["judul_buku"].AutoSizeMode =
                                DataGridViewAutoSizeColumnMode.Fill;
                        }

                        if (dtLaporan.Rows.Count == 0)
                        {
                            MessageBox.Show(
                                "Belum ada data laporan.",
                                "Informasi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Terjadi kesalahan saat memuat data:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        // ================== TOMBOL ==================
        private void btnCetak_Click_1(object sender, EventArgs e)
        {
            try
            {

                TotalLaporan formTotal = new TotalLaporan();
                formTotal.ShowDialog();   // Menggunakan ShowDialog agar modal (lebih rapi)
                // Atau gunakan formTotal.Show(); jika ingin non-modal
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka form Total Laporan:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            LoadDataLaporan();
        }
        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}