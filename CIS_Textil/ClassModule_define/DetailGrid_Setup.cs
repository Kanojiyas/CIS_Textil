using System;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using  CIS_Bussiness;using CIS_DBLayer;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

public class DetailGrid_Setup
{
    public static bool isRowsEditable = true;

    #region "Form : Grid Combo Box - for filling."

    #region GRID

    public static void CreateDtlGrid(object frm, Panel panel, DataGridViewEx fgDtls, [Optional, DefaultParameterValue(null)] DataTable dt_HasDtls, [Optional, DefaultParameterValue(null)] DataTable dt_AryCalcvalue, [Optional, DefaultParameterValue(null)] DataTable dt_AryIsRequired, [Optional, DefaultParameterValue(true)] bool SetGridSettingd, [Optional, DefaultParameterValue(false)] bool pShowGridby, [Optional, DefaultParameterValue(true)] bool pAttachHandles, [Optional, DefaultParameterValue(0)] int pManual_FormID, [Optional, DefaultParameterValue(0)] int pGridID, [Optional, DefaultParameterValue(false)] bool pShowFooter)
    {
        try
        {
            object pnl = panel;
            DataGridView view;

            if (SetGridSettingd)
            {
                view = fgDtls;
                Navigate.SetPropertydtlGrid(pnl, view, DockStyle.Fill);
                fgDtls.ScrollBars = ScrollBars.Both;
                fgDtls = (DataGridViewEx)view;
            }

            fgDtls.TabIndex = 0;
            string str = frm.ToString().Replace("" + ".", "");
            str = str.Substring(0, str.IndexOf(","));
            string[] strArray = Db_Detials.Hashref[RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(frm, null, "iIDentity", new object[0], null, null, null))].ToString().Split(new char[] { ';' });
            fgDtls.Grid_UID = pGridID;
            fgDtls.Grid_ID = Localization.ParseNativeInt(strArray[5].ToString());

            fgDtls.Grid_Tbl = (DB.GetSnglValue(string.Format("Select TblName From {0} Where GridID = {1} And SubGridID = {2}", "tbl_GridSettings_tbls", strArray[5].ToString(), pGridID)));
            if (pAttachHandles)
            {
                fgDtls.CellEndEdit += new DataGridViewCellEventHandler(EventHandles.fgDtls_CellEndEdit);
                fgDtls.CellParsing += new DataGridViewCellParsingEventHandler(EventHandles.fgDtls_CellParsing);
            }

            fgDtls.KeyDown += new KeyEventHandler(EventHandles.fgDtls_KeyDown);
            fgDtls.KeyPress += new KeyPressEventHandler(EventHandles.fgDtls_KeyPress);
            fgDtls.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(EventHandles.fgDtls_EditingControlShowing);
            fgDtls.CellValidating += new DataGridViewCellValidatingEventHandler(EventHandles.fgDtls_CellValidating);
            fgDtls.DataError += new DataGridViewDataErrorEventHandler(EventHandles.fgDtls_DataError);
            fgDtls.CellEnter += new DataGridViewCellEventHandler(EventHandles.fgDtls_CellEnter);
            fgDtls.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(EventHandles.fgdtls_ColumnHeaderMouseClick);
            fgDtls.CellValueChanged += new DataGridViewCellEventHandler(EventHandles.fgDtls_CellValueChanged);
            fgDtls.CellClick += new DataGridViewCellEventHandler(EventHandles.fgDtls_CellClick);

            using (DataTable table = DB.GetDT(string.Format("Select * From {0} Where GridID = {1} And SubGridID = {2} Order By ColIndex", "tbl_GridSettings", RuntimeHelpers.GetObjectValue(Interaction.IIf(pManual_FormID == 0, strArray[5].ToString(), pManual_FormID)), pGridID), false))
            {
                fgDtls.ColumnCount = 0;

                foreach (DataRow row in table.Rows)
                {
                    int ColAlign = Localization.ParseNativeInt(row["ColAllignment"].ToString().ToUpper());
                    switch (row["ColDataType"].ToString().ToUpper())
                    {
                        case "C":
                            view = fgDtls;

                            AddColto_GridCombo(ref view, Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                            row["Fill_Table"].ToString(), row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]), Localization.ParseBoolean(row["IsEditable"].ToString()),
                                            Localization.ParseBoolean(row["IsHidden"].ToString()), Localization.ParseBoolean(row["ShowStock"].ToString()),
                                            row["DisplayMember"].ToString(), row["ValueMember"].ToString(), row["ToolTip"].ToString(), row["whereCondition"].ToString(),
                                            row["Custom_Combo"].ToString());
                            fgDtls = (DataGridViewEx)view;


                            break;

                        case "I":
                            view = fgDtls;
                            if (ColAlign == 0)
                                ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleRight.ToString());

                            AddColto_Grid(ref view, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                Localization.ParseBoolean(row["ShowStock"].ToString()),
                                Enum_Define.DataType.pNumeric, DataGridViewContentAlignment.MiddleRight, row["ToolTip"].ToString());
                            fgDtls = (DataGridViewEx)view;


                            break;

                        case "D":
                            view = fgDtls;
                            if (ColAlign == 0)
                                ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleRight.ToString());

                            AddColto_Grid(ref view, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                      row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                      Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                      Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                      Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                      Localization.ParseBoolean(row["ShowStock"].ToString()),
                                      Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleRight, row["ToolTip"].ToString());
                            fgDtls = (DataGridViewEx)view;

                            break;

