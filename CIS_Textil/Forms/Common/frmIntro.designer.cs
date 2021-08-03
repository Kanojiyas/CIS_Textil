namespace CIS_Textil
{
    partial class frmIntro
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIntro));
            this.Label2 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblLicense = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.timerFadeIn = new System.Windows.Forms.Timer(this.components);
            this.timerFadeOut = new System.Windows.Forms.Timer(this.components);
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.OpenFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.btnExit = new CIS_CLibrary.CIS_Button();
            this.btnActOnline = new CIS_CLibrary.CIS_Button();
            this.btnActOffline = new CIS_CLibrary.CIS_Button();
            this.lblAppVer = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Font = new System.Drawing.Font("Lucida Bright", 32F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.Color.White;
            this.Label2.Location = new System.Drawing.Point(92, 164);
            this.Label2.Moveable = false;
            this.Label2.Name = "Label2";
            this.Label2.NameOfControl = null;
            this.Label2.Size = new System.Drawing.Size(438, 48);
            this.Label2.TabIndex = 10;
            this.Label2.Text = "Crocus IT Solutions";
            // 
            // lblLicense
            // 
            this.lblLicense.AutoSize = true;
            this.lblLicense.BackColor = System.Drawing.Color.Transparent;
            this.lblLicense.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicense.ForeColor = System.Drawing.Color.White;
            this.lblLicense.Location = new System.Drawing.Point(8, 324);
            this.lblLicense.Moveable = false;
            this.lblLicense.Name = "lblLicense";
            this.lblLicense.NameOfControl = null;
            this.lblLicense.Size = new System.Drawing.Size(84, 13);
            this.lblLicense.TabIndex = 14;
            this.lblLicense.Text = "Licensed to : ";
            // 
            // timerFadeIn
            // 
            this.timerFadeIn.Interval = 20;
            this.timerFadeIn.Tick += new System.EventHandler(this.timerFadeIn_Tick);
            // 
            // timerFadeOut
            // 
            this.timerFadeOut.Interval = 10;
            this.timerFadeOut.Tick += new System.EventHandler(this.timerFadeOut_Tick);
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.FileName = "OpenFileDialog1";
            this.OpenFileDialog1.Filter = "*.mdf|*.mdf";
            // 
            // OpenFileDialog2
            // 
            this.OpenFileDialog2.FileName = "OpenFileDialog2";
            this.OpenFileDialog2.Filter = "*.ldf|*.ldf";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Purple;
            this.btnExit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Location = new System.Drawing.Point(495, 2);
            this.btnExit.Moveable = false;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(71, 43);
            this.btnExit.TabIndex = 17;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnActOnline
            // 
            this.btnActOnline.BackColor = System.Drawing.Color.Purple;
            this.btnActOnline.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActOnline.Location = new System.Drawing.Point(349, 2);
            this.btnActOnline.Moveable = false;
            this.btnActOnline.Name = "btnActOnline";
            this.btnActOnline.Size = new System.Drawing.Size(71, 43);
            this.btnActOnline.TabIndex = 15;
            this.btnActOnline.Text = "Activate Online";
            this.btnActOnline.UseVisualStyleBackColor = false;
            this.btnActOnline.Visible = false;
            // 
            // btnActOffline
            // 
            this.btnActOffline.BackColor = System.Drawing.Color.Purple;
            this.btnActOffline.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActOffline.Location = new System.Drawing.Point(422, 2);
            this.btnActOffline.Moveable = false;
            this.btnActOffline.Name = "btnActOffline";
            this.btnActOffline.Size = new System.Drawing.Size(71, 43);
            this.btnActOffline.TabIndex = 16;
            this.btnActOffline.Text = "Activate Offline";
            this.btnActOffline.UseVisualStyleBackColor = false;
            this.btnActOffline.Visible = false;
            this.btnActOffline.Click += new System.EventHandler(this.btnActOffline_Click);
            // 
            // lblAppVer
            // 
            this.lblAppVer.AutoSize = true;
            this.lblAppVer.BackColor = System.Drawing.Color.Transparent;
            this.lblAppVer.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblAppVer.ForeColor = System.Drawing.Color.MediumVioletRed;
            this.lblAppVer.Location = new System.Drawing.Point(159, 403);
            this.lblAppVer.Moveable = false;
            this.lblAppVer.Name = "lblAppVer";
            this.lblAppVer.NameOfControl = null;
            this.lblAppVer.Size = new System.Drawing.Size(56, 13);
            this.lblAppVer.TabIndex = 19;
            this.lblAppVer.Text = "Version";
            // 
            // Timer1
            // 
            this.Timer1.Enabled = true;
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // frmIntro
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::CIS_Textil.Properties.Resources.Intro;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(568, 425);
            this.Controls.Add(this.lblAppVer);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnActOnline);
            this.Controls.Add(this.btnActOffline);
            this.Controls.Add(this.lblLicense);
            this.Controls.Add(this.Label2);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmIntro";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Load += new System.EventHandler(this.frmIntro_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal CIS_CLibrary.CIS_TextLabel Label2;
        internal CIS_CLibrary.CIS_TextLabel lblLicense;
        internal CIS_CLibrary.CIS_Button btnExit;
        internal CIS_CLibrary.CIS_Button btnActOnline;
        internal CIS_CLibrary.CIS_Button btnActOffline;
        private System.Windows.Forms.Timer timerFadeIn;
        private System.Windows.Forms.Timer timerFadeOut;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog2;
        private CIS_CLibrary.CIS_TextLabel lblAppVer;
        internal System.Windows.Forms.Timer Timer1;
    }
}