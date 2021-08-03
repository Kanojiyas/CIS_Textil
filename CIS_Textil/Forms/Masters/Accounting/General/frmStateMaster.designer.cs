namespace CIS_Textil
{
    partial class frmStateMaster
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
            this.txtstatename = new CIS_CLibrary.CIS_Textbox();
            this.lblCityNameColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblStatename = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.ChkActive = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.label2 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblAliasName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtAliasName = new CIS_CLibrary.CIS_Textbox();
            this.lblCountry = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblWeaverColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblRegName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtRegName = new CIS_CLibrary.CIS_Textbox();
            this.ciS_TextLabel2 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.cboCountry = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
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
            this.pnlContent.Controls.Add(this.cboCountry);
            this.pnlContent.Controls.Add(this.lblRegName);
            this.pnlContent.Controls.Add(this.txtRegName);
            this.pnlContent.Controls.Add(this.ciS_TextLabel2);
            this.pnlContent.Controls.Add(this.lblCountry);
            this.pnlContent.Controls.Add(this.lblWeaverColon);
            this.pnlContent.Controls.Add(this.label2);
            this.pnlContent.Controls.Add(this.lblAliasName);
            this.pnlContent.Controls.Add(this.txtAliasName);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.Add(this.ChkActive);
            this.pnlContent.Controls.Add(this.lblStatename);
            this.pnlContent.Controls.Add(this.txtstatename);
            this.pnlContent.Controls.Add(this.lblCityNameColon);
            this.pnlContent.Controls.SetChildIndex(this.lblCityNameColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtstatename, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblStatename, 0);
            this.pnlContent.Controls.SetChildIndex(this.ChkActive, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtAliasName, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblAliasName, 0);
            this.pnlContent.Controls.SetChildIndex(this.label2, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblWeaverColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblCountry, 0);
            this.pnlContent.Controls.SetChildIndex(this.ciS_TextLabel2, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtRegName, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblRegName, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboCountry, 0);
            // 
            // txtstatename
            // 
            this.txtstatename.AutoFillDate = false;
            this.txtstatename.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtstatename.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtstatename.CheckForSymbol = null;
            this.txtstatename.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtstatename.DecimalPlace = 0;
            this.txtstatename.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtstatename.HelpText = "Enter State Name";
            this.txtstatename.HoldMyText = null;
            this.txtstatename.IsMandatory = true;
            this.txtstatename.IsSingleQuote = true;
            this.txtstatename.IsSysmbol = false;
            this.txtstatename.Location = new System.Drawing.Point(361, 16);
            this.txtstatename.Mask = null;
            this.txtstatename.MaxLength = 50;
            this.txtstatename.Moveable = false;
            this.txtstatename.Name = "txtstatename";
            this.txtstatename.NameOfControl = "State name";
            this.txtstatename.Prefix = null;
            this.txtstatename.ShowBallonTip = false;
            this.txtstatename.ShowErrorIcon = false;
            this.txtstatename.ShowMessage = null;
            this.txtstatename.Size = new System.Drawing.Size(231, 22);
            this.txtstatename.Suffix = null;
            this.txtstatename.TabIndex = 1;
            this.txtstatename.Leave += new System.EventHandler(this.txtstatename_Leave);
            // 
            // lblCityNameColon
            // 
            this.lblCityNameColon.AutoSize = true;
            this.lblCityNameColon.BackColor = System.Drawing.Color.Transparent;
            this.lblCityNameColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCityNameColon.Location = new System.Drawing.Point(346, 18);
            this.lblCityNameColon.Moveable = false;
            this.lblCityNameColon.Name = "lblCityNameColon";
            this.lblCityNameColon.NameOfControl = null;
            this.lblCityNameColon.Size = new System.Drawing.Size(12, 14);
            this.lblCityNameColon.TabIndex = 1027;
            this.lblCityNameColon.Text = ":";
            // 
            // lblStatename
            // 
            this.lblStatename.AutoSize = true;
            this.lblStatename.BackColor = System.Drawing.Color.Transparent;
            this.lblStatename.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatename.Location = new System.Drawing.Point(230, 18);
            this.lblStatename.Moveable = false;
            this.lblStatename.Name = "lblStatename";
            this.lblStatename.NameOfControl = null;
            this.lblStatename.Size = new System.Drawing.Size(84, 14);
            this.lblStatename.TabIndex = 1026;
            this.lblStatename.Text = "State Name";
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
            this.ChkActive.Location = new System.Drawing.Point(362, 119);
            this.ChkActive.Moveable = false;
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.NameOfControl = null;
            this.ChkActive.Size = new System.Drawing.Size(110, 18);
            this.ChkActive.TabIndex = 5;
            this.ChkActive.Text = "Active Status";
            this.ChkActive.UseVisualStyleBackColor = false;
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
            this.txtCode.Location = new System.Drawing.Point(3, 0);
            this.txtCode.Mask = null;
            this.txtCode.Moveable = false;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(30, 22);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 1031;
            this.txtCode.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(346, 68);
            this.label2.Moveable = false;
            this.label2.Name = "label2";
            this.label2.NameOfControl = null;
            this.label2.Size = new System.Drawing.Size(12, 14);
            this.label2.TabIndex = 1038;
            this.label2.Text = ":";
            // 
            // lblAliasName
            // 
            this.lblAliasName.AutoSize = true;
            this.lblAliasName.BackColor = System.Drawing.Color.Transparent;
            this.lblAliasName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAliasName.Location = new System.Drawing.Point(230, 71);
            this.lblAliasName.Moveable = false;
            this.lblAliasName.Name = "lblAliasName";
            this.lblAliasName.NameOfControl = null;
            this.lblAliasName.Size = new System.Drawing.Size(81, 14);
            this.lblAliasName.TabIndex = 1037;
            this.lblAliasName.Text = "Alias Name";
            // 
            // txtAliasName
            // 
            this.txtAliasName.AutoFillDate = false;
            this.txtAliasName.BackColor = System.Drawing.Color.White;
            this.txtAliasName.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtAliasName.CheckForSymbol = null;
            this.txtAliasName.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtAliasName.DecimalPlace = 0;
            this.txtAliasName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAliasName.HelpText = "Enter Alias Name";
            this.txtAliasName.HoldMyText = null;
            this.txtAliasName.IsMandatory = false;
            this.txtAliasName.IsSingleQuote = true;
            this.txtAliasName.IsSysmbol = false;
            this.txtAliasName.Location = new System.Drawing.Point(361, 68);
            this.txtAliasName.Mask = null;
            this.txtAliasName.MaxLength = 50;
            this.txtAliasName.Moveable = false;
            this.txtAliasName.Name = "txtAliasName";
            this.txtAliasName.NameOfControl = "Alias name";
            this.txtAliasName.Prefix = null;
            this.txtAliasName.ShowBallonTip = false;
            this.txtAliasName.ShowErrorIcon = false;
            this.txtAliasName.ShowMessage = null;
            this.txtAliasName.Size = new System.Drawing.Size(231, 22);
            this.txtAliasName.Suffix = null;
            this.txtAliasName.TabIndex = 3;
            this.txtAliasName.Leave += new System.EventHandler(this.txtAliasName_Leave);
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.BackColor = System.Drawing.Color.Transparent;
            this.lblCountry.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountry.Location = new System.Drawing.Point(230, 96);
            this.lblCountry.Moveable = false;
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.NameOfControl = null;
            this.lblCountry.Size = new System.Drawing.Size(59, 14);
            this.lblCountry.TabIndex = 1003;
            this.lblCountry.Text = "Country";
            // 
            // lblWeaverColon
            // 
            this.lblWeaverColon.AutoSize = true;
            this.lblWeaverColon.BackColor = System.Drawing.Color.Transparent;
            this.lblWeaverColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeaverColon.Location = new System.Drawing.Point(347, 94);
            this.lblWeaverColon.Moveable = false;
            this.lblWeaverColon.Name = "lblWeaverColon";
            this.lblWeaverColon.NameOfControl = null;
            this.lblWeaverColon.Size = new System.Drawing.Size(12, 14);
            this.lblWeaverColon.TabIndex = 1041;
            this.lblWeaverColon.Text = ":";
            // 
            // lblRegName
            // 
            this.lblRegName.AutoSize = true;
            this.lblRegName.BackColor = System.Drawing.Color.Transparent;
            this.lblRegName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegName.Location = new System.Drawing.Point(230, 44);
            this.lblRegName.Moveable = false;
            this.lblRegName.Name = "lblRegName";
            this.lblRegName.NameOfControl = null;
            this.lblRegName.Size = new System.Drawing.Size(117, 14);
            this.lblRegName.TabIndex = 1043;
            this.lblRegName.Text = "Reg. State Name";
            // 
            // txtRegName
            // 
            this.txtRegName.AutoFillDate = false;
            this.txtRegName.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtRegName.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtRegName.CheckForSymbol = null;
            this.txtRegName.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtRegName.DecimalPlace = 0;
            this.txtRegName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegName.HelpText = "Enter Regional State Name";
            this.txtRegName.HoldMyText = null;
            this.txtRegName.IsMandatory = true;
            this.txtRegName.IsSingleQuote = true;
            this.txtRegName.IsSysmbol = false;
            this.txtRegName.Location = new System.Drawing.Point(361, 42);
            this.txtRegName.Mask = null;
            this.txtRegName.MaxLength = 50;
            this.txtRegName.Moveable = false;
            this.txtRegName.Name = "txtRegName";
            this.txtRegName.NameOfControl = "Regional Name";
            this.txtRegName.Prefix = null;
            this.txtRegName.ShowBallonTip = false;
            this.txtRegName.ShowErrorIcon = false;
            this.txtRegName.ShowMessage = null;
            this.txtRegName.Size = new System.Drawing.Size(231, 22);
            this.txtRegName.Suffix = null;
            this.txtRegName.TabIndex = 2;
            // 
            // ciS_TextLabel2
            // 
            this.ciS_TextLabel2.AutoSize = true;
            this.ciS_TextLabel2.BackColor = System.Drawing.Color.Transparent;
            this.ciS_TextLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ciS_TextLabel2.Location = new System.Drawing.Point(346, 44);
            this.ciS_TextLabel2.Moveable = false;
            this.ciS_TextLabel2.Name = "ciS_TextLabel2";
            this.ciS_TextLabel2.NameOfControl = null;
            this.ciS_TextLabel2.Size = new System.Drawing.Size(12, 14);
            this.ciS_TextLabel2.TabIndex = 1044;
            this.ciS_TextLabel2.Text = ":";
            // 
            // cboCountry
            // 
            this.cboCountry.AutoComplete = false;
            this.cboCountry.AutoDropdown = false;
            this.cboCountry.BackColor = System.Drawing.Color.White;
            this.cboCountry.BackColorEven = System.Drawing.Color.Lavender;
            this.cboCountry.BackColorOdd = System.Drawing.Color.White;
            this.cboCountry.ColumnNames = "";
            this.cboCountry.ColumnWidthDefault = 175;
            this.cboCountry.ColumnWidths = "";
            this.cboCountry.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboCountry.Fill_ComboID = 0;
            this.cboCountry.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.cboCountry.FormattingEnabled = true;
            this.cboCountry.GroupType = 0;
            this.cboCountry.HelpText = "Select Pur A/C.";
            this.cboCountry.IsMandatory = false;
            this.cboCountry.LinkedColumnIndex = 0;
            this.cboCountry.LinkedTextBox = null;
            this.cboCountry.Location = new System.Drawing.Point(361, 94);
            this.cboCountry.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboCountry.Moveable = false;
            this.cboCountry.Name = "cboCountry";
            this.cboCountry.NameOfControl = "Country";
            this.cboCountry.OpenForm = "461";
            this.cboCountry.ShowBallonTip = false;
            this.cboCountry.Size = new System.Drawing.Size(231, 23);
            this.cboCountry.TabIndex = 4;
            // 
            // lblEI1
            // 
            this.lblEI1.AutoSize = true;
            this.lblEI1.BackColor = System.Drawing.Color.Transparent;
            this.lblEI1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI1.Location = new System.Drawing.Point(234, 170);
            this.lblEI1.Moveable = false;
            this.lblEI1.Name = "lblEI1";
            this.lblEI1.NameOfControl = "EI1";
            this.lblEI1.Size = new System.Drawing.Size(30, 14);
            this.lblEI1.TabIndex = 1091;
            this.lblEI1.Text = "EI1";
            this.lblEI1.Visible = false;
            // 
            // lblEI1Colon
            // 
            this.lblEI1Colon.AutoSize = true;
            this.lblEI1Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblEI1Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI1Colon.Location = new System.Drawing.Point(347, 170);
            this.lblEI1Colon.Moveable = false;
            this.lblEI1Colon.Name = "lblEI1Colon";
            this.lblEI1Colon.NameOfControl = "EI1";
            this.lblEI1Colon.Size = new System.Drawing.Size(12, 14);
            this.lblEI1Colon.TabIndex = 1092;
            this.lblEI1Colon.Text = ":";
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
            this.cboEI1.HelpText = "Select State";
            this.cboEI1.IsMandatory = true;
            this.cboEI1.LinkedColumnIndex = 0;
            this.cboEI1.LinkedTextBox = null;
            this.cboEI1.Location = new System.Drawing.Point(361, 166);
            this.cboEI1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboEI1.Moveable = false;
            this.cboEI1.Name = "cboEI1";
            this.cboEI1.NameOfControl = "EI1";
            this.cboEI1.OpenForm = null;
            this.cboEI1.ShowBallonTip = false;
            this.cboEI1.Size = new System.Drawing.Size(231, 23);
            this.cboEI1.TabIndex = 1090;
            this.cboEI1.Visible = false;
            // 
            // lblEI2
            // 
            this.lblEI2.AutoSize = true;
            this.lblEI2.BackColor = System.Drawing.Color.Transparent;
            this.lblEI2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI2.Location = new System.Drawing.Point(234, 194);
            this.lblEI2.Moveable = false;
            this.lblEI2.Name = "lblEI2";
            this.lblEI2.NameOfControl = "EI2";
            this.lblEI2.Size = new System.Drawing.Size(30, 14);
            this.lblEI2.TabIndex = 1088;
            this.lblEI2.Text = "EI2";
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
            this.cboEI2.HelpText = "Select Country";
            this.cboEI2.IsMandatory = true;
            this.cboEI2.LinkedColumnIndex = 0;
            this.cboEI2.LinkedTextBox = null;
            this.cboEI2.Location = new System.Drawing.Point(361, 192);
            this.cboEI2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboEI2.Moveable = false;
            this.cboEI2.Name = "cboEI2";
            this.cboEI2.NameOfControl = "EI2";
            this.cboEI2.OpenForm = null;
            this.cboEI2.ShowBallonTip = false;
            this.cboEI2.Size = new System.Drawing.Size(231, 23);
            this.cboEI2.TabIndex = 1087;
            this.cboEI2.Visible = false;
            // 
            // lblEI2Colon
            // 
            this.lblEI2Colon.AutoSize = true;
            this.lblEI2Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblEI2Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI2Colon.Location = new System.Drawing.Point(347, 194);
            this.lblEI2Colon.Moveable = false;
            this.lblEI2Colon.Name = "lblEI2Colon";
            this.lblEI2Colon.NameOfControl = null;
            this.lblEI2Colon.Size = new System.Drawing.Size(12, 14);
            this.lblEI2Colon.TabIndex = 1089;
            this.lblEI2Colon.Text = ":";
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
            this.txtET3.HelpText = "Enter Alias Name";
            this.txtET3.HoldMyText = null;
            this.txtET3.IsMandatory = false;
            this.txtET3.IsSingleQuote = true;
            this.txtET3.IsSysmbol = false;
            this.txtET3.Location = new System.Drawing.Point(361, 270);
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
            this.txtET3.TabIndex = 1080;
            this.txtET3.Visible = false;
            // 
            // lblET3Colon
            // 
            this.lblET3Colon.AutoSize = true;
            this.lblET3Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblET3Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET3Colon.Location = new System.Drawing.Point(347, 273);
            this.lblET3Colon.Moveable = false;
            this.lblET3Colon.Name = "lblET3Colon";
            this.lblET3Colon.NameOfControl = null;
            this.lblET3Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET3Colon.TabIndex = 1086;
            this.lblET3Colon.Text = ":";
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
            this.txtET2.HelpText = "Enter Alias Name";
            this.txtET2.HoldMyText = null;
            this.txtET2.IsMandatory = false;
            this.txtET2.IsSingleQuote = true;
            this.txtET2.IsSysmbol = false;
            this.txtET2.Location = new System.Drawing.Point(361, 243);
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
            this.txtET2.TabIndex = 1079;
            this.txtET2.Visible = false;
            // 
            // lblET2Colon
            // 
            this.lblET2Colon.AutoSize = true;
            this.lblET2Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblET2Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET2Colon.Location = new System.Drawing.Point(347, 248);
            this.lblET2Colon.Moveable = false;
            this.lblET2Colon.Name = "lblET2Colon";
            this.lblET2Colon.NameOfControl = "ET2";
            this.lblET2Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET2Colon.TabIndex = 1084;
            this.lblET2Colon.Text = ":";
            this.lblET2Colon.Visible = false;
            // 
            // lblET3
            // 
            this.lblET3.AutoSize = true;
            this.lblET3.BackColor = System.Drawing.Color.Transparent;
            this.lblET3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET3.Location = new System.Drawing.Point(234, 272);
            this.lblET3.Moveable = false;
            this.lblET3.Name = "lblET3";
            this.lblET3.NameOfControl = "ET3";
            this.lblET3.Size = new System.Drawing.Size(32, 14);
            this.lblET3.TabIndex = 1085;
            this.lblET3.Text = "ET3";
            this.lblET3.Visible = false;
            // 
            // lblET2
            // 
            this.lblET2.AutoSize = true;
            this.lblET2.BackColor = System.Drawing.Color.Transparent;
            this.lblET2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET2.Location = new System.Drawing.Point(234, 246);
            this.lblET2.Moveable = false;
            this.lblET2.Name = "lblET2";
            this.lblET2.NameOfControl = "ET2";
            this.lblET2.Size = new System.Drawing.Size(32, 14);
            this.lblET2.TabIndex = 1083;
            this.lblET2.Text = "ET2";
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
            this.txtET1.HelpText = "Enter Alias Name";
            this.txtET1.HoldMyText = null;
            this.txtET1.IsMandatory = false;
            this.txtET1.IsSingleQuote = true;
            this.txtET1.IsSysmbol = false;
            this.txtET1.Location = new System.Drawing.Point(361, 218);
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
            this.txtET1.TabIndex = 1078;
            this.txtET1.Visible = false;
            // 
            // lblET1Colon
            // 
            this.lblET1Colon.AutoSize = true;
            this.lblET1Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblET1Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET1Colon.Location = new System.Drawing.Point(347, 220);
            this.lblET1Colon.Moveable = false;
            this.lblET1Colon.Name = "lblET1Colon";
            this.lblET1Colon.NameOfControl = "ET1";
            this.lblET1Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET1Colon.TabIndex = 1082;
            this.lblET1Colon.Text = ":";
            this.lblET1Colon.Visible = false;
            // 
            // lblET1
            // 
            this.lblET1.AutoSize = true;
            this.lblET1.BackColor = System.Drawing.Color.Transparent;
            this.lblET1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET1.Location = new System.Drawing.Point(234, 220);
            this.lblET1.Moveable = false;
            this.lblET1.Name = "lblET1";
            this.lblET1.NameOfControl = "ET1";
            this.lblET1.Size = new System.Drawing.Size(32, 14);
            this.lblET1.TabIndex = 1081;
            this.lblET1.Text = "ET1";
            this.lblET1.Visible = false;
            // 
            // frmStateMaster
            // 
            this.ClientSize = new System.Drawing.Size(808, 545);
            this.Controls.Add(this.lblEI1);
            this.Controls.Add(this.lblEI1Colon);
            this.Controls.Add(this.cboEI1);
            this.Controls.Add(this.lblEI2);
            this.Controls.Add(this.cboEI2);
            this.Controls.Add(this.lblEI2Colon);
            this.Controls.Add(this.txtET3);
            this.Controls.Add(this.lblET3Colon);
            this.Controls.Add(this.txtET2);
            this.Controls.Add(this.lblET2Colon);
            this.Controls.Add(this.lblET3);
            this.Controls.Add(this.lblET2);
            this.Controls.Add(this.txtET1);
            this.Controls.Add(this.lblET1Colon);
            this.Controls.Add(this.lblET1);
            this.Name = "frmStateMaster";
            this.Load += new System.EventHandler(this.frm_Load);
            this.Controls.SetChildIndex(this.pnlContent, 0);
            this.Controls.SetChildIndex(this.lblET1, 0);
            this.Controls.SetChildIndex(this.lblET1Colon, 0);
            this.Controls.SetChildIndex(this.txtET1, 0);
            this.Controls.SetChildIndex(this.lblET2, 0);
            this.Controls.SetChildIndex(this.lblET3, 0);
            this.Controls.SetChildIndex(this.lblET2Colon, 0);
            this.Controls.SetChildIndex(this.txtET2, 0);
            this.Controls.SetChildIndex(this.lblET3Colon, 0);
            this.Controls.SetChildIndex(this.txtET3, 0);
            this.Controls.SetChildIndex(this.lblEI2Colon, 0);
            this.Controls.SetChildIndex(this.cboEI2, 0);
            this.Controls.SetChildIndex(this.lblEI2, 0);
            this.Controls.SetChildIndex(this.cboEI1, 0);
            this.Controls.SetChildIndex(this.lblEI1Colon, 0);
            this.Controls.SetChildIndex(this.lblEI1, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal CIS_CLibrary.CIS_Textbox txtstatename;
        internal CIS_CLibrary.CIS_TextLabel lblCityNameColon;
        internal CIS_CLibrary.CIS_TextLabel lblStatename;
        internal CIS_CLibrary.CIS_CheckBox ChkActive;
        public CIS_CLibrary.CIS_Textbox txtCode;
        internal CIS_CLibrary.CIS_TextLabel label2;
        internal CIS_CLibrary.CIS_TextLabel lblAliasName;
        internal CIS_CLibrary.CIS_Textbox txtAliasName;
        internal CIS_CLibrary.CIS_TextLabel lblCountry;
        internal CIS_CLibrary.CIS_TextLabel lblWeaverColon;
        internal CIS_CLibrary.CIS_TextLabel lblRegName;
        internal CIS_CLibrary.CIS_Textbox txtRegName;
        internal CIS_CLibrary.CIS_TextLabel ciS_TextLabel2;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboCountry;
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
