using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_Evaluator;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmBeamDesignMaster : frmMasterIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        public frmBeamDesignMaster()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
        }

        #region Form Event
        private void frmBeamDesignMaster_Load(object sender, EventArgs e)
        {
            try
            {
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls.CellEndEdit += new DataGridViewCellEventHandler(this.fgDtls_CellEndEdit);
                if (!Localization.ParseBoolean(GlobalVariables.BD_ON))
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
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtBeamDesign.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtBeamDesign.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtBeamDesign.Focus();
                        this.isComboAddText = true;
                        this.isSecondMessage = true;
                    }
                }

                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    FillControls();
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region Form Navigation

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

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "BeamDesignID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtBeamDesign, "BeamDesignName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtOrderNo, "OrderNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtWidth, "Width", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
                DetailGrid_Setup.FillGrid(fgDtls, fgDtls.Grid_UID, fgDtls.Grid_Tbl, "BeamDesignID", Localization.ParseNativeDouble(this.txtCode.Text).ToString(), base.dt_HasDtls_Grd);
                this.CalcVal();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
            ApplyActStatus();
        }

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                EventHandles.CreateDefault_Rows(fgDtls, dt_HasDtls_Grd, dt_AryCalcvalue, dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                txtBeamDesign.Focus();
                ApplyActStatus();
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
                sComboAddText = txtBeamDesign.Text;
                ArrayList pArrayData = new ArrayList
                {
                   0, 
                   txtBeamDesign.Text.Trim(),
                   txtAliasName.Text==""?null:txtAliasName.Text,
                   txtOrderNo.Text.Trim(),
                   txtWidth.Text.Trim(),
                    (ChkActive.Checked ? 1 : 0)
                };

                DBSp.Transcation_AddEdit(pArrayData, fgDtls, true);
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
                string str;
                if (!EventHandles.IsValidGridReq(this.fgDtls, base.dt_AryIsRequired))
                {
                    return true;
                }
                if (!EventHandles.IsRequiredInGrid(fgDtls, this.dt_AryIsRequired, false))
                {
                    return true;
                }
                if (txtBeamDesign.Text.Trim() == "" || txtBeamDesign.Text.Trim() == "-" || txtBeamDesign.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Beam Design");
                    this.txtBeamDesign.Focus();
                    return true;
                }
                if (DBSp.rtnAction())
                {
                    str = "tbl_BeamDesignMaster";
                    if (Navigate.CheckDuplicate(ref str, "BeamDesignName", txtBeamDesign.Text.Trim(), false, "", 0, "", "This BeamDesign is already available"))
                    {
                        this.txtBeamDesign.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "Aliasname", txtAliasName.Text.Trim(), false, "", 0, "", "This Aliasname is already available"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "Aliasname", txtBeamDesign.Text.Trim(), false, "", 0, "", "This BeamDesign is already Used in AliasName"))
                    {
                        this.txtBeamDesign.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "BeamDesignName", txtAliasName.Text.Trim(), false, "", 0, "", "This AliasName is already Used in BeamDesign"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    str = "tbl_BeamDesignMaster";
                    if (Navigate.CheckDuplicate(ref str, "BeamDesignName", txtBeamDesign.Text.Trim(), true, "BeamDesignID", (long)Math.Round(Localization.ParseNativeDouble(this.txtCode.Text.Trim())), "", "This BeamDesign is already available"))
                    {
                        this.txtBeamDesign.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), true, "BeamDesignID", (long)Math.Round(Localization.ParseNativeDouble(this.txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtBeamDesign.Text.Trim(), true, "BeamDesignID", (long)Math.Round(Localization.ParseNativeDouble(this.txtCode.Text.Trim())), "", "This BeamDesign is already Used in AliasName"))
                    {
                        this.txtBeamDesign.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "BeamDesignName", txtAliasName.Text.Trim(), true, "BeamDesignID", (long)Math.Round(Localization.ParseNativeDouble(this.txtCode.Text.Trim())), "", "This AliasName is already Used in BeamDesign"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtBeamDesign, txtAliasName, "tbl_BeamDesignMaster", "BeamDesignName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtBeamDesign, txtAliasName, "tbl_BeamDesignMaster", "BeamDesignName"))
                    return true;
                this.CalcVal();
                return false;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                return false;
            }
        }

        private void ChkActive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        public enum dgCols
        {
            BeamDesignID,
            SubBeamDesignID,
            BeamTypeID,
            YarnID,
            ColorID,
            ShadeID,
            FmlID,
            TapLen,
            Count,
            Cones,
            Ends,
            WtMtrs,
            WtPerTaka
        }

        #endregion

        private void fgDtls_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    if ((((e.ColumnIndex != 8) & (e.ColumnIndex != 1)) & (e.ColumnIndex != 0)) & (e.ColumnIndex != 14))
                    {
                        fgDtls.Rows[e.RowIndex].Cells[9].Value = Localization.ParseNativeDouble(DB.GetSnglValue("Select [COUNT] From tbl_YarnMaster Where YarnID = " + (fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString())));
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
                if (e.ColumnIndex == 6 || e.ColumnIndex == 7 || e.ColumnIndex == 9 || e.ColumnIndex == 13)
                {
                    CalcWeight(e.ColumnIndex);
                }

                if (fgDtls.Rows[e.RowIndex].Cells[13].Value != null && fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString() != "" && fgDtls.Rows[e.RowIndex].Cells[7].Value != null && fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString() != "")
                {
                    if (e.ColumnIndex == 14 && Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[13].Value.ToString()) == 0)
                    {
                        if (Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString()) != 0)
                        {
                            fgDtls.Rows[e.RowIndex].Cells[13].Value = Evaluator.EvalToDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[14].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString())).ToString());
                        }
                    }
                }
                if (e.ColumnIndex == 13)
                {
                    if (fgDtls.Rows[e.RowIndex].Cells[11].Value != null && fgDtls.Rows[e.RowIndex].Cells[11].Value.ToString() != "" && fgDtls.Rows[e.RowIndex].Cells[12].Value != null && fgDtls.Rows[e.RowIndex].Cells[12].Value.ToString() != "")
                    {
                        fgDtls.Rows[e.RowIndex].Cells[13].Value = Evaluator.EvalToDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[11].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[12].Value.ToString())).ToString());
                    }
                }
                this.CalcVal();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void CalcWeight(int ColumnIndex)
        {
            try
            {
                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                    {
                        decimal strcount;
                        decimal strDenier;

                        if (fgDtls.Rows[i].Cells[3].Value != null && fgDtls.Rows[i].Cells[3].Value.ToString() != "" && fgDtls.Rows[i].Cells[3].Value.ToString() != "0")
                        {
                            strcount = Localization.ParseNativeDecimal(DB.GetSnglValue("Select count from tbl_YarnMaster Where YarnID=" + Localization.ParseNativeInt(fgDtls.Rows[i].Cells[3].Value.ToString()) + "").ToString());
                            strDenier = Localization.ParseNativeDecimal(DB.GetSnglValue("Select Denier from tbl_YarnMaster Where YarnID=" + Localization.ParseNativeInt(fgDtls.Rows[i].Cells[3].Value.ToString()) + "").ToString());

                            fgDtls.Rows[i].Cells[14].Value = 0;
                            fgDtls.Rows[i].Cells[15].Value = 0;
                            string str = fgDtls.Rows[i].Cells[6].FormattedValue.ToString();

                            if (str.Contains("C"))
                            {
                                if (strcount.ToString() == "0" || strcount.ToString() == "0.000" || strcount.ToString() == "")
                                {
                                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Count In Yarn Master");
                                    return;
                                }
                            }
                            if (fgDtls.Rows[i].Cells[7].FormattedValue.ToString() != "")
                            {
                                str = str.Replace("T", fgDtls.Rows[i].Cells[7].FormattedValue.ToString().Replace(",", ""));
                            }
                            if (fgDtls.Rows[i].Cells[9].FormattedValue.ToString() != "")
                            {
                                str = str.Replace("C", strcount.ToString().Replace(",", ""));

                                str = str.Replace("D", strDenier.ToString().Replace(",", ""));
                            }
                            if (fgDtls.Rows[i].Cells[13].FormattedValue.ToString() != "")
                            {
                                str = str.Replace("E", fgDtls.Rows[i].Cells[13].FormattedValue.ToString().Replace(",", ""));
                            }

                            string sVal = Evaluator.EvalToDouble(str.Replace(",", "")).ToString();
                            if (Localization.ParseNativeDouble(sVal) > 0)
                            {
                                fgDtls.Rows[i].Cells[15].Value = string.Format("{0:N3}", Localization.ParseNativeDouble(sVal));
                                if (fgDtls.Rows[i].Cells[7].Value != null)
                                {
                                    fgDtls.Rows[i].Cells[14].Value = ((Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[15].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[7].Value.ToString())));
                                }
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

        private void CalcVal()
        {
            TxtTotWts.Text = string.Format("{0:N3}", CommonCls.GetColSum(fgDtls, 15, -1, -1));
        }

        private void txtBeamDesign_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtBeamDesign, txtAliasName, "tbl_BeamDesignMaster", "BeamDesignName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtBeamDesign, txtAliasName, "tbl_BeamDesignMaster", "BeamDesignName");
        }
    }
}
