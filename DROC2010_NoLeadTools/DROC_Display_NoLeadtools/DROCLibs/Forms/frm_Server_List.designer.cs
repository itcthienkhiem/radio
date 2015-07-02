namespace VietBaIT.DROC
{
    partial class frm_ServerList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_ServerList));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.cmdInsert = new System.Windows.Forms.ToolStripButton();
            this.cmdUpdate = new System.Windows.Forms.ToolStripButton();
            this.cmdDelete = new System.Windows.Forms.ToolStripButton();
            this.cmdSearchOnGrid = new System.Windows.Forms.ToolStripButton();
            this.cmdPrint = new System.Windows.Forms.ToolStripButton();
            this.cmdClose = new System.Windows.Forms.ToolStripButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.txtSearchCalledAETitle = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSearchIpAddress = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMsg = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.colIsActive = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colServerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFilmSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colsServerType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIpAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCalledAETitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocalAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLocalPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCallingAETitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTimeOut = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServerType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.cboFSize = new System.Windows.Forms.ComboBox();
            this.label39 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cboServerType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtTimeOut = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCallingAETitle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.txtport = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txtCalledAETitle = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.IpAddress = new System.Windows.Forms.Label();
            this.cmdSave = new System.Windows.Forms.Button();
            this.txtLocalPort = new System.Windows.Forms.TextBox();
            this.txtLocalAddress = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdTest = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Azure;
            this.toolStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdInsert,
            this.cmdUpdate,
            this.cmdDelete,
            this.cmdTest,
            this.cmdSearchOnGrid,
            this.cmdPrint,
            this.cmdClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1073, 55);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // cmdInsert
            // 
            this.cmdInsert.Image = ((System.Drawing.Image)(resources.GetObject("cmdInsert.Image")));
            this.cmdInsert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdInsert.Name = "cmdInsert";
            this.cmdInsert.Size = new System.Drawing.Size(158, 52);
            this.cmdInsert.Text = "Thêm mới";
            this.cmdInsert.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdate.Image")));
            this.cmdUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(151, 52);
            this.cmdUpdate.Text = "Cập nhật";
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.Image = ((System.Drawing.Image)(resources.GetObject("cmdDelete.Image")));
            this.cmdDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(102, 52);
            this.cmdDelete.Text = "Xóa";
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // cmdSearchOnGrid
            // 
            this.cmdSearchOnGrid.Image = ((System.Drawing.Image)(resources.GetObject("cmdSearchOnGrid.Image")));
            this.cmdSearchOnGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdSearchOnGrid.Name = "cmdSearchOnGrid";
            this.cmdSearchOnGrid.Size = new System.Drawing.Size(233, 52);
            this.cmdSearchOnGrid.Text = "Tìm kiếm trên lưới";
            // 
            // cmdPrint
            // 
            this.cmdPrint.Image = ((System.Drawing.Image)(resources.GetObject("cmdPrint.Image")));
            this.cmdPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(187, 52);
            this.cmdPrint.Text = "In danh sách";
            this.cmdPrint.Click += new System.EventHandler(this.cmdPrint_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Image = ((System.Drawing.Image)(resources.GetObject("cmdClose.Image")));
            this.cmdClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(119, 52);
            this.cmdClose.Text = "Thoát";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 55);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1073, 110);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.cmdSearch);
            this.tabPage1.Controls.Add(this.txtSearchCalledAETitle);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.txtSearchIpAddress);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1065, 72);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Tìm kiếm nhanh";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // cmdSearch
            // 
            this.cmdSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSearch.Image = ((System.Drawing.Image)(resources.GetObject("cmdSearch.Image")));
            this.cmdSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSearch.Location = new System.Drawing.Point(848, 5);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(211, 61);
            this.cmdSearch.TabIndex = 2;
            this.cmdSearch.Text = "Tìm kiếm";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // txtSearchCalledAETitle
            // 
            this.txtSearchCalledAETitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchCalledAETitle.Location = new System.Drawing.Point(501, 16);
            this.txtSearchCalledAETitle.Name = "txtSearchCalledAETitle";
            this.txtSearchCalledAETitle.Size = new System.Drawing.Size(341, 31);
            this.txtSearchCalledAETitle.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(347, 19);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(148, 25);
            this.label11.TabIndex = 6;
            this.label11.Text = "Called AETitle";
            // 
            // txtSearchIpAddress
            // 
            this.txtSearchIpAddress.Location = new System.Drawing.Point(92, 13);
            this.txtSearchIpAddress.Name = "txtSearchIpAddress";
            this.txtSearchIpAddress.Size = new System.Drawing.Size(238, 31);
            this.txtSearchIpAddress.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 25);
            this.label10.TabIndex = 4;
            this.label10.Text = "Địa chỉ";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblMsg});
            this.statusStrip1.Location = new System.Drawing.Point(0, 560);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1073, 30);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(230, 25);
            this.toolStripStatusLabel1.Text = "Trạng thái thực hiện:";
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMsg.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(188, 25);
            this.lblMsg.Text = "Mời bạn thao tác";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdList);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 165);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1073, 395);
            this.panel1.TabIndex = 5;
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colIsActive,
            this.colServerName,
            this.colFilmSize,
            this.colsServerType,
            this.colIpAddress,
            this.colCalledAETitle,
            this.colLocalAddress,
            this.colLocalPort,
            this.colCallingAETitle,
            this.colPort,
            this.colTimeOut,
            this.colID,
            this.colServerType});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grdList.DefaultCellStyle = dataGridViewCellStyle7;
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RowHeadersWidth = 5;
            this.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdList.Size = new System.Drawing.Size(1073, 138);
            this.grdList.TabIndex = 8;
            // 
            // colIsActive
            // 
            this.colIsActive.DataPropertyName = "IsActive";
            this.colIsActive.FalseValue = "0";
            this.colIsActive.HeaderText = "Hoạt động";
            this.colIsActive.Name = "colIsActive";
            this.colIsActive.TrueValue = "1";
            this.colIsActive.Width = 150;
            // 
            // colServerName
            // 
            this.colServerName.DataPropertyName = "ServerName";
            this.colServerName.HeaderText = "Tên máy in";
            this.colServerName.Name = "colServerName";
            this.colServerName.Width = 200;
            // 
            // colFilmSize
            // 
            this.colFilmSize.DataPropertyName = "FilmSize";
            this.colFilmSize.HeaderText = "Kích thước Film";
            this.colFilmSize.Name = "colFilmSize";
            this.colFilmSize.Width = 200;
            // 
            // colsServerType
            // 
            this.colsServerType.DataPropertyName = "sServerType";
            this.colsServerType.HeaderText = "Loại máy";
            this.colsServerType.Name = "colsServerType";
            this.colsServerType.Width = 140;
            // 
            // colIpAddress
            // 
            this.colIpAddress.DataPropertyName = "IpAddress";
            this.colIpAddress.HeaderText = "Địa chỉ máy chủ";
            this.colIpAddress.Name = "colIpAddress";
            this.colIpAddress.ReadOnly = true;
            this.colIpAddress.Width = 250;
            // 
            // colCalledAETitle
            // 
            this.colCalledAETitle.DataPropertyName = "CalledAETitle";
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.colCalledAETitle.DefaultCellStyle = dataGridViewCellStyle6;
            this.colCalledAETitle.HeaderText = "Called AETitle";
            this.colCalledAETitle.Name = "colCalledAETitle";
            this.colCalledAETitle.ReadOnly = true;
            this.colCalledAETitle.Width = 250;
            // 
            // colLocalAddress
            // 
            this.colLocalAddress.DataPropertyName = "localAddress";
            this.colLocalAddress.HeaderText = "Địa chỉ máy local";
            this.colLocalAddress.Name = "colLocalAddress";
            this.colLocalAddress.Width = 200;
            // 
            // colLocalPort
            // 
            this.colLocalPort.DataPropertyName = "LocalPort";
            this.colLocalPort.HeaderText = "Local Port";
            this.colLocalPort.Name = "colLocalPort";
            this.colLocalPort.Width = 150;
            // 
            // colCallingAETitle
            // 
            this.colCallingAETitle.DataPropertyName = "CallingAETitle";
            this.colCallingAETitle.HeaderText = "Calling AETitle";
            this.colCallingAETitle.Name = "colCallingAETitle";
            this.colCallingAETitle.ReadOnly = true;
            this.colCallingAETitle.Width = 250;
            // 
            // colPort
            // 
            this.colPort.DataPropertyName = "Port";
            this.colPort.HeaderText = "Cổng";
            this.colPort.Name = "colPort";
            // 
            // colTimeOut
            // 
            this.colTimeOut.DataPropertyName = "TimeOut";
            this.colTimeOut.HeaderText = "TimeOut";
            this.colTimeOut.Name = "colTimeOut";
            this.colTimeOut.ReadOnly = true;
            this.colTimeOut.Width = 130;
            // 
            // colID
            // 
            this.colID.DataPropertyName = "ID";
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            // 
            // colServerType
            // 
            this.colServerType.DataPropertyName = "ServerType";
            this.colServerType.HeaderText = "Column1";
            this.colServerType.Name = "colServerType";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label40);
            this.groupBox1.Controls.Add(this.cboFSize);
            this.groupBox1.Controls.Add(this.label39);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.cboServerType);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtTimeOut);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCallingAETitle);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.chkIsActive);
            this.groupBox1.Controls.Add(this.txtport);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmdCancel);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtCalledAETitle);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtIpAddress);
            this.groupBox1.Controls.Add(this.IpAddress);
            this.groupBox1.Controls.Add(this.cmdSave);
            this.groupBox1.Controls.Add(this.txtLocalPort);
            this.groupBox1.Controls.Add(this.txtLocalAddress);
            this.groupBox1.Controls.Add(this.txtID);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 138);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1073, 257);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin máy chủ PACS Server(hoặc DicomPrinter)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(540, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 25);
            this.label6.TabIndex = 47;
            this.label6.Text = "Local Address:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.label40.Location = new System.Drawing.Point(575, 27);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(124, 25);
            this.label40.TabIndex = 46;
            this.label40.Text = "Tên máy in:";
            // 
            // cboFSize
            // 
            this.cboFSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFSize.FormattingEnabled = true;
            this.cboFSize.Items.AddRange(new object[] {
            "8INX10IN",
            "10INX12IN",
            "11INX14IN",
            "14INX17IN"});
            this.cboFSize.Location = new System.Drawing.Point(166, 174);
            this.cboFSize.Name = "cboFSize";
            this.cboFSize.Size = new System.Drawing.Size(368, 33);
            this.cboFSize.TabIndex = 12;
            // 
            // label39
            // 
            this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F);
            this.label39.Location = new System.Drawing.Point(0, 174);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(159, 33);
            this.label39.TabIndex = 45;
            this.label39.Text = "Kích thước Film";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.BackColor = System.Drawing.Color.White;
            this.txtName.Location = new System.Drawing.Point(713, 24);
            this.txtName.MaxLength = 100;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(323, 31);
            this.txtName.TabIndex = 5;
            // 
            // cboServerType
            // 
            this.cboServerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServerType.FormattingEnabled = true;
            this.cboServerType.Items.AddRange(new object[] {
            "Máy chủ lưu trữ hình ảnh (PACS Server)",
            "Máy chủ in ảnh Dicom (Dicom Printer)"});
            this.cboServerType.Location = new System.Drawing.Point(166, 27);
            this.cboServerType.Name = "cboServerType";
            this.cboServerType.Size = new System.Drawing.Size(368, 33);
            this.cboServerType.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(54, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 25);
            this.label5.TabIndex = 34;
            this.label5.Text = "Loại máy:";
            // 
            // txtTimeOut
            // 
            this.txtTimeOut.BackColor = System.Drawing.Color.White;
            this.txtTimeOut.Location = new System.Drawing.Point(166, 140);
            this.txtTimeOut.Name = "txtTimeOut";
            this.txtTimeOut.Size = new System.Drawing.Size(368, 31);
            this.txtTimeOut.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(61, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 25);
            this.label3.TabIndex = 32;
            this.label3.Text = "TimeOut:";
            // 
            // txtCallingAETitle
            // 
            this.txtCallingAETitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCallingAETitle.Location = new System.Drawing.Point(713, 100);
            this.txtCallingAETitle.Name = "txtCallingAETitle";
            this.txtCallingAETitle.Size = new System.Drawing.Size(323, 31);
            this.txtCallingAETitle.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(540, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 25);
            this.label2.TabIndex = 29;
            this.label2.Text = "Remote AETitle:";
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Location = new System.Drawing.Point(166, 213);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(196, 29);
            this.chkIsActive.TabIndex = 11;
            this.chkIsActive.Text = "Đang hoạt động?";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // txtport
            // 
            this.txtport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtport.BackColor = System.Drawing.Color.White;
            this.txtport.Location = new System.Drawing.Point(713, 63);
            this.txtport.Name = "txtport";
            this.txtport.Size = new System.Drawing.Size(323, 31);
            this.txtport.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(630, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 25);
            this.label1.TabIndex = 27;
            this.label1.Text = "Cổng:";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancel.Image")));
            this.cmdCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdCancel.Location = new System.Drawing.Point(830, 174);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(206, 73);
            this.cmdCancel.TabIndex = 14;
            this.cmdCancel.Text = "Hủy thao tác";
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(1039, 100);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 25);
            this.label12.TabIndex = 17;
            this.label12.Text = "(*)";
            // 
            // txtCalledAETitle
            // 
            this.txtCalledAETitle.Location = new System.Drawing.Point(166, 103);
            this.txtCalledAETitle.Name = "txtCalledAETitle";
            this.txtCalledAETitle.Size = new System.Drawing.Size(368, 31);
            this.txtCalledAETitle.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 25);
            this.label4.TabIndex = 7;
            this.label4.Text = "Local AETitle:";
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Location = new System.Drawing.Point(166, 66);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(368, 31);
            this.txtIpAddress.TabIndex = 6;
            // 
            // IpAddress
            // 
            this.IpAddress.AutoSize = true;
            this.IpAddress.Location = new System.Drawing.Point(6, 66);
            this.IpAddress.Name = "IpAddress";
            this.IpAddress.Size = new System.Drawing.Size(153, 25);
            this.IpAddress.TabIndex = 5;
            this.IpAddress.Text = "Địa chỉ Server:";
            // 
            // cmdSave
            // 
            this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdSave.Image = ((System.Drawing.Image)(resources.GetObject("cmdSave.Image")));
            this.cmdSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdSave.Location = new System.Drawing.Point(618, 174);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(206, 73);
            this.cmdSave.TabIndex = 13;
            this.cmdSave.Text = "Lưu thông tin";
            this.cmdSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cmdSave.UseVisualStyleBackColor = true;
            // 
            // txtLocalPort
            // 
            this.txtLocalPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocalPort.BackColor = System.Drawing.Color.White;
            this.txtLocalPort.Location = new System.Drawing.Point(969, 136);
            this.txtLocalPort.Name = "txtLocalPort";
            this.txtLocalPort.Size = new System.Drawing.Size(67, 31);
            this.txtLocalPort.TabIndex = 49;
            this.txtLocalPort.Text = "0";
            // 
            // txtLocalAddress
            // 
            this.txtLocalAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocalAddress.Location = new System.Drawing.Point(713, 137);
            this.txtLocalAddress.Name = "txtLocalAddress";
            this.txtLocalAddress.Size = new System.Drawing.Size(250, 31);
            this.txtLocalAddress.TabIndex = 48;
            // 
            // txtID
            // 
            this.txtID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtID.BackColor = System.Drawing.Color.White;
            this.txtID.Location = new System.Drawing.Point(713, 137);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(323, 31);
            this.txtID.TabIndex = 11;
            this.txtID.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Pos";
            this.dataGridViewTextBoxColumn1.HeaderText = "STT";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 40;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Doctor_Code";
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewTextBoxColumn2.HeaderText = "Mã Bác sĩ";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 150;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Doctor_Name";
            this.dataGridViewTextBoxColumn3.HeaderText = "Tên Bác sĩ";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 250;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Desc";
            this.dataGridViewTextBoxColumn4.HeaderText = "Mô tả thêm";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 250;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "IsEmerency";
            this.dataGridViewTextBoxColumn5.HeaderText = "IsEmerency";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Visible = false;
            this.dataGridViewTextBoxColumn5.Width = 80;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "ID";
            this.dataGridViewTextBoxColumn6.HeaderText = "ID";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // cmdTest
            // 
            this.cmdTest.Image = global::DROCLibs.Properties.Resources.OK;
            this.cmdTest.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdTest.Name = "cmdTest";
            this.cmdTest.Size = new System.Drawing.Size(118, 52);
            this.cmdTest.Text = "TEST";
           
            // 
            // frm_ServerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 590);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frm_ServerList";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh sách PACS Server ( hoặc Dicom Printer Server) ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_ServerList_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton cmdInsert;
        private System.Windows.Forms.ToolStripButton cmdUpdate;
        private System.Windows.Forms.ToolStripButton cmdDelete;
        private System.Windows.Forms.ToolStripButton cmdPrint;
        private System.Windows.Forms.ToolStripButton cmdClose;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtSearchCalledAETitle;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSearchIpAddress;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.ToolStripButton cmdSearchOnGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cboServerType;
        private System.Windows.Forms.Label label5;
        internal System.Windows.Forms.TextBox txtID;
        internal System.Windows.Forms.TextBox txtTimeOut;
        internal System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCallingAETitle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkIsActive;
        internal System.Windows.Forms.TextBox txtport;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtCalledAETitle;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Label IpAddress;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.ToolStripStatusLabel lblMsg;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.ComboBox cboFSize;
        private System.Windows.Forms.Label label39;
        internal System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtLocalAddress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsActive;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFilmSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn colsServerType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colIpAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCalledAETitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocalAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLocalPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCallingAETitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPort;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTimeOut;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServerType;
        internal System.Windows.Forms.TextBox txtLocalPort;
        private System.Windows.Forms.ToolStripButton cmdTest;
    }
}