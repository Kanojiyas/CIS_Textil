using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_DBLayer;
using CIS_Bussiness;
using CIS_CLibrary;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class frmEmpFabricReceipt : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        ArrayList OrgInGridArray = new ArrayList();

        public bool NewPieceNo;
        private int iMaxMyID_Stock;
        public string strUniqueID;

        public frmEmpFabricReceipt()
        {
            InitializeComponent();

            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
        }

        #region Event

        private void frmEmpFabricReceipt_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboProcessType, Combobox_Setup.ComboType.Mst_FabricProcessType, "");
                Combobox_Setup.FillCbo(ref cboProcesser, Combobox_Setup.ComboType.Mst_Dyer, "");
                Combobox_Setup.FillCbo(ref cboBroker, Combobox_Setup.ComboType.Mst_Brokers, "");
                Combobox_Setup.FillCbo(ref CboDepartment, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);

                NewPieceNo = Localization.ParseBoolean(GlobalVariables.FR_N_PNo);
                txtEntryNo.Enabled = false;

                cboProcessType.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MiscID From fn_MiscMaster_tbl() Where MiscName='Embroidery'")));
                cboProcessType.Enabled = false;

                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls.CellEndEdit += new DataGridViewCellEventHandler(this.fgDtls_CellEndEdit);
                this.fgDtls.KeyDown += new KeyEventHandler(this.fgDtls_KeyDown);
                this.fgDtls.RowsAdded += new DataGridViewRowsAddedEventHandler(this.fgDtls_RowsAdded);
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
                DBValue.Return_DBValue(this, txtCode, "EmbFabReceiptID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtRefNo, "RefNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtRefDate, "RefDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboProcessType, "ProcessTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboProcesser, "ProcesserID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, CboDepartment, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLrNo, "LrNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtLrDate, "LrDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtVehicleNo, "VehicalNo", Enum_Define.ValidationType.Text);
                //DBValue.Return_DBValue(this, cboAcType, "ProcessAcID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEd1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "EmbFabReceiptID", txtCode.Text, base.dt_HasDtls_Grd, this.iIDentity, GlobalVariables.VALIDATE_EDIT, Db_Detials.CompID.ToString(), 1);

                int icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_StockFabricLedger_tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));
                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls_footer, fgDtls_footer, fgDtls_footer.Grid_ID.ToString(), fgDtls_footer.Grid_UID);
                    setTempRowIndex();
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
                        strQry = strQry + string.Format("Update tbl_StockFabricLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                        DB.ExecuteSQL(strQry);

                        strQry = string.Format("Update tbl_StockFabricLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                        DB.ExecuteSQL(strQry);
                    }
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

                bool RtnValue = Localization.ParseBoolean(DB.GetSnglValue("Select [dbo].[fn_ChkDel_EmbFabricReceipt](" + txtCode.Text + ")"));
                if (RtnValue == true)
                {
                    CboDepartment.Enabled = false;
                }
                else
                {
                    CboDepartment.Enabled = true;
                }

                SetoldPieceNo();
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
                this.txtRefNo.Text = CommonCls.AutoInc(this, "RefNo", "EmbFabReceiptID", "");
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                int MaxID = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(EmbFabReceiptID),0) From {0}  Where IsDeleted=0 and StoreID={1} and  CompID = {2} and BranchID={3} and YearID = {4}", "tbl_EmbFabricReceiptMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID))));
                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where IsDeleted=0 and  EmbFabReceiptID = {1} and StoreID={2} and CompID={3} and BranchID={4} and YearID={5}", new object[] { "tbl_EmbFabricReceiptMain", MaxID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                        dtRefDate.Text = (Localization.ToVBDateString(reader["RefDate"].ToString()));
                        cboProcessType.SelectedValue = Localization.ParseNativeDouble(reader["ProcessTypeID"].ToString());
                        cboProcesser.SelectedValue = Localization.ParseNativeDouble(reader["ProcesserID"].ToString());
                        cboBroker.SelectedValue = Localization.ParseNativeDouble(reader["BrokerID"].ToString());
                        CboDepartment.SelectedValue = Localization.ParseNativeDouble(reader["DepartmentID"].ToString());
                        cboTransport.SelectedValue = Localization.ParseNativeDouble(reader["TransportID"].ToString());
                    }
                }
                cboProcessType.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MiscID From fn_MiscMaster_tbl() Where MiscName='Embroidery'")));
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                if (NewPieceNo)
                {
                    if (((fgDtls.RowCount > 0) & (fgDtls.ColumnCount > 0)) & fgDtls.Columns[4].Visible)
                    {
                        fgDtls.Rows[0].Cells[4].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select  {0}({1},{2},{3},{4},{5},{6})", new object[] { "dbo.fn_FetchPieceNo", MaxID, base.iIDentity, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })), Db_Detials.PCS_NO_INCMT);
                    }
                    else
                    {
                        fgDtls.Rows[0].Cells[4].Value = "-";
                    }
                }
                dtEntryDate.Focus();
                AplySelectBtnEnbl();
                txtUniqueID.Text = CommonCls.GenUniqueID();
                strUniqueID = txtUniqueID.Text;
                cboProcesser.Enabled = true;
                CboDepartment.Enabled = true;
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
                CIS_ReportTool.frmMultiPrint.TblNm = "tbl_EmbFabricReceiptMain";
                CIS_ReportTool.frmMultiPrint.TblNm_D = "tbl_EmbFabricReceiptDtls";
                CIS_ReportTool.frmMultiPrint.IdStr = "EmbFabReceiptID";

                CIS_ReportTool.frmMultiPrint.iStoreID = Db_Detials.StoreID;
                CIS_ReportTool.frmMultiPrint.iCompID = Db_Detials.CompID;
                CIS_ReportTool.frmMultiPrint.iBranchID = Db_Detials.BranchID;
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
            int PartyID = 0;
            try
            {
                ArrayList pArrayData = new ArrayList
                {
                this.frmVoucherTypeID,
                ("(#ENTRYNO#)"),
                (dtEntryDate.TextFormat(false, true)),
                ("(#OTHERNO#)"),
                (dtRefDate.TextFormat(false, true)),
                (cboProcessType.SelectedValue),
                (cboProcesser.SelectedValue),
                (cboBroker.SelectedValue),
                (CboDepartment.SelectedValue),
                (cboTransport.SelectedValue),
                (txtVehicleNo.Text),
                (txtLrNo.Text.ToString()),
                (dtLrDate.TextFormat(false, true)),
                (txtVehicleNo.Text.ToString()),
                (string.Format("{0:N0}", CommonCls.GetColSum(this.fgDtls, 15, -1, -1)).ToString().Replace(",", "")),
                (string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 16, -1, -1)).ToString().Replace(",", "")),
                (txtDescription.Text.ToString()),
                cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue,
                cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue,
                dtEd1.TextFormat(false,true), 
                txtET1.Text,
                txtET2.Text,
                txtET3.Text
                };
                int UnitID = 0;
                int ShrinkageID = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format("Select LedgerId From {0} Where LedgerName = 'SHORTAGE ACCOUNT' ", "tbl_LedgerMaster"))));

                string strAdjQry = string.Empty;
                for (int j = 0; j <= fgDtls.RowCount - 1; j++)
                {
                    DataGridViewRow row = this.fgDtls.Rows[j];
                    //if (row2.Cells[16].Value != null)
                    {
                        if (Localization.ParseNativeDouble(row.Cells[16].Value.ToString()) > 0)
                        {
                            string OldQualityID = DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = " + row.Cells[6].Value.ToString(), "tbl_FabricDesignMaster"));

                            strAdjQry += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                "(#CodeID#)", (j + 1).ToString(), "(#ENTRYNO#)", dtRefDate.Text,
                                Localization.ParseNativeDouble(CboDepartment.SelectedValue.ToString()),
                                Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (j + 1).ToString(),
                                row.Cells[44].Value == null ? "NULL" : row.Cells[44].Value.ToString().Trim() == "" ? "NULL" : row.Cells[44].Value.ToString(),
                                row.Cells[3].Value == null ? "NULL" : row.Cells[3].Value.ToString().Trim() == "" ? "NULL" : row.Cells[3].Value.ToString(),
                                row.Cells[4].Value == null ? "NULL" : row.Cells[4].Value.ToString().Trim() == "" ? "NULL" : row.Cells[4].Value.ToString(),
                                row.Cells[5].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[5].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[14].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                                0, 0, 0, Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()), "NULL",
                                row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                row.Cells[27].Value == null ? "NULL" : row.Cells[27].Value.ToString().Trim() == "" ? "NULL" : row.Cells[27].Value.ToString(),
                                row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                row.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[29].Value.ToString()),
                                row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                row.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[32].Value.ToString()),
                                row.Cells[33].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[33].Value.ToString()),
                                row.Cells[34].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[34].Value.ToString()),
                                row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" || row.Cells[35].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[35].Value.ToString()),
                                row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" || row.Cells[36].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[36].Value.ToString()),
                                row.Cells[37].Value == null || row.Cells[37].Value.ToString() == "" ? "-" : row.Cells[37].Value.ToString(),
                                row.Cells[38].Value == null || row.Cells[38].Value.ToString() == "" ? "-" : row.Cells[38].Value.ToString(),
                                row.Cells[39].Value == null || row.Cells[39].Value.ToString() == "" ? "-" : row.Cells[39].Value.ToString(),
                                row.Cells[40].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[40].Value.ToString()),
                                row.Cells[41].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[41].Value.ToString()),
                                "NULL", j, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);


                            if (row.Cells[19].Value != null)
                            {
                                if (Localization.ParseNativeDouble(row.Cells[19].Value.ToString()) > 0)
                                {
                                    strAdjQry += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                        "(#CodeID#)", (j + 1).ToString(), "(#ENTRYNO#)", dtRefDate.Text,
                                        Localization.ParseNativeDouble(ShrinkageID.ToString()),
                                        Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                        base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (j + 1).ToString(),
                                        row.Cells[44].Value == null ? "NULL" : row.Cells[44].Value.ToString().Trim() == "" ? "NULL" : row.Cells[44].Value.ToString(),
                                        row.Cells[3].Value == null ? "NULL" : row.Cells[3].Value.ToString().Trim() == "" ? "NULL" : row.Cells[3].Value.ToString(),
                                        row.Cells[4].Value == null ? "NULL" : row.Cells[4].Value.ToString().Trim() == "" ? "NULL" : row.Cells[4].Value.ToString(),
                                        row.Cells[5].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[5].Value.ToString()),
                                        Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                        Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                        Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                        Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                                        Localization.ParseNativeDouble(row.Cells[14].Value.ToString()),
                                        Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                                        Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                        Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                                        0, 0, 0, Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()), "NULL",
                                        row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                        row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                        row.Cells[27].Value == null ? "NULL" : row.Cells[27].Value.ToString().Trim() == "" ? "NULL" : row.Cells[27].Value.ToString(),
                                        row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                        row.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[29].Value.ToString()),
                                        row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                        row.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[32].Value.ToString()),
                                        row.Cells[33].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[33].Value.ToString()),
                                        row.Cells[34].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[34].Value.ToString()),
                                        row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" || row.Cells[35].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[35].Value.ToString()),
                                        row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" || row.Cells[36].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[36].Value.ToString()),
                                        row.Cells[37].Value == null || row.Cells[37].Value.ToString() == "" ? "-" : row.Cells[37].Value.ToString(),
                                        row.Cells[38].Value == null || row.Cells[38].Value.ToString() == "" ? "-" : row.Cells[38].Value.ToString(),
                                        row.Cells[39].Value == null || row.Cells[39].Value.ToString() == "" ? "-" : row.Cells[39].Value.ToString(),
                                        row.Cells[40].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[40].Value.ToString()),
                                        row.Cells[41].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[41].Value.ToString()),
                                        "NULL", j, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                                }
                            }
                            strAdjQry += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                       "(#CodeID#)", (j + 1).ToString(), "(#ENTRYNO#)", dtRefDate.Text,
                                       Localization.ParseNativeDouble(cboProcesser.SelectedValue.ToString()),
                                       Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                       base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (j + 1).ToString(),
                                       row.Cells[44].Value == null ? "NULL" : row.Cells[44].Value.ToString().Trim() == "" ? "NULL" : row.Cells[44].Value.ToString(),
                                       row.Cells[3].Value == null ? "NULL" : row.Cells[3].Value.ToString().Trim() == "" ? "NULL" : row.Cells[3].Value.ToString(),
                                       row.Cells[4].Value == null ? "NULL" : row.Cells[4].Value.ToString().Trim() == "" ? "NULL" : row.Cells[4].Value.ToString(),
                                       row.Cells[5].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[5].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                       Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[14].Value.ToString()),
                                       0, 0, 0,
                                       Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                                       Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                       Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                                       Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()), "NULL",
                                       row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                       row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                       row.Cells[27].Value == null ? "NULL" : row.Cells[27].Value.ToString().Trim() == "" ? "NULL" : row.Cells[27].Value.ToString(),
                                       row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                       cboProcessType.SelectedValue == null ? 0 : Localization.ParseNativeInt(cboProcessType.SelectedValue.ToString()),
                                       row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                       row.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[32].Value.ToString()),
                                       row.Cells[33].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[33].Value.ToString()),
                                       row.Cells[34].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[34].Value.ToString()),
                                       row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" || row.Cells[35].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[35].Value.ToString()),
                                       row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" || row.Cells[36].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[36].Value.ToString()),
                                       row.Cells[37].Value == null || row.Cells[37].Value.ToString() == "" ? "-" : row.Cells[37].Value.ToString(),
                                       row.Cells[38].Value == null || row.Cells[38].Value.ToString() == "" ? "-" : row.Cells[38].Value.ToString(),
                                       row.Cells[39].Value == null || row.Cells[39].Value.ToString() == "" ? "-" : row.Cells[39].Value.ToString(),
                                       row.Cells[40].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[40].Value.ToString()),
                                       row.Cells[41].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[41].Value.ToString()),
                                       "NULL", j, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                            OldQualityID = "0";
                            UnitID = Localization.ParseNativeInt(row.Cells[14].Value.ToString());
                            PartyID = Localization.ParseNativeInt(row.Cells[26].Value.ToString());
                        }
                    }
                    row = null;
                }

                strAdjQry += "Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";";

                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                DBSp.Transcation_AddEdit(pArrayData, fgDtls, true, strAdjQry, "", txtEntryNo.Text, txtRefNo.Text, "RefNo");

                #region Embroidery Delivery Challan
                if (ChkAutoGenChallan.Checked == true)
                {
                    string sQry = string.Empty;
                    string sQryDtls = string.Empty;
                    string sQryStock = string.Empty;

                    int iMaxReceiptID = Localization.ParseNativeInt(DB.GetSnglValue("SELECT MAX(EmbFabReceiptID) FROM tbl_EmbFabricReceiptMain Where IsDeleted=0"));
                    int iPartyID = Localization.ParseNativeInt(DB.GetSnglValue("SELECT DepartmentID From tbl_EmbFabricReceiptDtls Where EmbFabReceiptID=" + iMaxReceiptID));
                    int iMenuID = Localization.ParseNativeInt(DB.GetSnglValue("SELECT MenuID From tbl_MenuMaster Where FormCall='frmEmbFabricOutward'"));
                    int iMaxEntryNo = Localization.ParseNativeInt(DB.GetSnglValue("SELECT ISNULL(MAX(EntryNo),0)+1  As EntryNo From tbl_EmbFabricOutwardMain Where IsDeleted=0"));

                    using (IDataReader idr = DB.GetRS("SELECT * FROM tbl_EmbFabricReceiptMain Where EmbFabReceiptID=" + iMaxReceiptID))
                    {
                        while (idr.Read())
                        {
                            #region Main
                            sQry = string.Format("INSERT INTO tbl_EmbFabricOutwardMain (VoucherTypeID,EntryNo,EntryDate,OrderType,RefNo,RefDate,PartyID,BrokerID,DepartmentID,DeliveryAtID,HasteID,TransportID,LRNo,LRDate,BatchNo,Bales,BaleNo,Description1,TotPcs,TotMtrs,TotWt,Description2,EI1,EI2,ED1,ET1,ET2,ET3,StoreID,CompID,BranchID,YearID,AddedOn,AddedBy,IsModified,ModifiedOn,ModifiedBy,IsDeleted,DeletedOn,DeletedBy,IsCanclled,CancelledOn,CancelledBy,IsApproved,ApprovedOn,ApprovedBy,IsAudited,AuditedOn,AuditedBy) VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},Getdate(),{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43},{44},{45},{46})" + Environment.NewLine,
                                    this.frmVoucherTypeID,
                                    iMaxEntryNo,
                                    CommonLogic.SQuote(Localization.ToSqlDateString(idr["EntryDate"].ToString())),
                                    0,
                                    CommonLogic.SQuote(idr["RefNo"].ToString()),
                                    CommonLogic.SQuote(Localization.ToSqlDateString(idr["RefDate"].ToString())),
                                    iPartyID,
                                    Localization.ParseNativeInt(idr["BrokerID"].ToString()),
                                    Localization.ParseNativeInt(idr["DepartmentID"].ToString()),
                                    Localization.ParseNativeInt(idr["DepartmentID"].ToString()),
                                /*"HasteID"*/0,
                                    Localization.ParseNativeInt(idr["TransportID"].ToString()),
                                    (idr["LrNo"].ToString() == null ? "NULL" : (idr["LrNo"].ToString() == "" ? "NULL" : CommonLogic.SQuote(idr["LrNo"].ToString()))),
                                    CommonLogic.SQuote(Localization.ToSqlDateString(idr["LrDate"].ToString())),
                                /*"BatchNo"*/"NULL",
                                /*"Bales"*/"NULL",
                                /*"BaleNo"*/"NULL",
                                    (idr["Description"].ToString() == null ? "NULL" : (idr["Description"].ToString() == "" ? "NULL" : CommonLogic.SQuote(idr["Description"].ToString()))),
                                    Localization.ParseNativeInt(idr["TotPcs"].ToString()),
                                    Localization.ParseNativeDecimal(idr["TotMtrs"].ToString()),
                                    0,
                                    (idr["Description"].ToString() == null ? "NULL" : (idr["Description"].ToString() == "" ? "NULL" : CommonLogic.SQuote(idr["Description"].ToString()))),
                                    Localization.ParseNativeInt(idr["EI1"].ToString()),
                                    Localization.ParseNativeInt(idr["EI2"].ToString()),
                                    idr["ED1"].ToString() == null ? "0" :idr["ED1"].ToString() == ""?"0": CommonLogic.SQuote(Localization.ToSqlDateString(idr["ED1"].ToString())),
                                    (idr["ET1"].ToString() == null ? "NULL" : (idr["ET1"].ToString() == "" ? "NULL" : CommonLogic.SQuote(idr["ET1"].ToString()))),
                                    (idr["ET2"].ToString() == null ? "NULL" : (idr["ET2"].ToString() == "" ? "NULL" : CommonLogic.SQuote(idr["ET2"].ToString()))),
                                    (idr["ET3"].ToString() == null ? "NULL" : (idr["ET3"].ToString() == "" ? "NULL" : CommonLogic.SQuote(idr["ET3"].ToString()))),
                                    Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID,
                                    0, "NULL", "NULL", 0, "NULL", "NULL", 0, "NULL", "NULL", 0, "NULL", "NULL", 0, "NULL", "NULL");
                            #endregion

                            int iSrNo = 1;
                            sQryStock = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", "(#CodeID#)", iMenuID);

                            using (IDataReader dr = DB.GetRS("SELECT * FROM tbl_EmbFabricReceiptDtls Where EmbFabReceiptID=" + iMaxReceiptID + " Order By SubFabReceiptID ASC"))
                            {
                                while (dr.Read())
                                {
                                    sQryDtls += string.Format("INSERT INTO tbl_EmbFabricOutwardDtls (EmbFabOutwardID,SubEmbFabOutwardID,FabSOID,BatchNo,BarcodeNo,EmbDesignID,FabricID,DesignID,QualityID,ShadeID,GradeID,UnitID,Pcs,Mtrs,Wt,StockValue,BaleNo,InitMtrs,InitWt,DepartmentID,SubDepartmentID,Description,ProductionOrdID,InwLedID,InwTransID,ProcessOrdID,ProcessTypeID,ProgramID,ProcessID,EI1,EI2,EI3,ED1,ED2,ET1,ET2,ET3,EN1,EN2,RefID,ARefID,MainRefID,MyID,TempRowIndex,BalPcs_temp) VALUES ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43},{44},{45})" + Environment.NewLine,
                                             "(#CodeID#)", iSrNo, 0,
                                             (dr["BatchNo"].ToString() == null ? "NULL" : dr["BatchNo"].ToString() == "" ? "NULL" : CommonLogic.SQuote(dr["BatchNo"].ToString())),
                                             (dr["BarcodeNo"].ToString() == null ? "NULL" : dr["BarcodeNo"].ToString() == "" ? "NULL" : CommonLogic.SQuote(dr["BarcodeNo"].ToString())),
                                             (dr["NFabricID"].ToString() == null ? 0 : dr["NFabricID"].ToString() == "" ? 0 : Localization.ParseNativeInt(dr["NFabricID"].ToString())),
                                             (dr["NDesignID"].ToString() == null ? 0 : dr["NDesignID"].ToString() == "" ? 0 : Localization.ParseNativeInt(dr["NDesignID"].ToString())),
                                             (dr["NQualityID"].ToString() == null ? 0 : dr["NQualityID"].ToString() == "" ? 0 : Localization.ParseNativeInt(dr["NQualityID"].ToString())),
                                             (dr["NShadeID"].ToString() == null ? 0 : dr["NShadeID"].ToString() == "" ? 0 : Localization.ParseNativeInt(dr["NShadeID"].ToString())),
                                             (dr["GradeID"].ToString() == null ? 0 : dr["GradeID"].ToString() == "" ? 0 : Localization.ParseNativeInt(dr["GradeID"].ToString())),
                                             (dr["UnitID"].ToString() == null ? 0 : dr["UnitID"].ToString() == "" ? 0 : Localization.ParseNativeInt(dr["UnitID"].ToString())),
                                             (dr["Pcs"].ToString() == null ? 0 : dr["Pcs"].ToString() == "" ? 0 : Localization.ParseNativeInt(dr["Pcs"].ToString())),
                                             (dr["Mtrs"].ToString() == null ? 0 : dr["Mtrs"].ToString() == "" ? 0 : Localization.ParseNativeDecimal(dr["Mtrs"].ToString())),
                                             (dr["Wt"].ToString() == null ? 0 : dr["Wt"].ToString() == "" ? 0 : Localization.ParseNativeDecimal(dr["Wt"].ToString())),
                                             (dr["StockValue"].ToString() == null ? 0 : dr["StockValue"].ToString() == "" ? 0 : Localization.ParseNativeDecimal(dr["StockValue"].ToString())),
                                             "NULL",
                                             "NULL",
                                             "NULL",
                                             (idr["DepartmentID"].ToString() == null ? 0 : (idr["DepartmentID"].ToString() == "" ? 0 : Localization.ParseNativeInt(idr["DepartmentID"].ToString()))),
                                             (dr["SubDepartmentID"].ToString() == null ? 0 : dr["SubDepartmentID"].ToString() == "" ? 0 : Localization.ParseNativeInt(dr["SubDepartmentID"].ToString())),
                                             "NULL",
                                             (dr["ProductionOrdID"].ToString() == null ? 0 : dr["ProductionOrdID"].ToString() == "" ? 0 : Localization.ParseNativeInt(dr["ProductionOrdID"].ToString())),
                                             (dr["InwLedID"].ToString() == null ? 0 : dr["InwLedID"].ToString() == "" ? 0 : Localization.ParseNativeInt(dr["InwLedID"].ToString())),
                                             (dr["InwTransID"].ToString() == null ? 0 : dr["InwTransID"].ToString() == "" ? 0 : Localization.ParseNativeInt(dr["InwTransID"].ToString())),
                                             (dr["ProcessOrdID"].ToString() == null ? 0 : dr["ProcessOrdID"].ToString() == "" ? 0 : Localization.ParseNativeInt(dr["ProcessOrdID"].ToString())),
                                             (dr["ProcessTypeID"].ToString() == null ? 0 : dr["ProcessTypeID"].ToString() == "" ? 0 : Localization.ParseNativeInt(dr["ProcessTypeID"].ToString())),
                                             (dr["ProgramID"].ToString() == null ? "0" : dr["ProgramID"].ToString() == "" ? "0" : dr["ProgramID"].ToString()),
                                             (dr["ProcessID"].ToString() == null ? 0 : dr["ProcessID"].ToString() == "" ? 0 : Localization.ParseNativeInt(dr["ProcessID"].ToString())),
                                             Localization.ParseNativeInt(idr["EI1"].ToString()),
                                             Localization.ParseNativeInt(idr["EI2"].ToString()),
                                             Localization.ParseNativeInt(idr["EI3"].ToString()),
                                             idr["ED1"].ToString() == null ? "0" : CommonLogic.SQuote(Localization.ToSqlDateString(idr["ED1"].ToString())),
                                             idr["ED2"].ToString() == null ? "0" : CommonLogic.SQuote(Localization.ToSqlDateString(idr["ED2"].ToString())),
                                             (idr["ET1"].ToString() == null ? "NULL" : (idr["ET1"].ToString() == "" ? "NULL" : CommonLogic.SQuote(idr["ET1"].ToString()))),
                                             (idr["ET2"].ToString() == null ? "NULL" : (idr["ET2"].ToString() == "" ? "NULL" : CommonLogic.SQuote(idr["ET2"].ToString()))),
                                             (idr["ET3"].ToString() == null ? "NULL" : (idr["ET3"].ToString() == "" ? "NULL" : CommonLogic.SQuote(idr["ET3"].ToString()))),
                                             idr["EN1"].ToString() == null ? 0 : Localization.ParseNativeDecimal(idr["EN1"].ToString()),
                                             idr["EN2"].ToString() == null ? 0 : Localization.ParseNativeDecimal(idr["EN2"].ToString()), "NULL",
                                             (dr["RefID"].ToString() == null ? "NULL" : dr["RefID"].ToString() == "" ? "NULL" : CommonLogic.SQuote(dr["RefID"].ToString())),
                                             (dr["MainRefID"].ToString() == null ? "NULL" : dr["MainRefID"].ToString() == "" ? "NULL" : CommonLogic.SQuote(dr["MainRefID"].ToString())),
                                             (dr["MyID"].ToString() == null ? 0 : dr["MyID"].ToString() == "" ? 0 : Localization.ParseNativeInt(dr["MyID"].ToString())),
                                             iSrNo - 1, (dr["Pcs"].ToString() == null ? 0 : dr["Pcs"].ToString() == "" ? 0 : Localization.ParseNativeInt(dr["Pcs"].ToString()))
                                        );

                                    string LotNo = Conversions.ToString(dr["BatchNo"].ToString());
                                    if (Localization.ParseNativeDouble(Conversions.ToString(dr["Mtrs"].ToString())) != 0.0)
                                    {
                                        if ((LotNo == null) || (LotNo == "0"))
                                        {
                                            LotNo = "-";
                                        }
                                        sQryStock = sQryStock + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(iMenuID.ToString()), "(#CodeID#)", (iSrNo).ToString(), iMaxEntryNo.ToString(),
                                            dtRefDate.Text, Localization.ParseNativeDouble(CboDepartment.SelectedValue.ToString()),
                                            Localization.ParseNativeInt(dr["SubDepartmentID"].ToString()),
                                            (dr["RefID"].ToString() == null ? "NULL" : dr["RefID"].ToString() == "" ? "NULL" : dr["RefID"].ToString()),
                                            (dr["MainRefID"].ToString() == null ? "NULL" : dr["MainRefID"].ToString() == "" ? "NULL" : dr["MainRefID"].ToString()),
                                            (dr["BatchNo"].ToString() == null ? "NULL" : dr["BatchNo"].ToString() == "" ? "NULL" : dr["BatchNo"].ToString()),
                                            dr["BarcodeNo"].ToString() == null ? "-" : dr["BarcodeNo"].ToString() == "" ? "-" : dr["BarcodeNo"].ToString() == "0" ? "-" : CommonLogic.SQuote(dr["BarcodeNo"].ToString()),
                                            Localization.ParseNativeInt(dr["NFabricID"].ToString()),
                                            Localization.ParseNativeDouble(dr["NQualityID"].ToString()),
                                            Localization.ParseNativeDouble(dr["NDesignID"].ToString()),
                                            Localization.ParseNativeDouble(dr["NShadeID"].ToString()),
                                            Localization.ParseNativeInt(dr["GradeID"].ToString()),
                                            Localization.ParseNativeDouble(dr["UnitID"].ToString()), 0, 0, 0, Localization.ParseNativeDecimal(dr["Pcs"].ToString()),
                                            Localization.ParseNativeDecimal(dr["Mtrs"].ToString()), Localization.ParseNativeDecimal(dr["Wt"].ToString()),
                                            (dr["StockValue"].ToString() == null ? 0 : Localization.ParseNativeDecimal(dr["StockValue"].ToString())), "NULL",
                                            dr["ProductionOrdID"].ToString() == null ? 0 : Localization.ParseNativeInt(dr["ProductionOrdID"].ToString()),
                                            dr["InwLedID"].ToString() == null ? 0 : dr["InwLedID"].ToString() == null ? 0 : Localization.ParseNativeInt(dr["InwLedID"].ToString()),
                                            dr["InwTransID"].ToString() == null ? "NULL" : dr["InwTransID"].ToString() == "" ? "NULL" : CommonLogic.SQuote(dr["InwTransID"].ToString()),
                                            Localization.ParseNativeInt(dr["ProcessOrdID"].ToString()),
                                            Localization.ParseNativeInt(dr["ProcessTypeID"].ToString()),
                                            Localization.ParseNativeInt(dr["ProcessID"].ToString()),
                                            Localization.ParseNativeInt(idr["EI1"].ToString()),
                                             Localization.ParseNativeInt(idr["EI2"].ToString()),
                                             Localization.ParseNativeInt(idr["EI3"].ToString()),
                                             CommonLogic.SQuote(Localization.ToSqlDateString(idr["ED1"].ToString())),
                                             CommonLogic.SQuote(Localization.ToSqlDateString(idr["ED2"].ToString())),
                                             (idr["ET1"].ToString() == null ? "NULL" : (idr["ET1"].ToString() == "" ? "NULL" : CommonLogic.SQuote(idr["ET1"].ToString()))),
                                             (idr["ET2"].ToString() == null ? "NULL" : (idr["ET2"].ToString() == "" ? "NULL" : CommonLogic.SQuote(idr["ET2"].ToString()))),
                                             (idr["ET3"].ToString() == null ? "NULL" : (idr["ET3"].ToString() == "" ? "NULL" : CommonLogic.SQuote(idr["ET3"].ToString()))),
                                             Localization.ParseNativeDecimal(idr["EN1"].ToString()),
                                             Localization.ParseNativeDecimal(idr["EN2"].ToString()),
                                             "NULL", (iSrNo - 1), 1,
                                             Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                                        PartyID = Localization.ParseNativeInt(dr["InwLedID"].ToString());
                                    }
                                    iSrNo++;
                                }
                            }

                            //if (cboTransport.SelectedValue != null && Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()) > 0.0)
                            //{
                            //    sQryStock = sQryStock + DBSp.InsertIntoTrasportLedger("(#CodeID#)", txtRefNo.Text, dtRefDate.Text,
                            //        Localization.ParseNativeDouble(iMenuID.ToString()), Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()),
                            //        Localization.ParseNativeDouble(CboDepartment.SelectedValue.ToString()), Localization.ParseNativeDouble(PartyID.ToString()), txtLrNo.Text,
                            //        dtLrDate.Text, null, Localization.ParseNativeDouble(UnitID.ToString()), Localization.ParseNativeInt(string.Format("{0:N0}", CommonCls.GetColSum(this.fgDtls, 15, -1, -1))),
                            //        Localization.ParseNativeDecimal(string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 16, -1, -1))), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                            //}
                        }
                        sQryStock += "Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";";
                        sQryStock = sQryStock.Replace("'null'", "null").Replace("Nnull", "null");
                    }
                    DB.ExecuteTranscation(sQry, "", true, sQryDtls + sQryStock);
                }
                #endregion

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

                if (!EventHandles.IsValidGridReq(this.fgDtls, base.dt_AryIsRequired))
                {
                    return true;
                }

                if (!EventHandles.IsRequiredInGrid(fgDtls, this.dt_AryIsRequired, false))
                {
                    return true;
                }

                string strTblName;
                if (txtEntryNo.Text.Trim() == "" || txtEntryNo.Text.Trim() == "-" || txtEntryNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry No.");
                    txtEntryNo.Focus();
                    return true;
                }

                if (!Information.IsDate(dtEntryDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry Date");
                    dtEntryDate.Focus();
                    return true;
                }

                if (!CommonCls.CheckDate(dtEntryDate.Text, true))
                    return true;

                if (txtRefNo.Text.Trim() == "" || txtRefNo.Text.Trim() == "-" || txtRefNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Challan No.");
                    txtRefNo.Focus();
                    return true;
                }

                if (!Information.IsDate(dtRefDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Challan Date");
                    dtRefDate.Focus();
                    return true;
                }

                if (!CommonCls.CheckDate(dtRefDate.Text, true))
                    return true;


                if (!CommonCls.CheckDate(dtLrDate.Text, true))
                    return true;

                if (cboProcessType.SelectedValue == null || cboProcessType.Text.Trim().ToString() == "-" || cboProcessType.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Process Type.");
                    cboProcessType.Focus();
                    return true;
                }

                if (cboProcesser.SelectedValue == null || cboProcesser.Text.Trim().ToString() == "-" || cboProcesser.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Processer.");
                    cboProcesser.Focus();
                    return true;
                }

                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    fgDtls.Rows[i].Cells[23].Value = CboDepartment.SelectedValue;
                }

                if (CboDepartment.SelectedValue == null || CboDepartment.Text.Trim().ToString() == "-" || CboDepartment.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    CboDepartment.Focus();
                    return true;
                }

                if (txtRefNo.Text.Trim().Length > 0)
                {
                    if (base.blnFormAction == 0)
                    {
                        strTblName = "tbl_EmbFabricReceiptMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "RefNo", txtRefNo.Text, false, "", 0, " ProcesserID = " + cboProcesser.SelectedValue + " and StoreID=" + Db_Detials.StoreID + " AND CompID =" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID = " + Db_Detials.YearID + "", "This Processor already used this Challan No in Entry No : " + DB.GetSnglValue(string.Format("Select Entryno from {0} where ProcesserID = {1} and RefNo = '{2}' ", "tbl_EmbFabricReceiptMain", cboProcesser.SelectedValue, txtRefNo.Text.ToString()))))
                        {
                            txtRefNo.Focus();
                            return true;
                        }
                    }
                    else if (Localization.ParseNativeInt(base.blnFormAction.ToString()) == 1)
                    {
                        strTblName = "tbl_EmbFabricReceiptMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "RefNo", txtRefNo.Text, true, "EmbFabReceiptID", Localization.ParseNativeLong(txtCode.Text.Trim()), " ProcesserID = " + cboProcesser.SelectedValue + " and StoreID=" + Db_Detials.StoreID + " and CompID =" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + "  And YearID = " + Db_Detials.YearID + "", "This Processor already used this Challan No in Entry No : " + DB.GetSnglValue(string.Format("Select Entryno from {0} where ProcesserID = {1} and RefNo = '{2}' ", "tbl_EmbFabricReceiptMain", cboProcesser.SelectedValue, txtRefNo.Text.ToString()))))
                        {
                            txtRefNo.Focus();
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

                if (cboProcesser.SelectedValue == null || cboProcesser.SelectedValue.ToString() == "" || cboProcesser.SelectedValue.ToString() == "-" || cboProcesser.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Processor.");
                    cboProcesser.Focus();
                    return;
                }
                else
                {
                    #region StockAdjQuery
                    string strarray = "";
                    string strQry = string.Empty;
                    int ibitcol = 0;
                    string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", iIDentity, 0.0));
                    string strQry_ColName = "";
                    string[] arr = CommonCls.GetAdjColName(base.iIDentity, 0.0).Split(';');
                    strQry_ColName = arr[0].ToString();
                    strQry = string.Format(" Select {0} From {1} ({2}, {3},{4},{5}) Where  BatchNo <> '-' ", new object[] { strQry_ColName, snglValue, this.cboProcesser.SelectedValue, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID });
                    ibitcol = Localization.ParseNativeInt(arr[1]);

                    #endregion

                    frmStockAdj frmStockAdj = new frmStockAdj();
                    frmStockAdj.MenuID = base.iIDentity;
                    frmStockAdj.Entity_IsfFtr = 0.0;
                    frmStockAdj.ref_fgDtls = this.fgDtls;
                    frmStockAdj.QueryString = strQry;
                    frmStockAdj.IsRefQuery = true;
                    frmStockAdj.ibitCol = ibitcol;
                    frmStockAdj.LedgerID = Conversions.ToString(this.cboProcesser.SelectedValue);
                    frmStockAdj.UsedInGridArray = this.OrgInGridArray;
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

                                    if (fgDtls.Rows[i].Cells[47].Value != null)
                                    {
                                        if (Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[47].Value.ToString()) < iPcs)
                                        {
                                            iPcs = Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[47].Value.ToString());
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
                                                else if (m != 4 && m != 16)
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
                                    fgDtls.Rows[i].Cells[15].Value = fgDtls.Rows[i].Cells[47].Value.ToString();
                                }
                            }
                        }

                        fgDtls.Rows.RemoveAt(fgDtls.RowCount - 1);
                        DataGridViewEx ex2 = fgDtls;
                        for (int j = 0; j <= ex2.RowCount - 1; j++)
                        {
                            if (!NewPieceNo)
                            {
                                ex2.Rows[j].Cells[4].Value = ex2.Rows[j].Cells[48].Value;
                            }
                            else if (j == 0)
                            {
                                int MaxId = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(EmbFabReceiptID),0) From {0} where IsDeleted=0 ", "tbl_EmbFabricReceiptMain"))));
                                fgDtls.Rows[0].Cells[4].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select  {0}({1},{2},{3},{4},{5},{6})", new object[] { "dbo.fn_FetchPieceNo", MaxId, base.iIDentity, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })), Db_Detials.PCS_NO_INCMT);
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
                            ex2.Rows[j].Cells[10].Value = Localization.ParseNativeInt(ex2.Rows[j].Cells[6].Value.ToString());
                            ex2.Rows[j].Cells[12].Value = Localization.ParseNativeInt(ex2.Rows[j].Cells[8].Value.ToString());
                            ex2.Rows[j].Cells[11].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", ex2.Rows[j].Cells[10].Value)));
                            //ex2.Rows[j].Cells[15].Value = 1;
                        }
                        SetoldPieceNo();
                        SendKeys.Send("{TAB}");
                        if (fgDtls.Rows.Count > 0)
                        {
                            fgDtls.CurrentCell = fgDtls[15, 0];
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

        private void cboProcesser_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if ((this.cboProcesser.SelectedValue != null) && (Conversion.Val(RuntimeHelpers.GetObjectValue(cboProcesser.SelectedValue)) > 0.0))
                {
                    cboBroker.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select BrokerID From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboProcesser.SelectedValue)));
                    cboTransport.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select TransportId From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboProcesser.SelectedValue)));
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
                    string strTblname;
                    if (blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        string strVal = fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString();
                        if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString().Length > 0)
                        {
                            if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString() != "-")
                            {
                                strTblname = "tbl_StockFabricLedger";
                                if (Navigate.CheckDuplicate(ref strTblname, "BatchNo", strVal, false, "", 0, "StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
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
                                if (Navigate.CheckDuplicate(ref strTblname, "BatchNo", fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString(), true, "TransID", Localization.ParseNativeLong(txtCode.Text.Trim()), "StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
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

        private void fgDtls_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            //try
            //{
            //    if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
            //    {
            //        fgDtls.Rows[e.RowIndex].Cells[29].Value = Localization.ParseNativeInt(cboProcessType.SelectedValue.ToString());
            //    }
            //}
            //catch { }
        }

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    try
                    {
                        if ((e.ColumnIndex == 15) | (e.ColumnIndex == 16) | (e.ColumnIndex == 17))
                        {
                            ExecuterTempQry(e.RowIndex);
                        }
                    }
                    catch (Exception ex)
                    {
                        Navigate.logError(ex.Message, ex.StackTrace);
                    }

                    switch (e.ColumnIndex)
                    {
                        case 6:
                            fgDtls.Rows[e.RowIndex].Cells[10].Value = fgDtls.Rows[e.RowIndex].Cells[6].Value;
                            break;

                        case 8:
                            fgDtls.Rows[e.RowIndex].Cells[12].Value = fgDtls.Rows[e.RowIndex].Cells[8].Value;
                            break;

                        case 7:
                            fgDtls.Rows[e.RowIndex].Cells[11].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", fgDtls.Rows[e.RowIndex].Cells[10].Value)));
                            break;
                    }
                }

                if (fgDtls.RowCount > 1)
                {
                    cboProcesser.Enabled = false;
                }
                else
                {
                    cboProcesser.Enabled = true;
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void SetoldPieceNo()
        {
            try
            {
                if (blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (txtCode.Text.Trim() != "")
                    {
                        using (IDataReader reader = DB.GetRS(string.Format(" select SubTransID, Batchno from {0} where IsDeleted=0 and transid = {1} and transtype = {2} and cr_mtrs <> 0 Order By SubTransID", "tbl_StockFabricLedger", txtCode.Text, base.iIDentity)))
                        {
                            while (reader.Read())
                            {
                                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                                {
                                    if (Localization.ParseNativeInt(fgDtls.Rows[i].Cells[1].Value.ToString()) == Localization.ParseNativeInt(reader["SubTransID"].ToString()))
                                    {
                                        fgDtls.Rows[i].Cells[48].Value = reader["BatchNo"].ToString();
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

        private void cboProcessType_SelectedValueChanged(object sender, EventArgs e)
        {
            EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
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


                                string OldQualityID = DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = " + row.Cells[6].Value.ToString(), "tbl_FabricDesignMaster"));
                                if (MyID != "" && row.Cells[16].Value != null && row.Cells[16].Value.ToString() != "" && row.Cells[16].Value.ToString() != "0" && row.Cells[15].Value != null && row.Cells[15].Value.ToString() != "" && row.Cells[15].Value.ToString() != "0")
                                {
                                    strQry = strQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                       MyID, (i + 1).ToString(), txtEntryNo.Text, dtRefDate.Text,
                                       Localization.ParseNativeDouble(cboProcesser.SelectedValue.ToString()),
                                       Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                       (row.Cells[43].Value == null ? "0" : row.Cells[43].Value.ToString() == "" ? "0" : row.Cells[43].Value.ToString()),
                                       row.Cells[44].Value == null ? "NULL" : row.Cells[44].Value.ToString().Trim() == "" ? "NULL" : row.Cells[44].Value.ToString(),
                                       row.Cells[3].Value == null ? "NULL" : row.Cells[3].Value.ToString().Trim() == "" ? "NULL" : row.Cells[3].Value.ToString(),
                                       row.Cells[4].Value == null ? "NULL" : row.Cells[4].Value.ToString().Trim() == "" ? "NULL" : row.Cells[4].Value.ToString(),
                                       row.Cells[5].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[5].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                       Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[14].Value.ToString()),
                                       0, 0, 0,
                                       Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                                       Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                       row.Cells[17].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                                       row.Cells[18].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()), "NULL",
                                       row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                       row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                       row.Cells[27].Value == null ? "NULL" : row.Cells[27].Value.ToString().Trim() == "" ? "NULL" : row.Cells[27].Value.ToString(),
                                       row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                       cboProcessType.SelectedValue == null ? 0 : Localization.ParseNativeInt(cboProcessType.SelectedValue.ToString()),
                                       row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                       row.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[32].Value.ToString()),
                                       row.Cells[33].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[33].Value.ToString()),
                                       row.Cells[34].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[34].Value.ToString()),
                                       row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" || row.Cells[35].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[35].Value.ToString()),
                                       row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" || row.Cells[36].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[36].Value.ToString()),
                                       row.Cells[37].Value == null || row.Cells[37].Value.ToString() == "" ? "-" : row.Cells[37].Value.ToString(),
                                       row.Cells[38].Value == null || row.Cells[38].Value.ToString() == "" ? "-" : row.Cells[38].Value.ToString(),
                                       row.Cells[39].Value == null || row.Cells[39].Value.ToString() == "" ? "-" : row.Cells[39].Value.ToString(),
                                       row.Cells[40].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[40].Value.ToString()),
                                       row.Cells[41].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[41].Value.ToString()),
                                       txtUniqueID.Text, i, StatusID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID,
                                       Db_Detials.UserID, DateAndTime.Now.Date);
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
                                    string OldQualityID = DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = " + row.Cells[6].Value.ToString(), "tbl_FabricDesignMaster"));
                                    strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[46].Value.ToString()) + " and AddedBy=" + Db_Detials.UserID + ";");

                                    strQry = strQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                       MyID, (RowIndex + 1).ToString(), txtEntryNo.Text, dtRefDate.Text,
                                       Localization.ParseNativeDouble(cboProcesser.SelectedValue.ToString()),
                                       Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                       (row.Cells[43].Value == null ? "0" : row.Cells[43].Value.ToString() == "" ? "0" : row.Cells[43].Value.ToString()),
                                       row.Cells[44].Value == null ? "NULL" : row.Cells[44].Value.ToString().Trim() == "" ? "NULL" : row.Cells[44].Value.ToString(),
                                       row.Cells[3].Value == null ? "NULL" : row.Cells[3].Value.ToString().Trim() == "" ? "NULL" : row.Cells[3].Value.ToString(),
                                       row.Cells[4].Value == null ? "NULL" : row.Cells[4].Value.ToString().Trim() == "" ? "NULL" : row.Cells[4].Value.ToString(),
                                       row.Cells[5].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[5].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                       Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[14].Value.ToString()),
                                       0, 0, 0,
                                       Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                                       Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                       row.Cells[17].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                                       row.Cells[18].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()), "NULL",
                                       row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                       row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                       row.Cells[27].Value == null ? "NULL" : row.Cells[27].Value.ToString().Trim() == "" ? "NULL" : row.Cells[27].Value.ToString(),
                                       row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                       cboProcessType.SelectedValue == null ? 0 : Localization.ParseNativeInt(cboProcessType.SelectedValue.ToString()),
                                       row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                       row.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[32].Value.ToString()),
                                       row.Cells[33].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[33].Value.ToString()),
                                       row.Cells[34].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[34].Value.ToString()),
                                       row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" || row.Cells[35].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[35].Value.ToString()),
                                       row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" || row.Cells[36].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[36].Value.ToString()),
                                       row.Cells[37].Value == null || row.Cells[37].Value.ToString() == "" ? "-" : row.Cells[37].Value.ToString(),
                                       row.Cells[38].Value == null || row.Cells[38].Value.ToString() == "" ? "-" : row.Cells[38].Value.ToString(),
                                       row.Cells[39].Value == null || row.Cells[39].Value.ToString() == "" ? "-" : row.Cells[39].Value.ToString(),
                                       row.Cells[40].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[40].Value.ToString()),
                                       row.Cells[41].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[41].Value.ToString()),
                                       txtUniqueID.Text, Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[46].Value.ToString()), StatusID,
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

        private void CboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
            EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
        }
    }
}
