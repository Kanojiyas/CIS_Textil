namespace CIS_Textil
{
    partial class frmSecurityMaster
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
            this.lblColnSecurityLevel = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.ChkActive = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.txtSecurityLevel = new CIS_CLibrary.CIS_Textbox();
            this.lblSecurityName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.ciS_TextLabel1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtAliasName = new CIS_CLibrary.CIS_Textbox();
            this.lblAliasName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.ciS_TextLabel1);
            this.pnlContent.Controls.Add(this.txtAliasName);
            this.pnlContent.Controls.Add(this.lblAliasName);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.Add(this.lblColnSecurityLevel);
            this.pnlContent.Controls.Add(this.ChkActive);
            this.pnlContent.Controls.Add(this.txtSecurityLevel);
            this.pnlContent.Controls.Add(this.lblSecurityName);
            this.pnlContent.Size = new System.Drawing.Size(805, 496);
            this.pnlContent.Controls.SetChildIndex(this.lblSecurityName, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtSecurityLevel, 0);
            this.pnlContent.Controls.SetChildIndex(this.ChkActive, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblColnSecurityLevel, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblAliasName, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtAliasName, 0);
            this.pnlContent.Controls.SetChildIndex(this.ciS_TextLabel1, 0);
            // 
            // lblColnSecurityLevel
            // 
            this.lblColnSecurityLevel.AutoSize = true;
            this.lblColnSecurityLevel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblColnSecurityLevel.Location = new System.Drawing.Point(303, 36);
            this.lblColnSecurityLevel.Moveable = false;
            this.lblColnSecurityLevel.Name = "lblColnSecurityLevel";
            this.lblColnSecurityLevel.NameOfControl = null;
            this.lblColnSecurityLevel.Size = new System.Drawing.Size(12, 14);
            this.lblColnSecurityLevel.TabIndex = 10;
            this.lblColnSecurityLevel.Text = ":";
            // 
            // ChkActive
            // 
            this.ChkActive.AutoSize = true;
            this.ChkActive.BackColor = System.Drawing.Color.MintCream;
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.ChkActive.HelpText = null;
            this.ChkActive.Location = new System.Drawing.Point(320, 88);
            this.ChkActive.Moveable = false;
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.NameOfControl = null;
            this.ChkActive.Size = new System.Drawing.Size(113, 18);
            this.ChkActive.TabIndex = 3;
            this.ChkActive.Text = "Active Status";
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // txtSecurityLevel
            // 
            this.txtSecurityLevel.AutoFillDate = false;
            this.txtSecurityLevel.BackColor = System.Drawing.Color.White;
            this.txtSecurityLevel.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtSecurityLevel.CheckForSymbol = null;
            this.txtSecurityLevel.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtSecurityLevel.DecimalPlace = 0;
            this.txtSecurityLevel.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtSecurityLevel.HelpText = "Enter Security Level";
            this.txtSecurityLevel.HoldMyText = null;
            this.txtSecurityLevel.IsMandatory = false;
            this.txtSecurityLevel.IsSingleQuote = true;
            this.txtSecurityLevel.IsSysmbol = false;
            this.txtSecurityLevel.Location = new System.Drawing.Point(320, 34);
            this.txtSecurityLevel.Mask = null;
            this.txtSecurityLevel.MaxLength = 50;
            this.txtSecurityLevel.Moveable = false;
            this.txtSecurityLevel.Name = "txtSecurityLevel";
            this.txtSecurityLevel.NameOfControl = null;
            this.txtSecurityLevel.Prefix = null;
            this.txtSecurityLevel.ShowBallonTip = false;
            this.txtSecurityLevel.ShowErrorIcon = false;
            this.txtSecurityLevel.ShowMessage = null;
            this.txtSecurityLevel.Size = new System.Drawing.Size(250, 22);
            this.txtSecurityLevel.Suffix = null;
            this.txtSecurityLevel.TabIndex = 1;
            // 
            // lblSecurityName
            // 
            this.lblSecurityName.AutoSize = true;
            this.lblSecurityName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblSecurityName.Location = new System.Drawing.Point(195, 36);
            this.lblSecurityName.Moveable = false;
            this.lblSecurityName.Name = "lblSecurityName";
            this.lblSecurityName.NameOfControl = null;
            this.lblSecurityName.Size = new System.Drawing.Size(102, 14);
            this.lblSecurityName.TabIndex = 9;
            this.lblSecurityName.Text = "Security Level";
            // 
            // txtCode
            // 
            this.txtCode.AutoFillDate = false;
            this.txtCode.BackColor = System.Drawing.Color.White;
            this.txtCode.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtCode.CheckForSymbol = null;
            this.txtCode.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtCode.DecimalPlace = 0;
            this.txtCode.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.HelpText = "";
            this.txtCode.HoldMyText = null;
            this.txtCode.IsMandatory = false;
            this.txtCode.IsSingleQuote = true;
            this.txtCode.IsSysmbol = false;
            this.txtCode.Location = new System.Drawing.Point(11, 9);
            this.txtCode.Mask = null;
            this.txtCode.Moveable = false;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(43, 22);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 11;
            this.txtCode.Visible = false;
            // 
            // ciS_TextLabel1
            // 
            this.ciS_TextLabel1.AutoSize = true;
            this.ciS_TextLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.ciS_TextLabel1.Location = new System.Drawing.Point(303, 62);
            this.ciS_TextLabel1.Moveable = false;
            this.ciS_TextLabel1.Name = "ciS_TextLabel1";
            this.ciS_TextLabel1.NameOfControl = null;
            this.ciS_TextLabel1.Size = new System.Drawing.Size(12, 14);
            this.ciS_TextLabel1.TabIndex = 14;
            this.ciS_TextLabel1.Text = ":";
            // 
            // txtAliasName
            // 
            this.txtAliasName.AutoFillDate = false;
            this.txtAliasName.BackColor = System.Drawing.Color.White;
            this.txtAliasName.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtAliasName.CheckForSymbol = null;
            this.txtAliasName.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtAliasName.DecimalPlace = 0;
            this.txtAliasName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.txtAliasName.HelpText = "Enter Security Level";
            this.txtAliasName.HoldMyText = null;
            this.txtAliasName.IsMandatory = false;
            this.txtAliasName.IsSingleQuote = true;
            this.txtAliasName.IsSysmbol = false;
            this.txtAliasName.Location = new System.Drawing.Point(320, 60);
            this.txtAliasName.Mask = null;
            this.txtAliasName.MaxLength = 50;
            this.txtAliasName.Moveable = false;
            this.txtAliasName.Name = "txtAliasName";
            this.txtAliasName.NameOfControl = null;
            this.txtAliasName.Prefix = null;
            this.txtAliasName.ShowBallonTip = false;
            this.txtAliasName.ShowErrorIcon = false;
            this.txtAliasName.ShowMessage = null;
            this.txtAliasName.Size = new System.Drawing.Size(250, 22);
            this.txtAliasName.Suffix = null;
            this.txtAliasName.TabIndex = 2;
            // 
            // lblAliasName
            // 
            this.lblAliasName.AutoSize = true;
            this.lblAliasName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.lblAliasName.Location = new System.Drawing.Point(195, 62);
            this.lblAliasName.Moveable = false;
            this.lblAliasName.Name = "lblAliasName";
            this.lblAliasName.NameOfControl = null;
            this.lblAliasName.Size = new System.Drawing.Size(81, 14);
            this.lblAliasName.TabIndex = 13;
            this.lblAliasName.Text = "Alias Name";
            // 
            // frmSecurityMaster
            // 
            this.ClientSize = new System.Drawing.Size(805, 547);
            this.Name = "frmSecurityMaster";
            this.Load += new System.EventHandler(this.frmSecurityMaster_Load);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal CIS_CLibrary.CIS_TextLabel lblColnSecurityLevel;
        internal CIS_CLibrary.CIS_CheckBox ChkActive;
        internal CIS_CLibrary.CIS_Textbox txtSecurityLevel;
        internal CIS_CLibrary.CIS_TextLabel lblSecurityName;
        public CIS_CLibrary.CIS_Textbox txtCode;
        internal CIS_CLibrary.CIS_TextLabel ciS_TextLabel1;
        internal CIS_CLibrary.CIS_Textbox txtAliasName;
        internal CIS_CLibrary.CIS_TextLabel lblAliasName;
    }
}
