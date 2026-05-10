namespace ProjectAplikasiPerpustakaan
{
    partial class Admin
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnDaftarPengajuan = new System.Windows.Forms.Button();
            this.btnEditBuku = new System.Windows.Forms.Button();
            this.btnLoadDatabase = new System.Windows.Forms.Button();
            this.btnLaporan = new System.Windows.Forms.Button();
            this.btnTambahBuku = new System.Windows.Forms.Button();
            this.btnHapusBuku = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.btnCari = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCariBuku = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(839, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "ADMIN MENU";
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(30, 12);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(184, 47);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Log out";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(30, 158);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1683, 513);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnDaftarPengajuan
            // 
            this.btnDaftarPengajuan.Location = new System.Drawing.Point(246, 730);
            this.btnDaftarPengajuan.Name = "btnDaftarPengajuan";
            this.btnDaftarPengajuan.Size = new System.Drawing.Size(183, 65);
            this.btnDaftarPengajuan.TabIndex = 3;
            this.btnDaftarPengajuan.Text = "Daftar Pengajuan";
            this.btnDaftarPengajuan.UseVisualStyleBackColor = true;
            this.btnDaftarPengajuan.Click += new System.EventHandler(this.btnDaftarPengajuan_Click);
            // 
            // btnEditBuku
            // 
            this.btnEditBuku.Location = new System.Drawing.Point(467, 730);
            this.btnEditBuku.Name = "btnEditBuku";
            this.btnEditBuku.Size = new System.Drawing.Size(183, 65);
            this.btnEditBuku.TabIndex = 5;
            this.btnEditBuku.Text = "Edit Buku";
            this.btnEditBuku.UseVisualStyleBackColor = true;
            this.btnEditBuku.Click += new System.EventHandler(this.btnEditBuku_Click);
            // 
            // btnLoadDatabase
            // 
            this.btnLoadDatabase.Location = new System.Drawing.Point(52, 730);
            this.btnLoadDatabase.Name = "btnLoadDatabase";
            this.btnLoadDatabase.Size = new System.Drawing.Size(147, 65);
            this.btnLoadDatabase.TabIndex = 6;
            this.btnLoadDatabase.Text = "Load Database";
            this.btnLoadDatabase.UseVisualStyleBackColor = true;
            this.btnLoadDatabase.Click += new System.EventHandler(this.btnLoadDatabase_Click);
            // 
            // btnLaporan
            // 
            this.btnLaporan.Location = new System.Drawing.Point(1073, 730);
            this.btnLaporan.Name = "btnLaporan";
            this.btnLaporan.Size = new System.Drawing.Size(161, 65);
            this.btnLaporan.TabIndex = 8;
            this.btnLaporan.Text = "Laporan";
            this.btnLaporan.UseVisualStyleBackColor = true;
            this.btnLaporan.Click += new System.EventHandler(this.btnLaporan_Click);
            // 
            // btnTambahBuku
            // 
            this.btnTambahBuku.Location = new System.Drawing.Point(686, 730);
            this.btnTambahBuku.Name = "btnTambahBuku";
            this.btnTambahBuku.Size = new System.Drawing.Size(147, 65);
            this.btnTambahBuku.TabIndex = 9;
            this.btnTambahBuku.Text = "Tambah Buku";
            this.btnTambahBuku.UseVisualStyleBackColor = true;
            this.btnTambahBuku.Click += new System.EventHandler(this.btnTambahBuku_Click);
            // 
            // btnHapusBuku
            // 
            this.btnHapusBuku.Location = new System.Drawing.Point(881, 730);
            this.btnHapusBuku.Name = "btnHapusBuku";
            this.btnHapusBuku.Size = new System.Drawing.Size(147, 65);
            this.btnHapusBuku.TabIndex = 10;
            this.btnHapusBuku.Text = "Hapus Buku";
            this.btnHapusBuku.UseVisualStyleBackColor = true;
            this.btnHapusBuku.Click += new System.EventHandler(this.btnHapusBuku_Click);
            // 
            // btnCari
            // 
            this.btnCari.Location = new System.Drawing.Point(662, 101);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(90, 32);
            this.btnCari.TabIndex = 13;
            this.btnCari.Text = "Cari";
            this.btnCari.UseVisualStyleBackColor = true;
            this.btnCari.Click += new System.EventHandler(this.btnCari_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Cari Buku";
            // 
            // txtCariBuku
            // 
            this.txtCariBuku.Location = new System.Drawing.Point(128, 101);
            this.txtCariBuku.Name = "txtCariBuku";
            this.txtCariBuku.Size = new System.Drawing.Size(514, 26);
            this.txtCariBuku.TabIndex = 11;
            this.txtCariBuku.TextChanged += new System.EventHandler(this.txtCariBuku_TextChanged);
            // 
            // Admin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1735, 830);
            this.Controls.Add(this.btnCari);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCariBuku);
            this.Controls.Add(this.btnHapusBuku);
            this.Controls.Add(this.btnTambahBuku);
            this.Controls.Add(this.btnLaporan);
            this.Controls.Add(this.btnLoadDatabase);
            this.Controls.Add(this.btnEditBuku);
            this.Controls.Add(this.btnDaftarPengajuan);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.label1);
            this.Name = "Admin";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Admin_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnDaftarPengajuan;
        private System.Windows.Forms.Button btnEditBuku;
        private System.Windows.Forms.Button btnLoadDatabase;
        private System.Windows.Forms.Button btnLaporan;
        private System.Windows.Forms.Button btnTambahBuku;
        private System.Windows.Forms.Button btnHapusBuku;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCariBuku;
    }
}