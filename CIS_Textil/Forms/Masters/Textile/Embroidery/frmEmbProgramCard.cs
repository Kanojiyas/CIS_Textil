using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_CLibrary;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class frmEmbProgramCard : frmMasterIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;
        private string strTbl;

        public frmEmbProgramCard()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
        }

        #region Event
        private void frmProgramCard_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboParty, Combobox_Setup.ComboType.Mst_Customer, "");
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                txtEntryNo.Enabled = false;
                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    FillControls();
                }

                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls.CellEndEdit += new DataGridViewCellEventHandler(this.fgDtls_CellEndEdit);
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
                DBValue.Return_DBValue(this, txtCode, "EmbProgramID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, dtProgramDate, "ProgramDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtProgramNo, "ProgramNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtProgramName, "ProgramName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboParty, "PartyID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtRemark, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEd1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "EmbProgramID", Conversions.ToString(Localization.ParseNativeDouble(this.txtCode.Text)), base.dt_HasDtls_Grd);

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                }
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
                    dtEntryDate.TextFormat(false, true),
                    txtProgramNo.Text,
                    dtProgramDate.TextFormat(false, true),
                    txtProgramName.Text.ToString(),
                    cboParty.SelectedValue,
                    string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 5, -1, -1)).ToString().Replace(",",""),
                    string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 6, -1, -1)).ToString().Replace(",",""),
                    txtRemark.Text,
                    cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue,
                    cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue,
                    dtEd1.TextFormat(false,true), 
                    txtET1.Text,
                    txtET2.Text,
                    txtET3.Text
                };
                DBSp.Transcation_AddEdit(pArrayData, fgDtls, true, "", "", txtEntryNo.Text, "", "");
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                if (!Information.IsDate(dtEntryDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry Date");
                    dtEntryDate.Focus();
                    return true;
                }

                if (!Information.IsDate(dtProgramDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry Date");
                    dtProgramDate.Focus();
                    return true;
                }

                if (txtProgramNo.Text.Trim() == "" || txtProgramNo.Text.Trim() == "-" || txtProgramNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Program No");
                    txtProgramNo.Focus();
                    return true;
                }

                if (txtProgramName.Text.Trim() == "" || txtProgramName.Text.Trim() == "-" || txtProgramName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Program Name");
                    txtProgramName.Focus();
                    return true;
                }

                if (cboParty.SelectedValue == null || cboParty.Text.Trim().ToString() == "-" || cboParty.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Party");
                    cboParty.Focus();
                    return true;
                }

                if (!EventHandles.IsValidGridReq(fgDtls, base.dt_AryIsRequired))
                {
                    return true;
                }
                if (!EventHandles.IsRequiredInGrid(fgDtls, dt_AryIsRequired, false))
                {
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    strTbl = "tbl_EmbProgramMain";
                    if (Navigate.CheckDuplicate(ref strTbl, "ProgramNo", txtProgramNo.Text.Trim(), false, "", 0, "StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                    {
                        txtProgramNo.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTbl, "ProgramName", txtProgramName.Text.Trim(), false, "", 0, "StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                    {
                        txtProgramName.Focus();
                        return true;
                    }
                }
                else
                {
                    strTbl = "tbl_EmbProgramMain";
                    if (Navigate.CheckDuplicate(ref strTbl, "ProgramNo", txtProgramNo.Text.Trim(), true, "EmbProgramID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                    {
                        txtProgramNo.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTbl, "ProgramName", txtProgramName.Text.Trim(), true, "EmbProgramID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                    {
                        txtProgramName.Focus();
                        return true;
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

        private void fgDtls_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                {
                }
            }
            catch { }
        }

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    switch (e.ColumnIndex)
                    {
                        case 2:
                            IDataReader idr = DB.GetRS("Select * From fn_EmbDesignCardMain_Tbl() Where EmbDesignCardID=" + fgDtls.Rows[e.RowIndex].Cells[2].Value + "");
                            while (idr.Read())
                            {
                                fgDtls.Rows[e.RowIndex].Cells[4].Value = idr["TotStitches"].ToString();
                                fgDtls.Rows[e.RowIndex].Cells[7].Value = Localization.ParseNativeDecimal(idr["Rate"].ToString());
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

        public void PrintRecord()
        {
            CIS_ReportTool.frmMultiPrint frmMultiPrint = new CIS_ReportTool.frmMultiPrint();
            CIS_ReportTool.frmMultiPrint.MenuID = base.iIDentity;
            CIS_ReportTool.frmMultiPrint.Id = Localization.ParseNativeInt(this.txtCode.Text);
            CIS_ReportTool.frmMultiPrint.TblNm = "tbl_EmbProgramMain";
            CIS_ReportTool.frmMultiPrint.IdStr = "EmbProgramID";
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

        public string strTable { get; set; }
    }
}
