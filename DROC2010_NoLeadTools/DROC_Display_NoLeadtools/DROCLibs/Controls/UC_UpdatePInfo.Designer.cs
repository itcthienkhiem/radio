namespace VietBaIT.DROC.Controls
{
    partial class UC_UpdatePInfo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UC_UpdatePInfo));
            this.label1 = new System.Windows.Forms.Label();
            this.txtSearchTag = new System.Windows.Forms.TextBox();
            this.grdList = new System.Windows.Forms.DataGridView();
            this.colTagName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTagValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.cmdReLoad = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cmdOption = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tìm kiếm";
            // 
            // txtSearchTag
            // 
            this.txtSearchTag.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchTag.Location = new System.Drawing.Point(59, 3);
            this.txtSearchTag.Name = "txtSearchTag";
            this.txtSearchTag.Size = new System.Drawing.Size(192, 20);
            this.txtSearchTag.TabIndex = 1;
            this.txtSearchTag.TextChanged += new System.EventHandler(this.txtSearchTag_TextChanged);
            // 
            // grdList
            // 
            this.grdList.AllowUserToAddRows = false;
            this.grdList.BackgroundColor = System.Drawing.Color.White;
            this.grdList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTagName,
            this.colTagValue});
            this.grdList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdList.Location = new System.Drawing.Point(0, 0);
            this.grdList.Name = "grdList";
            this.grdList.RowHeadersWidth = 5;
            this.grdList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grdList.Size = new System.Drawing.Size(254, 320);
            this.grdList.TabIndex = 2;
            // 
            // colTagName
            // 
            this.colTagName.DataPropertyName = "TagName";
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.RoyalBlue;
            this.colTagName.DefaultCellStyle = dataGridViewCellStyle1;
            this.colTagName.HeaderText = "Tag";
            this.colTagName.Name = "colTagName";
            this.colTagName.ReadOnly = true;
            this.colTagName.Width = 120;
            // 
            // colTagValue
            // 
            this.colTagValue.DataPropertyName = "TagValue";
            this.colTagValue.HeaderText = "Giá trị";
            this.colTagValue.Name = "colTagValue";
            this.colTagValue.ReadOnly = true;
            this.colTagValue.Width = 220;
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdate.Image")));
            this.cmdUpdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdUpdate.Location = new System.Drawing.Point(134, 6);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(90, 10);
            this.cmdUpdate.TabIndex = 3;
            this.cmdUpdate.Text = "Cập nhật";
            this.cmdUpdate.UseVisualStyleBackColor = true;
            this.cmdUpdate.Visible = false;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
            // 
            // cmdReLoad
            // 
            this.cmdReLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdReLoad.Image = ((System.Drawing.Image)(resources.GetObject("cmdReLoad.Image")));
            this.cmdReLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdReLoad.Location = new System.Drawing.Point(4, 6);
            this.cmdReLoad.Name = "cmdReLoad";
            this.cmdReLoad.Size = new System.Drawing.Size(124, 10);
            this.cmdReLoad.TabIndex = 4;
            this.cmdReLoad.Text = "Load lại thông tin";
            this.cmdReLoad.UseVisualStyleBackColor = true;
            this.cmdReLoad.Visible = false;
            this.cmdReLoad.Click += new System.EventHandler(this.cmdReLoad_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtSearchTag);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(254, 29);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cmdOption);
            this.panel2.Controls.Add(this.cmdUpdate);
            this.panel2.Controls.Add(this.cmdReLoad);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 349);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(254, 43);
            this.panel2.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.grdList);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 29);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(254, 320);
            this.panel3.TabIndex = 7;
            // 
            // cmdOption
            // 
            this.cmdOption.Image = ((System.Drawing.Image)(resources.GetObject("cmdOption.Image")));
            this.cmdOption.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.cmdOption.Location = new System.Drawing.Point(159, 6);
            this.cmdOption.Name = "cmdOption";
            this.cmdOption.Size = new System.Drawing.Size(92, 26);
            this.cmdOption.TabIndex = 5;
            this.cmdOption.Text = "Tùy chọn";
            this.cmdOption.UseVisualStyleBackColor = true;
            this.cmdOption.Click += new System.EventHandler(this.cmdOption_Click);
            // 
            // UC_UpdatePInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "UC_UpdatePInfo";
            this.Size = new System.Drawing.Size(254, 392);
            ((System.ComponentModel.ISupportInitialize)(this.grdList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSearchTag;
        private System.Windows.Forms.DataGridView grdList;
        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.Button cmdReLoad;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTagName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTagValue;
        private System.Windows.Forms.Button cmdOption;
    }
}
