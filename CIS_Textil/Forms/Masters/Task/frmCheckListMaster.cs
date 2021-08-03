using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;


namespace CIS_Textil
{
    public partial class frmCheckListMaster : frmMasterIface
    {
        public frmCheckListMaster()
        {
            InitializeComponent();
        }

        #region Form Events

        private void frmYarnShadeMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboCheklistType, Combobox_Setup.ComboType.Mst_ChekListType, "");

                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtCheckListName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtCheckListName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtCheckListName.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "CheckListID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCheckListName, "CheckListName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboCheklistType, "CheckListTypeID", Enum_Define.ValidationType.Text);
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
                sComboAddText = txtCheckListName.Text;
                ArrayList pArrayData = new ArrayList
                {
                    (cboCheklistType.SelectedValue.ToString()),
                    (txtCheckListName.Text.Trim()),
                    (txtAliasName.Text == ""? null : txtAliasName.Text),
                    (ChkActive.Checked ? 1 : 0),
                    (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                    (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                    (txtET1.Text.Trim()),
                    (txtET2.Text.Trim()),
                    (txtET3.Text.Trim())
                };
                DBSp.Master_AddEdit(pArrayData, "");
                this.IsMasterAdded = true;
            }
            catch (Exception exception1)
            {
                this.IsMasterAdded = false;
                Navigate.logError(exception1.Message, exception1.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                string strTableName;
                if (txtCheckListName.Text.Trim() == "" || txtCheckListName.Text.Trim() == "-" || txtCheckListName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Checklist Name");
                    txtCheckListName.Focus();
                    return true;
                }

                if (cboCheklistType.SelectedValue == null || cboCheklistType.SelectedValue.ToString() == "-" || cboCheklistType.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Checklist Type");
                    cboCheklistType.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    strTableName = "tbl_CheckListMaster";
                    if (Navigate.CheckDuplicate(ref strTableName, "CheckListName", txtCheckListName.Text.Trim(), false, "", 0L, "", "This CheckListName is already available"))
                    {
                        txtCheckListName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", txtCheckListName.Text.Trim(), false, "", 0L, "", "This CheckListName is already Used in AliasName"))
                    {
                        txtCheckListName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTableName, "CheckListName", txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in CheckListName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    strTableName = "tbl_CheckListMaster";
                    if (Navigate.CheckDuplicate(ref strTableName, "CheckListName", txtCheckListName.Text.Trim(), true, "CheckListID", (long)Math.Round(Localization.ParseDBDouble(txtCode.Text.Trim())), "", "This CheckListName is already available"))
                    {
                        txtCheckListName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", txtAliasName.Text.Trim(), true, "CheckListID", (long)Math.Round(Localization.ParseDBDouble(txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", txtCheckListName.Text.Trim(), true, "CheckListID", (long)Math.Round(Localization.ParseDBDouble(txtCode.Text.Trim())), "", "This CheckListName is already Used in AliasName"))
                    {
                        txtCheckListName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTableName, "CheckListName", txtAliasName.Text.Trim(), true, "CheckListID", (long)Math.Round(Localization.ParseDBDouble(txtCode.Text.Trim())), "", "This AliasName is already Used in CheckListName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtCheckListName, txtAliasName, "tbl_CheckListMaster", "CheckListName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtCheckListName, txtAliasName, "tbl_CheckListMaster", "CheckListName"))
                    return true;

                return false;
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
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

        private void txtCheckListName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtCheckListName, txtAliasName, "tbl_CheckListMaster", "CheckListName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtCheckListName, txtAliasName, "tbl_CheckListMaster", "CheckListName");
        }
    }
}
