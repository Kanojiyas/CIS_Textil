namespace CIS_Textil
{
    partial class NewColumnDialog
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
            Infragistics.Win.UltraWinEditors.EditorButton editorButton1 = new Infragistics.Win.UltraWinEditors.EditorButton();
            Infragistics.Win.Appearance Appearance1 = new Infragistics.Win.Appearance();
            this.ultraLabelFormula = new Infragistics.Win.Misc.UltraLabel();
            this.ultraTextEditorFormula = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraButtonCancel = new Infragistics.Win.Misc.UltraButton();
            this.ultraButtonOk = new Infragistics.Win.Misc.UltraButton();
            this.ultraComboEditorType = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraTextEditorName = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTextEditorFormula)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraComboEditorType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTextEditorName)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraLabelFormula
            // 
            this.ultraLabelFormula.Location = new System.Drawing.Point(8, 64);
            this.ultraLabelFormula.Name = "ultraLabelFormula";
            this.ultraLabelFormula.Size = new System.Drawing.Size(100, 23);
            this.ultraLabelFormula.TabIndex = 15;
            this.ultraLabelFormula.Text = "Formula (optional)";
            // 
            // ultraTextEditorFormula
            // 
            Appearance1.FontData.BoldAsString = "True";
            Appearance1.TextHAlignAsString = "Center";
            Appearance1.TextVAlignAsString = "Middle";
            editorButton1.Appearance = Appearance1;
            editorButton1.ButtonStyle = Infragistics.Win.UIElementButtonStyle.Button;
            editorButton1.Text = "...";
            this.ultraTextEditorFormula.ButtonsRight.Add(editorButton1);
            this.ultraTextEditorFormula.Location = new System.Drawing.Point(112, 64);
            this.ultraTextEditorFormula.Name = "ultraTextEditorFormula";
            this.ultraTextEditorFormula.Size = new System.Drawing.Size(144, 21);
            this.ultraTextEditorFormula.TabIndex = 14;
            // 
            // ultraButtonCancel
            // 
            this.ultraButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ultraButtonCancel.Location = new System.Drawing.Point(136, 96);
            this.ultraButtonCancel.Name = "ultraButtonCancel";
            this.ultraButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ultraButtonCancel.TabIndex = 13;
            this.ultraButtonCancel.Text = "&Cancel";
            this.ultraButtonCancel.Click += new System.EventHandler(this.UltraButtonCancel_Click);
            // 
            // ultraButtonOk
            // 
            this.ultraButtonOk.Location = new System.Drawing.Point(48, 96);
            this.ultraButtonOk.Name = "ultraButtonOk";
            this.ultraButtonOk.Size = new System.Drawing.Size(75, 23);
            this.ultraButtonOk.TabIndex = 12;
            this.ultraButtonOk.Text = "&Ok";
            this.ultraButtonOk.Click += new System.EventHandler(this.UltraButtonOk_Click);
            // 
            // ultraComboEditorType
            // 
            this.ultraComboEditorType.DropDownStyle = Infragistics.Win.DropDownStyle.DropDownList;
            this.ultraComboEditorType.Location = new System.Drawing.Point(112, 40);
            this.ultraComboEditorType.Name = "ultraComboEditorType";
            this.ultraComboEditorType.Size = new System.Drawing.Size(144, 21);
            this.ultraComboEditorType.TabIndex = 11;
            // 
            // ultraLabel2
            // 
            this.ultraLabel2.Location = new System.Drawing.Point(8, 40);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(100, 23);
            this.ultraLabel2.TabIndex = 10;
            this.ultraLabel2.Text = "Type";
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.Location = new System.Drawing.Point(8, 16);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(100, 23);
            this.ultraLabel1.TabIndex = 9;
            this.ultraLabel1.Text = "Name";
            // 
            // ultraTextEditorName
            // 
            this.ultraTextEditorName.Location = new System.Drawing.Point(112, 16);
            this.ultraTextEditorName.Name = "ultraTextEditorName";
            this.ultraTextEditorName.Size = new System.Drawing.Size(144, 21);
            this.ultraTextEditorName.TabIndex = 8;
            // 
            // NewColumnDialog
            // 
            this.AcceptButton = this.ultraButtonOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.ultraButtonCancel;
            this.ClientSize = new System.Drawing.Size(266, 128);
            this.Controls.Add(this.ultraLabelFormula);
            this.Controls.Add(this.ultraTextEditorFormula);
            this.Controls.Add(this.ultraButtonCancel);
            this.Controls.Add(this.ultraButtonOk);
            this.Controls.Add(this.ultraComboEditorType);
            this.Controls.Add(this.ultraLabel2);
            this.Controls.Add(this.ultraLabel1);
            this.Controls.Add(this.ultraTextEditorName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "NewColumnDialog";
            this.Text = "New Column";
            this.Load += new System.EventHandler(this.NewColumnDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraTextEditorFormula)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraComboEditorType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTextEditorName)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Infragistics.Win.Misc.UltraLabel ultraLabelFormula;
        internal Infragistics.Win.UltraWinEditors.UltraTextEditor ultraTextEditorFormula;
        internal Infragistics.Win.Misc.UltraButton ultraButtonCancel;
        internal Infragistics.Win.Misc.UltraButton ultraButtonOk;
        internal Infragistics.Win.UltraWinEditors.UltraComboEditor ultraComboEditorType;
        internal Infragistics.Win.Misc.UltraLabel ultraLabel2;
        internal Infragistics.Win.Misc.UltraLabel ultraLabel1;
        internal Infragistics.Win.UltraWinEditors.UltraTextEditor ultraTextEditorName;
    }
}