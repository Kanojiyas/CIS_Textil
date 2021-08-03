using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmIntro : Form
    {
        IniFile ini;
        string ServerMACHINENAME = System.Environment.MachineName;

        bool IsDemoUser = false;
        Double i = 0.1;
        static int j = 0;
        string strOtp = string.Empty;

        private bool IsConnected = false;
        private bool IsAttachDb = false;
        private bool blnStartTimerFadeIn = true;

        public frmIntro()
        {
            InitializeComponent();
        }

        private void frmIntro_Load(object sender, EventArgs e)
        {
            //string sUid =CIS_Bussiness.CommonLogic.UnmungeString("6up7UOx8SwNudY/Ey6uHHw==");
            //string sPwd = CIS_Bussiness.CommonLogic.UnmungeString("TOKZ68j+0dBFX0d65qrPrA==");
            ini = new IniFile(Application.StartupPath.ToString() + "\\Others\\System.ini");
            #region CHANGE DATE FORMATE OF SYSTEM TO dd/MM/yyyy
            string keyName = Registry.CurrentUser.ToString() + "\\Control Panel\\International";
            string valueName = "sShortDate";
            string s = Registry.GetValue(keyName, valueName, string.Empty).ToString();
            if (s != "dd/MM/yyyy")
            {
                Registry.SetValue(keyName, valueName, "dd/MM/yyyy");
            }
            #endregion
            lblAppVer.Text = Db_Detials.AppVersion;
            //this.lblversion.Text = "Version : " + Application.ProductVersion;
            this.lblLicense.Text = "License : ";

            if (CommonCls.CheckForInternetConnection())
            {
                SendOtp();
            }
            CheckConnectionsAndSettings();
            Timer1.Enabled = true;
            this.Opacity = this.i;
            this.timerFadeIn.Enabled = true;
            this.timerFadeOut.Enabled = false;

            try
            {
                object sReportPath = null;
                Db_Detials.objReport = new ReportDocument();
                if (Application.StartupPath.ToString().Contains(@"bin\Debug"))
                {
                    sReportPath = Application.StartupPath.ToString().Replace(@"bin\Debug", "") + @"Reports\CompanyHeader.rpt";
                }
                else
                {
                    sReportPath = Application.StartupPath.ToString() + @"\Reports\CompanyHeader.rpt";
                }
                Db_Detials.objReport.Load(Conversions.ToString(sReportPath));
            }
            catch { }
        }

        public void CheckConnectionsAndSettings()
        {
            try
            {
                System.ServiceProcess.ServiceController[] theServices = null;
                theServices = System.ServiceProcess.ServiceController.GetServices();
                string ServerMachineName = System.Environment.MachineName.ToUpper();
                bool running = false;
                bool NotFind = true;
                string sIPAddress = null;
                string sInstanceNM = null;
                string sPortNo = null;
                sIPAddress = "";

                if (CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ConnectType")).ToUpper() == "ISONLINE")
                {
                    if (CommonCls.CheckForInternetConnection() == false)
                    {
                        AppStart.isSuccessFullLogIn = false;
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Internet not Connected... Please check Internet Connection", "");
                        System.Environment.Exit(0);
                    }
                    else
                    {
                        IPInfo.MIscServicesSoapClient _ipInfo = new IPInfo.MIscServicesSoapClient("MIscServicesSoap");
                        sIPAddress = _ipInfo.GetIPAdd(CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ClientID")));
                        sInstanceNM = _ipInfo.GetInstanceNM(CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ClientID")));
                        sPortNo = _ipInfo.GetPortNo(CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ClientID")));

                        ini.IniWriteValue("DATABASESETTING", "ServerName", CommonLogic.MungeString(sIPAddress + "\\" + sInstanceNM + "," + sPortNo));
                        // string comm = CommonLogic.MungeString("2");
                        if (CommonCls.CheckConnection() == false)
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Online Server not Connecting... Please Contact System Administrator", "");
                            AppStart.isSuccessFullLogIn = false;
                        }
                        else
                        {
                            int icount = Localization.ParseNativeInt(DB.GetSnglValue("Select Count(0) From tbl_CompanyMaster Where IsDeleted=0"));
                            if (icount > 0)
                            {

                                using (IDataReader dr = DB.GetRS(string.Format(Db_Detials.sp_FetchCompSelection)))
                                {
                                    if (dr.Read())
                                        Db_Detials.CompanyName = dr["CompanyName"].ToString();
                                }
                                Db_Detials.DbName = ini.IniReadValue("DATABASESETTING", "DataBaseName");
                            }
                            else
                            {
                                frmCompCreation frm = new frmCompCreation();
                                if (frm.ShowDialog() == DialogResult.Cancel)
                                {
                                    frm.Dispose();
                                    return;
                                }
                                else
                                {
                                    using (IDataReader dr = DB.GetRS(string.Format(Db_Detials.sp_FetchCompSelection)))
                                    {
                                        if (dr.Read())
                                            Db_Detials.CompanyName = dr["CompanyName"].ToString();
                                    }
                                    Db_Detials.DbName = ini.IniReadValue("DATABASESETTING", "DataBaseName");
                                }
                            }
                            Timer1.Enabled = true;
                        }
                    }
                }

                else if (CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ConnectType")).ToUpper() == "NETWORK")
                {
                    if (CommonCls.CheckConnection() == false)
                    {
                        Timer1.Enabled = false;
                        timerFadeOut.Enabled = false;
                        AppStart.isSuccessFullLogIn = false;
                        if (CIS_Utilities.CIS_Dialog.Show("SQL SERVER cannot access the Database!. Do you want to change database setting?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            frmDBSettings frm = new frmDBSettings();
                            frm.pnlAttachDB.Visible = false;
                            frm.grp_DBSetting.Visible = true;
                            frm.ShowDialog();
                            Timer1.Enabled = true;
                            IsConnected = true;
                            timerFadeIn.Enabled = true;
                            //AppStart.IsBool = 0;
                        }
                        else
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Network Server not connecting... Please Contact System Administrator ", "");
                            System.Environment.Exit(0);
                        }
                    }

                }

                else if (CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ConnectType")).ToUpper() == "LOCAL")
                {
                    if (ServerMachineName == CommonLogic.UnmungeString(ini.IniReadValue("SERVERMACHINENAME", "MACHINEName")))
                    {
                        foreach (System.ServiceProcess.ServiceController theservice in theServices)
                        {
                            if ((theservice.ServiceName == "MSSQL$SQLEXPRESS") || (theservice.ServiceName == "MSSQLEXPRESS") || (theservice.ServiceName == "MSSQL$SQLSERVER") || (theservice.ServiceName == "MSSQLSERVER"))
                            {
                                running = (theservice.Status == System.ServiceProcess.ServiceControllerStatus.Running);
                                NotFind = false;
                                break;
                            }
                        }

                        if (NotFind == true)
                        {
                            Timer1.Enabled = false;
                            timerFadeOut.Enabled = false;
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Application not finding SQL Server instance on this System... Please Contact System Administrator", "");
                            System.Environment.Exit(0);
                        }
                        else if ((NotFind == false) && (running == true))
                        {
                            DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();
                            string ServerName = CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ServerName"));
                            string[] sSplit = ServerName.Split('\\');

                            DataRow[] rst = servers.Select("ServerName=" + CommonLogic.SQuote(sSplit[0].ToString()) + " and InstanceName=" + CommonLogic.SQuote(sSplit[1].ToString()));
                            if (rst.Length == 0)
                            {
                                Timer1.Enabled = false;
                                timerFadeOut.Enabled = false;
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "SQL Server Instance not found..", "");
                                System.Environment.Exit(0);
                            }
                        }
                        else if (running == false)
                        {
                            Timer1.Enabled = false;
                            timerFadeOut.Enabled = false;
                            timerFadeIn.Enabled = false;
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "SQL Server Engine is Stopped. Please start the SQL Server Engine.", "");
                            System.Environment.Exit(0);
                        }
                    }
                    if (CommonCls.CheckConnection() == false)
                    {
                        Timer1.Enabled = false;
                        timerFadeOut.Enabled = false;
                        timerFadeIn.Enabled = false;
                        if (ServerMachineName == CommonLogic.UnmungeString(ini.IniReadValue("SERVERMACHINENAME", "MACHINEName")))
                        {
                            if (CIS_Utilities.CIS_Dialog.Show("SQL SERVER cannot access the Database!. If you want to Attach Database click on ‘Yes’, to cancel click on ‘No’", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                frmDBSettings frm = new frmDBSettings();
                                frm.pnlAttachDB.Visible = true;
                                frm.grp_DBSetting.Visible = false;
                                frm.ShowDialog();
                                IsAttachDb = true;
                                timerFadeIn.Enabled = true;
                            }
                            else
                            {
                                System.Environment.Exit(0);
                            }
                        }
                        else
                        {
                            CIS_Utilities.CIS_Dialog.Show("This Client System cannot access the Server Please make sure that server system Accessibility", "Connection error");
                            System.Environment.Exit(0);
                        }
                    }
                    else
                    {
                        try
                        {
                            int icount = Localization.ParseNativeInt(DB.GetSnglValue("Select Count(0) From tbl_CompanyMaster Where IsDeleted=0"));
                            if (icount > 0)
                            {

                                using (IDataReader dr = DB.GetRS(string.Format(Db_Detials.sp_FetchCompSelection)))
                                {
                                    if (dr.Read())
                                        Db_Detials.CompanyName = dr["CompanyName"].ToString();
                                }
                                Db_Detials.DbName = ini.IniReadValue("DATABASESETTING", "DataBaseName");
                            }
                            else
                            {
                                Timer1.Enabled = false;
                                timerFadeOut.Enabled = false;
                                timerFadeIn.Enabled = false;
                                frmCompCreation frm = new frmCompCreation();
                                if (frm.ShowDialog() == DialogResult.Cancel)
                                {
                                    frm.Dispose();
                                    return;
                                }
                                else
                                {
                                    using (IDataReader dr = DB.GetRS(string.Format(Db_Detials.sp_FetchCompSelection)))
                                    {
                                        if (dr.Read())
                                            Db_Detials.CompanyName = dr["CompanyName"].ToString();
                                    }
                                    Db_Detials.DbName = ini.IniReadValue("DATABASESETTING", "DataBaseName");
                                }
                            }
                            Timer1.Enabled = true;
                        }
                        catch (Exception ex)
                        {
                            Navigate.logError(ex.Message, ex.StackTrace);
                        }
                    }
                }
            }
            catch { }
        }

        private void btnActOffline_Click(object sender, EventArgs e)
        {
            CIS_Streamfile.common pro_file = new CIS_Streamfile.common();
            string strCurrSerial = CIS_Bussiness.ClsSecure.CreateSecInfo_1(pro_file.CreateInfo(), Crypto.EncodingType.HEX, false);

            // Suppose full file that we want to protect
            FileInfo fi = new FileInfo(System.Windows.Forms.Application.StartupPath.ToString() + "\\client_lic.dat");
            if (fi.Exists)
                fi.Delete();

            StreamWriter stream_writer = default(StreamWriter);
            stream_writer = File.CreateText(System.Windows.Forms.Application.StartupPath.ToString() + "\\client_lic.dat");
            stream_writer.WriteLine(strCurrSerial);
            stream_writer.Close();

            //~~ Original file name to store in encrypted file.
            string strOriginalFilename = fi.Name;

            // Full name of protected file
            string strNewFilename = fi.DirectoryName + "\\" + fi.Name.Substring(0, fi.Name.Length - fi.Extension.Length) + ".cis";
            pro_file.ProtectFile(fi.FullName, strNewFilename, strOriginalFilename);
            fi.Delete();

            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecuritySuccess, "Client License", "Client License created successfully...");
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
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
                if (blnStartTimerFadeIn == true)
                    Timer1.Enabled = true;
                return;
            }
            this.Opacity = i;
        }

        private void timerFadeOut_Tick(object sender, EventArgs e)
        {
            i -= 0.10;
            timerFadeIn.Enabled = false;
            ////stop the Fade In Effect
            //if form is invisible we execute the Fade In Effect again
            if ((i <= 0.01))
            {
                this.Opacity = 0.0;
                AppStart.IsBool = 1;
                if (IsConnected || IsAttachDb)
                {
                    AppStart.IsBool = 0;
                }
                this.Close();
                return;
            }
            this.Opacity = i;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            j = j + 1;
            if (IsDemoUser == true)
                j = 0;
            if (j == 10 || j > 10)
                ////start the Fade Out
                timerFadeOut.Enabled = true;
            //else { timerFadeOut.Enabled = true; }
        }

        public string GetMACAddress2()
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            String sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)// only return MAC Address from first card  
                {
                    IPInterfaceProperties properties = adapter.GetIPProperties();
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            } return sMacAddress;
        }

        public void SendOtp()
        {
            string strQry = string.Empty;
            DateTime date1 = Conversions.ToDate(DateAndTime.Now.Date.ToString());
            string strOtpDate = DB.GetSnglValue("Select Top 1 OtpDate From fn_UserMaster_tbl() Where IsUserOtp=1 Order By OtpDate Desc");
            if (strOtpDate != "")
            {
                DateTime date2 = Conversions.ToDate(strOtpDate);
                if (DateTime.Compare(date2, date1) < 0)
                {
                    using (IDataReader idr = DB.GetRS("Select * from fn_UserMaster_tbl() Where IsUserOtp=1"))
                    {
                        while (idr.Read())
                        {
                            GenOtp();
                            strQry = string.Format("Update tbl_UserMaster Set Password={0},otp={1},OtpDate={2} Where UserID={3} and IsDeleted=0;", CommonLogic.SQuote(CommonLogic.MungeString(strOtp)), CommonLogic.SQuote(CommonLogic.MungeString(strOtp)), CommonLogic.SQuote(Localization.ToSqlDateString(DateAndTime.Now.Date.ToString())), idr["UserID"].ToString());
                            DB.ExecuteSQL(strQry);
                            //CommonCls.SendOtpSms(idr["UserID"].ToString(), "38");
                        }
                    }
                }
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
    }
}
