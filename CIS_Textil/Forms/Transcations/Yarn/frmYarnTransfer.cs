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
    public partial class frmYarnTransfer : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        public ArrayList OrgInGridArray;
        public string strUniqueID;
        private int iMaxMyID_Stock;

        public frmYarnTransfer()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
            OrgInGridArray = new ArrayList();
        }

        #region Form Events
        private void frmYarnTransfer_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboDepartFrm, Combobox_Setup.ComboType.Mst_DepartmentNWeaverNSizer, "");
                Combobox_Setup.FillCbo(ref cboDepartTo, Combobox_Setup.ComboType.Mst_DepartmentNWeaverNSizer, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                //EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls.KeyDown += new KeyEventHandler(this.fgDtls_KeyDown);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }

        }
        #endregion

        #region Event Navigation

        public void MovetoField()
        {
            try
            {
                {
                    if (strUniqueID != null)
                    {
                        string strQry = string.Format("Delete From tbl_StockYarnLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                        strQry = strQry + string.Format("Update  tbl_StockYarnLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                        DB.ExecuteSQL(strQry);
                        strQry = string.Format("Update tbl_StockYarnLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                        DB.ExecuteSQL(strQry);
                    }
                    txtCode.Text = "";
                    CIS_Textbox txtEntryNo = this.txtEntryNo;
                    CommonCls.IncFieldID(this, ref txtEntryNo, "");
                    this.txtEntryNo = txtEntryNo;
                    this.txtRefNo.Text = CommonCls.AutoInc(this, "RefNo", "YarnTransID", "");
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                    int MaxiD = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Max(isnull(YarnTransID,0)) From {0} Where StoreID = {1} and CompID = {2} and BranchID = {3} and YearID = {4}", "tbl_YarnTransferMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID)));
                    using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where YarnTransID = {1} ", "tbl_YarnTransferMain", MaxiD)))
                    {
                        while (reader.Read())
                        {
                            dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                            dtRefDate.Text = Localization.ToVBDateString(reader["RefDate"].ToString());
                            cboTransport.SelectedValue = Localization.ParseNativeInt(reader["TransportID"].ToString());
                            cboDepartFrm.SelectedValue = Localization.ParseNativeInt(reader["DepartmentfmID"].ToString());
                            cboDepartTo.SelectedValue = Localization.ParseNativeInt(reader["DepartmenttoID"].ToString());
                        }
                    }
                    dtEntryDate.Focus();
                    txtUniqueID.Text = CommonCls.GenUniqueID();
                    strUniqueID = txtUniqueID.Text;
                }
                AplySelectBtnEnbl();
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
                DBValue.Return_DBValue(this, txtCode, "YarnTransID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtRefNo, "RefNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtRefDate, "RefDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboDepartFrm, "DepartmentfmID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDepartTo, "DepartmenttoID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDesc, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtED1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);
                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "YarnTransID", txtCode.Text, base.dt_HasDtls_Grd);

                AplySelectBtnEnbl();

                int icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_StockYarnLedger_Tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    setTempRowIndex();
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);

                    try
                    {
                        string strOldUniqueID = string.Empty;
                        strOldUniqueID = txtUniqueID.Text;
                        txtUniqueID.Text = CommonCls.GenUniqueID();
                        strUniqueID = txtUniqueID.Text;
                        if (icount == 0)
                        {
                            string strQry = string.Format("Update tbl_StockYarnLedger Set UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + ", StatusID=2 Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + "");
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
                        string strQry = string.Format("Delete From tbl_StockYarnLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                        strQry = strQry + string.Format("Update  tbl_StockYarnLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                        DB.ExecuteSQL(strQry);
                        strQry = string.Format("Update tbl_StockYarnLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                        DB.ExecuteSQL(strQry);
                    }
                }

                if ((base.blnFormAction == Enum_Define.ActionType.View_Record) && !(base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_StockYarnLedger_Tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));
                }
                System.Windows.Forms.DataGridViewCellStyle dgvCellStyle_Ref = new System.Windows.Forms.DataGridViewCellStyle();
                dgvCellStyle_Ref.BackColor = System.Drawing.Color.LightSteelBlue;
                dgvCellStyle_Ref.ForeColor = System.Drawing.SystemColors.WindowText;
                dgvCellStyle_Ref.SelectionBackColor = System.Drawing.Color.SteelBlue;
                dgvCellStyle_Ref.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
                try
                {
                    for (int j = 0; j <= fgDtls.Rows.Count - 1; j++)
                    {
                        if (icount > 0)
                        {
                            btnSelect.Enabled = false;
                            fgDtls.Rows[j].ReadOnly = true;
                            fgDtls.Rows[j].DefaultCellStyle = dgvCellStyle_Ref;
                        }
                        else
                        {
                            btnSelect.Enabled = true;
                            fgDtls.Rows[j].ReadOnly = false;
                        }
                    }
                }
                catch (Exception ex1) { Navigate.logError(ex1.Message, ex1.StackTrace); }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        public void SaveRecord()
        {
            try
            {
                string sTotQty = "", sTotWeight = "";
                sTotQty = string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 13, -1, -1));
                sTotWeight = string.Format("{0:N3}", CommonCls.GetColSum(this.fgDtls, 15, -1, -1));

                ArrayList pArrayData = new ArrayList
                {
                    this.frmVoucherTypeID,
                    "(#ENTRYNO#)",
                    dtEntryDate.TextFormat(false, true),
                    "(#OTHERNO#)",
                    dtRefDate.TextFormat(false, true),
                    cboDepartFrm.SelectedValue,
                    cboDepartTo.SelectedValue,
                    cboTransport.SelectedValue,
                    (txtDesc.Text == "" ? "-" : txtDesc.Text),
                    sTotQty.Replace(",",""),
                    sTotWeight.Replace(",",""),
                    (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                    (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                    (dtED1.TextFormat(false, true)),
                    (txtET1.Text.Trim()),
                    (txtET2.Text.Trim()),
                    (txtET3.Text.Trim())
                };

                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockYarnLedger", "(#CodeID#)", Localization.ParseNativeInt(base.iIDentity.ToString()));
                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    string BatchNo = "";
                    DataGridViewRow row = fgDtls.Rows[i];
                    if (row.Cells[9].Value.ToString() != null && row.Cells[9].Value.ToString().Length > 0)
                    {
                        BatchNo = row.Cells[9].Value.ToString();
                    }
                    else
                    {
                        BatchNo = "-";
                    }
                    
                    if (row.Cells[15].Value != null)
                    {
                        strAdjQry = strAdjQry + DBSp.InsertIntoYarnStockLedger(Localization.ParseNativeInt(base.iIDentity.ToString()), "(#CodeID#)", (i + 1).ToString(),
                                "(#ENTRYNO#)", dtRefDate.Text,
                                row.Cells[17].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                row.Cells[38].Value == null ? "NULL" : row.Cells[38].Value.ToString().Trim() == "" ? "NULL" : row.Cells[38].Value.ToString(),
                                BatchNo, (row.Cells[11].Value == null ? "null" : row.Cells[11].Value.ToString() == "" ? "" : row.Cells[11].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[5].Value.ToString()), Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[7].Value.ToString()), Localization.ParseNativeDouble(row.Cells[12].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[14].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()), 0, 0, 0,
                                row.Cells[16].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                "NULL", row.Cells[19].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[19].Value.ToString()),
                                row.Cells[20].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[20].Value.ToString()),
                                row.Cells[21].Value == null ? "NULL" : row.Cells[21].Value.ToString(),
                                row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                row.Cells[27].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[27].Value.ToString()),
                                row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                row.Cells[29].Value == null || row.Cells[29].Value.ToString() == "" || row.Cells[29].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[29].Value.ToString()),
                                row.Cells[30].Value == null || row.Cells[30].Value.ToString() == "" || row.Cells[30].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[30].Value.ToString()),
                                row.Cells[31].Value == null || row.Cells[31].Value.ToString() == "" ? "-" : row.Cells[31].Value.ToString(),
                                row.Cells[32].Value == null || row.Cells[32].Value.ToString() == "" ? "-" : row.Cells[32].Value.ToString(),
                                row.Cells[33].Value == null || row.Cells[33].Value.ToString() == "" ? "-" : row.Cells[33].Value.ToString(),
                                row.Cells[34].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[34].Value.ToString()),
                                row.Cells[35].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[35].Value.ToString()),
                                "null", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);

                        strAdjQry = strAdjQry + DBSp.InsertIntoYarnStockLedger(Localization.ParseNativeInt(base.iIDentity.ToString()), "(#CodeID#)", (i + 1).ToString(),
                                "(#ENTRYNO#)", dtRefDate.Text,
                                row.Cells[17].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                row.Cells[38].Value == null ? "NULL" : row.Cells[38].Value.ToString().Trim() == "" ? "NULL" : row.Cells[38].Value.ToString(),
                                BatchNo, (row.Cells[11].Value == null ? "NULL" : row.Cells[11].Value.ToString() == "" ? "" : row.Cells[11].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[5].Value.ToString()), Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                Localization.ParseNativeDouble(row.Cells[7].Value.ToString()), Localization.ParseNativeDouble(row.Cells[12].Value.ToString()),
                                0, 0, 0,
                                Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[14].Value.ToString()),
                                Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()), 
                                row.Cells[16].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                "NULL", row.Cells[19].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[19].Value.ToString()),
                                row.Cells[20].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[20].Value.ToString()),
                                row.Cells[21].Value == null ? "NULL" : row.Cells[21].Value.ToString(),
                                row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                row.Cells[27].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[27].Value.ToString()),
                                row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                row.Cells[29].Value == null || row.Cells[29].Value.ToString() == "" || row.Cells[29].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[29].Value.ToString()),
                                row.Cells[30].Value == null || row.Cells[30].Value.ToString() == "" || row.Cells[30].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[30].Value.ToString()),
                                row.Cells[31].Value == null || row.Cells[31].Value.ToString() == "" ? "-" : row.Cells[31].Value.ToString(),
                                row.Cells[32].Value == null || row.Cells[32].Value.ToString() == "" ? "-" : row.Cells[32].Value.ToString(),
                                row.Cells[33].Value == null || row.Cells[33].Value.ToString() == "" ? "-" : row.Cells[33].Value.ToString(),
                                row.Cells[34].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[34].Value.ToString()),
                                row.Cells[35].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[35].Value.ToString()),
                                "null", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                    }
                }
                int UnitID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select UnitID from {0} Where UnitName = Yarn", "tbl_UnitsMaster")));
                if ((cboTransport.SelectedValue != null) && (Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()) > 0.0))
                {
                    //strAdjQry = strAdjQry + DBSp.InsertIntoTrasportLedger("(#CodeID#)", "(#OTHERNO#)", dtRefDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()), Localization.ParseNativeDouble(cboDepartFrm.SelectedValue.ToString()),
                    //    Localization.ParseNativeDouble(cboDepartTo.SelectedValue.ToString()), "null", null, "null", (double)UnitID, Localization.ParseNativeInt(string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 11, -1, -1))), Localization.ParseNativeDecimal(string.Format("{0:N3}", CommonCls.GetColSum(this.fgDtls, 13, -1, -1))), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                }
                strAdjQry += "Delete From tbl_StockYarnLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";";
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                DBSp.Transcation_AddEdit(pArrayData, this.fgDtls, true, strAdjQry, "", txtEntryNo.Text, txtRefNo.Text, "RefNo");
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
                if ((!EventHandles.IsRequiredInGrid(fgDtls, this.dt_AryIsRequired, false) || !CommonCls.CheckDate(dtEntryDate.Text, true)) || !CommonCls.CheckDate(dtRefDate.Text, true))
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
                if (txtRefNo.Text.Trim() == "" || txtRefNo.Text.Trim() == "-" || txtRefNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Ref No.");
                    this.txtRefNo.Focus();
                    return true;
                }
                if ((txtRefNo.Text != null) && (txtRefNo.Text.Trim().Length) > 0)
                {
                    string strTblName;
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        strTblName = "tbl_YarnTransferMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "RefNo", txtRefNo.Text, false, "", 0L, string.Format("StoreID = {0} and CompID = {1} and BranchID = {2} and YearID = {3}", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID), "This Ref No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where RefNo = '{1}' and StoreID = {2} and CompID = {3} and BranchID = {4} and YearID = {5}", new object[] { "tbl_YarnTransferMain", txtRefNo.Text.ToString(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }))))
                        {
                            txtRefNo.Focus();
                            return true;
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        strTblName = "tbl_YarnTransferMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "RefNo", txtRefNo.Text, true, "YarnTransID", (long)Math.Round(Localization.ParseNativeDecimal(txtCode.Text.Trim())), string.Format("StoreID = {0} and CompID = {1} and BranchID = {2} and YearID = {3}", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID), "This Ref No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where RefNo = '{1}' and StoreID = {2} and And CompID = {3} and BranchID = {4} and YearID = {5}", new object[] { "tbl_YarnTransferMain", txtRefNo.Text.ToString(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }))))
                        {
                            txtRefNo.Focus();
                            return true;
                        }
                    }
                }
                if (!Information.IsDate(dtRefDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Ref Date");
                    this.dtRefDate.Focus();
                    return true;
                }
                if (cboDepartFrm.SelectedValue == null || cboDepartFrm.Text.Trim().ToString() == "-" || cboDepartFrm.Text.Trim().ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department From");
                    cboDepartFrm.Focus();
                    return true;
                }
                if (cboDepartTo.SelectedValue == null || cboDepartTo.Text.Trim().ToString() == "-" || cboDepartTo.Text.Trim().ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department To");
                    cboDepartTo.Focus();
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

        #region Other
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

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (Information.IsDate(dtRefDate.Text.ToString()))
                {
                    if (cboDepartFrm.SelectedValue == null || cboDepartFrm.Text.Trim().ToString() == "-" || cboDepartFrm.Text.Trim().ToString() == "0" || cboDepartFrm.Text.Trim().ToString() == "")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department From");
                        cboDepartFrm.Focus();
                        return;
                    }

                    #region StockAdjQuery
                    string strQry = string.Empty;
                    int ibitcol = 0;
                    string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", iIDentity, 0.0));
                    string strQry_ColName = "";
                    string[] arr = CommonCls.GetAdjColName(base.iIDentity, 0.0).Split(';');
                    strQry_ColName = arr[0].ToString();
                    ibitcol = Localization.ParseNativeInt(arr[1]);
                    strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}, {6}, {7})", new object[] { strQry_ColName, snglValue, DB.DateQuote(Localization.ToSqlDateString(Conversions.ToString(dtRefDate.Text))), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboDepartFrm.SelectedValue });
                    #endregion

                    frmStockAdj frmStockAdj = new CIS_Textil.frmStockAdj();
                    frmStockAdj.MenuID = base.iIDentity;
                    frmStockAdj.Entity_IsfFtr = 0.0;
                    frmStockAdj.ref_fgDtls = this.fgDtls;
                    frmStockAdj.AsonDate = Conversions.ToDate(this.dtRefDate.Text);
                    frmStockAdj.LedgerID = cboDepartFrm.SelectedValue.ToString();
                    frmStockAdj.UsedInGridArray = OrgInGridArray;
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
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Ref Date");
                }
                this.fgDtls.Select();
                setTempRowIndex();
                setMyID_Stock();
                ExecuterTempQry(-1);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    try
                    {
                        if ((e.ColumnIndex == 13) | (e.ColumnIndex == 15))
                        {
                            ExecuterTempQry(e.RowIndex);
                        }
                    }
                    catch (Exception ex1)
                    {
                        Navigate.logError(ex1.Message, ex1.StackTrace);
                    }

                    if (e.ColumnIndex == 2)
                    {
                        if (this.fgDtls.Rows[e.RowIndex].Cells[2].Value != null)
                        {
                            this.fgDtls.Rows[e.RowIndex].Cells[5].Value = this.fgDtls.Rows[e.RowIndex].Cells[2].Value;
                        }
                    }
                    else if (e.ColumnIndex == 3)
                    {
                        if (this.fgDtls.Rows[e.RowIndex].Cells[3].Value != null)
                        {
                            this.fgDtls.Rows[e.RowIndex].Cells[6].Value = this.fgDtls.Rows[e.RowIndex].Cells[3].Value;
                        }
                    }
                    else if ((e.ColumnIndex == 4) && (this.fgDtls.Rows[e.RowIndex].Cells[4].Value != null))
                    {
                        this.fgDtls.Rows[e.RowIndex].Cells[7].Value = this.fgDtls.Rows[e.RowIndex].Cells[4].Value;
                    }
                    if ((e.ColumnIndex == 8) && (this.fgDtls.Rows[e.RowIndex].Cells[9].Value == null))
                    {
                        this.fgDtls.Rows[e.RowIndex].Cells[9].Value = this.fgDtls.Rows[e.RowIndex].Cells[8].Value;
                    }
                    if (e.ColumnIndex == 8)
                    {
                        if (this.fgDtls.Rows[e.RowIndex].Cells[8].Value != null)
                        {
                            this.fgDtls.Rows[e.RowIndex].Cells[9].Value = this.fgDtls.Rows[e.RowIndex].Cells[8].Value;
                        }
                    }
                    if (e.ColumnIndex == 10)
                    {
                        if (this.fgDtls.Rows[e.RowIndex].Cells[10].Value != null)
                        {
                            this.fgDtls.Rows[e.RowIndex].Cells[11].Value = this.fgDtls.Rows[e.RowIndex].Cells[10].Value;
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

        private void cboDepartFrm_SelectedValueChanged(object sender, EventArgs e)
        {
            EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
            EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
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
                            strQry = string.Format("Delete From tbl_StockYarnLedger Where Dr_Qty=0 and Dr_Wt=0 and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
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
                                    StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockYarnLedger_Tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockYarnLedger_Tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + "")));
                                    MyID = txtCode.Text;
                                }

                                if (MyID != "" && row.Cells[15].Value != null && row.Cells[15].Value.ToString() != "" && row.Cells[15].Value.ToString() != "0")
                                {
                                    if (Localization.ParseNativeDouble(row.Cells[15].Value.ToString()) > 0)
                                    {
                                        strQry = strQry + DBSp.InsertIntoYarnStockLedger(Localization.ParseNativeInt(base.iIDentity.ToString()), MyID, (i + 1).ToString(),
                                                txtEntryNo.Text, dtRefDate.Text,
                                                row.Cells[17].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                                row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                                row.Cells[37].Value == null ? "NULL" : row.Cells[37].Value.ToString().Trim() == "" ? "NULL" : row.Cells[37].Value.ToString(),
                                                row.Cells[38].Value == null ? "NULL" : row.Cells[38].Value.ToString().Trim() == "" ? "NULL" : row.Cells[38].Value.ToString(),
                                                (row.Cells[9].Value == null ? "-" : (row.Cells[9].Value.ToString().Length > 0 ? "-" : row.Cells[9].Value.ToString())),
                                                (row.Cells[11].Value == null ? "NULL" : (row.Cells[11].Value.ToString() == "" ? "" : row.Cells[11].Value.ToString())),
                                                Localization.ParseNativeDouble(row.Cells[5].Value.ToString()), Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                                Localization.ParseNativeDouble(row.Cells[7].Value.ToString()), Localization.ParseNativeDouble(row.Cells[12].Value.ToString()),
                                                0, 0, 0,
                                                Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[14].Value.ToString()),
                                                Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                                                row.Cells[16].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                                "NULL", row.Cells[19].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[19].Value.ToString()),
                                                row.Cells[20].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[20].Value.ToString()),
                                                row.Cells[21].Value == null ? "NULL" : row.Cells[21].Value.ToString(),
                                                row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                                row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                                row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                                row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                                row.Cells[27].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[27].Value.ToString()),
                                                row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                                row.Cells[29].Value == null || row.Cells[29].Value.ToString() == "" || row.Cells[29].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[29].Value.ToString()),
                                                row.Cells[30].Value == null || row.Cells[30].Value.ToString() == "" || row.Cells[30].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[30].Value.ToString()),
                                                row.Cells[31].Value == null || row.Cells[31].Value.ToString() == "" ? "-" : row.Cells[31].Value.ToString(),
                                                row.Cells[32].Value == null || row.Cells[32].Value.ToString() == "" ? "-" : row.Cells[32].Value.ToString(),
                                                row.Cells[33].Value == null || row.Cells[33].Value.ToString() == "" ? "-" : row.Cells[33].Value.ToString(),
                                                row.Cells[34].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[34].Value.ToString()),
                                                row.Cells[35].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[35].Value.ToString()),
                                                txtUniqueID.Text, i, StatusID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID,
                                                Db_Detials.UserID, DateAndTime.Now.Date);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if ((fgDtls.CurrentCell.ColumnIndex == 13) || (fgDtls.CurrentCell.ColumnIndex == 15))
                            {
                                DataGridViewRow row = fgDtls.Rows[RowIndex];
                                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                                {
                                    StatusID = 1;
                                    MyID = iMaxMyID_Stock.ToString();
                                }
                                else
                                {
                                    StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockYarnLedger_Tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_StockYarnLedger_Tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + "")));
                                    MyID = txtCode.Text;
                                }

                                if (MyID != "" && row.Cells[15].Value != null && row.Cells[15].Value.ToString() != "" && row.Cells[15].Value.ToString() != "0")
                                {
                                    if (txtUniqueID.Text != null)
                                    {
                                        strQry += string.Format("Delete From tbl_StockYarnLedger Where Dr_Qty=0 and Dr_Wt=0 and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[40].Value.ToString()) + " and AddedBy=" + Db_Detials.UserID + ";");

                                        strQry = strQry + DBSp.InsertIntoYarnStockLedger(Localization.ParseNativeInt(base.iIDentity.ToString()), MyID, (RowIndex + 1).ToString(),
                                                txtEntryNo.Text, dtRefDate.Text,
                                                row.Cells[17].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                                row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                                row.Cells[37].Value == null ? "NULL" : row.Cells[37].Value.ToString().Trim() == "" ? "NULL" : row.Cells[37].Value.ToString(),
                                                row.Cells[38].Value == null ? "NULL" : row.Cells[38].Value.ToString().Trim() == "" ? "NULL" : row.Cells[38].Value.ToString(),
                                                (row.Cells[9].Value == null ? "-" : (row.Cells[9].Value.ToString().Length > 0 ? "-" : row.Cells[9].Value.ToString())),
                                                (row.Cells[11].Value == null ? "NULL" : (row.Cells[11].Value.ToString() == "" ? "" : row.Cells[11].Value.ToString())),
                                                Localization.ParseNativeDouble(row.Cells[5].Value.ToString()), Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                                Localization.ParseNativeDouble(row.Cells[7].Value.ToString()), Localization.ParseNativeDouble(row.Cells[12].Value.ToString()),
                                                0, 0, 0,
                                                Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[14].Value.ToString()),
                                                Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                                                row.Cells[16].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()),
                                                "NULL", row.Cells[19].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[19].Value.ToString()),
                                                row.Cells[20].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[20].Value.ToString()),
                                                row.Cells[21].Value == null ? "NULL" : row.Cells[21].Value.ToString(),
                                                row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                                row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                                row.Cells[25].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[25].Value.ToString()),
                                                row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()),
                                                row.Cells[27].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[27].Value.ToString()),
                                                row.Cells[28].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[28].Value.ToString()),
                                                row.Cells[29].Value == null || row.Cells[29].Value.ToString() == "" || row.Cells[29].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[29].Value.ToString()),
                                                row.Cells[30].Value == null || row.Cells[30].Value.ToString() == "" || row.Cells[30].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[30].Value.ToString()),
                                                row.Cells[31].Value == null || row.Cells[31].Value.ToString() == "" ? "-" : row.Cells[31].Value.ToString(),
                                                row.Cells[32].Value == null || row.Cells[32].Value.ToString() == "" ? "-" : row.Cells[32].Value.ToString(),
                                                row.Cells[33].Value == null || row.Cells[33].Value.ToString() == "" ? "-" : row.Cells[33].Value.ToString(),
                                                row.Cells[34].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[34].Value.ToString()),
                                                row.Cells[35].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[35].Value.ToString()),
                                                txtUniqueID.Text, Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[40].Value.ToString()), StatusID,
                                                Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                                    }
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
                            if ((Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select COUNT(0) From fn_StockYarnLedger_Tbl() Where RefId='" + fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[36].Value + "' and RefID<>'' and Transtype<>" + iIDentity + ""))) > 0))
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityWarning, "", "Reference Found In Another Module..Row Cannot Be Deleted");
                                return;
                            }
                            else
                            {
                                try
                                {
                                    string strQry = string.Format("Update tbl_StockYarnLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[40].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
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
                                }
                                else
                                {
                                    EventHandles.CreateDefault_Rows(fgDtls, table2, table3, table4, true, false);
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
                                string strQry = string.Format("Update tbl_StockYarnLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[40].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
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
                            }
                            else
                            {
                                EventHandles.CreateDefault_Rows(fgDtls, table2, table3, table4, true, false);
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

        private void setTempRowIndex()
        {
            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[40].Value = i;
            }
        }

        private void frmYarnTransfer_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (strUniqueID != null)
            {
                string strQry = string.Format("Delete From tbl_StockYarnLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                strQry = strQry + string.Format("Update  tbl_StockYarnLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                DB.ExecuteSQL(strQry);
                strQry = string.Format("Update tbl_StockYarnLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                DB.ExecuteSQL(strQry);
            }
        }

        private void setMyID_Stock()
        {
            iMaxMyID_Stock = Localization.ParseNativeInt(DB.GetSnglValue("Select MAX(MyId + 1) from tbl_StockYarnLedger Where IsDeleted=0"));

            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[39].Value = iMaxMyID_Stock;
            }
        }
    }
}
