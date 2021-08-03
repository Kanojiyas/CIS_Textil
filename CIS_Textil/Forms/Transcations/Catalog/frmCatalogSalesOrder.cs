using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmCatalogSalesOrder : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        private bool F_SO;
        private bool flg_Sms;
        private bool flg_Email;

        public frmCatalogSalesOrder()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
            F_SO = false;
        }

        private void frmCatalogSalesOrder_Load(object sender, EventArgs e)
        {
            try
            {
                F_SO = Localization.ParseBoolean(GlobalVariables.FSO);

                if (!F_SO)
                {
                    lblInternalOrderNo.Visible = false;
                    lblCol.Visible = false;
                    cboDsgnONo.Visible = false;
                }
                else
                {
                    lblInternalOrderNo.Visible = true;
                    lblCol.Visible = true;
                    cboDsgnONo.Visible = true;
                    string sqlQuery = string.Format("select distinct FabricDesignNo as 'OrderID' , FabricDesignNo as 'OrderNo' from tbl_FabricDesignMaster where StoreID = {0} and CompID = {1} and YearID = {2} and FabricDesignNo is not null order by FabricDesignNo ", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID);
                    Combobox_Setup.Fill_Combo(cboDsgnONo, sqlQuery, "OrderNO", "OrderID");
                    cboDsgnONo.ColumnWidths = "0;150";
                    cboDsgnONo.AutoComplete = true;
                    cboDsgnONo.AutoComplete = true;
                    cboDsgnONo.SelectedIndex = -1;
                }

                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboPartyName, Combobox_Setup.ComboType.Mst_Customer, "");
                Combobox_Setup.FillCbo(ref cboBroker, Combobox_Setup.ComboType.Mst_Brokers, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
                Combobox_Setup.FillCbo(ref cboDelivaryAt, Combobox_Setup.ComboType.Mst_Haste, "");
                //Combobox_Setup.FillCbo(ref cboDelivaryAt, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboOrderStatus, Combobox_Setup.ComboType.Mst_OrderStatus, "");
                cboOrderStatus.SelectedValue = DB.GetSnglValue("Select MiscID from fn_MiscMaster_Tbl() Where MiscType='Order Status' AND MiscName='Pending'");
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);

                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(fgDtls_CellValueChanged);
                this.fgDtls.KeyDown += new KeyEventHandler(this.fgdtls_KeyDown);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #region Navigation

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                txtOrderNo.Text = CommonCls.AutoInc(this, "CatSONo", "CatSOID", "");
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                int MaxID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Isnull(Max(CatSOID),0) From {0}  Where StoreID={1} and CompID = {2} and BranchID={3} and YearID = {4}", "tbl_CatalogSalesOrderMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID)));
                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where CatSOID= {1} and StoreID={2} and CompID={3} and BranchID={4} and YearID={5}", new object[] { "tbl_CatalogSalesOrderMain", MaxID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                        dtOrderDate.Text = Localization.ToVBDateString(reader["CatSODate"].ToString());
                        cboPartyName.SelectedValue = Localization.ParseNativeInt(reader["PartyID"].ToString());
                        cboBroker.SelectedValue = Localization.ParseNativeInt(reader["BrokerID"].ToString());
                        cboTransport.SelectedValue = Localization.ParseNativeInt(reader["TransportID"].ToString());
                        cboDelivaryAt.SelectedValue = Localization.ParseNativeInt(reader["DelivaryAtID"].ToString());
                    }
                }
                dtEntryDate.Focus();
                dtEntryDate.Text = Conversions.ToString(DateAndTime.Now.Date);
                lblTotalQty.Text = "0";
                lblTotalAmt.Text = "0.00";
                cboDsgnONo.Enabled = true;
                string sqlQuery = string.Format("select distinct FabricDesignNo as 'OrderID', FabricDesignNo as 'OrderNo' from tbl_FabricDesignMaster where StoreID = {0} and CompID = {1} and YearID = {2} and FabricDesignNo is not null and FabricDesignNo not in (Select InternalSONo From tbl_CatalogSalesOrderMain where StoreID={0} and CompID = {1} and YearID = {2} and BranchID={3}) order by FabricDesignNo", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID);
                Combobox_Setup.Fill_Combo(cboDsgnONo, sqlQuery, "OrderNO", "OrderID");
                cboDsgnONo.ColumnWidths = "0;150";
                cboDsgnONo.AutoComplete = true;
                cboDsgnONo.AutoComplete = true;
                cboDsgnONo.SelectedIndex = -1;

                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    try
                    {
                        cboOrderStatus.SelectedValue = DB.GetSnglValue("Select MiscID from fn_MiscMaster() Where MiscType='Order Status' AND MiscName='Pending'");
                        cboOrderStatus.Enabled = false;
                    }
                    catch { }
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
            cboPartyName.Enabled = true;
        }

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "CatSOID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtOrderNo, "CatSONo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtOrderDate, "CatSODate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboPartyName, "PartyID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDelivaryAt, "DelivaryAtID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDsgnONo, "InternalSONo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCreditdays, "CrDays", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboOrderStatus, "OrderStatusID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, lblTotalQty, "TotQty", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, lblTotalAmt, "TotAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtED1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "CatSOID", txtCode.Text, base.dt_HasDtls_Grd);
                string sqlQuery = string.Format("select distinct FabricDesignNo as 'OrderID' , FabricDesignNo as 'OrderNo' from tbl_FabricDesignMaster where StoreID = {0} and CompID = {1} and YearID = {2} and FabricDesignNo is not null order by FabricDesignNo ", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID);
                Combobox_Setup.Fill_Combo(cboDsgnONo, sqlQuery, "OrderNO", "OrderID");
                cboDsgnONo.ColumnWidths = "0;150";
                cboDsgnONo.AutoComplete = true;
                cboDsgnONo.AutoComplete = true;
                cboDsgnONo.SelectedIndex = -1;

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                }
                if ((base.blnFormAction == Enum_Define.ActionType.View_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    cboDsgnONo.SelectedValue = DB.GetSnglValue(string.Format("Select InternalSONo from {0} Where CatSOID = {1} and StoreID={2} and CompID={3} and BranchID={4} and YearID={5}", new object[] { "tbl_CatalogSalesOrderMain", Localization.ParseNativeDouble(txtCode.Text), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })).ToString();
                    cboDsgnONo.Enabled = false;

                    cboOrderStatus.Enabled = true;
                }
                else
                {
                    cboDsgnONo.Enabled = true;
                }
                CalcVal();

                System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                dgvCellStyle.BackColor = System.Drawing.Color.LightGray;
                dgvCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
                dgvCellStyle.SelectionBackColor = System.Drawing.Color.Purple;
                dgvCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

                try
                {
                    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    {
                        if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_UnionofCatalogSales(" + Db_Detials.StoreID + "," + Db_Detials.CompID + "," + Db_Detials.BranchID + "," + Db_Detials.YearID + ") WHERE OrderNo='" + txtOrderNo.Text + "'" + " and CatalogID=" + Localization.ParseNativeInt(fgDtls.Rows[i].Cells[2].Value.ToString()) + " ")) > 0)
                        {

                            fgDtls.Rows[i].ReadOnly = true;
                            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle;
                        }
                        else
                        {
                            fgDtls.Rows[i].ReadOnly = false;
                        }
                    }
                }
                catch { }

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    bool RtnValue = Localization.ParseBoolean(DB.GetSnglValue("Select [dbo].[fn_ChkDel_FabricSaleOrder](" + txtCode.Text + ")"));
                    if (RtnValue == true)
                    {
                        cboPartyName.Enabled = false;
                    }
                    else
                    {
                        cboPartyName.Enabled = true;
                    }
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
                    "(#ENTRYNO#)",
                    dtEntryDate.TextFormat(false, true),
                    "(#OTHERNO#)",
                    dtOrderDate.TextFormat(false, true),
                    cboPartyName.SelectedValue,
                    cboBroker.SelectedValue,
                    cboTransport.SelectedValue,
                    cboDelivaryAt.SelectedValue,
                    cboDsgnONo.SelectedValue,
                    txtCreditdays.Text,
                    cboOrderStatus.SelectedValue,
                    lblTotalQty.Text.ToString().Replace(",", ""),
                    lblTotalAmt.Text.ToString().Replace(",", ""),
                    txtDescription.Text,
                    (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                    (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                    (dtED1.TextFormat(false, true)),
                    (txtET1.Text.Trim()),
                    (txtET2.Text.Trim()),
                    (txtET3.Text.Trim())
                };

                string sBatchNo = string.Empty;
                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_CatalogOrderLedger", "(#CodeID#)", Localization.ParseNativeInt(base.iIDentity.ToString()));
                for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    if (row.Cells[2].Value != null && row.Cells[2].Value.ToString() != "" && row.Cells[4].Value != null && row.Cells[4].Value.ToString() != "")
                    {
                        if (Localization.ParseNativeDouble(row.Cells[4].Value.ToString()) > 0)
                        {
                            if (row.Cells[9].Value != null && row.Cells[9].Value.ToString() != "")
                            {
                                if (Localization.ParseNativeInt(row.Cells[9].Value.ToString()) == Localization.ParseNativeInt(DB.GetSnglValue("Select MiscID from fn_MiscMaster_tbl()  Where MiscType='Order Status' AND MiscName='Approved'")))
                                {
                                    strAdjQry += DBSp.InsertIntoCatalogOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                       "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtOrderDate.Text, Localization.ParseNativeDouble(cboPartyName.SelectedValue.ToString()),
                                       base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(), "0", "(#CodeID#)", txtOrderNo.Text,
                                       Localization.ParseNativeDouble(row.Cells[2].Value.ToString()), 0, Localization.ParseNativeDecimal(row.Cells[4].Value.ToString()), 0, 
                                       row.Cells[5].Value == null || row.Cells[5].Value.ToString() == "" ? 0 : Localization.ParseNativeDecimal(row.Cells[5].Value.ToString()),
                                       row.Cells[8].Value == null || row.Cells[8].Value.ToString() == "" || row.Cells[8].Value.ToString() == "0" ? "NULL" : Convert.ToString(row.Cells[8].Value),
                                       0,
                                       row.Cells[11].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[11].Value.ToString()),
                                       row.Cells[12].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[12].Value.ToString()),
                                       row.Cells[13].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                                       row.Cells[14].Value == null || row.Cells[14].Value.ToString() == "" || row.Cells[14].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[14].Value.ToString()),
                                       row.Cells[15].Value == null || row.Cells[15].Value.ToString() == "" || row.Cells[15].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[15].Value.ToString()),
                                       row.Cells[16].Value == null || row.Cells[16].Value.ToString() == "" ? "-" : row.Cells[16].Value.ToString(),
                                       row.Cells[17].Value == null || row.Cells[17].Value.ToString() == "" ? "-" : row.Cells[17].Value.ToString(),
                                       row.Cells[18].Value == null || row.Cells[18].Value.ToString() == "" ? "-" : row.Cells[18].Value.ToString(),
                                       row.Cells[19].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[19].Value.ToString()),
                                       row.Cells[20].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[20].Value.ToString()),
                                       "NULL", i, 1, "Sales", this.frmVoucherTypeID,
                                       Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                                }
                                else if (Localization.ParseNativeInt(row.Cells[9].Value.ToString()) == Localization.ParseNativeInt(DB.GetSnglValue("Select MiscID from fn_MiscMaster_tbl()  Where MiscType='Order Status' AND MiscName='Completed'")))
                                {
                                    decimal dBalQty = Localization.ParseNativeDecimal(DB.GetSnglValue("Select BalPcs from  fn_FetchCatalogOrders(" + "'" + Localization.ToSqlDateString(DateTime.Now.ToString()) + "'" + "," +  Db_Detials.StoreID + "," + Db_Detials.CompID + "," + Db_Detials.BranchID+ "," + Db_Detials.YearID + "," + cboPartyName.SelectedValue.ToString() + ")" + " Where ARefID= '" + base.iIDentity.ToString() + "|" + txtCode.Text + "|" + (i + 1).ToString() + "'"));

                                    strAdjQry += DBSp.InsertIntoCatalogOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                       "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtOrderDate.Text, Localization.ParseNativeDouble(cboPartyName.SelectedValue.ToString()),
                                       base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(), "0", "(#CodeID#)", txtOrderNo.Text,
                                       Localization.ParseNativeDouble(row.Cells[2].Value.ToString()), 0, Localization.ParseNativeDecimal(row.Cells[4].Value.ToString()), 0,
                                       row.Cells[5].Value == null || row.Cells[5].Value.ToString() == "" ? 0 : Localization.ParseNativeDecimal(row.Cells[5].Value.ToString()),
                                       "On Completion Dr Side From Sales Order Only", 0,
                                       row.Cells[11].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[11].Value.ToString()),
                                       row.Cells[12].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[12].Value.ToString()),
                                       row.Cells[13].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                                       row.Cells[14].Value == null || row.Cells[14].Value.ToString() == "" || row.Cells[14].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[14].Value.ToString()),
                                       row.Cells[15].Value == null || row.Cells[15].Value.ToString() == "" || row.Cells[15].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[15].Value.ToString()),
                                       row.Cells[16].Value == null || row.Cells[16].Value.ToString() == "" ? "-" : row.Cells[16].Value.ToString(),
                                       row.Cells[17].Value == null || row.Cells[17].Value.ToString() == "" ? "-" : row.Cells[17].Value.ToString(),
                                       row.Cells[18].Value == null || row.Cells[18].Value.ToString() == "" ? "-" : row.Cells[18].Value.ToString(),
                                       row.Cells[19].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[19].Value.ToString()),
                                       row.Cells[20].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[20].Value.ToString()),
                                       "NULL", i, 1, "Sales", this.frmVoucherTypeID,
                                       Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);

                                    if (dBalQty == 0)
                                    {
                                        dBalQty = Localization.ParseNativeDecimal(row.Cells[4].Value.ToString());
                                    }

                                    strAdjQry += DBSp.InsertIntoCatalogOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                     "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtOrderDate.Text, Localization.ParseNativeDouble(cboPartyName.SelectedValue.ToString()),
                                     base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(), "0", "(#CodeID#)", txtOrderNo.Text,
                                     Localization.ParseNativeDouble(row.Cells[2].Value.ToString()), 0, 0, dBalQty,
                                     row.Cells[5].Value == null || row.Cells[5].Value.ToString() == "" ? 0 : Localization.ParseNativeDecimal(row.Cells[5].Value.ToString()),
                                     "On Completion Cr Side From Sales Order Only", 0,
                                     row.Cells[11].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[11].Value.ToString()),
                                     row.Cells[12].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[12].Value.ToString()),
                                     row.Cells[13].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                                     row.Cells[14].Value == null || row.Cells[14].Value.ToString() == "" || row.Cells[14].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[14].Value.ToString()),
                                     row.Cells[15].Value == null || row.Cells[15].Value.ToString() == "" || row.Cells[15].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[15].Value.ToString()),
                                     row.Cells[16].Value == null || row.Cells[16].Value.ToString() == "" ? "-" : row.Cells[16].Value.ToString(),
                                     row.Cells[17].Value == null || row.Cells[17].Value.ToString() == "" ? "-" : row.Cells[17].Value.ToString(),
                                     row.Cells[18].Value == null || row.Cells[18].Value.ToString() == "" ? "-" : row.Cells[18].Value.ToString(),
                                     row.Cells[19].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[19].Value.ToString()),
                                     row.Cells[20].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[20].Value.ToString()),
                                     "NULL", i, 1, "Sales", this.frmVoucherTypeID,
                                     Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                                }
                            }
                        }
                        row = null;
                    }
                }

                double dblTransID = 0;
                string sPartyID = cboPartyName.SelectedValue.ToString();
                DBSp.Transcation_AddEdit(pArrayData, this.fgDtls, true, strAdjQry, "", txtEntryNo.Text, txtOrderNo.Text, "CatSONo");

                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) || (base.blnFormAction == Enum_Define.ActionType.View_Record))
                {
                    flg_Sms = Localization.ParseBoolean(GlobalVariables.SMS_SEND_BookSO);
                    flg_Email = Localization.ParseBoolean(GlobalVariables.EMAIL_SEND_BookSO);

                    if (blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        string sEntryNo = DB.GetSnglValue("SELECT EntryNo from fn_CatalogSalesOrderMain_Tbl() WHERE CatSOID=" + dblTransID);
                        if (flg_Sms == true || flg_Email == true)
                        {
                            if (flg_Sms == true)
                            {
                                try { CommonCls.SendSms(dblTransID.ToString(), base.iIDentity.ToString(),1,sPartyID); }
                                catch { }
                            }

                            if (flg_Email == true)
                            {
                                try
                                {
                                    CommonCls.sendEmail(dblTransID.ToString(), sEntryNo, sPartyID, base.iIDentity.ToString(), false, 0);
                                }
                                catch { }
                            }
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                    {
                        if (flg_Email == true)
                        {
                            string sisactive = DB.GetSnglValue("Select IsActive from tbl_MailingConfig where menuid=" + base.iIDentity.ToString());
                            if (sisactive == "True")
                            {
                                try
                                {
                                    CommonCls.sendEmail(txtCode.Text, txtEntryNo.Text, sPartyID, base.iIDentity.ToString(), true, 0);
                                }
                                catch { }
                            }
                        }
                    }
                }

                string sqlQuery = string.Format("select distinct FabricDesignNo as 'OrderID', FabricDesignNo as 'OrderNo' from tbl_FabricDesignMaster where StoreID = {0} and CompID = {1} and YearID = {2} and FabricDesignNo is not null and FabricDesignNo not in (Select InternalSONo From tbl_FabricSalesOrderMain where StoreID = {0} and CompID = {1} and YearID = {2} and BranchID = {3}) order by FabricDesignNo", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID);
                Combobox_Setup.Fill_Combo(cboDsgnONo, sqlQuery, "OrderNO", "OrderID");
                cboDsgnONo.ColumnWidths = "0;150";
                cboDsgnONo.AutoComplete = true;
                cboDsgnONo.AutoComplete = true;
                cboDsgnONo.SelectedIndex = -1;
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        #endregion

        #region Validation

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
                if (!Information.IsDate(dtEntryDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry Date");
                    dtEntryDate.Focus();
                    return true;
                }
                if (txtOrderNo.Text.Trim() == "" || txtOrderNo.Text.Trim() == "-" || txtOrderNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Order No.");
                    txtOrderNo.Focus();
                    return true;
                }
                if (txtOrderNo.Text.Trim().Length > 0)
                {
                    string strTblName;
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        strTblName = "tbl_CatalogSalesOrderMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "CatSONo", this.txtOrderNo.Text, false, "", 0, "PartyID =" + cboPartyName.SelectedValue + " AND StoreID = " + Db_Detials.StoreID +  " AND CompID = " + Db_Detials.CompID + " AND BranchID = " + Db_Detials.BranchID + " And YearID =" + Db_Detials.YearID + "", "This Party already used this Sales Order No in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where CatSONo = '{1}' and StoreID = {2} and CompID = {3} and BranchID = {4} and YearID = {5} ", new object[] { "tbl_CatalogSalesOrderMain", txtOrderNo.Text.ToString(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }))))
                        {
                            txtOrderNo.Focus();
                            return true;
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        strTblName = "tbl_CatalogSalesOrderMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "CatSONo", txtOrderNo.Text, true, "CatSOID", Localization.ParseNativeLong(txtCode.Text), "PartyID =" + cboPartyName.SelectedValue + " AND StoreID = " + Db_Detials.StoreID + " AND CompID = " + Db_Detials.CompID + " AND BranchID = " + Db_Detials.BranchID + " And YearID =" + Db_Detials.YearID + "", "This Party already used this Sales Order No in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where CatSONo = '{1}' and StoreID = {2} and CompID = {3} and BranchID = {4} and YearID = {5} ", new object[] { "tbl_CatalogSalesOrderMain", this.txtOrderNo.Text.ToString(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }))))
                        {
                            txtOrderNo.Focus();
                            return true;
                        }
                    }
                }

                if (!Information.IsDate(dtOrderDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Order Date");
                    dtOrderDate.Focus();
                    return true;
                }
                if (cboPartyName.SelectedValue == null || cboPartyName.SelectedValue.ToString() == "-" || cboPartyName.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Party ");
                    cboPartyName.Focus();
                    return true;
                }
                CalcVal();
                return false;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                return false;
            }
        }

        private void CalcVal()
        {
            try
            {
                lblTotalQty.Text = string.Format("{0:N0}", CommonCls.GetColSum(fgDtls, 3, -1, -1));
                lblTotalAmt.Text = string.Format("{0:N2}", CommonCls.GetColSum(fgDtls, 5, -1, -1));
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
                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    if (((e.ColumnIndex == 3) | (e.ColumnIndex == 5)) | (e.ColumnIndex == 6))
                    {
                        CalcVal();
                    }
                    else if ((e.ColumnIndex == 3) && ((cboPartyName.SelectedValue != null) & (fgDtls.Rows[e.RowIndex].Cells[4].Value != null)))
                    {
                        lblLastOrderRate.Text = DB.GetSnglValue(string.Format("Select [dbo].[fn_FetchLastOrderRate]({0},0,{1},0,{2},{3},{4},{5})", new object[] { cboPartyName.SelectedValue, fgDtls.Rows[e.RowIndex].Cells[4].Value, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }));
                    }

                    if ((fgDtls.Rows[e.RowIndex].Cells[5].Value != null) && (fgDtls.Rows[e.RowIndex].Cells[4].Value != null) && (fgDtls.Rows[e.RowIndex].Cells[6].Value != null))
                    {
                        if (Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString())).ToString())) != Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[6].Value == null ? "0" : fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString()))
                        {
                            // fgDtls.Rows[e.RowIndex].Cells[6].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString())).ToString()));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgdtls_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                object frm = Navigate.GetActiveChild();
                dynamic frmObj = frm;
                int iCalcCol = 0;

                if ((e.Control == true & e.KeyCode == Keys.D) | e.KeyCode == Keys.F5)
                {
                    if (fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[2].Value != null)
                    {
                        if ((GlobalVariables.VALIDATE_EDIT == "TRUE") && (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_UnionofCatalogSales(" + Db_Detials.StoreID + "," + Db_Detials.CompID + "," + Db_Detials.BranchID + "," + Db_Detials.YearID + ") WHERE OrderNo='" + txtOrderNo.Text + "'" + " and CatalogID=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[2].Value.ToString()) + " ")) > 0))
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityError, "Referance Found", "Referance Found in Other Transaction, This Row Cannot be Delete...");
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

        private void cboPartyName_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                bool strQry;
                strQry = Localization.ParseBoolean(DB.GetSnglValue("Select Count(Blocked) from fn_LedgerMaster_Tbl()  Where LedgerID=" + cboPartyName.SelectedValue + " and Blocked='Yes'"));

                if (strQry)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Selected Party Is Blocked.");
                }

                if (cboPartyName.SelectedValue != null && cboPartyName.SelectedValue.ToString() != "0")
                {
                    cboBroker.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select BrokerID From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboPartyName.SelectedValue)));
                    cboTransport.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select TransportId From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboPartyName.SelectedValue)));
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboOrderStatus_SelectedValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                var _with1 = fgDtls;
                for (int i = 0; i <= _with1.RowCount - 1; i++)
                {
                    _with1.Rows[i].Cells[9].Value = cboOrderStatus.SelectedValue;
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
                CIS_ReportTool.frmMultiPrint.TblNm = "tbl_CatalogSalesOrderMain";
                CIS_ReportTool.frmMultiPrint.IdStr = "CatSOID";
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

        protected void fgDtls_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                if ((e.Control == true & e.KeyCode == Keys.D) | e.KeyCode == Keys.F5)
                {
                    //-- Calc Values
                    object frm = Navigate.GetActiveChild();
                    dynamic frmObj = frm;
                    int iCalcCol = 0;
                    CIS_DataGridViewEx.DataGridViewEx fgDtls = (CIS_DataGridViewEx.DataGridViewEx)sender;

                    if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        try
                        {
                            if (Localization.ParseNativeInt(DB.GetSnglValue(string.Format("SELECT count(0) from {0}({1},{2},{3},{4}) WHERE OrderID={5} and and CatalogID={6}", "fn_CatalogSalesDtls_FR", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, txtCode.Text, Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[2].Value.ToString())))) > 0)
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityError, "", "Reference Found In Another Module..Row Cannot Be Deleted");
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
                            }
                        }
                        catch { }
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

                        }
                        catch { }
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
