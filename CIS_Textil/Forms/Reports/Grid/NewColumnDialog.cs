using System;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;

namespace CIS_Textil
{
    public partial class NewColumnDialog : Form
    {
        private UltraGridColumn _createdColumn;
        private UltraGridBand _band;
        
        public NewColumnDialog()
        {
            InitializeComponent();
        }

        private bool CreateColumnHelper()
        {
            string columnName = this.ultraTextEditorName.Text;
            if (null != columnName)
            {
                columnName = columnName.Trim();
            }
            if ((columnName == null) || (columnName.Length <= 0))
            {
                MessageBox.Show(this, "Please enter a column name.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (this.Band.Columns.Exists(columnName) && ((CreatedColumn == null) || (CreatedColumn.Key != columnName)))
            {
                MessageBox.Show(this, "A column by this name already exists. Please enter a different name.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            ValueListItem item = (ValueListItem)ultraComboEditorType.SelectedItem;
            if (null == item)
            {
                MessageBox.Show(this, "Please select the type of column you want to add.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return false;
            }
            if (null == this.CreatedColumn)
            {
                this._createdColumn = this.Band.Columns.Add(columnName);
            }
            else
            {
                this.CreatedColumn.Key = columnName;
                this.CreatedColumn.DataType = (System.Type)item.DataValue;
                this.CreatedColumn.Formula = this.ultraTextEditorFormula.Text;
            }
            return true;
        }

        private void LoadTypesCombo()
        {
            object[] arr = new object[] { typeof(string), "Text", typeof(bool), "Checkbox", typeof(decimal), "Currency", typeof(int), "Whole Number", typeof(double), "Real Number", typeof(DateTime), "Date" };
            this.ultraComboEditorType.Items.Clear();
            for (int i = 0; i <= (arr.Length - 1); i += 2)
            {
                System.Type type = (System.Type) arr[i];
                string description = arr[i + 1].ToString();
                this.ultraComboEditorType.Items.Add(type, description);
            }

            ValueList vl = this.ultraComboEditorType.Items[0].ValueList;
            vl.DisplayStyle = ValueListDisplayStyle.DisplayText;
            vl.SortStyle = ValueListSortStyle.Ascending;
            if (this.ultraComboEditorType.Items.Count > 0)
            {
                this.ultraComboEditorType.SelectedIndex = 0;
            }
        }

        private void NewColumnDialog_Load(object sender, EventArgs e)
        {
            this.LoadTypesCombo();
        }

        private void UltraButtonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            if (null != this.CreatedColumn)
            {
                this.Band.Columns.Remove(this.CreatedColumn);
            }
            this._createdColumn = null;
        }

        private void UltraButtonOk_Click(object sender, EventArgs e)
        {
            if (this.CreateColumnHelper())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        //private void UltraTextEditorFormula_EditorButtonClick(object sender, EditorButtonEventArgs e)
        //{
        //    this.CreateColumnHelper();
        //    if (null != this.CreatedColumn)
        //    {
        //        if (null == this.CreatedColumn.Layout.Grid.CalcManager)
        //        {
        //            this.CreatedColumn.Layout.Grid.CalcManager = new UltraCalcManager();
        //        }
        //        FormulaBuilderDialog dlg = new FormulaBuilderDialog(this.CreatedColumn, true);
        //        DialogResult result = dlg.ShowDialog(this);
        //        if (DialogResult.OK == result)
        //        {
        //            this.ultraTextEditorFormula.Text = dlg.Formula;
        //        }
        //        this.CreatedColumn.Formula = this.ultraTextEditorFormula.Text;
        //    }
        //}

        public UltraGridBand Band
        {
            get
            {
                return this._band;
            }
            set
            {
                this._band = value;
            }
        }

        public UltraGridColumn CreatedColumn
        {
            get
            {
                return this._createdColumn;
            }
        }

    }
}
