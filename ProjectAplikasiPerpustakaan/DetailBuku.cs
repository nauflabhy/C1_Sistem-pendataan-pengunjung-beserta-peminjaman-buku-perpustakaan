using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ProjectAplikasiPerpustakaan
{
    public partial class DetailBuku : Form
    {
        private readonly SqlConnection conn;
        private readonly string connectionString =
            "Data Source=NAUFAL\\NZO2;Initial Catalog=db_perpustakaan;Integrated Security=True";

        private readonly int idBuku;

        public DetailBuku(int idBuku, string judulBuku)
        {
            InitializeComponent();
            conn = new SqlConnection(connectionString);
            this.idBuku = idBuku;

            // Set judul form dengan nama buku
            this.Text = $"Detail Buku - {judulBuku}";
        }

        private void DetailBuku_Load(object sender, EventArgs e)
        {
            TampilkanDetailBuku();
        }

        private void TampilkanDetailBuku()
        {
            try
            {
                conn.Open();

                string query = @"
                    SELECT 
                        kode_buku, judul, pengarang, penerbit, 
                        tahun_terbit, kategori, stok_total, 
                        stok_tersedia, lokasi
                    FROM BUKU 
                    WHERE id_buku = @idBuku";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@idBuku", idBuku);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblKodeBuku.Text = reader["kode_buku"].ToString();
                            lblJudul.Text = reader["judul"].ToString();
                            lblPengarang.Text = reader["pengarang"].ToString();
                            lblPenerbit.Text = reader["penerbit"].ToString();
                            lblTahunTerbit.Text = reader["tahun_terbit"].ToString();
                            lblKategori.Text = reader["kategori"].ToString();
                            lblStokTotal.Text = reader["stok_total"].ToString();
                            lblStokTersedia.Text = reader["stok_tersedia"].ToString();
                            lblLokasi.Text = reader["lokasi"].ToString();

                            // Warnai stok tersedia jika sedikit
                            int stok = Convert.ToInt32(reader["stok_tersedia"]);
                            if (stok <= 2)
                            {
                                lblStokTersedia.ForeColor = Color.Red;
                                lblStokTersedia.Font = new Font(lblStokTersedia.Font, FontStyle.Bold);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat detail buku:\n" + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        // ================== TOMBOL TUTUP ==================
        private void btnTutup_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}