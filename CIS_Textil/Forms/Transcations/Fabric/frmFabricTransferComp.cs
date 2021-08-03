using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_CLibrary;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmFabricTransferComp : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        public ArrayList OrgInGridArray;
        public static bool FAB_MAINTAINWEIGHT;
        private int RefMenuID;
        public string strUniqueID;
        private static string RefVoucherID;
        private int iMaxMyID_Stock;

        public frmFabricTransferComp()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
            OrgInGridArray = new ArrayList();
        }

        #region Form Event

        private void frmFabricTransferComp_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref CboCompanyFrom, Combobox_Setup.ComboType.Mst_Company, "");
                Combobox_Setup.FillCbo(ref CboCompanyTo, Combobox_Setup.ComboType.Mst_Company, "");
                Combobox_Setup.FillCbo(ref cboDepartTo, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboDepartFrm, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
                FAB_MAINTAINWEIGHT = Localization.ParseBoolean(GlobalVariables.FAB_MAINTAINWEIGHT);
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                txtEntryNo.Enabled = false;
                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    CboCompanyFrom.SelectedValue = Db_Detials.CompID;
                    CboCompanyFrom.Enabled = false;
                }
                RefMenuID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MenuID from tbl_VoucherTypeMaster Where GenMenuID=" + base.iIDentity + "")));
                GetRefModID();
                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls.RowsAdded += new DataGridViewRowsAddedEventHandler(this.fgDtls_RowsAdded);
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
                CIS_Textbox txtEntryNo = this.txtEntryNo;
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                this.txtEntryNo = txtEntryNo;
                this.txtRefNo.Text = CommonCls.AutoInc(this, "RefNo", "FabricTransCompID", "");
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                int MaxID = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(FabricTransCompID),0) From {0}  Where CompID = {1} and YearID = {2}", "tbl_FabricTransferCompMain", Db_Detials.CompID, Db_Detials.YearID))));

                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where FabricTransCompID = {1} and CompID={2} and YearID={3}", new object[] { "tbl_FabricTransferCompMain", MaxID, Db_Detials.CompID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                        dtRefDate.Text = Localization.ToVBDateString(reader["RefDate"].ToString());
                        CboCompanyFrom.SelectedValue = Db_Detials.CompID;
                        CboCompanyTo.SelectedValue = Localization.ParseNativeInt(reader["CompToID"].ToString());
                        cboDepartFrm.SelectedValue = Localization.ParseNativeInt(reader["DepartmentfmID"].ToString());
                        cboDepartTo.SelectedValue = Localization.ParseNativeInt(reader["DepartmenttoID"].ToString());
                        cboTransport.SelectedValue = Localization.ParseNativeInt(reader["TransportID"].ToString());
                    }
                }

                if (CboCompanyFrom.SelectedValue == null || Localization.ParseNativeInt(CboCompanyFrom.SelectedValue.ToString()) == 0)
                {
                    CboCompanyFrom.SelectedValue = Db_Detials.CompID;
                }
                dtEntryDate.Focus();
                txtUniqueID.Text = CommonCls.GenUniqueID();
                strUniqueID = txtUniqueID.Text;
                //fgDtls.Rows[0].Cells[17].Value = "-";
                AplySelectBtnEnbl();
                cboDepartFrm.Enabled = true;
                cboDepartTo.Enabled = true;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

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

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "FabricTransCompID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtRefNo, "RefNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtRefDate, "RefDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, CboCompanyFrom, "CompFromID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, CboCompanyTo, "CompToID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDepartFrm, "DepartmentfmID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDepartTo, "DepartmenttoID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDesc, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtUniqueID, "UniqueID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtED1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);
                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "FabricTransCompID", txtCode.Text, base.dt_HasDtls_Grd);

                int icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_StockFabricLedger_tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));
                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, dt_HasDtls_Grd, dt_AryCalcvalue, dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                    try
                    {
                        string strOldUniqueID = string.Empty;
                        strOldUniqueID = txtUniqueID.Text;
                        txtUniqueID.Text = CommonCls.GenUniqueID();
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
                    string strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                    DB.ExecuteSQL(strQry);
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
                        if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_StockFabricLedger() WHERE RefID<>'' AND RefID=" + CommonLogic.SQuote(fgDtls.Rows[i].Cells["RefID"].Value.ToString()))) > 1)
                        {
                            fgDtls.Rows[i].ReadOnly = true;
                            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle;
                            fgDtls.Rows[i].Cells[14].ReadOnly = false;
                            fgDtls.Rows[i].Cells[15].ReadOnly = false;
                        }
                        else
                            fgDtls.Rows[i].ReadOnly = false;
                    }
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                }
                AplySelectBtnEnbl();
                cboDepartFrm.Enabled = true;
                if (CboCompanyFrom.SelectedValue != null && CboCompanyFrom.SelectedValue.ToString() != "" && CboCompanyFrom.SelectedValue.ToString() != "-")
                {
                    CboCompanyFrom.Enabled = false;
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
                    ("(#ENTRYNO#)"),
                    (dtEntryDate.TextFormat(false, true)),
                    ("(#OTHERNO#)"),
                    (dtRefDate.TextFormat(false, true)),
                    (CboCompanyFrom.SelectedValue),
                    (CboCompanyTo.SelectedValue), 
                    (cboDepartFrm.SelectedValue),
                    (cboDepartTo.SelectedValue),
                    (cboTransport.SelectedValue),
                    (txtDesc.Text.ToString()),
                    txtUniqueID.Text,
                    (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                    (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                    (dtED1.TextFormat(false, true)),
                    (txtET1.Text.Trim()),
                    (txtET2.Text.Trim()),
                    (txtET3.Text.Trim())
                };
                int UnitID = 0;
                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", "(#CodeID#)", Localization.ParseNativeInt(Conversions.ToString(base.iIDentity)));
                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    //if (row.Cells[12].Value != null)
                    {
                        string LotNo;
                        if (row.Cells[2].Value != null && row.Cells[2].Value.ToString().Length > 0)
                        {
                            LotNo = row.Cells[2].Value.ToString();
                        }
                        else
                        {
                            LotNo = "-";
                        }

                        strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                            (i + 1).ToString(), "(#ENTRYNO#)", dtRefDate.Text, Localization.ParseNativeDouble(cboDepartTo.SelectedValue.ToString()),
                            fgDtls.Rows[i].Cells[23].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[23].Value.ToString()),
                            base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                            row.Cells[43].Value == null ? "NULL" : row.Cells[43].Value.ToString().Trim() == "" ? "NULL" : row.Cells[43].Value.ToString(),
                            LotNo, row.Cells[3].Value.ToString(),
                            row.Cells[10].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[10].Value.ToString()),
                            row.Cells[12].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[12].Value.ToString()),
                            row.Cells[11].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[11].Value.ToString()),
                            row.Cells[13].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[13].Value.ToString()),
                            row.Cells[14].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[14].Value.ToString()),
                            row.Cells[15].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[15].Value.ToString()),
                            Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                            Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()), 0, 0, 0,
                            row.Cells[19].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[19].Value.ToString()),
                            "NULL",
                            fgDtls.Rows[i].Cells[24].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[24].Value.ToString()),
                            fgDtls.Rows[i].Cells[25].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[25].Value.ToString()),
                            fgDtls.Rows[i].Cells[26].Value == null ? "NULL" : fgDtls.Rows[i].Cells[26].Value.ToString(),
                            fgDtls.Rows[i].Cells[27].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[27].Value.ToString()),
                            fgDtls.Rows[i].Cells[28].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[28].Value.ToString()),
                            fgDtls.Rows[i].Cells[30].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[30].Value.ToString()),
                            fgDtls.Rows[i].Cells[31].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[31].Value.ToString()),
                            fgDtls.Rows[i].Cells[32].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[32].Value.ToString()),
                            fgDtls.Rows[i].Cells[33].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[33].Value.ToString()),
                            fgDtls.Rows[i].Cells[34].Value == null || fgDtls.Rows[i].Cells[34].Value.ToString() == "" || fgDtls.Rows[i].Cells[34].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(fgDtls.Rows[i].Cells[34].Value.ToString()),
                            fgDtls.Rows[i].Cells[35].Value == null || fgDtls.Rows[i].Cells[35].Value.ToString() == "" || fgDtls.Rows[i].Cells[35].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(fgDtls.Rows[i].Cells[35].Value.ToString()),
                            fgDtls.Rows[i].Cells[36].Value == null || fgDtls.Rows[i].Cells[36].Value.ToString() == "" ? "-" : fgDtls.Rows[i].Cells[36].Value.ToString(),
                            fgDtls.Rows[i].Cells[37].Value == null || fgDtls.Rows[i].Cells[37].Value.ToString() == "" ? "-" : fgDtls.Rows[i].Cells[37].Value.ToString(),
                            fgDtls.Rows[i].Cells[38].Value == null || fgDtls.Rows[i].Cells[38].Value.ToString() == "" ? "-" : fgDtls.Rows[i].Cells[38].Value.ToString(),
                            fgDtls.Rows[i].Cells[39].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[39].Value.ToString()),
                            fgDtls.Rows[i].Cells[40].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[40].Value.ToString()),
                            "NULL", i, 1, Db_Detials.StoreID, Localization.ParseNativeInt(CboCompanyTo.SelectedValue.ToString()), Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date)


                        + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                            (i + 1).ToString(), "(#ENTRYNO#)", dtRefDate.Text, Localization.ParseNativeDouble(cboDepartFrm.SelectedValue.ToString()),
                            fgDtls.Rows[i].Cells[23].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[23].Value.ToString()),
                            row.Cells[42].Value == null ? "NULL" : row.Cells[42].Value.ToString().Trim() == "" ? "NULL" : row.Cells[42].Value.ToString(),
                            row.Cells[43].Value == null ? "NULL" : row.Cells[43].Value.ToString().Trim() == "" ? "NULL" : row.Cells[43].Value.ToString(),
                            LotNo, row.Cells[3].Value.ToString(),
                            row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                            row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                            row.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                            row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                            row.Cells[8].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                            row.Cells[9].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                            0, 0, 0,
                            Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                            Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()),
                            row.Cells[19].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[19].Value.ToString()),
                            "NULL",
                            fgDtls.Rows[i].Cells[24].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[24].Value.ToString()),
                            fgDtls.Rows[i].Cells[25].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[25].Value.ToString()),
                            fgDtls.Rows[i].Cells[26].Value == null ? "NULL" : fgDtls.Rows[i].Cells[26].Value.ToString(),
                            fgDtls.Rows[i].Cells[27].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[27].Value.ToString()),
                            fgDtls.Rows[i].Cells[28].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[28].Value.ToString()),
                            fgDtls.Rows[i].Cells[30].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[30].Value.ToString()),
                            fgDtls.Rows[i].Cells[31].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[31].Value.ToString()),
                            fgDtls.Rows[i].Cells[32].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[32].Value.ToString()),
                            fgDtls.Rows[i].Cells[33].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[33].Value.ToString()),
                            fgDtls.Rows[i].Cells[34].Value == null || fgDtls.Rows[i].Cells[34].Value.ToString() == "" || fgDtls.Rows[i].Cells[34].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(fgDtls.Rows[i].Cells[34].Value.ToString()),
                            fgDtls.Rows[i].Cells[35].Value == null || fgDtls.Rows[i].Cells[35].Value.ToString() == "" || fgDtls.Rows[i].Cells[35].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(fgDtls.Rows[i].Cells[35].Value.ToString()),
                            fgDtls.Rows[i].Cells[36].Value == null || fgDtls.Rows[i].Cells[36].Value.ToString() == "" ? "-" : fgDtls.Rows[i].Cells[36].Value.ToString(),
                            fgDtls.Rows[i].Cells[37].Value == null || fgDtls.Rows[i].Cells[37].Value.ToString() == "" ? "-" : fgDtls.Rows[i].Cells[37].Value.ToString(),
                            fgDtls.Rows[i].Cells[38].Value == null || fgDtls.Rows[i].Cells[38].Value.ToString() == "" ? "-" : fgDtls.Rows[i].Cells[38].Value.ToString(),
                            fgDtls.Rows[i].Cells[39].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[39].Value.ToString()),
                            fgDtls.Rows[i].Cells[40].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[40].Value.ToString()),
                            "NULL", i, 1, Db_Detials.StoreID, Localization.ParseNativeInt(CboCompanyFrom.SelectedValue.ToString()), 
                            Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                        UnitID = Localization.ParseNativeInt(row.Cells[15].Value.ToString());
                    }
                    row = null;
                }
                strAdjQry += "Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";";
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                if (cboTransport.SelectedValue != null && Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()) > 0.0)
                {
                    //strAdjQry = strAdjQry + DBSp.InsertIntoTrasportLedger("(#CodeID#)", ("(#OTHERNO#)"), dtRefDate.Text,
                    //            Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()),
                    //            Localization.ParseNativeDouble(cboDepartFrm.SelectedValue.ToString()), Localization.ParseNativeDouble(cboDepartTo.SelectedValue.ToString()),
                    //            null, null, null, Localization.ParseNativeDouble(UnitID.ToString()),
                    //            Localization.ParseNativeInt(CommonCls.GetColSum(this.fgDtls, 16, -1, -1).ToString()),
                    //            Localization.ParseNativeDecimal(CommonCls.GetColSum(this.fgDtls, 17, -1, -1).ToString()),
                    //            Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                }

                DBSp.Transcation_AddEdit(pArrayData, fgDtls, true, strAdjQry, "", txtEntryNo.Text, txtRefNo.Text, "RefNo");
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        #endregion

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
                    return true;
                }
                if (!Information.IsDate(dtEntryDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry Date");
                    dtEntryDate.Focus();
                    return true;
                }
                if (txtRefNo.Text.Trim().Length <= 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Ref No.");
                    txtRefNo.Focus();
                    return true;
                }
                if (txtRefNo.Text.Trim().Length > 0)
                {
                    string strtblName;
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        strtblName = "tbl_FabricTransferCompMain";
                        if (Navigate.CheckDuplicate(ref strtblName, "RefNo", txtRefNo.Text, false, "", 0L, string.Format("StoreID = {0} and CompID = {1} and BranchID = {2} and YearID = {3}", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID), "This Ref No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where RefNo = '{1}' and StoreID = {2} and CompID = {3} and BranchID = {4}  and YearID = {5} ", new object[] { "tbl_FabricTransferCompMain", txtRefNo.Text, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }))))
                        {
                            txtRefNo.Focus();

                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        strtblName = "tbl_FabricTransferCompMain";
                        if (Navigate.CheckDuplicate(ref strtblName, "RefNo", this.txtRefNo.Text, true, "FabricTransCompID", Localization.ParseNativeLong(txtCode.Text.Trim()), string.Format("StoreID = {0} and CompID = {1} and BranchID = {2} and YearID = {3}", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID), "This Ref No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where RefNo = '{1}' and StoreID = {2} and CompID = {3} and BranchID = {4}  and YearID = {5} ", new object[] { "tbl_FabricTransferCompMain", txtRefNo.Text, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }))))
                        {
                            txtRefNo.Focus();
                            return false;
                        }
                    }
                }
                if (!Information.IsDate(dtRefDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Ref Date");
                    dtRefDate.Focus();
                    return true;
                }
                if (CboCompanyFrom.SelectedValue == null || CboCompanyFrom.SelectedValue.ToString() == "-" || CboCompanyFrom.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Company From");
                    CboCompanyFrom.Focus();
                    return true;
                }
                if (CboCompanyTo.SelectedValue == null || CboCompanyTo.SelectedValue.ToString() == "-" || CboCompanyTo.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Company To");
                    CboCompanyTo.Focus();
                    return true;
                }
                if (cboDepartFrm.SelectedValue == null || cboDepartFrm.SelectedValue.ToString() == "-" || cboDepartFrm.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department From");
                    cboDepartFrm.Focus();
                    return true;
                }
                if (cboDepartTo.SelectedValue == null || cboDepartFrm.SelectedValue.ToString() == "-" || cboDepartTo.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department To");
                    cboDepartTo.Focus();
                    return true;
                }

                if (FAB_MAINTAINWEIGHT)
                {
                    for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                    {
                        if (fgDtls.Rows[i].Cells[18].Value == null || Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[18].Value.ToString()) == 0 || fgDtls.Rows[i].Cells[18].Value.ToString() == "")
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

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record)))
            {
                try
                {
                    if (CboCompanyFrom.Text.Trim().ToString() != "-" || CboCompanyFrom.SelectedValue != null)
                    {
                        if (cboDepartFrm.Text.Trim().ToString() != "-" || cboDepartFrm.SelectedValue != null)
                        {
                            if (dtRefDate.Text != "__/__/____")
                            {
                                #region StockAdjQuery
                                string strQry = string.Empty;
                                int ibitcol = 0;
                                string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", iIDentity, 0.0));
                                string strQry_ColName = "";
                                string[] arr = CommonCls.GetAdjColName(base.iIDentity, 0.0).Split(';');
                                strQry_ColName = arr[0].ToString();
                                ibitcol = Localization.ParseNativeInt(arr[1]);
                                strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}, {6}, {7})", new object[] { strQry_ColName, snglValue, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(this.dtRefDate.Text))), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboDepartFrm.SelectedValue });
                                #endregion

                                frmStockAdj frmStockAdj = new frmStockAdj();
                                frmStockAdj.MenuID = base.iIDentity;
                                frmStockAdj.Entity_IsfFtr = 0.0;
                                frmStockAdj.ref_fgDtls = this.fgDtls;
                                frmStockAdj.QueryString = strQry;
                                frmStockAdj.IsRefQuery = true;
                                frmStockAdj.ibitCol = ibitcol;
                                frmStockAdj.AsonDate = Conversions.ToDate(this.dtRefDate.Text);
                                frmStockAdj.LedgerID = cboDepartFrm.SelectedValue.ToString();
                                frmStockAdj.UsedInGridArray = OrgInGridArray;
                                if (frmStockAdj.ShowDialog() == DialogResult.Cancel)
                                {
                                    frmStockAdj.Dispose();
                                    return;
                                }
                                frmStockAdj.Dispose();
                                frmStockAdj = null;
                            }
                            fgDtls.Select();
                            setMyID_Stock();
                            setTempRowIndex();
                            ExecuterTempQry(-1);
                        }
                        else
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department From");
                            cboDepartFrm.Focus();
                        }
                    }
                    else
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Company From");
                        cboDepartFrm.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                }
            }
        }

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((e.ColumnIndex == 16) | (e.ColumnIndex == 17) | (e.ColumnIndex == 18))
                {
                    ExecuterTempQry(e.RowIndex);
                }
                if (((e.ColumnIndex == 11) && ((fgDtls.Rows[e.RowIndex].Cells[11].Value != null) && (fgDtls.Rows[e.RowIndex].Cells[11].Value.ToString() != "") && (fgDtls.Rows[e.RowIndex].Cells[11].Value.ToString().Length > 0))))
                {
                    fgDtls.Rows[e.RowIndex].Cells[12].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", fgDtls.Rows[e.RowIndex].Cells[11].Value)));
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
                    fgDtls.Rows[e.RowIndex].Cells[18].Value = "0";
                    fgDtls.Rows[e.RowIndex].Cells[2].Value = "-";
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
                bool isblankrecord = false;
                if (blnFormAction == Enum_Define.ActionType.New_Record | blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if ((cboDepartFrm.SelectedValue != null) && (dtRefDate.Text.Trim() != "__/__/____"))
                    {
                        if ((txtScan.Text != null) && !string.IsNullOrEmpty(txtScan.Text) && (Localization.ParseNativeInt(cboDepartFrm.SelectedValue.ToString()) > 0))
                        {
                            string strBatchDate = DB.GetSnglValue("Select Top 1 TransDate From fn_StockFabricLedger_Tbl() Where TransType in (215,352,372,403) and BarCodeNo='" + txtScan.Text.Trim() + "'" + " Order By TransDate Desc");
                            if (strBatchDate != "")
                            {
                                DateTime time4 = Conversions.ToDate(strBatchDate);
                                if (DateTime.Compare(Conversions.ToDate(dtRefDate.Text.Trim()), time4) < 0)
                                {
                                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Ref Date Should  be Greater Or Equal to Merging Date Or Opening Date");
                                    return;
                                }
                            }
                            var _with1 = fgDtls;
                            for (int i = 0; i <= _with1.RowCount - 1; i++)
                            {
                                var _with2 = _with1.Rows[i];
                                if ((_with2.Cells[3].Value != null))
                                {
                                    if (_with2.Cells[3].Value.ToString().Trim().ToUpper() == txtScan.Text.ToString().Trim().ToUpper())
                                    {
                                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "This BarCode No is already Selected");
                                        txtScan.Text = "";
                                        txtScan.Focus();
                                        return;
                                    }
                                }
                            }
                            int icount = 0;
                            using (IDataReader dr = DB.GetRS(string.Format("{0} {1},{2},{3},{4},{5},{6},'{7}' ", "sp_FetchFabricStock_Barcode", DB.SQuoteNotUnicode(Localization.ToSqlDateString(dtRefDate.Text)), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboDepartFrm.SelectedValue == null ? "0" : cboDepartFrm.SelectedValue, txtScan.Text.Trim())))
                            {
                                while (dr.Read())
                                {
                                    isblankrecord = true;
                                    _with1.Rows[_with1.RowCount - 1].Cells[2].Value = dr["BatchNo"].ToString();
                                    _with1.Rows[_with1.RowCount - 1].Cells[3].Value = dr["BarCodeNo"].ToString();
                                    _with1.Rows[_with1.RowCount - 1].Cells[5].Value = Localization.ParseNativeInt(dr["FabricDesignID"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[6].Value = Localization.ParseNativeInt(dr["FabricQualityID"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[7].Value = Localization.ParseNativeInt(dr["FabricShadeID"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[8].Value = Localization.ParseNativeInt(dr["GradeID"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[9].Value = Localization.ParseNativeInt(dr["UnitID"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[11].Value = Localization.ParseNativeInt(dr["FabricDesignID"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[12].Value = Localization.ParseNativeInt(dr["FabricQualityID"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[13].Value = Localization.ParseNativeInt(dr["FabricShadeID"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[15].Value = Localization.ParseNativeInt(dr["UnitID"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[16].Value = Localization.ParseNativeDecimal(dr["BalQty"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[17].Value = Localization.ParseNativeDecimal(dr["BalMeters"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[18].Value = Localization.ParseNativeDecimal(dr["BalWeight"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[20].Value = null;
                                    _with1.Rows[_with1.RowCount - 1].Cells[21].Value = icount;
                                    _with1.Rows[_with1.RowCount - 1].Cells[23].Value = (dr["StoreLocationID"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[25].Value = Localization.ParseNativeInt(dr["PartyID"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[26].Value = Localization.ParseNativeInt(dr["RefNo"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[28].Value = Localization.ParseNativeInt(dr["ProcessTypeID"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[41].Value = "0";
                                    _with1.Rows[_with1.RowCount - 1].Cells[42].Value = (dr["ARefID"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[43].Value = dr["MainRefID"] == null ? "NULL" : dr["MainRefID"].ToString();
                                    _with1.Rows[_with1.RowCount - 1].Cells[44].Value = (dr["MyID"].ToString());
                                    _with1.Rows[_with1.RowCount - 1].Cells[45].Value = 1;
                                    icount++;
                                }
                            }

                            if (isblankrecord == true)
                            {
                                setMyID_Stock();
                                setTempRowIndex();
                                ExecuterTempQry(-1);
                            }

                            if (isblankrecord == false)
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "No Records Found");
                            }
                            else
                            {
                                _with1.CurrentCell = _with1[1, (fgDtls.RowCount - 1)];
                                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                            }
                            txtScan.Text = "";
                            txtScan.Focus();
                        }
                    }
                    else
                    {
                        if (txtScan.Text.Trim().Length > 0)
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Plese Select From Department and Enter Ref Details");
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

                                double Weight = 0;
                                DataGridViewRow row = fgDtls.Rows[i];
                                decimal FabricDesign = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select WtPerMtr From tbl_FabricDesignMaster Where FabricDesignID=" + Localization.ParseNativeInt(fgDtls.Rows[i].Cells[5].Value.ToString()) + "")));
                                fgDtls.Rows[i].Cells[20].Value = Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[17].Value.ToString());
                                fgDtls.Rows[i].Cells[21].Value = Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[18].Value.ToString());

                                if (fgDtls.Rows[i].Cells[21].Value == null || Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[21].Value.ToString()) == 0)
                                {
                                    if (FabricDesign > 0)
                                        fgDtls.Rows[i].Cells[21].Value = FabricDesign;
                                }

                                if (fgDtls.Rows[i].Cells[20].Value != null && Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[20].Value.ToString()) > 0 && Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[20].Value.ToString()) != 0)
                                {
                                    if (fgDtls.Rows[i].Cells[21].Value == null || Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[21].Value.ToString()) == 0)
                                    {
                                        Weight = (Localization.ParseNativeDouble(FabricDesign.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[20].Value.ToString())) * Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[17].Value.ToString());
                                    }
                                    else
                                    {
                                        Weight = (Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[21].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[20].Value.ToString())) * Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[17].Value.ToString());
                                    }
                                    fgDtls.Rows[i].Cells[18].Value = Math.Round(Weight, 3);
                                }
                                string LotNo;
                                if (row.Cells[2].Value != null && row.Cells[2].Value.ToString().Length > 0)
                                {
                                    LotNo = row.Cells[2].Value.ToString();
                                }
                                else
                                {
                                    LotNo = "-";
                                }
                                if (MyID != "" && fgDtls.Rows[i].Cells[16].Value != null && fgDtls.Rows[i].Cells[17].Value.ToString() != "" && fgDtls.Rows[i].Cells[17].Value.ToString() != "0" && fgDtls.Rows[i].Cells[17].Value.ToString() != "")
                                {
                                    strQry += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), MyID,
                                           (i + 1).ToString(), txtEntryNo.Text, dtRefDate.Text, Localization.ParseNativeDouble(cboDepartFrm.SelectedValue.ToString()),
                                           row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                           row.Cells[42].Value == null ? "NULL" : row.Cells[42].Value.ToString().Trim() == "" ? "NULL" : row.Cells[42].Value.ToString(),
                                           row.Cells[43].Value == null ? "NULL" : row.Cells[43].Value.ToString().Trim() == "" ? "NULL" : row.Cells[43].Value.ToString(),
                                           LotNo, row.Cells[3].Value.ToString(),
                                           row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                           row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                           row.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                           row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                           row.Cells[8].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                           row.Cells[9].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                           0, 0, 0,
                                           Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                                           Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()),
                                           row.Cells[19].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[19].Value.ToString()),
                                           "NULL",
                                           row.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                           row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                           row.Cells[26].Value == null ? "NULL" : row.Cells[26].Value.ToString(),
                                           row.Cells[27].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[27].Value.ToString()),
                                           row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                           row.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[30].Value.ToString()),
                                           row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                           row.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[32].Value.ToString()),
                                           row.Cells[33].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[33].Value.ToString()),
                                           row.Cells[34].Value == null || row.Cells[34].Value.ToString() == "" || row.Cells[34].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[34].Value.ToString()),
                                           row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" || row.Cells[35].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[35].Value.ToString()),
                                           row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" ? "-" : row.Cells[36].Value.ToString(),
                                           row.Cells[37].Value == null || row.Cells[37].Value.ToString() == "" ? "-" : row.Cells[37].Value.ToString(),
                                           row.Cells[38].Value == null || row.Cells[38].Value.ToString() == "" ? "-" : row.Cells[38].Value.ToString(),
                                           row.Cells[39].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[39].Value.ToString()),
                                           row.Cells[40].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[40].Value.ToString()),
                                           txtUniqueID.Text, i, StatusID,
                                           Db_Detials.StoreID, Localization.ParseNativeInt(CboCompanyFrom.SelectedValue.ToString()), Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                                }
                            }
                        }
                        else
                        {
                            if ((fgDtls.CurrentCell.ColumnIndex == 16) || (fgDtls.CurrentCell.ColumnIndex == 17) || (fgDtls.CurrentCell.ColumnIndex == 18))
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

                                if (MyID != "" && row.Cells[16].Value != null && row.Cells[16].Value.ToString() != "" && row.Cells[17].Value != null && row.Cells[17].Value.ToString() != "0" && row.Cells[17].Value.ToString() != "")
                                {
                                    decimal FabricDesign = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select WtPerMtr From tbl_FabricDesignMaster Where FabricDesignID=" + Localization.ParseNativeInt(row.Cells[5].Value.ToString()) + "")));
                                    double Weight = 0;
                                    if (row.Cells[20].Value != null && Localization.ParseNativeDecimal(row.Cells[20].Value.ToString()) > 0 && Localization.ParseNativeDecimal(row.Cells[20].Value.ToString()) != 0)
                                    {
                                        if (row.Cells[21].Value == null || Localization.ParseNativeDecimal(row.Cells[21].Value.ToString()) == 0)
                                        {
                                            Weight = (Localization.ParseNativeDouble(FabricDesign.ToString()) / Localization.ParseNativeDouble(row.Cells[20].Value.ToString())) * Localization.ParseNativeDouble(row.Cells[17].Value.ToString());
                                        }
                                        else
                                        {
                                            Weight = (Localization.ParseNativeDouble(row.Cells[21].Value.ToString()) / Localization.ParseNativeDouble(row.Cells[20].Value.ToString())) * Localization.ParseNativeDouble(row.Cells[17].Value.ToString());
                                        }
                                        row.Cells[18].Value = Math.Round(Weight, 3);
                                    }

                                    string LotNo;
                                    if (row.Cells[2].Value != null && row.Cells[2].Value.ToString().Length > 0)
                                    {
                                        LotNo = row.Cells[2].Value.ToString();
                                    }
                                    else
                                    {
                                        LotNo = "-";
                                    }

                                    strQry = string.Format("Delete From tbl_StockFabricLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and RowIndex=" + fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[45].Value.ToString() + " and AddedBy=" + Db_Detials.UserID + ";");
                                    strQry += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), MyID,
                                          (RowIndex + 1).ToString(), txtEntryNo.Text, dtRefDate.Text, Localization.ParseNativeDouble(cboDepartFrm.SelectedValue.ToString()),
                                          row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                          row.Cells[42].Value == null ? "NULL" : row.Cells[42].Value.ToString().Trim() == "" ? "NULL" : row.Cells[42].Value.ToString(),
                                          row.Cells[43].Value == null ? "NULL" : row.Cells[43].Value.ToString().Trim() == "" ? "NULL" : row.Cells[43].Value.ToString(),
                                          LotNo, row.Cells[3].Value.ToString(),
                                          row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                          row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                          row.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                          row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                          row.Cells[8].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                          row.Cells[9].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                          0, 0, 0,
                                          Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                                          Localization.ParseNativeDecimal(row.Cells[18].Value.ToString()),
                                          row.Cells[19].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[19].Value.ToString()),
                                          "NULL",
                                          row.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                          row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                          row.Cells[26].Value == null ? "NULL" : row.Cells[26].Value.ToString(),
                                          row.Cells[27].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[27].Value.ToString()),
                                          row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                          row.Cells[30].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[30].Value.ToString()),
                                          row.Cells[31].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[31].Value.ToString()),
                                          row.Cells[32].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[32].Value.ToString()),
                                          row.Cells[33].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[33].Value.ToString()),
                                          row.Cells[34].Value == null || row.Cells[34].Value.ToString() == "" || row.Cells[34].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[34].Value.ToString()),
                                          row.Cells[35].Value == null || row.Cells[35].Value.ToString() == "" || row.Cells[35].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[35].Value.ToString()),
                                          row.Cells[36].Value == null || row.Cells[36].Value.ToString() == "" ? "-" : row.Cells[36].Value.ToString(),
                                          row.Cells[37].Value == null || row.Cells[37].Value.ToString() == "" ? "-" : row.Cells[37].Value.ToString(),
                                          row.Cells[38].Value == null || row.Cells[38].Value.ToString() == "" ? "-" : row.Cells[38].Value.ToString(),
                                          row.Cells[39].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[39].Value.ToString()),
                                          row.Cells[40].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[40].Value.ToString()),
                                          txtUniqueID.Text, Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[45].Value.ToString()), StatusID,
                                          Db_Detials.StoreID, Localization.ParseNativeInt(CboCompanyFrom.SelectedValue.ToString()), Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
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
                if (RefMenuID == 0)
                {
                    RefMenuID = iIDentity;
                }
                string sMappingID = DB.GetSnglValue(string.Format("Select MappingIDs from tbl_VoucherTypeMaster Where GenMenuID=" + iIDentity + ""));
                string[] arr = sMappingID.Split(',');
                for (int i = 0; i <= arr.Length - 1; i++)
                {
                    RefVoucherID += DB.GetSnglValue(string.Format("Select SeriesID From fn_MenuMaster_tbl() Where MenuID=" + arr[i] + "")) + ",";
                }
                RefVoucherID = RefVoucherID.Remove(RefVoucherID.Length - 1);
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
            #endregion
        }

        private void setMyID_Stock()
        {
            iMaxMyID_Stock = Localization.ParseNativeInt(DB.GetSnglValue("Select ISNULL(MAX(MyID), 0) + 1 from tbl_StockFabricLedger Where IsDeleted=0"));

            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[44].Value = iMaxMyID_Stock;
            }
        }

        private void setTempRowIndex()
        {
            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[45].Value = i;
            }
        }

        private void frmFabricTransferComp_FormClosed(object sender, FormClosedEventArgs e)
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
                            if ((Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select COUNT(0) From fn_StockFabricLedger_tbl() Where RefId='" + fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[42].Value + "' and RefID<>'' and Transtype<>" + iIDentity + ""))) > 0))
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityWarning, "", "Reference Found In Another Module..Row Cannot Be Deleted");
                                return;
                            }
                            else
                            {
                                try
                                {
                                    string strQry = string.Format("Update tbl_StockFabricLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[47].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                                    if (strQry.Length > 0)
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
                                string strQry = string.Format("Update tbl_StockFabricLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[47].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
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

    }
}
