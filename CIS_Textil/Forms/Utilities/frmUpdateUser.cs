using System;
using System.Drawing;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using Infragistics.Win.UltraWinGrid;

namespace CIS_Textil
{
    public partial class frmUpdateUser : frmMasterIface
    {
        public int userid = 0;
        public frmUpdateUser()
        {
            InitializeComponent();
        }

        #region Form Events

        private void frmUpdateUser_Load(object sender, EventArgs e)
        {
            try
            {
                Form cForm = this;
                Combobox_Setup.FilterId = "";
                Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                Combobox_Setup.FillCbo(ref cboUserName, Combobox_Setup.ComboType.Mst_User, "");
                int usertype = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select UserTypeID From fn_UserMaster_tbl() Where UserID=" + Db_Detials.UserID + "")));
                lblHtext.Visible = false;
                if (usertype == 1)
                {
                    btnKill.Visible = true;

                }
                else
                {
                    btnKill.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }

        }
        #endregion

        private void FillUltraGrid(object sender, EventArgs e)
        {
            try
            {
                fgdtls_f.DataSource = DB.GetDT(string.Format("select Username,IsActive,Isloggedin,Sessions from tbl_UserMaster Where IsDeleted=0"), false);
                foreach (UltraGridBand band in fgdtls_f.DisplayLayout.Bands)
                {
                    foreach (UltraGridColumn column in band.Columns)
                    {
                        if ((column.Index == 1) || (column.Index == 2))
                            column.CellActivation = Activation.AllowEdit;
                        else
                            column.CellActivation = Activation.NoEdit;

                        if (column.Index > 5)
                            column.Hidden = true;
                        else
                            column.Hidden = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string strQry = string.Empty;
                strQry = string.Format("Update tbl_UserMaster set Sessions={0} , Isloggedin={1} where IsDeleted=0 and UserID={2}", 0, ChkActive.Checked ? 1 : 0, Localization.ParseNativeInt(cboUserName.SelectedValue.ToString()));
                if (strQry != "")
                {
                    try
                    {
                        DB.ExecuteSQL(strQry);
                        cboUserName_SelectedIndexChanged(null, null);
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "USER UPDATED SUCCESSFULLY...");
                        int iactionType = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster_tbl() Where MiscName='isEdit'"));
                        DBSp.Log_CurrentUser(base.iIDentity, iactionType, 0, 0, 0, 0);
                    }
                    catch
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "ERROR WHILE UPDATING USER...");
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }

        }

        private void fgdtls_f_KeyDown(object sender, KeyEventArgs e)
        {
            if (blnFormAction == Enum_Define.ActionType.New_Record | blnFormAction == Enum_Define.ActionType.Edit_Record)
            {
                try
                {
                    foreach (UltraGridBand band in fgdtls_f.DisplayLayout.Bands)
                    {

                        if ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.Enter))
                        {
                            fgdtls_f.PerformAction(UltraGridAction.BelowCell, true, false);
                            fgdtls_f.PerformAction(UltraGridAction.EnterEditMode);
                            e.SuppressKeyPress = true;
                        }
                        else if (e.KeyCode == Keys.Up)
                        {
                            fgdtls_f.PerformAction(UltraGridAction.AboveCell, true, false);
                            fgdtls_f.PerformAction(UltraGridAction.EnterEditMode);
                            e.SuppressKeyPress = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                }

            }
        }

        private void fgdtls_f_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            if (e != null)
            {
                e.Layout.Override.RowSizing = RowSizing.Free;
                e.Layout.Bands[0].AutoPreviewEnabled = true;
                e.Layout.Override.FilterUIType = FilterUIType.FilterRow;
                e.Layout.Override.FilterOperandStyle = FilterOperandStyle.Combo;
                e.Layout.Override.FilterEvaluationTrigger = FilterEvaluationTrigger.OnCellValueChange;
                e.Layout.Override.FilterOperatorLocation = FilterOperatorLocation.WithOperand;
                e.Layout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.StartsWith;
                e.Layout.Override.FilterClearButtonLocation = FilterClearButtonLocation.RowAndCell;
                e.Layout.Override.FilterRowAppearance.BackColor = Color.LightYellow;
                e.Layout.Override.FilterRowPromptAppearance.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
                e.Layout.Override.SpecialRowSeparator = SpecialRowSeparator.FilterRow;
                e.Layout.Override.SpecialRowSeparatorAppearance.BackColor = Color.FromArgb(0xe9, 0xf2, 0xc7);
                e.Layout.Override.AllowRowSummaries = AllowRowSummaries.True;
                e.Layout.Override.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
                e.Layout.Override.SummaryDisplayArea |= SummaryDisplayAreas.GroupByRowsFooter;
                e.Layout.Override.SummaryDisplayArea |= SummaryDisplayAreas.InGroupByRows;
                e.Layout.Override.GroupBySummaryDisplayStyle = GroupBySummaryDisplayStyle.SummaryCells;
                e.Layout.Override.SummaryFooterAppearance.BackColor = SystemColors.Info;
                e.Layout.Override.SummaryValueAppearance.BackColor = SystemColors.Window;
                e.Layout.Override.SummaryValueAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                e.Layout.Override.GroupBySummaryValueAppearance.BackColor = SystemColors.Window;
                e.Layout.Override.GroupBySummaryValueAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                e.Layout.Bands[0].SummaryFooterCaption = "Grand Totals:";
                e.Layout.Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                e.Layout.Override.SummaryFooterCaptionVisible = Infragistics.Win.DefaultableBoolean.False;
                e.Layout.Override.SpecialRowSeparator = SpecialRowSeparator.SummaryRow;
                e.Layout.Override.SpecialRowSeparatorAppearance.BackColor = Color.FromArgb(0xda, 0xd9, 0xf1);
                e.Layout.Override.SpecialRowSeparatorHeight = 6;
                e.Layout.Override.BorderStyleSpecialRowSeparator = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
                e.Layout.Override.CellClickAction = CellClickAction.EditAndSelectText;
                e.Layout.Override.SelectTypeRow = SelectType.None;
                e.Layout.ViewStyle = ViewStyle.SingleBand;
                e.Layout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
            }
        }

        private void cboUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtActiveSessions.Text = DB.GetSnglValue("Select Sessions From tbl_UserMaster Where UserID=" + cboUserName.SelectedValue + "");
                txtIpAddress.Text = DB.GetSnglValue("Select IPAddress From tbl_UserMaster Where UserID=" + cboUserName.SelectedValue + "");

                bool bLoggedID = Localization.ParseBoolean(DB.GetSnglValue("Select IsLoggedIn From tbl_UserMaster Where UserID=" + cboUserName.SelectedValue + "").ToString());

                if (bLoggedID == true)
                {
                    ChkActive.Checked = true;
                }
                else
                {
                    ChkActive.Checked = false;
                }


            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }

        }

