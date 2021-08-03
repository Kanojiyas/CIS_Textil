namespace CIS_Textil
{
    partial class frmUserMaster
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
            this.CboUserType = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblUserType = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtPassword = new CIS_CLibrary.CIS_Textbox();
            this.txtUserName = new CIS_CLibrary.CIS_Textbox();
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.lblPassWord = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblUserName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblColnUserName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblColnPassword = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblColnSLevel = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblColnEmpPassword = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblColnEmpName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtEmpID = new CIS_CLibrary.CIS_Textbox();
            this.txtEmployeeName = new CIS_CLibrary.CIS_Textbox();
            this.lblEmpID = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblEmpName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.label1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtGroupName = new CIS_CLibrary.CIS_Textbox();
            this.lblGroupName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.dgvCmpSelection = new System.Windows.Forms.DataGridView();
            this.CompID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtCompName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.txtImageData = new CIS_CLibrary.CIS_Textbox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.pctImage = new System.Windows.Forms.PictureBox();
            this.txtImagePath = new CIS_CLibrary.CIS_Textbox();
            this.chkShowPanelStock = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.lblColMobile = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtMobileNo = new CIS_CLibrary.CIS_Textbox();
            this.lblMobileNo = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.chkAutoGenPwd = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.lblColEmail = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtEmailID = new CIS_CLibrary.CIS_Textbox();
            this.lblEmail = new CIS_CLibrary.CIS_TextLabel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCmpSelection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctImage)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.lblColEmail);
            this.pnlContent.Controls.Add(this.txtEmailID);
            this.pnlContent.Controls.Add(this.lblEmail);
            this.pnlContent.Controls.Add(this.chkAutoGenPwd);
            this.pnlContent.Controls.Add(this.lblColMobile);
            this.pnlContent.Controls.Add(this.txtMobileNo);
            this.pnlContent.Controls.Add(this.lblMobileNo);
            this.pnlContent.Controls.Add(this.chkShowPanelStock);
            this.pnlContent.Controls.Add(this.txtImageData);
            this.pnlContent.Controls.Add(this.btnBrowse);
            this.pnlContent.Controls.Add(this.pctImage);
            this.pnlContent.Controls.Add(this.txtImagePath);
            this.pnlContent.Controls.Add(this.dgvCmpSelection);
            this.pnlContent.Controls.Add(this.label1);
            this.pnlContent.Controls.Add(this.txtGroupName);
            this.pnlContent.Controls.Add(this.lblGroupName);
            this.pnlContent.Controls.Add(this.lblColnEmpPassword);
            this.pnlContent.Controls.Add(this.lblColnEmpName);
            this.pnlContent.Controls.Add(this.txtEmpID);
            this.pnlContent.Controls.Add(this.txtEmployeeName);
            this.pnlContent.Controls.Add(this.lblEmpID);
            this.pnlContent.Controls.Add(this.lblEmpName);
            this.pnlContent.Controls.Add(this.lblColnSLevel);
            this.pnlContent.Controls.Add(this.lblColnPassword);
            this.pnlContent.Controls.Add(this.lblColnUserName);
            this.pnlContent.Controls.Add(this.ChkActive);
            this.pnlContent.Controls.Add(this.CboUserType);
            this.pnlContent.Controls.Add(this.lblUserType);
            this.pnlContent.Controls.Add(this.txtPassword);
            this.pnlContent.Controls.Add(this.txtUserName);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.Add(this.lblPassWord);
            this.pnlContent.Controls.Add(this.lblUserName);
            this.pnlContent.Size = new System.Drawing.Size(805, 496);
            this.pnlContent.Controls.SetChildIndex(this.lblUserName, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblPassWord, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtUserName, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtPassword, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblUserType, 0);
            this.pnlContent.Controls.SetChildIndex(this.CboUserType, 0);
            this.pnlContent.Controls.SetChildIndex(this.ChkActive, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblColnUserName, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblColnPassword, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblColnSLevel, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEmpName, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEmpID, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtEmployeeName, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtEmpID, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblColnEmpName, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblColnEmpPassword, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblGroupName, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtGroupName, 0);
            this.pnlContent.Controls.SetChildIndex(this.label1, 0);
            this.pnlContent.Controls.SetChildIndex(this.dgvCmpSelection, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtImagePath, 0);
            this.pnlContent.Controls.SetChildIndex(this.pctImage, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnBrowse, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtImageData, 0);
            this.pnlContent.Controls.SetChildIndex(this.chkShowPanelStock, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblMobileNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtMobileNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblColMobile, 0);
            this.pnlContent.Controls.SetChildIndex(this.chkAutoGenPwd, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEmail, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtEmailID, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblColEmail, 0);
            // 
            // lblHelpText
            // 
            this.lblHelpText.Location = new System.Drawing.Point(46, 3);
            // 
            // lblUUser
            // 
            this.lblUUser.Location = new System.Drawing.Point(667, 3);
            // 
            // lblCUser
            // 
            this.lblCUser.Location = new System.Drawing.Point(359, 3);
            // 
            // ChkActive
            // 
            this.ChkActive.AutoSize = true;
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChkActive.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.HelpText = null;
            this.ChkActive.Location = new System.Drawing.Point(289, 158);
            this.ChkActive.Moveable = false;
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.NameOfControl = null;
            this.ChkActive.Size = new System.Drawing.Size(109, 17);
            this.ChkActive.TabIndex = 6;
            this.ChkActive.Text = "Active Status";
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // CboUserType
            // 
            this.CboUserType.AutoComplete = false;
            this.CboUserType.AutoDropdown = false;
            this.CboUserType.BackColor = System.Drawing.Color.White;
            this.CboUserType.BackColorEven = System.Drawing.Color.White;
            this.CboUserType.BackColorOdd = System.Drawing.Color.White;
            this.CboUserType.ColumnNames = "";
            this.CboUserType.ColumnWidthDefault = 175;
            this.CboUserType.ColumnWidths = "";
            this.CboUserType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.CboUserType.Fill_ComboID = 0;
            this.CboUserType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.CboUserType.FormattingEnabled = true;
            this.CboUserType.GroupType = 0;
            this.CboUserType.HelpText = "Select Security Level";
            this.CboUserType.IsMandatory = false;
            this.CboUserType.LinkedColumnIndex = 0;
            this.CboUserType.LinkedTextBox = null;
            this.CboUserType.Location = new System.Drawing.Point(289, 129);
            this.CboUserType.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.CboUserType.Moveable = false;
            this.CboUserType.Name = "CboUserType";
            this.CboUserType.NameOfControl = null;
            this.CboUserType.OpenForm = "frmYarnTypeMaster";
            this.CboUserType.ShowBallonTip = false;
            this.CboUserType.Size = new System.Drawing.Size(200, 23);
            this.CboUserType.TabIndex = 5;
            // 
            // lblUserType
            // 
            this.lblUserType.AutoSize = true;
            this.lblUserType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblUserType.Location = new System.Drawing.Point(149, 132);
            this.lblUserType.Moveable = false;
            this.lblUserType.Name = "lblUserType";
            this.lblUserType.NameOfControl = null;
            this.lblUserType.Size = new System.Drawing.Size(106, 14);
            this.lblUserType.TabIndex = 29;
            this.lblUserType.Text = "Security Level ";
            // 
            // txtPassword
            // 
            this.txtPassword.AutoFillDate = false;
            this.txtPassword.BackColor = System.Drawing.Color.White;
            this.txtPassword.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtPassword.CheckForSymbol = null;
            this.txtPassword.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtPassword.DecimalPlace = 0;
            this.txtPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtPassword.HelpText = "Enter Password";
            this.txtPassword.HoldMyText = null;
            this.txtPassword.IsMandatory = false;
            this.txtPassword.IsSingleQuote = true;
            this.txtPassword.IsSysmbol = false;
            this.txtPassword.Location = new System.Drawing.Point(289, 54);
            this.txtPassword.Mask = null;
            this.txtPassword.Moveable = false;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.NameOfControl = null;
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Prefix = null;
            this.txtPassword.ShowBallonTip = false;
            this.txtPassword.ShowErrorIcon = false;
            this.txtPassword.ShowMessage = null;
            this.txtPassword.Size = new System.Drawing.Size(200, 22);
            this.txtPassword.Suffix = null;
            this.txtPassword.TabIndex = 2;
            // 
            // txtUserName
            // 
            this.txtUserName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtUserName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtUserName.AutoFillDate = false;
            this.txtUserName.BackColor = System.Drawing.Color.White;
            this.txtUserName.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtUserName.CheckForSymbol = null;
            this.txtUserName.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtUserName.DecimalPlace = 0;
            this.txtUserName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtUserName.HelpText = "Enter User Name";
            this.txtUserName.HoldMyText = null;
            this.txtUserName.IsMandatory = false;
            this.txtUserName.IsSingleQuote = true;
            this.txtUserName.IsSysmbol = false;
            this.txtUserName.Location = new System.Drawing.Point(289, 29);
            this.txtUserName.Mask = null;
            this.txtUserName.Moveable = false;
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.NameOfControl = null;
            this.txtUserName.Prefix = null;
            this.txtUserName.ShowBallonTip = false;
            this.txtUserName.ShowErrorIcon = false;
            this.txtUserName.ShowMessage = null;
            this.txtUserName.Size = new System.Drawing.Size(200, 22);
            this.txtUserName.Suffix = null;
            this.txtUserName.TabIndex = 1;
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
            this.txtCode.Size = new System.Drawing.Size(32, 22);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 28;
            this.txtCode.Visible = false;
            // 
            // lblPassWord
            // 
            this.lblPassWord.AutoSize = true;
            this.lblPassWord.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblPassWord.Location = new System.Drawing.Point(149, 56);
            this.lblPassWord.Moveable = false;
            this.lblPassWord.Name = "lblPassWord";
            this.lblPassWord.NameOfControl = null;
            this.lblPassWord.Size = new System.Drawing.Size(72, 14);
            this.lblPassWord.TabIndex = 26;
            this.lblPassWord.Text = "Password";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblUserName.Location = new System.Drawing.Point(149, 31);
            this.lblUserName.Moveable = false;
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.NameOfControl = null;
            this.lblUserName.Size = new System.Drawing.Size(80, 14);
            this.lblUserName.TabIndex = 25;
            this.lblUserName.Text = "User Name";
            // 
            // lblColnUserName
            // 
            this.lblColnUserName.AutoSize = true;
            this.lblColnUserName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblColnUserName.Location = new System.Drawing.Point(272, 31);
            this.lblColnUserName.Moveable = false;
            this.lblColnUserName.Name = "lblColnUserName";
            this.lblColnUserName.NameOfControl = null;
            this.lblColnUserName.Size = new System.Drawing.Size(12, 14);
            this.lblColnUserName.TabIndex = 32;
            this.lblColnUserName.Text = ":";
            // 
            // lblColnPassword
            // 
            this.lblColnPassword.AutoSize = true;
            this.lblColnPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblColnPassword.Location = new System.Drawing.Point(272, 56);
            this.lblColnPassword.Moveable = false;
            this.lblColnPassword.Name = "lblColnPassword";
            this.lblColnPassword.NameOfControl = null;
            this.lblColnPassword.Size = new System.Drawing.Size(12, 14);
            this.lblColnPassword.TabIndex = 33;
            this.lblColnPassword.Text = ":";
            // 
            // lblColnSLevel
            // 
            this.lblColnSLevel.AutoSize = true;
            this.lblColnSLevel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblColnSLevel.Location = new System.Drawing.Point(272, 132);
            this.lblColnSLevel.Moveable = false;
            this.lblColnSLevel.Name = "lblColnSLevel";
            this.lblColnSLevel.NameOfControl = null;
            this.lblColnSLevel.Size = new System.Drawing.Size(12, 14);
            this.lblColnSLevel.TabIndex = 34;
            this.lblColnSLevel.Text = ":";
            // 
            // lblColnEmpPassword
            // 
            this.lblColnEmpPassword.AutoSize = true;
            this.lblColnEmpPassword.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblColnEmpPassword.Location = new System.Drawing.Point(272, 108);
            this.lblColnEmpPassword.Moveable = false;
            this.lblColnEmpPassword.Name = "lblColnEmpPassword";
            this.lblColnEmpPassword.NameOfControl = null;
            this.lblColnEmpPassword.Size = new System.Drawing.Size(12, 14);
            this.lblColnEmpPassword.TabIndex = 40;
            this.lblColnEmpPassword.Text = ":";
            // 
            // lblColnEmpName
            // 
            this.lblColnEmpName.AutoSize = true;
            this.lblColnEmpName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblColnEmpName.Location = new System.Drawing.Point(272, 83);
            this.lblColnEmpName.Moveable = false;
            this.lblColnEmpName.Name = "lblColnEmpName";
            this.lblColnEmpName.NameOfControl = null;
            this.lblColnEmpName.Size = new System.Drawing.Size(12, 14);
            this.lblColnEmpName.TabIndex = 39;
            this.lblColnEmpName.Text = ":";
            // 
            // txtEmpID
            // 
            this.txtEmpID.AutoFillDate = false;
            this.txtEmpID.BackColor = System.Drawing.Color.White;
            this.txtEmpID.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtEmpID.CheckForSymbol = null;
            this.txtEmpID.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtEmpID.DecimalPlace = 0;
            this.txtEmpID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtEmpID.HelpText = "Enter EmployeeID";
            this.txtEmpID.HoldMyText = null;
            this.txtEmpID.IsMandatory = false;
            this.txtEmpID.IsSingleQuote = true;
            this.txtEmpID.IsSysmbol = false;
            this.txtEmpID.Location = new System.Drawing.Point(289, 104);
            this.txtEmpID.Mask = null;
            this.txtEmpID.Moveable = false;
            this.txtEmpID.Name = "txtEmpID";
            this.txtEmpID.NameOfControl = null;
            this.txtEmpID.Prefix = null;
            this.txtEmpID.ShowBallonTip = false;
            this.txtEmpID.ShowErrorIcon = false;
            this.txtEmpID.ShowMessage = null;
            this.txtEmpID.Size = new System.Drawing.Size(200, 22);
            this.txtEmpID.Suffix = null;
            this.txtEmpID.TabIndex = 4;
            // 
            // txtEmployeeName
            // 
            this.txtEmployeeName.AutoFillDate = false;
            this.txtEmployeeName.BackColor = System.Drawing.Color.White;
            this.txtEmployeeName.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtEmployeeName.CheckForSymbol = null;
            this.txtEmployeeName.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtEmployeeName.DecimalPlace = 0;
            this.txtEmployeeName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtEmployeeName.HelpText = "Enter Employee  Name";
            this.txtEmployeeName.HoldMyText = null;
            this.txtEmployeeName.IsMandatory = false;
            this.txtEmployeeName.IsSingleQuote = true;
            this.txtEmployeeName.IsSysmbol = false;
            this.txtEmployeeName.Location = new System.Drawing.Point(289, 79);
            this.txtEmployeeName.Mask = null;
            this.txtEmployeeName.Moveable = false;
            this.txtEmployeeName.Name = "txtEmployeeName";
            this.txtEmployeeName.NameOfControl = null;
            this.txtEmployeeName.Prefix = null;
            this.txtEmployeeName.ShowBallonTip = false;
            this.txtEmployeeName.ShowErrorIcon = false;
            this.txtEmployeeName.ShowMessage = null;
            this.txtEmployeeName.Size = new System.Drawing.Size(200, 22);
            this.txtEmployeeName.Suffix = null;
            this.txtEmployeeName.TabIndex = 3;
            // 
            // lblEmpID
            // 
            this.lblEmpID.AutoSize = true;
            this.lblEmpID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmpID.Location = new System.Drawing.Point(149, 108);
            this.lblEmpID.Moveable = false;
            this.lblEmpID.Name = "lblEmpID";
            this.lblEmpID.NameOfControl = null;
            this.lblEmpID.Size = new System.Drawing.Size(91, 14);
            this.lblEmpID.TabIndex = 38;
            this.lblEmpID.Text = "Employee ID";
            // 
            // lblEmpName
            // 
            this.lblEmpName.AutoSize = true;
            this.lblEmpName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmpName.Location = new System.Drawing.Point(149, 82);
            this.lblEmpName.Moveable = false;
            this.lblEmpName.Name = "lblEmpName";
            this.lblEmpName.NameOfControl = null;
            this.lblEmpName.Size = new System.Drawing.Size(113, 14);
            this.lblEmpName.TabIndex = 37;
            this.lblEmpName.Text = "Employee Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(301, 263);
            this.label1.Moveable = false;
            this.label1.Name = "label1";
            this.label1.NameOfControl = null;
            this.label1.Size = new System.Drawing.Size(12, 14);
            this.label1.TabIndex = 43;
            this.label1.Text = ":";
            // 
            // txtGroupName
            // 
            this.txtGroupName.AutoFillDate = false;
            this.txtGroupName.BackColor = System.Drawing.Color.White;
            this.txtGroupName.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtGroupName.CheckForSymbol = null;
            this.txtGroupName.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtGroupName.DecimalPlace = 0;
            this.txtGroupName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtGroupName.HelpText = "Enter Company Group  Name";
            this.txtGroupName.HoldMyText = null;
            this.txtGroupName.IsMandatory = false;
            this.txtGroupName.IsSingleQuote = true;
            this.txtGroupName.IsSysmbol = false;
            this.txtGroupName.Location = new System.Drawing.Point(314, 261);
            this.txtGroupName.Mask = null;
            this.txtGroupName.Moveable = false;
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.NameOfControl = null;
            this.txtGroupName.Prefix = null;
            this.txtGroupName.ShowBallonTip = false;
            this.txtGroupName.ShowErrorIcon = false;
            this.txtGroupName.ShowMessage = null;
            this.txtGroupName.Size = new System.Drawing.Size(319, 22);
            this.txtGroupName.Suffix = null;
            this.txtGroupName.TabIndex = 11;
            // 
            // lblGroupName
            // 
            this.lblGroupName.AutoSize = true;
            this.lblGroupName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblGroupName.Location = new System.Drawing.Point(150, 263);
            this.lblGroupName.Moveable = false;
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.NameOfControl = null;
            this.lblGroupName.Size = new System.Drawing.Size(150, 14);
            this.lblGroupName.TabIndex = 42;
            this.lblGroupName.Text = "Company GroupName";
            // 
            // dgvCmpSelection
            // 
            this.dgvCmpSelection.AllowUserToAddRows = false;
            this.dgvCmpSelection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCmpSelection.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CompID,
            this.txtCompName,
            this.chkSelect});
            this.dgvCmpSelection.Location = new System.Drawing.Point(152, 292);
            this.dgvCmpSelection.Name = "dgvCmpSelection";
            this.dgvCmpSelection.RowHeadersVisible = false;
            this.dgvCmpSelection.Size = new System.Drawing.Size(481, 150);
            this.dgvCmpSelection.TabIndex = 12;
            // 
            // CompID
            // 
            this.CompID.HeaderText = "CompID";
            this.CompID.Name = "CompID";
            this.CompID.Visible = false;
            // 
            // txtCompName
            // 
            this.txtCompName.FillWeight = 380F;
            this.txtCompName.HeaderText = "Company";
            this.txtCompName.Name = "txtCompName";
            this.txtCompName.Width = 380;
            // 
            // chkSelect
            // 
            this.chkSelect.HeaderText = "Select";
            this.chkSelect.Name = "chkSelect";
            this.chkSelect.Width = 95;
            // 
            // txtImageData
            // 
            this.txtImageData.AutoFillDate = false;
            this.txtImageData.BackColor = System.Drawing.Color.White;
            this.txtImageData.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtImageData.CheckForSymbol = null;
            this.txtImageData.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtImageData.DecimalPlace = 0;
            this.txtImageData.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImageData.HelpText = "";
            this.txtImageData.HoldMyText = null;
            this.txtImageData.IsMandatory = false;
            this.txtImageData.IsSingleQuote = true;
            this.txtImageData.IsSysmbol = false;
            this.txtImageData.Location = new System.Drawing.Point(548, 205);
            this.txtImageData.Mask = null;
            this.txtImageData.Moveable = false;
            this.txtImageData.Name = "txtImageData";
            this.txtImageData.NameOfControl = null;
            this.txtImageData.Prefix = null;
            this.txtImageData.ReadOnly = true;
            this.txtImageData.ShowBallonTip = false;
            this.txtImageData.ShowErrorIcon = false;
            this.txtImageData.ShowMessage = null;
            this.txtImageData.Size = new System.Drawing.Size(111, 20);
            this.txtImageData.Suffix = null;
            this.txtImageData.TabIndex = 1140;
            this.txtImageData.Visible = false;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(566, 166);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(104, 32);
            this.btnBrowse.TabIndex = 13;
            this.btnBrowse.Text = "Browse...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // pctImage
            // 
            this.pctImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pctImage.Location = new System.Drawing.Point(548, 20);
            this.pctImage.Name = "pctImage";
            this.pctImage.Size = new System.Drawing.Size(140, 140);
            this.pctImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctImage.TabIndex = 1138;
            this.pctImage.TabStop = false;
            // 
            // txtImagePath
            // 
            this.txtImagePath.AutoFillDate = false;
            this.txtImagePath.BackColor = System.Drawing.Color.White;
            this.txtImagePath.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtImagePath.CheckForSymbol = null;
            this.txtImagePath.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtImagePath.DecimalPlace = 0;
            this.txtImagePath.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImagePath.HelpText = "";
            this.txtImagePath.HoldMyText = null;
            this.txtImagePath.IsMandatory = false;
            this.txtImagePath.IsSingleQuote = true;
            this.txtImagePath.IsSysmbol = false;
            this.txtImagePath.Location = new System.Drawing.Point(551, 174);
            this.txtImagePath.Mask = null;
            this.txtImagePath.Moveable = false;
            this.txtImagePath.Name = "txtImagePath";
            this.txtImagePath.NameOfControl = null;
            this.txtImagePath.Prefix = null;
            this.txtImagePath.ReadOnly = true;
            this.txtImagePath.ShowBallonTip = false;
            this.txtImagePath.ShowErrorIcon = false;
            this.txtImagePath.ShowMessage = null;
            this.txtImagePath.Size = new System.Drawing.Size(111, 20);
            this.txtImagePath.Suffix = null;
            this.txtImagePath.TabIndex = 1139;
            this.txtImagePath.Visible = false;
            // 
            // chkShowPanelStock
            // 
            this.chkShowPanelStock.AutoSize = true;
            this.chkShowPanelStock.BackColor = System.Drawing.Color.MintCream;
            this.chkShowPanelStock.Checked = true;
            this.chkShowPanelStock.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowPanelStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkShowPanelStock.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowPanelStock.HelpText = null;
            this.chkShowPanelStock.Location = new System.Drawing.Point(404, 158);
            this.chkShowPanelStock.Moveable = false;
            this.chkShowPanelStock.Name = "chkShowPanelStock";
            this.chkShowPanelStock.NameOfControl = null;
            this.chkShowPanelStock.Size = new System.Drawing.Size(101, 17);
            this.chkShowPanelStock.TabIndex = 7;
            this.chkShowPanelStock.Text = "Show Stock ";
            this.chkShowPanelStock.UseVisualStyleBackColor = true;
            // 
            // lblColMobile
            // 
            this.lblColMobile.AutoSize = true;
            this.lblColMobile.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblColMobile.Location = new System.Drawing.Point(272, 209);
            this.lblColMobile.Moveable = false;
            this.lblColMobile.Name = "lblColMobile";
            this.lblColMobile.NameOfControl = null;
            this.lblColMobile.Size = new System.Drawing.Size(12, 14);
            this.lblColMobile.TabIndex = 1144;
            this.lblColMobile.Text = ":";
            // 
            // txtMobileNo
            // 
            this.txtMobileNo.AutoFillDate = false;
            this.txtMobileNo.BackColor = System.Drawing.Color.White;
            this.txtMobileNo.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtMobileNo.CheckForSymbol = null;
            this.txtMobileNo.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtMobileNo.DecimalPlace = 0;
            this.txtMobileNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtMobileNo.HelpText = "Enter Mobile No";
            this.txtMobileNo.HoldMyText = null;
            this.txtMobileNo.IsMandatory = false;
            this.txtMobileNo.IsSingleQuote = true;
            this.txtMobileNo.IsSysmbol = false;
            this.txtMobileNo.Location = new System.Drawing.Point(289, 205);
            this.txtMobileNo.Mask = null;
            this.txtMobileNo.MaxLength = 15;
            this.txtMobileNo.Moveable = false;
            this.txtMobileNo.Name = "txtMobileNo";
            this.txtMobileNo.NameOfControl = null;
            this.txtMobileNo.Prefix = null;
            this.txtMobileNo.ShowBallonTip = false;
            this.txtMobileNo.ShowErrorIcon = false;
            this.txtMobileNo.ShowMessage = null;
            this.txtMobileNo.Size = new System.Drawing.Size(200, 22);
            this.txtMobileNo.Suffix = null;
            this.txtMobileNo.TabIndex = 9;
            // 
            // lblMobileNo
            // 
            this.lblMobileNo.AutoSize = true;
            this.lblMobileNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblMobileNo.Location = new System.Drawing.Point(149, 209);
            this.lblMobileNo.Moveable = false;
            this.lblMobileNo.Name = "lblMobileNo";
            this.lblMobileNo.NameOfControl = null;
            this.lblMobileNo.Size = new System.Drawing.Size(72, 14);
            this.lblMobileNo.TabIndex = 1143;
            this.lblMobileNo.Text = "Mobile No";
            // 
            // chkAutoGenPwd
            // 
            this.chkAutoGenPwd.AutoSize = true;
            this.chkAutoGenPwd.Checked = true;
            this.chkAutoGenPwd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoGenPwd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkAutoGenPwd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoGenPwd.HelpText = null;
            this.chkAutoGenPwd.Location = new System.Drawing.Point(289, 181);
            this.chkAutoGenPwd.Moveable = false;
            this.chkAutoGenPwd.Name = "chkAutoGenPwd";
            this.chkAutoGenPwd.NameOfControl = null;
            this.chkAutoGenPwd.Size = new System.Drawing.Size(119, 17);
            this.chkAutoGenPwd.TabIndex = 8;
            this.chkAutoGenPwd.Text = "Auto Password";
            this.chkAutoGenPwd.UseVisualStyleBackColor = true;
            this.chkAutoGenPwd.CheckedChanged += new System.EventHandler(this.chkAutoGenPwd_CheckedChanged);
            // 
            // lblColEmail
            // 
            this.lblColEmail.AutoSize = true;
            this.lblColEmail.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblColEmail.Location = new System.Drawing.Point(272, 234);
            this.lblColEmail.Moveable = false;
            this.lblColEmail.Name = "lblColEmail";
            this.lblColEmail.NameOfControl = null;
            this.lblColEmail.Size = new System.Drawing.Size(12, 14);
            this.lblColEmail.TabIndex = 1151;
            this.lblColEmail.Text = ":";
            // 
            // txtEmailID
            // 
            this.txtEmailID.AutoFillDate = false;
            this.txtEmailID.BackColor = System.Drawing.Color.White;
            this.txtEmailID.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtEmailID.CheckForSymbol = null;
            this.txtEmailID.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtEmailID.DecimalPlace = 0;
            this.txtEmailID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtEmailID.HelpText = "Enter EmailID";
            this.txtEmailID.HoldMyText = null;
            this.txtEmailID.IsMandatory = false;
            this.txtEmailID.IsSingleQuote = true;
            this.txtEmailID.IsSysmbol = false;
            this.txtEmailID.Location = new System.Drawing.Point(289, 230);
            this.txtEmailID.Mask = null;
            this.txtEmailID.MaxLength = 50;
            this.txtEmailID.Moveable = false;
            this.txtEmailID.Name = "txtEmailID";
            this.txtEmailID.NameOfControl = null;
            this.txtEmailID.Prefix = null;
            this.txtEmailID.ShowBallonTip = false;
            this.txtEmailID.ShowErrorIcon = false;
            this.txtEmailID.ShowMessage = null;
            this.txtEmailID.Size = new System.Drawing.Size(200, 22);
            this.txtEmailID.Suffix = null;
            this.txtEmailID.TabIndex = 10;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmail.Location = new System.Drawing.Point(149, 234);
            this.lblEmail.Moveable = false;
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.NameOfControl = null;
            this.lblEmail.Size = new System.Drawing.Size(63, 14);
            this.lblEmail.TabIndex = 1150;
            this.lblEmail.Text = "Email ID";
            // 
            // frmUserMaster
            // 
            this.ClientSize = new System.Drawing.Size(805, 547);
            this.Name = "frmUserMaster";
            this.Load += new System.EventHandler(this.frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCmpSelection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pctImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal CIS_CLibrary.CIS_CheckBox ChkActive;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox CboUserType;
        internal CIS_CLibrary.CIS_TextLabel lblUserType;
        internal CIS_CLibrary.CIS_Textbox txtPassword;
        internal CIS_CLibrary.CIS_Textbox txtUserName;
        public CIS_CLibrary.CIS_Textbox txtCode;
        internal CIS_CLibrary.CIS_TextLabel lblPassWord;
        internal CIS_CLibrary.CIS_TextLabel lblUserName;
        internal CIS_CLibrary.CIS_TextLabel lblColnSLevel;
        internal CIS_CLibrary.CIS_TextLabel lblColnPassword;
        internal CIS_CLibrary.CIS_TextLabel lblColnUserName;
        internal CIS_CLibrary.CIS_TextLabel lblColnEmpPassword;
        internal CIS_CLibrary.CIS_TextLabel lblColnEmpName;
        internal CIS_CLibrary.CIS_Textbox txtEmpID;
        internal CIS_CLibrary.CIS_Textbox txtEmployeeName;
        internal CIS_CLibrary.CIS_TextLabel lblEmpID;
        internal CIS_CLibrary.CIS_TextLabel lblEmpName;
        private System.Windows.Forms.DataGridView dgvCmpSelection;
        internal CIS_CLibrary.CIS_TextLabel label1;
        internal CIS_CLibrary.CIS_Textbox txtGroupName;
        internal CIS_CLibrary.CIS_TextLabel lblGroupName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompID;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtCompName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn chkSelect;
        private CIS_CLibrary.CIS_Textbox txtImageData;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.PictureBox pctImage;
        private CIS_CLibrary.CIS_Textbox txtImagePath;
        internal CIS_CLibrary.CIS_CheckBox chkShowPanelStock;
        internal CIS_CLibrary.CIS_CheckBox chkAutoGenPwd;
        internal CIS_CLibrary.CIS_TextLabel lblColMobile;
        internal CIS_CLibrary.CIS_Textbox txtMobileNo;
        internal CIS_CLibrary.CIS_TextLabel lblMobileNo;
        internal CIS_CLibrary.CIS_TextLabel lblColEmail;
        internal CIS_CLibrary.CIS_Textbox txtEmailID;
        internal CIS_CLibrary.CIS_TextLabel lblEmail;
    }
}
