using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;
using System.Data;

namespace CIS_Textil
{
    public partial class frmDistrictMaster : frmMasterIface
    {
        public frmDistrictMaster()
        {
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
                        txtDistrictName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtDistrictName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtDistrictName.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "DistrictID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDistrictName, "Districtname", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtRegName, "RegLangName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboCountry, "CountryID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboStateName, "StateID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
            }
            catch (Exception ex)
            {
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
                    cboCountry.SelectedValue,
                    cboStateName.SelectedValue,
                    txtDistrictName.Text.Trim(),
                    txtRegName.Text.Trim(),
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
                if (txtDistrictName.Text.Trim() == "" || txtDistrictName.Text.Trim() == "-" || txtDistrictName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter District Name");
                    txtDistrictName.Focus();
                    return true;
                }

                if (cboStateName.SelectedValue == null || cboStateName.SelectedValue.ToString() == "-" || cboStateName.SelectedValue.ToString() == "" || cboStateName.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select State");
                    this.cboStateName.Focus();
                    return true;
                }

                if (cboCountry.SelectedValue == null || cboCountry.SelectedValue.ToString() == "-" || cboCountry.SelectedValue.ToString() == "" || cboCountry.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Country");
                    this.cboStateName.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    str = "tbl_DistrictMaster";
                    if (Navigate.CheckDuplicate(ref str, "DistrictName", txtDistrictName.Text.Trim(), false, "", 0, "CountryID=" + cboCountry.SelectedValue + "and StateID=" + cboStateName.SelectedValue, "This DistrictName is already available"))
                    {
                        txtDistrictName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "Aliasname", txtAliasName.Text.Trim(), false, "", 0, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "Aliasname", txtDistrictName.Text.Trim(), false, "", 0, "CountryID=" + cboCountry.SelectedValue + "and StateID=" + cboStateName.SelectedValue, "This DistrictName is already Used in AliasName"))
                    {
                        txtDistrictName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "DistrictName", txtAliasName.Text.Trim(), false, "", 0, "", "This AliasName is already Used in DistrictName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    str = "tbl_DistrictMaster";
                    if (Navigate.CheckDuplicate(ref str, "DistrictName", txtDistrictName.Text.Trim(), true, "DistrictID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "CountryID=" + cboCountry.SelectedValue + "and StateID=" + cboStateName.SelectedValue, "This DistrictName is already available"))
                    {
                        txtDistrictName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), true, "DistrictID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtDistrictName.Text.Trim(), true, "DistrictID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "CountryID=" + cboCountry.SelectedValue + "and StateID=" + cboStateName.SelectedValue, "This DistrictName is already Used in AliasName"))
                    {
                        txtDistrictName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "DistrictName", txtAliasName.Text.Trim(), true, "DistrictID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This AliasName is already Used in DistrictName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtDistrictName, txtAliasName, "tbl_DistrictMaster", "DistrictName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtDistrictName, txtAliasName, "tbl_DistrictMaster", "DistrictName"))
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
                CommonCls.AutoCompleteText(this.strTableName, "DistrictName", ref txtDistrictName);
                CommonCls.AutoCompleteText(this.strTableName, "AliasName", ref txtAliasName);
                CommonCls.AutoCompleteText(this.strTableName, "RegLangName", ref txtRegName);
                CommonCls.AutoCompleteText(this.strTableName, "ET1", ref txtET1);
                CommonCls.AutoCompleteText(this.strTableName, "ET2", ref txtET2);
                CommonCls.AutoCompleteText(this.strTableName, "ET3", ref txtET3);

                ApplyActStatus();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #endregion

        public void FillComboBox()
        {
            Combobox_Setup.FillCbo(ref cboCountry, Combobox_Setup.ComboType.Mst_Country, "");
            Combobox_Setup.FillCbo(ref cboStateName, Combobox_Setup.ComboType.Mst_State_DistrictForm, "");
        }

        private void txtDistrict_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtDistrictName, txtAliasName, "tbl_DistrictMaster", "DistrictName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtDistrictName, txtAliasName, "tbl_DistrictMaster", "DistrictName");
        }

        private void cboStateName_SelectedValueChanged(object sender, EventArgs e)
        {
            if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
            {
                if (cboStateName.SelectedValue != null && cboStateName.SelectedValue.ToString() != "0" && cboStateName.SelectedValue.ToString() != "-" && cboStateName.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    Combobox_Setup.FillCbo(ref cboCountry, Combobox_Setup.ComboType.Mst_Country, "StateID=" + cboStateName.SelectedValue.ToString() + "");
                    cboCountry.SelectedValue = DB.GetSnglValue("select CountryID from fn_StateMaster_Tbl() Where StateId=" + cboStateName.SelectedValue.ToString());
                }
            }
        }
    }
}
