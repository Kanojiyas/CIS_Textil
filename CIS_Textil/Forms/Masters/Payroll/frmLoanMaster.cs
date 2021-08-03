using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmLoanMaster : frmMasterIface
    {
        public frmLoanMaster()
        {
            InitializeComponent();
        }

        private void frmLoanMaster_Load(object sender, EventArgs e)
        {
            try
            {
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtLoanName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtLoanName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtLoanName.Focus();
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

        #region From Event

        public void MovetoField()
        {
            try
            {
                txtLoanName.Focus();
                ChkActive.Enabled = true;
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
                DBValue.Return_DBValue(this, txtCode, "LoanID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLoanName, "LoanName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtIntRate, "IntRate", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMaxLmt, "MaxLimit", Enum_Define.ValidationType.Text);
                string NoOfInstallMent = DB.GetSnglValue("SELECT MaxInst FROM tbl_LoanMaster WHERE LoanID=" + txtCode.Text + "");
                cboMaxInstallments.Text = NoOfInstallMent;
                string IsActive = DBValue.Return_DBValue(this, "IsActive");

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
                sComboAddText = txtLoanName.Text;

                ArrayList pArrayData = new ArrayList
                {
                    txtLoanName.Text.Trim(),
                    txtAliasName.Text.Trim(),
                    txtIntRate.Text.Trim(),
                    txtMaxLmt.Text.Trim(),
                    cboMaxInstallments.SelectedItem.ToString(),
                    ChkActive.Checked?1:0,
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
                if (txtLoanName.Text.Trim() == "" || txtLoanName.Text.Trim() == "-" || txtLoanName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Loan Name");
                    txtLoanName.Focus();
                    return true;
                }

                if (txtIntRate.Text.Trim() == "" || txtIntRate.Text.Trim() == "-" || txtIntRate.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Interest Rate");
                    txtIntRate.Focus();
                    return true;
                }

                if (txtMaxLmt.Text.Trim() == "" || txtMaxLmt.Text.Trim() == "-" || txtMaxLmt.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Maximum Limit");
                    txtMaxLmt.Focus();
                    return true;
                }

                if (cboMaxInstallments.SelectedValue == null || cboMaxInstallments.SelectedValue.ToString() == "-" || cboMaxInstallments.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Maximum Installments");
                    cboMaxInstallments.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    str = "tbl_LoanMaster";
                    if (Navigate.CheckDuplicate(ref str, "LoanName", txtLoanName.Text.Trim(), false, "", 0L, "", "This LoanName is already available"))
                    {
                        txtLoanName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtLoanName.Text.Trim(), false, "", 0L, "", "This LoanName is already Used in AliasName"))
                    {
                        txtLoanName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "LoanName", txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in LoanName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    str= "tbl_LoanMaster";
                    if (Navigate.CheckDuplicate(ref str, "LoanName", txtLoanName.Text.Trim(), true, "LoanID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This LoanName is already available"))
                    {
                        txtLoanName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), true, "LoanID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtLoanName.Text.Trim(), true, "LoanID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This LoanName is already Used in AliasName"))
                    {
                        txtLoanName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "LoanName", txtAliasName.Text.Trim(), true, "LoanID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This AliasName is already Used in LoanName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtLoanName, txtAliasName, "tbl_LoanMaster", "LoanName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtLoanName, txtAliasName, "tbl_LoanMaster", "LoanName"))
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

        private void txtLoanName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtLoanName, txtAliasName, "tbl_LoanMaster", "LoanName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtLoanName, txtAliasName, "tbl_LoanMaster", "LoanName");
        }
    }
}
