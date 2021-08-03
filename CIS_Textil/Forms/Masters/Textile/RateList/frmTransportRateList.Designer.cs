namespace CIS_Textil
{
    partial class frmTransportRateList
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
            this.lblTransportColon = new CIS_CLibrary.CIS_TextLabel();
            this.cboTransport = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblTransport = new CIS_CLibrary.CIS_TextLabel();
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
            this.pnlContent.Controls.Add(this.lblTransportColon);
            this.pnlContent.Controls.Add(this.cboTransport);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.Add(this.lblTransport);
            this.tltOnControls.SetToolTip(this.pnlContent, "");
            this.pnlContent.Controls.SetChildIndex(this.lblTransport, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboTransport, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblTransportColon, 0);
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
            this.pnlDetail.Location = new System.Drawing.Point(182, 43);
            this.pnlDetail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(591, 234);
            this.pnlDetail.TabIndex = 1;
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
            this.txtCode.Location = new System.Drawing.Point(3, -1);
            this.txtCode.Mask = null;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(42, 20);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 90088;
            this.tltOnControls.SetToolTip(this.txtCode, "");
            this.txtCode.Visible = false;
            // 
            // lblTransportColon
            // 
            this.lblTransportColon.AutoSize = true;
            this.lblTransportColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransportColon.Location = new System.Drawing.Point(366, 17);
            this.lblTransportColon.Name = "lblTransportColon";
            this.lblTransportColon.Size = new System.Drawing.Size(12, 14);
            this.lblTransportColon.TabIndex = 90092;
            this.lblTransportColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblTransportColon, "");
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
            this.cboTransport.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTransport.FormattingEnabled = true;
            this.cboTransport.HelpText = "Select Transport";
            this.cboTransport.IsMandatory = true;
            this.cboTransport.LinkedColumnIndex = 0;
            this.cboTransport.LinkedTextBox = null;
            this.cboTransport.Location = new System.Drawing.Point(383, 15);
            this.cboTransport.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboTransport.Name = "cboTransport";
            this.cboTransport.NameOfControl = "Transport ";
            this.cboTransport.OpenForm = null;
            this.cboTransport.ShowBallonTip = false;
            this.cboTransport.Size = new System.Drawing.Size(266, 22);
            this.cboTransport.TabIndex = 0;
            this.tltOnControls.SetToolTip(this.cboTransport, "Select Transport");
            // 
            // lblTransport
            // 
            this.lblTransport.AutoSize = true;
            this.lblTransport.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransport.Location = new System.Drawing.Point(284, 17);
            this.lblTransport.Name = "lblTransport";
            this.lblTransport.Size = new System.Drawing.Size(71, 14);
            this.lblTransport.TabIndex = 90091;
            this.lblTransport.Text = "Transport";
            this.tltOnControls.SetToolTip(this.lblTransport, "");
            // 
            // tltOnControls
            // 
            this.tltOnControls.DataProvider = stringDataProvider1;
            this.tltOnControls.LoadText = "";
            this.tltOnControls.ShowToolTip = false;
            // 
            // frmTransportRateList
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.ClientSize = new System.Drawing.Size(955, 547);
            this.DoubleBuffered = false;
            this.Name = "frmTransportRateList";
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
        internal CIS_CLibrary.CIS_TextLabel lblTransportColon;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboTransport;
        internal CIS_CLibrary.CIS_TextLabel lblTransport;
        private CIS_CLibrary.ToolTip.CIS_ToolTip tltOnControls;
    }
}
