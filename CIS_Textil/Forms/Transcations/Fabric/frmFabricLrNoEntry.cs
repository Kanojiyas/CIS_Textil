using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_DBLayer;
using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using CIS_CLibrary;

namespace CIS_Textil
{
    public partial class frmFabricLrNoEntry : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        public frmFabricLrNoEntry()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
        }

        private void frmFabricLrNoEntry_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboVoucherType, Combobox_Setup.ComboType.Mst_LrUpdForms, "");
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                txtEntryNo.Enabled = false;
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
                dtEntryDate.Text = Localization.ToVBDateString(DateTime.Now.ToString());
                dtEntryDate.Focus();
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
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
                DBValue.Return_DBValue(this, txtCode, "PendingLrID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboVoucherType, "VoucherTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);

                DetailGrid_Setup.FillGrid(fgDtls, fgDtls.Grid_UID, fgDtls.Grid_Tbl, "PendingLrID", txtCode.Text, base.dt_HasDtls_Grd);

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                }

                System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                dgvCellStyle.BackColor = System.Drawing.Color.LightGray;
                dgvCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
                dgvCellStyle.SelectionBackColor = System.Drawing.Color.Purple;
                dgvCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

                //try
                //{
                //    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                //    {
                //        if ((Localization.ParseNativeInt(DB.GetSnglValue("SELECT COUNT(0) from fn_FabricReceipt_FindDtls(" + Db_Detials.CompID + "," + Db_Detials.YearID + ") WHERE LotNo=" + Localization.ParseNativeInt(fgDtls.Rows[i].Cells[3].Value.ToString()) + "")) > 0))
                //        {
                //            fgDtls.Rows[i].ReadOnly = true;
                //            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle;
                //        }
                //        else
                //            fgDtls.Rows[i].ReadOnly = false;
                //    }
                //}
                //catch { }
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
                (txtEntryNo.Text.Trim()),
                (dtEntryDate.TextFormat(false, true)),
                (cboVoucherType.SelectedValue),
                (txtDescription.Text.ToString() == "" ? "-" : txtDescription.Text.ToString())
                };

                string strAdjQry = string.Empty;
                if (cboVoucherType.SelectedValue != null)
                {
                    switch (Localization.ParseNativeInt(cboVoucherType.SelectedValue.ToString()))
                    {
                        //Sales Bill
                        case 366:
                            for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                            {
                                if (fgDtls.Rows[i].Cells[3].Value != null && fgDtls.Rows[i].Cells[3].Value.ToString().Length > 0 && fgDtls.Rows[i].Cells[3].Value.ToString() != "0" && fgDtls.Rows[i].Cells[3].Value.ToString() != "")
                                {
                                    strAdjQry += "Update tbl_FabricSalesMain Set LrNo=" + CommonLogic.SQuote(fgDtls.Rows[i].Cells[3].Value.ToString()) + " Where FabricSalesID= " + fgDtls.Rows[i].Cells[2].Value + ";";
                                }
                            }
                            break;

                        //Catalog Sales
                        case 408:
                            for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                            {
                                if (fgDtls.Rows[i].Cells[3].Value != null && fgDtls.Rows[i].Cells[3].Value.ToString().Length > 0 && fgDtls.Rows[i].Cells[3].Value.ToString() != "0" && fgDtls.Rows[i].Cells[3].Value.ToString() != "")
                                {
                                    strAdjQry += "Update tbl_CatalogSalesMain Set LrNo=" + CommonLogic.SQuote(fgDtls.Rows[i].Cells[3].Value.ToString()) + " Where CatSalesID= " + fgDtls.Rows[i].Cells[2].Value + ";";
                                }
                            }
                            break;

                        ////Fabric Sales Serial
                        //case 473:
                        //    for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                        //    {
                        //        if (fgDtls.Rows[i].Cells[3].Value != null && fgDtls.Rows[i].Cells[3].Value.ToString().Length > 0 && fgDtls.Rows[i].Cells[3].Value.ToString() != "0" && fgDtls.Rows[i].Cells[3].Value.ToString() != "")
                        //        {
                        //            strAdjQry += "Update tbl_FabricInvoiceMain2 Set LrNo=" + CommonLogic.SQuote(fgDtls.Rows[i].Cells[3].Value.ToString()) + " Where InvoiceID= " + fgDtls.Rows[i].Cells[2].Value + ";";
                        //        }
                        //    }
                        //    break;

                        ////Fabric Sales Roll
                        //case 511:
                        //    for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                        //    {
                        //        if (fgDtls.Rows[i].Cells[3].Value != null && fgDtls.Rows[i].Cells[3].Value.ToString().Length > 0 && fgDtls.Rows[i].Cells[3].Value.ToString() != "0" && fgDtls.Rows[i].Cells[3].Value.ToString() != "")
                        //        {
                        //            strAdjQry += "Update tbl_FabricInvoiceMain_Roll Set LrNo=" + CommonLogic.SQuote(fgDtls.Rows[i].Cells[3].Value.ToString()) + " Where InvoiceID= " + fgDtls.Rows[i].Cells[2].Value + ";";
                        //        }
                        //    }
                        //    break;
                    }
                }
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                double dblTransID = 0;
                DBSp.Transcation_AddEdit_Trans(pArrayData, fgDtls, true, ref dblTransID, strAdjQry, "", txtEntryNo.Text, "", "");
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
                if (!Information.IsDate(Strings.Trim(dtEntryDate.Text.ToString())))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry Date");
                    dtEntryDate.Focus();
                    return true;
                }
                if (cboVoucherType.SelectedValue == null || cboVoucherType.Text.Trim().ToString() == "-" || cboVoucherType.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Voucher");
                    cboVoucherType.Focus();
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

        private void btnFetchLr_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboVoucherType.SelectedValue == null || cboVoucherType.Text.Trim().ToString() == "-" || cboVoucherType.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Voucher");
                    cboVoucherType.Focus();
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
                #endregion

                frmStockAdj frmStockAdj = new frmStockAdj();
                frmStockAdj.MenuID = base.iIDentity;
                frmStockAdj.ref_fgDtls = this.fgDtls;
                frmStockAdj.ReturnType = cboVoucherType.SelectedValue.ToString();

                strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}, {6})", new object[] { strQry_ColName, snglValue, Db_Detials.StoreID, Db_Detials.CompID,  Db_Detials.BranchID, Db_Detials.YearID, cboVoucherType.SelectedValue.ToString() });

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
                this.fgDtls.Select();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Error Occured While Fetching Lr's");
            }
        }

        protected void fgDtls_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string SQry = "";
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
                            SQry = DB.GetSnglValue("SELECT COUNT(0) from fn_FabricReceipt_FindDtls(" + Db_Detials.CompID + "," + Db_Detials.YearID + ") WHERE LotNo=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[3].Value.ToString()));

                            if (SQry != "" && SQry != "0")
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityError, "Referance Found", "Reference Found In Another Module..Row Cannot Be Deleted");
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
                                            iCalcCol = Localization.ParseNativeInt(dtRow_AryCalcvalue[i]["ColOrder"].ToString());
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
                                        iCalcCol = Localization.ParseNativeInt(dtRow_AryCalcvalue[i]["ColOrder"].ToString());
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
    }
}
