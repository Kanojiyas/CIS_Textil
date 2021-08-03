namespace CIS_Textil
{
    partial class frmToDoList
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
            this.txtTopic = new CIS_CLibrary.CIS_Textbox();
            this.lblMiscNameColon = new CIS_CLibrary.CIS_TextLabel();
            this.lblTOpic = new CIS_CLibrary.CIS_TextLabel();
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.label1 = new CIS_CLibrary.CIS_TextLabel();
            this.lblPriority = new CIS_CLibrary.CIS_TextLabel();
            this.cboPriority = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.ChkActive = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.ChkActive);
            this.pnlContent.Controls.Add(this.cboPriority);
            this.pnlContent.Controls.Add(this.label1);
            this.pnlContent.Controls.Add(this.lblPriority);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.Add(this.txtTopic);
            this.pnlContent.Controls.Add(this.lblMiscNameColon);
            this.pnlContent.Controls.Add(this.lblTOpic);
            this.pnlContent.Controls.SetChildIndex(this.lblTOpic, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblMiscNameColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtTopic, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblPriority, 0);
            this.pnlContent.Controls.SetChildIndex(this.label1, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboPriority, 0);
            this.pnlContent.Controls.SetChildIndex(this.ChkActive, 0);
            // 
            // txtTopic
            // 
            this.txtTopic.AutoFillDate = false;
            this.txtTopic.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtTopic.CheckForSymbol = null;
            this.txtTopic.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtTopic.DecimalPlace = 0;
            this.txtTopic.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTopic.HelpText = "Enter Misc. Name";
            this.txtTopic.HoldMyText = null;
            this.txtTopic.IsMandatory = true;
            this.txtTopic.IsSingleQuote = true;
            this.txtTopic.IsSysmbol = false;
            this.txtTopic.Location = new System.Drawing.Point(383, 15);
            this.txtTopic.Mask = null;
            this.txtTopic.MaxLength = 50;
            this.txtTopic.Multiline = true;
            this.txtTopic.Name = "txtTopic";
            this.txtTopic.NameOfControl = null;
            this.txtTopic.Prefix = null;
            this.txtTopic.ShowBallonTip = false;
            this.txtTopic.ShowErrorIcon = false;
            this.txtTopic.ShowMessage = null;
            this.txtTopic.Size = new System.Drawing.Size(296, 64);
            this.txtTopic.Suffix = null;
            this.txtTopic.TabIndex = 1008;
            // 
            // lblMiscNameColon
            // 
            this.lblMiscNameColon.AutoSize = true;
            this.lblMiscNameColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMiscNameColon.Location = new System.Drawing.Point(364, 39);
            this.lblMiscNameColon.Name = "lblMiscNameColon";
            this.lblMiscNameColon.Size = new System.Drawing.Size(12, 14);
            this.lblMiscNameColon.TabIndex = 1010;
            this.lblMiscNameColon.Text = ":";
            // 
            // lblTOpic
            // 
            this.lblTOpic.AutoSize = true;
            this.lblTOpic.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTOpic.Location = new System.Drawing.Point(307, 39);
            this.lblTOpic.Name = "lblTOpic";
            this.lblTOpic.Size = new System.Drawing.Size(42, 14);
            this.lblTOpic.TabIndex = 1009;
            this.lblTOpic.Text = "Topic";
            // 
            // txtCode
            // 
            this.txtCode.AutoFillDate = false;
            this.txtCode.BackColor = System.Drawing.Color.White;
            this.txtCode.CheckForSymbol = null;
            this.txtCode.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtCode.DecimalPlace = 0;
            this.txtCode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.HelpText = "";
            this.txtCode.HoldMyText = null;
            this.txtCode.IsMandatory = false;
            this.txtCode.IsSingleQuote = true;
            this.txtCode.IsSysmbol = false;
            this.txtCode.Location = new System.Drawing.Point(29, 6);
            this.txtCode.Mask = null;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(98, 20);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 1011;
            this.txtCode.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(364, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 14);
            this.label1.TabIndex = 1014;
            this.label1.Text = ":";
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPriority.Location = new System.Drawing.Point(307, 86);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(57, 14);
            this.lblPriority.TabIndex = 1013;
            this.lblPriority.Text = "Priority";
            // 
            // cboPriority
            // 
            this.cboPriority.AutoComplete = false;
            this.cboPriority.AutoDropdown = false;
            this.cboPriority.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboPriority.BackColorEven = System.Drawing.Color.White;
            this.cboPriority.BackColorOdd = System.Drawing.Color.White;
            this.cboPriority.ColumnNames = "";
            this.cboPriority.ColumnWidthDefault = 175;
            this.cboPriority.ColumnWidths = "";
            this.cboPriority.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboPriority.Fill_ComboID = 0;
            this.cboPriority.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPriority.FormattingEnabled = true;
            this.cboPriority.HelpText = "Select Type";
            this.cboPriority.IsMandatory = true;
            this.cboPriority.LinkedColumnIndex = 0;
            this.cboPriority.LinkedTextBox = null;
            this.cboPriority.Location = new System.Drawing.Point(383, 84);
            this.cboPriority.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboPriority.Name = "cboPriority";
            this.cboPriority.NameOfControl = null;
            this.cboPriority.OpenForm = null;
            this.cboPriority.ShowBallonTip = false;
            this.cboPriority.Size = new System.Drawing.Size(101, 21);
            this.cboPriority.TabIndex = 1015;
            // 
            // ChkActive
            // 
            this.ChkActive.AutoSize = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ChkActive.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ChkActive.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.ChkActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChkActive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.HelpText = "Checked If Active";
            this.ChkActive.Location = new System.Drawing.Point(383, 111);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.Size = new System.Drawing.Size(110, 18);
            this.ChkActive.TabIndex = 1046;
            this.ChkActive.Text = "Is Completed";
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // frmToDoList
            // 
            this.ClientSize = new System.Drawing.Size(955, 547);
            this.Name = "frmToDoList";
            this.Load += new System.EventHandler(this.frmToDoList_Load);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal CIS_CLibrary.CIS_Textbox txtTopic;
        internal CIS_CLibrary.CIS_TextLabel lblMiscNameColon;
        internal CIS_CLibrary.CIS_TextLabel lblTOpic;
        public CIS_CLibrary.CIS_Textbox txtCode;
        internal CIS_CLibrary.CIS_TextLabel label1;
        internal CIS_CLibrary.CIS_TextLabel lblPriority;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboPriority;
        internal CIS_CLibrary.CIS_CheckBox ChkActive;
    }
}
