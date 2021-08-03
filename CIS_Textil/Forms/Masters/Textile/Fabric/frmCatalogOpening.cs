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
    public partial class frmCatalogOpening : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        public frmCatalogOpening()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
            fgDtls.ShowFieldChooser = true;
        }
        string strunit = "";

        #region Event
        private void frmCatalogOpening_Load(object sender, EventArgs e)
        {
            try
            {
                strunit = DB.GetSnglValue("Select UnitID From tbl_UnitsMaster Where UnitName='Pcs'");
                Combobox_Setup.FillCbo(ref cboDepartment, Combobox_Setup.ComboType.Mst_Department, "");
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                this.fgDtls.CellEndEdit += new DataGridViewCellEventHandler(this.fgDtls_CellEndEdit);
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
                DBValue.Return_DBValue(this, txtCode, "CatOpeningID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, dtOpDate, "OpDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboDepartment, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtTotalQty, "TotPcs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtED1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "CatOpeningID", Conversions.ToString(Localization.ParseNativeDouble(this.txtCode.Text)), base.dt_HasDtls_Grd, this.iIDentity, GlobalVariables.VALIDATE_EDIT, Db_Detials.CompID.ToString(), 2);
                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
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
                        if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_CatalogSalesDtls_FR(" + Db_Detials.StoreID + "," + Db_Detials.CompID + "," + Db_Detials.BranchID + "," + Db_Detials.YearID + ") WHERE DepartmentID=" + cboDepartment.SelectedValue + "" + " AND CatalogID=" + Localization.ParseNativeInt(fgDtls.Rows[i].Cells[4].Value.ToString()) + "")) > 0)
                        {
                            fgDtls.Rows[i].ReadOnly = true;
                            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle;
                        }
                        else if (DetailGrid_Setup.isRowsEditable)
                        {
                            fgDtls.Rows[i].ReadOnly = false;
                        }
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
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);

                int MaxID = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(CatOpeningID),0) From {0}  Where CompID = {1} and YearID = {2}", "tbl_CatalogOpeningMain", Db_Detials.CompID, Db_Detials.YearID))));
                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where CatOpeningID = {1} and StoreID={2} and CompID={3} and BranchID={4} and YearID={5}", new object[] { "tbl_CatalogOpeningMain", MaxID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                        cboDepartment.SelectedValue = Localization.ParseNativeInt(reader["DepartmentID"].ToString());
                    }
                }

                if (fgDtls.Columns[3].Visible)
                {
                    fgDtls.Rows[0].Cells[3].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select  {0}({1},{2},{3},{4},{5},{6})", new object[] { "fn_FetchCatalogBarcodeNo", MaxID, base.iIDentity, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })), Db_Detials.PCS_NO_INCMT);
                }
                else
                {
                    fgDtls.Rows[0].Cells[3].Value = "-";
                }

                dtEntryDate.Focus();
                txtTotalQty.Text = "0";
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
                    (dtOpDate.TextFormat(false, true)),
                    (cboDepartment.SelectedValue),
                    (txtTotalQty.Text.ToString().Replace(",", "")),
                    (txtDescription.Text.ToString() == ""? "-": txtDescription.Text.ToString()),
                    (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                    (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                    (dtED1.TextFormat(false, true)),
                    (txtET1.Text.Trim()),
                    (txtET2.Text.Trim()),
                    (txtET3.Text.Trim())
                };
                string strAdjQry = string.Empty;
                //strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_AcLedger", "(#CodeID#)", base.iIDentity);
                strAdjQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockCatalogLedger", "(#CodeID#)", base.iIDentity);
                //if (Localization.ParseNativeDouble(TxtGrossAmount.Text.ToString()) > 0.0)
                //{
                //    int OpLedgerId = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format("Select LedgerId From {0} Where LedgerName = 'OPENING STOCK' ", "tbl_LedgerMaster"))));
                //    strAdjQry = strAdjQry + DBSp.InsertInto_AcLedger("(#CodeID#)", "0", "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), cboDepartment.SelectedValue.ToString(), 2, Db_Detials.Ac_AdjType.OnAccount, "(#CodeID#)", txtEntryNo.Text.Trim(), dtOpDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), 0, Localization.ParseNativeDecimal(TxtGrossAmount.Text), txtDescription.Text.Trim(),Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date) + DBSp.InsertInto_AcLedger("(#CodeID#)", "0", txtEntryNo.Text.ToString(), dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), OpLedgerId.ToString(), 1, Db_Detials.Ac_AdjType.OnAccount, "(#CodeID#)", txtEntryNo.Text.Trim(), dtOpDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDecimal(TxtGrossAmount.Text), 0, txtDescription.Text.Trim(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                //}
                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    //if (row.Cells[14].Value != null)
                    {
                        if (row.Cells[2].Value != null && row.Cells[2].Value.ToString() != "" && Localization.ParseNativeDouble(row.Cells[6].Value.ToString()) > 0)
                        {
                            string BatchNo = "";
                            if (row.Cells[2].Value != null)
                            {
                                if (row.Cells[2].Value.ToString().Length > 0)
                                {
                                    BatchNo = row.Cells[2].Value.ToString();
                                }
                                else
                                {
                                    BatchNo = "-";
                                }
                            }

                            string BarCodeNo = "";
                            if (row.Cells[3].Value != null)
                            {
                                if (row.Cells[3].Value.ToString().Length > 0)
                                {
                                    BarCodeNo = row.Cells[3].Value.ToString();
                                }
                                else
                                {
                                    BarCodeNo = "-";
                                }
                            }
                            string OpDt = Localization.ToVBDateString(DB.GetSnglValue(string.Format("select Yr_From from tbl_YearMaster where YearID={0}", Db_Detials.YearID)));
                            // OpDt = DateAndTime.DateAdd("d", -1.0, OpDt).ToString();
                            strAdjQry = strAdjQry + DBSp.InsertIntoCatalogStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtOpDate.Text.Trim().ToString(),
                                Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()), row.Cells[9].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[9].Value.ToString()),
                                base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(), base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                BatchNo, BarCodeNo, 
                                Localization.ParseNativeDouble(row.Cells[4].Value.ToString()), Localization.ParseNativeDouble(row.Cells[5].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[6].Value.ToString()),
                                0, row.Cells[7].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[7].Value.ToString()), row.Cells[10].Value == null ? "NULL" : (row.Cells[10].Value.ToString() == "" ? "NULL" : row.Cells[10].Value.ToString()),
                                row.Cells[12].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[12].Value.ToString()),
                                row.Cells[13].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                                row.Cells[14].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[14].Value.ToString()),
                                row.Cells[15].Value == null || row.Cells[15].Value.ToString() == "" || row.Cells[15].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[15].Value.ToString()),
                                row.Cells[16].Value == null || row.Cells[16].Value.ToString() == "" || row.Cells[16].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[16].Value.ToString()),
                                row.Cells[17].Value == null || row.Cells[17].Value.ToString() == "" ? "-" : row.Cells[17].Value.ToString(),
                                row.Cells[18].Value == null || row.Cells[18].Value.ToString() == "" ? "-" : row.Cells[18].Value.ToString(),
                                row.Cells[19].Value == null || row.Cells[19].Value.ToString() == "" ? "-" : row.Cells[19].Value.ToString(),
                                row.Cells[20].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[20].Value.ToString()),
                                row.Cells[21].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[21].Value.ToString()),
                                "NULL", i, 1,
                                Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                        }
                    }
                    row = null;
                }
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Null", "null");
                DBSp.Transcation_AddEdit(pArrayData, fgDtls, true, strAdjQry, "", txtEntryNo.Text, "", "");
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

                if (!Information.IsDate(dtEntryDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry Date");
                    dtEntryDate.Focus();
                    return true;
                }

                if (!Information.IsDate(dtOpDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Opening Date");
                    dtOpDate.Focus();
                    return true;
                }
                if (cboDepartment.SelectedValue == null || cboDepartment.SelectedValue.ToString() == "-" || cboDepartment.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    cboDepartment.Focus();
                    return true;
                }
                if (!this.cboDepartment.IsValidSelect)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Valid Department");
                    cboDepartment.Focus();
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

        private void CalcVal()
        {
            txtTotalQty.Text = string.Format("{0:N0}", CommonCls.GetColSum(this.fgDtls, 6, -1, -1));
        }

        private void fgDtls_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    string strTbleName;
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        string primaryFieldNameValue = fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString();
                        if (fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString().Length > 0)
                        {
                            if (fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString() != "-")
                            {
                                strTbleName = "tbl_StockCatalogLedger";
                                if (Navigate.CheckDuplicate(ref strTbleName, "BarCodeNo", primaryFieldNameValue, false, "", 0L, " StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                                {
                                    fgDtls.CurrentCell = fgDtls[3, e.RowIndex];
                                }
                            }
                        }
                        else if (fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString().Length <= 0)
                        {
                            fgDtls.Rows[e.RowIndex].Cells[3].Value = "-";
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        if (fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString().Length > 0)
                        {
                            if (fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString() != "-")
                            {
                                strTbleName = "tbl_StockCatalogLedger";
                                if (Navigate.CheckDuplicate(ref strTbleName, "BarCodeNo", fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString(), true, "TransID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), " StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                                {
                                    fgDtls.CurrentCell = fgDtls[3, e.RowIndex];
                                }
                            }
                        }
                        else if (fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString().Length <= 0)
                        {
                            fgDtls.Rows[e.RowIndex].Cells[3].Value = "-";
                        }
                    }
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
                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    switch (e.ColumnIndex)
                    {
                        case 6:
                            this.CalcVal();
                            break;

                        case 4:
                            if (e.ColumnIndex == 4)
                            {
                                fgDtls.Rows[e.RowIndex].Cells[5].Value = Localization.ParseNativeInt(strunit);
                            }
                            break;
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
                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    if (fgDtls.Rows.Count > 1)
                    {
                        int iCount = this.fgDtls.CurrentRow.Index + 1;
                        if (iCount < fgDtls.Rows.Count)
                        {
                            for (int i = 3; i <= (fgDtls.ColumnCount - 8); i++)
                            {
                                fgDtls.Rows[iCount].Cells[i].Value = fgDtls.Rows[iCount - 1].Cells[i].Value;
                            }
                        }
                        fgDtls.CurrentCell = fgDtls[6, e.RowIndex];
                        if (fgDtls.Rows[e.RowIndex - 1].Cells[3].Value.ToString().Trim() != "-")
                        {
                            fgDtls.Rows[e.RowIndex].Cells[3].Value = CommonCls.AutoInc_Runtime(fgDtls.Rows[e.RowIndex - 1].Cells[3].Value.ToString(), Db_Detials.PCS_NO_INCMT);
                        }
                        else
                        {
                            fgDtls.Rows[e.RowIndex].Cells[3].Value = "-";
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                object frm = Navigate.GetActiveChild();
                dynamic frmObj = frm;
                int iCalcCol = 0;

                if ((e.Control == true & e.KeyCode == Keys.D) | e.KeyCode == Keys.F5)
                {
                    string sRefID = "";
                    try
                    {
                        sRefID = fgDtls.Rows[fgDtls.CurrentRow.Index].Cells["RefID"].Value.ToString();
                    }
                    catch { sRefID = "0"; }

                    if (fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[4].Value != null)
                    {
                        if (Localization.ParseNativeInt(DB.GetSnglValue(("SELECT count(0) from fn_StockCatalogLedger_Tbl() WHERE RefID<>'' AND RefID='" + sRefID + "'" + (" and CompID=" + Db_Detials.CompID + "")))) > 1 || (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_CatalogSalesDtls_FR(" + Db_Detials.StoreID + "," + Db_Detials.CompID + "," + Db_Detials.BranchID + "," + Db_Detials.YearID + ") WHERE DepartmentID=" + cboDepartment.SelectedValue + "" + " AND CatalogID=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[4].Value.ToString()) + "")) > 0))
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

        public void PrintRecord()
        {
            try
            {
                CIS_ReportTool.frmMultiPrint frmMultiPrint = new CIS_ReportTool.frmMultiPrint();
                CIS_ReportTool.frmMultiPrint.MenuID = base.iIDentity;
                CIS_ReportTool.frmMultiPrint.Id = Localization.ParseNativeInt(this.txtCode.Text);
                CIS_ReportTool.frmMultiPrint.TblNm = "tbl_CatalogOpeningMain";
                CIS_ReportTool.frmMultiPrint.IdStr = "CatOpeningID";
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

    }
}
