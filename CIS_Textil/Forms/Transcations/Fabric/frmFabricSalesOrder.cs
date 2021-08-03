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
using CIS_Utilities;
using CIS_CLibrary;

namespace CIS_Textil
{
    public partial class frmFabricSalesOrder : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;
        bool F_SO = false;
        bool FSO_RATETYPE = false;
        private bool flg_MTY_DC;
        private bool flg_Series;
        bool OVERDUE_ALT = false;
        bool FABSO_APRVD = false;
        private bool flg_Email;
        private bool flg_Sms;
        private int RefMenuID;
        private string SRateCalcType = string.Empty;

        public frmFabricSalesOrder()
        {
            F_SO = false;
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
        }

        #region Event

        private void frmFabricSalesOrder_Load(object sender, EventArgs e)
        {
            try
            {
                OVERDUE_ALT = Localization.ParseBoolean(GlobalVariables.OVERDUE_ALT);
                FSO_RATETYPE = Localization.ParseBoolean(GlobalVariables.FSO_RATETYPE);
                FABSO_APRVD = Localization.ParseBoolean(GlobalVariables.FABSO_APRVD);
                F_SO = Localization.ParseBoolean(GlobalVariables.FSO);
                flg_MTY_DC = Localization.ParseBoolean(GlobalVariables.MTY_DC);
                flg_Series = Localization.ParseBoolean(GlobalVariables.flg_Series);


                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboPartyName, Combobox_Setup.ComboType.Mst_Customer, "");
                Combobox_Setup.FillCbo(ref cboBroker, Combobox_Setup.ComboType.Mst_Brokers, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
                Combobox_Setup.FillCbo(ref cboDelivaryAt, Combobox_Setup.ComboType.Mst_Ledger, "");
                Combobox_Setup.FillCbo(ref cboOrderStatus, Combobox_Setup.ComboType.Mst_OrderStatus, "");
                Combobox_Setup.FillCbo(ref cboHaste, Combobox_Setup.ComboType.Mst_Haste, "");

                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                txtEntryNo.Enabled = false;

                this.cboPartyName.SelectedValueChanged += new System.EventHandler(this.cboPartyName_SelectedValueChanged);

                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    FillControls();
                }
                GetRefModID();
                this.fgDtls.KeyDown += new KeyEventHandler(this.fgDtls_KeyDown);
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

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    txtOrderNo.Text = CommonCls.AutoInc(this, "FabSONo", "FabSOID", "");
                }
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                int MaxID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format(" Select Isnull(Max(FabSOID),0) From {0}  Where CompID = {1} and YearID = {2} and VoucherTypeID={3}", "tbl_FabricSalesOrderMain", Db_Detials.CompID, Db_Detials.YearID, this.frmVoucherTypeID)));
                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where IsDeleted=0 and FabSOID = {1} and CompID={2} and YearID={3}", new object[] { "tbl_FabricSalesOrderMain", MaxID, Db_Detials.CompID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        dtOrderDate.Text = Localization.ToVBDateString(reader["FabSODate"].ToString());
                        cboPartyName.SelectedValue = Localization.ParseNativeInt(reader["PartyID"].ToString());
                        cboBroker.SelectedValue = Localization.ParseNativeInt(reader["BrokerID"].ToString());
                        cboTransport.SelectedValue = Localization.ParseNativeInt(reader["TransportID"].ToString());
                        cboDelivaryAt.SelectedValue = Localization.ParseNativeInt(reader["DelivaryAtID"].ToString());
                        cboHaste.SelectedValue = Localization.ParseNativeInt(reader["HasteID"].ToString());
                    }
                }
                //cboSeries.SelectedValue =this.frmVoucherTypeID;
                dtEntryDate.Focus();
                //dtEntryDate.Text = Conversions.ToString(DateAndTime.Now.Date);
                cboSeries_SelectedValueChanged(null, null);
                GetRefModID();
                //if (flg_Series)
                //{
                //    cboSeries.Enabled = true;
                //}
                //else
                //{
                //    int id = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MiscID from fn_MiscMaster_tbl() Where MiscType='SERIESTYPE' and MiscName='Regular'")));
                //    cboSeries.Enabled = false;
                //    cboSeries.SelectedValue = id.ToString();
                //}

                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    try
                    {
                        if (FABSO_APRVD)
                        {
                            cboOrderStatus.SelectedValue = DB.GetSnglValue("Select MiscID from fn_MiscMaster_tbl()  Where MiscType='Order Status' AND MiscName='Approved'");
                            cboOrderStatus.Enabled = false;
                        }
                        else
                        {
                            cboOrderStatus.SelectedValue = DB.GetSnglValue("Select MiscID from fn_MiscMaster_tbl()  Where MiscType='Order Status' AND MiscName='Pending'");
                            cboOrderStatus.Enabled = false;
                        }
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "FabSOID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtOrderNo, "FabSONo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtOrderDate, "FabSODate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboPartyName, "PartyID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDelivaryAt, "DelivaryAtID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboHaste, "HasteID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCreditdays, "CrDays", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboOrderStatus, "VoucherTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboOrderStatus, "OrderStatusID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEd1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);
                DetailGrid_Setup.FillGrid(fgDtls, fgDtls.Grid_UID, fgDtls.Grid_Tbl, "FabSOID", txtCode.Text, base.dt_HasDtls_Grd);

                System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                dgvCellStyle.BackColor = System.Drawing.Color.LightGray;
                dgvCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
                dgvCellStyle.SelectionBackColor = System.Drawing.Color.Purple;
                dgvCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    cboOrderStatus.Enabled = false;
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                }
                else
                {
                    cboOrderStatus.Enabled = true;
                }

                if ((base.blnFormAction == Enum_Define.ActionType.View_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    cboOrderStatus.Enabled = true;
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
                ("(#OTHERNO#)"),
                dtOrderDate.TextFormat(false, true),
                cboPartyName.SelectedValue,
                cboBroker.SelectedValue,
                cboTransport.SelectedValue,
                cboDelivaryAt.SelectedValue,
                cboHaste.SelectedValue,
                txtCreditdays.Text,
                cboOrderStatus.SelectedValue,
                string.Format("{0:N0}", CommonCls.GetColSum(fgDtls, 7, -1, -1)).Replace(",", ""),
                string.Format("{0:N2}", CommonCls.GetColSum(fgDtls, 9, -1, -1)).Replace(",", ""),
                string.Format("{0:N2}", CommonCls.GetColSum(fgDtls, 11, -1, -1)).Replace(",", ""),
                txtDescription.Text,
                cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue,
                cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue,
                dtEd1.TextFormat(false,true), 
                txtET1.Text,
                txtET2.Text,
                txtET3.Text
               };

                string sBatchNo = string.Empty;
                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_FabricOrderLedger", "(#CodeID#)", Localization.ParseNativeInt(base.iIDentity.ToString()));
                for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    if (row.Cells[3].Value != null && row.Cells[3].Value.ToString() != "" && row.Cells[9].Value != null && row.Cells[9].Value.ToString() != "")
                    {
                        if (Localization.ParseNativeDouble(row.Cells[9].Value.ToString()) > 0)
                        {
                            if (row.Cells[15].Value != null && row.Cells[15].Value.ToString() != "")
                            {
                                if (Localization.ParseNativeInt(row.Cells[15].Value.ToString()) == Localization.ParseNativeInt(DB.GetSnglValue("Select MiscID from fn_MiscMaster_tbl()  Where MiscType='Order Status' AND MiscName='Approved'")))
                                {
                                    strAdjQry += DBSp.InsertIntoFabricOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                       "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)",
                                       dtOrderDate.Text, Localization.ParseNativeDouble(cboPartyName.SelectedValue.ToString()),
                                       base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                       "0", "(#CodeID#)", txtOrderNo.Text,
                                       Localization.ParseNativeDouble(row.Cells[2].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[4].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[3].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                       Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                       Localization.ParseNativeDecimal(row.Cells[8].Value.ToString()),
                                       Localization.ParseNativeDecimal(row.Cells[7].Value.ToString()),
                                       Localization.ParseNativeDecimal(row.Cells[9].Value.ToString()),
                                       0, 0, 0, 0,
                                       Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()),
                                       row.Cells[14].Value == null || row.Cells[14].Value.ToString() == "" || row.Cells[14].Value.ToString() == "0" ? "NULL" : Convert.ToString(row.Cells[14].Value),
                                       0,
                                       row.Cells[16].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[16].Value.ToString()),
                                       row.Cells[17].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                       row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                       row.Cells[19].Value == null || row.Cells[19].Value.ToString() == "" || row.Cells[19].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[19].Value.ToString()),
                                       row.Cells[20].Value == null || row.Cells[20].Value.ToString() == "" || row.Cells[20].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[20].Value.ToString()),
                                       row.Cells[21].Value == null || row.Cells[21].Value.ToString() == "" ? "-" : row.Cells[21].Value.ToString(),
                                       row.Cells[22].Value == null || row.Cells[22].Value.ToString() == "" ? "-" : row.Cells[22].Value.ToString(),
                                       row.Cells[23].Value == null || row.Cells[23].Value.ToString() == "" ? "-" : row.Cells[23].Value.ToString(),
                                       row.Cells[24].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[24].Value.ToString()),
                                       row.Cells[25].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[25].Value.ToString()),
                                       "NULL", i, 1, "Sales", this.frmVoucherTypeID,
                                       Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);

                                }
                                else if (Localization.ParseNativeInt(row.Cells[15].Value.ToString()) == Localization.ParseNativeInt(DB.GetSnglValue("Select MiscID from fn_MiscMaster_tbl()  Where MiscType='Order Status' AND MiscName='Completed'")))
                                {
                                    decimal dBalQty = Localization.ParseNativeDecimal(DB.GetSnglValue("Select BalQty from  fn_FetchFabricOrders(" + "'" + Localization.ToSqlDateString(DateTime.Now.ToString()) + "'" + "," + Db_Detials.CompID + "," + Db_Detials.YearID + "," + cboPartyName.SelectedValue.ToString() + ")" + " Where ARefID= '" + base.iIDentity.ToString() + "|" + txtCode.Text + "|" + (i + 1).ToString() + "'"));
                                    decimal dBalMeters = Localization.ParseNativeDecimal(DB.GetSnglValue("Select BalMeters from  fn_FetchFabricOrders(" + "'" + Localization.ToSqlDateString(DateTime.Now.ToString()) + "'" + "," + Db_Detials.CompID + "," + Db_Detials.YearID + "," + cboPartyName.SelectedValue.ToString() + ")" + " Where ARefID= '" + base.iIDentity.ToString() + "|" + txtCode.Text + "|" + (i + 1).ToString() + "'"));

                                    strAdjQry += DBSp.InsertIntoFabricOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                      "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)",
                                      dtOrderDate.Text, Localization.ParseNativeDouble(cboPartyName.SelectedValue.ToString()),
                                      base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                      "0", "(#CodeID#)", txtOrderNo.Text,
                                      Localization.ParseNativeDouble(row.Cells[2].Value.ToString()),
                                      Localization.ParseNativeDouble(row.Cells[4].Value.ToString()),
                                      Localization.ParseNativeDouble(row.Cells[3].Value.ToString()),
                                      Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                      Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                      Localization.ParseNativeDecimal(row.Cells[8].Value.ToString()),
                                      Localization.ParseNativeDecimal(row.Cells[7].Value.ToString()),
                                      Localization.ParseNativeDecimal(row.Cells[9].Value.ToString()),
                                      0, 0, 0, 0,
                                      Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()),
                                      "On Completion Dr Side From Sales Order Only",
                                      0,
                                      row.Cells[16].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[16].Value.ToString()),
                                      row.Cells[17].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                      row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                      row.Cells[19].Value == null || row.Cells[19].Value.ToString() == "" || row.Cells[19].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[19].Value.ToString()),
                                      row.Cells[20].Value == null || row.Cells[20].Value.ToString() == "" || row.Cells[20].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[20].Value.ToString()),
                                      row.Cells[21].Value == null || row.Cells[21].Value.ToString() == "" ? "-" : row.Cells[21].Value.ToString(),
                                      row.Cells[22].Value == null || row.Cells[22].Value.ToString() == "" ? "-" : row.Cells[22].Value.ToString(),
                                      row.Cells[23].Value == null || row.Cells[23].Value.ToString() == "" ? "-" : row.Cells[23].Value.ToString(),
                                      row.Cells[24].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[24].Value.ToString()),
                                      row.Cells[25].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[25].Value.ToString()),
                                      "NULL", i, 1, "Sales", this.frmVoucherTypeID,
                                      Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);

                                    if (dBalQty == 0)
                                    {
                                        dBalQty = Localization.ParseNativeDecimal(row.Cells[7].Value.ToString());
                                    }
                                    if (dBalMeters == 0)
                                    {
                                        dBalMeters = Localization.ParseNativeDecimal(row.Cells[9].Value.ToString());
                                    }

                                    strAdjQry += DBSp.InsertIntoFabricOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                     "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)",
                                     dtOrderDate.Text, Localization.ParseNativeDouble(cboPartyName.SelectedValue.ToString()),
                                     base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                     "0", "(#CodeID#)", txtOrderNo.Text,
                                     Localization.ParseNativeDouble(row.Cells[2].Value.ToString()),
                                     Localization.ParseNativeDouble(row.Cells[4].Value.ToString()),
                                     Localization.ParseNativeDouble(row.Cells[3].Value.ToString()),
                                     Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                     Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                     Localization.ParseNativeDecimal(row.Cells[8].Value.ToString()),
                                     0, 0, 0,dBalQty,dBalMeters,0,
                                     Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()),
                                     "On Completion Cr Side From Sales Order Only",
                                     0,
                                     row.Cells[16].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[16].Value.ToString()),
                                     row.Cells[17].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                     row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                     row.Cells[19].Value == null || row.Cells[19].Value.ToString() == "" || row.Cells[19].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[19].Value.ToString()),
                                     row.Cells[20].Value == null || row.Cells[20].Value.ToString() == "" || row.Cells[20].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[20].Value.ToString()),
                                     row.Cells[21].Value == null || row.Cells[21].Value.ToString() == "" ? "-" : row.Cells[21].Value.ToString(),
                                     row.Cells[22].Value == null || row.Cells[22].Value.ToString() == "" ? "-" : row.Cells[22].Value.ToString(),
                                     row.Cells[23].Value == null || row.Cells[23].Value.ToString() == "" ? "-" : row.Cells[23].Value.ToString(),
                                     row.Cells[24].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[24].Value.ToString()),
                                     row.Cells[25].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[25].Value.ToString()),
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
                DBSp.Transcation_AddEdit_Validate_Trans(pArrayData, this.fgDtls, true, ref dblTransID, strAdjQry, "", txtEntryNo.Text, txtOrderNo.Text, "FabSONo", this.frmVoucherTypeID);

                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) || (base.blnFormAction == Enum_Define.ActionType.View_Record))
                {
                    flg_Sms = Localization.ParseBoolean(GlobalVariables.SMS_SEND_SO);
                    flg_Email = Localization.ParseBoolean(GlobalVariables.EMAIL_SEND_SO);

                    if (blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        string sEntryNo = DB.GetSnglValue("SELECT  Entryno from fn_FabricSalesOrderMain_tbl() WHERE FabSOID=" + dblTransID);
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
                                    CommonCls.sendEmail(dblTransID.ToString(), sEntryNo, sPartyID, base.iIDentity.ToString(), false, this.frmVoucherTypeID, 1);
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
                                    CommonCls.sendEmail(txtCode.Text, txtEntryNo.Text, sPartyID, base.iIDentity.ToString(), true, this.frmVoucherTypeID, 1);
                                }
                                catch { }
                            }
                        }
                    }
                }
                string sqlQuery = string.Format("select distinct FabDesOrderNo as 'OrderID' , FabDesOrderNo as 'OrderNo' from tbl_FabricDesignMaster where CompID = {0} and YearID = {1} and FabDesOrderNo is not null and FabDesOrderNo not in (Select InternalSONo From fn_FabricSalesOrderMain_tbl() where StoreID={0} and CompID = {1} and BranchID={2} and YearID = {3}) order by FabDesOrderNo", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID);
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
                        strTblName = "tbl_FabricSalesOrderMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "FabSONo", this.txtOrderNo.Text, false, "", 0, "FabricID=" + this.frmVoucherTypeID + " and StoreID=" + Db_Detials.StoreID + " and CompID = " + Db_Detials.CompID + " and BranchID" + Db_Detials.BranchID + " And YearID =" + Db_Detials.YearID + " and VoucherTypeID=" + this.frmVoucherTypeID + "", "Duplicate Sales Order No in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where FabSONo = '{1}' and CompID = {2} and YearID = {3} and VoucherTypeID={4} ", new object[] { "tbl_FabricSalesOrderMain", txtOrderNo.Text.ToString(), Db_Detials.CompID, Db_Detials.YearID, this.frmVoucherTypeID }))))
                        {
                            txtOrderNo.Focus();
                            return true;
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        strTblName = "tbl_FabricSalesOrderMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "FabSONo", txtOrderNo.Text, true, "FabSOID", Localization.ParseNativeLong(txtCode.Text), "FabricID=" + this.frmVoucherTypeID + " and PartyID =" + cboPartyName.SelectedValue + " AND StoreID=" + Db_Detials.StoreID + " and CompID = " + Db_Detials.CompID + " and BranchID" + Db_Detials.BranchID + " And YearID =" + Db_Detials.YearID + " and VoucherTypeID=" + this.frmVoucherTypeID + "", "This Party already used this Sales Order No in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where FabSONo = '{1}' and StoreID={2} and CompID = {3} and BranchID={4} and YearID = {5} and VoucherTypeID={6} ", new object[] { "fn_FabricSalesOrderMain_tbl()", this.txtOrderNo.Text.ToString(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, this.frmVoucherTypeID }))))
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
                if (cboPartyName.SelectedValue == null || cboPartyName.Text.Trim().ToString() == "-" || cboPartyName.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Party ");
                    cboPartyName.Focus();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                return false;
            }

        }

        //private void CalcVal()
        //{
        //    try
        //    {
        //        lblTotalQty.Text = string.Format("{0:N0}", CommonCls.GetColSum(fgDtls, 7, -1, -1));
        //        lblTotalMtrs.Text = string.Format("{0:N2}", CommonCls.GetColSum(fgDtls, 9, -1, -1));
        //        lblTotalAmt.Text = string.Format("{0:N2}", CommonCls.GetColSum(fgDtls, 11, -1, -1));
        //    }
        //    catch (Exception ex)
        //    {
        //        Navigate.logError(ex.Message, ex.StackTrace);
        //    }
        //}

        private void fgDtls_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    SRateCalcType = "";
                    if (fgDtls.Rows[e.RowIndex].Cells[6].Value != null && fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString() != "" && fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString() != "-")
                    {
                        SRateCalcType = DB.GetSnglValue("Select RateCalcType from fn_UnitsMaster_tbl() Where UnitID=" + fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString() + "");
                    }
                    switch (e.ColumnIndex)
                    {

                        case 7:
                            if (fgDtls.Rows[e.RowIndex].Cells[7].Value != null && fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[8].Value != null && fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString() != "0")
                            {
                                fgDtls.Rows[e.RowIndex].Cells[9].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString())).ToString()));

                                if (SRateCalcType == "P")
                                {
                                    if (fgDtls.Rows[e.RowIndex].Cells[7].Value != null && fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[10].Value != null && fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString() != "0")
                                        fgDtls.Rows[e.RowIndex].Cells[11].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString())).ToString()));
                                }
                                else if (SRateCalcType == "M")
                                {
                                    if (fgDtls.Rows[e.RowIndex].Cells[9].Value != null && fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[10].Value != null && fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString() != "0")
                                        fgDtls.Rows[e.RowIndex].Cells[11].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString())).ToString()));
                                }
                            }
                            break;

                        case 8:
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[7].Value != null && fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[8].Value != null && fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString() != "0")
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[9].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString())).ToString()));
                                }
                                if (fgDtls.Rows[e.RowIndex].Cells[9].Value != null && fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[10].Value != null && fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString() != "0")
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[11].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString())).ToString()));
                                }
                            }

                            break;

                        case 9:
                            if (SRateCalcType == "M")
                            {
                                if (Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString())).ToString())) != Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[11].Value == null ? "0" : fgDtls.Rows[e.RowIndex].Cells[11].Value.ToString()))
                                {
                                    if (fgDtls.Rows[e.RowIndex].Cells[9].Value != null && fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[10].Value != null && fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString() != "0")
                                    {
                                        fgDtls.Rows[e.RowIndex].Cells[11].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString())).ToString()));
                                    }
                                }
                                else
                                {
                                    if (fgDtls.Rows[e.RowIndex].Cells[9].Value != null && fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[10].Value != null && fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString() != "0")
                                    {
                                        fgDtls.Rows[e.RowIndex].Cells[11].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString())).ToString()));
                                    }
                                }
                            }
                            else if (SRateCalcType == "P")
                            {
                                if (Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString())).ToString())) != Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[11].Value == null ? "0" : fgDtls.Rows[e.RowIndex].Cells[11].Value.ToString()))
                                {
                                    if (fgDtls.Rows[e.RowIndex].Cells[7].Value != null && fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[8].Value != null && fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString() != "0")
                                    {
                                        fgDtls.Rows[e.RowIndex].Cells[11].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString())).ToString()));
                                    }
                                }
                                else
                                {
                                    if (fgDtls.Rows[e.RowIndex].Cells[7].Value != null && fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[10].Value != null && fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString() != "0")
                                    {
                                        fgDtls.Rows[e.RowIndex].Cells[11].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString())).ToString()));
                                    }
                                }
                            }

                            break;

                        case 10:
                            if (SRateCalcType == "M")
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[9].Value != null && fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[10].Value != null && fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString() != "0")
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[11].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString())).ToString()));
                                }
                            }
                            else if (SRateCalcType == "P")
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[7].Value != null && fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[10].Value != null && fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString() != "0")
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[11].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString())).ToString()));
                                }
                            }

                            break;

                        case 11:
                            if (SRateCalcType == "M")
                            {
                                if (Localization.ParseNativeDouble(Operators.DivideObject(fgDtls.Rows[e.RowIndex].Cells[11].Value, fgDtls.Rows[e.RowIndex].Cells[10].Value).ToString()) != Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString()))
                                {
                                    if (fgDtls.Rows[e.RowIndex].Cells[11].Value != null && fgDtls.Rows[e.RowIndex].Cells[11].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[10].Value != null && fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString() != "0")
                                    {
                                        fgDtls.Rows[e.RowIndex].Cells[9].Value = Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[11].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString());
                                    }
                                }
                            }
                            else if (SRateCalcType == "P")
                            {
                                if (Localization.ParseNativeDouble(Operators.DivideObject(fgDtls.Rows[e.RowIndex].Cells[11].Value, fgDtls.Rows[e.RowIndex].Cells[10].Value).ToString()) != Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString()))
                                {
                                    if (fgDtls.Rows[e.RowIndex].Cells[11].Value != null && fgDtls.Rows[e.RowIndex].Cells[11].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[10].Value != null && fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString() != "0")
                                    {
                                        fgDtls.Rows[e.RowIndex].Cells[7].Value = Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[11].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString());
                                    }
                                }
                            }
                            break;
                    }

                    if (e.ColumnIndex == 3)
                    {
                        if (fgDtls.Rows[e.RowIndex].Cells[3].Value != null && fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString().Length > 0)
                        {
                            fgDtls.Rows[e.RowIndex].Cells[4].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", fgDtls.Rows[e.RowIndex].Cells[3].Value)));
                        }
                    }
                    //else if (fgDtls.Rows.Count > 1)
                    //{
                    //    if ((fgDtls.Rows[e.RowIndex].Cells[3].Value != null) && (fgDtls.Rows[e.RowIndex].Cells[4].Value != null) && (fgDtls.Rows[e.RowIndex].Cells[5].Value != null))
                    //    {
                    //        int count = 0;
                    //        for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    //        {
                    //            if (fgDtls.Rows[e.RowIndex].Cells[3].Value == fgDtls.Rows[i].Cells[3].Value)
                    //            {
                    //                if (fgDtls.Rows[e.RowIndex].Cells[4].Value == fgDtls.Rows[i].Cells[4].Value)
                    //                {
                    //                    if (fgDtls.Rows[e.RowIndex].Cells[5].Value == fgDtls.Rows[i].Cells[5].Value)
                    //                    {
                    //                        count = count + 1;
                    //                    }
                    //                }
                    //            }
                    //        }
                    //        if (count > 0)
                    //        {
                    //            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "This Product is already available in this order");
                    //            fgDtls.Rows[e.RowIndex].Cells[5].Value = null;
                    //        }
                    //    }
                    //}

                    if (e.ColumnIndex == 7 && Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString()) > 0.0)
                    {
                        if (Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select isnull(cuts,0) From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", fgDtls.Rows[e.RowIndex].Cells[3].Value))) <= 0)
                        {
                            return;
                        }
                        int OrderLimitQty = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select {0}({1},{2},{3},{4},{5})", new object[] { "dbo.fn_Chk_DesignOrderLimit_k", fgDtls.Rows[e.RowIndex].Cells[3].Value, fgDtls.Rows[e.RowIndex].Cells[4].Value, fgDtls.Rows[e.RowIndex].Cells[5].Value, Db_Detials.CompID, Db_Detials.YearID })));
                        //int OrderLimitQty = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Isnull(Sum(Qty),0) AS Qty From fn_FabricSalesOrderDtls() Where CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " and DesignID=" + fgDtls.Rows[e.RowIndex].Cells[3].Value + " and QualityID=" + fgDtls.Rows[e.RowIndex].Cells[4].Value + " and ShadeID=" + fgDtls.Rows[e.RowIndex].Cells[5].Value + "")));
                        if (OrderLimitQty <= 0)
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "There is not enough stock left to book an order for this Design");
                            fgDtls.Rows[e.RowIndex].Cells[6].Value = "0";
                            fgDtls.Rows[e.RowIndex].Cells[7].Value = "0";
                        }
                        else if (Operators.ConditionalCompareObjectLess(OrderLimitQty, fgDtls.Rows[e.RowIndex].Cells[6].Value, false))
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "There are only " + Conversions.ToString(OrderLimitQty) + "  Quantities left to book an order for this Design");
                            fgDtls.Rows[e.RowIndex].Cells[6].Value = "0";
                            fgDtls.Rows[e.RowIndex].Cells[7].Value = "0";
                        }
                    }

                    if (e.ColumnIndex == 7 || e.ColumnIndex == 10)
                    {
                        if (fgDtls.Rows[e.RowIndex].Cells[7].Value != null && fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[10].Value != null && fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString() != "0")
                        {
                            if (SRateCalcType == "P")
                                fgDtls.Rows[e.RowIndex].Cells[11].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString())).ToString()));
                        }
                    }

                    //if (FSO_RATETYPE == true)
                    //{
                    //    if (e.ColumnIndex == 10)
                    //    {
                    //        if (Conversion.Val(fgDtls.Rows[e.RowIndex].Cells[10].Value) > 0)
                    //        {
                    //            if ((fgDtls.Rows[e.RowIndex].Cells[4].Value != null))
                    //            {
                    //                decimal QualityRate = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select Rate From {0} Where FabricQualityID = {1}", Db_Detials.fn_FabricQualityMaster_tbl, fgDtls.Rows[e.RowIndex].Cells[4].Value)));
                    //                if (QualityRate == 0 | QualityRate == 1)
                    //                {
                    //                    fgDtls.Rows[e.RowIndex].Cells[16].Value = false;
                    //                }
                    //                else if (Localization.ParseNativeDecimal(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString()) >= QualityRate)
                    //                {
                    //                    fgDtls.Rows[e.RowIndex].Cells[16].Value = true;
                    //                }
                    //                else
                    //                {
                    //                    fgDtls.Rows[e.RowIndex].Cells[16].Value = false;
                    //                }
                    //            }
                    //        }
                    //    }
                    //}
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
                if (((base.blnFormAction == Enum_Define.ActionType.New_Record) || (base.blnFormAction == Enum_Define.ActionType.Edit_Record)))
                {
                    switch (e.ColumnIndex)
                    {
                        case 3:
                            try { fgDtls.Rows[e.RowIndex].Cells[15].Value = cboOrderStatus.SelectedValue; }
                            catch { }
                            break;

                        case 4:
                            fgDtls.Rows[e.RowIndex].Cells[10].Value = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select Rate From {0} Where FabricQualityID = {1}", Db_Detials.tbl_FabricQualityMaster, fgDtls.Rows[e.RowIndex].Cells[4].Value)));
                            lblLastOrderRate.Text = DB.GetSnglValue(string.Format("Select [dbo].[fn_FetchLastOrderRate]({0},0,{1},0,{2},{3},{4},{5})", cboPartyName.SelectedValue, fgDtls.Rows[e.RowIndex].Cells[4].Value, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID));
                            break;

                        case 9:
                            if (fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString() != "0")
                            {
                                fgDtls.Rows[e.RowIndex].Cells[11].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString())).ToString()));
                            }
                            break;

                        case 10:
                            if (fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString() != "0")
                            {
                                fgDtls.Rows[e.RowIndex].Cells[11].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString())).ToString()));
                            }
                            break;

                        case 11:
                            if (fgDtls.Rows[e.RowIndex].Cells[9].Value != null && fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[11].Value != null && fgDtls.Rows[e.RowIndex].Cells[11].Value.ToString() != "0")
                            {
                                if (Localization.ParseNativeDouble(Operators.DivideObject(fgDtls.Rows[e.RowIndex].Cells[11].Value, fgDtls.Rows[e.RowIndex].Cells[9].Value).ToString()) != Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString()))
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[10].Value = Math.Round(Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[11].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString()));
                                }
                            }
                            break;
                    }

                    if (e.ColumnIndex == 3 || e.ColumnIndex == 4 || e.ColumnIndex == 5)
                    {
                        for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                        {
                            if (fgDtls.Rows[i].Cells[3].Value != null && fgDtls.Rows[i].Cells[3].Value.ToString() != "" && fgDtls.Rows[i].Cells[4].Value != null && fgDtls.Rows[i].Cells[4].Value.ToString() != "" && fgDtls.Rows[i].Cells[5].Value != null && fgDtls.Rows[i].Cells[5].Value.ToString() != "")
                            {
                                fgDtls.Rows[i].Cells[2].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricID from fn_FabricMaster_Tbl() where FabricDesignID={0} and FabricQualityID={1} and FabricShadeID={2}", fgDtls.Rows[i].Cells[3].Value, fgDtls.Rows[i].Cells[4].Value, fgDtls.Rows[i].Cells[5].Value)));
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

        private void cboSeries_SelectedValueChanged(object sender, EventArgs e)
        {
            //if (cboSeries.SelectedValue != null)
            {
                txtOrderNo.Text = CommonCls.AutoInc(this, "FabSONo", "FabSOID", string.Format("VoucherTypeID = '{0}'", this.frmVoucherTypeID));
            }
            fgDtls_CellValueChanged(null, null);
        }

        private void cboPartyName_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboPartyName.SelectedValue != null && cboPartyName.SelectedValue.ToString() != "0")
                {
                    cboBroker.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select BrokerID From {0} Where LedgerID = {1}", "fn_LedgerMaster_tbl()", cboPartyName.SelectedValue)));
                    cboTransport.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select TransportId From {0} Where LedgerID = {1}", "tbl_LedgerMaster_tbl()", cboPartyName.SelectedValue)));
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboOrderStatus_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    fgDtls.Rows[i].Cells[15].Value = cboOrderStatus.SelectedValue;
                }

                using (DataTable Dt = DB.GetDT("SELECT * from tbl_FabricOutwardDtls WHERE OrderID=" + txtCode.Text + " and FabOutwardID IN(SELECT FabOutwardID FROM tbl_FabricOutwardMain WHERE IsDeleted=0 AND StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " AND YearID=" + Db_Detials.YearID + " )", false))
                {
                    if (Dt.Rows.Count > 0)
                    {
                        if ((cboOrderStatus.SelectedValue != null))
                        {
                            for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                            {
                                if (Localization.ParseNativeDouble(txtCode.Text) > 0)
                                {
                                    if (fgDtls.Rows[i].Cells[3].Value.ToString() != "" && fgDtls.Rows[i].Cells[3].Value != null && fgDtls.Rows[i].Cells[4].Value.ToString() != "" && fgDtls.Rows[i].Cells[4].Value != null && fgDtls.Rows[i].Cells[5].Value.ToString() != "" && fgDtls.Rows[i].Cells[5].Value != null)
                                    {
                                        DataRow[] rst = Dt.Select("DesignID=" + fgDtls.Rows[i].Cells[3].Value + " and QualityID=" + fgDtls.Rows[i].Cells[4].Value + " and ShadeID=" + fgDtls.Rows[i].Cells[5].Value);
                                        if (rst.Length == 0)
                                        {
                                            fgDtls.Rows[i].Cells[15].Value = cboOrderStatus.SelectedValue;
                                            fgDtls.Rows[i].Cells[15].ReadOnly = false;
                                        }
                                        else
                                        {
                                            fgDtls.Rows[i].Cells[15].ReadOnly = true;
                                        }
                                    }
                                }
                                else
                                {
                                    fgDtls.Rows[i].Cells[15].Value = cboOrderStatus.SelectedValue;
                                    fgDtls.Rows[i].Cells[15].ReadOnly = false;
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

        private void cboPartyName_Validated(object sender, EventArgs e)
        {
            try
            {
                if (OVERDUE_ALT)
                {
                    if (blnFormAction == Enum_Define.ActionType.New_Record | blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        if ((cboPartyName.SelectedValue != null))
                        {
                            if (Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(BillNo) From fn_OutStandingCustomer_Billwise({0},'{1}',{2},{3},{4},{5})", cboPartyName.SelectedValue, Localization.ToSqlDateString(dtOrderDate.Text.Trim()), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID))) > 0)
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Question, "Over Due Alert", "");
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

        private void fgDtls_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                if (fgDtls.Rows.Count > 1)
                {
                    if ((cboOrderStatus.SelectedValue != null))
                    {
                        fgDtls.Rows[e.RowIndex].Cells[15].Value = cboOrderStatus.SelectedValue;
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
                CIS_ReportTool.frmMultiPrint.MenuID = base.iIDentity;
                CIS_ReportTool.frmMultiPrint.Id = Localization.ParseNativeInt(this.txtCode.Text);
                CIS_ReportTool.frmMultiPrint.TblNm = "tbl_FabricSalesOrderMain";
                CIS_ReportTool.frmMultiPrint.IdStr = "FabSOID";
                CIS_ReportTool.frmMultiPrint.VoucherTypeID = this.frmVoucherTypeID;
                CIS_ReportTool.frmMultiPrint frmMPrnt = new CIS_ReportTool.frmMultiPrint();
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
                            if ((Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_FabricOutwardDtls_tbl() WHERE FabSOID=" + txtCode.Text + " and DesignID=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[3].Value.ToString()) + " and QualityID=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[4].Value.ToString()) + " and ShadeID=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[5].Value.ToString()) + " and StoreID=" + Db_Detials.StoreID + " and  CompID=" + Db_Detials.CompID + " and  BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + " ")) > 0) || (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_FabricDeliveryChallanDtls() WHERE OrderID=" + txtCode.Text + " and DesignID=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[3].Value.ToString()) + " and QualityID=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[4].Value.ToString()) + " and ShadeID=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[5].Value.ToString()) + " and CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " ")) > 0) || (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_FabricDeliveryChallanDtls() WHERE OrderID=" + txtCode.Text + " and DesignID=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[3].Value.ToString()) + " and QualityID=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[4].Value.ToString()) + " and ShadeID=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[5].Value.ToString()) + " and CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " ")) > 0) || (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_FabricInvoiceCutDtls() WHERE OrderID=" + txtCode.Text + " and DesignID=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[3].Value.ToString()) + " and QualityID=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[4].Value.ToString()) + " and ShadeID=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[5].Value.ToString()) + " and CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " ")) > 0))
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
                                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                            }
                            else
                            {
                                EventHandles.CreateDefault_Rows(fgDtls, table2, table3, table4, true, false);
                                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
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

        private void cboPartyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (blnFormAction == Enum_Define.ActionType.New_Record || blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (CommonCls.IsPartyBlocked(Localization.ParseNativeInt(cboPartyName.SelectedValue.ToString())) == true)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Selected Party Is Blocked...");
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

    }
}
