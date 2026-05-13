using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class btnKembali : Form
    {
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        private DataTable dtPengajuan;
        private readonly int idUser;
        private readonly string namaAdmin;
        private readonly string roleAdmin;
        private BindingSource bsPengajuan = new BindingSource();

        public btnKembali(int idUser, string namaAdmin, string roleAdmin)
        {
            InitializeComponent();

            this.idUser = idUser;
            this.namaAdmin = namaAdmin;
            this.roleAdmin = roleAdmin;
        }

        private void DaftarPengajuan_Load(object sender, EventArgs e)
        {
            SetupDataGridView();

            bindingNavigator1.BindingSource = bsPengajuan;   // 🔥 INI PENTING

            bsPengajuan.PositionChanged += bsPengajuan_PositionChanged; // bonus

            LoadDataPengajuan();
        }

        private void bsPengajuan_PositionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[bsPengajuan.Position].Selected = true;
            }
        }

        private void SetupDataGridView()
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
        }

        // ================== LOAD DATA PENGAJUAN ==================
        private void LoadDataPengajuan()
        {
            string query = @"
        SELECT
            pm.id_peminjaman,
            p.nama_lengkap AS Nama_Pengunjung,
            p.nik AS NIK,
            p.no_hp AS No_HP,
            p.email AS Email,
            b.kode_buku AS Kode_Buku,
            b.judul AS Judul_Buku,
            b.pengarang AS Pengarang,
            pm.tanggal_ajuan AS Tanggal_Ajuan,
            pm.tanggal_disetujui AS Tanggal_Disetujui,
            pm.status AS Status,
            pm.alasan_tolak AS Alasan_Tolak
        FROM PEMINJAMAN pm
        JOIN PENGUNJUNG p ON pm.id_pengunjung = p.id_pengunjung
        JOIN BUKU b ON pm.id_buku = b.id_buku
        ORDER BY 
            CASE 
                WHEN pm.status = 'menunggu' THEN 1
                WHEN pm.status = 'dipinjam' THEN 2
                WHEN pm.status = 'ditolak' THEN 3
                ELSE 4 
            END,
            pm.tanggal_ajuan DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                {
                    dtPengajuan = new DataTable();

                    da.Fill(dtPengajuan);

                    // reset datasource dulu
                    dataGridView1.DataSource = null;

                    bsPengajuan.DataSource = dtPengajuan;
                    dataGridView1.DataSource = bsPengajuan;

                    // refresh tampilan
                    dataGridView1.Refresh();

                    // Sembunyikan kolom ID
                    if (dataGridView1.Columns["id_peminjaman"] != null)
                        dataGridView1.Columns["id_peminjaman"].Visible = false;

                    // Atur urutan kolom
                    dataGridView1.Columns["Nama_Pengunjung"].DisplayIndex = 0;
                    dataGridView1.Columns["NIK"].DisplayIndex = 1;
                    dataGridView1.Columns["No_HP"].DisplayIndex = 2;
                    dataGridView1.Columns["Email"].DisplayIndex = 3;
                    dataGridView1.Columns["Kode_Buku"].DisplayIndex = 4;
                    dataGridView1.Columns["Judul_Buku"].DisplayIndex = 5;
                    dataGridView1.Columns["Pengarang"].DisplayIndex = 6;
                    dataGridView1.Columns["Tanggal_Ajuan"].DisplayIndex = 7;
                    dataGridView1.Columns["Tanggal_Disetujui"].DisplayIndex = 8;
                    dataGridView1.Columns["Status"].DisplayIndex = 9;
                    dataGridView1.Columns["Alasan_Tolak"].DisplayIndex = 10;

                    WarnaiBarisBerdasarkanStatus();
                    dataGridView1.ClearSelection();
                }

                this.Text = $"Daftar Pengajuan Peminjaman - Total: {dtPengajuan.Rows.Count} pengajuan";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat daftar pengajuan:\n" + ex.Message,
                    "Error Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void WarnaiBarisBerdasarkanStatus()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["Status"].Value == null) continue;

                string status = row.Cells["Status"].Value.ToString().ToLower();

                switch (status)
                {
                    case "menunggu":
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightYellow;
                        break;
                    case "dipinjam":
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                        break;
                    case "ditolak":
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightCoral;
                        break;
                    case "selesai":
                        row.DefaultCellStyle.BackColor = System.Drawing.Color.LightGray;
                        break;
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadDataPengajuan();
        }

        // ================== TERIMA (SETUJUI) ==================
        private void btnTerima_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silakan pilih salah satu pengajuan terlebih dahulu.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idPeminjaman = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_peminjaman"].Value);
            string judulBuku = dataGridView1.SelectedRows[0].Cells["Judul_Buku"].Value?.ToString() ?? "";

            DialogResult konfirm = MessageBox.Show(
                $"Setujui peminjaman buku:\n\n{judulBuku}\n\n" +
                "Buku akan dipinjamkan selama 7 hari.",
                "Konfirmasi Persetujuan",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (konfirm != DialogResult.Yes) return;

            string query = @"
        UPDATE PEMINJAMAN
        SET status = 'dipinjam',
            id_user = @id_user,                    -- Diubah dari id_admin
            tanggal_disetujui = GETDATE(),
            tanggal_pinjam = GETDATE(),
            tanggal_jatuh_tempo = DATEADD(DAY, 7, GETDATE())
        WHERE id_peminjaman = @id_peminjaman;

        UPDATE BUKU
        SET stok_tersedia = stok_tersedia - 1
        WHERE id_buku = (SELECT id_buku FROM PEMINJAMAN WHERE id_peminjaman = @id_peminjaman);";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_peminjaman", idPeminjaman);
                    cmd.Parameters.AddWithValue("@id_user", this.idUser); // atau kirim id_user asli

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("✅ Peminjaman berhasil disetujui!\nStok buku telah dikurangi.",
                            "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataPengajuan();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menyetujui peminjaman:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================== TOLAK ==================
        private void btnTolak_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silakan pilih pengajuan yang ingin ditolak.",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idPeminjaman = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id_peminjaman"].Value);
            string judulBuku = dataGridView1.SelectedRows[0].Cells["Judul_Buku"].Value.ToString();

            // Ganti InputBox dengan cara C# yang lebih modern
            using (Form prompt = new Form())
            {
                prompt.Width = 450;
                prompt.Height = 200;
                prompt.FormBorderStyle = FormBorderStyle.FixedDialog;
                prompt.Text = $"Tolak Pengajuan - {judulBuku}";
                prompt.StartPosition = FormStartPosition.CenterScreen;
                prompt.MaximizeBox = false;
                prompt.MinimizeBox = false;

                Label label = new Label()
                {
                    Left = 20,
                    Top = 20,
                    Text = "Masukkan alasan penolakan:",
                    AutoSize = true
                };

                TextBox textBox = new TextBox()
                {
                    Left = 20,
                    Top = 50,
                    Width = 390,
                    Height = 60,
                    Multiline = true
                };

                Button confirmation = new Button()
                {
                    Text = "Tolak",
                    Left = 230,
                    Width = 80,
                    Top = 120,
                    DialogResult = DialogResult.OK
                };

                Button cancel = new Button()
                {
                    Text = "Batal",
                    Left = 320,
                    Width = 80,
                    Top = 120,
                    DialogResult = DialogResult.Cancel
                };

                prompt.Controls.Add(label);
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(cancel);

                prompt.AcceptButton = confirmation;
                prompt.CancelButton = cancel;

                if (prompt.ShowDialog() == DialogResult.OK)
                {
                    string alasan = textBox.Text.Trim();

                    if (string.IsNullOrWhiteSpace(alasan))
                    {
                        MessageBox.Show("Alasan penolakan tidak boleh kosong.",
                            "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Proses tolak ke database
                    ProsesTolakPengajuan(idPeminjaman, alasan);
                }
            }
        }

        // Method terpisah agar lebih rapi
        private void ProsesTolakPengajuan(int idPeminjaman, string alasan)
        {
            string query = @"
            UPDATE PEMINJAMAN
            SET status = 'ditolak',
                alasan_tolak = @alasan,
                id_user = @id_user
            WHERE id_peminjaman = @id_peminjaman";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id_peminjaman", idPeminjaman);
                    cmd.Parameters.AddWithValue("@alasan", alasan);
                    cmd.Parameters.AddWithValue("@id_user", this.idUser);

                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Pengajuan berhasil ditolak.",
                            "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDataPengajuan(); // refresh daftar
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal menolak pengajuan:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Optional: Double click untuk melihat detail
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Bisa buka form detail jika diperlukan nanti
                MessageBox.Show("Detail pengajuan akan ditampilkan di sini (bisa dikembangkan).", "Info");
            }
        }

        // Event kosong yang tidak perlu (bisa dihapus)
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();

            // Buka kembali Form Admin dan kirim data admin yang sedang login
            btnKembali form = new btnKembali(idUser, namaAdmin, roleAdmin);
        }

        private void btnSelesai_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data terlebih dahulu.");
                return;
            }

            DataGridViewRow row = dataGridView1.SelectedRows[0];

            string status =
                row.Cells["Status"].Value.ToString().ToLower();

            if (status != "dipinjam")
            {
                MessageBox.Show(
                    "Hanya buku dengan status DIPINJAM yang bisa dikembalikan.",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return;
            }

            int idPeminjaman =
                Convert.ToInt32(row.Cells["id_peminjaman"].Value);

            string kodeBuku =
                row.Cells["Kode_Buku"].Value.ToString();

            string judulBuku =
                row.Cells["Judul_Buku"].Value.ToString();

            DateTime tanggalAjuan =
                Convert.ToDateTime(row.Cells["Tanggal_Ajuan"].Value);

            FormPengembalian form = new FormPengembalian(
                idPeminjaman,
                kodeBuku,
                judulBuku,
                tanggalAjuan,
                idUser
            );

            form.ShowDialog();

            LoadDataPengajuan();

        }
    }
}