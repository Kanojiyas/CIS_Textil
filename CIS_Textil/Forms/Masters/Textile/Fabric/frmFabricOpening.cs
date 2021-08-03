using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class frmFabricOpening : frmTrnsIface
    {
        private bool FAB_SERIALWISE;
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;
        private bool Vld_FOpening_WithoutRefID;
        private bool Vld_DupPieceNo;

        public frmFabricOpening()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
        }

        #region Event

        private void frmFabricOpening_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboDepartment, Combobox_Setup.ComboType.Mst_Department, "");
                Vld_FOpening_WithoutRefID = Localization.ParseBoolean(GlobalVariables.Vld_FOpening_WithoutRefID);
                Vld_DupPieceNo = Localization.ParseBoolean(GlobalVariables.Vld_DupPieceNo);
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                txtEntryNo.Enabled = false;

                FAB_SERIALWISE = Localization.ParseBoolean(GlobalVariables.FAB_SERIALWISE);

                if (FAB_SERIALWISE)
                {
                    fgDtls.Columns[4].Visible = true;
                    fgDtls.Columns[5].ReadOnly = true;
                    fgDtls.Columns[6].ReadOnly = true;
                    fgDtls.Columns[7].ReadOnly = true;
                }
                else
                {
                    fgDtls.Columns[4].Visible = false;
                    fgDtls.Columns[5].ReadOnly = false;
                    fgDtls.Columns[6].ReadOnly = false;
                    fgDtls.Columns[7].ReadOnly = false;
                }

                this.fgDtls.RowsAdded += new DataGridViewRowsAddedEventHandler(this.fgDtls_RowsAdded);
                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls.CellEndEdit += new DataGridViewCellEventHandler(this.fgDtls_CellEndEdit);
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
                DBValue.Return_DBValue(this, txtCode, "FabOpID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, dtOpDate, "FabOpDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboDepartment, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEd1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);
                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "FabOpID", Conversions.ToString(Localization.ParseNativeDouble(this.txtCode.Text)), base.dt_HasDtls_Grd, this.iIDentity, GlobalVariables.VALIDATE_EDIT, Db_Detials.CompID.ToString(), 1);

                if (Vld_FOpening_WithoutRefID)
                {
                    System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                    dgvCellStyle.BackColor = System.Drawing.Color.LightGray;
                    dgvCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
                    dgvCellStyle.SelectionBackColor = System.Drawing.Color.Purple;
                    dgvCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

                    //try
                    //{
                    //    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    //    {
                    //        if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_FabricInvoice2_FindDtls(" + Db_Detials.CompID + "," + Db_Detials.YearID + ") WHERE DepartmentID=" + cboDepartment.SelectedValue + "" + " AND BookDesignSrID=" + Localization.ParseNativeInt(fgDtls.Rows[i].Cells[4].Value.ToString()) + "")) > 0)
                    //        {
                    //            fgDtls.Rows[i].ReadOnly = true;
                    //            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle;
                    //        }
                    //        else
                    //        {
                    //            fgDtls.Rows[i].ReadOnly = false;
                    //        }
                    //    }
                    //}
                    //catch { }
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
                int MaxID = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(FabOpID),0) From {0}  Where IsDeleted=0 and  CompID = {1} and YearID = {2} and BranchID={3} and StoreID ={4}", "tbl_FabricOpeningMain", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.StoreID))));

                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where IsDeleted=0 and  FabOpID = {1} and CompID={2} and YearID={3} and BranchID={4} and StoreID ={5}", new object[] { "tbl_FabricOpeningMain", MaxID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.StoreID })))
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
                        fgDtls.Rows[0].Cells[3].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select  {0}({1},{2})", new object[] { "dbo.fn_FetchPieceNo_Stock", Db_Detials.CompID, Db_Detials.YearID })), Db_Detials.PCS_NO_INCMT);
                    }
                    else
                    {
                        fgDtls.Rows[0].Cells[3].Value = "-";
                    }
                }
                dtEntryDate.Focus();
            }
            catch (Exception ex3)
            {
                Navigate.logError(ex3.Message, ex3.StackTrace);
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
                (string.Format("{0:N0}", CommonCls.GetColSum(this.fgDtls, 10, -1, -1)).ToString().Replace(",", "")),
                (string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 15, -1, -1)).ToString().Replace(",", "")),
                (string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 0x12, -1, -1)).ToString().Replace(",", "")),
                ((txtDescription.Text.ToString() == ""? "-": txtDescription.Text.ToString())),
                (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                (dtEd1.TextFormat(false, true)),
                (txtET1.Text.Trim()),
                (txtET2.Text.Trim()),
                (txtET3.Text.Trim())
                };

                string strAdjQry = string.Empty;
                strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_AcLedger", "(#CodeID#)", base.iIDentity);
                strAdjQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", "(#CodeID#)", base.iIDentity);

                if (Localization.ParseNativeDouble(string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 0x12, -1, -1)).ToString()) > 0.0)
                {
                    int OpLedgerId = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format("Select LedgerId From {0} Where LedgerName = 'OPENING STOCK' ", "tbl_LedgerMaster"))));
                    strAdjQry = strAdjQry + DBSp.InsertInto_AcLedger("(#CodeID#)", "0", "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), cboDepartment.SelectedValue.ToString(), 2, Db_Detials.Ac_AdjType.OnAccount, "(#CodeID#)", txtEntryNo.Text.Trim(), dtOpDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), 0, Localization.ParseNativeDecimal(string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 0x12, -1, -1))), txtDescription.Text.Trim(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date)
                                          + DBSp.InsertInto_AcLedger("(#CodeID#)", "0", txtEntryNo.Text.ToString(), dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), OpLedgerId.ToString(), 1, Db_Detials.Ac_AdjType.OnAccount, "(#CodeID#)", txtEntryNo.Text.Trim(), dtOpDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDecimal(string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 0x12, -1, -1))), 0, txtDescription.Text.Trim(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                }

                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    {
                        if (Localization.ParseNativeDouble(Convert.ToString(row.Cells[15].Value)) != 0.0)
                        {
                            string LotNo = "";
                            if (row.Cells[2].Value != null || Convert.ToString(row.Cells[2].Value).Trim().Length > 0)
                            {
                                LotNo = Convert.ToString(row.Cells[2].Value);
                            }
                            else
                            {
                                LotNo = "-";
                            }
                            string barcodeNo = "";
                            if (row.Cells[3].Value != null || Convert.ToString(row.Cells[3].Value).Trim().Length > 0)
                            {
                                barcodeNo = Convert.ToString(row.Cells[3].Value);
                            }
                            else
                            {
                                barcodeNo = "-";
                            }
                            string OpDt = Localization.ToVBDateString(dtOpDate.Text.ToString());

                            OpDt = Convert.ToString(DateAndTime.DateAdd("d", -1.0, OpDt));
                            strAdjQry = strAdjQry + DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                        "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", OpDt, row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                        row.Cells[23].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[23].Value.ToString()), base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                        base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(), LotNo, barcodeNo, row.Cells[4].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[4].Value.ToString()),
                                        Localization.ParseNativeDouble(row.Cells[6].Value.ToString()), Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                        Localization.ParseNativeDouble(row.Cells[7].Value.ToString()), row.Cells[8].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[8].Value.ToString()),
                                        row.Cells[9].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[9].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[10].Value.ToString()),
                                        Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()), row.Cells[16].Value == null ? 0 : row.Cells[16].Value.ToString() == "" ? 0 : Localization.ParseNativeDecimal(row.Cells[16].Value.ToString()), 0, 0, 0,
                                        row.Cells[19].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[19].Value.ToString()), row.Cells[20].Value == null ? "0" : row.Cells[20].Value.ToString(), row.Cells[24].Value == null ? 0 : row.Cells[24].Value.ToString() == "" ? 0 : Localization.ParseNativeInt(row.Cells[24].Value.ToString()),
                                        row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()), "(#CodeID#)", 0, 0, 0,
                                        fgDtls.Rows[i].Cells[25].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[25].Value.ToString()),
                                        fgDtls.Rows[i].Cells[26].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[26].Value.ToString()),
                                        fgDtls.Rows[i].Cells[27].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[27].Value.ToString()),
                                        fgDtls.Rows[i].Cells[28].Value == null || fgDtls.Rows[i].Cells[28].Value.ToString() == "" || fgDtls.Rows[i].Cells[28].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(fgDtls.Rows[i].Cells[28].Value.ToString()),
                                        fgDtls.Rows[i].Cells[29].Value == null || fgDtls.Rows[i].Cells[29].Value.ToString() == "" || fgDtls.Rows[i].Cells[29].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(fgDtls.Rows[i].Cells[29].Value.ToString()),
                                        fgDtls.Rows[i].Cells[30].Value == null || fgDtls.Rows[i].Cells[30].Value.ToString() == "" ? "-" : fgDtls.Rows[i].Cells[30].Value.ToString(),
                                        fgDtls.Rows[i].Cells[31].Value == null || fgDtls.Rows[i].Cells[31].Value.ToString() == "" ? "-" : fgDtls.Rows[i].Cells[31].Value.ToString(),
                                        fgDtls.Rows[i].Cells[32].Value == null || fgDtls.Rows[i].Cells[32].Value.ToString() == "" ? "-" : fgDtls.Rows[i].Cells[32].Value.ToString(),
                                        fgDtls.Rows[i].Cells[33].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[33].Value.ToString()),
                                        fgDtls.Rows[i].Cells[34].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[34].Value.ToString()),
                                        "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
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

                if (!CommonCls.CheckDate(dtEntryDate.Text, true))
                    return true;

                if (!CommonCls.CheckDate(dtOpDate.Text, true))
                    return true;

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
                if (cboDepartment.SelectedValue == null || cboDepartment.Text.Trim().ToString() == "-" || cboDepartment.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    cboDepartment.Focus();
                    return true;
                }
                for (int i = 0; i <= fgDtls.RowCount -1; i++)
                {
                    fgDtls.Rows[i].Cells[22].Value = cboDepartment.SelectedValue;
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
                if (FAB_SERIALWISE)
                {
                    if (CheckSerialCombn())
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
                if (FAB_SERIALWISE)
                {
                    if (e.ColumnIndex == 4)
                    {
                        if (fgDtls.Rows[e.RowIndex].Cells[4].Value != null)
                        {
                            using (IDataReader dr = DB.GetRS("Select FabricDesignID,FabricQualityID,FabricShadeID from fn_FabricMaster_tbl() where FabricID=" + fgDtls.Rows[e.RowIndex].Cells[4].Value + ""))
                            {
                                while (dr.Read())
                                {
                                    fgDtls.Rows.Add();
                                    fgDtls.Rows[e.RowIndex].Cells[5].Value = Localization.ParseNativeInt(dr["FabricDesignID"].ToString());
                                    fgDtls.Rows[e.RowIndex].Cells[6].Value = Localization.ParseNativeInt(dr["FabricQualityID"].ToString());
                                    fgDtls.Rows[e.RowIndex].Cells[7].Value = Localization.ParseNativeInt(dr["FabricShadeID"].ToString());
                                }
                            }
                        }
                    }
                }

                if (e.ColumnIndex == 5)
                {
                    if (fgDtls.Rows[e.RowIndex].Cells[5].Value != null)
                    {
                        using (IDataReader dr = DB.GetRS("Select FabricQualityID,FabricShadeID from fn_FabricDesignMaster_tbl() where FabricDesignID=" + fgDtls.Rows[e.RowIndex].Cells[5].Value + ""))
                        {
                            while (dr.Read())
                            {
                                fgDtls.Rows[e.RowIndex].Cells[6].Value = Localization.ParseNativeInt(dr["FabricQualityID"].ToString());
                                //fgDtls.Rows[e.RowIndex].Cells[7].Value = Localization.ParseNativeInt(dr["FabricShadeID"].ToString());
                            }
                        }
                    }
                }

                if (!Vld_DupPieceNo)
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
                                    strTbleName = "tbl_StockFabricLedger";
                                    if (Navigate.CheckDuplicate(ref strTbleName, "BarCodeNo", primaryFieldNameValue, false, "", 0L, " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " and BranchID = " + Db_Detials.BranchID + " and StoreID=" + Db_Detials.StoreID, ""))
                                    {
                                        fgDtls.CurrentCell = fgDtls[2, e.RowIndex];
                                        //fgDtls.Rows[e.RowIndex].Cells[3].Value = "-";
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
                                    strTbleName = "tbl_StockFabricLedger";
                                    if (Navigate.CheckDuplicate(ref strTbleName, "BarCodeNo", fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString(), true, "TransID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " and BranchID = " + Db_Detials.BranchID + " and StoreID=" + Db_Detials.StoreID, ""))
                                    {
                                        //fgDtls.Rows[e.RowIndex].Cells[3].Value = "-";
                                        fgDtls.CurrentCell = fgDtls[2, e.RowIndex];
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


                if (Vld_DupPieceNo)
                {
                    if (e.ColumnIndex == 3)
                    {
                        string strTbleName;
                        if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                        {
                            string primaryFieldNameValue = fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString();
                            if (fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString().Length > 0)
                            {
                                //if (fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString() != "-")
                                {
                                    strTbleName = "tbl_StockFabricLedger";
                                    if (Navigate.CheckDuplicate(ref strTbleName, "BarCodeNo", primaryFieldNameValue, false, "", 0L, " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " and BranchID = " + Db_Detials.BranchID + " and StoreID=" + Db_Detials.StoreID, ""))
                                    {
                                        fgDtls.CurrentCell = fgDtls[2, e.RowIndex];
                                        //fgDtls.Rows[e.RowIndex].Cells[3].Value = "-";
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
                                //if (fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString() != "-")
                                {
                                    strTbleName = "tbl_StockFabricLedger";
                                    if (Navigate.CheckDuplicate(ref strTbleName, "BarCodeNo", fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString(), true, "TransID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " and BranchID = " + Db_Detials.BranchID + " and StoreID=" + Db_Detials.StoreID, ""))
                                    {
                                        //fgDtls.Rows[e.RowIndex].Cells[3].Value = "-";
                                        fgDtls.CurrentCell = fgDtls[2, e.RowIndex];
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
                if (((e.ColumnIndex == 4) | (e.ColumnIndex == 6)) && ((fgDtls.Rows[e.RowIndex].Cells[5].Value != null) && (fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString().Length > 0)))
                {
                    //fgDtls.Rows[e.RowIndex].Cells[5].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", fgDtls.Rows[e.RowIndex].Cells[4].Value)));
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
                        case 15:
                            fgDtls.Rows[e.RowIndex].Cells[18].Value = Math.Round(Localization.ParseNativeDouble(Operators.MultiplyObject(fgDtls.Rows[e.RowIndex].Cells[15].Value, fgDtls.Rows[e.RowIndex].Cells[17].Value).ToString()));
                            break;

                        case 17:
                            if (Conversions.ToBoolean(Operators.NotObject(Operators.CompareObjectEqual(Math.Round(Localization.ParseNativeDouble(Conversions.ToString(Operators.MultiplyObject(fgDtls.Rows[e.RowIndex].Cells[15].Value, fgDtls.Rows[e.RowIndex].Cells[17].Value)))), fgDtls.Rows[e.RowIndex].Cells[18].Value, false))))
                            {
                                fgDtls.Rows[e.RowIndex].Cells[18].Value = Math.Round(Localization.ParseNativeDouble(Conversions.ToString(Operators.MultiplyObject(fgDtls.Rows[e.RowIndex].Cells[15].Value, fgDtls.Rows[e.RowIndex].Cells[17].Value))));
                                fgDtls.Rows[e.RowIndex].Cells[19].Value = fgDtls.Rows[e.RowIndex].Cells[18].Value;
                            }
                            break;

                        case 18:
                            if (Localization.ParseNativeDouble(Conversions.ToString(Operators.DivideObject(fgDtls.Rows[e.RowIndex].Cells[18].Value, fgDtls.Rows[e.RowIndex].Cells[15].Value))) != Localization.ParseNativeDouble(Conversions.ToString(fgDtls.Rows[e.RowIndex].Cells[17].Value)))
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[17].Value != null)
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[17].Value = Localization.ParseNativeDouble(Conversions.ToString(Operators.DivideObject(fgDtls.Rows[e.RowIndex].Cells[18].Value, fgDtls.Rows[e.RowIndex].Cells[15].Value)));
                                }
                                else
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[17].Value = 0;
                                }
                            }
                            break;
                    }
                    if (((e.ColumnIndex == 4) | (e.ColumnIndex == 6)) && ((fgDtls.Rows[e.RowIndex].Cells[5].Value != null) && (Strings.Trim(Conversions.ToString(fgDtls.Rows[e.RowIndex].Cells[5].Value)).Length > 0)))
                    {
                        // fgDtls.Rows[e.RowIndex].Cells[5].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", fgDtls.Rows[e.RowIndex].Cells[4].Value)));
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
                        //int iCount = this.fgDtls.CurrentRow.Index + 1;
                        //if (iCount < fgDtls.Rows.Count)
                        //{
                        //    for (int i = 3; i <= (fgDtls.ColumnCount - 13); i++)
                        //    {
                        //        fgDtls.Rows[iCount].Cells[i].Value = fgDtls.Rows[iCount - 1].Cells[i].Value;
                        //    }
                        //}
                        fgDtls.CurrentCell = fgDtls[10, e.RowIndex];
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
                CIS_ReportTool.frmMultiPrint.TblNm = "tbl_FabricOpeningMain";
                CIS_ReportTool.frmMultiPrint.IdStr = "FabOpID";
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
                        string primaryFieldNameValue = fgDtls.Rows[i].Cells[3].Value.ToString();
                        if (fgDtls.Rows[i].Cells[3].Value.ToString() != null && fgDtls.Rows[i].Cells[3].Value.ToString().Length > 0)
                        {
                            //if (fgDtls.Rows[i].Cells[3].Value.ToString() != "-")
                            {
                                strTbleName = "tbl_StockFabricLedger";
                                if (Navigate.CheckDuplicate(ref strTbleName, "BarCodeNo", primaryFieldNameValue, false, "", 0L, " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " and BranchID = " + Db_Detials.BranchID + " and StoreID=" + Db_Detials.StoreID, ""))
                                {
                                    fgDtls.CurrentCell = fgDtls[3, i];
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
                        if (fgDtls.Rows[j].Cells[3].Value.ToString() != null && fgDtls.Rows[j].Cells[3].Value.ToString().Length > 0)
                        {
                            //if (fgDtls.Rows[j].Cells[3].Value.ToString() != "-")
                            {
                                strTbleName = "tbl_StockFabricLedger";
                                if (Navigate.CheckDuplicate(ref strTbleName, "BarCodeNo", fgDtls.Rows[j].Cells[3].Value.ToString(), true, "TransID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " and BranchID = " + Db_Detials.BranchID + " and StoreID=" + Db_Detials.StoreID, ""))
                                {
                                    fgDtls.CurrentCell = fgDtls[3, j];
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

        private bool CheckSerialCombn()
        {
            int iDesignID, iQualityID, iShadeID;
            for (int i = 0; i <= fgDtls.RowCount - 1; i++)
            {
                if (fgDtls.Rows[i].Cells[4].Value != null && fgDtls.Rows[i].Cells[4].Value.ToString() != "0" && fgDtls.Rows[i].Cells[4].Value.ToString() != "-")
                {
                    using (IDataReader dr = DB.GetRS("Select FabricDesignID,FabricQualityID,FabricShadeID from fn_FabricMaster_tbl() where FabricID=" + fgDtls.Rows[i].Cells[4].Value + ""))
                    {
                        while (dr.Read())
                        {
                            iDesignID = Localization.ParseNativeInt(dr["FabricDesignID"].ToString());
                            iQualityID = Localization.ParseNativeInt(dr["FabricQualityID"].ToString());
                            iShadeID = Localization.ParseNativeInt(dr["FabricShadeID"].ToString());
                            if (iDesignID != Localization.ParseNativeInt(fgDtls.Rows[i].Cells[5].Value.ToString()) || iQualityID != Localization.ParseNativeInt(fgDtls.Rows[i].Cells[6].Value.ToString()) || iShadeID != Localization.ParseNativeInt(fgDtls.Rows[i].Cells[7].Value.ToString()))
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Check Fabric Combination");
                                fgDtls.CurrentCell = fgDtls[4, i];
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Fabric");
                    fgDtls.CurrentCell = fgDtls[4, i];
                    return true;
                }
            }
            return false;
        }
    }
}
