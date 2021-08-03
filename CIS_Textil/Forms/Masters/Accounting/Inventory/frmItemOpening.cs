using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.Collections;
using System.Diagnostics;

namespace CIS_Textil
{
    public partial class frmItemOpening : frmMasterIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        private bool Vld_DupPieceNo;

        public frmItemOpening()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
        }

        #region Event

        private void frmItemOpening_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboDepartment, Combobox_Setup.ComboType.Mst_Ledger, "");
                Vld_DupPieceNo = Localization.ParseBoolean(GlobalVariables.Vld_DupPieceNo);
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls.RowsAdded += new DataGridViewRowsAddedEventHandler(this.fgDtls_RowsAdded);
                this.fgDtls.CellEndEdit += new DataGridViewCellEventHandler(this.fgDtls_CellEndEdit);
                txtEntryNo.Enabled = false;
            }
            catch (Exception ex1)
            {
                Navigate.logError(ex1.Message, ex1.StackTrace);
            }
        }

        #endregion

        #region Navigation

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "ItemOpID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, dtOpeningDate, "OpDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboDepartment, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, TxtTotalPiece, "TotPcs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, TxtTotWeight, "TotQty", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, TxtTotAmt, "GrossAmount", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);
                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "ItemOpID", Conversions.ToString(Localization.ParseNativeDouble(this.txtCode.Text)), base.dt_HasDtls_Grd, this.iIDentity, GlobalVariables.VALIDATE_EDIT, Db_Detials.CompID.ToString(), 1);
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
                        if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_StockItemLedger() WHERE RefID<>'' AND RefID=" + CommonLogic.SQuote(fgDtls.Rows[i].Cells["RefID"].Value.ToString()) + (Db_Detials.CompID.ToString() != "" ? " and CompID=" + Db_Detials.CompID : "") + "")) > 1)
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
            catch (Exception ex2)
            {
                Navigate.logError(ex2.Message, ex2.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex2.Message);
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
                int MaxID = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(ItemOpID),0) From {0}  Where IsDeleted=0 and  CompID = {1} and YearID = {2}", "tbl_ItemOpeningMain", Db_Detials.CompID, Db_Detials.YearID))));

                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where IsDeleted=0 and  ItemOpID = {1} and CompID={2} and YearID={3}", new object[] { "tbl_ItemOpeningMain", MaxID, Db_Detials.CompID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                        cboDepartment.SelectedValue = Localization.ParseNativeInt(reader["DepartmentID"].ToString());
                    }
                }

                if (fgDtls.Rows.Count > 0)
                {
                    if (fgDtls.Columns[2].Visible)
                    {
                        fgDtls.Rows[0].Cells[2].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select  {0}({1},{2})", new object[] { "dbo.fn_FetchPieceNo_ItemStock", Db_Detials.CompID, Db_Detials.YearID })), Db_Detials.PCS_NO_INCMT);
                    }
                    else
                    {
                        fgDtls.Rows[0].Cells[2].Value = "-";
                    }
                }

                dtEntryDate.Focus();
                TxtTotalPiece.Text = "0";
                TxtTotWeight.Text = "0.00";
            }
            catch (Exception ex3)
            {
                Navigate.logError(ex3.Message, ex3.StackTrace);
            }
        }

        public void SaveRecord()
        {
            string sTotQty = "", sTotWeight = "", sTotAmt = "";
            try
            {
                sTotQty = string.Format("{0:N0}", CommonCls.GetColSum(this.fgDtls, 7, -1, -1));
                sTotWeight = string.Format("{0:N3}", CommonCls.GetColSum(this.fgDtls, 10, -1, -1));
                sTotAmt = string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 12, -1, -1));
                ArrayList pArrayData = new ArrayList
                {
                this.frmVoucherTypeID,
                ("(#ENTRYNO#)"),
                (dtEntryDate.TextFormat(false, true)),
                (dtOpeningDate.TextFormat(false, true)),
                (cboDepartment.SelectedValue),
                (TxtTotalPiece.Text.ToString().Replace(",", "")),  
                (TxtTotWeight.Text.ToString().Replace(",", "")),
                (TxtTotAmt.Text.ToString().Replace(",", "")),
                ((txtDescription.Text.ToString() == ""? "-": txtDescription.Text.ToString()))
                };

                string strAdjQry = string.Empty;
                strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_AcLedger", "(#CodeID#)", base.iIDentity);
                strAdjQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockItemLedger", "(#CodeID#)", base.iIDentity);

                if (Localization.ParseNativeDouble(TxtTotAmt.Text.ToString()) > 0.0)
                {
                    int OpLedgerId = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format("Select LedgerId From {0} Where LedgerName = 'OPENING STOCK' ", "tbl_LedgerMaster"))));
                    strAdjQry = strAdjQry + DBSp.InsertInto_AcLedger("(#CodeID#)", "0", "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), cboDepartment.SelectedValue.ToString(), 2, Db_Detials.Ac_AdjType.OnAccount, "(#CodeID#)", txtEntryNo.Text.Trim(), dtOpeningDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), 0, Localization.ParseNativeDecimal(TxtTotAmt.Text), txtDescription.Text.Trim(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date) + DBSp.InsertInto_AcLedger("(#CodeID#)", "0", txtEntryNo.Text.ToString(), dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), OpLedgerId.ToString(), 1, Db_Detials.Ac_AdjType.OnAccount, "(#CodeID#)", txtEntryNo.Text.Trim(), dtOpeningDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDecimal(TxtTotAmt.Text), 0, txtDescription.Text.Trim(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                }

                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    {
                        if (Localization.ParseNativeDouble(row.Cells[10].Value.ToString()) != 0.0)
                        {
                            string LotNo = "";
                            if (row.Cells[4].Value != null && row.Cells[4].Value.ToString().Length > 0)
                            {
                                LotNo = row.Cells[4].Value.ToString();
                            }
                            else
                            {
                                LotNo = "-";
                            }
                            string batchNo = "";
                            if (row.Cells[2].Value != null && row.Cells[2].Value.ToString().Length > 0)
                            {
                                batchNo = row.Cells[2].Value.ToString();
                            }
                            else
                            {
                                batchNo = "-";
                            }
                            string OpDt = Localization.ToVBDateString(dtOpeningDate.Text.ToString());
                            OpDt = DateAndTime.DateAdd("d", -1.0, OpDt).ToString();
                            strAdjQry = strAdjQry + DBSp.InsertIntoItemStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)", (i + 1).ToString(),
                                        "(#ENTRYNO#)", dtEntryDate.Text.Trim(),
                                        row.Cells[17].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[17].Value.ToString()),
                                        row.Cells[16].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[16].Value.ToString()),
                                        base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                        base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(), 
                                        batchNo, OpDt,
                                        row.Cells[3].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[3].Value.ToString()),
                                        row.Cells[5].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                        row.Cells[6].Value == null ? 0 : Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                                        row.Cells[7].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[7].Value.ToString()),
                                        row.Cells[10].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()),
                                        row.Cells[11].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                        row.Cells[12].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()),
                                        0, 0, 0, 0, row.Cells[13].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[13].Value.ToString()),
                                        row.Cells[14].Value == null ? "" : row.Cells[14].Value.ToString(),
                                        LotNo, row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                        "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, 
                                        Db_Detials.UserID, DateAndTime.Now.Date.ToString());
                        }
                        row = null;
                    }
                }
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                DBSp.Transcation_AddEdit(pArrayData, fgDtls, true, strAdjQry, "", txtEntryNo.Text, "", "");
            }
            catch (Exception ex4)
            {
                Navigate.logError(ex4.Message, ex4.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex4.Message);
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
                if (!Information.IsDate(dtOpeningDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Opening Date");
                    dtOpeningDate.Focus();
                    return true;
                }
                if (cboDepartment.SelectedValue == null || cboDepartment.Text.Trim().ToString() == "-" || cboDepartment.SelectedValue.ToString() == "0")
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
                if (Vld_DupPieceNo)
                {
                    if (CheckDupPieceNo())
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex5)
            {
                Navigate.logError(ex5.Message, ex5.StackTrace);
                return false;
            }
        }

        #endregion

        private void fgDtls_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    if (fgDtls.Rows[e.RowIndex].Cells[3].Value != null)
                    {
                        using (IDataReader dr = DB.GetRS("Select UnitID from fn_ItemMaster_tbl() where ItemID=" + fgDtls.Rows[e.RowIndex].Cells[3].Value + ""))
                        {
                            while (dr.Read())
                            {
                                fgDtls.Rows[e.RowIndex].Cells[6].Value = Localization.ParseNativeInt(dr["UnitID"].ToString());
                            }
                        }
                    }
                }

                if (!Vld_DupPieceNo)
                {
                    if (e.ColumnIndex == 2)
                    {
                        string strTbleName;
                        if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                        {
                            string primaryFieldNameValue = fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString();
                            if (fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString().Length > 0)
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString() != "-")
                                {
                                    strTbleName = "fn_StockItemLedger_Tbl()";
                                    if (Navigate.CheckDuplicate(ref strTbleName, "BatchNo", primaryFieldNameValue, false, "", 0L, " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "", ""))
                                    {
                                        fgDtls.CurrentCell = fgDtls[2, e.RowIndex];
                                    }
                                }
                            }
                            else if (fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString().Length <= 0)
                            {
                                fgDtls.Rows[e.RowIndex].Cells[2].Value = "-";
                            }
                        }
                        else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                        {
                            if (fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString().Length > 0)
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString() != "-")
                                {
                                    strTbleName = "fn_StockItemLedger_Tbl()";
                                    if (Navigate.CheckDuplicate(ref strTbleName, "BatchNo", fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString(), true, "TransID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "", ""))
                                    {
                                        fgDtls.CurrentCell = fgDtls[2, e.RowIndex];
                                    }
                                }
                            }
                            else if (fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString().Length <= 0)
                            {
                                fgDtls.Rows[e.RowIndex].Cells[2].Value = "-";
                            }
                        }
                    }
                }

                if (Vld_DupPieceNo)
                {
                    if (e.ColumnIndex == 2)
                    {
                        string strTbleName;
                        if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                        {
                            string primaryFieldNameValue = fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString();
                            if (fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString().Length > 0)
                            {
                                //if (fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString() != "-")
                                {
                                    strTbleName = "fn_StockItemLedger_Tbl()";
                                    if (Navigate.CheckDuplicate(ref strTbleName, "BatchNo", primaryFieldNameValue, false, "", 0L, " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "", ""))
                                    {
                                        fgDtls.CurrentCell = fgDtls[2, e.RowIndex];
                                    }
                                }
                            }
                            else if (fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString().Length <= 0)
                            {
                                fgDtls.Rows[e.RowIndex].Cells[2].Value = "-";
                            }
                        }
                        else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                        {
                            if (fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString().Length > 0)
                            {
                                //if (fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString() != "-")
                                {
                                    strTbleName = "fn_StockItemLedger_Tbl()";
                                    if (Navigate.CheckDuplicate(ref strTbleName, "BatchNo", fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString(), true, "TransID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "", ""))
                                    {
                                        //fgDtls.Rows[e.RowIndex].Cells[2].Value = "-";
                                        fgDtls.CurrentCell = fgDtls[2, e.RowIndex];
                                    }
                                }
                            }
                            else if (fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString().Length <= 0)
                            {
                                fgDtls.Rows[e.RowIndex].Cells[2].Value = "-";
                            }
                        }
                    }
                }
            }
            catch (Exception ex7)
            {
                Navigate.logError(ex7.Message, ex7.StackTrace);
            }
        }

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!((base.blnFormAction == Enum_Define.ActionType.View_Record) | (base.blnFormAction == Enum_Define.ActionType.Not_Active)))
                {
                    switch (e.ColumnIndex)
                    {

                        case 7:
                            fgDtls.Rows[e.RowIndex].Cells[12].Value = Math.Round(Localization.ParseNativeDouble(Operators.MultiplyObject(fgDtls.Rows[e.RowIndex].Cells[7].Value, fgDtls.Rows[e.RowIndex].Cells[11].Value).ToString()));
                            break;

                        case 11:
                            if (Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(Math.Round(Localization.ParseNativeDouble(Conversions.ToString(Operators.MultiplyObject(fgDtls.Rows[e.RowIndex].Cells[7].Value, fgDtls.Rows[e.RowIndex].Cells[11].Value)))), fgDtls.Rows[e.RowIndex].Cells[12].Value, false))))
                            {
                                fgDtls.Rows[e.RowIndex].Cells[12].Value = Math.Round(Localization.ParseNativeDouble(Conversions.ToString(Operators.MultiplyObject(fgDtls.Rows[e.RowIndex].Cells[7].Value, fgDtls.Rows[e.RowIndex].Cells[11].Value))));
                            }
                            break;

                        case 12:
                            if (Localization.ParseNativeDouble(Conversions.ToString(Operators.DivideObject(fgDtls.Rows[e.RowIndex].Cells[12].Value, fgDtls.Rows[e.RowIndex].Cells[7].Value))) != Localization.ParseNativeDouble(Conversions.ToString(fgDtls.Rows[e.RowIndex].Cells[11].Value)))
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[11].Value != null)
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[11].Value = Localization.ParseNativeDouble(Conversions.ToString(Operators.DivideObject(fgDtls.Rows[e.RowIndex].Cells[12].Value, fgDtls.Rows[e.RowIndex].Cells[7].Value)));
                                }
                                else
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[11].Value = 0;
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex8)
            {
                Navigate.logError(ex8.Message, ex8.StackTrace);
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
                        fgDtls.CurrentCell = fgDtls[10, e.RowIndex];
                        if (fgDtls.Rows[e.RowIndex - 1].Cells[2].Value.ToString().Trim() != "-")
                        {
                            fgDtls.Rows[e.RowIndex].Cells[2].Value = CommonCls.AutoInc_Runtime(fgDtls.Rows[e.RowIndex - 1].Cells[2].Value.ToString(), Db_Detials.PCS_NO_INCMT);
                        }
                        else
                        {
                            fgDtls.Rows[e.RowIndex].Cells[2].Value = "-";
                        }
                    }
                }
            }
            catch (Exception ex9)
            {
                Navigate.logError(ex9.Message, ex9.StackTrace);
            }
        }

        public void PrintRecord()
        {
            try
            {
                CIS_ReportTool.frmMultiPrint frmMultiPrint = new CIS_ReportTool.frmMultiPrint();
                CIS_ReportTool.frmMultiPrint.MenuID = base.iIDentity;
                CIS_ReportTool.frmMultiPrint.Id = Localization.ParseNativeInt(this.txtCode.Text);
                CIS_ReportTool.frmMultiPrint.TblNm = "tbl_ItemOpeningMain";
                CIS_ReportTool.frmMultiPrint.IdStr = "ItemOpID";
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
            catch (Exception ex10)
            {
                Navigate.logError(ex10.Message, ex10.StackTrace);
            }
        }

        private bool CheckDupPieceNo()
        {
            string strTbleName;
            if (Vld_DupPieceNo)
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                    {
                        string primaryFieldNameValue = fgDtls.Rows[i].Cells[2].Value.ToString();
                        if (fgDtls.Rows[i].Cells[2].Value.ToString() != null && fgDtls.Rows[i].Cells[2].Value.ToString().Length > 0)
                        {
                            //if (fgDtls.Rows[i].Cells[2].Value.ToString() != "-")
                            {
                                strTbleName = "tbl_StockItemLedger";
                                if (Navigate.CheckDuplicate(ref strTbleName, "BatchNo", primaryFieldNameValue, false, "", 0L, " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "", ""))
                                {
                                    fgDtls.CurrentCell = fgDtls[2, i];
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                }
                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    for (int j = 0; j <= fgDtls.RowCount - 1; j++)
                    {
                        if (fgDtls.Rows[j].Cells[2].Value.ToString() != null && fgDtls.Rows[j].Cells[2].Value.ToString().Length > 0)
                        {
                            //if (fgDtls.Rows[j].Cells[2].Value.ToString() != "-")
                            {
                                strTbleName = "tbl_StockItemLedger";
                                if (Navigate.CheckDuplicate(ref strTbleName, "BatchNo", fgDtls.Rows[j].Cells[2].Value.ToString(), true, "TransID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "", ""))
                                {
                                    fgDtls.CurrentCell = fgDtls[2, j];
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                }
                return false;
            }
            return false;
        }
    }
}
