namespace CIS_Textil
{
    partial class frmWarpingRateList
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
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.cboWarper = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblWarper = new CIS_CLibrary.CIS_TextLabel();
            this.lblWarperColon = new CIS_CLibrary.CIS_TextLabel();
            this.tltOnControls = new CIS_CLibrary.ToolTip.CIS_ToolTip();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.pnlDetail);
            this.pnlContent.Controls.Add(this.cboWarper);
            this.pnlContent.Controls.Add(this.lblWarper);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.Add(this.lblWarperColon);
            this.tltOnControls.SetToolTip(this.pnlContent, "");
            this.pnlContent.Controls.SetChildIndex(this.lblWarperColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblWarper, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboWarper, 0);
            this.pnlContent.Controls.SetChildIndex(this.pnlDetail, 0);
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
            // pnlDetail
            // 
            this.pnlDetail.BackColor = System.Drawing.Color.LightBlue;
            this.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDetail.Location = new System.Drawing.Point(181, 45);
            this.pnlDetail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(591, 234);
            this.pnlDetail.TabIndex = 90095;
            this.tltOnControls.SetToolTip(this.pnlDetail, "");
            // 
            // txtCode
            // 
            this.txtCode.AutoFillDate = false;
            this.txtCode.BackColor = System.Drawing.Color.White;
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
            this.txtCode.Location = new System.Drawing.Point(3, 0);
            this.txtCode.Mask = null;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(42, 20);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 90093;
            this.tltOnControls.SetToolTip(this.txtCode, "");
            this.txtCode.Visible = false;
            // 
            // cboWarper
            // 
            this.cboWarper.AutoComplete = false;
            this.cboWarper.AutoDropdown = false;
            this.cboWarper.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboWarper.BackColorEven = System.Drawing.Color.White;
            this.cboWarper.BackColorOdd = System.Drawing.Color.White;
            this.cboWarper.ColumnNames = "";
            this.cboWarper.ColumnWidthDefault = 175;
            this.cboWarper.ColumnWidths = "";
            this.cboWarper.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboWarper.Fill_ComboID = 0;
            this.cboWarper.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboWarper.FormattingEnabled = true;
            this.cboWarper.HelpText = "Select Warper";
            this.cboWarper.IsMandatory = true;
            this.cboWarper.LinkedColumnIndex = 0;
            this.cboWarper.LinkedTextBox = null;
            this.cboWarper.Location = new System.Drawing.Point(356, 15);
            this.cboWarper.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboWarper.Name = "cboWarper";
            this.cboWarper.NameOfControl = "Warper";
            this.cboWarper.OpenForm = null;
            this.cboWarper.ShowBallonTip = false;
            this.cboWarper.Size = new System.Drawing.Size(295, 22);
            this.cboWarper.TabIndex = 0;
            this.tltOnControls.SetToolTip(this.cboWarper, "Select Warper");
            // 
            // lblWarper
            // 
            this.lblWarper.AutoSize = true;
            this.lblWarper.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarper.Location = new System.Drawing.Point(277, 17);
            this.lblWarper.Name = "lblWarper";
            this.lblWarper.Size = new System.Drawing.Size(57, 14);
            this.lblWarper.TabIndex = 90096;
            this.lblWarper.Text = "Warper";
            this.tltOnControls.SetToolTip(this.lblWarper, "");
            // 
            // lblWarperColon
            // 
            this.lblWarperColon.AutoSize = true;
            this.lblWarperColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarperColon.Location = new System.Drawing.Point(336, 17);
            this.lblWarperColon.Name = "lblWarperColon";
            this.lblWarperColon.Size = new System.Drawing.Size(12, 14);
            this.lblWarperColon.TabIndex = 90097;
            this.lblWarperColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblWarperColon, "");
            // 
            // tltOnControls
            // 
            this.tltOnControls.DataProvider = stringDataProvider1;
            this.tltOnControls.LoadText = "";
            this.tltOnControls.ShowToolTip = false;
            // 
            // frmWarpingRateList
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.ClientSize = new System.Drawing.Size(955, 547);
            this.DoubleBuffered = false;
            this.Name = "frmWarpingRateList";
            this.Load += new System.EventHandler(this.frm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlDetail;
        public CIS_CLibrary.CIS_Textbox txtCode;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboWarper;
        internal CIS_CLibrary.CIS_TextLabel lblWarper;
        internal CIS_CLibrary.CIS_TextLabel lblWarperColon;
        private CIS_CLibrary.ToolTip.CIS_ToolTip tltOnControls;
    }
}
