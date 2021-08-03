namespace CIS_Textil
{
    partial class frmSrchImg
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
            CIS_ImageViewer.CrocusSlideShowOptions crystalSlideShowOptions8 = new CIS_ImageViewer.CrocusSlideShowOptions();
            CIS_ImageViewer.CrocusDesignCollector crystalDesignCollector2 = new CIS_ImageViewer.CrocusDesignCollector();
            CIS_ImageViewer.CrocusHeaderStyle crystalHeaderStyle2 = new CIS_ImageViewer.CrocusHeaderStyle();
            this.imageSplitContainer = new System.Windows.Forms.SplitContainer();
            this.txtSearch = new CIS_CLibrary.CIS_Textbox();
            this.cboFilterType = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.btnShowImg = new System.Windows.Forms.Button();
            this.viewerMain = new CIS_ImageViewer.CrocusPictureShow();
            this.lblBorderRight = new CIS_CLibrary.CIS_TextLabel();
            this.label1 = new CIS_CLibrary.CIS_TextLabel();
            this.navToolStrip = new System.Windows.Forms.ToolStrip();
            this.rightButton = new System.Windows.Forms.ToolStripButton();
            this.leftButton = new System.Windows.Forms.ToolStripButton();
            this.zoomToolStripTrackBar = new CIS_ImageViewer.CrocusToolStripTrackBar();
            this.zoomComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.crystalImageGridView1 = new CIS_ImageViewer.CrocusImageGridView();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.pnlCaptionBar = new System.Windows.Forms.Panel();
            this.label21 = new CIS_CLibrary.CIS_TextLabel();
            this.lblBorderLeft = new CIS_CLibrary.CIS_TextLabel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblFormName = new CIS_CLibrary.CIS_TextLabel();
            ((System.ComponentModel.ISupportInitialize)(this.imageSplitContainer)).BeginInit();
            this.imageSplitContainer.Panel1.SuspendLayout();
            this.imageSplitContainer.Panel2.SuspendLayout();
            this.imageSplitContainer.SuspendLayout();
            this.viewerMain.SuspendLayout();
            this.navToolStrip.SuspendLayout();
            this.pnlCaptionBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageSplitContainer
            // 
            this.imageSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.imageSplitContainer.IsSplitterFixed = true;
            this.imageSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.imageSplitContainer.Name = "imageSplitContainer";
            this.imageSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // imageSplitContainer.Panel1
            // 
            this.imageSplitContainer.Panel1.Controls.Add(this.txtSearch);
            this.imageSplitContainer.Panel1.Controls.Add(this.cboFilterType);
            this.imageSplitContainer.Panel1.Controls.Add(this.btnShowImg);
            this.imageSplitContainer.Panel1.Controls.Add(this.viewerMain);
            this.imageSplitContainer.Panel1.Controls.Add(this.navToolStrip);
            this.imageSplitContainer.Panel1MinSize = 10;
            // 
            // imageSplitContainer.Panel2
            // 
            this.imageSplitContainer.Panel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.imageSplitContainer.Panel2.Controls.Add(this.crystalImageGridView1);
            this.imageSplitContainer.Panel2MinSize = 10;
            this.imageSplitContainer.Size = new System.Drawing.Size(723, 550);
            this.imageSplitContainer.SplitterDistance = 430;
            this.imageSplitContainer.TabIndex = 4;
            // 
            // txtSearch
            // 
            this.txtSearch.AutoFillDate = false;
            this.txtSearch.BackColor = System.Drawing.Color.White;
            this.txtSearch.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtSearch.CheckForSymbol = null;
            this.txtSearch.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtSearch.DecimalPlace = 0;
            this.txtSearch.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.HelpText = "";
            this.txtSearch.HoldMyText = null;
            this.txtSearch.IsMandatory = false;
            this.txtSearch.IsSingleQuote = true;
            this.txtSearch.IsSysmbol = false;
            this.txtSearch.Location = new System.Drawing.Point(116, 406);
            this.txtSearch.Mask = null;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.NameOfControl = null;
            this.txtSearch.Prefix = null;
            this.txtSearch.ShowBallonTip = false;
            this.txtSearch.ShowErrorIcon = false;
            this.txtSearch.ShowMessage = null;
            this.txtSearch.Size = new System.Drawing.Size(169, 20);
            this.txtSearch.Suffix = null;
            this.txtSearch.TabIndex = 2;
            // 
            // cboFilterType
            // 
            this.cboFilterType.AutoComplete = false;
            this.cboFilterType.AutoDropdown = false;
            this.cboFilterType.BackColor = System.Drawing.Color.White;
            this.cboFilterType.BackColorEven = System.Drawing.Color.White;
            this.cboFilterType.BackColorOdd = System.Drawing.Color.White;
            this.cboFilterType.ColumnNames = "";
            this.cboFilterType.ColumnWidthDefault = 175;
            this.cboFilterType.ColumnWidths = "";
            this.cboFilterType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboFilterType.Fill_ComboID = 0;
            this.cboFilterType.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFilterType.FormattingEnabled = true;
            this.cboFilterType.GroupType = 0;
            this.cboFilterType.HelpText = null;
            this.cboFilterType.IsMandatory = false;
            this.cboFilterType.Items.AddRange(new object[] {
            "EXACT",
            "START_WITH",
            "END_WITH",
            "CONTAINS"});
            this.cboFilterType.LinkedColumnIndex = 0;
            this.cboFilterType.LinkedTextBox = null;
            this.cboFilterType.Location = new System.Drawing.Point(11, 406);
            this.cboFilterType.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboFilterType.Name = "cboFilterType";
            this.cboFilterType.NameOfControl = null;
            this.cboFilterType.OpenForm = null;
            this.cboFilterType.ShowBallonTip = false;
            this.cboFilterType.Size = new System.Drawing.Size(103, 21);
            this.cboFilterType.TabIndex = 1;
            // 
            // btnShowImg
            // 
            this.btnShowImg.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnShowImg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowImg.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnShowImg.Location = new System.Drawing.Point(287, 404);
            this.btnShowImg.Name = "btnShowImg";
            this.btnShowImg.Size = new System.Drawing.Size(75, 23);
            this.btnShowImg.TabIndex = 3;
            this.btnShowImg.Text = "Show";
            this.btnShowImg.UseVisualStyleBackColor = false;
            this.btnShowImg.Click += new System.EventHandler(this.btnShowImg_Click);
            // 
            // viewerMain
            // 
            this.viewerMain.AutoScroll = true;
            this.viewerMain.CenterImage = true;
            this.viewerMain.Color1 = System.Drawing.Color.MintCream;
            this.viewerMain.Color2 = System.Drawing.Color.MintCream;
            this.viewerMain.ColorAngle = 360F;
            this.viewerMain.Controls.Add(this.lblBorderRight);
            this.viewerMain.Controls.Add(this.label1);
            this.viewerMain.CrocusImageCollector = null;
            this.viewerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewerMain.Image = null;
            this.viewerMain.ImageIndex = -1;
            this.viewerMain.ImageSizeMode = CIS_ImageViewer.SizeMode.Scrollable;
            this.viewerMain.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            this.viewerMain.Location = new System.Drawing.Point(0, 0);
            this.viewerMain.Name = "viewerMain";
            this.viewerMain.ShowPanOnZoom = true;
            this.viewerMain.Size = new System.Drawing.Size(721, 403);
            crystalSlideShowOptions8.ImageIntervalTime = 2F;
            crystalSlideShowOptions8.IntervalImageHold = 3000;
            crystalSlideShowOptions8.RepeatMode = false;
            crystalSlideShowOptions8.ShuffleMode = false;
            crystalSlideShowOptions8.SlideEffect = CIS_ImageViewer.SlideShowEffect.Cycle;
            this.viewerMain.SlideShowOptions = crystalSlideShowOptions8;
            this.viewerMain.TabIndex = 1;
            this.viewerMain.Text = "crystalPictureShow1";
            this.viewerMain.ToolTipText = "";
            this.viewerMain.UseThumbnailer = true;
            //this.viewerMain.ZoomFactor = 1.0F;
            this.viewerMain.FullScreenTerminated += new System.EventHandler(this.viewerMain_FullScreenTerminated);
            this.viewerMain.SlideShowTerminated += new System.EventHandler(this.viewerMain_SlideShowTerminated);
            this.viewerMain.DoubleClick += new System.EventHandler(this.viewerMain_DoubleClick);
            // 
            // lblBorderRight
            // 
            this.lblBorderRight.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblBorderRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblBorderRight.Location = new System.Drawing.Point(718, 0);
            this.lblBorderRight.Name = "lblBorderRight";
            this.lblBorderRight.Size = new System.Drawing.Size(3, 403);
            this.lblBorderRight.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.DodgerBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(3, 403);
            this.label1.TabIndex = 3;
            // 
            // navToolStrip
            // 
            this.navToolStrip.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.navToolStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.navToolStrip.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.navToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rightButton,
            this.leftButton,
            this.zoomToolStripTrackBar,
            this.zoomComboBox,
            this.toolStripLabel1});
            this.navToolStrip.Location = new System.Drawing.Point(0, 403);
            this.navToolStrip.Name = "navToolStrip";
            this.navToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.navToolStrip.Size = new System.Drawing.Size(721, 25);
            this.navToolStrip.TabIndex = 0;
            this.navToolStrip.Text = "Search Tool";
            // 
            // rightButton
            // 
            this.rightButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.rightButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rightButton.Image = global::CIS_Textil.Properties.Resources.arrowright_green_16;
            this.rightButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rightButton.Name = "rightButton";
            this.rightButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.rightButton.Size = new System.Drawing.Size(23, 22);
            this.rightButton.Text = "toolStripButton1";
            // 
            // leftButton
            // 
            this.leftButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.leftButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.leftButton.Image = global::CIS_Textil.Properties.Resources.arrowleft_green_16;
            this.leftButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.leftButton.Name = "leftButton";
            this.leftButton.Size = new System.Drawing.Size(23, 22);
            this.leftButton.Text = "toolStripButton2";
            // 
            // zoomToolStripTrackBar
            // 
            this.zoomToolStripTrackBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.zoomToolStripTrackBar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.zoomToolStripTrackBar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.zoomToolStripTrackBar.Name = "zoomToolStripTrackBar";
            this.zoomToolStripTrackBar.Size = new System.Drawing.Size(150, 22);
            this.zoomToolStripTrackBar.Text = "crystalToolStripTrackBar1";
            // 
            // zoomComboBox
            // 
            this.zoomComboBox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.zoomComboBox.Name = "zoomComboBox";
            this.zoomComboBox.Size = new System.Drawing.Size(75, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(78, 22);
            this.toolStripLabel1.Text = "Zoom Factor:";
            // 
            // crystalImageGridView1
            // 
            this.crystalImageGridView1.AlphaBlendValue = 150;
            this.crystalImageGridView1.AutoScroll = true;
            this.crystalImageGridView1.AutoScrollMinSize = new System.Drawing.Size(2705, 80);
            this.crystalImageGridView1.BackColor = System.Drawing.Color.Gray;
            this.crystalImageGridView1.BorderSplitMode = false;
            this.crystalImageGridView1.BorderState = CIS_ImageViewer.CrocusBorderState.CrocusRoundedRectFilledBorder;
            this.crystalImageGridView1.BorderWidth = 6;
            this.crystalImageGridView1.CellBorderColor = System.Drawing.Color.Maroon;
            this.crystalImageGridView1.CellMargin = 10;
            this.crystalImageGridView1.CellSelectedTextColor = System.Drawing.Color.White;
            this.crystalImageGridView1.CellSize = new System.Drawing.Size(80, 80);
            this.crystalImageGridView1.CellSplitColor = System.Drawing.Color.Black;
            this.crystalImageGridView1.CellTextColor = System.Drawing.Color.Black;
            this.crystalImageGridView1.Color1 = System.Drawing.Color.SteelBlue;
            this.crystalImageGridView1.Color2 = System.Drawing.Color.SteelBlue;
            this.crystalImageGridView1.ColorAngle = 90F;
            this.crystalImageGridView1.FocusedImage = null;
            this.crystalImageGridView1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crystalImageGridView1.ForeColor = System.Drawing.Color.White;
            crystalDesignCollector2.ImageFilter = null;
            crystalDesignCollector2.ImageLocation = null;
            crystalDesignCollector2.PersistThumbnails = true;
            crystalDesignCollector2.Thumbnailer = null;
            this.crystalImageGridView1.GridController = crystalDesignCollector2;
            this.crystalImageGridView1.GridImages = 9;
            this.crystalImageGridView1.GridMargin = 5;
            crystalHeaderStyle2.CollapsedImage = null;
            crystalHeaderStyle2.ExpandedImage = null;
            crystalHeaderStyle2.HeaderSize = new System.Drawing.Size(25, 25);
            this.crystalImageGridView1.HeaderStyle = crystalHeaderStyle2;
            this.crystalImageGridView1.ImageItemFilter = CIS_ImageViewer.CrocusFilterType.AllImages;
            this.crystalImageGridView1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            this.crystalImageGridView1.Location = new System.Drawing.Point(0, 3);
            this.crystalImageGridView1.Name = "crystalImageGridView1";
            this.crystalImageGridView1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.crystalImageGridView1.RightMouseButtonClick = false;
            this.crystalImageGridView1.ShowHeaders = false;
            this.crystalImageGridView1.ShowText = true;
            this.crystalImageGridView1.ShowThumbnails = true;
            this.crystalImageGridView1.Size = new System.Drawing.Size(718, 109);
            this.crystalImageGridView1.TabIndex = 0;
            this.crystalImageGridView1.Text = "crystalImageGridView1";
            this.crystalImageGridView1.TextHeight = 19;
            this.crystalImageGridView1.TextMargin = 2;
            this.crystalImageGridView1.UseAlphaBlending = false;
            //this.crystalImageGridView1.ZoomFactor = 1F;
            this.crystalImageGridView1.CrocusImageDoubleClicked += new System.EventHandler(this.crystalImageGridView1_CrocusImageDoubleClicked);
            // 
            // pnlCaptionBar
            // 
            this.pnlCaptionBar.BackColor = System.Drawing.Color.DodgerBlue;
            this.pnlCaptionBar.Controls.Add(this.label21);
            this.pnlCaptionBar.Controls.Add(this.lblBorderLeft);
            this.pnlCaptionBar.Controls.Add(this.btnCancel);
            this.pnlCaptionBar.Controls.Add(this.lblFormName);
            this.pnlCaptionBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCaptionBar.Location = new System.Drawing.Point(0, 0);
            this.pnlCaptionBar.Name = "pnlCaptionBar";
            this.pnlCaptionBar.Size = new System.Drawing.Size(723, 26);
            this.pnlCaptionBar.TabIndex = 7;
            this.pnlCaptionBar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlCaptionBar_MouseMove);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(6, 6);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(106, 13);
            this.label21.TabIndex = 408;
            this.label21.Text = "Search Product";
            // 
            // lblBorderLeft
            // 
            this.lblBorderLeft.BackColor = System.Drawing.Color.DodgerBlue;
            this.lblBorderLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblBorderLeft.Location = new System.Drawing.Point(0, 0);
            this.lblBorderLeft.Name = "lblBorderLeft";
            this.lblBorderLeft.Size = new System.Drawing.Size(3, 26);
            this.lblBorderLeft.TabIndex = 405;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.GhostWhite;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.DarkRed;
            this.btnCancel.Location = new System.Drawing.Point(697, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(23, 20);
            this.btnCancel.TabIndex = 404;
            this.btnCancel.TabStop = false;
            this.btnCancel.Text = "X";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblFormName
            // 
            this.lblFormName.AutoSize = true;
            this.lblFormName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormName.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.lblFormName.Location = new System.Drawing.Point(5, 6);
            this.lblFormName.Name = "lblFormName";
            this.lblFormName.Size = new System.Drawing.Size(0, 15);
            this.lblFormName.TabIndex = 402;
            // 
            // SrchImg
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(723, 550);
            this.ControlBox = false;
            this.Controls.Add(this.pnlCaptionBar);
            this.Controls.Add(this.imageSplitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SrchImg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Show Products";
            this.Load += new System.EventHandler(this.SrchImg_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SrchImg_KeyUp);
            this.imageSplitContainer.Panel1.ResumeLayout(false);
            this.imageSplitContainer.Panel1.PerformLayout();
            this.imageSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imageSplitContainer)).EndInit();
            this.imageSplitContainer.ResumeLayout(false);
            this.viewerMain.ResumeLayout(false);
            this.navToolStrip.ResumeLayout(false);
            this.navToolStrip.PerformLayout();
            this.pnlCaptionBar.ResumeLayout(false);
            this.pnlCaptionBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer imageSplitContainer;
        private System.Windows.Forms.ToolStrip navToolStrip;
        private CIS_ImageViewer.CrocusToolStripTrackBar zoomToolStripTrackBar;
        private System.Windows.Forms.ToolStripComboBox zoomComboBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton rightButton;
        private System.Windows.Forms.ToolStripButton leftButton;
        private CIS_ImageViewer.CrocusPictureShow viewerMain;
        private CIS_ImageViewer.CrocusImageGridView crystalImageGridView1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel pnlCaptionBar;
        private System.Windows.Forms.Button btnCancel;
        public CIS_CLibrary.CIS_TextLabel lblFormName;
        private CIS_CLibrary.CIS_TextLabel label1;
        private CIS_CLibrary.CIS_TextLabel lblBorderLeft;
        private CIS_CLibrary.CIS_TextLabel lblBorderRight;
        private System.Windows.Forms.Button btnShowImg;
        internal CIS_CLibrary.CIS_TextLabel label21;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboFilterType;
        internal CIS_CLibrary.CIS_Textbox txtSearch;
    }
}

