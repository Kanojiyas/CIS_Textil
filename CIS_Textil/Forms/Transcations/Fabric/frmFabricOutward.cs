using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_CLibrary;
using CIS_DBLayer;
using CIS_Utilities;
using CIS_Bussiness;
using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class frmFabricOutward : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;
        public double SalesTrnsID;
        public static bool FAB_MAINTAINWEIGHT;
        private bool FDC_ORD_WISE;
        private bool flg_IsBarcodeScan;
        private bool flg_MTY_DC;
        private bool flg_OrderConform;
        private bool flg_SUB_ORDER;
        public ArrayList OrgInGridArray;
        bool FDC_ORD_COMP = false;
        bool FDC_BRK_COM = false;
        bool FDC_RATETYPE = false;
        bool OVERDUE_ALT = false;
        private string SRateCalcType = string.Empty;
        private bool flg_Series;
        private bool flg_Email;
        private bool flg_Sms;
        public string strUniqueID;
        private int RefMenuID;
        private static string RefVoucherID;
        private string sPrevPartyID = string.Empty;
        private int iMaxMyID_Orders;
        private int iMaxMyID_Stock;
        private bool isRowDel = false;
        private int iTempDel_RowIndex;
        int iGrossRate = 0;
        private bool CellReadOnly = false;

        public frmFabricOutward()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
            OrgInGridArray = new ArrayList();
            flg_OrderConform = false;
            flg_SUB_ORDER = false;
            FDC_ORD_WISE = false;
        }

        #region Event

        private void frmFabricOutward_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboDepartment, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboParty, Combobox_Setup.ComboType.Mst_Customer, "");
                Combobox_Setup.FillCbo(ref cboOrderParty, Combobox_Setup.ComboType.Mst_Customer, "");
                Combobox_Setup.FillCbo(ref cboBroker, Combobox_Setup.ComboType.Mst_Brokers, "");
                Combobox_Setup.FillCbo(ref cboHaste, Combobox_Setup.ComboType.Mst_Haste, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
                Combobox_Setup.FillCbo(ref cboDeliveryAt, Combobox_Setup.ComboType.Mst_Ledger, "");
                Combobox_Setup.FillCbo(ref cboSalesAc, Combobox_Setup.ComboType.SalesAc, "");

                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);

                FAB_MAINTAINWEIGHT = Localization.ParseBoolean(GlobalVariables.FAB_MAINTAINWEIGHT);
                txtEntryNo.Enabled = false;

                flg_IsBarcodeScan = Localization.ParseBoolean(GlobalVariables.BR_Scan_Chln);
                if (flg_IsBarcodeScan)
                {
                    txtScan.Visible = true;
                    lblScan.Visible = true;
                    lblscan1.Visible = true;
                }
                else
                {
                    txtScan.Visible = false;
                    lblScan.Visible = false;
                    lblscan1.Visible = false;
                }

                FDC_ORD_COMP = Localization.ParseBoolean(GlobalVariables.FDC_ORD_COMP);
                FDC_ORD_WISE = Localization.ParseBoolean(GlobalVariables.FDC_ORD_WISE);

                if (FDC_ORD_WISE)
                {
                    btnShowOrder.Visible = true;
                    cboOrderType.Enabled = true;
                    cboOrderType.SelectedItem = "WITH ORDER";
                }
                else
                {
                    btnShowOrder.Visible = false;
                    cboOrderType.Enabled = false;
                    cboOrderType.SelectedItem = "WITHOUT ORDER";
                }

                flg_MTY_DC = Localization.ParseBoolean(GlobalVariables.MTY_DC);
                FDC_BRK_COM = Localization.ParseBoolean(GlobalVariables.FDC_BRK_COM);
                FDC_RATETYPE = Localization.ParseBoolean(GlobalVariables.FDC_RATETYPE);
                OVERDUE_ALT = Localization.ParseBoolean(GlobalVariables.OVERDUE_ALT);
                flg_Series = Localization.ParseBoolean(GlobalVariables.flg_Series);

                this.cboParty.SelectedIndexChanged += new System.EventHandler(this.cboParty_SelectedValueChanged);
                this.cboDepartment.SelectedValueChanged += new System.EventHandler(this.cboDepartment_SelectedValueChanged);
                this.cboParty.Leave += new System.EventHandler(this.cboParty_Leave);
                this.cboOrderType.SelectedValueChanged += new System.EventHandler(this.cboOrderType_SelectedValueChanged);
                cboOrderType_SelectedValueChanged(null, null);
                this.cboParty.SelectedValueChanged += new System.EventHandler(this.cboParty_SelectedValueChanged);

                RefMenuID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MenuID from tbl_VoucherTypeMaster Where GenMenuID=" + base.iIDentity + "")));
                GetRefModID();

                this.fgdtls_f.KeyDown += new KeyEventHandler(this.fgDtls_KeyDown_Orders);
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
                DBValue.Return_DBValue(this, txtCode, "FabOutwardID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboSalesAc, "FabSalesACID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtChlnNo, "RefNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtChlnDate, "RefDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboParty, "PartyID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLrNo, "LRNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDepartment, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDeliveryAt, "DeliveryAtID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboHaste, "HasteID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransportMode, "TransportModeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtLrDate, "LRDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtBales, "NoofBales", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtBaleNo, "BaleNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAdditionalDesc, "AdditionalDes", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDesc2, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEd1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, chkIsInvoice, "IsMakeInvoice", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtUniqueID, "UniqueID", Enum_Define.ValidationType.Text);
                try
                {
                    string sOrderType = DBValue.Return_DBValue(this, "OrderType");
                    cboOrderType.SelectedItem = sOrderType;
                }
                catch { }

                try
                {
                    DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "FabOutwardID", txtCode.Text, base.dt_HasDtls_Grd);
                }
                catch { }

                if (cboParty.SelectedValue != null)
                {
                    if (Localization.ParseNativeDouble(cboParty.SelectedValue.ToString()) > 0.0)
                    {
                        string sqlQuery = string.Format("Select Distinct OrderNo, OrderID from {0}({1},{2},{3},{4},{5},{6})  Where OrderTransType='Sales' and  RefVoucherID in ({7})", new object[] { "fn_FetchFabricOrders", CommonLogic.SQuote(Localization.ToSqlDateString(dtChlnDate.Text.ToString())), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboParty.SelectedValue.ToString(), RefVoucherID });
                    }
                }

                AplySelectBtnEnbl();

                int icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_StockFabricLedger_tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    cboOrderType.Enabled = false;
                    cboParty.Enabled = false;
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
                            string strQry = string.Format("Update tbl_StockFabricLedger Set UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + ", StatusID=2 Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + "");
                            DB.ExecuteSQL(strQry);
                        }
                        else
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityShieldBlue, "Warning", "This Record Is Edited By Another User..");
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

                CellReadOnly = false;
                if ((base.blnFormAction == Enum_Define.ActionType.View_Record) && !(base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_StockFabricLedger_tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));
                }

                System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                dgvCellStyle.BackColor = System.Drawing.Color.LightGray;
                dgvCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
                dgvCellStyle.SelectionBackColor = System.Drawing.Color.Purple;
                dgvCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

                try
                {
                    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    {
                        if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_FabricSalesMain_tbl() WHERE FabOutwardID=" + fgDtls.Rows[i].Cells["FabOutwardID"].Value.ToString() + " and StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + "")) > 0)
                        {
                            fgDtls.Rows[i].ReadOnly = true;
                            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle;
                            CellReadOnly = true;
                        }
                        else
                        {
                            fgDtls.Rows[i].ReadOnly = false;
                        }
                    }
                }
                catch { }

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
                        else if (icount <= 0 && CellReadOnly == false)
                        {
                            btnSelect.Enabled = true;
                            fgDtls.Rows[i].ReadOnly = false;
                        }
                    }
                }
                catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }

                int icount_Orders = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_FabricOrderLedger_tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    cboOrderType.Enabled = false;
                    cboParty.Enabled = false;
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);

                    try
                    {
                        if (icount_Orders == 0)
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
                    cboParty.Enabled = true;
                }

                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    if (strUniqueID != null)
                    {
                        string strQry = string.Format("Delete From tbl_FabricOrderLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                        strQry = strQry + string.Format("Update  tbl_FabricOrderLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                        DB.ExecuteSQL(strQry);
                        strQry = string.Format("Update tbl_FabricOrderLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                        DB.ExecuteSQL(strQry);
                    }
                }

                if ((base.blnFormAction == Enum_Define.ActionType.View_Record) && !(base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    icount_Orders = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_FabricOrderLedger_tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));
                }

                System.Windows.Forms.DataGridViewCellStyle dgvCellStyle_Ref_Orders = new System.Windows.Forms.DataGridViewCellStyle();
                dgvCellStyle_Ref.BackColor = System.Drawing.Color.LightSteelBlue;
                dgvCellStyle_Ref.ForeColor = System.Drawing.SystemColors.WindowText;
                dgvCellStyle_Ref.SelectionBackColor = System.Drawing.Color.SteelBlue;
                dgvCellStyle_Ref.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
                try
                {
                    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    {
                        if (icount_Orders > 0)
                        {
                            btnShowOrder.Enabled = false;
                            fgDtls.Rows[i].ReadOnly = true;
                            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle_Ref_Orders;
                        }
                        else if (icount_Orders <= 0 && CellReadOnly == false)
                        {
                            btnShowOrder.Enabled = true;
                            fgDtls.Rows[i].ReadOnly = false;
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
                if (strUniqueID != null)
                {
                    string strQry = string.Format("Delete From tbl_FabricOrderLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                    strQry = strQry + string.Format("Update  tbl_FabricOrderLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                    DB.ExecuteSQL(strQry);
                    strQry = string.Format("Update tbl_FabricOrderLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                    DB.ExecuteSQL(strQry);
                    strQry = "";
                }

                txtCode.Text = "";
                flg_OrderConform = false;
                CommonCls.IncFieldID(this, ref txtEntryNo, "");

                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                dtEntryDate.Focus();
                GetRefModID();
                if (this.frmVoucherTypeID != 0)
                    txtChlnNo.Text = CommonCls.AutoInc(this, "RefNo", "FabOutwardID", string.Format("VoucherTypeID = '{0}'", this.frmVoucherTypeID));
                else
                    txtChlnNo.Text = CommonCls.AutoInc(this, "RefNo", "FabOutwardID", "");
                dtEntryDate.Text = Conversions.ToString(DateAndTime.Now.Date);
                int MaxID = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(FabOutwardID),0) From {0} where StoreID={1} and CompID={2} and BranchID={3} and YearID={4} ", "tbl_FabricOutwardMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID))));
                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where FabOutwardID = {1} and StoreID={2} and  CompID={3} and BranchID={4} and YearID={5}", new object[] { "tbl_FabricOutwardMain", MaxID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                        dtChlnDate.Text = Localization.ToVBDateString(reader["RefDate"].ToString());
                        cboParty.SelectedValue = Localization.ParseNativeInt(reader["PartyID"].ToString());
                        cboBroker.SelectedValue = Localization.ParseNativeInt(reader["BrokerID"].ToString());
                        cboDepartment.SelectedValue = Localization.ParseNativeInt(reader["DepartmentID"].ToString());
                        cboHaste.SelectedValue = Localization.ParseNativeInt(reader["HasteID"].ToString());
                        cboTransport.SelectedValue = Localization.ParseNativeInt(reader["TransportID"].ToString());
                        dtLrDate.Text = Localization.ToVBDateString(reader["LRDate"].ToString());

                        if (reader["OrderType"].ToString() != "")
                            cboOrderType.SelectedItem = reader["OrderType"].ToString();
                        else
                            cboOrderType.SelectedItem = "WITH ORDER";
                        cboOrderType.Enabled = false;
                    }
                }

                cboOrderType.Enabled = true;
                cboDepartment.Enabled = true;
                cboParty.Enabled = true;

                fgdtls_f.DataSource = null;
                txtUniqueID.Text = CommonCls.GenUniqueID();
                strUniqueID = txtUniqueID.Text;
                cboDepartment.Enabled = true;
                AplySelectBtnEnbl();
                if (fgdtls_f != null)
                    fgdtls_f_InitializeLayout(null, null);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
            sPrevPartyID = string.Empty;
        }

        public void SaveRecord()
        {
            int iFabIssueID_Main = 0;
            try
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    iFabIssueID_Main = Localization.ParseNativeInt(DB.GetSnglValue("Select max(FabricSalesID+1) from tbl_FabricSalesMain where IsDeleted=0"));
                }
                else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    iFabIssueID_Main = Localization.ParseNativeInt(DB.GetSnglValue("Select FabricSalesID from tbl_FabricSalesMain where FabricOutwardID= " + txtCode.Text + " and IsDeleted=0"));
                }
                ArrayList pArrayData = new ArrayList
                {
                    this.frmVoucherTypeID,
                    cboSalesAc.SelectedValue,
                    ("(#ENTRYNO#)"),
                    (dtEntryDate.TextFormat(false, true)),
                    (cboOrderType.SelectedItem),
                    ("(#OTHERNO#)"),
                    (dtChlnDate.TextFormat(false, true)),
                    (cboParty.SelectedValue),
                    (cboBroker.SelectedValue),
                    (cboHaste.SelectedValue),
                    (cboDepartment.SelectedValue.ToString()=="-"?null:cboDepartment.SelectedValue),
                    (cboDeliveryAt.SelectedValue),
                    (cboTransportMode.SelectedValue),
                    (cboTransport.SelectedValue),
                    (txtLrNo.Text.ToString()),
                    (dtLrDate.TextFormat(false, true)),
                    (txtBales.Text.ToString()),
                    (txtBaleNo.Text.ToString()),
                    (txtAdditionalDesc.Text.ToString()),
                    (string.Format("{0:N}", CommonCls.GetColSum(this.fgDtls, 15, -1, -1)).Replace(",", "")),
                    (string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 17, -1, -1)).Replace(",", "")),
                    (string.Format("{0:N3}", CommonCls.GetColSum(this.fgDtls, 18, -1, -1)).Replace(",", "")),
                     (string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 20, -1, -1)).Replace(",", "")),
                    (txtDesc2.Text.ToString()),
                    iFabIssueID_Main,
                    cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue,
                    cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue,
                    dtEd1.TextFormat(false,true), 
                    txtET1.Text,
                    txtET2.Text,
                    txtET3.Text,
                    chkIsInvoice.Checked==true?1:0,
                    txtUniqueID.Text
                };

                int departmentID = 0;
                int UnitID = 0;
                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", "(#CodeID#)", Localization.ParseNativeInt(Conversions.ToString(base.iIDentity)));
                for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    if (Localization.ParseNativeDouble(Conversions.ToString(row.Cells[17].Value)) != 0.0)
                    {
                        string LotNo = Conversions.ToString(row.Cells[3].Value);
                        if ((LotNo == null) || (LotNo.Trim()) == "" || (LotNo) == "-")
                        {
                            LotNo = "-";
                        }

                        strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(
                            Localization.ParseNativeDouble(base.iIDentity.ToString()),
                            "(#CodeID#)",
                            (i + 1).ToString(),
                            "(#ENTRYNO#)",
                            dtChlnDate.Text,
                            Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                            row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                            (row.Cells[46].Value.ToString().Trim() == "" ? "0" : row.Cells[46].Value.ToString()),
                            (row.Cells[48].Value.ToString().Trim() == "" ? "0" : row.Cells[48].Value.ToString()),
                            LotNo,
                            (row.Cells[4].Value.ToString().Trim() == null ? "-" : row.Cells[4].Value.ToString().Trim() == "" ? "-" : row.Cells[4].Value.ToString()),
                            row.Cells[5].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[5].Value.ToString()),
                            row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                            row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                            row.Cells[8].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                            row.Cells[13].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                            row.Cells[14].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[14].Value.ToString()),
                            0, 0, 0,
                            Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                            Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                            Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()),
                            (row.Cells[21].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[21].Value.ToString())),
                            row.Cells[27].Value == null ? "NULL" : row.Cells[27].Value.ToString(),
                            row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                            row.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[29].Value.ToString()),
                            row.Cells[30].Value == null ? "NULL" : row.Cells[30].Value.ToString(),
                            row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                            row.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[32].Value.ToString()),
                            row.Cells[33].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[33].Value.ToString()),
                            row.Cells[34].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[34].Value.ToString()),
                            row.Cells[35].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[35].Value.ToString()),
                            row.Cells[36].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[36].Value.ToString()),
                            row.Cells[37].Value == null || row.Cells[37].Value.ToString() == "" || row.Cells[37].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[37].Value.ToString()),
                            row.Cells[38].Value == null || row.Cells[38].Value.ToString() == "" || row.Cells[38].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[38].Value.ToString()),
                            row.Cells[39].Value == null || row.Cells[39].Value.ToString() == "" ? "-" : row.Cells[39].Value.ToString(),
                            row.Cells[40].Value == null || row.Cells[40].Value.ToString() == "" ? "-" : row.Cells[40].Value.ToString(),
                            row.Cells[41].Value == null || row.Cells[41].Value.ToString() == "" ? "-" : row.Cells[41].Value.ToString(),
                            row.Cells[42].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[42].Value.ToString()),
                            row.Cells[43].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[43].Value.ToString()),
                            "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);

                        UnitID = Localization.ParseNativeInt(row.Cells[14].Value.ToString());
                        departmentID = Localization.ParseNativeInt(row.Cells[25].Value.ToString());
                    }
                    row = null;
                }

                if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                {
                    strAdjQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_FabricOrderLedger", "(#CodeID#)", Localization.ParseNativeInt(base.iIDentity.ToString()));
                    for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                    {
                        DataGridViewRow row = fgDtls.Rows[i];
                        if (row.Cells[2].Value != null && row.Cells[2].Value.ToString() != "" && row.Cells[17].Value != null && row.Cells[17].Value.ToString() != "")
                        {
                            string sBatchNo = string.Empty;
                            sBatchNo = DB.GetSnglValue("Select FabSONo from fn_FabricSalesOrderMain_Tbl() Where FabSOID=" + row.Cells[2].Value.ToString());
                            if (Localization.ParseNativeDouble(row.Cells[17].Value.ToString()) > 0)
                            {
                                strAdjQry += DBSp.InsertIntoFabricOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                       "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)",
                                       dtChlnDate.Text, Localization.ParseNativeDouble(cboParty.SelectedValue.ToString()),
                                       row.Cells[46].Value == null ? "0" : row.Cells[46].Value.ToString() == "" ? "0" : row.Cells[46].Value.ToString(),
                                       "0", row.Cells[2].Value.ToString(), sBatchNo,
                                       Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[14].Value.ToString()),
                                       Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                       0, 0, 0,
                                       Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                                       Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                                       Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()),
                                       0, row.Cells[27].Value == null || row.Cells[27].Value.ToString() == "" || row.Cells[27].Value.ToString() == "0" ? "NULL" : Convert.ToString(row.Cells[27].Value), 0,
                                       row.Cells[34].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[34].Value.ToString()),
                                       row.Cells[35].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[35].Value.ToString()),
                                       row.Cells[36].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[36].Value.ToString()),
                                       row.Cells[37].Value == null || row.Cells[37].Value.ToString() == "" || row.Cells[37].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[37].Value.ToString()),
                                       row.Cells[38].Value == null || row.Cells[38].Value.ToString() == "" || row.Cells[38].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[38].Value.ToString()),
                                       row.Cells[39].Value == null || row.Cells[39].Value.ToString() == "" ? "-" : row.Cells[39].Value.ToString(),
                                       row.Cells[40].Value == null || row.Cells[40].Value.ToString() == "" ? "-" : row.Cells[40].Value.ToString(),
                                       row.Cells[41].Value == null || row.Cells[41].Value.ToString() == "" ? "-" : row.Cells[41].Value.ToString(),
                                       row.Cells[42].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[42].Value.ToString()),
                                       row.Cells[43].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[43].Value.ToString()),
                                       "NULL", i, 1, "Sales", row.Cells[44].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[44].Value.ToString()),
                                       Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                            }
                            row = null;
                        }
                    }
                }

                strAdjQry += "Delete From tbl_FabricOrderLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";";
                strAdjQry += "Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";";
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                int iOutwardId_ForEdit = Localization.ParseNativeInt(DB.GetSnglValue("Select FabricSalesID from tbl_FabricSalesMain where FabricOutwardID=" + txtCode.Text + " and IsDeleted=0"));
                double dblTransID = 0;
                string sPartyID = cboParty.SelectedValue.ToString();
                DBSp.Transcation_AddEdit_Trans(pArrayData, fgDtls, true, ref dblTransID, strAdjQry, "", txtEntryNo.Text, txtChlnNo.Text, "RefNo", Localization.ParseNativeInt(this.frmVoucherTypeID.ToString()));
                flg_OrderConform = false;

                #region Autogenerate Sales
                if (chkIsInvoice.Checked == true)
                {
                    string sQry = string.Empty;
                    string sQryDtls = string.Empty;
                    string sQryStock = string.Empty;

                    sQryDtls = "";
                    int iPartyID = Localization.ParseNativeInt(DB.GetSnglValue("SELECT LedgerID From tbl_FabricOutwardDtls Where FabOutwardID=" + dblTransID));
                    int iMenuID = Localization.ParseNativeInt(DB.GetSnglValue("SELECT MenuID From tbl_MenuMaster Where FormCall='frmFabricSales'"));
                    int iMaxEntryNo = Localization.ParseNativeInt(DB.GetSnglValue("SELECT MAX((ISNULL(EntryNo,0))) + 1 As EntryNo From tbl_FabricSalesMain Where IsDeleted=0"));

                    using (IDataReader idr = DB.GetRS("SELECT * FROM tbl_FabricOutwardMain Where FabOutwardID=" + dblTransID))
                    {
                        while (idr.Read())
                        {
                            #region Main

                            if (iOutwardId_ForEdit > 0)
                            {
                                sQry = string.Format("Update tbl_FabricSalesMain Set VoucherTypeID={0},EntryNo={1},EntryDate={2},OrderType={3},BillNo={4},BillDate={5},PartyID={6},BrokerID={7},HasteID={8},TransportID={9},LrNo={10},LrDate={11},CrDays={12},SalesACID={13},AdditionalDes={14},TotPcs={15},TotMtrs={16},GrossAmt={17},AddLessAmt={18},NetAmt={19},Description={20},BrokerAvgPercentage={21},BrokerTotalAmount={22},EI1={23},EI2={24},StoreID={25},CompID={26},BranchID={27},YearID={28},AddedOn={29},AddedBy={30},ModifiedBy={31},DeletedBy={32},CancelledBy={33},ApprovedBy={34},AuditedBy={35} Where FabricSalesID={36}" + Environment.NewLine,
                                       this.frmVoucherTypeID,
                                       iMaxEntryNo,
                                      CommonLogic.SQuote(Localization.ToSqlDateString(Conversions.ToString(idr["EntryDate"].ToString()))),
                                       CommonLogic.SQuote(Conversions.ToString(idr["OrderType"].ToString())),
                                       CommonLogic.SQuote(Conversions.ToString(idr["RefNo"].ToString())),
                                       CommonLogic.SQuote(Localization.ToSqlDateString(Conversions.ToString(idr["RefDate"].ToString()))),
                                       Localization.ParseNativeInt(Conversions.ToString(idr["PartyID"].ToString())),
                                       Localization.ParseNativeInt(Conversions.ToString(idr["BrokerID"].ToString())),
                                       Localization.ParseNativeInt(Conversions.ToString(idr["HasteID"].ToString())),
                                       Localization.ParseNativeInt(Conversions.ToString(idr["TransPortID"].ToString())),
                                       Conversions.ToString(idr["LrNo"].ToString() == "" ? "NULL" : CommonLogic.SQuote(idr["LrNo"].ToString())),
                                       idr["LrDate"].ToString() == "" ? "NULL" : CommonLogic.SQuote(Localization.ToSqlDateString(Conversions.ToString(idr["LrDate"].ToString()))),
                                       0,
                                       Localization.ParseNativeInt(Conversions.ToString(idr["FabSalesACID"].ToString())),
                                      idr["AdditionalDes"].ToString() == "" ? "NULL" : CommonLogic.SQuote(Conversions.ToString(idr["AdditionalDes"].ToString())),
                                       Localization.ParseNativeInt(Conversions.ToString(idr["TotPcs"].ToString())),
                                       Localization.ParseNativeInt(Conversions.ToString(idr["TotMtrs"].ToString())),
                                       Localization.ParseNativeInt(Conversions.ToString(idr["TotWt"].ToString())),
                                       0,
                                       Localization.ParseNativeInt(Conversions.ToString(idr["TotAmount"].ToString())),
                                       idr["Description"].ToString() == "" ? "NULL" : CommonLogic.SQuote(Conversions.ToString(idr["Description"].ToString())),
                                       0,
                                       0,
                                       0,
                                       0,
                                       Db_Detials.StoreID,
                                       Db_Detials.CompID,
                                       Db_Detials.BranchID,
                                       Db_Detials.YearID,
                                       "getdate()",
                                       Db_Detials.UserID, Db_Detials.UserID, 0, 0, 0, 0, iOutwardId_ForEdit);
                            }
                            else
                            {
                                sQry = string.Format("Insert Into tbl_FabricSalesMain (VoucherTypeID,EntryNo,EntryDate,OrderType,BillNo,BillDate,PartyID,BrokerID,HasteID,TransportID,LrNo,LrDate,CrDays,SalesACID,AdditionalDes,TotPcs,TotMtrs,GrossAmt,AddLessAmt,NetAmt,Description,BrokerAvgPercentage,BrokerTotalAmount,FabricOutwardID,EI1,EI2,StoreID,CompID,BranchID,YearID,AddedOn,AddedBy,ModifiedBy,DeletedBy,CancelledBy,ApprovedBy,AuditedBy)Values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36})" + Environment.NewLine,
                                       this.frmVoucherTypeID,
                                       iMaxEntryNo,
                                      CommonLogic.SQuote(Localization.ToSqlDateString(Conversions.ToString(idr["EntryDate"].ToString()))),
                                       CommonLogic.SQuote(Conversions.ToString(idr["OrderType"].ToString())),
                                       CommonLogic.SQuote(Conversions.ToString(idr["RefNo"].ToString())),
                                       CommonLogic.SQuote(Localization.ToSqlDateString(Conversions.ToString(idr["RefDate"].ToString()))),
                                       Localization.ParseNativeInt(Conversions.ToString(idr["PartyID"].ToString())),
                                       Localization.ParseNativeInt(Conversions.ToString(idr["BrokerID"].ToString())),
                                       Localization.ParseNativeInt(Conversions.ToString(idr["HasteID"].ToString())),
                                       Localization.ParseNativeInt(Conversions.ToString(idr["TransPortID"].ToString())),
                                       Conversions.ToString(idr["LrNo"].ToString() == "" ? "NULL" : CommonLogic.SQuote(idr["LrNo"].ToString())),
                                       idr["LrDate"].ToString() == "" ? "NULL" : CommonLogic.SQuote(Localization.ToSqlDateString(Conversions.ToString(idr["LrDate"].ToString()))),
                                       0,
                                       Localization.ParseNativeInt(Conversions.ToString(idr["FabSalesACID"].ToString())),
                                      idr["AdditionalDes"].ToString() == "" ? "NULL" : CommonLogic.SQuote(Conversions.ToString(idr["AdditionalDes"].ToString())),
                                       Localization.ParseNativeInt(Conversions.ToString(idr["TotPcs"].ToString())),
                                       Localization.ParseNativeInt(Conversions.ToString(idr["TotMtrs"].ToString())),
                                       Localization.ParseNativeInt(Conversions.ToString(idr["TotWt"].ToString())),
                                       0,
                                       Localization.ParseNativeInt(Conversions.ToString(idr["TotAmount"].ToString())),
                                       idr["Description"].ToString() == "" ? "NULL" : CommonLogic.SQuote(Conversions.ToString(idr["Description"].ToString())),
                                       0,
                                       0, dblTransID,
                                       0,
                                       0,
                                       Db_Detials.StoreID,
                                       Db_Detials.CompID,
                                       Db_Detials.BranchID,
                                       Db_Detials.YearID,
                                       "getdate()",
                                       Db_Detials.UserID, 0, 0, 0, 0, 0);
                            }
                            #endregion

                            int iSrNo = 1;

                            sQryDtls = string.Format("Delete From tbl_FabricSalesDtls Where FabricSalesID=" + iOutwardId_ForEdit + "");
                            using (IDataReader dr = DB.GetRS("SELECT * FROM tbl_FabricOutwardDtls Where FabOutwardID=" + dblTransID + " Order By SubFabOutwardID ASC"))
                            {
                                while (dr.Read())
                                {
                                    sQryDtls += string.Format(" Insert Into tbl_FabricSalesDtls (FabricSalesID,SubFabricSalesID,FabOutwardID,FabSOID,FabricID,DesignID,QualityID,ShadeID,NFabricID,NDesignID,NQualityID,NShadeID,UnitID,Pcs,Size,Mtrs,Wt,Rate,Amount,StockValue,Description,BrokersPercent,BrokersAmount,InitMtrs,InitWt,DepartmentID,SubDepartmentID,ProductionOrdID,InwLedID,InwTransID,ProcessOrdID,ProcessTypeID,ProcessID,EI1,EI2,EI3,ED1,ED2,ET1,ET2,ET3,EN1,EN2,RefID,ARefID,MainRefID,MyID,TempRowIndex) Values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43},{44},{45},{46},{47});" + Environment.NewLine,
                                             "(#CodeID#)", iSrNo, dblTransID, Localization.ParseNativeInt(Conversions.ToString(dr["FabSOID"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["NFabricID"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["NDesignID"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["NQualityID"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["NShadeID"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["NFabricID"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["NDesignID"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["NQualityID"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["NShadeID"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["UnitID"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["Pcs"].ToString())),
                                            Localization.ParseNativeDecimal(Conversions.ToString(dr["Size"].ToString())),
                                            Localization.ParseNativeDecimal(Conversions.ToString(dr["Mtrs"].ToString())),
                                            Localization.ParseNativeDecimal(Conversions.ToString(dr["Wt"].ToString())),
                                            Localization.ParseNativeDecimal(Conversions.ToString(dr["Rate"].ToString())),
                                            Localization.ParseNativeDecimal(Conversions.ToString(dr["Amount"].ToString())),
                                            Localization.ParseNativeDecimal(Conversions.ToString(dr["StockValue"].ToString())),
                                            dr["Description"].ToString() == "" ? "NULL" : CommonLogic.SQuote(Conversions.ToString(dr["Description"].ToString())), 0, 0,
                                            Localization.ParseNativeDecimal(Conversions.ToString(dr["InitMtrs"].ToString())),
                                            Localization.ParseNativeDecimal(Conversions.ToString(dr["InitWt"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["DepartmentID"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["SubDepartmentID"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["ProductionOrdID"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["InwLedID"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["InwTransID"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["ProcessOrdID"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["ProcessTypeID"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["ProcessID"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["EI1"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["EI2"].ToString())),
                                            Localization.ParseNativeInt(Conversions.ToString(dr["EI2"].ToString())),
                                            dr["ED1"].ToString() == "" ? "NULL" : CommonLogic.SQuote(Localization.ToSqlDateString(Conversions.ToString(dr["ED1"].ToString()))),
                                            dr["ED2"].ToString() == "" ? "NULL" : CommonLogic.SQuote(Localization.ToSqlDateString(Conversions.ToString(dr["ED2"].ToString()))),
                                            dr["ET1"].ToString() == "" ? "NULL" : CommonLogic.SQuote(Conversions.ToString(dr["ET1"].ToString())),
                                            dr["ET2"].ToString() == "" ? "NULL" : CommonLogic.SQuote(Conversions.ToString(dr["ET2"].ToString())),
                                            dr["ET3"].ToString() == "" ? "NULL" : CommonLogic.SQuote(Conversions.ToString(dr["ET3"].ToString())),
                                            Localization.ParseNativeDecimal(Conversions.ToString(dr["EN1"].ToString())),
                                            Localization.ParseNativeDecimal(Conversions.ToString(dr["EN2"].ToString())),
                                            dr["RefID"].ToString() == "" ? "0" : CommonLogic.SQuote(Conversions.ToString(dr["RefID"].ToString())),
                                            dr["ARefID"].ToString() == "" ? "0" : CommonLogic.SQuote(Conversions.ToString(dr["ARefID"].ToString())),
                                            dr["MainRefID"].ToString() == "" ? "0" : CommonLogic.SQuote(Conversions.ToString(dr["MainRefID"].ToString())),
                                            dr["MyID"].ToString() == "" ? "NULL" : CommonLogic.SQuote(Conversions.ToString(dr["MyID"].ToString())),
                                            iSrNo
                                        );
                                    iSrNo++;
                                }
                            }
                        }
                        sQryDtls = sQryDtls.Replace("'null'", "null").Replace("Nnull", "null");
                    }
                    DB.ExecuteTranscation(sQry, "", true, sQryDtls);
                }
                #endregion

                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) || (base.blnFormAction == Enum_Define.ActionType.View_Record))
                {
                    flg_Sms = Localization.ParseBoolean(GlobalVariables.SMS_SEND_DC);
                    flg_Email = Localization.ParseBoolean(GlobalVariables.EMAIL_SEND_DC);

                    if (blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        string sEntryNo = DB.GetSnglValue("SELECT EntryNo from fn_FabricOutwardMain_tbl() WHERE  FabOutwardID =" + dblTransID);

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
                                    CommonCls.sendEmail(dblTransID.ToString(), sEntryNo, sPartyID, base.iIDentity.ToString(), false, Localization.ParseNativeInt(this.frmVoucherTypeID.ToString()), 1);
                                }
                                catch { }
                            }
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                    {
                        if (flg_Email == true)
                        {
                            string sisactive = DB.GetSnglValue("Select IsActive from tbl_MailingConfig where menuid=" + RefMenuID);
                            if (sisactive == "True")
                            {
                                try
                                {
                                    CommonCls.sendEmail(txtCode.Text, txtEntryNo.Text, sPartyID, base.iIDentity.ToString(), true, Localization.ParseNativeInt(this.frmVoucherTypeID.ToString()), 1);
                                }
                                catch { }
                            }
                        }
                    }
                }
                Form cForm = this;
                Navigate.NavigateForm(Enum_Define.Navi_form.Cancel_Record, ref cForm, false, false);
                if (Localization.ParseBoolean(GlobalVariables.PASAV) && (CIS_Utilities.CIS_Dialog.Show("Do you want to Print this Record?", GetAssemblyInfo.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    CIS_ReportTool.frmReportViewer frm = new CIS_ReportTool.frmReportViewer();
                    frm.GenerateReport(base.iIDentity, "", "", Conversions.ToInteger(this.txtCode.Text), "", 0, true, 0, false, 0);
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

                if (txtChlnNo.Text.Trim() == "" || txtChlnNo.Text.Trim() == "-" || txtChlnNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Challan No.");
                    txtChlnNo.Focus();
                    return true;
                }

                if (chkIsInvoice.Checked)
                {
                    if (cboSalesAc.SelectedValue == null || cboSalesAc.Text.Trim().ToString() == "-" || cboSalesAc.SelectedValue.ToString() == "0")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Sales A/c");
                        cboSalesAc.Focus();
                        return true;
                    }

                }

                if ((txtChlnNo.Text != null) && (txtChlnNo.Text.Trim().Length > 0))
                {
                    string strTblName;
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        strTblName = "tbl_FabricOutwardMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "RefNo", txtChlnNo.Text, false, "", 0, string.Format("StoreID={0} and CompID = {1} and BranchID={2} and YearID = {3} and VoucherTypeID={4}", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, this.frmVoucherTypeID), "This Challan No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where RefNo = '{1}' and StoreID={2} and  CompId = {3} and BranchID={4} and YearId = {5} and VoucherTypeID={6}", new object[] { "tbl_FabricOutwardMain", txtChlnNo.Text.ToString(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, this.frmVoucherTypeID }))))
                        {
                            this.txtChlnNo.Focus();
                            return true;
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        strTblName = "tbl_FabricOutwardMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "RefNo", txtChlnNo.Text, true, "FabOutwardID", Localization.ParseNativeLong(txtCode.Text.Trim()), string.Format("storeID={0} and CompID = {1} and BranchID={2} and YearID = {3} and VoucherTypeID={4}", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, this.frmVoucherTypeID), "This Challan No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where RefNo = '{1}'  and StoreID={2} and  CompId = {3} and BranchID={4} and YearId = {5} and VoucherTypeID={6}", new object[] { "tbl_FabricOutwardMain", txtChlnNo.Text.ToString(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, this.frmVoucherTypeID }))))
                        {
                            txtChlnNo.Focus();
                            return true;
                        }
                    }
                }

                if (cboParty.SelectedValue == null || cboParty.Text.Trim().ToString() == "-" || cboParty.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Party");
                    cboParty.Focus();
                    return true;
                }

                decimal CreditLimit = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select isnull(CreditLimit,0) From {0} Where LedgerId = {1} ", "tbl_LedgerMaster", cboParty.SelectedValue)));

                if (CreditLimit > 0)
                {
                    decimal TotSalseValue = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("select sum(isnull(NetAmount,0)) From {0} Where LedgerID = {1} and StoreID={2} and CompID = {3} and BranchID={4} and YearID ={5}", new object[] { "tbl_FabricSalesMain", cboParty.SelectedValue, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })));
                    if (TotSalseValue > CreditLimit)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Exceeding Credit Limit");
                        return true;
                    }
                }
                if (cboDepartment.SelectedValue == null || cboDepartment.SelectedValue.ToString() == "-" || cboDepartment.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    cboDepartment.Focus();
                    return true;
                }

                if (FDC_BRK_COM == true)
                {
                    if (cboBroker.SelectedValue == null || cboBroker.SelectedValue.ToString() == "-" || cboBroker.SelectedValue.ToString() == "0")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Broker");
                        cboBroker.Focus();
                        return true;
                    }
                }

                if (CheckOrder())
                {
                    return true;
                }

                if (FAB_MAINTAINWEIGHT)
                {
                    for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                    {
                        if (fgDtls.Rows[i].Cells[18].Value == null || Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[18].Value.ToString()) == 0 || fgDtls.Rows[i].Cells[18].Value.ToString() == "" || fgDtls.Rows[i].Cells[18].Value.ToString() == "NaN")
                        {
                            fgDtls.CurrentCell = fgDtls[18, i];
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Weight");
                            fgDtls.Rows.Add();
                            return true;
                        }
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

        #region Other Event

        private void cboDepartment_SelectedValueChanged(object sender, EventArgs e)
        {
            EventHandles.CreateDefault_Rows(this.fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
            EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
        }

        public void AplySelectBtnEnbl()
        {
            if ((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record))
            {
                this.btnSelect.Enabled = true;
            }
            else
            {
                this.btnSelect.Enabled = false;
            }
        }

        public void PrintRecord()
        {
            try
            {
                CIS_ReportTool.frmMultiPrint.MenuID = base.iIDentity;
                CIS_ReportTool.frmMultiPrint.Id = Localization.ParseNativeInt(this.txtCode.Text);
                CIS_ReportTool.frmMultiPrint.TblNm = "tbl_FabricOutwardMain";
                CIS_ReportTool.frmMultiPrint.IdStr = "FabOutwardID";
                CIS_ReportTool.frmMultiPrint frmMPrnt = new CIS_ReportTool.frmMultiPrint();
                CIS_ReportTool.frmMultiPrint.VoucherTypeID = this.frmVoucherTypeID;
                CIS_ReportTool.frmMultiPrint.iCompID = Db_Detials.CompID;
                CIS_ReportTool.frmMultiPrint.iYearID = Db_Detials.YearID;
                CIS_ReportTool.frmMultiPrint.iUserID = Db_Detials.UserID;
                CIS_ReportTool.frmMultiPrint.objReport = Db_Detials.objReport;
                CIS_ReportTool.frmMultiPrint.sApplicationName = GetAssemblyInfo.ProductName;

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

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (fgDtls.RowCount > 1)
                {
                    cboOrderType.Enabled = false;
                    cboDepartment.Enabled = false;
                }
                else
                {
                    cboOrderType.Enabled = true;
                    cboDepartment.Enabled = true;
                }

                if ((e.ColumnIndex == 15) | (e.ColumnIndex == 17) | (e.ColumnIndex == 18))
                {
                    ExecuterTempQry(e.RowIndex);
                    ExecuterTempQry_Orders(e.RowIndex);
                }

                if ((Localization.ParseNativeInt(base.blnFormAction.ToString()) == 4) || (Localization.ParseNativeInt(base.blnFormAction.ToString()) == 5))
                {
                    return;
                }
                SRateCalcType = "";
                if (fgDtls.Rows[e.RowIndex].Cells[14].Value != null && fgDtls.Rows[e.RowIndex].Cells[14].Value.ToString() != "" && fgDtls.Rows[e.RowIndex].Cells[14].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[14].Value.ToString() != "-")
                {
                    SRateCalcType = DB.GetSnglValue("Select RateCalcType from tbl_UnitsMaster Where UnitID=" + fgDtls.Rows[e.RowIndex].Cells[14].Value.ToString() + " and IsDeleted=0");
                }
                switch (e.ColumnIndex)
                {
                    
                    case 6:
                        if (txtScan.Text.Trim().Length <= 0)
                        {
                            fgDtls.Rows[e.RowIndex].Cells[10].Value = fgDtls.Rows[e.RowIndex].Cells[6].Value;
                            fgDtls.Rows[e.RowIndex].Cells[7].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", fgDtls.Rows[e.RowIndex].Cells[6].Value)));
                        }
                        return;

                    case 7:
                        if (txtScan.Text.Trim().Length <= 0)
                        {
                            fgDtls.Rows[e.RowIndex].Cells[11].Value = fgDtls.Rows[e.RowIndex].Cells[7].Value;
                        }
                        return;

                    case 15:
                        if (base.blnFormAction == Enum_Define.ActionType.Edit_Record || base.blnFormAction == Enum_Define.ActionType.New_Record)
                        {
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[15].Value != null && fgDtls.Rows[e.RowIndex].Cells[16].Value != null)
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[17].Value = Localization.ParseNativeDecimal(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString()) * Localization.ParseNativeDecimal(fgDtls.Rows[e.RowIndex].Cells[16].Value.ToString());
                                }
                            }
                            goto case 17;
                        }
                        break;

                    case 19:
                        goto case 17;

                    case 18:
                        goto case 17;

                    case 17:
                        if (SRateCalcType == "M")
                        {
                            if (fgDtls.Rows[e.RowIndex].Cells[17].Value.ToString() != null || fgDtls.Rows[e.RowIndex].Cells[17].Value.ToString() != "" && fgDtls.Rows[e.RowIndex].Cells[19].Value.ToString() != null || fgDtls.Rows[e.RowIndex].Cells[19].Value.ToString() != "")
                                fgDtls.Rows[e.RowIndex].Cells[20].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[17].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[19].Value.ToString())).ToString()));
                        }
                        else if (SRateCalcType == "P")
                        {
                            if (fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != null || fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != "" && fgDtls.Rows[e.RowIndex].Cells[19].Value.ToString() != null || fgDtls.Rows[e.RowIndex].Cells[19].Value.ToString() != "")
                                fgDtls.Rows[e.RowIndex].Cells[20].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[19].Value.ToString())).ToString()));
                        }
                        else if (SRateCalcType == "W")
                        {
                            if (fgDtls.Rows[e.RowIndex].Cells[18].Value != null && fgDtls.Rows[e.RowIndex].Cells[18].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[18].Value != null && fgDtls.Rows[e.RowIndex].Cells[18].Value.ToString() != "0")
                            {
                                fgDtls.Rows[e.RowIndex].Cells[20].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[18].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[19].Value.ToString())).ToString()));
                            }
                        }
                        break;


                    case 8:
                        if (FDC_ORD_WISE == true)
                        {
                            if (flg_OrderConform == false)
                            {
                                flg_OrderConform = false;
                            }
                            if (fgDtls.Rows[e.RowIndex].Cells[2].Value == null)
                                fgDtls.Rows[e.RowIndex].Cells[2].Value = "";

                            if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                            {
                                if ((fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString() == "") && (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString() != "") && (fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString() != "") && (fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString() != "") && (fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString() != "") && (fgDtls.Rows[e.RowIndex].Cells[4].Value != null) && (fgDtls.Rows[e.RowIndex].Cells[6].Value != null) && (fgDtls.Rows[e.RowIndex].Cells[7].Value != null) && (fgDtls.Rows[e.RowIndex].Cells[8].Value != null))
                                {
                                    for (int i = 0; i <= (fgdtls_f.Rows.Count - 1); i++)
                                    {
                                        if (Operators.ConditionalCompareObjectEqual(fgDtls.Rows[e.RowIndex].Cells[6].Value, fgdtls_f.Rows[i].Cells[7].Value, false))
                                        {
                                            if (Operators.ConditionalCompareObjectEqual(fgDtls.Rows[e.RowIndex].Cells[7].Value, fgdtls_f.Rows[i].Cells[9].Value, false))
                                            {
                                                if (Operators.ConditionalCompareObjectEqual(fgDtls.Rows[e.RowIndex].Cells[8].Value, fgdtls_f.Rows[i].Cells[11].Value, false))
                                                {
                                                    if (Operators.ConditionalCompareObjectGreater(fgdtls_f.Rows[i].Cells[15].Value, 0, false))
                                                    {
                                                        flg_OrderConform = true;
                                                        fgDtls.Rows[e.RowIndex].Cells[2].Value = fgdtls_f.Rows[i].Cells[5].Value;
                                                        fgDtls.Rows[e.RowIndex].Cells[47].Value = fgdtls_f.Rows[i].Cells[2].Value;
                                                        fgDtls.Rows[e.RowIndex].Cells[16].Value = fgdtls_f.Rows[i].Cells[22].Value;
                                                        fgDtls.Rows[e.RowIndex].Cells[44].Value = fgdtls_f.Rows[i].Cells[23].Value;
                                                        fgDtls.Rows[e.RowIndex].Cells[19].Value = fgdtls_f.Rows[i].Cells[25].Value;

                                                        if (this.frmVoucherTypeID > 0)
                                                        {
                                                            try
                                                            {
                                                                fgDtls.Rows[e.RowIndex].Cells[16].Value = Localization.ParseNativeDecimal(fgdtls_f.Rows[i].Cells[21].Value.ToString());
                                                                if (Localization.ParseNativeDecimal(fgdtls_f.Rows[i].Cells[18].Value.ToString()) > 0)
                                                                {
                                                                    fgDtls.Rows[e.RowIndex].Cells[17].Value = Localization.ParseNativeDecimal(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString()) * Localization.ParseNativeDecimal(fgDtls.Rows[e.RowIndex].Cells[16].Value.ToString());
                                                                }
                                                                fgDtls.Rows[e.RowIndex].Cells[17].ReadOnly = true;
                                                            }
                                                            catch { }
                                                        }
                                                        // fgDtls.Rows[e.RowIndex].Cells[27].Value = Localization.ParseNativeDecimal(fgdtls_f.Rows[i].Cells[27].Value.ToString());
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        flg_OrderConform = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case 10:
                        if (fgDtls.Rows[e.RowIndex].Cells[10].Value != null)
                        {
                            fgDtls.Rows[e.RowIndex].Cells[11].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", fgDtls.Rows[e.RowIndex].Cells[10].Value)));
                        }
                        break;
                    default:
                        break;
                }
                if (txtScan.Text.Trim().Length <= 0)
                {
                    fgDtls.Rows[e.RowIndex].Cells[12].Value = fgDtls.Rows[e.RowIndex].Cells[8].Value;
                }
                if (e.ColumnIndex == 10 || e.ColumnIndex == 11 || e.ColumnIndex == 12)
                {
                    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    {
                        if (fgDtls.Rows[i].Cells[10].Value != null && fgDtls.Rows[i].Cells[10].Value.ToString() != "" && fgDtls.Rows[i].Cells[11].Value != null && fgDtls.Rows[i].Cells[11].Value.ToString().Trim() != "" && fgDtls.Rows[i].Cells[12].Value != null && fgDtls.Rows[i].Cells[12].Value.ToString().Trim() != "")
                        {
                            fgDtls.Rows[i].Cells[5].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricID from fn_FabricMaster_tbl() where FabricDesignID={0} and FabricQualityID={1} and FabricShadeID={2}", fgDtls.Rows[i].Cells[10].Value, fgDtls.Rows[i].Cells[11].Value, fgDtls.Rows[i].Cells[12].Value)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
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
                        if (fgDtls.Rows[m].Cells[6].Value != null && fgDtls.Rows[m].Cells[6].Value.ToString() != "")
                        {
                            iIndex = m;
                            isIndexAppld = true;
                        }
                    }
                    if (!isIndexAppld)
                    {
                        iIndex = -1;
                    }

                    if (cboDepartment.SelectedValue == null || cboDepartment.SelectedValue.ToString() == "-" || cboDepartment.SelectedValue.ToString().Trim() == "" || cboDepartment.SelectedValue.ToString() == "0")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department.");
                        cboDepartment.Focus();
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
                        strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5},{6},{7}) Where BalMeters > 0 Order by MyId ", new object[] { strQry_ColName, snglValue, CommonLogic.SQuote(Localization.ToSqlDateString(dtChlnDate.Text)), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, this.cboDepartment.SelectedValue });
                        #endregion

                        if (this.dtChlnDate.Text != "__/__/____")
                        {
                            frmStockAdj frmStockAdj = new frmStockAdj();
                            frmStockAdj.MenuID = base.iIDentity;
                            frmStockAdj.Entity_IsfFtr = 0.0;
                            frmStockAdj.ref_fgDtls = this.fgDtls;
                            frmStockAdj.QueryString = strQry;
                            frmStockAdj.IsRefQuery = true;
                            frmStockAdj.ibitCol = ibitcol;
                            frmStockAdj.AsonDate = Conversions.ToDate(this.dtChlnDate.Text);
                            frmStockAdj.LedgerID = Conversions.ToString(this.cboDepartment.SelectedValue);
                            frmStockAdj.UsedInGridArray = this.OrgInGridArray;
                            if (frmStockAdj.ShowDialog() == DialogResult.Cancel)
                            {
                                frmStockAdj.Dispose();
                                return;
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

                                            if (fgDtls.Rows[i].Cells[41].Value != null)
                                            {
                                                if (Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[41].Value.ToString()) < iPcs)
                                                {
                                                    iPcs = Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[41].Value.ToString());
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
                                                        else if (m != 17 && m != 18)
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
                                            fgDtls.Rows[i].Cells[15].Value = fgDtls.Rows[i].Cells[41].Value.ToString();
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Challan Date");
                        }
                        this.fgDtls.Select();
                        CalculateOrder();
                        setTempRowIndex();
                        setMyID_Stock();
                        ExecuterTempQry(-1);
                        setMyID_Orders();
                        ExecuterTempQry_Orders(-1);
                    }
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                }
            }
        }

        public void CalculateOrder()
        {
            try
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                    {
                        int iCount = 0;
                        if (fgDtls.Rows.Count > 0)
                        {
                            for (int i = 0; i < (fgDtls.Rows.Count - 1); i++)
                            {
                                if (fgDtls.Rows[i].Cells[6].Value != null && Localization.ParseNativeInt(fgDtls.Rows[i].Cells[6].Value.ToString()) != 0)
                                {
                                    iCount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(0) from fn_FetchFabricOrders({0},{1},{2},{3},{4},{5}) where DesignID={6} and QualityID={7} and ShadeID={8} AND RefVoucherID in ({9})",
                                                CommonLogic.SQuote(Localization.ToSqlDateString(dtChlnDate.Text.ToString())), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboParty.SelectedValue, fgDtls.Rows[i].Cells[6].Value, fgDtls.Rows[i].Cells[7].Value,
                                                fgDtls.Rows[i].Cells[8].Value, RefVoucherID)));
                                    if (iCount == 0)
                                    {
                                        // fgDtls.Rows.RemoveAt(i);
                                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "This Design is not in Selected Sales Order No");
                                    }
                                }

                                fgDtls.Rows[i].Cells[1].Value = (i + 1);
                            }
                        }
                    }
                }
                if (Localization.ParseBoolean(GlobalVariables.FDC))
                {
                    int iRowCnt = fgDtls.RowCount - 1;
                    for (int i = 0; i < iRowCnt; i++)
                    {
                        if ((fgDtls.Rows[i].Cells[15].Value != null))
                        {
                            if (Localization.ParseNativeInt(fgDtls.Rows[i].Cells[15].Value.ToString()) != 0)
                            {
                                double num3 = Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[15].Value.ToString());
                                int num6 = (int)Math.Round((double)(num3 + i));
                                for (int j = i + 1; j <= num6; j++)
                                {
                                    fgDtls.Rows.Insert(j, new DataGridViewRow());
                                    int num7 = fgDtls.ColumnCount - 1;
                                    for (int k = 0; k <= num7; k++)
                                    {
                                        if (k == 15)
                                        {
                                            fgDtls.Rows[j].Cells[k].Value = 1;
                                        }
                                        else if (k == 1)
                                        {
                                            fgDtls.Rows[j].Cells[k].Value = j;
                                        }
                                        else
                                        {
                                            fgDtls.Rows[j].Cells[k].Value = fgDtls.Rows[i].Cells[k].Value;
                                        }
                                    }
                                }
                                fgDtls.Rows.RemoveAt(i);
                                i = (int)Math.Round((double)(i + (num3 - 1.0)));
                                iRowCnt = fgDtls.RowCount - 1;
                            }
                        }
                    }
                    fgDtls.Rows.RemoveAt(fgDtls.RowCount - 1);
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboParty_SelectedValueChanged(object sender, EventArgs e)
        {
            if (blnFormAction == Enum_Define.ActionType.New_Record | blnFormAction == Enum_Define.ActionType.Edit_Record)
            {
                try
                {
                    if (cboParty.SelectedValue != null)
                    {
                        cboBroker.SelectedValue = Localization.ParseNativeDouble(DB.GetSnglValue(string.Format("Select BrokerID From {0} Where LedgerID = {1} ", "tbl_LedgerMaster", cboParty.SelectedValue)));
                        cboTransport.SelectedValue = Localization.ParseNativeDouble(DB.GetSnglValue(string.Format("Select TransportID From {0} Where LedgerID = {1} ", "tbl_LedgerMaster", cboParty.SelectedValue)));
                        cboHaste.SelectedValue = cboParty.SelectedValue;
                        lblPartyVatNo.Text = DB.GetSnglValue("SELECT VatTinNo from fn_LedgerMaster_tbl() WHERE LedgerID=" + cboParty.SelectedValue);
                    }
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                }
            }
        }

        private void cboOrderNo_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (FDC_ORD_WISE == true)
                {
                    if (cboParty.SelectedValue != null && fgDtls.Rows[fgDtls.CurrentCell.RowIndex].Cells[fgDtls.CurrentCell.ColumnIndex].Value != null)
                    {
                        if (blnFormAction == Enum_Define.ActionType.New_Record || blnFormAction == Enum_Define.ActionType.Edit_Record)
                        {
                            if (Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentCell.RowIndex].Cells[fgDtls.CurrentCell.ColumnIndex].Value.ToString()) > 0)
                            {
                                ////fgdtls_f.Rows.Dispose();
                                //fgdtls_f.DataSource = DB.GetDT(String.Format("Select FabSOID,FabSONo,FabSODate,DesignID,FabricDesignName,QualityID,Quality,ShadeID,FabricShadeName,OrderPcs,DispatchPcs, 0 as CDispatchPcs,BalPcs, Rate, Size  from {0}({1},{2},{3}) Where FabSOID = {4} AND VoucherTypeID in ({5})", Db_Detials.fn_FetchFabSalesOrderDtls, cboParty.SelectedValue, Db_Detials.CompID, Db_Detials.YearID, fgDtls.Rows[fgDtls.CurrentCell.RowIndex].Cells[fgDtls.CurrentCell.ColumnIndex].Value, RefVoucherID), false);
                                //dtOrderDate.Text = Localization.ToVBDateString(DB.GetSnglValue(String.Format("Select top 1 OrderDate from {0}({1},{2},{3}) Where FabSOID = {4} and VoucherTypeID in ({5})", Db_Detials.fn_FetchFabSalesOrderDtls, cboParty.SelectedValue, Db_Detials.CompID, Db_Detials.YearID, fgDtls.Rows[fgDtls.CurrentCell.RowIndex].Cells[fgDtls.CurrentCell.ColumnIndex].Value, RefVoucherID)));

                                fgdtls_f.DataSource = DB.GetDT(string.Format("Select * from {0} ({1},{2},{3},{4},{5},{6}) Where OrderTransType='Sales' and  RefVoucherID in ({7}) and OrderID = {8}", "fn_FetchFabricOrders", CommonLogic.SQuote(Localization.ToSqlDateString(dtChlnDate.Text.ToString())), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboParty.SelectedValue.ToString(), RefVoucherID, fgDtls.Rows[fgDtls.CurrentCell.RowIndex].Cells[fgDtls.CurrentCell.ColumnIndex].Value), false);
                                foreach (UltraGridBand Band in fgdtls_f.DisplayLayout.Bands)
                                {
                                    foreach (UltraGridColumn Column in Band.Columns)
                                    {
                                        using (IDataReader dr = DB.GetRS(String.Format("Select * From {0} Where GridID = {1} and SubGridID = 1 and ColIndex = {2}", "tbl_GridSettings", iIDentity, Column.Index)))
                                        {
                                            while (dr.Read())
                                            {
                                                Column.Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
                                                Column.Hidden = Localization.ParseBoolean(dr["IsHidden"].ToString());
                                                Column.CellActivation = Activation.NoEdit;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //// fgdtls_f.Rows.Dispose();
                                //fgdtls_f.DataSource = DB.GetDT(String.Format("Select FabSOID,FabSONo,FabSODate,DesignID,FabricDesignName,QualityID,Quality,ShadeID,FabricShadeName,OrderPcs,DispatchPcs, 0 as CDispatchPcs,BalPcs, Rate, Size  from {0}({1},{2},{3}) WHERE VoucherTypeID in ({4});", Db_Detials.fn_FetchFabSalesOrderDtls, cboParty.SelectedValue, Db_Detials.CompID, Db_Detials.YearID, RefVoucherID), false);
                                fgdtls_f.DataSource = DB.GetDT(string.Format("Select * from {0} ({1},{2},{3},{4},{5},{6}) Where OrderTransType='Sales' and  RefVoucherID in ({7})", "fn_FetchFabricOrders", CommonLogic.SQuote(Localization.ToSqlDateString(dtChlnDate.Text.ToString())), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboParty.SelectedValue.ToString(), RefVoucherID), false);
                                foreach (UltraGridBand Band in fgdtls_f.DisplayLayout.Bands)
                                {
                                    foreach (UltraGridColumn Column in Band.Columns)
                                    {
                                        using (IDataReader dr = DB.GetRS(string.Format("Select * From {0} Where GridID = {1} and SubGridID = 1 and ColIndex = {2}", Db_Detials.tbl_GridSettings, iIDentity, Column.Index)))
                                        {
                                            while (dr.Read())
                                            {
                                                Column.Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
                                                Column.Hidden = (Localization.ParseBoolean(dr["IsHidden"].ToString()));
                                                Column.CellActivation = Activation.NoEdit;
                                            }
                                        }
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

        private void cboHaste_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (cboHaste.SelectedValue != null)
                {
                    if (Localization.ParseNativeInt(cboHaste.SelectedValue.ToString()) > 0)
                        cboTransport.SelectedValue = Localization.ParseNativeDouble(DB.GetSnglValue(String.Format("Select TransportID From {0} Where LedgerID = {1} ", " tbl_LedgerMaster", cboHaste.SelectedValue)));
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void txtScan_Validated(object sender, EventArgs e)
        {
            try
            {
                bool flag = false;
                if (((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record)) && ((this.txtScan.Text != null) && (this.txtScan.Text != "")))
                {
                    for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                    {
                        DataGridViewRow row = fgDtls.Rows[i];
                        if (row.Cells[4].Value != null)
                        {
                            if (row.Cells[4].Value.ToString().ToUpper() == txtScan.Text.ToUpper())
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "This Barcode No is already Selected");
                                txtScan.Text = "";
                                txtScan.Focus();
                                return;
                            }
                        }
                        row = null;
                    }

                    using (IDataReader reader = DB.GetRS(string.Format("{0} {1},{2},{3},{4},{5},{6},'{7}' ", new object[] { "sp_FetchFabricStock_Barcode", DB.SQuoteNotUnicode(Localization.ToSqlDateString(dtChlnDate.Text)), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboDepartment.SelectedValue == null ? "0" : cboDepartment.SelectedValue, txtScan.Text.Trim() })))
                    {
                        while (reader.Read())
                        {
                            flag = true;
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[3].Value = Localization.ParseNativeDecimal(reader["BatchNo"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[4].Value = reader["BarcodeNo"].ToString();
                            //fgDtls.Rows[fgDtls.RowCount - 1].Cells[5].Value = Localization.ParseNativeInt(reader["FabricID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[6].Value = Localization.ParseNativeInt(reader["FabricDesignID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[7].Value = Localization.ParseNativeInt(reader["FabricQualityID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[8].Value = Localization.ParseNativeInt(reader["FabricShadeID"].ToString());

                            if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                            {
                                if (((this.FDC_ORD_WISE && this.FDC_ORD_COMP) && !this.flg_OrderConform))
                                {
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[4].Value = "";
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[6].Value = null;
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[7].Value = null;
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[8].Value = null;
                                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Warning, "", "Order For this BarcodeNo is not available");
                                    txtScan.Text = "";
                                    txtScan.Focus();
                                    return;
                                }
                            }
                            // fgDtls.Rows[fgDtls.RowCount - 1].Cells[9].Value = Localization.ParseNativeInt(reader["FabricID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[10].Value = Localization.ParseNativeInt(reader["FabricDesignID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[11].Value = Localization.ParseNativeInt(reader["FabricQualityID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[12].Value = Localization.ParseNativeInt(reader["FabricShadeID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[13].Value = Localization.ParseNativeInt(reader["GradeID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[14].Value = Localization.ParseNativeInt(reader["UnitID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[15].Value = Localization.ParseNativeDecimal(reader["BalQty"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[16].Value = "0";
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[17].Value = Localization.ParseNativeDecimal(reader["BalMeters"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[18].Value = Localization.ParseNativeDecimal(reader["BalWeight"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[21].Value = "0";
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[22].Value = "";
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[25].Value = Localization.ParseNativeInt(reader["DepartmentID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[26].Value = Localization.ParseNativeInt(reader["SubDepartmentID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[28].Value = Localization.ParseNativeInt(reader["ProductionOrdID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[29].Value = Localization.ParseNativeInt(reader["InwLedID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[30].Value = Localization.ParseNativeInt(reader["InwTransID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[31].Value = Localization.ParseNativeInt(reader["ProcessOrdID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[32].Value = Localization.ParseNativeInt(reader["ProcessTypeID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[33].Value = Localization.ParseNativeInt(reader["ProcessID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[45].Value = "0";
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[46].Value = reader["ARefID"].ToString();
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[48].Value = reader["MainRefID"].ToString();
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[49].Value = reader["MyID"].ToString();
                        }
                    }
                    if (!flag)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "No Records Found");
                        txtScan.Text = "";
                        txtScan.Focus();
                    }
                    else
                    {
                        fgDtls.CurrentCell = fgDtls[1, fgDtls.RowCount - 1];
                        DataGridViewEx ex2 = this.fgDtls;
                        EventHandles.CreateDefault_Rows(ex2, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                        EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                        this.fgDtls = ex2;
                    }
                    txtScan.Text = "";
                    txtScan.Focus();
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Panel1.Visible = false;
        }

        private void btnShowOrder_Click(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if ((cboParty.SelectedValue != null))
                    {
                        if (blnFormAction == Enum_Define.ActionType.New_Record | blnFormAction == Enum_Define.ActionType.Edit_Record)
                        {
                            var _with1 = fgdtls_f;
                            {
                                fgdtls_f.DataSource = DB.GetDT(string.Format("Select * from {0} ({1},{2},{3},{4},{5},{6}) Where OrderTransType='Sales' and  RefVoucherID in ({7})", "fn_FetchFabricOrders", CommonLogic.SQuote(Localization.ToSqlDateString(dtChlnDate.Text.ToString())), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboParty.SelectedValue.ToString(), RefVoucherID), false);
                                foreach (UltraGridBand band in fgdtls_f.DisplayLayout.Bands)
                                {
                                    foreach (UltraGridColumn column in band.Columns)
                                    {
                                        using (IDataReader dr = DB.GetRS(string.Format("Select * From {0} Where GridID = {1} and SubGridID = 1 and ColIndex = {2}", "tbl_GridSettings", iIDentity, column.Index)))
                                        {
                                            while (dr.Read())
                                            {
                                                column.Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
                                                column.Hidden = (Localization.ParseBoolean(dr["IsHidden"].ToString()));
                                                //column.Hidden = (Localization.ParseBoolean(dr["IsHidden"].ToString()) == true ? false : true);
                                                column.CellActivation = Activation.NoEdit;
                                            }
                                        }
                                    }
                                }
                            }
                            if (fgdtls_f.Rows.Count <= 0)
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Order Details Not Found");
                            }
                        }
                    }
                    cboDepartment.Focus();
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                }

                Panel1.Visible = true;
                fgdtls_f.Focus();
            }
            catch { }
        }

        private void cboParty_Leave(object sender, EventArgs e)
        {
            try
            {
                if (blnFormAction == Enum_Define.ActionType.New_Record | blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (sPrevPartyID != cboParty.SelectedValue.ToString())
                    {
                        fgdtls_f.DataSource = DB.GetDT(string.Format("Select * from {0} ({1},{2},{3},{4},{5},{6}) Where OrderTransType='Sales' and  RefVoucherID in ({7})", "fn_FetchFabricOrders", CommonLogic.SQuote(Localization.ToSqlDateString(dtChlnDate.Text.ToString())), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboParty.SelectedValue.ToString(), RefVoucherID), false);

                        foreach (UltraGridBand band in fgdtls_f.DisplayLayout.Bands)
                        {
                            foreach (UltraGridColumn column in band.Columns)
                            {
                                using (IDataReader dr = DB.GetRS(string.Format("Select * From {0} Where GridID = {1} and SubGridID = 1 and ColIndex = {2}", Db_Detials.tbl_GridSettings, iIDentity, column.Index)))
                                {
                                    while (dr.Read())
                                    {
                                        column.Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
                                        column.Hidden = (Localization.ParseBoolean(dr["IsHidden"].ToString()));
                                        column.CellActivation = Activation.NoEdit;
                                    }
                                }
                            }
                        }
                    }

                    if ((cboParty.SelectedValue != null))
                    {
                        if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                        {
                            try
                            {
                                if (sPrevPartyID == null || sPrevPartyID != cboParty.SelectedValue.ToString())
                                {
                                    string sqlQuery = string.Format("Select Distinct OrderNo, OrderID from {0}({1},{2},{3},{4},{5},{6})  Where OrderTransType='Sales' and  RefVoucherID in ({7})", new object[] { "fn_FetchFabricOrders", CommonLogic.SQuote(Localization.ToSqlDateString(dtChlnDate.Text.ToString())), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboParty.SelectedValue.ToString(), RefVoucherID });
                                }
                            }
                            catch { }

                            if (FDC_RATETYPE == false)
                            {
                                if (FDC_ORD_WISE == true)
                                {
                                    var _with1 = fgdtls_f;
                                    if (sPrevPartyID == null || sPrevPartyID != cboParty.SelectedValue.ToString())
                                    {
                                        //fgdtls_f.DataSource = DB.GetDT(string.Format("Select FabSOID,FabSONo,FabSODate,DesignID,FabricDesignName,QualityID,Quality,ShadeID,FabricShadeName,OrderPcs,DispatchPcs, 0 as CDispatchPcs,BalPcs, Rate, Size  from {0}({1},{2},{3}) where VoucherTypeID in ({4})", Db_Detials.fn_FetchFabSalesOrderDtls, cboParty.SelectedValue, Db_Detials.CompID, Db_Detials.YearID, RefVoucherID), false);
                                        fgdtls_f.DataSource = DB.GetDT(string.Format("Select * from {0} ({1},{2},{3},{4},{5},{6}) Where OrderTransType='Sales' and  RefVoucherID in ({7})", "fn_FetchFabricOrders", CommonLogic.SQuote(Localization.ToSqlDateString(dtChlnDate.Text.ToString())), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboParty.SelectedValue.ToString(), RefVoucherID), false);
                                        foreach (UltraGridBand band in fgdtls_f.DisplayLayout.Bands)
                                        {
                                            foreach (UltraGridColumn column in band.Columns)
                                            {
                                                using (IDataReader dr = DB.GetRS(string.Format("Select * From {0} Where GridID = {1} and SubGridID = 1 and ColIndex = {2}", Db_Detials.tbl_GridSettings, iIDentity, column.Index)))
                                                {
                                                    while (dr.Read())
                                                    {
                                                        column.Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
                                                        column.Hidden = (Localization.ParseBoolean(dr["IsHidden"].ToString()));
                                                        //column.Hidden = (Localization.ParseBoolean(dr["IsHidden"].ToString()) == true ? false : true);
                                                        column.CellActivation = Activation.NoEdit;
                                                    }
                                                }
                                            }

                                        }
                                    }
                                    if (fgdtls_f.Rows.Count <= 0)
                                    {
                                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Order Details Not Found");
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
            if (cboParty.SelectedValue != null && cboParty.SelectedValue.ToString() != "")
                sPrevPartyID = cboParty.SelectedValue.ToString();
        }

        private void fgdtls_f_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            if (e != null)
            {
                e.Layout.Override.RowSizing = RowSizing.Free;
                e.Layout.Bands[0].AutoPreviewEnabled = true;
                e.Layout.Override.FilterUIType = FilterUIType.FilterRow;
                e.Layout.Override.FilterOperandStyle = FilterOperandStyle.Combo;
                e.Layout.Override.FilterEvaluationTrigger = FilterEvaluationTrigger.OnCellValueChange;
                e.Layout.Override.FilterOperatorLocation = FilterOperatorLocation.WithOperand;
                e.Layout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.StartsWith;
                e.Layout.Override.FilterClearButtonLocation = FilterClearButtonLocation.RowAndCell;
                e.Layout.Override.FilterRowAppearance.BackColor = Color.LightYellow;
                e.Layout.Override.FilterRowPromptAppearance.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
                e.Layout.Override.SpecialRowSeparator = SpecialRowSeparator.FilterRow;
                e.Layout.Override.SpecialRowSeparatorAppearance.BackColor = Color.FromArgb(0xe9, 0xf2, 0xc7);
                e.Layout.Override.AllowRowSummaries = AllowRowSummaries.True;
                e.Layout.Override.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
                e.Layout.Override.SummaryDisplayArea |= SummaryDisplayAreas.GroupByRowsFooter;
                e.Layout.Override.SummaryDisplayArea |= SummaryDisplayAreas.InGroupByRows;
                e.Layout.Override.GroupBySummaryDisplayStyle = GroupBySummaryDisplayStyle.SummaryCells;
                e.Layout.Override.SummaryFooterAppearance.BackColor = SystemColors.Info;
                e.Layout.Override.SummaryValueAppearance.BackColor = SystemColors.Window;
                e.Layout.Override.SummaryValueAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                e.Layout.Override.GroupBySummaryValueAppearance.BackColor = SystemColors.Window;
                e.Layout.Override.GroupBySummaryValueAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                e.Layout.Bands[0].SummaryFooterCaption = "Grand Totals:";
                e.Layout.Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                e.Layout.Override.SummaryFooterCaptionVisible = Infragistics.Win.DefaultableBoolean.False;
                e.Layout.Override.SpecialRowSeparator = SpecialRowSeparator.SummaryRow;
                e.Layout.Override.SpecialRowSeparatorAppearance.BackColor = Color.FromArgb(0xda, 0xd9, 0xf1);
                e.Layout.Override.SpecialRowSeparatorHeight = 6;
                e.Layout.Override.BorderStyleSpecialRowSeparator = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
                e.Layout.Override.CellClickAction = CellClickAction.EditAndSelectText;
                e.Layout.Override.SelectTypeRow = SelectType.None;
                e.Layout.ViewStyle = ViewStyle.SingleBand;
                e.Layout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
            }
        }

        private void cboOrderType_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                DataGridViewEx ex2 = this.fgDtls;
                if (cboOrderType.Text == "WITHOUT ORDER")
                {
                    ex2.Columns[2].ReadOnly = true;
                    ex2.Columns[4].ReadOnly = false;
                    ex2.Columns[6].ReadOnly = false;
                    ex2.Columns[7].ReadOnly = false;
                    ex2.Columns[8].ReadOnly = false;
                    ex2.Columns[10].ReadOnly = false;
                    ex2.Columns[11].ReadOnly = false;
                    ex2.Columns[12].ReadOnly = false;
                    ex2.Columns[13].ReadOnly = false;
                    ex2.Columns[14].ReadOnly = false;
                    ex2.Columns[15].ReadOnly = false;
                    ex2.Columns[17].ReadOnly = false;
                    ex2.Columns[18].ReadOnly = false;
                    ex2.Columns[22].ReadOnly = false;
                    ex2.Columns[3].ReadOnly = false;
                    ex2.Columns[27].ReadOnly = false;
                    btnShowOrder.Enabled = false;
                }

                //For Order Column Changes
                if (cboOrderType.Text == "WITHOUT ORDER")
                {
                    ex2.Columns[2].ReadOnly = true;
                    ex2.Columns[2].Visible = false;
                }
                else
                {
                    ex2.Columns[2].ReadOnly = true;
                    ex2.Columns[2].Visible = true;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public bool CheckOrder()
        {
            if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
            {
                for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                {
                    if (fgDtls.Rows[i].Cells[2].Value == null || fgDtls.Rows[i].Cells[2].Value.ToString() == "" || fgDtls.Rows[i].Cells[2].Value.ToString() == "0")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Valid OrderNo");
                        fgDtls.CurrentCell = fgDtls[2, i];
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion

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

                                decimal FabricDesign = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select WtPerMtr From tbl_FabricDesignMaster Where FabricDesignID=" + Localization.ParseNativeInt(fgDtls.Rows[i].Cells[6].Value.ToString()) + "")));
                                fgDtls.Rows[i].Cells[23].Value = Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[17].Value.ToString());
                                fgDtls.Rows[i].Cells[24].Value = Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[18].Value.ToString());

                                if (fgDtls.Rows[i].Cells[24].Value == null || Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[24].Value.ToString()) == 0)
                                {
                                    if (FabricDesign != 0)
                                        fgDtls.Rows[i].Cells[24].Value = FabricDesign;
                                }
                                double Weight = 0;
                                if (fgDtls.Rows[i].Cells[23].Value != null && Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[23].Value.ToString()) > 0 && Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[23].Value.ToString()) != 0)
                                {
                                    if (fgDtls.Rows[i].Cells[24].Value == null || Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[24].Value.ToString()) == 0)
                                    {
                                        Weight = (Localization.ParseNativeDouble(FabricDesign.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[24].Value.ToString())) * Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[17].Value.ToString());
                                    }
                                    else
                                    {
                                        Weight = (Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[24].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[23].Value.ToString())) * Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[17].Value.ToString());
                                    }

                                    if (Weight.ToString() != "NaN")
                                    {
                                        fgDtls.Rows[i].Cells[18].Value = Math.Round(Weight, 3);
                                    }
                                }

                                string LotNo = Conversions.ToString(row.Cells[3].Value);
                                if ((LotNo == null) || (LotNo.Trim()) == "" || LotNo == "-")
                                {
                                    LotNo = "-";
                                }
                                if (MyID != "" && fgDtls.Rows[i].Cells[15].Value != null && fgDtls.Rows[i].Cells[15].Value.ToString() != "" && fgDtls.Rows[i].Cells[15].Value.ToString() != "0" && fgDtls.Rows[i].Cells[17].Value != null && fgDtls.Rows[i].Cells[17].Value.ToString() != "" && fgDtls.Rows[i].Cells[17].Value.ToString() != "0")
                                {

                                    strQry = strQry + DBSp.InsertIntoFabrIcStockLedger(
                                    Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                    MyID,
                                    (i + 1).ToString(),
                                    txtEntryNo.Text,
                                    dtChlnDate.Text,
                                    Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                                    row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                    (row.Cells[46].Value.ToString().Trim() == "" ? "0" : row.Cells[46].Value.ToString()),
                                    (row.Cells[48].Value.ToString().Trim() == "" ? "0" : row.Cells[48].Value.ToString()),
                                    LotNo,
                                    (row.Cells[4].Value.ToString().Trim() == null ? "-" : row.Cells[4].Value.ToString().Trim() == "" ? "-" : row.Cells[4].Value.ToString()),
                                    row.Cells[5].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[5].Value.ToString()),
                                    row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                    row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                    row.Cells[8].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                    row.Cells[13].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                                    row.Cells[14].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[14].Value.ToString()),
                                    0, 0, 0,
                                    Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                                    Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                                    Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()),
                                    (row.Cells[21].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[21].Value.ToString())),
                                    row.Cells[27].Value == null ? "NULL" : row.Cells[27].Value.ToString(),
                                    row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                    row.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[29].Value.ToString()),
                                    row.Cells[30].Value == null ? "NULL" : row.Cells[30].Value.ToString(),
                                    row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                    row.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[32].Value.ToString()),
                                    row.Cells[33].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[33].Value.ToString()),
                                    row.Cells[34].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[34].Value.ToString()),
                                    row.Cells[35].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[35].Value.ToString()),
                                    row.Cells[36].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[36].Value.ToString()),
                                    row.Cells[37].Value == null || row.Cells[37].Value.ToString() == "" || row.Cells[37].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[37].Value.ToString()),
                                    row.Cells[38].Value == null || row.Cells[38].Value.ToString() == "" || row.Cells[38].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[38].Value.ToString()),
                                    row.Cells[39].Value == null || row.Cells[39].Value.ToString() == "" ? "-" : row.Cells[39].Value.ToString(),
                                    row.Cells[40].Value == null || row.Cells[40].Value.ToString() == "" ? "-" : row.Cells[40].Value.ToString(),
                                    row.Cells[41].Value == null || row.Cells[41].Value.ToString() == "" ? "-" : row.Cells[41].Value.ToString(),
                                    row.Cells[42].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[42].Value.ToString()),
                                    row.Cells[43].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[43].Value.ToString()),
                                    txtUniqueID.Text, i, StatusID, Db_Detials.StoreID, Db_Detials.CompID,
                                    Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                                }
                            }
                        }
                        else
                        {
                            if ((fgDtls.CurrentCell.ColumnIndex == 15) || (fgDtls.CurrentCell.ColumnIndex == 17) || (fgDtls.CurrentCell.ColumnIndex == 18))
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

                                if (MyID != "" && fgDtls.Rows[RowIndex].Cells[15].Value != null && fgDtls.Rows[RowIndex].Cells[15].Value.ToString() != "" && fgDtls.Rows[RowIndex].Cells[15].Value.ToString() != "0" && fgDtls.Rows[RowIndex].Cells[17].Value != null && fgDtls.Rows[RowIndex].Cells[17].Value.ToString() != "" && fgDtls.Rows[RowIndex].Cells[17].Value.ToString() != "0")
                                {
                                    decimal FabricDesign = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select WtPerMtr From tbl_FabricDesignMaster Where FabricDesignID=" + Localization.ParseNativeInt(fgDtls.Rows[RowIndex].Cells[6].Value.ToString()) + "")));
                                    double Weight = 0;
                                    if (fgDtls.Rows[RowIndex].Cells[23].Value != null && Localization.ParseNativeDecimal(fgDtls.Rows[RowIndex].Cells[23].Value.ToString()) > 0 && Localization.ParseNativeDecimal(fgDtls.Rows[RowIndex].Cells[23].Value.ToString()) != 0)
                                    {
                                        if (fgDtls.Rows[RowIndex].Cells[24].Value == null || Localization.ParseNativeDecimal(fgDtls.Rows[RowIndex].Cells[24].Value.ToString()) == 0)
                                        {
                                            Weight = (Localization.ParseNativeDouble(FabricDesign.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[24].Value.ToString())) * Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[17].Value.ToString());
                                        }
                                        else
                                        {
                                            Weight = (Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[24].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[23].Value.ToString())) * Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[17].Value.ToString());
                                        }
                                        if (Weight.ToString() != "NaN")
                                        {
                                            fgDtls.Rows[RowIndex].Cells[18].Value = Math.Round(Weight, 3);
                                        }
                                    }

                                    string LotNo = Conversions.ToString(row.Cells[3].Value);
                                    if ((LotNo == null) || (LotNo.Trim() == "0") || (LotNo.Trim() == ""))
                                    {
                                        LotNo = "-";
                                    }

                                    strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and RowIndex=" + fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[50].Value + " and AddedBy=" + Db_Detials.UserID + ";");

                                    strQry = strQry + DBSp.InsertIntoFabrIcStockLedger(
                                    Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                    MyID,
                                    (RowIndex + 1).ToString(),
                                    txtEntryNo.Text,
                                    dtChlnDate.Text,
                                    Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                                    row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                    (row.Cells[46].Value.ToString().Trim() == "" ? "0" : row.Cells[46].Value.ToString()),
                                    (row.Cells[48].Value.ToString().Trim() == "" ? "0" : row.Cells[48].Value.ToString()),
                                    LotNo,
                                    (row.Cells[4].Value.ToString().Trim() == null ? "-" : row.Cells[4].Value.ToString().Trim() == "" ? "-" : row.Cells[4].Value.ToString()),
                                    row.Cells[5].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[5].Value.ToString()),
                                    row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                    row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                    row.Cells[8].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                    row.Cells[13].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                                    row.Cells[14].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[14].Value.ToString()),
                                    0, 0, 0,
                                    Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                                    Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                                    Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()),
                                    (row.Cells[21].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[21].Value.ToString())),
                                    row.Cells[27].Value == null ? "NULL" : row.Cells[27].Value.ToString(),
                                    row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                    row.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[29].Value.ToString()),
                                    row.Cells[30].Value == null ? "NULL" : row.Cells[30].Value.ToString(),
                                    row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                    row.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[32].Value.ToString()),
                                    row.Cells[33].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[33].Value.ToString()),
                                    row.Cells[34].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[34].Value.ToString()),
                                    row.Cells[35].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[35].Value.ToString()),
                                    row.Cells[36].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[36].Value.ToString()),
                                    row.Cells[37].Value == null || row.Cells[37].Value.ToString() == "" || row.Cells[37].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[37].Value.ToString()),
                                    row.Cells[38].Value == null || row.Cells[38].Value.ToString() == "" || row.Cells[38].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[38].Value.ToString()),
                                    row.Cells[39].Value == null || row.Cells[39].Value.ToString() == "" ? "-" : row.Cells[39].Value.ToString(),
                                    row.Cells[40].Value == null || row.Cells[40].Value.ToString() == "" ? "-" : row.Cells[40].Value.ToString(),
                                    row.Cells[41].Value == null || row.Cells[41].Value.ToString() == "" ? "-" : row.Cells[41].Value.ToString(),
                                    row.Cells[42].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[42].Value.ToString()),
                                    row.Cells[43].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[43].Value.ToString()),
                                    txtUniqueID.Text, RowIndex, StatusID, Db_Detials.StoreID, Db_Detials.CompID,
                                    Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
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

        private void GetRefModID()
        {
            #region Multiple series Mapping
            try
            {
                RefVoucherID = "";
                RefMenuID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MenuID From tbl_VoucherTypeMaster Where GenMenuID=" + iIDentity + "")));
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
                        RefVoucherID += DB.GetSnglValue(string.Format("Select VoucherTypeID From tbl_VoucherTypeMaster Where GenMenuID='" + arr[i] + "'")) + ",";
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

        public void ExecuterTempQry_Orders(int RowIndex)
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
                                strQry = string.Format("Delete From tbl_FabricOrderLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                                for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                                {
                                    DataGridViewRow row = fgDtls.Rows[i];

                                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                                    {
                                        StatusID = 1;
                                        MyID = iMaxMyID_Orders.ToString();
                                    }
                                    else
                                    {
                                        StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_FabricOrderLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_FabricOrderLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + "")));
                                        MyID = txtCode.Text;
                                    }

                                    if (MyID != "" && row.Cells[2].Value != null && row.Cells[2].Value.ToString().Trim() != "" && row.Cells[2].Value.ToString().Trim() != "0" && row.Cells[17].Value != null && row.Cells[17].Value.ToString().Trim() != "" && row.Cells[17].Value.ToString().Trim() != "0")
                                    {
                                        string sBatchNo = string.Empty;
                                        sBatchNo = DB.GetSnglValue("Select FabSONo from fn_FabricSalesOrderMain_Tbl() Where FabSOID=" + row.Cells[2].Value.ToString());

                                        if (Localization.ParseNativeDouble(row.Cells[17].Value.ToString()) > 0)
                                        {
                                            strQry = DBSp.InsertIntoFabricOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                            MyID,
                                            (i + 1).ToString(),
                                            txtEntryNo.Text,
                                            dtChlnDate.Text,
                                            Localization.ParseNativeDouble(cboParty.SelectedValue.ToString()),
                                            row.Cells[46].Value == null ? "0" : row.Cells[46].Value.ToString() == "" ? "0" : row.Cells[46].Value.ToString(),
                                            "0",
                                            row.Cells[2].Value.ToString(),
                                            sBatchNo,
                                            Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                            Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                            Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                            Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                            Localization.ParseNativeDouble(row.Cells[14].Value.ToString()),
                                            Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                            0, 0, 0,
                                            Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                                            Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                                            Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()),
                                            0,
                                            row.Cells[27].Value == null || row.Cells[27].Value.ToString() == "" || row.Cells[27].Value.ToString() == "0" ? "NULL" : Convert.ToString(row.Cells[27].Value),
                                            0,
                                            row.Cells[34].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[34].Value.ToString()),
                                            row.Cells[35].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[35].Value.ToString()),
                                            row.Cells[36].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[36].Value.ToString()),
                                            row.Cells[37].Value == null || row.Cells[37].Value.ToString() == "" || row.Cells[37].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[37].Value.ToString()),
                                            row.Cells[38].Value == null || row.Cells[38].Value.ToString() == "" || row.Cells[38].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[38].Value.ToString()),
                                            row.Cells[39].Value == null || row.Cells[39].Value.ToString() == "" ? "-" : row.Cells[39].Value.ToString(),
                                            row.Cells[40].Value == null || row.Cells[40].Value.ToString() == "" ? "-" : row.Cells[40].Value.ToString(),
                                            row.Cells[41].Value == null || row.Cells[41].Value.ToString() == "" ? "-" : row.Cells[41].Value.ToString(),
                                            row.Cells[42].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[42].Value.ToString()),
                                            row.Cells[43].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[43].Value.ToString()),
                                            txtUniqueID.Text, i, 1, "Sales", row.Cells[44].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[44].Value.ToString()),
                                            Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);

                                            //strQry += DBSp.InsertIntoFabricOrderLedger(MyID, (i + 1).ToString(), txtEntryNo.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                            //          Localization.ParseNativeDouble(cboParty.SelectedValue.ToString()), row.Cells[47].Value == null ? "0" : row.Cells[47].Value.ToString() == "" ? "0" : row.Cells[47].Value.ToString(),
                                            //          row.Cells[2].Value.ToString(), sBatchNo, Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                            //          Localization.ParseNativeDouble(row.Cells[7].Value.ToString()), Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                            //          Localization.ParseNativeDouble(row.Cells[14].Value.ToString()), 0, 0, 0, Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                                            //          Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()), 0, row.Cells[16].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()), "NULL", txtUniqueID.Text, i, StatusID, "Sales",
                                            //          row.Cells[44].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[44].Value.ToString()), Localization.ParseNativeDecimal(iGrossRate.ToString()),
                                            //          row.Cells[27].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[27].Value.ToString()), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if ((fgDtls.CurrentCell.ColumnIndex == 15) || (fgDtls.CurrentCell.ColumnIndex == 17) || (fgDtls.CurrentCell.ColumnIndex == 18))
                                {
                                    DataGridViewRow row = fgDtls.Rows[RowIndex];
                                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                                    {
                                        StatusID = 1;
                                        MyID = iMaxMyID_Orders.ToString();
                                    }
                                    else
                                    {
                                        StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_FabricOrderLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_FabricOrderLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + "")));
                                        MyID = txtCode.Text;
                                    }

                                    if (MyID != "" && row.Cells[2].Value != null && row.Cells[2].Value.ToString().Trim() != "" && row.Cells[2].Value.ToString().Trim() != "0" && row.Cells[17].Value != null && row.Cells[17].Value.ToString().Trim() != "" && row.Cells[17].Value.ToString().Trim() != "0")
                                    {
                                        string sBatchNo = string.Empty;
                                        sBatchNo = DB.GetSnglValue("Select FabSONo from fn_FabricSalesOrderMain_Tbl() Where FabSOID=" + row.Cells[2].Value.ToString());

                                        if (txtUniqueID.Text != null)
                                        {
                                            strQry += string.Format("Delete From tbl_FabricOrderLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text.Trim()) + " and RowIndex=" + fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[50].Value + " and AddedBy=" + Db_Detials.UserID + ";");
                                            strQry = DBSp.InsertIntoFabricOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                            MyID,
                                            (RowIndex + 1).ToString(),
                                            txtEntryNo.Text,
                                            dtChlnDate.Text,
                                            Localization.ParseNativeDouble(cboParty.SelectedValue.ToString()),
                                            row.Cells[46].Value == null ? "0" : row.Cells[46].Value.ToString() == "" ? "0" : row.Cells[46].Value.ToString(),
                                            "0",
                                            row.Cells[2].Value.ToString(),
                                            sBatchNo,
                                            Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                            Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                            Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                            Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                            Localization.ParseNativeDouble(row.Cells[14].Value.ToString()),
                                            Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                            0, 0, 0,
                                            Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                                            Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                                            Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()),
                                            0,
                                            row.Cells[27].Value == null || row.Cells[27].Value.ToString() == "" || row.Cells[27].Value.ToString() == "0" ? "NULL" : Convert.ToString(row.Cells[27].Value),
                                            0,
                                            row.Cells[34].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[34].Value.ToString()),
                                            row.Cells[35].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[35].Value.ToString()),
                                            row.Cells[36].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[36].Value.ToString()),
                                            row.Cells[37].Value == null || row.Cells[37].Value.ToString() == "" || row.Cells[37].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[37].Value.ToString()),
                                            row.Cells[38].Value == null || row.Cells[38].Value.ToString() == "" || row.Cells[38].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[38].Value.ToString()),
                                            row.Cells[39].Value == null || row.Cells[39].Value.ToString() == "" ? "-" : row.Cells[39].Value.ToString(),
                                            row.Cells[40].Value == null || row.Cells[40].Value.ToString() == "" ? "-" : row.Cells[40].Value.ToString(),
                                            row.Cells[41].Value == null || row.Cells[41].Value.ToString() == "" ? "-" : row.Cells[41].Value.ToString(),
                                            row.Cells[42].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[42].Value.ToString()),
                                            row.Cells[43].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[43].Value.ToString()),
                                            txtUniqueID.Text, RowIndex, 1, "Sales", row.Cells[44].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[44].Value.ToString()),
                                            Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                                        }
                                    }
                                }
                            }
                            if (strQry != "" && strQry.Length > 0)
                            {
                                DB.ExecuteSQL(strQry);
                            }
                            //Show Orders
                            fgdtls_f.DataSource = DB.GetDT(string.Format("Select * from {0} ({1},{2},{3},{4},{5},{6}) Where OrderTransType='Sales' and  RefVoucherID in ({7})", "fn_FetchFabricOrders", CommonLogic.SQuote(Localization.ToSqlDateString(dtChlnDate.Text.ToString())), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboParty.SelectedValue.ToString(), RefVoucherID), false);
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
                isRowDel = false;
                iTempDel_RowIndex = -1;
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
                            if ((Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select COUNT(0) From fn_StockFabricLedger_tbl() Where RefId='" + fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[45].Value + "' and RefID<>'' and Transtype<>" + iIDentity + ""))) > 0))
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityWarning, "", "Reference Found In Another Module..Row Cannot Be Deleted");
                                return;
                            }
                            else
                            {
                                try
                                {
                                    string strQry = string.Format("Update tbl_StockFabricLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[50].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                                    DB.ExecuteSQL(strQry);
                                }
                                catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }

                                //Row Removing Commented and Will be Deleted After Next KeyDown Event
                                //fgDtls.Rows.RemoveAt(fgDtls.CurrentRow.Index);
                                isRowDel = true;
                                iTempDel_RowIndex = fgDtls.CurrentRow.Index;

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
                                string strQry = string.Format("Update tbl_StockFabricLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[49].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                                if (strQry.Length > 0)
                                    DB.ExecuteSQL(strQry);
                            }
                            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }

                            //Row Removing Commented and Will be Deleted After Next KeyDown Event
                            //fgDtls.Rows.RemoveAt(fgDtls.CurrentRow.Index);
                            isRowDel = true;
                            iTempDel_RowIndex = fgDtls.CurrentRow.Index;

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

        protected void fgDtls_KeyDown_Orders(object sender, KeyEventArgs e)
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
                            if ((Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select COUNT(0) From fn_FabricOrderLedger_tbl() Where RefId='" + fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[45].Value + "' and RefID<>'' and Transtype<>" + iIDentity + ""))) > 0))
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityWarning, "", "Reference Found In Another Module..Row Cannot Be Deleted");
                                return;
                            }
                            else
                            {
                                try
                                {
                                    string strQry = string.Format("Update tbl_FabricOrderLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[49].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                                    DB.ExecuteSQL(strQry);
                                    //Show Orders
                                    fgdtls_f.DataSource = DB.GetDT(string.Format("Select * from {0} ({1},{2},{3},{4}) Where OrderTransType='Sales' and  RefVoucherID in ({5})", "fn_FetchFabricOrders", CommonLogic.SQuote(Localization.ToSqlDateString(dtChlnDate.Text.ToString())), Db_Detials.CompID, Db_Detials.YearID, cboParty.SelectedValue.ToString(), RefVoucherID), false);
                                }
                                catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                                //Row Removing Commented and Will be Deleted After KeyDown Event
                                //fgDtls.Rows.RemoveAt(fgDtls.CurrentRow.Index);
                                isRowDel = true;
                                iTempDel_RowIndex = fgDtls.CurrentRow.Index;

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
                            //Row Removing Commented and Will be Deleted After KeyDown Event
                            //fgDtls.Rows.RemoveAt(fgDtls.CurrentRow.Index);
                            try
                            {
                                string strQry = string.Format("Update tbl_FabricOrderLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[50].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                                DB.ExecuteSQL(strQry);

                                //Show Orders
                                fgdtls_f.DataSource = DB.GetDT(string.Format("Select * from {0} ({1},{2},{3},{4},{5},{6}) Where OrderTransType='Sales' and  RefVoucherID in ({7})", "fn_FetchFabricOrders", CommonLogic.SQuote(Localization.ToSqlDateString(dtChlnDate.Text.ToString())), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboParty.SelectedValue.ToString(), RefVoucherID), false);
                            }
                            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }

                            isRowDel = true;
                            iTempDel_RowIndex = fgDtls.CurrentRow.Index;

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

                    if (isRowDel && iTempDel_RowIndex != -1)
                    {
                        fgDtls.Rows.RemoveAt(iTempDel_RowIndex);
                        iTempDel_RowIndex = -1;
                    }

                    DataTable table5 = (DataTable)frmObj.dt_HasDtls_Grd;
                    DataTable table6 = (DataTable)frmObj.dt_AryCalcvalue;
                    DataTable table7 = (DataTable)frmObj.dt_AryIsRequired;

                    if (fgDtls.RowCount == 0)
                    {
                        EventHandles.CreateDefault_Rows(fgDtls, table5, table6, table7, false, false);
                        EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                    }
                    else
                    {
                        EventHandles.CreateDefault_Rows(fgDtls, table5, table6, table7, true, false);
                        EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
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
                fgDtls.Rows[i].Cells[50].Value = i;
            }
        }

        private void setMyID_Orders()
        {
            iMaxMyID_Orders = Localization.ParseNativeInt(DB.GetSnglValue("Select MAX(MyId + 1) from tbl_FabricOrderLedger Where IsDeleted=0"));

            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[51].Value = iMaxMyID_Orders;
            }
        }

        private void setMyID_Stock()
        {
            iMaxMyID_Stock = Localization.ParseNativeInt(DB.GetSnglValue("Select MAX(MyId + 1) from tbl_StockFabricLedger Where IsDeleted=0"));

            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[49].Value = iMaxMyID_Stock;
            }
        }

        private void frmFabricOutward_FormClosed(object sender, FormClosedEventArgs e)
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

            if (strUniqueID != null)
            {
                string strQry = string.Format("Delete From tbl_FabricOrderLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                strQry = strQry + string.Format("Update  tbl_FabricOrderLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                DB.ExecuteSQL(strQry);
                strQry = string.Format("Update tbl_FabricOrderLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                DB.ExecuteSQL(strQry);
                strQry = "";
            }
        }
    }
}

