using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;
using System.Data;


namespace CIS_Textil
{
    public partial class frmYarnColorMaster : frmMasterIface
    {
        public frmYarnColorMaster()
        {
            InitializeComponent();
        }

        #region Form Events
        private void frmYarnColorMaster_Load(object sender, EventArgs e)
        {
            try
            {
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtColorName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtColorName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtColorName.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "YarnColorID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtColorName, "YarnColorName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                #region Fill Multiple Company
                BindComp();
                string str = DB.GetSnglValue("select IntCompID from fn_YarnColorMaster_Tbl() Where YarnColorID=" + txtCode.Text + "");
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
                sComboAddText = txtColorName.Text;
                ArrayList pArrayData = new ArrayList
                {
                    txtColorName.Text.Trim(),
                    txtAliasName.Text==""?null:txtAliasName.Text,
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
            catch (Exception ex) 
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                this.IsMasterAdded = false;
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        public bool ValidateForm()
        {
            
            try
            {
                string strTableName = Db_Detials.fn_YarnColorMaster_Tbl;

                if (txtColorName.Text.Trim() == "" || txtColorName.Text.Trim() == "-" || txtColorName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Color Name");
                    this.txtColorName.Focus();
                    return true;
                }
                if (DBSp.rtnAction())
                {
                    

                    if (Navigate.CheckDuplicate(ref strTableName, "YarnColorName", this.txtColorName.Text.Trim(), false, "", 0L, "", "This ColorName is already available"))
                    {
                        this.txtColorName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", this.txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", this.txtColorName.Text.Trim(), false, "", 0L, "", "This ColorName is already Used in AliasName"))
                    {
                        this.txtColorName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "YarnColorName", this.txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in ColorName"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    
                    if (Navigate.CheckDuplicate(ref strTableName, "YarnColorName", this.txtColorName.Text.Trim(), true, "YarnColorID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This ColorName is already available"))
                    {
                        this.txtColorName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", this.txtAliasName.Text.Trim(), true, "YarnColorID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", this.txtColorName.Text.Trim(), true, "YarnColorID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This ColorName is already Used in AliasName"))
                    {
                        this.txtColorName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "YarnColorName", this.txtAliasName.Text.Trim(), true, "YarnColorID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This AliasName is already Used in ColorName"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }
                }

                if(CommonCls.ValidateMaster(this, txtColorName, txtAliasName, "tbl_YarnColorMaster", "YarnColorName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtColorName, txtAliasName, "tbl_YarnColorMaster", "YarnColorName"))
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
                txtColorName.Focus();
                ApplyActStatus();
                BindComp();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #endregion

        private void txtColorName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtColorName, txtAliasName, "tbl_YarnColorMaster", "YarnColorName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtColorName, txtAliasName, "tbl_YarnColorMaster", "YarnColorName");
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
