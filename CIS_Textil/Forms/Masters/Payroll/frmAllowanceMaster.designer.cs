namespace CIS_Textil
{
    partial class frmAllowanceMaster
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
            CIS_CLibrary.ToolTip.StringDataProvider stringDataProvider1 = new CIS_CLibrary.ToolTip.StringDataProvider();
            this.lblallowance = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lbltype = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblamount = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblpaysheet = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblpresentdays = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.chkpresentdays = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.lblForm16Head = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.rdballowance = new CIS_CLibrary.CIS_RadioButton();
            this.rdbdeduction = new CIS_CLibrary.CIS_RadioButton();
            this.rdbnone = new CIS_CLibrary.CIS_RadioButton();
            this.txtallowanceColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblForm16HeadColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lbltypeColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblpaysheetColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblamountColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblpresentdaysColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.ChkActive = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.tltOnControls = new CIS_CLibrary.ToolTip.CIS_ToolTip();
            this.txtallowance = new CIS_CLibrary.CIS_Textbox();
            this.txtamount = new CIS_CLibrary.CIS_Textbox();
            this.txtpaysheet = new CIS_CLibrary.CIS_Textbox();
            this.ddltype = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.txtAliasName = new CIS_CLibrary.CIS_Textbox();
            this.lblAliasName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblShortCodeColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblEI1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblEI1Colon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.cboEI1 = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblEI2 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.cboEI2 = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblEI2Colon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtET3 = new CIS_CLibrary.CIS_Textbox();
            this.lblET3Colon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtET2 = new CIS_CLibrary.CIS_Textbox();
            this.lblET2Colon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblET3 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblET2 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtET1 = new CIS_CLibrary.CIS_Textbox();
            this.lblET1Colon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblET1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.lblEI1);
            this.pnlContent.Controls.Add(this.txtAliasName);
            this.pnlContent.Controls.Add(this.lblEI1Colon);
            this.pnlContent.Controls.Add(this.lblAliasName);
            this.pnlContent.Controls.Add(this.cboEI1);
            this.pnlContent.Controls.Add(this.lblEI2);
            this.pnlContent.Controls.Add(this.lblShortCodeColon);
            this.pnlContent.Controls.Add(this.cboEI2);
            this.pnlContent.Controls.Add(this.ddltype);
            this.pnlContent.Controls.Add(this.lblEI2Colon);
            this.pnlContent.Controls.Add(this.txtpaysheet);
            this.pnlContent.Controls.Add(this.txtET3);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.Add(this.lblET3Colon);
            this.pnlContent.Controls.Add(this.txtamount);
            this.pnlContent.Controls.Add(this.txtET2);
            this.pnlContent.Controls.Add(this.ChkActive);
            this.pnlContent.Controls.Add(this.lblET2Colon);
            this.pnlContent.Controls.Add(this.txtallowance);
            this.pnlContent.Controls.Add(this.lblET3);
            this.pnlContent.Controls.Add(this.lblpaysheetColon);
            this.pnlContent.Controls.Add(this.lblET2);
            this.pnlContent.Controls.Add(this.lblamountColon);
            this.pnlContent.Controls.Add(this.txtET1);
            this.pnlContent.Controls.Add(this.lbltypeColon);
            this.pnlContent.Controls.Add(this.lblET1Colon);
            this.pnlContent.Controls.Add(this.lblForm16HeadColon);
            this.pnlContent.Controls.Add(this.lblET1);
            this.pnlContent.Controls.Add(this.txtallowanceColon);
            this.pnlContent.Controls.Add(this.rdballowance);
            this.pnlContent.Controls.Add(this.lblpresentdaysColon);
            this.pnlContent.Controls.Add(this.lblForm16Head);
            this.pnlContent.Controls.Add(this.lblallowance);
            this.pnlContent.Controls.Add(this.lblpaysheet);
            this.pnlContent.Controls.Add(this.chkpresentdays);
            this.pnlContent.Controls.Add(this.lblamount);
            this.pnlContent.Controls.Add(this.lblpresentdays);
            this.pnlContent.Controls.Add(this.lbltype);
            this.pnlContent.Controls.Add(this.rdbnone);
            this.pnlContent.Controls.Add(this.rdbdeduction);
            this.pnlContent.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tltOnControls.SetToolTip(this.pnlContent, "");
            this.pnlContent.Controls.SetChildIndex(this.rdbdeduction, 0);
            this.pnlContent.Controls.SetChildIndex(this.rdbnone, 0);
            this.pnlContent.Controls.SetChildIndex(this.lbltype, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblpresentdays, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblamount, 0);
            this.pnlContent.Controls.SetChildIndex(this.chkpresentdays, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblpaysheet, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblallowance, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblForm16Head, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblpresentdaysColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.rdballowance, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtallowanceColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET1, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblForm16HeadColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET1Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lbltypeColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtET1, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblamountColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET2, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblpaysheetColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET3, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtallowance, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET2Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.ChkActive, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtET2, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtamount, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET3Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtET3, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtpaysheet, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEI2Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.ddltype, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboEI2, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblShortCodeColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEI2, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboEI1, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblAliasName, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEI1Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtAliasName, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEI1, 0);
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
            // lblallowance
            // 
            this.lblallowance.AutoSize = true;
            this.lblallowance.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblallowance.Location = new System.Drawing.Point(48, 16);
            this.lblallowance.Moveable = false;
            this.lblallowance.Name = "lblallowance";
            this.lblallowance.NameOfControl = null;
            this.lblallowance.Size = new System.Drawing.Size(117, 14);
            this.lblallowance.TabIndex = 0;
            this.lblallowance.Text = "Allowance Name";
            this.tltOnControls.SetToolTip(this.lblallowance, "");
            // 
            // lbltype
            // 
            this.lbltype.AutoSize = true;
            this.lbltype.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltype.Location = new System.Drawing.Point(48, 41);
            this.lbltype.Moveable = false;
            this.lbltype.Name = "lbltype";
            this.lbltype.NameOfControl = null;
            this.lbltype.Size = new System.Drawing.Size(39, 14);
            this.lbltype.TabIndex = 4;
            this.lbltype.Text = "Type";
            this.tltOnControls.SetToolTip(this.lbltype, "");
            // 
            // lblamount
            // 
            this.lblamount.AutoSize = true;
            this.lblamount.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblamount.Location = new System.Drawing.Point(492, 40);
            this.lblamount.Moveable = false;
            this.lblamount.Name = "lblamount";
            this.lblamount.NameOfControl = null;
            this.lblamount.Size = new System.Drawing.Size(57, 14);
            this.lblamount.TabIndex = 6;
            this.lblamount.Text = "Amount";
            this.tltOnControls.SetToolTip(this.lblamount, "");
            // 
            // lblpaysheet
            // 
            this.lblpaysheet.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblpaysheet.AutoSize = true;
            this.lblpaysheet.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpaysheet.Location = new System.Drawing.Point(48, 67);
            this.lblpaysheet.Moveable = false;
            this.lblpaysheet.Name = "lblpaysheet";
            this.lblpaysheet.NameOfControl = null;
            this.lblpaysheet.Size = new System.Drawing.Size(155, 14);
            this.lblpaysheet.TabIndex = 8;
            this.lblpaysheet.Text = "Order No. In Paysheet";
            this.tltOnControls.SetToolTip(this.lblpaysheet, "");
            // 
            // lblpresentdays
            // 
            this.lblpresentdays.AutoSize = true;
            this.lblpresentdays.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpresentdays.Location = new System.Drawing.Point(492, 65);
            this.lblpresentdays.Moveable = false;
            this.lblpresentdays.Name = "lblpresentdays";
            this.lblpresentdays.NameOfControl = null;
            this.lblpresentdays.Size = new System.Drawing.Size(180, 13);
            this.lblpresentdays.TabIndex = 10;
            this.lblpresentdays.Text = "Calc. Amt as Per Present Days";
            this.tltOnControls.SetToolTip(this.lblpresentdays, "");
            // 
            // chkpresentdays
            // 
            this.chkpresentdays.AutoSize = true;
            this.chkpresentdays.BackColor = System.Drawing.Color.MintCream;
            this.chkpresentdays.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkpresentdays.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.chkpresentdays.HelpText = "Check If Amt As Per Present Days";
            this.chkpresentdays.Location = new System.Drawing.Point(688, 66);
            this.chkpresentdays.Moveable = false;
            this.chkpresentdays.Name = "chkpresentdays";
            this.chkpresentdays.NameOfControl = null;
            this.chkpresentdays.Size = new System.Drawing.Size(12, 11);
            this.chkpresentdays.TabIndex = 6;
            this.tltOnControls.SetToolTip(this.chkpresentdays, "");
            this.chkpresentdays.UseVisualStyleBackColor = true;
            // 
            // lblForm16Head
            // 
            this.lblForm16Head.AutoSize = true;
            this.lblForm16Head.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForm16Head.Location = new System.Drawing.Point(48, 92);
            this.lblForm16Head.Moveable = false;
            this.lblForm16Head.Name = "lblForm16Head";
            this.lblForm16Head.NameOfControl = null;
            this.lblForm16Head.Size = new System.Drawing.Size(113, 14);
            this.lblForm16Head.TabIndex = 12;
            this.lblForm16Head.Text = "Form 16 Head   ";
            this.tltOnControls.SetToolTip(this.lblForm16Head, "");
            // 
            // rdballowance
            // 
            this.rdballowance.AutoSize = true;
            this.rdballowance.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdballowance.HelpText = "Check If Allowance U/S 10";
            this.rdballowance.Location = new System.Drawing.Point(238, 92);
            this.rdballowance.Moveable = false;
            this.rdballowance.Name = "rdballowance";
            this.rdballowance.Size = new System.Drawing.Size(154, 18);
            this.rdballowance.TabIndex = 7;
            this.rdballowance.TabStop = true;
            this.rdballowance.Text = "Allowance U/S 10  ";
            this.tltOnControls.SetToolTip(this.rdballowance, "");
            this.rdballowance.UseVisualStyleBackColor = true;
            // 
            // rdbdeduction
            // 
            this.rdbdeduction.AutoSize = true;
            this.rdbdeduction.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbdeduction.HelpText = "Check If Deduction U/S 10";
            this.rdbdeduction.Location = new System.Drawing.Point(395, 92);
            this.rdbdeduction.Moveable = false;
            this.rdbdeduction.Name = "rdbdeduction";
            this.rdbdeduction.Size = new System.Drawing.Size(158, 18);
            this.rdbdeduction.TabIndex = 8;
            this.rdbdeduction.TabStop = true;
            this.rdbdeduction.Text = "U/S 10 - Deduction ";
            this.tltOnControls.SetToolTip(this.rdbdeduction, "");
            this.rdbdeduction.UseVisualStyleBackColor = true;
            // 
            // rdbnone
            // 
            this.rdbnone.AutoSize = true;
            this.rdbnone.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rdbnone.HelpText = "Check If None";
            this.rdbnone.Location = new System.Drawing.Point(557, 92);
            this.rdbnone.Moveable = false;
            this.rdbnone.Name = "rdbnone";
            this.rdbnone.Size = new System.Drawing.Size(71, 18);
            this.rdbnone.TabIndex = 9;
            this.rdbnone.TabStop = true;
            this.rdbnone.Text = "None   ";
            this.tltOnControls.SetToolTip(this.rdbnone, "");
            this.rdbnone.UseVisualStyleBackColor = true;
            // 
            // txtallowanceColon
            // 
            this.txtallowanceColon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtallowanceColon.AutoSize = true;
            this.txtallowanceColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtallowanceColon.Location = new System.Drawing.Point(223, 16);
            this.txtallowanceColon.Moveable = false;
            this.txtallowanceColon.Name = "txtallowanceColon";
            this.txtallowanceColon.NameOfControl = null;
            this.txtallowanceColon.Size = new System.Drawing.Size(12, 14);
            this.txtallowanceColon.TabIndex = 16;
            this.txtallowanceColon.Text = ":";
            this.tltOnControls.SetToolTip(this.txtallowanceColon, "");
            // 
            // lblForm16HeadColon
            // 
            this.lblForm16HeadColon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblForm16HeadColon.AutoSize = true;
            this.lblForm16HeadColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblForm16HeadColon.Location = new System.Drawing.Point(223, 92);
            this.lblForm16HeadColon.Moveable = false;
            this.lblForm16HeadColon.Name = "lblForm16HeadColon";
            this.lblForm16HeadColon.NameOfControl = null;
            this.lblForm16HeadColon.Size = new System.Drawing.Size(12, 14);
            this.lblForm16HeadColon.TabIndex = 17;
            this.lblForm16HeadColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblForm16HeadColon, "");
            // 
            // lbltypeColon
            // 
            this.lbltypeColon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbltypeColon.AutoSize = true;
            this.lbltypeColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltypeColon.Location = new System.Drawing.Point(223, 41);
            this.lbltypeColon.Moveable = false;
            this.lbltypeColon.Name = "lbltypeColon";
            this.lbltypeColon.NameOfControl = null;
            this.lbltypeColon.Size = new System.Drawing.Size(12, 14);
            this.lbltypeColon.TabIndex = 18;
            this.lbltypeColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lbltypeColon, "");
            // 
            // lblpaysheetColon
            // 
            this.lblpaysheetColon.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblpaysheetColon.AutoSize = true;
            this.lblpaysheetColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpaysheetColon.Location = new System.Drawing.Point(223, 66);
            this.lblpaysheetColon.Moveable = false;
            this.lblpaysheetColon.Name = "lblpaysheetColon";
            this.lblpaysheetColon.NameOfControl = null;
            this.lblpaysheetColon.Size = new System.Drawing.Size(12, 14);
            this.lblpaysheetColon.TabIndex = 19;
            this.lblpaysheetColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblpaysheetColon, "");
            // 
            // lblamountColon
            // 
            this.lblamountColon.AutoSize = true;
            this.lblamountColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblamountColon.Location = new System.Drawing.Point(673, 41);
            this.lblamountColon.Moveable = false;
            this.lblamountColon.Name = "lblamountColon";
            this.lblamountColon.NameOfControl = null;
            this.lblamountColon.Size = new System.Drawing.Size(12, 14);
            this.lblamountColon.TabIndex = 21;
            this.lblamountColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblamountColon, "");
            // 
            // lblpresentdaysColon
            // 
            this.lblpresentdaysColon.AutoSize = true;
            this.lblpresentdaysColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblpresentdaysColon.Location = new System.Drawing.Point(673, 63);
            this.lblpresentdaysColon.Moveable = false;
            this.lblpresentdaysColon.Name = "lblpresentdaysColon";
            this.lblpresentdaysColon.NameOfControl = null;
            this.lblpresentdaysColon.Size = new System.Drawing.Size(12, 14);
            this.lblpresentdaysColon.TabIndex = 22;
            this.lblpresentdaysColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblpresentdaysColon, "");
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
            this.txtCode.Size = new System.Drawing.Size(25, 22);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 0;
            this.tltOnControls.SetToolTip(this.txtCode, "");
            this.txtCode.Visible = false;
            // 
            // ChkActive
            // 
            this.ChkActive.AutoSize = true;
            this.ChkActive.BackColor = System.Drawing.Color.MintCream;
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChkActive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.HelpText = "Checked If Active";
            this.ChkActive.Location = new System.Drawing.Point(238, 117);
            this.ChkActive.Moveable = false;
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.NameOfControl = null;
            this.ChkActive.Size = new System.Drawing.Size(110, 18);
            this.ChkActive.TabIndex = 10;
            this.ChkActive.Text = "Active Status";
            this.tltOnControls.SetToolTip(this.ChkActive, "");
            this.ChkActive.UseVisualStyleBackColor = false;
            // 
            // tltOnControls
            // 
            this.tltOnControls.DataProvider = stringDataProvider1;
            this.tltOnControls.LoadText = "";
            this.tltOnControls.ShowToolTip = false;
            // 
            // txtallowance
            // 
            this.txtallowance.AutoFillDate = false;
            this.txtallowance.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtallowance.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtallowance.CheckForSymbol = null;
            this.txtallowance.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtallowance.DecimalPlace = 0;
            this.txtallowance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtallowance.HelpText = "Enter Allowance Name";
            this.txtallowance.HoldMyText = null;
            this.txtallowance.IsMandatory = true;
            this.txtallowance.IsSingleQuote = true;
            this.txtallowance.IsSysmbol = false;
            this.txtallowance.Location = new System.Drawing.Point(238, 13);
            this.txtallowance.Mask = null;
            this.txtallowance.MaxLength = 50;
            this.txtallowance.Moveable = false;
            this.txtallowance.Name = "txtallowance";
            this.txtallowance.NameOfControl = "Allowance";
            this.txtallowance.Prefix = null;
            this.txtallowance.ShowBallonTip = false;
            this.txtallowance.ShowErrorIcon = false;
            this.txtallowance.ShowMessage = null;
            this.txtallowance.Size = new System.Drawing.Size(201, 21);
            this.txtallowance.Suffix = null;
            this.txtallowance.TabIndex = 1;
            this.tltOnControls.SetToolTip(this.txtallowance, "");
            this.txtallowance.Leave += new System.EventHandler(this.txtallowance_Leave);
            // 
            // txtamount
            // 
            this.txtamount.AutoFillDate = false;
            this.txtamount.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtamount.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtamount.CheckForSymbol = null;
            this.txtamount.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtamount.DecimalPlace = 0;
            this.txtamount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtamount.HelpText = "Enter Amount";
            this.txtamount.HoldMyText = null;
            this.txtamount.IsMandatory = true;
            this.txtamount.IsSingleQuote = true;
            this.txtamount.IsSysmbol = false;
            this.txtamount.Location = new System.Drawing.Point(688, 39);
            this.txtamount.Mask = null;
            this.txtamount.MaxLength = 10;
            this.txtamount.Moveable = false;
            this.txtamount.Name = "txtamount";
            this.txtamount.NameOfControl = "Amount";
            this.txtamount.Prefix = null;
            this.txtamount.ShowBallonTip = false;
            this.txtamount.ShowErrorIcon = false;
            this.txtamount.ShowMessage = null;
            this.txtamount.Size = new System.Drawing.Size(83, 21);
            this.txtamount.Suffix = null;
            this.txtamount.TabIndex = 4;
            this.tltOnControls.SetToolTip(this.txtamount, "");
            // 
            // txtpaysheet
            // 
            this.txtpaysheet.AutoFillDate = false;
            this.txtpaysheet.BackColor = System.Drawing.Color.White;
            this.txtpaysheet.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtpaysheet.CheckForSymbol = null;
            this.txtpaysheet.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtpaysheet.DecimalPlace = 0;
            this.txtpaysheet.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtpaysheet.HelpText = "Enter Order No. In Paysheet";
            this.txtpaysheet.HoldMyText = null;
            this.txtpaysheet.IsMandatory = false;
            this.txtpaysheet.IsSingleQuote = true;
            this.txtpaysheet.IsSysmbol = false;
            this.txtpaysheet.Location = new System.Drawing.Point(238, 64);
            this.txtpaysheet.Mask = null;
            this.txtpaysheet.MaxLength = 10;
            this.txtpaysheet.Moveable = false;
            this.txtpaysheet.Name = "txtpaysheet";
            this.txtpaysheet.NameOfControl = "Order No. In Paysheet";
            this.txtpaysheet.Prefix = null;
            this.txtpaysheet.ShowBallonTip = false;
            this.txtpaysheet.ShowErrorIcon = false;
            this.txtpaysheet.ShowMessage = null;
            this.txtpaysheet.Size = new System.Drawing.Size(57, 21);
            this.txtpaysheet.Suffix = null;
            this.txtpaysheet.TabIndex = 5;
            this.tltOnControls.SetToolTip(this.txtpaysheet, "");
            // 
            // ddltype
            // 
            this.ddltype.AutoComplete = false;
            this.ddltype.AutoDropdown = false;
            this.ddltype.BackColor = System.Drawing.Color.PapayaWhip;
            this.ddltype.BackColorEven = System.Drawing.Color.White;
            this.ddltype.BackColorOdd = System.Drawing.Color.White;
            this.ddltype.ColumnNames = "";
            this.ddltype.ColumnWidthDefault = 175;
            this.ddltype.ColumnWidths = "";
            this.ddltype.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ddltype.Fill_ComboID = 0;
            this.ddltype.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddltype.FormattingEnabled = true;
            this.ddltype.GroupType = 0;
            this.ddltype.HelpText = "Select Type";
            this.ddltype.IsMandatory = false;
            this.ddltype.LinkedColumnIndex = 0;
            this.ddltype.LinkedTextBox = null;
            this.ddltype.Location = new System.Drawing.Point(238, 38);
            this.ddltype.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ddltype.Moveable = false;
            this.ddltype.Name = "ddltype";
            this.ddltype.NameOfControl = "Type";
            this.ddltype.OpenForm = null;
            this.ddltype.ShowBallonTip = false;
            this.ddltype.Size = new System.Drawing.Size(201, 22);
            this.ddltype.TabIndex = 3;
            this.tltOnControls.SetToolTip(this.ddltype, "");
            // 
            // txtAliasName
            // 
            this.txtAliasName.AutoFillDate = false;
            this.txtAliasName.BackColor = System.Drawing.Color.White;
            this.txtAliasName.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtAliasName.CheckForSymbol = null;
            this.txtAliasName.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtAliasName.DecimalPlace = 0;
            this.txtAliasName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAliasName.HelpText = "Enter Alias Name";
            this.txtAliasName.HoldMyText = null;
            this.txtAliasName.IsMandatory = false;
            this.txtAliasName.IsSingleQuote = true;
            this.txtAliasName.IsSysmbol = false;
            this.txtAliasName.Location = new System.Drawing.Point(688, 14);
            this.txtAliasName.Mask = null;
            this.txtAliasName.MaxLength = 50;
            this.txtAliasName.Moveable = false;
            this.txtAliasName.Name = "txtAliasName";
            this.txtAliasName.NameOfControl = "Alias name";
            this.txtAliasName.Prefix = null;
            this.txtAliasName.ShowBallonTip = false;
            this.txtAliasName.ShowErrorIcon = false;
            this.txtAliasName.ShowMessage = null;
            this.txtAliasName.Size = new System.Drawing.Size(83, 21);
            this.txtAliasName.Suffix = null;
            this.txtAliasName.TabIndex = 2;
            this.tltOnControls.SetToolTip(this.txtAliasName, "");
            this.txtAliasName.Leave += new System.EventHandler(this.txtAliasName_Leave);
            // 
            // lblAliasName
            // 
            this.lblAliasName.AutoSize = true;
            this.lblAliasName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAliasName.Location = new System.Drawing.Point(492, 16);
            this.lblAliasName.Moveable = false;
            this.lblAliasName.Name = "lblAliasName";
            this.lblAliasName.NameOfControl = null;
            this.lblAliasName.Size = new System.Drawing.Size(81, 14);
            this.lblAliasName.TabIndex = 23;
            this.lblAliasName.Text = "Alias Name";
            this.tltOnControls.SetToolTip(this.lblAliasName, "");
            // 
            // lblShortCodeColon
            // 
            this.lblShortCodeColon.AutoSize = true;
            this.lblShortCodeColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShortCodeColon.Location = new System.Drawing.Point(673, 16);
            this.lblShortCodeColon.Moveable = false;
            this.lblShortCodeColon.Name = "lblShortCodeColon";
            this.lblShortCodeColon.NameOfControl = null;
            this.lblShortCodeColon.Size = new System.Drawing.Size(12, 14);
            this.lblShortCodeColon.TabIndex = 25;
            this.lblShortCodeColon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblShortCodeColon, "");
            // 
            // lblEI1
            // 
            this.lblEI1.AutoSize = true;
            this.lblEI1.BackColor = System.Drawing.Color.Transparent;
            this.lblEI1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI1.Location = new System.Drawing.Point(136, 143);
            this.lblEI1.Moveable = false;
            this.lblEI1.Name = "lblEI1";
            this.lblEI1.NameOfControl = "EI1";
            this.lblEI1.Size = new System.Drawing.Size(30, 14);
            this.lblEI1.TabIndex = 1309;
            this.lblEI1.Text = "EI1";
            this.tltOnControls.SetToolTip(this.lblEI1, "");
            this.lblEI1.Visible = false;
            // 
            // lblEI1Colon
            // 
            this.lblEI1Colon.AutoSize = true;
            this.lblEI1Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblEI1Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI1Colon.Location = new System.Drawing.Point(222, 143);
            this.lblEI1Colon.Moveable = false;
            this.lblEI1Colon.Name = "lblEI1Colon";
            this.lblEI1Colon.NameOfControl = "EI1";
            this.lblEI1Colon.Size = new System.Drawing.Size(12, 14);
            this.lblEI1Colon.TabIndex = 1310;
            this.lblEI1Colon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblEI1Colon, "");
            this.lblEI1Colon.Visible = false;
            // 
            // cboEI1
            // 
            this.cboEI1.AutoComplete = false;
            this.cboEI1.AutoDropdown = false;
            this.cboEI1.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboEI1.BackColorEven = System.Drawing.Color.White;
            this.cboEI1.BackColorOdd = System.Drawing.Color.White;
            this.cboEI1.ColumnNames = "";
            this.cboEI1.ColumnWidthDefault = 175;
            this.cboEI1.ColumnWidths = "";
            this.cboEI1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboEI1.Fill_ComboID = 0;
            this.cboEI1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEI1.FormattingEnabled = true;
            this.cboEI1.GroupType = 0;
            this.cboEI1.HelpText = "Select EI1";
            this.cboEI1.IsMandatory = true;
            this.cboEI1.LinkedColumnIndex = 0;
            this.cboEI1.LinkedTextBox = null;
            this.cboEI1.Location = new System.Drawing.Point(239, 139);
            this.cboEI1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboEI1.Moveable = false;
            this.cboEI1.Name = "cboEI1";
            this.cboEI1.NameOfControl = "EI1";
            this.cboEI1.OpenForm = null;
            this.cboEI1.ShowBallonTip = false;
            this.cboEI1.Size = new System.Drawing.Size(231, 23);
            this.cboEI1.TabIndex = 1308;
            this.tltOnControls.SetToolTip(this.cboEI1, "");
            this.cboEI1.Visible = false;
            // 
            // lblEI2
            // 
            this.lblEI2.AutoSize = true;
            this.lblEI2.BackColor = System.Drawing.Color.Transparent;
            this.lblEI2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI2.Location = new System.Drawing.Point(136, 167);
            this.lblEI2.Moveable = false;
            this.lblEI2.Name = "lblEI2";
            this.lblEI2.NameOfControl = "EI2";
            this.lblEI2.Size = new System.Drawing.Size(30, 14);
            this.lblEI2.TabIndex = 1306;
            this.lblEI2.Text = "EI2";
            this.tltOnControls.SetToolTip(this.lblEI2, "");
            this.lblEI2.Visible = false;
            // 
            // cboEI2
            // 
            this.cboEI2.AutoComplete = true;
            this.cboEI2.AutoDropdown = true;
            this.cboEI2.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboEI2.BackColorEven = System.Drawing.Color.White;
            this.cboEI2.BackColorOdd = System.Drawing.Color.White;
            this.cboEI2.ColumnNames = "";
            this.cboEI2.ColumnWidthDefault = 175;
            this.cboEI2.ColumnWidths = "";
            this.cboEI2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboEI2.Fill_ComboID = 0;
            this.cboEI2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.cboEI2.FormattingEnabled = true;
            this.cboEI2.GroupType = 0;
            this.cboEI2.HelpText = "Select EI2";
            this.cboEI2.IsMandatory = true;
            this.cboEI2.LinkedColumnIndex = 0;
            this.cboEI2.LinkedTextBox = null;
            this.cboEI2.Location = new System.Drawing.Point(239, 165);
            this.cboEI2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboEI2.Moveable = false;
            this.cboEI2.Name = "cboEI2";
            this.cboEI2.NameOfControl = "EI2";
            this.cboEI2.OpenForm = null;
            this.cboEI2.ShowBallonTip = false;
            this.cboEI2.Size = new System.Drawing.Size(231, 23);
            this.cboEI2.TabIndex = 1305;
            this.tltOnControls.SetToolTip(this.cboEI2, "");
            this.cboEI2.Visible = false;
            // 
            // lblEI2Colon
            // 
            this.lblEI2Colon.AutoSize = true;
            this.lblEI2Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblEI2Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI2Colon.Location = new System.Drawing.Point(222, 167);
            this.lblEI2Colon.Moveable = false;
            this.lblEI2Colon.Name = "lblEI2Colon";
            this.lblEI2Colon.NameOfControl = null;
            this.lblEI2Colon.Size = new System.Drawing.Size(12, 14);
            this.lblEI2Colon.TabIndex = 1307;
            this.lblEI2Colon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblEI2Colon, "");
            this.lblEI2Colon.Visible = false;
            // 
            // txtET3
            // 
            this.txtET3.AutoFillDate = false;
            this.txtET3.BackColor = System.Drawing.Color.White;
            this.txtET3.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtET3.CheckForSymbol = null;
            this.txtET3.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtET3.DecimalPlace = 0;
            this.txtET3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtET3.HelpText = "Enter ET3";
            this.txtET3.HoldMyText = null;
            this.txtET3.IsMandatory = false;
            this.txtET3.IsSingleQuote = true;
            this.txtET3.IsSysmbol = false;
            this.txtET3.Location = new System.Drawing.Point(239, 243);
            this.txtET3.Mask = null;
            this.txtET3.MaxLength = 50;
            this.txtET3.Moveable = false;
            this.txtET3.Name = "txtET3";
            this.txtET3.NameOfControl = "ET3";
            this.txtET3.Prefix = null;
            this.txtET3.ShowBallonTip = false;
            this.txtET3.ShowErrorIcon = false;
            this.txtET3.ShowMessage = null;
            this.txtET3.Size = new System.Drawing.Size(231, 22);
            this.txtET3.Suffix = null;
            this.txtET3.TabIndex = 1298;
            this.tltOnControls.SetToolTip(this.txtET3, "");
            this.txtET3.Visible = false;
            // 
            // lblET3Colon
            // 
            this.lblET3Colon.AutoSize = true;
            this.lblET3Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblET3Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET3Colon.Location = new System.Drawing.Point(222, 246);
            this.lblET3Colon.Moveable = false;
            this.lblET3Colon.Name = "lblET3Colon";
            this.lblET3Colon.NameOfControl = null;
            this.lblET3Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET3Colon.TabIndex = 1304;
            this.lblET3Colon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblET3Colon, "");
            this.lblET3Colon.Visible = false;
            // 
            // txtET2
            // 
            this.txtET2.AutoFillDate = false;
            this.txtET2.BackColor = System.Drawing.Color.White;
            this.txtET2.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtET2.CheckForSymbol = null;
            this.txtET2.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtET2.DecimalPlace = 0;
            this.txtET2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtET2.HelpText = "Enter ET2";
            this.txtET2.HoldMyText = null;
            this.txtET2.IsMandatory = false;
            this.txtET2.IsSingleQuote = true;
            this.txtET2.IsSysmbol = false;
            this.txtET2.Location = new System.Drawing.Point(239, 216);
            this.txtET2.Mask = null;
            this.txtET2.MaxLength = 50;
            this.txtET2.Moveable = false;
            this.txtET2.Name = "txtET2";
            this.txtET2.NameOfControl = "ET2";
            this.txtET2.Prefix = null;
            this.txtET2.ShowBallonTip = false;
            this.txtET2.ShowErrorIcon = false;
            this.txtET2.ShowMessage = null;
            this.txtET2.Size = new System.Drawing.Size(231, 22);
            this.txtET2.Suffix = null;
            this.txtET2.TabIndex = 1297;
            this.tltOnControls.SetToolTip(this.txtET2, "");
            this.txtET2.Visible = false;
            // 
            // lblET2Colon
            // 
            this.lblET2Colon.AutoSize = true;
            this.lblET2Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblET2Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET2Colon.Location = new System.Drawing.Point(222, 221);
            this.lblET2Colon.Moveable = false;
            this.lblET2Colon.Name = "lblET2Colon";
            this.lblET2Colon.NameOfControl = "ET2";
            this.lblET2Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET2Colon.TabIndex = 1302;
            this.lblET2Colon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblET2Colon, "");
            this.lblET2Colon.Visible = false;
            // 
            // lblET3
            // 
            this.lblET3.AutoSize = true;
            this.lblET3.BackColor = System.Drawing.Color.Transparent;
            this.lblET3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET3.Location = new System.Drawing.Point(135, 245);
            this.lblET3.Moveable = false;
            this.lblET3.Name = "lblET3";
            this.lblET3.NameOfControl = "ET3";
            this.lblET3.Size = new System.Drawing.Size(32, 14);
            this.lblET3.TabIndex = 1303;
            this.lblET3.Text = "ET3";
            this.tltOnControls.SetToolTip(this.lblET3, "");
            this.lblET3.Visible = false;
            // 
            // lblET2
            // 
            this.lblET2.AutoSize = true;
            this.lblET2.BackColor = System.Drawing.Color.Transparent;
            this.lblET2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET2.Location = new System.Drawing.Point(135, 219);
            this.lblET2.Moveable = false;
            this.lblET2.Name = "lblET2";
            this.lblET2.NameOfControl = "ET2";
            this.lblET2.Size = new System.Drawing.Size(32, 14);
            this.lblET2.TabIndex = 1301;
            this.lblET2.Text = "ET2";
            this.tltOnControls.SetToolTip(this.lblET2, "");
            this.lblET2.Visible = false;
            // 
            // txtET1
            // 
            this.txtET1.AutoFillDate = false;
            this.txtET1.BackColor = System.Drawing.Color.White;
            this.txtET1.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtET1.CheckForSymbol = null;
            this.txtET1.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtET1.DecimalPlace = 0;
            this.txtET1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtET1.HelpText = "Enter ET1";
            this.txtET1.HoldMyText = null;
            this.txtET1.IsMandatory = false;
            this.txtET1.IsSingleQuote = true;
            this.txtET1.IsSysmbol = false;
            this.txtET1.Location = new System.Drawing.Point(239, 191);
            this.txtET1.Mask = null;
            this.txtET1.MaxLength = 50;
            this.txtET1.Moveable = false;
            this.txtET1.Name = "txtET1";
            this.txtET1.NameOfControl = "ET1";
            this.txtET1.Prefix = null;
            this.txtET1.ShowBallonTip = false;
            this.txtET1.ShowErrorIcon = false;
            this.txtET1.ShowMessage = null;
            this.txtET1.Size = new System.Drawing.Size(231, 22);
            this.txtET1.Suffix = null;
            this.txtET1.TabIndex = 1296;
            this.tltOnControls.SetToolTip(this.txtET1, "");
            this.txtET1.Visible = false;
            // 
            // lblET1Colon
            // 
            this.lblET1Colon.AutoSize = true;
            this.lblET1Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblET1Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET1Colon.Location = new System.Drawing.Point(222, 193);
            this.lblET1Colon.Moveable = false;
            this.lblET1Colon.Name = "lblET1Colon";
            this.lblET1Colon.NameOfControl = "ET1";
            this.lblET1Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET1Colon.TabIndex = 1300;
            this.lblET1Colon.Text = ":";
            this.tltOnControls.SetToolTip(this.lblET1Colon, "");
            this.lblET1Colon.Visible = false;
            // 
            // lblET1
            // 
            this.lblET1.AutoSize = true;
            this.lblET1.BackColor = System.Drawing.Color.Transparent;
            this.lblET1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET1.Location = new System.Drawing.Point(135, 193);
            this.lblET1.Moveable = false;
            this.lblET1.Name = "lblET1";
            this.lblET1.NameOfControl = "ET1";
            this.lblET1.Size = new System.Drawing.Size(32, 14);
            this.lblET1.TabIndex = 1299;
            this.lblET1.Text = "ET1";
            this.tltOnControls.SetToolTip(this.lblET1, "");
            this.lblET1.Visible = false;
            // 
            // frmAllowanceMaster
            // 
            this.ClientSize = new System.Drawing.Size(808, 545);
            this.Name = "frmAllowanceMaster";
            this.Load += new System.EventHandler(this.frmAllowanceMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CIS_CLibrary.CIS_TextLabel lblallowance;
        private CIS_CLibrary.CIS_TextLabel lbltype;
        private CIS_CLibrary.CIS_TextLabel lblamount;
        private CIS_CLibrary.CIS_TextLabel lblpaysheet;
        private CIS_CLibrary.CIS_TextLabel lblpresentdays;
        private CIS_CLibrary.CIS_CheckBox chkpresentdays;
        private CIS_CLibrary.CIS_TextLabel lblForm16Head;
        private CIS_CLibrary.CIS_RadioButton rdbnone;
        private  CIS_CLibrary.CIS_RadioButton rdbdeduction;
        private CIS_CLibrary.CIS_RadioButton rdballowance;
        private CIS_CLibrary.CIS_TextLabel lblpresentdaysColon;
        private CIS_CLibrary.CIS_TextLabel lblamountColon;
        private CIS_CLibrary.CIS_TextLabel lblpaysheetColon;
        private CIS_CLibrary.CIS_TextLabel lbltypeColon;
        private CIS_CLibrary.CIS_TextLabel lblForm16HeadColon;
        private CIS_CLibrary.CIS_TextLabel txtallowanceColon;
        public CIS_CLibrary.CIS_Textbox txtCode;
        internal CIS_CLibrary.CIS_CheckBox ChkActive;
        private CIS_CLibrary.ToolTip.CIS_ToolTip tltOnControls;
        private CIS_CLibrary.CIS_Textbox txtallowance;
        private CIS_CLibrary.CIS_Textbox txtpaysheet;
        private CIS_CLibrary.CIS_Textbox txtamount;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox ddltype;
        internal CIS_CLibrary.CIS_Textbox txtAliasName;
        private CIS_CLibrary.CIS_TextLabel lblAliasName;
        private CIS_CLibrary.CIS_TextLabel lblShortCodeColon;
        internal CIS_CLibrary.CIS_TextLabel lblEI1;
        internal CIS_CLibrary.CIS_TextLabel lblEI1Colon;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboEI1;
        internal CIS_CLibrary.CIS_TextLabel lblEI2;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboEI2;
        internal CIS_CLibrary.CIS_TextLabel lblEI2Colon;
        internal CIS_CLibrary.CIS_Textbox txtET3;
        internal CIS_CLibrary.CIS_TextLabel lblET3Colon;
        internal CIS_CLibrary.CIS_Textbox txtET2;
        internal CIS_CLibrary.CIS_TextLabel lblET2Colon;
        internal CIS_CLibrary.CIS_TextLabel lblET3;
        internal CIS_CLibrary.CIS_TextLabel lblET2;
        internal CIS_CLibrary.CIS_Textbox txtET1;
        internal CIS_CLibrary.CIS_TextLabel lblET1Colon;
        internal CIS_CLibrary.CIS_TextLabel lblET1;
    }
}
