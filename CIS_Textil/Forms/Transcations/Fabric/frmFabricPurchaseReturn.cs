using System;
using System.Collections;
using System.Collections.Generic;
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
    public partial class frmFabricPurchaseReturn : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        public DataGridViewEx fgDtls_f;
        public DataGridViewEx fgDtls_f_footer;

        private bool ENABLE_BROKER_FAB_PURCHASERETURN;
        private bool ENABLE_BROKER_CALCMETHOD1;
        private bool ENABLE_BROKER_CALCMETHOD2;
        string SBrokersPerc = string.Empty;
        private int iMaxMyID;
        public string strUniqueID;

        public frmFabricPurchaseReturn()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;

            fgDtls_f = GrdDtls.fgDtls;
            fgDtls_f_footer = GrdDtls.fgDtls_f;
        }

        #region Event

        private void frmFabricPurchaseReturn_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboSupplier, Combobox_Setup.ComboType.Mst_Suppliers, "");
                Combobox_Setup.FillCbo(ref cboBroker, Combobox_Setup.ComboType.Mst_Brokers, "");
                Combobox_Setup.FillCbo(ref cboDepartment, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
                Combobox_Setup.FillCbo(ref cboAcType, Combobox_Setup.ComboType.PurchaseAc, "");
                object instance = new Dictionary<int, string>();
                NewLateBinding.LateIndexSet(instance, new object[] { 1, "Before Bill" }, null);
                NewLateBinding.LateIndexSet(instance, new object[] { 2, "After Bill" }, null);
                NewLateBinding.LateIndexSet(instance, new object[] { 3, "Other" }, null);
                CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboReturnType = this.cboReturnType;
                cboReturnType.DataSource = new BindingSource(RuntimeHelpers.GetObjectValue(instance), null);
                cboReturnType.DisplayMember = "Value";
                cboReturnType.ValueMember = "Key";
                cboReturnType.ColumnWidths = "0;";
                cboReturnType.AutoComplete = true;
                cboReturnType.AutoDropdown = true;
                cboReturnType.SelectedIndex = -1;
                cboReturnType = null;
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls_f, fgDtls_f_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, true, true, 0, 1, true);
                txtEntryNo.Enabled = false;
                ENABLE_BROKER_FAB_PURCHASERETURN = Localization.ParseBoolean(GlobalVariables.ENABLE_BROKER_FAB_PURCHASERETURN);
                ENABLE_BROKER_CALCMETHOD1 = Localization.ParseBoolean(GlobalVariables.ENABLE_BROKER_CALCMETHOD1);
                ENABLE_BROKER_CALCMETHOD2 = Localization.ParseBoolean(GlobalVariables.ENABLE_BROKER_CALCMETHOD2);

                if (ENABLE_BROKER_FAB_PURCHASERETURN)
                {
                    if (ENABLE_BROKER_CALCMETHOD2)
                    {
                        fgDtls.Columns[18].Visible = true;
                        fgDtls.Columns[19].Visible = true;
                        txtBrokerPercent.Enabled = false;
                    }
                    else
                    {
                        fgDtls.Columns[18].Visible = false;
                        fgDtls.Columns[19].Visible = false;
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
                    fgDtls.Columns[18].Visible = false;
                    fgDtls.Columns[19].Visible = false;
                }

                fgDtls.CellValueChanged +=new DataGridViewCellEventHandler(fgDtls_CellValueChanged);
                fgDtls.RowsAdded +=new DataGridViewRowsAddedEventHandler(fgDtls_RowsAdded);
                fgDtls_f.CellValueChanged +=new DataGridViewCellEventHandler(fgDtls_f_CellValueChanged);
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
                DBValue.Return_DBValue(this, txtCode, "FabPurRtnID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtBillNo, "BillNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtBillDate, "BillDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtRefNo, "RefNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtRefDate, "RefDate", Enum_Define.ValidationType.IsDate);

                int i = Localization.ParseNativeInt(DB.GetSnglValue("select ReturnTypeID from tbl_FabricPurchaseReturnMain where FabPurRtnID='" + txtCode.Text + "'"));
                if (i == 1)
                {
                    object instance = new Dictionary<int, string>();
                    NewLateBinding.LateIndexSet(instance, new object[] { 1, "Before Bill" }, null);
                    NewLateBinding.LateIndexSet(instance, new object[] { 2, "After Bill" }, null);
                    NewLateBinding.LateIndexSet(instance, new object[] { 3, "Other" }, null);
                    CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboReturnType = this.cboReturnType;
                    cboReturnType.DataSource = new BindingSource(RuntimeHelpers.GetObjectValue(instance), null);
                    cboReturnType.DisplayMember = "Value";
                    cboReturnType.ValueMember = "Key";
                    cboReturnType.ColumnWidths = "0;";
                    cboReturnType.SelectedIndex = 0;
                }
                else if (i == 2)
                {
                    object instance = new Dictionary<int, string>();
                    NewLateBinding.LateIndexSet(instance, new object[] { 1, "Before Bill" }, null);
                    NewLateBinding.LateIndexSet(instance, new object[] { 2, "After Bill" }, null);
                    NewLateBinding.LateIndexSet(instance, new object[] { 3, "Other" }, null);
                    CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboReturnType = this.cboReturnType;
                    cboReturnType.DataSource = new BindingSource(RuntimeHelpers.GetObjectValue(instance), null);
                    cboReturnType.DisplayMember = "Value";
                    cboReturnType.ValueMember = "Key";
                    cboReturnType.ColumnWidths = "0;";
                    cboReturnType.SelectedIndex = 1;
                }
                else
                {
                    object instance = new Dictionary<int, string>();
                    NewLateBinding.LateIndexSet(instance, new object[] { 1, "Before Bill" }, null);
                    NewLateBinding.LateIndexSet(instance, new object[] { 2, "After Bill" }, null);
                    NewLateBinding.LateIndexSet(instance, new object[] { 3, "Other" }, null);
                    CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboReturnType = this.cboReturnType;
                    cboReturnType.DataSource = new BindingSource(RuntimeHelpers.GetObjectValue(instance), null);
                    cboReturnType.DisplayMember = "Value";
                    cboReturnType.ValueMember = "Key";
                    cboReturnType.ColumnWidths = "0;";
                    cboReturnType.SelectedIndex = 2;
                }
                DBValue.Return_DBValue(this, cboSupplier, "SupplierID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDepartment, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLrNo, "LrNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtLrDate, "LrDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboAcType, "PurchaseID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, TxtTotalPcs, "TotPcs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, TxtTotMtrs, "TotMtrs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, TxtGrossAmount, "GrossAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAddLessAmt, "AddLessAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtNetAmt, "NetAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtBrokerPercent, "BrokerAvgPercentage", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtBrokerTotalAmount, "BrokerTotalAmount", Enum_Define.ValidationType.Text);

                DetailGrid_Setup.FillGrid(fgDtls, fgDtls.Grid_UID, fgDtls.Grid_Tbl, "FabPurRtnID", txtCode.Text, base.dt_HasDtls_Grd);
                DetailGrid_Setup.FillGrid(fgDtls_f, fgDtls_f.Grid_UID, fgDtls_f.Grid_Tbl, "FabPurRtnID", txtCode.Text, base.dt_HasDtls_Grd);

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                }
                AplySelectBtnEnbl();
                CalcVal();

                int icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_StockFabricLedger_tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));
                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, dt_HasDtls_Grd, dt_AryCalcvalue, dt_AryIsRequired, true, false);
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
                    for (int j = 0; j <= fgDtls.Rows.Count - 1; j++)
                    {
                        if (icount > 0)
                        {
                            btnSelect.Enabled = false;
                            fgDtls.Rows[j].ReadOnly = true;
                            fgDtls.Rows[j].DefaultCellStyle = dgvCellStyle_Ref;
                        }
                        else
                        {
                            btnSelect.Enabled = true;
                            fgDtls.Rows[j].ReadOnly = false;
                        }
                    }
                }
                catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
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
                if (!EventHandles.IsRequiredInGrid(fgDtls, this.dt_AryIsRequired, false))
                {
                    return true;
                }

                if (!Information.IsDate(dtEntryDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry Date");
                    dtEntryDate.Focus();
                    return true;
                }
                if (txtEntryNo.Text.Trim() == "" || txtEntryNo.Text.Trim() == "-" || txtEntryNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry No.");
                    txtEntryNo.Focus();
                    return true;
                }
                if (!Information.IsDate(dtBillDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Bill Date");
                    dtBillDate.Focus();
                    return true;
                }
                if (txtBillNo.Text.Trim() == "" || txtBillNo.Text.Trim() == "-" || txtBillNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Bill No.");
                    txtBillNo.Focus();
                    return true;
                }
                if (cboSupplier.SelectedValue == null || cboSupplier.Text.Trim().ToString() == "-" || cboSupplier.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Party ");
                    cboSupplier.Focus();
                    return true;
                }
                if (cboAcType.SelectedValue == null || cboAcType.Text.Trim().ToString() == "-" || cboAcType.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Purchase A/c ");
                    cboAcType.Focus();
                    return true;
                }
                if (cboDepartment.SelectedValue == null || cboDepartment.SelectedValue.ToString() == "" || cboDepartment.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department ");
                    cboDepartment.Focus();
                    return true;
                }
                if (txtBillNo.Text.Trim().Length > 0)
                {
                    string strTblName;
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        strTblName = "tbl_FabricPurchaseReturnMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "BillNo", txtBillNo.Text, false, "", 0L, string.Format("CompID = {0} and YearID = {1} and BranchID = {2} and StoreID = {3}", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.StoreID), "This Purchase Return No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where BillNo = '{1}' And CompID = {2} And YearID = {3} and BranchID = {4} and StoreID = {5}", new object[] { "tbl_FabricPurchaseReturnMain", txtBillNo.Text.ToString(), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.StoreID }))))
                        {
                            txtBillNo.Focus();
                            return true;
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        strTblName = "tbl_FabricPurchaseReturnMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "BillNo", txtBillNo.Text, true, "FabPurRtnID", Localization.ParseNativeLong(txtCode.Text.Trim()), string.Format("CompID = {0} and YearID = {1} and BranchID = {2} and StoreID = {3}", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.StoreID), "This Purchase Return No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where BillNo = '{1}' And CompID = {2} And YearID = {3} and BranchID = {4} and StoreID = {5}", new object[] { "tbl_FabricPurchaseReturnMain", txtBillNo.Text.ToString(), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.StoreID }))))
                        {
                            txtBillNo.Focus();
                            return true;
                        }
                    }
                }

                if (ENABLE_BROKER_FAB_PURCHASERETURN)
                {
                    if (cboBroker.SelectedValue == null || cboBroker.Text.Trim().ToString() == "-" || cboBroker.SelectedValue.ToString() == "0")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Broker");
                        cboBroker.Focus();
                        return true;
                    }
                }

                if (cboReturnType.SelectedItem.ToString() == "[3, Other]")
                {
                    if (cboDepartment.SelectedValue == null || cboDepartment.Text.Trim().ToString() == "-" || cboDepartment.SelectedValue.ToString() == "0")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                        cboDepartment.Focus();
                        return true;
                    }
                }
                CalcVal();
                return false;
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                return false;
            }
        }

        public void MovetoField()
        {
            try
            {
                if (strUniqueID != null)
                {
                    string strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and CUserId=" + Db_Detials.UserID + ";");
                    strQry = strQry + string.Format("Update  tbl_StockFabricLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and CUserId=" + Db_Detials.UserID + ";");
                    DB.ExecuteSQL(strQry);
                    strQry = string.Format("Update tbl_StockFabricLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                    DB.ExecuteSQL(strQry);
                }
                txtCode.Text = "";
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                this.txtBillNo.Text = CommonCls.AutoInc(this, "BillNo", "FabPurRtnID", "");
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CreateDefault_Rows(fgDtls_f, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);

                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                EventHandles.CalculateFooter_Rows(fgDtls_f, fgDtls_f_footer, fgDtls_f.Grid_ID.ToString(), fgDtls_f.Grid_UID);
                int MaxId = Localization.ParseNativeInt(DB.GetSnglValue(string.Format(" Select Isnull(Max(FabPurRtnID),0) From {0}  Where CompID = {1} and YearID = {2} and BranchID = {3} and StoreID = {4}", "tbl_FabricPurchaseReturnMain", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.StoreID)));
                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where FabPurRtnID = {1} and CompID={2} and YearID={3} and BranchID = {4} and StoreID = {5}", new object[] { "tbl_FabricPurchaseReturnMain", MaxId, Db_Detials.CompID, Db_Detials.YearID , Db_Detials.BranchID, Db_Detials.StoreID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                        dtBillDate.Text = Localization.ToVBDateString(reader["BillDate"].ToString());
                        dtRefDate.Text = Localization.ToVBDateString(reader["RefDate"].ToString());
                        cboSupplier.SelectedValue = Localization.ParseNativeInt(reader["SupplierID"].ToString());
                        cboTransport.SelectedValue = Localization.ParseNativeInt(reader["TransportID"].ToString());
                        cboBroker.SelectedValue = Localization.ParseNativeInt(reader["BrokerID"].ToString());
                        cboDepartment.SelectedValue = Localization.ParseNativeInt(reader["DepartmentID"].ToString());
                        cboAcType.SelectedValue = Localization.ParseNativeInt(reader["PurchaseID"].ToString());
                    }
                }
                if (fgDtls.Rows.Count > 0)
                {
                    if (fgDtls.Columns[3].Visible)
                    {
                        fgDtls.Rows[0].Cells[3].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select {0}({1},{2})", new object[] { "dbo.fn_FetchPieceNo_Stock", Db_Detials.CompID, Db_Detials.YearID })), Db_Detials.PCS_NO_INCMT);
                    }
                    else
                    {
                        fgDtls.Rows[0].Cells[3].Value = "-";
                    }
                }
                dtEntryDate.Focus();
                AplySelectBtnEnbl();
                TxtTotalPcs.Text = "0";
                TxtTotMtrs.Text = "0.00";
                TxtGrossAmount.Text = "0.00";
                txtAddLessAmt.Text = "0.00";
                txtNetAmt.Text = "0.00";
                txtBrokerPercent.Text = "";
                cboReturnType.SelectedValue = 1;
                txtUniqueID.Text = CommonCls.GenUniqueID();
                strUniqueID = txtUniqueID.Text;
                cboReturnType.Enabled = true;
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
                (txtBillNo.Text.Trim()),
                (dtBillDate.TextFormat(false, true)),
                ("(#OTHERNO#)"),
                (dtRefDate.TextFormat(false, true)),
                ((cboReturnType.SelectedValue)),
                ((cboSupplier.SelectedValue)),
                ((cboBroker.SelectedValue)),
                ((cboDepartment.SelectedValue)),
                ((cboTransport.SelectedValue)),
                (txtLrNo.Text.Trim()),
                (dtLrDate.TextFormat(false, true)),
                ((cboAcType.SelectedValue)),
                (Localization.ParseNativeDouble(TxtTotalPcs.Text.Replace(",", ""))),
                (Localization.ParseNativeDouble(TxtTotMtrs.Text.Replace(",", ""))),
                (Localization.ParseNativeDouble(TxtGrossAmount.Text.Replace(",", ""))),
                (Localization.ParseNativeDouble(txtAddLessAmt.Text.Replace(",", ""))),
                (Localization.ParseNativeDouble(txtNetAmt.Text.Replace(",", ""))),
                ((txtDescription.Text.Trim() == "" ? "-": txtDescription.Text.ToString())),
                (txtBrokerPercent.Text.ToString().Replace(",", "")),
                (txtBrokerTotalAmount.Text.ToString().Replace(",","")),
                (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                (dtEd1.TextFormat(false, true)),
                (txtET1.Text.Trim()),
                (txtET2.Text.Trim()),
                (txtET3.Text.Trim())
                };
                int UnitId = 0;
                string strAdjQry = (string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_AcLedger", "(#CodeID#)", Localization.ParseNativeInt(Conversions.ToString(base.iIDentity))) + string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", "(#CodeID#)", Localization.ParseNativeInt(Conversions.ToString(base.iIDentity))));
                strAdjQry = strAdjQry + string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_VatLedger", "(#CodeID#)", base.iIDentity);
                strAdjQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_BrokerLedger", "(#CodeID#)", base.iIDentity);
                strAdjQry = strAdjQry + DBSp.InsertInto_AcLedger("(#CodeID#)", "0", txtEntryNo.Text.ToString(), dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), Conversions.ToString(cboSupplier.SelectedValue), 1, Db_Detials.Ac_AdjType.NewRef, "(#CodeID#)", txtBillNo.Text.Trim(), dtBillDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDecimal(txtNetAmt.Text), 0, txtDescription.Text.Trim(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                strAdjQry = strAdjQry + DBSp.InsertInto_AcLedger("(#CodeID#)", "0", txtEntryNo.Text.ToString(), dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), Conversions.ToString(cboAcType.SelectedValue), 2, Db_Detials.Ac_AdjType.NewRef, "(#CodeID#)", txtBillNo.Text.Trim(), dtBillDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), 0, Localization.ParseNativeDecimal(TxtGrossAmount.Text), txtDescription.Text.Trim(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                DataGridViewEx ex = this.fgDtls_f;
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
                            if (dblDedAmt > 0.0)
                            {
                                strAdjQry = strAdjQry + DBSp.InsertInto_AcLedger("(#CodeID#)", Conversions.ToString((int)(i + 3)), "(#ENTRYNO#)", this.dtEntryDate.Text, (double)base.iIDentity,
                                            Conversions.ToString(Localization.ParseNativeInt(row.Cells[2].Value.ToString())), 2, Db_Detials.Ac_AdjType.OnAccount, "(#CodeID#)",
                                            this.txtBillNo.Text.Trim(), this.dtBillDate.Text, (double)base.iIDentity, decimal.Zero, new decimal(dblDedAmt), "null",
                                            Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, 0, DateAndTime.Now.Date);
                            }
                            else
                            {
                                string sDedAmt = dblDedAmt.ToString();
                                if (sDedAmt.StartsWith("-"))
                                {
                                    sDedAmt = sDedAmt.Substring(1);
                                }
                                dblDedAmt = Localization.ParseNativeDouble(sDedAmt.ToString());
                                strAdjQry = strAdjQry + DBSp.InsertInto_AcLedger("(#CodeID#)", Conversions.ToString((int)(i + 3)), "(#ENTRYNO#)", this.dtEntryDate.Text, (double)base.iIDentity,
                                            Conversions.ToString(Localization.ParseNativeInt(row.Cells[2].Value.ToString())), 1, Db_Detials.Ac_AdjType.OnAccount, "(#CodeID#)",
                                            this.txtBillNo.Text.Trim(), this.dtBillDate.Text, (double)base.iIDentity, new decimal(dblDedAmt), decimal.Zero, "null",
                                            Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, 0, DateAndTime.Now.Date);
                            }
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
                                strAdjQry = strAdjQry + DBSp.InsertInto_VatLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                             row2.Cells[2].Value.ToString(), Localization.ParseNativeInt(row2.Cells[3].Value.ToString()), Localization.ParseNativeDecimal(row2.Cells[4].Value.ToString()),
                                             "(#CodeID#)", 0, Localization.ParseNativeDecimal(row2.Cells[5].Value.ToString()), "null", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                            }
                            if (Operators.ConditionalCompareObjectEqual(row2.Cells[3].FormattedValue, "-", false))
                            {
                                strAdjQry = strAdjQry + DBSp.InsertInto_VatLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                         row2.Cells[2].Value.ToString(), Localization.ParseNativeInt(row2.Cells[3].Value.ToString()), Localization.ParseNativeDecimal(row2.Cells[4].Value.ToString()),
                                         "(#CodeID#)", Localization.ParseNativeDecimal(row2.Cells[5].Value.ToString()), 0, "null", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                            }
                        }
                    }
                }
                catch { }
                #endregion

                for (int j = 0; j <= fgDtls.RowCount - 1; j++)
                {
                    DataGridViewRow row2 = fgDtls.Rows[j];
                    if (Localization.ParseNativeDouble(row2.Cells[12].Value.ToString()) != 0.0)
                    {
                        strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                                "(#CodeID#)", (j + 1).ToString(), "(#ENTRYNO#)", dtBillDate.Text,
                                                Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()), row2.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[23].Value.ToString()),
                                                row2.Cells[41].Value == null ? "0" : row2.Cells[41].Value.ToString() == "" ? "0" : row2.Cells[41].Value.ToString() == "-" ? "0" : row2.Cells[41].Value.ToString(),
                                                row2.Cells[42].Value == null ? "0" : row2.Cells[42].Value.ToString() == "" ? "0" : row2.Cells[42].Value.ToString() == "-" ? "0" : row2.Cells[42].Value.ToString(),
                                                row2.Cells[2].Value == null ? "-" : row2.Cells[2].Value.ToString() == "" ? "-" : row2.Cells[2].Value.ToString() == "0" ? "-" : row2.Cells[2].Value.ToString(),
                                                row2.Cells[3].Value.ToString(),
                                                row2.Cells[6].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[6].Value.ToString()),
                                                Localization.ParseNativeDouble(row2.Cells[8].Value.ToString()),
                                                Localization.ParseNativeDouble(row2.Cells[7].Value.ToString()),
                                                Localization.ParseNativeDouble(row2.Cells[9].Value.ToString()),
                                                row2.Cells[10].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[10].Value.ToString()),
                                                Localization.ParseNativeDouble(row2.Cells[11].Value.ToString()),
                                                0, 0, 0,
                                                Localization.ParseNativeDecimal(row2.Cells[12].Value.ToString()),
                                                Localization.ParseNativeDecimal(row2.Cells[13].Value.ToString()),
                                                Localization.ParseNativeDecimal(row2.Cells[14].Value.ToString()),
                                                0, "null",
                                                row2.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[24].Value.ToString()),
                                                row2.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[25].Value.ToString()), row2.Cells[26].Value == null ? "0" : row2.Cells[26].Value.ToString(), 0, 0, 0,
                                                row2.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[30].Value.ToString()),
                                                row2.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[31].Value.ToString()),
                                                row2.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[32].Value.ToString()),
                                                row2.Cells[33].Value == null || row2.Cells[33].Value.ToString() == "" || row2.Cells[33].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row2.Cells[33].Value.ToString()),
                                                row2.Cells[34].Value == null || row2.Cells[34].Value.ToString() == "" || row2.Cells[34].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row2.Cells[34].Value.ToString()),
                                                row2.Cells[35].Value == null || row2.Cells[35].Value.ToString() == "" ? "-" : row2.Cells[35].Value.ToString(),
                                                row2.Cells[36].Value == null || row2.Cells[36].Value.ToString() == "" ? "-" : row2.Cells[36].Value.ToString(),
                                                row2.Cells[37].Value == null || row2.Cells[37].Value.ToString() == "" ? "-" : row2.Cells[37].Value.ToString(),
                                                row2.Cells[38].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[38].Value.ToString()),
                                                row2.Cells[39].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[39].Value.ToString()),
                                                "NULL", j, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                        UnitId = Localization.ParseNativeInt(row2.Cells[11].Value.ToString());
                        try
                        {
                            if (ENABLE_BROKER_CALCMETHOD2)
                            {
                                if (ENABLE_BROKER_FAB_PURCHASERETURN)
                                {
                                    if (row2.Cells[19].Value != null && row2.Cells[19].Value.ToString() != "" && row2.Cells[19].Value.ToString() != "0")
                                    {
                                        if (cboBroker.SelectedValue != null && cboBroker.SelectedValue.ToString() != "" && cboBroker.SelectedValue.ToString() != "0")
                                        {
                                            strAdjQry = strAdjQry + DBSp.InsertIntoBrokerLedger("(#CodeID#)", (j + 1).ToString(), "(#ENTRYNO#)", Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboBroker.SelectedValue.ToString()), row2.Cells[3].Value.ToString(), dtEntryDate.Text.ToString(), Localization.ParseNativeDouble(row2.Cells[11].Value.ToString()), Localization.ParseNativeDecimal(row2.Cells[18].Value.ToString()), 0, Localization.ParseNativeDecimal(row2.Cells[19].Value.ToString()), "-", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date, 0, 1);
                                        }
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                    row2 = null;
                }

                try
                {
                    if (ENABLE_BROKER_CALCMETHOD1)
                    {
                        if (ENABLE_BROKER_FAB_PURCHASERETURN)
                        {
                            if (txtBrokerTotalAmount.Text != null && txtBrokerTotalAmount.Text != "0.00")
                            {
                                if (cboBroker.SelectedValue != null && cboBroker.SelectedValue.ToString() != "" && cboBroker.SelectedValue.ToString() != "0")
                                {
                                    strAdjQry = strAdjQry + DBSp.InsertIntoBrokerLedger("(#CodeID#)", "0", "(#ENTRYNO#)", Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboBroker.SelectedValue.ToString()), "", dtEntryDate.Text.ToString(), 0, Localization.ParseNativeDecimal(txtBrokerPercent.Text.ToString()), 0, Localization.ParseNativeDecimal(txtBrokerTotalAmount.Text), "-", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date, 0, 0);
                                }
                            }
                        }
                    }
                }
                catch { }

                //if (cboTransport.SelectedValue != null && Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()) > 0.0)
                //{
                //    strAdjQry = strAdjQry + DBSp.InsertIntoTrasportLedger("(#CodeID#)", txtBillNo.Text.ToString(), dtBillDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()), Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()), Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()), txtLrNo.Text, dtLrDate.Text, null, UnitId, Conversions.ToInteger(TxtTotalPcs.Text), Conversions.ToDecimal(TxtTotMtrs.Text), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                //}
                strAdjQry += "Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";";
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                DBSp.Transcation_AddEdit(pArrayData, this.fgDtls, true, strAdjQry, "", txtEntryNo.Text, txtRefNo.Text, "RefNo", new DataGridViewEx[] { this.fgDtls_f });
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        #endregion

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

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dtRefDate.Text != "__/__/____")
                {
                    if ((cboSupplier.SelectedValue != null) && (cboSupplier.SelectedValue.ToString() != "") && (cboSupplier.SelectedValue.ToString() != "0"))
                    {
                        frmStockAdj frmStockAdj = new frmStockAdj();
                        frmStockAdj.MenuID = base.iIDentity;
                        frmStockAdj.Entity_IsfFtr = 0.0;
                        frmStockAdj.AsonDate = Conversions.ToDate(this.dtRefDate.Text);
                        frmStockAdj.LedgerID = Conversions.ToString(cboSupplier.SelectedValue);

                        #region StockAdjQuery
                        string strQry = string.Empty;
                        int ibitcol = 0;
                        string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", iIDentity, 0.0));
                        string strQry_ColName = "";
                        string[] arr = CommonCls.GetAdjColName(base.iIDentity, 0.0).Split(';');
                        strQry_ColName = arr[0].ToString();
                        ibitcol = Localization.ParseNativeInt(arr[1]);
                        // strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) Where BalMeters > 0 Order by MyId ", new object[] { strQry_ColName, snglValue, CommonLogic.SQuote(Localization.ToSqlDateString(dtChlnDate.Text)), Db_Detials.CompID, Db_Detials.YearID, this.cboDepartment.SelectedValue });
                        #endregion

                        try
                        {
                            if (cboReturnType.SelectedItem.ToString() == "[1, Before Bill]")
                            {
                                strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}, {6}, {7}) Where BillNo IS NULL or BillNo=0 ", new object[] { strQry_ColName, snglValue, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(Conversions.ToDate(this.dtRefDate.Text)))), Db_Detials.StoreID, Db_Detials.CompID,Db_Detials.BranchID, Db_Detials.YearID, cboSupplier.SelectedValue });
                            }
                            else if (cboReturnType.SelectedItem.ToString() == "[2, After Bill]")
                            {
                                strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}, {6}, {7}) Where BillNo IS NOT NULL and BillNO <> 0", new object[] { strQry_ColName, snglValue, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(Conversions.ToDate(this.dtRefDate.Text)))), Db_Detials.StoreID, Db_Detials.CompID,Db_Detials.BranchID, Db_Detials.YearID, cboSupplier.SelectedValue });
                            }
                            else if (cboReturnType.SelectedItem.ToString() == "[3, Other]")
                            {
                                strQry = string.Format("Select BarcodeNo As [BarcodeNo],  CAST('False' as Bit) As [Sel], 0 As [FabricInwardID], 0 As  [FabPurchaseID] , 0 as [SupplierID] , DepartmentName As [Department], FabricQualityID As [QualityID], FabricQualityName As [Quality], FabricDesignID As [FabricDesignID], FabricDesignName As [Design],FabricShadeID As [ShadeID], FabricShadeName As [Shade],UnitID As [unit], UnitName As [Unit], BalQty As [BalQty], BalMeters As [BalMeters], BalWeight As [BalWeight], 0 as [Rate], BatchNo As [BatchNo], ARefID As [RefID], GradeID As [GradeID],SubDepartmentID AS [SubDepartmentID],SubDepartmentName as [StoreLocation] From {0} ( {1}, {2}, {3}, {4}, {5}, {6}) Where BalMeters > 0 Order by MyId ", new object[] { "fn_FetchFabricStock", DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(Conversions.ToDate(this.dtRefDate.Text)))), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboDepartment.SelectedValue });
                                frmStockAdj.LedgerID = Conversions.ToString(cboDepartment.SelectedValue);
                            }
                            else
                            {
                                frmStockAdj.ReturnType = "0";
                            }
                        }
                        catch { frmStockAdj.ReturnType = "0"; }

                        CIS_ReportTool.frmMultiPrint.VoucherTypeID = 0;
                        frmStockAdj.ref_fgDtls = this.fgDtls;
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
                            this.fgDtls.Select();
                            frmStockAdj = null;
                        }
                        if (fgDtls.RowCount > 1)
                        {
                            cboReturnType.Enabled = false;
                        }
                    }
                    else
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Supplier to Fetch Stock");
                        cboSupplier.Focus();
                    }
                }
                else
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Question, "", "Please Enter Challan Date to Fetch Stock");
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
            fgDtls.Select();
            setTempRowIndex();
            setMyID();
            ExecuterTempQry(-1);
        }

        private void CalcVal()
        {
            try
            {
                TxtTotalPcs.Text = string.Format("{0:N0}", CommonCls.GetColSum(this.fgDtls, 12, -1, -1));
                TxtTotMtrs.Text = string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 13, -1, -1));
                TxtGrossAmount.Text = string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 16, -1, -1));
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
            }

            //string sIsQualityWise = DB.GetSnglValue(string.Format("select IsQualityWise from tbl_BrokerLedger where TransId={0} and TransType={1}", txtCode.Text, base.iIDentity.ToString()));
            //if (base.blnFormAction == Enum_Define.ActionType.View_Record)
            //{
            //    if (sIsQualityWise == "1")
            //    {
            //        if (ENABLE_BROKER_CALCMETHOD2)
            //        {
            //            if (ENABLE_BROKER_FAB_PURCHASERETURN)
            //            {
            //                txtBrokerTotalAmount.Text = string.Format("{0:N2}", Math.Round(CommonCls.GetColSum(this.fgDtls, 17, -1, -1)));
            //            }
            //        }
            //    }
            //}
            if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
            {
                if (ENABLE_BROKER_CALCMETHOD2)
                {
                    if (ENABLE_BROKER_FAB_PURCHASERETURN)
                    {
                        txtBrokerTotalAmount.Text = string.Format("{0:N2}", Math.Round(CommonCls.GetColSum(this.fgDtls, 19, -1, -1)));
                    }
                }
            }
            double dblDedAmt = 0.0;
            DataGridViewEx ex = this.fgDtls_f;
            for (int i = 0; i <= ex.RowCount - 1; i++)
            {
                if (ex.Rows[i].Cells[3].Value != null)
                {
                    if (Operators.ConditionalCompareObjectEqual(ex.Rows[i].Cells[3].FormattedValue, "-", false))
                    {
                        dblDedAmt -= Localization.ParseNativeDouble(ex.Rows[i].Cells[5].Value.ToString());
                    }
                    else if (Operators.ConditionalCompareObjectEqual(ex.Rows[i].Cells[3].FormattedValue, "+", false))
                    {
                        dblDedAmt += Localization.ParseNativeDouble(ex.Rows[i].Cells[5].Value.ToString());
                    }
                }
            }
            ex = null;
            txtAddLessAmt.Text = string.Format("{0:N2}", Math.Round(dblDedAmt));
            txtNetAmt.Text = string.Format("{0:N2}", Math.Round((double)(Convert.ToDouble(Localization.ParseNativeDecimal(TxtGrossAmount.Text)) + dblDedAmt)));
        }

        private void cboReturnType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    if (cboReturnType.SelectedItem.ToString() != null)
                    {
                        if (cboReturnType.SelectedItem.ToString() == "[3, Other]")
                        {
                            fgDtls.Rows[i].Cells[4].ReadOnly = false;
                            fgDtls.Rows[i].Cells[5].ReadOnly = false;
                            fgDtls.Rows[i].Cells[6].ReadOnly = false;
                            fgDtls.Rows[i].Cells[7].ReadOnly = false;
                            fgDtls.Rows[i].Cells[8].ReadOnly = false;
                            fgDtls.Rows[i].Cells[9].ReadOnly = false;
                            fgDtls.Rows[i].Cells[10].ReadOnly = false;
                            fgDtls.Rows[i].Cells[11].ReadOnly = false;
                            fgDtls.Rows[i].Cells[4].Value = "0";
                            fgDtls.Rows[i].Cells[5].Value = "0";
                            txtScan.Enabled = true;
                        }
                        else
                        {
                            fgDtls.Rows[i].Cells[4].ReadOnly = true;
                            fgDtls.Rows[i].Cells[5].ReadOnly = true;
                            fgDtls.Rows[i].Cells[6].ReadOnly = true;
                            fgDtls.Rows[i].Cells[7].ReadOnly = true;
                            fgDtls.Rows[i].Cells[8].ReadOnly = true;
                            fgDtls.Rows[i].Cells[9].ReadOnly = true;
                            fgDtls.Rows[i].Cells[10].ReadOnly = true;
                            fgDtls.Rows[i].Cells[11].ReadOnly = true;
                            txtScan.Enabled = false;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboSupplier_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if ((cboSupplier.SelectedValue != null) && (Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString())) > 0.0)
                {
                    cboBroker.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select BrokerID From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboSupplier.SelectedValue)));
                    cboTransport.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select TransportId From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboSupplier.SelectedValue)));
                    cboAcType.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select PurchSalesID From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboSupplier.SelectedValue)));
                }
            }
            catch (Exception ex)
            { Navigate.logError(ex.Message, ex.StackTrace); }
        }

        private void fgDtls_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (cboReturnType.SelectedItem != null)
                    {
                        if (cboReturnType.SelectedItem.ToString() == "[3, Other]")
                        {
                            if (fgDtls.Rows.Count > 1)
                            {
                                fgDtls.Rows[e.RowIndex].Cells[4].Value = "0";
                                fgDtls.Rows[e.RowIndex].Cells[5].Value = "0";
                                fgDtls.Rows[e.RowIndex].Cells[40].Value = "0";
                                fgDtls.Rows[e.RowIndex].Cells[41].Value = "0";
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

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                #region commented
                //if ((Localization.ParseNativeInt(base.blnFormAction.ToString()) == 4) || (Localization.ParseNativeInt(base.blnFormAction.ToString()) == 5))
                //{
                //    return;
                //}
                //switch (e.ColumnIndex)
                //{
                //    case 6:
                //        {
                //            if (fgDtls.Rows[e.RowIndex].Cells[3].Value == null)
                //                fgDtls.Rows[e.RowIndex].Cells[3].Value = "";

                //            if ((fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString() == "") && (fgDtls.Rows[e.RowIndex].Cells[4].Value != null) && (fgDtls.Rows[e.RowIndex].Cells[4].Value != null) && (fgDtls.Rows[e.RowIndex].Cells[5].Value != null) && (fgDtls.Rows[e.RowIndex].Cells[6].Value != null))
                //            {
                //                for (int i = 0; i <= (fgDtls_f.Rows.Count - 1); i++)
                //                {
                //                    if (Operators.ConditionalCompareObjectEqual(fgDtls.Rows[e.RowIndex].Cells[4].Value, fgdtls_f.Rows[i].Cells[4].Value, false))
                //                    {
                //                        if (Operators.ConditionalCompareObjectEqual(fgDtls.Rows[e.RowIndex].Cells[5].Value, fgdtls_f.Rows[i].Cells[5].Value, false))
                //                        {
                //                            if (Operators.ConditionalCompareObjectEqual(fgDtls.Rows[e.RowIndex].Cells[6].Value, fgdtls_f.Rows[i].Cells[7].Value, false))
                //                            {
                //                                if (Operators.ConditionalCompareObjectGreater(fgdtls_f.Rows[i].Cells[12].Value, 0, false))
                //                                {
                //                                    flg_OrderConform = true;
                //                                    fgDtls.Rows[e.RowIndex].Cells[3].Value = fgdtls_f.Rows[i].Cells[0].Value;
                //                                    fgdtls_f.Rows[i].Cells[11].Value = Localization.ParseNativeInt(fgdtls_f.Rows[i].Cells[11].Value.ToString()) + 1;
                //                                    fgdtls_f.Rows[i].Cells[12].Value = (Localization.ParseNativeDouble(fgdtls_f.Rows[i].Cells[9].Value.ToString()) - Localization.ParseNativeDouble(this.fgdtls_f.Rows[i].Cells[10].Value.ToString())) - Localization.ParseNativeDouble(fgdtls_f.Rows[i].Cells[11].Value.ToString());

                //                                    if (Localization.ParseNativeInt(cboSeries.SelectedValue.ToString()) > 0)
                //                                    {
                //                                        try
                //                                        {
                //                                            if (cboSeries.Text == "READY_CURTANS")
                //                                            {
                //                                                fgDtls.Rows[e.RowIndex].Cells[13].Value = Localization.ParseNativeDecimal(fgdtls_f.Rows[i].Cells[14].Value.ToString());
                //                                                fgDtls.Rows[e.RowIndex].Cells[14].Value = Localization.ParseNativeDecimal(fgDtls.Rows[e.RowIndex].Cells[12].Value.ToString()) * Localization.ParseNativeDecimal(fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString());
                //                                                fgDtls.Rows[e.RowIndex].Cells[14].ReadOnly = true;
                //                                            }
                //                                            else
                //                                            {
                //                                                fgDtls.Rows[e.RowIndex].Cells[13].Value = 0.0;
                //                                                fgDtls.Rows[e.RowIndex].Cells[14].ReadOnly = false;
                //                                            }
                //                                        }
                //                                        catch { }
                //                                    }

                //                                    fgDtls.Rows[e.RowIndex].Cells[41].Value = Localization.ParseNativeDecimal(fgdtls_f.Rows[i].Cells[13].Value.ToString());

                //                                    break;
                //                                }
                //                                else
                //                                {
                //                                    flg_OrderConform = false;
                //                                }
                //                            }
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //        break;
                //    default:
                //        break;
                //}
                //if (txtScan.Text.Trim().Length <= 0)
                //{
                //    fgDtls.Rows[e.RowIndex].Cells[9].Value = fgDtls.Rows[e.RowIndex].Cells[6].Value;
                //}
                #endregion

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
                DataGridViewEx ex = this.fgDtls_f;
                if (!((base.blnFormAction == Enum_Define.ActionType.View_Record) | (base.blnFormAction == Enum_Define.ActionType.Not_Active)))
                {
                    if (((e.ColumnIndex == 13) | (e.ColumnIndex == 15)) | (e.ColumnIndex == 16))
                    {
                        CalcVal();
                    }

                    if (e.ColumnIndex == 7 || e.ColumnIndex == 8 || e.ColumnIndex == 9)
                    {
                        for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                        {
                            if (fgDtls.Rows[i].Cells[7].Value != null && fgDtls.Rows[i].Cells[7].Value.ToString() != "" && fgDtls.Rows[i].Cells[8].Value != null && fgDtls.Rows[i].Cells[8].Value.ToString() != "" && fgDtls.Rows[i].Cells[9].Value != null && fgDtls.Rows[i].Cells[9].Value.ToString() != "")
                            {
                                fgDtls.Rows[i].Cells[6].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricID from fn_FabricMaster_Tbl() where DesignID={0} and QualityID={1} and ShadeID={2}", fgDtls.Rows[i].Cells[7].Value, fgDtls.Rows[i].Cells[8].Value, fgDtls.Rows[i].Cells[9].Value)));
                            }
                        }
                    }
                    switch (e.ColumnIndex)
                    {
                        case 8:
                            fgDtls.Rows[e.RowIndex].Cells[11].Value = fgDtls.Rows[e.RowIndex].Cells[8].Value;
                            if (ENABLE_BROKER_CALCMETHOD2)
                            {
                                if (ENABLE_BROKER_FAB_PURCHASERETURN)
                                {
                                    if (cboBroker.SelectedValue != null && cboBroker.SelectedValue.ToString() != "" && cboBroker.SelectedValue.ToString() != "0")
                                    {
                                        SBrokersPerc = DB.GetSnglValue(string.Format("SELECT percentage from tbl_BrokerPercentDtls a left join tbl_BrokerPercentMain B on A.BrokerPercentID=b.BrokerPercentID where b.BrokerID={0} and a.QualityID={1}", cboBroker.SelectedValue, fgDtls.Rows[e.RowIndex].Cells[8].Value));
                                        fgDtls.Rows[e.RowIndex].Cells[18].Value = SBrokersPerc;

                                        if (fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value.ToString() != "0" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value.ToString() != "0")
                                        {
                                            decimal dbrokertotalamt_gridrow = (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value.ToString()) / 100) * (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[41].Value.ToString()));
                                            fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[19].Value = dbrokertotalamt_gridrow;
                                        }
                                        else
                                        {
                                            fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[19].Value = 0;
                                        }
                                    }
                                }
                            }
                            CalcVal();
                            break;

                        case 13:
                            if (fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != "0")
                            {
                                fgDtls.Rows[e.RowIndex].Cells[16].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString())).ToString()));
                                for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                                {
                                    if (ex.Rows[i].Cells[4].Value != null && ex.Rows[i].Cells[4].Value.ToString() != "0")
                                    {
                                        ex.Rows[i].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[i].Cells[4].Value))), 100M).ToString().Replace(",", "");
                                        CalcVal();
                                    }
                                }
                            }
                            break;

                        case 15:
                            if (ENABLE_BROKER_CALCMETHOD2)
                            {
                                if (ENABLE_BROKER_FAB_PURCHASERETURN && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value.ToString() != "0" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value.ToString() != "0")
                                {
                                    decimal dbrokertotalamt_gridrow = (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value.ToString()) / 100) * (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value.ToString()));
                                    fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[19].Value = dbrokertotalamt_gridrow;
                                }
                                else
                                {
                                    fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[19].Value = 0;
                                }
                            }
                            if (fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != "0")
                            {
                                fgDtls.Rows[e.RowIndex].Cells[16].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString())).ToString()));
                                for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                                {
                                    if (ex.Rows[i].Cells[4].Value != null && ex.Rows[i].Cells[4].Value.ToString() != "0")
                                    {
                                        ex.Rows[i].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[i].Cells[4].Value))), 100M).ToString().Replace(",", "");
                                        CalcVal();
                                    }
                                }
                            }
                            CalcVal();
                            break;

                        case 20:
                            if (ENABLE_BROKER_CALCMETHOD2)
                            {
                                if (ENABLE_BROKER_FAB_PURCHASERETURN && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value.ToString() != "0" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value.ToString() != "0")
                                {
                                    decimal dbrokertotalamt_gridrow = (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value.ToString()) / 100) * (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[16].Value.ToString()));
                                    fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[19].Value = dbrokertotalamt_gridrow;
                                }
                                else
                                {
                                    fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[19].Value = 0;
                                }
                            }
                            CalcVal();
                            break;
                    }
                }
                CalcVal();
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                CalcVal();
            }
        }

        #region CellandEdit

        //private void fgDtls_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        //{
        //    try
        //    {
        //        if (e.ColumnIndex == 7 || e.ColumnIndex == 12)
        //        {
        //            if (fgDtls.Rows[e.RowIndex].Cells[7].Value != null || fgDtls.Rows[e.RowIndex].Cells[8].Value != null || fgDtls.Rows[e.RowIndex].Cells[9].Value != null)
        //            {
        //                decimal dcm = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select Sum(IsNull(Dr_Qty,0)-IsNull(Cr_Qty,0)) As BalQty From fn_StockFabricLedger() Where BatchNo='" + fgDtls.Rows[e.RowIndex].Cells[4].Value + "' and FabricQualityID=" + fgDtls.Rows[e.RowIndex].Cells[8].Value + " and FabricDesignID=" + fgDtls.Rows[e.RowIndex].Cells[7].Value + " and FabricShadeID=" + fgDtls.Rows[e.RowIndex].Cells[9].Value + " and CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "")));
        //                if (dcm == 0)
        //                {
        //                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "There Are No Balance Stock For this Piece No ");
        //                }
        //            }
        //        }
        //    }
        //    catch { }
        //}

        #endregion

        private void fgDtls_f_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (!((base.blnFormAction == Enum_Define.ActionType.View_Record) || (base.blnFormAction == Enum_Define.ActionType.Not_Active)))
                {
                    DataGridViewEx ex = this.fgDtls_f;
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
                                        ex.Rows[ex.CurrentRow.Index].Cells[5].Value = ((Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(reader["Percentage"].ToString())) / 100);
                                    }
                                }
                            }
                            break;

                        case 4:
                            ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                            break;

                        case 5:
                            ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(TxtGrossAmount.Text)) * 100);
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
                            if (blnFormAction == Enum_Define.ActionType.New_Record)
                            {
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
                            CalcVal();
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
                }
            }
            catch (Exception ex1) { Navigate.logError(ex1.Message, ex1.StackTrace); }

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

            catch (Exception ex2)
            {
                Navigate.logError(ex2.Message, ex2.StackTrace);
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

        public void PrintRecord()
        {
            try
            {
                CIS_ReportTool.frmMultiPrint frmMultiPrint = new CIS_ReportTool.frmMultiPrint();
                CIS_ReportTool.frmMultiPrint.MenuID = base.iIDentity;
                CIS_ReportTool.frmMultiPrint.Id = Localization.ParseNativeInt(txtCode.Text);
                CIS_ReportTool.frmMultiPrint.TblNm = "tbl_FabricPurchaseReturnMain";
                CIS_ReportTool.frmMultiPrint.IdStr = "FabPurRtnID";
                CIS_ReportTool.frmMultiPrint.iCompID = Db_Detials.CompID;
                CIS_ReportTool.frmMultiPrint.iYearID = Db_Detials.YearID;
                CIS_ReportTool.frmMultiPrint.iBranchID = Db_Detials.BranchID;
                CIS_ReportTool.frmMultiPrint.iStoreID = Db_Detials.StoreID;
                CIS_ReportTool.frmMultiPrint.iUserID = Db_Detials.UserID;
                CIS_ReportTool.frmMultiPrint.objReport = Db_Detials.objReport;
                CIS_ReportTool.frmMultiPrint.VoucherTypeID = 0;
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

        #region Scan

        private void txtScan_Validated(object sender, EventArgs e)
        {
            try
            {
                //bool flag = false;
                bool isblankrecord = false;
                if (((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record)) && ((this.txtScan.Text != null) && (this.txtScan.Text != "")))
                {
                    if ((cboDepartment.SelectedValue != null) && (dtRefDate.Text.Trim() != "__/__/____"))
                    {
                        if ((txtScan.Text != null) && !string.IsNullOrEmpty(txtScan.Text) && (Localization.ParseNativeInt(cboDepartment.SelectedValue.ToString()) > 0))
                        {
                            for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                            {
                                DataGridViewRow row = fgDtls.Rows[i];
                                if (row.Cells[3].Value != null)
                                {
                                    if (row.Cells[3].Value.ToString().ToUpper() == txtScan.Text.ToUpper())
                                    {
                                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "This Piece No is already Selected");
                                        txtScan.Text = "";
                                        txtScan.Focus();
                                        return;
                                    }
                                }
                                row = null;
                            }

                            using (IDataReader reader = DB.GetRS(string.Format("Select * from {0}({1},{2},{3},{4},{5},{6}) Where BarCodeNo='{7}'", new object[] { "fn_FetchFabricStock", DB.SQuoteNotUnicode(Localization.ToSqlDateString(dtRefDate.Text)), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboDepartment.SelectedValue, txtScan.Text.Trim() })))
                            {
                                while (reader.Read())
                                {
                                    isblankrecord = true;
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[3].Value = reader["BarCodeNo"].ToString();
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[4].Value = 0;
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[4].Value = 0;
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[5].Value = 0;
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[6].Value = 0;
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[7].Value = Localization.ParseNativeInt(reader["FabricDesignID"].ToString());
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[8].Value = Localization.ParseNativeInt(reader["FabricQualityID"].ToString());
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[9].Value = Localization.ParseNativeInt(reader["FabricShadeID"].ToString());
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[10].Value = Localization.ParseNativeInt(reader["GradeID"].ToString());
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[11].Value = Localization.ParseNativeInt(reader["UnitID"].ToString());
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[12].Value = Localization.ParseNativeDecimal(reader["BalQty"].ToString());
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[13].Value = Localization.ParseNativeDecimal(reader["BalMeters"].ToString());
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[14].Value = 0;
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[15].Value = 0;
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[2].Value = reader["BatchNo"].ToString();
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[41].Value = (reader["ARefID"].ToString());
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[23].Value = (reader["SubDepartmentID"].ToString());
                                }
                            }
                            if (isblankrecord == false)
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "No Records Found");
                            }
                            else
                            {
                                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                                //fgDtls.Rows.Insert(fgDtls.RowCount + 1, new DataGridViewRow());          
                            }
                            fgDtls.CurrentCell = fgDtls[1, fgDtls.RowCount - 1];
                            //DataGridViewEx ex2 = this.fgDtls;
                            //EventHandles.CreateDefault_Rows(ex2, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                            //this.fgDtls = ex2;

                            txtScan.Text = "";
                            txtScan.Focus();

                            if (isblankrecord == true)
                            {
                                setTempRowIndex();
                                setMyID();
                                ExecuterTempQry(-1);
                            }
                        }
                    }
                    else
                    {
                        if (txtScan.Text.Trim().Length > 0)
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Plese Select From Department and Enter Challan Details");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
            return;
        }

        private void txtBrokerPercent_Leave(object sender, EventArgs e)
        {
            try
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (ENABLE_BROKER_FAB_PURCHASERETURN)
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

        #endregion

        public void ExecuterTempQry(int RowIndex)
        {
            if (cboReturnType.SelectedItem.ToString() == "[3, Other]")
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
                                strQry = string.Format("Delete From tbl_StockFabricLedger Where Dr_Qty=0 and Dr_Mtrs=0 and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
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
                                        StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockFabricLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockFabricLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + "")));
                                        MyID = txtCode.Text;
                                    }

                                    if (MyID != "" && fgDtls.Rows[i].Cells[12].Value != null && fgDtls.Rows[i].Cells[12].Value.ToString() != "" && fgDtls.Rows[i].Cells[12].Value.ToString() != "0")
                                    {
                                        strQry = strQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                               MyID, (i + 1).ToString(), txtEntryNo.Text, dtBillDate.Text,
                                               Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()), row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                               row.Cells[41].Value == null ? "0" : row.Cells[41].Value.ToString() == "" ? "0" : row.Cells[41].Value.ToString() == "-" ? "0" : row.Cells[41].Value.ToString(),
                                               row.Cells[42].Value == null ? "0" : row.Cells[42].Value.ToString() == "" ? "0" : row.Cells[42].Value.ToString() == "-" ? "0" : row.Cells[42].Value.ToString(),
                                               row.Cells[2].Value == null ? "-" : row.Cells[2].Value.ToString() == "" ? "-" : row.Cells[2].Value.ToString() == "0" ? "-" : row.Cells[2].Value.ToString(),
                                               row.Cells[3].Value.ToString(),
                                               row.Cells[6].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[6].Value.ToString()),
                                               Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                               Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                               Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                               row.Cells[10].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[10].Value.ToString()),
                                               Localization.ParseNativeDouble(row.Cells[11].Value.ToString()),
                                               0, 0, 0,
                                               Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()),
                                               Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()),
                                               Localization.ParseNativeDecimal(row.Cells[14].Value.ToString()),
                                               0, "null",
                                               row.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                               row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()), row.Cells[26].Value == null ? "0" : row.Cells[26].Value.ToString(), 0, 0, 0,
                                               row.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[30].Value.ToString()),
                                               row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                               row.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[32].Value.ToString()),
                                               row.Cells[33].Value == null || row.Cells[33].Value.ToString() == "" || row.Cells[33].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[33].Value.ToString()),
                                               row.Cells[34].Value == null || row.Cells[34].Value.ToString() == "" || row.Cells[34].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[34].Value.ToString()),
                                               row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" ? "-" : row.Cells[35].Value.ToString(),
                                               row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" ? "-" : row.Cells[36].Value.ToString(),
                                               row.Cells[37].Value == null || row.Cells[37].Value.ToString() == "" ? "-" : row.Cells[37].Value.ToString(),
                                               row.Cells[38].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[38].Value.ToString()),
                                               row.Cells[39].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[39].Value.ToString()),
                                               txtUniqueID.Text, i, StatusID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                                    }
                                }
                            }
                            else
                            {
                                if ((fgDtls.CurrentCell.ColumnIndex == 13) || (fgDtls.CurrentCell.ColumnIndex == 12) || (fgDtls.CurrentCell.ColumnIndex == 14))
                                {
                                    DataGridViewRow row = fgDtls.Rows[RowIndex];
                                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                                    {
                                        StatusID = 1;
                                        MyID = iMaxMyID.ToString();
                                    }
                                    else
                                    {
                                        StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockFabricLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockFabricLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + "")));
                                        MyID = txtCode.Text;
                                    }

                                    if (MyID != "" && row.Cells[12].Value != null && row.Cells[12].Value.ToString() != "" && row.Cells[12].Value.ToString() != "0")
                                    {
                                        if (txtUniqueID.Text != null)
                                        {
                                            strQry += string.Format("Delete From tbl_StockFabricLedger Where Dr_Qty=0 and Dr_Mtrs=0 and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[44].Value.ToString()) + " and CUserId=" + Db_Detials.UserID + ";");

                                            strQry = strQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                                   MyID, (RowIndex + 1).ToString(), txtEntryNo.Text, dtBillDate.Text,
                                                   Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()), row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                                   row.Cells[41].Value == null ? "0" : row.Cells[41].Value.ToString() == "" ? "0" : row.Cells[41].Value.ToString() == "-" ? "0" : row.Cells[41].Value.ToString(),
                                                   row.Cells[42].Value == null ? "0" : row.Cells[42].Value.ToString() == "" ? "0" : row.Cells[42].Value.ToString() == "-" ? "0" : row.Cells[42].Value.ToString(),
                                                   row.Cells[2].Value == null ? "-" : row.Cells[2].Value.ToString() == "" ? "-" : row.Cells[2].Value.ToString() == "0" ? "-" : row.Cells[2].Value.ToString(),
                                                   row.Cells[3].Value.ToString(),
                                                   row.Cells[6].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[6].Value.ToString()),
                                                   Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                                   Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                                   Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                                   row.Cells[10].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[10].Value.ToString()),
                                                   Localization.ParseNativeDouble(row.Cells[11].Value.ToString()),
                                                   0, 0, 0,
                                                   Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()),
                                                   Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()),
                                                   Localization.ParseNativeDecimal(row.Cells[14].Value.ToString()),
                                                   0, "null",
                                                   row.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                                   row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()), row.Cells[26].Value == null ? "0" : row.Cells[26].Value.ToString(), 0, 0, 0,
                                                   row.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[30].Value.ToString()),
                                                   row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                                   row.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[32].Value.ToString()),
                                                   row.Cells[33].Value == null || row.Cells[33].Value.ToString() == "" || row.Cells[33].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[33].Value.ToString()),
                                                   row.Cells[34].Value == null || row.Cells[34].Value.ToString() == "" || row.Cells[34].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[34].Value.ToString()),
                                                   row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" ? "-" : row.Cells[35].Value.ToString(),
                                                   row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" ? "-" : row.Cells[36].Value.ToString(),
                                                   row.Cells[37].Value == null || row.Cells[37].Value.ToString() == "" ? "-" : row.Cells[37].Value.ToString(),
                                                   row.Cells[38].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[38].Value.ToString()),
                                                   row.Cells[39].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[39].Value.ToString()),
                                                   txtUniqueID.Text, Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[44].Value.ToString()), StatusID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
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
                            if ((Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select COUNT(0) From fn_StockFabricLedger_tbl() Where RefId='" + fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[40].Value + "' and RefID<>'' and Transtype<>" + iIDentity + ""))) > 0))
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityWarning, "", "Reference Found In Another Module..Row Cannot Be Deleted");
                                return;
                            }
                            else
                            {
                                try
                                {
                                    string strQry = string.Format("Update tbl_StockFabricLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[44].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
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
                                string strQry = string.Format("Update tbl_StockFabricLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[44].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
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
                fgDtls.Rows[i].Cells[44].Value = i;
            }
        }

        private void frmFabricPurchaseReturn_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (strUniqueID != null)
            {
                string strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and CUserId=" + Db_Detials.UserID + ";");
                strQry = strQry + string.Format("Update  tbl_StockFabricLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                DB.ExecuteSQL(strQry);
                strQry = string.Format("Update tbl_StockFabricLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                DB.ExecuteSQL(strQry);
            }
        }

        private void setMyID()
        {
            iMaxMyID = Localization.ParseNativeInt(DB.GetSnglValue("Select MAX(MyId + 1) from tbl_StockFabricLedger Where IsDeleted=0"));

            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[43].Value = iMaxMyID;
            }
        }
    }
}
