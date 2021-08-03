using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmDeductionMaster : frmMasterIface
    {
        public frmDeductionMaster()
        {
            InitializeComponent();
        }

        private void frmDeductionMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref ddltype, Combobox_Setup.ComboType.Mst_Type, "");

                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtdeduction.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtdeduction.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtdeduction.Focus();
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

        #region FromEvent

        public void MovetoField()
        {
            try
            {
                txtdeduction.Focus();
                ChkActive.Enabled = true;
                rdbdeduction.Checked = false;
                rdbdeduction.Checked = false;
                rdbnone.Checked = false;
                ddltype.SelectedValue = "0";
                ApplyActStatus();
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
           
        }
        #endregion FromEvent

        #region FormNavigation

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
                DBValue.Return_DBValue(this, txtCode, "DeductID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtdeduction, "DeductionType", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEmpAmt, "EmployeeAmount", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ddltype, "IsAmount", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtpaysheet, "OrderNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtempAmt1, "EmployerAmount", Enum_Define.ValidationType.Text);
                string IsActive = DBValue.Return_DBValue(this, "IsActive");
                string strForm16 = DBValue.Return_DBValue(this, "Form16HeadID");
                if (strForm16 == "1")
                { rdbdeduction.Checked = true; }

                else if (strForm16 == "2")
                { rdbdeduction.Checked = true; }

                else if (strForm16 == "3")
                { rdbnone.Checked = true; }

                if (IsActive == "True")
                {
                    ChkActive.Enabled = false;
                    ChkActive.Checked = true;
                }
                else if (IsActive == "False")
                {
                    ChkActive.Enabled = false;
                    ChkActive.Checked = false;
                }
                else
                {
                    ChkActive.Enabled = true;
                    ChkActive.Checked = false;
                    rdbdeduction.Checked = false;
                    rdbdeduction.Checked = false;
                    rdbnone.Checked = false;
                }
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
            ApplyActStatus();
        }

        public void SaveRecord()
        {
            try
            {
                sComboAddText = txtdeduction.Text;

                int iSel = 0;

                if (rdbdeduction.Checked == true)
                    iSel = 1;
                else if (rdbdeduction.Checked == true)
                    iSel = 2;
                else
                    iSel = 3;
                ArrayList pArrayData = new ArrayList
                {
                txtdeduction.Text.Trim(),
                txtAliasName.Text==""?null:txtAliasName.Text,
                ddltype.SelectedValue,
                "U",
                txtEmpAmt.Text.Trim(),
                txtpaysheet.Text.Trim(),
                txtempAmt1.Text.Trim(),
                iSel,
                (ChkActive.Checked ? 1 : 0),
                (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
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
                string str;
                if (txtdeduction.Text.Trim() == "" || txtdeduction.Text.Trim() == "-" || txtdeduction.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Deduction Name");
                    txtdeduction.Focus();
                    return true;
                }

                if (txtEmpAmt.Text.Trim() == "" || txtEmpAmt.Text.Trim() == "-" || txtEmpAmt.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Amount");
                    txtEmpAmt.Focus();
                    return true;
                }
                if (txtpaysheet.Text.Trim() == "" || txtpaysheet.Text.Trim() == "-" || txtpaysheet.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Order In Paysheet");
                    txtpaysheet.Focus();
                    return true;
                }
                if (DBSp.rtnAction())
                {
                    str = "tbl_DeductionMaster";
                    if (Navigate.CheckDuplicate(ref str, "DeductionType", txtdeduction.Text.Trim(), false, "", 0L, "", "This DeductionName is already available"))
                    {
                        txtdeduction.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtdeduction.Text.Trim(), false, "", 0L, "", "This DeductionName is already Used in AliasName"))
                    {
                        txtdeduction.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "DeductionType", txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in DeductionName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                }
                else
                {
                    str = "tbl_DeductionMaster";
                    if (Navigate.CheckDuplicate(ref str, "DeductionType", txtdeduction.Text.Trim(), true, "DeductID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This DeductionName is already available"))
                    {
                        txtdeduction.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), true, "DeductID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtdeduction.Text.Trim(), true, "DeductID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This DeductionName is already Used in AliasName"))
                    {
                        txtdeduction.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "DeductionType", txtAliasName.Text.Trim(), true, "DeductID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This AliasName is already Used in DeductionName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtdeduction, txtAliasName, "tbl_DeductionMaster", "DeductionType"))
                    return true;

                if (CommonCls.ValidateShortCode(this, txtdeduction, txtAliasName, "tbl_DeductionMaster", "DeductionType"))
                    return true;

                return false;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                return false;
            }
        }

        #endregion FormNavigation

        private void txtdeduction_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtdeduction, txtAliasName, "tbl_DeductionMaster", "DeductionType");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtdeduction, txtAliasName, "tbl_DeductionMaster", "DeductionType");
        }
    }
}
