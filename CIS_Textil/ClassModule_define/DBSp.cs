using System;
using System.Collections;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

public class DBSp
{
    public static object instance = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
    public static int iactionType = 0;

    #region " Get Entry Action Type"

    public static bool rtnAction()
    {
        try
        {
            object frm = Navigate.GetActiveChild();
            dynamic objfrm = frm;
            return (objfrm.blnFormAction == Enum_Define.ActionType.Edit_Record ? false : true);
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return true;
        }
    }

    public static void GetActionType()
    {
        try
        {
            object frm = Navigate.GetActiveChild();
            dynamic objfrm = frm;

            if (objfrm.blnFormAction == Enum_Define.ActionType.New_Record)
            {
                iactionType = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster() Where MiscName='isadd'"));
            }
            else if (objfrm.blnFormAction == Enum_Define.ActionType.Edit_Record)
            {
                iactionType = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster() Where MiscName='isEdit'"));
            }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    #endregion

    #region " Execute Add/Edit For Masters or Transcations"

    public static double Master_AddEdit(ArrayList pArrayData, string str1 = "")
    {
        try
        {
            object frm = Navigate.GetActiveChild();
            dynamic objfrm = frm;
            string strQuery = string.Empty;
            if (((DataTable)objfrm.ds).Rows.Count == 0)
            {
                strQuery = GetDB_Column(Conversions.ToString(objfrm.strTableName), rtnAction(), Conversions.ToString(0), pArrayData, true, false);
            }
            else
            {
                strQuery = GetDB_Column(Conversions.ToString(objfrm.strTableName), rtnAction(), ((DataTable)objfrm.ds).Rows[Conversions.ToInteger(objfrm.RecordNo)][0].ToString(), pArrayData, true, false);
            }
            return DB.ExecuteSQL_InsertUpdate(strQuery, rtnAction(), str1);
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return 0;
        }

    }

    public static double Master_AddEdit_Trns(ArrayList pArrayData, ref double dblTransID, string str1 = "")
    {
        try
        {
            object frm = Navigate.GetActiveChild();
            dynamic objfrm = frm;
            string strQuery = string.Empty;
            if (((DataTable)objfrm.ds).Rows.Count == 0)
            {
                strQuery = GetDB_Column(Conversions.ToString(objfrm.strTableName), rtnAction(), Conversions.ToString(0), pArrayData, true, false);
            }
            else
            {
                strQuery = GetDB_Column(Conversions.ToString(objfrm.strTableName), rtnAction(), ((DataTable)objfrm.ds).Rows[Conversions.ToInteger(objfrm.RecordNo)][0].ToString(), pArrayData, true, false);
            }
            return DB.ExecuteSQL_InsertUpdate(strQuery, ref dblTransID, rtnAction(), str1);
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return 0;
        }
    }

    public static object Transcation_AddEdit(ArrayList pArrayData, CIS_DataGridViewEx.DataGridViewEx fgDtls)
    {
        return Transcation_AddEdit(pArrayData, fgDtls, false, "", "", null);
    }

    public static object Transcation_AddEdit(ArrayList pArrayData, CIS_DataGridViewEx.DataGridViewEx fgDtls, bool PopulateDtls)
    {
        return Transcation_AddEdit(pArrayData, fgDtls, PopulateDtls, "", "", null);
    }

    public static object Transcation_AddEdit(ArrayList pArrayData, CIS_DataGridViewEx.DataGridViewEx fgDtls, bool PopulateDtls, string str1)
    {
        return Transcation_AddEdit(pArrayData, fgDtls, PopulateDtls, str1, "", null);
    }

    public static object Transcation_AddEdit(ArrayList pArrayData, CIS_DataGridViewEx.DataGridViewEx fgDtls, bool PopulateDtls, string str1, params CIS_DataGridViewEx.DataGridViewEx[] args)
    {
        return Transcation_AddEdit(pArrayData, fgDtls, PopulateDtls, str1, "", args);
    }

    public static object Transcation_AddEdit(ArrayList pArrayData, CIS_DataGridViewEx.DataGridViewEx fgDtls, bool PopulateDtls, string str1, string str2, string sEntryNo, string sOtherNo = "", string sOtherName = "", params CIS_DataGridViewEx.DataGridViewEx[] args)
    {
        return Transcation_AddEdit_Validate(pArrayData, fgDtls, PopulateDtls, str1, str2, sEntryNo, sOtherNo, sOtherName, args);
    }

    public static object Transcation_AddEdit_Trans(ArrayList pArrayData, CIS_DataGridViewEx.DataGridViewEx fgDtls, bool PopulateDtls, ref double dblTransID, string str1, string str2, string sEntryNo, string sOtherNo = "", string sOtherName = "", int iVoucherTypeID = 0, params CIS_DataGridViewEx.DataGridViewEx[] args)
    {
        return Transcation_AddEdit_Validate_Trans(pArrayData, fgDtls, PopulateDtls, ref dblTransID, str1, str2, sEntryNo, sOtherNo, sOtherName, iVoucherTypeID, args);
    }

    public static object Transcation_AddEdit(ArrayList pArrayData, CIS_DataGridViewEx.DataGridViewEx fgDtls, bool PopulateDtls, string str1, string str2, params CIS_DataGridViewEx.DataGridViewEx[] args)
    {
        try
        {
            object frm = Navigate.GetActiveChild();
            dynamic objfrm = frm;
            string strQuery = string.Empty;
            string strDtls = string.Empty;
            double dblCode = 0;

            if (!rtnAction())
                dblCode = Conversions.ToDouble(((DataTable)objfrm.ds).Rows[Conversions.ToInteger(objfrm.RecordNo)][0].ToString());

            strQuery = GetDB_Column(Conversions.ToString(objfrm.strTableName), rtnAction(), Conversions.ToString(dblCode), pArrayData, true, false);
            if (PopulateDtls)
                strDtls = Conversions.ToString(GetDB_Details(RuntimeHelpers.GetObjectValue(objfrm), fgDtls, fgDtls.Grid_Tbl, ((DataTable)objfrm.ds).Columns[0].ColumnName, Conversions.ToString(dblCode), (DataTable)objfrm.dt_HasDtls_Grd, (DataTable)objfrm.dt_AryIsRequired));

            if ((args != null))
            {
                if (args.Length >= 0)
                {
                    for (int i = 0; i <= Information.UBound(args, 1); i++)
                    {
                        CIS_DataGridViewEx.DataGridViewEx fgDtls_f = (CIS_DataGridViewEx.DataGridViewEx)args[i];
                        if ((fgDtls_f != null))
                            strDtls = Conversions.ToString(Operators.ConcatenateObject(strDtls, GetDB_Details(RuntimeHelpers.GetObjectValue(objfrm), fgDtls_f, fgDtls_f.Grid_Tbl, ((DataTable)objfrm.ds).Columns[0].ColumnName, Conversions.ToString(dblCode), (DataTable)objfrm.dt_HasDtls_Grd, (DataTable)objfrm.dt_AryIsRequired)));
                    }
                }
            }

            if (str1.Trim().Length != 0)
                strDtls += str1;

            if (strDtls.Trim().Length != 0)
                DB.ExecuteSQL_InsertUpdate(strQuery, rtnAction(), strDtls);
            return 0;

        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return 1;
        }
    }

    public static object Transcation_AddEdit_Validate(ArrayList pArrayData, CIS_DataGridViewEx.DataGridViewEx fgDtls, bool PopulateDtls, string str1, string str2, string sEntryNo, string sOtherNo = "", string sOtherName = "", params CIS_DataGridViewEx.DataGridViewEx[] args)
    {
        string sRetVal = "";
        try
        {
            object frm = Navigate.GetActiveChild();
            dynamic objfrm = frm;
            string strQuery = string.Empty;
            string strDtls = string.Empty;
            double dblCode = 0;

            if (!rtnAction())
                dblCode = Conversions.ToDouble(((DataTable)objfrm.ds).Rows[Conversions.ToInteger(objfrm.RecordNo)][0].ToString());

            strQuery = GetDB_Column(Conversions.ToString(NewLateBinding.LateGet(objfrm, null, "strTableName", new object[0], null, null, null)), rtnAction(), Conversions.ToString(dblCode), pArrayData, true, false);
            if (PopulateDtls)
                strDtls = Conversions.ToString(GetDB_Details(RuntimeHelpers.GetObjectValue(objfrm), fgDtls, fgDtls.Grid_Tbl, ((DataTable)objfrm.ds).Columns[0].ColumnName, Conversions.ToString(dblCode), (DataTable)objfrm.dt_HasDtls_Grd, (DataTable)objfrm.dt_AryIsRequired));

            if ((args != null))
            {
                if (args.Length >= 0)
                {
                    for (int i = 0; i <= Information.UBound(args, 1); i++)
                    {
                        CIS_DataGridViewEx.DataGridViewEx fgDtls_f = (CIS_DataGridViewEx.DataGridViewEx)args[i];
                        if ((fgDtls_f != null))
                            strDtls = Conversions.ToString(Operators.ConcatenateObject(strDtls, GetDB_Details(RuntimeHelpers.GetObjectValue(objfrm), fgDtls_f, fgDtls_f.Grid_Tbl, ((DataTable)objfrm.ds).Columns[0].ColumnName, Conversions.ToString(dblCode), (DataTable)objfrm.dt_HasDtls_Grd, (DataTable)objfrm.dt_AryIsRequired)));
                    }
                }
            }

            if (str1.Trim().Length != 0)
                strDtls += str1;

            if (strDtls.Trim().Length != 0)
            {
                sRetVal = DB.ExecuteSQL_InsertUpdate(strQuery, rtnAction(), strDtls, sEntryNo, sOtherNo, sOtherName);
                if ((sRetVal != "ERROR") && (sRetVal != ""))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecuritySuccess, sRetVal, "Record Added Successfully..");
                    GetActionType();
                    DBSp.Log_CurrentUser(Localization.ParseNativeInt(Conversions.ToString(NewLateBinding.LateGet(instance, null, "iIDentity", new object[0], null, null, null))), iactionType, 0, 0, 0, 0);
                }
            }
            return 0;
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return 1;
        }
    }

