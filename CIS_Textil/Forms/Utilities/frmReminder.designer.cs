namespace CIS_Textil
{
    partial class frmReminder
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
            this.label1 = new CIS_CLibrary.CIS_TextLabel();
            this.lblFromDt = new CIS_CLibrary.CIS_TextLabel();
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.txtTopic = new CIS_CLibrary.CIS_Textbox();
            this.lblMiscNameColon = new CIS_CLibrary.CIS_TextLabel();
            this.lblTOpic = new CIS_CLibrary.CIS_TextLabel();
            this.txtToDt = new CIS_CLibrary.CIS_Textbox();
            this.label2 = new CIS_CLibrary.CIS_TextLabel();
            this.lblToDate = new CIS_CLibrary.CIS_TextLabel();
            this.txtFromDt = new CIS_CLibrary.CIS_Textbox();
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
            this.pnlContent.Controls.Add(this.txtFromDt);
            this.pnlContent.Controls.Add(this.txtToDt);
            this.pnlContent.Controls.Add(this.label2);
            this.pnlContent.Controls.Add(this.lblToDate);
            this.pnlContent.Controls.Add(this.label1);
            this.pnlContent.Controls.Add(this.lblTOpic);
            this.pnlContent.Controls.Add(this.lblFromDt);
            this.pnlContent.Controls.Add(this.lblMiscNameColon);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.Add(this.txtTopic);
            this.pnlContent.Controls.SetChildIndex(this.txtTopic, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblMiscNameColon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblFromDt, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblTOpic, 0);
            this.pnlContent.Controls.SetChildIndex(this.label1, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblToDate, 0);
            this.pnlContent.Controls.SetChildIndex(this.label2, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtToDt, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtFromDt, 0);
            this.pnlContent.Controls.SetChildIndex(this.ChkActive, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(310, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 14);
            this.label1.TabIndex = 1021;
            this.label1.Text = ":";
            // 
            // lblFromDt
            // 
            this.lblFromDt.AutoSize = true;
            this.lblFromDt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFromDt.Location = new System.Drawing.Point(235, 86);
            this.lblFromDt.Name = "lblFromDt";
            this.lblFromDt.Size = new System.Drawing.Size(76, 14);
            this.lblFromDt.TabIndex = 1020;
            this.lblFromDt.Text = "From Date";
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
            this.txtCode.Location = new System.Drawing.Point(44, 3);
            this.txtCode.Mask = null;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(98, 20);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 1019;
            this.txtCode.Visible = false;
            // 
            // txtTopic
            // 
            this.txtTopic.AutoFillDate = false;
            this.txtTopic.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtTopic.CheckForSymbol = null;
            this.txtTopic.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtTopic.DecimalPlace = 0;
            this.txtTopic.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTopic.HelpText = "Enter Topic";
            this.txtTopic.HoldMyText = null;
            this.txtTopic.IsMandatory = true;
            this.txtTopic.IsSingleQuote = true;
            this.txtTopic.IsSysmbol = false;
            this.txtTopic.Location = new System.Drawing.Point(325, 12);
            this.txtTopic.Mask = null;
            this.txtTopic.MaxLength = 50;
            this.txtTopic.Multiline = true;
            this.txtTopic.Name = "txtTopic";
            this.txtTopic.NameOfControl = null;
            this.txtTopic.Prefix = null;
            this.txtTopic.ShowBallonTip = false;
            this.txtTopic.ShowErrorIcon = false;
            this.txtTopic.ShowMessage = null;
            this.txtTopic.Size = new System.Drawing.Size(407, 64);
            this.txtTopic.Suffix = null;
            this.txtTopic.TabIndex = 1;
            // 
            // lblMiscNameColon
            // 
            this.lblMiscNameColon.AutoSize = true;
            this.lblMiscNameColon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMiscNameColon.Location = new System.Drawing.Point(310, 36);
            this.lblMiscNameColon.Name = "lblMiscNameColon";
            this.lblMiscNameColon.Size = new System.Drawing.Size(12, 14);
            this.lblMiscNameColon.TabIndex = 1018;
            this.lblMiscNameColon.Text = ":";
            // 
            // lblTOpic
            // 
            this.lblTOpic.AutoSize = true;
            this.lblTOpic.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTOpic.Location = new System.Drawing.Point(235, 36);
            this.lblTOpic.Name = "lblTOpic";
            this.lblTOpic.Size = new System.Drawing.Size(42, 14);
            this.lblTOpic.TabIndex = 1017;
            this.lblTOpic.Text = "Topic";
            // 
            // txtToDt
            // 
            this.txtToDt.AutoFillDate = true;
            this.txtToDt.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtToDt.CheckForSymbol = null;
            this.txtToDt.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.ApplyDate;
            this.txtToDt.DecimalPlace = 0;
            this.txtToDt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtToDt.HelpText = "Enter To Date";
            this.txtToDt.HoldMyText = null;
            this.txtToDt.IsMandatory = true;
            this.txtToDt.IsSingleQuote = true;
            this.txtToDt.IsSysmbol = false;
            this.txtToDt.Location = new System.Drawing.Point(632, 82);
            this.txtToDt.Mask = null;
            this.txtToDt.MaxLength = 50;
            this.txtToDt.Name = "txtToDt";
            this.txtToDt.NameOfControl = null;
            this.txtToDt.Prefix = null;
            this.txtToDt.ShowBallonTip = false;
            this.txtToDt.ShowErrorIcon = false;
            this.txtToDt.ShowMessage = null;
            this.txtToDt.Size = new System.Drawing.Size(100, 20);
            this.txtToDt.Suffix = null;
            this.txtToDt.TabIndex = 3;
            this.txtToDt.Text = "__/__/____";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(616, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 14);
            this.label2.TabIndex = 1024;
            this.label2.Text = ":";
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToDate.Location = new System.Drawing.Point(556, 85);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(58, 14);
            this.lblToDate.TabIndex = 1023;
            this.lblToDate.Text = "To Date";
            // 
            // txtFromDt
            // 
            this.txtFromDt.AutoFillDate = true;
            this.txtFromDt.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtFromDt.CheckForSymbol = null;
            this.txtFromDt.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.ApplyDate;
            this.txtFromDt.DecimalPlace = 0;
            this.txtFromDt.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFromDt.HelpText = "Enter From Date";
            this.txtFromDt.HoldMyText = null;
            this.txtFromDt.IsMandatory = true;
            this.txtFromDt.IsSingleQuote = true;
            this.txtFromDt.IsSysmbol = false;
            this.txtFromDt.Location = new System.Drawing.Point(324, 82);
            this.txtFromDt.Mask = null;
            this.txtFromDt.MaxLength = 50;
            this.txtFromDt.Name = "txtFromDt";
            this.txtFromDt.NameOfControl = null;
            this.txtFromDt.Prefix = null;
            this.txtFromDt.ShowBallonTip = false;
            this.txtFromDt.ShowErrorIcon = false;
            this.txtFromDt.ShowMessage = null;
            this.txtFromDt.Size = new System.Drawing.Size(100, 20);
            this.txtFromDt.Suffix = null;
            this.txtFromDt.TabIndex = 2;
            this.txtFromDt.Text = "__/__/____";
            // 
            // ChkActive
            // 
            this.ChkActive.AutoSize = true;
             this.ChkActive.BackColor = System.Drawing.Color.Transparent;
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.ChkActive.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ChkActive.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.ChkActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChkActive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.HelpText = "Checked If Active";
            this.ChkActive.Location = new System.Drawing.Point(324, 108);
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.Size = new System.Drawing.Size(110, 18);
            this.ChkActive.TabIndex = 4;
            this.ChkActive.Text = "Active Status";
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // frmReminder
            // 
            this.ClientSize = new System.Drawing.Size(955, 547);
            this.Name = "frmReminder";
            this.Load += new System.EventHandler(this.frmReminder_Load);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal CIS_CLibrary.CIS_TextLabel label1;
        internal CIS_CLibrary.CIS_TextLabel lblFromDt;
        public CIS_CLibrary.CIS_Textbox txtCode;
        internal CIS_CLibrary.CIS_Textbox txtTopic;
        internal CIS_CLibrary.CIS_TextLabel lblMiscNameColon;
        internal CIS_CLibrary.CIS_TextLabel lblTOpic;
        internal CIS_CLibrary.CIS_Textbox txtToDt;
        internal CIS_CLibrary.CIS_TextLabel label2;
        internal CIS_CLibrary.CIS_TextLabel lblToDate;
        internal CIS_CLibrary.CIS_Textbox txtFromDt;
        internal CIS_CLibrary.CIS_CheckBox ChkActive;
    }
}
