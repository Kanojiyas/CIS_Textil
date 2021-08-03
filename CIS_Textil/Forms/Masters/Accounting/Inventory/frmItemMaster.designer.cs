namespace CIS_Textil
{
    partial class frmItemMaster
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
            this.cboItemCategory = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblItemCategoryColun = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblItemCategory = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtCode = new CIS_CLibrary.CIS_Textbox();
            this.cboUnits = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblUnitsColun = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblUnits = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.cboItemGroup = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblItemGroupColun = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblItemGroup = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtItemName = new CIS_CLibrary.CIS_Textbox();
            this.lblItemNameColun = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblItemName = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.ChkActive = new CIS_CLibrary.CIS_CheckBox(this.components);
            this.txtAliasname = new CIS_CLibrary.CIS_Textbox();
            this.label1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.label2 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtQty = new CIS_CLibrary.CIS_Textbox();
            this.ciS_TextLabel1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.ciS_TextLabel2 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblEI1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblEI1Colon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.cboEI1 = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblEI2 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.cboEI2 = new CIS_MultiColumnComboBox.CIS_MultiColumnComboBox();
            this.lblEI2Colon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtET3 = new CIS_CLibrary.CIS_Textbox();
            this.lblET3Colon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtET2 = new CIS_CLibrary.CIS_Textbox();
            this.lblET2Colon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblET3 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblET2 = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.txtET1 = new CIS_CLibrary.CIS_Textbox();
            this.lblET1Colon = new CIS_CLibrary.CIS_TextLabel(this.components);
            this.lblET1 = new CIS_CLibrary.CIS_TextLabel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).BeginInit();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.lblEI1);
            this.pnlContent.Controls.Add(this.txtQty);
            this.pnlContent.Controls.Add(this.lblEI1Colon);
            this.pnlContent.Controls.Add(this.ciS_TextLabel1);
            this.pnlContent.Controls.Add(this.cboEI1);
            this.pnlContent.Controls.Add(this.lblEI2);
            this.pnlContent.Controls.Add(this.ciS_TextLabel2);
            this.pnlContent.Controls.Add(this.cboEI2);
            this.pnlContent.Controls.Add(this.txtAliasname);
            this.pnlContent.Controls.Add(this.lblEI2Colon);
            this.pnlContent.Controls.Add(this.label1);
            this.pnlContent.Controls.Add(this.txtET3);
            this.pnlContent.Controls.Add(this.label2);
            this.pnlContent.Controls.Add(this.lblET3Colon);
            this.pnlContent.Controls.Add(this.cboItemCategory);
            this.pnlContent.Controls.Add(this.txtET2);
            this.pnlContent.Controls.Add(this.txtCode);
            this.pnlContent.Controls.Add(this.lblET2Colon);
            this.pnlContent.Controls.Add(this.ChkActive);
            this.pnlContent.Controls.Add(this.lblET3);
            this.pnlContent.Controls.Add(this.cboUnits);
            this.pnlContent.Controls.Add(this.lblET2);
            this.pnlContent.Controls.Add(this.lblItemCategoryColun);
            this.pnlContent.Controls.Add(this.txtET1);
            this.pnlContent.Controls.Add(this.lblItemCategory);
            this.pnlContent.Controls.Add(this.lblET1Colon);
            this.pnlContent.Controls.Add(this.cboItemGroup);
            this.pnlContent.Controls.Add(this.lblET1);
            this.pnlContent.Controls.Add(this.txtItemName);
            this.pnlContent.Controls.Add(this.lblUnitsColun);
            this.pnlContent.Controls.Add(this.lblUnits);
            this.pnlContent.Controls.Add(this.lblItemName);
            this.pnlContent.Controls.Add(this.lblItemGroupColun);
            this.pnlContent.Controls.Add(this.lblItemGroup);
            this.pnlContent.Controls.Add(this.lblItemNameColun);
            this.pnlContent.Controls.SetChildIndex(this.lblItemNameColun, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblItemGroup, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblItemGroupColun, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblItemName, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblUnits, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblUnitsColun, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtItemName, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET1, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboItemGroup, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET1Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblItemCategory, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtET1, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblItemCategoryColun, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET2, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboUnits, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET3, 0);
            this.pnlContent.Controls.SetChildIndex(this.ChkActive, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET2Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtCode, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtET2, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboItemCategory, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblET3Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.label2, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtET3, 0);
            this.pnlContent.Controls.SetChildIndex(this.label1, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEI2Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtAliasname, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboEI2, 0);
            this.pnlContent.Controls.SetChildIndex(this.ciS_TextLabel2, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEI2, 0);
            this.pnlContent.Controls.SetChildIndex(this.cboEI1, 0);
            this.pnlContent.Controls.SetChildIndex(this.ciS_TextLabel1, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEI1Colon, 0);
            this.pnlContent.Controls.SetChildIndex(this.txtQty, 0);
            this.pnlContent.Controls.SetChildIndex(this.lblEI1, 0);
            // 
            // cboItemCategory
            // 
            this.cboItemCategory.AutoComplete = false;
            this.cboItemCategory.AutoDropdown = false;
            this.cboItemCategory.BackColor = System.Drawing.Color.White;
            this.cboItemCategory.BackColorEven = System.Drawing.Color.White;
            this.cboItemCategory.BackColorOdd = System.Drawing.Color.White;
            this.cboItemCategory.ColumnNames = "";
            this.cboItemCategory.ColumnWidthDefault = 175;
            this.cboItemCategory.ColumnWidths = "";
            this.cboItemCategory.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboItemCategory.Fill_ComboID = 0;
            this.cboItemCategory.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboItemCategory.FormattingEnabled = true;
            this.cboItemCategory.GroupType = 0;
            this.cboItemCategory.HelpText = "Select Item Category";
            this.cboItemCategory.IsMandatory = false;
            this.cboItemCategory.LinkedColumnIndex = 0;
            this.cboItemCategory.LinkedTextBox = null;
            this.cboItemCategory.Location = new System.Drawing.Point(346, 89);
            this.cboItemCategory.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboItemCategory.Moveable = false;
            this.cboItemCategory.Name = "cboItemCategory";
            this.cboItemCategory.NameOfControl = "Item Group";
            this.cboItemCategory.OpenForm = null;
            this.cboItemCategory.ShowBallonTip = false;
            this.cboItemCategory.Size = new System.Drawing.Size(270, 22);
            this.cboItemCategory.TabIndex = 4;
            // 
            // lblItemCategoryColun
            // 
            this.lblItemCategoryColun.AutoSize = true;
            this.lblItemCategoryColun.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCategoryColun.Location = new System.Drawing.Point(325, 91);
            this.lblItemCategoryColun.Moveable = false;
            this.lblItemCategoryColun.Name = "lblItemCategoryColun";
            this.lblItemCategoryColun.NameOfControl = null;
            this.lblItemCategoryColun.Size = new System.Drawing.Size(12, 14);
            this.lblItemCategoryColun.TabIndex = 1018;
            this.lblItemCategoryColun.Text = ":";
            // 
            // lblItemCategory
            // 
            this.lblItemCategory.AutoSize = true;
            this.lblItemCategory.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemCategory.Location = new System.Drawing.Point(224, 91);
            this.lblItemCategory.Moveable = false;
            this.lblItemCategory.Name = "lblItemCategory";
            this.lblItemCategory.NameOfControl = null;
            this.lblItemCategory.Size = new System.Drawing.Size(102, 14);
            this.lblItemCategory.TabIndex = 1017;
            this.lblItemCategory.Text = "Item Category";
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
            this.txtCode.Location = new System.Drawing.Point(0, 0);
            this.txtCode.Mask = null;
            this.txtCode.Moveable = false;
            this.txtCode.Name = "txtCode";
            this.txtCode.NameOfControl = null;
            this.txtCode.Prefix = null;
            this.txtCode.ShowBallonTip = false;
            this.txtCode.ShowErrorIcon = false;
            this.txtCode.ShowMessage = null;
            this.txtCode.Size = new System.Drawing.Size(24, 22);
            this.txtCode.Suffix = null;
            this.txtCode.TabIndex = 0;
            this.txtCode.Visible = false;
            // 
            // cboUnits
            // 
            this.cboUnits.AutoComplete = false;
            this.cboUnits.AutoDropdown = false;
            this.cboUnits.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboUnits.BackColorEven = System.Drawing.Color.White;
            this.cboUnits.BackColorOdd = System.Drawing.Color.White;
            this.cboUnits.ColumnNames = "";
            this.cboUnits.ColumnWidthDefault = 175;
            this.cboUnits.ColumnWidths = "";
            this.cboUnits.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboUnits.Fill_ComboID = 0;
            this.cboUnits.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboUnits.FormattingEnabled = true;
            this.cboUnits.GroupType = 0;
            this.cboUnits.HelpText = "Select Unit";
            this.cboUnits.IsMandatory = true;
            this.cboUnits.LinkedColumnIndex = 0;
            this.cboUnits.LinkedTextBox = null;
            this.cboUnits.Location = new System.Drawing.Point(346, 115);
            this.cboUnits.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboUnits.Moveable = false;
            this.cboUnits.Name = "cboUnits";
            this.cboUnits.NameOfControl = "Units";
            this.cboUnits.OpenForm = null;
            this.cboUnits.ShowBallonTip = false;
            this.cboUnits.Size = new System.Drawing.Size(270, 22);
            this.cboUnits.TabIndex = 5;
            // 
            // lblUnitsColun
            // 
            this.lblUnitsColun.AutoSize = true;
            this.lblUnitsColun.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnitsColun.Location = new System.Drawing.Point(325, 117);
            this.lblUnitsColun.Moveable = false;
            this.lblUnitsColun.Name = "lblUnitsColun";
            this.lblUnitsColun.NameOfControl = null;
            this.lblUnitsColun.Size = new System.Drawing.Size(12, 14);
            this.lblUnitsColun.TabIndex = 1016;
            this.lblUnitsColun.Text = ":";
            // 
            // lblUnits
            // 
            this.lblUnits.AutoSize = true;
            this.lblUnits.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnits.Location = new System.Drawing.Point(224, 117);
            this.lblUnits.Moveable = false;
            this.lblUnits.Name = "lblUnits";
            this.lblUnits.NameOfControl = null;
            this.lblUnits.Size = new System.Drawing.Size(41, 14);
            this.lblUnits.TabIndex = 1015;
            this.lblUnits.Text = "Units";
            // 
            // cboItemGroup
            // 
            this.cboItemGroup.AutoComplete = false;
            this.cboItemGroup.AutoDropdown = false;
            this.cboItemGroup.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboItemGroup.BackColorEven = System.Drawing.Color.White;
            this.cboItemGroup.BackColorOdd = System.Drawing.Color.White;
            this.cboItemGroup.ColumnNames = "";
            this.cboItemGroup.ColumnWidthDefault = 175;
            this.cboItemGroup.ColumnWidths = "";
            this.cboItemGroup.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboItemGroup.Fill_ComboID = 0;
            this.cboItemGroup.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboItemGroup.FormattingEnabled = true;
            this.cboItemGroup.GroupType = 0;
            this.cboItemGroup.HelpText = "Select Item Group";
            this.cboItemGroup.IsMandatory = true;
            this.cboItemGroup.LinkedColumnIndex = 0;
            this.cboItemGroup.LinkedTextBox = null;
            this.cboItemGroup.Location = new System.Drawing.Point(346, 63);
            this.cboItemGroup.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboItemGroup.Moveable = false;
            this.cboItemGroup.Name = "cboItemGroup";
            this.cboItemGroup.NameOfControl = "Item Group";
            this.cboItemGroup.OpenForm = null;
            this.cboItemGroup.ShowBallonTip = false;
            this.cboItemGroup.Size = new System.Drawing.Size(270, 22);
            this.cboItemGroup.TabIndex = 3;
            // 
            // lblItemGroupColun
            // 
            this.lblItemGroupColun.AutoSize = true;
            this.lblItemGroupColun.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemGroupColun.Location = new System.Drawing.Point(325, 65);
            this.lblItemGroupColun.Moveable = false;
            this.lblItemGroupColun.Name = "lblItemGroupColun";
            this.lblItemGroupColun.NameOfControl = null;
            this.lblItemGroupColun.Size = new System.Drawing.Size(12, 14);
            this.lblItemGroupColun.TabIndex = 1014;
            this.lblItemGroupColun.Text = ":";
            // 
            // lblItemGroup
            // 
            this.lblItemGroup.AutoSize = true;
            this.lblItemGroup.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemGroup.Location = new System.Drawing.Point(224, 65);
            this.lblItemGroup.Moveable = false;
            this.lblItemGroup.Name = "lblItemGroup";
            this.lblItemGroup.NameOfControl = null;
            this.lblItemGroup.Size = new System.Drawing.Size(82, 14);
            this.lblItemGroup.TabIndex = 1013;
            this.lblItemGroup.Text = "Item Group";
            // 
            // txtItemName
            // 
            this.txtItemName.AutoFillDate = false;
            this.txtItemName.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtItemName.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtItemName.CheckForSymbol = null;
            this.txtItemName.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtItemName.DecimalPlace = 0;
            this.txtItemName.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItemName.HelpText = "Enter Item Name";
            this.txtItemName.HoldMyText = null;
            this.txtItemName.IsMandatory = true;
            this.txtItemName.IsSingleQuote = true;
            this.txtItemName.IsSysmbol = false;
            this.txtItemName.Location = new System.Drawing.Point(346, 13);
            this.txtItemName.Mask = null;
            this.txtItemName.MaxLength = 50;
            this.txtItemName.Moveable = false;
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.NameOfControl = "Item Name";
            this.txtItemName.Prefix = null;
            this.txtItemName.ShowBallonTip = false;
            this.txtItemName.ShowErrorIcon = false;
            this.txtItemName.ShowMessage = null;
            this.txtItemName.Size = new System.Drawing.Size(270, 21);
            this.txtItemName.Suffix = null;
            this.txtItemName.TabIndex = 1;
            this.txtItemName.Leave += new System.EventHandler(this.txtItemName_Leave);
            // 
            // lblItemNameColun
            // 
            this.lblItemNameColun.AutoSize = true;
            this.lblItemNameColun.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemNameColun.Location = new System.Drawing.Point(325, 15);
            this.lblItemNameColun.Moveable = false;
            this.lblItemNameColun.Name = "lblItemNameColun";
            this.lblItemNameColun.NameOfControl = null;
            this.lblItemNameColun.Size = new System.Drawing.Size(12, 14);
            this.lblItemNameColun.TabIndex = 1012;
            this.lblItemNameColun.Text = ":";
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemName.Location = new System.Drawing.Point(224, 15);
            this.lblItemName.Moveable = false;
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.NameOfControl = null;
            this.lblItemName.Size = new System.Drawing.Size(80, 14);
            this.lblItemName.TabIndex = 1011;
            this.lblItemName.Text = "Item Name";
            // 
            // ChkActive
            // 
            this.ChkActive.AutoSize = true;
            this.ChkActive.BackColor = System.Drawing.Color.MintCream;
            this.ChkActive.Checked = true;
            this.ChkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ChkActive.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChkActive.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkActive.HelpText = "Checked If Active";
            this.ChkActive.Location = new System.Drawing.Point(346, 167);
            this.ChkActive.Moveable = false;
            this.ChkActive.Name = "ChkActive";
            this.ChkActive.NameOfControl = null;
            this.ChkActive.Size = new System.Drawing.Size(110, 18);
            this.ChkActive.TabIndex = 6;
            this.ChkActive.Text = "Active Status";
            this.ChkActive.UseVisualStyleBackColor = true;
            // 
            // txtAliasname
            // 
            this.txtAliasname.AutoFillDate = false;
            this.txtAliasname.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtAliasname.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtAliasname.CheckForSymbol = null;
            this.txtAliasname.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtAliasname.DecimalPlace = 0;
            this.txtAliasname.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAliasname.HelpText = "Enter Alias Name";
            this.txtAliasname.HoldMyText = null;
            this.txtAliasname.IsMandatory = false;
            this.txtAliasname.IsSingleQuote = true;
            this.txtAliasname.IsSysmbol = false;
            this.txtAliasname.Location = new System.Drawing.Point(346, 38);
            this.txtAliasname.Mask = null;
            this.txtAliasname.MaxLength = 50;
            this.txtAliasname.Moveable = false;
            this.txtAliasname.Name = "txtAliasname";
            this.txtAliasname.NameOfControl = "Alias Name";
            this.txtAliasname.Prefix = null;
            this.txtAliasname.ShowBallonTip = false;
            this.txtAliasname.ShowErrorIcon = false;
            this.txtAliasname.ShowMessage = null;
            this.txtAliasname.Size = new System.Drawing.Size(270, 21);
            this.txtAliasname.Suffix = null;
            this.txtAliasname.TabIndex = 2;
            this.txtAliasname.Leave += new System.EventHandler(this.txtAliasname_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(224, 40);
            this.label1.Moveable = false;
            this.label1.Name = "label1";
            this.label1.NameOfControl = null;
            this.label1.Size = new System.Drawing.Size(81, 14);
            this.label1.TabIndex = 1021;
            this.label1.Text = "Alias Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(325, 40);
            this.label2.Moveable = false;
            this.label2.Name = "label2";
            this.label2.NameOfControl = null;
            this.label2.Size = new System.Drawing.Size(12, 14);
            this.label2.TabIndex = 1022;
            this.label2.Text = ":";
            // 
            // txtQty
            // 
            this.txtQty.AutoFillDate = false;
            this.txtQty.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtQty.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtQty.CheckForSymbol = null;
            this.txtQty.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtQty.DecimalPlace = 0;
            this.txtQty.Font = new System.Drawing.Font("Verdana", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQty.HelpText = "Enter Quantity";
            this.txtQty.HoldMyText = null;
            this.txtQty.IsMandatory = false;
            this.txtQty.IsSingleQuote = true;
            this.txtQty.IsSysmbol = false;
            this.txtQty.Location = new System.Drawing.Point(346, 141);
            this.txtQty.Mask = null;
            this.txtQty.MaxLength = 50;
            this.txtQty.Moveable = false;
            this.txtQty.Name = "txtQty";
            this.txtQty.NameOfControl = "Quantity";
            this.txtQty.Prefix = null;
            this.txtQty.ShowBallonTip = false;
            this.txtQty.ShowErrorIcon = false;
            this.txtQty.ShowMessage = null;
            this.txtQty.Size = new System.Drawing.Size(87, 21);
            this.txtQty.Suffix = null;
            this.txtQty.TabIndex = 1023;
            // 
            // ciS_TextLabel1
            // 
            this.ciS_TextLabel1.AutoSize = true;
            this.ciS_TextLabel1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ciS_TextLabel1.Location = new System.Drawing.Point(222, 143);
            this.ciS_TextLabel1.Moveable = false;
            this.ciS_TextLabel1.Name = "ciS_TextLabel1";
            this.ciS_TextLabel1.NameOfControl = null;
            this.ciS_TextLabel1.Size = new System.Drawing.Size(64, 14);
            this.ciS_TextLabel1.TabIndex = 1024;
            this.ciS_TextLabel1.Text = "Quantity";
            // 
            // ciS_TextLabel2
            // 
            this.ciS_TextLabel2.AutoSize = true;
            this.ciS_TextLabel2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ciS_TextLabel2.Location = new System.Drawing.Point(325, 143);
            this.ciS_TextLabel2.Moveable = false;
            this.ciS_TextLabel2.Name = "ciS_TextLabel2";
            this.ciS_TextLabel2.NameOfControl = null;
            this.ciS_TextLabel2.Size = new System.Drawing.Size(12, 14);
            this.ciS_TextLabel2.TabIndex = 1025;
            this.ciS_TextLabel2.Text = ":";
            // 
            // lblEI1
            // 
            this.lblEI1.AutoSize = true;
            this.lblEI1.BackColor = System.Drawing.Color.Transparent;
            this.lblEI1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI1.Location = new System.Drawing.Point(243, 195);
            this.lblEI1.Moveable = false;
            this.lblEI1.Name = "lblEI1";
            this.lblEI1.NameOfControl = "EI1";
            this.lblEI1.Size = new System.Drawing.Size(30, 14);
            this.lblEI1.TabIndex = 1279;
            this.lblEI1.Text = "EI1";
            this.lblEI1.Visible = false;
            // 
            // lblEI1Colon
            // 
            this.lblEI1Colon.AutoSize = true;
            this.lblEI1Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblEI1Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI1Colon.Location = new System.Drawing.Point(325, 195);
            this.lblEI1Colon.Moveable = false;
            this.lblEI1Colon.Name = "lblEI1Colon";
            this.lblEI1Colon.NameOfControl = "EI1";
            this.lblEI1Colon.Size = new System.Drawing.Size(12, 14);
            this.lblEI1Colon.TabIndex = 1280;
            this.lblEI1Colon.Text = ":";
            this.lblEI1Colon.Visible = false;
            // 
            // cboEI1
            // 
            this.cboEI1.AutoComplete = false;
            this.cboEI1.AutoDropdown = false;
            this.cboEI1.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboEI1.BackColorEven = System.Drawing.Color.White;
            this.cboEI1.BackColorOdd = System.Drawing.Color.White;
            this.cboEI1.ColumnNames = "";
            this.cboEI1.ColumnWidthDefault = 175;
            this.cboEI1.ColumnWidths = "";
            this.cboEI1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboEI1.Fill_ComboID = 0;
            this.cboEI1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboEI1.FormattingEnabled = true;
            this.cboEI1.GroupType = 0;
            this.cboEI1.HelpText = "Select EI1";
            this.cboEI1.IsMandatory = true;
            this.cboEI1.LinkedColumnIndex = 0;
            this.cboEI1.LinkedTextBox = null;
            this.cboEI1.Location = new System.Drawing.Point(346, 191);
            this.cboEI1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboEI1.Moveable = false;
            this.cboEI1.Name = "cboEI1";
            this.cboEI1.NameOfControl = "EI1";
            this.cboEI1.OpenForm = null;
            this.cboEI1.ShowBallonTip = false;
            this.cboEI1.Size = new System.Drawing.Size(231, 23);
            this.cboEI1.TabIndex = 1278;
            this.cboEI1.Visible = false;
            // 
            // lblEI2
            // 
            this.lblEI2.AutoSize = true;
            this.lblEI2.BackColor = System.Drawing.Color.Transparent;
            this.lblEI2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI2.Location = new System.Drawing.Point(243, 219);
            this.lblEI2.Moveable = false;
            this.lblEI2.Name = "lblEI2";
            this.lblEI2.NameOfControl = "EI2";
            this.lblEI2.Size = new System.Drawing.Size(30, 14);
            this.lblEI2.TabIndex = 1276;
            this.lblEI2.Text = "EI2";
            this.lblEI2.Visible = false;
            // 
            // cboEI2
            // 
            this.cboEI2.AutoComplete = true;
            this.cboEI2.AutoDropdown = true;
            this.cboEI2.BackColor = System.Drawing.Color.PapayaWhip;
            this.cboEI2.BackColorEven = System.Drawing.Color.White;
            this.cboEI2.BackColorOdd = System.Drawing.Color.White;
            this.cboEI2.ColumnNames = "";
            this.cboEI2.ColumnWidthDefault = 175;
            this.cboEI2.ColumnWidths = "";
            this.cboEI2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.cboEI2.Fill_ComboID = 0;
            this.cboEI2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold);
            this.cboEI2.FormattingEnabled = true;
            this.cboEI2.GroupType = 0;
            this.cboEI2.HelpText = "Select EI2";
            this.cboEI2.IsMandatory = true;
            this.cboEI2.LinkedColumnIndex = 0;
            this.cboEI2.LinkedTextBox = null;
            this.cboEI2.Location = new System.Drawing.Point(346, 217);
            this.cboEI2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboEI2.Moveable = false;
            this.cboEI2.Name = "cboEI2";
            this.cboEI2.NameOfControl = "EI2";
            this.cboEI2.OpenForm = null;
            this.cboEI2.ShowBallonTip = false;
            this.cboEI2.Size = new System.Drawing.Size(231, 23);
            this.cboEI2.TabIndex = 1275;
            this.cboEI2.Visible = false;
            // 
            // lblEI2Colon
            // 
            this.lblEI2Colon.AutoSize = true;
            this.lblEI2Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblEI2Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEI2Colon.Location = new System.Drawing.Point(325, 219);
            this.lblEI2Colon.Moveable = false;
            this.lblEI2Colon.Name = "lblEI2Colon";
            this.lblEI2Colon.NameOfControl = null;
            this.lblEI2Colon.Size = new System.Drawing.Size(12, 14);
            this.lblEI2Colon.TabIndex = 1277;
            this.lblEI2Colon.Text = ":";
            this.lblEI2Colon.Visible = false;
            // 
            // txtET3
            // 
            this.txtET3.AutoFillDate = false;
            this.txtET3.BackColor = System.Drawing.Color.White;
            this.txtET3.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtET3.CheckForSymbol = null;
            this.txtET3.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtET3.DecimalPlace = 0;
            this.txtET3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtET3.HelpText = "Enter ET3";
            this.txtET3.HoldMyText = null;
            this.txtET3.IsMandatory = false;
            this.txtET3.IsSingleQuote = true;
            this.txtET3.IsSysmbol = false;
            this.txtET3.Location = new System.Drawing.Point(346, 295);
            this.txtET3.Mask = null;
            this.txtET3.MaxLength = 50;
            this.txtET3.Moveable = false;
            this.txtET3.Name = "txtET3";
            this.txtET3.NameOfControl = "ET3";
            this.txtET3.Prefix = null;
            this.txtET3.ShowBallonTip = false;
            this.txtET3.ShowErrorIcon = false;
            this.txtET3.ShowMessage = null;
            this.txtET3.Size = new System.Drawing.Size(231, 22);
            this.txtET3.Suffix = null;
            this.txtET3.TabIndex = 1268;
            this.txtET3.Visible = false;
            // 
            // lblET3Colon
            // 
            this.lblET3Colon.AutoSize = true;
            this.lblET3Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblET3Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET3Colon.Location = new System.Drawing.Point(325, 298);
            this.lblET3Colon.Moveable = false;
            this.lblET3Colon.Name = "lblET3Colon";
            this.lblET3Colon.NameOfControl = null;
            this.lblET3Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET3Colon.TabIndex = 1274;
            this.lblET3Colon.Text = ":";
            this.lblET3Colon.Visible = false;
            // 
            // txtET2
            // 
            this.txtET2.AutoFillDate = false;
            this.txtET2.BackColor = System.Drawing.Color.White;
            this.txtET2.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtET2.CheckForSymbol = null;
            this.txtET2.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtET2.DecimalPlace = 0;
            this.txtET2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtET2.HelpText = "Enter ET2";
            this.txtET2.HoldMyText = null;
            this.txtET2.IsMandatory = false;
            this.txtET2.IsSingleQuote = true;
            this.txtET2.IsSysmbol = false;
            this.txtET2.Location = new System.Drawing.Point(346, 268);
            this.txtET2.Mask = null;
            this.txtET2.MaxLength = 50;
            this.txtET2.Moveable = false;
            this.txtET2.Name = "txtET2";
            this.txtET2.NameOfControl = "ET2";
            this.txtET2.Prefix = null;
            this.txtET2.ShowBallonTip = false;
            this.txtET2.ShowErrorIcon = false;
            this.txtET2.ShowMessage = null;
            this.txtET2.Size = new System.Drawing.Size(231, 22);
            this.txtET2.Suffix = null;
            this.txtET2.TabIndex = 1267;
            this.txtET2.Visible = false;
            // 
            // lblET2Colon
            // 
            this.lblET2Colon.AutoSize = true;
            this.lblET2Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblET2Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET2Colon.Location = new System.Drawing.Point(325, 273);
            this.lblET2Colon.Moveable = false;
            this.lblET2Colon.Name = "lblET2Colon";
            this.lblET2Colon.NameOfControl = "ET2";
            this.lblET2Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET2Colon.TabIndex = 1272;
            this.lblET2Colon.Text = ":";
            this.lblET2Colon.Visible = false;
            // 
            // lblET3
            // 
            this.lblET3.AutoSize = true;
            this.lblET3.BackColor = System.Drawing.Color.Transparent;
            this.lblET3.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET3.Location = new System.Drawing.Point(242, 297);
            this.lblET3.Moveable = false;
            this.lblET3.Name = "lblET3";
            this.lblET3.NameOfControl = "ET3";
            this.lblET3.Size = new System.Drawing.Size(32, 14);
            this.lblET3.TabIndex = 1273;
            this.lblET3.Text = "ET3";
            this.lblET3.Visible = false;
            // 
            // lblET2
            // 
            this.lblET2.AutoSize = true;
            this.lblET2.BackColor = System.Drawing.Color.Transparent;
            this.lblET2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET2.Location = new System.Drawing.Point(242, 271);
            this.lblET2.Moveable = false;
            this.lblET2.Name = "lblET2";
            this.lblET2.NameOfControl = "ET2";
            this.lblET2.Size = new System.Drawing.Size(32, 14);
            this.lblET2.TabIndex = 1271;
            this.lblET2.Text = "ET2";
            this.lblET2.Visible = false;
            // 
            // txtET1
            // 
            this.txtET1.AutoFillDate = false;
            this.txtET1.BackColor = System.Drawing.Color.White;
            this.txtET1.CCase = CIS_CLibrary.CIS_Textbox.CCasing_Type.Normal;
            this.txtET1.CheckForSymbol = null;
            this.txtET1.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtET1.DecimalPlace = 0;
            this.txtET1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtET1.HelpText = "Enter ET1";
            this.txtET1.HoldMyText = null;
            this.txtET1.IsMandatory = false;
            this.txtET1.IsSingleQuote = true;
            this.txtET1.IsSysmbol = false;
            this.txtET1.Location = new System.Drawing.Point(346, 243);
            this.txtET1.Mask = null;
            this.txtET1.MaxLength = 50;
            this.txtET1.Moveable = false;
            this.txtET1.Name = "txtET1";
            this.txtET1.NameOfControl = "ET1";
            this.txtET1.Prefix = null;
            this.txtET1.ShowBallonTip = false;
            this.txtET1.ShowErrorIcon = false;
            this.txtET1.ShowMessage = null;
            this.txtET1.Size = new System.Drawing.Size(231, 22);
            this.txtET1.Suffix = null;
            this.txtET1.TabIndex = 1266;
            this.txtET1.Visible = false;
            // 
            // lblET1Colon
            // 
            this.lblET1Colon.AutoSize = true;
            this.lblET1Colon.BackColor = System.Drawing.Color.Transparent;
            this.lblET1Colon.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET1Colon.Location = new System.Drawing.Point(325, 245);
            this.lblET1Colon.Moveable = false;
            this.lblET1Colon.Name = "lblET1Colon";
            this.lblET1Colon.NameOfControl = "ET1";
            this.lblET1Colon.Size = new System.Drawing.Size(12, 14);
            this.lblET1Colon.TabIndex = 1270;
            this.lblET1Colon.Text = ":";
            this.lblET1Colon.Visible = false;
            // 
            // lblET1
            // 
            this.lblET1.AutoSize = true;
            this.lblET1.BackColor = System.Drawing.Color.Transparent;
            this.lblET1.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblET1.Location = new System.Drawing.Point(242, 245);
            this.lblET1.Moveable = false;
            this.lblET1.Name = "lblET1";
            this.lblET1.NameOfControl = "ET1";
            this.lblET1.Size = new System.Drawing.Size(32, 14);
            this.lblET1.TabIndex = 1269;
            this.lblET1.Text = "ET1";
            this.lblET1.Visible = false;
            // 
            // frmItemMaster
            // 
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Tile;
            this.ClientSize = new System.Drawing.Size(808, 545);
            this.DoubleBuffered = false;
            this.KeyPreview = true;
            this.Name = "frmItemMaster";
            this.Load += new System.EventHandler(this.frmItemMaster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryCalcvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_AryIsRequired)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_HasDtls_Grd)).EndInit();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboItemCategory;
        internal CIS_CLibrary.CIS_TextLabel lblItemCategoryColun;
        internal CIS_CLibrary.CIS_TextLabel lblItemCategory;
        public CIS_CLibrary.CIS_Textbox txtCode;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboUnits;
        internal CIS_CLibrary.CIS_TextLabel lblUnitsColun;
        internal CIS_CLibrary.CIS_TextLabel lblUnits;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboItemGroup;
        internal CIS_CLibrary.CIS_TextLabel lblItemGroupColun;
        internal CIS_CLibrary.CIS_TextLabel lblItemGroup;
        internal CIS_CLibrary.CIS_Textbox txtItemName;
        internal CIS_CLibrary.CIS_TextLabel lblItemNameColun;
        internal CIS_CLibrary.CIS_TextLabel lblItemName;
        internal CIS_CLibrary.CIS_CheckBox ChkActive;
        internal CIS_CLibrary.CIS_Textbox txtAliasname;
        internal CIS_CLibrary.CIS_TextLabel label1;
        internal CIS_CLibrary.CIS_TextLabel label2;
        internal CIS_CLibrary.CIS_Textbox txtQty;
        internal CIS_CLibrary.CIS_TextLabel ciS_TextLabel1;
        internal CIS_CLibrary.CIS_TextLabel ciS_TextLabel2;
        internal CIS_CLibrary.CIS_TextLabel lblEI1;
        internal CIS_CLibrary.CIS_TextLabel lblEI1Colon;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboEI1;
        internal CIS_CLibrary.CIS_TextLabel lblEI2;
        internal CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboEI2;
        internal CIS_CLibrary.CIS_TextLabel lblEI2Colon;
        internal CIS_CLibrary.CIS_Textbox txtET3;
        internal CIS_CLibrary.CIS_TextLabel lblET3Colon;
        internal CIS_CLibrary.CIS_Textbox txtET2;
        internal CIS_CLibrary.CIS_TextLabel lblET2Colon;
        internal CIS_CLibrary.CIS_TextLabel lblET3;
        internal CIS_CLibrary.CIS_TextLabel lblET2;
        internal CIS_CLibrary.CIS_Textbox txtET1;
        internal CIS_CLibrary.CIS_TextLabel lblET1Colon;
        internal CIS_CLibrary.CIS_TextLabel lblET1;
    }
}
