using CIS_DataGridViewEx;
using CIS_MultiColumnComboBox;
using CIS_Bussiness;
using CIS_CLibrary;
using CIS_DBLayer;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Data;

namespace CIS_Textil
{
    public partial class frmYarnPurchaseOrder : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;
        private string SRateCalcType = string.Empty;

        public frmYarnPurchaseOrder()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
        }

        #region Form Events
        private void frmYarnPurchaseOrder_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboAgentName, Combobox_Setup.ComboType.Mst_Brokers, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
                Combobox_Setup.FillCbo(ref cboDelivaryAt, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboPartyName, Combobox_Setup.ComboType.Mst_Suppliers, "");
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                txtEntryNo.Enabled = false;
                if (blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    FillControls();
                }
                dtEntryDate.Focus();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region Form Navigation
        public void MovetoField()
        {
            try
            {
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                this.txtOrderNo.Text = CommonCls.AutoInc(this, "YarnPONo", "YarnPOID", "");
                EventHandles.CreateDefault_Rows(fgDtls, dt_HasDtls_Grd, dt_AryCalcvalue, dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                int MaxiD = Localization.ParseNativeInt(DB.GetSnglValue(string.Format(" Select Isnull(Max(YarnPOID),0) From {0} Where StoreID = {1} And CompID = {2} And BranchID = {3} And YearID = {4} ", "tbl_YarnPurchaseOrderMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID)));
                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where YarnPOID = {1} and StoreID = {2} and CompID = {3} and BranchID = {4} and YearID = {5}", new object[] { "tbl_YarnPurchaseOrderMain", MaxiD, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                        dtOrderDate.Text = Localization.ToVBDateString(reader["YarnPODate"].ToString());
                        cboPartyName.SelectedValue = Localization.ParseNativeInt(reader["SupplierID"].ToString());
                        cboAgentName.SelectedValue = Localization.ParseNativeInt(reader["BrokerID"].ToString());
                        cboTransport.SelectedValue = Localization.ParseNativeInt(reader["TransportID"].ToString());
                        cboDelivaryAt.SelectedValue = Localization.ParseNativeInt(reader["DeliveryAtID"].ToString());
                    }
                }
                dtEntryDate.Focus();
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
                DBValue.Return_DBValue(this, txtCode, "YarnPOID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtOrderNo, "YarnPONo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtOrderDate, "YarnPODate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboPartyName, "SupplierID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboAgentName, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDelivaryAt, "DeliveryAtID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCreditdays, "CrDays", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDescription, "Narration", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtED1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "YarnPOID", this.txtCode.Text, base.dt_HasDtls_Grd);
                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                }
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", exception1.Message);
            }
        }

        

        public void SaveRecord()
        {
            string sTotQty = "", sTotWeight = "", sTotAmt = "";
            try
            {
                sTotQty = string.Format("{0:N0}", CommonCls.GetColSum(this.fgDtls, 6, -1, -1));
                sTotWeight = string.Format("{0:N3}", CommonCls.GetColSum(this.fgDtls, 7, -1, -1));
                sTotAmt = string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 9, -1, -1));
                ArrayList pArrayData = new ArrayList
                {
                    "(#ENTRYNO#)",
                    dtEntryDate.TextFormat(false, true),
                    ("(#OTHERNO#)"),
                    dtOrderDate.TextFormat(false, true),
                    cboPartyName.SelectedValue,
                    cboAgentName.SelectedValue,
                    cboTransport.SelectedValue,
                    cboDelivaryAt.SelectedValue,
                    txtCreditdays.Text.ToString(),
                    sTotQty.Replace(",",""),
                    sTotWeight.Replace(",",""),
                    sTotAmt.Replace(",",""),
                    txtDescription.Text.ToString(),
                    (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                    (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                    (dtED1.TextFormat(false, true)),
                    (txtET1.Text.Trim()),
                    (txtET2.Text.Trim()),
                    (txtET3.Text.Trim())
                };

                string sBatchNo = string.Empty;
                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_YarnOrderLedger", "(#CodeID#)", Localization.ParseNativeInt(base.iIDentity.ToString()));
                for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    if (row.Cells[2].Value != null && row.Cells[2].Value.ToString() != "" && row.Cells[7].Value != null && row.Cells[7].Value.ToString() != "")
                    {
                        if (Localization.ParseNativeDouble(row.Cells[7].Value.ToString()) > 0)
                        {
                            strAdjQry += DBSp.InsertIntoYarnOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)", (i + 1).ToString(),
                                    "(#ENTRYNO#)", dtOrderDate.Text,
                                    Localization.ParseNativeDouble(cboPartyName.SelectedValue.ToString()),
                                    base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                    base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                    0, sBatchNo, dtEntryDate.Text, 
                                    Localization.ParseNativeDouble(row.Cells[2].Value.ToString()), Localization.ParseNativeDouble(row.Cells[3].Value.ToString()),
                                    Localization.ParseNativeDouble(row.Cells[4].Value.ToString()), Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                    Localization.ParseNativeDecimal(row.Cells[6].Value.ToString()),
                                    0,
                                    Localization.ParseNativeDecimal(row.Cells[9].Value.ToString()),
                                    0, 0, 0, 
                                    0, 
                                    row.Cells[11].Value == null ? "NULL" : row.Cells[11].Value.ToString().Trim() == "" ? "NULL" : row.Cells[11].Value.ToString(),
                                    row.Cells[12].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[12].Value.ToString()),
                                    row.Cells[13].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                                    row.Cells[14].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[14].Value.ToString()),
                                    row.Cells[15].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[15].Value.ToString()),
                                    row.Cells[16].Value == null || row.Cells[16].Value.ToString() == "" || row.Cells[16].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[16].Value.ToString()),
                                    row.Cells[17].Value == null || row.Cells[17].Value.ToString() == "" || row.Cells[17].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[17].Value.ToString()),
                                    row.Cells[18].Value == null || row.Cells[18].Value.ToString() == "" ? "-" : row.Cells[18].Value.ToString(),
                                    row.Cells[19].Value == null || row.Cells[19].Value.ToString() == "" ? "-" : row.Cells[19].Value.ToString(),
                                    row.Cells[20].Value == null || row.Cells[20].Value.ToString() == "" ? "-" : row.Cells[20].Value.ToString(),
                                    row.Cells[21].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[21].Value.ToString()),
                                    row.Cells[22].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[22].Value.ToString()),
                                    "NULL", i, 1, "Purchase", 0,
                                    Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                        }
                        row = null;
                    }
                }
                DBSp.Transcation_AddEdit(pArrayData, this.fgDtls, true, strAdjQry, "", txtEntryNo.Text, txtOrderNo.Text, "YarnPONo");
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
                if (!EventHandles.IsValidGridReq(this.fgDtls, base.dt_AryIsRequired))
                {
                    return true;
                }
                if ((!EventHandles.IsRequiredInGrid(fgDtls, this.dt_AryIsRequired, false)))
                {
                    return true;
                }
                if (!CommonCls.CheckDate(this.dtEntryDate.Text, true))
                {
                    dtEntryDate.Focus();
                    return true;
                }
                if (!Information.IsDate(dtOrderDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Order Date.");
                    dtOrderDate.Focus();
                    return true;
                }
                if (!CommonCls.CheckDate(this.dtOrderDate.Text, true))
                {
                    dtOrderDate.Focus();
                    return true;
                }
                if (txtOrderNo.Text.Trim() == "" || txtOrderNo.Text.Trim() == "-" || txtOrderNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Order No.");
                    txtOrderNo.Focus();
                    return true;
                }
                if (cboPartyName.SelectedValue == null || cboPartyName.SelectedValue.ToString() == "-" || cboPartyName.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Party ");
                    cboPartyName.Focus();
                    return true;
                }

                if ((txtOrderNo.Text != null) && (txtOrderNo.Text.Trim().Length > 0))
                {
                    string strTblName;
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        strTblName = "tbl_YarnPurchaseOrderMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "YarnPONo", txtOrderNo.Text, false, "", 0L, string.Format("StoreID = {0} and CompID = {1} and BranchID = {2} and YearID = {3}", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID), "This Order No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where YarnPONo = '{1}' and StoreID = {2} and CompID = {3} and BranchID = {4} and YearID = {5}", new object[] { "tbl_YarnPurchaseOrderMain", this.txtOrderNo.Text.ToString(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }))))
                        {
                            txtOrderNo.Focus();
                            return true;
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        strTblName = "tbl_YarnPurchaseOrderMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "YarnPONo", txtOrderNo.Text, true, "YarnPOID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), string.Format("StoreID = {0} and CompID = {1} and BranchID = {2} and YearID = {3}", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID), "This Order No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where YarnPONo = '{1}' and StoreID = {2} and CompID = {3} and BranchID = {4} and YearID = {5}", new object[] { "tbl_YarnPurchaseOrderMain", txtOrderNo.Text.ToString(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }))))
                        {
                            txtOrderNo.Focus();
                            return true;
                        }
                    }
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

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (base.blnFormAction == Enum_Define.ActionType.New_Record | base.blnFormAction == Enum_Define.ActionType.Edit_Record)
            {
                SRateCalcType = "";
                if (fgDtls.Rows[e.RowIndex].Cells[5].Value != null && fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString() != "" && fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString() != "-")
                {
                    SRateCalcType = DB.GetSnglValue("Select RateCalcType from tbl_UnitsMaster Where UnitID=" + fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString() + " and IsDeleted=0");
                }
                switch (e.ColumnIndex)
                {
                    case 5:
                        goto case 6;

                    case 8:
                        goto case 6;

                    case 6:
                        if (SRateCalcType == "B")
                        {
                            if (fgDtls.Rows[e.RowIndex].Cells[6].Value != null && fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString() != "" && fgDtls.Rows[e.RowIndex].Cells[8].Value != null && fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString() != "")
                            {
                                fgDtls.Rows[e.RowIndex].Cells[9].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString())).ToString()));
                            }
                        }
                        else if ((SRateCalcType == "W"))
                        {
                            if (fgDtls.Rows[e.RowIndex].Cells[7].Value != null && fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString() != "" && fgDtls.Rows[e.RowIndex].Cells[8].Value != null && fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString() != "")
                            {
                                fgDtls.Rows[e.RowIndex].Cells[9].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString())).ToString()));
                            }
                        }
                        break;

                    case 7:
                        if (SRateCalcType == "B")
                        {
                            if (fgDtls.Rows[e.RowIndex].Cells[6].Value != null && fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString() != "" && fgDtls.Rows[e.RowIndex].Cells[8].Value != null && fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString() != "")
                            {
                                fgDtls.Rows[e.RowIndex].Cells[9].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString())).ToString()));
                            }
                        }
                        else if ((SRateCalcType == "W"))
                        {
                            if (fgDtls.Rows[e.RowIndex].Cells[7].Value != null && fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString() != "" && fgDtls.Rows[e.RowIndex].Cells[8].Value != null && fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString() != "")
                            {
                                fgDtls.Rows[e.RowIndex].Cells[9].Value = (Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString())).ToString()));
                            }
                        }
                        break;

                    case 9:
                        if (SRateCalcType == "B")
                        {
                            if (fgDtls.Rows[e.RowIndex].Cells[6].Value != null && fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[9].Value != null && fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString() != "0")
                            {
                                if (Localization.ParseNativeDouble(Operators.DivideObject(fgDtls.Rows[e.RowIndex].Cells[9].Value, fgDtls.Rows[e.RowIndex].Cells[6].Value).ToString()) != Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString()))
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[8].Value = (Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString()));
                                }
                            }
                        }
                        else if ((SRateCalcType == "W"))
                        {
                            if (fgDtls.Rows[e.RowIndex].Cells[7].Value != null && fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[9].Value != null && fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString() != "0")
                            {
                                if (Localization.ParseNativeDouble(Operators.DivideObject(fgDtls.Rows[e.RowIndex].Cells[9].Value, fgDtls.Rows[e.RowIndex].Cells[7].Value).ToString()) != Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString()))
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[8].Value = (Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[7].Value.ToString()));
                                }
                            }
                        }
                        break;
                }
            }
        }

        private void txtOrderNo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if ((txtOrderNo.Text != null) && (txtOrderNo.Text.Trim().Length > 0))
                {
                    string strTblName;
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        strTblName = "tbl_YarnPurchaseOrderMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "YarnPONo", this.txtOrderNo.Text, false, "", 0L, string.Format("StoreID = {0} and CompID = {1} and BranchID = {2} and YearID = {3}", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID), "This Order No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where YarnPONo = {1} and StoreID = {2} and CompID = {3} and BranchID = {4} and YearID = {5}", new object[] { "tbl_YarnPurchaseOrderMain", this.txtOrderNo.Text.ToString(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }))))
                        {
                            txtOrderNo.Focus();
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        strTblName = "tbl_YarnPurchaseOrderMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "YarnPONo", this.txtOrderNo.Text, true, "YarnPOID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), string.Format("StoreID = {0} and CompID = {1} and BranchID = {2} and YearID = {3}", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID), "This Order No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where YarnPONo = {1} and StoreID = {2} and CompID = {3} and BranchID = {4} and YearID = {5}", new object[] { "tbl_YarnPurchaseOrderMain", this.txtOrderNo.Text.ToString(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }))))
                        {
                            txtOrderNo.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboPartyName_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if ((cboPartyName.Text != null) && (Localization.ParseNativeDouble(cboPartyName.SelectedValue.ToString())) > 0.0)
                {
                    cboAgentName.SelectedValue = Localization.ParseNativeDouble(DB.GetSnglValue(string.Format("Select BrokerID From {0} Where LedgerID = {1} ", "tbl_LedgerMaster", cboPartyName.SelectedValue)));
                    cboTransport.SelectedValue = Localization.ParseNativeDouble(DB.GetSnglValue(string.Format("Select TransportID From {0} Where LedgerID = {1} ", "tbl_LedgerMaster", cboPartyName.SelectedValue)));
                    cboDelivaryAt.SelectedValue = RuntimeHelpers.GetObjectValue(cboPartyName.SelectedValue);
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
    }
}
