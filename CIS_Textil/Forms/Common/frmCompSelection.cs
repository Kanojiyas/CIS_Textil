using System;
using System.Data;
using System.Windows.Forms;
using CIS_Bussiness;using CIS_DBLayer;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmCompSelection : Form
    {
        double i = 0.1;
        public frmCompSelection()
        {
            InitializeComponent();
        }

        private void frmCompSection_Load(object sender, EventArgs e)
        {
            this.Opacity = i;
            //set the opacity of the form to 0.1 when form load
            timerFadeIn.Enabled = true;
            //start the Fade In Effect
            timerFadeOut.Enabled = false;

            try
            {
                string SqlQuery = "";

                if ((Db_Detials.UserType == 1) || (Db_Detials.UserType == 2))
                    SqlQuery = "Select CompanyId,CompanyName From tbl_CompanyMaster Where CompanyID in (Select CompID from tbl_UserMasterDtls)";
                else
                    SqlQuery = "Select CompanyId,CompanyName From tbl_CompanyMaster Where CompanyID in (Select CompID from tbl_UserMasterDtls where UserID=" + Db_Detials.UserID + ")";

                DataTable Dt = DB.GetDT(SqlQuery, false);
                DataRow row = Dt.NewRow();
                row["CompanyName"] = "--ALL--";
                row["CompanyID"] = 0;
                Dt.Rows.InsertAt(row, 0);
                CboCompany.DataSource = Dt;
                CboCompany.DisplayMember = "CompanyName";
                CboCompany.ValueMember = "CompanyID";
                CboCompany.SelectedValue = "0";
                CboCompany.ColumnWidths = "0;180;0;0;0;0";
                CboCompany.AutoComplete = true;
                CboCompany.AutoDropdown = true;

                ShowCompany_Year();

                if (Db_Detials.CompID > 0)
                {
                    CboCompany.SelectedValue = Db_Detials.CompID;
                }

                if (Db_Detials.YearID > 0)
                {
                    CboYear.SelectedValue = Db_Detials.YearID;
                }

                lblCompanyName.Text = Db_Detials.CompanyName;

            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void ShowCompany_Year()
        {
            try
            {
                if (this.CboCompany.SelectedValue.ToString().Length > 0)
                {
                    Combobox_Setup.Fill_Combo(this.CboYear, "Select YearID, YrDesc From tbl_YearMaster", "YrDesc", "YearID");
                    CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboYear = this.CboYear;
                    cboYear.SelectedIndex = cboYear.Items.Count - 1;
                    cboYear.ColumnWidths = "0;180;";
                    cboYear.AutoComplete = true;
                    cboYear.AutoDropdown = true;
                    cboYear = null;
                }
            }
            catch (Exception exception1)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", exception1.Message);
                Navigate.logError(exception1.Message, exception1.StackTrace);
            }
        }

        private void cboYear_GotFocus(object sender, EventArgs e)
        {
            this.ShowCompany_Year();
        }

        private void timerFadeIn_Tick(object sender, EventArgs e)
        {
            this.i += 0.05;
            if (this.i >= 1.0)
            {
                this.Opacity = 1.0;
                this.timerFadeIn.Enabled = false;
            }
            else
            {
                this.Opacity = this.i;
            }
        }

        private void timerFadeOut_Tick(object sender, EventArgs e)
        {
            this.i -= 0.05;
            this.timerFadeIn.Enabled = false;
            if (this.i <= 0.01)
            {
                this.Opacity = 0.0;
                AppStart.IsBool = 3;
                this.Close();
                //this.timerFadeOut.Enabled = false;
                //frmLogin frmLogin = new CIS_Textil.frmLogin();
                //frmLogin.ShowDialog();
            }
            else
            {
                this.Opacity = this.i;
            }
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            try
            {
                if (CboCompany.SelectedValue == null)
                    CboCompany.SelectedValue = "0";
                if (CboYear.SelectedValue == null)
                    CboYear.SelectedValue = "0";

                if (/*(this.CboCompany.SelectedValue.ToString().Length <= 0) | */(this.CboYear.SelectedValue.ToString().Length <= 0))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Select Year", "Year not selected...");
                }
                else
                {
                    if (CboCompany.SelectedValue.ToString() == "0")
                    {
                        if ((Db_Detials.UserType == 1) || (Db_Detials.UserType == 2))
                        {
                            Db_Detials.CompID = Localization.ParseNativeInt(DB.GetSnglValue("Select Top 1 CompanyId From tbl_CompanyMaster"));
                            Db_Detials.CompanyName = DB.GetSnglValue("Select Top 1 CompanyName From tbl_CompanyMaster");
                        }
                        else
                        {
                            Db_Detials.CompID = Localization.ParseNativeInt(DB.GetSnglValue("Select Top 1 CompanyId From tbl_CompanyMaster Where CompanyID in (Select CompID from tbl_UserMasterDtls where UserID=" + Db_Detials.UserID + ")"));
                            Db_Detials.CompanyName = DB.GetSnglValue("Select Top 1 CompanyName From tbl_CompanyMaster Where CompanyID in (Select CompID from tbl_UserMasterDtls where UserID=" + Db_Detials.UserID + ")");
                        }
                        Db_Detials.YearID = Localization.ParseNativeInt(this.CboYear.SelectedValue.ToString());
                        try
                        {
                            int iactionType = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster_tbl() Where MiscName='IsLogin'"));
                            DBSp.Log_CurrentUser(1, iactionType, 0, 0, 0, 0);
                        }
                        catch { }
                    }
                    else
                    {
                        Db_Detials.CompID = Localization.ParseNativeInt(this.CboCompany.SelectedValue.ToString());
                        Db_Detials.CompanyName = this.CboCompany.Text;
                        Db_Detials.YearID = Localization.ParseNativeInt(this.CboYear.SelectedValue.ToString());
                        try
                        {
                            int iactionType = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster_tbl() Where MiscName='IsLogin'"));
                            DBSp.Log_CurrentUser(1, iactionType, 0, 0, 0, 0);
                        }
                        catch (Exception ex1)
                        {
                            Navigate.logError(ex1.Message, ex1.StackTrace);
                        }
                    }
                }
                this.timerFadeOut.Enabled = true;

            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void UpDateUserStatus()
        {
            try
            {
                int session = Localization.ParseNativeInt(DB.GetSnglValue("Select Sessions From tbl_UserMaster Where isdeleted=0 and UserID=" + Db_Detials.UserID + ""));
                DB.ExecuteSQL(string.Format("Update {0} Set IsLoggedIn = 0, IPAddress='{1}',Sessions={2} Where UserID = {3}", "tbl_UserMaster", CommonCls.GetIP(), session - 1, Db_Detials.UserID));
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            try
            {
                Form cForm = null;
                Navigate.NavigateForm(Enum_Define.Navi_form.Close_form, ref cForm, true, false);
                UpDateUserStatus();
                AppStart.IsBool = 1;
                this.Close();
                // Application.Exit();
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
    }
}
