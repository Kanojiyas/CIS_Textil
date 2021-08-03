using System;
using System.Collections;
using System.Windows.Forms;
using CIS_DBLayer;
using CIS_Bussiness;
using CIS_CLibrary;

namespace CIS_Textil
{
    public partial class frmAdvanceMaster : frmMasterIface
    {
        public frmAdvanceMaster()
        {
            InitializeComponent();
        }

        private void frmAdvanceMaster_Load(object sender, EventArgs e)
        {
            try
            {
                FillControls();
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtAdvName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtAdvName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtAdvName.Focus();
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
                ApplyActStatus();
                txtCode.Text = "";
                txtAdvName.Focus();
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
           
        }

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "AdvanceID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAdvName, "AdvanceName", Enum_Define.ValidationType.Text);
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
                sComboAddText = txtAdvName.Text;
                ArrayList pArrayData = new ArrayList
                {
                txtAdvName.Text.Trim(),
                txtAliasName.Text== "" ? null : txtAliasName.Text,
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
                if (txtAdvName.Text.Trim() == "" || txtAdvName.Text.Trim() == "-" || txtAdvName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter AdvanceName");
                    txtAdvName.Focus();
                    return true;
                }
                if (DBSp.rtnAction())
                {
                    str = "tbl_AdvanceMaster";
                    if (Navigate.CheckDuplicate(ref str, "AdvanceName", txtAdvName.Text.Trim(), false, "", 0L, "", "This AdvanceName is already available"))
                    {
                        txtAdvName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAdvName.Text.Trim(), false, "", 0L, "", "This AdvanceName is already Used in AliasName"))
                    {
                        txtAdvName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AdvanceName", txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in AdvanceName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    str = "tbl_AdvanceMaster";
                    if (Navigate.CheckDuplicate(ref str, "AdvanceName", txtAdvName.Text.Trim(), true, "AdvanceID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This AdvanceName is already available"))
                    {
                        txtAdvName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), true, "AdvanceID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAdvName.Text.Trim(), true, "AdvanceID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This AdvanceName is already Used in AliasName"))
                    {
                        txtAdvName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AdvanceName", txtAliasName.Text.Trim(), true, "AdvanceID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This AliasName is already Used in AdvanceName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtAdvName, txtAliasName, "tbl_AdvanceMaster", "AdvanceName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtAdvName, txtAliasName, "tbl_AdvanceMaster", "AdvanceName"))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                return false;
            }
        }

        private void txtAdvName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtAdvName, txtAliasName, "tbl_AdvanceMaster", "AdvanceName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtAdvName, txtAliasName, "tbl_AdvanceMaster", "AdvanceName");
        }
    }
}
