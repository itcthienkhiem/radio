namespace VietBaIT.Controls
{
    partial class ScheduledControl
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScheduledControl));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblPrintCount = new System.Windows.Forms.Label();
            this.lblNew = new System.Windows.Forms.Label();
            this.AnatomyObject = new System.Windows.Forms.Button();
            this.lblDelete = new System.Windows.Forms.Label();
            this.lblReject = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPrintCount
            // 
            this.lblPrintCount.AutoSize = true;
            this.lblPrintCount.BackColor = System.Drawing.Color.Transparent;
            this.lblPrintCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrintCount.Location = new System.Drawing.Point(8, 8);
            this.lblPrintCount.Name = "lblPrintCount";
            this.lblPrintCount.Size = new System.Drawing.Size(0, 16);
            this.lblPrintCount.TabIndex = 1;
            // 
            // lblNew
            // 
            this.lblNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNew.BackColor = System.Drawing.Color.Transparent;
            this.lblNew.Image = ((System.Drawing.Image)(resources.GetObject("lblNew.Image")));
            this.lblNew.Location = new System.Drawing.Point(101, 3);
            this.lblNew.Name = "lblNew";
            this.lblNew.Size = new System.Drawing.Size(20, 20);
            this.lblNew.TabIndex = 2;
            // 
            // AnatomyObject
            // 
            this.AnatomyObject.BackColor = System.Drawing.SystemColors.Control;
            this.AnatomyObject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.AnatomyObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnatomyObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AnatomyObject.ForeColor = System.Drawing.Color.Black;
            this.AnatomyObject.Image = global:: DROCLibs.Properties.Resources.PRINTER;
            this.AnatomyObject.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.AnatomyObject.Location = new System.Drawing.Point(0, 0);
            this.AnatomyObject.Name = "AnatomyObject";
            this.AnatomyObject.Size = new System.Drawing.Size(124, 71);
            this.AnatomyObject.TabIndex = 0;
            this.AnatomyObject.Text = "Scheduled";
            this.AnatomyObject.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.AnatomyObject.UseVisualStyleBackColor = false;
            // 
            // lblDelete
            // 
            this.lblDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDelete.BackColor = System.Drawing.Color.Transparent;
            this.lblDelete.Image = ((System.Drawing.Image)(resources.GetObject("lblDelete.Image")));
            this.lblDelete.Location = new System.Drawing.Point(101, 24);
            this.lblDelete.Name = "lblDelete";
            this.lblDelete.Size = new System.Drawing.Size(20, 20);
            this.lblDelete.TabIndex = 3;
            // 
            // lblReject
            // 
            this.lblReject.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReject.BackColor = System.Drawing.Color.Transparent;
            this.lblReject.Image = ((System.Drawing.Image)(resources.GetObject("lblReject.Image")));
            this.lblReject.Location = new System.Drawing.Point(101, 46);
            this.lblReject.Name = "lblReject";
            this.lblReject.Size = new System.Drawing.Size(20, 20);
            this.lblReject.TabIndex = 4;
            // 
            // ScheduledControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.lblReject);
            this.Controls.Add(this.lblDelete);
            this.Controls.Add(this.lblNew);
            this.Controls.Add(this.lblPrintCount);
            this.Controls.Add(this.AnatomyObject);
            this.Name = "ScheduledControl";
            this.Size = new System.Drawing.Size(124, 71);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AnatomyObject;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label lblPrintCount;
        private System.Windows.Forms.Label lblNew;
        private System.Windows.Forms.Label lblDelete;
        private System.Windows.Forms.Label lblReject;
    }
}
