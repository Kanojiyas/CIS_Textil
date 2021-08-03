
    partial class frmTrnsIface
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
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.lblBorderLeft = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblBorderRight = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.pnlCaptionBar = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.lblFormName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblUUser = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.LblUpdateUser = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblCUser = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblCreatedUser = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblHelpText = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblHelp = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.pnlContent.SuspendLayout();
            this.pnlCaptionBar.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContent.BackColor = System.Drawing.Color.MintCream;
            this.pnlContent.Controls.Add(this.pnlRight);
            this.pnlContent.Controls.Add(this.pnlLeft);
            this.pnlContent.Controls.Add(this.lblBorderLeft);
            this.pnlContent.Controls.Add(this.lblBorderRight);
            this.pnlContent.Location = new System.Drawing.Point(0, 26);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(1072, 496);
            this.pnlContent.TabIndex = 0;
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.DodgerBlue;
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(1069, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(3, 496);
            this.pnlRight.TabIndex = 4;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.DodgerBlue;
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(3, 496);
            this.pnlLeft.TabIndex = 3;
            // 
            // lblBorderLeft
            // 
            this.lblBorderLeft.AutoSize = true;
            this.lblBorderLeft.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblBorderLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblBorderLeft.Location = new System.Drawing.Point(0, 0);
            this.lblBorderLeft.Moveable = false;
            this.lblBorderLeft.Name = "lblBorderLeft";
            this.lblBorderLeft.NameOfControl = null;
            this.lblBorderLeft.Size = new System.Drawing.Size(0, 13);
            this.lblBorderLeft.TabIndex = 2;
            // 
            // lblBorderRight
            // 
            this.lblBorderRight.AutoSize = true;
            this.lblBorderRight.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblBorderRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblBorderRight.Location = new System.Drawing.Point(1072, 0);
            this.lblBorderRight.Moveable = false;
            this.lblBorderRight.Name = "lblBorderRight";
            this.lblBorderRight.NameOfControl = null;
            this.lblBorderRight.Size = new System.Drawing.Size(0, 13);
            this.lblBorderRight.TabIndex = 1;
            // 
            // pnlCaptionBar
            // 
            this.pnlCaptionBar.BackColor = System.Drawing.Color.DodgerBlue;
            this.pnlCaptionBar.Controls.Add(this.btnCancel);
            this.pnlCaptionBar.Controls.Add(this.btnMinimize);
            this.pnlCaptionBar.Controls.Add(this.lblFormName);
            this.pnlCaptionBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCaptionBar.Location = new System.Drawing.Point(0, 0);
            this.pnlCaptionBar.Name = "pnlCaptionBar";
            this.pnlCaptionBar.Size = new System.Drawing.Size(1072, 26);
            this.pnlCaptionBar.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.GhostWhite;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.DarkRed;
            this.btnCancel.Location = new System.Drawing.Point(1045, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(23, 20);
            this.btnCancel.TabIndex = 404;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "X";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimize.BackColor = System.Drawing.Color.GhostWhite;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimize.Location = new System.Drawing.Point(1019, 3);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(23, 20);
            this.btnMinimize.TabIndex = 403;
            this.btnMinimize.TabStop = false;
            this.btnMinimize.Text = "---";
            this.btnMinimize.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // lblFormName
            // 
            this.lblFormName.AutoSize = true;
            this.lblFormName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormName.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblFormName.Location = new System.Drawing.Point(5, 6);
            this.lblFormName.Moveable = false;
            this.lblFormName.Name = "lblFormName";
            this.lblFormName.NameOfControl = null;
            this.lblFormName.Size = new System.Drawing.Size(0, 14);
            this.lblFormName.TabIndex = 402;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BackColor = System.Drawing.Color.DodgerBlue;
            this.pnlBottom.Controls.Add(this.lblUUser);
            this.pnlBottom.Controls.Add(this.LblUpdateUser);
            this.pnlBottom.Controls.Add(this.lblCUser);
            this.pnlBottom.Controls.Add(this.lblCreatedUser);
            this.pnlBottom.Controls.Add(this.lblHelpText);
            this.pnlBottom.Controls.Add(this.lblHelp);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 522);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1072, 25);
            this.pnlBottom.TabIndex = 466;
            // 
            // lblUUser
            // 
            this.lblUUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUUser.AutoSize = true;
            this.lblUUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblUUser.ForeColor = System.Drawing.Color.Maroon;
            this.lblUUser.Location = new System.Drawing.Point(911, 3);
            this.lblUUser.Moveable = false;
            this.lblUUser.Name = "lblUUser";
            this.lblUUser.NameOfControl = null;
            this.lblUUser.Size = new System.Drawing.Size(88, 17);
            this.lblUUser.TabIndex = 406;
            this.lblUUser.Text = "User Name";
            this.lblUUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LblUpdateUser
            // 
            this.LblUpdateUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.LblUpdateUser.AutoSize = true;
            this.LblUpdateUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.LblUpdateUser.Location = new System.Drawing.Point(797, 3);
            this.LblUpdateUser.Moveable = false;
            this.LblUpdateUser.Name = "LblUpdateUser";
            this.LblUpdateUser.NameOfControl = null;
            this.LblUpdateUser.Size = new System.Drawing.Size(118, 17);
            this.LblUpdateUser.TabIndex = 405;
            this.LblUpdateUser.Text = "Updated User :";
            // 
            // lblCUser
            // 
            this.lblCUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCUser.AutoSize = true;
            this.lblCUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblCUser.ForeColor = System.Drawing.Color.Maroon;
            this.lblCUser.Location = new System.Drawing.Point(481, 3);
            this.lblCUser.Moveable = false;
            this.lblCUser.Name = "lblCUser";
            this.lblCUser.NameOfControl = null;
            this.lblCUser.Size = new System.Drawing.Size(88, 17);
            this.lblCUser.TabIndex = 404;
            this.lblCUser.Text = "User Name";
            this.lblCUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCreatedUser
            // 
            this.lblCreatedUser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCreatedUser.AutoSize = true;
            this.lblCreatedUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblCreatedUser.Location = new System.Drawing.Point(371, 3);
            this.lblCreatedUser.Moveable = false;
            this.lblCreatedUser.Name = "lblCreatedUser";
            this.lblCreatedUser.NameOfControl = null;
            this.lblCreatedUser.Size = new System.Drawing.Size(114, 17);
            this.lblCreatedUser.TabIndex = 403;
            this.lblCreatedUser.Text = "Created User :";
            // 
            // lblHelpText
            // 
            this.lblHelpText.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblHelpText.AutoSize = true;
            this.lblHelpText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblHelpText.ForeColor = System.Drawing.Color.Maroon;
            this.lblHelpText.Location = new System.Drawing.Point(50, 3);
            this.lblHelpText.Moveable = false;
            this.lblHelpText.Name = "lblHelpText";
            this.lblHelpText.NameOfControl = null;
            this.lblHelpText.Size = new System.Drawing.Size(77, 17);
            this.lblHelpText.TabIndex = 402;
            this.lblHelpText.Text = "Help Text";
            this.lblHelpText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHelp
            // 
            this.lblHelp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblHelp.AutoSize = true;
            this.lblHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHelp.Location = new System.Drawing.Point(3, 3);
            this.lblHelp.Moveable = false;
            this.lblHelp.Name = "lblHelp";
            this.lblHelp.NameOfControl = null;
            this.lblHelp.Size = new System.Drawing.Size(51, 17);
            this.lblHelp.TabIndex = 401;
            this.lblHelp.Text = "Help :";
            // 
            // frmTrnsIface
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1072, 547);
            this.ControlBox = false;
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlCaptionBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTrnsIface";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Interface Form";
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlCaptionBar.ResumeLayout(false);
            this.pnlCaptionBar.PerformLayout();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlCaptionBar;
        internal System.Windows.Forms.Panel pnlBottom;
        internal CIS_CLibrary.CIS_TextLabel LblUpdateUser;
        internal CIS_CLibrary.CIS_TextLabel lblCreatedUser;
        internal CIS_CLibrary.CIS_TextLabel lblHelp;
        public CIS_CLibrary.CIS_TextLabel lblHelpText;
        private CIS_CLibrary.CIS_TextLabel lblBorderRight;
        private CIS_CLibrary.CIS_TextLabel lblBorderLeft;
        private System.Windows.Forms.Button btnMinimize;
        private System.Windows.Forms.Button btnCancel;
        public CIS_CLibrary.CIS_TextLabel lblFormName;
        public CIS_CLibrary.CIS_TextLabel lblUUser;
        public CIS_CLibrary.CIS_TextLabel lblCUser;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        
    }
