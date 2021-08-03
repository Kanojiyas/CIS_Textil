using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_DBLayer;
using CIS_Bussiness;
using CIS_CLibrary;
using CIS_Utilities;
using Infragistics.Documents.Excel;
using Infragistics.UltraChart.Resources.Appearance;
using Infragistics.UltraChart.Shared.Events;
using Infragistics.UltraChart.Shared.Styles;
using Infragistics.Win;
using Infragistics.Win.Printing;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinGrid.DocumentExport;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using PopupControl;


namespace CIS_Textil
{
    public partial class frmReportTool : Form
    {
        ResourceManager m_resourceManger = null;
        [AccessedThroughProperty("fgDtls")]
        private static DataGridViewEx fgDtls;
        private bool chkExpandAll;

        private CustomColumnChooser customColumnChooserDialog;
        private int firstRowIndex;
        public int iIDentity;
        private bool IsLoadGrd;
        public bool isMultyFilter;
        private int lastRowIndex;
        private Popup pp_FontUC;
        private r_UCFont r_FontUC;
        private int totalColumnIndex;
        private readonly DisplaySettings _originalSettings;

        public DataTable dt;

        private int iReportID;
        private string sTableNM_Img = "";
        private string sColNM_Img = "";
        private string sImgForColNM = "";
        private int iColPosition = 0;
        private bool isAttachImgInEmail = false;

        private string sParam = "";
        private enum Col : int
        {
            Col_ID,
            Data_Col,
            Data_rmd,
            Data_rmdnew,
            Align
        }

        private enum ReportLst : int
        {
            ReportID,
            QueryID,
            QueryName,
            IsNewRpt
        }

        public frmReportTool()
        {
            ///* GET ORIGINAL SCREEN RESOLUTION */
            _originalSettings = DisplayManager.GetCurrentSettings();
            fgDtls = new CIS_DataGridViewEx.DataGridViewEx(); InitializeComponent();
            iIDentity = 0;
            chkExpandAll = true;
            isMultyFilter = false;
            IsLoadGrd = false;
            firstRowIndex = -1;
            lastRowIndex = -1;
            totalColumnIndex = -1;
            r_FontUC = new r_UCFont();
            customColumnChooserDialog = null;
            ultrchrt.Axis.X.ScrollScale.Visible = true;
            ultrchrt.Axis.Y.ScrollScale.Visible = true;
            //m_resourceManger = new ResourceManager("CIS_Textil.Localize.Localization", Assembly.GetExecutingAssembly());
            // Init UICulture to CurrentCulture
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            // Init Controls
            UpdateUIControls();
            lblPleaseWait.Visible = false;
        }

