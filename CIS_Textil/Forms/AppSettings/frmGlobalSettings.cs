using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CIS_DBLayer;
using CIS_Bussiness;

namespace CIS_Textil
{
    public partial class frmGlobalSettings : frmTrnsIface
    {
        string strQry = "";

        public frmGlobalSettings()
        {
            InitializeComponent();
        }

        private void frmGlobalSettings_Load(object sender, EventArgs e)
        {
            try
            {
                //LoadDBSettings();
                LoadFabricDeliveryChallanSettingVariables();
                LoadBeamSettingVariable();
                LoadYarnSettingVariables();
                InitDBBackUpVariables();
                LoadThemeSettingsVariables();
                LoadThemeSettingsVariables();
                LoadGeneralVariables();

                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtMailID.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtMailID.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtMailID.Focus();
                        this.isComboAddText = true;
                        this.isSecondMessage = true;
                    }
                }
                FillControls();

                if (cboBMEnableAgentCommissionCalcMethod1.Text == "TRUE")
                {
                    cboBMEnableAgentCommissionCalcMethod2.Enabled = false;
                }
                else
                {
                    cboBMEnableAgentCommissionCalcMethod2.Enabled = true;
                }
                if (cboBMEnableAgentCommissionCalcMethod2.Text == "TRUE")
                {
                    cboBMEnableAgentCommissionCalcMethod1.Enabled = false;
                }
                else
                {
                    cboBMEnableAgentCommissionCalcMethod1.Enabled = true;
                }
                rdbOnceaDay_CheckedChanged(null, null);
                rdbEnabled_CheckedChanged(null, null);
                tbMain.TabPages.Remove(tbStatutory);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void LoadDBSettings()
        {
            try
            {
                using (IDataReader iDr = DB.GetRS("SELECT  * from tbl_GlobalSettings WHERE GType='DBBACKUP' AND CompanyForID=" + Db_Detials.CompID + ""))
                {
                    while (iDr.Read())
                    {
                        if (iDr["GSName"].ToString().ToUpper() == "AUTOBACKUPENABLED")
                        {
                            if (Localization.ParseBoolean(iDr["GSValue"].ToString()) == true)
                            {
                                rdbEnabled.Checked = true;
                                pnlAutoBackUp.Visible = true;
                            }
                            else
                                pnlAutoBackUp.Visible = false;
                        }

                        if (iDr["GSName"].ToString().ToUpper() == "ISONCEADAY")
                        {
                            if (Localization.ParseBoolean(iDr["GSValue"].ToString()) == true)
                                rdbOnceaDay.Checked = true;
                        }

                        if (iDr["GSName"].ToString().ToUpper() == "SEPARATE_FILE")
                        {
                            if (Localization.ParseBoolean(iDr["GSValue"].ToString()) == true)
                                chkSeparateFile.Checked = true;
                        }

                        if (iDr["GSName"].ToString().ToUpper() == "ODTIME")
                        {
                            try
                            {
                                string[] sTime = iDr["GSValue"].ToString().Split(':');
                                cboHr.SelectedItem = sTime[0];
                                string[] sTime_AMPM = sTime[1].Split(' ');
                                cboMin.SelectedItem = sTime_AMPM[0];
                                cboAMPM.SelectedItem = sTime_AMPM[1];
                            }
                            catch { }
                        }

                        if (iDr["GSName"].ToString().ToUpper() == "ATVHR")
                        {
                            try
                            {
                                cboHr.SelectedValue = Localization.ParseNativeInt(iDr["GSValue"].ToString());
                            }
                            catch { }
                        }

                        if (iDr["GSName"].ToString().ToUpper() == "AUTOBACKUPPATH")
                        {
                            try
                            {
                                txtlocation.Text = iDr["GSValue"].ToString();
                            }
                            catch { }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void rdbEnabled_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbEnabled.Checked)
            {
                pnlAutoBackUp.Visible = true;
                CmdSavepath.Visible = true;
                txtlocation.Visible = true;
                lbllocation.Visible = true;
            }
            else
            {
                pnlAutoBackUp.Visible = false;
                CmdSavepath.Visible = false;
                txtlocation.Visible = false;
                lbllocation.Visible = false;
            }
        }

        private void rdbOnceaDay_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbOnceaDay.Checked)
            {
                lblTime.Text = "Exactly At: ";
                cboMin.Visible = true;
                cboAMPM.Visible = true;
            }
            else
            {
                lblTime.Text = "After Every (HH) : ";
                cboMin.Visible = false;
                cboAMPM.Visible = false;
            }
        }

        public void FillControls()
        {
            try
            {
                LoadFabricDeliveryChallanSettingVariables();
                LoadBeamSettingVariable();
                LoadYarnSettingVariables();
                LoadUserRights();
                LoadDBSettings();
                LoadBrokerModuleVaraibles();
                LoadBookModuleVaraibles();
                LoadAccountsModuleVaraibles();
                LoadSMSSettingsVariables();
                LoadEmailSettingsVariables();
                LoadSMSSettingsVariables();
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                return false;
            }
            catch { return false; }
        }

        public void MovetoField()
        {
            try
            {
                CommonCls.IncFieldID(this, ref txtCode, "");
                txtMailID.Text = "";
                txtPassword.Text = "";
                txtSignature.Text = "";
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
                //FabricSave();
                //BookSave();
                //AccountsSave();
                //YarnSave();
                //BeamSave();
                //UserRightsSave();
                //BackUpSave();
                //Broker_ModuleSave();
                //SaveEmailSettings();
                ThemeSave();
                //GeneralSave();
                //SaveSMSSettings();

                if (strQry.Length > 0)
                {
                    try
                    {
                        DB.ExecuteSQL(strQry);
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecuritySuccess, "Success", "Settings Updated Successfully...");
                        strQry = "";
                    }
                    catch (Exception ex)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
                        strQry = "";
                        Navigate.logError(ex.Message, ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void BackUpSave()
        {
            DataTable Dt = DB.GetDT("SELECT  * from tbl_GlobalSettings WHERE GType='DBBACKUP' AND CompanyForID=" + Db_Detials.CompID + "", false);

            string sStartTime = "";
            sStartTime = CommonLogic.SQuote(cboHr.SelectedItem + ":" + cboMin.SelectedItem + " " + cboAMPM.SelectedItem);

            DataRow[] rst_IsAutometic = Dt.Select("GSName='AutoBackupEnabled'");
            if (rst_IsAutometic.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, {2}, {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                    "'DBBACKUP'", "'AutoBackupEnabled'", (rdbEnabled.Checked ? "'True'" : "'False'"), "'Automatic Database BackUp'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue={2}, Description={3}, CompID={4}, YearId={5},AddedBy={6}, AddedOn=GETDATE() WHERE GType='DBBACKUP' AND GSName='AutoBackupEnabled';",
                               "'DBBACKUP'", "'AutoBackupEnabled'", (rdbEnabled.Checked ? "'True'" : "'False'"), "'Automatic Database BackUp'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }

            DataRow[] rst_IsOnceADay = Dt.Select("GSName='IsOnceADay'");
            if (rst_IsOnceADay.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, {2}, {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'DBBACKUP'", "'IsOnceADay'", (rdbOnceaDay.Checked ? "'True'" : "'False'"), "'IF BackUp Required once a day'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue={2}, Description={3}, CompID={4}, YearId={5},AddedBy={6}, AddedOn=GETDATE() WHERE GType='DBBACKUP' AND GSName='IsOnceADay';",
                               "'DBBACKUP'", "'IsOnceADay'", (rdbOnceaDay.Checked ? "'True'" : "'False'"), "'IF BackUp Required once a day'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }

            DataRow[] rst_ODTime = Dt.Select("GSName='ODTime'");
            if (rst_ODTime.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, {2}, {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'DBBACKUP'", "'ODTime'", CommonLogic.SQuote(sStartTime), "'Time when BackUp will happen if Once A Day'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue={2}, Description={3}, CompID={4}, YearId={5},AddedBy={6}, AddedOn=GETDATE() WHERE GType='DBBACKUP' AND GSName='ODTime';",
                               "'DBBACKUP'", "'ODTime'", sStartTime, "'Time when BackUp will happen if Once A Day'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }

            DataRow[] rst_ATVHR = Dt.Select("GSName='ATVHR'");
            if (rst_ATVHR.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'DBBACKUP'", "'ATVHR'", cboHr.Text, "'DB BackUp interval in Hour'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6}, AddedOn=GETDATE() WHERE GType='DBBACKUP' AND GSName='ATVHR';",
                               "'DBBACKUP'", "'ATVHR'", cboHr.Text, "'DB BackUp interval in Hour'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }

            DataRow[] rst_SEPARATE_FILE = Dt.Select("GSName='SEPARATE_FILE'");
            if (rst_SEPARATE_FILE.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, {2}, {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                    "'DBBACKUP'", "'SEPARATE_FILE'", (chkSeparateFile.Checked ? "'True'" : "'False'"), "'Whether to make separate file each time auto Back Up Runs..'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue={2}, Description={3}, CompID={4}, YearId={5},AddedBy={6}, AddedOn=GETDATE() WHERE GType='DBBACKUP' AND GSName='SEPARATE_FILE';",
                               "'DBBACKUP'", "'SEPARATE_FILE'", (chkSeparateFile.Checked ? "'True'" : "'False'"), "'Whether to make separate file each time auto Back Up Runs..'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }

            DataRow[] rst_AUTOBACKUPPATH = Dt.Select("GSName='AUTOBACKUPPATH'");
            string stxtlocation = string.Empty;
            if (rdbDisable.Checked)
            {
                stxtlocation = "-";
            }
            else
            {
                stxtlocation = txtlocation.Text;
            }
            if (rst_AUTOBACKUPPATH.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'DBBACKUP'", "'AUTOBACKUPPATH'", stxtlocation, "'Automatic Database Backup Path'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='DBBACKUP' AND GSName='AUTOBACKUPPATH'  and CompanyForID={7};",
                              "'DBBACKUP'", "'AUTOBACKUPPATH'", stxtlocation, "'Automatic Database Backup Path'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }
        }

        private void InitDBBackUpVariables()
        {
            using (IDataReader iDr = DB.GetRS("SELECT  * from tbl_GlobalSettings WHERE GType='DBBACKUP' AND CompanyForID=" + Db_Detials.CompID + ""))
            {
                while (iDr.Read())
                {
                    if (iDr["GSName"].ToString().ToUpper() == "AUTOBACKUPENABLED")
                    {
                        if (Localization.ParseBoolean(iDr["GSValue"].ToString()) == true)
                        {
                            Db_Detials.IsTypeAutometic = true;
                        }
                        else
                            Db_Detials.IsTypeAutometic = false;
                    }

                    if (iDr["GSName"].ToString().ToUpper() == "ISONCEADAY")
                    {
                        if (Localization.ParseBoolean(iDr["GSValue"].ToString()) == true)
                            Db_Detials.IsOnceADay = true;
                        else
                            Db_Detials.IsOnceADay = false;
                    }

                    if (iDr["GSName"].ToString().ToUpper() == "SEPARATE_FILE")
                    {
                        if (Localization.ParseBoolean(iDr["GSValue"].ToString()) == true)
                            Db_Detials.IsAlwaysNewFile = true;
                        else
                            Db_Detials.IsAlwaysNewFile = false;
                    }

                    if (iDr["GSName"].ToString().ToUpper() == "LAST_BACKUP_FILE_NAME")
                    {
                        Db_Detials.sLastFileNM = iDr["GSValue"].ToString();
                    }

                    if (iDr["GSName"].ToString().ToUpper() == "ODTIME")
                    {
                        try { Db_Detials.dtBackUpTime = Localization.ParseNativeDateTime(iDr["GSValue"].ToString()); }
                        catch { }
                    }

                    if (iDr["GSName"].ToString().ToUpper() == "ATVHR")
                    {
                        try
                        {
                            Db_Detials.iTimeInterval = Localization.ParseNativeInt(iDr["GSValue"].ToString());
                        }
                        catch { }
                    }

                    if (iDr["GSName"].ToString().ToUpper() == "AUTOBACKUPPATH")
                    {
                        try
                        {
                            Db_Detials.sAutometicBkpPath = iDr["GSValue"].ToString();
                        }
                        catch { }
                    }
                }
            }
        }

        private void SaveEmailSettings()
        {
            DataTable Dt = DB.GetDT("SELECT  * from tbl_GlobalSettings WHERE GType='EMAIL' AND CompanyForID=" + Db_Detials.CompID + "", false);
            string sStartTime = "";
            sStartTime = CommonLogic.SQuote(cboHr.SelectedItem + ":" + cboMin.SelectedItem + " " + cboAMPM.SelectedItem);

            DataRow[] rst_EMAIL_ADDRESS = Dt.Select("GSName='EMAIL_ADDRESS'");
            if (rst_EMAIL_ADDRESS.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, {2}, {3}, {4}, {5},GETDATE(),{6},0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL);",
                    "'EMAIL'", "'EMAIL_ADDRESS'", CommonLogic.SQuote(CommonLogic.MungeString(txtMailID.Text.Trim())), "'Email Address For Mail Configuration'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue={2}, Description={3}, CompID={4}, YearId={5}, UpdatedBy={6}, UpdatedOn=GETDATE(),IsModified=1 WHERE GType='EMAIL' AND GSName='EMAIL_ADDRESS';",
                               "'EMAIL'", "'EMAIL_ADDRESS'", CommonLogic.SQuote(CommonLogic.MungeString(txtMailID.Text.Trim())), "'Email Address For Mail Configuration'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }

            DataRow[] rst_EMAIL_PASSWORD = Dt.Select("GSName='EMAIL_PASSWORD'");
            if (rst_EMAIL_PASSWORD.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, {2}, {3}, {4}, {5},{6}, GETDATE(), {7},0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL);",
                           "'EMAIL'", "'EMAIL_PASSWORD'", CommonLogic.SQuote(CommonLogic.MungeString(txtMailPwd.Text.Trim())), "'Email Password For Mail Configuration'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue={2}, Description={3}, CompID={4}, YearId={5}, UpdatedBy={6}, UpdatedOn=GETDATE(),IsModified=1 WHERE GType='EMAIL' AND GSName='EMAIL_PASSWORD';",
                               "'EMAIL'", "'EMAIL_PASSWORD'", CommonLogic.SQuote(CommonLogic.MungeString(txtMailPwd.Text.Trim())), "'Email Password For Mail Configuration'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }

            DataRow[] rst_EMAIL_HOST = Dt.Select("GSName='EMAIL_HOST'");
            if (rst_EMAIL_HOST.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, {2}, {3}, {4}, {5},{6}, GETDATE(), {7},0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL);",
                           "'EMAIL'", "'EMAIL_HOST'", CommonLogic.SQuote(txtHost.Text), "'Email Host Name For Mail Configuration'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue={2}, Description={3}, CompID={4}, YearId={5}, UpdatedBy={6}, UpdatedOn=GETDATE(),IsModified=1 WHERE GType='EMAIL' AND GSName='EMAIL_HOST';",
                               "'EMAIL'", "'EMAIL_HOST'", CommonLogic.SQuote(txtHost.Text), "'Email Host Name For Mail Configuration'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }

            DataRow[] rst_EMAIL_PORT = Dt.Select("GSName='EMAIL_PORT'");
            if (rst_EMAIL_PORT.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, {2}, {3}, {4}, {5},{6}, GETDATE(), {7},0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL);",
                           "'EMAIL'", "'EMAIL_PORT'", CommonLogic.SQuote(txtPortNo.Text), "'Email Port Number For Mail Configuration'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue={2}, Description={3}, CompID={4}, YearId={5}, UpdatedBy={6}, UpdatedOn=GETDATE(),IsModified=1 WHERE GType='EMAIL' AND GSName='EMAIL_PORT';",
                               "'EMAIL'", "'EMAIL_PORT'", CommonLogic.SQuote(txtPortNo.Text), "'Email Port Number For Mail Configuration'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }

            DataRow[] rst_EMAIL_SIGNATURE = Dt.Select("GSName='EMAIL_SIGNATURE'");
            if (rst_EMAIL_SIGNATURE.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, {2}, {3}, {4}, {5},{6}, GETDATE(), {7},0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL);",
                           "'EMAIL'", "'EMAIL_SIGNATURE'", CommonLogic.SQuote(txtSignature.Text), "'Email Signature For Mail Configuration'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue={2}, Description={3}, CompID={4}, YearId={5}, UpdatedBy={6}, UpdatedOn=GETDATE(),IsModified=1 WHERE GType='EMAIL' AND GSName='EMAIL_SIGNATURE';",
                               "'EMAIL'", "'EMAIL_SIGNATURE'", CommonLogic.SQuote(txtSignature.Text), "'Email Signature For Mail Configuration'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }

            DataRow[] rst_MAIL_TYPE = Dt.Select("GSName='MAIL_TYPE'");
            if (rst_MAIL_TYPE.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'EMAIL'", "'MAIL_TYPE'", cboGmail.Text == "" ? "FALSE" : cboGmail.Text, "'Set true If Want To Send Mail From Gmail Account rather than any other Email Provider'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6}, IsModified=1,AddedOn=GETDATE() WHERE GType='EMAIL' AND GSName='MAIL_TYPE' and CompanyForID={7};",
                               "'EMAIL'", "'MAIL_TYPE'", cboGmail.Text == "" ? "FALSE" : cboGmail.Text, "'Set true If Want To Send Mail From Gmail Account rather than any other Email Provider'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }
        }

        private void SaveSMSSettings()
        {
            DataTable Dt = DB.GetDT("SELECT  * from tbl_GlobalSettings WHERE GType='SMS' AND CompanyForID=" + Db_Detials.CompID + "", false);
            string sStartTime = "";
            sStartTime = CommonLogic.SQuote(cboHr.SelectedItem + ":" + cboMin.SelectedItem + " " + cboAMPM.SelectedItem);

            DataRow[] rst_SMS_UserName = Dt.Select("GSName='SMS_UserName'");
            if (rst_SMS_UserName.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, {2}, {3}, {4}, {5},GETDATE(),{6},0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL);",
                    "'SMS'", "'SMS_UserName'", CommonLogic.SQuote(CommonLogic.MungeString(txtUserNM.Text.Trim())), "'User Name of SMS Pack'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue={2}, Description={3}, CompID={4}, YearId={5}, UpdatedBy={6}, UpdatedOn=GETDATE(),IsModified=1 WHERE GType='SMS' AND GSName='SMS_UserName';",
                               "'SMS'", "'SMS_UserName'", CommonLogic.SQuote(CommonLogic.MungeString(txtUserNM.Text.Trim())), "'User Name of SMS Pack'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }

            DataRow[] rst_SMS_Password = Dt.Select("GSName='SMS_Password'");
            if (rst_SMS_Password.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, {2}, {3}, {4}, {5},{6}, GETDATE(), {7},0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL);",
                           "'SMS'", "'SMS_Password'", CommonLogic.SQuote(CommonLogic.MungeString(txtPassword.Text.Trim())), "'Password of SMS Pack'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue={2}, Description={3}, CompID={4}, YearId={5}, UpdatedBy={6}, UpdatedOn=GETDATE(),IsModified=1 WHERE GType='SMS' AND GSName='SMS_Password';",
                               "'SMS'", "'SMS_Password'", CommonLogic.SQuote(CommonLogic.MungeString(txtPassword.Text.Trim())), "'Password of SMS Pack'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }

            DataRow[] rst_SMS_SendSMS = Dt.Select("GSName='SMS_SendSMS'");
            if (rst_SMS_SendSMS.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, {2}, {3}, {4}, {5},{6}, GETDATE(), {7},0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL);",
                           "'SMS'", "'SMS_SendSMS'", CommonLogic.SQuote(txtSendSMS.Text), "'Send the message as per detail of sending'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue={2}, Description={3}, CompID={4}, YearId={5}, UpdatedBy={6}, UpdatedOn=GETDATE(),IsModified=1 WHERE GType='SMS' AND GSName='SMS_SendSMS';",
                               "'SMS'", "'SMS_SendSMS'", CommonLogic.SQuote(txtSendSMS.Text), "'Send the message as per detail of sending'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }

            DataRow[] rst_SMS_Balance = Dt.Select("GSName='SMS_Balance'");
            if (rst_SMS_Balance.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, {2}, {3}, {4}, {5},{6}, GETDATE(), {7},0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL);",
                           "'SMS'", "'SMS_Balance'", CommonLogic.SQuote(txtSMSBal.Text), "'Get Balance sms and Date of validity in of the pack'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue={2}, Description={3}, CompID={4}, YearId={5}, UpdatedBy={6}, UpdatedOn=GETDATE(),IsModified=1 WHERE GType='SMS' AND GSName='SMS_Balance';",
                               "'SMS'", "'SMS_Balance'", CommonLogic.SQuote(txtSMSBal.Text), "'Get Balance sms and Date of validity in of the pack'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }

        }

        private void FabricSave()
        {
            DataTable Dt = DB.GetDT("SELECT * from tbl_GlobalSettings WHERE GType like 'FABRIC%' AND CompanyForID=" + Db_Detials.CompID + "", false);

            DataRow[] rst_FabricDesign = Dt.Select("GSName='FD_ON'");
            if (rst_FabricDesign.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                    "'FABRIC'", "'FD_ON'", cboFDesignOrderNo.Text == "" ? "FALSE" : cboFDesignOrderNo.Text, "'Allow FabDesOrderNo in Fabric Design Master'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FD_ON'  and CompanyForID={7};",
                               "'FABRIC'", "'FD_ON'", cboFDesignOrderNo.Text == "" ? "FALSE" : cboFDesignOrderNo.Text, "'Allow FabDesOrderNo in Fabric Design Master'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FABSO_APRVD = Dt.Select("GSName='FABSO_APRVD'");
            if (rst_FABSO_APRVD.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FABSO_APRVD'", cboFOrderStatus.Text == "" ? "FALSE" : cboFOrderStatus.Text, "'Allow FabDesOrderNo in Fabric Design Master'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FABSO_APRVD'  and CompanyForID={7};",
                               "'FABRIC'", "'FABSO_APRVD'", cboFOrderStatus.Text == "" ? "FALSE" : cboFOrderStatus.Text, "'Allow FabDesOrderNo in Fabric Design Master'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FSO = Dt.Select("GSName='FSO'");
            if (rst_FSO.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FSO'", cboFSOInternalOrder.Text == "" ? "FALSE" : cboFSOInternalOrder.Text, "'Allow InternalSONo in Fabric Sales Order transaction'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FSO'  and CompanyForID={7};",
                               "'FABRIC'", "'FSO'", cboFSOInternalOrder.Text == "" ? "FALSE" : cboFSOInternalOrder.Text, "'Allow InternalSONo in Fabric Sales Order transaction'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FDC = Dt.Select("GSName='FDC'");
            if (rst_FDC.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FDC'", cboFPiecewiseEntry.Text == "" ? "FALSE" : cboFPiecewiseEntry.Text, "'Allow Piecewise Entry in Fabric Delivery Challan'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FDC'  and CompanyForID={7};",
                               "'FABRIC'", "'FDC'", cboFPiecewiseEntry.Text == "" ? "FALSE" : cboFPiecewiseEntry.Text, "'Allow Piecewise Entry in Fabric Delivery Challan'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_BR_Scan_Chln = Dt.Select("GSName='BR_Scan_Chln'");
            if (rst_BR_Scan_Chln.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'BR_Scan_Chln'", cboFBarcode.Text == "" ? "FALSE" : cboFBarcode.Text, "'Barcode Scaning in Delivery Challan'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='BR_Scan_Chln'  and CompanyForID={7};",
                               "'FABRIC'", "'BR_Scan_Chln'", cboFBarcode.Text == "" ? "FALSE" : cboFBarcode.Text, "'Barcode Scaning in Delivery Challan'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_MTY_DC = Dt.Select("GSName='MTY_DC'");
            if (rst_MTY_DC.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'MTY_DC'", cboFMultiSeries.Text == "" ? "FALSE" : cboFMultiSeries.Text, "'Multy Series in Delivery Challan Entry'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6}, IsModified=1,AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='MTY_DC'  and CompanyForID={7};",
                               "'FABRIC'", "'MTY_DC'", cboFMultiSeries.Text == "" ? "FALSE" : cboFMultiSeries.Text, "'Multy Series in Delivery Challan Entry'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FR_N_PNo = Dt.Select("GSName='FR_N_PNo'");
            if (rst_FR_N_PNo.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FR_N_PNo'", cboFNewPieceNo.Text == "" ? "FALSE" : cboFNewPieceNo.Text, "'New Piece No in Fabric Receipt'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FR_N_PNo'  and CompanyForID={7};",
                               "'FABRIC'", "'FR_N_PNo'", cboFNewPieceNo.Text == "" ? "FALSE" : cboFNewPieceNo.Text, "'New Piece No in Fabric Receipt'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FP_WC_LM = Dt.Select("GSName='FP_WC_LM'");
            if (rst_FP_WC_LM.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FP_WC_LM'", cboFLoomWise.Text == "" ? "FALSE" : cboFLoomWise.Text, "'Allow Loom Wise Fabric Production With Consumption'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FP_WC_LM' and CompanyForID={7};",
                               "'FABRIC'", "'FP_WC_LM'", cboFLoomWise.Text == "" ? "FALSE" : cboFLoomWise.Text, "'Allow Loom Wise Fabric Production With Consumption'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FR_PCSNO_ETRYNo = Dt.Select("GSName='FR_PCSNO_ETRYNo'");
            if (rst_FR_PCSNO_ETRYNo.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FR_PCSNO_ETRYNo'", cboFPieceWiseReciept.Text == "" ? "FALSE" : cboFPieceWiseReciept.Text, "'In Fabric Receipt Entry Piece No on based on Entry No(For Jai Bhavani)'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FR_PCSNO_ETRYNo' and CompanyForID={7};",
                               "'FABRIC'", "'FR_PCSNO_ETRYNo'", cboFPieceWiseReciept.Text == "" ? "FALSE" : cboFPieceWiseReciept.Text, "'In Fabric Receipt Entry Piece No on based on Entry No(For Jai Bhavani)'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }


            DataRow[] rst_FIN_CPY_FIS = Dt.Select("GSName='FIN_CPY_FIS'");
            if (rst_FIN_CPY_FIS.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FIN_CPY_FIS'", cboFEntryAutoCopy.Text == "" ? "FALSE" : cboFEntryAutoCopy.Text, "'Fabric Inward Entry Auto Copy to Fabric Issue Entry'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FIN_CPY_FIS' and CompanyForID={7};",
                               "'FABRIC'", "'FIN_CPY_FIS'", cboFEntryAutoCopy.Text == "" ? "FALSE" : cboFEntryAutoCopy.Text, "'Fabric Inward Entry Auto Copy to Fabric Issue Entry'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FDC_ORD_WISE = Dt.Select("GSName='FDC_ORD_WISE'");
            if (rst_FDC_ORD_WISE.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FDC_ORD_WISE'", cboFOrderMandatory.Text == "" ? "FALSE" : cboFOrderMandatory.Text, "'Order Manditory In Fabic Delivery Challan Entry'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FDC_ORD_WISE' and CompanyForID={7};",
                               "'FABRIC'", "'FDC_ORD_WISE'", cboFOrderMandatory.Text == "" ? "FALSE" : cboFOrderMandatory.Text, "'Order Manditory In Fabic Delivery Challan Entry'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FP_MLT_CHLN = Dt.Select("GSName='FP_MLT_CHLN'");
            if (rst_FP_MLT_CHLN.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FP_MLT_CHLN'", cboFMultiChallanBill.Text == "" ? "FALSE" : cboFMultiChallanBill.Text, "'Multy Challan Billing in Fabric Purchase Entry'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FP_MLT_CHLN' and CompanyForID={7};",
                    "'FABRIC'", "'FP_MLT_CHLN'", cboFMultiChallanBill.Text == "" ? "FALSE" : cboFMultiChallanBill.Text, "'Multy Challan Billing in Fabric Purchase Entry'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_RTN_FM_STK = Dt.Select("GSName='RTN_FM_STK'");
            if (rst_RTN_FM_STK.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'RTN_FM_STK'", cboFSalesReturn.Text == "" ? "FALSE" : cboFSalesReturn.Text, "'Allows Fabric Sales Returns and Fabric Purchase Retrurns From Stock '", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='RTN_FM_STK' and CompanyForID={7};",
                               "'FABRIC'", "'RTN_FM_STK'", cboFSalesReturn.Text == "" ? "FALSE" : cboFSalesReturn.Text, "'Allows Fabric Sales Returns and Fabric Purchase Retrurns From Stock '", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FDC_RATETYPE = Dt.Select("GSName='FDC_RATETYPE'");
            if (rst_FDC_RATETYPE.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FDC_RATETYPE'", cboFRateType.Text == "" ? "FALSE" : cboFRateType.Text, "'Allows Enable Radio Buttons to Select Gross Rate Or Net Rate In Fabric delivery Challan Entry For JBF'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FDC_RATETYPE' and CompanyForID={7};",
                               "'FABRIC'", "'FDC_RATETYPE'", cboFRateType.Text == "" ? "FALSE" : cboFRateType.Text, "'Allows Enable Radio Buttons to Select Gross Rate Or Net Rate In Fabric delivery Challan Entry For JBF'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_SUB_ORDER = Dt.Select("GSName='SUB_ORDER'");
            if (rst_SUB_ORDER.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'SUB_ORDER'", cboFSubOrder.Text == "" ? "FALSE" : cboFSubOrder.Text, "'Allow Sub Order In Fabric Delivery Challan'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='SUB_ORDER' and CompanyForID={7};",
                               "'FABRIC'", "'SUB_ORDER'", cboFSubOrder.Text == "" ? "FALSE" : cboFSubOrder.Text, "'Allow Sub Order In Fabric Delivery Challan'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FDC_BRK_COM = Dt.Select("GSName='FDC_BRK_COM'");
            if (rst_FDC_BRK_COM.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FDC_BRK_COM'", cboFBrokerMandatory.Text == "" ? "FALSE" : cboFBrokerMandatory.Text, "'Broker Maditory int Deliver Challan Entry'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FDC_BRK_COM' and CompanyForID={7};",
                               "'FABRIC'", "'FDC_BRK_COM'", cboFBrokerMandatory.Text == "" ? "FALSE" : cboFBrokerMandatory.Text, "'Broker Maditory int Deliver Challan Entry'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FS_BRK_COM = Dt.Select("GSName='FS_BRK_COM'");
            if (rst_FS_BRK_COM.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FS_BRK_COM'", cboFIBrokerMandatory.Text == "" ? "FALSE" : cboFIBrokerMandatory.Text, "'Allow Sub Order In Fabric Delivery Challan'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FS_BRK_COM' and CompanyForID={7};",
                               "'FABRIC'", "'FS_BRK_COM'", cboFIBrokerMandatory.Text == "" ? "FALSE" : cboFIBrokerMandatory.Text, "'Allow Sub Order In Fabric Delivery Challan'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FDC_ORD_COMP = Dt.Select("GSName='FDC_ORD_COMP'");
            if (rst_FDC_ORD_COMP.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FDC_ORD_COMP'", cboFOrderMandatory.Text == "" ? "FALSE" : cboFOrderMandatory.Text, "'Order Maditory on Delivery Challan Entry'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FDC_ORD_COMP' and CompanyForID={7};",
                               "'FABRIC'", "'FDC_ORD_COMP'", cboFOrderMandatory.Text == "" ? "FALSE" : cboFOrderMandatory.Text, "'Order Maditory on Delivery Challan Entry'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FSO_RATETYPE = Dt.Select("GSName='FSO_RATETYPE'");
            if (rst_SUB_ORDER.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FSO_RATETYPE'", cboFSORateType.Text == "" ? "FALSE" : cboFSORateType.Text, "'Allows Enable Gross Rate Or Net Rate In Fabric Salse Order Entry For JBF'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FSO_RATETYPE' and CompanyForID={7};",
                               "'FABRIC'", "'FSO_RATETYPE'", cboFSORateType.Text == "" ? "FALSE" : cboFSORateType.Text, "'Allows Enable Gross Rate Or Net Rate In Fabric Salse Order Entry For JBF'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_INVOICE_IN_DC = Dt.Select("GSName='INVOICE_IN_DC'");
            if (rst_INVOICE_IN_DC.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'INVOICE_IN_DC'", cboFGenerateInvoice.Text == "" ? "FALSE" : cboFGenerateInvoice.Text, "'Generates Invoice from Delivery challan'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='INVOICE_IN_DC' and CompanyForID={7};",
                               "'FABRIC'", "'INVOICE_IN_DC'", cboFGenerateInvoice.Text == "" ? "FALSE" : cboFGenerateInvoice.Text, "'Generates Invoice from Delivery challan'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FSI_DSGN_WS = Dt.Select("GSName='FSI_DSGN_WS'");
            if (rst_FSI_DSGN_WS.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FSI_DSGN_WS'", cboFShowDesign.Text == "" ? "FALSE" : cboFShowDesign.Text, "'Shows Design Column in Grid'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FSI_DSGN_WS' and CompanyForID={7};",
                               "'FABRIC'", "'FSI_DSGN_WS'", cboFShowDesign.Text == "" ? "FALSE" : cboFShowDesign.Text, "'Shows Design Column in Grid'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_DEFECT_MTR_SAVE = Dt.Select("GSName='DEFECT_MTR_SAVE'");
            if (rst_FSI_DSGN_WS.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'DEFECT_MTR_SAVE'", CboFEDefectMtrs.Text == "" ? "FALSE" : CboFEDefectMtrs.Text, "'TO SAVE DEFACT METERS'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='DEFECT_MTR_SAVE' and CompanyForID={7};",
                               "'FABRIC'", "'DEFECT_MTR_SAVE'", CboFEDefectMtrs.Text == "" ? "FALSE" : CboFEDefectMtrs.Text, "'TO SAVE DEFACT METERS'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_DSGN_CHECK = Dt.Select("GSName='DSGN_CHECK'");
            if (rst_FSI_DSGN_WS.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'DSGN_CHECK'", cboFShowMessage.Text == "" ? "FALSE" : cboFShowMessage.Text, "'Show msg on worng selection of Design at Production'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='DSGN_CHECK' and CompanyForID={7};",
                               "'FABRIC'", "'DSGN_CHECK'", cboFShowMessage.Text == "" ? "FALSE" : cboFShowMessage.Text, "'Show msg on worng selection of Design at Production'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EBR_Scan_Chln = Dt.Select("GSName='EBR_Scan_Chln'");
            if (rst_FSI_DSGN_WS.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'EBR_Scan_Chln'", cboFEBarcode.Text == "" ? "FALSE" : cboFEBarcode.Text, "'Barcode Scanning in Delivery Challan'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='EBR_Scan_Chln' and CompanyForID={7};",
                               "'FABRIC'", "'EBR_Scan_Chln'", cboFEBarcode.Text == "" ? "FALSE" : cboFEBarcode.Text, "'Barcode Scanning in Delivery Challan'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EFDC_BRK_COM = Dt.Select("GSName='EFDC_BRK_COM'");
            if (rst_EFDC_BRK_COM.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'EFDC_BRK_COM'", CboFEBrokerMandatory.Text == "" ? "FALSE" : CboFEBrokerMandatory.Text, "'Barcode Scanning in Delivery Challan'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='EFDC_BRK_COM' and CompanyForID={7};",
                               "'FABRIC'", "'EFDC_BRK_COM'", CboFEBrokerMandatory.Text == "" ? "FALSE" : CboFEBrokerMandatory.Text, "'Barcode Scanning in Delivery Challan'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EFDC_ORD_COMP = Dt.Select("GSName='EFDC_ORD_COMP'");
            if (rst_EFDC_ORD_COMP.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'EFDC_ORD_COMP'", cboFEOrdermandatoryComp.Text == "" ? "FALSE" : cboFEOrdermandatoryComp.Text, "'Order Mandatory on Embroidary Delivery Challan Entry'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='EFDC_ORD_COMP' and CompanyForID={7};",
                               "'FABRIC'", "'EFDC_ORD_COMP'", cboFEOrdermandatoryComp.Text == "" ? "FALSE" : cboFEOrdermandatoryComp.Text, "'Order Mandatory on Embroidary Delivery Challan Entry'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EFDC_ORD_WISE = Dt.Select("GSName='EFDC_ORD_WISE'");
            if (rst_EFDC_ORD_WISE.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'EFDC_ORD_WISE'", cboFEOrderMendatoryWise.Text == "" ? "FALSE" : cboFEOrderMendatoryWise.Text, "'Order Mandatory Wise on Embroidary Delivery Challan Entry'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='EFDC_ORD_WISE' and CompanyForID={7};",
                               "'FABRIC'", "'EFDC_ORD_WISE'", cboFEOrderMendatoryWise.Text == "" ? "FALSE" : cboFEOrderMendatoryWise.Text, "'Order Mandatory Wise on Embroidary Delivery Challan Entry'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EFDC_RATETYPE = Dt.Select("GSName='EFDC_RATETYPE'");
            if (rst_EFDC_RATETYPE.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'EFDC_RATETYPE'", cboFEnableRadio.Text == "" ? "FALSE" : cboFEnableRadio.Text, "'Allows Enable RadioButtons to Select Gross Rate Or Net Rate'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='EFDC_RATETYPE' and CompanyForID={7};",
                               "'FABRIC'", "'EFDC_RATETYPE'", cboFEnableRadio.Text == "" ? "FALSE" : cboFEnableRadio.Text, "'Allows Enable RadioButtons to Select Gross Rate Or Net Rate'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EOVERDUE_ALT = Dt.Select("GSName='EOVERDUE_ALT'");
            if (rst_EOVERDUE_ALT.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'EOVERDUE_ALT'", cboFAlertMsgInEmbroidary.Text == "" ? "FALSE" : cboFAlertMsgInEmbroidary.Text, "'Give Alert Message on Credit Period Completion of Pending Bills'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='EOVERDUE_ALT' and CompanyForID={7};",
                               "'FABRIC'", "'EOVERDUE_ALT'", cboFAlertMsgInEmbroidary.Text == "" ? "FALSE" : cboFAlertMsgInEmbroidary.Text, "'Give Alert Message on Credit Period Completion of Pending Bills'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_ESUB_ORDER = Dt.Select("GSName='ESUB_ORDER'");
            if (rst_ESUB_ORDER.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'ESUB_ORDER'", cboFEnableSubOrder.Text == "" ? "FALSE" : cboFEnableSubOrder.Text, "'Allow Sub Order in Fabric Delivery Challan'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='ESUB_ORDER' and CompanyForID={7};",
                               "'FABRIC'", "'ESUB_ORDER'", cboFEnableSubOrder.Text == "" ? "FALSE" : cboFEnableSubOrder.Text, "'Allow Sub Order in Fabric Delivery Challan'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FDC_FABRICDELIVERYCHALLAN = Dt.Select("GSName='FDC_FABRICDELIVERYCHALLAN'");
            if (rst_FDC_FABRICDELIVERYCHALLAN.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FDC_FABRICDELIVERYCHALLAN'", CboFEnableProgramNo.Text == "" ? "FALSE" : CboFEnableProgramNo.Text, "'Allow FabricDelivery Challan program no'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FDC_FABRICDELIVERYCHALLAN' and CompanyForID={7};",
                               "'FABRIC'", "'FDC_FABRICDELIVERYCHALLAN'", CboFEnableProgramNo.Text == "" ? "FALSE" : CboFEnableProgramNo.Text, "'Allow FabricDelivery Challan program no'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FAB_DELDG_IN_EH = Dt.Select("GSName='FAB_DELDG_IN_EH'");
            if (rst_FAB_DELDG_IN_EH.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FAB_DELDG_IN_EH'", txtFAB_DELDG_IN_EH.Text, "'Enter The MenuID to Stop Delete Validation From Fabric Stock Table'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FAB_DELDG_IN_EH'  and CompanyForID={7};",
                               "'FABRIC'", "'FAB_DELDG_IN_EH'", txtFAB_DELDG_IN_EH.Text, "'Enter The MenuID to Stop Delete Validation From Fabric Stock Table'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_STRT_PNO = Dt.Select("GSName='STRT_PNO'");
            if (rst_STRT_PNO.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                    "'FABRIC'", "'STRT_PNO'", txtStartPNo.Text == "" ? "0000001" : txtStartPNo.Text, "'Enter The PieceNo to be started in Fabric Inward'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='STRT_PNO'  and CompanyForID={7};",
                               "'FABRIC'", "'STRT_PNO'", txtStartPNo.Text == "" ? "0000001" : txtStartPNo.Text, "'Enter The PieceNo to be started in Fabric Inward'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }


            #region Email
            DataRow[] rst_EMAIL_SEND_BookInv = Dt.Select("GSName='EMAIL_SEND_BookInv'");
            if (rst_EMAIL_SEND_BookInv.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'EMAIL_SEND_BookInv'", CboEmailBookInv.Text == "" ? "FALSE" : CboEmailBookInv.Text, "'Send Email on Saving In Book Invoice'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='EMAIL_SEND_BookInv' and CompanyForID={7};",
                               "'FABRIC'", "'EMAIL_SEND_BookInv'", CboEmailBookInv.Text == "" ? "FALSE" : CboEmailBookInv.Text, "'Send Email on Saving In Book Invoice'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EMAIL_SEND_BookSO = Dt.Select("GSName='EMAIL_SEND_BookSO'");
            if (rst_EMAIL_SEND_BookSO.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'EMAIL_SEND_BookSO'", CboEmailBookSO.Text == "" ? "FALSE" : CboEmailBookSO.Text, "'Send Email in Book Sales Order'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='EMAIL_SEND_BookSO' and CompanyForID={7};",
                               "'FABRIC'", "'EMAIL_SEND_BookSO'", CboEmailBookSO.Text == "" ? "FALSE" : CboEmailBookSO.Text, "'Send Email in Book Sales Order'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EMAIL_SEND_DC = Dt.Select("GSName='EMAIL_SEND_DC'");
            if (rst_EMAIL_SEND_DC.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'EMAIL_SEND_DC'", CboEmailDC.Text == "" ? "FALSE" : CboEmailDC.Text, "'Send Email To On Saving Of Record in Delivery Challan'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='EMAIL_SEND_DC' and CompanyForID={7};",
                               "'FABRIC'", "'EMAIL_SEND_DC'", CboEmailDC.Text == "" ? "FALSE" : CboEmailDC.Text, "'Send Email To On Saving Of Record in Delivery Challan'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EMAIL_SEND_DC_Serail = Dt.Select("GSName='EMAIL_SEND_DC_Serail'");
            if (rst_FDC_FABRICDELIVERYCHALLAN.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'EMAIL_SEND_DC_Serail'", CboEmailDC_Serial.Text == "" ? "FALSE" : CboEmailDC_Serial.Text, "'Send Email in Saving in Fabric Delivery Challan Serial'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='EMAIL_SEND_DC_Serail' and CompanyForID={7};",
                               "'FABRIC'", "'EMAIL_SEND_DC_Serail'", CboEmailDC_Serial.Text == "" ? "FALSE" : CboEmailDC_Serial.Text, "'Send Email in Saving in Fabric Delivery Challan Serial'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EMAIL_SEND_FabInv = Dt.Select("GSName='EMAIL_SEND_FabInv'");
            if (rst_EMAIL_SEND_FabInv.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'EMAIL_SEND_FabInv'", CboEmailFabInv.Text == "" ? "FALSE" : CboEmailFabInv.Text, "'Send Email On Saving In Teh Fabric Invoice'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='EMAIL_SEND_FabInv' and CompanyForID={7};",
                               "'FABRIC'", "'EMAIL_SEND_FabInv'", CboEmailFabInv.Text == "" ? "FALSE" : CboEmailFabInv.Text, "'Send Email On Saving In Teh Fabric Invoice'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EMAIL_SEND_FabInv_Cut = Dt.Select("GSName='EMAIL_SEND_FabInv_Cut'");
            if (rst_EMAIL_SEND_FabInv_Cut.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'EMAIL_SEND_FabInv_Cut'", CboEmailFabInv_Cut.Text == "" ? "FALSE" : CboEmailFabInv_Cut.Text, "'Send Email on Saving  in Fabric Invoice Cut'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='CboEmailFabInv_Cut' and CompanyForID={7};",
                               "'FABRIC'", "'EMAIL_SEND_FabInv_Cut'", CboEmailFabInv_Cut.Text == "" ? "FALSE" : CboEmailFabInv_Cut.Text, "'Send Email on Saving  in Fabric Invoice Cut'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EMAIL_SEND_FabInv_ROLL = Dt.Select("GSName='EMAIL_SEND_FabInv_ROLL'");
            if (rst_EMAIL_SEND_FabInv_ROLL.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'EMAIL_SEND_FabInv_ROLL'", CboEmailFabInv_Roll.Text == "" ? "FALSE" : CboEmailFabInv_Roll.Text, "'Send Email on Saving In fabric Sales Invoice Roll'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='EMAIL_SEND_FabInv_ROLL' and CompanyForID={7};",
                               "'FABRIC'", "'EMAIL_SEND_FabInv_ROLL'", CboEmailFabInv_Roll.Text == "" ? "FALSE" : CboEmailFabInv_Roll.Text, "'Send Email on Saving In fabric Sales Invoice Roll'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EMAIL_SEND_FabInv_Serial = Dt.Select("GSName='EMAIL_SEND_FabInv_Serial'");
            if (rst_EMAIL_SEND_FabInv_Serial.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'EMAIL_SEND_FabInv_Serial'", CboEmailFabInv_Serial.Text == "" ? "FALSE" : CboEmailFabInv_Serial.Text, "'Send Email on Saving In fabric Sales Invoice Roll'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='EMAIL_SEND_FabInv_Serial' and CompanyForID={7};",
                               "'FABRIC'", "'EMAIL_SEND_FabInv_Serial'", CboEmailFabInv_Serial.Text == "" ? "FALSE" : CboEmailFabInv_Serial.Text, "'Send Email on Saving In fabric Sales Invoice Roll'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EMAIL_SEND_SO = Dt.Select("GSName='EMAIL_SEND_SO'");
            if (rst_EMAIL_SEND_SO.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'EMAIL_SEND_SO'", CboEmailFabSO.Text == "" ? "FALSE" : CboEmailFabSO.Text, "'Send Email On Saving in The Fabric Sales Order'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='EMAIL_SEND_SO' and CompanyForID={7};",
                           "'FABRIC'", "'EMAIL_SEND_SO'", CboEmailFabSO.Text == "" ? "FALSE" : CboEmailFabSO.Text, "'Send Email On Saving in The Fabric Sales Order'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EMAIL_SEND_FI = Dt.Select("GSName='EMAIL_SEND_FI'");
            if (rst_EMAIL_SEND_FI.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'EMAIL_SEND_FI'", cboEmailFI.Text == "" ? "FALSE" : cboEmailFI.Text, "'Send Email On Saving in The Fabric Inward'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='EMAIL_SEND_FI' and CompanyForID={7};",
                           "'FABRIC'", "'EMAIL_SEND_FI'", cboEmailFI.Text == "" ? "FALSE" : cboEmailFI.Text, "'Send Email On Saving in The Fabric Inward'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EMAIL_SEND_SO_ROLL = Dt.Select("GSName='EMAIL_SEND_SO_ROLL'");
            if (rst_EMAIL_SEND_SO_ROLL.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'EMAIL_SEND_SO_ROLL'", CboEmailFabSO_Roll.Text == "" ? "FALSE" : CboEmailFabSO_Roll.Text, "'Send Email on Saving In fabric Sales Order Roll'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='EMAIL_SEND_SO_ROLL' and CompanyForID={7};",
                               "'FABRIC'", "'EMAIL_SEND_SO_ROLL'", CboEmailFabSO_Roll.Text == "" ? "FALSE" : CboEmailFabSO_Roll.Text, "'Send Email on Saving In fabric Sales Order Roll'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EMAIL_SEND_SO_Serial = Dt.Select("GSName='EMAIL_SEND_SO_Serial'");
            if (rst_EMAIL_SEND_SO_Serial.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'EMAIL_SEND_SO_Serial'", CboEmailFabSO_Serial.Text == "" ? "FALSE" : CboEmailFabSO_Serial.Text, "'Send Email on Saving In fabric Sales Order Serial'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='EMAIL_SEND_SO_Serial' and CompanyForID={7};",
                               "'FABRIC'", "'EMAIL_SEND_SO_Serial'", CboEmailFabSO_Serial.Text == "" ? "FALSE" : CboEmailFabSO_Serial.Text, "'Send Email on Saving In fabric Sales Order Serial'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }
            #endregion

            #region SMS
            DataRow[] rst_SMS_SEND_BookInv = Dt.Select("GSName='SMS_SEND_BookInv'");
            if (rst_SMS_SEND_BookInv.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'SMS_SEND_BookInv'", CboSMSBookInv.Text == "" ? "FALSE" : CboSMSBookInv.Text, "'Send SMS on Saving In Book Invoice'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='SMS_SEND_BookInv' and CompanyForID={7};",
                               "'FABRIC'", "'SMS_SEND_BookInv'", CboSMSBookInv.Text == "" ? "FALSE" : CboSMSBookInv.Text, "'Send SMS on Saving In Book Invoice'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_SMS_SEND_BookSO = Dt.Select("GSName='SMS_SEND_BookSO'");
            if (rst_SMS_SEND_BookSO.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'SMS_SEND_BookSO'", CboSMSBookSO.Text == "" ? "FALSE" : CboSMSBookSO.Text, "'Send SMS in Book Sales Order'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='SMS_SEND_BookSO' and CompanyForID={7};",
                               "'FABRIC'", "'SMS_SEND_BookSO'", CboSMSBookSO.Text == "" ? "FALSE" : CboSMSBookSO.Text, "'Send SMS in Book Sales Order'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_SMS_SEND_DC = Dt.Select("GSName='SMS_SEND_DC'");
            if (rst_SMS_SEND_DC.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'SMS_SEND_DC'", CboSMSDC.Text == "" ? "FALSE" : CboSMSDC.Text, "'Send SMS To On Saving Of Record in Delivery Challan'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='SMS_SEND_DC' and CompanyForID={7};",
                               "'FABRIC'", "'SMS_SEND_DC'", CboSMSDC.Text == "" ? "FALSE" : CboSMSDC.Text, "'Send SMS To On Saving Of Record in Delivery Challan'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_SMS_SEND_DC_Serail = Dt.Select("GSName='SMS_SEND_DC_Serail'");
            if (rst_FDC_FABRICDELIVERYCHALLAN.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'SMS_SEND_DC_Serail'", CboSMSDC_Serial.Text == "" ? "FALSE" : CboSMSDC_Serial.Text, "'Send SMS in Saving in Fabric Delivery Challan Serial'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='SMS_SEND_DC_Serail' and CompanyForID={7};",
                               "'FABRIC'", "'SMS_SEND_DC_Serail'", CboSMSDC_Serial.Text == "" ? "FALSE" : CboSMSDC_Serial.Text, "'Send SMS in Saving in Fabric Delivery Challan Serial'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_SMS_SEND_FabInv = Dt.Select("GSName='SMS_SEND_FabInv'");
            if (rst_SMS_SEND_FabInv.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'SMS_SEND_FabInv'", CboSMSFabInv.Text == "" ? "FALSE" : CboSMSFabInv.Text, "'Send SMS On Saving In Teh Fabric Invoice'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='SMS_SEND_FabInv' and CompanyForID={7};",
                               "'FABRIC'", "'SMS_SEND_FabInv'", CboSMSFabInv.Text == "" ? "FALSE" : CboSMSFabInv.Text, "'Send SMS On Saving In Teh Fabric Invoice'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_SMS_SEND_FabInv_Cut = Dt.Select("GSName='SMS_SEND_FabInv_Cut'");
            if (rst_SMS_SEND_FabInv_Cut.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'SMS_SEND_FabInv_Cut'", CboSMSFabInv_Cut.Text == "" ? "FALSE" : CboSMSFabInv_Cut.Text, "'Send SMS on Saving  in Fabric Invoice Cut'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='CboSMSFabInv_Cut' and CompanyForID={7};",
                               "'FABRIC'", "'SMS_SEND_FabInv_Cut'", CboSMSFabInv_Cut.Text == "" ? "FALSE" : CboSMSFabInv_Cut.Text, "'Send SMS on Saving  in Fabric Invoice Cut'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_SMS_SEND_FabInv_ROLL = Dt.Select("GSName='SMS_SEND_FabInv_ROLL'");
            if (rst_SMS_SEND_FabInv_ROLL.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'SMS_SEND_FabInv_ROLL'", CboSMSFabInv_Roll.Text == "" ? "FALSE" : CboSMSFabInv_Roll.Text, "'Send SMS on Saving In fabric Sales Invoice Roll'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='SMS_SEND_FabInv_ROLL' and CompanyForID={7};",
                               "'FABRIC'", "'SMS_SEND_FabInv_ROLL'", CboSMSFabInv_Roll.Text == "" ? "FALSE" : CboSMSFabInv_Roll.Text, "'Send SMS on Saving In fabric Sales Invoice Roll'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_SMS_SEND_FabInv_Serial = Dt.Select("GSName='SMS_SEND_FabInv_Serial'");
            if (rst_SMS_SEND_FabInv_Serial.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'SMS_SEND_FabInv_Serial'", CboSMSFabInv_Serial.Text == "" ? "FALSE" : CboSMSFabInv_Serial.Text, "'Send SMS on Saving In fabric Sales Invoice Roll'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='SMS_SEND_FabInv_Serial' and CompanyForID={7};",
                               "'FABRIC'", "'SMS_SEND_FabInv_Serial'", CboSMSFabInv_Serial.Text == "" ? "FALSE" : CboSMSFabInv_Serial.Text, "'Send SMS on Saving In fabric Sales Invoice Roll'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_SMS_SEND_SO = Dt.Select("GSName='SMS_SEND_SO'");
            if (rst_FDC_FABRICDELIVERYCHALLAN.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'SMS_SEND_SO'", CboSMSFabSO.Text == "" ? "FALSE" : CboSMSFabSO.Text, "'Send SMS On Saving in The Fabric Sales Order'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='SMS_SEND_SO' and CompanyForID={7};",
                           "'FABRIC'", "'SMS_SEND_SO'", CboSMSFabSO.Text == "" ? "FALSE" : CboSMSFabSO.Text, "'Send SMS On Saving in The Fabric Sales Order'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_SMS_SEND_FI = Dt.Select("GSName='SMS_SEND_FI'");
            if (rst_SMS_SEND_FI.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'SMS_SEND_FI'", cboSMSFI.Text == "" ? "FALSE" : cboSMSFI.Text, "'Send SMS On Saving in The Fabric Inward'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='SMS_SEND_FI' and CompanyForID={7};",
                           "'FABRIC'", "'SMS_SEND_FI'", cboSMSFI.Text == "" ? "FALSE" : cboSMSFI.Text, "'Send SMS On Saving in The Fabric Inward'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_SMS_SEND_SO_ROLL = Dt.Select("GSName='SMS_SEND_SO_ROLL'");
            if (rst_SMS_SEND_SO_ROLL.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'SMS_SEND_SO_ROLL'", CboSMSFabSO_Roll.Text == "" ? "FALSE" : CboSMSFabSO_Roll.Text, "'Send SMS on Saving In fabric Sales Order Roll'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='SMS_SEND_SO_ROLL' and CompanyForID={7};",
                               "'FABRIC'", "'SMS_SEND_SO_ROLL'", CboSMSFabSO_Roll.Text == "" ? "FALSE" : CboSMSFabSO_Roll.Text, "'Send SMS on Saving In fabric Sales Order Roll'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_SMS_SEND_SO_Serial = Dt.Select("GSName='SMS_SEND_SO_Serial'");
            if (rst_SMS_SEND_SO_Serial.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'SMS_SEND_SO_Serial'", CboSMSFabSO_Serial.Text == "" ? "FALSE" : CboSMSFabSO_Serial.Text, "'Send SMS on Saving In fabric Sales Order Serial'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='SMS_SEND_SO_Serial' and CompanyForID={7};",
                               "'FABRIC'", "'SMS_SEND_SO_Serial'", CboSMSFabSO_Serial.Text == "" ? "FALSE" : CboSMSFabSO_Serial.Text, "'Send SMS on Saving In fabric Sales Order Serial'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }
            #endregion

            #region Other
            DataRow[] rst_FPO_SEND_SMS = Dt.Select("GSName='FPO_SEND_SMS'");
            if (rst_FPO_SEND_SMS.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FPO_SEND_SMS'", CboSMSFPO.Text == "" ? "FALSE" : CboSMSFPO.Text, "'Send Sms In the Fabric purchase Order'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FPO_SEND_SMS' and CompanyForID={7};",
                               "'FABRIC'", "'FPO_SEND_SMS'", CboSMSFPO.Text == "" ? "FALSE" : CboSMSFPO.Text, "'Send Sms In the Fabric purchase Order'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FPO_SEND_EMAIL = Dt.Select("GSName='FPO_SEND_EMAIL'");
            if (rst_FPO_SEND_EMAIL.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FPO_SEND_EMAIL'", CboEmailFPO.Text == "" ? "FALSE" : CboEmailFPO.Text, "'Send Email In the Fabric Purchase Order'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FPO_SEND_EMAIL' and CompanyForID={7};",
                               "'FABRIC'", "'FPO_SEND_EMAIL'", CboEmailFPO.Text == "" ? "FALSE" : CboEmailFPO.Text, "'Send Email In the Fabric Purchase Order'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FO_BOOKDESIGN = Dt.Select("GSName='FO_BOOKDESIGN'");
            if (rst_FO_BOOKDESIGN.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FO_BOOKDESIGN'", CboShowFO_BOOKDESIGN.Text == "" ? "FALSE" : CboShowFO_BOOKDESIGN.Text, "'Shows BookDesignID In The Grid Of Fabric Opening'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FO_BOOKDESIGN' and CompanyForID={7};",
                               "'FABRIC'", "'FO_BOOKDESIGN'", CboShowFO_BOOKDESIGN.Text == "" ? "FALSE" : CboShowFO_BOOKDESIGN.Text, "'Shows BookDesignID In The Grid Of Fabric Opening'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_PO_SERIAL = Dt.Select("GSName='PO_SERIAL'");
            if (rst_PO_SERIAL.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'PO_SERIAL'", CboPOBookSerial.Text == "" ? "FALSE" : CboPOBookSerial.Text, "'Add Book Serial in Purchase Order'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='PO_SERIAL' and CompanyForID={7};",
                               "'FABRIC'", "'PO_SERIAL'", CboPOBookSerial.Text == "" ? "FALSE" : CboPOBookSerial.Text, "'Add Book Serial in Purchase Order'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FO_SERIAL = Dt.Select("GSName='FO_SERIAL'");
            if (rst_FO_SERIAL.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FO_SERIAL'", CboFOBookSerial.Text == "" ? "FALSE" : CboFOBookSerial.Text, "'Add Book Serial in Fabric Opening'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FO_SERIAL' and CompanyForID={7};",
                               "'FABRIC'", "'FO_SERIAL'", CboFOBookSerial.Text == "" ? "FALSE" : CboFOBookSerial.Text, "'Add Book Serial in Fabric Opening'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FP_CUTWISE = Dt.Select("GSName='FP_CUTWISE'");
            if (rst_FP_CUTWISE.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FP_CUTWISE'", CboCutwiseFP3.Text == "" ? "FALSE" : CboCutwiseFP3.Text, "'Cutwise Consumption in Fabric Production 3'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FP_CUTWISE' and CompanyForID={7};",
                               "'FABRIC'", "'FP_CUTWISE'", CboCutwiseFP3.Text == "" ? "FALSE" : CboCutwiseFP3.Text, "'Cutwise Consumption in Fabric Production 3'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_INWARD_SERIAL = Dt.Select("GSName='INWARD_SERIAL'");
            if (rst_INWARD_SERIAL.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'INWARD_SERIAL'", CboSerialwiseInward.Text == "" ? "FALSE" : CboSerialwiseInward.Text, "'Serialwise Inward'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='INWARD_SERIAL' and CompanyForID={7};",
                               "'FABRIC'", "'INWARD_SERIAL'", CboSerialwiseInward.Text == "" ? "FALSE" : CboSerialwiseInward.Text, "'Serialwise Inward'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_flg_Series = Dt.Select("GSName='flg_Series'");
            if (rst_flg_Series.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'flg_Series'", CboEnableSeries.Text == "" ? "FALSE" : CboEnableSeries.Text, "'ENABLE SERIES'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='flg_Series' and CompanyForID={7};",
                               "'FABRIC'", "'flg_Series'", CboEnableSeries.Text == "" ? "FALSE" : CboEnableSeries.Text, "'ENABLE SERIES'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FDC_OVERDUE_ALT = Dt.Select("GSName='FDC_OVERDUE_ALT'");
            if (rst_FDC_OVERDUE_ALT.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FDC_OVERDUE_ALT'", CboOverdueAlertBI.Text == "" ? "FALSE" : CboOverdueAlertBI.Text, "'OVER DUE ALERT FOR BOOK ISSUE'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FDC_OVERDUE_ALT' and CompanyForID={7};",
                               "'FABRIC'", "'FDC_OVERDUE_ALT'", CboOverdueAlertBI.Text == "" ? "FALSE" : CboOverdueAlertBI.Text, "'OVER DUE ALERT FOR BOOK ISSUE'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FP_Pipe_Pnl = Dt.Select("GSName='FP_Pipe_Pnl'");
            if (rst_FP_Pipe_Pnl.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FP_Pipe_Pnl'", CboPipePanelFP3.Text == "" ? "FALSE" : CboPipePanelFP3.Text, "'Show The Pipe Panel in The Fabric Production3'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FP_Pipe_Pnl' and CompanyForID={7};",
                               "'FABRIC'", "'FP_Pipe_Pnl'", CboPipePanelFP3.Text == "" ? "FALSE" : CboPipePanelFP3.Text, "'Show The Pipe Panel in The Fabric Production3'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_PO_BOOKDESIGN = Dt.Select("GSName='PO_BOOKDESIGN'");
            if (rst_PO_BOOKDESIGN.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'PO_BOOKDESIGN'", CboShowPO_BOOKDESIGN.Text == "" ? "FALSE" : CboShowPO_BOOKDESIGN.Text, "'Add BookSerial in Purchase Order'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='PO_BOOKDESIGN' and CompanyForID={7};",
                               "'FABRIC'", "'PO_BOOKDESIGN'", CboShowPO_BOOKDESIGN.Text == "" ? "FALSE" : CboShowPO_BOOKDESIGN.Text, "'Add BookSerial in Purchase Order'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FT_SERIAL = Dt.Select("GSName='FT_SERIAL'");
            if (rst_FT_SERIAL.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FT_SERIAL'", CboHideFunc_BS.Text == "" ? "FALSE" : CboHideFunc_BS.Text, "'Hide the Functionality of Book Serial'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FT_SERIAL' and CompanyForID={7};",
                               "'FABRIC'", "'FT_SERIAL'", CboHideFunc_BS.Text == "" ? "FALSE" : CboHideFunc_BS.Text, "'Hide the Functionality of Book Serial'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_Vld_DupPieceNo = Dt.Select("GSName='Vld_DupPieceNo'");
            if (rst_Vld_DupPieceNo.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'Vld_DupPieceNo'", CboValDupPieceNo.Text == "" ? "FALSE" : CboValDupPieceNo.Text, "'Validates PieceNo and Returns True'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='Vld_DupPieceNo' and CompanyForID={7};",
                               "'FABRIC'", "'Vld_DupPieceNo'", CboValDupPieceNo.Text == "" ? "FALSE" : CboValDupPieceNo.Text, "'Validates PieceNo and Returns True'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_IncrtPieceNo_serialsalesReturn = Dt.Select("GSName='IncrtPieceNo_serialsalesReturn'");
            if (rst_IncrtPieceNo_serialsalesReturn.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'IncrtPieceNo_serialsalesReturn'", CboIncPieceNo.Text == "" ? "FALSE" : CboIncPieceNo.Text, "'Increament Piece No on Adding New Row'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='IncrtPieceNo_serialsalesReturn' and CompanyForID={7};",
                               "'FABRIC'", "'IncrtPieceNo_serialsalesReturn'", CboIncPieceNo.Text == "" ? "FALSE" : CboIncPieceNo.Text, "'Increament Piece No on Adding New Row'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_INC_PNO_STOCK = Dt.Select("GSName='INC_PNO_STOCK'");
            if (rst_INC_PNO_STOCK.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'INC_PNO_STOCK'", CboPieceNoIncStock.Text == "" ? "FALSE" : CboPieceNoIncStock.Text, "'Set True If the PieceNo  Wants  to be Increamented from Stock'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='INC_PNO_STOCK' and CompanyForID={7};",
                               "'FABRIC'", "'INC_PNO_STOCK'", CboPieceNoIncStock.Text == "" ? "FALSE" : CboPieceNoIncStock.Text, "'Set True If the PieceNo  Wants  to be Increamented from Stock'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            #endregion

            DataRow[] rst_PR_FR_DEP = Dt.Select("GSName='PR_FR_DEP'");
            if (rst_PR_FR_DEP.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'PR_FR_DEP'", CboPickStockGodown.Text == "" ? "FALSE" : CboPickStockGodown.Text, "'Allow Pick Stock From Godown'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='PR_FR_DEP' and CompanyForID={7};",
                               "'FABRIC'", "'PR_FR_DEP'", CboPickStockGodown.Text == "" ? "FALSE" : CboPickStockGodown.Text, "'Allow Pick Stock From Godown'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_PR_PNO_JB = Dt.Select("GSName='PR_PNO_JB'");
            if (rst_PR_PNO_JB.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'PR_PNO_JB'", CboFRollWithLoomNo.Text == "" ? "FALSE" : CboFRollWithLoomNo.Text, "'Allow Roll No With LUM No'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='PR_PNO_JB' and CompanyForID={7};",
                               "'FABRIC'", "'PR_PNO_JB'", CboFRollWithLoomNo.Text == "" ? "FALSE" : CboFRollWithLoomNo.Text, "'Allow Roll No With LUM No'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EMBFDC_RATETYPE = Dt.Select("GSName='EMBFDC_RATETYPE'");
            if (rst_EMBFDC_RATETYPE.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'EMBFDC_RATETYPE'", CboFEmbRateType.Text == "" ? "FALSE" : CboFEmbRateType.Text, "'Allow Rate Type In Embroadery Delivery Chalaln'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='EMBFDC_RATETYPE' and CompanyForID={7};",
                               "'FABRIC'", "'EMBFDC_RATETYPE'", CboFEmbRateType.Text == "" ? "FALSE" : CboFEmbRateType.Text, "'Allow Rate Type In Embroadery Delivery Chalaln'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FDC_POGRAMCARDNo = Dt.Select("GSName='FDC_POGRAMCARDNo'");
            if (rst_FDC_POGRAMCARDNo.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FDC_POGRAMCARDNo'", CboFDC_POGRAMCARDNo.Text == "" ? "FALSE" : CboFDC_POGRAMCARDNo.Text, "'Allow Fabric Delivery Challan Program No'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FDC_POGRAMCARDNo' and CompanyForID={7};",
                               "'FABRIC'", "'FDC_POGRAMCARDNo'", CboFDC_POGRAMCARDNo.Text == "" ? "FALSE" : CboFDC_POGRAMCARDNo.Text, "'Allow Fabric Delivery Challan Program No'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FP_ONLYINWARD = Dt.Select("GSName='FP_ONLYINWARD'");
            if (rst_FP_ONLYINWARD.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FP_ONLYINWARD'", CboFP_ONLYINWARD.Text == "" ? "FALSE" : CboFP_ONLYINWARD.Text, "'Enables Inward only in Fabric Production 3'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FP_ONLYINWARD' and CompanyForID={7};",
                               "'FABRIC'", "'FP_ONLYINWARD'", CboFP_ONLYINWARD.Text == "" ? "FALSE" : CboFP_ONLYINWARD.Text, "'Enables Inward only in Fabric Production 3'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FP_ORD_WISE = Dt.Select("GSName='FP_ORD_WISE'");
            if (rst_FP_ORD_WISE.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FP_ORD_WISE'", CboFP_ORD_WISE.Text == "" ? "FALSE" : CboFP_ORD_WISE.Text, "'Allow Fabric Production Order Wise'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FP_ORD_WISE' and CompanyForID={7};",
                               "'FABRIC'", "'FP_ORD_WISE'", CboFP_ORD_WISE.Text == "" ? "FALSE" : CboFP_ORD_WISE.Text, "'Allow Fabric Production Order Wise'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FP_PipeNo = Dt.Select("GSName='FP_PipeNo'");
            if (rst_FP_PipeNo.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FP_PipeNo'", CboFP_PipeNo.Text == "" ? "FALSE" : CboFP_PipeNo.Text, "'Enables Pipe Panel in Fabric Production 3'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FP_PipeNo' and CompanyForID={7};",
                               "'FABRIC'", "'FP_PipeNo'", CboFP_PipeNo.Text == "" ? "FALSE" : CboFP_PipeNo.Text, "'Enables Pipe Panel in Fabric Production 3'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_FDC_GRIDENABLE = Dt.Select("GSName='FDC_GRIDENABLE'");
            if (rst_FDC_GRIDENABLE.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'FABRIC'", "'FDC_GRIDENABLE'", CboFDC_GRIDENABLE.Text == "" ? "FALSE" : CboFDC_GRIDENABLE.Text, "'Allow Grid To Be Enable in The Delivery Challan'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='FABRIC' AND GSName='FDC_GRIDENABLE' and CompanyForID={7};",
                               "'FABRIC'", "'FDC_GRIDENABLE'", CboFDC_GRIDENABLE.Text == "" ? "FALSE" : CboFDC_GRIDENABLE.Text, "'Allow Grid To Be Enable in The Delivery Challan'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }


        }

        private void Broker_ModuleSave()
        {
            DataTable Dt = DB.GetDT("SELECT  * from tbl_GlobalSettings WHERE GType like 'Broker%' AND CompanyForID=" + Db_Detials.CompID + "", false);

            DataRow[] rst_EnableAgentCommsnCalcMethod1 = Dt.Select("GSName='ENABLE_BROKER_CALCMETHOD1'");
            if (rst_EnableAgentCommsnCalcMethod1.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BROKER MODULE'", "'ENABLE_BROKER_CALCMETHOD1'", cboBMEnableAgentCommissionCalcMethod1.Text == "" ? "FALSE" : cboBMEnableAgentCommissionCalcMethod1.Text, "'Allows Calculation On Percentage Base, On Total Of Item Amount Voucher'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BROKER MODULE' AND GSName='ENABLE_BROKER_CALCMETHOD1'  and CompanyForID={7};",
                               "'BROKER MODULE'", "'ENABLE_BROKER_CALCMETHOD1'", cboBMEnableAgentCommissionCalcMethod1.Text == "" ? "FALSE" : cboBMEnableAgentCommissionCalcMethod1.Text, "'Allows Calculation On Percentage Base, On Total Of Item Amount Voucher'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EnableAgentCommsnCalcMethod2 = Dt.Select("GSName='ENABLE_BROKER_CALCMETHOD2'");
            if (rst_EnableAgentCommsnCalcMethod2.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BROKER MODULE'", "'ENABLE_BROKER_CALCMETHOD2'", cboBMEnableAgentCommissionCalcMethod2.Text == "" ? "FALSE" : cboBMEnableAgentCommissionCalcMethod2.Text, "'Allows Calculation On Item Qty Base'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BROKER MODULE' AND GSName='ENABLE_BROKER_CALCMETHOD2'  and CompanyForID={7};",
                               "'BROKER MODULE'", "'ENABLE_BROKER_CALCMETHOD2'", cboBMEnableAgentCommissionCalcMethod2.Text == "" ? "FALSE" : cboBMEnableAgentCommissionCalcMethod2.Text, "'Allows Calculation On Item Qty Base'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EnableBrokerinSalesOrder = Dt.Select("GSName='ENABLE_BROKER_FABSALESORDER'");
            if (rst_EnableBrokerinSalesOrder.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BROKER MODULE'", "'ENABLE_BROKER_FABSALESORDER'", cboBMEnableAgentinSalesOrder.Text == "" ? "FALSE" : cboBMEnableAgentinSalesOrder.Text, "'Allows Broker Commmission in Fabric SalesOrder'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BROKER MODULE' AND GSName='ENABLE_BROKER_FABSALESORDER'  and CompanyForID={7};",
                               "'BROKER MODULE'", "'ENABLE_BROKER_FABSALESORDER'", cboBMEnableAgentinSalesOrder.Text == "" ? "FALSE" : cboBMEnableAgentinSalesOrder.Text, "'Allows Broker Commmission in Fabric SalesOrder'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EnableBrokerinGoodsReceiptNote = Dt.Select("GSName='ENABLE_BROKER_GOODSRECEIPTNOTE'");
            if (rst_EnableBrokerinGoodsReceiptNote.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BROKER MODULE'", "'ENABLE_BROKER_GOODSRECEIPTNOTE'", cboBMEnableAgentinGoodsReceiptNote.Text == "" ? "FALSE" : cboBMEnableAgentinGoodsReceiptNote.Text, "'Allows Broker Commmission in Goods Receipt Note'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BROKER MODULE' AND GSName='ENABLE_BROKER_GOODSRECEIPTNOTE'  and CompanyForID={7};",
                               "'BROKER MODULE'", "'ENABLE_BROKER_GOODSRECEIPTNOTE'", cboBMEnableAgentinGoodsReceiptNote.Text == "" ? "FALSE" : cboBMEnableAgentinGoodsReceiptNote.Text, "'Allows Broker Commmission in Goods Receipt Note'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EnableBrokerinDelChln = Dt.Select("GSName='ENABLE_BROKER_FAB_DLRYCHLN'");
            if (rst_EnableBrokerinDelChln.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BROKER MODULE'", "'ENABLE_BROKER_FAB_DLRYCHLN'", cboBMEnableAgentinDeliveryChallan.Text == "" ? "FALSE" : cboBMEnableAgentinDeliveryChallan.Text, "'Allows Broker Commmission in Fabric DeliveryChallan'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BROKER MODULE' AND GSName='ENABLE_BROKER_FAB_DLRYCHLN'  and CompanyForID={7};",
                               "'BROKER MODULE'", "'ENABLE_BROKER_FAB_DLRYCHLN'", cboBMEnableAgentinDeliveryChallan.Text == "" ? "FALSE" : cboBMEnableAgentinDeliveryChallan.Text, "'Allows Broker Commmission in Fabric DeliveryChallan'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EnableBrokerinFabPurchOrder = Dt.Select("GSName='ENABLE_BROKER_FAB_PURCHORDER'");
            if (rst_EnableBrokerinFabPurchOrder.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BROKER MODULE'", "'ENABLE_BROKER_FAB_PURCHORDER'", cboBMEnableAgentinPurchaseOrder.Text == "" ? "FALSE" : cboBMEnableAgentinPurchaseOrder.Text, "'Allows Broker Commmission in Fabric Purchase Order'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BROKER MODULE' AND GSName='ENABLE_BROKER_FAB_PURCHORDER'  and CompanyForID={7};",
                               "'BROKER MODULE'", "'ENABLE_BROKER_FAB_PURCHORDER'", cboBMEnableAgentinPurchaseOrder.Text == "" ? "FALSE" : cboBMEnableAgentinPurchaseOrder.Text, "'Allows Broker Commmission in Fabric Purchase Order'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EnableBrokerinFabSalesBill = Dt.Select("GSName='ENABLE_BROKER_FAB_SALESBILL'");
            if (rst_EnableBrokerinFabSalesBill.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BROKER MODULE'", "'ENABLE_BROKER_FAB_SALESBILL'", cboBMEnableAgentinSalesBill.Text == "" ? "FALSE" : cboBMEnableAgentinSalesBill.Text, "'Allows Broker Commmission in Fabric Sales Bill'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BROKER MODULE' AND GSName='ENABLE_BROKER_FAB_SALESBILL'  and CompanyForID={7};",
                               "'BROKER MODULE'", "'ENABLE_BROKER_FAB_SALESBILL'", cboBMEnableAgentinSalesBill.Text == "" ? "FALSE" : cboBMEnableAgentinSalesBill.Text, "'Allows Broker Commmission in Fabric Sales Bill'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EnableBrokerinFabSalesBill_Cut = Dt.Select("GSName='ENABLE_BROKER_FAB_SALESBILL_CUT'");
            if (rst_EnableBrokerinFabSalesBill_Cut.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BROKER MODULE'", "'ENABLE_BROKER_FAB_SALESBILL_CUT'", cboBMEnableAgentinSaleBill_Cut.Text == "" ? "FALSE" : cboBMEnableAgentinSaleBill_Cut.Text, "'Allows Broker Commmission in Fabric Sales Bill_Cut'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BROKER MODULE' AND GSName='ENABLE_BROKER_FAB_SALESBILL_CUT'  and CompanyForID={7};",
                               "'BROKER MODULE'", "'ENABLE_BROKER_FAB_SALESBILL_CUT'", cboBMEnableAgentinSaleBill_Cut.Text == "" ? "FALSE" : cboBMEnableAgentinSaleBill_Cut.Text, "'Allows Broker Commmission in Fabric Sales Bill_Cut'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EnableBrokerinFabSalesBill_Return = Dt.Select("GSName='ENABLE_BROKER_FAB_SALESBILLRETURN'");
            if (rst_EnableBrokerinFabSalesBill_Return.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BROKER MODULE'", "'ENABLE_BROKER_FAB_SALESBILLRETURN'", cboBMEnableAgentinSalesReturn.Text == "" ? "FALSE" : cboBMEnableAgentinSalesReturn.Text, "'Allows Broker Commmission in Fabric Sales Bill Return'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BROKER MODULE' AND GSName='ENABLE_BROKER_FAB_SALESBILLRETURN'  and CompanyForID={7};",
                               "'BROKER MODULE'", "'ENABLE_BROKER_FAB_SALESBILLRETURN'", cboBMEnableAgentinSalesReturn.Text == "" ? "FALSE" : cboBMEnableAgentinSalesReturn.Text, "'Allows Broker Commmission in Fabric Sales Bill Return'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EnableBrokerinFabSalesBill_CutReturn = Dt.Select("GSName='ENABLE_BROKER_FAB_SALESBILL_CUTRETURN'");
            if (rst_EnableBrokerinFabSalesBill_CutReturn.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BROKER MODULE'", "'ENABLE_BROKER_FAB_SALESBILL_CUTRETURN'", cboBMEnableAgentinSaleBill_CutReturn.Text == "" ? "FALSE" : cboBMEnableAgentinSaleBill_CutReturn.Text, "'Allows Broker Commmission in Fabric Sales Bill Cut Return'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BROKER MODULE' AND GSName='ENABLE_BROKER_FAB_SALESBILL_CUTRETURN'  and CompanyForID={7};",
                               "'BROKER MODULE'", "'ENABLE_BROKER_FAB_SALESBILL_CUTRETURN'", cboBMEnableAgentinSaleBill_CutReturn.Text == "" ? "FALSE" : cboBMEnableAgentinSaleBill_CutReturn.Text, "'Allows Broker Commmission in Fabric Sales Bill Cut Return'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EnableBrokerinFabPurchase = Dt.Select("GSName='ENABLE_BROKER_FAB_PURCHASE'");
            if (rst_EnableBrokerinFabPurchase.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BROKER MODULE'", "'ENABLE_BROKER_FAB_PURCHASE'", cboBMEnableAgentinPurchase.Text == "" ? "FALSE" : cboBMEnableAgentinPurchase.Text, "'Allows Broker Commmission in Fabric Purchase'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BROKER MODULE' AND GSName='ENABLE_BROKER_FAB_PURCHASE'  and CompanyForID={7};",
                               "'BROKER MODULE'", "'ENABLE_BROKER_FAB_PURCHASE'", cboBMEnableAgentinPurchase.Text == "" ? "FALSE" : cboBMEnableAgentinPurchase.Text, "'Allows Broker Commmission in Fabric Purchase'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_EnableBrokerinFabPurchaseReturn = Dt.Select("GSName='ENABLE_BROKER_FAB_PURCHASERETURN'");
            if (rst_EnableBrokerinFabPurchaseReturn.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BROKER MODULE'", "'ENABLE_BROKER_FAB_PURCHASERETURN'", cboBMEnableAgentinPurchaseReturn.Text == "" ? "FALSE" : cboBMEnableAgentinPurchaseReturn.Text, "'Allows Broker Commmission in Fabric Purchase Return'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BROKER MODULE' AND GSName='ENABLE_BROKER_FAB_PURCHASERETURN'  and CompanyForID={7};",
                               "'BROKER MODULE'", "'ENABLE_BROKER_FAB_PURCHASERETURN'", cboBMEnableAgentinPurchaseReturn.Text == "" ? "FALSE" : cboBMEnableAgentinPurchaseReturn.Text, "'Allows Broker Commmission in Fabric Purchase Return'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }
        }

        private void YarnSave()
        {
            DataTable Dt = DB.GetDT("SELECT  * from tbl_GlobalSettings WHERE GType like 'YARN%' AND CompanyForID=" + Db_Detials.CompID + "", false);

            DataRow[] rst_YR_PO = Dt.Select("GSName='YR_PO'");
            if (rst_YR_PO.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'YARN'", "'YR_PO'", CboYProcessOrder.Text == "" ? "FALSE" : CboYProcessOrder.Text, "'Yarn Receipt on Process Order'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='YARN' AND GSName='YR_PO' and CompanyForID={7};",
                               "'YARN'", "'YR_PO'", CboYProcessOrder.Text == "" ? "FALSE" : CboYProcessOrder.Text, "'Yarn Receipt on Process Order'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_Y_RTN_BOX = Dt.Select("GSName='Y_RTN_BOX'");
            if (rst_Y_RTN_BOX.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'YARN'", "'Y_RTN_BOX'", cboYRBoxWise.Text == "" ? "FALSE" : cboYRBoxWise.Text, "'Yarn Return Box Wise'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='YARN' AND GSName='Y_RTN_BOX' and CompanyForID={7};",
                               "'YARN'", "'Y_RTN_BOX'", cboYRBoxWise.Text == "" ? "FALSE" : cboYRBoxWise.Text, "'Yarn Return Box Wise'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_YI_OD = Dt.Select("GSName='YI_OD'");
            if (rst_YI_OD.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'YARN'", "'YI_OD'", cboYIInternalOrder.Text == "" ? "FALSE" : cboYIInternalOrder.Text, "'Allow InternalOrderNo in YarnIssue transaction'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='YARN' AND GSName='YI_OD' and CompanyForID={7};",
                               "'YARN'", "'YI_OD'", cboYIInternalOrder.Text == "" ? "FALSE" : cboYIInternalOrder.Text, "'Allow InternalOrderNo in YarnIssue transaction'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_YDC = Dt.Select("GSName='YDC'");
            if (rst_YDC.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'YARN'", "'YDC'", cboYBagWise.Text == "" ? "FALSE" : cboYBagWise.Text, "'Allow Bag wise Entry in Yarn Delivery Challan'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='YARN' AND GSName='YDC' and CompanyForID={7};",
                               "'YARN'", "'YDC'", cboYBagWise.Text == "" ? "FALSE" : cboYBagWise.Text, "'Allow Bag wise Entry in Yarn Delivery Challan'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }
            DataRow[] rst_MTY_DO = Dt.Select("GSName='MTY_DO'");
            if (rst_MTY_DO.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5}, {6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'YARN'", "'MTY_DO'", txtMultiDo.Text == "" ? "FALSE" : txtMultiDo.Text, "'Multy DO Selection'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='YARN' AND GSName='MTY_DO'  and CompanyForID={7};",
                               "'YARN'", "'MTY_DO'", txtMultiDo.Text == "" ? "FALSE" : txtMultiDo.Text, "'Multy DO Selection'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

        }

        private void BeamSave()
        {
            DataTable Dt = DB.GetDT("SELECT  * from tbl_GlobalSettings WHERE GType like 'Beam%' AND CompanyForID=" + Db_Detials.CompID + "", false);

            DataRow[] rst_BD_ON = Dt.Select("GSName='BD_ON'");
            if (rst_BD_ON.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BEAM'", "'BD_ON'", cboBInternalOrder.Text == "" ? "FALSE" : cboBInternalOrder.Text, "'Allow InternalONo in Beam Design Master'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6}, IsModified=1,AddedOn=GETDATE() WHERE GType='BEAM' AND GSName='BD_ON' and CompanyForID={7};",
                               "'BEAM'", "'BD_ON'", cboBInternalOrder.Text == "" ? "FALSE" : cboBInternalOrder.Text, "'Allow InternalONo in Beam Design Master'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_BP_DEP_WS = Dt.Select("GSName='BP_DEP_WS'");
            if (rst_BP_DEP_WS.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BEAM'", "'BP_DEP_WS'", cboBProdcutionDepartment.Text == "" ? "FALSE" : cboBProdcutionDepartment.Text, "'Allows Beam Production Department Wise'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BEAM' AND GSName='BP_DEP_WS' and CompanyForID={7};",
                               "'BEAM'", "'BP_DEP_WS'", cboBProdcutionDepartment.Text == "" ? "FALSE" : cboBProdcutionDepartment.Text, "'Allows Beam Production Department Wise'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_ENABLE_TWEIGHT = Dt.Select("GSName='ENABLE_TWEIGHT'");
            if (rst_ENABLE_TWEIGHT.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BEAM'", "'ENABLE_TWEIGHT'", cboBEnableWt.Text == "" ? "FALSE" : cboBEnableWt.Text, "'Allow InternalOrderNo in YarnIssue transaction'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BEAM' AND GSName='ENABLE_TWEIGHT' and CompanyForID={7};",
                               "'BEAM'", "'ENABLE_TWEIGHT'", cboBEnableWt.Text == "" ? "FALSE" : cboBEnableWt.Text, "'Allow InternalOrderNo in YarnIssue transaction'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_BPW_PipeNo = Dt.Select("GSName='BPW_PipeNo'");
            if (rst_BPW_PipeNo.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BEAM'", "'BPW_PipeNo'", CboBPW_PipeNo.Text == "" ? "FALSE" : CboBPW_PipeNo.Text, "'Enable Pipe No In the Beam Production'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BEAM' AND GSName='BPW_PipeNo' and CompanyForID={7};",
                               "'BEAM'", "'BPW_PipeNo'", CboBPW_PipeNo.Text == "" ? "FALSE" : CboBPW_PipeNo.Text, "'Enable Pipe No In the Beam Production'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_BI_PipeNo = Dt.Select("GSName='BI_PipeNo'");
            if (rst_BI_PipeNo.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BEAM'", "'BI_PipeNo'", CboBI_PipeNo.Text == "" ? "FALSE" : CboBI_PipeNo.Text, "'Enable Pipe No In The Beam Issue Grid'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BEAM' AND GSName='BI_PipeNo' and CompanyForID={7};",
                               "'BEAM'", "'BI_PipeNo'", CboBI_PipeNo.Text == "" ? "FALSE" : CboBI_PipeNo.Text, "'Enable Pipe No In The Beam Issue Grid'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_BI_GROSSWT = Dt.Select("GSName='BI_GROSSWT'");
            if (rst_BI_GROSSWT.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BEAM'", "'BI_GROSSWT'", CboBeamGrossWt.Text == "" ? "FALSE" : CboBeamGrossWt.Text, "'Show Gross Wt in Beam Issue'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BEAM' AND GSName='BI_GROSSWT' and CompanyForID={7};",
                               "'BEAM'", "'BI_GROSSWT'", CboBeamGrossWt.Text == "" ? "FALSE" : CboBeamGrossWt.Text, "'Show Gross Wt in Beam Issue'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_WC_BD = Dt.Select("GSName='WC_BD'");
            if (rst_WC_BD.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BEAM'", "'WC_BD'", CboWC_BD.Text == "" ? "FALSE" : CboWC_BD.Text, "'Weft Consumption from Beam Design Card'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BEAM' AND GSName='WC_BD' and CompanyForID={7};",
                               "'BEAM'", "'WC_BD'", CboWC_BD.Text == "" ? "FALSE" : CboWC_BD.Text, "'Weft Consumption from Beam Design Card'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }
        }

        private void BookSave()
        {
            DataTable Dt = DB.GetDT("SELECT  * from tbl_GlobalSettings WHERE GType like 'Book%' AND CompanyForID=" + Db_Detials.CompID + "", false);

            DataRow[] rst_BI_BRK_COM = Dt.Select("GSName='BI_BRK_COM'");
            if (rst_BI_BRK_COM.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BOOK'", "'BI_BRK_COM'", CboBI_BRK_COM.Text == "" ? "FALSE" : CboBI_BRK_COM.Text, "'Broker Mandetory int Book Issue Entry'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6}, IsModified=1,AddedOn=GETDATE() WHERE GType='BOOK' AND GSName='BI_BRK_COM' and CompanyForID={7};",
                               "'BOOK'", "'BI_BRK_COM'", CboBI_BRK_COM.Text == "" ? "FALSE" : CboBI_BRK_COM.Text, "'Broker Mandetory int Book Issue Entry'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_BI_ORD_COMP = Dt.Select("GSName='BI_ORD_COMP'");
            if (rst_BI_ORD_COMP.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BOOK'", "'BI_ORD_COMP'", CboBI_ORD_COMP.Text == "" ? "FALSE" : CboBI_ORD_COMP.Text, "'Order Mandetory on Book Issue Entry'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BOOK' AND GSName='BI_ORD_COMP' and CompanyForID={7};",
                               "'BOOK'", "'BI_ORD_COMP'", CboBI_ORD_COMP.Text == "" ? "FALSE" : CboBI_ORD_COMP.Text, "'Order Mandetory on Book Issue Entry'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_BI_ORD_WISE = Dt.Select("GSName='BI_ORD_WISE'");
            if (rst_BI_ORD_WISE.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BOOK'", "'BI_ORD_WISE'", CboBI_ORD_WISE.Text == "" ? "FALSE" : CboBI_ORD_WISE.Text, "'Order Mandetory In Book Issue Entry'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BOOK' AND GSName='BI_ORD_WISE' and CompanyForID={7};",
                               "'BOOK'", "'BI_ORD_WISE'", CboBI_ORD_WISE.Text == "" ? "FALSE" : CboBI_ORD_WISE.Text, "'Order Mandetory In Book Issue Entry'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_BI_OVERDUE_ALT = Dt.Select("GSName='BI_OVERDUE_ALT'");
            if (rst_BI_OVERDUE_ALT.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BOOK'", "'BI_OVERDUE_ALT'", CboBI_OVERDUE_ALT.Text == "" ? "FALSE" : CboBI_OVERDUE_ALT.Text, "'Give Alert Massage on Credit Period Compleyio of Pending Bills'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BOOK' AND GSName='BI_OVERDUE_ALT' and CompanyForID={7};",
                               "'BOOK'", "'BI_OVERDUE_ALT'", CboBI_OVERDUE_ALT.Text == "" ? "FALSE" : CboBI_OVERDUE_ALT.Text, "'Give Alert Massage on Credit Period Compleyio of Pending Bills'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            //Not Confirmed
            DataRow[] rst_BSO_RATETYPE = Dt.Select("GSName='BSO_RATETYPE'");
            if (rst_BSO_RATETYPE.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'BOOK'", "'BSO_RATETYPE'", CboBSO_RATETYPE.Text == "" ? "FALSE" : CboBSO_RATETYPE.Text, "'Allows Enable Radio Buttons To Select Gross Rate In Fabric Delivery Challan Entry'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='BOOK' AND GSName='BSO_RATETYPE' and CompanyForID={7};",
                               "'BOOK'", "'BSO_RATETYPE'", CboBSO_RATETYPE.Text == "" ? "FALSE" : CboBSO_RATETYPE.Text, "'Allows Enable Radio Buttons To Select Gross Rate In Fabric Delivery Challan Entry'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }
        }

        private void AccountsSave()
        {
            DataTable Dt = DB.GetDT("SELECT  * from tbl_GlobalSettings WHERE GType like 'Accounts%' AND CompanyForID=" + Db_Detials.CompID + "", false);

            DataRow[] rst_LED_SHOWTYPE = Dt.Select("GSName='LED_SHOWTYPE'");
            if (rst_LED_SHOWTYPE.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'ACCOUNTS'", "'LED_SHOWTYPE'", CboLED_SHOWTYPE.Text == "" ? "FALSE" : CboLED_SHOWTYPE.Text, "'Show The Type Of Task in The Ledger Master'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6}, IsModified=1,AddedOn=GETDATE() WHERE GType='ACCOUNTS' AND GSName='LED_SHOWTYPE' and CompanyForID={7};",
                               "'ACCOUNTS'", "'LED_SHOWTYPE'", CboLED_SHOWTYPE.Text == "" ? "FALSE" : CboLED_SHOWTYPE.Text, "'Show The Type Of Task in The Ledger Master'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_ANS_SE = Dt.Select("GSName='ANS_SE'");
            if (rst_ANS_SE.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'ACCOUNTS'", "'ANS_SE'", CboANS_SE.Text == "" ? "FALSE" : CboANS_SE.Text, "'Allow Negative Stock in Sales Entry'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='ACCOUNTS' AND GSName='ANS_SE' and CompanyForID={7};",
                               "'ACCOUNTS'", "'ANS_SE'", CboANS_SE.Text == "" ? "FALSE" : CboANS_SE.Text, "'Allow Negative Stock in Sales Entry'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_ANS_SOE = Dt.Select("GSName='ANS_SOE'");
            if (rst_ANS_SOE.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'ACCOUNTS'", "'ANS_SOE'", CboANS_SOE.Text == "" ? "FALSE" : CboANS_SOE.Text, "'Allow Negative Stock in Sales Order Entry'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='ACCOUNTS' AND GSName='ANS_SOE' and CompanyForID={7};",
                               "'ACCOUNTS'", "'ANS_SOE'", CboANS_SOE.Text == "" ? "FALSE" : CboANS_SOE.Text, "'Allow Negative Stock in Sales Order Entry'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] ct_Req_LM = Dt.Select("GSName='CT_REQ_LM'");
            if (ct_Req_LM.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'ACCOUNTS'", "'CT_REQ_LM'", CboCityReq_LM.Text == "" ? "FALSE" : CboCityReq_LM.Text, "'City Required in Ledger Master'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='ACCOUNTS' AND GSName='CT_REQ_LM' and CompanyForID={7};",
                               "'ACCOUNTS'", "'CT_REQ_LM'", CboCityReq_LM.Text == "" ? "FALSE" : CboCityReq_LM.Text, "'City Required in Ledger Master'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] st_Req_LM = Dt.Select("GSName='ST_REQ_LM'");
            if (st_Req_LM.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'ACCOUNTS'", "'ST_REQ_LM'", cboStateReq_LM.Text == "" ? "FALSE" : cboStateReq_LM.Text, "'State Required in Ledger Master'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='ACCOUNTS' AND GSName='ST_REQ_LM' and CompanyForID={7};",
                               "'ACCOUNTS'", "'ST_REQ_LM'", cboStateReq_LM.Text == "" ? "FALSE" : cboStateReq_LM.Text, "'State Required in Ledger Master'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] br_Req_LM = Dt.Select("GSName='BR_REQ_LM'");
            if (br_Req_LM.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'ACCOUNTS'", "'BR_REQ_LM'", cboBrokerReq_LM.Text == "" ? "FALSE" : cboBrokerReq_LM.Text, "'Broker Required in Ledger Master'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='ACCOUNTS' AND GSName='BR_REQ_LM' and CompanyForID={7};",
                               "'ACCOUNTS'", "'BR_REQ_LM'", cboBrokerReq_LM.Text == "" ? "FALSE" : cboBrokerReq_LM.Text, "'Broker Required in Ledger Master'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] tr_Req_LM = Dt.Select("GSName='TR_REQ_LM'");
            if (tr_Req_LM.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'ACCOUNTS'", "'TR_REQ_LM'", cboTransportReq_LM.Text == "" ? "FALSE" : cboTransportReq_LM.Text, "'Transport Required in Ledger Master'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='ACCOUNTS' AND GSName='TR_REQ_LM' and CompanyForID={7};",
                               "'ACCOUNTS'", "'TR_REQ_LM'", cboTransportReq_LM.Text == "" ? "FALSE" : cboTransportReq_LM.Text, "'Transport Required in Ledger Master'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] ps_Req_LM = Dt.Select("GSName='PS_REQ_LM'");
            if (ps_Req_LM.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'ACCOUNTS'", "'PS_REQ_LM'", cboPurcSalesReq_LM.Text == "" ? "FALSE" : cboPurcSalesReq_LM.Text, "'Purchase/Sales Required in Ledger Master'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6},IsModified=1, AddedOn=GETDATE() WHERE GType='ACCOUNTS' AND GSName='PS_REQ_LM' and CompanyForID={7};",
                               "'ACCOUNTS'", "'PS_REQ_LM'", cboPurcSalesReq_LM.Text == "" ? "FALSE" : cboPurcSalesReq_LM.Text, "'Purchase/Sales Required in Ledger Master'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }
        }

        private void UserRightsSave()
        {
            DataTable Dt = DB.GetDT("SELECT  * from tbl_GlobalSettings WHERE GType like 'USER%' AND CompanyForID=" + Db_Detials.CompID + "", false);

            DataRow[] rst_APPROVE = Dt.Select("GSName='APPROVE'");
            if (rst_APPROVE.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'USER RIGHTS'", "'APPROVE'", cboApprove.Text == "" ? "FALSE" : cboApprove.Text, "'Allow Approve Righs in User Rights'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6}, IsModified=1,AddedOn=GETDATE() WHERE GType='USER RIGHTS' AND GSName='APPROVE' and CompanyForID={7};",
                               "'USER RIGHTS'", "'APPROVE'", cboApprove.Text == "" ? "FALSE" : cboApprove.Text, "'Allow Approve Righs in User Rights'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_AUDIT = Dt.Select("GSName='AUDIT'");
            if (rst_AUDIT.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'USER RIGHTS'", "'AUDIT'", cboAudit.Text == "" ? "FALSE" : cboAudit.Text, "'Allow AUDIT Righs in User Rights'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6}, IsModified=1,AddedOn=GETDATE() WHERE GType='USER RIGHTS' AND GSName='AUDIT' and CompanyForID={7};",
                               "'USER RIGHTS'", "'AUDIT'", cboAudit.Text == "" ? "FALSE" : cboAudit.Text, "'Allow AUDIT Righs in User Rights'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }

            DataRow[] rst_ApplnLogOffTime = Dt.Select("GSName='APPLICATIONLOGOFFTIME'");
            if (rst_AUDIT.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'USER RIGHTS'", "'APPLICATIONLOGOFFTIME'", txtApplicationLogOffTime.Text, "'Application Log Off Time,When User Is In StandBy Mode'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6}, IsModified=1,AddedOn=GETDATE() WHERE GType='USER RIGHTS' AND GSName='ApplicationLogOffTime' and CompanyForID={7};",
                               "'USER RIGHTS'", "'APPLICATIONLOGOFFTIME'", txtApplicationLogOffTime.Text, "'Application Log Off Time,When User Is In StandBy Mode'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }
        }

        private void GeneralSave()
        {
            DataTable Dt = DB.GetDT("SELECT  * from tbl_GlobalSettings WHERE GType like 'GENERAL%' AND CompanyForID=" + Db_Detials.CompID + "", false);
            DataRow[] rst_PASAV = Dt.Select("GSName='PASAV'");
            if (rst_PASAV.Length == 0)
            {
                strQry += string.Format("INSERT INTO tbl_GlobalSettings VALUES({0}, {1}, '{2}', {3}, {4}, {5},{6}, GETDATE(), {7},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                           "'GENERAL'", "'PASAV'", cboPasav.Text == "" ? "FALSE" : cboPasav.Text, "'Set true If Want To Print After Saving in All Voucher'", Db_Detials.CompID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
            }
            else
            {
                strQry += string.Format("UPDATE tbl_GlobalSettings SET GType={0}, GSName={1}, GSValue='{2}', Description={3}, CompID={4}, YearId={5},AddedBy={6}, IsModified=1,AddedOn=GETDATE() WHERE GType='GENERAL' AND GSName='PASAV' and CompanyForID={7};",
                               "'GENERAL'", "'PASAV'", cboPasav.Text == "" ? "FALSE" : cboPasav.Text, "'Set true If Want To Print After Saving in All Voucher'", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.CompID);
            }
        }

        private void LoadEmailSettingsVariables()
        {
            using (IDataReader iDr = DB.GetRS("SELECT  * from tbl_GlobalSettings WHERE GType='EMAIL' AND CompanyForID=" + Db_Detials.CompID + ""))
            {
                while (iDr.Read())
                {
                    if (iDr["GSName"].ToString().ToUpper() == "EMAIL_ADDRESS".ToUpper())
                    {
                        txtMailID.Text = CommonLogic.UnmungeString(iDr["GSValue"].ToString());
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "EMAIL_PASSWORD".ToUpper())
                    {
                        txtMailPwd.Text = CommonLogic.UnmungeString(iDr["GSValue"].ToString());
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "EMAIL_HOST".ToUpper())
                    {
                        txtHost.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "EMAIL_PORT".ToUpper())
                    {
                        txtPortNo.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "EMAIL_SIGNATURE".ToUpper())
                    {
                        txtSignature.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "MAIL_TYPE".ToUpper())
                    {
                        cboGmail.Text = iDr["GSValue"].ToString();
                    }
                }
            }

        }

        private void LoadSMSSettingsVariables()
        {
            using (IDataReader iDr = DB.GetRS("SELECT  * from tbl_GlobalSettings WHERE GType='SMS' AND CompanyForID=" + Db_Detials.CompID + ""))
            {
                while (iDr.Read())
                {
                    if (iDr["GSName"].ToString().ToUpper() == "SMS_UserName".ToUpper())
                    {
                        txtUserNM.Text = CommonLogic.UnmungeString(iDr["GSValue"].ToString());
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "SMS_Password".ToUpper())
                    {
                        txtPassword.Text = CommonLogic.UnmungeString(iDr["GSValue"].ToString());
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "SMS_SendSMS".ToUpper())
                    {
                        txtSendSMS.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "SMS_Balance".ToUpper())
                    {
                        txtSMSBal.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "SMS_Prefix".ToUpper())
                    {
                        txtSMSPrefix.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "SMS_Sender".ToUpper())
                    {
                        txtSender.Text = iDr["GSValue"].ToString();
                    }
                }
            }
        }

        private void LoadFabricDeliveryChallanSettingVariables()
        {
            using (IDataReader iDr = DB.GetRS("SELECT Distinct GsName,GSvalue from tbl_GlobalSettings WHERE GType like 'FABRIC%' AND CompanyForID=" + Db_Detials.CompID + ""))
            {
                while (iDr.Read())
                {
                    #region OLD

                    if (iDr["GSName"].ToString().ToUpper() == "BR_Scan_Chln".ToUpper())
                    {
                        cboFBarcode.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FR_PCSNO_ETRYNo".ToUpper())
                    {
                        cboFPieceWiseReciept.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "MTY_DC".ToUpper())
                    {
                        cboFMultiSeries.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "SUB_ORDER".ToUpper())
                    {
                        cboFSubOrder.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FDC_RATETYPE".ToUpper())
                    {
                        cboFRateType.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FDC_BRK_COM".ToUpper())
                    {
                        cboFBrokerMandatory.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FDC_ORD_COMP".ToUpper())
                    {
                        cboFOrderMandatory.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FDC".ToUpper())
                    {
                        cboFPiecewiseEntry.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "OVERDUE_ALT".ToUpper())
                    {
                        cboFAlertMsg.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "INVOICE_IN_DC".ToUpper())
                    {
                        cboFGenerateInvoice.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "PCS_NO_INCMT".ToUpper())
                    {
                        txtPieceNo.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FIN_CPY_FIS".ToUpper())
                    {
                        cboFEntryAutoCopy.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FD_ON".ToUpper())
                    {
                        cboFDesignOrderNo.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FSO".ToUpper())
                    {
                        cboFSOInternalOrder.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FSO_RATETYPE".ToUpper())
                    {
                        cboFSORateType.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FABSO_APRVD".ToUpper())
                    {
                        cboFOrderStatus.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "DEFECT_MTR_SAVE".ToUpper())
                    {
                        CboFEDefectMtrs.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "DSGN_CHECK".ToUpper())
                    {
                        cboFShowMessage.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "EBR_Scan_Chln".ToUpper())
                    {
                        cboFEBarcode.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "EFDC_BRK_COM".ToUpper())
                    {
                        CboFEBrokerMandatory.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "EFDC_ORD_COMP".ToUpper())
                    {
                        cboFEOrdermandatoryComp.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "EFDC_ORD_WISE".ToUpper())
                    {
                        cboFEOrderMendatoryWise.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "EFDC_RATETYPE".ToUpper())
                    {
                        cboFEnableRadio.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "EOVERDUE_ALT".ToUpper())
                    {
                        cboFAlertMsgInEmbroidary.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "ESUB_ORDER".ToUpper())
                    {
                        cboFEnableSubOrder.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FDC_FABRICDELIVERYCHALLAN".ToUpper())
                    {
                        CboFEnableProgramNo.Text = iDr["GSValue"].ToString();
                    }


                    else if (iDr["GSName"].ToString().ToUpper() == "FP_WC_LM".ToUpper())
                    {
                        cboFLoomWise.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FR_N_PNo".ToUpper())
                    {
                        cboFNewPieceNo.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FP_MLT_CHLN".ToUpper())
                    {
                        cboFMultiChallanBill.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FSI_DSGN_WS".ToUpper())
                    {
                        cboFShowDesign.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FS_BRK_COM".ToUpper())
                    {
                        cboFIBrokerMandatory.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "RTN_FM_STK".ToUpper())
                    {
                        cboFSalesReturn.Text = iDr["GSValue"].ToString();
                    }

                    #endregion

                    #region EMAIL
                    if (iDr["GSName"].ToString().ToUpper() == "EMAIL_SEND_BookInv".ToUpper())
                    {
                        CboEmailBookInv.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "EMAIL_SEND_BookSO".ToUpper())
                    {
                        CboEmailBookSO.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "EMAIL_SEND_DC".ToUpper())
                    {
                        CboEmailDC.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "EMAIL_SEND_DC_Serail".ToUpper())
                    {
                        CboEmailDC_Serial.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "EMAIL_SEND_FabInv".ToUpper())
                    {
                        CboEmailFabInv.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "EMAIL_SEND_FabInv_Cut".ToUpper())
                    {
                        CboEmailFabInv_Cut.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "EMAIL_SEND_FabInv_ROLL".ToUpper())
                    {
                        CboEmailFabInv_Roll.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "EMAIL_SEND_FabInv_Serial".ToUpper())
                    {
                        CboEmailFabInv_Serial.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "EMAIL_SEND_SO".ToUpper())
                    {
                        CboEmailFabSO.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "EMAIL_SEND_FI".ToUpper())
                    {
                        cboEmailFI.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "EMAIL_SEND_SO_ROLL".ToUpper())
                    {
                        CboEmailFabSO_Roll.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "EMAIL_SEND_SO_Serial".ToUpper())
                    {
                        CboEmailFabSO_Serial.Text = iDr["GSValue"].ToString();
                    }
                    #endregion

                    #region SMS
                    if (iDr["GSName"].ToString().ToUpper() == "SMS_SEND_BookInv".ToUpper())
                    {
                        CboSMSBookInv.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "SMS_SEND_BookSO".ToUpper())
                    {
                        CboSMSBookSO.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "SMS_SEND_DC".ToUpper())
                    {
                        CboSMSDC.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "SMS_SEND_DC_Serail".ToUpper())
                    {
                        CboSMSDC_Serial.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "SMS_SEND_FabInv".ToUpper())
                    {
                        CboSMSFabInv.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "SMS_SEND_FabInv_Cut".ToUpper())
                    {
                        CboSMSFabInv_Cut.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "SMS_SEND_FabInv_ROLL".ToUpper())
                    {
                        CboSMSFabInv_Roll.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "SMS_SEND_FabInv_Serial".ToUpper())
                    {
                        CboSMSFabInv_Serial.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "SMS_SEND_SO".ToUpper())
                    {
                        CboSMSFabSO.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "SMS_SEND_FI".ToUpper())
                    {
                        cboSMSFI.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "SMS_SEND_SO_ROLL".ToUpper())
                    {
                        CboSMSFabSO_Roll.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "SMS_SEND_SO_Serial".ToUpper())
                    {
                        CboSMSFabSO_Serial.Text = iDr["GSValue"].ToString();
                    }
                    #endregion

                    #region Other
                    if (iDr["GSName"].ToString().ToUpper() == "FPO_SEND_SMS".ToUpper())
                    {
                        CboSMSFPO.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FPO_SEND_EMAIL".ToUpper())
                    {
                        CboEmailFPO.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FO_BOOKDESIGN".ToUpper())
                    {
                        CboShowFO_BOOKDESIGN.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "PO_SERIAL".ToUpper())
                    {
                        CboPOBookSerial.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FO_SERIAL".ToUpper())
                    {
                        CboFOBookSerial.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FP_CUTWISE".ToUpper())
                    {
                        CboCutwiseFP3.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "INWARD_SERIAL".ToUpper())
                    {
                        CboSerialwiseInward.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "flg_Series".ToUpper())
                    {
                        CboEnableSeries.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FDC_OVERDUE_ALT".ToUpper())
                    {
                        CboOverdueAlertBI.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FP_Pipe_Pnl".ToUpper())
                    {
                        CboPipePanelFP3.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "PO_BOOKDESIGN".ToUpper())
                    {
                        CboShowPO_BOOKDESIGN.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "FT_SERIAL".ToUpper())
                    {
                        CboHideFunc_BS.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "Vld_DupPieceNo".ToUpper())
                    {
                        CboValDupPieceNo.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "IncrtPieceNo_serialsalesReturn".ToUpper())
                    {
                        CboIncPieceNo.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "INC_PNO_STOCK".ToUpper())
                    {
                        CboPieceNoIncStock.Text = iDr["GSValue"].ToString();
                    }
                    #endregion

                    if (iDr["GSName"].ToString().ToUpper() == "PR_FR_DEP".ToUpper())
                    {
                        CboPickStockGodown.Text = iDr["GSValue"].ToString();
                    }

                    if (iDr["GSName"].ToString().ToUpper() == "PR_PNO_JB".ToUpper())
                    {
                        CboFRollWithLoomNo.Text = iDr["GSValue"].ToString();
                    }

                    if (iDr["GSName"].ToString().ToUpper() == "EMBFDC_RATETYPE".ToUpper())
                    {
                        CboFEmbRateType.Text = iDr["GSValue"].ToString();
                    }

                    if (iDr["GSName"].ToString().ToUpper() == "FDC_POGRAMCARDNo".ToUpper())
                    {
                        CboFDC_POGRAMCARDNo.Text = iDr["GSValue"].ToString();
                    }

                    if (iDr["GSName"].ToString().ToUpper() == "FP_ONLYINWARD".ToUpper())
                    {
                        CboFP_ONLYINWARD.Text = iDr["GSValue"].ToString();
                    }

                    if (iDr["GSName"].ToString().ToUpper() == "FP_ORD_WISE".ToUpper())
                    {
                        CboFP_ORD_WISE.Text = iDr["GSValue"].ToString();
                    }

                    if (iDr["GSName"].ToString().ToUpper() == "FP_PipeNo".ToUpper())
                    {
                        CboFP_PipeNo.Text = iDr["GSValue"].ToString();
                    }

                    if (iDr["GSName"].ToString().ToUpper() == "FDC_GRIDENABLE".ToUpper())
                    {
                        CboFDC_GRIDENABLE.Text = iDr["GSValue"].ToString();
                    }

                    if (iDr["GSName"].ToString().ToUpper() == "FAB_DELDG_IN_EH".ToUpper())
                    {
                        txtFAB_DELDG_IN_EH.Text = iDr["GSValue"].ToString();
                    }

                    if (iDr["GSName"].ToString().ToUpper() == "STRT_PNO".ToUpper())
                    {
                        txtStartPNo.Text = iDr["GSValue"].ToString();
                    }
                }
            }
        }

        private void LoadYarnSettingVariables()
        {
            using (IDataReader iDr = DB.GetRS("SELECT Distinct GsName,GSvalue from tbl_GlobalSettings WHERE GType Like 'YARN%' AND CompanyForID=" + Db_Detials.CompID + ""))
            {
                while (iDr.Read())
                {
                    if (iDr["GSName"].ToString().ToUpper() == "Y_RTN_BOX".ToUpper())
                    {
                        cboYRBoxWise.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "YDC".ToUpper())
                    {
                        cboYBagWise.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "YI_OD".ToUpper())
                    {
                        cboYIInternalOrder.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "YR_PO".ToUpper())
                    {
                        CboYProcessOrder.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "MTY_DO".ToUpper())
                    {
                        txtMultiDo.Text = iDr["GSValue"].ToString();
                    }
                }
            }
        }

        private void LoadGeneralVariables()
        {
            using (IDataReader iDr = DB.GetRS("SELECT Distinct GsName,GSvalue from tbl_GlobalSettings WHERE GType Like 'GENERAL%' AND CompanyForID=" + Db_Detials.CompID + ""))
            {
                while (iDr.Read())
                {
                    if (iDr["GSName"].ToString().ToUpper() == "PASAV".ToUpper())
                    {
                        cboPasav.Text = iDr["GSValue"].ToString();
                    }
                }
            }
        }

        private void LoadBeamSettingVariable()
        {
            using (IDataReader iDr = DB.GetRS("SELECT Distinct GsName,GSvalue from tbl_GlobalSettings WHERE GType Like 'Beam%' AND CompanyForID=" + Db_Detials.CompID + ""))
            {
                while (iDr.Read())
                {
                    if (iDr["GSName"].ToString().ToUpper() == "BP_DEP_WS".ToUpper())
                    {
                        cboBProdcutionDepartment.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "BD_ON".ToUpper())
                    {
                        cboBInternalOrder.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "ENABLE_TWEIGHT".ToUpper())
                    {
                        cboBEnableWt.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "BPW_PipeNo".ToUpper())
                    {
                        CboBPW_PipeNo.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "BI_PipeNo".ToUpper())
                    {
                        CboBI_PipeNo.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "BI_GROSSWT".ToUpper())
                    {
                        CboBeamGrossWt.Text = iDr["GSValue"].ToString();
                    }

                    else if (iDr["GSName"].ToString().ToUpper() == "WC_BD".ToUpper())
                    {
                        CboWC_BD.Text = iDr["GSValue"].ToString();
                    }
                }
            }
        }

        private void LoadBrokerModuleVaraibles()
        {
            using (IDataReader iDr = DB.GetRS("SELECT Distinct GsName,GSvalue from tbl_GlobalSettings WHERE GType Like 'Broker%' AND CompanyForID=" + Db_Detials.CompID + ""))
            {
                while (iDr.Read())
                {
                    if (iDr["GSName"].ToString().ToUpper() == "ENABLE_BROKER_CALCMETHOD1".ToUpper())
                    {
                        cboBMEnableAgentCommissionCalcMethod1.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "ENABLE_BROKER_CALCMETHOD2".ToUpper())
                    {
                        cboBMEnableAgentCommissionCalcMethod2.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "ENABLE_BROKER_FABSALESORDER".ToUpper())
                    {
                        cboBMEnableAgentinSalesOrder.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "ENABLE_BROKER_GOODSRECEIPTNOTE".ToUpper())
                    {
                        cboBMEnableAgentinGoodsReceiptNote.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "ENABLE_BROKER_FAB_DLRYCHLN".ToUpper())
                    {
                        cboBMEnableAgentinDeliveryChallan.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "ENABLE_BROKER_FAB_PURCHORDER".ToUpper())
                    {
                        cboBMEnableAgentinPurchaseOrder.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "ENABLE_BROKER_FAB_SALESBILL".ToUpper())
                    {
                        cboBMEnableAgentinSalesBill.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "ENABLE_BROKER_FAB_SALESBILL_CUT".ToUpper())
                    {
                        cboBMEnableAgentinSaleBill_Cut.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "ENABLE_BROKER_FAB_SALESBILLRETURN".ToUpper())
                    {
                        cboBMEnableAgentinSalesReturn.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "ENABLE_BROKER_FAB_SALESBILL_CUTRETURN".ToUpper())
                    {
                        cboBMEnableAgentinSaleBill_CutReturn.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "ENABLE_BROKER_FAB_PURCHASE".ToUpper())
                    {
                        cboBMEnableAgentinPurchase.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "ENABLE_BROKER_FAB_PURCHASERETURN".ToUpper())
                    {
                        cboBMEnableAgentinPurchaseReturn.Text = iDr["GSValue"].ToString();
                    }
                }
            }
        }

        private void LoadBookModuleVaraibles()
        {
            using (IDataReader iDr = DB.GetRS("SELECT Distinct GsName,GSvalue from tbl_GlobalSettings WHERE GType Like 'Book%' AND CompanyForID=" + Db_Detials.CompID + ""))
            {
                while (iDr.Read())
                {
                    if (iDr["GSName"].ToString().ToUpper() == "BI_BRK_COM".ToUpper())
                    {
                        CboBI_BRK_COM.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "BI_ORD_COMP".ToUpper())
                    {
                        CboBI_ORD_COMP.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "BI_ORD_WISE".ToUpper())
                    {
                        CboBI_ORD_WISE.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "BI_OVERDUE_ALT".ToUpper())
                    {
                        CboBI_OVERDUE_ALT.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "BSO_RATETYPE".ToUpper())
                    {
                        CboBSO_RATETYPE.Text = iDr["GSValue"].ToString();
                    }
                }
            }
        }

        private void LoadAccountsModuleVaraibles()
        {
            using (IDataReader iDr = DB.GetRS("SELECT Distinct GsName,GSvalue from tbl_GlobalSettings WHERE GType Like 'Accounts%' AND CompanyForID=" + Db_Detials.CompID + ""))
            {
                while (iDr.Read())
                {
                    if (iDr["GSName"].ToString().ToUpper() == "LED_SHOWTYPE".ToUpper())
                    {
                        CboLED_SHOWTYPE.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "ANS_SE".ToUpper())
                    {
                        CboANS_SE.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "ANS_SOE".ToUpper())
                    {
                        CboANS_SOE.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "CT_REQ_LM".ToUpper())
                    {
                        CboCityReq_LM.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "ST_REQ_LM".ToUpper())
                    {
                        cboStateReq_LM.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "BR_REQ_LM".ToUpper())
                    {
                        cboBrokerReq_LM.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "TR_REQ_LM".ToUpper())
                    {
                        cboTransportReq_LM.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "PS_REQ_LM".ToUpper())
                    {
                        cboPurcSalesReq_LM.Text = iDr["GSValue"].ToString();
                    }
                }
            }
        }

        private void LoadUserRights()
        {
            using (IDataReader iDr = DB.GetRS("SELECT Distinct GsName,GSvalue from tbl_GlobalSettings WHERE GType Like 'USER RIGHTS%' AND CompanyForID=" + Db_Detials.CompID + ""))
            {
                while (iDr.Read())
                {
                    if (iDr["GSName"].ToString().ToUpper() == "APPROVE".ToUpper())
                    {
                        cboApprove.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "AUDIT".ToUpper())
                    {
                        cboAudit.Text = iDr["GSValue"].ToString();
                    }
                    else if (iDr["GSName"].ToString().ToUpper() == "APPLICATIONLOGOFFTIME".ToUpper())
                    {
                        txtApplicationLogOffTime.Text = iDr["GSValue"].ToString();
                    }
                }
            }
        }

        private void tbMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (tbMain.SelectedIndex)
                {
                    case 3:
                        LoadDBSettings();
                        break;

                    case 4:
                        LoadSMSSettingsVariables();
                        break;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboBMEnableAgentCommissionCalcMethod1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboBMEnableAgentCommissionCalcMethod1.Text == "TRUE")
            {
                cboBMEnableAgentCommissionCalcMethod2.Enabled = false;
            }
            else
            {
                cboBMEnableAgentCommissionCalcMethod2.Enabled = true;
            }
        }

        private void cboBMEnableAgentCommissionCalcMethod2_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboBMEnableAgentCommissionCalcMethod2.Text == "TRUE")
            {
                cboBMEnableAgentCommissionCalcMethod1.Enabled = false;
            }
            else
            {
                cboBMEnableAgentCommissionCalcMethod1.Enabled = true;
            }
        }

        private void CmdSavepath_Click(object sender, EventArgs e)
        {
            this.saveBakFile.ShowDialog();
            if (this.saveBakFile.FileName.Trim() != "")
            {
                this.txtlocation.Text = this.saveBakFile.FileName.Replace(".bak", "") + "_" + DateTime.Now.ToString("ddMMyyyy") + ".bak";
            }
            else
            {
                this.openBakFile.ShowDialog();
                this.txtlocation.Text = this.openBakFile.FileName;
            }
        }

        #region Apply Theme To User
        private void LoadThemeSettingsVariables()
        {
            Combobox_Setup.FillCbo(ref cboSelectTheme, Combobox_Setup.ComboType.Mst_ThemeName, "");
        }

        private void ThemeSave()
        {
            string sQry = string.Empty;
            if (cboSelectTheme.SelectedValue != null && cboSelectTheme.SelectedValue.ToString() != "0")
            {
                if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT Count(0) From tbl_ThemeSettings_User Where UserID=" + Db_Detials.UserID)) > 0)
                {
                    sQry = string.Format("Update tbl_ThemeSettings_User SET IsActive = 1, ThemeID=" + cboSelectTheme.SelectedValue + ", ThemeName =" + CommonLogic.SQuote(cboSelectTheme.Text) + " Where UserID = " + Db_Detials.UserID + ";");
                }
                else
                {
                    sQry = string.Format("Insert into tbl_ThemeSettings_User (ThemeID,ThemeName,UserID,IsActive) Values({0},{1},{2},{3});" + Environment.NewLine, new object[] { cboSelectTheme.SelectedValue, CommonLogic.SQuote(cboSelectTheme.Text), Db_Detials.UserID, 1 });
                }
                if(sQry.Length>0)
                DB.ExecuteSQL(sQry);
            }
        }
        #endregion

    }
}
