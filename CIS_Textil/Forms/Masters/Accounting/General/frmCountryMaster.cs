using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;
using System.Data;

namespace CIS_Textil
{
    public partial class frmCountryMaster : frmMasterIface
    {
        public frmCountryMaster()
        {
            InitializeComponent();
        }

        #region Event
        private void frm_Load(object sender, EventArgs e)
        {
            try
            {
                CommonCls.AutoCompleteText(this.strTableName, "CountryName", ref txtCountry);
                CommonCls.AutoCompleteText(this.strTableName, "AliasName", ref txtAliasName);
                CommonCls.AutoCompleteText(this.strTableName, "RegLangName", ref txtRegName);
                CommonCls.AutoCompleteText(this.strTableName, "ET1", ref txtET1);
                CommonCls.AutoCompleteText(this.strTableName, "ET2", ref txtET2);
                CommonCls.AutoCompleteText(this.strTableName, "ET3", ref txtET3);

                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    FillControls();
                }
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtCountry.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtCountry.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtCountry.Focus();
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

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "CountryID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCountry, "CountryName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtRegName, "RegLangName", Enum_Define.ValidationType.Text);
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
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
            ApplyActStatus();
        }

        public void SaveRecord()
        {
            try
            {
                ArrayList pArrayData = new ArrayList
                {               
                    (txtCountry.Text.Trim()),
                    (txtRegName.Text.Trim()),
                    (txtAliasName.Text.Trim()),
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
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                string str;
                if (txtCountry.Text.Trim() == "" || txtCountry.Text.Trim() == "-" || txtCountry.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Country Name");
                    txtCountry.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    str = "tbl_CountryMaster";
                    if (Navigate.CheckDuplicate(ref str, "CountryName", txtCountry.Text.Trim(), false, "", 0, "", "This CountryName is already available"))
                    {
                        txtCountry.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "Aliasname", txtAliasName.Text.Trim(), false, "", 0, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "Aliasname", txtCountry.Text.Trim(), false, "", 0, "", "This CountryName is already Used in AliasName"))
                    {
                        txtCountry.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "CountryName", txtAliasName.Text.Trim(), false, "", 0, "", "This AliasName is already Used in CountryName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    str = "tbl_CountryMaster";
                    if (Navigate.CheckDuplicate(ref str, "CountryName", txtCountry.Text.Trim(), true, "CountryID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This CountryName is already available"))
                    {
                        txtCountry.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "Aliasname", txtAliasName.Text.Trim(), true, "CountryID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "Aliasname", txtCountry.Text.Trim(), true, "CountryID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This CountryName is already Used in AliasName"))
                    {
                        txtCountry.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "CountryName", txtAliasName.Text.Trim(), true, "CountryID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This AliasName is already Used in CountryName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtCountry, txtAliasName, "tbl_CountryMaster", "CountryName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtCountry, txtAliasName, "tbl_CountryMaster", "CountryName"))
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

        private void txtcountry_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtCountry, txtAliasName, "tbl_CountryMaster", "CountryName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtCountry, txtAliasName, "tbl_CountryMaster", "CountryName");
        }
    }
}
