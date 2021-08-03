namespace CIS_Textil
{
    partial class frmMenuSettings
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
            this.lblGroupNm = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.Label2 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.Label1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.dgProperty = new System.Windows.Forms.DataGridView();
            this.tvList = new CIS_CLibrary.CIS_TreeView();
            this.cboObjProp = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.btnAdd = new CIS_CLibrary.CIS_Button();
            this.btnSave = new CIS_CLibrary.CIS_Button();
            this.btnCancel_F = new CIS_CLibrary.CIS_Button();
            this.btnUp = new CIS_CLibrary.CIS_Button();
            this.btnDown = new CIS_CLibrary.CIS_Button();
            this.btnDelete = new CIS_CLibrary.CIS_Button();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProperty)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.PaleTurquoise;
            this.pnlContent.Controls.Add(this.btnDelete);
            this.pnlContent.Controls.Add(this.btnDown);
            this.pnlContent.Controls.Add(this.btnUp);
            this.pnlContent.Controls.Add(this.btnCancel_F);
            this.pnlContent.Controls.Add(this.btnSave);
            this.pnlContent.Controls.Add(this.btnAdd);
            this.pnlContent.Controls.Add(this.cboObjProp);
            this.pnlContent.Controls.Add(this.tvList);
            this.pnlContent.Controls.Add(this.lblGroupNm);
            this.pnlContent.Controls.Add(this.Label2);
            this.pnlContent.Controls.Add(this.Label1);
            this.pnlContent.Controls.Add(this.dgProperty);
            this.pnlContent.Size = new System.Drawing.Size(710, 449);
            this.pnlContent.Controls.SetChildIndex(this.dgProperty, 0);
            this.pnlContent.Controls.SetChildIndex(this.Label1, 0);
            this.pnlContent.Controls.SetChildIndex(this.Label2, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblGroupNm, 0);
            this.pnlContent.Controls.SetChildIndex(this.tvList, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboObjProp, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnAdd, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnSave, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnCancel_F, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnUp, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnDown, 0);
            this.pnlContent.Controls.SetChildIndex(this.btnDelete, 0);
            // 
            // lblHelpText
            // 
            this.lblHelpText.Location = new System.Drawing.Point(1, 3);
            // 
            // lblUUser
            // 
            this.lblUUser.Location = new System.Drawing.Point(622, 3);
            // 
            // lblCUser
            // 
            this.lblCUser.Location = new System.Drawing.Point(314, 3);
            // 
            // lblGroupNm
            // 
            this.lblGroupNm.AutoSize = true;
            this.lblGroupNm.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupNm.Location = new System.Drawing.Point(456, 11);
            this.lblGroupNm.Moveable = false;
            this.lblGroupNm.Name = "lblGroupNm";
            this.lblGroupNm.NameOfControl = null;
            this.lblGroupNm.Size = new System.Drawing.Size(0, 13);
            this.lblGroupNm.TabIndex = 34;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.Location = new System.Drawing.Point(354, 11);
            this.Label2.Moveable = false;
            this.Label2.Name = "Label2";
            this.Label2.NameOfControl = null;
            this.Label2.Size = new System.Drawing.Size(98, 14);
            this.Label2.TabIndex = 33;
            this.Label2.Text = "Group Name :";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(30, 12);
            this.Label1.Moveable = false;
            this.Label1.Name = "Label1";
            this.Label1.NameOfControl = null;
            this.Label1.Size = new System.Drawing.Size(229, 13);
            this.Label1.TabIndex = 32;
            this.Label1.Text = "Select Item and add to list below :";
            // 
            // dgProperty
            // 
            this.dgProperty.AllowUserToAddRows = false;
            this.dgProperty.AllowUserToDeleteRows = false;
            this.dgProperty.AllowUserToOrderColumns = true;
            this.dgProperty.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgProperty.Location = new System.Drawing.Point(350, 27);
            this.dgProperty.MultiSelect = false;
            this.dgProperty.Name = "dgProperty";
            this.dgProperty.Size = new System.Drawing.Size(348, 380);
            this.dgProperty.TabIndex = 28;
            this.dgProperty.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgProperty_CellClick);
            this.dgProperty.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgProperty_CellEndEdit);
            // 
            // tvList
            // 
            this.tvList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.tvList.FontHotTracking = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvList.Location = new System.Drawing.Point(34, 59);
            this.tvList.Name = "tvList";
            this.tvList.Size = new System.Drawing.Size(268, 348);
            this.tvList.TabIndex = 35;
            // 
            // cboObjProp
            // 
            this.cboObjProp.AutoComplete = false;
            this.cboObjProp.AutoDropdown = false;
            this.cboObjProp.BackColor = System.Drawing.Color.White;
            this.cboObjProp.BackColorEven = System.Drawing.Color.White;
            this.cboObjProp.BackColorOdd = System.Drawing.Color.White;
            this.cboObjProp.ColumnNames = "";
            this.cboObjProp.ColumnWidthDefault = 175;
            this.cboObjProp.ColumnWidths = "";
            this.cboObjProp.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboObjProp.Fill_ComboID = 0;
            this.cboObjProp.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboObjProp.FormattingEnabled = true;
            this.cboObjProp.GroupType = 0;
            this.cboObjProp.HelpText = null;
            this.cboObjProp.IsMandatory = false;
            this.cboObjProp.LinkedColumnIndex = 0;
            this.cboObjProp.LinkedTextBox = null;
            this.cboObjProp.Location = new System.Drawing.Point(34, 30);
            this.cboObjProp.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboObjProp.Moveable = false;
            this.cboObjProp.Name = "cboObjProp";
            this.cboObjProp.NameOfControl = null;
            this.cboObjProp.OpenForm = "frmYarnTypeMaster";
            this.cboObjProp.ShowBallonTip = false;
            this.cboObjProp.Size = new System.Drawing.Size(190, 23);
            this.cboObjProp.TabIndex = 142;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.CadetBlue;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(229, 30);
            this.btnAdd.Moveable = false;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 144;
            this.btnAdd.Text = "&ADD";
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.CadetBlue;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(523, 413);
            this.btnSave.Moveable = false;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 29);
            this.btnSave.TabIndex = 145;
            this.btnSave.Text = "&OK";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel_F
            // 
            this.btnCancel_F.BackColor = System.Drawing.Color.CadetBlue;
            this.btnCancel_F.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel_F.Location = new System.Drawing.Point(615, 413);
            this.btnCancel_F.Moveable = false;
            this.btnCancel_F.Name = "btnCancel_F";
            this.btnCancel_F.Size = new System.Drawing.Size(83, 29);
            this.btnCancel_F.TabIndex = 146;
            this.btnCancel_F.Text = "&CANCEL";
            this.btnCancel_F.UseVisualStyleBackColor = false;
            this.btnCancel_F.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnUp
            // 
            this.btnUp.BackColor = System.Drawing.Color.CadetBlue;
            this.btnUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUp.Image = global::CIS_Textil.Properties.Resources.BlackCollapse;
            this.btnUp.Location = new System.Drawing.Point(308, 152);
            this.btnUp.Moveable = false;
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(36, 37);
            this.btnUp.TabIndex = 147;
            this.btnUp.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnUp.UseVisualStyleBackColor = false;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.BackColor = System.Drawing.Color.CadetBlue;
            this.btnDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDown.Image = global::CIS_Textil.Properties.Resources.BlackExpand;
            this.btnDown.Location = new System.Drawing.Point(308, 194);
            this.btnDown.Moveable = false;
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(36, 37);
            this.btnDown.TabIndex = 148;
            this.btnDown.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.CadetBlue;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Image = global::CIS_Textil.Properties.Resources.BlackDelete;
            this.btnDelete.Location = new System.Drawing.Point(308, 236);
            this.btnDelete.Moveable = false;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(36, 37);
            this.btnDelete.TabIndex = 149;
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // frmMenuSettings
            // 
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.ClientSize = new System.Drawing.Size(710, 500);
            this.DoubleBuffered = false;
            this.Name = "frmMenuSettings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMenuSettings_FormClosing);
            this.Load += new System.EventHandler(this.frmMenuSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgProperty)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal CIS_CLibrary.CIS_TextLabel lblGroupNm;
        internal CIS_CLibrary.CIS_TextLabel Label2;
        internal CIS_CLibrary.CIS_TextLabel Label1;
        internal System.Windows.Forms.DataGridView dgProperty;
        private CIS_CLibrary.CIS_TreeView tvList;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboObjProp;
        private CIS_CLibrary.CIS_Button btnAdd;
        private CIS_CLibrary.CIS_Button btnCancel_F;
        private CIS_CLibrary.CIS_Button btnSave;
        private CIS_CLibrary.CIS_Button btnDelete;
        private CIS_CLibrary.CIS_Button btnDown;
        private CIS_CLibrary.CIS_Button btnUp;
    }
}
