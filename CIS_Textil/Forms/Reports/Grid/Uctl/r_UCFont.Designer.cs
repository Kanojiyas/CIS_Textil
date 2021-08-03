namespace CIS_Textil
{
    partial class r_UCFont
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        //private System.ComponentModel.IContainer components = null;

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
            this.btnDone = new CIS_CLibrary.CIS_Button();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.cboSize = new System.Windows.Forms.ComboBox();
            this.Label3 = new CIS_CLibrary.CIS_TextLabel();
            this.cboFont = new System.Windows.Forms.ComboBox();
            this.Label2 = new CIS_CLibrary.CIS_TextLabel();
            this.SuspendLayout();
            // 
            // btnDone
            // 
            this.btnDone.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDone.Location = new System.Drawing.Point(98, 100);
            this.btnDone.Name = "btnDone";
            this.btnDone.Size = new System.Drawing.Size(84, 37);
            this.btnDone.TabIndex = 15;
            this.btnDone.Text = "Apply";
            this.btnDone.Click += new System.EventHandler(this.btnDone_Click);
            // 
            // GroupBox1
            // 
            this.GroupBox1.Location = new System.Drawing.Point(-1, 87);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(289, 8);
            this.GroupBox1.TabIndex = 16;
            this.GroupBox1.TabStop = false;
            // 
            // cboSize
            // 
            this.cboSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSize.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSize.FormattingEnabled = true;
            this.cboSize.Location = new System.Drawing.Point(55, 55);
            this.cboSize.Margin = new System.Windows.Forms.Padding(4);
            this.cboSize.Name = "cboSize";
            this.cboSize.Size = new System.Drawing.Size(78, 21);
            this.cboSize.TabIndex = 14;
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.Location = new System.Drawing.Point(14, 57);
            this.Label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(40, 17);
            this.Label3.TabIndex = 13;
            this.Label3.Text = "Size :";
            // 
            // cboFont
            // 
            this.cboFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFont.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFont.FormattingEnabled = true;
            this.cboFont.Location = new System.Drawing.Point(55, 26);
            this.cboFont.Margin = new System.Windows.Forms.Padding(4);
            this.cboFont.Name = "cboFont";
            this.cboFont.Size = new System.Drawing.Size(224, 21);
            this.cboFont.TabIndex = 12;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(13, 28);
            this.Label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(44, 17);
            this.Label2.TabIndex = 11;
            this.Label2.Text = "Font :";
            // 
            // r_UCFont
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Khaki;
            this.Controls.Add(this.btnDone);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.cboSize);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.cboFont);
            this.Controls.Add(this.Label2);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "r_UCFont";
            this.Size = new System.Drawing.Size(288, 146);
            this.Load += new System.EventHandler(this.r_UCFont_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal CIS_CLibrary.CIS_Button btnDone;
        internal System.Windows.Forms.GroupBox GroupBox1;
        internal System.Windows.Forms.ComboBox cboSize;
        internal CIS_CLibrary.CIS_TextLabel Label3;
        internal System.Windows.Forms.ComboBox cboFont;
        internal CIS_CLibrary.CIS_TextLabel Label2;
    }
}
