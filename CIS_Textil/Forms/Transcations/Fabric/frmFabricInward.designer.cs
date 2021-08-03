using System.Windows.Forms;
namespace CIS_Textil
{
    partial class frmFabricInward
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        /// 
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
            this.lblProcesser = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.cboProcesser = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblProcesser1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblLrDt = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtLrNo = new CIS_CLibrary.CIS_Textbox();
            this.lblLrNo = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblLrNoColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblLrDtColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblTrnsprtColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.cboTransport = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblTrnsprt = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblDeprtmt = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.cboDepartment = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblDeprtmtColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblDescription = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtDescription = new CIS_CLibrary.CIS_Textbox();
            this.lblDescriptionColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblEntryNo = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.lblEntryDtColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblEntryDt = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblEntryNoColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtEntryNo = new CIS_CLibrary.CIS_Textbox();
            this.lblBrokerColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.cboBroker = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblBroker = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblRecvdColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblRecvd = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblSupplierColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.cboSupplier = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblSupplier = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblRefNoColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtRefNo = new CIS_CLibrary.CIS_Textbox();
            this.lblRefNo = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.label5 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.label6 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.cboOrderType = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.btnSelectStock = new CIS_CLibrary.CIS_Button();
            this.dtEntryDate = new CIS_CLibrary.CIS_Textbox();
            this.dtChallanDate = new CIS_CLibrary.CIS_Textbox();
            this.txtUniqueID = new CIS_CLibrary.CIS_Textbox();
            this.dtLrDate = new CIS_CLibrary.CIS_Textbox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtEd1 = new CIS_CLibrary.CIS_Textbox();
            this.GrdMain = new Crocus_CClib.DataGridViewX();
            this.lblED1Colon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblED1 = new CIS_CLibrary.CIS_TextLabel(this.components);
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
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.lblED1Colon);
            this.pnlContent.Controls.Add(this.lblED1);
            this.pnlContent.Controls.Add(this.lblEI1);
            this.pnlContent.Controls.Add(this.lblEI1Colon);
            this.pnlContent.Controls.Add(this.cboEI1);
            this.pnlContent.Controls.Add(this.lblEI2);
            this.pnlContent.Controls.Add(this.cboEI2);
            this.pnlContent.Controls.Add(this.lblEI2Colon);
            this.pnlContent.Controls.Add(this.txtET3);
            this.pnlContent.Controls.Add(this.lblET3Colon);
            this.pnlContent.Controls.Add(this.txtET2);
            this.pnlContent.Controls.Add(this.lblET2Colon);
            this.pnlContent.Controls.Add(this.lblET3);
            this.pnlContent.Controls.Add(this.lblET2);
            this.pnlContent.Controls.Add(this.txtET1);
            this.pnlContent.Controls.Add(this.lblET1Colon);
            this.pnlContent.Controls.Add(this.lblET1);
            this.pnlContent.Controls.Add(this.dtLrDate);
            this.pnlContent.Controls.Add(this.txtUniqueID);
            this.pnlContent.Controls.Add(this.panel1);
            this.pnlContent.Controls.Add(this.dtEntryDate);
            this.pnlContent.Controls.Add(this.label5);
            this.pnlContent.Controls.Add(this.label6);
            this.pnlContent.Controls.Add(this.cboOrderType);
            this.pnlContent.Controls.Add(this.lblProcesser);
            this.pnlContent.Controls.Add(this.cboProcesser);
            this.pnlContent.Controls.Add(this.lblProcesser1);
            this.pnlContent.Controls.Add(this.lblLrDt);
            this.pnlContent.Controls.Add(this.txtLrNo);
            this.pnlContent.Controls.Add(this.lblLrNo);
            this.pnlContent.Controls.Add(this.lblLrNoColon);
            this.pnlContent.Controls.Add(this.lblLrDtColon);
            this.pnlContent.Controls.Add(this.lblTrnsprtColon);
            this.pnlContent.Controls.Add(this.cboTransport);
            this.pnlContent.Controls.Add(this.lblTrnsprt);
            this.pnlContent.Controls.Add(this.lblDeprtmt);
            this.pnlContent.Controls.Add(this.cboDepartment);
            this.pnlContent.Controls.Add(this.lblDeprtmtColon);
            this.pnlContent.Controls.Add(this.lblDescription);
            this.pnlContent.Controls.Add(this.txtDescription);
            this.pnlContent.Controls.Add(this.lblDescriptionColon);
            this.pnlContent.Controls.Add(this.lblEntryNo);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.Add(this.lblEntryDtColon);
            this.pnlContent.Controls.Add(this.lblEntryDt);
            this.pnlContent.Controls.Add(this.lblEntryNoColon);
            this.pnlContent.Controls.Add(this.txtEntryNo);
            this.pnlContent.Controls.Add(this.lblBrokerColon);
            this.pnlContent.Controls.Add(this.cboBroker);
            this.pnlContent.Controls.Add(this.lblBroker);
            this.pnlContent.Controls.Add(this.lblRecvdColon);
            this.pnlContent.Controls.Add(this.lblRecvd);
            this.pnlContent.Controls.Add(this.lblSupplierColon);
            this.pnlContent.Controls.Add(this.cboSupplier);
            this.pnlContent.Controls.Add(this.lblSupplier);
            this.pnlContent.Controls.Add(this.lblRefNoColon);
            this.pnlContent.Controls.Add(this.txtRefNo);
            this.pnlContent.Controls.Add(this.lblRefNo);
            this.pnlContent.Controls.Add(this.btnSelectStock);
            this.pnlContent.Controls.Add(this.dtChallanDate);
            this.pnlContent.Size = new System.Drawing.Size(1072, 498);
            this.pnlContent.Controls.SetChildIndex(this.dtChallanDate, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnSelectStock, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblRefNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtRefNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblRefNoColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblSupplier, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboSupplier, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblSupplierColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblRecvd, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblRecvdColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblBroker, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboBroker, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblBrokerColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtEntryNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryNoColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryDt, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryDtColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblDescriptionColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtDescription, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblDescription, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblDeprtmtColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboDepartment, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblDeprtmt, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblTrnsprt, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboTransport, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblTrnsprtColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblLrDtColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblLrNoColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblLrNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtLrNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblLrDt, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblProcesser1, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboProcesser, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblProcesser, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboOrderType, 0);
            this.pnlContent.Controls.SetChildIndex(this.label6, 0);
            this.pnlContent.Controls.SetChildIndex(this.label5, 0);
            this.pnlContent.Controls.SetChildIndex(this.dtEntryDate, 0);
            this.pnlContent.Controls.SetChildIndex(this.panel1, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtUniqueID, 0);
            this.pnlContent.Controls.SetChildIndex(this.dtLrDate, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET1, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET1Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtET1, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET2, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET3, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET2Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtET2, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET3Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtET3, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEI2Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboEI2, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEI2, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboEI1, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEI1Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEI1, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblED1, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblED1Colon, 0);
            // 
            // lblProcesser
            // 
            this.lblProcesser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblProcesser.AutoSize = true;
            this.lblProcesser.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblProcesser.Location = new System.Drawing.Point(110, 124);
            this.lblProcesser.Moveable = false;
            this.lblProcesser.Name = "lblProcesser";
            this.lblProcesser.NameOfControl = null;
            this.lblProcesser.Size = new System.Drawing.Size(73, 14);
            this.lblProcesser.TabIndex = 90147;
            this.lblProcesser.Text = "Processer";
            // 
            // cboProcesser
            // 
            this.cboProcesser.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboProcesser.AutoComplete = false;
            this.cboProcesser.AutoDropdown = false;
            this.cboProcesser.BackColor = System.Drawing.Color.White;
            this.cboProcesser.BackColorEven = System.Drawing.Color.White;
            this.cboProcesser.BackColorOdd = System.Drawing.Color.White;
            this.cboProcesser.ColumnNames = "";
            this.cboProcesser.ColumnWidthDefault = 175;
            this.cboProcesser.ColumnWidths = "";
            this.cboProcesser.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboProcesser.Fill_ComboID = 0;
            this.cboProcesser.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProcesser.FormattingEnabled = true;
            this.cboProcesser.GroupType = 0;
            this.cboProcesser.HelpText = "Select Processer";
            this.cboProcesser.IsMandatory = false;
            this.cboProcesser.LinkedColumnIndex = 0;
            this.cboProcesser.LinkedTextBox = null;
            this.cboProcesser.Location = new System.Drawing.Point(203, 121);
            this.cboProcesser.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboProcesser.Moveable = false;
            this.cboProcesser.Name = "cboProcesser";
            this.cboProcesser.NameOfControl = "Processor";
            this.cboProcesser.OpenForm = null;
            this.cboProcesser.ShowBallonTip = false;
            this.cboProcesser.Size = new System.Drawing.Size(315, 23);
            this.cboProcesser.TabIndex = 12;
            // 
            // lblProcesser1
            // 
            this.lblProcesser1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblProcesser1.AutoSize = true;
            this.lblProcesser1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblProcesser1.Location = new System.Drawing.Point(189, 125);
            this.lblProcesser1.Moveable = false;
            this.lblProcesser1.Name = "lblProcesser1";
            this.lblProcesser1.NameOfControl = null;
            this.lblProcesser1.Size = new System.Drawing.Size(12, 14);
            this.lblProcesser1.TabIndex = 90146;
            this.lblProcesser1.Text = ":";
            // 
            // lblLrDt
            // 
            this.lblLrDt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLrDt.AutoSize = true;
            this.lblLrDt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblLrDt.Location = new System.Drawing.Point(775, 97);
            this.lblLrDt.Moveable = false;
            this.lblLrDt.Name = "lblLrDt";
            this.lblLrDt.NameOfControl = null;
            this.lblLrDt.Size = new System.Drawing.Size(56, 14);
            this.lblLrDt.TabIndex = 90140;
            this.lblLrDt.Text = "Lr Date";
            // 
            // txtLrNo
            // 
            this.txtLrNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtLrNo.AutoFillDate = false;
            this.txtLrNo.BackColor = System.Drawing.Color.White;
            this.txtLrNo.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtLrNo.CheckForSymbol = null;
            this.txtLrNo.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtLrNo.DecimalPlace = 0;
            this.txtLrNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLrNo.HelpText = "Enter Lr.No.";
            this.txtLrNo.HoldMyText = null;
            this.txtLrNo.IsMandatory = false;
            this.txtLrNo.IsSingleQuote = true;
            this.txtLrNo.IsSysmbol = false;
            this.txtLrNo.Location = new System.Drawing.Point(641, 94);
            this.txtLrNo.Mask = null;
            this.txtLrNo.MaxLength = 20;
            this.txtLrNo.Moveable = false;
            this.txtLrNo.Name = "txtLrNo";
            this.txtLrNo.NameOfControl = "Lr No";
            this.txtLrNo.Prefix = null;
            this.txtLrNo.ShowBallonTip = false;
            this.txtLrNo.ShowErrorIcon = false;
            this.txtLrNo.ShowMessage = null;
            this.txtLrNo.Size = new System.Drawing.Size(95, 22);
            this.txtLrNo.Suffix = null;
            this.txtLrNo.TabIndex = 10;
            // 
            // lblLrNo
            // 
            this.lblLrNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLrNo.AutoSize = true;
            this.lblLrNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblLrNo.Location = new System.Drawing.Point(540, 96);
            this.lblLrNo.Moveable = false;
            this.lblLrNo.Name = "lblLrNo";
            this.lblLrNo.NameOfControl = null;
            this.lblLrNo.Size = new System.Drawing.Size(47, 14);
            this.lblLrNo.TabIndex = 90138;
            this.lblLrNo.Text = "Lr No.";
            // 
            // lblLrNoColon
            // 
            this.lblLrNoColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLrNoColon.AutoSize = true;
            this.lblLrNoColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblLrNoColon.Location = new System.Drawing.Point(626, 96);
            this.lblLrNoColon.Moveable = false;
            this.lblLrNoColon.Name = "lblLrNoColon";
            this.lblLrNoColon.NameOfControl = null;
            this.lblLrNoColon.Size = new System.Drawing.Size(12, 14);
            this.lblLrNoColon.TabIndex = 90139;
            this.lblLrNoColon.Text = ":";
            // 
            // lblLrDtColon
            // 
            this.lblLrDtColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblLrDtColon.AutoSize = true;
            this.lblLrDtColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblLrDtColon.Location = new System.Drawing.Point(843, 98);
            this.lblLrDtColon.Moveable = false;
            this.lblLrDtColon.Name = "lblLrDtColon";
            this.lblLrDtColon.NameOfControl = null;
            this.lblLrDtColon.Size = new System.Drawing.Size(12, 14);
            this.lblLrDtColon.TabIndex = 90141;
            this.lblLrDtColon.Text = ":";
            // 
            // lblTrnsprtColon
            // 
            this.lblTrnsprtColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTrnsprtColon.AutoSize = true;
            this.lblTrnsprtColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblTrnsprtColon.Location = new System.Drawing.Point(189, 98);
            this.lblTrnsprtColon.Moveable = false;
            this.lblTrnsprtColon.Name = "lblTrnsprtColon";
            this.lblTrnsprtColon.NameOfControl = null;
            this.lblTrnsprtColon.Size = new System.Drawing.Size(12, 14);
            this.lblTrnsprtColon.TabIndex = 90137;
            this.lblTrnsprtColon.Text = ":";
            // 
            // cboTransport
            // 
            this.cboTransport.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboTransport.AutoComplete = false;
            this.cboTransport.AutoDropdown = false;
            this.cboTransport.BackColor = System.Drawing.Color.White;
            this.cboTransport.BackColorEven = System.Drawing.Color.White;
            this.cboTransport.BackColorOdd = System.Drawing.Color.White;
            this.cboTransport.ColumnNames = "";
            this.cboTransport.ColumnWidthDefault = 175;
            this.cboTransport.ColumnWidths = "";
            this.cboTransport.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboTransport.Fill_ComboID = 0;
            this.cboTransport.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTransport.FormattingEnabled = true;
            this.cboTransport.GroupType = 0;
            this.cboTransport.HelpText = "Select Transport";
            this.cboTransport.IsMandatory = false;
            this.cboTransport.LinkedColumnIndex = 0;
            this.cboTransport.LinkedTextBox = null;
            this.cboTransport.Location = new System.Drawing.Point(203, 94);
            this.cboTransport.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboTransport.Moveable = false;
            this.cboTransport.Name = "cboTransport";
            this.cboTransport.NameOfControl = "Transport";
            this.cboTransport.OpenForm = null;
            this.cboTransport.ShowBallonTip = false;
            this.cboTransport.Size = new System.Drawing.Size(315, 23);
            this.cboTransport.TabIndex = 9;
            // 
            // lblTrnsprt
            // 
            this.lblTrnsprt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTrnsprt.AutoSize = true;
            this.lblTrnsprt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblTrnsprt.Location = new System.Drawing.Point(110, 98);
            this.lblTrnsprt.Moveable = false;
            this.lblTrnsprt.Name = "lblTrnsprt";
            this.lblTrnsprt.NameOfControl = null;
            this.lblTrnsprt.Size = new System.Drawing.Size(71, 14);
            this.lblTrnsprt.TabIndex = 90136;
            this.lblTrnsprt.Text = "Transport";
            // 
            // lblDeprtmt
            // 
            this.lblDeprtmt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDeprtmt.AutoSize = true;
            this.lblDeprtmt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblDeprtmt.Location = new System.Drawing.Point(540, 71);
            this.lblDeprtmt.Moveable = false;
            this.lblDeprtmt.Name = "lblDeprtmt";
            this.lblDeprtmt.NameOfControl = null;
            this.lblDeprtmt.Size = new System.Drawing.Size(85, 14);
            this.lblDeprtmt.TabIndex = 90135;
            this.lblDeprtmt.Text = "Department";
            // 
            // cboDepartment
            // 
            this.cboDepartment.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboDepartment.AutoComplete = false;
            this.cboDepartment.AutoDropdown = false;
            this.cboDepartment.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboDepartment.BackColorEven = System.Drawing.Color.White;
            this.cboDepartment.BackColorOdd = System.Drawing.Color.White;
            this.cboDepartment.ColumnNames = "";
            this.cboDepartment.ColumnWidthDefault = 175;
            this.cboDepartment.ColumnWidths = "";
            this.cboDepartment.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboDepartment.Fill_ComboID = 0;
            this.cboDepartment.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDepartment.FormattingEnabled = true;
            this.cboDepartment.GroupType = 0;
            this.cboDepartment.HelpText = "Select Department";
            this.cboDepartment.IsMandatory = true;
            this.cboDepartment.LinkedColumnIndex = 0;
            this.cboDepartment.LinkedTextBox = null;
            this.cboDepartment.Location = new System.Drawing.Point(641, 67);
            this.cboDepartment.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboDepartment.Moveable = false;
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.NameOfControl = "Department";
            this.cboDepartment.OpenForm = null;
            this.cboDepartment.ShowBallonTip = false;
            this.cboDepartment.Size = new System.Drawing.Size(315, 23);
            this.cboDepartment.TabIndex = 8;
            // 
            // lblDeprtmtColon
            // 
            this.lblDeprtmtColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDeprtmtColon.AutoSize = true;
            this.lblDeprtmtColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblDeprtmtColon.Location = new System.Drawing.Point(626, 71);
            this.lblDeprtmtColon.Moveable = false;
            this.lblDeprtmtColon.Name = "lblDeprtmtColon";
            this.lblDeprtmtColon.NameOfControl = null;
            this.lblDeprtmtColon.Size = new System.Drawing.Size(12, 14);
            this.lblDeprtmtColon.TabIndex = 90122;
            this.lblDeprtmtColon.Text = ":";
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescription.Location = new System.Drawing.Point(48, 464);
            this.lblDescription.Moveable = false;
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.NameOfControl = null;
            this.lblDescription.Size = new System.Drawing.Size(82, 14);
            this.lblDescription.TabIndex = 90129;
            this.lblDescription.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDescription.AutoFillDate = false;
            this.txtDescription.BackColor = System.Drawing.Color.White;
            this.txtDescription.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtDescription.CheckForSymbol = null;
            this.txtDescription.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtDescription.DecimalPlace = 0;
            this.txtDescription.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.HelpText = "Enter Description";
            this.txtDescription.HoldMyText = null;
            this.txtDescription.IsMandatory = false;
            this.txtDescription.IsSingleQuote = true;
            this.txtDescription.IsSysmbol = false;
            this.txtDescription.Location = new System.Drawing.Point(141, 461);
            this.txtDescription.Mask = null;
            this.txtDescription.MaxLength = 1000;
            this.txtDescription.Moveable = false;
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.NameOfControl = "Description";
            this.txtDescription.Prefix = null;
            this.txtDescription.ShowBallonTip = false;
            this.txtDescription.ShowErrorIcon = false;
            this.txtDescription.ShowMessage = null;
            this.txtDescription.Size = new System.Drawing.Size(875, 20);
            this.txtDescription.Suffix = null;
            this.txtDescription.TabIndex = 16;
            // 
            // lblDescriptionColon
            // 
            this.lblDescriptionColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDescriptionColon.AutoSize = true;
            this.lblDescriptionColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblDescriptionColon.Location = new System.Drawing.Point(129, 463);
            this.lblDescriptionColon.Moveable = false;
            this.lblDescriptionColon.Name = "lblDescriptionColon";
            this.lblDescriptionColon.NameOfControl = null;
            this.lblDescriptionColon.Size = new System.Drawing.Size(12, 14);
            this.lblDescriptionColon.TabIndex = 90130;
            this.lblDescriptionColon.Text = ":";
            // 
            // lblEntryNo
            // 
            this.lblEntryNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEntryNo.AutoSize = true;
            this.lblEntryNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblEntryNo.Location = new System.Drawing.Point(110, 22);
            this.lblEntryNo.Moveable = false;
            this.lblEntryNo.Name = "lblEntryNo";
            this.lblEntryNo.NameOfControl = null;
            this.lblEntryNo.Size = new System.Drawing.Size(68, 14);
            this.lblEntryNo.TabIndex = 90124;
            this.lblEntryNo.Text = "Entry No.";
            // 
            // txtCode
            // 
            this.txtCode.AutoFillDate = false;
            this.txtCode.BackColor = System.Drawing.Color.White;
            this.txtCode.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Lower;
            this.txtCode.CheckForSymbol = null;
            this.txtCode.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtCode.DecimalPlace = 0;
            this.txtCode.Enabled = false;
            this.txtCode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.HelpText = "";
            this.txtCode.HoldMyText = null;
            this.txtCode.IsMandatory = false;
            this.txtCode.IsSingleQuote = true;
            this.txtCode.IsSysmbol = false;
            this.txtCode.Location = new System.Drawing.Point(6, 1);
            this.txtCode.Mask = null;
            this.txtCode.Moveable = false;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(42, 22);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 90128;
            this.txtCode.Visible = false;
            // 
            // lblEntryDtColon
            // 
            this.lblEntryDtColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEntryDtColon.AutoSize = true;
            this.lblEntryDtColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblEntryDtColon.Location = new System.Drawing.Point(409, 22);
            this.lblEntryDtColon.Moveable = false;
            this.lblEntryDtColon.Name = "lblEntryDtColon";
            this.lblEntryDtColon.NameOfControl = null;
            this.lblEntryDtColon.Size = new System.Drawing.Size(12, 14);
            this.lblEntryDtColon.TabIndex = 90127;
            this.lblEntryDtColon.Text = ":";
            // 
            // lblEntryDt
            // 
            this.lblEntryDt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEntryDt.AutoSize = true;
            this.lblEntryDt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblEntryDt.Location = new System.Drawing.Point(337, 23);
            this.lblEntryDt.Moveable = false;
            this.lblEntryDt.Name = "lblEntryDt";
            this.lblEntryDt.NameOfControl = null;
            this.lblEntryDt.Size = new System.Drawing.Size(77, 14);
            this.lblEntryDt.TabIndex = 90125;
            this.lblEntryDt.Text = "Entry Date";
            // 
            // lblEntryNoColon
            // 
            this.lblEntryNoColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEntryNoColon.AutoSize = true;
            this.lblEntryNoColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblEntryNoColon.Location = new System.Drawing.Point(191, 22);
            this.lblEntryNoColon.Moveable = false;
            this.lblEntryNoColon.Name = "lblEntryNoColon";
            this.lblEntryNoColon.NameOfControl = null;
            this.lblEntryNoColon.Size = new System.Drawing.Size(12, 14);
            this.lblEntryNoColon.TabIndex = 90126;
            this.lblEntryNoColon.Text = ":";
            // 
            // txtEntryNo
            // 
            this.txtEntryNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEntryNo.AutoFillDate = false;
            this.txtEntryNo.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtEntryNo.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Lower;
            this.txtEntryNo.CheckForSymbol = null;
            this.txtEntryNo.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtEntryNo.DecimalPlace = 0;
            this.txtEntryNo.Enabled = false;
            this.txtEntryNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEntryNo.HelpText = "Entry No";
            this.txtEntryNo.HoldMyText = null;
            this.txtEntryNo.IsMandatory = true;
            this.txtEntryNo.IsSingleQuote = true;
            this.txtEntryNo.IsSysmbol = false;
            this.txtEntryNo.Location = new System.Drawing.Point(203, 19);
            this.txtEntryNo.Mask = null;
            this.txtEntryNo.Moveable = false;
            this.txtEntryNo.Name = "txtEntryNo";
            this.txtEntryNo.NameOfControl = "Entry No";
            this.txtEntryNo.Prefix = null;
            this.txtEntryNo.ShowBallonTip = false;
            this.txtEntryNo.ShowErrorIcon = false;
            this.txtEntryNo.ShowMessage = null;
            this.txtEntryNo.Size = new System.Drawing.Size(85, 22);
            this.txtEntryNo.Suffix = null;
            this.txtEntryNo.TabIndex = 1;
            this.txtEntryNo.TabStop = false;
            // 
            // lblBrokerColon
            // 
            this.lblBrokerColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblBrokerColon.AutoSize = true;
            this.lblBrokerColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblBrokerColon.Location = new System.Drawing.Point(189, 70);
            this.lblBrokerColon.Moveable = false;
            this.lblBrokerColon.Name = "lblBrokerColon";
            this.lblBrokerColon.NameOfControl = null;
            this.lblBrokerColon.Size = new System.Drawing.Size(12, 14);
            this.lblBrokerColon.TabIndex = 90123;
            this.lblBrokerColon.Text = ":";
            // 
            // cboBroker
            // 
            this.cboBroker.Anchor = System.Windows.Forms.AnchorStyles.None;
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
            this.cboBroker.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBroker.FormattingEnabled = true;
            this.cboBroker.GroupType = 0;
            this.cboBroker.HelpText = "Select Broker";
            this.cboBroker.IsMandatory = false;
            this.cboBroker.LinkedColumnIndex = 0;
            this.cboBroker.LinkedTextBox = null;
            this.cboBroker.Location = new System.Drawing.Point(203, 68);
            this.cboBroker.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboBroker.Moveable = false;
            this.cboBroker.Name = "cboBroker";
            this.cboBroker.NameOfControl = "Broker";
            this.cboBroker.OpenForm = null;
            this.cboBroker.ShowBallonTip = false;
            this.cboBroker.Size = new System.Drawing.Size(315, 23);
            this.cboBroker.TabIndex = 7;
            // 
            // lblBroker
            // 
            this.lblBroker.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblBroker.AutoSize = true;
            this.lblBroker.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblBroker.Location = new System.Drawing.Point(110, 70);
            this.lblBroker.Moveable = false;
            this.lblBroker.Name = "lblBroker";
            this.lblBroker.NameOfControl = null;
            this.lblBroker.Size = new System.Drawing.Size(52, 14);
            this.lblBroker.TabIndex = 90118;
            this.lblBroker.Text = "Broker";
            // 
            // lblRecvdColon
            // 
            this.lblRecvdColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRecvdColon.AutoSize = true;
            this.lblRecvdColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblRecvdColon.Location = new System.Drawing.Point(843, 45);
            this.lblRecvdColon.Moveable = false;
            this.lblRecvdColon.Name = "lblRecvdColon";
            this.lblRecvdColon.NameOfControl = null;
            this.lblRecvdColon.Size = new System.Drawing.Size(12, 14);
            this.lblRecvdColon.TabIndex = 90121;
            this.lblRecvdColon.Text = ":";
            // 
            // lblRecvd
            // 
            this.lblRecvd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRecvd.AutoSize = true;
            this.lblRecvd.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblRecvd.Location = new System.Drawing.Point(775, 45);
            this.lblRecvd.Moveable = false;
            this.lblRecvd.Name = "lblRecvd";
            this.lblRecvd.NameOfControl = null;
            this.lblRecvd.Size = new System.Drawing.Size(67, 14);
            this.lblRecvd.TabIndex = 90116;
            this.lblRecvd.Text = "Received";
            // 
            // lblSupplierColon
            // 
            this.lblSupplierColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSupplierColon.AutoSize = true;
            this.lblSupplierColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblSupplierColon.Location = new System.Drawing.Point(191, 47);
            this.lblSupplierColon.Moveable = false;
            this.lblSupplierColon.Name = "lblSupplierColon";
            this.lblSupplierColon.NameOfControl = null;
            this.lblSupplierColon.Size = new System.Drawing.Size(12, 14);
            this.lblSupplierColon.TabIndex = 90120;
            this.lblSupplierColon.Text = ":";
            // 
            // cboSupplier
            // 
            this.cboSupplier.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboSupplier.AutoComplete = false;
            this.cboSupplier.AutoDropdown = false;
            this.cboSupplier.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboSupplier.BackColorEven = System.Drawing.Color.White;
            this.cboSupplier.BackColorOdd = System.Drawing.Color.White;
            this.cboSupplier.ColumnNames = "";
            this.cboSupplier.ColumnWidthDefault = 175;
            this.cboSupplier.ColumnWidths = "";
            this.cboSupplier.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboSupplier.Fill_ComboID = 0;
            this.cboSupplier.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSupplier.FormattingEnabled = true;
            this.cboSupplier.GroupType = 0;
            this.cboSupplier.HelpText = "Select Supplier";
            this.cboSupplier.IsMandatory = true;
            this.cboSupplier.LinkedColumnIndex = 0;
            this.cboSupplier.LinkedTextBox = null;
            this.cboSupplier.Location = new System.Drawing.Point(203, 43);
            this.cboSupplier.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboSupplier.Moveable = false;
            this.cboSupplier.Name = "cboSupplier";
            this.cboSupplier.NameOfControl = "Supplier";
            this.cboSupplier.OpenForm = null;
            this.cboSupplier.ShowBallonTip = false;
            this.cboSupplier.Size = new System.Drawing.Size(315, 23);
            this.cboSupplier.TabIndex = 4;
            this.cboSupplier.SelectedValueChanged += new System.EventHandler(this.cboSupplier_SelectedValueChanged);
            // 
            // lblSupplier
            // 
            this.lblSupplier.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblSupplier.Location = new System.Drawing.Point(110, 47);
            this.lblSupplier.Moveable = false;
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.NameOfControl = null;
            this.lblSupplier.Size = new System.Drawing.Size(62, 14);
            this.lblSupplier.TabIndex = 90117;
            this.lblSupplier.Text = "Supplier";
            // 
            // lblRefNoColon
            // 
            this.lblRefNoColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRefNoColon.AutoSize = true;
            this.lblRefNoColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblRefNoColon.Location = new System.Drawing.Point(626, 44);
            this.lblRefNoColon.Moveable = false;
            this.lblRefNoColon.Name = "lblRefNoColon";
            this.lblRefNoColon.NameOfControl = null;
            this.lblRefNoColon.Size = new System.Drawing.Size(12, 14);
            this.lblRefNoColon.TabIndex = 90119;
            this.lblRefNoColon.Text = ":";
            // 
            // txtRefNo
            // 
            this.txtRefNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtRefNo.AutoFillDate = false;
            this.txtRefNo.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtRefNo.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtRefNo.CheckForSymbol = null;
            this.txtRefNo.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtRefNo.DecimalPlace = 0;
            this.txtRefNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRefNo.HelpText = "Enter Challan No";
            this.txtRefNo.HoldMyText = null;
            this.txtRefNo.IsMandatory = true;
            this.txtRefNo.IsSingleQuote = true;
            this.txtRefNo.IsSysmbol = false;
            this.txtRefNo.Location = new System.Drawing.Point(641, 42);
            this.txtRefNo.Mask = null;
            this.txtRefNo.Moveable = false;
            this.txtRefNo.Name = "txtRefNo";
            this.txtRefNo.NameOfControl = "Challan No";
            this.txtRefNo.Prefix = null;
            this.txtRefNo.ShowBallonTip = false;
            this.txtRefNo.ShowErrorIcon = false;
            this.txtRefNo.ShowMessage = null;
            this.txtRefNo.Size = new System.Drawing.Size(95, 22);
            this.txtRefNo.Suffix = null;
            this.txtRefNo.TabIndex = 5;
            // 
            // lblRefNo
            // 
            this.lblRefNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblRefNo.AutoSize = true;
            this.lblRefNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblRefNo.Location = new System.Drawing.Point(540, 44);
            this.lblRefNo.Moveable = false;
            this.lblRefNo.Name = "lblRefNo";
            this.lblRefNo.NameOfControl = null;
            this.lblRefNo.Size = new System.Drawing.Size(82, 14);
            this.lblRefNo.TabIndex = 90115;
            this.lblRefNo.Text = "Challan No.";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(625, 19);
            this.label5.Moveable = false;
            this.label5.Name = "label5";
            this.label5.NameOfControl = null;
            this.label5.Size = new System.Drawing.Size(12, 14);
            this.label5.TabIndex = 90151;
            this.label5.Text = ":";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(540, 20);
            this.label6.Moveable = false;
            this.label6.Name = "label6";
            this.label6.NameOfControl = null;
            this.label6.Size = new System.Drawing.Size(82, 14);
            this.label6.TabIndex = 90150;
            this.label6.Text = "Order Type";
            // 
            // cboOrderType
            // 
            this.cboOrderType.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboOrderType.AutoComplete = false;
            this.cboOrderType.AutoDropdown = true;
            this.cboOrderType.BackColor = System.Drawing.Color.White;
            this.cboOrderType.BackColorEven = System.Drawing.Color.White;
            this.cboOrderType.BackColorOdd = System.Drawing.Color.White;
            this.cboOrderType.ColumnNames = "";
            this.cboOrderType.ColumnWidthDefault = 175;
            this.cboOrderType.ColumnWidths = "";
            this.cboOrderType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboOrderType.Fill_ComboID = 0;
            this.cboOrderType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.cboOrderType.FormattingEnabled = true;
            this.cboOrderType.GroupType = 0;
            this.cboOrderType.HelpText = "Select OrderType";
            this.cboOrderType.IsMandatory = false;
            this.cboOrderType.Items.AddRange(new object[] {
            "WITH ORDER",
            "WITHOUT ORDER"});
            this.cboOrderType.LinkedColumnIndex = 0;
            this.cboOrderType.LinkedTextBox = null;
            this.cboOrderType.Location = new System.Drawing.Point(641, 15);
            this.cboOrderType.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboOrderType.Moveable = false;
            this.cboOrderType.Name = "cboOrderType";
            this.cboOrderType.NameOfControl = "OrderType";
            this.cboOrderType.OpenForm = null;
            this.cboOrderType.ShowBallonTip = false;
            this.cboOrderType.Size = new System.Drawing.Size(315, 23);
            this.cboOrderType.TabIndex = 3;
            this.cboOrderType.SelectedIndexChanged += new System.EventHandler(this.cboOrderType_SelectedIndexChanged);
            // 
            // btnSelectStock
            // 
            this.btnSelectStock.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSelectStock.BackColor = System.Drawing.Color.CadetBlue;
            this.btnSelectStock.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelectStock.Location = new System.Drawing.Point(824, 121);
            this.btnSelectStock.Moveable = false;
            this.btnSelectStock.Name = "btnSelectStock";
            this.btnSelectStock.Size = new System.Drawing.Size(130, 23);
            this.btnSelectStock.TabIndex = 13;
            this.btnSelectStock.Text = "Select Orders";
            this.btnSelectStock.UseVisualStyleBackColor = false;
            this.btnSelectStock.Visible = false;
            this.btnSelectStock.Click += new System.EventHandler(this.btnSelectStock_Click);
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
            this.dtEntryDate.Location = new System.Drawing.Point(424, 18);
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
            // 
            // dtChallanDate
            // 
            this.dtChallanDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtChallanDate.AutoFillDate = true;
            this.dtChallanDate.BackColor = System.Drawing.Color.PapayaWhip;
            this.dtChallanDate.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.dtChallanDate.CheckForSymbol = null;
            this.dtChallanDate.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.ApplyDate;
            this.dtChallanDate.DecimalPlace = 0;
            this.dtChallanDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.dtChallanDate.HelpText = "Enter Recieved Date";
            this.dtChallanDate.HoldMyText = null;
            this.dtChallanDate.IsMandatory = true;
            this.dtChallanDate.IsSingleQuote = true;
            this.dtChallanDate.IsSysmbol = false;
            this.dtChallanDate.Location = new System.Drawing.Point(861, 42);
            this.dtChallanDate.Mask = "__/__/____";
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
            this.dtChallanDate.TabIndex = 6;
            this.dtChallanDate.Text = "__/__/____";
            // 
            // txtUniqueID
            // 
            this.txtUniqueID.AutoFillDate = false;
            this.txtUniqueID.BackColor = System.Drawing.Color.White;
            this.txtUniqueID.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Lower;
            this.txtUniqueID.CheckForSymbol = null;
            this.txtUniqueID.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtUniqueID.DecimalPlace = 0;
            this.txtUniqueID.Enabled = false;
            this.txtUniqueID.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUniqueID.HelpText = "";
            this.txtUniqueID.HoldMyText = null;
            this.txtUniqueID.IsMandatory = false;
            this.txtUniqueID.IsSingleQuote = true;
            this.txtUniqueID.IsSysmbol = false;
            this.txtUniqueID.Location = new System.Drawing.Point(4, 25);
            this.txtUniqueID.Mask = null;
            this.txtUniqueID.Moveable = false;
            this.txtUniqueID.Name = "txtUniqueID";
            this.txtUniqueID.NameOfControl = null;
            this.txtUniqueID.Prefix = null;
            this.txtUniqueID.ShowBallonTip = false;
            this.txtUniqueID.ShowErrorIcon = false;
            this.txtUniqueID.ShowMessage = null;
            this.txtUniqueID.Size = new System.Drawing.Size(42, 22);
            this.txtUniqueID.Suffix = null;
            this.txtUniqueID.TabIndex = 90152;
            this.txtUniqueID.Visible = false;
            // 
            // dtLrDate
            // 
            this.dtLrDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtLrDate.AutoFillDate = false;
            this.dtLrDate.BackColor = System.Drawing.Color.PapayaWhip;
            this.dtLrDate.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.dtLrDate.CheckForSymbol = null;
            this.dtLrDate.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.ApplyDate;
            this.dtLrDate.DecimalPlace = 0;
            this.dtLrDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.dtLrDate.HelpText = "Enter Lr Date";
            this.dtLrDate.HoldMyText = null;
            this.dtLrDate.IsMandatory = true;
            this.dtLrDate.IsSingleQuote = true;
            this.dtLrDate.IsSysmbol = false;
            this.dtLrDate.Location = new System.Drawing.Point(861, 94);
            this.dtLrDate.Mask = "__/__/____";
            this.dtLrDate.MaxLength = 10;
            this.dtLrDate.Moveable = false;
            this.dtLrDate.Name = "dtLrDate";
            this.dtLrDate.NameOfControl = "LR Date";
            this.dtLrDate.Prefix = null;
            this.dtLrDate.ShowBallonTip = false;
            this.dtLrDate.ShowErrorIcon = false;
            this.dtLrDate.ShowMessage = null;
            this.dtLrDate.Size = new System.Drawing.Size(94, 22);
            this.dtLrDate.Suffix = null;
            this.dtLrDate.TabIndex = 11;
            this.dtLrDate.Text = "__/__/____";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dtEd1);
            this.panel1.Controls.Add(this.GrdMain);
            this.panel1.Location = new System.Drawing.Point(54, 163);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(962, 284);
            this.panel1.TabIndex = 14;
            // 
            // dtEd1
            // 
            this.dtEd1.AutoFillDate = true;
            this.dtEd1.BackColor = System.Drawing.Color.PapayaWhip;
            this.dtEd1.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.dtEd1.CheckForSymbol = null;
            this.dtEd1.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.ApplyDate;
            this.dtEd1.DecimalPlace = 0;
            this.dtEd1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.dtEd1.HelpText = "Enter Opening Date";
            this.dtEd1.HoldMyText = null;
            this.dtEd1.IsMandatory = true;
            this.dtEd1.IsSingleQuote = true;
            this.dtEd1.IsSysmbol = false;
            this.dtEd1.Location = new System.Drawing.Point(418, 67);
            this.dtEd1.Mask = "__/__/____";
            this.dtEd1.MaxLength = 10;
            this.dtEd1.Moveable = false;
            this.dtEd1.Name = "dtEd1";
            this.dtEd1.NameOfControl = "ED1";
            this.dtEd1.Prefix = null;
            this.dtEd1.ShowBallonTip = false;
            this.dtEd1.ShowErrorIcon = false;
            this.dtEd1.ShowMessage = null;
            this.dtEd1.Size = new System.Drawing.Size(99, 22);
            this.dtEd1.Suffix = null;
            this.dtEd1.TabIndex = 503;
            this.dtEd1.Text = "__/__/____";
            this.dtEd1.Visible = false;
            // 
            // GrdMain
            // 
            this.GrdMain.blnFormAction = 0;
            this.GrdMain.CompID = 0;
            this.GrdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrdMain.Location = new System.Drawing.Point(0, 0);
            this.GrdMain.Name = "GrdMain";
            this.GrdMain.Size = new System.Drawing.Size(962, 284);
            this.GrdMain.TabIndex = 15;
            this.GrdMain.YearID = 0;
            // 
            // lblED1Colon
            // 
            this.lblED1Colon.AutoSize = true;
            this.lblED1Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblED1Colon.Location = new System.Drawing.Point(457, 234);
            this.lblED1Colon.Moveable = false;
            this.lblED1Colon.Name = "lblED1Colon";
            this.lblED1Colon.NameOfControl = "ED1";
            this.lblED1Colon.Size = new System.Drawing.Size(12, 14);
            this.lblED1Colon.TabIndex = 90170;
            this.lblED1Colon.Text = ":";
            this.lblED1Colon.Visible = false;
            // 
            // lblED1
            // 
            this.lblED1.AutoSize = true;
            this.lblED1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblED1.Location = new System.Drawing.Point(344, 232);
            this.lblED1.Moveable = false;
            this.lblED1.Name = "lblED1";
            this.lblED1.NameOfControl = "ED1";
            this.lblED1.Size = new System.Drawing.Size(34, 14);
            this.lblED1.TabIndex = 90169;
            this.lblED1.Text = "ED1";
            this.lblED1.Visible = false;
            // 
            // lblEI1
            // 
            this.lblEI1.AutoSize = true;
            this.lblEI1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI1.Location = new System.Drawing.Point(344, 182);
            this.lblEI1.Moveable = false;
            this.lblEI1.Name = "lblEI1";
            this.lblEI1.NameOfControl = "EI1";
            this.lblEI1.Size = new System.Drawing.Size(30, 14);
            this.lblEI1.TabIndex = 90166;
            this.lblEI1.Text = "EI1";
            this.lblEI1.Visible = false;
            // 
            // lblEI1Colon
            // 
            this.lblEI1Colon.AutoSize = true;
            this.lblEI1Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI1Colon.Location = new System.Drawing.Point(457, 182);
            this.lblEI1Colon.Moveable = false;
            this.lblEI1Colon.Name = "lblEI1Colon";
            this.lblEI1Colon.NameOfControl = "EI1";
            this.lblEI1Colon.Size = new System.Drawing.Size(12, 14);
            this.lblEI1Colon.TabIndex = 90167;
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
            this.cboEI1.Location = new System.Drawing.Point(471, 178);
            this.cboEI1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboEI1.Moveable = false;
            this.cboEI1.Name = "cboEI1";
            this.cboEI1.NameOfControl = "EI1";
            this.cboEI1.OpenForm = null;
            this.cboEI1.ShowBallonTip = false;
            this.cboEI1.Size = new System.Drawing.Size(231, 23);
            this.cboEI1.TabIndex = 501;
            this.cboEI1.Visible = false;
            // 
            // lblEI2
            // 
            this.lblEI2.AutoSize = true;
            this.lblEI2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI2.Location = new System.Drawing.Point(344, 206);
            this.lblEI2.Moveable = false;
            this.lblEI2.Name = "lblEI2";
            this.lblEI2.NameOfControl = "EI2";
            this.lblEI2.Size = new System.Drawing.Size(30, 14);
            this.lblEI2.TabIndex = 90163;
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
            this.cboEI2.Location = new System.Drawing.Point(471, 204);
            this.cboEI2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboEI2.Moveable = false;
            this.cboEI2.Name = "cboEI2";
            this.cboEI2.NameOfControl = "EI2";
            this.cboEI2.OpenForm = null;
            this.cboEI2.ShowBallonTip = false;
            this.cboEI2.Size = new System.Drawing.Size(231, 23);
            this.cboEI2.TabIndex = 502;
            this.cboEI2.Visible = false;
            // 
            // lblEI2Colon
            // 
            this.lblEI2Colon.AutoSize = true;
            this.lblEI2Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI2Colon.Location = new System.Drawing.Point(457, 206);
            this.lblEI2Colon.Moveable = false;
            this.lblEI2Colon.Name = "lblEI2Colon";
            this.lblEI2Colon.NameOfControl = null;
            this.lblEI2Colon.Size = new System.Drawing.Size(12, 14);
            this.lblEI2Colon.TabIndex = 90164;
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
            this.txtET3.Location = new System.Drawing.Point(471, 307);
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
            this.txtET3.TabIndex = 505;
            this.txtET3.Visible = false;
            // 
            // lblET3Colon
            // 
            this.lblET3Colon.AutoSize = true;
            this.lblET3Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET3Colon.Location = new System.Drawing.Point(457, 310);
            this.lblET3Colon.Moveable = false;
            this.lblET3Colon.Name = "lblET3Colon";
            this.lblET3Colon.NameOfControl = null;
            this.lblET3Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET3Colon.TabIndex = 90161;
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
            this.txtET2.Location = new System.Drawing.Point(471, 281);
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
            this.txtET2.TabIndex = 504;
            this.txtET2.Visible = false;
            // 
            // lblET2Colon
            // 
            this.lblET2Colon.AutoSize = true;
            this.lblET2Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET2Colon.Location = new System.Drawing.Point(457, 286);
            this.lblET2Colon.Moveable = false;
            this.lblET2Colon.Name = "lblET2Colon";
            this.lblET2Colon.NameOfControl = "ET2";
            this.lblET2Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET2Colon.TabIndex = 90159;
            this.lblET2Colon.Text = ":";
            this.lblET2Colon.Visible = false;
            // 
            // lblET3
            // 
            this.lblET3.AutoSize = true;
            this.lblET3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET3.Location = new System.Drawing.Point(344, 309);
            this.lblET3.Moveable = false;
            this.lblET3.Name = "lblET3";
            this.lblET3.NameOfControl = "ET3";
            this.lblET3.Size = new System.Drawing.Size(32, 14);
            this.lblET3.TabIndex = 90160;
            this.lblET3.Text = "ET3";
            this.lblET3.Visible = false;
            // 
            // lblET2
            // 
            this.lblET2.AutoSize = true;
            this.lblET2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET2.Location = new System.Drawing.Point(344, 284);
            this.lblET2.Moveable = false;
            this.lblET2.Name = "lblET2";
            this.lblET2.NameOfControl = "ET2";
            this.lblET2.Size = new System.Drawing.Size(32, 14);
            this.lblET2.TabIndex = 90158;
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
            this.txtET1.Location = new System.Drawing.Point(471, 256);
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
            this.txtET1.TabIndex = 504;
            this.txtET1.Visible = false;
            // 
            // lblET1Colon
            // 
            this.lblET1Colon.AutoSize = true;
            this.lblET1Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET1Colon.Location = new System.Drawing.Point(457, 258);
            this.lblET1Colon.Moveable = false;
            this.lblET1Colon.Name = "lblET1Colon";
            this.lblET1Colon.NameOfControl = "ET1";
            this.lblET1Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET1Colon.TabIndex = 90157;
            this.lblET1Colon.Text = ":";
            this.lblET1Colon.Visible = false;
            // 
            // lblET1
            // 
            this.lblET1.AutoSize = true;
            this.lblET1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET1.Location = new System.Drawing.Point(344, 258);
            this.lblET1.Moveable = false;
            this.lblET1.Name = "lblET1";
            this.lblET1.NameOfControl = "ET1";
            this.lblET1.Size = new System.Drawing.Size(32, 14);
            this.lblET1.TabIndex = 90156;
            this.lblET1.Text = "ET1";
            this.lblET1.Visible = false;
            // 
            // frmFabricInward
            // 
            this.ClientSize = new System.Drawing.Size(1072, 547);
            this.Name = "frmFabricInward";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmFabricInward_FormClosed);
            this.Load += new System.EventHandler(this.frmFabricInward_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal CIS_CLibrary.CIS_TextLabel lblProcesser;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboProcesser;
        internal CIS_CLibrary.CIS_TextLabel lblProcesser1;
        internal CIS_CLibrary.CIS_TextLabel lblLrDt;
        internal CIS_CLibrary.CIS_Textbox txtLrNo;
        internal CIS_CLibrary.CIS_TextLabel lblLrNo;
        internal CIS_CLibrary.CIS_TextLabel lblLrNoColon;
        internal CIS_CLibrary.CIS_TextLabel lblLrDtColon;
        internal CIS_CLibrary.CIS_TextLabel lblTrnsprtColon;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboTransport;
        internal CIS_CLibrary.CIS_TextLabel lblTrnsprt;
        internal CIS_CLibrary.CIS_TextLabel lblDeprtmt;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboDepartment;
        internal CIS_CLibrary.CIS_TextLabel lblDeprtmtColon;
        internal CIS_CLibrary.CIS_TextLabel lblDescription;
        internal CIS_CLibrary.CIS_Textbox txtDescription;
        internal CIS_CLibrary.CIS_TextLabel lblDescriptionColon;
        internal CIS_CLibrary.CIS_TextLabel lblEntryNo;
        public CIS_CLibrary.CIS_Textbox txtCode;
        internal CIS_CLibrary.CIS_TextLabel lblEntryDtColon;
        internal CIS_CLibrary.CIS_TextLabel lblEntryDt;
        internal CIS_CLibrary.CIS_TextLabel lblEntryNoColon;
        internal CIS_CLibrary.CIS_Textbox txtEntryNo;
        internal CIS_CLibrary.CIS_TextLabel lblBrokerColon;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboBroker;
        internal CIS_CLibrary.CIS_TextLabel lblBroker;
        internal CIS_CLibrary.CIS_TextLabel lblRecvdColon;
        internal CIS_CLibrary.CIS_TextLabel lblRecvd;
        internal CIS_CLibrary.CIS_TextLabel lblSupplierColon;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboSupplier;
        internal CIS_CLibrary.CIS_TextLabel lblSupplier;
        internal CIS_CLibrary.CIS_TextLabel lblRefNoColon;
        internal CIS_CLibrary.CIS_Textbox txtRefNo;
        internal CIS_CLibrary.CIS_TextLabel lblRefNo;
        internal CIS_CLibrary.CIS_TextLabel label5;
        internal CIS_CLibrary.CIS_TextLabel label6;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboOrderType;
        internal CIS_CLibrary.CIS_Button btnSelectStock;
        internal CIS_CLibrary.CIS_Textbox dtEntryDate;
        internal CIS_CLibrary.CIS_Textbox dtChallanDate;
        public CIS_CLibrary.CIS_Textbox txtUniqueID;
        internal CIS_CLibrary.CIS_Textbox dtLrDate;
        private Panel panel1;
        private Crocus_CClib.DataGridViewX GrdMain;
        internal CIS_CLibrary.CIS_TextLabel lblED1Colon;
        internal CIS_CLibrary.CIS_TextLabel lblED1;
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
        internal CIS_CLibrary.CIS_Textbox dtEd1;
    }
}
