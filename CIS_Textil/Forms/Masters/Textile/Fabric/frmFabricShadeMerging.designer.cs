namespace CIS_Textil
{
    partial class frmFabricShadeMerging
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
            this.CboShadeFrom = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblDesignFrom = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.label1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.CboShadeTo = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
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
            // CboShadeFrom
            // 
            this.CboShadeFrom.AutoComplete = false;
            this.CboShadeFrom.AutoDropdown = false;
            this.CboShadeFrom.BackColor = System.Drawing.Color.White;
            this.CboShadeFrom.BackColorEven = System.Drawing.Color.White;
            this.CboShadeFrom.BackColorOdd = System.Drawing.Color.White;
            this.CboShadeFrom.ColumnNames = "";
            this.CboShadeFrom.ColumnWidthDefault = 175;
            this.CboShadeFrom.ColumnWidths = "";
            this.CboShadeFrom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.CboShadeFrom.Fill_ComboID = 0;
            this.CboShadeFrom.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboShadeFrom.FormattingEnabled = true;
            this.CboShadeFrom.GroupType = 0;
            this.CboShadeFrom.HelpText = null;
            this.CboShadeFrom.IsMandatory = false;
            this.CboShadeFrom.LinkedColumnIndex = 0;
            this.CboShadeFrom.LinkedTextBox = null;
            this.CboShadeFrom.Location = new System.Drawing.Point(120, 55);
            this.CboShadeFrom.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.CboShadeFrom.Moveable = false;
            this.CboShadeFrom.Name = "CboShadeFrom";
            this.CboShadeFrom.NameOfControl = null;
            this.CboShadeFrom.OpenForm = "frmFabricShadeMaster";
            this.CboShadeFrom.ShowBallonTip = false;
            this.CboShadeFrom.Size = new System.Drawing.Size(201, 23);
            this.CboShadeFrom.TabIndex = 144;
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
            this.lblDesignFrom.Text = "Shade Name";
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
            // CboShadeTo
            // 
            this.CboShadeTo.AutoComplete = false;
            this.CboShadeTo.AutoDropdown = false;
            this.CboShadeTo.BackColor = System.Drawing.Color.White;
            this.CboShadeTo.BackColorEven = System.Drawing.Color.White;
            this.CboShadeTo.BackColorOdd = System.Drawing.Color.White;
            this.CboShadeTo.ColumnNames = "";
            this.CboShadeTo.ColumnWidthDefault = 175;
            this.CboShadeTo.ColumnWidths = "";
            this.CboShadeTo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.CboShadeTo.Fill_ComboID = 0;
            this.CboShadeTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CboShadeTo.FormattingEnabled = true;
            this.CboShadeTo.GroupType = 0;
            this.CboShadeTo.HelpText = null;
            this.CboShadeTo.IsMandatory = false;
            this.CboShadeTo.LinkedColumnIndex = 0;
            this.CboShadeTo.LinkedTextBox = null;
            this.CboShadeTo.Location = new System.Drawing.Point(121, 55);
            this.CboShadeTo.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.CboShadeTo.Moveable = false;
            this.CboShadeTo.Name = "CboShadeTo";
            this.CboShadeTo.NameOfControl = null;
            this.CboShadeTo.OpenForm = "frmFabricShadeMaster";
            this.CboShadeTo.ShowBallonTip = false;
            this.CboShadeTo.Size = new System.Drawing.Size(201, 23);
            this.CboShadeTo.TabIndex = 147;
            // 
            // lblDesignTo
            // 
            this.lblDesignTo.AutoSize = true;
            this.lblDesignTo.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblDesignTo.Location = new System.Drawing.Point(7, 61);
            this.lblDesignTo.Moveable = false;
            this.lblDesignTo.Name = "lblDesignTo";
            this.lblDesignTo.NameOfControl = null;
            this.lblDesignTo.Size = new System.Drawing.Size(90, 14);
            this.lblDesignTo.TabIndex = 149;
            this.lblDesignTo.Text = "Shade Name";
            // 
            // grpFrom
            // 
            this.grpFrom.Controls.Add(this.CboShadeFrom);
            this.grpFrom.Controls.Add(this.lblDesignFrom);
            this.grpFrom.Controls.Add(this.lblCompColon);
            this.grpFrom.Location = new System.Drawing.Point(45, 58);
            this.grpFrom.Name = "grpFrom";
            this.grpFrom.Size = new System.Drawing.Size(337, 133);
            this.grpFrom.TabIndex = 150;
            this.grpFrom.TabStop = false;
            this.grpFrom.Text = "Fabric Shade From";
            // 
            // grpTo
            // 
            this.grpTo.Controls.Add(this.CboShadeTo);
            this.grpTo.Controls.Add(this.label1);
            this.grpTo.Controls.Add(this.lblDesignTo);
            this.grpTo.Location = new System.Drawing.Point(419, 58);
            this.grpTo.Name = "grpTo";
            this.grpTo.Size = new System.Drawing.Size(337, 133);
            this.grpTo.TabIndex = 151;
            this.grpTo.TabStop = false;
            this.grpTo.Text = "Fabric Shade To";
            // 
            // frmFabricShadeMerging
            // 
            this.ClientSize = new System.Drawing.Size(808, 545);
            this.Name = "frmFabricShadeMerging";
            this.Load += new System.EventHandler(this.frmShadeMerging_Load);
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
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox CboShadeFrom;
        internal CIS_CLibrary.CIS_TextLabel lblDesignFrom;
        internal CIS_CLibrary.CIS_TextLabel lblDesignTo;
        internal CIS_CLibrary.CIS_TextLabel label1;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox CboShadeTo;
        private System.Windows.Forms.GroupBox grpTo;
        private System.Windows.Forms.GroupBox grpFrom;
    }
}
