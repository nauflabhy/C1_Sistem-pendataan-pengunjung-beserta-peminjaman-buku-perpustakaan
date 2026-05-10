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
        public CetakLaporan()
        {
            InitializeComponent();
        }
        private void CetakLaporan_Load(object sender, EventArgs e)
        {
            LoadDataLaporan();
        }
        // ================== LOAD DATA PENGEMBALIAN ==================
        private void LoadDataLaporan()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                    SELECT
                        pm.id_peminjaman,
                        pjg.nama_lengkap AS nama_pengunjung,
                        b.judul AS judul_buku,
                        b.kode_buku,
                        pm.tanggal_pinjam,
                        pgb.tanggal_kembali,

                        ISNULL(pgb.kondisi_buku, '-') AS kondisi_buku,
                        ISNULL(pgb.denda, 0) AS denda,

                        pm.status,
    
                        CASE 
                            WHEN pm.status = 'ditolak' 
                            THEN pm.alasan_tolak
                            ELSE ISNULL(pgb.catatan, '-')
                        END AS catatan

                    FROM PEMINJAMAN pm
                    JOIN PENGUNJUNG pjg ON pm.id_pengunjung = pjg.id_pengunjung
                    JOIN BUKU b ON pm.id_buku = b.id_buku

                    LEFT JOIN PENGEMBALIAN pgb 
                        ON pm.id_peminjaman = pgb.id_peminjaman

                    ORDER BY pm.tanggal_ajuan DESC";
                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        dtLaporan = new DataTable();
                        adapter.Fill(dtLaporan);
                        dgvLaporan.DataSource = dtLaporan;
                        if (dgvLaporan.Columns.Count > 0)
                        {
                            // Sembunyikan kolom teknis
                            if (dgvLaporan.Columns["id_pengembalian"] != null)
                                dgvLaporan.Columns["id_pengembalian"].Visible = false;

                            if (dgvLaporan.Columns["kode_buku"] != null)
                                dgvLaporan.Columns["kode_buku"].Visible = false;

                            if (dgvLaporan.Columns["tanggal_ajuan"] != null)
                                dgvLaporan.Columns["tanggal_ajuan"].Visible = false;
                            // Header label
                            dgvLaporan.Columns["nama_pengunjung"].HeaderText = "Nama Pengunjung";
                            dgvLaporan.Columns["judul_buku"].HeaderText = "Judul Buku";
                            dgvLaporan.Columns["tanggal_pinjam"].HeaderText = "Tanggal Pinjam";
                            dgvLaporan.Columns["tanggal_kembali"].HeaderText = "Tanggal Kembali";
                            dgvLaporan.Columns["kondisi_buku"].HeaderText = "Kondisi Buku";
                            dgvLaporan.Columns["denda"].HeaderText = "Denda (Rp)";
                            dgvLaporan.Columns["status"].HeaderText = "Status";
                            dgvLaporan.Columns["catatan"].HeaderText = "Catatan";
                            // Format kolom tanggal
                            dgvLaporan.Columns["tanggal_pinjam"].DefaultCellStyle.Format = "dd MMM yyyy";
                            dgvLaporan.Columns["tanggal_kembali"].DefaultCellStyle.Format = "dd MMM yyyy";
                            // Format kolom denda
                            dgvLaporan.Columns["denda"].DefaultCellStyle.Format = "N0";
                            dgvLaporan.Columns["denda"].DefaultCellStyle.Alignment =
                            DataGridViewContentAlignment.MiddleRight;
                            // Urutan kolom yang rapi
                            dgvLaporan.Columns["nama_pengunjung"].DisplayIndex = 0;
                            dgvLaporan.Columns["judul_buku"].DisplayIndex = 1;
                            dgvLaporan.Columns["tanggal_pinjam"].DisplayIndex = 2;
                            dgvLaporan.Columns["tanggal_kembali"].DisplayIndex = 3;
                            dgvLaporan.Columns["kondisi_buku"].DisplayIndex = 4;
                            dgvLaporan.Columns["denda"].DisplayIndex = 5;
                            dgvLaporan.Columns["status"].DisplayIndex = 6;
                            dgvLaporan.Columns["catatan"].DisplayIndex = 7;
                            // Lebar kolom otomatis
                            dgvLaporan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            dgvLaporan.Columns["judul_buku"].AutoSizeMode =
                            DataGridViewAutoSizeColumnMode.Fill;
                        }
                        if (dtLaporan.Rows.Count == 0)
                        {
                            MessageBox.Show("Belum ada data pengembalian.", "Informasi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Terjadi kesalahan saat memuat data:\n" + ex.Message,
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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