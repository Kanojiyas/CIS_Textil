using System;
using System.Collections;
using System.Windows.Forms;
using  CIS_Bussiness;using CIS_DBLayer;

namespace CIS_Textil
{

    public partial class frmCurrencyMaster : frmMasterIface
    {
        public frmCurrencyMaster()
        {
            InitializeComponent();
        }

        #region FormEvent

        private void frmCurrencyMaster_Load(object sender, EventArgs e)
        {
            try
            {
                ChkActive.LostFocus += new EventHandler(EventHandles.OnSave_KeyEnter);
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtCurrency.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtCurrency.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtCurrency.Focus();
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

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                txtCurrency.Focus();
                ApplyActStatus();
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
                DBValue.Return_DBValue(this, txtCode, "CurrencyID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCurrency, "CurrencyName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtSymbol, "CurrencySymbol", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDecimal, "NoofDecimals", Enum_Define.ValidationType.Text);
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
                sComboAddText = txtCurrency.Text;
                ArrayList pArrayData = new ArrayList
                {
                    txtCurrency.Text.Trim(),
                    txtAliasName.Text==""?null:txtAliasName.Text,
                    txtSymbol.Text.Trim(),
                    txtDecimal.Text.Trim(),
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
                if (txtCurrency.Text.Trim() == "" || txtCurrency.Text.Trim() == "-" || txtCurrency.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Currency");
                    txtCurrency.Focus();
                    return true;
                }
                if (txtSymbol.Text.Trim() == "" || txtSymbol.Text.Trim() == "-" || txtSymbol.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Currency Symbol");
                    txtSymbol.Focus();
                    return true;
                }
                if (txtDecimal.Text.Trim() == "" || txtDecimal.Text.Trim() == "-" || txtDecimal.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Decimal Places");
                    txtDecimal.Focus();
                    return true;
                }
                string strTblName = "tbl_CurrencyMaster";
                if (DBSp.rtnAction())
                {
                    strTblName = "tbl_CurrencyMaster";
                    if (Navigate.CheckDuplicate(ref strTblName, "CurrencyName", txtCurrency.Text, false, "", 0, "", "This CurrencyName is already available"))
                    {
                        txtCurrency.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtAliasName.Text, false, "", 0, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtCurrency.Text, false, "", 0, "", "This CurrencyName is already Used in AliasName"))
                    {
                        txtCurrency.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "CurrencyName", txtAliasName.Text, false, "", 0, "", "This AliasName is already Used in CurrencyName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    strTblName = "tbl_CurrencyMaster";
                    if (Navigate.CheckDuplicate(ref strTblName, "CurrencyName", txtCurrency.Text, true, "CurrencyId", Localization.ParseNativeLong(txtCode.Text), "", "This CurrencyName is already available"))
                    {
                        txtCurrency.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtAliasName.Text, true, "CurrencyId", Localization.ParseNativeLong(txtCode.Text), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtCurrency.Text, true, "CurrencyId", Localization.ParseNativeLong(txtCode.Text), "", "This CurrencyName is already Used in AliasName"))
                    {
                        txtCurrency.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "CurrencyName", txtAliasName.Text, true, "CurrencyId", Localization.ParseNativeLong(txtCode.Text), "", "This AliasName is already Used in CurrencyName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtCurrency, txtAliasName, "tbl_CurrencyMaster", "CurrencyName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtCurrency, txtAliasName, "tbl_CurrencyMaster", "CurrencyName"))
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

        private void txtCurrency_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtCurrency, txtAliasName, "tbl_CurrencyMaster", "CurrencyName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtCurrency, txtAliasName, "tbl_CurrencyMaster", "CurrencyName");
        }
    }
}
