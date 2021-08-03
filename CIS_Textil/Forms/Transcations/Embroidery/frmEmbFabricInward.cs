using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class frmEmbFabricInward : frmTrnsIface
    {
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;
        public decimal OrderMeters;
        public static int OrderQualityID;
        private bool Alert;

        public frmEmbFabricInward()
        {
            OrderMeters = new decimal();
            Alert = false;
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
        }

        #region Load
        private void frmEmbFabricInward_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref CboParty, Combobox_Setup.ComboType.Mst_Customer, "");
                Combobox_Setup.FillCbo(ref cboBroker, Combobox_Setup.ComboType.Mst_Brokers, "");
                Combobox_Setup.FillCbo(ref cboDepartment, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
                Combobox_Setup.FillCbo(ref cboProcesser, Combobox_Setup.ComboType.Mst_Dyer);

                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);

                this.fgDtls.CellEndEdit += new DataGridViewCellEventHandler(this.fgDtls_CellEndEdit);
                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                this.fgDtls.RowsAdded += new DataGridViewRowsAddedEventHandler(this.fgDtls_RowsAdded);

                txtEntryNo.Enabled = false;
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
                DBValue.Return_DBValue(this, txtCode, "EmbFabInwardID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtInwardNo, "RefNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtpartyLotNo, "PartyRefNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtRefDate, "RefDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, CboParty, "SupplierID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDepartment, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLrNo, "LrNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtLrDate, "LrDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);
                //DBValue.Return_DBValue(this, txtRackShelf, "RackShelf", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEd1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                DetailGrid_Setup.FillGrid(fgDtls, fgDtls.Grid_UID, fgDtls.Grid_Tbl, "EmbFabInwardID", txtCode.Text, base.dt_HasDtls_Grd, this.iIDentity, GlobalVariables.VALIDATE_EDIT, Db_Detials.CompID.ToString(), 1);
                if (CboParty.SelectedValue != null)
                {
                    if (Localization.ParseNativeDouble(CboParty.SelectedValue.ToString()) > 0.0)
                    {
                        string sqlQuery = string.Format("Select * from {0}({1},{2},{3},{4},{5})", "fn_FetchFabPurchaseOrderDtls", CboParty.SelectedValue, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID);
                        Combobox_Setup.Fill_Combo(this.cboOrderNo, sqlQuery, "FabPONo,FabPoDate,QualityID,Quality,BalPcs,BalMtrs", "FabPOID");
                        CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboOrderNo = this.cboOrderNo;
                        cboOrderNo.ColumnWidths = "0;100;0;0;100;50;80";
                        cboOrderNo.AutoComplete = true;
                        cboOrderNo.AutoDropdown = true;
                    }
                }


                try
                {
                    if (cboOrderNo.Items.Count > 0)
                        DBValue.Return_DBValue(this, this.cboOrderNo, "OrderID", Enum_Define.ValidationType.Text);
                }
                catch { }
                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                    EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                    bool isRowsEditable = false;
                    try
                    {
                        for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                        {
                            if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_StockFabricLedger() WHERE RefID<>'' AND RefID='" + fgDtls.Rows[i].Cells["RefID"].Value.ToString() + "' AND TransType<>" + iIDentity + (Convert.ToString(Db_Detials.CompID) != "" ? " and CompID=" + Db_Detials.CompID : "") + "")) > 0)
                            {
                                isRowsEditable = true;
                                break;
                            }
                        }

                        if (isRowsEditable)
                        {
                            txtpartyLotNo.Enabled = false;
                        }
                        else { txtpartyLotNo.Enabled = true; }
                    }
                    catch { }
                }
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
                txtCode.Text = "";
                CommonCls.IncFieldID(this, ref txtEntryNo, "");
                txtInwardNo.Text = CommonCls.AutoInc(this, "RefNo", "EmbFabInwardID", "");

                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                int MaxId = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(EmbFabInwardID),0) From {0}  Where IsDeleted=0 and StoreID={1} and CompID = {2} and BranchID={3} and YearID = {4}", "tbl_EmbFabricInwardMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID))));
                if (MaxId > 0)
                {
                    using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where IsDeleted=0 and EmbFabInwardID = {1} and StoreID={2} and CompID={3} and BranchID={4} and YearID={5}", "tbl_EmbFabricInwardMain", MaxId, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID)))
                    {
                        while (reader.Read())
                        {
                            dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                            dtRefDate.Text = Localization.ToVBDateString(reader["RefDate"].ToString());
                            CboParty.SelectedValue = Localization.ParseNativeDouble(reader["SupplierID"].ToString());
                            cboBroker.SelectedValue = Localization.ParseNativeDouble(reader["BrokerID"].ToString());
                            cboDepartment.SelectedValue = Localization.ParseNativeDouble(reader["DepartmentID"].ToString());
                            cboTransport.SelectedValue = Localization.ParseNativeDouble(reader["TransportID"].ToString());
                            dtLrDate.Text = Localization.ToVBDateString(reader["LrDate"].ToString());
                            txtRackShelf.Text = (reader["RackShelf"].ToString());
                        }
                    }
                }
                if (((fgDtls.RowCount > 0) & (fgDtls.ColumnCount > 0)) & fgDtls.Columns[4].Visible)
                {
                    fgDtls.Rows[0].Cells[4].Value = CommonCls.AutoInc_Runtime(DB.GetSnglValue(string.Format("Select  {0}({1},{2},{3},{4},{5},{6})", "dbo.fn_FetchBarcodeNo", MaxId, base.iIDentity, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID)), Db_Detials.PCS_NO_INCMT);
                }
                else
                {
                    fgDtls.Rows[0].Cells[4].Value = "-";
                }
                dtEntryDate.Focus();
                Alert = false;
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
                (txtInwardNo.Text),
                (dtRefDate.TextFormat(false, true)),
                 (txtpartyLotNo.Text),
                (CboParty.SelectedValue),
                (cboBroker.SelectedValue),
                (cboDepartment.SelectedValue),
                (cboTransport.SelectedValue),
                (txtLrNo.Text),
                (dtLrDate.TextFormat(false, true)),
                (txtDescription.Text.ToString() == "" ? "-" : txtDescription.Text.ToString()),
                cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue,
                cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue,
                dtEd1.TextFormat(false,true), 
                txtET1.Text,
                txtET2.Text,
                txtET3.Text
                };
                int UnitId = 0;
                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockFabricLedger", "(#CodeID#)", Localization.ParseNativeInt(Conversions.ToString(base.iIDentity)));
                for (int i = 0; i <= (fgDtls.RowCount - 1); i++)
                {
                    if (fgDtls.Rows[i].Cells[12].Value.ToString().Length > 0)
                    {
                        string strLotNo = "-";
                        if (fgDtls.Rows[i].Cells[3].Value.ToString().Length > 0)
                        {
                            strLotNo = fgDtls.Rows[i].Cells[3].Value.ToString();
                        }
                        else
                        {
                            strLotNo = "-";
                        }
                        if (fgDtls.Rows[i].Cells[4].Value == null || fgDtls.Rows[i].Cells[4].Value.ToString() == "" || fgDtls.Rows[i].Cells[4].Value.ToString().Length == 0)
                        {
                            fgDtls.Rows[i].Cells[4].Value = "-";
                        }
                        strAdjQry += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)", (i + 1).ToString(),
                                "(#ENTRYNO#)", dtRefDate.Text,
                                Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                                Localization.ParseNativeInt(fgDtls.Rows[i].Cells[19].Value.ToString()),
                                base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                strLotNo, fgDtls.Rows[i].Cells[4].Value.ToString(),
                                 Localization.ParseNativeInt(fgDtls.Rows[i].Cells[5].Value.ToString()),
                                Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[7].Value.ToString()),
                                Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[6].Value.ToString()),
                                Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[8].Value.ToString()),
                                  Localization.ParseNativeInt(fgDtls.Rows[i].Cells[9].Value.ToString()),
                                Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[10].Value.ToString()),
                                Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[11].Value.ToString()),
                                Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[12].Value.ToString()),
                                fgDtls.Rows[i].Cells[13].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[13].Value.ToString()),
                                0, 0, 0, 0, "null", (fgDtls.Rows[i].Cells[20].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[20].Value.ToString())),
                                Localization.ParseNativeInt(CboParty.SelectedValue.ToString()), "(#CodeID#)",
                                0,
                                0, 0,
                                fgDtls.Rows[i].Cells[21].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[21].Value.ToString()),
                                fgDtls.Rows[i].Cells[22].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[22].Value.ToString()),
                                fgDtls.Rows[i].Cells[23].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[23].Value.ToString()),
                                fgDtls.Rows[i].Cells[24].Value == null || fgDtls.Rows[i].Cells[24].Value.ToString() == "" || fgDtls.Rows[i].Cells[24].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(fgDtls.Rows[i].Cells[24].Value.ToString()),
                                fgDtls.Rows[i].Cells[25].Value == null || fgDtls.Rows[i].Cells[25].Value.ToString() == "" || fgDtls.Rows[i].Cells[25].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(fgDtls.Rows[i].Cells[25].Value.ToString()),
                                fgDtls.Rows[i].Cells[26].Value == null || fgDtls.Rows[i].Cells[26].Value.ToString() == "" ? "-" : fgDtls.Rows[i].Cells[26].Value.ToString(),
                                fgDtls.Rows[i].Cells[27].Value == null || fgDtls.Rows[i].Cells[27].Value.ToString() == "" ? "-" : fgDtls.Rows[i].Cells[27].Value.ToString(),
                                fgDtls.Rows[i].Cells[28].Value == null || fgDtls.Rows[i].Cells[28].Value.ToString() == "" ? "-" : fgDtls.Rows[i].Cells[28].Value.ToString(),
                                fgDtls.Rows[i].Cells[29].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[29].Value.ToString()),
                                fgDtls.Rows[i].Cells[30].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[30].Value.ToString()),
                                "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID,
                                Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);

                        strAdjQry += DBSp.InsertIntoFabrIcStockLedger(Localization.ParseNativeDouble(base.iIDentity.ToString()), "(#CodeID#)", (i + 1).ToString(),
                                "(#ENTRYNO#)", dtRefDate.Text,
                                Localization.ParseNativeDouble(CboParty.SelectedValue.ToString()),
                                Localization.ParseNativeInt(fgDtls.Rows[i].Cells[19].Value.ToString()),
                                base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                                strLotNo, fgDtls.Rows[i].Cells[4].Value.ToString(),
                                 Localization.ParseNativeInt(fgDtls.Rows[i].Cells[5].Value.ToString()),
                                Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[7].Value.ToString()),
                                Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[6].Value.ToString()),
                                Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[8].Value.ToString()),
                                  Localization.ParseNativeInt(fgDtls.Rows[i].Cells[9].Value.ToString()),
                                Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[10].Value.ToString()), 0, 0, 0,
                                Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[11].Value.ToString()),
                                Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[12].Value.ToString()),
                                fgDtls.Rows[i].Cells[13].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[13].Value.ToString()),
                                0, "null", (fgDtls.Rows[i].Cells[20].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[20].Value.ToString())),
                                Localization.ParseNativeInt(CboParty.SelectedValue.ToString()), "(#CodeID#)",
                                0, 0, 0,
                                fgDtls.Rows[i].Cells[21].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[21].Value.ToString()),
                                fgDtls.Rows[i].Cells[22].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[22].Value.ToString()),
                                fgDtls.Rows[i].Cells[23].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[23].Value.ToString()),
                                fgDtls.Rows[i].Cells[24].Value == null || fgDtls.Rows[i].Cells[24].Value.ToString() == "" || fgDtls.Rows[i].Cells[24].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(fgDtls.Rows[i].Cells[24].Value.ToString()),
                                fgDtls.Rows[i].Cells[25].Value == null || fgDtls.Rows[i].Cells[25].Value.ToString() == "" || fgDtls.Rows[i].Cells[25].Value.ToString() == "0" ? "NULL" : Localization.ToSqlDateString(fgDtls.Rows[i].Cells[25].Value.ToString()),
                                fgDtls.Rows[i].Cells[26].Value == null || fgDtls.Rows[i].Cells[26].Value.ToString() == "" ? "-" : fgDtls.Rows[i].Cells[26].Value.ToString(),
                                fgDtls.Rows[i].Cells[27].Value == null || fgDtls.Rows[i].Cells[27].Value.ToString() == "" ? "-" : fgDtls.Rows[i].Cells[27].Value.ToString(),
                                fgDtls.Rows[i].Cells[28].Value == null || fgDtls.Rows[i].Cells[28].Value.ToString() == "" ? "-" : fgDtls.Rows[i].Cells[28].Value.ToString(),
                                fgDtls.Rows[i].Cells[29].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[29].Value.ToString()),
                                fgDtls.Rows[i].Cells[30].Value == null ? 0 : Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[30].Value.ToString()),
                                "NULL", i, 1, Db_Detials.StoreID, Db_Detials.CompID,
                                Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);

                        UnitId = Localization.ParseNativeInt(fgDtls.Rows[i].Cells[10].Value.ToString());
                    }
                }
                //if (cboTransport.SelectedValue != null && Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()) > 0.0)
                //{
                //    strAdjQry += DBSp.InsertIntoTrasportLedger("(#CodeID#)", txtInwardNo.Text.ToString(), dtRefDate.Text,
                //               Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()),
                //               Localization.ParseNativeDouble(CboParty.SelectedValue.ToString()),
                //               Localization.ParseNativeDouble(cboDepartment.SelectedValue.ToString()),
                //               txtLrNo.Text, dtLrDate.Text, null, UnitId, Localization.ParseNativeInt(string.Format("{0:N0}", CommonCls.GetColSum(fgDtls, 10, -1, -1))),
                //               Localization.ParseNativeDecimal(string.Format("{0:N2}", CommonCls.GetColSum(fgDtls, 11, -1, -1))), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID,
                //               DateAndTime.Now.Date);
                //}
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                DBSp.Transcation_AddEdit(pArrayData, fgDtls, true, strAdjQry, "", txtEntryNo.Text, "", "");
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

                if (!EventHandles.IsRequiredInGrid(fgDtls, this.dt_AryIsRequired, false))
                {
                    return true;
                }

                if (!Information.IsDate(Strings.Trim(dtEntryDate.Text.ToString())))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry Date");
                    dtEntryDate.Focus();
                    return true;
                }

                if (!CommonCls.CheckDate(dtEntryDate.Text, true))
                    return true;

                if (txtInwardNo.Text.Trim() == "" || txtInwardNo.Text.Trim() == "-" || txtInwardNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Inward No.");
                    txtInwardNo.Focus();
                    return true;
                }

                if (txtpartyLotNo.Text.Trim() == "" || txtpartyLotNo.Text.Trim() == "-" || txtpartyLotNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Party Lot No.");
                    this.txtpartyLotNo.Focus();
                    return true;
                }

                if (!Information.IsDate(dtRefDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Received Date");
                    dtRefDate.Focus();
                    return true;
                }

                if (!CommonCls.CheckDate(dtRefDate.Text, true))
                    return true;

                if (!CommonCls.CheckDate(dtLrDate.Text, true))
                    return true;

                if (CboParty.SelectedValue == null || CboParty.Text.Trim().ToString() == "-" || CboParty.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Party");
                    CboParty.Focus();
                    return true;
                }

                if (cboDepartment.SelectedValue == null || cboDepartment.Text.Trim().ToString() == "-" || cboDepartment.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    cboDepartment.Focus();
                    return true;
                }

                if (txtInwardNo.Text.Trim().Length > 0)
                {
                    string strTable;
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        strTable = "tbl_EmbFabricInwardMain";
                        if (Navigate.CheckDuplicate(ref strTable, "LotNo", txtInwardNo.Text, false, "", 0, " SupplierID = " + CboParty.SelectedValue + " AND StoreID=" + Db_Detials.StoreID + " and CompID = " + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " And YearID = " + Db_Detials.YearID + "", "This Party already used this Inward No in Entry No : " + DB.GetSnglValue(string.Format("Select Entryno from {0} where Supplierid = {1} and LotNo = {2} ", "tbl_EmbFabricInwardMain", CboParty.SelectedValue, txtInwardNo.Text.Trim()))))
                        {
                            txtInwardNo.Focus();
                            return true;
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        strTable = "tbl_EmbFabricInwardMain";
                        if (Navigate.CheckDuplicate(ref strTable, "LotNo", txtInwardNo.Text, true, "EmbFabInwardID", Localization.ParseNativeLong(txtCode.Text), " SupplierID = " + CboParty.SelectedValue + " AND StoreID=" + Db_Detials.StoreID + " and CompID = " + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " And YearID = " + Db_Detials.YearID + "", "This Party already used this Challan No in Entry No : " + DB.GetSnglValue(string.Format("Select Entryno from {0} where Supplierid = {1} and LotNo = {2} ", "tbl_EmbFabricInwardMain", CboParty.SelectedValue, txtInwardNo.Text.Trim()))))
                        {
                            txtInwardNo.Focus();
                            return true;
                        }
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
        #endregion

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!((base.blnFormAction == Enum_Define.ActionType.View_Record) || (base.blnFormAction == Enum_Define.ActionType.Not_Active)))
                {
                    if ((((e.ColumnIndex == 4) | (e.ColumnIndex == 6))) && ((fgDtls.Rows[e.RowIndex].Cells[6].Value != null) && (fgDtls.Rows[e.RowIndex].Cells[6].Value.ToString().Length > 0)))
                    {
                        fgDtls.Rows[e.RowIndex].Cells[7].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", RuntimeHelpers.GetObjectValue(fgDtls.Rows[e.RowIndex].Cells[6].Value))));
                    }
                    if ((e.ColumnIndex == 3) && (this.cboOrderNo.SelectedValue != null))
                    {
                        fgDtls.Rows[e.RowIndex].Cells[2].Value = Localization.ParseNativeInt(cboOrderNo.SelectedValue.ToString());
                    }
                    if (txtpartyLotNo.Text != "" || txtpartyLotNo.Text.ToString() != null)
                    {
                        fgDtls.Rows[e.RowIndex].Cells[3].Value = txtpartyLotNo.Text.Trim().ToString();
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
                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    if (fgDtls.Rows.Count > 1)
                    {
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
                fgDtls.Rows[e.RowIndex].Cells[18].Value = Localization.ParseNativeInt(cboDepartment.SelectedValue.ToString());
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
                                    if (Navigate.CheckDuplicate(ref strVal, "BatchNo", primaryFieldNameValue, false, "", 0L, "StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
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
                                    if (Navigate.CheckDuplicate(ref strVal, "BatchNo", fgDtls.Rows[e.RowIndex].Cells[4].Value.ToString(), true, "TransID", Localization.ParseNativeLong(txtCode.Text.Trim()), "StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
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
                    //if (((e.ColumnIndex == 4) | (e.ColumnIndex == 6)) && ((fgDtls.Rows[e.RowIndex].Cells[6].Value != null) && (Strings.Trim(Conversions.ToString(fgDtls.Rows[e.RowIndex].Cells[6].Value)).Length > 0)))
                    //{
                    //    fgDtls.Rows[e.RowIndex].Cells[7].Value = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select FabricQualityID From {0} Where FabricDesignID = {1}", "tbl_FabricDesignMaster", fgDtls.Rows[e.RowIndex].Cells[6].Value)));
                    //}
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboSupplier_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (((base.blnFormAction == Enum_Define.ActionType.New_Record) && (CboParty.SelectedValue != null)) && Localization.ParseNativeDouble(CboParty.SelectedValue.ToString()) > 0)
                {
                    cboBroker.SelectedValue = Localization.ParseNativeDouble(DB.GetSnglValue(string.Format("Select BrokerID From {0} Where LedgerID = {1} ", "tbl_LedgerMaster", CboParty.SelectedValue)));
                    cboTransport.SelectedValue = Localization.ParseNativeDouble(DB.GetSnglValue(string.Format("Select TransportID From {0} Where LedgerID = {1} ", "tbl_LedgerMaster", CboParty.SelectedValue)));
                    string sqlQuery = string.Format("Select * from {0}({1},{2},{3},{4},{5}) Where BalMtrs > 0", new object[] { "fn_FetchFabPurchaseOrderDtls", CboParty.SelectedValue, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID });
                    Combobox_Setup.Fill_Combo(this.cboOrderNo, sqlQuery, "FabPONo,FabPoDate,QualityID,Quality,BalPcs,BalMtrs", "FabPOID");
                    CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboOrderNo = this.cboOrderNo;
                    cboOrderNo.ColumnWidths = "0;100;0;0;100;50;80";
                    cboOrderNo.AutoComplete = true;
                    cboOrderNo.AutoDropdown = true;
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
                CIS_ReportTool.frmMultiPrint.TblNm = "tbl_EmbFabricInwardMain";
                CIS_ReportTool.frmMultiPrint.IdStr = "EmbFabInwardID";
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

        private void txtpartyLotNo_TextChanged(object sender, EventArgs e)
        {
            if (txtpartyLotNo.Text != null && txtpartyLotNo.Text != "" && txtpartyLotNo.Text != "0")
            {
                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    fgDtls.Rows[i].Cells[3].Value = txtpartyLotNo.Text;
                }
            }
        }

        private void cboDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            try 
            {
                EventHandles.CreateDefault_Rows(this.fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
            }
            catch { }
        }
    }
}
