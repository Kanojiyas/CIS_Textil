using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;

namespace CIS_Textil
{
    public partial class frmSecurityMaster : frmMasterIface
    {
        public frmSecurityMaster()
        {
            InitializeComponent();
        }

        private void frmSecurityMaster_Load(object sender, EventArgs e)
        {
            try
            {
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    txtSecurityLevel.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                }
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
                DBValue.Return_DBValue(this, txtCode, "SecurityID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtSecurityLevel, "SecurityLvl", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
                ApplyActStatus();
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", exception1.Message);
            }
        }

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                CommonCls.AutoCompleteText(this.strTableName, "SecurityLvl", ref txtSecurityLevel);
                CommonCls.AutoCompleteText(this.strTableName, "AliasName", ref txtAliasName);
                ApplyActStatus();
            }
            catch (Exception ex) { Navigate.logError(ex.Message,ex.StackTrace); }
        }

        public void SaveRecord()
        {
            try
            {
                ArrayList pArrayData = new ArrayList
                {
                    txtSecurityLevel.Text.Trim(),
                    txtAliasName.Text.Trim(),
                    (ChkActive.Checked ? 1 : 0),
                };
                DBSp.Master_AddEdit(pArrayData, "");
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", exception1.Message);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                string strTableName;
                if (Strings.Len(Strings.Trim(this.txtSecurityLevel.Text.ToString())) <= 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Security Level");
                    this.txtSecurityLevel.Focus();
                    return true;
                }
                if (DBSp.rtnAction())
                {
                    strTableName = "tbl_Securitymaster";
                    if (Navigate.CheckDuplicate(ref strTableName, "SecurityLvl", this.txtSecurityLevel.Text.Trim(), false, "", 0L, "", ""))
                    {
                        this.txtSecurityLevel.Focus();
                        return false;
                    }
                }
                else
                {
                    strTableName = "tbl_Securitymaster";
                    if (Navigate.CheckDuplicate(ref strTableName, "SecurityLvl", this.txtSecurityLevel.Text.Trim(), true, "SecurityID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", ""))
                    {
                        this.txtSecurityLevel.Focus();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                return false;
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
    }
}
