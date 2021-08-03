using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_DBLayer;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class frmStockAdj : Form
    {
        private int CheckBoxID;
        private Hashtable HasDtls_Link;
        public DateTime AsonDate;
        public double Entity_IsfFtr;
        public object frm;
        public bool IsMultiSelect;
        public bool IsRefQuery = false;
        public int iSnglID;
        public int CompID;
        public string LedgerID;
        public string QueryString;
        public string RefNo;
        public string Other;
        public int MenuID;
        public string ReturnType;
        public DataGridView ref_fgDtls;
        private int iUniqueID;
        public ArrayList UsedInGridArray;
        public List<int> ColIndexArray;
        public List<double> UniqueIDsArray;
        public int ibitCol;
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        public bool IsFirstBitSelected;
        public bool IsStock = true;

        public frmStockAdj()
        {
            InitializeComponent();
            this.ref_fgDtls = new DataGridView();
            this.HasDtls_Link = new Hashtable();
            this.UsedInGridArray = new ArrayList();
            this.UniqueIDsArray = new List<double>();
            this.ColIndexArray = new List<int>();

            this.IsMultiSelect = true;
            this.iSnglID = -1;
            iUniqueID = 0;
        }

        private enum enmCol
        {
            ColIndex,
            LinkID,
            ColDataType,
            IsRequired,
            IsEqual,
            IfNull_Value,
            GridType
        }

        private void frmStockAdj_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsStock)
                {
                    if (Localization.ParseNativeInt(LedgerID) > 0)
                        lblFormCaption.Text = "Select Stock - " + DB.GetSnglValue("SELECT LedgerName from tbl_LedgerMaster WHERE LedgerID=" + LedgerID);
                    else
                        lblFormCaption.Text = "Select Stock";
                }
                else
                {
                    if (Localization.ParseNativeInt(LedgerID) > 0)
                        lblFormCaption.Text = "Select Orders - " + DB.GetSnglValue("SELECT LedgerName from tbl_LedgerMaster WHERE LedgerID=" + LedgerID);
                    else
                        lblFormCaption.Text = "Select Orders";
                }

                string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", MenuID, Entity_IsfFtr));
                int sIsSP = Conversions.ToInteger(DB.GetSnglValue("select Count(0) from sysobjects Where xtype = 'P' And [Name] = '" + snglValue + "'"));

                DataTable dT = DB.GetDT(string.Format(" sp_ExecQuery 'select * from {0} Where GridID = {1} And GridType = {2} Order By ColIndex'", "tbl_GridFields_Mapping", MenuID, Entity_IsfFtr), false);
                string strQry_ColName = CommonCls.GetAdjColName(MenuID, Entity_IsfFtr);

                for (int i = 0; i <= dT.Rows.Count - 1; i++)
                {
                    if (dT.Rows[i]["ColDataType"].ToString() == "B")
                    {
                        ibitCol = i;
                        CheckBoxID = Conversions.ToInteger(dT.Rows[i]["ColIndex"].ToString());
                    }
                }

                if (MenuID == Localization.ParseNativeInt(DB.GetSnglValue("Select menuid from tbl_MenuMaster where FormCall='frmMenuMaster_Comp'")) || MenuID == Localization.ParseNativeInt(DB.GetSnglValue("Select menuid from tbl_MenuMaster where FormCall='frmMenuMaster_User'")))
                {
                    ibitCol = 1;
                }

                if (sIsSP == 0)
                {
                    ApplyQuery(strQry_ColName, snglValue);
                }
                else
                {
                    UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", snglValue), false);
                }
                BandEnumerator enumerator = UGrid_Rpt.DisplayLayout.Bands.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    ColumnEnumerator enumerator2 = enumerator.Current.Columns.GetEnumerator();
                    while (enumerator2.MoveNext())
                    {
                        UltraGridColumn current = enumerator2.Current;
                        DataRow row = dT.Rows[current.Index];
                        HasDtls_Link.Add(Localization.ParseUSInt(row["ColIndex"].ToString()), string.Format("{0};{1};{2};{3};{4}", new object[] { Localization.ParseUSInt(row["ColIndex"].ToString()), row["LinkID"].ToString(), row["ColDataType"].ToString(), row["IsRequired"].ToString(), row["IsEqual"].ToString(), row["IfNull_Value"].ToString() }));
                        if (!Localization.ParseBoolean(row["IsEditable"].ToString()))
                        {
                            current.CellActivation = Activation.NoEdit;
                        }
                        else
                        {
                            current.CellActivation = Activation.AllowEdit;
                        }
                        current.Hidden = Localization.ParseBoolean(row["IsHidden"].ToString());
                    }
                }

                if (UGrid_Rpt.Rows.Count > 0)
                {
                    this.UGrid_Rpt.DisplayLayout.Bands[0].Columns[ibitCol].Header.CheckBoxVisibility = HeaderCheckBoxVisibility.WhenUsingCheckEditor;
                    this.UGrid_Rpt.DisplayLayout.Bands[0].Columns[ibitCol].Header.CheckBoxAlignment = HeaderCheckBoxAlignment.Right;
                    this.UGrid_Rpt.DisplayLayout.Bands[0].Columns[ibitCol].Header.CheckBoxSynchronization = HeaderCheckBoxSynchronization.RowsCollection;
                }

                //this.UGrid_Rpt.AfterHeaderCheckStateChanged += new Infragistics.Win.UltraWinGrid.AfterHeaderCheckStateChangedEventHandler(this.UGrid_Rpt_AfterHeaderCheckStateChanged);
                using (IDataReader iDr = DB.GetRS("SELECT ColIndex FROM tbl_GridFields_Mapping WHERE GridID=" + MenuID + " and GridType=" + Entity_IsfFtr + "  and IsUnique=1"))
                {
                    while (iDr.Read())
                    {
                        ColIndexArray.Add(Localization.ParseNativeInt(iDr["ColIndex"].ToString()));
                    }
                }

                UGrid_Rpt.Focus();
                UGrid_Rpt.Rows.FilterRow.Cells[0].Activate();
                UGrid_Rpt.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
                UGrid_Rpt.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextCell;

                if ((ref_fgDtls != null) && (ref_fgDtls.RowCount != 0))
                {
                    UltraGridBand band2 = UGrid_Rpt.DisplayLayout.Bands[0];
                    for (int j = 0; j <= band2.Columns.Count - 1; j++)
                    {
                        if (Localization.ParseBoolean(HasDtls_Link[j].ToString().Split(new char[] { ';' })[3]))
                        {
                            int iReqCol = j;
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void CmdSelect_Click(object sender, EventArgs e)
        {
            this.UniqueIDsArray = new List<double>();
            int iRowCount = 1;
            ColumnFiltersCollection columnFilters = this.UGrid_Rpt.DisplayLayout.Bands[0].ColumnFilters;
            columnFilters[ibitCol].FilterConditions.Add((FilterComparisionOperator)0, "true");
            UltraGridRow[] filteredRows = UGrid_Rpt.Rows.GetFilteredInNonGroupByRows();

            foreach (var iColIndex in ColIndexArray)
            {
                if (iColIndex > 0)
                {
                    foreach (UltraGridRow row in filteredRows)
                    {
                        if (Localization.ParseBoolean(row.Cells[CheckBoxID].Value.ToString()))
                        {
                            UniqueIDsArray.Add(Localization.ParseNativeDouble(row.Cells[iColIndex].Value.ToString()));
                        }
                    }

                    var result = (from m in UniqueIDsArray select m).Distinct().ToList();
                    if (result.Count > 1)
                    {
                        string sMsg = DB.GetSnglValue("SELECT UniqueMsg FROM tbl_GridFields_Mapping WHERE GridID=" + MenuID + " and GridType= " + Entity_IsfFtr + " and ColIndex=" + iColIndex);
                        columnFilters[ibitCol].FilterConditions.Clear();
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "Info", sMsg);
                        return;
                    }
                }
            }

            try
            {
                if (ref_fgDtls != null)
                {
                    DataTable table;
                    DataTable table2;
                    DataTable table3;
                    bool blnIsAdd = false;
                    if (ref_fgDtls.RowCount == 0)
                    {
                        object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                        table = (DataTable)NewLateBinding.LateGet(objectValue, null, "dt_HasDtls_Grd", new object[0], null, null, null);
                        table2 = (DataTable)NewLateBinding.LateGet(objectValue, null, "dt_AryCalcvalue", new object[0], null, null, null);
                        table3 = (DataTable)NewLateBinding.LateGet(objectValue, null, "dt_AryIsRequired", new object[0], null, null, null);
                        EventHandles.CreateDefault_Rows((DataGridViewEx)ref_fgDtls, table, table2, table3, false, false);
                        NewLateBinding.LateSetComplex(objectValue, null, "dt_AryIsRequired", new object[] { table3 }, null, null, true, false);
                        NewLateBinding.LateSetComplex(objectValue, null, "dt_AryCalcvalue", new object[] { table2 }, null, null, true, false);
                        NewLateBinding.LateSetComplex(objectValue, null, "dt_HasDtls_Grd", new object[] { table }, null, null, true, false);
                    }

                    foreach (UltraGridRow row in filteredRows)
                    {
                        if (iRowCount == 1)
                        {
                            UltraGridRow Row = row;
                            SelectRecords(ref Row);
                            // iRowCount++;
                        }

                        for (int i = 0; i <= row.Cells.Count - 1; i++)
                        {
                            string[] strValues = HasDtls_Link[i].ToString().Split(new char[] { ';' });
                            if (strValues[1] != "-1")
                            {
                                DataGridViewCell cell = ref_fgDtls.Rows[ref_fgDtls.RowCount - 1].Cells[Localization.ParseNativeInt(strValues[1])];
                                switch (strValues[2])
                                {
                                    case "I":
                                        cell.Value = RuntimeHelpers.GetObjectValue(row.Cells[i].Value);
                                        break;

                                    case "D":
                                        if (row.Cells[i].Value.ToString() != "")
                                        {
                                            cell.Value = string.Format("{0:N3}", Localization.ParseNativeDecimal(Conversions.ToString(row.Cells[i].Value)));
                                        }
                                        break;
                                    case "S":
                                        if (row.Cells[i].Value.ToString() != "")
                                        {
                                            cell.Value = Localization.ToVBDateString(Conversions.ToString(row.Cells[i].Value));
                                        }
                                        break;

                                    // Added by Harish 24/12/2015
                                    case "B":
                                        if (row.Cells[i].Value.ToString() != "")
                                        {
                                            cell.Value = Localization.ParseBoolean(Conversions.ToString(row.Cells[i].Value));
                                        }
                                        break;

                                    default:
                                        cell.Value = row.Cells[i].Value.ToString();
                                        break;
                                }
                                blnIsAdd = true;
                                cell = null;
                            }
                            if (Conversions.ToBoolean(strValues[4]))
                            {
                                UsedInGridArray.Add(row.Cells[i].Value.ToString());
                            }
                        }

                        if (blnIsAdd)
                        {
                            blnIsAdd = false;
                            object ObjectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                            if (IsMultiSelect)
                            {
                                table3 = (DataTable)NewLateBinding.LateGet(ObjectValue, null, "Dt_HasDtls_Grd", new object[0], null, null, null);
                                table2 = (DataTable)NewLateBinding.LateGet(ObjectValue, null, "Dt_AryCalcvalue", new object[0], null, null, null);
                                table = (DataTable)NewLateBinding.LateGet(ObjectValue, null, "Dt_AryIsRequired", new object[0], null, null, null);
                                EventHandles.CreateDefault_Rows((DataGridViewEx)ref_fgDtls, table3, table2, table, true, true);
                                NewLateBinding.LateSetComplex(ObjectValue, null, "Dt_AryIsRequired", new object[] { table }, null, null, true, false);
                                NewLateBinding.LateSetComplex(ObjectValue, null, "Dt_AryCalcvalue", new object[] { table2 }, null, null, true, false);
                                NewLateBinding.LateSetComplex(ObjectValue, null, "Dt_HasDtls_Grd", new object[] { table3 }, null, null, true, false);
                            }
                        }
                    }
                }
                else
                {
                    foreach (UltraGridRow row in filteredRows)
                    {
                        UltraGridRow Row = row;
                        SelectRecords(ref Row);
                        break;
                    }
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void UGrid_Rpt_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            // UltraGrid has a built in ui that lets the user display a column chooser dialog.
            // You can enable the ui by enabling the row selectors and setting the RowSelectorHeaderStyle.
            e.Layout.Override.RowSelectors = DefaultableBoolean.True;
            e.Layout.Override.RowSelectorHeaderStyle = RowSelectorHeaderStyle.ColumnChooserButton;
            {
                e.Layout.Override.RowSizing = RowSizing.Free;
                e.Layout.Bands[0].AutoPreviewEnabled = true;
                //e.Layout.Bands[0].Columns[0].TabStop = true;
                if (UGrid_Rpt.Rows.Count > 0)
                {
                    UGrid_Rpt.ActiveCell = UGrid_Rpt.GetRow(0).Cells[0];
                }
                // FILTER ROW FUNCTIONALITY RELATED ULTRAGRID SETTINGS
                // ----------------------------------------------------------------------------------
                // Enable the the filter row user interface by setting the FilterUIType to FilterRow.
                e.Layout.Override.FilterUIType = FilterUIType.FilterRow;

                // FilterEvaluationTrigger specifies when UltraGrid applies the filter criteria typed 
                // into a filter row. Default is OnCellValueChange which will cause the UltraGrid to
                // re-filter the data as soon as the user modifies the value of a filter cell.
                e.Layout.Override.FilterEvaluationTrigger = FilterEvaluationTrigger.OnCellValueChange;

                // By default the UltraGrid selects the type of the filter operand editor based on
                // the column's DataType. For DateTime and boolean columns it uses the column's editors.
                // For other column types it uses the Combo. You can explicitly specify the operand
                // editor style by setting the FilterOperandStyle on the override or the individual
                // columns.
                //e.Layout.Override.FilterOperandStyle = FilterOperandStyle.Combo;

                // By default UltraGrid displays user interface for selecting the filter operator. 
                // You can set the FilterOperatorLocation to hide this user interface. This
                // property is available on column as well so it can be controlled on a per column
                // basis. Default is WithOperand. This property is exposed off the column as well.
                e.Layout.Override.FilterOperatorLocation = FilterOperatorLocation.WithOperand;

                // By default the UltraGrid uses StartsWith as the filter operator. You use
                // the FilterOperatorDefaultValue property to specify a different filter operator
                // to use. This is the default or the initial filter operator value of the cells
                // in filter row. If filter operator user interface is enabled (FilterOperatorLocation
                // is not set to None) then that ui will be initialized to the value of this
                // property. The user can then change the operator as he/she chooses via the operator
                // drop down.
                e.Layout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.StartsWith;

                // FilterOperatorDropDownItems property can be used to control the options provided
                // to the user for selecting the filter operator. By default UltraGrid bases 
                // what operator options to provide on the column's data type. This property is
                // avaibale on the column as well.
                //e.Layout.Override.FilterOperatorDropDownItems = FilterOperatorDropDownItems.All;

                // By default UltraGrid displays a clear button in each cell of the filter row
                // as well as in the row selector of the filter row. When the user clicks this
                // button the associated filter criteria is cleared. You can use the 
                // FilterClearButtonLocation property to control if and where the filter clear
                // buttons are displayed.
                e.Layout.Override.FilterClearButtonLocation = FilterClearButtonLocation.RowAndCell;

                // Appearance of the filter row can be controlled using the FilterRowAppearance proeprty.
                e.Layout.Override.FilterRowAppearance.BackColor = Color.LightYellow;

                // You can use the FilterRowPrompt to display a prompt in the filter row. By default
                // UltraGrid does not display any prompt in the filter row.
                //e.Layout.Override.FilterRowPrompt = "Click here to filter data..."

                // You can use the FilterRowPromptAppearance to change the appearance of the prompt.
                // By default the prompt is transparent and uses the same fore color as the filter row.
                // You can make it non-transparent by setting the appearance' BackColorAlpha property 
                // or by setting the BackColor to a desired value.
                e.Layout.Override.FilterRowPromptAppearance.BackColorAlpha = Alpha.Opaque;

                // By default the prompt is spread across multiple cells if it's bigger than the
                // first cell. You can confine the prompt to a particular cell by setting the
                // SpecialRowPromptField property off the band to the key of a column.
                //e.Layout.Bands(0).SpecialRowPromptField = e.Layout.Bands(0).Columns(0).Key

                // Display a separator between the filter row other rows. SpecialRowSeparator property 
                // can be used to display separators between various 'special' rows, including for the
                // filter row. This property is a flagged enum property so it can take multiple values.
                e.Layout.Override.SpecialRowSeparator = SpecialRowSeparator.FilterRow;

                // You can control the appearance of the separator using the SpecialRowSeparatorAppearance
                // property.
                e.Layout.Override.SpecialRowSeparatorAppearance.BackColor = Color.FromArgb(233, 242, 199);

                // ------------------------------------------------------------------------
                // To allow the user to be able to add/remove summaries set the 
                // AllowRowSummaries property. This does not have to be set to summarize
                // data in code.
                e.Layout.Override.AllowRowSummaries = AllowRowSummaries.True;

                // To display summary footer on the top of the row collections set the 
                // SummaryDisplayArea property to a value that has the Top or TopFixed flag
                // set. TopFixed will make the summary fixed (non-scrolling). Note that 
                // summaries are not fixed in the child rows. TopFixed setting behaves
                // the same way as Top in child rows. Default is resolved to Bottom (and
                // InGroupByRows more about which follows).
                e.Layout.Override.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;

                // By default UltraGrid does not display summary footers or headers of
                // group-by row islands. To display summary footers or headers of group-by row
                // islands set the SummaryDisplayArea to a value that has GroupByRowsFooter
                // flag set.
                e.Layout.Override.SummaryDisplayArea = e.Layout.Override.SummaryDisplayArea | SummaryDisplayAreas.GroupByRowsFooter;

                // If you want to to display summaries of child rows in each group-by row
                // set the SummaryDisplayArea to a value that has SummaryDisplayArea flag
                // set. If SummaryDisplayArea is left to Default then the UltraGrid by
                // default displays summaries in group-by rows.
                e.Layout.Override.SummaryDisplayArea = e.Layout.Override.SummaryDisplayArea | SummaryDisplayAreas.InGroupByRows;

                // By default any summaries to be displayed in the group-by rows are displayed
                // as text appended to the group-by row's description. You can set the 
                // GroupBySummaryDisplayStyle property to SummaryCells or 
                // SummaryCellsAlwaysBelowDescription to display summary values as a separate
                // ui element (cell like element with border, to which the summary value related
                // appearances are applied). Default value of GroupBySummaryDisplayStyle is resolved
                // to Text.
                e.Layout.Override.GroupBySummaryDisplayStyle = GroupBySummaryDisplayStyle.SummaryCells;

                // Appearance of the summary area can be controlled using the 
                // SummaryFooterAppearance. Even though the property's name contains the
                // word 'footer', this appearance applies to summary area that is displayed
                // on top as well (summary headers).
                e.Layout.Override.SummaryFooterAppearance.BackColor = SystemColors.Info;

                // Appearance of summary values can be controlled using the 
                // SummaryValueAppearance property.
                e.Layout.Override.SummaryValueAppearance.BackColor = SystemColors.Window;
                e.Layout.Override.SummaryValueAppearance.FontData.Bold = DefaultableBoolean.True;

                // Appearance of summary values that are displayed inside of group-by rows can 
                // be controlled using the GroupBySummaryValueAppearance property.
                e.Layout.Override.GroupBySummaryValueAppearance.BackColor = SystemColors.Window;
                e.Layout.Override.GroupBySummaryValueAppearance.TextHAlign = HAlign.Right;

                // Caption of the summary area can be set using the SummaryFooterCaption
                // proeprty of the band.
                e.Layout.Bands[0].SummaryFooterCaption = "Grand Totals:";

                // Caption's appearance can be controlled using the SummaryFooterCaptionAppearance
                // property.
                e.Layout.Override.SummaryFooterCaptionAppearance.FontData.Bold = DefaultableBoolean.True;

                // By default summary footer caption is visible. You can hide it using the
                // SummaryFooterCaptionVisible property.
                e.Layout.Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;

                // Display a separator between summary rows and scrolling rows.
                // SpecialRowSeparator property can be used to display separators between
                // various 'special' rows, including filer row, add-row, summary row and
                // fixed rows. This property is a flagged enum property so it can take 
                // multiple values.
                e.Layout.Override.SpecialRowSeparator = SpecialRowSeparator.SummaryRow;

                // Appearance of the separator can be controlled using the 
                // SpecialRowSeparatorAppearance property.
                e.Layout.Override.SpecialRowSeparatorAppearance.BackColor = Color.FromArgb(218, 217, 241);

                // Height of the separator can be controlled as well using the 
                // SpecialRowSeparatorHeight property.
                e.Layout.Override.SpecialRowSeparatorHeight = 6;

                // Border style of the separator can be controlled using the 
                // BorderStyleSpecialRowSeparator property.
                e.Layout.Override.BorderStyleSpecialRowSeparator = UIElementBorderStyle.RaisedSoft;

                e.Layout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
                e.Layout.Override.SelectTypeRow = SelectType.None;
                //e.Layout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.Single

                // ------------------------------------------------------------------------
                // OTHER MISCELLANEOUS ULTRAGRID SETTINGS
                // ------------------------------------------------------------------------
                // Set the view style to SingleBand.
                e.Layout.ViewStyle = ViewStyle.SingleBand;

                // Set the view style band to OutlookGroupBy.
                e.Layout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
                if (UGrid_Rpt.Rows.Count > 0)
                {
                    e.Layout.Rows.FilterRow.Cells[0].Activation = Activation.AllowEdit;
                }
            }
        }

        private void UGrid_Rpt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Space)
                {
                    if ((UGrid_Rpt.Rows.Count != 0) && (UGrid_Rpt.ActiveRow.Index != -1))
                    {
                        int iRptID = UGrid_Rpt.ActiveCell.Column.Index;

                        if (!IsMultiSelect)
                        {
                            for (int i = 0; i <= UGrid_Rpt.Rows.Count - 1; i++)
                            {
                                UGrid_Rpt.Rows[i].Cells[CheckBoxID].Value = false;
                                UGrid_Rpt.Rows[i].Cells[CheckBoxID].Selected = false;
                            }
                        }
                        if (Operators.ConditionalCompareObjectEqual(UGrid_Rpt.ActiveRow.Cells[CheckBoxID].Value, true, false))
                        {
                            UGrid_Rpt.ActiveRow.Cells[CheckBoxID].Selected = false;
                            UGrid_Rpt.ActiveRow.Cells[CheckBoxID].Value = false;
                        }
                        else
                        {
                            UGrid_Rpt.ActiveRow.Cells[CheckBoxID].Selected = true;
                            UGrid_Rpt.ActiveRow.Cells[CheckBoxID].Value = true;
                        }

                    }
                }
                else if (e.Alt == true && e.KeyCode == Keys.S)
                {
                    CmdSelect_Click(null, null);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    UGrid_Rpt.PerformAction(UltraGridAction.NextCell);
                    UGrid_Rpt.PerformAction(UltraGridAction.EnterEditMode);
                }
                else if (e.KeyCode == Keys.Return)
                {
                    UGrid_Rpt.PerformAction(UltraGridAction.NextCell);
                    UGrid_Rpt.PerformAction(UltraGridAction.EnterEditMode);
                }
                else if ((((e.KeyCode == Keys.Right) || (e.KeyCode == Keys.Left)) || (e.KeyCode == Keys.Down)) || (e.KeyCode == Keys.Up))
                {
                    UGrid_Rpt.PerformAction(UltraGridAction.EnterEditMode);
                }
                else if (e.KeyCode == Keys.Shift && e.KeyCode == Keys.Tab)
                {
                    UGrid_Rpt.PerformAction(UltraGridAction.PrevCell);
                    UGrid_Rpt.PerformAction(UltraGridAction.EnterEditMode);
                }
                else if (e.KeyCode == Keys.PageUp)
                {
                    UGrid_Rpt.PerformAction(UltraGridAction.PageUpCell);
                }
                else if (e.KeyCode == Keys.PageDown)
                {
                    UGrid_Rpt.PerformAction(UltraGridAction.PageDownCell);
                }
                else
                {
                    UGrid_Rpt.PerformAction(UltraGridAction.EnterEditMode);
                }
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
        }

        private void ApplyQuery(string strQry_ColName, string strQuery)
        {
            try
            {
                Interaction.IIf(AsonDate.ToString() == null, DateTime.Compare(AsonDate, DateAndTime.Now.Date) == 0, null);
                switch (MenuID)
                {
                    #region 12
                    case 12:
                        Console.WriteLine(string.Format(" sp_ExecQuery 'Select {0} From {1} (''{2}'', {3}, {4}, {5}) '", new object[] { strQry_ColName, strQuery, Localization.ToSqlDateString(Conversions.ToString(AsonDate)), Db_Detials.CompID, Db_Detials.YearID, LedgerID }));
                        UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery 'Select {0} From {1} (''{2}'', {3}, {4}, {5}) '", new object[] { strQry_ColName, strQuery, Localization.ToSqlDateString(Conversions.ToString(AsonDate)), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                        break;
                    #endregion

                    #region 53
                    case 53://Quick Access Menu --Santosh
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format("Select Distinct ROW_NUMBER() OVER (ORDER BY MenuID) As [Sr. No.],  CAST('False' as Bit) As [Sel], MenuID,ParentID,OrderBy,Menu_Caption,Form_Caption,ToolTip,TblName_Main,PmryColumn,SearchQry,SearchQry_Dtls,FormCall,FormCall_Web,FormType,MenuType,IsForm,IsSeparator,ApplyYear,IsVisible From fn_MenuMaster_Comp() Where MenuID not in ({0}) And IsSeparator = 0 and CompanyID=" + Db_Detials.CompID + " and UserType=" + Other + " Order by MenuID ", new object[] { strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format("Select Distinct ROW_NUMBER() OVER (ORDER BY MenuID) As [Sr. No.],  CAST('False' as Bit) As [Sel],  MenuID,ParentID,OrderBy,Menu_Caption,Form_Caption,ToolTip,TblName_Main,PmryColumn,SearchQry,SearchQry_Dtls,FormCall,FormCall_Web,FormType,MenuType,IsForm,IsSeparator,ApplyYear,IsVisible From tbl_MenuMaster Where IsSeparator = 0 and MenuID NOT IN (Select MenuID from fn_MenuMaster_User() where IsSeparator = 0 and CompanyID=" + CompID + " and UserType=" + Other + " )"), false);
                            }
                            break;
                        }
                    #endregion

                    #region 116
                    case 116:
                        UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4})", new object[] { strQry_ColName, strQuery, LedgerID, Db_Detials.CompID, Db_Detials.YearID }), false);
                        break;
                    #endregion

                    #region 118
                    case 118:
                        UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4})", new object[] { strQry_ColName, strQuery, LedgerID, Db_Detials.CompID, Db_Detials.YearID }), false);
                        break;
                    #endregion

                    #region 139
                    case 139:
                        UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5})", new object[] { strQry_ColName, strQuery, DB.SQuote(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                        break;


                    #endregion

                    #region 146
                    case 146:
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int m = 0; m <= UsedInGridArray.Count - 1; m++)
                                {
                                    string str = Conversions.ToString(UsedInGridArray[m]);
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[m]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) Where BatchNo Not In ({6}) ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5})", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                            }
                            break;
                        }

                    #endregion

                    #region 156
                    case 156://Fabric Checking
                        {
                            string strarray = " ";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }

                            //if (CIS_Textil.frmFabricChecking.VoucherType == 350)
                            //{
                            //    if (UsedInGridArray.Count > 0)
                            //    {
                            //        UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) where  PieceNo in (Select PieceNo from tbl_FabricProductionDtls2 where CheckStat='False') and PieceNo not in({6})", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID, strarray }), false);
                            //    }
                            //    else
                            //    {
                            //        UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) where PieceNo in (Select PieceNo from tbl_FabricProductionDtls2 where CheckStat='False')", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                            //    }
                            //}
                            //else if (CIS_Textil.frmFabricChecking.VoucherType == 160)
                            //{
                            //    if (UsedInGridArray.Count > 0)
                            //    {
                            //        UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) where  PieceNo in (Select PieceNo from {7} where CheckState='False') and PieceNo not in({6})", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID, strarray, "tbl_FabricReceiptDtls" }), false);
                            //    }
                            //    else
                            //    {
                            //        UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) where PieceNo in (Select PieceNo from {6} where CheckState='False')", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID, "tbl_FabricReceiptDtls" }), false);
                            //    }
                            //}
                            //else if (CIS_Textil.frmFabricChecking.VoucherType == 151)
                            //{
                            //    if (UsedInGridArray.Count > 0)
                            //    {
                            //        UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) where  PieceNo in (Select PieceNo from tbl_FabricOpeningDtls where CheckState='False') and PieceNo not in({6})", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID, strarray }), false);
                            //    }
                            //    else
                            //    {
                            //        UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) where PieceNo in (Select PieceNo from tbl_FabricOpeningDtls where CheckState='False')", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                            //    }
                            //}
                            break;
                        }
                    #endregion

                    #region 158
                    case 158://Fabric Process Order
                        UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2},{3},{4}) Where TransID not in(Select FabricIssueID From {5})", new object[] { strQry_ColName, strQuery, LedgerID, Db_Detials.CompID, Db_Detials.YearID, "tbl_FabricProcessOrderDtls" }), false);
                        break;
                    #endregion

                    #region 159
                    case 159://Fabric Process Entry
                        UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4})", new object[] { strQry_ColName, strQuery, LedgerID, Db_Detials.CompID, Db_Detials.YearID }), false);
                        break;
                    #endregion

                    #region 164
                    case 164://Fabric Packing Return
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}) where Piece No not in({5})", new object[] { strQry_ColName, strQuery, LedgerID, Db_Detials.CompID, Db_Detials.YearID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4})", new object[] { strQry_ColName, strQuery, LedgerID, Db_Detials.CompID, Db_Detials.YearID }), false);
                            }
                            break;
                        }
                    #endregion

                    #region 170
                    case 170://Performa Invoice, Transport Receipt
                        UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3})", new object[] { strQry_ColName, strQuery, Db_Detials.CompID, Db_Detials.YearID }), false);
                        break;
                    #endregion

                    #region 171
                    case 171://Fabric Invoice
                        //UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3})", new object[] { strQry_ColName, strQuery, Db_Detials.CompID, Db_Detials.YearID }), false);
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) where PieceNo not in ({6}) and BalMeters > 0 Order by MyId ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) Where BalMeters > 0 Order by MyId ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                            }
                            break;
                        }
                    #endregion

                    #region 532
                    case 532://Fabric Invoice
                        //UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3})", new object[] { strQry_ColName, strQuery, Db_Detials.CompID, Db_Detials.YearID }), false);
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4},{5}) where PieceNo not in ({6}) Order by MyId ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4},{5}) Order by MyId ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                            }
                            break;
                        }
                    #endregion


                    #region 328
                    case 328://bOOK iNVOICE
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}) where CatalogID not in ({5}) and Bal_Pcs > 0 Order by CatalogID ", new object[] { strQry_ColName, strQuery, LedgerID, Db_Detials.CompID, Db_Detials.YearID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2},{3},{4}) Where  Bal_Pcs > 0 ", new object[] { strQry_ColName, strQuery, LedgerID, Db_Detials.CompID, Db_Detials.YearID }), false);
                            }
                            break;
                        }
                    #endregion

                    #region 345
                    case 345://Pending Lots
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3})", new object[] { strQry_ColName, strQuery, Db_Detials.CompID, Db_Detials.YearID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2},{3})", new object[] { strQry_ColName, strQuery, Db_Detials.CompID, Db_Detials.YearID }), false);
                            }
                            break;
                        }
                    #endregion

                    #region 377
                    case 377://Yarn Process Order
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4})  where strcon not in ({5}) ", new object[] { strQry_ColName, strQuery, LedgerID, Db_Detials.CompID, Db_Detials.YearID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ('{2}', {3}, {4}) ", new object[] { strQry_ColName, strQuery, LedgerID, Db_Detials.CompID, Db_Detials.YearID }), false);
                            }
                            break;
                        }
                    #endregion

                    #region 400
                    case 400:
                        {
                            UGrid_Rpt.DataSource = DB.GetDT(String.Format(" Select {0} From {1} ({4}, {2}, {3})", strQry_ColName, strQuery, Db_Detials.CompID, Db_Detials.YearID, LedgerID), false);
                            break;
                        }
                    #endregion

                    #region 418
                    case 418://Daily Work Inward
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    string str = Conversions.ToString(UsedInGridArray[i]);
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} () Where SubDailyWrkInwrdID Not In ({2})", strQry_ColName, strQuery, strarray), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ()", new object[] { strQry_ColName, strQuery }), false);
                            }

                            break;
                        }
                    #endregion

                    #region 428
                    case 428://Cutting Entry
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4})  where SrNo not in({5}) ", new object[] { strQry_ColName, strQuery, LedgerID, Db_Detials.CompID, Db_Detials.YearID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}) ", new object[] { strQry_ColName, strQuery, LedgerID, Db_Detials.CompID, Db_Detials.YearID }), false);
                            }
                            break;
                        }
                    #endregion

                    #region 431
                    case 431://Book Issues
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4})  where SrNo not in({5}) ", new object[] { strQry_ColName, strQuery, LedgerID, Db_Detials.CompID, Db_Detials.YearID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}) ", new object[] { strQry_ColName, strQuery, LedgerID, Db_Detials.CompID, Db_Detials.YearID }), false);
                            }
                            break;
                        }
                    #endregion

                    #region 456
                    case 456://Beam delivery Challan
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int j = 0; j <= UsedInGridArray.Count - 1; j++)
                                {
                                    string str = Conversions.ToString(UsedInGridArray[j]);
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[j]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) Where MyID not in ({6})", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5})", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                            }
                            break;
                        }
                    #endregion

                    #region 457
                    case 457://Fabric Invoice
                        UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3})", new object[] { strQry_ColName, strQuery, Db_Detials.CompID, Db_Detials.YearID }), false);
                        break;
                    #endregion

                    #region 426
                    case 426://Fabric Invoice
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}) where LedgerID not in ({4}) and Bal_Mtrs > 0 Order by FabricDesignID ", new object[] { strQry_ColName, strQuery, Db_Detials.CompID, Db_Detials.YearID, LedgerID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}) Where LedgerID in ({4}) and Bal_Mtrs > 0 Order by FabricDesignID ", new object[] { strQry_ColName, strQuery, Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                            }
                            break;
                        }

                    #endregion

                    #region 472
                    case 472://fabric DeliveryChallan JBF 
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) where PieceNo not in ({6}) and BalMeters > 0 Order by MyId ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) Where BalMeters > 0 Order by MyId ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                            }
                            break;
                        }

                    #endregion

                    #region 473
                    case 473://Fabric Invoice Serial
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) where PieceNo not in ({6}) and BalMeters > 0 Order by MyId ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) Where BalMeters > 0 Order by MyId ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                            }
                            break;
                        }
                    #endregion

                    //#region 489
                    //case 489://Embroadary Fabric Receipt Embroidary
                    //    {
                    //        string strarray = "";
                    //        if (UsedInGridArray.Count > 0)
                    //        {
                    //            for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                    //            {
                    //                strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                    //            }
                    //            strarray = strarray.Remove(strarray.Length - 1);
                    //        }
                    //        if (UsedInGridArray.Count > 0)
                    //        {
                    //            UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}) Where MyId not in({4}) and LotNo <> '-' ", new object[] { strQry_ColName, strQuery, LedgerID, Db_Detials.CompID, strarray }), false);
                    //        }
                    //        else
                    //        {
                    //            UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}) Where  LotNo <> '-' ", new object[] { strQry_ColName, strQuery, LedgerID, Db_Detials.CompID }), false);
                    //        }
                    //        break;
                    //    }
                    //#endregion

                    //#region 488
                    //case 488://Embroidary Fabric Issue Embroidary
                    //    UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4})", new object[] { strQry_ColName, strQuery, LedgerID, Db_Detials.CompID, Db_Detials.YearID }), false);
                    //    break;
                    //#endregion

                    //#region 490
                    //case 490://Fabric Deleivery Challan Embroidary
                    //    {
                    //        string strarray = "";
                    //        if (UsedInGridArray.Count > 0)
                    //        {
                    //            for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                    //            {
                    //                strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                    //            }
                    //            strarray = strarray.Remove(strarray.Length - 1);
                    //        }
                    //        if (UsedInGridArray.Count > 0)
                    //        {
                    //            UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) where PieceNo not in ({6}) and BalMeters > 0 Order by MyId ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID, strarray }), false);
                    //        }
                    //        else
                    //        {
                    //            UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) Where BalMeters > 0 Order by MyId ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                    //        }
                    //        break;
                    //    }
                    //#endregion

                    #region 504
                    case 504://Fabric Deleivery Challan Embroidary
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) where PieceNo not in ({6}) and BalMeters > 0 Order by SalesID ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) Where BalMeters > 0 Order by SalesID ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                            }
                            break;
                        }
                    #endregion

                    #region 511
                    case 511://Fabric Invoice Serial
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) where PieceNo not in ({6}) and BalMeters > 0 Order by MyId ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) Where BalMeters > 0 Order by MyId ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                            }
                            break;
                        }
                    #endregion

                    #region 517
                    case 517:
                        UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5})", new object[] { strQry_ColName, strQuery, DB.SQuote(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                        break;
                    #endregion

                    #region 521
                    case 521://Fabric Invoice Roll Return
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) where PieceNo not in ({6}) and BalMeters > 0 Order by SalesID ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) Where BalMeters > 0 Order by SalesID ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                            }
                            break;
                        }
                    #endregion

                    #region 522
                    case 522://Fabric Invoice Roll Return
                        {
                            //string strarray = "";
                            //if (UsedInGridArray.Count > 0)
                            //{
                            //    for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                            //    {
                            //        strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                            //    }
                            //    strarray = strarray.Remove(strarray.Length - 1);
                            //}
                            //if (UsedInGridArray.Count > 0)
                            //{
                            //    UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) where PieceNo not in ({6}) and BalQty > 0 Order by SalesID ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID, strarray }), false);
                            //}
                            //else
                            //{
                            UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) Where BalQty > 0 Order by SalesID ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                            // }
                            break;
                        }
                    #endregion

                    #region 536
                    case 536://Menu Master Company
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format("Select Distinct ROW_NUMBER() OVER (ORDER BY MenuID) As [Sr. No.],  CAST('False' as Bit) As [Sel], MenuID,ParentID,OrderBy,Menu_Caption,Form_Caption,ToolTip,TblName_Main,PmryColumn,SearchQry,SearchQry_Dtls,FormCall,FormCall_Web,FormType,MenuType,IsForm,IsSeparator,ApplyYear,IsVisible From tbl_MenuMaster Where MenuID not in ({0}) And IsSeparator = 0 Order by MenuID ", new object[] { strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format("Select Distinct ROW_NUMBER() OVER (ORDER BY MenuID) As [Sr. No.],  CAST('False' as Bit) As [Sel],  MenuID,ParentID,OrderBy,Menu_Caption,Form_Caption,ToolTip,TblName_Main,PmryColumn,SearchQry,SearchQry_Dtls,FormCall,FormCall_Web,FormType,MenuType,IsForm,IsSeparator,ApplyYear,IsVisible From tbl_MenuMaster Where IsSeparator = 0 and MenuID NOT IN(Select MenuID from fn_MenuMaster_Comp() Where CompanyID=" + CompID + ")"), false);
                            }
                            break;
                        }
                    #endregion

                    #region 549
                    case 549://Fabric Invoice Return
                        //UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3})", new object[] { strQry_ColName, strQuery, Db_Detials.CompID, Db_Detials.YearID }), false);
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4},{5}) where PieceNo not in ({6}) ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4},{5}) ", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                            }
                            break;
                        }
                    #endregion

                    #region 574
                    case 574://Fabric Merging Serial
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) where MyID not in ({6})", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5})", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                            }
                            break;
                        }
                    #endregion

                    #region 575
                    case 575://Fabric Transfer Serial
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) where MyID not in ({6})", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5})", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                            }
                            break;
                        }
                    #endregion

                    #region 637
                    case 637://Fabric Transfer
                        {
                            string strarray = "";
                            if (UsedInGridArray.Count > 0)
                            {
                                for (int i = 0; i <= UsedInGridArray.Count - 1; i++)
                                {
                                    strarray = Conversions.ToString(Operators.AddObject(strarray, Operators.AddObject(Operators.AddObject("'", UsedInGridArray[i]), "',")));
                                }
                                strarray = strarray.Remove(strarray.Length - 1);
                            }
                            if (UsedInGridArray.Count > 0)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) where MyID not in ({6})", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID, strarray }), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5})", new object[] { strQry_ColName, strQuery, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(AsonDate))), Db_Detials.CompID, Db_Detials.YearID, LedgerID }), false);
                            }
                            break;
                        }
                    #endregion

                    default:
                        {
                            if (IsRefQuery)
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format("{0}", QueryString), false);
                            }
                            else
                            {
                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery 'Select * From {0}'", strQuery), false);
                            }
                        }
                        break;
                }

                //try
                //{
                //    UGrid_Rpt.ActiveCell = UGrid_Rpt.GetRow(0).Cells[0];
                //}
                //catch { }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        private void SelectRecords(ref UltraGridRow row)
        {
            try
            {
                int menuID = MenuID;
                switch (menuID)
                {
                    //case 121:// Yarn D.o.Entry
                    //    frmYarnDOEntry YarnDoEntry = (frmYarnDOEntry)Navigate.GetForm_byName("frmYarnDOEntry");
                    //    YarnDoEntry.SetSupplier = row.Cells[14].Value.ToString();
                    //    break;

                    //case 123://Yarn Inward 
                    //    frmYarnInward inward2 = (frmYarnInward)Navigate.GetForm_byName("frmYarnInward");
                    //    if (Entity_IsfFtr == 0)
                    //    {
                    //        inward2.SetSupplire = row.Cells[5].Value.ToString();
                    //        inward2.SetDepartment = row.Cells[20].Value.ToString();
                    //        inward2.SetLRNo = row.Cells[18].Value.ToString();
                    //        inward2.SetLrDate = Localization.ToVBDateString(row.Cells[19].Value.ToString());
                    //        inward2 = null;
                    //    }
                    //    else if (Entity_IsfFtr == 1)
                    //    {
                    //        inward2.SetSupplire = row.Cells[14].Value.ToString();
                    //    }
                    //    break;

                    //case 304://Yarn Purchase
                    //    frmYarnPurchase purchase2 = (frmYarnPurchase)Navigate.GetForm_byName("frmYarnPurchase");
                    //    if (Entity_IsfFtr == 0)
                    //    {
                    //        purchase2.SetRefNo = row.Cells[2].Value.ToString();
                    //        purchase2.SetChallanDate = Localization.ToVBDateString(row.Cells[3].Value.ToString());
                    //        purchase2.SetSupplire = row.Cells[4].Value.ToString();
                    //        purchase2.SetTransport = row.Cells[8].Value.ToString();
                    //        purchase2 = null;
                    //    }
                    //    else if (Entity_IsfFtr == 1)
                    //    {
                    //        purchase2.SetSupplire = row.Cells[14].Value.ToString();
                    //    }
                    //    break;

                    //case 142://Beam loom loading
                    //    frmBeamLoomLoading loading2 = (frmBeamLoomLoading)Navigate.GetForm_byName("frmBeamLoomLoading");
                    //    loading2.SetBeamNo = row.Cells[3].Value.ToString();
                    //    loading2 = null;
                    //    break;

                    //case 143://Beam loom unloading
                    //    frmBeamUnloadOrComplete complete2 = (frmBeamUnloadOrComplete)Navigate.GetForm_byName("frmBeamUnloadOrComplete");
                    //    complete2.SetLoomID = row.Cells[0].Value.ToString();
                    //    complete2.SetBeamNo = row.Cells[6].Value.ToString();
                    //    complete2.SetDesignID = row.Cells[7].Value.ToString();
                    //    complete2.SetCuts = row.Cells[3].Value.ToString();
                    //    complete2.SetMtrs = row.Cells[4].Value.ToString();
                    //    complete2.SetWt = row.Cells[5].Value.ToString();
                    //    complete2.setRefID = row.Cells[11].Value.ToString();
                    //    complete2 = null;
                    //    break;


                    //case 152:// Fabric Inward
                    //    frmFabricInward FabInward = (frmFabricInward)Navigate.GetForm_byName("frmFabricInward");
                    //    FabInward.SetSupplier = row.Cells[14].Value.ToString();
                    //    break;

                    //case 665:// Item Inward
                    //    frmItemPurchaseInward ItemPurchaseInward = (frmItemPurchaseInward)Navigate.GetForm_byName("frmItemPurchaseInward");
                    //    ItemPurchaseInward.SetSupplier = row.Cells[9].Value.ToString();
                    //    break;

                    //case 153:// Fabric Purchase
                    //    frmFabricPurchase invoice3 = (frmFabricPurchase)Navigate.GetForm_byName("frmFabricPurchase");
                    //    if (Entity_IsfFtr == 0)
                    //    {
                    //        invoice3.SetSupplier = row.Cells[4].Value.ToString();
                    //        invoice3.setBroker = row.Cells[8].Value.ToString();
                    //        invoice3.setTransport = row.Cells[9].Value.ToString();
                    //        invoice3.setLrNo = row.Cells[10].Value.ToString();
                    //        invoice3.setLrDate = Localization.ToVBDateString(row.Cells[11].Value.ToString());
                    //    }
                    //    else
                    //    {
                    //        invoice3.SetSupplier = row.Cells[14].Value.ToString();
                    //    }
                    //    break;

                    //case 667:// Item Purchase
                    //    frmItemPurchase itemPurchase = (frmItemPurchase)Navigate.GetForm_byName("frmItemPurchase");
                    //    if (Entity_IsfFtr == 0)
                    //    {
                    //        itemPurchase.SetSupplier = row.Cells[5].Value.ToString();
                    //        itemPurchase.setBroker = row.Cells[9].Value.ToString();
                    //        itemPurchase.setTransport = row.Cells[10].Value.ToString();
                    //        itemPurchase.setLrNo = row.Cells[11].Value.ToString();
                    //        itemPurchase.setLrDate = Localization.ToVBDateString(row.Cells[12].Value.ToString());
                    //    }
                    //    else
                    //    {
                    //        itemPurchase.SetSupplier = row.Cells[10].Value.ToString();
                    //    }
                    //    break;

                    //case 170: //Performa Invoice
                    //    frmPerformaInvoice frmPerfomaInvoice = (frmPerformaInvoice)Navigate.GetForm_byName("frmPerformaInvoice");
                    //    frmPerfomaInvoice.SetChallanID = row.Cells[0].Value.ToString();
                    //    frmPerfomaInvoice.SetRefNo = row.Cells[2].Value.ToString();
                    //    frmPerfomaInvoice.setChallanDate = Localization.ToVBDateString(row.Cells[3].Value.ToString());
                    //    frmPerfomaInvoice.SetPartyID = row.Cells[4].Value.ToString();
                    //    frmPerfomaInvoice.SetDeliveryAt = row.Cells[7].Value.ToString();
                    //    frmPerfomaInvoice.SetLrNo = row.Cells[8].Value.ToString();
                    //    frmPerfomaInvoice.SetLrDate = row.Cells[9].Value.ToString();
                    //    frmPerfomaInvoice.SetTransport = row.Cells[10].Value.ToString();
                    //    break;

                    //case 171://Fabric Invoice
                    //    frmFabricInvoice invoice4 = (frmFabricInvoice)Navigate.GetForm_byName("frmFabricInvoice");
                    //    invoice4.setOrderDate = row.Cells[16].Value.ToString();
                    //    invoice4.setParty = row.Cells[10].Value.ToString();
                    //    invoice4.SetBroker = row.Cells[12].Value.ToString();
                    //    invoice4.SetHaste = row.Cells[21].Value.ToString();
                    //    invoice4.setTransport = row.Cells[17].Value.ToString();
                    //    invoice4.setLrNo = row.Cells[19].Value.ToString();
                    //    invoice4.setLrDate = row.Cells[20].Value.ToString();
                    //    invoice4 = null;
                    //    break;

                    //case 352://Yarn Sales Return 
                    //    frmYarnSalesReturn frmySReturn = (frmYarnSalesReturn)Navigate.GetForm_byName("frmYarnSalesReturn");
                    //    frmySReturn.setParty = row.Cells[7].Value.ToString();
                    //    frmySReturn = null;
                    //    break;

                    //case 428://Cutting Entry
                    //    frmCuttingEntry receipt3 = (frmCuttingEntry)Navigate.GetForm_byName("frmCuttingEntry");
                    //    receipt3.SetChlnNo = row.Cells[4].Value.ToString();
                    //    receipt3.SetTakaNo = row.Cells[5].Value.ToString();
                    //    receipt3.setTakaMtrs = row.Cells[11].Value.ToString();
                    //    receipt3.setTakaUnit = row.Cells[18].Value.ToString();
                    //    receipt3.SetIssueId = row.Cells[0].Value.ToString();
                    //    receipt3.SetSubIssueId = row.Cells[3].Value.ToString();
                    //    receipt3.IssuePcs = Conversions.ToDouble(row.Cells[10].Value.ToString());
                    //    break;

                    //case 522://Book Invoice Return
                    //    frmCatalogInvoiceReturn bookinvoiceRetrun = (frmCatalogInvoiceReturn)Navigate.GetForm_byName("frmCatalogInvoiceReturn");
                    //    bookinvoiceRetrun.setParty = row.Cells[5].Value.ToString();
                    //    bookinvoiceRetrun.SetBillNo = row.Cells[4].Value.ToString();
                    //    bookinvoiceRetrun = null;
                    //    break;

                    //case 668:// Item Sales Invoice
                    //    frmItemSalesInvoice invoice = (frmItemSalesInvoice)Navigate.GetForm_byName("frmItemSalesInvoice");
                    //    invoice.setOrderDate = row.Cells[15].Value.ToString();
                    //    invoice.setParty = row.Cells[9].Value.ToString();
                    //    invoice.SetBroker = row.Cells[11].Value.ToString();
                    //    invoice.SetHaste = row.Cells[20].Value.ToString();
                    //    invoice.setTransport = row.Cells[16].Value.ToString();
                    //    invoice.setLrNo = row.Cells[18].Value.ToString();
                    //    invoice.setLrDate = row.Cells[19].Value.ToString();
                    //    invoice = null;
                    //    break;

                    //case 521://Fabric Invoice Return Roll
                    //    frmFabricInvoiceReturnRoll FabricinvoiceReturn_Roll = (frmFabricInvoiceReturnRoll)Navigate.GetForm_byName("frmFabricInvoiceReturnRoll");
                    //    FabricinvoiceReturn_Roll.SetParty = row.Cells[5].Value.ToString();
                    //    FabricinvoiceReturn_Roll.SetBillNo = row.Cells[4].Value.ToString();
                    //    FabricinvoiceReturn_Roll = null;
                    //    break;

                    //case 504://Fabric Invoice Return Serial
                    //    frmFabricInvoiceReturnSerial FabricinvoiceReturn_Serial = (frmFabricInvoiceReturnSerial)Navigate.GetForm_byName("frmFabricInvoiceReturnSerial");
                    //    FabricinvoiceReturn_Serial.SetBillNo = row.Cells[4].Value.ToString();
                    //    FabricinvoiceReturn_Serial = null;
                    //    break;

                    //case 168://Fabric Delivery Challan Serial
                    //    frmFabricDeliveryChallan_Series FabricDeliveryChln_Serial = (frmFabricDeliveryChallan_Series)Navigate.GetForm_byName("frmFabricDeliveryChallan_Series ");
                    //    FabricDeliveryChln_Serial = null;
                    //    break;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        private void UGrid_Rpt_MouseClick(object sender, MouseEventArgs e)
        {
            if ((UGrid_Rpt.Rows.Count != 0) && (UGrid_Rpt.ActiveRow.Index != -1))
            {
                if (UGrid_Rpt.ActiveCell != null)
                {
                    if (UGrid_Rpt.ActiveCell.Column.Index == CheckBoxID)
                    {
                        if (!IsMultiSelect)
                        {
                            for (int i = 0; i <= UGrid_Rpt.Rows.Count - 1; i++)
                            {
                                UGrid_Rpt.Rows[i].Cells[CheckBoxID].Value = false;
                                UGrid_Rpt.Rows[i].Cells[CheckBoxID].Selected = false;
                            }
                        }

                        if (Operators.ConditionalCompareObjectEqual(UGrid_Rpt.ActiveRow.Cells[CheckBoxID].Value, true, false))
                        {
                            UGrid_Rpt.ActiveRow.Cells[CheckBoxID].Selected = false;
                            UGrid_Rpt.ActiveRow.Cells[CheckBoxID].Value = false;
                        }
                        else
                        {
                            UGrid_Rpt.ActiveRow.Cells[CheckBoxID].Selected = true;
                            UGrid_Rpt.ActiveRow.Cells[CheckBoxID].Value = true;
                        }
                    }
                    else
                    {
                        UGrid_Rpt.PerformAction(UltraGridAction.EnterEditMode);
                    }
                }
            }

        }

        private void pnlCaptionBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(base.Handle, 0xa1, 2, 0);
            }
        }

        private void UGrid_Rpt_ClickCell(object sender, ClickCellEventArgs e)
        {
            //if ((UGrid_Rpt.Rows.Count != 0) && (UGrid_Rpt.ActiveRow.Index != -1))
            //{
            //    if (Localization.ParseBoolean(UGrid_Rpt.ActiveRow.Cells[CheckBoxID].Value.ToString()))
            //    {
            //        foreach (int iColIndex in ColIndexArray)
            //        {
            //            if (iColIndex > 0)
            //            {
            //                if (iUniqueID > 0)
            //                {
            //                    if (Localization.ParseNativeInt(UGrid_Rpt.ActiveRow.Cells[iColIndex].Value.ToString()) == iUniqueID)
            //                    {
            //                        UGrid_Rpt.ActiveRow.Cells[CheckBoxID].Selected = true;
            //                    }
            //                    else
            //                    {
            //                        UGrid_Rpt.ActiveRow.Cells[CheckBoxID].Selected = false;
            //                        string sMsg = DB.GetSnglValue("SELECT UniqueMsg FROM tbl_GridFields_Mapping WHERE GridID=" + MenuID + " and ColIndex=" + iColIndex);
            //                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "Info", sMsg);
            //                    }
            //                }
            //                else
            //                {
            //                    iUniqueID = Localization.ParseNativeInt(UGrid_Rpt.ActiveRow.Cells[iColIndex].Value.ToString());
            //                }
            //            }
            //        }

            //    }
            //}
        }

        private void frmStockAdj_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CIS_Utilities.CIS_Dialog.Show("Do you want to close this screen ?", GetAssemblyInfo.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
