using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class frmUserRights : frmTrnsIface
    {
        [AccessedThroughProperty("fgDtls")]
        private DataGridViewEx _fgDtls;
        public virtual DataGridViewEx fgDtls
        {
            [DebuggerNonUserCode]
            get
            {
                return this._fgDtls;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                DataGridViewCellCancelEventHandler handler = new DataGridViewCellCancelEventHandler(this.fgDtls_CellBeginEdit);
                if (this._fgDtls != null)
                {
                    this._fgDtls.CellBeginEdit -= handler;
                }
                this._fgDtls = value;
                if (this._fgDtls != null)
                {
                    this._fgDtls.CellBeginEdit += handler;
                }
            }
        }
        private DataTable dtMenutbl;
        private DataTable dtEntrys;
        string IsApprove = "";
        string IsAudit = "";
        private bool isPageLoad;

        public frmUserRights()
        {
            InitializeComponent();
            this.fgDtls = new DataGridViewEx();
        }

        #region Events

        private void frmUserRights_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                isPageLoad = true;
                this.dtMenutbl = DB.GetDT(string.Format("Select Distinct MenuID, ParentID From {0} ", "fn_MenuMaster_Comp()"), false);
                if (this.dtMenutbl.Rows.Count == 0)
                {
                    this.dtMenutbl = DB.GetDT(string.Format("Select Distinct MenuID, ParentID From {0} ", "fn_MenuMaster_tbl()"), false);
                }
                CheckForApproveAudtiRights();
                DetailGrid_Setup.CreateDtlGrid(this, pnlDetail, fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, false);
                Combobox_Setup.FillCbo(ref CboUserType, Combobox_Setup.ComboType.Mst_UserType, "");
                Combobox_Setup.FillCbo(ref cboCompany, Combobox_Setup.ComboType.Mst_Company, "");
                this.fgDtls.TabIndex = 0;
                
                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    FillControls();
                }
                if (!isPageLoad)
                {
                    cboCompany.SelectedValueChanged += new System.EventHandler(cboCompany_SelectedValueChanged);
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void ChkView_CheckStateChanged(object sender, EventArgs e)
        {
            this.Check_UserRights(this.chkView, dgCols.Rights_View);
        }

        private void ChkAdd_CheckStateChanged(object sender, EventArgs e)
        {
            this.Check_UserRights(this.chkAdd, dgCols.Rights_Add);
        }

        private void ChkEdit_CheckStateChanged(object sender, EventArgs e)
        {
            this.Check_UserRights(this.chkEdit, dgCols.Rights_Edit);
        }

        private void ChkDelete_CheckStateChanged(object sender, EventArgs e)
        {
            this.Check_UserRights(this.chkDelete, dgCols.Rights_Delete);
        }

        private void chkPrint_CheckStateChanged(object sender, EventArgs e)
        {
            this.Check_UserRights(this.chkPrint, dgCols.Rights_Print);
        }

        private void chkEmail_CheckStateChanged(object sender, EventArgs e)
        {
            this.Check_UserRights(this.chkEmail, dgCols.Rights_Email);
        }

        private void chkSMS_CheckStateChanged(object sender, EventArgs e)
        {
            this.Check_UserRights(this.chkSMS, dgCols.Rights_SMS);
        }

        private void chkSettings_CheckStateChanged(object sender, EventArgs e)
        {
            this.Check_UserRights(this.chkSettings, dgCols.Rights_Settings);
        }

        #endregion

        #region Navigation

        public void ApplyActStatus()
        {
            if (base.blnFormAction == Enum_Define.ActionType.New_Record)
            {
                ChkActive.Checked = true;
                ChkActive.Visible = false;
            }
            else
            {
                ChkActive.Visible = true;
            }
        }

        public void MovetoField()
        {
            try
            {
                isPageLoad = false;
                txtCode.Text = "";
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                //this.ShowParentIDs(true);
                ApplyActStatus();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "UserRightsId", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, CboUserType, "UserTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboCompany, "CompanyID", Enum_Define.ValidationType.Text);

                lblCUser.Text = DB.GetSnglValue("Select UserName From tbl_UserMaster where UserID=(Select AddedBy from tbl_UserRightsMain Where UserRightsID =" + txtCode.Text + " )");
                lblUUser.Text = DB.GetSnglValue("Select UserName From tbl_UserMaster where UserID=(Select UserID from tbl_UserRightsMain Where UserRightsID =" + txtCode.Text + " )");

                if (cboCompany.SelectedValue != null && cboCompany.SelectedValue.ToString() != "0" && cboCompany.SelectedValue.ToString() != "-" && cboCompany.SelectedValue.ToString() != "System.Data.DataRowView")
                {
                    this.dtMenutbl = DB.GetDT(string.Format("Select Distinct MenuID, ParentID From {0} Where CompanyID =" + cboCompany.SelectedValue, "fn_MenuMaster_Comp()"), false);
                    if (this.dtMenutbl.Rows.Count == 0)
                    {
                        this.dtMenutbl = DB.GetDT(string.Format("Select Distinct MenuID, ParentID From {0}", "fn_MenuMaster_tbl()"), false);
                    }
                }
                if (this.fgDtls.ColumnCount != 0)
                {
                    this.ShowParentIDs(false);
                    CheckForApproveAudtiRights();
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
            ApplyActStatus();
        }

        private void ShowParentIDs(bool IsNewReco)
        {
            try
            {
                chkView.Checked = false;
                chkAdd.Checked = false;
                chkEdit.Checked = false;
                chkDelete.Checked = false;
                chkPrint.Checked = false;
                chkEmail.Checked = false;
                chkSMS.Checked = false;
                chkSettings.Checked = false;
                chkApprove.Checked = false;
                chkAudit.Checked = false;
                if (!IsNewReco)
                {
                    if (cboCompany.SelectedValue != null && cboCompany.SelectedValue.ToString() != "0" && cboCompany.SelectedValue.ToString() != "-")
                    {
                        //this.dtEntrys = DB.GetDT(string.Format("Select Distinct * from {0} AS A Where A.UserRightsID={1} and A.Form_MenuID in (Select Distinct MenuID from tbl_MenuMaster_Comp AS C Where C.CompanyID=" + Db_Detials.CompID + ")", this.fgDtls.Grid_Tbl, this.txtCode.Text), false);
                        this.dtEntrys = DB.GetDT(string.Format("Select Distinct * from {0} AS A Where A.UserRightsID={1} and A.Form_MenuID in (Select Distinct MenuID from fn_MenuMaster_Comp() AS C Where C.CompanyID=" + cboCompany.SelectedValue + ")", this.fgDtls.Grid_Tbl, this.txtCode.Text), false);

                        if (this.dtEntrys.Rows.Count == 0) 
                        {
                            this.dtEntrys = DB.GetDT(string.Format("Select Distinct * from {0} AS A Where A.UserRightsID={1} and A.Form_MenuID in (Select Distinct MenuID from fn_MenuMaster_tbl() AS C)", this.fgDtls.Grid_Tbl, this.txtCode.Text), false);
                        }
                    }
                }

                if (cboCompany.SelectedValue != null && cboCompany.SelectedValue.ToString() != "0" && cboCompany.SelectedValue.ToString() != "-")
                {
                    DataRow[] rowArray = this.dtMenutbl.Select("ParentID = 0");
                    if (rowArray.Length != 0)
                    {
                        fgDtls.Rows.Clear();

                        for (int i = 0; i <= (rowArray.Length - 1); i++)
                        {
                            fgDtls.Rows.Add();
                            DataGridViewRow row = fgDtls.Rows[fgDtls.RowCount - 1];
                            row.Cells[1].Value = i + 1;
                            row.Cells[4].Value = Localization.ParseNativeInt(rowArray[i]["MenuID"].ToString());
                            for (int j = 0; j <= fgDtls.ColumnCount - 1; j++)
                            {
                                row.Cells[j].Style.Font = new Font("Verdana", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
                                row.Cells[j].Style.BackColor = Color.FromArgb(149, 179, 215);
                            }
                            if (!IsNewReco)
                            {
                                try
                                {
                                    DataRow[] rowArray2 = this.dtEntrys.Select("Form_MenuID = " + Localization.ParseNativeInt(rowArray[i]["MenuID"].ToString()));
                                    if (rowArray2.Length > 0)
                                    {
                                        ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_View]).Value = Localization.ParseBoolean(rowArray2[0]["View_Rights"].ToString());
                                        ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_Add]).Value = Localization.ParseBoolean(rowArray2[0]["Add_Rights"].ToString());
                                        ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_Edit]).Value = Localization.ParseBoolean(rowArray2[0]["Edit_Rights"].ToString());
                                        ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_Delete]).Value = Localization.ParseBoolean(rowArray2[0]["Delete_Rights"].ToString());
                                        ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_Print]).Value = Localization.ParseBoolean(rowArray2[0]["Print_Rights"].ToString());
                                        ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_Email]).Value = Localization.ParseBoolean(rowArray2[0]["Email_Rights"].ToString());
                                        ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_SMS]).Value = Localization.ParseBoolean(rowArray2[0]["SMS_Rights"].ToString());
                                        ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_Settings]).Value = Localization.ParseBoolean(rowArray2[0]["Settings_Rights"].ToString());
                                        ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_Approve]).Value = Localization.ParseBoolean(rowArray2[0]["Approve_Rights"].ToString());
                                        ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_Audit]).Value = Localization.ParseBoolean(rowArray2[0]["Audit_Rights"].ToString());
                                    }
                                }
                                catch (Exception ex1)
                                {
                                    Navigate.logError(ex1.Message, ex1.StackTrace);
                                }
                            }
                            row.Cells[3].Value = "P";
                            this.AddChildMenu(Localization.ParseNativeInt(rowArray[i]["MenuID"].ToString()), 1, IsNewReco, false);
                            row = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void AddChildMenu(int ParentID, int iSrno, bool IsNewReco, [Optional, DefaultParameterValue(false)] bool IsNode)
        {
            try
            {
                DataRow[] rowArray = this.dtMenutbl.Select("ParentID = " + ParentID.ToString());
                if (rowArray.Length != 0)
                {
                    for (int i = 0; i <= (rowArray.Length - 1); i++)
                    {
                        if (iSrno == 0)
                        {
                            for (int j = 0; j <= (fgDtls.ColumnCount - 1); j++)
                            {
                                fgDtls.Rows[fgDtls.RowCount - 1].Cells[j].Style.Font = new Font("Verdana", 8f, FontStyle.Bold, GraphicsUnit.Point, 0);
                                fgDtls.Rows[fgDtls.RowCount - 1].Cells[j].Style.BackColor = Color.FromArgb(219, 229, 241);
                            }
                            if (IsNode)
                            {
                                fgDtls.Rows[fgDtls.RowCount - 1].Cells[3].Value = "S";
                            }
                            iSrno++;
                        }
                        fgDtls.Rows.Add();
                        DataGridViewRow row = fgDtls.Rows[fgDtls.RowCount - 1];
                        row.Cells[1].Value = iSrno;
                        row.Cells[2].Value = ParentID;
                        row.Cells[4].Value = Localization.ParseNativeInt(rowArray[i]["MenuID"].ToString());
                        if (!IsNewReco)
                        {
                            try
                            {
                                DataRow[] rowArray2 = this.dtEntrys.Select("Form_MenuID = " + Localization.ParseNativeInt(rowArray[i]["MenuID"].ToString()));
                                if (rowArray2.Length > 0)
                                {
                                    ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_View]).Value = Localization.ParseBoolean(rowArray2[0]["View_Rights"].ToString());
                                    ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_Add]).Value = Localization.ParseBoolean(rowArray2[0]["Add_Rights"].ToString());
                                    ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_Edit]).Value = Localization.ParseBoolean(rowArray2[0]["Edit_Rights"].ToString());
                                    ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_Delete]).Value = Localization.ParseBoolean(rowArray2[0]["Delete_Rights"].ToString());
                                    ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_Print]).Value = Localization.ParseBoolean(rowArray2[0]["Print_Rights"].ToString());
                                    ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_Email]).Value = Localization.ParseBoolean(rowArray2[0]["Email_Rights"].ToString());
                                    ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_SMS]).Value = Localization.ParseBoolean(rowArray2[0]["SMS_Rights"].ToString());
                                    ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_Settings]).Value = Localization.ParseBoolean(rowArray2[0]["Settings_Rights"].ToString());
                                    ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_Approve]).Value = Localization.ParseBoolean(rowArray2[0]["Approve_Rights"].ToString());
                                    ((DataGridViewCheckBoxCell)row.Cells[(int)dgCols.Rights_Audit]).Value = Localization.ParseBoolean(rowArray2[0]["Audit_Rights"].ToString());
                                }
                            }
                            catch (Exception ex1)
                            {
                                Navigate.logError(ex1.Message, ex1.StackTrace);
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex1.Message);
                            }
                        }
                        iSrno++;
                        this.AddChildMenu(Localization.ParseNativeInt(rowArray[i]["MenuID"].ToString()), 0, IsNewReco, true);
                        row = null;
                    }
                }
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
                string strqury = string.Empty;
                ArrayList pArrayData = new ArrayList
                {
                CboUserType.SelectedValue,
                cboCompany.SelectedValue,
                ChkActive.Checked==true?1:0
                };
                //int j = 0;
                try
                {
                    strqury += string.Format("Delete from tbl_MenuMaster_Comp where CompanyID=" + cboCompany.SelectedValue + " and UserTypeID=" + CboUserType.SelectedValue + ";");
                    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    {
                        //DataGridViewCheckBoxCell chkCurrView = (DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[5];
                        //if (Localization.ParseBoolean(chkCurrView.Value.ToString()))
                        {
                            string strMenuid = fgDtls.Rows[i].Cells[4].Value.ToString();
                            strqury += string.Format("Insert into tbl_MenuMaster_Comp (MenuID,ParentID,OrderBy,CompanyID, UserTypeID) (select MenuID,ParentID,OrderBy," + cboCompany.SelectedValue + "," + CboUserType.SelectedValue + "  from tbl_MenuMaster where MenuID=" + strMenuid + ");" + Environment.NewLine);
                        }
                    }
                }
                catch (Exception ex1)
                {
                    Navigate.logError(ex1.Message, ex1.StackTrace);
                }

                strqury += string.Format("Update tbl_MenuMaster_Comp set CompanyID=" + cboCompany.SelectedValue + " where CompanyID is null");
                DBSp.Transcation_AddEdit(pArrayData, fgDtls, true, strqury);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                if (Localization.ParseNativeDouble(CboUserType.SelectedValue.ToString()) <= 0.0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Security Level");
                    CboUserType.Focus();
                    return true;
                }
                if (this.fgDtls.RowCount < 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Details");
                    CboUserType.Focus();
                    return true;
                }
                if (this.fgDtls.RowCount > 0)
                {
                    bool chkBoolean = false;
                    for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                    {
                        DataGridViewCheckBoxCell chk_View = (DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[5];
                        DataGridViewCheckBoxCell chk_Add = (DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[6];
                        DataGridViewCheckBoxCell chk_Edit = (DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[7];
                        DataGridViewCheckBoxCell chk_Delete = (DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[8];
                        DataGridViewCheckBoxCell chk_Print = (DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[9];
                        DataGridViewCheckBoxCell chk_Email = (DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[10];
                        DataGridViewCheckBoxCell chk_SMS = (DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[11];
                        DataGridViewCheckBoxCell chk_Settings = (DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[12];
                        DataGridViewCheckBoxCell chk_Approve = (DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[13];
                        DataGridViewCheckBoxCell chk_Audit = (DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[14];

                        if ((chk_View.Value == null ? false : Localization.ParseBoolean(chk_View.Value.ToString()) == true) || (chk_Add.Value == null ? false : Localization.ParseBoolean(chk_Add.Value.ToString()) == true) || (chk_Edit.Value == null ? false : Localization.ParseBoolean(chk_Edit.Value.ToString()) == true) || (chk_Delete.Value == null ? false : Localization.ParseBoolean(chk_Delete.Value.ToString()) == true) || (chk_Print.Value == null ? false : Localization.ParseBoolean(chk_Print.Value.ToString()) == true) || (chk_Email.Value == null ? false : Localization.ParseBoolean(chk_Email.Value.ToString()) == true) || (chk_SMS.Value == null ? false : Localization.ParseBoolean(chk_SMS.Value.ToString()) == true) || (chk_Settings.Value == null ? false : Localization.ParseBoolean(chk_Settings.Value.ToString()) == true) || (chk_Approve.Value == null ? false : Localization.ParseBoolean(chk_Approve.Value.ToString()) == true) || (chk_Audit.Value == null ? false : Localization.ParseBoolean(chk_Audit.Value.ToString()) == true))
                        {
                            chkBoolean = true;
                            break;
                        }
                        else
                            chkBoolean = false;
                    }
                    if (chkBoolean == false)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select at least one User Right");
                        CboUserType.Focus();
                        return true;
                    }
                }


                if (DBSp.rtnAction())
                {
                    //strTable = "tbl_UserRightsMain";
                    //if (Navigate.CheckDuplicate(ref strTable, "UserType", CboUserType.SelectedValue.ToString(), false, "", 0L, "", ""))
                    //{
                    //    CboUserType.Focus();
                    //    return true;
                    //}
                }
                else
                {
                    //strTable = "tbl_UserRightsMain";
                    //if (Navigate.CheckDuplicate(ref strTable, "UserType", CboUserType.SelectedValue.ToString(), true, "UserRightsID", Localization.ParseNativeLong(txtCode.Text.Trim()), "", ""))
                    //{
                    //    CboUserType.Focus();
                    //    return true;
                    //}
                }
                return false;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                return true;
            }
        }

        #endregion

        private void Check_UserRights(CheckBox objCheckBox, dgCols Col_Index)
        {
            try
            {
                for (int i = 0; i <= (fgDtls.Rows.Count - 1); i++)
                {
                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[(int)Col_Index];
                    cell.Value = objCheckBox.Checked;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_CellBeginEdit(object sender, System.Windows.Forms.DataGridViewCellCancelEventArgs e)
        {
            try
            {
                if ((fgDtls.Rows[e.RowIndex].Cells[(int)dgCols.ParentType].Value != null))
                {
                    if (fgDtls.Rows[e.RowIndex].Cells[(int)dgCols.ParentType].Value.ToString() == "S")
                    {
                        for (int i = e.RowIndex - 1; i >= 0; i += -1)
                        {
                            if ((fgDtls.Rows[i].Cells[(int)dgCols.ParentType].Value != null))
                            {
                                if (fgDtls.Rows[i].Cells[(int)dgCols.ParentType].Value.ToString() == "P")
                                {

                                    if (fgDtls.Rows[e.RowIndex].Cells[(int)dgCols.ParentID].Value == fgDtls.Rows[i].Cells[(int)dgCols.Form_MenuID].Value)
                                    {
                                        DataGridViewCheckBoxCell chkCurr = (DataGridViewCheckBoxCell)fgDtls.Rows[e.RowIndex].Cells[e.ColumnIndex];
                                        DataGridViewCheckBoxCell chkprt = (DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[e.ColumnIndex];
                                        if (chkprt.Value.ToString() == "True")
                                        {
                                            if (chkCurr.Selected == false)
                                            {
                                                if (chkprt.Selected == true)
                                                    e.Cancel = true;
                                            }
                                            else
                                            {
                                                CheckUnCheck(Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[(int)dgCols.Form_MenuID].Value.ToString()), e.RowIndex + 1, e.ColumnIndex);
                                            }
                                            return;
                                        }
                                        else
                                        {
                                            e.Cancel = true;
                                            return;
                                        }

                                    }
                                }
                            }
                        }
                    }
                }

                if (((e.ColumnIndex == Localization.ParseNativeInt(dgCols.Rights_View.ToString())) || (e.ColumnIndex == (int)dgCols.Rights_Add) || (e.ColumnIndex == (int)dgCols.Rights_Edit) || (e.ColumnIndex == (int)dgCols.Rights_Delete)) && (fgDtls.Rows[e.RowIndex].Cells[(int)dgCols.ParentID].Value != null))
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)fgDtls.Rows[e.RowIndex].Cells[e.ColumnIndex];
                    if (!(chk.HasStyle == true))
                    {
                        //for (int i = e.RowIndex - 1; i >= 0; i += -1)
                        //{

                        //    if (fgDtls.Rows[e.RowIndex].Cells[(int)dgCols.ParentID].Value != fgDtls.Rows[i].Cells[(int)dgCols.ParentID].Value)
                        //    {
                        //        DataGridViewCheckBoxCell chkCurr = (DataGridViewCheckBoxCell)fgDtls.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        //        DataGridViewCheckBoxCell chkprt = (DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[e.ColumnIndex];
                        //        if (chkprt.Value == "True")
                        //        {
                        //            if (chkCurr.Selected == false)
                        //                if (chkprt.Selected == true)
                        //                    e.Cancel = true;
                        //            return;
                        //        }
                        //        else
                        //        {
                        //            e.Cancel = true;
                        //            return;
                        //        }
                        //    }
                        //}
                    }
                }
                else
                {
                    if (fgDtls.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                        return;
                    CheckUnCheck(Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[(int)dgCols.Form_MenuID].Value.ToString()), e.RowIndex + 1, e.ColumnIndex);
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void CheckUnCheck(int iParentID, int iCurrRow, int iCurrCol)
        {
            try
            {
                for (int i = iCurrRow; i <= fgDtls.RowCount - 1; i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    if (Localization.ParseNativeInt(row.Cells[2].Value.ToString()) == iParentID)
                    {
                        row.Cells[iCurrCol].Value = "False";
                        if (iParentID != Localization.ParseNativeInt(row.Cells[2].Value.ToString()))
                        {
                            CheckUnCheck(Localization.ParseNativeInt(row.Cells[2].Value.ToString()), i + 1, iCurrCol);
                        }
                        else if (Operators.ConditionalCompareObjectEqual(row.Cells[3].Value, "S", false))
                        {
                            CheckUnCheck(Localization.ParseNativeInt(row.Cells[4].Value.ToString()), i + 1, iCurrCol);
                        }
                    }
                    row = null;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private enum dgCols
        {
            Form_MenuID = 4,
            ParentID = 2,
            ParentType = 3,
            Rights_Add = 6,
            Rights_Delete = 8,
            Rights_Edit = 7,
            Rights_View = 5,
            Rights_Approve = 13,
            Rights_Audit = 14,
            Rights_Print = 9,
            Rights_Email = 10,
            Rights_SMS = 11,
            Rights_Settings = 12,
            Srno = 1
        }

        private void chkAudit_CheckStateChanged(object sender, EventArgs e)
        {
            this.Check_UserRights(this.chkAudit, dgCols.Rights_Audit);
        }

        private void chkApprove_CheckStateChanged(object sender, EventArgs e)
        {
            this.Check_UserRights(this.chkApprove, dgCols.Rights_Approve);
        }

        private void CheckForApproveAudtiRights()
        {
            try
            {
                string StrQry = string.Empty;
                IsApprove = GlobalVariables.APPROVE;
                IsAudit = GlobalVariables.AUDIT;

                if (IsApprove == "TRUE")
                {
                    StrQry += string.Format("Update tbl_GridSettings Set IsHidden=0 Where GridID=" + iIDentity + " and ColFields='Approve_Rights';");
                    chkApprove.Visible = true;
                }
                if (IsApprove == "FALSE")
                {
                    StrQry += string.Format("Update tbl_GridSettings Set IsHidden=1 Where GridID=" + iIDentity + " and ColFields='Approve_Rights';");
                    chkApprove.Visible = false;
                }
                if (IsAudit == "TRUE")
                {
                    StrQry += string.Format("Update tbl_GridSettings Set IsHidden=0 Where GridID=" + iIDentity + " and ColFields='Audit_Rights';");
                    chkAudit.Visible = true;
                }
                if (IsAudit == "FALSE")
                {
                    StrQry += string.Format("Update tbl_GridSettings Set IsHidden=1 Where GridID=" + iIDentity + " and ColFields='Audit_Rights';");
                    chkAudit.Visible = false;
                }
                if(StrQry.Length>0)
                DB.ExecuteSQL(StrQry);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboCompany_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboCompany.SelectedValue != null && cboCompany.SelectedValue.ToString() != "0" && cboCompany.SelectedValue.ToString() != "-" && cboCompany.SelectedValue.ToString() != "System.Data.DataRowView")
            {
                this.dtMenutbl = DB.GetDT(string.Format("Select Distinct MenuID, ParentID From {0} Where CompanyID =" + cboCompany.SelectedValue, "fn_MenuMaster_Comp()"), false);
                if (this.dtMenutbl.Rows.Count ==0)
                {
                    this.dtMenutbl = DB.GetDT(string.Format("Select Distinct MenuID, ParentID From {0}", "fn_MenuMaster_tbl()"), false);
                }
                ShowParentIDs(true);
            }
        }
    }
}
