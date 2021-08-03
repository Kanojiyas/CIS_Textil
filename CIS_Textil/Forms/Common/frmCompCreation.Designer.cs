namespace CIS_Textil
{
    partial class frmCompCreation
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.ciS_TextLabel5 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.btnCancel = new CIS_CLibrary.CIS_Button();
            this.btnCreate = new CIS_CLibrary.CIS_Button();
            this.lblCompanyname = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtCompany = new CIS_CLibrary.CIS_Textbox();
            this.lblUserName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtUserName = new CIS_CLibrary.CIS_Textbox();
            this.lblPassword = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtPassword = new CIS_CLibrary.CIS_Textbox();
            this.ciS_TextLabel3 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblAdministrator = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.ciS_TextLabel1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.ciS_TextLabel2 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.ciS_TextLabel4 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.pnlBody = new System.Windows.Forms.Panel();
            this.ciS_TextLabel6 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtConfirmPassword = new CIS_CLibrary.CIS_Textbox();
            this.lblConfirmPwd = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.lineShape2 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.pnlHeader.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.DarkCyan;
            this.pnlHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(463, 27);
            this.pnlHeader.TabIndex = 8;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(6, 4);
            this.lblTitle.Moveable = false;
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.NameOfControl = null;
            this.lblTitle.Size = new System.Drawing.Size(158, 18);
            this.lblTitle.TabIndex = 10;
            this.lblTitle.Text = "Start New Company";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.DarkCyan;
            this.pnlFooter.Controls.Add(this.ciS_TextLabel5);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 281);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(463, 27);
            this.pnlFooter.TabIndex = 9;
            // 
            // ciS_TextLabel5
            // 
            this.ciS_TextLabel5.AutoSize = true;
            this.ciS_TextLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ciS_TextLabel5.Location = new System.Drawing.Point(240, 4);
            this.ciS_TextLabel5.Moveable = false;
            this.ciS_TextLabel5.Name = "ciS_TextLabel5";
            this.ciS_TextLabel5.NameOfControl = null;
            this.ciS_TextLabel5.Size = new System.Drawing.Size(217, 18);
            this.ciS_TextLabel5.TabIndex = 11;
            this.ciS_TextLabel5.Text = "www.crocusitsolutions.com";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Teal;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCancel.GlowColor = System.Drawing.Color.Transparent;
            this.btnCancel.Location = new System.Drawing.Point(326, 220);
            this.btnCancel.Moveable = false;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(69, 27);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "C&ancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.BackColor = System.Drawing.Color.Teal;
            this.btnCreate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnCreate.GlowColor = System.Drawing.Color.Transparent;
            this.btnCreate.Location = new System.Drawing.Point(246, 220);
            this.btnCreate.Moveable = false;
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(69, 27);
            this.btnCreate.TabIndex = 8;
            this.btnCreate.Text = "&Create";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // lblCompanyname
            // 
            this.lblCompanyname.AutoSize = true;
            this.lblCompanyname.BackColor = System.Drawing.Color.Transparent;
            this.lblCompanyname.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyname.ForeColor = System.Drawing.Color.White;
            this.lblCompanyname.Location = new System.Drawing.Point(24, 45);
            this.lblCompanyname.Moveable = false;
            this.lblCompanyname.Name = "lblCompanyname";
            this.lblCompanyname.NameOfControl = null;
            this.lblCompanyname.Size = new System.Drawing.Size(127, 16);
            this.lblCompanyname.TabIndex = 12;
            this.lblCompanyname.Text = "Company Name ";
            // 
            // txtCompany
            // 
            this.txtCompany.AutoFillDate = false;
            this.txtCompany.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtCompany.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtCompany.CheckForSymbol = null;
            this.txtCompany.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtCompany.DecimalPlace = 0;
            this.txtCompany.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCompany.HelpText = null;
            this.txtCompany.HoldMyText = null;
            this.txtCompany.IsMandatory = true;
            this.txtCompany.IsSingleQuote = true;
            this.txtCompany.IsSysmbol = false;
            this.txtCompany.Location = new System.Drawing.Point(188, 44);
            this.txtCompany.Mask = null;
            this.txtCompany.MaxLength = 50;
            this.txtCompany.Moveable = false;
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.NameOfControl = null;
            this.txtCompany.Prefix = null;
            this.txtCompany.ShowBallonTip = false;
            this.txtCompany.ShowErrorIcon = false;
            this.txtCompany.ShowMessage = null;
            this.txtCompany.Size = new System.Drawing.Size(245, 21);
            this.txtCompany.Suffix = null;
            this.txtCompany.TabIndex = 10;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblUserName.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.Color.White;
            this.lblUserName.Location = new System.Drawing.Point(24, 143);
            this.lblUserName.Moveable = false;
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.NameOfControl = null;
            this.lblUserName.Size = new System.Drawing.Size(87, 16);
            this.lblUserName.TabIndex = 14;
            this.lblUserName.Text = "User Name";
            // 
            // txtUserName
            // 
            this.txtUserName.AutoFillDate = false;
            this.txtUserName.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtUserName.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtUserName.CheckForSymbol = null;
            this.txtUserName.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtUserName.DecimalPlace = 0;
            this.txtUserName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserName.HelpText = null;
            this.txtUserName.HoldMyText = null;
            this.txtUserName.IsMandatory = true;
            this.txtUserName.IsSingleQuote = true;
            this.txtUserName.IsSysmbol = false;
            this.txtUserName.Location = new System.Drawing.Point(188, 142);
            this.txtUserName.Mask = null;
            this.txtUserName.MaxLength = 50;
            this.txtUserName.Moveable = false;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.NameOfControl = null;
            this.txtUserName.Prefix = null;
            this.txtUserName.ShowBallonTip = false;
            this.txtUserName.ShowErrorIcon = false;
            this.txtUserName.ShowMessage = null;
            this.txtUserName.Size = new System.Drawing.Size(245, 21);
            this.txtUserName.Suffix = null;
            this.txtUserName.TabIndex = 13;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.White;
            this.lblPassword.Location = new System.Drawing.Point(24, 168);
            this.lblPassword.Moveable = false;
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.NameOfControl = null;
            this.lblPassword.Size = new System.Drawing.Size(82, 16);
            this.lblPassword.TabIndex = 16;
            this.lblPassword.Text = "Password ";
            // 
            // txtPassword
            // 
            this.txtPassword.AutoFillDate = false;
            this.txtPassword.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtPassword.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtPassword.CheckForSymbol = null;
            this.txtPassword.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtPassword.DecimalPlace = 0;
            this.txtPassword.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.HelpText = null;
            this.txtPassword.HoldMyText = null;
            this.txtPassword.IsMandatory = true;
            this.txtPassword.IsSingleQuote = true;
            this.txtPassword.IsSysmbol = false;
            this.txtPassword.Location = new System.Drawing.Point(188, 167);
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
            this.txtPassword.Size = new System.Drawing.Size(245, 21);
            this.txtPassword.Suffix = null;
            this.txtPassword.TabIndex = 15;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // ciS_TextLabel3
            // 
            this.ciS_TextLabel3.AutoSize = true;
            this.ciS_TextLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ciS_TextLabel3.ForeColor = System.Drawing.Color.White;
            this.ciS_TextLabel3.Location = new System.Drawing.Point(24, 18);
            this.ciS_TextLabel3.Moveable = false;
            this.ciS_TextLabel3.Name = "ciS_TextLabel3";
            this.ciS_TextLabel3.NameOfControl = null;
            this.ciS_TextLabel3.Size = new System.Drawing.Size(148, 18);
            this.ciS_TextLabel3.TabIndex = 17;
            this.ciS_TextLabel3.Text = "Company Creation";
            // 
            // lblAdministrator
            // 
            this.lblAdministrator.AutoSize = true;
            this.lblAdministrator.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.lblAdministrator.ForeColor = System.Drawing.Color.White;
            this.lblAdministrator.Location = new System.Drawing.Point(24, 109);
            this.lblAdministrator.Moveable = false;
            this.lblAdministrator.Name = "lblAdministrator";
            this.lblAdministrator.NameOfControl = null;
            this.lblAdministrator.Size = new System.Drawing.Size(163, 18);
            this.lblAdministrator.TabIndex = 18;
            this.lblAdministrator.Text = "Create Administrator";
            // 
            // ciS_TextLabel1
            // 
            this.ciS_TextLabel1.AutoSize = true;
            this.ciS_TextLabel1.BackColor = System.Drawing.Color.Transparent;
            this.ciS_TextLabel1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ciS_TextLabel1.ForeColor = System.Drawing.Color.White;
            this.ciS_TextLabel1.Location = new System.Drawing.Point(172, 145);
            this.ciS_TextLabel1.Moveable = false;
            this.ciS_TextLabel1.Name = "ciS_TextLabel1";
            this.ciS_TextLabel1.NameOfControl = null;
            this.ciS_TextLabel1.Size = new System.Drawing.Size(13, 16);
            this.ciS_TextLabel1.TabIndex = 19;
            this.ciS_TextLabel1.Text = ":";
            // 
            // ciS_TextLabel2
            // 
            this.ciS_TextLabel2.AutoSize = true;
            this.ciS_TextLabel2.BackColor = System.Drawing.Color.Transparent;
            this.ciS_TextLabel2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ciS_TextLabel2.ForeColor = System.Drawing.Color.White;
            this.ciS_TextLabel2.Location = new System.Drawing.Point(171, 45);
            this.ciS_TextLabel2.Moveable = false;
            this.ciS_TextLabel2.Name = "ciS_TextLabel2";
            this.ciS_TextLabel2.NameOfControl = null;
            this.ciS_TextLabel2.Size = new System.Drawing.Size(13, 16);
            this.ciS_TextLabel2.TabIndex = 20;
            this.ciS_TextLabel2.Text = ":";
            // 
            // ciS_TextLabel4
            // 
            this.ciS_TextLabel4.AutoSize = true;
            this.ciS_TextLabel4.BackColor = System.Drawing.Color.Transparent;
            this.ciS_TextLabel4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ciS_TextLabel4.ForeColor = System.Drawing.Color.White;
            this.ciS_TextLabel4.Location = new System.Drawing.Point(172, 169);
            this.ciS_TextLabel4.Moveable = false;
            this.ciS_TextLabel4.Name = "ciS_TextLabel4";
            this.ciS_TextLabel4.NameOfControl = null;
            this.ciS_TextLabel4.Size = new System.Drawing.Size(13, 16);
            this.ciS_TextLabel4.TabIndex = 21;
            this.ciS_TextLabel4.Text = ":";
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.CadetBlue;
            this.pnlBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBody.Controls.Add(this.ciS_TextLabel6);
            this.pnlBody.Controls.Add(this.txtConfirmPassword);
            this.pnlBody.Controls.Add(this.lblConfirmPwd);
            this.pnlBody.Controls.Add(this.ciS_TextLabel4);
            this.pnlBody.Controls.Add(this.ciS_TextLabel2);
            this.pnlBody.Controls.Add(this.ciS_TextLabel1);
            this.pnlBody.Controls.Add(this.lblAdministrator);
            this.pnlBody.Controls.Add(this.ciS_TextLabel3);
            this.pnlBody.Controls.Add(this.txtPassword);
            this.pnlBody.Controls.Add(this.lblPassword);
            this.pnlBody.Controls.Add(this.txtUserName);
            this.pnlBody.Controls.Add(this.lblUserName);
            this.pnlBody.Controls.Add(this.txtCompany);
            this.pnlBody.Controls.Add(this.lblCompanyname);
            this.pnlBody.Controls.Add(this.btnCreate);
            this.pnlBody.Controls.Add(this.btnCancel);
            this.pnlBody.Controls.Add(this.shapeContainer1);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 27);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(463, 254);
            this.pnlBody.TabIndex = 10;
            // 
            // ciS_TextLabel6
            // 
            this.ciS_TextLabel6.AutoSize = true;
            this.ciS_TextLabel6.BackColor = System.Drawing.Color.Transparent;
            this.ciS_TextLabel6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ciS_TextLabel6.ForeColor = System.Drawing.Color.White;
            this.ciS_TextLabel6.Location = new System.Drawing.Point(172, 194);
            this.ciS_TextLabel6.Moveable = false;
            this.ciS_TextLabel6.Name = "ciS_TextLabel6";
            this.ciS_TextLabel6.NameOfControl = null;
            this.ciS_TextLabel6.Size = new System.Drawing.Size(13, 16);
            this.ciS_TextLabel6.TabIndex = 24;
            this.ciS_TextLabel6.Text = ":";
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.AutoFillDate = false;
            this.txtConfirmPassword.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtConfirmPassword.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtConfirmPassword.CheckForSymbol = null;
            this.txtConfirmPassword.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtConfirmPassword.DecimalPlace = 0;
            this.txtConfirmPassword.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConfirmPassword.HelpText = null;
            this.txtConfirmPassword.HoldMyText = null;
            this.txtConfirmPassword.IsMandatory = true;
            this.txtConfirmPassword.IsSingleQuote = true;
            this.txtConfirmPassword.IsSysmbol = false;
            this.txtConfirmPassword.Location = new System.Drawing.Point(188, 192);
            this.txtConfirmPassword.Mask = null;
            this.txtConfirmPassword.MaxLength = 50;
            this.txtConfirmPassword.Moveable = false;
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.NameOfControl = null;
            this.txtConfirmPassword.Prefix = null;
            this.txtConfirmPassword.ShowBallonTip = false;
            this.txtConfirmPassword.ShowErrorIcon = false;
            this.txtConfirmPassword.ShowMessage = null;
            this.txtConfirmPassword.Size = new System.Drawing.Size(245, 21);
            this.txtConfirmPassword.Suffix = null;
            this.txtConfirmPassword.TabIndex = 22;
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // lblConfirmPwd
            // 
            this.lblConfirmPwd.AutoSize = true;
            this.lblConfirmPwd.BackColor = System.Drawing.Color.Transparent;
            this.lblConfirmPwd.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConfirmPwd.ForeColor = System.Drawing.Color.White;
            this.lblConfirmPwd.Location = new System.Drawing.Point(24, 193);
            this.lblConfirmPwd.Moveable = false;
            this.lblConfirmPwd.Name = "lblConfirmPwd";
            this.lblConfirmPwd.NameOfControl = null;
            this.lblConfirmPwd.Size = new System.Drawing.Size(143, 16);
            this.lblConfirmPwd.TabIndex = 23;
            this.lblConfirmPwd.Text = "Confirm Password ";
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape2,
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(461, 252);
            this.shapeContainer1.TabIndex = 25;
            this.shapeContainer1.TabStop = false;
            // 
            // lineShape2
            // 
            this.lineShape2.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lineShape2.Name = "lineShape2";
            this.lineShape2.X1 = -1;
            this.lineShape2.X2 = 461;
            this.lineShape2.Y1 = 90;
            this.lineShape2.Y2 = 90;
            // 
            // lineShape1
            // 
            this.lineShape1.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = -1;
            this.lineShape1.X2 = 461;
            this.lineShape1.Y1 = 93;
            this.lineShape1.Y2 = 93;
            // 
            // frmCompCreation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(463, 308);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlHeader);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmCompCreation";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCompCreation";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmCompCreation_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            this.pnlBody.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlFooter;
        private CIS_CLibrary.CIS_TextLabel lblTitle;
        internal CIS_CLibrary.CIS_Button btnCancel;
        internal CIS_CLibrary.CIS_Button btnCreate;
        internal CIS_CLibrary.CIS_TextLabel lblCompanyname;
        internal CIS_CLibrary.CIS_Textbox txtCompany;
        internal CIS_CLibrary.CIS_TextLabel lblUserName;
        internal CIS_CLibrary.CIS_Textbox txtUserName;
        internal CIS_CLibrary.CIS_TextLabel lblPassword;
        internal CIS_CLibrary.CIS_Textbox txtPassword;
        private CIS_CLibrary.CIS_TextLabel ciS_TextLabel3;
        private CIS_CLibrary.CIS_TextLabel lblAdministrator;
        internal CIS_CLibrary.CIS_TextLabel ciS_TextLabel1;
        internal CIS_CLibrary.CIS_TextLabel ciS_TextLabel2;
        internal CIS_CLibrary.CIS_TextLabel ciS_TextLabel4;
        private System.Windows.Forms.Panel pnlBody;
        private CIS_CLibrary.CIS_TextLabel ciS_TextLabel5;
        internal CIS_CLibrary.CIS_TextLabel ciS_TextLabel6;
        internal CIS_CLibrary.CIS_Textbox txtConfirmPassword;
        internal CIS_CLibrary.CIS_TextLabel lblConfirmPwd;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape2;
        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
    }
}