namespace VietBaIT.Controls
{
    partial class ProjectionControl
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
            this.ProjectionObject = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ProjectionObject
            // 
            this.ProjectionObject.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ProjectionObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ProjectionObject.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProjectionObject.ForeColor = System.Drawing.Color.Black;
            this.ProjectionObject.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ProjectionObject.Location = new System.Drawing.Point(0, 0);
            this.ProjectionObject.Name = "ProjectionObject";
            this.ProjectionObject.Size = new System.Drawing.Size(191, 64);
            this.ProjectionObject.TabIndex = 0;
            this.ProjectionObject.Text = "Projection Name";
            this.ProjectionObject.UseVisualStyleBackColor = false;
            // 
            // ProjectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ProjectionObject);
            this.Name = "ProjectionControl";
            this.Size = new System.Drawing.Size(191, 64);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ProjectionObject;
    }
}
