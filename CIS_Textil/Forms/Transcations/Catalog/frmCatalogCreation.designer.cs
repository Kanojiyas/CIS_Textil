namespace CIS_Textil
{
    partial class frmCatalogCreation
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
            this.cboDeptFrom = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.btnShow = new CIS_CLibrary.CIS_Button();
            this.txtNoOfCatalogs = new CIS_CLibrary.CIS_Textbox();
            this.lblDeptFromColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblDeptFrom = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblNoofCatalogs = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblWeaverColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblCatalogName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblCatalogNameColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.GrdMain = new Crocus_CClib.DataGridViewX();
            this.dtEntryDate = new CIS_CLibrary.CIS_Textbox();
            this.lblEntryDate = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtEntryNo = new CIS_CLibrary.CIS_Textbox();
            this.lblEntryNo = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblEntryNoColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblEntryDateColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.cboCatalogName = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.tltOnControls = new CIS_CLibrary.ToolTip.CIS_ToolTip();
            this.pnlAddless = new System.Windows.Forms.Panel();
            this.GrdAddLess = new Crocus_CClib.DataGridViewX();
            this.PnlTemp = new System.Windows.Forms.Panel();
            this.GrdFooter = new Crocus_CClib.DataGridViewX();
            this.cboProcessor = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.label1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblProcessor = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtRate = new CIS_CLibrary.CIS_Textbox();
            this.lblRate = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.label5 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.dtRefdate = new CIS_CLibrary.CIS_Textbox();
            this.lblRefDate = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtRefNo = new CIS_CLibrary.CIS_Textbox();
            this.lblRefNo = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.label4 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.label6 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.Panel3 = new System.Windows.Forms.Panel();
            this.txtNetAmt = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtAddLessAmt = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblNetAmt = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblAddLess = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.Panel2 = new System.Windows.Forms.Panel();
            this.TxtGrossAmount = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.LblTotalAmount = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.pnlDetail.SuspendLayout();
            this.pnlAddless.SuspendLayout();
            this.PnlTemp.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.Panel2);
            this.pnlContent.Controls.Add(this.Panel3);
            this.pnlContent.Controls.Add(this.PnlTemp);
            this.pnlContent.Controls.Add(this.dtRefdate);
            this.pnlContent.Controls.Add(this.lblRefDate);
            this.pnlContent.Controls.Add(this.txtRefNo);
            this.pnlContent.Controls.Add(this.lblRefNo);
            this.pnlContent.Controls.Add(this.label4);
            this.pnlContent.Controls.Add(this.label6);
            this.pnlContent.Controls.Add(this.txtRate);
            this.pnlContent.Controls.Add(this.lblRate);
            this.pnlContent.Controls.Add(this.label5);
            this.pnlContent.Controls.Add(this.cboProcessor);
            this.pnlContent.Controls.Add(this.label1);
            this.pnlContent.Controls.Add(this.lblProcessor);
            this.pnlContent.Controls.Add(this.pnlAddless);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.Add(this.cboCatalogName);
            this.pnlContent.Controls.Add(this.cboDeptFrom);
            this.pnlContent.Controls.Add(this.btnShow);
            this.pnlContent.Controls.Add(this.txtNoOfCatalogs);
            this.pnlContent.Controls.Add(this.lblDeptFromColon);
            this.pnlContent.Controls.Add(this.lblDeptFrom);
            this.pnlContent.Controls.Add(this.lblNoofCatalogs);
            this.pnlContent.Controls.Add(this.lblWeaverColon);
            this.pnlContent.Controls.Add(this.lblCatalogNameColon);
            this.pnlContent.Controls.Add(this.pnlDetail);
            this.pnlContent.Controls.Add(this.lblCatalogName);
            this.pnlContent.Controls.Add(this.dtEntryDate);
            this.pnlContent.Controls.Add(this.lblEntryDate);
            this.pnlContent.Controls.Add(this.txtEntryNo);
            this.pnlContent.Controls.Add(this.lblEntryNo);
            this.pnlContent.Controls.Add(this.lblEntryNoColon);
            this.pnlContent.Controls.Add(this.lblEntryDateColon);
            this.tltOnControls.SetToolTip(this.pnlContent, "");
            this.pnlContent.Controls.SetChildIndex(this.lblEntryDateColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryNoColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtEntryNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryDate, 0);
            this.pnlContent.Controls.SetChildIndex(this.dtEntryDate, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblCatalogName, 0);
            this.pnlContent.Controls.SetChildIndex(this.pnlDetail, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblCatalogNameColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblWeaverColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblNoofCatalogs, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblDeptFrom, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblDeptFromColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtNoOfCatalogs, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnShow, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboDeptFrom, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboCatalogName, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.pnlAddless, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblProcessor, 0);
            this.pnlContent.Controls.SetChildIndex(this.label1, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboProcessor, 0);
            this.pnlContent.Controls.SetChildIndex(this.label5, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblRate, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtRate, 0);
            this.pnlContent.Controls.SetChildIndex(this.label6, 0);
            this.pnlContent.Controls.SetChildIndex(this.label4, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblRefNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtRefNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblRefDate, 0);
            this.pnlContent.Controls.SetChildIndex(this.dtRefdate, 0);
            this.pnlContent.Controls.SetChildIndex(this.PnlTemp, 0);
            this.pnlContent.Controls.SetChildIndex(this.Panel3, 0);
            this.pnlContent.Controls.SetChildIndex(this.Panel2, 0);
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
            // cboDeptFrom
            // 
            this.cboDeptFrom.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboDeptFrom.AutoComplete = false;
            this.cboDeptFrom.AutoDropdown = false;
            this.cboDeptFrom.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboDeptFrom.BackColorEven = System.Drawing.Color.White;
            this.cboDeptFrom.BackColorOdd = System.Drawing.Color.White;
            this.cboDeptFrom.ColumnNames = "";
            this.cboDeptFrom.ColumnWidthDefault = 175;
            this.cboDeptFrom.ColumnWidths = "";
            this.cboDeptFrom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboDeptFrom.Fill_ComboID = 0;
            this.cboDeptFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.cboDeptFrom.FormattingEnabled = true;
            this.cboDeptFrom.GroupType = 0;
            this.cboDeptFrom.HelpText = "Select Department";
            this.cboDeptFrom.IsMandatory = true;
            this.cboDeptFrom.LinkedColumnIndex = 0;
            this.cboDeptFrom.LinkedTextBox = null;
            this.cboDeptFrom.Location = new System.Drawing.Point(201, 38);
            this.cboDeptFrom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboDeptFrom.Moveable = false;
            this.cboDeptFrom.Name = "cboDeptFrom";
            this.cboDeptFrom.NameOfControl = "Description";
            this.cboDeptFrom.OpenForm = null;
            this.cboDeptFrom.ShowBallonTip = false;
            this.cboDeptFrom.Size = new System.Drawing.Size(291, 23);
            this.cboDeptFrom.TabIndex = 5;
            this.tltOnControls.SetToolTip(this.cboDeptFrom, "");
            // 
            // btnShow
            // 
            this.btnShow.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnShow.BackColor = System.Drawing.Color.CadetBlue;
            this.btnShow.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow.Location = new System.Drawing.Point(811, 92);
            this.btnShow.Moveable = false;
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(162, 25);
            this.btnShow.TabIndex = 10;
            this.btnShow.Text = "Show";
            this.tltOnControls.SetToolTip(this.btnShow, "");
            this.btnShow.UseVisualStyleBackColor = false;
            this.btnShow.Click += new System.EventHandler(this.btnShow_Click);
            // 
            // txtNoOfCatalogs
            // 
            this.txtNoOfCatalogs.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtNoOfCatalogs.AutoFillDate = false;
            this.txtNoOfCatalogs.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtNoOfCatalogs.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtNoOfCatalogs.CheckForSymbol = null;
            this.txtNoOfCatalogs.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtNoOfCatalogs.DecimalPlace = 0;
            this.txtNoOfCatalogs.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtNoOfCatalogs.HelpText = "Enter No of Catalogs";
            this.txtNoOfCatalogs.HoldMyText = null;
            this.txtNoOfCatalogs.IsMandatory = true;
            this.txtNoOfCatalogs.IsSingleQuote = true;
            this.txtNoOfCatalogs.IsSysmbol = false;
            this.txtNoOfCatalogs.Location = new System.Drawing.Point(681, 64);
            this.txtNoOfCatalogs.Mask = null;
            this.txtNoOfCatalogs.MaxLength = 20;
            this.txtNoOfCatalogs.Moveable = false;
            this.txtNoOfCatalogs.Name = "txtNoOfCatalogs";
            this.txtNoOfCatalogs.NameOfControl = "No Of Catalogs";
            this.txtNoOfCatalogs.Prefix = null;
            this.txtNoOfCatalogs.ShowBallonTip = false;
            this.txtNoOfCatalogs.ShowErrorIcon = false;
            this.txtNoOfCatalogs.ShowMessage = null;
            this.txtNoOfCatalogs.Size = new System.Drawing.Size(85, 22);
            this.txtNoOfCatalogs.Suffix = null;
            this.txtNoOfCatalogs.TabIndex = 8;
            this.txtNoOfCatalogs.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tltOnControls.SetToolTip(this.txtNoOfCatalogs, "");
            this.txtNoOfCatalogs.TextChanged += new System.EventHandler(this.txtNoOfCatalogs_TextChanged);
            // 
            // lblDeptFromColon
            // 
            this.lblDeptFromColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDeptFromColon.AutoSize = true;
            this.lblDeptFromColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeptFromColon.Location = new System.Drawing.Point(191, 40);
            this.lblDeptFromColon.Moveable = false;
            this.lblDeptFromColon.Name = "lblDeptFromColon";
            this.lblDeptFromColon.NameOfControl = null;
            this.lblDeptFromColon.Size = new System.Drawing.Size(12, 14);
            this.lblDeptFromColon.TabIndex = 337;
            this.lblDeptFromColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblDeptFromColon, "");
            // 
            // lblDeptFrom
            // 
            this.lblDeptFrom.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDeptFrom.AutoSize = true;
            this.lblDeptFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDeptFrom.Location = new System.Drawing.Point(108, 40);
            this.lblDeptFrom.Moveable = false;
            this.lblDeptFrom.Name = "lblDeptFrom";
            this.lblDeptFrom.NameOfControl = null;
            this.lblDeptFrom.Size = new System.Drawing.Size(85, 14);
            this.lblDeptFrom.TabIndex = 335;
            this.lblDeptFrom.Text = "Department";
            this.tltOnControls.SetToolTip(this.lblDeptFrom, "");
            // 
            // lblNoofCatalogs
            // 
            this.lblNoofCatalogs.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblNoofCatalogs.AutoSize = true;
            this.lblNoofCatalogs.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoofCatalogs.Location = new System.Drawing.Point(570, 66);
            this.lblNoofCatalogs.Moveable = false;
            this.lblNoofCatalogs.Name = "lblNoofCatalogs";
            this.lblNoofCatalogs.NameOfControl = null;
            this.lblNoofCatalogs.Size = new System.Drawing.Size(103, 14);
            this.lblNoofCatalogs.TabIndex = 336;
            this.lblNoofCatalogs.Text = "No of Catalogs";
            this.tltOnControls.SetToolTip(this.lblNoofCatalogs, "");
            // 
            // lblWeaverColon
            // 
            this.lblWeaverColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblWeaverColon.AutoSize = true;
            this.lblWeaverColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeaverColon.Location = new System.Drawing.Point(669, 66);
            this.lblWeaverColon.Moveable = false;
            this.lblWeaverColon.Name = "lblWeaverColon";
            this.lblWeaverColon.NameOfControl = null;
            this.lblWeaverColon.Size = new System.Drawing.Size(12, 14);
            this.lblWeaverColon.TabIndex = 338;
            this.lblWeaverColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblWeaverColon, "");
            // 
            // lblCatalogName
            // 
            this.lblCatalogName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCatalogName.AutoSize = true;
            this.lblCatalogName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCatalogName.Location = new System.Drawing.Point(108, 68);
            this.lblCatalogName.Moveable = false;
            this.lblCatalogName.Name = "lblCatalogName";
            this.lblCatalogName.NameOfControl = null;
            this.lblCatalogName.Size = new System.Drawing.Size(57, 14);
            this.lblCatalogName.TabIndex = 330;
            this.lblCatalogName.Text = "Catalog";
            this.tltOnControls.SetToolTip(this.lblCatalogName, "");
            // 
            // lblCatalogNameColon
            // 
            this.lblCatalogNameColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCatalogNameColon.AutoSize = true;
            this.lblCatalogNameColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCatalogNameColon.Location = new System.Drawing.Point(191, 68);
            this.lblCatalogNameColon.Moveable = false;
            this.lblCatalogNameColon.Name = "lblCatalogNameColon";
            this.lblCatalogNameColon.NameOfControl = null;
            this.lblCatalogNameColon.Size = new System.Drawing.Size(12, 14);
            this.lblCatalogNameColon.TabIndex = 331;
            this.lblCatalogNameColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblCatalogNameColon, "");
            // 
            // pnlDetail
            // 
            this.pnlDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDetail.BackColor = System.Drawing.Color.LightBlue;
            this.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDetail.Controls.Add(this.GrdMain);
            this.pnlDetail.Location = new System.Drawing.Point(64, 126);
            this.pnlDetail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(616, 263);
            this.pnlDetail.TabIndex = 11;
            this.tltOnControls.SetToolTip(this.pnlDetail, "");
            // 
            // GrdMain
            // 
            this.GrdMain.blnFormAction = 0;
            this.GrdMain.CompID = 0;
            this.GrdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrdMain.Location = new System.Drawing.Point(0, 0);
            this.GrdMain.Name = "GrdMain";
            this.GrdMain.Size = new System.Drawing.Size(614, 261);
            this.GrdMain.TabIndex = 17;
            this.tltOnControls.SetToolTip(this.GrdMain, "");
            this.GrdMain.YearID = 0;
            // 
            // dtEntryDate
            // 
            this.dtEntryDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtEntryDate.AutoFillDate = true;
            this.dtEntryDate.BackColor = System.Drawing.Color.PapayaWhip;
            this.dtEntryDate.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.dtEntryDate.CheckForSymbol = null;
            this.dtEntryDate.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.ApplyDate;
            this.dtEntryDate.DecimalPlace = 0;
            this.dtEntryDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.dtEntryDate.HelpText = "Enter Entry Date";
            this.dtEntryDate.HoldMyText = null;
            this.dtEntryDate.IsMandatory = true;
            this.dtEntryDate.IsSingleQuote = true;
            this.dtEntryDate.IsSysmbol = false;
            this.dtEntryDate.Location = new System.Drawing.Point(399, 14);
            this.dtEntryDate.Mask = "__/__/____";
            this.dtEntryDate.MaxLength = 10;
            this.dtEntryDate.Moveable = false;
            this.dtEntryDate.Name = "dtEntryDate";
            this.dtEntryDate.NameOfControl = "Entry date";
            this.dtEntryDate.Prefix = null;
            this.dtEntryDate.ShowBallonTip = false;
            this.dtEntryDate.ShowErrorIcon = false;
            this.dtEntryDate.ShowMessage = null;
            this.dtEntryDate.Size = new System.Drawing.Size(94, 22);
            this.dtEntryDate.Suffix = null;
            this.dtEntryDate.TabIndex = 2;
            this.dtEntryDate.Text = "__/__/____";
            this.tltOnControls.SetToolTip(this.dtEntryDate, "");
            // 
            // lblEntryDate
            // 
            this.lblEntryDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEntryDate.AutoSize = true;
            this.lblEntryDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryDate.Location = new System.Drawing.Point(313, 16);
            this.lblEntryDate.Moveable = false;
            this.lblEntryDate.Name = "lblEntryDate";
            this.lblEntryDate.NameOfControl = null;
            this.lblEntryDate.Size = new System.Drawing.Size(77, 14);
            this.lblEntryDate.TabIndex = 328;
            this.lblEntryDate.Text = "Entry Date";
            this.tltOnControls.SetToolTip(this.lblEntryDate, "");
            // 
            // txtEntryNo
            // 
            this.txtEntryNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEntryNo.AutoFillDate = false;
            this.txtEntryNo.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtEntryNo.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtEntryNo.CheckForSymbol = null;
            this.txtEntryNo.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.NumericWithDecimal;
            this.txtEntryNo.DecimalPlace = 0;
            this.txtEntryNo.Enabled = false;
            this.txtEntryNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtEntryNo.HelpText = "Enter Entry NO.";
            this.txtEntryNo.HoldMyText = null;
            this.txtEntryNo.IsMandatory = true;
            this.txtEntryNo.IsSingleQuote = true;
            this.txtEntryNo.IsSysmbol = false;
            this.txtEntryNo.Location = new System.Drawing.Point(201, 13);
            this.txtEntryNo.Mask = null;
            this.txtEntryNo.MaxLength = 20;
            this.txtEntryNo.Moveable = false;
            this.txtEntryNo.Name = "txtEntryNo";
            this.txtEntryNo.NameOfControl = "Entey No";
            this.txtEntryNo.Prefix = null;
            this.txtEntryNo.ShowBallonTip = false;
            this.txtEntryNo.ShowErrorIcon = false;
            this.txtEntryNo.ShowMessage = null;
            this.txtEntryNo.Size = new System.Drawing.Size(85, 22);
            this.txtEntryNo.Suffix = null;
            this.txtEntryNo.TabIndex = 1;
            this.txtEntryNo.Text = "0";
            this.txtEntryNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tltOnControls.SetToolTip(this.txtEntryNo, "");
            // 
            // lblEntryNo
            // 
            this.lblEntryNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEntryNo.AutoSize = true;
            this.lblEntryNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryNo.Location = new System.Drawing.Point(108, 15);
            this.lblEntryNo.Moveable = false;
            this.lblEntryNo.Name = "lblEntryNo";
            this.lblEntryNo.NameOfControl = null;
            this.lblEntryNo.Size = new System.Drawing.Size(68, 14);
            this.lblEntryNo.TabIndex = 326;
            this.lblEntryNo.Text = "Entry No.";
            this.tltOnControls.SetToolTip(this.lblEntryNo, "");
            // 
            // lblEntryNoColon
            // 
            this.lblEntryNoColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEntryNoColon.AutoSize = true;
            this.lblEntryNoColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryNoColon.Location = new System.Drawing.Point(191, 14);
            this.lblEntryNoColon.Moveable = false;
            this.lblEntryNoColon.Name = "lblEntryNoColon";
            this.lblEntryNoColon.NameOfControl = null;
            this.lblEntryNoColon.Size = new System.Drawing.Size(12, 14);
            this.lblEntryNoColon.TabIndex = 327;
            this.lblEntryNoColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblEntryNoColon, "");
            // 
            // lblEntryDateColon
            // 
            this.lblEntryDateColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEntryDateColon.AutoSize = true;
            this.lblEntryDateColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryDateColon.Location = new System.Drawing.Point(388, 16);
            this.lblEntryDateColon.Moveable = false;
            this.lblEntryDateColon.Name = "lblEntryDateColon";
            this.lblEntryDateColon.NameOfControl = null;
            this.lblEntryDateColon.Size = new System.Drawing.Size(12, 14);
            this.lblEntryDateColon.TabIndex = 329;
            this.lblEntryDateColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblEntryDateColon, "");
            // 
            // cboCatalogName
            // 
            this.cboCatalogName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboCatalogName.AutoComplete = false;
            this.cboCatalogName.AutoDropdown = false;
            this.cboCatalogName.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboCatalogName.BackColorEven = System.Drawing.Color.White;
            this.cboCatalogName.BackColorOdd = System.Drawing.Color.White;
            this.cboCatalogName.ColumnNames = "";
            this.cboCatalogName.ColumnWidthDefault = 175;
            this.cboCatalogName.ColumnWidths = "";
            this.cboCatalogName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboCatalogName.Fill_ComboID = 0;
            this.cboCatalogName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.cboCatalogName.FormattingEnabled = true;
            this.cboCatalogName.GroupType = 0;
            this.cboCatalogName.HelpText = "Select Catalog";
            this.cboCatalogName.IsMandatory = true;
            this.cboCatalogName.LinkedColumnIndex = 0;
            this.cboCatalogName.LinkedTextBox = null;
            this.cboCatalogName.Location = new System.Drawing.Point(201, 64);
            this.cboCatalogName.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboCatalogName.Moveable = false;
            this.cboCatalogName.Name = "cboCatalogName";
            this.cboCatalogName.NameOfControl = null;
            this.cboCatalogName.OpenForm = null;
            this.cboCatalogName.ShowBallonTip = false;
            this.cboCatalogName.Size = new System.Drawing.Size(291, 23);
            this.cboCatalogName.TabIndex = 7;
            this.tltOnControls.SetToolTip(this.cboCatalogName, "");
            // 
            // txtCode
            // 
            this.txtCode.AutoFillDate = false;
            this.txtCode.BackColor = System.Drawing.Color.White;
            this.txtCode.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtCode.CheckForSymbol = null;
            this.txtCode.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.NumericWithDecimal;
            this.txtCode.DecimalPlace = 0;
            this.txtCode.Enabled = false;
            this.txtCode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.HelpText = "";
            this.txtCode.HoldMyText = null;
            this.txtCode.IsMandatory = false;
            this.txtCode.IsSingleQuote = true;
            this.txtCode.IsSysmbol = false;
            this.txtCode.Location = new System.Drawing.Point(3, -1);
            this.txtCode.Mask = null;
            this.txtCode.MaxLength = 20;
            this.txtCode.Moveable = false;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(24, 22);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 343;
            this.txtCode.Text = "0";
            this.txtCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tltOnControls.SetToolTip(this.txtCode, "");
            this.txtCode.Visible = false;
            // 
            // tltOnControls
            // 
            this.tltOnControls.DataProvider = stringDataProvider1;
            this.tltOnControls.LoadText = "";
            this.tltOnControls.ShowToolTip = false;
            // 
            // pnlAddless
            // 
            this.pnlAddless.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.pnlAddless.BackColor = System.Drawing.Color.LightBlue;
            this.pnlAddless.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddless.Controls.Add(this.GrdAddLess);
            this.pnlAddless.Location = new System.Drawing.Point(682, 126);
            this.pnlAddless.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlAddless.Name = "pnlAddless";
            this.pnlAddless.Size = new System.Drawing.Size(325, 263);
            this.pnlAddless.TabIndex = 12;
            this.tltOnControls.SetToolTip(this.pnlAddless, "");
            // 
            // GrdAddLess
            // 
            this.GrdAddLess.blnFormAction = 0;
            this.GrdAddLess.CompID = 0;
            this.GrdAddLess.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrdAddLess.Location = new System.Drawing.Point(0, 0);
            this.GrdAddLess.Name = "GrdAddLess";
            this.GrdAddLess.Size = new System.Drawing.Size(323, 261);
            this.GrdAddLess.TabIndex = 18;
            this.tltOnControls.SetToolTip(this.GrdAddLess, "");
            this.GrdAddLess.YearID = 0;
            // 
            // PnlTemp
            // 
            this.PnlTemp.BackColor = System.Drawing.Color.LightBlue;
            this.PnlTemp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlTemp.Controls.Add(this.GrdFooter);
            this.PnlTemp.Location = new System.Drawing.Point(682, 427);
            this.PnlTemp.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.PnlTemp.Name = "PnlTemp";
            this.PnlTemp.Size = new System.Drawing.Size(325, 63);
            this.PnlTemp.TabIndex = 344;
            this.tltOnControls.SetToolTip(this.PnlTemp, "");
            this.PnlTemp.Visible = false;
            // 
            // GrdFooter
            // 
            this.GrdFooter.blnFormAction = 0;
            this.GrdFooter.CompID = 0;
            this.GrdFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrdFooter.Location = new System.Drawing.Point(0, 0);
            this.GrdFooter.Name = "GrdFooter";
            this.GrdFooter.Size = new System.Drawing.Size(323, 61);
            this.GrdFooter.TabIndex = 17;
            this.tltOnControls.SetToolTip(this.GrdFooter, "");
            this.GrdFooter.YearID = 0;
            // 
            // cboProcessor
            // 
            this.cboProcessor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboProcessor.AutoComplete = false;
            this.cboProcessor.AutoDropdown = false;
            this.cboProcessor.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboProcessor.BackColorEven = System.Drawing.Color.White;
            this.cboProcessor.BackColorOdd = System.Drawing.Color.White;
            this.cboProcessor.ColumnNames = "";
            this.cboProcessor.ColumnWidthDefault = 175;
            this.cboProcessor.ColumnWidths = "";
            this.cboProcessor.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboProcessor.Fill_ComboID = 0;
            this.cboProcessor.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.cboProcessor.FormattingEnabled = true;
            this.cboProcessor.GroupType = 0;
            this.cboProcessor.HelpText = "Select Processor";
            this.cboProcessor.IsMandatory = true;
            this.cboProcessor.LinkedColumnIndex = 0;
            this.cboProcessor.LinkedTextBox = null;
            this.cboProcessor.Location = new System.Drawing.Point(682, 38);
            this.cboProcessor.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboProcessor.Moveable = false;
            this.cboProcessor.Name = "cboProcessor";
            this.cboProcessor.NameOfControl = "Processor";
            this.cboProcessor.OpenForm = null;
            this.cboProcessor.ShowBallonTip = false;
            this.cboProcessor.Size = new System.Drawing.Size(291, 23);
            this.cboProcessor.TabIndex = 6;
            this.tltOnControls.SetToolTip(this.cboProcessor, "");
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(669, 39);
            this.label1.Moveable = false;
            this.label1.Name = "label1";
            this.label1.NameOfControl = null;
            this.label1.Size = new System.Drawing.Size(12, 14);
            this.label1.TabIndex = 350;
            this.label1.Text = ":";
            this.tltOnControls.SetToolTip(this.label1, "");
            // 
            // lblProcessor
            // 
            this.lblProcessor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblProcessor.AutoSize = true;
            this.lblProcessor.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProcessor.Location = new System.Drawing.Point(570, 40);
            this.lblProcessor.Moveable = false;
            this.lblProcessor.Name = "lblProcessor";
            this.lblProcessor.NameOfControl = null;
            this.lblProcessor.Size = new System.Drawing.Size(73, 14);
            this.lblProcessor.TabIndex = 349;
            this.lblProcessor.Text = "Processor";
            this.tltOnControls.SetToolTip(this.lblProcessor, "");
            // 
            // txtRate
            // 
            this.txtRate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtRate.AutoFillDate = false;
            this.txtRate.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtRate.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtRate.CheckForSymbol = null;
            this.txtRate.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtRate.DecimalPlace = 0;
            this.txtRate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtRate.HelpText = "Enter Rate";
            this.txtRate.HoldMyText = null;
            this.txtRate.IsMandatory = true;
            this.txtRate.IsSingleQuote = true;
            this.txtRate.IsSysmbol = false;
            this.txtRate.Location = new System.Drawing.Point(880, 64);
            this.txtRate.Mask = null;
            this.txtRate.MaxLength = 20;
            this.txtRate.Moveable = false;
            this.txtRate.Name = "txtRate";
            this.txtRate.NameOfControl = "Rate";
            this.txtRate.Prefix = null;
            this.txtRate.ShowBallonTip = false;
            this.txtRate.ShowErrorIcon = false;
            this.txtRate.ShowMessage = null;
            this.txtRate.Size = new System.Drawing.Size(93, 22);
            this.txtRate.Suffix = null;
            this.txtRate.TabIndex = 9;
            this.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tltOnControls.SetToolTip(this.txtRate, "");
            this.txtRate.TextChanged += new System.EventHandler(this.txtRate_TextChanged);
            // 
            // lblRate
            // 
            this.lblRate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRate.AutoSize = true;
            this.lblRate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRate.Location = new System.Drawing.Point(807, 66);
            this.lblRate.Moveable = false;
            this.lblRate.Name = "lblRate";
            this.lblRate.NameOfControl = null;
            this.lblRate.Size = new System.Drawing.Size(37, 14);
            this.lblRate.TabIndex = 352;
            this.lblRate.Text = "Rate";
            this.tltOnControls.SetToolTip(this.lblRate, "");
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(869, 66);
            this.label5.Moveable = false;
            this.label5.Name = "label5";
            this.label5.NameOfControl = null;
            this.label5.Size = new System.Drawing.Size(12, 14);
            this.label5.TabIndex = 353;
            this.label5.Text = ":";
            this.tltOnControls.SetToolTip(this.label5, "");
            // 
            // dtRefdate
            // 
            this.dtRefdate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtRefdate.AutoFillDate = true;
            this.dtRefdate.BackColor = System.Drawing.Color.PapayaWhip;
            this.dtRefdate.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.dtRefdate.CheckForSymbol = null;
            this.dtRefdate.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.ApplyDate;
            this.dtRefdate.DecimalPlace = 0;
            this.dtRefdate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.dtRefdate.HelpText = "Enter Ref Date";
            this.dtRefdate.HoldMyText = null;
            this.dtRefdate.IsMandatory = true;
            this.dtRefdate.IsSingleQuote = true;
            this.dtRefdate.IsSysmbol = false;
            this.dtRefdate.Location = new System.Drawing.Point(879, 13);
            this.dtRefdate.Mask = "__/__/____";
            this.dtRefdate.MaxLength = 10;
            this.dtRefdate.Moveable = false;
            this.dtRefdate.Name = "dtRefdate";
            this.dtRefdate.NameOfControl = "Ref Date";
            this.dtRefdate.Prefix = null;
            this.dtRefdate.ShowBallonTip = false;
            this.dtRefdate.ShowErrorIcon = false;
            this.dtRefdate.ShowMessage = null;
            this.dtRefdate.Size = new System.Drawing.Size(94, 22);
            this.dtRefdate.Suffix = null;
            this.dtRefdate.TabIndex = 4;
            this.dtRefdate.Text = "__/__/____";
            this.tltOnControls.SetToolTip(this.dtRefdate, "");
            // 
            // lblRefDate
            // 
            this.lblRefDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRefDate.AutoSize = true;
            this.lblRefDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefDate.Location = new System.Drawing.Point(807, 15);
            this.lblRefDate.Moveable = false;
            this.lblRefDate.Name = "lblRefDate";
            this.lblRefDate.NameOfControl = null;
            this.lblRefDate.Size = new System.Drawing.Size(64, 14);
            this.lblRefDate.TabIndex = 90191;
            this.lblRefDate.Text = "Ref Date";
            this.tltOnControls.SetToolTip(this.lblRefDate, "");
            // 
            // txtRefNo
            // 
            this.txtRefNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtRefNo.AutoFillDate = false;
            this.txtRefNo.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtRefNo.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtRefNo.CheckForSymbol = null;
            this.txtRefNo.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.NumericWithDecimal;
            this.txtRefNo.DecimalPlace = 0;
            this.txtRefNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtRefNo.HelpText = "Enter Ref No.";
            this.txtRefNo.HoldMyText = null;
            this.txtRefNo.IsMandatory = true;
            this.txtRefNo.IsSingleQuote = true;
            this.txtRefNo.IsSysmbol = false;
            this.txtRefNo.Location = new System.Drawing.Point(681, 13);
            this.txtRefNo.Mask = null;
            this.txtRefNo.MaxLength = 20;
            this.txtRefNo.Moveable = false;
            this.txtRefNo.Name = "txtRefNo";
            this.txtRefNo.NameOfControl = "Ref Date";
            this.txtRefNo.Prefix = null;
            this.txtRefNo.ShowBallonTip = false;
            this.txtRefNo.ShowErrorIcon = false;
            this.txtRefNo.ShowMessage = null;
            this.txtRefNo.Size = new System.Drawing.Size(85, 22);
            this.txtRefNo.Suffix = null;
            this.txtRefNo.TabIndex = 3;
            this.txtRefNo.Text = "0";
            this.txtRefNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tltOnControls.SetToolTip(this.txtRefNo, "");
            // 
            // lblRefNo
            // 
            this.lblRefNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRefNo.AutoSize = true;
            this.lblRefNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefNo.Location = new System.Drawing.Point(570, 15);
            this.lblRefNo.Moveable = false;
            this.lblRefNo.Name = "lblRefNo";
            this.lblRefNo.NameOfControl = null;
            this.lblRefNo.Size = new System.Drawing.Size(55, 14);
            this.lblRefNo.TabIndex = 90189;
            this.lblRefNo.Text = "Ref No.";
            this.tltOnControls.SetToolTip(this.lblRefNo, "");
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(669, 14);
            this.label4.Moveable = false;
            this.label4.Name = "label4";
            this.label4.NameOfControl = null;
            this.label4.Size = new System.Drawing.Size(12, 14);
            this.label4.TabIndex = 90190;
            this.label4.Text = ":";
            this.tltOnControls.SetToolTip(this.label4, "");
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(868, 15);
            this.label6.Moveable = false;
            this.label6.Name = "label6";
            this.label6.NameOfControl = null;
            this.label6.Size = new System.Drawing.Size(12, 14);
            this.label6.TabIndex = 90192;
            this.label6.Text = ":";
            this.tltOnControls.SetToolTip(this.label6, "");
            // 
            // Panel3
            // 
            this.Panel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Panel3.BackColor = System.Drawing.Color.LightBlue;
            this.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel3.Controls.Add(this.txtNetAmt);
            this.Panel3.Controls.Add(this.txtAddLessAmt);
            this.Panel3.Controls.Add(this.lblNetAmt);
            this.Panel3.Controls.Add(this.lblAddLess);
            this.Panel3.Location = new System.Drawing.Point(681, 393);
            this.Panel3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(325, 28);
            this.Panel3.TabIndex = 90193;
            this.tltOnControls.SetToolTip(this.Panel3, "");
            // 
            // txtNetAmt
            // 
            this.txtNetAmt.AutoSize = true;
            this.txtNetAmt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetAmt.ForeColor = System.Drawing.Color.Brown;
            this.txtNetAmt.Location = new System.Drawing.Point(244, 6);
            this.txtNetAmt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtNetAmt.Moveable = false;
            this.txtNetAmt.Name = "txtNetAmt";
            this.txtNetAmt.NameOfControl = null;
            this.txtNetAmt.Size = new System.Drawing.Size(38, 14);
            this.txtNetAmt.TabIndex = 31;
            this.txtNetAmt.Text = "0.00";
            this.tltOnControls.SetToolTip(this.txtNetAmt, "");
            // 
            // txtAddLessAmt
            // 
            this.txtAddLessAmt.AutoSize = true;
            this.txtAddLessAmt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddLessAmt.ForeColor = System.Drawing.Color.Brown;
            this.txtAddLessAmt.Location = new System.Drawing.Point(80, 6);
            this.txtAddLessAmt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtAddLessAmt.Moveable = false;
            this.txtAddLessAmt.Name = "txtAddLessAmt";
            this.txtAddLessAmt.NameOfControl = null;
            this.txtAddLessAmt.Size = new System.Drawing.Size(38, 14);
            this.txtAddLessAmt.TabIndex = 29;
            this.txtAddLessAmt.Text = "0.00";
            this.tltOnControls.SetToolTip(this.txtAddLessAmt, "");
            // 
            // lblNetAmt
            // 
            this.lblNetAmt.AutoSize = true;
            this.lblNetAmt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetAmt.Location = new System.Drawing.Point(155, 6);
            this.lblNetAmt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNetAmt.Moveable = false;
            this.lblNetAmt.Name = "lblNetAmt";
            this.lblNetAmt.NameOfControl = null;
            this.lblNetAmt.Size = new System.Drawing.Size(93, 14);
            this.lblNetAmt.TabIndex = 30;
            this.lblNetAmt.Text = "Net Amount :";
            this.tltOnControls.SetToolTip(this.lblNetAmt, "");
            // 
            // lblAddLess
            // 
            this.lblAddLess.AutoSize = true;
            this.lblAddLess.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddLess.Location = new System.Drawing.Point(6, 6);
            this.lblAddLess.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAddLess.Moveable = false;
            this.lblAddLess.Name = "lblAddLess";
            this.lblAddLess.NameOfControl = null;
            this.lblAddLess.Size = new System.Drawing.Size(79, 14);
            this.lblAddLess.TabIndex = 28;
            this.lblAddLess.Text = "Add/Less :";
            this.tltOnControls.SetToolTip(this.lblAddLess, "");
            // 
            // Panel2
            // 
            this.Panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Panel2.BackColor = System.Drawing.Color.LightBlue;
            this.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel2.Controls.Add(this.TxtGrossAmount);
            this.Panel2.Controls.Add(this.LblTotalAmount);
            this.Panel2.Location = new System.Drawing.Point(64, 393);
            this.Panel2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(615, 28);
            this.Panel2.TabIndex = 90194;
            this.tltOnControls.SetToolTip(this.Panel2, "");
            // 
            // TxtGrossAmount
            // 
            this.TxtGrossAmount.AutoSize = true;
            this.TxtGrossAmount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtGrossAmount.ForeColor = System.Drawing.Color.Brown;
            this.TxtGrossAmount.Location = new System.Drawing.Point(532, 6);
            this.TxtGrossAmount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TxtGrossAmount.Moveable = false;
            this.TxtGrossAmount.Name = "TxtGrossAmount";
            this.TxtGrossAmount.NameOfControl = null;
            this.TxtGrossAmount.Size = new System.Drawing.Size(38, 14);
            this.TxtGrossAmount.TabIndex = 31;
            this.TxtGrossAmount.Text = "0.00";
            this.tltOnControls.SetToolTip(this.TxtGrossAmount, "");
            // 
            // LblTotalAmount
            // 
            this.LblTotalAmount.AutoSize = true;
            this.LblTotalAmount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalAmount.Location = new System.Drawing.Point(427, 6);
            this.LblTotalAmount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTotalAmount.Moveable = false;
            this.LblTotalAmount.Name = "LblTotalAmount";
            this.LblTotalAmount.NameOfControl = null;
            this.LblTotalAmount.Size = new System.Drawing.Size(108, 14);
            this.LblTotalAmount.TabIndex = 30;
            this.LblTotalAmount.Text = "Gross Amount :";
            this.tltOnControls.SetToolTip(this.LblTotalAmount, "");
            // 
            // frmCatalogCreation
            // 
            this.ClientSize = new System.Drawing.Size(1072, 547);
            this.Name = "frmCatalogCreation";
            this.Load += new System.EventHandler(this.frmFabricInward_Load);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlDetail.ResumeLayout(false);
            this.pnlAddless.ResumeLayout(false);
            this.PnlTemp.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboDeptFrom;
        internal CIS_CLibrary.CIS_Button btnShow;
        internal CIS_CLibrary.CIS_Textbox txtNoOfCatalogs;
        internal CIS_CLibrary.CIS_TextLabel lblDeptFromColon;
        internal CIS_CLibrary.CIS_TextLabel lblDeptFrom;
        internal CIS_CLibrary.CIS_TextLabel lblNoofCatalogs;
        internal CIS_CLibrary.CIS_TextLabel lblWeaverColon;
        internal CIS_CLibrary.CIS_TextLabel lblCatalogName;
        internal CIS_CLibrary.CIS_TextLabel lblCatalogNameColon;
        internal System.Windows.Forms.Panel pnlDetail;
        internal CIS_CLibrary.CIS_Textbox dtEntryDate;
        internal CIS_CLibrary.CIS_TextLabel lblEntryDate;
        internal CIS_CLibrary.CIS_Textbox txtEntryNo;
        internal CIS_CLibrary.CIS_TextLabel lblEntryNo;
        internal CIS_CLibrary.CIS_TextLabel lblEntryNoColon;
        internal CIS_CLibrary.CIS_TextLabel lblEntryDateColon;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboCatalogName;
        public CIS_CLibrary.CIS_Textbox txtCode;
        private CIS_CLibrary.ToolTip.CIS_ToolTip tltOnControls;
        internal System.Windows.Forms.Panel pnlAddless;
        internal System.Windows.Forms.Panel PnlTemp;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboProcessor;
        internal CIS_CLibrary.CIS_TextLabel label1;
        internal CIS_CLibrary.CIS_TextLabel lblProcessor;
        internal CIS_CLibrary.CIS_Textbox txtRate;
        internal CIS_CLibrary.CIS_TextLabel lblRate;
        internal CIS_CLibrary.CIS_TextLabel label5;
        internal CIS_CLibrary.CIS_Textbox dtRefdate;
        internal CIS_CLibrary.CIS_TextLabel lblRefDate;
        internal CIS_CLibrary.CIS_Textbox txtRefNo;
        internal CIS_CLibrary.CIS_TextLabel lblRefNo;
        internal CIS_CLibrary.CIS_TextLabel label4;
        internal CIS_CLibrary.CIS_TextLabel label6;
        private Crocus_CClib.DataGridViewX GrdFooter;
        private Crocus_CClib.DataGridViewX GrdAddLess;
        private Crocus_CClib.DataGridViewX GrdMain;
        internal System.Windows.Forms.Panel Panel3;
        internal CIS_CLibrary.CIS_TextLabel txtNetAmt;
        internal CIS_CLibrary.CIS_TextLabel txtAddLessAmt;
        internal CIS_CLibrary.CIS_TextLabel lblNetAmt;
        internal CIS_CLibrary.CIS_TextLabel lblAddLess;
        internal System.Windows.Forms.Panel Panel2;
        internal CIS_CLibrary.CIS_TextLabel TxtGrossAmount;
        internal CIS_CLibrary.CIS_TextLabel LblTotalAmount;
    }
}
