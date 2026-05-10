using System;
using System.Data.SqlClient;
using System.Drawing.Text;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class EditBuku : Form
    {
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        private readonly int idBuku;

        public EditBuku(int idBuku)
        {
            InitializeComponent();
            this.idBuku = idBuku;

            // DEBUG 1: Cek ID yang diterima
            MessageBox.Show($"Form EditBuku dibuka dengan ID Buku = {idBuku}",
                "DEBUG Constructor", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void EditBuku_Load(object sender, EventArgs e)
        {
            // DEBUG 2
            MessageBox.Show("Event Load Form sudah berjalan", "DEBUG Load Event",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            SetupComboBoxKategori();
            LoadDataBuku();
            SetupInputRestrictions();
        }

        private void SetupComboBoxKategori()
        {
            cmbKategori.Items.Clear();
            cmbKategori.Items.Add("Fiksi IT");
            cmbKategori.Items.Add("Non-Fiksi IT");
            cmbKategori.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadDataBuku()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT kode_buku, judul, pengarang, penerbit,
                               tahun_terbit, kategori, stok_tersedia, lokasi
                        FROM BUKU
                        WHERE id_buku = @idBuku";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@idBuku", idBuku);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtKodeBuku.Text = reader["kode_buku"].ToString();
                                txtJudulBuku.Text = reader["judul"].ToString();
                                txtPengarang.Text = reader["pengarang"].ToString();
                                txtPenerbit.Text = reader["penerbit"]?.ToString() ?? "";
                                txtTahunTerbit.Text = reader["tahun_terbit"]?.ToString() ?? "";
                                txtStokTersedia.Text = reader["stok_tersedia"].ToString();
                                txtLokasi.Text = reader["lokasi"]?.ToString() ?? "";

                                string kategori = reader["kategori"]?.ToString()?.Trim() ?? "";
                                if (!string.IsNullOrEmpty(kategori))
                                {
                                    cmbKategori.SelectedItem = kategori;
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Data buku dengan ID {idBuku} tidak ditemukan!",
                                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data buku:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupInputRestrictions()
        {
            txtKodeBuku.MaxLength = 20;
            txtJudulBuku.MaxLength = 200;
            txtPengarang.MaxLength = 100;
            txtPenerbit.MaxLength = 100;
            txtLokasi.MaxLength = 50;
            txtTahunTerbit.MaxLength = 4;
            txtStokTersedia.MaxLength = 5;

            txtKodeBuku.KeyPress += (s, ev) => { if (!char.IsLetterOrDigit(ev.KeyChar) && ev.KeyChar != '-' && ev.KeyChar != '\b') ev.Handled = true; };
            txtJudulBuku.KeyPress += AllowAlphanumericWithSpace;
            txtPengarang.KeyPress += AllowAlphanumericWithSpace;
            txtPenerbit.KeyPress += AllowAlphanumericWithSpace;
            txtLokasi.KeyPress += AllowAlphanumericWithSpace;
            txtTahunTerbit.KeyPress += AllowOnlyNumbers;
            txtStokTersedia.KeyPress += AllowOnlyNumbers;
        }

        private void AllowAlphanumericWithSpace(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != '\b')
                e.Handled = true;
        }

        private void AllowOnlyNumbers(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
                e.Handled = true;
        }

        private void btlBatal_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtKodeBuku_TextChanged(object sender, EventArgs e) { }
        private void txtJudulBuku_TextChanged(object sender, EventArgs e) { }
        private void txtPengarang_TextChanged(object sender, EventArgs e) { }
        private void txtPenerbit_TextChanged(object sender, EventArgs e) { }
        private void txtTahunTerbit_TextChanged(object sender, EventArgs e) { }
        private void txtStokTersedia_TextChanged(object sender, EventArgs e) { }
        private void txtLokasi_TextChanged(object sender, EventArgs e) { }
        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e) { }

        private void btlUpdateBuku_Click(object sender, EventArgs e)
        {
            // Validasi
            if (string.IsNullOrWhiteSpace(txtKodeBuku.Text) ||
                string.IsNullOrWhiteSpace(txtJudulBuku.Text) ||
                string.IsNullOrWhiteSpace(txtPengarang.Text) ||
                cmbKategori.SelectedIndex == -1)
            {
                MessageBox.Show("Kode Buku, Judul, Pengarang, dan Kategori harus diisi!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtStokTersedia.Text, out int stok) || stok < 0)
            {
                MessageBox.Show("Stok Tersedia harus angka positif!",
                    "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        UPDATE BUKU
                        SET kode_buku = @kode,
                            judul = @judul,
                            pengarang = @pengarang,
                            penerbit = @penerbit,
                            tahun_terbit = @tahun,
                            kategori = @kategori,
                            stok_tersedia = @stokTersedia,
                            lokasi = @lokasi
                        WHERE id_buku = @idBuku";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@kode", txtKodeBuku.Text.Trim().ToUpper());
                        cmd.Parameters.AddWithValue("@judul", txtJudulBuku.Text.Trim());
                        cmd.Parameters.AddWithValue("@pengarang", txtPengarang.Text.Trim());
                        cmd.Parameters.AddWithValue("@penerbit", string.IsNullOrWhiteSpace(txtPenerbit.Text) ? (object)DBNull.Value : txtPenerbit.Text.Trim());
                        cmd.Parameters.AddWithValue("@tahun", string.IsNullOrWhiteSpace(txtTahunTerbit.Text) ? (object)DBNull.Value : txtTahunTerbit.Text.Trim());
                        cmd.Parameters.AddWithValue("@kategori", cmbKategori.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@stokTersedia", stok);
                        cmd.Parameters.AddWithValue("@lokasi", string.IsNullOrWhiteSpace(txtLokasi.Text) ? (object)DBNull.Value : txtLokasi.Text.Trim());
                        cmd.Parameters.AddWithValue("@idBuku", idBuku);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("✅ Data buku berhasil diperbarui!", "Sukses",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal update data:\n" + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        // ================== UNTUK MENGHILANGKAN ERROR DESIGNER ==================
        private void EditBuku_Load_1(object sender, EventArgs e)
        {
            LoadDataBuku();
        }




    }
}