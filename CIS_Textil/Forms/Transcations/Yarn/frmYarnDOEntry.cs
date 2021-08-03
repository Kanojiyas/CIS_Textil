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
    public partial class frmYarnDOEntry : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        public string strUniqueID;
        private int iMaxMyID;

        public frmYarnDOEntry()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
        }

        #region Form Events

        private void frmYarnDOEntry_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboSupplier, Combobox_Setup.ComboType.Mst_Suppliers, "");
                Combobox_Setup.FillCbo(ref CboBroker, Combobox_Setup.ComboType.Mst_Brokers, "");
                Combobox_Setup.FillCbo(ref cboMill, Combobox_Setup.ComboType.Mst_Mill, "");
                Combobox_Setup.FillCbo(ref cboDeliveryAt, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                
                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls.KeyDown += new KeyEventHandler(this.fgDtls_KeyDown);

                cboOrderType.AutoComplete = true;
                cboOrderType.AutoDropdown = true;
                if (blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    FillControls();
                }
                cboOrderType_SelectedIndexChanged(null, null);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #endregion

        #region Property

        public void MovetoField()
        {
            try
            {
                if (strUniqueID != null)
                {
                    string strQry = string.Format("Delete From tbl_YarnOrderLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                    strQry = strQry + string.Format("Update  tbl_YarnOrderLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                    DB.ExecuteSQL(strQry);
                    strQry = string.Format("Update tbl_YarnOrderLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                    DB.ExecuteSQL(strQry);
                }
                CIS_Textbox txtEntryNo = this.txtEntryNo;
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                this.txtEntryNo = txtEntryNo;

                int MaxID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Max(isnull(YarnDOID,0)) From {0}  Where StoreID={1} and CompID = {2} and BranchID = {3} and YearID = {4}", "tbl_YarnDOMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID)));
                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where YarnDOID = {1} and StoreID={2} and CompID={3} and BranchID={4} and YearID={5} ", new object[] { "tbl_YarnDOMain", MaxID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        if (reader["OrderType"].ToString() != "")
                        {
                            cboOrderType.SelectedItem = reader["OrderType"].ToString();
                        }
                        else
                        {
                            cboOrderType.SelectedItem = "WITH ORDER";
                            cboOrderType.Enabled = false;
                        }
                    }
                }
                dtEntryDate.Focus();
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                cboOrderType_SelectedIndexChanged(null, null);
                txtUniqueID.Text = CommonCls.GenUniqueID();
                strUniqueID = txtUniqueID.Text;
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #endregion

        #region Form Navigation

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "YarnDOID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "Entrydate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtDoNo, "DONO", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtDoDate, "DODate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboSupplier, "SupplierID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, CboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboMill, "MillID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtVehicleNo, "VehicleNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLRno, "LrNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtLrDate, "LrDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboDeliveryAt, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtUniqueID, "UniqueID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtED1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                try
                {
                    string sOrderType = DBValue.Return_DBValue(this, "OrderType");
                    cboOrderType.SelectedItem = sOrderType;
                }
                catch { }

                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "YarnDOID", Conversions.ToString(Localization.ParseNativeDouble(this.txtCode.Text)), base.dt_HasDtls_Grd);
                int icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_YarnOrderLedger_Tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    cboOrderType.Enabled = false;
                    cboSupplier.Enabled = false;
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                    setTempRowIndex();

                    try
                    {
                        string strOldUniqueID = string.Empty;
                        strOldUniqueID = txtUniqueID.Text;
                        txtUniqueID.Text = CommonCls.GenUniqueID();
                        strUniqueID = txtUniqueID.Text;
                        if (icount == 0)
                        {
                            string strQry = string.Format("Update tbl_YarnOrderLedger Set UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + ", StatusID=2 Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + "");
                            DB.ExecuteSQL(strQry);
                        }
                        else
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityShieldBlue, "Warning", "This Record Is Edited By Another User..");
                        }
                    }
                    catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                }
                else
                {
                    cboOrderType.Enabled = true;
                    cboSupplier.Enabled = true;
                }

                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    if (strUniqueID != null)
                    {
                        string strQry = string.Format("Delete From tbl_YarnOrderLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                        strQry = strQry + string.Format("Update  tbl_YarnOrderLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                        DB.ExecuteSQL(strQry);
                        strQry = string.Format("Update tbl_YarnOrderLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                        DB.ExecuteSQL(strQry);
                    }
                }

                if ((base.blnFormAction == Enum_Define.ActionType.View_Record) && !(base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_YarnOrderLedger_Tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));
                }

                try
                {
                    System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                    dgvCellStyle.BackColor = System.Drawing.Color.LightGray;
                    dgvCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
                    dgvCellStyle.SelectionBackColor = System.Drawing.Color.Purple;
                    dgvCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

                    try
                    {
                        for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                        {
                            if (Localization.ParseNativeInt(DB.GetSnglValue(string.Format("SELECT COUNT(0) from fn_YarnInwardDtls_Tbl() WHERE ARefID =" + CommonLogic.SQuote(fgDtls.Rows[i].Cells[22].Value.ToString())))) > 0)
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
                catch (Exception ex1)
                {
                    Navigate.logError(ex1.Message, ex1.StackTrace);
                }

                //System.Windows.Forms.DataGridViewCellStyle dgvCellStyle_Ref = new System.Windows.Forms.DataGridViewCellStyle();
                //dgvCellStyle_Ref.BackColor = System.Drawing.Color.LightSteelBlue;
                //dgvCellStyle_Ref.ForeColor = System.Drawing.SystemColors.WindowText;
                //dgvCellStyle_Ref.SelectionBackColor = System.Drawing.Color.SteelBlue;
                //dgvCellStyle_Ref.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
                //try
                //{
                //    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                //    {
                //        if (icount > 0)
                //        {
                //            btnSelectStock.Enabled = false;
                //            fgDtls.Rows[i].ReadOnly = true;
                //            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle_Ref;
                //        }
                //        else
                //        {
                //            btnSelectStock.Enabled = true;
                //            fgDtls.Rows[i].ReadOnly = false;
                //        }
                //    }
                //}
                //catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }

                cboOrderType_SelectedIndexChanged(null, null);
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", exception1.Message);
            }
        }

        public void SaveRecord()
        {
            string sTotQty = "", sTotWeight = "";
            try
            {
                sTotQty = string.Format("{0:N0}", CommonCls.GetColSum(this.fgDtls, 8, -1, -1));
                sTotWeight = string.Format("{0:N3}", CommonCls.GetColSum(this.fgDtls, 10, -1, -1));
                ArrayList pArrayData = new ArrayList
                {
                   ("(#ENTRYNO#)"),
                   dtEntryDate.TextFormat(false, true),
                   cboOrderType.SelectedItem.ToString(),
                   ("(#OTHERNO#)"),
                   dtDoDate.TextFormat(false, true),
                   cboSupplier.SelectedValue,
                   CboBroker.SelectedValue,
                   cboMill.SelectedValue,
                   cboTransport.SelectedValue,
                   txtVehicleNo.Text.ToString(),
                   txtLRno.Text.ToString(),
                   dtLrDate.TextFormat(false, true),
                   cboDeliveryAt.SelectedValue,
                   sTotQty.Replace(",",""),
                   sTotWeight.Replace(",",""),
                   txtUniqueID.Text,
                   txtNarration.Text,
                   (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                   (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                   (dtED1.TextFormat(false, true)),
                   (txtET1.Text.Trim()),
                   (txtET2.Text.Trim()),
                   (txtET3.Text.Trim())
                };
                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_YarnOrderLedger", "(#CodeID#)", Localization.ParseNativeInt(base.iIDentity.ToString()));
                for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    if (row.Cells[2].Value != null && row.Cells[2].Value.ToString() != "")
                    {
                        string sBatchNo = string.Empty;
                        sBatchNo = DB.GetSnglValue("Select YarnPONo from fn_YarnPurchaseOrderMain_Tbl() Where YarnPOID=" + row.Cells[2].Value.ToString());
                        if (Localization.ParseNativeDouble(row.Cells[10].Value.ToString()) > 0)
                        {
                            strAdjQry += DBSp.InsertIntoYarnOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)", (i + 1).ToString(),
                                "(#ENTRYNO#)", dtDoDate.Text,
                                Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()),
                                row.Cells[10].Value == null ? "0" : row.Cells[10].Value.ToString() == "" ? "0" : row.Cells[10].Value.ToString(), "0",
                                0, sBatchNo, dtEntryDate.Text, Localization.ParseNativeDouble(row.Cells[3].Value.ToString()), Localization.ParseNativeDouble(row.Cells[4].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[5].Value.ToString()), Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                0, 0, 0, Localization.ParseNativeDecimal(row.Cells[8].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[9].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()),
                                0, "NULL", 0,
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
                                "NULL", i, 1, "Purchase", 0,
                                Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                        }
                        row = null;
                    }
                }
                strAdjQry += "Delete From tbl_YarnOrderLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";";
                double dblTransID = 0; 
                DBSp.Transcation_AddEdit_Trans(pArrayData, this.fgDtls, true, ref dblTransID, strAdjQry, "", txtEntryNo.Text, txtDoNo.Text, "DONO");
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
                if ((txtEntryNo.Text != null) && (txtEntryNo.Text.Trim().Length <= 0))
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
                if (cboOrderType.SelectedItem.ToString() == "" || cboOrderType.Text.Trim().ToString() == "-")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Order Type");
                    cboOrderType.Focus();
                    return true;
                }
                if (txtDoNo.Text.Trim() == "" || txtDoNo.Text.Trim() == "-" || txtDoNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Delivery Order No.");
                    txtDoNo.Focus();
                    return true;
                }
                if (!Information.IsDate(dtDoDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Delivery Order Date");
                    dtEntryDate.Focus();
                    return true;
                }
                if (!CommonCls.CheckDate(this.dtDoDate.Text, true))
                {
                    dtDoDate.Focus();
                    return true;
                }
                if (cboSupplier.SelectedValue == null || cboSupplier.SelectedValue.ToString() == "0" || cboSupplier.Text.Trim().ToString() == "-")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Supplier");
                    cboSupplier.Focus();
                    return true;
                }
                if (CboBroker.SelectedValue == null || CboBroker.SelectedValue.ToString() == "0" || CboBroker.Text.Trim().ToString() == "-")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Broker");
                    CboBroker.Focus();
                    return true;
                }

                if (cboDeliveryAt.SelectedValue == null || cboDeliveryAt.SelectedValue.ToString() == "0" || cboDeliveryAt.Text.Trim().ToString() == "-")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Delivery Location");
                    cboDeliveryAt.Focus();
                    return true;
                }

                if (txtDoNo.Text.Trim().Length > 0)
                {
                    string strTblName;
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        strTblName = "tbl_YarnDOMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "DONO", this.txtDoNo.Text, false, "", 0L, string.Format("StoreID = {0} and CompID = {1} and BranchID = {2} and YearID = {3}", Db_Detials.CompID, Db_Detials.YearID), "This Party already used this D.O.No in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where YarnPONo = {1} and StoreID = {2} and CompID = {3} and BranchID = {4} and YearID = {5}", new object[] { "tbl_YarnDOMain", this.txtDoNo.Text.ToString(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }))))
                        {
                            txtDoNo.Focus();
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        strTblName = "tbl_YarnDOMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "DONO", this.txtDoNo.Text, true, "YarnDOID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), string.Format("StoreID = {0} and CompID = {1} and BranchID = {2} and YearID = {3}", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID), "This Party already used this D.O.No in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where YarnPONo = {1} and StoreID = {2} and CompID = {3} and BranchID = {4} and YearID = {5}", new object[] { "tbl_YarnDOMain", this.txtDoNo.Text.ToString(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }))))
                        {
                            txtDoNo.Focus();
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

        private void cboSupplier_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if ((cboSupplier.SelectedValue != null) && (Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()) > 0.0))
                {
                    CboBroker.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select BrokerID From {0} Where LedgerID = {1}", "tbl_LedgerMaster", RuntimeHelpers.GetObjectValue(this.cboSupplier.SelectedValue))));
                    cboTransport.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select TransportID From {0} Where LedgerID = {1}", "tbl_LedgerMaster", RuntimeHelpers.GetObjectValue(this.cboSupplier.SelectedValue))));
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record || base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                    {
                        btnSelectStock.Enabled = true;
                        fgDtls.Columns[2].Visible = true;
                        fgDtls.Columns[3].ReadOnly = true;
                        fgDtls.Columns[4].ReadOnly = true;
                        fgDtls.Columns[5].ReadOnly = true;
                        fgDtls.Columns[7].ReadOnly = true;
                    }
                    else
                    {
                        btnSelectStock.Enabled = false;
                        fgDtls.Columns[2].Visible = false;
                        fgDtls.Columns[3].ReadOnly = false;
                        fgDtls.Columns[4].ReadOnly = false;
                        fgDtls.Columns[5].ReadOnly = false;
                        fgDtls.Columns[7].ReadOnly = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record)))
            {
                try
                {
                    if ((e.ColumnIndex == 8) | (e.ColumnIndex == 10))
                    {
                        ExecuterTempQry(e.RowIndex);
                    }
                    if (fgDtls.Rows.Count > 1)
                    {
                        cboOrderType.Enabled = false;
                    }
                    else
                    {
                        cboOrderType.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                }
            }
        }

        private void CalcVal()
        {
            try
            {
                //lblTotalQty.Text = string.Format("{0:N0}", CommonCls.GetColSum(this.fgDtls, 6, -1, -1));
                //lblTotalWeight.Text = string.Format("{0:N3}", CommonCls.GetColSum(this.fgDtls, 8, -1, -1));
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public string SetSupplier
        {
            get
            {
                return cboSupplier.SelectedValue.ToString();
            }
            set
            {
                if (value.Length != 0)
                {
                    cboSupplier.SelectedValue = value;
                }
            }
        }

        private void btnSelectStock_Click(object sender, EventArgs e)
        {
            try
            {
                bool isIndexAppld = false;
                int iIndex = fgDtls.RowCount - 1;
                for (int m = 0; m <= fgDtls.RowCount - 1; m++)
                {
                    if (fgDtls.Rows[m].Cells[2].Value != null && fgDtls.Rows[m].Cells[2].Value.ToString() != "")
                    {
                        iIndex = m;
                        isIndexAppld = true;
                    }
                }
                if (!isIndexAppld)
                {
                    iIndex = -1;
                }

                string sSupID = string.Empty;
                if (cboSupplier.SelectedValue == null || cboSupplier.SelectedValue.ToString() == "-" || cboSupplier.SelectedValue.ToString() == "" || cboSupplier.SelectedValue.ToString() == "0")
                {
                    sSupID = "0";
                }
                else
                {
                    sSupID = cboSupplier.SelectedValue.ToString();
                }
                if (!Information.IsDate(dtDoDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter D.O Date");
                    dtDoDate.Focus();
                    return;
                }

                #region StockAdjQuery
                string strQry = string.Empty;
                int ibitcol = 0;
                string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", iIDentity, 0.0));
                string strQry_ColName = "";
                string[] arr = CommonCls.GetAdjColName(base.iIDentity, 0.0).Split(';');
                strQry_ColName = arr[0].ToString();
                ibitcol = Localization.ParseNativeInt(arr[1]);
                strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}, {6}, {7}) Where OrderTransType ='Purchase' Order by MyId ", new object[] { strQry_ColName, snglValue, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(dtDoDate.Text))), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, sSupID });
                #endregion

                frmStockAdj frmStockAdj = new frmStockAdj();
                frmStockAdj.MenuID = base.iIDentity;
                frmStockAdj.Entity_IsfFtr = 0.0;
                frmStockAdj.ref_fgDtls = this.fgDtls;
                frmStockAdj.AsonDate = Conversions.ToDate(this.dtDoDate.Text);
                frmStockAdj.LedgerID = sSupID;
                frmStockAdj.IsStock = false;
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
                            if ((fgDtls.Rows[i].Cells[8].Value != null) && (fgDtls.Rows[i].Cells[8].Value.ToString() != "0") && (fgDtls.Rows[i].Cells[8].Value.ToString() != ""))
                            {
                                //if (fgDtls.Rows[i].Cells[23].Value.ToString() != fgDtls.Rows[i].Cells[25].Value.ToString())
                                {
                                    double iPcs = Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[8].Value.ToString());

                                    if (Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[26].Value.ToString()) < iPcs)
                                    {
                                        iPcs = Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[26].Value.ToString());
                                    }

                                    if (iPcs > 0)
                                    {
                                        int num8 = (int)Math.Round((double)(iPcs + i));
                                        for (int k = i + 1; k <= num8; k++)
                                        {
                                            fgDtls.Rows.Insert(k, new DataGridViewRow());
                                            for (int m = 0; m <= fgDtls.ColumnCount - 1; m++)
                                            {
                                                if (m == 8)
                                                {
                                                    fgDtls.Rows[k].Cells[m].Value = 1;
                                                }
                                                else if (m == 1)
                                                {
                                                    fgDtls.Rows[k].Cells[m].Value = k;
                                                }
                                                else if (m != 6 && m != 10)
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
                            }
                            else
                            {
                                fgDtls.Rows[i].Cells[8].Value = fgDtls.Rows[i].Cells[26].Value.ToString();
                            }
                        }
                    }
                    fgDtls.Rows.RemoveAt(fgDtls.RowCount - 1);
                    if (fgDtls.RowCount == 0)
                    {
                        EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                        EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);

                    }
                    SendKeys.Send("{TAB}");
                    if (fgDtls.Rows.Count > 0)
                    {
                        fgDtls.CurrentCell = fgDtls[3, fgDtls.RowCount - 1];
                    }
                    fgDtls.Select();
                    setTempRowIndex();
                    setMyID();
                    ExecuterTempQry(-1);
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
                            strQry = string.Format("Delete From tbl_YarnOrderLedger Where Dr_Bags=0 and Dr_Wt=0 and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                            {
                                DataGridViewRow row = fgDtls.Rows[i];
                                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                                {
                                    StatusID = 1;
                                    MyID = iMaxMyID.ToString();
                                }
                                else
                                {
                                    StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_YarnOrderLedger_Tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockFabricLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + "")));
                                    MyID = txtCode.Text;
                                }

                                if (MyID != "" && row.Cells[2].Value != null && row.Cells[2].Value.ToString() != "" && row.Cells[2].Value.ToString() != "0" && row.Cells[10].Value != null && row.Cells[10].Value.ToString() != "" && row.Cells[10].Value.ToString() != "0")
                                {
                                    string sBatchNo = string.Empty;
                                    sBatchNo = DB.GetSnglValue("Select YarnPONo from fn_YarnPurchaseOrderMain_Tbl() Where YarnPOID=" + row.Cells[2].Value.ToString());

                                    strQry += DBSp.InsertIntoYarnOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)", (i + 1).ToString(),
                                            "(#ENTRYNO#)", dtDoDate.Text, 
                                            Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()), 
                                            row.Cells[10].Value == null ? "0" : row.Cells[10].Value.ToString() == "" ? "0" : row.Cells[10].Value.ToString(), "0",
                                            0, sBatchNo, dtEntryDate.Text, Localization.ParseNativeDouble(row.Cells[3].Value.ToString()), Localization.ParseNativeDouble(row.Cells[4].Value.ToString()),
                                            Localization.ParseNativeDouble(row.Cells[5].Value.ToString()), Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                            0, 0, 0, Localization.ParseNativeDecimal(row.Cells[8].Value.ToString()),
                                            Localization.ParseNativeDecimal(row.Cells[9].Value.ToString()),
                                            Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()), 
                                            0, "NULL", 0,
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
                                            "NULL", i, 1, "Purchase", 0, 
                                            Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                                }
                            }
                        }
                        else
                        {
                            if ((fgDtls.CurrentCell.ColumnIndex == 8) || (fgDtls.CurrentCell.ColumnIndex == 10))
                            {
                                DataGridViewRow row = fgDtls.Rows[RowIndex];
                                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                                {
                                    StatusID = 1;
                                    MyID = iMaxMyID.ToString();
                                }
                                else
                                {
                                    StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_YarnOrderLedger_Tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_YarnOrderLedger_Tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + "")));
                                    MyID = txtCode.Text;
                                }

                                if (MyID != "" && row.Cells[2].Value != null && row.Cells[2].Value.ToString() != "" && row.Cells[2].Value.ToString() != "0" && row.Cells[10].Value != null && row.Cells[10].Value.ToString() != "" && row.Cells[10].Value.ToString() != "0")
                                {
                                    string sBatchNo = string.Empty;
                                    sBatchNo = DB.GetSnglValue("Select YarnPONo from fn_YarnPurchaseOrderMain_Tbl() Where YarnPOID=" + row.Cells[2].Value.ToString());

                                    if (txtUniqueID.Text != null)
                                    {
                                        strQry = string.Format("Delete From tbl_YarnOrderLedger Where Dr_Bags=0 and Dr_Wt=0 and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[25].Value.ToString()) + " and AddedBy=" + Db_Detials.UserID + ";");

                                        strQry += DBSp.InsertIntoYarnOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)", (RowIndex + 1).ToString(),
                                                "(#ENTRYNO#)", dtDoDate.Text,
                                                Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()),
                                                row.Cells[10].Value == null ? "0" : row.Cells[10].Value.ToString() == "" ? "0" : row.Cells[10].Value.ToString(), "0",
                                                0, sBatchNo, dtEntryDate.Text, Localization.ParseNativeDouble(row.Cells[3].Value.ToString()), Localization.ParseNativeDouble(row.Cells[4].Value.ToString()),
                                                Localization.ParseNativeDouble(row.Cells[5].Value.ToString()), Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                                0, 0, 0, Localization.ParseNativeDecimal(row.Cells[8].Value.ToString()),
                                                Localization.ParseNativeDecimal(row.Cells[9].Value.ToString()),
                                                Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()),
                                                0, "NULL", 0,
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
                                                txtUniqueID.Text, Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[25].Value.ToString()), StatusID, "Purchase", 0,
                                                Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                                    }
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
                string SRefID = "";
                object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                if ((e.Control == true & e.KeyCode == Keys.D) | e.KeyCode == Keys.F5)
                {
                    object frm = Navigate.GetActiveChild();
                    dynamic frmObj = frm;
                    int iCalcCol = 0;
                    CIS_DataGridViewEx.DataGridViewEx fgDtls = (CIS_DataGridViewEx.DataGridViewEx)sender;

                    if (fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value != null)
                    {
                        SRefID = fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value.ToString();
                    }
                    else
                    {
                        SRefID = "''";
                    }

                    if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        try
                        {
                            if (Localization.ParseNativeInt(DB.GetSnglValue(string.Format("SELECT COUNT(0) from fn_YarnInwardDtls_Tbl() WHERE ARefID =" + CommonLogic.SQuote(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value.ToString())))) > 0 || (Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select COUNT(0) From fn_YarnOrderLedger_Tbl() Where RefId='" + SRefID + "' and RefID<>'' and Transtype<>" + iIDentity + "")))) > 0)
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityWarning, "", "Reference Found In Another Module..Row Cannot Be Deleted");
                                return;
                            }
                            else
                            {
                                try
                                {
                                    string strQry = string.Format("Update tbl_YarnOrderLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[25].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
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
                                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                                }
                                else
                                {
                                    EventHandles.CreateDefault_Rows(fgDtls, table2, table3, table4, true, false);
                                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);   
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
                                string strQry = string.Format("Update tbl_YarnOrderLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[25].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
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

        private void setTempRowIndex()
        {
            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[25].Value = i;
            }
        }

        private void frmYarnDOEntry_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (strUniqueID != null)
            {
                string strQry = string.Format("Delete From tbl_YarnOrderLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                strQry = strQry + string.Format("Update  tbl_YarnOrderLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                DB.ExecuteSQL(strQry);
                strQry = string.Format("Update tbl_YarnOrderLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                DB.ExecuteSQL(strQry);
            }
        }

        private void setMyID()
        {
            iMaxMyID = Localization.ParseNativeInt(DB.GetSnglValue("Select MAX(MyId + 1) from tbl_YarnOrderLedger Where IsDeleted=0"));

            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[24].Value = iMaxMyID;
            }
        }
    }
}
