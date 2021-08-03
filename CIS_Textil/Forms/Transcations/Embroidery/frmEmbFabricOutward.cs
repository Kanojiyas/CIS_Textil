using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_DBLayer;
using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class frmEmbFabricOutward : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        private bool FDC_ORD_WISE;
        private bool flg_IsBarcodeScan;
        private bool flg_MTY_DC;
        private bool flg_OrderConform;
        private bool flg_SUB_ORDER;
        private bool flg_Program_ORDER;
        public ArrayList OrgInGridArray;
        bool FDC_ORD_COMP = false;
        bool FDC_BRK_COM = false;
        bool FDC_RATETYPE = false;
        bool OVERDUE_ALT = false;
        private int iMaxMyID_Stock;
        public string strUniqueID;

        public frmEmbFabricOutward()
        {
            InitializeComponent();

            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;

            flg_OrderConform = false;
            OrgInGridArray = new ArrayList();

            FDC_ORD_WISE = false;
            flg_SUB_ORDER = false;
        }

        #region Event

        private void frmEmbFabricOutward_Load(object sender, EventArgs e)
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

                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);

                txtEntryNo.Enabled = false;
                flg_IsBarcodeScan = Localization.ParseBoolean(GlobalVariables.EBR_Scan_Chln);
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

                FDC_ORD_COMP = Localization.ParseBoolean(GlobalVariables.EFDC_ORD_COMP);
                FDC_ORD_WISE = Localization.ParseBoolean(GlobalVariables.EFDC_ORD_WISE);
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

                flg_Program_ORDER = Localization.ParseBoolean(GlobalVariables.FDC_POGRAMCARDNo);

                flg_SUB_ORDER = Localization.ParseBoolean(GlobalVariables.ESUB_ORDER);
                if (flg_SUB_ORDER)
                {
                    lbl0.Visible = true;
                    lblSubOrder.Visible = true;
                    lblSubOrderDate.Visible = true;
                    lbl1.Width = -1;
                    cboSubOrder.Visible = true;
                    dtSubOrderDate.Visible = true;
                }
                else
                {
                    lbl0.Visible = false;
                    lblSubOrder.Visible = false;
                    lblSubOrderDate.Visible = false;
                    lbl1.Width = 0;
                    cboSubOrder.Visible = false;
                    dtSubOrderDate.Visible = false;
                }

                flg_MTY_DC = Localization.ParseBoolean(GlobalVariables.MTY_DC);
                FDC_BRK_COM = Localization.ParseBoolean(GlobalVariables.EFDC_BRK_COM);
                FDC_RATETYPE = Localization.ParseBoolean(GlobalVariables.EMBFDC_RATETYPE);
                OVERDUE_ALT = Localization.ParseBoolean(GlobalVariables.EOVERDUE_ALT);

                FDC_ORD_COMP = Localization.ParseBoolean(GlobalVariables.EFDC_ORD_COMP);

                this.cboParty.SelectedIndexChanged += new System.EventHandler(this.cboParty_SelectedValueChanged);
                this.cboParty.Leave += new System.EventHandler(this.cboParty_Leave);

                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls.KeyDown += new KeyEventHandler(this.fgDtls_KeyDown);
                this.fgDtls.RowsAdded += new DataGridViewRowsAddedEventHandler(this.fgDtls_RowsAdded);
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
                DBValue.Return_DBValue(this, txtCode, "EmbFabOutwardID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtRefNo, "RefNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtRefDate, "RefDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboParty, "PartyID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLrNo, "LRNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDepartment, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboHaste, "HasteID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtOrderDate, "OrderDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, dtSubOrderDate, "SubOrderDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtLrDate, "LRDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtBatchNo, "BatchNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtBales, "Bales", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtBaleNo, "BaleNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDesc1, "Description1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDesc2, "Description2", Enum_Define.ValidationType.Text);
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

                try
                {
                    DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "EmbFabOutwardID", txtCode.Text, base.dt_HasDtls_Grd);
                }
                catch { }

                if (cboParty.SelectedValue != null)
                {
                    if (Localization.ParseNativeDouble(cboParty.SelectedValue.ToString()) > 0.0)
                    {
                        string sqlQuery = string.Format("Select Distinct ProgramNo, EmbProgramID from fn_EmbProgramCardDtls() Where PartyID= " + cboParty.SelectedValue + "");

                        if (cboProgramNo != null)
                        {
                            Combobox_Setup.Fill_Combo(cboProgramNo, sqlQuery, "ProgramNo", "EmbProgramID");
                            cboProgramNo.ColumnWidths = "100;0";
                            cboProgramNo.AutoComplete = true;
                            cboProgramNo.AutoDropdown = true;
                            DBValue.Return_DBValue(this, cboProgramNo, "EmbProgramID", 0);
                        }
                    }
                }

                if (flg_Program_ORDER)
                {
                    string strTable = "";
                    strTable = string.Format("Select Distinct ProgramNo, EmbProgramID from fn_EmbprogramCardDtls() Where PartyID= " + cboParty.SelectedValue + "");
                    Combobox_Setup.Fill_Combo(this.cboSubOrder, strTable, "ProgramNo", "EmbProgramID");
                    cboSubOrder.ColumnWidths = "100;0";
                    cboSubOrder.AutoComplete = true;
                    cboSubOrder.AutoDropdown = true;
                }

                DBValue.Return_DBValue(this, cboSubOrder, "SubOrderID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtSubOrderDate, "SubOrderDate", Enum_Define.ValidationType.IsDate);
                AplySelectBtnEnbl();
                OrgInGridArray.Clear();

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(this.fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls_footer, fgDtls_footer, fgDtls_footer.Grid_ID.ToString(), fgDtls_footer.Grid_UID);
                    cboOrderType.Enabled = false;
                }
                else
                    cboOrderType.Enabled = true;

                int icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_StockFabricLedger_tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    cboOrderType.Enabled = false;
                    cboParty.Enabled = false;
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls_footer, fgDtls_footer, fgDtls_footer.Grid_ID.ToString(), fgDtls_footer.Grid_UID);
                    setTempRowIndex();
                    try
                    {
                        string strOldUniqueID = string.Empty;
                        strOldUniqueID = txtUniqueID.Text;
                        txtUniqueID.Text = CommonCls.GenUniqueID();
                        strUniqueID = txtUniqueID.Text;

                        if (icount == 0)
                        {
                            string strQry = string.Format("Update tbl_StockFabricLEdger Set UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + ", StatusID=2 Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + "");
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

                if ((base.blnFormAction == Enum_Define.ActionType.View_Record) && !(base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_StockFabricLedger_tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));
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
                            btnSelect.Enabled = false;
                            fgDtls.Rows[i].ReadOnly = true;
                            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle_Ref;
                        }
                        else if (icount <= 0)
                        {
                            btnSelect.Enabled = true;
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
                txtCode.Text = "";
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                //if (!this.flg_MTY_DC)
                //{
                //    this.txtChlnNo.Text = CommonCls.AutoInc(this, "RefNo", "EmbFabOutwardID", "");
                //}

                txtRefNo.Text = CommonCls.AutoInc(this, "RefNo", "EmbFabOutwardID", string.Format("VoucherTypeID = {0}", this.frmVoucherTypeID));
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls_footer, fgDtls_footer, fgDtls_footer.Grid_ID.ToString(), fgDtls_footer.Grid_UID);
                dtEntryDate.Focus();
                dtEntryDate.Text = Conversions.ToString(DateAndTime.Now.Date);
                int MaxID = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(EmbFabOutwardID),0) From {0} where StoreID={1} and CompID={2} and  BranchID={3} and YearID={4}", "tbl_EmbFabricOutwardMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID))));
                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where EmbFabOutwardID = {1} and StoreID={2} and CompID={3} and BranchID={4} and YearID={5}", new object[] { "tbl_EmbFabricOutwardMain", MaxID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                        dtRefDate.Text = Localization.ToVBDateString(reader["RefDate"].ToString());
                        cboParty.SelectedValue = Localization.ParseNativeInt(reader["PartyID"].ToString());
                        cboBroker.SelectedValue = Localization.ParseNativeInt(reader["BrokerID"].ToString());
                        cboDepartment.SelectedValue = Localization.ParseNativeInt(reader["DepartmentID"].ToString());
                        cboHaste.SelectedValue = Localization.ParseNativeInt(reader["HasteID"].ToString());
                        cboTransport.SelectedValue = Localization.ParseNativeInt(reader["TransportID"].ToString());
                        dtOrderDate.Text = Localization.ToVBDateString(reader["OrderDate"].ToString());
                        dtLrDate.Text = Localization.ToVBDateString(reader["LRDate"].ToString());

                        if (reader["OrderType"].ToString() != "")
                            cboOrderType.SelectedItem = reader["OrderType"].ToString();
                        else
                            cboOrderType.SelectedItem = "WITH ORDER";

                        cboOrderType.Enabled = false;
                    }
                }

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    cboOrderType.Enabled = false;
                else
                    cboOrderType.Enabled = true;

                AplySelectBtnEnbl();

                if (fgdtls_f != null)
                    fgdtls_f_InitializeLayout(null, null);

                txtUniqueID.Text = CommonCls.GenUniqueID();
                strUniqueID = txtUniqueID.Text;

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
                    this.frmVoucherTypeID,
                    ("(#OTHERNO#)"),
                    (dtRefDate.TextFormat(false, true)),
                    (cboParty.SelectedValue),
                    (cboBroker.SelectedValue),
                    (cboDepartment.SelectedValue),
                    (cboDeliveryAt.SelectedValue),
                    (cboHaste.SelectedValue),
                    (cboOrderType.SelectedItem),
                    (cboProgramNo.SelectedValue),
                    (dtOrderDate.TextFormat(false, true)),
                    (cboTransport.SelectedValue),
                    (txtLrNo.Text.ToString()),
                    (dtLrDate.TextFormat(false, true)),
                    (txtBatchNo.Text.ToString()),
                    (txtBales.Text.ToString()),
                    (txtBaleNo.Text.ToString()),
                    (cboSubOrder.SelectedValue),
                    (dtSubOrderDate.TextFormat(false, true)),
                    (txtDesc1.Text.ToString()),
                    (string.Format("{0:N}", CommonCls.GetColSum(this.fgDtls, 12, -1, -1)).Replace(",", "")),
                    (string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 13, -1, -1)).Replace(",", "")),
                    (string.Format("{0:N3}", CommonCls.GetColSum(this.fgDtls, 14, -1, -1)).Replace(",", "")),
                    (txtDesc2.Text.ToString()),
                    cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue,
                    cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue,
                    dtEd1.TextFormat(false,true), 
                    txtET1.Text,
                    txtET2.Text,
                    txtET3.Text
                };

                int UnitID = 0;
                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", "(#CodeID#)", Localization.ParseNativeInt(Conversions.ToString(base.iIDentity)));
                for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    string BatchNo = Conversions.ToString(row.Cells[4].Value);
                    if (Localization.ParseNativeDouble(Conversions.ToString(row.Cells[13].Value)) != 0.0)
                    {
                        if ((BatchNo == null) || (BatchNo == "0"))
                        {
                            BatchNo = "-";
                        }
                        strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtRefDate.Text,
                                Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                                Localization.ParseNativeInt(row.Cells[20].Value.ToString()),
                                (row.Cells[40].Value == null ? "0" : row.Cells[40].Value.ToString() == "" ? "0" : row.Cells[40].Value.ToString()),
                                row.Cells[41].Value == null ? "NULL" : row.Cells[41].Value.ToString().Trim() == "" ? "NULL" : row.Cells[41].Value.ToString(),
                                row.Cells[3].Value == null ? "NULL" : row.Cells[3].Value.ToString().Trim() == "" ? "NULL" : row.Cells[3].Value.ToString(),
                                row.Cells[4].Value == null ? "NULL" : row.Cells[4].Value.ToString().Trim() == "" ? "NULL" : row.Cells[4].Value.ToString(),
                                row.Cells[6].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[6].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                Localization.ParseNativeInt(row.Cells[10].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[11].Value.ToString()),
                                0, 0, 0, 
                                Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[14].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()), 
                                row.Cells[21].Value == null ? "NULL" : row.Cells[21].Value.ToString().Trim() == "" ? "NULL" : CommonLogic.SQuote(row.Cells[21].Value.ToString()),
                                row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                row.Cells[24].Value == null ? "NULL" : row.Cells[24].Value.ToString().Trim() == "" ? "NULL" : row.Cells[24].Value.ToString(),
                                row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                row.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[29].Value.ToString()),
                                row.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[30].Value.ToString()),
                                row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                row.Cells[32].Value == null || row.Cells[32].Value.ToString() == "" || row.Cells[32].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[32].Value.ToString()),
                                row.Cells[33].Value == null || row.Cells[33].Value.ToString() == "" || row.Cells[33].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[33].Value.ToString()),
                                row.Cells[34].Value == null || row.Cells[34].Value.ToString() == "" ? "-" : row.Cells[34].Value.ToString(),
                                row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" ? "-" : row.Cells[35].Value.ToString(),
                                row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" ? "-" : row.Cells[36].Value.ToString(),
                                row.Cells[37].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[37].Value.ToString()),
                                row.Cells[38].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[38].Value.ToString()),
                                "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                        UnitID = Localization.ParseNativeInt(row.Cells[11].Value.ToString());
                    }
                    row = null;
                }

                //if (cboTransport.SelectedValue != null && Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()) > 0.0)
                //{
                //    strAdjQry = strAdjQry + DBSp.InsertIntoTrasportLedger("(#CodeID#)", "(#OTHERNO#)", dtRefDate.Text,
                //        Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()),
                //        Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()), Localization.ParseNativeDouble(cboParty.SelectedValue.ToString()), txtLrNo.Text,
                //        dtLrDate.Text, null, Localization.ParseNativeDouble(UnitID.ToString()), Localization.ParseNativeInt(string.Format("{0:N}", CommonCls.GetColSum(this.fgDtls, 12, -1, -1))),
                //        Localization.ParseNativeDecimal(string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 13, -1, -1))), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                //}
                strAdjQry += "Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";";
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                DBSp.Transcation_AddEdit(pArrayData, fgDtls, true, strAdjQry, "", txtEntryNo.Text, txtRefNo.Text, "RefNo");
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

                if (!CommonCls.CheckDate(dtEntryDate.Text, true))
                    return true;

                if (!CommonCls.CheckDate(dtRefDate.Text, true))
                    return true;

                if (!CommonCls.CheckDate(dtLrDate.Text, false))
                    return true;

                if (!CommonCls.CheckDate(dtOrderDate.Text, true))
                    return true;

                if (txtEntryNo.Text.Trim() == "" || txtEntryNo.Text.Trim() == "-" || txtEntryNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry No.");
                    txtEntryNo.Focus();
                    return true;
                }

                if (txtRefNo.Text.Trim() == "" || txtRefNo.Text.Trim() == "-" || txtRefNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Challan No.");
                    txtRefNo.Focus();
                    return true;
                }

                if ((txtRefNo.Text != null) && (txtRefNo.Text.Trim().Length > 0))
                {
                    string strTblName;
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        strTblName = "tbl_EmbFabricOutwardMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "RefNo", txtRefNo.Text, false, "", 0, string.Format("StoreID={0} and CompID = {1} and BranchID={2} and YearID = {3}", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID), "This Ref No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where RefNo = '{1}' and StoreID={2} and CompId = {3} and BranchID={4} and YearId = {5} ", new object[] { "tbl_EmbFabricOutwardMain", txtRefNo.Text.ToString(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }))))
                        {
                            this.txtRefNo.Focus();
                            return true;
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        strTblName = "tbl_EmbFabricOutwardMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "RefNo", txtRefNo.Text, true, "EmbFabOutwardID", Localization.ParseNativeLong(txtCode.Text.Trim()), string.Format("StoreID={0} and CompID = {1} and BranchID={2} and YearID = {3}", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID), "This Ref No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where RefNo = '{1}' and StoreID={2} and CompId = {3} and BranchID={4} and YearId = {5} ", new object[] { "tbl_EmbFabricOutwardMain", txtRefNo.Text.ToString(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }))))
                        {
                            txtRefNo.Focus();
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
                    decimal TotSalesValue = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("select sum(isnull(NetAmount,0)) From {0} Where LedgerID = {1} and BranchID={2} and CompID = {3} and BranchID={4} and YearID ={5}", new object[] { "tbl_FabricSalesMain", cboParty.SelectedValue, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })));
                    if (TotSalesValue > CreditLimit)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Exceeding Credit Limit");
                        return true;
                    }
                }

                if (cboDepartment.SelectedValue == null || cboDepartment.Text.Trim().ToString() == "-" || cboDepartment.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    cboDepartment.Focus();
                    return true;
                }

                if (FDC_BRK_COM == true)
                {
                    if (cboBroker.SelectedValue == null || cboBroker.Text.Trim().ToString() == "-" || cboBroker.SelectedValue.ToString() == "0")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Broker");
                        cboBroker.Focus();
                        return true;
                    }
                }

                if (txtBales.Text.Trim() == "" || txtBales.Text.Trim() == "-" || txtBales.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter No. of Bales");
                    txtBales.Focus();
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

        #endregion

        public void AplySelectBtnEnbl()
        {
            try
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
                CIS_ReportTool.frmMultiPrint.TblNm = "tbl_EmbFabricOutwardMain";
                CIS_ReportTool.frmMultiPrint.IdStr = "EmbFabOutwardID";
                CIS_ReportTool.frmMultiPrint frmMPrnt = new CIS_ReportTool.frmMultiPrint();
                CIS_ReportTool.frmMultiPrint.iStoreID = Db_Detials.StoreID;
                CIS_ReportTool.frmMultiPrint.iCompID = Db_Detials.CompID;
                CIS_ReportTool.frmMultiPrint.iBranchID = Db_Detials.BranchID;
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
                if ((Localization.ParseNativeInt(base.blnFormAction.ToString()) == 4) || (Localization.ParseNativeInt(base.blnFormAction.ToString()) == 5))
                {
                    return;
                }
                try
                {
                    if ((e.ColumnIndex == 13) | (e.ColumnIndex == 14) | (e.ColumnIndex == 12))
                    {
                        ExecuterTempQry(e.RowIndex);
                    }
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                }
                switch (e.ColumnIndex)
                {
                    case 4:
                        if (txtScan.Text.Trim().Length <= 0)
                        {
                            //fgDtls.Rows[e.RowIndex].Cells[NDesignID].Value = fgDtls.Rows[e.RowIndex].Cells[7].Value;
                            fgDtls.Rows[e.RowIndex].Cells[8].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", fgDtls.Rows[e.RowIndex].Cells[7].Value)));
                        }
                        return;

                    //case 5:
                    //    if (txtScan.Text.Trim().Length <= 0)
                    //    {
                    //        fgDtls.Rows[e.RowIndex].Cells[NQualityID].Value = fgDtls.Rows[e.RowIndex].Cells[8].Value;
                    //    }
                    //    return;

                    //case 7:
                    //    if (fgDtls.Rows[e.RowIndex].Cells[NDesignID].Value != null)
                    //    {
                    //        fgDtls.Rows[e.RowIndex].Cells[NQualityID].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", fgDtls.Rows[e.RowIndex].Cells[NDesignID].Value)));
                    //    }
                    //    break;

                    case 30:
                        if (fgDtls.Rows[e.RowIndex].Cells[27].Value != null && Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[27].Value.ToString()) != 0 && fgDtls.Rows[e.RowIndex].Cells[5].Value != null && Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString()) != 0)
                        {
                            fgDtls.Rows[e.RowIndex].Cells[18].Value = DB.GetSnglValue(string.Format("Select IsNUll(Rate,0) As Rate From tbl_EmbprogramCardDtls Where DesignID=" + Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString()) + " and EmbProgramID=" + Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[27].Value.ToString()) + ""));
                        }
                        break;

                    default:
                        break;
                }
                if (txtScan.Text.Trim().Length <= 0)
                {
                    //fgDtls.Rows[e.RowIndex].Cells[NShadeID].Value = fgDtls.Rows[e.RowIndex].Cells[9].Value;
                }

                if (fgDtls.RowCount > 1)
                {
                    cboOrderType.Enabled = false;
                    cboParty.Enabled = false;
                    dtRefDate.Enabled = false;
                }
                else
                {
                    cboOrderType.Enabled = true;
                    cboParty.Enabled = true;
                    dtRefDate.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
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

                if (cboDepartment.SelectedValue == null)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department.");
                    cboDepartment.Focus();
                    return;
                }
                else
                {
                    if (this.dtRefDate.Text != "__/__/____")
                    {
                        #region StockAdjQuery
                        string strQry = string.Empty;
                        int ibitcol = 0;
                        string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", iIDentity, 0.0));
                        string strQry_ColName = "";
                        string[] arr = CommonCls.GetAdjColName(base.iIDentity, 0.0).Split(';');
                        strQry_ColName = arr[0].ToString();
                        strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5},{6},{7}) Where BalMeters > 0 Order by MyId ", new object[] { strQry_ColName, snglValue, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(this.dtRefDate.Text))), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, this.cboDepartment.SelectedValue });
                        ibitcol = Localization.ParseNativeInt(arr[1]);
                        #endregion

                        frmStockAdj frmStockAdj = new frmStockAdj();
                        frmStockAdj.MenuID = base.iIDentity;
                        frmStockAdj.Entity_IsfFtr = 0.0;
                        frmStockAdj.ref_fgDtls = this.fgDtls;
                        frmStockAdj.QueryString = strQry;
                        frmStockAdj.IsRefQuery = true;
                        frmStockAdj.ibitCol = ibitcol;
                        frmStockAdj.AsonDate = Conversions.ToDate(this.dtRefDate.Text);
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
                                    if ((fgDtls.Rows[i].Cells[12].Value != null) && (fgDtls.Rows[i].Cells[12].Value.ToString() != "0") && (fgDtls.Rows[i].Cells[12].Value.ToString() != ""))
                                    {
                                        double iPcs = Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[12].Value.ToString());

                                        if (fgDtls.Rows[i].Cells[44].Value != null)
                                        {
                                            if (Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[44].Value.ToString()) < iPcs)
                                            {
                                                iPcs = Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[44].Value.ToString());
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
                                                    if (m == 12)
                                                    {
                                                        fgDtls.Rows[k].Cells[m].Value = 1;
                                                    }
                                                    else if (m == 1)
                                                    {
                                                        fgDtls.Rows[k].Cells[m].Value = k;
                                                    }
                                                    else if (m != 14 && m != 13)
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
                                        fgDtls.Rows[i].Cells[12].Value = fgDtls.Rows[i].Cells[44].Value.ToString();
                                    }
                                }
                            }
                            fgDtls.Rows.RemoveAt(fgDtls.RowCount - 1);
                        }
                    }
                    else
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Ref Date");
                    }

                    this.fgDtls.Select();
                    setTempRowIndex();
                    setMyID_Stock();
                    ExecuterTempQry(-1);

                    for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                    {
                        if (fgDtls.Rows[i].Cells[27].Value != null && Localization.ParseNativeInt(fgDtls.Rows[i].Cells[27].Value.ToString()) != 0 && fgDtls.Rows[i].Cells[5].Value != null && Localization.ParseNativeInt(fgDtls.Rows[i].Cells[5].Value.ToString()) != 0)
                        {
                            fgDtls.Rows[i].Cells[18].Value = DB.GetSnglValue(string.Format("Select IsNUll(Rate,0) As Rate From tbl_EmbprogramDtls Where EmbDesignID=" + Localization.ParseNativeInt(fgDtls.Rows[i].Cells[5].Value.ToString()) + " and EmbProgramID=" + Localization.ParseNativeInt(fgDtls.Rows[i].Cells[27].Value.ToString()) + ""));
                            fgDtls.Rows[i].Cells[28].Value = Localization.ParseNativeInt(fgDtls.Rows[i].Cells[27].Value.ToString());
                        }
                    }
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
                    if (cboParty.SelectedValue != null && cboProgramNo.SelectedValue != null)
                    {
                        if (blnFormAction == Enum_Define.ActionType.New_Record || blnFormAction == Enum_Define.ActionType.Edit_Record)
                        {
                            if (Localization.ParseNativeInt(cboProgramNo.SelectedValue.ToString()) > 0)
                            {
                                //fgdtls_f.Rows.Dispose();
                                fgdtls_f.DataSource = DB.GetDT(string.Format("Select Distinct ProgramNo, EmbProgramID,Party,DesignName,TotStitches,Mtrs,Rate from fn_EmbprogramCardDtls() Where PartyID= " + cboParty.SelectedValue + ""), false);
                                dtOrderDate.Text = DB.GetSnglValue(String.Format("Select top 1 FabSODate from {0}({1},{2},{3},{4},{5}) Where FabSOID = {4}", Db_Detials.fn_FetchFabSalesOrderDtls, cboParty.SelectedValue, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboProgramNo.SelectedValue));
                                foreach (UltraGridBand Band in fgdtls_f.DisplayLayout.Bands)
                                {
                                    foreach (UltraGridColumn Column in Band.Columns)
                                    {
                                        using (IDataReader dr = DB.GetRS(String.Format("Select * From {0} Where GridID = {1} and SubGridID = 1 and ColIndex = {2}", "tbl_GridSettings", iIDentity, Column.Index)))
                                        {
                                            while (dr.Read())
                                            {
                                                Column.Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
                                                Column.Hidden = Localization.ParseBoolean(dr["IsHidden"].ToString()) == true ? false : true;
                                                Column.CellActivation = Activation.NoEdit;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // fgdtls_f.Rows.Dispose();
                                fgdtls_f.DataSource = DB.GetDT(string.Format("Select Distinct ProgramNo, EmbProgramID,Party,EmbDesignName,TotStitches,Mtrs,Rate from fn_EmbprogramCardDtls() Where PartyID= " + cboParty.SelectedValue + ""), false);
                                foreach (UltraGridBand Band in fgdtls_f.DisplayLayout.Bands)
                                {
                                    foreach (UltraGridColumn Column in Band.Columns)
                                    {
                                        using (IDataReader dr = DB.GetRS(string.Format("Select * From {0} Where GridID = {1} and SubGridID = 1 and ColIndex = {2}", Db_Detials.tbl_GridSettings, iIDentity, Column.Index)))
                                        {
                                            while (dr.Read())
                                            {
                                                Column.Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
                                                Column.Hidden = (Localization.ParseBoolean(dr["IsHidden"].ToString()) == true ? false : true);
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
                    {
                        cboTransport.SelectedValue = Localization.ParseNativeDouble(DB.GetSnglValue(String.Format("Select TransportID From {0} Where LedgerID = {1} ", " tbl_LedgerMaster", cboHaste.SelectedValue)));
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
                    fgDtls.Rows[e.RowIndex].Cells[16].Value = txtBaleNo.Text;
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

                    using (IDataReader reader = DB.GetRS(string.Format("Select * from {0}({1},{2},{3},{4},{5},{6},'{7}')", new object[] { "fn_EmbFetchFabricStock", DB.SQuoteNotUnicode(Localization.ToSqlDateString(dtRefDate.Text)), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboDepartment.SelectedValue, txtScan.Text.Trim() })))
                    {
                        while (reader.Read())
                        {
                            flag = true;
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[27].Value = reader["EmbProgramID"].ToString();
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[4].Value = reader["BarcodeNo"].ToString();
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[7].Value = Localization.ParseNativeInt(reader["DesignID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[8].Value = Localization.ParseNativeInt(reader["QualityID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[9].Value = Localization.ParseNativeInt(reader["ShadeID"].ToString());
                            //fgDtls.Rows[fgDtls.RowCount - 1].Cells[NDesignID].Value = Localization.ParseNativeInt(reader["DesignID"].ToString());
                            //fgDtls.Rows[fgDtls.RowCount - 1].Cells[NQualityID].Value = Localization.ParseNativeInt(reader["QualityID"].ToString());
                            //fgDtls.Rows[fgDtls.RowCount - 1].Cells[NShadeID].Value = Localization.ParseNativeInt(reader["ShadeID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[10].Value = Localization.ParseNativeInt(reader["GradeID"].ToString());

                            if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                            {
                                if (((this.FDC_ORD_WISE && this.FDC_ORD_COMP) && !this.flg_OrderConform))
                                {
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[4].Value = "";
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[7].Value = null;
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[8].Value = null;
                                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[9].Value = null;
                                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Warning, "", "Order For this BarcodeNo is not available");
                                    return;
                                }
                            }


                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[11].Value = Localization.ParseNativeInt(reader["UnitID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[12].Value = Localization.ParseNativeDecimal(reader["BalQty"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[13].Value = Localization.ParseNativeDecimal(reader["BalMeters"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[14].Value = "";
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[16].Value = "";
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[3].Value = Localization.ParseNativeDecimal(reader["BatchNo"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[19].Value = Localization.ParseNativeInt(reader["LedgerID"].ToString());
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[18].Value = "";
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[39].Value = "";
                            fgDtls.Rows[fgDtls.RowCount - 1].Cells[40].Value = reader["RefID"].ToString();
                        }
                    }
                    if (!flag)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "No Records Found");
                    }
                    else
                    {
                        fgDtls.CurrentCell = fgDtls[1, fgDtls.RowCount - 1];
                        DataGridViewEx ex2 = this.fgDtls;
                        EventHandles.CreateDefault_Rows(ex2, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                        EventHandles.CalculateFooter_Rows(fgDtls_footer, fgDtls_footer, fgDtls_footer.Grid_ID.ToString(), fgDtls_footer.Grid_UID);
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
            Panel1.Visible = true;
            if ((cboParty.SelectedValue != null))
            {
                if (blnFormAction == Enum_Define.ActionType.New_Record | blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    var _with1 = fgdtls_f;
                    fgdtls_f.DataSource = DB.GetDT(string.Format("Select Distinct ProgramNo, EmbProgramID,Party,DesignName,TotStitches,Mtrs,Rate from fn_EmbprogramCardDtls() Where PartyID= " + cboParty.SelectedValue + ""), false);
                    foreach (UltraGridBand band in fgdtls_f.DisplayLayout.Bands)
                    {
                        foreach (UltraGridColumn column in band.Columns)
                        {
                            using (IDataReader dr = DB.GetRS(string.Format("Select * From {0} Where GridID = {1} and SubGridID = 1 and ColIndex = {2}", "tbl_GridSettings", iIDentity, column.Index)))
                            {
                                while (dr.Read())
                                {
                                    column.Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
                                    column.Hidden = (Localization.ParseBoolean(dr["IsHidden"].ToString()) == true ? false : true);
                                    column.CellActivation = Activation.NoEdit;
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
            txtRefNo.Focus();
            fgdtls_f.Focus();
        }

        private void cboParty_Leave(object sender, EventArgs e)
        {
            try
            {
                if (blnFormAction == Enum_Define.ActionType.New_Record | blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if ((cboParty.SelectedValue != null))
                    {
                        if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                        {
                            try
                            {
                                string sqlQuery = string.Format("Select Distinct ProgramNo, EmbProgramID from fn_EmbprogramCardDtls() Where PartyID= " + cboParty.SelectedValue + "");
                                Combobox_Setup.Fill_Combo(this.cboProgramNo, sqlQuery, "ProgramNo", "EmbProgramID");
                                cboProgramNo.ColumnWidths = "100;0";
                                cboProgramNo.AutoComplete = true;
                                cboProgramNo.AutoDropdown = true;
                            }
                            catch { }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboSubOrder_LostFocus(object sender, System.EventArgs e)
        {
            try
            {
                if ((cboSubOrder.SelectedValue != null))
                {
                    dtSubOrderDate.Text = Localization.ToVBDateString(DB.GetSnglValue(string.Format("Select EntryDate From {0} Where EmbProgramID = {1} where IsDeleted=0", "tbl_EmbProgramMain", cboSubOrder.SelectedValue)));
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
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

        private void lblSubOrder_Click(object sender, EventArgs e)
        {

        }

        private void cboDepartment_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                EventHandles.CreateDefault_Rows(this.fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                EventHandles.CalculateFooter_Rows(fgDtls_footer, fgDtls_footer, fgDtls_footer.Grid_ID.ToString(), fgDtls_footer.Grid_UID);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboParty_SelectedIndexChanged(object sender, EventArgs e)
        {

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

                                if (MyID != "" && row.Cells[13].Value != null && row.Cells[13].Value.ToString() != "" && row.Cells[13].Value.ToString() != "0" && row.Cells[12].Value != null && row.Cells[12].Value.ToString() != "" && row.Cells[12].Value.ToString() != "0")
                                {
                                    string BatchNo = Conversions.ToString(row.Cells[4].Value);
                                    if ((BatchNo == null) || (Localization.ParseNativeInt(BatchNo) == 0))
                                    {
                                        BatchNo = "-";
                                    }
                                    strQry = strQry +DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtRefDate.Text,
                                Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                                Localization.ParseNativeInt(row.Cells[20].Value.ToString()),
                                (row.Cells[40].Value == null ? "0" : row.Cells[40].Value.ToString() == "" ? "0" : row.Cells[40].Value.ToString()),
                                row.Cells[41].Value == null ? "NULL" : row.Cells[41].Value.ToString().Trim() == "" ? "NULL" : row.Cells[41].Value.ToString(),
                                row.Cells[3].Value == null ? "NULL" : row.Cells[3].Value.ToString().Trim() == "" ? "NULL" : row.Cells[3].Value.ToString(),
                                row.Cells[4].Value == null ? "NULL" : row.Cells[4].Value.ToString().Trim() == "" ? "NULL" : row.Cells[4].Value.ToString(),
                                row.Cells[6].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[6].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                Localization.ParseNativeInt(row.Cells[10].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[11].Value.ToString()),
                                0, 0, 0, 
                                Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[14].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()), 
                                row.Cells[21].Value == null ? "NULL" : row.Cells[21].Value.ToString().Trim() == "" ? "NULL" : CommonLogic.SQuote(row.Cells[21].Value.ToString()),
                                row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                row.Cells[24].Value == null ? "NULL" : row.Cells[24].Value.ToString().Trim() == "" ? "NULL" : row.Cells[24].Value.ToString(),
                                row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                row.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[29].Value.ToString()),
                                row.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[30].Value.ToString()),
                                row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                row.Cells[32].Value == null || row.Cells[32].Value.ToString() == "" || row.Cells[32].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[32].Value.ToString()),
                                row.Cells[33].Value == null || row.Cells[33].Value.ToString() == "" || row.Cells[33].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[33].Value.ToString()),
                                row.Cells[34].Value == null || row.Cells[34].Value.ToString() == "" ? "-" : row.Cells[34].Value.ToString(),
                                row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" ? "-" : row.Cells[35].Value.ToString(),
                                row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" ? "-" : row.Cells[36].Value.ToString(),
                                row.Cells[37].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[37].Value.ToString()),
                                row.Cells[38].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[38].Value.ToString()),
                                txtUniqueID.Text, i, StatusID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                                    
                                    //DBSp.InsertIntoFabrIcStockLedger(MyID, Convert.ToString(i + 1), txtEntryNo.Text,
                                    //           Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                                    //           (row.Cells[40].Value == null ? "0" : row.Cells[40].Value.ToString() == "" ? "0" : row.Cells[40].Value.ToString()),
                                    //           row.Cells[4].Value == null ? "-" : row.Cells[4].Value.ToString().Trim() == "" ? "-" : row.Cells[4].Value.ToString().Trim() == "0" ? "-" : row.Cells[4].Value.ToString()
                                    //           , dtRefDate.Text, Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                    //           Localization.ParseNativeDouble(row.Cells[7].Value.ToString()), Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                    //           Localization.ParseNativeDouble(row.Cells[11].Value.ToString()), 0, 0, 0, Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()),
                                    //           Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[14].Value.ToString()), row.Cells[4].Value == null ? "-" : row.Cells[4].Value.ToString().Trim() == "" ? "-" : row.Cells[4].Value.ToString().Trim() == "0" ? "-" : row.Cells[4].Value.ToString(),
                                    //           Localization.ParseNativeInt(row.Cells[27].Value.ToString()).ToString(),
                                    //           Localization.ParseNativeInt(row.Cells[10].Value.ToString()),
                                    //           row.Cells[20].Value == null ? 0 : row.Cells[20].Value.ToString() == "" ? 0 : Localization.ParseNativeInt(row.Cells[20].Value.ToString()),
                                    //           txtUniqueID.Text, i, StatusID, row.Cells[23].Value == null ? 0 : row.Cells[23].Value.ToString() == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()), row.Cells[24].Value.ToString(),
                                    //           Localization.ParseNativeInt(row.Cells[27].Value.ToString()), Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                    //           row.Cells[41].Value == null ? "NULL" : row.Cells[41].Value.ToString().Trim() == "" ? "NULL" : row.Cells[41].Value.ToString(),
                                    //           (row.Cells[15].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[15].Value.ToString())),
                                    //           row.Cells[5].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[5].Value.ToString()),
                                    //           row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                    //           Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                                }
                            }
                        }
                        else
                        {
                            if ((fgDtls.CurrentCell.ColumnIndex == 12) || (fgDtls.CurrentCell.ColumnIndex == 13) || (fgDtls.CurrentCell.ColumnIndex == 14))
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

                                if (MyID != "" && row.Cells[13].Value != null && row.Cells[13].Value.ToString() != "" && row.Cells[13].Value.ToString() != "0" && row.Cells[12].Value != null && row.Cells[12].Value.ToString() != "" && row.Cells[12].Value.ToString() != "0")
                                {
                                    strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[43].Value.ToString()) + " and AddedBy=" + Db_Detials.UserID + ";");
                                    string BatchNo = Conversions.ToString(row.Cells[4].Value);
                                    if ((BatchNo == null) || (Localization.ParseNativeInt(BatchNo) == 0))
                                    {
                                        BatchNo = "-";
                                    }
                                    strQry = strQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                             "(#CodeID#)", (RowIndex + 1).ToString(), "(#ENTRYNO#)", dtRefDate.Text,
                                             Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                                             Localization.ParseNativeInt(row.Cells[20].Value.ToString()),
                                             (row.Cells[40].Value == null ? "0" : row.Cells[40].Value.ToString() == "" ? "0" : row.Cells[40].Value.ToString()),
                                             row.Cells[41].Value == null ? "NULL" : row.Cells[41].Value.ToString().Trim() == "" ? "NULL" : row.Cells[41].Value.ToString(),
                                             row.Cells[3].Value == null ? "NULL" : row.Cells[3].Value.ToString().Trim() == "" ? "NULL" : row.Cells[3].Value.ToString(),
                                             row.Cells[4].Value == null ? "NULL" : row.Cells[4].Value.ToString().Trim() == "" ? "NULL" : row.Cells[4].Value.ToString(),
                                             row.Cells[6].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[6].Value.ToString()),
                                             Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                             Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                             Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                             Localization.ParseNativeInt(row.Cells[10].Value.ToString()),
                                             Localization.ParseNativeDouble(row.Cells[11].Value.ToString()),
                                             0, 0, 0,
                                             Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()),
                                             Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()),
                                             Localization.ParseNativeDecimal(row.Cells[14].Value.ToString()),
                                             Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                                             row.Cells[21].Value == null ? "NULL" : row.Cells[21].Value.ToString().Trim() == "" ? "NULL" : CommonLogic.SQuote(row.Cells[21].Value.ToString()),
                                             row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                             row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                             row.Cells[24].Value == null ? "NULL" : row.Cells[24].Value.ToString().Trim() == "" ? "NULL" : row.Cells[24].Value.ToString(),
                                             row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                             row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                             row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                             row.Cells[29].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[29].Value.ToString()),
                                             row.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[30].Value.ToString()),
                                             row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                             row.Cells[32].Value == null || row.Cells[32].Value.ToString() == "" || row.Cells[32].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[32].Value.ToString()),
                                             row.Cells[33].Value == null || row.Cells[33].Value.ToString() == "" || row.Cells[33].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[33].Value.ToString()),
                                             row.Cells[34].Value == null || row.Cells[34].Value.ToString() == "" ? "-" : row.Cells[34].Value.ToString(),
                                             row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" ? "-" : row.Cells[35].Value.ToString(),
                                             row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" ? "-" : row.Cells[36].Value.ToString(),
                                             row.Cells[37].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[37].Value.ToString()),
                                             row.Cells[38].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[38].Value.ToString()),
                                             txtUniqueID.Text, Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[43].Value.ToString()), StatusID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                                    //strQry = strQry + DBSp.InsertIntoFabrIcStockLedger(MyID, Convert.ToString(RowIndex + 1), txtEntryNo.Text,
                                    //          Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                                    //          (row.Cells[40].Value == null ? "0" : row.Cells[40].Value.ToString() == "" ? "0" : row.Cells[40].Value.ToString()),
                                    //          row.Cells[4].Value == null ? "-" : row.Cells[4].Value.ToString().Trim() == "" ? "-" : row.Cells[4].Value.ToString().Trim() == "0" ? "-" : row.Cells[4].Value.ToString(),
                                    //          dtRefDate.Text, Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                    //          Localization.ParseNativeDouble(row.Cells[7].Value.ToString()), Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                    //          Localization.ParseNativeDouble(row.Cells[11].Value.ToString()), 0, 0, 0, Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()),
                                    //          Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[14].Value.ToString()), row.Cells[4].Value == null ? "-" : row.Cells[4].Value.ToString().Trim() == "" ? "-" : row.Cells[4].Value.ToString().Trim() == "0" ? "-" : row.Cells[4].Value.ToString(),
                                    //          Localization.ParseNativeInt(row.Cells[27].Value.ToString()).ToString(),
                                    //          Localization.ParseNativeInt(row.Cells[10].Value.ToString()), row.Cells[20].Value == null ? 0 : row.Cells[20].Value.ToString() == "" ? 0 : Localization.ParseNativeInt(row.Cells[20].Value.ToString()),
                                    //          txtUniqueID.Text, Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[43].Value.ToString()), StatusID,
                                    //          row.Cells[23].Value == null ? 0 : row.Cells[23].Value.ToString() == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()), row.Cells[24].Value.ToString(), Localization.ParseNativeInt(row.Cells[27].Value.ToString()), Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                    //          row.Cells[41].Value == null ? "NULL" : row.Cells[41].Value.ToString().Trim() == "" ? "NULL" : row.Cells[41].Value.ToString(),
                                    //          (row.Cells[15].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[15].Value.ToString())), 
                                    //          row.Cells[5].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[5].Value.ToString()),
                                    //          row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                    //          Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
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
                            if ((Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select COUNT(0) From fn_StockFabricLedger_tbl() Where RefId='" + fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[39].Value + "' and RefID<>'' and Transtype<>" + iIDentity + ""))) > 0))
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityWarning, "", "Reference Found In Another Module..Row Cannot Be Deleted");
                                return;
                            }
                            else
                            {
                                try
                                {
                                    string strQry = string.Format("Update tbl_StockFabricLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[43].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
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
                                    EventHandles.CalculateFooter_Rows(fgDtls_footer, fgDtls_footer, fgDtls_footer.Grid_ID.ToString(), fgDtls_footer.Grid_UID);
                                }
                                else
                                {
                                    EventHandles.CreateDefault_Rows(fgDtls, table2, table3, table4, true, false);
                                    EventHandles.CalculateFooter_Rows(fgDtls_footer, fgDtls_footer, fgDtls_footer.Grid_ID.ToString(), fgDtls_footer.Grid_UID);
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
                                string strQry = string.Format("Update tbl_StockFabricLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[43].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
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
                                EventHandles.CalculateFooter_Rows(fgDtls_footer, fgDtls_footer, fgDtls_footer.Grid_ID.ToString(), fgDtls_footer.Grid_UID);
                            }
                            else
                            {
                                EventHandles.CreateDefault_Rows(fgDtls, table2, table3, table4, true, false);
                                EventHandles.CalculateFooter_Rows(fgDtls_footer, fgDtls_footer, fgDtls_footer.Grid_ID.ToString(), fgDtls_footer.Grid_UID);
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

        private void setMyID_Stock()
        {
            iMaxMyID_Stock = Localization.ParseNativeInt(DB.GetSnglValue("Select MAX(MyId + 1) from tbl_StockFabricLedger Where IsDeleted=0"));

            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[42].Value = iMaxMyID_Stock;
            }
        }

        private void setTempRowIndex()
        {
            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[43].Value = i;
            }
        }

        private void frmEmbFabricOutward_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (strUniqueID != null)
            {
                string strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                strQry = strQry + string.Format("Update tbl_StockFabricLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                DB.ExecuteSQL(strQry);
                strQry = string.Format("Update tbl_StockFabricLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                DB.ExecuteSQL(strQry);
                strQry = "";
            }
        }

        private void cboOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                {
                    btnShowOrder.Enabled = true;
                }
                else { btnShowOrder.Enabled = false; }

            }
            catch { }
        }

        private void txtBaleNo_Leave(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    fgDtls.Rows[i].Cells[16].Value = txtBaleNo.Text;
            }
            catch { }
        }
    }
}

