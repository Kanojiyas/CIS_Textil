namespace CIS_Textil
{
    partial class frmAbout
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
            this.Label5 = new CIS_CLibrary.CIS_TextLabel();
            this.lblLicense = new CIS_CLibrary.CIS_TextLabel();
            this.lblversion = new CIS_CLibrary.CIS_TextLabel();
            this.Label1 = new CIS_CLibrary.CIS_TextLabel();
            this.Label2 = new CIS_CLibrary.CIS_TextLabel();
            this.SuspendLayout();
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Font = new System.Drawing.Font("Lucida Bright", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.Color.White;
            this.Label5.Location = new System.Drawing.Point(328, 300);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(205, 15);
            this.Label5.TabIndex = 19;
            this.Label5.Text = "We nourish yours Business...";
            // 
            // lblLicense
            // 
            this.lblLicense.AutoSize = true;
            this.lblLicense.BackColor = System.Drawing.Color.Transparent;
            this.lblLicense.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicense.ForeColor = System.Drawing.Color.White;
            this.lblLicense.Location = new System.Drawing.Point(55, 449);
            this.lblLicense.Name = "lblLicense";
            this.lblLicense.Size = new System.Drawing.Size(112, 17);
            this.lblLicense.TabIndex = 18;
            this.lblLicense.Text = "Licensed to : ";
            // 
            // lblversion
            // 
            this.lblversion.AutoSize = true;
            this.lblversion.BackColor = System.Drawing.Color.Transparent;
            this.lblversion.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblversion.ForeColor = System.Drawing.Color.White;
            this.lblversion.Location = new System.Drawing.Point(522, 449);
            this.lblversion.Name = "lblversion";
            this.lblversion.Size = new System.Drawing.Size(78, 17);
            this.lblversion.TabIndex = 17;
            this.lblversion.Text = "Version :";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Lucida Bright", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.White;
            this.Label1.Location = new System.Drawing.Point(328, 216);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(149, 15);
            this.Label1.TabIndex = 16;
            this.Label1.Text = "IT Solutions Pvt. Ltd.";
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Lucida Bright", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.Color.White;
            this.Label2.Location = new System.Drawing.Point(243, 152);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(255, 72);
            this.Label2.TabIndex = 15;
            this.Label2.Text = "Crocus";
            // 
            // frmAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;            
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(826, 570);
            this.ControlBox = false;
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.lblLicense);
            this.Controls.Add(this.lblversion);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.Label2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmAbout_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAbout_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal CIS_CLibrary.CIS_TextLabel Label5;
        internal CIS_CLibrary.CIS_TextLabel lblLicense;
        internal CIS_CLibrary.CIS_TextLabel lblversion;
        internal CIS_CLibrary.CIS_TextLabel Label1;
        internal CIS_CLibrary.CIS_TextLabel Label2;
    }
}