    public static object Transcation_AddEdit_Validate_Trans(ArrayList pArrayData, CIS_DataGridViewEx.DataGridViewEx fgDtls, bool PopulateDtls, ref double dblTransID, string str1, string str2, string sEntryNo, string sOtherNo = "", string sOtherName = "", int iVoucherTypeID = 0, params CIS_DataGridViewEx.DataGridViewEx[] args)
    {
        string sRetVal = "";
        try
        {
            object frm = Navigate.GetActiveChild();
            dynamic objfrm = frm;
            string strQuery = string.Empty;
            string strDtls = string.Empty;
            double dblCode = 0;

            if (!rtnAction())
                dblCode = Conversions.ToDouble(((DataTable)objfrm.ds).Rows[Conversions.ToInteger(objfrm.RecordNo)][0].ToString());

            strQuery = GetDB_Column(Conversions.ToString(NewLateBinding.LateGet(objfrm, null, "strTableName", new object[0], null, null, null)), rtnAction(), Conversions.ToString(dblCode), pArrayData, true, false);
            if (PopulateDtls)
                strDtls = Conversions.ToString(GetDB_Details(RuntimeHelpers.GetObjectValue(objfrm), fgDtls, fgDtls.Grid_Tbl, ((DataTable)objfrm.ds).Columns[0].ColumnName, Conversions.ToString(dblCode), (DataTable)objfrm.dt_HasDtls_Grd, (DataTable)objfrm.dt_AryIsRequired));

            if ((args != null))
            {
                if (args.Length >= 0)
                {
                    for (int i = 0; i <= Information.UBound(args, 1); i++)
                    {
                        CIS_DataGridViewEx.DataGridViewEx fgDtls_f = (CIS_DataGridViewEx.DataGridViewEx)args[i];
                        if ((fgDtls_f != null))
                            strDtls = Conversions.ToString(Operators.ConcatenateObject(strDtls, GetDB_Details(RuntimeHelpers.GetObjectValue(objfrm), fgDtls_f, fgDtls_f.Grid_Tbl, ((DataTable)objfrm.ds).Columns[0].ColumnName, Conversions.ToString(dblCode), (DataTable)objfrm.dt_HasDtls_Grd, (DataTable)objfrm.dt_AryIsRequired)));
                    }
                }
            }

            if (str1.Trim().Length != 0)
                strDtls += str1;

            if (strDtls.Trim().Length != 0)
            {
                sRetVal = DB.ExecuteSQL_InsertUpdate_Trans(strQuery, ref dblTransID, rtnAction(), strDtls, sEntryNo, sOtherNo, sOtherName, iVoucherTypeID);
                if ((sRetVal != "ERROR") && (sRetVal != ""))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecuritySuccess, sRetVal, "Record Added Successfully..");
                    GetActionType();
                    DBSp.Log_CurrentUser(Localization.ParseNativeInt(Conversions.ToString(NewLateBinding.LateGet(instance, null, "iIDentity", new object[0], null, null, null))), iactionType, 0, 0, 0, 0);
                }
            }
            return 0;

        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return 1;
        }
    }

    public static void Log_CurrentUser(int ModuleID, int TypeofAction, int iReportID, int isCrystalReport, int IsBarcode, int isChequePrint)
    {
        try
        {
            string format = "Insert Into {0} (MenuID, ReportID, IsCrystalReport, IsBarCode, IsChequePrint, ActionType, UserID, UserDt,StoreID ,CompID,BranchID, YearID, IPAddress, MacAddress) Values({1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12},{13},{14})";
            DB.ExecuteSQL(string.Format(format, new object[] { "tbl_UserReportLog", ModuleID, iReportID, isCrystalReport, IsBarcode, isChequePrint, Localization.ParseNativeInt(Conversions.ToString((int)TypeofAction)), Db_Detials.UserID, "getDate()", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, DB.SQuote(CommonCls.GetIP()), DB.SQuote(CommonCls.FetchMacId()) }));
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }

    }

    #endregion

    #region "Ledger Posting"

    #region Accounts Ledger Posting

    public static string InsertInto_AcLedger(string TransID, string SubTransID, string EntryNo, string EntryDate, double TransType, string LedgerID, int DrCrID, Db_Detials.Ac_AdjType AdjType, string RefID, string RefNo, string RefDate, double RefTransType, decimal Dr_Amt, decimal Cr_Amt, string Narration, int StoreID, int CompID, int BranchID, int YearID, int CUID, DateTime CUDate)
    {
        try
        {
            string strQry = string.Format("Insert Into {0} (TransID, SubTransID, EntryNo, EntryDate, TransType, LedgerID, DrCrID, AdjType, RefID, RefNo, RefDate, RefTransType, Dr_Amount, Cr_Amount, Narration, StoreID, CompID, BranchID, YearID, AddedOn, AddedBy, IsDeleted, DeletedOn, Deletedby)", "tbl_AcLedger") + string.Format(" Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16},{17},{18},{19},{20},{21},{22},{23});" + Environment.NewLine, new object[] { 
                    TransID, SubTransID, EntryNo, DB.SQuote(Localization.ToSqlDateString(EntryDate)), 
                    Localization.ParseNativeInt(Conversions.ToString(TransType)), LedgerID, 
                    Localization.ParseNativeInt(Conversions.ToString(DrCrID)), 
                    Localization.ParseNativeInt(Conversions.ToString((int) AdjType)),DB.SQuote( RefID), DB.SQuote(RefNo), 
                    (RefDate!="__/__/____"?DB.SQuote(Localization.ToSqlDateString(RefDate)):"NULL"), Localization.ParseNativeInt(Conversions.ToString(RefTransType)), Dr_Amt, Cr_Amt, RuntimeHelpers.GetObjectValue(Interaction.IIf((Narration.Trim().Length == 0) | (Narration == "null"), "NULL", DB.SQuote(Narration.Trim()))), 
                    StoreID, CompID, BranchID, YearID,DB.SQuote(Localization.ToSqlDateString(Conversions.ToString(CUDate))), CUID, 0, 0, 0
            });
            return strQry;
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return string.Empty;

        }
    }

    public static string InsertInto_VatLedger(string TransID, string SubTransID, string EntryNo, string EntryDate, double TransType, string LedgerID, int AddLessTypeID, decimal Percentage, string RefID, decimal Dr_Amt, decimal Cr_Amt, string Narration, int CompID, int YearID, int CUID, DateTime CUDate)
    {
        try
        {
            string strQry = string.Format("Insert Into {0} (TransID, SubTransID, EntryNo, EntryDate, TransType, LedgerID, AddLessTypeID, Percentage, RefID, Dr_Amt, Cr_Amt, Narration, CompID, YearID, CUID, CUDate)", "tbl_VatLedger") + string.Format(" Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15});" + Environment.NewLine, new object[] { 
                    TransID, SubTransID, EntryNo, DB.SQuote(Localization.ToSqlDateString(EntryDate)), 
                    Localization.ParseNativeInt(Conversions.ToString(TransType)), LedgerID, 
                    Localization.ParseNativeInt(Conversions.ToString(AddLessTypeID)), Percentage, 
                   DB.SQuote(RefID), Dr_Amt, Cr_Amt, RuntimeHelpers.GetObjectValue(Interaction.IIf((Narration.Trim().Length == 0) | (Narration == "null"), "NULL", DB.SQuote(Narration.Trim()))), CompID, 
                    YearID, CUID, DB.SQuote(Localization.ToSqlDateString(Conversions.ToString(CUDate)))
                 });

            return strQry;
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return string.Empty;
        }
    }

    public static string InsertInto_TdsLedger(string TransID, string SubTransID, string EntryNo, string EntryDate, double TransType, string LedgerID, int AddLessTypeID, decimal Percentage, string RefID, decimal Dr_Amt, decimal Cr_Amt, string Narration, int CompID, int YearID, int CUID, DateTime CUDate)
    {
        try
        {
            string strQry = string.Format("Insert Into {0} (TransID, SubTransID, EntryNo, EntryDate, TransType, LedgerID, AddLessTypeID, Percentage, RefID, Dr_Amt, Cr_Amt, Narration, CompID, YearID, CUID, CUDate)", "tbl_TdsLedger") + string.Format(" Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15});" + Environment.NewLine, new object[] { 
                    TransID, SubTransID, EntryNo, DB.SQuote(Localization.ToSqlDateString(EntryDate)), 
                    Localization.ParseNativeInt(Conversions.ToString(TransType)), LedgerID, 
                    Localization.ParseNativeInt(Conversions.ToString(AddLessTypeID)), Percentage, 
                    DB.SQuote(RefID), Dr_Amt, Cr_Amt, RuntimeHelpers.GetObjectValue(Interaction.IIf((Narration.Trim().Length == 0) | (Narration == "null"), "NULL", DB.SQuote(Narration.Trim()))), CompID, 
                    YearID, CUID, DB.SQuote(Localization.ToSqlDateString(Conversions.ToString(CUDate)))
                 });

            return strQry;
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return string.Empty;
        }
    }

    public static string InsertIntoBrokerLedger(string TransID, string SubTransID, string EntryNo, double TransType, double BrokerID, string BatchNo, string BatchDate, double UnitID, decimal BrokerPercent, decimal Dr_Amt, decimal Cr_Amt, string NarratIon, int StoreID, int CompID, int BranchID, int YearID, int CUID, DateTime CUDate, double IsDeleted, int IsItemWise)
    {
        string str;
        try
        {
            str = string.Format("Insert Into {0} (TransID, SubTransID, EntryNo, TransType, BrokerID,BatchNo, BatchDate, UnitID, BrokerPercent, Dr_Amt, Cr_Amt,DescrIptIon, IsItemWise,StoreID, CompID, BranchID,YearID, AddedBy, CUserDt,IsDeleted)", "tbl_BrokerLedger") + string.Format(" Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8} ,{9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17},{18},{19});" + Environment.NewLine, new object[] { 
                    TransID, SubTransID, EntryNo, Localization.ParseNativeInt(Conversions.ToString(TransType)), BrokerID,DB.SQuote(BatchNo), DB.SQuote(Localization.ToSqlDateString(BatchDate)), Localization.ParseNativeInt(Conversions.ToString(UnitID)), BrokerPercent, Dr_Amt,Cr_Amt, 
                    RuntimeHelpers.GetObjectValue(Interaction.IIf((NarratIon.Trim().Length == 0) | (NarratIon == "null"), "null", DB.SQuote(NarratIon.Trim()))), IsItemWise, StoreID,CompID,BranchID ,YearID, CUID, DB.SQuote(Localization.ToSqlDateString(Conversions.ToString(CUDate))), Localization.ParseNativeInt(IsDeleted.ToString())});
        }
        catch (Exception exception1)
        {
            ProjectData.SetProjectError(exception1);
            Exception exception = exception1;
            Navigate.logError(exception.Message, exception.StackTrace);
            str = string.Empty;
            ProjectData.ClearProjectError();
            return str;
        }
        return str;
    }

    public static string InsertIntoTrasportLedger(string TransID, string VoucherNo, string VoucherDate, double TransType, double TransportID, double FromDepartID, double ToDepartID, string LrNo, string LrDate, string VehicleNo, double UnitID, int Packs, decimal Qty, int CompID, int YearID, int CUID, DateTime CUDate)
    {
        try
        {
            string Lr_Date = null;
            if (LrDate == null | LrDate == "__/__/____")
            {
                Lr_Date = "null";
            }
            else
            {
                Lr_Date = DB.SQuote(Localization.ToSqlDateString(LrDate));
            }
            string strQry = string.Format("Insert Into {0} (TransID, VoucherNo, VoucherDate, TransType, TransportID, FromLedgerID, ToLedgerID, LrNo, LrDate, VehicleNo, UnitID, Packs, Qty,CompID, YearID, AddedBy, CUDate)", "tbl_TransportLedger") + string.Format(" Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16});" + Environment.NewLine, new object[] { 
                    TransID, DB.SQuote(VoucherNo), DB.SQuote(Localization.ToSqlDateString(VoucherDate)), Localization.ParseNativeInt(Conversions.ToString(TransType)), Localization.ParseNativeInt(Conversions.ToString(TransportID)), Localization.ParseNativeInt(Conversions.ToString(FromDepartID)), Localization.ParseNativeInt(Conversions.ToString(ToDepartID)), RuntimeHelpers.GetObjectValue(Interaction.IIf(LrNo == null, "null", DB.SQuote(LrNo))), Lr_Date, RuntimeHelpers.GetObjectValue(Interaction.IIf(VehicleNo == null, "null", DB.SQuote(VehicleNo))), UnitID, Packs, Qty, CompID, YearID, CUID, 
                    DB.SQuote(Localization.ToSqlDateString(Conversions.ToString(CUDate)))});

            return strQry;

        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return string.Empty;

        }
    }

    public static string InsertIntoChequeLog(int ChequeCatalogID, string ChequeNo, bool IsUsed, bool IsPrinted, bool IsCancelled, bool IsReturned, bool IsStopped, int CompID, int YearID, int CUID, DateTime CUDate)
    {
        string str;
        try
        {
            str = string.Format("Insert Into {0} (ChequeCatalogID,ChequeNo,IsUsed,IsPrinted,IsCancelled,IsReturned,IsStopped,CompID,YearID,UserID,UserDt)", "tbl_ChequeLog") + string.Format(" Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9},{10});" + Environment.NewLine, new object[] 
            {
                ChequeCatalogID, CommonLogic.SQuote(ChequeNo),IsUsed==true?"1":"0",IsPrinted==true?"1":"0",IsCancelled==true?"1":"0",IsReturned==true?"1":"0",IsStopped==true?"1":"0",CompID, YearID, CUID, DB.SQuote(Localization.ToSqlDateString(Conversions.ToString(CUDate)))
            });
        }
        catch (Exception exception1)
        {
            ProjectData.SetProjectError(exception1);
            Exception exception = exception1;
            Navigate.logError(exception.Message, exception.StackTrace);
            str = string.Empty;
            ProjectData.ClearProjectError();
            return str;
        }
        return str;
    }

    #endregion

    #region Yarn Stock Ledger Posting

    public static string InsertIntoYarnOrderLedger(double TransType, string TransID, string SubTransID, string TransNo, string TransDate, double DepartmentID, string RefID, string MainRefID, int BatchID, string BatchNo, string BatchDate, double YarnID, double ColorID, double ShadeID, double UnitID, decimal Dr_Qty, decimal Dr_Cops, decimal Dr_Wt, decimal Cr_Qty, decimal Cr_Cops, decimal Cr_Wt, decimal StockValue, string Description, int ProductionOrdID, int EI1, int EI2, int EI3, string ED1, string ED2, string ET1, string ET2, string ET3, decimal EN1, decimal EN2, string UniqueID, int RowIndex, int StatusID, string OrderTransType, int RefVoucherID, int StoreID, int CompID, int YearID, int BranchID, int AddedBy, DateTime AddedOn)
    {
        try
        {
            string strQry = string.Format("Insert Into {0} (TransType,TransID,SubTransID,TransNo,TransDate,LedgerID,RefID,MainRefID,BatchID,BatchNo,BatchDate,YarnID,ColorID,ShadeID,UnitID,Dr_Bags,Dr_Cops,Dr_Wt,Cr_Bags,Cr_Cops,Cr_Wt,StockValue,Description,ProductionOrdID,EI1,EI2,EI3,ED1,ED2,ET1,ET2,ET3,EN1,EN2,UniqueID,RowIndex,StatusID,OrderTransType,RefVoucherID,StoreID,CompID,BranchID,YearID,AddedBy,AddedOn,IsDeleted,Deletedby,Deletedon)", "tbl_YarnOrderLedger") + string.Format(" Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19} ,{20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30}, {31}, {32}, {33}, {34}, {35}, {36}, {37}, {38}, {39}, {40}, {41}, {42}, {43}, {44}, 0, 0, 0);" + Environment.NewLine, new object[] { 
                    Localization.ParseNativeInt(Conversions.ToString(TransType)),TransID,SubTransID,TransNo,DB.SQuote(Localization.ToSqlDateString(TransDate)),
                    DepartmentID, CommonLogic.SQuote(Convert.ToString(RefID)), CommonLogic.SQuote(Convert.ToString(MainRefID)), BatchID,DB.SQuote(BatchNo),DB.SQuote(Localization.ToSqlDateString(BatchDate)),
                    Localization.ParseNativeInt(Conversions.ToString(YarnID)), Localization.ParseNativeInt(Conversions.ToString(ColorID)),
                    Localization.ParseNativeInt(Conversions.ToString(ShadeID)), Localization.ParseNativeInt(Conversions.ToString(UnitID)),
                    Dr_Qty, Dr_Cops, Dr_Wt, Cr_Qty, Cr_Cops, Cr_Wt, StockValue, RuntimeHelpers.GetObjectValue(Interaction.IIf((Description.Trim().Length == 0) | (Description == "null"), "null", DB.SQuote(Description.Trim()))),
                    ProductionOrdID, EI1, EI2, EI3, ED1 == "NULL" ? "0" : ED1 , ED2 == "NULL" ? "0" : ED2, DB.SQuote(ET1), DB.SQuote(ET2), DB.SQuote(ET3), EN1, EN2,
                    RuntimeHelpers.GetObjectValue(Interaction.IIf((UniqueID.Trim().Length == 0) | (UniqueID == "null"), "null", DB.SQuote(UniqueID.Trim()))),RowIndex,StatusID,CommonLogic.SQuote(OrderTransType),
                    RefVoucherID,StoreID,CompID,BranchID,YearID,AddedBy,DB.SQuote(Localization.ToSqlDateString(Conversions.ToString(AddedOn))), });
            return strQry;
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return string.Empty;
        }
    }

    public static string InsertIntoYarnStockLedger(double TransType, string TransID, string SubTransID, string TransNo, string TransDate, double DepartmentID, double SubDepartmentID, string RefID, string MainRefID, string BatchNo, string BoxNo, double YarnID, double ColorID, double ShadeID, double UnitID, decimal Dr_Qty, decimal Dr_Cops, decimal Dr_Wt, decimal Cr_Qty, decimal Cr_Cops, decimal Cr_Wt, decimal StockValue, string Description, int ProductionOrdID, int InwLedID, string InwTransID, int ProcessOrdID, int ProcessTypeID, int ProcessID, int EI1, int EI2, int EI3, string ED1, string ED2, string ET1, string ET2, string ET3, decimal EN1, decimal EN2, string UniqueID, int RowIndex, int StatusID, int StoreID, int CompID, int YearID, int BranchID, int AddedBy, DateTime AddedOn)
    {
        try
        {
            string strQry = string.Format("Insert Into {0} (TransType, TransID, SubTransID, TransNo, TransDate, DepartmentID, SubDepartmentID, RefID, MainRefID, BatchNo, BoxNo, YarnID, ColorID, ShadeID, UnitID, Dr_Qty, Dr_Cops, Dr_Wt, Cr_Qty, Cr_Cops, Cr_Wt, StockValue, Description, ProductionOrdID, InwLedID, InwTransID, ProcessOrdID, ProcessTypeID, ProcessID, EI1, EI2, EI3, ED1, ED2, ET1, ET2, ET3, EN1, EN2, UniqueID, RowIndex, StatusID, StoreID, CompID, BranchID, YearID, AddedBy, AddedOn, IsDeleted, Deletedby, Deletedon)", "tbl_StockYarnLedger") + string.Format(" Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19} , {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30}, {31}, {32}, {33}, {34}, {35}, {36}, {37}, {38}, {39}, {40}, {41}, {42}, {43}, {44}, {45}, {46}, {47}, 0, 0, 0);" + Environment.NewLine, new object[] { 
                    Localization.ParseNativeInt(Conversions.ToString(TransType)), TransID, SubTransID, TransNo,DB.SQuote(Localization.ToSqlDateString(TransDate)), DepartmentID,SubDepartmentID, 
                    DB.SQuote(RefID), DB.SQuote(MainRefID), DB.SQuote(BatchNo), RuntimeHelpers.GetObjectValue(Interaction.IIf((BoxNo.Trim().Length == 0) | (BoxNo == "null"), "null", DB.SQuote(BoxNo.Trim()))),
                    Localization.ParseNativeInt(Conversions.ToString(YarnID)), Localization.ParseNativeInt(Conversions.ToString(ColorID)), Localization.ParseNativeInt(Conversions.ToString(ShadeID)), Localization.ParseNativeInt(Conversions.ToString(UnitID)),
                    Dr_Qty, Dr_Cops, Dr_Wt, Cr_Qty, Cr_Cops, Cr_Wt, StockValue, 
                    RuntimeHelpers.GetObjectValue(Interaction.IIf((Description.Trim().Length == 0) | (Description == "null"), "null", DB.SQuote(Description.Trim()))), 
                    ProductionOrdID, InwLedID, (InwTransID == null ? 0 : (InwTransID.ToString() == "" ? 0 : Localization.ParseNativeInt(InwTransID))),
                    ProcessOrdID, ProcessTypeID, ProcessID, EI1,EI2,EI3,ED1 == "NULL" ? "0" : ED1 , ED2 == "NULL" ? "0" : ED2,DB.SQuote(ET1),DB.SQuote(ET2),DB.SQuote(ET3),EN1,EN2,
                    CommonLogic.SQuote(UniqueID), RowIndex, StatusID, StoreID, CompID, YearID, BranchID, AddedBy,
                    DB.SQuote(Localization.ToSqlDateString(Conversions.ToString(AddedOn)))});

            return strQry;
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return string.Empty;
        }
    }

    #endregion

    #region Beam Stock Ledger Posting

    public static string InsertIntoBeamStockLedger(double TransType, string TransID, string SubTransID, string EntryNo, double StoreLocationID, double LedgerID, string RefID, string MainRefID, int LoomID, string BatchNo, string BatchDate, double BeamDesignID, decimal Dr_Qty, decimal Dr_Mtrs, decimal Dr_Weight, decimal Cr_Qty, decimal Cr_Mtrs, decimal Cr_Weight, decimal StockValue, string Description, int ProductionOrdID, int InwLedID, string InwTransID, int ProcessOrdID, int ProcessTypeID, int ProcessID, string UniqueID, int RowIndex, int StatusID, int StoreID, int CompID, int YearID, int BranchID, int CUID, DateTime CUDate)
    {
        try
        {
            string strQry = string.Format("Insert Into {0} (TransType,TransID,SubTransID,EntryNo,StoreLocationID,LedgerID,RefId,MainRefID,LoomID,BatchNo,BatchDate,BeamDesignID,Dr_Qty,Dr_Mtrs,Dr_Weight,Cr_Qty,Cr_Mtrs,Cr_Weight,StockValue,Description,ProductionOrdID,InwLedID,InwTransID,ProcessOrdID,ProcessTypeID,ProcessID,UniqueID,RowIndex,StatusID,StoreID,CompID,YearID,BranchID,AddedBy,CUDate)", "tbl_StockBeamLedger") + string.Format(" Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19} , {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30}, {31}, {32}, {33}, {34});" + Environment.NewLine, new object[] { 
                    Localization.ParseNativeInt(Conversions.ToString(TransType)), TransID, SubTransID, EntryNo, StoreLocationID, LedgerID, DB.SQuote(RefID), 
                    DB.SQuote(MainRefID), LoomID,
                    DB.SQuote(BatchNo), DB.SQuote(Localization.ToSqlDateString(BatchDate)), Localization.ParseNativeInt(Conversions.ToString(BeamDesignID)), 
                    Dr_Qty, Dr_Mtrs, Dr_Weight, Cr_Qty, Cr_Mtrs, Cr_Weight, StockValue, 
                    RuntimeHelpers.GetObjectValue(Interaction.IIf((Description.Trim().Length == 0) | (Description == "null"), "null", DB.SQuote(Description.Trim()))), 
                    ProcessOrdID, InwLedID, (InwTransID == null ? 0 : (InwTransID.ToString() == "" ? 0 : Localization.ParseNativeInt(InwTransID))),
                    ProcessOrdID, ProcessTypeID, ProcessID, CommonLogic.SQuote(UniqueID), RowIndex, StatusID, StoreID, CompID, YearID, BranchID, CUID,
                    DB.SQuote(Localization.ToSqlDateString(Conversions.ToString(CUDate)))});

            return strQry;
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return string.Empty;
        }
    }

    #endregion

    #region "Fabric Stock Ledger Posting"

    public static string InsertIntoFabricOrderLedger(double TransType, string TransID, string SubTransID, string TransNo, string TransDate, double LedgerID, string RefID, string MainRefID, string BatchID, string BatchNo, double FabricID, double QualityID, double DesignID, double ShadeID, double UnitID, decimal Size, decimal Dr_Qty, decimal Dr_Mtrs, decimal Dr_WeIght, decimal Cr_Qty, decimal Cr_Mtrs, decimal Cr_WeIght, decimal Rate, string NarratIon, int ProductionOrderID, int EI1, int EI2, int EI3, string ED1, string ED2, string ET1, string ET2, string ET3, decimal EN1, decimal EN2, string UniqueID, int RowIndex, int StatusID, string OrderTransType, int RefVoucherID, int StoreID, int CompID, int YearID, int BranchID, int CUID, DateTime CUDate)
    {
        try
        {
            string strQry = string.Format("Insert Into {0} (TransType,TransID,SubTransID,TransNo,TransDate,LedgerID,RefID,MainRefID,BatchID,BatchNo,FabricID,QualityID,DesignID,ShadeID,UnitID,Size,Dr_Qty,Dr_Mtrs,Dr_Wt,Cr_Qty,Cr_Mtrs,Cr_Wt,Rate,Description,ProductionOrdID,EI1,EI2,EI3,ED1,ED2,ET1,ET2,ET3,EN1,EN2,UniqueID,RowIndex,StatusID,OrderTransType,RefVoucherID,StoreID,CompID,BranchID,YearID,AddedOn,AddedBy,IsDeleted,Deletedon,Deletedby)", "tbl_FabricOrderLedger") + string.Format(" Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19} ,{20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30}, {31}, {32}, {33}, {34}, {35}, {36}, {37}, {38}, {39}, {40}, {41}, {42}, {43}, {44}, {45}, 0, 0, 0);" + Environment.NewLine, new object[] { 
                    Localization.ParseNativeInt(Conversions.ToString(TransType)),TransID,SubTransID,TransNo,DB.SQuote(Localization.ToSqlDateString(TransDate)),LedgerID,CommonLogic.SQuote(Convert.ToString(RefID)),CommonLogic.SQuote(Convert.ToString(MainRefID)),DB.SQuote(BatchID),DB.SQuote(BatchNo),Localization.ParseNativeInt(Conversions.ToString(FabricID)),Localization.ParseNativeInt(Conversions.ToString(QualityID)),
                    Localization.ParseNativeInt(Conversions.ToString(DesignID)),Localization.ParseNativeInt(Conversions.ToString(ShadeID)),Localization.ParseNativeInt(Conversions.ToString(UnitID)),Size,Dr_Qty,Dr_Mtrs,
                    Dr_WeIght,Cr_Qty, Cr_Mtrs,Cr_WeIght, Rate,RuntimeHelpers.GetObjectValue(Interaction.IIf((NarratIon.Trim().Length == 0) | (NarratIon == "null"), "null", DB.SQuote(NarratIon.Trim()))),
                    ProductionOrderID,EI1,EI2,EI3,ED1 == "NULL" ? "0" : ED1 , ED2 == "NULL" ? "0" : ED2,DB.SQuote(ET1),DB.SQuote(ET2),DB.SQuote(ET3),EN1,EN2,
                    RuntimeHelpers.GetObjectValue(Interaction.IIf((UniqueID.Trim().Length == 0) | (UniqueID == "null"), "null", DB.SQuote(UniqueID.Trim()))),RowIndex,StatusID,CommonLogic.SQuote(OrderTransType),
                    RefVoucherID,StoreID,CompID,BranchID,YearID,DB.SQuote(Localization.ToSqlDateString(Conversions.ToString(CUDate))),CUID});
            return strQry;
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return string.Empty;

        }
    }

    public static string InsertIntoFabrIcStockLedger(double TransType, string TransID, string SubTransID, string TransNo, string TransDate, double DepartmentID, int SubDepartmentID, string RefId, string MainRefID, string BatchNo, string BarCodeNo, int FabricID, double FabrIcQualItyID, double FabrIcDesIgnID, double FabrIcShadeID, int GradeID, double UnitID, decimal Dr_Qty, decimal Dr_Mtrs, decimal Dr_WeIght, decimal Cr_Qty, decimal Cr_Mtrs, decimal Cr_WeIght, decimal StockValue, string Description, int ProductionOrdID, int InwdLedgerID, string InwdTransID, int ProcessOrdID, int ProcessTypeID, int ProcessID, int EI1, int EI2, int EI3, string ED1, string ED2, string ET1, string ET2, string ET3, decimal EN1, decimal EN2, string UniqueID, int RowIndex, int StatusID, int StoreID, int CompID, int BranchID, int YearID, int CUID, DateTime CUDate)
    {
        string str;
        try
        {
            str = string.Format("Insert Into {0} (TransType,TransID,SubTransID,TransNo,TransDate,DepartmentID,SubDepartmentID,RefId,MainRefID,BatchNo,BarCodeNo,FabricID,FabricQualityID,FabricDesignID,FabricShadeID,GradeID,UnitID,Dr_Qty,Dr_Mtrs,Dr_Wt,Cr_Qty,Cr_Mtrs,Cr_Wt,StockValue,Description,ProductionOrdID,InwLedID,InwTransID,ProcessOrdID,ProcessTypeID,ProcessID,EI1,EI2,EI3,ED1,ED2,ET1,ET2,ET3,EN1,EN2,UniqueID,RowIndex,StatusID,StoreID,CompID,BranchID,YearID,AddedBy,AddedOn,IsDeleted,DeletedOn,DeletedBy)", "tbl_StockFabricLedger") + string.Format(" Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30}, {31}, {32}, {33}, {34}, {35}, {36}, {37}, {38}, {39}, {40}, {41}, {42}, {43}, {44}, {45}, {46}, {47}, {48}, {49}, 0, 0, 0);" + Environment.NewLine, new object[] {
                    Localization.ParseNativeInt(Conversions.ToString(TransType)),TransID, SubTransID, TransNo, DB.SQuote(Localization.ToSqlDateString(TransDate)), DepartmentID, SubDepartmentID ,DB.SQuote(Convert.ToString(RefId)),CommonLogic.SQuote(Convert.ToString(MainRefID)), (BatchNo=="0"||BatchNo==""||BatchNo==" "?"'-'": DB.SQuote(BatchNo)), 
                    DB.SQuote(Convert.ToString(BarCodeNo)),Localization.ParseNativeInt(Conversions.ToString(FabricID)), Localization.ParseNativeInt(Conversions.ToString(FabrIcQualItyID)),
                    Localization.ParseNativeInt(Conversions.ToString(FabrIcDesIgnID)), Localization.ParseNativeInt(Conversions.ToString(FabrIcShadeID)),Localization.ParseNativeInt(Conversions.ToString(GradeID)), 
                    Localization.ParseNativeInt(Conversions.ToString(UnitID)), Dr_Qty, Dr_Mtrs, Dr_WeIght, Cr_Qty,Cr_Mtrs, Cr_WeIght,StockValue, 
                    RuntimeHelpers.GetObjectValue(Interaction.IIf((Description.Trim().Length == 0) | (Description== "null"), "null", DB.SQuote(Description.Trim()))), Localization.ParseNativeInt(Conversions.ToString(ProductionOrdID)),
                    Localization.ParseNativeInt(Conversions.ToString(InwdLedgerID)),Localization.ParseNativeInt(Conversions.ToString(InwdTransID)),Localization.ParseNativeInt(Conversions.ToString(ProcessOrdID)),Localization.ParseNativeInt(Conversions.ToString(ProcessTypeID)),
                    Localization.ParseNativeInt(Conversions.ToString(ProcessID)),EI1,EI2,EI3,ED1 == "NULL" ? "0" : ED1 , ED2 == "NULL" ? "0" : ED2,DB.SQuote(ET1),DB.SQuote(ET2),DB.SQuote(ET3),Localization.ParseNativeDecimal(Conversions.ToString(EN1)), Localization.ParseNativeDecimal(Conversions.ToString(EN2)),CommonLogic.SQuote(UniqueID),RowIndex,StatusID,StoreID,CompID,BranchID,YearID,CUID,
                    DB.SQuote(Localization.ToSqlDateString(Conversions.ToString(CUDate)))
                 });
        }
        catch (Exception exception1)
        {
            ProjectData.SetProjectError(exception1);
            Exception exception = exception1;
            Navigate.logError(exception.Message, exception.StackTrace);
            str = string.Empty;
            ProjectData.ClearProjectError();
            return str;
        }
        return str;
    }

    #endregion

    #region "Catalog Stock Ledger Posting"

    public static string InsertIntoCatalogOrderLedger(double TransType, string TransID, string SubTransID, string TransNo, string TransDate, double LedgerID, string RefID, string MainRefID, string BatchID, string BatchNo, double CatalogID, double UnitID, decimal Dr_Pcs, decimal Cr_Pcs, decimal Rate, string NarratIon, int ProductionOrderID, int EI1, int EI2, int EI3, string ED1, string ED2, string ET1, string ET2, string ET3, decimal EN1, decimal EN2, string UniqueID, int RowIndex, int StatusID, string OrderTransType, int RefVoucherID, int StoreID, int CompID, int YearID, int BranchID, int CUID, DateTime CUDate)
    {
        try
        {
            string strQry = string.Format("Insert Into {0} (TransType,TransID,SubTransID,TransNo,TransDate,LedgerID,RefID,MainRefID,BatchID,BatchNo,CatalogID,UnitID,Dr_Pcs,Cr_Pcs,Rate,Description,ProductionOrdID,EI1,EI2,EI3,ED1,ED2,ET1,ET2,ET3,EN1,EN2,UniqueID,RowIndex,StatusID,OrderTransType,RefVoucherID,StoreID,CompID,BranchID,YearID,AddedOn,AddedBy,IsDeleted,Deletedon,Deletedby)", "tbl_CatalogOrderLedger") + string.Format(" Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19} ,{20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30}, {31}, {32}, {33}, {34}, {35}, {36}, {37}, 0, 0, 0);" + Environment.NewLine, new object[] { 
                    Localization.ParseNativeInt(Conversions.ToString(TransType)),TransID,SubTransID,TransNo,DB.SQuote(Localization.ToSqlDateString(TransDate)),LedgerID,CommonLogic.SQuote(Convert.ToString(RefID)),CommonLogic.SQuote(Convert.ToString(MainRefID)),DB.SQuote(BatchID),DB.SQuote(BatchNo),Localization.ParseNativeInt(Conversions.ToString(CatalogID)), Localization.ParseNativeInt(Conversions.ToString(UnitID)),Dr_Pcs,Cr_Pcs,
                    Rate,RuntimeHelpers.GetObjectValue(Interaction.IIf((NarratIon.Trim().Length == 0) | (NarratIon == "null"), "null", DB.SQuote(NarratIon.Trim()))),
                    ProductionOrderID,EI1,EI2,EI3,ED1 == "NULL" ? "0" : ED1 , ED2 == "NULL" ? "0" : ED2,DB.SQuote(ET1),DB.SQuote(ET2),DB.SQuote(ET3),EN1,EN2,
                    RuntimeHelpers.GetObjectValue(Interaction.IIf((UniqueID.Trim().Length == 0) | (UniqueID == "null"), "null", DB.SQuote(UniqueID.Trim()))),RowIndex,StatusID,CommonLogic.SQuote(OrderTransType),
                    RefVoucherID,StoreID,CompID,BranchID,YearID,DB.SQuote(Localization.ToSqlDateString(Conversions.ToString(CUDate))),CUID});
            return strQry;
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return string.Empty;

        }
    }

    public static string InsertIntoCatalogStockLedger(double TransType, string TransID, string SubTransID, string TransNo, string TransDate, double DepartmentID, int SubDepartmentID, string RefId, string MainRefID, string BatchNo, string BarCodeNo, double CatalogID, double UnitID, decimal Dr_Qty, decimal Cr_Qty, decimal StockValue, string Description, int EI1, int EI2, int EI3, string ED1, string ED2, string ET1, string ET2, string ET3, decimal EN1, decimal EN2, string UniqueID, int RowIndex, int StatusID, int StoreID, int CompID, int BranchID, int YearID, int AddedBy, DateTime AddedOn)
    {
        string str;
        try
        {
            str = string.Format("Insert Into {0} (TransType,TransID,SubTransID,TransNo,TransDate,DepartmentID,SubDepartmentID,RefID,MainRefID,BatchNo,BarCodeNo,CatalogID,UnitID,Dr_Qty,Cr_Qty,StockValue,Description,EI1,EI2,EI3,ED1,ED2,ET1,ET2,ET3,EN1,EN2,UniqueID, RowIndex,StatusID,StoreID,CompID,BranchID,YearID,AddedBy,AddedOn,IsDeleted,DeletedOn,DeletedBy)", "tbl_StockCatalogLedger") + string.Format(" Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30}, {31}, {32}, {33}, {34}, {35}, 0, 0, 0);" + Environment.NewLine, new object[] { 
                    Localization.ParseNativeInt(Conversions.ToString(TransType)),TransID, SubTransID, TransNo, DB.SQuote(Localization.ToSqlDateString(TransDate)), DepartmentID, SubDepartmentID ,DB.SQuote(Convert.ToString(RefId)),CommonLogic.SQuote(Convert.ToString(MainRefID)), (BatchNo=="0"||BatchNo==""||BatchNo==" "?"'-'": DB.SQuote(BatchNo)), 
                    DB.SQuote(Convert.ToString(BarCodeNo)),Localization.ParseNativeInt(Conversions.ToString(CatalogID)), Localization.ParseNativeInt(Conversions.ToString(UnitID)), Dr_Qty, Cr_Qty, StockValue, 
                    RuntimeHelpers.GetObjectValue(Interaction.IIf((Description.Trim().Length == 0) | (Description== "null"), "null", DB.SQuote(Description.Trim()))),
                    EI1,EI2,EI3,ED1 == "NULL" ? "0" : ED1 , ED2 == "NULL" ? "0" : ED2,DB.SQuote(ET1),DB.SQuote(ET2),DB.SQuote(ET3),Localization.ParseNativeDecimal(Conversions.ToString(EN1)), Localization.ParseNativeDecimal(Conversions.ToString(EN2)),DB.SQuote(UniqueID.Trim()),RowIndex,StatusID,StoreID,CompID,BranchID,YearID,AddedBy,
                    DB.SQuote(Localization.ToSqlDateString(Conversions.ToString(AddedOn)))
            });
        }
        catch (Exception exception1)
        {
            ProjectData.SetProjectError(exception1);
            Exception exception = exception1;
            Navigate.logError(exception.Message, exception.StackTrace);
            str = string.Empty;
            ProjectData.ClearProjectError();
            return str;
        }
        return str;
    }

    #endregion

    #region Item Stock Ledger Posting

    public static string InsertIntoItemOrderLedger(string TransID, string SubTransID, string EntryNo, double TransType, double LedgerID, string RefID, string BatchID, string BatchNo, string BatchDate, double ItemID, double UnitID, decimal Dr_Qty, decimal Dr_WeIght, decimal Cr_Qty, decimal Cr_WeIght, string NarratIon, string UniqueID, int RowIndex, int StatusID, string OrderTransType, int RefVoucherID, decimal RateType, decimal Rate, int CompID, int YearID, int CUID, DateTime CUDate)
    {
        try
        {
            string strQry = string.Format("Insert Into {0} (TransID, SubTransID, EntryNo, TransType, LedgerID, RefID, BatchID ,BatchNo, BatchDate, ItemID,UnitID, Dr_Qty, Dr_WeIght, Cr_Qty, Cr_WeIght, DescrIptIon, UniqueID, RowIndex, StatusID, OrderTransType, RefVoucherID, RateType, Rate, CompID, YearID, AddedBy, CUDate, IsDeleted, DeletedOn, DeletedBy)", "tbl_ItemOrderLedger") + string.Format(" Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19} ,{20}, {21}, {22}, {23}, {24}, {25}, {26}, 0, 0, 0);" + Environment.NewLine, new object[] { 
                    TransID, SubTransID, EntryNo, Localization.ParseNativeInt(Conversions.ToString(TransType)), 
                    LedgerID, DB.SQuote(RefID), DB.SQuote(BatchID), DB.SQuote(BatchNo), DB.SQuote(Localization.ToSqlDateString(BatchDate)), 
                    Localization.ParseNativeInt(Conversions.ToString(ItemID)),Localization.ParseNativeInt(Conversions.ToString(UnitID)), Dr_Qty, Dr_WeIght, Cr_Qty, 
                    Cr_WeIght, RuntimeHelpers.GetObjectValue(Interaction.IIf((NarratIon.Trim().Length == 0) | (NarratIon == "null"), "null", DB.SQuote(NarratIon.Trim()))), RuntimeHelpers.GetObjectValue(Interaction.IIf((UniqueID.Trim().Length == 0) | (UniqueID == "null"), "null",
                    DB.SQuote(UniqueID.Trim()))), RowIndex, StatusID, CommonLogic.SQuote(OrderTransType), RefVoucherID, RateType, 
                    Rate, CompID, YearID, CUID, DB.SQuote(Localization.ToSqlDateString(Conversions.ToString(CUDate)))});
            return strQry;
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return string.Empty;

        }
    }

    public static string InsertIntoItemStockLedger(double TransType, string TransID, string SubTransID, string EntryNo, string EntryDate, double StoreLocationID, double LedgerID, string RefID, string MainRefID, string BatchNo, string BatchDate, double ItemID, double GradeID, double UnitID, decimal Dr_Qty, decimal Dr_Weight, decimal Dr_Rate, decimal Dr_Amt, decimal Cr_Qty, decimal Cr_Weight, decimal Cr_Rate, decimal Cr_Amt, decimal StockValue, string Description, string LotNo, int ProductionOrdID, string UniqueID, int RowIndex, int StatusID, int StoreID, int CompID, int YearID, int BranchID, int CUID, string CUDate)
    {
        try
        {
            return string.Format("Insert Into {0} (TransType, TransID, SubTransID, EntryNo, EntryDate, StoreLocationID, LedgerID, RefId, MainRefID, BatchNo, BatchDate, ItemId, GradeID, UnitID, Dr_Qty, Dr_Weight, Dr_Rate, Dr_Amt, Cr_Qty, Cr_Weight, Cr_Rate, Cr_Amt, StockValue, Description, LotNo, ProductionOrdID, UniqueID, RowIndex, StatusID, StoreID, CompID, YearID, BranchID, AddedBy, CUDate, IsDeleted)", "tbl_StockItemLedger") +
                string.Format("Values ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}, {17}, {18}, {19}, {20}, {21}, {22}, {23}, {24}, {25}, {26}, {27}, {28}, {29}, {30}, {31}, {32}, {33}, {34}, 0);" + Environment.NewLine,
                new object[] { Localization.ParseNativeInt(Conversions.ToString(TransType)), TransID, SubTransID, EntryNo, 
                        DB.SQuote(Localization.ToSqlDateString(Conversions.ToString(EntryDate))), StoreLocationID, 
                        LedgerID, DB.SQuote(RefID), DB.SQuote(RefID), CommonLogic.SQuote(BatchNo), DB.SQuote(Localization.ToSqlDateString(Conversions.ToString(BatchDate))),
                        Localization.ParseNativeInt(Conversions.ToString(ItemID)), Localization.ParseNativeInt(Conversions.ToString(GradeID)), Localization.ParseNativeInt(Conversions.ToString(UnitID)), 
                        Dr_Qty,Dr_Weight,Dr_Rate,Dr_Amt, Cr_Qty,Cr_Weight,Cr_Rate,Cr_Amt, StockValue,
                        RuntimeHelpers.GetObjectValue(Interaction.IIf((Description.Trim().Length == 0) | (Description == "null"), "null", DB.SQuote(Description.Trim()))),
                        CommonLogic.SQuote(LotNo), ProductionOrdID,
                        CommonLogic.SQuote(UniqueID), RowIndex, StatusID, 
                        StoreID, CompID, YearID, BranchID, CUID, DB.SQuote(Localization.ToSqlDateString(Conversions.ToString(CUDate))) });
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return string.Empty;
        }
    }

    #endregion

    #endregion

    #region " Collection of fields "

    public static object GetDB_Details(object objfrm, CIS_DataGridViewEx.DataGridViewEx fgDtls, string strTableDtls, string columnnm, string CodeID, DataTable HasDtls_Grd, DataTable AryIsRequired)
    {
        try
        {
            string ColumnNames = string.Empty;
            string ColumnValues = string.Empty;
            string strRtnQry = string.Empty;
            bool isValue = false;

            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                isValue = false;
                ColumnNames = string.Empty;
                ColumnValues = string.Empty;

                DataRow[] dtRow_HasDtls_Grd = HasDtls_Grd.Select("SubGridID = " + fgDtls.Grid_UID);

                for (int j = 1; j <= (dtRow_HasDtls_Grd.Length - 1); j++)
                {
                    string ColDataType = dtRow_HasDtls_Grd[j]["ColDataType"].ToString();
                    string ColFields = dtRow_HasDtls_Grd[j]["ColFields"].ToString();
                    if (ColFields != "-")
                    {
                        DataRow[] dtRow_AryIsRequired = AryIsRequired.Select("SubGridID = " + fgDtls.Grid_UID + " And ColIndex = " + j);
                        if (dtRow_AryIsRequired[0]["IsRequired"].ToString() == "True")
                        {
                            if (fgDtls.Rows[i].Cells[j].Value == null)
                            {
                                isValue = false;
                                break;  // TODO: might not be correct. Was : Exit For
                            }
                        }

                        if (j > 1 & !string.IsNullOrEmpty(ColumnNames))
                        {
                            ColumnNames += ", ";
                            ColumnValues += ", ";
                        }

                        ColumnNames += ColFields;

                        switch (ColDataType.ToUpper())
                        {
                            #region C
                            case "C":
                                if ((fgDtls.Rows[i].Cells[j].Value != null))
                                {
                                    if (!string.IsNullOrEmpty(fgDtls.Rows[i].Cells[j].Value.ToString()))
                                    {
                                        if (Conversion.Val(fgDtls.Rows[i].Cells[j].Value) != 0)
                                        {
                                            if ((Information.IsNumeric(fgDtls.Rows[i].Cells[j].Value)))
                                            {
                                                ColumnValues = ColumnValues + Localization.ParseNativeInt(fgDtls.Rows[i].Cells[j].Value.ToString());
                                                isValue = true;
                                            }
                                            else
                                            {
                                                ColumnValues = ColumnValues + CommonLogic.SQuote(fgDtls.Rows[i].Cells[j].Value.ToString());
                                                isValue = true;
                                            }
                                        }
                                        else
                                            ColumnValues = ColumnValues + 0;
                                    }
                                    else
                                        ColumnValues = ColumnValues + 0;
                                }
                                else
                                    ColumnValues = ColumnValues + 0;

                                break;
                            #endregion
                            #region I and D
                            case "I":
                            case "D":
                                if ((fgDtls.Rows[i].Cells[j].Value != null))
                                {
                                    if (!string.IsNullOrEmpty(fgDtls.Rows[i].Cells[j].Value.ToString()))
                                    {
                                        if (Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[j].Value.ToString()) != 0)
                                        {
                                            ColumnValues = ColumnValues + Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[j].Value.ToString());
                                            isValue = true;
                                        }
                                        else
                                        {
                                            ColumnValues = ColumnValues + 0;
                                        }
                                    }
                                    else
                                    {
                                        ColumnValues = ColumnValues + 0;
                                    }
                                }
                                else
                                {
                                    ColumnValues = ColumnValues + 0;
                                }

                                break;
                            #endregion
                            #region T
                            case "Z":
                            case "T":
                                if ((fgDtls.Rows[i].Cells[j].Value != null))
                                {
                                    if (Strings.Trim(fgDtls.Rows[i].Cells[j].Value.ToString()).Length != 0)
                                    {
                                        ColumnValues = ColumnValues + CommonLogic.SQuote(fgDtls.Rows[i].Cells[j].FormattedValue.ToString());
                                        isValue = true;
                                    }
                                    else
                                    {
                                        ColumnValues = ColumnValues + "null";
                                    }
                                }
                                else
                                {
                                    ColumnValues = ColumnValues + "null";
                                }

                                break;
                            #endregion
                            #region B
                            case "B":
                                if ((fgDtls.Rows[i].Cells[j].Value != null))
                                {
                                    DataGridViewCheckBoxCell chkStatus = (DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[j];
                                    ColumnValues = ColumnValues + (Localization.ParseBoolean(chkStatus.Value.ToString()) ? 1 : 0);
                                    isValue = true;
                                }
                                else
                                {
                                    ColumnValues = ColumnValues + "0";
                                }
                                break;

                            #endregion
                            #region S
                            case "S":
                                if ((fgDtls.Rows[i].Cells[j].Value != null))
                                {
                                    string strDate = fgDtls.Rows[i].Cells[j].Value.ToString();

                                    if (!string.IsNullOrEmpty(strDate))
                                    {
                                        ColumnValues = ColumnValues + CommonLogic.SQuote(Localization.ToSqlDateString(fgDtls.Rows[i].Cells[j].Value.ToString()));
                                        isValue = true;
                                    }
                                    else
                                    {
                                        ColumnValues = ColumnValues + "null";
                                    }
                                }
                                else
                                {
                                    ColumnValues = ColumnValues + "null";
                                }

                                break;
                            #endregion
                        }
                    }
                }
                if ((isValue == true) && (ColumnNames.ToString() != ""))
                    if (!string.IsNullOrEmpty(ColumnNames.ToString()))
                        strRtnQry += "Insert Into " + strTableDtls + " (" + columnnm + "," + ColumnNames.ToString() + ")" + " Values((#CodeID#)," + ColumnValues.ToString() + "); " + Environment.NewLine;
            }

            strRtnQry = string.Format("Delete From {0} Where {1} = (#CodeID#);" + Environment.NewLine, fgDtls.Grid_Tbl, columnnm) + strRtnQry;
            return strRtnQry;
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return "";
        }
    }

    private static string GetDB_Column(string strTable, bool pActionType = true, string iCode = "", ArrayList pArrayData = null, bool AddFirstFld = true, bool OnlyFlds = false)
    {
        try
        {
            DataColumnCollection columnsValue = DB.GetTable_FillSchema(string.Format("Select * From {0} Where 1=2", strTable));
            System.Text.StringBuilder commaSeparatedColumnNames = new System.Text.StringBuilder();
            string firstColumnnm = string.Empty;
            bool ActionType = pActionType;
            int StartFormat_ID = 0;
            string strAddValues = string.Empty;
            string strRtnQry = "";

            if (AddFirstFld == false)
                StartFormat_ID = 1;

            if (columnsValue == null)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Table not in database :: " + strTable);
                return "";
            }

            foreach (DataColumn column in columnsValue)
            {
                if (commaSeparatedColumnNames.ToString().Length != 0)
                {
                    if (!ActionType)
                    {
                        if ((column.ColumnName.ToUpper() != "ADDEDBY") && (column.ColumnName.ToUpper() != "ADDEDON"))
                            commaSeparatedColumnNames.Append(", ");

                    }
                    else
                    {
                        commaSeparatedColumnNames.Append(", ");
                    }
                }
                if (OnlyFlds)
                {
                    commaSeparatedColumnNames.Append("[" + column.ColumnName + "]");
                }
                else
                {
                    if (!AddFirstFld)
                    {
                        if (pActionType == false)
                        {
                            if (column.ColumnName.ToUpper() != "GUID")
                            {
                                switch (column.ColumnName.ToUpper())
                                {
                                    case "STOREID":
                                        commaSeparatedColumnNames.Append("[" + column.ColumnName + "]" + (pActionType == true ? "" : (column.DataType.Name.ToString() == "String" | column.DataType.Name.ToString() == "DateTime" ? " = '{" + (StartFormat_ID - 1) + "}'" : " = " + Db_Detials.StoreID)));
                                        break;

                                    case "COMPID":
                                        commaSeparatedColumnNames.Append("[" + column.ColumnName + "]" + (pActionType == true ? "" : (column.DataType.Name.ToString() == "String" | column.DataType.Name.ToString() == "DateTime" ? " = '{" + (StartFormat_ID - 1) + "}'" : " = " + Db_Detials.CompID)));
                                        break;

                                    case "INTCOMPID":
                                        commaSeparatedColumnNames.Append("[" + column.ColumnName + "]" + (pActionType == true ? "" : " = " + CommonLogic.SQuote(Db_Detials.IntCompID)));
                                        break;

                                    case "YEARID":
                                        commaSeparatedColumnNames.Append("[" + column.ColumnName + "]" + (pActionType == true ? "" : (column.DataType.Name.ToString() == "String" | column.DataType.Name.ToString() == "DateTime" ? " = '{" + (StartFormat_ID - 1) + "}'" : " = " + Db_Detials.YearID)));
                                        break;

                                    case "BRANCHID":
                                        commaSeparatedColumnNames.Append("[" + column.ColumnName + "]" + (pActionType == true ? "" : (column.DataType.Name.ToString() == "String" | column.DataType.Name.ToString() == "DateTime" ? " = '{" + (StartFormat_ID - 1) + "}'" : " = " + Db_Detials.BranchID)));
                                        break;

                                    case "MODIFIEDON":
                                        commaSeparatedColumnNames.Append("[" + column.ColumnName + "]" + (pActionType == true ? "" : (column.DataType.Name.ToString() == "String" ? " = '{" + (StartFormat_ID - 1) + "}'" : " = " + "Getdate()")));
                                        break;

                                    case "MODIFIEDBY":
                                        commaSeparatedColumnNames.Append("[" + column.ColumnName + "]" + (pActionType == true ? "" : (column.DataType.Name.ToString() == "String" | column.DataType.Name.ToString() == "DateTime" ? " = '{" + (StartFormat_ID - 1) + "}'" : " = " + Db_Detials.UserID)));
                                        break;

                                    case "ADDEDBY":
                                    case "ADDEDON":
                                        break;

                                    case "DELETEDON":
                                    case "CANCELLEDON":
                                    case "APPROVEDON":
                                    case "AUDITEDON":
                                        commaSeparatedColumnNames.Append("[" + column.ColumnName + "]" + (pActionType == true ? "" : (column.DataType.Name.ToString() == "String" ? " = '{" + (StartFormat_ID - 1) + "}'" : " = " + "NULL")));
                                        break;

                                    case "DELETEDBY":
                                    case "CANCELLEDBY":
                                    case "APPROVEDBY":
                                    case "AUDITEDBY":
                                        commaSeparatedColumnNames.Append("[" + column.ColumnName + "]" + (pActionType == true ? "" : (column.DataType.Name.ToString() == "String" ? " = '{" + (StartFormat_ID - 1) + "}'" : " = " + "NULL")));
                                        break;

                                    case "ISMODIFIED":
                                        commaSeparatedColumnNames.Append("[" + column.ColumnName + "]" + (pActionType == true ? "" : (column.DataType.Name.ToString() == "String" | column.DataType.Name.ToString() == "DateTime" ? " = '{" + (StartFormat_ID - 1) + "}'" : " = " + 1)));
                                        break;

                                    case "ISDELETED":
                                    case "ISCANCLLED":
                                    case "ISAPPROVED":
                                    case "ISAUDITED":
                                        commaSeparatedColumnNames.Append("[" + column.ColumnName + "]" + (pActionType == true ? "" : (column.DataType.Name.ToString() == "String" | column.DataType.Name.ToString() == "DateTime" ? " = '{" + (StartFormat_ID - 1) + "}'" : " = " + 0)));
                                        break;

                                    default:
                                        commaSeparatedColumnNames.Append("[" + column.ColumnName + "]" + (pActionType == true ? "" : (column.DataType.Name.ToString() == "String" | column.DataType.Name.ToString() == "DateTime" ? " = '{" + (StartFormat_ID - 1) + "}'" : " = {" + (StartFormat_ID - 1) + "}")));
                                        break;
                                }
                            }
                        }
                        else
                        {
                            if (column.ColumnName.ToUpper() != "GUID")
                            {
                                commaSeparatedColumnNames.Append("[" + column.ColumnName + "]" + (pActionType == true ? "" : (column.DataType.Name.ToString() == "String" | column.DataType.Name.ToString() == "DateTime" ? " = '{" + (StartFormat_ID - 1) + "}'" : " = {" + (StartFormat_ID - 1) + "}")));
                            }
                        }
                        if (column.DataType.Name.ToString() == "String" || column.DataType.Name.ToString() == "DateTime")
                        {
                            if ((column.ColumnName.ToUpper() != "INTCOMPID") && (column.ColumnName.ToUpper() != "ADDEDON") && (column.ColumnName.ToUpper() != "MODIFIEDON") && (column.ColumnName.ToUpper() != "DELETEDON") && (column.ColumnName.ToUpper() != "CANCELLEDON") && (column.ColumnName.ToUpper() != "APPROVEDON") && (column.ColumnName.ToUpper() != "AUDITEDON"))
                                strAddValues += " '{" + (StartFormat_ID - 1) + "}',";
                            else
                            {
                                if (ActionType)
                                {
                                    if (column.ColumnName.ToUpper() == "ADDEDON")
                                    {
                                        strAddValues += "GetDate()" + " ,";
                                        StartFormat_ID -= 1;
                                    }
                                    else if (column.ColumnName.ToUpper() == "INTCOMPID")
                                    {
                                        strAddValues += CommonLogic.SQuote(Db_Detials.IntCompID) + " ,";
                                        StartFormat_ID -= 1;
                                    }
                                    else
                                    {
                                        strAddValues += "NULL" + " ,";
                                        StartFormat_ID -= 1;
                                    }
                                }
                                else if (column.ColumnName.ToUpper() == "INTCOMPID")
                                {
                                    strAddValues += CommonLogic.SQuote(Db_Detials.IntCompID) + " ,";
                                    StartFormat_ID -= 1;
                                }
                            }
                        }
                        else
                        {
                            if (column.DataType.Name.ToUpper() != "GUID")
                            {
                                switch (column.ColumnName.ToUpper())
                                {
                                    case "STOREID":
                                        strAddValues += Db_Detials.StoreID + " ,";
                                        StartFormat_ID -= 1;
                                        break;

                                    case "COMPID":
                                        strAddValues += Db_Detials.CompID + " ,";
                                        StartFormat_ID -= 1;
                                        break;

                                    case "YEARID":
                                        strAddValues += Db_Detials.YearID + " ,";
                                        StartFormat_ID -= 1;
                                        break;

                                    case "BRANCHID":
                                        strAddValues += Db_Detials.BranchID + " ,";
                                        StartFormat_ID -= 1;
                                        break;

                                    case "ADDEDBY":
                                        if (ActionType)
                                        {
                                            strAddValues += Db_Detials.UserID + " ,";
                                            StartFormat_ID -= 1;
                                        }
                                        break;

                                    case "MODIFIEDON":
                                    case "MODIFIEDBY":
                                    case "DELETEDBY":
                                    case "CANCELLEDBY":
                                    case "APPROVEDBY":
                                    case "AUDITEDBY":
                                        strAddValues += "NULL" + " ,";
                                        StartFormat_ID -= 1;
                                        break;

                                    case "ISMODIFIED":
                                    case "ISDELETED":
                                    case "ISCANCLLED":
                                    case "ISAPPROVED":
                                    case "ISAUDITED":
                                        strAddValues += 0 + " ,";
                                        StartFormat_ID -= 1;
                                        break;

                                    default:
                                        strAddValues += " {" + (StartFormat_ID - 1) + "},";
                                        break;
                                }
                            }
                            else
                            {
                                StartFormat_ID -= 1;
                            }
                        }
                    }
                    else
                    {
                        firstColumnnm = " Where [" + column.ColumnName + "] = " + iCode;
                        AddFirstFld = false;
                    }
                    StartFormat_ID += 1;
                }
            }

            if (OnlyFlds)
            {
                return " (" + commaSeparatedColumnNames.ToString() + ")";
            }
            else
            {
                if (ActionType)
                {
                    if (strAddValues.Length != 0)
                        strAddValues = strAddValues.Substring(0, strAddValues.Length - 1);
                    strRtnQry = "Insert Into " + strTable + " (" + commaSeparatedColumnNames.ToString() + ") values(" + strAddValues + ");";
                }
                else
                {
                    strRtnQry = "Update " + strTable + " Set " + commaSeparatedColumnNames.ToString() + " " + firstColumnnm + ";";
                }
            }

            for (int i = 0; i <= (pArrayData.Count - 1); i++)
            {
                try
                {
                    string str = "";
                    try
                    {
                        str = pArrayData[i].ToString();
                    }
                    catch { str = ""; }

                    if (string.IsNullOrEmpty(str))
                    {
                        strRtnQry = strRtnQry.Replace("{" + i + "}", "null");
                    }
                    else
                    {
                        strRtnQry = strRtnQry.Replace("{" + i + "}", pArrayData[i].ToString());
                    }
                }
                catch
                {
                    strRtnQry = strRtnQry.Replace("{" + i + "}", "null");
                }
            }
            try
            {
                GetActionType();
                object instance1 = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                DBSp.Log_CurrentUser(Localization.ParseNativeInt(Conversions.ToString(NewLateBinding.LateGet(instance1, null, "iIDentity", new object[0], null, null, null))), iactionType, 0, 0, 0, 0);
            }
            catch { }
            return strRtnQry.Replace("'null'", "null").Replace("^", "''");
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return string.Empty;
        }
    }
    #endregion
}
