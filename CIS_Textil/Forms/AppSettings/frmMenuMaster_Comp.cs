using CIS_DataGridViewEx;
using  CIS_Bussiness;using CIS_DBLayer;
using CIS_CLibrary;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CIS_Textil
{
    public partial class frmMenuMaster_Comp : frmMasterIface
    {
        [AccessedThroughProperty("fgDtls")]
        private DataGridViewEx _fgDtls;
        public ArrayList OrgInGridArray;

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
        private DataTable dtMenutbl_Comp;
        private DataTable dtcopy;

        public frmMenuMaster_Comp()
        {
            InitializeComponent();
            this.fgDtls = new DataGridViewEx();
        }

        #region Events

        private void frmMenuMaster_Comp_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                //if (cboCompany.SelectedValue != null)

                DetailGrid_Setup.CreateDtlGrid(this, pnlDetail, fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0,false);
                Combobox_Setup.FillCbo(ref cboCompany, Combobox_Setup.ComboType.Mst_Company, "");
                this.fgDtls.TabIndex = 0;

                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    FillControls();
                }
                this.cboCompany.SelectedValueChanged += new System.EventHandler(this.cboCompany_SelectedValueChanged);
                btnPaste.Visible = false;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void ChkView_CheckStateChanged(object sender, EventArgs e)
        {
            this.Check_UserRights(this.chkSelectAll, dgCols.Select);
        }

        #endregion

        #region Navigation

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                //this.dtMenutbl = DB.GetDT(string.Format("Select Distinct MenuID,ParentID,OrderBy,Menu_Caption,Form_Caption,ToolTip,TblName_Main,PmryColumn,SearchQry,SearchQry_Dtls,FormCall,FormCall_Web,FormType,MenuType,IsForm,IsSeparator,ApplyYear,IsVisible  From {0} Where MenuID Not In (7,8,9,10,11) And IsSeparator = 0", "tbl_MenuMaster"), false);
                fgDtls.Rows.Clear();
                ShowParentIDs(true);
                OrgInGridArray = new ArrayList();
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
                //DBValue.Return_DBValue(this, txtCode, "MenuID", Enum_Define.ValidationType.Text);
                //DBValue.Return_DBValue(this, cboCompany, "CompanyID", Enum_Define.ValidationType.Text);

                lblCUser.Text = DB.GetSnglValue("Select UserName From tbl_UserMaster where UserID=(Select AddedBy from tbl_UserRightsMain Where UserRightsID =" + txtCode.Text + " )");
                lblUUser.Text = DB.GetSnglValue("Select UserName From tbl_UserMaster where UserID=(Select UserID from tbl_UserRightsMain Where UserRightsID =" + txtCode.Text + " )");

                if (this.fgDtls.ColumnCount != 0)
                {
                    this.ShowParentIDs(true);
                }
                OrgInGridArray.Clear();
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void ShowParentIDs(bool IsNewReco)
        {
            try
            {
                chkSelectAll.Checked = false;

                if (!IsNewReco)
                {
                    if (cboCompany.SelectedValue != null)
                        this.dtEntrys = DB.GetDT(string.Format("Select Distinct MenuID,ParentID,OrderBy,Menu_Caption,Form_Caption,ToolTip,TblName_Main,PmryColumn,SearchQry,SearchQry_Dtls,FormCall,FormCall_Web,FormType,MenuType,VoucherTypeID,RefMenuID,IsForm,IsSeparator,ApplyYear,IsVisible,CompanyID,UserTypeID  From {0} Where MenuID = {1} and CompanyID=" + cboCompany.SelectedValue + " ", "fn_MenuMaster_Comp()", this.txtCode.Text), false);
                    //else
                    //  this.dtEntrys = DB.GetDT(string.Format("Select Distinct MenuID,ParentID,OrderBy,Menu_Caption,Form_Caption,ToolTip,TblName_Main,PmryColumn,SearchQry,SearchQry_Dtls,FormCall,FormCall_Web,FormType,MenuType,IsForm,IsSeparator,ApplyYear,IsVisible  From {0} Where MenuID = {1} ", "tbl_MenuMaster", this.txtCode.Text), false);
                }

                //DataRow[] rowArray;
                //if (cboCompany.SelectedValue != null && cboCompany.SelectedValue.ToString() != "0")
                //{
                //    rowArray = this.dtMenutbl.Select("ParentID = 0");
                //}
                //else
                //{
                //    rowArray = this.dtMenutbl.Select("ParentID = 0");
                //}

                //if (rowArray.Length != 0)
                //{
                //    fgDtls.Rows.Clear();
                //    fgDtls.ColumnCount = 20;

                //    for (int i = 0; i <= (rowArray.Length - 1); i++)
                //    {
                //        //if(fgDtls.Rows.Count!=0)
                //        fgDtls.Rows.Add();
                //        DataGridViewRow row = fgDtls.Rows[fgDtls.RowCount - 1];
                //        row.Cells[1].Value = i + 1;
                //        row.Cells[0].Value = Localization.ParseNativeInt(rowArray[i]["MenuID"].ToString());
                //        row.Cells[3].Value = Localization.ParseNativeInt(rowArray[i]["OrderBy"].ToString());
                //        row.Cells[4].Value = rowArray[i]["Menu_Caption"].ToString();
                //        row.Cells[5].Value = (rowArray[i]["Form_Caption"].ToString());
                //        row.Cells[6].Value = (rowArray[i]["ToolTip"].ToString());
                //        row.Cells[7].Value = (rowArray[i]["TblName_Main"].ToString());
                //        row.Cells[8].Value = (rowArray[i]["PmryColumn"].ToString());
                //        row.Cells[9].Value = (rowArray[i]["SearchQry"].ToString());
                //        row.Cells[10].Value = (rowArray[i]["SearchQry_Dtls"].ToString());
                //        row.Cells[11].Value = (rowArray[i]["FormCall"].ToString());
                //        row.Cells[12].Value = (rowArray[i]["FormCall_Web"].ToString());
                //        row.Cells[13].Value = Localization.ParseNativeInt(rowArray[i]["FormType"].ToString());
                //        row.Cells[14].Value = Localization.ParseNativeInt(rowArray[i]["MenuType"].ToString());
                //        row.Cells[15].Value = Localization.ParseBoolean(rowArray[i]["IsForm"].ToString());
                //        row.Cells[16].Value = Localization.ParseBoolean(rowArray[i]["IsSeparator"].ToString());
                //        row.Cells[17].Value = Localization.ParseBoolean(rowArray[i]["ApplyYear"].ToString());
                //        row.Cells[18].Value = Localization.ParseBoolean(rowArray[i]["IsVisible"].ToString());
                //        row.Cells[19].Value = false;

                //        for (int j = 0; j <= fgDtls.ColumnCount - 1; j++)
                //        {
                //            row.Cells[j].Style.Font = new Font("Verdana", 9f, FontStyle.Bold, GraphicsUnit.Point, 0);
                //            row.Cells[j].Style.BackColor = Color.FromArgb(149, 179, 215);
                //        }
                //        if (!IsNewReco)
                //        {
                //            try
                //            {
                //                DataRow[] rowArray2 = this.dtEntrys.Select("MenuID = " + Localization.ParseNativeInt(rowArray[i]["MenuID"].ToString()));
                //                if (rowArray2.Length > 0)
                //                {
                //                    ((DataGridViewCheckBoxCell)row.Cells[19]).Value = Localization.ParseBoolean(rowArray2[0]["chkSelect"].ToString());
                //                }
                //            }
                //            catch (Exception ex)
                //            {
                //                Navigate.logError(ex.Message, ex.StackTrace);
                //            }
                //        }
                //        row.Cells[14].Value = "P";
                //        this.AddChildMenu(Localization.ParseNativeInt(rowArray[i]["MenuID"].ToString()), 1, IsNewReco, false);
                //        row = null;
                //    }
                //}
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
                DataRow[] rowArray;
                if (cboCompany.SelectedValue == null || cboCompany.SelectedValue.ToString() == "0")
                    rowArray = this.dtMenutbl.Select("ParentID = " + ParentID.ToString());
                else
                    rowArray = this.dtMenutbl.Select("ParentID = " + ParentID.ToString() + "");

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
                                fgDtls.Rows[fgDtls.RowCount - 1].Cells[14].Value = "S";
                            }
                            iSrno++;
                        }
                        fgDtls.Rows.Add();
                        DataGridViewRow row = fgDtls.Rows[fgDtls.RowCount - 1];
                        row.Cells[1].Value = iSrno;
                        row.Cells[2].Value = ParentID;
                        row.Cells[0].Value = rowArray[i]["MenuID"].ToString();
                        row.Cells[3].Value = Localization.ParseNativeInt(rowArray[i]["OrderBy"].ToString());
                        row.Cells[4].Value = rowArray[i]["Menu_Caption"].ToString();
                        row.Cells[5].Value = (rowArray[i]["Form_Caption"].ToString());
                        row.Cells[6].Value = (rowArray[i]["ToolTip"].ToString());
                        row.Cells[7].Value = (rowArray[i]["TblName_Main"].ToString());
                        row.Cells[8].Value = (rowArray[i]["PmryColumn"].ToString());
                        row.Cells[9].Value = (rowArray[i]["SearchQry"].ToString());
                        row.Cells[10].Value = (rowArray[i]["SearchQry_Dtls"].ToString());
                        row.Cells[11].Value = (rowArray[i]["FormCall"].ToString());
                        row.Cells[12].Value = (rowArray[i]["FormCall_Web"].ToString());
                        row.Cells[13].Value = Localization.ParseNativeInt(rowArray[i]["FormType"].ToString());
                        row.Cells[14].Value = Localization.ParseNativeInt(rowArray[i]["MenuType"].ToString());
                        row.Cells[15].Value = Localization.ParseBoolean(rowArray[i]["IsForm"].ToString());
                        row.Cells[16].Value = Localization.ParseBoolean(rowArray[i]["IsSeparator"].ToString());
                        row.Cells[17].Value = Localization.ParseBoolean(rowArray[i]["ApplyYear"].ToString());
                        row.Cells[18].Value = Localization.ParseBoolean(rowArray[i]["IsVisible"].ToString());
                        row.Cells[19].Value = false;
                        if (!IsNewReco)
                        {
                            try
                            {
                                DataRow[] rowArray2 = this.dtEntrys.Select("MenuID = " + Localization.ParseNativeInt(rowArray[i]["MenuID"].ToString()));
                                if (rowArray2.Length > 0)
                                {
                                    ((DataGridViewCheckBoxCell)row.Cells[19]).Value = Localization.ParseBoolean(rowArray2[0]["chkSelect"].ToString());
                                }
                            }
                            catch (Exception ex)
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
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
                //ArrayList pArrayData = new ArrayList
                //{
                //cboCompany.SelectedValue,
                //ChkActive.Checked==true?1:0
                //};
                int j = 0;
                try
                {
                    strqury += string.Format("Delete from tbl_MenuMaster_Comp where CompanyID=" + cboCompany.SelectedValue + ";");
                    for (int i = 0; i < fgDtls.Rows.Count; i++)
                    {
                        //DataGridViewCheckBoxCell chkCurrView = (DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[19];
                        //if (Localization.ParseBoolean(chkCurrView.Value.ToString()))
                        {
                            string strMenuid = fgDtls.Rows[i].Cells[0].Value.ToString();

                            if (strMenuid != "0")
                            {
                                strqury += string.Format(" Insert into tbl_MenuMaster_Comp (MenuID,ParentID,OrderBy,CompanyID,UserTypeID) values ({0},{1},{2},{3},{4});" + Environment.NewLine,
                                    fgDtls.Rows[i].Cells[0].Value,fgDtls.Rows[i].Cells[2].Value == null ? "''" : fgDtls.Rows[i].Cells[2].Value.ToString() == "" ? "''" : fgDtls.Rows[i].Cells[2].Value.ToString(),
                                    CommonLogic.SQuote(fgDtls.Rows[i].Cells[3].Value.ToString()),
                                    cboCompany.SelectedValue, Db_Detials.UserType);
                            }
                        }
                    }
                }
                catch (Exception ex2)
                {
                    Navigate.logError(ex2.Message, ex2.StackTrace);
                }

                strqury += string.Format(" Update tbl_MenuMaster_Comp set CompanyID=" + cboCompany.SelectedValue + " where CompanyID is null");
                try
                {
                    DB.ExecuteSQL(strqury);
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecuritySuccess, "Success", "Record Saved Successfully");
                }
                catch (Exception ex1)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Error", "Error While Saving Record");
                    Navigate.logError(ex1.Message, ex1.StackTrace);
                }
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
                if (Localization.ParseNativeDouble(cboCompany.SelectedValue.ToString()) <= 0.0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Company");
                    cboCompany.Focus();
                    return true;
                }

                if (this.fgDtls.RowCount > 0)
                {
                    bool chkBoolean = false;
                    for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                    {
                        DataGridViewCheckBoxCell chk_View = (DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[19];

                        if ((chk_View.Value == null ? false : Localization.ParseBoolean(chk_View.Value.ToString()) == true))
                        {
                            chkBoolean = true;
                            break;
                        }
                        else
                            chkBoolean = false;
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
                if ((fgDtls.Rows[e.RowIndex].Cells[(int)dgCols.MenuType].Value != null))
                {
                    if (fgDtls.Rows[e.RowIndex].Cells[(int)dgCols.MenuType].Value.ToString() == "S")
                    {
                        for (int i = e.RowIndex - 1; i >= 0; i += -1)
                        {
                            if ((fgDtls.Rows[i].Cells[(int)dgCols.MenuType].Value != null))
                            {
                                if (fgDtls.Rows[i].Cells[(int)dgCols.MenuType].Value.ToString() == "P")
                                {

                                    if (fgDtls.Rows[e.RowIndex].Cells[(int)dgCols.ParentID].Value == fgDtls.Rows[i].Cells[(int)dgCols.MenuID].Value)
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
                                                CheckUnCheck(Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[(int)dgCols.MenuID].Value.ToString()), e.RowIndex + 1, e.ColumnIndex);
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

                if (((e.ColumnIndex == Localization.ParseNativeInt(dgCols.Select.ToString()))))
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
                    CheckUnCheck(Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[(int)dgCols.MenuID].Value.ToString()), e.RowIndex + 1, e.ColumnIndex);
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
                            CheckUnCheck(Localization.ParseNativeInt(row.Cells[19].Value.ToString()), i + 1, iCurrCol);
                        }
                        else if (Operators.ConditionalCompareObjectEqual(row.Cells[14].Value, "S", false))
                        {
                            CheckUnCheck(Localization.ParseNativeInt(row.Cells[19].Value.ToString()), i + 1, iCurrCol);
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
            MenuID = 0,
            SrNo = 1,
            ParentID = 2,
            OrderBy = 3,
            Menu_Caption = 4,
            Form_Caption = 5,
            ToolTip = 6,
            TableName = 7,
            PmryColumn = 8,
            SearchQry = 9,
            SearchQry_Dtls = 10,
            FormCall = 11,
            FormCall_Web = 12,
            FormType = 13,
            MenuType = 14,
            IsForm = 15,
            IsSeperator = 16,
            ApplyYear = 17,
            IsVisible = 18,
            Select = 19
        }

        private void chkAudit_CheckStateChanged(object sender, EventArgs e)
        {
            this.Check_UserRights(this.chkSelectAll, dgCols.Select);
        }

        private void tsbtn_Up_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = fgDtls.CurrentCell.RowIndex;
                //DataGridViewRow currentRow = fgDtls.CurrentRow;
                DataGridViewRow currentRow = fgDtls.Rows[rowIndex];
                int iParentID = Localization.ParseNativeInt(currentRow.Cells[2].Value.ToString());
                if (rowIndex == 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "This is first column can't move up...");
                }
                else if (currentRow.Cells[3].Value.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "This is first column in this ParentID can't move up...");
                }
                else
                {
                    string scellvalue = currentRow.Cells[4].Value.ToString();
                    currentRow.Cells[4].Value = fgDtls.Rows[rowIndex -1].Cells[4].Value;
                    fgDtls.Rows[rowIndex - 1].Cells[4].Value = scellvalue;
                    fgDtls.CurrentCell = fgDtls[4, rowIndex - 1];
                    //fgDtls.Rows.Remove(currentRow);
                    //fgDtls.Rows.Insert(rowIndex - 1, currentRow);
                    //fgDtls.CurrentCell = fgDtls[1, rowIndex - 1];
                    //this.SetOrderBy(iParentID);
                }
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void SetOrderBy(int parentid)
        {
            try
            {
                int num2 = fgDtls.RowCount - 1;
                for (int i = 0; i <= num2; i++)
                {
                    if (Localization.ParseNativeInt(fgDtls.Rows[i].Cells[2].Value.ToString()) == parentid)
                    {
                        fgDtls.Rows[i].Cells[3].Value = i;
                    }
                    //fgDtls.Rows[i].Cells[1].Value = i;
                }
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void tsbtn_Down_Click(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = fgDtls.CurrentCell.RowIndex;
                //DataGridViewRow currentRow = fgDtls.CurrentRow;
                DataGridViewRow currentRow = fgDtls.Rows[rowIndex];
                int iParentID = Localization.ParseNativeInt(currentRow.Cells[2].Value.ToString());

                if (rowIndex == (this.fgDtls.RowCount - 1))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "This is last column can't move down...");
                }
                else if (currentRow.Cells[3].Value.ToString() == DB.GetSnglValue("Select Max(Orderby) from tbl_MenuMaster where parentid=" + currentRow.Cells[2].Value.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "This is last column in this ParentID can't move down...");
                }
                else
                {
                    string scellvalue = currentRow.Cells[4].Value.ToString();
                    currentRow.Cells[4].Value = fgDtls.Rows[rowIndex + 1].Cells[4].Value;
                    fgDtls.Rows[rowIndex + 1].Cells[4].Value = scellvalue;
                    fgDtls.CurrentCell = fgDtls[4, rowIndex + 1];
                    //fgDtls.Rows.Remove(currentRow);
                    //fgDtls.Rows.Insert(rowIndex + 1, currentRow);
                    //fgDtls.CurrentCell = fgDtls[1, rowIndex + 1];
                    //this.SetOrderBy(iParentID);
                }
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboCompany_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboCompany.SelectedValue != null || cboCompany.SelectedValue.ToString() != "0" || cboCompany.SelectedValue.ToString() != "-")
            {
                fgDtls.Rows.Clear();
                using (IDataReader idr = DB.GetRS("Select Distinct MenuID,ParentID,OrderBy,Menu_Caption,Form_Caption,ToolTip,TblName_Main,PmryColumn,SearchQry,SearchQry_Dtls,FormCall,FormCall_Web,FormType,MenuType,IsForm,IsSeparator,ApplyYear,IsVisible from fn_menumaster_Comp() Where CompanyID =" + cboCompany.SelectedValue + " Order by ParentID,OrderBy"))
                {
                    while (idr.Read())
                    {
                        fgDtls.Rows.Add();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[0].Value = idr["MenuID"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[1].Value = fgDtls.Rows.Count;
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[2].Value = idr["ParentID"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[3].Value = idr["OrderBy"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[4].Value = idr["Menu_Caption"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[5].Value = idr["Form_Caption"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[6].Value = idr["ToolTip"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[7].Value = idr["tblName_Main"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[8].Value = idr["PmryColumn"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[9].Value = idr["SearchQry"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[10].Value = idr["SearchQry_Dtls"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[11].Value = idr["FormCall"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[12].Value = idr["FormCall_Web"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[13].Value = idr["FormType"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[14].Value = idr["MenuType"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[15].Value = Localization.ParseBoolean(idr["IsForm"].ToString());
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[16].Value = Localization.ParseBoolean(idr["IsSeparator"].ToString());
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[17].Value = Localization.ParseBoolean(idr["ApplyYear"].ToString());
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[18].Value = Localization.ParseBoolean(idr["IsVisible"].ToString());
                    }
                }
                fgDtls.Rows.Add();
            }
            if (DB.GetSnglValue("Select * from fn_menumaster_Comp() Where CompanyID =" + cboCompany.SelectedValue) == null || DB.GetSnglValue("Select * from fn_menumaster_Comp() Where CompanyID =" + cboCompany.SelectedValue) == "")
            {
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
            }
        }

        private void fgDtls_SelectionChanged(object sender, EventArgs e)
        {
            if (fgDtls.SelectedCells.Count > 0)
            {
                int selectedrowindex = fgDtls.SelectedCells[0].RowIndex;

                DataGridViewRow selectedRow = fgDtls.Rows[selectedrowindex];

                //string a = Convert.ToString(selectedRow.Cells["column name"].Value);
            }
        }

        private void addNewRowButton_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    DataGridViewRow dgwr = new DataGridViewRow();
            //    int selectedrowindex = fgDtls.SelectedCells[0].RowIndex;
            //    this.fgDtls.Rows.Add();
            //    fgDtls.Rows.Insert(selectedrowindex, dgwr);

            //    string isrno = string.Empty;
            //    //selectedrowindex = fgDtls.SelectedRows[0].Index;

            //    if (fgDtls.Rows[selectedrowindex].Cells[1].Value != null)
            //    {
            //        isrno = fgDtls.Rows[selectedrowindex].Cells[1].Value.ToString();
            //    }

            //    for (int i = 0; i > fgDtls.SelectedCells[0].RowIndex; i++)
            //    {
            //        {
            //            isrno = isrno + 1; ;
            //        }

            //        //dataGridView1.Rows[e.RowIndex].Cells["COLUMN_ID"].FormattedValue.ToString();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Navigate.logError(ex.Message, ex.StackTrace);
            //}

            try
            {
                if (cboCompany.SelectedValue == null || cboCompany.SelectedValue.ToString() == "-" || cboCompany.SelectedValue.ToString() == "" || cboCompany.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Company.");
                    cboCompany.Focus();
                }
                else
                {
                    frmStockAdj frmStockAdj = new frmStockAdj();
                    frmStockAdj.MenuID = base.iIDentity;
                    frmStockAdj.CompID = Localization.ParseNativeInt(cboCompany.SelectedValue.ToString());
                    frmStockAdj.Entity_IsfFtr = 0.0;
                    frmStockAdj.ref_fgDtls = this.fgDtls;
                    frmStockAdj.UsedInGridArray = this.OrgInGridArray;

                    if (frmStockAdj.ShowDialog() == DialogResult.Cancel)
                    {
                        frmStockAdj.Dispose();
                        return;
                    }
                    frmStockAdj.Dispose();
                    frmStockAdj = null;
                    this.fgDtls.Select();
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (cboCompany.SelectedValue != null || cboCompany.SelectedValue.ToString() != "0" || cboCompany.SelectedValue.ToString() != "-")
            {
                dtcopy = DB.GetDT("Select Distinct MenuID,ParentID,OrderBy,Menu_Caption,Form_Caption,ToolTip,TblName_Main,PmryColumn,SearchQry,SearchQry_Dtls,FormCall,FormCall_Web,FormType,MenuType,IsForm,IsSeparator,ApplyYear,IsVisible from fn_menumaster_Comp() Where CompanyID =" + cboCompany.SelectedValue, false);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecuritySuccess, "", "Copied Successfully..");
                btnCopy.Enabled = false;
                btnPaste.Visible = true;
                lblNotification.Visible = true;
            }
            else
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Company");
            }
        }

        private void btnCopy_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                btnCopy_Click(null, null);
            }
            if (e.Control && e.KeyCode == Keys.V)
            {
                btnPaste_Click(null, null);
            }
             
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (cboCompany.SelectedValue != null || cboCompany.SelectedValue.ToString() != "0" || cboCompany.SelectedValue.ToString() != "-")
            {
                fgDtls.Rows.Clear();
                if (dtcopy.Rows.Count > 1)
                {
                    for (int i = 0; i <= dtcopy.Rows.Count - 1; i++)
                    {
                        fgDtls.Rows.Add();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[0].Value = dtcopy.Rows[i]["MenuID"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[1].Value = fgDtls.Rows.Count;
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[2].Value = dtcopy.Rows[i]["ParentID"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[3].Value = dtcopy.Rows[i]["OrderBy"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[4].Value = dtcopy.Rows[i]["Menu_Caption"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[5].Value = dtcopy.Rows[i]["Form_Caption"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[6].Value = dtcopy.Rows[i]["ToolTip"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[7].Value = dtcopy.Rows[i]["tblName_Main"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[8].Value = dtcopy.Rows[i]["PmryColumn"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[9].Value = dtcopy.Rows[i]["SearchQry"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[10].Value = dtcopy.Rows[i]["SearchQry_Dtls"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[11].Value = dtcopy.Rows[i]["FormCall"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[12].Value = dtcopy.Rows[i]["FormCall_Web"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[13].Value = dtcopy.Rows[i]["FormType"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[14].Value = dtcopy.Rows[i]["MenuType"].ToString();
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[15].Value = Localization.ParseBoolean(dtcopy.Rows[i]["IsForm"].ToString());
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[16].Value = Localization.ParseBoolean(dtcopy.Rows[i]["IsSeparator"].ToString());
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[17].Value = Localization.ParseBoolean(dtcopy.Rows[i]["ApplyYear"].ToString());
                        fgDtls.Rows[fgDtls.RowCount - 1].Cells[18].Value = Localization.ParseBoolean(dtcopy.Rows[i]["IsVisible"].ToString());
                    }
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecuritySuccess, "", "Pasted Successfully..");
                    btnPaste.Visible = false;
                    btnCopy.Enabled = true;
                    lblNotification.Visible = false;
                }
                dtcopy = null;
            }
        }
    }
}
