namespace VietBaIT.DROC
{
    partial class frm_UDM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_UDM));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.lblCapacity = new System.Windows.Forms.Label();
            this.cmdOpen = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmdDir = new System.Windows.Forms.Button();
            this.txtDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdLoad = new System.Windows.Forms.Button();
            this.txtURL = new System.Windows.Forms.TextBox();
            this._lblUrl = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblPercent = new System.Windows.Forms.Label();
            this.lblUploadCapacity = new System.Windows.Forms.Label();
            this.cmdCancelUpload = new System.Windows.Forms.Button();
            this.lblUploadResult = new System.Windows.Forms.Label();
            this.picUpLoad = new System.Windows.Forms.PictureBox();
            this.lblUploadStatus = new System.Windows.Forms.Label();
            this.cmdFile = new System.Windows.Forms.Button();
            this.txtFileUpload = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmdUpload = new System.Windows.Forms.Button();
            this.txtUploadURL = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.grbProxy = new System.Windows.Forms.GroupBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chkAuthentication = new System.Windows.Forms.CheckBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProxyServer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.optUseConfig = new System.Windows.Forms.RadioButton();
            this.optAutoDetect = new System.Windows.Forms.RadioButton();
            this.cmdclose = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUpLoad)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.grbProxy.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(6, 83);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(529, 272);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.cmdCancel);
            this.tabPage1.Controls.Add(this.lblCapacity);
            this.tabPage1.Controls.Add(this.cmdOpen);
            this.tabPage1.Controls.Add(this.lblResult);
            this.tabPage1.Controls.Add(this.picLoading);
            this.tabPage1.Controls.Add(this.lblStatus);
            this.tabPage1.Controls.Add(this.cmdDir);
            this.tabPage1.Controls.Add(this.txtDir);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.cmdLoad);
            this.tabPage1.Controls.Add(this.txtURL);
            this.tabPage1.Controls.Add(this._lblUrl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(521, 246);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Download";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(6, 192);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(495, 40);
            this.label2.TabIndex = 18;
            this.label2.Text = "Chú ý: Nếu sau 10 giây kể từ lúc nhấn nút Download mà dung lượng file (KB) vẫn là" +
                " 0 (KB) thì bạn hãy hủy bỏ và Download lại...";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdCancel
            // 
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdCancel.Location = new System.Drawing.Point(333, 152);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(90, 23);
            this.cmdCancel.TabIndex = 4;
            this.cmdCancel.Text = "Hủy bỏ";
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // lblCapacity
            // 
            this.lblCapacity.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCapacity.Location = new System.Drawing.Point(238, 152);
            this.lblCapacity.Name = "lblCapacity";
            this.lblCapacity.Size = new System.Drawing.Size(89, 23);
            this.lblCapacity.TabIndex = 16;
            this.lblCapacity.Text = "100 (KB)";
            this.lblCapacity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdOpen
            // 
            this.cmdOpen.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdOpen.Location = new System.Drawing.Point(289, 155);
            this.cmdOpen.Name = "cmdOpen";
            this.cmdOpen.Size = new System.Drawing.Size(135, 23);
            this.cmdOpen.TabIndex = 4;
            this.cmdOpen.Text = "Mở ảnh vừa Download";
            this.cmdOpen.Visible = false;
            this.cmdOpen.Click += new System.EventHandler(this.cmdOpen_Click);
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.ForeColor = System.Drawing.Color.Navy;
            this.lblResult.Location = new System.Drawing.Point(18, 158);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(243, 16);
            this.lblResult.TabIndex = 14;
            this.lblResult.Text = "Đã Download thành công về thư mục";
            this.lblResult.Visible = false;
            // 
            // picLoading
            // 
            this.picLoading.Image = ((System.Drawing.Image)(resources.GetObject("picLoading.Image")));
            this.picLoading.Location = new System.Drawing.Point(203, 150);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(29, 28);
            this.picLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLoading.TabIndex = 13;
            this.picLoading.TabStop = false;
            this.picLoading.Visible = false;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.Navy;
            this.lblStatus.Location = new System.Drawing.Point(90, 155);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(103, 15);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "Đang download...";
            this.lblStatus.Visible = false;
            // 
            // cmdDir
            // 
            this.cmdDir.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdDir.Location = new System.Drawing.Point(430, 37);
            this.cmdDir.Name = "cmdDir";
            this.cmdDir.Size = new System.Drawing.Size(83, 23);
            this.cmdDir.TabIndex = 1;
            this.cmdDir.Text = "Chọn thư mục";
            this.cmdDir.Click += new System.EventHandler(this.cmdDir_Click);
            // 
            // txtDir
            // 
            this.txtDir.Location = new System.Drawing.Point(8, 39);
            this.txtDir.Name = "txtDir";
            this.txtDir.Size = new System.Drawing.Size(416, 20);
            this.txtDir.TabIndex = 0;
            this.txtDir.Text = "C:\\DownloadIMG\\";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 23);
            this.label1.TabIndex = 10;
            this.label1.Text = "Thư mục lưu file:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdLoad
            // 
            this.cmdLoad.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdLoad.Location = new System.Drawing.Point(429, 93);
            this.cmdLoad.Name = "cmdLoad";
            this.cmdLoad.Size = new System.Drawing.Size(83, 23);
            this.cmdLoad.TabIndex = 3;
            this.cmdLoad.Text = "Download";
            this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(7, 95);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(416, 20);
            this.txtURL.TabIndex = 2;
            this.txtURL.Text = "Http://";
            // 
            // _lblUrl
            // 
            this._lblUrl.Location = new System.Drawing.Point(5, 69);
            this._lblUrl.Name = "_lblUrl";
            this._lblUrl.Size = new System.Drawing.Size(181, 23);
            this._lblUrl.TabIndex = 7;
            this._lblUrl.Text = "Đường dẫn tới file(file URL):";
            this._lblUrl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblPercent);
            this.tabPage2.Controls.Add(this.lblUploadCapacity);
            this.tabPage2.Controls.Add(this.cmdCancelUpload);
            this.tabPage2.Controls.Add(this.lblUploadResult);
            this.tabPage2.Controls.Add(this.picUpLoad);
            this.tabPage2.Controls.Add(this.lblUploadStatus);
            this.tabPage2.Controls.Add(this.cmdFile);
            this.tabPage2.Controls.Add(this.txtFileUpload);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.cmdUpload);
            this.tabPage2.Controls.Add(this.txtUploadURL);
            this.tabPage2.Controls.Add(this.label12);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(521, 246);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Upload";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblPercent
            // 
            this.lblPercent.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPercent.Location = new System.Drawing.Point(179, 126);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(89, 23);
            this.lblPercent.TabIndex = 31;
            this.lblPercent.Text = "%";
            this.lblPercent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPercent.Visible = false;
            // 
            // lblUploadCapacity
            // 
            this.lblUploadCapacity.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUploadCapacity.Location = new System.Drawing.Point(239, 153);
            this.lblUploadCapacity.Name = "lblUploadCapacity";
            this.lblUploadCapacity.Size = new System.Drawing.Size(89, 23);
            this.lblUploadCapacity.TabIndex = 30;
            this.lblUploadCapacity.Text = "0 (KB)";
            this.lblUploadCapacity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblUploadCapacity.Visible = false;
            this.lblUploadCapacity.Click += new System.EventHandler(this.lblUploadCapacity_Click);
            // 
            // cmdCancelUpload
            // 
            this.cmdCancelUpload.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdCancelUpload.Location = new System.Drawing.Point(334, 153);
            this.cmdCancelUpload.Name = "cmdCancelUpload";
            this.cmdCancelUpload.Size = new System.Drawing.Size(90, 23);
            this.cmdCancelUpload.TabIndex = 23;
            this.cmdCancelUpload.Text = "Hủy bỏ";
            this.cmdCancelUpload.Visible = false;
            this.cmdCancelUpload.Click += new System.EventHandler(this.cmdCancelUpload_Click);
            // 
            // lblUploadResult
            // 
            this.lblUploadResult.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUploadResult.ForeColor = System.Drawing.Color.Navy;
            this.lblUploadResult.Location = new System.Drawing.Point(6, 156);
            this.lblUploadResult.Name = "lblUploadResult";
            this.lblUploadResult.Size = new System.Drawing.Size(489, 76);
            this.lblUploadResult.TabIndex = 29;
            this.lblUploadResult.Text = "Đã Upload thành công đến địa chỉ trên!";
            this.lblUploadResult.Visible = false;
            // 
            // picUpLoad
            // 
            this.picUpLoad.Image = ((System.Drawing.Image)(resources.GetObject("picUpLoad.Image")));
            this.picUpLoad.Location = new System.Drawing.Point(204, 152);
            this.picUpLoad.Name = "picUpLoad";
            this.picUpLoad.Size = new System.Drawing.Size(29, 28);
            this.picUpLoad.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picUpLoad.TabIndex = 28;
            this.picUpLoad.TabStop = false;
            this.picUpLoad.Visible = false;
            // 
            // lblUploadStatus
            // 
            this.lblUploadStatus.AutoSize = true;
            this.lblUploadStatus.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUploadStatus.ForeColor = System.Drawing.Color.Navy;
            this.lblUploadStatus.Location = new System.Drawing.Point(91, 157);
            this.lblUploadStatus.Name = "lblUploadStatus";
            this.lblUploadStatus.Size = new System.Drawing.Size(113, 15);
            this.lblUploadStatus.TabIndex = 27;
            this.lblUploadStatus.Text = "Đang Upload ảnh...";
            this.lblUploadStatus.Visible = false;
            // 
            // cmdFile
            // 
            this.cmdFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdFile.Location = new System.Drawing.Point(431, 34);
            this.cmdFile.Name = "cmdFile";
            this.cmdFile.Size = new System.Drawing.Size(83, 23);
            this.cmdFile.TabIndex = 20;
            this.cmdFile.Text = "Chọn file";
            this.cmdFile.Click += new System.EventHandler(this.cmdFile_Click);
            // 
            // txtFileUpload
            // 
            this.txtFileUpload.Location = new System.Drawing.Point(9, 36);
            this.txtFileUpload.Name = "txtFileUpload";
            this.txtFileUpload.Size = new System.Drawing.Size(416, 20);
            this.txtFileUpload.TabIndex = 19;
            this.txtFileUpload.Text = "Yourfile";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(7, 10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(116, 23);
            this.label11.TabIndex = 26;
            this.label11.Text = "Chọn file:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmdUpload
            // 
            this.cmdUpload.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdUpload.Location = new System.Drawing.Point(430, 90);
            this.cmdUpload.Name = "cmdUpload";
            this.cmdUpload.Size = new System.Drawing.Size(83, 23);
            this.cmdUpload.TabIndex = 22;
            this.cmdUpload.Text = "Upload";
            this.cmdUpload.Click += new System.EventHandler(this.cmdUpload_Click);
            // 
            // txtUploadURL
            // 
            this.txtUploadURL.Location = new System.Drawing.Point(8, 92);
            this.txtUploadURL.Name = "txtUploadURL";
            this.txtUploadURL.Size = new System.Drawing.Size(416, 20);
            this.txtUploadURL.TabIndex = 21;
            this.txtUploadURL.Text = "Http://10.1.6.58:1088/WBN/UploadFile.aspx";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(6, 66);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(181, 23);
            this.label12.TabIndex = 25;
            this.label12.Text = "Chọn địa chỉ Web";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.grbProxy);
            this.tabPage3.Controls.Add(this.optUseConfig);
            this.tabPage3.Controls.Add(this.optAutoDetect);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(521, 246);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Thiết lập kết nối(Connection Settings)";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // grbProxy
            // 
            this.grbProxy.Controls.Add(this.txtPassword);
            this.grbProxy.Controls.Add(this.label6);
            this.grbProxy.Controls.Add(this.txtUsername);
            this.grbProxy.Controls.Add(this.label5);
            this.grbProxy.Controls.Add(this.chkAuthentication);
            this.grbProxy.Controls.Add(this.txtPort);
            this.grbProxy.Controls.Add(this.label4);
            this.grbProxy.Controls.Add(this.txtProxyServer);
            this.grbProxy.Controls.Add(this.label3);
            this.grbProxy.Enabled = false;
            this.grbProxy.Location = new System.Drawing.Point(42, 91);
            this.grbProxy.Name = "grbProxy";
            this.grbProxy.Size = new System.Drawing.Size(442, 140);
            this.grbProxy.TabIndex = 2;
            this.grbProxy.TabStop = false;
            this.grbProxy.Text = "Thông tin Proxy";
            // 
            // txtPassword
            // 
            this.txtPassword.Enabled = false;
            this.txtPassword.Location = new System.Drawing.Point(100, 113);
            this.txtPassword.MaxLength = 100;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(170, 20);
            this.txtPassword.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(13, 111);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 23);
            this.label6.TabIndex = 19;
            this.label6.Text = "Password";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUsername
            // 
            this.txtUsername.Enabled = false;
            this.txtUsername.Location = new System.Drawing.Point(100, 87);
            this.txtUsername.MaxLength = 100;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(170, 20);
            this.txtUsername.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(13, 85);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 23);
            this.label5.TabIndex = 17;
            this.label5.Text = "Username";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkAuthentication
            // 
            this.chkAuthentication.AutoSize = true;
            this.chkAuthentication.Location = new System.Drawing.Point(16, 64);
            this.chkAuthentication.Name = "chkAuthentication";
            this.chkAuthentication.Size = new System.Drawing.Size(210, 17);
            this.chkAuthentication.TabIndex = 2;
            this.chkAuthentication.Text = "Quyền truy cập Proxy (Authentication)?";
            this.chkAuthentication.UseVisualStyleBackColor = true;
            this.chkAuthentication.CheckedChanged += new System.EventHandler(this.chkAuthentication_CheckedChanged);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(309, 29);
            this.txtPort.MaxLength = 4;
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(98, 20);
            this.txtPort.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(276, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 23);
            this.label4.TabIndex = 14;
            this.label4.Text = "Port";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtProxyServer
            // 
            this.txtProxyServer.Location = new System.Drawing.Point(100, 29);
            this.txtProxyServer.MaxLength = 50;
            this.txtProxyServer.Name = "txtProxyServer";
            this.txtProxyServer.Size = new System.Drawing.Size(170, 20);
            this.txtProxyServer.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(13, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 23);
            this.label3.TabIndex = 12;
            this.label3.Text = "Proxy Server";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // optUseConfig
            // 
            this.optUseConfig.AutoSize = true;
            this.optUseConfig.Location = new System.Drawing.Point(22, 62);
            this.optUseConfig.Name = "optUseConfig";
            this.optUseConfig.Size = new System.Drawing.Size(209, 17);
            this.optUseConfig.TabIndex = 1;
            this.optUseConfig.Text = "Sử dụng Proxy theo cấu hình dưới đây:";
            this.optUseConfig.UseVisualStyleBackColor = true;
            this.optUseConfig.CheckedChanged += new System.EventHandler(this.optUseConfig_CheckedChanged);
            // 
            // optAutoDetect
            // 
            this.optAutoDetect.AutoSize = true;
            this.optAutoDetect.Checked = true;
            this.optAutoDetect.Location = new System.Drawing.Point(22, 29);
            this.optAutoDetect.Name = "optAutoDetect";
            this.optAutoDetect.Size = new System.Drawing.Size(126, 17);
            this.optAutoDetect.TabIndex = 0;
            this.optAutoDetect.TabStop = true;
            this.optAutoDetect.Text = "Tự động dò tìm Proxy";
            this.optAutoDetect.UseVisualStyleBackColor = true;
            this.optAutoDetect.CheckedChanged += new System.EventHandler(this.optAutoDetect_CheckedChanged);
            // 
            // cmdclose
            // 
            this.cmdclose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdclose.Location = new System.Drawing.Point(440, 374);
            this.cmdclose.Name = "cmdclose";
            this.cmdclose.Size = new System.Drawing.Size(83, 23);
            this.cmdclose.TabIndex = 18;
            this.cmdclose.Text = "Thoát";
            this.cmdclose.Click += new System.EventHandler(this.cmdclose_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.AliceBlue;
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(542, 70);
            this.panel1.TabIndex = 19;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.AliceBlue;
            this.label7.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(78, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(453, 71);
            this.label7.TabIndex = 11;
            this.label7.Text = "TIỆN ÍCH UPLOAD VÀ DOWNLOAD FILE";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.AliceBlue;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 70);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(-9, 65);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(618, 2);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(-42, 364);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(618, 2);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            // 
            // frm_UDM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 431);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmdclose);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_UDM";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Upload & Download file";
            this.Load += new System.EventHandler(this.frm_UDM_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_UDM_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picUpLoad)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.grbProxy.ResumeLayout(false);
            this.grbProxy.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        void frm_UDM_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            try
            {
                t.Abort();
                t = null;
                if (!AbortThread)
                {
                    tUpLoad.Abort();
                    tUpLoad = null;
                    bUploading = true;
                }
                else
                {
                    bUploading = false;
                }
            }
            catch
            {
            }
            bDownloading = false;
            
        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button cmdDir;
        private System.Windows.Forms.TextBox txtDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdLoad;
        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label _lblUrl;
        private System.Windows.Forms.PictureBox picLoading;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button cmdOpen;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label lblCapacity;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdclose;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox grbProxy;
        private System.Windows.Forms.TextBox txtProxyServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton optUseConfig;
        private System.Windows.Forms.RadioButton optAutoDetect;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chkAuthentication;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdCancelUpload;
        private System.Windows.Forms.Label lblUploadResult;
        private System.Windows.Forms.PictureBox picUpLoad;
        private System.Windows.Forms.Label lblUploadStatus;
        private System.Windows.Forms.Button cmdFile;
        private System.Windows.Forms.TextBox txtFileUpload;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button cmdUpload;
        private System.Windows.Forms.TextBox txtUploadURL;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblUploadCapacity;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}