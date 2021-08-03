namespace CIS_Textil
{
    partial class frmUpdateUser
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            this.pnlOrderDtls = new System.Windows.Forms.Panel();
            this.fgdtls_f = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnUpdate = new CIS_CLibrary.CIS_Button();
            this.pnlUpdateMtrs = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new CIS_CLibrary.CIS_TextLabel();
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.txtIpAddress = new CIS_CLibrary.CIS_TextLabel();
            this.lblIPAdd = new CIS_CLibrary.CIS_TextLabel();
            this.txtActiveSessions = new CIS_CLibrary.CIS_TextLabel();
            this.lblActiveSessions = new CIS_CLibrary.CIS_TextLabel();
            this.lblUserName = new CIS_CLibrary.CIS_TextLabel();
            this.btnClearSessions = new CIS_CLibrary.CIS_Button();
            this.lblDepartmentFromColon = new CIS_CLibrary.CIS_TextLabel();
            this.cboUserName = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.label2 = new CIS_CLibrary.CIS_TextLabel();
            this.label4 = new CIS_CLibrary.CIS_TextLabel();
            this.ChkActive = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.btnKill = new System.Windows.Forms.Button();
            this.lblHtext = new CIS_CLibrary.CIS_TextLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.pnlOrderDtls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgdtls_f)).BeginInit();
            this.pnlUpdateMtrs.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.lblHtext);
            this.pnlContent.Controls.Add(this.btnKill);
            this.pnlContent.Controls.Add(this.ChkActive);
            this.pnlContent.Controls.Add(this.btnUpdate);
            this.pnlContent.Controls.Add(this.label4);
            this.pnlContent.Controls.Add(this.label2);
            this.pnlContent.Controls.Add(this.lblDepartmentFromColon);
            this.pnlContent.Controls.Add(this.cboUserName);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.Add(this.pnlUpdateMtrs);
            this.pnlContent.Controls.Add(this.btnClearSessions);
            this.pnlContent.Controls.Add(this.txtIpAddress);
            this.pnlContent.Controls.Add(this.lblIPAdd);
            this.pnlContent.Controls.Add(this.txtActiveSessions);
            this.pnlContent.Controls.Add(this.lblActiveSessions);
            this.pnlContent.Controls.Add(this.lblUserName);
            this.pnlContent.Controls.SetChildIndex(this.lblUserName, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblActiveSessions, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtActiveSessions, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblIPAdd, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtIpAddress, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnClearSessions, 0);
            this.pnlContent.Controls.SetChildIndex(this.pnlUpdateMtrs, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboUserName, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblDepartmentFromColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.label2, 0);
            this.pnlContent.Controls.SetChildIndex(this.label4, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnUpdate, 0);
            this.pnlContent.Controls.SetChildIndex(this.ChkActive, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnKill, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblHtext, 0);
            // 
            // pnlOrderDtls
            // 
            this.pnlOrderDtls.BackColor = System.Drawing.Color.LightBlue;
            this.pnlOrderDtls.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlOrderDtls.Controls.Add(this.fgdtls_f);
            this.pnlOrderDtls.Font = new System.Drawing.Font(Db_Detials.ActiveFont, Db_Detials.ActiveFontSize);
            this.pnlOrderDtls.Location = new System.Drawing.Point(-1, 27);
            this.pnlOrderDtls.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlOrderDtls.Name = "pnlOrderDtls";
            this.pnlOrderDtls.Size = new System.Drawing.Size(826, 215);
            this.pnlOrderDtls.TabIndex = 22;
            // 
            // fgdtls_f
            // 
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.fgdtls_f.DisplayLayout.Appearance = appearance1;
            this.fgdtls_f.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded4;
            this.fgdtls_f.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.fgdtls_f.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.fgdtls_f.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.fgdtls_f.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.fgdtls_f.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.fgdtls_f.DisplayLayout.MaxColScrollRegions = 1;
            this.fgdtls_f.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.Color.White;
            appearance5.BackGradientAlignment = Infragistics.Win.GradientAlignment.Client;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.fgdtls_f.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.fgdtls_f.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.fgdtls_f.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.fgdtls_f.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.fgdtls_f.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.fgdtls_f.DisplayLayout.Override.CellAppearance = appearance8;
            this.fgdtls_f.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.fgdtls_f.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.fgdtls_f.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.fgdtls_f.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.fgdtls_f.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.fgdtls_f.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.fgdtls_f.DisplayLayout.Override.RowAppearance = appearance11;
            this.fgdtls_f.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.fgdtls_f.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.fgdtls_f.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.fgdtls_f.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.fgdtls_f.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.fgdtls_f.Dock = System.Windows.Forms.DockStyle.Top;
            this.fgdtls_f.Font = new System.Drawing.Font("Verdana", 8.363437F);
            this.fgdtls_f.Location = new System.Drawing.Point(0, 0);
            this.fgdtls_f.Name = "fgdtls_f";
            this.fgdtls_f.Size = new System.Drawing.Size(824, 138);
            this.fgdtls_f.TabIndex = 3;
            this.fgdtls_f.Text = "UltraGrid1";
            this.fgdtls_f.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.fgdtls_f.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.fgdtls_f_InitializeLayout);
            this.fgdtls_f.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fgdtls_f_KeyDown);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.btnUpdate.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnUpdate.InnerBorderColor = System.Drawing.Color.DarkSlateGray;
            this.btnUpdate.Location = new System.Drawing.Point(225, 131);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(96, 27);
            this.btnUpdate.TabIndex = 3;
            this.btnUpdate.Text = "&Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // pnlUpdateMtrs
            // 
            this.pnlUpdateMtrs.BackColor = System.Drawing.Color.LightBlue;
            this.pnlUpdateMtrs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUpdateMtrs.Controls.Add(this.panel1);
            this.pnlUpdateMtrs.Controls.Add(this.pnlOrderDtls);
            this.pnlUpdateMtrs.Font = new System.Drawing.Font(Db_Detials.ActiveFont, Db_Detials.ActiveFontSize);
            this.pnlUpdateMtrs.Location = new System.Drawing.Point(53, 235);
            this.pnlUpdateMtrs.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlUpdateMtrs.Name = "pnlUpdateMtrs";
            this.pnlUpdateMtrs.Size = new System.Drawing.Size(826, 258);
            this.pnlUpdateMtrs.TabIndex = 90155;
            this.pnlUpdateMtrs.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(818, 22);
            this.panel1.TabIndex = 847;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 14);
            this.label3.TabIndex = 846;
            this.label3.Text = "Update User";
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
            this.txtCode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.HelpText = "";
            this.txtCode.HoldMyText = null;
            this.txtCode.IsMandatory = false;
            this.txtCode.IsSingleQuote = true;
            this.txtCode.IsSysmbol = false;
            this.txtCode.Location = new System.Drawing.Point(9, 3);
            this.txtCode.Mask = null;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(23, 20);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 90156;
            this.txtCode.Visible = false;
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.AutoSize = true;
            this.txtIpAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIpAddress.ForeColor = System.Drawing.Color.Black;
            this.txtIpAddress.Location = new System.Drawing.Point(232, 77);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(81, 17);
            this.txtIpAddress.TabIndex = 90172;
            this.txtIpAddress.Text = "IPAddress";
            // 
            // lblIPAdd
            // 
            this.lblIPAdd.AutoSize = true;
            this.lblIPAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIPAdd.ForeColor = System.Drawing.Color.Black;
            this.lblIPAdd.Location = new System.Drawing.Point(93, 77);
            this.lblIPAdd.Name = "lblIPAdd";
            this.lblIPAdd.Size = new System.Drawing.Size(86, 17);
            this.lblIPAdd.TabIndex = 90171;
            this.lblIPAdd.Text = "IP Address";
            // 
            // txtActiveSessions
            // 
            this.txtActiveSessions.AutoSize = true;
            this.txtActiveSessions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtActiveSessions.ForeColor = System.Drawing.Color.Black;
            this.txtActiveSessions.Location = new System.Drawing.Point(232, 50);
            this.txtActiveSessions.Name = "txtActiveSessions";
            this.txtActiveSessions.Size = new System.Drawing.Size(117, 17);
            this.txtActiveSessions.TabIndex = 90167;
            this.txtActiveSessions.Text = "ActiveSessions";
            // 
            // lblActiveSessions
            // 
            this.lblActiveSessions.AutoSize = true;
            this.lblActiveSessions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActiveSessions.ForeColor = System.Drawing.Color.Black;
            this.lblActiveSessions.Location = new System.Drawing.Point(93, 50);
            this.lblActiveSessions.Name = "lblActiveSessions";
            this.lblActiveSessions.Size = new System.Drawing.Size(122, 17);
            this.lblActiveSessions.TabIndex = 90166;
            this.lblActiveSessions.Text = "Active Sessions";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.ForeColor = System.Drawing.Color.Black;
            this.lblUserName.Location = new System.Drawing.Point(93, 23);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(83, 17);
            this.lblUserName.TabIndex = 90165;
            this.lblUserName.Text = "UserName";
            // 
            // btnClearSessions
            // 
            this.btnClearSessions.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.btnClearSessions.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnClearSessions.InnerBorderColor = System.Drawing.Color.DarkSlateGray;
            this.btnClearSessions.Location = new System.Drawing.Point(334, 130);
            this.btnClearSessions.Name = "btnClearSessions";
            this.btnClearSessions.Size = new System.Drawing.Size(113, 28);
            this.btnClearSessions.TabIndex = 4;
            this.btnClearSessions.Text = "Clear Sessions";
            this.btnClearSessions.UseVisualStyleBackColor = false;
            this.btnClearSessions.Visible = false;
            this.btnClearSessions.Click += new System.EventHandler(this.btnClearSessions_Click);
            // 
            // lblDepartmentFromColon
            // 
            this.lblDepartmentFromColon.AutoSize = true;
            this.lblDepartmentFromColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepartmentFromColon.Location = new System.Drawing.Point(216, 25);
            this.lblDepartmentFromColon.Name = "lblDepartmentFromColon";
            this.lblDepartmentFromColon.Size = new System.Drawing.Size(12, 14);
            this.lblDepartmentFromColon.TabIndex = 90176;
            this.lblDepartmentFromColon.Text = ":";
            // 
            // cboUserName
            // 
            this.cboUserName.AutoComplete = false;
            this.cboUserName.AutoDropdown = false;
            this.cboUserName.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboUserName.BackColorEven = System.Drawing.Color.White;
            this.cboUserName.BackColorOdd = System.Drawing.Color.White;
            this.cboUserName.ColumnNames = "";
            this.cboUserName.ColumnWidthDefault = 75;
            this.cboUserName.ColumnWidths = "";
            this.cboUserName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboUserName.Fill_ComboID = 0;
            this.cboUserName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.cboUserName.FormattingEnabled = true;
            this.cboUserName.GroupType = 0;
            this.cboUserName.HelpText = "Select UserName";
            this.cboUserName.IsMandatory = true;
            this.cboUserName.LinkedColumnIndex = 0;
            this.cboUserName.LinkedTextBox = null;
            this.cboUserName.Location = new System.Drawing.Point(232, 22);
            this.cboUserName.Name = "cboUserName";
            this.cboUserName.NameOfControl = "UserName";
            this.cboUserName.OpenForm = null;
            this.cboUserName.ShowBallonTip = false;
            this.cboUserName.Size = new System.Drawing.Size(438, 22);
            this.cboUserName.TabIndex = 1;
            this.cboUserName.SelectedIndexChanged += new System.EventHandler(this.cboUserName_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(216, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 14);
            this.label2.TabIndex = 90177;
            this.label2.Text = ":";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(216, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 14);
            this.label4.TabIndex = 90178;
            this.label4.Text = ":";
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
            this.ChkActive.Location = new System.Drawing.Point(232, 106);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.Size = new System.Drawing.Size(89, 18);
            this.ChkActive.TabIndex = 2;
            this.ChkActive.Text = "Logged In";
            this.ChkActive.UseVisualStyleBackColor = false;
            // 
            // btnKill
            // 
            this.btnKill.BackColor = System.Drawing.Color.Transparent;
            this.btnKill.BackgroundImage = global::CIS_Textil.Properties.Resources.Kill_logo;
            this.btnKill.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnKill.Location = new System.Drawing.Point(814, 22);
            this.btnKill.Name = "btnKill";
            this.btnKill.Size = new System.Drawing.Size(94, 85);
            this.btnKill.TabIndex = 90179;
            this.btnKill.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnKill.UseVisualStyleBackColor = false;
            this.btnKill.MouseLeave += new System.EventHandler(this.btnKill_MouseLeave);
            this.btnKill.MouseHover += new System.EventHandler(this.btnKill_MouseHover);
            // 
            // lblHtext
            // 
            this.lblHtext.AutoSize = true;
            this.lblHtext.BackColor = System.Drawing.Color.Transparent;
            this.lblHtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHtext.ForeColor = System.Drawing.Color.Black;
            this.lblHtext.Location = new System.Drawing.Point(796, 110);
            this.lblHtext.Name = "lblHtext";
            this.lblHtext.Size = new System.Drawing.Size(138, 39);
            this.lblHtext.TabIndex = 90180;
            this.lblHtext.Text = "This Will Kill \r\nThe Running Software \r\nof Selected User";
            this.lblHtext.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmUpdateUser
            // 
            this.ClientSize = new System.Drawing.Size(955, 547);
            this.Name = "frmUpdateUser";
            this.Load += new System.EventHandler(this.frmUpdateUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlOrderDtls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fgdtls_f)).EndInit();
            this.pnlUpdateMtrs.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Panel pnlUpdateMtrs;
        private System.Windows.Forms.Panel panel1;
        internal CIS_CLibrary.CIS_TextLabel label3;
        internal System.Windows.Forms.Panel pnlOrderDtls;
        internal Infragistics.Win.UltraWinGrid.UltraGrid fgdtls_f;
        internal CIS_CLibrary.CIS_Button btnUpdate;
        public CIS_CLibrary.CIS_Textbox txtCode;
        internal CIS_CLibrary.CIS_TextLabel txtIpAddress;
        internal CIS_CLibrary.CIS_TextLabel lblIPAdd;
        internal CIS_CLibrary.CIS_TextLabel txtActiveSessions;
        internal CIS_CLibrary.CIS_TextLabel lblActiveSessions;
        internal CIS_CLibrary.CIS_TextLabel lblUserName;
        internal CIS_CLibrary.CIS_Button btnClearSessions;
        internal CIS_CLibrary.CIS_TextLabel lblDepartmentFromColon;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboUserName;
        internal CIS_CLibrary.CIS_TextLabel label4;
        internal CIS_CLibrary.CIS_TextLabel label2;
        internal CIS_CLibrary.CIS_CheckBox ChkActive;
        private System.Windows.Forms.Button btnKill;
        internal CIS_CLibrary.CIS_TextLabel lblHtext;
    }
}
