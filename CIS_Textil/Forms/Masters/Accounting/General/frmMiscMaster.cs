using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmMiscMaster : frmMasterIface
    {
        public frmMiscMaster()
        {
            InitializeComponent();
        }

        #region Event

        private void frmMiscMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboGroupType, Combobox_Setup.ComboType.Mst_MiscType, "");
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtMiscName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtMiscName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtMiscName.Focus();
                        this.isComboAddText = true;
                        this.isSecondMessage = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #endregion

        #region Navigation

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
                DBValue.Return_DBValue(this, txtCode, "MiscID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMiscName, "MiscName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboGroupType, "MiscTypeID", Enum_Define.ValidationType.Text);
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

        public void SaveRecord()
        {
            try
            {
                sComboAddText = txtMiscName.Text;
                ArrayList pArrayData = new ArrayList
                {
                txtMiscName.Text.Trim(),
                txtAliasName.Text==""?null:txtAliasName.Text,
                cboGroupType.SelectedValue.ToString(),
                "U",
                (ChkActive.Checked == true ? 1 : 0),
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
            string str;
            try
            {
                if (txtMiscName.Text.Trim() == "" || txtMiscName.Text.Trim() == "-" || txtMiscName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Miscellaneous Name");
                    txtMiscName.Focus();
                    return true;
                }

                if (cboGroupType.SelectedValue == null || cboGroupType.SelectedValue.ToString() == "-" || cboGroupType.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Group Type");
                    cboGroupType.Focus();
                    return true;
                }

                if (!cboGroupType.IsValidSelect)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Valid Type");
                    cboGroupType.Focus();

                }

                if (DBSp.rtnAction())
                {
                    str = "tbl_MiscMaster";
                    if (Navigate.CheckDuplicate(ref str, "MiscName", txtMiscName.Text.Trim(), false, "", 0L, "TypeID=" + cboGroupType.SelectedValue, "This MiscName is already available"))
                    {
                        txtMiscName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtMiscName.Text.Trim(), false, "", 0L, "TypeID=" + cboGroupType.SelectedValue, "This MiscName is already Used in AliasName"))
                    {
                        txtMiscName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "MiscName", txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in MiscName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    str = "tbl_MiscMaster";
                    if (Navigate.CheckDuplicate(ref str, "MiscName", txtMiscName.Text.Trim(), true, "MiscID", Localization.ParseNativeLong(txtCode.Text.Trim()), "", "This MiscName is already available"))
                    {
                        txtMiscName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "Aliasname", txtAliasName.Text.Trim(), true, "MiscID", Localization.ParseNativeLong(txtCode.Text.Trim()), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "Aliasname", txtMiscName.Text.Trim(), true, "MiscID", Localization.ParseNativeLong(txtCode.Text.Trim()), "", "This MiscName is already Used in AliasName"))
                    {
                        txtMiscName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "MiscName", txtAliasName.Text.Trim(), true, "MiscID", Localization.ParseNativeLong(txtCode.Text.Trim()), "", "This AliasName is already Used in MiscName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtMiscName, txtAliasName, "tbl_MiscMaster", "MiscName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtMiscName, txtAliasName, "tbl_MiscMaster", "MiscName"))
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

        private void txtMiscName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtMiscName, txtAliasName, "tbl_MiscMaster", "MiscName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtMiscName, txtAliasName, "tbl_MiscMaster", "MiscName");
        }
    }
}
