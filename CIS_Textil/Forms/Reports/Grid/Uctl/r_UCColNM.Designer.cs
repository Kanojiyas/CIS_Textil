namespace CIS_Textil
{
    partial class r_UCColNM
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
            this.spc_RenameCol = new System.Windows.Forms.SplitContainer();
            this.btnDone = new CIS_CLibrary.CIS_Button();
            this.chkUpdateInRpt = new CIS_CLibrary.CIS_CheckBox();
            this.spc_RenameCol.Panel2.SuspendLayout();
            this.spc_RenameCol.SuspendLayout();
            this.SuspendLayout();
            // 
            // spc_RenameCol
            // 
            this.spc_RenameCol.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spc_RenameCol.Location = new System.Drawing.Point(0, 0);
            this.spc_RenameCol.Name = "spc_RenameCol";
            this.spc_RenameCol.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spc_RenameCol.Panel2
            // 
            this.spc_RenameCol.Panel2.Controls.Add(this.btnDone);
            this.spc_RenameCol.Panel2.Controls.Add(this.chkUpdateInRpt);
            this.spc_RenameCol.Size = new System.Drawing.Size(462, 333);
            this.spc_RenameCol.SplitterDistance = 273;
            this.spc_RenameCol.TabIndex = 2;
            // 
            // btnDone
            // 
            this.btnDone.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDone.Location = new System.Drawing.Point(33, 8);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(133, 41);
            this.btnDone.TabIndex = 3;
            this.btnDone.Text = "Apply";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // chkUpdateInRpt
            // 
            this.chkUpdateInRpt.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUpdateInRpt.Location = new System.Drawing.Point(212, 15);
            this.chkUpdateInRpt.Name = "chkUpdateInRpt";
            this.chkUpdateInRpt.Size = new System.Drawing.Size(171, 30);
            this.chkUpdateInRpt.TabIndex = 2;
            this.chkUpdateInRpt.Text = "Update In Report Only";
            this.chkUpdateInRpt.UseVisualStyleBackColor = true;
            // 
            // r_UCColNM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.Controls.Add(this.spc_RenameCol);
            this.Name = "r_UCColNM";
            this.Size = new System.Drawing.Size(462, 333);
            this.Load += new System.EventHandler(this.r_UCColNM_Load);
            this.spc_RenameCol.Panel2.ResumeLayout(false);
            this.spc_RenameCol.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.SplitContainer spc_RenameCol;
        internal CIS_CLibrary.CIS_Button btnDone;
        internal CIS_CLibrary.CIS_CheckBox chkUpdateInRpt;
    }
}
