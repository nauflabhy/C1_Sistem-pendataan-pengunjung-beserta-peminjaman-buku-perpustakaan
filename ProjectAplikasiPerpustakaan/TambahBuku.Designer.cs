namespace ProjectAplikasiPerpustakaan
{
    partial class TambahBuku
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btlBatal = new System.Windows.Forms.Button();
            this.btnTambah = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtKodeBuku = new System.Windows.Forms.TextBox();
            this.txtJudul = new System.Windows.Forms.TextBox();
            this.txtPengarang = new System.Windows.Forms.TextBox();
            this.txtPenerbit = new System.Windows.Forms.TextBox();
            this.txtTahunTerbit = new System.Windows.Forms.TextBox();
            this.txtStokTersedia = new System.Windows.Forms.TextBox();
            this.txtLokasi = new System.Windows.Forms.TextBox();
            this.cmbKategori = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btlBatal
            // 
            this.btlBatal.Location = new System.Drawing.Point(634, 660);
            this.btlBatal.Name = "btlBatal";
            this.btlBatal.Size = new System.Drawing.Size(208, 72);
            this.btlBatal.TabIndex = 0;
            this.btlBatal.Text = "Batal";
            this.btlBatal.UseVisualStyleBackColor = true;
            this.btlBatal.Click += new System.EventHandler(this.btlBatal_Click);
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(896, 660);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(208, 72);
            this.btnTambah.TabIndex = 1;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(633, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Kode Buku:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(633, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Judul:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(633, 203);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Pengarang:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(633, 261);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Penerbit:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(633, 314);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Tahun Terbit:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(633, 368);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "Kategori:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(633, 447);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(111, 20);
            this.label9.TabIndex = 10;
            this.label9.Text = "Stok Tersedia:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(633, 515);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 20);
            this.label10.TabIndex = 11;
            this.label10.Text = "Lokasi:";
            // 
            // txtKodeBuku
            // 
            this.txtKodeBuku.Location = new System.Drawing.Point(839, 90);
            this.txtKodeBuku.Name = "txtKodeBuku";
            this.txtKodeBuku.Size = new System.Drawing.Size(265, 26);
            this.txtKodeBuku.TabIndex = 12;
            this.txtKodeBuku.TextChanged += new System.EventHandler(this.txtKodeBuku_TextChanged);
            // 
            // txtJudul
            // 
            this.txtJudul.Location = new System.Drawing.Point(839, 150);
            this.txtJudul.Name = "txtJudul";
            this.txtJudul.Size = new System.Drawing.Size(265, 26);
            this.txtJudul.TabIndex = 13;
            this.txtJudul.TextChanged += new System.EventHandler(this.txtJudul_TextChanged);
            // 
            // txtPengarang
            // 
            this.txtPengarang.Location = new System.Drawing.Point(839, 203);
            this.txtPengarang.Name = "txtPengarang";
            this.txtPengarang.Size = new System.Drawing.Size(265, 26);
            this.txtPengarang.TabIndex = 14;
            this.txtPengarang.TextChanged += new System.EventHandler(this.txtPengarang_TextChanged);
            // 
            // txtPenerbit
            // 
            this.txtPenerbit.Location = new System.Drawing.Point(839, 255);
            this.txtPenerbit.Name = "txtPenerbit";
            this.txtPenerbit.Size = new System.Drawing.Size(265, 26);
            this.txtPenerbit.TabIndex = 15;
            this.txtPenerbit.TextChanged += new System.EventHandler(this.txtPenerbit_TextChanged);
            // 
            // txtTahunTerbit
            // 
            this.txtTahunTerbit.Location = new System.Drawing.Point(839, 308);
            this.txtTahunTerbit.Name = "txtTahunTerbit";
            this.txtTahunTerbit.Size = new System.Drawing.Size(265, 26);
            this.txtTahunTerbit.TabIndex = 16;
            this.txtTahunTerbit.TextChanged += new System.EventHandler(this.txtTahunTerbit_TextChanged);
            // 
            // txtStokTersedia
            // 
            this.txtStokTersedia.Location = new System.Drawing.Point(839, 447);
            this.txtStokTersedia.Name = "txtStokTersedia";
            this.txtStokTersedia.Size = new System.Drawing.Size(265, 26);
            this.txtStokTersedia.TabIndex = 19;
            this.txtStokTersedia.TextChanged += new System.EventHandler(this.txtStokTersedia_TextChanged);
            // 
            // txtLokasi
            // 
            this.txtLokasi.Location = new System.Drawing.Point(839, 512);
            this.txtLokasi.Name = "txtLokasi";
            this.txtLokasi.Size = new System.Drawing.Size(265, 26);
            this.txtLokasi.TabIndex = 20;
            this.txtLokasi.TextChanged += new System.EventHandler(this.txtLokasi_TextChanged);
            // 
            // cmbKategori
            // 
            this.cmbKategori.FormattingEnabled = true;
            this.cmbKategori.Items.AddRange(new object[] {
            "Fiksi",
            "Non Fiksi"});
            this.cmbKategori.Location = new System.Drawing.Point(839, 368);
            this.cmbKategori.Name = "cmbKategori";
            this.cmbKategori.Size = new System.Drawing.Size(265, 28);
            this.cmbKategori.TabIndex = 21;
            this.cmbKategori.SelectedIndexChanged += new System.EventHandler(this.cmbKategori_SelectedIndexChanged);
            // 
            // TambahBuku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1691, 844);
            this.Controls.Add(this.cmbKategori);
            this.Controls.Add(this.txtLokasi);
            this.Controls.Add(this.txtStokTersedia);
            this.Controls.Add(this.txtTahunTerbit);
            this.Controls.Add(this.txtPenerbit);
            this.Controls.Add(this.txtPengarang);
            this.Controls.Add(this.txtJudul);
            this.Controls.Add(this.txtKodeBuku);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.btlBatal);
            this.Name = "TambahBuku";
            this.Text = "TambahBuku";
            this.Load += new System.EventHandler(this.TambahBuku_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btlBatal;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtKodeBuku;
        private System.Windows.Forms.TextBox txtJudul;
        private System.Windows.Forms.TextBox txtPengarang;
        private System.Windows.Forms.TextBox txtPenerbit;
        private System.Windows.Forms.TextBox txtTahunTerbit;
        private System.Windows.Forms.TextBox txtStokTersedia;
        private System.Windows.Forms.TextBox txtLokasi;
        private System.Windows.Forms.ComboBox cmbKategori;
    }
}