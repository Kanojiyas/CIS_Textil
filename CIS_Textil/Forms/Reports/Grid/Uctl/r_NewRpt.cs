using Infragistics.Win;
using Infragistics.Win.Misc;
using Infragistics.Win.UltraWinCalcManager;
using Infragistics.Win.UltraWinCalcManager.FormulaBuilder;
using Infragistics.Win.UltraWinEditors;
using Infragistics.Win.UltraWinGrid;
using System.Data;
using System;
using System.Collections;
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
    public partial class r_NewRpt : UserControl
    {
        public static ComboBox cboRptlst;
        public static DataGridViewEx fgDtls;
        public static int iIDentity = 0;
        public static bool IsNewRpt;
        public static string sReportName = string.Empty;
        public static UltraGrid UGrid_Rpt;

        public r_NewRpt()
        {
            InitializeComponent();
        }

        private enum ReportLst : int
        {
            ReportID,
            QueryID,
            QueryName,
            IsNewRpt
        }

        private enum Col : int
        {
            Col_ID,
            Data_Col,
            Data_rmd,
            Data_rmdnew,
            Align
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                string strQryDtls = " Insert Into " + Db_Detials.tbl_ReportConfigDtls + " (ReportID, Qry_ColName, Rpt_ColName, Col_Order, Col_Width, IsShow, IsGroup, Group_lvl, IsAvg, IsSum, IsMax, IsMin, IsCount, Filter_Type, Filter_Text, AlignType) Values((#CodeID#), {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14});" + Environment.NewLine;
                string strInsertMain = string.Empty;
                string[] strSelect = cboRptlst.SelectedValue.ToString().Split(';');
                int iCond = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(Reportname)  From tbl_ReportConfigMain Where Reportname ='" + txtNewRpt.Text + "'")));
                if (iCond == 0)
                {
                    {
                        if (IsNewRpt)
                        {
                            strInsertMain = string.Format(" Insert Into {0} (QueryID, ReportName, Font_Name, Font_Size, UserID, CompanyID) Values({1}, {2}, {3}, {4}, {5}, {6});" + Environment.NewLine,
                                Db_Detials.tbl_ReportConfigMain, strSelect[1],
                                txtNewRpt.TextFormat(true), DB.SQuote(UGrid_Rpt.Font.FontFamily.Name), UGrid_Rpt.Font.Size, Db_Detials.UserID, Db_Detials.CompID);
                        }
                        else
                        {
                            strInsertMain = string.Format(" Insert Into {0} (QueryID, ReportName, Font_Name, Font_Size, UserID, CompanyID) Values({1}, {2}, {3}, {4}, {5}, {6});" + Environment.NewLine,
                                Db_Detials.tbl_ReportConfigMain, strSelect[1], txtNewRpt.TextFormat(true),
                                DB.SQuote(UGrid_Rpt.Font.FontFamily.Name), UGrid_Rpt.Font.Size, Db_Detials.UserID, Db_Detials.CompID);
                        }
                    }
                    //else
                    //{
                    //    strInsertMain = string.Format(" Update {0} Set QueryID = {1}, ReportName = {2}, Font_Name = {3}, Font_Size = {4} Where ReportID = {5} and UserID={6} and CompanyID={7};" + Environment.NewLine,
                    //        Db_Detials.tbl_ReportConfigMain, strSelect[1],
                    //        DB.SQuote(cboRptlst.Text), DB.SQuote(UGrid_Rpt.Font.FontFamily.Name), UGrid_Rpt.Font.Size,
                    //        strSelect[0], Db_Detials.UserID, Db_Detials.CompID);
                    //}
                }
                else
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "Report Already Available With This Name.");
                    return;
                }

                string strInsertQry = string.Empty;
                int i = 0;
                foreach (UltraGridBand band in UGrid_Rpt.DisplayLayout.Bands)
                {
                    foreach (UltraGridColumn column in band.Columns)
                    {
                        string[] strSumry = new string[5];

                        ColumnFiltersCollection columnFilters = UGrid_Rpt.DisplayLayout.Bands[0].ColumnFilters;

                        foreach (SummarySettings oSummary in column.Band.Summaries)
                        {
                            if (object.ReferenceEquals(oSummary.SourceColumn, column))
                            {
                                strSumry[Localization.ParseNativeInt(oSummary.SummaryType.ToString())] = "'True'";
                                //IsAvg, IsSum, IsMin, IsMax, IsCount
                            }
                        }

                        for (int iSumry = 0; iSumry <= 4; iSumry++)
                        {
                            if (strSumry[iSumry] == null)
                                strSumry[iSumry] = "'False'";
                        }

                        string strCmprValue = string.Empty;
                        string strCmprOptr = string.Empty;

                        try
                        {
                            strCmprValue = DB.SQuote(columnFilters[column.Index].FilterConditions[0].CompareValue.ToString());
                            strCmprOptr = Localization.ParseNativeInt(columnFilters[column.Index].FilterConditions[0].ComparisionOperator.ToString()).ToString();

                        }
                        catch
                        {
                            strCmprValue = "NULL";
                            strCmprOptr = "NULL";
                        }

                        //Console.Write(column.Header.Caption & " :: " & column.SortIndicator & " :: " & column.IsGroupByColumn & Environment.NewLine)

                        strInsertQry += string.Format(strQryDtls, DB.SQuote(Conversions.ToString(fgDtls.Rows[i].Cells[(int)Col.Data_Col].Value)),
                                        //old// DB.SQuote(fgDtls.Rows[i].Cells[Localization.ParseNativeInt(Col.Data_Col.ToString())].Value.ToString()),
                                        //DB.SQuote(column.Header.Caption), 
                                        DB.SQuote(Conversions.ToString(fgDtls.Rows[i].Cells[(int)Col.Data_Col].Value)),
                                        column.Index, column.Width,
                                        DB.SQuote(Conversions.ToString(column.Hidden)), DB.SQuote(column.IsGroupByColumn.ToString()),
                                        (column.IsGroupByColumn ? column.Level : -1), strSumry[0], strSumry[1],
                                        strSumry[2], strSumry[3], strSumry[4], strCmprOptr, strCmprValue,
                                        DB.SQuote(Conversions.ToString(fgDtls.Rows[i].Cells[4].Value)));
                        i += 1;
                    }
                }

                if (strInsertQry.Length != 0)
                {
                    strInsertQry = string.Format("Delete From {0} Where ReportID = (#CodeID#); ", Db_Detials.tbl_ReportConfigDtls) + strInsertQry;
                    if (IsNewRpt || strSelect[Localization.ParseNativeInt(ReportLst.ReportID.ToString())] == "-1")
                    {
                        DB.ExecuteTranscation(strInsertMain, "-1", IsNewRpt, strInsertQry);
                    }
                    else
                    {
                        DB.ExecuteTranscation(strInsertMain, strSelect[Localization.ParseNativeInt(ReportLst.ReportID.ToString())].ToString(), IsNewRpt, strInsertQry);
                    }
                }

                if (IsNewRpt || strSelect[Localization.ParseNativeInt(ReportLst.ReportID.ToString())] == "-1")
                {
                    string sReportName = cboRptlst.Text;
                    {
                        cboRptlst.DataSource = DB.GetDT(" sp_ShowReportList '" + iIDentity + "' ", false);
                        cboRptlst.DisplayMember = "ReportName";
                        cboRptlst.ValueMember = "ReportID";
                        cboRptlst.SelectedIndex = cboRptlst.Items.Count - 1;
                        cboRptlst.Text = "";
                        cboRptlst.SelectedText = txtNewRpt.Text;
                    }
                }
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "Report configuration saved successfully.");

            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", ex.Message);
            }
        }

        private void r_NewRpt_Load(object sender, EventArgs e)
        {
            this.txtNewRpt.Focus();
        }
    }
}
