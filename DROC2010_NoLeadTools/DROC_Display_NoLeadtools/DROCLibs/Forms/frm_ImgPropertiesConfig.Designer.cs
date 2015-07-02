namespace VietBaIT
{
    partial class frm_ImgPropertiesConfig
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
            this.lstAllTags = new System.Windows.Forms.ListBox();
            this.lstViewTag = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstAllTags
            // 
            this.lstAllTags.FormattingEnabled = true;
            this.lstAllTags.Location = new System.Drawing.Point(2, 3);
            this.lstAllTags.Name = "lstAllTags";
            this.lstAllTags.Size = new System.Drawing.Size(302, 394);
            this.lstAllTags.TabIndex = 0;
            // 
            // lstViewTag
            // 
            this.lstViewTag.FormattingEnabled = true;
            this.lstViewTag.Location = new System.Drawing.Point(352, 3);
            this.lstViewTag.Name = "lstViewTag";
            this.lstViewTag.Size = new System.Drawing.Size(289, 394);
            this.lstViewTag.TabIndex = 1;
            // 
            // frm_ImgPropertiesConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 469);
            this.Controls.Add(this.lstViewTag);
            this.Controls.Add(this.lstAllTags);
            this.Name = "frm_ImgPropertiesConfig";
            this.Text = "frm_ImgPropertiesConfig";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstAllTags;
        private System.Windows.Forms.ListBox lstViewTag;
    }
}