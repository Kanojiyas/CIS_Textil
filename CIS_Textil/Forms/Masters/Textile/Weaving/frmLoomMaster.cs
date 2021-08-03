using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class frmLoomMaster : frmMasterIface
    {
        public frmLoomMaster()
        {
            InitializeComponent();
        }

        #region Form Event

        private void frmLoomMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboWeaver, Combobox_Setup.ComboType.Mst_WeaverNDepartment, "");
                Combobox_Setup.FillCbo(ref cboLoomType, Combobox_Setup.ComboType.Mst_LoomType, "");

                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtLoomName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtLoomName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtLoomName.Focus();
                        this.isComboAddText = true;
                        this.isSecondMessage = true;
                    }
                }

                if (blnFormAction == Enum_Define.ActionType.View_Record)
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

        # region From Navigation

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
                CommonCls.IncFieldID(this, ref txtCode, "");
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
                DBValue.Return_DBValue(this, txtCode, "LoomID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLoomName, "LoomName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboLoomType, "LoomTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboWeaver, "WeaverID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtRPM, "RPM", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPerHour, "PerHour", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPerDay, "PerDay", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPerWeek, "PerWeek", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPerMonth, "PerMonth", Enum_Define.ValidationType.Text);
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
                sComboAddText = txtLoomName.Text;
                ArrayList pArrayData = new ArrayList
                {
                   txtLoomName.Text.ToString(),
                   txtAliasName.Text==""?null:txtAliasName.Text,
                   cboWeaver.SelectedValue,
                   cboLoomType.SelectedValue,
                   txtRPM.Text.ToString().Replace(",", ""),
                   txtPerHour.Text.ToString().Replace(",", ""),
                   txtPerDay.Text.ToString().Replace(",", ""),
                   txtPerWeek.Text.ToString().Replace(",", ""),
                   txtPerMonth.Text.ToString().Replace(",", ""),
                   (ChkActive.Checked == true ? 1 : 0),
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
                string str = string.Empty;
                if (txtLoomName.Text.Trim() == "" || txtLoomName.Text.Trim() == "-" || txtLoomName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Loom Name");
                    txtLoomName.Focus();
                    return true;
                }
                if (cboWeaver.SelectedValue == null || cboWeaver.SelectedValue.ToString() == "-" || cboWeaver.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Weaver");
                    cboWeaver.Focus();
                    return true;
                }
                if (!cboWeaver.IsValidSelect)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Valid Weaver");
                    cboWeaver.Focus();
                    return true;
                }
                if (cboLoomType.SelectedValue == null || cboLoomType.SelectedValue.ToString() == "-" || cboLoomType.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Loom Type");
                    cboLoomType.Focus();
                    return true;
                }
                if ((base.blnFormAction == Enum_Define.ActionType.Edit_Record) && (Conversions.ToDouble(DB.GetSnglValue(string.Format("Select Count(0) from {0} Where LoomID <> {1} and LoomName = {2} and WeaverID = {3}", new object[] { "tbl_LoomMaster", txtCode.Text, DB.SQuote(txtLoomName.Text), RuntimeHelpers.GetObjectValue(cboWeaver.SelectedValue) }))) > 0.0))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "This Loom is already exists in this Weaver");
                    txtLoomName.Focus();
                }
                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) && (Conversions.ToDouble(DB.GetSnglValue(string.Format("Select Count(0) from {0} Where LoomName = {1} and WeaverID = {2}", "tbl_LoomMaster", DB.SQuote(txtLoomName.Text), RuntimeHelpers.GetObjectValue(cboWeaver.SelectedValue)))) > 0.0))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "This Loom is already exists in this Weaver");
                    txtLoomName.Focus();
                }

                if (DBSp.rtnAction())
                {
                    str = "tbl_LoomMaster";
                    if (Navigate.CheckDuplicate(ref str, "LoomName", this.txtLoomName.Text.Trim(), false, "", 0L, "", "This LoomName is already available"))
                    {
                        this.txtLoomName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", this.txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", this.txtLoomName.Text.Trim(), false, "", 0L, "", "This LoomName is already Used in AliasName"))
                    {
                        this.txtLoomName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "LoomName", this.txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in LoomName"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    str = "tbl_LoomMaster";
                    if (Navigate.CheckDuplicate(ref str, "LoomName", this.txtLoomName.Text.Trim(), true, "LoomID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This LoomName is already available"))
                    {
                        this.txtLoomName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", this.txtAliasName.Text.Trim(), true, "LoomID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", this.txtLoomName.Text.Trim(), true, "LoomID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This LoomName is already Used in AliasName"))
                    {
                        this.txtLoomName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "LoomName", this.txtAliasName.Text.Trim(), true, "LoomID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This AliasName is already Used in LoomName"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtLoomName, txtAliasName, "tbl_LoomMaster", "LoomName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtLoomName, txtAliasName, "tbl_LoomMaster", "LoomName"))
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                return false;
            }
        }

        #endregion

        private void cboWeaver_LostFocus(object sender, EventArgs e)
        {
            if ((txtLoomName.Text != null) && (cboWeaver.SelectedValue != null))
            {
                if ((base.blnFormAction == Enum_Define.ActionType.Edit_Record) && (Conversions.ToDouble(DB.GetSnglValue(string.Format("Select Count(0) from {0} Where LoomID <> {1} and LoomName = {2} and WeaverID = {3}", new object[] { "tbl_LoomMaster", txtCode.Text, DB.SQuote(txtLoomName.Text), RuntimeHelpers.GetObjectValue(cboWeaver.SelectedValue) }))) > 0.0))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "This Loom is already exists in this Weaver");
                    txtLoomName.Text = "";
                    txtLoomName.Focus();
                }
                else if ((base.blnFormAction == Enum_Define.ActionType.New_Record) && (Conversions.ToDouble(DB.GetSnglValue(string.Format("Select Count(0) from {0} Where LoomName = {1} and WeaverID = {2}", "tbl_LoomMaster", DB.SQuote(txtLoomName.Text), RuntimeHelpers.GetObjectValue(cboWeaver.SelectedValue)))) > 0.0))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "This Loom is already exists in this Weaver");
                    txtLoomName.Text = "";
                    txtLoomName.Focus();
                }
            }
        }

        private void txtLoomName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtLoomName, txtAliasName, "tbl_LoomMaster", "LoomName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtLoomName, txtAliasName, "tbl_LoomMaster", "LoomName");
        }
    }
}
