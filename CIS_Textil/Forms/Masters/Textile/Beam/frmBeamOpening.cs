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
    public partial class frmBeamOpening : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        private bool IsValidate = false;

        public frmBeamOpening()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
        }

        #region Form Event
        private void frmBeamOpening_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboDepartment, Combobox_Setup.ComboType.Mst_WeaverNDepartment, "");
                Combobox_Setup.FillCbo(ref cboBDesign, Combobox_Setup.ComboType.Mst_BeamDesign, "");
                Combobox_Setup.FillCbo(ref cboLoomNo, Combobox_Setup.ComboType.Mst_Loom, "");
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                txtEntryNo.Enabled = false;

                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    FillControls();
                }

                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls.CellParsing += new DataGridViewCellParsingEventHandler(this.fgDtls_CellParsing);
                this.fgDtls.KeyDown += new KeyEventHandler(this.fgDtls_KeyDown);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region Form Navigation

        public bool ValidateForm()
        {
            try
            {
                if (!EventHandles.IsValidGridReq(fgDtls, base.dt_AryIsRequired))
                {
                    return true;
                }
                if ((!EventHandles.IsRequiredInGrid(fgDtls, this.dt_AryIsRequired, false)))
                {
                    return true;
                }

                if (!Information.IsDate(Strings.Trim(dtEntryDate.Text.ToString())))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry Date");
                    dtEntryDate.Focus();
                    return true;
                }

                if (!Information.IsDate(Strings.Trim(dtOpeingDate.Text.ToString())))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Opening Date");
                    dtEntryDate.Focus();
                    return true;
                }

                if (txtEntryNo.Text.Trim() == "" || txtEntryNo.Text.Trim() == "-" || txtEntryNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry No.");
                    return true;
                }

                if (cboDepartment.SelectedValue == null || cboDepartment.SelectedValue.ToString() == "-" || cboDepartment.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    cboDepartment.Focus();
                    return true;
                }

                if (cboBDesign.SelectedValue == null || cboBDesign.SelectedValue.ToString() == "-" || cboBDesign.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Beam Design No.");
                    cboBDesign.Focus();
                    return true;
                }

                if (txtBeamNo.Text.Trim() == "" || txtBeamNo.Text.Trim() == "-" || txtBeamNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Beam No.");
                    txtBeamNo.Focus();
                    return true;
                }

                string strTable = "";
                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    strTable = "tbl_StockBeamLedger";
                    if (Navigate.CheckDuplicate(ref strTable, "BatchNo", this.txtBeamNo.Text, false, "", 0L, string.Format("CompID = {0} And YearID = {1}", Db_Detials.CompID, Db_Detials.YearID), "This Beam No is already Used"))
                    {
                        this.txtBeamNo.Text = "";
                        this.txtBeamNo.Focus();
                        return true;
                    }
                }

                if (txtCuts.Text.Trim() == "" || txtCuts.Text.Trim() == "-" || txtCuts.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Cuts");
                    txtCuts.Focus();
                    return true;
                }
                if (txtMtrs.Text.Trim() == "" || txtMtrs.Text.Trim() == "-" || txtMtrs.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Meters");
                    txtMtrs.Focus();
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

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "BeamOpeningID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, dtOpeingDate, "OpeningDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboDepartment, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtBeamNo, "BeamNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtSetNo, "SetNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBDesign, "BeamDesignID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboLoomNo, "LoomID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEnds, "Ends", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtTpLen, "Taplen", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCuts, "Cuts", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMtrs, "Mtrs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtNarration, "Narration", Enum_Define.ValidationType.Text);
                DetailGrid_Setup.FillGrid(fgDtls, fgDtls.Grid_UID, fgDtls.Grid_Tbl, "BeamOpeningID", Localization.ParseNativeDouble(txtCode.Text).ToString(), base.dt_HasDtls_Grd);

                IsValidate = false;
                try
                {
                    System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                    dgvCellStyle.BackColor = System.Drawing.Color.LightGray;
                    dgvCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
                    dgvCellStyle.SelectionBackColor = System.Drawing.Color.Purple;
                    dgvCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

                    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    {
                        if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_StockBeamLedger() WHERE RefID<>'' AND RefID=" + CommonLogic.SQuote(fgDtls.Rows[i].Cells["RefID"].Value.ToString()) + (Db_Detials.CompID.ToString() != "" ? " and CompID=" + Db_Detials.CompID : "") + "")) > 1)
                        {
                            fgDtls.Rows[i].ReadOnly = true;
                            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle;
                            IsValidate = true;
                        }
                        else
                        {
                            fgDtls.Rows[i].ReadOnly = false;
                        }
                    }
                    if (IsValidate)
                    {
                        txtBeamNo.Enabled = false;
                        cboDepartment.Enabled = false;
                        cboBDesign.Enabled = false;
                        cboLoomNo.Enabled = false;
                        txtEnds.Enabled = false;
                        txtTpLen.Enabled = false;
                        txtCuts.Enabled = false;
                        txtMtrs.Enabled = false;
                    }
                    else
                    {
                        txtBeamNo.Enabled = true;
                        cboDepartment.Enabled = true;
                        cboBDesign.Enabled = true;
                        cboLoomNo.Enabled = true;
                        txtEnds.Enabled = true;
                        txtTpLen.Enabled = true;
                        txtCuts.Enabled = true;
                        txtMtrs.Enabled = true;
                    }
                }
                catch (Exception ex1)
                {
                    Navigate.logError(ex1.Message, ex1.StackTrace);
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                dtEntryDate.Focus();
                int iMaxID = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format("Select Isnull(Max(BeamOpeningID),0) From {0}  Where CompID={1} and YearID={2}", "tbl_BeamOpeningMain", Db_Detials.CompID, Db_Detials.YearID))));
                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where BeamOpeningID = {1} and CompID={2} and YearID={3}", "tbl_BeamOpeningMain", iMaxID, Db_Detials.CompID, Db_Detials.YearID)))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                        dtOpeingDate.Text = Localization.ToVBDateString(reader["OpeningDate"].ToString());
                        cboDepartment.SelectedValue = Localization.ParseNativeInt(reader["DepartmentID"].ToString());
                        cboBDesign.SelectedValue = Localization.ParseNativeInt(reader["BeamDesignID"].ToString());
                    }
                }
                cboBDesign_LostFocus(null, null);
                txtBeamNo.Enabled = true;
                cboDepartment.Enabled = true;
                cboBDesign.Enabled = true;
                cboLoomNo.Enabled = true;
                txtEnds.Enabled = true;
                txtTpLen.Enabled = true;
                txtCuts.Enabled = true;
                txtMtrs.Enabled = true;
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
                    "(#ENTRYNO#)",
                    dtEntryDate.TextFormat(false, true),
                    dtOpeingDate.TextFormat(false, true),
                    cboDepartment.SelectedValue,
                    txtBeamNo.Text.ToString(),
                    txtSetNo.Text.ToString(),
                    cboBDesign.SelectedValue,
                    cboLoomNo.SelectedValue,
                    Strings.Replace(txtEnds.Text.ToString(), ",", "", 1, -1,   CompareMethod.Binary),
                    Strings.Replace(txtTpLen.Text.ToString(), ",", "", 1, -1, CompareMethod.Binary),
                    Strings.Replace(txtCuts.Text.ToString(), ",", "", 1, -1, CompareMethod.Binary),
                    Strings.Replace(txtMtrs.Text.ToString(), ",", "", 1, -1, CompareMethod.Binary),
                    Strings.Replace(string.Format("{0:N3}", CommonCls.GetColSum(fgDtls, 8, -1, -1)).ToString(), ",", "", 1, -1, CompareMethod.Binary),
                    txtNarration.Text.ToString()
                };

                string strQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockBeamLedger", "(#CodeID#)", Localization.ParseNativeInt(base.iIDentity.ToString()));
                if (Localization.ParseNativeDouble(string.Format("{0:N3}", CommonCls.GetColSum(fgDtls, 8, -1, -1)).ToString()) > 0.0)
                {
                    strQry = strQry + DBSp.InsertIntoBeamStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)", "1", "(#ENTRYNO#)",
                        Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()), Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                        base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + "(#ENTRYNO#)", base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + "(#ENTRYNO#)",  
                        cboLoomNo.SelectedValue == null ? 0 : cboLoomNo.SelectedValue.ToString() == "" ? 0 : Localization.ParseNativeInt(cboLoomNo.SelectedValue.ToString()),
                        txtBeamNo.Text.ToString(), dtEntryDate.Text, Localization.ParseNativeDouble(cboBDesign.SelectedValue.ToString()),
                        Localization.ParseNativeDecimal(txtCuts.Text.ToString()),
                        Localization.ParseNativeDecimal(txtMtrs.Text.ToString()),
                        Localization.ParseNativeDecimal(string.Format("{0:N3}", CommonCls.GetColSum(fgDtls, 8, -1, -1)).ToString()), decimal.Zero, decimal.Zero, decimal.Zero, 0, txtNarration.Text.Trim(),
                        0, 0, "(#CodeID#)", 0, 0, 0, "null", Localization.ParseNativeInt("(#ENTRYNO#)"), 1,
                        Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                }

                strQry = strQry.Replace("'null'", "null").Replace("Nnull", "null");
                DBSp.Transcation_AddEdit(pArrayData, fgDtls, true, strQry, "", txtEntryNo.Text, "", "");
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        #endregion

        #region Form Validation

        private void cboBDesign_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (Localization.ParseNativeDouble(cboBDesign.SelectedValue.ToString()) > 0.0)
                {
                    using (IDataReader reader = DB.GetRS(string.Format("Select * From {0} Where BeamDesignID = {1} ", "tbl_BeamDesignDtls", Conversion.Val(RuntimeHelpers.GetObjectValue(this.cboBDesign.SelectedValue)))))
                    {
                        fgDtls.Rows.Clear();
                        while (reader.Read())
                        {
                            fgDtls.Rows.Add();
                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[1].Value = fgDtls.Rows.Count;
                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[2].Value = Localization.ParseNativeInt(reader["YarnID"].ToString());
                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[3].Value = Localization.ParseNativeInt(reader["ColorID"].ToString());
                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[4].Value = Localization.ParseNativeInt(reader["ShadeID"].ToString());
                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[5].Value = reader["Count"].ToString();
                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[6].Value = reader["Ends"].ToString();
                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[7].Value = Localization.ParseNativeInt(reader["FormulaID"].ToString());
                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[10].Value = reader["WtPerCut"].ToString();
                            this.txtTpLen.Text = reader["TapLen"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void CalcVal()
        {
            try
            {
                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    txtMtrs.Text = Strings.Format(Localization.ParseNativeDouble(txtTpLen.Text) * Localization.ParseNativeDouble(txtCuts.Text), "0.00");
                    txtEnds.Text = string.Format("{0:N0}", CommonCls.GetColSum(fgDtls, 6, -1, -1));
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void txtCuts_TextChanged(object sender, EventArgs e)
        {
            CalcVal();
        }

        private void txtCuts_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                    {

                        fgDtls.Rows[i].Cells[8].Value = Operators.MultiplyObject(txtCuts.Text, fgDtls.Rows[i].Cells[10].Value);
                        fgDtls.Rows[i].Cells[9].Value = Operators.MultiplyObject(txtCuts.Text, fgDtls.Rows[i].Cells[10].Value);
                    }
                    CalcVal();
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[8].Value = Localization.ParseNativeDouble(txtCuts.Text) * Localization.ParseNativeDouble(fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[10].Value.ToString());
                fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[9].Value = Localization.ParseNativeDouble(txtCuts.Text) * Localization.ParseNativeDouble(fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[10].Value.ToString());
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
                            txtEnds.Text = string.Format("{0:N0}", CommonCls.GetColSum(fgDtls, 6, -1, -1));
                            break;
                    }
                    CalcVal();
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public enum dgCols
        {
            BeamOpeningID,
            SubBeamOpeningID,
            YarnID,
            ColorID,
            ShadeID,
            Count,
            Ends,
            FormulaID,
            Weight,
            CalcWt,
            WtPerCut
        }

        #endregion

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
                            if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_StockBeamLedger() WHERE RefID<>'' AND RefID=" + CommonLogic.SQuote(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells["RefID"].Value.ToString()) + (Db_Detials.CompID.ToString() != "" ? " and CompID=" + Db_Detials.CompID : "") + "")) > 1)
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityWarning, "", "Reference Found In Another Module..Row Cannot Be Deleted");
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
                        catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
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
    }
}
