namespace CIS_Textil
{
    partial class frmCatalogMerging
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
            this.grpCataologTo = new CIS_Utilities.CIS_GroupBox();
            this.cboCatalogTo = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblBookToColon = new System.Windows.Forms.Label();
            this.lblBookTo = new System.Windows.Forms.Label();
            this.grpCatalogFrom = new CIS_Utilities.CIS_GroupBox();
            this.cboCatalogFrom = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblBookFromColon = new System.Windows.Forms.Label();
            this.lblCatalogFrom = new System.Windows.Forms.Label();
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.grpCataologTo.SuspendLayout();
            this.grpCatalogFrom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.Add(this.grpCataologTo);
            this.pnlContent.Controls.Add(this.grpCatalogFrom);
            this.pnlContent.Controls.SetChildIndex(this.grpCatalogFrom, 0);
            this.pnlContent.Controls.SetChildIndex(this.grpCataologTo, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            // 
            // grpCataologTo
            // 
            this.grpCataologTo.BackgroundColor = System.Drawing.Color.White;
            this.grpCataologTo.BackgroundGradientColor = System.Drawing.Color.White;
            this.grpCataologTo.BackgroundGradientMode = CIS_Utilities.CIS_GroupBox.GroupBoxGradientMode.None;
            this.grpCataologTo.BorderColor = System.Drawing.Color.Black;
            this.grpCataologTo.BorderThickness = 1F;
            this.grpCataologTo.Controls.Add(this.cboCatalogTo);
            this.grpCataologTo.Controls.Add(this.lblBookToColon);
            this.grpCataologTo.Controls.Add(this.lblBookTo);
            this.grpCataologTo.CustomGroupBoxColor = System.Drawing.Color.Transparent;
            this.grpCataologTo.Flat = 1;
            this.grpCataologTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCataologTo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpCataologTo.GroupImage = null;
            this.grpCataologTo.GroupTitle = "Catalog To";
            this.grpCataologTo.Location = new System.Drawing.Point(406, 49);
            this.grpCataologTo.Name = "grpCataologTo";
            this.grpCataologTo.Padding = new System.Windows.Forms.Padding(20);
            this.grpCataologTo.PaintGroupBox = false;
            this.grpCataologTo.RoundCorners = 1;
            this.grpCataologTo.ShadowColor = System.Drawing.Color.DarkGray;
            this.grpCataologTo.ShadowControl = false;
            this.grpCataologTo.ShadowThickness = 3;
            this.grpCataologTo.Size = new System.Drawing.Size(351, 120);
            this.grpCataologTo.TabIndex = 6;
            // 
            // cboCatalogTo
            // 
            this.cboCatalogTo.AutoComplete = false;
            this.cboCatalogTo.AutoDropdown = false;
            this.cboCatalogTo.BackColor = System.Drawing.Color.White;
            this.cboCatalogTo.BackColorEven = System.Drawing.Color.White;
            this.cboCatalogTo.BackColorOdd = System.Drawing.Color.White;
            this.cboCatalogTo.ColumnNames = "";
            this.cboCatalogTo.ColumnWidthDefault = 175;
            this.cboCatalogTo.ColumnWidths = "";
            this.cboCatalogTo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboCatalogTo.Fill_ComboID = 0;
            this.cboCatalogTo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCatalogTo.FormattingEnabled = true;
            this.cboCatalogTo.GroupType = 0;
            this.cboCatalogTo.HelpText = null;
            this.cboCatalogTo.IsMandatory = false;
            this.cboCatalogTo.LinkedColumnIndex = 0;
            this.cboCatalogTo.LinkedTextBox = null;
            this.cboCatalogTo.Location = new System.Drawing.Point(142, 41);
            this.cboCatalogTo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboCatalogTo.Moveable = false;
            this.cboCatalogTo.Name = "cboCatalogTo";
            this.cboCatalogTo.NameOfControl = "Catalog Name";
            this.cboCatalogTo.OpenForm = null;
            this.cboCatalogTo.ShowBallonTip = false;
            this.cboCatalogTo.Size = new System.Drawing.Size(200, 22);
            this.cboCatalogTo.TabIndex = 2;
            // 
            // lblBookToColon
            // 
            this.lblBookToColon.AutoSize = true;
            this.lblBookToColon.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookToColon.Location = new System.Drawing.Point(125, 44);
            this.lblBookToColon.Name = "lblBookToColon";
            this.lblBookToColon.Size = new System.Drawing.Size(11, 13);
            this.lblBookToColon.TabIndex = 14;
            this.lblBookToColon.Text = ":";
            // 
            // lblBookTo
            // 
            this.lblBookTo.AutoSize = true;
            this.lblBookTo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookTo.Location = new System.Drawing.Point(11, 44);
            this.lblBookTo.Name = "lblBookTo";
            this.lblBookTo.Size = new System.Drawing.Size(97, 13);
            this.lblBookTo.TabIndex = 11;
            this.lblBookTo.Text = "Catalog Name";
            // 
            // grpCatalogFrom
            // 
            this.grpCatalogFrom.BackgroundColor = System.Drawing.Color.White;
            this.grpCatalogFrom.BackgroundGradientColor = System.Drawing.Color.White;
            this.grpCatalogFrom.BackgroundGradientMode = CIS_Utilities.CIS_GroupBox.GroupBoxGradientMode.None;
            this.grpCatalogFrom.BorderColor = System.Drawing.Color.Black;
            this.grpCatalogFrom.BorderThickness = 1F;
            this.grpCatalogFrom.Controls.Add(this.cboCatalogFrom);
            this.grpCatalogFrom.Controls.Add(this.lblBookFromColon);
            this.grpCatalogFrom.Controls.Add(this.lblCatalogFrom);
            this.grpCatalogFrom.CustomGroupBoxColor = System.Drawing.Color.Transparent;
            this.grpCatalogFrom.Flat = 1;
            this.grpCatalogFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpCatalogFrom.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpCatalogFrom.GroupImage = null;
            this.grpCatalogFrom.GroupTitle = "Catalog From";
            this.grpCatalogFrom.Location = new System.Drawing.Point(33, 49);
            this.grpCatalogFrom.Name = "grpCatalogFrom";
            this.grpCatalogFrom.Padding = new System.Windows.Forms.Padding(20);
            this.grpCatalogFrom.PaintGroupBox = false;
            this.grpCatalogFrom.RoundCorners = 1;
            this.grpCatalogFrom.ShadowColor = System.Drawing.Color.DarkGray;
            this.grpCatalogFrom.ShadowControl = false;
            this.grpCatalogFrom.ShadowThickness = 3;
            this.grpCatalogFrom.Size = new System.Drawing.Size(351, 120);
            this.grpCatalogFrom.TabIndex = 5;
            // 
            // cboCatalogFrom
            // 
            this.cboCatalogFrom.AutoComplete = false;
            this.cboCatalogFrom.AutoDropdown = false;
            this.cboCatalogFrom.BackColor = System.Drawing.Color.White;
            this.cboCatalogFrom.BackColorEven = System.Drawing.Color.White;
            this.cboCatalogFrom.BackColorOdd = System.Drawing.Color.White;
            this.cboCatalogFrom.ColumnNames = "";
            this.cboCatalogFrom.ColumnWidthDefault = 175;
            this.cboCatalogFrom.ColumnWidths = "";
            this.cboCatalogFrom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboCatalogFrom.Fill_ComboID = 0;
            this.cboCatalogFrom.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCatalogFrom.FormattingEnabled = true;
            this.cboCatalogFrom.GroupType = 0;
            this.cboCatalogFrom.HelpText = null;
            this.cboCatalogFrom.IsMandatory = false;
            this.cboCatalogFrom.LinkedColumnIndex = 0;
            this.cboCatalogFrom.LinkedTextBox = null;
            this.cboCatalogFrom.Location = new System.Drawing.Point(143, 41);
            this.cboCatalogFrom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboCatalogFrom.Moveable = false;
            this.cboCatalogFrom.Name = "cboCatalogFrom";
            this.cboCatalogFrom.NameOfControl = "Catalog Name";
            this.cboCatalogFrom.OpenForm = null;
            this.cboCatalogFrom.ShowBallonTip = false;
            this.cboCatalogFrom.Size = new System.Drawing.Size(200, 22);
            this.cboCatalogFrom.TabIndex = 1;
            // 
            // lblBookFromColon
            // 
            this.lblBookFromColon.AutoSize = true;
            this.lblBookFromColon.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookFromColon.Location = new System.Drawing.Point(127, 44);
            this.lblBookFromColon.Name = "lblBookFromColon";
            this.lblBookFromColon.Size = new System.Drawing.Size(11, 13);
            this.lblBookFromColon.TabIndex = 14;
            this.lblBookFromColon.Text = ":";
            // 
            // lblCatalogFrom
            // 
            this.lblCatalogFrom.AutoSize = true;
            this.lblCatalogFrom.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCatalogFrom.Location = new System.Drawing.Point(10, 44);
            this.lblCatalogFrom.Name = "lblCatalogFrom";
            this.lblCatalogFrom.Size = new System.Drawing.Size(97, 13);
            this.lblCatalogFrom.TabIndex = 11;
            this.lblCatalogFrom.Text = "Catalog Name";
            // 
            // txtCode
            // 
            this.txtCode.AutoFillDate = false;
            this.txtCode.BackColor = System.Drawing.Color.White;
            this.txtCode.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtCode.CheckForSymbol = null;
            this.txtCode.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtCode.DecimalPlace = 0;
            this.txtCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.HelpText = "";
            this.txtCode.HoldMyText = null;
            this.txtCode.IsMandatory = false;
            this.txtCode.IsSingleQuote = true;
            this.txtCode.IsSysmbol = false;
            this.txtCode.Location = new System.Drawing.Point(8, 3);
            this.txtCode.Mask = null;
            this.txtCode.Moveable = false;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(42, 21);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 7;
            this.txtCode.Visible = false;
            // 
            // frmCatalogMerging
            // 
            this.ClientSize = new System.Drawing.Size(808, 545);
            this.Name = "frmCatalogMerging";
            this.Load += new System.EventHandler(this.frmCatalogMerging_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.grpCataologTo.ResumeLayout(false);
            this.grpCataologTo.PerformLayout();
            this.grpCatalogFrom.ResumeLayout(false);
            this.grpCatalogFrom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CIS_Utilities.CIS_GroupBox grpCataologTo;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboCatalogTo;
        private System.Windows.Forms.Label lblBookToColon;
        private System.Windows.Forms.Label lblBookTo;
        private CIS_Utilities.CIS_GroupBox grpCatalogFrom;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboCatalogFrom;
        private System.Windows.Forms.Label lblBookFromColon;
        private System.Windows.Forms.Label lblCatalogFrom;
        public CIS_CLibrary.CIS_Textbox txtCode;
    }
}
