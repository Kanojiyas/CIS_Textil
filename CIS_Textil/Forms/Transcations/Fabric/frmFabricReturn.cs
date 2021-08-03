using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_DBLayer;
using CIS_Bussiness;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class frmFabricReturn : frmTrnsIface
    {
        private int RefMenuID;
        private static string RefVoucherID;
        public string strUniqueID;
        private string SRateCalcType = string.Empty;

        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        ArrayList OrgInGridArray = new ArrayList();

        public frmFabricReturn()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
        }

        #region Event

        private void frmFabricReturn_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboProcessType, Combobox_Setup.ComboType.Mst_FabricProcessType, "");
                Combobox_Setup.FillCbo(ref cboDeptTo, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboProcesser, Combobox_Setup.ComboType.Mst_Dyer, "");
                Combobox_Setup.FillCbo(ref cboBroker, Combobox_Setup.ComboType.Mst_Brokers, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                this.txtEntryNo.Enabled = false;
                RefMenuID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MenuID from tbl_VoucherTypeMaster Where GenMenuID=" + base.iIDentity + "")));
                GetRefModID();

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
                DBValue.Return_DBValue(this, txtCode, "FabricReturnID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtRefNo, "RefNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtRefDate, "RefDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboProcessType, "ProcessTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDeptTo, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboProcesser, "ProcesserID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLrNo, "LrNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtLrDate, "LrDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtED1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);
                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "FabricReturnID", Conversions.ToString(Localization.ParseNativeDouble(txtCode.Text)), base.dt_HasDtls_Grd);

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                }

                AplySelectBtnEnbl();

                System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                dgvCellStyle.BackColor = System.Drawing.Color.LightGray;
                dgvCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
                dgvCellStyle.SelectionBackColor = System.Drawing.Color.Purple;
                dgvCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
                try
                {
                    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    {
                        if (Localization.ParseNativeInt(DB.GetSnglValue(string.Format("SELECT COUNT(0) from fn_StockFabricLedger_Tbl() WHERE TransType<>" + iIDentity + " and RefID =" + CommonLogic.SQuote(fgDtls.Rows[i].Cells[36].Value.ToString())))) > 0)
                        {
                            fgDtls.Rows[i].ReadOnly = true;
                            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle;
                        }
                        else
                            fgDtls.Rows[i].ReadOnly = false;
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
                txtCode.Text = "";
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                dtEntryDate.Focus();
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                dtEntryDate.Text = (Conversions.ToString(DateAndTime.Now.Date));
                int MaxID = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format("Select Isnull(Max(FabricReturnID),0) From {0}  Where StoreID = {1} and CompID = {2} and BranchID = {3} and YearID = {4}", "tbl_FabricReturnMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID))));

                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where FabricReturnID = {1} and StoreID={2} and CompID={3} and BranchID={4} and YearID={5}", new object[] { "tbl_FabricReturnMain", MaxID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = (Localization.ToVBDateString(reader["EntryDate"].ToString()));
                        dtRefDate.Text = (Localization.ToVBDateString(reader["RefDate"].ToString()));
                        cboProcessType.SelectedValue = Localization.ParseNativeDouble(reader["ProcessTypeID"].ToString());
                        cboProcesser.SelectedValue = Localization.ParseNativeDouble(reader["ProcesserID"].ToString());
                        cboBroker.SelectedValue = Localization.ParseNativeDouble(reader["BrokerID"].ToString());
                        cboDeptTo.SelectedValue = Localization.ParseNativeDouble(reader["DepartmentId"].ToString());
                    }
                }
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
                    (dtRefDate.TextFormat(false, true)),
                    (cboProcessType.SelectedValue),
                    (cboDeptTo.SelectedValue),
                    (cboProcesser.SelectedValue),
                    (cboBroker.SelectedValue),
                    (cboTransport.SelectedValue),
                    (txtLrNo.Text.ToString()),
                    (dtLrDate.TextFormat(false, true)),
                    (txtDescription.Text.ToString()),
                    (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                    (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                    (dtED1.TextFormat(false, true)),
                    (txtET1.Text.Trim()),
                    (txtET2.Text.Trim()),
                    (txtET3.Text.Trim())
                };

                int UnitID = 0;
                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", "(#CodeID#)", Localization.ParseNativeInt(iIDentity.ToString()));
                for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    string BatchNo;
                    if (row.Cells[2].Value != null && row.Cells[2].Value.ToString().Length > 0)
                    {
                        BatchNo = row.Cells[2].Value.ToString();
                    }
                    else
                    {
                        BatchNo = "-";
                    }

                    if(row.Cells[11].Value != null)
                    {
                        if (Localization.ParseNativeDouble(row.Cells[11].Value.ToString()) > 0)
                        {
                            strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                                    (i + 1).ToString(), "(#ENTRYNO#)", dtRefDate.Text, Localization.ParseNativeDouble(cboDeptTo.SelectedValue.ToString()),
                                    row.Cells[17].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                    base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                    row.Cells[37].Value == null ? "NULL" : row.Cells[37].Value.ToString().Trim() == "" ? "NULL" : row.Cells[37].Value.ToString(),
                                    BatchNo, row.Cells[3].Value.ToString(),
                                    row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                    row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                    row.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                    row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                    row.Cells[8].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                    row.Cells[9].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                    Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                    Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()), 0, 0, 0,
                                    row.Cells[13].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()),
                                    "NULL",
                                    row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                    row.Cells[19].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[19].Value.ToString()),
                                    row.Cells[20].Value == null ? "NULL" : row.Cells[20].Value.ToString(),
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
                                    "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date)


                             + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                                    (i + 1).ToString(), "(#ENTRYNO#)", dtRefDate.Text, Localization.ParseNativeDouble(cboProcesser.SelectedValue.ToString()),
                                    row.Cells[17].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                    row.Cells[36].Value == null ? "NULL" : row.Cells[36].Value.ToString().Trim() == "" ? "NULL" : row.Cells[36].Value.ToString(),
                                    row.Cells[37].Value == null ? "NULL" : row.Cells[37].Value.ToString().Trim() == "" ? "NULL" : row.Cells[37].Value.ToString(),
                                    BatchNo, row.Cells[3].Value.ToString(),
                                    row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                    row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                    row.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                    row.Cells[7].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                    row.Cells[8].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                    row.Cells[9].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[9].Value.ToString()),
                                    0, 0, 0,
                                    Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()),
                                    Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                    Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()),
                                    row.Cells[13].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()),
                                    "NULL",
                                    row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                    row.Cells[19].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[19].Value.ToString()),
                                    row.Cells[20].Value == null ? "NULL" : row.Cells[20].Value.ToString(),
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
                            UnitID = Localization.ParseNativeInt(row.Cells[9].Value.ToString());
                        }
                    }
                    row = null;
                }
                if (cboTransport.SelectedValue != null && Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()) > 0.0)
                {
                    //strAdjQry = strAdjQry + DBSp.InsertIntoTrasportLedger("(#CodeID#)", "(#OTHERNO#)", dtRefDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()), Localization.ParseNativeDouble(cboProcesser.SelectedValue.ToString()), Localization.ParseNativeDouble(cboDeptTo.SelectedValue.ToString()), txtLrNo.Text, dtLrDate.Text, null, Localization.ParseNativeDouble(UnitID.ToString()), Localization.ParseNativeInt(string.Format("{0:N0}", CommonCls.GetColSum(fgDtls, 9, -1, -1))), Localization.ParseNativeDecimal(string.Format("{0:N2}", CommonCls.GetColSum(fgDtls, 10, -1, -1))), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                }
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                DBSp.Transcation_AddEdit(pArrayData, fgDtls, true, strAdjQry, "", txtEntryNo.Text, txtRefNo.Text, "RefNo");
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
                    return true;
                }

                if (!Information.IsDate(dtEntryDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry Date");
                    dtEntryDate.Focus();
                    return true;
                }

                if (txtRefNo.Text.Trim() == "" || txtRefNo.Text.Trim() == "-" || txtRefNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Ref No.");
                    txtRefNo.Focus();
                    return true;
                }

                if (!Information.IsDate(dtRefDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Ref Date");
                    dtRefDate.Focus();
                    return true;
                }

                if (cboDeptTo.SelectedValue == null || cboDeptTo.Text.Trim().ToString() == "-" || cboDeptTo.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department from");
                    cboDeptTo.Focus();
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

                if ((txtRefNo.Text.Trim().Length) > 0)
                {
                    string strTblName;
                    if (base.blnFormAction == 0)
                    {
                        strTblName = "tbl_FabricReturnMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "RefNo", txtRefNo.Text, false, "", 0, " ProcesserID = " + cboProcesser.SelectedValue + " AND StoreID =" + Db_Detials.StoreID + " AND CompID =" + Db_Detials.CompID + " AND BranchID =" + Db_Detials.BranchID + " And YearID = " + Db_Detials.YearID + "", "This Processor already used this Ref No in Entry No : " + DB.GetSnglValue(string.Format("Select Entryno from {0} where ProcesserID = {1} and RefNo = '{2}' ", "tbl_FabricReturnMain", cboProcesser.SelectedValue, txtRefNo.Text.ToString()))))
                        {
                            txtRefNo.Focus();
                            return true;
                        }
                    }
                    else if (Localization.ParseNativeInt(base.blnFormAction.ToString()) == 1)
                    {
                        strTblName = "tbl_FabricReturnMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "RefNo", txtRefNo.Text, true, "FabricReturnID", Localization.ParseNativeLong(txtCode.Text.Trim()), " ProcesserID = " + cboProcesser.SelectedValue + " AND StoreID =" + Db_Detials.StoreID + " AND CompID =" + Db_Detials.CompID + " AND BranchID =" + Db_Detials.BranchID + " And YearID =" + Db_Detials.YearID + "", "This Processor already used this Ref No in Entry No : " + DB.GetSnglValue(string.Format("Select Entryno from {0} where ProcesserID = {1} and RefNo = '{2}' ", "tbl_FabricReturnMain", cboProcesser.SelectedValue, txtRefNo.Text.ToString()))))
                        {
                            txtRefNo.Focus();
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
            if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
            {
                btnSelect.Enabled = true;
            }
            else
            {
                btnSelect.Enabled = false;
            }
        }

        public void ShowStock()
        {
            btnSelect_Click(null, null);
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboProcesser.Text.Trim().ToString() != "-" && cboProcesser.Text.Trim().ToString() != "-" && cboProcesser.SelectedValue != null)
                {
                    #region StockAdjQuery
                    string strQry = string.Empty;
                    int ibitcol = 0;
                    string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", iIDentity, 0.0));
                    string strQry_ColName = "";
                    string[] arr = CommonCls.GetAdjColName(base.iIDentity, 0.0).Split(';');
                    strQry_ColName = arr[0].ToString();
                    ibitcol = Localization.ParseNativeInt(arr[1]);
                    strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}, {6}) Where BatchNo <> '-' ", new object[] { strQry_ColName, snglValue, cboProcesser.SelectedValue, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID });
                    #endregion

                    frmStockAdj frmStockAdj = new frmStockAdj();
                    frmStockAdj.MenuID = base.iIDentity;
                    frmStockAdj.Entity_IsfFtr = 0.0;
                    frmStockAdj.ref_fgDtls = fgDtls;
                    frmStockAdj.QueryString = strQry;
                    frmStockAdj.IsRefQuery = true;
                    frmStockAdj.ibitCol = ibitcol;
                    frmStockAdj.LedgerID = cboProcesser.SelectedValue.ToString();
                    frmStockAdj.UsedInGridArray = OrgInGridArray;
                    if (frmStockAdj.ShowDialog() == DialogResult.Cancel)
                    {
                        frmStockAdj.Dispose();
                        fgDtls.CurrentCell = fgDtls[3, 0];
                    }
                    else
                    {
                        frmStockAdj.Dispose();
                        frmStockAdj = null;
                        fgDtls.Select();
                    }
                }
                else
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Processor.");
                    cboProcesser.Focus();
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboProcesser_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                if ((this.cboProcesser.SelectedValue != null) && (Conversion.Val(RuntimeHelpers.GetObjectValue(cboProcesser.SelectedValue)) > 0.0))
                {
                    cboBroker.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select BrokerID From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboProcesser.SelectedValue)));
                    cboTransport.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select TransportId From {0} Where LedgerID = {1}", "tbl_LedgerMaster", cboProcesser.SelectedValue)));
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
                if (RefMenuID == 0)
                {
                    RefMenuID = iIDentity;
                }
                string sMappingID = DB.GetSnglValue(string.Format("Select MappingIDs from tbl_VoucherTypeMaster Where GenMenuID=" + iIDentity + ""));
                string[] arr = sMappingID.Split(',');
                for (int i = 0; i <= arr.Length - 1; i++)
                {
                    RefVoucherID += DB.GetSnglValue(string.Format("Select VoucherTypeID From fn_MenuMaster_tbl() Where MenuID=" + arr[i] + "")) + ",";
                }
                RefVoucherID = RefVoucherID.Remove(RefVoucherID.Length - 1);
            }
            catch { }
            #endregion
        }
    }
}
