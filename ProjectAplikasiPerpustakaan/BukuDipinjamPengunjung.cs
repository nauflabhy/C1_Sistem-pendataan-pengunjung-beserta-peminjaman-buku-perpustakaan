using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class BukuDipinjamPengunjung : Form
    {
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        private DataTable dtDipinjam;

        public BukuDipinjamPengunjung()
        {
            InitializeComponent();
        }

        private void BukuDipinjamPengunjung_Load(object sender, EventArgs e)
        {
            SetupDataGridView();
            TampilkanBukuDipinjam();
        }

        private void SetupDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        // ================== TAMPILKAN BUKU YANG SEDANG DIPINJAM ==================
        private void TampilkanBukuDipinjam()
        {
            string query = @"
                SELECT 
                    pm.id_peminjaman,
                    b.kode_buku AS Kode_Buku,
                    b.judul AS Judul_Buku,
                    b.pengarang AS Pengarang,
                    pm.tanggal_pinjam AS Tanggal_Pinjam,
                    pm.tanggal_jatuh_tempo AS Jatuh_Tempo,
                    DATEDIFF(DAY, GETDATE(), pm.tanggal_jatuh_tempo) AS Sisa_Hari,
                    pm.status AS Status
                FROM PEMINJAMAN pm
                JOIN PENGUNJUNG p ON pm.id_pengunjung = p.id_pengunjung
                JOIN BUKU b ON pm.id_buku = b.id_buku
                WHERE pm.status IN ('dipinjam', 'disetujui')
                ORDER BY pm.tanggal_jatuh_tempo ASC";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    dtDipinjam = new DataTable();
                    da.Fill(dtDipinjam);
                    dataGridView1.DataSource = dtDipinjam;

                    // Sembunyikan ID
                    if (dataGridView1.Columns["id_peminjaman"] != null)
                        dataGridView1.Columns["id_peminjaman"].Visible = false;

                    if (dataGridView1.Columns["Sisa_Hari"] != null)
                        dataGridView1.Columns["Sisa_Hari"].HeaderText = "Sisa Hari";

                    WarnaiBarisBerdasarkanStatus();
                }

                this.Text = $"Buku Sedang Dipinjam - Total: {dtDipinjam.Rows.Count} buku";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data buku yang dipinjam:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WarnaiBarisBerdasarkanStatus()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Status"].Value == null) continue;

                string status = row.Cells["Status"].Value.ToString().ToLower();
                int sisaHari = 0;
                if (row.Cells["Sisa_Hari"].Value != null)
                    int.TryParse(row.Cells["Sisa_Hari"].Value.ToString(), out sisaHari);

                if (sisaHari < 0)
                {
                    row.DefaultCellStyle.BackColor = Color.LightCoral;
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
                else if (sisaHari <= 3)
                {
                    row.DefaultCellStyle.BackColor = Color.Orange;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }
        }

        // ================== EVENT HANDLER ==================
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kosongkan dulu, bisa dikembangkan nanti
        }

        private void btnKembali_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        // Optional: Tombol Refresh
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            TampilkanBukuDipinjam();
        }
    }
}