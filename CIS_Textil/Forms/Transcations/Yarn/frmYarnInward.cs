using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_CLibrary;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Infragistics.Win;
using CIS_DBLayer;
using CIS_Textil;

namespace CIS_Textil
{
    public partial class frmYarnInward : frmTrnsIface
    {
        [AccessedThroughProperty("fgDtls")]
        private DataGridViewEx _fgDtls;
        public string strUniqueID;
        private int iMaxMyID;
        private bool isOrderOrDOused;
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
                DataGridViewRowsAddedEventHandler handler = new DataGridViewRowsAddedEventHandler(this.fgDtls_RowsAdded);
                DataGridViewCellEventHandler handler2 = new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                DataGridViewCellEventHandler handler4 = new DataGridViewCellEventHandler(this.fgDtls_CellEndEdit);
                KeyEventHandler handler1 = new KeyEventHandler(fgDtls_KeyDown);
                if (this._fgDtls != null)
                {
                    this._fgDtls.RowsAdded -= handler;
                    this._fgDtls.CellValueChanged -= handler2;
                    this._fgDtls.CellEndEdit -= handler4;
                    this._fgDtls.KeyDown -= handler1;

                }
                this._fgDtls = value;
                if (this._fgDtls != null)
                {
                    this._fgDtls.RowsAdded += handler;
                    this._fgDtls.CellValueChanged += handler2;
                    this._fgDtls.CellEndEdit += handler4;
                    this._fgDtls.KeyDown += handler1;

                }
            }
        }

        public frmYarnInward()
        {
            InitializeComponent();
            this.fgDtls = new DataGridViewEx();
            OrgInGridArray = new ArrayList();
        }

        #region Form Events
        private void frmYarnInward_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboSupplier, Combobox_Setup.ComboType.Mst_Suppliers, "");
                Combobox_Setup.FillCbo(ref cboDepart, Combobox_Setup.ComboType.Mst_Department, "");
                Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
                cboOrderType.AutoComplete = true;
                cboOrderType.AutoDropdown = true;
                DetailGrid_Setup.CreateDtlGrid(this, pnlDetail, fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                txtEntryNo.Enabled = false;
                if (blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    FillControls();
                }
                cboOrderType_SelectedIndexChanged(null, null);
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
                if (strUniqueID != null)
                {
                    string strQry = string.Format("Delete From tbl_YarnOrderLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                    strQry = strQry + string.Format("Update  tbl_YarnOrderLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                    DB.ExecuteSQL(strQry);
                    strQry = string.Format("Update tbl_YarnOrderLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                    DB.ExecuteSQL(strQry);
                }

                txtCode.Text = "";
                CIS_Textbox txtEntryNo = this.txtEntryNo;
                CommonCls.IncFieldID(this, ref txtEntryNo, "");

                int MaxiD = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(YarnInwdID),0) From {0}  Where CompID = {1} and YearID = {2}", "tbl_YarnInwardMain", Db_Detials.CompID, Db_Detials.YearID))));
                using (IDataReader reader = DB.GetRS(string.Format("Select * from {0} Where YarnInwdID = {1} and CompID={2} and YearID={3} and Isdeleted=0 ", new object[] { "tbl_YarnInwardMain", MaxiD, Db_Detials.CompID, Db_Detials.YearID })))
                {
                    while (reader.Read())
                    {
                        dtEntryDate.Text = Localization.ToVBDateString(reader["EntryDate"].ToString());
                        dtRefDate.Text = Localization.ToVBDateString(reader["RefDate"].ToString());
                        cboSupplier.SelectedValue = Localization.ParseNativeInt(reader["SupplierID"].ToString());
                        cboDepart.SelectedValue = Localization.ParseNativeInt(reader["DepartmentID"].ToString());
                        cboTransport.SelectedValue = Localization.ParseNativeInt(reader["TransportID"].ToString());

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
                dtEntryDate.Focus();
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                cboOrderType_SelectedIndexChanged(null, null);
                txtUniqueID.Text = CommonCls.GenUniqueID();
                strUniqueID = txtUniqueID.Text;
                OrgInGridArray.Clear();
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
                DBValue.Return_DBValue(this, txtCode, "YarnInwdID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtRefNo, "RefNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtRefDate, "RefDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboSupplier, "SupplierID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDepart, "DepartmentID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLrNo, "LrNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtLrDate, "LrDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtVehicleNo, "VehicleNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtNarration, "Narration", Enum_Define.ValidationType.Text);

                try
                {
                    string sOrderType = DBValue.Return_DBValue(this, "OrderType");
                    cboOrderType.SelectedItem = sOrderType;
                }
                catch { }
                DetailGrid_Setup.FillGrid(fgDtls, fgDtls.Grid_UID, fgDtls.Grid_Tbl, "YarnInwdID", txtCode.Text, base.dt_HasDtls_Grd, this.iIDentity, GlobalVariables.VALIDATE_EDIT, Db_Detials.CompID.ToString(), 3);
                int icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_YarnOrderLedger_Tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));

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
                            string strQry = string.Format("Update tbl_YarnOrderLedger Set UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + ", StatusID=2 Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + "");
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
                        string strQry = string.Format("Delete From tbl_YarnOrderLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                        strQry = strQry + string.Format("Update  tbl_YarnOrderLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                        DB.ExecuteSQL(strQry);
                        strQry = string.Format("Update tbl_YarnOrderLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                        DB.ExecuteSQL(strQry);
                    }
                }

                if ((base.blnFormAction == Enum_Define.ActionType.View_Record) && !(base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    icount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(StatusID) From fn_YarnOrderLedger_Tbl() Where TransType=" + iIDentity + " and TransID=" + txtCode.Text + " and StatusID=2")));
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
                            btnSelectOrder.Enabled = false;
                            fgDtls.Rows[i].ReadOnly = true;
                            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle_Ref;
                        }
                        else
                        {
                            btnSelectOrder.Enabled = true;
                            fgDtls.Rows[i].ReadOnly = false;
                        }
                    }
                }
                catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }


                System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                dgvCellStyle.BackColor = System.Drawing.Color.LightGray;
                dgvCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
                dgvCellStyle.SelectionBackColor = System.Drawing.Color.Purple;
                dgvCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

                try
                {
                    for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                    {
                        if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_YarnPurchase_FindDtls(" + Db_Detials.CompID + "," + Db_Detials.YearID + ")" + "WHERE YarnInwardID=" + fgDtls.Rows[i].Cells["YarnInwdID"].Value.ToString() + "")) > 0)
                        {
                            fgDtls.Rows[i].ReadOnly = true;
                            fgDtls.Rows[i].DefaultCellStyle = dgvCellStyle;
                        }
                        else
                            fgDtls.Rows[i].ReadOnly = false;
                    }
                }
                catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                cboOrderType_SelectedIndexChanged(null, null);
                OrgInGridArray.Clear();
            }
            catch (Exception exception1)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", exception1.Message);
            }
        }

        public void SaveRecord()
        {
            try
            {
                string sTotQty = "", sTotWeight = "";
                sTotQty = string.Format("{0:N0}", CommonCls.GetColSum(this.fgDtls, 11, -1, -1));
                sTotWeight = string.Format("{0:N3}", CommonCls.GetColSum(this.fgDtls, 15, -1, -1));
                ArrayList pArrayData = new ArrayList
                {
                    "(#ENTRYNO#)",
                    dtEntryDate.TextFormat(false, true),
                    cboOrderType.SelectedItem.ToString(),
                    txtRefNo.Text,
                    dtRefDate.TextFormat(false, true),
                    cboSupplier.SelectedValue,
                    cboDepart.SelectedValue,
                    cboTransport.SelectedValue,
                    txtLrNo.Text.ToString(),
                    dtLrDate.TextFormat(false, true),
                    txtVehicleNo.Text.ToString(),
                    sTotQty.Replace(",",""),
                    sTotWeight.Replace(",",""),
                    txtNarration.Text.ToString()
                };

                string strAdjQry = string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_StockYarnLedger", "(#CodeID#)", Localization.ParseNativeInt(base.iIDentity.ToString()));
                strAdjQry += string.Format("Delete From {0} Where TransID = {1} And TransType = {2};", "tbl_YarnOrderLedger", "(#CodeID#)", Localization.ParseNativeInt(base.iIDentity.ToString()));
                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    DataGridViewRow row = fgDtls.Rows[i];
                    string LotNo = "";

                    if (row.Cells[9].Value.ToString() != null && row.Cells[9].Value.ToString().Length > 0)
                    {
                        LotNo = row.Cells[9].Value.ToString();
                    }
                    else
                    {
                        LotNo = "-";
                    }

                    if (Localization.ParseNativeDouble(row.Cells[15].Value.ToString()) > 0)
                    {
                        //strAdjQry = strAdjQry + DBSp.InsertIntoYarnStockLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)",
                        //            Localization.ParseNativeDouble(base.iIDentity.ToString()), Localization.ParseNativeDouble(cboDepart.SelectedValue.ToString()), base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(), LotNo,
                        //            dtRefDate.Text, Localization.ParseNativeDouble(row.Cells[5].Value.ToString()), Localization.ParseNativeDouble(row.Cells[6].Value.ToString()),
                        //            Localization.ParseNativeDouble(row.Cells[7].Value.ToString()), Localization.ParseNativeDouble(row.Cells[19].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                        //            Localization.ParseNativeDecimal(row.Cells[12].Value == null ? "NULL" : row.Cells[12].Value.ToString() == "" ? "0" : row.Cells[12].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()), 0, 0,
                        //            0, "", fgDtls.Rows[i].Cells[21].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[21].Value.ToString()), "null", i, 1,
                        //            Localization.ParseNativeInt(cboSupplier.SelectedValue.ToString()), "(#CodeID#)", 0, 0,
                        //            base.iIDentity.ToString() + "|" + "(#CodeID#)" + "|" + (i + 1).ToString(),
                        //            fgDtls.Rows[i].Cells[23].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[23].Value.ToString()),
                        //            fgDtls.Rows[i].Cells[24].Value == null ? 0 : Localization.ParseNativeInt(fgDtls.Rows[i].Cells[24].Value.ToString()),
                        //            Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date, (row.Cells[10].Value == null ? "NULL" : row.Cells[10].Value.ToString() == "" ? "" : row.Cells[10].Value.ToString()));
                    }

                    if (row.Cells[2].Value != null && row.Cells[2].Value.ToString() != "")
                    {
                        string sBatchNo = string.Empty;
                        sBatchNo = DB.GetSnglValue("Select PONo from fn_YarnPurchaseOrderMain_Tbl() Where YPOID=" + row.Cells[2].Value.ToString());

                        if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                        {
                            if (Localization.ParseNativeDouble(row.Cells[15].Value.ToString()) > 0)
                            {
                                //strAdjQry += DBSp.InsertIntoYarnOrderLedger("(#CodeID#)", (i + 1).ToString(), "(#ENTRYNO#)", Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                //             Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()), (row.Cells[18].Value == null ? "NULL" : row.Cells[18].Value.ToString() == "-" ? "0" : row.Cells[18].Value.ToString() == "" ? "0" : row.Cells[18].Value.ToString()),
                                //             row.Cells[2].Value.ToString(), sBatchNo, dtRefDate.Text, Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                //             Localization.ParseNativeDouble(row.Cells[6].Value.ToString()), Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                //             Localization.ParseNativeDouble(row.Cells[19].Value.ToString()), 0, 0, 0, Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                //             row.Cells[12].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                                //             "null", "null", i, 1, "Purchase", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                            }
                        }
                    }
                    row = null;
                }
                strAdjQry = strAdjQry.Replace("'null'", "null").Replace("Nnull", "null");
                if ((this.cboTransport.SelectedValue != null) && (Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()) > 0.0))
                {
                    //strAdjQry = strAdjQry + DBSp.InsertIntoTrasportLedger("(#CodeID#)", txtRefNo.Text.ToString(), dtRefDate.Text,
                    //            Localization.ParseNativeDouble(iIDentity.ToString()), Localization.ParseNativeDouble(cboTransport.SelectedValue.ToString()),
                    //            Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()), Localization.ParseNativeDouble(cboDepart.SelectedValue.ToString()),
                    //            txtLrNo.Text, dtLrDate.Text, txtVehicleNo.Text, 0, Localization.ParseNativeInt(sTotQty),
                    //            Localization.ParseNativeDecimal(sTotWeight), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                }
                strAdjQry += "Delete From tbl_YarnOrderLedger Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";";
                DBSp.Transcation_AddEdit(pArrayData, this.fgDtls, true, strAdjQry, "", txtEntryNo.Text, "", "");
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
                if (!Information.IsDate(dtEntryDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry Date");
                    dtEntryDate.Focus();
                    return true;
                }
                if (txtEntryNo.Text.Trim() == "" || txtEntryNo.Text.Trim() == "-" || txtEntryNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry No.");
                    txtEntryNo.Focus();
                    return true;
                }
                if (txtRefNo.Text.Trim() == "" || txtRefNo.Text.Trim() == "-" || txtRefNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Ref No.");
                    txtRefNo.Focus();
                    return true;
                }
                if (!Information.IsDate(Strings.Trim(this.dtRefDate.Text.ToString())))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Ref Date");
                    dtRefDate.Focus();
                    return true;
                }
                if (cboSupplier.SelectedValue == null || cboSupplier.SelectedValue.ToString() == "-" || cboSupplier.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Supplier");
                    cboSupplier.Focus();
                    return true;
                }
                if (cboDepart.SelectedValue == null || cboDepart.SelectedValue.ToString() == "-" || cboDepart.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Department");
                    cboDepart.Focus();
                    return true;
                }
                if (txtRefNo.Text.Trim().Length > 0)
                {
                    string strTableName;
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        strTableName = "fn_YarnInwardMain_tbl()";
                        if (Navigate.CheckDuplicate(ref strTableName, "RefNo", txtRefNo.Text, false, "", 0L, Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(" SupplierID = ", this.cboSupplier.SelectedValue), "AND CompID = "), Db_Detials.CompID), "And YearID = "), Db_Detials.YearID)), "This Party already used this Ref No in Entry No : " + DB.GetSnglValue(string.Format("Select Entryno from {0} where Supplierid = {1} and RefNo = {2} ", "tbl_YarnInwardMain", RuntimeHelpers.GetObjectValue(this.cboSupplier.SelectedValue), this.txtRefNo.Text.Trim()))))
                        {
                            txtRefNo.Focus();
                            return true;
                        }
                    }
                    else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        strTableName = "fn_YarnInwardMain_tbl()";
                        if (Navigate.CheckDuplicate(ref strTableName, "RefNo", txtRefNo.Text, true, "YarnInwdID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(Operators.ConcatenateObject(" SupplierID = ", this.cboSupplier.SelectedValue), "AND CompID = "), Db_Detials.CompID), "And YearID = "), Db_Detials.YearID)), "This Party already used this Ref No in Entry No : " + DB.GetSnglValue(string.Format("Select Entryno from {0} where Supplierid = {1} and RefNo = {2} ", "tbl_YarnInwardMain", cboSupplier.SelectedValue, txtRefNo.Text.Trim()))))
                        {
                            txtRefNo.Focus();
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

        #region Other

        private void fgDtls_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
        }

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                isOrderOrDOused = false;
                if ((e.ColumnIndex == 11) | (e.ColumnIndex == 13) | (e.ColumnIndex == 15))
                {
                    ExecuterTempQry(e.RowIndex);
                }
                if (fgDtls.Rows.Count > 1)
                {
                    cboOrderType.Enabled = false;
                }
                else
                {
                    cboOrderType.Enabled = true;
                }
                disablelot();
                for (int l = 0; l <= fgDtls.Rows.Count - 1; l++)
                {
                    if ((fgDtls.Rows[l].Cells[3].Value != null && fgDtls.Rows[l].Cells[3].Value.ToString() != "" && fgDtls.Rows[l].Cells[3].Value.ToString() != "-" && fgDtls.Rows[l].Cells[3].Value.ToString() != "0") || (fgDtls.Rows[l].Cells[2].Value != null && fgDtls.Rows[l].Cells[2].Value.ToString() != "" && fgDtls.Rows[l].Cells[2].Value.ToString() != "-" && fgDtls.Rows[l].Cells[2].Value.ToString() != "0"))
                    {
                        isOrderOrDOused = true;
                        break;
                    }
                }
                if (Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString()) > 0 || Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[3].Value.ToString()) > 0 || isOrderOrDOused == true)
                {
                    EnabDisab(false);
                }
                else
                {
                    EnabDisab(true);
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (fgDtls.Rows.Count > 1)
            {
                cboOrderType.Enabled = false;
            }
            else
            {
                cboOrderType.Enabled = true;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                bool isIndexAppld = false;
                int iIndex = fgDtls.RowCount - 1;
                for (int m = 0; m <= fgDtls.RowCount - 1; m++)
                {
                    if (fgDtls.Rows[m].Cells[3].Value != null && fgDtls.Rows[m].Cells[3].Value.ToString() != "")
                    {
                        iIndex = m;
                        isIndexAppld = true;
                    }
                }
                if (!isIndexAppld)
                {
                    iIndex = -1;
                }

                #region StockAdjQuery
                string strQry = string.Empty;
                int ibitcol = 0;
                string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", iIDentity, 0.0));
                string strQry_ColName = "";
                string[] arr = CommonCls.GetAdjColName(base.iIDentity, 0.0).Split(';');
                strQry_ColName = arr[0].ToString();
                ibitcol = Localization.ParseNativeInt(arr[1]);
                strQry = string.Format("Select {0} From {1} ({2}, {3}, {4}) ", new object[] { strQry_ColName, snglValue, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(dtRefDate.Text))), Db_Detials.CompID, Db_Detials.YearID });
                #endregion

                frmStockAdj frmAdj = new frmStockAdj();
                frmAdj.MenuID = base.iIDentity;
                frmAdj.Entity_IsfFtr = 0.0;
                frmAdj.ref_fgDtls = fgDtls;
                frmAdj.QueryString = strQry;
                frmAdj.IsRefQuery = true;
                frmAdj.ibitCol = ibitcol;
                frmAdj.UsedInGridArray = OrgInGridArray;

                if (frmAdj.ShowDialog() == DialogResult.Cancel)
                {
                    frmAdj.Dispose();
                }
                else
                {
                    frmAdj.Dispose();
                    frmAdj = null;
                    int iRows = fgDtls.RowCount - 1;
                    for (int i = 0; i < iRows; i++)
                    {
                        if (fgDtls.Rows[i].Index > iIndex || iIndex == -1)
                        {
                            if ((fgDtls.Rows[i].Cells[11].Value != null) && (fgDtls.Rows[i].Cells[11].Value.ToString() != "0") && (fgDtls.Rows[i].Cells[11].Value.ToString() != ""))
                            {
                                //if (fgDtls.Rows[i].Cells[11].Value.ToString() != fgDtls.Rows[i].Cells[15].Value.ToString())
                                {
                                    double iPcs = Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[11].Value.ToString());

                                    if (fgDtls.Rows[i].Cells[16].Value != null)
                                    {
                                        if (Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[16].Value.ToString()) > iPcs)
                                        {
                                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Not Enough Bal Bags");
                                        }
                                    }

                                    if (fgDtls.Rows[i].Cells[16].Value != null)
                                    {
                                        if (Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[16].Value.ToString()) < iPcs)
                                        {
                                            iPcs = Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[16].Value.ToString());
                                        }
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
                                                else if (m != 13)
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
                                fgDtls.Rows[i].Cells[11].Value = fgDtls.Rows[i].Cells[16].Value.ToString();
                            }
                        }
                    }
                    fgDtls.Rows.RemoveAt(fgDtls.RowCount - 1);
                    SendKeys.Send("{TAB}");
                    if (fgDtls.Rows.Count > 0)
                    {
                        fgDtls.CurrentCell = fgDtls[5, 0];
                    }
                    fgDtls.Select();

                    if (fgDtls.RowCount == 0)
                    {
                        EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                    }

                    for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                    {
                        fgDtls.Rows[i].Cells[14].Value = "0.000";
                        fgDtls.Rows[i].Cells[15].Value = Operators.SubtractObject(fgDtls.Rows[i].Cells[13].Value, fgDtls.Rows[i].Cells[14].Value);
                    }
                }
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", exception1.Message);
            }
        }

        public string SetDepartment
        {
            get
            {
                return cboDepart.SelectedValue.ToString();
            }
            set
            {
                if (value.Length != 0)
                {
                    cboDepart.SelectedValue = value;
                }
            }
        }

        public string SetSupplire
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

        public string SetLRNo
        {
            get
            {
                return this.txtLrNo.Text;
            }
            set
            {
                if (value.Length != 0)
                {
                    txtLrNo.Text = value;
                }
            }
        }

        public string SetLrDate
        {
            get
            {
                return this.dtLrDate.TextFormat(false, true);
            }
            set
            {
                if (value.Length != 0)
                {
                    dtLrDate.Text = (Localization.ToVBDateString(value).ToString());
                }
            }
        }

        private void cboSupplier_LostFocus(object sender, EventArgs e)
        {
            try
            {

            }
            catch { }
        }

        public void PrintRecord()
        {
            try
            {
                CIS_ReportTool.frmMultiPrint frmMultiPrint = new CIS_ReportTool.frmMultiPrint();
                CIS_ReportTool.frmMultiPrint.MenuID = base.iIDentity;
                CIS_ReportTool.frmMultiPrint.Id = Localization.ParseNativeInt(this.txtCode.Text);
                CIS_ReportTool.frmMultiPrint.TblNm = "tbl_YarnInwardMain";
                CIS_ReportTool.frmMultiPrint.IdStr = "YarnInwdID";
                CIS_ReportTool.frmMultiPrint.iStoreID = Db_Detials.StoreID;
                CIS_ReportTool.frmMultiPrint.iCompID = Db_Detials.CompID;
                CIS_ReportTool.frmMultiPrint.iBranchID = Db_Detials.BranchID;
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

        #endregion

        private void btnSelectOrder_Click(object sender, EventArgs e)
        {
            try
            {
                bool isIndexAppld = false;
                int iIndex = fgDtls.RowCount - 1;
                for (int m = 0; m <= fgDtls.RowCount - 1; m++)
                {
                    if (fgDtls.Rows[m].Cells[2].Value != null && fgDtls.Rows[m].Cells[2].Value.ToString() != "")
                    {
                        iIndex = m;
                        isIndexAppld = true;
                    }
                }
                if (!isIndexAppld)
                {
                    iIndex = -1;
                }

                string sSupID = string.Empty;
                if (cboSupplier.SelectedValue == null || cboSupplier.SelectedValue.ToString() == "-" || cboSupplier.SelectedValue.ToString() == "" || cboSupplier.SelectedValue.ToString() == "0")
                {
                    sSupID = "0";
                }
                else
                {
                    sSupID = cboSupplier.SelectedValue.ToString();
                }
                if (!Information.IsDate(dtRefDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Ref Date");
                    dtRefDate.Focus();
                    return;
                }

                #region StockAdjQuery
                string strQry = string.Empty;
                int ibitcol = 0;
                string snglValue = DB.GetSnglValue(string.Format("Select Top 1 QueryName From {0} Where GridID = {1} And GridType = {2}", "tbl_GridFields_Mapping", iIDentity, 1.0));
                string strQry_ColName = "";
                string[] arr = CommonCls.GetAdjColName(base.iIDentity, 1.0).Split(';');
                strQry_ColName = arr[0].ToString();
                ibitcol = Localization.ParseNativeInt(arr[1]);
                strQry = string.Format(" Select {0} From {1} ({2}, {3}, {4}, {5}, {6}, {7}) Where OrderTransType ='Purchase' Order by MyId ", new object[] { strQry_ColName, snglValue, DB.SQuoteNotUnicode(Localization.ToSqlDateString(Conversions.ToString(dtRefDate.Text))), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, sSupID });
                #endregion

                frmStockAdj frmStockAdj = new frmStockAdj();
                frmStockAdj.MenuID = base.iIDentity;
                frmStockAdj.Entity_IsfFtr = 1;
                frmStockAdj.ref_fgDtls = this.fgDtls;
                frmStockAdj.AsonDate = Conversions.ToDate(this.dtRefDate.Text);
                frmStockAdj.LedgerID = sSupID;
                frmStockAdj.IsStock = false;
                frmStockAdj.QueryString = strQry;
                frmStockAdj.IsRefQuery = true;
                frmStockAdj.ibitCol = ibitcol;

                if (frmStockAdj.ShowDialog() == DialogResult.Cancel)
                {
                    frmStockAdj.Dispose();
                }
                else
                {
                    frmStockAdj.Dispose();
                    frmStockAdj = null;

                    #region Edit
                    int iRows = fgDtls.RowCount - 1;
                    for (int i = 0; i < iRows; i++)
                    {
                        if (fgDtls.Rows[i].Index > iIndex || iIndex == -1)
                        {
                            if ((fgDtls.Rows[i].Cells[11].Value != null) && (fgDtls.Rows[i].Cells[11].Value.ToString() != "0") && (fgDtls.Rows[i].Cells[11].Value.ToString() != ""))
                            {
                                //if (fgDtls.Rows[i].Cells[11].Value.ToString() != fgDtls.Rows[i].Cells[15].Value.ToString())
                                {
                                    double iPcs = Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[11].Value.ToString());

                                    if (fgDtls.Rows[i].Cells[16].Value != null)
                                    {
                                        if (Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[16].Value.ToString()) > iPcs)
                                        {
                                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Not Enough Bal Bags");
                                            //return;
                                        }
                                    }

                                    if (fgDtls.Rows[i].Cells[16].Value != null)
                                    {
                                        if (Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[16].Value.ToString()) < iPcs)
                                        {
                                            iPcs = Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[16].Value.ToString());
                                        }
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
                                                else if (m != 13)
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
                                fgDtls.Rows[i].Cells[11].Value = fgDtls.Rows[i].Cells[16].Value.ToString();
                            }
                        }
                    }
                    fgDtls.Rows.RemoveAt(fgDtls.RowCount - 1);
                    SendKeys.Send("{TAB}");
                    if (fgDtls.Rows.Count > 0)
                    {
                        fgDtls.CurrentCell = fgDtls[2, fgDtls.RowCount - 1];
                    }
                    fgDtls.Select();
                    setTempRowIndex();
                    setMyID();
                    ExecuterTempQry(-1);

                    if (fgDtls.RowCount == 0)
                    {
                        EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                    }

                    for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                    {
                        fgDtls.Rows[i].Cells[14].Value = "0.000";
                        fgDtls.Rows[i].Cells[15].Value = Operators.SubtractObject(fgDtls.Rows[i].Cells[13].Value, fgDtls.Rows[i].Cells[14].Value);
                    }
                    #endregion
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
                    fgDtls.Columns[2].ReadOnly = true;
                    fgDtls.Columns[3].ReadOnly = true;
                    if (cboOrderType.SelectedItem.ToString() == "WITH ORDER")
                    {
                        btnSelectOrder.Enabled = true;
                        btnSelectDO.Enabled = false;
                        fgDtls.Columns[2].Visible = true;
                        fgDtls.Columns[3].Visible = false;
                        fgDtls.Columns[5].ReadOnly = true;
                        fgDtls.Columns[6].ReadOnly = true;
                        fgDtls.Columns[7].ReadOnly = true;
                        fgDtls.Columns[19].ReadOnly = true;
                    }
                    else if (cboOrderType.SelectedItem.ToString() == "WITHOUT ORDER")
                    {
                        btnSelectOrder.Enabled = false;
                        btnSelectDO.Enabled = true;
                        fgDtls.Columns[2].Visible = false;
                        fgDtls.Columns[3].Visible = true;
                        fgDtls.Columns[5].ReadOnly = false;
                        fgDtls.Columns[6].ReadOnly = false;
                        fgDtls.Columns[7].ReadOnly = false;
                        fgDtls.Columns[19].ReadOnly = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public void ExecuterTempQry(int RowIndex)
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
                                strQry = string.Format("Delete From tbl_YarnOrderLedger Where Dr_Bags=0 and Dr_Weight=0 and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
                                for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                                {
                                    DataGridViewRow row = fgDtls.Rows[i];
                                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                                    {
                                        StatusID = 1;
                                        MyID = iMaxMyID.ToString();
                                    }
                                    else
                                    {
                                        StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_YarnOrderLedger_Tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_YarnOrderLedger_Tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + i + "")));
                                        MyID = txtCode.Text;
                                    }

                                    if (MyID != "" && row.Cells[2].Value != null && row.Cells[2].Value.ToString() != "" && row.Cells[2].Value.ToString() != "0" && row.Cells[15].Value != null && row.Cells[15].Value.ToString() != "")
                                    {
                                        string sBatchNo = string.Empty;
                                        sBatchNo = DB.GetSnglValue("Select PONo from fn_YarnPurchaseOrderMain_Tbl() Where YPOID=" + row.Cells[2].Value.ToString());

                                        //strQry += DBSp.InsertIntoYarnOrderLedger(MyID, (i + 1).ToString(), txtEntryNo.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                        //Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()), (row.Cells[18].Value == null ? "NULL" : row.Cells[18].Value.ToString() == "-" ? "0" : row.Cells[18].Value.ToString() == "" ? "0" : row.Cells[18].Value.ToString()),
                                        //row.Cells[2].Value.ToString(), sBatchNo, dtRefDate.Text, Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                        //Localization.ParseNativeDouble(row.Cells[6].Value.ToString()), Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                        //Localization.ParseNativeDouble(row.Cells[19].Value.ToString()), 0, 0, 0, Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                        //row.Cells[12].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                                        //"null", txtUniqueID.Text, i, StatusID, "Purchase", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
                                    }
                                }
                            }
                            else
                            {
                                if ((fgDtls.CurrentCell.ColumnIndex == 11) || (fgDtls.CurrentCell.ColumnIndex == 13) || (fgDtls.CurrentCell.ColumnIndex == 15))
                                {
                                    DataGridViewRow row = fgDtls.Rows[RowIndex];
                                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                                    {
                                        StatusID = 1;
                                        MyID = iMaxMyID.ToString();
                                    }
                                    else
                                    {
                                        StatusID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_YarnOrderLedger_Tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + ""))) == 0 ? 1 : Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select StatusID From fn_YarnOrderLedger_Tbl() Where UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and Rowindex=" + RowIndex + "")));
                                        MyID = txtCode.Text;
                                    }

                                    if (MyID != "" && row.Cells[2].Value != null && row.Cells[2].Value.ToString() != "" && row.Cells[2].Value.ToString() != "0" && row.Cells[15].Value != null && row.Cells[15].Value.ToString() != "")
                                    {
                                        string sBatchNo = string.Empty;
                                        sBatchNo = DB.GetSnglValue("Select PONo from fn_YarnPurchaseOrderMain_Tbl() Where YPOID=" + row.Cells[2].Value.ToString());

                                        if (txtUniqueID.Text != null)
                                        {
                                            strQry += string.Format("Delete From tbl_YarnOrderLedger Where Dr_Bags=0 and Dr_Weight=0 and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value.ToString()) + " and AddedBy=" + Db_Detials.UserID + ";");

                                            //strQry += DBSp.InsertIntoYarnOrderLedger(MyID, (RowIndex + 1).ToString(), txtEntryNo.Text, Localization.ParseNativeDouble(base.iIDentity.ToString()),
                                            //       Localization.ParseNativeDouble(cboSupplier.SelectedValue.ToString()), (row.Cells[18].Value == null ? "NULL" : row.Cells[18].Value.ToString() == "-" ? "0" : row.Cells[18].Value.ToString() == "" ? "0" : row.Cells[18].Value.ToString()),
                                            //       row.Cells[2].Value.ToString(), sBatchNo, dtRefDate.Text, Localization.ParseNativeDouble(row.Cells[5].Value.ToString()),
                                            //       Localization.ParseNativeDouble(row.Cells[6].Value.ToString()), Localization.ParseNativeDouble(row.Cells[7].Value.ToString()),
                                            //       Localization.ParseNativeDouble(row.Cells[19].Value.ToString()), 0, 0, 0, Localization.ParseNativeDecimal(row.Cells[11].Value.ToString()),
                                            //       row.Cells[12].Value == null ? 0 : Localization.ParseNativeDecimal(row.Cells[12].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[15].Value.ToString()),
                                            //       "null", txtUniqueID.Text, Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value.ToString()), StatusID, "Purchase", Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, DateAndTime.Now.Date);
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

        protected void fgDtls_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string SRefID = "", SInwardID = "";

                object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                if ((e.Control == true & e.KeyCode == Keys.D) | e.KeyCode == Keys.F5)
                {
                    object frm = Navigate.GetActiveChild();
                    dynamic frmObj = frm;
                    int iCalcCol = 0;
                    CIS_DataGridViewEx.DataGridViewEx fgDtls = (CIS_DataGridViewEx.DataGridViewEx)sender;

                    if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        try
                        {
                            if (fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[17].Value != null)
                            {
                                SRefID = fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[17].Value.ToString();
                            }
                            else
                            {
                                SRefID = "''";
                            }

                            if (fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[0].Value != null)
                            {
                                SInwardID = fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[0].Value.ToString();
                            }
                            else
                            {
                                SInwardID = "''";
                            }

                            if ((Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select COUNT(0) From fn_YarnOrderLedger_Tbl() Where RefId='" + SRefID + "' and RefID<>'' and Transtype<>" + iIDentity + ""))) > 0) || Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_YarnPurchase_FindDtls(" + Db_Detials.CompID + "," + Db_Detials.YearID + ")" + "WHERE YarnInwardID=" + SInwardID + "")) > 0)
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityWarning, "", "Reference Found In Another Module..Row Cannot Be Deleted");
                                return;
                            }
                            else
                            {
                                try
                                {
                                    string strQry = string.Format("Update tbl_YarnOrderLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
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
                        catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                    }
                    if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    {
                        try
                        {
                            try
                            {
                                string strQry = string.Format("Update tbl_YarnOrderLedger Set IsDeleted=1,DeletedOn=Getdate() Where RowIndex=" + Localization.ParseNativeInt(fgDtls.Rows[fgDtls.CurrentRow.Index].Cells[22].Value.ToString()) + " and UniqueID=" + CommonLogic.SQuote(txtUniqueID.Text) + " and AddedBy=" + Db_Detials.UserID + ";");
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
                        catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                    }
                }

            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void setTempRowIndex()
        {
            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[22].Value = i;
            }
        }

        private void frmYarnInward_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (strUniqueID != null)
            {
                string strQry = string.Format("Delete From tbl_YarnOrderLedger Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and StatusID=1 and AddedBy=" + Db_Detials.UserID + ";");
                strQry = strQry + string.Format("Update  tbl_YarnOrderLedger Set IsDeleted=0 Where UniqueID=" + CommonLogic.SQuote(strUniqueID) + " and TransType=" + iIDentity + " and IsDeleted=1 and AddedBy=" + Db_Detials.UserID + ";");
                DB.ExecuteSQL(strQry);
                strQry = string.Format("Update tbl_YarnOrderLedger Set StatusID=1,UniqueID=null Where StatusID=2 and TransType=" + iIDentity + " and UniqueID=" + CommonLogic.SQuote(strUniqueID) + "");
                DB.ExecuteSQL(strQry);
            }
        }

        private void disablelot()
        {
            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                if (fgDtls.Rows[i].Cells[3].Value != null && fgDtls.Rows[i].Cells[3].Value.ToString() != "" && fgDtls.Rows[i].Cells[3].Value.ToString() != "0")
                {
                    fgDtls.Rows[i].Cells[9].ReadOnly = true;
                }
                else
                {
                    fgDtls.Rows[i].Cells[9].ReadOnly = false;
                }
            }
        }

        private void setMyID()
        {
            iMaxMyID = Localization.ParseNativeInt(DB.GetSnglValue("Select MAX(MyId + 1) from tbl_YarnOrderLedger Where IsDeleted=0"));

            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[20].Value = iMaxMyID;
            }
        }

        private void EnabDisab(bool isenab)
        {
            if (isenab)
            {
                fgDtls.Columns[5].ReadOnly = false;
                fgDtls.Columns[6].ReadOnly = false;
                fgDtls.Columns[7].ReadOnly = false;
                fgDtls.Columns[19].ReadOnly = false;
            }
            else
            {
                fgDtls.Columns[5].ReadOnly = true;
                fgDtls.Columns[6].ReadOnly = true;
                fgDtls.Columns[7].ReadOnly = true;
                fgDtls.Columns[19].ReadOnly = true;
            }
        }
    }
}
