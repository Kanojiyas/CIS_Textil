using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_CLibrary;
using CIS_DBLayer;
using CIS_Utilities;

namespace CIS_Textil
{
    public partial class frmEMailConfig : frmMasterIface
    {
        public frmEMailConfig()
        {
            InitializeComponent();
        }

        private void frmEMailConfig_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboFormName, Combobox_Setup.ComboType.Mst_FormName, "");
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtMailID.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                    }
                    else
                    {
                        this.txtMailID.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtMailID.Focus();
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

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                CommonCls.IncFieldID(this, ref txtCode, "");
                txtMailID.Text = "";
                txtPassword.Text = "";
                txtSignature.Text = "";
                txtSendMailTo.Text = "";
                txtMessage.Text = "";
                
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #region "Form Navigation"

        public void FillControls()
        {
            try
                
            {
                DBValue.Return_DBValue(this, txtCode, "MailingConfigID");
                DBValue.Return_DBValue(this, txtMailID, "MailAdd");
                string sPAsswrd = DBValue.Return_DBValue(this, "Password");
                txtPassword.Text = CommonLogic.UnmungeString(sPAsswrd);

                int iMenuID = Localization.ParseNativeInt(DBValue.Return_DBValue(this, "MenuID"));
                cboFormName.SelectedValue = iMenuID;

                DBValue.Return_DBValue(this, txtHost, "Host");
                DBValue.Return_DBValue(this, txtPortNo, "PortNo");
                txtSignature.Text = DBValue.Return_DBValue(this, "Signature");
                DBValue.Return_DBValue(this, txtSendMailTo, "SendMailTo", Enum_Define.ValidationType.Text);
                txtMessage.Text = DBValue.Return_DBValue(this, "Message");
                DBValue.Return_DBValue(this, chkIsGmail, "IsGmail", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, chkUpdActive, "IsActive", Enum_Define.ValidationType.Text);
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_DialogIcon.Error, "", ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public void SaveRecord()
        {
            try
            {
                string strdt = "";
                if (cboFormName.SelectedValue.ToString() != "-" || cboFormName.SelectedValue != null)
                {
                    strdt = DB.GetSnglValue("SELECT MENUID FROM TBL_MENUMASTER Where MENU_CAPTION='" + cboFormName.Text + "'");
                }
                else
                {
                    strdt = "0";
                }

                ArrayList ArrayData = new ArrayList
                {
                    txtMailID.Text.Trim(),
                    CommonLogic.MungeString( txtPassword.Text.Trim()),
                    txtPortNo.Text.Trim(),
                    txtHost.Text.Trim(),
                    txtSignature.Text.Trim(),
                    (ChkActive.Checked ? 1 : 0),
                    strdt,
                    txtSendMailTo.Text.Trim(),
                    txtMessage.Text.Trim(),
                    (chkIsGmail.Checked?1:0),
                    (chkUpdActive.Checked ? 1 :0)
                };

                sComboAddText = txtMailID.Text.Trim();
                DBSp.Master_AddEdit(ArrayData);
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_DialogIcon.Error, "", ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                if (txtMailID.Text.Trim().Length <= 0)
                {
                    Navigate.ShowMessage(CIS_DialogIcon.Error, "", "Please Enter e-Mail Address");
                    txtMailID.Focus();
                    return true;
                }
                else if (txtPassword.Text.Trim().Length <= 0)
                {
                    Navigate.ShowMessage(CIS_DialogIcon.Error, "", "Please Enter Password");
                    txtPassword.Focus();
                    return true;
                }
                else if (txtHost.Text.Trim().Length <= 0)
                {
                    Navigate.ShowMessage(CIS_DialogIcon.Error, "", "Please Enter Mailing Host");
                    txtHost.Focus();
                    return true;
                }
                else if (txtPortNo.Text.Trim().Length <= 0)
                {
                    Navigate.ShowMessage(CIS_DialogIcon.Error, "", "Please Enter Port No");
                    txtPortNo.Focus();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                return false;
            }
        }

        #endregion

        private void frmMailingConfig_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (CIS_Utilities.CIS_Dialog.Show("Do you want to close this form?", GetAssemblyInfo.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void grpbxUpdateEmail_Enter(object sender, EventArgs e)
        {

        }
    }
}
