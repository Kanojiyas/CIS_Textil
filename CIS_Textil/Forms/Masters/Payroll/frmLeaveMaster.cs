using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmLeaveMaster : frmMasterIface
    {
        public frmLeaveMaster()
        {
            InitializeComponent();
        }

        private void frmLeaveMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboLeaveType, Combobox_Setup.ComboType.Mst_LeaveType, "");
                Combobox_Setup.FillCbo(ref cboNoOfLeaves, Combobox_Setup.ComboType.Mst_Leave, "");

                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtLeaveName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtLeaveName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtLeaveName.Focus();
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

        #region From Event

        public void MovetoField()
        {
            try
            {
                txtLeaveName.Focus();
                ChkActive.Enabled = true;
                rdbCForward.Checked = false;
                rdbNCForward.Checked = false;
                ApplyActStatus();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #endregion FromEvent

        #region FormNavigation

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
                DBValue.Return_DBValue(this, txtCode, "LeaveID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLeaveName, "LeaveName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboLeaveType, "LeaveType", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboNoOfLeaves, "LeavesNos", Enum_Define.ValidationType.Text);
                string IsCrryFwd = DBValue.Return_DBValue(this, "IsCrryFwd");
                if (IsCrryFwd == "False")
                    rdbCForward.Checked = true;
                else
                    rdbNCForward.Checked = true;

                string IsEncashable = DBValue.Return_DBValue(this, "IsEncashable");
                if (IsEncashable == "False")
                    rdbYes.Checked = true;
                else
                    rdbNo.Checked = true;

                string IsActive = DBValue.Return_DBValue(this, "IsActive");

                if (IsActive == "True")
                {
                    ChkActive.Enabled = false;
                    ChkActive.Checked = true;
                }
                else if (IsActive == "False")
                {
                    ChkActive.Enabled = false;
                    ChkActive.Checked = false;
                }
                else
                {
                    ChkActive.Enabled = true;
                    ChkActive.Checked = false;
                    rdbCForward.Checked = false;
                    rdbNCForward.Checked = false;
                }
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
                sComboAddText = txtLeaveName.Text;

                int IsCarryForwrd = 0;
                int IsEncashable = 0;

                if (rdbCForward.Checked == true)
                    IsCarryForwrd = 0;
                else if (rdbNCForward.Checked == true)
                    IsCarryForwrd = 1;

                if (rdbYes.Checked == true)
                    IsEncashable = 0;
                else if (rdbNo.Checked == true)
                    IsEncashable = 1;

                ArrayList pArrayData = new ArrayList
                {
                txtLeaveName.Text.Trim(),
                txtAliasName.Text==""?null:txtAliasName.Text,
                cboLeaveType.SelectedValue,
                cboNoOfLeaves.SelectedValue,
                IsCarryForwrd,
                IsEncashable,
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
                if (txtLeaveName.Text.Trim() == "" || txtLeaveName.Text.Trim() == "-" || txtLeaveName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Leave Name");
                    txtLeaveName.Focus();
                    return true;
                }

                if (rdbYes.Checked == false && rdbNo.Checked == false)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Leave Encashable ");
                    rdbYes.Focus();
                    return true;
                }

                if (rdbCForward.Checked == false && rdbNCForward.Checked == false)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Leave Carry Forward ");
                    rdbCForward.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    strTblName = "tbl_LeaveMaster";
                    if (Navigate.CheckDuplicate(ref strTblName, "LeaveName", txtLeaveName.Text.Trim(), false, "", 0, "", "This LeaveName is already available"))
                    {
                        txtLeaveName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "Aliasname", txtAliasName.Text.Trim(), false, "", 0, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "Aliasname", txtLeaveName.Text.Trim(), false, "", 0, "", "This LeaveName is already Used in AliasName"))
                    {
                        txtLeaveName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "LeaveName", txtAliasName.Text.Trim(), false, "", 0, "", "This AliasName is already Used in LeaveName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    strTblName = "tbl_LeaveMaster";
                    if (Navigate.CheckDuplicate(ref strTblName, "LeaveName", txtLeaveName.Text.Trim(), true, "LeaveID", Localization.ParseNativeLong(txtCode.Text.Trim()), "", "This LeaveName is already available"))
                    {
                        txtLeaveName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtAliasName.Text.Trim(), true, "LeaveID", Localization.ParseNativeLong(txtCode.Text.Trim()), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtLeaveName.Text.Trim(), true, "LeaveID", Localization.ParseNativeLong(txtCode.Text.Trim()), "", "This LeaveName is already Used in AliasName"))
                    {
                        txtLeaveName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "LeaveName", txtAliasName.Text.Trim(), true, "LeaveID", Localization.ParseNativeLong(txtCode.Text.Trim()), "", "This AliasName is already Used in LeaveName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtLeaveName, txtAliasName, "tbl_LeaveMaster", "LeaveName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtLeaveName, txtAliasName, "tbl_LeaveMaster", "LeaveName"))
                    return true;
                
                return false;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                return false;
            }
        }

        #endregion FormNavigation

        private void txtLeaveName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtLeaveName, txtAliasName, "tbl_LeaveMaster", "LeaveName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtLeaveName, txtAliasName, "tbl_LeaveMaster", "LeaveName");
        }
    }
}
