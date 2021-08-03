namespace CIS_Textil
{
    partial class frmPackingRateList
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
            this.lblPackerColon = new CIS_CLibrary.CIS_TextLabel();
            this.cboPacker = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblPacker = new CIS_CLibrary.CIS_TextLabel();
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
            this.pnlContent.Controls.Add(this.lblPackerColon);
            this.pnlContent.Controls.Add(this.cboPacker);
            this.pnlContent.Controls.Add(this.lblPacker);
            this.pnlContent.Controls.Add(this.txtCode);
            this.tltOnControls.SetToolTip(this.pnlContent, "");
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblPacker, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboPacker, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblPackerColon, 0);
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
            this.pnlDetail.Location = new System.Drawing.Point(183, 45);
            this.pnlDetail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(587, 234);
            this.pnlDetail.TabIndex = 90100;
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
            this.txtCode.TabIndex = 90098;
            this.tltOnControls.SetToolTip(this.txtCode, "");
            this.txtCode.Visible = false;
            // 
            // lblPackerColon
            // 
            this.lblPackerColon.AutoSize = true;
            this.lblPackerColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblPackerColon.Location = new System.Drawing.Point(336, 19);
            this.lblPackerColon.Name = "lblPackerColon";
            this.lblPackerColon.Size = new System.Drawing.Size(12, 14);
            this.lblPackerColon.TabIndex = 90102;
            this.lblPackerColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblPackerColon, "");
            // 
            // cboPacker
            // 
            this.cboPacker.AutoComplete = false;
            this.cboPacker.AutoDropdown = false;
            this.cboPacker.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboPacker.BackColorEven = System.Drawing.Color.White;
            this.cboPacker.BackColorOdd = System.Drawing.Color.White;
            this.cboPacker.ColumnNames = "";
            this.cboPacker.ColumnWidthDefault = 175;
            this.cboPacker.ColumnWidths = "";
            this.cboPacker.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboPacker.Fill_ComboID = 0;
            this.cboPacker.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.cboPacker.FormattingEnabled = true;
            this.cboPacker.HelpText = "Select Packer";
            this.cboPacker.IsMandatory = true;
            this.cboPacker.LinkedColumnIndex = 0;
            this.cboPacker.LinkedTextBox = null;
            this.cboPacker.Location = new System.Drawing.Point(352, 15);
            this.cboPacker.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboPacker.Name = "cboPacker";
            this.cboPacker.NameOfControl = "Packer";
            this.cboPacker.OpenForm = null;
            this.cboPacker.ShowBallonTip = false;
            this.cboPacker.Size = new System.Drawing.Size(295, 22);
            this.cboPacker.TabIndex = 1;
            this.tltOnControls.SetToolTip(this.cboPacker, "Select Packer");
            // 
            // lblPacker
            // 
            this.lblPacker.AutoSize = true;
            this.lblPacker.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblPacker.Location = new System.Drawing.Point(277, 19);
            this.lblPacker.Name = "lblPacker";
            this.lblPacker.Size = new System.Drawing.Size(53, 14);
            this.lblPacker.TabIndex = 90101;
            this.lblPacker.Text = "Packer";
            this.tltOnControls.SetToolTip(this.lblPacker, "");
            // 
            // tltOnControls
            // 
            this.tltOnControls.DataProvider = stringDataProvider1;
            this.tltOnControls.LoadText = "";
            this.tltOnControls.ShowToolTip = false;
            // 
            // frmPackingRateList
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.ClientSize = new System.Drawing.Size(955, 547);
            this.DoubleBuffered = false;
            this.Name = "frmPackingRateList";
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
        internal CIS_CLibrary.CIS_TextLabel lblPackerColon;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboPacker;
        internal CIS_CLibrary.CIS_TextLabel lblPacker;
        private CIS_CLibrary.ToolTip.CIS_ToolTip tltOnControls;
    }
}
