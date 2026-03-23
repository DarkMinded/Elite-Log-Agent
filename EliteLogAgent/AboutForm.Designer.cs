namespace EliteLogAgent
{
    partial class About
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
            var resources = new System.ComponentModel.ComponentResourceManager(typeof(About));
            logoBox = new System.Windows.Forms.PictureBox();
            titleLabel = new System.Windows.Forms.Label();
            aboutLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)logoBox).BeginInit();
            SuspendLayout();
            // 
            // logoBox
            // 
            logoBox.Location = new System.Drawing.Point(14, 14);
            logoBox.Margin = new System.Windows.Forms.Padding(4);
            logoBox.Name = "logoBox";
            logoBox.Size = new System.Drawing.Size(56, 55);
            logoBox.TabIndex = 0;
            logoBox.TabStop = false;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            titleLabel.Location = new System.Drawing.Point(76, 14);
            titleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new System.Drawing.Size(151, 24);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "Software Name";
            // 
            // aboutLabel
            // 
            aboutLabel.AutoSize = true;
            aboutLabel.Location = new System.Drawing.Point(77, 41);
            aboutLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            aboutLabel.Name = "aboutLabel";
            aboutLabel.Size = new System.Drawing.Size(294, 150);
            aboutLabel.TabIndex = 2;
            aboutLabel.Text = resources.GetString("aboutLabel.Text");
            // 
            // About
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(410, 212);
            Controls.Add(aboutLabel);
            Controls.Add(titleLabel);
            Controls.Add(logoBox);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4);
            Name = "About";
            Text = "About";
            Load += About_Load;
            ((System.ComponentModel.ISupportInitialize)logoBox).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox logoBox;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label aboutLabel;
    }
}