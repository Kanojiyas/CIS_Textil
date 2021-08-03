using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmDesignationMaster : frmMasterIface
    {
        public frmDesignationMaster()
        {
            InitializeComponent();
        }

        #region Form Events

        private void frmDesignationMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboBranch, Combobox_Setup.ComboType.Mst_Branch, "");
                Combobox_Setup.FillCbo(ref CboDepartment, Combobox_Setup.ComboType.Mst_EmpDepartment, "");

                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtDesignationName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtDesignationName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtDesignationName.Focus();
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

        public void MovetoField()
        {
            try
            {
                cboBranch.Focus();
                ApplyActStatus();
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
                DBValue.Return_DBValue(this, txtCode, "DesignationID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBranch, "IBranchID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, CboDepartment, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDesignationName, "DesignationName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
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
                sComboAddText = txtDesignationName.Text;

                ArrayList pArrayData = new ArrayList
                {
                    cboBranch.SelectedValue.ToString(),
                    CboDepartment.SelectedValue.ToString(),
                    txtDesignationName.Text,
                    txtAliasName.Text==""?null:txtAliasName.Text,
                    ChkActive.Checked ? 1 : 0,
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
                int strCount = 0;
                string str = string.Empty;

                if (txtDesignationName.Text.Trim() == "" || txtDesignationName.Text.Trim() == "-" || txtDesignationName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Designation");
                    txtDesignationName.Focus();
                    return true;
                }
                if (cboBranch.SelectedValue == null || cboBranch.SelectedValue.ToString() == "-" || cboBranch.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Branch");
                    cboBranch.Focus();
                    return true;
                }
                if (!cboBranch.IsValidSelect)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Valid Branch Type");
                    this.cboBranch.Focus();
                    return true;
                }

                if (CboDepartment.SelectedValue == null || CboDepartment.SelectedValue.ToString() == "-" || CboDepartment.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    CboDepartment.Focus();
                    return true;
                }

                if (!CboDepartment.IsValidSelect)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Valid Department Type");
                    this.CboDepartment.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    str = "tbl_DesignationMaster";
                    if (Navigate.CheckDuplicate(ref str, "DesignationName", txtDesignationName.Text.Trim(), false, "", 0L, "", "This DesignationName is already available"))
                    {
                        txtDesignationName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtDesignationName.Text.Trim(), false, "", 0L, "", "This DesignationName is already Used in AliasName"))
                    {
                        txtDesignationName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "DesignationName", txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in DesignationName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                }
                else
                {
                    str = "tbl_DesignationMaster";
                    if (Navigate.CheckDuplicate(ref str, "DesignationName", txtDesignationName.Text.Trim(), true, "DesignationID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This DesignationName is already available"))
                    {
                        txtDesignationName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), true, "DesignationID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtDesignationName.Text.Trim(), true, "DesignationID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This DesignationName is already Used in AliasName"))
                    {
                        txtDesignationName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "DesignationName", txtAliasName.Text.Trim(), true, "DesignationID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This AliasName is already Used in DesignationName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    strCount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(0) from fn_DesignationMaster_Tbl() where DesignationName={0} and IBranchID not in({1}) and DepartmentID not in({2})", CommonLogic.SQuote(txtDesignationName.Text.Trim()), cboBranch.SelectedValue, CboDepartment.SelectedValue)));
                    if (strCount > 0)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "Duplicate Designation Name");
                        txtDesignationName.Focus();
                        return true;
                    }

                    strCount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(0) from fn_DesignationMaster_Tbl() where AliasName={0} and IBranchID not in({1}) and DepartmentID not in({2})", CommonLogic.SQuote(txtAliasName.Text.Trim()), cboBranch.SelectedValue, CboDepartment.SelectedValue)));
                    if (strCount > 0)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "Duplicate Alias Name");
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    strCount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(0) from fn_DesignationMaster_Tbl() where DesignationName={0} and IBranchID={1} and DepartmentID={2}", CommonLogic.SQuote(txtDesignationName.Text.Trim()), cboBranch.SelectedValue, CboDepartment.SelectedValue)));
                    if (strCount > 0)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "Duplicate Designation Name");
                        txtDesignationName.Focus();
                        return true;
                    }

                    strCount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(0) from fn_DesignationMaster_Tbl() where AliasName={0} and IBranchID={1} and DepartmentID={2}", CommonLogic.SQuote(txtAliasName.Text.Trim()), cboBranch.SelectedValue, CboDepartment.SelectedValue)));
                    if (strCount > 0)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Duplicate Alias Name");
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtDesignationName, txtAliasName, "tbl_DesignationMaster", "DesignationName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtDesignationName, txtAliasName, "tbl_DesignationMaster", "DesignationName"))
                    return true;
                return false;
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                return false;
            }
        }

        #endregion

        private void cboBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((cboBranch.SelectedValue != null) && (Localization.ParseNativeDouble(cboBranch.SelectedValue.ToString()) > 0.0))
                {
                    Combobox_Setup.FillCbo(ref CboDepartment, Combobox_Setup.ComboType.Mst_DepartmentPayroll, " IBranchID=" + cboBranch.SelectedValue + "");
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void txtDesignationName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtDesignationName, txtAliasName, "tbl_DesignationMaster", "DesignationName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtDesignationName, txtAliasName, "tbl_DesignationMaster", "DesignationName");
        }
    }
}
