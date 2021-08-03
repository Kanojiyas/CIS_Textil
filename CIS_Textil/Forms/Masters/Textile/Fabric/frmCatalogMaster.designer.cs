namespace CIS_Textil
{
    partial class frmCatalogMaster
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
            CIS_CLibrary.ToolTip.StringDataProvider stringDataProvider2 = new CIS_CLibrary.ToolTip.StringDataProvider();
            this.ChkActive = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.txtCatalogName = new CIS_CLibrary.CIS_Textbox();
            this.lblCatalogNameColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblCatalogName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.tltOnControls = new CIS_CLibrary.ToolTip.CIS_ToolTip();
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.txtAliasName = new CIS_CLibrary.CIS_Textbox();
            this.lblaliasNameColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblAliasName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.pnlDetail = new System.Windows.Forms.Panel();
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
            this.GrdMain = new Crocus_CClib.DataGridViewX();
            this.PnlFooter = new System.Windows.Forms.Panel();
            this.txtNoOfDesign = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblNoofDesign = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtMtrs = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblMtrs = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.label2 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.label1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtMinMtrs = new CIS_CLibrary.CIS_Textbox();
            this.txtMaxMtrs = new CIS_CLibrary.CIS_Textbox();
            this.txtMaxPcs = new CIS_CLibrary.CIS_Textbox();
            this.label6 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.label7 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.label5 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.label8 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.label4 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.label3 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.label9 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtMinPcs = new CIS_CLibrary.CIS_Textbox();
            this.label10 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSelComp = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.chkCompany = new CIS_CLibrary.CIS_CheckBoxList();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.pnlDetail.SuspendLayout();
            this.PnlFooter.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.lblSelComp);
            this.pnlContent.Controls.Add(this.chkCompany);
            this.pnlContent.Controls.Add(this.label2);
            this.pnlContent.Controls.Add(this.label1);
            this.pnlContent.Controls.Add(this.panel1);
            this.pnlContent.Controls.Add(this.PnlFooter);
            this.pnlContent.Controls.Add(this.pnlDetail);
            this.pnlContent.Controls.Add(this.txtAliasName);
            this.pnlContent.Controls.Add(this.lblaliasNameColon);
            this.pnlContent.Controls.Add(this.lblAliasName);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.Add(this.ChkActive);
            this.pnlContent.Controls.Add(this.txtCatalogName);
            this.pnlContent.Controls.Add(this.lblCatalogNameColon);
            this.pnlContent.Controls.Add(this.lblCatalogName);
            this.tltOnControls.SetToolTip(this.pnlContent, "");
            this.pnlContent.Controls.SetChildIndex(this.lblCatalogName, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblCatalogNameColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCatalogName, 0);
            this.pnlContent.Controls.SetChildIndex(this.ChkActive, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblAliasName, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblaliasNameColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtAliasName, 0);
            this.pnlContent.Controls.SetChildIndex(this.pnlDetail, 0);
            this.pnlContent.Controls.SetChildIndex(this.PnlFooter, 0);
            this.pnlContent.Controls.SetChildIndex(this.panel1, 0);
            this.pnlContent.Controls.SetChildIndex(this.label1, 0);
            this.pnlContent.Controls.SetChildIndex(this.label2, 0);
            this.pnlContent.Controls.SetChildIndex(this.chkCompany, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblSelComp, 0);
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
            // ChkActive
            // 
            this.ChkActive.AutoSize = true;
            this.ChkActive.BackColor = System.Drawing.Color.MintCream;
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChkActive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.HelpText = "Checked If Active";
            this.ChkActive.Location = new System.Drawing.Point(266, 70);
            this.ChkActive.Moveable = false;
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.NameOfControl = null;
            this.ChkActive.Size = new System.Drawing.Size(110, 18);
            this.ChkActive.TabIndex = 3;
            this.ChkActive.Text = "Active Status";
            this.tltOnControls.SetToolTip(this.ChkActive, "");
            this.ChkActive.UseVisualStyleBackColor = false;
            // 
            // txtCatalogName
            // 
            this.txtCatalogName.AutoFillDate = false;
            this.txtCatalogName.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtCatalogName.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtCatalogName.CheckForSymbol = null;
            this.txtCatalogName.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtCatalogName.DecimalPlace = 0;
            this.txtCatalogName.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCatalogName.HelpText = "Enter Book Name";
            this.txtCatalogName.HoldMyText = null;
            this.txtCatalogName.IsMandatory = true;
            this.txtCatalogName.IsSingleQuote = true;
            this.txtCatalogName.IsSysmbol = false;
            this.txtCatalogName.Location = new System.Drawing.Point(266, 18);
            this.txtCatalogName.Mask = null;
            this.txtCatalogName.MaxLength = 50;
            this.txtCatalogName.Moveable = false;
            this.txtCatalogName.Name = "txtCatalogName";
            this.txtCatalogName.NameOfControl = "Catalog Name";
            this.txtCatalogName.Prefix = null;
            this.txtCatalogName.ShowBallonTip = false;
            this.txtCatalogName.ShowErrorIcon = false;
            this.txtCatalogName.ShowMessage = null;
            this.txtCatalogName.Size = new System.Drawing.Size(231, 21);
            this.txtCatalogName.Suffix = null;
            this.txtCatalogName.TabIndex = 1;
            this.tltOnControls.SetToolTip(this.txtCatalogName, "");
            this.txtCatalogName.Leave += new System.EventHandler(this.txtBook_Leave);
            // 
            // lblCatalogNameColon
            // 
            this.lblCatalogNameColon.AutoSize = true;
            this.lblCatalogNameColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCatalogNameColon.Location = new System.Drawing.Point(248, 20);
            this.lblCatalogNameColon.Moveable = false;
            this.lblCatalogNameColon.Name = "lblCatalogNameColon";
            this.lblCatalogNameColon.NameOfControl = null;
            this.lblCatalogNameColon.Size = new System.Drawing.Size(12, 14);
            this.lblCatalogNameColon.TabIndex = 1013;
            this.lblCatalogNameColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblCatalogNameColon, "");
            // 
            // lblCatalogName
            // 
            this.lblCatalogName.AutoSize = true;
            this.lblCatalogName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCatalogName.Location = new System.Drawing.Point(130, 20);
            this.lblCatalogName.Moveable = false;
            this.lblCatalogName.Name = "lblCatalogName";
            this.lblCatalogName.NameOfControl = null;
            this.lblCatalogName.Size = new System.Drawing.Size(99, 14);
            this.lblCatalogName.TabIndex = 1012;
            this.lblCatalogName.Text = "Catalog Name";
            this.tltOnControls.SetToolTip(this.lblCatalogName, "");
            // 
            // tltOnControls
            // 
            this.tltOnControls.DataProvider = stringDataProvider2;
            this.tltOnControls.LoadText = "";
            this.tltOnControls.ShowToolTip = false;
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
            this.txtCode.Location = new System.Drawing.Point(6, 0);
            this.txtCode.Mask = null;
            this.txtCode.Moveable = false;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(56, 22);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 0;
            this.tltOnControls.SetToolTip(this.txtCode, "");
            this.txtCode.Visible = false;
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
            this.txtAliasName.Location = new System.Drawing.Point(266, 43);
            this.txtAliasName.Mask = null;
            this.txtAliasName.MaxLength = 50;
            this.txtAliasName.Moveable = false;
            this.txtAliasName.Name = "txtAliasName";
            this.txtAliasName.NameOfControl = "Alias name";
            this.txtAliasName.Prefix = null;
            this.txtAliasName.ShowBallonTip = false;
            this.txtAliasName.ShowErrorIcon = false;
            this.txtAliasName.ShowMessage = null;
            this.txtAliasName.Size = new System.Drawing.Size(231, 21);
            this.txtAliasName.Suffix = null;
            this.txtAliasName.TabIndex = 2;
            this.tltOnControls.SetToolTip(this.txtAliasName, "");
            this.txtAliasName.Leave += new System.EventHandler(this.txtAliasName_Leave);
            // 
            // lblaliasNameColon
            // 
            this.lblaliasNameColon.AutoSize = true;
            this.lblaliasNameColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblaliasNameColon.Location = new System.Drawing.Point(248, 45);
            this.lblaliasNameColon.Moveable = false;
            this.lblaliasNameColon.Name = "lblaliasNameColon";
            this.lblaliasNameColon.NameOfControl = null;
            this.lblaliasNameColon.Size = new System.Drawing.Size(12, 14);
            this.lblaliasNameColon.TabIndex = 1021;
            this.lblaliasNameColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblaliasNameColon, "");
            // 
            // lblAliasName
            // 
            this.lblAliasName.AutoSize = true;
            this.lblAliasName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAliasName.Location = new System.Drawing.Point(130, 45);
            this.lblAliasName.Moveable = false;
            this.lblAliasName.Name = "lblAliasName";
            this.lblAliasName.NameOfControl = null;
            this.lblAliasName.Size = new System.Drawing.Size(77, 14);
            this.lblAliasName.TabIndex = 1020;
            this.lblAliasName.Text = "AliasName";
            this.tltOnControls.SetToolTip(this.lblAliasName, "");
            // 
            // pnlDetail
            // 
            this.pnlDetail.BackColor = System.Drawing.Color.LightSkyBlue;
            this.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDetail.Controls.Add(this.lblEI1);
            this.pnlDetail.Controls.Add(this.lblEI1Colon);
            this.pnlDetail.Controls.Add(this.cboEI1);
            this.pnlDetail.Controls.Add(this.lblEI2);
            this.pnlDetail.Controls.Add(this.cboEI2);
            this.pnlDetail.Controls.Add(this.lblEI2Colon);
            this.pnlDetail.Controls.Add(this.txtET3);
            this.pnlDetail.Controls.Add(this.lblET3Colon);
            this.pnlDetail.Controls.Add(this.txtET2);
            this.pnlDetail.Controls.Add(this.lblET2Colon);
            this.pnlDetail.Controls.Add(this.lblET3);
            this.pnlDetail.Controls.Add(this.lblET2);
            this.pnlDetail.Controls.Add(this.txtET1);
            this.pnlDetail.Controls.Add(this.lblET1Colon);
            this.pnlDetail.Controls.Add(this.lblET1);
            this.pnlDetail.Controls.Add(this.GrdMain);
            this.pnlDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.590312F);
            this.pnlDetail.Location = new System.Drawing.Point(109, 97);
            this.pnlDetail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(476, 257);
            this.pnlDetail.TabIndex = 1030;
            this.tltOnControls.SetToolTip(this.pnlDetail, "");
            // 
            // lblEI1
            // 
            this.lblEI1.AutoSize = true;
            this.lblEI1.BackColor = System.Drawing.Color.Transparent;
            this.lblEI1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI1.Location = new System.Drawing.Point(58, 68);
            this.lblEI1.Moveable = false;
            this.lblEI1.Name = "lblEI1";
            this.lblEI1.NameOfControl = "EI1";
            this.lblEI1.Size = new System.Drawing.Size(30, 14);
            this.lblEI1.TabIndex = 90170;
            this.lblEI1.Text = "EI1";
            this.tltOnControls.SetToolTip(this.lblEI1, "");
            this.lblEI1.Visible = false;
            // 
            // lblEI1Colon
            // 
            this.lblEI1Colon.AutoSize = true;
            this.lblEI1Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblEI1Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI1Colon.Location = new System.Drawing.Point(171, 68);
            this.lblEI1Colon.Moveable = false;
            this.lblEI1Colon.Name = "lblEI1Colon";
            this.lblEI1Colon.NameOfControl = "EI1";
            this.lblEI1Colon.Size = new System.Drawing.Size(12, 14);
            this.lblEI1Colon.TabIndex = 90171;
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
            this.cboEI1.HelpText = "Select State";
            this.cboEI1.IsMandatory = true;
            this.cboEI1.LinkedColumnIndex = 0;
            this.cboEI1.LinkedTextBox = null;
            this.cboEI1.Location = new System.Drawing.Point(185, 64);
            this.cboEI1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboEI1.Moveable = false;
            this.cboEI1.Name = "cboEI1";
            this.cboEI1.NameOfControl = "EI1";
            this.cboEI1.OpenForm = null;
            this.cboEI1.ShowBallonTip = false;
            this.cboEI1.Size = new System.Drawing.Size(231, 23);
            this.cboEI1.TabIndex = 90169;
            this.tltOnControls.SetToolTip(this.cboEI1, "");
            this.cboEI1.Visible = false;
            // 
            // lblEI2
            // 
            this.lblEI2.AutoSize = true;
            this.lblEI2.BackColor = System.Drawing.Color.Transparent;
            this.lblEI2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI2.Location = new System.Drawing.Point(58, 92);
            this.lblEI2.Moveable = false;
            this.lblEI2.Name = "lblEI2";
            this.lblEI2.NameOfControl = "EI2";
            this.lblEI2.Size = new System.Drawing.Size(30, 14);
            this.lblEI2.TabIndex = 90167;
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
            this.cboEI2.HelpText = "Select Country";
            this.cboEI2.IsMandatory = true;
            this.cboEI2.LinkedColumnIndex = 0;
            this.cboEI2.LinkedTextBox = null;
            this.cboEI2.Location = new System.Drawing.Point(185, 90);
            this.cboEI2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboEI2.Moveable = false;
            this.cboEI2.Name = "cboEI2";
            this.cboEI2.NameOfControl = "EI2";
            this.cboEI2.OpenForm = null;
            this.cboEI2.ShowBallonTip = false;
            this.cboEI2.Size = new System.Drawing.Size(231, 23);
            this.cboEI2.TabIndex = 90166;
            this.tltOnControls.SetToolTip(this.cboEI2, "");
            this.cboEI2.Visible = false;
            // 
            // lblEI2Colon
            // 
            this.lblEI2Colon.AutoSize = true;
            this.lblEI2Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblEI2Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI2Colon.Location = new System.Drawing.Point(171, 92);
            this.lblEI2Colon.Moveable = false;
            this.lblEI2Colon.Name = "lblEI2Colon";
            this.lblEI2Colon.NameOfControl = null;
            this.lblEI2Colon.Size = new System.Drawing.Size(12, 14);
            this.lblEI2Colon.TabIndex = 90168;
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
            this.txtET3.HelpText = "Enter Alias Name";
            this.txtET3.HoldMyText = null;
            this.txtET3.IsMandatory = false;
            this.txtET3.IsSingleQuote = true;
            this.txtET3.IsSysmbol = false;
            this.txtET3.Location = new System.Drawing.Point(185, 168);
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
            this.txtET3.TabIndex = 90159;
            this.tltOnControls.SetToolTip(this.txtET3, "");
            this.txtET3.Visible = false;
            // 
            // lblET3Colon
            // 
            this.lblET3Colon.AutoSize = true;
            this.lblET3Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblET3Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET3Colon.Location = new System.Drawing.Point(171, 171);
            this.lblET3Colon.Moveable = false;
            this.lblET3Colon.Name = "lblET3Colon";
            this.lblET3Colon.NameOfControl = null;
            this.lblET3Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET3Colon.TabIndex = 90165;
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
            this.txtET2.HelpText = "Enter Alias Name";
            this.txtET2.HoldMyText = null;
            this.txtET2.IsMandatory = false;
            this.txtET2.IsSingleQuote = true;
            this.txtET2.IsSysmbol = false;
            this.txtET2.Location = new System.Drawing.Point(185, 141);
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
            this.txtET2.TabIndex = 90158;
            this.tltOnControls.SetToolTip(this.txtET2, "");
            this.txtET2.Visible = false;
            // 
            // lblET2Colon
            // 
            this.lblET2Colon.AutoSize = true;
            this.lblET2Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblET2Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET2Colon.Location = new System.Drawing.Point(171, 146);
            this.lblET2Colon.Moveable = false;
            this.lblET2Colon.Name = "lblET2Colon";
            this.lblET2Colon.NameOfControl = "ET2";
            this.lblET2Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET2Colon.TabIndex = 90163;
            this.lblET2Colon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblET2Colon, "");
            this.lblET2Colon.Visible = false;
            // 
            // lblET3
            // 
            this.lblET3.AutoSize = true;
            this.lblET3.BackColor = System.Drawing.Color.Transparent;
            this.lblET3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET3.Location = new System.Drawing.Point(58, 170);
            this.lblET3.Moveable = false;
            this.lblET3.Name = "lblET3";
            this.lblET3.NameOfControl = "ET3";
            this.lblET3.Size = new System.Drawing.Size(32, 14);
            this.lblET3.TabIndex = 90164;
            this.lblET3.Text = "ET3";
            this.tltOnControls.SetToolTip(this.lblET3, "");
            this.lblET3.Visible = false;
            // 
            // lblET2
            // 
            this.lblET2.AutoSize = true;
            this.lblET2.BackColor = System.Drawing.Color.Transparent;
            this.lblET2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET2.Location = new System.Drawing.Point(58, 144);
            this.lblET2.Moveable = false;
            this.lblET2.Name = "lblET2";
            this.lblET2.NameOfControl = "ET2";
            this.lblET2.Size = new System.Drawing.Size(32, 14);
            this.lblET2.TabIndex = 90162;
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
            this.txtET1.HelpText = "Enter Alias Name";
            this.txtET1.HoldMyText = null;
            this.txtET1.IsMandatory = false;
            this.txtET1.IsSingleQuote = true;
            this.txtET1.IsSysmbol = false;
            this.txtET1.Location = new System.Drawing.Point(185, 116);
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
            this.txtET1.TabIndex = 90157;
            this.tltOnControls.SetToolTip(this.txtET1, "");
            this.txtET1.Visible = false;
            // 
            // lblET1Colon
            // 
            this.lblET1Colon.AutoSize = true;
            this.lblET1Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblET1Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET1Colon.Location = new System.Drawing.Point(171, 118);
            this.lblET1Colon.Moveable = false;
            this.lblET1Colon.Name = "lblET1Colon";
            this.lblET1Colon.NameOfControl = "ET1";
            this.lblET1Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET1Colon.TabIndex = 90161;
            this.lblET1Colon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblET1Colon, "");
            this.lblET1Colon.Visible = false;
            // 
            // lblET1
            // 
            this.lblET1.AutoSize = true;
            this.lblET1.BackColor = System.Drawing.Color.Transparent;
            this.lblET1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET1.Location = new System.Drawing.Point(58, 118);
            this.lblET1.Moveable = false;
            this.lblET1.Name = "lblET1";
            this.lblET1.NameOfControl = "ET1";
            this.lblET1.Size = new System.Drawing.Size(32, 14);
            this.lblET1.TabIndex = 90160;
            this.lblET1.Text = "ET1";
            this.tltOnControls.SetToolTip(this.lblET1, "");
            this.lblET1.Visible = false;
            // 
            // GrdMain
            // 
            this.GrdMain.blnFormAction = 0;
            this.GrdMain.CompID = 0;
            this.GrdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrdMain.Location = new System.Drawing.Point(0, 0);
            this.GrdMain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GrdMain.Name = "GrdMain";
            this.GrdMain.Size = new System.Drawing.Size(474, 255);
            this.GrdMain.TabIndex = 90156;
            this.tltOnControls.SetToolTip(this.GrdMain, "");
            this.GrdMain.YearID = 0;
            // 
            // PnlFooter
            // 
            this.PnlFooter.BackColor = System.Drawing.Color.LightSteelBlue;
            this.PnlFooter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlFooter.Controls.Add(this.txtNoOfDesign);
            this.PnlFooter.Controls.Add(this.lblNoofDesign);
            this.PnlFooter.Controls.Add(this.txtMtrs);
            this.PnlFooter.Controls.Add(this.lblMtrs);
            this.PnlFooter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.590312F);
            this.PnlFooter.Location = new System.Drawing.Point(109, 358);
            this.PnlFooter.Name = "PnlFooter";
            this.PnlFooter.Size = new System.Drawing.Size(476, 28);
            this.PnlFooter.TabIndex = 1031;
            this.tltOnControls.SetToolTip(this.PnlFooter, "");
            // 
            // txtNoOfDesign
            // 
            this.txtNoOfDesign.AutoSize = true;
            this.txtNoOfDesign.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtNoOfDesign.ForeColor = System.Drawing.Color.Brown;
            this.txtNoOfDesign.Location = new System.Drawing.Point(309, 6);
            this.txtNoOfDesign.Moveable = false;
            this.txtNoOfDesign.Name = "txtNoOfDesign";
            this.txtNoOfDesign.NameOfControl = null;
            this.txtNoOfDesign.Size = new System.Drawing.Size(16, 14);
            this.txtNoOfDesign.TabIndex = 385;
            this.txtNoOfDesign.Text = "0";
            this.txtNoOfDesign.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tltOnControls.SetToolTip(this.txtNoOfDesign, "");
            // 
            // lblNoofDesign
            // 
            this.lblNoofDesign.AutoSize = true;
            this.lblNoofDesign.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblNoofDesign.Location = new System.Drawing.Point(183, 6);
            this.lblNoofDesign.Moveable = false;
            this.lblNoofDesign.Name = "lblNoofDesign";
            this.lblNoofDesign.NameOfControl = null;
            this.lblNoofDesign.Size = new System.Drawing.Size(122, 14);
            this.lblNoofDesign.TabIndex = 386;
            this.lblNoofDesign.Text = "No. Of Design\'s  :";
            this.tltOnControls.SetToolTip(this.lblNoofDesign, "");
            // 
            // txtMtrs
            // 
            this.txtMtrs.AutoSize = true;
            this.txtMtrs.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtMtrs.ForeColor = System.Drawing.Color.Brown;
            this.txtMtrs.Location = new System.Drawing.Point(412, 6);
            this.txtMtrs.Moveable = false;
            this.txtMtrs.Name = "txtMtrs";
            this.txtMtrs.NameOfControl = null;
            this.txtMtrs.Size = new System.Drawing.Size(47, 14);
            this.txtMtrs.TabIndex = 387;
            this.txtMtrs.Text = "0.000";
            this.txtMtrs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tltOnControls.SetToolTip(this.txtMtrs, "");
            // 
            // lblMtrs
            // 
            this.lblMtrs.AutoSize = true;
            this.lblMtrs.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblMtrs.Location = new System.Drawing.Point(357, 5);
            this.lblMtrs.Moveable = false;
            this.lblMtrs.Name = "lblMtrs";
            this.lblMtrs.NameOfControl = null;
            this.lblMtrs.Size = new System.Drawing.Size(49, 14);
            this.lblMtrs.TabIndex = 388;
            this.lblMtrs.Text = "Mtrs  :";
            this.tltOnControls.SetToolTip(this.lblMtrs, "");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(132, 398);
            this.label2.Moveable = false;
            this.label2.Name = "label2";
            this.label2.NameOfControl = null;
            this.label2.Size = new System.Drawing.Size(107, 14);
            this.label2.TabIndex = 1157;
            this.label2.Text = "Minimum Stock";
            this.tltOnControls.SetToolTip(this.label2, "");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(420, 398);
            this.label1.Moveable = false;
            this.label1.Name = "label1";
            this.label1.NameOfControl = null;
            this.label1.Size = new System.Drawing.Size(111, 14);
            this.label1.TabIndex = 1155;
            this.label1.Text = "Maximum Stock";
            this.tltOnControls.SetToolTip(this.label1, "");
            // 
            // txtMinMtrs
            // 
            this.txtMinMtrs.AutoFillDate = false;
            this.txtMinMtrs.BackColor = System.Drawing.Color.White;
            this.txtMinMtrs.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtMinMtrs.CheckForSymbol = null;
            this.txtMinMtrs.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.NumericWithDecimal;
            this.txtMinMtrs.DecimalPlace = 2;
            this.txtMinMtrs.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinMtrs.HelpText = "Enter Min Mtrs";
            this.txtMinMtrs.HoldMyText = null;
            this.txtMinMtrs.IsMandatory = false;
            this.txtMinMtrs.IsSingleQuote = true;
            this.txtMinMtrs.IsSysmbol = false;
            this.txtMinMtrs.Location = new System.Drawing.Point(133, 28);
            this.txtMinMtrs.Mask = null;
            this.txtMinMtrs.MaxLength = 4;
            this.txtMinMtrs.Moveable = false;
            this.txtMinMtrs.Name = "txtMinMtrs";
            this.txtMinMtrs.NameOfControl = "Min Mtrs";
            this.txtMinMtrs.Prefix = null;
            this.txtMinMtrs.ShowBallonTip = false;
            this.txtMinMtrs.ShowErrorIcon = false;
            this.txtMinMtrs.ShowMessage = null;
            this.txtMinMtrs.Size = new System.Drawing.Size(77, 21);
            this.txtMinMtrs.Suffix = null;
            this.txtMinMtrs.TabIndex = 12;
            this.txtMinMtrs.Text = "0.00";
            this.txtMinMtrs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tltOnControls.SetToolTip(this.txtMinMtrs, "");
            // 
            // txtMaxMtrs
            // 
            this.txtMaxMtrs.AutoFillDate = false;
            this.txtMaxMtrs.BackColor = System.Drawing.Color.White;
            this.txtMaxMtrs.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtMaxMtrs.CheckForSymbol = null;
            this.txtMaxMtrs.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.NumericWithDecimal;
            this.txtMaxMtrs.DecimalPlace = 2;
            this.txtMaxMtrs.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxMtrs.HelpText = "Enter Max Mtrs";
            this.txtMaxMtrs.HoldMyText = null;
            this.txtMaxMtrs.IsMandatory = false;
            this.txtMaxMtrs.IsSingleQuote = true;
            this.txtMaxMtrs.IsSysmbol = false;
            this.txtMaxMtrs.Location = new System.Drawing.Point(425, 28);
            this.txtMaxMtrs.Mask = null;
            this.txtMaxMtrs.MaxLength = 4;
            this.txtMaxMtrs.Moveable = false;
            this.txtMaxMtrs.Name = "txtMaxMtrs";
            this.txtMaxMtrs.NameOfControl = "Max Mtrs";
            this.txtMaxMtrs.Prefix = null;
            this.txtMaxMtrs.ShowBallonTip = false;
            this.txtMaxMtrs.ShowErrorIcon = false;
            this.txtMaxMtrs.ShowMessage = null;
            this.txtMaxMtrs.Size = new System.Drawing.Size(77, 21);
            this.txtMaxMtrs.Suffix = null;
            this.txtMaxMtrs.TabIndex = 14;
            this.txtMaxMtrs.Text = "0.00";
            this.txtMaxMtrs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tltOnControls.SetToolTip(this.txtMaxMtrs, "");
            // 
            // txtMaxPcs
            // 
            this.txtMaxPcs.AutoFillDate = false;
            this.txtMaxPcs.BackColor = System.Drawing.Color.White;
            this.txtMaxPcs.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtMaxPcs.CheckForSymbol = null;
            this.txtMaxPcs.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.NumericWithOutDecimal;
            this.txtMaxPcs.DecimalPlace = 0;
            this.txtMaxPcs.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaxPcs.HelpText = "Enter Max Pcs";
            this.txtMaxPcs.HoldMyText = null;
            this.txtMaxPcs.IsMandatory = false;
            this.txtMaxPcs.IsSingleQuote = true;
            this.txtMaxPcs.IsSysmbol = false;
            this.txtMaxPcs.Location = new System.Drawing.Point(425, 3);
            this.txtMaxPcs.Mask = null;
            this.txtMaxPcs.MaxLength = 6;
            this.txtMaxPcs.Moveable = false;
            this.txtMaxPcs.Name = "txtMaxPcs";
            this.txtMaxPcs.NameOfControl = "Max Pcs";
            this.txtMaxPcs.Prefix = null;
            this.txtMaxPcs.ShowBallonTip = false;
            this.txtMaxPcs.ShowErrorIcon = false;
            this.txtMaxPcs.ShowMessage = null;
            this.txtMaxPcs.Size = new System.Drawing.Size(77, 21);
            this.txtMaxPcs.Suffix = null;
            this.txtMaxPcs.TabIndex = 13;
            this.txtMaxPcs.Text = "0";
            this.txtMaxPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tltOnControls.SetToolTip(this.txtMaxPcs, "");
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(121, 5);
            this.label6.Moveable = false;
            this.label6.Name = "label6";
            this.label6.NameOfControl = null;
            this.label6.Size = new System.Drawing.Size(12, 14);
            this.label6.TabIndex = 1143;
            this.label6.Text = ":";
            this.tltOnControls.SetToolTip(this.label6, "");
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(363, 5);
            this.label7.Moveable = false;
            this.label7.Name = "label7";
            this.label7.NameOfControl = null;
            this.label7.Size = new System.Drawing.Size(30, 14);
            this.label7.TabIndex = 1147;
            this.label7.Text = "Pcs";
            this.tltOnControls.SetToolTip(this.label7, "");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(121, 30);
            this.label5.Moveable = false;
            this.label5.Name = "label5";
            this.label5.NameOfControl = null;
            this.label5.Size = new System.Drawing.Size(12, 14);
            this.label5.TabIndex = 1144;
            this.label5.Text = ":";
            this.tltOnControls.SetToolTip(this.label5, "");
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(363, 30);
            this.label8.Moveable = false;
            this.label8.Name = "label8";
            this.label8.NameOfControl = null;
            this.label8.Size = new System.Drawing.Size(36, 14);
            this.label8.TabIndex = 1148;
            this.label8.Text = "Mtrs";
            this.tltOnControls.SetToolTip(this.label8, "");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(72, 31);
            this.label4.Moveable = false;
            this.label4.Name = "label4";
            this.label4.NameOfControl = null;
            this.label4.Size = new System.Drawing.Size(36, 14);
            this.label4.TabIndex = 1142;
            this.label4.Text = "Mtrs";
            this.tltOnControls.SetToolTip(this.label4, "");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(72, 6);
            this.label3.Moveable = false;
            this.label3.Name = "label3";
            this.label3.NameOfControl = null;
            this.label3.Size = new System.Drawing.Size(30, 14);
            this.label3.TabIndex = 1141;
            this.label3.Text = "Pcs";
            this.tltOnControls.SetToolTip(this.label3, "");
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(414, 29);
            this.label9.Moveable = false;
            this.label9.Name = "label9";
            this.label9.NameOfControl = null;
            this.label9.Size = new System.Drawing.Size(12, 14);
            this.label9.TabIndex = 1150;
            this.label9.Text = ":";
            this.tltOnControls.SetToolTip(this.label9, "");
            // 
            // txtMinPcs
            // 
            this.txtMinPcs.AutoFillDate = false;
            this.txtMinPcs.BackColor = System.Drawing.Color.White;
            this.txtMinPcs.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtMinPcs.CheckForSymbol = null;
            this.txtMinPcs.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.NumericWithOutDecimal;
            this.txtMinPcs.DecimalPlace = 0;
            this.txtMinPcs.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinPcs.HelpText = "Enter Min Pcs";
            this.txtMinPcs.HoldMyText = null;
            this.txtMinPcs.IsMandatory = false;
            this.txtMinPcs.IsSingleQuote = true;
            this.txtMinPcs.IsSysmbol = false;
            this.txtMinPcs.Location = new System.Drawing.Point(133, 4);
            this.txtMinPcs.Mask = null;
            this.txtMinPcs.MaxLength = 6;
            this.txtMinPcs.Moveable = false;
            this.txtMinPcs.Name = "txtMinPcs";
            this.txtMinPcs.NameOfControl = "Min Pcs";
            this.txtMinPcs.Prefix = null;
            this.txtMinPcs.ShowBallonTip = false;
            this.txtMinPcs.ShowErrorIcon = false;
            this.txtMinPcs.ShowMessage = null;
            this.txtMinPcs.Size = new System.Drawing.Size(77, 21);
            this.txtMinPcs.Suffix = null;
            this.txtMinPcs.TabIndex = 11;
            this.txtMinPcs.Text = "0";
            this.txtMinPcs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tltOnControls.SetToolTip(this.txtMinPcs, "");
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(414, 4);
            this.label10.Moveable = false;
            this.label10.Name = "label10";
            this.label10.NameOfControl = null;
            this.label10.Size = new System.Drawing.Size(12, 14);
            this.label10.TabIndex = 1149;
            this.label10.Text = ":";
            this.tltOnControls.SetToolTip(this.label10, "");
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.panel1.Controls.Add(this.txtMinMtrs);
            this.panel1.Controls.Add(this.txtMaxMtrs);
            this.panel1.Controls.Add(this.txtMaxPcs);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtMinPcs);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Location = new System.Drawing.Point(57, 415);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 52);
            this.panel1.TabIndex = 1156;
            this.tltOnControls.SetToolTip(this.panel1, "");
            // 
            // lblSelComp
            // 
            this.lblSelComp.AutoSize = true;
            this.lblSelComp.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelComp.Location = new System.Drawing.Point(596, 18);
            this.lblSelComp.Moveable = false;
            this.lblSelComp.Name = "lblSelComp";
            this.lblSelComp.NameOfControl = "Select Company";
            this.lblSelComp.Size = new System.Drawing.Size(113, 14);
            this.lblSelComp.TabIndex = 90113;
            this.lblSelComp.Text = "Select Company";
            this.tltOnControls.SetToolTip(this.lblSelComp, "");
            // 
            // chkCompany
            // 
            this.chkCompany.FormattingEnabled = true;
            this.chkCompany.HelpText = null;
            this.chkCompany.Location = new System.Drawing.Point(595, 39);
            this.chkCompany.Moveable = false;
            this.chkCompany.Name = "chkCompany";
            this.chkCompany.Size = new System.Drawing.Size(115, 109);
            this.chkCompany.TabIndex = 90112;
            this.tltOnControls.SetToolTip(this.chkCompany, "");
            // 
            // frmCatalogMaster
            // 
            this.ClientSize = new System.Drawing.Size(808, 545);
            this.Name = "frmCatalogMaster";
            this.Load += new System.EventHandler(this.frmCatalogMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlDetail.ResumeLayout(false);
            this.pnlDetail.PerformLayout();
            this.PnlFooter.ResumeLayout(false);
            this.PnlFooter.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal CIS_CLibrary.CIS_CheckBox ChkActive;
        internal CIS_CLibrary.CIS_Textbox txtCatalogName;
        internal CIS_CLibrary.CIS_TextLabel lblCatalogNameColon;
        internal CIS_CLibrary.CIS_TextLabel lblCatalogName;
        private CIS_CLibrary.ToolTip.CIS_ToolTip tltOnControls;
        public CIS_CLibrary.CIS_Textbox txtCode;
        internal CIS_CLibrary.CIS_Textbox txtAliasName;
        internal CIS_CLibrary.CIS_TextLabel lblaliasNameColon;
        internal CIS_CLibrary.CIS_TextLabel lblAliasName;
        internal System.Windows.Forms.Panel pnlDetail;
        internal System.Windows.Forms.Panel PnlFooter;
        internal CIS_CLibrary.CIS_TextLabel txtNoOfDesign;
        internal CIS_CLibrary.CIS_TextLabel lblNoofDesign;
        internal CIS_CLibrary.CIS_TextLabel txtMtrs;
        internal CIS_CLibrary.CIS_TextLabel lblMtrs;
        internal CIS_CLibrary.CIS_TextLabel label2;
        internal CIS_CLibrary.CIS_TextLabel label1;
        private System.Windows.Forms.Panel panel1;
        internal CIS_CLibrary.CIS_Textbox txtMinMtrs;
        internal CIS_CLibrary.CIS_Textbox txtMaxMtrs;
        internal CIS_CLibrary.CIS_Textbox txtMaxPcs;
        internal CIS_CLibrary.CIS_TextLabel label6;
        internal CIS_CLibrary.CIS_TextLabel label7;
        internal CIS_CLibrary.CIS_TextLabel label5;
        internal CIS_CLibrary.CIS_TextLabel label8;
        internal CIS_CLibrary.CIS_TextLabel label4;
        internal CIS_CLibrary.CIS_TextLabel label3;
        internal CIS_CLibrary.CIS_TextLabel label9;
        internal CIS_CLibrary.CIS_Textbox txtMinPcs;
        internal CIS_CLibrary.CIS_TextLabel label10;
        private Crocus_CClib.DataGridViewX GrdMain;
        internal CIS_CLibrary.CIS_TextLabel lblSelComp;
        private CIS_CLibrary.CIS_CheckBoxList chkCompany;
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
