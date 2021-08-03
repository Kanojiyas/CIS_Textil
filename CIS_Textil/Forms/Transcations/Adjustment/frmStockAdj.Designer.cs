namespace CIS_Textil
{
    partial class frmStockAdj
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
            this.Panel1 = new System.Windows.Forms.Panel();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdSelect = new System.Windows.Forms.Button();
            this.lblBorderLeft = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblBorderRight = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.pnlCaptionBar = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblFormCaption = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblFormName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.pnlGrid = new System.Windows.Forms.Panel();
            this.UGrid_Rpt = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.Panel1.SuspendLayout();
            this.pnlCaptionBar.SuspendLayout();
            this.pnlGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.UGrid_Rpt)).BeginInit();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.BackColor = System.Drawing.Color.SlateBlue;
            this.Panel1.Controls.Add(this.cmdCancel);
            this.Panel1.Controls.Add(this.cmdSelect);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel1.Location = new System.Drawing.Point(0, 344);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(820, 48);
            this.Panel1.TabIndex = 2;
            // 
            // cmdCancel
            // 
            this.cmdCancel.BackColor = System.Drawing.Color.DarkKhaki;
            this.cmdCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCancel.Location = new System.Drawing.Point(411, 5);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 39);
            this.cmdCancel.TabIndex = 3;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = false;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdSelect
            // 
            this.cmdSelect.BackColor = System.Drawing.Color.DarkKhaki;
            this.cmdSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSelect.Location = new System.Drawing.Point(330, 5);
            this.cmdSelect.Name = "cmdSelect";
            this.cmdSelect.Size = new System.Drawing.Size(75, 39);
            this.cmdSelect.TabIndex = 2;
            this.cmdSelect.Text = "Select";
            this.cmdSelect.UseVisualStyleBackColor = false;
            this.cmdSelect.Click += new System.EventHandler(this.CmdSelect_Click);
            // 
            // lblBorderLeft
            // 
            this.lblBorderLeft.AutoSize = true;
            this.lblBorderLeft.BackColor = System.Drawing.Color.SlateBlue;
            this.lblBorderLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblBorderLeft.Location = new System.Drawing.Point(0, 0);
            this.lblBorderLeft.Moveable = false;
            this.lblBorderLeft.Name = "lblBorderLeft";
            this.lblBorderLeft.NameOfControl = null;
            this.lblBorderLeft.Size = new System.Drawing.Size(0, 13);
            this.lblBorderLeft.TabIndex = 3;
            // 
            // lblBorderRight
            // 
            this.lblBorderRight.AutoSize = true;
            this.lblBorderRight.BackColor = System.Drawing.Color.SlateBlue;
            this.lblBorderRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblBorderRight.Location = new System.Drawing.Point(820, 0);
            this.lblBorderRight.Moveable = false;
            this.lblBorderRight.Name = "lblBorderRight";
            this.lblBorderRight.NameOfControl = null;
            this.lblBorderRight.Size = new System.Drawing.Size(0, 13);
            this.lblBorderRight.TabIndex = 4;
            // 
            // pnlCaptionBar
            // 
            this.pnlCaptionBar.BackColor = System.Drawing.Color.SlateBlue;
            this.pnlCaptionBar.Controls.Add(this.btnCancel);
            this.pnlCaptionBar.Controls.Add(this.lblFormCaption);
            this.pnlCaptionBar.Controls.Add(this.lblFormName);
            this.pnlCaptionBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCaptionBar.Location = new System.Drawing.Point(0, 0);
            this.pnlCaptionBar.Name = "pnlCaptionBar";
            this.pnlCaptionBar.Size = new System.Drawing.Size(820, 26);
            this.pnlCaptionBar.TabIndex = 5;
            this.pnlCaptionBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlCaptionBar_MouseMove);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.GhostWhite;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.DarkRed;
            this.btnCancel.Location = new System.Drawing.Point(794, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(23, 20);
            this.btnCancel.TabIndex = 405;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "X";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblFormCaption
            // 
            this.lblFormCaption.AutoSize = true;
            this.lblFormCaption.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormCaption.ForeColor = System.Drawing.Color.White;
            this.lblFormCaption.Location = new System.Drawing.Point(9, 6);
            this.lblFormCaption.Moveable = false;
            this.lblFormCaption.Name = "lblFormCaption";
            this.lblFormCaption.NameOfControl = null;
            this.lblFormCaption.Size = new System.Drawing.Size(48, 14);
            this.lblFormCaption.TabIndex = 403;
            this.lblFormCaption.Text = "label1";
            // 
            // lblFormName
            // 
            this.lblFormName.AutoSize = true;
            this.lblFormName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormName.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblFormName.Location = new System.Drawing.Point(5, 6);
            this.lblFormName.Moveable = false;
            this.lblFormName.Name = "lblFormName";
            this.lblFormName.NameOfControl = null;
            this.lblFormName.Size = new System.Drawing.Size(0, 14);
            this.lblFormName.TabIndex = 402;
            // 
            // pnlGrid
            // 
            this.pnlGrid.Controls.Add(this.UGrid_Rpt);
            this.pnlGrid.Location = new System.Drawing.Point(3, 24);
            this.pnlGrid.Name = "pnlGrid";
            this.pnlGrid.Size = new System.Drawing.Size(813, 320);
            this.pnlGrid.TabIndex = 0;
            // 
            // UGrid_Rpt
            // 
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.UGrid_Rpt.DisplayLayout.Appearance = appearance1;
            this.UGrid_Rpt.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded4;
            this.UGrid_Rpt.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.UGrid_Rpt.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.UGrid_Rpt.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.UGrid_Rpt.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.UGrid_Rpt.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.UGrid_Rpt.DisplayLayout.MaxColScrollRegions = 1;
            this.UGrid_Rpt.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.Color.White;
            appearance5.BackGradientAlignment = Infragistics.Win.GradientAlignment.Client;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.UGrid_Rpt.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.UGrid_Rpt.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.UGrid_Rpt.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.UGrid_Rpt.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.UGrid_Rpt.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.UGrid_Rpt.DisplayLayout.Override.CellAppearance = appearance8;
            this.UGrid_Rpt.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.UGrid_Rpt.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.UGrid_Rpt.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.UGrid_Rpt.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.UGrid_Rpt.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.UGrid_Rpt.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.UGrid_Rpt.DisplayLayout.Override.RowAppearance = appearance11;
            this.UGrid_Rpt.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.UGrid_Rpt.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.UGrid_Rpt.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.UGrid_Rpt.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.UGrid_Rpt.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.UGrid_Rpt.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.UGrid_Rpt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UGrid_Rpt.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UGrid_Rpt.Location = new System.Drawing.Point(0, 0);
            this.UGrid_Rpt.Name = "UGrid_Rpt";
            this.UGrid_Rpt.Size = new System.Drawing.Size(813, 320);
            this.UGrid_Rpt.TabIndex = 1;
            this.UGrid_Rpt.Text = "UltraGrid1";
            this.UGrid_Rpt.UseFlatMode = Infragistics.Win.DefaultableBoolean.True;
            this.UGrid_Rpt.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.UGrid_Rpt_InitializeLayout);
            this.UGrid_Rpt.ClickCell += new Infragistics.Win.UltraWinGrid.ClickCellEventHandler(this.UGrid_Rpt_ClickCell);
            this.UGrid_Rpt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UGrid_Rpt_KeyDown);
            // 
            // frmStockAdj
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 392);
            this.Controls.Add(this.pnlGrid);
            this.Controls.Add(this.pnlCaptionBar);
            this.Controls.Add(this.lblBorderRight);
            this.Controls.Add(this.lblBorderLeft);
            this.Controls.Add(this.Panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(389, 210);
            this.Name = "frmStockAdj";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.frmStockAdj_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmStockAdj_KeyUp);
            this.Panel1.ResumeLayout(false);
            this.pnlCaptionBar.ResumeLayout(false);
            this.pnlCaptionBar.PerformLayout();
            this.pnlGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UGrid_Rpt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Panel Panel1;
        private CIS_CLibrary.CIS_TextLabel lblBorderLeft;
        private CIS_CLibrary.CIS_TextLabel lblBorderRight;
        private System.Windows.Forms.Panel pnlCaptionBar;
        public CIS_CLibrary.CIS_TextLabel lblFormName;
        private System.Windows.Forms.Panel pnlGrid;
        internal Infragistics.Win.UltraWinGrid.UltraGrid UGrid_Rpt;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdSelect;
        private CIS_CLibrary.CIS_TextLabel lblFormCaption;
        private System.Windows.Forms.Button btnCancel;
    }
}