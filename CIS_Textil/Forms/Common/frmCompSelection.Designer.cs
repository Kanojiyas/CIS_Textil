namespace CIS_Textil
{
    partial class frmCompSelection
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
            this.Label5 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblCompanyName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.grp_Select = new System.Windows.Forms.GroupBox();
            this.CboYear = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.CboCompany = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.Label2 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.Label1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblCopyright = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.timerFadeOut = new System.Windows.Forms.Timer(this.components);
            this.timerFadeIn = new System.Windows.Forms.Timer(this.components);
            this.btn_Cancel = new CIS_CLibrary.CIS_Button();
            this.btn_Select = new CIS_CLibrary.CIS_Button();
            this.grp_Select.SuspendLayout();
            this.SuspendLayout();
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.Color.White;
            this.Label5.Location = new System.Drawing.Point(16, 86);
            this.Label5.Moveable = false;
            this.Label5.Name = "Label5";
            this.Label5.NameOfControl = null;
            this.Label5.Size = new System.Drawing.Size(71, 13);
            this.Label5.TabIndex = 41;
            this.Label5.Text = "Licensed to";
            // 
            // lblCompanyName
            // 
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.BackColor = System.Drawing.Color.Transparent;
            this.lblCompanyName.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompanyName.ForeColor = System.Drawing.Color.White;
            this.lblCompanyName.Location = new System.Drawing.Point(14, 101);
            this.lblCompanyName.Moveable = false;
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.NameOfControl = null;
            this.lblCompanyName.Size = new System.Drawing.Size(82, 23);
            this.lblCompanyName.TabIndex = 40;
            this.lblCompanyName.Text = "Crocus";
            // 
            // grp_Select
            // 
            this.grp_Select.BackColor = System.Drawing.Color.Transparent;
            this.grp_Select.Controls.Add(this.CboYear);
            this.grp_Select.Controls.Add(this.CboCompany);
            this.grp_Select.Controls.Add(this.Label2);
            this.grp_Select.Controls.Add(this.Label1);
            this.grp_Select.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_Select.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.grp_Select.Location = new System.Drawing.Point(96, 155);
            this.grp_Select.Name = "grp_Select";
            this.grp_Select.Size = new System.Drawing.Size(323, 112);
            this.grp_Select.TabIndex = 36;
            this.grp_Select.TabStop = false;
            this.grp_Select.Text = "Select Company";
            // 
            // CboYear
            // 
            this.CboYear.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CboYear.AutoComplete = false;
            this.CboYear.AutoDropdown = false;
            this.CboYear.BackColor = System.Drawing.Color.PapayaWhip;
            this.CboYear.BackColorEven = System.Drawing.Color.White;
            this.CboYear.BackColorOdd = System.Drawing.Color.White;
            this.CboYear.ColumnNames = "";
            this.CboYear.ColumnWidthDefault = 175;
            this.CboYear.ColumnWidths = "";
            this.CboYear.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.CboYear.Fill_ComboID = 0;
            this.CboYear.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.CboYear.FormattingEnabled = true;
            this.CboYear.GroupType = 0;
            this.CboYear.HelpText = "Select Year";
            this.CboYear.IsMandatory = true;
            this.CboYear.LinkedColumnIndex = 0;
            this.CboYear.LinkedTextBox = null;
            this.CboYear.Location = new System.Drawing.Point(131, 59);
            this.CboYear.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.CboYear.Moveable = false;
            this.CboYear.Name = "CboYear";
            this.CboYear.NameOfControl = "Year";
            this.CboYear.OpenForm = null;
            this.CboYear.ShowBallonTip = false;
            this.CboYear.Size = new System.Drawing.Size(184, 23);
            this.CboYear.TabIndex = 2;
            // 
            // CboCompany
            // 
            this.CboCompany.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.CboCompany.AutoComplete = false;
            this.CboCompany.AutoDropdown = false;
            this.CboCompany.BackColor = System.Drawing.Color.PapayaWhip;
            this.CboCompany.BackColorEven = System.Drawing.Color.White;
            this.CboCompany.BackColorOdd = System.Drawing.Color.White;
            this.CboCompany.ColumnNames = "";
            this.CboCompany.ColumnWidthDefault = 175;
            this.CboCompany.ColumnWidths = "";
            this.CboCompany.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.CboCompany.Fill_ComboID = 0;
            this.CboCompany.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.CboCompany.FormattingEnabled = true;
            this.CboCompany.GroupType = 0;
            this.CboCompany.HelpText = "Select Company";
            this.CboCompany.IsMandatory = true;
            this.CboCompany.LinkedColumnIndex = 0;
            this.CboCompany.LinkedTextBox = null;
            this.CboCompany.Location = new System.Drawing.Point(131, 31);
            this.CboCompany.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.CboCompany.Moveable = false;
            this.CboCompany.Name = "CboCompany";
            this.CboCompany.NameOfControl = "Company";
            this.CboCompany.OpenForm = null;
            this.CboCompany.ShowBallonTip = false;
            this.CboCompany.Size = new System.Drawing.Size(184, 23);
            this.CboCompany.TabIndex = 1;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.ForeColor = System.Drawing.Color.White;
            this.Label2.Location = new System.Drawing.Point(8, 60);
            this.Label2.Moveable = false;
            this.Label2.Name = "Label2";
            this.Label2.NameOfControl = null;
            this.Label2.Size = new System.Drawing.Size(111, 16);
            this.Label2.TabIndex = 1;
            this.Label2.Text = "Financial Year";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.ForeColor = System.Drawing.Color.White;
            this.Label1.Location = new System.Drawing.Point(8, 33);
            this.Label1.Moveable = false;
            this.Label1.Name = "Label1";
            this.Label1.NameOfControl = null;
            this.Label1.Size = new System.Drawing.Size(123, 16);
            this.Label1.TabIndex = 0;
            this.Label1.Text = "Company Name";
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyright.Font = new System.Drawing.Font("Lucida Bright", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblCopyright.Location = new System.Drawing.Point(360, 75);
            this.lblCopyright.Moveable = false;
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.NameOfControl = null;
            this.lblCopyright.Size = new System.Drawing.Size(202, 15);
            this.lblCopyright.TabIndex = 39;
            this.lblCopyright.Text = "Crocus IT Solutions Pvt. Ltd.";
            // 
            // timerFadeOut
            // 
            this.timerFadeOut.Interval = 10;
            this.timerFadeOut.Tick += new System.EventHandler(this.timerFadeOut_Tick);
            // 
            // timerFadeIn
            // 
            this.timerFadeIn.Interval = 20;
            this.timerFadeIn.Tick += new System.EventHandler(this.timerFadeIn_Tick);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.BackColor = System.Drawing.Color.Teal;
            this.btn_Cancel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.InnerBorderColor = System.Drawing.Color.Gainsboro;
            this.btn_Cancel.Location = new System.Drawing.Point(342, 271);
            this.btn_Cancel.Moveable = false;
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 32);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "&Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Select
            // 
            this.btn_Select.BackColor = System.Drawing.Color.Teal;
            this.btn_Select.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Select.InnerBorderColor = System.Drawing.Color.Gainsboro;
            this.btn_Select.Location = new System.Drawing.Point(262, 271);
            this.btn_Select.Moveable = false;
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(74, 32);
            this.btn_Select.TabIndex = 3;
            this.btn_Select.Text = "&Select";
            this.btn_Select.UseVisualStyleBackColor = false;
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // frmCompSelection
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImage = global::CIS_Textil.Properties.Resources.Intro;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(568, 425);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Select);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.lblCompanyName);
            this.Controls.Add(this.grp_Select);
            this.Controls.Add(this.lblCopyright);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(500, 350);
            this.Name = "frmCompSelection";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Control;
            this.Load += new System.EventHandler(this.frmCompSection_Load);
            this.grp_Select.ResumeLayout(false);
            this.grp_Select.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal CIS_CLibrary.CIS_TextLabel Label5;
        internal CIS_CLibrary.CIS_TextLabel lblCompanyName;
        internal System.Windows.Forms.GroupBox grp_Select;
        internal CIS_CLibrary.CIS_TextLabel Label2;
        internal CIS_CLibrary.CIS_TextLabel Label1;
        internal CIS_CLibrary.CIS_TextLabel lblCopyright;
        private System.Windows.Forms.Timer timerFadeOut;
        private System.Windows.Forms.Timer timerFadeIn;
        internal CIS_CLibrary.CIS_Button btn_Cancel;
        internal CIS_CLibrary.CIS_Button btn_Select;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox CboCompany;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox CboYear;
    }
}