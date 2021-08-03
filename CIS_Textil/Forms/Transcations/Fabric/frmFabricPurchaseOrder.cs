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
using CIS_DBLayer;
using CIS_Utilities;

namespace CIS_Textil
{
    public partial class frmFabricPurchaseOrder : frmTrnsIface
    {
        private bool flg_Email;
        private bool flg_Sms;
        private bool FAB_SERIALWISE;
        private int RefMenuID;
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        public frmFabricPurchaseOrder()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
        }

        #region Event

        private void frmFabricPurchaseOrder_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboParty, Combobox_Setup.ComboType.Mst_Suppliers, "");
                Combobox_Setup.FillCbo(ref cboBroker, Combobox_Setup.ComboType.Mst_Brokers, "");
                Combobox_Setup.FillCbo(ref cboDelivaryAt, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                txtEntryNo.Enabled = false;
                FAB_SERIALWISE = Localization.ParseBoolean(GlobalVariables.FAB_SERIALWISE);

                if (FAB_SERIALWISE)
                {
                    fgDtls.Columns[2].Visible = true;
                    fgDtls.Columns[3].ReadOnly = true;
                    fgDtls.Columns[4].ReadOnly = true;
                    fgDtls.Columns[5].ReadOnly = true;
                }
                else
                {
                    fgDtls.Columns[2].Visible = false;
                    fgDtls.Columns[3].ReadOnly = false;
                    fgDtls.Columns[4].ReadOnly = false;
                    fgDtls.Columns[5].ReadOnly = false;
                }

                try
                {
                    // On Opening The Form Order Date is Not Filling
                    if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                    {
                        string sOrderDate = Localization.ToVBDateString(DB.GetSnglValue("select FabPoDate from tbl_FabricPurchaseOrdermain where FabPOID=" + txtCode.Text));
                        dtOrderDate.Text = sOrderDate;
                    }
                    GetRefModID();
                }
                catch (Exception ex1)
                {
                    Navigate.logError(ex1.Message, ex1.StackTrace);
                }

                this.fgDtls.KeyDown += new KeyEventHandler(this.fgDtls_KeyDown);
                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls.CellEndEdit += new DataGridViewCellEventHandler(this.fgDtls_CellEndEdit);
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
                DBValue.Return_DBValue(this, txtCode, "FabPoID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtOrderNo, "FabPONo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtOrderDate, "FabPoDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboParty, "PartyID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDelivaryAt, "DelivaryAtID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCreditdays, "CrDays", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtNarration, "Narration", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEd1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "FabPoID", txtCode.Text, base.dt_HasDtls_Grd);
                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                }

                System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                dgvCellStyle.BackColor = System.Drawing.Color.LightGray;
                dgvCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
                dgvCellStyle.SelectionBackColor = System.Drawing.Color.Purple;
                dgvCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

                //Need to Make New Function For check and Validate 
                //try
                //{
                //    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                //    {
                //        if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_UnionofInwardandPurchase(" + Db_Detials.CompID + "," + Db_Detials.YearID + ") WHERE OrderNo='" + txtOrderNo.Text + "'" + " and FabricDesignId=" + Localization.ParseNativeInt(fgDtls.Rows[i].Cells[3].Value.ToString()) + " and FabricQualityID=" + Localization.ParseNativeInt(fgDtls.Rows[i].Cells[4].Value.ToString()) + " and FabricShadeID=" + Localization.ParseNativeInt(fgDtls.Rows[i].Cells[5].Value.ToString()) + " ")) > 0)
                //        {
                //            fgDtls.Rows[i].ReadOnly = true;
                //            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle;
                //        }
                //        else
                //            fgDtls.Rows[i].ReadOnly = false;
                //    }
                //}
                //catch { }
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
                txtCode.Text = "";
                CIS_Textbox txtEntryNo = this.txtEntryNo;
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                this.txtEntryNo = txtEntryNo;
                txtOrderNo.Text = CommonCls.AutoInc(this, "FabPONo", "FabPOID", "");
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                int MaxId = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(FabPoID),0) From {0}  Where CompID = {1} and YearID = {2} and BranchID={3} and StoreID ={4}", "tbl_FabricPurchaseOrderMain", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.StoreID))));
                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where FabPoID = {1} and CompID={2} and YearID={3} and BranchID={4} and StoreID ={5}", new object[] { "tbl_FabricPurchaseOrderMain", MaxId, Db_Detials.CompID, Db_Detials.YearID  ,Db_Detials.BranchID, Db_Detials.StoreID})))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                        dtOrderDate.Text = Localization.ToVBDateString(reader["FabPoDate"].ToString());
                        cboParty.SelectedValue = Localization.ParseNativeInt(reader["PartyID"].ToString());
                        cboBroker.SelectedValue = Localization.ParseNativeInt(reader["BrokerID"].ToString());
                        cboTransport.SelectedValue = Localization.ParseNativeInt(reader["TransportID"].ToString());
                        cboDelivaryAt.SelectedValue = Localization.ParseNativeInt(reader["DelivaryAtID"].ToString());
                    }
                }
                dtEntryDate.Focus();
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
                (dtOrderDate.TextFormat(false, true)),
                (cboParty.SelectedValue),
                (cboBroker.SelectedValue),
                (cboTransport.SelectedValue),
                (cboDelivaryAt.SelectedValue),
                (txtCreditdays.Text.ToString()),
                (string.Format("{0:N0}", CommonCls.GetColSum(fgDtls, 7, -1, -1)).Replace(",", "")),
                ( string.Format("{0:N2}", CommonCls.GetColSum(fgDtls, 8, -1, -1)).Replace(",", "")),
                (string.Format("{0:N2}", CommonCls.GetColSum(fgDtls, 10, -1, -1)).Replace(",", "")),
                (txtNarration.Text.ToString()),
                (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                (dtEd1.TextFormat(false, true)),
                (txtET1.Text.Trim()),
                (txtET2.Text.Trim()),
                (txtET3.Text.Trim())
                };

                string sBatchNo = string.Empty;
                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_FabricOrderLedger", "(#CodeID#)", Localization.ParseNativeInt(base.iIDentity.ToString()));
                for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    if (row.Cells[3].Value != null && row.Cells[3].Value.ToString() != "" && row.Cells[7].Value != null && row.Cells[7].Value.ToString() != "")
                    {
                        if (Localization.ParseNativeDouble(row.Cells[7].Value.ToString()) > 0)
                        {
                            //For Checking Fabric Outward
                            //strAdjQry += DBSp.InsertIntoFabricOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                            //           "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)",
                            //           dtChlnDate.Text, Localization.ParseNativeDouble(cboParty.SelectedValue.ToString()),
                            //           row.Cells[43].Value == null ? "0" : row.Cells[43].Value.ToString() == "" ? "0" : row.Cells[43].Value.ToString(),
                            //           "0", row.Cells[2].Value.ToString(), sBatchNo,
                            //           Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                            //           Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                            //           Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                            //           Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                            //           Localization.ParseNativeDouble(row.Cells[24].Value.ToString()),
                            //           Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                            //           0, 0, 0,
                            //           Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                            //           Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                            //           Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()),
                            //           0, row.Cells[24].Value == null || row.Cells[24].Value.ToString() == "" || row.Cells[24].Value.ToString() == "0" ? "NULL" : Convert.ToString(row.Cells[24].Value), 0,
                            //           row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                            //           row.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[32].Value.ToString()),
                            //           row.Cells[33].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[33].Value.ToString()),
                            //           row.Cells[34].Value == null || row.Cells[34].Value.ToString() == "" || row.Cells[34].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[34].Value.ToString()),
                            //           row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" || row.Cells[35].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[35].Value.ToString()),
                            //           row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" ? "-" : row.Cells[36].Value.ToString(),
                            //           row.Cells[37].Value == null || row.Cells[37].Value.ToString() == "" ? "-" : row.Cells[37].Value.ToString(),
                            //           row.Cells[38].Value == null || row.Cells[38].Value.ToString() == "" ? "-" : row.Cells[38].Value.ToString(),
                            //           row.Cells[39].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[39].Value.ToString()),
                            //           row.Cells[40].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[40].Value.ToString()),
                            //           "NULL", i, 1, "Sales", row.Cells[41].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[41].Value.ToString()),
                            //           Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);

                            strAdjQry += DBSp.InsertIntoFabricOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtOrderDate.Text,
                                    Localization.ParseNativeDouble(cboParty.SelectedValue.ToString()), base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(), "0",
                                    "(#CodeID#)", txtOrderNo.Text, Localization.ParseNativeDouble(row.Cells[2].Value.ToString()), Localization.ParseNativeDouble(row.Cells[4].Value.ToString()),
                                    Localization.ParseNativeDouble(row.Cells[3].Value.ToString()), Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                    Localization.ParseNativeDouble(row.Cells[6].Value.ToString()), 0,
                                    Localization.ParseNativeDecimal(row.Cells[7].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[8].Value.ToString()), 0, 0, 0, 0, Localization.ParseNativeDecimal(row.Cells[9].Value.ToString()),
                                    row.Cells[13].Value == null || row.Cells[13].Value.ToString() == "" || row.Cells[13].Value.ToString() == "0" ? "NULL" : Convert.ToString(row.Cells[13].Value), 0,
                                    row.Cells[14].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[14].Value.ToString()),
                                    row.Cells[15].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[15].Value.ToString()),
                                    row.Cells[16].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[16].Value.ToString()),
                                    row.Cells[17].Value == null || row.Cells[17].Value.ToString() == "" || row.Cells[17].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[17].Value.ToString()),
                                    row.Cells[18].Value == null || row.Cells[18].Value.ToString() == "" || row.Cells[18].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[18].Value.ToString()),
                                    row.Cells[19].Value == null || row.Cells[19].Value.ToString() == "" ? "-" : row.Cells[19].Value.ToString(),
                                    row.Cells[20].Value == null || row.Cells[20].Value.ToString() == "" ? "-" : row.Cells[20].Value.ToString(),
                                    row.Cells[21].Value == null || row.Cells[21].Value.ToString() == "" ? "-" : row.Cells[21].Value.ToString(),
                                    row.Cells[22].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[22].Value.ToString()),
                                    row.Cells[23].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[23].Value.ToString()),  "NULL", i, 1, "Purchase", this.frmVoucherTypeID,
                                    Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                        }
                    }
                }

                double dblTransID = 0;
                string sPartyID = cboParty.SelectedValue.ToString();
                DBSp.Transcation_AddEdit_Trans(pArrayData, this.fgDtls, true, ref dblTransID, strAdjQry, "", txtEntryNo.Text, txtOrderNo.Text, "FabPONo");
                if (blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    string sEntryNo = DB.GetSnglValue("SELECT  Entryno from fn_FabricPurchaseOrderMain() WHERE FabPOID=" + dblTransID);
                    flg_Sms = Localization.ParseBoolean(GlobalVariables.SMS_SEND_SO);
                    flg_Email = Localization.ParseBoolean(GlobalVariables.EMAIL_SEND_SO);

                    if (flg_Sms == true || flg_Email == true)
                    {
                        if (flg_Sms == true)
                        {
                            try { CommonCls.SendSms(dblTransID.ToString(), base.iIDentity.ToString(), 1, sPartyID); }
                            catch { }
                        }

                        if (flg_Email == true)
                        {
                            try
                            {
                                CommonCls.sendEmail(dblTransID.ToString(), sEntryNo, sPartyID, base.iIDentity.ToString());
                            }
                            catch { }
                        }
                    }
                }
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
                if ((!EventHandles.IsRequiredInGrid(fgDtls, this.dt_AryIsRequired, false)))
                {
                    return true;
                }

                if (!CommonCls.CheckDate(dtEntryDate.Text, true))
                    return true;

                if (!CommonCls.CheckDate(dtOrderDate.Text, true))
                    return true;

                DataGridViewEx ex2 = this.fgDtls;
                for (int i = 0; i <= (ex2.RowCount - 1); i++)
                {
                    if (ex2.Rows[i].Cells[10].Value != null)
                    {
                        if (!CommonCls.CheckDate(ex2.Rows[i].Cells[10].Value.ToString(), false))
                        {
                            return true;
                        }
                    }
                }
                ex2 = null;

                if (txtOrderNo.Text.Trim() == "" || txtOrderNo.Text.Trim() == "-" || txtOrderNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Order No.");
                    txtOrderNo.Focus();
                    return true;
                }

                if (!Information.IsDate(dtOrderDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Order Date");
                    dtOrderDate.Focus();
                    return true;
                }

                if (txtOrderNo.Text.Trim().Length > 0)
                {
                    string strtblName;
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        strtblName = "tbl_FabricPurchaseOrderMain";
                        if (Navigate.CheckDuplicate(ref strtblName, "FabPONo", txtOrderNo.Text, false, "", 0L, string.Format("CompID = {0} and YearID = {1}", Db_Detials.CompID, Db_Detials.YearID), "This Purchase Order No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where FabPONo = '{1}' And CompID = {2} And YearID = {3}", new object[] { "tbl_FabricPurchaseOrderMain", txtOrderNo.Text.ToString(), Db_Detials.CompID, Db_Detials.YearID }))))
                        {
                            txtOrderNo.Focus();
                            return true;
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        strtblName = "tbl_FabricPurchaseOrderMain";
                        if (Navigate.CheckDuplicate(ref strtblName, "FabPONo", txtOrderNo.Text, true, "FabPOID", Localization.ParseNativeLong(txtCode.Text.Trim()), string.Format("CompID = {0} and YearID = {1}", Db_Detials.CompID, Db_Detials.YearID), "This Purchase Order No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where FabPONo = '{1}' And CompID = {2} And YearID = {3}", new object[] { "tbl_FabricPurchaseOrderMain", txtOrderNo.Text.ToString(), Db_Detials.CompID, Db_Detials.YearID }))))
                        {
                            txtOrderNo.Focus();
                            return true;
                        }
                    }
                }
                if (cboParty.SelectedValue == null || cboParty.Text.Trim().ToString() == "-" || cboParty.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Supplier ");
                    cboParty.Focus();
                    return true;
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

        //private void CalcVal()
        //{
        //    try
        //    {
        //        lblTotalQty.Text = string.Format("{0:N0}", CommonCls.GetColSum(fgDtls, 8, -1, -1));
        //        lblTotalMtrs.Text = string.Format("{0:N2}", CommonCls.GetColSum(fgDtls, 9, -1, -1));
        //        lblTotalAmt.Text = string.Format("{0:N2}", CommonCls.GetColSum(fgDtls, 11, -1, -1));
        //    }
        //    catch (Exception ex)
        //    {
        //        Navigate.logError(ex.Message, ex.StackTrace);
        //    }
        //}

        public void PrintRecord()
        {
            try
            {
                CIS_ReportTool.frmMultiPrint.MenuID = base.iIDentity;
                CIS_ReportTool.frmMultiPrint.Id = Localization.ParseNativeInt(this.txtCode.Text);
                CIS_ReportTool.frmMultiPrint.TblNm = "tbl_FabriCPurchaseOrderMain";
                CIS_ReportTool.frmMultiPrint.IdStr = "FabPOID";
                CIS_ReportTool.frmMultiPrint frmMPrnt = new CIS_ReportTool.frmMultiPrint();
                CIS_ReportTool.frmMultiPrint.iCompID = Db_Detials.CompID;
                CIS_ReportTool.frmMultiPrint.iYearID = Db_Detials.YearID;
                CIS_ReportTool.frmMultiPrint.iUserID = Db_Detials.UserID;
                CIS_ReportTool.frmMultiPrint.objReport = Db_Detials.objReport;
                CIS_ReportTool.frmMultiPrint.sApplicationName = GetAssemblyInfo.ProductName;
                CIS_ReportTool.frmMultiPrint.VoucherTypeID= 0;

                if (frmMPrnt.ShowDialog() == DialogResult.Cancel)
                {
                    frmMPrnt.Dispose();
                }
                else
                {
                    frmMPrnt = null;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboParty_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if ((this.cboParty.SelectedValue != null) && (Conversion.Val(this.cboParty.SelectedValue) > 0.0))
                {
                    this.cboBroker.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select BrokerID From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboParty.SelectedValue)));
                    this.cboTransport.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select TransportId From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboParty.SelectedValue)));
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
                if (FAB_SERIALWISE)
                {
                    if (e.ColumnIndex == 2)
                    {
                        if (fgDtls.Rows[e.RowIndex].Cells[2].Value != null)
                        {
                            using (IDataReader dr = DB.GetRS("Select FabricDesignID,FabricQualityID,FabricShadeID from fn_FabricMaster_tbl() where FabricID=" + fgDtls.Rows[e.RowIndex].Cells[2].Value + ""))
                            {
                                while (dr.Read())
                                {
                                    //fgDtls.Rows.Add();
                                    fgDtls.Rows[e.RowIndex].Cells[3].Value = Localization.ParseNativeInt(dr["FabricDesignID"].ToString());
                                    fgDtls.Rows[e.RowIndex].Cells[4].Value = Localization.ParseNativeInt(dr["FabricQualityID"].ToString());
                                    fgDtls.Rows[e.RowIndex].Cells[5].Value = Localization.ParseNativeInt(dr["FabricShadeID"].ToString());
                                }
                            }
                        }
                    }
                }

                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    if (e.ColumnIndex == 3)
                    {
                        if (fgDtls.Rows[e.RowIndex].Cells[3].Value != null && fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString().Length > 0)
                        {
                            fgDtls.Rows[e.RowIndex].Cells[4].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", fgDtls.Rows[e.RowIndex].Cells[3].Value)));
                            fgDtls.Rows[e.RowIndex].Cells[4].ReadOnly = true;
                        }
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
            try
            {
                if (e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
                {
                    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    {
                        if (fgDtls.Rows[i].Cells[2].Value == null || fgDtls.Rows[i].Cells[2].Value.ToString() == "" || fgDtls.Rows[i].Cells[2].Value.ToString() == "0" && fgDtls.Rows[i].Cells[3].Value != null && fgDtls.Rows[i].Cells[3].Value.ToString() != "" && fgDtls.Rows[i].Cells[4].Value != null && fgDtls.Rows[i].Cells[4].Value.ToString() != "" && fgDtls.Rows[i].Cells[5].Value != null && fgDtls.Rows[i].Cells[5].Value.ToString() != "")
                        {
                            fgDtls.Rows[i].Cells[2].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricID from fn_FabricMaster_tbl() where FabricDesignID={0} and FabricQualityID={1} and FabricShadeID={2}", fgDtls.Rows[i].Cells[3].Value, fgDtls.Rows[i].Cells[4].Value, fgDtls.Rows[i].Cells[5].Value)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void txtOrderNo_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (txtOrderNo.Text.Trim().Length > 0)
                {
                    string strTblName;
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        strTblName = "tbl_FabricPurchaseOrderMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "FabPONo", txtOrderNo.Text, false, "", 0L, string.Format("CompID = {0} and YearID = {1}", Db_Detials.CompID, Db_Detials.YearID), "This Purchase Order No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where FabPONo = {1} and CompID = {2} and YearID = {3}", new object[] { "tbl_FabricPurchaseOrderMain", txtOrderNo.Text.ToString(), Db_Detials.CompID, Db_Detials.YearID }))))
                        {
                            txtOrderNo.Focus();
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        strTblName = "tbl_FabricPurchaseOrderMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "FabPONo", txtOrderNo.Text, true, "FabPOID", Localization.ParseNativeLong(txtCode.Text.Trim()), string.Format("CompID = {0} and YearID = {1}", Db_Detials.CompID, Db_Detials.YearID), "This Purchase Order No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where FabPONo = {1} and CompID = {2} and YearID = {3}", new object[] { "tbl_FabricPurchaseOrderMain", txtOrderNo.Text.ToString(), Db_Detials.CompID, Db_Detials.YearID }))))
                        {
                            txtOrderNo.Focus();
                        }
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
                RefMenuID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MenuID From tbl_VoucherTypeMaster Where GenMenuID=" + iIDentity + "")));
                if (RefMenuID == 0)
                {
                    RefMenuID = iIDentity;
                }
            }
            catch { }
            #endregion
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
                            if ((Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select COUNT(0) From fn_FabricOrderLedger() Where RefId='" + fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[24].Value + "' and RefID<>'' and Transtype<>" + iIDentity + ""))) > 0))
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
