using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;

namespace CIS_Textil
{
    public partial class frmItemCategoryMaster : frmMasterIface
    {
        public frmItemCategoryMaster()
        {
            InitializeComponent();
        }

        #region Event

        private void frmItemCategoryMaster_Load(object sender, EventArgs e)
        {
            try
            {
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtItemCategory.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtItemCategory.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtItemCategory.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "ItemCategoryID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtItemCategory, "ItemCategoryName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasname, "AliasName", Enum_Define.ValidationType.Text);
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

        public void SaveRecord()
        {
            try
            {
                sComboAddText = txtItemCategory.Text;

                ArrayList pArrayData = new ArrayList
                {
                txtItemCategory.Text.Trim(),
                txtAliasname.Text.Trim(),
                (ChkActive.Checked == true ? 1 : 0),
                (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                (txtET1.Text.Trim()),
                (txtET2.Text.Trim()),
                (txtET3.Text.Trim())
                };

                sComboAddText = txtItemCategory.Text.Trim();
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
            string str;
            try
            {
                if (txtItemCategory.Text.Trim() == "" || txtItemCategory.Text.Trim() == "-" || txtItemCategory.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Item Category");
                    txtItemCategory.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    str = "tbl_ItemCategoryMaster";
                    if (Navigate.CheckDuplicate(ref str, "ItemCategoryName", this.txtItemCategory.Text.Trim(), false, "", 0L, "", "This ItemCategoryName is already available"))
                    {
                        this.txtItemCategory.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", this.txtAliasname.Text.Trim(), false, "", 0L, "", "This AliasName is already available"))
                    {
                        this.txtAliasname.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", this.txtItemCategory.Text.Trim(), false, "", 0L, "", "This ItemCategoryName is already Used in AliasName"))
                    {
                        this.txtItemCategory.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "ItemCategoryName", this.txtAliasname.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in ItemCategoryName"))
                    {
                        this.txtAliasname.Focus();
                        return true;
                    }
                }
                else
                {
                    str = "tbl_ItemCategoryMaster";
                    if (Navigate.CheckDuplicate(ref str, "ItemCategoryName", this.txtItemCategory.Text.Trim(), true, "ItemCategoryID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This ItemCategoryName is already available"))
                    {
                        this.txtItemCategory.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", this.txtAliasname.Text.Trim(), true, "ItemCategoryID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        this.txtAliasname.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", this.txtItemCategory.Text.Trim(), true, "ItemCategoryID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This ItemCategoryName is already Used in AliasName"))
                    {
                        this.txtItemCategory.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "ItemCategoryName", this.txtAliasname.Text.Trim(), true, "ItemCategoryID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This AliasName is already Used in ItemCategoryName"))
                    {
                        this.txtAliasname.Focus();
                        return true;
                    }
                }
                if(CommonCls.ValidateMaster(this, txtItemCategory, txtAliasname, "tbl_ItemCategoryMaster", "ItemCategoryName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtItemCategory, txtAliasname, "tbl_ItemCategoryMaster", "ItemCategoryName"))
                    return true;
                return false;
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                return false;
            }
        }

        #endregion

        private void txtItemCategory_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtItemCategory, txtAliasname, "tbl_ItemCategoryMaster", "ItemCategoryName");
        }

        private void txtAliasname_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtItemCategory, txtAliasname, "tbl_ItemCategoryMaster", "ItemCategoryName");
        }
    }
}
