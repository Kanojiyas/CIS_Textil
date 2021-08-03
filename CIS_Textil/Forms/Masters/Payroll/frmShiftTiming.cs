using System;
using System.Data;
using CIS_Bussiness;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmShiftTiming : frmMasterIface
    {
        public frmShiftTiming()
        {
            InitializeComponent();
        }

        private void frmShiftTiming_Load(object sender, EventArgs e)
        {
            try
            {
                FetchData();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }

        }

        #region FormNavigation

        public void FillControls()
        {
            try
            {
                FetchData();
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
                FetchData();
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
                string StrDtlQry = string.Empty;

                #region General
               
                if (chkGeneralShift.Checked == true)
                {
                    if (DB.GetSnglValue("select count(0) from tbl_ShiftSetting where ShiftName = 'General'") == "0")
                    {
                        StrDtlQry = string.Format("Insert Into tbl_ShiftSetting ([ShiftName],[StartTime],[EndTime],[Duration],[ShiftCharges],[IsAct],[CompID],[YearID]) values(" + CommonLogic.SQuote("General") + "," + CommonLogic.SQuote(cboGenHours.SelectedItem + ":" + cboGenMinit.SelectedItem + ":00 " + cboGenAmpm.SelectedItem) + "," + CommonLogic.SQuote(cboGenEndHours.SelectedItem + ":" + cboGenEndMinit.SelectedItem + ":00 " + cboGenEndAmpm.SelectedItem) + "," + CommonLogic.SQuote(txtGenDur.Text.Trim()) + "," + "NULL" + "," + (chkGeneralShift.Checked ? 1 : 0) + "," + Db_Detials.CompID + "," + Db_Detials.YearID + ");");
                        DB.ExecuteSQL(StrDtlQry);
                    }
                    else
                    {
                        StrDtlQry = string.Format("Update tbl_ShiftSetting Set ShiftName={0},StartTime={1},EndTime={2},Duration={3},ShiftCharges={4},IsAct={5},CompID={6},YearID={7} where ShiftName={8};" + Environment.NewLine,
                                                    CommonLogic.SQuote("General"), CommonLogic.SQuote(cboGenHours.SelectedItem + ":" + cboGenMinit.SelectedItem + ":00 " + cboGenAmpm.SelectedItem),
                                                    CommonLogic.SQuote(cboGenEndHours.SelectedItem + ":" + cboGenEndMinit.SelectedItem + ":00 " + cboGenEndAmpm.SelectedItem), CommonLogic.SQuote(txtGenDur.Text.Trim()),
                                                    "NULL", chkGeneralShift.Checked ? 1 : 0, Db_Detials.CompID, Db_Detials.YearID, CommonLogic.SQuote("General"));

                        DB.ExecuteSQL(StrDtlQry);
                    }

                }

                #endregion

                #region Morning
                
                if (chkMorShift.Checked == true)
                {
                    if (DB.GetSnglValue("select count(0) from tbl_ShiftSetting where ShiftName = 'Morning'") == "0")
                    {
                        StrDtlQry = string.Format("Insert Into tbl_ShiftSetting ([ShiftName],[StartTime],[EndTime],[Duration],[ShiftCharges],[IsAct],[CompID],[YearID]) values(" + CommonLogic.SQuote("Morning") + "," + CommonLogic.SQuote(cboMorHours.SelectedItem + ":" + cboMorMinit.SelectedItem + ":00 " + cboMorAmPm.SelectedItem) + "," + CommonLogic.SQuote(cboMorEndHours.SelectedItem + ":" + cboMorEndMinit.SelectedItem + ":00 " + cboMorEndAmpm.SelectedItem) + "," + CommonLogic.SQuote(txtMorDur.Text.Trim()) + "," + txtMorSC.Text.Trim() + "," + (chkMorShift.Checked ? 1 : 0) + "," + Db_Detials.CompID + "," + Db_Detials.YearID + ");");
                        DB.ExecuteSQL(StrDtlQry);
                    }
                    else
                    {
                        StrDtlQry = string.Format("Update tbl_ShiftSetting Set ShiftName={0},StartTime={1},EndTime={2},Duration={3},ShiftCharges={4},IsAct={5},CompID={6},YearID={7} where ShiftName={8};" + Environment.NewLine,
                                                             CommonLogic.SQuote("Morning"), CommonLogic.SQuote(cboMorHours.SelectedItem + ":" + cboMorMinit.SelectedItem + ":00 " + cboMorAmPm.SelectedItem),
                                                             CommonLogic.SQuote(cboMorEndHours.SelectedItem + ":" + cboMorEndMinit.SelectedItem + ":00 " + cboMorEndAmpm.SelectedItem), CommonLogic.SQuote(txtMorDur.Text.Trim()),
                                                             txtMorSC.Text.Trim(), chkMorShift.Checked ? 1 : 0, Db_Detials.CompID, Db_Detials.YearID, CommonLogic.SQuote("Morning"));

                        DB.ExecuteSQL(StrDtlQry);
                    }

                }

                #endregion

                #region AfterNoon
               
                if (chkAftShift.Checked == true)
                {
                    if (DB.GetSnglValue("select count(0) from tbl_ShiftSetting where ShiftName = 'Afternoon'") == "0")
                    {
                        StrDtlQry = string.Format("Insert Into tbl_ShiftSetting ([ShiftName],[StartTime],[EndTime],[Duration],[ShiftCharges],[IsAct],[CompID],[YearID]) values(" + CommonLogic.SQuote("Afternoon") + "," + CommonLogic.SQuote(cboAfterNoonHours.SelectedItem + ":" + cboAfterNoonMinit.SelectedItem + ":00 " + cboAfterNoonAmpm.SelectedItem) + "," + CommonLogic.SQuote(cboAfterNoonEndHours.SelectedItem + ":" + cboAfterNoonEndMinit.SelectedItem + ":00 " + cboAterNoonEndAmpm.SelectedItem) + "," + CommonLogic.SQuote(txtAftDur.Text.Trim()) + "," + txtAftSC.Text.Trim() + "," + (chkAftShift.Checked ? 1 : 0) + "," + Db_Detials.CompID + "," + Db_Detials.YearID + ");");
                        DB.ExecuteSQL(StrDtlQry);
                    }
                    else
                    {
                        StrDtlQry = string.Format("Update tbl_ShiftSetting Set ShiftName={0},StartTime={1},EndTime={2},Duration={3},ShiftCharges={4},IsAct={5},CompID={6},YearID={7} where ShiftName={8};" + Environment.NewLine,
                                                    CommonLogic.SQuote("Afternoon"), CommonLogic.SQuote(cboAfterNoonHours.SelectedItem + ":" + cboAfterNoonMinit.SelectedItem + ":00 " + cboAfterNoonAmpm.SelectedItem),
                                                    CommonLogic.SQuote(cboAfterNoonEndHours.SelectedItem + ":" + cboAfterNoonEndMinit.SelectedItem + ":00 " + cboAterNoonEndAmpm.SelectedItem), CommonLogic.SQuote(txtAftDur.Text.Trim()),
                                                    txtAftSC.Text.Trim(), chkAftShift.Checked ? 1 : 0, Db_Detials.CompID, Db_Detials.YearID, CommonLogic.SQuote("Afternoon"));

                        DB.ExecuteSQL(StrDtlQry);
                    }
                }

                #endregion

                #region Evening

                if (chkEveShift.Checked == true)
                {
                    if (DB.GetSnglValue("select count(0) from tbl_ShiftSetting where ShiftName = 'Evening'") == "0")
                    {
                        StrDtlQry = string.Format("Insert Into tbl_ShiftSetting ([ShiftName],[StartTime],[EndTime],[Duration],[ShiftCharges],[IsAct],[CompID],[YearID]) values(" + CommonLogic.SQuote("Evening") + "," + CommonLogic.SQuote(cboEveningHours.SelectedItem + ":" + cboEveningMinit.SelectedItem + ":00 " + cboEveningAmpm.SelectedItem) + "," + CommonLogic.SQuote(cboEveningEndHours.SelectedItem + ":" + cboEveningEndMinit.SelectedItem + ":00 " + cboEveningEndAmPm.SelectedItem) + "," + CommonLogic.SQuote(txtEveDur.Text.Trim()) + "," + txtEveSC.Text.Trim() + "," + (chkEveShift.Checked ? 1 : 0) + "," + Db_Detials.CompID + "," + Db_Detials.YearID + ");");
                        DB.ExecuteSQL(StrDtlQry);
                    }
                    else
                    {
                        StrDtlQry = string.Format("Update tbl_ShiftSetting Set ShiftName={0},StartTime={1},EndTime={2},Duration={3},ShiftCharges={4},IsAct={5},CompID={6},YearID={7} where ShiftName={8};" + Environment.NewLine,
                                        CommonLogic.SQuote("Evening"), CommonLogic.SQuote(cboEveningHours.SelectedItem + ":" + cboEveningMinit.SelectedItem + ":00 " + cboEveningAmpm.SelectedItem),
                                        CommonLogic.SQuote(cboEveningEndHours.SelectedItem + ":" + cboEveningEndMinit.SelectedItem + ":00 " + cboEveningEndAmPm.SelectedItem), CommonLogic.SQuote(txtEveDur.Text.Trim()),
                                        txtEveSC.Text.Trim(), chkEveShift.Checked ? 1 : 0, Db_Detials.CompID, Db_Detials.YearID, CommonLogic.SQuote("Evening"));

                        DB.ExecuteSQL(StrDtlQry);
                    }
                }

                #endregion

                #region Night

                if (chkNgtShift.Checked == true)
                {
                    if (DB.GetSnglValue("select count(0) from tbl_ShiftSetting where ShiftName = 'Night'") == "0")
                    {
                        StrDtlQry = string.Format("Insert Into tbl_ShiftSetting ([ShiftName],[StartTime],[EndTime],[Duration],[ShiftCharges],[IsAct],[CompID],[YearID]) values(" + CommonLogic.SQuote("Night") + "," + CommonLogic.SQuote(cboNighHours.SelectedItem + ":" + cboNightMinit.SelectedItem + ":00 " + cboNightAmPm.SelectedItem) + "," + CommonLogic.SQuote(cboNightEndHours.SelectedItem + ":" + cboNightEndMinit.SelectedItem + ":00 " + cboNightEndAmPm.SelectedItem) + "," + CommonLogic.SQuote(txtNgtDur.Text.Trim()) + "," + txtNgtSC.Text.Trim() + "," + (chkNgtShift.Checked ? 1 : 0) + "," + Db_Detials.CompID + "," + Db_Detials.YearID + ");");
                        DB.ExecuteSQL(StrDtlQry);
                    }
                    else
                    {
                        StrDtlQry = string.Format("Update tbl_ShiftSetting Set ShiftName={0},StartTime={1},EndTime={2},Duration={3},ShiftCharges={4},IsAct={5},CompID={6},YearID={7} where ShiftName={8};" + Environment.NewLine,
                                      CommonLogic.SQuote("Night"), CommonLogic.SQuote(cboNighHours.SelectedItem + ":" + cboNightMinit.SelectedItem + ":00 " + cboNightAmPm.SelectedItem),
                                      CommonLogic.SQuote(cboNightEndHours.SelectedItem + ":" + cboNightEndMinit.SelectedItem + ":00 " + cboNightEndAmPm.SelectedItem), CommonLogic.SQuote(txtNgtDur.Text.Trim()),
                                      txtNgtSC.Text.Trim(), chkNgtShift.Checked ? 1 : 0, Db_Detials.CompID, Db_Detials.YearID, CommonLogic.SQuote("Night"));

                        DB.ExecuteSQL(StrDtlQry);
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                if (chkGeneralShift.Checked==true)
                {
                    if (cboGenHours.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Start Hours For General Shift");
                        cboGenHours.Focus();
                        return true;
                    }

                    if (cboGenMinit.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Start Minit For General Shift");
                        cboGenMinit.Focus();
                        return true;
                    }

                    if(cboGenAmpm.SelectedItem ==null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Start Am/Pm For General Shift");
                        cboGenAmpm.Focus();
                        return true;
                    }

                    if(cboGenEndHours.SelectedItem==null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select End Hours For General Shift");
                        cboGenEndHours.Focus();
                        return true;
                    }

                    if(cboGenEndMinit.SelectedItem==null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select End Mint For General Shift");
                        cboGenEndMinit.Focus();
                        return true;    
                    }

                    if(cboGenEndAmpm.SelectedItem==null)
                    {
                         Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select End Am/Pm For General Shift");
                         cboGenEndAmpm.Focus();
                         return true;    
                    }
                }

                if (chkMorShift.Checked==true)
                {
                    if (cboMorHours.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Start Hours For Morning Shift");
                        cboMorHours.Focus();
                        return true;
                    }

                    if (cboMorMinit.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Start Minit For Morning Shift");
                        cboMorMinit.Focus();
                        return true;
                    }

                    if (cboMorAmPm.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Start Am/Pm For Morning Shift");
                        cboMorAmPm.Focus();
                        return true;
                    }

                    if (cboMorEndHours.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select End Hours For Morning Shift");
                        cboMorEndHours.Focus();
                        return true;
                    }

                    if (cboMorEndMinit.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select End Mint For Morning Shift");
                        cboMorEndMinit.Focus();
                        return true;
                    }

                    if (cboMorEndAmpm.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select End Am/Pm For Morning Shift");
                        cboMorEndAmpm.Focus();
                        return true;
                    }
                }

                if (chkAftShift.Checked == true)
                {
                    if (cboAfterNoonHours.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Start Hours For Afternoon Shift");
                        cboAfterNoonHours.Focus();
                        return true;
                    }

                    if (cboAfterNoonMinit.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Start Minit For Afternoon Shift");
                        cboAfterNoonMinit.Focus();
                        return true;
                    }

                    if (cboAfterNoonAmpm.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Start Am/Pm For Afternoon Shift");
                        cboAfterNoonAmpm.Focus();
                        return true;
                    }

                    if (cboAfterNoonEndHours.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select End Hours For Afternoon Shift");
                        cboAfterNoonEndHours.Focus();
                        return true;
                    }

                    if (cboAfterNoonEndMinit.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select End Mint For Afternoon Shift");
                        cboAfterNoonEndMinit.Focus();
                        return true;
                    }

                    if (cboAterNoonEndAmpm.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select End Am/Pm For Afternoon Shift");
                        cboAterNoonEndAmpm.Focus();
                        return true;
                    }
                }

                if (chkEveShift.Checked == true)
                {
                    if (cboEveningHours.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Start Hours For Evening Shift");
                        cboEveningHours.Focus();
                        return true;
                    }

                    if (cboEveningMinit.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Start Minit For Evening Shift");
                        cboEveningMinit.Focus();
                        return true;
                    }

                    if (cboEveningAmpm.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Start Am/Pm For Evening Shift");
                        cboEveningAmpm.Focus();
                        return true;
                    }

                    if (cboEveningEndHours.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select End Hours For Evening Shift");
                        cboEveningEndHours.Focus();
                        return true;
                    }

                    if (cboEveningEndMinit.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select End Mint For Evening Shift");
                        cboEveningEndMinit.Focus();
                        return true;
                    }

                    if (cboEveningEndAmPm.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select End Am/Pm For Evening Shift");
                        cboEveningEndAmPm.Focus();
                        return true;
                    }
                }

                if (chkNgtShift.Checked == true)
                {
                    if (cboNighHours.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Start Hours For Night Shift");
                        cboNighHours.Focus();
                        return true;
                    }

                    if (cboNightMinit.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Start Minit For Night Shift");
                        cboNightMinit.Focus();
                        return true;
                    }

                    if (cboNightAmPm.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Start Am/Pm For Night Shift");
                        cboNightAmPm.Focus();
                        return true;
                    }

                    if (cboNightEndHours.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select End Hours For Night Shift");
                        cboNightEndHours.Focus();
                        return true;
                    }

                    if (cboNightEndMinit.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select End Mint For Night Shift");
                        cboNightEndMinit.Focus();
                        return true;
                    }

                    if (cboNightEndAmPm.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select End Am/Pm For Night Shift");
                        cboNightEndAmPm.Focus();
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                return false;
            }
        }

        #endregion FormNavigation

        private void chkGeneralShift_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGeneralShift.Checked)
            {
                cboGenHours.Enabled = true;
                cboGenMinit.Enabled = true;
                cboGenAmpm.Enabled = true;
                cboGenEndHours.Enabled = true;
                cboGenEndMinit.Enabled = true;
                cboGenEndAmpm.Enabled = true;
            }
            else
            {
                cboGenHours.Enabled = false;
                cboGenMinit.Enabled = false;
                cboGenAmpm.Enabled = false;
                cboGenEndHours.Enabled = false;
                cboGenEndMinit.Enabled = false;
                cboGenEndAmpm.Enabled = false;
            }
        }

        private void chkMorShift_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMorShift.Checked)
            {
                cboMorHours.Enabled = true;
                cboMorMinit.Enabled = true;
                cboMorAmPm.Enabled = true;
                cboMorEndHours.Enabled = true;
                cboMorEndMinit.Enabled = true;
                cboMorEndAmpm.Enabled = true;
                txtMorSC.Enabled = true;
            }
            else
            {
                cboMorHours.Enabled = false;
                cboMorMinit.Enabled = false;
                cboMorAmPm.Enabled = false;
                cboMorEndHours.Enabled = false;
                cboMorEndMinit.Enabled = false;
                cboMorEndAmpm.Enabled = false;
                txtMorSC.Enabled = false;
            }
        }

        private void chkAftShift_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAftShift.Checked)
            {
                cboAfterNoonHours.Enabled = true;
                cboAfterNoonMinit.Enabled = true;
                cboAfterNoonAmpm.Enabled = true;
                cboAfterNoonEndHours.Enabled = true;
                cboAfterNoonEndMinit.Enabled = true;
                cboAterNoonEndAmpm.Enabled = true;
                txtAftSC.Enabled = true;
            }
            else
            {
                cboAfterNoonHours.Enabled = false;
                cboAfterNoonMinit.Enabled = false;
                cboAfterNoonAmpm.Enabled = false;
                cboAfterNoonEndHours.Enabled = false;
                cboAfterNoonEndMinit.Enabled = false;
                cboAterNoonEndAmpm.Enabled = false;
                txtAftSC.Enabled = false;
            }
        }

        private void chkEveShift_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEveShift.Checked)
            {
                cboEveningHours.Enabled = true;
                cboEveningMinit.Enabled = true;
                cboEveningAmpm.Enabled = true;
                cboEveningEndHours.Enabled = true;
                cboEveningEndMinit.Enabled = true;
                cboEveningEndAmPm.Enabled = true;
                txtEveSC.Enabled = true;
            }
            else
            {
                cboEveningHours.Enabled = false;
                cboEveningMinit.Enabled = false;
                cboEveningAmpm.Enabled = false;
                cboEveningEndHours.Enabled = false;
                cboEveningEndMinit.Enabled = false;
                cboEveningEndAmPm.Enabled = false;
                txtEveSC.Enabled = false;
            }
        }

        private void chkNgtShift_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNgtShift.Checked)
            {
                cboNighHours.Enabled = true;
                cboNightMinit.Enabled = true;
                cboNightAmPm.Enabled = true;
                cboNightEndHours.Enabled = true;
                cboNightEndMinit.Enabled = true;
                cboNightEndAmPm.Enabled = true;
                txtNgtSC.Enabled = true;
            }
            else
            {
                cboNighHours.Enabled = false;
                cboNightMinit.Enabled = false;
                cboNightAmPm.Enabled = false;
                cboNightEndHours.Enabled = false;
                cboNightEndMinit.Enabled = false;
                cboNightEndAmPm.Enabled = false;
                txtNgtSC.Enabled = false;
            }
        }

        private void chkSelectAll_Click(object sender, EventArgs e)
        {
            if (chkSelectAll.Checked==true)
            {
                chkGeneralShift.Checked = true;
                chkMorShift.Checked = true;
                chkAftShift.Checked = true;
                chkEveShift.Checked = true;
                chkNgtShift.Checked = true;
            }
            else
            {
                chkGeneralShift.Checked = false;
                chkMorShift.Checked = false;
                chkAftShift.Checked = false;
                chkEveShift.Checked = false;
                chkNgtShift.Checked = false;
            }
        }

        public void CalculateDuration(int ival)
        {
            string Hours=string.Empty;
            string Minit=string.Empty;
            string Ampm = string.Empty;
            string EndHours=string.Empty;
            string EndMinit=string.Empty;
            string EndAmpm = string.Empty;
            string StratTime=string.Empty;
            string EndTime = string.Empty;
            string diffDt = string.Empty;
            double TimeHr=0.0;

            if (ival==1)
            {
                if ((cboGenHours.SelectedItem != null) &&(cboGenMinit.SelectedItem != null) && (cboGenAmpm.SelectedItem != null) && (cboGenEndHours.SelectedItem != null) && (cboGenEndMinit.SelectedItem != null) && (cboGenEndAmpm.SelectedItem != null))
                {
                    Hours = cboGenHours.SelectedItem.ToString();
                    Minit = cboGenMinit.SelectedItem.ToString();
                    Ampm = cboGenAmpm.SelectedItem.ToString();

                    EndHours = cboGenEndHours.SelectedItem.ToString();
                    EndMinit = cboGenEndMinit.SelectedItem.ToString();
                    EndAmpm = cboGenEndAmpm.SelectedItem.ToString();

                    StratTime = Hours + ':' + Minit + ' ' + Ampm;
                    EndTime = EndHours + ':' + EndMinit + ' ' + EndAmpm;

                    diffDt = DB.GetSnglValue(string.Format("Select datediff(SECOND,'" + StratTime + "','" + EndTime + "')"));

                    TimeHr = Convert.ToDouble(diffDt) / 3600;
                    txtGenDur.Text = TimeHr.ToString();
                }
                else
                { 
                     
                }
            }
            else if(ival==2)
            {
                if ((cboMorHours.SelectedItem != null) && (cboMorMinit.SelectedItem != null) && (cboMorAmPm.SelectedItem != null) && (cboMorEndHours.SelectedItem != null) && (cboMorEndMinit.SelectedItem != null) && (cboMorEndAmpm.SelectedItem != null))
                {
                    Hours = cboMorHours.SelectedItem.ToString();
                    Minit = cboMorMinit.SelectedItem.ToString();
                    Ampm = cboMorAmPm.SelectedItem.ToString();

                    EndHours = cboMorEndHours.SelectedItem.ToString();
                    EndMinit = cboMorEndMinit.SelectedItem.ToString();
                    EndAmpm = cboMorEndAmpm.SelectedItem.ToString();

                    StratTime = Hours + ':' + Minit + ' ' + Ampm;
                    EndTime = EndHours + ':' + EndMinit + ' ' + EndAmpm;

                    diffDt = DB.GetSnglValue(string.Format("Select datediff(SECOND,'" + StratTime + "','" + EndTime + "')"));

                    TimeHr = Convert.ToDouble(diffDt) / 3600;
                    txtMorDur.Text = TimeHr.ToString();
                }
                else
                { 
                
                }
            }

            else if (ival==3)
            {
                if ((cboAfterNoonHours.SelectedItem != null) && (cboAfterNoonMinit.SelectedItem != null) && (cboAfterNoonAmpm.SelectedItem != null) && (cboAfterNoonEndHours.SelectedItem != null) && (cboAfterNoonEndMinit.SelectedItem != null) && (cboAterNoonEndAmpm.SelectedItem != null))
                {
                    Hours = cboAfterNoonHours.SelectedItem.ToString();
                    Minit = cboAfterNoonMinit.SelectedItem.ToString();
                    Ampm = cboAfterNoonAmpm.SelectedItem.ToString();

                    EndHours = cboAfterNoonEndHours.SelectedItem.ToString();
                    EndMinit = cboAfterNoonEndMinit.SelectedItem.ToString();
                    EndAmpm = cboAterNoonEndAmpm.SelectedItem.ToString();

                    StratTime = Hours + ':' + Minit + ' ' + Ampm;
                    EndTime = EndHours + ':' + EndMinit + ' ' + EndAmpm;

                    diffDt = DB.GetSnglValue(string.Format("Select datediff(SECOND,'" + StratTime + "','" + EndTime + "')"));

                    TimeHr = Convert.ToDouble(diffDt) / 3600;
                    txtAftDur.Text = TimeHr.ToString();
                }
                else
                { 
                
                }
            }
            else if (ival==4)
            {
                if ((cboEveningHours.SelectedItem != null) && (cboEveningMinit.SelectedItem != null) && (cboEveningAmpm.SelectedItem != null) && (cboEveningEndHours.SelectedItem != null) && (cboEveningEndMinit.SelectedItem != null) && (cboEveningEndAmPm.SelectedItem != null))
                {
                    Hours = cboEveningHours.SelectedItem.ToString();
                    Minit = cboEveningMinit.SelectedItem.ToString();
                    Ampm = cboEveningAmpm.SelectedItem.ToString();

                    EndHours = cboEveningEndHours.SelectedItem.ToString();
                    EndMinit = cboEveningEndMinit.SelectedItem.ToString();
                    EndAmpm = cboEveningEndAmPm.SelectedItem.ToString();

                    StratTime = Hours + ':' + Minit + ' ' + Ampm;
                    EndTime = EndHours + ':' + EndMinit + ' ' + EndAmpm;

                    diffDt = DB.GetSnglValue(string.Format("Select datediff(SECOND,'" + StratTime + "','" + EndTime + "')"));

                    TimeHr = Convert.ToDouble(diffDt) / 3600;
                    txtEveDur.Text = TimeHr.ToString();
                }
            
            }
            else if (ival==5)
            {
                if ((cboNighHours.SelectedItem != null) && (cboNightMinit.SelectedItem != null) && (cboNightAmPm.SelectedItem != null) && (cboNightEndHours.SelectedItem != null) && (cboNightEndMinit.SelectedItem != null) && (cboNightEndAmPm.SelectedItem != null))
                {
                    Hours = cboNighHours.SelectedItem.ToString();
                    Minit = cboNightMinit.SelectedItem.ToString();
                    Ampm = cboNightAmPm.SelectedItem.ToString();

                    EndHours = cboNightEndHours.SelectedItem.ToString();
                    EndMinit = cboNightEndMinit.SelectedItem.ToString();
                    EndAmpm = cboNightEndAmPm.SelectedItem.ToString();

                    StratTime = Hours + ':' + Minit + ' ' + Ampm;
                    EndTime = EndHours + ':' + EndMinit + ' ' + EndAmpm;

                    diffDt = DB.GetSnglValue(string.Format("Select datediff(SECOND,'" + StratTime + "','" + EndTime + "')"));

                    TimeHr = Convert.ToDouble(diffDt) / 3600;
                    txtNgtDur.Text = TimeHr.ToString();
                }
            }
            else
            { 
            
            }

        }

        private void cboGenHours_SelectedValueChanged(object sender, EventArgs e)
        {
            CalculateDuration(1);
        }

        private void cboMorHours_SelectedValueChanged(object sender, EventArgs e)
        {
            CalculateDuration(2);
        }

        private void cboAfterNoonHours_SelectedValueChanged(object sender, EventArgs e)
        {
            CalculateDuration(3);
        }

        private void cboEveningHours_SelectedValueChanged(object sender, EventArgs e)
        {
            CalculateDuration(4);
        }

        private void cboNighHours_SelectedValueChanged(object sender, EventArgs e)
        {
            CalculateDuration(5);
        }

        public void FetchData()
        {
            using (IDataReader iDr = DB.GetRS("Select * from fn_ShiftSettingView()"))
            {
                while (iDr.Read())
                {
                    string ShiftName = iDr["ShiftName"].ToString();

                    string[] StrStart;
                    string[] StrEnd;
                    string StrTime = string.Empty;
                    string StrEndTime;
                    string hours = string.Empty;
                    string minit = string.Empty;
                    string Ampm = string.Empty;

                    string Endhours = string.Empty;
                    string Endminit = string.Empty;
                    string EndAmpm = string.Empty;

                    if (ShiftName == "General")
                    {
                        StrTime = iDr["StartTime"].ToString();
                        StrStart = StrTime.Split(':');
                        hours = StrStart[0].ToString();
                        minit = StrStart[1].ToString();
                        Ampm = StrStart[2].ToString();

                        cboGenHours.SelectedItem = string.Format("{0:00}", Localization.ParseNativeInt(hours));
                        cboGenMinit.SelectedItem = minit;
                        cboGenAmpm.SelectedItem = Ampm.Trim();

                        StrEndTime = iDr["EndTime"].ToString();
                        StrEnd = StrEndTime.Split(':');
                        Endhours = StrEnd[0].ToString();
                        Endminit = StrEnd[1].ToString();
                        EndAmpm = StrEnd[2].ToString();

                        cboGenEndHours.SelectedItem = string.Format("{0:00}", Localization.ParseNativeInt(Endhours));
                        cboGenEndMinit.SelectedItem = Endminit;
                        cboGenEndAmpm.SelectedItem = EndAmpm.Trim();

                        txtGenDur.Text = iDr["Duration"].ToString();
                        chkGeneralShift.Checked = Localization.ParseBoolean(iDr["IsAct"].ToString());
                    }
                    if (ShiftName == "Morning")
                    {

                        StrTime = iDr["StartTime"].ToString();
                        StrStart = StrTime.Split(':');
                        hours = StrStart[0].ToString();
                        minit = StrStart[1].ToString();
                        Ampm = StrStart[2].ToString();

                        cboMorHours.SelectedItem = string.Format("{0:00}", Localization.ParseNativeInt(hours));
                        cboMorMinit.SelectedItem = minit;
                        cboMorAmPm.SelectedItem = Ampm.Trim();

                        StrEndTime = iDr["EndTime"].ToString();
                        StrEnd = StrEndTime.Split(':');
                        Endhours = StrEnd[0].ToString();
                        Endminit = StrEnd[1].ToString();
                        EndAmpm = StrEnd[2].ToString();

                        cboMorEndHours.SelectedItem = string.Format("{0:00}", Localization.ParseNativeInt(Endhours));
                        cboMorEndMinit.SelectedItem = Endminit;
                        cboMorEndAmpm.SelectedItem = EndAmpm.Trim();

                        txtMorDur.Text = iDr["Duration"].ToString();
                        txtMorSC.Text = iDr["ShiftCharges"].ToString();
                        chkMorShift.Checked = Localization.ParseBoolean(iDr["IsAct"].ToString());

                    }
                    if (ShiftName == "Afternoon")
                    {
                        StrTime = iDr["StartTime"].ToString();
                        StrStart = StrTime.Split(':');
                        hours = StrStart[0].ToString();
                        minit = StrStart[1].ToString();
                        Ampm = StrStart[2].ToString();

                        cboAfterNoonHours.SelectedItem = string.Format("{0:00}", Localization.ParseNativeInt(hours));
                        cboAfterNoonMinit.SelectedItem = minit;
                        cboAfterNoonAmpm.SelectedItem = Ampm.Trim();

                        StrEndTime = iDr["EndTime"].ToString();
                        StrEnd = StrEndTime.Split(':');
                        Endhours = StrEnd[0].ToString();
                        Endminit = StrEnd[1].ToString();
                        EndAmpm = StrEnd[2].ToString();

                        cboAfterNoonEndHours.SelectedItem = string.Format("{0:00}", Localization.ParseNativeInt(Endhours));
                        cboAfterNoonEndMinit.SelectedItem = Endminit;
                        cboAterNoonEndAmpm.SelectedItem = EndAmpm.Trim();

                        txtAftDur.Text = iDr["Duration"].ToString();
                        txtAftSC.Text = iDr["ShiftCharges"].ToString();
                        chkAftShift.Checked = Localization.ParseBoolean(iDr["IsAct"].ToString());
                    }

                    if (ShiftName == "Evening")
                    {
                        StrTime = iDr["StartTime"].ToString();
                        StrStart = StrTime.Split(':');
                        hours = StrStart[0].ToString();
                        minit = StrStart[1].ToString();
                        Ampm = StrStart[2].ToString();

                        cboEveningHours.SelectedItem = string.Format("{0:00}", Localization.ParseNativeInt(hours));
                        cboEveningMinit.SelectedItem = minit;
                        cboEveningAmpm.SelectedItem = Ampm.Trim();

                        StrEndTime = iDr["EndTime"].ToString();
                        StrEnd = StrEndTime.Split(':');
                        Endhours = StrEnd[0].ToString();
                        Endminit = StrEnd[1].ToString();
                        EndAmpm = StrEnd[2].ToString();

                        cboEveningEndHours.SelectedItem = string.Format("{0:00}", Localization.ParseNativeInt(Endhours));
                        cboEveningEndMinit.SelectedItem = Endminit;
                        cboEveningEndAmPm.SelectedItem = EndAmpm.Trim();

                        txtEveDur.Text = iDr["Duration"].ToString();
                        txtEveSC.Text = iDr["ShiftCharges"].ToString();
                        chkEveShift.Checked = Localization.ParseBoolean(iDr["IsAct"].ToString());
                    }
                    if (ShiftName == "Night")
                    {
                        StrTime = iDr["StartTime"].ToString();
                        StrStart = StrTime.Split(':');
                        hours = StrStart[0].ToString();
                        minit = StrStart[1].ToString();
                        Ampm = StrStart[2].ToString();

                        cboNighHours.SelectedItem = string.Format("{0:00}", Localization.ParseNativeInt(hours));
                        cboNightMinit.SelectedItem = minit;
                        cboNightAmPm.SelectedItem = Ampm.Trim();

                        StrEndTime = iDr["EndTime"].ToString();
                        StrEnd = StrEndTime.Split(':');
                        Endhours = StrEnd[0].ToString();
                        Endminit = StrEnd[1].ToString();
                        EndAmpm = StrEnd[2].ToString();

                        cboNightEndHours.SelectedItem = string.Format("{0:00}", Localization.ParseNativeInt(Endhours));
                        cboNightEndMinit.SelectedItem = Endminit;
                        cboNightEndAmPm.SelectedItem = EndAmpm.Trim();

                        txtNgtDur.Text = iDr["Duration"].ToString();
                        txtNgtSC.Text = iDr["ShiftCharges"].ToString();
                        chkNgtShift.Checked = Localization.ParseBoolean(iDr["IsAct"].ToString());
                    }
                }
            }
        }
    }
}
