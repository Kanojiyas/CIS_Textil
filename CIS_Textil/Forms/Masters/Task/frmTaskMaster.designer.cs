namespace CIS_Textil
{
    partial class frmTaskMaster
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
            CIS_CLibrary.ToolTip.StringDataProvider stringDataProvider2 = new CIS_CLibrary.ToolTip.StringDataProvider();
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.tltOnControls = new CIS_CLibrary.ToolTip.CIS_ToolTip();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.lblCheckListColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblChecklistNameColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblCheckListType = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.cboTaskType = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.ChkActive = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.lblCheckListName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtTaskName = new CIS_CLibrary.CIS_Textbox();
            this.label1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtAliasName = new CIS_CLibrary.CIS_Textbox();
            this.label25 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.GrdMain = new Crocus_CClib.DataGridViewX();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.pnlDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.pnlDetail);
            this.pnlContent.Controls.Add(this.label1);
            this.pnlContent.Controls.Add(this.txtAliasName);
            this.pnlContent.Controls.Add(this.label25);
            this.pnlContent.Controls.Add(this.lblCheckListColon);
            this.pnlContent.Controls.Add(this.lblChecklistNameColon);
            this.pnlContent.Controls.Add(this.lblCheckListType);
            this.pnlContent.Controls.Add(this.cboTaskType);
            this.pnlContent.Controls.Add(this.ChkActive);
            this.pnlContent.Controls.Add(this.lblCheckListName);
            this.pnlContent.Controls.Add(this.txtTaskName);
            this.pnlContent.Controls.Add(this.txtCode);
            this.tltOnControls.SetToolTip(this.pnlContent, "");
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtTaskName, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblCheckListName, 0);
            this.pnlContent.Controls.SetChildIndex(this.ChkActive, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboTaskType, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblCheckListType, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblChecklistNameColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblCheckListColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.label25, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtAliasName, 0);
            this.pnlContent.Controls.SetChildIndex(this.label1, 0);
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
            this.txtCode.Location = new System.Drawing.Point(2, -1);
            this.txtCode.Mask = null;
            this.txtCode.Moveable = false;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(35, 22);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 1017;
            this.tltOnControls.SetToolTip(this.txtCode, "");
            this.txtCode.Visible = false;
            // 
            // tltOnControls
            // 
            this.tltOnControls.DataProvider = stringDataProvider2;
            this.tltOnControls.LoadText = "";
            this.tltOnControls.ShowToolTip = false;
            // 
            // pnlDetail
            // 
            this.pnlDetail.BackColor = System.Drawing.Color.LightSkyBlue;
            this.pnlDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDetail.Controls.Add(this.GrdMain);
            this.pnlDetail.Location = new System.Drawing.Point(139, 117);
            this.pnlDetail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(562, 271);
            this.pnlDetail.TabIndex = 4;
            this.tltOnControls.SetToolTip(this.pnlDetail, "");
            // 
            // lblCheckListColon
            // 
            this.lblCheckListColon.AutoSize = true;
            this.lblCheckListColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckListColon.Location = new System.Drawing.Point(293, 19);
            this.lblCheckListColon.Moveable = false;
            this.lblCheckListColon.Name = "lblCheckListColon";
            this.lblCheckListColon.NameOfControl = null;
            this.lblCheckListColon.Size = new System.Drawing.Size(12, 14);
            this.lblCheckListColon.TabIndex = 1032;
            this.lblCheckListColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblCheckListColon, "");
            // 
            // lblChecklistNameColon
            // 
            this.lblChecklistNameColon.AutoSize = true;
            this.lblChecklistNameColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChecklistNameColon.Location = new System.Drawing.Point(293, 44);
            this.lblChecklistNameColon.Moveable = false;
            this.lblChecklistNameColon.Name = "lblChecklistNameColon";
            this.lblChecklistNameColon.NameOfControl = null;
            this.lblChecklistNameColon.Size = new System.Drawing.Size(12, 14);
            this.lblChecklistNameColon.TabIndex = 1031;
            this.lblChecklistNameColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblChecklistNameColon, "");
            // 
            // lblCheckListType
            // 
            this.lblCheckListType.AutoSize = true;
            this.lblCheckListType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckListType.Location = new System.Drawing.Point(195, 20);
            this.lblCheckListType.Moveable = false;
            this.lblCheckListType.Name = "lblCheckListType";
            this.lblCheckListType.NameOfControl = null;
            this.lblCheckListType.Size = new System.Drawing.Size(74, 14);
            this.lblCheckListType.TabIndex = 1030;
            this.lblCheckListType.Text = "Task Type";
            this.tltOnControls.SetToolTip(this.lblCheckListType, "");
            // 
            // cboTaskType
            // 
            this.cboTaskType.AutoComplete = false;
            this.cboTaskType.AutoDropdown = false;
            this.cboTaskType.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboTaskType.BackColorEven = System.Drawing.Color.White;
            this.cboTaskType.BackColorOdd = System.Drawing.Color.White;
            this.cboTaskType.ColumnNames = "";
            this.cboTaskType.ColumnWidthDefault = 175;
            this.cboTaskType.ColumnWidths = "";
            this.cboTaskType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboTaskType.Fill_ComboID = 0;
            this.cboTaskType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.cboTaskType.FormattingEnabled = true;
            this.cboTaskType.GroupType = 0;
            this.cboTaskType.HelpText = "Select Task Type";
            this.cboTaskType.IsMandatory = true;
            this.cboTaskType.LinkedColumnIndex = 0;
            this.cboTaskType.LinkedTextBox = null;
            this.cboTaskType.Location = new System.Drawing.Point(307, 15);
            this.cboTaskType.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboTaskType.Moveable = false;
            this.cboTaskType.Name = "cboTaskType";
            this.cboTaskType.NameOfControl = "Task Type";
            this.cboTaskType.OpenForm = null;
            this.cboTaskType.ShowBallonTip = false;
            this.cboTaskType.Size = new System.Drawing.Size(311, 23);
            this.cboTaskType.TabIndex = 1;
            this.tltOnControls.SetToolTip(this.cboTaskType, "");
            // 
            // ChkActive
            // 
            this.ChkActive.AutoSize = true;
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChkActive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.HelpText = "Checked If True";
            this.ChkActive.Location = new System.Drawing.Point(307, 93);
            this.ChkActive.Moveable = false;
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.NameOfControl = null;
            this.ChkActive.Size = new System.Drawing.Size(110, 18);
            this.ChkActive.TabIndex = 4;
            this.ChkActive.Text = "Active Status";
            this.tltOnControls.SetToolTip(this.ChkActive, "");
            this.ChkActive.UseVisualStyleBackColor = false;
            // 
            // lblCheckListName
            // 
            this.lblCheckListName.AutoSize = true;
            this.lblCheckListName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckListName.Location = new System.Drawing.Point(195, 45);
            this.lblCheckListName.Moveable = false;
            this.lblCheckListName.Name = "lblCheckListName";
            this.lblCheckListName.NameOfControl = null;
            this.lblCheckListName.Size = new System.Drawing.Size(80, 14);
            this.lblCheckListName.TabIndex = 1029;
            this.lblCheckListName.Text = "Task Name";
            this.tltOnControls.SetToolTip(this.lblCheckListName, "");
            // 
            // txtTaskName
            // 
            this.txtTaskName.AutoFillDate = false;
            this.txtTaskName.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtTaskName.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtTaskName.CheckForSymbol = null;
            this.txtTaskName.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtTaskName.DecimalPlace = 0;
            this.txtTaskName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtTaskName.HelpText = "Enter Task Name";
            this.txtTaskName.HoldMyText = null;
            this.txtTaskName.IsMandatory = true;
            this.txtTaskName.IsSingleQuote = true;
            this.txtTaskName.IsSysmbol = false;
            this.txtTaskName.Location = new System.Drawing.Point(307, 41);
            this.txtTaskName.Mask = null;
            this.txtTaskName.MaxLength = 50;
            this.txtTaskName.Moveable = false;
            this.txtTaskName.Name = "txtTaskName";
            this.txtTaskName.NameOfControl = "Task name";
            this.txtTaskName.Prefix = null;
            this.txtTaskName.ShowBallonTip = false;
            this.txtTaskName.ShowErrorIcon = false;
            this.txtTaskName.ShowMessage = null;
            this.txtTaskName.Size = new System.Drawing.Size(311, 22);
            this.txtTaskName.Suffix = null;
            this.txtTaskName.TabIndex = 2;
            this.tltOnControls.SetToolTip(this.txtTaskName, "");
            this.txtTaskName.Leave += new System.EventHandler(this.txtTaskName_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(293, 70);
            this.label1.Moveable = false;
            this.label1.Name = "label1";
            this.label1.NameOfControl = null;
            this.label1.Size = new System.Drawing.Size(12, 14);
            this.label1.TabIndex = 1166;
            this.label1.Text = ":";
            this.tltOnControls.SetToolTip(this.label1, "");
            // 
            // txtAliasName
            // 
            this.txtAliasName.AutoFillDate = false;
            this.txtAliasName.BackColor = System.Drawing.Color.White;
            this.txtAliasName.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtAliasName.CheckForSymbol = null;
            this.txtAliasName.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtAliasName.DecimalPlace = 0;
            this.txtAliasName.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAliasName.HelpText = "Enter Alias Name";
            this.txtAliasName.HoldMyText = null;
            this.txtAliasName.IsMandatory = false;
            this.txtAliasName.IsSingleQuote = true;
            this.txtAliasName.IsSysmbol = false;
            this.txtAliasName.Location = new System.Drawing.Point(307, 66);
            this.txtAliasName.Mask = null;
            this.txtAliasName.MaxLength = 50;
            this.txtAliasName.Moveable = false;
            this.txtAliasName.Name = "txtAliasName";
            this.txtAliasName.NameOfControl = "Alias name";
            this.txtAliasName.Prefix = null;
            this.txtAliasName.ShowBallonTip = false;
            this.txtAliasName.ShowErrorIcon = false;
            this.txtAliasName.ShowMessage = null;
            this.txtAliasName.Size = new System.Drawing.Size(311, 21);
            this.txtAliasName.Suffix = null;
            this.txtAliasName.TabIndex = 3;
            this.tltOnControls.SetToolTip(this.txtAliasName, "");
            this.txtAliasName.Leave += new System.EventHandler(this.txtAliasName_Leave);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(195, 70);
            this.label25.Moveable = false;
            this.label25.Name = "label25";
            this.label25.NameOfControl = null;
            this.label25.Size = new System.Drawing.Size(81, 14);
            this.label25.TabIndex = 1165;
            this.label25.Text = "Alias Name";
            this.tltOnControls.SetToolTip(this.label25, "");
            // 
            // GrdMain
            // 
            this.GrdMain.blnFormAction = 0;
            this.GrdMain.CompID = 0;
            this.GrdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrdMain.Location = new System.Drawing.Point(0, 0);
            this.GrdMain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.GrdMain.Name = "GrdMain";
            this.GrdMain.Size = new System.Drawing.Size(560, 269);
            this.GrdMain.TabIndex = 90155;
            this.tltOnControls.SetToolTip(this.GrdMain, "");
            this.GrdMain.YearID = 0;
            // 
            // frmTaskMaster
            // 
            this.ClientSize = new System.Drawing.Size(808, 545);
            this.Name = "frmTaskMaster";
            this.Load += new System.EventHandler(this.frmTaskMaster_Load);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlDetail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public CIS_CLibrary.CIS_Textbox txtCode;
        private CIS_CLibrary.ToolTip.CIS_ToolTip tltOnControls;
        internal System.Windows.Forms.Panel pnlDetail;
        internal CIS_CLibrary.CIS_TextLabel lblCheckListColon;
        internal CIS_CLibrary.CIS_TextLabel lblChecklistNameColon;
        internal CIS_CLibrary.CIS_TextLabel lblCheckListType;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboTaskType;
        internal CIS_CLibrary.CIS_CheckBox ChkActive;
        internal CIS_CLibrary.CIS_TextLabel lblCheckListName;
        internal CIS_CLibrary.CIS_Textbox txtTaskName;
        internal CIS_CLibrary.CIS_TextLabel label1;
        internal CIS_CLibrary.CIS_Textbox txtAliasName;
        internal CIS_CLibrary.CIS_TextLabel label25;
        private Crocus_CClib.DataGridViewX GrdMain;
    }
}
