using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class Admin : Form
    {
        private readonly string namaAdmin;
        private readonly string roleAdmin;
        private readonly int idUser;
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        private DataTable dtBuku;
        private BindingSource bsBuku = new BindingSource();

        public Admin(int idUser, string nama, string role)
        {
            InitializeComponent();
            this.namaAdmin = nama;
            this.roleAdmin = role;
            this.idUser = idUser;
        }

        public Admin()
        {
            InitializeComponent();
        }

        // ================== FORM LOAD (TIDAK load data otomatis) ==================
        private void Admin_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'db_perpustakaanDataSet1.vw_DaftarBuku' table. You can move, or remove it, as needed.
            this.vw_DaftarBukuTableAdapter.Fill(this.db_perpustakaanDataSet1.vw_DaftarBuku);
            // TODO: This line of code loads data into the 'db_perpustakaanDataSet.BUKU' table. You can move, or remove it, as needed.
            this.bUKUTableAdapter.Fill(this.db_perpustakaanDataSet.BUKU);
            if (!string.IsNullOrEmpty(namaAdmin))
            {
                this.Text = $"Admin Panel - {namaAdmin}";
            }

            bindingNavigator1.BindingSource = bsBuku;
        }

        // ================== LOAD DAFTAR BUKU ==================
        private void LoadDataBuku()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                SELECT *
                FROM vw_AdminDaftarBuku
                ORDER BY judul ASC";

                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        dtBuku = new DataTable();
                        da.Fill(dtBuku);

                        bsBuku.DataSource = dtBuku;
                        dataGridView1.DataSource = bsBuku;
                    }

                    dataGridView1.AutoSizeColumnsMode =
                        DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.AllowUserToAddRows = false;
                    dataGridView1.AllowUserToDeleteRows = false;
                    dataGridView1.SelectionMode =
                        DataGridViewSelectionMode.FullRowSelect;
                    dataGridView1.MultiSelect = false;

                    if (dataGridView1.Columns["id_buku"] != null)
                        dataGridView1.Columns["id_buku"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat daftar buku:\n" + ex.Message);
            }
        }

        // ================== TOMBOL LOAD DATABASE ==================
        private void btnLoadDatabase_Click(object sender, EventArgs e)
        {
            txtCariBuku.Clear();
            LoadDataBuku();
        }

        // ================== TOMBOL EDIT BUKU ==================
        private void btnEditBuku_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih buku yang ingin diedit.", "Peringatan",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idBuku = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_buku"].Value);

            // Debug ID
            MessageBox.Show($"Anda memilih Buku ID: {idBuku}", "Debug ID",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            EditBuku formEdit = new EditBuku(idBuku);
            formEdit.ShowDialog();

            if (formEdit.DialogResult == DialogResult.OK)
            {
                LoadDataBuku(); // refresh grid
            }
        }

        // ================== TOMBOL DAFTAR PENGAJUAN ==================
        private void btnDaftarPengajuan_Click(object sender, EventArgs e)
        {
            try
            {
                btnKembali formPengajuan = new btnKembali(idUser, namaAdmin, roleAdmin);
                formPengajuan.ShowDialog();

                // Refresh data setelah kembali dari form pengajuan
                LoadDataBuku();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka daftar pengajuan:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== TOMBOL LAPORAN ==================
        private void btnLaporan_Click(object sender, EventArgs e)
        {
            try
            {
                CetakLaporan formLaporan = new CetakLaporan();
                formLaporan.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka form Laporan:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== LOGOUT ==================
        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show("Apakah Anda yakin ingin keluar?",
                "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirmasi == DialogResult.Yes)
            {
                LoginMenu formLogin = new LoginMenu();
                formLogin.Show();
                this.Close();
            }
        }

        // Event kosong (bisa dihapus jika tidak dipakai)
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void btnTambahBuku_Click(object sender, EventArgs e)
        {
            try
            {
                using (TambahBuku formTambah = new TambahBuku())
                {
                    // Buka form Tambah Buku sebagai Dialog
                    DialogResult hasil = formTambah.ShowDialog();

                    // Jika buku berhasil ditambahkan (DialogResult.OK), refresh DataGridView
                    if (hasil == DialogResult.OK)
                    {
                        LoadDataBuku(); // Refresh daftar buku otomatis
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal membuka form Tambah Buku:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHapusBuku_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silakan pilih buku yang ingin dihapus terlebih dahulu.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ambil data buku yang dipilih
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            int idBuku = Convert.ToInt32(row.Cells["id_buku"].Value);
            string kodeBuku = row.Cells["kode_buku"].Value?.ToString() ?? "-";
            string judulBuku = row.Cells["judul"].Value?.ToString() ?? "-";

            // Konfirmasi penghapusan
            DialogResult konfirmasi = MessageBox.Show(
                $"Anda yakin ingin menghapus buku berikut?\n\n" +
                $"Kode Buku : {kodeBuku}\n" +
                $"Judul     : {judulBuku}\n\n" +
                "Tindakan ini tidak dapat dibatalkan!",
                "Konfirmasi Hapus Buku",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (konfirmasi != DialogResult.Yes) return;

            // Cek apakah buku sedang dipinjam
            if (BukuSedangDipinjam(idBuku))
            {
                MessageBox.Show("Buku tidak dapat dihapus karena sedang dipinjam atau memiliki pengajuan aktif.",
                    "Tidak Dapat Dihapus", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_DeleteBuku", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_buku", idBuku);

                        int result = cmd.ExecuteNonQuery();

                        if (result > 0)
                        {
                            MessageBox.Show("Buku berhasil dihapus.");
                            CariBukuByKeyword();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menghapus buku:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== CEK APAKAH BUKU SEDANG DIPINJAM ==================
        private bool BukuSedangDipinjam(int idBuku)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                SELECT COUNT(*)
                FROM vw_BukuSedangDipinjam
                WHERE id_buku = @id_buku";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id_buku", idBuku);
                        int jumlah = Convert.ToInt32(cmd.ExecuteScalar());
                        return jumlah > 0;
                    }
                }
            }
            catch
            {
                return true;
            }
        }

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
                        LoadDataBuku();
                        return;
                    }

                    using (SqlCommand cmd =
                        new SqlCommand("sp_SearchAdminBuku", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@keyword", keyword);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            dtBuku = new DataTable();
                            da.Fill(dtBuku);
                            bsBuku.DataSource = dtBuku;
                            dataGridView1.DataSource = bsBuku;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal mencari buku:\n" + ex.Message);
            }
        }

        private void txtCariBuku_TextChanged(object sender, EventArgs e)
        {
            CariBukuByKeyword();
        }

        private void btnCari_Click(object sender, EventArgs e)
        {
            CariBukuByKeyword();
            txtCariBuku.Focus();
        }

        private void btnTestDataInjection_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show(
        "Jalankan simulasi SQL Injection?\nSemua data buku akan dimodifikasi.",
        "Test SQL Injection",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning);

            if (konfirmasi != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string injectedQuery = @"
            UPDATE BUKU
            SET
                judul = 'HACKED',
                pengarang = 'HACKED',
                penerbit = 'HACKED',
                tahun_terbit = 9999,
                kategori = 'Fiksi',
                stok_tersedia = 999,
                lokasi = 'HACKED'
            ";

                    using (SqlCommand cmd = new SqlCommand(injectedQuery, conn))
                    {
                        int result = cmd.ExecuteNonQuery();

                        MessageBox.Show(
                            $"{result} data berhasil dimodifikasi.",
                            "SQL Injection Berhasil",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }

                LoadDataBuku();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menjalankan simulasi:\n" + ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            DialogResult konfirmasi = MessageBox.Show(
         "Kembalikan data buku dari backup?",
         "Restore Database",
         MessageBoxButtons.YesNo,
         MessageBoxIcon.Question);

            if (konfirmasi != DialogResult.Yes) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
            UPDATE b
            SET
                b.kode_buku = bb.kode_buku,
                b.judul = bb.judul,
                b.pengarang = bb.pengarang,
                b.penerbit = bb.penerbit,
                b.tahun_terbit = bb.tahun_terbit,
                b.kategori = bb.kategori,
                b.stok_tersedia = bb.stok_tersedia,
                b.lokasi = bb.lokasi
            FROM BUKU b
            INNER JOIN BUKU_BACKUP bb
                ON b.id_buku = bb.id_buku";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        int result = cmd.ExecuteNonQuery();

                        MessageBox.Show(
                            $"{result} data berhasil dipulihkan.",
                            "Restore Berhasil",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }

                LoadDataBuku();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal restore:\n" + ex.Message);
            }
        }
    }
}