using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_CLibrary;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmFabricPhysicalStock : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        public string strUniqueID;
        public ArrayList OrgInGridArray;
        private int RefMenuID;
        private static string RefVoucherID;
        public static bool FAB_MAINTAINWEIGHT;

        public frmFabricPhysicalStock()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
            OrgInGridArray = new ArrayList();
        }

        #region Event
        private void frmFabricPhysicalStock_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboDepartment, Combobox_Setup.ComboType.Mst_Department, "");
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                txtEntryNo.Enabled = false;
                FAB_MAINTAINWEIGHT = Localization.ParseBoolean(GlobalVariables.FAB_MAINTAINWEIGHT);
                RefMenuID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MenuID from tbl_VoucherTypeMaster Where GenMenuID=" + base.iIDentity + "")));
                GetRefModID();
                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls.KeyDown += new KeyEventHandler(this.fgDtls_KeyDown);
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
                CIS_Textbox txtEntryNo = this.txtEntryNo;
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                int MaxID = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(FabPhyStockID),0) From {0}  Where StoreID = {1} and CompID = {2} and BranchID = {3} and YearID = {4}", "tbl_FabricPhysicalStockMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID))));
                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where FabPhyStockID = {1} and StoreID={2} and CompID={3} and BranchID={4} and YearID={5}", new object[] { "tbl_FabricPhysicalStockMain", MaxID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                        cboDepartment.SelectedValue = Localization.ParseNativeInt(reader["DepartmentID"].ToString());
                    }
                }
                dtEntryDate.Focus();
                txtUniqueID.Text = CommonCls.GenUniqueID();
                strUniqueID = txtUniqueID.Text;
                AplySelectBtnEnbl();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public void AplySelectBtnEnbl()
        {
            if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
            {
                this.btnSelect.Enabled = true;
            }
            else
            {
                this.btnSelect.Enabled = false;
            }
        }

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "FabPhyStockID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboDepartment, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDesc, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtUniqueID, "UniqueID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtED1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);
                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "FabPhyStockID", txtCode.Text, base.dt_HasDtls_Grd);
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
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityShieldBlue, "This Record Is Edited By Another User...", "Warning");
                        }
                    }
                    catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                }

                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    if (strUniqueID != null)
                    {
                        string strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                        strQry = strQry + string.Format("Update  tbl_StockFabricLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                        DB.ExecuteSQL(strQry);
                        strQry = string.Format("Update tbl_StockFabricLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                        DB.ExecuteSQL(strQry);
                    }
                }

                System.Windows.Forms.DataGridViewCellStyle dgvCellStyle_Ref = new System.Windows.Forms.DataGridViewCellStyle();
                dgvCellStyle_Ref.BackColor = System.Drawing.Color.LightSteelBlue;
                dgvCellStyle_Ref.ForeColor = System.Drawing.SystemColors.WindowText;
                dgvCellStyle_Ref.SelectionBackColor = System.Drawing.Color.SteelBlue;
                dgvCellStyle_Ref.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
                try
                {
                    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    {
                        if (icount > 0)
                        {
                            btnSelect.Enabled = false;
                            fgDtls.Rows[i].ReadOnly = true;
                            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle_Ref;
                        }
                        else
                        {
                            btnSelect.Enabled = true;
                            fgDtls.Rows[i].ReadOnly = false;
                        }
                    }
                }
                catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                AplySelectBtnEnbl();
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
                    this.frmVoucherTypeID,
                    "(#ENTRYNO#)",
                    dtEntryDate.TextFormat(false, true),
                    cboDepartment.SelectedValue,
                    txtDesc.Text.ToString() == ""? "-": txtDesc.Text.ToString(),
                    txtUniqueID.Text,
                    (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                    (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                    (dtED1.TextFormat(false, true)),
                    (txtET1.Text.Trim()),
                    (txtET2.Text.Trim()),
                    (txtET3.Text.Trim())
                };

                int ShortationID = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format("Select LedgerId From {0} Where LedgerName = 'SHORTAGE ACCOUNT' ", "tbl_LedgerMaster"))));
                int LongetionID = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format("Select LedgerId From {0} Where LedgerName = 'EXCESS ACCOUNT' ", "tbl_LedgerMaster"))));
                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", "(#CodeID#)", base.iIDentity);

                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    if (row.Cells[12].Value != null)
                    {
                        decimal AdjMtrs = 0;
                        decimal AdjPcs = 0;
                        decimal AdjWts = 0;

                        if (Localization.ParseNativeDouble(row.Cells[12].Value.ToString()) != 0)
                        {
                            AdjPcs = Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()) - Localization.ParseNativeDecimal(row.Cells[11].Value.ToString());
                            AdjMtrs = Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()) - Localization.ParseNativeDecimal(row.Cells[13].Value.ToString());
                            AdjWts = Localization.ParseNativeDecimal(row.Cells[14].Value.ToString()) - Localization.ParseNativeDecimal(row.Cells[15].Value.ToString());
                        }

                        if (AdjMtrs < 0)
                        {
                            AdjMtrs = (AdjMtrs * -1);
                            strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                                (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                                row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                row.Cells[40].Value == null ? "NULL" : row.Cells[40].Value.ToString().Trim() == "" ? "NULL" : row.Cells[40].Value.ToString(),
                                row.Cells[41].Value == null ? "NULL" : row.Cells[41].Value.ToString().Trim() == "" ? "NULL" : row.Cells[41].Value.ToString(),
                                row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(),
                                row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                row.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                row.Cells[8].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                row.Cells[9].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                Localization.ParseNativeDecimal(Conversions.ToString(Interaction.IIf(decimal.Compare(AdjPcs, decimal.Zero) < 0, decimal.Multiply(AdjPcs, decimal.MinusOne), "0"))),
                                Localization.ParseNativeDecimal(Conversions.ToString(AdjMtrs)),
                                Localization.ParseNativeDecimal(Conversions.ToString(Interaction.IIf(decimal.Compare(AdjMtrs, decimal.Zero) < 0, decimal.Multiply(AdjWts, decimal.MinusOne), "0"))),
                                Localization.ParseNativeDecimal(Conversions.ToString(Interaction.IIf(decimal.Compare(AdjPcs, decimal.Zero) > 0, AdjPcs, "0"))), decimal.Zero, decimal.Zero,
                                row.Cells[16].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                row.Cells[17].Value == null ? "NULL" : (row.Cells[17].Value.ToString() == "" ? "NULL" : row.Cells[17].Value.ToString()),
                                row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                row.Cells[24].Value == null ? "NULL" : row.Cells[24].Value.ToString(),
                                row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                row.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[29].Value.ToString()),
                                row.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[30].Value.ToString()),
                                row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                row.Cells[32].Value == null || row.Cells[32].Value.ToString() == "" || row.Cells[32].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[32].Value.ToString()),
                                row.Cells[33].Value == null || row.Cells[33].Value.ToString() == "" || row.Cells[33].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[33].Value.ToString()),
                                row.Cells[34].Value == null || row.Cells[34].Value.ToString() == "" ? "-" : row.Cells[34].Value.ToString(),
                                row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" ? "-" : row.Cells[35].Value.ToString(),
                                row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" ? "-" : row.Cells[36].Value.ToString(),
                                row.Cells[37].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[37].Value.ToString()),
                                row.Cells[38].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[38].Value.ToString()),
                                "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);


                            strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                                (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(LongetionID.ToString()),
                                row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                row.Cells[40].Value == null ? "NULL" : row.Cells[40].Value.ToString().Trim() == "" ? "NULL" : row.Cells[40].Value.ToString(),
                                row.Cells[41].Value == null ? "NULL" : row.Cells[41].Value.ToString().Trim() == "" ? "NULL" : row.Cells[41].Value.ToString(),
                                row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(),
                                row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                row.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                row.Cells[8].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                row.Cells[9].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                Localization.ParseNativeDecimal(Conversions.ToString(Interaction.IIf(decimal.Compare(AdjPcs, decimal.Zero) < 0, decimal.Multiply(AdjPcs, decimal.MinusOne), "0"))),
                                decimal.Zero, decimal.Zero, Localization.ParseNativeDecimal(Conversions.ToString(Interaction.IIf(decimal.Compare(AdjPcs, decimal.Zero) > 0, AdjPcs, "0"))),
                                Localization.ParseNativeDecimal(Conversions.ToString(AdjMtrs)), 
                                Localization.ParseNativeDecimal(Conversions.ToString(Interaction.IIf(decimal.Compare(AdjWts, decimal.Zero) < 0, decimal.Multiply(AdjWts, decimal.MinusOne), "0"))),
                                row.Cells[16].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                row.Cells[17].Value == null ? "NULL" : (row.Cells[17].Value.ToString() == "" ? "NULL" : row.Cells[17].Value.ToString()),
                                row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                row.Cells[24].Value == null ? "NULL" : row.Cells[24].Value.ToString(),
                                row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                row.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[29].Value.ToString()),
                                row.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[30].Value.ToString()),
                                row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                row.Cells[32].Value == null || row.Cells[32].Value.ToString() == "" || row.Cells[32].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[32].Value.ToString()),
                                row.Cells[33].Value == null || row.Cells[33].Value.ToString() == "" || row.Cells[33].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[33].Value.ToString()),
                                row.Cells[34].Value == null || row.Cells[34].Value.ToString() == "" ? "-" : row.Cells[34].Value.ToString(),
                                row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" ? "-" : row.Cells[35].Value.ToString(),
                                row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" ? "-" : row.Cells[36].Value.ToString(),
                                row.Cells[37].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[37].Value.ToString()),
                                row.Cells[38].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[38].Value.ToString()),
                                "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                        }
                        else if (AdjMtrs > 0)
                        {
                            strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                                (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                                row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                row.Cells[40].Value == null ? "NULL" : row.Cells[40].Value.ToString().Trim() == "" ? "NULL" : row.Cells[40].Value.ToString(),
                                row.Cells[41].Value == null ? "NULL" : row.Cells[41].Value.ToString().Trim() == "" ? "NULL" : row.Cells[41].Value.ToString(),
                                row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(),
                                row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                row.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                row.Cells[8].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                row.Cells[9].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                Localization.ParseNativeDecimal(Conversions.ToString(Interaction.IIf(decimal.Compare(AdjPcs, decimal.Zero) > 0, AdjPcs, "0"))),
                                Localization.ParseNativeDecimal(Conversions.ToString(AdjMtrs)), Localization.ParseNativeDecimal(Conversions.ToString(Interaction.IIf(decimal.Compare(AdjWts, decimal.Zero) < 0, decimal.Multiply(AdjWts, decimal.MinusOne), "0"))),
                                Localization.ParseNativeDecimal(Conversions.ToString(Interaction.IIf(decimal.Compare(AdjPcs, decimal.Zero) < 0, decimal.Multiply(AdjPcs, decimal.MinusOne), "0"))), decimal.Zero, decimal.Zero, 
                                row.Cells[16].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                row.Cells[17].Value == null ? "NULL" : (row.Cells[17].Value.ToString() == "" ? "NULL" : row.Cells[17].Value.ToString()),
                                row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                row.Cells[24].Value == null ? "NULL" : row.Cells[24].Value.ToString(),
                                row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                row.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[29].Value.ToString()),
                                row.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[30].Value.ToString()),
                                row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                row.Cells[32].Value == null || row.Cells[32].Value.ToString() == "" || row.Cells[32].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[32].Value.ToString()),
                                row.Cells[33].Value == null || row.Cells[33].Value.ToString() == "" || row.Cells[33].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[33].Value.ToString()),
                                row.Cells[34].Value == null || row.Cells[34].Value.ToString() == "" ? "-" : row.Cells[34].Value.ToString(),
                                row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" ? "-" : row.Cells[35].Value.ToString(),
                                row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" ? "-" : row.Cells[36].Value.ToString(),
                                row.Cells[37].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[37].Value.ToString()),
                                row.Cells[38].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[38].Value.ToString()),
                                "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);


                            strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                                (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(LongetionID.ToString()),
                                row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                row.Cells[40].Value == null ? "NULL" : row.Cells[40].Value.ToString().Trim() == "" ? "NULL" : row.Cells[40].Value.ToString(),
                                row.Cells[41].Value == null ? "NULL" : row.Cells[41].Value.ToString().Trim() == "" ? "NULL" : row.Cells[41].Value.ToString(),
                                row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(),
                                row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                row.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                row.Cells[8].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                row.Cells[9].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                Localization.ParseNativeDecimal(Conversions.ToString(Interaction.IIf(decimal.Compare(AdjPcs, decimal.Zero) < 0, decimal.Multiply(AdjPcs, decimal.MinusOne), "0"))), decimal.Zero, decimal.Zero, 
                                Localization.ParseNativeDecimal(Conversions.ToString(Interaction.IIf(decimal.Compare(AdjPcs, decimal.Zero) > 0, AdjPcs, "0"))),
                                Localization.ParseNativeDecimal(Conversions.ToString(AdjMtrs)), 
                                Localization.ParseNativeDecimal(Conversions.ToString(Interaction.IIf(decimal.Compare(AdjWts, decimal.Zero) < 0, decimal.Multiply(AdjWts, decimal.MinusOne), "0"))),
                                row.Cells[16].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                row.Cells[17].Value == null ? "NULL" : (row.Cells[17].Value.ToString() == "" ? "NULL" : row.Cells[17].Value.ToString()),
                                row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                row.Cells[24].Value == null ? "NULL" : row.Cells[24].Value.ToString(),
                                row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                row.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[29].Value.ToString()),
                                row.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[30].Value.ToString()),
                                row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                row.Cells[32].Value == null || row.Cells[32].Value.ToString() == "" || row.Cells[32].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[32].Value.ToString()),
                                row.Cells[33].Value == null || row.Cells[33].Value.ToString() == "" || row.Cells[33].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[33].Value.ToString()),
                                row.Cells[34].Value == null || row.Cells[34].Value.ToString() == "" ? "-" : row.Cells[34].Value.ToString(),
                                row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" ? "-" : row.Cells[35].Value.ToString(),
                                row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" ? "-" : row.Cells[36].Value.ToString(),
                                row.Cells[37].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[37].Value.ToString()),
                                row.Cells[38].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[38].Value.ToString()),
                                "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                        }
                        else if (AdjMtrs == 0 && AdjPcs != 0)
                        {
                            strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                                (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                                row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                row.Cells[40].Value == null ? "NULL" : row.Cells[40].Value.ToString().Trim() == "" ? "NULL" : row.Cells[40].Value.ToString(),
                                row.Cells[41].Value == null ? "NULL" : row.Cells[41].Value.ToString().Trim() == "" ? "NULL" : row.Cells[41].Value.ToString(),
                                row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(),
                                row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                row.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                row.Cells[8].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                row.Cells[9].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                Localization.ParseNativeDecimal(Conversions.ToString(Interaction.IIf(decimal.Compare(AdjPcs, decimal.Zero) > 0, AdjPcs, "0"))),
                                Localization.ParseNativeDecimal(Conversions.ToString(AdjMtrs)), 
                                Localization.ParseNativeDecimal(Conversions.ToString(Interaction.IIf(decimal.Compare(AdjWts, decimal.Zero) < 0, decimal.Multiply(AdjWts, decimal.MinusOne), "0"))),
                                Localization.ParseNativeDecimal(Conversions.ToString(Interaction.IIf(decimal.Compare(AdjPcs, decimal.Zero) < 0, decimal.Multiply(AdjPcs, decimal.MinusOne), "0"))), decimal.Zero, decimal.Zero,
                                row.Cells[16].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                row.Cells[17].Value == null ? "NULL" : (row.Cells[17].Value.ToString() == "" ? "NULL" : row.Cells[17].Value.ToString()),
                                row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                row.Cells[24].Value == null ? "NULL" : row.Cells[24].Value.ToString(),
                                row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                row.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[29].Value.ToString()),
                                row.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[30].Value.ToString()),
                                row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                row.Cells[32].Value == null || row.Cells[32].Value.ToString() == "" || row.Cells[32].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[32].Value.ToString()),
                                row.Cells[33].Value == null || row.Cells[33].Value.ToString() == "" || row.Cells[33].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[33].Value.ToString()),
                                row.Cells[34].Value == null || row.Cells[34].Value.ToString() == "" ? "-" : row.Cells[34].Value.ToString(),
                                row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" ? "-" : row.Cells[35].Value.ToString(),
                                row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" ? "-" : row.Cells[36].Value.ToString(),
                                row.Cells[37].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[37].Value.ToString()),
                                row.Cells[38].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[38].Value.ToString()),
                                "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);


                            strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                                (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(LongetionID.ToString()),
                                row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                row.Cells[40].Value == null ? "NULL" : row.Cells[40].Value.ToString().Trim() == "" ? "NULL" : row.Cells[40].Value.ToString(),
                                row.Cells[41].Value == null ? "NULL" : row.Cells[41].Value.ToString().Trim() == "" ? "NULL" : row.Cells[41].Value.ToString(),
                                row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(),
                                row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                row.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                row.Cells[8].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                row.Cells[9].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                Localization.ParseNativeDecimal(Conversions.ToString(Interaction.IIf(decimal.Compare(AdjPcs, decimal.Zero) < 0, decimal.Multiply(AdjPcs, decimal.MinusOne), "0"))), decimal.Zero, decimal.Zero, 
                                Localization.ParseNativeDecimal(Conversions.ToString(Interaction.IIf(decimal.Compare(AdjPcs, decimal.Zero) > 0, AdjPcs, "0"))),
                                Localization.ParseNativeDecimal(Conversions.ToString(AdjMtrs)), 
                                Localization.ParseNativeDecimal(Conversions.ToString(Interaction.IIf(decimal.Compare(AdjWts, decimal.Zero) < 0, decimal.Multiply(AdjWts, decimal.MinusOne), "0"))), 
                                row.Cells[16].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                row.Cells[17].Value == null ? "NULL" : (row.Cells[17].Value.ToString() == "" ? "NULL" : row.Cells[17].Value.ToString()),
                                row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                row.Cells[24].Value == null ? "NULL" : row.Cells[24].Value.ToString(),
                                row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                row.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[29].Value.ToString()),
                                row.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[30].Value.ToString()),
                                row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                row.Cells[32].Value == null || row.Cells[32].Value.ToString() == "" || row.Cells[32].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[32].Value.ToString()),
                                row.Cells[33].Value == null || row.Cells[33].Value.ToString() == "" || row.Cells[33].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[33].Value.ToString()),
                                row.Cells[34].Value == null || row.Cells[34].Value.ToString() == "" ? "-" : row.Cells[34].Value.ToString(),
                                row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" ? "-" : row.Cells[35].Value.ToString(),
                                row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" ? "-" : row.Cells[36].Value.ToString(),
                                row.Cells[37].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[37].Value.ToString()),
                                row.Cells[38].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[38].Value.ToString()),
                                "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                        }
                    }
                    row = null;
                }
                strAdjQry += "Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";";
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                DBSp.Transcation_AddEdit(pArrayData, fgDtls, true, strAdjQry, "", txtEntryNo.Text, "", "");
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        #endregion

        public bool ValidateForm()
        {
            try
            {
                if (!EventHandles.IsValidGridReq(this.fgDtls, base.dt_AryIsRequired))
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
                if (FAB_MAINTAINWEIGHT)
                {
                    for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                    {
                        if (fgDtls.Rows[i].Cells[14].Value == null || Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[14].Value.ToString()) == 0 || fgDtls.Rows[i].Cells[14].Value.ToString() == "")
                        {
                            fgDtls.CurrentCell = fgDtls[14, i];
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Weight");
                            fgDtls.Rows.Add();
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

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record)))
            {
                try
                {
                    if (cboDepartment.Text.Trim().ToString() != "-" || cboDepartment.SelectedValue != null)
                    {
                        if (this.dtEntryDate.Text != "__/__/____")
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
                            frmStockAdj.UsedInGridArray = OrgInGridArray;
                            if (frmStockAdj.ShowDialog() == DialogResult.Cancel)
                            {
                                frmStockAdj.Dispose();
                                return;
                            }
                            frmStockAdj.Dispose();
                        }
                        fgDtls.Select();
                        ExecuterTempQry(-1);
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

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (((e.ColumnIndex == 11) | (e.ColumnIndex == 13)) | (e.ColumnIndex == 15))
                {
                    ExecuterTempQry(e.RowIndex);
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public void ExecuterTempQry(int RowIndex)
        {
            if (((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record)))
            {
                try
                {
                    int StatusID = 1;
                    string MyID = "";
                    if (txtUniqueID.Text != null && txtUniqueID.Text != "")
                    {
                        string strQry = "";
                        if (RowIndex == -1)
                        {
                            strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                            {
                                DataGridViewRow row = fgDtls.Rows[i];
                                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                                {
                                    StatusID = 1;
                                    MyID = row.Cells[42].Value.ToString();
                                }
                                else
                                {
                                    StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockFabricLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockFabricLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + "")));
                                    MyID = txtCode.Text;
                                }
                                double Weight = 0;
                                decimal FabricDesign = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select WtPerMtr From tbl_FabricDesignMaster Where FabricDesignID=" + Localization.ParseNativeInt(fgDtls.Rows[i].Cells[5].Value.ToString()) + "")));
                                row.Cells[18].Value = Localization.ParseNativeDecimal(row.Cells[12].Value.ToString());
                                row.Cells[19].Value = Localization.ParseNativeDecimal(row.Cells[14].Value.ToString());
                                if (row.Cells[19].Value == null || Localization.ParseNativeDecimal(row.Cells[19].Value.ToString()) == 0)
                                {
                                    if (FabricDesign != 0)
                                        row.Cells[19].Value = FabricDesign;
                                }
                                if (row.Cells[18].Value != null && Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()) > 0 && Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()) != 0)
                                {
                                    if (row.Cells[19].Value == null || Localization.ParseNativeDecimal(row.Cells[19].Value.ToString()) == 0)
                                    {
                                        Weight = (Localization.ParseNativeDouble(FabricDesign.ToString()) / Localization.ParseNativeDouble(row.Cells[18].Value.ToString())) * Localization.ParseNativeDouble(row.Cells[12].Value.ToString());
                                    }
                                    else
                                    {
                                        Weight = (Localization.ParseNativeDouble(row.Cells[19].Value.ToString()) / Localization.ParseNativeDouble(row.Cells[18].Value.ToString())) * Localization.ParseNativeDouble(row.Cells[12].Value.ToString());
                                    }
                                    row.Cells[14].Value = Math.Round(Weight, 3);
                                }
                                string BatchNo;
                                if (row.Cells[2].Value != null && row.Cells[2].Value.ToString().Length > 0)
                                {
                                    BatchNo = row.Cells[2].Value.ToString();
                                }
                                else
                                {
                                    BatchNo = "-";
                                }
                                if (MyID != "" && row.Cells[12].Value != null && row.Cells[12].Value.ToString() != "" && row.Cells[14].Value != null && row.Cells[14].Value.ToString() != "")
                                {
                                    strQry += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                                            (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(cboDepartment.ToString()),
                                            row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                            row.Cells[40].Value == null ? "NULL" : row.Cells[40].Value.ToString().Trim() == "" ? "NULL" : row.Cells[40].Value.ToString(),
                                            row.Cells[41].Value == null ? "NULL" : row.Cells[41].Value.ToString().Trim() == "" ? "NULL" : row.Cells[41].Value.ToString(),
                                            row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(),
                                            row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                            row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                            row.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                            row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                            row.Cells[8].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                            row.Cells[9].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                            0, 0, 0,
                                            Localization.ParseNativeDecimal(Conversions.ToString(row.Cells[10].Value)),
                                            Localization.ParseNativeDecimal(Conversions.ToString(row.Cells[12].Value)),
                                            Localization.ParseNativeDecimal(Conversions.ToString(row.Cells[14].Value)),
                                            row.Cells[16].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                            row.Cells[17].Value == null ? "NULL" : (row.Cells[17].Value.ToString() == "" ? "NULL" : row.Cells[17].Value.ToString()),
                                            row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                            row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                            row.Cells[24].Value == null ? "NULL" : row.Cells[24].Value.ToString(),
                                            row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                            row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                            row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                            row.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[29].Value.ToString()),
                                            row.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[30].Value.ToString()),
                                            row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                            row.Cells[32].Value == null || row.Cells[32].Value.ToString() == "" || row.Cells[32].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[32].Value.ToString()),
                                            row.Cells[33].Value == null || row.Cells[33].Value.ToString() == "" || row.Cells[33].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[33].Value.ToString()),
                                            row.Cells[34].Value == null || row.Cells[34].Value.ToString() == "" ? "-" : row.Cells[34].Value.ToString(),
                                            row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" ? "-" : row.Cells[35].Value.ToString(),
                                            row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" ? "-" : row.Cells[36].Value.ToString(),
                                            row.Cells[37].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[37].Value.ToString()),
                                            row.Cells[38].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[38].Value.ToString()),
                                            txtUniqueID.Text, i, StatusID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                                }
                            }
                        }
                        else
                        {
                            if ((RowIndex == 12) || (RowIndex == 14) || (RowIndex == 11))
                            {
                                DataGridViewRow row = fgDtls.Rows[RowIndex];
                                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                                {
                                    StatusID = 1;
                                    MyID = row.Cells[42].Value.ToString();
                                }
                                else
                                {
                                    StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockFabricLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockFabricLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + "")));
                                    MyID = txtCode.Text;
                                }

                                decimal FabricDesign = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select WtPerMtr From tbl_FabricDesignMaster Where FabricDesignID=" + Localization.ParseNativeInt(fgDtls.Rows[RowIndex].Cells[5].Value.ToString()) + "")));
                                double Weight = 0;
                                if (row.Cells[18].Value != null && Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()) > 0 && Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()) != 0)
                                {
                                    if (row.Cells[19].Value == null || Localization.ParseNativeDecimal(row.Cells[19].Value.ToString()) == 0)
                                    {
                                        Weight = (Localization.ParseNativeDouble(FabricDesign.ToString()) / Localization.ParseNativeDouble(row.Cells[18].Value.ToString())) * Localization.ParseNativeDouble(row.Cells[12].Value.ToString());
                                    }
                                    else
                                    {
                                        Weight = (Localization.ParseNativeDouble(row.Cells[19].Value.ToString()) / Localization.ParseNativeDouble(row.Cells[18].Value.ToString())) * Localization.ParseNativeDouble(row.Cells[12].Value.ToString());
                                    }
                                    row.Cells[14].Value = Math.Round(Weight, 3);
                                }

                                string BatchNo;
                                if (row.Cells[2].Value != null && row.Cells[2].Value.ToString().Length > 0)
                                {
                                    BatchNo = row.Cells[2].Value.ToString();
                                }
                                else
                                {
                                    BatchNo = "-";
                                }

                                if (MyID != "" && row.Cells[12].Value != null && row.Cells[12].Value.ToString() != "" && row.Cells[14].Value != null && row.Cells[14].Value.ToString() != "")
                                {
                                    strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and RowIndex=" + RowIndex + " and AddedBy=" + Db_Detials.UserID + ";");
                                    strQry += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), MyID,
                                            (RowIndex + 1).ToString(), txtEntryNo.Text, dtEntryDate.Text, Localization.ParseNativeDouble(cboDepartment.ToString()),
                                            row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                            row.Cells[40].Value == null ? "NULL" : row.Cells[40].Value.ToString().Trim() == "" ? "NULL" : row.Cells[40].Value.ToString(),
                                            row.Cells[41].Value == null ? "NULL" : row.Cells[41].Value.ToString().Trim() == "" ? "NULL" : row.Cells[41].Value.ToString(),
                                            row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString(),
                                            row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                            row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                            row.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                            row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                            row.Cells[8].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                            row.Cells[9].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                            0, 0, 0,
                                            Localization.ParseNativeDecimal(Conversions.ToString(row.Cells[10].Value)),
                                            Localization.ParseNativeDecimal(Conversions.ToString(row.Cells[12].Value)),
                                            Localization.ParseNativeDecimal(Conversions.ToString(row.Cells[14].Value)),
                                            row.Cells[16].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                            row.Cells[17].Value == null ? "NULL" : (row.Cells[17].Value.ToString() == "" ? "NULL" : row.Cells[17].Value.ToString()),
                                            row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                            row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                            row.Cells[24].Value == null ? "NULL" : row.Cells[24].Value.ToString(),
                                            row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                            row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                            row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                            row.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[29].Value.ToString()),
                                            row.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[30].Value.ToString()),
                                            row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                            row.Cells[32].Value == null || row.Cells[32].Value.ToString() == "" || row.Cells[32].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[32].Value.ToString()),
                                            row.Cells[33].Value == null || row.Cells[33].Value.ToString() == "" || row.Cells[33].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[33].Value.ToString()),
                                            row.Cells[34].Value == null || row.Cells[34].Value.ToString() == "" ? "-" : row.Cells[34].Value.ToString(),
                                            row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" ? "-" : row.Cells[35].Value.ToString(),
                                            row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" ? "-" : row.Cells[36].Value.ToString(),
                                            row.Cells[37].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[37].Value.ToString()),
                                            row.Cells[38].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[38].Value.ToString()),
                                            txtUniqueID.Text, Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[43].Value.ToString()), StatusID,
                                            Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
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

        protected void fgDtls_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                if ((e.Control == true & e.KeyCode == Keys.D) | e.KeyCode == Keys.F5)
                {
                    object frm = Navigate.GetActiveChild();
                    dynamic frmObj = frm;
                    int iCalcCol = 0;
                    CIS_DataGridViewEx.DataGridViewEx fgDtls = (CIS_DataGridViewEx.DataGridViewEx)sender;

                    if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        try
                        {
                            if ((Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select COUNT(0) From fn_StockFabricLedger_Tbl() Where RefId in (Select RefID From tbl_FabricPackingReceiptFooter Where ARefID='" + fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[39].Value + "') and RefID<>'' and TransType<>" + iIDentity + ""))) > 0))
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityWarning, "", "Reference Found In Another Module..Row Cannot Be Deleted");
                                return;
                            }
                            else
                            {
                                fgDtls.Rows.RemoveAt(fgDtls.CurrentRow.Index);
                                DataTable AryCalcvalue = ((DataTable)frmObj.dt_AryCalcvalue);
                                DataRow[] dtRow_AryCalcvalue = AryCalcvalue.Select("SubGridID = " + fgDtls.Grid_UID);

                                for (int i = 0; i <= (dtRow_AryCalcvalue.Length - 1); i++)
                                {
                                    string[] strValue = dtRow_AryCalcvalue[i]["ColCalcValue"].ToString().Split(',');
                                    string strColInsert = string.Empty;
                                    for (int j = 0; j <= (strValue.Length - 1); j++)
                                    {
                                        if (j == (strValue.Length - 1))
                                        {
                                            if (strValue[j].ToString() != "+")
                                                break; // TODO: might not be correct. Was : Exit For
                                            iCalcCol = Localization.ParseNativeInt(dtRow_AryCalcvalue[i]["ColIndex"].ToString());
                                        }
                                    }
                                }
                                if (iCalcCol != -1)
                                {
                                    for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                                    {
                                        fgDtls.Rows[i].Cells[iCalcCol].Value = i + 1;
                                    }
                                }
                                DataTable table2 = (DataTable)frmObj.dt_HasDtls_Grd;
                                DataTable table3 = (DataTable)frmObj.dt_AryCalcvalue;
                                DataTable table4 = (DataTable)frmObj.dt_AryIsRequired;

                                if (fgDtls.RowCount == 0)
                                {
                                    EventHandles.CreateDefault_Rows(fgDtls, table2, table3, table4, false, false);
                                }
                                else
                                {
                                    EventHandles.CreateDefault_Rows(fgDtls, table2, table3, table4, true, false);
                                }
                                try
                                {
                                    string strQry = string.Format("Update tbl_StockFabricLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + fgDtls.CurrentRow.Index + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                                    DB.ExecuteSQL(strQry);
                                }
                                catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                            }
                        }
                        catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                    }
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        try
                        {
                            fgDtls.Rows.RemoveAt(fgDtls.CurrentRow.Index);
                            DataTable AryCalcvalue = ((DataTable)frmObj.dt_AryCalcvalue);
                            DataRow[] dtRow_AryCalcvalue = AryCalcvalue.Select("SubGridID = " + fgDtls.Grid_UID);

                            for (int i = 0; i <= (dtRow_AryCalcvalue.Length - 1); i++)
                            {
                                string[] strValue = dtRow_AryCalcvalue[i]["ColCalcValue"].ToString().Split(',');
                                string strColInsert = string.Empty;
                                for (int j = 0; j <= (strValue.Length - 1); j++)
                                {
                                    if (j == (strValue.Length - 1))
                                    {
                                        if (strValue[j].ToString() != "+")
                                            break; // TODO: might not be correct. Was : Exit For
                                        iCalcCol = Localization.ParseNativeInt(dtRow_AryCalcvalue[i]["ColIndex"].ToString());
                                    }
                                }
                            }

                            if (iCalcCol != -1)
                            {
                                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                                {
                                    fgDtls.Rows[i].Cells[iCalcCol].Value = i + 1;
                                }
                            }

                            DataTable table2 = (DataTable)frmObj.dt_HasDtls_Grd;
                            DataTable table3 = (DataTable)frmObj.dt_AryCalcvalue;
                            DataTable table4 = (DataTable)frmObj.dt_AryIsRequired;
                            if (fgDtls.RowCount == 0)
                            {
                                EventHandles.CreateDefault_Rows(fgDtls, table2, table3, table4, false, false);
                            }
                            else
                            {
                                EventHandles.CreateDefault_Rows(fgDtls, table2, table3, table4, true, false);
                            }
                            try
                            {
                                string strQry = string.Format("Update tbl_StockFabricLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + fgDtls.CurrentRow.Index + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                                DB.ExecuteSQL(strQry);
                            }
                            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                        }
                        catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
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
