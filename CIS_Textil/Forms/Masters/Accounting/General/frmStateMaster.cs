using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;
using System.Data;

namespace CIS_Textil
{
    public partial class frmStateMaster : frmMasterIface
    {
        public frmStateMaster()
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
                        txtstatename.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtstatename.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtstatename.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "StateID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtstatename, "StateName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtRegName, "RegionalName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboCountry, "CountryID", Enum_Define.ValidationType.Text);
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
                    txtstatename.Text.Trim(),
                    txtRegName.Text.Trim(),
                    txtAliasName.Text.Trim(),
                    cboCountry.SelectedValue,
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
                if (txtstatename.Text.Trim() == "" || txtstatename.Text.Trim() == "-" || txtstatename.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter State Name");
                    txtstatename.Focus();
                    return true;
                }

                if (cboCountry.SelectedValue == null || cboCountry.SelectedValue.ToString() == "-" || cboCountry.Text.Trim().Length.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Country Name");
                    cboCountry.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    str = "tbl_StateMaster";
                    if (Navigate.CheckDuplicate(ref str, "Statename", txtstatename.Text.Trim(), false, "", 0, "CountryId=" + cboCountry.SelectedValue, "This StateName is already available"))
                    {
                        txtstatename.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "Aliasname", txtAliasName.Text.Trim(), false, "", 0, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "Aliasname", txtstatename.Text.Trim(), false, "", 0, "CountryId=" + cboCountry.SelectedValue, "This StateName is already Used in AliasName"))
                    {
                        txtstatename.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "Statename", txtAliasName.Text.Trim(), false, "", 0, "", "This AliasName is already Used in StateName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    str = "tbl_StateMaster";
                    if (Navigate.CheckDuplicate(ref str, "Statename", txtstatename.Text.Trim(), true, "StateID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "CountryId=" + cboCountry.SelectedValue, "This StateName is already available"))
                    {
                        txtstatename.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), true, "StateID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtstatename.Text.Trim(), true, "StateID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "CountryId=" + cboCountry.SelectedValue, "This StateName is already Used in AliasName"))
                    {
                        txtstatename.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "Statename", txtAliasName.Text.Trim(), true, "StateID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This AliasName is already Used in StateName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtstatename, txtAliasName, "tbl_StateMaster", "StateName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtstatename, txtAliasName, "tbl_StateMaster", "StateName"))
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
                CommonCls.AutoCompleteText(this.strTableName, "StateName", ref txtstatename);
                CommonCls.AutoCompleteText(this.strTableName, "AliasName", ref txtAliasName);
                CommonCls.AutoCompleteText(this.strTableName, "RegionalName", ref txtRegName);
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
            Combobox_Setup.FillCbo(ref cboCountry, Combobox_Setup.ComboType.Mst_Country, "", "");
        }


        private void txtstatename_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtstatename, txtAliasName, "tbl_StateMaster", "StateName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtstatename, txtAliasName, "tbl_StateMaster", "StateName");
        }
    }
}
