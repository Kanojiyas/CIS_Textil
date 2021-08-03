using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using CIS_DBLayer;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class frmLogin : Form
    {
        Double i = 0.1;
        string strOtp = string.Empty;

        public frmLogin()
        {
            InitializeComponent();
            string sIPAddress = null;
            string sInstanceNM = null;
            string sPortNo = null;
            sIPAddress = "";
            //IniFile ini = new IniFile(Application.StartupPath.ToString() + "\\Others\\System.ini");
            //if (ini.IniReadValue("DATABASESETTING", "IsOnline").ToUpper() == "TRUE")
            //{
            //    IPInfo.MIscServicesSoapClient _ipInfo = new IPInfo.MIscServicesSoapClient("MIscServicesSoap2");
            //    sIPAddress = _ipInfo.GetIPAdd(CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ClientID")));
            //    sInstanceNM = _ipInfo.GetInstanceNM(CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ClientID")));
            //    sPortNo = _ipInfo.GetPortNo(CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ClientID")));
            //    ini.IniWriteValue("DATABASESETTING", "ServerName", sIPAddress + "\\" + sInstanceNM + "," + sPortNo);
            //}
            //else
            //{
            //    ini.IniWriteValue("DATABASESETTING", "ServerName", ini.IniReadValue("DATABASESETTING", "LANServerName"));
            //    frmDBSettings fDBS = new frmDBSettings();
            //    fDBS.ShowDialog();
            //    if (!AppStart.isSuccessFullLogIn)
            //    {
            //        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "Info", "Please restart the application.");
            //        AppStart.IsBool = 0;
            //    }
            //}
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            this.Opacity = i;
            //set the opacity of the form to 0.1 when form load
            timerFadeIn.Enabled = true;
            //start the Fade In Effect
            timerFadeOut.Enabled = false;

            txtUserName.Focus();
            this.TopLevel = true;
            txtUserName.Select();
            grp_Login.Visible = true;
            GrpForgetPwd.Visible = false;
            Navigate.AppInfoTitle = "";
            lblCompanyName.Text = Db_Detials.CompanyName;
            if (Db_Detials.CompanyName != "")
            {
                try
                {
                    using (IDataReader dr = DB.GetRS(string.Format(Db_Detials.sp_FetchCompSelection)))
                    {
                        if (dr.Read())
                        {
                            Db_Detials.CompanyName = dr["CompanyName"].ToString();
                            lblCompanyName.Text = Db_Detials.CompanyName;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                }
            }

            IniFile ini = new IniFile(Application.StartupPath.ToString() + "\\Others\\System.ini");

            txtUserName.Text = ini.IniReadValue("UserLogin", "UserName");
            txtPassword.Text = ini.IniReadValue("UserLogin", "Password");
            txtUserName.Select();

            if (!CheckAppVer())
            {
                Interaction.MsgBox("The version of application you are running is older. Please update to new version and try again..", MsgBoxStyle.Critical, "Older Version of Application");
                System.Environment.Exit(0);
            }
        }

        private bool CheckAppVer()
        {
            string sCurrVer_DB = DB.GetSnglValue("SELECT ConfigVALUE FROM tbl_appConfig WHERE ConfigName='APPVERSION'");
            string[] sVer_DB = sCurrVer_DB.Split(' ');
            string[] sVer_Curr = Db_Detials.AppVersion.Split(' ');
            if (sCurrVer_DB != "")
            {
                double dblVer_DB = Localization.ParseNativeDouble(sVer_DB[1].Replace(".", "").ToString());
                double dblVer_Curr = Localization.ParseNativeDouble(sVer_Curr[1].Replace(".", "").ToString());
                
                if ((sCurrVer_DB != "") && (Db_Detials.AppVersion != sCurrVer_DB))
                {
                    if (dblVer_Curr > dblVer_DB)
                    {
                        DB.ExecuteSQL("UPDATE tbl_appConfig SET ConfigVALUE=" + CommonLogic.SQuote(Db_Detials.AppVersion) + "," + " Description=" + CommonLogic.SQuote(CommonCls.GetIP()) + " WHERE ConfigName='APPVERSION'");
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else if (sCurrVer_DB.Length == 0)
            {
                string sMaxAppCnfID = DB.GetSnglValue("SELECT IsNull(MAX(ConfigID)+1,0) FROM tbl_appConfig;");
                DB.ExecuteSQL(string.Format("INSERT INTO tbl_appConfig (ConfigGroup,ConfigID,ConfigName,Description,ConfigValue,StoreID,AddedOn,AddedBy,IsModified,UpdatedOn,UpdatedBy,IsDeleted,DeletedOn,DeletedBy,IsCanclled,CancelledOn,CancelledBy,IsApproved,ApprovedOn,ApprovedBy,IsAudited,AuditedOn,AuditedBy) VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22});",
                         "'APP'", sMaxAppCnfID, "'APPVERSION'", "'LATEST VERSION OF APPLICATION'", CommonLogic.SQuote(Db_Detials.AppVersion), 1, "getdate()", Db_Detials.UserID, 0, "NULL", "NULL", 0, "NULL", "NULL", 0, "NULL", "NULL", 0, "NULL", "NULL", 0, "NULL", "NULL"));

                return true;
            }
            return true;
        }

        private void frmLogin_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //    SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape)
            {
                try
                {
                    System.Environment.Exit(Localization.ParseNativeInt(CloseReason.ApplicationExitCall.ToString()));
                }
                catch (Exception ex)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
                    Navigate.logError(ex.Message, ex.StackTrace);
                }
            }
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { this.Dispose(); }
            catch { }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if ((txtUserName.Text.Trim().Length) == 0)
                {
                    Interaction.MsgBox("Please enter user name", MsgBoxStyle.RetryCancel, "Security Warning");
                    return;
                }
                else if ((txtPassword.Text.Trim().Length) == 0)
                {
                    Interaction.MsgBox("Please enter Password", MsgBoxStyle.RetryCancel, "Security Warning");
                    return;
                }
                string s = CommonLogic.MungeString(this.txtPassword.Text.Trim());
                using (SqlDataReader reader = DB.GetRS(string.Format("Select UserID, UserTypeID From {0} Where UserName = {1} And Password = {2}", "fn_userMaster_tbl()", DB.SQuote(this.txtUserName.Text.Trim()), DB.SQuote(CommonLogic.MungeString(this.txtPassword.Text.Trim())))))
                {
                    if (reader.Read())
                    {
                        Db_Detials.UserID = Conversions.ToInteger(reader["UserID"].ToString());
                        Db_Detials.UserType = Conversions.ToInteger(reader["UserTypeID"].ToString());
                        int strcount = Localization.ParseNativeInt(DB.GetSnglValue("Select UserID from fn_userMaster_tbl() Where UserID=" + Db_Detials.UserID + " and IPaddress<>'" + CommonCls.GetIP() + "' and IsLoggedIn=1 and Sessions<>0"));
                        int iisLogin = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster_tbl() Where MiscName='isLogin'"));
                        int iCompID = Localization.ParseNativeInt(DB.GetSnglValue("Select CompID,max(userdt) from tbl_UserReportLog Where ActionType=" + iisLogin + " and UserID= " + strcount + " group by StoreID,CompID,BranchID,YearID"));
                        int iYearID = Localization.ParseNativeInt(DB.GetSnglValue("Select YearID,max(userdt) from tbl_UserReportLog Where ActionType=" + iisLogin + " and UserID=" + strcount + " group by StoreID,CompID,BranchID,YearID"));
                        int iBranchID = Localization.ParseNativeInt(DB.GetSnglValue("Select BranchID,max(userdt) from tbl_UserReportLog Where ActionType=" + iisLogin + " and UserID= " + strcount + " group by StoreID,CompID,BranchID,YearID"));
                        int iStoreID = Localization.ParseNativeInt(DB.GetSnglValue("Select StoreID,max(userdt) from tbl_UserReportLog Where ActionType=" + iisLogin + " and UserID=" + strcount + " group by StoreID,CompID,BranchID,YearID"));

                        if (strcount > 0)
                        {
                            if (MessageBox.Show("User Already Login in Another System , Do You Want to Force Close The User ?", GetAssemblyInfo.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                btnLogin.Enabled = false;
                                string strQry = string.Empty;
                                // strQry = string.Format("Update tbl_UserMaster set Sessions={0} , Isloggedin={1}, IPAddress='{2}' where IsDeleted=0 and UserID={3} ", 0, 0, CommonCls.GetIP(), strcount);
                                strQry = string.Format("Update tbl_UserMaster set Sessions={0} , Isloggedin={1}  where IsDeleted=0 and UserID={2} and IPAddress != " + "'" + CommonCls.GetIP() + "'" + "", 0, 0, strcount);
                                DB.ExecuteSQL(strQry);
                                int iactionType = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster_tbl() Where MiscName='isLogOut'"));
                                try
                                {
                                    DB.ExecuteSQL("INSERT INTO tbl_UserReportLog(MenuID, ReportID, IsCrystalReport, IsBarCode, IsChequePrint, ActionType, UserID, UserDt, StoreID, CompID,BranchID ,YearID) VALUES(1, 0, 0, 0, 0, " + iactionType + "," + strcount + ",getdate()" + ","+iStoreID+"," + iCompID + ","+iBranchID+"," + iYearID + ")");
                                }
                                catch (Exception ex)
                                {
                                    Navigate.logError(ex.Message, ex.StackTrace);
                                }

                                Thread t = new Thread(new ThreadStart(LoginWait));
                                t.Start();
                                Thread.Sleep(9000);
                                btnLogin.Enabled = true;
                            }
                            return;
                        }
                        else
                        {
                            int session = Localization.ParseNativeInt(DB.GetSnglValue("Select Sessions From fn_UserMaster_tbl() Where UserID=" + Db_Detials.UserID + ""));
                            DB.ExecuteSQL(string.Format("Update {0} Set IsLoggedIn = 1, IPAddress='{1}',Sessions={2} Where UserID = {3}", "tbl_userMaster", CommonCls.GetIP(), session + 1, Db_Detials.UserID));
                        }
                        this.timerFadeOut.Enabled = true;
                    }
                    else
                    {
                        Interaction.MsgBox("Not a Valid User", MsgBoxStyle.Information, "Security Warning");
                    }
                }
            }
            catch (Exception exception1)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", exception1.Message);
                Navigate.logError(exception1.Message, exception1.StackTrace);
            }
        }

        private void LoginWait()
        {

        }

        private void btnInISave_Click(object sender, EventArgs e)
        {
            IniFile file = new IniFile(Application.StartupPath.ToString() + @"\Others\System.ini");
            //file.IniWriteValue("DATABASESETTING", "ServerName", this.txt_ServerName.Text);
            //file.IniWriteValue("DATABASESETTING", "DataBaseName ", this.txt_DbName.Text);            
            //this.txtUserName.Focus();
        }

        private void timerFadeOut_Tick(object sender, EventArgs e)
        {
            //Fade out effect
            i -= 0.10;

            timerFadeIn.Enabled = false;
            ////stop the Fade In Effect
            if ((i <= 0.01))
            {
                //if form is invisible we execute the Fade In Effect again
                this.Opacity = 0.0;
                AppStart.IsBool = 2;
                this.Close();
                return;
            }
            this.Opacity = i;

        }

        private void timerFadeIn_Tick(object sender, EventArgs e)
        {
            i += 0.10;

            //if form is full visible we execute the Fade Out Effect
            if (i >= 1)
            {
                this.Opacity = 1;
                timerFadeIn.Enabled = false;
                ////stop the Fade In Effect
                return;
            }
            this.Opacity = i;
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

        private void lnkDBSettings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AppStart.IsBool = 5;
            this.Close();
        }

        private void lnkForgerPwd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            grp_Login.Visible = false;
            GrpForgetPwd.Visible = true;
        }

        private void btnCancelForget_Click(object sender, EventArgs e)
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

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                SendOtp();
            }
            catch
            { }
        }

        public void SendOtp()
        {
            string strQry = string.Empty;
            int iUserID = Localization.ParseNativeInt(DB.GetSnglValue("Select IsNull(UserID,0) AS UserID From fn_UserMaster_tbl() Where UserName='" + txtOtpUserName.Text + "'"));
            if (iUserID != 0)
            {
                GenOtp();
                strQry = string.Format("Update tbl_UserMaster Set Password={0},otp={1},OtpDate={2},mobileNo={3} Where UserID={4} and IsDeleted=0;", CommonLogic.SQuote(CommonLogic.MungeString(strOtp)), CommonLogic.SQuote(CommonLogic.MungeString(strOtp)), CommonLogic.SQuote(Localization.ToSqlDateString(DateAndTime.Now.Date.ToString())), CommonLogic.SQuote(txtOtpMobileNo.Text), iUserID);
                DB.ExecuteSQL(strQry);
                //CommonCls.SendOtpSms(Convert.ToString(iUserID), "38");
                grp_Login.Visible = true;
                GrpForgetPwd.Visible = false;
            }
            else
            {
                Interaction.MsgBox("This UserName " + txtOtpUserName.Text + " is not valid.", MsgBoxStyle.RetryCancel, "Security Warning");
            }
        }

        public string GenOtp()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz@$";
            Random random = new Random();
            string result = new string(
            Enumerable.Repeat(chars, 10)
                      .Select(s => s[random.Next(s.Length)])
                      .ToArray());
            strOtp = result.ToString();
            return strOtp;
        }

        private void lnkLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            grp_Login.Visible = true;
            GrpForgetPwd.Visible = false;
        }

    }
}