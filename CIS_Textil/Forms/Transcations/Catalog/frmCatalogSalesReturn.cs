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
    public partial class frmCatalogSalesReturn : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        public DataGridViewEx fgDtls_f;
        public DataGridViewEx fgDtls_f_footer;

        private int _Mtrs_Unit;
        bool FS_BRK_COM = false;
        private bool flg_OrderConform;
        ArrayList OrgInGridArray = new ArrayList();

        public frmCatalogSalesReturn()
        {
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;

            fgDtls_f = GrdDtls.fgDtls;
            fgDtls_f_footer = GrdDtls.fgDtls_f;

            InitializeComponent();
            _Mtrs_Unit = 0;
        }

        #region Event

        private void frmFabricInvoice_Load(object sender, EventArgs e)
        {
            Combobox_Setup.FilterId = "";
            Combobox_Setup.FillCbo(ref cboDepartment, Combobox_Setup.ComboType.Mst_Department, "");
            Combobox_Setup.FillCbo(ref cboParty, Combobox_Setup.ComboType.Mst_CustomerWithVAT, "");
            Combobox_Setup.FillCbo(ref cboBroker, Combobox_Setup.ComboType.Mst_Brokers, "");
            Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
            Combobox_Setup.FillCbo(ref cboSalesAc, Combobox_Setup.ComboType.SalesAc, "");

            object instance = new Dictionary<int, string>();
            NewLateBinding.LateIndexSet(instance, new object[] { 1, "After Bill" }, null);
            NewLateBinding.LateIndexSet(instance, new object[] { 2, "Other" }, null);
            CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboReturnType = this.cboReturnType;
            cboReturnType.DataSource = new BindingSource(RuntimeHelpers.GetObjectValue(instance), null);
            cboReturnType.DisplayMember = "Value";
            cboReturnType.ValueMember = "Key";
            cboReturnType.ColumnWidths = "0;";
            cboReturnType.SelectedIndex = -1;
            cboReturnType.AutoComplete = true;
            cboReturnType.AutoDropdown = true;
            cboReturnType = null;

            _Mtrs_Unit = Localization.ParseNativeInt(DB.GetSnglValue("SELECT UnitID from tbl_Unitsmaster WHERE UnitName like'Meters'").ToString());

            DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
            DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls_f, fgDtls_f_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, true, true, 0, 1, true);
            txtEntryNo.Enabled = false;
        }

        #endregion

        #region Navigation

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "CatSalesReturnACID", Enum_Define.ValidationType.Text);
                int i = Localization.ParseNativeInt(DB.GetSnglValue("select ReturnTypeID from tbl_CatalogSalesReturnMain where CatSalesReturnACID=" + txtCode.Text + "and IsDeleted=0"));
                if (i == 1)
                {
                    object instance = new Dictionary<int, string>();
                    NewLateBinding.LateIndexSet(instance, new object[] { 1, "After Bill" }, null);
                    NewLateBinding.LateIndexSet(instance, new object[] { 2, "Other" }, null);
                    CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboReturnType = this.cboReturnType;
                    cboReturnType.DataSource = new BindingSource(RuntimeHelpers.GetObjectValue(instance), null);
                    cboReturnType.DisplayMember = "Value";
                    cboReturnType.ValueMember = "Key";
                    cboReturnType.ColumnWidths = "0;";
                    cboReturnType.SelectedIndex = 0;
                }
                else
                {
                    object instance = new Dictionary<int, string>();
                    NewLateBinding.LateIndexSet(instance, new object[] { 1, "After Bill" }, null);
                    NewLateBinding.LateIndexSet(instance, new object[] { 2, "Other" }, null);
                    CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboReturnType = this.cboReturnType;
                    cboReturnType.DataSource = new BindingSource(RuntimeHelpers.GetObjectValue(instance), null);
                    cboReturnType.DisplayMember = "Value";
                    cboReturnType.ValueMember = "Key";
                    cboReturnType.ColumnWidths = "0;";
                    cboReturnType.SelectedIndex = 1;
                }
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtRefNo, "RefNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtChallanDate, "RefDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtBillNo, "BillNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtBillDate, "BillDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboParty, "PartyID", Enum_Define.ValidationType.Text);
                try
                {
                    DBValue.Return_DBValue(this, cboDepartment, "DepartmentID", Enum_Define.ValidationType.Text);
                }
                catch { }
                DBValue.Return_DBValue(this, cboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLrNo, "LrNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtLrDate, "LrDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboSalesAc, "SalesReturnACID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, TxtTotalPcs, "TotPcs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, TxtGrossAmount, "GrossAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAddLessAmt, "AddLessAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtNetAmt, "NetAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtNarration, "Description", Enum_Define.ValidationType.Text);

                DetailGrid_Setup.FillGrid(fgDtls, fgDtls.Grid_UID, fgDtls.Grid_Tbl, "CatSalesReturnACID", txtCode.Text, base.dt_HasDtls_Grd);
                DetailGrid_Setup.FillGrid(fgDtls_f, fgDtls_f.Grid_UID, fgDtls_f.Grid_Tbl, "CatSalesReturnACID", txtCode.Text, base.dt_HasDtls_Grd);
                CalcVal();
            }
            catch { }
        }

        public void MovetoField()
        {
            {
                txtCode.Text = "";
                CommonCls.IncFieldID(this.txtEntryNo, ref txtEntryNo, "");
                this.txtRefNo.Text = (CommonCls.AutoInc(this, "RefNo", "CatSalesReturnACID", ""));
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CreateDefault_Rows(fgDtls_f, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);

                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                EventHandles.CalculateFooter_Rows(fgDtls_f, fgDtls_f_footer, fgDtls_f.Grid_ID.ToString(), fgDtls_f.Grid_UID);
                int MaxID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format(" Select Isnull(Max(CatSalesReturnACID),0) From {0}  where IsDeleted=0 and CompID={1} and YearID={2} and StoreID = {3} and BranchID={4}", "tbl_CatalogSalesReturnMain", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.StoreID, Db_Detials.BranchID)));
                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where IsDeleted=0 and CatSalesReturnACID = {1} and CompID={2} and YearID={3} and StoreID = {4} and BranchID={5}", new object[] { "tbl_CatalogSalesReturnMain", MaxID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.StoreID, Db_Detials.BranchID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = (Localization.ToVBDateString(reader["EntryDate"].ToString()));
                        dtBillDate.Text = (Localization.ToVBDateString(reader["BillDate"].ToString()));
                        dtChallanDate.Text = (Localization.ToVBDateString(reader["RefDate"].ToString()));
                        cboParty.SelectedValue = Localization.ParseNativeInt(reader["PartyID"].ToString());
                        cboDepartment.SelectedValue = Localization.ParseNativeInt(reader["DepartmentID"].ToString());
                        cboBroker.SelectedValue = Localization.ParseNativeInt(reader["BrokerID"].ToString());
                        cboTransport.SelectedValue = Localization.ParseNativeInt(reader["TransportID"].ToString());
                        dtLrDate.Text = (Localization.ToVBDateString(reader["LrDate"].ToString()));
                        cboReturnType.SelectedValue = Localization.ParseNativeInt(reader["ReturnTypeID"].ToString());
                        cboSalesAc.SelectedValue = Localization.ParseNativeInt(reader["SalesReturnACID"].ToString());
                    }
                }
                dtEntryDate.Focus();
                TxtTotalPcs.Text = "0";
                TxtGrossAmount.Text = "0.00";
                txtAddLessAmt.Text = "0.00";
                txtNetAmt.Text = "0.00";
                flg_OrderConform = false;
                cboReturnType_SelectedValueChanged(null, null);
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
                (dtChallanDate.TextFormat(false, true)),
                (cboReturnType.SelectedValue),
                (txtBillNo.Text.ToString()),
                (dtBillDate.TextFormat(false, true)),
                (cboParty.SelectedValue),
                (cboBroker.SelectedValue),
                (cboDepartment.SelectedValue),
                (cboTransport.SelectedValue),
                (txtLrNo.Text),
                (dtLrDate.TextFormat(false, true)),
                (cboSalesAc.SelectedValue),
                (TxtTotalPcs.Text.ToString().Replace(",", "")),
                (TxtGrossAmount.Text.ToString().Replace(",", "")),
                (txtAddLessAmt.Text.ToString().Replace(",", "")),
                (txtNetAmt.Text.ToString().Replace(",", "")),
                ((txtNarration.Text.ToString() == ""? "-":txtNarration.Text.ToString()))
                };

                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_AcLedger", "(#CodeID#)", base.iIDentity.ToString());
                strAdjQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockBookLedger", "(#CodeID#)", Localization.ParseNativeInt(Conversions.ToString(base.iIDentity)));
                // int UnitID = 0; //Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select UnitID from {0} Where  DlryChlnID = {1}", "tbl_FabricDeliveryChallanDtls2", cboRefNo.SelectedValue)));
                // int DepartmentID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select DepartmentID from {0} Where IsDeleted=0 and DlryChlnID = {1}", "tbl_FabricDeliveryChallanMain2", cboRefNo.SelectedValue)));

                strAdjQry += DBSp.InsertInto_AcLedger("(#CodeID#)", "0", "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                cboParty.SelectedValue.ToString(), 1, Db_Detials.Ac_AdjType.NewRef, "(#CodeID#)", txtBillNo.Text.Trim(), dtBillDate.Text,
                                Localization.ParseNativeDouble(base.iIDentity.ToString()), 0, Localization.ParseNativeDecimal(txtNetAmt.Text.ToString().Replace(",", "")),
                                txtNarration.Text.Trim(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                strAdjQry += DBSp.InsertInto_AcLedger("(#CodeID#)", "0", "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                cboSalesAc.SelectedValue.ToString(), 2, Db_Detials.Ac_AdjType.NewRef, "(#CodeID#)", txtBillNo.Text.Trim(), dtBillDate.Text,
                                Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDecimal(TxtGrossAmount.Text.ToString().Replace(",", "")), 0,
                                txtNarration.Text.Trim(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                DataGridViewEx ex = this.fgDtls_f;
                double dblDedAmt = 0.0;

                for (int i = 0; i <= (ex.RowCount - 1); i++)
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
                                strAdjQry = strAdjQry + DBSp.InsertInto_AcLedger("(#CodeID#)", Conversions.ToString((int)(i + 3)), "(#ENTRYNO#)", this.dtEntryDate.Text,
                                           (double)base.iIDentity, Conversions.ToString(Localization.ParseNativeInt(row.Cells[2].Value.ToString())), 2,
                                           Db_Detials.Ac_AdjType.OnAccount, "(#CodeID#)", this.txtBillNo.Text.Trim(), this.dtBillDate.Text, (double)base.iIDentity,
                                            new decimal(dblDedAmt), decimal.Zero, "null", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
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
                                           Db_Detials.Ac_AdjType.OnAccount, "(#CodeID#)", this.txtBillNo.Text.Trim(), this.dtBillDate.Text, (double)base.iIDentity,
                                           decimal.Zero, new decimal(dblDedAmt), "null", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                            }
                        }
                    }
                    row = null;
                }


                for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];

                    using (IDataReader iDr = DB.GetRS("SELECT * from tbl_CatalogMaster WHERE CatalogID=" + Localization.ParseNativeDouble(row.Cells[4].Value.ToString())))
                    {
                        if (iDr.Read())
                        {
                            //strAdjQry += DBSp.InsertIntoCatalogStockLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)",
                            //Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()), base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                            //txtBillNo.Text.Trim(), dtBillDate.Text, Localization.ParseNativeDouble(iDr["CatalogID"].ToString()),
                            //_Mtrs_Unit, Localization.ParseNativeDecimal(row.Cells[5].Value.ToString()), 0, "-", row.Cells[8].Value == null ? "" : row.Cells[8].Value.ToString(),
                            //Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date, 0);
                        }
                    }
                    row = null;
                }

                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                DBSp.Transcation_AddEdit(pArrayData, fgDtls, true, strAdjQry, "", txtEntryNo.Text, txtRefNo.Text, "RefNo", new DataGridViewEx[] { fgDtls_f });
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

                if ((txtBillNo.Text != null) && ((txtBillNo.Text.Trim().Length) > 0))
                {
                    string strTblName;
                    if (base.blnFormAction == 0)
                    {
                        strTblName = "tbl_CatalogSalesReturnMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "BillNo", txtBillNo.Text, false, "", 0, string.Format("CompID = {0} and YearID = {1} and StoreID = {2} and BranchID={3}", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.StoreID, Db_Detials.BranchID), "This Bill No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where BillNo = '{1}' And CompID = {2} and YearID = {3} and StoreID = {4} and BranchID={5}", new object[] { "tbl_CatalogSalesReturnMain", txtBillNo.Text.ToString(), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.StoreID, Db_Detials.BranchID }))))
                        {
                            txtBillNo.Focus();
                            return true;
                        }
                    }
                    else if (Localization.ParseNativeInt(base.blnFormAction.ToString()) == 1)
                    {
                        strTblName = "tbl_CatalogSalesReturnMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "BillNo", txtBillNo.Text, true, "CatSalesReturnACID", Localization.ParseNativeLong(txtCode.Text), string.Format("CompID = {0} and YearID = {1} and StoreID = {2} and BranchID={3}", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.StoreID, Db_Detials.BranchID), "This Bill No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where BillNo = '{1}' And CompID = {2} and YearID = {3} and StoreID = {4} and BranchID={5}", new object[] { "tbl_CatalogSalesReturnMain", txtBillNo.Text.ToString(), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.StoreID, Db_Detials.BranchID }))))
                        {
                            txtBillNo.Focus();
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

                if (cboDepartment.SelectedValue == null || cboDepartment.SelectedValue.ToString() == "-" || cboDepartment.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    cboDepartment.Focus();
                    return true;
                }

                if (cboParty.SelectedValue == null || cboParty.SelectedValue.ToString() == "-" || cboParty.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Party");
                    cboParty.Focus();
                    return true;
                }

                if (cboSalesAc.SelectedValue == null || cboSalesAc.SelectedValue.ToString() == "-" || cboSalesAc.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Sales Account");
                    cboSalesAc.Focus();
                    return true;
                }
                if (cboTransport.SelectedValue == null || cboTransport.SelectedValue.ToString() == "-" || cboTransport.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Transport");
                    cboTransport.Focus();
                    return true;
                }

                decimal CreditLimit = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select isnull(CreditLimit,0) From {0} Where LedgerId = {1} ", "tbl_LedgerMaster", (cboParty.SelectedValue))));
                decimal TotSalseValue;
                if (CreditLimit > 0)
                {
                    TotSalseValue = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("select sum(isnull(NetAmount,0)) From {0} Where LedgerID = {1} and CompID = {2} and YearID ={3}  and StoreID = {4} and BranchID={5}", new object[] { "tbl_CatalogSalesReturnMain", (this.cboParty.SelectedValue), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.StoreID, Db_Detials.BranchID })));
                    if (TotSalseValue + Localization.ParseNativeDecimal(txtNetAmt.Text) > CreditLimit)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Exceeding Credit Limit");
                        return true;
                    }
                }

                if (FS_BRK_COM == true)
                {
                    if (cboBroker.SelectedValue == null || cboBroker.SelectedValue.ToString() == "-" || cboBroker.SelectedValue.ToString() == "0")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Broker");
                        cboBroker.Focus();
                        return true;
                    }
                }
                if (TxtGrossAmount.Text.Trim() == "" || TxtGrossAmount.Text.Trim() == "-" || TxtGrossAmount.Text.Trim() == "0.00")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Gross Amount Details");
                    TxtGrossAmount.Focus();
                    return true;
                }
                if (txtNetAmt.Text.Trim() == "" || txtNetAmt.Text.Trim() == "-" || txtNetAmt.Text.Trim() == "0.00")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Net Amount Details");
                    txtNetAmt.Focus();
                    return true;
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

        #endregion

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (this.dtBillDate.Text != "__/__/____")
            {
                if (this.cboParty.SelectedValue != null)
                {
                    frmStockAdj adj2 = new frmStockAdj();
                    adj2.MenuID = base.iIDentity;
                    adj2.Entity_IsfFtr = 0.0;
                    adj2.ref_fgDtls = this.fgDtls;
                    adj2.AsonDate = Conversions.ToDate(this.dtBillDate.Text);
                    adj2.LedgerID = Conversions.ToString(this.cboParty.SelectedValue);
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
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Question, "", "Please Select Party to Fetch Stock");
                }
            }
            else
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Question, "", "Please Enter Bill Date to Fetch Stock");
            }
            fgDtls.Select();
        }

        private void CalcVal()
        {
            TxtTotalPcs.Text = string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 5, -1, -1));
            TxtGrossAmount.Text = string.Format("{0:N2}", Math.Round(CommonCls.GetColSum(this.fgDtls, 7, -1, -1)));
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

        private void cboParty_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboParty.SelectedValue != null && Localization.ParseNativeDouble(cboParty.SelectedValue.ToString()) > 0.0)
                {
                    cboBroker.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select BrokerID From {0} Where LedgerID = {1}", "fn_LedgerMaster_Tbl()", (cboParty.SelectedValue))));
                    cboSalesAc.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select PurchSalesID From {0} Where LedgerID = {1}", "fn_LedgerMaster_Tbl()", (cboParty.SelectedValue))));
                    cboTransport.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select TransportId From {0} Where LedgerID = {1}", "fn_LedgerMaster_Tbl()", (cboParty.SelectedValue))));
                }
            }
            catch { }
        }

        private void cboSalesAc_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (!((base.blnFormAction == Enum_Define.ActionType.View_Record) | (base.blnFormAction == Enum_Define.ActionType.Not_Active)))
                {
                    DataGridViewEx ex = this.fgDtls_f;
                    int VatType = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("select VATTypeId from fn_LedgerMaster_Tbl() where LedgerId={0}", (cboSalesAc.SelectedValue))));
                    if (VatType != 0)
                    {
                        using (IDataReader reader = DB.GetRS(string.Format("select LedgerName, Percentage from fn_LedgerMaster_Tbl() where VATTypeId={0} and LedgerGroupId=25", VatType)))
                        {
                            if (reader.Read())
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[2].Value = reader["LedgerName"].ToString();
                                ex.Rows[ex.CurrentRow.Index].Cells[3].Value = "+";
                                ex.Rows[ex.CurrentRow.Index].Cells[4].Value = Localization.ParseNativeDecimal(reader["Percentage"].ToString());
                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = ((Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(reader["Percentage"].ToString())) / 100);
                            }
                        }
                    }
                    else
                    {
                        ex.Rows[ex.CurrentRow.Index].Cells[2].Value = "";
                        ex.Rows[ex.CurrentRow.Index].Cells[3].Value = "+";
                        ex.Rows[ex.CurrentRow.Index].Cells[4].Value = 0.0;
                        ex.Rows[ex.CurrentRow.Index].Cells[5].Value = 0.0;
                    }
                }
            }
            catch { }
        }

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewEx ex = this.fgDtls_f;
                if (!((base.blnFormAction == Enum_Define.ActionType.View_Record) || (base.blnFormAction == Enum_Define.ActionType.Not_Active)))
                {
                    if (((e.ColumnIndex == 5) | (e.ColumnIndex == 6)) | (e.ColumnIndex == 7))
                    {
                        CalcVal();
                    }
                    switch (e.ColumnIndex)
                    {
                        case 6:
                            fgDtls.Rows[e.RowIndex].Cells[7].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString())).ToString()));
                            for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                            {
                                if (ex.Rows[i].Cells[4].Value != null && ex.Rows[i].Cells[4].Value.ToString() != "0")
                                {
                                    ex.Rows[i].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[i].Cells[4].Value))), 100M).ToString().Replace(",", "");
                                    CalcVal();
                                }
                            }
                            break;

                        case 5:
                            if (Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString())).ToString())) != Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[7].Value == null ? "0" : fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString()))
                            {
                                fgDtls.Rows[e.RowIndex].Cells[7].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString())).ToString()));
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

                        case 7:
                            if (Localization.ParseNativeDouble(Operators.DivideObject(fgDtls.Rows[e.RowIndex].Cells[7].Value, fgDtls.Rows[e.RowIndex].Cells[6].Value).ToString()) != Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString()))
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString() != null && Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString()) != 0)
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[6].Value = Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString());
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
                    }
                    CalcVal();
                }
            }
            catch { }
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
            catch { }
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
            catch { }

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
            catch
            {
            }
        }

        public void PrintRecord()
        {
            CIS_ReportTool.frmMultiPrint frmMultiPrint = new CIS_ReportTool.frmMultiPrint();
            CIS_ReportTool.frmMultiPrint.MenuID = base.iIDentity;
            CIS_ReportTool.frmMultiPrint.Id = Localization.ParseNativeInt(txtCode.Text);
            CIS_ReportTool.frmMultiPrint.TblNm = "tbl_CatalogSalesReturnMain";
            CIS_ReportTool.frmMultiPrint.IdStr = "CatSalesReturnACID";
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

        public string SetBillNo
        {
            get
            {
                return txtBillNo.Text.ToString();
            }
            set
            {
                if (value.Length != 0)
                {
                    txtBillNo.Text = value;
                }
            }
        }

        public string setParty
        {
            get
            {
                return cboParty.SelectedValue.ToString();
            }
            set
            {
                if (value.Length != 0)
                {
                    cboParty.SelectedValue = value;
                }
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
                        if (cboReturnType.SelectedItem.ToString() == "[2, Other]")
                        {
                            fgDtls.Rows[i].Cells[3].ReadOnly = false;
                            fgDtls.Rows[i].Cells[4].ReadOnly = false;
                            fgDtls.Rows[i].Cells[5].ReadOnly = false;
                        }
                        else
                        {
                            fgDtls.Rows[i].Cells[3].ReadOnly = true;
                            fgDtls.Rows[i].Cells[4].ReadOnly = true;
                            fgDtls.Rows[i].Cells[5].ReadOnly = true;
                        }
                    }
                }
            }
            catch { }
        }
    }
}
