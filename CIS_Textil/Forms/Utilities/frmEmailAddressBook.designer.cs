namespace CIS_Textil
{
    partial class frmEmailAddressBook
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        /// 
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdAddressBook = new CIS_DataGridViewEx.DataGridViewEx();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Email = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MobileNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblTop = new CIS_CLibrary.CIS_TextLabel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnMinimize = new System.Windows.Forms.Button();
            this.lblBottom = new CIS_CLibrary.CIS_TextLabel();
            this.btnSelect = new System.Windows.Forms.Button();
            this.lblLeft = new CIS_CLibrary.CIS_TextLabel();
            this.lblRIght = new CIS_CLibrary.CIS_TextLabel();
            this.cboGroup = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.txtSearchText = new CIS_CLibrary.CIS_Textbox();
            this.lblFilter = new CIS_CLibrary.CIS_TextLabel();
            this.lblEmailAcc_R = new CIS_CLibrary.CIS_TextLabel();
            this.cboFromEmailID_R = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblSubject = new CIS_CLibrary.CIS_TextLabel();
            this.txtSubject = new CIS_CLibrary.CIS_Textbox();
            this.btnShow = new System.Windows.Forms.Button();
            this.label2 = new CIS_CLibrary.CIS_TextLabel();
            this.cboToEntryNo = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.label1 = new CIS_CLibrary.CIS_TextLabel();
            this.cboFromEntryNo = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.btnSendSms = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAddressBook)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdAddressBook);
            this.panel1.Location = new System.Drawing.Point(2, 85);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(505, 280);
            this.panel1.TabIndex = 0;
            // 
            // grdAddressBook
            // 
            this.grdAddressBook.AllowUserToAddRows = false;
            this.grdAddressBook.AllowUserToDeleteRows = false;
            this.grdAddressBook.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grdAddressBook.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Purple;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grdAddressBook.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grdAddressBook.ColumnHeadersHeight = 30;
            this.grdAddressBook.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grdAddressBook.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.Name,
            this.No,
            this.Email,
            this.MobileNo,
            this.ID});
            this.grdAddressBook.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.grdAddressBook.EnableHeadersVisualStyles = false;
            this.grdAddressBook.Grid_Tbl = null;
            this.grdAddressBook.Grid_UID = 0;
            this.grdAddressBook.HeaderRowColor = System.Drawing.Color.Empty;
            this.grdAddressBook.IsGroupCell = false;
            this.grdAddressBook.Location = new System.Drawing.Point(1, 1);
            this.grdAddressBook.Name = "grdAddressBook";
            this.grdAddressBook.NameOfControl = null;
            this.grdAddressBook.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.grdAddressBook.RowHeadersVisible = false;
            this.grdAddressBook.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.grdAddressBook.ShowFieldChooser = false;
            this.grdAddressBook.ShowRowErrors = false;
            this.grdAddressBook.Size = new System.Drawing.Size(505, 278);
            this.grdAddressBook.TabIndex = 131;
            // 
            // Select
            // 
            this.Select.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Select.Frozen = true;
            this.Select.HeaderText = "";
            this.Select.MinimumWidth = 20;
            this.Select.Name = "Select";
            this.Select.Width = 20;
            // 
            // Name
            // 
            this.Name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Name.Frozen = true;
            this.Name.HeaderText = "Name";
            this.Name.MinimumWidth = 100;
            this.Name.Name = "Name";
            this.Name.ReadOnly = true;
            this.Name.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Name.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // No
            // 
            this.No.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.No.Frozen = true;
            this.No.HeaderText = "No";
            this.No.MinimumWidth = 100;
            this.No.Name = "No";
            this.No.ReadOnly = true;
            this.No.Width = 101;
            // 
            // Email
            // 
            this.Email.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Email.Frozen = true;
            this.Email.HeaderText = "Email";
            this.Email.MinimumWidth = 180;
            this.Email.Name = "Email";
            this.Email.ReadOnly = true;
            this.Email.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Email.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Email.Width = 180;
            // 
            // MobileNo
            // 
            this.MobileNo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.MobileNo.Frozen = true;
            this.MobileNo.HeaderText = "MobileNo";
            this.MobileNo.MinimumWidth = 103;
            this.MobileNo.Name = "MobileNo";
            this.MobileNo.ReadOnly = true;
            this.MobileNo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MobileNo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MobileNo.Width = 103;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // lblTop
            // 
            this.lblTop.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTop.Location = new System.Drawing.Point(0, 0);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(510, 26);
            this.lblTop.TabIndex = 130;
            this.lblTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTop_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.DarkRed;
            this.btnCancel.Location = new System.Drawing.Point(483, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(23, 22);
            this.btnCancel.TabIndex = 410;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "X";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackColor = System.Drawing.Color.White;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimize.Location = new System.Drawing.Point(457, 2);
            this.btnMinimize.Margin = new System.Windows.Forms.Padding(0);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.Size = new System.Drawing.Size(23, 22);
            this.btnMinimize.TabIndex = 409;
            this.btnMinimize.TabStop = false;
            this.btnMinimize.Text = "---";
            this.btnMinimize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMinimize.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // lblBottom
            // 
            this.lblBottom.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBottom.Location = new System.Drawing.Point(0, 369);
            this.lblBottom.Name = "lblBottom";
            this.lblBottom.Size = new System.Drawing.Size(510, 31);
            this.lblBottom.TabIndex = 411;
            // 
            // btnSelect
            // 
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(408, 374);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(77, 24);
            this.btnSelect.TabIndex = 413;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // lblLeft
            // 
            this.lblLeft.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLeft.Location = new System.Drawing.Point(0, 26);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(3, 343);
            this.lblLeft.TabIndex = 414;
            // 
            // lblRIght
            // 
            this.lblRIght.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblRIght.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblRIght.Location = new System.Drawing.Point(507, 26);
            this.lblRIght.Name = "lblRIght";
            this.lblRIght.Size = new System.Drawing.Size(3, 343);
            this.lblRIght.TabIndex = 415;
            // 
            // cboGroup
            // 
            this.cboGroup.AutoComplete = false;
            this.cboGroup.AutoDropdown = false;
            this.cboGroup.BackColor = System.Drawing.Color.White;
            this.cboGroup.BackColorEven = System.Drawing.Color.Lavender;
            this.cboGroup.BackColorOdd = System.Drawing.Color.White;
            this.cboGroup.ColumnNames = "";
            this.cboGroup.ColumnWidthDefault = 150;
            this.cboGroup.ColumnWidths = "";
            this.cboGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboGroup.Fill_ComboID = 0;
            this.cboGroup.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboGroup.FormattingEnabled = true;
            this.cboGroup.GroupType = 0;
            this.cboGroup.HelpText = null;
            this.cboGroup.IsMandatory = false;
            this.cboGroup.LinkedColumnIndex = 0;
            this.cboGroup.LinkedTextBox = null;
            this.cboGroup.Location = new System.Drawing.Point(61, 29);
            this.cboGroup.Name = "cboGroup";
            this.cboGroup.NameOfControl = null;
            this.cboGroup.OpenForm = null;
            this.cboGroup.ShowBallonTip = false;
            this.cboGroup.Size = new System.Drawing.Size(189, 21);
            this.cboGroup.TabIndex = 416;
            this.cboGroup.Visible = false;
            // 
            // txtSearchText
            // 
            this.txtSearchText.AutoFillDate = false;
            this.txtSearchText.BackColor = System.Drawing.Color.White;
            this.txtSearchText.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Lower;
            this.txtSearchText.CheckForSymbol = null;
            this.txtSearchText.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtSearchText.DecimalPlace = 0;
            this.txtSearchText.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearchText.HelpText = "";
            this.txtSearchText.HoldMyText = null;
            this.txtSearchText.IsMandatory = false;
            this.txtSearchText.IsSingleQuote = true;
            this.txtSearchText.IsSysmbol = false;
            this.txtSearchText.Location = new System.Drawing.Point(255, 30);
            this.txtSearchText.Mask = null;
            this.txtSearchText.Name = "txtSearchText";
            this.txtSearchText.NameOfControl = null;
            this.txtSearchText.Prefix = null;
            this.txtSearchText.ShowBallonTip = false;
            this.txtSearchText.ShowErrorIcon = false;
            this.txtSearchText.ShowMessage = null;
            this.txtSearchText.Size = new System.Drawing.Size(246, 20);
            this.txtSearchText.Suffix = null;
            this.txtSearchText.TabIndex = 417;
            this.txtSearchText.Visible = false;
            this.txtSearchText.TextChanged += new System.EventHandler(this.txtSearchText_TextChanged);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilter.Location = new System.Drawing.Point(3, 33);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(47, 13);
            this.lblFilter.TabIndex = 418;
            this.lblFilter.Text = "Filter : ";
            this.lblFilter.Visible = false;
            // 
            // lblEmailAcc_R
            // 
            this.lblEmailAcc_R.AutoSize = true;
            this.lblEmailAcc_R.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblEmailAcc_R.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmailAcc_R.ForeColor = System.Drawing.Color.White;
            this.lblEmailAcc_R.Location = new System.Drawing.Point(15, 377);
            this.lblEmailAcc_R.Name = "lblEmailAcc_R";
            this.lblEmailAcc_R.Size = new System.Drawing.Size(92, 13);
            this.lblEmailAcc_R.TabIndex = 421;
            this.lblEmailAcc_R.Text = "Email Account:";
            // 
            // cboFromEmailID_R
            // 
            this.cboFromEmailID_R.AutoComplete = false;
            this.cboFromEmailID_R.AutoDropdown = false;
            this.cboFromEmailID_R.BackColor = System.Drawing.Color.White;
            this.cboFromEmailID_R.BackColorEven = System.Drawing.Color.Lavender;
            this.cboFromEmailID_R.BackColorOdd = System.Drawing.Color.White;
            this.cboFromEmailID_R.ColumnNames = "";
            this.cboFromEmailID_R.ColumnWidthDefault = 150;
            this.cboFromEmailID_R.ColumnWidths = "";
            this.cboFromEmailID_R.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboFromEmailID_R.Fill_ComboID = 0;
            this.cboFromEmailID_R.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFromEmailID_R.FormattingEnabled = true;
            this.cboFromEmailID_R.GroupType = 0;
            this.cboFromEmailID_R.HelpText = null;
            this.cboFromEmailID_R.IsMandatory = false;
            this.cboFromEmailID_R.LinkedColumnIndex = 0;
            this.cboFromEmailID_R.LinkedTextBox = null;
            this.cboFromEmailID_R.Location = new System.Drawing.Point(113, 374);
            this.cboFromEmailID_R.Name = "cboFromEmailID_R";
            this.cboFromEmailID_R.NameOfControl = null;
            this.cboFromEmailID_R.OpenForm = null;
            this.cboFromEmailID_R.ShowBallonTip = false;
            this.cboFromEmailID_R.Size = new System.Drawing.Size(280, 21);
            this.cboFromEmailID_R.TabIndex = 420;
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubject.Location = new System.Drawing.Point(6, 61);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(94, 13);
            this.lblSubject.TabIndex = 437;
            this.lblSubject.Text = "Subject          :";
            // 
            // txtSubject
            // 
            this.txtSubject.AutoFillDate = false;
            this.txtSubject.BackColor = System.Drawing.Color.White;
            this.txtSubject.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Lower;
            this.txtSubject.CheckForSymbol = null;
            this.txtSubject.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtSubject.DecimalPlace = 0;
            this.txtSubject.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubject.HelpText = "";
            this.txtSubject.HoldMyText = null;
            this.txtSubject.IsMandatory = false;
            this.txtSubject.IsSingleQuote = true;
            this.txtSubject.IsSysmbol = false;
            this.txtSubject.Location = new System.Drawing.Point(106, 58);
            this.txtSubject.Mask = null;
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.NameOfControl = null;
            this.txtSubject.Prefix = null;
            this.txtSubject.ShowBallonTip = false;
            this.txtSubject.ShowErrorIcon = false;
            this.txtSubject.ShowMessage = null;
            this.txtSubject.Size = new System.Drawing.Size(395, 20);
            this.txtSubject.Suffix = null;
            this.txtSubject.TabIndex = 436;
            // 
            // btnShow
            // 
            this.btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShow.Location = new System.Drawing.Point(408, 31);
            this.btnShow.Name = "btnShow";
            this.btnShow.Size = new System.Drawing.Size(93, 23);
            this.btnShow.TabIndex = 435;
            this.btnShow.Text = "Show";
            this.btnShow.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(218, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 434;
            this.label2.Text = "Entry No To :";
            // 
            // cboToEntryNo
            // 
            this.cboToEntryNo.AutoComplete = false;
            this.cboToEntryNo.AutoDropdown = false;
            this.cboToEntryNo.BackColor = System.Drawing.Color.White;
            this.cboToEntryNo.BackColorEven = System.Drawing.Color.Lavender;
            this.cboToEntryNo.BackColorOdd = System.Drawing.Color.White;
            this.cboToEntryNo.ColumnNames = "";
            this.cboToEntryNo.ColumnWidthDefault = 150;
            this.cboToEntryNo.ColumnWidths = "";
            this.cboToEntryNo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboToEntryNo.Fill_ComboID = 0;
            this.cboToEntryNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboToEntryNo.FormattingEnabled = true;
            this.cboToEntryNo.GroupType = 0;
            this.cboToEntryNo.HelpText = null;
            this.cboToEntryNo.IsMandatory = false;
            this.cboToEntryNo.LinkedColumnIndex = 0;
            this.cboToEntryNo.LinkedTextBox = null;
            this.cboToEntryNo.Location = new System.Drawing.Point(301, 32);
            this.cboToEntryNo.Name = "cboToEntryNo";
            this.cboToEntryNo.NameOfControl = null;
            this.cboToEntryNo.OpenForm = null;
            this.cboToEntryNo.ShowBallonTip = false;
            this.cboToEntryNo.Size = new System.Drawing.Size(104, 21);
            this.cboToEntryNo.TabIndex = 433;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 13);
            this.label1.TabIndex = 432;
            this.label1.Text = "Entry No From :";
            // 
            // cboFromEntryNo
            // 
            this.cboFromEntryNo.AutoComplete = false;
            this.cboFromEntryNo.AutoDropdown = false;
            this.cboFromEntryNo.BackColor = System.Drawing.Color.White;
            this.cboFromEntryNo.BackColorEven = System.Drawing.Color.Lavender;
            this.cboFromEntryNo.BackColorOdd = System.Drawing.Color.White;
            this.cboFromEntryNo.ColumnNames = "";
            this.cboFromEntryNo.ColumnWidthDefault = 150;
            this.cboFromEntryNo.ColumnWidths = "";
            this.cboFromEntryNo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboFromEntryNo.Fill_ComboID = 0;
            this.cboFromEntryNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFromEntryNo.FormattingEnabled = true;
            this.cboFromEntryNo.GroupType = 0;
            this.cboFromEntryNo.HelpText = null;
            this.cboFromEntryNo.IsMandatory = false;
            this.cboFromEntryNo.LinkedColumnIndex = 0;
            this.cboFromEntryNo.LinkedTextBox = null;
            this.cboFromEntryNo.Location = new System.Drawing.Point(105, 32);
            this.cboFromEntryNo.Name = "cboFromEntryNo";
            this.cboFromEntryNo.NameOfControl = null;
            this.cboFromEntryNo.OpenForm = null;
            this.cboFromEntryNo.ShowBallonTip = false;
            this.cboFromEntryNo.Size = new System.Drawing.Size(106, 21);
            this.cboFromEntryNo.TabIndex = 431;
            // 
            // btnSendSms
            // 
            this.btnSendSms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendSms.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendSms.Location = new System.Drawing.Point(400, 374);
            this.btnSendSms.Name = "btnSendSms";
            this.btnSendSms.Size = new System.Drawing.Size(99, 23);
            this.btnSendSms.TabIndex = 438;
            this.btnSendSms.Text = "Send Sms";
            this.btnSendSms.UseVisualStyleBackColor = true;
            this.btnSendSms.Click += new System.EventHandler(this.btnSendSMS_Click);
            // 
            // frmEmailAddressBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 400);
            this.ControlBox = false;
            this.Controls.Add(this.btnSendSms);
            this.Controls.Add(this.lblSubject);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.btnShow);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboToEntryNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cboFromEntryNo);
            this.Controls.Add(this.cboFromEmailID_R);
            this.Controls.Add(this.lblEmailAcc_R);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.txtSearchText);
            this.Controls.Add(this.cboGroup);
            this.Controls.Add(this.lblRIght);
            this.Controls.Add(this.lblLeft);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnMinimize);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.lblTop);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblBottom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmEmailAddressBook_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAddressBook)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.Panel panel1;
        private CIS_CLibrary.CIS_TextLabel lblTop;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnMinimize;
        private CIS_DataGridViewEx.DataGridViewEx grdAddressBook;
        private CIS_CLibrary.CIS_TextLabel lblBottom;
        private System.Windows.Forms.Button btnSelect;
        private CIS_CLibrary.CIS_TextLabel lblLeft;
        private CIS_CLibrary.CIS_TextLabel lblRIght;
        private CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboGroup;
        private CIS_CLibrary.CIS_Textbox txtSearchText;
        private CIS_CLibrary.CIS_TextLabel lblFilter;
        private CIS_CLibrary.CIS_TextLabel lblEmailAcc_R;
        private CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboFromEmailID_R;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn No;
        private System.Windows.Forms.DataGridViewTextBoxColumn Email;
        private System.Windows.Forms.DataGridViewTextBoxColumn MobileNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private CIS_CLibrary.CIS_TextLabel lblSubject;
        private CIS_CLibrary.CIS_Textbox txtSubject;
        private System.Windows.Forms.Button btnShow;
        private CIS_CLibrary.CIS_TextLabel label2;
        private CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboToEntryNo;
        private CIS_CLibrary.CIS_TextLabel label1;
        private CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboFromEntryNo;
        private System.Windows.Forms.Button btnSendSms;
    }
}