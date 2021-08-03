using System;
using System.Collections;
using System.Windows.Forms;
using CIS_DBLayer;
using CIS_Bussiness;
using Microsoft.VisualBasic;

namespace CIS_Textil
{
    public partial class frmDescriptionMaster : frmMasterIface
    {
        public frmDescriptionMaster()
        {
            base.Load += new EventHandler(this.frmDescriptionMaster_Load);
            InitializeComponent();
        }

        #region Form Events
        private void frmDescriptionMaster_Load(object sender, EventArgs e)
        {
            try
            {
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtDescription.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtDescription.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtDescription.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "DescriptionID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);
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
                sComboAddText = txtDescription.Text;
                ArrayList pArrayData = new ArrayList
                {
                    txtDescription.Text.Trim(),
                    txtAliasName.Text==""?null:txtAliasName.Text,
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
                string str;
                if (txtDescription.Text.Trim() == "" || txtDescription.Text.Trim() == "-" || txtDescription.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Description");
                    txtDescription.Focus();
                    return true;
                }
                if (DBSp.rtnAction())
                {
                    str = "tbl_DescriptionMaster";
                    if (Navigate.CheckDuplicate(ref str, "Description", txtDescription.Text.Trim(), false, "", 0, "", "This Description is already available"))
                    {
                        txtDescription.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), false, "", 0, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtDescription.Text.Trim(), false, "", 0, "", "This Description is already Used in AliasName"))
                    {
                        txtDescription.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "Description", txtAliasName.Text.Trim(), false, "", 0, "", "This AliasName is already Used in Description"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    str = "tbl_DescriptionMaster";
                    if (Navigate.CheckDuplicate(ref str, "Description", txtDescription.Text.Trim(), true, "DescriptionID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This Description is already available"))
                    {
                        txtDescription.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), true, "DescriptionID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtDescription.Text.Trim(), true, "DescriptionID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This Description is already Used in AliasName"))
                    {
                        txtDescription.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "Description", txtAliasName.Text.Trim(), true, "DescriptionID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This AliasName is already Used in Description"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if(CommonCls.ValidateMaster(this, txtDescription, txtAliasName, "tbl_DescriptionMaster", "Description"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtDescription, txtAliasName, "tbl_DescriptionMaster", "Description"))
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

        private void txtDescription_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtDescription, txtAliasName, "tbl_DescriptionMaster", "Description");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtDescription, txtAliasName, "tbl_DescriptionMaster", "Description");
        }
    }
}
