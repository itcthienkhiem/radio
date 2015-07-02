namespace VietBaIT.CommonLibrary
{
    partial class frm_LogError
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_LogError));
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem1 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem2 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.EditControls.UIComboBoxItem uiComboBoxItem3 = new Janus.Windows.EditControls.UIComboBoxItem();
            Janus.Windows.GridEX.GridEXLayout grdLogError_DesignTimeLayout = new Janus.Windows.GridEX.GridEXLayout();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.cmdXoa = new System.Windows.Forms.ToolStripButton();
            this.cmdThoat = new System.Windows.Forms.ToolStripButton();
            this.uiGroupBox1 = new Janus.Windows.EditControls.UIGroupBox();
            this.dtToDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.dtFromDate = new Janus.Windows.CalendarCombo.CalendarCombo();
            this.chkByDate = new Janus.Windows.EditControls.UICheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboKieu = new Janus.Windows.EditControls.UIComboBox();
            this.cmdSearch = new Janus.Windows.EditControls.UIButton();
            this.uiGroupBox2 = new Janus.Windows.EditControls.UIGroupBox();
            this.grdLogError = new Janus.Windows.GridEX.GridEX();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).BeginInit();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).BeginInit();
            this.uiGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLogError)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmdXoa,
            this.cmdThoat});
            this.toolStrip.Location = new System.Drawing.Point(0, 0);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(697, 31);
            this.toolStrip.TabIndex = 10;
            this.toolStrip.Text = "toolStrip1";
            // 
            // cmdXoa
            // 
            this.cmdXoa.Font = new System.Drawing.Font("Tahoma", 9F);
            this.cmdXoa.Image = ((System.Drawing.Image)(resources.GetObject("cmdXoa.Image")));
            this.cmdXoa.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdXoa.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdXoa.Name = "cmdXoa";
            this.cmdXoa.Size = new System.Drawing.Size(114, 28);
            this.cmdXoa.Text = "&Xóa thông tin ";
            this.cmdXoa.Click += new System.EventHandler(this.cmdXoa_Click);
            // 
            // cmdThoat
            // 
            this.cmdThoat.Image = ((System.Drawing.Image)(resources.GetObject("cmdThoat.Image")));
            this.cmdThoat.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.cmdThoat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdThoat.Name = "cmdThoat";
            this.cmdThoat.Size = new System.Drawing.Size(87, 28);
            this.cmdThoat.Text = "&Thoát(Esc)";
            this.cmdThoat.Click += new System.EventHandler(this.cmdThoat_Click);
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.dtToDate);
            this.uiGroupBox1.Controls.Add(this.dtFromDate);
            this.uiGroupBox1.Controls.Add(this.chkByDate);
            this.uiGroupBox1.Controls.Add(this.label1);
            this.uiGroupBox1.Controls.Add(this.cboKieu);
            this.uiGroupBox1.Controls.Add(this.cmdSearch);
            this.uiGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiGroupBox1.Location = new System.Drawing.Point(0, 31);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Size = new System.Drawing.Size(697, 71);
            this.uiGroupBox1.TabIndex = 11;
            this.uiGroupBox1.Text = "&Thông tin tìm kiếm";
            // 
            // dtToDate
            // 
            this.dtToDate.CustomFormat = "dd/MM/yyyy";
            this.dtToDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtToDate.DropDownCalendar.Name = "";
            this.dtToDate.DropDownCalendar.Visible = false;
            this.dtToDate.Location = new System.Drawing.Point(271, 38);
            this.dtToDate.Name = "dtToDate";
            this.dtToDate.ShowUpDown = true;
            this.dtToDate.Size = new System.Drawing.Size(138, 20);
            this.dtToDate.TabIndex = 43;
            this.dtToDate.Value = new System.DateTime(2012, 8, 24, 0, 0, 0, 0);
            // 
            // dtFromDate
            // 
            this.dtFromDate.CustomFormat = "dd/MM/yyyy";
            this.dtFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom;
            // 
            // 
            // 
            this.dtFromDate.DropDownCalendar.Name = "";
            this.dtFromDate.DropDownCalendar.Visible = false;
            this.dtFromDate.Location = new System.Drawing.Point(127, 38);
            this.dtFromDate.Name = "dtFromDate";
            this.dtFromDate.ShowUpDown = true;
            this.dtFromDate.Size = new System.Drawing.Size(138, 20);
            this.dtFromDate.TabIndex = 42;
            this.dtFromDate.Value = new System.DateTime(2012, 8, 24, 0, 0, 0, 0);
            // 
            // chkByDate
            // 
            this.chkByDate.Checked = true;
            this.chkByDate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkByDate.Image = ((System.Drawing.Image)(resources.GetObject("chkByDate.Image")));
            this.chkByDate.Location = new System.Drawing.Point(15, 40);
            this.chkByDate.Name = "chkByDate";
            this.chkByDate.Size = new System.Drawing.Size(88, 23);
            this.chkByDate.TabIndex = 41;
            this.chkByDate.Text = "&Từ ngày";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Kiểu";
            // 
            // cboKieu
            // 
            uiComboBoxItem1.FormatStyle.Alpha = 0;
            uiComboBoxItem1.IsSeparator = false;
            uiComboBoxItem1.Text = "Tất Cả";
            uiComboBoxItem1.Value = "-1";
            uiComboBoxItem2.FormatStyle.Alpha = 0;
            uiComboBoxItem2.IsSeparator = false;
            uiComboBoxItem2.Text = "ERROR";
            uiComboBoxItem2.Value = "ERROR";
            uiComboBoxItem3.FormatStyle.Alpha = 0;
            uiComboBoxItem3.IsSeparator = false;
            uiComboBoxItem3.Text = "INFO";
            uiComboBoxItem3.Value = "INFO";
            this.cboKieu.Items.AddRange(new Janus.Windows.EditControls.UIComboBoxItem[] {
            uiComboBoxItem1,
            uiComboBoxItem2,
            uiComboBoxItem3});
            this.cboKieu.Location = new System.Drawing.Point(127, 14);
            this.cboKieu.Name = "cboKieu";
            this.cboKieu.Size = new System.Drawing.Size(219, 20);
            this.cboKieu.TabIndex = 1;
            this.cboKieu.Text = "Kiểu";
            // 
            // cmdSearch
            // 
            this.cmdSearch.Location = new System.Drawing.Point(575, 19);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(110, 39);
            this.cmdSearch.TabIndex = 0;
            this.cmdSearch.Text = "&Tìm kiếm(F3)";
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.grdLogError);
            this.uiGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uiGroupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uiGroupBox2.Image = ((System.Drawing.Image)(resources.GetObject("uiGroupBox2.Image")));
            this.uiGroupBox2.Location = new System.Drawing.Point(0, 102);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Size = new System.Drawing.Size(697, 440);
            this.uiGroupBox2.TabIndex = 12;
            this.uiGroupBox2.Text = "&Thông tin loại dịch vụ";
            // 
            // grdLogError
            // 
            this.grdLogError.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False;
            this.grdLogError.AlternatingColors = true;
            this.grdLogError.BuiltInTextsData = "<LocalizableData ID=\"LocalizableStrings\" Collection=\"true\"><FilterRowInfoText>Lọc" +
                " thông tin log</FilterRowInfoText></LocalizableData>";
            this.grdLogError.ColumnAutoResize = true;
            this.grdLogError.DefaultFilterRowComparison = Janus.Windows.GridEX.FilterConditionOperator.Contains;
            grdLogError_DesignTimeLayout.LayoutString = resources.GetString("grdLogError_DesignTimeLayout.LayoutString");
            this.grdLogError.DesignTimeLayout = grdLogError_DesignTimeLayout;
            this.grdLogError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLogError.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic;
            this.grdLogError.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown;
            this.grdLogError.FilterRowFormatStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.grdLogError.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges;
            this.grdLogError.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdLogError.GroupByBoxVisible = false;
            this.grdLogError.GroupRowFormatStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.grdLogError.GroupRowFormatStyle.ForeColor = System.Drawing.Color.Red;
            this.grdLogError.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003;
            this.grdLogError.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight;
            this.grdLogError.Location = new System.Drawing.Point(3, 18);
            this.grdLogError.Name = "grdLogError";
            this.grdLogError.RecordNavigator = true;
            this.grdLogError.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.True;
            this.grdLogError.Size = new System.Drawing.Size(691, 419);
            this.grdLogError.TabIndex = 0;
            // 
            // frm_LogError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 542);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.uiGroupBox1);
            this.Controls.Add(this.toolStrip);
            this.Name = "frm_LogError";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thông tin log nhận được";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frm_LogError_Load);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox1)).EndInit();
            this.uiGroupBox1.ResumeLayout(false);
            this.uiGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uiGroupBox2)).EndInit();
            this.uiGroupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLogError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton cmdXoa;
        private System.Windows.Forms.ToolStripButton cmdThoat;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox1;
        private Janus.Windows.EditControls.UIGroupBox uiGroupBox2;
        private Janus.Windows.GridEX.GridEX grdLogError;
        private Janus.Windows.EditControls.UIButton cmdSearch;
        private System.Windows.Forms.Label label1;
        private Janus.Windows.EditControls.UIComboBox cboKieu;
        private Janus.Windows.CalendarCombo.CalendarCombo dtToDate;
        private Janus.Windows.CalendarCombo.CalendarCombo dtFromDate;
        private Janus.Windows.EditControls.UICheckBox chkByDate;
    }
}