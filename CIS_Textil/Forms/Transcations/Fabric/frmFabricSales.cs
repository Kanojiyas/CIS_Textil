using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DataGridViewEx;
using CIS_DBLayer;
using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class frmFabricSales : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;
        public DataGridViewEx fgDtls_f;
        public DataGridViewEx fgDtls_f_footer;

        private int iMaxMyID_Orders;
        public static bool FAB_MAINTAINWEIGHT;
        private bool FDC_ORD_WISE;
        bool FDC_ORD_COMP = false;
        bool FDC_BRK_COM = false;

        private int iMaxMyID_Stock;
        private bool isRowDel = false;

        private bool flg_IsBarcodeScan;
        private bool flg_MTY_DC;
        private bool flg_OrderConform;
        private bool flg_SUB_ORDER;
        private bool flg_Email;
        private bool flg_Sms;
        private bool flg_Series;
        private bool ApplyMain_OneTime;
        private bool FS_BRK_COM = false;
        public bool FSI_DSGN_WS = false;
        public bool FSI_SHADE_WS = false;
        private bool ENABLE_BROKER_FAB_SALESBILL;
        private bool ENABLE_BROKER_CALCMETHOD1;
        private bool ENABLE_BROKER_CALCMETHOD2;
        string SBrokersPerc = string.Empty;
        private int RefMenuID;
        private static string RefVoucherID;
        private string SRateCalcType = string.Empty;

        public frmFabricSales()
        {
            InitializeComponent();

            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;

            fgDtls_f = GrdFooter.fgDtls;
            fgDtls_f_footer = GrdFooter.fgDtls_f;

            flg_OrderConform = false;
            flg_SUB_ORDER = false;
            FDC_ORD_WISE = false;
        }

        #region Event

        private void frmFabricSales_Load(object sender, EventArgs e)
        {
            try
            {
                FSI_DSGN_WS = Localization.ParseBoolean(GlobalVariables.FSI_DSGN_WS);
                ENABLE_BROKER_FAB_SALESBILL = Localization.ParseBoolean(GlobalVariables.ENABLE_BROKER_FAB_SALESBILL);
                ENABLE_BROKER_CALCMETHOD1 = Localization.ParseBoolean(GlobalVariables.ENABLE_BROKER_CALCMETHOD1);
                ENABLE_BROKER_CALCMETHOD2 = Localization.ParseBoolean(GlobalVariables.ENABLE_BROKER_CALCMETHOD2);

                Combobox_Setup.FilterId = "";

                Combobox_Setup.FillCbo(ref cboParty, Combobox_Setup.ComboType.Mst_Customer, "");
                Combobox_Setup.FillCbo(ref cboBroker, Combobox_Setup.ComboType.Mst_Brokers, "");
                Combobox_Setup.FillCbo(ref CboHaste, Combobox_Setup.ComboType.Mst_Ledger, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
                Combobox_Setup.FillCbo(ref cboSalesAc, Combobox_Setup.ComboType.SalesAc, "");

                string sqlQuery = string.Format("Select FabOutwardID, RefNo From {0} Where IsDeleted=0 and storeID={1} and CompID = {2} and BranchID={3} And YearID = {4} ", "tbl_FabricOutwardMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID);

                FS_BRK_COM = Localization.ParseBoolean(GlobalVariables.FS_BRK_COM);
                flg_Series = Localization.ParseBoolean(GlobalVariables.flg_Series);

                flg_MTY_DC = Localization.ParseBoolean(GlobalVariables.MTY_DC);
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls_f, fgDtls_f_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 1, true);
                txtEntryNo.Enabled = false;
                if (ENABLE_BROKER_FAB_SALESBILL)
                {
                    if (ENABLE_BROKER_CALCMETHOD2)
                    {
                        fgDtls.Columns[21].Visible = true;
                        fgDtls.Columns[22].Visible = true;
                        txtBrokerPercent.Enabled = false;
                    }
                    else
                    {
                        fgDtls.Columns[21].Visible = false;
                        fgDtls.Columns[22].Visible = false;
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
                    fgDtls.Columns[21].Visible = false;
                    fgDtls.Columns[22].Visible = false;
                }
                FDC_ORD_COMP = Localization.ParseBoolean(GlobalVariables.FDC_ORD_COMP);
                FDC_ORD_WISE = Localization.ParseBoolean(GlobalVariables.FDC_ORD_WISE);

                this.cboParty.SelectedValueChanged += new System.EventHandler(this.cboParty_SelectedValueChanged);
                this.txtBrokerPercent.Leave += new System.EventHandler(this.txtBrokerPercent_Leave);
                GetRefModID();

                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
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
                DBValue.Return_DBValue(this, txtCode, "FabricSalesID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtBillNo, "BillNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtBillDate, "BillDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboParty, "PartyID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, CboHaste, "HasteID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLrNo, "LrNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtLrDate, "LrDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboSalesAc, "SalesACID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCrDays, "CrDays", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtDueDate, "DueDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtAddtionalDes, "AdditionalDes", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, TxtGrossAmount, "GrossAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAddLessAmt, "AddLessAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtNetAmt, "NetAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtBrokerPercent, "BrokerAvgPercentage", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtBrokerTotalAmount, "BrokerTotalAmount", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEd1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "FabricSalesID", txtCode.Text, base.dt_HasDtls_Grd);
                DetailGrid_Setup.FillGrid(fgDtls_f, this.fgDtls_f.Grid_UID, this.fgDtls_f.Grid_Tbl, "FabricSalesID", txtCode.Text, base.dt_HasDtls_Grd);

                CalcVal();
                cboParty.Enabled = false;

                System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                dgvCellStyle.BackColor = System.Drawing.Color.LightGray;
                dgvCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
                dgvCellStyle.SelectionBackColor = System.Drawing.Color.Purple;
                dgvCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

                try
                {
                    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    {
                        if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_FabricOutwardReturnDtls_FR(" + Db_Detials.StoreID + "," + Db_Detials.CompID + "," + Db_Detials.BranchID + "," + Db_Detials.YearID + ") WHERE FabricSalesID=" + fgDtls.Rows[i].Cells["FabricSalesID"].Value + "")) > 0)
                        {
                            fgDtls.Rows[i].ReadOnly = true;
                            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle;
                        }
                        else
                            fgDtls.Rows[i].ReadOnly = false;
                    }
                }
                catch { }
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
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CreateDefault_Rows(fgDtls_f, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);

                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                EventHandles.CalculateFooter_Rows(fgDtls_f, fgDtls_f_footer, fgDtls_f.Grid_ID.ToString(), fgDtls_f.Grid_UID);
                GetRefModID();
                if (!this.flg_MTY_DC)
                {
                    if (this.frmVoucherTypeID != 0)
                    {
                        string sBillNO = DB.GetSnglValue("SELECT BillNO from fn_FabricSalesMain_Tbl() WHERE FabricSalesID=(SELECT MAX(FabricSalesID) FROm fn_FabricSalesMain_Tbl() WHERE VoucherTypeID=" + this.frmVoucherTypeID + " and storeID=" + Db_Detials.StoreID + " and  CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + ") ");
                        txtBillNo.Text = CommonCls.AutoInc(this, "BillNo", "FabricSalesID", string.Format("VoucherTypeID = '{0}'", sBillNO));
                    }
                    else
                    {
                        this.txtBillNo.Text = CommonCls.AutoInc(this, "BillNo", "FabricSalesID", "");
                    }
                }

                int MaxID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format(" Select Isnull(Max(FabricSalesID),0) From {0}  where IsDeleted=0 AND StoreID={1} and CompID={2} and BranchID={3} and YearID={4}", "tbl_FabricSalesMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID)));

                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where FabricSalesID = {1} and StoreID={2} and CompID={3} and BranchID={4} and YearID={5}", new object[] { "tbl_FabricSalesMain", MaxID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = (Localization.ToVBDateString(reader["EntryDate"].ToString()));
                        dtBillDate.Text = (Localization.ToVBDateString(reader["BillDate"].ToString()));
                        cboParty.SelectedValue = Localization.ParseNativeInt(reader["PartyID"].ToString());
                        cboBroker.SelectedValue = Localization.ParseNativeInt(reader["BrokerID"].ToString());
                        CboHaste.SelectedValue = Localization.ParseNativeInt(reader["HasteID"].ToString());
                        cboTransport.SelectedValue = Localization.ParseNativeInt(reader["TransportID"].ToString());
                        cboSalesAc.SelectedValue = Localization.ParseNativeInt(reader["SalseAcID"].ToString());
                        dtLrDate.Text = (Localization.ToVBDateString(reader["LrDate"].ToString()));
                    }
                }

                dtEntryDate.Focus();
                TxtGrossAmount.Text = "0.00";
                txtAddLessAmt.Text = "0.00";
                txtNetAmt.Text = "0.00";
                txtBrokerPercent.Text = "";
                cboParty.Enabled = true;
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
                (cboOrderType.SelectedItem),
                ("(#OTHERNO#)"),
                (dtBillDate.TextFormat(false, true)),
                (cboParty.SelectedValue),
                (cboBroker.SelectedValue),
                (CboHaste.SelectedValue),
                (cboTransport.SelectedValue),
                (txtLrNo.Text.ToString().Replace("'","^")),
                (dtLrDate.TextFormat(false, true)),
                (txtCrDays.Text.ToString()),
                 (dtDueDate.TextFormat(false, true)),
                (cboSalesAc.SelectedValue),
                (txtAddtionalDes.Text.ToString() == ""?"-": txtAddtionalDes.Text.ToString()),
                string.Format("{0:N2}", Math.Round(CommonCls.GetColSum(this.fgDtls, 13, -1, -1))),
                string.Format("{0:N2}", Math.Round(CommonCls.GetColSum(this.fgDtls, 15, -1, -1))),
                (TxtGrossAmount.Text.ToString().Replace(",", "")),
                (txtAddLessAmt.Text.ToString().Replace(",", "")),
                (txtNetAmt.Text.ToString().Replace(",", "")),
                (txtDescription.Text.ToString() == ""?"-": txtDescription.Text.ToString()),
                (txtBrokerPercent.Text.ToString().Replace(",", "")),
                (txtBrokerTotalAmount.Text.ToString().Replace(",","")),0,
                cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue,
                cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue,
                dtEd1.TextFormat(false,true), 
                txtET1.Text,
                txtET2.Text,
                txtET3.Text
                };

                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_AcLedger", "(#CodeID#)", base.iIDentity.ToString());
                strAdjQry = strAdjQry + string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_VatLedger", "(#CodeID#)", base.iIDentity);
                strAdjQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_BrokerLedger", "(#CodeID#)", base.iIDentity);
                //int UnitID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select UnitID from {0} Where FabOutwardID = {1}", "tbl_FabricOutwardDtls", rows[].Cells[2].Value)));
                //int DepartmentID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select DepartmentID from {0} Where IsDeleted=0 and FabOutwardID = {1}", "tbl_FabricOutwardMain", cboRefNo.SelectedValue)));
                strAdjQry += DBSp.InsertInto_AcLedger("(#CodeID#)", "0", "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                cboParty.SelectedValue.ToString(), 1, Db_Detials.Ac_AdjType.NewRef, "(#CodeID#)", "(#OTHERNO#)", dtBillDate.Text,
                                Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDecimal(txtNetAmt.Text.ToString().Replace(",", "")), 0,
                                txtDescription.Text.Trim(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                strAdjQry += DBSp.InsertInto_AcLedger("(#CodeID#)", "0", "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                cboSalesAc.SelectedValue.ToString(), 2, Db_Detials.Ac_AdjType.NewRef, "(#CodeID#)", "(#OTHERNO#)", dtBillDate.Text,
                                Localization.ParseNativeDouble(base.iIDentity.ToString()), 0, Localization.ParseNativeDecimal(TxtGrossAmount.Text.ToString().Replace(",", "")),
                                txtDescription.Text.Trim(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);

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
                                strAdjQry = strAdjQry + DBSp.InsertInto_AcLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text,
                                    Localization.ParseNativeDouble(base.iIDentity.ToString()), row.Cells[2].Value.ToString(), 2, Db_Detials.Ac_AdjType.OnAccount,
                                    "(#CodeID#)", "(#OTHERNO#)", dtBillDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), 0,
                                    Localization.ParseNativeDecimal(dblDedAmt.ToString()), "null", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID,
                                    DateAndTime.Now.Date);
                            }
                            else
                            {
                                string sDedAmt = dblDedAmt.ToString();
                                if (sDedAmt.StartsWith("-"))
                                {
                                    sDedAmt = sDedAmt.Substring(1);
                                }
                                dblDedAmt = Localization.ParseNativeDouble(sDedAmt.ToString());
                                strAdjQry = strAdjQry + DBSp.InsertInto_AcLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text,
                                    Localization.ParseNativeDouble(base.iIDentity.ToString()), row.Cells[2].Value.ToString(), 2, Db_Detials.Ac_AdjType.OnAccount,
                                    "(#CodeID#)", "(#OTHERNO#)", dtBillDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                    Localization.ParseNativeDecimal(dblDedAmt.ToString()), 0, "null", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID,
                                    DateAndTime.Now.Date);
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

                for (int i = 0; i <= (fgDtls.Rows.Count - 1); i++)
                {
                    DataGridViewRow row2 = fgDtls.Rows[i];
                    try
                    {
                        if (ENABLE_BROKER_CALCMETHOD2)
                        {
                            if (ENABLE_BROKER_FAB_SALESBILL)
                            {
                                if (row2.Cells[22].Value != null && row2.Cells[22].Value.ToString() != "" && row2.Cells[22].Value.ToString() != "0")
                                {
                                    if (cboBroker.SelectedValue != null && cboBroker.SelectedValue.ToString() != "" && cboBroker.SelectedValue.ToString() != "0")
                                    {
                                        strAdjQry = strAdjQry + DBSp.InsertIntoBrokerLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboBroker.SelectedValue.ToString()), "NULL", dtEntryDate.Text.ToString(), Localization.ParseNativeDouble(row2.Cells[12].Value.ToString()), Localization.ParseNativeDecimal(row2.Cells[21].Value.ToString()), Localization.ParseNativeDecimal(row2.Cells[22].Value.ToString()), 0, "-", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date, 0, 1);
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
                        if (ENABLE_BROKER_FAB_SALESBILL)
                        {
                            if (txtBrokerTotalAmount.Text != null && txtBrokerTotalAmount.Text != "0.00")
                            {
                                if (cboBroker.SelectedValue != null && cboBroker.SelectedValue.ToString() != "" && cboBroker.SelectedValue.ToString() != "0")
                                {
                                    strAdjQry = strAdjQry + DBSp.InsertIntoBrokerLedger("(#CodeID#)", "0", "(#ENTRYNO#)", Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboBroker.SelectedValue.ToString()), "", dtEntryDate.Text.ToString(), 0, Localization.ParseNativeDecimal(txtBrokerPercent.Text.ToString()), Localization.ParseNativeDecimal(txtBrokerTotalAmount.Text), 0, "", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date, 0, 0);
                                }
                            }
                        }
                    }
                }
                catch { }

                //if (cboTransport.SelectedValue != null && Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()) > 0.0)
                //{
                //    strAdjQry = strAdjQry + DBSp.InsertIntoTrasportLedger("(#CodeID#)", "(#OTHERNO#)", dtBillDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()), Localization.ParseNativeDouble(DepartmentID.ToString()), Localization.ParseNativeDouble(cboParty.SelectedValue.ToString()), txtLrNo.Text, dtLrDate.Text, txtVehicleNo.Text, Localization.ParseNativeDouble(UnitID.ToString()), Localization.ParseNativeInt( string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 8, -1, -1))), Localization.ParseNativeDecimal(string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 10, -1, -1))), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                //}
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");

                double dblTransID = 0;
                string sPartyID = cboParty.SelectedValue.ToString();
                DBSp.Transcation_AddEdit_Trans(pArrayData, fgDtls, true, ref dblTransID, strAdjQry, "", txtEntryNo.Text, txtBillNo.Text, "BillNo", this.frmVoucherTypeID, new DataGridViewEx[] { fgDtls_f });

                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) || (base.blnFormAction == Enum_Define.ActionType.View_Record))
                {
                    flg_Sms = Localization.ParseBoolean(GlobalVariables.SMS_SEND_FabInv);
                    flg_Email = Localization.ParseBoolean(GlobalVariables.EMAIL_SEND_FabInv);

                    if (blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        string sEntryNo = DB.GetSnglValue("SELECT EntryNo from fn_FabricSalesMain_tbl() WHERE FabricSalesID=" + dblTransID);
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

                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        if (Localization.ParseBoolean(GlobalVariables.PASAV) && (CIS_Utilities.CIS_Dialog.Show("Do you want to Print this Record?", GetAssemblyInfo.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                        {
                            CIS_ReportTool.frmReportViewer frmReport = new CIS_ReportTool.frmReportViewer();
                            frmReport.PrinterID = -1;
                            frmReport.PrintCopies = 1;
                            frmReport.iCompID = Db_Detials.CompID;
                            frmReport.iYearID = Db_Detials.YearID;
                            frmReport.iStoreID = Db_Detials.StoreID;
                            frmReport.iBranchID = Db_Detials.BranchID;
                            frmReport.iUserID = Db_Detials.UserID;
                            frmReport.objReport = Db_Detials.objReport;
                            frmReport.iID = Localization.ParseNativeInt(dblTransID.ToString());
                            frmReport.sReportID = "20";
                            frmReport.strApplicationName = GetAssemblyInfo.ProductName;
                            frmReport.GenerateReport(base.iIDentity, "", "", Localization.ParseNativeInt(dblTransID.ToString()), "", 0, true, 0, false, 0);
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
                        strTblName = "tbl_FabricSalesMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "BillNo", txtBillNo.Text, false, "", 0, string.Format("StoreID={0} and CompID = {1} and BranchID={2} and YearID = {3} and VoucherTypeID={4}", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, this.frmVoucherTypeID), "This Bill No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where BillNo = '{1}' And CompID = {2} and YearID = {3} and VoucherTypeID=" + this.frmVoucherTypeID + "", new object[] { "tbl_FabricSalesMain", txtBillNo.Text.ToString(), Db_Detials.CompID, Db_Detials.YearID }))))
                        {
                            txtBillNo.Focus();
                            return true;
                        }
                    }
                    else if (Localization.ParseNativeInt(base.blnFormAction.ToString()) == 1)
                    {
                        strTblName = "tbl_FabricSalesMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "BillNo", txtBillNo.Text, true, "FabricSalesID", Localization.ParseNativeLong(txtCode.Text), string.Format("StoreID={0} and CompID = {1} and BranchID={2} and YearID = {3} and VoucherTypeID={4}", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, this.frmVoucherTypeID), "This Bill No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where BillNo = '{1}' And CompID = {2} and YearID = {3} and VoucherTypeID=" + this.frmVoucherTypeID + "", new object[] { "tbl_FabricSalesMain", txtBillNo.Text.ToString(), Db_Detials.CompID, Db_Detials.YearID }))))
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
                if (cboParty.SelectedValue == null || cboParty.Text.Trim().ToString() == "-" || cboParty.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Party");
                    cboParty.Focus();
                    return true;
                }

                if (cboSalesAc.SelectedValue == null || cboSalesAc.Text.Trim().ToString() == "-" || cboSalesAc.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Sales Account");
                    cboSalesAc.Focus();
                    return true;
                }
                decimal CreditLimit = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select isnull(CreditLimit,0) From {0} Where LedgerId = {1} ", "tbl_LedgerMaster", (cboParty.SelectedValue))));
                decimal TotSalseValue;
                if (CreditLimit > 0)
                {
                    TotSalseValue = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("select sum(isnull(NetAmount,0)) From {0} Where LedgerID = {1} and StoreID={2} and CompID = {3} and BranchID={4} and YearID ={5}", new object[] { "tbl_FabricSalesMain", (this.cboParty.SelectedValue), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })));
                    if (TotSalseValue + Localization.ParseNativeDecimal(txtNetAmt.Text) > CreditLimit)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Exceeding Credit Limit");
                        return true;
                    }
                }

                if (FS_BRK_COM == true)
                {
                    if (cboBroker.SelectedValue == null || cboBroker.Text.Trim().ToString() == "-" || cboBroker.SelectedValue.ToString() == "0")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Broker");
                        cboBroker.Focus();
                        return true;
                    }
                }
                if (TxtGrossAmount.Text.Trim() == "" || TxtGrossAmount.Text.Trim() == "-" || TxtGrossAmount.Text.Trim() == "0.00")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Gross Amount Details");
                    return true;
                }
                if (txtNetAmt.Text.Trim() == "" || txtNetAmt.Text.Trim() == "-" || txtNetAmt.Text.Trim() == "0.00")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Net Amount Details");
                    return true;
                }
                if (ENABLE_BROKER_FAB_SALESBILL)
                {
                    if (cboBroker.SelectedValue == null || cboBroker.Text.Trim().ToString() == "-" || cboBroker.SelectedValue.ToString() == "0")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Broker");
                        cboBroker.Focus();
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

        #endregion

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                #region StockAdjQuery
                string strQry = string.Empty;
                int ibitcol = 0;
                string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", iIDentity, 0.0));
                string strQry_ColName = "";
                string[] arr = CommonCls.GetAdjColName(base.iIDentity, 0.0).Split(';');
                strQry_ColName = arr[0].ToString();
                ibitcol = Localization.ParseNativeInt(arr[1]);
                strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}) Where PartyID={6} and RefDate<={7} and Meters > 0 Order by RefNo ", new object[] { strQry_ColName, snglValue, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboParty.SelectedValue, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(dtBillDate.Text))) });
                #endregion

                frmStockAdj frmStockAdj = new frmStockAdj();
                frmStockAdj.MenuID = base.iIDentity;
                frmStockAdj.Entity_IsfFtr = 0.0;
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
                    frmStockAdj = null;
                }
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
                TxtGrossAmount.Text = string.Format("{0:N2}", Math.Round(CommonCls.GetColSum(this.fgDtls, 18, -1, -1)));

                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (ENABLE_BROKER_CALCMETHOD2)
                    {
                        if (ENABLE_BROKER_FAB_SALESBILL)
                        {
                            txtBrokerTotalAmount.Text = string.Format("{0:N2}", Math.Round(CommonCls.GetColSum(this.fgDtls, 22, -1, -1)));
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
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboParty_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboParty.SelectedValue != null && Localization.ParseNativeDouble(cboParty.SelectedValue.ToString()) > 0.0)
                {
                    cboBroker.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select BrokerID From {0} Where LedgerID = {1}", "tbl_LedgerMaster", (cboParty.SelectedValue))));
                    cboSalesAc.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select PurchSalesID From {0} Where LedgerID = {1}", "tbl_LedgerMaster", (cboParty.SelectedValue))));
                    cboTransport.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select TransportId From {0} Where LedgerID = {1}", "tbl_LedgerMaster", (cboParty.SelectedValue))));

                    lblPartyVatNo.Text = DB.GetSnglValue("SELECT VatTinNo from fn_LedgerMaster_tbl() WHERE LedgerID=" + cboParty.SelectedValue);
                }
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
        }

        private void cboSalesAc_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (!((base.blnFormAction == Enum_Define.ActionType.View_Record) | (base.blnFormAction == Enum_Define.ActionType.Not_Active)))
                {
                    DataGridViewEx ex = this.fgDtls_f;
                    int VatType = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("select VATTypeId from tbl_LedgerMaster where LedgerId={0}", (cboSalesAc.SelectedValue))));
                    if (VatType != 0)
                    {
                        using (IDataReader reader = DB.GetRS(string.Format("select LedgerName, Percentage from tbl_LedgerMaster where VATTypeId={0} and LedgerGroupId=25", VatType)))
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

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void dtBillDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (((base.blnFormAction == 0) | (Localization.ParseNativeInt(base.blnFormAction.ToString()) == 1)) && (this.dtBillDate.Text != "__/__/____"))
                {
                    dtDueDate.Text = (dtBillDate.Text);
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
                DataGridViewEx ex = this.fgDtls_f;
                if (((e.ColumnIndex == 13) | (e.ColumnIndex == 15)) | (e.ColumnIndex == 16))
                {
                    CalcVal();
                }
                if (!((base.blnFormAction == Enum_Define.ActionType.View_Record) || (base.blnFormAction == Enum_Define.ActionType.Not_Active)))
                {
                    SRateCalcType = "";
                    if (fgDtls.Rows[e.RowIndex].Cells[12].Value != null && fgDtls.Rows[e.RowIndex].Cells[12].Value.ToString() != "" && fgDtls.Rows[e.RowIndex].Cells[12].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[12].Value.ToString() != "-")
                    {
                        SRateCalcType = DB.GetSnglValue("Select RateCalcType from tbl_UnitsMaster Where UnitID=" + fgDtls.Rows[e.RowIndex].Cells[12].Value.ToString() + " and IsDeleted=0");
                    }
                    switch (e.ColumnIndex)
                    {
                        case 15:
                            if (SRateCalcType == "M")
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != null || fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != "" && fgDtls.Rows[e.RowIndex].Cells[17].Value.ToString() != null || fgDtls.Rows[e.RowIndex].Cells[17].Value.ToString() != "")
                                    fgDtls.Rows[e.RowIndex].Cells[18].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[17].Value.ToString())).ToString()));
                                for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                                {
                                    if (ex.Rows[i].Cells[4].Value != null && ex.Rows[i].Cells[4].Value.ToString() != "0")
                                    {
                                        ex.Rows[i].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[i].Cells[4].Value))), 100M).ToString().Replace(",", "");
                                        CalcVal();
                                    }
                                }
                            }
                            else if (SRateCalcType == "P")
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString() != null || fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString() != "" && fgDtls.Rows[e.RowIndex].Cells[17].Value.ToString() != null || fgDtls.Rows[e.RowIndex].Cells[17].Value.ToString() != "")
                                    fgDtls.Rows[e.RowIndex].Cells[18].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[17].Value.ToString())).ToString()));
                                for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                                {
                                    if (ex.Rows[i].Cells[4].Value != null && ex.Rows[i].Cells[4].Value.ToString() != "0")
                                    {
                                        ex.Rows[i].Cells[5].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[i].Cells[4].Value))), 100M).ToString().Replace(",", "");
                                        CalcVal();
                                    }
                                }
                            }
                            else if (SRateCalcType == "W")
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[15].Value != null && fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[17].Value != null && fgDtls.Rows[e.RowIndex].Cells[17].Value.ToString() != "0")
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[18].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[17].Value.ToString())).ToString()));
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

                        case 17:
                            if (SRateCalcType == "M")
                            {
                                if (Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[17].Value.ToString())).ToString())) != Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[18].Value == null ? "0" : fgDtls.Rows[e.RowIndex].Cells[18].Value.ToString()))
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[18].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[17].Value.ToString())).ToString()));
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
                            else if (SRateCalcType == "P")
                            {
                                if (Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[17].Value.ToString())).ToString())) != Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[18].Value == null ? "0" : fgDtls.Rows[e.RowIndex].Cells[18].Value.ToString()))
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[18].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[17].Value.ToString())).ToString()));
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
                                if (ENABLE_BROKER_FAB_SALESBILL && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value.ToString() != "0" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value.ToString() != "0")
                                {
                                    decimal dbrokertotalamt_gridrow = (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[21].Value.ToString()) / 100) * (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value.ToString()));
                                    fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value = dbrokertotalamt_gridrow;
                                }
                                else
                                {
                                    fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value = 0;
                                }
                            }
                            CalcVal();
                            break;

                        case 5:
                            if (ENABLE_BROKER_CALCMETHOD2)
                            {
                                if (ENABLE_BROKER_FAB_SALESBILL)
                                {
                                    if (cboBroker.SelectedValue != null && cboBroker.SelectedValue.ToString() != "" && cboBroker.SelectedValue.ToString() != "0")
                                    {
                                        SBrokersPerc = DB.GetSnglValue(string.Format("SELECT percentage from tbl_BrokerPercentDtls a left join tbl_BrokerPercentMain B on A.BrokerPercentID=b.BrokerPercentID where b.BrokerID={0} and a.QualityID={1}", cboBroker.SelectedValue, fgDtls.Rows[e.RowIndex].Cells[5].Value));
                                        fgDtls.Rows[e.RowIndex].Cells[21].Value = SBrokersPerc;

                                        if (fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value.ToString() != "0" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[21].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[21].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[21].Value.ToString() != "0")
                                        {
                                            decimal dbrokertotalamt_gridrow = (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[21].Value.ToString()) / 100) * (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value.ToString()));
                                            fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value = dbrokertotalamt_gridrow;
                                        }
                                        else
                                        {
                                            fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value = 0;
                                        }
                                    }
                                }
                            }
                            CalcVal();
                            break;

                        case 21:
                            if (ENABLE_BROKER_CALCMETHOD2)
                            {
                                if (ENABLE_BROKER_FAB_SALESBILL && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value.ToString() != "0" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[21].Value != null && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[21].Value.ToString() != "" && fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[21].Value.ToString() != "0")
                                {
                                    decimal dbrokertotalamt_gridrow = (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[21].Value.ToString()) / 100) * (Localization.ParseNativeDecimal(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[18].Value.ToString()));
                                    fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value = dbrokertotalamt_gridrow;
                                }
                                else
                                {
                                    fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value = 0;
                                }
                            }
                            CalcVal();
                            break;

                        case 18:
                            if ((e.ColumnIndex == 18) && (Localization.ParseNativeDouble(Conversions.ToString(Operators.DivideObject(fgDtls.Rows[e.RowIndex].Cells[18].Value, fgDtls.Rows[e.RowIndex].Cells[16].Value))) != Localization.ParseNativeDouble(Conversions.ToString(fgDtls.Rows[e.RowIndex].Cells[17].Value))))
                            {
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
                    }
                    CalcVal();
                }
                CalcVal();

                if (fgDtls.RowCount > 1)
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
                }
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }

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

        public void PrintRecord()
        {
            try
            {
                CIS_ReportTool.frmMultiPrint frmMultiPrint = new CIS_ReportTool.frmMultiPrint();
                CIS_ReportTool.frmMultiPrint.MenuID = base.iIDentity;
                CIS_ReportTool.frmMultiPrint.Id = Localization.ParseNativeInt(txtCode.Text);
                CIS_ReportTool.frmMultiPrint.TblNm = "tbl_FabricSalesMain";
                CIS_ReportTool.frmMultiPrint.IdStr = "FabricSalesID";
                CIS_ReportTool.frmMultiPrint.VoucherTypeID = Localization.ParseNativeInt(this.frmVoucherTypeID.ToString());
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

        private void txtCrDays_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if ((((base.blnFormAction == 0) | (Localization.ParseNativeInt(base.blnFormAction.ToString()) == 1)) && ((txtCrDays.Text != null) && (Conversion.Val(txtCrDays.Text) > 0.0))) && (this.dtDueDate.Text != "__/__/____"))
                {
                    DateTime time = Conversions.ToDate(this.dtBillDate.Text);
                    dtDueDate.Text = Localization.ToVBDateString(time.AddDays(Localization.ParseNativeDouble(txtCrDays.Text)).ToString());
                }
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
        }

        public string SetBroker
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

        public string SetHaste
        {
            get
            {
                return CboHaste.SelectedValue.ToString();
            }
            set
            {
                if (value.Length != 0)
                {
                    CboHaste.SelectedValue = value;
                }
            }
        }

        public string setLrDate
        {
            get
            {
                return dtLrDate.TextFormat(false, true);
            }
            set
            {
                if (value.Length != 0)
                {
                    dtLrDate.Text = value;
                }
            }
        }

        public string setLrNo
        {
            get
            {
                return txtLrNo.Text;
            }
            set
            {
                if (value.Length != 0)
                {
                    txtLrNo.Text = value;
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

        private void txtBrokerPercent_Leave(object sender, EventArgs e)
        {
            try
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (ENABLE_BROKER_FAB_SALESBILL)
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

        private void cboParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (blnFormAction == Enum_Define.ActionType.New_Record || blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (CommonCls.IsPartyBlocked(Localization.ParseNativeInt(cboParty.SelectedValue.ToString())) == true)
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

        private void cboHaste_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (CboHaste.SelectedValue != null)
                {
                    if (Localization.ParseNativeInt(CboHaste.SelectedValue.ToString()) > 0)
                        cboTransport.SelectedValue = Localization.ParseNativeDouble(DB.GetSnglValue(String.Format("Select TransportID From {0} Where LedgerID = {1} ", " tbl_LedgerMaster", CboHaste.SelectedValue)));
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
    }
}
