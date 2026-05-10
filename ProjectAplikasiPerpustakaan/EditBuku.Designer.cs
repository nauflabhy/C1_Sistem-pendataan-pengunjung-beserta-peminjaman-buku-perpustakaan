namespace ProjectAplikasiPerpustakaan
{
    partial class EditBuku
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtKodeBuku = new System.Windows.Forms.TextBox();
            this.txtJudulBuku = new System.Windows.Forms.TextBox();
            this.txtPengarang = new System.Windows.Forms.TextBox();
            this.txtPenerbit = new System.Windows.Forms.TextBox();
            this.txtTahunTerbit = new System.Windows.Forms.TextBox();
            this.txtStokTersedia = new System.Windows.Forms.TextBox();
            this.txtLokasi = new System.Windows.Forms.TextBox();
            this.btlBatal = new System.Windows.Forms.Button();
            this.btlUpdateBuku = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbKategori = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(863, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Edit Buku";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(734, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Kode Buku:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(734, 244);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Judul Buku:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(734, 301);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "Pengarang:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(735, 356);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "Penerbit:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(735, 414);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "Tahun Terbit:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(735, 527);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 20);
            this.label8.TabIndex = 7;
            this.label8.Text = "Stok Tersedia:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(735, 586);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 20);
            this.label9.TabIndex = 8;
            this.label9.Text = "Lokasi:";
            // 
            // txtKodeBuku
            // 
            this.txtKodeBuku.Location = new System.Drawing.Point(885, 190);
            this.txtKodeBuku.Name = "txtKodeBuku";
            this.txtKodeBuku.Size = new System.Drawing.Size(257, 26);
            this.txtKodeBuku.TabIndex = 9;
            this.txtKodeBuku.TextChanged += new System.EventHandler(this.txtKodeBuku_TextChanged);
            // 
            // txtJudulBuku
            // 
            this.txtJudulBuku.Location = new System.Drawing.Point(885, 244);
            this.txtJudulBuku.Name = "txtJudulBuku";
            this.txtJudulBuku.Size = new System.Drawing.Size(257, 26);
            this.txtJudulBuku.TabIndex = 10;
            this.txtJudulBuku.TextChanged += new System.EventHandler(this.txtJudulBuku_TextChanged);
            // 
            // txtPengarang
            // 
            this.txtPengarang.Location = new System.Drawing.Point(885, 298);
            this.txtPengarang.Name = "txtPengarang";
            this.txtPengarang.Size = new System.Drawing.Size(257, 26);
            this.txtPengarang.TabIndex = 11;
            this.txtPengarang.TextChanged += new System.EventHandler(this.txtPengarang_TextChanged);
            // 
            // txtPenerbit
            // 
            this.txtPenerbit.Location = new System.Drawing.Point(885, 353);
            this.txtPenerbit.Name = "txtPenerbit";
            this.txtPenerbit.Size = new System.Drawing.Size(257, 26);
            this.txtPenerbit.TabIndex = 12;
            this.txtPenerbit.TextChanged += new System.EventHandler(this.txtPenerbit_TextChanged);
            // 
            // txtTahunTerbit
            // 
            this.txtTahunTerbit.Location = new System.Drawing.Point(885, 408);
            this.txtTahunTerbit.Name = "txtTahunTerbit";
            this.txtTahunTerbit.Size = new System.Drawing.Size(257, 26);
            this.txtTahunTerbit.TabIndex = 13;
            this.txtTahunTerbit.TextChanged += new System.EventHandler(this.txtTahunTerbit_TextChanged);
            // 
            // txtStokTersedia
            // 
            this.txtStokTersedia.Location = new System.Drawing.Point(885, 521);
            this.txtStokTersedia.Name = "txtStokTersedia";
            this.txtStokTersedia.Size = new System.Drawing.Size(257, 26);
            this.txtStokTersedia.TabIndex = 15;
            this.txtStokTersedia.TextChanged += new System.EventHandler(this.txtStokTersedia_TextChanged);
            // 
            // txtLokasi
            // 
            this.txtLokasi.Location = new System.Drawing.Point(885, 583);
            this.txtLokasi.Name = "txtLokasi";
            this.txtLokasi.Size = new System.Drawing.Size(257, 26);
            this.txtLokasi.TabIndex = 16;
            this.txtLokasi.TextChanged += new System.EventHandler(this.txtLokasi_TextChanged);
            // 
            // btlBatal
            // 
            this.btlBatal.Location = new System.Drawing.Point(738, 689);
            this.btlBatal.Name = "btlBatal";
            this.btlBatal.Size = new System.Drawing.Size(167, 60);
            this.btlBatal.TabIndex = 17;
            this.btlBatal.Text = "Batal";
            this.btlBatal.UseVisualStyleBackColor = true;
            this.btlBatal.Click += new System.EventHandler(this.btlBatal_Click);
            // 
            // btlUpdateBuku
            // 
            this.btlUpdateBuku.Location = new System.Drawing.Point(993, 689);
            this.btlUpdateBuku.Name = "btlUpdateBuku";
            this.btlUpdateBuku.Size = new System.Drawing.Size(167, 60);
            this.btlUpdateBuku.TabIndex = 18;
            this.btlUpdateBuku.Text = "Update Buku";
            this.btlUpdateBuku.UseVisualStyleBackColor = true;
            this.btlUpdateBuku.Click += new System.EventHandler(this.btlUpdateBuku_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(735, 472);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 20);
            this.label7.TabIndex = 19;
            this.label7.Text = "Kategori:";
            // 
            // cmbKategori
            // 
            this.cmbKategori.FormattingEnabled = true;
            this.cmbKategori.Items.AddRange(new object[] {
            "Fiksi",
            "Non Fiksi"});
            this.cmbKategori.Location = new System.Drawing.Point(885, 469);
            this.cmbKategori.Name = "cmbKategori";
            this.cmbKategori.Size = new System.Drawing.Size(265, 28);
            this.cmbKategori.TabIndex = 22;
            this.cmbKategori.SelectedIndexChanged += new System.EventHandler(this.cmbKategori_SelectedIndexChanged);
            // 
            // EditBuku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1802, 834);
            this.Controls.Add(this.cmbKategori);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btlUpdateBuku);
            this.Controls.Add(this.btlBatal);
            this.Controls.Add(this.txtLokasi);
            this.Controls.Add(this.txtStokTersedia);
            this.Controls.Add(this.txtTahunTerbit);
            this.Controls.Add(this.txtPenerbit);
            this.Controls.Add(this.txtPengarang);
            this.Controls.Add(this.txtJudulBuku);
            this.Controls.Add(this.txtKodeBuku);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "EditBuku";
            this.Text = "EditBuku";
            this.Load += new System.EventHandler(this.EditBuku_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtKodeBuku;
        private System.Windows.Forms.TextBox txtJudulBuku;
        private System.Windows.Forms.TextBox txtPengarang;
        private System.Windows.Forms.TextBox txtPenerbit;
        private System.Windows.Forms.TextBox txtTahunTerbit;
        private System.Windows.Forms.TextBox txtStokTersedia;
        private System.Windows.Forms.TextBox txtLokasi;
        private System.Windows.Forms.Button btlBatal;
        private System.Windows.Forms.Button btlUpdateBuku;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbKategori;
    }
}