namespace CIS_Textil
{
    partial class frmWeaverRateList
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
            this.cboWeaver = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblWeaverColon = new CIS_CLibrary.CIS_TextLabel();
            this.lblWeaver = new CIS_CLibrary.CIS_TextLabel();
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
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
            this.pnlContent.Controls.Add(this.cboWeaver);
            this.pnlContent.Controls.Add(this.lblWeaverColon);
            this.pnlContent.Controls.Add(this.lblWeaver);
            this.pnlContent.Controls.Add(this.txtCode);
            this.tltOnControls.SetToolTip(this.pnlContent, "");
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblWeaver, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblWeaverColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboWeaver, 0);
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
            this.pnlDetail.Location = new System.Drawing.Point(209, 45);
            this.pnlDetail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(535, 230);
            this.pnlDetail.TabIndex = 2;
            this.tltOnControls.SetToolTip(this.pnlDetail, "");
            // 
            // cboWeaver
            // 
            this.cboWeaver.AutoComplete = false;
            this.cboWeaver.AutoDropdown = false;
            this.cboWeaver.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboWeaver.BackColorEven = System.Drawing.Color.White;
            this.cboWeaver.BackColorOdd = System.Drawing.Color.White;
            this.cboWeaver.ColumnNames = "";
            this.cboWeaver.ColumnWidthDefault = 175;
            this.cboWeaver.ColumnWidths = "";
            this.cboWeaver.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboWeaver.Fill_ComboID = 0;
            this.cboWeaver.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.cboWeaver.FormattingEnabled = true;
            this.cboWeaver.HelpText = "Select Weaver";
            this.cboWeaver.IsMandatory = true;
            this.cboWeaver.LinkedColumnIndex = 0;
            this.cboWeaver.LinkedTextBox = null;
            this.cboWeaver.Location = new System.Drawing.Point(378, 17);
            this.cboWeaver.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboWeaver.Name = "cboWeaver";
            this.cboWeaver.NameOfControl = "Weaver";
            this.cboWeaver.OpenForm = null;
            this.cboWeaver.ShowBallonTip = false;
            this.cboWeaver.Size = new System.Drawing.Size(260, 22);
            this.cboWeaver.TabIndex = 1;
            this.tltOnControls.SetToolTip(this.cboWeaver, "Select Weaver");
            // 
            // lblWeaverColon
            // 
            this.lblWeaverColon.AutoSize = true;
            this.lblWeaverColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeaverColon.Location = new System.Drawing.Point(356, 19);
            this.lblWeaverColon.Name = "lblWeaverColon";
            this.lblWeaverColon.Size = new System.Drawing.Size(12, 14);
            this.lblWeaverColon.TabIndex = 108;
            this.lblWeaverColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblWeaverColon, "");
            // 
            // lblWeaver
            // 
            this.lblWeaver.AutoSize = true;
            this.lblWeaver.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWeaver.Location = new System.Drawing.Point(291, 19);
            this.lblWeaver.Name = "lblWeaver";
            this.lblWeaver.Size = new System.Drawing.Size(59, 14);
            this.lblWeaver.TabIndex = 107;
            this.lblWeaver.Text = "Weaver";
            this.tltOnControls.SetToolTip(this.lblWeaver, "");
            // 
            // txtCode
            // 
            this.txtCode.AutoFillDate = false;
            this.txtCode.BackColor = System.Drawing.Color.White;
            this.txtCode.CheckForSymbol = null;
            this.txtCode.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtCode.DecimalPlace = 0;
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
            this.txtCode.Size = new System.Drawing.Size(35, 20);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 0;
            this.tltOnControls.SetToolTip(this.txtCode, "");
            this.txtCode.Visible = false;
            // 
            // tltOnControls
            // 
            this.tltOnControls.DataProvider = stringDataProvider1;
            this.tltOnControls.LoadText = "";
            this.tltOnControls.ShowToolTip = false;
            // 
            // frmWeaverRateList
            // 
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.ClientSize = new System.Drawing.Size(955, 547);
            this.DoubleBuffered = false;
            this.Name = "frmWeaverRateList";
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
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboWeaver;
        internal CIS_CLibrary.CIS_TextLabel lblWeaverColon;
        internal CIS_CLibrary.CIS_TextLabel lblWeaver;
        public CIS_CLibrary.CIS_Textbox txtCode;
        private CIS_CLibrary.ToolTip.CIS_ToolTip tltOnControls;
    }
}
