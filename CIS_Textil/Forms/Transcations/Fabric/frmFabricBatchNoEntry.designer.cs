namespace CIS_Textil
{
    partial class frmFabricBatchNoEntry
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
            CIS_CLibrary.ToolTip.StringDataProvider stringDataProvider1 = new CIS_CLibrary.ToolTip.StringDataProvider();
            this.lblDescriptionColon = new System.Windows.Forms.Label();
            this.txtDescription = new CIS_CLibrary.CIS_Textbox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblEntryDateColon = new System.Windows.Forms.Label();
            this.dtEntryDate = new CIS_CLibrary.CIS_Textbox();
            this.lblEntryDate = new System.Windows.Forms.Label();
            this.lblEntryNoColon = new System.Windows.Forms.Label();
            this.txtEntryNo = new CIS_CLibrary.CIS_Textbox();
            this.lblEntryNo = new System.Windows.Forms.Label();
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.btnFetchBatch = new CIS_CLibrary.CIS_Button();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.GrdMain = new Crocus_CClib.DataGridViewX();
            this.tltOnControls = new CIS_CLibrary.ToolTip.CIS_ToolTip();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.pnlDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.lblDescriptionColon);
            this.pnlContent.Controls.Add(this.txtDescription);
            this.pnlContent.Controls.Add(this.lblDescription);
            this.pnlContent.Controls.Add(this.lblEntryDateColon);
            this.pnlContent.Controls.Add(this.dtEntryDate);
            this.pnlContent.Controls.Add(this.lblEntryDate);
            this.pnlContent.Controls.Add(this.lblEntryNoColon);
            this.pnlContent.Controls.Add(this.txtEntryNo);
            this.pnlContent.Controls.Add(this.lblEntryNo);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.Add(this.btnFetchBatch);
            this.pnlContent.Controls.Add(this.pnlDetail);
            this.tltOnControls.SetToolTip(this.pnlContent, "");
            this.pnlContent.Controls.SetChildIndex(this.pnlDetail, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnFetchBatch, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtEntryNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryNoColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryDate, 0);
            this.pnlContent.Controls.SetChildIndex(this.dtEntryDate, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEntryDateColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblDescription, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtDescription, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblDescriptionColon, 0);
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
            // lblDescriptionColon
            // 
            this.lblDescriptionColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDescriptionColon.AutoSize = true;
            this.lblDescriptionColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescriptionColon.Location = new System.Drawing.Point(217, 460);
            this.lblDescriptionColon.Name = "lblDescriptionColon";
            this.lblDescriptionColon.Size = new System.Drawing.Size(12, 14);
            this.lblDescriptionColon.TabIndex = 843;
            this.lblDescriptionColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblDescriptionColon, "");
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
            this.txtDescription.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.txtDescription.HelpText = "Enter Description";
            this.txtDescription.HoldMyText = null;
            this.txtDescription.IsMandatory = false;
            this.txtDescription.IsSingleQuote = true;
            this.txtDescription.IsSysmbol = false;
            this.txtDescription.Location = new System.Drawing.Point(235, 458);
            this.txtDescription.Mask = null;
            this.txtDescription.Moveable = false;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.NameOfControl = "Description";
            this.txtDescription.Prefix = null;
            this.txtDescription.ShowBallonTip = false;
            this.txtDescription.ShowErrorIcon = false;
            this.txtDescription.ShowMessage = null;
            this.txtDescription.Size = new System.Drawing.Size(689, 21);
            this.txtDescription.Suffix = null;
            this.txtDescription.TabIndex = 5;
            this.tltOnControls.SetToolTip(this.txtDescription, "");
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(129, 460);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(82, 14);
            this.lblDescription.TabIndex = 842;
            this.lblDescription.Text = "Description";
            this.tltOnControls.SetToolTip(this.lblDescription, "");
            // 
            // lblEntryDateColon
            // 
            this.lblEntryDateColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEntryDateColon.AutoSize = true;
            this.lblEntryDateColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryDateColon.Location = new System.Drawing.Point(431, 31);
            this.lblEntryDateColon.Name = "lblEntryDateColon";
            this.lblEntryDateColon.Size = new System.Drawing.Size(12, 14);
            this.lblEntryDateColon.TabIndex = 839;
            this.lblEntryDateColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblEntryDateColon, "");
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
            this.dtEntryDate.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.dtEntryDate.HelpText = "Enter Entry Date";
            this.dtEntryDate.HoldMyText = null;
            this.dtEntryDate.IsMandatory = true;
            this.dtEntryDate.IsSingleQuote = true;
            this.dtEntryDate.IsSysmbol = false;
            this.dtEntryDate.Location = new System.Drawing.Point(447, 28);
            this.dtEntryDate.Mask = "__/__/____";
            this.dtEntryDate.MaxLength = 10;
            this.dtEntryDate.Moveable = false;
            this.dtEntryDate.Name = "dtEntryDate";
            this.dtEntryDate.NameOfControl = "Entry date";
            this.dtEntryDate.Prefix = null;
            this.dtEntryDate.ShowBallonTip = false;
            this.dtEntryDate.ShowErrorIcon = false;
            this.dtEntryDate.ShowMessage = null;
            this.dtEntryDate.Size = new System.Drawing.Size(99, 21);
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
            this.lblEntryDate.Location = new System.Drawing.Point(355, 31);
            this.lblEntryDate.Name = "lblEntryDate";
            this.lblEntryDate.Size = new System.Drawing.Size(77, 14);
            this.lblEntryDate.TabIndex = 840;
            this.lblEntryDate.Text = "Entry Date";
            this.tltOnControls.SetToolTip(this.lblEntryDate, "");
            // 
            // lblEntryNoColon
            // 
            this.lblEntryNoColon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblEntryNoColon.AutoSize = true;
            this.lblEntryNoColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntryNoColon.Location = new System.Drawing.Point(199, 31);
            this.lblEntryNoColon.Name = "lblEntryNoColon";
            this.lblEntryNoColon.Size = new System.Drawing.Size(12, 14);
            this.lblEntryNoColon.TabIndex = 838;
            this.lblEntryNoColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblEntryNoColon, "");
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
            this.txtEntryNo.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold);
            this.txtEntryNo.HelpText = "Entry No";
            this.txtEntryNo.HoldMyText = null;
            this.txtEntryNo.IsMandatory = true;
            this.txtEntryNo.IsSingleQuote = true;
            this.txtEntryNo.IsSysmbol = false;
            this.txtEntryNo.Location = new System.Drawing.Point(212, 28);
            this.txtEntryNo.Mask = null;
            this.txtEntryNo.MaxLength = 20;
            this.txtEntryNo.Moveable = false;
            this.txtEntryNo.Name = "txtEntryNo";
            this.txtEntryNo.NameOfControl = "Entry No";
            this.txtEntryNo.Prefix = null;
            this.txtEntryNo.ShowBallonTip = false;
            this.txtEntryNo.ShowErrorIcon = false;
            this.txtEntryNo.ShowMessage = null;
            this.txtEntryNo.Size = new System.Drawing.Size(102, 21);
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
            this.lblEntryNo.Location = new System.Drawing.Point(121, 31);
            this.lblEntryNo.Name = "lblEntryNo";
            this.lblEntryNo.Size = new System.Drawing.Size(68, 14);
            this.lblEntryNo.TabIndex = 837;
            this.lblEntryNo.Text = "Entry No.";
            this.tltOnControls.SetToolTip(this.lblEntryNo, "");
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
            this.txtCode.Location = new System.Drawing.Point(3, 3);
            this.txtCode.Mask = null;
            this.txtCode.Moveable = false;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(27, 20);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 0;
            this.tltOnControls.SetToolTip(this.txtCode, "");
            this.txtCode.Visible = false;
            // 
            // btnFetchBatch
            // 
            this.btnFetchBatch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnFetchBatch.BackColor = System.Drawing.Color.CadetBlue;
            this.btnFetchBatch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFetchBatch.Location = new System.Drawing.Point(661, 25);
            this.btnFetchBatch.Moveable = false;
            this.btnFetchBatch.Name = "btnFetchBatch";
            this.btnFetchBatch.Size = new System.Drawing.Size(263, 45);
            this.btnFetchBatch.TabIndex = 3;
            this.btnFetchBatch.Text = "Fetch Pending Lot\'s";
            this.tltOnControls.SetToolTip(this.btnFetchBatch, "");
            this.btnFetchBatch.UseVisualStyleBackColor = false;
            this.btnFetchBatch.Click += new System.EventHandler(this.btnFetchBatch_Click);
            // 
            // pnlDetail
            // 
            this.pnlDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDetail.BackColor = System.Drawing.Color.LightSkyBlue;
            this.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDetail.Controls.Add(this.GrdMain);
            this.pnlDetail.Location = new System.Drawing.Point(122, 76);
            this.pnlDetail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(802, 376);
            this.pnlDetail.TabIndex = 4;
            this.tltOnControls.SetToolTip(this.pnlDetail, "");
            // 
            // GrdMain
            // 
            this.GrdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.GrdMain.blnFormAction = 0;
            this.GrdMain.CompID = 0;
            this.GrdMain.Location = new System.Drawing.Point(0, 0);
            this.GrdMain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GrdMain.Name = "GrdMain";
            this.GrdMain.Size = new System.Drawing.Size(800, 374);
            this.GrdMain.TabIndex = 90157;
            this.tltOnControls.SetToolTip(this.GrdMain, "");
            this.GrdMain.YearID = 0;
            // 
            // tltOnControls
            // 
            this.tltOnControls.DataProvider = stringDataProvider1;
            this.tltOnControls.LoadText = "";
            this.tltOnControls.ShowToolTip = false;
            // 
            // frmFabricBatchNoEntry
            // 
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.ClientSize = new System.Drawing.Size(1072, 547);
            this.DoubleBuffered = false;
            this.Name = "frmFabricBatchNoEntry";
            this.Load += new System.EventHandler(this.frmFabricBatchNoEntry_Load);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlDetail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Label lblDescriptionColon;
        internal CIS_CLibrary.CIS_Textbox txtDescription;
        internal System.Windows.Forms.Label lblDescription;
        internal System.Windows.Forms.Label lblEntryDateColon;
        internal CIS_CLibrary.CIS_Textbox dtEntryDate;
        internal System.Windows.Forms.Label lblEntryDate;
        internal System.Windows.Forms.Label lblEntryNoColon;
        internal CIS_CLibrary.CIS_Textbox txtEntryNo;
        internal System.Windows.Forms.Label lblEntryNo;
        public CIS_CLibrary.CIS_Textbox txtCode;
        internal CIS_CLibrary.CIS_Button btnFetchBatch;
        internal System.Windows.Forms.Panel pnlDetail;
        private CIS_CLibrary.ToolTip.CIS_ToolTip tltOnControls;
        private Crocus_CClib.DataGridViewX GrdMain;
    }
}
