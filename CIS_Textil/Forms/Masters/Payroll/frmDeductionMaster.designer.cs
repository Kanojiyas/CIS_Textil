namespace CIS_Textil
{
    partial class frmDeductionMaster
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
            CIS_CLibrary.ToolTip.StringDataProvider stringDataProvider1 = new CIS_CLibrary.ToolTip.StringDataProvider();
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.tltOnControls = new CIS_CLibrary.ToolTip.CIS_ToolTip();
            this.txtempAmt1 = new CIS_CLibrary.CIS_Textbox();
            this.txtEmpAmt = new CIS_CLibrary.CIS_Textbox();
            this.txtpaysheet = new CIS_CLibrary.CIS_Textbox();
            this.txtdeduction = new CIS_CLibrary.CIS_Textbox();
            this.ddltype = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.ChkActive = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.lblempAmt1Colon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblEmpAmtColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblpaysheetColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lbltypeColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblForm16HeadColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lbldeductionColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.rdbnone = new CIS_CLibrary.CIS_RadioButton();
            this.rdoSection = new CIS_CLibrary.CIS_RadioButton();
            this.rdbdeduction = new CIS_CLibrary.CIS_RadioButton();
            this.lblForm16Head = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblempAmt1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblpaysheet = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblEmpAmt = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lbltype = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lbldeduction = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.label1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtAliasName = new CIS_CLibrary.CIS_Textbox();
            this.label25 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblEI1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblEI1Colon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.cboEI1 = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblEI2 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.cboEI2 = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblEI2Colon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtET3 = new CIS_CLibrary.CIS_Textbox();
            this.lblET3Colon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtET2 = new CIS_CLibrary.CIS_Textbox();
            this.lblET2Colon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblET3 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblET2 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtET1 = new CIS_CLibrary.CIS_Textbox();
            this.lblET1Colon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblET1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.lblEI1);
            this.pnlContent.Controls.Add(this.label1);
            this.pnlContent.Controls.Add(this.lblEI1Colon);
            this.pnlContent.Controls.Add(this.txtAliasName);
            this.pnlContent.Controls.Add(this.cboEI1);
            this.pnlContent.Controls.Add(this.lblEI2);
            this.pnlContent.Controls.Add(this.label25);
            this.pnlContent.Controls.Add(this.cboEI2);
            this.pnlContent.Controls.Add(this.txtempAmt1);
            this.pnlContent.Controls.Add(this.lblEI2Colon);
            this.pnlContent.Controls.Add(this.txtEmpAmt);
            this.pnlContent.Controls.Add(this.txtET3);
            this.pnlContent.Controls.Add(this.txtpaysheet);
            this.pnlContent.Controls.Add(this.lblET3Colon);
            this.pnlContent.Controls.Add(this.txtdeduction);
            this.pnlContent.Controls.Add(this.txtET2);
            this.pnlContent.Controls.Add(this.ddltype);
            this.pnlContent.Controls.Add(this.lblET2Colon);
            this.pnlContent.Controls.Add(this.ChkActive);
            this.pnlContent.Controls.Add(this.lblET3);
            this.pnlContent.Controls.Add(this.lblempAmt1Colon);
            this.pnlContent.Controls.Add(this.lblET2);
            this.pnlContent.Controls.Add(this.lblEmpAmtColon);
            this.pnlContent.Controls.Add(this.txtET1);
            this.pnlContent.Controls.Add(this.lblpaysheetColon);
            this.pnlContent.Controls.Add(this.lblET1Colon);
            this.pnlContent.Controls.Add(this.lbltypeColon);
            this.pnlContent.Controls.Add(this.lblET1);
            this.pnlContent.Controls.Add(this.lblForm16HeadColon);
            this.pnlContent.Controls.Add(this.lbldeductionColon);
            this.pnlContent.Controls.Add(this.rdbnone);
            this.pnlContent.Controls.Add(this.rdoSection);
            this.pnlContent.Controls.Add(this.rdbdeduction);
            this.pnlContent.Controls.Add(this.lblForm16Head);
            this.pnlContent.Controls.Add(this.lblempAmt1);
            this.pnlContent.Controls.Add(this.lblpaysheet);
            this.pnlContent.Controls.Add(this.lblEmpAmt);
            this.pnlContent.Controls.Add(this.lbltype);
            this.pnlContent.Controls.Add(this.lbldeduction);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tltOnControls.SetToolTip(this.pnlContent, "");
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.lbldeduction, 0);
            this.pnlContent.Controls.SetChildIndex(this.lbltype, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEmpAmt, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblpaysheet, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblempAmt1, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblForm16Head, 0);
            this.pnlContent.Controls.SetChildIndex(this.rdbdeduction, 0);
            this.pnlContent.Controls.SetChildIndex(this.rdoSection, 0);
            this.pnlContent.Controls.SetChildIndex(this.rdbnone, 0);
            this.pnlContent.Controls.SetChildIndex(this.lbldeductionColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblForm16HeadColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET1, 0);
            this.pnlContent.Controls.SetChildIndex(this.lbltypeColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET1Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblpaysheetColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtET1, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEmpAmtColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET2, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblempAmt1Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET3, 0);
            this.pnlContent.Controls.SetChildIndex(this.ChkActive, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET2Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.ddltype, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtET2, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtdeduction, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET3Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtpaysheet, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtET3, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtEmpAmt, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEI2Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtempAmt1, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboEI2, 0);
            this.pnlContent.Controls.SetChildIndex(this.label25, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEI2, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboEI1, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtAliasName, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEI1Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.label1, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEI1, 0);
            // 
            // lblHelpText
            // 
            this.tltOnControls.SetToolTip(this.lblHelpText, "");
            // 
            // lblFormName
            // 
            this.tltOnControls.SetToolTip(this.lblFormName, "");
            // 
            // lblUUser
            // 
            this.tltOnControls.SetToolTip(this.lblUUser, "");
            // 
            // lblCUser
            // 
            this.tltOnControls.SetToolTip(this.lblCUser, "");
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
            this.txtCode.Location = new System.Drawing.Point(2, 0);
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
            this.tltOnControls.SetToolTip(this.txtCode, "");
            this.txtCode.Visible = false;
            // 
            // tltOnControls
            // 
            this.tltOnControls.DataProvider = stringDataProvider1;
            this.tltOnControls.LoadText = "";
            this.tltOnControls.ShowToolTip = false;
            // 
            // txtempAmt1
            // 
            this.txtempAmt1.AutoFillDate = false;
            this.txtempAmt1.BackColor = System.Drawing.Color.White;
            this.txtempAmt1.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtempAmt1.CheckForSymbol = null;
            this.txtempAmt1.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtempAmt1.DecimalPlace = 0;
            this.txtempAmt1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtempAmt1.HelpText = "Enter Employer Amount";
            this.txtempAmt1.HoldMyText = null;
            this.txtempAmt1.IsMandatory = false;
            this.txtempAmt1.IsSingleQuote = true;
            this.txtempAmt1.IsSysmbol = false;
            this.txtempAmt1.Location = new System.Drawing.Point(660, 64);
            this.txtempAmt1.Mask = null;
            this.txtempAmt1.MaxLength = 10;
            this.txtempAmt1.Moveable = false;
            this.txtempAmt1.Name = "txtempAmt1";
            this.txtempAmt1.NameOfControl = "Employer Amount";
            this.txtempAmt1.Prefix = null;
            this.txtempAmt1.ShowBallonTip = false;
            this.txtempAmt1.ShowErrorIcon = false;
            this.txtempAmt1.ShowMessage = null;
            this.txtempAmt1.Size = new System.Drawing.Size(84, 22);
            this.txtempAmt1.Suffix = null;
            this.txtempAmt1.TabIndex = 6;
            this.tltOnControls.SetToolTip(this.txtempAmt1, "");
            // 
            // txtEmpAmt
            // 
            this.txtEmpAmt.AutoFillDate = false;
            this.txtEmpAmt.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtEmpAmt.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtEmpAmt.CheckForSymbol = null;
            this.txtEmpAmt.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtEmpAmt.DecimalPlace = 0;
            this.txtEmpAmt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmpAmt.HelpText = "Enter Employee Amount";
            this.txtEmpAmt.HoldMyText = null;
            this.txtEmpAmt.IsMandatory = true;
            this.txtEmpAmt.IsSingleQuote = true;
            this.txtEmpAmt.IsSysmbol = false;
            this.txtEmpAmt.Location = new System.Drawing.Point(660, 40);
            this.txtEmpAmt.Mask = null;
            this.txtEmpAmt.MaxLength = 10;
            this.txtEmpAmt.Moveable = false;
            this.txtEmpAmt.Name = "txtEmpAmt";
            this.txtEmpAmt.NameOfControl = "Employee Amount";
            this.txtEmpAmt.Prefix = null;
            this.txtEmpAmt.ShowBallonTip = false;
            this.txtEmpAmt.ShowErrorIcon = false;
            this.txtEmpAmt.ShowMessage = null;
            this.txtEmpAmt.Size = new System.Drawing.Size(84, 22);
            this.txtEmpAmt.Suffix = null;
            this.txtEmpAmt.TabIndex = 4;
            this.tltOnControls.SetToolTip(this.txtEmpAmt, "");
            // 
            // txtpaysheet
            // 
            this.txtpaysheet.AutoFillDate = false;
            this.txtpaysheet.BackColor = System.Drawing.Color.White;
            this.txtpaysheet.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtpaysheet.CheckForSymbol = null;
            this.txtpaysheet.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtpaysheet.DecimalPlace = 0;
            this.txtpaysheet.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpaysheet.HelpText = "Enter Order No. In PaySheet";
            this.txtpaysheet.HoldMyText = null;
            this.txtpaysheet.IsMandatory = false;
            this.txtpaysheet.IsSingleQuote = true;
            this.txtpaysheet.IsSysmbol = false;
            this.txtpaysheet.Location = new System.Drawing.Point(230, 65);
            this.txtpaysheet.Mask = null;
            this.txtpaysheet.MaxLength = 10;
            this.txtpaysheet.Moveable = false;
            this.txtpaysheet.Name = "txtpaysheet";
            this.txtpaysheet.NameOfControl = "Order No. In Paysheet ";
            this.txtpaysheet.Prefix = null;
            this.txtpaysheet.ShowBallonTip = false;
            this.txtpaysheet.ShowErrorIcon = false;
            this.txtpaysheet.ShowMessage = null;
            this.txtpaysheet.Size = new System.Drawing.Size(84, 22);
            this.txtpaysheet.Suffix = null;
            this.txtpaysheet.TabIndex = 5;
            this.tltOnControls.SetToolTip(this.txtpaysheet, "");
            // 
            // txtdeduction
            // 
            this.txtdeduction.AutoFillDate = false;
            this.txtdeduction.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtdeduction.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtdeduction.CheckForSymbol = null;
            this.txtdeduction.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtdeduction.DecimalPlace = 0;
            this.txtdeduction.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtdeduction.HelpText = "Enter Deduction Name";
            this.txtdeduction.HoldMyText = null;
            this.txtdeduction.IsMandatory = true;
            this.txtdeduction.IsSingleQuote = true;
            this.txtdeduction.IsSysmbol = false;
            this.txtdeduction.Location = new System.Drawing.Point(230, 15);
            this.txtdeduction.Mask = null;
            this.txtdeduction.MaxLength = 50;
            this.txtdeduction.Moveable = false;
            this.txtdeduction.Name = "txtdeduction";
            this.txtdeduction.NameOfControl = "Deduction Name";
            this.txtdeduction.Prefix = null;
            this.txtdeduction.ShowBallonTip = false;
            this.txtdeduction.ShowErrorIcon = false;
            this.txtdeduction.ShowMessage = null;
            this.txtdeduction.Size = new System.Drawing.Size(212, 22);
            this.txtdeduction.Suffix = null;
            this.txtdeduction.TabIndex = 1;
            this.tltOnControls.SetToolTip(this.txtdeduction, "");
            this.txtdeduction.Leave += new System.EventHandler(this.txtdeduction_Leave);
            // 
            // ddltype
            // 
            this.ddltype.AutoComplete = false;
            this.ddltype.AutoDropdown = false;
            this.ddltype.BackColor = System.Drawing.Color.PapayaWhip;
            this.ddltype.BackColorEven = System.Drawing.Color.White;
            this.ddltype.BackColorOdd = System.Drawing.Color.White;
            this.ddltype.ColumnNames = "";
            this.ddltype.ColumnWidthDefault = 175;
            this.ddltype.ColumnWidths = "";
            this.ddltype.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ddltype.Fill_ComboID = 0;
            this.ddltype.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddltype.FormattingEnabled = true;
            this.ddltype.GroupType = 0;
            this.ddltype.HelpText = "Select Deduction Type";
            this.ddltype.IsMandatory = false;
            this.ddltype.LinkedColumnIndex = 0;
            this.ddltype.LinkedTextBox = null;
            this.ddltype.Location = new System.Drawing.Point(230, 39);
            this.ddltype.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ddltype.Moveable = false;
            this.ddltype.Name = "ddltype";
            this.ddltype.NameOfControl = "Type";
            this.ddltype.OpenForm = null;
            this.ddltype.ShowBallonTip = false;
            this.ddltype.Size = new System.Drawing.Size(134, 22);
            this.ddltype.TabIndex = 3;
            this.tltOnControls.SetToolTip(this.ddltype, "");
            // 
            // ChkActive
            // 
            this.ChkActive.AutoSize = true;
            this.ChkActive.BackColor = System.Drawing.Color.MintCream;
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChkActive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.HelpText = "Checked If Active";
            this.ChkActive.Location = new System.Drawing.Point(232, 116);
            this.ChkActive.Moveable = false;
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.NameOfControl = null;
            this.ChkActive.Size = new System.Drawing.Size(110, 18);
            this.ChkActive.TabIndex = 10;
            this.ChkActive.Text = "Active Status";
            this.tltOnControls.SetToolTip(this.ChkActive, "");
            this.ChkActive.UseVisualStyleBackColor = false;
            // 
            // lblempAmt1Colon
            // 
            this.lblempAmt1Colon.AutoSize = true;
            this.lblempAmt1Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblempAmt1Colon.Location = new System.Drawing.Point(642, 66);
            this.lblempAmt1Colon.Moveable = false;
            this.lblempAmt1Colon.Name = "lblempAmt1Colon";
            this.lblempAmt1Colon.NameOfControl = null;
            this.lblempAmt1Colon.Size = new System.Drawing.Size(12, 14);
            this.lblempAmt1Colon.TabIndex = 133;
            this.lblempAmt1Colon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblempAmt1Colon, "");
            // 
            // lblEmpAmtColon
            // 
            this.lblEmpAmtColon.AutoSize = true;
            this.lblEmpAmtColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpAmtColon.Location = new System.Drawing.Point(642, 42);
            this.lblEmpAmtColon.Moveable = false;
            this.lblEmpAmtColon.Name = "lblEmpAmtColon";
            this.lblEmpAmtColon.NameOfControl = null;
            this.lblEmpAmtColon.Size = new System.Drawing.Size(12, 14);
            this.lblEmpAmtColon.TabIndex = 132;
            this.lblEmpAmtColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblEmpAmtColon, "");
            // 
            // lblpaysheetColon
            // 
            this.lblpaysheetColon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblpaysheetColon.AutoSize = true;
            this.lblpaysheetColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpaysheetColon.Location = new System.Drawing.Point(213, 68);
            this.lblpaysheetColon.Moveable = false;
            this.lblpaysheetColon.Name = "lblpaysheetColon";
            this.lblpaysheetColon.NameOfControl = null;
            this.lblpaysheetColon.Size = new System.Drawing.Size(12, 14);
            this.lblpaysheetColon.TabIndex = 130;
            this.lblpaysheetColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblpaysheetColon, "");
            // 
            // lbltypeColon
            // 
            this.lbltypeColon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbltypeColon.AutoSize = true;
            this.lbltypeColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltypeColon.Location = new System.Drawing.Point(213, 42);
            this.lbltypeColon.Moveable = false;
            this.lbltypeColon.Name = "lbltypeColon";
            this.lbltypeColon.NameOfControl = null;
            this.lbltypeColon.Size = new System.Drawing.Size(12, 14);
            this.lbltypeColon.TabIndex = 129;
            this.lbltypeColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lbltypeColon, "");
            // 
            // lblForm16HeadColon
            // 
            this.lblForm16HeadColon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblForm16HeadColon.AutoSize = true;
            this.lblForm16HeadColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForm16HeadColon.Location = new System.Drawing.Point(213, 92);
            this.lblForm16HeadColon.Moveable = false;
            this.lblForm16HeadColon.Name = "lblForm16HeadColon";
            this.lblForm16HeadColon.NameOfControl = null;
            this.lblForm16HeadColon.Size = new System.Drawing.Size(12, 14);
            this.lblForm16HeadColon.TabIndex = 128;
            this.lblForm16HeadColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblForm16HeadColon, "");
            // 
            // lbldeductionColon
            // 
            this.lbldeductionColon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbldeductionColon.AutoSize = true;
            this.lbldeductionColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldeductionColon.Location = new System.Drawing.Point(213, 18);
            this.lbldeductionColon.Moveable = false;
            this.lbldeductionColon.Name = "lbldeductionColon";
            this.lbldeductionColon.NameOfControl = null;
            this.lbldeductionColon.Size = new System.Drawing.Size(12, 14);
            this.lbldeductionColon.TabIndex = 127;
            this.lbldeductionColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lbldeductionColon, "");
            // 
            // rdbnone
            // 
            this.rdbnone.AutoSize = true;
            this.rdbnone.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbnone.HelpText = "Check If None";
            this.rdbnone.Location = new System.Drawing.Point(493, 92);
            this.rdbnone.Moveable = false;
            this.rdbnone.Name = "rdbnone";
            this.rdbnone.Size = new System.Drawing.Size(71, 18);
            this.rdbnone.TabIndex = 9;
            this.rdbnone.TabStop = true;
            this.rdbnone.Text = "None   ";
            this.tltOnControls.SetToolTip(this.rdbnone, "");
            this.rdbnone.UseVisualStyleBackColor = true;
            // 
            // rdoSection
            // 
            this.rdoSection.AutoSize = true;
            this.rdoSection.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdoSection.HelpText = "Check If Section 80 C";
            this.rdoSection.Location = new System.Drawing.Point(382, 92);
            this.rdoSection.Moveable = false;
            this.rdoSection.Name = "rdoSection";
            this.rdoSection.Size = new System.Drawing.Size(105, 18);
            this.rdoSection.TabIndex = 8;
            this.rdoSection.TabStop = true;
            this.rdoSection.Text = "Section 80C";
            this.tltOnControls.SetToolTip(this.rdoSection, "");
            this.rdoSection.UseVisualStyleBackColor = true;
            // 
            // rdbdeduction
            // 
            this.rdbdeduction.AutoSize = true;
            this.rdbdeduction.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbdeduction.HelpText = "Check If Deduction U/S 10";
            this.rdbdeduction.Location = new System.Drawing.Point(232, 92);
            this.rdbdeduction.Moveable = false;
            this.rdbdeduction.Name = "rdbdeduction";
            this.rdbdeduction.Size = new System.Drawing.Size(144, 18);
            this.rdbdeduction.TabIndex = 7;
            this.rdbdeduction.TabStop = true;
            this.rdbdeduction.Text = "Deduction U/S 10";
            this.tltOnControls.SetToolTip(this.rdbdeduction, "");
            this.rdbdeduction.UseVisualStyleBackColor = true;
            // 
            // lblForm16Head
            // 
            this.lblForm16Head.AutoSize = true;
            this.lblForm16Head.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForm16Head.Location = new System.Drawing.Point(58, 93);
            this.lblForm16Head.Moveable = false;
            this.lblForm16Head.Name = "lblForm16Head";
            this.lblForm16Head.NameOfControl = null;
            this.lblForm16Head.Size = new System.Drawing.Size(113, 14);
            this.lblForm16Head.TabIndex = 126;
            this.lblForm16Head.Text = "Form 16 Head   ";
            this.tltOnControls.SetToolTip(this.lblForm16Head, "");
            // 
            // lblempAmt1
            // 
            this.lblempAmt1.AutoSize = true;
            this.lblempAmt1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblempAmt1.Location = new System.Drawing.Point(486, 68);
            this.lblempAmt1.Moveable = false;
            this.lblempAmt1.Name = "lblempAmt1";
            this.lblempAmt1.NameOfControl = null;
            this.lblempAmt1.Size = new System.Drawing.Size(123, 14);
            this.lblempAmt1.TabIndex = 125;
            this.lblempAmt1.Text = "Employer Amount";
            this.tltOnControls.SetToolTip(this.lblempAmt1, "");
            // 
            // lblpaysheet
            // 
            this.lblpaysheet.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblpaysheet.AutoSize = true;
            this.lblpaysheet.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpaysheet.Location = new System.Drawing.Point(58, 68);
            this.lblpaysheet.Moveable = false;
            this.lblpaysheet.Name = "lblpaysheet";
            this.lblpaysheet.NameOfControl = null;
            this.lblpaysheet.Size = new System.Drawing.Size(155, 14);
            this.lblpaysheet.TabIndex = 124;
            this.lblpaysheet.Text = "Order No. In Paysheet";
            this.tltOnControls.SetToolTip(this.lblpaysheet, "");
            // 
            // lblEmpAmt
            // 
            this.lblEmpAmt.AutoSize = true;
            this.lblEmpAmt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmpAmt.Location = new System.Drawing.Point(486, 42);
            this.lblEmpAmt.Moveable = false;
            this.lblEmpAmt.Name = "lblEmpAmt";
            this.lblEmpAmt.NameOfControl = null;
            this.lblEmpAmt.Size = new System.Drawing.Size(125, 14);
            this.lblEmpAmt.TabIndex = 123;
            this.lblEmpAmt.Text = "Employee Amount";
            this.tltOnControls.SetToolTip(this.lblEmpAmt, "");
            // 
            // lbltype
            // 
            this.lbltype.AutoSize = true;
            this.lbltype.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltype.Location = new System.Drawing.Point(58, 43);
            this.lbltype.Moveable = false;
            this.lbltype.Name = "lbltype";
            this.lbltype.NameOfControl = null;
            this.lbltype.Size = new System.Drawing.Size(39, 14);
            this.lbltype.TabIndex = 122;
            this.lbltype.Text = "Type";
            this.tltOnControls.SetToolTip(this.lbltype, "");
            // 
            // lbldeduction
            // 
            this.lbldeduction.AutoSize = true;
            this.lbldeduction.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbldeduction.Location = new System.Drawing.Point(58, 18);
            this.lbldeduction.Moveable = false;
            this.lbldeduction.Name = "lbldeduction";
            this.lbldeduction.NameOfControl = null;
            this.lbldeduction.Size = new System.Drawing.Size(115, 14);
            this.lbldeduction.TabIndex = 120;
            this.lbldeduction.Text = "Deduction Name";
            this.tltOnControls.SetToolTip(this.lbldeduction, "");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(641, 18);
            this.label1.Moveable = false;
            this.label1.Name = "label1";
            this.label1.NameOfControl = null;
            this.label1.Size = new System.Drawing.Size(12, 14);
            this.label1.TabIndex = 1157;
            this.label1.Text = ":";
            this.tltOnControls.SetToolTip(this.label1, "");
            // 
            // txtAliasName
            // 
            this.txtAliasName.AutoFillDate = false;
            this.txtAliasName.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtAliasName.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtAliasName.CheckForSymbol = null;
            this.txtAliasName.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtAliasName.DecimalPlace = 0;
            this.txtAliasName.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAliasName.HelpText = "Enter Beam Design";
            this.txtAliasName.HoldMyText = null;
            this.txtAliasName.IsMandatory = false;
            this.txtAliasName.IsSingleQuote = true;
            this.txtAliasName.IsSysmbol = false;
            this.txtAliasName.Location = new System.Drawing.Point(660, 15);
            this.txtAliasName.Mask = null;
            this.txtAliasName.MaxLength = 50;
            this.txtAliasName.Moveable = false;
            this.txtAliasName.Name = "txtAliasName";
            this.txtAliasName.NameOfControl = "Alias name";
            this.txtAliasName.Prefix = null;
            this.txtAliasName.ShowBallonTip = false;
            this.txtAliasName.ShowErrorIcon = false;
            this.txtAliasName.ShowMessage = null;
            this.txtAliasName.Size = new System.Drawing.Size(84, 21);
            this.txtAliasName.Suffix = null;
            this.txtAliasName.TabIndex = 2;
            this.tltOnControls.SetToolTip(this.txtAliasName, "");
            this.txtAliasName.Leave += new System.EventHandler(this.txtAliasName_Leave);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(486, 18);
            this.label25.Moveable = false;
            this.label25.Name = "label25";
            this.label25.NameOfControl = null;
            this.label25.Size = new System.Drawing.Size(81, 14);
            this.label25.TabIndex = 1156;
            this.label25.Text = "Alias Name";
            this.tltOnControls.SetToolTip(this.label25, "");
            // 
            // lblEI1
            // 
            this.lblEI1.AutoSize = true;
            this.lblEI1.BackColor = System.Drawing.Color.Transparent;
            this.lblEI1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI1.Location = new System.Drawing.Point(127, 144);
            this.lblEI1.Moveable = false;
            this.lblEI1.Name = "lblEI1";
            this.lblEI1.NameOfControl = "EI1";
            this.lblEI1.Size = new System.Drawing.Size(30, 14);
            this.lblEI1.TabIndex = 1354;
            this.lblEI1.Text = "EI1";
            this.tltOnControls.SetToolTip(this.lblEI1, "");
            this.lblEI1.Visible = false;
            // 
            // lblEI1Colon
            // 
            this.lblEI1Colon.AutoSize = true;
            this.lblEI1Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblEI1Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI1Colon.Location = new System.Drawing.Point(213, 144);
            this.lblEI1Colon.Moveable = false;
            this.lblEI1Colon.Name = "lblEI1Colon";
            this.lblEI1Colon.NameOfControl = "EI1";
            this.lblEI1Colon.Size = new System.Drawing.Size(12, 14);
            this.lblEI1Colon.TabIndex = 1355;
            this.lblEI1Colon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblEI1Colon, "");
            this.lblEI1Colon.Visible = false;
            // 
            // cboEI1
            // 
            this.cboEI1.AutoComplete = false;
            this.cboEI1.AutoDropdown = false;
            this.cboEI1.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboEI1.BackColorEven = System.Drawing.Color.White;
            this.cboEI1.BackColorOdd = System.Drawing.Color.White;
            this.cboEI1.ColumnNames = "";
            this.cboEI1.ColumnWidthDefault = 175;
            this.cboEI1.ColumnWidths = "";
            this.cboEI1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboEI1.Fill_ComboID = 0;
            this.cboEI1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEI1.FormattingEnabled = true;
            this.cboEI1.GroupType = 0;
            this.cboEI1.HelpText = "Select EI1";
            this.cboEI1.IsMandatory = true;
            this.cboEI1.LinkedColumnIndex = 0;
            this.cboEI1.LinkedTextBox = null;
            this.cboEI1.Location = new System.Drawing.Point(230, 140);
            this.cboEI1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboEI1.Moveable = false;
            this.cboEI1.Name = "cboEI1";
            this.cboEI1.NameOfControl = "EI1";
            this.cboEI1.OpenForm = null;
            this.cboEI1.ShowBallonTip = false;
            this.cboEI1.Size = new System.Drawing.Size(231, 23);
            this.cboEI1.TabIndex = 1353;
            this.tltOnControls.SetToolTip(this.cboEI1, "");
            this.cboEI1.Visible = false;
            // 
            // lblEI2
            // 
            this.lblEI2.AutoSize = true;
            this.lblEI2.BackColor = System.Drawing.Color.Transparent;
            this.lblEI2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI2.Location = new System.Drawing.Point(127, 168);
            this.lblEI2.Moveable = false;
            this.lblEI2.Name = "lblEI2";
            this.lblEI2.NameOfControl = "EI2";
            this.lblEI2.Size = new System.Drawing.Size(30, 14);
            this.lblEI2.TabIndex = 1351;
            this.lblEI2.Text = "EI2";
            this.tltOnControls.SetToolTip(this.lblEI2, "");
            this.lblEI2.Visible = false;
            // 
            // cboEI2
            // 
            this.cboEI2.AutoComplete = true;
            this.cboEI2.AutoDropdown = true;
            this.cboEI2.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboEI2.BackColorEven = System.Drawing.Color.White;
            this.cboEI2.BackColorOdd = System.Drawing.Color.White;
            this.cboEI2.ColumnNames = "";
            this.cboEI2.ColumnWidthDefault = 175;
            this.cboEI2.ColumnWidths = "";
            this.cboEI2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboEI2.Fill_ComboID = 0;
            this.cboEI2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.cboEI2.FormattingEnabled = true;
            this.cboEI2.GroupType = 0;
            this.cboEI2.HelpText = "Select EI2";
            this.cboEI2.IsMandatory = true;
            this.cboEI2.LinkedColumnIndex = 0;
            this.cboEI2.LinkedTextBox = null;
            this.cboEI2.Location = new System.Drawing.Point(230, 166);
            this.cboEI2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboEI2.Moveable = false;
            this.cboEI2.Name = "cboEI2";
            this.cboEI2.NameOfControl = "EI2";
            this.cboEI2.OpenForm = null;
            this.cboEI2.ShowBallonTip = false;
            this.cboEI2.Size = new System.Drawing.Size(231, 23);
            this.cboEI2.TabIndex = 1350;
            this.tltOnControls.SetToolTip(this.cboEI2, "");
            this.cboEI2.Visible = false;
            // 
            // lblEI2Colon
            // 
            this.lblEI2Colon.AutoSize = true;
            this.lblEI2Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblEI2Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI2Colon.Location = new System.Drawing.Point(213, 168);
            this.lblEI2Colon.Moveable = false;
            this.lblEI2Colon.Name = "lblEI2Colon";
            this.lblEI2Colon.NameOfControl = null;
            this.lblEI2Colon.Size = new System.Drawing.Size(12, 14);
            this.lblEI2Colon.TabIndex = 1352;
            this.lblEI2Colon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblEI2Colon, "");
            this.lblEI2Colon.Visible = false;
            // 
            // txtET3
            // 
            this.txtET3.AutoFillDate = false;
            this.txtET3.BackColor = System.Drawing.Color.White;
            this.txtET3.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtET3.CheckForSymbol = null;
            this.txtET3.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtET3.DecimalPlace = 0;
            this.txtET3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtET3.HelpText = "Enter ET3";
            this.txtET3.HoldMyText = null;
            this.txtET3.IsMandatory = false;
            this.txtET3.IsSingleQuote = true;
            this.txtET3.IsSysmbol = false;
            this.txtET3.Location = new System.Drawing.Point(230, 244);
            this.txtET3.Mask = null;
            this.txtET3.MaxLength = 50;
            this.txtET3.Moveable = false;
            this.txtET3.Name = "txtET3";
            this.txtET3.NameOfControl = "ET3";
            this.txtET3.Prefix = null;
            this.txtET3.ShowBallonTip = false;
            this.txtET3.ShowErrorIcon = false;
            this.txtET3.ShowMessage = null;
            this.txtET3.Size = new System.Drawing.Size(231, 22);
            this.txtET3.Suffix = null;
            this.txtET3.TabIndex = 1343;
            this.tltOnControls.SetToolTip(this.txtET3, "");
            this.txtET3.Visible = false;
            // 
            // lblET3Colon
            // 
            this.lblET3Colon.AutoSize = true;
            this.lblET3Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblET3Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET3Colon.Location = new System.Drawing.Point(213, 247);
            this.lblET3Colon.Moveable = false;
            this.lblET3Colon.Name = "lblET3Colon";
            this.lblET3Colon.NameOfControl = null;
            this.lblET3Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET3Colon.TabIndex = 1349;
            this.lblET3Colon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblET3Colon, "");
            this.lblET3Colon.Visible = false;
            // 
            // txtET2
            // 
            this.txtET2.AutoFillDate = false;
            this.txtET2.BackColor = System.Drawing.Color.White;
            this.txtET2.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtET2.CheckForSymbol = null;
            this.txtET2.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtET2.DecimalPlace = 0;
            this.txtET2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtET2.HelpText = "Enter ET2";
            this.txtET2.HoldMyText = null;
            this.txtET2.IsMandatory = false;
            this.txtET2.IsSingleQuote = true;
            this.txtET2.IsSysmbol = false;
            this.txtET2.Location = new System.Drawing.Point(230, 217);
            this.txtET2.Mask = null;
            this.txtET2.MaxLength = 50;
            this.txtET2.Moveable = false;
            this.txtET2.Name = "txtET2";
            this.txtET2.NameOfControl = "ET2";
            this.txtET2.Prefix = null;
            this.txtET2.ShowBallonTip = false;
            this.txtET2.ShowErrorIcon = false;
            this.txtET2.ShowMessage = null;
            this.txtET2.Size = new System.Drawing.Size(231, 22);
            this.txtET2.Suffix = null;
            this.txtET2.TabIndex = 1342;
            this.tltOnControls.SetToolTip(this.txtET2, "");
            this.txtET2.Visible = false;
            // 
            // lblET2Colon
            // 
            this.lblET2Colon.AutoSize = true;
            this.lblET2Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblET2Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET2Colon.Location = new System.Drawing.Point(213, 222);
            this.lblET2Colon.Moveable = false;
            this.lblET2Colon.Name = "lblET2Colon";
            this.lblET2Colon.NameOfControl = "ET2";
            this.lblET2Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET2Colon.TabIndex = 1347;
            this.lblET2Colon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblET2Colon, "");
            this.lblET2Colon.Visible = false;
            // 
            // lblET3
            // 
            this.lblET3.AutoSize = true;
            this.lblET3.BackColor = System.Drawing.Color.Transparent;
            this.lblET3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET3.Location = new System.Drawing.Point(126, 246);
            this.lblET3.Moveable = false;
            this.lblET3.Name = "lblET3";
            this.lblET3.NameOfControl = "ET3";
            this.lblET3.Size = new System.Drawing.Size(32, 14);
            this.lblET3.TabIndex = 1348;
            this.lblET3.Text = "ET3";
            this.tltOnControls.SetToolTip(this.lblET3, "");
            this.lblET3.Visible = false;
            // 
            // lblET2
            // 
            this.lblET2.AutoSize = true;
            this.lblET2.BackColor = System.Drawing.Color.Transparent;
            this.lblET2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET2.Location = new System.Drawing.Point(126, 220);
            this.lblET2.Moveable = false;
            this.lblET2.Name = "lblET2";
            this.lblET2.NameOfControl = "ET2";
            this.lblET2.Size = new System.Drawing.Size(32, 14);
            this.lblET2.TabIndex = 1346;
            this.lblET2.Text = "ET2";
            this.tltOnControls.SetToolTip(this.lblET2, "");
            this.lblET2.Visible = false;
            // 
            // txtET1
            // 
            this.txtET1.AutoFillDate = false;
            this.txtET1.BackColor = System.Drawing.Color.White;
            this.txtET1.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtET1.CheckForSymbol = null;
            this.txtET1.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtET1.DecimalPlace = 0;
            this.txtET1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtET1.HelpText = "Enter ET1";
            this.txtET1.HoldMyText = null;
            this.txtET1.IsMandatory = false;
            this.txtET1.IsSingleQuote = true;
            this.txtET1.IsSysmbol = false;
            this.txtET1.Location = new System.Drawing.Point(230, 192);
            this.txtET1.Mask = null;
            this.txtET1.MaxLength = 50;
            this.txtET1.Moveable = false;
            this.txtET1.Name = "txtET1";
            this.txtET1.NameOfControl = "ET1";
            this.txtET1.Prefix = null;
            this.txtET1.ShowBallonTip = false;
            this.txtET1.ShowErrorIcon = false;
            this.txtET1.ShowMessage = null;
            this.txtET1.Size = new System.Drawing.Size(231, 22);
            this.txtET1.Suffix = null;
            this.txtET1.TabIndex = 1341;
            this.tltOnControls.SetToolTip(this.txtET1, "");
            this.txtET1.Visible = false;
            // 
            // lblET1Colon
            // 
            this.lblET1Colon.AutoSize = true;
            this.lblET1Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblET1Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET1Colon.Location = new System.Drawing.Point(213, 194);
            this.lblET1Colon.Moveable = false;
            this.lblET1Colon.Name = "lblET1Colon";
            this.lblET1Colon.NameOfControl = "ET1";
            this.lblET1Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET1Colon.TabIndex = 1345;
            this.lblET1Colon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblET1Colon, "");
            this.lblET1Colon.Visible = false;
            // 
            // lblET1
            // 
            this.lblET1.AutoSize = true;
            this.lblET1.BackColor = System.Drawing.Color.Transparent;
            this.lblET1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET1.Location = new System.Drawing.Point(126, 194);
            this.lblET1.Moveable = false;
            this.lblET1.Name = "lblET1";
            this.lblET1.NameOfControl = "ET1";
            this.lblET1.Size = new System.Drawing.Size(32, 14);
            this.lblET1.TabIndex = 1344;
            this.lblET1.Text = "ET1";
            this.tltOnControls.SetToolTip(this.lblET1, "");
            this.lblET1.Visible = false;
            // 
            // frmDeductionMaster
            // 
            this.ClientSize = new System.Drawing.Size(808, 545);
            this.Name = "frmDeductionMaster";
            this.Load += new System.EventHandler(this.frmDeductionMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public CIS_CLibrary.CIS_Textbox txtCode;
        private CIS_CLibrary.ToolTip.CIS_ToolTip tltOnControls;
        private CIS_CLibrary.CIS_Textbox txtempAmt1;
        private CIS_CLibrary.CIS_Textbox txtEmpAmt;
        private CIS_CLibrary.CIS_Textbox txtpaysheet;
        private CIS_CLibrary.CIS_Textbox txtdeduction;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox ddltype;
        internal CIS_CLibrary.CIS_CheckBox ChkActive;
        private CIS_CLibrary.CIS_TextLabel lblempAmt1Colon;
        private CIS_CLibrary.CIS_TextLabel lblEmpAmtColon;
        private CIS_CLibrary.CIS_TextLabel lblpaysheetColon;
        private CIS_CLibrary.CIS_TextLabel lbltypeColon;
        private CIS_CLibrary.CIS_TextLabel lblForm16HeadColon;
        private CIS_CLibrary.CIS_TextLabel lbldeductionColon;
        private CIS_CLibrary.CIS_RadioButton rdbnone;
        private CIS_CLibrary.CIS_RadioButton rdoSection;
        private CIS_CLibrary.CIS_RadioButton rdbdeduction;
        private CIS_CLibrary.CIS_TextLabel lblForm16Head;
        private CIS_CLibrary.CIS_TextLabel lblempAmt1;
        private CIS_CLibrary.CIS_TextLabel lblpaysheet;
        private CIS_CLibrary.CIS_TextLabel lblEmpAmt;
        private CIS_CLibrary.CIS_TextLabel lbltype;
        private CIS_CLibrary.CIS_TextLabel lbldeduction;
        internal CIS_CLibrary.CIS_TextLabel label1;
        internal CIS_CLibrary.CIS_Textbox txtAliasName;
        internal CIS_CLibrary.CIS_TextLabel label25;
        internal CIS_CLibrary.CIS_TextLabel lblEI1;
        internal CIS_CLibrary.CIS_TextLabel lblEI1Colon;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboEI1;
        internal CIS_CLibrary.CIS_TextLabel lblEI2;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboEI2;
        internal CIS_CLibrary.CIS_TextLabel lblEI2Colon;
        internal CIS_CLibrary.CIS_Textbox txtET3;
        internal CIS_CLibrary.CIS_TextLabel lblET3Colon;
        internal CIS_CLibrary.CIS_Textbox txtET2;
        internal CIS_CLibrary.CIS_TextLabel lblET2Colon;
        internal CIS_CLibrary.CIS_TextLabel lblET3;
        internal CIS_CLibrary.CIS_TextLabel lblET2;
        internal CIS_CLibrary.CIS_Textbox txtET1;
        internal CIS_CLibrary.CIS_TextLabel lblET1Colon;
        internal CIS_CLibrary.CIS_TextLabel lblET1;

    }
}
