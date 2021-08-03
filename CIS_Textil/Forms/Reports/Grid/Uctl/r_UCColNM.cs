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
    public partial class r_UCColNM : UserControl
    {
        [AccessedThroughProperty("fgDtls1")]
        private DataGridViewEx _fgDtls1;
        public static DataGridViewEx fgDtls1;

        private SplitContainer _spc_RenameCol;
        public static ComboBox cboRptlst;
        public static DataGridViewEx fgDtls;
        public static int iIDentity = 0;
        public static UltraGrid UGrid_Rpt = new UltraGrid();

        private enum Col : int
        {
            Col_ID,
            Data_Col,
            Data_rmd,
            Data_rmdnew,
            Align
        }

        private enum ReportLst : int
        {
            ReportID,
            QueryID,
            QueryName,
            IsNewRpt
        }

        public r_UCColNM()
        {
            InitializeComponent();
            fgDtls1 = new DataGridViewEx();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.chkUpdateInRpt.Checked)
                {
                    string[] strSelect = cboRptlst.SelectedValue.ToString().Split(new char[] { ';' });
                    DataGridViewEx fgDtls1 = fgDtls;
                    string strQuery = string.Empty;
                    for (int i = 0; i <= (fgDtls1.Rows.Count - 1); i++)
                    {
                        DataGridViewRow Rws = fgDtls1.Rows[i];
                        strQuery = strQuery + string.Format("Update {0} Set Rpt_ColName = '{1}' Where ReportID = {2} And Qry_ColName = '{3}';" + Environment.NewLine, new object[] { "tbl_ReportConfigDtls", Rws.Cells[3].Value, strSelect[0].ToString(), Rws.Cells[1].Value });
                        Rws = null;
                    }
                    DB.ExecuteSQL(strQuery);
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecuritySuccess, "Success", "Record Deleted Successfully.");
                    fgDtls1 = null;
                }
                BandEnumerator band = UGrid_Rpt.DisplayLayout.Bands.GetEnumerator();
                while (band.MoveNext())
                {
                    ColumnEnumerator col = band.Current.Columns.GetEnumerator();
                    while (col.MoveNext())
                    {
                        UltraGridColumn column = col.Current;
                        column.Header.Caption = Conversions.ToString(fgDtls.Rows[column.Index].Cells[3].Value);
                        object frm = fgDtls.Rows[column.Index].Cells[4].Value;
                        if (Operators.ConditionalCompareObjectEqual(frm, "C", false))
                        {
                            column.CellAppearance.TextHAlign = HAlign.Center;
                        }
                        else if (Operators.ConditionalCompareObjectEqual(frm, "L", false))
                        {
                            column.CellAppearance.TextHAlign = HAlign.Left;
                        }
                        else if (Operators.ConditionalCompareObjectEqual(frm, "R", false))
                        {
                            column.CellAppearance.TextHAlign = HAlign.Right;
                        }
                    }
                }
                for (int iRow = 0; iRow <= fgDtls1.Rows.Count - 1; iRow++)
                {
                    for (int iCol = 0; iCol <= fgDtls1.ColumnCount - 1; iCol++)
                    {
                        fgDtls.Rows[iRow].Cells[2].Value = RuntimeHelpers.GetObjectValue(fgDtls1.Rows[iRow].Cells[2].Value);
                        fgDtls.Rows[iRow].Cells[3].Value = RuntimeHelpers.GetObjectValue(fgDtls1.Rows[iRow].Cells[3].Value);
                        fgDtls.Rows[iRow].Cells[4].Value = RuntimeHelpers.GetObjectValue(fgDtls1.Rows[iRow].Cells[4].Value);
                    }
                }
                fgDtls1 = null;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                ProjectData.ClearProjectError();
            }
        }

        private void r_UCColNM_Load(object sender, EventArgs e)
        {
            DataGridView view = fgDtls1;
            Navigate.SetPropertydtlGrid(spc_RenameCol.Panel1, view, DockStyle.Fill);
            DetailGrid_Setup.AddColto_Grid(ref  view, 0, "Sr. No.", "SrNo", 60, 10, 0, false, false,false, Enum_Define.DataType.pNumeric, DataGridViewContentAlignment.MiddleLeft, "");
            DetailGrid_Setup.AddColto_Grid(ref  view, 1, "Original Column", "OriginalColumn", 150, 30, 0, false, true, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
            DetailGrid_Setup.AddColto_Grid(ref  view, 2, "Original Column", "OriginalColumn", 150, 30, 0, false, false, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
            DetailGrid_Setup.AddColto_Grid(ref  view, 3, "Rename Column", "RenameColumn", 150, 30, 0, true, false, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
            DetailGrid_Setup.AddColto_GridCombo(ref   view, 80, 4, "", "Alignment", "Alignment", true, false, false, "", "", "", "", "C-Center, L-Left, R-Right", null, 0.0);
            fgDtls1 = (DataGridViewEx)view;
            fgDtls1.ForeColor = Color.Black;
            fgDtls1.Rows.Clear();
            for (int iRow = 0; iRow <= (fgDtls.Rows.Count - 1); iRow++)
            {
                fgDtls1.Rows.Add();
                // for (int iCol = 0; iCol <= (fgDtls.ColumnCount - 1); iCol++)
                {
                    fgDtls1.Rows[iRow].Cells[(int)Col.Col_ID].Value = (fgDtls.Rows[iRow].Cells[(int)Col.Col_ID].Value);
                    fgDtls1.Rows[iRow].Cells[(int)Col.Data_Col].Value = (fgDtls.Rows[iRow].Cells[(int)Col.Data_Col].Value);
                    fgDtls1.Rows[iRow].Cells[(int)Col.Data_rmd].Value = (fgDtls.Rows[iRow].Cells[(int)Col.Data_rmd].Value);
                    fgDtls1.Rows[iRow].Cells[(int)Col.Data_rmdnew].Value = (fgDtls.Rows[iRow].Cells[(int)Col.Data_rmdnew].Value);
                    fgDtls1.Rows[iRow].Cells[(int)Col.Align].Value = (fgDtls.Rows[iRow].Cells[(int)Col.Align].Value);
                }
            }
        }
    }
}
