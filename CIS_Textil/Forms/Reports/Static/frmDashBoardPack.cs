using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Crocus_Bussiness;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Crocus_Core
{
    public partial class frmDashBoardPack : Form
    {
        public frmDashBoardPack()
        {
            InitializeComponent();
        }

        public void frmDashBoardPack_Load(object sender, EventArgs e)
        {
            try
            {
                string snglValue = DB.GetSnglValue("SELECT Yr_From FROM tbl_YearMaster WHERE YrId = " + Conversions.ToString(Db_Detials.YrID));
                string sql = ((((((string.Empty + string.Format("Select YrDesc From tbl_YearMaster Where YrID = {0};", Db_Detials.YrID)) + string.Format("select * from [fn_CashBankBal]({0},{1});", Db_Detials.YrID, Db_Detials.CompID) + string.Format("select * from [fn_OutStanding_DashBoard]({0},{1});", Db_Detials.YrID, Db_Detials.CompID)) + string.Format("select * from [fn_SalePurchase_DashBoard]({0},{1});", Db_Detials.YrID, Db_Detials.CompID) + string.Format("select * from [fn_DayBook_Dashboard]({1},{0});", Db_Detials.YrID, Db_Detials.CompID)) + string.Format("Select top 5 Particulars,ClosingBal from fn_OutStandingCustomer_Summary({0},{1},{2},{3})  order by ClosingBal desc;", new object[] { CommonLogic.SQuote(Localization.ToSqlDateString(snglValue)), CommonLogic.SQuote(Localization.ToSqlDateString(DateTime.Now.Date.ToString())), Db_Detials.CompID, Db_Detials.YrID }) + string.Format("Select top 5 Particulars,ClosingBal from fn_OutStandingSupplier_Summary({0},{1},{2},{3})  order by ClosingBal desc;", new object[] { CommonLogic.SQuote(Localization.ToSqlDateString(snglValue)), CommonLogic.SQuote(Localization.ToSqlDateString(DateTime.Now.Date.ToString())), Db_Detials.CompID, Db_Detials.YrID })) + string.Format("select * from [Fn_FetchPackingStkDashboard]({0},{1});", Db_Detials.CompID, Db_Detials.YrID) + string.Format("select * from [fn_LedgerReportMonthWise](82,{0},{1});", Db_Detials.CompID, Db_Detials.YrID)) + string.Format("select * from [fn_LedgerReportMonthWise](87,{0},{1});", Db_Detials.CompID, Db_Detials.YrID) + string.Format("select * from [fn_FetchFabricStock_All]({0},{1},{2});", CommonLogic.SQuote(Localization.ToSqlDateString(DateTime.Now.Date.ToString())), Db_Detials.CompID, Db_Detials.YrID)) + string.Format("select * from [fn_FetchTopFabricSales]({0},{1},{2});", CommonLogic.SQuote(Localization.ToSqlDateString(DateTime.Now.Date.ToString())), Db_Detials.CompID, Db_Detials.YrID) + string.Format("select * from [fn_FetchTopFabricPurch]({0},{1},{2});", CommonLogic.SQuote(Localization.ToSqlDateString(DateTime.Now.Date.ToString())), Db_Detials.CompID, Db_Detials.YrID);
                this.lblFYearID.Text = "";
                using (DataSet set = DB.GetDS(sql, false))
                {
                    object pnlCashBank;
                    DataGridView view2;
                    using (IDataReader reader = set.Tables[0].CreateDataReader())
                    {
                        if (reader.Read())
                        {
                            this.lblFYearID.Text = reader["YrDesc"].ToString();
                        }
                    }
                    using (IDataReader reader2 = set.Tables[1].CreateDataReader())
                    {
                        DataGridView grdCashBank = this.grdCashBank;
                        pnlCashBank = this.pnlCashBank;
                        view2 = this.grdCashBank;
                        Navigate.SetPropertydtlGrid(pnlCashBank, view2, DockStyle.Fill);
                        this.grdCashBank = view2;
                        this.pnlCashBank = (Panel)pnlCashBank;
                        view2 = this.grdCashBank;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 0, "Particulars", 110, 60, 0, true, true, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdCashBank = view2;
                        view2 = this.grdCashBank;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 1, "Value", 0xa7, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdCashBank = view2;
                        grdCashBank.Rows.Clear();
                        while (reader2.Read())
                        {
                            grdCashBank.Rows.Add();
                            grdCashBank.Rows[grdCashBank.Rows.Count - 1].Cells[0].Value = reader2["Particulars"].ToString();
                            grdCashBank.Rows[grdCashBank.Rows.Count - 1].Cells[1].Value = RuntimeHelpers.GetObjectValue(Interaction.IIf(Localization.ParseNativeDouble(reader2["Value"].ToString()) > 0.0, reader2["Value"].ToString() + " Dr", (Conversions.ToString(Math.Abs(Localization.ParseNativeDecimal(reader2["Value"].ToString()))) + " Cr").ToString()));
                        }
                        grdCashBank = null;
                    }
                    using (IDataReader reader3 = set.Tables[2].CreateDataReader())
                    {
                        DataGridView grdOS = this.grdOS;
                        pnlCashBank = this.pnlOS;
                        view2 = this.grdOS;
                        Navigate.SetPropertydtlGrid(pnlCashBank, view2, DockStyle.Fill);
                        this.grdOS = view2;
                        this.pnlOS = (Panel)pnlCashBank;
                        view2 = this.grdOS;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 0, "Particulars", 110, 60, 0, true, true, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdOS = view2;
                        view2 = this.grdOS;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 1, "Value", 0xa7, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdOS = view2;
                        grdOS.Rows.Clear();
                        while (reader3.Read())
                        {
                            grdOS.Rows.Add();
                            grdOS.Rows[grdOS.Rows.Count - 1].Cells[0].Value = reader3["Particulars"].ToString();
                            grdOS.Rows[grdOS.Rows.Count - 1].Cells[1].Value = RuntimeHelpers.GetObjectValue(Interaction.IIf(Localization.ParseNativeDouble(reader3["Value"].ToString()) > 0.0, reader3["Value"].ToString() + " Dr", (Conversions.ToString(Math.Abs(Localization.ParseNativeDecimal(reader3["Value"].ToString()))) + " Cr").ToString()));
                        }
                        grdOS = null;
                    }
                    using (IDataReader reader4 = set.Tables[3].CreateDataReader())
                    {
                        DataGridView grdSP = this.grdSP;
                        pnlCashBank = this.pnlSP;
                        view2 = this.grdSP;
                        Navigate.SetPropertydtlGrid(pnlCashBank, view2, DockStyle.Fill);
                        this.grdSP = view2;
                        this.pnlSP = (Panel)pnlCashBank;
                        view2 = this.grdSP;
                        DetailGrid_Setup.AddColto_Grid(ref  view2, 0, "Particulars", 110, 60, 0, true, true, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdSP = view2;
                        view2 = this.grdSP;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 1, "Value", 0xa7, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdSP = view2;
                        grdSP.Rows.Clear();
                        while (reader4.Read())
                        {
                            grdSP.Rows.Add();
                            grdSP.Rows[grdSP.Rows.Count - 1].Cells[0].Value = reader4["Particulars"].ToString();
                            grdSP.Rows[grdSP.Rows.Count - 1].Cells[1].Value = RuntimeHelpers.GetObjectValue(Interaction.IIf(Localization.ParseNativeDouble(reader4["Value"].ToString()) > 0.0, reader4["Value"].ToString() + " Dr", (Conversions.ToString(Math.Abs(Localization.ParseNativeDecimal(reader4["Value"].ToString()))) + " Cr").ToString()));
                        }
                        grdSP = null;
                    }
                    using (IDataReader reader5 = set.Tables[4].CreateDataReader())
                    {
                        DataGridView grdDayBook = this.grdDayBook;
                        pnlCashBank = this.pnlDayBook;
                        view2 = this.grdDayBook;
                        Navigate.SetPropertydtlGrid(pnlCashBank, view2, DockStyle.Fill);
                        this.grdDayBook = view2;
                        this.pnlDayBook = (Panel)pnlCashBank;
                        view2 = this.grdDayBook;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 0, "Particulars", 120, 60, 0, true, true, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdDayBook = view2;
                        view2 = this.grdDayBook;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 1, "Debit", 80, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdDayBook = view2;
                        view2 = this.grdDayBook;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 2, "Credit", 80, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdDayBook = view2;
                        grdDayBook.Rows.Clear();
                        while (reader5.Read())
                        {
                            grdDayBook.Rows.Add();
                            grdDayBook.Rows[grdDayBook.Rows.Count - 1].Cells[0].Value = reader5["LedgerName"].ToString();
                            grdDayBook.Rows[grdDayBook.Rows.Count - 1].Cells[1].Value = Localization.ParseNativeDecimal(reader5["Dr_Amt"].ToString());
                            grdDayBook.Rows[grdDayBook.Rows.Count - 1].Cells[2].Value = Localization.ParseNativeDecimal(reader5["Cr_Amt"].ToString());
                        }
                        grdDayBook = null;
                    }
                    using (IDataReader reader6 = set.Tables[5].CreateDataReader())
                    {
                        DataGridView grdTopCust = this.grdTopCust;
                        pnlCashBank = this.pnlTopCust;
                        view2 = this.grdTopCust;
                        Navigate.SetPropertydtlGrid(pnlCashBank, view2, DockStyle.Fill);
                        this.grdTopCust = view2;
                        this.pnlTopCust = (Panel)pnlCashBank;
                        view2 = this.grdTopCust;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 0, "Particulars", 150, 60, 0, true, true, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdTopCust = view2;
                        view2 = this.grdTopCust;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 1, "ClosingBal", 90, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdTopCust = view2;
                        grdTopCust.Rows.Clear();
                        while (reader6.Read())
                        {
                            grdTopCust.Rows.Add();
                            grdTopCust.Rows[grdTopCust.Rows.Count - 1].Cells[0].Value = reader6["Particulars"].ToString();
                            grdTopCust.Rows[grdTopCust.Rows.Count - 1].Cells[1].Value = Localization.ParseNativeDecimal(reader6["ClosingBal"].ToString());
                        }
                        grdTopCust = null;
                    }
                    using (IDataReader reader7 = set.Tables[6].CreateDataReader())
                    {
                        DataGridView grdTopSupplier = this.grdTopSupplier;
                        pnlCashBank = this.pnlTopSuplier;
                        view2 = this.grdTopSupplier;
                        Navigate.SetPropertydtlGrid(pnlCashBank, view2, DockStyle.Fill);
                        this.grdTopSupplier = view2;
                        this.pnlTopSuplier = (Panel)pnlCashBank;
                        view2 = this.grdTopSupplier;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 0, "Particulars", 150, 60, 0, true, true, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdTopSupplier = view2;
                        view2 = this.grdTopSupplier;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 1, "Closing Bal", 90, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdTopSupplier = view2;
                        grdTopSupplier.Rows.Clear();
                        while (reader7.Read())
                        {
                            grdTopSupplier.Rows.Add();
                            grdTopSupplier.Rows[grdTopSupplier.Rows.Count - 1].Cells[0].Value = reader7["Particulars"].ToString();
                            grdTopSupplier.Rows[grdTopSupplier.Rows.Count - 1].Cells[1].Value = Localization.ParseNativeDecimal(reader7["ClosingBal"].ToString());
                        }
                        grdTopSupplier = null;
                    }
                    using (IDataReader reader8 = set.Tables[7].CreateDataReader())
                    {
                        DataGridView grdPackStk = this.grdPackStk;
                        pnlCashBank = this.pnlPackStoke;
                        view2 = this.grdPackStk;
                        Navigate.SetPropertydtlGrid(pnlCashBank, view2, DockStyle.Fill);
                        this.grdPackStk = view2;
                        this.pnlPackStoke = (Panel)pnlCashBank;
                        view2 = this.grdPackStk;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 0, "Packing Name", 160, 60, 0, true, true, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdPackStk = view2;
                        view2 = this.grdPackStk;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 1, "Fabric Quality", 180, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdPackStk = view2;
                        view2 = this.grdPackStk;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 2, "Bal Qty", 180, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdPackStk = view2;
                        view2 = this.grdPackStk;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 3, "Bal Mtrs", 120, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdPackStk = view2;
                        grdPackStk.Rows.Clear();
                        while (reader8.Read())
                        {
                            grdPackStk.Rows.Add();
                            grdPackStk.Rows[grdPackStk.Rows.Count - 1].Cells[0].Value = reader8["Packing Name"].ToString();
                            grdPackStk.Rows[grdPackStk.Rows.Count - 1].Cells[1].Value = reader8["FabricQualityName"].ToString();
                            grdPackStk.Rows[grdPackStk.Rows.Count - 1].Cells[2].Value = Localization.ParseNativeDecimal(reader8["Bal_Qty"].ToString());
                            grdPackStk.Rows[grdPackStk.Rows.Count - 1].Cells[3].Value = Localization.ParseNativeDecimal(reader8["Bal_Mtrs"].ToString());
                        }
                        grdPackStk = null;
                    }
                    using (IDataReader reader9 = set.Tables[8].CreateDataReader())
                    {
                        DataGridView grdMonthPur = this.grdMonthPur;
                        pnlCashBank = this.pnlMnthPurchase;
                        view2 = this.grdMonthPur;
                        Navigate.SetPropertydtlGrid(pnlCashBank, view2, DockStyle.Fill);
                        this.grdMonthPur = view2;
                        this.pnlMnthPurchase = (Panel)pnlCashBank;
                        view2 = this.grdMonthPur;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 0, "Month Name", 180, 60, 0, true, true, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdMonthPur = view2;
                        view2 = this.grdMonthPur;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 1, "Debit", 0x91, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdMonthPur = view2;
                        view2 = this.grdMonthPur;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 2, "Credit", 0x91, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdMonthPur = view2;
                        grdMonthPur.Rows.Clear();
                        while (reader9.Read())
                        {
                            grdMonthPur.Rows.Add();
                            grdMonthPur.Rows[grdMonthPur.Rows.Count - 1].Cells[0].Value = reader9["Particulars"].ToString();
                            grdMonthPur.Rows[grdMonthPur.Rows.Count - 1].Cells[1].Value = reader9["Dr_Amt"].ToString();
                            grdMonthPur.Rows[grdMonthPur.Rows.Count - 1].Cells[2].Value = reader9["Cr_Amt"].ToString();
                        }
                        grdMonthPur = null;
                    }
                    using (IDataReader reader10 = set.Tables[9].CreateDataReader())
                    {
                        DataGridView grdMnthlySale = this.grdMnthlySale;
                        pnlCashBank = this.pnlMnthSale;
                        view2 = this.grdMnthlySale;
                        Navigate.SetPropertydtlGrid(pnlCashBank, view2, DockStyle.Fill);
                        this.grdMnthlySale = view2;
                        this.pnlMnthSale = (Panel)pnlCashBank;
                        view2 = this.grdMnthlySale;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 0, "Month Name", 180, 60, 0, true, true, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdMnthlySale = view2;
                        view2 = this.grdMnthlySale;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 1, "Debit", 0x91, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdMnthlySale = view2;
                        view2 = this.grdMnthlySale;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 2, "Credit", 0x91, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdMnthlySale = view2;
                        grdMnthlySale.Rows.Clear();
                        while (reader10.Read())
                        {
                            grdMnthlySale.Rows.Add();
                            grdMnthlySale.Rows[grdMnthlySale.Rows.Count - 1].Cells[0].Value = reader10["Particulars"].ToString();
                            grdMnthlySale.Rows[grdMnthlySale.Rows.Count - 1].Cells[1].Value = reader10["Dr_Amt"].ToString();
                            grdMnthlySale.Rows[grdMnthlySale.Rows.Count - 1].Cells[2].Value = reader10["Cr_Amt"].ToString();
                        }
                        grdMnthlySale = null;
                    }
                    using (IDataReader reader11 = set.Tables[10].CreateDataReader())
                    {
                        DataGridView grdFabricStk = this.grdFabricStk;
                        pnlCashBank = this.pnlFabStk;
                        view2 = this.grdFabricStk;
                        Navigate.SetPropertydtlGrid(pnlCashBank, view2, DockStyle.Fill);
                        this.grdFabricStk = view2;
                        this.pnlFabStk = (Panel)pnlCashBank;
                        view2 = this.grdFabricStk;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 0, "FabricName", 160, 60, 0, true, true, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdFabricStk = view2;
                        view2 = this.grdFabricStk;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 1, "LedgerName", 150, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdFabricStk = view2;
                        view2 = this.grdFabricStk;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 2, "BalanceQty", 110, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdFabricStk = view2;
                        view2 = this.grdFabricStk;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 3, "BalanceMtrs", 110, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdFabricStk = view2;
                        grdFabricStk.Rows.Clear();
                        while (reader11.Read())
                        {
                            grdFabricStk.Rows.Add();
                            grdFabricStk.Rows[grdFabricStk.Rows.Count - 1].Cells[0].Value = reader11["FabricQualityName"].ToString();
                            grdFabricStk.Rows[grdFabricStk.Rows.Count - 1].Cells[1].Value = reader11["LedgerName"].ToString();
                            grdFabricStk.Rows[grdFabricStk.Rows.Count - 1].Cells[2].Value = reader11["BalQty"].ToString();
                            grdFabricStk.Rows[grdFabricStk.Rows.Count - 1].Cells[3].Value = reader11["BalMtrs"].ToString();
                        }
                        grdFabricStk = null;
                    }
                    using (IDataReader reader12 = set.Tables[11].CreateDataReader())
                    {
                        DataGridView grdTopFebSale = this.grdTopFebSale;
                        pnlCashBank = this.pnlTopFebSale;
                        view2 = this.grdTopFebSale;
                        Navigate.SetPropertydtlGrid(pnlCashBank, view2, DockStyle.Fill);
                        this.grdTopFebSale = view2;
                        this.pnlTopFebSale = (Panel)pnlCashBank;
                        view2 = this.grdTopFebSale;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 0, "FabricName", 160, 60, 0, true, true, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdTopFebSale = view2;
                        view2 = this.grdTopFebSale;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 1, "BalanceQty", 110, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdTopFebSale = view2;
                        view2 = this.grdTopFebSale;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 2, "BalanceMtrs", 110, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdTopFebSale = view2;
                        grdTopFebSale.Rows.Clear();
                        while (reader12.Read())
                        {
                            grdTopFebSale.Rows.Add();
                            grdTopFebSale.Rows[grdTopFebSale.Rows.Count - 1].Cells[0].Value = reader12["FabricQualityName"].ToString();
                            grdTopFebSale.Rows[grdTopFebSale.Rows.Count - 1].Cells[1].Value = reader12["BalQty"].ToString();
                            grdTopFebSale.Rows[grdTopFebSale.Rows.Count - 1].Cells[2].Value = reader12["BalMtrs"].ToString();
                        }
                        grdTopFebSale = null;
                    }
                    using (IDataReader reader13 = set.Tables[11].CreateDataReader())
                    {
                        DataGridView grdTopFabPur = this.grdTopFabPur;
                        pnlCashBank = this.pnlTopFabPur;
                        view2 = this.grdTopFabPur;
                        Navigate.SetPropertydtlGrid(pnlCashBank, view2, DockStyle.Fill);
                        this.grdTopFabPur = view2;
                        this.pnlTopFabPur = (Panel)pnlCashBank;
                        view2 = this.grdTopFabPur;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 0, "FabricName", 160, 60, 0, true, true, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdTopFabPur = view2;
                        view2 = this.grdTopFabPur;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 1, "BalanceQty", 110, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdTopFabPur = view2;
                        view2 = this.grdTopFabPur;
                        DetailGrid_Setup.AddColto_Grid(ref   view2, 2, "BalanceMtrs", 110, 60, 2, true, true, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleLeft, "");
                        this.grdTopFabPur = view2;
                        grdTopFabPur.Rows.Clear();
                        while (reader13.Read())
                        {
                            grdTopFabPur.Rows.Add();
                            grdTopFabPur.Rows[grdTopFabPur.Rows.Count - 1].Cells[0].Value = reader13["FabricQualityName"].ToString();
                            grdTopFabPur.Rows[grdTopFabPur.Rows.Count - 1].Cells[1].Value = reader13["BalQty"].ToString();
                            grdTopFabPur.Rows[grdTopFabPur.Rows.Count - 1].Cells[2].Value = reader13["BalMtrs"].ToString();
                        }
                        grdTopFabPur = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.frmDashBoardPack_Load(null, null);
        }

        private void frmDashBoardPack_Load_1(object sender, EventArgs e)
        {

        }
    }
}