                        case "T":
                            view = fgDtls;
                            if (ColAlign == 0)
                                ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleLeft.ToString());

                            AddColto_Grid(ref view, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                       row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                       Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                       Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                       Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                       Localization.ParseBoolean(row["ShowStock"].ToString()),
                                       Enum_Define.DataType.pString, (DataGridViewContentAlignment)ColAlign, row["ToolTip"].ToString());
                            fgDtls = (DataGridViewEx)view;

                            break;

                        case "B":
                            view = fgDtls;
                            if (ColAlign == 0)
                                ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleLeft.ToString());

                            AddChekboxto_Grid(ref view, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                       row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                       Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                       Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                      Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                      Localization.ParseBoolean(row["ShowStock"].ToString()),
                                      Enum_Define.DataType.pBoolean, (DataGridViewContentAlignment)ColAlign);
                            fgDtls = (DataGridViewEx)view;

                            break;

                        case "S":
                            view = fgDtls;
                            if (ColAlign == 0)
                                ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleLeft.ToString());

                            AddColto_Grid(ref view, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                      row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                      Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                      Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                      Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                      Localization.ParseBoolean(row["ShowStock"].ToString()),
                                      Enum_Define.DataType.pDate, (DataGridViewContentAlignment)ColAlign, row["ToolTip"].ToString());
                            fgDtls = (DataGridViewEx)view;

                            break;

                        case "M":
                            view = fgDtls;
                            if (ColAlign == 0)
                                ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleLeft.ToString());

                            AddPicto_Grid(ref view, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                      row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                      Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                      Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                      Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                      Localization.ParseBoolean(row["ShowStock"].ToString()),
                                      Enum_Define.DataType.pImage, (DataGridViewContentAlignment)ColAlign);
                            fgDtls = (DataGridViewEx)view;

                            break;

                        case "Z":
                            view = fgDtls;
                            if (ColAlign == 0)
                                ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleLeft.ToString());
                            AddTimeColto_Grid(ref view, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                        row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                       Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                       Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                      Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                      Localization.ParseBoolean(row["ShowStock"].ToString()),
                                      Enum_Define.DataType.pTime, (DataGridViewContentAlignment)ColAlign, row["ToolTip"].ToString());
                            fgDtls = (DataGridViewEx)view;

                            break;
                    }

                    dt_HasDtls.Rows.Add(row["SubGridID"].ToString(), row["ColIndex"], row["ColOrder"], row["ColDataType"].ToString(),
                        row["ColFields"].ToString().Trim(), row["ColDataFormat"].ToString(), row["Openform"].ToString(), row["ToolTip"].ToString());

                    if (row["ColCalcValue"].ToString() != "")
                        dt_AryCalcvalue.Rows.Add(row["SubGridID"].ToString(), row["ColIndex"], row["ColOrder"], Convert.ToString(row["ColFields"]), Convert.ToString(row["ColCalcValue"]));

                    dt_AryIsRequired.Rows.Add(row["SubGridID"].ToString(), row["ColIndex"], row["ColOrder"], Convert.ToString(row["ColFields"]), row["IsRequired"].ToString(), row["IsRepeatRow"].ToString(),
                        row["IsCompulsoryCol"].ToString());
                }
            }
            EventHandles.CreateDefault_Rows(fgDtls, dt_HasDtls, dt_AryCalcvalue, dt_AryIsRequired, false, false);
            try
            {
                NewLateBinding.LateCall(frm, null, "FillControls", new object[0], null, null, null, true);
            }
            catch
            { }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    public static void CreateDtlGrid_footer(object frm, DataGridViewEx fgDtls, DataGridViewEx fgDtls_f, [Optional, DefaultParameterValue(null)] DataTable dt_HasDtls, [Optional, DefaultParameterValue(null)] DataTable dt_AryCalcvalue, [Optional, DefaultParameterValue(null)] DataTable dt_AryIsRequired, [Optional, DefaultParameterValue(true)] bool SetGridSettingd, [Optional, DefaultParameterValue(false)] bool pShowGridby, [Optional, DefaultParameterValue(true)] bool pAttachHandles, [Optional, DefaultParameterValue(0)] int pManual_FormID, [Optional, DefaultParameterValue(0)] int pGridID, [Optional, DefaultParameterValue(false)] bool pShowFooter)
    {
        try
        {
            DataGridView view;
            if (SetGridSettingd)
            {
                view = fgDtls;
                Navigate.SetPropertydtlGrid(view);
                fgDtls = (DataGridViewEx)view;
            }

            fgDtls.TabIndex = 0;
            string str = frm.ToString().Replace("" + ".", "");
            str = str.Substring(0, str.IndexOf(","));
            string[] strArray = Db_Detials.Hashref[RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(frm, null, "iIDentity", new object[0], null, null, null))].ToString().Split(new char[] { ';' });
            fgDtls.Grid_UID = pGridID;
            fgDtls.Grid_ID = Localization.ParseNativeInt(strArray[5].ToString());
            fgDtls.Grid_Tbl = (DB.GetSnglValue(string.Format("Select TblName From {0} Where GridID = {1} And SubGridID = {2}", "tbl_GridSettings_tbls", strArray[5].ToString(), pGridID)));

            if (pAttachHandles)
            {
                fgDtls.CellEndEdit += new DataGridViewCellEventHandler(EventHandles.fgDtls_CellEndEdit);
                fgDtls.CellParsing += new DataGridViewCellParsingEventHandler(EventHandles.fgDtls_CellParsing);
            }

            fgDtls.KeyDown += new KeyEventHandler(EventHandles.fgDtls_KeyDown);
            fgDtls.KeyPress += new KeyPressEventHandler(EventHandles.fgDtls_KeyPress);
            fgDtls.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(EventHandles.fgDtls_EditingControlShowing);
            fgDtls.CellValidating += new DataGridViewCellValidatingEventHandler(EventHandles.fgDtls_CellValidating);
            fgDtls.DataError += new DataGridViewDataErrorEventHandler(EventHandles.fgDtls_DataError);
            fgDtls.CellEnter += new DataGridViewCellEventHandler(EventHandles.fgDtls_CellEnter);
            fgDtls.ColumnHeaderMouseClick += new DataGridViewCellMouseEventHandler(EventHandles.fgdtls_ColumnHeaderMouseClick);
            fgDtls.CellClick += new DataGridViewCellEventHandler(EventHandles.fgDtls_CellClick);

            using (DataTable table = DB.GetDT(string.Format("Select * From {0} Where GridID = {1} And SubGridID = {2} Order By ColIndex", "tbl_GridSettings", RuntimeHelpers.GetObjectValue(Interaction.IIf(pManual_FormID == 0, strArray[5].ToString(), pManual_FormID)), pGridID), false))
            {
                fgDtls.ColumnCount = 0;

                foreach (DataRow row in table.Rows)
                {
                    int ColAlign = Localization.ParseNativeInt(row["ColAllignment"].ToString().ToUpper());
                    switch (row["ColDataType"].ToString().ToUpper())
                    {
                        case "C":
                            view = fgDtls;
                            AddColto_GridCombo(ref view, Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                      row["Fill_Table"].ToString(), row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]), Localization.ParseBoolean(row["IsEditable"].ToString()),
                                      Localization.ParseBoolean(row["IsHidden"].ToString()), Localization.ParseBoolean(row["ShowStock"].ToString()), row["DisplayMember"].ToString(),
                                      row["ValueMember"].ToString(), row["ToolTip"].ToString(), row["whereCondition"].ToString(),
                                      row["Custom_Combo"].ToString());
                            fgDtls = (DataGridViewEx)view;
                            break;

                        case "I":
                            view = fgDtls;
                            if (ColAlign == 0)
                                ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleRight.ToString());

                            AddColto_Grid(ref view, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                      row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                      Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                      Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                      Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                      Localization.ParseBoolean(row["ShowStock"].ToString()),
                                      Enum_Define.DataType.pNumeric, DataGridViewContentAlignment.MiddleRight, row["ToolTip"].ToString());
                            fgDtls = (DataGridViewEx)view;
                            break;

                        case "D":
                            view = fgDtls;
                            if (ColAlign == 0)
                                ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleRight.ToString());

                            AddColto_Grid(ref view, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                      row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                      Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                      Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                      Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                      Localization.ParseBoolean(row["ShowStock"].ToString()),
                                      Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleRight, row["ToolTip"].ToString());
                            fgDtls = (DataGridViewEx)view;
                            break;

                        case "T":
                            view = fgDtls;
                            if (ColAlign == 0)
                                ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleLeft.ToString());

                            AddColto_Grid(ref view, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                      row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                      Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                      Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                      Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                      Localization.ParseBoolean(row["ShowStock"].ToString()),
                                      Enum_Define.DataType.pString, (DataGridViewContentAlignment)ColAlign, row["ToolTip"].ToString());
                            fgDtls = (DataGridViewEx)view;
                            break;

                        case "B":
                            view = fgDtls;
                            if (ColAlign == 0)
                                ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleLeft.ToString());

                            AddChekboxto_Grid(ref view, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                      row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                      Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                      Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                      Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                      Localization.ParseBoolean(row["ShowStock"].ToString()),
                                      Enum_Define.DataType.pBoolean, (DataGridViewContentAlignment)ColAlign);
                            fgDtls = (DataGridViewEx)view;
                            break;

                        case "S":
                            view = fgDtls;
                            if (ColAlign == 0)
                                ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleLeft.ToString());

                            AddColto_Grid(ref view, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                      row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                      Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                      Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                      Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                      Localization.ParseBoolean(row["ShowStock"].ToString()),
                                      Enum_Define.DataType.pDate, (DataGridViewContentAlignment)ColAlign, row["ToolTip"].ToString());
                            fgDtls = (DataGridViewEx)view;
                            break;

                        case "M":
                            view = fgDtls;
                            if (ColAlign == 0)
                                ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleLeft.ToString());

                            AddPicto_Grid(ref view, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                      row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                      Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                      Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                      Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                      Localization.ParseBoolean(row["ShowStock"].ToString()),
                                      Enum_Define.DataType.pImage, (DataGridViewContentAlignment)ColAlign);
                            fgDtls = (DataGridViewEx)view;
                            break;

                        case "Z":
                            view = fgDtls;
                            if (ColAlign == 0)
                                ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleLeft.ToString());
                            AddTimeColto_Grid(ref view, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                      row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                      Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                      Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                      Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                      Localization.ParseBoolean(row["ShowStock"].ToString()),
                                      Enum_Define.DataType.pTime, (DataGridViewContentAlignment)ColAlign, row["ToolTip"].ToString());
                            fgDtls = (DataGridViewEx)view;
                            break;
                    }

                    dt_HasDtls.Rows.Add(row["SubGridID"].ToString(), row["ColIndex"], row["ColOrder"], row["ColDataType"].ToString(),
                        row["ColFields"].ToString().Trim(), row["ColDataFormat"].ToString(), row["Openform"].ToString(), row["ToolTip"].ToString());

                    if (row["ColCalcValue"].ToString() != "")
                        dt_AryCalcvalue.Rows.Add(row["SubGridID"].ToString(), row["ColIndex"], row["ColOrder"], Convert.ToString(row["ColFields"]), Convert.ToString(row["ColCalcValue"]));

                    dt_AryIsRequired.Rows.Add(row["SubGridID"].ToString(), row["ColIndex"], row["ColOrder"], Convert.ToString(row["ColFields"]), row["IsRequired"].ToString(), row["IsRepeatRow"].ToString(),
                        row["IsCompulsoryCol"].ToString());
                }
            }
            EventHandles.CreateDefault_Rows(fgDtls, dt_HasDtls, dt_AryCalcvalue, dt_AryIsRequired, false, false);
            try
            {
                NewLateBinding.LateCall(frm, null, "FillControls", new object[0], null, null, null, true);
            }
            catch
            { }

            if (pShowFooter)
            {
                FillFooter(fgDtls, fgDtls_f);
            }

        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    public static void FillFooter(DataGridViewEx fgDtls, DataGridViewEx fgDtls_f)
    {
        try
        {
            DataGridView viewFooter;
            viewFooter = fgDtls_f;
            viewFooter.ScrollBars = ScrollBars.Horizontal;
            viewFooter.RowCount = 1;
            viewFooter.ColumnCount = 0;
            viewFooter.AllowUserToOrderColumns = false;
            viewFooter.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            fgDtls_f = (DataGridViewEx)viewFooter;

            if (fgDtls_f.Columns.Count == 0)
            {
                using (DataTable table = DB.GetDT(string.Format("Select * From {0} Where GridID = {1} And SubGridID = {2} Order By ColIndex", "tbl_GridSettings", fgDtls.Grid_ID, fgDtls.Grid_UID), false))
                {
                    fgDtls_f.ColumnCount = 0;
                    foreach (DataRow row in table.Rows)
                    {
                        int ColAlign = Localization.ParseNativeInt(row["ColAllignment"].ToString().ToUpper());
                        switch (row["ColDataType"].ToString().ToUpper())
                        {
                            case "C":
                                {
                                    viewFooter = fgDtls_f;
                                    AddColto_Grid(ref viewFooter, Localization.ParseNativeInt(row["ColIndex"].ToString()), row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                           Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                           Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                           Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                           Localization.ParseBoolean(row["ShowStock"].ToString()),
                                           Enum_Define.DataType.pString, (DataGridViewContentAlignment)ColAlign, row["ToolTip"].ToString());
                                    fgDtls_f = (DataGridViewEx)viewFooter;
                                }
                                break;

                            case "I":
                                viewFooter = fgDtls_f;
                                {
                                    AddColto_Grid(ref viewFooter, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                          row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                          Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                          Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                          Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                          Localization.ParseBoolean(row["ShowStock"].ToString()),
                                          Enum_Define.DataType.pNumeric, DataGridViewContentAlignment.MiddleRight, row["ToolTip"].ToString());
                                    fgDtls_f = (DataGridViewEx)viewFooter;
                                }
                                break;

                            case "D":
                                viewFooter = fgDtls_f;
                                if (ColAlign == 0)
                                    ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleRight.ToString());
                                {
                                    AddColto_Grid(ref viewFooter, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                          row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                          Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                          Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                          Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                          Localization.ParseBoolean(row["ShowStock"].ToString()),
                                          Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleRight, row["ToolTip"].ToString());
                                    fgDtls_f = (DataGridViewEx)viewFooter;
                                }
                                break;

                            case "T":
                                viewFooter = fgDtls_f;
                                if (ColAlign == 0)
                                    ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleLeft.ToString());
                                {
                                    AddColto_Grid(ref viewFooter, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                          row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                          Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                          Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                          Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                          Localization.ParseBoolean(row["ShowStock"].ToString()),
                                          Enum_Define.DataType.pString, (DataGridViewContentAlignment)ColAlign, row["ToolTip"].ToString());
                                    fgDtls_f = (DataGridViewEx)viewFooter;
                                }
                                break;

                            case "B":
                                viewFooter = fgDtls_f;
                                if (ColAlign == 0)
                                    ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleLeft.ToString());
                                {

                                    AddColto_Grid(ref viewFooter, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                           row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                           Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                           Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                           Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                           Localization.ParseBoolean(row["ShowStock"].ToString()),
                                           Enum_Define.DataType.pString, (DataGridViewContentAlignment)ColAlign, row["ToolTip"].ToString());
                                    fgDtls_f = (DataGridViewEx)viewFooter;
                                }
                                break;

                            case "S":
                                viewFooter = fgDtls_f;
                                if (ColAlign == 0)
                                    ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleLeft.ToString());
                                {
                                    AddColto_Grid(ref viewFooter, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                           row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                           Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                           Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                           Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                           Localization.ParseBoolean(row["ShowStock"].ToString()),
                                           Enum_Define.DataType.pString, (DataGridViewContentAlignment)ColAlign, row["ToolTip"].ToString());
                                    fgDtls_f = (DataGridViewEx)viewFooter;
                                }
                                break;

                            case "M":
                                viewFooter = fgDtls_f;
                                if (ColAlign == 0)
                                    ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleLeft.ToString());
                                {
                                    AddColto_Grid(ref viewFooter, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                           row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                           Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                           Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                           Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                           Localization.ParseBoolean(row["ShowStock"].ToString()),
                                           Enum_Define.DataType.pString, (DataGridViewContentAlignment)ColAlign, row["ToolTip"].ToString());
                                    fgDtls_f = (DataGridViewEx)viewFooter;
                                }
                                break;

                            case "Z":
                                viewFooter = fgDtls_f;
                                if (ColAlign == 0)
                                    ColAlign = Localization.ParseNativeInt(DataGridViewContentAlignment.MiddleLeft.ToString());

                                AddColto_Grid(ref viewFooter, Localization.ParseNativeInt(row["ColIndex"].ToString()),
                                           row["ColHeading"].ToString(), Convert.ToString(row["ColFields"]),
                                           Localization.ParseNativeInt(row["ColSize"].ToString()), Localization.ParseNativeInt(row["ColMaxLength"].ToString()),
                                           Localization.ParseNativeInt(row["ColDataFormat"].ToString()),
                                           Localization.ParseBoolean(row["IsEditable"].ToString()), Localization.ParseBoolean(row["IsHidden"].ToString()),
                                           Localization.ParseBoolean(row["ShowStock"].ToString()),
                                           Enum_Define.DataType.pString, (DataGridViewContentAlignment)ColAlign, row["ToolTip"].ToString());
                                fgDtls_f = (DataGridViewEx)viewFooter;
                                break;
                        }
                        fgDtls_f.ColumnCount = fgDtls.ColumnCount;
                        EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_f, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                        fgDtls_f.Rows[0].Cells[1].Style.BackColor = System.Drawing.Color.LightSkyBlue;
                        fgDtls_f.Rows[0].DefaultCellStyle.BackColor = System.Drawing.Color.LightBlue;
                        fgDtls_f.ColumnHeadersVisible = false;
                        fgDtls_f.RowHeadersVisible = false;
                        fgDtls_f.Rows[0].Cells[1].Value = "SUM";
                    }
                }
            }

            int i = 0;
            DataTable dt = new DataTable();
            dt = DB.GetDT(string.Format("Select * From {0} Where GridID = {1} And SubGridID = {2} Order By [ColIndex]", "tbl_GridSettings", fgDtls.Grid_ID, fgDtls.Grid_UID), false);
            foreach (DataRow dr in dt.Rows)
            {
                #region fgDtls
                fgDtls.Columns[i].HeaderText = dr["ColHeading"].ToString();
                fgDtls.Columns[i].DisplayIndex = Localization.ParseNativeInt(dr["ColOrder"].ToString());
                fgDtls.Columns[i].Visible = Localization.ParseBoolean(dr["IsHidden"].ToString()) == false ? true : false;
                fgDtls.Columns[i].Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
                fgDtls.Columns[i].ReadOnly = Localization.ParseBoolean(dr["IsEditable"].ToString()) == false ? true : false;
                fgDtls.Columns[i].ToolTipText = dr["ToolTip"].ToString();
                fgDtls.Columns[i].Tag = dr["ShowStock"].ToString();
                #endregion

                #region fgDtls_f
                try
                {
                    fgDtls_f.Columns[i].DisplayIndex = Localization.ParseNativeInt(dr["ColOrder"].ToString());

                    if (dr["Coldatatype"].ToString() == "D" || dr["Coldatatype"].ToString() == "I")
                    {
                        fgDtls_f.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                    fgDtls_f.Columns[i].Visible = Localization.ParseBoolean(dr["IsHidden"].ToString()) == false ? true : false;
                    fgDtls_f.Columns[i].Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
                    fgDtls_f.Columns[i].ReadOnly = Localization.ParseBoolean(dr["IsEditable"].ToString()) == false ? true : false;
                    fgDtls_f.Columns[i].ToolTipText = dr["ToolTip"].ToString();
                    i++;
                }
                catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }

                #endregion
            }

        }
        catch { }
    }

    public static void CreateDtlGrid(object frm, DataGridViewEx fgDtls, DataGridViewEx fgDtls_f)
    {
        int iGridID = fgDtls.Grid_UID;
        double dbliIDentity = Conversion.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(frm, null, "iIDentity", new object[0], null, null, null)));
        int i = 0;
        DataTable dt = new DataTable();
        dt = DB.GetDT(string.Format("Select * From {0} Where GridID = {1} And SubGridID = {2} Order By [ColIndex]", "tbl_GridSettings", dbliIDentity, iGridID), false);
        foreach (DataRow dr in dt.Rows)
        {
            #region fgDtls
            fgDtls.Columns[i].HeaderText = dr["ColHeading"].ToString();
            fgDtls.Columns[i].DisplayIndex = Localization.ParseNativeInt(dr["ColOrder"].ToString());
            fgDtls.Columns[i].Visible = Localization.ParseBoolean(dr["IsHidden"].ToString()) == false ? true : false;
            fgDtls.Columns[i].Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
            fgDtls.Columns[i].ReadOnly = Localization.ParseBoolean(dr["IsEditable"].ToString()) == false ? true : false;
            fgDtls.Columns[i].ToolTipText = dr["ToolTip"].ToString();
            fgDtls.Columns[i].Tag = dr["ShowStock"].ToString();
            #endregion

            #region fgDtls_f
            try
            {
                fgDtls_f.Columns[i].DisplayIndex = Localization.ParseNativeInt(dr["ColOrder"].ToString());
                fgDtls_f.Columns[i].Visible = Localization.ParseBoolean(dr["IsHidden"].ToString()) == false ? true : false;
                fgDtls_f.Columns[i].Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
                fgDtls_f.Columns[i].ReadOnly = Localization.ParseBoolean(dr["IsEditable"].ToString()) == false ? true : false;
                fgDtls_f.Columns[i].ToolTipText = dr["ToolTip"].ToString();
                //fgDtls.Columns[i].Tag = dr["ShowStock"].ToString();

                if (dr["Coldatatype"].ToString() == "D" || dr["Coldatatype"].ToString() == "I")
                {
                    fgDtls_f.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                i++;
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
            #endregion
        }
    }

    public static void CreateDtlGrid(object frm, DataGridViewEx fgDtls)
    {
        int iGridID = fgDtls.Grid_UID;
        double dbliIDentity = Conversion.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(frm, null, "iIDentity", new object[0], null, null, null)));
        int i = 0;
        DataTable dt = new DataTable();
        dt = DB.GetDT(string.Format("Select * From {0} Where GridID = {1} And SubGridID = {2} Order By [ColIndex]", "tbl_GridSettings", dbliIDentity, iGridID), false);
        foreach (DataRow dr in dt.Rows)
        {
            #region fgDtls
            fgDtls.Columns[i].HeaderText = dr["ColHeading"].ToString();
            fgDtls.Columns[i].DisplayIndex = Localization.ParseNativeInt(dr["ColOrder"].ToString());
            fgDtls.Columns[i].Visible = Localization.ParseBoolean(dr["IsHidden"].ToString()) == false ? true : false;
            fgDtls.Columns[i].Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
            fgDtls.Columns[i].ReadOnly = Localization.ParseBoolean(dr["IsEditable"].ToString()) == false ? true : false;
            fgDtls.Columns[i].ToolTipText = dr["ToolTip"].ToString();
            fgDtls.Columns[i].Tag = dr["ShowStock"].ToString();
            #endregion

            //#region fgDtlsFooter
            //try
            //{
            //    fgDtlsFooter.Columns[i].DisplayIndex = Localization.ParseNativeInt(dr["ColOrder"].ToString());
            //    fgDtlsFooter.Columns[i].Visible = Localization.ParseBoolean(dr["IsHidden"].ToString()) == false ? true : false;
            //    fgDtlsFooter.Columns[i].Width = Localization.ParseNativeInt(dr["ColSize"].ToString());
            //    fgDtlsFooter.Columns[i].ReadOnly = Localization.ParseBoolean(dr["IsEditable"].ToString()) == false ? true : false;
            //    fgDtlsFooter.Columns[i].ToolTipText = dr["ToolTip"].ToString();
            //    i++;
            //}
            //catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
            //#endregion
        }
    }

    #endregion

    /// <summary>
    /// Add Column in DataGridView()
    /// </summary>
    public static void AddColto_Grid(ref DataGridView dgClient, int Col_Index, string Col_text, string Col_Name, int Col_Width, int Col_MaxLen, int Col_Format, bool Col_IsReadonly, bool Col_IsHidden, bool ShowStock, Enum_Define.DataType Col_DataType, DataGridViewContentAlignment Col_Align, string sToolTip)
    {
        try
        {
            DataGridViewTextBoxColumn objtxt = new DataGridViewTextBoxColumn();
            switch ((int)Col_DataType)
            {
                case 0:
                    {
                        objtxt.ValueType = Type.GetType("System.String");
                        break;
                    }
                case 1:
                    {
                        objtxt.ValueType = Type.GetType("System.Int32");
                        objtxt.DefaultCellStyle.Format = "N0";
                        break;
                    }
                case 2:
                    {
                        objtxt.ValueType = Type.GetType("System.Decimal");
                        objtxt.DefaultCellStyle.Format = "N" + Conversions.ToString(Col_Format);
                        break;
                    }
                case 5:
                    {
                        objtxt.ValueType = Type.GetType("System.DateTime");
                        objtxt.DefaultCellStyle.Format = "dd/MM/yyyy";
                        break;
                    }

                case 8:
                    {
                        objtxt.ValueType = Type.GetType("System.DateTime");
                        objtxt.DefaultCellStyle.Format = "dd/MM/yyyy";
                        break;
                    }
            }

            objtxt.HeaderText = Col_text;
            //objtxt.DisplayIndex = Col_Index;
            objtxt.Name = Col_Name;

            try
            {
                objtxt.ToolTipText = sToolTip;
            }
            catch { }
            objtxt.Name = Col_text;


            objtxt.SortMode = DataGridViewColumnSortMode.NotSortable;
            objtxt.Width = Col_Width;
            objtxt.DefaultCellStyle.Alignment = Col_Align;

            if (Col_IsReadonly)
                objtxt.ReadOnly = false;
            else
                objtxt.ReadOnly = true;


            if (Col_IsHidden)
                objtxt.Visible = false;
            else
                objtxt.Visible = true;

            objtxt.Tag = Convert.ToString(ShowStock);

            objtxt.CellTemplate.Value = "0 ";

            try
            {
                objtxt.MaxInputLength = Col_MaxLen;
            }
            catch
            {
            }
            dgClient.Columns.Add((DataGridViewColumn)objtxt);
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    /// <summary>
    /// Add Column in DataGridView()
    /// </summary>
    public static void AddTimeColto_Grid(ref DataGridView dgClient, int Col_Index, string Col_text, string Col_Name, int Col_Width, int Col_MaxLen, int Col_Format, bool Col_IsReadonly, bool Col_IsHidden, bool ShowStock, Enum_Define.DataType Col_DataType, DataGridViewContentAlignment Col_Align, string sToolTip)
    {
        try
        {

            CIS_CLibrary.DataGridViewTimeColumn objtxt = new CIS_CLibrary.DataGridViewTimeColumn();
            objtxt.HeaderText = Col_text;

            try
            {
                objtxt.ToolTipText = sToolTip;
            }
            catch { }

            objtxt.Name = Col_text;
            objtxt.SortMode = DataGridViewColumnSortMode.NotSortable;
            objtxt.Width = Col_Width;
            objtxt.DefaultCellStyle.Alignment = Col_Align;
            if (Col_IsReadonly)
                objtxt.ReadOnly = false;
            else
                objtxt.ReadOnly = true;

            if (Col_IsHidden)
                objtxt.Visible = false;
            else
                objtxt.Visible = true;

            objtxt.Tag = Convert.ToString(ShowStock);

            objtxt.CellTemplate.Value = "0 ";
            // objtxt.DisplayIndex = Col_Index;
            objtxt.Name = Col_Name;
            dgClient.Columns.Add((DataGridViewColumn)objtxt);

        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    /// <summary>
    /// Add Column type is Checkbox
    /// </summary>
    /// 
    public static void AddChekboxto_Grid(ref DataGridView dgClient, int Col_Index, string Col_text, string Col_Name, int Col_Width, int Col_MaxLen, int Col_Format, bool Col_IsReadonly, bool Col_IsHidden, bool ShowStock, Enum_Define.DataType Col_DataType, DataGridViewContentAlignment Col_Align)
    {
        try
        {
            DataGridViewCheckBoxColumn objchk = new DataGridViewCheckBoxColumn();
            {
                objchk.ValueType = Type.GetType("System.Boolean");
                objchk.CellTemplate = new DataGridViewCheckBoxCell(false);
            }

            objchk.HeaderText = Col_text;
            objchk.Name = Col_text;
            objchk.SortMode = DataGridViewColumnSortMode.NotSortable;
            objchk.Width = Col_Width;
            objchk.DefaultCellStyle.Alignment = Col_Align;

            if (Col_IsReadonly)
                objchk.ReadOnly = false;
            else
                objchk.ReadOnly = true;

            if (Col_IsHidden)
                objchk.Visible = false;
            else
                objchk.Visible = true;

            objchk.Tag = Convert.ToString(ShowStock);
            //objchk.DisplayIndex = Col_Index;
            objchk.Name = Col_Name;
            objchk.CellTemplate.Selected = false;
            dgClient.Columns.Add((DataGridViewColumn)objchk);
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    /// <summary>
    /// Add Column type is Picture 
    /// </summary>
    /// 
    public static void AddPicto_Grid(ref DataGridView dgClient, int Col_Index, string Col_text, string Col_Name, int Col_Width, int Col_MaxLen, int Col_Format, bool Col_IsReadonly, bool Col_IsHidden, bool ShowStock, Enum_Define.DataType Col_DataType, DataGridViewContentAlignment Col_Align)
    {
        try
        {
            DataGridViewImageColumn objPic = new DataGridViewImageColumn();

            objPic.HeaderText = Col_text;
            objPic.Name = Col_text;
            objPic.SortMode = DataGridViewColumnSortMode.NotSortable;
            objPic.Width = Col_Width;
            objPic.DefaultCellStyle.Alignment = Col_Align;

            if (Col_IsReadonly)
                objPic.ReadOnly = false;
            else
                objPic.ReadOnly = true;

            if (Col_IsHidden)
                objPic.Visible = false;
            else
                objPic.Visible = true;
            //objPic.DisplayIndex = Col_Index;
            objPic.Tag = Convert.ToString(ShowStock);

            objPic.Name = Col_Name;
            dgClient.Columns.Add((DataGridViewColumn)objPic);
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    /// <summary>
    /// Add Column type is combo and fill it with masters in DataGridView()
    /// </summary>
    /// 
    public static void AddColto_GridCombo(ref DataGridView dgClient, int ColWidth, int ColIndex, string TableName, string HeaderText, string Col_Name, bool Col_IsReadonly, bool Col_IsHidden, bool ShowStock, string DisplayMember, string ValueMember, string sToolTip, string whereCondition, string IsComboString = "", DataGridViewComboBoxColumn dgColRef = null, double DropDownWith = 0)
    {
        DataGridViewComboBoxColumn Col_ComboBox = new DataGridViewComboBoxColumn();
        try
        {
            string DisplayMembr = DisplayMember + "," + DisplayMember;
            if ((dgColRef != null))
                Col_ComboBox = dgColRef;

            bool IsNum = false;
            // build data mapping 
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(ValueMember) && !string.IsNullOrEmpty(DisplayMember) && !string.IsNullOrEmpty(TableName))
            {

                if (TableName.ToString().Trim().Length != 0)
                {
                    if (TableName != "-")
                        dt = DB.GetDT(string.Format("Select {0}, {1} From {2} {3}", (string.Format("Distinct({0})", DisplayMember)), ValueMember, TableName, (whereCondition.ToString().Trim().Length == 0 ? "" : " Where " + whereCondition.ToString().Trim())), false);
                    else
                        dt = DB.GetDT(IsComboString.ToString().Trim(), false);
                }
            }
            else
            {
                string[] strCbo = IsComboString.Split(',');
                string str = string.Empty;

                for (int i = 0; i <= strCbo.Length - 1; i++)
                {
                    if (str.Length != 0)
                        str += " Union ";
                    string[] str1 = strCbo[i].Split(',');
                    if (Information.IsNumeric(str1[0].ToString().Trim()))
                        IsNum = true;
                    if (strCbo[i].Contains("-") & str1[0].Length > 1)
                    {
                        string[] str2 = str1[0].Split('-');
                        str += string.Format("Select '{1}' As Col1, '{0}' As Col2 ", str2[1].ToString().Trim(), str2[0].ToString().Trim());
                    }
                    else
                    {
                        str += string.Format("Select '{1}' As Col1, '{0}' As Col2 ", str1[0].ToString().Trim(), str1[0].ToString().Trim());
                    }
                }

                dt = DB.GetDT(str, false);
                DisplayMember = "Col2";
                ValueMember = "Col1";
            }
            DataRow row = dt.NewRow();
            row[DisplayMember] = "-- SELECT --";
            row[ValueMember] = 0;
            dt.Rows.InsertAt(row, 0);

            Col_ComboBox.DataSource = dt;
            Col_ComboBox.HeaderText = HeaderText;
            Col_ComboBox.DisplayMember = DisplayMember;
            Col_ComboBox.DataPropertyName = ValueMember;
            Col_ComboBox.ValueMember = ValueMember;
            Col_ComboBox.Name = Col_Name;
            if (Localization.ParseNativeInt(DropDownWith.ToString()) > 0)
                Col_ComboBox.DropDownWidth = Localization.ParseNativeInt(DropDownWith.ToString());

            if (IsComboString.ToString().Length != 0 & IsNum == false)
                Col_ComboBox.ValueType = typeof(string);
            else
                Col_ComboBox.ValueType = typeof(Double);

            if (ColWidth == 0)
                Col_ComboBox.Width = 150;
            else
                Col_ComboBox.Width = ColWidth;
            Col_ComboBox.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            Col_ComboBox.SortMode = DataGridViewColumnSortMode.NotSortable;
            Col_ComboBox.DisplayStyleForCurrentCellOnly = true;
            Col_ComboBox.AutoComplete = true;

            Col_ComboBox.MaxDropDownItems = 10;
            Col_ComboBox.FlatStyle = FlatStyle.Flat;
            Col_ComboBox.Width = ColWidth;
            Col_ComboBox.HeaderText = HeaderText;
            Col_ComboBox.ToolTipText = sToolTip;
            // Col_ComboBox.DisplayIndex = ColIndex;
            if (Col_IsReadonly)
                Col_ComboBox.ReadOnly = false;
            else
                Col_ComboBox.ReadOnly = true;

            if (Col_IsHidden)
                Col_ComboBox.Visible = false;
            else
                Col_ComboBox.Visible = true;

            Col_ComboBox.Tag = Convert.ToString(ShowStock);

            if (dgColRef == null)
                dgClient.Columns.Add(Col_ComboBox);
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    /// <summary>
    /// Add Column type is combo and fill it with masters in DataGridView()
    /// </summary>
    /// 
    public static void Repopulate_GridCombo(ref CIS_DataGridViewEx.DataGridViewEx dgClient, int Form_iIDentity, int ColID, string whereCondition)
    {
        try
        {
            DataGridViewComboBoxColumn Col_ComboBox = (DataGridViewComboBoxColumn)dgClient.Columns[ColID];
            DataGridView dgView;
            dgView = dgClient;
            using (DataTable dt = DB.GetDT(string.Format("Select * From {0} Where GridID = {1} And SubGridID = {2} And [ColIndex] = {3}", Db_Detials.tbl_GridSettings, Form_iIDentity, dgClient.Grid_UID, ColID), false))
            {
                if (dt.Rows.Count > 0)
                {
                    AddColto_GridCombo(ref dgView, Localization.ParseNativeInt(dt.Rows[0]["ColSize"].ToString()), Localization.ParseNativeInt(dt.Rows[0]["ColIndex"].ToString()), dt.Rows[0]["Fill_Table"].ToString(), dt.Rows[0]["ColHeading"].ToString(), dt.Rows[0]["ColFields"].ToString(), Localization.ParseBoolean(dt.Rows[0]["IsEditable"].ToString()), Localization.ParseBoolean(dt.Rows[0]["IsHidden"].ToString()), Localization.ParseBoolean(dt.Rows[0]["ShowStock"].ToString()), dt.Rows[0]["DisplayMember"].ToString(), dt.Rows[0]["ValueMember"].ToString(), dt.Rows[0]["ToolTip"].ToString(), whereCondition,
                    dt.Rows[0]["Custom_Combo"].ToString(), Col_ComboBox);
                }
            }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    #endregion

    #region Fill Grid
    public static void FillGrid(DataGridViewEx piGrid, int pGridUID, string ptbl, string piFld, string pID, DataTable dt_HasDtls_Grd, int iTransType = 0, string sIsValidate = "", string sCompID = "", int iStockTable = 0)
    {
        try
        {
            if (pID != "")
            {
                piGrid.Rows.Clear();
                string str = string.Empty;
                if ((piFld.ToString().Length != 0) & (pID.ToString().Length != 0))
                {
                    str = string.Format("Where {0} = {1}", piFld, pID);
                }
                int num = Conversions.ToInteger(DB.GetSnglValue("select Count(0) from sysobjects Where xtype = 'P' And [Name] = '" + ptbl + "'"));
                string sql = string.Empty;
                if (num == 0)
                {
                    sql = string.Format(" sp_ExecQuery 'Select * From {0} {1} '", ptbl, str);
                }
                else if (!string.IsNullOrEmpty(piFld))
                {
                    sql = string.Format(" sp_ExecQuery '{0}' ", ptbl + piFld);
                }
                else
                {
                    sql = string.Format(" sp_ExecQuery '{0}' ", ptbl);
                }

                if (ptbl != null)
                {
                    using (DataTable table = DB.GetDT(sql, false))
                    {
                        if (table.Rows.Count > 0)
                        {
                            int num6 = table.Rows.Count - 1;
                            for (int i = 0; i <= num6; i++)
                            {
                                DataGridViewEx ex = piGrid;
                                ex.Rows.Add();
                                ex.Rows[ex.RowCount - 1].Cells[0].Value = ex.Rows.Count - 1;
                                DataRow[] rowArray = dt_HasDtls_Grd.Select("SubGridID = " + Conversions.ToString(pGridUID));
                                int num7 = rowArray.Length - 1;
                                for (int j = 0; j <= num7; j++)
                                {
                                    string str4 = rowArray[j]["ColDataType"].ToString();
                                    string str5 = rowArray[j]["ColFields"].ToString();
                                    string str3 = rowArray[j]["ColDataFormat"].ToString();
                                    if (str5 != "-")
                                    {
                                        switch (str4)
                                        {
                                            case "T":
                                            case "X":
                                                {
                                                    ex.Rows[ex.RowCount - 1].Cells[j].Value = table.Rows[i][str5].ToString();
                                                    continue;
                                                }

                                            case "B":
                                                {
                                                    DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)ex.Rows[ex.RowCount - 1].Cells[j];
                                                    cell.Value = Localization.ParseBoolean(table.Rows[i][str5].ToString());
                                                    continue;
                                                }

                                            case "C":
                                                {
                                                    if (Versioned.IsNumeric(table.Rows[i][str5].ToString()))
                                                    {
                                                        ex.Rows[ex.RowCount - 1].Cells[j].Value = Localization.ParseNativeInt(table.Rows[i][str5].ToString());
                                                    }
                                                    else
                                                    {
                                                        ex.Rows[ex.RowCount - 1].Cells[j].Value = table.Rows[i][str5].ToString();
                                                    }
                                                    continue;
                                                }

                                            case "S":
                                                {
                                                    if (table.Rows[i][str5].ToString() != "")
                                                    {
                                                        string str6 = Localization.ToVBDateString(table.Rows[i][str5].ToString());
                                                        ex.Rows[ex.RowCount - 1].Cells[j].Value = str6;
                                                    }
                                                    continue;
                                                }

                                            case "Z":
                                                {
                                                    if (table.Rows[i][str5].ToString() != "")
                                                    {
                                                        string str6 = table.Rows[i][str5].ToString();
                                                        string[] dtArray = str6.Split(' ');
                                                        ex.Rows[ex.RowCount - 1].Cells[j].Value = dtArray[1].ToString() + " " + dtArray[2].ToString();
                                                    }
                                                    continue;
                                                }
                                        }

                                        if (Versioned.IsNumeric(table.Rows[i][str5].ToString()))
                                        {
                                            double num5 = Convert.ToDouble(Localization.ParseNativeDecimal(table.Rows[i][str5].ToString()));
                                            num5 = Conversions.ToDouble(string.Format("{0:N" + str3 + "}", num5));
                                            ex.Rows[ex.RowCount - 1].Cells[j].Value = num5;
                                        }
                                        else
                                        {
                                            ex.Rows[ex.RowCount - 1].Cells[j].Value = table.Rows[i][str5].ToString();
                                        }
                                    }
                                }
                                ex = null;
                            }
                        }
                    }
                }
                switch (iStockTable)
                {
                    case 1:
                        {
                            if ((sIsValidate == "TRUE") && (iTransType > 0))
                            {
                                System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                                dgvCellStyle.BackColor = System.Drawing.Color.LightGray;
                                dgvCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
                                dgvCellStyle.SelectionBackColor = System.Drawing.Color.Purple;
                                dgvCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

                                try
                                {
                                    for (int i = 0; i <= piGrid.Rows.Count - 1; i++)
                                    {
                                        if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_StockFabricLedger_Tbl() WHERE RefID<>'' AND RefID='" + piGrid.Rows[i].Cells["RefID"].Value.ToString() + "' AND TransType<>" + iTransType + (sCompID != "" ? " and CompID=" + sCompID : "") + "")) > 0)
                                        {
                                            piGrid.Rows[i].ReadOnly = true;
                                            piGrid.Rows[i].DefaultCellStyle = dgvCellStyle;
                                            isRowsEditable = false;
                                        }
                                        else
                                            piGrid.Rows[i].ReadOnly = false;
                                    }
                                }
                                catch { }
                            }
                        }
                        break;

                    case 2:
                        {
                            if ((sIsValidate == "TRUE") && (iTransType > 0))
                            {
                                System.Windows.Forms.DataGridViewCellStyle dgvCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
                                dgvCellStyle.BackColor = System.Drawing.Color.LightGray;
                                dgvCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
                                dgvCellStyle.SelectionBackColor = System.Drawing.Color.Purple;
                                dgvCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

                                try
                                {
                                    for (int i = 0; i <= piGrid.Rows.Count - 1; i++)
                                    {
                                        if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT count(0) from fn_StockCatalogLedger_Tbl() WHERE RefID<>'' AND RefID='" + piGrid.Rows[i].Cells["RefID"].Value.ToString() + "'" + (sCompID != "" ? " and CompID=" + sCompID : "") + "")) > 1)
                                        {
                                            piGrid.Rows[i].ReadOnly = true;
                                            piGrid.Rows[i].DefaultCellStyle = dgvCellStyle;
                                            isRowsEditable = false;
                                        }
                                        else
                                            piGrid.Rows[i].ReadOnly = false;
                                    }
                                }
                                catch { }
                            }
                        }
                        break;
                }

                if (piGrid.Rows.Count == 0)
                {
                    if (piGrid.ColumnCount > 0)
                        piGrid.Rows.Add();
                }
            }
        }
        catch (Exception exception1)
        {
            ProjectData.SetProjectError(exception1);
            Exception exception = exception1;
            Navigate.logError(exception.Message, exception.StackTrace);
            ProjectData.ClearProjectError();
        }
    }
    #endregion
}
