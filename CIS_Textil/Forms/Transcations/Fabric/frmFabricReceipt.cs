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
using CIS_Textil;

namespace CIS_Textil
{
    public partial class frmFabricReceipt : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        ArrayList OrgInGridArray = new ArrayList();
        public bool NewPieceNo;
        private bool Vld_DupPieceNo;
        public static bool FAB_MAINTAINWEIGHT;

        public string strUniqueID;
        private int RefMenuID;
        private static string RefVoucherID;
        private int iMaxMyID_Stock;

        public frmFabricReceipt()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
        }

        #region Event

        private void frmFabricReceipt_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboProcessType, Combobox_Setup.ComboType.Mst_FabricProcessType, "");
                Combobox_Setup.FillCbo(ref cboProcesser, Combobox_Setup.ComboType.Mst_Dyer, "");
                Combobox_Setup.FillCbo(ref cboBroker, Combobox_Setup.ComboType.Mst_Brokers, "");
                Combobox_Setup.FillCbo(ref cboToDepartment, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
                Combobox_Setup.FillCbo(ref cboAcType, Combobox_Setup.ComboType.Mst_Ledger, "");
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                NewPieceNo = Localization.ParseBoolean(GlobalVariables.FR_N_PNo);
                Vld_DupPieceNo = Localization.ParseBoolean(GlobalVariables.Vld_DupPieceNo);
                FAB_MAINTAINWEIGHT = Localization.ParseBoolean(GlobalVariables.FAB_MAINTAINWEIGHT);
                txtEntryNo.Enabled = false;
                RefMenuID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MenuID from tbl_VoucherTypeMaster Where GenMenuID=" + base.iIDentity + "")));
                GetRefModID();
                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls.CellEndEdit += new DataGridViewCellEventHandler(this.fgDtls_CellEndEdit);
                this.fgDtls.KeyDown += new KeyEventHandler(fgDtls_KeyDown);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #endregion

        #region Navigation

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "FabReceiptID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtRefNo, "RefNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtRefDate, "RefDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboProcessType, "ProcessTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboProcesser, "ProcesserID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboToDepartment, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLrNo, "LrNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtLrDate, "LrDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtVehicleNo, "VehicleNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboAcType, "ProcessAcID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtED1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);
                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "FabReceiptID", txtCode.Text, base.dt_HasDtls_Grd, this.iIDentity, GlobalVariables.VALIDATE_EDIT, Db_Detials.CompID.ToString(), 1);

                int icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_StockFabricLedger_tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                    setTempRowIndex();

                    bool RtnValue = Localization.ParseBoolean(DB.GetSnglValue("Select [dbo].[fn_ChkDel_FabricReciept](" + txtCode.Text + ")"));
                    if (RtnValue == true)
                    {
                        cboToDepartment.Enabled = false;
                    }
                    else
                    {
                        cboToDepartment.Enabled = true;
                    }
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
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityShieldBlue, "Editing Warning", "This Record Is Edited By Another User...");
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
                for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                {
                    fgDtls.Rows[i].Cells[16].ReadOnly = false;
                }

                if ((base.blnFormAction == Enum_Define.ActionType.View_Record) && !(base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_StockFabricLedger_tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));
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

                SetOldBarcodeNo();
                AplySelectBtnEnbl();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

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
                this.txtEntryNo = txtEntryNo;
                this.txtRefNo.Text = CommonCls.AutoInc(this, "RefNo", "FabReceiptID", "");
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                int MaxID = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(FabReceiptID),0) From {0}  Where IsDeleted=0 and StoreID = {1} and  CompID = {2} and BranchID = {3} and YearID = {4}", "tbl_FabricReceiptMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID))));
                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where IsDeleted=0 and  FabReceiptID = {1} and StoreID={2} and CompID={3} and BranchID={4} and YearID={5}", new object[] { "tbl_FabricReceiptMain", MaxID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                        dtRefDate.Text = (Localization.ToVBDateString(reader["RefDate"].ToString()));
                        cboProcessType.SelectedValue = Localization.ParseNativeDouble(reader["ProcessTypeID"].ToString());
                        cboProcesser.SelectedValue = Localization.ParseNativeDouble(reader["ProcesserID"].ToString());
                        cboBroker.SelectedValue = Localization.ParseNativeDouble(reader["BrokerID"].ToString());
                        cboToDepartment.SelectedValue = Localization.ParseNativeDouble(reader["DepartmentID"].ToString());
                        cboTransport.SelectedValue = Localization.ParseNativeDouble(reader["TransportID"].ToString());
                        cboAcType.SelectedValue = Localization.ParseNativeDouble(reader["ProcessAcID"].ToString());
                    }
                }
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                if (NewPieceNo)
                {
                    if (((fgDtls.RowCount > 0) & (fgDtls.ColumnCount > 0)) & fgDtls.Columns[4].Visible)
                    {
                        fgDtls.Rows[0].Cells[4].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select  {0}({1},{2},{3},{4},{5},{6})", new object[] { "dbo.fn_FetchBarcodeNo", MaxID, base.iIDentity, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })), Db_Detials.PCS_NO_INCMT);
                    }
                    else
                    {
                        fgDtls.Rows[0].Cells[4].Value = "-";
                    }
                }
                dtEntryDate.Focus();
                txtUniqueID.Text = CommonCls.GenUniqueID();
                strUniqueID = txtUniqueID.Text;
                AplySelectBtnEnbl();
                cboProcesser.Enabled = true;
                cboToDepartment.Enabled = true;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public void PrintRecord()
        {
            try
            {
                CIS_ReportTool.frmMultiPrint frmMultiPrint = new CIS_ReportTool.frmMultiPrint();
                CIS_ReportTool.frmMultiPrint.MenuID = base.iIDentity;
                CIS_ReportTool.frmMultiPrint.Id = Localization.ParseNativeInt(this.txtCode.Text);
                CIS_ReportTool.frmMultiPrint.TblNm = "tbl_FabricReceiptMain";
                CIS_ReportTool.frmMultiPrint.TblNm_D = "tbl_FabricReceiptDtls";
                CIS_ReportTool.frmMultiPrint.IdStr = "FabReceiptID";
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

        public void SaveRecord()
        {
            try
            {
                ArrayList pArrayData = new ArrayList
                {
                    this.frmVoucherTypeID,
                    ("(#ENTRYNO#)"),
                    (dtEntryDate.TextFormat(false, true)),
                    (txtRefNo.Text.ToString()),
                    (dtRefDate.TextFormat(false, true)),
                    (cboProcessType.SelectedValue),
                    (cboProcesser.SelectedValue),
                    (cboBroker.SelectedValue),
                    (cboToDepartment.SelectedValue),
                    (cboTransport.SelectedValue),
                    (txtVehicleNo.Text.ToString()),
                    (txtLrNo.Text.ToString()),
                    (dtLrDate.TextFormat(false, true)),
                    (cboAcType.SelectedValue),
                    (string.Format("{0:N0}", CommonCls.GetColSum(this.fgDtls, 15, -1, -1)).ToString().Replace(",", "")),
                    (string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 16, -1, -1)).ToString().Replace(",", "")),
                    (txtDescription.Text.ToString()),
                    txtUniqueID.Text,
                    (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                    (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                    (dtED1.TextFormat(false, true)),
                    (txtET1.Text.Trim()),
                    (txtET2.Text.Trim()),
                    (txtET3.Text.Trim())
                };
                int UnitID = 0;
                int ShrinkageID = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format("Select LedgerId From {0} Where LedgerName = 'SHORTAGE ACCOUNT' ", "tbl_LedgerMaster"))));
                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", "(#CodeID#)", base.iIDentity);

                for (int j = 0; j <= fgDtls.RowCount - 1; j++)
                {
                    DataGridViewRow row2 = this.fgDtls.Rows[j];
                    //if (row2.Cells[16].Value != null)
                    {
                        if (Localization.ParseNativeDouble(row2.Cells[16].Value.ToString()) > 0)
                        {
                            string OldQualityID = DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = " + row2.Cells[6].Value.ToString(), "tbl_FabricDesignMaster"));

                            strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)", (j + 1).ToString(), 
                                    "(#ENTRYNO#)", dtRefDate.Text, Localization.ParseNativeDouble(cboToDepartment.SelectedValue.ToString()), 
                                    row2.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[24].Value.ToString()), 
                                    base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (j + 1).ToString(), 
                                    row2.Cells[44].Value == null ? "NULL" : row2.Cells[44].Value.ToString().Trim() == "" ? "NULL" : row2.Cells[44].Value.ToString(), 
                                    (row2.Cells[3].Value == null ? "-" : (row2.Cells[3].Value.ToString() == "" ? "-" : row2.Cells[3].Value.ToString())),
                                    (row2.Cells[4].Value == null ? "-" : (row2.Cells[4].Value.ToString() == "" ? "-" : row2.Cells[4].Value.ToString())),
                                    row2.Cells[9].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[9].Value.ToString()), 
                                    row2.Cells[11].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[11].Value.ToString()), 
                                    row2.Cells[10].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[10].Value.ToString()), 
                                    row2.Cells[12].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[12].Value.ToString()), 
                                    row2.Cells[13].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[13].Value.ToString()), 
                                    row2.Cells[14].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[14].Value.ToString()), 
                                    Localization.ParseNativeDecimal(row2.Cells[15].Value.ToString()), Localization.ParseNativeDecimal(row2.Cells[16].Value.ToString()), 
                                    Localization.ParseNativeDecimal(row2.Cells[17].Value.ToString()), 0, 0, 0, 
                                    row2.Cells[18].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[18].Value.ToString()),
                                    "NULL",
                                    row2.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[25].Value.ToString()),
                                    row2.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[26].Value.ToString()),
                                    row2.Cells[27].Value == null ? "NULL" : row2.Cells[27].Value.ToString(),
                                    row2.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[28].Value.ToString()),
                                    row2.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[29].Value.ToString()),
                                    row2.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[30].Value.ToString()),
                                    row2.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[31].Value.ToString()),
                                    row2.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[32].Value.ToString()),
                                    row2.Cells[34].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[34].Value.ToString()),
                                    row2.Cells[35].Value == null || row2.Cells[35].Value.ToString() == "" || row2.Cells[35].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row2.Cells[35].Value.ToString()),
                                    row2.Cells[36].Value == null || row2.Cells[36].Value.ToString() == "" || row2.Cells[36].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row2.Cells[36].Value.ToString()),
                                    row2.Cells[37].Value == null || row2.Cells[37].Value.ToString() == "" ? "-" : row2.Cells[37].Value.ToString(),
                                    row2.Cells[38].Value == null || row2.Cells[38].Value.ToString() == "" ? "-" : row2.Cells[38].Value.ToString(),
                                    row2.Cells[39].Value == null || row2.Cells[39].Value.ToString() == "" ? "-" : row2.Cells[39].Value.ToString(),
                                    row2.Cells[40].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[40].Value.ToString()),
                                    row2.Cells[41].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[41].Value.ToString()),
                                    "NULL", j, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                            //strAdjQry += DBSp.InsertIntoFabrIcStockLedger("(#CodeID#)", (j + 1).ToString(), "(#ENTRYNO#)",
                            //            Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboToDepartment.SelectedValue.ToString()),
                            //            base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (j + 1).ToString(), row2.Cells[4].Value.ToString(), dtRefDate.Text,
                            //            Localization.ParseNativeDouble(row2.Cells[11].Value.ToString()), Localization.ParseNativeDouble(row2.Cells[10].Value.ToString()),
                            //            Localization.ParseNativeDouble(row2.Cells[12].Value.ToString()), Localization.ParseNativeDouble(row2.Cells[UnitID].Value.ToString()),
                            //            Localization.ParseNativeDecimal(row2.Cells[15].Value.ToString()), Localization.ParseNativeDecimal(row2.Cells[16].Value.ToString()),
                            //            row2.Cells[17].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[17].Value.ToString()), 0, 0, 0,
                            //            row2.Cells[3].Value.ToString(), "null", row2.Cells[GradeID].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[GradeID].Value.ToString()),
                            //            Localization.ParseNativeInt(row2.Cells[SubDepartmentID].Value.ToString()), "NULL", j, 1,
                            //            row2.Cells[InwLedID].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[InwLedID].Value.ToString()),
                            //            row2.Cells[InwTransID].Value == null ? "0" : row2.Cells[InwTransID].Value.ToString(),
                            //            0, Localization.ParseNativeInt(cboProcessType.SelectedValue.ToString()),
                            //            row2.Cells[MainRefID].Value == null ? "0" : row2.Cells[MainRefID].Value.ToString(),
                            //            (row2.Cells[18].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[18].Value.ToString())), 0,
                            //            row2.Cells[ProductionOrdID].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[ProductionOrdID].Value.ToString()),
                            //            Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                            if (row2.Cells[19].Value != null)
                            {
                                if (Localization.ParseNativeDouble(row2.Cells[19].Value.ToString()) > 0)
                                {
                                    strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)", (j + 1).ToString(),
                                            "(#ENTRYNO#)", dtRefDate.Text, Localization.ParseNativeDouble(ShrinkageID.ToString()),
                                            row2.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[24].Value.ToString()),
                                            base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (j + 1).ToString(),
                                            row2.Cells[44].Value == null ? "NULL" : row2.Cells[44].Value.ToString().Trim() == "" ? "NULL" : row2.Cells[44].Value.ToString(),
                                            (row2.Cells[3].Value == null ? "-" : (row2.Cells[3].Value.ToString() == "" ? "-" : row2.Cells[3].Value.ToString())),
                                            (row2.Cells[4].Value == null ? "-" : (row2.Cells[4].Value.ToString() == "" ? "-" : row2.Cells[4].Value.ToString())),
                                            row2.Cells[9].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[9].Value.ToString()),
                                            row2.Cells[11].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[11].Value.ToString()),
                                            row2.Cells[10].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[10].Value.ToString()),
                                            row2.Cells[12].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[12].Value.ToString()),
                                            row2.Cells[13].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[13].Value.ToString()),
                                            row2.Cells[14].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[14].Value.ToString()),
                                            0, Localization.ParseNativeDecimal(row2.Cells[19].Value.ToString()),
                                            Localization.ParseNativeDecimal(row2.Cells[17].Value.ToString()), 0, 0, 0,
                                            row2.Cells[18].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[18].Value.ToString()),
                                            "NULL",
                                            row2.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[25].Value.ToString()),
                                            row2.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[26].Value.ToString()),
                                            row2.Cells[27].Value == null ? "NULL" : row2.Cells[27].Value.ToString(),
                                            row2.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[28].Value.ToString()),
                                            row2.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[29].Value.ToString()),
                                            row2.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[30].Value.ToString()),
                                            row2.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[31].Value.ToString()),
                                            row2.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[32].Value.ToString()),
                                            row2.Cells[34].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[34].Value.ToString()),
                                            row2.Cells[35].Value == null || row2.Cells[35].Value.ToString() == "" || row2.Cells[35].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row2.Cells[35].Value.ToString()),
                                            row2.Cells[36].Value == null || row2.Cells[36].Value.ToString() == "" || row2.Cells[36].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row2.Cells[36].Value.ToString()),
                                            row2.Cells[37].Value == null || row2.Cells[37].Value.ToString() == "" ? "-" : row2.Cells[37].Value.ToString(),
                                            row2.Cells[38].Value == null || row2.Cells[38].Value.ToString() == "" ? "-" : row2.Cells[38].Value.ToString(),
                                            row2.Cells[39].Value == null || row2.Cells[39].Value.ToString() == "" ? "-" : row2.Cells[39].Value.ToString(),
                                            row2.Cells[40].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[40].Value.ToString()),
                                            row2.Cells[41].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[41].Value.ToString()),
                                            "NULL", j, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);


                                    //strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger("(#CodeID#)", (j + 1).ToString(), "(#ENTRYNO#)",
                                    //    Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(ShrinkageID.ToString()),
                                    //    (row2.Cells[ARefID].Value == null ? "0" : row2.Cells[ARefID].Value.ToString() == "" ? "0" : row2.Cells[ARefID].Value.ToString()),
                                    //    row2.Cells[47].Value.ToString(), dtRefDate.Text, Localization.ParseNativeDouble(row2.Cells[11].Value.ToString()),
                                    //    Localization.ParseNativeDouble(row2.Cells[10].Value.ToString()), Localization.ParseNativeDouble(row2.Cells[12].Value.ToString()),
                                    //    Localization.ParseNativeDouble(row2.Cells[UnitID].Value.ToString()), 0, Localization.ParseNativeDecimal(row2.Cells[19].Value.ToString()),
                                    //    Localization.ParseNativeDecimal(row2.Cells[17].Value.ToString()), 0, 0, 0, row2.Cells[3].Value.ToString(), "null",
                                    //    row2.Cells[GradeID].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[GradeID].Value.ToString()),
                                    //    Localization.ParseNativeInt(row2.Cells[SubDepartmentID].Value.ToString()), "NULL", j, 1,
                                    //     row2.Cells[InwLedID].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[InwLedID].Value.ToString()),
                                    //    row2.Cells[InwTransID].Value == null ? "0" : row2.Cells[InwTransID].Value.ToString(),
                                    //    0, Localization.ParseNativeInt(cboProcessType.SelectedValue.ToString()),
                                    //    row2.Cells[MainRefID].Value == null ? "0" : row2.Cells[MainRefID].Value.ToString(),
                                    //    (row2.Cells[18].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[18].Value.ToString())), 0,
                                    //     row2.Cells[ProductionOrdID].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[ProductionOrdID].Value.ToString()),
                                    //    Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                                }
                            }
                            strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)", (j + 1).ToString(),
                                        "(#ENTRYNO#)", dtRefDate.Text, Localization.ParseNativeDouble(cboProcesser.SelectedValue.ToString()),
                                        row2.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[24].Value.ToString()),
                                        row2.Cells[43].Value == null ? "NULL" : row2.Cells[43].Value.ToString().Trim() == "" ? "NULL" : row2.Cells[43].Value.ToString(),
                                        row2.Cells[44].Value == null ? "NULL" : row2.Cells[44].Value.ToString().Trim() == "" ? "NULL" : row2.Cells[44].Value.ToString(),
                                        (row2.Cells[3].Value == null ? "-" : (row2.Cells[3].Value.ToString() == "" ? "-" : row2.Cells[3].Value.ToString())),
                                        (row2.Cells[4].Value == null ? "-" : (row2.Cells[4].Value.ToString() == "" ? "-" : row2.Cells[4].Value.ToString())),
                                        row2.Cells[9].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[9].Value.ToString()),
                                        row2.Cells[11].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[11].Value.ToString()),
                                        row2.Cells[10].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[10].Value.ToString()),
                                        row2.Cells[12].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[12].Value.ToString()),
                                        row2.Cells[13].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[13].Value.ToString()),
                                        row2.Cells[14].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[14].Value.ToString()),
                                        0, 0, 0,
                                        Localization.ParseNativeDecimal(row2.Cells[15].Value.ToString()),
                                        (Localization.ParseNativeDecimal(row2.Cells[16].Value.ToString()) + (row2.Cells[19].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[19].Value.ToString()))),
                                        Localization.ParseNativeDecimal(row2.Cells[17].Value.ToString()), 
                                        row2.Cells[18].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[18].Value.ToString()),
                                        "NULL",
                                        row2.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[25].Value.ToString()),
                                        row2.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[26].Value.ToString()),
                                        row2.Cells[27].Value == null ? "NULL" : row2.Cells[27].Value.ToString(),
                                        row2.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[28].Value.ToString()),
                                        row2.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[29].Value.ToString()),
                                        row2.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[30].Value.ToString()),
                                        row2.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[31].Value.ToString()),
                                        row2.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[32].Value.ToString()),
                                        row2.Cells[34].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[34].Value.ToString()),
                                        row2.Cells[35].Value == null || row2.Cells[35].Value.ToString() == "" || row2.Cells[35].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row2.Cells[35].Value.ToString()),
                                        row2.Cells[36].Value == null || row2.Cells[36].Value.ToString() == "" || row2.Cells[36].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row2.Cells[36].Value.ToString()),
                                        row2.Cells[37].Value == null || row2.Cells[37].Value.ToString() == "" ? "-" : row2.Cells[37].Value.ToString(),
                                        row2.Cells[38].Value == null || row2.Cells[38].Value.ToString() == "" ? "-" : row2.Cells[38].Value.ToString(),
                                        row2.Cells[39].Value == null || row2.Cells[39].Value.ToString() == "" ? "-" : row2.Cells[39].Value.ToString(),
                                        row2.Cells[40].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[40].Value.ToString()),
                                        row2.Cells[41].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[41].Value.ToString()),
                                        "NULL", j, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                            //strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger("(#CodeID#)", (j + 1).ToString(), "(#ENTRYNO#)",
                            //    Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboProcesser.SelectedValue.ToString()),
                            //    (row2.Cells[ARefID].Value == null ? "0" : row2.Cells[ARefID].Value.ToString() == "" ? "0" : row2.Cells[ARefID].Value.ToString()), row2.Cells[47].Value.ToString(), dtRefDate.Text,
                            //    Localization.ParseNativeDouble(OldQualityID), Localization.ParseNativeDouble(row2.Cells[6].Value.ToString()),
                            //    Localization.ParseNativeDouble(row2.Cells[8].Value.ToString()), Localization.ParseNativeDouble(row2.Cells[UnitID].Value.ToString()),
                            //    0, 0, 0, Localization.ParseNativeDecimal(row2.Cells[15].Value.ToString()), (Localization.ParseNativeDecimal(row2.Cells[16].Value.ToString()) + (row2.Cells[19].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[19].Value.ToString()))),
                            //    row2.Cells[17].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[17].Value.ToString()), row2.Cells[3].Value.ToString(), "null",
                            //    row2.Cells[GradeID].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[GradeID].Value.ToString()),
                            //    Localization.ParseNativeInt(row2.Cells[SubDepartmentID].Value.ToString()), "NULL", j, 1,
                            //    row2.Cells[InwLedID].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[InwLedID].Value.ToString()),
                            //    row2.Cells[InwTransID].Value == null ? "0" : row2.Cells[InwTransID].Value.ToString(),
                            //    0, Localization.ParseNativeInt(cboProcessType.SelectedValue.ToString()),
                            //    row2.Cells[MainRefID].Value == null ? "0" : row2.Cells[MainRefID].Value.ToString(),
                            //    (row2.Cells[18].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[18].Value.ToString())), 0, row2.Cells[ProductionOrdID].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[ProductionOrdID].Value.ToString()),
                            //    Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                            OldQualityID = "0";
                            UnitID = Localization.ParseNativeInt(row2.Cells[UnitID].Value.ToString());
                        }
                    }
                    row2 = null;
                }
                if ((cboTransport.SelectedValue != null) && (Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString())) > 0.0)
                {
                    //strAdjQry = strAdjQry + DBSp.InsertIntoTrasportLedger("(#CodeID#)", txtRefNo.Text.ToString(), dtRefDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()), Localization.ParseNativeDouble(cboProcesser.SelectedValue.ToString()), Localization.ParseNativeDouble(cboToDepartment.SelectedValue.ToString()), txtLrNo.Text, dtLrDate.Text, txtVehicleNo.Text, Localization.ParseNativeDouble(UnitID.ToString()), Localization.ParseNativeInt(string.Format("{0:N0}", CommonCls.GetColSum(this.fgDtls, 15, -1, -1))), Localization.ParseNativeDecimal(string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 16, -1, -1))), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                }
                strAdjQry += "Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";";
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                DBSp.Transcation_AddEdit(pArrayData, fgDtls, true, strAdjQry, "", txtEntryNo.Text, "", "");
                OrgInGridArray.Clear();
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
                string strTblName;
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
                if (txtRefNo.Text.Trim() == "" || txtRefNo.Text.Trim() == "-" || txtRefNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Ref No.");
                    txtRefNo.Focus();
                    return true;
                }
                if (!Information.IsDate(dtRefDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Ref Date");
                    dtRefDate.Focus();
                    return true;
                }
                if (cboProcessType.SelectedValue == null || cboProcessType.Text.Trim().ToString() == "-" || cboProcessType.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Issue Type");
                    cboProcessType.Focus();
                    return true;
                }
                if (cboProcesser.SelectedValue == null || cboProcesser.Text.Trim().ToString() == "-" || cboProcesser.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Processer");
                    cboProcesser.Focus();
                    return true;
                }
                if (cboToDepartment.SelectedValue == null || cboToDepartment.Text.Trim().ToString() == "-" || cboToDepartment.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    cboToDepartment.Focus();
                    return true;
                }

                if (txtRefNo.Text.Trim().Length > 0)
                {
                    if (base.blnFormAction == 0)
                    {
                        strTblName = "tbl_FabricReceiptMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "RefNo", txtRefNo.Text, false, "", 0, " ProcesserID = " + cboProcesser.SelectedValue + " AND StoreID = " + Db_Detials.StoreID + " AND CompID =" + Db_Detials.CompID + " AND BranchID = " + Db_Detials.BranchID + " And YearID = " + Db_Detials.YearID + "", "This Processor already used this Ref No in Entry No : " + DB.GetSnglValue(string.Format("Select Entryno from {0} where ProcesserID = {1} and RefNo = '{2}' ", "tbl_FabricReceiptMain", cboProcesser.SelectedValue, txtRefNo.Text.ToString()))))
                        {
                            txtRefNo.Focus();
                            return true;
                        }
                    }
                    else if (Localization.ParseNativeInt(base.blnFormAction.ToString()) == 1)
                    {
                        strTblName = "tbl_FabricReceiptMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "RefNo", txtRefNo.Text, true, "FabReceiptID", Localization.ParseNativeLong(txtCode.Text.Trim()), " ProcesserID = " + cboProcesser.SelectedValue + " AND StoreID = " + Db_Detials.StoreID + " AND CompID =" + Db_Detials.CompID + " AND BranchID = " + Db_Detials.BranchID + " And YearID = " + Db_Detials.YearID + "", "This Processor already used this Ref No in Entry No : " + DB.GetSnglValue(string.Format("Select Entryno from {0} where ProcesserID = {1} and RefNo = '{2}' ", "tbl_FabricReceiptMain", cboProcesser.SelectedValue, txtRefNo.Text.ToString()))))
                        {
                            txtRefNo.Focus();
                            return true;
                        }
                    }
                }
                if (Vld_DupPieceNo)
                {
                    if (CheckDupPieceNo())
                    {
                        return true;
                    }
                }

                if (FAB_MAINTAINWEIGHT)
                {
                    for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                    {
                        if (fgDtls.Rows[i].Cells[17].Value == null || Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[17].Value.ToString()) == 0 || fgDtls.Rows[i].Cells[17].Value.ToString() == "")
                        {
                            fgDtls.CurrentCell = fgDtls[17, i];
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Weight");
                            fgDtls.Rows.Add();
                            return true;
                        }
                    }
                }
                return false;
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                return false;
            }
        }

        #endregion

        public void AplySelectBtnEnbl()
        {
            if (base.blnFormAction == Enum_Define.ActionType.New_Record || blnFormAction == Enum_Define.ActionType.Edit_Record)
            {
                btnSelect.Enabled = true;
            }
            else
            {
                btnSelect.Enabled = false;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
            {
                try
                {
                    bool isIndexAppld = false;
                    int iIndex = fgDtls.RowCount - 1;
                    for (int m = 0; m <= fgDtls.RowCount - 1; m++)
                    {
                        if (fgDtls.Rows[m].Cells[3].Value != null && fgDtls.Rows[m].Cells[3].Value.ToString() != "")
                        {
                            iIndex = m;
                            isIndexAppld = true;
                        }
                    }
                    if (!isIndexAppld)
                    {
                        iIndex = -1;
                    }

                    if (cboProcesser.SelectedValue == null || cboProcesser.SelectedValue.ToString() == "-" || cboProcesser.SelectedValue.ToString() == "" || cboProcesser.SelectedValue.ToString() == "0")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Processor.");
                        cboProcesser.Focus();
                    }
                    else
                    {
                        #region StockAdjQuery
                        string strQry = string.Empty;
                        int ibitcol = 0;
                        string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", iIDentity, 0.0));
                        string strQry_ColName = "";
                        string[] arr = CommonCls.GetAdjColName(base.iIDentity, 0.0).Split(';');
                        strQry_ColName = arr[0].ToString();
                        ibitcol = Localization.ParseNativeInt(arr[1]);
                        strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}, {6}) Where BatchNo <> '-' ", new object[] { strQry_ColName, snglValue, this.cboProcesser.SelectedValue, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID });
                        #endregion

                        frmStockAdj frmStockAdj = new frmStockAdj();
                        frmStockAdj.MenuID = base.iIDentity;
                        frmStockAdj.Entity_IsfFtr = 0.0;
                        frmStockAdj.ref_fgDtls = this.fgDtls;
                        frmStockAdj.LedgerID = Conversions.ToString(this.cboProcesser.SelectedValue);
                        frmStockAdj.UsedInGridArray = this.OrgInGridArray;
                        frmStockAdj.QueryString = strQry;
                        frmStockAdj.IsRefQuery = true;
                        frmStockAdj.ibitCol = ibitcol;

                        if (frmStockAdj.ShowDialog() == DialogResult.Cancel)
                        {
                            frmStockAdj.Dispose();
                        }
                        else
                        {
                            frmStockAdj.Dispose();
                            frmStockAdj = null;
                            int iRows = fgDtls.RowCount - 1;

                            for (int i = 0; i < iRows; i++)
                            {
                                if (fgDtls.Rows[i].Index > iIndex || iIndex == -1)
                                {
                                    if ((fgDtls.Rows[i].Cells[15].Value != null) && (fgDtls.Rows[i].Cells[15].Value.ToString() != "0") && (fgDtls.Rows[i].Cells[15].Value.ToString() != ""))
                                    {
                                        double iPcs = Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[15].Value.ToString());

                                        if (fgDtls.Rows[i].Cells[48].Value != null)
                                        {
                                            if (Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[48].Value.ToString()) < iPcs)
                                            {
                                                iPcs = Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[48].Value.ToString());
                                            }
                                        }

                                        if (iPcs > 0)
                                        {
                                            int num8 = (int)Math.Round((double)(iPcs + i));
                                            for (int k = i + 1; k <= num8; k++)
                                            {
                                                fgDtls.Rows.Insert(k, new DataGridViewRow());
                                                for (int m = 0; m <= fgDtls.ColumnCount - 1; m++)
                                                {
                                                    if (m == 15)
                                                    {
                                                        fgDtls.Rows[k].Cells[m].Value = 1;
                                                    }
                                                    else if (m == 1)
                                                    {
                                                        fgDtls.Rows[k].Cells[m].Value = k;
                                                    }
                                                    else if (m != 4 && m != 16 && m != 17)
                                                    {
                                                        fgDtls.Rows[k].Cells[m].Value = fgDtls.Rows[i].Cells[m].Value;
                                                    }
                                                }
                                            }
                                            fgDtls.Rows.RemoveAt(i);
                                            i = (int)Math.Round((double)(i + (iPcs - 1.0)));
                                            iRows = fgDtls.RowCount - 1;
                                        }
                                    }
                                    else
                                    {
                                        fgDtls.Rows[i].Cells[15].Value = fgDtls.Rows[i].Cells[48].Value.ToString();
                                    }
                                }
                            }
                            fgDtls.Rows.RemoveAt(fgDtls.RowCount - 1);
                            DataGridViewEx ex2 = fgDtls;
                            for (int j = 0; j <= ex2.RowCount - 1; j++)
                            {
                                if (!NewPieceNo)
                                {
                                    ex2.Rows[j].Cells[4].Value = ex2.Rows[j].Cells[47].Value;
                                }
                                else if (j == 0)
                                {
                                    int MaxId = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(FabReceiptID),0) From {0} ", "tbl_FabricReceiptMain"))));
                                    fgDtls.Rows[0].Cells[4].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select  {0}({1},{2},{3},{4},{5},{6})", new object[] { "dbo.fn_FetchBarcodeNo", MaxId, base.iIDentity, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })), Db_Detials.PCS_NO_INCMT);
                                }
                                else if (j > 0)
                                {
                                    if (ex2.Rows[j - 1].Cells[4].Value.ToString() != "-")
                                    {
                                        ex2.Rows[j].Cells[4].Value = CommonCls.AutoInc_Runtime(ex2.Rows[j - 1].Cells[4].Value.ToString(), Db_Detials.PCS_NO_INCMT);
                                    }
                                    else
                                    {
                                        ex2.Rows[j].Cells[4].Value = "-";
                                    }
                                }
                                ex2.Rows[j].Cells[10].Value = ex2.Rows[j].Cells[6].Value;
                                ex2.Rows[j].Cells[12].Value = ex2.Rows[j].Cells[8].Value;
                                ex2.Rows[j].Cells[11].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", ex2.Rows[j].Cells[10].Value)));
                                //ex2.Rows[j].Cells[15].Value = 1;
                            }
                            SetOldBarcodeNo();
                            SendKeys.Send("{TAB}");
                            if (fgDtls.Rows.Count > 0)
                            {
                                fgDtls.CurrentCell = fgDtls[8, 0];
                            }
                            fgDtls.Select();
                            setTempRowIndex();
                            setMyID_Stock();
                            ExecuterTempQry(-1);
                        }
                    }
                }
                catch (Exception ex)
                { 
                    Navigate.logError(ex.Message, ex.StackTrace);
                }
            }
        }

        private void cboProcesser_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if ((this.cboProcesser.SelectedValue != null) && (Conversion.Val(RuntimeHelpers.GetObjectValue(cboProcesser.SelectedValue)) > 0.0))
                {
                    cboBroker.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select BrokerID From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboProcesser.SelectedValue)));
                    cboTransport.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select TransportID From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboProcesser.SelectedValue)));
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (NewPieceNo && (e.ColumnIndex == 4))
                {
                    if (!Vld_DupPieceNo)
                    {
                        string strTblname;
                        if (blnFormAction == Enum_Define.ActionType.New_Record)
                        {
                            string strVal = fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString();
                            if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString().Length > 0)
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString() != "-")
                                {
                                    strTblname = "tbl_StockFabricLedger";
                                    if (Navigate.CheckDuplicate(ref strTblname, "BarcodeNo", strVal, false, "", 0, " StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                                    {
                                        fgDtls.CurrentCell = fgDtls[4, e.RowIndex];
                                    }
                                }
                            }
                            else if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString().Length <= 0)
                            {
                                fgDtls.Rows[e.RowIndex].Cells[4].Value = "-";
                            }
                        }
                        else if (blnFormAction == Enum_Define.ActionType.Edit_Record)
                        {
                            if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString().Length > 0)
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString() != "-")
                                {
                                    strTblname = "tbl_StockFabricLedger";
                                    if (Navigate.CheckDuplicate(ref strTblname, "BarcodeNo", fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString(), true, "TransID", Localization.ParseNativeLong(txtCode.Text.Trim()), " StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                                    {
                                        fgDtls.CurrentCell = fgDtls[4, e.RowIndex];
                                    }
                                }
                            }
                            else if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString().Length <= 0)
                            {
                                fgDtls.Rows[e.RowIndex].Cells[4].Value = "-";
                            }
                        }
                    }
                    if (Vld_DupPieceNo)
                    {
                        string strTblname;
                        if (blnFormAction == Enum_Define.ActionType.New_Record)
                        {
                            string strVal = fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString();
                            if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString().Length > 0)
                            {
                                //if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString() != "-")
                                {
                                    strTblname = "tbl_StockFabricLedger";
                                    if (Navigate.CheckDuplicate(ref strTblname, "BarcodeNo", strVal, false, "", 0, " StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                                    {
                                        fgDtls.CurrentCell = fgDtls[4, e.RowIndex];
                                    }
                                }
                            }
                            else if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString().Length <= 0)
                            {
                                fgDtls.Rows[e.RowIndex].Cells[4].Value = "-";
                            }
                        }
                        else if (blnFormAction == Enum_Define.ActionType.Edit_Record)
                        {
                            if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString().Length > 0)
                            {
                                //if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString() != "-")
                                {
                                    strTblname = "tbl_StockFabricLedger";
                                    if (Navigate.CheckDuplicate(ref strTblname, "BarcodeNo", fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString(), true, "TransID", Localization.ParseNativeLong(txtCode.Text.Trim()), " StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                                    {
                                        fgDtls.CurrentCell = fgDtls[4, e.RowIndex];
                                    }
                                }
                            }
                            else if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString().Length <= 0)
                            {
                                fgDtls.Rows[e.RowIndex].Cells[4].Value = "-";
                            }
                        }
                    }
                }
                if ((e.ColumnIndex == 10) | (e.ColumnIndex == 12))
                {
                    fgDtls.Rows[e.RowIndex].Cells[11].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", fgDtls.Rows[e.RowIndex].Cells[10].Value)));
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
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (((e.ColumnIndex == 15) | (e.ColumnIndex == 16)))
                    {
                        ExecuterTempQry(e.RowIndex);
                    }
                    switch (e.ColumnIndex)
                    {
                        case 6:
                            fgDtls.Rows[e.RowIndex].Cells[10].Value = fgDtls.Rows[e.RowIndex].Cells[6].Value;
                            break;

                        case 8:
                            fgDtls.Rows[e.RowIndex].Cells[12].Value = fgDtls.Rows[e.RowIndex].Cells[8].Value;
                            break;

                        case 10:
                            fgDtls.Rows[e.RowIndex].Cells[11].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", fgDtls.Rows[e.RowIndex].Cells[10].Value)));
                            break;

                        case 15:
                            break;

                    }

                    if (e.ColumnIndex == 10 || e.ColumnIndex == 11 || e.ColumnIndex == 12)
                    {
                        for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                        {
                            if (fgDtls.Rows[i].Cells[10].Value != null && fgDtls.Rows[i].Cells[10].Value.ToString() != "" && fgDtls.Rows[i].Cells[11].Value != null && fgDtls.Rows[i].Cells[11].Value.ToString() != "" && fgDtls.Rows[i].Cells[12].Value != null && fgDtls.Rows[i].Cells[12].Value.ToString() != "")
                            {
                                fgDtls.Rows[i].Cells[5].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select SerialID from fn_BookserialMaster_tbl() where DesignID={0} and QualityID={1} and ShadeID={2}", fgDtls.Rows[i].Cells[10].Value, fgDtls.Rows[i].Cells[11].Value, fgDtls.Rows[i].Cells[12].Value)));
                            }
                        }
                    }

                    if (fgDtls.RowCount > 1)
                    {
                        cboProcesser.Enabled = false;
                        cboToDepartment.Enabled = false;
                    }
                    else
                    {
                        cboProcesser.Enabled = true;
                        cboToDepartment.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void SetOldBarcodeNo()
        {
            try
            {
                if ((blnFormAction == Enum_Define.ActionType.Edit_Record) || (blnFormAction == Enum_Define.ActionType.View_Record))
                {
                    if (txtCode.Text.Trim() != "")
                    {
                        using (IDataReader reader = DB.GetRS(string.Format("Select SubTransID, BarCodeNo from {0} where IsDeleted=0 and TransID = {1} and TransType = {2} and Cr_Mtrs <> 0 Order By SubTransID", "tbl_StockFabricLedger", txtCode.Text, base.iIDentity)))
                        {
                            while (reader.Read())
                            {
                                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                                {
                                    if (Localization.ParseNativeInt(fgDtls.Rows[i].Cells[1].Value.ToString()) == Localization.ParseNativeInt(reader["SubTransID"].ToString()))
                                    {
                                        fgDtls.Rows[i].Cells[47].Value = reader["BarCodeNo"].ToString();
                                        break;
                                    }
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

        private bool CheckDupPieceNo()
        {
            string strTbleName;
            if (Vld_DupPieceNo)
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                    {
                        string primaryFieldNameValue = fgDtls.Rows[i].Cells[4].Value.ToString();
                        if (fgDtls.Rows[i].Cells[4].Value.ToString() != null && fgDtls.Rows[i].Cells[4].Value.ToString().Length > 0)
                        {
                            //if (fgDtls.Rows[i].Cells[2].Value.ToString() != "-")
                            {
                                strTbleName = "tbl_StockFabricLedger";
                                if (Navigate.CheckDuplicate(ref strTbleName, "BarcodeNo", primaryFieldNameValue, false, "", 0L, " StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                                {
                                    fgDtls.CurrentCell = fgDtls[4, i];
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                }
                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    for (int j = 0; j <= fgDtls.RowCount - 1; j++)
                    {
                        if (fgDtls.Rows[j].Cells[4].Value.ToString() != null && fgDtls.Rows[j].Cells[4].Value.ToString().Length > 0)
                        {
                            //if (fgDtls.Rows[j].Cells[2].Value.ToString() != "-")
                            {
                                strTbleName = "tbl_StockFabricLedger";
                                if (Navigate.CheckDuplicate(ref strTbleName, "BarcodeNo", fgDtls.Rows[j].Cells[4].Value.ToString(), true, "TransID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), " StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                                {
                                    fgDtls.CurrentCell = fgDtls[4, j];
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                }
                return false;
            }
            return false;
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
                                    MyID = iMaxMyID_Stock.ToString();
                                }
                                else
                                {
                                    StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockFabricLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockFabricLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + "")));
                                    MyID = txtCode.Text;
                                }

                                decimal FabricDesign = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select WtPerMtr From tbl_FabricDesignMaster Where FabricDesignID=" + Localization.ParseNativeInt(row.Cells[10].Value.ToString()) + "")));

                                if (row.Cells[16].Value != null)
                                {
                                    row.Cells[21].Value = Localization.ParseNativeDecimal(row.Cells[16].Value.ToString());
                                }
                                if (row.Cells[17].Value != null)
                                {
                                    row.Cells[22].Value = Localization.ParseNativeDecimal(row.Cells[17].Value.ToString());
                                }

                                if (row.Cells[22].Value == null || Localization.ParseNativeDecimal(row.Cells[22].Value.ToString()) == 0)
                                {
                                    if (FabricDesign != 0)
                                        row.Cells[22].Value = FabricDesign;
                                }

                                double Weight = 0;

                                if (row.Cells[21].Value != null && Localization.ParseNativeDecimal(row.Cells[21].Value.ToString()) > 0 && Localization.ParseNativeDecimal(row.Cells[21].Value.ToString()) != 0)
                                {
                                    if (row.Cells[22].Value == null || Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[22].Value.ToString()) == 0)
                                    {
                                        Weight = (Localization.ParseNativeDouble(FabricDesign.ToString()) / Localization.ParseNativeDouble(row.Cells[21].Value.ToString())) * Localization.ParseNativeDouble(row.Cells[16].Value.ToString());
                                    }
                                    else
                                    {
                                        Weight = (Localization.ParseNativeDouble(row.Cells[22].Value.ToString()) / Localization.ParseNativeDouble(row.Cells[21].Value.ToString())) * Localization.ParseNativeDouble(row.Cells[16].Value.ToString());
                                    }
                                    if (Weight.ToString() != "NaN")
                                    {
                                        row.Cells[17].Value = Math.Round(Weight, 3);
                                    }
                                }
                                if (MyID != "" && row.Cells[16].Value != null && row.Cells[16].Value.ToString() != "" && row.Cells[16].Value.ToString() != "0" && row.Cells[15].Value != null && row.Cells[15].Value.ToString() != "" && row.Cells[15].Value.ToString() != "0")
                                {
                                    strQry = strQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), MyID, (i + 1).ToString(),
                                        txtEntryNo.Text, dtRefDate.Text, Localization.ParseNativeDouble(cboProcesser.SelectedValue.ToString()),
                                        row.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                        row.Cells[43].Value == null ? "NULL" : row.Cells[43].Value.ToString().Trim() == "" ? "NULL" : row.Cells[43].Value.ToString(),
                                        row.Cells[44].Value == null ? "NULL" : row.Cells[44].Value.ToString().Trim() == "" ? "NULL" : row.Cells[44].Value.ToString(),
                                        (row.Cells[3].Value == null ? "-" : (row.Cells[3].Value.ToString() == "" ? "-" : row.Cells[3].Value.ToString())),
                                        (row.Cells[4].Value == null ? "-" : (row.Cells[4].Value.ToString() == "" ? "-" : row.Cells[4].Value.ToString())),
                                        row.Cells[9].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[9].Value.ToString()),
                                        row.Cells[11].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[11].Value.ToString()),
                                        row.Cells[10].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[10].Value.ToString()),
                                        row.Cells[12].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[12].Value.ToString()),
                                        row.Cells[13].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                                        row.Cells[14].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[14].Value.ToString()),
                                        0, 0, 0,
                                        Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                                        (Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()) + (row.Cells[19].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[19].Value.ToString()))),
                                        Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                                        row.Cells[18].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()),
                                        "NULL",
                                        row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                        row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                        row.Cells[27].Value == null ? "NULL" : row.Cells[27].Value.ToString(),
                                        row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                        row.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[29].Value.ToString()),
                                        row.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[30].Value.ToString()),
                                        row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                        row.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[32].Value.ToString()),
                                        row.Cells[34].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[34].Value.ToString()),
                                        row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" || row.Cells[35].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[35].Value.ToString()),
                                        row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" || row.Cells[36].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[36].Value.ToString()),
                                        row.Cells[37].Value == null || row.Cells[37].Value.ToString() == "" ? "-" : row.Cells[37].Value.ToString(),
                                        row.Cells[38].Value == null || row.Cells[38].Value.ToString() == "" ? "-" : row.Cells[38].Value.ToString(),
                                        row.Cells[39].Value == null || row.Cells[39].Value.ToString() == "" ? "-" : row.Cells[39].Value.ToString(),
                                        row.Cells[40].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[40].Value.ToString()),
                                        row.Cells[41].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[41].Value.ToString()),
                                        txtUniqueID.Text, i, StatusID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);


                                    //strQry += DBSp.InsertIntoFabrIcStockLedger(MyID, Convert.ToString(i + 1), txtEntryNo.Text,
                                    //    Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboProcesser.SelectedValue.ToString()),
                                    //    (row.Cells[ARefID].Value == null ? "0" : row.Cells[ARefID].Value.ToString() == "" ? "0" : row.Cells[ARefID].Value.ToString()),
                                    //    row.Cells[47].Value.ToString(), dtRefDate.Text,
                                    //    Localization.ParseNativeDouble(row.Cells[11].Value.ToString()), Localization.ParseNativeDouble(row.Cells[10].Value.ToString()),
                                    //    Localization.ParseNativeDouble(row.Cells[12].Value.ToString()), Localization.ParseNativeDouble(row.Cells[UnitID].Value.ToString()),
                                    //    0, 0, 0, Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                    //    row.Cells[17].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                                    //    row.Cells[3].Value.ToString(), "null", row.Cells[GradeID].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[GradeID].Value.ToString()),
                                    //    Localization.ParseNativeInt(row.Cells[SubDepartmentID].Value.ToString()), txtUniqueID.Text, i, StatusID,
                                    //    row.Cells[InwLedID].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[InwLedID].Value.ToString()),
                                    //    row.Cells[InwTransID].Value == null ? "0" : row.Cells[InwTransID].Value.ToString(),
                                    //    0, Localization.ParseNativeInt(cboProcessType.SelectedValue.ToString()),
                                    //    row.Cells[MainRefID].Value == null ? "0" : row.Cells[MainRefID].Value.ToString(),
                                    //    (row.Cells[18].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[18].Value.ToString())), 0,
                                    //     row.Cells[ProductionOrdID].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[ProductionOrdID].Value.ToString()),
                                    //    Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                                }
                            }
                        }
                        else
                        {
                            if ((fgDtls.CurrentCell.ColumnIndex == 15) || (fgDtls.CurrentCell.ColumnIndex == 16) || (fgDtls.CurrentCell.ColumnIndex == 17))
                            {
                                DataGridViewRow row = fgDtls.Rows[RowIndex];
                                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                                {
                                    StatusID = 1;
                                    MyID = iMaxMyID_Stock.ToString();
                                }
                                else
                                {
                                    StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockFabricLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockFabricLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + "")));
                                    MyID = txtCode.Text;
                                }

                                if (MyID != "" && row.Cells[16].Value != null && row.Cells[16].Value.ToString() != "" && row.Cells[16].Value.ToString() != "0" && row.Cells[15].Value != null && row.Cells[15].Value.ToString() != "" && row.Cells[15].Value.ToString() != "0")
                                {
                                    decimal FabricDesign = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select WtPerMtr From tbl_FabricDesignMaster Where FabricDesignID=" + Localization.ParseNativeInt(fgDtls.Rows[RowIndex].Cells[10].Value.ToString()) + "")));
                                    double Weight = 0;

                                    if (fgDtls.Rows[RowIndex].Cells[21].Value != null && Localization.ParseNativeDecimal(fgDtls.Rows[RowIndex].Cells[21].Value.ToString()) > 0 && Localization.ParseNativeDecimal(fgDtls.Rows[RowIndex].Cells[21].Value.ToString()) != 0)
                                    {
                                        if (fgDtls.Rows[RowIndex].Cells[22].Value == null || Localization.ParseNativeDecimal(fgDtls.Rows[RowIndex].Cells[22].Value.ToString()) == 0)
                                        {
                                            Weight = (Localization.ParseNativeDouble(FabricDesign.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[21].Value.ToString())) * Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[16].Value.ToString());
                                        }
                                        else
                                        {
                                            Weight = (Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[22].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[21].Value.ToString())) * Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[16].Value.ToString());
                                        }
                                        if (Weight.ToString() != "NaN")
                                        {
                                            fgDtls.Rows[RowIndex].Cells[17].Value = Math.Round(Weight, 3);
                                        }
                                    }

                                    strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[46].Value.ToString()) + " and AddedBy=" + Db_Detials.UserID + ";");
                                    strQry = strQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), MyID, (RowIndex + 1).ToString(),
                                                txtEntryNo.Text, dtRefDate.Text, Localization.ParseNativeDouble(cboProcesser.SelectedValue.ToString()),
                                                row.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                                row.Cells[43].Value == null ? "NULL" : row.Cells[43].Value.ToString().Trim() == "" ? "NULL" : row.Cells[43].Value.ToString(),
                                                row.Cells[44].Value == null ? "NULL" : row.Cells[44].Value.ToString().Trim() == "" ? "NULL" : row.Cells[44].Value.ToString(),
                                                (row.Cells[3].Value == null ? "-" : (row.Cells[3].Value.ToString() == "" ? "-" : row.Cells[3].Value.ToString())),
                                                (row.Cells[4].Value == null ? "-" : (row.Cells[4].Value.ToString() == "" ? "-" : row.Cells[4].Value.ToString())),
                                                row.Cells[9].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[9].Value.ToString()),
                                                row.Cells[11].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[11].Value.ToString()),
                                                row.Cells[10].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[10].Value.ToString()),
                                                row.Cells[12].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[12].Value.ToString()),
                                                row.Cells[13].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                                                row.Cells[14].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[14].Value.ToString()),
                                                0, 0, 0,
                                                Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                                                (Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()) + (row.Cells[19].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[19].Value.ToString()))),
                                                Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                                                row.Cells[18].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()),
                                                "NULL",
                                                row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                                row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                                row.Cells[27].Value == null ? "NULL" : row.Cells[27].Value.ToString(),
                                                row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                                row.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[29].Value.ToString()),
                                                row.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[30].Value.ToString()),
                                                row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                                row.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[32].Value.ToString()),
                                                row.Cells[34].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[34].Value.ToString()),
                                                row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" || row.Cells[35].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[35].Value.ToString()),
                                                row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" || row.Cells[36].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[36].Value.ToString()),
                                                row.Cells[37].Value == null || row.Cells[37].Value.ToString() == "" ? "-" : row.Cells[37].Value.ToString(),
                                                row.Cells[38].Value == null || row.Cells[38].Value.ToString() == "" ? "-" : row.Cells[38].Value.ToString(),
                                                row.Cells[39].Value == null || row.Cells[39].Value.ToString() == "" ? "-" : row.Cells[39].Value.ToString(),
                                                row.Cells[40].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[40].Value.ToString()),
                                                row.Cells[41].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[41].Value.ToString()),
                                                txtUniqueID.Text, Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[46].Value.ToString()), StatusID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                                    //strQry += DBSp.InsertIntoFabrIcStockLedger(MyID, Convert.ToString(RowIndex + 1), txtEntryNo.Text,
                                    //           Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboProcesser.SelectedValue.ToString()),
                                    //           (row.Cells[ARefID].Value == null ? "0" : row.Cells[ARefID].Value.ToString() == "" ? "0" : row.Cells[ARefID].Value.ToString()),
                                    //           row.Cells[47].Value.ToString(), dtRefDate.Text,
                                    //           Localization.ParseNativeDouble(row.Cells[11].Value.ToString()), Localization.ParseNativeDouble(row.Cells[10].Value.ToString()),
                                    //           Localization.ParseNativeDouble(row.Cells[12].Value.ToString()), Localization.ParseNativeDouble(row.Cells[UnitID].Value.ToString()),
                                    //           0, 0, 0, Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                    //           row.Cells[17].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                                    //           row.Cells[3].Value.ToString(), "null", row.Cells[GradeID].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[GradeID].Value.ToString()),
                                    //           Localization.ParseNativeInt(row.Cells[SubDepartmentID].Value.ToString()), txtUniqueID.Text,
                                    //           Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[46].Value.ToString()), StatusID,
                                    //           row.Cells[InwLedID].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[InwLedID].Value.ToString()),
                                    //           row.Cells[InwTransID].Value == null ? "0" : row.Cells[InwTransID].Value.ToString(),
                                    //           0, Localization.ParseNativeInt(cboProcessType.SelectedValue.ToString()),
                                    //           row.Cells[MainRefID].Value == null ? "0" : row.Cells[MainRefID].Value.ToString(),
                                    //           (row.Cells[18].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[18].Value.ToString())), 0,
                                    //            row.Cells[ProductionOrdID].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[ProductionOrdID].Value.ToString()),
                                    //           Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
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
                            if ((Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select COUNT(0) From fn_StockFabricLedger_tbl() Where RefId='" + fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[42].Value + "' and RefID<>'' and Transtype<>" + iIDentity + ""))) > 0))
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityWarning, "", "Reference Found In Another Module..Row Cannot Be Deleted");
                                return;
                            }
                            else
                            {
                                try
                                {
                                    string strQry = string.Format("Update tbl_StockFabricLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[46].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                                    DB.ExecuteSQL(strQry);
                                }
                                catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
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
                            }
                        }
                        catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                    }
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        try
                        {
                            try
                            {
                                string strQry = string.Format("Update tbl_StockFabricLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[46].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                                DB.ExecuteSQL(strQry);
                            }
                            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }

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
                if (sMappingID != "")
                {
                    string[] arr = sMappingID.Split(',');
                    for (int i = 0; i <= arr.Length - 1; i++)
                    {
                        RefVoucherID += DB.GetSnglValue(string.Format("Select VoucherTypeID From fn_MenuMaster_tbl() Where MenuID=" + arr[i] + "")) + ",";
                    }
                    RefVoucherID = RefVoucherID.Remove(RefVoucherID.Length - 1);
                }
                else
                {
                    RefVoucherID = "0";
                }
            }
            catch { }
            #endregion
        }

        private void setMyID_Stock()
        {
            iMaxMyID_Stock = Localization.ParseNativeInt(DB.GetSnglValue("Select MAX(MyId + 1) from tbl_StockFabricLedger Where IsDeleted=0"));

            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[45].Value = iMaxMyID_Stock;
            }
        }

        private void setTempRowIndex()
        {
            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[46].Value = i;
            }
        }

        private void frmFabricReceipt_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (strUniqueID != null)
            {
                string strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                strQry = strQry + string.Format("Update tbl_StockFabricLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                DB.ExecuteSQL(strQry);
                strQry = string.Format("Update tbl_StockFabricLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                DB.ExecuteSQL(strQry);
                strQry = "";
            }
        }
    }
}
