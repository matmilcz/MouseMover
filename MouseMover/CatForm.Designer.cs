namespace MouseMover
{
    partial class CatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CatForm));
            this.CatPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.CatPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // CatPictureBox
            // 
            this.CatPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("CatPictureBox.Image")));
            this.CatPictureBox.InitialImage = null;
            this.CatPictureBox.Location = new System.Drawing.Point(12, 12);
            this.CatPictureBox.Name = "CatPictureBox";
            this.CatPictureBox.Size = new System.Drawing.Size(55, 53);
            this.CatPictureBox.TabIndex = 0;
            this.CatPictureBox.TabStop = false;
            // 
            // CatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(78, 75);
            this.Controls.Add(this.CatPictureBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CatForm";
            this.Text = "CatForm";
            ((System.ComponentModel.ISupportInitialize)(this.CatPictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox CatPictureBox;
    }
}