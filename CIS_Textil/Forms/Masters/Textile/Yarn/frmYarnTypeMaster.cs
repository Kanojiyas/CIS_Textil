using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;

namespace CIS_Textil
{
    public partial class frmYarnTypeMaster : frmMasterIface
    {
        public frmYarnTypeMaster()
        {
            InitializeComponent();
        }

        #region Form Events
        private void frmYarnTypeMaster_Load(object sender, EventArgs e)
        {
            try
            {
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtYarnType.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtYarnType.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtYarnType.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "YarnTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtYarnType, "YarnType", Enum_Define.ValidationType.Text);
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
                sComboAddText = txtYarnType.Text;
                ArrayList pArrayData = new ArrayList
                {
                    txtYarnType.Text.Trim(),
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
                string strTableName = Db_Detials.fn_YarnTypeMaster_Tbl;
                if (txtYarnType.Text.Trim() == "" || txtYarnType.Text.Trim() == "-" || txtYarnType.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Yarn Type");
                    txtYarnType.Focus();
                    return true;
                }
                if (DBSp.rtnAction())
                {
                    
                    if (Navigate.CheckDuplicate(ref strTableName, "YarnType", txtYarnType.Text.Trim(), false, "", 0L, "", "This YarnType is already available"))
                    {
                        txtYarnType.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", txtYarnType.Text.Trim(), false, "", 0L, "", "This YarnType is already Used in AliasName"))
                    {
                        txtYarnType.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "YarnType", txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in YarnType"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    
                    if (Navigate.CheckDuplicate(ref strTableName, "YarnType", txtYarnType.Text.Trim(), true, "YarnTypeID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This YarnType is already available"))
                    {
                        txtYarnType.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", txtAliasName.Text.Trim(), true, "YarnTypeID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", txtYarnType.Text.Trim(), true, "YarnTypeID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This YarnType is already Used in AliasName"))
                    {
                        txtYarnType.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "YarnType", txtAliasName.Text.Trim(), true, "YarnTypeID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This AliasName is already Used in YarnType"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtYarnType, txtAliasName, "tbl_YarnTypeMaster", "YarnType"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtYarnType, txtAliasName, "tbl_YarnTypeMaster", "YarnType"))
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
                txtYarnType.Focus();
                ApplyActStatus();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
        #endregion

        private void txtYarnType_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtYarnType, txtAliasName, "tbl_YarnTypeMaster", "YarnType");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtYarnType, txtAliasName, "tbl_YarnTypeMaster", "YarnType");
        }
    }
}
