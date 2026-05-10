namespace ProjectAplikasiPerpustakaan
{
    partial class Pinjam
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNIK = new System.Windows.Forms.TextBox();
            this.txtNamaLengkap = new System.Windows.Forms.TextBox();
            this.txtNoHp = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPerguruan = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnAjukanPeminjaman = new System.Windows.Forms.Button();
            this.btnKembali = new System.Windows.Forms.Button();
            this.lblKodeBuku = new System.Windows.Forms.Label();
            this.lblJudulBuku = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Roboto Mono", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(831, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(294, 32);
            this.label2.TabIndex = 5;
            this.label2.Text = "FORM PEMINJAMAN BUKU";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(787, 296);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Nama Lengkap";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(787, 346);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "No Hp";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(787, 252);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "NIK";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(787, 395);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Email";
            // 
            // txtNIK
            // 
            this.txtNIK.Location = new System.Drawing.Point(954, 249);
            this.txtNIK.Name = "txtNIK";
            this.txtNIK.Size = new System.Drawing.Size(231, 26);
            this.txtNIK.TabIndex = 10;
            // 
            // txtNamaLengkap
            // 
            this.txtNamaLengkap.Location = new System.Drawing.Point(954, 296);
            this.txtNamaLengkap.Name = "txtNamaLengkap";
            this.txtNamaLengkap.Size = new System.Drawing.Size(231, 26);
            this.txtNamaLengkap.TabIndex = 11;
            // 
            // txtNoHp
            // 
            this.txtNoHp.Location = new System.Drawing.Point(954, 346);
            this.txtNoHp.Name = "txtNoHp";
            this.txtNoHp.Size = new System.Drawing.Size(231, 26);
            this.txtNoHp.TabIndex = 12;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(954, 395);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(231, 26);
            this.txtEmail.TabIndex = 13;
            // 
            // txtPerguruan
            // 
            this.txtPerguruan.Location = new System.Drawing.Point(954, 445);
            this.txtPerguruan.Name = "txtPerguruan";
            this.txtPerguruan.Size = new System.Drawing.Size(231, 26);
            this.txtPerguruan.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(787, 448);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 20);
            this.label7.TabIndex = 16;
            this.label7.Text = "Perguruan/Sekolah";
            // 
            // btnAjukanPeminjaman
            // 
            this.btnAjukanPeminjaman.Location = new System.Drawing.Point(1035, 524);
            this.btnAjukanPeminjaman.Name = "btnAjukanPeminjaman";
            this.btnAjukanPeminjaman.Size = new System.Drawing.Size(169, 49);
            this.btnAjukanPeminjaman.TabIndex = 18;
            this.btnAjukanPeminjaman.Text = "Ajukan Peminjaman";
            this.btnAjukanPeminjaman.UseVisualStyleBackColor = true;
            this.btnAjukanPeminjaman.Click += new System.EventHandler(this.btnAjukanPeminjaman_Click);
            // 
            // btnKembali
            // 
            this.btnKembali.Location = new System.Drawing.Point(788, 524);
            this.btnKembali.Name = "btnKembali";
            this.btnKembali.Size = new System.Drawing.Size(165, 49);
            this.btnKembali.TabIndex = 19;
            this.btnKembali.Text = "Kembali";
            this.btnKembali.UseVisualStyleBackColor = true;
            this.btnKembali.Click += new System.EventHandler(this.btnKembali_Click);
            // 
            // lblKodeBuku
            // 
            this.lblKodeBuku.AutoSize = true;
            this.lblKodeBuku.Location = new System.Drawing.Point(827, 172);
            this.lblKodeBuku.Name = "lblKodeBuku";
            this.lblKodeBuku.Size = new System.Drawing.Size(83, 20);
            this.lblKodeBuku.TabIndex = 20;
            this.lblKodeBuku.Text = "KodeBuku";
            // 
            // lblJudulBuku
            // 
            this.lblJudulBuku.AutoSize = true;
            this.lblJudulBuku.Location = new System.Drawing.Point(1041, 172);
            this.lblJudulBuku.Name = "lblJudulBuku";
            this.lblJudulBuku.Size = new System.Drawing.Size(84, 20);
            this.lblJudulBuku.TabIndex = 21;
            this.lblJudulBuku.Text = "JudulBuku";
            // 
            // Pinjam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1813, 726);
            this.Controls.Add(this.lblJudulBuku);
            this.Controls.Add(this.lblKodeBuku);
            this.Controls.Add(this.btnKembali);
            this.Controls.Add(this.btnAjukanPeminjaman);
            this.Controls.Add(this.txtPerguruan);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtNoHp);
            this.Controls.Add(this.txtNamaLengkap);
            this.Controls.Add(this.txtNIK);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "Pinjam";
            this.Text = " ";
            this.Load += new System.EventHandler(this.Pinjam_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtNIK;
        private System.Windows.Forms.TextBox txtNamaLengkap;
        private System.Windows.Forms.TextBox txtNoHp;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPerguruan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnAjukanPeminjaman;
        private System.Windows.Forms.Button btnKembali;
        private System.Windows.Forms.Label lblKodeBuku;
        private System.Windows.Forms.Label lblJudulBuku;
    }
}