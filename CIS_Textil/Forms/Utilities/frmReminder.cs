using System;
using System.Collections;
using CIS_DBLayer;
using CIS_Bussiness;
using Microsoft.VisualBasic;

namespace CIS_Textil
{
    public partial class frmReminder : frmMasterIface
    {
        public frmReminder()
        {
            InitializeComponent();
        }

        #region Event

        private void frmReminder_Load(object sender, EventArgs e)
        {
            try
            {
                if (txtCode.Text == "")
                {
                    if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                    {
                        FillControls();
                    }
                }
                else
                {
                    base.blnFormAction = Enum_Define.ActionType.View_Record;
                    FillControls();
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }

        }

        #endregion

        #region Navigation

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "ReminderID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtTopic, "Topic", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtFromDt, "FromDt", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtToDt, "TODt", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
            }
            catch (Exception ex) 
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        public void MovetoField()
        {
            CommonCls.IncFieldID(this, ref txtCode, "");
        }

        public void SaveRecord()
        {
            try
            {
                ArrayList pArrayData = new ArrayList
                {
                    txtTopic.Text.Trim(),
                    Localization.ToSqlDateString(txtFromDt.Text.Trim()),
                    Localization.ToSqlDateString(txtToDt.Text.Trim()),
                    (ChkActive.Checked == true ? 1 : 0),
                    Db_Detials.UserID
                };

                sComboAddText = txtTopic.Text.Trim();
                DBSp.Master_AddEdit(pArrayData, "");
            }
            catch (Exception ex) 
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Error", ex.Message);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                if (txtTopic.Text.Trim().Length <= 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Topic");
                    txtTopic.Focus();
                    return true;
                }

                if (!Information.IsDate(txtFromDt.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please enter valid date");
                    txtFromDt.Focus();
                    return true;
                }

                if (!Information.IsDate(txtToDt.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please enter valid date");
                    txtToDt.Focus();
                    return true;
                }

                return false;
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                return false;
            }
        }

        #endregion

        private int _iReminderID;
        public int iReminderID
        { 
             get { return _iReminderID; }
            set { _iReminderID = value; }
        }
    }
}
