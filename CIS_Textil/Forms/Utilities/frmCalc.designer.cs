namespace CIS_Textil
{
    partial class frmCalc
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
            this.txtResult = new CIS_CLibrary.CIS_Textbox();
            this.btnnumSign = new System.Windows.Forms.Button();
            this.btnInverse = new System.Windows.Forms.Button();
            this.btnSqrt = new System.Windows.Forms.Button();
            this.btnMemoryRecall = new System.Windows.Forms.Button();
            this.btnMemoryMinus = new System.Windows.Forms.Button();
            this.btnMemoryPlus = new System.Windows.Forms.Button();
            this.btnCLR = new System.Windows.Forms.Button();
            this.btnBackspace = new System.Windows.Forms.Button();
            this.btnCLR_Curr = new System.Windows.Forms.Button();
            this.btn_Operator_Add = new System.Windows.Forms.Button();
            this.btn_Operator_Subt = new System.Windows.Forms.Button();
            this.btnPow = new System.Windows.Forms.Button();
            this.btn_Operator_div = new System.Windows.Forms.Button();
            this.btn_Operator_Multi = new System.Windows.Forms.Button();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnNumPeriod = new System.Windows.Forms.Button();
            this.btn_Num_0 = new System.Windows.Forms.Button();
            this.btn_Num_9 = new System.Windows.Forms.Button();
            this.btn_Num_8 = new System.Windows.Forms.Button();
            this.btn_Num_7 = new System.Windows.Forms.Button();
            this.btn_Num_6 = new System.Windows.Forms.Button();
            this.btn_Num_5 = new System.Windows.Forms.Button();
            this.btn_Num_4 = new System.Windows.Forms.Button();
            this.btn_Num_3 = new System.Windows.Forms.Button();
            this.btn_Num_2 = new System.Windows.Forms.Button();
            this.btn_Num_1 = new System.Windows.Forms.Button();
            this.btnMemStatus = new System.Windows.Forms.Button();
            this.lblTop = new CIS_CLibrary.CIS_TextLabel();
            this.lblBottom = new CIS_CLibrary.CIS_TextLabel();
            this.lblLeft = new CIS_CLibrary.CIS_TextLabel();
            this.lblRIght = new CIS_CLibrary.CIS_TextLabel();
            this.SuspendLayout();
            // 
            // txtResult
            // 
            this.txtResult.AutoFillDate = false;
            this.txtResult.BackColor = System.Drawing.Color.PapayaWhip;
            this.txtResult.CheckForSymbol = null;
            this.txtResult.Control_Type = CIS_CLibrary.CIS_Textbox.Attribute_Type.AcceptAll;
            this.txtResult.DecimalPlace = 0;
            this.txtResult.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResult.HelpText = "";
            this.txtResult.HoldMyText = null;
            this.txtResult.IsMandatory = true;
            this.txtResult.IsSingleQuote = true;
            this.txtResult.IsSysmbol = false;
            this.txtResult.Location = new System.Drawing.Point(12, 17);
            this.txtResult.Mask = null;
            this.txtResult.MaxLength = 50;
            this.txtResult.Name = "txtResult";
            this.txtResult.NameOfControl = null;
            this.txtResult.Prefix = null;
            this.txtResult.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtResult.ShowBallonTip = false;
            this.txtResult.ShowErrorIcon = false;
            this.txtResult.ShowMessage = null;
            this.txtResult.Size = new System.Drawing.Size(251, 20);
            this.txtResult.Suffix = null;
            this.txtResult.TabIndex = 122;
            this.txtResult.Click += new System.EventHandler(this.txtResult_TextChanged);
            this.txtResult.TextChanged += new System.EventHandler(this.txtResult_TextChanged);
            this.txtResult.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtResult_KeyPress);
            // 
            // btnnumSign
            // 
            this.btnnumSign.BackColor = System.Drawing.SystemColors.Control;
            this.btnnumSign.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnnumSign.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnnumSign.ForeColor = System.Drawing.Color.Blue;
            this.btnnumSign.Location = new System.Drawing.Point(100, 175);
            this.btnnumSign.Name = "btnnumSign";
            this.btnnumSign.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnnumSign.Size = new System.Drawing.Size(31, 26);
            this.btnnumSign.TabIndex = 121;
            this.btnnumSign.TabStop = false;
            this.btnnumSign.Text = "+/-";
            this.btnnumSign.UseVisualStyleBackColor = false;
            this.btnnumSign.Click += new System.EventHandler(this.btnnumSign_Click);
            // 
            // btnInverse
            // 
            this.btnInverse.BackColor = System.Drawing.SystemColors.Control;
            this.btnInverse.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnInverse.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInverse.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnInverse.Location = new System.Drawing.Point(220, 143);
            this.btnInverse.Name = "btnInverse";
            this.btnInverse.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnInverse.Size = new System.Drawing.Size(40, 28);
            this.btnInverse.TabIndex = 120;
            this.btnInverse.TabStop = false;
            this.btnInverse.Text = "1/x";
            this.btnInverse.UseVisualStyleBackColor = false;
            this.btnInverse.Click += new System.EventHandler(this.btnInverse_Click);
            // 
            // btnSqrt
            // 
            this.btnSqrt.BackColor = System.Drawing.SystemColors.Control;
            this.btnSqrt.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSqrt.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSqrt.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnSqrt.Location = new System.Drawing.Point(220, 79);
            this.btnSqrt.Name = "btnSqrt";
            this.btnSqrt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnSqrt.Size = new System.Drawing.Size(40, 28);
            this.btnSqrt.TabIndex = 119;
            this.btnSqrt.TabStop = false;
            this.btnSqrt.Text = "sqrt";
            this.btnSqrt.UseVisualStyleBackColor = false;
            this.btnSqrt.Click += new System.EventHandler(this.btnSqrt_Click);
            // 
            // btnMemoryRecall
            // 
            this.btnMemoryRecall.BackColor = System.Drawing.SystemColors.Control;
            this.btnMemoryRecall.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMemoryRecall.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMemoryRecall.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnMemoryRecall.Location = new System.Drawing.Point(12, 79);
            this.btnMemoryRecall.Name = "btnMemoryRecall";
            this.btnMemoryRecall.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnMemoryRecall.Size = new System.Drawing.Size(36, 26);
            this.btnMemoryRecall.TabIndex = 117;
            this.btnMemoryRecall.TabStop = false;
            this.btnMemoryRecall.Text = "MR";
            this.btnMemoryRecall.UseVisualStyleBackColor = false;
            this.btnMemoryRecall.Click += new System.EventHandler(this.btnMemoryRecall_Click);
            // 
            // btnMemoryMinus
            // 
            this.btnMemoryMinus.BackColor = System.Drawing.SystemColors.Control;
            this.btnMemoryMinus.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMemoryMinus.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMemoryMinus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnMemoryMinus.Location = new System.Drawing.Point(14, 144);
            this.btnMemoryMinus.Name = "btnMemoryMinus";
            this.btnMemoryMinus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnMemoryMinus.Size = new System.Drawing.Size(36, 56);
            this.btnMemoryMinus.TabIndex = 116;
            this.btnMemoryMinus.TabStop = false;
            this.btnMemoryMinus.Text = "M-";
            this.btnMemoryMinus.UseVisualStyleBackColor = false;
            this.btnMemoryMinus.Click += new System.EventHandler(this.btnMemoryMinus_Click);
            // 
            // btnMemoryPlus
            // 
            this.btnMemoryPlus.BackColor = System.Drawing.SystemColors.Control;
            this.btnMemoryPlus.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMemoryPlus.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMemoryPlus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnMemoryPlus.Location = new System.Drawing.Point(12, 111);
            this.btnMemoryPlus.Name = "btnMemoryPlus";
            this.btnMemoryPlus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnMemoryPlus.Size = new System.Drawing.Size(36, 28);
            this.btnMemoryPlus.TabIndex = 115;
            this.btnMemoryPlus.TabStop = false;
            this.btnMemoryPlus.Text = "M+";
            this.btnMemoryPlus.UseVisualStyleBackColor = false;
            this.btnMemoryPlus.Click += new System.EventHandler(this.btnMemoryPlus_Click);
            // 
            // btnCLR
            // 
            this.btnCLR.BackColor = System.Drawing.SystemColors.Control;
            this.btnCLR.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCLR.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCLR.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCLR.Location = new System.Drawing.Point(204, 48);
            this.btnCLR.Name = "btnCLR";
            this.btnCLR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCLR.Size = new System.Drawing.Size(55, 27);
            this.btnCLR.TabIndex = 114;
            this.btnCLR.TabStop = false;
            this.btnCLR.Text = "C";
            this.btnCLR.UseVisualStyleBackColor = false;
            this.btnCLR.Click += new System.EventHandler(this.btnCLR_Click);
            // 
            // btnBackspace
            // 
            this.btnBackspace.BackColor = System.Drawing.SystemColors.Control;
            this.btnBackspace.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnBackspace.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackspace.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnBackspace.Location = new System.Drawing.Point(60, 48);
            this.btnBackspace.Name = "btnBackspace";
            this.btnBackspace.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnBackspace.Size = new System.Drawing.Size(83, 27);
            this.btnBackspace.TabIndex = 113;
            this.btnBackspace.TabStop = false;
            this.btnBackspace.Text = "Backspace";
            this.btnBackspace.UseVisualStyleBackColor = false;
            this.btnBackspace.Click += new System.EventHandler(this.btnBackspace_Click);
            // 
            // btnCLR_Curr
            // 
            this.btnCLR_Curr.BackColor = System.Drawing.SystemColors.Control;
            this.btnCLR_Curr.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCLR_Curr.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCLR_Curr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCLR_Curr.Location = new System.Drawing.Point(148, 48);
            this.btnCLR_Curr.Name = "btnCLR_Curr";
            this.btnCLR_Curr.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCLR_Curr.Size = new System.Drawing.Size(53, 27);
            this.btnCLR_Curr.TabIndex = 112;
            this.btnCLR_Curr.TabStop = false;
            this.btnCLR_Curr.Text = "CE";
            this.btnCLR_Curr.UseVisualStyleBackColor = false;
            this.btnCLR_Curr.Click += new System.EventHandler(this.btnCLR_Curr_Click);
            // 
            // btn_Operator_Add
            // 
            this.btn_Operator_Add.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Operator_Add.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Operator_Add.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Operator_Add.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Operator_Add.Location = new System.Drawing.Point(180, 79);
            this.btn_Operator_Add.Name = "btn_Operator_Add";
            this.btn_Operator_Add.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Operator_Add.Size = new System.Drawing.Size(31, 26);
            this.btn_Operator_Add.TabIndex = 111;
            this.btn_Operator_Add.TabStop = false;
            this.btn_Operator_Add.Text = "+";
            this.btn_Operator_Add.UseVisualStyleBackColor = false;
            this.btn_Operator_Add.Click += new System.EventHandler(this.btn_Operator_Add_Click);
            // 
            // btn_Operator_Subt
            // 
            this.btn_Operator_Subt.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Operator_Subt.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Operator_Subt.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Operator_Subt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Operator_Subt.Location = new System.Drawing.Point(180, 111);
            this.btn_Operator_Subt.Name = "btn_Operator_Subt";
            this.btn_Operator_Subt.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Operator_Subt.Size = new System.Drawing.Size(31, 28);
            this.btn_Operator_Subt.TabIndex = 110;
            this.btn_Operator_Subt.TabStop = false;
            this.btn_Operator_Subt.Text = "-";
            this.btn_Operator_Subt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_Operator_Subt.UseVisualStyleBackColor = false;
            this.btn_Operator_Subt.Click += new System.EventHandler(this.btn_Operator_Subt_Click);
            // 
            // btnPow
            // 
            this.btnPow.BackColor = System.Drawing.SystemColors.Control;
            this.btnPow.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnPow.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPow.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btnPow.Location = new System.Drawing.Point(220, 111);
            this.btnPow.Name = "btnPow";
            this.btnPow.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnPow.Size = new System.Drawing.Size(40, 28);
            this.btnPow.TabIndex = 109;
            this.btnPow.TabStop = false;
            this.btnPow.Text = "x^";
            this.btnPow.UseVisualStyleBackColor = false;
            this.btnPow.Click += new System.EventHandler(this.btnPow_Click);
            // 
            // btn_Operator_div
            // 
            this.btn_Operator_div.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Operator_div.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Operator_div.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Operator_div.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Operator_div.Location = new System.Drawing.Point(180, 175);
            this.btn_Operator_div.Name = "btn_Operator_div";
            this.btn_Operator_div.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Operator_div.Size = new System.Drawing.Size(31, 26);
            this.btn_Operator_div.TabIndex = 108;
            this.btn_Operator_div.TabStop = false;
            this.btn_Operator_div.Text = "/";
            this.btn_Operator_div.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_Operator_div.UseVisualStyleBackColor = false;
            this.btn_Operator_div.Click += new System.EventHandler(this.btn_Operator_div_Click);
            // 
            // btn_Operator_Multi
            // 
            this.btn_Operator_Multi.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Operator_Multi.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Operator_Multi.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Operator_Multi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Operator_Multi.Location = new System.Drawing.Point(180, 143);
            this.btn_Operator_Multi.Name = "btn_Operator_Multi";
            this.btn_Operator_Multi.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Operator_Multi.Size = new System.Drawing.Size(31, 28);
            this.btn_Operator_Multi.TabIndex = 107;
            this.btn_Operator_Multi.TabStop = false;
            this.btn_Operator_Multi.Text = "x";
            this.btn_Operator_Multi.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_Operator_Multi.UseVisualStyleBackColor = false;
            this.btn_Operator_Multi.Click += new System.EventHandler(this.btn_Operator_Multi_Click);
            // 
            // btnCalculate
            // 
            this.btnCalculate.BackColor = System.Drawing.SystemColors.Control;
            this.btnCalculate.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCalculate.Font = new System.Drawing.Font("Verdana", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalculate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCalculate.Location = new System.Drawing.Point(220, 175);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCalculate.Size = new System.Drawing.Size(40, 25);
            this.btnCalculate.TabIndex = 106;
            this.btnCalculate.TabStop = false;
            this.btnCalculate.Text = "=";
            this.btnCalculate.UseVisualStyleBackColor = false;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // btnNumPeriod
            // 
            this.btnNumPeriod.BackColor = System.Drawing.SystemColors.Control;
            this.btnNumPeriod.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnNumPeriod.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNumPeriod.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnNumPeriod.Location = new System.Drawing.Point(140, 175);
            this.btnNumPeriod.Name = "btnNumPeriod";
            this.btnNumPeriod.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnNumPeriod.Size = new System.Drawing.Size(31, 26);
            this.btnNumPeriod.TabIndex = 105;
            this.btnNumPeriod.TabStop = false;
            this.btnNumPeriod.Text = ".";
            this.btnNumPeriod.UseVisualStyleBackColor = false;
            this.btnNumPeriod.Click += new System.EventHandler(this.btnNumPeriod_Click);
            // 
            // btn_Num_0
            // 
            this.btn_Num_0.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Num_0.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Num_0.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Num_0.ForeColor = System.Drawing.Color.Blue;
            this.btn_Num_0.Location = new System.Drawing.Point(60, 175);
            this.btn_Num_0.Name = "btn_Num_0";
            this.btn_Num_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Num_0.Size = new System.Drawing.Size(31, 26);
            this.btn_Num_0.TabIndex = 104;
            this.btn_Num_0.TabStop = false;
            this.btn_Num_0.Text = "0";
            this.btn_Num_0.UseVisualStyleBackColor = false;
            this.btn_Num_0.Click += new System.EventHandler(this.btn_Num_0_Click);
            // 
            // btn_Num_9
            // 
            this.btn_Num_9.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Num_9.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Num_9.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Num_9.ForeColor = System.Drawing.Color.Blue;
            this.btn_Num_9.Location = new System.Drawing.Point(140, 79);
            this.btn_Num_9.Name = "btn_Num_9";
            this.btn_Num_9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Num_9.Size = new System.Drawing.Size(31, 27);
            this.btn_Num_9.TabIndex = 103;
            this.btn_Num_9.TabStop = false;
            this.btn_Num_9.Text = "9";
            this.btn_Num_9.UseVisualStyleBackColor = false;
            this.btn_Num_9.Click += new System.EventHandler(this.btn_Num_9_Click);
            // 
            // btn_Num_8
            // 
            this.btn_Num_8.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Num_8.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Num_8.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Num_8.ForeColor = System.Drawing.Color.Blue;
            this.btn_Num_8.Location = new System.Drawing.Point(100, 79);
            this.btn_Num_8.Name = "btn_Num_8";
            this.btn_Num_8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Num_8.Size = new System.Drawing.Size(31, 28);
            this.btn_Num_8.TabIndex = 102;
            this.btn_Num_8.TabStop = false;
            this.btn_Num_8.Text = "8";
            this.btn_Num_8.UseVisualStyleBackColor = false;
            this.btn_Num_8.Click += new System.EventHandler(this.btn_Num_8_Click);
            // 
            // btn_Num_7
            // 
            this.btn_Num_7.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Num_7.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Num_7.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Num_7.ForeColor = System.Drawing.Color.Blue;
            this.btn_Num_7.Location = new System.Drawing.Point(60, 79);
            this.btn_Num_7.Name = "btn_Num_7";
            this.btn_Num_7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Num_7.Size = new System.Drawing.Size(31, 27);
            this.btn_Num_7.TabIndex = 101;
            this.btn_Num_7.TabStop = false;
            this.btn_Num_7.Text = "7";
            this.btn_Num_7.UseVisualStyleBackColor = false;
            this.btn_Num_7.Click += new System.EventHandler(this.btn_Num_7_Click);
            // 
            // btn_Num_6
            // 
            this.btn_Num_6.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Num_6.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Num_6.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Num_6.ForeColor = System.Drawing.Color.Blue;
            this.btn_Num_6.Location = new System.Drawing.Point(140, 111);
            this.btn_Num_6.Name = "btn_Num_6";
            this.btn_Num_6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Num_6.Size = new System.Drawing.Size(31, 28);
            this.btn_Num_6.TabIndex = 100;
            this.btn_Num_6.TabStop = false;
            this.btn_Num_6.Text = "6";
            this.btn_Num_6.UseVisualStyleBackColor = false;
            this.btn_Num_6.Click += new System.EventHandler(this.btn_Num_6_Click);
            // 
            // btn_Num_5
            // 
            this.btn_Num_5.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Num_5.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Num_5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Num_5.ForeColor = System.Drawing.Color.Blue;
            this.btn_Num_5.Location = new System.Drawing.Point(100, 111);
            this.btn_Num_5.Name = "btn_Num_5";
            this.btn_Num_5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Num_5.Size = new System.Drawing.Size(31, 28);
            this.btn_Num_5.TabIndex = 99;
            this.btn_Num_5.TabStop = false;
            this.btn_Num_5.Text = "5";
            this.btn_Num_5.UseVisualStyleBackColor = false;
            this.btn_Num_5.Click += new System.EventHandler(this.btn_Num_5_Click);
            // 
            // btn_Num_4
            // 
            this.btn_Num_4.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Num_4.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Num_4.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Num_4.ForeColor = System.Drawing.Color.Blue;
            this.btn_Num_4.Location = new System.Drawing.Point(60, 111);
            this.btn_Num_4.Name = "btn_Num_4";
            this.btn_Num_4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Num_4.Size = new System.Drawing.Size(31, 28);
            this.btn_Num_4.TabIndex = 98;
            this.btn_Num_4.TabStop = false;
            this.btn_Num_4.Text = "4";
            this.btn_Num_4.UseVisualStyleBackColor = false;
            this.btn_Num_4.Click += new System.EventHandler(this.btn_Num_4_Click);
            // 
            // btn_Num_3
            // 
            this.btn_Num_3.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Num_3.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Num_3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Num_3.ForeColor = System.Drawing.Color.Blue;
            this.btn_Num_3.Location = new System.Drawing.Point(140, 143);
            this.btn_Num_3.Name = "btn_Num_3";
            this.btn_Num_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Num_3.Size = new System.Drawing.Size(31, 28);
            this.btn_Num_3.TabIndex = 97;
            this.btn_Num_3.TabStop = false;
            this.btn_Num_3.Text = "3";
            this.btn_Num_3.UseVisualStyleBackColor = false;
            this.btn_Num_3.Click += new System.EventHandler(this.btn_Num_3_Click);
            // 
            // btn_Num_2
            // 
            this.btn_Num_2.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Num_2.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Num_2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Num_2.ForeColor = System.Drawing.Color.Blue;
            this.btn_Num_2.Location = new System.Drawing.Point(100, 143);
            this.btn_Num_2.Name = "btn_Num_2";
            this.btn_Num_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Num_2.Size = new System.Drawing.Size(31, 28);
            this.btn_Num_2.TabIndex = 96;
            this.btn_Num_2.TabStop = false;
            this.btn_Num_2.Text = "2";
            this.btn_Num_2.UseVisualStyleBackColor = false;
            this.btn_Num_2.Click += new System.EventHandler(this.btn_Num_2_Click);
            // 
            // btn_Num_1
            // 
            this.btn_Num_1.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Num_1.Cursor = System.Windows.Forms.Cursors.Default;
            this.btn_Num_1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Num_1.ForeColor = System.Drawing.Color.Blue;
            this.btn_Num_1.Location = new System.Drawing.Point(60, 143);
            this.btn_Num_1.Name = "btn_Num_1";
            this.btn_Num_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btn_Num_1.Size = new System.Drawing.Size(31, 28);
            this.btn_Num_1.TabIndex = 95;
            this.btn_Num_1.TabStop = false;
            this.btn_Num_1.Text = "1";
            this.btn_Num_1.UseVisualStyleBackColor = false;
            this.btn_Num_1.Click += new System.EventHandler(this.btn_Num_1_Click);
            // 
            // btnMemStatus
            // 
            this.btnMemStatus.BackColor = System.Drawing.SystemColors.Control;
            this.btnMemStatus.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnMemStatus.Enabled = false;
            this.btnMemStatus.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMemStatus.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.btnMemStatus.Location = new System.Drawing.Point(12, 48);
            this.btnMemStatus.Name = "btnMemStatus";
            this.btnMemStatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnMemStatus.Size = new System.Drawing.Size(36, 26);
            this.btnMemStatus.TabIndex = 118;
            this.btnMemStatus.UseVisualStyleBackColor = false;
            // 
            // lblTop
            // 
            this.lblTop.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTop.Location = new System.Drawing.Point(0, 0);
            this.lblTop.Name = "lblTop";
            this.lblTop.Size = new System.Drawing.Size(275, 3);
            this.lblTop.TabIndex = 123;
            // 
            // lblBottom
            // 
            this.lblBottom.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBottom.Location = new System.Drawing.Point(0, 216);
            this.lblBottom.Name = "lblBottom";
            this.lblBottom.Size = new System.Drawing.Size(275, 3);
            this.lblBottom.TabIndex = 124;
            // 
            // lblLeft
            // 
            this.lblLeft.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLeft.Location = new System.Drawing.Point(0, 3);
            this.lblLeft.Name = "lblLeft";
            this.lblLeft.Size = new System.Drawing.Size(3, 213);
            this.lblLeft.TabIndex = 125;
            // 
            // lblRIght
            // 
            this.lblRIght.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblRIght.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblRIght.Location = new System.Drawing.Point(272, 3);
            this.lblRIght.Name = "lblRIght";
            this.lblRIght.Size = new System.Drawing.Size(3, 213);
            this.lblRIght.TabIndex = 126;
            // 
            // frmCalc1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(275, 219);
            this.Controls.Add(this.lblRIght);
            this.Controls.Add(this.lblLeft);
            this.Controls.Add(this.lblBottom);
            this.Controls.Add(this.lblTop);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnnumSign);
            this.Controls.Add(this.btnInverse);
            this.Controls.Add(this.btnSqrt);
            this.Controls.Add(this.btnMemStatus);
            this.Controls.Add(this.btnMemoryRecall);
            this.Controls.Add(this.btnMemoryMinus);
            this.Controls.Add(this.btnMemoryPlus);
            this.Controls.Add(this.btnCLR);
            this.Controls.Add(this.btnBackspace);
            this.Controls.Add(this.btnCLR_Curr);
            this.Controls.Add(this.btn_Operator_Add);
            this.Controls.Add(this.btn_Operator_Subt);
            this.Controls.Add(this.btnPow);
            this.Controls.Add(this.btn_Operator_div);
            this.Controls.Add(this.btn_Operator_Multi);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.btnNumPeriod);
            this.Controls.Add(this.btn_Num_0);
            this.Controls.Add(this.btn_Num_9);
            this.Controls.Add(this.btn_Num_8);
            this.Controls.Add(this.btn_Num_7);
            this.Controls.Add(this.btn_Num_6);
            this.Controls.Add(this.btn_Num_5);
            this.Controls.Add(this.btn_Num_4);
            this.Controls.Add(this.btn_Num_3);
            this.Controls.Add(this.btn_Num_2);
            this.Controls.Add(this.btn_Num_1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmCalc1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmCalc";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmCalc_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmCalc_KeyPress);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmCalc1_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal CIS_CLibrary.CIS_Textbox txtResult;
        public System.Windows.Forms.Button btnnumSign;
        public System.Windows.Forms.Button btnInverse;
        public System.Windows.Forms.Button btnSqrt;
        public System.Windows.Forms.Button btnMemoryRecall;
        public System.Windows.Forms.Button btnMemoryMinus;
        public System.Windows.Forms.Button btnMemoryPlus;
        public System.Windows.Forms.Button btnCLR;
        public System.Windows.Forms.Button btnBackspace;
        public System.Windows.Forms.Button btnCLR_Curr;
        public System.Windows.Forms.Button btn_Operator_Add;
        public System.Windows.Forms.Button btn_Operator_Subt;
        public System.Windows.Forms.Button btnPow;
        public System.Windows.Forms.Button btn_Operator_div;
        public System.Windows.Forms.Button btn_Operator_Multi;
        public System.Windows.Forms.Button btnCalculate;
        public System.Windows.Forms.Button btnNumPeriod;
        public System.Windows.Forms.Button btn_Num_0;
        public System.Windows.Forms.Button btn_Num_9;
        public System.Windows.Forms.Button btn_Num_8;
        public System.Windows.Forms.Button btn_Num_7;
        public System.Windows.Forms.Button btn_Num_6;
        public System.Windows.Forms.Button btn_Num_5;
        public System.Windows.Forms.Button btn_Num_4;
        public System.Windows.Forms.Button btn_Num_3;
        public System.Windows.Forms.Button btn_Num_2;
        public System.Windows.Forms.Button btn_Num_1;
        public System.Windows.Forms.Button btnMemStatus;
        private CIS_CLibrary.CIS_TextLabel lblTop;
        private CIS_CLibrary.CIS_TextLabel lblBottom;
        private CIS_CLibrary.CIS_TextLabel lblLeft;
        private CIS_CLibrary.CIS_TextLabel lblRIght;
    }
}