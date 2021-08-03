using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_DBLayer;
using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using CIS_CLibrary;

namespace CIS_Textil
{
    public partial class frmFabricInward : frmTrnsIface
    {
        private bool IsOrderSelected = false;
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;
        public decimal OrderMeters;
        private int _MtrsID = 0;
        public static int OrderQualityID;
        public ArrayList OrgInGridArray;
        private bool FAB_SERIALWISE;
        private int RefMenuID;
        private bool Vld_DupPieceNo;
        private bool INC_PNO_STOCK;
        private string STRT_PNO;
        private int IFEntry;
        private bool flg_Email;
        private bool flg_Sms;
        private int iFabIssueID_Main;
        private bool FIN_CPY_FIS;
        private bool isRet = false;
        private bool Alert = false;
        private int ExcessInward_Mtrs;
        private string strQry = "";
        public string strUniqueID;
        private static string RefVoucherID;
        private int iMaxMyID_Orders;
        public ArrayList dArray;

        public frmFabricInward()
        {
            OrderMeters = new decimal();
            Alert = false;
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
        }

        private void frmFabricInward_Load(object sender, EventArgs e)
        {
            try
            {
                dArray = new ArrayList();
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboSupplier, Combobox_Setup.ComboType.Mst_Suppliers, "");
                Combobox_Setup.FillCbo(ref cboBroker, Combobox_Setup.ComboType.Mst_Brokers, "");
                Combobox_Setup.FillCbo(ref cboDepartment, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
                Combobox_Setup.FillCbo(ref cboProcesser, Combobox_Setup.ComboType.Mst_Dyer);
                cboOrderType.AutoComplete = true;
                cboOrderType.AutoDropdown = true;

                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                txtEntryNo.Enabled = false;
                _MtrsID = Localization.ParseNativeInt(DB.GetSnglValue("SELECT UnitID from tbl_UnitsMaster WHERE IsDeleted=0 and UnitName='Meters'"));

                FIN_CPY_FIS = Localization.ParseBoolean(GlobalVariables.FIN_CPY_FIS);
                FAB_SERIALWISE = Localization.ParseBoolean(GlobalVariables.FAB_SERIALWISE);
                Vld_DupPieceNo = Localization.ParseBoolean(GlobalVariables.Vld_DupPieceNo);
                INC_PNO_STOCK = Localization.ParseBoolean(GlobalVariables.INC_PNO_STOCK);
                STRT_PNO = GlobalVariables.STRT_PNO.ToString();
                ExcessInward_Mtrs = Localization.ParseNativeInt(GlobalVariables.EX_IN_MTRS_PERC.ToString());

                if (FAB_SERIALWISE)
                {
                    fgDtls.Columns[5].Visible = true;
                    fgDtls.Columns[6].ReadOnly = true;
                    fgDtls.Columns[7].ReadOnly = true;
                    fgDtls.Columns[8].ReadOnly = true;
                }
                else
                {
                    fgDtls.Columns[5].Visible = false;
                    fgDtls.Columns[6].ReadOnly = false;
                    fgDtls.Columns[7].ReadOnly = false;
                    fgDtls.Columns[8].ReadOnly = false;
                }

                cboSupplier.GroupType = LedgerGroup.Suppliers;
                cboBroker.GroupType = LedgerGroup.Brokers;
                btnSelectStock.Enabled = false;

                int IFEntry = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(FabricInwardID),0) From {0}  Where IsDeleted=0 and  CompID = {1} and YearID = {2}", "tbl_FabricInwardMain", Db_Detials.CompID, Db_Detials.YearID))));

                if (IFEntry == 0)
                {
                    if (INC_PNO_STOCK)
                    {
                        if (((fgDtls.RowCount > 0) && (fgDtls.ColumnCount > 0)) && fgDtls.Columns[2].Visible)
                        {
                            fgDtls.Rows[0].Cells[4].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select  {0}({1},{2})", new object[] { "dbo.fn_FetchPieceNo_Stock", Db_Detials.CompID, Db_Detials.YearID })), Db_Detials.PCS_NO_INCMT);
                        }
                        else
                        {
                            fgDtls.Rows[0].Cells[4].Value = "-";
                        }
                    }
                    else
                    {
                        if (((fgDtls.RowCount > 0) & (fgDtls.ColumnCount > 0)) & fgDtls.Columns[2].Visible)
                        {
                            fgDtls.Rows[0].Cells[4].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select  {0}({1},{2},{3},{4})", "dbo.fn_FetchPieceNo", IFEntry, base.iIDentity, Db_Detials.CompID, Db_Detials.YearID)), Db_Detials.PCS_NO_INCMT);
                        }
                        else
                        {
                            fgDtls.Rows[0].Cells[4].Value = "-";
                        }
                    }
                }

                cboOrderType_SelectedIndexChanged(null, null);
                RefMenuID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MenuID from tbl_VoucherTypeMaster Where GenMenuID=" + base.iIDentity + "")));
                GetRefModID();

                this.fgDtls.RowsAdded += new DataGridViewRowsAddedEventHandler(this.fgDtls_RowsAdded);
                this.fgDtls.CellEndEdit += new DataGridViewCellEventHandler(this.fgDtls_CellEndEdit);
                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls.KeyDown += new KeyEventHandler(this.fgDtls_KeyDown);
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
                if (strUniqueID != null)
                {
                    string strQry = string.Format("Delete From tbl_FabricOrderLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                    strQry = strQry + string.Format("Update  tbl_FabricOrderLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                    DB.ExecuteSQL(strQry);
                    strQry = string.Format("Update tbl_FabricOrderLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                    DB.ExecuteSQL(strQry);
                    strQry = "";
                }

                txtCode.Text = "";
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                cboDepartment.Enabled = true;
                IsOrderSelected = false;
                OrgInGridArray = new ArrayList();
                int MaxId = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(FabricInwardID),0) From {0}  Where IsDeleted=0 and  CompID = {1} and YearID = {2} and BranchID={3} and StoreID ={4}", "tbl_FabricInwardMain", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.StoreID))));
                if (MaxId > 0)
                {
                    using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where IsDeleted=0 and FabricInwardID = {1} and CompID={2} and YearID={3} and BranchID={4} and StoreID ={5}", "tbl_FabricInwardMain", MaxId, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.StoreID)))
                    {
                        while (reader.Read())
                        {
                            dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                            dtChallanDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                            cboSupplier.SelectedValue = Localization.ParseNativeDouble(reader["SupplierID"].ToString());
                            cboBroker.SelectedValue = Localization.ParseNativeDouble(reader["BrokerID"].ToString());
                            cboDepartment.SelectedValue = Localization.ParseNativeDouble(reader["DepartmentID"].ToString());
                            cboTransport.SelectedValue = Localization.ParseNativeDouble(reader["TransportID"].ToString());
                            dtLrDate.Text = Localization.ToVBDateString(reader["LrDate"].ToString());

                            if (reader["OrderType"].ToString() != "")
                            {
                                cboOrderType.SelectedItem = reader["OrderType"].ToString();
                            }
                            else
                            {
                                cboOrderType.SelectedItem = "WITH ORDER";
                                cboOrderType.Enabled = false;
                            }
                        }
                    }
                }
                cboOrderType.Enabled = true;
                cboSupplier.Enabled = true;
                dtChallanDate.Enabled = true;

                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);

                if (INC_PNO_STOCK)
                {
                    if (((fgDtls.RowCount > 0) && (fgDtls.ColumnCount > 0)) && fgDtls.Columns[2].Visible)
                    {
                        fgDtls.Rows[0].Cells[4].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select {0}({1},{2})", new object[] { "dbo.fn_FetchPieceNo_Stock", Db_Detials.CompID, Db_Detials.YearID })), Db_Detials.PCS_NO_INCMT);
                    }
                    else
                    {
                        fgDtls.Rows[0].Cells[4].Value = "-";
                    }
                }
                else
                {
                    if (((fgDtls.RowCount > 0) & (fgDtls.ColumnCount > 0)) & fgDtls.Columns[2].Visible)
                    {
                        fgDtls.Rows[0].Cells[4].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select  {0}({1},{2},{3},{4})", "dbo.fn_FetchPieceNo", MaxId, base.iIDentity, Db_Detials.CompID, Db_Detials.YearID)), Db_Detials.PCS_NO_INCMT);
                    }
                    else
                    {
                        fgDtls.Rows[0].Cells[4].Value = "-";
                    }
                }

                dtEntryDate.Focus();
                Alert = false;
                txtUniqueID.Text = CommonCls.GenUniqueID();
                strUniqueID = txtUniqueID.Text;
                cboOrderType_SelectedIndexChanged(null, null);
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
                DBValue.Return_DBValue(this, txtCode, "FabricInwardID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtRefNo, "RefNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtChallanDate, "RefDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboSupplier, "SupplierID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDepartment, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLrNo, "LrNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtLrDate, "LrDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboProcesser, "ProcessorID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEd1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                DetailGrid_Setup.FillGrid(fgDtls, fgDtls.Grid_UID, fgDtls.Grid_Tbl, "FabricInwardID", txtCode.Text, base.dt_HasDtls_Grd, this.iIDentity, GlobalVariables.VALIDATE_EDIT, Db_Detials.CompID.ToString(), 1);
                int icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_FabricOrderLedger_tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));
                try
                {
                    string sOrderType = DBValue.Return_DBValue(this, "OrderType");
                    cboOrderType.SelectedItem = sOrderType;
                }
                catch { }

                try
                {
                    if (base.blnFormAction == Enum_Define.ActionType.View_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        string sChlnDate = Localization.ToVBDateString(DB.GetSnglValue("select ChallanDate from tbl_FabricInwardMain where FabricInwardID=" + txtCode.Text + "and Isdeleted=0"));
                        string sLrDate = Localization.ToVBDateString(DB.GetSnglValue("select LrDate from tbl_FabricInwardMain where FabricInwardID=" + txtCode.Text + "and Isdeleted=0"));
                        dtChallanDate.Text = sChlnDate;
                        if (Information.IsDate(sLrDate))
                        {
                            dtLrDate.Text = sLrDate;
                        }
                        else
                        {
                            dtLrDate.Text = "__/__/____";
                        }
                    }
                }
                catch (Exception ex1)
                {
                    Navigate.logError(ex1.Message, ex1.StackTrace);
                }

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT COUNT(0) from fn_StockFabricLedger() WHERE TransType<>" + this.iIDentity + " and RefID in (SELECT RefID from tbl_FabricInwardDtls AS A Left join tbl_FabricInwardMain as B ON A.FabricInwardID=B.FabricInwardID  WHERE A.FabricInwardID=" + txtCode.Text.Trim() + ")")) > 0)
                    {
                        cboDepartment.Enabled = false;
                    }
                    else
                    {
                        cboDepartment.Enabled = true;
                    }
                    cboOrderType_SelectedIndexChanged(null, null);
                }
                else
                    cboDepartment.Enabled = true;

                this.CalcVal();


                System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                dgvCellStyle.BackColor = System.Drawing.Color.LightGray;
                dgvCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
                dgvCellStyle.SelectionBackColor = System.Drawing.Color.Purple;
                dgvCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

                try
                {
                    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    {
                        if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT COUNT(0) from fn_StockFabricLedger() WHERE TransType<>" + this.iIDentity + " and RefID in (SELECT RefID from tbl_FabricInwardDtls AS A Left join tbl_FabricInwardMain as B ON A.FabricInwardID=B.FabricInwardID  WHERE A.FabricInwardID=" + txtCode.Text.Trim() + ")")) > 0)
                        {
                            fgDtls.Rows[i].ReadOnly = true;
                            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle;
                        }
                        else
                        {
                            if (DetailGrid_Setup.isRowsEditable)
                            {
                                fgDtls.Rows[i].ReadOnly = false;
                            }
                        }
                    }
                }
                catch { }

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    cboOrderType.Enabled = false;
                    cboSupplier.Enabled = false;
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    setTempRowIndex();

