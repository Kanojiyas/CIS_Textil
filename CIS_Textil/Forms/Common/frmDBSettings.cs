using System;
using System.Data;
using System.Data.Sql;
using System.IO;
using System.Windows.Forms;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmDBSettings : Form
    {
        bool IsDemoUser = false;
        Double i = 0.1;
        public bool SqlConnected = false;
        public bool AttachDb = false;
        static int j = 0;
        IniFile ini;
        string ServerMACHINENAME = System.Environment.MachineName;

        public frmDBSettings()
        {
            InitializeComponent();
        }

        private void frmDBSettings_Load(object sender, EventArgs e)
        {
            try
            {
                ini = new IniFile(Application.StartupPath.ToString() + "\\Others\\System.ini");
                pnlAttachDB.Visible = false;
                this.lblversion.Text = "Version : " + Application.ProductVersion;
                grp_DBSetting.Visible = true;
                LoadPreviousSettings();
                // CheckConnectionsAndSettings();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                System.Environment.Exit(0);
            }
        }

        //public void CheckConnectionsAndSettings()
        //{
        //    try
        //    {
        //        System.ServiceProcess.ServiceController[] theServices = null;
        //        theServices = System.ServiceProcess.ServiceController.GetServices();
        //        string ServerMachineName = System.Environment.MachineName.ToUpper();
        //        bool running = false;
        //        bool NotFind = true;
        //        string sIPAddress = null;
        //        string sInstanceNM = null;
        //        string sPortNo = null;
        //        sIPAddress = "";

        //        if (CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ConnectType")).ToUpper() == "ISONLINE")
        //        {

        //            if (CommonCls.CheckForInternetConnection() == false)
        //            {
        //                AppStart.isSuccessFullLogIn = false;
        //                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Internet not Connected... Please check Internet Connection", "");
        //            }
        //            else
        //            {
        //                IPInfo.MIscServicesSoapClient _ipInfo = new IPInfo.MIscServicesSoapClient("MIscServicesSoap");
        //                sIPAddress = _ipInfo.GetIPAdd(CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ClientID")));
        //                sInstanceNM = _ipInfo.GetInstanceNM(CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ClientID")));
        //                sPortNo = _ipInfo.GetPortNo(CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ClientID")));

        //                ini.IniWriteValue("DATABASESETTING", "ServerName", CommonLogic.MungeString(sIPAddress + "\\" + sInstanceNM + "," + sPortNo));
        //                // string comm = CommonLogic.MungeString("2");
        //                if (CommonCls.CheckConnection() == false)
        //                {
        //                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Online Server not Connecting... Please Contact System Administrator", "");
        //                    AppStart.isSuccessFullLogIn = false;
        //                }
        //            }
        //        }

        //        else if (CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ConnectType")).ToUpper() == "NETWORK")
        //        {
        //            if (CommonCls.CheckConnection() == false)
        //            {
        //                AppStart.isSuccessFullLogIn = false;
        //                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Network Server not connecting... Please Contact System Administrator ", "");
        //            }

        //        }

        //        else if (CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ConnectType")).ToUpper() == "LOCAL")
        //        {
        //            if (ServerMachineName == CommonLogic.UnmungeString(ini.IniReadValue("SERVERMACHINENAME", "MACHINEName")))
        //            {
        //                foreach (System.ServiceProcess.ServiceController theservice in theServices)
        //                {
        //                    if ((theservice.ServiceName == "MSSQL$SQLEXPRESS") || (theservice.ServiceName == "MSSQLEXPRESS") || (theservice.ServiceName == "MSSQL$SQLSERVER") || (theservice.ServiceName == "MSSQLSERVER"))
        //                    {
        //                        running = (theservice.Status == System.ServiceProcess.ServiceControllerStatus.Running);
        //                        NotFind = false;

        //                        break;
        //                    }
        //                }

        //                if (NotFind == true)
        //                {
        //                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Application not finding SQL Server instance on this System... Please Contact System Administrator", "");
        //                }
        //                else if ((NotFind == false) && (running == true))
        //                {
        //                    DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();
        //                    string ServerName = CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ServerName"));
        //                    string[] sSplit = ServerName.Split('\\');

        //                    DataRow[] rst = servers.Select("ServerName=" + CommonLogic.SQuote(sSplit[0].ToString()) + " and InstanceName=" + CommonLogic.SQuote(sSplit[1].ToString()));
        //                    if (rst.Length == 0)
        //                    {
        //                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "SQL Server Instance not found..", "");
        //                    }
        //                }
        //                else if (running == false)
        //                {
        //                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "SQL Server Engine is Stopped. Please start the SQL Server Engine.", "");
        //                }
        //            }
        //            if (CommonCls.CheckConnection() == false)
        //            {
        //                if (ServerMachineName == CommonLogic.UnmungeString(ini.IniReadValue("SERVERMACHINENAME", "MACHINEName")))
        //                {
        //                    if (CIS_Utilities.CIS_Dialog.Show("SQL SERVER cannot access the Database!. If you want to Attache Database click on ‘Yes’, to cancel click on ‘No’", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //                    {
        //                        AppStart.IsBool = 5;
        //                        AttachDb = true;
        //                        pnlAttachDB.Visible = true;
        //                        grp_DBSetting.Visible = false;
        //                        SqlConnected = false;
        //                    }
        //                    else
        //                    {
        //                        SqlConnected = false;
        //                        System.Environment.Exit(0);
        //                    }
        //                }
        //                else
        //                {
        //                    CIS_Utilities.CIS_Dialog.Show("This Client System cannot access the Server Please make sure that server system Accessibility", "Connection error");
        //                    // Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Question, "Connection error", "This Client System cannot access the Server Pleae make sure that server system Accessibility");
        //                    System.Environment.Exit(0);
        //                }
        //            }
        //            else
        //            {
        //                try
        //                {
        //                    using (IDataReader dr = DB.GetRS(string.Format(Db_Detials.sp_FetchCompSelection)))
        //                    {
        //                        if (dr.Read())
        //                        {
        //                            Db_Detials.CompanyName = dr["CompanyName"].ToString();
        //                        }
        //                        else
        //                        {
        //                        }
        //                    }
        //                    Db_Detials.DbName = ini.IniReadValue("DATABASESETTING", "DataBaseName");
        //                    SqlConnected = true;
        //                    //this.Close();
        //                }
        //                catch (Exception ex)
        //                {
        //                    Navigate.logError(ex.Message, ex.StackTrace);
        //                }
        //            }
        //        }
        //    }
        //    catch { }
        //}

        private void btnMdf_Click(System.Object sender, System.EventArgs e)
        {
            OpenFileDialog1.FileName = string.Empty;
            this.OpenFileDialog1.ShowDialog(this);
            if (!string.IsNullOrEmpty(OpenFileDialog1.FileName))
            {
                txtMdf.Text = OpenFileDialog1.FileName;
            }
        }

        private void btnLdf_Click(System.Object sender, System.EventArgs e)
        {
            OpenFileDialog2.FileName = string.Empty;
            this.OpenFileDialog2.ShowDialog(this);
            if (!string.IsNullOrEmpty(OpenFileDialog2.FileName))
            {
                txtLdf.Text = OpenFileDialog2.FileName;
            }
        }

        private void btnAttachDB_Click(object sender, EventArgs e)
        {
            if (txtMdf.Text.Trim() == "" && txtMdf.Text.Trim().Length == 0)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Please Select Database MDF File...", "");
            }

            IniFile ini = new IniFile(Application.StartupPath.ToString() + "\\Others\\System.ini");
            if (ini.IniReadValue("DATABASESETTING", "IsIntegratedSecurity") == "1")
            {
                DB._activeDBConn = string.Format("data source={0};initial catalog={1};Min Pool Size=5;Max Pool Size=500;User ID={2};Password={3};Trusted_Connection=False;packet size=4096;", ini.IniReadValue("DATABASESETTING", "ServerName"), "Master", DB._UserName, DB._Password);
            }
            else
            {
                DB._activeDBConn = string.Format("data source={0};initial catalog={1};Min Pool Size=5;Max Pool Size=500;Integrated Security=SSPI;Trusted_Connection=True;persist security info=True;packet size=4096;Connect Timeout=60;", ini.IniReadValue("DATABASESETTING", "ServerName"), "Master");
            }

            if (txtMdf.Text.Trim().Length > 0 & txtLdf.Text.Trim().Length > 0)
            {
                if (Localization.ParseNativeInt(DB.GetSnglValue(string.Format("select count(0) From sys.databases where name = '{0}'", ini.IniReadValue("DATABASESETTING", "DataBaseName")))) > 0)
                {
                    try
                    {
                        DB.ExecuteSQL(string.Format("sp_detach_db '{0}', true ", ini.IniReadValue("DATABASESETTING", "DataBaseName")));
                    }
                    catch
                    {
                    }
                }
                DB.ExecuteSQL(string.Format("sp_Attach_db '{0}','{1}','{2}' ", ini.IniReadValue("DATABASESETTING", "DataBaseName"), txtMdf.Text, txtLdf.Text));
            }
            if (ini.IniReadValue("DATABASESETTING", "IsIntegratedSecurity") == "1")
            {
                DB._activeDBConn = string.Format("data source={0};initial catalog={1};Min Pool Size=5;Max Pool Size=500;User ID={2};Password={3};Trusted_Connection=False;packet size=4096;", ini.IniReadValue("DATABASESETTING", "ServerName"), ini.IniReadValue("DATABASESETTING", "DataBaseName"), ini.IniReadValue("DATABASESETTING", "UserName"), ini.IniReadValue("DATABASESETTING", "Password"));
            }
            else
            {
                DB._activeDBConn = string.Format("data source={0};initial catalog={1};Min Pool Size=5;Max Pool Size=500;Integrated Security=SSPI;Trusted_Connection=True;persist security info=True;packet size=4096;Connect Timeout=60;", ini.IniReadValue("DATABASESETTING", "ServerName"), ini.IniReadValue("DATABASESETTING", "DataBaseName"));
            }

            try
            {
                using (IDataReader dr = DB.GetRS(string.Format(Db_Detials.sp_FetchCompSelection)))
                {
                    if (dr.Read())
                        Db_Detials.CompanyName = dr["CompanyName"].ToString();
                }
            }
            catch { }
            SqlConnected = true;
            AppStart.IsBool = 0;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ini.IniWriteValue("DATABASESETTING", "ServerName", CommonLogic.MungeString(txtSQLServerNM.Text));
                ini.IniWriteValue("DATABASESETTING", "LANServerName", CommonLogic.MungeString(txtSQLServerNM.Text));
                ini.IniWriteValue("DATABASESETTING", "DataBaseName ", CommonLogic.MungeString(txtDBName.Text));
                ini.IniWriteValue("DATABASESETTING", "UserName ", CommonLogic.MungeString(txtUserName.Text));
                ini.IniWriteValue("DATABASESETTING", "Password ", CommonLogic.MungeString(txtPassword.Text));

                if (rdbLocal.Checked == true)
                {
                    ini.IniWriteValue("DATABASESETTING", "ConnectType", CommonLogic.MungeString("LOCAL"));
                    ini.IniWriteValue("SERVERMACHINENAME", "MachineName", CommonLogic.SQuote("\"\""));
                }
                else if (rdbLAN.Checked == true)
                {
                    ini.IniWriteValue("DATABASESETTING", "ConnectType", CommonLogic.MungeString("NETWORK"));
                    ini.IniWriteValue("SERVERMACHINENAME", "MachineName", CommonLogic.MungeString(txtServerName.Text));
                }
                else if (rdbOnline.Checked == true)
                {
                    ini.IniWriteValue("DATABASESETTING", "ConnectType", CommonLogic.MungeString("ONLINE"));
                }
                this.Close();
                AppStart.IsBool = 0;
            }
            catch
            {
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            System.Environment.Exit(0);
        }

        private void rdbLocal_CheckedChanged(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void rdbLAN_CheckedChanged(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void rdbOnline_CheckedChanged(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void LoadSettings()
        {
            if (rdbLocal.Checked == true)
            {
                lblServerName.Visible = false;
                lblSQLServerName.Visible = true;
                lblDBName.Visible = true;
                lbl_UserName.Visible = true;
                lbl_Password.Visible = true;

                lblDOT1.Visible = false;
                lblDOT2.Visible = true;
                lblDOT3.Visible = true;
                lblDOT4.Visible = true;
                lblDOT5.Visible = true;

                txtServerName.Visible = false;
                txtSQLServerNM.Visible = true;
                txtDBName.Visible = true;
                txtUserName.Visible = true;
                txtPassword.Visible = true;

                txtServerName.Text = CommonLogic.UnmungeString(ini.IniReadValue("SERVERMACHINENAME", "MachineName"));
                txtSQLServerNM.Text = CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ServerName"));
                txtDBName.Text = CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "DataBaseName"));
                txtUserName.Text = CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "UserName"));
                txtPassword.Text = CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "Password"));
            }

            else if (rdbLAN.Checked == true)
            {
                lblServerName.Visible = true;
                lblSQLServerName.Visible = true;
                lblDBName.Visible = true;
                lbl_UserName.Visible = true;
                lbl_Password.Visible = true;

                lblDOT1.Visible = true;
                lblDOT2.Visible = true;
                lblDOT3.Visible = true;
                lblDOT4.Visible = true;
                lblDOT5.Visible = true;

                txtServerName.Visible = true;
                txtSQLServerNM.Visible = true;
                txtDBName.Visible = true;
                txtUserName.Visible = true;
                txtPassword.Visible = true;

                txtServerName.Text = CommonLogic.UnmungeString(ini.IniReadValue("SERVERMACHINENAME", "MachineName"));
                txtSQLServerNM.Text = CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ServerName"));
                txtDBName.Text = CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "DataBaseName"));
                txtUserName.Text = CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "UserName"));
                txtPassword.Text = CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "Password"));

            }
            else if (rdbOnline.Checked == true)
            {

                lblServerName.Visible = false;
                lblSQLServerName.Visible = false;
                lblDBName.Visible = false;
                lbl_UserName.Visible = false;
                lbl_Password.Visible = false;
                lblDOT1.Visible = false;
                lblDOT2.Visible = false;
                lblDOT3.Visible = false;
                lblDOT4.Visible = false;
                lblDOT5.Visible = false;

                txtServerName.Visible = false;
                txtSQLServerNM.Visible = false;
                txtDBName.Visible = false;
                txtUserName.Visible = false;
                txtPassword.Visible = false;

                txtSQLServerNM.Text = CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "LANServerName"));

            }
        }

        private void LoadPreviousSettings()
        {


            if (CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ConnectType")).ToUpper() == "LOCAL")
            {
                rdbLocal.Checked = true;

            }
            else if (CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ConnectType")).ToUpper() == "NETWORK")
            {
                rdbLAN.Checked = true;
            }

            else if (CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ConnectType")).ToUpper() == "ONLINE")
            {

                rdbOnline.Checked = true;
            }

            string s = CommonLogic.UnmungeString("7EZ/BPuxICNTq3FOU+1fhQ==");
            LoadSettings();
        }

    }

}
