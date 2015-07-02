namespace VietBaIT.Controls
{
    partial class OScheduledControl
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
            try
            {
                this.components = new System.ComponentModel.Container();
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OScheduledControl));
                this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
                this.imageList2 = new System.Windows.Forms.ImageList(this.components);
                this.imageList1 = new System.Windows.Forms.ImageList(this.components);
                this.lblTitle = new System.Windows.Forms.Label();
                this.lblIcon = new System.Windows.Forms.Label();
                this.AnatomyObject = new System.Windows.Forms.Button();
                this.SuspendLayout();
                // 
                // imageList2
                // 
                this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
                this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
                this.imageList2.Images.SetKeyName(0, "OK.PNG");
                this.imageList2.Images.SetKeyName(1, "REMOVE.PNG");
                this.imageList2.Images.SetKeyName(2, "file_copy.png");
                this.imageList2.Images.SetKeyName(3, "TOOLS.PNG");
                this.imageList2.Images.SetKeyName(4, "IMAGE.PNG");
                // 
                // imageList1
                // 
                this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
                this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
                this.imageList1.Images.SetKeyName(0, "OK.PNG");
                this.imageList1.Images.SetKeyName(1, "REMOVE.PNG");
                this.imageList1.Images.SetKeyName(2, "file_copy.png");
                this.imageList1.Images.SetKeyName(3, "TOOLS.PNG");
                this.imageList1.Images.SetKeyName(4, "IMAGE.PNG");
                // 
                // lblTitle
                // 
                this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
                this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.lblTitle.ForeColor = System.Drawing.Color.White;
                this.lblTitle.Location = new System.Drawing.Point(0, 0);
                this.lblTitle.Name = "lblTitle";
                this.lblTitle.Size = new System.Drawing.Size(152, 29);
                this.lblTitle.TabIndex = 3;
                this.lblTitle.Text = "CHEST/AP";
                this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
                this.lblTitle.Click += new System.EventHandler(this.label1_Click);
                // 
                // lblIcon
                // 
                this.lblIcon.BackColor = System.Drawing.Color.WhiteSmoke;
                this.lblIcon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.lblIcon.Dock = System.Windows.Forms.DockStyle.Left;
                this.lblIcon.ImageIndex = 0;
                this.lblIcon.ImageList = this.imageList2;
                this.lblIcon.Location = new System.Drawing.Point(0, 29);
                this.lblIcon.Name = "lblIcon";
                this.lblIcon.Size = new System.Drawing.Size(45, 106);
                this.lblIcon.TabIndex = 4;
                this.lblIcon.Click += new System.EventHandler(this.lblIcon_Click_1);
                // 
                // AnatomyObject
                // 
                this.AnatomyObject.BackColor = System.Drawing.SystemColors.Control;
                this.AnatomyObject.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                this.AnatomyObject.Dock = System.Windows.Forms.DockStyle.Fill;
                this.AnatomyObject.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                this.AnatomyObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                this.AnatomyObject.ForeColor = System.Drawing.Color.Black;
                this.AnatomyObject.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
                this.AnatomyObject.Location = new System.Drawing.Point(45, 29);
                this.AnatomyObject.Name = "AnatomyObject";
                this.AnatomyObject.Size = new System.Drawing.Size(107, 106);
                this.AnatomyObject.TabIndex = 5;
                this.AnatomyObject.Text = "Scheduled";
                this.AnatomyObject.UseVisualStyleBackColor = false;
                this.AnatomyObject.Click += new System.EventHandler(this.AnatomyObject_Click_2);
                // 
                // OScheduledControl
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
                this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.Controls.Add(this.AnatomyObject);
                this.Controls.Add(this.lblIcon);
                this.Controls.Add(this.lblTitle);
                this.Name = "OScheduledControl";
                this.Size = new System.Drawing.Size(152, 135);
                this.ResumeLayout(false);
            }
            catch
            {
            }

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblIcon;
        private System.Windows.Forms.Button AnatomyObject;
    }
}
