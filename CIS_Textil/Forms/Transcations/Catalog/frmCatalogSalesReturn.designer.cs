namespace CIS_Textil
{
    partial class frmCatalogSalesReturn
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
            CIS_CLibrary.ToolTip.StringDataProvider stringDataProvider5 = new CIS_CLibrary.ToolTip.StringDataProvider();
            this.lblEntryNo = new System.Windows.Forms.Label();
            this.cboSalesAc = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblSalesAc = new System.Windows.Forms.Label();
            this.lblSalesAcColon = new System.Windows.Forms.Label();
            this.lblLrNo = new System.Windows.Forms.Label();
            this.lblLrNoColon = new System.Windows.Forms.Label();
            this.txtLrNo = new CIS_CLibrary.CIS_Textbox();
            this.lblChallanDtColon = new System.Windows.Forms.Label();
            this.lblLrDate = new System.Windows.Forms.Label();
            this.lblChallanDt = new System.Windows.Forms.Label();
            this.lblLrDateColon = new System.Windows.Forms.Label();
            this.lblRefNoColon = new System.Windows.Forms.Label();
            this.lblRefNo = new System.Windows.Forms.Label();
            this.cboTransport = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.cboParty = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.txtEntryNo = new CIS_CLibrary.CIS_Textbox();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.pnlAddLess = new System.Windows.Forms.Panel();
            this.lblTransportColon = new System.Windows.Forms.Label();
            this.lblTransport = new System.Windows.Forms.Label();
            this.Panel3 = new System.Windows.Forms.Panel();
            this.txtNetAmt = new System.Windows.Forms.Label();
            this.txtAddLessAmt = new System.Windows.Forms.Label();
            this.lblNetAmt = new System.Windows.Forms.Label();
            this.lblAddLess = new System.Windows.Forms.Label();
            this.Panel1 = new System.Windows.Forms.Panel();
            this.TxtGrossAmount = new System.Windows.Forms.Label();
            this.TxtTotalPcs = new System.Windows.Forms.Label();
            this.LblTotalAmount = new System.Windows.Forms.Label();
            this.LblTotalCuts = new System.Windows.Forms.Label();
            this.lblNarrationColon = new System.Windows.Forms.Label();
            this.txtNarration = new CIS_CLibrary.CIS_Textbox();
            this.lblNarration = new System.Windows.Forms.Label();
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.lblPartyColon = new System.Windows.Forms.Label();
            this.lblParty = new System.Windows.Forms.Label();
            this.lblBillDateColon = new System.Windows.Forms.Label();
            this.lblBillDate = new System.Windows.Forms.Label();
            this.lblBillNoColon = new System.Windows.Forms.Label();
            this.txtBillNo = new CIS_CLibrary.CIS_Textbox();
            this.lblBillNo = new System.Windows.Forms.Label();
            this.lblEntryDateColon = new System.Windows.Forms.Label();
            this.lblEntryDate = new System.Windows.Forms.Label();
            this.lblEntryNoColon = new System.Windows.Forms.Label();
            this.tltOnControls = new CIS_CLibrary.ToolTip.CIS_ToolTip();
            this.btnSelect = new CIS_CLibrary.CIS_Button();
            this.lblReturnType = new System.Windows.Forms.Label();
            this.cboReturnType = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblReturnTypeColon = new System.Windows.Forms.Label();
            this.cboBroker = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblBroker = new System.Windows.Forms.Label();
            this.lblBrokerColon = new System.Windows.Forms.Label();
            this.cboDepartment = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.lblDepartmentColon = new System.Windows.Forms.Label();
            this.txtRefNo = new CIS_CLibrary.CIS_Textbox();
            this.dtEntryDate = new CIS_CLibrary.CIS_Textbox();
            this.dtChallanDate = new CIS_CLibrary.CIS_Textbox();
            this.dtBillDate = new CIS_CLibrary.CIS_Textbox();
            this.dtLrDate = new CIS_CLibrary.CIS_Textbox();
            this.GrdMain = new Crocus_CClib.DataGridViewX();
            this.GrdDtls = new Crocus_CClib.DataGridViewX();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.pnlDetail.SuspendLayout();
            this.pnlAddLess.SuspendLayout();
            this.Panel3.SuspendLayout();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.dtLrDate);
            this.pnlContent.Controls.Add(this.dtBillDate);
            this.pnlContent.Controls.Add(this.dtChallanDate);
            this.pnlContent.Controls.Add(this.dtEntryDate);
            this.pnlContent.Controls.Add(this.txtRefNo);
            this.pnlContent.Controls.Add(this.cboDepartment);
            this.pnlContent.Controls.Add(this.lblDepartment);
            this.pnlContent.Controls.Add(this.lblDepartmentColon);
            this.pnlContent.Controls.Add(this.cboBroker);
            this.pnlContent.Controls.Add(this.lblBroker);
            this.pnlContent.Controls.Add(this.lblBrokerColon);
            this.pnlContent.Controls.Add(this.lblReturnType);
            this.pnlContent.Controls.Add(this.cboReturnType);
            this.pnlContent.Controls.Add(this.lblReturnTypeColon);
            this.pnlContent.Controls.Add(this.btnSelect);
            this.pnlContent.Controls.Add(this.lblEntryNo);
            this.pnlContent.Controls.Add(this.cboSalesAc);
            this.pnlContent.Controls.Add(this.lblSalesAc);
            this.pnlContent.Controls.Add(this.lblSalesAcColon);
            this.pnlContent.Controls.Add(this.lblLrNo);
            this.pnlContent.Controls.Add(this.lblLrNoColon);
            this.pnlContent.Controls.Add(this.txtLrNo);
            this.pnlContent.Controls.Add(this.lblChallanDtColon);
            this.pnlContent.Controls.Add(this.lblLrDate);
            this.pnlContent.Controls.Add(this.lblChallanDt);
            this.pnlContent.Controls.Add(this.lblLrDateColon);
            this.pnlContent.Controls.Add(this.lblRefNoColon);
            this.pnlContent.Controls.Add(this.lblRefNo);
            this.pnlContent.Controls.Add(this.cboTransport);
            this.pnlContent.Controls.Add(this.cboParty);
            this.pnlContent.Controls.Add(this.txtEntryNo);
            this.pnlContent.Controls.Add(this.pnlDetail);
            this.pnlContent.Controls.Add(this.pnlAddLess);
            this.pnlContent.Controls.Add(this.lblTransportColon);
            this.pnlContent.Controls.Add(this.lblTransport);
            this.pnlContent.Controls.Add(this.Panel3);
            this.pnlContent.Controls.Add(this.Panel1);
            this.pnlContent.Controls.Add(this.lblNarrationColon);
            this.pnlContent.Controls.Add(this.txtNarration);
            this.pnlContent.Controls.Add(this.lblNarration);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.Add(this.lblPartyColon);
            this.pnlContent.Controls.Add(this.lblParty);
            this.pnlContent.Controls.Add(this.lblBillDateColon);
            this.pnlContent.Controls.Add(this.lblBillDate);
            this.pnlContent.Controls.Add(this.lblBillNoColon);
            this.pnlContent.Controls.Add(this.txtBillNo);
            this.pnlContent.Controls.Add(this.lblBillNo);
            this.pnlContent.Controls.Add(this.lblEntryDateColon);
            this.pnlContent.Controls.Add(this.lblEntryDate);
            this.pnlContent.Controls.Add(this.lblEntryNoColon);
            this.pnlContent.Size = new System.Drawing.Size(955, 496);
            this.tltOnControls.SetToolTip(this.pnlContent, "");
            this.pnlContent.Controls.SetChildIndex(this.lblEntryNoColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryDate, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryDateColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblBillNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtBillNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblBillNoColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblBillDate, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblBillDateColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblParty, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblPartyColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblNarration, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtNarration, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblNarrationColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.Panel1, 0);
            this.pnlContent.Controls.SetChildIndex(this.Panel3, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblTransport, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblTransportColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.pnlAddLess, 0);
            this.pnlContent.Controls.SetChildIndex(this.pnlDetail, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtEntryNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboParty, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboTransport, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblRefNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblRefNoColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblLrDateColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblChallanDt, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblLrDate, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblChallanDtColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtLrNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblLrNoColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblLrNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblSalesAcColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblSalesAc, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboSalesAc, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnSelect, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblReturnTypeColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboReturnType, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblReturnType, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblBrokerColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblBroker, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboBroker, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblDepartmentColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblDepartment, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboDepartment, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtRefNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.dtEntryDate, 0);
            this.pnlContent.Controls.SetChildIndex(this.dtChallanDate, 0);
            this.pnlContent.Controls.SetChildIndex(this.dtBillDate, 0);
            this.pnlContent.Controls.SetChildIndex(this.dtLrDate, 0);
            // 
            // lblHelpText
            // 
            this.lblHelpText.Location = new System.Drawing.Point(-9, 3);
            this.tltOnControls.SetToolTip(this.lblHelpText, "");
            // 
            // lblFormName
            // 
            this.tltOnControls.SetToolTip(this.lblFormName, "");
            // 
            // lblUUser
            // 
            this.lblUUser.Location = new System.Drawing.Point(852, 3);
            this.tltOnControls.SetToolTip(this.lblUUser, "");
            // 
            // lblCUser
            // 
            this.lblCUser.Location = new System.Drawing.Point(422, 3);
            this.tltOnControls.SetToolTip(this.lblCUser, "");
            // 
            // lblEntryNo
            // 
            this.lblEntryNo.AutoSize = true;
            this.lblEntryNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryNo.Location = new System.Drawing.Point(61, 11);
            this.lblEntryNo.Name = "lblEntryNo";
            this.lblEntryNo.Size = new System.Drawing.Size(64, 14);
            this.lblEntryNo.TabIndex = 90047;
            this.lblEntryNo.Text = "Entry No";
            this.tltOnControls.SetToolTip(this.lblEntryNo, "");
            // 
            // cboSalesAc
            // 
            this.cboSalesAc.AutoComplete = false;
            this.cboSalesAc.AutoDropdown = false;
            this.cboSalesAc.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboSalesAc.BackColorEven = System.Drawing.Color.White;
            this.cboSalesAc.BackColorOdd = System.Drawing.Color.White;
            this.cboSalesAc.ColumnNames = "";
            this.cboSalesAc.ColumnWidthDefault = 175;
            this.cboSalesAc.ColumnWidths = "";
            this.cboSalesAc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboSalesAc.Fill_ComboID = 0;
            this.cboSalesAc.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.cboSalesAc.FormattingEnabled = true;
            this.cboSalesAc.GroupType = 0;
            this.cboSalesAc.HelpText = "Select Sales A/C";
            this.cboSalesAc.IsMandatory = true;
            this.cboSalesAc.LinkedColumnIndex = 0;
            this.cboSalesAc.LinkedTextBox = null;
            this.cboSalesAc.Location = new System.Drawing.Point(590, 110);
            this.cboSalesAc.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboSalesAc.Moveable = false;
            this.cboSalesAc.Name = "cboSalesAc";
            this.cboSalesAc.NameOfControl = "Sales A/c";
            this.cboSalesAc.OpenForm = null;
            this.cboSalesAc.ShowBallonTip = false;
            this.cboSalesAc.Size = new System.Drawing.Size(298, 22);
            this.cboSalesAc.TabIndex = 14;
            this.tltOnControls.SetToolTip(this.cboSalesAc, "");
            this.cboSalesAc.SelectedValueChanged += new System.EventHandler(this.cboSalesAc_LostFocus);
            // 
            // lblSalesAc
            // 
            this.lblSalesAc.AutoSize = true;
            this.lblSalesAc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalesAc.Location = new System.Drawing.Point(494, 113);
            this.lblSalesAc.Name = "lblSalesAc";
            this.lblSalesAc.Size = new System.Drawing.Size(71, 14);
            this.lblSalesAc.TabIndex = 90098;
            this.lblSalesAc.Text = "Sales A/c";
            this.tltOnControls.SetToolTip(this.lblSalesAc, "");
            // 
            // lblSalesAcColon
            // 
            this.lblSalesAcColon.AutoSize = true;
            this.lblSalesAcColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSalesAcColon.Location = new System.Drawing.Point(578, 113);
            this.lblSalesAcColon.Name = "lblSalesAcColon";
            this.lblSalesAcColon.Size = new System.Drawing.Size(12, 14);
            this.lblSalesAcColon.TabIndex = 90099;
            this.lblSalesAcColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblSalesAcColon, "");
            // 
            // lblLrNo
            // 
            this.lblLrNo.AutoSize = true;
            this.lblLrNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLrNo.Location = new System.Drawing.Point(494, 88);
            this.lblLrNo.Name = "lblLrNo";
            this.lblLrNo.Size = new System.Drawing.Size(50, 14);
            this.lblLrNo.TabIndex = 90092;
            this.lblLrNo.Text = "LR No.";
            this.tltOnControls.SetToolTip(this.lblLrNo, "");
            // 
            // lblLrNoColon
            // 
            this.lblLrNoColon.AutoSize = true;
            this.lblLrNoColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLrNoColon.Location = new System.Drawing.Point(578, 88);
            this.lblLrNoColon.Name = "lblLrNoColon";
            this.lblLrNoColon.Size = new System.Drawing.Size(12, 14);
            this.lblLrNoColon.TabIndex = 90095;
            this.lblLrNoColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblLrNoColon, "");
            // 
            // txtLrNo
            // 
            this.txtLrNo.AutoFillDate = false;
            this.txtLrNo.BackColor = System.Drawing.Color.White;
            this.txtLrNo.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtLrNo.CheckForSymbol = null;
            this.txtLrNo.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtLrNo.DecimalPlace = 0;
            this.txtLrNo.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.txtLrNo.HelpText = "Enter LR.NO";
            this.txtLrNo.HoldMyText = null;
            this.txtLrNo.IsMandatory = false;
            this.txtLrNo.IsSingleQuote = true;
            this.txtLrNo.IsSysmbol = false;
            this.txtLrNo.Location = new System.Drawing.Point(590, 85);
            this.txtLrNo.Mask = null;
            this.txtLrNo.MaxLength = 20;
            this.txtLrNo.Moveable = false;
            this.txtLrNo.Name = "txtLrNo";
            this.txtLrNo.NameOfControl = "Lr No";
            this.txtLrNo.Prefix = null;
            this.txtLrNo.ShowBallonTip = false;
            this.txtLrNo.ShowErrorIcon = false;
            this.txtLrNo.ShowMessage = null;
            this.txtLrNo.Size = new System.Drawing.Size(100, 21);
            this.txtLrNo.Suffix = null;
            this.txtLrNo.TabIndex = 11;
            this.tltOnControls.SetToolTip(this.txtLrNo, "");
            // 
            // lblChallanDtColon
            // 
            this.lblChallanDtColon.AutoSize = true;
            this.lblChallanDtColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChallanDtColon.Location = new System.Drawing.Point(780, 9);
            this.lblChallanDtColon.Name = "lblChallanDtColon";
            this.lblChallanDtColon.Size = new System.Drawing.Size(12, 14);
            this.lblChallanDtColon.TabIndex = 90103;
            this.lblChallanDtColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblChallanDtColon, "");
            // 
            // lblLrDate
            // 
            this.lblLrDate.AutoSize = true;
            this.lblLrDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLrDate.Location = new System.Drawing.Point(694, 89);
            this.lblLrDate.Name = "lblLrDate";
            this.lblLrDate.Size = new System.Drawing.Size(59, 14);
            this.lblLrDate.TabIndex = 90093;
            this.lblLrDate.Text = "LR Date";
            this.tltOnControls.SetToolTip(this.lblLrDate, "");
            // 
            // lblChallanDt
            // 
            this.lblChallanDt.AutoSize = true;
            this.lblChallanDt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChallanDt.Location = new System.Drawing.Point(694, 9);
            this.lblChallanDt.Name = "lblChallanDt";
            this.lblChallanDt.Size = new System.Drawing.Size(91, 14);
            this.lblChallanDt.TabIndex = 90102;
            this.lblChallanDt.Text = "Challan Date";
            this.tltOnControls.SetToolTip(this.lblChallanDt, "");
            // 
            // lblLrDateColon
            // 
            this.lblLrDateColon.AutoSize = true;
            this.lblLrDateColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLrDateColon.Location = new System.Drawing.Point(782, 88);
            this.lblLrDateColon.Name = "lblLrDateColon";
            this.lblLrDateColon.Size = new System.Drawing.Size(12, 14);
            this.lblLrDateColon.TabIndex = 90097;
            this.lblLrDateColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblLrDateColon, "");
            // 
            // lblRefNoColon
            // 
            this.lblRefNoColon.AutoSize = true;
            this.lblRefNoColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefNoColon.Location = new System.Drawing.Point(578, 9);
            this.lblRefNoColon.Name = "lblRefNoColon";
            this.lblRefNoColon.Size = new System.Drawing.Size(12, 14);
            this.lblRefNoColon.TabIndex = 90101;
            this.lblRefNoColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblRefNoColon, "");
            // 
            // lblRefNo
            // 
            this.lblRefNo.AutoSize = true;
            this.lblRefNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefNo.Location = new System.Drawing.Point(494, 9);
            this.lblRefNo.Name = "lblRefNo";
            this.lblRefNo.Size = new System.Drawing.Size(73, 14);
            this.lblRefNo.TabIndex = 90100;
            this.lblRefNo.Text = "Credit No.";
            this.tltOnControls.SetToolTip(this.lblRefNo, "");
            // 
            // cboTransport
            // 
            this.cboTransport.AutoComplete = false;
            this.cboTransport.AutoDropdown = false;
            this.cboTransport.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboTransport.BackColorEven = System.Drawing.Color.White;
            this.cboTransport.BackColorOdd = System.Drawing.Color.White;
            this.cboTransport.ColumnNames = "";
            this.cboTransport.ColumnWidthDefault = 175;
            this.cboTransport.ColumnWidths = "";
            this.cboTransport.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboTransport.Fill_ComboID = 0;
            this.cboTransport.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.cboTransport.FormattingEnabled = true;
            this.cboTransport.GroupType = 0;
            this.cboTransport.HelpText = "Select Transport";
            this.cboTransport.IsMandatory = true;
            this.cboTransport.LinkedColumnIndex = 0;
            this.cboTransport.LinkedTextBox = null;
            this.cboTransport.Location = new System.Drawing.Point(590, 58);
            this.cboTransport.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboTransport.Moveable = false;
            this.cboTransport.Name = "cboTransport";
            this.cboTransport.NameOfControl = "Transport";
            this.cboTransport.OpenForm = null;
            this.cboTransport.ShowBallonTip = false;
            this.cboTransport.Size = new System.Drawing.Size(298, 22);
            this.cboTransport.TabIndex = 9;
            this.tltOnControls.SetToolTip(this.cboTransport, "");
            // 
            // cboParty
            // 
            this.cboParty.AutoComplete = false;
            this.cboParty.AutoDropdown = false;
            this.cboParty.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboParty.BackColorEven = System.Drawing.Color.White;
            this.cboParty.BackColorOdd = System.Drawing.Color.White;
            this.cboParty.ColumnNames = "";
            this.cboParty.ColumnWidthDefault = 175;
            this.cboParty.ColumnWidths = "";
            this.cboParty.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboParty.Fill_ComboID = 0;
            this.cboParty.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.cboParty.FormattingEnabled = true;
            this.cboParty.GroupType = 0;
            this.cboParty.HelpText = "Select Party";
            this.cboParty.IsMandatory = true;
            this.cboParty.LinkedColumnIndex = 0;
            this.cboParty.LinkedTextBox = null;
            this.cboParty.Location = new System.Drawing.Point(160, 58);
            this.cboParty.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboParty.Moveable = false;
            this.cboParty.Name = "cboParty";
            this.cboParty.NameOfControl = "Party ";
            this.cboParty.OpenForm = null;
            this.cboParty.ShowBallonTip = false;
            this.cboParty.Size = new System.Drawing.Size(296, 22);
            this.cboParty.TabIndex = 8;
            this.tltOnControls.SetToolTip(this.cboParty, "");
            this.cboParty.SelectedValueChanged += new System.EventHandler(this.cboParty_SelectedValueChanged);
            // 
            // txtEntryNo
            // 
            this.txtEntryNo.AutoFillDate = false;
            this.txtEntryNo.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtEntryNo.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtEntryNo.CheckForSymbol = null;
            this.txtEntryNo.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.NumericWithOutDecimal;
            this.txtEntryNo.DecimalPlace = 0;
            this.txtEntryNo.Enabled = false;
            this.txtEntryNo.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.txtEntryNo.HelpText = "Entry No";
            this.txtEntryNo.HoldMyText = null;
            this.txtEntryNo.IsMandatory = true;
            this.txtEntryNo.IsSingleQuote = true;
            this.txtEntryNo.IsSysmbol = false;
            this.txtEntryNo.Location = new System.Drawing.Point(160, 7);
            this.txtEntryNo.Mask = null;
            this.txtEntryNo.MaxLength = 20;
            this.txtEntryNo.Moveable = false;
            this.txtEntryNo.Name = "txtEntryNo";
            this.txtEntryNo.NameOfControl = "Entry N0";
            this.txtEntryNo.Prefix = null;
            this.txtEntryNo.ShowBallonTip = false;
            this.txtEntryNo.ShowErrorIcon = false;
            this.txtEntryNo.ShowMessage = null;
            this.txtEntryNo.Size = new System.Drawing.Size(100, 21);
            this.txtEntryNo.Suffix = null;
            this.txtEntryNo.TabIndex = 1;
            this.txtEntryNo.Text = "0";
            this.txtEntryNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tltOnControls.SetToolTip(this.txtEntryNo, "");
            // 
            // pnlDetail
            // 
            this.pnlDetail.BackColor = System.Drawing.Color.LightBlue;
            this.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDetail.Controls.Add(this.GrdMain);
            this.pnlDetail.Location = new System.Drawing.Point(68, 169);
            this.pnlDetail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(479, 267);
            this.pnlDetail.TabIndex = 16;
            this.tltOnControls.SetToolTip(this.pnlDetail, "");
            // 
            // pnlAddLess
            // 
            this.pnlAddLess.BackColor = System.Drawing.Color.LightBlue;
            this.pnlAddLess.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAddLess.Controls.Add(this.GrdDtls);
            this.pnlAddLess.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAddLess.Location = new System.Drawing.Point(551, 169);
            this.pnlAddLess.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlAddLess.Name = "pnlAddLess";
            this.pnlAddLess.Size = new System.Drawing.Size(338, 268);
            this.pnlAddLess.TabIndex = 17;
            this.tltOnControls.SetToolTip(this.pnlAddLess, "");
            // 
            // lblTransportColon
            // 
            this.lblTransportColon.AutoSize = true;
            this.lblTransportColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransportColon.Location = new System.Drawing.Point(578, 61);
            this.lblTransportColon.Name = "lblTransportColon";
            this.lblTransportColon.Size = new System.Drawing.Size(12, 14);
            this.lblTransportColon.TabIndex = 90096;
            this.lblTransportColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblTransportColon, "");
            // 
            // lblTransport
            // 
            this.lblTransport.AutoSize = true;
            this.lblTransport.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransport.Location = new System.Drawing.Point(494, 61);
            this.lblTransport.Name = "lblTransport";
            this.lblTransport.Size = new System.Drawing.Size(71, 14);
            this.lblTransport.TabIndex = 90094;
            this.lblTransport.Text = "Transport";
            this.tltOnControls.SetToolTip(this.lblTransport, "");
            // 
            // Panel3
            // 
            this.Panel3.BackColor = System.Drawing.Color.PowderBlue;
            this.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel3.Controls.Add(this.txtNetAmt);
            this.Panel3.Controls.Add(this.txtAddLessAmt);
            this.Panel3.Controls.Add(this.lblNetAmt);
            this.Panel3.Controls.Add(this.lblAddLess);
            this.Panel3.Location = new System.Drawing.Point(551, 439);
            this.Panel3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel3.Name = "Panel3";
            this.Panel3.Size = new System.Drawing.Size(338, 28);
            this.Panel3.TabIndex = 90066;
            this.tltOnControls.SetToolTip(this.Panel3, "");
            // 
            // txtNetAmt
            // 
            this.txtNetAmt.AutoSize = true;
            this.txtNetAmt.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNetAmt.ForeColor = System.Drawing.Color.Brown;
            this.txtNetAmt.Location = new System.Drawing.Point(230, 7);
            this.txtNetAmt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtNetAmt.Name = "txtNetAmt";
            this.txtNetAmt.Size = new System.Drawing.Size(35, 13);
            this.txtNetAmt.TabIndex = 31;
            this.txtNetAmt.Text = "0.00";
            this.tltOnControls.SetToolTip(this.txtNetAmt, "");
            // 
            // txtAddLessAmt
            // 
            this.txtAddLessAmt.AutoSize = true;
            this.txtAddLessAmt.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddLessAmt.ForeColor = System.Drawing.Color.Brown;
            this.txtAddLessAmt.Location = new System.Drawing.Point(76, 7);
            this.txtAddLessAmt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.txtAddLessAmt.Name = "txtAddLessAmt";
            this.txtAddLessAmt.Size = new System.Drawing.Size(35, 13);
            this.txtAddLessAmt.TabIndex = 29;
            this.txtAddLessAmt.Text = "0.00";
            this.tltOnControls.SetToolTip(this.txtAddLessAmt, "");
            // 
            // lblNetAmt
            // 
            this.lblNetAmt.AutoSize = true;
            this.lblNetAmt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNetAmt.Location = new System.Drawing.Point(143, 6);
            this.lblNetAmt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNetAmt.Name = "lblNetAmt";
            this.lblNetAmt.Size = new System.Drawing.Size(93, 14);
            this.lblNetAmt.TabIndex = 30;
            this.lblNetAmt.Text = "Net Amount :";
            this.tltOnControls.SetToolTip(this.lblNetAmt, "");
            // 
            // lblAddLess
            // 
            this.lblAddLess.AutoSize = true;
            this.lblAddLess.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddLess.Location = new System.Drawing.Point(3, 6);
            this.lblAddLess.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblAddLess.Name = "lblAddLess";
            this.lblAddLess.Size = new System.Drawing.Size(79, 14);
            this.lblAddLess.TabIndex = 28;
            this.lblAddLess.Text = "Add/Less :";
            this.tltOnControls.SetToolTip(this.lblAddLess, "");
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.PowderBlue;
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel1.Controls.Add(this.TxtGrossAmount);
            this.Panel1.Controls.Add(this.TxtTotalPcs);
            this.Panel1.Controls.Add(this.LblTotalAmount);
            this.Panel1.Controls.Add(this.LblTotalCuts);
            this.Panel1.Location = new System.Drawing.Point(68, 439);
            this.Panel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(479, 28);
            this.Panel1.TabIndex = 90089;
            this.tltOnControls.SetToolTip(this.Panel1, "");
            // 
            // TxtGrossAmount
            // 
            this.TxtGrossAmount.AutoSize = true;
            this.TxtGrossAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtGrossAmount.ForeColor = System.Drawing.Color.Brown;
            this.TxtGrossAmount.Location = new System.Drawing.Point(382, 8);
            this.TxtGrossAmount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TxtGrossAmount.Name = "TxtGrossAmount";
            this.TxtGrossAmount.Size = new System.Drawing.Size(35, 13);
            this.TxtGrossAmount.TabIndex = 31;
            this.TxtGrossAmount.Text = "0.00";
            this.tltOnControls.SetToolTip(this.TxtGrossAmount, "");
            // 
            // TxtTotalPcs
            // 
            this.TxtTotalPcs.AutoSize = true;
            this.TxtTotalPcs.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTotalPcs.ForeColor = System.Drawing.Color.Brown;
            this.TxtTotalPcs.Location = new System.Drawing.Point(80, 6);
            this.TxtTotalPcs.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TxtTotalPcs.Name = "TxtTotalPcs";
            this.TxtTotalPcs.Size = new System.Drawing.Size(15, 13);
            this.TxtTotalPcs.TabIndex = 27;
            this.TxtTotalPcs.Text = "0";
            this.TxtTotalPcs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tltOnControls.SetToolTip(this.TxtTotalPcs, "");
            // 
            // LblTotalAmount
            // 
            this.LblTotalAmount.AutoSize = true;
            this.LblTotalAmount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalAmount.Location = new System.Drawing.Point(277, 6);
            this.LblTotalAmount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTotalAmount.Name = "LblTotalAmount";
            this.LblTotalAmount.Size = new System.Drawing.Size(108, 14);
            this.LblTotalAmount.TabIndex = 30;
            this.LblTotalAmount.Text = "Gross Amount :";
            this.tltOnControls.SetToolTip(this.LblTotalAmount, "");
            // 
            // LblTotalCuts
            // 
            this.LblTotalCuts.AutoSize = true;
            this.LblTotalCuts.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotalCuts.Location = new System.Drawing.Point(3, 6);
            this.LblTotalCuts.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTotalCuts.Name = "LblTotalCuts";
            this.LblTotalCuts.Size = new System.Drawing.Size(80, 14);
            this.LblTotalCuts.TabIndex = 26;
            this.LblTotalCuts.Text = "Total Pcs. :";
            this.tltOnControls.SetToolTip(this.LblTotalCuts, "");
            // 
            // lblNarrationColon
            // 
            this.lblNarrationColon.AutoSize = true;
            this.lblNarrationColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNarrationColon.Location = new System.Drawing.Point(148, 473);
            this.lblNarrationColon.Name = "lblNarrationColon";
            this.lblNarrationColon.Size = new System.Drawing.Size(12, 14);
            this.lblNarrationColon.TabIndex = 90088;
            this.lblNarrationColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblNarrationColon, "");
            // 
            // txtNarration
            // 
            this.txtNarration.AutoFillDate = false;
            this.txtNarration.BackColor = System.Drawing.Color.White;
            this.txtNarration.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtNarration.CheckForSymbol = null;
            this.txtNarration.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtNarration.DecimalPlace = 0;
            this.txtNarration.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.txtNarration.HelpText = "Enter Narration";
            this.txtNarration.HoldMyText = null;
            this.txtNarration.IsMandatory = false;
            this.txtNarration.IsSingleQuote = true;
            this.txtNarration.IsSysmbol = false;
            this.txtNarration.Location = new System.Drawing.Point(160, 471);
            this.txtNarration.Mask = null;
            this.txtNarration.Moveable = false;
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.NameOfControl = "Narration";
            this.txtNarration.Prefix = null;
            this.txtNarration.ShowBallonTip = false;
            this.txtNarration.ShowErrorIcon = false;
            this.txtNarration.ShowMessage = null;
            this.txtNarration.Size = new System.Drawing.Size(729, 21);
            this.txtNarration.Suffix = null;
            this.txtNarration.TabIndex = 18;
            this.tltOnControls.SetToolTip(this.txtNarration, "");
            // 
            // lblNarration
            // 
            this.lblNarration.AutoSize = true;
            this.lblNarration.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNarration.Location = new System.Drawing.Point(70, 473);
            this.lblNarration.Name = "lblNarration";
            this.lblNarration.Size = new System.Drawing.Size(70, 14);
            this.lblNarration.TabIndex = 90087;
            this.lblNarration.Text = "Narration";
            this.tltOnControls.SetToolTip(this.lblNarration, "");
            // 
            // txtCode
            // 
            this.txtCode.AutoFillDate = false;
            this.txtCode.BackColor = System.Drawing.Color.White;
            this.txtCode.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtCode.CheckForSymbol = null;
            this.txtCode.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtCode.DecimalPlace = 0;
            this.txtCode.Enabled = false;
            this.txtCode.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.txtCode.Size = new System.Drawing.Size(31, 20);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 0;
            this.tltOnControls.SetToolTip(this.txtCode, "");
            this.txtCode.Visible = false;
            // 
            // lblPartyColon
            // 
            this.lblPartyColon.AutoSize = true;
            this.lblPartyColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPartyColon.Location = new System.Drawing.Point(148, 60);
            this.lblPartyColon.Name = "lblPartyColon";
            this.lblPartyColon.Size = new System.Drawing.Size(12, 14);
            this.lblPartyColon.TabIndex = 90084;
            this.lblPartyColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblPartyColon, "");
            // 
            // lblParty
            // 
            this.lblParty.AutoSize = true;
            this.lblParty.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParty.Location = new System.Drawing.Point(61, 60);
            this.lblParty.Name = "lblParty";
            this.lblParty.Size = new System.Drawing.Size(43, 14);
            this.lblParty.TabIndex = 90064;
            this.lblParty.Text = "Party";
            this.tltOnControls.SetToolTip(this.lblParty, "");
            // 
            // lblBillDateColon
            // 
            this.lblBillDateColon.AutoSize = true;
            this.lblBillDateColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillDateColon.Location = new System.Drawing.Point(780, 36);
            this.lblBillDateColon.Name = "lblBillDateColon";
            this.lblBillDateColon.Size = new System.Drawing.Size(12, 14);
            this.lblBillDateColon.TabIndex = 90083;
            this.lblBillDateColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblBillDateColon, "");
            // 
            // lblBillDate
            // 
            this.lblBillDate.AutoSize = true;
            this.lblBillDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillDate.Location = new System.Drawing.Point(696, 36);
            this.lblBillDate.Name = "lblBillDate";
            this.lblBillDate.Size = new System.Drawing.Size(63, 14);
            this.lblBillDate.TabIndex = 90055;
            this.lblBillDate.Text = "Bill Date";
            this.tltOnControls.SetToolTip(this.lblBillDate, "");
            // 
            // lblBillNoColon
            // 
            this.lblBillNoColon.AutoSize = true;
            this.lblBillNoColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillNoColon.Location = new System.Drawing.Point(578, 36);
            this.lblBillNoColon.Name = "lblBillNoColon";
            this.lblBillNoColon.Size = new System.Drawing.Size(12, 14);
            this.lblBillNoColon.TabIndex = 90082;
            this.lblBillNoColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblBillNoColon, "");
            // 
            // txtBillNo
            // 
            this.txtBillNo.AutoFillDate = false;
            this.txtBillNo.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtBillNo.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtBillNo.CheckForSymbol = null;
            this.txtBillNo.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtBillNo.DecimalPlace = 0;
            this.txtBillNo.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.txtBillNo.HelpText = "Enter Bill No";
            this.txtBillNo.HoldMyText = null;
            this.txtBillNo.IsMandatory = true;
            this.txtBillNo.IsSingleQuote = true;
            this.txtBillNo.IsSysmbol = false;
            this.txtBillNo.Location = new System.Drawing.Point(590, 32);
            this.txtBillNo.Mask = null;
            this.txtBillNo.MaxLength = 20;
            this.txtBillNo.Moveable = false;
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.NameOfControl = "Bill No";
            this.txtBillNo.Prefix = null;
            this.txtBillNo.ShowBallonTip = false;
            this.txtBillNo.ShowErrorIcon = false;
            this.txtBillNo.ShowMessage = null;
            this.txtBillNo.Size = new System.Drawing.Size(100, 21);
            this.txtBillNo.Suffix = null;
            this.txtBillNo.TabIndex = 6;
            this.tltOnControls.SetToolTip(this.txtBillNo, "");
            // 
            // lblBillNo
            // 
            this.lblBillNo.AutoSize = true;
            this.lblBillNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBillNo.Location = new System.Drawing.Point(494, 36);
            this.lblBillNo.Name = "lblBillNo";
            this.lblBillNo.Size = new System.Drawing.Size(54, 14);
            this.lblBillNo.TabIndex = 90052;
            this.lblBillNo.Text = "Bill No.";
            this.tltOnControls.SetToolTip(this.lblBillNo, "");
            // 
            // lblEntryDateColon
            // 
            this.lblEntryDateColon.AutoSize = true;
            this.lblEntryDateColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryDateColon.Location = new System.Drawing.Point(350, 11);
            this.lblEntryDateColon.Name = "lblEntryDateColon";
            this.lblEntryDateColon.Size = new System.Drawing.Size(12, 14);
            this.lblEntryDateColon.TabIndex = 90080;
            this.lblEntryDateColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblEntryDateColon, "");
            // 
            // lblEntryDate
            // 
            this.lblEntryDate.AutoSize = true;
            this.lblEntryDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryDate.Location = new System.Drawing.Point(265, 11);
            this.lblEntryDate.Name = "lblEntryDate";
            this.lblEntryDate.Size = new System.Drawing.Size(77, 14);
            this.lblEntryDate.TabIndex = 90049;
            this.lblEntryDate.Text = "Entry Date";
            this.tltOnControls.SetToolTip(this.lblEntryDate, "");
            // 
            // lblEntryNoColon
            // 
            this.lblEntryNoColon.AutoSize = true;
            this.lblEntryNoColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryNoColon.Location = new System.Drawing.Point(148, 11);
            this.lblEntryNoColon.Name = "lblEntryNoColon";
            this.lblEntryNoColon.Size = new System.Drawing.Size(12, 14);
            this.lblEntryNoColon.TabIndex = 90078;
            this.lblEntryNoColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblEntryNoColon, "");
            // 
            // tltOnControls
            // 
            this.tltOnControls.DataProvider = stringDataProvider5;
            this.tltOnControls.LoadText = "";
            this.tltOnControls.ShowToolTip = false;
            // 
            // btnSelect
            // 
            this.btnSelect.BackColor = System.Drawing.Color.CadetBlue;
            this.btnSelect.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(754, 138);
            this.btnSelect.Moveable = false;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(135, 25);
            this.btnSelect.TabIndex = 15;
            this.btnSelect.Text = "Select &Challan";
            this.tltOnControls.SetToolTip(this.btnSelect, "");
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // lblReturnType
            // 
            this.lblReturnType.AutoSize = true;
            this.lblReturnType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblReturnType.Location = new System.Drawing.Point(61, 37);
            this.lblReturnType.Name = "lblReturnType";
            this.lblReturnType.Size = new System.Drawing.Size(87, 14);
            this.lblReturnType.TabIndex = 90118;
            this.lblReturnType.Text = "Return Type";
            this.tltOnControls.SetToolTip(this.lblReturnType, "");
            // 
            // cboReturnType
            // 
            this.cboReturnType.AutoComplete = false;
            this.cboReturnType.AutoDropdown = false;
            this.cboReturnType.BackColor = System.Drawing.Color.White;
            this.cboReturnType.BackColorEven = System.Drawing.Color.White;
            this.cboReturnType.BackColorOdd = System.Drawing.Color.White;
            this.cboReturnType.ColumnNames = "";
            this.cboReturnType.ColumnWidthDefault = 175;
            this.cboReturnType.ColumnWidths = "";
            this.cboReturnType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboReturnType.Fill_ComboID = 0;
            this.cboReturnType.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.cboReturnType.FormattingEnabled = true;
            this.cboReturnType.GroupType = 0;
            this.cboReturnType.HelpText = "Select Return Type";
            this.cboReturnType.IsMandatory = false;
            this.cboReturnType.LinkedColumnIndex = 0;
            this.cboReturnType.LinkedTextBox = null;
            this.cboReturnType.Location = new System.Drawing.Point(160, 32);
            this.cboReturnType.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboReturnType.Moveable = false;
            this.cboReturnType.Name = "cboReturnType";
            this.cboReturnType.NameOfControl = "Return Type";
            this.cboReturnType.OpenForm = null;
            this.cboReturnType.ShowBallonTip = false;
            this.cboReturnType.Size = new System.Drawing.Size(296, 22);
            this.cboReturnType.TabIndex = 5;
            this.tltOnControls.SetToolTip(this.cboReturnType, "");
            this.cboReturnType.SelectedValueChanged += new System.EventHandler(this.cboReturnType_SelectedValueChanged);
            // 
            // lblReturnTypeColon
            // 
            this.lblReturnTypeColon.AutoSize = true;
            this.lblReturnTypeColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblReturnTypeColon.Location = new System.Drawing.Point(148, 36);
            this.lblReturnTypeColon.Name = "lblReturnTypeColon";
            this.lblReturnTypeColon.Size = new System.Drawing.Size(12, 14);
            this.lblReturnTypeColon.TabIndex = 90119;
            this.lblReturnTypeColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblReturnTypeColon, "");
            // 
            // cboBroker
            // 
            this.cboBroker.AutoComplete = false;
            this.cboBroker.AutoDropdown = false;
            this.cboBroker.BackColor = System.Drawing.Color.White;
            this.cboBroker.BackColorEven = System.Drawing.Color.White;
            this.cboBroker.BackColorOdd = System.Drawing.Color.White;
            this.cboBroker.ColumnNames = "";
            this.cboBroker.ColumnWidthDefault = 175;
            this.cboBroker.ColumnWidths = "";
            this.cboBroker.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboBroker.Fill_ComboID = 0;
            this.cboBroker.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.cboBroker.FormattingEnabled = true;
            this.cboBroker.GroupType = 0;
            this.cboBroker.HelpText = "Select Broker";
            this.cboBroker.IsMandatory = false;
            this.cboBroker.LinkedColumnIndex = 0;
            this.cboBroker.LinkedTextBox = null;
            this.cboBroker.Location = new System.Drawing.Point(160, 84);
            this.cboBroker.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboBroker.Moveable = false;
            this.cboBroker.Name = "cboBroker";
            this.cboBroker.NameOfControl = "Broker";
            this.cboBroker.OpenForm = null;
            this.cboBroker.ShowBallonTip = false;
            this.cboBroker.Size = new System.Drawing.Size(296, 22);
            this.cboBroker.TabIndex = 10;
            this.tltOnControls.SetToolTip(this.cboBroker, "");
            // 
            // lblBroker
            // 
            this.lblBroker.AutoSize = true;
            this.lblBroker.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblBroker.Location = new System.Drawing.Point(61, 89);
            this.lblBroker.Name = "lblBroker";
            this.lblBroker.Size = new System.Drawing.Size(52, 14);
            this.lblBroker.TabIndex = 90121;
            this.lblBroker.Text = "Broker";
            this.tltOnControls.SetToolTip(this.lblBroker, "");
            // 
            // lblBrokerColon
            // 
            this.lblBrokerColon.AutoSize = true;
            this.lblBrokerColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblBrokerColon.Location = new System.Drawing.Point(148, 87);
            this.lblBrokerColon.Name = "lblBrokerColon";
            this.lblBrokerColon.Size = new System.Drawing.Size(12, 14);
            this.lblBrokerColon.TabIndex = 90122;
            this.lblBrokerColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblBrokerColon, "");
            // 
            // cboDepartment
            // 
            this.cboDepartment.AutoComplete = false;
            this.cboDepartment.AutoDropdown = false;
            this.cboDepartment.BackColor = System.Drawing.Color.White;
            this.cboDepartment.BackColorEven = System.Drawing.Color.Lavender;
            this.cboDepartment.BackColorOdd = System.Drawing.Color.White;
            this.cboDepartment.ColumnNames = "";
            this.cboDepartment.ColumnWidthDefault = 175;
            this.cboDepartment.ColumnWidths = "";
            this.cboDepartment.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboDepartment.Fill_ComboID = 0;
            this.cboDepartment.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.cboDepartment.FormattingEnabled = true;
            this.cboDepartment.GroupType = 0;
            this.cboDepartment.HelpText = "Select Department";
            this.cboDepartment.IsMandatory = false;
            this.cboDepartment.LinkedColumnIndex = 0;
            this.cboDepartment.LinkedTextBox = null;
            this.cboDepartment.Location = new System.Drawing.Point(160, 110);
            this.cboDepartment.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboDepartment.Moveable = false;
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.NameOfControl = "Department";
            this.cboDepartment.OpenForm = null;
            this.cboDepartment.ShowBallonTip = false;
            this.cboDepartment.Size = new System.Drawing.Size(296, 22);
            this.cboDepartment.TabIndex = 13;
            this.tltOnControls.SetToolTip(this.cboDepartment, "");
            // 
            // lblDepartment
            // 
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblDepartment.Location = new System.Drawing.Point(61, 114);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(85, 14);
            this.lblDepartment.TabIndex = 90124;
            this.lblDepartment.Text = "Department";
            this.tltOnControls.SetToolTip(this.lblDepartment, "");
            // 
            // lblDepartmentColon
            // 
            this.lblDepartmentColon.AutoSize = true;
            this.lblDepartmentColon.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartmentColon.Location = new System.Drawing.Point(148, 113);
            this.lblDepartmentColon.Name = "lblDepartmentColon";
            this.lblDepartmentColon.Size = new System.Drawing.Size(12, 16);
            this.lblDepartmentColon.TabIndex = 90125;
            this.lblDepartmentColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblDepartmentColon, "");
            // 
            // txtRefNo
            // 
            this.txtRefNo.AutoFillDate = false;
            this.txtRefNo.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtRefNo.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtRefNo.CheckForSymbol = null;
            this.txtRefNo.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtRefNo.DecimalPlace = 0;
            this.txtRefNo.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.txtRefNo.HelpText = "Enter Challan No";
            this.txtRefNo.HoldMyText = null;
            this.txtRefNo.IsMandatory = true;
            this.txtRefNo.IsSingleQuote = true;
            this.txtRefNo.IsSysmbol = false;
            this.txtRefNo.Location = new System.Drawing.Point(590, 7);
            this.txtRefNo.Mask = null;
            this.txtRefNo.MaxLength = 20;
            this.txtRefNo.Moveable = false;
            this.txtRefNo.Name = "txtRefNo";
            this.txtRefNo.NameOfControl = "Challan No";
            this.txtRefNo.Prefix = null;
            this.txtRefNo.ShowBallonTip = false;
            this.txtRefNo.ShowErrorIcon = false;
            this.txtRefNo.ShowMessage = null;
            this.txtRefNo.Size = new System.Drawing.Size(100, 21);
            this.txtRefNo.Suffix = null;
            this.txtRefNo.TabIndex = 3;
            this.tltOnControls.SetToolTip(this.txtRefNo, "");
            // 
            // dtEntryDate
            // 
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
            this.dtEntryDate.Location = new System.Drawing.Point(362, 6);
            this.dtEntryDate.Mask = "##/##/####";
            this.dtEntryDate.MaxLength = 10;
            this.dtEntryDate.Moveable = false;
            this.dtEntryDate.Name = "dtEntryDate";
            this.dtEntryDate.NameOfControl = "Entry  Date";
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
            // dtChallanDate
            // 
            this.dtChallanDate.AutoFillDate = true;
            this.dtChallanDate.BackColor = System.Drawing.Color.White;
            this.dtChallanDate.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.dtChallanDate.CheckForSymbol = null;
            this.dtChallanDate.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.ApplyDate;
            this.dtChallanDate.DecimalPlace = 0;
            this.dtChallanDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.dtChallanDate.HelpText = "Enter Entry Date";
            this.dtChallanDate.HoldMyText = null;
            this.dtChallanDate.IsMandatory = false;
            this.dtChallanDate.IsSingleQuote = true;
            this.dtChallanDate.IsSysmbol = false;
            this.dtChallanDate.Location = new System.Drawing.Point(794, 7);
            this.dtChallanDate.Mask = "##/##/####";
            this.dtChallanDate.MaxLength = 10;
            this.dtChallanDate.Moveable = false;
            this.dtChallanDate.Name = "dtChallanDate";
            this.dtChallanDate.NameOfControl = "Challan Date";
            this.dtChallanDate.Prefix = null;
            this.dtChallanDate.ShowBallonTip = false;
            this.dtChallanDate.ShowErrorIcon = false;
            this.dtChallanDate.ShowMessage = null;
            this.dtChallanDate.Size = new System.Drawing.Size(94, 22);
            this.dtChallanDate.Suffix = null;
            this.dtChallanDate.TabIndex = 4;
            this.dtChallanDate.Text = "__/__/____";
            this.tltOnControls.SetToolTip(this.dtChallanDate, "");
            // 
            // dtBillDate
            // 
            this.dtBillDate.AutoFillDate = true;
            this.dtBillDate.BackColor = System.Drawing.Color.White;
            this.dtBillDate.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.dtBillDate.CheckForSymbol = null;
            this.dtBillDate.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.ApplyDate;
            this.dtBillDate.DecimalPlace = 0;
            this.dtBillDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.dtBillDate.HelpText = "Enter Entry Date";
            this.dtBillDate.HoldMyText = null;
            this.dtBillDate.IsMandatory = false;
            this.dtBillDate.IsSingleQuote = true;
            this.dtBillDate.IsSysmbol = false;
            this.dtBillDate.Location = new System.Drawing.Point(794, 32);
            this.dtBillDate.Mask = "##/##/####";
            this.dtBillDate.MaxLength = 10;
            this.dtBillDate.Moveable = false;
            this.dtBillDate.Name = "dtBillDate";
            this.dtBillDate.NameOfControl = "Bill Date";
            this.dtBillDate.Prefix = null;
            this.dtBillDate.ShowBallonTip = false;
            this.dtBillDate.ShowErrorIcon = false;
            this.dtBillDate.ShowMessage = null;
            this.dtBillDate.Size = new System.Drawing.Size(94, 22);
            this.dtBillDate.Suffix = null;
            this.dtBillDate.TabIndex = 7;
            this.dtBillDate.Text = "__/__/____";
            this.tltOnControls.SetToolTip(this.dtBillDate, "");
            // 
            // dtLrDate
            // 
            this.dtLrDate.AutoFillDate = true;
            this.dtLrDate.BackColor = System.Drawing.Color.White;
            this.dtLrDate.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.dtLrDate.CheckForSymbol = null;
            this.dtLrDate.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.ApplyDate;
            this.dtLrDate.DecimalPlace = 0;
            this.dtLrDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.dtLrDate.HelpText = "Enter Entry Date";
            this.dtLrDate.HoldMyText = null;
            this.dtLrDate.IsMandatory = false;
            this.dtLrDate.IsSingleQuote = true;
            this.dtLrDate.IsSysmbol = false;
            this.dtLrDate.Location = new System.Drawing.Point(794, 84);
            this.dtLrDate.Mask = "##/##/####";
            this.dtLrDate.MaxLength = 10;
            this.dtLrDate.Moveable = false;
            this.dtLrDate.Name = "dtLrDate";
            this.dtLrDate.NameOfControl = "Lr date";
            this.dtLrDate.Prefix = null;
            this.dtLrDate.ShowBallonTip = false;
            this.dtLrDate.ShowErrorIcon = false;
            this.dtLrDate.ShowMessage = null;
            this.dtLrDate.Size = new System.Drawing.Size(94, 22);
            this.dtLrDate.Suffix = null;
            this.dtLrDate.TabIndex = 12;
            this.dtLrDate.Text = "__/__/____";
            this.tltOnControls.SetToolTip(this.dtLrDate, "");
            // 
            // GrdMain
            // 
            this.GrdMain.blnFormAction = 0;
            this.GrdMain.CompID = 0;
            this.GrdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrdMain.Location = new System.Drawing.Point(0, 0);
            this.GrdMain.Margin = new System.Windows.Forms.Padding(49, 3, 49, 3);
            this.GrdMain.Name = "GrdMain";
            this.GrdMain.Size = new System.Drawing.Size(477, 265);
            this.GrdMain.TabIndex = 18;
            this.tltOnControls.SetToolTip(this.GrdMain, "");
            this.GrdMain.YearID = 0;
            // 
            // GrdDtls
            // 
            this.GrdDtls.blnFormAction = 0;
            this.GrdDtls.CompID = 0;
            this.GrdDtls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrdDtls.Location = new System.Drawing.Point(0, 0);
            this.GrdDtls.Margin = new System.Windows.Forms.Padding(87, 3, 87, 3);
            this.GrdDtls.Name = "GrdDtls";
            this.GrdDtls.Size = new System.Drawing.Size(336, 266);
            this.GrdDtls.TabIndex = 19;
            this.tltOnControls.SetToolTip(this.GrdDtls, "");
            this.GrdDtls.YearID = 0;
            // 
            // frmCatalogSalesReturn
            // 
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.ClientSize = new System.Drawing.Size(955, 547);
            this.DoubleBuffered = false;
            this.Name = "frmCatalogSalesReturn";
            this.Load += new System.EventHandler(this.frmFabricInvoice_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlDetail.ResumeLayout(false);
            this.pnlAddLess.ResumeLayout(false);
            this.Panel3.ResumeLayout(false);
            this.Panel3.PerformLayout();
            this.Panel1.ResumeLayout(false);
            this.Panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label lblEntryNo;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboSalesAc;
        internal System.Windows.Forms.Label lblSalesAc;
        internal System.Windows.Forms.Label lblSalesAcColon;
        internal System.Windows.Forms.Label lblLrNo;
        internal System.Windows.Forms.Label lblLrNoColon;
        internal CIS_CLibrary.CIS_Textbox txtLrNo;
        internal System.Windows.Forms.Label lblChallanDtColon;
        internal System.Windows.Forms.Label lblLrDate;
        internal System.Windows.Forms.Label lblChallanDt;
        internal System.Windows.Forms.Label lblLrDateColon;
        internal System.Windows.Forms.Label lblRefNoColon;
        internal System.Windows.Forms.Label lblRefNo;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboTransport;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboParty;
        internal CIS_CLibrary.CIS_Textbox txtEntryNo;
        internal System.Windows.Forms.Panel pnlDetail;
        internal System.Windows.Forms.Panel pnlAddLess;
        internal System.Windows.Forms.Label lblTransportColon;
        internal System.Windows.Forms.Label lblTransport;
        internal System.Windows.Forms.Panel Panel3;
        internal System.Windows.Forms.Label txtNetAmt;
        internal System.Windows.Forms.Label txtAddLessAmt;
        internal System.Windows.Forms.Label lblNetAmt;
        internal System.Windows.Forms.Label lblAddLess;
        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Label TxtGrossAmount;
        internal System.Windows.Forms.Label TxtTotalPcs;
        internal System.Windows.Forms.Label LblTotalAmount;
        internal System.Windows.Forms.Label LblTotalCuts;
        internal System.Windows.Forms.Label lblNarrationColon;
        internal CIS_CLibrary.CIS_Textbox txtNarration;
        internal System.Windows.Forms.Label lblNarration;
        public CIS_CLibrary.CIS_Textbox txtCode;
        internal System.Windows.Forms.Label lblPartyColon;
        internal System.Windows.Forms.Label lblParty;
        internal System.Windows.Forms.Label lblBillDateColon;
        internal System.Windows.Forms.Label lblBillDate;
        internal System.Windows.Forms.Label lblBillNoColon;
        internal CIS_CLibrary.CIS_Textbox txtBillNo;
        internal System.Windows.Forms.Label lblBillNo;
        internal System.Windows.Forms.Label lblEntryDateColon;
        internal System.Windows.Forms.Label lblEntryDate;
        internal System.Windows.Forms.Label lblEntryNoColon;
        private CIS_CLibrary.ToolTip.CIS_ToolTip tltOnControls;
        internal CIS_CLibrary.CIS_Button btnSelect;
        internal System.Windows.Forms.Label lblReturnType;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboReturnType;
        internal System.Windows.Forms.Label lblReturnTypeColon;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboBroker;
        internal System.Windows.Forms.Label lblBroker;
        internal System.Windows.Forms.Label lblBrokerColon;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboDepartment;
        internal System.Windows.Forms.Label lblDepartment;
        internal System.Windows.Forms.Label lblDepartmentColon;
        internal CIS_CLibrary.CIS_Textbox txtRefNo;
        internal CIS_CLibrary.CIS_Textbox dtEntryDate;
        internal CIS_CLibrary.CIS_Textbox dtChallanDate;
        internal CIS_CLibrary.CIS_Textbox dtBillDate;
        internal CIS_CLibrary.CIS_Textbox dtLrDate;
        private Crocus_CClib.DataGridViewX GrdMain;
        private Crocus_CClib.DataGridViewX GrdDtls;
    }
}