        private void btnClearSessions_Click(object sender, EventArgs e)
        {
            try
            {
                string strQry = string.Empty;
                strQry = string.Format("Update tbl_UserMaster set Sessions={0} where IsDeleted=0 and UserID={1}",0, Localization.ParseNativeInt(cboUserName.SelectedValue.ToString()));
                if (strQry != "")
                {
                    try
                    {
                        DB.ExecuteSQL(strQry);
                        txtActiveSessions.Text = DB.GetSnglValue("Select Sessions From tbl_UserMaster Where UserID=" + cboUserName.SelectedValue + "");
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "SESSION CLEARED SUCCESSFULLY...");
                    }
                    catch
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "ERROR WHILE CLEARING SESSION...");
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }

        }

        private void btnKill_MouseHover(object sender, EventArgs e)
        {
            lblHtext.Visible = true;
        }

        private void btnKill_MouseLeave(object sender, EventArgs e)
        {
            lblHtext.Visible = false;
        }

        //private void fgdtls_f_AfterSelectChange(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e)
        //{
        //    if (this.fgdtls_f.Selected.Rows.Count > 0)
        //    {
        //        //this.ultraTextEditor1.Text = "Band, Row \n";
        //        foreach (UltraGridRow rowSelected in this.fgdtls_f.Selected.Rows)
        //        {
        //            int i = Localization.ParseNativeInt(rowSelected.Band.Index.ToString());
        //            string strusername = fgdtls_f.Rows[i].Cells[0].Value.ToString();

        //          
        //            //this.ultraTextEditor1.Text += rowSelected.Band.Index.ToString() +
        //            //  " , " + rowSelected.Index.ToString() + "\n";
        //        }
        //    }
        //}

        //private void btnClearSessions_Click(object sender, EventArgs e)
        //{
        //    if (userid != 0)
        //    {
        //        DB.ExecuteSQL(string.Format("Update tbl_UserMaster Set Sessions=0 where UserID=" + userid + ""));
        //    }
        //}

    }

}
