namespace VietBaIT.DROC
{
    partial class frm_Anatomy_Projection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Anatomy_Projection));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmdDeleteAnatomy = new System.Windows.Forms.Button();
            this.cmdSaveParam = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cboDevice = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblMultiCheck = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cmdMakeAsEmerency = new System.Windows.Forms.Button();
            this.cmdDelProjection = new System.Windows.Forms.Button();
            this.cmdChooseProjection = new System.Windows.Forms.Button();
            this.cmdChooseAnatomy = new System.Windows.Forms.Button();
            this.pnlAnatomyList = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlParams = new System.Windows.Forms.Panel();
            this.CHK_FILMHD = new System.Windows.Forms.Label();
            this.chkAutoVFlip = new System.Windows.Forms.Label();
            this.chkAutoHFlip = new System.Windows.Forms.Label();
            this.txtRadCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nmrMAS = new System.Windows.Forms.NumericUpDown();
            this.nmrMA = new System.Windows.Forms.NumericUpDown();
            this.nmrKVP = new System.Windows.Forms.NumericUpDown();
            this.cboBodySize = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pnlProjectionList = new System.Windows.Forms.FlowLayoutPanel();
            this.chkIsLargeFocus = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrMAS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrMA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrKVP)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmdDeleteAnatomy);
            this.panel1.Controls.Add(this.cmdSaveParam);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.cmdClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 659);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1267, 93);
            this.panel1.TabIndex = 19;
            // 
            // cmdDeleteAnatomy
            // 
            this.cmdDeleteAnatomy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cmdDeleteAnatomy.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDeleteAnatomy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdDeleteAnatomy.Location = new System.Drawing.Point(3, 3);
            this.cmdDeleteAnatomy.Name = "cmdDeleteAnatomy";
            this.cmdDeleteAnatomy.Size = new System.Drawing.Size(252, 82);
            this.cmdDeleteAnatomy.TabIndex = 19;
            this.cmdDeleteAnatomy.Tag = "";
            this.cmdDeleteAnatomy.Text = "Xóa Tab";
            this.cmdDeleteAnatomy.UseVisualStyleBackColor = true;
            this.cmdDeleteAnatomy.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdSaveParam
            // 
            this.cmdSaveParam.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSaveParam.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSaveParam.Image = ((System.Drawing.Image)(resources.GetObject("cmdSaveParam.Image")));
            this.cmdSaveParam.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSaveParam.Location = new System.Drawing.Point(659, 6);
            this.cmdSaveParam.Name = "cmdSaveParam";
            this.cmdSaveParam.Size = new System.Drawing.Size(345, 82);
            this.cmdSaveParam.TabIndex = 5;
            this.cmdSaveParam.Text = "Lưu thông tin tham số";
            this.cmdSaveParam.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSaveParam.UseVisualStyleBackColor = true;
            this.cmdSaveParam.Click += new System.EventHandler(this.cmdSaveParam_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(3, 41);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(650, 47);
            this.progressBar1.TabIndex = 18;
            this.progressBar1.Visible = false;
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Image = ((System.Drawing.Image)(resources.GetObject("cmdClose.Image")));
            this.cmdClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdClose.Location = new System.Drawing.Point(1010, 6);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(254, 82);
            this.cmdClose.TabIndex = 6;
            this.cmdClose.Text = "Thoát";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cboDevice
            // 
            this.cboDevice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDevice.FormattingEnabled = true;
            this.cboDevice.ItemHeight = 46;
            this.cboDevice.Location = new System.Drawing.Point(253, 0);
            this.cboDevice.Name = "cboDevice";
            this.cboDevice.Size = new System.Drawing.Size(621, 54);
            this.cboDevice.TabIndex = 23;
            this.cboDevice.SelectedIndexChanged += new System.EventHandler(this.cboDevice_SelectedIndexChanged);
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.Yellow;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label24.Location = new System.Drawing.Point(4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(251, 54);
            this.label24.TabIndex = 22;
            this.label24.Text = "Chọn thiết bị cần cấu hình:";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblMultiCheck);
            this.panel2.Controls.Add(this.cmdMakeAsEmerency);
            this.panel2.Controls.Add(this.cmdDelProjection);
            this.panel2.Controls.Add(this.cmdChooseProjection);
            this.panel2.Controls.Add(this.cmdChooseAnatomy);
            this.panel2.Controls.Add(this.cboDevice);
            this.panel2.Controls.Add(this.label24);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1267, 135);
            this.panel2.TabIndex = 24;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // lblMultiCheck
            // 
            this.lblMultiCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMultiCheck.BackColor = System.Drawing.SystemColors.Control;
            this.lblMultiCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMultiCheck.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblMultiCheck.ImageIndex = 1;
            this.lblMultiCheck.ImageList = this.imageList1;
            this.lblMultiCheck.Location = new System.Drawing.Point(875, 0);
            this.lblMultiCheck.Name = "lblMultiCheck";
            this.lblMultiCheck.Size = new System.Drawing.Size(387, 54);
            this.lblMultiCheck.TabIndex = 28;
            this.lblMultiCheck.Text = "Cho phép chọn nhiều vị trí chụp?";
            this.lblMultiCheck.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblMultiCheck.Click += new System.EventHandler(this.lblMultiCheck_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "OK.PNG");
            this.imageList1.Images.SetKeyName(1, "REMOVE.PNG");
            // 
            // cmdMakeAsEmerency
            // 
            this.cmdMakeAsEmerency.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdMakeAsEmerency.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdMakeAsEmerency.Image = ((System.Drawing.Image)(resources.GetObject("cmdMakeAsEmerency.Image")));
            this.cmdMakeAsEmerency.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdMakeAsEmerency.Location = new System.Drawing.Point(875, 53);
            this.cmdMakeAsEmerency.Name = "cmdMakeAsEmerency";
            this.cmdMakeAsEmerency.Size = new System.Drawing.Size(389, 82);
            this.cmdMakeAsEmerency.TabIndex = 27;
            this.cmdMakeAsEmerency.Text = "Đánh dấu là dịch vụ mặc định";
            this.cmdMakeAsEmerency.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdMakeAsEmerency.UseVisualStyleBackColor = true;
            this.cmdMakeAsEmerency.Click += new System.EventHandler(this.cmdMakeAsEmerency_Click);
            // 
            // cmdDelProjection
            // 
            this.cmdDelProjection.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDelProjection.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelProjection.Image")));
            this.cmdDelProjection.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdDelProjection.Location = new System.Drawing.Point(542, 53);
            this.cmdDelProjection.Name = "cmdDelProjection";
            this.cmdDelProjection.Size = new System.Drawing.Size(332, 82);
            this.cmdDelProjection.TabIndex = 26;
            this.cmdDelProjection.Text = "Xóa các hướng chụp đang chọn";
            this.cmdDelProjection.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdDelProjection.UseVisualStyleBackColor = true;
            this.cmdDelProjection.Click += new System.EventHandler(this.cmdDelProjection_Click);
            // 
            // cmdChooseProjection
            // 
            this.cmdChooseProjection.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdChooseProjection.Image = ((System.Drawing.Image)(resources.GetObject("cmdChooseProjection.Image")));
            this.cmdChooseProjection.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdChooseProjection.Location = new System.Drawing.Point(253, 53);
            this.cmdChooseProjection.Name = "cmdChooseProjection";
            this.cmdChooseProjection.Size = new System.Drawing.Size(289, 82);
            this.cmdChooseProjection.TabIndex = 25;
            this.cmdChooseProjection.Text = "Chọn hướng chụp";
            this.cmdChooseProjection.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdChooseProjection.UseVisualStyleBackColor = true;
            this.cmdChooseProjection.Click += new System.EventHandler(this.cmdChooseProjection_Click);
            // 
            // cmdChooseAnatomy
            // 
            this.cmdChooseAnatomy.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdChooseAnatomy.Image = ((System.Drawing.Image)(resources.GetObject("cmdChooseAnatomy.Image")));
            this.cmdChooseAnatomy.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdChooseAnatomy.Location = new System.Drawing.Point(1, 53);
            this.cmdChooseAnatomy.Name = "cmdChooseAnatomy";
            this.cmdChooseAnatomy.Size = new System.Drawing.Size(254, 82);
            this.cmdChooseAnatomy.TabIndex = 24;
            this.cmdChooseAnatomy.Text = "Thêm vị trí chụp";
            this.cmdChooseAnatomy.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdChooseAnatomy.UseVisualStyleBackColor = true;
            this.cmdChooseAnatomy.Click += new System.EventHandler(this.cmdChooseAnatomy_Click);
            // 
            // pnlAnatomyList
            // 
            this.pnlAnatomyList.AutoScroll = true;
            this.pnlAnatomyList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlAnatomyList.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAnatomyList.Location = new System.Drawing.Point(0, 135);
            this.pnlAnatomyList.Name = "pnlAnatomyList";
            this.pnlAnatomyList.Size = new System.Drawing.Size(255, 524);
            this.pnlAnatomyList.TabIndex = 25;
            // 
            // pnlParams
            // 
            this.pnlParams.BackColor = System.Drawing.SystemColors.Control;
            this.pnlParams.Controls.Add(this.chkIsLargeFocus);
            this.pnlParams.Controls.Add(this.CHK_FILMHD);
            this.pnlParams.Controls.Add(this.chkAutoVFlip);
            this.pnlParams.Controls.Add(this.chkAutoHFlip);
            this.pnlParams.Controls.Add(this.txtRadCode);
            this.pnlParams.Controls.Add(this.label5);
            this.pnlParams.Controls.Add(this.label4);
            this.pnlParams.Controls.Add(this.label3);
            this.pnlParams.Controls.Add(this.nmrMAS);
            this.pnlParams.Controls.Add(this.nmrMA);
            this.pnlParams.Controls.Add(this.nmrKVP);
            this.pnlParams.Controls.Add(this.cboBodySize);
            this.pnlParams.Controls.Add(this.label1);
            this.pnlParams.Controls.Add(this.label2);
            this.pnlParams.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlParams.Location = new System.Drawing.Point(620, 0);
            this.pnlParams.Name = "pnlParams";
            this.pnlParams.Size = new System.Drawing.Size(392, 524);
            this.pnlParams.TabIndex = 27;
            // 
            // CHK_FILMHD
            // 
            this.CHK_FILMHD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CHK_FILMHD.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHK_FILMHD.ForeColor = System.Drawing.Color.Navy;
            this.CHK_FILMHD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.CHK_FILMHD.ImageIndex = 1;
            this.CHK_FILMHD.ImageList = this.imageList1;
            this.CHK_FILMHD.Location = new System.Drawing.Point(33, 427);
            this.CHK_FILMHD.Name = "CHK_FILMHD";
            this.CHK_FILMHD.Size = new System.Drawing.Size(310, 29);
            this.CHK_FILMHD.TabIndex = 35;
            this.CHK_FILMHD.Text = "Mỗi ảnh tính 1 Film?";
            this.CHK_FILMHD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CHK_FILMHD.Click += new System.EventHandler(this.CHK_FILMHD_Click);
            // 
            // chkAutoVFlip
            // 
            this.chkAutoVFlip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAutoVFlip.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoVFlip.ForeColor = System.Drawing.Color.Navy;
            this.chkAutoVFlip.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkAutoVFlip.ImageIndex = 1;
            this.chkAutoVFlip.ImageList = this.imageList1;
            this.chkAutoVFlip.Location = new System.Drawing.Point(33, 374);
            this.chkAutoVFlip.Name = "chkAutoVFlip";
            this.chkAutoVFlip.Size = new System.Drawing.Size(310, 29);
            this.chkAutoVFlip.TabIndex = 33;
            this.chkAutoVFlip.Text = "Lật ảnh theo phương ngang?";
            this.chkAutoVFlip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAutoVFlip.Click += new System.EventHandler(this.chkAutoVFlip_Click);
            // 
            // chkAutoHFlip
            // 
            this.chkAutoHFlip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkAutoHFlip.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoHFlip.ForeColor = System.Drawing.Color.Navy;
            this.chkAutoHFlip.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkAutoHFlip.ImageIndex = 1;
            this.chkAutoHFlip.ImageList = this.imageList1;
            this.chkAutoHFlip.Location = new System.Drawing.Point(33, 429);
            this.chkAutoHFlip.Name = "chkAutoHFlip";
            this.chkAutoHFlip.Size = new System.Drawing.Size(347, 29);
            this.chkAutoHFlip.TabIndex = 34;
            this.chkAutoHFlip.Text = "Lật ảnh theo phương thẳng đứng?";
            this.chkAutoHFlip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAutoHFlip.Visible = false;
            this.chkAutoHFlip.Click += new System.EventHandler(this.chkAutoHFlip_Click);
            // 
            // txtRadCode
            // 
            this.txtRadCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRadCode.Location = new System.Drawing.Point(128, 295);
            this.txtRadCode.Name = "txtRadCode";
            this.txtRadCode.Size = new System.Drawing.Size(252, 62);
            this.txtRadCode.TabIndex = 4;
            this.txtRadCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label5.Location = new System.Drawing.Point(3, 238);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 54);
            this.label5.TabIndex = 31;
            this.label5.Text = "mAs:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(3, 177);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(121, 54);
            this.label4.TabIndex = 30;
            this.label4.Text = "mA:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(3, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 54);
            this.label3.TabIndex = 29;
            this.label3.Text = "kVp:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // nmrMAS
            // 
            this.nmrMAS.DecimalPlaces = 2;
            this.nmrMAS.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmrMAS.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.nmrMAS.Location = new System.Drawing.Point(128, 237);
            this.nmrMAS.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nmrMAS.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nmrMAS.Name = "nmrMAS";
            this.nmrMAS.Size = new System.Drawing.Size(252, 62);
            this.nmrMAS.TabIndex = 3;
            this.nmrMAS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nmrMAS.ThousandsSeparator = true;
            this.nmrMAS.Value = new decimal(new int[] {
            125,
            0,
            0,
            131072});
            // 
            // nmrMA
            // 
            this.nmrMA.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmrMA.Location = new System.Drawing.Point(128, 176);
            this.nmrMA.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmrMA.Name = "nmrMA";
            this.nmrMA.Size = new System.Drawing.Size(252, 62);
            this.nmrMA.TabIndex = 2;
            this.nmrMA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nmrKVP
            // 
            this.nmrKVP.DecimalPlaces = 1;
            this.nmrKVP.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nmrKVP.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nmrKVP.Location = new System.Drawing.Point(128, 114);
            this.nmrKVP.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nmrKVP.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.nmrKVP.Name = "nmrKVP";
            this.nmrKVP.Size = new System.Drawing.Size(252, 62);
            this.nmrKVP.TabIndex = 1;
            this.nmrKVP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nmrKVP.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // cboBodySize
            // 
            this.cboBodySize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBodySize.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBodySize.FormattingEnabled = true;
            this.cboBodySize.ItemHeight = 55;
            this.cboBodySize.Location = new System.Drawing.Point(3, 46);
            this.cboBodySize.Name = "cboBodySize";
            this.cboBodySize.Size = new System.Drawing.Size(377, 63);
            this.cboBodySize.TabIndex = 0;
            this.cboBodySize.SelectedIndexChanged += new System.EventHandler(this.cboBodySize_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(3, 289);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 85);
            this.label1.TabIndex = 32;
            this.label1.Text = "Code:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(3, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(264, 54);
            this.label2.TabIndex = 25;
            this.label2.Text = "BODY SIZE:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pnlProjectionList);
            this.panel3.Controls.Add(this.pnlParams);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(255, 135);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1012, 524);
            this.panel3.TabIndex = 29;
            // 
            // pnlProjectionList
            // 
            this.pnlProjectionList.AutoScroll = true;
            this.pnlProjectionList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlProjectionList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlProjectionList.Location = new System.Drawing.Point(0, 0);
            this.pnlProjectionList.Name = "pnlProjectionList";
            this.pnlProjectionList.Size = new System.Drawing.Size(620, 524);
            this.pnlProjectionList.TabIndex = 29;
            // 
            // chkIsLargeFocus
            // 
            this.chkIsLargeFocus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkIsLargeFocus.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIsLargeFocus.ForeColor = System.Drawing.Color.Navy;
            this.chkIsLargeFocus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.chkIsLargeFocus.ImageIndex = 1;
            this.chkIsLargeFocus.ImageList = this.imageList1;
            this.chkIsLargeFocus.Location = new System.Drawing.Point(33, 480);
            this.chkIsLargeFocus.Name = "chkIsLargeFocus";
            this.chkIsLargeFocus.Size = new System.Drawing.Size(310, 29);
            this.chkIsLargeFocus.TabIndex = 36;
            this.chkIsLargeFocus.Text = "Large Focus?";
            this.chkIsLargeFocus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkIsLargeFocus.Click += new System.EventHandler(this.chkIsLargeFocus_Click);
            // 
            // frm_Anatomy_Projection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(1267, 752);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.pnlAnatomyList);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_Anatomy_Projection";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.pnlParams.ResumeLayout(false);
            this.pnlParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nmrMAS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrMA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmrKVP)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cboDevice;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel pnlAnatomyList;
        private System.Windows.Forms.Button cmdChooseProjection;
        private System.Windows.Forms.Button cmdChooseAnatomy;
        private System.Windows.Forms.Button cmdDelProjection;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel pnlParams;
        private System.Windows.Forms.NumericUpDown nmrKVP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboBodySize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nmrMAS;
        private System.Windows.Forms.NumericUpDown nmrMA;
        private System.Windows.Forms.Button cmdSaveParam;
        private System.Windows.Forms.Button cmdMakeAsEmerency;
        private System.Windows.Forms.Label lblMultiCheck;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox txtRadCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.FlowLayoutPanel pnlProjectionList;
        private System.Windows.Forms.Button cmdDeleteAnatomy;
        private System.Windows.Forms.Label chkAutoVFlip;
        private System.Windows.Forms.Label chkAutoHFlip;
        private System.Windows.Forms.Label CHK_FILMHD;
        private System.Windows.Forms.Label chkIsLargeFocus;
    }
}