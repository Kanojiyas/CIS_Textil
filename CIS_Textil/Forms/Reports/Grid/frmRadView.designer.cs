namespace CIS_Textil
{
    partial class frmRadView
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
            this.btnView = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.btnView);
            this.pnlContent.Controls.SetChildIndex(this.btnView, 0);
            // 
            // btnView
            // 
            this.btnView.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnView.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnView.Location = new System.Drawing.Point(9, 6);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(145, 73);
            this.btnView.TabIndex = 3;
            this.btnView.Text = "View Report";
            this.btnView.UseVisualStyleBackColor = false;
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // frmRadView
            // 
            this.ClientSize = new System.Drawing.Size(955, 547);
            this.Name = "frmRadView";
            this.Load += new System.EventHandler(this.frmRadView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnView;
    }
}
