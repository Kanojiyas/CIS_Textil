using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_DBLayer;
using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class frmCatalogInvoice : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        public DataGridViewEx fgDtls_f;
        public DataGridViewEx fgDtls_f_footer;
        private int _Mtrs_Unit;

        bool FS_BRK_COM = false;
        private bool flg_OrderConform;
        public ArrayList OrgInGridArray;
        private bool flg_Sms;
        private bool flg_Email;
        private static string RefVoucherID;
        private int RefMenuID;

        public frmCatalogInvoice()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;

            fgDtls_f = GrdDtls.fgDtls;
            fgDtls_f_footer = GrdDtls.fgDtls_f;

            OrgInGridArray = new ArrayList();
            _Mtrs_Unit = 0;
        }

        #region Event

        private void frmCatalogInvoice_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboDepartment, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboParty, Combobox_Setup.ComboType.Mst_CustomerWithVAT, "");
                Combobox_Setup.FillCbo(ref cboBroker, Combobox_Setup.ComboType.Mst_Brokers, "");
                Combobox_Setup.FillCbo(ref CboDeliveryAt, Combobox_Setup.ComboType.Mst_Customer, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
                Combobox_Setup.FillCbo(ref cboSalesAc, Combobox_Setup.ComboType.SalesAc, "");
                FS_BRK_COM = Localization.ParseBoolean(GlobalVariables.FS_BRK_COM);
                _Mtrs_Unit = Localization.ParseNativeInt(DB.GetSnglValue("SELECT UnitID from tbl_Unitsmaster WHERE UnitName like'Meters'").ToString());

                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls_f, fgDtls_f_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, true, true, 0, 1, true);

                txtEntryNo.Enabled = false;
                this.cboParty.Leave += new System.EventHandler(this.cboParty_Leave);
                flg_Email = Localization.ParseBoolean(GlobalVariables.EMAIL_SEND_FabInv_Serial);
                flg_Sms = Localization.ParseBoolean(GlobalVariables.SMS_SEND_FabInv_Serial);
                GetRefModID();

                fgDtls.CellValueChanged += new DataGridViewCellEventHandler(fgDtls_CellValueChanged);
                fgDtls_f.CellParsing += new DataGridViewCellParsingEventHandler(fgDtls_f_CellParsing);
                fgDtls_f.CellValueChanged += new DataGridViewCellEventHandler(fgDtls_f_CellValueChanged);
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
                DBValue.Return_DBValue(this, txtCode, "CatSalesID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                try
                {
                    string sOrderType = DBValue.Return_DBValue(this, "OrderType");
                    cboOrderType.SelectedItem = sOrderType;
                }
                catch { }
                DBValue.Return_DBValue(this, txtBillNo, "BillNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtBillDate, "BillDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboParty, "PartyID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDepartment, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, CboDeliveryAt, "HasteID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLrNo, "LrNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtLrDate, "LrDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboSalesAc, "SalseAcID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCrDays, "CrDays", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtDueDate, "DueDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtDescription1, "Description1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, TxtTotalPcs, "TotPcs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, TxtTotMtrs, "TotMtrs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, TxtGrossAmount, "GrossAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAddLessAmt, "AddLessAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtNetAmt, "NetAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtNarration, "Description2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEd1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                //DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "CatSalesID", txtCode.Text, base.dt_HasDtls_Grd, this.iIDentity, GlobalVariables.VALIDATE_EDIT, Db_Detials.CompID.ToString(), 2);
                //DetailGrid_Setup.FillGrid(fgDtls_f, this.fgDtls_f.Grid_UID, this.fgDtls_f.Grid_Tbl, "CatSalesID", txtCode.Text, base.dt_HasDtls_Grd);

                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "CatSalesID", txtCode.Text, base.dt_HasDtls_Grd, this.iIDentity, GlobalVariables.VALIDATE_EDIT, Db_Detials.CompID.ToString(), 2);
                DetailGrid_Setup.FillGrid(fgDtls_f, this.fgDtls_f.Grid_UID, this.fgDtls_f.Grid_Tbl, "CatSalesID", txtCode.Text, base.dt_HasDtls_Grd);

                CalcVal();

                if (Localization.ParseNativeInt(txtCode.Text.Trim()) > 0)
                {
                    if (cboParty.SelectedValue != null)
                    {
                        if ((Localization.ParseNativeInt(cboParty.SelectedValue.ToString()) > 0))
                        {
                            fgdtls_f_f.DataSource = DB.GetDT(string.Format("Select * from {0} ({1},{2},{3},{4},{5},{6}) Where OrderTransType='Sales' and  RefVoucherID in ({7}) and OrderID = {8}", "fn_FetchCatalogOrders", CommonLogic.SQuote(Localization.ToSqlDateString(dtBillDate.Text.ToString())), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboParty.SelectedValue.ToString(), RefVoucherID, fgDtls.Rows[fgDtls.CurrentCell.RowIndex].Cells[fgDtls.CurrentCell.ColumnIndex].Value), false);
                            //fgdtls_f_f.DataSource = DB.GetDT(string.Format("Select BookSalesID,BookSONo,BookSODate,CatalogID,BookName,OrderPcs,DispatchPcs, 0 as CDispatchPcs,BalPcs, Rate  from {0}({1},{2},{3})", "fn_FetchBookSalesOrderForInvoice", cboParty.SelectedValue, Db_Detials.CompID, Db_Detials.YearID), false);
                            ////fgdtls_f_f.DataSource = DB.GetDT(string.Format("Select  CAST('False' as Bit) As [Sel], BookSalesID,BookSONo,BookSODate,CatalogID,BookName,OrderPcs,DispatchPcs, 0 as CDispatchPcs,BalPcs, Rate  from {0}({1},{2},{3})", "fn_FetchBookSalesOrderForInvoice", cboParty.SelectedValue, Db_Detials.CompID, Db_Detials.YearID), false);
                            foreach (UltraGridBand band in fgdtls_f_f.DisplayLayout.Bands)
                            {
                                foreach (UltraGridColumn column in band.Columns)
                                {
                                    using (IDataReader dr = DB.GetRS(string.Format("Select * From {0} Where GridID = {1} and SubGridID = 2 and ColIndex = {2}", Db_Detials.tbl_GridSettings, iIDentity, column.Index)))
                                    {
                                        while (dr.Read())
                                        {
                                            column.Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
                                            column.Hidden = Localization.ParseBoolean(dr["IsHidden"].ToString());
                                            column.CellActivation = Activation.NoEdit;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
            if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
            {
                EventHandles.CreateDefault_Rows(this.fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
            }
        }

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                try
                {
                    cboOrderType.SelectedItem = "WITH ORDER";
                }
                catch { }
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                this.txtBillNo.Text = (CommonCls.AutoInc(this, "BillNo", "CatSalesID", ""));
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CreateDefault_Rows(fgDtls_f, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);

                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                EventHandles.CalculateFooter_Rows(fgDtls_f, fgDtls_f_footer, fgDtls_f.Grid_ID.ToString(), fgDtls_f.Grid_UID);
                int MaxID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format(" Select Isnull(Max(CatSalesID),0) From {0}  where IsDeleted=0 and CompID={1} and YearID={2} and BranchID = {3} and StoreID = {4}", "tbl_CatalogSalesMain", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.StoreID)));
                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where IsDeleted=0 and CatSalesID = {1} and CompID={2} and YearID={3} and BranchID = {4} and StoreID = {5}", new object[] { "tbl_CatalogSalesMain", MaxID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.StoreID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = (Localization.ToVBDateString(reader["EntryDate"].ToString()));
                        dtBillDate.Text = (Localization.ToVBDateString(reader["BillDate"].ToString()));
                        cboParty.SelectedValue = Localization.ParseNativeInt(reader["PartyID"].ToString());
                        cboDepartment.SelectedValue = Localization.ParseNativeInt(reader["DepartmentID"].ToString());
                        cboBroker.SelectedValue = Localization.ParseNativeInt(reader["BrokerID"].ToString());
                        CboDeliveryAt.SelectedValue = Localization.ParseNativeInt(reader["HasteID"].ToString());
                        cboTransport.SelectedValue = Localization.ParseNativeInt(reader["TransportID"].ToString());
                        cboSalesAc.SelectedValue = Localization.ParseNativeInt(reader["SalseAcID"].ToString());
                        dtLrDate.Text = (Localization.ToVBDateString(reader["LrDate"].ToString()));
                        cboSalesAc.SelectedValue = Localization.ParseNativeInt(reader["SalseAcID"].ToString());

                        try
                        {
                            if (reader["OrderType"].ToString() != "")
                                cboOrderType.SelectedItem = reader["OrderType"].ToString();
                            else
                                cboOrderType.SelectedItem = "WITH ORDER";
                        }
                        catch { cboOrderType.SelectedItem = "WITH ORDER"; }
                    }
                }

                dtEntryDate.Focus();
                TxtTotalPcs.Text = "0";
                TxtTotMtrs.Text = "0.00";
                TxtGrossAmount.Text = "0.00";
                txtAddLessAmt.Text = "0.00";
                txtNetAmt.Text = "0.00";
                flg_OrderConform = false;
                fgdtls_f_f.DataSource = null;
                //foreach (UltraGridBand Band in fgdtls_f_f.DisplayLayout.Bands)
                //{
                //    foreach (UltraGridColumn Column in Band.Columns)
                //    {
                //        using (IDataReader dr = DB.GetRS(String.Format("Select * From {0} Where GridID = {1} and SubGridID = 2 and ColIndex = {2}", "tbl_GridSettings", iIDentity, Column.Index)))
                //        {
                //            while (dr.Read())
                //            {
                //                Column.Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
                //                Column.Hidden = Localization.ParseBoolean(dr["IsHidden"].ToString());
                //                Column.CellActivation = (Localization.ParseBoolean(dr["IsEditable"].ToString()) == true ? Activation.AllowEdit : Activation.NoEdit);
                //            }
                //        }
                //    }
                //}
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
                ArrayList pArrayData = new ArrayList
                {
                    this.frmVoucherTypeID,
                    ("(#ENTRYNO#)"),
                    (dtEntryDate.TextFormat(false, true)),
                    (cboOrderType.SelectedItem.ToString()),
                    ("(#OTHERNO#)"),
                    (dtBillDate.TextFormat(false, true)),
                    (cboParty.SelectedValue),
                    (cboDepartment.SelectedValue),
                    (cboBroker.SelectedValue),
                    (cboTransport.SelectedValue),
                    (txtLrNo.Text.ToString()),
                    (dtLrDate.TextFormat(false, true)),
                    (cboSalesAc.SelectedValue),
                    (txtCrDays.Text.ToString()),
                    (dtDueDate.TextFormat(false, true)),
                    (txtDescription1.Text.ToString() == ""?"-": txtDescription1.Text.ToString()),
                    (TxtTotalPcs.Text.ToString().Replace(",", "")),
                    (TxtTotMtrs.Text.ToString().Replace(",", "")),
                    (TxtGrossAmount.Text.ToString().Replace(",", "")),
                    (txtAddLessAmt.Text.ToString().Replace(",", "")),
                    (txtNetAmt.Text.ToString().Replace(",", "")),
                    (txtNarration.Text.ToString() == ""?"-": txtNarration.Text.ToString()),
                    (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                    (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                    (dtEd1.TextFormat(false, true)),
                    (txtET1.Text.Trim()),
                    (txtET2.Text.Trim()),
                    (txtET3.Text.Trim())
                };

                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_AcLedger", "(#CodeID#)", base.iIDentity.ToString());
                strAdjQry = strAdjQry + string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_VatLedger", "(#CodeID#)", base.iIDentity);
                strAdjQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockCataLogledger", "(#CodeID#)", Localization.ParseNativeInt(Conversions.ToString(base.iIDentity)));
                int UnitID = Localization.ParseNativeInt(DB.GetSnglValue("Select UnitID From fn_UnitsMaster_tbl() Where UnitName ='Pieces'"));

                strAdjQry += DBSp.InsertInto_AcLedger("(#CodeID#)", "0", "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                cboParty.SelectedValue.ToString(), 1, Db_Detials.Ac_AdjType.NewRef, "(#CodeID#)", "(#OTHERNO#)", dtBillDate.Text,
                                Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDecimal(txtNetAmt.Text.ToString().Replace(",", "")), 0,
                                txtNarration.Text.Trim(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                strAdjQry += DBSp.InsertInto_AcLedger("(#CodeID#)", "0", "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                cboSalesAc.SelectedValue.ToString(), 2, Db_Detials.Ac_AdjType.NewRef, "(#CodeID#)", "(#OTHERNO#)", dtBillDate.Text,
                                Localization.ParseNativeDouble(base.iIDentity.ToString()), 0, Localization.ParseNativeDecimal(TxtGrossAmount.Text.ToString().Replace(",", "")),
                                txtNarration.Text.Trim(), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);

                DataGridViewEx ex = this.fgDtls_f;
                double dblDedAmt = 0.0;

                for (int i = 0; i <= (ex.RowCount - 1); i++)
                {
                    DataGridViewRow row = ex.Rows[i];
                    if (row.Cells[2].Value != null)
                    {
                        if (Localization.ParseNativeDouble(row.Cells[2].Value.ToString()) > 0)
                        {
                            if (Operators.ConditionalCompareObjectEqual(row.Cells[3].FormattedValue, "-", false))
                            {
                                dblDedAmt = -Localization.ParseNativeDouble(row.Cells[6].Value.ToString());
                            }
                            else if (Operators.ConditionalCompareObjectEqual(row.Cells[3].FormattedValue, "+", false))
                            {
                                dblDedAmt = Localization.ParseNativeDouble(row.Cells[6].Value.ToString());
                            }

                            if (dblDedAmt > 0.0)
                            {
                                strAdjQry = strAdjQry + DBSp.InsertInto_AcLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text,
                                    Localization.ParseNativeDouble(base.iIDentity.ToString()), row.Cells[2].Value.ToString(), 2, Db_Detials.Ac_AdjType.OnAccount,
                                    "(#CodeID#)", "(#OTHERNO#)", dtBillDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()), 0,
                                    Localization.ParseNativeDecimal(dblDedAmt.ToString()), "null", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID,
                                    DateAndTime.Now.Date);
                            }
                            else
                            {
                                string sDedAmt = dblDedAmt.ToString();
                                if (sDedAmt.StartsWith("-"))
                                {
                                    sDedAmt = sDedAmt.Substring(1);
                                }
                                dblDedAmt = Localization.ParseNativeDouble(sDedAmt.ToString());
                                strAdjQry = strAdjQry + DBSp.InsertInto_AcLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text,
                                    Localization.ParseNativeDouble(base.iIDentity.ToString()), row.Cells[2].Value.ToString(), 2, Db_Detials.Ac_AdjType.OnAccount,
                                    "(#CodeID#)", "(#OTHERNO#)", dtBillDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                    Localization.ParseNativeDecimal(dblDedAmt.ToString()), 0, "null", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID,
                                    DateAndTime.Now.Date);
                            }
                        }
                    }
                    row = null;
                }

                for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    if (row.Cells[5].Value != null && row.Cells[5].Value.ToString() != "" && row.Cells[5].Value.ToString() != "0" && row.Cells[8].Value != null && row.Cells[8].Value.ToString() != "" && row.Cells[8].Value.ToString() != "0")
                    {
                        strAdjQry += DBSp.InsertIntoCatalogStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtBillDate.Text,
                                        Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()), row.Cells[13].Value == null ? 0 : row.Cells[13].Value.ToString() == "" ? 0 : Localization.ParseNativeInt(row.Cells[13].Value.ToString()),
                                        row.Cells[28].Value == null ? "0" : row.Cells[28].Value.ToString() == "" ? "0" : row.Cells[28].Value.ToString(), row.Cells[30].Value == null ? "0" : row.Cells[30].Value.ToString() == "" ? "0" : row.Cells[30].Value.ToString(),
                                        "NULL", row.Cells[4].Value == null ? "0" : row.Cells[4].Value.ToString() == "" ? "0" : row.Cells[4].Value.ToString(),
                                        Localization.ParseNativeDouble(row.Cells[5].Value.ToString()), Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                        0, Localization.ParseNativeDecimal(row.Cells[8].Value.ToString()), 0, row.Cells[14].Value == null ? "NULL" : row.Cells[14].Value.ToString() == "" ? "NULL" : row.Cells[14].Value.ToString(),
                                        row.Cells[16].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[16].Value.ToString()),
                                        row.Cells[17].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                        row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                        row.Cells[19].Value == null || row.Cells[19].Value.ToString() == "" || row.Cells[19].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[19].Value.ToString()),
                                        row.Cells[20].Value == null || row.Cells[20].Value.ToString() == "" || row.Cells[20].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[20].Value.ToString()),
                                        row.Cells[21].Value == null || row.Cells[21].Value.ToString() == "" ? "-" : row.Cells[21].Value.ToString(),
                                        row.Cells[22].Value == null || row.Cells[22].Value.ToString() == "" ? "-" : row.Cells[22].Value.ToString(),
                                        row.Cells[23].Value == null || row.Cells[23].Value.ToString() == "" ? "-" : row.Cells[23].Value.ToString(),
                                        row.Cells[24].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[24].Value.ToString()),
                                        row.Cells[25].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[25].Value.ToString()),
                                        "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                    }
                    row = null;
                }

                if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                {
                    strAdjQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_CatalogOrderLedger", "(#CodeID#)", Localization.ParseNativeInt(base.iIDentity.ToString()));
                    for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                    {
                        DataGridViewRow row = fgDtls.Rows[i];
                        if (row.Cells[3].Value != null && row.Cells[3].Value.ToString() != "" && row.Cells[3].Value.ToString() != "0" && row.Cells[8].Value != null && row.Cells[8].Value.ToString() != "" && row.Cells[8].Value.ToString() != "0")
                        {
                            string sBatchNo = string.Empty;
                            sBatchNo = DB.GetSnglValue("Select CatSONo from fn_CatalogSalesOrderMain_Tbl() Where CatSOID=" + row.Cells[3].Value.ToString());

                            if (Localization.ParseNativeDouble(row.Cells[8].Value.ToString()) > 0)
                            {
                                strAdjQry += DBSp.InsertIntoCatalogOrderLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtBillDate.Text,
                                        Localization.ParseNativeDouble(cboParty.SelectedValue.ToString()), row.Cells[29].Value == null ? "0" : row.Cells[29].Value.ToString() == "" ? "0" : row.Cells[29].Value.ToString(), "0",
                                        row.Cells[3].Value.ToString(), sBatchNo, Localization.ParseNativeDouble(row.Cells[5].Value.ToString()), Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                        0, Localization.ParseNativeDecimal(row.Cells[8].Value.ToString()), row.Cells[9].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[9].Value.ToString()),
                                        row.Cells[14].Value == null ? "NULL" : row.Cells[14].Value.ToString() == "" ? "NULL" : row.Cells[14].Value.ToString(), 0,
                                        row.Cells[16].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[16].Value.ToString()),
                                        row.Cells[17].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[17].Value.ToString()),
                                        row.Cells[18].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[18].Value.ToString()),
                                        row.Cells[19].Value == null || row.Cells[19].Value.ToString() == "" || row.Cells[19].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[19].Value.ToString()),
                                        row.Cells[20].Value == null || row.Cells[20].Value.ToString() == "" || row.Cells[20].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(row.Cells[20].Value.ToString()),
                                        row.Cells[21].Value == null || row.Cells[21].Value.ToString() == "" ? "-" : row.Cells[21].Value.ToString(),
                                        row.Cells[22].Value == null || row.Cells[22].Value.ToString() == "" ? "-" : row.Cells[22].Value.ToString(),
                                        row.Cells[23].Value == null || row.Cells[23].Value.ToString() == "" ? "-" : row.Cells[23].Value.ToString(),
                                        row.Cells[24].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[24].Value.ToString()),
                                        row.Cells[25].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[25].Value.ToString()),
                                        "NULL", i, 1, "Sales", row.Cells[26].Value == null ? 0 : Localization.ParseNativeInt(row.Cells[26].Value.ToString()), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                            }
                            row = null;
                        }
                    }
                }

                #region VatLedger Posting
                try
                {
                    string sVatAcMisc = DB.GetSnglValue("Select MiscID from fn_MiscMaster_Tbl() Where MiscName='VAT'");
                    for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                    {
                        DataGridViewRow row2 = fgDtls_f.Rows[i];
                        string sVatAcLedger = DB.GetSnglValue("select TaxtypeID from fn_LedgerMaster_Tbl() Where LedgerId=" + fgDtls_f.Rows[i].Cells[2].Value + "");
                        if (sVatAcMisc == sVatAcLedger)
                        {
                            if (Operators.ConditionalCompareObjectEqual(row2.Cells[3].FormattedValue, "+", false))
                            {
                                strAdjQry = strAdjQry + DBSp.InsertInto_VatLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                             row2.Cells[2].Value.ToString(), Localization.ParseNativeInt(row2.Cells[3].Value.ToString()), Localization.ParseNativeDecimal(row2.Cells[4].Value.ToString()),
                                             "(#CodeID#)", 0, Localization.ParseNativeDecimal(row2.Cells[6].Value.ToString()), "null", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                            }
                            if (Operators.ConditionalCompareObjectEqual(row2.Cells[3].FormattedValue, "-", false))
                            {
                                strAdjQry = strAdjQry + DBSp.InsertInto_VatLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", dtEntryDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                         row2.Cells[2].Value.ToString(), Localization.ParseNativeInt(row2.Cells[3].Value.ToString()), Localization.ParseNativeDecimal(row2.Cells[4].Value.ToString()),
                                         "(#CodeID#)", Localization.ParseNativeDecimal(row2.Cells[6].Value.ToString()), 0, "null", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                            }
                        }
                    }
                }
                catch { }
                #endregion

                //if (cboTransport.SelectedValue != null && Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()) > 0.0)
                //{
                //    strAdjQry = strAdjQry + DBSp.InsertIntoTrasportLedger("(#CodeID#)", "(#OTHERNO#)", dtBillDate.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                //            Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()), Localization.ParseNativeDouble(cboDepartment.ToString()),
                //            Localization.ParseNativeDouble(cboParty.SelectedValue.ToString()), txtLrNo.Text, dtLrDate.Text, txtVehicleNo.Text,
                //            Localization.ParseNativeDouble(UnitID.ToString()), Localization.ParseNativeInt(TxtTotalPcs.Text), Localization.ParseNativeDecimal(TxtTotMtrs.Text),
                //            Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                //}
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");

                double dblTransID = 0;
                string sPartyID = cboParty.SelectedValue.ToString();
                DBSp.Transcation_AddEdit_Trans(pArrayData, fgDtls, true, ref dblTransID, strAdjQry, "", txtEntryNo.Text, txtBillNo.Text, "BillNo", 0, new DataGridViewEx[] { fgDtls_f });

                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) || (base.blnFormAction == Enum_Define.ActionType.View_Record))
                {
                    flg_Sms = Localization.ParseBoolean(GlobalVariables.SMS_SEND_BookInv);
                    flg_Email = Localization.ParseBoolean(GlobalVariables.EMAIL_SEND_BookInv);

                    if (blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        string sEntryNo = DB.GetSnglValue("SELECT EntryNo from fn_CatalogSalesMain_Tbl() WHERE CatSalesID=" + dblTransID);
                        if (flg_Sms == true || flg_Email == true)
                        {

                            if (flg_Sms == true)
                            {
                                try { CommonCls.SendSms(dblTransID.ToString(), base.iIDentity.ToString(), 1, sPartyID); }
                                catch { }
                            }

                            if (flg_Email == true)
                            {
                                try
                                {
                                    CommonCls.sendEmail(dblTransID.ToString(), sEntryNo, sPartyID, base.iIDentity.ToString(), false, 0);
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
                                    CommonCls.sendEmail(txtCode.Text, txtEntryNo.Text, sPartyID, base.iIDentity.ToString(), true, 0);
                                }
                                catch { }
                            }
                        }
                    }

                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        if (Localization.ParseBoolean(GlobalVariables.PASAV) && (CIS_Utilities.CIS_Dialog.Show("Do you want to Print this Record?", GetAssemblyInfo.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                        {
                            CIS_ReportTool.frmReportViewer frmReport = new CIS_ReportTool.frmReportViewer();
                            frmReport.PrinterID = -1;
                            frmReport.PrintCopies = 1;
                            frmReport.iCompID = Db_Detials.CompID;
                            frmReport.iYearID = Db_Detials.YearID;
                            frmReport.iUserID = Db_Detials.UserID;
                            frmReport.objReport = Db_Detials.objReport;
                            frmReport.iID = Localization.ParseNativeInt(dblTransID.ToString());
                            frmReport.sReportID = "20";
                            frmReport.strApplicationName = GetAssemblyInfo.ProductName;
                            frmReport.GenerateReport(base.iIDentity, "", "", Localization.ParseNativeInt(dblTransID.ToString()), "", 0, true, 0, false, 0);
                        }
                    }

                    if (Localization.ParseBoolean(GlobalVariables.PASAV) && (CIS_Utilities.CIS_Dialog.Show("Do you want to Print this Record?", GetAssemblyInfo.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                    {
                        CIS_ReportTool.frmReportViewer frmReport = new CIS_ReportTool.frmReportViewer();
                        frmReport.PrinterID = -1;
                        frmReport.PrintCopies = 1;
                        frmReport.iCompID = Db_Detials.CompID;
                        frmReport.iYearID = Db_Detials.YearID;
                        frmReport.iUserID = Db_Detials.UserID;
                        frmReport.objReport = Db_Detials.objReport;
                        frmReport.iID = Localization.ParseNativeInt(dblTransID.ToString());
                        frmReport.sReportID = "60";
                        frmReport.strApplicationName = GetAssemblyInfo.ProductName;

                        frmReport.GenerateReport(base.iIDentity, "", "", Localization.ParseNativeInt(dblTransID.ToString()), "", 0, true, 0);
                    }
                }
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
                if (!EventHandles.IsValidGridReq(this.fgDtls, base.dt_AryIsRequired) && !EventHandles.IsValidGridReq(this.fgDtls_f, base.dt_AryIsRequired))
                {
                    return true;
                }
                if (!EventHandles.IsRequiredInGrid(fgDtls, this.dt_AryIsRequired, false))
                {
                    return true;
                }
                if (!EventHandles.IsRequiredInGrid(fgDtls, this.dt_AryIsRequired, false))
                {
                    return true;
                }
                if (txtEntryNo.Text.Trim() == "" || txtEntryNo.Text.Trim() == "-" || txtEntryNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry No.");
                    txtEntryNo.Focus();
                    return true;
                }
                if (!Information.IsDate(dtEntryDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry Date");
                    dtEntryDate.Focus();
                    return true;
                }
                if (txtEntryNo.Text.Trim() == "" || txtEntryNo.Text.Trim() == "-" || txtEntryNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Bill No.");
                    txtBillNo.Focus();
                    return true;
                }

                if ((txtBillNo.Text != null) && ((txtBillNo.Text.Trim().Length) > 0))
                {
                    string strTblName;
                    if (base.blnFormAction == 0)
                    {
                        strTblName = "tbl_CatalogSalesMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "BillNo", txtBillNo.Text, false, "", 0, string.Format("CompID = {0} and YearID = {1} and BranchID = {2} and StoreID = {3}", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.StoreID), "This Bill No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where BillNo = '{1}' And CompID = {2} and YearID = {3} and BranchID = {4} and StoreID = {5}", new object[] { "tbl_CatalogSalesMain", txtBillNo.Text.ToString(), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.StoreID }))))
                        {
                            txtBillNo.Focus();
                            return true;
                        }
                    }
                    else if (Localization.ParseNativeInt(base.blnFormAction.ToString()) == 1)
                    {
                        strTblName = "tbl_CatalogSalesMain";
                        if (Navigate.CheckDuplicate(ref strTblName, "BillNo", txtBillNo.Text, true, "CatSalesID", Localization.ParseNativeLong(txtCode.Text), string.Format("CompID = {0} and YearID = {1} and BranchID = {2} and StoreID = {3}", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.StoreID), "This Bill No is already used in Entry No : " + DB.GetSnglValue(string.Format("Select EntryNo From {0} Where BillNo = '{1}' And CompID = {2} and YearID = {3} and BranchID = {4} and StoreID = {5}", new object[] { "tbl_CatalogSalesMain", txtBillNo.Text.ToString(), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.StoreID }))))
                        {
                            txtBillNo.Focus();
                            return true;
                        }
                    }
                }
                if (!Information.IsDate(dtBillDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Bill Date");
                    dtBillDate.Focus();
                    return true;
                }

                if (cboDepartment.SelectedValue == null || cboDepartment.SelectedValue.ToString() == "-" || cboDepartment.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    cboDepartment.Focus();
                    return true;
                }

                if (cboParty.SelectedValue == null || cboParty.Text.Trim().ToString() == "-" || cboParty.Text.Trim().ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Party");
                    cboParty.Focus();
                    return true;
                }

                if (cboSalesAc.SelectedValue == null || cboSalesAc.Text.Trim().ToString() == "-" || cboSalesAc.Text.Trim().ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Sales Account");
                    cboSalesAc.Focus();
                    return true;
                }
                if (cboTransport.SelectedValue == null || cboTransport.Text.Trim().ToString() == "-" || cboTransport.Text.Trim().ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Transport");
                    cboTransport.Focus();
                    return true;
                }

                if (CboDeliveryAt.SelectedValue == null || CboDeliveryAt.Text.Trim().ToString() == "-" || CboDeliveryAt.Text.Trim().ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select DeliveryAt");
                    CboDeliveryAt.Focus();
                    return true;
                }

                decimal CreditLimit = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select isnull(CreditLimit,0) From {0} Where LedgerId = {1} ", "tbl_LedgerMaster", (cboParty.SelectedValue))));
                decimal TotSalseValue;
                if (CreditLimit > 0)
                {
                    TotSalseValue = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("select sum(isnull(NetAmount,0)) From {0} Where LedgerID = {1} and CompID = {2} and YearID ={3} and BranchID = {4} and StoreID = {5}", new object[] { "tbl_CatalogSalesMain", (this.cboParty.SelectedValue), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.StoreID })));
                    if (TotSalseValue + Localization.ParseNativeDecimal(txtNetAmt.Text) > CreditLimit)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Exceeding Credit Limit");
                        return true;
                    }
                }

                if (FS_BRK_COM == true)
                {
                    if (cboBroker.SelectedValue == null || cboBroker.Text.Trim().ToString() == "-" || cboBroker.Text.Trim().ToString() == "0")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Broker");
                        cboBroker.Focus();
                        return true;
                    }
                }
                if (TxtGrossAmount.Text.Trim() == "" || TxtGrossAmount.Text.Trim() == "-" || TxtGrossAmount.Text.Trim() == "0.00")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Gross Amount Details");
                    TxtGrossAmount.Focus();
                    return true;
                }
                if (txtNetAmt.Text.Trim() == "" || txtNetAmt.Text.Trim() == "-" || txtNetAmt.Text.Trim() == "0.00")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Net Amount Details");
                    txtNetAmt.Focus();
                    return true;
                }
                CalcVal();

                return false;
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                return false;
            }

        }

        #endregion

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboDepartment.SelectedValue == null || cboDepartment.SelectedValue.ToString() == "-" || cboDepartment.SelectedValue.ToString().Trim() == "" || cboDepartment.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department.");
                    cboDepartment.Focus();
                }
                else
                {
                    #region StockAdjQuery
                    string strQry = string.Empty;
                    int ibitcol = 0;
                    string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", iIDentity, 0.0));
                    string strQry_ColName = "";
                    string[] arr = CommonCls.GetAdjColName(base.iIDentity, 0.0).Split(';');
                    strQry_ColName = arr[0].ToString();
                    ibitcol = Localization.ParseNativeInt(arr[1]);
                    strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5},{6}) Where Bal_Pcs > 0 Order by MyId ", new object[] { strQry_ColName, snglValue, this.cboDepartment.SelectedValue, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID });
                    #endregion

                    if (this.dtBillDate.Text != "__/__/____")
                    {
                        frmStockAdj frmStockAdj = new frmStockAdj();
                        frmStockAdj.MenuID = base.iIDentity;
                        frmStockAdj.Entity_IsfFtr = 0;
                        frmStockAdj.ref_fgDtls = this.fgDtls;
                        frmStockAdj.QueryString = strQry;
                        frmStockAdj.IsRefQuery = true;
                        frmStockAdj.ibitCol = ibitcol;
                        frmStockAdj.LedgerID = Conversions.ToString(this.cboDepartment.SelectedValue);
                        if (frmStockAdj.ShowDialog() == DialogResult.Cancel)
                        {
                            frmStockAdj.Dispose();
                        }
                        else
                        {
                            frmStockAdj.Dispose();
                            frmStockAdj = null;
                        }
                        this.fgDtls.Select();
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        //private void cboOrderNo_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (cboParty.SelectedValue != null && cboSalseOrder.SelectedValue != null)
        //        {
        //            if (blnFormAction == Enum_Define.ActionType.New_Record || blnFormAction == Enum_Define.ActionType.Edit_Record)
        //            {
        //                if (Localization.ParseNativeInt(cboSalseOrder.SelectedValue.ToString()) > 0)
        //                {
        //                    fgdtls_f_f.DataSource = DB.GetDT(String.Format("Select CAST('False' as Bit) As [Sel], BookSalesID,BookSONo,BookSODate,CatalogID,BookName,OrderPcs,DispatchPcs, 0 as CDispatchPcs,BalPcs, Rate  from {0}({1},{2},{3}) Where BookSalesID = {4}", "fn_FetchBookSalesOrderForInvoice", cboParty.SelectedValue, Db_Detials.CompID, Db_Detials.YearID, cboSalseOrder.SelectedValue), false);
        //                    dtOrderDate.Text = Localization.ToVBDateString(DB.GetSnglValue(String.Format("Select top 1 BookSODate from {0}({1},{2},{3}) Where BookSalesID = {4}", "fn_FetchBookSalesOrderForInvoice", cboParty.SelectedValue, Db_Detials.CompID, Db_Detials.YearID, cboSalseOrder.SelectedValue)));
        //                    foreach (UltraGridBand Band in fgdtls_f_f.DisplayLayout.Bands)
        //                    {
        //                        foreach (UltraGridColumn Column in Band.Columns)
        //                        {
        //                            using (IDataReader dr = DB.GetRS(String.Format("Select * From {0} Where GridID = {1} and SubGridID = 2 and ColIndex = {2}", "tbl_GridSettings", iIDentity, Column.Index)))
        //                            {
        //                                while (dr.Read())
        //                                {
        //                                    Column.Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
        //                                    Column.Hidden = Localization.ParseBoolean(dr["IsHidden"].ToString());
        //                                    Column.CellActivation = (Localization.ParseBoolean(dr["IsEditable"].ToString()) == true ? Activation.AllowEdit : Activation.NoEdit);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    fgdtls_f_f.DataSource = DB.GetDT(String.Format("Select CAST('False' as Bit) As [Sel],  BookSalesID,BookSONo,BookSODate,CatalogID,BookName,OrderPcs,DispatchPcs, 0 as CDispatchPcs,BalPcs,Rate  from {0}({1},{2},{3})", "fn_FetchBookSalesOrderForInvoice", cboParty.SelectedValue, Db_Detials.CompID, Db_Detials.YearID), false);
        //                    foreach (UltraGridBand Band in fgdtls_f_f.DisplayLayout.Bands)
        //                    {
        //                        foreach (UltraGridColumn Column in Band.Columns)
        //                        {
        //                            using (IDataReader dr = DB.GetRS(string.Format("Select * From {0} Where GridID = {1} and SubGridID = 2 and ColIndex = {2}", Db_Detials.tbl_GridSettings, iIDentity, Column.Index)))
        //                            {
        //                                while (dr.Read())
        //                                {
        //                                    Column.Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
        //                                    Column.Hidden = (Localization.ParseBoolean(dr["IsHidden"].ToString()));
        //                                    Column.CellActivation = (Localization.ParseBoolean(dr["IsEditable"].ToString()) == true ? Activation.AllowEdit : Activation.NoEdit);
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch { }
        //}

        private void CalcVal()
        {
            TxtTotalPcs.Text = string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 9, -1, -1));
            TxtGrossAmount.Text = string.Format("{0:N2}", Math.Round(CommonCls.GetColSum(this.fgDtls, 10, -1, -1)));
            double dblDedAmt = 0.0;
            DataGridViewEx ex = this.fgDtls_f;
            for (int i = 0; i <= ex.RowCount - 1; i++)
            {
                if (ex.Rows[i].Cells[3].Value != null)
                {
                    if (Operators.ConditionalCompareObjectEqual(ex.Rows[i].Cells[3].FormattedValue, "-", false))
                    {
                        dblDedAmt -= Localization.ParseNativeDouble(ex.Rows[i].Cells[5].Value.ToString());
                    }
                    else if (Operators.ConditionalCompareObjectEqual(ex.Rows[i].Cells[3].FormattedValue, "+", false))
                    {
                        dblDedAmt += Localization.ParseNativeDouble(ex.Rows[i].Cells[5].Value.ToString());
                    }
                }
            }
            ex = null;
            txtAddLessAmt.Text = string.Format("{0:N2}", Math.Round(dblDedAmt));
            txtNetAmt.Text = string.Format("{0:N2}", Math.Round((double)(Convert.ToDouble(Localization.ParseNativeDecimal(TxtGrossAmount.Text)) + dblDedAmt)));
        }

        //private void cboChallanNo_LostFocus(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Localization.ParseNativeInt(cboChallanNo.SelectedValue.ToString()) != 0)
        //        {
        //            this.fgDtls.Rows.Clear();
        //            int iSrNo = 0;
        //            using (IDataReader reader = DB.GetRS(string.Format("Select * from {0}({2},{3}) Where DlryChlnID = {1} ", new object[] { "fn_FetchChlnForBookInvoice", (cboChallanNo.SelectedValue), Db_Detials.CompID, Db_Detials.YearID })))
        //            {
        //                while (reader.Read())
        //                {
        //                    dtChallanDate.Text = (Localization.ToVBDateString(reader["ChallanDate"].ToString()));
        //                    if (reader["BookSODate"].ToString() != "")
        //                    {
        //                        dtOrderDate.Text = (Localization.ToVBDateString(reader["BookSODate"].ToString()));
        //                    }
        //                    cboParty.SelectedValue = Localization.ParseNativeInt(reader["PartyId"].ToString());
        //                    cboBroker.SelectedValue = Localization.ParseNativeInt(reader["BrokerID"].ToString());
        //                    CboDeliveryAt.SelectedValue = Localization.ParseNativeInt(reader["HasteID"].ToString());
        //                    cboTransport.SelectedValue = Localization.ParseNativeInt(reader["TransportID"].ToString());
        //                    txtDescription1.Text = (reader["Description1"].ToString());
        //                    txtNarration.Text = (reader["Description2"].ToString());
        //                    if (Localization.ParseNativeDouble(cboParty.SelectedValue.ToString()) > 0.0)
        //                    {
        //                        string sqlQuery = string.Format("Select * from {0}({1},{2},{3})", new object[] { "fn_FetchBookSalesOrderForInvoice", cboParty.SelectedValue, Db_Detials.CompID, Db_Detials.YearID });
        //                        Combobox_Setup.Fill_Combo(this.cboSalseOrder, sqlQuery, "BookSONo,BookSODate,BalPcs,Rate", "BookSalesID");
        //                        //CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboSalseOrder = this.cboSalseOrder;
        //                        cboSalseOrder.ColumnWidths = "0;100;0;0;100;50;80;0";
        //                        cboSalseOrder.AutoComplete = true;
        //                        cboSalseOrder.AutoDropdown = true;
        //                    }
        //                    try
        //                    {
        //                        this.cboSalseOrder.SelectedValue = Localization.ParseNativeInt(reader["SalesOrderID"].ToString());
        //                    }
        //                    catch { }
        //                    fgDtls.Columns[0].Visible = false;
        //                    fgDtls.Rows.Add();
        //                    fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[1].Value = ++iSrNo;
        //                    fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[2].Value = Localization.ParseNativeInt(reader["DlryChlnID"].ToString());
        //                    fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[ChallanNo].Value = reader["ChallanNo"].ToString();
        //                    fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[3].Value = Localization.ParseNativeInt(reader["BookSONo"].ToString());
        //                    fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[4].Value = Localization.ParseNativeInt(reader["CatalogID"].ToString());
        //                    //fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[5].Value = Localization.ParseNativeInt(reader["QualityID"].ToString());
        //                    fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[QualityID].Value = Localization.ParseNativeInt(reader["NCatalogID"].ToString());
        //                    //fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[7].Value = Localization.ParseNativeInt(reader["NQualityID"].ToString());
        //                    fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[6].Value = Localization.ParseNativeInt(reader["Pcs"].ToString());
        //                    //fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[8].Value = Localization.ParseNativeDecimal(reader["Meters"].ToString());
        //                    //fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[Meters].Value = Localization.ParseNativeDecimal(reader["Weight"].ToString());
        //                    fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[9].Value = Localization.ParseNativeDecimal(reader["Rate"].ToString());
        //                    fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[10].Value = Localization.ParseNativeDecimal(reader["UnitID"].ToString());
        //                }
        //            }
        //            btnSelect.Enabled = false;
        //        }
        //    }
        //    catch { }
        //}

        private void cboParty_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {

                if (cboParty.SelectedValue != null && Localization.ParseNativeDouble(cboParty.SelectedValue.ToString()) > 0.0)
                {
                    cboBroker.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select BrokerID From {0} Where LedgerID = {1}", "fn_LedgerMaster_Tbl()", (cboParty.SelectedValue))));
                    cboSalesAc.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select PurchSalesID From {0} Where LedgerID = {1}", "fn_LedgerMaster_Tbl()", (cboParty.SelectedValue))));
                    cboTransport.SelectedValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select TransportId From {0} Where LedgerID = {1}", "fn_LedgerMaster_Tbl()", (cboParty.SelectedValue))));

                    try
                    { CboDeliveryAt.SelectedValue = cboParty.SelectedValue; }
                    catch { }
                }
            }
            catch { }
        }

        private void cboParty_Leave(object sender, EventArgs e)
        {
            if (blnFormAction == Enum_Define.ActionType.New_Record | blnFormAction == Enum_Define.ActionType.Edit_Record)
            {
                int strQry;
                strQry = Localization.ParseNativeInt(DB.GetSnglValue("Select Count(0) from fn_LedgerMaster_Tbl()  Where LedgerID=" + cboParty.SelectedValue + " and Blocked='Yes'"));

                if (strQry > 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Selected Party Is Blocked.");
                }

                if ((cboParty.SelectedValue != null))
                {
                    if (cboOrderType.SelectedItem.ToString() == "WITH ORDER" || cboOrderType.SelectedItem != null)
                    {
                        fgdtls_f_f.DataSource = DB.GetDT(string.Format("Select * from {0} ({1},{2},{3},{4},{5},{6}) Where OrderTransType='Sales' and  RefVoucherID in ({7})", "fn_FetchCatalogOrders", CommonLogic.SQuote(Localization.ToSqlDateString(dtBillDate.Text.ToString())), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, cboParty.SelectedValue.ToString(), RefVoucherID), false);
                        foreach (UltraGridBand band in fgdtls_f_f.DisplayLayout.Bands)
                        {
                            foreach (UltraGridColumn column in band.Columns)
                            {
                                using (IDataReader dr = DB.GetRS(string.Format("Select * From {0} Where GridID = {1} and SubGridID = 2 and ColIndex = {2}", Db_Detials.tbl_GridSettings, iIDentity, column.Index)))
                                {
                                    while (dr.Read())
                                    {
                                        column.Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
                                        column.Hidden = (Localization.ParseBoolean(dr["IsHidden"].ToString()));
                                        //column.CellActivation = (Localization.ParseBoolean(dr["IsEditable"].ToString()) == true ? Activation.AllowEdit : Activation.NoEdit);
                                        column.CellActivation = Activation.NoEdit;
                                    }
                                }
                            }
                        }

                        if (Localization.ParseNativeInt(txtCode.Text.Trim()) == 0)
                        {
                            EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                            EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                        }
                    }
                }
            }
        }

        private void cboSalesAc_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (!((base.blnFormAction == Enum_Define.ActionType.View_Record) | (base.blnFormAction == Enum_Define.ActionType.Not_Active)))
                {
                    DataGridViewEx ex = this.fgDtls_f;
                    int VatType = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("select VATTypeId from fn_LedgerMaster_Tbl() where LedgerId={0}", (cboSalesAc.SelectedValue))));
                    if (VatType != 0)
                    {
                        using (IDataReader reader = DB.GetRS(string.Format("select LedgerName, Percentage from fn_LedgerMaster_Tbl() where VATTypeId={0} and LedgerGroupId=25", VatType)))
                        {
                            if (reader.Read())
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[2].Value = reader["LedgerName"].ToString();
                                ex.Rows[ex.CurrentRow.Index].Cells[3].Value = "+";
                                ex.Rows[ex.CurrentRow.Index].Cells[4].Value = Localization.ParseNativeDecimal(reader["Percentage"].ToString());
                                ex.Rows[ex.CurrentRow.Index].Cells[6].Value = ((Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(reader["Percentage"].ToString())) / 100);
                            }
                        }
                    }
                    else
                    {
                        ex.Rows[ex.CurrentRow.Index].Cells[2].Value = "";
                        ex.Rows[ex.CurrentRow.Index].Cells[3].Value = "+";
                        ex.Rows[ex.CurrentRow.Index].Cells[4].Value = 0.0;
                        ex.Rows[ex.CurrentRow.Index].Cells[6].Value = 0.0;
                    }
                }
            }
            catch { }
        }

        private void dtBillDate_TextChanged(object sender, EventArgs e)
        {
            if (((base.blnFormAction == 0) | (Localization.ParseNativeInt(base.blnFormAction.ToString()) == 1)) && (this.dtBillDate.Text != "__/__/____"))
            {
                dtDueDate.Text = (dtBillDate.Text);
            }
        }

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewEx ex = this.fgDtls_f;
                if (!((base.blnFormAction == Enum_Define.ActionType.View_Record) || (base.blnFormAction == Enum_Define.ActionType.Not_Active)))
                {
                    if (((e.ColumnIndex == 8) | (e.ColumnIndex == 9)))
                    {
                        CalcVal();
                    }
                    switch (e.ColumnIndex)
                    {
                        case 8:
                            if (fgDtls.Rows[e.RowIndex].Cells[8].Value != null && fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString() != "0" && fgDtls.Rows[e.RowIndex].Cells[9].Value != null && fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString() != "0")
                            {
                                fgDtls.Rows[e.RowIndex].Cells[10].Value = Math.Round(Localization.ParseNativeDouble((Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString()) * Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString())).ToString()));
                            }
                            for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                            {
                                if (ex.Rows[i].Cells[4].Value != null && ex.Rows[i].Cells[4].Value.ToString() != "0")
                                {
                                    ex.Rows[i].Cells[6].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[i].Cells[4].Value))), 100M).ToString().Replace(",", "");
                                    CalcVal();
                                }
                            }
                            CalcVal();
                            break;

                        case 9:
                            goto case 8;

                        case 5:
                            if (fgDtls.Rows[e.RowIndex].Cells[5].Value != null && fgDtls.Rows[e.RowIndex].Cells[5].Value.ToString().Length > 0)
                            {
                                string strOrder = string.Empty;

                                if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                                {
                                    if ((fgDtls.Rows[e.RowIndex].Cells[3].Value == null) || (fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString() == "0") || (fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString() == "") && (fgDtls.Rows[e.RowIndex].Cells[5].Value != null))
                                    {
                                        for (int i = 0; i <= (fgdtls_f_f.Rows.Count - 1); i++)
                                        {
                                            if (Operators.ConditionalCompareObjectEqual(fgDtls.Rows[e.RowIndex].Cells[5].Value, fgdtls_f_f.Rows[i].Cells[7].Value, false))
                                            {
                                                if (Operators.ConditionalCompareObjectGreater(fgdtls_f_f.Rows[i].Cells[11].Value, 0, false))
                                                {
                                                    flg_OrderConform = true;
                                                    fgDtls.Rows[e.RowIndex].Cells[3].Value = Localization.ParseNativeInt(fgdtls_f_f.Rows[i].Cells[5].Value.ToString());
                                                    fgDtls.Rows[e.RowIndex].Cells[8].Value = Localization.ParseNativeDecimal(fgdtls_f_f.Rows[i].Cells[11].Value.ToString());
                                                    fgDtls.Rows[e.RowIndex].Cells[9].Value = Localization.ParseNativeDecimal(fgdtls_f_f.Rows[i].Cells[15].Value.ToString());
                                                    break;
                                                }
                                                else
                                                {
                                                    flg_OrderConform = false;
                                                }
                                            }
                                            // fgDtls.Rows[e.RowIndex].Cells[9].Value = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("Select Rate From fn_BookInvoiceRate(" + fgDtls.Rows[e.RowIndex].Cells[0].Value + "," +  ) Where CatalogID = {1} and ORDERID=" + fgDtls.Rows[e.RowIndex].Cells[3].Value + "", "fn_BookSalesOrderDtls", fgDtls.Rows[e.RowIndex].Cells[4].Value)));
                                        }
                                    }
                                    //if (!flg_OrderConform)
                                    //{
                                    //    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "Order Not Found", "No Order Found for selected party and Book Design");
                                    //}
                                    //strOrder = DB.GetSnglValue(string.Format("Select BookSalesID From fn_FetchBookSalesOrderForInvoice(" + cboParty.SelectedValue + "," + Db_Detials.CompID + "," + Db_Detials.YearID + ") where SerialID=" + fgDtls.Rows[e.RowIndex].Cells[4].Value + ""));
                                    //if (strOrder != "" || strOrder != null)
                                    //{
                                    //    fgDtls.Rows[e.RowIndex].Cells[3].Value = Localization.ParseNativeInt(strOrder);
                                    //}
                                    //else
                                    //{
                                    //    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "Order Not Found", "No Order Found for selected party and Book Design");
                                    //}
                                }

                                if (cboOrderType.SelectedItem.ToString() != "WITH ORDER")
                                {
                                    using (IDataReader iDr = DB.GetRS(string.Format("select * from tbl_CatalogMaster where IsDeleted=0 and CatalogID = " + fgDtls.Rows[e.RowIndex].Cells[5].Value + "")))
                                    {
                                        if (iDr.Read())
                                        {
                                            if (cboParty.SelectedValue != null)
                                            {
                                                if (Localization.ParseNativeInt(cboParty.SelectedValue.ToString()) > 0)
                                                {
                                                    int _stateCnt = Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_LedgerMaster_Tbl() WHERE LedgerID=" + cboParty.SelectedValue + " and StateName like '%Maharashtra%'"));
                                                    if (_stateCnt > 0)
                                                        fgDtls.Rows[e.RowIndex].Cells[9].Value = Localization.ParseNativeDouble(iDr["MaharashtraRate"].ToString());
                                                    else
                                                        fgDtls.Rows[e.RowIndex].Cells[9].Value = Localization.ParseNativeDouble(iDr["OthersRate"].ToString());
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            break;

                        case 10:
                            if (Localization.ParseNativeDouble(Operators.DivideObject(fgDtls.Rows[e.RowIndex].Cells[10].Value, fgDtls.Rows[e.RowIndex].Cells[8].Value).ToString()) != Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString()))
                            {
                                if (fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString() != null)
                                {
                                    fgDtls.Rows[e.RowIndex].Cells[9].Value = Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[10].Value.ToString()) / Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString());
                                    for (int i = 0; i <= fgDtls_f.RowCount - 1; i++)
                                    {
                                        if (ex.Rows[i].Cells[4].Value != null && ex.Rows[i].Cells[4].Value.ToString() != "0")
                                        {
                                            ex.Rows[i].Cells[6].Value = decimal.Divide(decimal.Multiply(Localization.ParseNativeDecimal(TxtGrossAmount.Text), Localization.ParseNativeDecimal(Conversions.ToString(ex.Rows[i].Cells[4].Value))), 100M).ToString().Replace(",", "");
                                            CalcVal();
                                        }
                                    }
                                }
                            }
                            CalcVal();
                            break;
                    }
                }
            }
            catch { }
        }

        private void fgDtls_f_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (!((base.blnFormAction == Enum_Define.ActionType.View_Record) || (base.blnFormAction == Enum_Define.ActionType.Not_Active)))
                {
                    DataGridViewEx ex = this.fgDtls_f;
                    switch (e.ColumnIndex)
                    {
                        case 2:
                            if (ex.Rows[ex.CurrentRow.Index].Cells[2].Value.ToString() != "")
                            {
                                using (IDataReader reader = DB.GetRS(string.Format("Select Percentage From {0} Where LedgerID = {1}", "tbl_LedgerMaster", ex.Rows[ex.CurrentRow.Index].Cells[2].Value.ToString())))
                                {
                                    if (reader.Read())
                                    {
                                        if (ex.Rows[ex.CurrentRow.Index].Cells[3].Value == null)
                                        {
                                            ex.Rows[ex.CurrentRow.Index].Cells[3].Value = "-";
                                        }
                                        ex.Rows[ex.CurrentRow.Index].Cells[4].Value = reader["Percentage"].ToString();
                                        ex.Rows[ex.CurrentRow.Index].Cells[5].Value = ((Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(reader["Percentage"].ToString())) / 100);
                                    }
                                }
                            }
                            break;

                        case 4:
                            ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                            break;

                        case 5:
                            ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(TxtGrossAmount.Text)) * 100);
                            CalcVal();
                            break;
                    }
                }
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_f_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (((base.blnFormAction == Enum_Define.ActionType.New_Record)))
                {
                    DataGridViewEx ex = this.fgDtls_f;
                    bool CalcDednWithNetAmt = Localization.ParseBoolean(DB.GetSnglValue(string.Format("select CalcDednWithNetAmt from {0} where LedgerID={1}", "tbl_Ledgermaster", ex.Rows[ex.CurrentRow.Index].Cells[2].Value)));
                    switch (e.ColumnIndex)
                    {
                        case 2:
                            if (blnFormAction == Enum_Define.ActionType.New_Record)
                            {
                                if (ex.Rows[ex.CurrentRow.Index].Cells[2].Value.ToString() != "")
                                {
                                    using (IDataReader reader = DB.GetRS(string.Format("Select Percentage From {0} Where LedgerID = {1}", "tbl_LedgerMaster", ex.Rows[ex.CurrentRow.Index].Cells[2].Value.ToString())))
                                    {
                                        if (reader.Read())
                                        {
                                            if (ex.Rows[ex.CurrentRow.Index].Cells[3].Value == null)
                                            {
                                                ex.Rows[ex.CurrentRow.Index].Cells[3].Value = "-";
                                            }
                                            ex.Rows[ex.CurrentRow.Index].Cells[4].Value = reader["Percentage"].ToString();

                                            if (CalcDednWithNetAmt.ToString() == "True")
                                            {
                                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = ((Localization.ParseNativeDecimal(txtNetAmt.Text) * Localization.ParseNativeDecimal(reader["Percentage"].ToString())) / 100);
                                            }
                                            else
                                            {
                                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = ((Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(reader["Percentage"].ToString())) / 100);
                                            }
                                        }
                                    }
                                }
                            }
                            break;

                        case 3:
                            if (CalcDednWithNetAmt.ToString() == "True")
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(txtNetAmt.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                                ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(txtNetAmt.Text)) * 100);
                            }
                            else
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                                ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(TxtGrossAmount.Text)) * 100);
                            }
                            CalcVal();
                            break;

                        case 4:

                            if (CalcDednWithNetAmt.ToString() == "True")
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(txtNetAmt.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                            }
                            else
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                            }
                            break;

                        case 5:

                            if (CalcDednWithNetAmt.ToString() == "True")
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(txtNetAmt.Text)) * 100);
                            }
                            else
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(TxtGrossAmount.Text)) * 100);
                            }
                            CalcVal();
                            break;
                    }
                    CalcVal();
                }
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }

            try
            {
                if (((base.blnFormAction == Enum_Define.ActionType.Edit_Record)))
                {
                    DataGridViewEx ex = this.fgDtls_f;
                    bool CalcDednWithNetAmt = Localization.ParseBoolean(DB.GetSnglValue(string.Format("select CalcDednWithNetAmt from {0} where LedgerID={1}", "tbl_Ledgermaster", ex.Rows[ex.CurrentRow.Index].Cells[2].Value)));
                    switch (e.ColumnIndex)
                    {
                        case 3:
                            if (CalcDednWithNetAmt.ToString() == "True")
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(txtNetAmt.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                                ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(txtNetAmt.Text)) * 100);
                            }
                            else
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                                ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(TxtGrossAmount.Text)) * 100);
                            }
                            CalcVal();
                            break;

                        case 4:
                            if (CalcDednWithNetAmt.ToString() == "True")
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(txtNetAmt.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                            }
                            else
                            {
                                ex.Rows[ex.CurrentRow.Index].Cells[5].Value = (Localization.ParseNativeDecimal(TxtGrossAmount.Text) * Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[4].Value.ToString()) / 100).ToString().Replace(",", "");
                            }
                            CalcVal();
                            break;

                        case 5:
                            if (ex.Rows[ex.CurrentRow.Index].Cells[5].Value != null && ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString() != "0.00")
                            {
                                if (CalcDednWithNetAmt.ToString() == "True")
                                {
                                    ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(txtNetAmt.Text)) * 100);
                                }
                                else
                                {
                                    ex.Rows[ex.CurrentRow.Index].Cells[4].Value = (Localization.ParseNativeDecimal(ex.Rows[ex.CurrentRow.Index].Cells[5].Value.ToString()) / (Localization.ParseNativeDecimal(TxtGrossAmount.Text)) * 100);
                                }
                            }
                            CalcVal();
                            break;
                    }
                    CalcVal();
                    ex = null;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgdtls_f_f_InitializeLayout(object sender, InitializeLayoutEventArgs e)
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

        public void PrintRecord()
        {
            CIS_ReportTool.frmMultiPrint frmMultiPrint = new CIS_ReportTool.frmMultiPrint();
            CIS_ReportTool.frmMultiPrint.MenuID = base.iIDentity;
            CIS_ReportTool.frmMultiPrint.Id = Localization.ParseNativeInt(txtCode.Text);
            CIS_ReportTool.frmMultiPrint.TblNm = "tbl_CatalogSalesMain";
            CIS_ReportTool.frmMultiPrint.IdStr = "CatSalesID";
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

        private void txtCrDays_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if ((((base.blnFormAction == 0) | (Localization.ParseNativeInt(base.blnFormAction.ToString()) == 1)) && ((txtCrDays.Text != null) && (Conversion.Val(txtCrDays.Text) > 0.0))) && (this.dtDueDate.Text != "__/__/____"))
                {
                    DateTime time = Conversions.ToDate(this.dtBillDate.Text);
                    dtDueDate.Text = Localization.ToVBDateString(time.AddDays(Localization.ParseNativeDouble(txtCrDays.Text)).ToString());
                }
            }
            catch { }
        }

        public string SetBroker
        {
            get
            {
                return cboBroker.SelectedValue.ToString();
            }
            set
            {
                if (value.Length != 0)
                {
                    cboBroker.SelectedValue = value;
                }
            }
        }

        public string SetHaste
        {
            get
            {
                return CboDeliveryAt.SelectedValue.ToString();
            }
            set
            {
                if (value.Length != 0)
                {
                    CboDeliveryAt.SelectedValue = value;
                }
            }
        }

        public string setLrDate
        {
            get
            {
                return dtLrDate.TextFormat(false, true);
            }
            set
            {
                if (value.Length != 0)
                {
                    dtLrDate.Text = value;
                }
            }
        }

        public string setLrNo
        {
            get
            {
                return txtLrNo.Text;
            }
            set
            {
                if (value.Length != 0)
                {
                    txtLrNo.Text = value;
                }
            }
        }

        public string setParty
        {
            get
            {
                return cboParty.SelectedValue.ToString();
            }
            set
            {
                if (value.Length != 0)
                {
                    cboParty.SelectedValue = value;
                }
            }
        }

        public string setTransport
        {
            get
            {
                return cboTransport.SelectedValue.ToString();
            }
            set
            {
                if (value.Length != 0)
                {
                    cboTransport.SelectedValue = value;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void btnShowOrder_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            fgdtls_f_f.Focus();

            //int iSrNo = 0;
            //iSrNo = (fgDtls.Rows.Count - 1);
            //string dtQry="Select sum(Dr_Qty-Cr_Qty) AS BalPcs from fn_StockBookLedger() Where CatalogID=" + fgDtls.Rows[iSrNo].Cells[4].Value + " Group by CatalogID having SUm(Dr_Qty-Cr_Qty)>0" "

            //DataRow[] rst_CatalogID = dtQry.Select("CatalogID=" + fgDtls.Rows[e]. );
            //if (rst_CatalogID.Length > 0)
            //{
            //    foreach (DataRow dr in rst_CatalogID)
            //    {
            //        fgDtls.Rows[iSrNo].Cells[0].ReadOnly = true;
            //        iSrNo++;
            //    }
            //}


            //int iSrNo = 0;
            //iSrNo = (fgDtls.Rows.Count - 1);
            //IDataReader Idr = DB.GetRS("Select sum(Dr_Qty-Cr_Qty) AS BalPcs from fn_StockBookLedger() Where CatalogID=" + fgDtls.Rows[iSrNo].Cells[4].Value + " Group by CatalogID having SUm(Dr_Qty-Cr_Qty)>0");
            //while (Idr.Read())
            //{
            //    fgDtls.Rows[iSrNo].Cells[0].ReadOnly = true;
            //    iSrNo++;
            //}
        }

        private void cboOrderType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataGridViewEx ex2 = this.fgDtls;
                if (cboOrderType.Text == "WITHOUT ORDER")
                {
                    ex2.Columns[2].Visible = false;
                    ex2.Columns[3].Visible = false;
                    ex2.Columns[4].ReadOnly = true;
                    ex2.Columns[5].ReadOnly = true;
                    ex2.Columns[6].ReadOnly = false;
                    ex2.Columns[7].ReadOnly = true;
                    ex2.Columns[9].ReadOnly = false;
                    ex2.Columns[10].ReadOnly = false;
                    btnShowOrder.Enabled = false;
                }
                else if (cboOrderType.Text == "WITH ORDER")
                {
                    ex2.Columns[2].Visible = true;
                    ex2.Columns[3].Visible = true;
                    ex2.Columns[4].ReadOnly = false;
                    ex2.Columns[5].ReadOnly = false;
                    ex2.Columns[6].ReadOnly = true;
                    ex2.Columns[7].ReadOnly = false;
                    ex2.Columns[9].ReadOnly = true;
                    ex2.Columns[10].ReadOnly = true;
                    btnShowOrder.Enabled = true;
                }

                //For Order Column Changes
                if (cboOrderType.Text == "WITHOUT ORDER")
                {
                    ex2.Columns[2].ReadOnly = true;
                    ex2.Columns[2].Visible = false;
                }
                else
                {
                    ex2.Columns[2].ReadOnly = true;
                    ex2.Columns[2].Visible = true;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (blnFormAction == Enum_Define.ActionType.New_Record || blnFormAction == Enum_Define.ActionType.Edit_Record)
            {
                if (CommonCls.IsPartyBlocked(Localization.ParseNativeInt(cboParty.SelectedValue.ToString())) == true)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Selected Party Is Blocked...");
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
    }
}
