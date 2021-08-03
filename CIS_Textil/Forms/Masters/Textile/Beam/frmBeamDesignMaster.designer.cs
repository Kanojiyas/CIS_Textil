namespace CIS_Textil
{
    partial class frmBeamDesignMaster
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
            CIS_CLibrary.ToolTip.StringDataProvider stringDataProvider5 = new CIS_CLibrary.ToolTip.StringDataProvider();
            this.TxtTotWts = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.LblTotaWeight = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtOrderNo = new CIS_CLibrary.CIS_Textbox();
            this.lblOrderNoColun = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblOrderNo = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.GrdMain = new Crocus_CClib.DataGridViewX();
            this.lblBeamDesignNameColun = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblWidthColun = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtWidth = new CIS_CLibrary.CIS_Textbox();
            this.txtBeamDesign = new CIS_CLibrary.CIS_Textbox();
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.lblWidth = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblBeamDesignName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.ChkActive = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.tltOnControls = new CIS_CLibrary.ToolTip.CIS_ToolTip();
            this.label1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtAliasName = new CIS_CLibrary.CIS_Textbox();
            this.label2 = new CIS_CLibrary.CIS_TextLabel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.pnlDetail.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.label1);
            this.pnlContent.Controls.Add(this.txtAliasName);
            this.pnlContent.Controls.Add(this.label2);
            this.pnlContent.Controls.Add(this.ChkActive);
            this.pnlContent.Controls.Add(this.TxtTotWts);
            this.pnlContent.Controls.Add(this.LblTotaWeight);
            this.pnlContent.Controls.Add(this.txtOrderNo);
            this.pnlContent.Controls.Add(this.lblOrderNoColun);
            this.pnlContent.Controls.Add(this.lblOrderNo);
            this.pnlContent.Controls.Add(this.pnlDetail);
            this.pnlContent.Controls.Add(this.lblBeamDesignNameColun);
            this.pnlContent.Controls.Add(this.lblWidthColun);
            this.pnlContent.Controls.Add(this.txtWidth);
            this.pnlContent.Controls.Add(this.txtBeamDesign);
            this.pnlContent.Controls.Add(this.lblWidth);
            this.pnlContent.Controls.Add(this.lblBeamDesignName);
            this.pnlContent.Controls.Add(this.txtCode);
            this.tltOnControls.SetToolTip(this.pnlContent, "");
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblBeamDesignName, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblWidth, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtBeamDesign, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtWidth, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblWidthColun, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblBeamDesignNameColun, 0);
            this.pnlContent.Controls.SetChildIndex(this.pnlDetail, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblOrderNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblOrderNoColun, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtOrderNo, 0);
            this.pnlContent.Controls.SetChildIndex(this.LblTotaWeight, 0);
            this.pnlContent.Controls.SetChildIndex(this.TxtTotWts, 0);
            this.pnlContent.Controls.SetChildIndex(this.ChkActive, 0);
            this.pnlContent.Controls.SetChildIndex(this.label2, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtAliasName, 0);
            this.pnlContent.Controls.SetChildIndex(this.label1, 0);
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
            // TxtTotWts
            // 
            this.TxtTotWts.AutoSize = true;
            this.TxtTotWts.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTotWts.ForeColor = System.Drawing.Color.Brown;
            this.TxtTotWts.Location = new System.Drawing.Point(724, 421);
            this.TxtTotWts.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TxtTotWts.Moveable = false;
            this.TxtTotWts.Name = "TxtTotWts";
            this.TxtTotWts.NameOfControl = null;
            this.TxtTotWts.Size = new System.Drawing.Size(47, 14);
            this.TxtTotWts.TabIndex = 7;
            this.TxtTotWts.Text = "0.000";
            this.tltOnControls.SetToolTip(this.TxtTotWts, "");
            // 
            // LblTotaWeight
            // 
            this.LblTotaWeight.AutoSize = true;
            this.LblTotaWeight.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblTotaWeight.Location = new System.Drawing.Point(624, 421);
            this.LblTotaWeight.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LblTotaWeight.Moveable = false;
            this.LblTotaWeight.Name = "LblTotaWeight";
            this.LblTotaWeight.NameOfControl = null;
            this.LblTotaWeight.Size = new System.Drawing.Size(100, 14);
            this.LblTotaWeight.TabIndex = 6;
            this.LblTotaWeight.Text = "Total Weight :";
            this.tltOnControls.SetToolTip(this.LblTotaWeight, "");
            // 
            // txtOrderNo
            // 
            this.txtOrderNo.AutoFillDate = false;
            this.txtOrderNo.BackColor = System.Drawing.Color.White;
            this.txtOrderNo.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtOrderNo.CheckForSymbol = null;
            this.txtOrderNo.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtOrderNo.DecimalPlace = 0;
            this.txtOrderNo.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderNo.HelpText = "Enter Order No";
            this.txtOrderNo.HoldMyText = null;
            this.txtOrderNo.IsMandatory = false;
            this.txtOrderNo.IsSingleQuote = true;
            this.txtOrderNo.IsSysmbol = false;
            this.txtOrderNo.Location = new System.Drawing.Point(147, 40);
            this.txtOrderNo.Mask = null;
            this.txtOrderNo.MaxLength = 20;
            this.txtOrderNo.Moveable = false;
            this.txtOrderNo.Name = "txtOrderNo";
            this.txtOrderNo.NameOfControl = "Order No";
            this.txtOrderNo.Prefix = null;
            this.txtOrderNo.ShowBallonTip = false;
            this.txtOrderNo.ShowErrorIcon = false;
            this.txtOrderNo.ShowMessage = null;
            this.txtOrderNo.Size = new System.Drawing.Size(75, 21);
            this.txtOrderNo.Suffix = null;
            this.txtOrderNo.TabIndex = 3;
            this.txtOrderNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tltOnControls.SetToolTip(this.txtOrderNo, "");
            // 
            // lblOrderNoColun
            // 
            this.lblOrderNoColun.AutoSize = true;
            this.lblOrderNoColun.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNoColun.Location = new System.Drawing.Point(132, 42);
            this.lblOrderNoColun.Moveable = false;
            this.lblOrderNoColun.Name = "lblOrderNoColun";
            this.lblOrderNoColun.NameOfControl = null;
            this.lblOrderNoColun.Size = new System.Drawing.Size(12, 14);
            this.lblOrderNoColun.TabIndex = 1065;
            this.lblOrderNoColun.Text = ":";
            this.tltOnControls.SetToolTip(this.lblOrderNoColun, "");
            // 
            // lblOrderNo
            // 
            this.lblOrderNo.AutoSize = true;
            this.lblOrderNo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNo.Location = new System.Drawing.Point(31, 44);
            this.lblOrderNo.Moveable = false;
            this.lblOrderNo.Name = "lblOrderNo";
            this.lblOrderNo.NameOfControl = null;
            this.lblOrderNo.Size = new System.Drawing.Size(68, 14);
            this.lblOrderNo.TabIndex = 1064;
            this.lblOrderNo.Text = "Order No";
            this.tltOnControls.SetToolTip(this.lblOrderNo, "");
            // 
            // pnlDetail
            // 
            this.pnlDetail.BackColor = System.Drawing.Color.LightBlue;
            this.pnlDetail.Controls.Add(this.GrdMain);
            this.pnlDetail.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlDetail.Location = new System.Drawing.Point(34, 84);
            this.pnlDetail.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Size = new System.Drawing.Size(741, 334);
            this.pnlDetail.TabIndex = 6;
            this.tltOnControls.SetToolTip(this.pnlDetail, "");
            // 
            // GrdMain
            // 
            this.GrdMain.blnFormAction = 0;
            this.GrdMain.CompID = 0;
            this.GrdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GrdMain.Location = new System.Drawing.Point(0, 0);
            this.GrdMain.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.GrdMain.Name = "GrdMain";
            this.GrdMain.Size = new System.Drawing.Size(741, 334);
            this.GrdMain.TabIndex = 90156;
            this.tltOnControls.SetToolTip(this.GrdMain, "");
            this.GrdMain.YearID = 0;
            // 
            // lblBeamDesignNameColun
            // 
            this.lblBeamDesignNameColun.AutoSize = true;
            this.lblBeamDesignNameColun.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBeamDesignNameColun.Location = new System.Drawing.Point(132, 17);
            this.lblBeamDesignNameColun.Moveable = false;
            this.lblBeamDesignNameColun.Name = "lblBeamDesignNameColun";
            this.lblBeamDesignNameColun.NameOfControl = null;
            this.lblBeamDesignNameColun.Size = new System.Drawing.Size(12, 14);
            this.lblBeamDesignNameColun.TabIndex = 1061;
            this.lblBeamDesignNameColun.Text = ":";
            this.tltOnControls.SetToolTip(this.lblBeamDesignNameColun, "");
            // 
            // lblWidthColun
            // 
            this.lblWidthColun.AutoSize = true;
            this.lblWidthColun.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWidthColun.Location = new System.Drawing.Point(323, 45);
            this.lblWidthColun.Moveable = false;
            this.lblWidthColun.Name = "lblWidthColun";
            this.lblWidthColun.NameOfControl = null;
            this.lblWidthColun.Size = new System.Drawing.Size(12, 14);
            this.lblWidthColun.TabIndex = 3;
            this.lblWidthColun.Text = ":";
            this.tltOnControls.SetToolTip(this.lblWidthColun, "");
            this.lblWidthColun.Visible = false;
            // 
            // txtWidth
            // 
            this.txtWidth.AutoFillDate = false;
            this.txtWidth.BackColor = System.Drawing.Color.White;
            this.txtWidth.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtWidth.CheckForSymbol = null;
            this.txtWidth.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.NumericWithDecimal;
            this.txtWidth.DecimalPlace = 2;
            this.txtWidth.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWidth.HelpText = "Enter Width";
            this.txtWidth.HoldMyText = null;
            this.txtWidth.IsMandatory = false;
            this.txtWidth.IsSingleQuote = true;
            this.txtWidth.IsSysmbol = false;
            this.txtWidth.Location = new System.Drawing.Point(336, 42);
            this.txtWidth.Mask = null;
            this.txtWidth.MaxLength = 7;
            this.txtWidth.Moveable = false;
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.NameOfControl = "Width";
            this.txtWidth.Prefix = null;
            this.txtWidth.ShowBallonTip = false;
            this.txtWidth.ShowErrorIcon = false;
            this.txtWidth.ShowMessage = null;
            this.txtWidth.Size = new System.Drawing.Size(50, 21);
            this.txtWidth.Suffix = null;
            this.txtWidth.TabIndex = 4;
            this.txtWidth.Text = "0.00";
            this.txtWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tltOnControls.SetToolTip(this.txtWidth, "");
            this.txtWidth.Visible = false;
            // 
            // txtBeamDesign
            // 
            this.txtBeamDesign.AutoFillDate = false;
            this.txtBeamDesign.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtBeamDesign.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtBeamDesign.CheckForSymbol = null;
            this.txtBeamDesign.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtBeamDesign.DecimalPlace = 0;
            this.txtBeamDesign.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBeamDesign.HelpText = "Enter Beam Design";
            this.txtBeamDesign.HoldMyText = null;
            this.txtBeamDesign.IsMandatory = true;
            this.txtBeamDesign.IsSingleQuote = true;
            this.txtBeamDesign.IsSysmbol = false;
            this.txtBeamDesign.Location = new System.Drawing.Point(147, 15);
            this.txtBeamDesign.Mask = null;
            this.txtBeamDesign.MaxLength = 50;
            this.txtBeamDesign.Moveable = false;
            this.txtBeamDesign.Name = "txtBeamDesign";
            this.txtBeamDesign.NameOfControl = "Beam Design ";
            this.txtBeamDesign.Prefix = null;
            this.txtBeamDesign.ShowBallonTip = false;
            this.txtBeamDesign.ShowErrorIcon = false;
            this.txtBeamDesign.ShowMessage = null;
            this.txtBeamDesign.Size = new System.Drawing.Size(239, 21);
            this.txtBeamDesign.Suffix = null;
            this.txtBeamDesign.TabIndex = 1;
            this.tltOnControls.SetToolTip(this.txtBeamDesign, "");
            this.txtBeamDesign.Leave += new System.EventHandler(this.txtBeamDesign_Leave);
            // 
            // txtCode
            // 
            this.txtCode.AutoFillDate = false;
            this.txtCode.BackColor = System.Drawing.Color.White;
            this.txtCode.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtCode.CheckForSymbol = null;
            this.txtCode.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtCode.DecimalPlace = 0;
            this.txtCode.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.HelpText = "";
            this.txtCode.HoldMyText = null;
            this.txtCode.IsMandatory = false;
            this.txtCode.IsSingleQuote = true;
            this.txtCode.IsSysmbol = false;
            this.txtCode.Location = new System.Drawing.Point(6, 3);
            this.txtCode.Mask = null;
            this.txtCode.Moveable = false;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(25, 21);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 0;
            this.tltOnControls.SetToolTip(this.txtCode, "");
            this.txtCode.Visible = false;
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWidth.Location = new System.Drawing.Point(274, 44);
            this.lblWidth.Moveable = false;
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.NameOfControl = null;
            this.lblWidth.Size = new System.Drawing.Size(50, 14);
            this.lblWidth.TabIndex = 1059;
            this.lblWidth.Text = "Width ";
            this.tltOnControls.SetToolTip(this.lblWidth, "");
            this.lblWidth.Visible = false;
            // 
            // lblBeamDesignName
            // 
            this.lblBeamDesignName.AutoSize = true;
            this.lblBeamDesignName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBeamDesignName.Location = new System.Drawing.Point(31, 17);
            this.lblBeamDesignName.Moveable = false;
            this.lblBeamDesignName.Name = "lblBeamDesignName";
            this.lblBeamDesignName.NameOfControl = null;
            this.lblBeamDesignName.Size = new System.Drawing.Size(93, 14);
            this.lblBeamDesignName.TabIndex = 1057;
            this.lblBeamDesignName.Text = "Beam Design";
            this.tltOnControls.SetToolTip(this.lblBeamDesignName, "");
            // 
            // ChkActive
            // 
            this.ChkActive.AutoSize = true;
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChkActive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.HelpText = "Checked If Active";
            this.ChkActive.Location = new System.Drawing.Point(535, 42);
            this.ChkActive.Moveable = false;
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.NameOfControl = null;
            this.ChkActive.Size = new System.Drawing.Size(110, 18);
            this.ChkActive.TabIndex = 5;
            this.ChkActive.Text = "Active Status";
            this.tltOnControls.SetToolTip(this.ChkActive, "");
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // tltOnControls
            // 
            this.tltOnControls.DataProvider = stringDataProvider5;
            this.tltOnControls.LoadText = "";
            this.tltOnControls.ShowToolTip = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(520, 15);
            this.label1.Moveable = false;
            this.label1.Name = "label1";
            this.label1.NameOfControl = null;
            this.label1.Size = new System.Drawing.Size(12, 14);
            this.label1.TabIndex = 1068;
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
            this.txtAliasName.Location = new System.Drawing.Point(535, 15);
            this.txtAliasName.Mask = null;
            this.txtAliasName.MaxLength = 50;
            this.txtAliasName.Moveable = false;
            this.txtAliasName.Name = "txtAliasName";
            this.txtAliasName.NameOfControl = "Alias Name";
            this.txtAliasName.Prefix = null;
            this.txtAliasName.ShowBallonTip = false;
            this.txtAliasName.ShowErrorIcon = false;
            this.txtAliasName.ShowMessage = null;
            this.txtAliasName.Size = new System.Drawing.Size(239, 21);
            this.txtAliasName.Suffix = null;
            this.txtAliasName.TabIndex = 2;
            this.tltOnControls.SetToolTip(this.txtAliasName, "");
            this.txtAliasName.Leave += new System.EventHandler(this.txtAliasName_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(419, 15);
            this.label2.Moveable = false;
            this.label2.Name = "label2";
            this.label2.NameOfControl = null;
            this.label2.Size = new System.Drawing.Size(81, 14);
            this.label2.TabIndex = 1067;
            this.label2.Text = "Alias Name";
            this.tltOnControls.SetToolTip(this.label2, "");
            // 
            // frmBeamDesignMaster
            // 
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(808, 545);
            this.DoubleBuffered = false;
            this.Name = "frmBeamDesignMaster";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmBeamDesignMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlDetail.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal CIS_CLibrary.CIS_TextLabel TxtTotWts;
        internal CIS_CLibrary.CIS_TextLabel LblTotaWeight;
        internal CIS_CLibrary.CIS_Textbox txtOrderNo;
        internal CIS_CLibrary.CIS_TextLabel lblOrderNoColun;
        internal CIS_CLibrary.CIS_TextLabel lblOrderNo;
        internal System.Windows.Forms.Panel pnlDetail;
        internal CIS_CLibrary.CIS_TextLabel lblBeamDesignNameColun;
        internal CIS_CLibrary.CIS_TextLabel lblWidthColun;
        internal CIS_CLibrary.CIS_Textbox txtWidth;
        internal CIS_CLibrary.CIS_Textbox txtBeamDesign;
        public CIS_CLibrary.CIS_Textbox txtCode;
        internal CIS_CLibrary.CIS_TextLabel lblWidth;
        internal CIS_CLibrary.CIS_TextLabel lblBeamDesignName;
        internal CIS_CLibrary.CIS_CheckBox ChkActive;
        private CIS_CLibrary.ToolTip.CIS_ToolTip tltOnControls;
        internal CIS_CLibrary.CIS_TextLabel label1;
        internal CIS_CLibrary.CIS_Textbox txtAliasName;
        internal CIS_CLibrary.CIS_TextLabel label2;
        private Crocus_CClib.DataGridViewX GrdMain;
    }
}
