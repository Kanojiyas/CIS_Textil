using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_DBLayer;
using CIS_CLibrary;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.Data.SqlClient;

public class CommonCls
{
    public static ConnectionInfo connectionInfo;
    public static CrystalReportViewer crViewer;
    public static ReportDocument objReport;

    public static void LoadMDIMenu()
    {
        CIS_Textil.MDIMain main = (CIS_Textil.MDIMain)Application.OpenForms["MDIMain"];
        LoadMenu menu = new LoadMenu();
        try
        {
            main.Menu = null;

            Form pForm = main;
            main = (CIS_Textil.MDIMain)pForm;

            if (main.mnuMain.Items.Count > 0)
                main.mnuMain = new MenuStrip();

            menu.LoadDynamicMenu(ref pForm, Db_Detials.UserType, Db_Detials.UserID, main.mnuMain, Db_Detials.CompID);
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
        //main.Menu = (MainMenu)menu.LoadDynamicMenu(ref pForm, Db_Detials.UserType);
        main.HashFunctions.Clear();
        Db_Detials.Hashref.Clear();
        main.HashFunctions = menu.HashFunctions;
        Db_Detials.Hashref = menu.HashRef;
    }

    public static void LoadMDIMenu_NoHash()
    {
        CIS_Textil.MDIMain main = (CIS_Textil.MDIMain)Application.OpenForms["MDIMain"];
        LoadMenu menu = new LoadMenu();
        main.Menu = null;
        Form pForm = main;
        main = (CIS_Textil.MDIMain)pForm;

        menu.LoadDynamicMenu_NoHash(ref pForm, Db_Detials.UserType, main.mnuVertical);
    }

    public static void IncFieldID(object frm, ref CIS_CLibrary.CIS_Textbox txt, string pColumn = "")
    {
        dynamic objfrm = frm;
        try
        {
            if (!string.IsNullOrEmpty(pColumn))
            {
                if (objfrm.frmVoucherTypeID != 0)
                {
                    txt.Text = DB.GetSnglValue(string.Format("Select Isnull(Max(Convert(int," + pColumn + ")),0) + 1 From {0} Where IsDeleted=0 and StoreID={1} and CompID = {2} and BranchID={3} and YearID = {4} and VoucherTypeID={5}", objfrm.strTableName, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, objfrm.frmVoucherTypeID));
                }
                else
                {
                    txt.Text = DB.GetSnglValue(string.Format("Select Isnull(Max(Convert(int," + pColumn + ")),0) + 1 From {0} Where IsDeleted=0 and StoreID = {1} and CompID = {2} and BranchID={3} and YearID={4}", objfrm.strTableName, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID));
                }
            }
            else
            {
                if (objfrm.frmVoucherTypeID != 0)
                {
                    txt.Text = DB.GetSnglValue(string.Format("Select Isnull(Max(EntryNo),0) + 1 From {0} Where IsDeleted=0 and StoreID={1} and CompID={2} and BranchID={3} and YearID= {4} and VoucherTypeID={5}", objfrm.strTableName, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, objfrm.frmVoucherTypeID));
                }
                else
                {
                    txt.Text = DB.GetSnglValue(string.Format("Select Isnull(Max(EntryNo),0) + 1 From {0} Where IsDeleted=0 and StoreID={1} and CompID={2} and BranchID={3} and YearID = {4}", objfrm.strTableName, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID));
                }
            }

        }
        catch (Exception ex)
        { Navigate.logError(ex.Message, ex.StackTrace); Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message); }
    }


