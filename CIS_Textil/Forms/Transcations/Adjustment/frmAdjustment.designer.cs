namespace CIS_Textil
{
    partial class frmAdjustment
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
            CIS_CLibrary.ToolTip.StringDataProvider stringDataProvider3 = new CIS_CLibrary.ToolTip.StringDataProvider();
            this.lblSemi02 = new System.Windows.Forms.Label();
            this.lblSemi01 = new System.Windows.Forms.Label();
            this.Label2 = new System.Windows.Forms.Label();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnClose = new CIS_CLibrary.CIS_Button();
            this.spc_Adj = new System.Windows.Forms.SplitContainer();
            this.txtUnAdjustedAnt = new CIS_CLibrary.CIS_Textbox();
            this.txtAdjustedAmt = new CIS_CLibrary.CIS_Textbox();
            this.lblAdvance = new System.Windows.Forms.Label();
            this.lblAdvance_Clr = new System.Windows.Forms.Label();
            this.lblNewRef = new System.Windows.Forms.Label();
            this.lblNewRef_Clr = new System.Windows.Forms.Label();
            this.lblOnAc = new System.Windows.Forms.Label();
            this.lblOnAc_Clr = new System.Windows.Forms.Label();
            this.btnDone = new CIS_CLibrary.CIS_Button();
            this.btnAutoAdj = new CIS_CLibrary.CIS_Button();
            this.tltOnControls = new CIS_CLibrary.ToolTip.CIS_ToolTip();
            this.lblBorderRight = new System.Windows.Forms.Label();
            this.lblBorderLeft = new System.Windows.Forms.Label();
            this.lblDockTop = new System.Windows.Forms.Label();
            this.lblDockBottom = new System.Windows.Forms.Label();
            this.lblFormCaption = new System.Windows.Forms.Label();
            this.txtOnAccountAmt = new CIS_CLibrary.CIS_Textbox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblOnAccountAmt = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.spc_Adj)).BeginInit();
            this.spc_Adj.Panel2.SuspendLayout();
            this.spc_Adj.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSemi02
            // 
            this.lblSemi02.AutoSize = true;
            this.lblSemi02.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSemi02.Location = new System.Drawing.Point(656, 36);
            this.lblSemi02.Name = "lblSemi02";
            this.lblSemi02.Size = new System.Drawing.Size(12, 14);
            this.lblSemi02.TabIndex = 6;
            this.lblSemi02.Text = ":";
            this.tltOnControls.SetToolTip(this.lblSemi02, "");
            // 
            // lblSemi01
            // 
            this.lblSemi01.AutoSize = true;
            this.lblSemi01.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSemi01.Location = new System.Drawing.Point(656, 10);
            this.lblSemi01.Name = "lblSemi01";
            this.lblSemi01.Size = new System.Drawing.Size(12, 14);
            this.lblSemi01.TabIndex = 5;
            this.lblSemi01.Text = ":";
            this.tltOnControls.SetToolTip(this.lblSemi01, "");
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(542, 36);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(113, 14);
            this.Label2.TabIndex = 4;
            this.Label2.Text = "Un. Adjust. Amt.";
            this.tltOnControls.SetToolTip(this.Label2, "");
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(542, 10);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(87, 14);
            this.Label1.TabIndex = 3;
            this.Label1.Text = "Adjust. Amt.";
            this.tltOnControls.SetToolTip(this.Label1, "");
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.CadetBlue;
            this.btnClose.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(256, 21);
            this.btnClose.Moveable = false;
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 37);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Cancel";
            this.tltOnControls.SetToolTip(this.btnClose, "");
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // spc_Adj
            // 
            this.spc_Adj.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_Adj.IsSplitterFixed = true;
            this.spc_Adj.Location = new System.Drawing.Point(0, 25);
            this.spc_Adj.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.spc_Adj.Name = "spc_Adj";
            this.spc_Adj.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spc_Adj.Panel1
            // 
            this.spc_Adj.Panel1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.tltOnControls.SetToolTip(this.spc_Adj.Panel1, "");
            // 
            // spc_Adj.Panel2
            // 
            this.spc_Adj.Panel2.BackColor = System.Drawing.Color.Transparent;
            this.spc_Adj.Panel2.Controls.Add(this.txtOnAccountAmt);
            this.spc_Adj.Panel2.Controls.Add(this.label3);
            this.spc_Adj.Panel2.Controls.Add(this.lblOnAccountAmt);
            this.spc_Adj.Panel2.Controls.Add(this.txtUnAdjustedAnt);
            this.spc_Adj.Panel2.Controls.Add(this.txtAdjustedAmt);
            this.spc_Adj.Panel2.Controls.Add(this.lblAdvance);
            this.spc_Adj.Panel2.Controls.Add(this.lblAdvance_Clr);
            this.spc_Adj.Panel2.Controls.Add(this.lblNewRef);
            this.spc_Adj.Panel2.Controls.Add(this.lblNewRef_Clr);
            this.spc_Adj.Panel2.Controls.Add(this.lblOnAc);
            this.spc_Adj.Panel2.Controls.Add(this.lblOnAc_Clr);
            this.spc_Adj.Panel2.Controls.Add(this.lblSemi02);
            this.spc_Adj.Panel2.Controls.Add(this.lblSemi01);
            this.spc_Adj.Panel2.Controls.Add(this.Label2);
            this.spc_Adj.Panel2.Controls.Add(this.Label1);
            this.spc_Adj.Panel2.Controls.Add(this.btnClose);
            this.spc_Adj.Panel2.Controls.Add(this.btnDone);
            this.spc_Adj.Panel2.Controls.Add(this.btnAutoAdj);
            this.tltOnControls.SetToolTip(this.spc_Adj.Panel2, "");
            this.spc_Adj.Panel2MinSize = 50;
            this.spc_Adj.Size = new System.Drawing.Size(813, 381);
            this.spc_Adj.SplitterDistance = 290;
            this.spc_Adj.SplitterWidth = 1;
            this.spc_Adj.TabIndex = 1;
            this.tltOnControls.SetToolTip(this.spc_Adj, "");
            // 
            // txtUnAdjustedAnt
            // 
            this.txtUnAdjustedAnt.AutoFillDate = false;
            this.txtUnAdjustedAnt.BackColor = System.Drawing.Color.MistyRose;
            this.txtUnAdjustedAnt.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtUnAdjustedAnt.CheckForSymbol = null;
            this.txtUnAdjustedAnt.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtUnAdjustedAnt.DecimalPlace = 0;
            this.txtUnAdjustedAnt.Enabled = false;
            this.txtUnAdjustedAnt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUnAdjustedAnt.HelpText = "";
            this.txtUnAdjustedAnt.HoldMyText = null;
            this.txtUnAdjustedAnt.IsMandatory = false;
            this.txtUnAdjustedAnt.IsSingleQuote = true;
            this.txtUnAdjustedAnt.IsSysmbol = false;
            this.txtUnAdjustedAnt.Location = new System.Drawing.Point(673, 32);
            this.txtUnAdjustedAnt.Mask = null;
            this.txtUnAdjustedAnt.MaxLength = 50;
            this.txtUnAdjustedAnt.Moveable = false;
            this.txtUnAdjustedAnt.Name = "txtUnAdjustedAnt";
            this.txtUnAdjustedAnt.NameOfControl = "UnAdjusted Amount";
            this.txtUnAdjustedAnt.Prefix = null;
            this.txtUnAdjustedAnt.ShowBallonTip = false;
            this.txtUnAdjustedAnt.ShowErrorIcon = false;
            this.txtUnAdjustedAnt.ShowMessage = null;
            this.txtUnAdjustedAnt.Size = new System.Drawing.Size(131, 22);
            this.txtUnAdjustedAnt.Suffix = null;
            this.txtUnAdjustedAnt.TabIndex = 16;
            this.tltOnControls.SetToolTip(this.txtUnAdjustedAnt, "");
            // 
            // txtAdjustedAmt
            // 
            this.txtAdjustedAmt.AutoFillDate = false;
            this.txtAdjustedAmt.BackColor = System.Drawing.Color.PowderBlue;
            this.txtAdjustedAmt.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtAdjustedAmt.CheckForSymbol = null;
            this.txtAdjustedAmt.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtAdjustedAmt.DecimalPlace = 0;
            this.txtAdjustedAmt.Enabled = false;
            this.txtAdjustedAmt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdjustedAmt.HelpText = "";
            this.txtAdjustedAmt.HoldMyText = null;
            this.txtAdjustedAmt.IsMandatory = false;
            this.txtAdjustedAmt.IsSingleQuote = true;
            this.txtAdjustedAmt.IsSysmbol = false;
            this.txtAdjustedAmt.Location = new System.Drawing.Point(673, 6);
            this.txtAdjustedAmt.Mask = null;
            this.txtAdjustedAmt.MaxLength = 50;
            this.txtAdjustedAmt.Moveable = false;
            this.txtAdjustedAmt.Name = "txtAdjustedAmt";
            this.txtAdjustedAmt.NameOfControl = "Adjusted Amount";
            this.txtAdjustedAmt.Prefix = null;
            this.txtAdjustedAmt.ShowBallonTip = false;
            this.txtAdjustedAmt.ShowErrorIcon = false;
            this.txtAdjustedAmt.ShowMessage = null;
            this.txtAdjustedAmt.Size = new System.Drawing.Size(131, 22);
            this.txtAdjustedAmt.Suffix = null;
            this.txtAdjustedAmt.TabIndex = 15;
            this.tltOnControls.SetToolTip(this.txtAdjustedAmt, "");
            // 
            // lblAdvance
            // 
            this.lblAdvance.AutoSize = true;
            this.lblAdvance.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAdvance.Location = new System.Drawing.Point(253, 67);
            this.lblAdvance.Name = "lblAdvance";
            this.lblAdvance.Size = new System.Drawing.Size(63, 14);
            this.lblAdvance.TabIndex = 14;
            this.lblAdvance.Text = "Advance";
            this.tltOnControls.SetToolTip(this.lblAdvance, "");
            this.lblAdvance.Visible = false;
            // 
            // lblAdvance_Clr
            // 
            this.lblAdvance_Clr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(217)))), ((int)(((byte)(255)))));
            this.lblAdvance_Clr.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblAdvance_Clr.Location = new System.Drawing.Point(233, 65);
            this.lblAdvance_Clr.Name = "lblAdvance_Clr";
            this.lblAdvance_Clr.Size = new System.Drawing.Size(16, 16);
            this.lblAdvance_Clr.TabIndex = 13;
            this.tltOnControls.SetToolTip(this.lblAdvance_Clr, "");
            this.lblAdvance_Clr.Visible = false;
            // 
            // lblNewRef
            // 
            this.lblNewRef.AutoSize = true;
            this.lblNewRef.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNewRef.Location = new System.Drawing.Point(162, 67);
            this.lblNewRef.Name = "lblNewRef";
            this.lblNewRef.Size = new System.Drawing.Size(67, 14);
            this.lblNewRef.TabIndex = 12;
            this.lblNewRef.Text = "New Ref.";
            this.tltOnControls.SetToolTip(this.lblNewRef, "");
            this.lblNewRef.Visible = false;
            // 
            // lblNewRef_Clr
            // 
            this.lblNewRef_Clr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(255)))), ((int)(((byte)(243)))));
            this.lblNewRef_Clr.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblNewRef_Clr.Location = new System.Drawing.Point(142, 65);
            this.lblNewRef_Clr.Name = "lblNewRef_Clr";
            this.lblNewRef_Clr.Size = new System.Drawing.Size(16, 16);
            this.lblNewRef_Clr.TabIndex = 11;
            this.tltOnControls.SetToolTip(this.lblNewRef_Clr, "");
            this.lblNewRef_Clr.Visible = false;
            // 
            // lblOnAc
            // 
            this.lblOnAc.AutoSize = true;
            this.lblOnAc.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOnAc.Location = new System.Drawing.Point(52, 67);
            this.lblOnAc.Name = "lblOnAc";
            this.lblOnAc.Size = new System.Drawing.Size(82, 14);
            this.lblOnAc.TabIndex = 10;
            this.lblOnAc.Text = "On Account";
            this.tltOnControls.SetToolTip(this.lblOnAc, "");
            this.lblOnAc.Visible = false;
            // 
            // lblOnAc_Clr
            // 
            this.lblOnAc_Clr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(238)))), ((int)(((byte)(225)))));
            this.lblOnAc_Clr.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblOnAc_Clr.Location = new System.Drawing.Point(32, 65);
            this.lblOnAc_Clr.Name = "lblOnAc_Clr";
            this.lblOnAc_Clr.Size = new System.Drawing.Size(16, 16);
            this.lblOnAc_Clr.TabIndex = 9;
            this.tltOnControls.SetToolTip(this.lblOnAc_Clr, "");
            this.lblOnAc_Clr.Visible = false;
            // 
            // btnDone
            // 
            this.btnDone.BackColor = System.Drawing.Color.CadetBlue;
            this.btnDone.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDone.Location = new System.Drawing.Point(150, 21);
            this.btnDone.Moveable = false;
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(100, 37);
            this.btnDone.TabIndex = 1;
            this.btnDone.Text = "&Done";
            this.tltOnControls.SetToolTip(this.btnDone, "");
            this.btnDone.UseVisualStyleBackColor = false;
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // btnAutoAdj
            // 
            this.btnAutoAdj.BackColor = System.Drawing.Color.CadetBlue;
            this.btnAutoAdj.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutoAdj.Location = new System.Drawing.Point(44, 21);
            this.btnAutoAdj.Moveable = false;
            this.btnAutoAdj.Name = "btnAutoAdj";
            this.btnAutoAdj.Size = new System.Drawing.Size(100, 37);
            this.btnAutoAdj.TabIndex = 0;
            this.btnAutoAdj.Text = "&Auto Adjust";
            this.tltOnControls.SetToolTip(this.btnAutoAdj, "");
            this.btnAutoAdj.UseVisualStyleBackColor = false;
            this.btnAutoAdj.Click += new System.EventHandler(this.btnAutoAdj_Click);
            // 
            // tltOnControls
            // 
            this.tltOnControls.DataProvider = stringDataProvider3;
            this.tltOnControls.LoadText = "";
            this.tltOnControls.ShowToolTip = false;
            // 
            // lblBorderRight
            // 
            this.lblBorderRight.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblBorderRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblBorderRight.Location = new System.Drawing.Point(810, 25);
            this.lblBorderRight.Name = "lblBorderRight";
            this.lblBorderRight.Size = new System.Drawing.Size(3, 381);
            this.lblBorderRight.TabIndex = 2;
            this.tltOnControls.SetToolTip(this.lblBorderRight, "");
            // 
            // lblBorderLeft
            // 
            this.lblBorderLeft.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblBorderLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblBorderLeft.Location = new System.Drawing.Point(0, 25);
            this.lblBorderLeft.Name = "lblBorderLeft";
            this.lblBorderLeft.Size = new System.Drawing.Size(3, 381);
            this.lblBorderLeft.TabIndex = 3;
            this.tltOnControls.SetToolTip(this.lblBorderLeft, "");
            // 
            // lblDockTop
            // 
            this.lblDockTop.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblDockTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDockTop.Location = new System.Drawing.Point(0, 0);
            this.lblDockTop.Name = "lblDockTop";
            this.lblDockTop.Size = new System.Drawing.Size(813, 25);
            this.lblDockTop.TabIndex = 4;
            this.tltOnControls.SetToolTip(this.lblDockTop, "");
            this.lblDockTop.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblDockTop_MouseMove);
            // 
            // lblDockBottom
            // 
            this.lblDockBottom.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblDockBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblDockBottom.Location = new System.Drawing.Point(3, 403);
            this.lblDockBottom.Name = "lblDockBottom";
            this.lblDockBottom.Size = new System.Drawing.Size(807, 3);
            this.lblDockBottom.TabIndex = 5;
            this.tltOnControls.SetToolTip(this.lblDockBottom, "");
            // 
            // lblFormCaption
            // 
            this.lblFormCaption.AutoSize = true;
            this.lblFormCaption.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblFormCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormCaption.ForeColor = System.Drawing.Color.White;
            this.lblFormCaption.Location = new System.Drawing.Point(14, 5);
            this.lblFormCaption.Name = "lblFormCaption";
            this.lblFormCaption.Size = new System.Drawing.Size(90, 13);
            this.lblFormCaption.TabIndex = 0;
            this.lblFormCaption.Text = "Bill Adjustment";
            this.tltOnControls.SetToolTip(this.lblFormCaption, "");
            // 
            // txtOnAccountAmt
            // 
            this.txtOnAccountAmt.AutoFillDate = false;
            this.txtOnAccountAmt.BackColor = System.Drawing.Color.PowderBlue;
            this.txtOnAccountAmt.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtOnAccountAmt.CheckForSymbol = null;
            this.txtOnAccountAmt.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtOnAccountAmt.DecimalPlace = 0;
            this.txtOnAccountAmt.Enabled = false;
            this.txtOnAccountAmt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOnAccountAmt.HelpText = "";
            this.txtOnAccountAmt.HoldMyText = null;
            this.txtOnAccountAmt.IsMandatory = false;
            this.txtOnAccountAmt.IsSingleQuote = true;
            this.txtOnAccountAmt.IsSysmbol = false;
            this.txtOnAccountAmt.Location = new System.Drawing.Point(673, 58);
            this.txtOnAccountAmt.Mask = null;
            this.txtOnAccountAmt.MaxLength = 50;
            this.txtOnAccountAmt.Moveable = false;
            this.txtOnAccountAmt.Name = "txtOnAccountAmt";
            this.txtOnAccountAmt.NameOfControl = "OnAccount Amt";
            this.txtOnAccountAmt.Prefix = null;
            this.txtOnAccountAmt.ShowBallonTip = false;
            this.txtOnAccountAmt.ShowErrorIcon = false;
            this.txtOnAccountAmt.ShowMessage = null;
            this.txtOnAccountAmt.Size = new System.Drawing.Size(131, 22);
            this.txtOnAccountAmt.Suffix = null;
            this.txtOnAccountAmt.TabIndex = 19;
            this.tltOnControls.SetToolTip(this.txtOnAccountAmt, "");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(656, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 14);
            this.label3.TabIndex = 18;
            this.label3.Text = ":";
            this.tltOnControls.SetToolTip(this.label3, "");
            // 
            // lblOnAccountAmt
            // 
            this.lblOnAccountAmt.AutoSize = true;
            this.lblOnAccountAmt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOnAccountAmt.Location = new System.Drawing.Point(542, 62);
            this.lblOnAccountAmt.Name = "lblOnAccountAmt";
            this.lblOnAccountAmt.Size = new System.Drawing.Size(112, 14);
            this.lblOnAccountAmt.TabIndex = 17;
            this.lblOnAccountAmt.Text = "On Account.Amt";
            this.tltOnControls.SetToolTip(this.lblOnAccountAmt, "");
            // 
            // frmAdjustment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(813, 406);
            this.ControlBox = false;
            this.Controls.Add(this.lblFormCaption);
            this.Controls.Add(this.lblDockBottom);
            this.Controls.Add(this.lblBorderLeft);
            this.Controls.Add(this.lblBorderRight);
            this.Controls.Add(this.spc_Adj);
            this.Controls.Add(this.lblDockTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmAdjustment";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmAdjustment_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmAdjustment_KeyUp);
            this.spc_Adj.Panel2.ResumeLayout(false);
            this.spc_Adj.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spc_Adj)).EndInit();
            this.spc_Adj.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lblSemi02;
        internal System.Windows.Forms.Label lblSemi01;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label Label1;
        internal CIS_CLibrary.CIS_Button btnClose;
        internal System.Windows.Forms.SplitContainer spc_Adj;
        internal CIS_CLibrary.CIS_Button btnDone;
        internal CIS_CLibrary.CIS_Button btnAutoAdj;
        private CIS_CLibrary.ToolTip.CIS_ToolTip tltOnControls;
        private System.Windows.Forms.Label lblBorderRight;
        private System.Windows.Forms.Label lblBorderLeft;
        private System.Windows.Forms.Label lblDockTop;
        private System.Windows.Forms.Label lblDockBottom;
        private System.Windows.Forms.Label lblFormCaption;
        internal CIS_CLibrary.CIS_Textbox txtUnAdjustedAnt;
        internal CIS_CLibrary.CIS_Textbox txtAdjustedAmt;
        internal System.Windows.Forms.Label lblAdvance;
        internal System.Windows.Forms.Label lblAdvance_Clr;
        internal System.Windows.Forms.Label lblNewRef;
        internal System.Windows.Forms.Label lblNewRef_Clr;
        internal System.Windows.Forms.Label lblOnAc;
        internal System.Windows.Forms.Label lblOnAc_Clr;
        internal CIS_CLibrary.CIS_Textbox txtOnAccountAmt;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label lblOnAccountAmt;
    }
}