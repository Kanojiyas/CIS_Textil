namespace CIS_Textil
{
    partial class frmDBSettings
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
            this.lblversion = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.OpenFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.grp_DBSetting = new System.Windows.Forms.GroupBox();
            this.lblDOT1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblDOT3 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblDOT2 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblDOT4 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblDOT5 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtServerName = new CIS_CLibrary.CIS_Textbox();
            this.lblServerName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.rdbLocal = new CIS_CLibrary.CIS_RadioButton();
            this.btnSave = new CIS_CLibrary.CIS_Button();
            this.btnCancel = new CIS_CLibrary.CIS_Button();
            this.rdbLAN = new CIS_CLibrary.CIS_RadioButton();
            this.rdbOnline = new CIS_CLibrary.CIS_RadioButton();
            this.txtDBName = new CIS_CLibrary.CIS_Textbox();
            this.lblDBName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtSQLServerNM = new CIS_CLibrary.CIS_Textbox();
            this.lblSQLServerName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtUserName = new CIS_CLibrary.CIS_Textbox();
            this.txtPassword = new CIS_CLibrary.CIS_Textbox();
            this.lbl_UserName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lbl_Password = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.pnlAttachDB = new System.Windows.Forms.Panel();
            this.btnLdf = new CIS_CLibrary.CIS_Button();
            this.btnMdf = new CIS_CLibrary.CIS_Button();
            this.txtLdf = new CIS_CLibrary.CIS_Textbox();
            this.txtMdf = new CIS_CLibrary.CIS_Textbox();
            this.btnAttachDB = new CIS_CLibrary.CIS_Button();
            this.grp_DBSetting.SuspendLayout();
            this.pnlAttachDB.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblversion
            // 
            this.lblversion.AutoSize = true;
            this.lblversion.BackColor = System.Drawing.Color.Transparent;
            this.lblversion.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblversion.ForeColor = System.Drawing.Color.White;
            this.lblversion.Location = new System.Drawing.Point(374, 329);
            this.lblversion.Moveable = false;
            this.lblversion.Name = "lblversion";
            this.lblversion.NameOfControl = null;
            this.lblversion.Size = new System.Drawing.Size(59, 13);
            this.lblversion.TabIndex = 13;
            this.lblversion.Text = "Version :";
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
            // grp_DBSetting
            // 
            this.grp_DBSetting.BackColor = System.Drawing.Color.Transparent;
            this.grp_DBSetting.Controls.Add(this.lblDOT1);
            this.grp_DBSetting.Controls.Add(this.lblDOT3);
            this.grp_DBSetting.Controls.Add(this.lblDOT2);
            this.grp_DBSetting.Controls.Add(this.lblDOT4);
            this.grp_DBSetting.Controls.Add(this.lblDOT5);
            this.grp_DBSetting.Controls.Add(this.txtServerName);
            this.grp_DBSetting.Controls.Add(this.lblServerName);
            this.grp_DBSetting.Controls.Add(this.rdbLocal);
            this.grp_DBSetting.Controls.Add(this.btnSave);
            this.grp_DBSetting.Controls.Add(this.btnCancel);
            this.grp_DBSetting.Controls.Add(this.rdbLAN);
            this.grp_DBSetting.Controls.Add(this.rdbOnline);
            this.grp_DBSetting.Controls.Add(this.txtDBName);
            this.grp_DBSetting.Controls.Add(this.lblDBName);
            this.grp_DBSetting.Controls.Add(this.txtSQLServerNM);
            this.grp_DBSetting.Controls.Add(this.lblSQLServerName);
            this.grp_DBSetting.Controls.Add(this.txtUserName);
            this.grp_DBSetting.Controls.Add(this.txtPassword);
            this.grp_DBSetting.Controls.Add(this.lbl_UserName);
            this.grp_DBSetting.Controls.Add(this.lbl_Password);
            this.grp_DBSetting.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_DBSetting.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grp_DBSetting.Location = new System.Drawing.Point(83, 117);
            this.grp_DBSetting.Name = "grp_DBSetting";
            this.grp_DBSetting.Size = new System.Drawing.Size(402, 212);
            this.grp_DBSetting.TabIndex = 22;
            this.grp_DBSetting.TabStop = false;
            this.grp_DBSetting.Text = "Database Settings";
            // 
            // lblDOT1
            // 
            this.lblDOT1.AutoSize = true;
            this.lblDOT1.BackColor = System.Drawing.Color.Transparent;
            this.lblDOT1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOT1.ForeColor = System.Drawing.Color.White;
            this.lblDOT1.Location = new System.Drawing.Point(162, 56);
            this.lblDOT1.Moveable = false;
            this.lblDOT1.Name = "lblDOT1";
            this.lblDOT1.NameOfControl = "ServerName";
            this.lblDOT1.Size = new System.Drawing.Size(13, 16);
            this.lblDOT1.TabIndex = 5;
            this.lblDOT1.Text = ":";
            // 
            // lblDOT3
            // 
            this.lblDOT3.AutoSize = true;
            this.lblDOT3.BackColor = System.Drawing.Color.Transparent;
            this.lblDOT3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOT3.ForeColor = System.Drawing.Color.White;
            this.lblDOT3.Location = new System.Drawing.Point(162, 102);
            this.lblDOT3.Moveable = false;
            this.lblDOT3.Name = "lblDOT3";
            this.lblDOT3.NameOfControl = "DBName";
            this.lblDOT3.Size = new System.Drawing.Size(13, 16);
            this.lblDOT3.TabIndex = 11;
            this.lblDOT3.Text = ":";
            // 
            // lblDOT2
            // 
            this.lblDOT2.AutoSize = true;
            this.lblDOT2.BackColor = System.Drawing.Color.Transparent;
            this.lblDOT2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOT2.ForeColor = System.Drawing.Color.White;
            this.lblDOT2.Location = new System.Drawing.Point(162, 79);
            this.lblDOT2.Moveable = false;
            this.lblDOT2.Name = "lblDOT2";
            this.lblDOT2.NameOfControl = "ServerName";
            this.lblDOT2.Size = new System.Drawing.Size(13, 16);
            this.lblDOT2.TabIndex = 8;
            this.lblDOT2.Text = ":";
            // 
            // lblDOT4
            // 
            this.lblDOT4.AutoSize = true;
            this.lblDOT4.BackColor = System.Drawing.Color.Transparent;
            this.lblDOT4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOT4.ForeColor = System.Drawing.Color.White;
            this.lblDOT4.Location = new System.Drawing.Point(162, 125);
            this.lblDOT4.Moveable = false;
            this.lblDOT4.Name = "lblDOT4";
            this.lblDOT4.NameOfControl = null;
            this.lblDOT4.Size = new System.Drawing.Size(13, 16);
            this.lblDOT4.TabIndex = 14;
            this.lblDOT4.Text = ":";
            // 
            // lblDOT5
            // 
            this.lblDOT5.AutoSize = true;
            this.lblDOT5.BackColor = System.Drawing.Color.Transparent;
            this.lblDOT5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDOT5.ForeColor = System.Drawing.Color.White;
            this.lblDOT5.Location = new System.Drawing.Point(162, 148);
            this.lblDOT5.Moveable = false;
            this.lblDOT5.Name = "lblDOT5";
            this.lblDOT5.NameOfControl = null;
            this.lblDOT5.Size = new System.Drawing.Size(13, 16);
            this.lblDOT5.TabIndex = 17;
            this.lblDOT5.Text = ":";
            // 
            // txtServerName
            // 
            this.txtServerName.AutoFillDate = false;
            this.txtServerName.BackColor = System.Drawing.Color.White;
            this.txtServerName.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtServerName.CheckForSymbol = null;
            this.txtServerName.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtServerName.DecimalPlace = 0;
            this.txtServerName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServerName.HelpText = "";
            this.txtServerName.HoldMyText = null;
            this.txtServerName.IsMandatory = false;
            this.txtServerName.IsSingleQuote = true;
            this.txtServerName.IsSysmbol = false;
            this.txtServerName.Location = new System.Drawing.Point(176, 56);
            this.txtServerName.Mask = null;
            this.txtServerName.Moveable = false;
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.NameOfControl = null;
            this.txtServerName.Prefix = null;
            this.txtServerName.ShowBallonTip = false;
            this.txtServerName.ShowErrorIcon = false;
            this.txtServerName.ShowMessage = null;
            this.txtServerName.Size = new System.Drawing.Size(212, 21);
            this.txtServerName.Suffix = null;
            this.txtServerName.TabIndex = 6;
            // 
            // lblServerName
            // 
            this.lblServerName.AutoSize = true;
            this.lblServerName.BackColor = System.Drawing.Color.Transparent;
            this.lblServerName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblServerName.ForeColor = System.Drawing.Color.White;
            this.lblServerName.Location = new System.Drawing.Point(25, 58);
            this.lblServerName.Moveable = false;
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.NameOfControl = "ServerName";
            this.lblServerName.Size = new System.Drawing.Size(102, 16);
            this.lblServerName.TabIndex = 4;
            this.lblServerName.Text = "Server Name";
            // 
            // rdbLocal
            // 
            this.rdbLocal.AutoSize = true;
            this.rdbLocal.Checked = true;
            this.rdbLocal.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbLocal.ForeColor = System.Drawing.Color.White;
            this.rdbLocal.HelpText = null;
            this.rdbLocal.Location = new System.Drawing.Point(61, 24);
            this.rdbLocal.Moveable = false;
            this.rdbLocal.Name = "rdbLocal";
            this.rdbLocal.Size = new System.Drawing.Size(64, 20);
            this.rdbLocal.TabIndex = 1;
            this.rdbLocal.TabStop = true;
            this.rdbLocal.Text = "Local";
            this.rdbLocal.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.CadetBlue;
            this.btnSave.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.btnSave.GlowColor = System.Drawing.Color.Transparent;
            this.btnSave.Location = new System.Drawing.Point(178, 179);
            this.btnSave.Moveable = false;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(84, 26);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.CadetBlue;
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancel.GlowColor = System.Drawing.Color.Transparent;
            this.btnCancel.Location = new System.Drawing.Point(271, 179);
            this.btnCancel.Moveable = false;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(84, 26);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // rdbLAN
            // 
            this.rdbLAN.AutoSize = true;
            this.rdbLAN.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbLAN.ForeColor = System.Drawing.Color.White;
            this.rdbLAN.HelpText = null;
            this.rdbLAN.Location = new System.Drawing.Point(156, 24);
            this.rdbLAN.Moveable = false;
            this.rdbLAN.Name = "rdbLAN";
            this.rdbLAN.Size = new System.Drawing.Size(86, 20);
            this.rdbLAN.TabIndex = 2;
            this.rdbLAN.Text = "Network";
            this.rdbLAN.UseVisualStyleBackColor = true;
            // 
            // rdbOnline
            // 
            this.rdbOnline.AutoSize = true;
            this.rdbOnline.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbOnline.ForeColor = System.Drawing.Color.White;
            this.rdbOnline.HelpText = null;
            this.rdbOnline.Location = new System.Drawing.Point(276, 24);
            this.rdbOnline.Moveable = false;
            this.rdbOnline.Name = "rdbOnline";
            this.rdbOnline.Size = new System.Drawing.Size(72, 20);
            this.rdbOnline.TabIndex = 3;
            this.rdbOnline.Text = "Online";
            this.rdbOnline.UseVisualStyleBackColor = true;
            // 
            // txtDBName
            // 
            this.txtDBName.AutoFillDate = false;
            this.txtDBName.BackColor = System.Drawing.Color.White;
            this.txtDBName.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtDBName.CheckForSymbol = null;
            this.txtDBName.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtDBName.DecimalPlace = 0;
            this.txtDBName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDBName.HelpText = "";
            this.txtDBName.HoldMyText = null;
            this.txtDBName.IsMandatory = false;
            this.txtDBName.IsSingleQuote = true;
            this.txtDBName.IsSysmbol = false;
            this.txtDBName.Location = new System.Drawing.Point(176, 102);
            this.txtDBName.Mask = null;
            this.txtDBName.Moveable = false;
            this.txtDBName.Name = "txtDBName";
            this.txtDBName.NameOfControl = null;
            this.txtDBName.Prefix = null;
            this.txtDBName.ShowBallonTip = false;
            this.txtDBName.ShowErrorIcon = false;
            this.txtDBName.ShowMessage = null;
            this.txtDBName.Size = new System.Drawing.Size(212, 21);
            this.txtDBName.Suffix = null;
            this.txtDBName.TabIndex = 12;
            // 
            // lblDBName
            // 
            this.lblDBName.AutoSize = true;
            this.lblDBName.BackColor = System.Drawing.Color.Transparent;
            this.lblDBName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDBName.ForeColor = System.Drawing.Color.White;
            this.lblDBName.Location = new System.Drawing.Point(25, 104);
            this.lblDBName.Moveable = false;
            this.lblDBName.Name = "lblDBName";
            this.lblDBName.NameOfControl = "DBName";
            this.lblDBName.Size = new System.Drawing.Size(123, 16);
            this.lblDBName.TabIndex = 10;
            this.lblDBName.Text = "Database Name";
            // 
            // txtSQLServerNM
            // 
            this.txtSQLServerNM.AutoFillDate = false;
            this.txtSQLServerNM.BackColor = System.Drawing.Color.White;
            this.txtSQLServerNM.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtSQLServerNM.CheckForSymbol = null;
            this.txtSQLServerNM.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtSQLServerNM.DecimalPlace = 0;
            this.txtSQLServerNM.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSQLServerNM.HelpText = "";
            this.txtSQLServerNM.HoldMyText = null;
            this.txtSQLServerNM.IsMandatory = false;
            this.txtSQLServerNM.IsSingleQuote = true;
            this.txtSQLServerNM.IsSysmbol = false;
            this.txtSQLServerNM.Location = new System.Drawing.Point(176, 79);
            this.txtSQLServerNM.Mask = null;
            this.txtSQLServerNM.Moveable = false;
            this.txtSQLServerNM.Name = "txtSQLServerNM";
            this.txtSQLServerNM.NameOfControl = null;
            this.txtSQLServerNM.Prefix = null;
            this.txtSQLServerNM.ShowBallonTip = false;
            this.txtSQLServerNM.ShowErrorIcon = false;
            this.txtSQLServerNM.ShowMessage = null;
            this.txtSQLServerNM.Size = new System.Drawing.Size(212, 21);
            this.txtSQLServerNM.Suffix = null;
            this.txtSQLServerNM.TabIndex = 9;
            // 
            // lblSQLServerName
            // 
            this.lblSQLServerName.AutoSize = true;
            this.lblSQLServerName.BackColor = System.Drawing.Color.Transparent;
            this.lblSQLServerName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSQLServerName.ForeColor = System.Drawing.Color.White;
            this.lblSQLServerName.Location = new System.Drawing.Point(25, 81);
            this.lblSQLServerName.Moveable = false;
            this.lblSQLServerName.Name = "lblSQLServerName";
            this.lblSQLServerName.NameOfControl = "ServerName";
            this.lblSQLServerName.Size = new System.Drawing.Size(134, 16);
            this.lblSQLServerName.TabIndex = 7;
            this.lblSQLServerName.Text = "SQL Server Name";
            // 
            // txtUserName
            // 
            this.txtUserName.AutoFillDate = false;
            this.txtUserName.BackColor = System.Drawing.Color.White;
            this.txtUserName.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtUserName.CheckForSymbol = null;
            this.txtUserName.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtUserName.DecimalPlace = 0;
            this.txtUserName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.HelpText = "";
            this.txtUserName.HoldMyText = null;
            this.txtUserName.IsMandatory = false;
            this.txtUserName.IsSingleQuote = true;
            this.txtUserName.IsSysmbol = false;
            this.txtUserName.Location = new System.Drawing.Point(176, 125);
            this.txtUserName.Mask = null;
            this.txtUserName.Moveable = false;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.NameOfControl = null;
            this.txtUserName.Prefix = null;
            this.txtUserName.ShowBallonTip = false;
            this.txtUserName.ShowErrorIcon = false;
            this.txtUserName.ShowMessage = null;
            this.txtUserName.Size = new System.Drawing.Size(212, 21);
            this.txtUserName.Suffix = null;
            this.txtUserName.TabIndex = 15;
            // 
            // txtPassword
            // 
            this.txtPassword.AutoFillDate = false;
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtPassword.CheckForSymbol = null;
            this.txtPassword.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtPassword.DecimalPlace = 0;
            this.txtPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.HelpText = "";
            this.txtPassword.HoldMyText = null;
            this.txtPassword.IsMandatory = false;
            this.txtPassword.IsSingleQuote = true;
            this.txtPassword.IsSysmbol = false;
            this.txtPassword.Location = new System.Drawing.Point(176, 148);
            this.txtPassword.Mask = null;
            this.txtPassword.Moveable = false;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.NameOfControl = null;
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Prefix = null;
            this.txtPassword.ShowBallonTip = false;
            this.txtPassword.ShowErrorIcon = false;
            this.txtPassword.ShowMessage = null;
            this.txtPassword.Size = new System.Drawing.Size(212, 22);
            this.txtPassword.Suffix = null;
            this.txtPassword.TabIndex = 18;
            // 
            // lbl_UserName
            // 
            this.lbl_UserName.AutoSize = true;
            this.lbl_UserName.BackColor = System.Drawing.Color.Transparent;
            this.lbl_UserName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_UserName.ForeColor = System.Drawing.Color.White;
            this.lbl_UserName.Location = new System.Drawing.Point(25, 127);
            this.lbl_UserName.Moveable = false;
            this.lbl_UserName.Name = "lbl_UserName";
            this.lbl_UserName.NameOfControl = null;
            this.lbl_UserName.Size = new System.Drawing.Size(87, 16);
            this.lbl_UserName.TabIndex = 13;
            this.lbl_UserName.Text = "User Name";
            // 
            // lbl_Password
            // 
            this.lbl_Password.AutoSize = true;
            this.lbl_Password.BackColor = System.Drawing.Color.Transparent;
            this.lbl_Password.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Password.ForeColor = System.Drawing.Color.White;
            this.lbl_Password.Location = new System.Drawing.Point(25, 150);
            this.lbl_Password.Moveable = false;
            this.lbl_Password.Name = "lbl_Password";
            this.lbl_Password.NameOfControl = null;
            this.lbl_Password.Size = new System.Drawing.Size(78, 16);
            this.lbl_Password.TabIndex = 16;
            this.lbl_Password.Text = "Password";
            // 
            // pnlAttachDB
            // 
            this.pnlAttachDB.BackColor = System.Drawing.Color.Transparent;
            this.pnlAttachDB.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlAttachDB.Controls.Add(this.btnLdf);
            this.pnlAttachDB.Controls.Add(this.btnMdf);
            this.pnlAttachDB.Controls.Add(this.txtLdf);
            this.pnlAttachDB.Controls.Add(this.txtMdf);
            this.pnlAttachDB.Controls.Add(this.btnAttachDB);
            this.pnlAttachDB.Location = new System.Drawing.Point(43, 153);
            this.pnlAttachDB.Name = "pnlAttachDB";
            this.pnlAttachDB.Size = new System.Drawing.Size(482, 118);
            this.pnlAttachDB.TabIndex = 23;
            // 
            // btnLdf
            // 
            this.btnLdf.BackColor = System.Drawing.Color.CadetBlue;
            this.btnLdf.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLdf.Location = new System.Drawing.Point(423, 42);
            this.btnLdf.Moveable = false;
            this.btnLdf.Name = "btnLdf";
            this.btnLdf.Size = new System.Drawing.Size(46, 21);
            this.btnLdf.TabIndex = 25;
            this.btnLdf.Text = "LDF";
            this.btnLdf.UseVisualStyleBackColor = false;
            this.btnLdf.Click += new System.EventHandler(this.btnLdf_Click);
            // 
            // btnMdf
            // 
            this.btnMdf.BackColor = System.Drawing.Color.CadetBlue;
            this.btnMdf.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMdf.Location = new System.Drawing.Point(423, 18);
            this.btnMdf.Moveable = false;
            this.btnMdf.Name = "btnMdf";
            this.btnMdf.Size = new System.Drawing.Size(46, 21);
            this.btnMdf.TabIndex = 23;
            this.btnMdf.Text = "MDF";
            this.btnMdf.UseVisualStyleBackColor = false;
            this.btnMdf.Click += new System.EventHandler(this.btnMdf_Click);
            // 
            // txtLdf
            // 
            this.txtLdf.AutoFillDate = false;
            this.txtLdf.BackColor = System.Drawing.Color.White;
            this.txtLdf.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtLdf.CheckForSymbol = null;
            this.txtLdf.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtLdf.DecimalPlace = 0;
            this.txtLdf.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLdf.HelpText = "";
            this.txtLdf.HoldMyText = null;
            this.txtLdf.IsMandatory = false;
            this.txtLdf.IsSingleQuote = true;
            this.txtLdf.IsSysmbol = false;
            this.txtLdf.Location = new System.Drawing.Point(8, 42);
            this.txtLdf.Mask = null;
            this.txtLdf.Moveable = false;
            this.txtLdf.Name = "txtLdf";
            this.txtLdf.NameOfControl = null;
            this.txtLdf.Prefix = null;
            this.txtLdf.ShowBallonTip = false;
            this.txtLdf.ShowErrorIcon = false;
            this.txtLdf.ShowMessage = null;
            this.txtLdf.Size = new System.Drawing.Size(409, 21);
            this.txtLdf.Suffix = null;
            this.txtLdf.TabIndex = 24;
            // 
            // txtMdf
            // 
            this.txtMdf.AutoFillDate = false;
            this.txtMdf.BackColor = System.Drawing.Color.White;
            this.txtMdf.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtMdf.CheckForSymbol = null;
            this.txtMdf.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtMdf.DecimalPlace = 0;
            this.txtMdf.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMdf.HelpText = "";
            this.txtMdf.HoldMyText = null;
            this.txtMdf.IsMandatory = false;
            this.txtMdf.IsSingleQuote = true;
            this.txtMdf.IsSysmbol = false;
            this.txtMdf.Location = new System.Drawing.Point(8, 18);
            this.txtMdf.Mask = null;
            this.txtMdf.Moveable = false;
            this.txtMdf.Name = "txtMdf";
            this.txtMdf.NameOfControl = null;
            this.txtMdf.Prefix = null;
            this.txtMdf.ShowBallonTip = false;
            this.txtMdf.ShowErrorIcon = false;
            this.txtMdf.ShowMessage = null;
            this.txtMdf.Size = new System.Drawing.Size(409, 21);
            this.txtMdf.Suffix = null;
            this.txtMdf.TabIndex = 22;
            // 
            // btnAttachDB
            // 
            this.btnAttachDB.BackColor = System.Drawing.Color.CadetBlue;
            this.btnAttachDB.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttachDB.Location = new System.Drawing.Point(174, 68);
            this.btnAttachDB.Moveable = false;
            this.btnAttachDB.Name = "btnAttachDB";
            this.btnAttachDB.Size = new System.Drawing.Size(132, 27);
            this.btnAttachDB.TabIndex = 26;
            this.btnAttachDB.Text = "Attach Database";
            this.btnAttachDB.UseVisualStyleBackColor = false;
            this.btnAttachDB.Click += new System.EventHandler(this.btnAttachDB_Click);
            // 
            // frmDBSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::CIS_Textil.Properties.Resources.Intro;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(568, 425);
            this.Controls.Add(this.pnlAttachDB);
            this.Controls.Add(this.grp_DBSetting);
            this.Controls.Add(this.lblversion);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDBSettings";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmDBSettings_Load);
            this.grp_DBSetting.ResumeLayout(false);
            this.grp_DBSetting.PerformLayout();
            this.pnlAttachDB.ResumeLayout(false);
            this.pnlAttachDB.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal CIS_CLibrary.CIS_TextLabel lblversion;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog2;
        internal CIS_CLibrary.CIS_TextLabel lblDOT1;
        internal CIS_CLibrary.CIS_TextLabel lblDOT3;
        internal CIS_CLibrary.CIS_TextLabel lblDOT2;
        internal CIS_CLibrary.CIS_TextLabel lblDOT4;
        internal CIS_CLibrary.CIS_TextLabel lblDOT5;
        internal CIS_CLibrary.CIS_Textbox txtServerName;
        internal CIS_CLibrary.CIS_TextLabel lblServerName;
        internal CIS_CLibrary.CIS_RadioButton rdbLocal;
        internal CIS_CLibrary.CIS_Button btnSave;
        internal CIS_CLibrary.CIS_Button btnCancel;
        internal CIS_CLibrary.CIS_RadioButton rdbLAN;
        internal CIS_CLibrary.CIS_RadioButton rdbOnline;
        internal CIS_CLibrary.CIS_Textbox txtDBName;
        internal CIS_CLibrary.CIS_TextLabel lblDBName;
        internal CIS_CLibrary.CIS_Textbox txtSQLServerNM;
        internal CIS_CLibrary.CIS_TextLabel lblSQLServerName;
        internal CIS_CLibrary.CIS_Textbox txtUserName;
        internal CIS_CLibrary.CIS_Textbox txtPassword;
        internal CIS_CLibrary.CIS_TextLabel lbl_UserName;
        internal CIS_CLibrary.CIS_TextLabel lbl_Password;
        internal CIS_CLibrary.CIS_Button btnLdf;
        internal CIS_CLibrary.CIS_Button btnMdf;
        internal CIS_CLibrary.CIS_Textbox txtLdf;
        internal CIS_CLibrary.CIS_Textbox txtMdf;
        internal CIS_CLibrary.CIS_Button btnAttachDB;
        public System.Windows.Forms.Panel pnlAttachDB;
        public System.Windows.Forms.GroupBox grp_DBSetting;
    }
}