using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class frmEmbFabricReturn : frmTrnsIface
    {
        private int RefMenuID;
        private static string RefVoucherID;
        public string strUniqueID;
        private string SRateCalcType = string.Empty;
        private int iMaxMyID_Stock;
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;
        ArrayList OrgInGridArray = new ArrayList();

        public frmEmbFabricReturn()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
        }

        #region Event

        private void frmEmbFabricReturn_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboProcessType, Combobox_Setup.ComboType.Mst_FabricProcessType, "");
                Combobox_Setup.FillCbo(ref cboDeptTo, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboProcessor, Combobox_Setup.ComboType.Mst_Dyer, "");
                Combobox_Setup.FillCbo(ref cboBroker, Combobox_Setup.ComboType.Mst_Brokers, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
                DetailGrid_Setup.CreateDtlGrid(this, pnlDetail, fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0);
                this.txtEntryNo.Enabled = false;
                RefMenuID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MenuID from tbl_VoucherTypeMaster Where GenMenuID=" + base.iIDentity + "")));
                GetRefModID();
                cboProcessType.Enabled = false;
                cboProcessType.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MiscID From fn_MiscMaster_tbl() Where MiscName='Embroidery'")));

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

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "EmbFabricReturnID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtRefNo, "RefNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtRefDate, "RefDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboProcessType, "ProcessTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDeptTo, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboProcessor, "ProcesserID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLrNo, "LrNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtLrDate, "LrDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEd1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "EmbFabricReturnID", Conversions.ToString(Localization.ParseNativeDouble(txtCode.Text)), base.dt_HasDtls_Grd);
                if (Localization.ParseNativeInt(base.blnFormAction.ToString()) == 1)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                }

                int icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_StockFabricLedger_tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));
                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
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
                AplySelectBtnEnbl();
                System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                dgvCellStyle.BackColor = System.Drawing.Color.LightGray;
                dgvCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
                dgvCellStyle.SelectionBackColor = System.Drawing.Color.Purple;
                dgvCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
                try
                {
                    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    {
                        if (Localization.ParseNativeInt(DB.GetSnglValue(string.Format("SELECT COUNT(0) from fn_StockFabricLedger_tbl() WHERE TransType<>436 and RefID =" + CommonLogic.SQuote(fgDtls.Rows[i].Cells[34].Value.ToString())))) > 0)
                        {
                            fgDtls.Rows[i].ReadOnly = true;
                            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle;
                        }
                        else
                            fgDtls.Rows[i].ReadOnly = false;
                    }
                }
                catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
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
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                dtEntryDate.Focus();
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                dtEntryDate.Text = (Conversions.ToString(DateAndTime.Now.Date));
                int MaxID = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(EmbFabricReturnID),0) From {0}  Where StoreID={1} and CompID = {2} and BranchID={3} and YearID = {4}", "tbl_EmbFabricReturnMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID))));

                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where EmbFabricReturnID = {1} and StoreID={2} and CompID={3} and BranchID={4} and YearID={5}", new object[] { "tbl_EmbFabricReturnMain", MaxID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = (Localization.ToVBDateString(reader["EntryDate"].ToString()));
                        dtRefDate.Text = (Localization.ToVBDateString(reader["RefDate"].ToString()));
                        cboProcessType.SelectedValue = Localization.ParseNativeDouble(reader["ProcessTypeID"].ToString());
                        cboProcessor.SelectedValue = Localization.ParseNativeDouble(reader["ProcesserID"].ToString());
                        cboBroker.SelectedValue = Localization.ParseNativeDouble(reader["BrokerID"].ToString());
                        cboDeptTo.SelectedValue = Localization.ParseNativeDouble(reader["DepartmentId"].ToString());
                    }
                }

                txtUniqueID.Text = CommonCls.GenUniqueID();
                strUniqueID = txtUniqueID.Text;
                AplySelectBtnEnbl();
                cboProcessType.Enabled = false;
                cboProcessType.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MiscID From fn_MiscMaster_tbl() Where MiscName='Embroidery'")));
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
                ("(#OTHERNO#)"),
                (dtRefDate.TextFormat(false, true)),
                (cboProcessType.SelectedValue),
                (cboDeptTo.SelectedValue),
                (cboProcessor.SelectedValue),
                (cboBroker.SelectedValue),
                (cboTransport.SelectedValue),
                (txtLrNo.Text.ToString()),
                (dtLrDate.TextFormat(false, true)),
                (txtDescription.Text.ToString()),
                cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue,
                cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue,
                dtEd1.TextFormat(false,true), 
                txtET1.Text,
                txtET2.Text,
                txtET3.Text
                };
                int UnitID = 0;
                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", "(#CodeID#)", Localization.ParseNativeInt(iIDentity.ToString()));
                for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    //if (row.Cells[10].Value != null )
                    {
                        if (Localization.ParseNativeDouble(row.Cells[10].Value.ToString()) > 0)
                        {
                            strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                               "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtRefDate.Text,
                               Localization.ParseNativeDouble(cboProcessor.SelectedValue.ToString()),
                               Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                               row.Cells[43].Value == null ? "0" : row.Cells[43].Value.ToString().Trim() == "" ? "0" : row.Cells[43].Value.ToString(),
                               row.Cells[44].Value == null ? "NULL" : row.Cells[44].Value.ToString().Trim() == "" ? "NULL" : row.Cells[44].Value.ToString(),
                               row.Cells[2].Value == null ? "NULL" : row.Cells[2].Value.ToString().Trim() == "" ? "NULL" : row.Cells[2].Value.ToString(),
                               row.Cells[3].Value == null ? "NULL" : row.Cells[3].Value.ToString().Trim() == "" ? "NULL" : row.Cells[3].Value.ToString(),
                               row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                               Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                               Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                               Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                               0,
                               Localization.ParseNativeDouble(row.Cells[8].Value.ToString()), 0, 0, 0,
                               Localization.ParseNativeDecimal(row.Cells[9].Value.ToString()),
                               Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()),
                               Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()), "NULL",
                               row.Cells[17].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                               row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                               row.Cells[19].Value == null ? "NULL" : row.Cells[19].Value.ToString().Trim() == "" ? "NULL" : row.Cells[19].Value.ToString(),
                               row.Cells[20].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[20].Value.ToString()),
                               row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                               row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),

                               row.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                               row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                               row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                               row.Cells[27].Value == null || row.Cells[27].Value.ToString() == "" || row.Cells[27].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[27].Value.ToString()),
                               row.Cells[28].Value == null || row.Cells[28].Value.ToString() == "" || row.Cells[28].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[28].Value.ToString()),
                               row.Cells[29].Value == null || row.Cells[29].Value.ToString() == "" ? "-" : row.Cells[29].Value.ToString(),
                               row.Cells[30].Value == null || row.Cells[30].Value.ToString() == "" ? "-" : row.Cells[30].Value.ToString(),
                               row.Cells[31].Value == null || row.Cells[31].Value.ToString() == "" ? "-" : row.Cells[31].Value.ToString(),
                               row.Cells[32].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[32].Value.ToString()),
                               row.Cells[33].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[33].Value.ToString()),
                               "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                            strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                               "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtRefDate.Text,
                               Localization.ParseNativeDouble(cboDeptTo.SelectedValue.ToString()),
                               Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                               row.Cells[43].Value == null ? "0" : row.Cells[43].Value.ToString().Trim() == "" ? "0" : row.Cells[43].Value.ToString(),
                               row.Cells[44].Value == null ? "NULL" : row.Cells[44].Value.ToString().Trim() == "" ? "NULL" : row.Cells[44].Value.ToString(),
                               row.Cells[2].Value == null ? "NULL" : row.Cells[2].Value.ToString().Trim() == "" ? "NULL" : row.Cells[2].Value.ToString(),
                               row.Cells[3].Value == null ? "NULL" : row.Cells[3].Value.ToString().Trim() == "" ? "NULL" : row.Cells[3].Value.ToString(),
                               row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                               Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                               Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                               Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                               0,
                               Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                               Localization.ParseNativeDecimal(row.Cells[9].Value.ToString()),
                               Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()),
                               Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                               0, 0, 0,
                                Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()), "NULL",
                               row.Cells[17].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                               row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                               row.Cells[19].Value == null ? "NULL" : row.Cells[19].Value.ToString().Trim() == "" ? "NULL" : row.Cells[19].Value.ToString(),
                               row.Cells[20].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[20].Value.ToString()),
                               row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                               row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                               row.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                               row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                               row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                               row.Cells[27].Value == null || row.Cells[27].Value.ToString() == "" || row.Cells[27].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[27].Value.ToString()),
                               row.Cells[28].Value == null || row.Cells[28].Value.ToString() == "" || row.Cells[28].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[28].Value.ToString()),
                               row.Cells[29].Value == null || row.Cells[29].Value.ToString() == "" ? "-" : row.Cells[29].Value.ToString(),
                               row.Cells[30].Value == null || row.Cells[30].Value.ToString() == "" ? "-" : row.Cells[30].Value.ToString(),
                               row.Cells[31].Value == null || row.Cells[31].Value.ToString() == "" ? "-" : row.Cells[31].Value.ToString(),
                               row.Cells[32].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[32].Value.ToString()),
                               row.Cells[33].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[33].Value.ToString()),
                               "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                            UnitID = Localization.ParseNativeInt(row.Cells[8].Value.ToString());
                        }
                    }
                    row = null;
                }
                //if (cboTransport.SelectedValue != null && Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()) > 0.0)
                //{
                //    strAdjQry = strAdjQry + DBSp.InsertIntoTrasportLedger("(#CodeID#)", "(#OTHERNO#)", dtChallanDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()), Localization.ParseNativeDouble(cboProcessor.SelectedValue.ToString()), Localization.ParseNativeDouble(cboDeptTo.SelectedValue.ToString()), txtLrNo.Text, dtLrDate.Text, null, Localization.ParseNativeDouble(UnitID.ToString()), Localization.ParseNativeInt(string.Format("{0:N0}", CommonCls.GetColSum(fgDtls, 9, -1, -1))), Localization.ParseNativeDecimal(string.Format("{0:N0}", CommonCls.GetColSum(fgDtls, 10, -1, -1))), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                //}
                strAdjQry += "Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";";
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                DBSp.Transcation_AddEdit(pArrayData, fgDtls, true, strAdjQry, "", txtEntryNo.Text, txtRefNo.Text, "RefNo");
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

                if (txtEntryNo.Text.Trim() == "" || txtEntryNo.Text.Trim() == "-" || txtEntryNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry No.");
                    return true;
                }
                if (!CommonCls.CheckDate(dtEntryDate.Text, true))
                    return true;

                if (!Information.IsDate(dtEntryDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry Date");
                    dtEntryDate.Focus();
                    return true;
                }
                if (txtRefNo.Text.Trim() == "" || txtRefNo.Text.Trim() == "-" || txtRefNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Challan No.");
                    txtRefNo.Focus();
                    return true;
                }
                if (!CommonCls.CheckDate(dtRefDate.Text, true))
                    return true;
                if (!Information.IsDate(dtRefDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Challan Date");
                    dtRefDate.Focus();
                    return true;
                }
                if (cboDeptTo.SelectedValue == null || cboDeptTo.Text.Trim().ToString() == "-" || cboDeptTo.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    cboDeptTo.Focus();
                    return true;
                }
                if (cboProcessor.SelectedValue == null || cboProcessor.Text.Trim().ToString() == "-" || cboProcessor.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Processor");
                    cboProcessor.Focus();
                    return true;
                }
                if (cboProcessType.SelectedValue == null || cboProcessType.Text.Trim().ToString() == "-" || cboProcessType.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Process Type");
                    cboProcessType.Focus();
                    return true;
                }
                if ((txtRefNo.Text.Trim().Length) > 0)
                {
                    string strTblName;
                    if (base.blnFormAction == 0)
                    {
                        strTblName = "tbl_EmbFabricReturnMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "RefNo", txtRefNo.Text, false, "", 0, " ProcesserID = " + cboProcessor.SelectedValue + " and StoreID=" + Db_Detials.StoreID + " and CompID =" + Db_Detials.CompID + " And YearID = " + Db_Detials.YearID + "", "This Processor already used this Challan No in Entry No : " + DB.GetSnglValue(string.Format("Select Entryno from {0} where ProcesserID = {1} and RefNo = '{2}' ", "tbl_EmbFabricReturnMain", cboProcessor.SelectedValue, txtRefNo.Text.ToString()))))
                        {
                            txtRefNo.Focus();
                            return true;
                        }
                    }
                    else if (Localization.ParseNativeInt(base.blnFormAction.ToString()) == 1)
                    {
                        strTblName = "tbl_EmbFabricReturnMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "RefNo", txtRefNo.Text, true, "EmbFabricReturnID", Localization.ParseNativeLong(txtCode.Text.Trim()), " ProcesserID = " + cboProcessor.SelectedValue + " and StoreID=" + Db_Detials.StoreID + " and CompID =" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " And YearID = " + Db_Detials.YearID + "", "This Processor already used this Challan No in Entry No : " + DB.GetSnglValue(string.Format("Select Entryno from {0} where ProcesserID = {1} and RefNo = '{2}' ", "tbl_EmbFabricReturnMain", cboProcessor.SelectedValue, txtRefNo.Text.ToString()))))
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
            if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
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
                if (cboProcessor.SelectedValue != null && cboProcessor.Text.Trim().ToString() != "-" && cboProcessor.Text.Trim().ToString() != "")
                {
                    #region StockAdjQuery
                    string strQry = string.Empty;
                    int ibitcol = 0;
                    string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", iIDentity, 0.0));
                    string strQry_ColName = "";
                    string[] arr = CommonCls.GetAdjColName(base.iIDentity, 0.0).Split(';');
                    strQry_ColName = arr[0].ToString();
                    ibitcol = Localization.ParseNativeInt(arr[1]);
                    strQry = string.Format(" Select {0} From {1} ({2}, {3},{4},{5}) Where BatchNo <> '-' ", new object[] { strQry_ColName, snglValue, cboProcessor.SelectedValue, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID });
                    #endregion

                    frmStockAdj frmStockAdj = new frmStockAdj();
                    frmStockAdj.MenuID = base.iIDentity;
                    frmStockAdj.Entity_IsfFtr = 0.0;
                    frmStockAdj.ref_fgDtls = fgDtls;
                    frmStockAdj.QueryString = strQry;
                    frmStockAdj.IsRefQuery = true;
                    frmStockAdj.ibitCol = ibitcol;
                    frmStockAdj.LedgerID = cboProcessor.SelectedValue.ToString();
                    frmStockAdj.UsedInGridArray = OrgInGridArray;
                    if (frmStockAdj.ShowDialog() == DialogResult.Cancel)
                    {
                        frmStockAdj.Dispose();
                        fgDtls.CurrentCell = fgDtls[5, 0];
                    }
                    else
                    {
                        frmStockAdj.Dispose();
                        frmStockAdj = null;
                        fgDtls.Select();
                    }
                    setTempRowIndex();
                    setMyID_Stock();
                    ExecuterTempQry(-1);
                }
                else
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Processor.");
                    cboProcessor.Focus();
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
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (e.ColumnIndex == 9 || e.ColumnIndex == 10 || e.ColumnIndex == 11)
                    {
                        ExecuterTempQry(e.RowIndex);
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
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                if ((this.cboProcessor.SelectedValue != null) && (Conversion.Val(RuntimeHelpers.GetObjectValue(cboProcessor.SelectedValue)) > 0.0))
                {
                    cboBroker.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select BrokerID From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboProcessor.SelectedValue)));
                    cboTransport.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select TransportId From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboProcessor.SelectedValue)));
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
                if (sMappingID != "")
                {
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
                                decimal FabricDesign = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select WtPerMtr From tbl_FabricDesignMaster Where FabricDesignID=" + Localization.ParseNativeInt(row.Cells[5].Value.ToString()) + "")));
                                row.Cells[13].Value = Localization.ParseNativeDecimal(row.Cells[10].Value.ToString());
                                row.Cells[14].Value = Localization.ParseNativeDecimal(row.Cells[11].Value.ToString());

                                if (row.Cells[14].Value == null || Localization.ParseNativeDecimal(row.Cells[14].Value.ToString()) == 0)
                                {
                                    if (FabricDesign != 0)
                                        row.Cells[14].Value = FabricDesign;
                                }

                                double Weight = 0;

                                if (row.Cells[13].Value != null && Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()) > 0 && Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()) != 0)
                                {
                                    if (row.Cells[14].Value == null || Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[14].Value.ToString()) == 0)
                                    {
                                        Weight = (Localization.ParseNativeDouble(FabricDesign.ToString()) / Localization.ParseNativeDouble(row.Cells[13].Value.ToString())) * Localization.ParseNativeDouble(row.Cells[10].Value.ToString());
                                    }
                                    else
                                    {
                                        Weight = (Localization.ParseNativeDouble(row.Cells[14].Value.ToString()) / Localization.ParseNativeDouble(row.Cells[13].Value.ToString())) * Localization.ParseNativeDouble(row.Cells[10].Value.ToString());
                                    }
                                    if (Weight.ToString() != "NaN")
                                    {
                                        row.Cells[11].Value = Math.Round(Weight, 3);
                                    }
                                }

                                if (MyID != "" && row.Cells[10].Value != null && row.Cells[10].Value.ToString() != "" && row.Cells[10].Value.ToString() != "0" && row.Cells[11].Value != null && row.Cells[11].Value.ToString() != "" && row.Cells[11].Value.ToString() != "0")
                                {
                                    strQry = strQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                    MyID, (i + 1).ToString(), txtEntryNo.Text, dtRefDate.Text,
                                    Localization.ParseNativeDouble(cboProcessor.SelectedValue.ToString()),
                                    Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                    row.Cells[43].Value == null ? "0" : row.Cells[43].Value.ToString().Trim() == "" ? "0" : row.Cells[43].Value.ToString(),
                                    row.Cells[44].Value == null ? "NULL" : row.Cells[44].Value.ToString().Trim() == "" ? "NULL" : row.Cells[44].Value.ToString(),
                                    row.Cells[2].Value == null ? "NULL" : row.Cells[2].Value.ToString().Trim() == "" ? "NULL" : row.Cells[2].Value.ToString(),
                                    row.Cells[3].Value == null ? "NULL" : row.Cells[3].Value.ToString().Trim() == "" ? "NULL" : row.Cells[3].Value.ToString(),
                                    row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                    Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                    Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                    Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                    0,
                                    Localization.ParseNativeDouble(row.Cells[8].Value.ToString()), 0, 0, 0,
                                    Localization.ParseNativeDecimal(row.Cells[9].Value.ToString()),
                                    Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()),
                                    Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                     Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()), "NULL",
                                    row.Cells[17].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                    row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                    row.Cells[19].Value == null ? "NULL" : row.Cells[19].Value.ToString().Trim() == "" ? "NULL" : row.Cells[19].Value.ToString(),
                                    row.Cells[20].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[20].Value.ToString()),
                                    row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                    row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),

                                    row.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                    row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                    row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                    row.Cells[27].Value == null || row.Cells[27].Value.ToString() == "" || row.Cells[27].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[27].Value.ToString()),
                                    row.Cells[28].Value == null || row.Cells[28].Value.ToString() == "" || row.Cells[28].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[28].Value.ToString()),
                                    row.Cells[29].Value == null || row.Cells[29].Value.ToString() == "" ? "-" : row.Cells[29].Value.ToString(),
                                    row.Cells[30].Value == null || row.Cells[30].Value.ToString() == "" ? "-" : row.Cells[30].Value.ToString(),
                                    row.Cells[31].Value == null || row.Cells[31].Value.ToString() == "" ? "-" : row.Cells[31].Value.ToString(),
                                    row.Cells[32].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[32].Value.ToString()),
                                    row.Cells[33].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[33].Value.ToString()),
                                    txtUniqueID.Text, i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                                }
                            }
                        }
                        else
                        {
                            if ((fgDtls.CurrentCell.ColumnIndex == 9) || (fgDtls.CurrentCell.ColumnIndex == 10) || (fgDtls.CurrentCell.ColumnIndex == 11))
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

                                if (MyID != "" && row.Cells[10].Value != null && row.Cells[10].Value.ToString() != "" && row.Cells[10].Value.ToString() != "0" && row.Cells[11].Value != null && row.Cells[11].Value.ToString() != "" && row.Cells[11].Value.ToString() != "0")
                                {
                                    decimal FabricDesign = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select WtPerMtr From tbl_FabricDesignMaster Where FabricDesignID=" + Localization.ParseNativeInt(fgDtls.Rows[RowIndex].Cells[5].Value.ToString()) + "")));
                                    double Weight = 0;

                                    if (fgDtls.Rows[RowIndex].Cells[13].Value != null && Localization.ParseNativeDecimal(fgDtls.Rows[RowIndex].Cells[13].Value.ToString()) > 0 && Localization.ParseNativeDecimal(fgDtls.Rows[RowIndex].Cells[13].Value.ToString()) != 0)
                                    {
                                        if (fgDtls.Rows[RowIndex].Cells[14].Value == null || Localization.ParseNativeDecimal(fgDtls.Rows[RowIndex].Cells[14].Value.ToString()) == 0)
                                        {
                                            Weight = (Localization.ParseNativeDouble(FabricDesign.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[13].Value.ToString())) * Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[10].Value.ToString());
                                        }
                                        else
                                        {
                                            Weight = (Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[14].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[13].Value.ToString())) * Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[10].Value.ToString());
                                        }
                                        if (Weight.ToString() != "NaN")
                                        {
                                            fgDtls.Rows[RowIndex].Cells[11].Value = Math.Round(Weight, 3);
                                        }
                                    }

                                    strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[38].Value.ToString()) + " and AddedBy=" + Db_Detials.UserID + ";");

                                    strQry = strQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                    MyID, (RowIndex + 1).ToString(), txtEntryNo.Text, dtRefDate.Text,
                                    Localization.ParseNativeDouble(cboProcessor.SelectedValue.ToString()),
                                    Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                    row.Cells[43].Value == null ? "0" : row.Cells[43].Value.ToString().Trim() == "" ? "0" : row.Cells[43].Value.ToString(),
                                    row.Cells[44].Value == null ? "NULL" : row.Cells[44].Value.ToString().Trim() == "" ? "NULL" : row.Cells[44].Value.ToString(),
                                    row.Cells[2].Value == null ? "NULL" : row.Cells[2].Value.ToString().Trim() == "" ? "NULL" : row.Cells[2].Value.ToString(),
                                    row.Cells[3].Value == null ? "NULL" : row.Cells[3].Value.ToString().Trim() == "" ? "NULL" : row.Cells[3].Value.ToString(),
                                    row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                    Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                    Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                    Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),0,
                                    Localization.ParseNativeDouble(row.Cells[8].Value.ToString()), 0, 0, 0,
                                    Localization.ParseNativeDecimal(row.Cells[9].Value.ToString()),
                                    Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()),
                                    Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                     Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()), "NULL",
                                    row.Cells[17].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                    row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                    row.Cells[19].Value == null ? "NULL" : row.Cells[19].Value.ToString().Trim() == "" ? "NULL" : row.Cells[19].Value.ToString(),
                                    row.Cells[20].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[20].Value.ToString()),
                                    row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                    row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                    row.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                    row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                    row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                    row.Cells[27].Value == null || row.Cells[27].Value.ToString() == "" || row.Cells[27].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[27].Value.ToString()),
                                    row.Cells[28].Value == null || row.Cells[28].Value.ToString() == "" || row.Cells[28].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[28].Value.ToString()),
                                    row.Cells[29].Value == null || row.Cells[29].Value.ToString() == "" ? "-" : row.Cells[29].Value.ToString(),
                                    row.Cells[30].Value == null || row.Cells[30].Value.ToString() == "" ? "-" : row.Cells[30].Value.ToString(),
                                    row.Cells[31].Value == null || row.Cells[31].Value.ToString() == "" ? "-" : row.Cells[31].Value.ToString(),
                                    row.Cells[32].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[32].Value.ToString()),
                                    row.Cells[33].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[33].Value.ToString()),
                                    txtUniqueID.Text, RowIndex, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID,
                                    Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
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

        private void setMyID_Stock()
        {
            iMaxMyID_Stock = Localization.ParseNativeInt(DB.GetSnglValue("Select MAX(MyId + 1) from tbl_StockFabricLedger Where IsDeleted=0"));

            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[37].Value = iMaxMyID_Stock;
            }
        }

        private void setTempRowIndex()
        {
            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[38].Value = i;
            }
        }

        private void frmEmbFabricReturn_FormClosed(object sender, FormClosedEventArgs e)
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
                            if ((Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select COUNT(0) From fn_StockFabricLedger_tbl() Where RefId='" + fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[34].Value + "' and RefID<>'' and Transtype<>" + iIDentity + ""))) > 0))
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityWarning, "", "Reference Found In Another Module..Row Cannot Be Deleted");
                                return;
                            }
                            else
                            {
                                try
                                {
                                    string strQry = string.Format("Update tbl_StockFabricLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[38].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
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
                                string strQry = string.Format("Update tbl_StockFabricLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[38].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
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
    }
}
