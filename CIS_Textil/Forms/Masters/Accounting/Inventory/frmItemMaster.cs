using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;

namespace CIS_Textil
{
    public partial class frmItemMaster : frmMasterIface
    {
        public frmItemMaster()
        {
            InitializeComponent();
        }

        #region Event

        private void frmItemMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboItemCategory, Combobox_Setup.ComboType.Mst_ItemCategory, "");
                Combobox_Setup.FillCbo(ref cboItemGroup, Combobox_Setup.ComboType.Mst_ItemGroup, "");
                Combobox_Setup.FillCbo(ref cboUnits, Combobox_Setup.ComboType.Mst_Unit, "");

                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtItemName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtItemName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtItemName.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "ItemID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtItemName, "ItemName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasname, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboItemGroup, "ItemGroupID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboItemCategory, "ItemCategoryID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboUnits, "UnitID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtQty, "Quantity", Enum_Define.ValidationType.Text);
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
                sComboAddText = txtItemName.Text;
                ArrayList pArrayData = new ArrayList
                {
                txtItemName.Text.Trim(), 
                txtAliasname.Text.Trim(),
                cboItemGroup.SelectedValue,
                cboItemCategory.SelectedValue,
                cboUnits.SelectedValue,
                txtQty.Text.Trim(),
                (ChkActive.Checked == true ? 1 : 0),
                (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                (txtET1.Text.Trim()),
                (txtET2.Text.Trim()),
                (txtET3.Text.Trim())
                };
                sComboAddText = txtItemName.Text.Trim();
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
                if (txtItemName.Text.Trim() == "" || txtItemName.Text.Trim() == "-" || txtItemName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Item name");
                    txtItemName.Focus();
                    return true;
                }

                if (cboItemGroup.SelectedValue == null || cboItemGroup.SelectedValue.ToString() == "-" || cboItemGroup.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Item Group");
                    cboItemGroup.Focus();
                    return true;
                }
                if (cboItemCategory.SelectedValue == null || cboItemCategory.SelectedValue.ToString() == "-" || cboItemCategory.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Item Category");
                    cboItemCategory.Focus();
                    return true;
                }
                if (cboUnits.SelectedValue == null || cboUnits.SelectedValue.ToString() == "-" || cboUnits.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Units");
                    cboUnits.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    str = "tbl_ItemMaster";
                    if (Navigate.CheckDuplicate(ref str, "ItemName", txtItemName.Text.Trim(), false, "ItemID", 0, "", "This ItemName is already available"))
                    {
                        txtItemName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasname.Text.Trim(), false, "ItemID", 0, "", "This Aliasname is already available"))
                    {
                        txtAliasname.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtItemName.Text.Trim(), false, "ItemID", 0, "", "This ItemName is already Used in AliasName"))
                    {
                        txtItemName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "ItemName", txtAliasname.Text.Trim(), false, "ItemID", 0, "", "This AliasName is already Used in ItemName"))
                    {
                        txtAliasname.Focus();
                        return true;
                    }
                }
                else
                {
                    str = "tbl_ItemMaster";
                    if (Navigate.CheckDuplicate(ref str, "ItemName", txtItemName.Text.Trim(), true, "ItemID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This ItemName is already available"))
                    {
                        txtItemName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasname.Text.Trim(), true, "ItemID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasname.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtItemName.Text.Trim(), true, "ItemID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This ItemName is already Used in AliasName"))
                    {
                        txtItemName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "ItemName", txtAliasname.Text.Trim(), true, "ItemID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This AliasName is already Used in ItemName"))
                    {
                        txtAliasname.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtItemName, txtAliasname, "tbl_ItemMaster", "ItemName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtItemName, txtAliasname, "tbl_ItemMaster", "ItemName"))
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

        private void txtItemName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtItemName, txtAliasname, "tbl_ItemMaster", "ItemName");
        }

        private void txtAliasname_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtItemName, txtAliasname, "tbl_ItemMaster", "ItemName");
        }
    }
}
