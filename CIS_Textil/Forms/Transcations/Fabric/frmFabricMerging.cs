 using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_Evaluator;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using CIS_DBLayer;
using CIS_Textil;

namespace CIS_Textil
{
    public partial class frmFabricMerging : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;
        public DataGridViewEx fgDtls_f;
        public DataGridViewEx fgDtls_f_footer;

        public ArrayList OrgInGridArray;
        public bool FAB_MAINTAINWEIGHT;
        public string strUniqueID;
        private int RefMenuID;
        private static string RefVoucherID;
        public CIS_CLibrary.CIS_Textbox TxtTotMtrs_F;

        public frmFabricMerging()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
            TxtTotMtrs_F = new CIS_CLibrary.CIS_Textbox();
            fgDtls_f = GrdFooter.fgDtls;
            fgDtls_f_footer = GrdFooter.fgDtls_f;
            OrgInGridArray = new ArrayList();
        }

        #region Event

        private void frmFabricMerging_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboDepartment, Combobox_Setup.ComboType.Mst_Department, "");
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls_f, fgDtls_f_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, true, true, 0, 1, true);
                txtEntryNo.Enabled = false;
                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls_f.RowsAdded += new DataGridViewRowsAddedEventHandler(this.fgDtls_f_RowsAdded);
                this.fgDtls_f.CellLeave += new DataGridViewCellEventHandler(this.fgDtls_f_CellLeave);
                this.fgDtls_f.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_f_CellValueChanged);
                this.fgDtls_f.CellEndEdit += new DataGridViewCellEventHandler(this.fgDtls_f_CellEndEdit);
                FAB_MAINTAINWEIGHT = Localization.ParseBoolean(GlobalVariables.FAB_MAINTAINWEIGHT);
                RefMenuID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MenuID from tbl_VoucherTypeMaster Where GenMenuID=" + base.iIDentity + "")));
                GetRefModID();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #endregion

        #region Navigation

        public void MovetoField()
        {
            try
            {
                if (strUniqueID != null)
                {
                    string strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                    strQry = strQry + string.Format("Update  tbl_StockFabricLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                    DB.ExecuteSQL(strQry);
                    strQry = string.Format("Update tbl_StockFabricLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                    DB.ExecuteSQL(strQry);
                    strQry = "";
                }
                txtCode.Text = "";
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                EventHandles.CreateDefault_Rows(fgDtls_f, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls_f, fgDtls_f_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);

                int MaxID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format(" Select Isnull(Max(FabricMergeID),0) From {0}  Where IsDeleted=0 and CompID = {1} and YearID = {2}", "tbl_FabricMergingMain", Db_Detials.CompID, Db_Detials.YearID)));

                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where IsDeleted=0 and FabricMergeID = {1} and CompID={2} and YearID={3}", new object[] { "tbl_FabricMergingMain", MaxID, Db_Detials.CompID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                        cboDepartment.SelectedValue = Localization.ParseNativeInt(reader["DepartmentID"].ToString());
                    }
                }
                txtUniqueID.Text = CommonCls.GenUniqueID();
                strUniqueID = txtUniqueID.Text;
                dtEntryDate.Focus();
                TxtTotMtrs_F.Text = "0.00";
                AplySelectBtnEnbl();
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
                DBValue.Return_DBValue(this, txtCode, "FabricMergeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboDepartment, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtSampleMtrs, "SampleMtrs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtFentMtrs, "FentMtrs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPiecesMtrs, "PcsMtrs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtUniqueID, "UniqueID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtED1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "FabricMergeID", txtCode.Text, base.dt_HasDtls_Grd, this.iIDentity);
                DetailGrid_Setup.FillGrid(fgDtls_f, this.fgDtls_f.Grid_UID, this.fgDtls_f.Grid_Tbl, "FabricMergeID", txtCode.Text, base.dt_HasDtls_Grd, this.iIDentity, GlobalVariables.VALIDATE_EDIT, Db_Detials.CompID.ToString(), 1);

                int icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_StockFabricLedger_tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                    try
                    {
                        string strOldUniqueID = string.Empty;
                        strOldUniqueID = txtUniqueID.Text;
                        txtUniqueID.Text = CommonCls.GenUniqueID();
                        strUniqueID = txtUniqueID.Text;
                        if (icount == 0)
                        {
                            string strQry = string.Format("Update tbl_StockFabricLedger Set UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + ",StatusID=2 Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + "");
                            DB.ExecuteSQL(strQry);
                        }
                        else
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityShieldBlue, "Warning", "This Record Is Edited By Another User...");
                        }
                    }
                    catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                }
                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    if (strUniqueID != null)
                    {
                        string strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                        DB.ExecuteSQL(strQry);

                        strQry = string.Format("Update tbl_StockFabricLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                        DB.ExecuteSQL(strQry);
                    }
                }
                CalcVal();
                AplySelectBtnEnbl();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        public void SaveRecord()
        {
            try
            {
                ArrayList pArrayData = new ArrayList
                {
                    "(#ENTRYNO#)",
                    dtEntryDate.TextFormat(false, true),
                    cboDepartment.SelectedValue,
                    txtSampleMtrs.Text.Replace(",",""),
                    txtFentMtrs.Text.Replace(",", ""),
                    txtPiecesMtrs.Text.Replace(",", ""),
                    txtDescription.Text == ""? "-": txtDescription.Text,
                    txtUniqueID.Text,
                    (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                    (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                    (dtED1.TextFormat(false, true)),
                    (txtET1.Text.Trim()),
                    (txtET2.Text.Trim()),
                    (txtET3.Text.Trim())
                };

                int MetersID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select UnitID From {0} Where UnitName = 'Meters' ", "fn_UnitsMaster_Tbl()")));
                int SamplingGodawnID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select LedgerId From {0} Where LedgerName = 'SAMPLING' ", "fn_LedgerMaster_Tbl()")));
                int FentGodownID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select LedgerID From {0} Where LedgerName = 'FENT'", "fn_LedgerMaster_Tbl()")));
                int ShortageAccount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select LedgerID from {0} where LedgerName='SHORTAGE ACCOUNT'", "fn_LedgerMaster_Tbl()")));

                string StrAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", "(#CodeID#)", base.iIDentity);
                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    if (row.Cells[11].Value != null)
                    {
                        if (Localization.ParseNativeDouble(row.Cells[11].Value.ToString()) != 0.0)
                        {
                            StrAdjQry = StrAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                                (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                                row.Cells[17].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                row.Cells[37].Value == null ? "NULL" : row.Cells[37].Value.ToString().Trim() == "" ? "NULL" : row.Cells[37].Value.ToString(),
                                row.Cells[2].Value == null ? "-" : row.Cells[2].Value.ToString() == "0" ? "-" : row.Cells[2].Value.ToString() == "" ? "-" : row.Cells[2].Value.ToString(),
                                row.Cells[3].Value.ToString(),
                                row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                row.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                row.Cells[8].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                row.Cells[9].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                0, 0, 0,
                                Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()), 
                                row.Cells[13].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()),
                                "NULL",
                                row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                row.Cells[19].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[19].Value.ToString()),
                                row.Cells[20].Value == null ? "NULL" : row.Cells[20].Value.ToString(),
                                row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                row.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                row.Cells[27].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[27].Value.ToString()),
                                row.Cells[28].Value == null || row.Cells[28].Value.ToString() == "" || row.Cells[28].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[28].Value.ToString()),
                                row.Cells[29].Value == null || row.Cells[29].Value.ToString() == "" || row.Cells[29].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[29].Value.ToString()),
                                row.Cells[30].Value == null || row.Cells[30].Value.ToString() == "" ? "-" : row.Cells[30].Value.ToString(),
                                row.Cells[31].Value == null || row.Cells[31].Value.ToString() == "" ? "-" : row.Cells[31].Value.ToString(),
                                row.Cells[32].Value == null || row.Cells[32].Value.ToString() == "" ? "-" : row.Cells[32].Value.ToString(),
                                row.Cells[33].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[33].Value.ToString()),
                                row.Cells[34].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[34].Value.ToString()),
                                "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                        }
                    }
                    row = null;
                }

                DataGridViewEx ex2 = this.fgDtls_f;
                for (int j = 0; j <= ex2.RowCount - 1; j++)
                {
                    DataGridViewRow row2 = ex2.Rows[j];
                    if (row2.Cells[11].Value != null)
                    {
                        if (Localization.ParseNativeDouble(row2.Cells[11].Value.ToString()) != 0.0)
                        {
                            StrAdjQry = StrAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                                (j + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                                row2.Cells[17].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[17].Value.ToString()),
                                base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (j + 1).ToString(),
                                row2.Cells[37].Value == null ? "NULL" : row2.Cells[37].Value.ToString().Trim() == "" ? "NULL" : row2.Cells[37].Value.ToString(),
                                row2.Cells[2].Value == null ? "-" : row2.Cells[2].Value.ToString() == "0" ? "-" : row2.Cells[2].Value.ToString() == "" ? "-" : row2.Cells[2].Value.ToString(),
                                row2.Cells[3].Value.ToString(),
                                row2.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[4].Value.ToString()),
                                row2.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[6].Value.ToString()),
                                row2.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[5].Value.ToString()),
                                row2.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[7].Value.ToString()),
                                row2.Cells[8].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[8].Value.ToString()),
                                row2.Cells[9].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[9].Value.ToString()),
                                Localization.ParseNativeDecimal(row2.Cells[10].Value.ToString()), Localization.ParseNativeDecimal(row2.Cells[11].Value.ToString()),
                                Localization.ParseNativeDecimal(row2.Cells[12].Value.ToString()), 0, 0, 0,
                                row2.Cells[13].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[13].Value.ToString()),
                                "NULL",
                                row2.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[18].Value.ToString()),
                                row2.Cells[19].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[19].Value.ToString()),
                                row2.Cells[20].Value == null ? "NULL" : row2.Cells[20].Value.ToString(),
                                row2.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[21].Value.ToString()),
                                row2.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[22].Value.ToString()),
                                row2.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[24].Value.ToString()),
                                row2.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[25].Value.ToString()),
                                row2.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[26].Value.ToString()),
                                row2.Cells[27].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[27].Value.ToString()),
                                row2.Cells[28].Value == null || row2.Cells[28].Value.ToString() == "" || row2.Cells[28].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row2.Cells[28].Value.ToString()),
                                row2.Cells[29].Value == null || row2.Cells[29].Value.ToString() == "" || row2.Cells[29].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row2.Cells[29].Value.ToString()),
                                row2.Cells[30].Value == null || row2.Cells[30].Value.ToString() == "" ? "-" : row2.Cells[30].Value.ToString(),
                                row2.Cells[31].Value == null || row2.Cells[31].Value.ToString() == "" ? "-" : row2.Cells[31].Value.ToString(),
                                row2.Cells[32].Value == null || row2.Cells[32].Value.ToString() == "" ? "-" : row2.Cells[32].Value.ToString(),
                                row2.Cells[33].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[33].Value.ToString()),
                                row2.Cells[34].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[34].Value.ToString()),
                                "NULL", j, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                        }
                    }
                    row2 = null;
                }
                string strRefID = fgDtls.Rows[0].Cells[36].Value.ToString();
                if (Localization.ParseNativeDouble(txtSampleMtrs.Text) > 0.0)
                {
                    StrAdjQry = StrAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                            "101", "(#ENTRYNO#)", dtEntryDate.Text, SamplingGodawnID,
                            fgDtls.Rows[0].Cells[17].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[17].Value.ToString()),
                            base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (0 + 1).ToString(),
                            fgDtls.Rows[0].Cells[37].Value == null ? "NULL" : fgDtls.Rows[0].Cells[37].Value.ToString().Trim() == "" ? "NULL" : fgDtls.Rows[0].Cells[37].Value.ToString(),
                            fgDtls.Rows[0].Cells[2].Value == null ? "-" : fgDtls.Rows[0].Cells[2].Value.ToString() == "0" ? "-" : fgDtls.Rows[0].Cells[2].Value.ToString() == "" ? "-" : fgDtls.Rows[0].Cells[2].Value.ToString(),
                            fgDtls.Rows[0].Cells[3].Value.ToString(),
                            fgDtls.Rows[0].Cells[4].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[4].Value.ToString()),
                            fgDtls.Rows[0].Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(fgDtls.Rows[0].Cells[6].Value.ToString()),
                            fgDtls.Rows[0].Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(fgDtls.Rows[0].Cells[5].Value.ToString()),
                            fgDtls.Rows[0].Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(fgDtls.Rows[0].Cells[7].Value.ToString()),
                            fgDtls_f.Rows[0].Cells[8].Value == null ? 0 : Localization.ParseNativeInt(fgDtls_f.Rows[0].Cells[8].Value.ToString()),
                            MetersID,
                            0, Localization.ParseNativeDecimal(txtSampleMtrs.Text.Trim().ToString()),
                            0, 0, 0, 0,
                            fgDtls.Rows[0].Cells[13].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[0].Cells[13].Value.ToString()),
                            "NULL",
                            fgDtls.Rows[0].Cells[18].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[18].Value.ToString()),
                            fgDtls.Rows[0].Cells[19].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[19].Value.ToString()),
                            fgDtls.Rows[0].Cells[20].Value == null ? "NULL" : fgDtls.Rows[0].Cells[20].Value.ToString(),
                            fgDtls.Rows[0].Cells[21].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[21].Value.ToString()),
                            fgDtls.Rows[0].Cells[22].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[22].Value.ToString()),
                            fgDtls.Rows[0].Cells[24].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[24].Value.ToString()),
                            fgDtls.Rows[0].Cells[25].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[25].Value.ToString()),
                            fgDtls.Rows[0].Cells[26].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[26].Value.ToString()),
                            fgDtls.Rows[0].Cells[27].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[27].Value.ToString()),
                            fgDtls.Rows[0].Cells[28].Value == null || fgDtls.Rows[0].Cells[28].Value.ToString() == "" || fgDtls.Rows[0].Cells[28].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(fgDtls.Rows[0].Cells[28].Value.ToString()),
                            fgDtls.Rows[0].Cells[29].Value == null || fgDtls.Rows[0].Cells[29].Value.ToString() == "" || fgDtls.Rows[0].Cells[29].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(fgDtls.Rows[0].Cells[29].Value.ToString()),
                            fgDtls.Rows[0].Cells[30].Value == null || fgDtls.Rows[0].Cells[30].Value.ToString() == "" ? "-" : fgDtls.Rows[0].Cells[30].Value.ToString(),
                            fgDtls.Rows[0].Cells[31].Value == null || fgDtls.Rows[0].Cells[31].Value.ToString() == "" ? "-" : fgDtls.Rows[0].Cells[31].Value.ToString(),
                            fgDtls.Rows[0].Cells[32].Value == null || fgDtls.Rows[0].Cells[32].Value.ToString() == "" ? "-" : fgDtls.Rows[0].Cells[32].Value.ToString(),
                            fgDtls.Rows[0].Cells[33].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[0].Cells[33].Value.ToString()),
                            fgDtls.Rows[0].Cells[34].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[0].Cells[34].Value.ToString()),
                            "NULL", 0, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                }
                if (Localization.ParseNativeDouble(txtFentMtrs.Text) > 0.0)
                {
                    StrAdjQry += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                            "101", "(#ENTRYNO#)", dtEntryDate.Text, SamplingGodawnID,
                            fgDtls.Rows[0].Cells[17].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[17].Value.ToString()),
                            base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (0 + 1).ToString(),
                            fgDtls.Rows[0].Cells[37].Value == null ? "NULL" : fgDtls.Rows[0].Cells[37].Value.ToString().Trim() == "" ? "NULL" : fgDtls.Rows[0].Cells[37].Value.ToString(),
                            fgDtls.Rows[0].Cells[2].Value == null ? "-" : fgDtls.Rows[0].Cells[2].Value.ToString() == "0" ? "-" : fgDtls.Rows[0].Cells[2].Value.ToString() == "" ? "-" : fgDtls.Rows[0].Cells[2].Value.ToString(),
                            fgDtls.Rows[0].Cells[3].Value.ToString(),
                            fgDtls.Rows[0].Cells[4].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[4].Value.ToString()),
                            fgDtls.Rows[0].Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(fgDtls.Rows[0].Cells[6].Value.ToString()),
                            fgDtls.Rows[0].Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(fgDtls.Rows[0].Cells[5].Value.ToString()),
                            fgDtls.Rows[0].Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(fgDtls.Rows[0].Cells[7].Value.ToString()),
                            fgDtls_f.Rows[0].Cells[8].Value == null ? 0 : Localization.ParseNativeInt(fgDtls_f.Rows[0].Cells[8].Value.ToString()),
                            MetersID,
                            0, Localization.ParseNativeDecimal(txtFentMtrs.Text.Trim().ToString()),
                            0, 0, 0, 0,
                            fgDtls.Rows[0].Cells[13].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[0].Cells[13].Value.ToString()),
                            "NULL",
                            fgDtls.Rows[0].Cells[18].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[18].Value.ToString()),
                            fgDtls.Rows[0].Cells[19].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[19].Value.ToString()),
                            fgDtls.Rows[0].Cells[20].Value == null ? "NULL" : fgDtls.Rows[0].Cells[20].Value.ToString(),
                            fgDtls.Rows[0].Cells[21].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[21].Value.ToString()),
                            fgDtls.Rows[0].Cells[22].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[22].Value.ToString()),
                            fgDtls.Rows[0].Cells[24].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[24].Value.ToString()),
                            fgDtls.Rows[0].Cells[25].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[25].Value.ToString()),
                            fgDtls.Rows[0].Cells[26].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[26].Value.ToString()),
                            fgDtls.Rows[0].Cells[27].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[27].Value.ToString()),
                            fgDtls.Rows[0].Cells[28].Value == null || fgDtls.Rows[0].Cells[28].Value.ToString() == "" || fgDtls.Rows[0].Cells[28].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(fgDtls.Rows[0].Cells[28].Value.ToString()),
                            fgDtls.Rows[0].Cells[29].Value == null || fgDtls.Rows[0].Cells[29].Value.ToString() == "" || fgDtls.Rows[0].Cells[29].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(fgDtls.Rows[0].Cells[29].Value.ToString()),
                            fgDtls.Rows[0].Cells[30].Value == null || fgDtls.Rows[0].Cells[30].Value.ToString() == "" ? "-" : fgDtls.Rows[0].Cells[30].Value.ToString(),
                            fgDtls.Rows[0].Cells[31].Value == null || fgDtls.Rows[0].Cells[31].Value.ToString() == "" ? "-" : fgDtls.Rows[0].Cells[31].Value.ToString(),
                            fgDtls.Rows[0].Cells[32].Value == null || fgDtls.Rows[0].Cells[32].Value.ToString() == "" ? "-" : fgDtls.Rows[0].Cells[32].Value.ToString(),
                            fgDtls.Rows[0].Cells[33].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[0].Cells[33].Value.ToString()),
                            fgDtls.Rows[0].Cells[34].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[0].Cells[34].Value.ToString()),
                            "NULL", 0, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                }
                if (Localization.ParseNativeDouble(txtPiecesMtrs.Text) > 0.0)
                {
                    StrAdjQry += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                            "101", "(#ENTRYNO#)", dtEntryDate.Text, SamplingGodawnID,
                            fgDtls.Rows[0].Cells[17].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[17].Value.ToString()),
                            base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (0 + 1).ToString(),
                            fgDtls.Rows[0].Cells[37].Value == null ? "NULL" : fgDtls.Rows[0].Cells[37].Value.ToString().Trim() == "" ? "NULL" : fgDtls.Rows[0].Cells[37].Value.ToString(),
                            fgDtls.Rows[0].Cells[2].Value == null ? "-" : fgDtls.Rows[0].Cells[2].Value.ToString() == "0" ? "-" : fgDtls.Rows[0].Cells[2].Value.ToString() == "" ? "-" : fgDtls.Rows[0].Cells[2].Value.ToString(),
                            fgDtls.Rows[0].Cells[3].Value.ToString(),
                            fgDtls.Rows[0].Cells[4].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[4].Value.ToString()),
                            fgDtls.Rows[0].Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(fgDtls.Rows[0].Cells[6].Value.ToString()),
                            fgDtls.Rows[0].Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(fgDtls.Rows[0].Cells[5].Value.ToString()),
                            fgDtls.Rows[0].Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(fgDtls.Rows[0].Cells[7].Value.ToString()),
                            fgDtls_f.Rows[0].Cells[8].Value == null ? 0 : Localization.ParseNativeInt(fgDtls_f.Rows[0].Cells[8].Value.ToString()),
                            MetersID,
                            0, Localization.ParseNativeDecimal(txtPiecesMtrs.Text.Trim().ToString()),
                            0, 0, 0, 0,
                            fgDtls.Rows[0].Cells[13].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[0].Cells[13].Value.ToString()),
                            "NULL",
                            fgDtls.Rows[0].Cells[18].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[18].Value.ToString()),
                            fgDtls.Rows[0].Cells[19].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[19].Value.ToString()),
                            fgDtls.Rows[0].Cells[20].Value == null ? "NULL" : fgDtls.Rows[0].Cells[20].Value.ToString(),
                            fgDtls.Rows[0].Cells[21].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[21].Value.ToString()),
                            fgDtls.Rows[0].Cells[22].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[22].Value.ToString()),
                            fgDtls.Rows[0].Cells[24].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[24].Value.ToString()),
                            fgDtls.Rows[0].Cells[25].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[25].Value.ToString()),
                            fgDtls.Rows[0].Cells[26].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[26].Value.ToString()),
                            fgDtls.Rows[0].Cells[27].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[0].Cells[27].Value.ToString()),
                            fgDtls.Rows[0].Cells[28].Value == null || fgDtls.Rows[0].Cells[28].Value.ToString() == "" || fgDtls.Rows[0].Cells[28].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(fgDtls.Rows[0].Cells[28].Value.ToString()),
                            fgDtls.Rows[0].Cells[29].Value == null || fgDtls.Rows[0].Cells[29].Value.ToString() == "" || fgDtls.Rows[0].Cells[29].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(fgDtls.Rows[0].Cells[29].Value.ToString()),
                            fgDtls.Rows[0].Cells[30].Value == null || fgDtls.Rows[0].Cells[30].Value.ToString() == "" ? "-" : fgDtls.Rows[0].Cells[30].Value.ToString(),
                            fgDtls.Rows[0].Cells[31].Value == null || fgDtls.Rows[0].Cells[31].Value.ToString() == "" ? "-" : fgDtls.Rows[0].Cells[31].Value.ToString(),
                            fgDtls.Rows[0].Cells[32].Value == null || fgDtls.Rows[0].Cells[32].Value.ToString() == "" ? "-" : fgDtls.Rows[0].Cells[32].Value.ToString(),
                            fgDtls.Rows[0].Cells[33].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[0].Cells[33].Value.ToString()),
                            fgDtls.Rows[0].Cells[34].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[0].Cells[34].Value.ToString()),
                            "NULL", 0, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                }
                StrAdjQry += "Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";";
                StrAdjQry = StrAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                DBSp.Transcation_AddEdit(pArrayData, fgDtls, true, StrAdjQry, "", txtEntryNo.Text, "", "", new DataGridViewEx[] { fgDtls_f });
                OrgInGridArray.Clear();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        private void CalcVal()
        {
            try
            {
                TxtTotMtrs_F.Text = string.Format("{0:N2}", CommonCls.GetColSum(fgDtls_f, 11, -1, -1));
                TxtTotMtrs_F.Text = Conversions.ToString(Evaluator.EvalToDouble(string.Format("({0})+({1})+({2})+({3})", Localization.ParseNativeDecimal(string.Format("{0:N2}", CommonCls.GetColSum(fgDtls_f, 11, -1, -1))), Localization.ParseNativeDecimal(txtFentMtrs.Text), Localization.ParseNativeDecimal(txtPiecesMtrs.Text), Localization.ParseNativeDecimal(txtSampleMtrs.Text))));
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public void AplySelectBtnEnbl()
        {
            if ((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record))
            {
                btnSelect.Enabled = true;
            }
            else
            {
                btnSelect.Enabled = false;
            }
        }

        public bool ValidateForm()
        {
            try
            {
                if (!EventHandles.IsValidGridReq(fgDtls, base.dt_AryIsRequired) || !EventHandles.IsValidGridReq(this.fgDtls_f, base.dt_AryIsRequired))
                {
                    return true;
                }
                if (!EventHandles.IsRequiredInGrid(fgDtls, this.dt_AryIsRequired, false))
                {
                    return true;
                }
                if (!EventHandles.IsRequiredInGrid(fgDtls, this.dt_AryIsRequired, false))
                {
                    return true;
                }
                if (txtEntryNo.Text.Trim() == "" || txtEntryNo.Text.Trim() == "-" || txtEntryNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry No.");
                    return true;
                }
                if (!Information.IsDate(dtEntryDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry Date");
                    dtEntryDate.Focus();
                    return true;
                }
                if (cboDepartment.SelectedValue == null || cboDepartment.Text.Trim().ToString() == "-" || cboDepartment.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    cboDepartment.Focus();
                    return true;
                }
                if (Conversion.Val(string.Format("{0:N2}", CommonCls.GetColSum(fgDtls, 11, -1, -1))) != Conversion.Val(TxtTotMtrs_F.Text))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "The Marging Meters are not Maching");
                    return true;
                }
                CalcVal();

                if (FAB_MAINTAINWEIGHT)
                {
                    for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                    {
                        if (fgDtls_f.Rows[i].Cells[12].Value == null || Localization.ParseNativeDecimal(fgDtls_f.Rows[i].Cells[12].Value.ToString()) == 0 || fgDtls_f.Rows[i].Cells[12].Value.ToString() == "")
                        {
                            fgDtls_f.CurrentCell = fgDtls_f[12, i];
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Weight");
                            fgDtls_f.Rows.Add();
                            return true;
                        }
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

        #region Grid Events

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!((base.blnFormAction == Enum_Define.ActionType.View_Record) | (base.blnFormAction == Enum_Define.ActionType.Not_Active)) && ((e.ColumnIndex == 10) | (e.ColumnIndex == 11)))
                {

                    if (e.ColumnIndex == 10 || e.ColumnIndex == 11 || e.ColumnIndex == 12)
                    {
                        CalcVal();
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_f_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewEx ex = this.fgDtls_f;
                if (e.ColumnIndex == 3)
                {
                    string strTblName;
                    if (base.blnFormAction == 0)
                    {
                        string str = ex.Rows[e.RowIndex].Cells[3].Value.ToString();
                        if ((Strings.Trim(Conversions.ToString(ex.Rows[e.RowIndex].Cells[3].Value)) != null) && (Strings.Trim(Conversions.ToString(ex.Rows[e.RowIndex].Cells[3].Value)).Length > 0))
                        {
                            if (ex.Rows[e.RowIndex].Cells[3].Value.ToString() != "-")
                            {
                                strTblName = "tbl_StockFabricLedger";
                                if (Navigate.CheckDuplicate(ref strTblName, "BarCodeNo", str, false, "", 0L, " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "", ""))
                                {
                                    ex.CurrentCell = ex[3, e.RowIndex];
                                }
                            }
                        }
                        else if (ex.Rows[e.RowIndex].Cells[3].Value.ToString().Length <= 0)
                        {
                            ex.Rows[e.RowIndex].Cells[3].Value = "-";
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        if (ex.Rows[e.RowIndex].Cells[3].Value != null && ex.Rows[e.RowIndex].Cells[3].Value.ToString().Length > 0)
                        {
                            if (ex.Rows[e.RowIndex].Cells[3].Value.ToString() != "-")
                            {
                                strTblName = "tbl_StockFabricLedger";
                                if (Navigate.CheckDuplicate(ref strTblName, "BarCodeNo", ex.Rows[e.RowIndex].Cells[3].Value.ToString(), true, "TransID", Localization.ParseNativeLong(txtCode.Text), " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "", ""))
                                {
                                    ex.CurrentCell = ex[3, e.RowIndex];
                                }
                            }
                        }
                        else if (ex.Rows[e.RowIndex].Cells[3].Value.ToString().Length <= 0)
                        {
                            ex.Rows[e.RowIndex].Cells[3].Value = "-";
                        }
                    }
                }
                if (((e.ColumnIndex == 5) | (e.ColumnIndex == 7)) && ((ex.Rows[e.RowIndex].Cells[5].Value != null) && (Strings.Trim(Conversions.ToString(ex.Rows[e.RowIndex].Cells[5].Value)).Length > 0)))
                {
                    ex.Rows[e.RowIndex].Cells[6].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", ex.Rows[e.RowIndex].Cells[5].Value)));
                }
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (e.ColumnIndex == 3 || e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 7 || e.ColumnIndex == 8 || e.ColumnIndex == 9)
                    {
                        for (int j = 0; j <= fgDtls_f.RowCount - 1; j++)
                        {
                            string CrSidePieceNo = fgDtls.Rows[0].Cells[3].Value.ToString().ToUpper();
                            string DrSidePieceNO = fgDtls_f.Rows[j].Cells[3].Value.ToString().ToUpper();
                            if (fgDtls.Rows[0].Cells[3].Value != null && fgDtls.Rows[0].Cells[3].Value.ToString() != "" && fgDtls_f.Rows[j].Cells[3].Value != null && fgDtls_f.Rows[j].Cells[3].Value.ToString() != "")
                            {
                                if (CrSidePieceNo == DrSidePieceNO)
                                {
                                    fgDtls_f.Rows[j].Cells[5].ReadOnly = true;
                                    fgDtls_f.Rows[j].Cells[6].ReadOnly = true;
                                    fgDtls_f.Rows[j].Cells[7].ReadOnly = true;
                                    fgDtls_f.Rows[j].Cells[8].ReadOnly = true;
                                    fgDtls_f.Rows[j].Cells[9].ReadOnly = true;
                                }
                                else
                                {
                                    fgDtls_f.Rows[j].Cells[5].ReadOnly = false;
                                    fgDtls_f.Rows[j].Cells[6].ReadOnly = false;
                                    fgDtls_f.Rows[j].Cells[7].ReadOnly = false;
                                    fgDtls_f.Rows[j].Cells[8].ReadOnly = false;
                                    fgDtls_f.Rows[j].Cells[9].ReadOnly = false;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_f_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!((base.blnFormAction == Enum_Define.ActionType.View_Record) | (base.blnFormAction == Enum_Define.ActionType.Not_Active)))
                {
                    if ((e.ColumnIndex == 10) | (e.ColumnIndex == 11) | (e.ColumnIndex == 12))
                    {
                        this.CalcVal();
                        ExecuterTempQry(e.RowIndex);
                    }
                    DataGridViewEx ex = this.fgDtls_f;
                    if (e.ColumnIndex == 5 || e.ColumnIndex == 7 && ex.Rows[e.RowIndex].Cells[5].Value != null && ex.Rows[e.RowIndex].Cells[5].Value.ToString().Length > 0)
                    {
                        ex.Rows[e.RowIndex].Cells[6].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", ex.Rows[e.RowIndex].Cells[5].Value)));
                    }
                }

                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (e.ColumnIndex == 3 || e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 7 || e.ColumnIndex == 8 || e.ColumnIndex == 9)
                    {
                        for (int j = 0; j <= fgDtls_f.RowCount - 1; j++)
                        {
                            string CrSidePieceNo = fgDtls.Rows[0].Cells[3].Value.ToString().ToUpper();
                            string DrSidePieceNO = fgDtls_f.Rows[j].Cells[3].Value.ToString().ToUpper();

                            if (fgDtls.Rows[0].Cells[3].Value != null && fgDtls.Rows[0].Cells[3].Value.ToString() != "" && fgDtls_f.Rows[j].Cells[3].Value != null && fgDtls_f.Rows[j].Cells[3].Value.ToString() != "")
                            {
                                if (CrSidePieceNo == DrSidePieceNO)
                                {
                                    fgDtls_f.Rows[j].Cells[5].ReadOnly = true;
                                    fgDtls_f.Rows[j].Cells[6].ReadOnly = true;
                                    fgDtls_f.Rows[j].Cells[7].ReadOnly = true;
                                    fgDtls_f.Rows[j].Cells[8].ReadOnly = true;
                                    fgDtls_f.Rows[j].Cells[9].ReadOnly = true;
                                }
                                else
                                {
                                    fgDtls_f.Rows[j].Cells[5].ReadOnly = false;
                                    fgDtls_f.Rows[j].Cells[6].ReadOnly = false;
                                    fgDtls_f.Rows[j].Cells[7].ReadOnly = false;
                                    fgDtls_f.Rows[j].Cells[8].ReadOnly = false;
                                    fgDtls_f.Rows[j].Cells[9].ReadOnly = false;
                                }
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_f_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                //{
                //    if (e.ColumnIndex == 3)
                //    {
                //        if ((fgDtls_f.Rows[e.RowIndex].Cells[5].Value == null) || (Localization.ParseNativeInt(fgDtls_f.Rows[e.RowIndex].Cells[5].Value.ToString()) == 0))
                //        {
                //            fgDtls_f.Rows[e.RowIndex].Cells[5].Value = Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString());
                //            fgDtls_f.Rows[e.RowIndex].Cells[6].Value = Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString());
                //            fgDtls_f.Rows[e.RowIndex].Cells[7].Value = Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString());
                //            fgDtls_f.Rows[e.RowIndex].Cells[8].Value = Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString());
                //            fgDtls_f.Rows[e.RowIndex].Cells[9].Value = Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString());
                //            fgDtls_f.Rows[e.RowIndex].Cells[2].Value = fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString();
                //            fgDtls_f.Rows[e.RowIndex].Cells[35].Value = fgDtls.Rows[e.RowIndex].Cells[12].Value.ToString();
                //            fgDtls_f.Rows[e.RowIndex].Cells[5].ReadOnly = true;
                //            fgDtls_f.Rows[e.RowIndex].Cells[6].ReadOnly = true;
                //            fgDtls_f.Rows[e.RowIndex].Cells[7].ReadOnly = true;
                //            fgDtls_f.Rows[e.RowIndex].Cells[8].ReadOnly = true;
                //            fgDtls_f.Rows[e.RowIndex].Cells[9].ReadOnly = true;
                //            fgDtls_f.Rows[e.RowIndex].Cells[2].ReadOnly = true;
                //            fgDtls_f.Rows[e.RowIndex].Cells[35].ReadOnly = true;
                //        }
                //    }
                //}
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_f_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                if (base.blnFormAction == 0)
                {
                    DataGridViewEx ex = this.fgDtls_f;
                    if (ex.Rows.Count > 1)
                    {
                        int num = fgDtls.CurrentRow.Index + 1;
                        if (ex.Rows[e.RowIndex - 1].Cells[3].Value.ToString().Trim() != "-")
                        {
                            ex.Rows[e.RowIndex].Cells[3].Value = CommonCls.AutoInc_Runtime(ex.Rows[e.RowIndex - 1].Cells[3].Value.ToString(), Db_Detials.PCS_NO_INCMT);
                            ex.Rows[e.RowIndex].Cells[12].Value = 0;
                        }
                        else
                        {
                            ex.Rows[e.RowIndex].Cells[3].Value = "-";
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #endregion

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
            {
                try
                {
                    if (cboDepartment.SelectedValue != null && cboDepartment.SelectedValue.ToString() != "" && cboDepartment.Text.Trim().ToString() != "-")
                    {
                        if (dtEntryDate.Text != "__/__/____")
                        {
                            #region StockAdjQuery
                            string strQry = string.Empty;
                            int ibitcol = 0;
                            string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", iIDentity, 0.0));
                            string strQry_ColName = "";
                            string[] arr = CommonCls.GetAdjColName(base.iIDentity, 0.0).Split(';');
                            strQry_ColName = arr[0].ToString();
                            ibitcol = Localization.ParseNativeInt(arr[1]);
                            strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}, {6}, {7})", new object[] { strQry_ColName, snglValue, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(this.dtEntryDate.Text))), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboDepartment.SelectedValue });
                            #endregion

                            frmStockAdj frmStockAdj = new frmStockAdj();
                            frmStockAdj.MenuID = base.iIDentity;
                            frmStockAdj.Entity_IsfFtr = 0.0;
                            frmStockAdj.ref_fgDtls = this.fgDtls;
                            frmStockAdj.QueryString = strQry;
                            frmStockAdj.IsRefQuery = true;
                            frmStockAdj.ibitCol = ibitcol;
                            frmStockAdj.AsonDate = Conversions.ToDate(this.dtEntryDate.Text);
                            frmStockAdj.LedgerID = cboDepartment.SelectedValue.ToString();
                            frmStockAdj.UsedInGridArray = this.OrgInGridArray;
                            frmStockAdj.IsMultiSelect = false;
                            if (frmStockAdj.ShowDialog() == DialogResult.Cancel)
                            {
                                frmStockAdj.Dispose();
                                return;
                            }
                            frmStockAdj.Dispose();
                        }
                        try
                        {
                            fgDtls_f.Rows[0].Cells[5].Value = Localization.ParseNativeInt(fgDtls.Rows[0].Cells[5].Value.ToString());
                            fgDtls_f.Rows[0].Cells[6].Value = Localization.ParseNativeInt(fgDtls.Rows[0].Cells[6].Value.ToString());
                            fgDtls_f.Rows[0].Cells[7].Value = Localization.ParseNativeInt(fgDtls.Rows[0].Cells[7].Value.ToString());
                            fgDtls_f.Rows[0].Cells[8].Value = Localization.ParseNativeInt(fgDtls.Rows[0].Cells[8].Value.ToString());
                            fgDtls_f.Rows[0].Cells[9].Value = Localization.ParseNativeInt(fgDtls.Rows[0].Cells[9].Value.ToString());
                            fgDtls_f.Rows[0].Cells[2].Value = fgDtls.Rows[0].Cells[2].Value.ToString();
                            fgDtls_f.Rows[0].Cells[35].Value = fgDtls.Rows[0].Cells[35].Value.ToString();
                            fgDtls_f.Rows[0].Cells[36].Value = fgDtls.Rows[0].Cells[36].Value.ToString();
                            fgDtls_f.Rows[0].Cells[38].Value = fgDtls.Rows[0].Cells[38].Value.ToString();
                        }
                        catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }

                        fgDtls.Select();

                        try
                        {
                            decimal Weight = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select WtPerMtr From tbl_FabricDesignMaster Where FabricDesignID=" + Localization.ParseNativeInt(fgDtls.Rows[0].Cells[5].Value.ToString()) + "")));
                            if (fgDtls.Rows[0].Cells[12].Value == null || Localization.ParseNativeDecimal(fgDtls.Rows[0].Cells[12].Value.ToString()) == 0)
                            {
                                if (Weight != 0)
                                    fgDtls.Rows[0].Cells[12].Value = Weight;
                            }
                        }
                        catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                    }
                    else
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                        cboDepartment.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                }
            }
        }

        public void PrintRecord()
        {
            try
            {
                CIS_ReportTool.frmMultiPrint frmMultiPrint = new CIS_ReportTool.frmMultiPrint();
                CIS_ReportTool.frmMultiPrint.MenuID = base.iIDentity;
                CIS_ReportTool.frmMultiPrint.Id = Localization.ParseNativeInt(this.txtCode.Text);
                CIS_ReportTool.frmMultiPrint.TblNm = "tbl_FabricMergingMain";
                CIS_ReportTool.frmMultiPrint.TblNm_D = "tbl_FabricMergingFooter";
                CIS_ReportTool.frmMultiPrint.IdStr = "FabricMergeID";
                CIS_ReportTool.frmMultiPrint.iCompID = Db_Detials.CompID;
                CIS_ReportTool.frmMultiPrint.iYearID = Db_Detials.YearID;
                CIS_ReportTool.frmMultiPrint.iUserID = Db_Detials.UserID;
                CIS_ReportTool.frmMultiPrint.objReport = Db_Detials.objReport;
                CIS_ReportTool.frmMultiPrint.sApplicationName = GetAssemblyInfo.ProductName;

                if (frmMultiPrint.ShowDialog() == DialogResult.Cancel)
                {
                    frmMultiPrint.Dispose();
                }
                else
                {
                    frmMultiPrint = null;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void txtFentMtrs_TextChanged(object sender, EventArgs e)
        {
            CalcVal();
        }

        private void txtPiecesMtrs_TextChanged(object sender, EventArgs e)
        {
            CalcVal();
        }

        private void cboDepartment_SelectedValueChanged(object sender, EventArgs e)
        {
            EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
            EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
            EventHandles.CreateDefault_Rows(fgDtls_f, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
            EventHandles.CalculateFooter_Rows(fgDtls_f, fgDtls_f_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
        }

        private void txtSampleMtrs_TextChanged(object sender, EventArgs e)
        {
            CalcVal();
        }

        public void ExecuterTempQry(int RowIndex)
        {
            if (((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record)))
            {
                int StatusID = 1;
                string MyID = "";
                try
                {
                    if (txtUniqueID.Text != null && txtUniqueID.Text != "")
                    {
                        string strQry = "";
                        if (RowIndex == -1)
                        {
                            strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                            for (int i = 0; i <= fgDtls_f.Rows.Count - 1; i++)
                            {
                                DataGridViewRow row = fgDtls_f.Rows[i];
                                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                                {
                                    StatusID = 1;
                                    MyID = row.Cells[38].Value.ToString();
                                }
                                else
                                {
                                    StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockFabricLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockFabricLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + "")));
                                    MyID = txtCode.Text;
                                }

                                row.Cells[38].Value = fgDtls.Rows[0].Cells[38].Value;

                                decimal FabricDesign = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select WtPerMtr From tbl_FabricDesignMaster Where FabricDesignID=" + Localization.ParseNativeInt(fgDtls.Rows[0].Cells[3].Value.ToString()) + "")));
                                row.Cells[14].Value = Localization.ParseNativeDecimal(fgDtls.Rows[0].Cells[11].Value.ToString());
                                row.Cells[15].Value = Localization.ParseNativeDecimal(fgDtls.Rows[0].Cells[12].Value.ToString());
                                row.Cells[38].Value = fgDtls.Rows[0].Cells[38].Value.ToString();
                                if (fgDtls.Rows[0].Cells[12].Value == null || Localization.ParseNativeDecimal(fgDtls.Rows[0].Cells[12].Value.ToString()) == 0)
                                {
                                    if (FabricDesign != 0)
                                        fgDtls.Rows[0].Cells[12].Value = FabricDesign;
                                }
                                double Weight = 0;
                                if (row.Cells[14].Value != null && Localization.ParseNativeDecimal(row.Cells[14].Value.ToString()) > 0 && Localization.ParseNativeDecimal(row.Cells[14].Value.ToString()) != 0)
                                {
                                    if (row.Cells[15].Value == null || Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()) == 0)
                                    {
                                        Weight = (Localization.ParseNativeDouble(FabricDesign.ToString()) / Localization.ParseNativeDouble(row.Cells[14].Value.ToString())) * Localization.ParseNativeDouble(row.Cells[11].Value.ToString());
                                    }
                                    else
                                    {
                                        Weight = (Localization.ParseNativeDouble(row.Cells[15].Value.ToString()) / Localization.ParseNativeDouble(row.Cells[14].Value.ToString())) * Localization.ParseNativeDouble(row.Cells[11].Value.ToString());
                                    }
                                    row.Cells[12].Value = Math.Round(Weight, 3);
                                }


                                if (MyID != "" && row.Cells[3].Value != null && row.Cells[3].Value.ToString() != "" && row.Cells[10].Value != null && row.Cells[10].Value.ToString() != "" && row.Cells[11].Value != null && row.Cells[11].Value.ToString() != "0" && row.Cells[11].Value.ToString() != "")
                                {
                                    strQry = DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), MyID,
                                            (i + 1).ToString(), txtEntryNo.Text, dtEntryDate.Text, Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                                            row.Cells[17].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                            row.Cells[36].Value == null ? "NULL" : row.Cells[36].Value.ToString().Trim() == "" ? "NULL" : row.Cells[36].Value.ToString(),
                                            row.Cells[37].Value == null ? "NULL" : row.Cells[37].Value.ToString().Trim() == "" ? "NULL" : row.Cells[37].Value.ToString(),
                                            row.Cells[2].Value == null ? "-" : row.Cells[2].Value.ToString() == "0" ? "-" : row.Cells[2].Value.ToString() == "" ? "-" : row.Cells[2].Value.ToString(),
                                            row.Cells[3].Value.ToString(),
                                            row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                            row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                            row.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                            row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                            row.Cells[8].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                            row.Cells[9].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                            0, 0, 0,
                                            Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                            Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()),
                                            row.Cells[13].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()),
                                            "NULL",
                                            row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                            row.Cells[19].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[19].Value.ToString()),
                                            row.Cells[20].Value == null ? "NULL" : row.Cells[20].Value.ToString(),
                                            row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                            row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                            row.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                            row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                            row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                            row.Cells[27].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[27].Value.ToString()),
                                            row.Cells[28].Value == null || row.Cells[28].Value.ToString() == "" || row.Cells[28].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[28].Value.ToString()),
                                            row.Cells[29].Value == null || row.Cells[29].Value.ToString() == "" || row.Cells[29].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[29].Value.ToString()),
                                            row.Cells[30].Value == null || row.Cells[30].Value.ToString() == "" ? "-" : row.Cells[30].Value.ToString(),
                                            row.Cells[31].Value == null || row.Cells[31].Value.ToString() == "" ? "-" : row.Cells[31].Value.ToString(),
                                            row.Cells[32].Value == null || row.Cells[32].Value.ToString() == "" ? "-" : row.Cells[32].Value.ToString(),
                                            row.Cells[33].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[33].Value.ToString()),
                                            row.Cells[34].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[34].Value.ToString()),
                                            txtUniqueID.Text, i, StatusID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                                }
                            }
                        }
                        else
                        {
                            if ((fgDtls_f.CurrentCell.ColumnIndex == 10) || (fgDtls_f.CurrentCell.ColumnIndex == 11) || (fgDtls_f.CurrentCell.ColumnIndex == 12))
                            {
                                DataGridViewRow row = fgDtls_f.Rows[RowIndex];
                                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                                {
                                    StatusID = 1;
                                    MyID = row.Cells[38].Value.ToString();
                                }
                                else
                                {
                                    StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockFabricLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockFabricLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + "")));
                                    MyID = txtCode.Text;
                                }
                                if (fgDtls_f.Rows[RowIndex].Cells[3].Value != null && fgDtls_f.Rows[RowIndex].Cells[3].Value.ToString() != "" && fgDtls_f.Rows[RowIndex].Cells[10].Value != null && fgDtls_f.Rows[RowIndex].Cells[10].Value.ToString() != "" && fgDtls_f.Rows[RowIndex].Cells[11].Value != null && fgDtls_f.Rows[RowIndex].Cells[11].Value.ToString() != "" && fgDtls_f.Rows[RowIndex].Cells[11].Value.ToString() != "0" && fgDtls_f.Rows[RowIndex].Cells[12].Value != null && fgDtls_f.Rows[RowIndex].Cells[12].Value.ToString() != "")
                                {
                                    decimal FabricDesign = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select WtPerMtr From tbl_FabricDesignMaster Where FabricDesignID=" + Localization.ParseNativeInt(fgDtls_f.Rows[RowIndex].Cells[5].Value.ToString()) + "")));
                                    row.Cells[38].Value = fgDtls.Rows[0].Cells[38].Value.ToString();
                                    double Weight = 0;

                                    if (fgDtls.Rows[0].Cells[11].Value != null && Localization.ParseNativeDecimal(fgDtls.Rows[0].Cells[11].Value.ToString()) > 0 && Localization.ParseNativeDecimal(fgDtls.Rows[0].Cells[11].Value.ToString()) != 0)
                                    {
                                        if (fgDtls.Rows[0].Cells[12].Value == null || Localization.ParseNativeDecimal(fgDtls.Rows[0].Cells[12].Value.ToString()) == 0)
                                        {
                                            Weight = (Localization.ParseNativeDouble(FabricDesign.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[0].Cells[11].Value.ToString())) * Localization.ParseNativeDouble(fgDtls_f.Rows[RowIndex].Cells[11].Value.ToString());
                                        }
                                        else
                                        {
                                            Weight = (Localization.ParseNativeDouble(fgDtls.Rows[0].Cells[12].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[0].Cells[11].Value.ToString())) * Localization.ParseNativeDouble(fgDtls_f.Rows[RowIndex].Cells[11].Value.ToString());
                                        }
                                        row.Cells[12].Value = Math.Round(Weight, 3);
                                    }

                                    strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and RowIndex=" + RowIndex + " and AddedBy=" + Db_Detials.UserID + ";");
                                    strQry = DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), MyID,
                                            (RowIndex + 1).ToString(), txtEntryNo.Text, dtEntryDate.Text, Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                                            row.Cells[17].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                            row.Cells[36].Value == null ? "NULL" : row.Cells[36].Value.ToString().Trim() == "" ? "NULL" : row.Cells[36].Value.ToString(),
                                            row.Cells[37].Value == null ? "NULL" : row.Cells[37].Value.ToString().Trim() == "" ? "NULL" : row.Cells[37].Value.ToString(),
                                            row.Cells[2].Value == null ? "-" : row.Cells[2].Value.ToString() == "0" ? "-" : row.Cells[2].Value.ToString() == "" ? "-" : row.Cells[2].Value.ToString(),
                                            row.Cells[3].Value.ToString(),
                                            row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                            row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                            row.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                            row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                            row.Cells[8].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                            row.Cells[9].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                            0, 0, 0,
                                            Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                            Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()),
                                            row.Cells[13].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()),
                                            "NULL",
                                            row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                            row.Cells[19].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[19].Value.ToString()),
                                            row.Cells[20].Value == null ? "NULL" : row.Cells[20].Value.ToString(),
                                            row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                            row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                            row.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                            row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                            row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                            row.Cells[27].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[27].Value.ToString()),
                                            row.Cells[28].Value == null || row.Cells[28].Value.ToString() == "" || row.Cells[28].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[28].Value.ToString()),
                                            row.Cells[29].Value == null || row.Cells[29].Value.ToString() == "" || row.Cells[29].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[29].Value.ToString()),
                                            row.Cells[30].Value == null || row.Cells[30].Value.ToString() == "" ? "-" : row.Cells[30].Value.ToString(),
                                            row.Cells[31].Value == null || row.Cells[31].Value.ToString() == "" ? "-" : row.Cells[31].Value.ToString(),
                                            row.Cells[32].Value == null || row.Cells[32].Value.ToString() == "" ? "-" : row.Cells[32].Value.ToString(),
                                            row.Cells[33].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[33].Value.ToString()),
                                            row.Cells[34].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[34].Value.ToString()),
                                            txtUniqueID.Text, Localization.ParseNativeInt(fgDtls_f.Rows[fgDtls_f.CurrentRow.Index].Cells[39].Value.ToString()), StatusID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                                }
                            }
                        }
                        if (strQry != "" && strQry.Length > 0)
                        {
                            DB.ExecuteSQL(strQry);
                        }
                    }
                }
                catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
            }
        }

        private void GetRefModID()
        {
            #region Multiple series Mapping
            try
            {
                RefVoucherID = "";
                if (RefMenuID == 0)
                {
                    RefMenuID = iIDentity;
                }
                string sMappingID = DB.GetSnglValue(string.Format("Select MappingIDs from tbl_VoucherTypeMaster Where GenMenuID=" + iIDentity + ""));
                string[] arr = sMappingID.Split(',');
                for (int i = 0; i <= arr.Length - 1; i++)
                {
                    RefVoucherID += DB.GetSnglValue(string.Format("Select SeriesID From fn_MenuMaster_tbl() Where MenuID=" + arr[i] + "")) + ",";
                }
                RefVoucherID = RefVoucherID.Remove(RefVoucherID.Length - 1);
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
            #endregion
        }
    }
}
