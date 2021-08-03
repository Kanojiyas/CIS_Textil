using System;
using System.Data;
using CIS_DBLayer;
using CIS_Bussiness;

namespace CIS_Textil
{
    public partial class frmToDoList : frmMasterIface
    {
        string sTaskType;
        private int TaskID;
        public frmToDoList()
        {
            InitializeComponent();
            sTaskType = "";
        }

        #region Event

        private void frmToDoList_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboPriority, Combobox_Setup.ComboType.Mst_priority, "");
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

        #endregion

        #region Navigation

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "TaskAssignID", Enum_Define.ValidationType.Text);

                using (DataTable Dt = DB.GetDT("SELECT * from tbl_TaskAssignmentDtls WHERE TaskAssignID = " + txtCode.Text, false))
                {
                    if (Dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in Dt.Rows)
                        {
                            cboPriority.SelectedValue = r["Priority"].ToString();
                            TaskID = Localization.ParseNativeInt(r["TaskID"].ToString());
                            txtTopic.Text = DB.GetSnglValue("SELECT TaskName from tbl_TaskMasterMain WHERE TaskID = " + r["TaskID"].ToString());
                            ChkActive.Checked = (Localization.ParseNativeDouble(r["PerCompleted"].ToString()) < 100 ? false : true);
                        }
                    }
                }

            }
            catch (Exception ex) 
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        public void MovetoField()
        {
            try
            {
                CommonCls.IncFieldID(this, ref txtCode, "");
                txtTopic.Text = "";
                cboPriority.SelectedValue = "0";
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
                int iDefaultTaskType = Localization.ParseNativeInt(DB.GetSnglValue("SELECT TaskTypeID FROM tbl_TaskTypeMaster WHERE TaskTypeName like '%TODOLIST%'"));
                if (iDefaultTaskType == 0)
                {
                    string sTaskType_Qry = string.Format("INSERT INTO tbl_TaskTypeMaster VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21})",
                        "'TODOLIST'", "NULL", 1, Db_Detials.CompID, Db_Detials.YearID, "getdate()", Db_Detials.UserID, 0, "NULL", "NULL", 0, "NULL", "NULL", 0, "NULL", "NULL", 0, "NULL", "NULL", 0, "NULL", "NULL");

                    DB.ExecuteSQL(sTaskType_Qry);
                    sTaskType = DB.GetSnglValue("SELECT COUNT(0) FROM tbl_TaskTypeMaster WHERE TaskTypeName like '%TODOLIST%'");
                }
                else
                    sTaskType = iDefaultTaskType.ToString();


                #region INSERT INTO TaskMaster
                string sTaskMstr_Qry = "";
                if (txtCode.Text.Trim() == "")
                {
                    sTaskMstr_Qry = string.Format("INSERT INTO tbl_TaskMasterMain VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22})",
                                 sTaskType, CommonLogic.SQuote(txtTopic.Text.Trim()), "NULL", 1, Db_Detials.CompID, Db_Detials.YearID, "getdate()", Db_Detials.UserID, 0, "NULL", "NULL", 0, "NULL", "NULL", 0, "NULL", "NULL", 0, "NULL", "NULL", 0, "NULL", "NULL");
                    TaskID = 0;
                }
                else
                {
                    sTaskMstr_Qry = string.Format("UPDATE tbl_TaskMasterMain SET TaskName={0} WHERE TaskID={1}", CommonLogic.SQuote(txtTopic.Text), TaskID);
                }

                DB.ExecuteSQL(sTaskMstr_Qry);
                if (TaskID == 0)
                    TaskID = Localization.ParseNativeInt(DB.GetSnglValue("SELECT TOP 1 TaskID from tbl_TaskMasterMain ORDER BY TaskID DESC"));

                #endregion

                #region INSERT INTO Task Assignment
                string sTaskAssignment_QryMain = "";
                if (txtCode.Text.Trim() == "")
                {
                    sTaskAssignment_QryMain = string.Format("INSERT INTO tbl_TaskAssignmentMain VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20})",
                                 "getdate()", Db_Detials.UserID, Db_Detials.CompID, Db_Detials.YearID, "getdate()", Db_Detials.UserID, 0, "NULL", "NULL", 0, "NULL", "NULL", 0, "NULL", "NULL", 0, "NULL", "NULL", 0, "NULL", "NULL");
                }

                string sTaskAssignment_QryDtls = "";
                if (txtCode.Text.Trim() != "")
                {
                    sTaskAssignment_QryDtls = "DELETE FROM tbl_TaskAssignmentDtls WHERE TaskAssignID = " + txtCode.Text.Trim() + Environment.NewLine;
                }

                sTaskAssignment_QryDtls += string.Format("INSERT INTO tbl_TaskAssignmentDtls VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14})",
                    (txtCode.Text.Trim() != "" ? txtCode.Text : "(#CodeID#)"), 1, TaskID, 0, Db_Detials.UserID, CommonLogic.SQuote(Localization.ToSqlDateString(DateTime.Now.ToString())), "getdate()", CommonLogic.SQuote(Localization.ToSqlDateString(DateTime.Now.ToString())), "getdate()",
                    0, cboPriority.SelectedValue, (ChkActive.Checked ? 100 : 0), "NULL", 0, 0);

                if (sTaskAssignment_QryMain.Length > 0)
                    DB.ExecuteTranscation(sTaskAssignment_QryMain, txtCode.Text, true, sTaskAssignment_QryDtls);
                else
                    DB.ExecuteSQL(sTaskAssignment_QryDtls);

                txtTopic.Text = "";
                txtCode.Text = "";
                cboPriority.SelectedValue = "0";
                #endregion

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
