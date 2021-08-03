using System.Windows.Forms;
namespace CIS_Textil
{
    partial class frmFabricPhysicalStock
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
            this.dtEntryDate = new CIS_CLibrary.CIS_Textbox();
            this.txtEntryNo = new CIS_CLibrary.CIS_Textbox();
            this.lblDepartment = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblEntryDtColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblDepartmentColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblEntryDt = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.cboDepartment = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblEntryNoColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblEntryNo = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.dtED1 = new CIS_CLibrary.CIS_Textbox();
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
            this.GrdMain = new Crocus_CClib.DataGridViewX();
            this.lblDescription = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtDesc = new CIS_CLibrary.CIS_Textbox();
            this.lblDescriptionColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.txtUniqueID = new CIS_CLibrary.CIS_Textbox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.pnlDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.txtUniqueID);
            this.pnlContent.Controls.Add(this.btnSelect);
            this.pnlContent.Controls.Add(this.dtEntryDate);
            this.pnlContent.Controls.Add(this.txtEntryNo);
            this.pnlContent.Controls.Add(this.lblDepartment);
            this.pnlContent.Controls.Add(this.lblEntryDtColon);
            this.pnlContent.Controls.Add(this.lblDepartmentColon);
            this.pnlContent.Controls.Add(this.lblEntryDt);
            this.pnlContent.Controls.Add(this.cboDepartment);
            this.pnlContent.Controls.Add(this.lblEntryNoColon);
            this.pnlContent.Controls.Add(this.lblEntryNo);
            this.pnlContent.Controls.Add(this.pnlDetail);
            this.pnlContent.Controls.Add(this.lblDescription);
            this.pnlContent.Controls.Add(this.txtDesc);
            this.pnlContent.Controls.Add(this.lblDescriptionColon);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblDescriptionColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtDesc, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblDescription, 0);
            this.pnlContent.Controls.SetChildIndex(this.pnlDetail, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryNoColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboDepartment, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryDt, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblDepartmentColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryDtColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblDepartment, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtEntryNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.dtEntryDate, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnSelect, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtUniqueID, 0);
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
            this.dtEntryDate.Location = new System.Drawing.Point(591, 13);
            this.dtEntryDate.Mask = "##/##/####";
            this.dtEntryDate.MaxLength = 10;
            this.dtEntryDate.Moveable = false;
            this.dtEntryDate.Name = "dtEntryDate";
            this.dtEntryDate.NameOfControl = "Entry Date";
            this.dtEntryDate.Prefix = null;
            this.dtEntryDate.ShowBallonTip = false;
            this.dtEntryDate.ShowErrorIcon = false;
            this.dtEntryDate.ShowMessage = null;
            this.dtEntryDate.Size = new System.Drawing.Size(99, 22);
            this.dtEntryDate.Suffix = null;
            this.dtEntryDate.TabIndex = 2;
            this.dtEntryDate.Text = "__/__/____";
            // 
            // txtEntryNo
            // 
            this.txtEntryNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtEntryNo.AutoFillDate = false;
            this.txtEntryNo.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtEntryNo.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtEntryNo.CheckForSymbol = null;
            this.txtEntryNo.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.NumericWithOutDecimal;
            this.txtEntryNo.DecimalPlace = 0;
            this.txtEntryNo.Enabled = false;
            this.txtEntryNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtEntryNo.HelpText = "";
            this.txtEntryNo.HoldMyText = null;
            this.txtEntryNo.IsMandatory = true;
            this.txtEntryNo.IsSingleQuote = true;
            this.txtEntryNo.IsSysmbol = false;
            this.txtEntryNo.Location = new System.Drawing.Point(302, 13);
            this.txtEntryNo.Mask = null;
            this.txtEntryNo.MaxLength = 20;
            this.txtEntryNo.Moveable = false;
            this.txtEntryNo.Name = "txtEntryNo";
            this.txtEntryNo.NameOfControl = "Entry No";
            this.txtEntryNo.Prefix = null;
            this.txtEntryNo.ShowBallonTip = false;
            this.txtEntryNo.ShowErrorIcon = false;
            this.txtEntryNo.ShowMessage = null;
            this.txtEntryNo.Size = new System.Drawing.Size(97, 22);
            this.txtEntryNo.Suffix = null;
            this.txtEntryNo.TabIndex = 1;
            this.txtEntryNo.Text = "0";
            this.txtEntryNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDepartment
            // 
            this.lblDepartment.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDepartment.AutoSize = true;
            this.lblDepartment.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartment.Location = new System.Drawing.Point(203, 43);
            this.lblDepartment.Moveable = false;
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.NameOfControl = null;
            this.lblDepartment.Size = new System.Drawing.Size(85, 14);
            this.lblDepartment.TabIndex = 883;
            this.lblDepartment.Text = "Department";
            // 
            // lblEntryDtColon
            // 
            this.lblEntryDtColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEntryDtColon.AutoSize = true;
            this.lblEntryDtColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryDtColon.Location = new System.Drawing.Point(578, 16);
            this.lblEntryDtColon.Moveable = false;
            this.lblEntryDtColon.Name = "lblEntryDtColon";
            this.lblEntryDtColon.NameOfControl = null;
            this.lblEntryDtColon.Size = new System.Drawing.Size(12, 14);
            this.lblEntryDtColon.TabIndex = 888;
            this.lblEntryDtColon.Text = ":";
            // 
            // lblDepartmentColon
            // 
            this.lblDepartmentColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDepartmentColon.AutoSize = true;
            this.lblDepartmentColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartmentColon.Location = new System.Drawing.Point(290, 43);
            this.lblDepartmentColon.Moveable = false;
            this.lblDepartmentColon.Name = "lblDepartmentColon";
            this.lblDepartmentColon.NameOfControl = null;
            this.lblDepartmentColon.Size = new System.Drawing.Size(12, 14);
            this.lblDepartmentColon.TabIndex = 884;
            this.lblDepartmentColon.Text = ":";
            // 
            // lblEntryDt
            // 
            this.lblEntryDt.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEntryDt.AutoSize = true;
            this.lblEntryDt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryDt.Location = new System.Drawing.Point(493, 16);
            this.lblEntryDt.Moveable = false;
            this.lblEntryDt.Name = "lblEntryDt";
            this.lblEntryDt.NameOfControl = null;
            this.lblEntryDt.Size = new System.Drawing.Size(77, 14);
            this.lblEntryDt.TabIndex = 887;
            this.lblEntryDt.Text = "Entry Date";
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
            this.cboDepartment.ColumnWidthDefault = 75;
            this.cboDepartment.ColumnWidths = "";
            this.cboDepartment.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboDepartment.Fill_ComboID = 0;
            this.cboDepartment.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.cboDepartment.FormattingEnabled = true;
            this.cboDepartment.GroupType = 0;
            this.cboDepartment.HelpText = "Select Department";
            this.cboDepartment.IsMandatory = true;
            this.cboDepartment.LinkedColumnIndex = 0;
            this.cboDepartment.LinkedTextBox = null;
            this.cboDepartment.Location = new System.Drawing.Point(302, 39);
            this.cboDepartment.Moveable = false;
            this.cboDepartment.Name = "cboDepartment";
            this.cboDepartment.NameOfControl = "Department";
            this.cboDepartment.OpenForm = null;
            this.cboDepartment.ShowBallonTip = false;
            this.cboDepartment.Size = new System.Drawing.Size(388, 23);
            this.cboDepartment.TabIndex = 3;
            // 
            // lblEntryNoColon
            // 
            this.lblEntryNoColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEntryNoColon.AutoSize = true;
            this.lblEntryNoColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryNoColon.Location = new System.Drawing.Point(290, 16);
            this.lblEntryNoColon.Moveable = false;
            this.lblEntryNoColon.Name = "lblEntryNoColon";
            this.lblEntryNoColon.NameOfControl = null;
            this.lblEntryNoColon.Size = new System.Drawing.Size(12, 14);
            this.lblEntryNoColon.TabIndex = 886;
            this.lblEntryNoColon.Text = ":";
            // 
            // lblEntryNo
            // 
            this.lblEntryNo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEntryNo.AutoSize = true;
            this.lblEntryNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryNo.Location = new System.Drawing.Point(203, 16);
            this.lblEntryNo.Moveable = false;
            this.lblEntryNo.Name = "lblEntryNo";
            this.lblEntryNo.NameOfControl = null;
            this.lblEntryNo.Size = new System.Drawing.Size(64, 14);
            this.lblEntryNo.TabIndex = 885;
            this.lblEntryNo.Text = "Entry No";
            // 
            // pnlDetail
            // 
            this.pnlDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDetail.BackColor = System.Drawing.Color.LightSkyBlue;
            this.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDetail.Controls.Add(this.dtED1);
            this.pnlDetail.Controls.Add(this.lblED1Colon);
            this.pnlDetail.Controls.Add(this.lblED1);
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
            this.pnlDetail.Location = new System.Drawing.Point(53, 68);
            this.pnlDetail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(959, 385);
            this.pnlDetail.TabIndex = 5;
            // 
            // dtED1
            // 
            this.dtED1.AutoFillDate = true;
            this.dtED1.BackColor = System.Drawing.Color.PapayaWhip;
            this.dtED1.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.dtED1.CheckForSymbol = null;
            this.dtED1.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.ApplyDate;
            this.dtED1.DecimalPlace = 0;
            this.dtED1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.dtED1.HelpText = "Enter ED1";
            this.dtED1.HoldMyText = null;
            this.dtED1.IsMandatory = true;
            this.dtED1.IsSingleQuote = true;
            this.dtED1.IsSysmbol = false;
            this.dtED1.Location = new System.Drawing.Point(413, 86);
            this.dtED1.Mask = "##/##/####";
            this.dtED1.MaxLength = 10;
            this.dtED1.Moveable = false;
            this.dtED1.Name = "dtED1";
            this.dtED1.NameOfControl = "ED1";
            this.dtED1.Prefix = null;
            this.dtED1.ShowBallonTip = false;
            this.dtED1.ShowErrorIcon = false;
            this.dtED1.ShowMessage = null;
            this.dtED1.Size = new System.Drawing.Size(94, 22);
            this.dtED1.Suffix = null;
            this.dtED1.TabIndex = 90208;
            this.dtED1.Text = "__/__/____";
            this.dtED1.Visible = false;
            // 
            // lblED1Colon
            // 
            this.lblED1Colon.AutoSize = true;
            this.lblED1Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblED1Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblED1Colon.Location = new System.Drawing.Point(399, 89);
            this.lblED1Colon.Moveable = false;
            this.lblED1Colon.Name = "lblED1Colon";
            this.lblED1Colon.NameOfControl = "ED1";
            this.lblED1Colon.Size = new System.Drawing.Size(12, 14);
            this.lblED1Colon.TabIndex = 90207;
            this.lblED1Colon.Text = ":";
            this.lblED1Colon.Visible = false;
            // 
            // lblED1
            // 
            this.lblED1.AutoSize = true;
            this.lblED1.BackColor = System.Drawing.Color.Transparent;
            this.lblED1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblED1.Location = new System.Drawing.Point(286, 87);
            this.lblED1.Moveable = false;
            this.lblED1.Name = "lblED1";
            this.lblED1.NameOfControl = "ED1";
            this.lblED1.Size = new System.Drawing.Size(34, 14);
            this.lblED1.TabIndex = 90206;
            this.lblED1.Text = "ED1";
            this.lblED1.Visible = false;
            // 
            // lblEI1
            // 
            this.lblEI1.AutoSize = true;
            this.lblEI1.BackColor = System.Drawing.Color.Transparent;
            this.lblEI1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI1.Location = new System.Drawing.Point(286, 37);
            this.lblEI1.Moveable = false;
            this.lblEI1.Name = "lblEI1";
            this.lblEI1.NameOfControl = "EI1";
            this.lblEI1.Size = new System.Drawing.Size(30, 14);
            this.lblEI1.TabIndex = 90204;
            this.lblEI1.Text = "EI1";
            this.lblEI1.Visible = false;
            // 
            // lblEI1Colon
            // 
            this.lblEI1Colon.AutoSize = true;
            this.lblEI1Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblEI1Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI1Colon.Location = new System.Drawing.Point(399, 37);
            this.lblEI1Colon.Moveable = false;
            this.lblEI1Colon.Name = "lblEI1Colon";
            this.lblEI1Colon.NameOfControl = "EI1";
            this.lblEI1Colon.Size = new System.Drawing.Size(12, 14);
            this.lblEI1Colon.TabIndex = 90205;
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
            this.cboEI1.HelpText = "Select EI1";
            this.cboEI1.IsMandatory = true;
            this.cboEI1.LinkedColumnIndex = 0;
            this.cboEI1.LinkedTextBox = null;
            this.cboEI1.Location = new System.Drawing.Point(413, 33);
            this.cboEI1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboEI1.Moveable = false;
            this.cboEI1.Name = "cboEI1";
            this.cboEI1.NameOfControl = "EI1";
            this.cboEI1.OpenForm = null;
            this.cboEI1.ShowBallonTip = false;
            this.cboEI1.Size = new System.Drawing.Size(231, 23);
            this.cboEI1.TabIndex = 90191;
            this.cboEI1.Visible = false;
            // 
            // lblEI2
            // 
            this.lblEI2.AutoSize = true;
            this.lblEI2.BackColor = System.Drawing.Color.Transparent;
            this.lblEI2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI2.Location = new System.Drawing.Point(286, 61);
            this.lblEI2.Moveable = false;
            this.lblEI2.Name = "lblEI2";
            this.lblEI2.NameOfControl = "EI2";
            this.lblEI2.Size = new System.Drawing.Size(30, 14);
            this.lblEI2.TabIndex = 90202;
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
            this.cboEI2.HelpText = "Select EI2";
            this.cboEI2.IsMandatory = true;
            this.cboEI2.LinkedColumnIndex = 0;
            this.cboEI2.LinkedTextBox = null;
            this.cboEI2.Location = new System.Drawing.Point(413, 59);
            this.cboEI2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboEI2.Moveable = false;
            this.cboEI2.Name = "cboEI2";
            this.cboEI2.NameOfControl = "EI2";
            this.cboEI2.OpenForm = null;
            this.cboEI2.ShowBallonTip = false;
            this.cboEI2.Size = new System.Drawing.Size(231, 23);
            this.cboEI2.TabIndex = 90192;
            this.cboEI2.Visible = false;
            // 
            // lblEI2Colon
            // 
            this.lblEI2Colon.AutoSize = true;
            this.lblEI2Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblEI2Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI2Colon.Location = new System.Drawing.Point(399, 61);
            this.lblEI2Colon.Moveable = false;
            this.lblEI2Colon.Name = "lblEI2Colon";
            this.lblEI2Colon.NameOfControl = null;
            this.lblEI2Colon.Size = new System.Drawing.Size(12, 14);
            this.lblEI2Colon.TabIndex = 90203;
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
            this.txtET3.HelpText = "Enter ET3";
            this.txtET3.HoldMyText = null;
            this.txtET3.IsMandatory = false;
            this.txtET3.IsSingleQuote = true;
            this.txtET3.IsSysmbol = false;
            this.txtET3.Location = new System.Drawing.Point(413, 162);
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
            this.txtET3.TabIndex = 90195;
            this.txtET3.Visible = false;
            // 
            // lblET3Colon
            // 
            this.lblET3Colon.AutoSize = true;
            this.lblET3Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblET3Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET3Colon.Location = new System.Drawing.Point(399, 165);
            this.lblET3Colon.Moveable = false;
            this.lblET3Colon.Name = "lblET3Colon";
            this.lblET3Colon.NameOfControl = null;
            this.lblET3Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET3Colon.TabIndex = 90201;
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
            this.txtET2.HelpText = "Enter ET2";
            this.txtET2.HoldMyText = null;
            this.txtET2.IsMandatory = false;
            this.txtET2.IsSingleQuote = true;
            this.txtET2.IsSysmbol = false;
            this.txtET2.Location = new System.Drawing.Point(413, 136);
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
            this.txtET2.TabIndex = 90194;
            this.txtET2.Visible = false;
            // 
            // lblET2Colon
            // 
            this.lblET2Colon.AutoSize = true;
            this.lblET2Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblET2Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET2Colon.Location = new System.Drawing.Point(399, 141);
            this.lblET2Colon.Moveable = false;
            this.lblET2Colon.Name = "lblET2Colon";
            this.lblET2Colon.NameOfControl = "ET2";
            this.lblET2Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET2Colon.TabIndex = 90199;
            this.lblET2Colon.Text = ":";
            this.lblET2Colon.Visible = false;
            // 
            // lblET3
            // 
            this.lblET3.AutoSize = true;
            this.lblET3.BackColor = System.Drawing.Color.Transparent;
            this.lblET3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET3.Location = new System.Drawing.Point(286, 164);
            this.lblET3.Moveable = false;
            this.lblET3.Name = "lblET3";
            this.lblET3.NameOfControl = "ET3";
            this.lblET3.Size = new System.Drawing.Size(32, 14);
            this.lblET3.TabIndex = 90200;
            this.lblET3.Text = "ET3";
            this.lblET3.Visible = false;
            // 
            // lblET2
            // 
            this.lblET2.AutoSize = true;
            this.lblET2.BackColor = System.Drawing.Color.Transparent;
            this.lblET2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET2.Location = new System.Drawing.Point(286, 139);
            this.lblET2.Moveable = false;
            this.lblET2.Name = "lblET2";
            this.lblET2.NameOfControl = "ET2";
            this.lblET2.Size = new System.Drawing.Size(32, 14);
            this.lblET2.TabIndex = 90198;
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
            this.txtET1.HelpText = "Enter ET1";
            this.txtET1.HoldMyText = null;
            this.txtET1.IsMandatory = false;
            this.txtET1.IsSingleQuote = true;
            this.txtET1.IsSysmbol = false;
            this.txtET1.Location = new System.Drawing.Point(413, 111);
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
            this.txtET1.TabIndex = 90193;
            this.txtET1.Visible = false;
            // 
            // lblET1Colon
            // 
            this.lblET1Colon.AutoSize = true;
            this.lblET1Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblET1Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET1Colon.Location = new System.Drawing.Point(399, 113);
            this.lblET1Colon.Moveable = false;
            this.lblET1Colon.Name = "lblET1Colon";
            this.lblET1Colon.NameOfControl = "ET1";
            this.lblET1Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET1Colon.TabIndex = 90197;
            this.lblET1Colon.Text = ":";
            this.lblET1Colon.Visible = false;
            // 
            // lblET1
            // 
            this.lblET1.AutoSize = true;
            this.lblET1.BackColor = System.Drawing.Color.Transparent;
            this.lblET1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET1.Location = new System.Drawing.Point(286, 113);
            this.lblET1.Moveable = false;
            this.lblET1.Name = "lblET1";
            this.lblET1.NameOfControl = "ET1";
            this.lblET1.Size = new System.Drawing.Size(32, 14);
            this.lblET1.TabIndex = 90196;
            this.lblET1.Text = "ET1";
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
            this.GrdMain.Size = new System.Drawing.Size(957, 383);
            this.GrdMain.TabIndex = 90190;
            this.GrdMain.YearID = 0;
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(53, 462);
            this.lblDescription.Moveable = false;
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.NameOfControl = null;
            this.lblDescription.Size = new System.Drawing.Size(82, 14);
            this.lblDescription.TabIndex = 880;
            this.lblDescription.Text = "Description";
            // 
            // txtDesc
            // 
            this.txtDesc.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtDesc.AutoFillDate = false;
            this.txtDesc.BackColor = System.Drawing.Color.White;
            this.txtDesc.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtDesc.CheckForSymbol = null;
            this.txtDesc.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtDesc.DecimalPlace = 0;
            this.txtDesc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtDesc.HelpText = "Enetr Description";
            this.txtDesc.HoldMyText = null;
            this.txtDesc.IsMandatory = false;
            this.txtDesc.IsSingleQuote = true;
            this.txtDesc.IsSysmbol = false;
            this.txtDesc.Location = new System.Drawing.Point(152, 459);
            this.txtDesc.Mask = null;
            this.txtDesc.MaxLength = 1000;
            this.txtDesc.Moveable = false;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.NameOfControl = "Description";
            this.txtDesc.Prefix = null;
            this.txtDesc.ShowBallonTip = false;
            this.txtDesc.ShowErrorIcon = false;
            this.txtDesc.ShowMessage = null;
            this.txtDesc.Size = new System.Drawing.Size(860, 22);
            this.txtDesc.Suffix = null;
            this.txtDesc.TabIndex = 6;
            // 
            // lblDescriptionColon
            // 
            this.lblDescriptionColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDescriptionColon.AutoSize = true;
            this.lblDescriptionColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescriptionColon.Location = new System.Drawing.Point(139, 460);
            this.lblDescriptionColon.Moveable = false;
            this.lblDescriptionColon.Name = "lblDescriptionColon";
            this.lblDescriptionColon.NameOfControl = null;
            this.lblDescriptionColon.Size = new System.Drawing.Size(12, 14);
            this.lblDescriptionColon.TabIndex = 881;
            this.lblDescriptionColon.Text = ":";
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
            this.txtCode.Size = new System.Drawing.Size(25, 22);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 0;
            this.txtCode.Visible = false;
            // 
            // txtUniqueID
            // 
            this.txtUniqueID.AutoFillDate = false;
            this.txtUniqueID.BackColor = System.Drawing.Color.White;
            this.txtUniqueID.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
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
            this.txtUniqueID.Location = new System.Drawing.Point(6, 29);
            this.txtUniqueID.Mask = null;
            this.txtUniqueID.Moveable = false;
            this.txtUniqueID.Name = "txtUniqueID";
            this.txtUniqueID.NameOfControl = null;
            this.txtUniqueID.Prefix = null;
            this.txtUniqueID.ShowBallonTip = false;
            this.txtUniqueID.ShowErrorIcon = false;
            this.txtUniqueID.ShowMessage = null;
            this.txtUniqueID.Size = new System.Drawing.Size(25, 22);
            this.txtUniqueID.Suffix = null;
            this.txtUniqueID.TabIndex = 895;
            this.txtUniqueID.Visible = false;
            // 
            // btnSelect
            // 
            this.btnSelect.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSelect.BackColor = System.Drawing.Color.Teal;
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelect.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(707, 13);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(135, 49);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "Se&lect";
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // frmFabricPhysicalStock
            // 
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.ClientSize = new System.Drawing.Size(1072, 547);
            this.DoubleBuffered = false;
            this.Name = "frmFabricPhysicalStock";
            this.Load += new System.EventHandler(this.frmFabricPhysicalStock_Load);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlDetail.ResumeLayout(false);
            this.pnlDetail.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal CIS_CLibrary.CIS_Textbox dtEntryDate;
        internal CIS_CLibrary.CIS_Textbox txtEntryNo;
        internal CIS_CLibrary.CIS_TextLabel lblDepartment;
        internal CIS_CLibrary.CIS_TextLabel lblEntryDtColon;
        internal CIS_CLibrary.CIS_TextLabel lblDepartmentColon;
        internal CIS_CLibrary.CIS_TextLabel lblEntryDt;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboDepartment;
        internal CIS_CLibrary.CIS_TextLabel lblEntryNoColon;
        internal CIS_CLibrary.CIS_TextLabel lblEntryNo;
        internal System.Windows.Forms.Panel pnlDetail;
        internal CIS_CLibrary.CIS_TextLabel lblDescription;
        internal CIS_CLibrary.CIS_Textbox txtDesc;
        internal CIS_CLibrary.CIS_TextLabel lblDescriptionColon;
        public CIS_CLibrary.CIS_Textbox txtCode;
        public CIS_CLibrary.CIS_Textbox txtUniqueID;
        internal Button btnSelect;
        internal CIS_CLibrary.CIS_Textbox dtED1;
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
        private Crocus_CClib.DataGridViewX GrdMain;
    }
}
