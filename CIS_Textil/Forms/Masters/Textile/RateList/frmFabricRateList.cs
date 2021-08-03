using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_CLibrary;
using Microsoft.VisualBasic.CompilerServices;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmFabricRateList : frmMasterIface
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
                DataGridViewCellEventHandler handler3 = new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);

                if (this._fgDtls != null)
                {
                    this._fgDtls.CellValueChanged -= handler3;
                }
                this._fgDtls = value;
                if (this._fgDtls != null)
                {
                    this._fgDtls.CellValueChanged += handler3;
                }
            }
        }

        public frmFabricRateList()
        {
            InitializeComponent();
            fgDtls = new DataGridViewEx();
            //m_resourceManger = new ResourceManager("CIS_Textil.Localize.Localization", Assembly.GetExecutingAssembly());
            //// Init UICulture to CurrentCulture
            //Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
        }

        private void frmFabricRateList_Load(object sender, EventArgs e)
        {
            try
            {
                DetailGrid_Setup.CreateDtlGrid(this, pnlDetail, fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0);
                this.txtEntryNo.Enabled = false;
                rdbQuality_CheckedChanged(null, null);
                rdoDesign_CheckedChanged(null, null);
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
                CIS_Textbox txtEntryNo = this.txtEntryNo;
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                DataGridViewEx fgDtls = this.fgDtls;
                this.txtEntryNo = txtEntryNo;
                dtEntryDate.Text = "__/__/____";
                dtEntryDate.Focus();
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                CommonCls.IncFieldID(this, ref txtCode, "");
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
                string Rdbtext = string.Empty;
                if (rdoQuality.Checked == true)
                { Rdbtext = "0"; }
                else if (rdoDesign.Checked == true)
                { Rdbtext = "1"; }

                ArrayList pArrayData = new ArrayList();
                ArrayList list2 = pArrayData;
                list2.Add(this.txtEntryNo.Text.ToString());
                list2.Add(this.dtEntryDate.TextFormat(false, true));
                list2.Add(Rdbtext);

                int iDupID = (fgDtls.RowCount - 2);

                for (int i = 0; i <= fgDtls.RowCount - 3; i++)
                {
                    if ((fgDtls.Rows[i].Cells[2].Value != null))
                    {
                        if (fgDtls.Rows[i].Cells[2].Value.ToString().Trim().ToUpper() == fgDtls.Rows[iDupID].Cells[2].Value.ToString().Trim().ToUpper())
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "There Are Some Item in grid Which is already Selected");
                            return;
                        }
                    }
                }
                DBSp.Transcation_AddEdit(pArrayData, this.fgDtls, true);
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
            }
        }

        public bool ValidateForm()
        {
            try
            {
                if (!EventHandles.IsValidGridReq(this.fgDtls, base.dt_AryIsRequired) || !CommonCls.CheckDate(this.dtEntryDate.Text, false))
                {
                    return true;
                }
                if (!CommonCls.CheckDate(dtEntryDate.Text, true))
                    return true;

                if (txtEntryNo.Text.ToString() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry No");
                    return true;
                }

                for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                {
                    if (fgDtls.Rows[i].Cells[4].Value.ToString() == "" || fgDtls.Rows[i].Cells[4].Value.ToString() == "0" || fgDtls.Rows[i].Cells[4].Value.ToString() == "0.00")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Error", "Please Enter Rate");
                        return true;
                    }
                }
            }

            catch (Exception exception1)
            {
                Exception exception = exception1;
                Navigate.logError(exception.Message, exception.StackTrace);
            }
            return false;
        }

        public void FillControls()
        {
            try
            {
                this.txtEntryNo.Focus();
                DBValue.Return_DBValue(this, txtCode, "FabricRateListID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);

                try
                {
                    string strRdo = string.Empty;
                    strRdo = DB.GetSnglValue("Select RdoGroupType From tbl_FabricRateListMain Where IsDeleted=0 and FabricRateListID=" + txtCode.Text + "");
                    if (strRdo == "0")
                    {
                        rdoQuality.Checked = true;
                    }
                    else if (strRdo == "1")
                    {
                        rdoDesign.Checked = true;
                    }
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                }

                try
                {
                    DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "FabricRateListID", txtCode.Text, base.dt_HasDtls_Grd);
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                }
                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, dt_HasDtls_Grd, dt_AryCalcvalue, dt_AryIsRequired, true, false);
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        #region FillGrid
        //private void FillGrid()
        //{
        //    DataGridViewEx fgDtls = this.fgDtls;

        //    using (DataTable dt = DB.GetDT(string.Format("Select * from {0} ", "fn_ItemMaster() Where RateID=" + txtCode.Text), false))
        //    {
        //        int srno = 0;
        //        fgDtls.Rows.Clear();
        //        for (int j = 0; j < (dt.Rows.Count); j++)
        //        {
        //            fgDtls.Rows.Add();
        //            fgDtls.Rows[j].Cells[0].Value = "";
        //            fgDtls.Rows[j].Cells[1].Value = j + 1;
        //            fgDtls.Rows[j].Cells[2].Value = Localization.ParseNativeInt(dt.Rows[j]["ItemID"].ToString());
        //            fgDtls.Rows[j].Cells[3].Value = Localization.ParseNativeInt(dt.Rows[j]["ItemGroupID"].ToString());
        //            fgDtls.Rows[j].Cells[4].Value = Localization.ParseNativeInt(dt.Rows[j]["UnitID"].ToString());
        //            fgDtls.Rows[j].Cells[5].Value = Localization.ParseNativeDecimal(dt.Rows[j]["Rate"].ToString());
        //            fgDtls.Rows[j].Cells[6].Value = dtEntryDate.Text.Trim().ToString();
        //            fgDtls.Rows[j].Cells[2].ReadOnly = false;
        //            fgDtls.Rows[j].Cells[3].ReadOnly = false;
        //            fgDtls.Rows[j].Cells[4].ReadOnly = false;
        //            srno++;
        //        }
        //    }
        //    if ((this.txtCode.Text != null) & (this.txtCode.Text != ""))
        //    {
        //        using (DataTable table = DB.GetDT(string.Format("Select * from {0} where RateID={1}", "tbl_ItemRateListDtls", this.txtCode.Text), false))
        //        {
        //            int num3 = table.Rows.Count - 1;
        //            for (int i = 0; i <= num3; i++)
        //            {
        //                int num4 = this.fgDtls.Rows.Count - 1;
        //                for (int j = 0; j <= num4; j++)
        //                {
        //                    DataGridViewRow row = fgDtls.Rows[j];
        //                    if ((Operators.ConditionalCompareObjectEqual(row.Cells[3].Value, table.Rows[i]["ItemGroupID"].ToString(), false) && (row.Cells[2].Value.ToString() == table.Rows[i]["ItemID"].ToString())) && (row.Cells[4].Value.ToString() == table.Rows[i]["UnitID"].ToString()))
        //                    {
        //                        row.Cells[5].Value = Localization.ParseNativeInt(table.Rows[i]["Rate"].ToString());
        //                    }
        //                    row = null;
        //                }
        //            }
        //        }
        //    }
        //    fgDtls = null;
        //}
        #endregion

        private void btnAddRateList_Click(object sender, EventArgs e)
        {
            try
            {
                var grd = fgDtls;
                bool isblankrecord = false;
                int j = 0;

                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (rdoQuality.Checked == true)
                    {
                        using (IDataReader dr = DB.GetRS(string.Format("Select * from {0}({1},{2}) ", "fn_FabricQuality_Find", Db_Detials.CompID, Db_Detials.YearID)))
                        {
                            while (dr.Read())

                            {
                                isblankrecord = true;
                                grd.Rows[grd.RowCount - 1].Cells[1].Value = (j + 1);
                                grd.Rows[grd.RowCount - 1].Cells[2].Value = Localization.ParseNativeInt(dr["FabricQualityID"].ToString());
                                //grd.Rows[grd.RowCount - 1].Cells[3].Value = Localization.ParseNativeInt(dr["ItemGroupID"].ToString());
                                grd.Rows[grd.RowCount - 1].Cells[4].Value = Localization.ParseNativeDecimal(dr["Rate"].ToString());
                                grd.Rows.Add();
                                j++;
                            }
                        }
                    }

                    if (rdoDesign.Checked == true)
                    {
                        using (IDataReader dr = DB.GetRS(string.Format("Select * from {0}({1},{2}) ", "fn_FabricDesign_Find", Db_Detials.CompID, Db_Detials.YearID)))
                        {
                            while (dr.Read())
                            {
                                isblankrecord = true;
                                grd.Rows[grd.RowCount - 1].Cells[1].Value = (j + 1);
                                grd.Rows[grd.RowCount - 1].Cells[2].Value = Localization.ParseNativeInt(dr["FabricQualityID"].ToString());
                                grd.Rows[grd.RowCount - 1].Cells[3].Value = Localization.ParseNativeInt(dr["FabricDesignID"].ToString());
                                //grd.Rows[grd.RowCount - 1].Cells[4].Value = Localization.ParseNativeDecimal(dr["Rate"].ToString());
                                grd.Rows.Add();
                                j++;
                            }
                        }
                    }

                    if (isblankrecord == false)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "No Records Found");
                    }
                    else
                    {
                        grd.CurrentCell = grd[1, (fgDtls.RowCount - 1)];
                        EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if ((e.ColumnIndex == 2) && (fgDtls.Rows[e.RowIndex].Cells[2].Value != null))
            //{
            //    fgDtls.Rows[e.RowIndex].Cells[3].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select ItemGroupID From {0} Where ItemID = {1}", "tbl_ItemMaster", RuntimeHelpers.GetObjectValue(fgDtls.Rows[e.RowIndex].Cells[2].Value))));
            //    fgDtls.Rows[e.RowIndex].Cells[4].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select UnitID From {0} Where ItemID = {1}", "tbl_ItemMaster", RuntimeHelpers.GetObjectValue(fgDtls.Rows[e.RowIndex].Cells[2].Value))));
            //}
        }

        private void rdbQuality_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    fgDtls.Rows.Clear();
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    fgDtls.Columns[2].Visible = true;
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void rdoDesign_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    fgDtls.Rows.Clear();
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    fgDtls.Columns[2].Visible = false;
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
    }
}
