namespace CIS_Textil
{
    partial class frmFabricRateList
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
            this.GrpFabricType = new System.Windows.Forms.GroupBox();
            this.rdoDesign = new CIS_CLibrary.CIS_RadioButton();
            this.rdoQuality = new CIS_CLibrary.CIS_RadioButton();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.btnSelect = new CIS_CLibrary.CIS_Button();
            this.dtEntryDate = new CIS_CLibrary.CIS_Textbox();
            this.lblEntryDate = new CIS_CLibrary.CIS_TextLabel();
            this.lblEntryDateColon = new CIS_CLibrary.CIS_TextLabel();
            this.lblEntryNoColon = new CIS_CLibrary.CIS_TextLabel();
            this.txtEntryNo = new CIS_CLibrary.CIS_Textbox();
            this.lblEntryNo = new CIS_CLibrary.CIS_TextLabel();
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.GrpFabricType.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.Add(this.dtEntryDate);
            this.pnlContent.Controls.Add(this.lblEntryDate);
            this.pnlContent.Controls.Add(this.btnSelect);
            this.pnlContent.Controls.Add(this.lblEntryDateColon);
            this.pnlContent.Controls.Add(this.pnlDetail);
            this.pnlContent.Controls.Add(this.lblEntryNoColon);
            this.pnlContent.Controls.Add(this.GrpFabricType);
            this.pnlContent.Controls.Add(this.txtEntryNo);
            this.pnlContent.Controls.Add(this.lblEntryNo);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtEntryNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.GrpFabricType, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryNoColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.pnlDetail, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryDateColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnSelect, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryDate, 0);
            this.pnlContent.Controls.SetChildIndex(this.dtEntryDate, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            // 
            // GrpFabricType
            // 
            this.GrpFabricType.Controls.Add(this.rdoDesign);
            this.GrpFabricType.Controls.Add(this.rdoQuality);
            this.GrpFabricType.Location = new System.Drawing.Point(598, 12);
            this.GrpFabricType.Name = "GrpFabricType";
            this.GrpFabricType.Size = new System.Drawing.Size(274, 40);
            this.GrpFabricType.TabIndex = 1;
            this.GrpFabricType.TabStop = false;
            this.GrpFabricType.Text = " Select";
            // 
            // rdoDesign
            // 
            this.rdoDesign.AutoSize = true;
            this.rdoDesign.HelpText = null;
            this.rdoDesign.Location = new System.Drawing.Point(152, 16);
            this.rdoDesign.Name = "rdoDesign";
            this.rdoDesign.Size = new System.Drawing.Size(58, 17);
            this.rdoDesign.TabIndex = 3;
            this.rdoDesign.TabStop = true;
            this.rdoDesign.Text = "Design";
            this.rdoDesign.UseVisualStyleBackColor = true;
            this.rdoDesign.CheckedChanged += new System.EventHandler(this.rdoDesign_CheckedChanged);
            // 
            // rdoQuality
            // 
            this.rdoQuality.AutoSize = true;
            this.rdoQuality.HelpText = null;
            this.rdoQuality.Location = new System.Drawing.Point(44, 16);
            this.rdoQuality.Name = "rdoQuality";
            this.rdoQuality.Size = new System.Drawing.Size(57, 17);
            this.rdoQuality.TabIndex = 2;
            this.rdoQuality.TabStop = true;
            this.rdoQuality.Text = "Quality";
            this.rdoQuality.UseVisualStyleBackColor = true;
            this.rdoQuality.CheckedChanged += new System.EventHandler(this.rdbQuality_CheckedChanged);
            // 
            // pnlDetail
            // 
            this.pnlDetail.BackColor = System.Drawing.Color.LightBlue;
            this.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDetail.Location = new System.Drawing.Point(96, 97);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(776, 383);
            this.pnlDetail.TabIndex = 8;
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.btnSelect.Location = new System.Drawing.Point(715, 67);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(157, 22);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.TabStop = false;
            this.btnSelect.Text = "Add All";
            this.btnSelect.UseVisualStyleBackColor = false;
            this.btnSelect.Click += new System.EventHandler(this.btnAddRateList_Click);
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
            this.dtEntryDate.Location = new System.Drawing.Point(389, 29);
            this.dtEntryDate.Mask = "__/__/____";
            this.dtEntryDate.MaxLength = 10;
            this.dtEntryDate.Name = "dtEntryDate";
            this.dtEntryDate.NameOfControl = "Entry date";
            this.dtEntryDate.Prefix = null;
            this.dtEntryDate.ShowBallonTip = false;
            this.dtEntryDate.ShowErrorIcon = false;
            this.dtEntryDate.ShowMessage = null;
            this.dtEntryDate.Size = new System.Drawing.Size(91, 21);
            this.dtEntryDate.Suffix = null;
            this.dtEntryDate.TabIndex = 1;
            this.dtEntryDate.Text = "__/__/____";
            // 
            // lblEntryDate
            // 
            this.lblEntryDate.AutoSize = true;
            this.lblEntryDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryDate.Location = new System.Drawing.Point(300, 31);
            this.lblEntryDate.Name = "lblEntryDate";
            this.lblEntryDate.Size = new System.Drawing.Size(77, 14);
            this.lblEntryDate.TabIndex = 253;
            this.lblEntryDate.Text = "Entry Date";
            // 
            // lblEntryDateColon
            // 
            this.lblEntryDateColon.AutoSize = true;
            this.lblEntryDateColon.Font = new System.Drawing.Font("Arial", 9.884062F, System.Drawing.FontStyle.Bold);
            this.lblEntryDateColon.Location = new System.Drawing.Point(376, 30);
            this.lblEntryDateColon.Name = "lblEntryDateColon";
            this.lblEntryDateColon.Size = new System.Drawing.Size(12, 16);
            this.lblEntryDateColon.TabIndex = 254;
            this.lblEntryDateColon.Text = ":";
            // 
            // lblEntryNoColon
            // 
            this.lblEntryNoColon.AutoSize = true;
            this.lblEntryNoColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryNoColon.Location = new System.Drawing.Point(187, 30);
            this.lblEntryNoColon.Name = "lblEntryNoColon";
            this.lblEntryNoColon.Size = new System.Drawing.Size(12, 14);
            this.lblEntryNoColon.TabIndex = 256;
            this.lblEntryNoColon.Text = ":";
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
            this.txtEntryNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtEntryNo.HelpText = "Entry No";
            this.txtEntryNo.HoldMyText = null;
            this.txtEntryNo.IsMandatory = true;
            this.txtEntryNo.IsSingleQuote = true;
            this.txtEntryNo.IsSysmbol = false;
            this.txtEntryNo.Location = new System.Drawing.Point(199, 29);
            this.txtEntryNo.Mask = null;
            this.txtEntryNo.MaxLength = 20;
            this.txtEntryNo.Name = "txtEntryNo";
            this.txtEntryNo.NameOfControl = "Entry No";
            this.txtEntryNo.Prefix = null;
            this.txtEntryNo.ShowBallonTip = false;
            this.txtEntryNo.ShowErrorIcon = false;
            this.txtEntryNo.ShowMessage = null;
            this.txtEntryNo.Size = new System.Drawing.Size(96, 21);
            this.txtEntryNo.Suffix = null;
            this.txtEntryNo.TabIndex = 251;
            this.txtEntryNo.Text = "0";
            this.txtEntryNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblEntryNo
            // 
            this.lblEntryNo.AutoSize = true;
            this.lblEntryNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryNo.Location = new System.Drawing.Point(93, 33);
            this.lblEntryNo.Name = "lblEntryNo";
            this.lblEntryNo.Size = new System.Drawing.Size(68, 14);
            this.lblEntryNo.TabIndex = 255;
            this.lblEntryNo.Text = "Entry No.";
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
            this.txtCode.Location = new System.Drawing.Point(9, 6);
            this.txtCode.Mask = null;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(23, 20);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 90098;
            this.txtCode.Visible = false;
            // 
            // frmFabricRateList
            // 
            this.ClientSize = new System.Drawing.Size(955, 547);
            this.Name = "frmFabricRateList";
            this.Load += new System.EventHandler(this.frmFabricRateList_Load);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.GrpFabricType.ResumeLayout(false);
            this.GrpFabricType.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox GrpFabricType;
        private CIS_CLibrary.CIS_RadioButton rdoDesign;
        private CIS_CLibrary.CIS_RadioButton rdoQuality;
        private System.Windows.Forms.Panel pnlDetail;
        internal CIS_CLibrary.CIS_Button btnSelect;
        internal CIS_CLibrary.CIS_Textbox dtEntryDate;
        internal CIS_CLibrary.CIS_TextLabel lblEntryDate;
        internal CIS_CLibrary.CIS_TextLabel lblEntryDateColon;
        internal CIS_CLibrary.CIS_TextLabel lblEntryNoColon;
        internal CIS_CLibrary.CIS_Textbox txtEntryNo;
        internal CIS_CLibrary.CIS_TextLabel lblEntryNo;
        public CIS_CLibrary.CIS_Textbox txtCode;
    }
}
