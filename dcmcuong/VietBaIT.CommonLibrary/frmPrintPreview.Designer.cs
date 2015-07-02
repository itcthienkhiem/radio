namespace VietBaIT.CommonLibrary
{
    partial class frmPrintPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrintPreview));
            this.Label1 = new System.Windows.Forms.Label();
            this.ToolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.stbr = new System.Windows.Forms.StatusStrip();
            this.crptViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.cmdTrinhKy = new Janus.Windows.EditControls.UIButton();
            this.cmdExcel = new Janus.Windows.EditControls.UIButton();
            this.cmdTieuDe = new Janus.Windows.EditControls.UIButton();
            this.stbr.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.Navy;
            this.Label1.Location = new System.Drawing.Point(643, 6);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(201, 15);
            this.Label1.TabIndex = 8;
            this.Label1.Text = "Nhấn P hoặc Ctrl+P để in ra máy in";
            // 
            // ToolStripStatusLabel2
            // 
            this.ToolStripStatusLabel2.Name = "ToolStripStatusLabel2";
            this.ToolStripStatusLabel2.Size = new System.Drawing.Size(442, 17);
            this.ToolStripStatusLabel2.Spring = true;
            this.ToolStripStatusLabel2.Text = "Nhấn S hoặc Ctrl+S để lưu dữ liệu in ra file Excel";
            // 
            // ToolStripStatusLabel1
            // 
            this.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1";
            this.ToolStripStatusLabel1.Size = new System.Drawing.Size(442, 17);
            this.ToolStripStatusLabel1.Spring = true;
            this.ToolStripStatusLabel1.Text = "Nhấn P hoặc Ctrl+P để in ra máy in";
            // 
            // stbr
            // 
            this.stbr.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripStatusLabel1,
            this.ToolStripStatusLabel2});
            this.stbr.Location = new System.Drawing.Point(0, 458);
            this.stbr.Name = "stbr";
            this.stbr.Size = new System.Drawing.Size(900, 22);
            this.stbr.TabIndex = 7;
            this.stbr.Text = "StatusStrip1";
            // 
            // crptViewer
            // 
            this.crptViewer.ActiveViewIndex = -1;
            this.crptViewer.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.crptViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crptViewer.DisplayGroupTree = false;
            this.crptViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crptViewer.EnableDrillDown = false;
            this.crptViewer.Location = new System.Drawing.Point(0, 0);
            this.crptViewer.Name = "crptViewer";
            this.crptViewer.SelectionFormula = "";
            this.crptViewer.Size = new System.Drawing.Size(900, 480);
            this.crptViewer.TabIndex = 10;
            this.crptViewer.ViewTimeSelectionFormula = "";
            // 
            // cmdTrinhKy
            // 
            this.cmdTrinhKy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTrinhKy.Image = ((System.Drawing.Image)(resources.GetObject("cmdTrinhKy.Image")));
            this.cmdTrinhKy.Location = new System.Drawing.Point(431, 2);
            this.cmdTrinhKy.Name = "cmdTrinhKy";
            this.cmdTrinhKy.Size = new System.Drawing.Size(91, 23);
            this.cmdTrinhKy.TabIndex = 11;
            this.cmdTrinhKy.Text = "&Trình ký";
            this.cmdTrinhKy.Click += new System.EventHandler(this.cmdTrinhKy_Click_2);
            // 
            // cmdExcel
            // 
            this.cmdExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExcel.Image = ((System.Drawing.Image)(resources.GetObject("cmdExcel.Image")));
            this.cmdExcel.Location = new System.Drawing.Point(528, 2);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Size = new System.Drawing.Size(91, 23);
            this.cmdExcel.TabIndex = 12;
            this.cmdExcel.Text = "&Xuất Excel";
            // 
            // cmdTieuDe
            // 
            this.cmdTieuDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTieuDe.Image = ((System.Drawing.Image)(resources.GetObject("cmdTieuDe.Image")));
            this.cmdTieuDe.Location = new System.Drawing.Point(334, 2);
            this.cmdTieuDe.Name = "cmdTieuDe";
            this.cmdTieuDe.Size = new System.Drawing.Size(91, 23);
            this.cmdTieuDe.TabIndex = 13;
            this.cmdTieuDe.Text = "&Tiều đề";
            this.cmdTieuDe.Click += new System.EventHandler(this.cmdTieuDe_Click);
            // 
            // frmPrintPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 480);
            this.Controls.Add(this.cmdTieuDe);
            this.Controls.Add(this.cmdExcel);
            this.Controls.Add(this.cmdTrinhKy);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.stbr);
            this.Controls.Add(this.crptViewer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "frmPrintPreview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PrintPreview";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmPrintPreview_Load_1);
            this.stbr.ResumeLayout(false);
            this.stbr.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel2;
        internal System.Windows.Forms.ToolStripStatusLabel ToolStripStatusLabel1;
        internal System.Windows.Forms.StatusStrip stbr;
        public CrystalDecisions.Windows.Forms.CrystalReportViewer crptViewer;
        private Janus.Windows.EditControls.UIButton cmdTrinhKy;
        private Janus.Windows.EditControls.UIButton cmdExcel;
        private Janus.Windows.EditControls.UIButton cmdTieuDe;

    }
}