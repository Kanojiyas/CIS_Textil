namespace CIS_Textil
{
    partial class frmUserRights
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
            this.chkDelete = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.chkEdit = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.chkAdd = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.chkView = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.lblSelectAll = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.ChkActive = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.lblSecurityLevel = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.CboUserType = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.chkApprove = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.chkAudit = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.cboCompany = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblCompany = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblCompColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.chkSettings = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.chkSMS = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.chkEmail = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.chkPrint = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.chkSettings);
            this.pnlContent.Controls.Add(this.chkSMS);
            this.pnlContent.Controls.Add(this.chkEmail);
            this.pnlContent.Controls.Add(this.chkPrint);
            this.pnlContent.Controls.Add(this.lblCompColon);
            this.pnlContent.Controls.Add(this.cboCompany);
            this.pnlContent.Controls.Add(this.lblCompany);
            this.pnlContent.Controls.Add(this.chkAudit);
            this.pnlContent.Controls.Add(this.chkApprove);
            this.pnlContent.Controls.Add(this.CboUserType);
            this.pnlContent.Controls.Add(this.chkDelete);
            this.pnlContent.Controls.Add(this.chkEdit);
            this.pnlContent.Controls.Add(this.chkAdd);
            this.pnlContent.Controls.Add(this.chkView);
            this.pnlContent.Controls.Add(this.lblSelectAll);
            this.pnlContent.Controls.Add(this.pnlDetail);
            this.pnlContent.Controls.Add(this.ChkActive);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.Add(this.lblSecurityLevel);
            this.pnlContent.Controls.SetChildIndex(this.lblSecurityLevel, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.ChkActive, 0);
            this.pnlContent.Controls.SetChildIndex(this.pnlDetail, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblSelectAll, 0);
            this.pnlContent.Controls.SetChildIndex(this.chkView, 0);
            this.pnlContent.Controls.SetChildIndex(this.chkAdd, 0);
            this.pnlContent.Controls.SetChildIndex(this.chkEdit, 0);
            this.pnlContent.Controls.SetChildIndex(this.chkDelete, 0);
            this.pnlContent.Controls.SetChildIndex(this.CboUserType, 0);
            this.pnlContent.Controls.SetChildIndex(this.chkApprove, 0);
            this.pnlContent.Controls.SetChildIndex(this.chkAudit, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblCompany, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboCompany, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblCompColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.chkPrint, 0);
            this.pnlContent.Controls.SetChildIndex(this.chkEmail, 0);
            this.pnlContent.Controls.SetChildIndex(this.chkSMS, 0);
            this.pnlContent.Controls.SetChildIndex(this.chkSettings, 0);
            // 
            // chkDelete
            // 
            this.chkDelete.AutoSize = true;
            this.chkDelete.BackColor = System.Drawing.Color.MintCream;
            this.chkDelete.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDelete.HelpText = null;
            this.chkDelete.Location = new System.Drawing.Point(547, 464);
            this.chkDelete.Moveable = false;
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.NameOfControl = null;
            this.chkDelete.Size = new System.Drawing.Size(15, 14);
            this.chkDelete.TabIndex = 134;
            this.chkDelete.UseVisualStyleBackColor = true;
            this.chkDelete.CheckStateChanged += new System.EventHandler(this.ChkDelete_CheckStateChanged);
            // 
            // chkEdit
            // 
            this.chkEdit.AutoSize = true;
            this.chkEdit.BackColor = System.Drawing.Color.MintCream;
            this.chkEdit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEdit.HelpText = null;
            this.chkEdit.Location = new System.Drawing.Point(496, 464);
            this.chkEdit.Moveable = false;
            this.chkEdit.Name = "chkEdit";
            this.chkEdit.NameOfControl = null;
            this.chkEdit.Size = new System.Drawing.Size(15, 14);
            this.chkEdit.TabIndex = 136;
            this.chkEdit.UseVisualStyleBackColor = true;
            this.chkEdit.CheckStateChanged += new System.EventHandler(this.ChkEdit_CheckStateChanged);
            // 
            // chkAdd
            // 
            this.chkAdd.AutoSize = true;
            this.chkAdd.BackColor = System.Drawing.Color.MintCream;
            this.chkAdd.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAdd.HelpText = null;
            this.chkAdd.Location = new System.Drawing.Point(446, 464);
            this.chkAdd.Moveable = false;
            this.chkAdd.Name = "chkAdd";
            this.chkAdd.NameOfControl = null;
            this.chkAdd.Size = new System.Drawing.Size(15, 14);
            this.chkAdd.TabIndex = 135;
            this.chkAdd.UseVisualStyleBackColor = true;
            this.chkAdd.CheckStateChanged += new System.EventHandler(this.ChkAdd_CheckStateChanged);
            // 
            // chkView
            // 
            this.chkView.AutoSize = true;
            this.chkView.BackColor = System.Drawing.Color.MintCream;
            this.chkView.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkView.HelpText = null;
            this.chkView.Location = new System.Drawing.Point(397, 464);
            this.chkView.Moveable = false;
            this.chkView.Name = "chkView";
            this.chkView.NameOfControl = null;
            this.chkView.Size = new System.Drawing.Size(15, 14);
            this.chkView.TabIndex = 133;
            this.chkView.UseVisualStyleBackColor = true;
            this.chkView.CheckStateChanged += new System.EventHandler(this.ChkView_CheckStateChanged);
            // 
            // lblSelectAll
            // 
            this.lblSelectAll.AutoSize = true;
            this.lblSelectAll.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblSelectAll.Location = new System.Drawing.Point(249, 464);
            this.lblSelectAll.Moveable = false;
            this.lblSelectAll.Name = "lblSelectAll";
            this.lblSelectAll.NameOfControl = null;
            this.lblSelectAll.Size = new System.Drawing.Size(78, 14);
            this.lblSelectAll.TabIndex = 132;
            this.lblSelectAll.Text = "Select All :";
            // 
            // pnlDetail
            // 
            this.pnlDetail.BackColor = System.Drawing.Color.LightBlue;
            this.pnlDetail.Location = new System.Drawing.Point(33, 41);
            this.pnlDetail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(891, 417);
            this.pnlDetail.TabIndex = 131;
            // 
            // ChkActive
            // 
            this.ChkActive.AutoSize = true;
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.HelpText = null;
            this.ChkActive.Location = new System.Drawing.Point(750, 16);
            this.ChkActive.Moveable = false;
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.NameOfControl = null;
            this.ChkActive.Size = new System.Drawing.Size(112, 17);
            this.ChkActive.TabIndex = 130;
            this.ChkActive.Text = "Active Status";
            this.ChkActive.UseVisualStyleBackColor = false;
            // 
            // txtCode
            // 
            this.txtCode.AutoFillDate = false;
            this.txtCode.BackColor = System.Drawing.Color.White;
            this.txtCode.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtCode.CheckForSymbol = null;
            this.txtCode.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtCode.DecimalPlace = 0;
            this.txtCode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.HelpText = "";
            this.txtCode.HoldMyText = null;
            this.txtCode.IsMandatory = false;
            this.txtCode.IsSingleQuote = true;
            this.txtCode.IsSysmbol = false;
            this.txtCode.Location = new System.Drawing.Point(0, 0);
            this.txtCode.Mask = null;
            this.txtCode.Moveable = false;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(32, 22);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 128;
            this.txtCode.Visible = false;
            // 
            // lblSecurityLevel
            // 
            this.lblSecurityLevel.AutoSize = true;
            this.lblSecurityLevel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblSecurityLevel.Location = new System.Drawing.Point(402, 15);
            this.lblSecurityLevel.Moveable = false;
            this.lblSecurityLevel.Name = "lblSecurityLevel";
            this.lblSecurityLevel.NameOfControl = null;
            this.lblSecurityLevel.Size = new System.Drawing.Size(111, 14);
            this.lblSecurityLevel.TabIndex = 127;
            this.lblSecurityLevel.Text = "Security Level :";
            // 
            // CboUserType
            // 
            this.CboUserType.AutoComplete = false;
            this.CboUserType.AutoDropdown = false;
            this.CboUserType.BackColor = System.Drawing.Color.White;
            this.CboUserType.BackColorEven = System.Drawing.Color.White;
            this.CboUserType.BackColorOdd = System.Drawing.Color.White;
            this.CboUserType.ColumnNames = "";
            this.CboUserType.ColumnWidthDefault = 175;
            this.CboUserType.ColumnWidths = "";
            this.CboUserType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.CboUserType.Fill_ComboID = 0;
            this.CboUserType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboUserType.FormattingEnabled = true;
            this.CboUserType.GroupType = 0;
            this.CboUserType.HelpText = null;
            this.CboUserType.IsMandatory = false;
            this.CboUserType.LinkedColumnIndex = 0;
            this.CboUserType.LinkedTextBox = null;
            this.CboUserType.Location = new System.Drawing.Point(518, 14);
            this.CboUserType.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.CboUserType.Moveable = false;
            this.CboUserType.Name = "CboUserType";
            this.CboUserType.NameOfControl = null;
            this.CboUserType.OpenForm = "frmYarnTypeMaster";
            this.CboUserType.ShowBallonTip = false;
            this.CboUserType.Size = new System.Drawing.Size(190, 23);
            this.CboUserType.TabIndex = 137;
            // 
            // chkApprove
            // 
            this.chkApprove.AutoSize = true;
            this.chkApprove.BackColor = System.Drawing.Color.MintCream;
            this.chkApprove.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkApprove.HelpText = null;
            this.chkApprove.Location = new System.Drawing.Point(799, 464);
            this.chkApprove.Moveable = false;
            this.chkApprove.Name = "chkApprove";
            this.chkApprove.NameOfControl = null;
            this.chkApprove.Size = new System.Drawing.Size(15, 14);
            this.chkApprove.TabIndex = 138;
            this.chkApprove.UseVisualStyleBackColor = true;
            this.chkApprove.CheckStateChanged += new System.EventHandler(this.chkApprove_CheckStateChanged);
            // 
            // chkAudit
            // 
            this.chkAudit.AutoSize = true;
            this.chkAudit.BackColor = System.Drawing.Color.MintCream;
            this.chkAudit.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAudit.HelpText = null;
            this.chkAudit.Location = new System.Drawing.Point(849, 464);
            this.chkAudit.Moveable = false;
            this.chkAudit.Name = "chkAudit";
            this.chkAudit.NameOfControl = null;
            this.chkAudit.Size = new System.Drawing.Size(15, 14);
            this.chkAudit.TabIndex = 139;
            this.chkAudit.UseVisualStyleBackColor = true;
            this.chkAudit.CheckStateChanged += new System.EventHandler(this.chkAudit_CheckStateChanged);
            // 
            // cboCompany
            // 
            this.cboCompany.AutoComplete = false;
            this.cboCompany.AutoDropdown = false;
            this.cboCompany.BackColor = System.Drawing.Color.White;
            this.cboCompany.BackColorEven = System.Drawing.Color.White;
            this.cboCompany.BackColorOdd = System.Drawing.Color.White;
            this.cboCompany.ColumnNames = "";
            this.cboCompany.ColumnWidthDefault = 175;
            this.cboCompany.ColumnWidths = "";
            this.cboCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboCompany.Fill_ComboID = 0;
            this.cboCompany.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.GroupType = 0;
            this.cboCompany.HelpText = null;
            this.cboCompany.IsMandatory = false;
            this.cboCompany.LinkedColumnIndex = 0;
            this.cboCompany.LinkedTextBox = null;
            this.cboCompany.Location = new System.Drawing.Point(143, 13);
            this.cboCompany.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboCompany.Moveable = false;
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.NameOfControl = null;
            this.cboCompany.OpenForm = "frmYarnTypeMaster";
            this.cboCompany.ShowBallonTip = false;
            this.cboCompany.Size = new System.Drawing.Size(218, 23);
            this.cboCompany.TabIndex = 141;
            this.cboCompany.SelectedValueChanged += new System.EventHandler(this.cboCompany_SelectedValueChanged);
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblCompany.Location = new System.Drawing.Point(42, 16);
            this.lblCompany.Moveable = false;
            this.lblCompany.Name = "lblCompany";
            this.lblCompany.NameOfControl = null;
            this.lblCompany.Size = new System.Drawing.Size(72, 14);
            this.lblCompany.TabIndex = 140;
            this.lblCompany.Text = "Company ";
            // 
            // lblCompColon
            // 
            this.lblCompColon.AutoSize = true;
            this.lblCompColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblCompColon.Location = new System.Drawing.Point(130, 16);
            this.lblCompColon.Moveable = false;
            this.lblCompColon.Name = "lblCompColon";
            this.lblCompColon.NameOfControl = null;
            this.lblCompColon.Size = new System.Drawing.Size(12, 14);
            this.lblCompColon.TabIndex = 142;
            this.lblCompColon.Text = ":";
            // 
            // chkSettings
            // 
            this.chkSettings.AutoSize = true;
            this.chkSettings.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSettings.HelpText = null;
            this.chkSettings.Location = new System.Drawing.Point(750, 464);
            this.chkSettings.Moveable = false;
            this.chkSettings.Name = "chkSettings";
            this.chkSettings.NameOfControl = null;
            this.chkSettings.Size = new System.Drawing.Size(15, 14);
            this.chkSettings.TabIndex = 144;
            this.chkSettings.UseVisualStyleBackColor = true;
            this.chkSettings.CheckStateChanged += new System.EventHandler(this.chkSettings_CheckStateChanged);
            // 
            // chkSMS
            // 
            this.chkSMS.AutoSize = true;
            this.chkSMS.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSMS.HelpText = null;
            this.chkSMS.Location = new System.Drawing.Point(699, 464);
            this.chkSMS.Moveable = false;
            this.chkSMS.Name = "chkSMS";
            this.chkSMS.NameOfControl = null;
            this.chkSMS.Size = new System.Drawing.Size(15, 14);
            this.chkSMS.TabIndex = 146;
            this.chkSMS.UseVisualStyleBackColor = true;
            this.chkSMS.CheckStateChanged += new System.EventHandler(this.chkSMS_CheckStateChanged);
            // 
            // chkEmail
            // 
            this.chkEmail.AutoSize = true;
            this.chkEmail.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkEmail.HelpText = null;
            this.chkEmail.Location = new System.Drawing.Point(648, 464);
            this.chkEmail.Moveable = false;
            this.chkEmail.Name = "chkEmail";
            this.chkEmail.NameOfControl = null;
            this.chkEmail.Size = new System.Drawing.Size(15, 14);
            this.chkEmail.TabIndex = 145;
            this.chkEmail.UseVisualStyleBackColor = true;
            this.chkEmail.CheckStateChanged += new System.EventHandler(this.chkEmail_CheckStateChanged);
            // 
            // chkPrint
            // 
            this.chkPrint.AutoSize = true;
            this.chkPrint.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPrint.HelpText = null;
            this.chkPrint.Location = new System.Drawing.Point(597, 464);
            this.chkPrint.Moveable = false;
            this.chkPrint.Name = "chkPrint";
            this.chkPrint.NameOfControl = null;
            this.chkPrint.Size = new System.Drawing.Size(15, 14);
            this.chkPrint.TabIndex = 143;
            this.chkPrint.UseVisualStyleBackColor = true;
            this.chkPrint.CheckStateChanged += new System.EventHandler(this.chkPrint_CheckStateChanged);
            // 
            // frmUserRights
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.ClientSize = new System.Drawing.Size(955, 547);
            this.DoubleBuffered = false;
            this.Name = "frmUserRights";
            this.Load += new System.EventHandler(this.frmUserRights_Load);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal CIS_CLibrary.CIS_CheckBox chkDelete;
        internal CIS_CLibrary.CIS_CheckBox chkEdit;
        internal CIS_CLibrary.CIS_CheckBox chkAdd;
        internal CIS_CLibrary.CIS_CheckBox chkView;
        internal CIS_CLibrary.CIS_TextLabel lblSelectAll;
        internal System.Windows.Forms.Panel pnlDetail;
        internal CIS_CLibrary.CIS_CheckBox ChkActive;
        public CIS_CLibrary.CIS_Textbox txtCode;
        internal CIS_CLibrary.CIS_TextLabel lblSecurityLevel;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox CboUserType;
        internal CIS_CLibrary.CIS_CheckBox chkAudit;
        internal CIS_CLibrary.CIS_CheckBox chkApprove;
        internal CIS_CLibrary.CIS_TextLabel lblCompColon;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboCompany;
        internal CIS_CLibrary.CIS_TextLabel lblCompany;
        internal CIS_CLibrary.CIS_CheckBox chkSettings;
        internal CIS_CLibrary.CIS_CheckBox chkSMS;
        internal CIS_CLibrary.CIS_CheckBox chkEmail;
        internal CIS_CLibrary.CIS_CheckBox chkPrint;
    }
}
