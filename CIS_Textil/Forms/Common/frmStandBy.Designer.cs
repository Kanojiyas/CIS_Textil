namespace CIS_Textile

{
    partial class frmStandBy
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
            this.grp_Login = new System.Windows.Forms.GroupBox();
            this.lnkForgerPwd = new System.Windows.Forms.LinkLabel();
            this.lnkDBSettings = new System.Windows.Forms.LinkLabel();
            this.txtPassword = new Crocus_CClibrary.CIS_Textbox();
            this.txtUserName = new Crocus_CClibrary.CIS_Textbox();
            this.lbl_UserName = new Crocus_CClibrary.CIS_TextLabel();
            this.lbl_Password = new Crocus_CClibrary.CIS_TextLabel();
            this.btnLogin = new Crocus_CClibrary.CIS_Button();
            this.btnCancel = new Crocus_CClibrary.CIS_Button();
            this.lblCopyright = new Crocus_CClibrary.CIS_TextLabel();
            this.Label4 = new Crocus_CClibrary.CIS_TextLabel();
            this.grp_Connect = new System.Windows.Forms.GroupBox();
            this.btnInISave = new Crocus_CClibrary.CIS_Button();
            this.btnInICancel = new Crocus_CClibrary.CIS_Button();
            this.txt_DbName = new Crocus_CClibrary.CIS_Textbox();
            this.Label3 = new Crocus_CClibrary.CIS_TextLabel();
            this.txt_ServerName = new Crocus_CClibrary.CIS_Textbox();
            this.Label1 = new Crocus_CClibrary.CIS_TextLabel();
            this.timerFadeOut = new System.Windows.Forms.Timer(this.components);
            this.timerFadeIn = new System.Windows.Forms.Timer(this.components);
            this.GrpForgetPwd = new System.Windows.Forms.GroupBox();
            this.lnkLogin = new System.Windows.Forms.LinkLabel();
            this.txtOtpMobileNo = new Crocus_CClibrary.CIS_Textbox();
            this.txtOtpUserName = new Crocus_CClibrary.CIS_Textbox();
            this.label2 = new Crocus_CClibrary.CIS_TextLabel();
            this.label6 = new Crocus_CClibrary.CIS_TextLabel();
            this.btnSend = new Crocus_CClibrary.CIS_Button();
            this.btnCancelForget = new Crocus_CClibrary.CIS_Button();
            this.grp_Login.SuspendLayout();
            this.grp_Connect.SuspendLayout();
            this.GrpForgetPwd.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_Login
            // 
            this.grp_Login.BackColor = System.Drawing.Color.Transparent;
            this.grp_Login.Controls.Add(this.lnkForgerPwd);
            this.grp_Login.Controls.Add(this.lnkDBSettings);
            this.grp_Login.Controls.Add(this.txtPassword);
            this.grp_Login.Controls.Add(this.txtUserName);
            this.grp_Login.Controls.Add(this.lbl_UserName);
            this.grp_Login.Controls.Add(this.lbl_Password);
            this.grp_Login.Controls.Add(this.btnLogin);
            this.grp_Login.Controls.Add(this.btnCancel);
            this.grp_Login.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold);
            this.grp_Login.Location = new System.Drawing.Point(145, 196);
            this.grp_Login.Name = "grp_Login";
            this.grp_Login.Size = new System.Drawing.Size(321, 170);
            this.grp_Login.TabIndex = 38;
            this.grp_Login.TabStop = false;
            this.grp_Login.Text = "Login";
            // 
            // lnkForgerPwd
            // 
            this.lnkForgerPwd.AutoSize = true;
            this.lnkForgerPwd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkForgerPwd.Location = new System.Drawing.Point(120, 125);
            this.lnkForgerPwd.Name = "lnkForgerPwd";
            this.lnkForgerPwd.Size = new System.Drawing.Size(116, 13);
            this.lnkForgerPwd.TabIndex = 9;
            this.lnkForgerPwd.TabStop = true;
            this.lnkForgerPwd.Text = "Forget Password";
            this.lnkForgerPwd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkForgerPwd_LinkClicked);
            // 
            // lnkDBSettings
            // 
            this.lnkDBSettings.AutoSize = true;
            this.lnkDBSettings.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkDBSettings.Location = new System.Drawing.Point(206, 152);
            this.lnkDBSettings.Name = "lnkDBSettings";
            this.lnkDBSettings.Size = new System.Drawing.Size(112, 13);
            this.lnkDBSettings.TabIndex = 6;
            this.lnkDBSettings.TabStop = true;
            this.lnkDBSettings.Text = "System Settings";
            this.lnkDBSettings.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkDBSettings_LinkClicked);
            // 
            // txtPassword
            // 
            this.txtPassword.AutoFillDate = false;
            this.txtPassword.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtPassword.CCase = Crocus_CClibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtPassword.CheckForSymbol = null;
            this.txtPassword.Control_Type = Crocus_CClibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtPassword.DecimalPlace = 0;
            this.txtPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.HelpText = null;
            this.txtPassword.HoldMyText = null;
            this.txtPassword.IsMandatory = true;
            this.txtPassword.IsSingleQuote = true;
            this.txtPassword.IsSysmbol = false;
            this.txtPassword.Location = new System.Drawing.Point(116, 65);
            this.txtPassword.Mask = null;
            this.txtPassword.MaxLength = 50;
            this.txtPassword.Moveable = false;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.NameOfControl = null;
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Prefix = null;
            this.txtPassword.ShowBallonTip = false;
            this.txtPassword.ShowErrorIcon = false;
            this.txtPassword.ShowMessage = null;
            this.txtPassword.Size = new System.Drawing.Size(143, 20);
            this.txtPassword.Suffix = null;
            this.txtPassword.TabIndex = 2;
            // 
            // txtUserName
            // 
            this.txtUserName.AutoFillDate = false;
            this.txtUserName.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtUserName.CCase = Crocus_CClibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtUserName.CheckForSymbol = null;
            this.txtUserName.Control_Type = Crocus_CClibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtUserName.DecimalPlace = 0;
            this.txtUserName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.HelpText = null;
            this.txtUserName.HoldMyText = null;
            this.txtUserName.IsMandatory = true;
            this.txtUserName.IsSingleQuote = true;
            this.txtUserName.IsSysmbol = false;
            this.txtUserName.Location = new System.Drawing.Point(116, 39);
            this.txtUserName.Mask = null;
            this.txtUserName.MaxLength = 50;
            this.txtUserName.Moveable = false;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.NameOfControl = null;
            this.txtUserName.Prefix = null;
            this.txtUserName.ShowBallonTip = false;
            this.txtUserName.ShowErrorIcon = false;
            this.txtUserName.ShowMessage = null;
            this.txtUserName.Size = new System.Drawing.Size(144, 20);
            this.txtUserName.Suffix = null;
            this.txtUserName.TabIndex = 1;
            // 
            // lbl_UserName
            // 
            this.lbl_UserName.AutoSize = true;
            this.lbl_UserName.BackColor = System.Drawing.Color.Transparent;
            this.lbl_UserName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_UserName.ForeColor = System.Drawing.Color.White;
            this.lbl_UserName.Location = new System.Drawing.Point(11, 43);
            this.lbl_UserName.Name = "lbl_UserName";
            this.lbl_UserName.Size = new System.Drawing.Size(96, 16);
            this.lbl_UserName.TabIndex = 7;
            this.lbl_UserName.Text = "User Name :";
            // 
            // lbl_Password
            // 
            this.lbl_Password.AutoSize = true;
            this.lbl_Password.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Password.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Password.ForeColor = System.Drawing.Color.White;
            this.lbl_Password.Location = new System.Drawing.Point(11, 66);
            this.lbl_Password.Name = "lbl_Password";
            this.lbl_Password.Size = new System.Drawing.Size(95, 16);
            this.lbl_Password.TabIndex = 8;
            this.lbl_Password.Text = "Password   :";
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.btnLogin.GlowColor = System.Drawing.Color.Transparent;
            this.btnLogin.Location = new System.Drawing.Point(116, 92);
            this.btnLogin.Moveable = false;
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(69, 27);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "&Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.GlowColor = System.Drawing.Color.Transparent;
            this.btnCancel.Location = new System.Drawing.Point(190, 92);
            this.btnCancel.Moveable = false;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 27);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyright.Font = new System.Drawing.Font("Verdana", 20F, System.Drawing.FontStyle.Bold);
            this.lblCopyright.ForeColor = System.Drawing.Color.White;
            this.lblCopyright.Location = new System.Drawing.Point(139, 9);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(446, 32);
            this.lblCopyright.TabIndex = 37;
            this.lblCopyright.Text = "Crocus IT Solutions Pvt. Ltd.";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Font = new System.Drawing.Font("Verdana", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.ForeColor = System.Drawing.Color.White;
            this.Label4.Location = new System.Drawing.Point(387, 702);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(326, 25);
            this.Label4.TabIndex = 36;
            this.Label4.Text = "www.crocusitsolutions.com";
            // 
            // grp_Connect
            // 
            this.grp_Connect.BackColor = System.Drawing.Color.Transparent;
            this.grp_Connect.Controls.Add(this.btnInISave);
            this.grp_Connect.Controls.Add(this.btnInICancel);
            this.grp_Connect.Controls.Add(this.txt_DbName);
            this.grp_Connect.Controls.Add(this.Label3);
            this.grp_Connect.Controls.Add(this.txt_ServerName);
            this.grp_Connect.Controls.Add(this.Label1);
            this.grp_Connect.Enabled = false;
            this.grp_Connect.Font = new System.Drawing.Font("Verdana", 20.25F, System.Drawing.FontStyle.Bold);
            this.grp_Connect.Location = new System.Drawing.Point(145, 214);
            this.grp_Connect.Name = "grp_Connect";
            this.grp_Connect.Size = new System.Drawing.Size(303, 152);
            this.grp_Connect.TabIndex = 34;
            this.grp_Connect.TabStop = false;
            this.grp_Connect.Text = "Connection";
            this.grp_Connect.Visible = false;
            // 
            // btnInISave
            // 
            this.btnInISave.BackColor = System.Drawing.Color.Transparent;
            this.btnInISave.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.btnInISave.GlowColor = System.Drawing.Color.Transparent;
            this.btnInISave.Location = new System.Drawing.Point(125, 100);
            this.btnInISave.Moveable = false;
            this.btnInISave.Name = "btnInISave";
            this.btnInISave.Size = new System.Drawing.Size(69, 27);
            this.btnInISave.TabIndex = 6;
            this.btnInISave.Text = "&Save";
            this.btnInISave.UseVisualStyleBackColor = false;
            // 
            // btnInICancel
            // 
            this.btnInICancel.BackColor = System.Drawing.Color.Transparent;
            this.btnInICancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.btnInICancel.GlowColor = System.Drawing.Color.Transparent;
            this.btnInICancel.Location = new System.Drawing.Point(198, 100);
            this.btnInICancel.Moveable = false;
            this.btnInICancel.Name = "btnInICancel";
            this.btnInICancel.Size = new System.Drawing.Size(69, 27);
            this.btnInICancel.TabIndex = 7;
            this.btnInICancel.Text = "&Cancel";
            this.btnInICancel.UseVisualStyleBackColor = false;
            // 
            // txt_DbName
            // 
            this.txt_DbName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DbName.Location = new System.Drawing.Point(97, 73);
            this.txt_DbName.Name = "txt_DbName";
            this.txt_DbName.Size = new System.Drawing.Size(170, 21);
            this.txt_DbName.TabIndex = 5;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.Color.White;
            this.Label3.Location = new System.Drawing.Point(5, 47);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(65, 16);
            this.Label3.TabIndex = 11;
            this.Label3.Text = "Server :";
            // 
            // txt_ServerName
            // 
            this.txt_ServerName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ServerName.Location = new System.Drawing.Point(97, 45);
            this.txt_ServerName.Name = "txt_ServerName";
            this.txt_ServerName.Size = new System.Drawing.Size(170, 21);
            this.txt_ServerName.TabIndex = 4;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.Color.White;
            this.Label1.Location = new System.Drawing.Point(5, 75);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(86, 16);
            this.Label1.TabIndex = 12;
            this.Label1.Text = "Database :";
            // 
            // timerFadeOut
            // 
            this.timerFadeOut.Interval = 10;
            this.timerFadeOut.Tick += new System.EventHandler(this.timerFadeOut_Tick);
            // 
            // timerFadeIn
            // 
            this.timerFadeIn.Interval = 20;
            this.timerFadeIn.Tick += new System.EventHandler(this.timerFadeIn_Tick);
            // 
            // GrpForgetPwd
            // 
            this.GrpForgetPwd.BackColor = System.Drawing.Color.Transparent;
            this.GrpForgetPwd.Controls.Add(this.lnkLogin);
            this.GrpForgetPwd.Controls.Add(this.txtOtpMobileNo);
            this.GrpForgetPwd.Controls.Add(this.txtOtpUserName);
            this.GrpForgetPwd.Controls.Add(this.label2);
            this.GrpForgetPwd.Controls.Add(this.label6);
            this.GrpForgetPwd.Controls.Add(this.btnSend);
            this.GrpForgetPwd.Controls.Add(this.btnCancelForget);
            this.GrpForgetPwd.Font = new System.Drawing.Font("Verdana", 15.25F, System.Drawing.FontStyle.Bold);
            this.GrpForgetPwd.Location = new System.Drawing.Point(145, 196);
            this.GrpForgetPwd.Name = "GrpForgetPwd";
            this.GrpForgetPwd.Size = new System.Drawing.Size(321, 170);
            this.GrpForgetPwd.TabIndex = 40;
            this.GrpForgetPwd.TabStop = false;
            this.GrpForgetPwd.Text = "Forgot Password";
            // 
            // lnkLogin
            // 
            this.lnkLogin.AutoSize = true;
            this.lnkLogin.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold);
            this.lnkLogin.Location = new System.Drawing.Point(117, 125);
            this.lnkLogin.Name = "lnkLogin";
            this.lnkLogin.Size = new System.Drawing.Size(102, 17);
            this.lnkLogin.TabIndex = 9;
            this.lnkLogin.TabStop = true;
            this.lnkLogin.Text = "Go To Login";
            this.lnkLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLogin_LinkClicked);
            // 
            // txtOtpMobileNo
            // 
            this.txtOtpMobileNo.AutoFillDate = false;
            this.txtOtpMobileNo.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtOtpMobileNo.CCase = Crocus_CClibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtOtpMobileNo.CheckForSymbol = null;
            this.txtOtpMobileNo.Control_Type = Crocus_CClibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtOtpMobileNo.DecimalPlace = 0;
            this.txtOtpMobileNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtpMobileNo.HelpText = null;
            this.txtOtpMobileNo.HoldMyText = null;
            this.txtOtpMobileNo.IsMandatory = true;
            this.txtOtpMobileNo.IsSingleQuote = true;
            this.txtOtpMobileNo.IsSysmbol = false;
            this.txtOtpMobileNo.Location = new System.Drawing.Point(116, 69);
            this.txtOtpMobileNo.Mask = null;
            this.txtOtpMobileNo.MaxLength = 50;
            this.txtOtpMobileNo.Moveable = false;
            this.txtOtpMobileNo.Name = "txtOtpMobileNo";
            this.txtOtpMobileNo.NameOfControl = null;
            this.txtOtpMobileNo.PasswordChar = '*';
            this.txtOtpMobileNo.Prefix = null;
            this.txtOtpMobileNo.ShowBallonTip = false;
            this.txtOtpMobileNo.ShowErrorIcon = false;
            this.txtOtpMobileNo.ShowMessage = null;
            this.txtOtpMobileNo.Size = new System.Drawing.Size(144, 20);
            this.txtOtpMobileNo.Suffix = null;
            this.txtOtpMobileNo.TabIndex = 2;
            // 
            // txtOtpUserName
            // 
            this.txtOtpUserName.AutoFillDate = false;
            this.txtOtpUserName.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtOtpUserName.CCase = Crocus_CClibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtOtpUserName.CheckForSymbol = null;
            this.txtOtpUserName.Control_Type = Crocus_CClibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtOtpUserName.DecimalPlace = 0;
            this.txtOtpUserName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOtpUserName.HelpText = null;
            this.txtOtpUserName.HoldMyText = null;
            this.txtOtpUserName.IsMandatory = true;
            this.txtOtpUserName.IsSingleQuote = true;
            this.txtOtpUserName.IsSysmbol = false;
            this.txtOtpUserName.Location = new System.Drawing.Point(116, 43);
            this.txtOtpUserName.Mask = null;
            this.txtOtpUserName.MaxLength = 50;
            this.txtOtpUserName.Moveable = false;
            this.txtOtpUserName.Name = "txtOtpUserName";
            this.txtOtpUserName.NameOfControl = null;
            this.txtOtpUserName.Prefix = null;
            this.txtOtpUserName.ShowBallonTip = false;
            this.txtOtpUserName.ShowErrorIcon = false;
            this.txtOtpUserName.ShowMessage = null;
            this.txtOtpUserName.Size = new System.Drawing.Size(144, 20);
            this.txtOtpUserName.Suffix = null;
            this.txtOtpUserName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(11, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "User Name :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(11, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 16);
            this.label6.TabIndex = 8;
            this.label6.Text = "Mobile No   :";
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.Transparent;
            this.btnSend.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.btnSend.GlowColor = System.Drawing.Color.Transparent;
            this.btnSend.Location = new System.Drawing.Point(116, 96);
            this.btnSend.Moveable = false;
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(69, 27);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "&Send";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnCancelForget
            // 
            this.btnCancelForget.BackColor = System.Drawing.Color.Transparent;
            this.btnCancelForget.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelForget.GlowColor = System.Drawing.Color.Transparent;
            this.btnCancelForget.Location = new System.Drawing.Point(190, 96);
            this.btnCancelForget.Moveable = false;
            this.btnCancelForget.Name = "btnCancelForget";
            this.btnCancelForget.Size = new System.Drawing.Size(69, 27);
            this.btnCancelForget.TabIndex = 5;
            this.btnCancelForget.Text = "&Cancel";
            this.btnCancelForget.UseVisualStyleBackColor = false;
            this.btnCancelForget.Click += new System.EventHandler(this.btnCancelForget_Click);
            // 
            // frmStandBy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.BackgroundImage = global::Crocus_Core.Properties.Resources.Crocus_template;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1376, 778);
            this.ControlBox = false;
            this.Controls.Add(this.grp_Login);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.grp_Connect);
            this.Controls.Add(this.GrpForgetPwd);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmStandBy";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStandBy_FormClosing);
            this.Load += new System.EventHandler(this.frmStandBy_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmStandBy_KeyUp);
            this.grp_Login.ResumeLayout(false);
            this.grp_Login.PerformLayout();
            this.grp_Connect.ResumeLayout(false);
            this.grp_Connect.PerformLayout();
            this.GrpForgetPwd.ResumeLayout(false);
            this.GrpForgetPwd.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.GroupBox grp_Login;
        internal Crocus_CClibrary.CIS_TextLabel lbl_UserName;
        internal Crocus_CClibrary.CIS_TextLabel lbl_Password;
        internal Crocus_CClibrary.CIS_Button btnLogin;
        internal Crocus_CClibrary.CIS_Button btnCancel;
        internal Crocus_CClibrary.CIS_TextLabel lblCopyright;
        internal Crocus_CClibrary.CIS_TextLabel Label4;
        internal System.Windows.Forms.GroupBox grp_Connect;
        internal Crocus_CClibrary.CIS_Button btnInISave;
        internal Crocus_CClibrary.CIS_Button btnInICancel;
        internal Crocus_CClibrary.CIS_Textbox txt_DbName;
        internal Crocus_CClibrary.CIS_TextLabel Label3;
        internal Crocus_CClibrary.CIS_Textbox txt_ServerName;
        internal Crocus_CClibrary.CIS_TextLabel Label1;
        private System.Windows.Forms.Timer timerFadeOut;
        private System.Windows.Forms.Timer timerFadeIn;
        internal Crocus_CClibrary.CIS_Textbox txtPassword;
        internal Crocus_CClibrary.CIS_Textbox txtUserName;
        internal System.Windows.Forms.LinkLabel lnkDBSettings;
        internal System.Windows.Forms.LinkLabel lnkForgerPwd;
        internal System.Windows.Forms.GroupBox GrpForgetPwd;
        internal Crocus_CClibrary.CIS_Textbox txtOtpMobileNo;
        internal Crocus_CClibrary.CIS_Textbox txtOtpUserName;
        internal Crocus_CClibrary.CIS_TextLabel label2;
        internal Crocus_CClibrary.CIS_TextLabel label6;
        internal Crocus_CClibrary.CIS_Button btnSend;
        internal Crocus_CClibrary.CIS_Button btnCancelForget;
        private System.Windows.Forms.LinkLabel lnkLogin;
    }
}