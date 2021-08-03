namespace CIS_Textil
{
    partial class r_NewRpt
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
            this.txtNewRpt = new CIS_CLibrary.CIS_Textbox();
            this.btnDone = new CIS_CLibrary.CIS_Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.lblCap = new CIS_CLibrary.CIS_TextLabel();
            this.SuspendLayout();
            // 
            // txtNewRpt
            // 
            this.txtNewRpt.AutoFillDate = false;
            this.txtNewRpt.CheckForSymbol = null;
            this.txtNewRpt.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtNewRpt.DecimalPlace = 0;
            this.txtNewRpt.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewRpt.HoldMyText = null;
            this.txtNewRpt.IsMandatory = false;
            this.txtNewRpt.IsSingleQuote = true;
            this.txtNewRpt.IsSysmbol = false;
            this.txtNewRpt.Location = new System.Drawing.Point(106, 24);
            this.txtNewRpt.Mask = null;
            this.txtNewRpt.MaxLength = 50;
            this.txtNewRpt.Name = "txtNewRpt";
            this.txtNewRpt.Prefix = null;
            this.txtNewRpt.ShowErrorIcon = false;
            this.txtNewRpt.ShowMessage = null;
            this.txtNewRpt.Size = new System.Drawing.Size(263, 22);
            this.txtNewRpt.Suffix = null;
            this.txtNewRpt.TabIndex = 7;
            // 
            // btnDone
            // 
            this.btnDone.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDone.Location = new System.Drawing.Point(126, 69);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(127, 37);
            this.btnDone.TabIndex = 5;
            this.btnDone.Text = "Create Report";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Location = new System.Drawing.Point(1, 55);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(470, 8);
            this.GroupBox1.TabIndex = 6;
            this.GroupBox1.TabStop = false;
            // 
            // lblCap
            // 
            this.lblCap.AutoSize = true;
            this.lblCap.ForeColor = System.Drawing.Color.Black;
            this.lblCap.Location = new System.Drawing.Point(3, 24);
            this.lblCap.Name = "lblCap";
            this.lblCap.Size = new System.Drawing.Size(97, 17);
            this.lblCap.TabIndex = 4;
            this.lblCap.Text = "Report Name :";
            // 
            // r_NewRpt
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Khaki;
            this.Controls.Add(this.txtNewRpt);
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.lblCap);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "r_NewRpt";
            this.Size = new System.Drawing.Size(382, 113);
            this.Load += new System.EventHandler(this.r_NewRpt_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal CIS_CLibrary.CIS_Textbox txtNewRpt;
        internal CIS_CLibrary.CIS_Button btnDone;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal CIS_CLibrary.CIS_TextLabel lblCap;
    }
}
