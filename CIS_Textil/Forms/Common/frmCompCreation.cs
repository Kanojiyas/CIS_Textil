using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using CIS_DBLayer;
using CIS_Bussiness;
using System.Windows.Forms;

namespace CIS_Textil
{
    public partial class frmCompCreation : Form
    {
        public frmCompCreation()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                ValidateDtls();
                int iUserID = 0;
                int iCompID = 0;
                string strQry = string.Empty;
                strQry = string.Format("Insert Into tbl_CompanyMaster (CompanyName,YearId,IsModified,IsDeleted,IsCanclled,IsApproved,IsAudited) Values ({0},0,0,0,0,0,0)", CommonLogic.SQuote(txtCompany.Text));
                if(strQry.Length>0)
                iCompID = Localization.ParseNativeInt(Convert.ToString(DB.ExecuteSQL_Trns(strQry)));
                //strQry = string.Empty;
                //strQry = string.Format("Insert Into tbl_UserMaster (UserName,Password,EI1,EI2,Usertype,IsActive,BranchID,IsModified,IsDeleted,IsCanclled,IsApproved,IsAudited) Values ({0},{1},0,0,{2},{3},{4},0,0,0,0,0)", CommonLogic.SQuote(txtUserName.Text), CommonLogic.SQuote(CommonLogic.MungeString(txtPassword.Text)), 1, 1, 1);
                //if (strQry.Length > 0)
                //iUserID = DB.ExecuteSQL_Trns(strQry);
                //strQry = string.Empty;
                //strQry = string.Format("Insert Into tbl_UserMasterDtls (UserID,StoreID,BranchID,CompID) Values ({0},{1},{2},{3})", iUserID, 1,1, iCompID);

                if (iCompID > 0)
                {
                    DB.ExecuteSQL("sp_InsertAdministratorRights " + iCompID + "," + CommonLogic.SQuote(txtUserName.Text) + "," + CommonLogic.SQuote(CommonLogic.MungeString(txtPassword.Text)) + "");
                }
                
                AppStart.IsBool = 1;
                this.Dispose();
                this.Close();
            }
            catch (Exception ex) {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityError, "Security Warning", "Error While Saving Record...");
                Navigate.logError(ex.Message, ex.StackTrace); }
        }


        public bool ValidateDtls()
        {
            try 
            {
                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityError, "Security Warning", "Entered Password Does Not Match");
                    return true;
                }
                if (txtCompany.Text == null && txtCompany.Text.Trim() == "" && txtCompany.Text.Trim() == "-")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityError, "Security Warning", "Entered Password Does Not Match");
                    return true;
                }
                int icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(0) From tbl_CompanyMaster Where IsDeleted=0 and CompanyName=" + CommonLogic.SQuote(txtCompany.Text) + "")));
                if (icount > 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityError, "Security Warning", "Company Already Present");
                    return true;
                }
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); return true; }
            return false;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Form cForm = null;
                Navigate.NavigateForm(Enum_Define.Navi_form.Close_form, ref cForm, true, false);
                Environment.Exit(6);
                Environment.ExitCode = 6;
                Application.ExitThread();
                this.Close();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        private void frmCompCreation_Load(object sender, EventArgs e)
        {

        }
    }
}
