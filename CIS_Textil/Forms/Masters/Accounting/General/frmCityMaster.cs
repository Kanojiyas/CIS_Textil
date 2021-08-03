using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using System.Data;

namespace CIS_Textil
{
    public partial class frmCityMaster : frmMasterIface
    {
        public frmCityMaster()
        {
            base.Load += new EventHandler(this.frm_Load);
            InitializeComponent();
        }

        #region Event

        private void frm_Load(object sender, EventArgs e)
        {
            try
            {
                FillComboBox();
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
                        txtCityName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtCityName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtCityName.Focus();
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
        public void FillComboBox()
        {
            CommonCls.AutoCompleteText(this.strTableName, "CityName", ref txtCityName);
            CommonCls.AutoCompleteText(this.strTableName, "AliasName", ref txtAliasName);
            CommonCls.AutoCompleteText(this.strTableName, "RegLangName", ref txtRegName);
            CommonCls.AutoCompleteText(this.strTableName, "ET1", ref txtET1);
            CommonCls.AutoCompleteText(this.strTableName, "ET2", ref txtET2);
            CommonCls.AutoCompleteText(this.strTableName, "ET3", ref txtET3);
        }

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "CityId", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCityName, "CityName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtRegName, "RegLangName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboCountry, "CountryID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboStateName, "StateID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDistrict, "DistrictID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
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
                    txtCityName.Text.Trim(),
                    txtRegName.Text,
                    cboCountry.SelectedValue,
                    cboStateName.SelectedValue,
                    cboDistrict.SelectedValue,
                    txtAliasName.Text.Trim(),
                    (ChkActive.Checked?1:0),
                    cboEI1.SelectedValue==null?0:cboEI1.SelectedValue,
                    cboEI2.SelectedValue==null?0:cboEI2.SelectedValue,
                    txtET1.Text,
                    txtET2.Text,
                    txtET3.Text
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
                if (txtCityName.Text.Trim() == "" || txtCityName.Text.Trim() == "-" || txtCityName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter City Name");
                    return true;
                }
                if (cboCountry.SelectedValue == null || cboCountry.SelectedValue.ToString() == "" || cboCountry.SelectedValue.ToString() == "-" || cboCountry.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Country");
                    this.cboCountry.Focus();
                    return true;
                }
                if (cboStateName.SelectedValue == null || cboStateName.SelectedValue.ToString() == "" || cboStateName.SelectedValue.ToString() == "-" || cboStateName.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select State");
                    this.cboStateName.Focus();
                    return true;
                }
                if (cboDistrict.SelectedValue == null || cboDistrict.SelectedValue.ToString() == "" || cboDistrict.SelectedValue.ToString() == "-" || cboDistrict.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select District");
                    this.cboDistrict.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    str = "tbl_CityMaster";
                    if (Navigate.CheckDuplicate(ref str, "CityName", txtCityName.Text.Trim(), false, "", 0L, "CountryID=" + cboCountry.SelectedValue + " and StateID= " + cboStateName.SelectedValue + " and DistrictID= " + cboDistrict.SelectedValue, "This CityName is already available"))
                    {
                        txtCityName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtCityName.Text.Trim(), false, "", 0L, "CountryID=" + cboCountry.SelectedValue + " and StateID= " + cboStateName.SelectedValue + " and DistrictID= " + cboDistrict.SelectedValue, "This CityName is already Used in AliasName"))
                    {
                        txtCityName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "CityName", txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in CityName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    str = "tbl_CityMaster";
                    if (Navigate.CheckDuplicate(ref str, "CityName", txtCityName.Text.Trim(), true, "CityId", Localization.ParseNativeLong(txtCode.Text.Trim()), "CountryID=" + cboCountry.SelectedValue + " and StateID= " + cboStateName.SelectedValue + " and DistrictID= " + cboDistrict.SelectedValue, "This CityName is already available"))
                    {
                        txtCityName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "Aliasname", txtAliasName.Text.Trim(), true, "CityId", Localization.ParseNativeLong(txtCode.Text.Trim()), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "Aliasname", txtCityName.Text.Trim(), true, "CityId", Localization.ParseNativeLong(txtCode.Text.Trim()), "CountryID=" + cboCountry.SelectedValue + " and StateID= " + cboStateName.SelectedValue + " and DistrictID= " + cboDistrict.SelectedValue, "This CityName is already Used in AliasName"))
                    {
                        txtCityName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "CityName", txtAliasName.Text.Trim(), true, "CityId", Localization.ParseNativeLong(txtCode.Text.Trim()), "", "This AliasName is already Used in CityName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtCityName, txtAliasName, "tbl_CityMaster", "CityName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtCityName, txtAliasName, "tbl_CityMaster", "CityName"))
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
                FillComboBox();
                Combobox_Setup.FillCbo(ref cboCountry, Combobox_Setup.ComboType.Mst_Country, "");
                Combobox_Setup.FillCbo(ref cboDistrict, Combobox_Setup.ComboType.Mst_District, "");
                Combobox_Setup.FillCbo(ref cboStateName, Combobox_Setup.ComboType.Mst_State, "");
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

        private void txtState_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtCityName, txtAliasName, "tbl_CityMaster", "CityName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtCityName, txtAliasName, "tbl_CityMaster", "CityName");
        }

        private void cboDistrict_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboDistrict.SelectedValue != null && cboDistrict.SelectedValue.ToString() != "0" && cboDistrict.SelectedValue.ToString() != "-" && cboDistrict.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    Combobox_Setup.FillCbo(ref cboStateName, Combobox_Setup.ComboType.Mst_State, " DistrictID=" + cboDistrict.SelectedValue.ToString() + "");
                    cboStateName.SelectedValue = DB.GetSnglValue("select StateId from fn_DistrictMaster_Tbl() Where DistrictId=" + cboDistrict.SelectedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboStateName_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboStateName.SelectedValue != null && cboStateName.SelectedValue.ToString() != "0" && cboStateName.SelectedValue.ToString() != "-" && cboStateName.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    Combobox_Setup.FillCbo(ref cboCountry, Combobox_Setup.ComboType.Mst_Country, " StateID=" + cboStateName.SelectedValue.ToString() + "");
                    cboCountry.SelectedValue = DB.GetSnglValue("select CountryID from fn_StateMaster_Tbl() Where StateID=" + cboStateName.SelectedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
    }
}
