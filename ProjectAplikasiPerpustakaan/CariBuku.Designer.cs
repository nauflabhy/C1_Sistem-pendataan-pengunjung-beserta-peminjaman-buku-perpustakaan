namespace ProjectAplikasiPerpustakaan
{
    partial class CariBuku
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnDetailBuku = new System.Windows.Forms.Button();
            this.btnPinjam = new System.Windows.Forms.Button();
            this.txtCariBuku = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCari = new System.Windows.Forms.Button();
            this.btnBukuDipinjam = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnLoadDatabase = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(29, 156);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1747, 508);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // btnDetailBuku
            // 
            this.btnDetailBuku.Location = new System.Drawing.Point(62, 719);
            this.btnDetailBuku.Name = "btnDetailBuku";
            this.btnDetailBuku.Size = new System.Drawing.Size(149, 68);
            this.btnDetailBuku.TabIndex = 2;
            this.btnDetailBuku.Text = "Detail Buku";
            this.btnDetailBuku.UseVisualStyleBackColor = true;
            this.btnDetailBuku.Click += new System.EventHandler(this.btnDetailBuku_Click);
            // 
            // btnPinjam
            // 
            this.btnPinjam.Location = new System.Drawing.Point(250, 723);
            this.btnPinjam.Name = "btnPinjam";
            this.btnPinjam.Size = new System.Drawing.Size(149, 59);
            this.btnPinjam.TabIndex = 3;
            this.btnPinjam.Text = "Pinjam Buku";
            this.btnPinjam.UseVisualStyleBackColor = true;
            this.btnPinjam.Click += new System.EventHandler(this.btnPinjam_Click);
            // 
            // txtCariBuku
            // 
            this.txtCariBuku.Location = new System.Drawing.Point(535, 55);
            this.txtCariBuku.Name = "txtCariBuku";
            this.txtCariBuku.Size = new System.Drawing.Size(514, 26);
            this.txtCariBuku.TabIndex = 5;
            this.txtCariBuku.TextChanged += new System.EventHandler(this.txtCariBuku_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(435, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Cari Buku";
            // 
            // btnCari
            // 
            this.btnCari.Location = new System.Drawing.Point(1072, 55);
            this.btnCari.Name = "btnCari";
            this.btnCari.Size = new System.Drawing.Size(90, 32);
            this.btnCari.TabIndex = 7;
            this.btnCari.Text = "Cari";
            this.btnCari.UseVisualStyleBackColor = true;
            this.btnCari.Click += new System.EventHandler(this.btnCari_Click);
            // 
            // btnBukuDipinjam
            // 
            this.btnBukuDipinjam.Location = new System.Drawing.Point(439, 724);
            this.btnBukuDipinjam.Name = "btnBukuDipinjam";
            this.btnBukuDipinjam.Size = new System.Drawing.Size(149, 59);
            this.btnBukuDipinjam.TabIndex = 8;
            this.btnBukuDipinjam.Text = "Buku yang dipinjam";
            this.btnBukuDipinjam.UseVisualStyleBackColor = true;
            this.btnBukuDipinjam.Click += new System.EventHandler(this.btnBukuDipinjam_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(90, 41);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(121, 46);
            this.btnLogin.TabIndex = 9;
            this.btnLogin.Text = "Log In";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnLoadDatabase
            // 
            this.btnLoadDatabase.Location = new System.Drawing.Point(615, 725);
            this.btnLoadDatabase.Name = "btnLoadDatabase";
            this.btnLoadDatabase.Size = new System.Drawing.Size(158, 59);
            this.btnLoadDatabase.TabIndex = 10;
            this.btnLoadDatabase.Text = "Load Database";
            this.btnLoadDatabase.UseVisualStyleBackColor = true;
            this.btnLoadDatabase.Click += new System.EventHandler(this.btnLoadDatabase_Click);
            // 
            // CariBuku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1788, 832);
            this.Controls.Add(this.btnLoadDatabase);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnBukuDipinjam);
            this.Controls.Add(this.btnCari);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCariBuku);
            this.Controls.Add(this.btnPinjam);
            this.Controls.Add(this.btnDetailBuku);
            this.Controls.Add(this.dataGridView1);
            this.Name = "CariBuku";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.CariBuku_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnDetailBuku;
        private System.Windows.Forms.Button btnPinjam;
        private System.Windows.Forms.TextBox txtCariBuku;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCari;
        private System.Windows.Forms.Button btnBukuDipinjam;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnLoadDatabase;
    }
}