namespace VietBaIT.CommonLibrary
{
    partial class frm_SignInfor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_SignInfor));
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem10 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem11 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem12 = new Janus.Windows.EditControls.UIComboBoxItem();
            this.sysColor = new System.Windows.Forms.Label();
            this.cmdQuit = new Janus.Windows.EditControls.UIButton();
            this.cmdOK = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.chkGhiLai = new Janus.Windows.EditControls.UICheckBox();
            this.cboFontName = new System.Windows.Forms.ComboBox();
            this.txtBaoCao = new Janus.Windows.GridEX.EditControls.EditBox();
            this.cboFontStyle = new Janus.Windows.EditControls.UIComboBox();
            this.txtNoiDungKy = new System.Windows.Forms.TextBox();
            this.cboFontSize = new System.Windows.Forms.ComboBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.PricTure = new System.Windows.Forms.PictureBox();
            this.txtTieuDe = new Janus.Windows.GridEX.EditControls.EditBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PricTure)).BeginInit();
            this.SuspendLayout();
            // 
            // sysColor
            // 
            this.sysColor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.sysColor.Dock = System.Windows.Forms.DockStyle.Top;
            this.sysColor.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sysColor.ForeColor = System.Drawing.Color.Maroon;
            this.sysColor.Location = new System.Drawing.Point(0, 0);
            this.sysColor.Name = "sysColor";
            this.sysColor.Size = new System.Drawing.Size(845, 61);
            this.sysColor.TabIndex = 0;
            this.sysColor.Text = "TÙY BIẾN TRÌNH KÝ CHO CÁC BÁO CÁO";
            this.sysColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmdQuit
            // 
            this.cmdQuit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdQuit.Image = ((System.Drawing.Image)(resources.GetObject("cmdQuit.Image")));
            this.cmdQuit.Location = new System.Drawing.Point(421, 548);
            this.cmdQuit.Name = "cmdQuit";
            this.cmdQuit.Size = new System.Drawing.Size(115, 28);
            this.cmdQuit.TabIndex = 3;
            this.cmdQuit.Text = "&Thoát Form(Esc)";
            this.cmdQuit.ToolTipText = "Thoát Form hiện tại";
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdOK.Image = ((System.Drawing.Image)(resources.GetObject("cmdOK.Image")));
            this.cmdOK.Location = new System.Drawing.Point(282, 548);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(133, 28);
            this.cmdOK.TabIndex = 2;
            this.cmdOK.Text = "&Chấp nhận(Ctrl+A)";
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click_2);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.txtTieuDe);
            this.uiGroupBox1.Controls.Add(this.label6);
            this.uiGroupBox1.Controls.Add(this.chkGhiLai);
            this.uiGroupBox1.Controls.Add(this.cboFontName);
            this.uiGroupBox1.Controls.Add(this.txtBaoCao);
            this.uiGroupBox1.Controls.Add(this.cboFontStyle);
            this.uiGroupBox1.Controls.Add(this.txtNoiDungKy);
            this.uiGroupBox1.Controls.Add(this.cboFontSize);
            this.uiGroupBox1.Controls.Add(this.Label5);
            this.uiGroupBox1.Controls.Add(this.Label4);
            this.uiGroupBox1.Controls.Add(this.Label3);
            this.uiGroupBox1.Controls.Add(this.Label2);
            this.uiGroupBox1.Controls.Add(this.Label1);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox1.Image = ((System.Drawing.Image)(resources.GetObject("uiGroupBox1.Image")));
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 61);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(845, 481);
            this.uiGroupBox1.TabIndex = 1;
            this.uiGroupBox1.Text = "&Thông tin trình ký";
            this.uiGroupBox1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2003;
            // 
            // chkGhiLai
            // 
            this.chkGhiLai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkGhiLai.Checked = true;
            this.chkGhiLai.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGhiLai.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkGhiLai.ForeColor = System.Drawing.Color.Navy;
            this.chkGhiLai.Image = ((System.Drawing.Image)(resources.GetObject("chkGhiLai.Image")));
            this.chkGhiLai.Location = new System.Drawing.Point(105, 453);
            this.chkGhiLai.Name = "chkGhiLai";
            this.chkGhiLai.Size = new System.Drawing.Size(251, 23);
            this.chkGhiLai.TabIndex = 4;
            this.chkGhiLai.Text = "&Ghi lại cho lần sau dùng";
            this.chkGhiLai.ToolTipText = "Thông tin sẽ được lưu lại cho báo cáo trên";
            // 
            // cboFontName
            // 
            this.cboFontName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFontName.FormattingEnabled = true;
            this.cboFontName.Location = new System.Drawing.Point(105, 80);
            this.cboFontName.Name = "cboFontName";
            this.cboFontName.Size = new System.Drawing.Size(709, 23);
            this.cboFontName.TabIndex = 0;
            // 
            // txtBaoCao
            // 
            this.txtBaoCao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBaoCao.Enabled = false;
            this.txtBaoCao.Location = new System.Drawing.Point(105, 23);
            this.txtBaoCao.Name = "txtBaoCao";
            this.txtBaoCao.Size = new System.Drawing.Size(709, 21);
            this.txtBaoCao.TabIndex = 20;
            // 
            // cboFontStyle
            // 
            this.cboFontStyle.ComboStyle = Janus.Windows.EditControls.ComboStyle.DropDownList;
            uiComboBoxItem10.FormatStyle.Alpha = 0;
            uiComboBoxItem10.IsSeparator = false;
            uiComboBoxItem10.Text = "Chữ đậm";
            uiComboBoxItem10.Value = "Chữ đậm";
            uiComboBoxItem11.FormatStyle.Alpha = 0;
            uiComboBoxItem11.IsSeparator = false;
            uiComboBoxItem11.Text = "Chữ nghiêng";
            uiComboBoxItem11.Value = "Chữ nghiêng";
            uiComboBoxItem12.FormatStyle.Alpha = 0;
            uiComboBoxItem12.IsSeparator = false;
            uiComboBoxItem12.Text = "Chữ thường";
            uiComboBoxItem12.Value = "Chữ thường";
            this.cboFontStyle.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem10,
            uiComboBoxItem11,
            uiComboBoxItem12});
            this.cboFontStyle.Location = new System.Drawing.Point(105, 111);
            this.cboFontStyle.Name = "cboFontStyle";
            this.cboFontStyle.SelectedIndex = 0;
            this.cboFontStyle.Size = new System.Drawing.Size(212, 21);
            this.cboFontStyle.TabIndex = 1;
            this.cboFontStyle.Text = "Chữ đậm";
            // 
            // txtNoiDungKy
            // 
            this.txtNoiDungKy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNoiDungKy.Location = new System.Drawing.Point(105, 137);
            this.txtNoiDungKy.Multiline = true;
            this.txtNoiDungKy.Name = "txtNoiDungKy";
            this.txtNoiDungKy.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtNoiDungKy.Size = new System.Drawing.Size(709, 314);
            this.txtNoiDungKy.TabIndex = 3;
            this.txtNoiDungKy.WordWrap = false;
            // 
            // cboFontSize
            // 
            this.cboFontSize.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboFontSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFontSize.FormattingEnabled = true;
            this.cboFontSize.Location = new System.Drawing.Point(377, 110);
            this.cboFontSize.Name = "cboFontSize";
            this.cboFontSize.Size = new System.Drawing.Size(437, 23);
            this.cboFontSize.TabIndex = 2;
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(323, 114);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(45, 15);
            this.Label5.TabIndex = 18;
            this.Label5.Text = "Cỡ chữ";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(22, 137);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(75, 15);
            this.Label4.TabIndex = 17;
            this.Label4.Text = "Tên báo cáo";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(22, 110);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(55, 15);
            this.Label3.TabIndex = 15;
            this.Label3.Text = "Kiểu chữ";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(22, 83);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(54, 15);
            this.Label2.TabIndex = 12;
            this.Label2.Text = "Font chữ";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(22, 23);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(75, 15);
            this.Label1.TabIndex = 9;
            this.Label1.Text = "Tên báo cáo";
            // 
            // PricTure
            // 
            this.PricTure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.PricTure.Image = ((System.Drawing.Image)(resources.GetObject("PricTure.Image")));
            this.PricTure.Location = new System.Drawing.Point(1, 0);
            this.PricTure.Name = "PricTure";
            this.PricTure.Size = new System.Drawing.Size(76, 61);
            this.PricTure.TabIndex = 14;
            this.PricTure.TabStop = false;
            // 
            // txtTieuDe
            // 
            this.txtTieuDe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTieuDe.Location = new System.Drawing.Point(105, 50);
            this.txtTieuDe.Name = "txtTieuDe";
            this.txtTieuDe.Size = new System.Drawing.Size(709, 21);
            this.txtTieuDe.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 15);
            this.label6.TabIndex = 21;
            this.label6.Text = "&Tiêu đề";
            // 
            // frm_SignInfor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 578);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.PricTure);
            this.Controls.Add(this.cmdOK);
            this.Controls.Add(this.cmdQuit);
            this.Controls.Add(this.sysColor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_SignInfor";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VietBaIT JSC-HIS.Thông tin trình ký";
            this.Load += new System.EventHandler(this.frm_SignInfor_Load_1);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_SignInfor_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PricTure)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label sysColor;
        private Janus.Windows.EditControls.UIButton cmdQuit;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        internal System.Windows.Forms.TextBox txtNoiDungKy;
        internal System.Windows.Forms.ComboBox cboFontSize;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label4;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.PictureBox PricTure;
        public Janus.Windows.EditControls.UIComboBox cboFontStyle;
        public Janus.Windows.GridEX.EditControls.EditBox txtBaoCao;
        public Janus.Windows.EditControls.UIButton cmdOK;
        public System.Windows.Forms.ComboBox cboFontName;
        public Janus.Windows.EditControls.UICheckBox chkGhiLai;
        public Janus.Windows.GridEX.EditControls.EditBox txtTieuDe;
        internal System.Windows.Forms.Label label6;
    }
}