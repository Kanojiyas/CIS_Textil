using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmBranchMaster : frmMasterIface
    {
        public frmBranchMaster()
        {
            InitializeComponent();
        }

        private void frmBranchMaster_Load(object sender, EventArgs e)
        {
            try
            {
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtBranchName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtBranchName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtBranchName.Focus();
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

        #region FormEvent

        public void MovetoField()
        {
            try
            {
                txtBranchName.Focus();
                ApplyActStatus();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
        #endregion FormEvent

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
                DBValue.Return_DBValue(this, txtCode, "IBranchID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtBranchName, "BranchName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAddress, "Address", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtphone, "PhoneNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
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
                sComboAddText = txtBranchName.Text;
                ArrayList pArrayData = new ArrayList
                {
                    txtBranchName.Text.Trim(),
                    txtAliasName.Text==""?null:txtAliasName.Text,
                    txtAddress.Text.Trim(),
                    txtphone.Text.Trim(),
                    (ChkActive.Checked?1:0)
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
                string strTblName;
                if (txtBranchName.Text.Trim() == "" || txtBranchName.Text.Trim() == "-" || txtBranchName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Branch Name");
                    txtBranchName.Focus();
                    return true;
                }
                if (txtAddress.Text.Trim() == "" || txtAddress.Text.Trim() == "-" || txtAddress.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Address");
                    txtAddress.Focus();
                    return true;
                }
                if (txtphone.Text.Trim() == "" || txtphone.Text.Trim() == "-" || txtphone.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Phone Number");
                    txtphone.Focus();
                    return true;
                }
                if (DBSp.rtnAction())
                {
                    strTblName = "tbl_BranchMaster";
                    if (Navigate.CheckDuplicate(ref strTblName, "BranchName", txtBranchName.Text.Trim(), false, "", 0, "", "This BranchName is already available"))
                    {
                        txtBranchName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "Aliasname", txtAliasName.Text.Trim(), false, "", 0, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "Aliasname", txtBranchName.Text.Trim(), false, "", 0, "", "This BranchName is already Used in AliasName"))
                    {
                        txtBranchName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "BranchName", txtAliasName.Text.Trim(), false, "", 0, "", "This AliasName is already Used in BranchName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    strTblName = "tbl_BranchMaster";
                    if (Navigate.CheckDuplicate(ref strTblName, "BranchName", txtBranchName.Text.Trim(), true, "BranchID", Localization.ParseNativeLong(txtCode.Text), "", "This BranchName is already available"))
                    {
                        txtBranchName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtAliasName.Text.Trim(), true, "BranchID", Localization.ParseNativeLong(txtCode.Text), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtBranchName.Text.Trim(), true, "BranchID", Localization.ParseNativeLong(txtCode.Text), "", "This BranchName is already Used in AliasName"))
                    {
                        txtBranchName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "BranchName", txtAliasName.Text.Trim(), true, "BranchID", Localization.ParseNativeLong(txtCode.Text), "", "This AliasName is already Used in BranchName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtBranchName, txtAliasName, "tbl_BranchMaster", "BranchName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtBranchName, txtAliasName, "tbl_BranchMaster", "BranchName"))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                return false;
            }
        }

        #endregion Navigation

        private void txtBranchName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtBranchName, txtAliasName, "tbl_BranchMaster", "BranchName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtBranchName, txtAliasName, "tbl_BranchMaster", "BranchName");
        }

    }
}