    public static string AutoInc(object frm, string pColumn, string IDColumn, [Optional, DefaultParameterValue("")] string Condetion)
    {
        string str;
        dynamic objfrm = frm;
        int iIDentity = Conversions.ToInteger(objfrm.iIDentity);
        int iValVoucher = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(0) from tbl_VoucherNumberingMain Where MenuID=" + iIDentity + "")));

        string str3 = Conversions.ToString(Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select isnull(Max({0}),0) From {1} Where IsDeleted=0 and StoreID ={2} and CompID = {3} and BranchID={4} and YearID = {5} {6}", new object[] { IDColumn, RuntimeHelpers.GetObjectValue(objfrm.strTableName), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, RuntimeHelpers.GetObjectValue(Interaction.IIf(Condetion.ToString().Length > 0, "And " + Condetion, "")) }))));

        if (Conversions.ToDouble(str3) <= 0.0)
        {
            return "";
        }
        string snglValue = DB.GetSnglValue(string.Format("Select {0} From {1} Where IsDeleted=0 and {2} = {3} and StoreID={4} and CompID = {5} and BranchID={6} and YearID = {7}", new object[] { pColumn, RuntimeHelpers.GetObjectValue(objfrm.strTableName), IDColumn, str3, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID }));
        if (snglValue.Trim().Length <= 0)
        {
            return "";
        }
        try
        {
            int num = 0;
            string str6 = string.Empty;
            string str4 = string.Empty;
            string str5 = snglValue.Trim();
            if (Versioned.IsNumeric(str5.Substring(num, 1)))
            {
                int num4 = str5.Length - 1;
                for (num = 0; num <= num4; num++)
                {
                    if (!Versioned.IsNumeric(str5.Substring(num, 1)))
                    {
                        str4 = str5.Substring(num, str5.Length - num);
                        str5 = str5.Substring(0, num);
                        break;
                    }
                }
            }
            else
            {
                int num5 = str5.Length - 1;
                for (num = 0; num <= num5; num++)
                {
                    if (Versioned.IsNumeric(str5.Substring(num, 1)))
                    {
                        str6 = str5.Substring(0, num);
                        str5 = str5.Substring(num, str5.Length - num);
                        break;
                    }
                }
            }
            int num6 = str5.Length - 1;
            for (num = 0; num <= num6; num++)
            {
                if (!Versioned.IsNumeric(str5.Substring(num, 1)))
                {
                    str4 = str5.Substring(num, str5.Length - num);
                    str5 = str5.Substring(0, num);
                    break;
                }
            }
            int num2 = 0;
            int num7 = str5.Length - 1;
            for (num = 0; num <= num7; num++)
            {
                if (str5.Substring(num, 1) == "0")
                {
                    num2++;
                }
                else
                {
                    break;
                }
            }
            int length = str5.Length;
            if (str5.Length > 0)
            {
                str5 = Conversions.ToString((double)(Conversions.ToDouble(str5) + 1.0));
            }
            if (num2 > 0)
            {
                if (length < (str5.Length + num2))
                {
                    num2--;
                }
                int num8 = num2;
                for (num = 1; num <= num8; num++)
                {
                    str5 = "0" + str5;
                }
            }
            str = string.Format("{0}{1}{2}", str6, str5, str4);
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return "";
        }
        //if (iValVoucher > 0)
        //{
        //    //str
        //    using (IDataReader idr = DB.GetRS(string.Format("Select * from tbl_VoucherNumberingMain Where MenuId=" + iIDentity + "")))
        //    {
        //        while (idr.Read())
        //        {
        //            str = idr["Prefix"].ToString() + str + idr["Suffix"].ToString();
        //        }
        //    }

        //}
        return str;
    }

    //Public Shared Function AutoInc_Runtime(ByVal iString As String) As String

    //    If iString.Trim().Length <= 0 Then Return ""

    //    Try
    //        Dim strRtnValue As String = String.Empty
    //        Dim r_Alphabets As String = String.Empty
    //        Dim l_Alphabets As String = String.Empty
    //        Dim n_Char As String = iString.Trim()
    //        Dim i As Integer

    //        If IsNumeric(n_Char.Substring(i, 1)) Then
    //            For i = 0 To n_Char.Length - 1
    //                If Not IsNumeric(n_Char.Substring(i, 1)) Then
    //                    l_Alphabets = n_Char.Substring(i, n_Char.Length - i)
    //                    n_Char = n_Char.Substring(0, i)
    //                    Exit For
    //                End If
    //            Next
    //        Else
    //            For i = 0 To n_Char.Length - 1
    //                If IsNumeric(n_Char.Substring(i, 1)) Then
    //                    r_Alphabets = n_Char.Substring(0, i)
    //                    n_Char = n_Char.Substring(i, n_Char.Length - i)
    //                    Exit For
    //                End If
    //            Next
    //        End If

    //        For i = 0 To n_Char.Length - 1
    //            If Not IsNumeric(n_Char.Substring(i, 1)) Then
    //                l_Alphabets = n_Char.Substring(i, n_Char.Length - i)
    //                n_Char = n_Char.Substring(0, i)
    //                Exit For
    //            End If
    //        Next

    //        Dim iZeroCnt As Integer = 0
    //        For i = 0 To n_Char.Length - 1
    //            If n_Char.Substring(i, 1) = "0" Then
    //                iZeroCnt += 1
    //            Else
    //                Exit For
    //            End If
    //        Next
    //        ' Coded By Raj
    //        ' Start 
    //        Dim NLen As Integer = n_Char.Length
    //        If n_Char.Length > 0 Then n_Char = n_Char + 1
    //        If iZeroCnt > 0 Then
    //            If NLen < n_Char.Length + iZeroCnt Then
    //                iZeroCnt = iZeroCnt - 1
    //            End If
    //            For i = 1 To iZeroCnt
    //                n_Char = "0" & n_Char
    //            Next
    //        Else
    //            'nothing
    //        End If
    //        ' End

    //        Return String.Format("{0}{1}{2}", r_Alphabets, n_Char, l_Alphabets)


    //    Catch ex As Exception
    //        Return ""
    //    End Try

    //End Function

    #region AutoInc_Runtime_Old

    public static string AutoInc_Runtime(string sPieceNo, int val)
    {
        if (sPieceNo.Trim().Length <= 0 | sPieceNo == "-")
            return "-";

        string strPrefix = "";
        string strVal = "";
        string strRetVal = "";
        string str1 = "";
        string str2 = "";
        string str3 = "";

        string _iVal1 = "";
        string _iVal2 = "";
        string _iVal3 = "";

        for (int i = 0; i <= sPieceNo.Length - 1; i++)
        {
            if (char.IsNumber(sPieceNo, i))
            {
                _iVal1 = sPieceNo.Substring(i, (sPieceNo.Length - i));
                str1 = sPieceNo.Substring(0, i);
                strRetVal = str1;

                break; // TODO: might not be correct. Was : Exit For
            }
        }

        sPieceNo = _iVal1;
        for (int i = 0; i <= sPieceNo.Length - 1; i++)
        {
            if (!char.IsNumber(sPieceNo, i))
            {
                _iVal2 = sPieceNo.Substring(i, (sPieceNo.Length - i));
                _iVal1 = sPieceNo.Substring(0, i);
                strRetVal += strPrefix;

                break; // TODO: might not be correct. Was : Exit For
            }
        }

        sPieceNo = _iVal2;
        for (int i = 0; i <= sPieceNo.Length - 1; i++)
        {
            if (char.IsNumber(sPieceNo, i))
            {
                _iVal2 = sPieceNo.Substring(i, (sPieceNo.Length - i));
                str2 = sPieceNo.Substring(0, i);
                strRetVal = strPrefix;
                break; // TODO: might not be correct. Was : Exit For
            }
        }

        sPieceNo = _iVal2;
        for (int i = 0; i <= sPieceNo.Length - 1; i++)
        {
            if (!char.IsNumber(sPieceNo, i))
            {
                strVal = sPieceNo.Substring(i, (sPieceNo.Length - i));
                _iVal2 = sPieceNo.Substring(0, i);
                strRetVal += strPrefix;

                break; // TODO: might not be correct. Was : Exit For
            }
        }

        sPieceNo = strVal;
        for (int i = 0; i <= sPieceNo.Length - 1; i++)
        {
            if (char.IsNumber(sPieceNo, i))
            {
                _iVal3 = sPieceNo.Substring(i, (sPieceNo.Length - i));
                str3 = sPieceNo.Substring(0, i);
                strRetVal = strPrefix;
                break; // TODO: might not be correct. Was : Exit For
            }
        }

        if ((string.IsNullOrEmpty(_iVal2)) && (string.IsNullOrEmpty(_iVal3)))
        {
            if (_iVal1 != "")
                _iVal1 = Convert.ToString(Int32.Parse(_iVal1) + 1).PadLeft(_iVal1.Length, '0');
        }
        else
        {
            if (val == 1 || val == 0)
            {
                if (_iVal1 != "")
                    _iVal1 = (Int32.Parse(_iVal1) + 1).ToString().PadLeft(_iVal1.Length, '0');
            }
            else if (val == 2)
            {
                if (_iVal2 != "")
                    _iVal2 = (Int32.Parse(_iVal2) + 1).ToString().PadLeft(_iVal2.Length, '0');
            }
            else
            {
                if (string.IsNullOrEmpty(_iVal3))
                {
                    if (_iVal2 != "")
                        _iVal2 = (Int32.Parse(_iVal2) + 1).ToString().PadLeft(_iVal2.Length, '0');
                }
                else
                {
                    if (_iVal3 != "")
                        _iVal3 = (Int32.Parse(_iVal3) + 1).ToString().PadLeft(_iVal3.Length, '0');
                }
            }
        }

        if (string.IsNullOrEmpty(_iVal3) || string.IsNullOrEmpty(str3))
            return str1 + _iVal1 + str2 + _iVal2 + str3 + _iVal3 + strVal;
        else
            return str1 + _iVal1 + str2 + _iVal2 + str3 + _iVal3;

    }

    #endregion

    //New
    //public static string AutoInc_Runtime(string sPieceNo, int val)
    //{
    //    if (sPieceNo.Trim().Length <= 0 | sPieceNo == "-")
    //        return "-";

    //    string strPrefix = "";
    //    string strVal = "";
    //    string strRetVal = "";
    //    string str1 = "";
    //    string str2 = "";
    //    string str3 = "";
    //    string str4 = "";

    //    string _iVal1 = "";
    //    string _iVal2 = "";
    //    string _iVal3 = "";
    //    string _iVal4 = "";

    //    for (int i = 0; i <= sPieceNo.Length - 1; i++)
    //    {
    //        if (char.IsNumber(sPieceNo, i))
    //        {
    //            _iVal1 = sPieceNo.Substring(i, (sPieceNo.Length - i));
    //            str1 = sPieceNo.Substring(0, i);
    //            strRetVal = str1;

    //            break; // TODO: might not be correct. Was : Exit For
    //        }
    //    }

    //    sPieceNo = _iVal1;
    //    for (int i = 0; i <= sPieceNo.Length - 1; i++)
    //    {
    //        if (!char.IsNumber(sPieceNo, i))
    //        {
    //            _iVal2 = sPieceNo.Substring(i, (sPieceNo.Length - i));
    //            _iVal1 = sPieceNo.Substring(0, i);
    //            strRetVal += strPrefix;

    //            break; // TODO: might not be correct. Was : Exit For
    //        }
    //    }

    //    sPieceNo = _iVal2;
    //    for (int i = 0; i <= sPieceNo.Length - 1; i++)
    //    {
    //        if (char.IsNumber(sPieceNo, i))
    //        {
    //            _iVal2 = sPieceNo.Substring(i, (sPieceNo.Length - i));
    //            str2 = sPieceNo.Substring(0, i);
    //            strRetVal = strPrefix;
    //            break; // TODO: might not be correct. Was : Exit For
    //        }
    //    }

    //    sPieceNo = _iVal2;
    //    for (int i = 0; i <= sPieceNo.Length - 1; i++)
    //    {
    //        if (!char.IsNumber(sPieceNo, i))
    //        {
    //            strVal = sPieceNo.Substring(i, (sPieceNo.Length - i));
    //            _iVal2 = sPieceNo.Substring(0, i);
    //            strRetVal += strPrefix;

    //            break; // TODO: might not be correct. Was : Exit For
    //        }
    //    }


    //    //Start Region
    //    //(CREATED FOR 3RD TIME CUTTING OF THE SAME PIECE)
    //    //if (_iVal2.Length > 1)
    //    {
    //        sPieceNo = strVal;
    //        for (int i = 0; i <= sPieceNo.Length - 1; i++)
    //        {
    //            if (char.IsNumber(sPieceNo, i))
    //            {
    //                _iVal3 = sPieceNo.Substring(i, (sPieceNo.Length - i));
    //                str3 = sPieceNo.Substring(0, i);
    //                strRetVal = strPrefix;
    //                break; // TODO: might not be correct. Was : Exit For
    //            }
    //        }

    //        sPieceNo = _iVal3;
    //        for (int i = 0; i <= sPieceNo.Length - 1; i++)
    //        {
    //            if (!char.IsNumber(sPieceNo, i))
    //            {
    //                strVal = sPieceNo.Substring(i, (sPieceNo.Length - i));
    //                _iVal3 = sPieceNo.Substring(0, i);
    //                strRetVal += strPrefix;

    //                break; // TODO: might not be correct. Was : Exit For
    //            }
    //        }
    //    }

    //    //End Region

    //    sPieceNo = strVal;
    //    for (int i = 0; i <= sPieceNo.Length - 1; i++)
    //    {
    //        if (char.IsNumber(sPieceNo, i))
    //        {
    //            _iVal4 = sPieceNo.Substring(i, (sPieceNo.Length - i));
    //            str4 = sPieceNo.Substring(0, i);
    //            strRetVal = strPrefix;
    //            break; // TODO: might not be correct. Was : Exit For
    //        }
    //    }

    //    if ((string.IsNullOrEmpty(_iVal2)) && (string.IsNullOrEmpty(_iVal3)))
    //    {
    //        _iVal1 = (Int32.Parse(_iVal1) + 1).ToString().PadLeft(_iVal1.Length, '0');
    //    }
    //    else
    //    {
    //        if (val == 1 || val == 0)
    //        {
    //            _iVal1 = (Int32.Parse(_iVal1) + 1).ToString().PadLeft(_iVal1.Length, '0');
    //        }
    //        else if (val == 2)
    //        {
    //            _iVal2 = (Int32.Parse(_iVal2) + 1).ToString().PadLeft(_iVal2.Length, '0');
    //        }
    //        else if (val == 3)
    //        {
    //            if (string.IsNullOrEmpty(_iVal3))
    //            {
    //                _iVal2 = (Int32.Parse(_iVal2) + 1).ToString().PadLeft(_iVal2.Length, '0');
    //            }
    //            else
    //            {
    //                _iVal3 = (Int32.Parse(_iVal3) + 1).ToString().PadLeft(_iVal3.Length, '0');
    //            }
    //        }
    //        else
    //        {
    //            if (string.IsNullOrEmpty(_iVal3))
    //            {
    //                _iVal2 = (Int32.Parse(_iVal2) + 1).ToString().PadLeft(_iVal2.Length, '0');
    //            }
    //            else if (string.IsNullOrEmpty(_iVal4))
    //            {
    //                _iVal3 = (Int32.Parse(_iVal3) + 1).ToString().PadLeft(_iVal3.Length, '0');
    //            }
    //            else
    //            {
    //                _iVal4 = (Int32.Parse(_iVal4) + 1).ToString().PadLeft(_iVal4.Length, '0');
    //            }
    //        }
    //    }

    //    //OLD
    //    //if (string.IsNullOrEmpty(_iVal3) || string.IsNullOrEmpty(str3))
    //    //    return str1 + _iVal1 + str2 + _iVal2 + str3 + _iVal3 + strVal;
    //    //else
    //    //    return str1 + _iVal1 + str2 + _iVal2 + str3 + _iVal3;

    //    //NEW
    //    if (string.IsNullOrEmpty(_iVal4) || string.IsNullOrEmpty(str4) || string.IsNullOrEmpty(_iVal3) || string.IsNullOrEmpty(str3))
    //        return str1 + _iVal1 + str2 + _iVal2 + str3 + _iVal3 + str4 + _iVal4 + strVal;
    //    else
    //        return str1 + _iVal1 + str2 + _iVal2 + str3 + _iVal3 + str4 + _iVal4;

    //    //return str1 + _iVal1 + str2 + _iVal2 + str3 + _iVal3 + str4 + _iVal4;
    //}

    public static string AutoInc_Runtime_CuttingReport(string sPieceNo, int val)
    {
        string sPiece = "";
        string sIncrement = "";
        string sStatic = Strings.Left(sPieceNo, (sPieceNo.Length - 1));
        string sChange = Strings.Right(sPieceNo, 1);
        sIncrement = (Localization.ParseNativeInt(sChange.ToString()) + 1).ToString();
        sPiece = sStatic + sIncrement;
        return sPiece;
    }

    public static void SetGridNum(CIS_DataGridViewEx.DataGridViewEx fgDtls)
    {
        try
        {
            int intCount = 1;
            int iSubCount = 0;
            {
                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    if (fgDtls.Rows[i].Cells[Db_Detials.pCol.IsHead.ToString()].Value.ToString() == "1")
                    {
                        fgDtls.Rows[i].Cells[Db_Detials.pCol.Srno.ToString()].Value = intCount;

                        DataGridViewRow theRow = fgDtls.Rows[i];
                        theRow.DefaultCellStyle.BackColor = Color.FromArgb(235, 237, 161);
                        theRow.DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 8f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
                        ((DataGridViewImageCell)theRow.Cells[Db_Detials.pCol.Sel.ToString()]).Value = CIS_Textil.Properties.Resources.AdjAmt;

                        iSubCount = 1;
                        intCount += 1;
                    }
                    else
                    {
                        DataGridViewRow theRow = fgDtls.Rows[i];
                        theRow.DefaultCellStyle.BackColor = Color.White;
                        theRow.DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 8f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, Convert.ToByte(0));
                        ((DataGridViewImageCell)theRow.Cells[Db_Detials.pCol.Sel.ToString()]).Value = CIS_Textil.Properties.Resources.Img_NoClick;

                        fgDtls.Rows[i].Cells[Db_Detials.pCol.Srno.ToString()].Value = iSubCount;
                        iSubCount += 1;
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
        //{
        //    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
        //}
    }

    public static string ShowFormCaptionByID(int intRtnID)
    {
        return Db_Detials.Hashref[intRtnID].ToString().Split(new char[] { ';' })[3].ToString();

        //Select Case strForm
        //    Case "PURCHASE"
        //        If Not blnRtnID Then
        //            Return "PR"
        //        Else
        //            Return Db_Detials.Ac_TrCodes.Purchase
        //        End If
        //    Case "PR"
        //        If Not blnRtnID Then
        //            Return "PURCHASE"
        //        Else
        //            Return Db_Detials.Ac_TrCodes.Purchase
        //        End If
        //    Case "SALES"
        //        If Not blnRtnID Then
        //            Return "SL"
        //        Else
        //            Return Db_Detials.Ac_TrCodes.Sales
        //        End If
        //    Case "SL"
        //        If Not blnRtnID Then
        //            Return "SALES"
        //        Else
        //            Return Db_Detials.Ac_TrCodes.Sales
        //        End If
        //    Case "PY"
        //        If Not blnRtnID Then
        //            Return "PAYMENT"
        //        Else
        //            Return Db_Detials.Ac_TrCodes.Payment
        //        End If
        //    Case "PAYMENT"
        //        If Not blnRtnID Then
        //            Return "PY"
        //        Else
        //            Return Db_Detials.Ac_TrCodes.Payment
        //        End If
        //    Case "RC"
        //        If Not blnRtnID Then
        //            Return "RECEIPT"
        //        Else
        //            Return Db_Detials.Ac_TrCodes.Receipt
        //        End If
        //    Case "RECEIPT"
        //        If Not blnRtnID Then
        //            Return "RC"
        //        Else
        //            Return Db_Detials.Ac_TrCodes.Receipt
        //        End If
        //    Case "CT"
        //        If Not blnRtnID Then
        //            Return "CONTRA"
        //        Else
        //            Return Db_Detials.Ac_TrCodes.Contra
        //        End If
        //    Case "DR"
        //        Return "DEBIT"
        //    Case "DEBIT"
        //        Return "DR"
        //    Case "CR"
        //        Return "CREDIT"
        //    Case "CREDIT"
        //        Return "CR"
        //    Case Else
        //        Select Case Val(strForm)
        //            Case Db_Detials.Ac_TrCodes.Contra
        //                Return "Contra"
        //            Case Db_Detials.Ac_TrCodes.Journal
        //                Return "Journal"
        //            Case Db_Detials.Ac_TrCodes.Payment
        //                Return "Payment"
        //            Case Db_Detials.Ac_TrCodes.Purchase
        //                Return "Purchase"
        //            Case Db_Detials.Ac_TrCodes.Receipt
        //                Return "Receipt"
        //            Case Db_Detials.Ac_TrCodes.Sales
        //                Return "Sales"
        //        End Select
        //        Return strForm
        //End Select

    }

    public static double Get_DrCr_Sum(DataGridView fgDtls, int ColDrCr)
    {
        double dblCalc = 0;
        try
        {
            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                if (ColDrCr == Localization.ParseNativeInt(fgDtls.Rows[i].Cells[Db_Detials.pCol.DrCr.ToString()].Value.ToString()))
                {
                    dblCalc += Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[Db_Detials.pCol.Amount.ToString()].Value.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
        return dblCalc;
    }

    //public static bool CheckDate(string StrDate, bool ChkCurDate)
    //{
    //    bool flag = false;
    //    DateTime time = Conversions.ToDate(DB.GetSnglValue(string.Format("Select Yr_From From tbl_YearMaster Where YearID = {0}", Db_Detials.YearID)));
    //    DateTime time2 = Conversions.ToDate(DB.GetSnglValue(string.Format("Select Yr_To From tbl_YearMaster Where YearID = {0}", Db_Detials.YearID)));
    //    DateTime curTime = Conversions.ToDate(DateTime.Now.Date);
    //    if ((StrDate == "__/__/____") | (StrDate == ""))
    //    {
    //        return true;
    //    }

    //    DateTime time3 = Conversions.ToDate(StrDate);
    //    if ((DateTime.Compare(time3, time) >= 0) && (DateTime.Compare(time3, time2) <= 0))
    //    {
    //        return true;
    //    }

    //    if (ChkCurDate == true)
    //    {
    //        if ((DateTime.Compare(time3, curTime) > 0))
    //        {
    //            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Date Should Be Upto Current Date");
    //            flag = false;
    //            return true;
    //        }
    //    }
    //    flag = false;
    //    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Valid Date");
    //    return flag;
    //}

    public static bool CheckDate(string StrDate, bool strchk)
    {
        bool flag = false;
        DateTime time = Conversions.ToDate(DB.GetSnglValue(string.Format("Select Yr_From From tbl_YearMaster Where YearID = {0}", Db_Detials.YearID)));
        DateTime time2 = Conversions.ToDate(DB.GetSnglValue(string.Format("Select Yr_To From tbl_YearMaster Where YearID = {0}", Db_Detials.YearID)));
        if ((StrDate == "__/__/____") | (StrDate == ""))
        {
            return true;
        }
        DateTime time3 = Conversions.ToDate(StrDate);
        if ((DateTime.Compare(time3, time) >= 0) && (DateTime.Compare(time3, time2) <= 0))
        {
            return true;
        }
        flag = false;
        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Valid Date");
        return flag;
    }

    //public static bool CheckDate(string StrDate, bool ChkCurDate)
    //{
    //    bool flag = false;
    //    DateTime time = Conversions.ToDate(DB.GetSnglValue(string.Format("Select Yr_From From tbl_YearMaster Where YearID = {0}", Db_Detials.YearID)));
    //    DateTime time2 = Conversions.ToDate(DB.GetSnglValue(string.Format("Select Yr_To From tbl_YearMaster Where YearID = {0}", Db_Detials.YearID)));
    //    DateTime time3 = Conversions.ToDate(DateTime.Now.Date);
    //    if ((StrDate == "__/__/____") | (StrDate == ""))
    //    {
    //        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Valid Date");
    //        flag = true;
    //        return flag;
    //    }

    //    DateTime time4 = Conversions.ToDate(StrDate);
    //    if ((DateTime.Compare(time4, time) <= 0) && (DateTime.Compare(time4, time2) >= 0))
    //    {
    //        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Enter Date Between Financial Year");
    //        flag = true;
    //        return flag;
    //    }

    //    if (ChkCurDate == true)
    //    {
    //        if ((DateTime.Compare(time4, time3) > 0))
    //        {
    //            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Date Should Be Upto Current Date");
    //            flag = true;
    //            return flag;
    //        }
    //    }
    //    return flag;
    //}

    public static double GetColSum(CIS_DataGridViewEx.DataGridViewEx fgDtls, int ColIndex, int Col_IdxDrCr = -1, int Col_IdxDrCrVal = -1)
    {
        double num = 0;
        try
        {
            double num2 = 0.0;
            DataGridViewEx ex = fgDtls;
            int num5 = ex.RowCount - 1;
            for (int i = 0; i <= num5; i++)
            {
                if (Col_IdxDrCr != -1)
                {
                    if (Localization.ParseNativeInt(Conversions.ToString(fgDtls.Rows[i].Cells[Col_IdxDrCr].Value)) == Col_IdxDrCrVal)
                    {
                        num2 -= Localization.ParseNativeDouble(Conversions.ToString(fgDtls.Rows[i].Cells[Col_IdxDrCr].Value));
                    }
                    else
                    {
                        num2 += Localization.ParseNativeDouble(Conversions.ToString(ex.Rows[i].Cells[ColIndex].Value));
                    }
                }
                else
                {
                    num2 += Localization.ParseNativeDouble(Conversions.ToString(ex.Rows[i].Cells[ColIndex].Value));
                }
            }
            ex = null;
            num = num2;
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
        return num;

    }

    public static double GetColSum_Head(CIS_DataGridViewEx.DataGridViewEx fgDtls, int ColIndex, int Col_IdxDrCr = -1, int Col_IdxDrCrVal = -1, int flag = 0)
    {
        double num = 0;
        if (flag != 0)
        {
            try
            {
                double num2 = 0.0;
                DataGridViewEx ex = fgDtls;
                int num5 = ex.RowCount - 1;
                for (int i = 0; i <= num5; i++)
                {
                    if (Col_IdxDrCr != -1)
                    {
                        if (Localization.ParseNativeInt(Conversions.ToString(fgDtls.Rows[i].Cells[Col_IdxDrCr].Value)) == flag)
                        {
                            num2 -= Localization.ParseNativeDouble(Conversions.ToString(fgDtls.Rows[i].Cells[Col_IdxDrCr].Value));
                        }
                        else
                        {
                            num2 += Localization.ParseNativeDouble(Conversions.ToString(ex.Rows[i].Cells[ColIndex].Value));
                        }
                    }
                    else
                    {
                        num2 += Localization.ParseNativeDouble(Conversions.ToString(ex.Rows[i].Cells[ColIndex].Value));
                    }
                }
                ex = null;
                num = num2;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
        return num;

    }

    public static object gf_CalcMeters(string pFormula, int pEnds = 0, int pTapLen = 0, int pDnr_YarnID = 0, int pWidth = 0, int pPick = 0)
    {
        try
        {
            string strFrml = pFormula.ToString().ToUpper();
            if (!string.IsNullOrEmpty(strFrml))
            {
                if (pEnds != 0)
                    strFrml = strFrml.Replace("ENDS", pEnds.ToString());
                if (pTapLen != 0)
                    strFrml = strFrml.Replace("TAPLEN", pTapLen.ToString());
                if (pDnr_YarnID != 0)
                    strFrml = strFrml.Replace("COUNT", pDnr_YarnID.ToString());
                if (pDnr_YarnID != 0)
                    strFrml = strFrml.Replace("WIDTH", pWidth.ToString());
                if (pDnr_YarnID != 0)
                    strFrml = strFrml.Replace("PICK", pPick.ToString());
                if (pFormula.ToString().ToUpper() != strFrml)
                {
                    return CIS_Evaluator.Evaluator.EvalToDouble(strFrml);
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }

        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return "";
        }
    }

    public static void GetPieceNo(ref string PieceNo, string old_PieceNo = "")
    {
        try
        {
            PieceNo = DB.GetSnglValue(string.Format("select isnull(max(convert(numeric(18,0),BatchNo)),0) + 1  as BatchNo from {0} where BatchNo <> '-' ", Db_Detials.tbl_StockFabricLedger));
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    public static void ShowAdjBills(CIS_DataGridViewEx.DataGridViewEx fgDtls, int RowIndex, int EntryNo, string EntryDate, int iDentity, Db_Detials.Ac_DrCr DrCr, Db_Detials.pCol pSetFocus, Enum_Define.ActionType FormAction, int pOnRow = -1, int TransID = 0, DataTable Dt_tmp = null)
    {
        try
        {
            {
                if (fgDtls.Rows[RowIndex].Cells[14].Value == null && string.IsNullOrEmpty(fgDtls.Rows[RowIndex].Cells[14].Value.ToString()))
                    return;
                fgDtls.Rows[RowIndex].Cells[15].Value = "1";
                if (Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(0) From {0} Where LedgerId = {1} And MBM = 1", Db_Detials.tbl_LedgerMaster, Localization.ParseNativeInt(fgDtls.Rows[RowIndex].Cells[3].Value.ToString())))) <= 0)
                {
                    if (fgDtls.Rows[RowIndex].Cells[15].Value == null)
                    {
                        if (((Localization.ParseNativeInt(fgDtls.Rows[fgDtls.RowCount - 1].Cells[3].Value.ToString()) == 0 && Localization.ParseNativeInt(fgDtls.Rows[fgDtls.RowCount - 1].Cells[4].Value.ToString()) == 0)))
                            return;
                    }
                    fgDtls.Rows[RowIndex].Cells[15].Value = "0";
                    fgDtls.Rows[RowIndex].Cells[16].Value = (Localization.ParseBoolean(DB.GetSnglValue(string.Format("Select IVA From {0} Where LedgerId = {1}", Db_Detials.tbl_LedgerMaster, Localization.ParseNativeInt(fgDtls.Rows[RowIndex].Cells[3].Value.ToString())))) ? 1 : 0);
                    fgDtls.Rows.Add();
                    CommonCls.SetGridNum(fgDtls);
                    //fgDtls.CurrentCell = fgDtls[Localization.ParseNativeInt(pSetFocus.ToString()), (fgDtls.RowCount - 1)];                    
                    return;
                }

                if (RowIndex > 0)
                {
                    for (int i = RowIndex - 1; i >= 0; i += -1)
                    {
                        if (fgDtls.Rows[RowIndex].Cells[14].Value != null && fgDtls.Rows[RowIndex].Cells[14].Value.ToString() == "1")
                        {
                            if (Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[3].Value.ToString()) == Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[3].Value.ToString()))
                            {
                                fgDtls.CurrentCell = fgDtls[3, RowIndex];
                                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Warning, Application.ProductName, "This Account Ledger already selected, please select other Ledger...");
                                return;
                            }
                        }
                    }
                }
            }

            CIS_Textil.frmAdjustment adjust = new CIS_Textil.frmAdjustment();
            adjust.Ref_fgDtls = fgDtls;
            adjust.Ref_intRowNum = RowIndex;
            adjust.pLedgerID = Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[3].Value.ToString());
            adjust.dblAmount = Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[4].Value.ToString());
            adjust.Ref_TxnCode = EntryNo;
            adjust.Ref_Date = EntryDate;
            adjust.RefModID = iDentity;

            adjust.RefDrCr = DrCr;
            adjust.Ref_Dt_tmp = Dt_tmp;
            adjust.FormAction = FormAction;
            if (TransID > 0)
                adjust.TransId = TransID;
            adjust.ShowDialog();
            if (fgDtls.Rows[fgDtls.RowCount - 1].Cells[14].Value != null && !string.IsNullOrEmpty(fgDtls.Rows[fgDtls.RowCount - 1].Cells[14].Value.ToString()))
                return;
            if (((fgDtls.Rows[fgDtls.RowCount - 1].Cells[3].Value != null && Localization.ParseNativeInt(fgDtls.Rows[fgDtls.RowCount - 1].Cells[3].Value.ToString()) == 0 && fgDtls.Rows[fgDtls.RowCount - 1].Cells[4].Value != null && Localization.ParseNativeDouble(fgDtls.Rows[fgDtls.RowCount - 1].Cells[4].Value.ToString()) == 0)))
            {
                fgDtls.Rows.Add();
                CommonCls.SetGridNum(fgDtls);
                if (pOnRow == -1)
                {
                    fgDtls.CurrentCell = fgDtls[Localization.ParseNativeInt(pSetFocus.ToString()), (fgDtls.RowCount - 1)];
                }
                else
                {
                    for (int i = Localization.ParseNativeInt(pSetFocus.ToString()); i <= fgDtls.ColumnCount; i++)
                    {
                        if (fgDtls.Rows[pOnRow].Cells[i].ReadOnly == false & fgDtls.Rows[pOnRow].Cells[i].Visible == true)
                        {
                            fgDtls.CurrentCell = fgDtls[i, RowIndex];
                            break;
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

    public static void TransactionAdjBills(CIS_DataGridViewEx.DataGridViewEx fgDtls, int RowIndex, int EntryNo, string EntryDate, int iDentity, Db_Detials.Ac_DrCr DrCr, Db_Detials.pCol pSetFocus, Enum_Define.ActionType FormAction, int pOnRow = -1, int TransID = 0, DataTable Dt_tmp = null)
    {
        try
        {
            //{
            //    if (string.IsNullOrEmpty(fgDtls.Rows[RowIndex].Cells[14].Value.ToString()))
            //        return;
            //    fgDtls.Rows[RowIndex].Cells[15].Value = "1";
            //    if (Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(0) From {0} Where LedgerId = {1} And MBM = 1", Db_Detials.tbl_LedgerMaster, Localization.ParseNativeInt(fgDtls.Rows[RowIndex].Cells[3].Value.ToString())))) <= 0)
            //    {
            //        if (fgDtls.Rows[RowIndex].Cells[15].Value == null)
            //        {
            //            if (((Localization.ParseNativeInt(fgDtls.Rows[fgDtls.RowCount - 1].Cells[3].Value.ToString()) == 0 && Localization.ParseNativeInt(fgDtls.Rows[fgDtls.RowCount - 1].Cells[4].Value.ToString()) == 0)))
            //                return;
            //        }
            //        fgDtls.Rows[RowIndex].Cells[15].Value = "0";
            //        fgDtls.Rows[RowIndex].Cells[16].Value = (Localization.ParseBoolean(DB.GetSnglValue(string.Format("Select IVA From {0} Where LedgerId = {1}", Db_Detials.tbl_LedgerMaster, Localization.ParseNativeInt(fgDtls.Rows[RowIndex].Cells[3].Value.ToString())))) ? 1 : 0);
            //        fgDtls.Rows.Add();
            //        CommonCls.SetGridNum(fgDtls);
            //        //fgDtls.CurrentCell = fgDtls[Localization.ParseNativeInt(pSetFocus.ToString()), (fgDtls.RowCount - 1)];                    
            //        return;
            //    }

            //    if (RowIndex > 0)
            //    {
            //        for (int i = RowIndex - 1; i >= 0; i += -1)
            //        {
            //            if (fgDtls.Rows[RowIndex].Cells[13].Value.ToString() == "1")
            //            {
            //                if (Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[3].Value.ToString()) == Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[3].Value.ToString()))
            //                {
            //                    fgDtls.CurrentCell = fgDtls[3, RowIndex];
            //                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Warning, Application.ProductName, "This Account Ledger already selected, please select other Ledger...");
            //                    return;
            //                }
            //            }
            //        }
            //    }
            //}

            CIS_Textil.frmInstrumentDtls adjust = new CIS_Textil.frmInstrumentDtls();
            adjust.Ref_fgDtls = fgDtls;
            adjust.Ref_intRowNum = RowIndex;
            adjust.pLedgerID = Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[0].Value.ToString());
            adjust.dblAmount = Localization.ParseNativeDouble(fgDtls.Rows[RowIndex].Cells[8].Value.ToString());
            adjust.Ref_TxnCode = EntryNo;
            adjust.Ref_Date = EntryDate;
            adjust.RefModID = iDentity;

            adjust.RefDrCr = DrCr;
            adjust.Ref_Dt_tmp = Dt_tmp;
            adjust.FormAction = FormAction;
            if (TransID > 0)
                adjust.TransId = TransID;
            adjust.ShowDialog();
            if (!string.IsNullOrEmpty(fgDtls.Rows[fgDtls.RowCount - 1].Cells[14].Value.ToString()))
                return;
            if (((Localization.ParseNativeInt(fgDtls.Rows[fgDtls.RowCount - 1].Cells[3].Value.ToString()) == 0 && Localization.ParseNativeDouble(fgDtls.Rows[fgDtls.RowCount - 1].Cells[4].Value.ToString()) == 0)))
            {
                fgDtls.Rows.Add();
                CommonCls.SetGridNum(fgDtls);
                if (pOnRow == -1)
                {
                    fgDtls.CurrentCell = fgDtls[Localization.ParseNativeInt(pSetFocus.ToString()), (fgDtls.RowCount - 1)];
                }
                else
                {
                    for (int i = Localization.ParseNativeInt(pSetFocus.ToString()); i <= fgDtls.ColumnCount; i++)
                    {
                        if (fgDtls.Rows[pOnRow].Cells[i].ReadOnly == false & fgDtls.Rows[pOnRow].Cells[i].Visible == true)
                        {
                            fgDtls.CurrentCell = fgDtls[i, RowIndex];
                            break;
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

    public static string AutoAdjustments(int LedgerID, double AdjAmt, Db_Detials.Ac_DrCr DrCrID)
    {
        try
        {
            string strAdjustQry = string.Empty;
            using (DataTable dt = DB.GetDT(string.Format("{0} -1, -1, {1}", Db_Detials.sp_BillAdjustment, LedgerID), false))
            {
                DataRow[] drRows = dt.Select(string.Format("(Bal_Amt <> 0 And DrCrID = {0} And AdjType <> {1}) ", DrCrID, Localization.ParseNativeInt(Db_Detials.Ac_AdjType.OnAccount.ToString())));
                if (drRows.Length > 0)
                {
                }
            }
            return strAdjustQry;
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            return "";
        }
    }

    public static void grdSearch_InitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e)
    {
        // UltraGrid has a built in ui that lets the user display a column chooser dialog.
        // You can enable the ui by enabling the row selectors and setting the RowSelectorHeaderStyle.
        e.Layout.Override.RowSelectors = DefaultableBoolean.True;
        e.Layout.Override.RowSelectorHeaderStyle = RowSelectorHeaderStyle.ColumnChooserButton;

        e.Layout.Override.RowSizing = RowSizing.Free;
        e.Layout.Bands[0].AutoPreviewEnabled = true;

        // FILTER ROW FUNCTIONALITY RELATED ULTRAGRID SETTINGS
        // ----------------------------------------------------------------------------------
        // Enable the the filter row user interface by setting the FilterUIType to FilterRow.
        e.Layout.Override.FilterUIType = FilterUIType.FilterRow;

        // FilterEvaluationTrigger specifies when UltraGrid applies the filter criteria typed 
        // into a filter row. Default is OnCellValueChange which will cause the UltraGrid to
        // re-filter the data as soon as the user modifies the value of a filter cell.
        e.Layout.Override.FilterEvaluationTrigger = FilterEvaluationTrigger.OnCellValueChange;

        // By default the UltraGrid selects the type of the filter operand editor based on
        // the column's DataType. For DateTime and boolean columns it uses the column's editors.
        // For other column types it uses the Combo. You can explicitly specify the operand
        // editor style by setting the FilterOperandStyle on the override or the individual
        // columns.
        e.Layout.Override.FilterOperandStyle = FilterOperandStyle.Combo;

        // By default UltraGrid displays user interface for selecting the filter operator. 
        // You can set the FilterOperatorLocation to hide this user interface. This
        // property is available on column as well so it can be controlled on a per column
        // basis. Default is WithOperand. This property is exposed off the column as well.
        e.Layout.Override.FilterOperatorLocation = FilterOperatorLocation.WithOperand;

        // By default the UltraGrid uses StartsWith as the filter operator. You use
        // the FilterOperatorDefaultValue property to specify a different filter operator
        // to use. This is the default or the initial filter operator value of the cells
        // in filter row. If filter operator user interface is enabled (FilterOperatorLocation
        // is not set to None) then that ui will be initialized to the value of this
        // property. The user can then change the operator as he/she chooses via the operator
        // drop down.
        e.Layout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.StartsWith;

        // FilterOperatorDropDownItems property can be used to control the options provided
        // to the user for selecting the filter operator. By default UltraGrid bases 
        // what operator options to provide on the column's data type. This property is
        // avaibale on the column as well.
        //e.Layout.Override.FilterOperatorDropDownItems = FilterOperatorDropDownItems.All;

        // By default UltraGrid displays a clear button in each cell of the filter row
        // as well as in the row selector of the filter row. When the user clicks this
        // button the associated filter criteria is cleared. You can use the 
        // FilterClearButtonLocation property to control if and where the filter clear
        // buttons are displayed.
        e.Layout.Override.FilterClearButtonLocation = FilterClearButtonLocation.RowAndCell;

        // Appearance of the filter row can be controlled using the FilterRowAppearance proeprty.
        e.Layout.Override.FilterRowAppearance.BackColor = Color.LightYellow;

        // You can use the FilterRowPrompt to display a prompt in the filter row. By default
        // UltraGrid does not display any prompt in the filter row.
        //e.Layout.Override.FilterRowPrompt = "Click here to filter data..."

        // You can use the FilterRowPromptAppearance to change the appearance of the prompt.
        // By default the prompt is transparent and uses the same fore color as the filter row.
        // You can make it non-transparent by setting the appearance' BackColorAlpha property 
        // or by setting the BackColor to a desired value.
        e.Layout.Override.FilterRowPromptAppearance.BackColorAlpha = Alpha.Transparent;

        // By default the prompt is spread across multiple cells if it's bigger than the
        // first cell. You can confine the prompt to a particular cell by setting the
        // SpecialRowPromptField property off the band to the key of a column.
        //e.Layout.Bands[0].SpecialRowPromptField = e.Layout.Bands[0].Columns[0].Key;

        // Display a separator between the filter row other rows. SpecialRowSeparator property 
        // can be used to display separators between various 'special' rows, including for the
        // filter row. This property is a flagged enum property so it can take multiple values.
        e.Layout.Override.SpecialRowSeparator = SpecialRowSeparator.FilterRow;

        // You can control the appearance of the separator using the SpecialRowSeparatorAppearance
        // property.
        e.Layout.Override.SpecialRowSeparatorAppearance.BackColor = Color.FromArgb(233, 242, 199);

        // ------------------------------------------------------------------------
        // To allow the user to be able to add/remove summaries set the 
        // AllowRowSummaries property. This does not have to be set to summarize
        // data in code.
        e.Layout.Override.AllowRowSummaries = AllowRowSummaries.True;
        e.Layout.Override.AllowRowSummaries = AllowRowSummaries.BasedOnDataType;

        // To display summary footer on the top of the row collections set the 
        // SummaryDisplayArea property to a value that has the Top or TopFixed flag
        // set. TopFixed will make the summary fixed (non-scrolling). Note that 
        // summaries are not fixed in the child rows. TopFixed setting behaves
        // the same way as Top in child rows. Default is resolved to Bottom (and
        // InGroupByRows more about which follows).
        e.Layout.Override.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;

        // By default UltraGrid does not display summary footers or headers of
        // group-by row islands. To display summary footers or headers of group-by row
        // islands set the SummaryDisplayArea to a value that has GroupByRowsFooter
        // flag set.
        e.Layout.Override.SummaryDisplayArea = e.Layout.Override.SummaryDisplayArea | SummaryDisplayAreas.GroupByRowsFooter;

        // If you want to to display summaries of child rows in each group-by row
        // set the SummaryDisplayArea to a value that has SummaryDisplayArea flag
        // set. If SummaryDisplayArea is left to Default then the UltraGrid by
        // default displays summaries in group-by rows.
        e.Layout.Override.SummaryDisplayArea = e.Layout.Override.SummaryDisplayArea | SummaryDisplayAreas.InGroupByRows;

        // By default any summaries to be displayed in the group-by rows are displayed
        // as text appended to the group-by row's description. You can set the 
        // GroupBySummaryDisplayStyle property to SummaryCells or 
        // SummaryCellsAlwaysBelowDescription to display summary values as a separate
        // ui element (cell like element with border, to which the summary value related
        // appearances are applied). Default value of GroupBySummaryDisplayStyle is resolved
        // to Text.
        e.Layout.Override.GroupBySummaryDisplayStyle = GroupBySummaryDisplayStyle.SummaryCells;

        // Appearance of the summary area can be controlled using the 
        // SummaryFooterAppearance. Even though the property's name contains the
        // word 'footer', this appearance applies to summary area that is displayed
        // on top as well (summary headers).
        e.Layout.Override.SummaryFooterAppearance.BackColor = SystemColors.Info;

        // Appearance of summary values can be controlled using the 
        // SummaryValueAppearance property.
        e.Layout.Override.SummaryValueAppearance.BackColor = SystemColors.Window;
        e.Layout.Override.SummaryValueAppearance.FontData.Bold = DefaultableBoolean.True;

        // Appearance of summary values that are displayed inside of group-by rows can 
        // be controlled using the GroupBySummaryValueAppearance property.
        e.Layout.Override.GroupBySummaryValueAppearance.BackColor = SystemColors.Window;
        e.Layout.Override.GroupBySummaryValueAppearance.TextHAlign = HAlign.Right;

        // Caption of the summary area can be set using the SummaryFooterCaption
        // proeprty of the band.
        e.Layout.Bands[0].SummaryFooterCaption = "Grand Totals:";

        // Caption's appearance can be controlled using the SummaryFooterCaptionAppearance
        // property.
        e.Layout.Override.SummaryFooterCaptionAppearance.FontData.Bold = DefaultableBoolean.True;

        // By default summary footer caption is visible. You can hide it using the
        // SummaryFooterCaptionVisible property.
        e.Layout.Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;

        // Display a separator between summary rows and scrolling rows.
        // SpecialRowSeparator property can be used to display separators between
        // various 'special' rows, including filer row, add-row, summary row and
        // fixed rows. This property is a flagged enum property so it can take 
        // multiple values.
        e.Layout.Override.SpecialRowSeparator = SpecialRowSeparator.SummaryRow;

        // Appearance of the separator can be controlled using the 
        // SpecialRowSeparatorAppearance property.
        e.Layout.Override.SpecialRowSeparatorAppearance.BackColor = Color.FromArgb(218, 217, 241);

        // Height of the separator can be controlled as well using the 
        // SpecialRowSeparatorHeight property.
        e.Layout.Override.SpecialRowSeparatorHeight = 6;
        e.Layout.Override.RowSelectorNumberStyle = RowSelectorNumberStyle.RowIndex;

        // Border style of the separator can be controlled using the 
        // BorderStyleSpecialRowSeparator property.
        e.Layout.Override.BorderStyleSpecialRowSeparator = UIElementBorderStyle.RaisedSoft;

        // ------------------------------------------------------------------------
        // OTHER MISCELLANEOUS ULTRAGRID SETTINGS
        // ------------------------------------------------------------------------
        // Set the view style to SingleBand.
        e.Layout.ViewStyle = ViewStyle.SingleBand;

        // Set the view style band to OutlookGroupBy.
        e.Layout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;

        //UGrid_Rpt.DisplayLayout.Override.FilterUIType = FilterUIType.HeaderIcons

    }

    public static bool ValidateMaster(Form frm, CIS_Textbox txtMaster, CIS_Textbox txtShortCode, string TbleName, string PmryColumn)
    {
        try
        {
            Enum_Define.ActionType FormAction = (Enum_Define.ActionType)NewLateBinding.LateGet(frm, null, "blnFormAction", new object[0], null, null, null);
            if (FormAction == Enum_Define.ActionType.New_Record)
            {
                if (txtMaster.Text.Trim().Length > 0)
                {
                    string SqlQry = "Select count(0) from " + TbleName + " Where AliasName=" + CommonLogic.SQuote(txtMaster.Text) + " and IsDeleted=0";
                    if (Localization.ParseNativeInt(DB.GetSnglValue(SqlQry)) > 0)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "This " + PmryColumn + " is Already used in Alias Name...!");
                        txtMaster.Text = "";
                        txtMaster.Focus();
                        return true;
                    }
                }
            }
            else if (FormAction == Enum_Define.ActionType.Edit_Record)
            {
                CIS_CLibrary.CIS_Textbox txtCode = (CIS_Textbox)NewLateBinding.LateGet(frm, null, "txtCode", new object[0], null, null, null);
                string strPrimaryCol = ((DataTable)NewLateBinding.LateGet(frm, null, "ds", new object[0], null, null, null)).Columns[0].ColumnName;
                if (txtMaster.Text.Trim().Length > 0)
                {
                    string SqlQry = "Select count(0) from " + TbleName + " Where AliasName=" + CommonLogic.SQuote(txtMaster.Text) + " and " + strPrimaryCol + "<>" + txtCode.Text + " and IsDeleted=0";
                    if (Localization.ParseNativeInt(DB.GetSnglValue(SqlQry)) > 0)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "This " + PmryColumn + " is Already used in Alias Name...!");
                        txtMaster.Text = "";
                        txtMaster.Focus();
                        return true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
        return false;
    }

    public static bool ValidateShortCode(Form frm, CIS_Textbox txtMaster, CIS_Textbox txtShortCode, string TbleName, string PmryColumn)
    {
        try
        {
            Enum_Define.ActionType FormAction = (Enum_Define.ActionType)NewLateBinding.LateGet(frm, null, "blnFormAction", new object[0], null, null, null);
            if (FormAction == Enum_Define.ActionType.New_Record || FormAction == Enum_Define.ActionType.Edit_Record)
            {
                if (txtShortCode.Text.Trim().Length > 0)
                {
                    if (FormAction == Enum_Define.ActionType.New_Record)
                    {
                        if (Localization.ParseNativeInt(DB.GetSnglValue("Select count(0) from " + TbleName + " Where " + PmryColumn + "=" + CommonLogic.SQuote(txtShortCode.Text) + "")) > 0)
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "This Alias Name is Already used in " + PmryColumn + "...!");
                            txtShortCode.Text = "";
                            txtShortCode.Focus();
                            return true;
                        }

                        if (Localization.ParseNativeInt(DB.GetSnglValue("Select count(0) from " + TbleName + " Where AliasName=" + CommonLogic.SQuote(txtShortCode.Text) + "")) > 0)
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Duplicate Alias name not Allowed... !");
                            txtShortCode.Text = "";
                            txtShortCode.Focus();
                            return true;
                        }
                    }
                    else if (FormAction == Enum_Define.ActionType.Edit_Record)
                    {

                        CIS_CLibrary.CIS_Textbox txtCode = (CIS_Textbox)NewLateBinding.LateGet(frm, null, "txtCode", new object[0], null, null, null);
                        string strPrimaryCol = ((DataTable)NewLateBinding.LateGet(frm, null, "ds", new object[0], null, null, null)).Columns[0].ColumnName;
                        if (Localization.ParseNativeInt(DB.GetSnglValue("Select count(0) from " + TbleName + " Where " + PmryColumn + "=" + CommonLogic.SQuote(txtShortCode.Text) + " and " + strPrimaryCol + "<>" + txtCode.Text + " and IsDeleted=0")) > 0)
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "This Alias Name is Already used in " + PmryColumn + "...!");
                            txtShortCode.Text = "";
                            txtShortCode.Focus();
                            return true;

                        }

                        if (Localization.ParseNativeInt(DB.GetSnglValue("Select count(0) from " + TbleName + " Where " + PmryColumn + "=" + CommonLogic.SQuote(txtShortCode.Text) + " and " + strPrimaryCol + "<>" + txtCode.Text + " and IsDeleted=0")) > 0)
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Duplicate Alias name not Allowed... !");
                            txtShortCode.Text = "";
                            txtShortCode.Focus();
                            return true;
                        }
                    }

                    if (txtShortCode.Text.Trim().Length > 0 && txtMaster.Text.Trim().Length > 0)
                    {
                        if (txtShortCode.Text.ToUpper() == txtMaster.Text.ToUpper())
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Alias Name should not be same as " + PmryColumn + "... !");
                            txtShortCode.Text = "";
                            txtShortCode.Focus();
                            return true;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
        return false;
    }

    public static string GetIP()
    {
        string Str = "";
        Str = System.Net.Dns.GetHostName();
        IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(Str);
        IPAddress[] addr = ipEntry.AddressList;
        if (addr[addr.Length - 1].ToString().Contains(":") == false)
        {
            return addr[addr.Length - 1].ToString();
        }
        else { return addr[addr.Length - 2].ToString(); }
    }

    public static string FetchMacId()
    {
        string macAddresses = "";

        foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (nic.OperationalStatus == OperationalStatus.Up)
            {
                macAddresses += nic.GetPhysicalAddress().ToString();
                break;
            }
        }
        return macAddresses;
    }

    public static void InsertGlobalSetings(int iCompID)
    {
        try
        {
            string sQry = "";
            if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT COUNT(0) FROM tbl_GlobalSettings WHERE CompanyForID=" + iCompID)) == 0)
            {
                sQry = " INSERT INTO tbl_GlobalSettings (GType, GSName, GSValue, Description, CompanyForID, CompID, YearId, AddedOn, AddedBy, IsModified, IsDeleted, IsCanclled, IsApproved, IsAudited)";
                sQry += "SELECT	GType, GSName, GSValue, Description, " + iCompID + ", CompID, YearId, AddedOn, AddedBy, IsModified, IsDeleted, IsCanclled, IsApproved, IsAudited FROM tbl_GlobalSettings  WHERE CompanyForID=1";
                DB.ExecuteSQL(sQry);
            }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    public static string GetReportDocument(string sID, string sVal, string sMenuId, int sVoucherTypeID)
    {
        string sstartPath = "";
        string sReportName = "";
        object sReportPath = null;
        CrystalConnection();
        objReport = new ReportDocument();
        crViewer = new CrystalReportViewer();
        if (sID != "")
        {
            using (IDataReader dr = DB.GetRS(string.Format("Select * From fn_GetReportList({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},0,0)", sMenuId, 0, sID, sVal, sVal, "NULL", "NULL", 0, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, "'ReportType'", 1, sVoucherTypeID)))
            {
                while (dr.Read())
                {
                    if (Localization.ParseBoolean(dr["ReportType"].ToString()))
                    {
                        if (Application.StartupPath.ToString().Contains(@"bin\Debug"))
                        {
                            sReportPath = Application.StartupPath.ToString().Replace(@"bin\Debug", "") + @"Reports\" + dr["ReportName"].ToString() + ".rpt";
                        }
                        else
                        {
                            sReportPath = Application.StartupPath.ToString() + @"\Reports\" + dr["ReportName"].ToString() + ".rpt";
                        }

                        objReport.Load(Conversions.ToString(sReportPath));
                        SetDBLogonForReport(connectionInfo, objReport);
                        if (!Localization.ParseBoolean(dr["IsSelectQry"].ToString()))
                        {
                            continue;
                        }
                        string StrQuery = string.Empty;
                        sReportName = dr["ReportName"].ToString();
                        StrQuery = dr["QueryString_Main"].ToString();

                        objReport.SetDataSource(DB.GetDT(StrQuery.ToString().Trim(), false));

                        crViewer.ReportSource = objReport;
                        ParameterFields myParameterFields = crViewer.ParameterFieldInfo;
                        if (Conversions.ToInteger(DB.GetSnglValue("select Count(0) from sysobjects Where xtype IN ('P','IF') And [Name] = '" + dr["QueryString"].ToString() + "'")) == 1)
                        {
                            using (IDataReader iDr_P = DB.GetRS("SELECT * from tbl_ReportList_Parameters WHERE ReportID=" + dr["ReportID"].ToString() + " Order By OrderNo;"))
                            {
                                while (iDr_P.Read())
                                {
                                    if (iDr_P["ParameterFld"].ToString() == "LedgerID")
                                        SetCurrentValuesForParameterField(myParameterFields, iDr_P["PrmyCOl"].ToString(), sID);
                                    else if (iDr_P["ParameterFld"].ToString() == "StoreID")
                                        SetCurrentValuesForParameterField(myParameterFields, iDr_P["PrmyCOl"].ToString(), Db_Detials.StoreID.ToString());
                                    else if (iDr_P["ParameterFld"].ToString() == "CompID")
                                        SetCurrentValuesForParameterField(myParameterFields, iDr_P["PrmyCOl"].ToString(), Db_Detials.CompID.ToString());
                                    else if (iDr_P["ParameterFld"].ToString() == "BranchID")
                                        SetCurrentValuesForParameterField(myParameterFields, iDr_P["PrmyCOl"].ToString(), Db_Detials.BranchID.ToString());
                                    else if (iDr_P["ParameterFld"].ToString() == "YearID")
                                        SetCurrentValuesForParameterField(myParameterFields, iDr_P["PrmyCOl"].ToString(), Db_Detials.YearID.ToString());
                                    else if (iDr_P["ParameterFld"].ToString() == "ReportType")
                                        SetCurrentValuesForParameterField(myParameterFields, iDr_P["PrmyCOl"].ToString(), "1");
                                }
                            }
                        }
                        continue;
                    }

                    if (!Localization.ParseBoolean(dr["ReportType"].ToString()))
                    {
                        SetDBLogonForSubreports(connectionInfo, objReport, Localization.ParseNativeInt(sMenuId), "", "", Localization.ParseNativeInt(sID));
                    }
                }
            }

            try
            {
                sstartPath = Application.StartupPath.ToString().Replace("bin\\Debug", "") + "EmailDoc\\" + sMenuId + "\\" + Db_Detials.UserID + "\\" + sReportName.Replace(" ", "").Replace("rpt_", "") + "_" + Localization.ToSqlDateString(DateTime.Now.Date.ToString()).Replace("/", "_") + "_" + Db_Detials.UserID;
                if (!Directory.Exists(sstartPath))
                    Directory.CreateDirectory(sstartPath);


                string sMenuNM = DB.GetSnglValue("SELECT Form_Caption from tbl_MenuMaster WHERE MenuID=" + sMenuId);
                ExportOptions CrExportOptions;
                DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
                PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
                CrDiskFileDestinationOptions.DiskFileName = sstartPath + "//" + sMenuNM + ".pdf";
                CrExportOptions = objReport.ExportOptions;
                {
                    CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                    CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                    CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
                    CrExportOptions.FormatOptions = CrFormatTypeOptions;
                }
                objReport.Export();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
        return sstartPath + ";" + sReportName;
    }

    public static ConnectionInfo CrystalConnection()
    {
        IniFile ini = new IniFile(Application.StartupPath.ToString() + @"\Others\System.ini");
        connectionInfo = new ConnectionInfo();
        connectionInfo.AllowCustomConnection = true;
        connectionInfo.ServerName = CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "ServerName"));
        connectionInfo.DatabaseName = CommonLogic.UnmungeString(ini.IniReadValue("DATABASESETTING", "DataBaseName"));
        connectionInfo.IntegratedSecurity = false;
        connectionInfo.Type = ConnectionInfoType.SQL;
        connectionInfo.UserID = DB._UserName;
        connectionInfo.Password = DB._Password;
        return connectionInfo;
    }

    public static void SetDBLogonForReport(ConnectionInfo myConnectionInfo, ReportDocument myReportDocument)
    {
        try
        {
            IEnumerator con = null;
            Tables myTables = myReportDocument.Database.Tables;
            try
            {
                con = myTables.GetEnumerator();
                while (con.MoveNext())
                {
                    Table myTable = (Table)con.Current;
                    TableLogOnInfo myTableLogonInfo = myTable.LogOnInfo;
                    myTableLogonInfo.ConnectionInfo = myConnectionInfo;
                    myTable.ApplyLogOnInfo(myTableLogonInfo);
                }
            }
            finally
            {
                if (con is IDisposable)
                {
                    (con as IDisposable).Dispose();
                }
            }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    public static void SetCurrentValuesForParameterField(ParameterFields myParameterFields, string ParameterField, string submittedValue)
    {
        ParameterValues currentParameterValues = new ParameterValues();
        ParameterDiscreteValue myParameterDiscreteValue = new ParameterDiscreteValue
        {
            Value = submittedValue.ToString()
        };
        currentParameterValues.Add((ParameterValue)myParameterDiscreteValue);
        ParameterField myParameterField = myParameterFields["@" + ParameterField];
        myParameterField.CurrentValues = currentParameterValues;
    }

    public static void SetDBLogonForSubreports(ConnectionInfo myConnectionInfo, ReportDocument myReportDocument, int sMenuID, string pFromDt = "", string pToDt = "", int pLedgerID = -1)
    {
        try
        {
            IEnumerator con = null;
            Sections mySections = myReportDocument.ReportDefinition.Sections;
            try
            {
                con = mySections.GetEnumerator();
                while (con.MoveNext())
                {
                    IEnumerator iEnum = null;
                    Section mySection = (Section)con.Current;
                    ReportObjects myReportObjects = mySection.ReportObjects;
                    try
                    {
                        iEnum = myReportObjects.GetEnumerator();
                        while (iEnum.MoveNext())
                        {
                            ReportObject myReportObject = (ReportObject)iEnum.Current;
                            if (myReportObject.Kind == ReportObjectKind.SubreportObject)
                            {
                                SubreportObject mySubreportObject = (SubreportObject)myReportObject;
                                ReportDocument subReportDocument = mySubreportObject.OpenSubreport(mySubreportObject.SubreportName);
                                SetDBLogonForReport(myConnectionInfo, subReportDocument);
                                Console.WriteLine(mySubreportObject.SubreportName);
                                using (IDataReader dr = DB.GetRS(string.Format("Select * From {0} Where ModuleID = {1} And ReportType = 0 And Reportname = {2} ;", "tbl_ReportList", sMenuID, DB.SQuote(mySubreportObject.SubreportName.ToString().Replace(".rpt", "")))))
                                {
                                    if (dr.Read())
                                    {
                                        if (Localization.ParseBoolean(dr["IsSelectQry"].ToString()))
                                        {
                                            string StrQuery = string.Empty;
                                            string strRptName = dr["ReportName"].ToString();
                                            if ((strRptName == "BalanceSheet_Liabilities") || (strRptName == "BalanceSheet_Assets"))
                                            {
                                                StrQuery = string.Format("{0} '{1}', '{2}', {3}, {4},{5},{6}", new object[] { dr["QueryString"].ToString(), Localization.ToSqlDateString(pFromDt), Localization.ToSqlDateString(pToDt), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID });
                                            }
                                            else if (strRptName == "CompanyHeader" || strRptName == "CompanyHeader2")
                                            {
                                                StrQuery = String.Format("{0} {1}", dr["QueryString"].ToString(), pLedgerID);
                                            }
                                            else if ((strRptName == "rpt_FabricInvoiceAddLess") || (strRptName == "rpt_FabricCreditNoteAddLess") || (strRptName == "rpt_FabricInvoiceAddLess_Book") || (strRptName == "rpt_FabricDebitnoteAddLess") || (strRptName == "rpt_YarnSalesReturnAddLess"))
                                            {
                                                StrQuery = String.Format("{0} {1}", dr["QueryString"].ToString(), pLedgerID);
                                            }
                                            else
                                            {
                                                StrQuery = String.Format("{0} '{1}', '{2}', {3}, {4}, {5},{6},{7}", dr["QueryString"].ToString(), pFromDt, pToDt, pLedgerID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID);
                                            }
                                            subReportDocument.SetDataSource(DB.GetDT(StrQuery, false));
                                        }
                                        else if (mySubreportObject.SubreportName.ToString().Replace(".rpt", "") == "CompanyHeader" || mySubreportObject.SubreportName.ToString().Replace(".rpt", "") == "CompanyHeader2")
                                        {
                                            string StrQuery = string.Empty;
                                            StrQuery = string.Format(" sp_ExecQuery '{0} {1}'", dr["QueryString"].ToString(), Db_Detials.CompID);
                                            subReportDocument.SetDataSource(DB.GetDT(StrQuery.Trim(), false));
                                        }
                                    }
                                }
                            }
                        }
                    }
                    finally
                    {
                        if (iEnum is IDisposable)
                        {
                            (iEnum as IDisposable).Dispose();
                        }
                    }
                }
            }
            finally
            {
                if (con is IDisposable)
                {
                    (con as IDisposable).Dispose();
                }
            }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    public static bool CheckForInternetConnection()
    {
        try
        {
            using (var client = new System.Net.WebClient())
            using (var stream = client.OpenRead("http://www.google.com"))
            { return true; }
        }
        catch
        {
            return false;
        }
    }

    #region SMS
    public static string GetValueForSms(string sTransID, string sMenuID)
    {
        string sTMessage = "";
        string sMessages = "";
        string sMobileNo = "";
        int RefMenuID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select RefMenuID From tbl_MenuMaster Where MenuID=" + sMenuID + "")));
        if (RefMenuID > 0)
        {
            sMenuID = RefMenuID.ToString();
        }
        using (IDataReader idr_Menu = DB.GetRS("SELECT * FROM fn_SMSTemplate_Tbl() Where MenuID=" + sMenuID))
        {
            if (idr_Menu.Read())
            {
                using (IDataReader iDr = DB.GetRS("SELECT * from " + idr_Menu["TemplateQry"].ToString() + "(" + sMenuID + "," + sTransID + ")"))
                {
                    if (iDr.Read())
                    {
                        sTMessage = idr_Menu["Message"].ToString();
                        if (sTMessage.Contains("{ENTRYNO}") == true)
                        {
                            sTMessage = sTMessage.Replace("{ENTRYNO}", iDr["EntryNo"].ToString());
                        }
                        if (sTMessage.Contains("{PARTYNAME}") == true)
                        {
                            sTMessage = sTMessage.Replace("{PARTYNAME}", iDr["PartyName"].ToString());
                        }
                        if (sTMessage.Contains("{PARTICULAR}") == true)
                        {
                            sTMessage = sTMessage.Replace("{PARTICULAR}", iDr["Particular"].ToString());
                        }
                        if (sTMessage.Contains("{OTP}") == true)
                        {
                            sTMessage = sTMessage.Replace("{OTP}", CommonLogic.UnmungeString(iDr["OTP"].ToString()));
                        }
                        if (sTMessage.Contains("{USERNAME}") == true)
                        {
                            sTMessage = sTMessage.Replace("{USERNAME}", iDr["UserName"].ToString());
                        }

                        sMobileNo = iDr["MobileNo"].ToString();
                        sMessages += sTMessage + "";
                    }
                }
            }
        }
        return sMessages + ';' + sMobileNo;
    }

    public static void SendSms(string sTransID, string sMenuID, int TypeID, [Optional, DefaultParameterValue("")] string sPartyID)
    {
        string sMessages = "";
        string sMobileNo = "";

        string strMessage = GetValueForSms(sTransID, sMenuID);
        string[] strApp = strMessage.Split(';');
        sMessages = strApp[0].ToString();
        sMobileNo = strApp[1].ToString();

        if (sMessages.Length > 0)
        {
            sMessages = sMessages.Replace("&", "%26");
            if ((sMobileNo != "") && (sMobileNo != "0"))
            {
                string sRetVal = string.Empty;
                //if (CheckForInternetConnection())
                //    sRetVal = SMSServer.SendSMS(sMobileNo, sMessages, Db_Detials.CompID);
                //else
                sRetVal = "Pending";
                try
                {
                    string[] sRet = sRetVal.Split(';');
                    if (sRet[0].ToString() == "True")
                    {
                        //string sRetMsg = GetSMSStatus(sRet[1].ToString());
                        string sRetMsg = sRet[1].ToString();
                        LogSMSSentStatus(sMenuID, sTransID, sMobileNo, sMessages, sRetMsg, Localization.ParseBoolean(sRet[0].ToString()), sRet[1].ToString(), TypeID);
                    }
                    else
                    {
                        //string sRetMsg = GetSMSStatus(sRet[1].ToString());
                        string sRetMsg = sRet[1].ToString();
                        LogSMSSentStatus(sMenuID, sTransID, sMobileNo, sMessages, sRet[1].ToString(), Localization.ParseBoolean(sRet[0].ToString()), "", TypeID);
                    }
                }
                catch { LogSMSSentStatus(sMenuID, sTransID, sMobileNo, sMessages, sRetVal, false); }
            }
            else
            {
                LogSMSSentStatus(sMenuID, sTransID, sMobileNo, sMessages, "Invalid Mobile No.", false);
            }
        }
    }

    private static string GetSMSStatus(string sRetVal)
    {
        try
        {
            string sVal = DB.GetSnglValue("SELECT GSValue from tbl_GlobalSettings WHERE GType='SMS' AND GSName='SMS_STATUS' AND CompanyForID=" + Db_Detials.CompID);
            sVal = sVal.Replace("{MESSAGEID}", sRetVal);
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(sVal);
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); return sRetVal; }
    }

    private static void LogSMSSentStatus(string sMenuID, string sTransID, string sMobileNos, string sMessage, string sStatusMsg, bool sStatus, string sResponseID = "", int iType = 1)
    {
        try
        {
            DB.ExecuteSQL(string.Format("INSERT INTO tbl_SMSSentDetails VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10},{11}, getdate());",
                sMenuID, sTransID, CommonLogic.SQuote(sMobileNos), CommonLogic.SQuote(sMessage), CommonLogic.SQuote(sStatusMsg), (sStatus == true ? 1 : 0), Convert.ToString(iType), CommonLogic.SQuote(sResponseID),
                        CommonLogic.SQuote(GetIP()), Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID));
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }
    #endregion

    #region Email
    public static void LogEmailSentStatus(string sMenuID, string sTransID, string sEmailIDs, bool blnStatus, string sStatus, bool IsManual, string sEntryNo, int iPartyID, int iVoucherTypeID, int iType)
    {
        try
        {
            DB.ExecuteSQL(string.Format("INSERT INTO tbl_EmailSentDetails VALUES({0}, {1}, {2}, {3}, {4}, 1,  {5}, {6}, {7}, {8},{9},{10},{11},{12}, getdate());",
                    sMenuID, sTransID, CommonLogic.SQuote(sEmailIDs), CommonLogic.SQuote(sStatus.ToString()), (blnStatus == true ? 1 : 0), iType, (IsManual == true ? 1 : 0), Localization.ParseNativeDouble(sEntryNo), iPartyID, iVoucherTypeID,
                    Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID));
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    public static string sendEmail(string sTransID, string sEntryNo, string sPartyID, string sMenuID, bool blnIsUpd = false, int sVoucherTypeID = 0, int iType = 1)
    {
        string _filename = string.Empty;
        string _Password = string.Empty;
        string _Host = string.Empty;
        string _PortNo = string.Empty;
        string _Signature = string.Empty;
        string _FromEmail = string.Empty;
        string _Subject = string.Empty;
        string _MenuID = string.Empty;
        bool _blnIsUpdActive = false;
        bool IsGmail = false;
        string _sEmailID_Update = "";
        string[] sRet = new string[1];
        string sRetMsg = string.Empty;

        IsGmail = Localization.ParseBoolean(GlobalVariables.MAIL_TYPE);

        int ival = 0;
        if (IsGmail)
        { ival = 1; }
        else { ival = 0; }

        //DataGridView dgv_AttachFile = new DataGridView();
        //string sstartPath = "";
        //string sReportName = "";
        string sRetVal = string.Empty;
        string sMenuCaption = DB.GetSnglValue("SELECT Form_Caption from tbl_MenuMaster WHERE MenuID=" + sMenuID + "");
        string sEmailID = DB.GetSnglValue(string.Format("SELECT CASE  WHEN EmailID1 IS NOT NULL AND EmailID1<>'' AND EmailID<>'' AND EmailId IS Not NULL THEN IsNull(EmailID+'','')+''+isnull(EmailID1,'') WHEN EmailID1 IS NULL THEN EmailID WHEN EmailId IS NULL THEN EmailId1 ELSE '' END AS EmailID from fn_LedgerMaster_all() WHERE LEdgerID=" + sPartyID));
        
        try
        {
            string[] sRetn = sRetVal.Split(';');
            if (sRetn[0].ToString() == "True")
            {
                // string sRetnMsg = sRetn[1].ToString();
                string sRetnMsg = "Pending";
                LogEmailSentStatus(sMenuID, sTransID, sEmailID, blnIsUpd, sRetnMsg, false, sEntryNo, Localization.ParseNativeInt(sPartyID), sVoucherTypeID, iType);
            }
            else
            {
                // string sRetnMsg = sRetn[1].ToString();
                string sRetnMsg = "Pending";
                LogEmailSentStatus(sMenuID, sTransID, sEmailID, blnIsUpd, sRetnMsg, false, sEntryNo, Localization.ParseNativeInt(sPartyID), sVoucherTypeID, iType);
            }
        }
        catch { LogEmailSentStatus(sMenuID, sTransID, sEmailID, blnIsUpd, sRetVal, false, sEntryNo, Localization.ParseNativeInt(sPartyID), sVoucherTypeID, iType); }

        return sRetVal;
    }
    #endregion

    public static bool GridDeleteVld(string sMenuID)
    {
        try
        {
            string strGValue = DB.GetSnglValue(string.Format("Select GSValue From tbl_GlobalSettings Where GsName='FAB_DELDG_IN_EH' and CompanyForID=" + Db_Detials.CompID + ""));
            string[] rtval = strGValue.Split(',');

            foreach (string sval in rtval)
            {
                if (sval == sMenuID)
                {
                    return true;
                }
            }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return false;
        }
        return false;
    }

    public static bool IsPartyBlocked(int partyID)
    {
        try
        {
            int IGValue = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Count(0) from tbl_LedgerBlocked Where CompID=" + Db_Detials.CompID + " and LedgerId=" + partyID + "")));
            if (IGValue > 0)
            {
                return true;
            }
            else { return false; }
        }
        catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); return false; }
    }

    #region  Num2WOrd
    //Added By Santosh
    public static string NumbersToWords(int inputNumber)
    {
        int inputNo = inputNumber;

        if (inputNo == 0)
            return "Zero";

        int[] numbers = new int[4];
        int first = 0;
        int u, h, t;
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        if (inputNo < 0)
        {
            sb.Append("Minus ");
            inputNo = -inputNo;
        }

        string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
            "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
        string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
            "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
        string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
            "Seventy ","Eighty ", "Ninety "};
        string[] words3 = { "Thousand ", "Lakh ", "Crore " };

        numbers[0] = inputNo % 1000; // units
        numbers[1] = inputNo / 1000;
        numbers[2] = inputNo / 100000;
        numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
        numbers[3] = inputNo / 10000000; // crores
        numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs

        for (int i = 3; i > 0; i--)
        {
            if (numbers[i] != 0)
            {
                first = i;
                break;
            }
        }
        for (int i = first; i >= 0; i--)
        {
            if (numbers[i] == 0) continue;
            u = numbers[i] % 10; // ones
            t = numbers[i] / 10;
            h = numbers[i] / 100; // hundreds
            t = t - 10 * h; // tens
            if (h > 0) sb.Append(words0[h] + "Hundred ");
            if (u > 0 || t > 0)
            {
                if (h > 0 || i == 0) sb.Append("and ");
                if (t == 0)
                    sb.Append(words0[u]);
                else if (t == 1)
                    sb.Append(words1[u]);
                else
                    sb.Append(words2[t - 2] + words0[u]);
            }
            if (i != 0) sb.Append(words3[i - 1]);
        }
        return sb.ToString().TrimEnd();
    }

    public static string DateFormat(DateTime dt, string strFormat)
    {
        int day = dt.Day;
        int month = dt.Month;
        int year = dt.Year;

        DateTime r = new DateTime(year, month, day, 0, 0, 0, 123);

        string strDate = String.Format("{0:" + strFormat + "}", r);
        return strDate;

    }

    public static string GenUniqueID()
    {
        string strOtp = string.Empty;
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz@$";
        Random random = new Random();
        string result = new string(
        Enumerable.Repeat(chars, 10)
                  .Select(s => s[random.Next(s.Length)])
                  .ToArray());
        strOtp = result.ToString();
        return strOtp;
    }
    #endregion

    #region StockAdj

    public static string GetAdjColName(int MenuID, double Entity_IsfFtr)
    {
        int ibitCol = 0;
        string strQry_ColName = string.Empty;
        DataTable dT = DB.GetDT(string.Format(" sp_ExecQuery 'select * from {0} Where GridID = {1} And GridType = {2} Order By ColIndex'", "tbl_GridFields_Mapping", MenuID, Entity_IsfFtr), false);

        for (int i = 0; i <= dT.Rows.Count - 1; i++)
        {
            if (dT.Rows[i]["ColDataType"].ToString() == "B")
            {
                ibitCol = i;
                strQry_ColName = strQry_ColName + " CAST('False' as Bit) As [" + dT.Rows[i]["ColHeading"].ToString() + "], ";
            }
            else if (Localization.ParseBoolean(dT.Rows[i]["IsInQuery"].ToString()))
            {
                strQry_ColName = strQry_ColName + dT.Rows[i]["ColFields"].ToString() + " As [" + dT.Rows[i]["ColHeading"].ToString() + "], ";
            }
            else
            {
                strQry_ColName = strQry_ColName + "'' As [" + dT.Rows[i]["ColHeading"].ToString() + "], ";
            }
        }

        if (strQry_ColName.Length != 0)
        {
            strQry_ColName = Localization.Left(strQry_ColName, 2);
        }
        return strQry_ColName + ";" + ibitCol.ToString();
    }
    #endregion

    public static bool CheckConnection()
    {
        try
        {
            using (SqlConnection dbconn = new SqlConnection())
            {
                dbconn.ConnectionString = DB.GetDBConn();
                dbconn.Open();
                return true;
            }
        }
        catch
        {
            return false;
        }
    }


    public static AutoCompleteStringCollection AutoCompleteText(string TableName, string ColumnName, ref CIS_Textbox TextBox)
    {
        TextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
        TextBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
        AutoCompleteStringCollection NewCollection = new AutoCompleteStringCollection();

        try
        {
            using (IDataReader idr = DB.GetRS(string.Format("Select Distinct " + ColumnName + " From " + TableName + " where IsDeleted=0")))
            {
                while (idr.Read())
                {
                    NewCollection.Add(idr.GetString(0));
                }
            }
        }
        catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }

        TextBox.AutoCompleteCustomSource = NewCollection;
        return NewCollection;
    }
}

