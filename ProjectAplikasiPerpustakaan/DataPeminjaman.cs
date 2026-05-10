using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ProjectAplikasiPerpustakaan
{
    public partial class DataPeminjaman : Form
    {
        private readonly string namaPengguna;
        private readonly string rolePengguna;
        public DataPeminjaman(string kodeBuku, string judulBuku, string nik,
                              string namaLengkap, string noHp, string email, string peguruan,
                              string namaPengguna, string rolePengguna)
        {
            InitializeComponent();
            this.namaPengguna = namaPengguna;
            this.rolePengguna = rolePengguna;

            // Isi semua label dengan data yang dikirim dari form Pinjam
            lblKodeBuku.Text = kodeBuku;
            lblJudulBuku.Text = judulBuku;
            lblNIK.Text = nik;
            lblNamaLengkap.Text = namaLengkap;
            lblNoHp.Text = noHp;
            lblEmail.Text = email;
            lblPeguruan.Text = peguruan;
            lblTanggalPeminjaman.Text = DateTime.Now.ToString("dd MMMM yyyy, HH:mm");
        }

        private void DataPeminjaman_Load(object sender, EventArgs e)
        {

        }


        private void lblKodeBuku_Click(object sender, EventArgs e)
        {

        }

        private void lblJudulBuku_Click(object sender, EventArgs e)
        {

        }

        private void lblNIK_Click(object sender, EventArgs e)
        {

        }

        private void lblNamaLengkap_Click(object sender, EventArgs e)
        {

        }

        private void lblNoHp_Click(object sender, EventArgs e)
        {

        }

        private void lblEmail_Click(object sender, EventArgs e)
        {

        }

        private void lblPeguruan_Click(object sender, EventArgs e)
        {

        }

        private void lblTanggalPeminjaman_Click(object sender, EventArgs e)
        {

        }

        private void btnKembali_Click(object sender, EventArgs e)
        {
            this.Close();

            // Buka form CariBuku lagi
            CariBuku formCariBuku = new CariBuku();
            formCariBuku.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Judul_Click(object sender, EventArgs e)
        {

        }
    }
}
