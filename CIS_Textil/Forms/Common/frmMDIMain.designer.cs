namespace CIS_Textil
{
    partial class frmMDIMain
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
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.mnuMain.BackColor = System.Drawing.Color.Transparent;
            this.mnuMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.mnuMain.Dock = System.Windows.Forms.DockStyle.None;
            this.mnuMain.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mnuMain.ImageScalingSize = new System.Drawing.Size(16, 20);
            this.mnuMain.Location = new System.Drawing.Point(580, 121);
            this.mnuMain.Margin = new System.Windows.Forms.Padding(10);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.ShowItemToolTips = true;
            this.mnuMain.Size = new System.Drawing.Size(202, 24);
            this.mnuMain.TabIndex = 91;
            // 
            // frmMDIMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1362, 744);
            this.Controls.Add(this.mnuMain);
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Name = "frmMDIMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMDIMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.MenuStrip mnuMain;
    }
}