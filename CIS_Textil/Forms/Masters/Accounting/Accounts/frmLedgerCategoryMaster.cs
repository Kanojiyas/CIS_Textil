using System;
using System.Collections;
using System.Windows.Forms;
using  CIS_Bussiness;using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmLedgerCategoryMaster : frmMasterIface
    {
        public frmLedgerCategoryMaster()
        {
            InitializeComponent();
        }

        #region Form Events

        private void frmLedgerCategoryMaster_Load(object sender, EventArgs e)
        {
            try
            {
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtLedgerCategory.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtLedgerCategory.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtLedgerCategory.Focus();
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

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                txtLedgerCategory.Focus();
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
                sComboAddText = txtLedgerCategory.Text;

                ArrayList pArrayData = new ArrayList
                {
                    txtLedgerCategory.Text,
                    txtAliasName.Text==""?null:txtAliasName.Text,
                    ChkActive.Checked?1:0,
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
                if (txtLedgerCategory.Text.Trim() == "" || txtLedgerCategory.Text.Trim() == "-" || txtLedgerCategory.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Ledger Category");
                    txtLedgerCategory.Focus();
                    return true;
                }
                if (DBSp.rtnAction())
                {
                    strTblName = "tbl_LedgerCategoryMaster";
                    if (Navigate.CheckDuplicate(ref strTblName, "LedgerCategory", txtLedgerCategory.Text, false, "", 0, "", "This LedgerCategory is already available"))
                    {
                        txtLedgerCategory.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtAliasName.Text, false, "", 0, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtLedgerCategory.Text, false, "", 0, "", "This LedgerCategory is already Used in AliasName"))
                    {
                        txtLedgerCategory.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "LedgerCategory", txtAliasName.Text, false, "", 0, "", "This AliasName is already Used in LedgerCategory"))
                    {
                        txtAliasName.Focus(); 
                        return true;
                    }
                }
                else
                {
                    strTblName = "tbl_LedgerCategoryMaster";
                    if (Navigate.CheckDuplicate(ref strTblName, "LedgerCategory", txtLedgerCategory.Text, true, "LedgerCategoryID", Localization.ParseNativeLong(txtCode.Text), "", "This LedgerCategory is already available"))
                    {
                        txtLedgerCategory.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtAliasName.Text, true, "LedgerCategoryID", Localization.ParseNativeLong(txtCode.Text), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtLedgerCategory.Text, true, "LedgerCategoryID", Localization.ParseNativeLong(txtCode.Text), "", "This LedgerCategory is already Used in AliasName"))
                    {
                        txtLedgerCategory.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "LedgerCategory", txtAliasName.Text, true, "LedgerCategoryID", Localization.ParseNativeLong(txtCode.Text), "", "This AliasName is already Used in LedgerCategory"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                txtLedgerCategory_Leave(null, null);
                txtAliasName_Leave(null, null);
                return false;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                return false;
            }
        }

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "LedgerCategoryID", 0);
                DBValue.Return_DBValue(this, txtLedgerCategory, "LedgerCategory", 0);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", 0);
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
        #endregion

        private void txtLedgerCategory_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtLedgerCategory, txtAliasName, "tbl_LedgerCategoryMaster", "LedgerCategory");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtLedgerCategory, txtAliasName, "tbl_LedgerCategoryMaster", "LedgerCategory");
        }
    }
}
