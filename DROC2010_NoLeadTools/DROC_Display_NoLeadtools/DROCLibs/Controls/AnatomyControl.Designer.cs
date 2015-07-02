namespace VietBaIT.Controls
{
    partial class AnatomyControl
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
            this.AnatomyObject = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // AnatomyObject
            // 
            this.AnatomyObject.BackColor = System.Drawing.Color.WhiteSmoke;
            this.AnatomyObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AnatomyObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AnatomyObject.ForeColor = System.Drawing.Color.Black;
            this.AnatomyObject.Location = new System.Drawing.Point(0, 0);
            this.AnatomyObject.Name = "AnatomyObject";
            this.AnatomyObject.Size = new System.Drawing.Size(191, 64);
            this.AnatomyObject.TabIndex = 0;
            this.AnatomyObject.Text = "Anatomy Name";
            this.AnatomyObject.UseVisualStyleBackColor = false;
            // 
            // AnatomyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AnatomyObject);
            this.Name = "AnatomyControl";
            this.Size = new System.Drawing.Size(191, 64);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AnatomyObject;
    }
}
