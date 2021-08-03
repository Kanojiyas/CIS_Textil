using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;

namespace CIS_Textil
{
    public partial class frmTaskTypeMaster : frmMasterIface
    {
        public frmTaskTypeMaster()
        {
            InitializeComponent();
        }

        #region Form Events

        private void frmTaskMaster_Load(object sender, EventArgs e)
        {
            try
            {
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtTaskTypeName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtTaskTypeName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtTaskTypeName.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "TaskTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtTaskTypeName, "TaskTypeName", Enum_Define.ValidationType.Text);
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
                sComboAddText = txtTaskTypeName.Text;
                ArrayList pArrayData = new ArrayList
                {
                    txtTaskTypeName.Text.Trim(),
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
            catch (Exception exception1)
            {
                this.IsMasterAdded = false;
                Navigate.logError(exception1.Message, exception1.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                string strTableName;
                if (txtTaskTypeName.Text.Trim() == "" || txtTaskTypeName.Text.Trim() == "-" || txtTaskTypeName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Task Name");
                    txtTaskTypeName.Focus();
                    return true;
                }
                if (DBSp.rtnAction())
                {
                    strTableName = "tbl_TaskTypeMaster";
                    if (Navigate.CheckDuplicate(ref strTableName, "TaskTypeName", this.txtTaskTypeName.Text.Trim(), false, "", 0L, "", "This TasktypeName is already available"))
                    {
                        this.txtTaskTypeName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", this.txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", this.txtTaskTypeName.Text.Trim(), false, "", 0L, "", "This TasktypeName is already Used in AliasName"))
                    {
                        this.txtTaskTypeName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "TaskTypeName", this.txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in TasktypeName"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    strTableName = "tbl_TaskTypeMaster";
                    if (Navigate.CheckDuplicate(ref strTableName, "TaskTypeName", this.txtTaskTypeName.Text.Trim(), true, "TaskTypeID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This TasktypeName is already available"))
                    {
                        txtTaskTypeName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", this.txtAliasName.Text.Trim(), true, "TaskTypeID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", this.txtTaskTypeName.Text.Trim(), true, "TaskTypeID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This TasktypeName is already Used in AliasName"))
                    {
                        txtTaskTypeName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "TaskTypeName", this.txtAliasName.Text.Trim(), true, "TaskTypeID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This AliasName is already Used in TasktypeName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtTaskTypeName, txtAliasName, "tbl_TaskTypeMaster", "TaskTypeName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtTaskTypeName, txtAliasName, "tbl_TaskTypeMaster", "TaskTypeName"))
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

        private void txtTaskTypeName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtTaskTypeName, txtAliasName, "tbl_TaskTypeMaster", "TaskTypeName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtTaskTypeName, txtAliasName, "tbl_TaskTypeMaster", "TaskTypeName");
        }
    }
}
