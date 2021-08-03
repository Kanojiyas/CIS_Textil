using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;
namespace CIS_Textil
{
    public partial class frmFormulaMaster : frmMasterIface
    {
        public frmFormulaMaster()
        {
            InitializeComponent();
        }

        #region Form Event

        private void frmFormulaMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref CboType, Combobox_Setup.ComboType.Mst_FormulaType, "");
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtFrml.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtFrml.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtFrml.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "FormulaID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtFrml, "Formula", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, CboType, "FormulaType", Enum_Define.ValidationType.Text);
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
                sComboAddText = txtFrml.Text;

                ArrayList pArrayData = new ArrayList
                {
                   txtFrml.Text.Trim(),
                   txtAliasName.Text==""?null:txtAliasName.Text,
                   CboType.SelectedValue,
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
                string strTableName = Db_Detials.fn_FormulaMaster_Tbl;

                if (txtFrml.Text.Trim() == "" || txtFrml.Text.Trim() == "-" || txtFrml.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Formula");
                    txtFrml.Focus();
                    return true;
                }

                if (CboType.SelectedValue == null || CboType.SelectedValue.ToString() == "-" || CboType.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Formula Type");
                    CboType.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    
                    if (Navigate.CheckDuplicate(ref strTableName, "Formula", this.txtFrml.Text.Trim(), false, "FormulaID", 0L, "", "This Formula is already available"))
                    {
                        txtFrml.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", this.txtAliasName.Text.Trim(), false, "FormulaID", 0L, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", this.txtFrml.Text.Trim(), false, "FormulaID", 0L, "", "This Formula is already Used in AliasName"))
                    {
                        txtFrml.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTableName, "Formula", this.txtAliasName.Text.Trim(), false, "FormulaID", 0L, "", "This AliasName is already Used in Formula"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    if (Navigate.CheckDuplicate(ref strTableName, "Formula", this.txtFrml.Text.Trim(), true, "FormulaID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This Formula is already available"))
                    {
                        txtFrml.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", this.txtAliasName.Text.Trim(), true, "FormulaID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", this.txtFrml.Text.Trim(), true, "FormulaID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This Formula is already Used in AliasName"))
                    {
                        txtFrml.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "Formula", this.txtAliasName.Text.Trim(), true, "FormulaID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This AliasName is already Used in Formula"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if(CommonCls.ValidateMaster(this, txtFrml, txtAliasName, "tbl_FormulaMaster", "Formula"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtFrml, txtAliasName, "tbl_FormulaMaster", "Formula"))
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
                txtFrml.Focus();
                ApplyActStatus();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #endregion

        private void txtFrml_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtFrml, txtAliasName, "tbl_FormulaMaster", "Formula");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtFrml, txtAliasName, "tbl_FormulaMaster", "Formula");
        }
    }
}
