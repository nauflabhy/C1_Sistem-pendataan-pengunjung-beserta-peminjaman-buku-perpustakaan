using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class TambahBuku : Form
    {
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        public TambahBuku()
        {
            InitializeComponent();
        }

        private void TambahBuku_Load(object sender, EventArgs e)
        {
            SetupComboBoxKategori();
            SetupInputRestrictions();
            txtKodeBuku.Focus();
        }

        // ================== SETUP COMBOBOX KATEGORI ==================
        private void SetupComboBoxKategori()
        {
            cmbKategori.Items.Clear();
            cmbKategori.Items.Add("Fiksi");
            cmbKategori.Items.Add("Non Fiksi");
            cmbKategori.DropDownStyle = ComboBoxStyle.DropDownList; // Tidak bisa ketik manual
            cmbKategori.SelectedIndex = 0; // Default Fiksi
        }

        private void SetupInputRestrictions()
        {
            // Kode Buku → Huruf + Angka + "-"
            txtKodeBuku.KeyPress += (s, ev) =>
            {
                if (!char.IsLetterOrDigit(ev.KeyChar) && ev.KeyChar != '-' && ev.KeyChar != '\b')
                    ev.Handled = true;
            };
            txtKodeBuku.MaxLength = 20;

            // Teks lainnya
            txtJudul.KeyPress += AllowAlphanumericWithSpace;
            txtPengarang.KeyPress += AllowAlphanumericWithSpace;
            txtPenerbit.KeyPress += AllowAlphanumericWithSpace;
            txtLokasi.KeyPress += AllowAlphanumericWithSpace;

            txtJudul.MaxLength = 200;
            txtPengarang.MaxLength = 100;
            txtPenerbit.MaxLength = 100;
            txtLokasi.MaxLength = 50;

            // Tahun Terbit & Stok
            txtTahunTerbit.KeyPress += AllowOnlyNumbers;
            txtTahunTerbit.MaxLength = 4;

            txtStokTersedia.KeyPress += AllowOnlyNumbers;
            txtStokTersedia.MaxLength = 5;
        }

        private void AllowOnlyLettersWithSpace(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar) &&
                e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void AllowAlphanumericWithSpace(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) &&
                !char.IsWhiteSpace(e.KeyChar) &&
                e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void AllowOnlyNumbers(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void txtKodeBuku_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtJudul_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPengarang_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPenerbit_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTahunTerbit_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtStokTersedia_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLokasi_TextChanged(object sender, EventArgs e)
        {

        }

        private void btlBatal_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Batalkan penambahan buku?\nData yang sudah diisi akan hilang.",
                "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            // Validasi input wajib
            if (string.IsNullOrWhiteSpace(txtKodeBuku.Text) ||
       string.IsNullOrWhiteSpace(txtJudul.Text) ||
       string.IsNullOrWhiteSpace(txtPengarang.Text) ||
       cmbKategori.SelectedIndex == -1 ||
       string.IsNullOrWhiteSpace(txtStokTersedia.Text))
            {
                MessageBox.Show(
                    "Kode Buku, Judul, Pengarang, Kategori, dan Stok Tersedia wajib diisi!",
                    "Peringatan",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtStokTersedia.Text, out int stokTersedia) || stokTersedia < 0)
            {
                MessageBox.Show(
                    "Stok Tersedia harus berupa angka positif!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_TambahBuku", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@kode_buku",
                            txtKodeBuku.Text.Trim().ToUpper());

                        cmd.Parameters.AddWithValue("@judul",
                            txtJudul.Text.Trim());

                        cmd.Parameters.AddWithValue("@pengarang",
                            txtPengarang.Text.Trim());

                        cmd.Parameters.AddWithValue("@penerbit",
                            string.IsNullOrWhiteSpace(txtPenerbit.Text)
                            ? (object)DBNull.Value
                            : txtPenerbit.Text.Trim());

                        cmd.Parameters.AddWithValue("@tahun_terbit",
                            string.IsNullOrWhiteSpace(txtTahunTerbit.Text)
                            ? (object)DBNull.Value
                            : Convert.ToInt32(txtTahunTerbit.Text));

                        cmd.Parameters.AddWithValue("@kategori",
                            cmbKategori.SelectedItem.ToString());

                        cmd.Parameters.AddWithValue("@stok_tersedia",
                            stokTersedia);

                        cmd.Parameters.AddWithValue("@lokasi",
                            string.IsNullOrWhiteSpace(txtLokasi.Text)
                            ? (object)DBNull.Value
                            : txtLokasi.Text.Trim());

                        cmd.ExecuteNonQuery();

                        MessageBox.Show(
                            "✅ Buku berhasil ditambahkan!",
                            "Sukses",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        ClearForm();
                        txtKodeBuku.Focus();
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    "Database Error:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
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

        private void ClearForm()
        {
            txtKodeBuku.Clear();
            txtJudul.Clear();
            txtPengarang.Clear();
            txtPenerbit.Clear();
            txtTahunTerbit.Clear();
            txtStokTersedia.Clear();
            txtLokasi.Clear();
        }

        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
