namespace CIS_Textil
{
    partial class frmChat
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
            this.lblBottom = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblTop = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblRight = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblLeft = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtMessage = new System.Windows.Forms.RichTextBox();
            this.lblUserNM = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtHistory = new System.Windows.Forms.RichTextBox();
            this.btnSentMessage = new System.Windows.Forms.Button();
            this.btnSentFile = new System.Windows.Forms.Button();
            this.btnClearHistory = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel = new CIS_CLibrary.CIS_Panel();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblBottom
            // 
            this.lblBottom.AutoSize = true;
            this.lblBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBottom.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblBottom.Location = new System.Drawing.Point(0, 514);
            this.lblBottom.Moveable = false;
            this.lblBottom.Name = "lblBottom";
            this.lblBottom.NameOfControl = null;
            this.lblBottom.Size = new System.Drawing.Size(0, 13);
            this.lblBottom.TabIndex = 3;
            // 
            // lblTop
            // 
            this.lblTop.AutoSize = true;
            this.lblTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTop.Location = new System.Drawing.Point(0, 0);
            this.lblTop.Moveable = false;
            this.lblTop.Name = "lblTop";
            this.lblTop.NameOfControl = null;
            this.lblTop.Size = new System.Drawing.Size(0, 13);
            this.lblTop.TabIndex = 2;
            // 
            // lblRight
            // 
            this.lblRight.AutoSize = true;
            this.lblRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblRight.Location = new System.Drawing.Point(365, 0);
            this.lblRight.Moveable = false;
            this.lblRight.Name = "lblRight";
            this.lblRight.NameOfControl = null;
            this.lblRight.Size = new System.Drawing.Size(0, 13);
            this.lblRight.TabIndex = 1;
            // 
            // lblLeft
            // 
            this.lblLeft.AutoSize = true;
            this.lblLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLeft.Location = new System.Drawing.Point(0, 0);
            this.lblLeft.Moveable = false;
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.NameOfControl = null;
            this.lblLeft.Size = new System.Drawing.Size(0, 13);
            this.lblLeft.TabIndex = 0;
            // 
            // txtMessage
            // 
            this.txtMessage.BackColor = System.Drawing.Color.Linen;
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessage.EnableAutoDragDrop = true;
            this.txtMessage.Location = new System.Drawing.Point(-1, 355);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(0);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(365, 71);
            this.txtMessage.TabIndex = 15;
            this.txtMessage.Text = "";
            // 
            // lblUserNM
            // 
            this.lblUserNM.AutoSize = true;
            this.lblUserNM.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblUserNM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserNM.Location = new System.Drawing.Point(8, 8);
            this.lblUserNM.Moveable = false;
            this.lblUserNM.Name = "lblUserNM";
            this.lblUserNM.NameOfControl = null;
            this.lblUserNM.Size = new System.Drawing.Size(40, 17);
            this.lblUserNM.TabIndex = 14;
            this.lblUserNM.Text = "user";
            // 
            // txtHistory
            // 
            this.txtHistory.BackColor = System.Drawing.Color.AliceBlue;
            this.txtHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHistory.Enabled = false;
            this.txtHistory.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHistory.Location = new System.Drawing.Point(2, 60);
            this.txtHistory.Margin = new System.Windows.Forms.Padding(0);
            this.txtHistory.Name = "txtHistory";
            this.txtHistory.Size = new System.Drawing.Size(360, 294);
            this.txtHistory.TabIndex = 1;
            this.txtHistory.Text = "";
            // 
            // btnSentMessage
            // 
            this.btnSentMessage.BackColor = System.Drawing.Color.Teal;
            this.btnSentMessage.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSentMessage.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSentMessage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSentMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSentMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSentMessage.ForeColor = System.Drawing.Color.White;
            this.btnSentMessage.Location = new System.Drawing.Point(33, 438);
            this.btnSentMessage.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.btnSentMessage.Name = "btnSentMessage";
            this.btnSentMessage.Size = new System.Drawing.Size(86, 36);
            this.btnSentMessage.TabIndex = 8;
            this.btnSentMessage.Text = "Send Msg";
            this.btnSentMessage.UseVisualStyleBackColor = false;
            // 
            // btnSentFile
            // 
            this.btnSentFile.BackColor = System.Drawing.Color.Teal;
            this.btnSentFile.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnSentFile.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnSentFile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnSentFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSentFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSentFile.ForeColor = System.Drawing.Color.White;
            this.btnSentFile.Location = new System.Drawing.Point(133, 438);
            this.btnSentFile.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.btnSentFile.Name = "btnSentFile";
            this.btnSentFile.Size = new System.Drawing.Size(86, 36);
            this.btnSentFile.TabIndex = 9;
            this.btnSentFile.Text = "Send File";
            this.btnSentFile.UseVisualStyleBackColor = false;
            // 
            // btnClearHistory
            // 
            this.btnClearHistory.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.btnClearHistory.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnClearHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearHistory.ForeColor = System.Drawing.Color.White;
            this.btnClearHistory.Location = new System.Drawing.Point(257, 31);
            this.btnClearHistory.Margin = new System.Windows.Forms.Padding(1);
            this.btnClearHistory.Name = "btnClearHistory";
            this.btnClearHistory.Size = new System.Drawing.Size(98, 25);
            this.btnClearHistory.TabIndex = 16;
            this.btnClearHistory.Text = "Clear History";
            this.btnClearHistory.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Teal;
            this.btnClose.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(233, 438);
            this.btnClose.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(86, 36);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // panel
            // 
            this.panel.AssociatedSplitter = null;
            this.panel.BackColor = System.Drawing.Color.Transparent;
            this.panel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.panel.CaptionFont = new System.Drawing.Font("Trebuchet MS", 12.5F, System.Drawing.FontStyle.Bold);
            this.panel.CaptionHeight = 27;
            this.panel.ColorScheme = CIS_CLibrary.ColorScheme.Custom;
            this.panel.Controls.Add(this.btnClose);
            this.panel.Controls.Add(this.btnClearHistory);
            this.panel.Controls.Add(this.btnSentFile);
            this.panel.Controls.Add(this.btnSentMessage);
            this.panel.Controls.Add(this.txtHistory);
            this.panel.Controls.Add(this.lblUserNM);
            this.panel.Controls.Add(this.txtMessage);
            this.panel.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(65)))), ((int)(((byte)(118)))));
            this.panel.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.panel.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.panel.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.panel.CustomColors.CaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(164)))), ((int)(((byte)(224)))));
            this.panel.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(225)))), ((int)(((byte)(252)))));
            this.panel.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(222)))));
            this.panel.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(203)))), ((int)(((byte)(136)))));
            this.panel.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.panel.CustomColors.CollapsedCaptionText = System.Drawing.SystemColors.ControlText;
            this.panel.CustomColors.ContentGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(190)))), ((int)(((byte)(245)))));
            this.panel.CustomColors.ContentGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(218)))), ((int)(((byte)(250)))));
            this.panel.CustomColors.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel.Image = null;
            this.panel.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.panel.Location = new System.Drawing.Point(0, 13);
            this.panel.MinimumSize = new System.Drawing.Size(27, 27);
            this.panel.Moveable = false;
            this.panel.Name = "panel";
            this.panel.NameOfControl = null;
            this.panel.PanelStyle = CIS_CLibrary.PanelStyle.Default;
            this.panel.ShowCaptionbar = false;
            this.panel.ShowTransparentBackground = false;
            this.panel.Size = new System.Drawing.Size(365, 501);
            this.panel.TabIndex = 4;
            this.panel.Text = "Chatting : Send Massage";
            this.panel.ToolTipTextCloseIcon = null;
            this.panel.ToolTipTextExpandIconPanelCollapsed = null;
            this.panel.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // frmChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(365, 527);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.lblBottom);
            this.Controls.Add(this.lblTop);
            this.Controls.Add(this.lblRight);
            this.Controls.Add(this.lblLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmChat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmChat_Load);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CIS_CLibrary.CIS_TextLabel lblLeft;
        private CIS_CLibrary.CIS_TextLabel lblRight;
        private CIS_CLibrary.CIS_TextLabel lblTop;
        private CIS_CLibrary.CIS_TextLabel lblBottom;
        private System.Windows.Forms.RichTextBox txtMessage;
        private CIS_CLibrary.CIS_TextLabel lblUserNM;
        private System.Windows.Forms.RichTextBox txtHistory;
        private System.Windows.Forms.Button btnSentMessage;
        private System.Windows.Forms.Button btnSentFile;
        private System.Windows.Forms.Button btnClearHistory;
        private System.Windows.Forms.Button btnClose;
        private CIS_CLibrary.CIS_Panel panel;
    }
}