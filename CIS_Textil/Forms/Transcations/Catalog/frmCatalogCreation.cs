using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmCatalogCreation : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;
        public DataGridViewEx fgDtls_A;
        public DataGridViewEx fgDtls_A_footer;
        public DataGridViewEx fgDtls_f;
        public DataGridViewEx fgDtls_f_footer;

        ArrayList OrgInGridArray = new ArrayList();
        string strLedgerWastage = string.Empty;
        string strunit = "";

        public frmCatalogCreation()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
            fgDtls_A = GrdAddLess.fgDtls;
            fgDtls_A_footer = GrdAddLess.fgDtls_f;
            fgDtls_f = GrdFooter.fgDtls;
            fgDtls_f_footer = GrdFooter.fgDtls_f;
        }

        private void frmFabricInward_Load(object sender, EventArgs e)
        {
            try
            {
                strunit = DB.GetSnglValue("Select UnitID From fn_UnitsMaster_Tbl() Where UnitName='Pcs'");
                string sQry = "";
                strLedgerWastage = DB.GetSnglValue("Select LedgerID from fn_LedgerMaster_Tbl() Where Ledgername like '%Fabric Wastage%'");
                if (strLedgerWastage == "")
                {
                    string slGID = DB.GetSnglValue("SELECT LedgerGroupID from fn_LedgerGroupMaster_Tbl() WHERE LedgerGroupName='Suspense A/c'");

                    sQry += string.Format("INSERT INTO tbl_LedgerMaster(LedgerName,AliasName,LedgerGroupId,MBM,CompID,YearID,AddedOn,AddedBy,IsModified,IsDeleted,IsCanclled,IsApproved,IsAudited) VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12});",
                              "'Fabric Wastage'", "NULL", slGID, 0, Db_Detials.CompID, Db_Detials.YearID, "GETDATE()", Db_Detials.UserID, 0, 0, 0, 0, 0
                      );
                    DB.ExecuteSQL(sQry);
                    strLedgerWastage = DB.GetSnglValue("Select LedgerID from fn_LedgerMaster_Tbl() Where LedgerName='Fabric Wastage'");
                }

                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboDeptFrom, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboProcessor, Combobox_Setup.ComboType.Mst_Dyer, "");
                Combobox_Setup.FillCbo(ref cboCatalogName, Combobox_Setup.ComboType.Mst_Catalog, "");
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls_A, fgDtls_A_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 1, true);
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls_f, fgDtls_f_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 2, true);

                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls_A.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_A_CellValueChanged);
                this.fgDtls_A.CellEndEdit += new DataGridViewCellEventHandler(this.fgDtls_A_CellEndEdit);

                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    FillControls();
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #region Navigation

        public void MovetoField()
        {
            try
            {
                {
                    txtCode.Text = "";
                    CommonCls.IncFieldID(this, ref txtEntryNo, "");
                    this.txtRefNo.Text = (CommonCls.AutoInc(this, "RefNo", "CatalogCreationID", ""));
                    int MaxId = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(CatalogCreationID),0) From {0}  Where StoreID = {1} and CompID = {2} and BranchID = {3} and YearID = {4}", "tbl_CatalogCreationMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID))));
                    if (MaxId > 0)
                    {
                        using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where CatalogCreationID={1} and StoreID={2} and CompID={3} and BranchID={4} and YearID={5}", "fn_CatalogCreationMain_Tbl()", MaxId, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID)))
                        {
                            while (reader.Read())
                            {
                                dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                                cboDeptFrom.SelectedValue = reader["DepartmentID"].ToString();
                                cboProcessor.SelectedValue = reader["ProcessorID"].ToString();
                                cboCatalogName.SelectedValue = reader["CatalogID"].ToString();
                                txtNoOfCatalogs.Text = reader["NoofCatalogs"].ToString();
                            }
                        }
                    }

                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                    EventHandles.CreateDefault_Rows(fgDtls_f, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                    EventHandles.CalculateFooter_Rows(fgDtls_f, fgDtls_f_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                    EventHandles.CreateDefault_Rows(fgDtls_A, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                    EventHandles.CalculateFooter_Rows(fgDtls_A, fgDtls_A_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                    
                    if (((fgDtls.RowCount > 0) & (fgDtls.ColumnCount > 0)) & fgDtls.Columns[2].Visible)
                    {
                        fgDtls.Rows[0].Cells[2].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select  {0}({1},{2},{3},{4},{5},{6})", "dbo.fn_FetchBarcodeNo", MaxId, base.iIDentity, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID)), Db_Detials.PCS_NO_INCMT);
                    }
                    else
                    {
                        fgDtls.Rows[0].Cells[2].Value = "-";
                    }
                    dtEntryDate.Focus();
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "CatalogCreationID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtRefNo, "RefNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtRefdate, "RefDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboDeptFrom, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtRate, "Rate", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboProcessor, "ProcessorID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboCatalogName, "CatalogID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtNoOfCatalogs, "NoofCatalogs", Enum_Define.ValidationType.Text);

                DetailGrid_Setup.FillGrid(fgDtls, fgDtls.Grid_UID, fgDtls.Grid_Tbl, "CatalogCreationID", txtCode.Text, base.dt_HasDtls_Grd);
                DetailGrid_Setup.FillGrid(fgDtls_A, fgDtls_A.Grid_UID, fgDtls_A.Grid_Tbl, "CatalogCreationID", txtCode.Text, base.dt_HasDtls_Grd);
                DetailGrid_Setup.FillGrid(fgDtls_f, fgDtls_f.Grid_UID, fgDtls_f.Grid_Tbl, "CatalogCreationID", txtCode.Text, base.dt_HasDtls_Grd);
                
                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public void SaveRecord()
        {
            try
            {
                ArrayList pArrayData = new ArrayList
                {
                ("(#ENTRYNO#)"),
                (dtEntryDate.TextFormat(false, true)),
                (txtRefNo.Text),
                (dtRefdate.TextFormat(false, true)),
                (cboDeptFrom.SelectedValue),
                (cboProcessor.SelectedValue),
                (cboCatalogName.SelectedValue),
                (txtNoOfCatalogs.Text),
                Localization.ParseNativeDecimal(txtRate.Text.ToString()),
                (string.Format("{0:N}", CommonCls.GetColSum(this.fgDtls, 8, -1, -1)).Replace(",", "")),
                (string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 9, -1, -1)).Replace(",", "")),
                Localization.ParseNativeDecimal(TxtGrossAmount.Text.ToString()),
                Localization.ParseNativeDecimal(txtAddLessAmt.Text.ToString()),
                Localization.ParseNativeDecimal(txtNetAmt.Text.ToString())
                };
                string strAdjQry = string.Empty;
                string strAddless = string.Empty;
                strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_AcLedger", "(#CodeID#)", base.iIDentity);
                strAdjQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", "(#CodeID#)", base.iIDentity);
                strAdjQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockCatalogLedger", "(#CodeID#)", base.iIDentity);
                strAdjQry += string.Format("Delete From {0} Where  CatalogCreationID= {1};", "tbl_CatalogCreationDtls", "(#CodeID#)");
                strAdjQry += string.Format("Delete From {0} Where  CatalogCreationID= {1};", "tbl_CatalogCreationAddless", "(#CodeID#)");

                DataGridViewEx ex = this.fgDtls_A;
                double dblDedAmt = 0.0;
                for (int i = 0; i <= ex.RowCount - 1; i++)
                {
                    DataGridViewRow row = ex.Rows[i];
                    if (row.Cells[2].Value != null)
                    {
                        if (Localization.ParseNativeDouble(row.Cells[2].Value.ToString()) > 0)
                        {
                            if (Operators.ConditionalCompareObjectEqual(row.Cells[3].FormattedValue, "-", false))
                            {
                                dblDedAmt = -Localization.ParseNativeDouble(row.Cells[5].Value.ToString());
                            }
                            else if (Operators.ConditionalCompareObjectEqual(row.Cells[3].FormattedValue, "+", false))
                            {
                                dblDedAmt = Localization.ParseNativeDouble(row.Cells[5].Value.ToString());
                            }
                            strAdjQry += DBSp.InsertInto_AcLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                        row.Cells[2].Value.ToString(), 2, Db_Detials.Ac_AdjType.OnAccount, "(#CodeID#)", txtRefNo.Text.Trim(), dtRefdate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                        Localization.ParseNativeDecimal(dblDedAmt.ToString()), 0, "null", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                            strAdjQry += string.Format("Insert Into tbl_CatalogCreationAddless Values({0},{1},{2},{3},{4},{5});", "(#CodeID#)", (i + 1).ToString(), Localization.ParseNativeInt(row.Cells[2].Value.ToString()), Localization.ParseNativeInt(row.Cells[3].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[4].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[5].Value.ToString()));
                        }
                    }
                    row = null;
                }
                ex = null;


                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    if (row.Cells[8].Value != null)
                    {
                        if (Localization.ParseNativeInt(row.Cells[8].Value.ToString()) >= 0)
                        {
                            string BatchNo = "-";
                            string BarcodeNo;
                            if (row.Cells[2].Value != null && row.Cells[2].Value.ToString().Length > 0)
                            {
                                BarcodeNo = row.Cells[2].Value.ToString();
                            }
                            else
                            {
                                BarcodeNo = "-";
                            }

                            string OpDt = Localization.ToVBDateString(DB.GetSnglValue(string.Format("select Yr_From from tbl_YearMaster where YearID={0}", Db_Detials.YearID)));
                            strAdjQry += string.Format("Insert into tbl_CatalogCreationDtls(CatalogCreationID,SubCatalogCreationID,FabricID,FabricDesignID,FabricQualityID,FabricShadeID,UnitID,Mtrs,Wastage,Bal_Mtrs,ProductionOrdID,EI1,EI2,EI3,ED1,ED2,ET1,ET2,ET3,EN1,EN2) values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20});" + Environment.NewLine,
                                        "(#CodeID#)", (i + 1).ToString(), row.Cells[2].Value, 
                                        Localization.ParseNativeInt(row.Cells[3].Value.ToString()),
                                        Localization.ParseNativeInt(row.Cells[4].Value.ToString()), 
                                        Localization.ParseNativeInt(row.Cells[5].Value.ToString()),
                                        Localization.ParseNativeInt(row.Cells[6].Value.ToString()), 
                                        Localization.ParseNativeInt(row.Cells[7].Value.ToString()),
                                        Localization.ParseNativeDecimal(row.Cells[8].Value.ToString()), 
                                        Localization.ParseNativeDecimal(row.Cells[9].Value.ToString()),
                                        Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()),
                                        row.Cells[11].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[11].Value.ToString()),
                                        row.Cells[12].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[12].Value.ToString()),
                                        row.Cells[13].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                                        row.Cells[14].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[14].Value.ToString()),
                                        row.Cells[15].Value == null || row.Cells[15].Value.ToString() == "" || row.Cells[15].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[15].Value.ToString()),
                                        row.Cells[16].Value == null || row.Cells[16].Value.ToString() == "" || row.Cells[16].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[16].Value.ToString()),
                                        row.Cells[17].Value == null || row.Cells[17].Value.ToString() == "" ? "-" : row.Cells[17].Value.ToString(),
                                        row.Cells[18].Value == null || row.Cells[18].Value.ToString() == "" ? "-" : row.Cells[18].Value.ToString(),
                                        row.Cells[19].Value == null || row.Cells[19].Value.ToString() == "" ? "-" : row.Cells[19].Value.ToString(),
                                        row.Cells[20].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[20].Value.ToString()),
                                        row.Cells[21].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[21].Value.ToString()));


                            strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                                (i + 1).ToString(), "(#ENTRYNO#)", OpDt, Localization.ParseNativeDouble(cboProcessor.SelectedValue.ToString()),
                                0, base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                BatchNo, BarcodeNo,
                                row.Cells[3].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[3].Value.ToString()),
                                row.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                row.Cells[4].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[4].Value.ToString()),
                                row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                0,
                                row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                0, 0, 0, 0, Localization.ParseNativeDecimal(row.Cells[8].Value.ToString()), 0, 
                                0,"NULL", row.Cells[11].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[11].Value.ToString()), 0, "NULL", 0, 0, 0,
                                row.Cells[12].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[12].Value.ToString()),
                                row.Cells[13].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                                row.Cells[14].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[14].Value.ToString()),
                                row.Cells[15].Value == null || row.Cells[15].Value.ToString() == "" || row.Cells[15].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[15].Value.ToString()),
                                row.Cells[16].Value == null || row.Cells[16].Value.ToString() == "" || row.Cells[16].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[16].Value.ToString()),
                                row.Cells[17].Value == null || row.Cells[17].Value.ToString() == "" ? "-" : row.Cells[17].Value.ToString(),
                                row.Cells[18].Value == null || row.Cells[18].Value.ToString() == "" ? "-" : row.Cells[18].Value.ToString(),
                                row.Cells[19].Value == null || row.Cells[19].Value.ToString() == "" ? "-" : row.Cells[19].Value.ToString(),
                                row.Cells[20].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[20].Value.ToString()),
                                row.Cells[21].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[21].Value.ToString()),
                                "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);


                            strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                                (i + 1).ToString(), "(#ENTRYNO#)", OpDt, Localization.ParseNativeDouble(strLedgerWastage),
                                0, base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                BatchNo, BarcodeNo,
                                row.Cells[3].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[3].Value.ToString()),
                                row.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                row.Cells[4].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[4].Value.ToString()),
                                row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                0,
                                row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                0, 0, 0, 0, Localization.ParseNativeDecimal(row.Cells[9].Value.ToString()), 0,
                                0, "NULL", row.Cells[11].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[11].Value.ToString()), 0, "NULL", 0, 0, 0,
                                row.Cells[12].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[12].Value.ToString()),
                                row.Cells[13].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                                row.Cells[14].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[14].Value.ToString()),
                                row.Cells[15].Value == null || row.Cells[15].Value.ToString() == "" || row.Cells[15].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[15].Value.ToString()),
                                row.Cells[16].Value == null || row.Cells[16].Value.ToString() == "" || row.Cells[16].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[16].Value.ToString()),
                                row.Cells[17].Value == null || row.Cells[17].Value.ToString() == "" ? "-" : row.Cells[17].Value.ToString(),
                                row.Cells[18].Value == null || row.Cells[18].Value.ToString() == "" ? "-" : row.Cells[18].Value.ToString(),
                                row.Cells[19].Value == null || row.Cells[19].Value.ToString() == "" ? "-" : row.Cells[19].Value.ToString(),
                                row.Cells[20].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[20].Value.ToString()),
                                row.Cells[21].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[21].Value.ToString()),
                                "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                        }
                    }

                    row = null;
                }

                string strAdjCatalogStock = string.Empty;
                strAdjCatalogStock += string.Format("Delete From {0} Where  CatalogCreationID= {1};", "tbl_CatalogCreationFooter", "(#CodeID#)");
                for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                {
                    DataGridViewRow row = fgDtls_f.Rows[i];
                    if (row.Cells[3].Value != null)
                    {
                        if (Localization.ParseNativeDouble(row.Cells[3].Value.ToString()) != 0.0)
                        {
                            string BatchNo = "-";
                            string BarcodeNo;
                            if (row.Cells[2].Value != null && row.Cells[2].Value.ToString().Length > 0)
                            {
                                BarcodeNo = row.Cells[2].Value.ToString();
                            }
                            else
                            {
                                BarcodeNo = "-";
                            }

                            strAdjCatalogStock += DBSp.InsertIntoCatalogStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtRefdate.Text.Trim().ToString(),
                                Localization.ParseNativeDouble(cboDeptFrom.SelectedValue.ToString()), 
                                0,
                                base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(), 
                                base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                BatchNo, BarcodeNo,
                                row.Cells[2].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[2].Value.ToString()),
                                Localization.ParseNativeDouble(strunit),
                                0, Localization.ParseNativeDecimal(row.Cells[9].Value.ToString()), 0,
                                "NULL", 
                                row.Cells[12].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[12].Value.ToString()),
                                row.Cells[13].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                                row.Cells[14].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[14].Value.ToString()),
                                row.Cells[15].Value == null || row.Cells[15].Value.ToString() == "" || row.Cells[15].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[15].Value.ToString()),
                                row.Cells[16].Value == null || row.Cells[16].Value.ToString() == "" || row.Cells[16].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[16].Value.ToString()),
                                row.Cells[17].Value == null || row.Cells[17].Value.ToString() == "" ? "-" : row.Cells[17].Value.ToString(),
                                row.Cells[18].Value == null || row.Cells[18].Value.ToString() == "" ? "-" : row.Cells[18].Value.ToString(),
                                row.Cells[19].Value == null || row.Cells[19].Value.ToString() == "" ? "-" : row.Cells[19].Value.ToString(),
                                row.Cells[20].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[20].Value.ToString()),
                                row.Cells[21].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[21].Value.ToString()),
                                "NULL", i, 1,
                                Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                            strAdjCatalogStock += string.Format("Insert Into tbl_CatalogCreationFooter Values({0},{1},{2},{3},{4},{5});" + Environment.NewLine, "(#CodeID#)", (i + 1).ToString(), Localization.ParseNativeInt(row.Cells[2].Value.ToString()), CommonLogic.SQuote(row.Cells[3].Value.ToString()), row.Cells[4].Value.ToString(), row.Cells[5].Value.ToString());
                        }
                    }
                    row = null;
                }
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Null", "null");
                DBSp.Transcation_AddEdit(pArrayData, this.fgDtls_A, true, strAdjQry + strAdjCatalogStock, "", txtEntryNo.Text, "", "");
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                if (!EventHandles.IsValidGridReq(fgDtls, base.dt_AryIsRequired))
                {
                    return true;
                }
                if (!EventHandles.IsRequiredInGrid(fgDtls, dt_AryIsRequired, false))
                {
                    return true;
                }

                if (txtEntryNo.Text == null || txtEntryNo.Text.ToString() == "" || txtEntryNo.Text.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry No.");
                    this.txtEntryNo.Focus();
                    return true;
                }

                if (txtNoOfCatalogs.Text == null || txtNoOfCatalogs.Text.ToString() == "" || txtNoOfCatalogs.Text.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter No.of Catalogs");
                    this.txtNoOfCatalogs.Focus();
                    return true;
                }

                if (!Information.IsDate(Strings.Trim(dtEntryDate.Text.ToString())))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry Date");
                    dtEntryDate.Focus();
                    return true;
                }

                if (cboDeptFrom.SelectedValue == null || cboDeptFrom.SelectedValue.ToString() == "-" || cboDeptFrom.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    cboDeptFrom.Focus();
                    return true;
                }

                //if (txtRate.Text == null || txtRate.Text.ToString() == "" || txtRate.Text.ToString() == "0.00")
                //{
                //    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Rate.");
                //    this.txtRate.Focus();
                //    return true;
                //}

                //if (Localization.ParseNativeInt(cboQualityName.SelectedValue.ToString()) <= 0)
                //{
                //    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Quality");
                //    cboQualityName.Focus();
                //    return true;
                //}

                if (cboCatalogName.SelectedValue == null || cboCatalogName.Text.Trim().ToString() == "-" || cboCatalogName.Text.Trim().ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Catalog");
                    cboCatalogName.Focus();
                    return true;
                }

                if (cboProcessor.SelectedValue == null || cboProcessor.Text.Trim().ToString() == "-" || cboProcessor.Text.Trim().ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Processor");
                    cboProcessor.Focus();
                    return true;
                }

                if (fgDtls.Rows.Count > 0)
                {
                    bool flag = false;
                    for (int i = 0; i < fgDtls.Rows.Count; i++)
                    {
                        DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[2];
                        if (chk.Value != null)
                        {
                            if (Localization.ParseBoolean(chk.Value.ToString()))
                            {
                                flag = true;
                            }
                        }
                    }
                    if (!flag)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select At least One Design");
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                return false;
            }
        }

        #endregion

        #region  GridEvent

        private void fgDtls_A_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (fgDtls.RowCount != 0)
                {
                    if (e.ColumnIndex == 2)
                    {
                        string strVal;
                        if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                        {
                            string primaryFieldNameValue = fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString();
                            if ((fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString() != null) && ((fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString().Length > 0)))
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString() != "-")
                                {
                                    strVal = "tbl_StockFabricLedger";
                                    if (Navigate.CheckDuplicate(ref strVal, "BatchNo", primaryFieldNameValue, false, "", 0L, " StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                                    {
                                        fgDtls.CurrentCell = fgDtls[2, e.RowIndex];
                                    }
                                }
                            }
                            else if (fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString().Length <= 0)
                            {
                                fgDtls.Rows[e.RowIndex].Cells[2].Value = "-";
                            }
                        }
                        else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                        {
                            if ((fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString() != null) && ((fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString().Length > 0)))
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString() != "-")
                                {
                                    strVal = "tbl_StockFabricLedger";
                                    if (Navigate.CheckDuplicate(ref strVal, "BatchNo", fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString(), true, "TransID", Localization.ParseNativeLong(txtCode.Text.Trim()), " StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                                    {
                                        fgDtls.CurrentCell = fgDtls[2, e.RowIndex];
                                    }
                                }
                            }
                            else if (fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString().Length <= 0)
                            {
                                fgDtls.Rows[e.RowIndex].Cells[2].Value = "-";
                            }
                        }
                    }
                    if (((e.ColumnIndex == 4) | (e.ColumnIndex == 6)) && ((fgDtls.Rows[e.RowIndex].Cells[4].Value != null) && (Strings.Trim(Conversions.ToString(fgDtls.Rows[e.RowIndex].Cells[4].Value)).Length > 0)))
                    {
                        fgDtls.Rows[e.RowIndex].Cells[5].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", fgDtls.Rows[e.RowIndex].Cells[4].Value)));
                    }
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_A_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!((base.blnFormAction == Enum_Define.ActionType.View_Record) | (base.blnFormAction == Enum_Define.ActionType.Not_Active)))
                {
                    DataGridViewEx ex = this.fgDtls_A;
                    switch (e.ColumnIndex)
                    {
                        case 2:
                            if (ex.Rows[ex.CurrentRow.Index].Cells[2].Value.ToString() != "")
                            {
                                using (IDataReader reader = DB.GetRS(string.Format("Select Percentage From {0} Where LedgerID = {1}", "tbl_LedgerMaster", ex.Rows[ex.CurrentRow.Index].Cells[2].Value.ToString())))
                                {
                                    if (reader.Read())
                                    {
                                        if (ex.Rows[ex.CurrentRow.Index].Cells[3].Value == null)
                                        {
                                            ex.Rows[ex.CurrentRow.Index].Cells[3].Value = "-";
                                        }
                                        ex.Rows[ex.CurrentRow.Index].Cells[4].Value = reader["Percentage"].ToString();
                                        ex.Rows[ex.CurrentRow.Index].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(reader["Percentage"].ToString())), 100M);
                                    }
                                }
                            }
                            break;

                        case 4:
                            ex.Rows[ex.CurrentRow.Index].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(this.TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[ex.CurrentRow.Index].Cells[4].Value))), 100M).ToString().Replace(",", "");
                            this.CalcVal();
                            break;

                        case 5:
                            ex.Rows[ex.CurrentRow.Index].Cells[4].Value = decimal.Multiply(decimal.Divide(Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[ex.CurrentRow.Index].Cells[5].Value)), Localization.ParseNativeDecimal(TxtGrossAmount.Text)), 100M);
                            this.CalcVal();
                            break;
                    }
                    ex = null;
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    if (fgDtls.Rows[e.RowIndex].Cells[3].Value != null || fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString().Length > 0)
                    {
                        using (IDataReader iDr = DB.GetRS(string.Format("select * from fn_FabricMaster_Tbl() where FabricID= " + fgDtls.Rows[e.RowIndex].Cells[3].Value + "")))
                        {
                            if (iDr.Read())
                            {
                                fgDtls.Rows[e.RowIndex].Cells[3].Value = Localization.ParseNativeInt(iDr["FabricID"].ToString());
                                fgDtls.Rows[e.RowIndex].Cells[4].Value = Localization.ParseNativeInt(iDr["DesignID"].ToString());
                                fgDtls.Rows[e.RowIndex].Cells[5].Value = Localization.ParseNativeInt(iDr["QualityID"].ToString());
                                fgDtls.Rows[e.RowIndex].Cells[6].Value = Localization.ParseNativeInt(iDr["ShadeID"].ToString());
                            }
                        }
                    }
                }

                if (!((base.blnFormAction == Enum_Define.ActionType.View_Record) || (base.blnFormAction == Enum_Define.ActionType.Not_Active)))
                {
                    switch (e.ColumnIndex)
                    {
                        case 8:
                            if (fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString() != null && Localization.ParseNativeDecimal(fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString()) != 0)
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString() != null)
                                {
                                    if (Localization.ParseNativeDecimal(fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString()) <= Localization.ParseNativeDecimal(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString()))
                                    {
                                        //fgDtls.Rows[e.RowIndex].Cells[5].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString()) - Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString())).ToString()));
                                    }
                                    else
                                    {
                                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "There Are only " + fgDtls.Rows[e.RowIndex].Cells[10].Value + " Quantity in the Stock. Please Enter Lesser Value.. ");
                                        fgDtls.Rows[e.RowIndex].Cells[8].Value = 0;
                                    }
                                }
                            }
                            CalcVal();
                            break;

                        case 9:
                            if (fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString() != null && Localization.ParseNativeDecimal(fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString()) != 0)
                            {
                                decimal strWastage = Localization.ParseNativeDecimal(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString()) - ((Localization.ParseNativeDecimal(fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString())));

                                if (strWastage <= Localization.ParseNativeDecimal(fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString()))
                                {
                                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Wastage Cannot Be More Than  " + strWastage + "  Quantity in the Stock.");
                                    fgDtls.Rows[e.RowIndex].Cells[9].Value = 0;
                                }


                            }
                            CalcVal();
                            break;

                        case 10:
                            CalcVal();
                            break;
                    }
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void CalcVal()
        {
            try
            {
                TxtGrossAmount.Text = string.Format("{0:N2}", Localization.ParseNativeDecimal(txtRate.Text.ToString()) * Localization.ParseNativeDecimal(txtNoOfCatalogs.Text.ToString()));

                double dblDedAmt = 0.0;
                DataGridViewEx ex = this.fgDtls_A;
                for (int i = 0; i <= ex.RowCount - 1; i++)
                {
                    if (ex.Rows[i].Cells[4].Value != null)
                    {
                        if (Operators.ConditionalCompareObjectEqual(ex.Rows[i].Cells[4].FormattedValue, "-", false))
                        {
                            dblDedAmt -= Localization.ParseNativeDouble(ex.Rows[i].Cells[6].Value.ToString());
                        }
                        else if (Operators.ConditionalCompareObjectEqual(ex.Rows[i].Cells[4].FormattedValue, "+", false))
                        {
                            dblDedAmt += Localization.ParseNativeDouble(ex.Rows[i].Cells[6].Value.ToString());
                        }
                    }
                }
                ex = null;

                txtAddLessAmt.Text = string.Format("{0:N2}", Math.Round(dblDedAmt));
                txtNetAmt.Text = string.Format("{0:N2}", Math.Round((double)(Convert.ToDouble(Localization.ParseNativeDecimal(TxtGrossAmount.Text)) + dblDedAmt)));
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region Other Event
        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboProcessor.Text.Trim().ToString() != "-" || cboProcessor.SelectedValue != null)
                {
                    frmStockAdj frmStockAdj = new frmStockAdj();
                    frmStockAdj.MenuID = base.iIDentity;
                    frmStockAdj.Entity_IsfFtr = 0.0;
                    frmStockAdj.ref_fgDtls = fgDtls;
                    frmStockAdj.LedgerID = Conversions.ToString(cboProcessor.SelectedValue);
                    frmStockAdj.UsedInGridArray = OrgInGridArray;
                    if (frmStockAdj.ShowDialog() == DialogResult.Cancel)
                    {
                        frmStockAdj.Dispose();
                        return;
                    }
                    frmStockAdj.Dispose();
                    frmStockAdj = null;
                }
                else
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    cboDeptFrom.Focus();
                }
                fgDtls.Select();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void txtRate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TxtGrossAmount.Text = string.Format("{0:N2}", Localization.ParseNativeDecimal(txtRate.Text.ToString()) * Localization.ParseNativeDecimal(txtNoOfCatalogs.Text.ToString()));
                CalcPiece();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public void CalcPiece()
        {
            try
            {
                EventHandles.CreateDefault_Rows(fgDtls_f, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls_f, fgDtls_f_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                string strtxt = "";
                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    if (txtCode.Text == "")
                    {
                        strtxt = "0";
                    }
                    else
                    {
                        strtxt = txtCode.Text;
                    }
                }
                else
                {
                    strtxt = txtCode.Text;
                }
                IDataReader idr = DB.GetRS(string.Format("Select * from fn_GetCatalogCreationPieceNo2(" + strtxt + "," + txtNoOfCatalogs.Text + "," + Db_Detials.StoreID + "," + Db_Detials.CompID + "," + Db_Detials.BranchID + "," + Db_Detials.YearID + ")", false));
                int iSrno = 0;
                while (idr.Read())
                {
                    fgDtls_f.Rows[iSrno].Cells[1].Value = iSrno + 1;
                    fgDtls_f.Rows[iSrno].Cells[2].Value = cboCatalogName.SelectedValue;
                    fgDtls_f.Rows[iSrno].Cells[3].Value = idr["BarcodeNo"].ToString();
                    fgDtls_f.Rows[iSrno].Cells[4].Value = "1";
                    fgDtls_f.Rows[iSrno].Cells[5].Value = "0";
                    fgDtls_f.Rows.Add();
                    iSrno++;
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void txtNoOfCatalogs_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtRate.Text.ToString() != "")
                {
                    TxtGrossAmount.Text = string.Format("{0:N2}", Localization.ParseNativeDecimal(txtRate.Text.ToString()) * Localization.ParseNativeDecimal(txtNoOfCatalogs.Text.ToString()));
                }
                CalcPiece();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                if (Localization.ParseNativeInt(cboCatalogName.SelectedValue.ToString()) != 0 && cboCatalogName.SelectedValue != null && Localization.ParseNativeInt(cboProcessor.SelectedValue.ToString()) != 0 && cboProcessor.SelectedValue != null)
                {
                    int iSrno = 0;

                    fgDtls.Rows.Clear();
                    DataTable dt = DB.GetDT("Select * from fn_CatalogSerialDtlsBind() Where CatalogID= " + cboCatalogName.SelectedValue + " ", false);
                    foreach (DataRow r in dt.Rows)
                    {
                        fgDtls.Rows.Add();
                        fgDtls.Rows[iSrno].Cells[1].Value = iSrno + 1;
                        fgDtls.Rows[iSrno].Cells[3].Value = Localization.ParseNativeInt(r["FabricID"].ToString());
                        fgDtls.Rows[iSrno].Cells[4].Value = Localization.ParseNativeInt(r["DesignID"].ToString());
                        fgDtls.Rows[iSrno].Cells[5].Value = Localization.ParseNativeInt(r["QualityID"].ToString());
                        fgDtls.Rows[iSrno].Cells[6].Value = Localization.ParseNativeInt(r["ShadeID"].ToString());
                        fgDtls.Rows[iSrno].Cells[7].Value = Localization.ParseNativeInt(strunit);
                        fgDtls.Rows[iSrno].Cells[8].Value = (Localization.ParseNativeDecimal(r["Mtrs"].ToString()) * Localization.ParseNativeInt(txtNoOfCatalogs.Text.ToString()));
                        fgDtls.Rows[iSrno].Cells[9].Value = "0.00";
                        DataTable dt1 = DB.GetDT("Select * from [fn_FetchFabricCatalogStock_CatalogCreation](" + Db_Detials.StoreID + "," + Db_Detials.CompID + "," + Db_Detials.BranchID + "," + Db_Detials.YearID + ")  where LedgerID=" + cboProcessor.SelectedValue + " and FabricDesignID=" + Localization.ParseNativeInt(r["DesignID"].ToString()) + " and FabricQualityID=" + Localization.ParseNativeInt(r["QualityID"].ToString()) + (Localization.ParseNativeInt(r["ShadeID"].ToString()) > 0 ? " and FabricShadeID =" + Localization.ParseNativeInt(r["ShadeID"].ToString()) + "" : ""), false);
                        if (dt1.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dt1.Rows.Count - 1; i++)
                            {
                                fgDtls.Rows[iSrno].Cells[10].Value = dt1.Rows[i]["Bal_Mtrs"].ToString();
                            }

                            if (fgDtls.Rows[iSrno].Cells[8].Value.ToString() != null && fgDtls.Rows[iSrno].Cells[10].Value != null && Localization.ParseNativeDecimal(fgDtls.Rows[iSrno].Cells[8].Value.ToString()) != 0)
                            {
                                if (Localization.ParseNativeDecimal(fgDtls.Rows[iSrno].Cells[8].Value.ToString()) <= Localization.ParseNativeDecimal(fgDtls.Rows[iSrno].Cells[10].Value.ToString()))
                                {
                                    //  fgDtls.Rows[e.RowIndex].Cells[5].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString()) - Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString())).ToString()));
                                }
                                else
                                {
                                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "There Are only " + fgDtls.Rows[iSrno].Cells[10].Value + " Quantity in the Stock. Please Enter Lesser Value.. ");
                                    fgDtls.Rows[iSrno].Cells[8].Value = 0;
                                }
                            }
                            CalcVal();
                        }
                        else
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "No Stock Avaliable.... ");
                            fgDtls.Rows[iSrno].Cells[8].Value = 0;
                        }
                        iSrno++;
                    }
                    CalcVal();
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }   
        #endregion
    }
}
