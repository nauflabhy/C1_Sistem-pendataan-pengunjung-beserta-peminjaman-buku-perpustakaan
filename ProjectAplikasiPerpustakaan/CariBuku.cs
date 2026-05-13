using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class CariBuku : Form
    {
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        private DataTable dtBuku;
        private BindingSource bsBuku = new BindingSource();

        public CariBuku()
        {
            InitializeComponent();
        }


        // ================== LOAD DATA BUKU (dipanggil oleh tombol) ==================
        private void LoadDataBuku()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM vw_DaftarBuku ORDER BY judul ASC";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        dtBuku = new DataTable();
                        da.Fill(dtBuku);
                        bsBuku.DataSource = dtBuku;
                        dataGridView1.DataSource = bsBuku;
                    }

                    dataGridView1.AutoSizeColumnsMode =
                        DataGridViewAutoSizeColumnsMode.Fill;

                    if (dataGridView1.Columns["id_buku"] != null)
                        dataGridView1.Columns["id_buku"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data buku:\n" + ex.Message);
            }

        }

        // ================== TOMBOL LOAD DATABASE ==================
        private void btnLoadDatabase_Click(object sender, EventArgs e)
        {
            LoadDataBuku();
            txtCariBuku.Focus(); 
        }

        // ================== PENCARIAN OTOMATIS ==================
        private void CariBukuByKeyword()
        {
            string keyword = txtCariBuku.Text.Trim();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    if (string.IsNullOrEmpty(keyword))
                    {
                        string query = "SELECT * FROM vw_DaftarBuku ORDER BY judul ASC";

                        using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                        {
                            dtBuku = new DataTable();
                            da.Fill(dtBuku);
                            bsBuku.DataSource = dtBuku;
                            dataGridView1.DataSource = bsBuku;
                        }
                    }
                    else
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_SearchBuku", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@keyword", keyword);

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                dtBuku = new DataTable();
                                da.Fill(dtBuku);
                                dataGridView1.DataSource = dtBuku;
                            }
                        }
                    }

                    if (dataGridView1.Columns["id_buku"] != null)
                        dataGridView1.Columns["id_buku"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mencari buku:\n" + ex.Message);
            }
        }


        // ================== Skenario SQL Injection ==================
        private void CariBukuByKeyword1()
        {
            string keyword = txtCariBuku.Text.Trim();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
            SELECT *
            FROM vw_DaftarBuku
            WHERE judul LIKE '%" + keyword + @"%'
               OR pengarang LIKE '%" + keyword + @"%'
               OR kategori LIKE '%" + keyword + @"%'
               OR kode_buku LIKE '%" + keyword + @"%'";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        dtBuku = new DataTable();
                        da.Fill(dtBuku);
                        bsBuku.DataSource = dtBuku;
                        dataGridView1.DataSource = bsBuku;
                    }

                    if (dataGridView1.Columns["id_buku"] != null)
                        dataGridView1.Columns["id_buku"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mencari buku:\n" + ex.Message);
            }
        }

        private void txtCariBuku_TextChanged(object sender, EventArgs e)
        {
            CariBukuByKeyword1();
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            CariBukuByKeyword();
            txtCariBuku.Focus();
        }

        // ================== FORM LOAD (Hanya inisialisasi, tidak load data) ==================
        private void CariBuku_Load(object sender, EventArgs e)
        {
            txtCariBuku.Focus();

            bindingNavigator1.BindingSource = bsBuku;
            txtCariBuku.Focus();
        }

        // ================== TOMBOL PINJAM ==================
        private void btnPinjam_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silakan pilih buku yang akan dipinjam terlebih dahulu.",
                                "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                int idBuku = Convert.ToInt32(selectedRow.Cells["id_buku"].Value);
                string kodeBuku = selectedRow.Cells["kode_buku"].Value?.ToString() ?? "";
                string judulBuku = selectedRow.Cells["judul"].Value?.ToString() ?? "";
                int stokTersedia = Convert.ToInt32(selectedRow.Cells["stok_tersedia"].Value);

                MessageBox.Show($"Debug Info:\nID: {idBuku}\nKode: {kodeBuku}\nJudul: {judulBuku}\nStok: {stokTersedia}",
                                "Data Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (stokTersedia <= 0)
                {
                    MessageBox.Show("Maaf, buku ini sedang tidak tersedia.", "Stok Habis",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Pinjam formPinjam = new Pinjam(idBuku, kodeBuku, judulBuku);
                formPinjam.ShowDialog();

                LoadDataBuku();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR:\n" + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== TOMBOL DETAIL BUKU ==================
        private void btnDetailBuku_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silakan pilih buku terlebih dahulu untuk melihat detail.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                int idBuku = Convert.ToInt32(selectedRow.Cells["id_buku"].Value);
                string judulBuku = selectedRow.Cells["judul"].Value?.ToString() ?? "";

                DetailBuku formDetail = new DetailBuku(idBuku, judulBuku);
                formDetail.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka detail buku:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== TOMBOL RIWAYAT BUKU DIPINJAM ==================
        private void btnBukuDipinjam_Click(object sender, EventArgs e)
        {
            try
            {
                BukuDipinjamPengunjung formRiwayat = new BukuDipinjamPengunjung();
                formRiwayat.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka form Riwayat Buku Dipinjam:\n" + ex.Message,
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                LoginMenu formLogin = new LoginMenu();


                formLogin.Show();


                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka halaman login:\n" + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       // ======================= SQL Injection ====================================


    }
}