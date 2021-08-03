namespace CIS_Textil
{
    partial class frmTaxDetailMaster
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
            CIS_CLibrary.ToolTip.StringDataProvider stringDataProvider4 = new CIS_CLibrary.ToolTip.StringDataProvider();
            this.lblSurchargePerc = new System.Windows.Forms.Label();
            this.lblperc = new System.Windows.Forms.Label();
            this.txtSurcharge = new CIS_CLibrary.CIS_Textbox();
            this.lblSurchargeColun = new System.Windows.Forms.Label();
            this.lblSurcharge = new System.Windows.Forms.Label();
            this.txtTaxPerc = new CIS_CLibrary.CIS_Textbox();
            this.lblTaxColun = new System.Windows.Forms.Label();
            this.lblTax = new System.Windows.Forms.Label();
            this.cboTaxType = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblTaxTypeColun = new System.Windows.Forms.Label();
            this.lblTaxType = new System.Windows.Forms.Label();
            this.txtTaxDetail = new CIS_CLibrary.CIS_Textbox();
            this.lblTaxDetailsColun = new System.Windows.Forms.Label();
            this.lblTaxDetails = new System.Windows.Forms.Label();
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.ciS_ToolTip1 = new CIS_CLibrary.ToolTip.CIS_ToolTip();
            this.ChkActive = new CIS_CLibrary.CIS_CheckBox(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.ChkActive);
            this.pnlContent.Controls.Add(this.lblSurchargePerc);
            this.pnlContent.Controls.Add(this.lblperc);
            this.pnlContent.Controls.Add(this.txtSurcharge);
            this.pnlContent.Controls.Add(this.lblSurchargeColun);
            this.pnlContent.Controls.Add(this.lblSurcharge);
            this.pnlContent.Controls.Add(this.txtTaxPerc);
            this.pnlContent.Controls.Add(this.lblTaxColun);
            this.pnlContent.Controls.Add(this.lblTax);
            this.pnlContent.Controls.Add(this.cboTaxType);
            this.pnlContent.Controls.Add(this.lblTaxTypeColun);
            this.pnlContent.Controls.Add(this.lblTaxType);
            this.pnlContent.Controls.Add(this.txtTaxDetail);
            this.pnlContent.Controls.Add(this.lblTaxDetailsColun);
            this.pnlContent.Controls.Add(this.lblTaxDetails);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlContent.Size = new System.Drawing.Size(806, 496);
            this.ciS_ToolTip1.SetToolTip(this.pnlContent, "");
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblTaxDetails, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblTaxDetailsColun, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtTaxDetail, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblTaxType, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblTaxTypeColun, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboTaxType, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblTax, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblTaxColun, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtTaxPerc, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblSurcharge, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblSurchargeColun, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtSurcharge, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblperc, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblSurchargePerc, 0);
            this.pnlContent.Controls.SetChildIndex(this.ChkActive, 0);
            // 
            // lblHelpText
            // 
            this.ciS_ToolTip1.SetToolTip(this.lblHelpText, "");
            // 
            // lblFormName
            // 
            this.ciS_ToolTip1.SetToolTip(this.lblFormName, "");
            // 
            // lblUUser
            // 
            this.ciS_ToolTip1.SetToolTip(this.lblUUser, "");
            // 
            // lblCUser
            // 
            this.ciS_ToolTip1.SetToolTip(this.lblCUser, "");
            // 
            // lblSurchargePerc
            // 
            this.lblSurchargePerc.AutoSize = true;
            this.lblSurchargePerc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSurchargePerc.Location = new System.Drawing.Point(530, 69);
            this.lblSurchargePerc.Name = "lblSurchargePerc";
            this.lblSurchargePerc.Size = new System.Drawing.Size(21, 13);
            this.lblSurchargePerc.TabIndex = 37;
            this.lblSurchargePerc.Text = "%";
            this.ciS_ToolTip1.SetToolTip(this.lblSurchargePerc, "");
            // 
            // lblperc
            // 
            this.lblperc.AutoSize = true;
            this.lblperc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblperc.Location = new System.Drawing.Point(345, 68);
            this.lblperc.Name = "lblperc";
            this.lblperc.Size = new System.Drawing.Size(22, 14);
            this.lblperc.TabIndex = 36;
            this.lblperc.Text = "%";
            this.ciS_ToolTip1.SetToolTip(this.lblperc, "");
            // 
            // txtSurcharge
            // 
            this.txtSurcharge.AcceptsReturn = true;
            this.txtSurcharge.AutoFillDate = false;
            this.txtSurcharge.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtSurcharge.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtSurcharge.CheckForSymbol = null;
            this.txtSurcharge.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.NumericWithDecimal;
            this.txtSurcharge.DecimalPlace = 0;
            this.txtSurcharge.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.txtSurcharge.HelpText = "Enter Surcharge in Percent";
            this.txtSurcharge.HoldMyText = null;
            this.txtSurcharge.IsMandatory = true;
            this.txtSurcharge.IsSingleQuote = true;
            this.txtSurcharge.IsSysmbol = false;
            this.txtSurcharge.Location = new System.Drawing.Point(482, 66);
            this.txtSurcharge.Mask = null;
            this.txtSurcharge.MaxLength = 6;
            this.txtSurcharge.Moveable = false;
            this.txtSurcharge.Name = "txtSurcharge";
            this.txtSurcharge.NameOfControl = "Surcharge";
            this.txtSurcharge.Prefix = null;
            this.txtSurcharge.ShowBallonTip = false;
            this.txtSurcharge.ShowErrorIcon = false;
            this.txtSurcharge.ShowMessage = "Please Enter Ledger Group";
            this.txtSurcharge.Size = new System.Drawing.Size(48, 21);
            this.txtSurcharge.Suffix = null;
            this.txtSurcharge.TabIndex = 4;
            this.txtSurcharge.Text = "0";
            this.txtSurcharge.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ciS_ToolTip1.SetToolTip(this.txtSurcharge, "");
            // 
            // lblSurchargeColun
            // 
            this.lblSurchargeColun.AutoSize = true;
            this.lblSurchargeColun.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSurchargeColun.Location = new System.Drawing.Point(469, 70);
            this.lblSurchargeColun.Name = "lblSurchargeColun";
            this.lblSurchargeColun.Size = new System.Drawing.Size(11, 13);
            this.lblSurchargeColun.TabIndex = 34;
            this.lblSurchargeColun.Text = ":";
            this.ciS_ToolTip1.SetToolTip(this.lblSurchargeColun, "");
            // 
            // lblSurcharge
            // 
            this.lblSurcharge.AutoSize = true;
            this.lblSurcharge.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSurcharge.Location = new System.Drawing.Point(391, 68);
            this.lblSurcharge.Name = "lblSurcharge";
            this.lblSurcharge.Size = new System.Drawing.Size(75, 14);
            this.lblSurcharge.TabIndex = 35;
            this.lblSurcharge.Text = "Surcharge";
            this.ciS_ToolTip1.SetToolTip(this.lblSurcharge, "");
            // 
            // txtTaxPerc
            // 
            this.txtTaxPerc.AcceptsReturn = true;
            this.txtTaxPerc.AutoFillDate = false;
            this.txtTaxPerc.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtTaxPerc.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtTaxPerc.CheckForSymbol = null;
            this.txtTaxPerc.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.NumericWithDecimal;
            this.txtTaxPerc.DecimalPlace = 0;
            this.txtTaxPerc.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.txtTaxPerc.HelpText = "Enter Tax in Percent";
            this.txtTaxPerc.HoldMyText = null;
            this.txtTaxPerc.IsMandatory = true;
            this.txtTaxPerc.IsSingleQuote = true;
            this.txtTaxPerc.IsSysmbol = false;
            this.txtTaxPerc.Location = new System.Drawing.Point(297, 66);
            this.txtTaxPerc.Mask = null;
            this.txtTaxPerc.MaxLength = 6;
            this.txtTaxPerc.Moveable = false;
            this.txtTaxPerc.Name = "txtTaxPerc";
            this.txtTaxPerc.NameOfControl = "Tax ";
            this.txtTaxPerc.Prefix = null;
            this.txtTaxPerc.ShowBallonTip = false;
            this.txtTaxPerc.ShowErrorIcon = false;
            this.txtTaxPerc.ShowMessage = "Please Enter Ledger Group";
            this.txtTaxPerc.Size = new System.Drawing.Size(48, 21);
            this.txtTaxPerc.Suffix = null;
            this.txtTaxPerc.TabIndex = 3;
            this.txtTaxPerc.Text = "0";
            this.txtTaxPerc.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ciS_ToolTip1.SetToolTip(this.txtTaxPerc, "");
            // 
            // lblTaxColun
            // 
            this.lblTaxColun.AutoSize = true;
            this.lblTaxColun.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxColun.Location = new System.Drawing.Point(286, 68);
            this.lblTaxColun.Name = "lblTaxColun";
            this.lblTaxColun.Size = new System.Drawing.Size(12, 14);
            this.lblTaxColun.TabIndex = 32;
            this.lblTaxColun.Text = ":";
            this.ciS_ToolTip1.SetToolTip(this.lblTaxColun, "");
            // 
            // lblTax
            // 
            this.lblTax.AutoSize = true;
            this.lblTax.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTax.Location = new System.Drawing.Point(209, 68);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(31, 14);
            this.lblTax.TabIndex = 33;
            this.lblTax.Text = "Tax";
            this.ciS_ToolTip1.SetToolTip(this.lblTax, "");
            // 
            // cboTaxType
            // 
            this.cboTaxType.AutoComplete = false;
            this.cboTaxType.AutoDropdown = false;
            this.cboTaxType.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboTaxType.BackColorEven = System.Drawing.Color.White;
            this.cboTaxType.BackColorOdd = System.Drawing.Color.White;
            this.cboTaxType.ColumnNames = "";
            this.cboTaxType.ColumnWidthDefault = 175;
            this.cboTaxType.ColumnWidths = "";
            this.cboTaxType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboTaxType.Fill_ComboID = 0;
            this.cboTaxType.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.cboTaxType.FormattingEnabled = true;
            this.cboTaxType.GroupType = 0;
            this.cboTaxType.HelpText = "Select Tax Type";
            this.cboTaxType.IsMandatory = true;
            this.cboTaxType.LinkedColumnIndex = 0;
            this.cboTaxType.LinkedTextBox = null;
            this.cboTaxType.Location = new System.Drawing.Point(297, 40);
            this.cboTaxType.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboTaxType.Moveable = false;
            this.cboTaxType.Name = "cboTaxType";
            this.cboTaxType.NameOfControl = "Tax Type";
            this.cboTaxType.OpenForm = null;
            this.cboTaxType.ShowBallonTip = false;
            this.cboTaxType.Size = new System.Drawing.Size(254, 22);
            this.cboTaxType.TabIndex = 2;
            this.ciS_ToolTip1.SetToolTip(this.cboTaxType, "");
            // 
            // lblTaxTypeColun
            // 
            this.lblTaxTypeColun.AutoSize = true;
            this.lblTaxTypeColun.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxTypeColun.Location = new System.Drawing.Point(286, 42);
            this.lblTaxTypeColun.Name = "lblTaxTypeColun";
            this.lblTaxTypeColun.Size = new System.Drawing.Size(12, 14);
            this.lblTaxTypeColun.TabIndex = 31;
            this.lblTaxTypeColun.Text = ":";
            this.ciS_ToolTip1.SetToolTip(this.lblTaxTypeColun, "");
            // 
            // lblTaxType
            // 
            this.lblTaxType.AutoSize = true;
            this.lblTaxType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxType.Location = new System.Drawing.Point(209, 42);
            this.lblTaxType.Name = "lblTaxType";
            this.lblTaxType.Size = new System.Drawing.Size(67, 14);
            this.lblTaxType.TabIndex = 30;
            this.lblTaxType.Text = "Tax Type";
            this.ciS_ToolTip1.SetToolTip(this.lblTaxType, "");
            // 
            // txtTaxDetail
            // 
            this.txtTaxDetail.AcceptsReturn = true;
            this.txtTaxDetail.AutoFillDate = false;
            this.txtTaxDetail.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtTaxDetail.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtTaxDetail.CheckForSymbol = null;
            this.txtTaxDetail.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtTaxDetail.DecimalPlace = 0;
            this.txtTaxDetail.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.txtTaxDetail.HelpText = "Enter Tax Detail";
            this.txtTaxDetail.HoldMyText = null;
            this.txtTaxDetail.IsMandatory = true;
            this.txtTaxDetail.IsSingleQuote = true;
            this.txtTaxDetail.IsSysmbol = false;
            this.txtTaxDetail.Location = new System.Drawing.Point(297, 15);
            this.txtTaxDetail.Mask = null;
            this.txtTaxDetail.MaxLength = 50;
            this.txtTaxDetail.Moveable = false;
            this.txtTaxDetail.Name = "txtTaxDetail";
            this.txtTaxDetail.NameOfControl = "Tax Detail";
            this.txtTaxDetail.Prefix = null;
            this.txtTaxDetail.ShowBallonTip = false;
            this.txtTaxDetail.ShowErrorIcon = false;
            this.txtTaxDetail.ShowMessage = "Please Enter Ledger Group";
            this.txtTaxDetail.Size = new System.Drawing.Size(254, 21);
            this.txtTaxDetail.Suffix = null;
            this.txtTaxDetail.TabIndex = 1;
            this.ciS_ToolTip1.SetToolTip(this.txtTaxDetail, "");
            // 
            // lblTaxDetailsColun
            // 
            this.lblTaxDetailsColun.AutoSize = true;
            this.lblTaxDetailsColun.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxDetailsColun.Location = new System.Drawing.Point(286, 17);
            this.lblTaxDetailsColun.Name = "lblTaxDetailsColun";
            this.lblTaxDetailsColun.Size = new System.Drawing.Size(12, 14);
            this.lblTaxDetailsColun.TabIndex = 28;
            this.lblTaxDetailsColun.Text = ":";
            this.ciS_ToolTip1.SetToolTip(this.lblTaxDetailsColun, "");
            // 
            // lblTaxDetails
            // 
            this.lblTaxDetails.AutoSize = true;
            this.lblTaxDetails.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxDetails.Location = new System.Drawing.Point(209, 17);
            this.lblTaxDetails.Name = "lblTaxDetails";
            this.lblTaxDetails.Size = new System.Drawing.Size(74, 14);
            this.lblTaxDetails.TabIndex = 29;
            this.lblTaxDetails.Text = "Tax Detail";
            this.ciS_ToolTip1.SetToolTip(this.lblTaxDetails, "");
            // 
            // txtCode
            // 
            this.txtCode.AutoFillDate = false;
            this.txtCode.BackColor = System.Drawing.Color.White;
            this.txtCode.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtCode.CheckForSymbol = null;
            this.txtCode.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtCode.DecimalPlace = 0;
            this.txtCode.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.HelpText = "";
            this.txtCode.HoldMyText = null;
            this.txtCode.IsMandatory = false;
            this.txtCode.IsSingleQuote = true;
            this.txtCode.IsSysmbol = false;
            this.txtCode.Location = new System.Drawing.Point(3, 3);
            this.txtCode.Mask = null;
            this.txtCode.Moveable = false;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(33, 20);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 27;
            this.txtCode.TabStop = false;
            this.ciS_ToolTip1.SetToolTip(this.txtCode, "");
            this.txtCode.Visible = false;
            // 
            // ciS_ToolTip1
            // 
            this.ciS_ToolTip1.DataProvider = stringDataProvider4;
            this.ciS_ToolTip1.LoadText = "";
            this.ciS_ToolTip1.ShowToolTip = true;
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
            this.ChkActive.Location = new System.Drawing.Point(297, 93);
            this.ChkActive.Moveable = false;
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.NameOfControl = null;
            this.ChkActive.Size = new System.Drawing.Size(110, 18);
            this.ChkActive.TabIndex = 5;
            this.ChkActive.Text = "Active Status";
            this.ciS_ToolTip1.SetToolTip(this.ChkActive, "");
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // frmTaxDetailMaster
            // 
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.ClientSize = new System.Drawing.Size(806, 547);
            this.DoubleBuffered = false;
            this.KeyPreview = true;
            this.Name = "frmTaxDetailMaster";
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label lblSurchargePerc;
        internal System.Windows.Forms.Label lblperc;
        internal CIS_CLibrary.CIS_Textbox txtSurcharge;
        internal System.Windows.Forms.Label lblSurchargeColun;
        internal System.Windows.Forms.Label lblSurcharge;
        internal CIS_CLibrary.CIS_Textbox txtTaxPerc;
        internal System.Windows.Forms.Label lblTaxColun;
        internal System.Windows.Forms.Label lblTax;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboTaxType;
        internal System.Windows.Forms.Label lblTaxTypeColun;
        internal System.Windows.Forms.Label lblTaxType;
        internal CIS_CLibrary.CIS_Textbox txtTaxDetail;
        internal System.Windows.Forms.Label lblTaxDetailsColun;
        internal System.Windows.Forms.Label lblTaxDetails;
        public CIS_CLibrary.CIS_Textbox txtCode;
        private CIS_CLibrary.ToolTip.CIS_ToolTip ciS_ToolTip1;
        internal CIS_CLibrary.CIS_CheckBox ChkActive;
    }
}
