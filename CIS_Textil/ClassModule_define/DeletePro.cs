
using System;
using System.Data;
using System.Runtime.CompilerServices;
using CIS_CLibrary;
using CIS_DBLayer;
using CIS_Utilities;
using Microsoft.VisualBasic.CompilerServices;

public class DeletePro
{
    public static string strDeleteFromFabricLedger = "Delete From " + Db_Detials.tbl_StockFabricLedger + " Where (TransType = {1} And TransID = {0});";
    public static string strDeleteFromBeamLedger = "Delete From " + Db_Detials.tbl_StockBeamLedger + " Where (TransType = {1} And TransID = {0});";
    public static string strDeleteFromYarnLedger = "Delete From " + Db_Detials.tbl_StockYarnLedger + " Where (TransType = {1} And TransID = {0});";
    public static string strDeleteFromACLedger = "Delete From " + Db_Detials.tbl_AcLedger + " Where (TransType = {1} And TransID = {0});";
    public static string strDeleteFromBookLedger = "Delete From " + Db_Detials.tbl_StockCatalogLedger + " Where (TransType = {1} And TransID = {0});";
    public static string strDeleteFromYarnOrderLedger = "Delete From " + Db_Detials.tbl_YarnOrderLedger + " Where (TransType = {1} And TransID = {0});";
    public static string strDeleteFromFabricOrderLedger = "Delete From " + Db_Detials.tbl_FabricOrderLedger + " Where (TransType = {1} And TransID = {0});";

