using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmBeamTypeMaster : frmMasterIface
    {
        public frmBeamTypeMaster()
        {
            InitializeComponent();
        }

        #region Form Event

        private void frmBeamTypeMaster_Load(object sender, EventArgs e)
        {
            try
            {
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtBeamType.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtBeamType.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtBeamType.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "BeamTypeId", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtBeamType, "BeamType", Enum_Define.ValidationType.Text);
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
                sComboAddText = txtBeamType.Text;

                ArrayList pArrayData = new ArrayList
                {
                    txtBeamType.Text.Trim().ToString(),
                    txtAliasName.Text==""?null:txtAliasName.Text,
                    (ChkActive.Checked ? 1: 0),
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
                string strTable;
                if (txtBeamType.Text.Trim() == "" || txtBeamType.Text.Trim() == "-" || txtBeamType.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Beam Type");
                    txtBeamType.Focus();
                    return true;
                }
                if (DBSp.rtnAction())
                {
                    strTable = "tbl_BeamTypeMaster";
                    if (Navigate.CheckDuplicate(ref strTable, "BeamType", txtBeamType.Text.Trim(), false, "", 0L, "", "This BeamType is already available"))
                    {
                        txtBeamType.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "Aliasname", txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "Aliasname", txtBeamType.Text.Trim(), false, "", 0L, "", "This BeamType is already Used in AliasName"))
                    {
                        txtBeamType.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "BeamType", txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in BeamType"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    strTable = "tbl_BeamTypeMaster";
                    if (Navigate.CheckDuplicate(ref strTable, "BeamType", txtBeamType.Text.Trim(), true, "BeamTypeId", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This BeamType is already available"))
                    {
                        txtBeamType.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTable, "AliasName", txtAliasName.Text.Trim(), true, "BeamTypeId", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "AliasName", txtBeamType.Text.Trim(), true, "BeamTypeId", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This BeamType is already Used in AliasName"))
                    {
                        txtBeamType.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTable, "BeamType", txtAliasName.Text.Trim(), true, "BeamTypeId", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This AliasName is already Used in BeamType"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtBeamType, txtAliasName, "tbl_BeamTypeMaster", "BeamType"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtBeamType, txtAliasName, "tbl_BeamTypeMaster", "BeamType"))
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
            txtBeamType.Focus();
            ApplyActStatus();
        }
        #endregion

        private void txtBeamType_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtBeamType, txtAliasName, "tbl_BeamTypeMaster", "BeamType");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtBeamType, txtAliasName, "tbl_BeamTypeMaster", "BeamType");
        }
    }
}
