using System;
using System.Collections;
using System.Windows.Forms;
using  CIS_Bussiness;using CIS_DBLayer;
using Microsoft.VisualBasic;
using System.Data;

namespace CIS_Textil
{
    public partial class frmLedgerGroupMaster : frmMasterIface
    {
        public frmLedgerGroupMaster()
        {
            InitializeComponent();
        }

        private void frmLedgerGroupMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboLedgerType, Combobox_Setup.ComboType.Mst_LedgerGroup, "");
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtLedgerGroup.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtLedgerGroup.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtLedgerGroup.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "LedgerGroupId", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLedgerGroup, "LedgerGroupName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboLedgerType, "LedgerGroupTypeId", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);
                BindComp();
                string str = DB.GetSnglValue("select IntCompID from fn_LedgerGroupMaster_Tbl() Where LedgerGroupId=" + txtCode.Text + "");
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
                txtLedgerGroup.Focus();
                ApplyActStatus();
                BindComp();
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
                sComboAddText = txtLedgerGroup.Text;

                ArrayList pArrayData = new ArrayList
                { 
                    txtLedgerGroup.Text.Trim(),
                    txtAliasName.Text == "" ? null : txtAliasName.Text, 
                    cboLedgerType.SelectedValue.ToString(), 
                    "1",
                    "2" ,
                    "L", 
                    "500",
                    "U",
                    ChkActive.Checked == true ? 1 : 0,
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
                if (txtLedgerGroup.Text.Trim() == "" || txtLedgerGroup.Text.Trim() == "-" || txtLedgerGroup.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Ledger Group");
                    txtLedgerGroup.Focus();
                    return true;
                }

                if (cboLedgerType.SelectedValue == null || cboLedgerType.SelectedValue.ToString() == "-" || cboLedgerType.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Group Type");
                    cboLedgerType.Focus();
                    return true;
                }

                if (!cboLedgerType.IsValidSelect)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Valid Group Type");
                    cboLedgerType.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    str = "fn_LedgerGroupMaster_Tbl()";
                    if (Navigate.CheckDuplicate(ref str, "LedgerGroupName", this.txtLedgerGroup.Text.Trim(), false, "", 0L, "", "This LedgerGroup is already available"))
                    {
                        txtLedgerGroup.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", this.txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", this.txtLedgerGroup.Text.Trim(), false, "", 0L, "", "This LedgerGroup is already Used in AliasName"))
                    {
                        txtLedgerGroup.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "LedgerGroupName", this.txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in LedgerGroup"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    str = "fn_LedgerGroupMaster_Tbl()";
                    if (Navigate.CheckDuplicate(ref str, "LedgerGroupName", this.txtLedgerGroup.Text.Trim(), true, "LedgerGroupID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This LedgerGroup is already available"))
                    {
                        txtLedgerGroup.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", this.txtAliasName.Text.Trim(), true, "LedgerGroupID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", this.txtLedgerGroup.Text.Trim(), true, "LedgerGroupID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This LedgerGroup is already Used in AliasName"))
                    {
                        txtLedgerGroup.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "LedgerGroupName", this.txtAliasName.Text.Trim(), true, "LedgerGroupID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This AliasName is already Used in LedgerGroup"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                txtLedgerGroup_Leave(null, null);
                txtAliasName_Leave(null, null);
                return false;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                return false;
            }
        }

        private void txtLedgerGroup_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtLedgerGroup, txtAliasName, "tbl_LedgerGroupMaster", "LedgerGroupName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtLedgerGroup, txtAliasName, "tbl_LedgerGroupMaster", "LedgerGroupName");
        }

        private void BindComp()
        {
            try
            {
                ((ListBox)chkCompany).DataSource = null;
                DataTable dt = DB.GetDT(string.Format("Select CompanyID,CompanyName From fn_CompanyMaster_Tbl() order by CompanyName asc"), false);
                ((ListBox)chkCompany).DataSource = null;((ListBox)chkCompany).DataSource = dt;
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
