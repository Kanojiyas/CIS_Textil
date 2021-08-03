using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;

namespace CIS_Textil
{
    public partial class frmMiscTypeMaster : frmMasterIface
    {
        public frmMiscTypeMaster()
        {
            InitializeComponent();
        }

        #region Event

        private void frmMiscTypeMaster_Load(object sender, EventArgs e)
        {
            try
            {
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtMiscType.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtMiscType.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtMiscType.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "MiscTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMiscType, "MiscType", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "Active", Enum_Define.ValidationType.Text);
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
                ArrayList pArrayData = new ArrayList
                {
                txtMiscType.Text.Trim(),
                txtAliasName.Text==""?null:txtAliasName.Text,
                0,
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
            try
            {
                string str;
                if (txtMiscType.Text.Trim() == "" || txtMiscType.Text.Trim() == "-" || txtMiscType.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Miscellaneous Type");
                    this.txtMiscType.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    str = "tbl_MiscTypeMaster";
                    if (Navigate.CheckDuplicate(ref str, "MiscType", this.txtMiscType.Text.Trim(), false, "", 0L, "", "This MiscType is already available"))
                    {
                        this.txtMiscType.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "txtAliasName", this.txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "txtAliasName", this.txtMiscType.Text.Trim(), false, "", 0L, "", "This MiscType is already Used in AliasName"))
                    {
                        this.txtMiscType.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "MiscType", this.txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in MiscType"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    str = "tbl_MiscTypeMaster";
                    if (Navigate.CheckDuplicate(ref str, "MiscType", this.txtMiscType.Text.Trim(), true, "MiscTypeID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This MiscType is already available"))
                    {
                        this.txtMiscType.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", this.txtAliasName.Text.Trim(), true, "MiscTypeID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", this.txtMiscType.Text.Trim(), true, "MiscTypeID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This MiscType is already Used in AliasName"))
                    {
                        this.txtMiscType.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "MiscType", this.txtAliasName.Text.Trim(), true, "MiscTypeID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This AliasName is already Used in MiscType"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtMiscType, txtAliasName, "tbl_MiscTypeMaster", "MiscType"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtMiscType, txtAliasName, "tbl_MiscTypeMaster", "MiscType"))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
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

        private void txtMiscType_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtMiscType, txtAliasName, "tbl_MiscTypeMaster", "MiscType");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtMiscType, txtAliasName, "tbl_MiscTypeMaster", "MiscType");
        }

    }
}