        private void UpdateUIControls()
        {
            try
            {
                if (m_resourceManger != null)
                {
                    this.lblLedger.Text = m_resourceManger.GetString("LEDGER");
                    this.lblReport.Text = m_resourceManger.GetString("REPORT");
                    this.lblFILTER.Text = m_resourceManger.GetString("FILTER");
                    this.lblFilterType.Text = m_resourceManger.GetString("FILTERTYPE");
                    this.lblFromDate.Text = m_resourceManger.GetString("FROMDATE");
                    this.lblTODATE.Text = m_resourceManger.GetString("TODATE");
                    this.lblReportname.Text = m_resourceManger.GetString("REPORTNAME");
                    this.btnView.Text = m_resourceManger.GetString("VIEW");
                    this.btnSave.Text = m_resourceManger.GetString("SAVE");
                    this.btnPrint.Text = m_resourceManger.GetString("PRINT");
                    this.btnSaveAll.Text = m_resourceManger.GetString("SAVEALL");
                    this.btnClose.Text = m_resourceManger.GetString("CLOSE");
                    this.btnNew.Text = m_resourceManger.GetString("NEW");
                    this.btnFont.Text = m_resourceManger.GetString("FONT");
                    this.btnColumn.Text = m_resourceManger.GetString("COLUMN");
                    this.btnEMail.Text = m_resourceManger.GetString("EMAIL");
                    this.btnExport.Text = m_resourceManger.GetString("EXPORT");
                    this.tbbtnPrint.Text = m_resourceManger.GetString("PREVIEW");
                    this.btnExpand.Text = m_resourceManger.GetString("EXPAND");
                    this.btnDelete.Text = m_resourceManger.GetString("DELETE");
                }
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        #region Form Control Events

        private void frmReportTool_Load(object sender, EventArgs e)
        {
            try
            {
                #region Apply Theme
                try
                {
                    object frm = this;
                    Theme oTheme = new Theme();
                    if (frm != null)
                    {
                        //if (Db_Detials.ActiveTheme == "Blue")
                        //    oTheme.SetThemeOnControls((Control)frm, ThemeName.Blue);
                        //else if (Db_Detials.ActiveTheme == "Gray")
                        //    oTheme.SetThemeOnControls((Control)frm, ThemeName.Gray);
                        //else if (Db_Detials.ActiveTheme == "Orange")
                        //    oTheme.SetThemeOnControls((Control)frm, ThemeName.Orange);
                        //else if (Db_Detials.ActiveTheme == "Default")
                        oTheme.SetThemeOnControls((Control)frm);
                    }
                }
                catch { }
                #endregion

                Combobox_Setup.FilterId = "";
                string StrQuery = string.Format(" sp_ShowReportList '" + Conversions.ToString(this.iIDentity) + "', " + Db_Detials.UserID + " ", new object[0]);
                Combobox_Setup.Fill_Combo(this.cboRptlst, StrQuery, "ReportName", "ReportID");
                cboRptlst.ColumnWidths = "0;280;";
                cboRptlst.SelectedIndex = 1;
                this.dtFrom.Text = Localization.ToVBDateString(DB.GetSnglValue(string.Format("Select Yr_From From tbl_YearMaster Where YearID = {0}", Db_Detials.YearID)));
                this.dtTo.Text = Localization.ToVBDateString(Conversions.ToString(DateAndTime.Now.Date));
                this.lblLedger.Enabled = true;
                this.cboLedger.Enabled = true;
                this.UGrid_Rpt.InitializeLayout += new InitializeLayoutEventHandler(CommonCls.grdSearch_InitializeLayout);
                Combobox_Setup.FillCbo(ref cboFillterType, Combobox_Setup.ComboType.Mst_FillterType, "");

                pnlDockReportSettigns.Expand = false;

                #region Hide Graph Button

                string sQryID = DB.GetSnglValue("Select QueryID From tbl_ReportQuery  Where MenuID=" + iIDentity + " and ReportName=" + cboRptlst.Text.ToString() + "");
                if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT Count(0) From tbl_ReportQuery  Where QueryID=" + sQryID + " and IsGraph=1")) > 0)
                    btnGraph.Visible = true;
                else
                    btnGraph.Visible = false;

                #endregion

                #region Collapse MDI Menu Dock
                try
                {
                    object objMDI1 = RuntimeHelpers.GetObjectValue(Navigate.GetForm_byName("MDIMain"));
                    NewLateBinding.LateSetComplex(NewLateBinding.LateGet(objMDI1, null, "pnlDockTop", new object[0], null, null, null), null, "Expand", new object[] { false }, null, null, false, true);
                    NewLateBinding.LateSetComplex(NewLateBinding.LateGet(objMDI1, null, "pnlDockLeft", new object[0], null, null, null), null, "Expand", new object[] { false }, null, null, false, true);
                    NewLateBinding.LateSetComplex(NewLateBinding.LateGet(objMDI1, null, "pnlDockRight", new object[0], null, null, null), null, "Expand", new object[] { false }, null, null, false, true);
                }
                catch { }
                #endregion

                object frm1 = this;
                DataGridViewEx ex = fgDtls;
                DataGridView view = fgDtls;
                Navigate.SetPropertydtlGrid(frm1, view, DockStyle.Fill);

                DetailGrid_Setup.AddColto_Grid(ref  view, 0, "Sr. No.", "SrNo", 60, 10, 0, true, true, false, Enum_Define.DataType.pNumeric, DataGridViewContentAlignment.MiddleLeft, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 1, "Original Column", "OriginalColumn", 150, 30, 0, false, false, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 2, "Original Column", "OriginalColumn", 150, 30, 0, false, true, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 3, "Rename Column", "RenameColumn", 150, 30, 0, false, true, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                DetailGrid_Setup.AddColto_GridCombo(ref   view, 70, 4, "", "Alignment", "Alignment", false, true, false, "", "", "", "", "C-Center, L-Left, R-Right", null, 0.0);
                fgDtls.ForeColor = Color.Black;

                MDIMain frmMDI = (MDIMain)Application.OpenForms["MDIMain"];
                int PnlDockLeftWidth = frmMDI.pnlDockLeft.Width;
                int PnlDockTopHeight = frmMDI.pnlDockTop.Height;
                int PnlDockBottomHeight = frmMDI.pnlDockBottom.Height;

                this.ClientSize = new System.Drawing.Size((Localization.ParseNativeInt(frmMDI.Width.ToString()) - (PnlDockLeftWidth + 20)), (Localization.ParseNativeInt(frmMDI.Height.ToString()) - (PnlDockTopHeight + PnlDockBottomHeight + 80)));
                this.Location = new Point(0, 0);

                this.cboRptlst.SelectedIndex = 1;
                this.cboRptlst.Focus();
                this.cboRptlst.SelectedValueChanged += new EventHandler(this.cboRptlst_SelectedValueChanged);

                this.ultrchrt.MouseWheel += new MouseEventHandler(this.ultrchrt_MouseClick);

                this.btnNew.Enabled = false;
                this.pp_FontUC = new Popup(this.r_FontUC);
                this.pp_FontUC.Resizable = true;
                if (SystemInformation.IsComboBoxAnimationEnabled)
                {
                    this.pp_FontUC.ShowingAnimation = PopupAnimations.Slide | PopupAnimations.TopToBottom;
                    this.pp_FontUC.HidingAnimation = PopupAnimations.Slide | PopupAnimations.BottomToTop;
                }
                else
                {
                    this.pp_FontUC.ShowingAnimation = pp_FontUC.HidingAnimation = PopupAnimations.None;
                }

                this.UGrid_Rpt.DrawFilter = new MyDrawFilter();
                ultrchrt.Visible = false;
                UGrid_Rpt.Visible = true;
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_DialogIcon.Information, "", ex.Message);
            }
        }

        public class MyDrawFilter : IUIElementDrawFilter
        {
            bool IUIElementDrawFilter.DrawElement(DrawPhase drawPhase, ref UIElementDrawParams drawParams)
            {
                /// RowCellAreaUIElement is the element that draws the row borders. 
                RowCellAreaUIElement rowCellAreaUIElement = drawParams.Element as RowCellAreaUIElement;
                if (null == rowCellAreaUIElement)
                {
                    // This should never happens, since we are only return a DrawPhase
                    // from GetPhasesToFilter when the element is a RowCellAreaUIElement.
                    return false;
                }

                // Get the original BorderSides that this element would draw. 
                Border3DSide borderSides = rowCellAreaUIElement.BorderSides;

                // Strip out left and right borders. 
                borderSides &= ~Border3DSide.Left;
                borderSides &= ~Border3DSide.Right;

                // Draw the borders
                drawParams.DrawBorders(rowCellAreaUIElement.BorderStyle, borderSides);

                // Return true to tell the grid that we handled the drawing and it should not to 
                // do the default drawing. 
                return true;
            }

            DrawPhase IUIElementDrawFilter.GetPhasesToFilter(ref UIElementDrawParams drawParams)
            {
                /// RowCellAreaUIElement is the element that draws the row borders. 
                if (drawParams.Element is RowCellAreaUIElement)
                {
                    //// If you want to limit the change to only when printing, you could add this check: 
                    DataAreaUIElement dataAreaUIElement = drawParams.Element.GetAncestor(typeof(DataAreaUIElement)) as DataAreaUIElement;
                    if (null == dataAreaUIElement ||
                        false == dataAreaUIElement.Layout.IsPrintLayout)
                    {
                        return DrawPhase.None;
                    }

                    return DrawPhase.BeforeDrawBorders;
                }

                return DrawPhase.None;
            }
        }

        private void btnviewrpt_Click(object sender, EventArgs e)
        {
            lblPleaseWait.Text = "Loading Report, Please wait..";
            lblPleaseWait.Visible = true;
            Application.DoEvents();
            if (!this.ValidateForm())
            {
                if (!this.isMultyFilter)
                {
                    this._viewrpt(false);
                }
                this.btnNew.Enabled = true;
            }
            lblPleaseWait.Visible = false;
            Application.DoEvents();

            try
            {
                if (UGrid_Rpt.Rows.Count >= 1)
                {
                    object instance = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                    int iactionType = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster() Where MiscName='IsReportView'"));
                    try
                    {
                        DB.ExecuteSQL("INSERT INTO tbl_UserReportLog(MenuID, ReportID, IsCrystalReport, IsBarCode, IsChequePrint, ActionType, UserID, UserDt,StoreID, CompID,BranchID, YearID, IPAddress, MacAddress) VALUES(" + Localization.ParseNativeInt(Conversions.ToString(NewLateBinding.LateGet(instance, null, "iIDentity", new object[0], null, null, null))) + ", " + iReportID + ", 0, 0, 0, " + iactionType + "," + Db_Detials.UserID + ",getdate()" + "," + Db_Detials.StoreID + "," + Db_Detials.CompID + "," + Db_Detials.BranchID + "," + Db_Detials.YearID + "," + DB.SQuote(CommonCls.GetIP()) + "," + DB.SQuote(CommonCls.FetchMacId()) + ")");
                    }
                    catch { }
                }
            }
            catch { }
        }

        private void _viewrpt(bool IsPara = false)
        {
            try
            {
                chkExpandAll = true;
                btnExpand.Text = "Expand";

                UGrid_Rpt.DataSource = null;
                UGrid_Rpt.Refresh();

                string[] strSelect = cboRptlst.SelectedValue.ToString().Split(';');
                string ReportName = cboRptlst.SelectedText;
                iReportID = Localization.ParseNativeInt(strSelect[(int)ReportLst.QueryID]);
                int sIsSP = Localization.ParseNativeInt(DB.GetSnglValue("select Count(0) from sysobjects Where xtype IN ('P','IF') And [Name] = '" + strSelect[(int)ReportLst.QueryName] + "'"));

                //Dim strIsSelectQry As String = DB.GetSnglValue(String.Format("sp_ExecQuery 'Select IsSelectQry From {0} Where ReportID = {1}'", tbl_ReportQuery, cboRptlst.ComboBox.SelectedValue))
                bool IsFilterClear = false;

                btnColumn.Enabled = true;
                btnExpand.Enabled = true;
                int iCount = 0;
                if (Db_Detials.UserID != 1)
                {
                    iCount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format(" sp_ExecQuery 'select Count(0) from {0}  as A Left JOIN tbl_ReportConfigMain as B ON A.Reportid=B.Reportid  where B.UserID=" + Db_Detials.UserID + " and B.CompanyID=" + Db_Detials.CompID + " AND B.QueryID = {1} '", Db_Detials.tbl_ReportConfigDtls, (strSelect[1].ToString() == "-1" ? "0" : strSelect[1].ToString()))));
                }
                else
                    iCount = Localization.ParseNativeInt(DB.GetSnglValue(string.Format(" sp_ExecQuery 'select Count(0) from {0}  as A Left JOIN tbl_ReportConfigMain as B ON A.Reportid=B.Reportid  where B.UserID=1 and B.CompanyID=" + Db_Detials.CompID + " AND B.QueryID = {1}'", Db_Detials.tbl_ReportConfigDtls, (strSelect[1].ToString() == "-1" ? "0" : strSelect[1].ToString()))));

                string strQry = string.Empty;
                if (iCount == 0)
                {
                    strQry = string.Format(" sp_ExecQuery 'select TOp 1 * from {0}  as A Left JOIN tbl_ReportConfigMain as B ON A.Reportid=B.Reportid  where B.UserID=" + Db_Detials.UserID + " and B.CompanyID=" + Db_Detials.CompID + " AND B.ReportID = {1}'", Db_Detials.tbl_ReportConfigDtls, (strSelect[(int)ReportLst.ReportID] == "-1" ? "0" : strSelect[(int)ReportLst.ReportID]));
                }
                else
                {
                    if (Db_Detials.UserID != 1)
                    {
                        strQry = string.Format(" sp_ExecQuery 'select * from {0}  as A Left JOIN tbl_ReportConfigMain as B ON A.Reportid=B.Reportid  where B.UserID=" + Db_Detials.UserID + " and B.CompanyID=" + Db_Detials.CompID + " AND B.QueryID = {1} AND B.ReportID = {2}'", Db_Detials.tbl_ReportConfigDtls, (strSelect[1].ToString() == "-1" ? "0" : strSelect[1].ToString()), strSelect[(int)ReportLst.ReportID]);
                    }
                    else
                        strQry = string.Format(" sp_ExecQuery 'select * from {0}  as A Left JOIN tbl_ReportConfigMain as B ON A.Reportid=B.Reportid  where B.UserID=1 and B.CompanyID=" + Db_Detials.CompID + " AND B.QueryID = {1} AND B.ReportID = {2}'", Db_Detials.tbl_ReportConfigDtls, (strSelect[1].ToString() == "-1" ? "0" : strSelect[1].ToString()), strSelect[(int)ReportLst.ReportID]);
                }

                // string strQry_ColName = string.Empty;
                using (DataTable dt = DB.GetDT(strQry, false))
                {
                    if (dt.Rows.Count > 0)
                    #region Region 1
                    {
                        SqlDataReader iDr = DB.GetRS("Select * From " + Db_Detials.tbl_ReportConfigMain + " Where UserID=" + Db_Detials.UserID + " and CompanyID=" + Db_Detials.CompID + " and Reportid = " + strSelect[(int)ReportLst.ReportID]);
                        iDr.Read();

                        if (!(Localization.ParseNativeInt(strSelect[(int)ReportLst.ReportID].ToString()) == -1))
                            this.UGrid_Rpt.Font = new Font(iDr["Font_Name"].ToString(), Convert.ToSingle(Localization.ParseNativeDecimal(iDr["Font_Size"].ToString())), FontStyle.Regular, GraphicsUnit.Point, 0);

                        string strQry_ColName = string.Empty;
                        {
                            fgDtls.Rows.Clear();
                            fgDtls.Rows.Add(dt.Rows.Count);

                            for (int i = 0; i <= dt.Rows.Count - 1; i++)
                            {
                                strQry_ColName += dt.Rows[i]["Qry_ColName"].ToString() + " As [" + dt.Rows[i]["Rpt_ColName"].ToString() + "], ";
                                fgDtls.Rows[i].Cells[(int)Col.Col_ID].Value = (i + 1);
                                fgDtls.Rows[i].Cells[(int)Col.Data_Col].Value = dt.Rows[i]["Qry_ColName"].ToString();
                                fgDtls.Rows[i].Cells[(int)Col.Data_rmd].Value = dt.Rows[i]["Rpt_ColName"].ToString();
                                fgDtls.Rows[i].Cells[(int)Col.Data_rmdnew].Value = dt.Rows[i]["Rpt_ColName"].ToString();
                            }
                        }
                        if (strQry_ColName.Length != 0)
                            strQry_ColName = Localization.Left(strQry_ColName, 2);

                        // Hide the grid caption by setting the Text to empty string.
                        UGrid_Rpt.Text = "";
                        string strQuery = string.Empty;
                        if (sIsSP == 0)
                        {
                            UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery 'Select {1} From {0}'", strSelect[(int)ReportLst.QueryName], strQry_ColName), false);
                        }
                        else
                        {
                            GetReportQry(strSelect[(int)ReportLst.QueryID], strSelect[(int)ReportLst.QueryName]);
                        }

                        dt.Select("", "Group_lvl");
                        for (int i = 0; i <= (dt.Rows.Count - 1); i++)
                        {
                            if (Localization.ParseBoolean(dt.Rows[i]["IsGroup"].ToString()))
                            {
                                UGrid_Rpt.DisplayLayout.Bands[0].SortedColumns.Add(dt.Rows[i]["Rpt_ColName"].ToString(), false, true);
                            }
                        }
                        dt.Select("", "");

                        foreach (UltraGridBand band in UGrid_Rpt.DisplayLayout.Bands)
                        {
                            foreach (UltraGridColumn column in band.Columns)
                            {
                                column.Width = Localization.ParseNativeInt(dt.Rows[column.Index]["Col_Width"].ToString());
                                column.Hidden = Localization.ParseBoolean(dt.Rows[column.Index]["IsShow"].ToString());
                                column.CellActivation = Activation.NoEdit;
                                column.Tag = dt.Rows[column.Index]["Qry_ColName"].ToString();
                                SummarySettings oSummary = null;
                                if (Localization.ParseBoolean(dt.Rows[column.Index]["IsAvg"].ToString()))
                                {
                                    oSummary = column.Band.Summaries.Add(SummaryType.Average, column, SummaryPosition.UseSummaryPositionColumn);
                                    oSummary.DisplayFormat = "{0}";
                                    oSummary.Appearance.TextHAlign = HAlign.Right;
                                }
                                if (Localization.ParseBoolean(dt.Rows[column.Index]["IsSum"].ToString()))
                                {
                                    oSummary = column.Band.Summaries.Add(SummaryType.Sum, column, SummaryPosition.UseSummaryPositionColumn);
                                    oSummary.DisplayFormat = "{0}";
                                    oSummary.Appearance.TextHAlign = HAlign.Right;
                                }
                                if (Localization.ParseBoolean(dt.Rows[column.Index]["IsMin"].ToString()))
                                {
                                    oSummary = column.Band.Summaries.Add(SummaryType.Minimum, column, SummaryPosition.UseSummaryPositionColumn);
                                }
                                if (Localization.ParseBoolean(dt.Rows[column.Index]["IsMax"].ToString()))
                                {
                                    oSummary = column.Band.Summaries.Add(SummaryType.Maximum, column, SummaryPosition.UseSummaryPositionColumn);
                                }
                                if (Localization.ParseBoolean(dt.Rows[column.Index]["IsCount"].ToString()))
                                {
                                    oSummary = column.Band.Summaries.Add(SummaryType.Count, column, SummaryPosition.UseSummaryPositionColumn);
                                }

                                // Applying filter conditions
                                //If the row filter mode is band based, then get the column filters off the band
                                ColumnFiltersCollection columnFilters = UGrid_Rpt.DisplayLayout.Bands[0].ColumnFilters;
                                if (IsFilterClear == false)
                                {
                                    columnFilters.ClearAllFilters();
                                    IsFilterClear = true;
                                }
                                {
                                    if (!string.IsNullOrEmpty(dt.Rows[column.Index]["Filter_Text"].ToString()) & !string.IsNullOrEmpty(dt.Rows[column.Index]["Filter_Type"].ToString()))
                                    {
                                        columnFilters[column.Index].FilterConditions.Add((FilterComparisionOperator)Localization.ParseNativeInt(dt.Rows[column.Index]["Filter_Type"].ToString()), dt.Rows[column.Index]["Filter_Text"].ToString());
                                    }
                                }

                            }
                        }
                    }
                    #endregion
                    else
                    #region Region 2
                    {
                        UGrid_Rpt.Text = "";
                        string strQuery = string.Empty;
                        if (sIsSP == 0)
                        {
                            UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery 'Select * From {0}'", DB.GetSnglValue(string.Format("sp_ExecQuery 'Select QueryName From {0} Where QueryID = {1}'", Db_Detials.tbl_ReportQuery, strSelect[(int)ReportLst.QueryID]))), false);
                        }
                        else
                        {
                            GetReportQry(strSelect[(int)ReportLst.QueryID], strSelect[(int)ReportLst.QueryName]);
                        }
                    }
                    #endregion

                    foreach (UltraGridBand band in UGrid_Rpt.DisplayLayout.Bands)
                    {
                        fgDtls.Rows.Clear();

                        foreach (UltraGridColumn column in band.Columns)
                        {
                            try
                            {
                                fgDtls.Rows.Add();
                                fgDtls.Rows[fgDtls.RowCount - 1].Cells[(int)Col.Col_ID].Value = (column.Index + 1);
                                fgDtls.Rows[fgDtls.RowCount - 1].Cells[(int)Col.Data_Col].Value = column.Header.Caption.ToString();
                                fgDtls.Rows[fgDtls.RowCount - 1].Cells[(int)Col.Data_rmd].Value = column.Header.Caption.ToString();
                                fgDtls.Rows[fgDtls.RowCount - 1].Cells[(int)Col.Data_rmdnew].Value = column.Header.Caption.ToString();
                                column.CellActivation = Activation.NoEdit;
                            }
                            catch { }
                        }
                        IsLoadGrd = true;
                    }
                }

                // AutoSize all of the columns
                foreach (UltraGridColumn column in UGrid_Rpt.DisplayLayout.Bands[0].Columns)
                {
                    //column.PerformAutoResize(PerformAutoSizeType.AllRowsInBand)
                    column.Header.Appearance.BackColor = Color.White;
                    column.Header.Appearance.BackColor2 = Color.White;
                    column.Header.Appearance.FontData.Bold = DefaultableBoolean.True;
                    column.Header.Appearance.BorderAlpha = Alpha.Transparent;

                    using (SqlDataReader iDr = DB.GetRS("Select * From " + Db_Detials.tbl_ReportConfigDtls + " as A Left JOIN tbl_ReportConfigMain as B ON A.Reportid=B.Reportid  where B.UserID=" + Db_Detials.UserID + " and B.CompanyID=" + Db_Detials.CompID + " AND A.Reportid = " + strSelect[(int)ReportLst.ReportID] + " And Col_Order = " + column.Index))
                    {
                        if ((iDr.Read()))
                        {
                            switch (iDr["AlignType"].ToString())
                            {
                                case "C":
                                    column.CellAppearance.TextHAlign = HAlign.Center;
                                    break;
                                case "L":
                                    column.CellAppearance.TextHAlign = HAlign.Left;
                                    break;
                                case "R":
                                    column.CellAppearance.TextHAlign = HAlign.Right;
                                    break;
                            }
                            if (string.IsNullOrEmpty(iDr["AlignType"].ToString()))
                            {
                                fgDtls.Rows[column.Index].Cells[(int)Col.Align].Value = "L";
                            }
                            else
                            {
                                fgDtls.Rows[column.Index].Cells[(int)Col.Align].Value = iDr["AlignType"].ToString();
                            }
                            column.Header.Caption = iDr["Rpt_ColName"].ToString();
                            column.Width = Localization.ParseNativeInt(iDr["Col_Width"].ToString());
                            fgDtls.Rows[column.Index].Cells[(int)Col.Data_rmdnew].Value = iDr["Rpt_ColName"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_DialogIcon.Information, "", ex.Message);
            }
        }

        private void GetReportQry(string sQryID, string sQryNM)
        {
            bool blnLedgerID = false;
            bool blnFromDt = false;
            bool blnToDt = false;

            string sQry = "";
            using (DataTable Dt = DB.GetDT("SELECT * from dbo.fn_ReportQuery() WHERE QueryID='" + sQryID + "' ORDER BY OrderNo", false))
            {
                foreach (DataRow r in Dt.Rows)
                {
                    if (r["ParameterFld"].ToString() == "LedgerID")
                    {
                        blnLedgerID = Localization.ParseBoolean(r["IsMandetory"].ToString());
                        sQry += (cboLedger.SelectedValue == null || cboLedger.SelectedValue.ToString() == "0" ? "0" : cboLedger.SelectedValue) + ",";
                    }
                    else if (r["ParameterFld"].ToString() == "FromDt")
                    {
                        blnFromDt = Localization.ParseBoolean(r["IsMandetory"].ToString());
                        sQry += CommonLogic.SQuote(Localization.ToSqlDateString(this.dtFrom.Text)) + ",";
                    }
                    else if (r["ParameterFld"].ToString() == "ToDt")
                    {
                        blnToDt = Localization.ParseBoolean(r["IsMandetory"].ToString());
                        sQry += CommonLogic.SQuote(Localization.ToSqlDateString(this.dtTo.Text)) + ",";
                    }
                    else if (r["ParameterFld"].ToString() == "CompID")
                    {
                        //blnToDt = Localization.ParseBoolean(r["IsMandetory"].ToString());
                        sQry += Db_Detials.CompID + ",";
                    }
                    else if (r["ParameterFld"].ToString() == "YearID")
                    {
                        //blnToDt = Localization.ParseBoolean(r["IsMandetory"].ToString());
                        sQry += Db_Detials.YearID + ",";
                    }
                    sParam = sQry;
                }
            }

            if ((blnFromDt == true) && (blnToDt == true) && (blnLedgerID == true))
            {
                if ((!Information.IsDate(this.dtFrom.Text) && !Information.IsDate(this.dtTo.Text)))
                {
                    Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
                }
                else if (cboLedger.SelectedValue != null && Localization.ParseNativeInt(cboLedger.SelectedValue.ToString()) == 0)
                {
                    Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please Select Ledger");
                }
            }
            else if ((blnFromDt == true) && (blnToDt == true) && (blnLedgerID == false))
            {
                if (!Information.IsDate(this.dtFrom.Text) && !Information.IsDate(this.dtTo.Text))
                {
                    Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
                }
            }

            else if ((blnFromDt == false) && (blnToDt == false) && (blnLedgerID == true))
            {
                if ((cboLedger.SelectedValue != null) && (Localization.ParseNativeInt(cboLedger.SelectedValue.ToString()) == 0))
                {
                    Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please Select Ledger");
                }
            }

            if (sQry.Length > 0)
            {
                sQry = sQry.Substring(0, (sQry.Length - 1));
            }

            dt = DB.GetDT(sQryNM + " " + sQry + "", false);
            this.UGrid_Rpt.DataSource = DB.GetDT(sQryNM + " " + sQry + "", 240);
            //this.UGrid_Rpt.DataSource = DB.resort(dt, sQryCN);
            ultrchrt.Visible = false;
            UGrid_Rpt.Visible = true;

        }

        //private void _viewrpt(bool IsPara = false)
        //{
        //    try
        //    {
        //        this.chkExpandAll = true;
        //        this.btnExpand.Text = "Expand";
        //        this.UGrid_Rpt.DataSource = null;
        //        this.UGrid_Rpt.Refresh();
        //        string[] strSelect = this.cboRptlst.SelectedValue.ToString().Split(new char[] { ';' });
        //        string ReportName = this.cboRptlst.SelectedText;
        //        int sIsSP = Conversions.ToInteger(DB.GetSnglValue("select Count(0) from sysobjects Where xtype IN ('P','IF') And [Name] = '" + strSelect[2] + "'"));
        //        bool IsFilterClear = false;
        //        this.btnColumn.Enabled = true;
        //        this.btnExpand.Enabled = true;
        //        using (DataTable dt = DB.GetDT(string.Format(" sp_ExecQuery 'select * from {0} where Reportid = {1} Order By Col_Order Asc'", "tbl_ReportConfigDtls", RuntimeHelpers.GetObjectValue(Interaction.IIf(strSelect[0] == "-1", 0, strSelect[0]))), false))
        //        {
        //            BandEnumerator bndEnum;
        //            if (dt.Rows.Count <= 0)
        //            {
        //                goto Label_17CE;
        //            }
        //            SqlDataReader iDr = DB.GetRS("Select * From tbl_ReportConfigMain Where UserID="+Db_Detials.UserID+" and CompanyID="+Db_Detials.CompID+" and Reportid = " + strSelect[0]);
        //            iDr.Read();
        //            if (Conversions.ToDouble(strSelect[0]) != -1.0)
        //            {
        //                this.UGrid_Rpt.Font = new Font(iDr["Font_Name"].ToString(), Convert.ToSingle(Localization.ParseNativeDecimal(iDr["Font_Size"].ToString())), FontStyle.Regular, GraphicsUnit.Point, 0);
        //            }
        //            string strQry_ColName = string.Empty;
        //            DataGridViewEx ex = fgDtls;
        //            ex.Rows.Clear();
        //            ex.Rows.Add(dt.Rows.Count);
        //            for (int i = 0; i <= (dt.Rows.Count - 1); i++)
        //            {
        //                strQry_ColName = strQry_ColName + dt.Rows[i]["Qry_ColName"].ToString() + " As [" + dt.Rows[i]["Rpt_ColName"].ToString() + "], ";
        //                ex.Rows[i].Cells[0].Value = i + 1;
        //                ex.Rows[i].Cells[1].Value = dt.Rows[i]["Qry_ColName"].ToString();
        //                ex.Rows[i].Cells[2].Value = dt.Rows[i]["Rpt_ColName"].ToString();
        //                ex.Rows[i].Cells[3].Value = dt.Rows[i]["Rpt_ColName"].ToString();
        //            }
        //            ex = null;
        //            if (strQry_ColName.Length != 0)
        //            {
        //                strQry_ColName = Localization.Left(strQry_ColName, 2);
        //            }
        //            this.UGrid_Rpt.Text = "";
        //            if (sIsSP == 0)
        //            {
        //                this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery 'Select {1} From {0}'", strSelect[2], strQry_ColName), false);
        //            }
        //            else
        //            {
        //                string sp_Name = strSelect[2];
        //                switch (sp_Name)
        //                {
        //                    case "Sp_FetchYarnWiseStock":
        //                    case "Sp_FetchShadeWiseStock":
        //                    case "Sp_FetchColorWiseStock":
        //                    case "sp_FetchFabricQualityWiseStk":
        //                    case "sp_FetchFabricDesignWiseStk1":
        //                    case "sp_FetchFabricShadeWiseStk":
        //                    case "Sp_InHouseYarnStock":
        //                    case "Sp_DyerYarnStock":
        //                    case "Sp_WarpingYarnStock":
        //                    case "Sp_YarnWeaverStock":
        //                    case "Sp_ProcesserYarnStock":
        //                    case "sp_FetchProcYrnPOStk":
        //                    case "Sp_InHouseYarnStockBoxWise":
        //                    case "sp_FetchFabricDesignWiseStk1_sum":
        //                    case "Sp_WeaverYarnStock":
        //                    case "Sp_WeaverDesignWiseBeamStk":
        //                    case "Sp_StockFabricLedger":
        //                        if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[2] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", new object[] { Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text), RuntimeHelpers.GetObjectValue(Interaction.IIf(this.cboLedger.SelectedValue == null, 0, RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue))) })), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                        }
        //                        break;

        //                    case "sp_FetchFabricDesignWiseStk":
        //                    case "sp_POWiseStockRpt":
        //                    case "sp_POWiseStockRpt1":
        //                    case "sp_POWiseFinishFabircStock":
        //                        if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                        {
        //                            if (this.cboLedger.SelectedValue != null)
        //                            {
        //                                this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[2] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", new object[] { Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text), RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue) })), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please Select Location");
        //                            }
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                        }
        //                        break;

        //                    case "sp_FetchOverAllYarnStock":
        //                    case "Sp_WeaverYarnStockSummary":
        //                    case "sp_FetchOverAllBeamStock":
        //                    case "sp_FetchOverAllFabStock":
        //                    case "Sp_FetchDeptYarnWiseStock":
        //                        if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" {3},''{0}'', {1}, {2}", new object[] { Localization.ToSqlDateString(this.dtTo.Text), Db_Detials.CompID, Db_Detials.YearID, RuntimeHelpers.GetObjectValue(Interaction.IIf(this.cboLedger.SelectedValue == null, 0, RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue))) })), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                        }
        //                        break;

        //                    case "sp_WeaverLoomwiseStock":
        //                    case "sp_DeptwiseBeamCutBalance":
        //                        if (Information.IsDate(this.dtTo.Text))
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format("''{0}'', {1}, {2}, {3}", new object[] { Localization.ToSqlDateString(this.dtTo.Text), Db_Detials.CompID, Db_Detials.YearID, RuntimeHelpers.GetObjectValue(Interaction.IIf(this.cboLedger.SelectedValue == null, 0, RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue))) })), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please Enter To Date");
        //                        }
        //                        break;

