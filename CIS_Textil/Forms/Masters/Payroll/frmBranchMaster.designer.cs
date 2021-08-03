namespace CIS_Textil
{
    partial class frmBranchMaster
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
            this.ChkActive = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.lblphoneColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblAddressColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblBranchNameColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblphone = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblAddress = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblBranchName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.txtBranchName = new CIS_CLibrary.CIS_Textbox();
            this.txtphone = new CIS_CLibrary.CIS_Textbox();
            this.txtAddress = new CIS_CLibrary.CIS_Textbox();
            this.label1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtAliasName = new CIS_CLibrary.CIS_Textbox();
            this.label25 = new CIS_CLibrary.CIS_TextLabel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.label1);
            this.pnlContent.Controls.Add(this.txtAliasName);
            this.pnlContent.Controls.Add(this.label25);
            this.pnlContent.Controls.Add(this.txtAddress);
            this.pnlContent.Controls.Add(this.txtphone);
            this.pnlContent.Controls.Add(this.txtBranchName);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.Add(this.lblBranchName);
            this.pnlContent.Controls.Add(this.lblBranchNameColon);
            this.pnlContent.Controls.Add(this.ChkActive);
            this.pnlContent.Controls.Add(this.lblAddressColon);
            this.pnlContent.Controls.Add(this.lblAddress);
            this.pnlContent.Controls.Add(this.lblphoneColon);
            this.pnlContent.Controls.Add(this.lblphone);
            this.pnlContent.Controls.SetChildIndex(this.lblphone, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblphoneColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblAddress, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblAddressColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.ChkActive, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblBranchNameColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblBranchName, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtBranchName, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtphone, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtAddress, 0);
            this.pnlContent.Controls.SetChildIndex(this.label25, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtAliasName, 0);
            this.pnlContent.Controls.SetChildIndex(this.label1, 0);
            // 
            // ChkActive
            // 
            this.ChkActive.AutoSize = true;
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChkActive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.HelpText = "Checked If Active";
            this.ChkActive.Location = new System.Drawing.Point(330, 189);
            this.ChkActive.Moveable = false;
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.NameOfControl = null;
            this.ChkActive.Size = new System.Drawing.Size(110, 18);
            this.ChkActive.TabIndex = 5;
            this.ChkActive.Text = "Active Status";
            this.ChkActive.UseVisualStyleBackColor = false;
            // 
            // lblphoneColon
            // 
            this.lblphoneColon.AutoSize = true;
            this.lblphoneColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblphoneColon.Location = new System.Drawing.Point(314, 168);
            this.lblphoneColon.Moveable = false;
            this.lblphoneColon.Name = "lblphoneColon";
            this.lblphoneColon.NameOfControl = null;
            this.lblphoneColon.Size = new System.Drawing.Size(12, 14);
            this.lblphoneColon.TabIndex = 477;
            this.lblphoneColon.Text = ":";
            // 
            // lblAddressColon
            // 
            this.lblAddressColon.AutoSize = true;
            this.lblAddressColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddressColon.Location = new System.Drawing.Point(311, 68);
            this.lblAddressColon.Moveable = false;
            this.lblAddressColon.Name = "lblAddressColon";
            this.lblAddressColon.NameOfControl = null;
            this.lblAddressColon.Size = new System.Drawing.Size(12, 14);
            this.lblAddressColon.TabIndex = 476;
            this.lblAddressColon.Text = ":";
            // 
            // lblBranchNameColon
            // 
            this.lblBranchNameColon.AutoSize = true;
            this.lblBranchNameColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBranchNameColon.Location = new System.Drawing.Point(311, 18);
            this.lblBranchNameColon.Moveable = false;
            this.lblBranchNameColon.Name = "lblBranchNameColon";
            this.lblBranchNameColon.NameOfControl = null;
            this.lblBranchNameColon.Size = new System.Drawing.Size(12, 14);
            this.lblBranchNameColon.TabIndex = 475;
            this.lblBranchNameColon.Text = ":";
            // 
            // lblphone
            // 
            this.lblphone.AutoSize = true;
            this.lblphone.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblphone.Location = new System.Drawing.Point(191, 168);
            this.lblphone.Moveable = false;
            this.lblphone.Name = "lblphone";
            this.lblphone.NameOfControl = null;
            this.lblphone.Size = new System.Drawing.Size(104, 14);
            this.lblphone.TabIndex = 473;
            this.lblphone.Text = "Phone Number";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Location = new System.Drawing.Point(191, 68);
            this.lblAddress.Moveable = false;
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.NameOfControl = null;
            this.lblAddress.Size = new System.Drawing.Size(60, 14);
            this.lblAddress.TabIndex = 471;
            this.lblAddress.Text = "Address";
            // 
            // lblBranchName
            // 
            this.lblBranchName.AutoSize = true;
            this.lblBranchName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBranchName.Location = new System.Drawing.Point(191, 18);
            this.lblBranchName.Moveable = false;
            this.lblBranchName.Name = "lblBranchName";
            this.lblBranchName.NameOfControl = null;
            this.lblBranchName.Size = new System.Drawing.Size(95, 14);
            this.lblBranchName.TabIndex = 468;
            this.lblBranchName.Text = "Branch Name";
            // 
            // txtCode
            // 
            this.txtCode.AutoFillDate = false;
            this.txtCode.BackColor = System.Drawing.Color.White;
            this.txtCode.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtCode.CheckForSymbol = null;
            this.txtCode.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtCode.DecimalPlace = 0;
            this.txtCode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.HelpText = "";
            this.txtCode.HoldMyText = null;
            this.txtCode.IsMandatory = false;
            this.txtCode.IsSingleQuote = true;
            this.txtCode.IsSysmbol = false;
            this.txtCode.Location = new System.Drawing.Point(9, 6);
            this.txtCode.Mask = null;
            this.txtCode.Moveable = false;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(25, 22);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 0;
            this.txtCode.Visible = false;
            // 
            // txtBranchName
            // 
            this.txtBranchName.AutoFillDate = false;
            this.txtBranchName.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtBranchName.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtBranchName.CheckForSymbol = null;
            this.txtBranchName.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtBranchName.DecimalPlace = 0;
            this.txtBranchName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBranchName.HelpText = "Enter Branch Name";
            this.txtBranchName.HoldMyText = null;
            this.txtBranchName.IsMandatory = true;
            this.txtBranchName.IsSingleQuote = true;
            this.txtBranchName.IsSysmbol = false;
            this.txtBranchName.Location = new System.Drawing.Point(330, 16);
            this.txtBranchName.Mask = null;
            this.txtBranchName.MaxLength = 50;
            this.txtBranchName.Moveable = false;
            this.txtBranchName.Name = "txtBranchName";
            this.txtBranchName.NameOfControl = "Branch name";
            this.txtBranchName.Prefix = null;
            this.txtBranchName.ShowBallonTip = false;
            this.txtBranchName.ShowErrorIcon = false;
            this.txtBranchName.ShowMessage = null;
            this.txtBranchName.Size = new System.Drawing.Size(259, 22);
            this.txtBranchName.Suffix = null;
            this.txtBranchName.TabIndex = 1;
            this.txtBranchName.Leave += new System.EventHandler(this.txtBranchName_Leave);
            // 
            // txtphone
            // 
            this.txtphone.AutoFillDate = false;
            this.txtphone.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtphone.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtphone.CheckForSymbol = null;
            this.txtphone.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.NumericWithOutDecimal;
            this.txtphone.DecimalPlace = 0;
            this.txtphone.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtphone.HelpText = "Enter Phone Number";
            this.txtphone.HoldMyText = null;
            this.txtphone.IsMandatory = true;
            this.txtphone.IsSingleQuote = true;
            this.txtphone.IsSysmbol = false;
            this.txtphone.Location = new System.Drawing.Point(330, 166);
            this.txtphone.Mask = null;
            this.txtphone.MaxLength = 15;
            this.txtphone.Moveable = false;
            this.txtphone.Name = "txtphone";
            this.txtphone.NameOfControl = "Phone";
            this.txtphone.Prefix = null;
            this.txtphone.ShowBallonTip = false;
            this.txtphone.ShowErrorIcon = false;
            this.txtphone.ShowMessage = null;
            this.txtphone.Size = new System.Drawing.Size(259, 22);
            this.txtphone.Suffix = null;
            this.txtphone.TabIndex = 4;
            this.txtphone.Text = "0";
            this.txtphone.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtAddress
            // 
            this.txtAddress.AutoFillDate = false;
            this.txtAddress.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtAddress.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtAddress.CheckForSymbol = null;
            this.txtAddress.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtAddress.DecimalPlace = 0;
            this.txtAddress.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.HelpText = "Enter Address";
            this.txtAddress.HoldMyText = null;
            this.txtAddress.IsMandatory = true;
            this.txtAddress.IsSingleQuote = true;
            this.txtAddress.IsSysmbol = false;
            this.txtAddress.Location = new System.Drawing.Point(330, 65);
            this.txtAddress.Mask = null;
            this.txtAddress.MaxLength = 500;
            this.txtAddress.Moveable = false;
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.NameOfControl = "Address";
            this.txtAddress.Prefix = null;
            this.txtAddress.ShowBallonTip = false;
            this.txtAddress.ShowErrorIcon = false;
            this.txtAddress.ShowMessage = null;
            this.txtAddress.Size = new System.Drawing.Size(259, 97);
            this.txtAddress.Suffix = null;
            this.txtAddress.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(311, 42);
            this.label1.Moveable = false;
            this.label1.Name = "label1";
            this.label1.NameOfControl = null;
            this.label1.Size = new System.Drawing.Size(12, 14);
            this.label1.TabIndex = 1151;
            this.label1.Text = ":";
            // 
            // txtAliasName
            // 
            this.txtAliasName.AutoFillDate = false;
            this.txtAliasName.BackColor = System.Drawing.Color.White;
            this.txtAliasName.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtAliasName.CheckForSymbol = null;
            this.txtAliasName.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtAliasName.DecimalPlace = 0;
            this.txtAliasName.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAliasName.HelpText = "Enter Alias Name";
            this.txtAliasName.HoldMyText = null;
            this.txtAliasName.IsMandatory = false;
            this.txtAliasName.IsSingleQuote = true;
            this.txtAliasName.IsSysmbol = false;
            this.txtAliasName.Location = new System.Drawing.Point(330, 40);
            this.txtAliasName.Mask = null;
            this.txtAliasName.MaxLength = 50;
            this.txtAliasName.Moveable = false;
            this.txtAliasName.Name = "txtAliasName";
            this.txtAliasName.NameOfControl = "Alias name";
            this.txtAliasName.Prefix = null;
            this.txtAliasName.ShowBallonTip = false;
            this.txtAliasName.ShowErrorIcon = false;
            this.txtAliasName.ShowMessage = null;
            this.txtAliasName.Size = new System.Drawing.Size(259, 21);
            this.txtAliasName.Suffix = null;
            this.txtAliasName.TabIndex = 2;
            this.txtAliasName.Leave += new System.EventHandler(this.txtAliasName_Leave);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(191, 43);
            this.label25.Moveable = false;
            this.label25.Name = "label25";
            this.label25.NameOfControl = null;
            this.label25.Size = new System.Drawing.Size(81, 14);
            this.label25.TabIndex = 1150;
            this.label25.Text = "Alias Name";
            // 
            // frmBranchMaster
            // 
            this.ClientSize = new System.Drawing.Size(808, 545);
            this.Name = "frmBranchMaster";
            this.Load += new System.EventHandler(this.frmBranchMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal CIS_CLibrary.CIS_CheckBox ChkActive;
        private CIS_CLibrary.CIS_TextLabel lblphoneColon;
        private CIS_CLibrary.CIS_TextLabel lblAddressColon;
        private CIS_CLibrary.CIS_TextLabel lblBranchNameColon;
        private CIS_CLibrary.CIS_TextLabel lblphone;
        private CIS_CLibrary.CIS_TextLabel lblAddress;
        private CIS_CLibrary.CIS_TextLabel lblBranchName;
        public CIS_CLibrary.CIS_Textbox txtCode;
        private CIS_CLibrary.CIS_Textbox txtphone;
        private CIS_CLibrary.CIS_Textbox txtBranchName;
        internal CIS_CLibrary.CIS_Textbox txtAddress;
        internal CIS_CLibrary.CIS_TextLabel label1;
        internal CIS_CLibrary.CIS_Textbox txtAliasName;
        internal CIS_CLibrary.CIS_TextLabel label25;
    }
}
