using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using System.Data;

namespace CIS_Textil
{
    public partial class frmItemGroupMaster : frmMasterIface
    {
        public frmItemGroupMaster()
        {
            InitializeComponent();
        }

        #region Event

        private void frmItemGroupMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboGroupType, Combobox_Setup.ComboType.Mst_ItemGroup, "");
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtItemGroup.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtItemGroup.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtItemGroup.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "ItemGroupID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtItemGroup, "ItemGroupName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboGroupType, "ItemGroupType", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasname, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                #region Fill Multiple Company
                BindComp();
                string str = DB.GetSnglValue("select IntCompID from fn_ItemGroupMaster_Tbl() Where ItemGroupID=" + txtCode.Text + "");
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

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                CommonCls.IncFieldID(this, ref txtCode, "");
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
                sComboAddText = txtItemGroup.Text;

                ArrayList pArrayData = new ArrayList
                {
                    txtItemGroup.Text.Trim(),
                    txtAliasname.Text.Trim(),
                    cboGroupType.SelectedValue,
                    (ChkActive.Checked == true ? 1 : 0),
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

                sComboAddText = txtItemGroup.Text.Trim();
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
                if (txtItemGroup.Text.Trim() == "" || txtItemGroup.Text.Trim() == "-" || txtItemGroup.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Item Group");
                    txtItemGroup.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    str = "tbl_ItemGroupMaster";
                    if (Navigate.CheckDuplicate(ref str, "ItemGroupName", txtItemGroup.Text.Trim(), false, "ItemGroupID", 0, "ItemGroupID=" + txtCode.Text + "", "This ItemGroupName is already available"))
                    {
                        txtItemGroup.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasname.Text.Trim(), false, "ItemGroupID", 0, "ItemGroupID=" + txtCode.Text + "", "This Aliasname is already available"))
                    {
                        txtAliasname.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtItemGroup.Text.Trim(), false, "ItemGroupID", 0, "ItemGroupID=" + txtCode.Text + "", "This ItemGroupName is already Used in AliasName"))
                    {
                        txtItemGroup.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "ItemGroupName", txtAliasname.Text.Trim(), false, "ItemGroupID", 0, "ItemGroupID=" + txtCode.Text + "", "This AliasName is already Used in ItemGroupName"))
                    {
                        txtAliasname.Focus();
                        return true;
                    }
                }
                else
                {
                    str = "tbl_ItemGroupMaster";
                    if (Navigate.CheckDuplicate(ref str, "ItemGroupName", txtItemGroup.Text.Trim(), true, "ItemGroupID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This ItemGroupName is already available"))
                    {
                        txtItemGroup.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasname.Text.Trim(), true, "ItemGroupID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasname.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtItemGroup.Text.Trim(), true, "ItemGroupID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This ItemGroupName is already Used in AliasName"))
                    {
                        txtItemGroup.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "ItemGroupName", txtAliasname.Text.Trim(), true, "ItemGroupID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This AliasName is already Used in ItemGroupName"))
                    {
                        txtAliasname.Focus();
                        return true;
                    }
                }

                if (CommonCls.ValidateMaster(this, txtItemGroup, txtAliasname, "tbl_ItemGroupMaster", "ItemGroupName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtItemGroup, txtAliasname, "tbl_ItemGroupMaster", "ItemGroupName"))
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

        private void txtItemGroup_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtItemGroup, txtAliasname, "tbl_ItemGroupMaster", "ItemGroupName");
        }

        private void txtAliasname_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtItemGroup, txtAliasname, "tbl_ItemGroupMaster", "ItemGroupName");
        }

        private void BindComp()
        {
            try
            {
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
