namespace CIS_Textil
{
    partial class frmMenuMaster_Comp
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
            this.chkSelectAll = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.lblSelectAll = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.ChkActive = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.cboCompany = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblCompany = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblCompColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblNotification = new CIS_CLibrary.CIS_Label(this.components);
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnAddNewRow = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnPaste = new System.Windows.Forms.Button();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.btnPaste);
            this.pnlContent.Controls.Add(this.btnCopy);
            this.pnlContent.Controls.Add(this.btnAddNewRow);
            this.pnlContent.Controls.Add(this.btnDown);
            this.pnlContent.Controls.Add(this.btnUp);
            this.pnlContent.Controls.Add(this.lblNotification);
            this.pnlContent.Controls.Add(this.lblCompColon);
            this.pnlContent.Controls.Add(this.cboCompany);
            this.pnlContent.Controls.Add(this.lblCompany);
            this.pnlContent.Controls.Add(this.chkSelectAll);
            this.pnlContent.Controls.Add(this.lblSelectAll);
            this.pnlContent.Controls.Add(this.pnlDetail);
            this.pnlContent.Controls.Add(this.ChkActive);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Size = new System.Drawing.Size(805, 496);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.ChkActive, 0);
            this.pnlContent.Controls.SetChildIndex(this.pnlDetail, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblSelectAll, 0);
            this.pnlContent.Controls.SetChildIndex(this.chkSelectAll, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblCompany, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboCompany, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblCompColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblNotification, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnUp, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnDown, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnAddNewRow, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnCopy, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnPaste, 0);
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkSelectAll.HelpText = null;
            this.chkSelectAll.Location = new System.Drawing.Point(634, 463);
            this.chkSelectAll.Moveable = false;
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.NameOfControl = null;
            this.chkSelectAll.Size = new System.Drawing.Size(15, 14);
            this.chkSelectAll.TabIndex = 133;
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.Visible = false;
            this.chkSelectAll.CheckStateChanged += new System.EventHandler(this.ChkView_CheckStateChanged);
            // 
            // lblSelectAll
            // 
            this.lblSelectAll.AutoSize = true;
            this.lblSelectAll.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblSelectAll.Location = new System.Drawing.Point(557, 461);
            this.lblSelectAll.Moveable = false;
            this.lblSelectAll.Name = "lblSelectAll";
            this.lblSelectAll.NameOfControl = null;
            this.lblSelectAll.Size = new System.Drawing.Size(78, 14);
            this.lblSelectAll.TabIndex = 132;
            this.lblSelectAll.Text = "Select All :";
            this.lblSelectAll.Visible = false;
            // 
            // pnlDetail
            // 
            this.pnlDetail.BackColor = System.Drawing.Color.SkyBlue;
            this.pnlDetail.Location = new System.Drawing.Point(29, 58);
            this.pnlDetail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(621, 400);
            this.pnlDetail.TabIndex = 131;
            // 
            // ChkActive
            // 
            this.ChkActive.AutoSize = true;
            this.ChkActive.BackColor = System.Drawing.Color.MintCream;
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.HelpText = null;
            this.ChkActive.Location = new System.Drawing.Point(527, 25);
            this.ChkActive.Moveable = false;
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.NameOfControl = null;
            this.ChkActive.Size = new System.Drawing.Size(112, 17);
            this.ChkActive.TabIndex = 130;
            this.ChkActive.Text = "Active Status";
            this.ChkActive.UseVisualStyleBackColor = false;
            this.ChkActive.Visible = false;
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
            this.txtCode.Location = new System.Drawing.Point(7, 2);
            this.txtCode.Mask = null;
            this.txtCode.Moveable = false;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(49, 22);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 128;
            this.txtCode.Visible = false;
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
            this.cboCompany.Location = new System.Drawing.Point(147, 22);
            this.cboCompany.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboCompany.Moveable = false;
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.NameOfControl = null;
            this.cboCompany.OpenForm = "frmYarnTypeMaster";
            this.cboCompany.ShowBallonTip = false;
            this.cboCompany.Size = new System.Drawing.Size(359, 23);
            this.cboCompany.TabIndex = 141;
            // 
            // lblCompany
            // 
            this.lblCompany.AutoSize = true;
            this.lblCompany.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblCompany.Location = new System.Drawing.Point(56, 26);
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
            this.lblCompColon.Location = new System.Drawing.Point(134, 26);
            this.lblCompColon.Moveable = false;
            this.lblCompColon.Name = "lblCompColon";
            this.lblCompColon.NameOfControl = null;
            this.lblCompColon.Size = new System.Drawing.Size(12, 14);
            this.lblCompColon.TabIndex = 142;
            this.lblCompColon.Text = ":";
            // 
            // lblNotification
            // 
            this.lblNotification.AutoSize = true;
            this.lblNotification.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.lblNotification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNotification.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblNotification.Location = new System.Drawing.Point(18, 469);
            this.lblNotification.Moveable = false;
            this.lblNotification.Name = "lblNotification";
            this.lblNotification.Size = new System.Drawing.Size(286, 16);
            this.lblNotification.TabIndex = 148;
            this.lblNotification.Text = "Now Change Company And Click On Paste";
            this.lblNotification.Visible = false;
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.DarkCyan;
            this.btnUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUp.Font = new System.Drawing.Font("Lucida Sans Unicode", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUp.Location = new System.Drawing.Point(672, 111);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(97, 74);
            this.btnUp.TabIndex = 468;
            this.btnUp.Text = "UP";
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.tsbtn_Up_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.DarkCyan;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDown.Font = new System.Drawing.Font("Lucida Sans Unicode", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDown.Location = new System.Drawing.Point(672, 189);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(97, 74);
            this.btnDown.TabIndex = 469;
            this.btnDown.Text = "DOWN";
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.tsbtn_Down_Click);
            // 
            // btnAddNewRow
            // 
            this.btnAddNewRow.BackColor = System.Drawing.Color.DarkCyan;
            this.btnAddNewRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewRow.Font = new System.Drawing.Font("Lucida Sans Unicode", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewRow.Location = new System.Drawing.Point(672, 291);
            this.btnAddNewRow.Name = "btnAddNewRow";
            this.btnAddNewRow.Size = new System.Drawing.Size(97, 41);
            this.btnAddNewRow.TabIndex = 470;
            this.btnAddNewRow.Text = "&SELECT";
            this.btnAddNewRow.UseVisualStyleBackColor = false;
            this.btnAddNewRow.Click += new System.EventHandler(this.addNewRowButton_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.BackColor = System.Drawing.Color.DarkCyan;
            this.btnCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCopy.Font = new System.Drawing.Font("Lucida Sans Unicode", 15F, System.Drawing.FontStyle.Bold);
            this.btnCopy.Location = new System.Drawing.Point(672, 336);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(97, 41);
            this.btnCopy.TabIndex = 471;
            this.btnCopy.Text = "&COPY";
            this.btnCopy.UseVisualStyleBackColor = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.BackColor = System.Drawing.Color.DarkCyan;
            this.btnPaste.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPaste.Font = new System.Drawing.Font("Lucida Sans Unicode", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPaste.Location = new System.Drawing.Point(672, 381);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(97, 41);
            this.btnPaste.TabIndex = 472;
            this.btnPaste.Text = "&Paste";
            this.btnPaste.UseVisualStyleBackColor = false;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // frmMenuMaster_Comp
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.ClientSize = new System.Drawing.Size(805, 547);
            this.DoubleBuffered = false;
            this.Name = "frmMenuMaster_Comp";
            this.Load += new System.EventHandler(this.frmMenuMaster_Comp_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnCopy_KeyUp);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnCopy_KeyUp);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal CIS_CLibrary.CIS_CheckBox chkSelectAll;
        internal CIS_CLibrary.CIS_TextLabel lblSelectAll;
        internal System.Windows.Forms.Panel pnlDetail;
        internal CIS_CLibrary.CIS_CheckBox ChkActive;
        public CIS_CLibrary.CIS_Textbox txtCode;
        internal CIS_CLibrary.CIS_TextLabel lblCompColon;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboCompany;
        internal CIS_CLibrary.CIS_TextLabel lblCompany;
        private CIS_CLibrary.CIS_Label lblNotification;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnAddNewRow;
        private System.Windows.Forms.Button btnDown;
    }
}
