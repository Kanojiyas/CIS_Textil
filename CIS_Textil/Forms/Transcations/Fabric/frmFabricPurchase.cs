using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_CLibrary;
using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmFabricPurchase : frmTrnsIface
    {
        private bool FP_ORD_WISE;
        private bool FO_BOOKDESIGN;

        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        public DataGridViewEx fgDtls_f;
        public DataGridViewEx fgDtls_f_footer;

        private int _MtrsID = 0;
        private bool IsOrderSelected = false;
        public ArrayList OrgInGridArray;
        private bool ENABLE_BROKER_FAB_PURCHASE;
        private bool ENABLE_BROKER_CALCMETHOD1;
        private bool ENABLE_BROKER_CALCMETHOD2;
        string SBrokersPerc = string.Empty;
        private bool Vld_DupPieceNo;
        private bool isRet = false;
        private int ExcessInward_Mtrs;
        private bool isStockpost = true;
        private bool isOrderused = false;
        private bool IsCheckOrders = false;
        private string SRateCalcType = string.Empty;
        private static string RefVoucherID;
        private int RefMenuID;
        private int iMaxMyID;
        private int iMaxMyID_Stock;
        public string strUniqueID;

        public frmFabricPurchase()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;

            fgDtls_f = GrdDtls.fgDtls;
            fgDtls_f_footer = GrdDtls.fgDtls_f;
        }

        #region Event

        private void frmFabricPurchase_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                FP_ORD_WISE = Localization.ParseBoolean(GlobalVariables.FP_ORD_WISE);
                ENABLE_BROKER_FAB_PURCHASE = Localization.ParseBoolean(GlobalVariables.ENABLE_BROKER_FAB_PURCHASE);
                ENABLE_BROKER_CALCMETHOD1 = Localization.ParseBoolean(GlobalVariables.ENABLE_BROKER_CALCMETHOD1);
                ENABLE_BROKER_CALCMETHOD2 = Localization.ParseBoolean(GlobalVariables.ENABLE_BROKER_CALCMETHOD2);
                Vld_DupPieceNo = Localization.ParseBoolean(GlobalVariables.Vld_DupPieceNo);
                ExcessInward_Mtrs = Localization.ParseNativeInt(GlobalVariables.EX_IN_MTRS_PERC.ToString());
                Combobox_Setup.FillCbo(ref cboSupplier, Combobox_Setup.ComboType.Mst_Suppliers, "");
                Combobox_Setup.FillCbo(ref cboBroker, Combobox_Setup.ComboType.Mst_Brokers, "");
                Combobox_Setup.FillCbo(ref cboDeptTo, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
                Combobox_Setup.FillCbo(ref cboAcType, Combobox_Setup.ComboType.PurchaseAc, "");
                _MtrsID = Localization.ParseNativeInt(DB.GetSnglValue("SELECT UnitID from tbl_UnitsMaster WHERE IsDeleted=0 and UnitName='Meters'"));
                string sqlQuery = string.Format("Select FabricInwardID, ChallanNo From {0} Where IsDeleted=0 and CompID = {1} And YearID = {2} and BranchID={3} and StoreID ={4} ", "tbl_FabricInwardMain", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.StoreID);
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls_f, fgDtls_f_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, true, true, 0, 1, true);
                txtEntryNo.Enabled = false;
                _MtrsID = Localization.ParseNativeInt(DB.GetSnglValue("SELECT  UnitID from tbl_UnitsMaster WHERE  UnitName='Meters'"));
                cboOrderType.SelectedItem = "WITHOUT ORDER";
                cboOrderType.AutoDropdown = true;
                cboOrderType.AutoComplete = true;
                btnMultipleorders.Enabled = false;
                btnSelectInward.Enabled = true;

                if (ENABLE_BROKER_FAB_PURCHASE)
                {
                    if (ENABLE_BROKER_CALCMETHOD2)
                    {
                        fgDtls.Columns[20].Visible = true;
                        fgDtls.Columns[21].Visible = true;
                        txtBrokerPercent.Enabled = false;
                    }
                    else
                    {
                        fgDtls.Columns[20].Visible = false;
                        fgDtls.Columns[21].Visible = false;
                        txtBrokerPercent.Enabled = true;
                    }
                }
                else
                {
                    lblBrokersPercent.Visible = false;
                    lblBrokerPercentColon.Visible = false;
                    lblBrokersAmt.Visible = false;
                    lblBrokersAmtColon.Visible = false;
                    txtBrokerPercent.Visible = false;
                    txtBrokerTotalAmount.Visible = false;
                    fgDtls.Columns[20].Visible = false;
                    fgDtls.Columns[21].Visible = false;
                }

                FO_BOOKDESIGN = Localization.ParseBoolean(GlobalVariables.FO_BOOKDESIGN);
                if (FO_BOOKDESIGN)
                {
                    fgDtls.Columns[6].Visible = true;
                }
                else
                {
                    fgDtls.Columns[6].Visible = false;
                }
                cboOrderType_SelectedIndexChanged(null, null);
                GetRefModID();

                this.fgDtls_f.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_f_CellValueChanged);
                this.fgDtls.RowsAdded += new DataGridViewRowsAddedEventHandler(this.fgDtls_RowsAdded);
                this.fgDtls.CellEndEdit += new DataGridViewCellEventHandler(this.fgDtls_CellEndEdit);
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
                DBValue.Return_DBValue(this, txtCode, "FabPurchaseID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtBillNo, "BillNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtBillDate, "BillDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboSupplier, "SupplierID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDeptTo, "DepartmentId", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLrNo, "LrNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtLrDate, "LrDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboAcType, "PurchaseACID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCrDays, "CrDays", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtDueDate, "DueDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, TxtTotalCuts, "TotPcs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, TxtTotMtrs, "TotMtrs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, TxtGrossAmount, "GrossAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAddLessAmt, "AddLessAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtNetAmt, "NetAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEd1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                try
                {
                    string sOrderType = DBValue.Return_DBValue(this, "OrderType");
                    cboOrderType.SelectedItem = sOrderType;
                }
                catch { }

                DBValue.Return_DBValue(this, txtBrokerPercent, "BrokerAvgPercentage", Enum_Define.ValidationType.Text);
                try
                {
                    DBValue.Return_DBValue(this, txtBrokerTotalAmount, "BrokerTotalAmount", Enum_Define.ValidationType.Text);
                }
                catch { }

                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "FabPurchaseID", txtCode.Text, base.dt_HasDtls_Grd);
                DetailGrid_Setup.FillGrid(fgDtls_f, this.fgDtls_f.Grid_UID, this.fgDtls_f.Grid_Tbl, "FabPurchaseID", txtCode.Text, base.dt_HasDtls_Grd);
                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                    //GetOrder();
                }
                CalcVal();
                //OrgInGridArray.Clear();
                isStockpost = true;

                int icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_FabricOrderLedger_tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    cboOrderType.Enabled = false;
                    cboSupplier.Enabled = false;
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CreateDefault_Rows(fgDtls_f, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    setTempRowIndex();

                    try
                    {
                        string strOldUniqueID = string.Empty;
                        strOldUniqueID = txtUniqueID.Text;
                        txtUniqueID.Text = CommonCls.GenUniqueID();
                        strUniqueID = txtUniqueID.Text;
                        if (icount == 0)
                        {
                            string strQry = string.Format("Update tbl_FabricOrderLedger Set UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + ", StatusID=2 Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + "");
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
                        string strQry = string.Format("Delete From tbl_FabricOrderLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and CUserId=" + Db_Detials.UserID + ";");
                        strQry = strQry + string.Format("Update  tbl_FabricOrderLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and CUserId=" + Db_Detials.UserID + ";");
                        DB.ExecuteSQL(strQry);
                        strQry = string.Format("Update tbl_FabricOrderLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                        DB.ExecuteSQL(strQry);
                    }
                }

                if ((base.blnFormAction == Enum_Define.ActionType.View_Record) && !(base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_FabricOrderLedger_tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));
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
                            //  btnSelectOrder.Enabled = false;
                            fgDtls.Rows[i].ReadOnly = true;
                            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle_Ref;
                        }
                        else
                        {
                            //btnSelectOrder.Enabled = true;
                            fgDtls.Rows[i].ReadOnly = false;
                        }
                    }
                }
                catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }

                if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                {
                    EnabDisab(false);
                }
                else if (cboOrderType.SelectedItem.ToString() == "WITHOUT ORDER")
                {
                    EnabDisab(true);
                }
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
                    string strQry = string.Format("Delete From tbl_FabricOrderLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and CUserId=" + Db_Detials.UserID + ";");
                    strQry = strQry + string.Format("Update  tbl_FabricOrderLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and CUserId=" + Db_Detials.UserID + ";");
                    DB.ExecuteSQL(strQry);
                    strQry = string.Format("Update tbl_FabricOrderLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                    DB.ExecuteSQL(strQry);
                }

                txtCode.Text = "";
                CIS_Textbox txtEntryNo = this.txtEntryNo;
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                this.txtBillNo.Text = CommonCls.AutoInc(this, "BillNo", "FabPurchaseID", "");
                this.txtEntryNo = txtEntryNo;
                OrgInGridArray = new ArrayList();
                string sqlQuery = string.Empty;

                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CreateDefault_Rows(fgDtls_f, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);

                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                EventHandles.CalculateFooter_Rows(fgDtls_f, fgDtls_f_footer, fgDtls_f.Grid_ID.ToString(), fgDtls_f.Grid_UID);

                int MaxiD = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(FabPurchaseID),0) From {0}  Where IsDeleted=0 and CompID = {1} and YearID = {2} and BranchID = {3} and StoreID = {4}", "tbl_FabricPurchaseMain", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.StoreID))));
                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where  FabPurchaseID = {1} and CompID={2} and YearID={3} and BranchID = {4} and StoreID = {5}", new object[] { "tbl_FabricPurchaseMain", MaxiD, Db_Detials.CompID, Db_Detials.YearID , Db_Detials.BranchID, Db_Detials.StoreID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                        dtBillDate.Text = Localization.ToVBDateString(reader["BillDate"].ToString());
                        //dtChallanDate.Text = Localization.ToVBDateString(reader["ChallanDate"].ToString());
                        cboSupplier.SelectedValue = Localization.ParseNativeDouble(reader["SupplierID"].ToString());
                        cboBroker.SelectedValue = Localization.ParseNativeDouble(reader["BrokerID"].ToString());
                        cboDeptTo.SelectedValue = Localization.ParseNativeDouble(reader["DepartmentId"].ToString());
                        cboTransport.SelectedValue = Localization.ParseNativeDouble(reader["TransportID"].ToString());
                        cboAcType.SelectedValue = Localization.ParseNativeDouble(reader["PurchaseID"].ToString());

                        try
                        {
                            if (reader["OrderType"].ToString() != "")
                                cboOrderType.SelectedItem = reader["OrderType"].ToString();
                            else
                                cboOrderType.SelectedItem = "WITH ORDER";
                        }
                        catch { }
                    }
                }
                TxtTotalCuts.Text = "0";
                TxtTotMtrs.Text = "0.00";
                TxtGrossAmount.Text = "0.00";
                txtAddLessAmt.Text = "0.00";
                txtNetAmt.Text = "0.00";
                txtBrokerPercent.Text = "";
                cboSupplier.Enabled = true;
                txtUniqueID.Text = CommonCls.GenUniqueID();
                strUniqueID = txtUniqueID.Text;

                if (fgDtls.Rows.Count > 0)
                {
                    if (fgDtls.Columns[2].Visible)
                    {
                        fgDtls.Rows[0].Cells[5].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select  {0}({1},{2})", new object[] { "dbo.fn_FetchPieceNo_Stock", Db_Detials.CompID, Db_Detials.YearID })), Db_Detials.PCS_NO_INCMT);
                    }
                    else
                    {
                        fgDtls.Rows[0].Cells[5].Value = "-";
                    }
                }
                cboOrderType_SelectedIndexChanged(null, null);
                if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                {
                    EnabDisab(false);
                }
                else if (cboOrderType.SelectedItem.ToString() == "WITHOUT ORDER")
                {
                    EnabDisab(true);
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
                int PurLedgerId = Localization.ParseNativeInt(cboAcType.SelectedValue.ToString());
                ArrayList pArrayData = new ArrayList
                {
                this.frmVoucherTypeID,
                ("(#ENTRYNO#)"),
                (dtEntryDate.TextFormat(false, true)),
                (cboOrderType.SelectedItem),
                (txtBillNo.Text.ToString()),
                (dtBillDate.TextFormat(false, true)),
                (cboSupplier.SelectedValue),
                (cboBroker.SelectedValue),
                (null),
                (cboDeptTo.SelectedValue),
                (cboTransport.SelectedValue),
                (null),
                (txtLrNo.Text.ToString()),
                (dtLrDate.TextFormat(false, true)),
                (PurLedgerId),
                (txtCrDays.Text.ToString()),
                (dtDueDate.TextFormat(false, true)),
                (TxtTotalCuts.Text.ToString().Replace(",", "")),
                (TxtTotMtrs.Text.ToString().Replace(",", "")),
                (TxtGrossAmount.Text.ToString().Replace(",", "")),
                (txtAddLessAmt.Text.ToString().Replace(",", "")),
                (txtNetAmt.Text.ToString().Replace(",", "")),
                (txtDescription.Text.ToString() == ""? "-": txtDescription.Text.ToString()),
                (txtBrokerPercent.Text.ToString().Replace(",", "")),
                (txtBrokerTotalAmount.Text.ToString().Replace(",","")),
                (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                (dtEd1.TextFormat(false, true)),
                (txtET1.Text.Trim()),
                (txtET2.Text.Trim()),
                (txtET3.Text.Trim())
                };

                int UnitID = Localization.ParseNativeInt(fgDtls.Rows[0].Cells[11].Value.ToString());
                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_AcLedger", "(#CodeID#)", base.iIDentity);
                strAdjQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", "(#CodeID#)", base.iIDentity);
                strAdjQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_VatLedger", "(#CodeID#)", base.iIDentity);
                strAdjQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_BrokerLedger", "(#CodeID#)", base.iIDentity);

                strAdjQry += DBSp.InsertInto_AcLedger("(#CodeID#)", "0", "(#ENTRYNO#)", dtEntryDate.Text,
                                Localization.ParseNativeDouble(base.iIDentity.ToString()), cboSupplier.SelectedValue.ToString(),
                                2, Db_Detials.Ac_AdjType.NewRef, "(#CodeID#)", txtBillNo.Text.Trim(), dtBillDate.Text,
                                Localization.ParseNativeDouble(base.iIDentity.ToString()), 0,
                                Localization.ParseNativeDecimal(txtNetAmt.Text), txtDescription.Text.Trim(),
                                Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);

                strAdjQry += DBSp.InsertInto_AcLedger("(#CodeID#)", "0", "(#ENTRYNO#)", dtEntryDate.Text,
                                Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                Conversions.ToString(Interaction.IIf(cboAcType.IsValidSelect, PurLedgerId,
                                RuntimeHelpers.GetObjectValue(cboAcType.SelectedValue))), 1, Db_Detials.Ac_AdjType.NewRef,
                                "(#CodeID#)", txtBillNo.Text.Trim(), dtBillDate.Text,
                                Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                Localization.ParseNativeDecimal(TxtGrossAmount.Text), 0, txtDescription.Text.Trim(),
                                Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);

                DataGridViewEx ex = this.fgDtls_f;
                double dblDedAmt = 0.0;
                for (int i = 0; i <= (ex.RowCount - 1); i++)
                {
                    DataGridViewRow row = ex.Rows[i];
                    if (row.Cells[5].Value != null)
                    {
                        if (Operators.ConditionalCompareObjectEqual(row.Cells[6].FormattedValue, "-", false))
                        {
                            dblDedAmt = -Localization.ParseNativeDouble(row.Cells[7].Value.ToString());
                        }
                        else if (Operators.ConditionalCompareObjectEqual(row.Cells[6].FormattedValue, "+", false))
                        {
                            dblDedAmt = Localization.ParseNativeDouble(row.Cells[7].Value.ToString());
                        }

                        if (dblDedAmt > 0.0)
                        {
                            strAdjQry += DBSp.InsertInto_AcLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text,
                                 Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                 row.Cells[5].Value.ToString(), 1, Db_Detials.Ac_AdjType.OnAccount, "(#CodeID#)", txtBillNo.Text.Trim(),
                                 dtBillDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDecimal(dblDedAmt.ToString()),0,
                                 "null", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                        }
                        else
                        {
                            string sDedAmt = dblDedAmt.ToString();
                            if (sDedAmt.StartsWith("-"))
                            {
                                sDedAmt = sDedAmt.Substring(1);
                            }
                            dblDedAmt = Localization.ParseNativeDouble(sDedAmt.ToString());
                            strAdjQry += DBSp.InsertInto_AcLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text,
                                Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                row.Cells[5].Value.ToString(), 2, Db_Detials.Ac_AdjType.OnAccount, "(#CodeID#)", txtBillNo.Text.Trim(),
                                dtBillDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), 0, Localization.ParseNativeDecimal(dblDedAmt.ToString()),
                                "null", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                        }
                    }
                    row = null;
                }
                ex = null;

                #region VatLedger Posting
                try
                {
                    string sVatAcMisc = DB.GetSnglValue("Select MiscID from fn_MiscMaster_Tbl() Where MiscName='VAT'");
                    for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                    {
                        DataGridViewRow row2 = fgDtls_f.Rows[i];
                        string sVatAcLedger = DB.GetSnglValue("select TaxtypeID from fn_LedgerMaster_Tbl() Where LedgerId=" + fgDtls_f.Rows[i].Cells[2].Value + "");
                        if (sVatAcMisc == sVatAcLedger)
                        {
                            if (Operators.ConditionalCompareObjectEqual(row2.Cells[3].FormattedValue, "+", false))
                            {
                                strAdjQry += DBSp.InsertInto_VatLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                             row2.Cells[2].Value.ToString(), Localization.ParseNativeInt(row2.Cells[3].Value.ToString()), Localization.ParseNativeDecimal(row2.Cells[4].Value.ToString()),
                                             "(#CodeID#)", Localization.ParseNativeDecimal(row2.Cells[5].Value.ToString()), 0, "null", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                            }
                            if (Operators.ConditionalCompareObjectEqual(row2.Cells[3].FormattedValue, "-", false))
                            {
                                strAdjQry += DBSp.InsertInto_VatLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                         row2.Cells[2].Value.ToString(), Localization.ParseNativeInt(row2.Cells[3].Value.ToString()), Localization.ParseNativeDecimal(row2.Cells[4].Value.ToString()),
                                         "(#CodeID#)", 0, Localization.ParseNativeDecimal(row2.Cells[5].Value.ToString()), "null", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                            }
                        }
                    }
                }
                catch { }
                #endregion

                isStockpost = true;
                for (int l = 0; l <= fgDtls.Rows.Count - 1; l++)
                {
                    if (fgDtls.Rows[l].Cells[2].Value != null && fgDtls.Rows[l].Cells[2].Value.ToString() != "" && fgDtls.Rows[l].Cells[2].Value.ToString() != "-" && fgDtls.Rows[l].Cells[2].Value.ToString() != "0")
                    {
                        isStockpost = false;
                        break;
                    }
                }

                if (isStockpost)
                {
                    strAdjQry += sStockPosting();
                }

                for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                {
                    DataGridViewRow row2 = fgDtls.Rows[i];
                    try
                    {
                        if (ENABLE_BROKER_CALCMETHOD2)
                        {
                            if (ENABLE_BROKER_FAB_PURCHASE)
                            {
                                if (row2.Cells[21].Value != null && row2.Cells[21].Value.ToString() != "" && row2.Cells[21].Value.ToString() != "0")
                                {
                                    if (cboBroker.SelectedValue != null && cboBroker.SelectedValue.ToString() != "" && cboBroker.SelectedValue.ToString() != "0")
                                    {
                                        strAdjQry = strAdjQry + DBSp.InsertIntoBrokerLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboBroker.SelectedValue.ToString()), row2.Cells[5].Value.ToString(), dtEntryDate.Text.ToString(), Localization.ParseNativeDouble(row2.Cells[11].Value.ToString()), Localization.ParseNativeDecimal(row2.Cells[20].Value.ToString()), Localization.ParseNativeDecimal(row2.Cells[21].Value.ToString()), 0, "-", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date, 0, 1);
                                    }
                                }
                            }
                        }
                    }
                    catch { }
                    row2 = null;
                }

                try
                {
                    if (ENABLE_BROKER_CALCMETHOD1)
                    {
                        if (ENABLE_BROKER_FAB_PURCHASE)
                        {
                            if (txtBrokerTotalAmount.Text != null && txtBrokerTotalAmount.Text != "0.00")
                            {
                                if (cboBroker.SelectedValue != null && cboBroker.SelectedValue.ToString() != "" && cboBroker.SelectedValue.ToString() != "0")
                                {
                                    strAdjQry = strAdjQry + DBSp.InsertIntoBrokerLedger("(#CodeID#)", "0", "(#ENTRYNO#)", Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboBroker.SelectedValue.ToString()), "", dtEntryDate.Text.ToString(), 0, Localization.ParseNativeDecimal(txtBrokerPercent.Text.ToString()), Localization.ParseNativeDecimal(txtBrokerTotalAmount.Text), 0, "-", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date, 0, 0);
                                }
                            }
                        }
                    }
                }
                catch { }

                if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                {
                    strAdjQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_FabricOrderLedger", "(#CodeID#)", Localization.ParseNativeInt(base.iIDentity.ToString()));
                    for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                    {
                        DataGridViewRow row = fgDtls.Rows[i];
                        if (row.Cells[3].Value != null && row.Cells[3].Value.ToString() != "" && row.Cells[3].Value.ToString() != "0" && row.Cells[13].Value != null && row.Cells[13].Value.ToString() != "" && row.Cells[13].Value.ToString() != "0")
                        {
                            string sBatchNo = string.Empty;
                            sBatchNo = DB.GetSnglValue("Select FabPONo from fn_FabricPurchaseOrderMain_Tbl() Where FabPOID=" + row.Cells[3].Value.ToString());

                            if (Localization.ParseNativeDouble(row.Cells[13].Value.ToString()) > 0)
                            {
                                strAdjQry += DBSp.InsertIntoFabricOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)",
                                        dtBillDate.Text, Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()), row.Cells[37].Value == null ? "0" : row.Cells[37].Value.ToString() == "" ? "0" : row.Cells[37].Value.ToString(),
                                        "0", row.Cells[4].Value.ToString(), sBatchNo, Localization.ParseNativeDouble(row.Cells[6].Value.ToString()), Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                        Localization.ParseNativeDouble(row.Cells[7].Value.ToString()), Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                        Localization.ParseNativeDouble(row.Cells[11].Value.ToString()), 0, 0, 0, 0, Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()),
                                        Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()), 0, 0, "NULL", 0,
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
                                        "NULL", i, 1, "Purchase", row.Cells[35].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[35].Value.ToString()), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                            }
                            row = null;
                        }
                    }
                }

                //if (cboTransport.SelectedValue != null && Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()) > 0.0)
                //{
                //    strAdjQry = strAdjQry + DBSp.InsertIntoTrasportLedger("(#CodeID#)", txtBillNo.Text.ToString(), dtBillDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()), Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()), Localization.ParseNativeDouble(cboDeptTo.SelectedValue.ToString()), txtLrNo.Text, dtLrDate.Text, null, Localization.ParseNativeDouble(UnitID.ToString()), Localization.ParseNativeInt(TxtTotalCuts.Text), Localization.ParseNativeDecimal(TxtTotMtrs.Text), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                //}
                strAdjQry += string.Format("Delete From tbl_FabricOrderLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                DBSp.Transcation_AddEdit(pArrayData, this.fgDtls, true, strAdjQry, "", txtEntryNo.Text, "", "", new DataGridViewEx[] { this.fgDtls_f });
            }
            catch (Exception exception1)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", exception1.Message);
            }
        }

        private string sStockPosting()
        {
            string strAdjQry = "";
            for (int j = 0; j <= (fgDtls.RowCount - 1); j++)
            {
                DataGridViewRow row2 = fgDtls.Rows[j];
                if (Localization.ParseNativeDouble(row2.Cells[12].Value.ToString()) != 0.0)
                {
                    if (row2.Cells[2].Value == null || row2.Cells[2].Value.ToString() == "0" || row2.Cells[2].Value.ToString() == "")
                    {
                        string LotNo;
                        if (row2.Cells[4].Value.ToString() != null && row2.Cells[4].Value.ToString().Length > 0)
                        {
                            LotNo = row2.Cells[4].Value.ToString();
                        }
                        else
                        {
                            LotNo = "-";
                        }
                        if (row2.Cells[5].Value == null)
                        {
                            row2.Cells[5].Value = "-";
                        }

                        int QualityId = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", Localization.ParseNativeInt(row2.Cells[7].Value.ToString()))));

                        strAdjQry += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                                    (j + 1).ToString(), "(#ENTRYNO#)", dtBillDate.Text, Localization.ParseNativeDouble(cboDeptTo.SelectedValue.ToString()),
                                    row2.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[23].Value.ToString()),
                                    base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (j + 1).ToString(), base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (j + 1).ToString(),
                                    LotNo, row2.Cells[5].Value.ToString(), row2.Cells[6].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[6].Value.ToString()),
                                    Localization.ParseNativeDouble(row2.Cells[8].Value.ToString()), Localization.ParseNativeDouble(row2.Cells[7].Value.ToString()),
                                    Localization.ParseNativeDouble(row2.Cells[9].Value.ToString()), row2.Cells[10].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[10].Value.ToString()),
                                    Localization.ParseNativeDouble(row2.Cells[11].Value.ToString()), Localization.ParseNativeDecimal(row2.Cells[12].Value.ToString()),
                                    Localization.ParseNativeDecimal(row2.Cells[13].Value.ToString()), row2.Cells[14].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[14].Value.ToString()),
                                    0, 0, 0, row2.Cells[17].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[17].Value.ToString()), row2.Cells[18].Value == null ? "NULL" : row2.Cells[18].Value.ToString(), row2.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[24].Value.ToString()),
                                    Localization.ParseNativeInt(cboSupplier.SelectedValue.ToString()), "(#CodeID#)", 0, 0, 0,
                                    row2.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[25].Value.ToString()),
                                    row2.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[26].Value.ToString()),
                                    row2.Cells[27].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[27].Value.ToString()),
                                    row2.Cells[28].Value == null || row2.Cells[28].Value.ToString() == "" || row2.Cells[28].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row2.Cells[29].Value.ToString()),
                                    row2.Cells[29].Value == null || row2.Cells[29].Value.ToString() == "" || row2.Cells[29].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row2.Cells[30].Value.ToString()),
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

            return strAdjQry;
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
                if (!EventHandles.IsRequiredInGrid(fgDtls_f, this.dt_AryIsRequired, false))
                {
                    return true;
                }
                //if (CommonCls.CheckDate(dtEntryDate.Text.ToString(), true) || CommonCls.CheckDate(dtOrderDate.Text.ToString(), true) || CommonCls.CheckDate(dtBillDate.Text.ToString(), true))
                //{
                //    return true;
                //}
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
                if (txtBillNo.Text.Trim() == "" || txtBillNo.Text.Trim() == "-" || txtBillNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Bill No.");
                    txtBillNo.Focus();
                    return true;
                }

                if (txtBillNo.Text.Trim().Length > 0)
                {
                    string strTblName;
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        strTblName = "tbl_FabricPurchaseMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "BillNo", txtBillNo.Text, false, "", 0L, " SupplierID = " + cboSupplier.SelectedValue + " AND CompID = " + Db_Detials.CompID + " And YearID = " + Db_Detials.YearID + "", "This Party already used this Bill No in Entry No : " + DB.GetSnglValue(string.Format("select EntryNo from {0} Where SupplierID = {1} and BillNo = {2}", "tbl_FabricPurchaseMain", cboSupplier.SelectedValue, txtBillNo.Text.ToString()))))
                        {
                            txtBillNo.Focus();
                            return true;
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        strTblName = "tbl_FabricPurchaseMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "BillNo", txtBillNo.Text, true, "FabPurchaseID", Localization.ParseNativeLong(txtCode.Text.Trim()), " SupplierID = " + cboSupplier.SelectedValue + " AND CompID = " + Db_Detials.CompID + " And YearID = " + Db_Detials.YearID + "", "This Party already used this Bill No in Entry No : " + DB.GetSnglValue(string.Format("select EntryNo from {0} Where SupplierID = {1} and BillNo = {2}", "tbl_FabricPurchaseMain", cboSupplier.SelectedValue, txtBillNo.Text.ToString()))))
                        {
                            return true;
                        }
                    }
                }

                if (!Information.IsDate(dtBillDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Bill Date");
                    dtBillDate.Focus();
                    return true;
                }
                if (cboSupplier.SelectedValue == null || cboSupplier.Text.Trim().ToString() == "-" || cboSupplier.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Supplier");
                    cboSupplier.Focus();
                    return true;
                }
                if (cboAcType.SelectedValue == null || cboAcType.Text.Trim().ToString() == "-" || cboAcType.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Purchase Account");
                    cboAcType.Focus();
                    return true;
                }
                if (cboDeptTo.SelectedValue == null || cboDeptTo.Text.Trim().ToString() == "-" || cboDeptTo.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    cboDeptTo.Focus();
                    return true;
                }
                if (ENABLE_BROKER_FAB_PURCHASE)
                {
                    if (cboBroker.SelectedValue == null || cboBroker.Text.Trim().ToString() == "-" || cboBroker.SelectedValue.ToString() == "0")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Broker");
                        cboBroker.Focus();
                        return true;
                    }
                }
                CalcVal();
                if (Vld_DupPieceNo)
                {
                    if (CheckDupPieceNo())
                    {
                        return true;
                    }
                }
                if (FO_BOOKDESIGN)
                {
                    if (CheckSerialCombn())
                    {
                        return true;
                    }
                }
                //CheckOrders();
                //if (isRet)
                //{
                //    return true;
                //}
                return false;
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                return false;
            }
        }

        #endregion

        //private void cboSupplier_Leave(object sender, EventArgs e)
        //{
        //    string sqlQuery = string.Empty;

        //    if (cboOrderType.SelectedItem.ToString() == "WITH ORDER" || cboOrderType.SelectedItem.ToString() != null)
        //    {
        //        GetOrder();
        //    }

        //    //try
        //    //{
        //    //    if (cboSupplier.SelectedValue != null)
        //    //    {
        //    //        if (Localization.ParseNativeInt(cboSupplier.SelectedValue.ToString()) > 0)
        //    //        {
        //    //            sqlQuery = string.Format("Select DISTINCT FabPOID, FabPONO  from {0}({1},{2},{3})", new object[] { "fn_FetchFabPurchaseOrderDtls", cboSupplier.SelectedValue, Db_Detials.CompID, Db_Detials.YearID });
        //    //            Combobox_Setup.Fill_Combo(this.cboOrderNo, sqlQuery, "FabPONo", "FabPOID");
        //    //            CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboOrderNo = this.cboOrderNo;
        //    //            cboOrderNo.ColumnWidths = "0;100;0;0;0;0;0";
        //    //            cboOrderNo.AutoComplete = true;
        //    //            cboOrderNo.AutoDropdown = true;
        //    //        }
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    Navigate.logError(ex.Message, ex.StackTrace);
        //    //}
        //}

        //private void GetOrder()
        //{
        //    try
        //    {
        //        if (FP_ORD_WISE == true)
        //        {
        //            if (blnFormAction == Enum_Define.ActionType.New_Record || blnFormAction == Enum_Define.ActionType.Edit_Record)
        //            {
        //                fgdtls_f_f.DataSource = DB.GetDT(String.Format("Select  CAST('False' as Bit) As [Sel], FabPOID,FabPONo,FabPoDate,DesignID,Design,QualityID,Quality,ShadeID,Shade,TotalPcs,Totalmtrs,(TotalPcs-BalPcs) AS [Dispatch Pcs], (TotalMtrs-BalMtrs) As [Dispatch Mtrs], 0 as CDispatchPcs, 0 as CDispatchMtrs, BalPcs, BalMtrs, Rate from {0}({1},{2},{3})", "fn_FetchFabPurchaseOrderDtls", cboSupplier.SelectedValue, Db_Detials.CompID, Db_Detials.YearID), false);
        //                foreach (UltraGridBand Band in fgdtls_f_f.DisplayLayout.Bands)
        //                {
        //                    foreach (UltraGridColumn Column in Band.Columns)
        //                    {
        //                        using (IDataReader dr = DB.GetRS(String.Format("Select * From {0} Where GridID = {1} and SubGridID = 2 and ColIndex = {2}", "tbl_GridSettings", iIDentity, Column.Index)))
        //                        {
        //                            while (dr.Read())
        //                            {
        //                                Column.Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
        //                                Column.Hidden = Localization.ParseBoolean(dr["IsHidden"].ToString());
        //                                Column.CellActivation = (Localization.ParseBoolean(dr["IsEditable"].ToString()) == true ? Activation.AllowEdit : Activation.NoEdit);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                fgdtls_f_f.DataSource = DB.GetDT(String.Format("Select  CAST('False' as Bit) As [Sel], FabPOID,FabPONo,FabPoDate,DesignID,Design,QualityID,Quality,ShadeID,Shade,TotalPcs,Totalmtrs,(TotalPcs-BalPcs) AS [Dispatch Pcs], (TotalMtrs-BalMtrs) As [Dispatch Mtrs], 0 as CDispatchPcs, 0 as CDispatchMtrs, BalPcs, BalMtrs, Rate from {0}({1},{2},{3})", "fn_FetchFabPurchaseOrderDtls", cboSupplier.SelectedValue, Db_Detials.CompID, Db_Detials.YearID), false);
        //                foreach (UltraGridBand Band in fgdtls_f_f.DisplayLayout.Bands)
        //                {
        //                    foreach (UltraGridColumn Column in Band.Columns)
        //                    {
        //                        using (IDataReader dr = DB.GetRS(string.Format("Select * From {0} Where GridID = {1} and SubGridID = 2 and ColIndex = {2}", Db_Detials.tbl_GridSettings, iIDentity, Column.Index)))
        //                        {
        //                            while (dr.Read())
        //                            {
        //                                Column.Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
        //                                Column.Hidden = Localization.ParseBoolean(dr["IsHidden"].ToString());
        //                                Column.CellActivation = (Localization.ParseBoolean(dr["IsEditable"].ToString()) == true ? Activation.AllowEdit : Activation.NoEdit);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Navigate.logError(ex.Message, ex.StackTrace);
        //    }
        //}

        //private void cboSeries_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (cboSeries.SelectedValue != null)
        //        {
        //            txtBillNo.Text = CommonCls.AutoInc(this, "BillNo", "FabPurchaseID", string.Format("SeriesID = {0}", cboSeries.SelectedValue));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Navigate.logError(ex.Message, ex.StackTrace);
        //    }
        //}

        private void txtCrDays_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (txtCrDays.Text != null && txtCrDays.Text.Trim().Length > 0)
                    {
                        if (dtBillDate.Text != "__/__/____")
                        {
                            DateTime time = Conversions.ToDate(dtBillDate.Text);
                            dtDueDate.Text = Conversions.ToString(time.AddDays(Localization.ParseNativeDouble(txtCrDays.Text)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btnCoppy_Click(object sender, EventArgs e)
        {
            try
            {
                for (long i = 0; i <= 53705; i++)
                {
                    CIS_Textbox txtEntryNo = this.txtEntryNo;
                    CommonCls.IncFieldID(this, ref txtEntryNo, "");
                    this.txtEntryNo = txtEntryNo;
                    txtEntryNo = this.txtBillNo;
                    CommonCls.IncFieldID(this, ref txtEntryNo, "BillNo");
                    txtBillNo = txtEntryNo;
                    if (!ValidateForm())
                    {
                        SaveRecord();
                        Form cForm = null;
                        Navigate.NavigateForm(Enum_Define.Navi_form.Cancel_Record, ref cForm, false, false);
                    }
                }
                Interaction.MsgBox("Copy completed", MsgBoxStyle.Information, null);
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
                TxtTotalCuts.Text = string.Format("{0:N0}", CommonCls.GetColSum(this.fgDtls, 11, -1, -1));
                TxtTotMtrs.Text = string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 12, -1, -1));
                TxtGrossAmount.Text = string.Format("{0:N2}", Math.Round(CommonCls.GetColSum(this.fgDtls, 15, -1, -1)));

                string sIsQualityWise = DB.GetSnglValue(string.Format("select IsItemWise from tbl_BrokerLedger where TransId={0} and TransType={1}", txtCode.Text, base.iIDentity.ToString()));

                ////if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                ////{
                ////    if (sIsQualityWise == "1")
                ////    {
                ////        if (ENABLE_BROKER_CALCMETHOD2)
                ////        {
                ////            if (ENABLE_BROKER_FAB_PURCHASE)
                ////            {
                ////                txtBrokerTotalAmount.Text = string.Format("{0:N2}", Math.Round(CommonCls.GetColSum(this.fgDtls, 17, -1, -1)));
                ////            }
                ////        }
                ////    }
                ////}
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (ENABLE_BROKER_CALCMETHOD2)
                    {
                        if (ENABLE_BROKER_FAB_PURCHASE)
                        {
                            txtBrokerTotalAmount.Text = string.Format("{0:N2}", Math.Round(CommonCls.GetColSum(this.fgDtls, 18, -1, -1)));
                        }
                    }
                }
                double a = 0.0;
                DataGridViewEx ex = this.fgDtls_f;
                for (int i = 0; i <= ex.RowCount - 1; i++)
                {
                    if ((ex.Rows[i].Cells[6].Value != null) && Operators.ConditionalCompareObjectGreater(ex.Rows[i].Cells[6].Value, "0", false))
                    {
                        if (Operators.ConditionalCompareObjectEqual(ex.Rows[i].Cells[6].FormattedValue, "-", false))
                        {
                            a -= Localization.ParseNativeDouble(ex.Rows[i].Cells[7].Value.ToString());
                        }
                        else if (Operators.ConditionalCompareObjectEqual(ex.Rows[i].Cells[6].FormattedValue, "+", false))
                        {
                            a += Localization.ParseNativeDouble(ex.Rows[i].Cells[7].Value.ToString());
                        }
                    }
                }
                ex = null;
                txtAddLessAmt.Text = string.Format("{0:N2}", Math.Round(a));
                txtNetAmt.Text = string.Format("{0:N2}", Math.Round(Localization.ParseNativeDouble(TxtGrossAmount.Text)) + a);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboAcType_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            try
            {
                if (!((base.blnFormAction == Enum_Define.ActionType.View_Record) | (base.blnFormAction == Enum_Define.ActionType.Not_Active)))
                {
                    DataGridViewEx ex = this.fgDtls_f;
                    int VATTypeId = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("select VATTypeId from tbl_LedgerMaster where LedgerId={0}", cboAcType.SelectedValue)));
                    if (VATTypeId != 0)
                    {
                        using (IDataReader reader = DB.GetRS(string.Format("select LedgerName, Percentage from tbl_LedgerMaster where VATTypeId={0} and LedgerGroupId=25", VATTypeId)))
                        {
                            if (reader.Read())
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = reader["LedgerName"].ToString();
                                ex.Rows[ex.CurrentRow.Index].Cells[6].Value = "+";
                                ex.Rows[ex.CurrentRow.Index].Cells[3].Value = Localization.ParseNativeDecimal(reader["Percentage"].ToString());
                                ex.Rows[ex.CurrentRow.Index].Cells[7].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(reader["Percentage"].ToString())), 100M);
                            }
                        }
                    }
                    else
                    {
                        ex.Rows[ex.CurrentRow.Index].Cells[5].Value = "";
                        ex.Rows[ex.CurrentRow.Index].Cells[6].Value = "+";
                        ex.Rows[ex.CurrentRow.Index].Cells[3].Value = 0.0;
                        ex.Rows[ex.CurrentRow.Index].Cells[7].Value = 0.0;
                    }
                    ex = null;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        //private void cboChallanNo_LostFocus(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if ((base.blnFormAction == Enum_Define.ActionType.New_Record) && (cboChallanNo.SelectedValue != null) && (cboChallanNo.SelectedValue.ToString() != "0"))
        //        {
        //            int rate = 0;
        //            if (cboChallanNo.SelectedValue != null && Localization.ParseNativeInt(cboChallanNo.SelectedValue.ToString()) != 0 && cboChallanNo.SelectedValue.ToString() != "-")
        //            {
        //                btnMultipleorders.Enabled = false;
        //                cboOrderNo.Enabled = false;
        //                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where FabricInwardID = {1} and CompID = {2} and YearID = {3} ", new object[] { "tbl_FabricInwardMain", cboChallanNo.SelectedValue, Db_Detials.CompID, Db_Detials.YearID })))
        //                {
        //                    while (reader.Read())
        //                    {
        //                        dtChallanDate.Text = Localization.ToVBDateString(reader["ChallanDate"].ToString());
        //                        if (reader["OrderDate"].ToString() != "")
        //                        {
        //                            dtOrderDate.Text = Localization.ToVBDateString(reader["OrderDate"].ToString());
        //                        }
        //                        cboSupplier.SelectedValue = Localization.ParseNativeInt(reader["SupplierID"].ToString());
        //                        cboBroker.SelectedValue = Localization.ParseNativeInt(reader["BrokerID"].ToString());
        //                        cboDeptTo.SelectedValue = Localization.ParseNativeInt(reader["DepartmentID"].ToString());
        //                        cboTransport.SelectedValue = Localization.ParseNativeInt(reader["TransportID"].ToString());
        //                        if (Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()) > 0.0)
        //                        {
        //                            string sqlQuery = string.Format("Select DISTINCT FabPOID, FabPONO  from {0}({1},{2},{3})", new object[] { "fn_FetchFabPurchaseOrderDtls", cboSupplier.SelectedValue, Db_Detials.CompID, Db_Detials.YearID });
        //                            Combobox_Setup.Fill_Combo(this.cboOrderNo, sqlQuery, "FabPONo", "FabPOID");
        //                            cboOrderNo.ColumnWidths = "0;100;0;0;0;0;0";
        //                            cboOrderNo.AutoComplete = true;
        //                            cboOrderNo.AutoDropdown = true;
        //                        }
        //                        this.cboOrderNo.SelectedValue = Localization.ParseNativeInt(reader["OrderID"].ToString());
        //                        if (this.cboOrderNo.SelectedItem != null)
        //                        {
        //                            DataRowView selectedItem = (DataRowView)cboOrderNo.SelectedItem;
        //                            //rate = Convert.ToInt32(Localization.ParseNativeDecimal(selectedItem.Row.ItemArray[7].ToString()));
        //                        }
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                btnMultipleorders.Enabled = true;
        //                cboOrderNo.Enabled = true;
        //            }

        //            if (cboChallanNo.SelectedValue != null && Localization.ParseNativeInt(cboChallanNo.SelectedValue.ToString()) != 0 && cboChallanNo.SelectedValue.ToString() != "-")
        //            {
        //                btnMultipleorders.Enabled = false;
        //                cboOrderNo.Enabled = false;
        //                using (IDataReader reader2 = DB.GetRS(string.Format("Select * from {0} Where FabricInwardID = {1} ", "tbl_FabricInwardDtls", cboChallanNo.SelectedValue)))
        //                {
        //                    fgDtls.Columns[0].Visible = false;
        //                    fgDtls.Rows.Clear();
        //                    while (reader2.Read())
        //                    {
        //                        fgDtls.Rows.Add();
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[1].Value = fgDtls.Rows.Count;
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[5].Value = reader2["PieceNo"].ToString();
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[3].Value = Localization.ParseNativeInt(reader2["FabricPoID"].ToString());
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[7].Value = Localization.ParseNativeInt(reader2["DesignID"].ToString());
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[8].Value = Localization.ParseNativeInt(reader2["QualityID"].ToString());
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[9].Value = Localization.ParseNativeInt(reader2["ShadeID"].ToString());
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[4].Value = reader2["LotNo"].ToString();
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[10].Value = Localization.ParseNativeInt(reader2["GradeID"].ToString());
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[11].Value = Localization.ParseNativeInt(reader2["UnitID"].ToString());
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[12].Value = Localization.ParseNativeInt(reader2["Cuts"].ToString());
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[13].Value = Localization.ParseNativeDecimal(reader2["Mtrs"].ToString());
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[14].Value = Localization.ParseNativeDecimal(reader2["NetWt"].ToString());
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[15].Value = Localization.ParseNativeDecimal(rate.ToString());
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[2].Value = Localization.ParseNativeInt(reader2["FabricInwardID"].ToString());
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                btnMultipleorders.Enabled = true;
        //                cboOrderNo.Enabled = true;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Navigate.logError(ex.Message, ex.StackTrace);
        //    }
        //}

        private void cboSupplier_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string sqlQuery = string.Empty;

                //if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                //{
                //    if (cboSupplier.SelectedValue != null && Localization.ParseNativeInt(cboSupplier.SelectedValue.ToString()) != 0 && cboSupplier.SelectedValue.ToString() != "-")
                //    {
                //        sqlQuery = string.Format("Select FabricInwardID, ChallanNo From {0} Where IsDeleted=0 AND OrderType='WITH ORDER' AND SupplierID = " + cboSupplier.SelectedValue + "  AND CompID = {1} And YearID = {2} and FabricInwardID not in (Select isnull(A.FabricInwardID,0) From {3} AS A LEFT JOIN tbl_FabricPurchaseMain AS B ON A.FabPurchaseID=B.FabPurchaseID where B.IsDeleted=0)", new object[] { "tbl_FabricInwardMain", Db_Detials.CompID, Db_Detials.YearID, "tbl_FabricPurchaseDtls" });
                //        Combobox_Setup.Fill_Combo(this.cboChallanNo, sqlQuery, "ChallanNo", "FabricInwardID");
                //    }
                //}

                //if (cboOrderType.SelectedItem.ToString() == "WITHOUT ORDER")
                //{
                //    if (cboSupplier.SelectedValue != null && Localization.ParseNativeInt(cboSupplier.SelectedValue.ToString()) != 0 && cboSupplier.SelectedValue.ToString() != "-")
                //    {
                //        sqlQuery = string.Format("Select FabricInwardID, ChallanNo From {0} Where IsDeleted=0 AND OrderType='WITHOUT ORDER' AND SupplierID = " + cboSupplier.SelectedValue + "  AND CompID = {1} And YearID = {2} and FabricInwardID not in (Select isnull(A.FabricInwardID,0) From {3} AS A LEFT JOIN tbl_FabricPurchaseMain AS B ON A.FabPurchaseID=B.FabPurchaseID where B.IsDeleted=0)", new object[] { "tbl_FabricInwardMain", Db_Detials.CompID, Db_Detials.YearID, "tbl_FabricPurchaseDtls" });
                //        Combobox_Setup.Fill_Combo(this.cboChallanNo, sqlQuery, "ChallanNo", "FabricInwardID");
                //    }
                //}

                if (((cboSupplier.SelectedValue != null) && (Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString())) > 0.0))
                {
                    cboBroker.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select BrokerID From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboSupplier.SelectedValue)));
                    cboTransport.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select TransportId From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboSupplier.SelectedValue)));
                    cboAcType.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select PurchSalesID From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboSupplier.SelectedValue)));
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void dtBillDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record)) && (dtBillDate.Text != "__/__/____"))
                {
                    dtDueDate.Text = dtBillDate.Text;
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
                isOrderused = false;
                for (int l = 0; l <= fgDtls.Rows.Count - 1; l++)
                {
                    if (fgDtls.Rows[l].Cells[3].Value != null && fgDtls.Rows[l].Cells[3].Value.ToString() != "" && fgDtls.Rows[l].Cells[3].Value.ToString() != "-" && fgDtls.Rows[l].Cells[3].Value.ToString() != "0")
                    {
                        isOrderused = true;
                        break;
                    }
                }
                if (Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString()) > 0 || isOrderused == true)
                {
                    EnabDisab(false);
                }
                else
                {
                    EnabDisab(true);
                }
                //if (this.cboChallanNo.SelectedValue == null)
                {
                    if (!Vld_DupPieceNo)
                    {
                        if (e.ColumnIndex == 2)
                        {
                            string strTblName;
                            if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                            {
                                string primaryFieldNameValue = fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString();
                                if (fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString().Length > 0)
                                {
                                    if (fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString() != "-")
                                    {
                                        strTblName = "tbl_StockFabricLedger";
                                        if (Navigate.CheckDuplicate(ref strTblName, "BarCodeNo", primaryFieldNameValue, false, "", 0L, " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " and BranchID = " + Db_Detials.BranchID + " and StoreID=" + Db_Detials.StoreID, ""))
                                        {
                                            fgDtls.CurrentCell = fgDtls[2, e.RowIndex];
                                        }
                                    }
                                }
                                else if (fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString().Length <= 0)
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[5].Value = "-";
                                }
                            }
                            else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString().Length > 0)
                                {
                                    if (fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString() != "-")
                                    {
                                        strTblName = "tbl_StockFabricLedger";
                                        if (Navigate.CheckDuplicate(ref strTblName, "BarCodeNo", fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString(), true, "TransID", Localization.ParseNativeLong(txtCode.Text.Trim()), " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " and BranchID = " + Db_Detials.BranchID + " and StoreID=" + Db_Detials.StoreID, ""))
                                        {
                                            fgDtls.CurrentCell = fgDtls[2, e.RowIndex];
                                        }
                                    }
                                }
                                else if (fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString().Length <= 0)
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[5].Value = "-";
                                }
                            }
                        }
                    }
                    if (Vld_DupPieceNo)
                    {
                        if (e.ColumnIndex == 2)
                        {
                            string strTbleName;
                            if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                            {
                                string primaryFieldNameValue = fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString();
                                if (fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString().Length > 0)
                                {
                                    //if (fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString() != "-")
                                    {
                                        strTbleName = "tbl_StockFabricLedger";
                                        if (Navigate.CheckDuplicate(ref strTbleName, "BarCodeNo", primaryFieldNameValue, false, "", 0L, " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " and BranchID = " + Db_Detials.BranchID + " and StoreID=" + Db_Detials.StoreID, ""))
                                        {
                                            fgDtls.CurrentCell = fgDtls[2, e.RowIndex];
                                            //fgDtls.Rows[e.RowIndex].Cells[5].Value = "-";
                                        }
                                    }
                                }
                                else if (fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString().Length <= 0)
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[5].Value = "-";
                                }
                            }
                            else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString().Length > 0)
                                {
                                    //if (fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString() != "-")
                                    {
                                        strTbleName = "tbl_StockFabricLedger";
                                        if (Navigate.CheckDuplicate(ref strTbleName, "BarCodeNo", fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString(), true, "TransID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " and BranchID = " + Db_Detials.BranchID + " and StoreID=" + Db_Detials.StoreID, ""))
                                        {
                                            //fgDtls.Rows[e.RowIndex].Cells[5].Value = "-";
                                            fgDtls.CurrentCell = fgDtls[2, e.RowIndex];
                                        }
                                    }
                                }
                                else if (fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString().Length <= 0)
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[5].Value = "-";
                                }
                            }
                        }
                    }

                    if (e.ColumnIndex == 7 || e.ColumnIndex == 9 && fgDtls.Rows[e.RowIndex].Cells[7].Value != null && fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString().Length > 0) 
                    {
                        fgDtls.Rows[e.RowIndex].Cells[8].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", fgDtls.Rows[e.RowIndex].Cells[7].Value)));
                    }
                }

                if (FO_BOOKDESIGN)
                {
                    if (e.ColumnIndex == 3)
                    {
                        if (fgDtls.Rows[e.RowIndex].Cells[6].Value != null)
                        {
                            using (IDataReader dr = DB.GetRS("Select FabricDesignID,FabricQualityID,FabricShadeID from fn_FabricMaster_Tbl() where FabricID=" + fgDtls.Rows[e.RowIndex].Cells[6].Value + ""))
                            {
                                while (dr.Read())
                                {
                                    //fgDtls.Rows.Add();
                                    fgDtls.Rows[e.RowIndex].Cells[7].Value = Localization.ParseNativeInt(dr["FabricDesignID"].ToString());
                                    fgDtls.Rows[e.RowIndex].Cells[8].Value = Localization.ParseNativeInt(dr["FabricQualityID"].ToString());
                                    fgDtls.Rows[e.RowIndex].Cells[9].Value = Localization.ParseNativeInt(dr["FabricShadeID"].ToString());
                                }
                            }
                        }
                    }
                }
                if (e.ColumnIndex == 9 || e.ColumnIndex == 8 || e.ColumnIndex == 7)
                {
                    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    {
                        if (fgDtls.Rows[i].Cells[7].Value != null && fgDtls.Rows[i].Cells[7].Value.ToString() != "" && fgDtls.Rows[i].Cells[8].Value != null && fgDtls.Rows[i].Cells[8].Value.ToString() != "" && fgDtls.Rows[i].Cells[9].Value != null && fgDtls.Rows[i].Cells[9].Value.ToString() != "")
                        {
                            fgDtls.Rows[i].Cells[6].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricID from fn_FabricMaster_Tbl() where FabricDesignID={0} and FabricQualityID={1} and FabricShadeID={2}", fgDtls.Rows[i].Cells[7].Value, fgDtls.Rows[i].Cells[8].Value, fgDtls.Rows[i].Cells[9].Value)));
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
                SRateCalcType = "";
                isOrderused = false;
                if (fgDtls.Rows[e.RowIndex].Cells[11].Value != null && fgDtls.Rows[e.RowIndex].Cells[11].Value.ToString() != "" && fgDtls.Rows[e.RowIndex].Cells[11].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[11].Value.ToString() != "-")
                {
                    SRateCalcType = DB.GetSnglValue("Select RateCalcType from tbl_UnitsMaster Where UnitID=" + fgDtls.Rows[e.RowIndex].Cells[11].Value.ToString() + " and IsDeleted=0");
                }
                for (int l = 0; l <= fgDtls.Rows.Count - 1; l++)
                {
                    if (fgDtls.Rows[l].Cells[3].Value != null && fgDtls.Rows[l].Cells[3].Value.ToString() != "" && fgDtls.Rows[l].Cells[3].Value.ToString() != "-" && fgDtls.Rows[l].Cells[3].Value.ToString() != "0")
                    {
                        isOrderused = true;
                        break;
                    }
                }
                if (Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString()) > 0 || isOrderused == true)
                {
                    EnabDisab(false);
                }
                else
                {
                    EnabDisab(true);
                }

                try
                {
                    if ((e.ColumnIndex == 14) | (e.ColumnIndex == 12) | (e.ColumnIndex == 13))
                    {
                        ExecuterTempQry(e.RowIndex);
                    }
                }
                catch (Exception ex1)
                {
                    Navigate.logError(ex1.Message, ex1.StackTrace);
                }
                if (e.ColumnIndex == 8 || e.ColumnIndex == 9 || e.ColumnIndex == 7)
                {
                    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    {
                        if (fgDtls.Rows[i].Cells[7].Value != null && fgDtls.Rows[i].Cells[7].Value.ToString() != "" && fgDtls.Rows[i].Cells[8].Value != null && fgDtls.Rows[i].Cells[8].Value.ToString() != "" && fgDtls.Rows[i].Cells[9].Value != null && fgDtls.Rows[i].Cells[9].Value.ToString() != "")
                        {
                            fgDtls.Rows[i].Cells[6].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricID from fn_FabricMaster_Tbl() where FabricDesignID={0} and FabricQualityID={1} and FabricShadeID={2}", fgDtls.Rows[i].Cells[7].Value, fgDtls.Rows[i].Cells[8].Value, fgDtls.Rows[i].Cells[9].Value)));
                        }
                    }
                }

                DataGridViewEx ex = this.fgDtls_f;
                if (!((base.blnFormAction == Enum_Define.ActionType.View_Record) | (base.blnFormAction == Enum_Define.ActionType.Not_Active)))
                {
                    try
                    {
                        if (((e.ColumnIndex == 12) | (e.ColumnIndex == 14)) | (e.ColumnIndex == 15))
                        {
                            CalcVal();
                        }

                        if (fgDtls.RowCount > 1)
                        {
                            cboOrderType.Enabled = false;
                        }
                        else
                        {
                            cboOrderType.Enabled = true;
                        }
                    }
                    catch { }
                    switch (e.ColumnIndex)
                    {
                        case 8:
                            if (ENABLE_BROKER_CALCMETHOD2)
                            {
                                if (ENABLE_BROKER_FAB_PURCHASE)
                                {
                                    if (cboBroker.SelectedValue != null && cboBroker.SelectedValue.ToString() != "" && cboBroker.SelectedValue.ToString() != "0")
                                    {
                                        SBrokersPerc = DB.GetSnglValue(string.Format("SELECT percentage from tbl_BrokerPercentDtls a left join tbl_BrokerPercentMain B on A.BrokerPercentID=b.BrokerPercentID where b.BrokerID={0} and a.QualityID={1}", cboBroker.SelectedValue, fgDtls.Rows[e.RowIndex].Cells[8].Value));
                                        fgDtls.Rows[e.RowIndex].Cells[20].Value = SBrokersPerc;

                                        if (fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value.ToString() != "0" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value.ToString() != "0")
                                        {
                                            decimal dbrokertotalamt_gridrow = (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[2].Value.ToString()) / 100) * (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[15].Value.ToString()));
                                            fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[21].Value = dbrokertotalamt_gridrow;
                                        }
                                        else
                                        {
                                            fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[21].Value = 0;
                                        }
                                    }
                                }
                            }
                            CalcVal();
                            break;

                        case 11:
                            goto case 13;

                        case 12:
                            goto case 13;

                        case 14:
                            goto case 13;

                        case 13:
                            if (SRateCalcType == "P")
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[12].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[12].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != "0")
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[16].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[12].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString())).ToString()));
                                    for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                                    {
                                        if (ex.Rows[i].Cells[4].Value != null && ex.Rows[i].Cells[4].Value.ToString() != "0")
                                        {
                                            ex.Rows[i].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[i].Cells[4].Value))), 100M).ToString().Replace(",", "");
                                            CalcVal();
                                        }
                                    }
                                }
                            }
                            else if ((SRateCalcType == "M"))
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != "0")
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[16].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString())).ToString()));
                                    for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                                    {
                                        if (ex.Rows[i].Cells[4].Value != null && ex.Rows[i].Cells[4].Value.ToString() != "0")
                                        {
                                            ex.Rows[i].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[i].Cells[4].Value))), 100M).ToString().Replace(",", "");
                                            CalcVal();
                                        }
                                    }
                                }
                            }
                            else if ((SRateCalcType == "W"))
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[14].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[14].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != "0")
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[16].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[14].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString())).ToString()));
                                    for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                                    {
                                        if (ex.Rows[i].Cells[4].Value != null && ex.Rows[i].Cells[4].Value.ToString() != "0")
                                        {
                                            ex.Rows[i].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[i].Cells[4].Value))), 100M).ToString().Replace(",", "");
                                            CalcVal();
                                        }
                                    }
                                }
                            }
                            CalcVal();
                            break;

                        case 15:
                            if (SRateCalcType == "P")
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[12].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[12].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != "0")
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[16].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[12].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString())).ToString()));
                                    for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                                    {
                                        if (ex.Rows[i].Cells[4].Value != null && ex.Rows[i].Cells[4].Value.ToString() != "0")
                                        {
                                            ex.Rows[i].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[i].Cells[4].Value))), 100M).ToString().Replace(",", "");
                                            CalcVal();
                                        }
                                    }
                                }
                            }
                            else if ((SRateCalcType == "M"))
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != "0")
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[16].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString())).ToString()));
                                    for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                                    {
                                        if (ex.Rows[i].Cells[4].Value != null && ex.Rows[i].Cells[4].Value.ToString() != "0")
                                        {
                                            ex.Rows[i].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[i].Cells[4].Value))), 100M).ToString().Replace(",", "");
                                            CalcVal();
                                        }
                                    }
                                }
                            }
                            else if ((SRateCalcType == "W"))
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[14].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[14].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != "0")
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[16].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[14].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString())).ToString()));
                                    for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                                    {
                                        if (ex.Rows[i].Cells[4].Value != null && ex.Rows[i].Cells[4].Value.ToString() != "0")
                                        {
                                            ex.Rows[i].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[i].Cells[4].Value))), 100M).ToString().Replace(",", "");
                                            CalcVal();
                                        }
                                    }
                                }
                            }
                            if (ENABLE_BROKER_CALCMETHOD2)
                            {
                                if (ENABLE_BROKER_FAB_PURCHASE && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value.ToString() != "0" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value.ToString() != "0")
                                {
                                    decimal dbrokertotalamt_gridrow = (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value.ToString()) / 100) * (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value.ToString()));
                                    fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[21].Value = dbrokertotalamt_gridrow;
                                }
                                else
                                {
                                    fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[21].Value = 0;
                                }
                            }
                            CalcVal();
                            break;

                        case 16:
                            if (SRateCalcType == "P")
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[12].Value != null && fgDtls.Rows[e.RowIndex].Cells[12].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[16].Value != null && fgDtls.Rows[e.RowIndex].Cells[16].Value.ToString() != "0")
                                {
                                    if (Localization.ParseNativeDouble(Operators.DivideObject(fgDtls.Rows[e.RowIndex].Cells[16].Value, fgDtls.Rows[e.RowIndex].Cells[12].Value).ToString()) != Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString()))
                                    {
                                        fgDtls.Rows[e.RowIndex].Cells[15].Value = (Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[16].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[12].Value.ToString()));
                                        for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                                        {
                                            if (ex.Rows[i].Cells[4].Value != null && ex.Rows[i].Cells[4].Value.ToString() != "0")
                                            {
                                                ex.Rows[i].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[i].Cells[4].Value))), 100M).ToString().Replace(",", "");
                                                CalcVal();
                                            }
                                        }
                                    }
                                }
                            }
                            else if ((SRateCalcType == "M"))
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[13].Value != null && fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[16].Value != null && fgDtls.Rows[e.RowIndex].Cells[16].Value.ToString() != "0")
                                {
                                    if (Localization.ParseNativeDouble(Operators.DivideObject(fgDtls.Rows[e.RowIndex].Cells[16].Value, fgDtls.Rows[e.RowIndex].Cells[13].Value).ToString()) != Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString()))
                                    {
                                        fgDtls.Rows[e.RowIndex].Cells[15].Value = (Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[16].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString()));
                                        for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                                        {
                                            if (ex.Rows[i].Cells[4].Value != null && ex.Rows[i].Cells[4].Value.ToString() != "0")
                                            {
                                                ex.Rows[i].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[i].Cells[4].Value))), 100M).ToString().Replace(",", "");
                                                CalcVal();
                                            }
                                        }
                                    }
                                }
                            }
                            else if ((SRateCalcType == "W"))
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[14].Value != null && fgDtls.Rows[e.RowIndex].Cells[14].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[16].Value != null && fgDtls.Rows[e.RowIndex].Cells[16].Value.ToString() != "0")
                                {
                                    if (Localization.ParseNativeDouble(Operators.DivideObject(fgDtls.Rows[e.RowIndex].Cells[16].Value, fgDtls.Rows[e.RowIndex].Cells[14].Value).ToString()) != Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString()))
                                    {
                                        fgDtls.Rows[e.RowIndex].Cells[15].Value = (Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[16].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[14].Value.ToString()));
                                        for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                                        {
                                            if (ex.Rows[i].Cells[4].Value != null && ex.Rows[i].Cells[4].Value.ToString() != "0")
                                            {
                                                ex.Rows[i].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[i].Cells[4].Value))), 100M).ToString().Replace(",", "");
                                                CalcVal();
                                            }
                                        }
                                    }
                                }
                            }
                            CalcVal();
                            break;

                        case 20:
                            if (ENABLE_BROKER_CALCMETHOD2)
                            {
                                if (ENABLE_BROKER_FAB_PURCHASE && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value.ToString() != "0" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value.ToString() != "0")
                                {
                                    decimal dbrokertotalamt_gridrow = (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value.ToString()) / 100) * (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value.ToString()));
                                    fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[21].Value = dbrokertotalamt_gridrow;
                                }
                                else
                                {
                                    fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[21].Value = 0;
                                }
                            }
                            CalcVal();
                            break;
                    }
                    if (((e.ColumnIndex == 5) | (e.ColumnIndex == 7)) && ((fgDtls.Rows[e.RowIndex].Cells[7].Value != null) && (Strings.Trim(Conversions.ToString(fgDtls.Rows[e.RowIndex].Cells[7].Value)).Length > 0)))
                    {
                        fgDtls.Rows[e.RowIndex].Cells[8].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", fgDtls.Rows[e.RowIndex].Cells[7].Value)));
                    }
                    if (e.ColumnIndex == 8 || e.ColumnIndex == 9 || e.ColumnIndex == 7)
                    {
                        for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                        {
                            if (fgDtls.Rows[i].Cells[7].Value != null && fgDtls.Rows[i].Cells[7].Value.ToString() != "" && fgDtls.Rows[i].Cells[8].Value != null && fgDtls.Rows[i].Cells[8].Value.ToString() != "" && fgDtls.Rows[i].Cells[9].Value != null && fgDtls.Rows[i].Cells[9].Value.ToString() != "")
                            {
                                fgDtls.Rows[i].Cells[6].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricID from fn_FabricMaster_Tbl() where FabricDesignID={0} and FabricQualityID={1} and FabricShadeID={2}", fgDtls.Rows[i].Cells[7].Value, fgDtls.Rows[i].Cells[8].Value, fgDtls.Rows[i].Cells[9].Value)));
                            }
                        }
                    }
                    CalcVal();
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
                if (((base.blnFormAction == Enum_Define.ActionType.New_Record)))
                {
                    DataGridViewEx ex = this.fgDtls_f;
                    bool CalcDednWithNetAmt = Localization.ParseBoolean(DB.GetSnglValue(string.Format("select CalcDednWithNetAmt from {0} where LedgerID={1}", "tbl_Ledgermaster", ex.Rows[ex.CurrentRow.Index].Cells[2].Value)));
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
                                        if (CalcDednWithNetAmt.ToString() == "True")
                                        {
                                            ex.Rows[ex.CurrentRow.Index].Cells[5].Value = ((Localization.ParseNativeDecimal(txtNetAmt.Text) * Localization.ParseNativeDecimal(reader["Percentage"].ToString())) / 100);
                                        }
                                        else
                                        {
                                            ex.Rows[ex.CurrentRow.Index].Cells[5].Value = ((Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(reader["Percentage"].ToString())) / 100);
                                        }
                                    }
                                }
                            }
                            CalcVal();
                            break;

                        case 3:
                            if (CalcDednWithNetAmt.ToString() == "True")
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(txtNetAmt.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                                ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(txtNetAmt.Text)) * 100);
                            }
                            else
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                                ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(TxtGrossAmount.Text)) * 100);
                            }
                            CalcVal();
                            break;

                        case 4:

                            if (CalcDednWithNetAmt.ToString() == "True")
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(txtNetAmt.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                            }
                            else
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                            }
                            break;

                        case 5:

                            if (CalcDednWithNetAmt.ToString() == "True")
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(txtNetAmt.Text)) * 100);
                            }
                            else
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(TxtGrossAmount.Text)) * 100);
                            }
                            CalcVal();
                            break;
                    }
                    CalcVal();
                    ex = null;
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }

            try
            {
                if (((base.blnFormAction == Enum_Define.ActionType.Edit_Record)))
                {
                    DataGridViewEx ex = this.fgDtls_f;
                    bool CalcDednWithNetAmt = Localization.ParseBoolean(DB.GetSnglValue(string.Format("select CalcDednWithNetAmt from {0} where LedgerID={1}", "tbl_Ledgermaster", ex.Rows[ex.CurrentRow.Index].Cells[2].Value)));
                    switch (e.ColumnIndex)
                    {
                        case 3:
                            if (CalcDednWithNetAmt.ToString() == "True")
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(txtNetAmt.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                                ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(txtNetAmt.Text)) * 100);
                            }
                            else
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                                ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(TxtGrossAmount.Text)) * 100);
                            }
                            CalcVal();
                            break;

                        case 4:
                            if (CalcDednWithNetAmt.ToString() == "True")
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(txtNetAmt.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                            }
                            else
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                            }
                            CalcVal();
                            break;

                        case 5:
                            if (ex.Rows[ex.CurrentRow.Index].Cells[5].Value != null && ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString() != "0.00")
                            {
                                if (CalcDednWithNetAmt.ToString() == "True")
                                {
                                    ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(txtNetAmt.Text)) * 100);
                                }
                                else
                                {
                                    ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(TxtGrossAmount.Text)) * 100);
                                }
                            }
                            CalcVal();
                            break;
                    }
                    CalcVal();
                    ex = null;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    fgDtls.Rows[e.RowIndex].Cells[15].Value = "0";
                    fgDtls.Rows[e.RowIndex].Cells[14].Value = "0";
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
            //if (cboChallanNo.SelectedValue == null || cboChallanNo.SelectedValue.ToString() == "-" || cboChallanNo.SelectedValue.ToString() == "0")
            {
                if (fgDtls.Rows.Count > 1)
                {
                    if (fgDtls.Rows[e.RowIndex - 1].Cells[5].Value != null && fgDtls.Rows[e.RowIndex - 1].Cells[5].Value.ToString().Trim() != "-")
                    {
                        fgDtls.Rows[e.RowIndex].Cells[5].Value = CommonCls.AutoInc_Runtime(fgDtls.Rows[e.RowIndex - 1].Cells[5].Value.ToString(), Db_Detials.PCS_NO_INCMT);
                    }
                    else
                    {
                        fgDtls.Rows[e.RowIndex].Cells[5].Value = "-";
                    }
                }
            }
        }

        //public virtual DataGridViewEx fgDtls
        //{
        //    [DebuggerNonUserCode]
        //    get
        //    {
        //        return this._fgDtls;
        //    }
        //    [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
        //    set
        //    {
        //        DataGridViewRowsAddedEventHandler handler = new DataGridViewRowsAddedEventHandler(this.fgDtls_RowsAdded);
        //        DataGridViewCellEventHandler handler2 = new DataGridViewCellEventHandler(this.fgDtls_CellEndEdit);
        //        DataGridViewCellEventHandler handler3 = new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
        //        KeyEventHandler handler4 = new KeyEventHandler(this.fgDtls_KeyDown);
        //        if (this._fgDtls != null)
        //        {
        //            this._fgDtls.RowsAdded -= handler;
        //            this._fgDtls.CellEndEdit -= handler2;
        //            this._fgDtls.CellValueChanged -= handler3;
        //            this._fgDtls.KeyDown -= handler4;
        //        }
        //        this._fgDtls = value;
        //        if (this._fgDtls != null)
        //        {
        //            this._fgDtls.RowsAdded += handler;
        //            this._fgDtls.CellEndEdit += handler2;
        //            this._fgDtls.CellValueChanged += handler3;
        //            this._fgDtls.KeyDown += handler4;
        //        }
        //    }
        //}

        //public virtual DataGridViewEx fgDtls_f
        //{
        //    [DebuggerNonUserCode]
        //    get
        //    {
        //        return this._fgDtls_f;
        //    }
        //    [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
        //    set
        //    {
        //        DataGridViewCellEventHandler handler = new DataGridViewCellEventHandler(this.fgDtls_f_CellValueChanged);
        //        if (this._fgDtls_f != null)
        //        {
        //            this._fgDtls_f.CellValueChanged -= handler;
        //        }
        //        this._fgDtls_f = value;
        //        if (this._fgDtls_f != null)
        //        {
        //            this._fgDtls_f.CellValueChanged += handler;
        //        }
        //    }
        //}

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                string sSupID = string.Empty;
                if (cboSupplier.SelectedValue == null || cboSupplier.SelectedValue.ToString() == "-" || cboSupplier.SelectedValue.ToString() == "" || cboSupplier.SelectedValue.ToString() == "0")
                {
                    sSupID = "0";
                }
                else
                {
                    sSupID = cboSupplier.SelectedValue.ToString();
                }

                #region StockAdjQuery
                string strQry = string.Empty;
                int ibitcol = 0;
                string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", iIDentity, 0.0));
                string strQry_ColName = "";
                string[] arr = CommonCls.GetAdjColName(base.iIDentity, 0.0).Split(';');
                strQry_ColName = arr[0].ToString();
                ibitcol = Localization.ParseNativeInt(arr[1]);
                strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5} , {6}, {7}) ", new object[] { strQry_ColName, snglValue, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(dtBillDate.Text))), sSupID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID });
                #endregion

                frmStockAdj frmStockAdj = new frmStockAdj();
                frmStockAdj.MenuID = base.iIDentity;
                frmStockAdj.Entity_IsfFtr = 0.0;
                frmStockAdj.ref_fgDtls = this.fgDtls;
                frmStockAdj.LedgerID = sSupID;
                frmStockAdj.QueryString = strQry;
                frmStockAdj.IsRefQuery = true;
                frmStockAdj.ibitCol = ibitcol;
                if (this.OrgInGridArray != null)
                {
                    frmStockAdj.UsedInGridArray = this.OrgInGridArray;
                }
                if (frmStockAdj.ShowDialog() == DialogResult.Cancel)
                {
                    frmStockAdj.Dispose();
                    return;
                }
                frmStockAdj.Dispose();
                frmStockAdj = null;
                this.fgDtls.Select();
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

        public string setBroker
        {
            get
            {
                return cboBroker.SelectedValue.ToString();
            }
            set
            {
                if (value.Length != 0)
                {
                    cboBroker.SelectedValue = value;
                }
            }
        }

        public string setTransport
        {
            get
            {
                return cboTransport.SelectedValue.ToString();
            }
            set
            {
                if (value.Length != 0)
                {
                    cboTransport.SelectedValue = value;
                }
            }
        }

        public string setLrNo
        {
            get
            {
                return txtLrNo.Text.ToString();
            }
            set
            {
                if (value.Length != 0)
                {
                    txtLrNo.Text = value;
                }
            }
        }

        public string setLrDate
        {
            get
            {
                return dtLrDate.Text;
            }
            set
            {
                if (value.Length != 0)
                {
                    dtLrDate.Text = value;
                }
            }
        }

        public void ShowStock()
        {
            try
            {
                bool isIndexAppld = false;
                int iIndex = fgDtls.RowCount - 1;
                for (int m = 0; m <= fgDtls.RowCount - 1; m++)
                {
                    if (fgDtls.Rows[m].Cells[7].Value != null && fgDtls.Rows[m].Cells[7].Value.ToString() != "")
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

                if (!Information.IsDate(dtBillDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Bill Date");
                    dtBillDate.Focus();
                    return;
                }

                #region StockAdjQuery
                string strQry = string.Empty;
                int ibitcol = 0;
                string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", iIDentity, 1.0));
                string strQry_ColName = "";
                string[] arr = CommonCls.GetAdjColName(base.iIDentity, 1.0).Split(';');
                strQry_ColName = arr[0].ToString();
                ibitcol = Localization.ParseNativeInt(arr[1]);
                strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}, {6}, {7}) Where OrderTransType ='Purchase' and  RefVoucherID in ({8}) Order by MyId", new object[] { strQry_ColName, snglValue, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(dtBillDate.Text))), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, sSupID, RefVoucherID });
                #endregion

                frmStockAdj frmStockAdj = new frmStockAdj();
                frmStockAdj.MenuID = base.iIDentity;
                frmStockAdj.Entity_IsfFtr = 1;
                frmStockAdj.ref_fgDtls = this.fgDtls;
                frmStockAdj.QueryString = strQry;
                frmStockAdj.IsRefQuery = true;
                frmStockAdj.ibitCol = ibitcol;
                frmStockAdj.LedgerID = sSupID;

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
                            if ((fgDtls.Rows[i].Cells[12].Value != null) && (fgDtls.Rows[i].Cells[12].Value.ToString() != "0") && (fgDtls.Rows[i].Cells[12].Value.ToString() != ""))
                            {
                                double iPcs = Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[12].Value.ToString());

                                if (Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[40].Value.ToString()) < iPcs)
                                {
                                    iPcs = Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[40].Value.ToString());
                                }

                                if (iPcs > 0)
                                {
                                    int num8 = (int)Math.Round((double)(iPcs + i));
                                    for (int k = i + 1; k <= num8; k++)
                                    {
                                        fgDtls.Rows.Insert(k, new DataGridViewRow());
                                        for (int m = 0; m <= fgDtls.ColumnCount - 1; m++)
                                        {
                                            if (m == 12)
                                            {
                                                fgDtls.Rows[k].Cells[m].Value = 1;
                                            }
                                            else if (m == 1)
                                            {
                                                fgDtls.Rows[k].Cells[m].Value = k;
                                            }
                                            else if (m != 5 && m != 13)
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
                                fgDtls.Rows[i].Cells[12].Value = fgDtls.Rows[i].Cells[40].Value.ToString();
                            }
                        }
                    }
                    fgDtls.Rows.RemoveAt(fgDtls.RowCount - 1);

                    DataGridViewEx ex2 = fgDtls;
                    for (int j = 0; j <= ex2.RowCount - 1; j++)
                    {
                        if (j == 0)
                        {
                            if (fgDtls.Rows.Count > 0)
                            {
                                if (fgDtls.Columns[2].Visible)
                                {
                                    fgDtls.Rows[0].Cells[5].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select  {0}({1},{2})", new object[] { "dbo.fn_FetchPieceNo_Stock", Db_Detials.CompID, Db_Detials.YearID })), Db_Detials.PCS_NO_INCMT);
                                }
                                else
                                {
                                    fgDtls.Rows[0].Cells[5].Value = "-";
                                }
                            }
                        }
                        else if (j > 0)
                        {
                            if (ex2.Rows[j - 1].Cells[5].Value.ToString() != "-")
                            {
                                ex2.Rows[j].Cells[5].Value = CommonCls.AutoInc_Runtime(ex2.Rows[j - 1].Cells[5].Value.ToString(), Db_Detials.PCS_NO_INCMT);
                            }
                            else
                            {
                                ex2.Rows[j].Cells[5].Value = "-";
                            }
                        }
                    }

                    CalcVal();
                    SendKeys.Send("{TAB}");
                    if (fgDtls.Rows.Count > 0)
                    {
                        fgDtls.CurrentCell = fgDtls[11, fgDtls.RowCount - 1];
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

        private void btnShowOrder_Click(object sender, EventArgs e)
        {
           
        }

        private void cboOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string sqlQuery = string.Empty;
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                    {
                        btnMultipleorders.Enabled = true;
                        fgDtls.Columns[3].Visible = true;
                        EnabDisab(false);
                    }
                    else
                    {
                        btnMultipleorders.Enabled = false;
                        fgDtls.Columns[3].Visible = false;
                        EnabDisab(true);
                    }

                //if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                //{
                //    if (cboSupplier.SelectedValue != null && Localization.ParseNativeInt(cboSupplier.SelectedValue.ToString()) != 0 && cboSupplier.SelectedValue.ToString() != "-")
                //    {
                //        sqlQuery = string.Format("Select FabricInwardID, ChallanNo From {0} Where IsDeleted=0 AND OrderType='WITH ORDER' AND SupplierID = " + cboSupplier.SelectedValue + "  AND CompID = {1} And YearID = {2} and FabricInwardID not in (Select isnull(A.FabricInwardID,0) From {3} AS A LEFT JOIN tbl_FabricPurchaseMain AS B ON A.FabPurchaseID=B.FabPurchaseID where B.IsDeleted=0)", new object[] { "tbl_FabricInwardMain", Db_Detials.CompID, Db_Detials.YearID, "tbl_FabricPurchaseDtls" });
                //        Combobox_Setup.Fill_Combo(this.cboChallanNo, sqlQuery, "ChallanNo", "FabricInwardID");
                //        btnMultipleorders.Enabled = true;
                //        cboOrderNo.Enabled = true;
                //    }
                //}

                //if (cboOrderType.SelectedItem.ToString() == "WITHOUT ORDER")
                //{
                //    if (cboSupplier.SelectedValue != null && Localization.ParseNativeInt(cboSupplier.SelectedValue.ToString()) != 0 && cboSupplier.SelectedValue.ToString() != "-")
                //    {
                //        sqlQuery = string.Format("Select FabricInwardID, ChallanNo From {0} Where IsDeleted=0 AND OrderType='WITHOUT ORDER' AND SupplierID = " + cboSupplier.SelectedValue + "  AND CompID = {1} And YearID = {2} and FabricInwardID not in (Select isnull(A.FabricInwardID,0) From {3} AS A LEFT JOIN tbl_FabricPurchaseMain AS B ON A.FabPurchaseID=B.FabPurchaseID where B.IsDeleted=0)", new object[] { "tbl_FabricInwardMain", Db_Detials.CompID, Db_Detials.YearID, "tbl_FabricPurchaseDtls" });
                //        Combobox_Setup.Fill_Combo(this.cboChallanNo, sqlQuery, "ChallanNo", "FabricInwardID");
                //        btnMultipleorders.Enabled = false;
                //        cboOrderNo.Enabled = false;
                //    }
                //}
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #region All Old Commented
        //private void btnSelectOrder_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (IsOrderSelected == true && fgDtls.Rows.Count > 1)
        //        {
        //            fgDtls.Rows.Clear();
        //            EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
        //        }
        //        int iSrNo = (fgDtls.Rows.Count - 1);

        //        for (int i = 0; i <= (fgdtls_f_f.Rows.Count - 1); i++)
        //        {
        //            bool blnChecked = Localization.ParseBoolean(fgdtls_f_f.Rows[i].Cells[0].Value.ToString());
        //            if (blnChecked)
        //            {
        //                try
        //                {
        //                    if (Localization.ParseNativeDouble(fgdtls_f_f.Rows[i].Cells[2].Value.ToString()) > 0)
        //                    {
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[1].Value = iSrNo + 1;
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[5].Value = "-";
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[3].Value = Localization.ParseNativeInt(fgdtls_f_f.Rows[i].Cells[1].Value.ToString());
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[7].Value = Localization.ParseNativeInt(fgdtls_f_f.Rows[i].Cells[3].Value.ToString());
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[8].Value = Localization.ParseNativeInt(fgdtls_f_f.Rows[i].Cells[8].Value.ToString());
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[9].Value = Localization.ParseNativeInt(fgdtls_f_f.Rows[i].Cells[4].Value.ToString());

        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[4].Value = "-";
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[10].Value = "0";
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[11].Value = _MtrsID;
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[12].Value = fgdtls_f_f.Rows[i].Cells[2].Value.ToString();
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[13].Value = fgdtls_f_f.Rows[i].Cells[20].Value.ToString();
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[14].Value = "0";
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[15].Value = fgdtls_f_f.Rows[i].Cells[21].Value.ToString();
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[16].Value = Localization.ParseNativeDouble(fgdtls_f_f.Rows[i].Cells[20].Value.ToString()) * Localization.ParseNativeDouble(fgdtls_f_f.Rows[i].Cells[21].Value.ToString());
        //                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[2].Value = "0";

        //                        ////fgdtls_f_f.Rows[i].Cells[15].Value = Localization.ParseNativeDouble(fgdtls_f_f.Rows[i].Cells[15].Value.ToString()) + Localization.ParseNativeDouble(fgdtls_f_f.Rows[i].Cells[2].Value.ToString());
        //                        ////fgdtls_f_f.Rows[i].Cells[16].Value = Localization.ParseNativeDouble(fgdtls_f_f.Rows[i].Cells[16].Value.ToString()) + Localization.ParseNativeDouble(fgdtls_f_f.Rows[i].Cells[20].Value.ToString());
        //                        ////fgdtls_f_f.Rows[i].Cells[2].Value = (Localization.ParseNativeDouble(fgdtls_f_f.Rows[i].Cells[11].Value.ToString()) - Localization.ParseNativeDouble(this.fgdtls_f_f.Rows[i].Cells[13].Value.ToString())) - Localization.ParseNativeDouble(fgdtls_f_f.Rows[i].Cells[15].Value.ToString());
        //                        ////fgdtls_f_f.Rows[i].Cells[20].Value = "0";

        //                        fgDtls.Rows.Add();
        //                        iSrNo++;
        //                        IsOrderSelected = true;

        //                        ////using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where FabPOID = {1} and CompID = {2} and YearID = {3} ", new object[] { "tbl_FabricPurchaseOrderMain", fgdtls_f_f.Rows[i].Cells[1].Value, Db_Detials.CompID, Db_Detials.YearID })))
        //                        ////{
        //                        ////    while (reader.Read())
        //                        ////    {
        //                        ////        cboOrderNo.SelectedValue = Localization.ParseNativeInt(reader["FabPONo"].ToString());
        //                        ////        if (reader["FabPoDate"].ToString() != "")
        //                        ////        {
        //                        ////            dtOrderDate.Text = Localization.ToVBDateString(reader["FabPoDate"].ToString());
        //                        ////        }
        //                        ////        cboSupplier.SelectedValue = Localization.ParseNativeInt(reader["PartyID"].ToString());
        //                        ////        cboBroker.SelectedValue = Localization.ParseNativeInt(reader["BrokerID"].ToString());
        //                        ////        cboTransport.SelectedValue = Localization.ParseNativeInt(reader["TransportID"].ToString());
        //                        ////        cboDeptTo.SelectedValue = Localization.ParseNativeInt(reader["DelivaryAtID"].ToString());

        //                        ////        if (Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()) > 0.0)
        //                        ////        {
        //                        ////            string sqlQuery = string.Format("Select DISTINCT FabPOID, FabPONO  from {0}({1},{2},{3})", new object[] { "fn_FetchFabPurchaseOrderDtls", cboSupplier.SelectedValue, Db_Detials.CompID, Db_Detials.YearID });
        //                        ////            Combobox_Setup.Fill_Combo(this.cboOrderNo, sqlQuery, "FabPONo", "FabPOID");
        //                        ////            cboOrderNo.ColumnWidths = "0;100;0;0;0;0;0";
        //                        ////            cboOrderNo.AutoComplete = true;
        //                        ////            cboOrderNo.AutoDropdown = true;
        //                        ////        }
        //                        ////    }
        //                        ////}

        //                    }
        //                }
        //                catch { }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Navigate.logError(ex.Message, ex.StackTrace);
        //    }
        //    panel4.Visible = false;
        //}

        //private void cboOrderNo_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if ((base.blnFormAction == Enum_Define.ActionType.New_Record || blnFormAction == Enum_Define.ActionType.Edit_Record) && (cboSupplier.SelectedValue != null))
        //        {
        //            if (cboOrderNo.SelectedValue != null && Localization.ParseNativeInt(cboOrderNo.SelectedValue.ToString()) != 0 && cboOrderNo.SelectedValue.ToString() != "-")
        //            {
        //                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where FabPOID = {1} and CompID = {2} and YearID = {3} ", new object[] { "tbl_FabricPurchaseOrderMain", cboOrderNo.SelectedValue, Db_Detials.CompID, Db_Detials.YearID })))
        //                {
        //                    while (reader.Read())
        //                    {
        //                        if (reader["FabPoDate"].ToString() != "")
        //                        {
        //                            dtOrderDate.Text = Localization.ToVBDateString(reader["FabPoDate"].ToString());
        //                        }
        //                        cboSupplier.SelectedValue = Localization.ParseNativeInt(reader["PartyID"].ToString());
        //                        cboBroker.SelectedValue = Localization.ParseNativeInt(reader["BrokerID"].ToString());
        //                        cboTransport.SelectedValue = Localization.ParseNativeInt(reader["TransportID"].ToString());
        //                        cboDeptTo.SelectedValue = Localization.ParseNativeInt(reader["DelivaryAtID"].ToString());
        //                    }
        //                }
        //            }

        //            if (cboSupplier.SelectedValue != null && Localization.ParseNativeInt(cboSupplier.SelectedValue.ToString()) != 0 && cboSupplier.SelectedValue.ToString() != "-")
        //            {
        //                if (cboOrderNo.SelectedValue != null && Localization.ParseNativeInt(cboOrderNo.SelectedValue.ToString()) != 0 && cboOrderNo.SelectedValue.ToString() != "-")
        //                {
        //                    using (IDataReader reader2 = DB.GetRS(string.Format("Select * from {0} Where FabPoID = {1} ", "tbl_FabricPurchaseOrderDtls", cboOrderNo.SelectedValue)))
        //                    {
        //                        fgDtls.Columns[0].Visible = false;
        //                        fgDtls.Rows.Clear();
        //                        while (reader2.Read())
        //                        {
        //                            fgDtls.Rows.Add();
        //                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[1].Value = fgDtls.Rows.Count;
        //                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[5].Value = "-";
        //                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[3].Value = Localization.ParseNativeInt(reader2["FabPOID"].ToString());
        //                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[7].Value = Localization.ParseNativeInt(reader2["DesignID"].ToString());
        //                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[8].Value = Localization.ParseNativeInt(reader2["QualityID"].ToString());
        //                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[9].Value = Localization.ParseNativeInt(reader2["ShadeID"].ToString());
        //                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[4].Value = "-";
        //                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[10].Value = Localization.ParseNativeInt(reader2["GradeID"].ToString());
        //                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[11].Value = _MtrsID;

        //                            for (int i = 0; i <= (fgdtls_f_f.Rows.Count - 1); i++)
        //                            {
        //                                if (Operators.ConditionalCompareObjectEqual(fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[7].Value, fgdtls_f_f.Rows[i].Cells[3].Value, false))
        //                                {
        //                                    if (Operators.ConditionalCompareObjectEqual(fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[8].Value, fgdtls_f_f.Rows[i].Cells[8].Value, false))
        //                                    {
        //                                        if (Operators.ConditionalCompareObjectEqual(fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[9].Value, fgdtls_f_f.Rows[i].Cells[4].Value, false))
        //                                        {
        //                                            if (Operators.ConditionalCompareObjectEqual(fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[3].Value, fgdtls_f_f.Rows[i].Cells[1].Value, false))
        //                                            {
        //                                                fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[12].Value = fgdtls_f_f.Rows[i].Cells[2].Value;
        //                                                fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[13].Value = fgdtls_f_f.Rows[i].Cells[20].Value;
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Navigate.logError(ex.Message, ex.StackTrace);
        //    }
        //}
        //private void cboChallanNo_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        for (int i = 0; i <= fgDtls.RowCount - 1; i++)
        //        {
        //            if (cboChallanNo.SelectedValue != null && cboChallanNo.SelectedValue.ToString() != "" && cboChallanNo.SelectedValue.ToString() != "0")
        //            {
        //                fgDtls.Rows[i].Cells[6].ReadOnly = true;
        //                fgDtls.Rows[i].Cells[3].ReadOnly = true;
        //                fgDtls.Rows[i].Cells[7].ReadOnly = true;
        //                fgDtls.Rows[i].Cells[8].ReadOnly = true;
        //                fgDtls.Rows[i].Cells[9].ReadOnly = true;
        //                fgDtls.Rows[i].Cells[4].ReadOnly = true;
        //                fgDtls.Rows[i].Cells[10].ReadOnly = true;
        //                fgDtls.Rows[i].Cells[11].ReadOnly = true;
        //                fgDtls.Rows[i].Cells[12].ReadOnly = true;
        //                fgDtls.Rows[i].Cells[13].ReadOnly = true;
        //                fgDtls.Rows[i].Cells[14].ReadOnly = true;
        //            }
        //            else
        //            {
        //                fgDtls.Rows[i].Cells[6].ReadOnly = false;
        //                fgDtls.Rows[i].Cells[3].ReadOnly = false;
        //                fgDtls.Rows[i].Cells[7].ReadOnly = false;
        //                fgDtls.Rows[i].Cells[8].ReadOnly = false;
        //                fgDtls.Rows[i].Cells[9].ReadOnly = false;
        //                fgDtls.Rows[i].Cells[4].ReadOnly = false;
        //                fgDtls.Rows[i].Cells[10].ReadOnly = false;
        //                fgDtls.Rows[i].Cells[11].ReadOnly = false;
        //                fgDtls.Rows[i].Cells[12].ReadOnly = false;
        //                fgDtls.Rows[i].Cells[13].ReadOnly = false;
        //                fgDtls.Rows[i].Cells[14].ReadOnly = false;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Navigate.logError(ex.Message, ex.StackTrace);
        //    }
        //}
        #endregion

        private void txtBrokerPercent_Leave(object sender, EventArgs e)
        {
            try
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (ENABLE_BROKER_FAB_PURCHASE)
                    {
                        if (ENABLE_BROKER_CALCMETHOD1)
                        {
                            if (txtBrokerPercent.Text != null && txtBrokerPercent.Text != "" && txtBrokerPercent.Text != "0" && TxtGrossAmount.Text != null && TxtGrossAmount.Text != "" && TxtGrossAmount.Text != "0")
                            {
                                decimal dbrokertotalamt = Localization.ParseNativeDecimal(txtBrokerTotalAmount.Text.ToString());
                                decimal dbrokersamt = ((Localization.ParseNativeDecimal(txtBrokerPercent.Text.ToString()) / 100) * (Localization.ParseNativeDecimal(TxtGrossAmount.Text.ToString())));

                                dbrokertotalamt = Localization.ParseNativeDecimal(dbrokersamt.ToString());
                                txtBrokerTotalAmount.Text = dbrokertotalamt.ToString();

                                string sstring = txtBrokerTotalAmount.Text;
                                string sval = sstring.Substring(0, (sstring.Length - 1));
                                txtBrokerTotalAmount.Text = sval;
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

        private void txtBrokerTotalAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (ENABLE_BROKER_CALCMETHOD2)
                    {
                        decimal TotalAmtCalc1 = Math.Round((Localization.ParseNativeDecimal(txtBrokerTotalAmount.Text.ToString()) / Localization.ParseNativeDecimal(TxtGrossAmount.Text.ToString())) * 100);
                        txtBrokerPercent.Text = TotalAmtCalc1.ToString();
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
            //if (cboChallanNo.SelectedValue == null || cboChallanNo.SelectedValue.ToString() == "-")
            {
                if (Vld_DupPieceNo)
                {
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                        {
                            string primaryFieldNameValue = fgDtls.Rows[i].Cells[5].Value.ToString();
                            if (fgDtls.Rows[i].Cells[5].Value.ToString() != null && fgDtls.Rows[i].Cells[5].Value.ToString().Length > 0)
                            {
                                //if (fgDtls.Rows[i].Cells[5].Value.ToString() != "-")
                                {
                                    strTbleName = "tbl_StockFabricLedger";
                                    if (Navigate.CheckDuplicate(ref strTbleName, "BatchNo", primaryFieldNameValue, false, "", 0L, " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "", ""))
                                    {
                                        fgDtls.CurrentCell = fgDtls[2, i];
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
                            if (fgDtls.Rows[j].Cells[5].Value.ToString() != null && fgDtls.Rows[j].Cells[5].Value.ToString().Length > 0)
                            {
                                //if (fgDtls.Rows[j].Cells[5].Value.ToString() != "-")
                                {
                                    strTbleName = "tbl_StockFabricLedger";
                                    if (Navigate.CheckDuplicate(ref strTbleName, "BatchNo", fgDtls.Rows[j].Cells[5].Value.ToString(), true, "TransID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "", ""))
                                    {
                                        fgDtls.CurrentCell = fgDtls[2, j];
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
            //return false;
        }

        private bool CheckSerialCombn()
        {
            int iDesignID, iQualityID, iShadeID;
            for (int i = 0; i <= fgDtls.RowCount - 1; i++)
            {
                if (fgDtls.Rows[i].Cells[6].Value != null && fgDtls.Rows[i].Cells[6].Value.ToString() != "0" && fgDtls.Rows[i].Cells[6].Value.ToString() != "-")
                {
                    using (IDataReader dr = DB.GetRS("Select DesignID,QualityID,ShadeID from fn_BookSerialDtlsBind() where SerialID=" + fgDtls.Rows[i].Cells[6].Value + ""))
                    {
                        while (dr.Read())
                        {
                            iDesignID = Localization.ParseNativeInt(dr["DesignID"].ToString());
                            iQualityID = Localization.ParseNativeInt(dr["QualityID"].ToString());
                            iShadeID = Localization.ParseNativeInt(dr["ShadeID"].ToString());
                            if (iDesignID != Localization.ParseNativeInt(fgDtls.Rows[i].Cells[7].Value.ToString()) || iQualityID != Localization.ParseNativeInt(fgDtls.Rows[i].Cells[8].Value.ToString()) || iShadeID != Localization.ParseNativeInt(fgDtls.Rows[i].Cells[9].Value.ToString()))
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Check Serial Combination");
                                fgDtls.CurrentCell = fgDtls[3, i];
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Serial");
                    fgDtls.CurrentCell = fgDtls[3, i];
                    return true;
                }
            }
            return false;
        }

        private void dtDueDate_Leave(object sender, EventArgs e)
        {
            try
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    //if (txtCrDays.Text == "0" || txtCrDays.Text == "" || txtCrDays.Text.Trim().Length == 0)
                    {
                        if (Information.IsDate(dtDueDate.Text.ToString()) && Information.IsDate(dtBillDate.Text.ToString()))
                        {
                            DateTime timedue = Conversions.ToDate(dtDueDate.Text);
                            DateTime timebill = Conversions.ToDate(dtBillDate.Text);
                            int days = (timedue - timebill).Days;
                            txtCrDays.Text = days.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        //private void CheckOrders()
        //{
        //    try
        //    {
        //        IsCheckOrders = false;
        //        for (int l = 0; l <= fgDtls.Rows.Count - 1; l++)
        //        {
        //            if (fgDtls.Rows[l].Cells[3].Value != null && fgDtls.Rows[l].Cells[3].Value.ToString() != "" && fgDtls.Rows[l].Cells[3].Value.ToString() != "-" && fgDtls.Rows[l].Cells[3].Value.ToString() != "0")
        //            {
        //                IsCheckOrders = true;
        //                break;
        //            }
        //        }
        //        if (base.blnFormAction == Enum_Define.ActionType.New_Record && (IsCheckOrders))
        //        {
        //            if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
        //            {
        //                string SOrderID = "", SUnitID = "", SOrderValidnType = "";
        //                int iCurrentpcs = 0;
        //                double dCurrentMtrs = 0;
        //                isRet = false;

        //                DataTable dtLocalC = new DataTable();

        //                foreach (DataGridViewColumn col in fgDtls.Columns)
        //                {
        //                    dtLocalC.Columns.Add(col.HeaderText, col.ValueType);
        //                }
        //                foreach (DataGridViewRow gridRow in fgDtls.Rows)
        //                {
        //                    DataRow dtRow = dtLocalC.NewRow();
        //                    for (int i = 0; i < fgDtls.Columns.Count; i++)
        //                        dtRow[i] = (gridRow.Cells[i].Value == null ? DBNull.Value : gridRow.Cells[i].Value);
        //                    dtLocalC.Rows.Add(dtRow);
        //                }

        //                //dtLocalC.Columns.Add("FabPurchaseID");
        //                //dtLocalC.Columns.Add("SubFabPurchaseID");
        //                //dtLocalC.Columns.Add("PieceNo");
        //                //dtLocalC.Columns.Add("BookSerialID");
        //                //dtLocalC.Columns.Add("OrderNo");
        //                //dtLocalC.Columns.Add("DesignID");
        //                //dtLocalC.Columns.Add("QualityID");
        //                //dtLocalC.Columns.Add("ShadeID");
        //                //dtLocalC.Columns.Add("LotNo");
        //                //dtLocalC.Columns.Add("GradeID");
        //                //dtLocalC.Columns.Add("UnitID");
        //                //dtLocalC.Columns.Add("Cuts");
        //                //dtLocalC.Columns.Add("Mtrs");
        //                //dtLocalC.Columns.Add("NetWt");
        //                //dtLocalC.Columns.Add("Rate");
        //                //dtLocalC.Columns.Add("Amount");
        //                //dtLocalC.Columns.Add("FabricInwardID");
        //                //dtLocalC.Columns.Add("BrokersPercent");
        //                //dtLocalC.Columns.Add("BrokersAmount");
        //                //dtLocalC.Columns.Add("RefID");
        //                //dtLocalC.Columns.Add("ARefID");

        //                //DataRow drLocal = null;
        //                //for (int i = 0; i <= fgDtls.RowCount - 1; i++)
        //                //{
        //                //    drLocal = dtLocalC.NewRow();
        //                //    drLocal["FabPurchaseID"] = fgDtls.Rows[i].Cells[0].Value;
        //                //    drLocal["SubFabPurchaseID"] = fgDtls.Rows[i].Cells[1].Value;
        //                //    drLocal["PieceNo"] = fgDtls.Rows[i].Cells[5].Value;
        //                //    drLocal["BookSerialID"] = fgDtls.Rows[i].Cells[6].Value;
        //                //    drLocal["OrderNo"] = fgDtls.Rows[i].Cells[3].Value;
        //                //    drLocal["DesignID"] = fgDtls.Rows[i].Cells[7].Value;
        //                //    drLocal["QualityID"] = fgDtls.Rows[i].Cells[8].Value;
        //                //    drLocal["ShadeID"] = fgDtls.Rows[i].Cells[9].Value;
        //                //    drLocal["LotNo"] = fgDtls.Rows[i].Cells[4].Value;
        //                //    drLocal["GradeID"] = fgDtls.Rows[i].Cells[10].Value;
        //                //    drLocal["UnitID"] = fgDtls.Rows[i].Cells[11].Value;
        //                //    drLocal["Cuts"] = fgDtls.Rows[i].Cells[12].Value;
        //                //    drLocal["Mtrs"] = fgDtls.Rows[i].Cells[13].Value;
        //                //    drLocal["NetWt"] = fgDtls.Rows[i].Cells[14].Value;
        //                //    drLocal["Rate"] = fgDtls.Rows[i].Cells[15].Value;
        //                //    drLocal["Amount"] = fgDtls.Rows[i].Cells[16].Value;
        //                //    drLocal["FabricInwardID"] = fgDtls.Rows[i].Cells[2].Value;
        //                //    drLocal["BrokersPercent"] = fgDtls.Rows[i].Cells[20].Value;
        //                //    drLocal["BrokersAmount"] = fgDtls.Rows[i].Cells[21].Value;
        //                //    drLocal["RefID"] = fgDtls.Rows[i].Cells[36].Value;
        //                //    drLocal["ARefID"] = fgDtls.Rows[i].Cells[37].Value;
        //                //    dtLocalC.Rows.Add(drLocal);
        //                //}

        //                for (int j = 0; j <= fgDtls.RowCount - 1; j++)
        //                {
        //                    DataGridViewRow row1 = fgDtls.Rows[j];
        //                    if (row1.Cells[3].Value != null && row1.Cells[3].Value.ToString() != "" && row1.Cells[3].Value.ToString() != "-" && row1.Cells[3].Value.ToString() != "0")
        //                    {
        //                        SOrderID += row1.Cells[3].Value + ",";
        //                    }
        //                }
        //                SOrderID = SOrderID.Remove(SOrderID.Length - 1);

        //                string[] sordarray = SOrderID.Split(',');
        //                string[] tmparray = sordarray;

        //                for (int k = 0; k <= tmparray.Length - 1; k++)
        //                {
        //                    if (Localization.ParseNativeInt(sordarray[0]) >= 0)
        //                    {
        //                        DataRow[] dr_orderID = dtLocalC.Select("OrderNo='" + Localization.ParseNativeInt(sordarray[0]) + "'");
        //                        if (dr_orderID.Length > 0)
        //                        {
        //                            foreach (DataRow r in dr_orderID)
        //                            {
        //                                DataRow[] dr_SngRefID = dtLocalC.Select("ARefID='" + r["ARefID"].ToString() + "'");
        //                                if (dr_SngRefID.Length > 0)
        //                                {
        //                                    foreach (DataRow dr_row in dr_SngRefID)
        //                                    {
        //                                        iCurrentpcs += Localization.ParseNativeInt(dr_row["Cuts"].ToString());
        //                                        dCurrentMtrs += Localization.ParseNativeDouble(dr_row["Mtrs"].ToString());
        //                                    }
        //                                }

        //                                int iBalPcs = Localization.ParseNativeInt(DB.GetSnglValue("Select Balpcs from fn_FetchFabPurchaseOrderDtls(" + cboSupplier.SelectedValue + "," + Db_Detials.CompID + "," + Db_Detials.YearID + ") Where RefID=" + CommonLogic.SQuote(r["ARefID"].ToString()) + "and FabPOID=" + sordarray[0]));
        //                                double dBalMtrs = Localization.ParseNativeDouble(DB.GetSnglValue("Select Balmtrs from fn_FetchFabPurchaseOrderDtls(" + cboSupplier.SelectedValue + "," + Db_Detials.CompID + "," + Db_Detials.YearID + ") Where RefID=" + CommonLogic.SQuote(r["ARefID"].ToString()) + "and FabPOID=" + sordarray[0]));
        //                                SUnitID = (DB.GetSnglValue("Select UnitID from fn_FetchFabPurchaseOrderDtls(" + cboSupplier.SelectedValue + "," + Db_Detials.CompID + "," + Db_Detials.YearID + ") Where RefID=" + CommonLogic.SQuote(r["ARefID"].ToString()) + "and FabPOID=" + sordarray[0]));
        //                                if (SUnitID != "" && SUnitID.Length > 0)
        //                                {
        //                                    //SunitName = DB.GetSnglValue("Select Unitname from fn_FabricPurchaseOrder_FindDtlsWithIDs(" + Db_Detials.CompID + "," + Db_Detials.YearID + ") Where  UnitID=" + SUnitID + " AND FabPOID=" + sordarray[0]);
        //                                    SOrderValidnType = DB.GetSnglValue("Select OrderValidationType from tbl_UnitsMaster Where UnitID= " + SUnitID + " and IsDeleted=0");
        //                                }

        //                                if (SOrderValidnType == "P")
        //                                {
        //                                    if (iBalPcs <= 0)
        //                                    {
        //                                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "Not Enough Bal Pieces For the Order No " + DB.GetSnglValue("select FabPONo from fn_FabricPurchaseOrderMain_Tbl() where FabPOID=" + sordarray[0]));
        //                                        isRet = true;
        //                                        return;
        //                                    }
        //                                    else if (iCurrentpcs > iBalPcs)
        //                                    {
        //                                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "Current Pieces " + iCurrentpcs + " Exceeding Bal Pieces  " + iBalPcs + "  For the Order No " + DB.GetSnglValue("select FabPONo from fn_FabricPurchaseOrderMain_Tbl() where FabPOID=" + sordarray[0]));
        //                                        isRet = true;
        //                                        return;
        //                                    }
        //                                }
        //                                if (SOrderValidnType == "M")
        //                                {
        //                                    if (dBalMtrs <= 0)
        //                                    {
        //                                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "Not Enough Bal Meters For the Order No " + DB.GetSnglValue("select FabPONo from fn_FabricPurchaseOrderMain_Tbl() where FabPOID=" + sordarray[0]));
        //                                        isRet = true;
        //                                        return;
        //                                    }

        //                                    if (ExcessInward_Mtrs <= 0)
        //                                    {
        //                                        if (dCurrentMtrs > dBalMtrs)
        //                                        {
        //                                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "Current Meters " + dCurrentMtrs + " Exceeding Bal Meters  " + dBalMtrs + "  For the Order No " + DB.GetSnglValue("select FabPONo from fn_FabricPurchaseOrderMain_Tbl() where FabPOID=" + sordarray[0]));
        //                                            isRet = true;
        //                                            return;
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        double dPercMtrs = (double)ExcessInward_Mtrs / (double)100 * dBalMtrs;
        //                                        dBalMtrs = dBalMtrs + dPercMtrs;
        //                                        if (dCurrentMtrs > dBalMtrs)
        //                                        {
        //                                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "Current Meters " + dCurrentMtrs + " Exceeding Bal Meters  " + dBalMtrs + "  For the Order No " + DB.GetSnglValue("select FabPONo from fn_FabricPurchaseOrderMain_Tbl() where FabPOID= " + sordarray[0]) + " After Excess Inward of  " + ExcessInward_Mtrs + "%");
        //                                            isRet = true;
        //                                            return;
        //                                        }
        //                                    }
        //                                }
        //                                iCurrentpcs = 0;
        //                                dCurrentMtrs = 0;
        //                            }
        //                            //sordarray = sordarray.Length - sordarray.Length - 1;
        //                            int numIdx = 0;
        //                            List<string> tmp = new List<string>(sordarray);
        //                            tmp.RemoveAt(numIdx);
        //                            sordarray = tmp.ToArray();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Navigate.logError(ex.Message, ex.StackTrace);
        //    }
        //}

        private void EnabDisab(bool isenab)
        {
            if (isenab)
            {
                fgDtls.Columns[3].ReadOnly = false;
                fgDtls.Columns[6].ReadOnly = false;
                fgDtls.Columns[7].ReadOnly = false;
                fgDtls.Columns[8].ReadOnly = false;
                fgDtls.Columns[9].ReadOnly = false;
                fgDtls.Columns[11].ReadOnly = false;
                btnSelectInward.Enabled = true;
                btnMultipleorders.Enabled = false;
            }
            else
            {
                fgDtls.Columns[3].ReadOnly = true;
                fgDtls.Columns[6].ReadOnly = true;
                fgDtls.Columns[7].ReadOnly = true;
                fgDtls.Columns[8].ReadOnly = true;
                fgDtls.Columns[9].ReadOnly = true;
                fgDtls.Columns[11].ReadOnly = true;
                btnSelectInward.Enabled = false;
                btnMultipleorders.Enabled = true;
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
                        RefVoucherID += DB.GetSnglValue(string.Format("Select SeriesID From fn_MenuMaster_tbl() Where MenuID=" + arr[i] + "")) + ",";
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
                            if ((Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select COUNT(0) From fn_FabricOrderLedger_tbl() Where RefId='" + fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[36].Value + "' and RefID<>'' and Transtype<>" + iIDentity + ""))) > 0))
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityWarning, "", "Reference Found In Another Module..Row Cannot Be Deleted");
                                return;
                            }
                            else
                            {
                                try
                                {
                                    string strQry = string.Format("Update tbl_FabricOrderLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[39].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and CUserID=" + Db_Detials.UserID + ";");
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
                                string strQry = string.Format("Update tbl_FabricOrderLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[39].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and CUserID=" + Db_Detials.UserID + ";");
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

        private void frmFabricPurchase_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (strUniqueID != null)
            {
                string strQry = string.Format("Delete From tbl_FabricOrderLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and CUserId=" + Db_Detials.UserID + ";");
                strQry = strQry + string.Format("Update  tbl_FabricOrderLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and CUserId=" + Db_Detials.UserID + ";");
                DB.ExecuteSQL(strQry);
                strQry = string.Format("Update tbl_FabricOrderLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                DB.ExecuteSQL(strQry);
            }
        }

        #region Runtime Less Of Orders

        public void ExecuterTempQry(int RowIndex)
        {
            if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
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
                                strQry = string.Format("Delete From tbl_FabricOrderLedger Where Dr_Qty=0 and Dr_Mtrs=0 and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and CUserId=" + Db_Detials.UserID + ";");
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
                                        StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_FabricOrderLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_FabricOrderLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + "")));
                                        MyID = txtCode.Text;
                                    }

                                    if (MyID != "" && row.Cells[3].Value != null && row.Cells[3].Value.ToString() != "" && row.Cells[3].Value.ToString() != "0" && row.Cells[13].Value != null && row.Cells[13].Value.ToString() != "")
                                    {
                                        string sBatchNo = string.Empty;
                                        sBatchNo = DB.GetSnglValue("Select FabPONo from fn_FabricPurchaseOrderMain_Tbl() Where FabPOID=" + row.Cells[3].Value.ToString());

                                        strQry += DBSp.InsertIntoFabricOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), MyID, (i + 1).ToString(), txtEntryNo.Text,
                                                    dtBillDate.Text, Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()), row.Cells[37].Value == null ? "0" : row.Cells[37].Value.ToString() == "" ? "0" : row.Cells[37].Value.ToString(),
                                                    "0", row.Cells[4].Value.ToString(), sBatchNo, Localization.ParseNativeDouble(row.Cells[6].Value.ToString()), Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                                    Localization.ParseNativeDouble(row.Cells[7].Value.ToString()), Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                                    Localization.ParseNativeDouble(row.Cells[11].Value.ToString()), 0, 0, 0, 0, Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()),
                                                    Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()), 0, 0, "NULL", 0,
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
                                                    txtUniqueID.Text, i, StatusID, "Purchase", row.Cells[35].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[35].Value.ToString()), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                                    }
                                }
                            }
                            else
                            {
                                if ((fgDtls.CurrentCell.ColumnIndex == 11) || (fgDtls.CurrentCell.ColumnIndex == 12) || (fgDtls.CurrentCell.ColumnIndex == 13))
                                {
                                    DataGridViewRow row = fgDtls.Rows[RowIndex];
                                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                                    {
                                        StatusID = 1;
                                        MyID = iMaxMyID.ToString();
                                    }
                                    else
                                    {
                                        StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_FabricOrderLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_FabricOrderLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + "")));
                                        MyID = txtCode.Text;
                                    }

                                    if (MyID != "" && row.Cells[3].Value != null && row.Cells[3].Value.ToString() != "" && row.Cells[3].Value.ToString() != "0" && row.Cells[13].Value != null && row.Cells[13].Value.ToString() != "")
                                    {
                                        string sBatchNo = string.Empty;
                                        sBatchNo = DB.GetSnglValue("Select FabPONo from fn_FabricPurchaseOrderMain_Tbl() Where FabPOID=" + row.Cells[3].Value.ToString());

                                        if (txtUniqueID.Text != null)
                                        {
                                            strQry += string.Format("Delete From tbl_FabricOrderLedger Where Dr_Qty=0 and Dr_Mtrs=0 and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[39].Value.ToString()) + " and CUserId=" + Db_Detials.UserID + ";");

                                            strQry += DBSp.InsertIntoFabricOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), MyID, (RowIndex + 1).ToString(), txtEntryNo.Text,
                                                        dtBillDate.Text, Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()), row.Cells[37].Value == null ? "0" : row.Cells[37].Value.ToString() == "" ? "0" : row.Cells[37].Value.ToString(),
                                                        "0", row.Cells[4].Value.ToString(), sBatchNo, Localization.ParseNativeDouble(row.Cells[6].Value.ToString()), Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                                        Localization.ParseNativeDouble(row.Cells[7].Value.ToString()), Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                                        Localization.ParseNativeDouble(row.Cells[11].Value.ToString()), 0, 0, 0, 0, Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()),
                                                        Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()), 0, 0, "NULL", 0,
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
                                                        txtUniqueID.Text, Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[39].Value.ToString()), StatusID, "Purchase", row.Cells[35].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[35].Value.ToString()), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
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
        }

        private void setTempRowIndex()
        {
            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[39].Value = i;
            }
        }

        private void setMyID()
        {
            iMaxMyID = Localization.ParseNativeInt(DB.GetSnglValue("Select MAX(MyId + 1) from tbl_FabricOrderLedger Where IsDeleted=0"));

            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[38].Value = iMaxMyID;
            }
        }

        #endregion
    }
}
