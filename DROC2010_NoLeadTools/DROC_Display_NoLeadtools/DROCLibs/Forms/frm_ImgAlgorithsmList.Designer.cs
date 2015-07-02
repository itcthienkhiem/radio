namespace VietBaIT
{
    partial class frm_ImgAlgorithsmList
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
            this.lstIEList = new System.Windows.Forms.ListBox();
            this.cmdAccept = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstIEList
            // 
            this.lstIEList.Dock = System.Windows.Forms.DockStyle.Top;
            this.lstIEList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstIEList.FormattingEnabled = true;
            this.lstIEList.ItemHeight = 16;
            this.lstIEList.Location = new System.Drawing.Point(0, 0);
            this.lstIEList.Name = "lstIEList";
            this.lstIEList.Size = new System.Drawing.Size(489, 436);
            this.lstIEList.TabIndex = 2;
            this.lstIEList.SelectedIndexChanged += new System.EventHandler(this.lstIEList_SelectedIndexChanged);
            // 
            // cmdAccept
            // 
            this.cmdAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdAccept.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdAccept.Location = new System.Drawing.Point(256, 452);
            this.cmdAccept.Name = "cmdAccept";
            this.cmdAccept.Size = new System.Drawing.Size(104, 35);
            this.cmdAccept.TabIndex = 3;
            this.cmdAccept.Text = "Đồng ý";
            this.cmdAccept.UseVisualStyleBackColor = true;
            this.cmdAccept.Click += new System.EventHandler(this.cmdAccept_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(373, 452);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(104, 35);
            this.cmdClose.TabIndex = 4;
            this.cmdClose.Text = "Thoát";
            this.cmdClose.UseVisualStyleBackColor = true;
            // 
            // frm_ImgAlgorithsmList
            // 
            this.AcceptButton = this.cmdAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(489, 499);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdAccept);
            this.Controls.Add(this.lstIEList);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frm_ImgAlgorithsmList";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select Image Processing Configuration";
            this.Load += new System.EventHandler(this.frm_ImgAlgorithsmList_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstIEList;
        private System.Windows.Forms.Button cmdAccept;
        private System.Windows.Forms.Button cmdClose;
    }
}