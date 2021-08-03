using System;
using System.Collections;
using System.Windows.Forms;
using  CIS_Bussiness;using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmAdjustmentMaster : frmMasterIface
    {
        public frmAdjustmentMaster()
        {
            InitializeComponent();
        }

        #region Form Events

        private void frmAdjustmentMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboFormName, Combobox_Setup.ComboType.Mst_Form, "");
                Combobox_Setup.FillCbo(ref cboLedgerName, Combobox_Setup.ComboType.Mst_Ledger, "");
                Combobox_Setup.FillCbo(ref cboDrCr, Combobox_Setup.ComboType.Mst_DrCr, "");
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        cboFormName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.cboFormName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        cboFormName.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "AdjustmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboFormName, "FormID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboLedgerName, "LedgerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtOrderNo, "OrderNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDrCr, "DrCrID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEd1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    cboFormName.Enabled = false;
                    cboLedgerName.Enabled = false;
                }
                else
                {
                    cboFormName.Enabled = true;
                    cboLedgerName.Enabled = true;
                }
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
                ApplyActStatus();
                cboFormName.Enabled = true;
                cboLedgerName.Enabled = true;
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
                sComboAddText = cboFormName.Text;

                ArrayList pArrayData = new ArrayList
                {
                    cboFormName.SelectedValue.ToString(),
                    cboLedgerName.SelectedValue.ToString(),
                    txtOrderNo.Text.Trim(),
                    cboDrCr.SelectedValue.ToString(),
                    (ChkActive.Checked ? 1 : 0),
                    (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                    (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                    (dtEd1.TextFormat(false, true)),
                    (txtET1.Text.Trim()),
                    (txtET2.Text.Trim()),
                    (txtET3.Text.Trim())
                };
                DBSp.Master_AddEdit(pArrayData, "");
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
                string strCondtn = string.Empty;
                string str = string.Empty;
                int strCount = 0;

                if ((cboFormName.SelectedValue == null) || (cboFormName.SelectedValue.ToString() == "-") || (cboFormName.SelectedValue.ToString() == "0"))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Form");
                    cboFormName.Focus();
                    return true;
                }
                if ((cboLedgerName.SelectedValue == null) || (cboLedgerName.SelectedValue.ToString() == "-") || (cboLedgerName.SelectedValue.ToString() == "0"))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Ledger");
                    cboLedgerName.Focus();
                    return true;
                }

                if (txtOrderNo.Text.Trim() == "" || txtOrderNo.Text.Trim() == "-" || txtOrderNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter OrderNo");
                    txtOrderNo.Focus();
                    return true;
                }

                if ((cboDrCr.SelectedValue == null) || (cboDrCr.SelectedValue.ToString() == "-") || (cboDrCr.SelectedValue.ToString() == "0"))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Dr.Cr");
                    cboDrCr.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    str = "fn_AdjustmentLedgersMain_Tbl()";
                    if (Navigate.CheckDuplicate(ref str, "LedgerID", cboLedgerName.SelectedValue.ToString(), false, "", 0L, " FormID= " + cboFormName.SelectedValue, ""))
                    {
                        cboLedgerName.Focus();
                        return true;
                    }
                }
                else
                {
                    str = "fn_AdjustmentLedgersMain_Tbl()";
                    if (Navigate.CheckDuplicate(ref str, "LedgerID", cboLedgerName.SelectedValue.ToString(), true, "AdjustmentID", Localization.ParseNativeLong(txtCode.Text.Trim()), " FormID= " + cboFormName.SelectedValue, ""))
                    {
                        cboLedgerName.Focus();
                        return true;
                    }
                }

                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    strCount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(0) from tbl_AdjustmentLedgers where FormID={0} and OrderNo={1}", cboFormName.SelectedValue, txtOrderNo.Text.Trim())));
                }
                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    strCount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(0) from tbl_AdjustmentLedgers where FormID={0} and OrderNo={1} and AdjustmentID != " + txtCode.Text + "", cboFormName.SelectedValue, txtOrderNo.Text.Trim())));
                }

                if (strCount > 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Duplicate Order No");
                    cboFormName.Focus();
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

        #endregion

    }
}
