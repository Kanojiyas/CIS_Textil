using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_DBLayer;
using CIS_Bussiness;
using CIS_CLibrary;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class frmEmbFabricIssue : frmTrnsIface
    {
        ArrayList OrgInGridArray = new ArrayList();
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;
        CIS_Textbox InwardID;
        public static bool FAB_MAINTAINWEIGHT;
        public string strUniqueID;
        private int RefMenuID;
        private static string RefVoucherID;
        private int iMaxMyID_Stock;
        private bool isRowDel = false;
        private int iTempDel_RowIndex;
        private bool CellReadOnly = false;

        /// <summary>
        /// dtCategories DataTable will have records for different categories
        /// </summary>
        DataTable dtProgramNo;

        /// <summary>
        /// dtItems DataTable will have records for different items
        /// </summary>
        DataTable dtPDesign;

        /// <summary>
        /// Will containd index of Category column from DataGridView.
        /// </summary>
        int indexPDesign = -1;

        /// <summary>
        /// Will containd index of Item column from DataGridView.
        /// </summary>
        int indexProgram = -1;
        ComboBox cmbProgram, cmbPdesign;

        private bool INC_PNO_STOCK;

        public frmEmbFabricIssue()
        {
            InwardID = new CIS_Textbox();
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
            OrgInGridArray = new ArrayList();
            indexProgram = fgDtls.Columns.IndexOf(fgDtls.Columns["ProgramID"]);
            indexPDesign = fgDtls.Columns.IndexOf(fgDtls.Columns["ProcessID"]);
            dtPDesign = new DataTable();
            dtProgramNo = new DataTable();

        }

        #region Event

        private void frmEmbFabricIssue_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref CboDepartment, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboBroker, Combobox_Setup.ComboType.Mst_Brokers, "");
                Combobox_Setup.FillCbo(ref cboProcessType, Combobox_Setup.ComboType.Mst_FabricProcessType, "");
                Combobox_Setup.FillCbo(ref cboProcesser, Combobox_Setup.ComboType.Mst_Dyer, "");
                Combobox_Setup.FillCbo(ref cboDeliveryAt, Combobox_Setup.ComboType.Mst_Dyer, "");
                Combobox_Setup.FillCbo(ref cboIssueType, Combobox_Setup.ComboType.Mst_IssueType, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");

                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);

                this.CboDepartment.SelectedValueChanged += new System.EventHandler(this.cboDeptFrom_SelectedValueChanged);
                FAB_MAINTAINWEIGHT = Localization.ParseBoolean(GlobalVariables.FAB_MAINTAINWEIGHT);

                RefMenuID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MenuID from tbl_VoucherTypeMaster Where GenMenuID=" + base.iIDentity + "")));
                GetRefModID();

                INC_PNO_STOCK = Localization.ParseBoolean(GlobalVariables.INC_PNO_STOCK);

                cboProcessType.Enabled = false;
                cboIssueType.Enabled = false;

                cboIssueType.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MiscID From fn_MiscMaster_tbl() Where MiscName='Fresh'")));
                cboProcessType.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MiscID From fn_MiscMaster_tbl() Where MiscName='Embroidery'")));

                this.cboProcessType.SelectedIndexChanged += new System.EventHandler(this.cboProcessType_SelectedIndexChanged);

                dtPDesign = DB.GetDT(string.Format("Select Distinct EmbDesignCardID,EmbDesignName,EmbProgramID,SubEmbProgramID From fn_EmbProgramComboFill()"), false);
                dtProgramNo = DB.GetDT(string.Format("Select Distinct EmbProgramID,ProgramNo From fn_EmbProgramComboFill()"), false);

                indexPDesign = 29;
                indexProgram = 2;

                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls.KeyDown += new KeyEventHandler(this.fgDtls_KeyDown);
                this.fgDtls.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.fgDtls_EditingControlShowing);
                this.fgDtls.RowsAdded += new DataGridViewRowsAddedEventHandler(this.fgDtls_RowsAdded);
                this.fgDtls.CellLeave += new DataGridViewCellEventHandler(this.fgDtls_CellLeave);

                ((DataGridViewComboBoxColumn)fgDtls.Columns[indexProgram]).DisplayMember = "ProgramNo";
                ((DataGridViewComboBoxColumn)fgDtls.Columns[indexProgram]).ValueMember = "EmbProgramID";
                ((DataGridViewComboBoxColumn)fgDtls.Columns[indexProgram]).DataSource = dtProgramNo;

                //We need to data bind items column when we load this form.
                ((DataGridViewComboBoxColumn)fgDtls.Columns[indexPDesign]).DisplayMember = "EmbDesignName";
                ((DataGridViewComboBoxColumn)fgDtls.Columns[indexPDesign]).ValueMember = "EmbDesignCardID";
                ((DataGridViewComboBoxColumn)fgDtls.Columns[indexPDesign]).DataSource = dtPDesign;
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
                DBValue.Return_DBValue(this, txtCode, "EmbFabIssueID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtRefNo, "RefNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtRefDate, "RefDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboProcessType, "ProcessTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboIssueType, "IssueTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboProcesser, "ProcesserID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, CboDepartment, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDeliveryAt, "DeliveryAtID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLrNo, "LrNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtLrDate, "LrDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtBatchNo, "BatchNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, InwardID, "FabricInwardID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtUniqueID, "UniqueID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEd1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);
                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "EmbFabIssueID", txtCode.Text, base.dt_HasDtls_Grd, this.iIDentity);

                int icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_StockFabricLedger_tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));
                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                    setTempRowIndex();
                    cboProcesser.Enabled = false;
                    cboProcessType.Enabled = false;
                    CboDepartment.Enabled = false;
                    cboIssueType.Enabled = false;

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
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityShieldBlue, "This Record Is Edited By Another User...", "Warning");
                        }
                    }
                    catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                }

                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    if (strUniqueID != null)
                    {
                        string strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and Addedby=" + Db_Detials.UserID + ";");
                        strQry = strQry + string.Format("Update  tbl_StockFabricLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and Addedby=" + Db_Detials.UserID + ";");
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
                        if (Localization.ParseNativeInt(DB.GetSnglValue(string.Format("SELECT COUNT(0) from fn_StockFabricLedger() WHERE TransType<>" + iIDentity + " and RefID =" + CommonLogic.SQuote(fgDtls.Rows[i].Cells[35].Value.ToString())))) > 0)
                        {
                            CellReadOnly = true;
                            fgDtls.Rows[i].ReadOnly = true;
                            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle;
                        }
                        else
                        {
                            fgDtls.Rows[i].ReadOnly = false;
                        }
                    }
                }
                catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }

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

                if (CellReadOnly)
                {
                    txtRefNo.Enabled = false;
                    dtRefDate.Enabled = false;
                }
                else
                {
                    dtRefDate.Enabled = true;
                    txtRefNo.Enabled = true;
                }
                AplySelectBtnEnbl();
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
                    string strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and Addedby=" + Db_Detials.UserID + ";");
                    strQry = strQry + string.Format("Update  tbl_StockFabricLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and Addedby=" + Db_Detials.UserID + ";");
                    DB.ExecuteSQL(strQry);
                    strQry = string.Format("Update tbl_StockFabricLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                    DB.ExecuteSQL(strQry);
                    strQry = "";
                }
                txtCode.Text = "";
                CIS_Textbox txtEntryNo = this.txtEntryNo;
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                this.txtEntryNo = txtEntryNo;
                this.txtRefNo.Text = CommonCls.AutoInc(this, "RefNo", "EmbFabIssueID", "");
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                int MaxID = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(EmbFabIssueID),0) From {0} where IsDeleted=0 and StoreID={1} and CompID={2} and BranchID={3} and YearID={4}", "tbl_EmbFabricIssueMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID))));

                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where IsDeleted=0 and  EmbFabIssueID = {1} and StoreID={2} and CompID={3} and BranchID={4} and YearID={5}", new object[] { "tbl_EmbFabricIssueMain", MaxID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                        dtRefDate.Text = Localization.ToVBDateString(reader["RefDate"].ToString());
                        cboProcessType.SelectedValue = Localization.ParseNativeDouble(reader["ProcessTypeID"].ToString());
                        cboIssueType.SelectedValue = Localization.ParseNativeDouble(reader["IssueTypeID"].ToString());
                        cboProcesser.SelectedValue = Localization.ParseNativeDouble(reader["ProcesserID"].ToString());
                        cboBroker.SelectedValue = Localization.ParseNativeDouble(reader["BrokerID"].ToString());
                        CboDepartment.SelectedValue = Localization.ParseNativeDouble(reader["DepartmentID"].ToString());
                        cboDeliveryAt.SelectedValue = Localization.ParseNativeDouble(reader["DeliveryAtID"].ToString());
                        cboTransport.SelectedValue = Localization.ParseNativeDouble(reader["TransportID"].ToString());
                        dtLrDate.Text = Localization.ToVBDateString(reader["LrDate"].ToString());
                    }
                }

                cboProcessType.Enabled = false;
                cboIssueType.Enabled = false;
                cboIssueType.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MiscID From fn_MiscMaster_tbl() Where MiscName='Fresh'")));
                cboProcessType.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MiscID From fn_MiscMaster_tbl() Where MiscName='Embroidery'")));

                dtEntryDate.Focus();
                AplySelectBtnEnbl();
                txtUniqueID.Text = CommonCls.GenUniqueID();
                strUniqueID = txtUniqueID.Text;
                cboProcesser.Enabled = true;
                CboDepartment.Enabled = true;
                dtRefDate.Enabled = true;
                txtRefNo.Enabled = true;
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
                (dtRefDate.TextFormat(false, true)),
                (cboProcessType.SelectedValue),
                (cboIssueType.SelectedValue),
                (cboProcesser.SelectedValue),
                (cboBroker.SelectedValue),
                (CboDepartment.SelectedValue),
                (cboDeliveryAt.SelectedValue),
                (cboTransport.SelectedValue),
                (txtLrNo.Text.ToString()),
                (dtLrDate.TextFormat(false, true)),
                (txtBatchNo.Text.ToString()),
                (txtDescription.Text.ToString()),
                (InwardID.Text==""?"null":InwardID.Text),
                txtUniqueID.Text,
                cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue,
                cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue,
                dtEd1.TextFormat(false,true), 
                txtET1.Text,
                txtET2.Text,
                txtET3.Text
                };
                int UnitID = 0;
                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", "(#CodeID#)", Localization.ParseNativeInt(base.iIDentity.ToString()));
                for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    if (Localization.ParseNativeDouble(row.Cells[11].Value.ToString()) > 0)
                    {
                        string LotNo;
                        if (txtBatchNo.Text != null && txtBatchNo.Text.Trim().Length > 0 && txtBatchNo.Text != "0")
                        {
                            LotNo = txtBatchNo.Text;
                        }
                        else
                        {
                            LotNo = "-";
                        }
                        strAdjQry += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtRefDate.Text,
                                Localization.ParseNativeDouble(cboProcesser.SelectedValue.ToString()),
                                Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                row.Cells[37].Value == null ? "NULL" : row.Cells[37].Value.ToString().Trim() == "" ? "NULL" : row.Cells[37].Value.ToString(),
                                LotNo,
                                row.Cells[3].Value == null ? "NULL" : row.Cells[3].Value.ToString().Trim() == "" ? "NULL" : row.Cells[3].Value.ToString(),
                                row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()),
                                0, 0, 0, Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()), "NULL",
                                row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                row.Cells[19].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[19].Value.ToString()),
                                row.Cells[20].Value == null ? "NULL" : row.Cells[20].Value.ToString().Trim() == "" ? "NULL" : row.Cells[20].Value.ToString(),
                                row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                row.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
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
                                "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                        strAdjQry += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtRefDate.Text,
                                Localization.ParseNativeDouble(CboDepartment.SelectedValue.ToString()),
                                Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                row.Cells[36].Value == null ? "0" : row.Cells[36].Value.ToString().Trim() == "" ? "0" : row.Cells[36].Value.ToString(),
                                row.Cells[37].Value == null ? "NULL" : row.Cells[37].Value.ToString().Trim() == "" ? "NULL" : row.Cells[37].Value.ToString(),
                                row.Cells[2].Value == null ? "0" : row.Cells[2].Value.ToString().Trim() == "" ? "0" : row.Cells[2].Value.ToString(),
                                row.Cells[3].Value == null ? "NULL" : row.Cells[3].Value.ToString().Trim() == "" ? "NULL" : row.Cells[3].Value.ToString(),
                                row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                0, 0, 0,
                                Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()), "NULL",
                                row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                row.Cells[19].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[19].Value.ToString()),
                                row.Cells[20].Value == null ? "NULL" : row.Cells[20].Value.ToString().Trim() == "" ? "NULL" : row.Cells[20].Value.ToString(),
                                row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                row.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
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
                                "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                    }
                    UnitID = Localization.ParseNativeInt(row.Cells[9].Value.ToString());
                    row = null;
                }
                //if ((cboTransport.SelectedValue != null) && (Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString())) > 0.0)
                //{
                //    strAdjQry = strAdjQry + DBSp.InsertIntoTrasportLedger("(#CodeID#)", "(#OTHERNO#)", dtRefDate.Text,
                //            Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()),
                //            Localization.ParseNativeDouble(cboDeptFrom.SelectedValue.ToString()), Localization.ParseNativeDouble(cboProcesser.SelectedValue.ToString()),
                //            txtLrNo.Text, dtLrDate.Text, null, Localization.ParseNativeDouble(UnitID.ToString()), Localization.ParseNativeInt(string.Format("{0:N0}", CommonCls.GetColSum(fgDtls, 10, -1, -1))),
                //            Localization.ParseNativeDecimal(string.Format("{0:N2}", CommonCls.GetColSum(fgDtls, 11, -1, -1))),
                //            Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                //}
                strAdjQry += "Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Addedby=" + Db_Detials.UserID + ";";
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                DBSp.Transcation_AddEdit(pArrayData, fgDtls, true, strAdjQry, "", txtEntryNo.Text, txtRefNo.Text, "RefNo");
                OrgInGridArray.Clear();
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
                string strTblName;
                if (!EventHandles.IsValidGridReq(fgDtls, base.dt_AryIsRequired))
                {
                    return true;
                }

                if (!EventHandles.IsRequiredInGrid(fgDtls, dt_AryIsRequired, false))
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

                if (!CommonCls.CheckDate(dtEntryDate.Text, true))
                    return true;

                if (txtRefNo.Text.Trim() == "" || txtRefNo.Text.Trim() == "-" || txtRefNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Challan No.");
                    txtRefNo.Focus();
                    return true;
                }

                if (txtRefNo.Text.Trim().Length > 0)
                {
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        strTblName = "tbl_EmbFabricIssueMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "RefNo", txtRefNo.Text, false, "", 0, string.Format("StoreID={0} and CompID = {1} and BranchID={2} and YearID = {3}", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID), "This Ref No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where RefNo= '{1}' And StoreID={2} and CompID = {3} and BranchID={4} and YearID = {5}", new object[] { "tbl_EmbFabricIssueMain", txtRefNo.Text, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }))))
                        {
                            txtRefNo.Focus();
                            return true;
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        strTblName = "tbl_EmbFabricIssueMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "RefNo", txtRefNo.Text, true, "EmbFabIssueID", Localization.ParseNativeLong(txtCode.Text.Trim()), string.Format("StoreID={0} and CompID = {1} and BranchID={2} and YearID = {3}", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID), "This Ref No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where RefNo= '{1}' And StoreID={2} and CompID = {3} and BranchID={4} and YearID = {5}", new object[] { "tbl_EmbFabricIssueMain", txtRefNo.Text, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }))))
                        {
                            txtRefNo.Focus();
                            return true;
                        }
                    }
                }

                if (txtBatchNo.Text.Trim().Length > 0)
                {
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        strTblName = "tbl_EmbFabricIssueMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "BatchNo", txtBatchNo.Text, false, "", 0, string.Format("StoreID={0} and CompID = {1} and BranchID={2} and YearID = {3} ", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID), "This Lot No is already used in Challan No : " + DB.GetSnglValue(string.Format("Select RefNo From {0} Where BatchNo= '{1}' And StoreID={2} and CompID = {3} and BranchID={4} and YearID = {5}", new object[] { "tbl_EmbFabricIssueMain", txtBatchNo.Text, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }))))
                        {
                            txtBatchNo.Focus();
                            return true;
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        strTblName = "tbl_EmbFabricIssueMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "BatchNo", txtBatchNo.Text, true, "EmbFabIssueID", Localization.ParseNativeLong(txtCode.Text.Trim()), string.Format("StoreID={0} and CompID = {1} and BranchID={2} and YearID = {3} ", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID), "This Lot No is already used in Challan No : " + DB.GetSnglValue(string.Format("Select RefNo From {0} Where BatchNo= '{1}' And StoreID={2} and CompID = {3} and BranchID={4} and YearID = {5}", new object[] { "tbl_EmbFabricIssueMain", txtBatchNo.Text, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }))))
                        {
                            txtBatchNo.Focus();
                            return true;
                        }
                    }
                }

                if (!Information.IsDate(dtRefDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Ref Date");
                    dtRefDate.Focus();
                    return true;
                }

                if (!CommonCls.CheckDate(dtRefDate.Text, true))
                    return true;

                if (!CommonCls.CheckDate(dtLrDate.Text, true))
                    return true;

                if (CboDepartment.SelectedValue == null || CboDepartment.Text.Trim().ToString() == "-" || CboDepartment.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department from");
                    CboDepartment.Focus();
                    return true;
                }

                if (cboProcesser.SelectedValue == null || cboProcesser.Text.Trim().ToString() == "-" || cboProcesser.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Processer");
                    cboProcesser.Focus();
                    return true;
                }

                if (cboProcessType.SelectedValue == null || cboProcessType.Text.Trim().ToString() == "-" || cboProcessType.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Process Type");
                    cboProcessType.Focus();
                    return true;
                }

                if (cboIssueType.SelectedValue == null || cboIssueType.Text.Trim().ToString() == "-" || cboIssueType.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Issue Type");
                    cboIssueType.Focus();
                    return true;
                }

                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    fgDtls.Rows[i].Cells[16].Value = CboDepartment.SelectedValue;
                }

                if (FAB_MAINTAINWEIGHT)
                {
                    for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                    {
                        if (fgDtls.Rows[i].Cells[11].Value == null || Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[11].Value.ToString()) == 0 || fgDtls.Rows[i].Cells[11].Value.ToString() == "")
                        {
                            fgDtls.CurrentCell = fgDtls[11, i];
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Weight");
                            fgDtls.Rows.Add();
                            return true;
                        }
                    }
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

        public void PrintRecord()
        {
            try
            {
                CIS_ReportTool.frmMultiPrint frmMultiPrint = new CIS_ReportTool.frmMultiPrint();
                CIS_ReportTool.frmMultiPrint.MenuID = base.iIDentity;
                CIS_ReportTool.frmMultiPrint.Id = Localization.ParseNativeInt(txtCode.Text);
                CIS_ReportTool.frmMultiPrint.VoucherTypeID = 0;
                CIS_ReportTool.frmMultiPrint.TblNm = "tbl_EmbFabricIssueMain";
                CIS_ReportTool.frmMultiPrint.IdStr = "EmbFabIssueID";
                CIS_ReportTool.frmMultiPrint.iStoreID = Db_Detials.StoreID;
                CIS_ReportTool.frmMultiPrint.iCompID = Db_Detials.CompID;
                CIS_ReportTool.frmMultiPrint.iBranchID = Db_Detials.BranchID;
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

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record)))
            {
                try
                {
                    bool isIndexAppld = false;
                    int iIndex = fgDtls.RowCount - 1;
                    for (int m = 0; m <= fgDtls.RowCount - 1; m++)
                    {
                        if (fgDtls.Rows[m].Cells[5].Value != null && fgDtls.Rows[m].Cells[5].Value.ToString() != "")
                        {
                            iIndex = m;
                            isIndexAppld = true;
                        }
                    }
                    if (!isIndexAppld)
                    {
                        iIndex = -1;
                    }

                    #region StockAdjQuery

                    string strQry = string.Empty;
                    int ibitcol = 0;
                    string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", iIDentity, 0.0));
                    string strQry_ColName = "";
                    string[] arr = CommonCls.GetAdjColName(base.iIDentity, 0.0).Split(';');
                    strQry_ColName = arr[0].ToString();
                    strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4},{5},{6})", new object[] { strQry_ColName, snglValue, CboDepartment.SelectedValue, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID });
                    ibitcol = Localization.ParseNativeInt(arr[1]);

                    #endregion

                    if (dtRefDate.Text != "__/__/____")
                    {
                        if (CboDepartment.SelectedValue != null && CboDepartment.SelectedValue.ToString() != "0" && CboDepartment.SelectedValue.ToString() != "-")
                        {
                            frmStockAdj frmStockAdj = new frmStockAdj();
                            frmStockAdj.MenuID = base.iIDentity;
                            frmStockAdj.Entity_IsfFtr = 0.0;
                            frmStockAdj.ref_fgDtls = fgDtls;
                            frmStockAdj.QueryString = strQry;
                            frmStockAdj.IsRefQuery = true;
                            frmStockAdj.ibitCol = ibitcol;
                            frmStockAdj.AsonDate = Conversions.ToDate(dtRefDate.Text);
                            frmStockAdj.LedgerID = Conversions.ToString(CboDepartment.SelectedValue);
                            frmStockAdj.UsedInGridArray = OrgInGridArray;
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
                                        if ((fgDtls.Rows[i].Cells[10].Value != null) && (fgDtls.Rows[i].Cells[10].Value.ToString() != "0") && (fgDtls.Rows[i].Cells[10].Value.ToString() != ""))
                                        {
                                            double iPcs = Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[10].Value.ToString());

                                            if (fgDtls.Rows[i].Cells[40].Value != null)
                                            {
                                                if (Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[40].Value.ToString()) < iPcs)
                                                {
                                                    iPcs = Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[40].Value.ToString());
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
                                                        if (m == 10)
                                                        {
                                                            fgDtls.Rows[k].Cells[m].Value = 1;
                                                        }
                                                        else if (m == 1)
                                                        {
                                                            fgDtls.Rows[k].Cells[m].Value = k;
                                                        }
                                                        else if (m != 11 && m != 12)
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
                                            fgDtls.Rows[i].Cells[10].Value = fgDtls.Rows[i].Cells[40].Value.ToString();
                                        }
                                    }
                                }
                                fgDtls.Rows.RemoveAt(fgDtls.RowCount - 1);

                                DataGridViewEx ex2 = fgDtls;
                                for (int j = 0; j <= ex2.RowCount - 1; j++)
                                {
                                    if (j == 0)
                                    {
                                        int MaxId = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(EmbFabIssueID),0) From {0}  Where IsDeleted=0 and StoreID={1} and CompID = {2} and BranchID={3} and YearID = {4}", "tbl_EmbFabricIssueMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID))));
                                        if (INC_PNO_STOCK)
                                        {
                                            if (((fgDtls.RowCount > 0) && (fgDtls.ColumnCount > 0)))
                                            {
                                                fgDtls.Rows[0].Cells[3].Value = "-";
                                                //fgDtls.Rows[0].Cells[3].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select {0}({1},{2})", new object[] { "dbo.fn_FetchPieceNo_Stock", Db_Detials.CompID, Db_Detials.YearID })), Db_Detials.PCS_NO_INCMT);
                                            }
                                            else
                                            {
                                                fgDtls.Rows[0].Cells[3].Value = "-";
                                            }
                                        }
                                        else
                                        {
                                            if (((fgDtls.RowCount > 0) & (fgDtls.ColumnCount > 0)))
                                            {
                                                fgDtls.Rows[0].Cells[3].Value = "-";
                                                //fgDtls.Rows[0].Cells[3].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select  {0}({1},{2},{3},{4})", "dbo.fn_FetchPieceNo", MaxId, base.iIDentity, Db_Detials.CompID, Db_Detials.YearID)), Db_Detials.PCS_NO_INCMT);
                                            }
                                            else
                                            {
                                                fgDtls.Rows[0].Cells[3].Value = "-";
                                            }
                                        }
                                    }
                                    else if (j > 0)
                                    {
                                        if (ex2.Rows[j - 1].Cells[3].Value.ToString() != "-")
                                        {
                                            ex2.Rows[j].Cells[3].Value = "-";
                                            // ex2.Rows[j].Cells[3].Value = CommonCls.AutoInc_Runtime(ex2.Rows[j - 1].Cells[3].Value.ToString(), Db_Detials.PCS_NO_INCMT);
                                        }
                                        else
                                        {
                                            ex2.Rows[j].Cells[3].Value = "-";
                                        }
                                    }
                                    //fgDtls.Rows[j].Cells[11].Value = Localization.ParseNativeDecimal(fgDtls.Rows[j].Cells[10].Value.ToString()) * Localization.ParseNativeDecimal(fgDtls.Rows[j].Cells[10].Value.ToString());
                                }
                                SendKeys.Send("{TAB}");
                                if (fgDtls.Rows.Count > 0)
                                {
                                    fgDtls.CurrentCell = fgDtls[10, 0];
                                }
                                fgDtls.Select();

                                if (fgDtls.RowCount == 0)
                                {
                                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                                }
                                frmStockAdj.Dispose();
                                frmStockAdj = null;
                            }
                            frmStockAdj.Dispose();
                            frmStockAdj = null;


                        }
                        else
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                            CboDepartment.Focus();
                        }
                    }
                    else
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Challan Date");
                        dtRefDate.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                }
                fgDtls.Select();

                setMyID_Stock();
                setTempRowIndex();
                ExecuterTempQry(-1);
                cboProcessType_SelectedIndexChanged(null, null);
            }
        }

        private void cboProcesser_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if ((cboProcesser.SelectedValue != null) && (Localization.ParseNativeDouble(cboProcesser.SelectedValue.ToString()) > 0.0))
                {
                    cboBroker.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select BrokerID From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboProcesser.SelectedValue)));
                    cboTransport.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select TransportId From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboProcesser.SelectedValue)));
                    cboDeliveryAt.SelectedValue = cboProcesser.SelectedValue;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
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
                            strQry = string.Format("Delete From tbl_StockFabricLedger Where Dr_Mtrs=0 and Dr_Qty=0 and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Addedby=" + Db_Detials.UserID + ";");
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

                                if (MyID != "" && row.Cells[10].Value != null && row.Cells[10].Value.ToString() != "" && row.Cells[10].Value.ToString() != "0" && row.Cells[11].Value != null && row.Cells[11].Value.ToString() != "" && row.Cells[11].Value.ToString() != "")
                                {
                                    decimal FabricDesign = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select WtPerMtr From tbl_FabricDesignMaster Where FabricDesignID=" + Localization.ParseNativeInt(row.Cells[5].Value.ToString()) + "")));
                                    row.Cells[14].Value = Localization.ParseNativeDecimal(row.Cells[11].Value.ToString());
                                    row.Cells[15].Value = Localization.ParseNativeDecimal(row.Cells[12].Value.ToString());

                                    if (row.Cells[15].Value == null || Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()) == 0)
                                    {
                                        if (FabricDesign != 0)
                                            row.Cells[15].Value = FabricDesign;
                                    }

                                    double Weight = 0;
                                    if (row.Cells[14].Value != null && Localization.ParseNativeDecimal(row.Cells[14].Value.ToString()) > 0 && Localization.ParseNativeDecimal(row.Cells[14].Value.ToString()) != 0)
                                    {
                                        if (row.Cells[15].Value == null || Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()) == 0)
                                        {
                                            Weight = (Localization.ParseNativeDouble(FabricDesign.ToString()) / Localization.ParseNativeDouble(row.Cells[14].Value.ToString())) * Localization.ParseNativeDouble(row.Cells[11].Value.ToString());
                                        }
                                        else
                                        {
                                            Weight = (Localization.ParseNativeDouble(row.Cells[15].Value.ToString()) / Localization.ParseNativeDouble(row.Cells[14].Value.ToString())) * Localization.ParseNativeDouble(row.Cells[11].Value.ToString());
                                        }
                                        if (Weight.ToString() != "NaN")
                                        {
                                            row.Cells[12].Value = Math.Round(Weight, 3);
                                        }
                                    }
                                    strQry += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                              MyID, (i + 1).ToString(), txtEntryNo.Text, dtRefDate.Text,
                                              Localization.ParseNativeDouble(CboDepartment.SelectedValue.ToString()),
                                              Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                              row.Cells[36].Value == null ? "0" : row.Cells[36].Value.ToString().Trim() == "" ? "0" : row.Cells[36].Value.ToString(),
                                              row.Cells[37].Value == null ? "NULL" : row.Cells[37].Value.ToString().Trim() == "" ? "NULL" : row.Cells[37].Value.ToString(),
                                              row.Cells[2].Value == null ? "0" : row.Cells[2].Value.ToString().Trim() == "" ? "0" : row.Cells[2].Value.ToString(),
                                              row.Cells[3].Value == null ? "NULL" : row.Cells[3].Value.ToString().Trim() == "" ? "NULL" : row.Cells[3].Value.ToString(),
                                              row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                              Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                              Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                              Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                              Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                              Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                              0, 0, 0,
                                              Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()),
                                              Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                              row.Cells[12].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()),
                                              Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()), "NULL",
                                              row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                              row.Cells[19].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[19].Value.ToString()),
                                              row.Cells[20].Value == null ? "NULL" : row.Cells[20].Value.ToString().Trim() == "" ? "NULL" : row.Cells[20].Value.ToString(),
                                              row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                              row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                              row.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
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
                                              txtUniqueID.Text, i, StatusID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                                }
                            }
                        }
                        else
                        {
                            if ((fgDtls.CurrentCell.ColumnIndex == 11) || (fgDtls.CurrentCell.ColumnIndex == 10) || (fgDtls.CurrentCell.ColumnIndex == 12))
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

                                if (MyID != "" && fgDtls.Rows[RowIndex].Cells[10].Value != null && fgDtls.Rows[RowIndex].Cells[10].Value.ToString() != "" && fgDtls.Rows[RowIndex].Cells[10].Value.ToString() != "0" && fgDtls.Rows[RowIndex].Cells[11].Value != null && fgDtls.Rows[RowIndex].Cells[11].Value.ToString() != "" && fgDtls.Rows[RowIndex].Cells[11].Value.ToString() != "")
                                {
                                    decimal FabricDesign = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select WtPerMtr From tbl_FabricDesignMaster Where FabricDesignID=" + Localization.ParseNativeInt(fgDtls.Rows[RowIndex].Cells[5].Value.ToString()) + "")));
                                    double Weight = 0;

                                    if (fgDtls.Rows[RowIndex].Cells[14].Value != null && Localization.ParseNativeDecimal(fgDtls.Rows[RowIndex].Cells[14].Value.ToString()) > 0 && Localization.ParseNativeDecimal(fgDtls.Rows[RowIndex].Cells[14].Value.ToString()) != 0)
                                    {
                                        if (fgDtls.Rows[RowIndex].Cells[15].Value == null || Localization.ParseNativeDecimal(fgDtls.Rows[RowIndex].Cells[15].Value.ToString()) == 0)
                                        {
                                            Weight = (Localization.ParseNativeDouble(FabricDesign.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[14].Value.ToString())) * Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[11].Value.ToString());
                                        }
                                        else
                                        {
                                            Weight = (Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[15].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[14].Value.ToString())) * Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[11].Value.ToString());
                                        }
                                        if (Weight.ToString() != "NaN")
                                        {
                                            fgDtls.Rows[RowIndex].Cells[12].Value = Math.Round(Weight, 3);
                                        }
                                    }
                                    strQry = string.Format("Delete From tbl_StockFabricLedger Where Dr_Mtrs=0 and Dr_Qty=0 and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and RowIndex=" + fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[39].Value.ToString() + " and Addedby=" + Db_Detials.UserID + ";");

                                    strQry += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                            MyID, (RowIndex + 1).ToString(), txtEntryNo.Text, dtRefDate.Text,
                                            Localization.ParseNativeDouble(CboDepartment.SelectedValue.ToString()),
                                            Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                            row.Cells[36].Value == null ? "0" : row.Cells[36].Value.ToString().Trim() == "" ? "0" : row.Cells[36].Value.ToString(),
                                            row.Cells[37].Value == null ? "NULL" : row.Cells[37].Value.ToString().Trim() == "" ? "NULL" : row.Cells[37].Value.ToString(),
                                            row.Cells[2].Value == null ? "0" : row.Cells[2].Value.ToString().Trim() == "" ? "0" : row.Cells[2].Value.ToString(),
                                            row.Cells[3].Value == null ? "NULL" : row.Cells[3].Value.ToString().Trim() == "" ? "NULL" : row.Cells[3].Value.ToString(),
                                            row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                            Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                            Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                            Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                            Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                            Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                            0, 0, 0,
                                            Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()),
                                            Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                            row.Cells[12].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()),
                                            Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()), "NULL",
                                            row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                            row.Cells[19].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[19].Value.ToString()),
                                            row.Cells[20].Value == null ? "NULL" : row.Cells[20].Value.ToString().Trim() == "" ? "NULL" : row.Cells[20].Value.ToString(),
                                            row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                            row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                            row.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
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
                                            txtUniqueID.Text, Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[39].Value.ToString()), StatusID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
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

        private void fgDtls_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (cmbPdesign != null)
            {
                cmbPdesign.DropDown -= new EventHandler(cmbPdesign_DropDown);
            }
            if (cmbProgram != null)
            {
                cmbProgram.SelectedValueChanged -= new EventHandler(cmbProgram_SelectedValueChanged);
            }
        }

        private void fgDtls_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //register a event to filter displaying value of items column.
            if (fgDtls.CurrentRow != null && fgDtls.CurrentCell.ColumnIndex == indexPDesign)
            {
                cmbPdesign = e.Control as ComboBox;
                if (cmbPdesign != null)
                {
                    cmbPdesign.DropDown += new EventHandler(cmbPdesign_DropDown);
                }
            }

            //Register SelectedValueChanged event and reset item comboBox to default if category changes
            if (fgDtls.CurrentRow != null && fgDtls.CurrentCell.ColumnIndex == indexProgram)
            {
                cmbProgram = e.Control as ComboBox;
                if (cmbProgram != null)
                {
                    cmbProgram.SelectedValueChanged += new EventHandler(cmbProgram_SelectedValueChanged);
                }
            }
        }

        void cmbProgram_SelectedValueChanged(object sender, EventArgs e)
        {
            //If category value changed then reset item to default.
            fgDtls.CurrentRow.Cells[indexPDesign].Value = 0;
        }

        void cmbPdesign_DropDown(object sender, EventArgs e)
        {
            int ProgramID = Convert.ToInt32(fgDtls.CurrentRow.Cells[indexProgram].Value);

            if (ProgramID > 0)
            {
                DataRow[] drTempRows = dtPDesign.Select(string.Format("EmbProgramID = {0}", ProgramID));

                if (drTempRows != null && drTempRows.Length > 0)
                {
                    DataTable dtTemp = drTempRows.CopyToDataTable();
                    DataRow drTemp = dtTemp.NewRow();
                    //drTemp["EmbDesignCardID"] = 0;
                    //drTemp["EmbDesignName"] = "--Select--";
                    //drTemp["EmbProgramID"] = 0;
                    //dtTemp.Rows.InsertAt(drTemp, 0);
                    cmbPdesign.DisplayMember = "EmbDesignName";
                    cmbPdesign.ValueMember = "EmbDesignCardID";
                    cmbPdesign.DataSource = dtTemp;
                }
            }
            else
            {
                DataTable dtTemp = dtPDesign.Clone();
                DataRow drTemp = dtTemp.NewRow();
                //drTemp["EmbDesignCardID"] = 0;
                //drTemp["EmbDesignName"] = "--Select--";
                //drTemp["EmbProgramID"] = 0;
                //dtTemp.Rows.InsertAt(drTemp, 0);
                cmbPdesign.DisplayMember = "EmbDesignName";
                cmbPdesign.ValueMember = "EmbDesignCardID";
                cmbPdesign.DataSource = dtTemp;
            }
        }

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record)))
            {
                try
                {
                    if ((e.ColumnIndex == 10) | (e.ColumnIndex == 11) | (e.ColumnIndex == 12))
                    {
                        ExecuterTempQry(e.RowIndex);
                    }
                    if (e.ColumnIndex == 24)
                    {
                        if (fgDtls.Rows[e.RowIndex].Cells[23].Value != null && Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[23].Value.ToString()) != 0 && fgDtls.Rows[e.RowIndex].Cells[24].Value != null && Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[24].Value.ToString()) != 0)
                        {
                            int icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(0) From tbl_EmbprogramDtls Where EmbDesignID=" + Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[24].Value.ToString()) + " and EmbProgramID=" + Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[23].Value.ToString()) + "")));
                            if (icount <= 0)
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Please Enter Valid Design For This Program", "Invalid Design");
                                fgDtls.Rows[e.RowIndex].Cells[24].Value = 0;
                                //return;
                            }
                        }
                    }

                    if (fgDtls.RowCount > 1)
                    {
                        CboDepartment.Enabled = false;
                        dtRefDate.Enabled = false;
                    }
                    else
                    {
                        CboDepartment.Enabled = true;
                        dtRefDate.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                }
            }
        }

        private void fgDtls_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    fgDtls.Rows[e.RowIndex].Cells[22].Value = Localization.ParseNativeInt(cboProcessType.SelectedValue.ToString());
                }
            }
            catch { }
        }

        private void txtScan_Validated(object sender, EventArgs e)
        {
            try
            {
                bool isblankrecord = false;
                if (blnFormAction == Enum_Define.ActionType.New_Record | blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if ((CboDepartment.SelectedValue != null) && (dtRefDate.Text.Trim() != "__/__/____"))
                    {
                        if ((txtScan.Text != null) && !string.IsNullOrEmpty(txtScan.Text) && (Localization.ParseNativeInt(CboDepartment.SelectedValue.ToString()) > 0))
                        {
                            var _with1 = fgDtls;
                            for (int i = 0; i <= _with1.RowCount - 1; i++)
                            {
                                var _with2 = _with1.Rows[i];
                                if ((_with2.Cells[3].Value != null))
                                {
                                    if (_with2.Cells[3].Value.ToString().Trim().ToUpper() == txtScan.Text.ToString().Trim().ToUpper())
                                    {
                                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "This Piece No is already Selected");
                                        txtScan.Text = "";
                                        txtScan.Focus();
                                        return;
                                    }
                                }
                            }

                            int irow = 0;
                            using (IDataReader dr = DB.GetRS(string.Format("Select * from {0}({1},{2},{3},{4},{5}) Where BatchNo='{6}'", "fn_FetchforEmbProcessEntry", CboDepartment.SelectedValue, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, txtScan.Text.Trim())))
                            {
                                while (dr.Read())
                                {
                                    isblankrecord = true;
                                    //_with1.Rows[_with1.RowCount - 1].Cells[23].Value = dr["ProgramID"].ToString();
                                    _with1.Rows[_with1.RowCount - 1].Cells[3].Value = dr["BarcodeNo"].ToString();
                                    _with1.Rows[_with1.RowCount - 1].Cells[5].Value = Localization.ParseNativeInt(dr["DesignID"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[6].Value = Localization.ParseNativeInt(dr["QualityID"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[7].Value = Localization.ParseNativeInt(dr["ShadeID"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[8].Value = Localization.ParseNativeInt(dr["GradeID"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[9].Value = Localization.ParseNativeInt(dr["UnitID"].ToString());
                                    //_with1.Rows[_with1.RowCount - 1].Cells[BaleNo].Value = "0";
                                    _with1.Rows[_with1.RowCount - 1].Cells[10].Value = Localization.ParseNativeDecimal(dr["BalQty"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[11].Value = Localization.ParseNativeDecimal(dr["BalMeters"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[12].Value = Localization.ParseNativeDecimal(dr["BalWeight"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[2].Value = dr["BatchNo"].ToString();
                                    _with1.Rows[_with1.RowCount - 1].Cells[35].Value = "0";
                                    _with1.Rows[_with1.RowCount - 1].Cells[36].Value = dr["ARefID"].ToString();
                                    _with1.Rows[_with1.RowCount - 1].Cells[38].Value = dr["MyID"].ToString();
                                    _with1.Rows[_with1.RowCount - 1].Cells[17].Value = dr["SubDepartmentID"].ToString();
                                    _with1.Rows[_with1.RowCount - 1].Cells[14].Value = 0;
                                    _with1.Rows[_with1.RowCount - 1].Cells[15].Value = 0;
                                    _with1.Rows[_with1.RowCount - 1].Cells[39].Value = irow;
                                    _with1.Rows[_with1.RowCount - 1].Cells[4].Value = 0;
                                    _with1.Rows[_with1.RowCount - 1].Cells[19].Value = dr["InwdLedID"].ToString();
                                    _with1.Rows[_with1.RowCount - 1].Cells[39].Value = dr["InwdTransID"].ToString();
                                    _with1.Rows[_with1.RowCount - 1].Cells[21].Value = dr["MainRefID"].ToString();
                                    _with1.Rows[_with1.RowCount - 1].Cells[21].Value = 0;
                                    _with1.Rows[_with1.RowCount - 1].Cells[22].Value = 0;
                                    _with1.Rows[_with1.RowCount - 1].Cells[13].Value = 0;
                                    _with1.Rows[_with1.RowCount - 1].Cells[40].Value = Localization.ParseNativeInt(dr["BalPcs"].ToString());
                                    irow++;
                                }
                            }
                            if (isblankrecord == false)
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "No Records Found");
                            }
                            else
                            {
                                _with1.CurrentCell = _with1[1, (fgDtls.RowCount - 1)];
                                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                            }
                            txtScan.Text = "";
                            txtScan.Focus();
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

        private void cboDeptFrom_SelectedValueChanged(object sender, EventArgs e)
        {
            EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
            EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
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

        private void setMyID_Stock()
        {
            iMaxMyID_Stock = Localization.ParseNativeInt(DB.GetSnglValue("Select MAX(MyId + 1) from tbl_StockFabricLedger Where IsDeleted=0"));

            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[38].Value = iMaxMyID_Stock;
            }
        }

        private void setTempRowIndex()
        {
            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[39].Value = i;
            }
        }

        private void frmEmbFabricIssue_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (strUniqueID != null)
            {
                string strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and Addedby=" + Db_Detials.UserID + ";");
                strQry = strQry + string.Format("Update  tbl_StockFabricLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and Addedby=" + Db_Detials.UserID + ";");
                DB.ExecuteSQL(strQry);
                strQry = string.Format("Update tbl_StockFabricLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                DB.ExecuteSQL(strQry);
                strQry = "";
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
                                    string strQry = string.Format("Update tbl_StockFabricLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[39].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and CUserID=" + Db_Detials.UserID + ";");
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
                                string strQry = string.Format("Update tbl_StockFabricLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[39].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and CUserID=" + Db_Detials.UserID + ";");
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

        private void cboProcessType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                {
                    fgDtls.Rows[i].Cells[22].Value = Localization.ParseNativeInt(cboProcessType.SelectedValue.ToString());
                }
            }
            catch { }
        }
    }
}
