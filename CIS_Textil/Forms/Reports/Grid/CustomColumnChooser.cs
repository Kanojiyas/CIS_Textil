using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinGrid;

namespace CIS_Textil
{
    public partial class CustomColumnChooser : Form
    {
        private UltraGridBase _grid;
        [AccessedThroughProperty("ultraButtonDeleteColumn")]
        private UltraButton _ultraButtonDeleteColumn;
        [AccessedThroughProperty("ultraButtonNewColumn")]
        private UltraButton _ultraButtonNewColumn;
        [AccessedThroughProperty("ultraGridBagLayoutManager1")]
        private UltraGridBagLayoutManager _ultraGridBagLayoutManager1;
        [AccessedThroughProperty("ultraGridColumnChooser1")]
        private UltraGridColumnChooser _ultraGridColumnChooser1;
        public CustomColumnChooser()
        {
            InitializeComponent();
        }

        private bool IsBandExcluded(UltraGridBand band)
        {
            while (null != band)
            {
                if (ExcludeFromColumnChooser.True == band.ExcludeFromColumnChooser)
                {
                    return true;
                }
                band = band.ParentBand;
            }
            return false;
        }

        private void UltraButtonDeleteColumn_Click(object sender, EventArgs e)
        {
            if (!(this.ultraGridColumnChooser1.CurrentSelectedItem is UltraGridColumn))
            {
                MessageBox.Show(this, "Please select a column to delete.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                UltraGridColumn column = (UltraGridColumn)this.ultraGridColumnChooser1.CurrentSelectedItem;
                if (column.IsBound)
                {
                    MessageBox.Show(this, "Only unbound columns can be deleted.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    DialogResult dlgResult = MessageBox.Show(this, string.Format("Deleting {0} column. Continue?", column.Header.Caption), this.Text, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (DialogResult.OK == dlgResult)
                    {
                        column.Band.Columns.Remove(column);
                    }
                }
            }
        }

        private void UltraButtonNewColumn_Click(object sender, EventArgs e)
        {
            UltraGridBand selectedBand = this.ultraGridColumnChooser1.CurrentBand;
            if (null != selectedBand)
            {
                new NewColumnDialog { Band = selectedBand }.ShowDialog();
            }
        }

        public UltraGridColumnChooser ColumnChooserControl
        {
            get
            {
                return this.ultraGridColumnChooser1;
            }
        }

        public UltraGridBand CurrentBand
        {
            get
            {
                return this.ColumnChooserControl.CurrentBand;
            }
            set
            {
                if ((value != null) && ((this.Grid == null) || (this.Grid != value.Layout.Grid)))
                {
                    throw new ArgumentException();
                }
            }
        }

        public UltraGridBase Grid
        {
            get
            {
                return this._grid;
            }
            set
            {
                if (value != this._grid)
                {
                    this._grid = value;
                    this.ultraGridColumnChooser1.SourceGrid = this._grid;
                }
            }
        }

        private void CustomColumnChooser_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (CIS_Utilities.CIS_Dialog.Show("Do you want to close this screen ?", GetAssemblyInfo.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
