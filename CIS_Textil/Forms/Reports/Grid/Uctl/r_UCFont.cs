using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinCalcManager;
using Infragistics.Win.UltraWinCalcManager.FormulaBuilder;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using System.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_MultiColumnComboBox;
using CIS_DBLayer;using CIS_Bussiness;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic;
namespace CIS_Textil
{
    public partial class r_UCFont : UserControl
    {
        private Label _Label3;
        private IContainer components;
        public static int iIDentity = 0;
        public static UltraGrid UGrid_Rpt = new UltraGrid();

        public r_UCFont()
        {
            InitializeComponent();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            if (decimal.Compare(Localization.ParseNativeDecimal(this.cboSize.Text), decimal.Zero) == 0)
            {
                this.cboSize.Text = Conversions.ToString(8);
            }
            UGrid_Rpt.Font = new Font(this.cboFont.Text, Convert.ToSingle(Localization.ParseNativeDecimal(this.cboSize.Text)), FontStyle.Regular, GraphicsUnit.Point, 0);
            ColumnEnumerator col = UGrid_Rpt.DisplayLayout.Bands[0].Columns.GetEnumerator();
            while (col.MoveNext())
            {
                col.Current.PerformAutoResize(PerformAutoSizeType.AllRowsInBand);
            }
        }

        private void r_UCFont_Load(object sender, EventArgs e)
        {
            int num;
            foreach (FontFamily ff in FontFamily.Families)
            {
                if (ff.IsStyleAvailable(FontStyle.Regular))
                {
                    this.cboFont.Items.Add(ff.Name);
                }
            }
            int i = 8;
            do
            {
                this.cboSize.Items.Add(i);
                i++;
                num = 14;
            }
            while (i <= num);
            this.cboFont.Text = "Arial";
            this.cboSize.Text = "8";
        }
    }
}
