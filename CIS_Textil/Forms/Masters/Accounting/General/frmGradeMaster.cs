using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using System.Data;

namespace CIS_Textil
{
    public partial class frmGradeMaster : frmMasterIface
    {
        public frmGradeMaster()
        {
            InitializeComponent();
        }

        #region Event

        private void frmGradeMaster_Load(object sender, EventArgs e)
        {
            try
            {
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtGradeName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtGradeName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtGradeName.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "GradeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtGradeName, "GradeName", Enum_Define.ValidationType.Text);
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
                sComboAddText = txtGradeName.Text;
                ArrayList pArrayData = new ArrayList
                {
                (txtGradeName.Text.Trim()),
                (txtAliasName.Text==""? null : txtAliasName.Text),
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
                if (txtGradeName.Text.Trim() == "" || txtGradeName.Text.Trim() == "-" || txtGradeName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Grade Name");
                    txtGradeName.Focus();
                    return true;
                }
                string str = string.Empty; 
                if (DBSp.rtnAction())
                {
                    str = "tbl_GradeMaster";
                    if (Navigate.CheckDuplicate(ref str, "GradeName", txtGradeName.Text.Trim(), false, "", 0L, "", "This GradeName is already available"))
                    {
                        txtGradeName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtGradeName.Text.Trim(), false, "", 0L, "", "This GradeName is already Used in AliasName"))
                    {
                        txtGradeName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "GradeName", txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in GradeName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    str = "tbl_GradeMaster";
                    if (Navigate.CheckDuplicate(ref str, "GradeName", txtGradeName.Text.Trim(), true, "GradeID", Localization.ParseNativeLong(txtCode.Text.Trim()), "", "This GradeName is already available"))
                    {
                        txtGradeName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "Aliasname", txtAliasName.Text.Trim(), true, "GradeID", Localization.ParseNativeLong(txtCode.Text.Trim()), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "Aliasname", txtGradeName.Text.Trim(), true, "GradeID", Localization.ParseNativeLong(txtCode.Text.Trim()), "", "This GradeName is already Used in AliasName"))
                    {
                        txtGradeName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "GradeName", txtAliasName.Text.Trim(), true, "GradeID", Localization.ParseNativeLong(txtCode.Text.Trim()), "", "This AliasName is already Used in GradeName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtGradeName, txtAliasName, "tbl_GradeMaster", "GradeName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtGradeName, txtAliasName, "tbl_GradeMaster", "GradeName"))
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

        private void txtGradeName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtGradeName, txtAliasName, "tbl_GradeMaster", "GradeName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtGradeName, txtAliasName, "tbl_GradeMaster", "GradeName");
        }
    }
}
