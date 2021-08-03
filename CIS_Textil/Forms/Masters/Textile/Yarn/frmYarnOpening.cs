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
    public partial class frmYarnOpening : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        private string SRateCalcType = string.Empty;

        public frmYarnOpening()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
        }

        #region Form Events
        private void frmYarnOpening_Load(object sender, EventArgs e)
        {
            try
            {
                //Combobox_Setup.FilterId = "(41,43,44,45,46,47)";
                //CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboDepartment = this.CboDepartment;
                Combobox_Setup.FillCbo(ref CboDepartment, Combobox_Setup.ComboType.Mst_Ledger, "");
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #endregion

        #region Form Navigation

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                CIS_Textbox txtEntryNo = this.txtEntryNo;
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                this.txtEntryNo = txtEntryNo;
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                int MaxiD = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Max(isnull(YarnOpID,0)) From {0} Where StoreID={1} and CompID = {2} and BranchID = {3} and YearId = {4} and IsDeleted=0", "tbl_YarnOpeningMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID)));
                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where YarnOpID={1} and StoreID={2} and CompID={3} and BranchID={4} and YearID={5}", new object[] { "tbl_YarnOpeningMain", MaxiD, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                        dtOpDate.Text = Localization.ToVBDateString(reader["OpeningDate"].ToString());
                        CboDepartment.SelectedValue = Localization.ParseNativeInt(reader["DepartmentID"].ToString());
                    }
                }
                dtEntryDate.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "YarnOpID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, dtOpDate, "YarnOpDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, CboDepartment, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtNarration, "Narration", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtED1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "YarnOpID", Conversions.ToString(Localization.ParseNativeDouble(this.txtCode.Text)), base.dt_HasDtls_Grd);

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                }

                try
                {
                    System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                    dgvCellStyle.BackColor = System.Drawing.Color.LightGray;
                    dgvCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
                    dgvCellStyle.SelectionBackColor = System.Drawing.Color.Purple;
                    dgvCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

                    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    {
                        if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_StockYarnLedger_Tbl() WHERE RefID<>'' AND RefID=" + CommonLogic.SQuote(fgDtls.Rows[i].Cells["RefID"].Value.ToString()) + (Db_Detials.CompID.ToString() != "" ? " and CompID=" + Db_Detials.CompID : "") + "")) > 1)
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
                catch (Exception ex1)
                {
                    Navigate.logError(ex1.Message, ex1.StackTrace);
                }
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", exception1.Message);
            }
        }

        public void SaveRecord()
        {
            try
            {
                string sTotQty = "", sTotWeight = "", sTotAmt = "";
                sTotQty = string.Format("{0:N0}", CommonCls.GetColSum(this.fgDtls, 10, -1, -1));
                sTotWeight = string.Format("{0:N3}", CommonCls.GetColSum(this.fgDtls, 14, -1, -1));
                sTotAmt = string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 16, -1, -1));

                ArrayList pArrayData = new ArrayList
                {
                   "(#ENTRYNO#)",
                    dtEntryDate.TextFormat(false, true),
                    dtOpDate.TextFormat(false, true),
                    CboDepartment.SelectedValue,
                    sTotQty.Replace(",", ""),
                    sTotWeight.Replace(",", ""),
                    sTotAmt.Replace(",", ""),
                    txtNarration.Text.ToString(),
                    (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                    (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                    (dtED1.TextFormat(false, true)),
                    (txtET1.Text.Trim()),
                    (txtET2.Text.Trim()),
                    (txtET3.Text.Trim())
                };

                int opLedgerID = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format("Select LedgerId From {0} Where LedgerName = 'OPENING STOCK' ", "tbl_LedgerMaster"))));
                string strAdjQry = (string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockYarnLedger", "(#CodeID#)", Localization.ParseNativeInt(base.iIDentity.ToString())) + string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_AcLedger", "(#CodeID#)", Localization.ParseNativeInt(base.iIDentity.ToString())));
                if (Localization.ParseNativeDouble(sTotAmt) > 0)
                {
                    strAdjQry += DBSp.InsertInto_AcLedger("(#CodeID#)", "0", this.txtEntryNo.Text.ToString(), this.dtEntryDate.Text, (double)Localization.ParseNativeInt(base.iIDentity.ToString()), Conversions.ToString(this.CboDepartment.SelectedValue), 2, Db_Detials.Ac_AdjType.OnAccount, "(#CodeID#)", this.txtEntryNo.Text.Trim(), this.dtEntryDate.Text, (double)Localization.ParseNativeInt(base.iIDentity.ToString()), 0, Localization.ParseNativeDecimal(sTotAmt), this.txtNarration.Text.Trim(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date) + DBSp.InsertInto_AcLedger("(#CodeID#)", "0", this.txtEntryNo.Text.ToString(), this.dtEntryDate.Text, (double)Localization.ParseNativeInt(base.iIDentity.ToString()), Conversions.ToString(opLedgerID), 1, Db_Detials.Ac_AdjType.OnAccount, "(#CodeID#)", this.txtEntryNo.Text.Trim(), this.dtEntryDate.Text, (double)Localization.ParseNativeInt(base.iIDentity.ToString()), Localization.ParseNativeDecimal(sTotAmt), 0, this.txtNarration.Text.Trim(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                }

                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    string BatchNo = "";
                    DataGridViewRow row = fgDtls.Rows[i];
                    
                    if (row.Cells[8].Value.ToString() != null && row.Cells[8].Value.ToString().Length > 0)
                    {
                        BatchNo = row.Cells[8].Value.ToString();
                    }
                    else
                    {
                        BatchNo = "-";
                    }
                    
                    if (row.Cells[14].Value != null)
                    {
                        if (Localization.ParseNativeDouble(row.Cells[14].Value.ToString()) > 0)
                        {
                            string dateValue = Localization.ToVBDateString(dtOpDate.Text.ToString());
                            dateValue = DateAndTime.DateAdd("d", -1.0, dateValue).ToString();
                            strAdjQry = strAdjQry + DBSp.InsertIntoYarnStockLedger(Localization.ParseNativeInt(base.iIDentity.ToString()), "(#CodeID#)", (i + 1).ToString(),
                                    "(#ENTRYNO#)", dateValue,
                                    row.Cells[19].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[19].Value.ToString()),
                                    row.Cells[20].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[20].Value.ToString()),
                                    base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                    base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                    BatchNo, (row.Cells[9].Value == null ? "null" : row.Cells[9].Value.ToString() == "" ? "" : row.Cells[9].Value.ToString()),
                                    Localization.ParseNativeDouble(row.Cells[2].Value.ToString()), Localization.ParseNativeDouble(row.Cells[3].Value.ToString()),
                                    Localization.ParseNativeDouble(row.Cells[4].Value.ToString()), Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                    Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                    Localization.ParseNativeDecimal(row.Cells[14].Value.ToString()), 0, 0, 0,
                                    row.Cells[17].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[17].Value.ToString()),
                                    row.Cells[18].Value.ToString(),
                                    row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                    0, "(#CodeID#)", 0, 0, 0,
                                    row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                    row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()),
                                    row.Cells[24].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                    row.Cells[25].Value == null || row.Cells[25].Value.ToString() == "" || row.Cells[25].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[25].Value.ToString()),
                                    row.Cells[26].Value == null || row.Cells[26].Value.ToString() == "" || row.Cells[26].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[26].Value.ToString()),
                                    row.Cells[27].Value == null || row.Cells[27].Value.ToString() == "" ? "-" : row.Cells[27].Value.ToString(),
                                    row.Cells[28].Value == null || row.Cells[28].Value.ToString() == "" ? "-" : row.Cells[28].Value.ToString(),
                                    row.Cells[29].Value == null || row.Cells[29].Value.ToString() == "" ? "-" : row.Cells[29].Value.ToString(),
                                    row.Cells[30].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[30].Value.ToString()),
                                    row.Cells[31].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[31].Value.ToString()),
                                    "null", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                        }
                    }
                    row = null;
                }
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                DBSp.Transcation_AddEdit(pArrayData, this.fgDtls, true, strAdjQry, "", txtEntryNo.Text, "", "");
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

                if (!Information.IsDate(Strings.Trim(dtEntryDate.Text.ToString())))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry Date");
                    dtEntryDate.Focus();
                    return true;
                }

                if (!Information.IsDate(Strings.Trim(dtOpDate.Text.ToString())))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Opening Date");
                    dtEntryDate.Focus();
                    return true;
                }

                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    fgDtls.Rows[i].Cells[19].Value = CboDepartment.SelectedValue;
                }

                if (CboDepartment.SelectedValue == null || CboDepartment.SelectedValue.ToString() == "-" || CboDepartment.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    CboDepartment.Focus();
                    return true;
                }

                return false;
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                return false;
            }
        }

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record | base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    SRateCalcType = "";
                    if (fgDtls.Rows[e.RowIndex].Cells[6].Value != null && fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString() != "" && fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString() != "-")
                    {
                        SRateCalcType = DB.GetSnglValue("Select RateCalcType from tbl_UnitsMaster Where UnitID=" + fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString() + " and IsDeleted=0");
                    }
                    switch (e.ColumnIndex)
                    {
                        case 6:
                            goto case 10;

                        case 12:
                            goto case 10;

                        case 13:
                            goto case 10;

                        case 14:
                            goto case 10;

                        case 10:
                            if (SRateCalcType == "B")
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[10].Value != null && fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString() != "" && fgDtls.Rows[e.RowIndex].Cells[15].Value != null && fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != "")
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[16].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString())).ToString()));
                                    //fgDtls.Rows[e.RowIndex].Cells[21].Value = fgDtls.Rows[e.RowIndex].Cells[15].Value;
                                }
                            }
                            else if ((SRateCalcType == "W"))
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[14].Value != null && fgDtls.Rows[e.RowIndex].Cells[14].Value.ToString() != "" && fgDtls.Rows[e.RowIndex].Cells[15].Value != null && fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString() != "")
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[16].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[14].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString())).ToString()));
                                    //fgDtls.Rows[e.RowIndex].Cells[21].Value = fgDtls.Rows[e.RowIndex].Cells[15].Value;
                                }
                            }
                            break;

                        case 16:
                            if (SRateCalcType == "B")
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[10].Value != null && fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[16].Value != null && fgDtls.Rows[e.RowIndex].Cells[16].Value.ToString() != "0")
                                {
                                    if (Localization.ParseNativeDouble(Operators.DivideObject(fgDtls.Rows[e.RowIndex].Cells[16].Value, fgDtls.Rows[e.RowIndex].Cells[10].Value).ToString()) != Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString()))
                                    {
                                        fgDtls.Rows[e.RowIndex].Cells[15].Value = (Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[16].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString()));
                                    }
                                }
                            }
                            else if ((SRateCalcType == "W"))
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[14].Value != null && fgDtls.Rows[e.RowIndex].Cells[14].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[16].Value != null && fgDtls.Rows[e.RowIndex].Cells[16].Value.ToString() != "0")
                                {
                                    if (Localization.ParseNativeDouble(Operators.DivideObject(fgDtls.Rows[e.RowIndex].Cells[16].Value, fgDtls.Rows[e.RowIndex].Cells[14].Value).ToString()) != Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[15].Value.ToString()))
                                    {
                                        fgDtls.Rows[e.RowIndex].Cells[15].Value = (Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[16].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[14].Value.ToString()));
                                    }
                                }
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
        #endregion
    }
}
