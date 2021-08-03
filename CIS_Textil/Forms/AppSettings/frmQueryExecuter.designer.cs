namespace CIS_Textil
{
    partial class frmQueryExecuter
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txt_Query = new System.Windows.Forms.RichTextBox();
            this.grd_ResultGrid = new System.Windows.Forms.DataGridView();
            this.btn_Open = new CIS_CLibrary.CIS_Button();
            this.btn_Save = new CIS_CLibrary.CIS_Button();
            this.btn_Execute = new CIS_CLibrary.CIS_Button();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.TSp_Result = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStrip1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSp_ServerNm = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStrip2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSp_DbNm = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStrip3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSp_NoRec = new System.Windows.Forms.ToolStripStatusLabel();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_ResultGrid)).BeginInit();
            this.StatusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.StatusStrip1);
            this.pnlContent.Controls.Add(this.btn_Open);
            this.pnlContent.Controls.Add(this.btn_Save);
            this.pnlContent.Controls.Add(this.btn_Execute);
            this.pnlContent.Controls.Add(this.grd_ResultGrid);
            this.pnlContent.Controls.Add(this.txt_Query);
            this.pnlContent.Controls.SetChildIndex(this.txt_Query, 0);
            this.pnlContent.Controls.SetChildIndex(this.grd_ResultGrid, 0);
            this.pnlContent.Controls.SetChildIndex(this.btn_Execute, 0);
            this.pnlContent.Controls.SetChildIndex(this.btn_Save, 0);
            this.pnlContent.Controls.SetChildIndex(this.btn_Open, 0);
            this.pnlContent.Controls.SetChildIndex(this.StatusStrip1, 0);
            // 
            // txt_Query
            // 
            this.txt_Query.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Query.Location = new System.Drawing.Point(12, 7);
            this.txt_Query.Name = "txt_Query";
            this.txt_Query.Size = new System.Drawing.Size(927, 176);
            this.txt_Query.TabIndex = 3;
            this.txt_Query.Text = "";
            // 
            // grd_ResultGrid
            // 
            this.grd_ResultGrid.AllowUserToAddRows = false;
            this.grd_ResultGrid.AllowUserToDeleteRows = false;
            this.grd_ResultGrid.AllowUserToResizeRows = false;
            this.grd_ResultGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grd_ResultGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.grd_ResultGrid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.grd_ResultGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grd_ResultGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.grd_ResultGrid.Location = new System.Drawing.Point(11, 237);
            this.grd_ResultGrid.Name = "grd_ResultGrid";
            this.grd_ResultGrid.ReadOnly = true;
            this.grd_ResultGrid.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.grd_ResultGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grd_ResultGrid.Size = new System.Drawing.Size(932, 226);
            this.grd_ResultGrid.TabIndex = 24;
            // 
            // btn_Open
            // 
            this.btn_Open.BackColor = System.Drawing.Color.SteelBlue;
            this.btn_Open.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Open.ForeColor = System.Drawing.Color.Black;
            this.btn_Open.Location = new System.Drawing.Point(703, 197);
            this.btn_Open.Name = "btn_Open";
            this.btn_Open.Size = new System.Drawing.Size(75, 34);
            this.btn_Open.TabIndex = 27;
            this.btn_Open.Text = "&Open";
            this.btn_Open.UseVisualStyleBackColor = false;
            this.btn_Open.Click += new System.EventHandler(this.btn_Open_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.BackColor = System.Drawing.Color.SteelBlue;
            this.btn_Save.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.ForeColor = System.Drawing.Color.Black;
            this.btn_Save.Location = new System.Drawing.Point(783, 197);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 34);
            this.btn_Save.TabIndex = 26;
            this.btn_Save.Text = "&Save";
            this.btn_Save.UseVisualStyleBackColor = false;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Execute
            // 
            this.btn_Execute.BackColor = System.Drawing.Color.SteelBlue;
            this.btn_Execute.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Execute.ForeColor = System.Drawing.Color.Black;
            this.btn_Execute.Location = new System.Drawing.Point(864, 197);
            this.btn_Execute.Name = "btn_Execute";
            this.btn_Execute.Size = new System.Drawing.Size(75, 34);
            this.btn_Execute.TabIndex = 25;
            this.btn_Execute.Text = "Execute";
            this.btn_Execute.UseVisualStyleBackColor = false;
            this.btn_Execute.Click += new System.EventHandler(this.btn_Execute_Click);
            // 
            // StatusStrip1
            // 
            this.StatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSp_Result,
            this.ToolStrip1,
            this.TSp_ServerNm,
            this.ToolStrip2,
            this.TSp_DbNm,
            this.ToolStrip3,
            this.TSp_NoRec});
            this.StatusStrip1.Location = new System.Drawing.Point(3, 474);
            this.StatusStrip1.Name = "StatusStrip1";
            this.StatusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.ManagerRenderMode;
            this.StatusStrip1.Size = new System.Drawing.Size(949, 22);
            this.StatusStrip1.TabIndex = 28;
            // 
            // TSp_Result
            // 
            this.TSp_Result.Name = "TSp_Result";
            this.TSp_Result.Size = new System.Drawing.Size(61, 17);
            this.TSp_Result.Text = "TSp_Result";
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new System.Drawing.Size(76, 17);
            this.ToolStrip1.Text = "Server Name :";
            // 
            // TSp_ServerNm
            // 
            this.TSp_ServerNm.Name = "TSp_ServerNm";
            this.TSp_ServerNm.Size = new System.Drawing.Size(78, 17);
            this.TSp_ServerNm.Text = "TSp_ServerNm";
            // 
            // ToolStrip2
            // 
            this.ToolStrip2.Name = "ToolStrip2";
            this.ToolStrip2.Size = new System.Drawing.Size(93, 17);
            this.ToolStrip2.Text = "Data Base Name :";
            // 
            // TSp_DbNm
            // 
            this.TSp_DbNm.Name = "TSp_DbNm";
            this.TSp_DbNm.Size = new System.Drawing.Size(59, 17);
            this.TSp_DbNm.Text = "TSp_DbNm";
            // 
            // ToolStrip3
            // 
            this.ToolStrip3.Name = "ToolStrip3";
            this.ToolStrip3.Size = new System.Drawing.Size(106, 17);
            this.ToolStrip3.Text = "Number of Records :";
            // 
            // TSp_NoRec
            // 
            this.TSp_NoRec.Name = "TSp_NoRec";
            this.TSp_NoRec.Size = new System.Drawing.Size(62, 17);
            this.TSp_NoRec.Text = "TSp_NoRec";
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.FileName = "OpenFileDialog1";
            // 
            // frmQueryExec
            // 
            this.ClientSize = new System.Drawing.Size(955, 547);
            this.KeyPreview = true;
            this.Name = "frmQueryExec";
            this.Load += new System.EventHandler(this.frmQueryExecuter_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmQueryExecuter_KeyDown);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd_ResultGrid)).EndInit();
            this.StatusStrip1.ResumeLayout(false);
            this.StatusStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.RichTextBox txt_Query;
        internal System.Windows.Forms.DataGridView grd_ResultGrid;
        internal CIS_CLibrary.CIS_Button btn_Open;
        internal CIS_CLibrary.CIS_Button btn_Save;
        internal CIS_CLibrary.CIS_Button btn_Execute;
        internal System.Windows.Forms.StatusStrip StatusStrip1;
        internal System.Windows.Forms.ToolStripStatusLabel TSp_Result;
        internal System.Windows.Forms.ToolStripStatusLabel ToolStrip1;
        internal System.Windows.Forms.ToolStripStatusLabel TSp_ServerNm;
        internal System.Windows.Forms.ToolStripStatusLabel ToolStrip2;
        internal System.Windows.Forms.ToolStripStatusLabel TSp_DbNm;
        internal System.Windows.Forms.ToolStripStatusLabel ToolStrip3;
        internal System.Windows.Forms.ToolStripStatusLabel TSp_NoRec;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog;
        internal System.Windows.Forms.SaveFileDialog SaveFileDialog;
    }
}
