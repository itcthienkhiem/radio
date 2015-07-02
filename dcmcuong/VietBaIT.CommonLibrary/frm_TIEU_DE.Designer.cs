namespace VietBaIT.CommonLibrary
{
    partial class frm_TIEU_DE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_TIEU_DE));
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtTieuDe = new Janus.Windows.GridEX.EditControls.EditBox();
            this.txtBaoCao = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cmdOK = new Janus.Windows.EditControls.UIButton();
            this.cmdQuit = new Janus.Windows.EditControls.UIButton();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.cmdOK);
            this.uiGroupBox1.Controls.Add(this.cmdQuit);
            this.uiGroupBox1.Controls.Add(this.txtTieuDe);
            this.uiGroupBox1.Controls.Add(this.txtBaoCao);
            this.uiGroupBox1.Controls.Add(this.label6);
            this.uiGroupBox1.Controls.Add(this.Label1);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(408, 140);
            this.uiGroupBox1.TabIndex = 24;
            this.uiGroupBox1.Text = "&Thông tin ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 25;
            this.label6.Text = "&Tiêu đề";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(12, 29);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(68, 13);
            this.Label1.TabIndex = 24;
            this.Label1.Text = "Tên báo cáo";
            // 
            // txtTieuDe
            // 
            this.txtTieuDe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTieuDe.Enabled = false;
            this.txtTieuDe.Location = new System.Drawing.Point(86, 56);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.Size = new System.Drawing.Size(310, 20);
            this.txtTieuDe.TabIndex = 27;
            // 
            // txtBaoCao
            // 
            this.txtBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBaoCao.Enabled = false;
            this.txtBaoCao.Location = new System.Drawing.Point(86, 29);
            this.txtBaoCao.Name = "txtBaoCao";
            this.txtBaoCao.Size = new System.Drawing.Size(310, 20);
            this.txtBaoCao.TabIndex = 26;
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdOK.Image = ((System.Drawing.Image)(resources.GetObject("cmdOK.Image")));
            this.cmdOK.Location = new System.Drawing.Point(86, 91);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(133, 28);
            this.cmdOK.TabIndex = 28;
            this.cmdOK.Text = "&Chấp nhận(Ctrl+A)";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // cmdQuit
            // 
            this.cmdQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdQuit.Image = ((System.Drawing.Image)(resources.GetObject("cmdQuit.Image")));
            this.cmdQuit.Location = new System.Drawing.Point(225, 91);
            this.cmdQuit.Name = "cmdQuit";
            this.cmdQuit.Size = new System.Drawing.Size(115, 28);
            this.cmdQuit.TabIndex = 29;
            this.cmdQuit.Text = "&Thoát Form(Esc)";
            this.cmdQuit.ToolTipText = "Thoát Form hiện tại";
            this.cmdQuit.Click += new System.EventHandler(this.cmdQuit_Click);
            // 
            // frm_TIEU_DE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 156);
            this.Controls.Add(this.uiGroupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_TIEU_DE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin tiêu đề báo cáo";
            this.Load += new System.EventHandler(this.frm_TIEU_DE_Load);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label Label1;
        public Janus.Windows.GridEX.EditControls.EditBox txtTieuDe;
        public Janus.Windows.GridEX.EditControls.EditBox txtBaoCao;
        public Janus.Windows.EditControls.UIButton cmdOK;
        private Janus.Windows.EditControls.UIButton cmdQuit;
    }
}