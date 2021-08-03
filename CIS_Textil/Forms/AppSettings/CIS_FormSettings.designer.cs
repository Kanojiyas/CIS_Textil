namespace CIS_CLibrary
{
    partial class CIS_FormSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblFormName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtClose = new CIS_CLibrary.CIS_Button();
            this.btnSave = new CIS_CLibrary.CIS_Button();
            this.fgDtls = new CIS_DataGridViewEx.DataGridViewEx();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ciS_TabControl1 = new CIS_CLibrary.CIS_TabControl(this.components);
            this.tbDesigner = new System.Windows.Forms.TabPage();
            this.pnlDesigner = new CIS_CLibrary.CIS_Panel();
            this.tabTable = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fgDtls)).BeginInit();
            this.panel3.SuspendLayout();
            this.ciS_TabControl1.SuspendLayout();
            this.tbDesigner.SuspendLayout();
            this.tabTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Desktop;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblFormName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(848, 28);
            this.panel1.TabIndex = 3;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CIS_FormSettings_MouseDown);
            // 
            // lblFormName
            // 
            this.lblFormName.AutoSize = true;
            this.lblFormName.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblFormName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFormName.Location = new System.Drawing.Point(3, 4);
            this.lblFormName.Name = "lblFormName";
            this.lblFormName.Size = new System.Drawing.Size(0, 17);
            this.lblFormName.TabIndex = 1;
            this.lblFormName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txtClose);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 406);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(848, 32);
            this.panel2.TabIndex = 465;
            // 
            // txtClose
            // 
            this.txtClose.BackColor = System.Drawing.Color.MidnightBlue;
            this.txtClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClose.Location = new System.Drawing.Point(404, 2);
            this.txtClose.Moveable = false;
            this.txtClose.Name = "txtClose";
            this.txtClose.Size = new System.Drawing.Size(75, 25);
            this.txtClose.TabIndex = 1;
            this.txtClose.Text = "&Cancel";
            this.txtClose.UseVisualStyleBackColor = false;
            this.txtClose.Click += new System.EventHandler(this.txtClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.MidnightBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(323, 2);
            this.btnSave.Moveable = false;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 25);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "&Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // fgDtls
            // 
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.LightSkyBlue;
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Purple;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.fgDtls.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            this.fgDtls.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.fgDtls.BackgroundColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.DodgerBlue;
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Purple;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.fgDtls.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.fgDtls.ColumnHeadersHeight = 30;
            this.fgDtls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.fgDtls.DefaultCellStyle = dataGridViewCellStyle7;
            this.fgDtls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fgDtls.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.fgDtls.EnableHeadersVisualStyles = false;
            this.fgDtls.Grid_Tbl = null;
            this.fgDtls.Grid_UID = 0;
            this.fgDtls.HeaderRowColor = System.Drawing.Color.Empty;
            this.fgDtls.IsGroupCell = false;
            this.fgDtls.Location = new System.Drawing.Point(3, 3);
            this.fgDtls.Moveable = false;
            this.fgDtls.Name = "fgDtls";
            this.fgDtls.NameOfControl = null;
            this.fgDtls.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.fgDtls.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.fgDtls.RowHeadersVisible = false;
            this.fgDtls.ShowFieldChooser = false;
            this.fgDtls.Size = new System.Drawing.Size(835, 341);
            this.fgDtls.TabIndex = 466;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.ciS_TabControl1);
            this.panel3.Location = new System.Drawing.Point(-1, 28);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(850, 409);
            this.panel3.TabIndex = 467;
            // 
            // ciS_TabControl1
            // 
            this.ciS_TabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.ciS_TabControl1.AutoTab = true;
            this.ciS_TabControl1.Controls.Add(this.tbDesigner);
            this.ciS_TabControl1.Controls.Add(this.tabTable);
            this.ciS_TabControl1.Location = new System.Drawing.Point(1, 0);
            this.ciS_TabControl1.Moveable = false;
            this.ciS_TabControl1.Name = "ciS_TabControl1";
            this.ciS_TabControl1.SelectedIndex = 0;
            this.ciS_TabControl1.Size = new System.Drawing.Size(849, 376);
            this.ciS_TabControl1.TabIndex = 0;
            this.ciS_TabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.ciS_TabControl1_Selecting);
            // 
            // tbDesigner
            // 
            this.tbDesigner.Controls.Add(this.pnlDesigner);
            this.tbDesigner.Location = new System.Drawing.Point(4, 25);
            this.tbDesigner.Name = "tbDesigner";
            this.tbDesigner.Padding = new System.Windows.Forms.Padding(3);
            this.tbDesigner.Size = new System.Drawing.Size(841, 347);
            this.tbDesigner.TabIndex = 0;
            this.tbDesigner.Text = "Designer";
            this.tbDesigner.UseVisualStyleBackColor = true;
            // 
            // pnlDesigner
            // 
            this.pnlDesigner.AssociatedSplitter = null;
            this.pnlDesigner.BackColor = System.Drawing.Color.Transparent;
            this.pnlDesigner.CaptionFont = new System.Drawing.Font("Trebuchet MS", 12.5F, System.Drawing.FontStyle.Bold);
            this.pnlDesigner.CaptionHeight = 27;
            this.pnlDesigner.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(65)))), ((int)(((byte)(118)))));
            this.pnlDesigner.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.pnlDesigner.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.pnlDesigner.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.pnlDesigner.CustomColors.CaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(164)))), ((int)(((byte)(224)))));
            this.pnlDesigner.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(225)))), ((int)(((byte)(252)))));
            this.pnlDesigner.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(222)))));
            this.pnlDesigner.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(203)))), ((int)(((byte)(136)))));
            this.pnlDesigner.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.pnlDesigner.CustomColors.CollapsedCaptionText = System.Drawing.SystemColors.ControlText;
            this.pnlDesigner.CustomColors.ContentGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(190)))), ((int)(((byte)(245)))));
            this.pnlDesigner.CustomColors.ContentGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(218)))), ((int)(((byte)(250)))));
            this.pnlDesigner.CustomColors.InnerBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pnlDesigner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDesigner.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlDesigner.Image = null;
            this.pnlDesigner.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.pnlDesigner.Location = new System.Drawing.Point(3, 3);
            this.pnlDesigner.MinimumSize = new System.Drawing.Size(27, 27);
            this.pnlDesigner.Moveable = false;
            this.pnlDesigner.Name = "pnlDesigner";
            this.pnlDesigner.NameOfControl = null;
            this.pnlDesigner.PanelStyle = CIS_CLibrary.PanelStyle.Default;
            this.pnlDesigner.ShowCaptionbar = false;
            this.pnlDesigner.ShowCloseIcon = true;
            this.pnlDesigner.Size = new System.Drawing.Size(835, 341);
            this.pnlDesigner.TabIndex = 467;
            this.pnlDesigner.Text = "Form Designer";
            this.pnlDesigner.ToolTipTextCloseIcon = null;
            this.pnlDesigner.ToolTipTextExpandIconPanelCollapsed = null;
            this.pnlDesigner.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // tabTable
            // 
            this.tabTable.Controls.Add(this.fgDtls);
            this.tabTable.Location = new System.Drawing.Point(4, 25);
            this.tabTable.Name = "tabTable";
            this.tabTable.Padding = new System.Windows.Forms.Padding(3);
            this.tabTable.Size = new System.Drawing.Size(841, 347);
            this.tabTable.TabIndex = 1;
            this.tabTable.Text = "Data";
            this.tabTable.UseVisualStyleBackColor = true;
            // 
            // CIS_FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Location = new System.Drawing.Point(78, 104);
            this.Name = "CIS_FormSettings";
            this.Size = new System.Drawing.Size(848, 438);
            this.Load += new System.EventHandler(this.CIS_FormSettings_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormSettingControl_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fgDtls)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ciS_TabControl1.ResumeLayout(false);
            this.tbDesigner.ResumeLayout(false);
            this.tabTable.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Panel panel2;
        private CIS_CLibrary.CIS_Button txtClose;
        private CIS_CLibrary.CIS_Button btnSave;
        private CIS_DataGridViewEx.DataGridViewEx fgDtls;
        private System.Windows.Forms.Label lblFormName;
        private System.Windows.Forms.Panel panel3;
        public CIS_Panel pnlDesigner;
        private CIS_TabControl ciS_TabControl1;
        private System.Windows.Forms.TabPage tbDesigner;
        private System.Windows.Forms.TabPage tabTable;

    }
}