        //                    case "sp_DeliveryRefNotInInvoice":
        //                        if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format("''{0}'',''{1}'', {2}, {3}", new object[] { Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text), Db_Detials.CompID, Db_Detials.YearID })), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please Enter From Date and To Date");
        //                        }
        //                        break;

        //                    case "sp_LedgerRptDayWise_Print":
        //                    case "sp_LedgerRptEntyWise_Print":
        //                        if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                        {
        //                            if (this.cboLedger.SelectedValue != null)
        //                            {
        //                                this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" {4}, {0}, {1}, ''{2}'', ''{3}''", new object[] { Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text), RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue) })), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please Select Ledger");
        //                            }
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                        }
        //                        break;

        //                }
        //                if ((sp_Name == "sp_LedgerRptDayWise_Print") || (sp_Name == "sp_LedgerRptEntyWise_Print"))
        //                {

        //                }
        //                else
        //                {
        //                    switch (sp_Name)
        //                    {
        //                        case "sp_FetchFabricDyingLotStkDtls":
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" ''{0}'', {1}, {2}, {3}", new object[] { Localization.ToSqlDateString(this.dtTo.Text), RuntimeHelpers.GetObjectValue(Interaction.IIf(this.cboLedger.SelectedValue == null, 0, RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue))), Db_Detials.CompID, Db_Detials.YearID })), false);
        //                            goto Label_13A1;

        //                        case "sp_LedgerRptMonthWise_Print":
        //                            if (this.cboLedger.SelectedValue != null)
        //                            {
        //                                this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" {0}, {1}, {2}", RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue), Db_Detials.CompID, Db_Detials.YearID)), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please Select Ledger");
        //                            }
        //                            goto Label_13A1;
        //                    }
        //                    if ((((((sp_Name == "sp_OutStandingCustomer_Billwise") || (sp_Name == "sp_OutStandingCustomer_Summary")) || ((sp_Name == "sp_OutStandingSupplier_Billwise") || (sp_Name == "sp_OutStandingSupplier_Summary"))) || (((sp_Name == "Sp_FetchMillwiseYarnStk") || (sp_Name == "sp_FetchWarpinYarnStk")) || ((sp_Name == "sp_FetchProcessYarnStk") || (sp_Name == "Sp_FetchWeaverYarnStk")))) || ((((sp_Name == "sp_FabricPendingYarnPurchOrderReport") || (sp_Name == "Sp_FetchYarnProdStock")) || ((sp_Name == "sp_FabricTypeWiseStock") || (sp_Name == "sp_FetchFabricUnitWiseStock"))) || (((sp_Name == "sp_FetchGodownFabricStk") || (sp_Name == "sp_FetchProcessFabricStk")) || ((sp_Name == "sp_FetchFabricPieceWiseStock") || (sp_Name == "sp_FabricPendingPuchaseOrderReport"))))) || ((sp_Name == "sp_FabricPendingSalesOrderReport") || (sp_Name == "Sp_FetchDeptYarnWiseStock")))
        //                    {
        //                        if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", new object[] { Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text), RuntimeHelpers.GetObjectValue(Interaction.IIf(this.cboLedger.SelectedValue == null, 0, RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue))) })), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                        }
        //                    }
        //                    else if ((((sp_Name == "sp_ItemWisePurchase") || (sp_Name == "sp_ItemWiseSales")) || ((sp_Name == "sp_PartyWisePurchase") || (sp_Name == "sp_PartyWiseSales"))) || (sp_Name == "sp_DayBook"))
        //                    {
        //                        if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", new object[] { Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text), RuntimeHelpers.GetObjectValue(Interaction.IIf(this.cboLedger.SelectedValue == null, 0, RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue))) })), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                        }
        //                    }
        //                    else if (sp_Name == "sp_WeaverWiseWeftIsuueReport")
        //                    {
        //                        if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[2] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", new object[] { Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text), RuntimeHelpers.GetObjectValue(Interaction.IIf(this.cboLedger.SelectedValue == null, 0, RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue))) })), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                        }
        //                    }
        //                    else if (((sp_Name == "sp_YarnShadeMAster") || (sp_Name == "sp_YarnColorMAster")) || (sp_Name == "sp_PODesignrpt"))
        //                    {
        //                        this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" {0}, {1}", Db_Detials.CompID, Db_Detials.YearID)), false);
        //                    }
        //                    else if ((((sp_Name == "sp_PoReport") || (sp_Name == "sp_PoSummeryReport")) || ((sp_Name == "sp_PoReport_New") || (sp_Name == "sp_PoSummeryReport_New"))) || ((sp_Name == "sp_FabOrderComlCompletionRpt") || (sp_Name == "sp_WeftCompletionRpt")))
        //                    {
        //                        if (this.cboLedger.SelectedValue != null)
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" ''{0}'', {1}, {2}", RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue), Db_Detials.CompID, Db_Detials.YearID)), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please select PO");
        //                        }
        //                    }
        //                    else if (sp_Name == "sp_DsgnWiseWeftConsuption")
        //                    {
        //                        this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" ''{0}'', {1}, {2}", RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue), Db_Detials.CompID, Db_Detials.YearID)), false);
        //                    }
        //                    else if (((sp_Name == "sp_LotWiseYarnReport") || (sp_Name == "sp_FabPendingSalesOrderRpt")) || ((sp_Name == "Sp_PendingSubSalesOrderRpt") || (sp_Name == "sp_OrderStatus")))
        //                    {
        //                        this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" {0}, {1}", Db_Detials.CompID, Db_Detials.YearID)), false);
        //                    }
        //                    else
        //                    {
        //                        this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[2] + string.Format("''{2}'', ''{3}'', {0}, {1}", new object[] { Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text) })), false);
        //                    }
        //                }
        //            }
        //        Label_13A1:
        //            dt.Select("", "Group_lvl");
        //            for (int i = 0; i <= (dt.Rows.Count - 1); i++)
        //            {
        //                if (Localization.ParseBoolean(dt.Rows[i]["IsGroup"].ToString()))
        //                {
        //                    this.UGrid_Rpt.DisplayLayout.Bands[0].SortedColumns.Add(dt.Rows[i]["Rpt_ColName"].ToString(), false, true);
        //                }
        //            }
        //            dt.Select("", "");
        //            BandEnumerator band = this.UGrid_Rpt.DisplayLayout.Bands.GetEnumerator();
        //            while (band.MoveNext())
        //            {
        //                ColumnEnumerator colEnum = band.Current.Columns.GetEnumerator();
        //                while (colEnum.MoveNext())
        //                {
        //                    UltraGridColumn column = colEnum.Current;
        //                    column.Width = Localization.ParseNativeInt(dt.Rows[column.Index]["Col_Width"].ToString());
        //                    column.Hidden = Localization.ParseBoolean(dt.Rows[column.Index]["IsShow"].ToString());
        //                    column.CellActivation = Activation.NoEdit;
        //                    column.Tag = dt.Rows[column.Index]["Qry_ColName"].ToString();
        //                    SummarySettings oSummary = null;
        //                    if (Localization.ParseBoolean(dt.Rows[column.Index]["IsAvg"].ToString()))
        //                    {
        //                        oSummary = column.Band.Summaries.Add(SummaryType.Average, column, SummaryPosition.UseSummaryPositionColumn);
        //                    }
        //                    if (Localization.ParseBoolean(dt.Rows[column.Index]["IsSum"].ToString()))
        //                    {
        //                        oSummary = column.Band.Summaries.Add(SummaryType.Sum, column, SummaryPosition.UseSummaryPositionColumn);
        //                        oSummary.DisplayFormat = "{0}";
        //                        oSummary.Appearance.TextHAlign = HAlign.Right;
        //                    }
        //                    if (Localization.ParseBoolean(dt.Rows[column.Index]["IsMin"].ToString()))
        //                    {
        //                        oSummary = column.Band.Summaries.Add(SummaryType.Minimum, column, SummaryPosition.UseSummaryPositionColumn);
        //                    }
        //                    if (Localization.ParseBoolean(dt.Rows[column.Index]["IsMax"].ToString()))
        //                    {
        //                        oSummary = column.Band.Summaries.Add(SummaryType.Maximum, column, SummaryPosition.UseSummaryPositionColumn);
        //                    }
        //                    if (Localization.ParseBoolean(dt.Rows[column.Index]["IsCount"].ToString()))
        //                    {
        //                        oSummary = column.Band.Summaries.Add(SummaryType.Count, column, SummaryPosition.UseSummaryPositionColumn);
        //                    }
        //                    ColumnFiltersCollection columnFilters = this.UGrid_Rpt.DisplayLayout.Bands[0].ColumnFilters;
        //                    if (!IsFilterClear)
        //                    {
        //                        columnFilters.ClearAllFilters();
        //                        IsFilterClear = true;
        //                    }
        //                    ColumnFiltersCollection colFiltr = columnFilters;
        //                    if ((dt.Rows[column.Index]["Filter_Text"].ToString() != "") & (dt.Rows[column.Index]["Filter_Type"].ToString() != ""))
        //                    {
        //                        columnFilters[column.Index].FilterConditions.Add((FilterComparisionOperator)Localization.ParseNativeInt(dt.Rows[column.Index]["Filter_Type"].ToString()), dt.Rows[column.Index]["Filter_Text"].ToString());
        //                    }
        //                    colFiltr = null;
        //                }
        //            }
        //            goto Label_2848;
        //        Label_17CE:
        //            this.UGrid_Rpt.Text = "";
        //            if (sIsSP == 0)
        //            {
        //                this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery 'Select * From {0}'", DB.GetSnglValue(string.Format("sp_ExecQuery 'Select QueryName From {0} Where QueryID = {1}'", "tbl_ReportQuery", strSelect[1]))), false);
        //            }
        //            else
        //            {
        //                string strsp_Qry = strSelect[2];
        //                switch (strsp_Qry)
        //                {
        //                    case "sp_FetchOverAllYarnStock":
        //                    case "Sp_WeaverYarnStockSummary":
        //                    case "sp_FetchOverAllBeamStock":
        //                    case "sp_FetchOverAllFabStock":
        //                        if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" {3},''{0}'', {1}, {2}", new object[] { Localization.ToSqlDateString(this.dtTo.Text), Db_Detials.CompID, Db_Detials.YearID, RuntimeHelpers.GetObjectValue(Interaction.IIf(this.cboLedger.SelectedValue == null, 0, Localization.ParseNativeDouble(Conversions.ToString(this.cboLedger.SelectedValue)))) })), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                        }
        //                        goto Label_2848;

        //                    case "Sp_FetchYarnWiseStock":
        //                    case "Sp_FetchShadeWiseStock":
        //                    case "Sp_FetchColorWiseStock":
        //                    case "sp_FetchFabricQualityWiseStk":
        //                    case "sp_FetchFabricShadeWiseStk":
        //                    case "Sp_FetchMillwiseYarnStk":
        //                    case "sp_FetchWarpinYarnStk":
        //                    case "sp_FetchProcessYarnStk":
        //                    case "Sp_FetchWeaverYarnStk":
        //                    case "sp_FabricPendingYarnPurchOrderReport":
        //                    case "Sp_FetchYarnProdStock":
        //                    case "sp_FabricTypeWiseStock":
        //                    case "sp_FetchFabricUnitWiseStock":
        //                    case "sp_FetchGodownFabricStk":
        //                    case "sp_FetchProcessFabricStk":
        //                    case "sp_FetchFabricPieceWiseStock":
        //                    case "sp_FabricPendingPuchaseOrderReport":
        //                    case "sp_FabricPendingSalesOrderReport":
        //                    case "sp_SupplierIntCalc":
        //                    case "sp_CustomerIntCalc":
        //                    case "Sp_InHouseYarnStock":
        //                    case "Sp_DyerYarnStock":
        //                    case "Sp_WarpingYarnStock":
        //                    case "Sp_YarnWeaverStock":
        //                    case "Sp_ProcesserYarnStock":
        //                    case "Sp_FetchDeptYarnWiseStock":
        //                    case "sp_FetchProcYrnPOStk":
        //                    case "Sp_InHouseYarnStockBoxWise":
        //                    case "sp_FetchFabricDesignWiseStk1":
        //                    case "sp_FetchFabricDesignWiseStk1_sum":
        //                    case "Sp_WeaverYarnStock":
        //                    case "Sp_WeaverDesignWiseBeamStk":
        //                    case "Sp_StockFabricLedger":
        //                        if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", new object[] { Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text), RuntimeHelpers.GetObjectValue(Interaction.IIf(this.cboLedger.SelectedValue == null, 0, Localization.ParseNativeDouble(Conversions.ToString(this.cboLedger.SelectedValue)))) })), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                        }
        //                        goto Label_2848;

        //                    case "sp_FetchFabricDesignWiseStk":
        //                    case "sp_POWiseStockRpt":
        //                    case "sp_POWiseStockRpt1":
        //                    case "sp_POWiseFinishFabircStock":
        //                        if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                        {
        //                            if (this.cboLedger.SelectedValue != null)
        //                            {
        //                                this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[2] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", new object[] { Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text), RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue) })), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please Select Location");
        //                            }
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                        }
        //                        goto Label_2848;

        //                    case "sp_WeaverLoomwiseStock":
        //                    case "sp_DeptwiseBeamCutBalance":
        //                        if (Information.IsDate(this.dtTo.Text))
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format("''{0}'', {1}, {2}, {3}", new object[] { Localization.ToSqlDateString(this.dtTo.Text), Db_Detials.CompID, Db_Detials.YearID, RuntimeHelpers.GetObjectValue(Interaction.IIf(this.cboLedger.SelectedValue == null, 0, Localization.ParseNativeDouble(Conversions.ToString(this.cboLedger.SelectedValue)))) })), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please Enter To Date");
        //                        }
        //                        goto Label_2848;
        //                }
        //                if ((strsp_Qry == "sp_LedgerRptDayWise_Print") || (strsp_Qry == "sp_LedgerRptEntyWise_Print"))
        //                {
        //                    if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                    {
        //                        if (this.cboLedger.SelectedValue != null)
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" {4}, {0}, {1}, ''{2}'', ''{3}''", new object[] { Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text), RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue) })), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please Select Ledger");
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                    }
        //                }
        //                else
        //                {
        //                    switch (strsp_Qry)
        //                    {
        //                        case "sp_FetchFabricDyingLotStkDtls":
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" '{0}', {1}, {2}, {3}", new object[] { Localization.ToSqlDateString(this.dtTo.Text), RuntimeHelpers.GetObjectValue(Interaction.IIf(this.cboLedger.SelectedValue == null, 0, RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue))), Db_Detials.CompID, Db_Detials.YearID })), false);
        //                            goto Label_2848;

        //                        case "sp_LedgerRptMonthWise_Print":
        //                            if (this.cboLedger.SelectedValue != null)
        //                            {
        //                                this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" {0}, {1}, {2}", RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue), Db_Detials.CompID, Db_Detials.YearID)), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please Select Ledger");
        //                            }
        //                            goto Label_2848;
        //                    }
        //                    if (((strsp_Qry == "sp_OutStandingCustomer_Billwise") || (strsp_Qry == "sp_OutStandingCustomer_Summary")) || ((strsp_Qry == "sp_OutStandingSupplier_Billwise") || (strsp_Qry == "sp_OutStandingSupplier_Summary")))
        //                    {
        //                        if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", new object[] { Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text), RuntimeHelpers.GetObjectValue(Interaction.IIf(this.cboLedger.SelectedValue == null, 0, RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue))) })), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                        }
        //                    }
        //                    else if ((((strsp_Qry == "sp_ItemWisePurchase") || (strsp_Qry == "sp_ItemWiseSales")) || ((strsp_Qry == "sp_PartyWisePurchase") || (strsp_Qry == "sp_PartyWiseSales"))) || (strsp_Qry == "sp_DayBook"))
        //                    {
        //                        if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format("''{2}'', ''{3}'', {0}, {1}", new object[] { Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text) })), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                        }
        //                    }
        //                    else if (strsp_Qry == "sp_WeaverWiseWeftIsuueReport")
        //                    {
        //                        if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[2] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", new object[] { Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text), RuntimeHelpers.GetObjectValue(Interaction.IIf(this.cboLedger.SelectedValue == null, 0, RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue))) })), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                        }
        //                    }
        //                    else if (strsp_Qry == "sp_EmployeeTaskRpt")
        //                    {
        //                        if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[2]), false);
        //                        }
        //                    }
        //                    else if (strsp_Qry == "sp_DailyWorkInwarRpt")
        //                    {
        //                        if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[2] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", new object[] { Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text), RuntimeHelpers.GetObjectValue(Interaction.IIf(this.cboLedger.SelectedValue == null, 0, RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue))) })), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                        }
        //                    }
        //                    else if (strsp_Qry == "sp_BookIssuePartyWise")
        //                    {
        //                        if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[2] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", new object[] { Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text), RuntimeHelpers.GetObjectValue(Interaction.IIf(this.cboLedger.SelectedValue == null, 0, RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue))) })), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                        }
        //                    }
        //                    else if (strsp_Qry == "sp_BookIssueDesignWise")
        //                    {
        //                        if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[2] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", new object[] { Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text), RuntimeHelpers.GetObjectValue(Interaction.IIf(this.cboLedger.SelectedValue == null, 0, RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue))) })), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                        }
        //                    }
        //                    else if (strsp_Qry == "sp_UserLoginStatusReport")
        //                    {
        //                        if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[2] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", new object[] { Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text), RuntimeHelpers.GetObjectValue(Interaction.IIf(this.cboLedger.SelectedValue == null, 0, RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue))) })), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                        }
        //                    }
        //                    else if (((strsp_Qry == "sp_YarnShadeMAster") || (strsp_Qry == "sp_YarnColorMAster")) || (strsp_Qry == "sp_PODesignrpt"))
        //                    {
        //                        this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" {0}, {1}", Db_Detials.CompID, Db_Detials.YearID)), false);
        //                    }
        //                    else if ((((strsp_Qry == "sp_PoReport") || (strsp_Qry == "sp_PoSummeryReport")) || ((strsp_Qry == "sp_PoReport_New") || (strsp_Qry == "sp_PoSummeryReport_New"))) || ((strsp_Qry == "sp_FabOrderComlCompletionRpt") || (strsp_Qry == "sp_WeftCompletionRpt")))
        //                    {
        //                        if (this.cboLedger.SelectedValue != null)
        //                        {
        //                            this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" ''{0}'', {1}, {2}", RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue), Db_Detials.CompID, Db_Detials.YearID)), false);
        //                        }
        //                        else
        //                        {
        //                            Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please select PO");
        //                        }
        //                    }
        //                    else if (strsp_Qry == "sp_DsgnWiseWeftConsuption")
        //                    {
        //                        this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" ''{0}'', {1}, {2}", RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue), Db_Detials.CompID, Db_Detials.YearID)), false);
        //                    }
        //                    else if (((strsp_Qry == "sp_LotWiseYarnReport") || (strsp_Qry == "sp_FabPendingSalesOrderRpt")) || ((strsp_Qry == "Sp_PendingSubSalesOrderRpt") || (strsp_Qry == "sp_OrderStatus")))
        //                    {
        //                        this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format(" {0}, {1}", Db_Detials.CompID, Db_Detials.YearID)), false);
        //                    }
        //                    else
        //                    {
        //                        this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[2] + string.Format("''{2}'', ''{3}'', {0}, {1}", new object[] { Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text) })), false);
        //                    }
        //                }
        //            }
        //        Label_2848:
        //            bndEnum = this.UGrid_Rpt.DisplayLayout.Bands.GetEnumerator();
        //            while (bndEnum.MoveNext())
        //            {
        //                UltraGridBand band1 = bndEnum.Current;
        //                fgDtls.Rows.Clear();
        //                ColumnEnumerator col = band1.Columns.GetEnumerator();
        //                while (col.MoveNext())
        //                {
        //                    UltraGridColumn column = col.Current;
        //                    fgDtls.Rows.Add();
        //                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[0].Value = column.Index + 1;
        //                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[1].Value = column.Header.Caption.ToString();
        //                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[2].Value = column.Header.Caption.ToString();
        //                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[3].Value = column.Header.Caption.ToString();
        //                    column.CellActivation = Activation.NoEdit;
        //                }
        //                this.IsLoadGrd = true;
        //            }
        //        }
        //        ColumnEnumerator col1 = this.UGrid_Rpt.DisplayLayout.Bands[0].Columns.GetEnumerator();
        //        while (col1.MoveNext())
        //        {
        //            UltraGridColumn column = col1.Current;
        //            column.Header.Appearance.BackColor = Color.White;
        //            column.Header.Appearance.BackColor2 = Color.White;
        //            column.Header.Appearance.FontData.Bold = DefaultableBoolean.True;
        //            column.Header.Appearance.BorderAlpha = Alpha.Transparent;
        //            using (SqlDataReader iDr = DB.GetRS("Select * From tbl_ReportConfigDtls Where Reportid = " + strSelect[0] + " And Col_Order = " + Conversions.ToString(column.Index)))
        //            {
        //                if (iDr.Read())
        //                {
        //                    switch (iDr["AlignType"].ToString())
        //                    {
        //                        case "C":
        //                            column.CellAppearance.TextHAlign = HAlign.Center;
        //                            break;

        //                        case "L":
        //                            column.CellAppearance.TextHAlign = HAlign.Left;
        //                            break;

        //                        case "R":
        //                            column.CellAppearance.TextHAlign = HAlign.Right;
        //                            break;
        //                    }
        //                    if (iDr["AlignType"].ToString() == "")
        //                    {
        //                        fgDtls.Rows[column.Index].Cells[4].Value = "L";
        //                    }
        //                    else
        //                    {
        //                        fgDtls.Rows[column.Index].Cells[4].Value = iDr["AlignType"].ToString();
        //                    }
        //                    column.Header.Caption = iDr["Rpt_ColName"].ToString();
        //                    column.Width = Conversions.ToInteger(iDr["Col_Width"].ToString());
        //                    fgDtls.Rows[column.Index].Cells[3].Value = iDr["Rpt_ColName"].ToString();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Navigate.ShowMessage(CIS_DialogIcon.Information, "", ex.Message);
        //    }
        //}

        #endregion

        #region UGrid_Rpt_InitializeLayout

        private void UGrid_Rpt_AfterColPosChanged(object sender, AfterColPosChangedEventArgs e)
        {
            if (this.IsLoadGrd)
            {
                IEnumerator enumerator = null;
                object instance = new DataGridViewRow();
                Infragistics.Win.UltraWinGrid.ColumnHeader[] columnHeaders = e.ColumnHeaders;

                for (int i = 0; i <= (fgDtls.Rows.Count - 1); i++)
                {
                    if (Operators.ConditionalCompareObjectEqual(fgDtls.Rows[i].Cells[2].Value, columnHeaders[0].Caption.ToString(), false))
                    {
                        Console.WriteLine(RuntimeHelpers.GetObjectValue(fgDtls.Rows[i].Cells[0].Value));
                        instance = fgDtls.Rows[i];
                        fgDtls.Rows.RemoveAt(i);
                        break;
                    }
                }
                fgDtls.Rows.Insert(Localization.ParseNativeInt(Conversions.ToString(columnHeaders[0].VisiblePosition)), new object[0]);
                try
                {
                    enumerator = ((IEnumerable)NewLateBinding.LateGet(instance, null, "Cells", new object[0], null, null, null)).GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        DataGridViewCell current = (DataGridViewCell)enumerator.Current;
                        fgDtls.Rows[Localization.ParseNativeInt(Conversions.ToString(columnHeaders[0].VisiblePosition))].Cells[current.ColumnIndex].Value = RuntimeHelpers.GetObjectValue(current.Value);
                    }
                }
                finally
                {
                    if (enumerator is IDisposable)
                    {
                        (enumerator as IDisposable).Dispose();
                    }
                }
                int num4 = fgDtls.Rows.Count - 1;
                for (int j = 0; j <= num4; j++)
                {
                    fgDtls.Rows[j].Cells[0].Value = j;
                }
            }
        }

        private void UGrid_Rpt_AfterSummaryDialog(object sender, AfterSummaryDialogEventArgs e)
        {
            if (e.SummariesChanged)
            {
                IEnumerator enumerator = null;
                try
                {
                    enumerator = ((IEnumerable)e.Column.Band.Summaries).GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        SummarySettings current = (SummarySettings)enumerator.Current;
                        if ((e.Column == current.SourceColumn) && (typeof(decimal) == e.Column.DataType))
                        {
                            switch (current.SummaryType)
                            {
                                case SummaryType.Average:
                                case SummaryType.Sum:
                                    {
                                        current.DisplayFormat = " {0:0.00}";
                                        current.Appearance.TextHAlign = HAlign.Right;
                                        continue;
                                    }
                                case SummaryType.Count:
                                    {
                                        current.DisplayFormat = " {0:0}";
                                        current.Appearance.TextHAlign = HAlign.Right;
                                        continue;
                                    }
                            }
                            string str = current.SummaryType.ToString();
                            current.Appearance.TextHAlign = HAlign.Default;
                        }
                    }
                }
                finally
                {
                    if (enumerator is IDisposable)
                    {
                        (enumerator as IDisposable).Dispose();
                    }
                }
            }
        }

        private void UGrid_Rpt_InitializePrint(object sender, CancelablePrintEventArgs e)
        {

            //ClassModule_define.ClsPrint _ClsPrint = new ClassModule_define.ClsPrint(UGrid_Rpt, "header doc text");
            //_ClsPrint.PrintForm();
            this.SetupPrint(e);
        }

        #endregion

        #region Report Controls

        #region For New Report Events

        private void btnNew_Click(object sender, EventArgs e)
        {
            r_NewRpt r_NewUC = new r_NewRpt();
            Popup pp_NewRpt = new Popup(r_NewUC)
            {
                Resizable = true
            };
            if (SystemInformation.IsComboBoxAnimationEnabled)
            {
                pp_NewRpt.ShowingAnimation = PopupAnimations.Slide | PopupAnimations.TopToBottom;
                pp_NewRpt.HidingAnimation = PopupAnimations.Slide | PopupAnimations.BottomToTop;
            }
            else
            {
                pp_NewRpt.ShowingAnimation = pp_NewRpt.HidingAnimation = PopupAnimations.None;
            }
            r_NewRpt.sReportName = "";
            r_NewRpt.IsNewRpt = true;
            r_NewRpt.cboRptlst = this.cboRptlst;
            r_NewRpt.UGrid_Rpt = this.UGrid_Rpt;
            r_NewRpt.fgDtls = fgDtls;
            r_NewRpt.iIDentity = this.iIDentity;
            pp_NewRpt.Show((System.Windows.Forms.Button)sender);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveRptSettings(false);
        }

        private void SaveRptSettings(bool IsNewRpt = false)
        {
            try
            {
                string strQryDtls = " Insert Into tbl_ReportConfigDtls (ReportID, Qry_ColName, Rpt_ColName, Col_Order, Col_Width, IsShow, IsGroup, Group_lvl, IsAvg, IsSum, IsMin, IsMax, IsCount, Filter_Type, Filter_Text, AlignType) Values((#CodeID#), {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14});" + Environment.NewLine;
                string strInsertMain = string.Empty;
                string[] strSelect = this.cboRptlst.SelectedValue.ToString().Split(';');
                string strQryDtls1 = string.Format("Update {0} Set ReportName={1} where QueryID={2}", "tbl_ReportQuery", (txtReportName.Text == "" ? DB.SQuote(this.cboRptlst.Text) : DB.SQuote(txtReportName.Text)), strSelect[1]);
                DB.ExecuteSQL(strQryDtls1);
                //bool bCon = Localization.ParseBoolean(DB.GetSnglValue(string.Format("Select * from tbl_ReportConfigMain  Where QueryID=235 and ReportName='Stock Opening Register Detail ' and UserID=3")));
                if (strSelect[0] == "-1")
                {
                    strInsertMain = string.Format(" Insert Into {0} (QueryID, ReportName, Font_Name, Font_Size, UserID, CompanyID) Values({1}, {2}, {3}, {4}, {5}, {6});" + Environment.NewLine,
                                    "tbl_ReportConfigMain", strSelect[1], (txtReportName.Text == "" ? DB.SQuote(this.cboRptlst.Text) : DB.SQuote(txtReportName.Text)),
                                    DB.SQuote(this.UGrid_Rpt.Font.FontFamily.Name), this.UGrid_Rpt.Font.Size, Db_Detials.UserID, Db_Detials.CompID);
                    IsNewRpt = true;
                }
                else
                {
                    strInsertMain = string.Format(" Update {0} Set QueryID = {1}, ReportName = {2}, Font_Name = {3}, Font_Size = {4} Where ReportID = {5} and UserID={6} and CompanyID={7};" + Environment.NewLine,
                                    "tbl_ReportConfigMain", strSelect[1], (txtReportName.Text == "" ? DB.SQuote(this.cboRptlst.Text) : DB.SQuote(txtReportName.Text)),
                                    DB.SQuote(this.UGrid_Rpt.Font.FontFamily.Name), this.UGrid_Rpt.Font.Size, strSelect[0], Db_Detials.UserID, Db_Detials.CompID);
                    IsNewRpt = false;
                }

                string strInsertQry = string.Empty;
                int i = 0;

                foreach (UltraGridBand band in UGrid_Rpt.DisplayLayout.Bands)
                {
                    foreach (UltraGridColumn column in band.Columns)
                    {
                        string[] strSumry = new string[5];
                        ColumnFiltersCollection columnFilters = UGrid_Rpt.DisplayLayout.Bands[0].ColumnFilters;

                        foreach (SummarySettings oSummary in column.Band.Summaries)
                        {
                            if (oSummary.SourceColumn == column)
                            {
                                if (oSummary.SummaryType.ToString() == "Average")
                                {
                                    strSumry[0] = "'True'";
                                }
                                if (oSummary.SummaryType.ToString() == "Sum")
                                {
                                    strSumry[1] = "'True'";
                                }
                                if (oSummary.SummaryType.ToString() == "Minimum")
                                {
                                    strSumry[2] = "'True'";
                                }
                                if (oSummary.SummaryType.ToString() == "Maximum")
                                {
                                    strSumry[3] = "'True'";
                                }
                                if (oSummary.SummaryType.ToString() == "Count")
                                {
                                    strSumry[4] = "'True'";
                                }
                                //strSumry[Localization.ParseNativeInt(oSummary.SummaryType.ToString())] = "'True'";
                            }
                        }

                        for (int iSumry = 0; iSumry <= 4; iSumry++)
                        {
                            if (strSumry[iSumry] == null)
                            {
                                strSumry[iSumry] = "'False'";
                            }
                        }

                        string strCmprValue = string.Empty;
                        string strCmprOptr = string.Empty;
                        try
                        {
                            strCmprValue = DB.SQuote(columnFilters[column.Index].FilterConditions[0].CompareValue.ToString());
                            strCmprOptr = Localization.ParseNativeInt(columnFilters[column.Index].FilterConditions[0].ComparisionOperator.ToString()).ToString();

                        }
                        catch
                        {
                            strCmprValue = "NULL";
                            strCmprOptr = "NULL";
                        }

                        string sAlign = "";
                        if (fgDtls.Rows[i].Cells[(int)Col.Align].Value == null)
                            sAlign = "L";

                        strInsertQry += string.Format(strQryDtls, DB.SQuote(Conversions.ToString(fgDtls.Rows[i].Cells[(int)Col.Data_Col].Value)),
                            //DB.SQuote(column.Header.Caption), 
                                    DB.SQuote(Conversions.ToString(fgDtls.Rows[i].Cells[(int)Col.Data_Col].Value)),
                                    column.Index, column.Width,
                                    DB.SQuote(Conversions.ToString(column.Hidden)),
                                    DB.SQuote(Conversions.ToString(column.IsGroupByColumn)),
                                    Interaction.IIf(column.IsGroupByColumn, column.Level, -1), strSumry[0], strSumry[1],
                                    strSumry[2], strSumry[3], strSumry[4], strCmprOptr, strCmprValue,
                                    DB.SQuote(Conversions.ToString(sAlign)));
                        i++;

                    }
                }

                if (strInsertQry.Length != 0)
                {
                    strInsertQry = string.Format("Delete From {0} Where ReportID = (#CodeID#); ", "tbl_ReportConfigDtls") + strInsertQry;
                    DB.ExecuteTranscation(strInsertMain, Conversions.ToString(Localization.ParseNativeDouble(strSelect[0])), IsNewRpt, strInsertQry);
                }

                string StrQuery = string.Format(" sp_ShowReportList '" + Conversions.ToString(iIDentity) + "'," + Db_Detials.UserID + " ");
                string RptNm = txtReportName.Text == "" ? this.cboRptlst.Text : txtReportName.Text;

                this.cboRptlst.SelectedValueChanged -= new EventHandler(this.cboRptlst_SelectedValueChanged);
                Combobox_Setup.Fill_Combo(this.cboRptlst, StrQuery, "ReportName", "ReportID");
                CIS_MultiColumnComboBox.CIS_MultiColumnComboBox colFiltr = this.cboRptlst;
                colFiltr.ColumnWidths = "0;250";
                colFiltr.AutoComplete = true;
                colFiltr.AutoDropdown = true;
                colFiltr = null;
                this.cboRptlst.SelectedValueChanged += new EventHandler(this.cboRptlst_SelectedValueChanged);
                this.cboRptlst.Text = RptNm;
                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Report configuration saved successfully.");
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_DialogIcon.Information, "", ex.Message);
            }
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            r_NewRpt r_NewUC = new r_NewRpt();
            Popup pp_NewRpt = new Popup(r_NewUC)
            {
                Resizable = true
            };
            r_NewRpt.sReportName = "";
            r_NewRpt.IsNewRpt = true;
            r_NewRpt.cboRptlst = this.cboRptlst;
            r_NewRpt.UGrid_Rpt = this.UGrid_Rpt;
            r_NewRpt.fgDtls = fgDtls;
            r_NewRpt.iIDentity = this.iIDentity;
            pp_NewRpt.Show((System.Windows.Forms.Button)sender);
        }

        #endregion

        #region For Rename Column Events
        private void btnChgColumn_Click(object sender, EventArgs e)
        {
            r_UCColNM _rUCColNM = new r_UCColNM();
            Popup pp_ColNM = new Popup(_rUCColNM)
            {
                Resizable = true
            };
            if (SystemInformation.IsComboBoxAnimationEnabled)
            {
                pp_ColNM.ShowingAnimation = PopupAnimations.Slide | PopupAnimations.TopToBottom;
                pp_ColNM.HidingAnimation = PopupAnimations.Slide | PopupAnimations.BottomToTop;
            }
            else
            {
                pp_ColNM.ShowingAnimation = pp_ColNM.HidingAnimation = PopupAnimations.None;
            }
            r_UCColNM.UGrid_Rpt = this.UGrid_Rpt;
            r_UCColNM.fgDtls = fgDtls;
            r_UCColNM.iIDentity = this.iIDentity;
            pp_ColNM.Show((System.Windows.Forms.Button)sender);
        }
        #endregion

        #region For Report Print Events
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                object instance = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                int iactionType = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster() Where MiscName='IsReportPrint'"));
                try
                {
                    DB.ExecuteSQL("INSERT INTO tbl_UserReportLog(MenuID, ReportID, IsCrystalReport, IsBarCode, IsChequePrint, ActionType, UserID, UserDt,StoreID, CompID, BranchID,YearID, IPAddress, MacAddress) VALUES(" + Localization.ParseNativeInt(Conversions.ToString(NewLateBinding.LateGet(instance, null, "iIDentity", new object[0], null, null, null))) + ", " + iReportID + ", 0, 0, 0, " + iactionType + "," + Db_Detials.UserID + ",getdate()" + "," + Db_Detials.StoreID + "," + Db_Detials.CompID + "," + Db_Detials.BranchID + "," + Db_Detials.YearID + "," + DB.SQuote(CommonCls.GetIP()) + "," + DB.SQuote(CommonCls.FetchMacId()) + ")");
                }
                catch { }
            }
            catch { }

            this.PrintReport();
        }

        private void PrintReport()
        {
            this.UGrid_Rpt.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.False;
            this.UGrid_Rpt.DisplayLayout.Override.AllowRowSummaries = AllowRowSummaries.False;
            this.UGrid_Rpt.DisplayLayout.Override.RowSelectorHeaderStyle = RowSelectorHeaderStyle.None;
            this.UGrid_Rpt.DisplayLayout.Override.HeaderStyle = HeaderStyle.Standard;
            BandEnumerator ex = this.UGrid_Rpt.DisplayLayout.Bands.GetEnumerator();
            while (ex.MoveNext())
            {
                ColumnEnumerator band = ex.Current.Columns.GetEnumerator();
                while (band.MoveNext())
                {
                    UltraGridColumn column = band.Current;
                    column.Header.Appearance.BackColor = Color.FromArgb(0xc6, 0xc6, 0xc6);
                    column.Header.Appearance.BorderColor = Color.Black;
                }
            }
            this.UPPrev.ShowDialog(this.UGrid_Rpt);
            BandEnumerator colEnum = this.UGrid_Rpt.DisplayLayout.Bands.GetEnumerator();
            while (colEnum.MoveNext())
            {
                ColumnEnumerator colFiltr = colEnum.Current.Columns.GetEnumerator();
                while (colFiltr.MoveNext())
                {
                    colFiltr.Current.Header.Appearance.BackColor = Color.White;
                }
            }

            this.UGrid_Rpt.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
            this.UGrid_Rpt.DisplayLayout.Override.AllowRowSummaries = AllowRowSummaries.BasedOnDataType;
            this.UGrid_Rpt.DisplayLayout.Override.RowSelectorHeaderStyle = RowSelectorHeaderStyle.ColumnChooserButton;
            this.UGrid_Rpt.DisplayLayout.Override.HeaderStyle = HeaderStyle.Default;
            this.UGrid_Rpt.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
        }

        private void UPPrev_Load(object sender, EventArgs e)
        {
            try
            {
                var _with1 = UPPrev;
                _with1.PreviewSettings.Zoom = 100 * 0.01;
                var _with2 = _with1.Document.DefaultPageSettings.Margins;
                _with2.Top = 5;
                _with2.Bottom = (int)0.25;
                _with2.Left = (int)0.17;
                _with2.Right = (int)0.25;

                _with1.Document.DocumentName = Application.CompanyName;
                _with1.Text = Application.CompanyName;
                _with1.ThumbnailAreaVisible = false;
            }
            catch
            {
            }

        }

        #endregion

        #region For Export Report Events
        private void ExportReport()
        {
            try
            {
                DialogResult result = this.saveFileDialog1.ShowDialog(this);
                if (result != DialogResult.Cancel)
                {
                    string fileName = this.saveFileDialog1.FileName;
                    this.firstRowIndex = -1;
                    if (this.saveFileDialog1.FilterIndex == 1)
                    {
                        this.UGridxlExport.Export(this.UGrid_Rpt, fileName);
                    }
                    else
                    {
                        this.UltraGridDocumentExporter1.Export(this.UGrid_Rpt, fileName, GridExportFileFormat.PDF);
                    }
                    Process.Start(fileName);
                }
            }
            catch
            {
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            this.ExportReport();
        }

        private void UGridxlExport_BeginExport(object sender, Infragistics.Win.UltraWinGrid.ExcelExport.BeginExportEventArgs e)
        {
            e.CurrentWorksheet.Workbook.CellReferenceMode = CellReferenceMode.R1C1;
        }

        private void UGridxlExport_HeaderCellExporting(object sender, Infragistics.Win.UltraWinGrid.ExcelExport.HeaderCellExportingEventArgs e)
        {
            if (e.GridHeader.Column.Key == "Total")
            {
                this.totalColumnIndex = e.CurrentColumnIndex;
            }
        }

        private void UGridxlExport_RowExporting(object sender, Infragistics.Win.UltraWinGrid.ExcelExport.RowExportingEventArgs e)
        {
            if (this.firstRowIndex == -1)
            {
                this.firstRowIndex = e.CurrentRowIndex;
            }
            this.lastRowIndex = e.CurrentRowIndex;
        }

        private void UGridxlExport_CellExporting(object sender, Infragistics.Win.UltraWinGrid.ExcelExport.CellExportingEventArgs e)
        {
            if (e.GridColumn.Key == "Total")
            {
                string str = "=R[0]C[-2] * R[0]C[-1]";
                WorksheetCell cell = e.CurrentWorksheet.Rows[e.CurrentRowIndex].Cells[e.CurrentColumnIndex];
                cell.ApplyFormula(str);
                cell.CellFormat.FormatString = "$#,##0.00";
                e.Cancel = true;
            }
        }

        private void UGridxlExport_RowExported(object sender, Infragistics.Win.UltraWinGrid.ExcelExport.RowExportedEventArgs e)
        {
            if ((e.GridRow.Cells != null) && e.GridRow.Cells.Exists("Photo"))
            {
                Image image = null;
                WorksheetImage shape = new WorksheetImage(image);
                Rectangle boundsInTwips = new WorksheetRegion(e.CurrentWorksheet, e.CurrentRowIndex - 3, 0, e.CurrentRowIndex - 1, 0).GetBoundsInTwips();
                shape.SetBoundsInTwips(e.CurrentWorksheet, boundsInTwips, true);
                e.CurrentWorksheet.Shapes.Add(shape);
                e.CurrentWorksheet.Rows[e.CurrentRowIndex - 3].Cells[0].Value = null;
            }
        }
        #endregion

        private void btnExpand_Click(object sender, EventArgs e)
        {
            if (this.chkExpandAll)
            {
                this.UGrid_Rpt.Rows.ExpandAll(true);
                this.btnExpand.Text = "Collapse";
                this.chkExpandAll = false;
                this.btnExpand.Image = CIS_Textil.Properties.Resources.BlackCollapse;

            }
            else
            {
                this.chkExpandAll = true;
                this.btnExpand.Text = "Expand";
                this.UGrid_Rpt.Rows.CollapseAll(true);
                this.btnExpand.Image = CIS_Textil.Properties.Resources.BlackExpand;
            }
        }

        private void btnFont_Click(object sender, EventArgs e)
        {
            r_UCFont.UGrid_Rpt = this.UGrid_Rpt;
            r_UCFont.iIDentity = this.iIDentity;
            this.pp_FontUC.Show((System.Windows.Forms.Button)sender);
        }

        private void DeleteReport()
        {
            try
            {
                if (CIS_Dialog.Show("Do you want to delete this record?", "System Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string[] strSelect = this.cboRptlst.SelectedValue.ToString().Split(new char[] { ';' });
                    DB.ExecuteSQL(string.Format(" delete from {0} where  reportid = {1};", "tbl_ReportConfigDtls", strSelect[0].ToString()) + string.Format(" delete from {0} where UserID=" + Db_Detials.UserID + " and CompanyID=" + Db_Detials.CompID + " and reportid = {1};", "tbl_ReportConfigMain", strSelect[0].ToString()));
                    Navigate.ShowMessage(CIS_DialogIcon.SecuritySuccess, "Success", "Record Deleted Successfully.");
                    CIS_MultiColumnComboBox.CIS_MultiColumnComboBox ex = this.cboRptlst;
                    ex.DataSource = DB.GetDT(" sp_ShowReportList '" + Conversions.ToString(this.iIDentity) + "'," + Db_Detials.UserID + " ", false);
                    ex.DisplayMember = "ReportName";
                    ex.ValueMember = "ReportID";
                    ex.SelectedIndex = 0;
                    ex = null;
                    this.UGrid_Rpt.Text = "";
                    this.UGrid_Rpt.DataSource = null;
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                ProjectData.ClearProjectError();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            this.DeleteReport();
        }
        #endregion

        private void UGrid_Rpt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (UGrid_Rpt.Rows.Count > 0)
                {
                    if (this.WindowState != FormWindowState.Minimized)
                    {
                        UltraGridRow activeRow = UGrid_Rpt.ActiveRow;
                        string strcolumnValue = activeRow.Cells[0].Value.ToString();
                        string[] strReportname = cboRptlst.SelectedValue.ToString().Split(';');
                        string strModuleID = DB.GetSnglValue("Select ParentMenuID from tbl_ReportQuery where QueryName=" + "'" + strReportname[2] + "'" + "");

                        if (Localization.ParseNativeInt(strModuleID) > 0)
                        {
                            this.WindowState = FormWindowState.Minimized;
                            EventHandles.ShowbyFormID(strModuleID, null, -1, -1, -1, 0);
                            object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                            Navigate.ShowbyID(objectValue, "[" + ((DataTable)NewLateBinding.LateGet(objectValue, null, "ds", new object[0], null, null, null)).Columns[0].ColumnName + "]", (int)Math.Round(Conversion.Val(activeRow.Cells[0].Text)));
                        }
                    }
                }
            }
        }

        public bool ValidateForm()
        {
            bool ValidateForm = false;
            try
            {
                if (!CommonCls.CheckDate(this.dtFrom.Text, false) && !CommonCls.CheckDate(this.dtTo.Text, false))
                {
                    return true;
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                Navigate.logError(ex.Message, ex.StackTrace);
                ProjectData.ClearProjectError();
            }
            return ValidateForm;
        }

        private void SetupPrint(CancelablePrintEventArgs e)
        {
            this.UGrid_Rpt.DrawFilter = new MyDrawFilter();
            e.DefaultLogicalPageLayoutInfo.FitWidthToPages = 1;
            e.PrintDocument.PrinterSettings.PrintRange = PrintRange.AllPages;
            string strQry = "";
            using (SqlDataReader iDr = DB.GetRS(string.Format("Select * From {0} Where CompanyID = {1} And YearId = {2}", "Vw_CompanyMaster", Db_Detials.CompID, Db_Detials.YearID)))
            {
                if (iDr.Read())
                {
                    strQry = iDr["CompanyName"].ToString() + Environment.NewLine;
                    if (iDr["FactoryAdd"].ToString() != "-")
                    {
                        strQry = strQry + Environment.NewLine + "Factory : " + iDr["FactoryAdd"].ToString().Replace("-", "");
                    }
                    if (iDr["M_PhoneNo"].ToString() != "-")
                    {
                        strQry = strQry + Environment.NewLine + "Tel. No : " + iDr["M_PhoneNo"].ToString().Replace("/", "") + Environment.NewLine;
                    }
                    //For Company Name (Shows Company Name on Report)
                    strQry = Environment.NewLine + iDr["CompanyName"].ToString() + Environment.NewLine + Environment.NewLine;
                    strQry += Environment.NewLine + txtReportName.Text == "" ? this.cboRptlst.Text.ToUpper() : txtReportName.Text.ToUpper();
                    strQry += Environment.NewLine + "From Date : " + Localization.ToVBDateString(this.dtFrom.Text) + " To " + Localization.ToVBDateString(this.dtTo.Text);

                    e.DefaultLogicalPageLayoutInfo.PageHeader = strQry;
                    e.DefaultLogicalPageLayoutInfo.PageFooterBorderStyle = UIElementBorderStyle.Solid;
                    e.DefaultLogicalPageLayoutInfo.PageHeaderBorderStyle = UIElementBorderStyle.Solid;

                }
            }

            e.DefaultLogicalPageLayoutInfo.PageHeaderHeight = 100;
            Infragistics.Win.Appearance ex = e.DefaultLogicalPageLayoutInfo.PageHeaderAppearance;
            ex.FontData.Bold = DefaultableBoolean.True;
            ex.TextHAlign = HAlign.Center;
            ex.FontData.SizeInPoints = 10f;
            //ex = null;
            //strQry = Application.ProductName;
            e.DefaultLogicalPageLayoutInfo.PageFooter = Environment.NewLine + "Page <#> of <##> .";
            e.DefaultLogicalPageLayoutInfo.PageFooterBorderStyle = UIElementBorderStyle.Solid;
            e.DefaultLogicalPageLayoutInfo.PageFooterHeight = 50;
            Infragistics.Win.Appearance band = e.DefaultLogicalPageLayoutInfo.PageFooterAppearance;
            band.FontData.Bold = DefaultableBoolean.False;
            band.ForeColor = Color.Silver;
            band.TextHAlign = HAlign.Right;
            band.FontData.SizeInPoints = 8f;
            band.TextTrimming = TextTrimming.Character;
            band = null;

            e.DefaultLogicalPageLayoutInfo.ClippingOverride = ClippingOverride.Yes;
        }

        private void ShowCustomColumnChooserDialog()
        {
            if ((this.customColumnChooserDialog == null) || this.customColumnChooserDialog.IsDisposed)
            {
                this.customColumnChooserDialog = new CustomColumnChooser();
                this.customColumnChooserDialog.Owner = this;
                this.customColumnChooserDialog.Grid = this.UGrid_Rpt;
            }
            this.customColumnChooserDialog.Show();
        }

        private void cboRptlst_SelectedValueChanged(object sender, EventArgs e)
        {
            string[] strSelect = null;
            try
            {
                string FromDt = Localization.ToVBDateString(DB.GetSnglValue(string.Format("Select Yr_From From tbl_YearMaster Where YearID = {0}", Db_Detials.YearID)));
                string ToDate = Localization.ToVBDateString(Conversions.ToString(DateAndTime.Now.Date));

                if (cboRptlst.SelectedValue.ToString() != "System.Data.DataRowView" && cboRptlst.SelectedValue != null)
                {
                    strSelect = this.cboRptlst.SelectedValue.ToString().Split(new char[] { ';' });
                    try
                    {
                        using (IDataReader iDr = DB.GetRS("SELECT * from tbl_ReportQuery WHERE QueryID=" + strSelect[(int)ReportLst.QueryID]))
                        {
                            if (iDr.Read())
                            {
                                //txtReportName.Text = iDr["ReportName"].ToString();

                                if (iDr["LedgerName"].ToString() != "")
                                {
                                    this.lblLedger.Enabled = true;
                                    this.lblLedger.Text = iDr["LedgerName"].ToString();
                                    cboLedger.Enabled = true;
                                    Combobox_Setup.ComboType enumVal = (Combobox_Setup.ComboType)System.Enum.Parse(typeof(Combobox_Setup.ComboType), iDr["ComboBoxEnum"].ToString());
                                    Combobox_Setup.FillCbo(ref cboLedger, enumVal, "");
                                }
                                else
                                {
                                    this.lblLedger.Enabled = false;
                                    this.lblLedger.Text = iDr["LedgerName"].ToString();
                                    cboLedger.Enabled = false;
                                }

                                if (Localization.ParseBoolean(iDr["IsParaDt"].ToString()))
                                {
                                    this.dtFrom.Enabled = true;
                                    this.dtTo.Enabled = true;
                                }
                                else
                                {
                                    this.dtFrom.Enabled = false;
                                    this.dtTo.Enabled = false;
                                }

                                this.dtFrom.Text = FromDt;
                                this.dtTo.Text = ToDate;
                            }
                        }
                    }
                    catch
                    {
                        this.lblLedger.Enabled = false;
                        cboLedger.Enabled = false;
                    }
                }

                txtReportName.Text = cboRptlst.Text;

                if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT Count(0) From tbl_ReportQuery  Where QueryID=" + strSelect[(int)ReportLst.QueryID] + " and IsGraph=1")) > 0)
                    btnGraph.Visible = true;
                else
                    btnGraph.Visible = false;

                //switch (strSelect[2])
                //{
                //    case "sp_RackRegisterdtlsByProduct":
                //        if (!this.isMultyFilter)
                //        {
                //            this.lblLedger.Enabled = true;
                //            //this.cboLedger.Enabled = false;
                //            this.lblLedger.Text = "Product";
                //            this.dtFrom.Enabled = false;
                //            this.dtTo.Enabled = false;
                //            this.dtFrom.Text = FromDt;
                //            this.dtTo.Text = ToDate;
                //            this.cboLedger.Enabled = true;
                //            Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_Product, "");
                //        }
                //        break;

                //    case "sp_RackRegisterdtlsByProductDtls":
                //        if (!this.isMultyFilter)
                //        {
                //            this.lblLedger.Enabled = true;
                //            //this.cboLedger.Enabled = false;
                //            this.lblLedger.Text = "Product";
                //            this.dtFrom.Enabled = false;
                //            this.dtTo.Enabled = false;
                //            this.dtFrom.Text = FromDt;
                //            this.dtTo.Text = ToDate;
                //            this.cboLedger.Enabled = true;
                //            Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_Product, "");
                //        }
                //        break;

                //    case "sp_RackRegister":
                //        if (!this.isMultyFilter)
                //        {
                //            this.lblLedger.Enabled = false;
                //            this.lblLedger.Text = "Rack";
                //            //this.cboLedger.Enabled = false;
                //            this.dtFrom.Enabled = true;
                //            this.dtTo.Enabled = true;
                //            this.dtFrom.Text = FromDt;
                //            this.dtTo.Text = ToDate;
                //            this.cboLedger.Enabled = true;
                //            Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_Rack, "");
                //        }
                //        break;

                //    case "sp_InwardRegister":
                //        if (!this.isMultyFilter)
                //        {
                //            this.lblLedger.Enabled = false;
                //            this.lblLedger.Text = "Rack";
                //            //this.cboLedger.Enabled = false;
                //            this.dtFrom.Enabled = true;
                //            this.dtTo.Enabled = true;
                //            this.dtFrom.Text = FromDt;
                //            this.dtTo.Text = ToDate;
                //            this.cboLedger.Enabled = false;

                //        }
                //        break;
                //    case "sp_InwardRegisterSummary":
                //        if (!this.isMultyFilter)
                //        {
                //            this.lblLedger.Enabled = false;
                //            this.lblLedger.Text = "Rack";
                //            //this.cboLedger.Enabled = false;
                //            this.dtFrom.Enabled = true;
                //            this.dtTo.Enabled = true;
                //            this.dtFrom.Text = FromDt;
                //            this.dtTo.Text = ToDate;
                //            this.cboLedger.Enabled = false;

                //        }
                //        break;
                //    case "sp_OutwardRegister":
                //        if (!this.isMultyFilter)
                //        {
                //            this.lblLedger.Enabled = false;
                //            this.lblLedger.Text = "Rack";
                //            //this.cboLedger.Enabled = false;
                //            this.dtFrom.Enabled = true;
                //            this.dtTo.Enabled = true;
                //            this.dtFrom.Text = FromDt;
                //            this.dtTo.Text = ToDate;
                //            this.cboLedger.Enabled = false;

                //        }
                //        break;
                //    case "sp_RackRegisterdtlsByRack":
                //        if (!this.isMultyFilter)
                //        {
                //            this.lblLedger.Enabled = true;
                //            this.lblLedger.Text = "Rack";
                //            //this.cboLedger.Enabled = false;
                //            this.dtFrom.Enabled = false;
                //            this.dtTo.Enabled = false;
                //            this.dtFrom.Text = FromDt;
                //            this.dtTo.Text = ToDate;
                //            this.cboLedger.Enabled = true;
                //            Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_Rack, "");
                //        }
                //        break;
                //    case "sp_RackRegisterdtlsByRackDtls":
                //        if (!this.isMultyFilter)
                //        {
                //            this.lblLedger.Enabled = true;
                //            this.lblLedger.Text = "Rack";
                //            //this.cboLedger.Enabled = false;
                //            this.dtFrom.Enabled = false;
                //            this.dtTo.Enabled = false;
                //            this.dtFrom.Text = FromDt;
                //            this.dtTo.Text = ToDate;
                //            this.cboLedger.Enabled = true;
                //            Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_Rack, "");
                //        }
                //        break;
                //    case "sp_RackRegisterdtlsByShelf":
                //        if (!this.isMultyFilter)
                //        {
                //            this.lblLedger.Enabled = true;
                //            this.lblLedger.Text = "Shelf";
                //            //this.cboLedger.Enabled = false;
                //            this.dtFrom.Enabled = false;
                //            this.dtTo.Enabled = false;
                //            this.dtFrom.Text = FromDt;
                //            this.dtTo.Text = ToDate;
                //            this.cboLedger.Enabled = true;
                //            Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_ShelfNo, "");
                //        }
                //        break;
                //    case "sp_RackRegisterdtlsByShelfDtls":
                //        if (!this.isMultyFilter)
                //        {
                //            this.lblLedger.Enabled = true;
                //            this.lblLedger.Text = "Shelf";
                //            //this.cboLedger.Enabled = false;
                //            this.dtFrom.Enabled = false;
                //            this.dtTo.Enabled = false;
                //            this.dtFrom.Text = FromDt;
                //            this.dtTo.Text = ToDate;
                //            this.cboLedger.Enabled = true;
                //            Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_ShelfNo, "");
                //        }
                //        break;
                //    case "sp_RackRegisterdtlsByColor":
                //        if (!this.isMultyFilter)
                //        {
                //            this.lblLedger.Enabled = true;
                //            this.lblLedger.Text = "Color";
                //            //this.cboLedger.Enabled = false;
                //            this.dtFrom.Enabled = false;
                //            this.dtTo.Enabled = false;
                //            this.dtFrom.Text = FromDt;
                //            this.dtTo.Text = ToDate;
                //            this.cboLedger.Enabled = true;
                //            Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_Colours, "");
                //        }
                //        break;
                //    case "sp_RackRegisterdtlsByColorDtls":
                //        if (!this.isMultyFilter)
                //        {
                //            this.lblLedger.Enabled = true;
                //            this.lblLedger.Text = "Color";
                //            //this.cboLedger.Enabled = false;
                //            this.dtFrom.Enabled = false;
                //            this.dtTo.Enabled = false;
                //            this.dtFrom.Text = FromDt;
                //            this.dtTo.Text = ToDate;
                //            this.cboLedger.Enabled = true;
                //            Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_Colours, "");
                //        }
                //        break;
                //    case "sp_RackRegisterdtlsBySize":
                //        if (!this.isMultyFilter)
                //        {
                //            this.lblLedger.Enabled = true;
                //            this.lblLedger.Text = "Size";
                //            //this.cboLedger.Enabled = false;
                //            this.dtFrom.Enabled = false;
                //            this.dtTo.Enabled = false;
                //            this.dtFrom.Text = FromDt;
                //            this.dtTo.Text = ToDate;
                //            this.cboLedger.Enabled = true;
                //            Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_Unit, "");
                //        }
                //        break;
                //    case "sp_RackRegisterdtlsBySizeDtls":
                //        if (!this.isMultyFilter)
                //        {
                //            this.lblLedger.Enabled = true;
                //            this.lblLedger.Text = "Size";
                //            //this.cboLedger.Enabled = false;
                //            this.dtFrom.Enabled = false;
                //            this.dtTo.Enabled = false;
                //            this.dtFrom.Text = FromDt;
                //            this.dtTo.Text = ToDate;
                //            this.cboLedger.Enabled = true;
                //            Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_Unit, "");
                //        }
                //        break;
                //    case "sp_CompleteStock":
                //        if (!this.isMultyFilter)
                //        {
                //            this.lblLedger.Enabled = true;
                //            this.lblLedger.Text = "ProductCate";
                //            //this.cboLedger.Enabled = false;
                //            this.dtFrom.Enabled = false;
                //            this.dtTo.Enabled = false;
                //            this.dtFrom.Text = FromDt;
                //            this.dtTo.Text = ToDate;
                //            this.cboLedger.Enabled = true;
                //            Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_ProductCategory, "");
                //        }
                //        break;
                //    case "sp_FreeRackShelfList":
                //        if (!this.isMultyFilter)
                //        {
                //            this.lblLedger.Enabled = true;
                //            this.lblLedger.Text = "Rack";
                //            //this.cboLedger.Enabled = false;
                //            this.dtFrom.Enabled = false;
                //            this.dtTo.Enabled = false;
                //            this.dtFrom.Text = FromDt;
                //            this.dtTo.Text = ToDate;
                //            this.cboLedger.Enabled = true;
                //            Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_Rack, "");
                //        }
                //        break;
                //    case "sp_RackRegisterdtlsByBrand":
                //        if (!this.isMultyFilter)
                //        {
                //            this.lblLedger.Enabled = true;
                //            this.lblLedger.Text = "Brand";
                //            //this.cboLedger.Enabled = false;
                //            this.dtFrom.Enabled = false;
                //            this.dtTo.Enabled = false;
                //            this.dtFrom.Text = FromDt;
                //            this.dtTo.Text = ToDate;
                //            this.cboLedger.Enabled = true;
                //            Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_Brand, "");
                //        }
                //        break;
                //    default:
                //        if (!this.isMultyFilter)
                //        {
                //            this.lblLedger.Enabled = false;
                //            //this.cboLedger.Enabled = false;
                //            this.dtFrom.Enabled = true;
                //            this.dtTo.Enabled = true;
                //            this.dtFrom.Text = FromDt;
                //            this.dtTo.Text = ToDate;
                //            this.cboLedger.Enabled = true;
                //            Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_Ledger, "");
                //        }
                //        break;


                //}
            }
            catch { }
        }

        private void frmReportTool_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                CloseForm();
            }
            else if (e.Control && e.KeyCode == Keys.G)
            {
                btnExpand_Click(null, null);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void CloseForm()
        {
            if (CIS_Dialog.Show("Do you want to close this Form?", GetAssemblyInfo.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    object instance = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                    int iactionType = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster() Where MiscName='IsFormClose'"));
                    try
                    {
                        DB.ExecuteSQL("INSERT INTO tbl_UserReportLog(MenuID, ReportID, IsCrystalReport, IsBarCode, IsChequePrint, ActionType, UserID, UserDt,StoreID, CompID, BranchID,YearID, IPAddress, MacAddress) VALUES(" + Localization.ParseNativeInt(Conversions.ToString(NewLateBinding.LateGet(instance, null, "iIDentity", new object[0], null, null, null))) + ", " + iReportID + ", 0, 0, 0, " + iactionType + "," + Db_Detials.UserID + ",getdate()" + "," + Db_Detials.StoreID + "," + Db_Detials.CompID + "," + Db_Detials.BranchID + "," + Db_Detials.YearID + "," + DB.SQuote(CommonCls.GetIP()) + "," + DB.SQuote(CommonCls.FetchMacId()) + ")");
                    }
                    catch { }
                }
                catch { }
                this.Close();

                #region Collapse MDI Menu Dock
                object objMDI1 = RuntimeHelpers.GetObjectValue(Navigate.GetForm_byName("MDIMain"));
                dynamic objfrm = objMDI1;
                try
                {
                    objfrm.pnlDockTop.Expand = true;
                }
                catch { }
                #endregion

                object objchld = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                if (objchld == null)
                {
                    dynamic objChldfrm = objchld;
                    objfrm.tblpnl_HelpText.Visible = false;
                    objfrm.lblNavigationPath.Text = "";
                }
                else
                {
                    dynamic objChldfrm = objchld;
                    objfrm.tblpnl_HelpText.Visible = true;
                    objfrm.lblNavigationPath.Text = "You are here: " + DB.GetSnglValue("select Menu_Path from [fn_MenuHierarchey](" + objChldfrm.iIDentity + ")");

                    if (DB.GetSnglValue("SELECT MenuType from tbl_MenuMaster WHERE MenuID = " + objChldfrm.iIDentity) == "R")
                    {
                        objfrm.tblpnl_HelpText.Visible = true;
                        objfrm.lblHelpText_Form.Text = AppMsg.REPORTS;
                    }
                    else
                    {
                        objfrm.tblpnl_HelpText.Visible = true;
                        objfrm.lblHelpText_Form.Text = AppMsg.FORMS;
                    }
                }
            }
        }

        private void cboFillter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFillterType.Text == "Quarterly")
            {
                try
                {
                    if (cboFillter.SelectedValue.ToString() != "0")
                    {
                        using (IDataReader idr = DB.GetRS("select * from fn_GetQuater() where YearID=" + Db_Detials.YearID + " and Quarter=" + CommonLogic.SQuote(cboFillter.Text) + ""))
                        {
                            string strQuaterDate = string.Empty;
                            string[] strQuaterDateSplit = null;
                            if (idr.Read())
                            {
                                if (cboFillterType.Text == "FRIST QUARTER")
                                {
                                    strQuaterDate = idr["QuaterDate"].ToString();
                                    strQuaterDateSplit = strQuaterDate.Split(' ');
                                    dtFrom.Text = Localization.ToVBDateString(strQuaterDateSplit[0].ToString());
                                    dtTo.Text = Localization.ToVBDateString(strQuaterDateSplit[2].ToString());
                                }
                                else if (cboFillterType.Text == "SECOND QUARTER")
                                {
                                    strQuaterDate = idr["QuaterDate"].ToString();
                                    strQuaterDateSplit = strQuaterDate.Split(' ');
                                    dtFrom.Text = Localization.ToVBDateString(strQuaterDateSplit[0].ToString());
                                    dtTo.Text = Localization.ToVBDateString(strQuaterDateSplit[2].ToString());

                                }
                                else if (cboFillterType.Text == "THIRD QUARTER")
                                {
                                    strQuaterDate = idr["QuaterDate"].ToString();
                                    strQuaterDateSplit = strQuaterDate.Split(' ');
                                    dtFrom.Text = Localization.ToVBDateString(strQuaterDateSplit[0].ToString());
                                    dtTo.Text = Localization.ToVBDateString(strQuaterDateSplit[2].ToString());
                                }
                                else
                                {
                                    strQuaterDate = idr["QuaterDate"].ToString();
                                    strQuaterDateSplit = strQuaterDate.Split(' ');
                                    dtFrom.Text = Localization.ToVBDateString(strQuaterDateSplit[0].ToString());
                                    dtTo.Text = Localization.ToVBDateString(strQuaterDateSplit[2].ToString());
                                }

                            }
                        }
                    }
                    else
                    {
                        this.dtFrom.Text = Localization.ToVBDateString(DB.GetSnglValue(string.Format("Select Yr_From From tbl_YearMaster Where YearID = {0}", Db_Detials.YearID)));
                        this.dtTo.Text = Localization.ToVBDateString(Conversions.ToString(DateAndTime.Now.Date));
                    }

                }
                catch (Exception ex2)
                {
                    Navigate.logError(ex2.Message, ex2.StackTrace);
                }
            }
            else if (cboFillterType.Text == "Yearly")
            {
                try
                {
                    if (cboFillter.SelectedValue.ToString() != "0")
                    {
                        using (IDataReader idr = DB.GetRS("select * from fn_GetYear() where YearID=" + Db_Detials.YearID + " and Year=" + CommonLogic.SQuote(cboFillter.Text) + ""))
                        {
                            string strStartYear = DB.GetSnglValue("select Year(Yr_From) from tbl_YearMaster where YearID=" + Db_Detials.YearID + "");
                            string strEndYear = DB.GetSnglValue("select Year(Yr_To) from tbl_YearMaster where YearID=" + Db_Detials.YearID + "");
                            string strfromTo = string.Empty;
                            string[] strFromToSplit = null;
                            if (idr.Read())
                            {
                                if (strStartYear == cboFillterType.Text)
                                {
                                    strfromTo = idr["FromTo"].ToString();
                                    strFromToSplit = strfromTo.Split(' ');
                                    dtFrom.Text = Localization.ToVBDateString(strFromToSplit[0].ToString());
                                    dtTo.Text = Localization.ToVBDateString(strFromToSplit[2].ToString());
                                }
                                else if (strEndYear == cboFillterType.Text)
                                {
                                    strfromTo = idr["FromTo"].ToString();
                                    strFromToSplit = strfromTo.Split(' ');
                                    dtFrom.Text = Localization.ToVBDateString(strFromToSplit[0].ToString());
                                    dtTo.Text = Localization.ToVBDateString(strFromToSplit[2].ToString());
                                }
                            }
                        }
                    }
                    else
                    {
                        this.dtFrom.Text = Localization.ToVBDateString(DB.GetSnglValue(string.Format("Select Yr_From From tbl_YearMaster Where YearID = {0}", Db_Detials.YearID)));
                        this.dtTo.Text = Localization.ToVBDateString(Conversions.ToString(DateAndTime.Now.Date));
                    }
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                }
            }
            else
            {
                try
                {
                    if (cboFillter.SelectedValue.ToString() != "0")
                    {
                        using (IDataReader idr = DB.GetRS("select * from fn_GetMonth() where YearID=" + Db_Detials.YearID + " and Month=" + CommonLogic.SQuote(cboFillter.Text) + ""))
                        {
                            string strMonthdate = string.Empty;
                            string[] strMonthDateSplit = null;
                            if (idr.Read())
                            {
                                strMonthdate = idr["MonthDate"].ToString();
                                strMonthDateSplit = strMonthdate.Split(' ');
                                dtFrom.Text = Localization.ToVBDateString(strMonthDateSplit[0].ToString());
                                dtTo.Text = Localization.ToVBDateString(strMonthDateSplit[2].ToString());
                            }
                        }
                    }
                    else
                    {
                        this.dtFrom.Text = Localization.ToVBDateString(DB.GetSnglValue(string.Format("Select Yr_From From tbl_YearMaster Where YearID = {0}", Db_Detials.YearID)));
                        this.dtTo.Text = Localization.ToVBDateString(Conversions.ToString(DateAndTime.Now.Date));
                    }
                }
                catch (Exception ex1)
                {
                    Navigate.logError(ex1.Message, ex1.StackTrace);
                }
            }

        }

        private void cboFillterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboFillterType.Text == "Monthly")
                {
                    Combobox_Setup.FillCbo(ref cboFillter, Combobox_Setup.ComboType.Mst_Month, "YearID=" + Db_Detials.YearID + "");
                }
                else if (cboFillterType.Text == "Quarterly")
                {
                    Combobox_Setup.FillCbo(ref cboFillter, Combobox_Setup.ComboType.Mst_Quater, "YearID=" + Db_Detials.YearID + "");
                }
                else if (cboFillterType.Text == "Yearly")
                {
                    Combobox_Setup.FillCbo(ref cboFillter, Combobox_Setup.ComboType.Mst_Year, "YearID=" + Db_Detials.YearID + "");
                }
                else
                {
                    this.dtFrom.Text = Localization.ToVBDateString(DB.GetSnglValue(string.Format("Select Yr_From From tbl_YearMaster Where YearID = {0}", Db_Detials.YearID)));
                    this.dtTo.Text = Localization.ToVBDateString(Conversions.ToString(DateAndTime.Now.Date));
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btnExport_Click_1(object sender, EventArgs e)
        {
            ExportReport();

            try
            {
                object instance = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                int iactionType = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster() Where MiscName='IsReportExport'"));
                try
                {
                    DB.ExecuteSQL("INSERT INTO tbl_UserReportLog(MenuID, ReportID, IsCrystalReport, IsBarCode, IsChequePrint, ActionType, UserID, UserDt,StoreID ,CompID, BranchID,YearID, IPAddress, MacAddress) VALUES(" + Localization.ParseNativeInt(Conversions.ToString(NewLateBinding.LateGet(instance, null, "iIDentity", new object[0], null, null, null))) + ", " + iReportID + ", 0, 0, 0, " + iactionType + "," + Db_Detials.UserID + ",getdate()" + "," + Db_Detials.StoreID + "," + Db_Detials.CompID + "," + Db_Detials.BranchID + "," + Db_Detials.YearID + "," + DB.SQuote(CommonCls.GetIP()) + "," + DB.SQuote(CommonCls.FetchMacId()) + ")");
                }
                catch { }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void UltraGridPrintDocument1_PageHeaderPrinting(object sender, HeaderFooterPrintingEventArgs e)
        {
            DataTable Dt_ChkImg = DB.GetDT("SELECT Image from tbl_CompanyMaster WHERE CompanyID=" + Db_Detials.CompID + "", false);
            if (Dt_ChkImg.Rows.Count > 0)
            {

                UltraPrintDocument pd = (UltraPrintDocument)sender;
                if (pd.PageNumber == 1)
                {
                    Image imgCmp = null;
                    DataTable Dt_Img = DB.GetDT("SELECT Image from tbl_CompanyMaster WHERE CompanyID=" + Db_Detials.CompID, false);
                    if (Dt_Img.Rows.Count > 0)
                    {
                        try
                        {
                            if (Dt_Img.Rows[0][0].ToString() != "")
                            {
                                byte[] imageData = (byte[])Dt_Img.Rows[0][0];
                                //Get image data from gridview column.
                                //Initialize image variable
                                //Read image data into a memory stream
                                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                                {
                                    ms.Write(imageData, 0, imageData.Length);

                                    //Set image variable value using memory stream.
                                    imgCmp = Image.FromStream(ms, true);
                                }
                            }
                        }
                        catch { }
                    }
                    //Font font = new Font("Times New Roman", 15.0f);
                    //SolidBrush myBrush = new SolidBrush(Color.Black);
                    //Point point1 = new Point(100, 10);
                    //StringFormat sf = new StringFormat();
                    //sf.LineAlignment = StringAlignment.Near;
                    //sf.Alignment = StringAlignment.Center;
                    long x = 0;
                    long y = 0;
                    if (pd.DefaultPageSettings.Landscape == true)
                    {
                        x = 320;
                        y = 10;
                    }
                    else
                    {
                        x = 200;
                        y = 10;
                    }

                    long width = 518;
                    long height = 45;

                    try
                    {
                        if (imgCmp != null)
                        {
                            e.Graphics.DrawImage(imgCmp, new System.Drawing.RectangleF(x, y, width, height));
                            //pd.OriginAtMargins = true;
                            //pd.DefaultPageSettings.Landscape = false;
                            //pd.DefaultPageSettings.Margins.Top = 100;
                            //pd.DefaultPageSettings.Margins.Left = 0;
                            //pd.DefaultPageSettings.Margins.Right = 50;
                            //pd.DefaultPageSettings.Margins.Bottom = 0;
                        }
                    }
                    catch { }
                }
            }
        }

        private void UGrid_Rpt_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT COUNT(0) FROM tbl_ReportQuery WHERE IsShowImg=1 and QueryID=" + iReportID)) > 0)
            {
                UltraGridLayout layout = e.Layout;
                UltraGridBand band = layout.Bands[0];
                e.Layout.Override.DefaultRowHeight = 50;
                e.Layout.Override.CellDisplayStyle = CellDisplayStyle.FormattedText;
                e.Layout.Override.CellAppearance.TextVAlign = VAlign.Middle;
                UltraGridColumn imageColumn = band.Columns.Add("ProductImage");
                imageColumn.DataType = typeof(Image);
                imageColumn.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Image;
                imageColumn.Header.VisiblePosition = 0;

                using (IDataReader iDr = DB.GetRS("SELECT ImgTable,ImgColName,ImgForColName,ImgForColPosition_InSP,AttachImagesImEmail FROM tbl_ReportQuery WHERE QueryID=" + iReportID))
                {
                    if (iDr.Read())
                    {
                        sTableNM_Img = iDr["ImgTable"].ToString();
                        sColNM_Img = iDr["ImgColName"].ToString();
                        sImgForColNM = iDr["ImgForColName"].ToString();
                        iColPosition = Localization.ParseNativeInt(iDr["ImgForColPosition_InSP"].ToString());
                        isAttachImgInEmail = Localization.ParseBoolean(iDr["AttachImagesImEmail"].ToString());
                    }
                }
            }
        }

        private void UGrid_Rpt_InitializeRow(object sender, InitializeRowEventArgs e)
        {
            if (e.Row.Cells.Exists("ProductImage"))
            {
                if ((e.Row.Cells[iColPosition].Value != null) && (e.Row.Cells[iColPosition].Value.ToString() != ""))
                    e.Row.Cells["ProductImage"].Value = GetImage(e.Row.Cells[iColPosition].Value.ToString());
            }
        }

        public Image GetImage(string sItemName)
        {
            if (sItemName != "")
            {
                try
                {
                    DataTable Dt = DB.GetDT("SELECT " + sColNM_Img + " from " + sTableNM_Img + " where " + sColNM_Img + " IS NOT NULL AND " + sImgForColNM + " = '" + sItemName + "'", false);
                    //object abc = (object)DB.GetSnglValue("select Picture from tbl_ImageStore where AddressID=" + strAddressID);
                    if (Dt.Rows.Count > 0)
                    {
                        byte[] imageData = (byte[])Dt.Rows[0][0];
                        //Get image data from gridview column.
                        //Initialize image variable
                        Image newImage;
                        //Read image data into a memory stream
                        Size outputSize = new Size(100, 100);
                        Bitmap backgroundBitmap = new Bitmap(outputSize.Width, outputSize.Height);

                        using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                        {
                            ms.Write(imageData, 0, imageData.Length);

                            //Set image variable value using memory stream.
                            newImage = Image.FromStream(ms, true);

                            using (Bitmap tempBitmap = new Bitmap(newImage))
                            {
                                using (Graphics g = Graphics.FromImage(backgroundBitmap))
                                {
                                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                    // Get the set of points that determine our rectangle for resizing.
                                    Point[] corners = {
                                    new Point(0, 0),
                                    new Point(backgroundBitmap.Width, 0),
                                    new Point(0, backgroundBitmap.Height)
                                };
                                    g.DrawImage(tempBitmap, corners);
                                }
                            }
                            //newImage.Size = new System.Drawing.Size(100, 100);
                        }
                        return backgroundBitmap;
                    }
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return null;
                }
            }
            else
                return null;
        }

        private void btnEMail_Click(object sender, EventArgs e)
        {
            string _filename = string.Empty;
            string _Password = string.Empty;
            string _Host = string.Empty;
            string _PortNo = string.Empty;
            string _Signature = string.Empty;
            string _FromEmail = string.Empty;
            DataGridView dgv_AttachFile = new DataGridView();
            string sstartPath = Application.StartupPath.ToString().Replace("bin\\Debug", "") + "EmailDoc";
            if (!Directory.Exists(sstartPath))
                Directory.CreateDirectory(sstartPath);

            CIS_Textbox txtToAddress = new CIS_Textbox();
            frmEmailAddressBook frmEmil = new frmEmailAddressBook();
            frmEmil.isEmail = true;
            frmEmil.isReportTool = true;
            frmEmil.refControl = txtToAddress;
            frmEmil.ShowDialog();
            _FromEmail = frmEmil.sFromEmailID;

            lblPleaseWait.Text = "Sending Mail, Please wait..";
            lblPleaseWait.Visible = true;
            Application.DoEvents();
            if (txtToAddress.Text.Trim().Length > 0)
            {
                string fileName = sstartPath + "\\" + cboRptlst.Text.Replace(" ", "") + "_" + Localization.ToSqlDateString(DateTime.Now.Date.ToString()).Replace("/", "_") + ".pdf";
                this.UltraGridDocumentExporter1.Export(this.UGrid_Rpt, fileName, GridExportFileFormat.PDF);

                using (IDataReader dr = DB.GetRS(string.Format("Select * from {0} Where MailAdd = {1} and CompID={2}", Db_Detials.tbl_MailingConfig, CommonLogic.SQuote(_FromEmail), Db_Detials.CompID)))
                {
                    while (dr.Read())
                    {
                        _Password = CommonLogic.UnmungeString(dr["Password"].ToString());
                        _Host = dr["Host"].ToString();
                        _PortNo = dr["PortNo"].ToString();
                        _Signature = dr["Signature"].ToString();
                    }
                }

                dgv_AttachFile.ColumnCount = 3;
                dgv_AttachFile.Rows.Clear();
                dgv_AttachFile.Rows.Add();

                dgv_AttachFile.Rows[0].Cells[0].Value = 1;
                dgv_AttachFile.Rows[0].Cells[1].Value = fileName;
                dgv_AttachFile.Rows[0].Cells[2].Value = Path.GetFileName(fileName);

                if (isAttachImgInEmail)
                {
                    sstartPath = Application.StartupPath.ToString().Replace("bin\\Debug", "") + "EmailDoc\\ProductImg\\" + cboRptlst.Text.Replace(" ", "") + "_" + Localization.ToSqlDateString(DateTime.Now.Date.ToString()).Replace("/", "_") + "_" + Db_Detials.UserID;
                    if (!Directory.Exists(sstartPath))
                        Directory.CreateDirectory(sstartPath);

                    DataTable Dt = new DataTable();
                    Dt = (DataTable)UGrid_Rpt.DataSource;
                    PictureBox pcbx = new PictureBox();
                    DataView view = new DataView(Dt);
                    DataTable distinctValues = view.ToTable(true, "ItemName");

                    if (distinctValues.Rows.Count > 0)
                    {
                        int iRowCnt = 1;
                        DataTable Dt_Img = DB.GetDT("SELECT " + sImgForColNM + "," + sColNM_Img + " from " + sTableNM_Img + " WHERE " + sColNM_Img + " IS NOT NULL", false);
                        foreach (DataRow r in distinctValues.Rows)
                        {
                            DataRow[] rst = Dt_Img.Select("" + sImgForColNM + "=" + CommonLogic.SQuote(distinctValues.Rows[iRowCnt - 1][0].ToString()));
                            if (rst.Length > 0)
                            {
                                byte[] imageData = (byte[])rst[0][1];
                                //Get image data from gridview column.
                                //Initialize image variable
                                Image newImage;
                                //Read image data into a memory stream
                                using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                                {
                                    ms.Write(imageData, 0, imageData.Length);

                                    //Set image variable value using memory stream.
                                    newImage = Image.FromStream(ms, true);
                                    pcbx.Image = newImage;
                                    pcbx.Image.Save(sstartPath + "\\" + r[sImgForColNM] + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                                }
                                dgv_AttachFile.Rows.Add();
                                dgv_AttachFile.Rows[iRowCnt].Cells[0].Value = 1;
                                dgv_AttachFile.Rows[iRowCnt].Cells[1].Value = sstartPath + "\\" + r[sImgForColNM] + ".jpg";
                                dgv_AttachFile.Rows[iRowCnt].Cells[2].Value = r[sImgForColNM] + ".jpg";
                                iRowCnt++;
                            }
                        }
                    }
                }

                //bool blnStatus = false;
                string sRetVal = string.Empty;
                try
                {
                    if (txtToAddress.Text.Trim() != "")
                    {
                        sRetVal = SendMail.sendMail("Report -" + cboRptlst.Text, _Signature, _FromEmail, _Password, txtToAddress.Text, _FromEmail, dgv_AttachFile, _Host, _PortNo, false);
                        string[] sRet = sRetVal.Split(';');
                        string sStatus = sRet[0].ToString();
                        if (sStatus.ToString() == "True")
                        {
                            Navigate.ShowMessage(CIS_DialogIcon.SecuritySuccess, "Success", "Mail Sent Successfully..");
                            try
                            {
                                object instance = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                                int iactionType = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster() Where MiscName='IsReportEmail'"));
                                try
                                {
                                    DB.ExecuteSQL("INSERT INTO tbl_UserReportLog(MenuID, ReportID, IsCrystalReport, IsBarCode, IsChequePrint, ActionType, UserID, UserDt,StoreID, CompID,BranchID,YearID, IPAddress, MacAddress) VALUES(" + Localization.ParseNativeInt(Conversions.ToString(NewLateBinding.LateGet(instance, null, "iIDentity", new object[0], null, null, null))) + ", " + iReportID + ", 0, 0, 0, " + iactionType + "," + Db_Detials.UserID + ",getdate()" + ","+Db_Detials.StoreID+"," + Db_Detials.CompID + ","+Db_Detials.BranchID+"," + Db_Detials.YearID + "," + DB.SQuote(CommonCls.GetIP()) + "," + DB.SQuote(CommonCls.FetchMacId()) + ")");
                                }
                                catch { }
                            }
                            catch { }
                        }
                        else
                        {
                            Navigate.ShowMessage(CIS_DialogIcon.Warning, "Error", "Mail Sending failed");
                        }
                    }
                    else
                    {
                        Navigate.ShowMessage(CIS_DialogIcon.Warning, "Error", "Please Enter To Email Address..");
                    }
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                    Navigate.ShowMessage(CIS_DialogIcon.Warning, "Error", "Mail Sending failed");
                }
            }
            lblPleaseWait.Visible = false;
            Application.DoEvents();
        }

        private void txtReportName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnGraph_Click(object sender, EventArgs e)
        {
            ultrchrt.Visible = true;
            UGrid_Rpt.Visible = false;
            string sQryID = DB.GetSnglValue("Select QueryID From tbl_ReportQuery  Where MenuID=" + iIDentity + " and IsGraph=1");
            string strQuery = DB.GetSnglValue("Select GraphQuery From tbl_ReportQuery  Where QueryID=" + sQryID + " and IsGraph=1");
            GetReportQry(sQryID, strQuery);
            if (sParam.Length > 0)
            {
                sParam = sParam.Substring(0, (sParam.Length - 1));
            }

            if (strQuery == "")
            {
                ultrchrt.Visible = false;
                UGrid_Rpt.Visible = true;
            }
            else
            {
                ultrchrt.Visible = true;
                UGrid_Rpt.Visible = false;
                ultrchrt.DataSource = DB.GetDT(strQuery + " " + sParam + "", false);
            }
        }

        private void ultrchrt_MouseClick(object sender, MouseEventArgs e)
        {
            this.ultrchrt.Focus();
        }

        private void ultrchrt_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs he = e as HandledMouseEventArgs;
            if (he == null) return;
            if (ModifierKeys == Keys.Shift)
            {
                if (ultrchrt.Axis.X.ScrollScale.Visible)
                {
                    double newScale = ultrchrt.Axis.X.ScrollScale.Scale;
                    if (e.Delta > 0)
                    {
                        newScale -= 0.1;
                        if (newScale > 1) newScale = 1;
                    }
                    else if (e.Delta < 0)
                    {
                        newScale += 0.1;
                        if (newScale < 0) newScale = 0;
                    }
                    ultrchrt.Axis.X.ScrollScale.Scale = newScale;
                }
                he.Handled = true;
            }
        }

        private void ultrchrt_Scaling(object sender, ChartScrollScaleEventArgs e)
        {
            foreach (ChartLayerAppearance layer in this.ultrchrt.CompositeChart.ChartLayers)
            {
                if (layer.Visible == true)
                {
                    switch (e.AxisNumber)
                    {
                        case AxisNumber.X_Axis:

                            layer.AxisX.ScrollScale.Scale = e.NewValue;
                            break;

                        case AxisNumber.Y_Axis:

                            layer.AxisY.ScrollScale.Scale = e.NewValue;
                            break;

                    }

                }

            }

        }

        private void ultrchrt_Scrolling(object sender, ChartScrollScaleEventArgs e)
        {
            foreach (ChartLayerAppearance layer in this.ultrchrt.CompositeChart.ChartLayers)
            {
                if (layer.Visible == true)
                {
                    switch (e.AxisNumber)
                    {
                        case AxisNumber.X_Axis:
                            layer.AxisX.ScrollScale.Scroll = e.NewValue;
                            break;
                        case AxisNumber.Y_Axis:
                            layer.AxisY.ScrollScale.Scroll = e.NewValue;
                            break;
                    }
                }
            }
        }



    }
}
