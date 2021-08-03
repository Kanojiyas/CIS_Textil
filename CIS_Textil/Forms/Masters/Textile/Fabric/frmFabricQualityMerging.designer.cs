namespace CIS_Textil
{
    partial class frmFabricQualityMerging
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
            this.lblCompColon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.CboQualityFrom = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblQualityFrom = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.label1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.CboQualityTo = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblQualityTo = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.grpFrom = new System.Windows.Forms.GroupBox();
            this.grpTo = new System.Windows.Forms.GroupBox();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.grpFrom.SuspendLayout();
            this.grpTo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.grpTo);
            this.pnlContent.Controls.Add(this.grpFrom);
            this.pnlContent.Controls.SetChildIndex(this.grpFrom, 0);
            this.pnlContent.Controls.SetChildIndex(this.grpTo, 0);
            // 
            // lblCompColon
            // 
            this.lblCompColon.AutoSize = true;
            this.lblCompColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblCompColon.Location = new System.Drawing.Point(107, 58);
            this.lblCompColon.Moveable = false;
            this.lblCompColon.Name = "lblCompColon";
            this.lblCompColon.NameOfControl = null;
            this.lblCompColon.Size = new System.Drawing.Size(12, 14);
            this.lblCompColon.TabIndex = 145;
            this.lblCompColon.Text = ":";
            // 
            // CboQualityFrom
            // 
            this.CboQualityFrom.AutoComplete = false;
            this.CboQualityFrom.AutoDropdown = false;
            this.CboQualityFrom.BackColor = System.Drawing.Color.White;
            this.CboQualityFrom.BackColorEven = System.Drawing.Color.White;
            this.CboQualityFrom.BackColorOdd = System.Drawing.Color.White;
            this.CboQualityFrom.ColumnNames = "";
            this.CboQualityFrom.ColumnWidthDefault = 175;
            this.CboQualityFrom.ColumnWidths = "";
            this.CboQualityFrom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.CboQualityFrom.Fill_ComboID = 0;
            this.CboQualityFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboQualityFrom.FormattingEnabled = true;
            this.CboQualityFrom.GroupType = 0;
            this.CboQualityFrom.HelpText = null;
            this.CboQualityFrom.IsMandatory = false;
            this.CboQualityFrom.LinkedColumnIndex = 0;
            this.CboQualityFrom.LinkedTextBox = null;
            this.CboQualityFrom.Location = new System.Drawing.Point(120, 55);
            this.CboQualityFrom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.CboQualityFrom.Moveable = false;
            this.CboQualityFrom.Name = "CboQualityFrom";
            this.CboQualityFrom.NameOfControl = null;
            this.CboQualityFrom.OpenForm = "frmYarnTypeMaster";
            this.CboQualityFrom.ShowBallonTip = false;
            this.CboQualityFrom.Size = new System.Drawing.Size(201, 23);
            this.CboQualityFrom.TabIndex = 144;
            // 
            // lblQualityFrom
            // 
            this.lblQualityFrom.AutoSize = true;
            this.lblQualityFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblQualityFrom.Location = new System.Drawing.Point(14, 59);
            this.lblQualityFrom.Moveable = false;
            this.lblQualityFrom.Name = "lblQualityFrom";
            this.lblQualityFrom.NameOfControl = null;
            this.lblQualityFrom.Size = new System.Drawing.Size(97, 14);
            this.lblQualityFrom.TabIndex = 143;
            this.lblQualityFrom.Text = "Quality Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(108, 58);
            this.label1.Moveable = false;
            this.label1.Name = "label1";
            this.label1.NameOfControl = null;
            this.label1.Size = new System.Drawing.Size(12, 14);
            this.label1.TabIndex = 148;
            this.label1.Text = ":";
            // 
            // CboQualityTo
            // 
            this.CboQualityTo.AutoComplete = false;
            this.CboQualityTo.AutoDropdown = false;
            this.CboQualityTo.BackColor = System.Drawing.Color.White;
            this.CboQualityTo.BackColorEven = System.Drawing.Color.White;
            this.CboQualityTo.BackColorOdd = System.Drawing.Color.White;
            this.CboQualityTo.ColumnNames = "";
            this.CboQualityTo.ColumnWidthDefault = 175;
            this.CboQualityTo.ColumnWidths = "";
            this.CboQualityTo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.CboQualityTo.Fill_ComboID = 0;
            this.CboQualityTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboQualityTo.FormattingEnabled = true;
            this.CboQualityTo.GroupType = 0;
            this.CboQualityTo.HelpText = null;
            this.CboQualityTo.IsMandatory = false;
            this.CboQualityTo.LinkedColumnIndex = 0;
            this.CboQualityTo.LinkedTextBox = null;
            this.CboQualityTo.Location = new System.Drawing.Point(121, 55);
            this.CboQualityTo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.CboQualityTo.Moveable = false;
            this.CboQualityTo.Name = "CboQualityTo";
            this.CboQualityTo.NameOfControl = null;
            this.CboQualityTo.OpenForm = "frmYarnTypeMaster";
            this.CboQualityTo.ShowBallonTip = false;
            this.CboQualityTo.Size = new System.Drawing.Size(201, 23);
            this.CboQualityTo.TabIndex = 147;
            // 
            // lblQualityTo
            // 
            this.lblQualityTo.AutoSize = true;
            this.lblQualityTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblQualityTo.Location = new System.Drawing.Point(7, 61);
            this.lblQualityTo.Moveable = false;
            this.lblQualityTo.Name = "lblQualityTo";
            this.lblQualityTo.NameOfControl = null;
            this.lblQualityTo.Size = new System.Drawing.Size(97, 14);
            this.lblQualityTo.TabIndex = 149;
            this.lblQualityTo.Text = "Quality Name";
            // 
            // grpFrom
            // 
            this.grpFrom.Controls.Add(this.CboQualityFrom);
            this.grpFrom.Controls.Add(this.lblQualityFrom);
            this.grpFrom.Controls.Add(this.lblCompColon);
            this.grpFrom.Location = new System.Drawing.Point(38, 58);
            this.grpFrom.Name = "grpFrom";
            this.grpFrom.Size = new System.Drawing.Size(337, 133);
            this.grpFrom.TabIndex = 150;
            this.grpFrom.TabStop = false;
            this.grpFrom.Text = "Fabric Quality From";
            // 
            // grpTo
            // 
            this.grpTo.Controls.Add(this.CboQualityTo);
            this.grpTo.Controls.Add(this.label1);
            this.grpTo.Controls.Add(this.lblQualityTo);
            this.grpTo.Location = new System.Drawing.Point(422, 58);
            this.grpTo.Name = "grpTo";
            this.grpTo.Size = new System.Drawing.Size(337, 133);
            this.grpTo.TabIndex = 151;
            this.grpTo.TabStop = false;
            this.grpTo.Text = "Fabric Quality To";
            // 
            // frmFabricQualityMerging
            // 
            this.ClientSize = new System.Drawing.Size(808, 545);
            this.Name = "frmFabricQualityMerging";
            this.Load += new System.EventHandler(this.frmQualityMerging_Load);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.grpFrom.ResumeLayout(false);
            this.grpFrom.PerformLayout();
            this.grpTo.ResumeLayout(false);
            this.grpTo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal CIS_CLibrary.CIS_TextLabel lblCompColon;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox CboQualityFrom;
        internal CIS_CLibrary.CIS_TextLabel lblQualityFrom;
        internal CIS_CLibrary.CIS_TextLabel lblQualityTo;
        internal CIS_CLibrary.CIS_TextLabel label1;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox CboQualityTo;
        private System.Windows.Forms.GroupBox grpTo;
        private System.Windows.Forms.GroupBox grpFrom;
    }
}