                    try
                    {
                        string strOldUniqueID = string.Empty;
                        strOldUniqueID = txtUniqueID.Text;
                        txtUniqueID.Text = CommonCls.GenUniqueID();
                        strUniqueID = txtUniqueID.Text;
                        if (icount == 0)
                        {
                            string strQry = string.Format("Update tbl_FabricOrderLedger Set UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + ", StatusID=2 Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + "");
                            DB.ExecuteSQL(strQry);
                        }
                        else
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityShieldBlue, "Warning", "This Record Is Edited By Another User..");
                        }
                    }
                    catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                }
                else
                {
                    cboOrderType.Enabled = true;
                    cboSupplier.Enabled = true;
                }

                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    if (strUniqueID != null)
                    {
                        string strQry = string.Format("Delete From tbl_FabricOrderLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                        strQry = strQry + string.Format("Update  tbl_FabricOrderLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                        DB.ExecuteSQL(strQry);
                        strQry = string.Format("Update tbl_FabricOrderLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                        DB.ExecuteSQL(strQry);
                    }
                }

                if ((base.blnFormAction == Enum_Define.ActionType.View_Record) && !(base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_FabricOrderLedger_tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));
                }

                System.Windows.Forms.DataGridViewCellStyle dgvCellStyle_Ref = new System.Windows.Forms.DataGridViewCellStyle();
                dgvCellStyle_Ref.BackColor = System.Drawing.Color.LightSteelBlue;
                dgvCellStyle_Ref.ForeColor = System.Drawing.SystemColors.WindowText;
                dgvCellStyle_Ref.SelectionBackColor = System.Drawing.Color.SteelBlue;
                dgvCellStyle_Ref.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
                try
                {
                    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    {
                        if (icount > 0)
                        {
                            fgDtls.Rows[i].ReadOnly = true;
                            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle_Ref;
                        }
                        else
                        {
                            fgDtls.Rows[i].ReadOnly = false;
                        }

                        if (fgDtls.RowCount > 1)
                        { btnSelectStock.Enabled = false; }
                        else { btnSelectStock.Enabled = true; }
                    }
                }
                catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void CalcVal()
        {
            try
            {
                //TxtTotalCuts.Text = string.Format("{0:N0}", CommonCls.GetColSum(fgDtls, 11, -1, -1));
                //TxtTotMtrs.Text = string.Format("{0:N2}", CommonCls.GetColSum(fgDtls, 12, -1, -1));
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                try
                {
                    if ((e.ColumnIndex == 11) | (e.ColumnIndex == 12) | (e.ColumnIndex == 13))
                    {
                        ExecuterTempQry_Orders(e.RowIndex);
                    }
                }
                catch (Exception exception1)
                {
                    Navigate.logError(exception1.Message, exception1.StackTrace);
                }

                if (!((base.blnFormAction == Enum_Define.ActionType.View_Record) || (base.blnFormAction == Enum_Define.ActionType.Not_Active)))
                {
                    if ((((e.ColumnIndex == 5) | (e.ColumnIndex == 7))) && ((fgDtls.Rows[e.RowIndex].Cells[6].Value != null) && (fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString().Length > 0)))
                    {
                        fgDtls.Rows[e.RowIndex].Cells[7].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", RuntimeHelpers.GetObjectValue(fgDtls.Rows[e.RowIndex].Cells[6].Value))));
                    }
                    if ((e.ColumnIndex == 11) | (e.ColumnIndex == 12))
                    {
                        CalcVal();
                    }
                }

                if ((e.ColumnIndex == 11) | (e.ColumnIndex == 12))
                {
                    CalcVal();
                    //ExecuterTempQry(e.RowIndex);
                }

                switch (e.ColumnIndex)
                {
                    case 7:
                        if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                        {
                            if (!IsOrderSelected)
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[2].Value == null)
                                    fgDtls.Rows[e.RowIndex].Cells[2].Value = "";

                                //if ((e.ColumnIndex == 11) && (e.ColumnIndex == 12) && (fgDtls.Rows[e.RowIndex].Cells[7].Value != null) && (fgDtls.Rows[e.RowIndex].Cells[6].Value != null) && (fgDtls.Rows[e.RowIndex].Cells[11].Value != null) && (fgDtls.Rows[e.RowIndex].Cells[12].Value != null))
                                //{
                                //    for (int i = 0; i <= (fgdtls_f_f.Rows.Count - 1); i++)
                                //    {
                                //        if (Operators.ConditionalCompareObjectEqual(fgDtls.Rows[e.RowIndex].Cells[6].Value, fgdtls_f_f.Rows[i].Cells[7].Value, false))
                                //        {
                                //            if (Operators.ConditionalCompareObjectEqual(fgDtls.Rows[e.RowIndex].Cells[7].Value, fgdtls_f_f.Rows[i].Cells[3].Value, false))
                                //            {
                                //                if (Operators.ConditionalCompareObjectEqual(fgDtls.Rows[e.RowIndex].Cells[8].Value, fgdtls_f_f.Rows[i].Cells[10].Value, false))
                                //                {
                                //                    if (Operators.ConditionalCompareObjectGreater(fgdtls_f_f.Rows[i].Cells[12].Value, 0, false))
                                //                    {
                                //                        fgDtls.Rows[e.RowIndex].Cells[2].Value = fgdtls_f_f.Rows[i].Cells[1].Value;
                                //                        fgDtls.Rows[e.RowIndex].Cells[11].Value = fgdtls_f_f.Rows[i].Cells[32].Value;
                                //                        fgDtls.Rows[e.RowIndex].Cells[12].Value = fgdtls_f_f.Rows[i].Cells[33].Value;

                                //                        //fgdtls_f_f.Rows[i].Cells[34].Value = Localization.ParseNativeDouble(fgdtls_f_f.Rows[i].Cells[34].Value.ToString()) + Localization.ParseNativeDouble(fgdtls_f_f.Rows[i].Cells[32].Value.ToString());
                                //                        //fgdtls_f_f.Rows[i].Cells[18].Value = Localization.ParseNativeDouble(fgdtls_f_f.Rows[i].Cells[18].Value.ToString()) + Localization.ParseNativeDouble(fgdtls_f_f.Rows[i].Cells[33].Value.ToString());
                                //                        fgdtls_f_f.Rows[i].Cells[32].Value = (Localization.ParseNativeDouble(fgdtls_f_f.Rows[i].Cells[12].Value.ToString()) - Localization.ParseNativeDouble(this.fgdtls_f_f.Rows[i].Cells[30].Value.ToString())) - Localization.ParseNativeDouble(fgdtls_f_f.Rows[i].Cells[34].Value.ToString());
                                //                        //fgdtls_f_f.Rows[i].Cells[33].Value = "0";
                                //                        fgDtls.Rows[e.RowIndex].Cells[30].Value = "0";
                                //                        break;
                                //                    }
                                //                }
                                //            }
                                //        }
                                //    }
                                //}
                            }
                        }
                        break;
                }
                if (fgDtls.RowCount > 1)
                {
                    cboOrderType.Enabled = false;
                }
                else
                {
                    cboOrderType.Enabled = true;
                }

                if (e.ColumnIndex == 5 || e.ColumnIndex == 6 || e.ColumnIndex == 7)
                {
                    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    {
                        if (fgDtls.Rows[i].Cells[6].Value != null && fgDtls.Rows[i].Cells[6].Value.ToString() != "" && fgDtls.Rows[i].Cells[7].Value != null && fgDtls.Rows[i].Cells[7].Value.ToString() != "" && fgDtls.Rows[i].Cells[8].Value != null && fgDtls.Rows[i].Cells[8].Value.ToString() != "")
                        {
                            fgDtls.Rows[i].Cells[5].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricID from fn_FabricMaster_Tbl() where FabricDesignID={0} and FabricQualityID={1} and FabricShadeID={2}", fgDtls.Rows[i].Cells[6].Value, fgDtls.Rows[i].Cells[7].Value, fgDtls.Rows[i].Cells[8].Value)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    //Piece No Increament Required in Feya
                    //if (cboOrderType.SelectedItem.ToString() == "WITHOUT ORDER")
                    {
                        if (fgDtls.Rows.Count > 1)
                        {
                            //int iCount = fgDtls.CurrentRow.Index + 1;
                            //if (iCount < fgDtls.Rows.Count)
                            //{
                            //    for (int i = 3; i <= fgDtls.ColumnCount - 4; i++)
                            //    {
                            //        fgDtls.Rows[iCount].Cells[i].Value = fgDtls.Rows[iCount - 1].Cells[i].Value;
                            //    }
                            //}
                            if (fgDtls.Rows[e.RowIndex - 1].Cells[4].Value.ToString().Trim() != "-")
                            {
                                fgDtls.Rows[e.RowIndex].Cells[4].Value = CommonCls.AutoInc_Runtime(fgDtls.Rows[e.RowIndex - 1].Cells[4].Value.ToString(), Db_Detials.PCS_NO_INCMT);
                            }
                            else
                            {
                                fgDtls.Rows[e.RowIndex].Cells[4].Value = "-";
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (fgDtls.RowCount != 0)
                {
                    if (FAB_SERIALWISE)
                    {
                        if (e.ColumnIndex == 4)
                        {
                            if (fgDtls.Rows[e.RowIndex].Cells[5].Value != null)
                            {
                                using (IDataReader dr = DB.GetRS("Select FabricDesignID,FabricQualityID,FabricShadeID from fn_FabricMaster_Tbl() where FabricID=" + fgDtls.Rows[e.RowIndex].Cells[5].Value + ""))
                                {
                                    while (dr.Read())
                                    {
                                        //fgDtls.Rows.Add();
                                        fgDtls.Rows[e.RowIndex].Cells[6].Value = Localization.ParseNativeInt(dr["FabricDesignID"].ToString());
                                        fgDtls.Rows[e.RowIndex].Cells[7].Value = Localization.ParseNativeInt(dr["FabricQualityID"].ToString());
                                        fgDtls.Rows[e.RowIndex].Cells[8].Value = Localization.ParseNativeInt(dr["FabricShadeID"].ToString());
                                    }
                                }
                            }
                        }
                    }
                    if (!Vld_DupPieceNo)
                    {
                        if (e.ColumnIndex == 2)
                        {
                            string strVal;
                            if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                            {
                                string primaryFieldNameValue = fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString();
                                if ((fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString() != null) && ((fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString().Length > 0)))
                                {
                                    if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString() != "-")
                                    {
                                        strVal = "tbl_StockFabricLedger";
                                        if (Navigate.CheckDuplicate(ref strVal, "BarCodeNo", primaryFieldNameValue, false, "", 0L, " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " and BranchID = " + Db_Detials.BranchID + " and StoreID=" + Db_Detials.StoreID, ""))
                                        {
                                            fgDtls.CurrentCell = fgDtls[2, e.RowIndex];
                                        }
                                    }
                                }
                                else if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString().Length <= 0)
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[4].Value = "-";
                                }
                            }
                            else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                            {
                                if ((fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString() != null) && ((fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString().Length > 0)))
                                {
                                    if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString() != "-")
                                    {
                                        strVal = "tbl_StockFabricLedger";
                                        if (Navigate.CheckDuplicate(ref strVal, "BarCodeNo", fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString(), true, "TransID", Localization.ParseNativeLong(txtCode.Text.Trim()), " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " and BranchID = " + Db_Detials.BranchID + " and StoreID=" + Db_Detials.StoreID, ""))
                                        {
                                            fgDtls.CurrentCell = fgDtls[2, e.RowIndex];
                                        }
                                    }
                                }
                                else if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString().Length <= 0)
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[4].Value = "-";
                                }
                            }
                        }
                    }

                    if (Vld_DupPieceNo)
                    {
                        if (e.ColumnIndex == 2)
                        {
                            string strTbleName;
                            if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                            {
                                string primaryFieldNameValue = fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString();
                                if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString().Length > 0)
                                {
                                    //if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString() != "-")
                                    {
                                        strTbleName = "tbl_StockFabricLedger";
                                        if (Navigate.CheckDuplicate(ref strTbleName, "BarCodeNo", primaryFieldNameValue, false, "", 0L, " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " and BranchID = " + Db_Detials.BranchID + " and StoreID=" + Db_Detials.StoreID, ""))
                                        {
                                            fgDtls.CurrentCell = fgDtls[2, e.RowIndex];
                                        }
                                    }
                                }
                                else if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString().Length <= 0)
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[4].Value = "-";
                                }
                            }
                            else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString() != null && fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString().Length > 0)
                                {
                                    //if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString() != "-")
                                    {
                                        strTbleName = "tbl_StockFabricLedger";
                                        if (Navigate.CheckDuplicate(ref strTbleName, "BarCodeNo", fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString(), true, "TransID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " and BranchID = " + Db_Detials.BranchID + " and StoreID=" + Db_Detials.StoreID, ""))
                                        {
                                            fgDtls.CurrentCell = fgDtls[2, e.RowIndex];
                                        }
                                    }
                                }
                                else if (fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString().Length <= 0)
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[4].Value = "-";
                                }
                            }
                        }
                    }
                    if (((e.ColumnIndex == 5) | (e.ColumnIndex == 7)) && ((fgDtls.Rows[e.RowIndex].Cells[6].Value != null) && (Strings.Trim(Conversions.ToString(fgDtls.Rows[e.RowIndex].Cells[6].Value)).Length > 0)))
                    {
                        fgDtls.Rows[e.RowIndex].Cells[7].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", fgDtls.Rows[e.RowIndex].Cells[6].Value)));
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
                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    if (cboProcesser.SelectedValue == null || cboProcesser.Text.Trim().ToString() == "-" || cboProcesser.SelectedValue.ToString() == "0")
                    {
                        iFabIssueID_Main = 0;
                    }
                    else
                    {
                        iFabIssueID_Main = Localization.ParseNativeInt(DB.GetSnglValue("Select max(FabIssueID+1) from tbl_FabricIssueMain where IsDeleted=0"));
                    }
                }
                else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    if (cboProcesser.SelectedValue == null || cboProcesser.Text.Trim().ToString() == "-" || cboProcesser.SelectedValue.ToString() == "0")
                    {
                        iFabIssueID_Main = 0;
                    }
                    else
                    {
                        iFabIssueID_Main = Localization.ParseNativeInt(DB.GetSnglValue("Select FabIssueID from tbl_FabricIssueMain where FabricInwardID= " + txtCode.Text + " and IsDeleted=0"));
                    }
                }

                ArrayList pArrayData = new ArrayList
                {
                this.frmVoucherTypeID,
                ("(#ENTRYNO#)"),
                (dtEntryDate.TextFormat(false, true)),
                (cboOrderType.SelectedItem),
                (txtRefNo.Text),
                (dtChallanDate.TextFormat(false, true)),
                (cboSupplier.SelectedValue),
                (cboBroker.SelectedValue),
                (cboDepartment.SelectedValue),
                (cboTransport.SelectedValue),
                (txtLrNo.Text),
                (dtLrDate.TextFormat(false, true)),
                (txtDescription.Text.ToString() == "" ? "-" : txtDescription.Text.ToString()),
                (cboProcesser.SelectedValue),
                (iFabIssueID_Main),
                (txtUniqueID.Text),
                (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                (dtEd1.TextFormat(false, true)),
                (txtET1.Text.Trim()),
                (txtET2.Text.Trim()),
                (txtET3.Text.Trim())
                };
                int UnitId = 0;
                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", "(#CodeID#)",
                        Localization.ParseNativeInt(Conversions.ToString(base.iIDentity)));

                for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                {
                    if (fgDtls.Rows[i].Cells[12].Value.ToString().Length > 0)
                    {
                        string strLotNo = "-";
                        if (fgDtls.Rows[i].Cells[3].Value != null && fgDtls.Rows[i].Cells[3].Value.ToString() != "0" && fgDtls.Rows[i].Cells[3].Value.ToString().Length > 0)
                        {
                            strLotNo = fgDtls.Rows[i].Cells[3].Value.ToString();
                        }
                        else
                        {
                            strLotNo = "-";
                        }
                        if (fgDtls.Rows[i].Cells[4].Value.ToString().Length == 0)
                        {
                            fgDtls.Rows[i].Cells[4].Value = "-";
                        }

                        strAdjQry += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)",
                                    (i + 1).ToString(), "(#ENTRYNO#)", dtChallanDate.Text, Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                                    fgDtls.Rows[i].Cells[18].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[18].Value.ToString()),
                                    base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(), base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                    strLotNo, fgDtls.Rows[i].Cells[4].Value.ToString(), fgDtls.Rows[i].Cells[5].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[5].Value.ToString()),
                                    Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[7].Value.ToString()), Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[6].Value.ToString()),
                                    Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[8].Value.ToString()), fgDtls.Rows[i].Cells[9].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[9].Value.ToString()),
                                    Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[10].Value.ToString()), Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[11].Value.ToString()),
                                    Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[12].Value.ToString()), fgDtls.Rows[i].Cells[13].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[13].Value.ToString()),
                                    0, 0, 0, fgDtls.Rows[i].Cells[14].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[14].Value.ToString()), "NULL", fgDtls.Rows[i].Cells[19].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[19].Value.ToString()),
                                    Localization.ParseNativeInt(cboSupplier.SelectedValue.ToString()), "(#CodeID#)", 0, 0, 0,
                                    fgDtls.Rows[i].Cells[20].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[20].Value.ToString()),
                                    fgDtls.Rows[i].Cells[21].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[21].Value.ToString()),
                                    fgDtls.Rows[i].Cells[22].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[22].Value.ToString()),
                                    fgDtls.Rows[i].Cells[23].Value == null || fgDtls.Rows[i].Cells[23].Value.ToString() == "" || fgDtls.Rows[i].Cells[23].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(fgDtls.Rows[i].Cells[23].Value.ToString()),
                                    fgDtls.Rows[i].Cells[24].Value == null || fgDtls.Rows[i].Cells[24].Value.ToString() == "" || fgDtls.Rows[i].Cells[24].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(fgDtls.Rows[i].Cells[24].Value.ToString()),
                                    fgDtls.Rows[i].Cells[25].Value == null || fgDtls.Rows[i].Cells[25].Value.ToString() == "" ? "-" : fgDtls.Rows[i].Cells[25].Value.ToString(),
                                    fgDtls.Rows[i].Cells[26].Value == null || fgDtls.Rows[i].Cells[26].Value.ToString() == "" ? "-" : fgDtls.Rows[i].Cells[26].Value.ToString(),
                                    fgDtls.Rows[i].Cells[27].Value == null || fgDtls.Rows[i].Cells[27].Value.ToString() == "" ? "-" : fgDtls.Rows[i].Cells[27].Value.ToString(),
                                    fgDtls.Rows[i].Cells[28].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[28].Value.ToString()),
                                    fgDtls.Rows[i].Cells[29].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[29].Value.ToString()),
                                    "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                        UnitId = Localization.ParseNativeInt(fgDtls.Rows[i].Cells[10].Value.ToString());
                    }
                }

                if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                {
                    strAdjQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_FabricOrderLedger", "(#CodeID#)", Localization.ParseNativeInt(base.iIDentity.ToString()));
                    for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                    {
                        DataGridViewRow row = fgDtls.Rows[i];
                        if (row.Cells[2].Value != null && row.Cells[2].Value.ToString() != "" && row.Cells[2].Value.ToString() != "0" && row.Cells[12].Value != null && row.Cells[12].Value.ToString() != "" && row.Cells[12].Value.ToString() != "0")
                        {
                            string sBatchNo = string.Empty;
                            sBatchNo = DB.GetSnglValue("Select FabPONo from fn_FabricPurchaseOrderMain_Tbl() Where FabPOID=" + row.Cells[2].Value.ToString());

                            if (Localization.ParseNativeDouble(row.Cells[12].Value.ToString()) > 0)
                            {
                                strAdjQry += DBSp.InsertIntoFabricOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)",
                                        dtChallanDate.Text, Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()), row.Cells[31].Value == null ? "0" : row.Cells[31].Value.ToString() == "" ? "0" : row.Cells[31].Value.ToString(),
                                        "0", row.Cells[2].Value.ToString(), sBatchNo, Localization.ParseNativeDouble(row.Cells[2].Value.ToString()), Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                        Localization.ParseNativeDouble(row.Cells[6].Value.ToString()), Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                        Localization.ParseNativeDouble(row.Cells[10].Value.ToString()), 0, 0, 0, 0, Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                        Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()), 0, 0, "NULL", 0,
                                        row.Cells[20].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[20].Value.ToString()),
                                        row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                        row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                        row.Cells[23].Value == null || row.Cells[23].Value.ToString() == "" || row.Cells[23].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[23].Value.ToString()),
                                        row.Cells[24].Value == null || row.Cells[24].Value.ToString() == "" || row.Cells[24].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[24].Value.ToString()),
                                        row.Cells[25].Value == null || row.Cells[25].Value.ToString() == "" ? "-" : row.Cells[25].Value.ToString(),
                                        row.Cells[26].Value == null || row.Cells[26].Value.ToString() == "" ? "-" : row.Cells[26].Value.ToString(),
                                        row.Cells[27].Value == null || row.Cells[27].Value.ToString() == "" ? "-" : row.Cells[27].Value.ToString(),
                                        row.Cells[28].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[28].Value.ToString()),
                                        row.Cells[29].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[29].Value.ToString()),
                                        "NULL", i, 1, "Purchase", row.Cells[16].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[16].Value.ToString()), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                            }
                            row = null;
                        }
                    }
                }

                //if (cboTransport.SelectedValue != null && Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()) > 0.0)
                //{
                //    strAdjQry += DBSp.InsertIntoTrasportLedger("(#CodeID#)", txtRefNo.Text.ToString(), dtChallanDate.Text,
                //               Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()),
                //               Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()),
                //               Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                //               txtLrNo.Text, dtLrDate.Text, null, UnitId, Localization.ParseNativeInt(string.Format("{0:N0}", CommonCls.GetColSum(fgDtls, 11, -1, -1))),
                //               Localization.ParseNativeDecimal(string.Format("{0:N2}", CommonCls.GetColSum(fgDtls, 12, -1, -1))), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID,
                //               DateAndTime.Now.Date);
                //}
                strAdjQry += string.Format("Delete From tbl_FabricOrderLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                double dblTransID = 0;
                string sSupplierD = cboSupplier.SelectedValue.ToString();

                DBSp.Transcation_AddEdit_Trans(pArrayData, fgDtls, true, ref dblTransID, strAdjQry, "", txtEntryNo.Text, "", "");

                #region  Saving in Dyeing Isse
                try
                {
                    string strDetailQuery = "";
                    string strMainQuery = "";
                    string strStockQry_FI = "";
                    string sDeleteQry = "";
                    bool isMainIns = false;
                    bool isDetIns = false;
                    int iFabisid = 0;
                    int processtypeid = Localization.ParseNativeInt(DB.GetSnglValue("Select MiscID from fn_MiscMaster_Tbl() where MiscName='Dyeing'"));
                    int Issuetypeid = Localization.ParseNativeInt(DB.GetSnglValue("Select MiscID from fn_MiscMaster_Tbl() where MiscName='Fresh'"));
                    int IBrokerid = Localization.ParseNativeInt(DB.GetSnglValue("Select Brokerid from fn_LedgerMasterMain_Tbl() where LedgerId=" + cboProcesser.SelectedValue));
                    int iMaxInwardID = Localization.ParseNativeInt(DB.GetSnglValue("select max(FabricInwardID) from  tbl_FabricInwardMain Where IsDeleted=0"));
                    int iIssueEntno = Localization.ParseNativeInt(DB.GetSnglValue("select max(EntryNo) from  tbl_FabricIssueMain Where IsDeleted=0"));
                    int iIssueId_ForEdit = Localization.ParseNativeInt(DB.GetSnglValue("Select FabIssueID from tbl_FabricIssueMain where FabricInwardID=" + txtCode.Text + " and IsDeleted=0"));
                    int iProcessorId_FI = Localization.ParseNativeInt(DB.GetSnglValue("select ProcessorID from  tbl_FabricInwardMain Where IsDeleted=0 and FabricInwardID=" + iMaxInwardID));
                    int iDeptID = Localization.ParseNativeInt(DB.GetSnglValue("select DepartmentID from  tbl_FabricInwardMain Where IsDeleted=0 and FabricInwardID=" + iMaxInwardID));

                    if (iProcessorId_FI > 0)
                    {
                        if (FIN_CPY_FIS)
                        {
                            if (iIssueId_ForEdit > 0)
                            {
                                using (IDataReader idr1 = DB.GetRS("Select * From tbl_FabricInwardMain Where IsDeleted=0 and FabricInwardID=" + iMaxInwardID))
                                {
                                    while (idr1.Read())
                                    {
                                        strMainQuery += string.Format("UPDATE {0} SET EntryDate={1},ProcesserID={2}, BrokerID={3}, DepartmentID={4}, DeliveryAtID={5}, TransportID={6}, LrNo={7}, LrDate={8} Where FabIssueID={9}",
                                                        "tbl_FabricIssueMain", CommonLogic.SQuote(Localization.ToSqlDateString(idr1["EntryDate"].ToString())), Localization.ParseNativeInt(idr1["ProcessorID"].ToString()),
                                                        Localization.ParseNativeInt(idr1["BrokerID"].ToString()), Localization.ParseNativeInt(idr1["DepartmentID"].ToString()), Localization.ParseNativeInt(idr1["ProcessorID"].ToString()),
                                                        idr1["TransportID"].ToString() == "" ? 0 : Localization.ParseNativeInt(idr1["TransportID"].ToString()), CommonLogic.SQuote(idr1["LrNo"].ToString()),
                                                        idr1["LrDate"].ToString() == "" ? "NULL" : CommonLogic.SQuote(Localization.ToSqlDateString(idr1["LrDate"].ToString())), iIssueId_ForEdit);
                                        isMainIns = true;
                                    }
                                }
                            }
                            else
                            {
                                using (IDataReader idr = DB.GetRS("Select * From tbl_FabricInwardMain Where IsDeleted=0 and FabricInwardID=" + iMaxInwardID))
                                {
                                    while (idr.Read())
                                    {
                                        frmFabricIssue frmfi = new frmFabricIssue();
                                        frmfi.strTableName = "TBL_FABRICISSUEMAIN";
                                        string SRefNo = CommonCls.AutoInc(frmfi, "RefNo", "FabIssueID", "");

                                        if (idr["ProcessorID"].ToString() != null && idr["ProcessorID"].ToString() != "0")
                                        {
                                            strMainQuery += string.Format("Insert Into tbl_FabricIssueMain (VoucherTypeID,EntryNo,EntryDate,RefNo,RefDate,ProcessTypeID,IssueTypeID,ProcesserID,BrokerID,DepartmentID,DeliveryAtID,TransportID,LrNo,LrDate,BatchNo,Description,FabricInwardID,UniqueID,EI1,EI2,ED1,ET1,ET2,ET3,StoreID,CompID,BranchID,YearID,AddedOn,AddedBy,IsModified,ModifiedOn,ModifiedBy,IsDeleted,DeletedOn,DeletedBy,IsCanclled,CancelledOn,CancelledBy,IsApproved,ApprovedOn,ApprovedBy,IsAudited,AuditedOn,AuditedBy) values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31},{32},{33},{34},{35},{36},{37},{38},{39},{40},{41},{42},{43},{44});",
                                                        this.frmVoucherTypeID, iIssueEntno + 1, CommonLogic.SQuote(Localization.ToSqlDateString(idr["EntryDate"].ToString())), CommonLogic.SQuote(SRefNo), CommonLogic.SQuote(Localization.ToSqlDateString(idr["RefDate"].ToString())),
                                                        processtypeid, Issuetypeid, Localization.ParseNativeInt(idr["ProcessorID"].ToString()), IBrokerid, Localization.ParseNativeInt(idr["DepartmentID"].ToString()),
                                                        Localization.ParseNativeInt(idr["DepartmentID"].ToString()), idr["TransportID"].ToString() == "" ? 0 : Localization.ParseNativeInt(idr["TransportID"].ToString()),
                                                        CommonLogic.SQuote(idr["LrNo"].ToString()), idr["LrDate"].ToString() == "" ? "NULL" : CommonLogic.SQuote(Localization.ToSqlDateString(idr["LrDate"].ToString())), "'-'", "NULL", dblTransID, "NULL", 0, 0, 0, "'-'", "'-'", "'-'", Db_Detials.StoreID , Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID,
                                                        CommonLogic.SQuote(Localization.ToSqlDateString(DateAndTime.Now.Date.ToString())), Db_Detials.UserID, 0, "NULL", "NULL", 0, "NULL", "NULL", 0, "NULL", "NULL", 0, "NULL", "NULL", 0, "NULL", "NULL",0,"NULL","NULL",0,"NULL","NULL");
                                            isMainIns = true;
                                        }
                                    }
                                }
                            }
                            if (isMainIns)
                            {
                                DB.ExecuteSQL(strMainQuery);
                            }
                            iIssueEntno = Localization.ParseNativeInt(DB.GetSnglValue("select max(EntryNo) from  tbl_FabricIssueMain Where IsDeleted=0"));
                            if (iIssueId_ForEdit > 0)
                            {
                                //iFabIssueID_Main = iIssueId_ForEdit;
                                strDetailQuery += string.Format("Delete from {0} Where FabIssueID= {1};", "tbl_FabricIssueDtls", iIssueId_ForEdit);
                                strStockQry_FI += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", iIssueId_ForEdit, 359);
                            }
                            else
                            {
                                iFabisid = Localization.ParseNativeInt(DB.GetSnglValue("Select FabIssueID from tbl_FabricIssueMain where EntryNo=(select max(EntryNo) from  tbl_FabricIssueMain Where IsDeleted=0) and IsDeleted=0"));
                                strDetailQuery += string.Format("Delete from {0} Where FabIssueID= {1};", "tbl_FabricIssueDtls", iFabisid);
                                strStockQry_FI += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", iFabisid, 359);
                            }

                            if (isMainIns)
                            {
                                if (iIssueId_ForEdit > 0)
                                {
                                    int iTempRowIndex = 0;
                                    int isubID = 0;
                                    using (IDataReader idr = DB.GetRS("Select * From tbl_FabricInwardDtls Where FabricInwardID=" + iMaxInwardID))
                                    {
                                        while (idr.Read())
                                        {
                                            isubID = iTempRowIndex + 1;
                                            strDetailQuery += string.Format("Insert Into tbl_FabricIssueDtls (FabIssueID,SubFabIssueID,BatchNo,BarcodeNo,FabricID,DesignID,QualityID,ShadeID,GradeID,UnitID,Pcs,Mtrs,Wt,StockValue,InitMtrs,InitWt,DepartmentID,SubDepartmentID,ProductionOrdID,InwLedID,InwTransID,ProcessOrdID,ProcessTypeID,ProgramID,ProcessID,EI1,EI2,EI3,ED1,ED2,ET1,ET2,ET3,EN1,EN2,RefID,ARefID,MainRefID, MyID,TempRowIndex) values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24}, 0, 0, 0, NULL,NULL,'-','-','-', 0, 0, NULL, '{25}','{26}', 0,{27});",
                                                                  iIssueId_ForEdit, Localization.ParseNativeInt(idr["SubFabricInwardID"].ToString()), CommonLogic.SQuote(idr["BatchNo"].ToString()), CommonLogic.SQuote(idr["BarcodeNo"].ToString()),
                                                                  Localization.ParseNativeInt(idr["FabricID"].ToString()), Localization.ParseNativeInt(idr["DesignID"].ToString()), Localization.ParseNativeInt(idr["QualityID"].ToString()), Localization.ParseNativeInt(idr["ShadeID"].ToString()),
                                                                  Localization.ParseNativeInt(idr["GradeID"].ToString()), Localization.ParseNativeInt(idr["UnitID"].ToString()),
                                                                  CommonLogic.SQuote(idr["Cuts"].ToString()), CommonLogic.SQuote(idr["Mtrs"].ToString()),
                                                                  CommonLogic.SQuote(idr["NetWt"].ToString()), 0, 0, 0, iDeptID, Localization.ParseNativeInt(CommonLogic.SQuote(idr["SubDepartmentID"].ToString())), 0, 0, 0, 0, processtypeid, 0, 0, Conversions.ToString(idr["RefID"].ToString()), "359" + "|" + iIssueId_ForEdit + "|" + isubID, iTempRowIndex);
                                            string LotNo = "-";
                                            using (IDataReader idrMain = DB.GetRS("Select * From tbl_FabricIssueMain Where FabIssueID=" + iIssueId_ForEdit + "and IsDeleted=0"))
                                            {
                                                while (idrMain.Read())
                                                {
                                                    strStockQry_FI += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble("359"), iIssueId_ForEdit.ToString(), idr["SubFabricInwardID"].ToString(), iIssueEntno.ToString(),
                                                     (idrMain["RefDate"].ToString()), Localization.ParseNativeDouble(idrMain["ProcesserID"].ToString()), 0,
                                                    359 + "|" + iIssueId_ForEdit + "|" + Localization.ParseNativeInt(idr["SubFabricInwardID"].ToString()), 359 + "|" + iIssueId_ForEdit + "|" + Localization.ParseNativeInt(idr["SubFabricInwardID"].ToString()),
                                                    LotNo, (idr["BarcodeNo"].ToString()), Localization.ParseNativeInt(idr["FabricID"].ToString()), Localization.ParseNativeDouble(idr["QualityID"].ToString()),
                                                    Localization.ParseNativeDouble(idr["DesignID"].ToString()), Localization.ParseNativeDouble(idr["ShadeID"].ToString()),
                                                     idr["GradeID"].ToString() == null ? 0 : Localization.ParseNativeInt(idr["GradeID"].ToString()), Localization.ParseNativeDouble(idr["UnitID"].ToString()), Localization.ParseNativeDecimal(idr["Cuts"].ToString()),
                                                    Localization.ParseNativeDecimal(idr["Mtrs"].ToString()), Localization.ParseNativeDecimal(idr["NetWt"].ToString()),
                                                    0, 0, 0, 0, "null", idr["ProductionOrdID"] == null ? 0 :  Localization.ParseNativeInt(idr["ProductionOrdID"].ToString()), 0, Conversions.ToString(dblTransID), 0, processtypeid, 0,
                                                    idr["EI1"] == null ? 0 : Localization.ParseNativeInt(idr["EI1"].ToString()),
                                                    idr["EI2"] == null ? 0 : Localization.ParseNativeInt(idr["EI2"].ToString()),
                                                    idr["EI3"] == null ? 0 : Localization.ParseNativeInt(idr["EI3"].ToString()),
                                                    idr["ED1"] == null || idr["ED1"].ToString() == "" || idr["ED1"].ToString() == "0" ? "NULL" : Localization.ToSqlDateString(idr["ED1"].ToString()),
                                                    idr["ED2"] == null || idr["ED2"].ToString() == "" || idr["ED2"].ToString() == "0" ? "NULL" : Localization.ToSqlDateString(idr["ED2"].ToString()),
                                                    idr["ET1"] == null || idr["ET1"].ToString() == "" ? "-" : idr["ET1"].ToString(),
                                                    idr["ET2"] == null || idr["ET2"].ToString() == "" ? "-" : idr["ET2"].ToString(),
                                                    idr["ET3"] == null || idr["ET3"].ToString() == "" ? "-" : idr["ET3"].ToString(),
                                                    idr["EN1"] == null ? 0 : Localization.ParseNativeDecimal(idr["EN1"].ToString()),
                                                    idr["EN2"] == null ? 0 : Localization.ParseNativeDecimal(idr["EN2"].ToString()),
                                                    "NULL", Localization.ParseNativeInt(idr["SubFabricInwardID"].ToString()), 1,
                                                    Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                                                    strStockQry_FI += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble("359"), iIssueId_ForEdit.ToString(), idr["SubFabricInwardID"].ToString(), iIssueEntno.ToString(),
                                                    (idrMain["RefDate"].ToString()), Localization.ParseNativeDouble(idrMain["DepartmentID"].ToString()), 0,
                                                    (idr["RefID"].ToString()), "0", (Localization.ParseNativeDouble(idr["BatchNo"].ToString()) != 0 ? Conversions.ToString(idr["BatchNo"].ToString()) : "-"),
                                                    (idr["BarcodeNo"].ToString()), Localization.ParseNativeInt(idr["FabricID"].ToString()), Localization.ParseNativeDouble(idr["QualityID"].ToString()),
                                                    Localization.ParseNativeDouble(idr["DesignID"].ToString()), Localization.ParseNativeDouble(idr["ShadeID"].ToString()), idr["GradeID"].ToString() == null ? 0 : Localization.ParseNativeInt(idr["GradeID"].ToString()),
                                                    Localization.ParseNativeDouble(idr["UnitID"].ToString()), 0, 0, 0, Localization.ParseNativeDecimal(idr["Cuts"].ToString()),
                                                    Localization.ParseNativeDecimal(idr["Mtrs"].ToString()), Localization.ParseNativeDecimal(idr["NetWt"].ToString()),
                                                     0, "null", idr["ProductionOrdID"] == null ? 0 : Localization.ParseNativeInt(idr["ProductionOrdID"].ToString()), 0, Conversions.ToString(dblTransID), 0, 0, 0,
                                                     idr["EI1"] == null ? 0 : Localization.ParseNativeInt(idr["EI1"].ToString()),
                                                    idr["EI2"] == null ? 0 : Localization.ParseNativeInt(idr["EI2"].ToString()),
                                                    idr["EI3"] == null ? 0 : Localization.ParseNativeInt(idr["EI3"].ToString()),
                                                    idr["ED1"] == null || idr["ED1"].ToString() == "" || idr["ED1"].ToString() == "0" ? "NULL" : Localization.ToSqlDateString(idr["ED1"].ToString()),
                                                    idr["ED2"] == null || idr["ED2"].ToString() == "" || idr["ED2"].ToString() == "0" ? "NULL" : Localization.ToSqlDateString(idr["ED2"].ToString()),
                                                    idr["ET1"] == null || idr["ET1"].ToString() == "" ? "-" : idr["ET1"].ToString(),
                                                    idr["ET2"] == null || idr["ET2"].ToString() == "" ? "-" : idr["ET2"].ToString(),
                                                    idr["ET3"] == null || idr["ET3"].ToString() == "" ? "-" : idr["ET3"].ToString(),
                                                    idr["EN1"] == null ? 0 : Localization.ParseNativeDecimal(idr["EN1"].ToString()),
                                                    idr["EN2"] == null ? 0 : Localization.ParseNativeDecimal(idr["EN2"].ToString()),
                                                    "NULL", Localization.ParseNativeInt(idr["SubFabricInwardID"].ToString()), 1,
                                                    Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                                                }
                                            }
                                            isDetIns = true;
                                            iTempRowIndex++;
                                        }
                                    }
                                }
                                else
                                {
                                    int iTempRowIndex = 0;
                                    int isubID = 0;
                                    using (IDataReader idr = DB.GetRS("Select * From tbl_FabricInwardDtls Where FabricInwardID=" + iMaxInwardID))
                                    {
                                        while (idr.Read())
                                        {
                                            isubID = iTempRowIndex + 1;
                                            strDetailQuery += string.Format("Insert Into tbl_FabricIssueDtls (FabIssueID,SubFabIssueID,BatchNo,BarcodeNo,FabricID,DesignID,QualityID,ShadeID,GradeID,UnitID,Pcs,Mtrs,Wt,StockValue,InitMtrs,InitWt,DepartmentID,SubDepartmentID,ProductionOrdID,InwLedID,InwTransID,ProcessOrdID,ProcessTypeID,ProgramID,ProcessID,EI1,EI2,EI3,ED1,ED2,ET1,ET2,ET3,EN1,EN2,RefID,ARefID,MainRefID, MyID,TempRowIndex) values ({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24}, 0, 0, 0, NULL,NULL,'-','-','-', 0, 0, NULL, '{25}','{26}', 0,{27});",
                                                                  iIssueId_ForEdit, Localization.ParseNativeInt(idr["SubFabricInwardID"].ToString()), CommonLogic.SQuote(idr["BatchNo"].ToString()), CommonLogic.SQuote(idr["BarcodeNo"].ToString()),
                                                                  Localization.ParseNativeInt(idr["FabricID"].ToString()), Localization.ParseNativeInt(idr["DesignID"].ToString()), Localization.ParseNativeInt(idr["QualityID"].ToString()), Localization.ParseNativeInt(idr["ShadeID"].ToString()),
                                                                  Localization.ParseNativeInt(idr["GradeID"].ToString()), Localization.ParseNativeInt(idr["UnitID"].ToString()),
                                                                  CommonLogic.SQuote(idr["Cuts"].ToString()), CommonLogic.SQuote(idr["Mtrs"].ToString()),
                                                                  CommonLogic.SQuote(idr["NetWt"].ToString()), 0, 0, 0, iDeptID, Localization.ParseNativeInt(CommonLogic.SQuote(idr["SubDepartmentID"].ToString())), 0, 0, 0, 0, processtypeid, 0, 0, Conversions.ToString(idr["RefID"].ToString()),"359" + "|" + iIssueId_ForEdit + "|" + isubID , iTempRowIndex);

                                            string LotNo = "-";
                                            using (IDataReader idrMain = DB.GetRS("Select * From tbl_FabricIssueMain Where FabIssueID=" + iFabisid + "and IsDeleted=0"))
                                            {
                                                while (idrMain.Read())
                                                {
                                                    strStockQry_FI += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble("359"), iFabisid.ToString(), idr["SubFabricInwardID"].ToString(), iIssueEntno.ToString(),
                                                     (idrMain["RefDate"].ToString()), Localization.ParseNativeDouble(idrMain["ProcesserID"].ToString()), 0,
                                                    359 + "|" + iIssueId_ForEdit + "|" + Localization.ParseNativeInt(idr["SubFabricInwardID"].ToString()), 359 + "|" + iFabisid + "|" + Localization.ParseNativeInt(idr["SubFabricInwardID"].ToString()),
                                                    LotNo, (idr["BarcodeNo"].ToString()), Localization.ParseNativeInt(idr["FabricID"].ToString()), Localization.ParseNativeDouble(idr["QualityID"].ToString()),
                                                    Localization.ParseNativeDouble(idr["DesignID"].ToString()), Localization.ParseNativeDouble(idr["ShadeID"].ToString()),
                                                     idr["GradeID"].ToString() == null ? 0 : Localization.ParseNativeInt(idr["GradeID"].ToString()), Localization.ParseNativeDouble(idr["UnitID"].ToString()), Localization.ParseNativeDecimal(idr["Cuts"].ToString()),
                                                    Localization.ParseNativeDecimal(idr["Mtrs"].ToString()), Localization.ParseNativeDecimal(idr["NetWt"].ToString()),
                                                    0, 0, 0, 0, "null", idr["ProductionOrdID"] == null ? 0 : Localization.ParseNativeInt(idr["ProductionOrdID"].ToString()), 0, Conversions.ToString(dblTransID), 0, processtypeid, 0,
                                                    idr["EI1"] == null ? 0 : Localization.ParseNativeInt(idr["EI1"].ToString()),
                                                    idr["EI2"] == null ? 0 : Localization.ParseNativeInt(idr["EI2"].ToString()),
                                                    idr["EI3"] == null ? 0 : Localization.ParseNativeInt(idr["EI3"].ToString()),
                                                    idr["ED1"] == null || idr["ED1"].ToString() == "" || idr["ED1"].ToString() == "0" ? "NULL" : Localization.ToSqlDateString(idr["ED1"].ToString()),
                                                    idr["ED2"] == null || idr["ED2"].ToString() == "" || idr["ED2"].ToString() == "0" ? "NULL" : Localization.ToSqlDateString(idr["ED2"].ToString()),
                                                    idr["ET1"] == null || idr["ET1"].ToString() == "" ? "-" : idr["ET1"].ToString(),
                                                    idr["ET2"] == null || idr["ET2"].ToString() == "" ? "-" : idr["ET2"].ToString(),
                                                    idr["ET3"] == null || idr["ET3"].ToString() == "" ? "-" : idr["ET3"].ToString(),
                                                    idr["EN1"] == null ? 0 : Localization.ParseNativeDecimal(idr["EN1"].ToString()),
                                                    idr["EN2"] == null ? 0 : Localization.ParseNativeDecimal(idr["EN2"].ToString()),
                                                    "NULL", Localization.ParseNativeInt(idr["SubFabricInwardID"].ToString()), 1,
                                                    Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                                                    strStockQry_FI += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble("359"), iFabisid.ToString(), idr["SubFabricInwardID"].ToString(), iIssueEntno.ToString(),
                                                    (idrMain["RefDate"].ToString()), Localization.ParseNativeDouble(idrMain["DepartmentID"].ToString()), 0,
                                                    (idr["RefID"].ToString()), "0", (Localization.ParseNativeDouble(idr["BatchNo"].ToString()) != 0 ? Conversions.ToString(idr["BatchNo"].ToString()) : "-"),
                                                    (idr["BarcodeNo"].ToString()), Localization.ParseNativeInt(idr["FabricID"].ToString()), Localization.ParseNativeDouble(idr["QualityID"].ToString()),
                                                    Localization.ParseNativeDouble(idr["DesignID"].ToString()), Localization.ParseNativeDouble(idr["ShadeID"].ToString()), idr["GradeID"].ToString() == null ? 0 : Localization.ParseNativeInt(idr["GradeID"].ToString()),
                                                    Localization.ParseNativeDouble(idr["UnitID"].ToString()), 0, 0, 0, Localization.ParseNativeDecimal(idr["Cuts"].ToString()),
                                                    Localization.ParseNativeDecimal(idr["Mtrs"].ToString()), Localization.ParseNativeDecimal(idr["NetWt"].ToString()),
                                                     0, "null", idr["ProductionOrdID"] == null ? 0 : Localization.ParseNativeInt(idr["ProductionOrdID"].ToString()), 0, Conversions.ToString(dblTransID), 0, 0, 0,
                                                     idr["EI1"] == null ? 0 : Localization.ParseNativeInt(idr["EI1"].ToString()),
                                                    idr["EI2"] == null ? 0 : Localization.ParseNativeInt(idr["EI2"].ToString()),
                                                    idr["EI3"] == null ? 0 : Localization.ParseNativeInt(idr["EI3"].ToString()),
                                                    idr["ED1"] == null || idr["ED1"].ToString() == "" || idr["ED1"].ToString() == "0" ? "NULL" : Localization.ToSqlDateString(idr["ED1"].ToString()),
                                                    idr["ED2"] == null || idr["ED2"].ToString() == "" || idr["ED2"].ToString() == "0" ? "NULL" : Localization.ToSqlDateString(idr["ED2"].ToString()),
                                                    idr["ET1"] == null || idr["ET1"].ToString() == "" ? "-" : idr["ET1"].ToString(),
                                                    idr["ET2"] == null || idr["ET2"].ToString() == "" ? "-" : idr["ET2"].ToString(),
                                                    idr["ET3"] == null || idr["ET3"].ToString() == "" ? "-" : idr["ET3"].ToString(),
                                                    idr["EN1"] == null ? 0 : Localization.ParseNativeDecimal(idr["EN1"].ToString()),
                                                    idr["EN2"] == null ? 0 : Localization.ParseNativeDecimal(idr["EN2"].ToString()),
                                                    "NULL", Localization.ParseNativeInt(idr["SubFabricInwardID"].ToString()), 1,
                                                    Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                                                }
                                            }
                                            isDetIns = true;
                                            iTempRowIndex++;
                                        }
                                    }
                                }
                                if (isDetIns)
                                {
                                    DB.ExecuteSQL(strDetailQuery);
                                    DB.ExecuteSQL(strStockQry_FI);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (iIssueId_ForEdit > 0)
                        {
                            sDeleteQry += string.Format("Update Tbl_FabricissueMain SET IsDeleted = 1 Where FabIssueID= {0} and CompID={1} and YearID={2};", iIssueId_ForEdit, Db_Detials.CompID, Db_Detials.YearID);
                            sDeleteQry += string.Format("Delete from {0} Where FabIssueID= {1};", "tbl_FabricIssueDtls", iIssueId_ForEdit);
                            sDeleteQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", iIssueId_ForEdit, 157);
                            DB.ExecuteSQL(sDeleteQry);
                        }
                    }
                }
                catch (Exception ex1)
                {
                    Navigate.logError(ex1.Message, ex1.StackTrace);
                }
                #endregion

                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) || (base.blnFormAction == Enum_Define.ActionType.View_Record))
                {
                    flg_Sms = Localization.ParseBoolean(GlobalVariables.SMS_SEND_FI);
                    flg_Email = Localization.ParseBoolean(GlobalVariables.EMAIL_SEND_FI);

                    if (blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        string sEntryNo = DB.GetSnglValue("SELECT Entryno from fn_FabricInwardMain_Tbl() WHERE FabricInwardID=" + dblTransID);
                        if (flg_Sms == true || flg_Email == true)
                        {
                            if (flg_Sms == true)
                            {
                                try { CommonCls.SendSms(dblTransID.ToString(), base.iIDentity.ToString(), 1, sSupplierD); }
                                catch { }
                            }

                            if (flg_Email == true)
                            {
                                try
                                {
                                    CommonCls.sendEmail(dblTransID.ToString(), sEntryNo, sSupplierD, base.iIDentity.ToString(), false);
                                }
                                catch { }
                            }
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                    {
                        if (flg_Email == true)
                        {
                            string sisactive = DB.GetSnglValue("Select IsActive from tbl_MailingConfig where menuid=" + base.iIDentity.ToString());
                            if (sisactive == "True")
                            {
                                try
                                {
                                    CommonCls.sendEmail(txtCode.Text, txtEntryNo.Text, sSupplierD, base.iIDentity.ToString(), true);
                                }
                                catch { }
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        public void ShowStock()
        {
            if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    try
                    {
                        bool isIndexAppld = false;
                        int iIndex = fgDtls.RowCount - 1;
                        for (int m = 0; m <= fgDtls.RowCount - 1; m++)
                        {
                            if (fgDtls.Rows[m].Cells[7].Value != null && fgDtls.Rows[m].Cells[7].Value.ToString() != "")
                            {
                                iIndex = m;
                                isIndexAppld = true;
                            }
                        }
                        if (!isIndexAppld)
                        {
                            iIndex = -1;
                        }

                        string strQry = "";
                        #region StockAdjQuery
                        int ibitcol = 0;
                        string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", base.iIDentity, 0.0));
                        string strQry_ColName = "";
                        string[] arr = CommonCls.GetAdjColName(base.iIDentity, 0.0).Split(';');
                        strQry_ColName = arr[0].ToString();
                        ibitcol = Localization.ParseNativeInt(arr[1]);
                        string sSupID = string.Empty;

                        if (cboSupplier.SelectedValue == null || cboSupplier.SelectedValue.ToString() == "-" || cboSupplier.SelectedValue.ToString() == "" || cboSupplier.SelectedValue.ToString() == "0")
                        {
                            sSupID = "0";
                        }
                        else
                        {
                            sSupID = cboSupplier.SelectedValue.ToString();
                        }

                        if (!Information.IsDate(dtChallanDate.Text.ToString()))
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Challan Date");
                            dtChallanDate.Focus();
                            return;
                        }

                        strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5} , {6}, {7}) Where OrderTransType ='Purchase' and  RefVoucherID in ({8}) Order by MyId", new object[] { strQry_ColName, snglValue, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(dtChallanDate.Text))), Db_Detials.StoreID, Db_Detials.CompID,  Db_Detials.BranchID, Db_Detials.YearID, sSupID, RefVoucherID });

                        #endregion
                        frmStockAdj frmStockAdj = new frmStockAdj();
                        frmStockAdj.MenuID = base.iIDentity;
                        frmStockAdj.Entity_IsfFtr = 0.0;
                        frmStockAdj.ref_fgDtls = this.fgDtls;
                        frmStockAdj.LedgerID = sSupID;
                        frmStockAdj.QueryString = strQry;
                        frmStockAdj.IsRefQuery = true;
                        frmStockAdj.ibitCol = ibitcol;
                        frmStockAdj.IsStock = false;

                        if (this.OrgInGridArray != null)
                        {
                            frmStockAdj.UsedInGridArray = this.OrgInGridArray;
                        }
                        if (frmStockAdj.ShowDialog() == DialogResult.Cancel)
                        {
                            frmStockAdj.Dispose();
                        }
                        else
                        {
                            frmStockAdj.Dispose();
                            frmStockAdj = null;
                            int iRows = fgDtls.RowCount - 1;
                            for (int i = 0; i < iRows; i++)
                            {
                                if (fgDtls.Rows[i].Index > iIndex || iIndex == -1)
                                {
                                    if ((fgDtls.Rows[i].Cells[11].Value != null) && (fgDtls.Rows[i].Cells[11].Value.ToString() != "0") && (fgDtls.Rows[i].Cells[11].Value.ToString() != ""))
                                    {
                                        //if (fgDtls.Rows[i].Cells[11].Value.ToString() != fgDtls.Rows[i].Cells[31].Value.ToString())
                                        {
                                            double iPcs = Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[11].Value.ToString());

                                            if (Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[34].Value.ToString()) < iPcs)
                                            {
                                                iPcs = Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[34].Value.ToString());
                                            }

                                            if (iPcs > 0)
                                            {
                                                int num8 = (int)Math.Round((double)(iPcs + i));
                                                for (int k = i + 1; k <= num8; k++)
                                                {
                                                    fgDtls.Rows.Insert(k, new DataGridViewRow());
                                                    for (int m = 0; m <= fgDtls.ColumnCount - 1; m++)
                                                    {
                                                        if (m == 11)
                                                        {
                                                            fgDtls.Rows[k].Cells[m].Value = 1;
                                                        }
                                                        else if (m == 1)
                                                        {
                                                            fgDtls.Rows[k].Cells[m].Value = k;
                                                        }
                                                        else if (m != 2 && m != 12)
                                                        {
                                                            fgDtls.Rows[k].Cells[m].Value = fgDtls.Rows[i].Cells[m].Value;
                                                        }
                                                    }
                                                }
                                                fgDtls.Rows.RemoveAt(i);
                                                i = (int)Math.Round((double)(i + (iPcs - 1.0)));
                                                iRows = fgDtls.RowCount - 1;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        fgDtls.Rows[i].Cells[11].Value = fgDtls.Rows[i].Cells[34].Value.ToString();
                                    }
                                }
                            }
                            fgDtls.Rows.RemoveAt(fgDtls.RowCount - 1);

                            DataGridViewEx ex2 = fgDtls;
                            for (int j = 0; j <= ex2.RowCount - 1; j++)
                            {
                                if (j == 0)
                                {
                                    int MaxId = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(FabricInwardID),0) From {0}  Where IsDeleted=0 and  CompID = {1} and YearID = {2}", "tbl_FabricInwardMain", Db_Detials.CompID, Db_Detials.YearID))));
                                    if (INC_PNO_STOCK)
                                    {
                                        if (((fgDtls.RowCount > 0) && (fgDtls.ColumnCount > 0)) && fgDtls.Columns[2].Visible)
                                        {
                                            fgDtls.Rows[0].Cells[4].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select {0}({1},{2})", new object[] { "dbo.fn_FetchPieceNo_Stock", Db_Detials.CompID, Db_Detials.YearID })), Db_Detials.PCS_NO_INCMT);
                                        }
                                        else
                                        {
                                            fgDtls.Rows[0].Cells[4].Value = "-";
                                        }
                                    }
                                    else
                                    {
                                        if (((fgDtls.RowCount > 0) & (fgDtls.ColumnCount > 0)) & fgDtls.Columns[2].Visible)
                                        {
                                            fgDtls.Rows[0].Cells[4].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select  {0}({1},{2},{3},{4})", "dbo.fn_FetchPieceNo", MaxId, base.iIDentity, Db_Detials.CompID, Db_Detials.YearID)), Db_Detials.PCS_NO_INCMT);
                                        }
                                        else
                                        {
                                            fgDtls.Rows[0].Cells[4].Value = "-";
                                        }
                                    }
                                }
                                else if (j > 0)
                                {
                                    if (ex2.Rows[j - 1].Cells[4].Value.ToString() != "-")
                                    {
                                        ex2.Rows[j].Cells[4].Value = CommonCls.AutoInc_Runtime(ex2.Rows[j - 1].Cells[4].Value.ToString(), Db_Detials.PCS_NO_INCMT);
                                    }
                                    else
                                    {
                                        ex2.Rows[j].Cells[4].Value = "-";
                                    }
                                }
                            }
                            CalcVal();
                            SendKeys.Send("{TAB}");
                            if (fgDtls.Rows.Count > 0)
                            {
                                fgDtls.CurrentCell = fgDtls[11, 0];
                            }
                            fgDtls.Select();
                            setTempRowIndex();
                            setMyID_Orders();
                            ExecuterTempQry_Orders(-1);

                            if (fgDtls.RowCount == 0)
                            {
                                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Navigate.logError(ex.Message, ex.StackTrace);
                    }
                }
            }
        }

        public bool ValidateForm()
        {
            try
            {
                if (!EventHandles.IsValidGridReq(this.fgDtls, base.dt_AryIsRequired))
                {
                    return true;
                }
                if (!EventHandles.IsRequiredInGrid(fgDtls, this.dt_AryIsRequired, false))
                {
                    return true;
                }

                if (!CommonCls.CheckDate(dtEntryDate.Text, true))
                    return true;

                if (!CommonCls.CheckDate(dtChallanDate.Text, true))
                    return true;

                if (!CommonCls.CheckDate(dtLrDate.Text, true))
                    return true;

                if (txtEntryNo.Text.Trim() == "" || txtEntryNo.Text.Trim() == "-" || txtEntryNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry No.");
                    return true;
                }
                if (!Information.IsDate(Strings.Trim(dtEntryDate.Text.ToString())))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry Date");
                    dtEntryDate.Focus();
                    return true;
                }
                if (cboOrderType.SelectedItem.ToString() == "" || cboOrderType.Text.Trim().ToString() == "-")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Order Type");
                    cboOrderType.Focus();
                    return true;
                }
                if (txtRefNo.Text.Trim() == "" || txtRefNo.Text.Trim() == "-" || txtRefNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Challan No.");
                    txtRefNo.Focus();
                    return true;
                }
                if (!Information.IsDate(dtChallanDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Challan Date");
                    dtChallanDate.Focus();
                    return true;
                }
                if (cboSupplier.SelectedValue == null || cboSupplier.Text.Trim().ToString() == "-" || cboSupplier.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Supplier");
                    cboSupplier.Focus();
                    return true;
                }
                if (!cboSupplier.IsValidSelect)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Valid Supplier");
                    cboSupplier.Focus();
                    return true;
                }
                if (cboDepartment.SelectedValue == null || cboDepartment.Text.Trim().ToString() == "-" || cboDepartment.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    cboDepartment.Focus();
                    return true;
                }
                if (!this.cboDepartment.IsValidSelect)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Valid Department");
                    cboDepartment.Focus();
                    return true;
                }
                if (txtRefNo.Text.Trim().Length > 0)
                {
                    string strTable;
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        strTable = "tbl_FabricInwardMain";
                        if (Navigate.CheckDuplicate(ref strTable, "RefNo", txtRefNo.Text, false, "", 0, " SupplierID = " + cboSupplier.SelectedValue + " AND CompID = " + Db_Detials.CompID + " And YearID = " + Db_Detials.YearID + " and BranchID =" + Db_Detials.BranchID + " and StoreID=" + Db_Detials.StoreID, "This Party already used this Challan No in Entry No : " + DB.GetSnglValue(string.Format("Select Entryno from {0} where Supplierid = {1} and RefNo = {2} ", "tbl_FabricInwardMain", cboSupplier.SelectedValue, txtRefNo.Text.Trim()))))
                        {
                            txtRefNo.Focus();
                            return true;
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        strTable = "tbl_FabricInwardMain";
                        if (Navigate.CheckDuplicate(ref strTable, "RefNo", txtRefNo.Text, true, "FabricInwardID", Localization.ParseNativeLong(txtCode.Text), " SupplierID = " + cboSupplier.SelectedValue + " AND CompID = " + Db_Detials.CompID + " And YearID = " + Db_Detials.YearID + " and BranchID =" + Db_Detials.BranchID + " and StoreID=" + Db_Detials.StoreID, "This Party already used this Challan No in Entry No : " + DB.GetSnglValue(string.Format("Select Entryno from {0} where Supplierid = {1} and RefNo = {2} ", "tbl_FabricInwardMain", cboSupplier.SelectedValue, txtRefNo.Text.Trim()))))
                        {
                            txtRefNo.Focus();
                            return true;
                        }
                    }
                }
                this.CalcVal();
                if (Vld_DupPieceNo)
                {
                    if (CheckDupPieceNo())
                    {
                        return true;
                    }
                }
                if (FAB_SERIALWISE)
                {
                    if (CheckSerialCombn())
                    {
                        return true;
                    }
                }
                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    fgDtls.Rows[i].Cells[17].Value = cboDepartment.SelectedValue;
                }
                //CheckOrders();
                //if (isRet)
                //{
                //    return true;
                //}
                return false;
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                return false;
            }
        }

        private void cboSupplier_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (((base.blnFormAction == Enum_Define.ActionType.New_Record) && (cboSupplier.SelectedValue != null)) && Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()) > 0)
                {
                    cboBroker.SelectedValue = Localization.ParseNativeDouble(DB.GetSnglValue(string.Format("Select BrokerID From {0} Where LedgerID = {1} ", "tbl_LedgerMaster", cboSupplier.SelectedValue)));
                    cboTransport.SelectedValue = Localization.ParseNativeDouble(DB.GetSnglValue(string.Format("Select TransportID From {0} Where LedgerID = {1} ", "tbl_LedgerMaster", cboSupplier.SelectedValue)));
                    string sqlQuery = string.Format("Select DISTINCT FabPOID, FabPONo ,FabPoDate,QualityID,Quality,BalPcs,BalMtrs from {0}({1},{2},{3}) Where BalMtrs > 0", new object[] { "fn_FetchFabPurchaseOrderDtls", cboSupplier.SelectedValue, Db_Detials.CompID, Db_Detials.YearID });
                    //Combobox_Setup.Fill_Combo(this.cboOrderNo, sqlQuery, "FabPONo,FabPoDate,QualityID,Quality,BalPcs,BalMtrs", "FabPOID");
                    //CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboOrderNo = this.cboOrderNo;
                    //cboOrderNo.ColumnWidths = "0;100;0;0;100;50;80";
                    //cboOrderNo.AutoComplete = true;
                    //cboOrderNo.AutoDropdown = true;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record || base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                    {
                        btnSelectStock.Enabled = true;
                        fgDtls.Columns[2].Visible = true;
                        fgDtls.Columns[5].Visible = true;
                        fgDtls.Columns[6].ReadOnly = true;
                        fgDtls.Columns[7].ReadOnly = true;
                        fgDtls.Columns[8].ReadOnly = true;
                        fgDtls.Columns[10].ReadOnly = true;
                        //ReadOnly False For Part Receiving
                        fgDtls.Columns[11].ReadOnly = false;
                        fgDtls.Columns[12].ReadOnly = false;
                        fgDtls.Columns[13].ReadOnly = false;
                    }
                    else
                    {
                        btnSelectStock.Enabled = false;
                        fgDtls.Columns[2].Visible = false;
                        fgDtls.Columns[5].Visible = false;
                        fgDtls.Columns[6].ReadOnly = false;
                        fgDtls.Columns[7].ReadOnly = false;
                        fgDtls.Columns[8].ReadOnly = false;
                        fgDtls.Columns[10].ReadOnly = false;
                        fgDtls.Columns[11].ReadOnly = false;
                        fgDtls.Columns[12].ReadOnly = false;
                        fgDtls.Columns[13].ReadOnly = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public void PrintRecord()
        {
            try
            {
                CIS_ReportTool.frmMultiPrint frmMultiPrint = new CIS_ReportTool.frmMultiPrint();
                CIS_ReportTool.frmMultiPrint.MenuID = base.iIDentity;
                CIS_ReportTool.frmMultiPrint.Id = Localization.ParseNativeInt(this.txtCode.Text);
                CIS_ReportTool.frmMultiPrint.TblNm = "tbl_FabricInwardMain";
                CIS_ReportTool.frmMultiPrint.IdStr = "FabricInwardID";
                CIS_ReportTool.frmMultiPrint.iCompID = Db_Detials.CompID;
                CIS_ReportTool.frmMultiPrint.iYearID = Db_Detials.YearID;
                CIS_ReportTool.frmMultiPrint.iUserID = Db_Detials.UserID;
                CIS_ReportTool.frmMultiPrint.objReport = Db_Detials.objReport;
                CIS_ReportTool.frmMultiPrint.sApplicationName = GetAssemblyInfo.ProductName;

                if (frmMultiPrint.ShowDialog() == DialogResult.Cancel)
                {
                    frmMultiPrint.Dispose();
                }
                else
                {
                    frmMultiPrint = null;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btnSelectStock_Click(object sender, EventArgs e)
        {

        }

        protected void fgDtls_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                if ((e.Control == true & e.KeyCode == Keys.D) | e.KeyCode == Keys.F5)
                {
                    //-- Calc Values
                    object frm = Navigate.GetActiveChild();
                    dynamic frmObj = frm;
                    int iCalcCol = 0;
                    CIS_DataGridViewEx.DataGridViewEx fgDtls = (CIS_DataGridViewEx.DataGridViewEx)sender;

                    string sRefID = "";
                    try
                    {
                        sRefID = fgDtls.Rows[fgDtls.CurrentRow.Index].Cells["RefID"].Value.ToString();
                    }
                    catch { sRefID = "0"; }

                    if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        try
                        {
                            if ((Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_StockFabricLedger() WHERE RefID='" + sRefID + "' AND TransType<>" + frmObj.iIDentity + "")) > 0) && sRefID != "" && (sRefID) != "0" && (sRefID) != "-" || (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_FabricPurchaseMain_Tbl() WHERE FabInwardID=" + fgDtls.Rows[fgDtls.CurrentRow.Index].Cells["FabricInwardID"].Value.ToString() + " and CompID=" + Db_Detials.CompID)) > 0))
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityWarning, "", "Reference Found In Another Module..Row Cannot Be Deleted");
                                return;
                            }
                            else
                            {
                                try
                                {
                                    string strQry = string.Format("Update tbl_FabricOrderLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[33].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                                    DB.ExecuteSQL(strQry);
                                }
                                catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }

                                fgDtls.Rows.RemoveAt(fgDtls.CurrentRow.Index);

                                DataTable AryCalcvalue = ((DataTable)frmObj.dt_AryCalcvalue);
                                DataRow[] dtRow_AryCalcvalue = AryCalcvalue.Select("SubGridID = " + fgDtls.Grid_UID);

                                for (int i = 0; i <= (dtRow_AryCalcvalue.Length - 1); i++)
                                {
                                    string[] strValue = dtRow_AryCalcvalue[i]["ColCalcValue"].ToString().Split(',');
                                    string strColInsert = string.Empty;

                                    for (int j = 0; j <= (strValue.Length - 1); j++)
                                    {
                                        if (j == (strValue.Length - 1))
                                        {
                                            if (strValue[j].ToString() != "+")
                                                break; // TODO: might not be correct. Was : Exit For
                                            iCalcCol = Localization.ParseNativeInt(dtRow_AryCalcvalue[i]["ColIndex"].ToString());
                                        }
                                    }
                                }

                                if (iCalcCol != -1)
                                {
                                    for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                                    {
                                        fgDtls.Rows[i].Cells[iCalcCol].Value = i + 1;
                                    }
                                }

                                DataTable table2 = (DataTable)frmObj.dt_HasDtls_Grd;
                                DataTable table3 = (DataTable)frmObj.dt_AryCalcvalue;
                                DataTable table4 = (DataTable)frmObj.dt_AryIsRequired;
                                if (fgDtls.RowCount == 0)
                                {
                                    EventHandles.CreateDefault_Rows(fgDtls, table2, table3, table4, false, false);
                                }
                                else
                                {
                                    EventHandles.CreateDefault_Rows(fgDtls, table2, table3, table4, true, false);
                                }
                            }
                        }
                        catch { }
                    }
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        try
                        {
                            try
                            {
                                string strQry = string.Format("Update tbl_FabricOrderLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[33].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                                DB.ExecuteSQL(strQry);
                            }
                            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }

                            fgDtls.Rows.RemoveAt(fgDtls.CurrentRow.Index);

                            DataTable AryCalcvalue = ((DataTable)frmObj.dt_AryCalcvalue);
                            DataRow[] dtRow_AryCalcvalue = AryCalcvalue.Select("SubGridID = " + fgDtls.Grid_UID);

                            for (int i = 0; i <= (dtRow_AryCalcvalue.Length - 1); i++)
                            {
                                string[] strValue = dtRow_AryCalcvalue[i]["ColCalcValue"].ToString().Split(',');
                                string strColInsert = string.Empty;

                                for (int j = 0; j <= (strValue.Length - 1); j++)
                                {
                                    if (j == (strValue.Length - 1))
                                    {
                                        if (strValue[j].ToString() != "+")
                                            break; // TODO: might not be correct. Was : Exit For
                                        iCalcCol = Localization.ParseNativeInt(dtRow_AryCalcvalue[i]["ColIndex"].ToString());
                                    }
                                }
                            }

                            if (iCalcCol != -1)
                            {
                                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                                {
                                    fgDtls.Rows[i].Cells[iCalcCol].Value = i + 1;
                                }
                            }

                            DataTable table2 = (DataTable)frmObj.dt_HasDtls_Grd;
                            DataTable table3 = (DataTable)frmObj.dt_AryCalcvalue;
                            DataTable table4 = (DataTable)frmObj.dt_AryIsRequired;
                            if (fgDtls.RowCount == 0)
                            {
                                EventHandles.CreateDefault_Rows(fgDtls, table2, table3, table4, false, false);
                            }
                            else
                            {
                                EventHandles.CreateDefault_Rows(fgDtls, table2, table3, table4, true, false);
                            }
                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private bool CheckDupPieceNo()
        {
            string strTbleName;

            if (Vld_DupPieceNo)
            {
                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                    {
                        string primaryFieldNameValue = fgDtls.Rows[i].Cells[4].Value.ToString();
                        if (fgDtls.Rows[i].Cells[4].Value.ToString() != null && fgDtls.Rows[i].Cells[4].Value.ToString().Length > 0)
                        {
                            //if (fgDtls.Rows[i].Cells[4].Value.ToString() != "-")
                            {
                                strTbleName = "fn_StockFabricLedger_tbl()";
                                if (Navigate.CheckDuplicate(ref strTbleName, "BarCodeNo", primaryFieldNameValue, false, "", 0L, " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "", ""))
                                {
                                    fgDtls.CurrentCell = fgDtls[2, i];
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                }

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    for (int j = 0; j <= fgDtls.RowCount - 1; j++)
                    {
                        if (fgDtls.Rows[j].Cells[4].Value.ToString() != null && fgDtls.Rows[j].Cells[4].Value.ToString().Length > 0)
                        {
                            //if (fgDtls.Rows[j].Cells[4].Value.ToString() != "-")
                            {
                                strTbleName = "fn_StockFabricLedger_tbl()";
                                if (Navigate.CheckDuplicate(ref strTbleName, "BarCodeNo", fgDtls.Rows[j].Cells[4].Value.ToString(), true, "TransID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), " CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "", ""))
                                {
                                    fgDtls.CurrentCell = fgDtls[2, j];
                                    return true;
                                }
                            }
                        }
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        private bool CheckSerialCombn()
        {
            int iDesignID, iQualityID, iShadeID;
            for (int i = 0; i <= fgDtls.RowCount - 1; i++)
            {
                if (fgDtls.Rows[i].Cells[5].Value != null && fgDtls.Rows[i].Cells[5].Value.ToString() != "0" && fgDtls.Rows[i].Cells[5].Value.ToString() != "-")
                {
                    using (IDataReader dr = DB.GetRS("Select DesignID,QualityID,ShadeID from fn_FabricMaster() where SerialID=" + fgDtls.Rows[i].Cells[5].Value + ""))
                    {
                        while (dr.Read())
                        {
                            iDesignID = Localization.ParseNativeInt(dr["DesignID"].ToString());
                            iQualityID = Localization.ParseNativeInt(dr["QualityID"].ToString());
                            iShadeID = Localization.ParseNativeInt(dr["ShadeID"].ToString());
                            if (iDesignID != Localization.ParseNativeInt(fgDtls.Rows[i].Cells[6].Value.ToString()) || iQualityID != Localization.ParseNativeInt(fgDtls.Rows[i].Cells[7].Value.ToString()) || iShadeID != Localization.ParseNativeInt(fgDtls.Rows[i].Cells[8].Value.ToString()))
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Check Serial Combination");
                                fgDtls.CurrentCell = fgDtls[4, i];
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Serial");
                    fgDtls.CurrentCell = fgDtls[4, i];
                    return true;
                }

            }
            return false;
        }

        public string SetSupplier
        {
            get
            {
                return cboSupplier.SelectedValue.ToString();
            }
            set
            {
                if (value.Length != 0)
                {
                    cboSupplier.SelectedValue = value;
                }
            }
        }

        private void GetRefModID()
        {
            #region Multiple series Mapping
            try
            {
                RefVoucherID = "";

                if (RefMenuID == 0)
                {
                    RefMenuID = iIDentity;
                }

                string sMappingID = DB.GetSnglValue(string.Format("Select MappingIDs from tbl_VoucherTypeMaster Where GenMenuID=" + iIDentity + ""));
                if (sMappingID != "")
                {
                    string[] arr = sMappingID.Split(',');
                    for (int i = 0; i <= arr.Length - 1; i++)
                    {
                        RefVoucherID += DB.GetSnglValue(string.Format("Select SeriesID From fn_MenuMaster_tbl() Where MenuID=" + arr[i] + "")) + ",";
                    }
                    RefVoucherID = RefVoucherID.Remove(RefVoucherID.Length - 1);
                }
                else
                {
                    RefVoucherID = "0";
                }
            }
            catch { }
            #endregion
        }

        public void ExecuterTempQry_Orders(int RowIndex)
        {
            if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
            {
                if (((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record)))
                {
                    try
                    {
                        int StatusID = 1;
                        string MyID = "";

                        if (txtUniqueID.Text != null && txtUniqueID.Text != "")
                        {
                            string strQry = "";
                            if (RowIndex == -1)
                            {
                                strQry = string.Format("Delete From tbl_FabricOrderLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                                for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                                {
                                    DataGridViewRow row = fgDtls.Rows[i];
                                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                                    {
                                        StatusID = 1;
                                        MyID = iMaxMyID_Orders.ToString();
                                    }
                                    else
                                    {
                                        StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_FabricOrderLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_FabricOrderLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + "")));
                                        MyID = txtCode.Text;
                                    }

                                    if (MyID != "" && row.Cells[2].Value != null && row.Cells[2].Value.ToString() != "" && row.Cells[2].Value.ToString() != "0" && row.Cells[12].Value != null && row.Cells[12].Value.ToString() != "" && row.Cells[12].Value.ToString() != "0")
                                    {
                                        string sBatchNo = string.Empty;
                                        sBatchNo = DB.GetSnglValue("Select FabPONo from fn_FabricPurchaseOrderMain_Tbl() Where FabPOID=" + row.Cells[2].Value.ToString());

                                        if (Localization.ParseNativeDouble(row.Cells[12].Value.ToString()) > 0)
                                        {
                                            strQry += DBSp.InsertIntoFabricOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()),  MyID, (i + 1).ToString(), txtEntryNo.Text,
                                               dtChallanDate.Text, Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()), row.Cells[31].Value == null ? "0" : row.Cells[31].Value.ToString() == "" ? "0" : row.Cells[31].Value.ToString(),
                                               "0", row.Cells[2].Value.ToString(), sBatchNo, Localization.ParseNativeDouble(row.Cells[2].Value.ToString()), Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                               Localization.ParseNativeDouble(row.Cells[6].Value.ToString()), Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                               Localization.ParseNativeDouble(row.Cells[10].Value.ToString()), 0, 0, 0, 0, Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                               Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()), 0, 0, "NULL", 0,
                                               row.Cells[20].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[20].Value.ToString()),
                                               row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                               row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                               row.Cells[23].Value == null || row.Cells[23].Value.ToString() == "" || row.Cells[23].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[23].Value.ToString()),
                                               row.Cells[24].Value == null || row.Cells[24].Value.ToString() == "" || row.Cells[24].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[24].Value.ToString()),
                                               row.Cells[25].Value == null || row.Cells[25].Value.ToString() == "" ? "-" : row.Cells[25].Value.ToString(),
                                               row.Cells[26].Value == null || row.Cells[26].Value.ToString() == "" ? "-" : row.Cells[26].Value.ToString(),
                                               row.Cells[27].Value == null || row.Cells[27].Value.ToString() == "" ? "-" : row.Cells[27].Value.ToString(),
                                               row.Cells[28].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[28].Value.ToString()),
                                               row.Cells[29].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[29].Value.ToString()),
                                               txtUniqueID.Text, i, StatusID, "Purchase", row.Cells[16].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[16].Value.ToString()), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if ((fgDtls.CurrentCell.ColumnIndex == 11) || (fgDtls.CurrentCell.ColumnIndex == 12) || (fgDtls.CurrentCell.ColumnIndex == 13))
                                {
                                    DataGridViewRow row = fgDtls.Rows[RowIndex];
                                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                                    {
                                        StatusID = 1;
                                        MyID = iMaxMyID_Orders.ToString();
                                    }
                                    else
                                    {
                                        StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_FabricOrderLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_FabricOrderLedger_tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + "")));
                                        MyID = txtCode.Text;
                                    }

                                    if (MyID != "" && row.Cells[2].Value != null && row.Cells[2].Value.ToString() != "" && row.Cells[2].Value.ToString() != "0" && row.Cells[12].Value != null && row.Cells[12].Value.ToString() != "" && row.Cells[12].Value.ToString() != "0")
                                    {
                                        string sBatchNo = string.Empty;
                                        sBatchNo = DB.GetSnglValue("Select FabPONo from fn_FabricPurchaseOrderMain_Tbl() Where FabPOID=" + row.Cells[2].Value.ToString());

                                        if (txtUniqueID.Text != null)
                                        {
                                            strQry += string.Format("Delete From tbl_FabricOrderLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and RowIndex=" + fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[33].Value + " and AddedBy=" + Db_Detials.UserID + ";");

                                            strQry += DBSp.InsertIntoFabricOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), MyID, (RowIndex + 1).ToString(), txtEntryNo.Text,
                                               dtChallanDate.Text, Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()), row.Cells[31].Value == null ? "0" : row.Cells[31].Value.ToString() == "" ? "0" : row.Cells[31].Value.ToString(),
                                               "0", row.Cells[2].Value.ToString(), sBatchNo, Localization.ParseNativeDouble(row.Cells[2].Value.ToString()), Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                               Localization.ParseNativeDouble(row.Cells[6].Value.ToString()), Localization.ParseNativeDouble(row.Cells[8].Value.ToString()),
                                               Localization.ParseNativeDouble(row.Cells[10].Value.ToString()), 0, 0, 0, 0, Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                               Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()), 0, 0, "NULL", 0,
                                               row.Cells[20].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[20].Value.ToString()),
                                               row.Cells[21].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[21].Value.ToString()),
                                               row.Cells[22].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[22].Value.ToString()),
                                               row.Cells[23].Value == null || row.Cells[23].Value.ToString() == "" || row.Cells[23].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[23].Value.ToString()),
                                               row.Cells[24].Value == null || row.Cells[24].Value.ToString() == "" || row.Cells[24].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[24].Value.ToString()),
                                               row.Cells[25].Value == null || row.Cells[25].Value.ToString() == "" ? "-" : row.Cells[25].Value.ToString(),
                                               row.Cells[26].Value == null || row.Cells[26].Value.ToString() == "" ? "-" : row.Cells[26].Value.ToString(),
                                               row.Cells[27].Value == null || row.Cells[27].Value.ToString() == "" ? "-" : row.Cells[27].Value.ToString(),
                                               row.Cells[28].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[28].Value.ToString()),
                                               row.Cells[29].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[29].Value.ToString()),
                                               txtUniqueID.Text, Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[33].Value.ToString()), StatusID, "Purchase", row.Cells[16].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[16].Value.ToString()), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                                        }
                                    }
                                }
                            }
                            if (strQry != "" && strQry.Length > 0)
                            {
                                DB.ExecuteSQL(strQry);
                            }
                        }
                    }
                    catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                }
            }
        }

        private void setMyID_Orders()
        {
            iMaxMyID_Orders = Localization.ParseNativeInt(DB.GetSnglValue("Select MAX(MyId + 1) from tbl_FabricOrderLedger Where IsDeleted=0"));

            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[32].Value = iMaxMyID_Orders;
            }
        }

        private void setTempRowIndex()
        {
            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[33].Value = i;
            }
        }

        private void frmFabricInward_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (strUniqueID != null)
            {
                string strQry = string.Format("Delete From tbl_FabricOrderLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                strQry = strQry + string.Format("Update  tbl_FabricOrderLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                DB.ExecuteSQL(strQry);
                strQry = string.Format("Update tbl_FabricOrderLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                DB.ExecuteSQL(strQry);
                strQry = "";
            }
        }

        //private void CheckOrders()
        //{
        //    try
        //    {
        //        if (base.blnFormAction == Enum_Define.ActionType.New_Record)
        //        {
        //            if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
        //            {
        //                string SOrderID = "", SUnitID = "", SOrderValidnType = "";
        //                int iCurrentpcs = 0;
        //                double dCurrentMtrs = 0;
        //                isRet = false;

        //                DataTable dtLocalC = new DataTable();

        //                foreach (DataGridViewColumn col in fgDtls.Columns)
        //                {
        //                    dtLocalC.Columns.Add(col.HeaderText, col.ValueType);
        //                }
        //                foreach (DataGridViewRow gridRow in fgDtls.Rows)
        //                {
        //                    DataRow dtRow = dtLocalC.NewRow();
        //                    for (int i = 0; i < fgDtls.Columns.Count; i++)
        //                        dtRow[i] = (gridRow.Cells[i].Value == null ? DBNull.Value : gridRow.Cells[i].Value);
        //                    dtLocalC.Rows.Add(dtRow);
        //                }

        //                for (int j = 0; j <= fgDtls.RowCount - 1; j++)
        //                {
        //                    DataGridViewRow row1 = fgDtls.Rows[j];
        //                    if (row1.Cells[3].Value != null && row1.Cells[3].Value.ToString() != "" && row1.Cells[3].Value.ToString() != "-" && row1.Cells[3].Value.ToString() != "0")
        //                    {
        //                        SOrderID += row1.Cells[3].Value + ",";
        //                    }
        //                }
        //                SOrderID = SOrderID.Remove(SOrderID.Length - 1);

        //                string[] sordarray = SOrderID.Split(',');
        //                string[] tmparray = sordarray;

        //                for (int k = 0; k <= tmparray.Length - 1; k++)
        //                {
        //                    if (Localization.ParseNativeInt(sordarray[0]) >= 0)
        //                    {
        //                        DataRow[] dr_orderID = dtLocalC.Select("Order='" + Localization.ParseNativeInt(sordarray[0]) + "'");
        //                        if (dr_orderID.Length > 0)
        //                        {
        //                            foreach (DataRow r in dr_orderID)
        //                            {
        //                                DataRow[] dr_SngRefID = dtLocalC.Select("ARefID='" + r["ARefID"].ToString() + "'");
        //                                if (dr_SngRefID.Length > 0)
        //                                {
        //                                    foreach (DataRow dr_row in dr_SngRefID)
        //                                    {
        //                                        iCurrentpcs += Localization.ParseNativeInt(dr_row["Pieces"].ToString());
        //                                        dCurrentMtrs += Localization.ParseNativeDouble(dr_row["Meters"].ToString());
        //                                    }
        //                                }

        //                                int iBalPcs = Localization.ParseNativeInt(DB.GetSnglValue("Select Balpcs from fn_FetchFabPurchaseOrderDtls(" + cboSupplier.SelectedValue + "," + Db_Detials.CompID + "," + Db_Detials.YearID + ") Where RefID=" + CommonLogic.SQuote(r["ARefID"].ToString()) + "and FabPOID=" + sordarray[0]));
        //                                double dBalMtrs = Localization.ParseNativeDouble(DB.GetSnglValue("Select Balmtrs from fn_FetchFabPurchaseOrderDtls(" + cboSupplier.SelectedValue + "," + Db_Detials.CompID + "," + Db_Detials.YearID + ") Where RefID=" + CommonLogic.SQuote(r["ARefID"].ToString()) + "and FabPOID=" + sordarray[0]));
        //                                SUnitID = (DB.GetSnglValue("Select UnitID from fn_FetchFabPurchaseOrderDtls(" + cboSupplier.SelectedValue + "," + Db_Detials.CompID + "," + Db_Detials.YearID + ") Where RefID=" + CommonLogic.SQuote(r["ARefID"].ToString()) + "and FabPOID=" + sordarray[0]));
        //                                if (SUnitID != "" && SUnitID.Length > 0)
        //                                {
        //                                    SOrderValidnType = DB.GetSnglValue("Select OrderValidationType from tbl_UnitsMaster Where UnitID= " + SUnitID + " and IsDeleted=0");
        //                                }

        //                                if (SOrderValidnType == "P")
        //                                {
        //                                    if (iBalPcs <= 0)
        //                                    {
        //                                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "Not Enough Bal Pieces For the Order No " + DB.GetSnglValue("select FabPONo from fn_FabricPurchaseOrderMain_Tbl() where FabPOID=" + sordarray[0]));
        //                                        isRet = true;
        //                                        return;
        //                                    }
        //                                    else if (iCurrentpcs > iBalPcs)
        //                                    {
        //                                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "Current Pieces " + iCurrentpcs + " Exceeding Bal Pieces  " + iBalPcs + "  For the Order No " + DB.GetSnglValue("select FabPONo from fn_FabricPurchaseOrderMain_Tbl() where FabPOID=" + sordarray[0]));
        //                                        isRet = true;
        //                                        return;
        //                                    }
        //                                }
        //                                if (SOrderValidnType == "M")
        //                                {
        //                                    if (dBalMtrs <= 0)
        //                                    {
        //                                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "Not Enough Bal Meters For the Order No " + DB.GetSnglValue("select FabPONo from fn_FabricPurchaseOrderMain_Tbl() where FabPOID=" + sordarray[0]));
        //                                        isRet = true;
        //                                        return;
        //                                    }

        //                                    if (ExcessInward_Mtrs <= 0)
        //                                    {
        //                                        if (dCurrentMtrs > dBalMtrs)
        //                                        {
        //                                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "Current Meters " + dCurrentMtrs + " Exceeding Bal Meters  " + dBalMtrs + "  For the Order No " + DB.GetSnglValue("select FabPONo from fn_FabricPurchaseOrderMain_Tbl() where FabPOID=" + sordarray[0]));
        //                                            isRet = true;
        //                                            return;
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        double dPercMtrs = (double)ExcessInward_Mtrs / (double)100 * dBalMtrs;
        //                                        dBalMtrs = dBalMtrs + dPercMtrs;
        //                                        if (dCurrentMtrs > dBalMtrs)
        //                                        {
        //                                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "Current Meters " + dCurrentMtrs + " Exceeding Bal Meters  " + dBalMtrs + "  For the Order No " + DB.GetSnglValue("select FabPONo from fn_FabricPurchaseOrderMain_Tbl() where FabPOID= " + sordarray[0]) + " After Excess Inward of  " + ExcessInward_Mtrs + "%");
        //                                            isRet = true;
        //                                            return;
        //                                        }
        //                                    }
        //                                }
        //                                iCurrentpcs = 0;
        //                                dCurrentMtrs = 0;
        //                            }
        //                            //sordarray = sordarray.Length - sordarray.Length - 1;
        //                            int numIdx = 0;
        //                            List<string> tmp = new List<string>(sordarray);
        //                            tmp.RemoveAt(numIdx);
        //                            sordarray = tmp.ToArray();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Navigate.logError(ex.Message, ex.StackTrace);
        //    }
        //}
    }
}
