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
using CIS_DBLayer;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class frmFabricOutwardReturn : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        public DataGridViewEx fgDtls_f;
        public DataGridViewEx fgDtls_f_footer;

        ArrayList OrgInGridArray = new ArrayList();
        bool FDC_ORD_COMP = false;
        public bool RTN_FM_STK;
        private bool ENABLE_BROKER_FAB_SALESBILLRETURN;
        private bool ENABLE_BROKER_CALCMETHOD1;
        private bool ENABLE_BROKER_CALCMETHOD2;
        string SBrokersPerc = string.Empty;

        public frmFabricOutwardReturn()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;

            fgDtls_f = GrdFooter.fgDtls;
            fgDtls_f_footer = GrdFooter.fgDtls_f;
        }

        #region Event

        private void frmFabricOutwardReturn_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                ENABLE_BROKER_FAB_SALESBILLRETURN = Localization.ParseBoolean(GlobalVariables.ENABLE_BROKER_FAB_SALESBILLRETURN);
                ENABLE_BROKER_CALCMETHOD1 = Localization.ParseBoolean(GlobalVariables.ENABLE_BROKER_CALCMETHOD1);
                ENABLE_BROKER_CALCMETHOD2 = Localization.ParseBoolean(GlobalVariables.ENABLE_BROKER_CALCMETHOD2);
                Combobox_Setup.FillCbo(ref cboDepartment, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboParty, Combobox_Setup.ComboType.Mst_Customer, "");
                Combobox_Setup.FillCbo(ref cboBroker, Combobox_Setup.ComboType.Mst_Brokers, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter);
                Combobox_Setup.FillCbo(ref cboSalesReturn, Combobox_Setup.ComboType.SalesAc, "");
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
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls_f, fgDtls_f_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 1, true);

                txtEntryNo.Enabled = false;

                if (ENABLE_BROKER_FAB_SALESBILLRETURN)
                {
                    if (ENABLE_BROKER_CALCMETHOD2)
                    {
                        fgDtls.Columns[22].Visible = true;
                        fgDtls.Columns[23].Visible = true;
                        txtBrokerPercent.Enabled = false;
                    }
                    else
                    {
                        fgDtls.Columns[22].Visible = false;
                        fgDtls.Columns[23].Visible = false;
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
                    fgDtls.Columns[22].Visible = false;
                    fgDtls.Columns[23].Visible = false;
                }

                this.RTN_FM_STK = Localization.ParseBoolean(GlobalVariables.RTN_FM_STK);
                this.txtBrokerPercent.Leave += new EventHandler(this.txtBrokerPercent_Leave);

                this.fgDtls.CellEndEdit += new DataGridViewCellEventHandler(this.fgDtls_CellEndEdit);
                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls.RowsAdded += new DataGridViewRowsAddedEventHandler(this.fgDtls_RowsAdded);

                this.fgDtls_f.CellParsing += new DataGridViewCellParsingEventHandler(this.fgDtls_f_CellParsing);
                this.fgDtls_f.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_f_CellValueChanged);
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
                DBValue.Return_DBValue(this, txtCode, "FabOutReturnID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtChlnNo, "RefNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtChlnDate, "RefDate", Enum_Define.ValidationType.IsDate);

                int i = Localization.ParseNativeInt(DB.GetSnglValue("select ReturnTypeID from tbl_FabricOutwardReturnMain where FabOutReturnID='" + txtCode.Text + "'and IsDeleted=0"));
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

                DBValue.Return_DBValue(this, cboParty, "PartyID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDepartment, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLrNo, "LrNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtLrDate, "LrDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboSalesReturn, "SalesReturnID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, TxtGrossAmount, "GrossAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAddLessAmt, "AddLessAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtNetAmt, "NetAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtBrokerPercent, "BrokerAvgPercentage", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtBrokerTotalAmount, "BrokerTotalAmount", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEd1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);
                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "FabOutReturnID", txtCode.Text, base.dt_HasDtls_Grd, this.iIDentity, GlobalVariables.VALIDATE_EDIT, Db_Detials.CompID.ToString(), 1);
                DetailGrid_Setup.FillGrid(fgDtls_f, this.fgDtls_f.Grid_UID, this.fgDtls_f.Grid_Tbl, "FabOutReturnID", txtCode.Text, base.dt_HasDtls_Grd);

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record || base.blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CreateDefault_Rows(fgDtls_f, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);

                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                    EventHandles.CalculateFooter_Rows(fgDtls_f, fgDtls_f_footer, fgDtls_f.Grid_ID.ToString(), fgDtls_f.Grid_UID);
                }

                AplySelectBtnEnbl();
                CalcVal();
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
                CommonCls.IncFieldID(this, ref txtChlnNo, "");
                this.txtEntryNo = txtEntryNo;

                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CreateDefault_Rows(fgDtls_f, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);

                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                EventHandles.CalculateFooter_Rows(fgDtls_f, fgDtls_f_footer, fgDtls_f.Grid_ID.ToString(), fgDtls_f.Grid_UID);

                int MaxID = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(FabOutReturnID),0) From {0} where IsDeleted=0 AND StoreID={1} and CompID={2} and BranchID={3} and YearID={4} ", "tbl_FabricOutwardReturnMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID))));

                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where IsDeleted=0 AND FabOutReturnID = {1} and StoreID={2} and CompID={3} and BranchID={4} and YearID={5}", new object[] { "tbl_FabricOutwardReturnMain", MaxID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                        dtChlnDate.Text = Localization.ToVBDateString(reader["RefDate"].ToString());
                        cboReturnType.SelectedValue = Localization.ParseNativeInt(reader["ReturnTypeID"].ToString());
                        cboParty.SelectedValue = Localization.ParseNativeInt(reader["PartyID"].ToString());
                        cboBroker.SelectedValue = Localization.ParseNativeInt(reader["BrokerID"].ToString());
                        cboDepartment.SelectedValue = Localization.ParseNativeInt(reader["DepartmentID"].ToString());
                        cboTransport.SelectedValue = Localization.ParseNativeInt(reader["TransportID"].ToString());
                        cboSalesReturn.SelectedValue = Localization.ParseNativeInt(reader["SalesReturnID"].ToString());
                    }
                }

                dtEntryDate.Focus();
                TxtGrossAmount.Text = "0.00";
                txtAddLessAmt.Text = "0.00";
                txtNetAmt.Text = "0.00";
                txtBrokerPercent.Text = "";
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
                    ("(#ENTRYNO#)"),
                    (dtEntryDate.TextFormat(false, true)),
                    ("(#OTHERNO#)"),
                    (dtChlnDate.TextFormat(false, true)),
                    (cboReturnType.SelectedValue),
                    (cboParty.SelectedValue),
                    (cboBroker.SelectedValue),
                    (cboDepartment.SelectedValue),
                    (cboTransport.SelectedValue),
                    (txtLrNo.Text),
                    (dtLrDate.TextFormat(false, true)),
                    (cboSalesReturn.SelectedValue),
                    (string.Format("{0:N}", CommonCls.GetColSum(fgDtls, 16, -1, -1)).ToString().Replace(",", "")),
                    (string.Format("{0:N2}", CommonCls.GetColSum(fgDtls, 17, -1, -1)).ToString().Replace(",", "")),
                    (TxtGrossAmount.Text.ToString().Replace(",", "")),
                    (txtAddLessAmt.Text.ToString().Replace(",", "")),
                    (txtNetAmt.Text.ToString().Replace(",", "")),
                    ((txtDescription.Text.ToString() == ""? "-":txtDescription.Text.ToString())),
                    (txtBrokerPercent.Text.ToString().Replace(",", "")),
                    (txtBrokerTotalAmount.Text.ToString().Replace(",","")),
                    cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue,
                    cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue,
                    dtEd1.TextFormat(false,true), 
                    txtET1.Text,
                    txtET2.Text,
                    txtET3.Text
                };
                int UnitID = 0;
                string strAdjQry = string.Format(" Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_AcLedger", "(#CodeID#)", Localization.ParseNativeInt(base.iIDentity.ToString()) + string.Format(" Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", "(#CodeID#)", Localization.ParseNativeInt(base.iIDentity.ToString())));
                strAdjQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_VatLedger", "(#CodeID#)", base.iIDentity);
                strAdjQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_BrokerLedger", "(#CodeID#)", base.iIDentity);
                if (Operators.ConditionalCompareObjectEqual(this.cboReturnType.SelectedValue, 2, false))
                {
                    strAdjQry = strAdjQry + DBSp.InsertInto_AcLedger("(#CodeID#)", "0", "(#ENTRYNO#)", dtEntryDate.Text,
                        Localization.ParseNativeDouble(base.iIDentity.ToString()), cboSalesReturn.SelectedValue.ToString(), 1, Db_Detials.Ac_AdjType.NewRef, "(#CodeID#)",
                        txtChlnNo.Text.Trim(), dtChlnDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                        Localization.ParseNativeDecimal(TxtGrossAmount.Text.ToString()), 0, txtDescription.Text.Trim(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID,
                        DateAndTime.Now.Date);
                    strAdjQry += DBSp.InsertInto_AcLedger("(#CodeID#)", "0", "(#ENTRYNO#)", dtEntryDate.Text,
                        Localization.ParseNativeDouble(base.iIDentity.ToString()), cboParty.SelectedValue.ToString(), 2, Db_Detials.Ac_AdjType.NewRef, "(#CodeID#)",
                        txtChlnNo.Text.Trim(), dtChlnDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), 0,
                        Localization.ParseNativeDecimal(txtNetAmt.Text), txtDescription.Text.Trim(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID,
                        DateAndTime.Now.Date);
                }

                DataGridViewEx ex = this.fgDtls_f;
                double dblDedAmt = 0.0;

                for (int i = 0; i <= (ex.RowCount - 1); i++)
                {
                    DataGridViewRow row = ex.Rows[i];
                    if (row.Cells[2].Value != null)
                    {
                        if (Localization.ParseNativeDouble(row.Cells[2].Value.ToString()) > 0)
                        {
                            if (Operators.ConditionalCompareObjectEqual(row.Cells[4].FormattedValue, "-", false))
                            {
                                dblDedAmt = -Localization.ParseNativeDouble(row.Cells[5].Value.ToString());
                            }
                            else if (Operators.ConditionalCompareObjectEqual(row.Cells[4].FormattedValue, "+", false))
                            {
                                dblDedAmt = Localization.ParseNativeDouble(row.Cells[5].Value.ToString());
                            }

                            if (dblDedAmt > 0.0)
                            {
                                strAdjQry = strAdjQry + DBSp.InsertInto_AcLedger("(#CodeID#)", Conversions.ToString((int)(i + 3)), "(#ENTRYNO#)", this.dtEntryDate.Text,
                                           (double)base.iIDentity, Conversions.ToString(Localization.ParseNativeInt(row.Cells[3].Value.ToString())), 1,
                                           Db_Detials.Ac_AdjType.OnAccount, "(#CodeID#)", this.txtChlnNo.Text.Trim(), this.dtChlnDate.Text, (double)base.iIDentity,
                                            new decimal(dblDedAmt), decimal.Zero, "null", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, 0, DateAndTime.Now.Date);
                            }
                            else
                            {
                                string sDedAmt = dblDedAmt.ToString();
                                if (sDedAmt.StartsWith("-"))
                                {
                                    sDedAmt = sDedAmt.Substring(1);
                                }
                                dblDedAmt = Localization.ParseNativeDouble(sDedAmt.ToString());
                                strAdjQry = strAdjQry + DBSp.InsertInto_AcLedger("(#CodeID#)", Conversions.ToString((int)(i + 3)), "(#ENTRYNO#)", this.dtEntryDate.Text,
                                           (double)base.iIDentity, Conversions.ToString(Localization.ParseNativeInt(row.Cells[2].Value.ToString())), 2,
                                           Db_Detials.Ac_AdjType.OnAccount, "(#CodeID#)", this.txtChlnNo.Text.Trim(), this.dtChlnDate.Text, (double)base.iIDentity,
                                           decimal.Zero, new decimal(dblDedAmt), "null", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, 0, DateAndTime.Now.Date);
                            }
                        }
                    }
                    row = null;
                }

                #region VatLedger Posting
                try
                {
                    string sVatAcMisc = DB.GetSnglValue("Select MiscID from fn_MiscMaster_tbl() Where MiscName='VAT'");
                    for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                    {
                        DataGridViewRow row2 = fgDtls_f.Rows[i];
                        string sVatAcLedger = DB.GetSnglValue("select TaxtypeID from fn_LedgerMaster_tbl() Where LedgerId=" + fgDtls_f.Rows[i].Cells[2].Value + "");
                        if (sVatAcMisc == sVatAcLedger)
                        {
                            if (Operators.ConditionalCompareObjectEqual(row2.Cells[4].FormattedValue, "+", false))
                            {
                                strAdjQry = strAdjQry + DBSp.InsertInto_VatLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                             row2.Cells[2].Value.ToString(), Localization.ParseNativeInt(row2.Cells[4].Value.ToString()), Localization.ParseNativeDecimal(row2.Cells[4].Value.ToString()),
                                             "(#CodeID#)", Localization.ParseNativeDecimal(row2.Cells[5].Value.ToString()), 0, "null", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                            }
                            if (Operators.ConditionalCompareObjectEqual(row2.Cells[4].FormattedValue, "-", false))
                            {
                                strAdjQry = strAdjQry + DBSp.InsertInto_VatLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                         row2.Cells[2].Value.ToString(), Localization.ParseNativeInt(row2.Cells[4].Value.ToString()), Localization.ParseNativeDecimal(row2.Cells[4].Value.ToString()),
                                         "(#CodeID#)", 0, Localization.ParseNativeDecimal(row2.Cells[5].Value.ToString()), "null", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                            }
                        }
                    }
                }
                catch { }
                #endregion

                for (int j = 0; j <= fgDtls.RowCount - 1; j++)
                {
                    DataGridViewRow row2 = fgDtls.Rows[j];
                    if (row2.Cells[17].Value != null)
                    {
                        if (Localization.ParseNativeDouble(row2.Cells[17].Value.ToString()) != 0.0)
                        {
                            strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(
                           Localization.ParseNativeDouble(base.iIDentity.ToString()),
                           "(#CodeID#)",
                           (j + 1).ToString(),
                           "(#ENTRYNO#)",
                           dtChlnDate.Text,
                           Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                           row2.Cells[27].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[27].Value.ToString()),
                           (row2.Cells[44].Value.ToString().Trim() == "" ? "0" : row2.Cells[44].Value.ToString()),
                           (row2.Cells[46].Value.ToString().Trim() == "" ? "0" : row2.Cells[46].Value.ToString()),
                           (row2.Cells[2].Value.ToString().Trim() == "" ? "-" : row2.Cells[2].Value.ToString()),
                           (row2.Cells[3].Value.ToString().Trim() == null ? "-" : row2.Cells[3].Value.ToString().Trim() == "" ? "-" : row2.Cells[3].Value.ToString()),
                           row2.Cells[10].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[10].Value.ToString()),
                           row2.Cells[12].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[12].Value.ToString()),
                           row2.Cells[11].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[11].Value.ToString()),
                           row2.Cells[13].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[13].Value.ToString()),
                           row2.Cells[14].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[14].Value.ToString()),
                           row2.Cells[15].Value == null ? 0 : Localization.ParseNativeDouble(row2.Cells[15].Value.ToString()),
                           Localization.ParseNativeDecimal(row2.Cells[16].Value.ToString()),
                           Localization.ParseNativeDecimal(row2.Cells[17].Value.ToString()),
                           Localization.ParseNativeDecimal(row2.Cells[18].Value.ToString()),
                           0, 0, 0,
                           (row2.Cells[21].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[21].Value.ToString())),
                           "NULL",
                           row2.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[28].Value.ToString()),
                           row2.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[29].Value.ToString()),
                           row2.Cells[30].Value == null ? "NULL" : row2.Cells[30].Value.ToString(),
                           row2.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[31].Value.ToString()),
                           row2.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[32].Value.ToString()),
                           row2.Cells[33].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[33].Value.ToString()),
                           row2.Cells[34].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[34].Value.ToString()),
                           row2.Cells[35].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[35].Value.ToString()),
                           row2.Cells[36].Value == null ? 0 : Localization.ParseNativeInt(row2.Cells[36].Value.ToString()),
                           row2.Cells[37].Value == null || row2.Cells[37].Value.ToString() == "" || row2.Cells[37].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row2.Cells[37].Value.ToString()),
                           row2.Cells[38].Value == null || row2.Cells[38].Value.ToString() == "" || row2.Cells[38].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row2.Cells[38].Value.ToString()),
                           row2.Cells[39].Value == null || row2.Cells[39].Value.ToString() == "" ? "-" : row2.Cells[39].Value.ToString(),
                           row2.Cells[40].Value == null || row2.Cells[40].Value.ToString() == "" ? "-" : row2.Cells[40].Value.ToString(),
                           row2.Cells[41].Value == null || row2.Cells[41].Value.ToString() == "" ? "-" : row2.Cells[41].Value.ToString(),
                           row2.Cells[42].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[42].Value.ToString()),
                           row2.Cells[43].Value == null ? 0 : Localization.ParseNativeDecimal(row2.Cells[43].Value.ToString()),
                           "NULL", j, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);

                            UnitID = Localization.ParseNativeInt(row2.Cells[15].Value.ToString());

                            try
                            {
                                if (ENABLE_BROKER_CALCMETHOD2)
                                {
                                    if (ENABLE_BROKER_FAB_SALESBILLRETURN)
                                    {
                                        if (row2.Cells[23].Value != null && row2.Cells[23].Value.ToString() != "" && row2.Cells[23].Value.ToString() != "0")
                                        {
                                            if (cboBroker.SelectedValue != null && cboBroker.SelectedValue.ToString() != "" && cboBroker.SelectedValue.ToString() != "0")
                                            {
                                                strAdjQry = strAdjQry + DBSp.InsertIntoBrokerLedger("(#CodeID#)", (j + 1).ToString(), "(#ENTRYNO#)", Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboBroker.SelectedValue.ToString()), row2.Cells[3].Value.ToString(), dtEntryDate.Text.ToString(), Localization.ParseNativeDouble(row2.Cells[15].Value.ToString()), Localization.ParseNativeDecimal(row2.Cells[22].Value.ToString()), 0, Localization.ParseNativeDecimal(row2.Cells[23].Value.ToString()), "-", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date, 0, 1);
                                            }
                                        }
                                    }
                                }
                            }
                            catch { }
                        }
                    }
                    row2 = null;
                }

                try
                {
                    if (ENABLE_BROKER_CALCMETHOD1)
                    {
                        if (ENABLE_BROKER_FAB_SALESBILLRETURN)
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
                //    strAdjQry = strAdjQry + DBSp.InsertIntoTrasportLedger("(#CodeID#)", this.txtBillNo.Text.ToString(), dtBillDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()), Localization.ParseNativeDouble(cboParty.SelectedValue.ToString()), Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()), txtLrNo.Text, dtLrDate.Text, null, Localization.ParseNativeDouble(UnitID.ToString()), Localization.ParseNativeInt(string.Format("{0:N}", CommonCls.GetColSum(fgDtls, 15, -1, -1))), Localization.ParseNativeDecimal(string.Format("{0:N2}", CommonCls.GetColSum(fgDtls, 16, -1, -1))), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                //}
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                DBSp.Transcation_AddEdit(pArrayData, fgDtls, true, strAdjQry, "", txtEntryNo.Text, txtChlnNo.Text, "RefNo", new DataGridViewEx[] { fgDtls_f });
            }
            catch
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                string strTblName;
                if (!EventHandles.IsValidGridReq(this.fgDtls, base.dt_AryIsRequired) && !EventHandles.IsValidGridReq(this.fgDtls_f, base.dt_AryIsRequired))
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
                if (txtChlnNo.Text.Trim() == "" || txtChlnNo.Text.Trim() == "-" || txtChlnNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Challan No.");
                    txtChlnNo.Focus();
                    return true;
                }

                if (!Information.IsDate(dtChlnDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Challan Date");
                    dtChlnDate.Focus();
                    return true;
                }
                if (cboReturnType.Text == "After Bill" || cboReturnType.Text == "-After Bill")
                {
                    //if (fgDtls.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    //    {
                    //        if (fgDtls.Rows[i].Cells[6].Value == null || fgDtls.Rows[i].Cells[6].Value.ToString() == "")
                    //        {
                    //            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Bill No Is Required For After Bill");
                    //            return true;
                    //        }
                    //    }
                    //}
                }

                //if (cboReturnType.Text == "Before Bill")
                //{
                //    if (fgDtls.Rows.Count > 0)
                //    {
                //        for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                //        {
                //            if (fgDtls.Rows[i].Cells[4].Value == null || fgDtls.Rows[i].Cells[4].Value.ToString() == "")
                //            {
                //                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Challan No Is Required For Before Bill");
                //                return true;
                //            }
                //        }
                //    }
                //}


                if (cboParty.SelectedValue == null || cboParty.Text.Trim().ToString() == "-" || cboParty.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Party");
                    cboParty.Focus();
                    return true;
                }
                if (cboDepartment.SelectedValue == null || cboDepartment.Text.Trim().ToString() == "-" || cboDepartment.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    cboDepartment.Focus();
                    return true;
                }

                if (cboSalesReturn.SelectedValue == null || cboSalesReturn.Text.Trim().ToString() == "-" || cboSalesReturn.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Sales Return A/c");
                    cboSalesReturn.Focus();
                    return true;
                }

                if (txtChlnNo.Text.Trim().Length > 0)
                {
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        strTblName = "tbl_FabricOutwardReturnMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "ChallanNo", txtChlnNo.Text, false, "", 0, " PartyID = " + cboParty.SelectedValue + " AND StoreID=" + Db_Detials.StoreID + " and  CompID = " + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " And YearID =" + Db_Detials.YearID + "", "This Party already used this Challan No in Entry No : " + DB.GetSnglValue(string.Format("Select Entryno from {0} where PartyID = {1} and ChallanNo = '{2}' ", "tbl_FabricOutwardReturnMain", cboParty.SelectedValue, txtChlnNo.Text.ToString()))))
                        {
                            txtChlnNo.Focus();
                            return true; ;
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        strTblName = "tbl_FabricOutwardReturnMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "ChallanNo", txtChlnNo.Text, true, "FabOutReturnID", Localization.ParseNativeLong(txtCode.Text.Trim()), " PartyID = " + cboParty.SelectedValue + " and StoreID=" + Db_Detials.StoreID + " AND CompID = " + Db_Detials.CompID + " and BarnchID=" + Db_Detials.BranchID + " And YearID =" + Db_Detials.YearID + "", "This Party already used this Challan No in Entry No : " + DB.GetSnglValue(string.Format("Select Entryno from {0} where PartyID = {1} and ChallanNo = '{2}' ", "tbl_FabricOutwardReturnMain", cboParty.SelectedValue, txtChlnNo.Text.ToString()))))
                        {
                            txtChlnNo.Focus();
                            return true; ;
                        }
                    }
                }
                if (ENABLE_BROKER_FAB_SALESBILLRETURN)
                {
                    if (cboBroker.SelectedValue == null || cboBroker.Text.Trim().ToString() == "-" || cboBroker.SelectedValue.ToString() == "0")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Broker");
                        cboBroker.Focus();
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

        public void AplySelectBtnEnbl()
        {
            if ((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record) && (cboReturnType.SelectedItem.ToString() != "[3, Other]"))
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
                if (!this.RTN_FM_STK)
                {
                    if (dtChlnDate.Text != "__/__/____")
                    {
                        if (cboParty.Text.Trim().ToString() != "-" || cboParty.SelectedValue != null)
                        {
                            #region StockAdjQuery
                            string strarray = "";
                            string strQry = string.Empty;
                            int ibitcol = 0;
                            string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", iIDentity, 0.0));
                            string strQry_ColName = "";
                            string[] arr = CommonCls.GetAdjColName(base.iIDentity, 0.0).Split(';');
                            strQry_ColName = arr[0].ToString();
                            #endregion

                            frmStockAdj frmStockAdj = new frmStockAdj();
                            frmStockAdj.MenuID = base.iIDentity;
                            frmStockAdj.Entity_IsfFtr = 0.0;
                            frmStockAdj.ref_fgDtls = fgDtls;
                            frmStockAdj.AsonDate = Conversions.ToDate(dtChlnDate.Text);
                            frmStockAdj.LedgerID = cboParty.SelectedValue.ToString();

                            try
                            {
                                if (cboReturnType.SelectedItem.ToString() == "[1, Before Bill]")
                                {
                                    strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5},{6},{7}) Where BillNo IS NULL", new object[] { strQry_ColName, snglValue, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(dtChlnDate.Text))), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboParty.SelectedValue });
                                }
                                else if (cboReturnType.SelectedItem.ToString() == "[2, After Bill]")
                                {
                                    strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5},{6},{7}) Where  BillNo IS NOT NULL", new object[] { strQry_ColName, snglValue, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(dtChlnDate.Text))), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboParty.SelectedValue });
                                }
                                else
                                {
                                    strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5},{6},{7}) Where BillNo IS NULL", new object[] { strQry_ColName, snglValue, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(dtChlnDate.Text))), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboParty.SelectedValue });
                                }
                            }
                            catch { }

                            frmStockAdj.QueryString = strQry;
                            frmStockAdj.IsRefQuery = true;
                            frmStockAdj.ibitCol = ibitcol;

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
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Party to Fetch Stock");
                            cboParty.Focus();
                        }
                    }
                }
                else if (this.dtChlnDate.Text != "__/__/____")
                {
                    if (this.cboDepartment.SelectedValue != null)
                    {
                        frmStockAdj adj2 = new frmStockAdj();
                        adj2.MenuID = base.iIDentity;
                        adj2.Entity_IsfFtr = 1.0;
                        adj2.ref_fgDtls = this.fgDtls;
                        adj2.AsonDate = Conversions.ToDate(this.dtChlnDate.Text);
                        adj2.LedgerID = Conversions.ToString(this.cboDepartment.SelectedValue);

                        try
                        {
                            if (cboReturnType.SelectedItem.ToString() == "[0, Before Bill]")
                            {
                                adj2.ReturnType = "0";
                            }
                            else if (cboReturnType.SelectedItem.ToString() == "[1, After Bill]")
                            {
                                adj2.ReturnType = "1";
                            }
                            else
                            {
                                adj2.ReturnType = "0";
                            }
                        }
                        catch { adj2.ReturnType = "0"; }

                        adj2.UsedInGridArray = this.OrgInGridArray;
                        if (adj2.ShowDialog() == DialogResult.Cancel)
                        {
                            adj2.Dispose();
                            return;
                        }
                        adj2.Dispose();
                        adj2 = null;
                    }
                    else
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Question, "", "Please Select Department to Fetch Stock");
                    }
                }
                else
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Question, "", "Please Enter Challan Date to Fetch Stock");
                }
                fgDtls.Select();
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
                TxtGrossAmount.Text = string.Format("{0:N2}", CommonCls.GetColSum(fgDtls, 20, -1, -1));

                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (ENABLE_BROKER_CALCMETHOD2)
                    {
                        if (ENABLE_BROKER_FAB_SALESBILLRETURN)
                        {
                            txtBrokerTotalAmount.Text = string.Format("{0:N2}", Math.Round(CommonCls.GetColSum(this.fgDtls, 23, -1, -1)));
                        }
                    }
                }

                double dblDedAmt = 0.0;
                DataGridViewEx ex = this.fgDtls_f;
                for (int i = 0; i <= ex.RowCount - 1; i++)
                {
                    if (ex.Rows[i].Cells[4].Value != null)
                    {
                        if (Operators.ConditionalCompareObjectEqual(ex.Rows[i].Cells[4].FormattedValue, "-", false))
                        {
                            dblDedAmt -= Localization.ParseNativeDouble(ex.Rows[i].Cells[5].Value.ToString());
                        }
                        else if (Operators.ConditionalCompareObjectEqual(ex.Rows[i].Cells[4].FormattedValue, "+", false))
                        {
                            dblDedAmt += Localization.ParseNativeDouble(ex.Rows[i].Cells[5].Value.ToString());
                        }
                    }
                }
                ex = null;
                txtAddLessAmt.Text = string.Format("{0:N2}", Math.Abs(dblDedAmt));
                txtNetAmt.Text = string.Format("{0:N2}", Localization.ParseNativeDouble(TxtGrossAmount.Text) + dblDedAmt);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
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
                            fgDtls.Rows[i].Cells[6].ReadOnly = false;
                            fgDtls.Rows[i].Cells[7].ReadOnly = false;
                            fgDtls.Rows[i].Cells[8].ReadOnly = false;
                            fgDtls.Rows[i].Cells[9].ReadOnly = false;
                            fgDtls.Rows[i].Cells[11].ReadOnly = false;
                            fgDtls.Rows[i].Cells[12].ReadOnly = false;
                            fgDtls.Rows[i].Cells[13].ReadOnly = false;
                            fgDtls.Rows[i].Cells[14].ReadOnly = false;
                            fgDtls.Rows[i].Cells[15].ReadOnly = false;
                            fgDtls.Rows[i].Cells[16].ReadOnly = false;
                            fgDtls.Rows[i].Cells[17].ReadOnly = false;
                            fgDtls.Rows[i].Cells[18].ReadOnly = false;
                            fgDtls.Rows[i].Cells[19].ReadOnly = false;
                            fgDtls.Rows[i].Cells[20].ReadOnly = false;
                            fgDtls.Rows[i].Cells[2].ReadOnly = false;
                            fgDtls.Rows[i].Cells[4].Value = "0";
                            fgDtls.Rows[i].Cells[5].Value = "0";
                            btnSelect.Enabled = false;
                        }
                        else
                        {
                            fgDtls.Rows[i].Cells[4].ReadOnly = true;
                            fgDtls.Rows[i].Cells[6].ReadOnly = true;
                            fgDtls.Rows[i].Cells[7].ReadOnly = true;
                            fgDtls.Rows[i].Cells[8].ReadOnly = true;
                            fgDtls.Rows[i].Cells[9].ReadOnly = true;
                            fgDtls.Rows[i].Cells[11].ReadOnly = true;
                            fgDtls.Rows[i].Cells[12].ReadOnly = true;
                            fgDtls.Rows[i].Cells[13].ReadOnly = true;
                            fgDtls.Rows[i].Cells[14].ReadOnly = true;
                            fgDtls.Rows[i].Cells[15].ReadOnly = true;
                            fgDtls.Rows[i].Cells[16].ReadOnly = true;
                            fgDtls.Rows[i].Cells[17].ReadOnly = true;
                            fgDtls.Rows[i].Cells[18].ReadOnly = true;
                            //fgDtls.Rows[i].Cells[19].ReadOnly = true;
                            //fgDtls.Rows[i].Cells[20].ReadOnly = true;
                            fgDtls.Rows[i].Cells[2].ReadOnly = true;
                            btnSelect.Enabled = true;
                        }
                    }
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
                                fgDtls.Rows[e.RowIndex].Cells[18].Value = "0";
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
                if (!((base.blnFormAction == Enum_Define.ActionType.View_Record) | (base.blnFormAction == Enum_Define.ActionType.Not_Active)))
                {
                    DataGridViewEx ex = this.fgDtls_f;
                    if (((e.ColumnIndex == 17) | (e.ColumnIndex == 19)) | (e.ColumnIndex == 20))
                    {
                        CalcVal();
                    }
                    switch (e.ColumnIndex)
                    {
                        case 7:
                            fgDtls.Rows[e.RowIndex].Cells[11].Value = fgDtls.Rows[e.RowIndex].Cells[7].Value;
                            break;

                        case 8:
                            fgDtls.Rows[e.RowIndex].Cells[12].Value = fgDtls.Rows[e.RowIndex].Cells[8].Value;
                            if (ENABLE_BROKER_CALCMETHOD2)
                            {
                                if (ENABLE_BROKER_FAB_SALESBILLRETURN)
                                {
                                    if (cboBroker.SelectedValue != null && cboBroker.SelectedValue.ToString() != "" && cboBroker.SelectedValue.ToString() != "0")
                                    {
                                        SBrokersPerc = DB.GetSnglValue(string.Format("SELECT percentage from tbl_BrokerPercentDtls a left join tbl_BrokerPercentMain B on A.BrokerPercentID=b.BrokerPercentID where b.BrokerID={0} and a.QualityID={1}", cboBroker.SelectedValue, fgDtls.Rows[e.RowIndex].Cells[8].Value));
                                        fgDtls.Rows[e.RowIndex].Cells[22].Value = SBrokersPerc;

                                        if (fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value.ToString() != "0" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value.ToString() != "0")
                                        {
                                            decimal dbrokertotalamt_gridrow = (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value.ToString()) / 100) * (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value.ToString()));
                                            fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[23].Value = dbrokertotalamt_gridrow;
                                        }
                                        else
                                        {
                                            fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[23].Value = 0;
                                        }
                                    }
                                }
                            }
                            break;

                        case 9:
                            fgDtls.Rows[e.RowIndex].Cells[13].Value = fgDtls.Rows[e.RowIndex].Cells[9].Value;
                            break;

                        case 16:
                            CalcVal();
                            break;

                        case 17:
                            for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                            {
                                if (ex.Rows[i].Cells[4].Value != null && ex.Rows[i].Cells[4].Value.ToString() != "0")
                                {
                                    ex.Rows[i].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[i].Cells[4].Value))), 100M).ToString().Replace(",", "");
                                    CalcVal();
                                }
                            }
                            CalcVal();
                            break;

                        case 19:
                            fgDtls.Rows[e.RowIndex].Cells[20].Value = Math.Round(Localization.ParseNativeDecimal(fgDtls.Rows[e.RowIndex].Cells[20].Value.ToString()));

                            if (ENABLE_BROKER_CALCMETHOD2)
                            {
                                if (ENABLE_BROKER_FAB_SALESBILLRETURN && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value.ToString() != "0" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value.ToString() != "0")
                                {
                                    decimal dbrokertotalamt_gridrow = (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value.ToString()) / 100) * (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value.ToString()));
                                    fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[23].Value = dbrokertotalamt_gridrow;
                                }
                                else
                                {
                                    fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[23].Value = 0;
                                }
                            }

                            for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                            {
                                if (ex.Rows[i].Cells[4].Value != null && ex.Rows[i].Cells[4].Value.ToString() != "0")
                                {
                                    ex.Rows[i].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[i].Cells[4].Value))), 100M).ToString().Replace(",", "");
                                    CalcVal();
                                }
                            }
                            break;

                        case 20:
                            for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                            {
                                if (ex.Rows[i].Cells[4].Value != null && ex.Rows[i].Cells[4].Value.ToString() != "0")
                                {
                                    ex.Rows[i].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[i].Cells[4].Value))), 100M).ToString().Replace(",", "");
                                    CalcVal();
                                }
                            }
                            CalcVal();
                            break;

                        case 22:
                            if (ENABLE_BROKER_CALCMETHOD2)
                            {
                                if (ENABLE_BROKER_FAB_SALESBILLRETURN && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value.ToString() != "0" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value.ToString() != "0")
                                {
                                    decimal dbrokertotalamt_gridrow = (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value.ToString()) / 100) * (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[20].Value.ToString()));
                                    fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[23].Value = dbrokertotalamt_gridrow;
                                }
                                else
                                {
                                    fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[23].Value = 0;
                                }
                            }
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

        private void fgDtls_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewEx fgDtls = this.fgDtls;
                if (fgDtls.RowCount != 0)
                {
                    if (e.ColumnIndex == 3)
                    {
                        string str2;
                        if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                        {
                            string primaryFieldNameValue = Conversions.ToString(fgDtls.Rows[e.RowIndex].Cells[3].Value);
                            if ((Strings.Trim(Conversions.ToString(fgDtls.Rows[e.RowIndex].Cells[3].Value)) != null) && (Strings.Trim(Conversions.ToString(fgDtls.Rows[e.RowIndex].Cells[3].Value)).Length > 0))
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString() != "-")
                                {
                                    str2 = "tbl_StockFabricLedger";
                                    if (Navigate.CheckDuplicate(ref str2, "BatchNo", primaryFieldNameValue, false, "", 0L, " StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and  BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                                    {
                                        fgDtls.CurrentCell = fgDtls[3, e.RowIndex];
                                    }
                                }
                            }
                            else if (Strings.Trim(Conversions.ToString(fgDtls.Rows[e.RowIndex].Cells[3].Value)).Length <= 0)
                            {
                                fgDtls.Rows[e.RowIndex].Cells[3].Value = "-";
                            }
                        }
                        else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                        {
                            if ((Strings.Trim(Conversions.ToString(fgDtls.Rows[e.RowIndex].Cells[3].Value)) != null) && (Strings.Trim(Conversions.ToString(fgDtls.Rows[e.RowIndex].Cells[3].Value)).Length > 0))
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString() != "-")
                                {
                                    str2 = "tbl_StockFabricLedger";
                                    if (Navigate.CheckDuplicate(ref str2, "BatchNo", fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString(), true, "TransID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), " StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                                    {
                                        fgDtls.CurrentCell = fgDtls[3, e.RowIndex];
                                    }
                                }
                            }
                            else if (Strings.Trim(Conversions.ToString(fgDtls.Rows[e.RowIndex].Cells[3].Value)).Length <= 0)
                            {
                                fgDtls.Rows[e.RowIndex].Cells[3].Value = "-";
                            }
                        }
                    }
                    if (((e.ColumnIndex == 7) | (e.ColumnIndex == 9)) && ((fgDtls.Rows[e.RowIndex].Cells[7].Value != null) && (Strings.Trim(Conversions.ToString(fgDtls.Rows[e.RowIndex].Cells[7].Value)).Length > 0)))
                    {
                        fgDtls.Rows[e.RowIndex].Cells[8].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", RuntimeHelpers.GetObjectValue(fgDtls.Rows[e.RowIndex].Cells[7].Value))));
                    }
                    fgDtls = null;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

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
                                            ex.Rows[ex.CurrentRow.Index].Cells[3].Value = "+";
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
                                            ex.Rows[ex.CurrentRow.Index].Cells[3].Value = "+";
                                        }
                                        ex.Rows[ex.CurrentRow.Index].Cells[4].Value = reader["Percentage"].ToString();
                                        ex.Rows[ex.CurrentRow.Index].Cells[5].Value = ((Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(reader["Percentage"].ToString())) / 100);
                                    }
                                }
                            }
                            CalcVal();
                            break;

                        case 3:
                            ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                            ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(TxtGrossAmount.Text)) * 100);
                            break;

                        case 4:
                            ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                            CalcVal();
                            break;

                        case 5:
                            ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(TxtGrossAmount.Text)) * 100);
                            CalcVal();
                            break;
                    }
                    CalcVal();
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
                    switch (e.ColumnIndex)
                    {
                        case 3:
                            ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                            ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(TxtGrossAmount.Text)) * 100);
                            CalcVal();
                            break;

                        case 4:
                            ex.Rows[ex.CurrentRow.Index].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[ex.CurrentRow.Index].Cells[4].Value))), 100M).ToString().Replace(",", "");
                            CalcVal();
                            break;

                        case 5:
                            ex.Rows[ex.CurrentRow.Index].Cells[4].Value = decimal.Multiply(decimal.Divide(Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[ex.CurrentRow.Index].Cells[5].Value)), Localization.ParseNativeDecimal(this.TxtGrossAmount.Text)), 100M);
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

        public void PrintRecord()
        {
            try
            {
                CIS_ReportTool.frmMultiPrint.MenuID = base.iIDentity;
                CIS_ReportTool.frmMultiPrint.Id = Localization.ParseNativeInt(this.txtCode.Text);
                CIS_ReportTool.frmMultiPrint.TblNm = "tbl_FabricOutwardReturnMain";
                CIS_ReportTool.frmMultiPrint.IdStr = "FabOutReturnID";
                CIS_ReportTool.frmMultiPrint.TblNm_D = "tbl_FabricOutwardReturnDtls";
                CIS_ReportTool.frmMultiPrint frmMPrnt = new CIS_ReportTool.frmMultiPrint();
                CIS_ReportTool.frmMultiPrint.iCompID = Db_Detials.CompID;
                CIS_ReportTool.frmMultiPrint.iYearID = Db_Detials.YearID;
                CIS_ReportTool.frmMultiPrint.iUserID = Db_Detials.UserID;
                CIS_ReportTool.frmMultiPrint.objReport = Db_Detials.objReport;
                CIS_ReportTool.frmMultiPrint.sApplicationName = GetAssemblyInfo.ProductName;
                CIS_ReportTool.frmMultiPrint.VoucherTypeID = 0;

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

        private enum dgDeduct
        {
            FabOutReturnID,
            SubFabOutReturnID,
            LedgerID,
            AddLessTypeID,
            Percentage,
            Amount
        }

        private void cboParty_SelectedValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (cboParty.SelectedValue != null && Localization.ParseNativeDouble(cboParty.SelectedValue.ToString()) > 0)
                {
                    cboBroker.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select BrokerID from {0} where LedgerId = {1}", "tbl_LedgerMaster", cboParty.SelectedValue)));
                    cboSalesReturn.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select PurchSalesID From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboParty.SelectedValue)));
                    cboTransport.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select TransportId from {0} LedgerId = {1}", "tbl_LedgerMaster", cboParty.SelectedValue)));
                }
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }

        }

        private void txtBrokerPercent_Leave(object sender, EventArgs e)
        {
            try
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (ENABLE_BROKER_FAB_SALESBILLRETURN)
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
    }
}



