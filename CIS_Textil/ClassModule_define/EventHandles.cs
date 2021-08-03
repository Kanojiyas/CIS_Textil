using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_DBLayer;
using CIS_Evaluator;
using CIS_Utilities;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

public class EventHandles
{
    #region "Grid Events"

    public static void fgDtls_CellEndEdit(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
    {
        object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
        dynamic objfrm = objectValue;
        if (((objectValue != null) && !(objectValue.ToString().Contains("frmDashBoardPack") | objectValue.ToString().Contains("frmDashBoard"))) && ((((DataTable)objfrm.dt_HasDtls_Grd).Rows.Count != 0) && ((Localization.ParseNativeInt(objfrm.blnFormAction.ToString()) == 1) | (Localization.ParseNativeInt(objfrm.blnFormAction.ToString()) == 0))))
        {
            DataTable table = (DataTable)objfrm.dt_HasDtls_Grd;
            DataTable table2 = (DataTable)objfrm.dt_AryCalcvalue;
            DataTable table3 = (DataTable)objfrm.dt_AryIsRequired;
            _CellEndEidt(RuntimeHelpers.GetObjectValue(sender), e, ref table, ref table2, ref table3);
        }
    }

    //public static void  HorizontalScrollBar_ValueChanged(object sender, EventArgs e)
    //{
    //    HScrollBarValueChanged(HorizontalScrollBar, EventArgs.Empty);
    //}

    public static void fgDtls_CellParsing(object sender, System.Windows.Forms.DataGridViewCellParsingEventArgs e)
    {
        object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
        dynamic objfrm = objectValue;
        if ((objectValue != null) && !((Localization.ParseNativeInt(objfrm.blnFormAction.ToString()) != 1) | (Localization.ParseNativeInt(objfrm.blnFormAction.ToString()) == 0)))
        {
            DataTable table = (DataTable)objfrm.dt_HasDtls_Grd;
            DataTable table2 = (DataTable)objfrm.dt_AryCalcvalue;
            DataTable table3 = (DataTable)objfrm.dt_AryIsRequired;
            _CellParsing(RuntimeHelpers.GetObjectValue(sender), e, ref table, ref table2, ref table3);
        }
    }

    public static void fgDtls_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
    {
        object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
        dynamic objfrm = objectValue;
        if ((objectValue != null) && ((Localization.ParseNativeInt(objfrm.blnFormAction.ToString()) == 1) | (Localization.ParseNativeInt(objfrm.blnFormAction.ToString()) == 0)))
        {
            DataTable table = (DataTable)objfrm.dt_HasDtls_Grd;
            _EditingControlShowing(RuntimeHelpers.GetObjectValue(sender), e, ref table);
        }
    }

    public static void fgDtls_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
    {
        try
        {
            object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
            dynamic objfrm = objectValue;
            if (objfrm != null)
            {
                DataTable table = (DataTable)objfrm.dt_HasDtls_Grd;
                if (((objectValue != null) && !(objectValue.ToString().Contains("CIS_Textil.frmDashBoard") | objectValue.ToString().Contains("CIS_Textil.frmDashBoardPack"))) && ((((DataTable)objfrm.dt_HasDtls_Grd).Rows.Count != 0) && ((Localization.ParseNativeInt(objfrm.blnFormAction.ToString()) == 1) | (Localization.ParseNativeInt(objfrm.blnFormAction.ToString()) == 0))))
                {
                    _CellValidating(RuntimeHelpers.GetObjectValue(sender), e, ref table);
                }
            }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    public static void fgDtls_DataError(object sender, System.Windows.Forms.DataGridViewDataErrorEventArgs e)
    {
        e.Cancel = true;
    }

    public static void fgDtls_CellEnter(object sender, DataGridViewCellEventArgs e)
    {
        object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
        dynamic objfrm = objectValue;
        CIS_DataGridViewEx.DataGridViewEx fgDtlst = (CIS_DataGridViewEx.DataGridViewEx)sender;
        if (objfrm != null)
        {
            try
            {
                if (fgDtlst.CurrentCell.OwningColumn.ToolTipText != "")
                {
                    objfrm.lblHelpText.Text = fgDtlst.CurrentCell.OwningColumn.ToolTipText;
                }
                else
                {
                    DataTable table = (DataTable)objfrm.dt_HasDtls_Grd;
                    DataRow[] rst = table.Select("ColIndex=" + fgDtlst.CurrentCell.ColumnIndex);
                    if (rst.Length > 0)
                    {
                        foreach (DataRow r in rst)
                        {
                            objfrm.lblHelpText.Text = r["ToolTip"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
    }

    public static void fgdtls_ColumnHeaderMouseClick(object sender, System.Windows.Forms.DataGridViewCellMouseEventArgs e)
    {
        try
        {
            if (e.Button == MouseButtons.Right)
            {
                CIS_DataGridViewEx.DataGridViewEx fgdtls = (CIS_DataGridViewEx.DataGridViewEx)sender;
                Form instance = (Form)RuntimeHelpers.GetObjectValue(Navigate.GetForm_byName("MDIMain"));
                CIS_Textil.CIS_GridSettings frm = new CIS_Textil.CIS_GridSettings();
                frm.SubGridID = fgdtls.Grid_UID;
                frm.Dock = DockStyle.None;
                frm.SetMDIform = instance.Name;
                instance.Controls.Add(frm);
            }
            else if (e.Button == MouseButtons.Left)
            {
                CIS_DataGridViewEx.DataGridViewEx fgdtls = (CIS_DataGridViewEx.DataGridViewEx)sender;
                for (int i = 0; i < fgdtls.ColumnCount; i++)
                {
                    if (i != e.ColumnIndex)
                    {
                        if (fgdtls.Columns[i].Visible)
                        {
                            Control[] CntlArry1 = fgdtls.Controls.Find("txt" + i, false);
                            foreach (Control c in CntlArry1)
                            {
                                TextBox txt = (TextBox)c;
                                txt.Visible = false;
                            }
                        }
                    }
                }

                Control[] CntlArry = fgdtls.Controls.Find("txt" + e.ColumnIndex, false);
                foreach (Control c in CntlArry)
                {
                    TextBox txt = (TextBox)c;
                    if (txt.Visible)
                    {
                        txt.Visible = false;
                    }
                    else
                    {
                        txt.Visible = true;
                        txt.Text = "";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    public static void fgDtls_KeyDown(object sender, KeyEventArgs e)
    {
        object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
        dynamic frmobjectValue = objectValue;
        //frmobjectValue.ShowStock();
        try
        {
            if (e.KeyCode == Keys.Enter)
            {
                CIS_DataGridViewEx.DataGridViewEx fgDtls = (CIS_DataGridViewEx.DataGridViewEx)sender;
                {
                    for (int i = fgDtls.CurrentCell.ColumnIndex + 1; i <= fgDtls.ColumnCount - 1; i++)
                    {
                        int iOrder = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select ColOrder From tbl_GridSettings Where GridID=" + frmobjectValue.iIDentity + " and  SubGridID=" + fgDtls.Grid_UID + " and  ColOrder=" + (fgDtls.CurrentCell.OwningColumn.DisplayIndex) + "")));
                        int iIndex = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select ColIndex From tbl_GridSettings Where GridID=" + frmobjectValue.iIDentity + " and  SubGridID=" + fgDtls.Grid_UID + " and  ColOrder=" + (iOrder + 1) + "")));

                        if (fgDtls.CurrentRow.Cells[iIndex].Visible == true)
                        {
                            e.SuppressKeyPress = true;
                            int iColumn = fgDtls.CurrentCell.ColumnIndex;
                            int iRow = fgDtls.CurrentCell.RowIndex;
                            if (iColumn == fgDtls.Columns.Count - 1)
                            {
                                fgDtls.CurrentCell = fgDtls[0, iRow + 1];
                            }
                            else
                            {
                                if (fgDtls.CurrentRow.Cells[iIndex].Visible == true)
                                {
                                    fgDtls.CurrentCell = fgDtls[iIndex, iRow];
                                }
                                else
                                {
                                    int iCol1 = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select ColOrder From tbl_GridSettings Where GridID=" + frmobjectValue.iIDentity + "  and  SubGridID=" + fgDtls.Grid_UID + " and  ColIndex=" + (iIndex) + "")));
                                    fgDtls.CurrentCell = fgDtls[iCol1 + 1, iRow];
                                }
                            }
                            break;
                        }
                    }
                }
                if (Convert.ToString(fgDtls.CurrentRow.Cells[fgDtls.CurrentCell.ColumnIndex].Tag) == "YES")
                {
                    frmobjectValue.ShowStock();
                }
            }
            else if ((e.Control == true & e.KeyCode == Keys.D) | e.KeyCode == Keys.F5)
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

                bool vlddelete = (CommonCls.GridDeleteVld(frmObj.iIDentity.ToString()));
                //Localization.ParseNativeInt(sRefID) > 0

                if (!vlddelete)
                {
                    if ((GlobalVariables.VALIDATE_EDIT == "TRUE") && sRefID != "" && (sRefID) != "0" && (sRefID) != "-" && (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_StockFabricLedger() WHERE RefID='" + sRefID + "' AND TransType<>" + frmObj.iIDentity + "")) > 0))
                    {
                        Navigate.ShowMessage(CIS_DialogIcon.SecurityError, "Referance Found", "Referance Found in Other Transaction, This Row Cannot be Delete...");
                        return;
                    }
                    else
                    {
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
                            CreateDefault_Rows(fgDtls, table2, table3, table4, false, false);
                        }
                        else
                        {
                            CreateDefault_Rows(fgDtls, table2, table3, table4, true, false);
                        }
                    }
                }
            }
            else if ((e.Control == true & e.KeyCode == Keys.T))
            {

            }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    public static void fgDtls_KeyPress(object sender, KeyPressEventArgs e)
    {

    }

    public static void fgDtls_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        try
        {

        }
        catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
    }

    public static void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {

    }

    public static void _CellEndEidt(object sender, DataGridViewCellEventArgs e, ref DataTable dt_HasDtls, ref DataTable dt_AryCalcvalue, ref DataTable dt_AryIsRequired)
    {
        try
        {
            object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
            dynamic objfrm = objectValue;
            if ((Localization.ParseNativeInt(objfrm.blnFormAction.ToString()) == 1) | (Localization.ParseNativeInt(objfrm.blnFormAction.ToString()) == 0))
            {
                DataGridViewEx ex2 = (DataGridViewEx)sender;
                DataRow[] rowArray = dt_AryCalcvalue.Select("SubGridID = " + Convert.ToString(ex2.Grid_UID));
                int num3 = rowArray.Length - 1;
                for (int i = 0; i <= num3; i++)
                {
                    string[] strArray2 = rowArray[i]["ColCalcValue"].ToString().Split(new char[] { ',' });
                    string str = string.Empty;
                    string[] strArray = null;
                    int num4 = strArray2.Length - 1;
                    for (int j = 0; j <= num4; j++)
                    {
                        if (j == (strArray2.Length - 1))
                        {
                            strArray = strArray2[j].ToString().Split(new char[] { ';' });
                            if (Versioned.IsNumeric(strArray[0]))
                            {
                                if (e != null)
                                {
                                    str = str + Conversions.ToString(Localization.ParseNativeDouble(ex2.Rows[e.RowIndex].Cells[Localization.ParseNativeInt(strArray[0].ToString())].FormattedValue.ToString()));
                                }
                            }
                            else
                            {
                                str = str + strArray[0].ToString();
                            }
                        }
                        else if (Versioned.IsNumeric(strArray2[j]))
                        {
                            if (e != null)
                            {
                                str = str + ex2.Rows[e.RowIndex].Cells[Localization.ParseNativeInt(strArray2[j].ToString())].FormattedValue.ToString();
                            }
                        }
                        else
                        {
                            str = str + strArray2[j].ToString();
                        }
                    }
                    try
                    {
                        if (str == "+")
                        {
                            if (e != null)
                            {
                                ex2.Rows[e.RowIndex].Cells[Localization.ParseNativeInt(rowArray[i]["ColIndex"].ToString())].Value = e.RowIndex + 1;
                            }
                        }
                        else if (str == "#")
                        {
                            if (e != null)
                            {
                                Console.Write(RuntimeHelpers.GetObjectValue(ex2.Rows[e.RowIndex].Cells[Localization.ParseNativeInt(rowArray[i]["ColIndex"].ToString())].Value));
                                if (ex2.Rows[e.RowIndex].Cells[Localization.ParseNativeInt(rowArray[i]["ColIndex"].ToString())].Value == null)
                                {
                                    if (e.RowIndex > 0)
                                    {
                                        ex2.Rows[e.RowIndex].Cells[Localization.ParseNativeInt(rowArray[i]["ColIndex"].ToString())].Value = Localization.AutoInc(Conversions.ToString(ex2.Rows[e.RowIndex - 1].Cells[Localization.ParseNativeInt(rowArray[i]["ColIndex"].ToString())].Value));
                                    }
                                    else if (e.RowIndex == 0)
                                    {
                                        string str3 = dt_HasDtls.Select("SubGridID = " + ex2.Grid_UID + " And ColIndex = " + Conversions.ToString(e.ColumnIndex))[0]["ColFields"].ToString();
                                        string snglValue = DB.GetSnglValue(string.Format("Select MAX({0}) As SelCol From {1}", str3, ex2.Grid_Tbl));
                                        if (snglValue == "")
                                        {
                                            ex2.Rows[e.RowIndex].Cells[Localization.ParseNativeInt(rowArray[i]["ColIndex"].ToString())].Value = 1;
                                        }
                                        else
                                        {
                                            ex2.Rows[e.RowIndex].Cells[Localization.ParseNativeInt(rowArray[i]["ColIndex"].ToString())].Value = Localization.AutoInc(snglValue);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (e != null)
                            {
                                double dblValue = Evaluator.EvalToDouble(str.Replace(",", ""));
                                if (dblValue > 0)
                                    ex2.Rows[e.RowIndex].Cells[Localization.ParseNativeInt(rowArray[i]["ColIndex"].ToString())].Value = dblValue;
                                else
                                    ex2.Rows[e.RowIndex].Cells[Localization.ParseNativeInt(rowArray[i]["ColIndex"].ToString())].Value = 0;
                            }
                        }
                    }
                    catch
                    {
                    }
                }
                DataGridViewEx fgDtls = (DataGridViewEx)sender;
                CreateDefault_Rows(fgDtls, dt_HasDtls, dt_AryCalcvalue, dt_AryIsRequired, true, false);
                ex2 = null;
            }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Error", ex.Message);
        }
    }

    public static void _CellParsing(object sender, DataGridViewCellParsingEventArgs e, ref DataTable dt_HasDtls, ref DataTable dt_AryCalcvalue, ref DataTable dt_AryIsRequired)
    {
        object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
        dynamic objfrm = objectValue;
        if ((Localization.ParseNativeInt(objfrm.blnFormAction.ToString()) == 1) | (Localization.ParseNativeInt(objfrm.blnFormAction.ToString()) == 0))
        {
            DataGridViewEx ex2 = (DataGridViewEx)sender;

            DataRow[] rowArray = dt_HasDtls.Select("SubGridID = " + Conversions.ToString(ex2.Grid_UID));
            int num3 = rowArray.Length - 1;
            for (int i = 1; i <= num3; i++)
            {
                string str2 = rowArray[i]["ColDataType"].ToString();
                string str = rowArray[i]["ColDataFormat"].ToString();
                if (ex2.CurrentCell.ColumnIndex < i)
                {
                    break;
                }
                if (ex2.CurrentCell.ColumnIndex == i)
                {
                    if (str2.ToUpper() == "S")
                    {
                        string expression = (string)e.Value;
                        try
                        {
                            if (e.Value.ToString().Contains("/"))
                            {
                                if (!Information.IsDate(expression))
                                {
                                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Invalid date format.");
                                }
                            }
                            else
                            {
                                string str4 = expression.Substring(0, 2);
                                string str5 = expression.Substring(2, 2);
                                string str6 = expression.Substring(4, 4);
                                DateTime time = new DateTime(Convert.ToInt32(str6), Convert.ToInt32(str5), Convert.ToInt32(str4));
                                e.Value = time;
                                e.ParsingApplied = true;
                            }
                        }
                        catch
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Invalid date format.");
                        }
                        break;
                    }
                    if (str2.ToUpper() == "D")
                    {
                        double num2 = Conversion.Val(RuntimeHelpers.GetObjectValue(e.Value));
                        if (decimal.Compare(Localization.ParseNativeDecimal(Conversion.Str(2).ToString()), decimal.Zero) != 0)
                        {
                            e.Value = string.Format("{0:N" + str + "}", num2);
                        }
                        else
                        {
                            e.Value = num2;
                        }
                        e.ParsingApplied = true;
                        break;
                    }
                }
            }
            ex2 = null;
        }
    }

    public static void _EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e, ref DataTable dt_HasDtls)
    {
        try
        {
            e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
            DataGridViewEx ex = (DataGridViewEx)sender;
            DataRow[] rowArray = dt_HasDtls.Select("SubGridID = " + Conversions.ToString(ex.Grid_UID));
            int num2 = rowArray.Length - 1;
            for (int i = 0; i <= num2; i++)
            {
                string str = rowArray[i]["ColDataType"].ToString();
                if ((i == ex.CurrentCell.ColumnIndex) && (str.ToString() == "C"))
                {
                    if (e.Control is ComboBox)
                    {
                        (e.Control as ComboBox).AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        (e.Control as ComboBox).AutoCompleteSource = AutoCompleteSource.ListItems;
                        (e.Control as ComboBox).DropDownStyle = ComboBoxStyle.DropDown;

                        if ((ex.Rows[ex.CurrentRow.Index].Cells[ex.CurrentCell.ColumnIndex].Value == null))
                        {
                            (e.Control as ComboBox).SelectedIndex = -1;
                        }
                        else if (ex.Rows[ex.CurrentRow.Index].Cells[ex.CurrentCell.ColumnIndex].Value.ToString() == "")
                        {
                            (e.Control as ComboBox).SelectedIndex = -1;
                        }
                    }
                    else { (e.Control as ComboBox).AutoCompleteMode = AutoCompleteMode.None; }
                }
            }


        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    public static void Control_KeyPress(object sender, KeyPressEventArgs e)
    {
        try
        {
            int ascii = Convert.ToInt32(e.KeyChar);
            object activeChild = (Form)Navigate.GetActiveChild();
            Form frm = (Form)activeChild;
            Control cntl = frm.ActiveControl;
            ComboBox editingControl;
            CIS_DataGridViewEx.DataGridViewEx sCbo = (CIS_DataGridViewEx.DataGridViewEx)cntl.Parent.Parent;
            editingControl = (ComboBox)sCbo.EditingControl;
            if (ascii == 32)
            {
                if (editingControl.Text.Trim().Length == 0)
                {
                    editingControl.DroppedDown = true;
                    editingControl.Text = "";
                    editingControl.Text.Trim();
                }
            }
            else if (editingControl.Text.Trim().Length > 0)
            {
                editingControl.SelectedIndex = -1;
            }
        }
        catch (Exception ex)
        {
            //Navigate.logError(ex.Message, ex.StackTrace);
        }


    }

    public static void _CellValidating(object sender, DataGridViewCellValidatingEventArgs e, ref DataTable dt_HasDtls)
    {
        try
        {
            DataGridViewEx sCbo = (DataGridViewEx)sender;
            DataRow[] rowArray = dt_HasDtls.Select("SubGridID = " + Conversions.ToString(sCbo.Grid_UID));
            int num2 = rowArray.Length - 1;
            object activeChild = (Form)Navigate.GetActiveChild();
            for (int i = 0; i <= num2; i++)
            {
                if (i == sCbo.CurrentCell.ColumnIndex)
                {
                    if (rowArray[i]["ColDataType"].ToString() == "C")
                    {
                        ComboBox editingControl = (ComboBox)sCbo.EditingControl;
                        if (editingControl.Text.Trim().Length != 0)
                        {
                            editingControl.SelectedIndex = editingControl.FindStringExact(editingControl.Text.Trim());
                            if (editingControl.SelectedValue == null)
                            {
                                e.Cancel = true;
                                return;
                            }
                        }
                        DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)sCbo.Rows[e.RowIndex].Cells[e.ColumnIndex];
                        cell.Value = RuntimeHelpers.GetObjectValue(editingControl.SelectedValue);
                    }
                    else if (((rowArray[i]["ColDataType"].ToString() == "I") | (rowArray[i]["ColDataType"].ToString() == "D")) && !string.IsNullOrEmpty(Conversions.ToString(e.FormattedValue)))
                    {
                        if (!Versioned.IsNumeric(RuntimeHelpers.GetObjectValue(e.FormattedValue)))
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Not Valid Data", "Only Numbers are allowed.");
                            e.Cancel = true;
                        }
                        else
                        {
                            e.Cancel = false;
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

    public static void CreateDefault_Rows(DataGridViewEx fgDtls, DataTable dt_HasDtls, DataTable dt_AryCalcvalue, DataTable dt_AryIsRequired, [Optional, DefaultParameterValue(false)] bool blnCopyRow, [Optional, DefaultParameterValue(false)] bool IsBypass)
    {
        try
        {
            if (fgDtls != null)
            {
                if (fgDtls.ColumnCount > 1)
                {
                    if (!blnCopyRow && (fgDtls.RowCount != 0))
                    {
                        fgDtls.RowCount = 0;
                    }
                    if (!IsBypass && !IsRequiredInGrid(fgDtls, dt_AryIsRequired, true))
                    {
                        return;
                    }
                    fgDtls.Rows.Add();

                    if (fgDtls.CurrentCell != null)
                    {
                        fgDtls.CurrentCell = fgDtls[fgDtls.CurrentCell.ColumnIndex, fgDtls.CurrentRow.Index];
                    }
                    DataRow[] dtRow = dt_AryCalcvalue.Select("SubGridID = " + Conversions.ToString(fgDtls.Grid_UID));
                    int num4 = dtRow.Length - 1;
                    for (int i = 0; i <= num4; i++)
                    {
                        string[] strArray = dtRow[i]["ColCalcValue"].ToString().Split(new char[] { ',' });
                        int num5 = strArray.Length - 1;
                        for (int k = 0; k <= num5; k++)
                        {
                            if (k == (strArray.Length - 1))
                            {
                                if (strArray[k].ToString() != "+")
                                {
                                    break;
                                }
                                if (fgDtls.Rows.Count == 1)
                                {
                                    try
                                    {
                                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[Localization.ParseNativeInt(dtRow[i]["ColIndex"].ToString())].Value = 1;
                                    }
                                    catch { }
                                }
                                else
                                {
                                    try
                                    {
                                        fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[Localization.ParseNativeInt(dtRow[i]["ColIndex"].ToString())].Value = Operators.AddObject(fgDtls.Rows[fgDtls.Rows.Count - 2].Cells[Localization.ParseNativeInt(dtRow[i]["ColIndex"].ToString())].Value, 1);
                                    }
                                    catch { }

                                    try
                                    {
                                        fgDtls.CurrentCell = fgDtls[Localization.ParseNativeInt(dtRow[i]["ColIndex"].ToString().ToString()), fgDtls.CurrentRow.Index];
                                    }
                                    catch { }
                                }
                            }

                        }
                    }

                    DataRow[] dtRow_h = dt_HasDtls.Select("SubGridID = " + Conversions.ToString(fgDtls.Grid_UID));
                    DataRow[] dtRow_Col = dt_HasDtls.Select("SubGridID = " + Conversions.ToString(fgDtls.Grid_UID));
                    DataRow[] dtRow_req = dt_AryIsRequired.Select("SubGridID = " + Conversions.ToString(fgDtls.Grid_UID));
                    int iDone = 0;
                    for (int j = 0; j <= (dtRow_h.Length - 1); j++)
                    {
                        if (((dtRow_req.Length > 0) && (fgDtls.Rows.Count > 1)) && (dtRow_req[j]["IsRepeatRow"].ToString() == "True"))
                        {
                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[Localization.ParseNativeInt(dtRow_req[j]["ColIndex"].ToString())].Value = RuntimeHelpers.GetObjectValue(fgDtls.Rows[fgDtls.Rows.Count - 2].Cells[Localization.ParseNativeInt(dtRow_req[j]["ColIndex"].ToString())].Value);
                            iDone = 1;
                        }
                        if (dtRow_h[j]["ColDataType"].ToString() == "M")
                        {
                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[j].Value = CIS_Textil.Properties.Resources.AdjAmt;
                        }
                        else if ((iDone == 0) && (blnCopyRow == false) && dtRow_h[j]["ColDataType"].ToString() == "B")
                        {
                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[j].Value = false;
                        }
                        else if ((iDone == 0) && (blnCopyRow == false) && dtRow_h[j]["ColDataType"].ToString() == "I")
                        {
                            try
                            {
                                if (fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[j].Value == null)
                                    fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[j].Value = 0;
                            }
                            catch { }
                        }

                        else if ((iDone == 0) && (blnCopyRow == false) && dtRow_h[j]["ColDataType"].ToString() == "D")
                        {
                            if (fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[j].Value == null)
                                fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[j].Value = 0;
                        }
                        else if ((iDone == 0) && (blnCopyRow == false))
                        {
                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[j].Value = "";
                        }
                        else if ((iDone == 0) && (blnCopyRow == true) && (IsBypass == true) && dtRow_h[j]["ColDataType"].ToString() == "B")
                        {
                            fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[j].Value = false;
                        }
                        else if ((iDone == 0) && ((blnCopyRow == true) && (IsBypass == true) && dtRow_h[j]["ColDataType"].ToString() == "I"))
                        {
                            if (fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[j].Value == null)
                                fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[j].Value = 0;
                        }

                        else if ((iDone == 0) && (blnCopyRow == true) && (IsBypass == true) && dtRow_h[j]["ColDataType"].ToString() == "D")
                        {
                            if (fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[j].Value == null)
                                fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[j].Value = 0;
                        }
                        else if ((iDone == 0) && (blnCopyRow == true) && (IsBypass == true))
                        {
                            if (fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[j].Value == null)
                                fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[j].Value = "";
                        }
                    }

                    for (int j = 0; j <= (dtRow_Col.Length - 1); j++)
                    {
                        if (dtRow_Col[j]["ColFields"].ToString() == "StoreLocationID")
                        {
                            if (Localization.ParseBoolean(GlobalVariables.FAB_RACKWISE) == true || Localization.ParseBoolean(GlobalVariables.FAB_SERIALWISE) == true)
                            {
                                fgDtls.Columns[j].Visible = true;
                            }
                            else
                            {
                                fgDtls.Columns[j].Visible = false;
                            }
                        }
                        if (dtRow_Col[j]["ColFields"].ToString() == "FabricID")
                        {
                            if (Localization.ParseBoolean(GlobalVariables.FAB_SERIALWISE) == true)
                            {
                                fgDtls.Columns[j].Visible = true;
                            }
                            else
                            {
                                fgDtls.Columns[j].Visible = false;
                            }
                        }
                        fgDtls.Columns[j].DisplayIndex = Localization.ParseNativeInt(Convert.ToString(dtRow_Col[j]["ColOrder"]));
                        //if( fgDtls.Columns[]

                        if (fgDtls.Columns[j].DisplayIndex == 1)
                        {
                            int iCol = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select ColIndex From tbl_GridSettings Where GridID=" + fgDtls.Grid_ID + " and SubGridID=" + fgDtls.Grid_UID + " and  ColOrder=1"))); ;
                            fgDtls.Columns[iCol].Frozen = true;
                        }

                    }
                }
            }
        }
        catch (Exception ex)
        { Navigate.logError(ex.Message, ex.StackTrace); }
    }

    public static void CalculateFooter_Rows(DataGridViewEx fgDtls, DataGridView fgDtlsFooter, string iIDentity, int Grid_UID)
    {
        try
        {
            using (DataTable dtRow_h = DB.GetDT(string.Format("Select * From {0} Where GridID = {1} And SubGridID = {2}", Db_Detials.tbl_GridSettings, iIDentity, Grid_UID), false))
            {
                if (dtRow_h.Rows.Count > 0)
                {
                    if (fgDtlsFooter.ColumnCount > 1)
                    {
                        for (int Col = 0; Col <= fgDtls.Columns.Count - 1; Col++)
                        {
                            if (fgDtls.Columns[Col].ValueType == typeof(double) || fgDtls.Columns[Col].ValueType == typeof(int) || fgDtls.Columns[Col].ValueType == typeof(decimal))
                            {
                                if (Localization.ParseBoolean(dtRow_h.Rows[Col]["SumCols"].ToString()) == true)
                                {
                                    object obj = fgDtls.Rows[0].Cells[Col].Value;
                                    if (obj != null || obj != DBNull.Value)
                                    {
                                        if (obj != null && obj.ToString() != "")
                                        {
                                            fgDtlsFooter.Rows[0].Cells[Col].Value = string.Format("{0:N2}", CommonCls.GetColSum(fgDtls, Col, -1, -1));
                                        }
                                        else
                                        {
                                            fgDtlsFooter.Rows[0].Cells[Col].Value = "";
                                        }
                                    }
                                    else { fgDtlsFooter[Col, 0].Value = ""; }
                                }
                                else { fgDtlsFooter[Col, 0].Value = ""; }
                            }
                            else { fgDtlsFooter[Col, 0].Value = ""; }
                            fgDtlsFooter.Columns[Col].DisplayIndex = Localization.ParseNativeInt(Convert.ToString(dtRow_h.Rows[Col]["ColOrder"]));

                            if (fgDtlsFooter.Columns[Col].DisplayIndex == 1)
                            {
                                int iCol = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select ColIndex From tbl_GridSettings Where GridID=" + fgDtls.Grid_ID + " and SubGridID=" + fgDtls.Grid_UID + " and  ColOrder=1"))); ;
                                fgDtlsFooter.Columns[iCol].Frozen = true;
                                fgDtlsFooter.Rows[0].Cells[iCol].Value = "SUM";
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        { Navigate.logError(ex.Message, ex.StackTrace); }
    }

    public static bool IsRequiredInGrid(DataGridViewEx fgDtls, DataTable dt_AryIsRequired, [Optional, DefaultParameterValue(false)] bool blnIsAddRow)
    {
        bool flag;
        try
        {
            ArrayList AryIsReq = new ArrayList();
            DataRow[] dtRow = dt_AryIsRequired.Select("SubGridID = " + Conversions.ToString(fgDtls.Grid_UID));

            DataGridViewEx ex = fgDtls;
            if ((ex.ColumnCount > 1) && (ex.Rows.Count >= 1))
            {
                for (int j = 0; j <= (dtRow.Length - 1); j++)
                {
                    if (dtRow[j]["IsRequired"].ToString() == "True")
                    {
                        string strContent = Conversions.ToString(ex.Rows[ex.Rows.Count - 1].Cells[Localization.ParseNativeInt(dtRow[j]["ColIndex"].ToString())].FormattedValue);
                        if (Versioned.IsNumeric(strContent))
                        {
                            if (Conversion.Val(strContent) < 0.0)
                            {
                                if (blnIsAddRow)
                                    return false;

                                AryIsReq.Add("0");
                            }
                            else
                                AryIsReq.Add("1");
                        }
                        else if (strContent == "")
                        {
                            if (blnIsAddRow)
                                return false;

                            AryIsReq.Add("0");
                        }
                        else
                            AryIsReq.Add("1");
                    }
                }
            }

            ex = null;

            string strCheck = "0";
            strCheck = "1";
            for (int i = 0; i <= (AryIsReq.Count - 1); i++)
            {
                if (AryIsReq[i].ToString() != strCheck)
                {
                    int iCheck = fgDtls.ColumnCount - 1;
                    for (int k = 0; k <= iCheck; k++)
                    {
                        if (dtRow[k]["IsCompulsoryCol"].ToString() == "True")
                        {
                            string strContent = Conversions.ToString(fgDtls.Rows[fgDtls.Rows.Count - 1].Cells[Localization.ParseNativeInt(dtRow[k]["ColIndex"].ToString())].FormattedValue);
                            if (Versioned.IsNumeric(strContent))
                            {
                                if (Conversion.Val(strContent) <= 0.0)
                                {
                                    fgDtls.Rows.RemoveAt(fgDtls.Rows.Count - 1);
                                    if (fgDtls.Rows.Count == 0)
                                        fgDtls.Rows.Add();

                                    return true;
                                }
                                if (strContent == "")
                                {
                                    fgDtls.Rows.RemoveAt(fgDtls.Rows.Count - 1);
                                    if (fgDtls.Rows.Count == 0)
                                    {
                                        fgDtls.Rows.Add();
                                    }
                                    return true;
                                }
                            }
                            else if (strContent == "")
                            {
                                fgDtls.Rows.RemoveAt(fgDtls.Rows.Count - 1);
                                if (fgDtls.Rows.Count == 0)
                                {
                                    fgDtls.Rows.Add();
                                }
                                return true;
                            }
                        }
                    }
                    return true;
                }
            }
            flag = true;
        }
        catch
        { return false; }
        return flag;
    }

    public static bool IsCompulsoryInGrid(ref DataGridViewEx fgDtls, ref DataTable dt_AryIsRequired, [Optional, DefaultParameterValue(false)] bool blnIsAddRow)
    {
        bool flag;
        try
        {
            DataRow[] dtRow = dt_AryIsRequired.Select("SubGridID = " + Conversions.ToString(fgDtls.Grid_UID));
            DataGridViewEx ex = fgDtls;
            if ((ex.ColumnCount > 1) && (ex.Rows.Count >= 1))
            {
                for (int i = 0; i <= (dtRow.Length - 1); i++)
                {
                    if ((dtRow[i]["IsCompulsoryCol"].ToString() == "True") && (Conversions.ToString(ex.Rows[ex.Rows.Count - 1].Cells[Localization.ParseNativeInt(dtRow[i]["ColIndex"].ToString())].FormattedValue) == ""))
                    { return false; }
                }
            }
            ex = null;
            flag = true;
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return false;
        }
        return flag;
    }

    public static bool IsValidGridReq(DataGridViewEx fgDtls, DataTable dt_AryIsRequired)
    {
        bool flag = false;
        try
        {
            int iReqFld = 0;
            int iAvlFld = 0;
            DataGridViewEx ex = fgDtls;
            if ((ex.ColumnCount > 1) && (ex.Rows.Count >= 1))
            {
                DataRow[] rowArray = dt_AryIsRequired.Select("SubGridID = " + Conversions.ToString(ex.Grid_UID));
                int jRow = Localization.ParseNativeInt((fgDtls.RowCount - (fgDtls.RowCount > 1 ? 2 : 1)).ToString());
                for (int i = 0; i <= jRow; i++)
                {
                    for (int j = 0; j <= (rowArray.Length - 1); j++)
                    {
                        if (rowArray[j]["IsRequired"].ToString() == "True")
                        {
                            iReqFld++;
                            string str = Conversions.ToString(ex.Rows[i].Cells[Localization.ParseNativeInt(rowArray[j]["ColIndex"].ToString())].FormattedValue);
                            if (!DBNull.Value.Equals(ex.Rows[i].Cells[Localization.ParseNativeInt(rowArray[j]["ColIndex"].ToString())].Value) && Conversions.ToString(ex.Rows[i].Cells[Localization.ParseNativeInt(rowArray[j]["ColIndex"].ToString())].Value) != "" && Conversions.ToString(ex.Rows[i].Cells[Localization.ParseNativeInt(rowArray[j]["ColIndex"].ToString())].Value) != null && Conversions.ToString(ex.Rows[i].Cells[Localization.ParseNativeInt(rowArray[j]["ColIndex"].ToString())].Value) != "0" && Conversions.ToString(ex.Rows[i].Cells[Localization.ParseNativeInt(rowArray[j]["ColIndex"].ToString())].FormattedValue) != "-- SELECT --")
                            {
                                if ((str.Trim() != "") & (str.Trim() != "False"))
                                {
                                    iAvlFld++;
                                }
                            }
                        }
                    }
                    if (iAvlFld > 0)
                    {
                        if (iAvlFld != iReqFld)
                        {
                            for (int k = 0; k <= (rowArray.Length - 1); k++)
                            {
                                if (rowArray[k]["IsRequired"].ToString() == "True")
                                {
                                    iReqFld++;
                                    string str2 = Conversions.ToString(ex.Rows[i].Cells[Localization.ParseNativeInt(rowArray[k]["ColIndex"].ToString())].FormattedValue);
                                    if (str2 == "" || str2 == null || str2 == "0" || str2 == "0.00" || str2 == "0.000" || str2 == "-- SELECT --")
                                    {
                                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", string.Format("Please enter {0} fields.", ex.Columns[Localization.ParseNativeInt(rowArray[k]["ColIndex"].ToString().ToString())].HeaderText));
                                        ex.CurrentCell = ex[Localization.ParseNativeInt(rowArray[k]["ColIndex"].ToString().ToString()), i];
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                    else if (ex.RowCount == 1)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please enter required fields.");
                        ex.CurrentCell = ex[1, i];
                        return false;
                    }
                    iReqFld = 0;
                    iAvlFld = 0;
                }
            }
            ex = null;
            flag = true;
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return flag;
        }
        return flag;
    }

    #endregion

    #region "Form Events"

    /// <summary>
    /// Dispose the Main Form of the application
    /// </summary>

    public static void CloseWithoutMessage()
    {
        try
        {
            object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
            dynamic frmObj = objectValue;
            try
            {
                Console.WriteLine(RuntimeHelpers.GetObjectValue(frmObj.blnFormAction));
                Navigate.EnableNavigate(0, Enum_Define.ActionType.Not_Active, ref objectValue);

                ((DataTable)frmObj.dt_HasDtls_Grd).Clear();
                ((DataTable)frmObj.dt_AryCalcvalue).Clear();
                ((DataTable)frmObj.dt_AryIsRequired).Clear();
            }
            catch
            {
            }

            frmObj.Close();
            frmObj.Dispose();
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    public static void Closeform()
    {
        try
        {
            object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
            dynamic objfrm = objectValue;
            if (objectValue == null)
            {
                if (CIS_Dialog.Show("Do you want to close this application?", GetAssemblyInfo.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CIS_Utilities.CIS_Dialog.Show("Thank you for using this Application.", "Crocus IT Solutions Pvt. Ltd.");
                    UpDateUserStatus();
                    AppStart.IsBool = 4;
                    AppStart.IsExit = true;
                    Application.ExitThread();
                }
            }
            else if (CIS_Dialog.Show("Do you want to close this screen ?", GetAssemblyInfo.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Console.WriteLine(RuntimeHelpers.GetObjectValue(objfrm.blnFormAction));
                    Navigate.EnableNavigate(0, Enum_Define.ActionType.Not_Active, ref objectValue);
                    ((DataTable)objfrm.dt_HasDtls_Grd).Clear();
                    ((DataTable)objfrm.dt_AryCalcvalue).Clear();
                    ((DataTable)objfrm.dt_AryIsRequired).Clear();
                }
                catch
                {
                }
                objfrm.Close();
                objfrm.Dispose();
            }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    //public static void cboFabType_Validating(object sender, CancelEventArgs e)
    //{
    //    try
    //    {
    //        CIS_MultiColumnComboBox.CIS_MultiColumnComboBox sCbo = (CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)sender;
    //        frmIface frmface = new frmIface();
    //        object activeChilds = (Form)Navigate.GetActiveChild();
    //        frmface = (frmIface)activeChilds;
    //        if (frmface.isSecondMessage == false)
    //        {
    //            if ((sCbo.Text.Trim() != "") && (sCbo.Text.Trim() != "-") && (sCbo.Text.Trim() != "--") && (sCbo.IsValidSelect != true))
    //            {
    //                if (sCbo.OpenForm.ToString() != "")
    //                {
    //                    if (CIS_Dialog.Show("This Text Does not exist in Master. Do You want to Add this in Master ?", "Add Text", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
    //                    {
    //                        object activeChild = (Form)Navigate.GetActiveChild();
    //                        if (Conversion.Val(sCbo.OpenForm.ToString()) != Conversion.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(activeChild, null, "iIDentity", new object[0], null, null, null))))
    //                        {
    //                            NewLateBinding.LateSet(activeChild, null, "WindowState", new object[] { FormWindowState.Minimized }, null, null);
    //                            ShowbyFormID(sCbo.OpenForm, RuntimeHelpers.GetObjectValue(activeChild), sCbo, -1, -1);
    //                        }

    //                    }
    //                }
    //                else
    //                {
    //                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "Entered text is not valid.");
    //                    e.Cancel = true;
    //                }
    //            }
    //        }
    //    }
    //    catch
    //    {
    //    }
    //}

    public static void cboFabType_Validating(object sender, CancelEventArgs e)
    {
        //try
        //{
        //    object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
        //    Enum_Define.ActionType FormAction = (Enum_Define.ActionType)objfrm.blnFormAction;
        //    if (FormAction == Enum_Define.ActionType.Edit_Record || FormAction == Enum_Define.ActionType.New_Record)
        //    {
        //        CIS_MultiColumnComboBox.CIS_MultiColumnComboBox Combo = (CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)sender;
        //        string IsMultiCombo = DB.GetSnglValue("Select IsMultiCombo from tbl_ComboBoxMaster where ComboID=" + Combo.Fill_ComboID + "");
        //        if (IsMultiCombo == "True")
        //        {
        //            DataRowView dr = (DataRowView)Combo.SelectedItem;
        //            Combo.Text = dr[2].ToString();
        //            if (!Combo.IsValidSelect)
        //            {
        //                Combo.Text = "";
        //            }
        //        }
        //    }
        //}
        //catch { }
    }

    public static void OnSave_KeyEnter(object sender, EventArgs e)
    {
        if (!Db_Detials.IsSaveClicked)
        {
            try
            {
                object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                dynamic objfrm = objectValue;
                if ((Localization.ParseNativeInt(objfrm.blnFormAction.ToString()) == 1) | (Localization.ParseNativeInt(objfrm.blnFormAction.ToString()) == 0))
                {
                    Form cForm = null;
                    Navigate.NavigateForm(Enum_Define.Navi_form.Save_Record, ref cForm, true, false);
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
    }

    public static void ShowbyFormID(string FormID, [Optional, DefaultParameterValue(null)] object ref_ParentForm, [Optional, DefaultParameterValue(null)] object sCbo, [Optional, DefaultParameterValue(-1)] int ref_ColID, [Optional, DefaultParameterValue(-1)] int ref_RowID, int selectedVal = 0)
    {
        try
        {
            object objectValue = RuntimeHelpers.GetObjectValue(new object());
            dynamic objfrm = objectValue;
            CIS_Textil.MDIMain objMD = (CIS_Textil.MDIMain)Navigate.GetForm_byName("MDIMain");
            int ival = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select COUNT(0) from  fn_MenuMaster_Comp() Where RefMenuID=" + FormID + "")));
            if (ival > 0)
            {
                CIS_Textil.frmChildForm frm = new CIS_Textil.frmChildForm();
                frm.iMenuID = Localization.ParseNativeInt(Convert.ToString(FormID));
                if (frm.ShowDialog() == DialogResult.Cancel)
                {
                    frm.Dispose();
                    if (frm.iRemMenuID != 0)
                    {
                        FormID = Convert.ToString(frm.iRemMenuID);
                    }
                    else { return; }
                }
                else
                {
                    frm = null;
                    return;
                }

            }
            string str = objMD.HashFunctions[Localization.ParseNativeDouble(FormID.ToString())].ToString();
            int VoucherTypeID = Localization.ParseNativeInt(DB.GetSnglValue("Select VoucherTypeID From tbl_Menumaster Where MenuID=" + FormID + ""));
            objMD.lblNavigationPath.Text = "You are here: " + DB.GetSnglValue("select Menu_Path from [fn_MenuHierarchey](" + Localization.ParseNativeDouble(FormID.ToString()) + ")");
            objMD.tblpnl_HelpText.Visible = true;
            
            if (str == "frmReportTool")
                objMD.lblHelpText_Form.Text = AppMsg.REPORTS;
            else
                objMD.lblHelpText_Form.Text = AppMsg.FORMS;

            if ((Application.OpenForms[str] == null) || (str == "frmReportTool") || VoucherTypeID >= 1 || VoucherTypeID == 0)
            {
                Type formTypeObj;
                string strFormname = "";

                strFormname = GetAssemblyInfo.ProductName + "." + str;
                formTypeObj = Type.GetType(strFormname, true, true);
                
                objfrm = (Form)Activator.CreateInstance(formTypeObj);

                objfrm.MdiParent = objMD;
                if (ref_ParentForm != null)
                {
                    objfrm.ref_ParentForm = RuntimeHelpers.GetObjectValue(ref_ParentForm);
                    objfrm.ref_Cbo = RuntimeHelpers.GetObjectValue(sCbo);
                    objfrm.ref_ColID = ref_ColID;
                    objfrm.ref_RowID = ref_RowID;
                }

                try
                {
                    objfrm.iIDentity = Localization.ParseNativeInt(FormID);

                    if (selectedVal > 0)
                    {
                        CIS_CLibrary.CIS_Textbox txtCOde = new CIS_CLibrary.CIS_Textbox();
                        txtCOde.Text = selectedVal.ToString();
                        objfrm.txtCode = txtCOde;
                    }
                    else
                    {
                        CIS_CLibrary.CIS_Textbox txtCOde = new CIS_CLibrary.CIS_Textbox();
                        txtCOde.Text = "";
                        objfrm.txtCode = txtCOde;
                    }
                }
                catch
                {
                }
                objfrm.Dock = DockStyle.Fill;
                objfrm.Show();
                int iactionType = 0;
                iactionType = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster() Where MiscName='IsFormOpen'"));
                DBSp.Log_CurrentUser(Localization.ParseNativeInt(FormID), iactionType, 0, 0, 0, 0);
            }
            else
            {
                objectValue = Application.OpenForms[str];
                NewLateBinding.LateCall(objectValue, null, "BringToFront", new object[0], null, null, null, true);
            }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "Module", "Error while loading the form... ");
        }
    }
    /// <summary>
    /// Handler For Form Close.
    /// </summary>
    /// 

    public static void frm_FormClosing(object sender, FormClosingEventArgs e)
    {
        try
        {
            object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetForm_byName("MDIMain"));
            if (Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "pnlDockBottom", new object[0], null, null, null), null, "Expand", new object[0], null, null, null), true, false))
            {
                NewLateBinding.LateSetComplex(NewLateBinding.LateGet(objectValue, null, "pnlDockBottom", new object[0], null, null, null), null, "Expand", new object[] { false }, null, null, false, true);
                NewLateBinding.LateSetComplex(NewLateBinding.LateGet(objectValue, null, "grdSearch", new object[0], null, null, null), null, "DataSource", new object[] { null }, null, null, false, true);
            }
            object instance = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
            object obj4 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(instance, null, "ref_ParentForm", new object[0], null, null, null));
            Form frm = (Form)obj4;
            if (obj4 != null)
            {
                NewLateBinding.LateSet(obj4, null, "WindowState", new object[] { FormWindowState.Normal }, null, null);
                Navigate.EnableNavigate(0, Enum_Define.ActionType.Not_Active, ref instance);
                if (obj4 != null)
                {
                    int introduced12 = Conversions.ToInteger(NewLateBinding.LateGet(obj4, null, "blnFormAction", new object[0], null, null, null));
                    Navigate.EnableNavigate(0, (Enum_Define.ActionType)introduced12, ref obj4);
                    try
                    {
                        if (NewLateBinding.LateGet(instance, null, "ref_Cbo", new object[0], null, null, null) is CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)
                        {
                            CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cbo = (CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)NewLateBinding.LateGet(instance, null, "ref_Cbo", new object[0], null, null, null);
                            string text = cbo.Text;
                            string ComboType = DB.GetSnglValue("Select ComboType from tbl_ComboBoxMaster where ComboID=" + cbo.Fill_ComboID + "");
                            Combobox_Setup.ComboType c = (Combobox_Setup.ComboType)Enum.Parse(typeof(Combobox_Setup.ComboType), ComboType, true);
                            Combobox_Setup.FillCbo(ref cbo, c, "");

                            cbo.Text = NewLateBinding.LateGet(instance, null, "sComboAddText", new object[0], null, null, null).ToString();
                            cbo.Focus();
                        }
                        else if (NewLateBinding.LateGet(instance, null, "ref_Cbo", new object[0], null, null, null) is CIS_DataGridViewEx.DataGridViewEx)
                        {
                            CIS_DataGridViewEx.DataGridViewEx cbo = new CIS_DataGridViewEx.DataGridViewEx();
                            cbo = (CIS_DataGridViewEx.DataGridViewEx)NewLateBinding.LateGet(instance, null, "ref_Cbo", new object[0], null, null, null);

                            DataGridViewComboBoxColumn column = (DataGridViewComboBoxColumn)((DataGridViewEx)NewLateBinding.LateGet(instance, null, "ref_Cbo", new object[0], null, null, null)).Columns[Localization.ParseNativeInt(Conversions.ToString(NewLateBinding.LateGet(instance, null, "ref_ColID", new object[0], null, null, null)))];
                            using (SqlDataReader reader = DB.GetRS(string.Format("Select * From tbl_GridSettings Where SubGridID={0} And GridId = {1} And ColIndex = {2}", cbo.Grid_UID, RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(obj4, null, "iIDentity", new object[0], null, null, null)), Localization.ParseNativeInt(Conversions.ToString(NewLateBinding.LateGet(instance, null, "ref_ColID", new object[0], null, null, null))))))
                            {
                                if (reader.Read())
                                {
                                    column.DataSource = DB.GetDT(string.Format("Select {0}, {1} From {2} {3}", new object[] { reader["DisplayMember"].ToString(), reader["ValueMember"].ToString(), reader["Fill_Table"].ToString(), RuntimeHelpers.GetObjectValue(Interaction.IIf(reader["whereCondition"].ToString().Trim().Length == 0, "", " Where " + reader["whereCondition"].ToString().ToString().Trim())) }), false);
                                    column.HeaderText = reader["ColHeading"].ToString();
                                    column.DisplayMember = reader["DisplayMember"].ToString();
                                    column.DataPropertyName = reader["DisplayMember"].ToString();
                                    column.ValueMember = reader["ValueMember"].ToString();
                                    column.CellTemplate.Value = NewLateBinding.LateGet(instance, null, "sComboAddText", new object[0], null, null, null).ToString();
                                }
                            }

                            ((DataGridViewEx)NewLateBinding.LateGet(instance, null, "ref_cbo", new object[0], null, null, null)).CurrentCell.Value = column.CellTemplate.Value;
                            ((DataGridViewEx)NewLateBinding.LateGet(instance, null, "ref_cbo", new object[0], null, null, null)).Focus();
                        }
                    }
                    catch (Exception exception1)
                    {
                        ProjectData.SetProjectError(exception1);
                        Exception exception = exception1;
                        ProjectData.ClearProjectError();
                    }
                }
            }
            NewLateBinding.LateCall(sender, null, "Dispose", new object[0], null, null, null, true);
            int iactionType = 0;
            iactionType = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster() Where MiscName='IsFormClose'"));
            DBSp.Log_CurrentUser(Localization.ParseNativeInt(Conversions.ToString(NewLateBinding.LateGet(instance, null, "iIDentity", new object[0], null, null, null))), iactionType, 0, 0, 0, 0);
            //DBSp.Log_CurrentUser(Localization.ParseNativeInt(Conversions.ToString(NewLateBinding.LateGet(instance, null, "iIDentity", new object[0], null, null, null))), Enum_Define.ActionType.Not_Active);

            object objMDI1 = RuntimeHelpers.GetObjectValue(Navigate.GetForm_byName("MDIMain"));
            dynamic objfrm = objMDI1;

            object objchld = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
            if (objchld == null)
            {
                dynamic objChldfrm = objchld;
                objfrm.tblpnl_HelpText.Visible = false;
                objfrm.lblNavigationPath.Text = "";
                objfrm.lblHelpText_Form.Text = "";
            }
            else
            {
                dynamic objChldfrm = objchld;
                objfrm.tblpnl_HelpText.Visible = true;
                objfrm.lblNavigationPath.Text = "You are here: " + DB.GetSnglValue("select Menu_Path from [fn_MenuHierarchey](" + objChldfrm.iIDentity + ")");
            }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    #endregion

    public static void PrintBarcode(string LabelNm, string PieceNo, string DesignNm, string QualityNm, string ShadeNm, string sLotNo, decimal Meters, int pcs, decimal Weight)
    {
        try
        {
            string sourceFileName = string.Empty;
            string path = string.Empty;
            string Batchpath = string.Empty;
            if (Application.StartupPath.ToString().Contains(@"bin\Debug"))
            {
                sourceFileName = Application.StartupPath.ToString().Replace(@"bin\Debug", "") + @"Barcode\" + LabelNm + ".prn";
                path = Application.StartupPath.ToString().Replace(@"bin\Debug", "") + @"Barcode\Temp.prn";
                Batchpath = Application.StartupPath.ToString().Replace(@"bin\Debug", "") + @"Barcode\PRBAR.BAT";
            }
            else
            {
                sourceFileName = Application.StartupPath.ToString() + @"\Barcode\" + LabelNm + ".prn";
                path = Application.StartupPath.ToString() + @"\Barcode\Temp.prn";
                Batchpath = Application.StartupPath.ToString() + @"\Barcode\PRBAR.BAT";
            }

            if (!File.Exists(path))
            {
                File.Copy(sourceFileName, path);
            }

            StreamWriter sw = new StreamWriter(Batchpath);
            sw.WriteLine("Print " + '"' + path + '"');
            sw.Flush();
            sw.Close();

            dynamic fso = null;
            dynamic inputFile = null;
            dynamic outputFile = null;
            string str = null;

            fso = Interaction.CreateObject("Scripting.FileSystemObject");
            inputFile = fso.OpenTextFile(sourceFileName, 1);
            str = inputFile.ReadAll;

            string Serial = DB.GetSnglValue("Select FabricName From fn_Fabricmaster() Where FabricDesignName = " + CommonLogic.SQuote(DesignNm) + " And FabricQualityName = " + CommonLogic.SQuote(QualityNm) + " And FabricShadeName = " + CommonLogic.SQuote(ShadeNm));


            str = str.Replace("PPPPPPPPPP", PieceNo);
            if (str.Contains("EEEEEEEEEEEEEEEEEEEE"))
                str = str.Replace("EEEEEEEEEEEEEEEEEEEE", Serial);
            if (str.Contains("QQQQQQQQQQQQQQQQQQQQ"))
                str = str.Replace("QQQQQQQQQQQQQQQQQQQQ", QualityNm);
            if (str.Contains("DDDDDDDDDDDDDDDDDDDD"))
                str = str.Replace("DDDDDDDDDDDDDDDDDDDD", DesignNm);
            str = str.Replace("SSSSSSSSSSSSSSSSSSSS", ShadeNm);
            if (str.Contains("LLLLLLLLLL"))
                str = str.Replace("LLLLLLLLLL", sLotNo);
            str = str.Replace("MMMMMMMMMM", Meters.ToString());
            outputFile = fso.CreateTextFile(path, true);
            outputFile.Write(str);
            outputFile.close();
            Interaction.Shell(Batchpath);
            System.Threading.Thread.Sleep(1000);
        }
        catch
        {
        }
    }

    public static void UpDateUserStatus()
    {
        int session = Localization.ParseNativeInt(DB.GetSnglValue("Select Sessions From tbl_UserMaster Where isdeleted=0 and UserID=" + Db_Detials.UserID + ""));
        DB.ExecuteSQL(string.Format("Update {0} Set IsLoggedIn = 0, IPAddress='{1}',Sessions={2} Where UserID = {3}", "tbl_UserMaster", CommonCls.GetIP(), session - 1, Db_Detials.UserID));
        try
        {
            int iactionType = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster() Where MiscName='IsLogOut'"));
            DBSp.Log_CurrentUser(1, iactionType, 0, 0, 0, 0);
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }
}


