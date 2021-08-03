using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmContractorName : frmMasterIface
    {
        public frmContractorName()
        {
            InitializeComponent();
        }

        private void frmContractorName_Load(object sender, EventArgs e)
        {
            try
            {
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtContName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtContName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtContName.Focus();
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
                txtContName.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "ContractorID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtContName, "ContractorName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAddress, "Address", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtphone, "PhoneNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtrate, "Rate", Enum_Define.ValidationType.Text);
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
                sComboAddText = txtContName.Text;
                ArrayList pArrayData = new ArrayList
                {
                txtContName.Text.Trim(),
                txtAliasName.Text==""?null:txtAliasName.Text,
                txtAddress.Text.Trim(),
                txtphone.Text.Trim(),
                txtrate.Text.Trim(),
                (ChkActive.Checked?1:0),
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
                string strTblName;
                if (txtContName.Text.Trim() == "" || txtContName.Text.Trim() == "-" || txtContName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Contractor Name");
                    txtContName.Focus();
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
                if (txtrate.Text.Trim() == "" || txtrate.Text.Trim() == "-" || txtrate.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Rate");
                    txtrate.Focus();
                    return true;
                }
                if (DBSp.rtnAction())
                {
                    strTblName = "tbl_ContractorMaster";
                    if (Navigate.CheckDuplicate(ref strTblName, "ContractorName", txtContName.Text.Trim(), false, "", 0, "", "This ContractorName is already available"))
                    {
                        txtContName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtAliasName.Text.Trim(), false, "", 0, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtContName.Text.Trim(), false, "", 0, "", "This ContractorName is already Used in AliasName"))
                    {
                        txtContName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "ContractorName", txtAliasName.Text.Trim(), false, "", 0, "", "This AliasName is already Used in ContractorName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    strTblName = "tbl_ContractorMaster";
                    if (Navigate.CheckDuplicate(ref strTblName, "ContractorName", txtContName.Text.Trim(), true, "ContractorID", Localization.ParseNativeLong(txtCode.Text), "", "This ContractorName is already available"))
                    {
                        txtContName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtAliasName.Text.Trim(), true, "ContractorID", Localization.ParseNativeLong(txtCode.Text), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtContName.Text.Trim(), true, "ContractorID", Localization.ParseNativeLong(txtCode.Text), "", "This ContractorName is already Used in AliasName"))
                    {
                        txtContName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "ContractorName", txtAliasName.Text.Trim(), true, "ContractorID", Localization.ParseNativeLong(txtCode.Text), "", "This AliasName is already Used in ContractorName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtContName, txtAliasName, "tbl_ContractorMaster", "ContractorName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtContName, txtAliasName, "tbl_ContractorMaster", "ContractorName"))
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

        private void txtContName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtContName, txtAliasName, "tbl_ContractorMaster", "ContractorName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtContName, txtAliasName, "tbl_ContractorMaster", "ContractorName");
        }
    }
}
