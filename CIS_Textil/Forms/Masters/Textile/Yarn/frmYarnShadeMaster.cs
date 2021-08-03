using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using System.Data;


namespace CIS_Textil
{
    public partial class frmYarnShadeMaster : frmMasterIface
    {
        public frmYarnShadeMaster()
        {
            InitializeComponent();
        }

        #region Form Events
        private void frmYarnShadeMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboColor, Combobox_Setup.ComboType.Mst_YarnColor, "");

                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtShadeName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtShadeName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtShadeName.Focus();
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

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                txtShadeName.Focus();
                ApplyActStatus();
                BindComp();
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
                DBValue.Return_DBValue(this, txtCode, "YarnShadeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtShadeName, "YarnShadeName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboColor, "ColorId", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                #region Fill Multiple Company
                BindComp();
                string str = DB.GetSnglValue("select IntCompID from fn_YarnShadeMaster_Tbl() Where YarnShadeID=" + txtCode.Text + "");
                string[] strMember = str.Split(',');
                if (str != "")
                {
                    for (int i = 0; i <= chkCompany.Items.Count - 1; i++)
                    {
                        DataRowView dr = (DataRowView)chkCompany.Items[i];
                        for (int j = 0; j <= strMember.Length - 1; j++)
                        {
                            if (dr[0].ToString() == strMember[j].ToString())
                            {
                                chkCompany.SetItemChecked(i, true);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < chkCompany.Items.Count; i++)
                    {
                        chkCompany.SetItemChecked(i, false);
                    }
                }
                #endregion
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
                sComboAddText = txtShadeName.Text;

                ArrayList pArrayData = new ArrayList
                {
                    txtShadeName.Text.Trim(),
                    txtAliasName.Text==""?null:txtAliasName.Text,
                    cboColor.SelectedValue.ToString(),
                    (ChkActive.Checked ? 1 : 0),
                    (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                    (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                    (txtET1.Text.Trim()),
                    (txtET2.Text.Trim()),
                    (txtET3.Text.Trim())
                };

                #region Multiple Company
                string strCheckedValue = string.Empty;
                for (int i = 0; i <= chkCompany.Items.Count - 1; i++)
                {
                    if (chkCompany.GetItemChecked(i) == true)
                    {
                        DataRowView dr = (DataRowView)chkCompany.Items[i];
                        strCheckedValue = strCheckedValue + dr[0].ToString() + ",";
                    }
                }
                if (strCheckedValue.Length > 0)
                {
                    strCheckedValue = strCheckedValue.Substring(0, strCheckedValue.Length - 1);
                }
                Db_Detials.IntCompID = strCheckedValue == null ? (Db_Detials.CompID).ToString() : (strCheckedValue == "" ? (Db_Detials.CompID).ToString() : strCheckedValue);
                #endregion

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
                string strTableName = Db_Detials.fn_YarnShadeMaster_Tbl;

                if (txtShadeName.Text.Trim() == "" || txtShadeName.Text.Trim() == "-" || txtShadeName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Shade Name");
                    txtShadeName.Focus();
                    return true;
                }
                if (cboColor.SelectedValue == null || cboColor.SelectedValue.ToString() == "-" || cboColor.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Color");
                    cboColor.Focus();
                    return true;
                }
                if (!cboColor.IsValidSelect)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Valid Color");
                    cboColor.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    
                    if (Navigate.CheckDuplicate(ref strTableName, "YarnShadeName", txtShadeName.Text.Trim(), false, "", 0L, "", "This ShadeName is already available"))
                    {
                        txtShadeName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", txtShadeName.Text.Trim(), false, "", 0L, "", "This ShadeName is already Used in AliasName"))
                    {
                        txtShadeName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "YarnShadeName", txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in ShadeName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    if (Navigate.CheckDuplicate(ref strTableName, "YarnShadeName", txtShadeName.Text.Trim(), true, "YarnShadeID", (long)Math.Round(Localization.ParseDBDouble(txtCode.Text.Trim())), "", "This ShadeName is already available"))
                    {
                        txtShadeName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", txtAliasName.Text.Trim(), true, "YarnShadeID", (long)Math.Round(Localization.ParseDBDouble(txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", txtShadeName.Text.Trim(), true, "YarnShadeID", (long)Math.Round(Localization.ParseDBDouble(txtCode.Text.Trim())), "", "This ShadeName is already Used in AliasName"))
                    {
                        txtShadeName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "YarnShadeName", txtAliasName.Text.Trim(), true, "YarnShadeID", (long)Math.Round(Localization.ParseDBDouble(txtCode.Text.Trim())), "", "This AliasName is already Used in ShadeName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtShadeName, txtAliasName, "tbl_YarnShadeMaster", "YarnShadeName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtShadeName, txtAliasName, "tbl_YarnShadeMaster", "YarnShadeName"))
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

        private void txtShadeName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtShadeName, txtAliasName, "tbl_YarnShadeMaster", "YarnShadeName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtShadeName, txtAliasName, "tbl_YarnShadeMaster", "YarnShadeName");
        }

        private void BindComp()
        {
            try
            {
                DataTable dt = DB.GetDT(string.Format("Select CompanyID,CompanyName From fn_CompanyMaster_Tbl() order by CompanyName asc"), false);
                ((ListBox)chkCompany).DataSource = null; ((ListBox)chkCompany).DataSource = dt;
                ((ListBox)chkCompany).DisplayMember = "CompanyName";
                ((ListBox)chkCompany).ValueMember = "CompanyID";
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
    }
}
