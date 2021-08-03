using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmDepartmentMaster : frmMasterIface
    {
        public frmDepartmentMaster()
        {
            InitializeComponent();
        }

        #region Form Events
        private void frmDepartmentMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboBranchname, Combobox_Setup.ComboType.Mst_Branch, "");

                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtDepartmentname.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtDepartmentname.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtDepartmentname.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBranchname, "IBranchID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDepartmentname, "DepartmentName", Enum_Define.ValidationType.Text);
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
                sComboAddText = txtDepartmentname.Text;
                ArrayList pArrayData = new ArrayList
                {
                    cboBranchname.SelectedValue.ToString(),
                    txtDepartmentname.Text.Trim(),
                    txtAliasName.Text==""?null:txtAliasName.Text,
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
                string strCondtn = string.Empty;
                string str = string.Empty;
                int strCount = 0;
                if (txtDepartmentname.Text.Trim() == "" || txtDepartmentname.Text.Trim() == "-" || txtDepartmentname.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Department Name");
                    txtDepartmentname.Focus();
                    return true;
                }

                if (cboBranchname.SelectedValue == null || cboBranchname.SelectedValue.ToString() == "-" || cboBranchname.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Branch Name");
                    cboBranchname.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    str = "tbl_DepartmentMaster";
                    if (Navigate.CheckDuplicate(ref str, "DepartmentName", txtDepartmentname.Text.Trim(), false, "", 0L, "", "This DepartmentName is already available"))
                    {
                        txtDepartmentname.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtDepartmentname.Text.Trim(), false, "", 0L, "", "This DepartmentName is already Used in AliasName"))
                    {
                        txtDepartmentname.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "DepartmentName", txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in DepartmentName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                }
                else
                {
                    str = "tbl_DepartmentMaster";
                    if (Navigate.CheckDuplicate(ref str, "DepartmentName", txtDepartmentname.Text.Trim(), true, "DepartmentID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This DepartmentName is already available"))
                    {
                        txtDepartmentname.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), true, "DepartmentID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtDepartmentname.Text.Trim(), true, "DepartmentID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This DepartmentName is already Used in AliasName"))
                    {
                        txtDepartmentname.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "DepartmentName", txtAliasName.Text.Trim(), true, "DepartmentID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This AliasName is already Used in DepartmentName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    strCount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(0) from fn_DepartmentMaster_Tbl() where DepartmentName={0} and IBranchID not in({1})", CommonLogic.SQuote(txtDepartmentname.Text.Trim()), cboBranchname.SelectedValue)));
                    if (strCount > 0)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Duplicate Department Name");
                        txtDepartmentname.Focus();
                        return true;
                    }

                    strCount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(0) from fn_DepartmentMaster_Tbl() where AliasName={0} and IBranchID not in({1})", CommonLogic.SQuote(txtAliasName.Text.Trim()), cboBranchname.SelectedValue)));
                    if (strCount > 0)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Duplicate Alias Name");
                        txtAliasName.Focus();
                        return true;
                    }
                }

                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    strCount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(0) from fn_DepartmentMaster_Tbl() where DepartmentName={0} and IBranchID={1}", CommonLogic.SQuote(txtDepartmentname.Text.Trim()), cboBranchname.SelectedValue)));
                    if (strCount > 0)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Duplicate Department Name");
                        txtDepartmentname.Focus();
                        return true;
                    }

                    strCount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(0) from fn_DepartmentMaster_Tbl() where AliasName={0} and IBranchID={1}", CommonLogic.SQuote(txtAliasName.Text.Trim()), cboBranchname.SelectedValue)));
                    if (strCount > 0)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Duplicate Alias Name");
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtDepartmentname, txtAliasName, "tbl_DepartmentMaster", "DepartmentName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtDepartmentname, txtAliasName, "tbl_DepartmentMaster", "DepartmentName"))
                    return true;
                return false;
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                return false;
            }
        }

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                CommonCls.IncFieldID(this, ref txtCode, "");
                ApplyActStatus();
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #endregion

        private void txtDepartmentname_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtDepartmentname, txtAliasName, "tbl_DepartmentMaster", "DepartmentName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtDepartmentname, txtAliasName, "tbl_DepartmentMaster", "DepartmentName");
        }

    }
}