    public static void CheckAndDelete(ref object frm)
    {
        try
        {
            dynamic objfrm = frm;
            CIS_CLibrary.CIS_Textbox txtCode = (CIS_Textbox)objfrm.txtCode;
            string strPrimaryCol = ((DataTable)objfrm.ds).Columns[0].ColumnName;
            int iIDentity = Conversions.ToInteger(objfrm.iIDentity);
            string strTable = objfrm.strTableName;
            string RtnValue = "";
            string sUserType = "";

            try
            {
                //if (CIS_Dialog.Show("Do you want to delete this record?", /*MyProject.Application.Info.Title*/"", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No)
                {
                    if (objfrm._Delete_Rights == true)
                    {
                        sUserType = DB.GetSnglValue("Select UserType from " + strTable + " Where " + strPrimaryCol + "=" + txtCode.Text);
                        if (sUserType == "U" || sUserType == "")
                        {
                            RtnValue = DB.GetSnglValue("exec sp_DeletePro  " + iIDentity + "," + txtCode.Text + "," + CommonLogic.SQuote(strPrimaryCol) + "," + Db_Detials.UserID + "," + Db_Detials.CompID + "");
                            if (RtnValue == "0")
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecuritySuccess, "Success", App_Messages.msg_Delete_Succes);
                                if ((objfrm._IRecordCount - 1) != 0)
                                    if ((objfrm.RecordNo) != 0)
                                        objfrm.RecordNo -= 1;

                                object instance = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                                int iactionType = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster_tbl() Where MiscName='isDelete'"));
                                //if (Navigate.DeleteDialogResult == true)
                                {
                                    try
                                    {
                                        DBSp.Log_CurrentUser(Localization.ParseNativeInt(Conversions.ToString(NewLateBinding.LateGet(instance, null, "iIDentity", new object[0], null, null, null))), iactionType, 0, 0, 0, 0);
                                    }
                                    catch { }
                                }
                                int iVoucherMenuID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MenuID From tbl_MenuMaster Where FormCall='frmVoucherType'")));
                                int iVMenuID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select GenMenuID from tbl_VoucherTypeMaster Where VoucherTypeID=" + txtCode.Text + "")));
                                string strQry = string.Empty;
                                if (iVoucherMenuID == iIDentity)
                                {
                                    strQry = string.Format("Delete From tbl_VoucherNumberingMain Where MenuID=" + iVMenuID + ";");
                                    strQry += string.Format("Delete From tbl_MenuMaster Where MenuID=" + iVMenuID + ";");
                                    strQry += string.Format("Delete From tbl_DeletePro Where MenuID=" + iVMenuID + ";");
                                    strQry += string.Format("Delete From tbl_MenuMaster_Comp Where MenuID=" + iVMenuID + ";");
                                    strQry += string.Format("Delete From tbl_GridSettings Where GridID=" + iVMenuID + ";");
                                    strQry += string.Format("Delete From tbl_GridFields_Mapping Where GridID=" + iVMenuID + ";");
                                    strQry += string.Format("Delete From tbl_GridSettings_tbls Where GridID=" + iVMenuID + ";");
                                    strQry += string.Format("Delete From tbl_ReportList Where ModuleID=" + iVMenuID + ";");
                                    strQry += string.Format("Delete From tbl_ReportQuery Where MenuID=" + iVMenuID + ";");
                                }
                                if (strQry.Length > 0)
                                {
                                    DB.ExecuteSQL(strQry);
                                }
                                //if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(Microsoft.VisualBasic.CompilerServices.Operators.SubtractObject(NewLateBinding.LateGet(frm, null, "_IRecordCount", new object[0], null, null, null), 1), 0, false) && Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectNotEqual(NewLateBinding.LateGet(frm, null, "RecordNo", new object[0], null, null, null), 0, false))
                                //{
                                //    object instance = frm;
                                //    NewLateBinding.LateSet(instance, null, "RecordNo", new object[] { Microsoft.VisualBasic.CompilerServices.Operators.SubtractObject(NewLateBinding.LateGet(instance, null, "RecordNo", new object[0], null, null, null), 1) }, null, null);
                                //}
                                Navigate.SaveRecord(frm);
                            }
                            else if (RtnValue == "1")
                            {
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityWarning, "Security Warning", App_Messages.msg_Delete_Ref);
                            }
                        }
                        else
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityWarning, "Warning", "Cannot Delete Default Entries");
                        }
                    }
                    else
                    {
                        Navigate.ShowMessage(CIS_DialogIcon.SecurityWarning, "No Rights", "You Have No Rights To Delete Current Record.");
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Error", "Error occure while Editing record.");
            }
        }
        catch (Exception ex1)
        {
            Navigate.logError(ex1.Message, ex1.StackTrace);
        }
    }

    public static string GenrateDtls_DelQry(string strCommonQry_D, int iIDentity, double tCode, string PrimaryCol)
    {
        string strDelQry = string.Empty;
        try
        {
            using (IDataReader iDr = DB.GetRS(string.Format("Select TblName From tbl_GridSettings_tbls Where GridID = {0}", iIDentity)))
            {
                while (iDr.Read())
                {
                    strDelQry = strDelQry + string.Format(strCommonQry_D, iDr["TblName"].ToString(), tCode, PrimaryCol);
                }
            }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
        return strDelQry;
    }

    public static bool CheckCount(string strQuery)
    {
        try
        {
            return (Conversions.ToBoolean(Localization.ParseNativeDouble(DB.GetSnglValue(strQuery)) > 0 ? true : false));
        }
        catch
        {
            return true;
        }
    }

    public static bool CheckAndEdit(ref object frm)
    {
        try
        {
            dynamic frmObj = frm;
            CIS_CLibrary.CIS_Textbox txtCode = (CIS_Textbox)frmObj.txtCode;
            string ssQuery = string.Empty;
            int iIDentity = Conversions.ToInteger(frmObj.iIDentity);
            //string strCommonQry = "Select Count(0) From {0} Where {2} = {1}";
            //string strCommonQry_D = "Delete From {0} Where {1} = {2}; ";

            //If CheckRefInTbls(frm.strTableName.ToString(),Localization.ParseNativeInt(txtCode.Text)) = False Then GoTo lblRecfoundExit

            switch (iIDentity)
            {
                case 47:
                    //~~ Purchase Entry
                    if (CheckCount(string.Format("{0} {1}, {2}, {3}", Db_Detials.sp_BillAdjustment, Db_Detials.CompID, Db_Detials.YearID, Localization.ParseNativeInt(txtCode.Text))) == true)
                        goto lblRecfoundExit;

                    break;

                //case 151:
                //    //~~ Fabric Opening
                //    if (CheckCount(string.Format("Select Count(0) From {0} Where PieceNo in (Select BatchNo From tbl_StockFabricLedger Where Transtype <> 3) And FabOpID = " + Localization.ParseNativeInt(txtCode.Text), Db_Detials.tbl_FabricOpeningDtls)) == true)
                //        goto lblRecfoundExit;

                //    break;
            }

            return false;
        lblRecfoundExit:

            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecurityWarning, "Security Warning", App_Messages.msg_Edit_Ref);
            return true;
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Error", "Error occure while Editing record.");
            return true;
        }
    }
}
