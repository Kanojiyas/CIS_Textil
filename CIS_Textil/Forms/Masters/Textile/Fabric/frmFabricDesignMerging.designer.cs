namespace CIS_Textil
{
    partial class frmFabricDesignMerging
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
            this.CboDesignFrom = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblDesignFrom = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.label1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.CboDesignTo = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblDesignTo = new CIS_CLibrary.CIS_TextLabel(this.components);
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
            // CboDesignFrom
            // 
            this.CboDesignFrom.AutoComplete = false;
            this.CboDesignFrom.AutoDropdown = false;
            this.CboDesignFrom.BackColor = System.Drawing.Color.White;
            this.CboDesignFrom.BackColorEven = System.Drawing.Color.White;
            this.CboDesignFrom.BackColorOdd = System.Drawing.Color.White;
            this.CboDesignFrom.ColumnNames = "";
            this.CboDesignFrom.ColumnWidthDefault = 175;
            this.CboDesignFrom.ColumnWidths = "";
            this.CboDesignFrom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.CboDesignFrom.Fill_ComboID = 0;
            this.CboDesignFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboDesignFrom.FormattingEnabled = true;
            this.CboDesignFrom.GroupType = 0;
            this.CboDesignFrom.HelpText = null;
            this.CboDesignFrom.IsMandatory = false;
            this.CboDesignFrom.LinkedColumnIndex = 0;
            this.CboDesignFrom.LinkedTextBox = null;
            this.CboDesignFrom.Location = new System.Drawing.Point(120, 55);
            this.CboDesignFrom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.CboDesignFrom.Moveable = false;
            this.CboDesignFrom.Name = "CboDesignFrom";
            this.CboDesignFrom.NameOfControl = null;
            this.CboDesignFrom.OpenForm = "frmYarnTypeMaster";
            this.CboDesignFrom.ShowBallonTip = false;
            this.CboDesignFrom.Size = new System.Drawing.Size(201, 23);
            this.CboDesignFrom.TabIndex = 144;
            // 
            // lblDesignFrom
            // 
            this.lblDesignFrom.AutoSize = true;
            this.lblDesignFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblDesignFrom.Location = new System.Drawing.Point(14, 59);
            this.lblDesignFrom.Moveable = false;
            this.lblDesignFrom.Name = "lblDesignFrom";
            this.lblDesignFrom.NameOfControl = null;
            this.lblDesignFrom.Size = new System.Drawing.Size(90, 14);
            this.lblDesignFrom.TabIndex = 143;
            this.lblDesignFrom.Text = "Design From";
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
            // CboDesignTo
            // 
            this.CboDesignTo.AutoComplete = false;
            this.CboDesignTo.AutoDropdown = false;
            this.CboDesignTo.BackColor = System.Drawing.Color.White;
            this.CboDesignTo.BackColorEven = System.Drawing.Color.White;
            this.CboDesignTo.BackColorOdd = System.Drawing.Color.White;
            this.CboDesignTo.ColumnNames = "";
            this.CboDesignTo.ColumnWidthDefault = 175;
            this.CboDesignTo.ColumnWidths = "";
            this.CboDesignTo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.CboDesignTo.Fill_ComboID = 0;
            this.CboDesignTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboDesignTo.FormattingEnabled = true;
            this.CboDesignTo.GroupType = 0;
            this.CboDesignTo.HelpText = null;
            this.CboDesignTo.IsMandatory = false;
            this.CboDesignTo.LinkedColumnIndex = 0;
            this.CboDesignTo.LinkedTextBox = null;
            this.CboDesignTo.Location = new System.Drawing.Point(121, 55);
            this.CboDesignTo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.CboDesignTo.Moveable = false;
            this.CboDesignTo.Name = "CboDesignTo";
            this.CboDesignTo.NameOfControl = null;
            this.CboDesignTo.OpenForm = "frmYarnTypeMaster";
            this.CboDesignTo.ShowBallonTip = false;
            this.CboDesignTo.Size = new System.Drawing.Size(201, 23);
            this.CboDesignTo.TabIndex = 147;
            // 
            // lblDesignTo
            // 
            this.lblDesignTo.AutoSize = true;
            this.lblDesignTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblDesignTo.Location = new System.Drawing.Point(7, 61);
            this.lblDesignTo.Moveable = false;
            this.lblDesignTo.Name = "lblDesignTo";
            this.lblDesignTo.NameOfControl = null;
            this.lblDesignTo.Size = new System.Drawing.Size(72, 14);
            this.lblDesignTo.TabIndex = 149;
            this.lblDesignTo.Text = "Design To";
            // 
            // grpFrom
            // 
            this.grpFrom.Controls.Add(this.CboDesignFrom);
            this.grpFrom.Controls.Add(this.lblDesignFrom);
            this.grpFrom.Controls.Add(this.lblCompColon);
            this.grpFrom.Location = new System.Drawing.Point(22, 58);
            this.grpFrom.Name = "grpFrom";
            this.grpFrom.Size = new System.Drawing.Size(337, 133);
            this.grpFrom.TabIndex = 150;
            this.grpFrom.TabStop = false;
            this.grpFrom.Text = "Fabric Design From";
            // 
            // grpTo
            // 
            this.grpTo.Controls.Add(this.CboDesignTo);
            this.grpTo.Controls.Add(this.label1);
            this.grpTo.Controls.Add(this.lblDesignTo);
            this.grpTo.Location = new System.Drawing.Point(433, 58);
            this.grpTo.Name = "grpTo";
            this.grpTo.Size = new System.Drawing.Size(337, 133);
            this.grpTo.TabIndex = 151;
            this.grpTo.TabStop = false;
            this.grpTo.Text = "Fabric Design To";
            // 
            // frmFabricDesignMerging
            // 
            this.ClientSize = new System.Drawing.Size(808, 545);
            this.Name = "frmFabricDesignMerging";
            this.Load += new System.EventHandler(this.frmDesignMerging_Load);
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
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox CboDesignFrom;
        internal CIS_CLibrary.CIS_TextLabel lblDesignFrom;
        internal CIS_CLibrary.CIS_TextLabel lblDesignTo;
        internal CIS_CLibrary.CIS_TextLabel label1;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox CboDesignTo;
        private System.Windows.Forms.GroupBox grpTo;
        private System.Windows.Forms.GroupBox grpFrom;
    }
}
