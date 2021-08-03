using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_Evaluator;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmWeftDesignMaster : frmMasterIface
    {
        private DataGridViewEx fgDtls;
        private DataGridViewEx fgDtls_footer;

        public frmWeftDesignMaster()
        {
            InitializeComponent();
            fgDtls = new DataGridViewEx();
        }

        #region Form Event

        private void frmWeftDesignMaster_Load(object sender, EventArgs e)
        {
            try
            {
                fgDtls = GrdMain.fgDtls;
                fgDtls_footer = GrdMain.fgDtls_f;


                if (!Localization.ParseBoolean(GlobalVariables.FD_ON))
                {
                    lblOrderNo.Visible = false;
                    lblOrderNoColun.Visible = false;
                    txtOrderNo.Visible = false;
                }
                else
                {
                    lblOrderNo.Visible = true;
                    lblOrderNoColun.Visible = true;
                    txtOrderNo.Visible = true;
                }
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboQuality, Combobox_Setup.ComboType.Mst_FabricQuality, "");
                Combobox_Setup.FillCbo(ref cboShadeColor, Combobox_Setup.ComboType.Mst_FabricShade, "");
                //Combobox_Setup.FillCbo(ref CboBeam1, Combobox_Setup.ComboType.Mst_BeamDesign, "");
                //Combobox_Setup.FillCbo(ref CboBeam2, Combobox_Setup.ComboType.Mst_BeamDesign, "");
                //Combobox_Setup.FillCbo(ref CboBeam3, Combobox_Setup.ComboType.Mst_BeamDesign, "");
                //Combobox_Setup.FillCbo(ref CboBeam4, Combobox_Setup.ComboType.Mst_BeamDesign, "");
                Combobox_Setup.FillCbo(ref cboDesignNo, Combobox_Setup.ComboType.Mst_DesignNo, "");
                Combobox_Setup.FillCbo(ref cboLoomType, Combobox_Setup.ComboType.Mst_LoomType, "");
                Combobox_Setup.FillCbo(ref cboDyeingType, Combobox_Setup.ComboType.Mst_FabricProcessType, "");
                Combobox_Setup.FillCbo(ref cboPayType, Combobox_Setup.ComboType.Mst_FabricProcessType, "");
                Combobox_Setup.FillCbo(ref cboWeftCal, Combobox_Setup.ComboType.Mst_FabricProcessType, "");

                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtDesignName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtDesignName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtDesignName.Focus();
                        this.isComboAddText = true;
                        this.isSecondMessage = true;
                    }
                }

                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    FillControls();
                }

                this.fgDtls.CellEndEdit += new DataGridViewCellEventHandler(this.fgDtls_CellEndEdit);
                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls.RowsAdded += new DataGridViewRowsAddedEventHandler(this.fgDtls_RowsAdded);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #endregion

        #region From Navigation

        public void ApplyActStatus()
        {
            if (base.blnFormAction == Enum_Define.ActionType.New_Record)
            {
                ChkActive.Checked = true;
                ChkActive.Visible = false;
            }
            else
            {
                ChkActive.Visible = true;
            }
        }

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                dtDate.Focus();
                ApplyActStatus();
                dtDate.Text = Conversions.ToString(DateAndTime.Now.Date);
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                int MaxId = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(FabricDesignID),0) From {0} ", "tbl_FabricDesignMaster"))));
                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where FabricDesignID = {1} and CompID={2} and YearID={3}", "tbl_FabricDesignMaster", MaxId, Db_Detials.CompID, Db_Detials.YearID)))
                {
                    while (reader.Read())
                    {
                        dtDate.Text = Localization.ToVBDateString(reader["Date"].ToString());
                        cboDesignNo.SelectedValue = Localization.ParseNativeInt(reader["FabricDesignNo"].ToString()).ToString();
                        cboQuality.SelectedValue = Localization.ParseNativeInt(reader["FabricQualityID"].ToString());
                        cboShadeColor.SelectedValue = Localization.ParseNativeInt(reader["FabricShadeID"].ToString());
                    }
                }
                txtDesignName.Focus();
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
                sComboAddText = txtDesignName.Text;
                ArrayList pArrayData = new ArrayList
                {
                    dtDate.TextFormat(false, true),
                    txtOrderNo.Text.Trim(),
                    txtDesignName.Text.Trim(),
                    null,
                    txtDesignNo.Text.Trim(),
                    txtSeriesNo.Text.Trim(),
                    cboQuality.SelectedValue,
                    cboShadeColor.SelectedValue,
                    "NULL",
                    txtReed.Text.Trim(),
                    txtPick.Text,
                    txtTotalPick.Text.Trim(),
                    txtWidth.Text.Trim(),
                    "0",
                    txtTapLen.Text.Trim(),
                    txtMtrsTaka.Text.Trim(),
                    txtWtCuts.Text.Trim(),
                    cboWeftCal.SelectedValue,
                    cboPayType.SelectedValue,
                    txtShortage.Text.Trim(),
                    txtProdRate.Text.Trim(),
                    txtSalesRate.Text.Trim(),
                    cboDesignNo.SelectedValue,
                    cboLoomType.SelectedValue,
                    txtWastage.Text.ToString(),
                    txtShrinkage.Text.ToString(),
                    cboDyeingType.SelectedValue,
                    (ChkActive.Checked?1: 0),
                    "NULL",
                    "NULL",
                    0,
                    0,
                    0,
                    0,
                    0
                };
                
                DBSp.Transcation_AddEdit(pArrayData, fgDtls, true, "");
                this.IsMasterAdded = true;
            }
            catch (Exception ex)
            {
                this.IsMasterAdded = false;
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                string str = "";
                if (!EventHandles.IsValidGridReq(fgDtls, base.dt_AryIsRequired))
                {
                    return true;
                }
                if (!EventHandles.IsRequiredInGrid(fgDtls, this.dt_AryIsRequired, false))
                {
                    return true;
                }
                if (txtDesignName.Text.Trim() == "" || txtDesignName.Text.Trim() == "-" || txtDesignName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Weft Design Name");
                    txtDesignName.Focus();
                    return true;
                }
                if (cboQuality.SelectedValue == null || cboQuality.SelectedValue.ToString() == "-" || cboQuality.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Valid Quality");
                    cboQuality.Focus();
                    return true;
                }
                if (cboShadeColor.SelectedValue == null || cboShadeColor.SelectedValue.ToString() == "-" || cboShadeColor.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Valid Shade");
                    cboShadeColor.Focus();
                    return true;
                }
                if (txtWidth.Text.Trim() == "" || txtWidth.Text.Trim() == "-" || txtWidth.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Width");
                    txtWidth.Focus();
                    return true;
                }
                if (txtTapLen.Text.Trim() == "" || txtTapLen.Text.Trim() == "-" || txtTapLen.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Tap Length");
                    txtTapLen.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    if (Navigate.CheckDuplicate(ref str, "FabricDesignName", txtDesignName.Text.Trim(), false, "", 0L, "", "This DesignName is already available"))
                    {
                        txtDesignName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtDesignName.Text.Trim(), false, "", 0L, "", "This DesignName is already Used"))
                    {
                        txtDesignName.Focus();
                        return true;
                    }
                }
                else
                {
                    if (Navigate.CheckDuplicate(ref str, "FabricDesignName", txtDesignName.Text.Trim(), true, "FabricDesignID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This DesignName is already available"))
                    {
                        this.txtDesignName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtDesignName.Text.Trim(), true, "FabricDesignID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This DesignName is already Used"))
                    {
                        this.txtDesignName.Focus();
                        return true;
                    }
                }
                CalcVal();
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
                DBValue.Return_DBValue(this, txtCode, "FabricDesignID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDesignName, "FabricDesignName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtOrderNo, "FabDesOrderNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDesignNo, "FabricDesignNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtSeriesNo, "SeriesNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboQuality, "FabricQualityID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboShadeColor, "FabricShadeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtReed, "Reed", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtTotalPick, "Pick", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtWidth, "Width", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtTapLen, "TapLen", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMtrsTaka, "MtrsTaka", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtWtCuts, "WtCuts", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboWeftCal, "WftCal", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboPayType, "PayType", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtProdRate, "Shortage", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtShortage, "ProdRate", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtSalesRate, "SalesRate", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtDate, "Date", Enum_Define.ValidationType.IsDate);
                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    DBValue.Return_DBValue(this, cboDesignNo, "DesignNoID", Enum_Define.ValidationType.Text);
                }
                DBValue.Return_DBValue(this, cboLoomType, "LoomTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtWastage, "Wastage", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtShrinkage, "Shrinkage", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDyeingType, "DyeingTypeID", Enum_Define.ValidationType.Text);

                DetailGrid_Setup.FillGrid(fgDtls, fgDtls.Grid_UID, fgDtls.Grid_Tbl, "FabricDesignID", Localization.ParseNativeDouble(txtCode.Text.Trim()).ToString(), base.dt_HasDtls_Grd);
                this.CalcVal();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
            ApplyActStatus();
        }

        #endregion

        private void CalcVal()
        {
            this.TxtTotWts.Text = string.Format("{0:N3}", CommonCls.GetColSum(fgDtls, 9, -1, -1));
            this.txtTotalPick.Text = string.Format("{0:N}", CommonCls.GetColSum(fgDtls, 5, -1, -1));
        }

        private void CalcWeight(int ColumnIndex, int RowIndex)
        {
            try
            {
                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    decimal strcount = 0;
                    decimal strDeniar = 0;
                    if (fgDtls.Rows[RowIndex].Cells[2].Value != null && fgDtls.Rows[RowIndex].Cells[2].Value.ToString() != "0" && fgDtls.Rows[RowIndex].Cells[2].Value.ToString() != "")
                    {
                        strcount = Localization.ParseNativeDecimal(DB.GetSnglValue("Select count from tbl_YarnMaster Where YarnID=" + Localization.ParseNativeInt(fgDtls.Rows[RowIndex].Cells[2].Value.ToString()) + "").ToString());
                        strDeniar = Localization.ParseNativeDecimal(DB.GetSnglValue("Select Deniar from tbl_YarnMaster Where YarnID=" + Localization.ParseNativeInt(fgDtls.Rows[RowIndex].Cells[2].Value.ToString()) + "").ToString());
                    }

                    string strFrml = fgDtls.Rows[RowIndex].Cells[8].FormattedValue.ToString();

                    if (strFrml.Contains("C"))
                    {
                        if (strcount.ToString() == "0" || strcount.ToString() == "0.000" || strcount.ToString() == "")
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Count In Yarn Master");
                            return;
                        }
                    }
                    if (strFrml.Contains("D"))
                    {
                        if (strDeniar.ToString() == "0" || strDeniar.ToString() == "0.000" || strDeniar.ToString() == "")
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Deniar In Yarn Master");
                            return;
                        }
                    }
                    if (fgDtls.Rows[RowIndex].Cells[6].FormattedValue.ToString() != "")
                    {
                        strFrml = strFrml.Replace("C", strcount.ToString().Replace(",", ""));

                        strFrml = strFrml.Replace("D", strDeniar.ToString().Replace(",", ""));
                    }
                    if (fgDtls.Rows[RowIndex].Cells[5].FormattedValue.ToString() != "")
                    {
                        strFrml = strFrml.Replace("P", fgDtls.Rows[RowIndex].Cells[5].Value.ToString().Replace(",", ""));
                    }
                    if (txtWidth.Text.ToString() != "")
                    {
                        strFrml = strFrml.Replace("W", txtWidth.Text.ToString().Replace(",", ""));
                    }
                    if (this.txtTapLen.Text.ToString() != "")
                    {
                        strFrml = strFrml.Replace("T", txtTapLen.Text.ToString().Replace(",", ""));
                    }
                    if (txtTotalPick.Text.ToString() != "")
                    {
                        strFrml = strFrml.Replace("S", txtTotalPick.Text.ToString().Replace(",", ""));
                    }
                    if (fgDtls.Rows[RowIndex].Cells[5].FormattedValue.ToString() != "")
                    {
                        strFrml = strFrml.Replace("I", fgDtls.Rows[RowIndex].Cells[5].FormattedValue.ToString().Replace(",", ""));
                    }
                    try
                    {
                        string sVal = Evaluator.EvalToDouble(strFrml.Replace(",", "")).ToString();
                        if (Localization.ParseNativeDouble(sVal) > 0)
                        {
                            fgDtls.Rows[RowIndex].Cells[9].Value = Localization.ParseNativeDouble(sVal);
                            if (txtTapLen.Text != null && txtTapLen.Text != "" && txtTapLen.Text != "0")
                            {
                                fgDtls.Rows[RowIndex].Cells[10].Value = Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[9].Value.ToString()) / Localization.ParseNativeDouble(this.txtTapLen.Text.ToString());
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                this.txtWtCuts.Text = string.Format("{0:N3}", CommonCls.GetColSum(fgDtls, 9, -1, -1));
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void ChkActive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void fgDtls_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record)) && (((e.ColumnIndex != 1) & (e.ColumnIndex != 0)) & (e.ColumnIndex != 8)))
            {
                try
                {
                }
                catch { }
            }
        }

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2 || e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 8)
                {
                    CalcWeight(e.ColumnIndex, e.RowIndex);
                }
                CalcVal();
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
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    fgDtls.Rows[e.RowIndex].Cells[6].Value = 0;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
    }
}