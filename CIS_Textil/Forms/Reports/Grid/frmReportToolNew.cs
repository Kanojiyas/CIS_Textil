using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_DBLayer;using CIS_Bussiness;
using CIS_Utilities;
using Infragistics.Documents.Excel;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Infragistics.Win.UltraWinGrid.DocumentExport;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using PopupControl;

namespace CIS_Textil
{
    public partial class frmReportToolNew : Form
    {
        ResourceManager m_resourceManger = null;
        [AccessedThroughProperty("fgDtls")]
        private static DataGridViewEx fgDtls;
        private bool chkExpandAll;
        private IContainer components;
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
        private string strPreviousLevelID;
        private string strNextLevelID;
        private ArrayList arrlist;
        private string strPreviousVal;
        private static bool isNexeID;
        private int iReportID;
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

        public frmReportToolNew()
        {
            ///* GET ORIGINAL SCREEN RESOLUTION */
            _originalSettings = DisplayManager.GetCurrentSettings();

            fgDtls = new CIS_DataGridViewEx.DataGridViewEx();
            arrlist = new ArrayList();
            iIDentity = 0;
            chkExpandAll = true;
            isMultyFilter = false;
            IsLoadGrd = false;
            firstRowIndex = -1;
            lastRowIndex = -1;
            totalColumnIndex = -1;
            r_FontUC = new r_UCFont();
            customColumnChooserDialog = null;
            InitializeComponent();
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
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
        private void frmReportToolNew_Load(object sender, EventArgs e)
        {
            try
            {
                isNexeID = false;
                strPreviousLevelID = "0";
                strPreviousVal = string.Empty;

                if (Localization.ParseNativeInt(DB.GetSnglValue(string.Format("SELECT Count(0) From tbl_MenuMaster Where MenuType='R' and FormType='R' and MenuID =" + (this.iIDentity)))) > 0)
                {
                    string strQry = string.Format("select MenuID,Menu_Caption,Form_Caption from {0} Where MenuID={1} order by Menu_Caption", "tbl_MenuMaster", (this.iIDentity));
                    Combobox_Setup.Fill_Combo(this.cboRptlst, strQry, "Menu_Caption", "MenuID");
                }
                else
                {
                    string squery = string.Format("select MenuID,Menu_Caption,Form_Caption from {0} Where FormType in('T','R') order by Menu_Caption", "tbl_MenuMaster");
                    Combobox_Setup.Fill_Combo(this.cboRptlst, squery, "Menu_Caption", "MenuID");
                }

                cboRptlst.ColumnWidths = "0;100";
                cboRptlst.AutoComplete = true;
                cboRptlst.AutoDropdown = true;
                
                this.dtFrom.Text = Localization.ToVBDateString(DB.GetSnglValue(string.Format("Select Yr_From From tbl_YearMaster Where YearID = {0}", Db_Detials.YearID)));
                this.dtTo.Text = Localization.ToVBDateString(Conversions.ToString(DateAndTime.Now.Date));
                cboRptlst.ColumnWidths = "0;240;0";
                cboLedger.ColumnWidths = "0;180";
                this.UGrid_Rpt.InitializeLayout += new InitializeLayoutEventHandler(CommonCls.grdSearch_InitializeLayout);

                pnlDockReportSettigns.Expand = false;

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

                DetailGrid_Setup.AddColto_Grid(ref  view, 0, "Sr. No.", "Sr. No.", 60, 10, 0, true, true, false, Enum_Define.DataType.pNumeric, DataGridViewContentAlignment.MiddleLeft, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 1, "Original Column", "Original Column", 150, 30, 0, false, false, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 2, "Original Column", "Original Column", 150, 30, 0, false, true, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 3, "Rename Column", "Rename Column", 150, 30, 0, false, true, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                DetailGrid_Setup.AddColto_GridCombo(ref   view, 70, 4, "", "", "Alignment", false, true, false, "", "", "", "", "C-Center, L-Left, R-Right", null, 0.0);
                fgDtls.ForeColor = Color.Black;

                MDIMain frmMDI = (MDIMain)Application.OpenForms["MDIMain"];
                int PnlDockLeftWidth = frmMDI.pnlDockLeft.Width;
                int PnlDockTopHeight = frmMDI.pnlDockTop.Height;
                int PnlDockBottomHeight = frmMDI.pnlDockBottom.Height;

                this.ClientSize = new System.Drawing.Size((Localization.ParseNativeInt(frmMDI.Width.ToString()) - (PnlDockLeftWidth  + 20)), (Localization.ParseNativeInt(frmMDI.Height.ToString()) - (PnlDockTopHeight + PnlDockBottomHeight + 80)));
                this.Location = new Point(0, 0);

                this.cboRptlst.SelectedIndex = 1;
                this.cboRptlst.Focus();
                //this.cboRptlst.SelectedValueChanged += new EventHandler(this.cboRptlst_SelectedValueChanged);

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
            if (cboLedger.SelectedValue != null && cboLedger.SelectedValue.ToString() != "0")
            {
                FillUgrid("0");
            }
            else
            {
                MessageBox.Show("Please Select Ledger Name...");
                return;
            }
        }

        private void FillUgrid(string strCheckValue)
        {
            string strSpName = DB.GetSnglValue("select FunctionName from tbl_ReportLevel where MenuID='" + cboRptlst.SelectedValue + "' and LevlId=" + cboLedger.SelectedValue);
            if (strSpName != "")
            {
                DataTable dt = DB.GetDT("exec " + strSpName + " " + Db_Detials.YearID + "," + strCheckValue + "," + cboLedger.SelectedValue  , false);
                UGrid_Rpt.DataSource = dt;
                foreach (UltraGridBand band in UGrid_Rpt.DisplayLayout.Bands)
                {
                    foreach (UltraGridColumn column in band.Columns)
                    {
                        column.CellActivation = Activation.NoEdit;
                    }
                }
                ultrchrt.DataSource = dt;
            }
        }

        //private void _viewrpt(bool IsPara = false)
        //{
        //    try
        //    {
        //        chkExpandAll = true;
        //        btnExpand.Text = "Expand";

        //        UGrid_Rpt.DataSource = null;
        //        UGrid_Rpt.Refresh();

        //        string[] strSelect = cboRptlst.SelectedValue.ToString().Split(';');
        //        string ReportName = cboRptlst.SelectedText;

        //        int sIsSP = Localization.ParseNativeInt(DB.GetSnglValue("select Count(0) from sysobjects Where xtype IN ('P','IF') And [Name] = '" + strSelect[(int)ReportLst.QueryName] + "'"));

        //        //Dim strIsSelectQry As String = DB.GetSnglValue(String.Format("sp_ExecQuery 'Select IsSelectQry From {0} Where ReportID = {1}'", tbl_ReportQuery, cboRptlst.ComboBox.SelectedValue))
        //        bool IsFilterClear = false;

        //        btnColumn.Enabled = true;
        //        btnExpand.Enabled = true;

        //        using (DataTable dt = DB.GetDT(string.Format(" sp_ExecQuery 'select * from {0} where Reportid = {1} Order By Col_Order Asc'", Db_Detials.tbl_ReportConfigDtls, (strSelect[(int)ReportLst.ReportID] == "-1" ? "0" : strSelect[(int)ReportLst.ReportID])), false))
        //        {
        //            if (dt.Rows.Count > 0)
        //            {
        //                SqlDataReader iDr = DB.GetRS("Select * From " + Db_Detials.tbl_ReportConfigMain + " Where UserID="+Db_Detials.UserID+" and CompanyID="+Db_Detials.CompID+" and Reportid = " + strSelect[(int)ReportLst.ReportID]);
        //                iDr.Read();

        //                if (!(Localization.ParseNativeInt(strSelect[(int)ReportLst.ReportID].ToString()) == -1))
        //                    this.UGrid_Rpt.Font = new Font(iDr["Font_Name"].ToString(), Convert.ToSingle(Localization.ParseNativeDecimal(iDr["Font_Size"].ToString())), FontStyle.Regular, GraphicsUnit.Point, 0);

        //                string strQry_ColName = string.Empty;
        //                {
        //                    fgDtls.Rows.Clear();
        //                    fgDtls.Rows.Add(dt.Rows.Count);

        //                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
        //                    {
        //                        strQry_ColName += dt.Rows[i]["Qry_ColName"].ToString() + " As [" + dt.Rows[i]["Rpt_ColName"].ToString() + "], ";
        //                        fgDtls.Rows[i].Cells[(int)Col.Col_ID].Value = (i + 1);
        //                        fgDtls.Rows[i].Cells[(int)Col.Data_Col].Value = dt.Rows[i]["Qry_ColName"].ToString();
        //                        fgDtls.Rows[i].Cells[(int)Col.Data_rmd].Value = dt.Rows[i]["Rpt_ColName"].ToString();
        //                        fgDtls.Rows[i].Cells[(int)Col.Data_rmdnew].Value = dt.Rows[i]["Rpt_ColName"].ToString();
        //                    }
        //                }
        //                if (strQry_ColName.Length != 0)
        //                    strQry_ColName = Localization.Left(strQry_ColName, 2);

        //                // Hide the grid caption by setting the Text to empty string.
        //                UGrid_Rpt.Text = "";
        //                string strQuery = string.Empty;
        //                if (sIsSP == 0)
        //                {
        //                    UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery 'Select {1} From {0}'", strSelect[(int)ReportLst.QueryName], strQry_ColName), false);
        //                }
        //                else
        //                {
        //                    switch (strSelect[(int)ReportLst.QueryName])
        //                    {
        //                        case "Sp_FetchYarnWiseStock":
        //                        case "Sp_FetchShadeWiseStock":
        //                        case "Sp_FetchColorWiseStock":
        //                        case "sp_FetchFabricQualityWiseStk":
        //                        case "sp_FetchFabricDesignWiseStk1":
        //                        case "sp_FetchFabricShadeWiseStk":
        //                        case "Sp_InHouseYarnStock":
        //                        case "Sp_DyerYarnStock":
        //                        case "Sp_WarpingYarnStock":
        //                        case "Sp_YarnWeaverStock":
        //                        case "Sp_ProcesserYarnStock":
        //                        case "sp_FetchProcYrnPOStk":
        //                        case "Sp_InHouseYarnStockBoxWise":
        //                        case "sp_FetchFabricDesignWiseStk1_sum":
        //                        case "Sp_WeaverYarnStock":
        //                        case "Sp_WeaverDesignWiseBeamStk":
        //                        case "Sp_StockFabricLedger":
        //                        case "sp_POWiseFinishFabircStock":
        //                        case "sp_WeaverBeamStock_SWMPL":
        //                            if (Information.IsDate(dtFrom.Text) & Information.IsDate(dtTo.Text))
        //                            {
        //                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[(int)ReportLst.QueryName] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(dtFrom.Text), Localization.ToSqlDateString(dtTo.Text), (cboLedger.SelectedValue == null ? 0 : cboLedger.SelectedValue))), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                            }
        //                            break;
        //                        case "sp_FetchFabricDesignWiseStk":
        //                        case "sp_POWiseStockRpt":
        //                        case "sp_POWiseStockRpt1":
        //                            if (Information.IsDate(dtFrom.Text) & Information.IsDate(dtTo.Text))
        //                            {
        //                                if ((cboLedger.SelectedValue != null))
        //                                {
        //                                    UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[(int)ReportLst.QueryName] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(dtFrom.Text), Localization.ToSqlDateString(dtTo.Text), cboLedger.SelectedValue)), false);
        //                                }
        //                                else
        //                                {
        //                                    Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please Select Location");
        //                                }
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                            }

        //                            break;
        //                        case "sp_FetchOverAllYarnStock":
        //                        case "Sp_WeaverYarnStockSummary":
        //                        case "sp_FetchOverAllBeamStock":
        //                        case "sp_FetchOverAllFabStock":
        //                        case "Sp_FetchDeptYarnWiseStock":
        //                            if (Information.IsDate(dtFrom.Text) & Information.IsDate(dtTo.Text))
        //                            {
        //                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" {3},''{0}'', {1}, {2}", Localization.ToSqlDateString(dtTo.Text), Db_Detials.CompID, Db_Detials.YearID, (cboLedger.SelectedValue == null ? 0 : cboLedger.SelectedValue))), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                            }
        //                            break;
        //                        case "sp_WeaverLoomwiseStock":
        //                        case "sp_DeptwiseBeamCutBalance":
        //                            if (Information.IsDate(dtTo.Text))
        //                            {
        //                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format("''{0}'', {1}, {2}, {3}", Localization.ToSqlDateString(dtTo.Text), Db_Detials.CompID, Db_Detials.YearID, (cboLedger.SelectedValue == null ? 0 : cboLedger.SelectedValue))), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please Enter To Date");
        //                            }

        //                            break;
        //                        case "sp_LedgerRptDayWise_Print":
        //                        case "sp_LedgerRptEntyWise_Print":
        //                            if (Information.IsDate(dtFrom.Text) & Information.IsDate(dtTo.Text))
        //                            {
        //                                if ((cboLedger.SelectedValue != null))
        //                                {
        //                                    UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" {4}, {0}, {1}, ''{2}'', ''{3}''", Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(dtFrom.Text), Localization.ToSqlDateString(dtTo.Text), cboLedger.SelectedValue)), false);
        //                                }
        //                                else
        //                                {
        //                                    Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please Select Ledger");
        //                                }
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                            }

        //                            break;
        //                        case "sp_FetchFabricDyingLotStkDtls":
        //                            UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" ''{0}'', {1}, {2}, {3}", Localization.ToSqlDateString(dtTo.Text), (cboLedger.SelectedValue == null ? 0 : cboLedger.SelectedValue), Db_Detials.CompID, Db_Detials.YearID)), false);

        //                            break;
        //                        case "sp_LedgerRptMonthWise_Print":
        //                            if ((cboLedger.SelectedValue != null))
        //                            {
        //                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" {0}, {1}, {2}", cboLedger.SelectedValue, Db_Detials.CompID, Db_Detials.YearID)), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please Select Ledger");
        //                            }
        //                            break;

        //                        case "sp_OutStandingCustomer_Billwise":
        //                        case "sp_OutStandingCustomer_Summary":
        //                        case "sp_OutStandingSupplier_Billwise":
        //                        case "sp_OutStandingSupplier_Summary":
        //                        case "Sp_FetchMillwiseYarnStk":
        //                        case "sp_FetchWarpinYarnStk":
        //                        case "sp_FetchProcessYarnStk":
        //                        case "Sp_FetchWeaverYarnStk":
        //                        case "sp_FabricPendingYarnPurchOrderReport":
        //                        case "Sp_FetchYarnProdStock":
        //                        case "sp_FabricTypeWiseStock":
        //                        case "sp_FetchFabricUnitWiseStock":
        //                        case "sp_FetchGodownFabricStk":
        //                        case "sp_FetchProcessFabricStk":
        //                        case "sp_FetchFabricPieceWiseStock":
        //                        case "sp_FabricPendingPuchaseOrderReport":
        //                        case "sp_FabricPendingSalesOrderReport":
        //                            //case "Sp_FetchDeptYarnWiseStock":

        //                            if (Information.IsDate(dtFrom.Text) & Information.IsDate(dtTo.Text))
        //                            {
        //                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(dtFrom.Text), Localization.ToSqlDateString(dtTo.Text), (cboLedger.SelectedValue == null ? 0 : cboLedger.SelectedValue))), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                            }
        //                            break;

        //                        case "sp_ItemWisePurchase":
        //                        case "sp_ItemWiseSales":
        //                        case "sp_PartyWisePurchase":
        //                        case "sp_PartyWiseSales":
        //                        case "sp_DayBook":
        //                            if (Information.IsDate(dtFrom.Text) & Information.IsDate(dtTo.Text))
        //                            {
        //                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(dtFrom.Text), Localization.ToSqlDateString(dtTo.Text), (cboLedger.SelectedValue == null ? 0 : cboLedger.SelectedValue))), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                            }
        //                            break;
        //                        case "sp_WeaverWiseWeftIsuueReport":

        //                            if (Information.IsDate(dtFrom.Text) & Information.IsDate(dtTo.Text))
        //                            {
        //                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[(int)ReportLst.QueryName] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(dtFrom.Text), Localization.ToSqlDateString(dtTo.Text), (cboLedger.SelectedValue == null ? 0 : cboLedger.SelectedValue))), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                            }

        //                            break;
        //                        case "sp_YarnShadeMAster":
        //                        case "sp_YarnColorMAster":
        //                        case "sp_PODesignrpt":
        //                            UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" {0}, {1}", Db_Detials.CompID, Db_Detials.YearID)), false);

        //                            break;
        //                        case "sp_PoReport":
        //                        case "sp_PoSummeryReport":
        //                        case "sp_PoReport_New":
        //                        case "sp_PoSummeryReport_New":
        //                        case "sp_FabOrderComlCompletionRpt":
        //                            if ((cboLedger.SelectedValue != null))
        //                            {
        //                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" ''{0}'', {1}, {2}", cboLedger.SelectedValue, Db_Detials.CompID, Db_Detials.YearID)), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please select PO");
        //                            }

        //                            break;
        //                        case "sp_WeftCompletionRpt":
        //                            UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" ''{0}'', {1}, {2}", (cboLedger.SelectedValue == null ? "" : cboLedger.SelectedValue), Db_Detials.CompID, Db_Detials.YearID)), false);

        //                            break;
        //                        case "sp_DsgnWiseWeftConsuption":

        //                            UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" ''{0}'', {1}, {2}", cboLedger.SelectedValue, Db_Detials.CompID, Db_Detials.YearID)), false);

        //                            break;
        //                        case "sp_LotWiseYarnReport":
        //                        case "sp_FabPendingSalesOrderRpt":
        //                        case "Sp_PendingSubSalesOrderRpt":
        //                        case "sp_OrderStatus":

        //                            UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" {0}, {1}", Db_Detials.CompID, Db_Detials.YearID)), false);
        //                            break;
        //                        //Below Case is only For Task DailyWorkInward
        //                        case "sp_DailyWorkInwarRpt":
        //                        //Below case is only for Task Employee Login Status Report
        //                        case "sp_UserLoginStatusReport":
        //                        case "sp_DailyWrkInwrdClientWise":
        //                            if (Information.IsDate(dtFrom.Text) & Information.IsDate(dtTo.Text))
        //                            {
        //                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(dtFrom.Text), Localization.ToSqlDateString(dtTo.Text), (cboLedger.SelectedValue == null ? 0 : cboLedger.SelectedValue))), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                            }
        //                            break;



        //                        default:
        //                            UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[(int)ReportLst.QueryName] + string.Format("''{2}'', ''{3}'', {0}, {1}", Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(dtFrom.Text), Localization.ToSqlDateString(dtTo.Text))), false);
        //                            break;
        //                    }
        //                }

        //                dt.Select("", "Group_lvl");
        //                for (int i = 0; i <= (dt.Rows.Count - 1); i++)
        //                {
        //                    if (Localization.ParseBoolean(dt.Rows[i]["IsGroup"].ToString()))
        //                    {
        //                        UGrid_Rpt.DisplayLayout.Bands[0].SortedColumns.Add(dt.Rows[i]["Rpt_ColName"].ToString(), false, true);
        //                    }
        //                }
        //                dt.Select("", "");

        //                foreach (UltraGridBand band in UGrid_Rpt.DisplayLayout.Bands)
        //                {
        //                    foreach (UltraGridColumn column in band.Columns)
        //                    {
        //                        column.Width = Localization.ParseNativeInt(dt.Rows[column.Index]["Col_Width"].ToString());
        //                        column.Hidden = Localization.ParseBoolean(dt.Rows[column.Index]["IsShow"].ToString());
        //                        column.CellActivation = Activation.NoEdit;
        //                        column.Tag = dt.Rows[column.Index]["Qry_ColName"].ToString();
        //                        SummarySettings oSummary = null;
        //                        if (Localization.ParseBoolean(dt.Rows[column.Index]["IsAvg"].ToString()))
        //                        {
        //                            oSummary = column.Band.Summaries.Add(SummaryType.Average, column, SummaryPosition.UseSummaryPositionColumn);
        //                            oSummary.DisplayFormat = "{0}";
        //                            oSummary.Appearance.TextHAlign = HAlign.Right;
        //                        }
        //                        if (Localization.ParseBoolean(dt.Rows[column.Index]["IsSum"].ToString()))
        //                        {
        //                            oSummary = column.Band.Summaries.Add(SummaryType.Sum, column, SummaryPosition.UseSummaryPositionColumn);
        //                            oSummary.DisplayFormat = "{0}";
        //                            oSummary.Appearance.TextHAlign = HAlign.Right;
        //                        }
        //                        if (Localization.ParseBoolean(dt.Rows[column.Index]["IsMin"].ToString()))
        //                        {
        //                            oSummary = column.Band.Summaries.Add(SummaryType.Minimum, column, SummaryPosition.UseSummaryPositionColumn);
        //                        }
        //                        if (Localization.ParseBoolean(dt.Rows[column.Index]["IsMax"].ToString()))
        //                        {
        //                            oSummary = column.Band.Summaries.Add(SummaryType.Maximum, column, SummaryPosition.UseSummaryPositionColumn);
        //                        }
        //                        if (Localization.ParseBoolean(dt.Rows[column.Index]["IsCount"].ToString()))
        //                        {
        //                            oSummary = column.Band.Summaries.Add(SummaryType.Count, column, SummaryPosition.UseSummaryPositionColumn);
        //                        }

        //                        // Applying filter conditions
        //                        //If the row filter mode is band based, then get the column filters off the band
        //                        ColumnFiltersCollection columnFilters = UGrid_Rpt.DisplayLayout.Bands[0].ColumnFilters;
        //                        if (IsFilterClear == false)
        //                        {
        //                            columnFilters.ClearAllFilters();
        //                            IsFilterClear = true;
        //                        }
        //                        {
        //                            if (!string.IsNullOrEmpty(dt.Rows[column.Index]["Filter_Text"].ToString()) & !string.IsNullOrEmpty(dt.Rows[column.Index]["Filter_Type"].ToString()))
        //                            {
        //                                columnFilters[column.Index].FilterConditions.Add((FilterComparisionOperator)Localization.ParseNativeInt(dt.Rows[column.Index]["Filter_Type"].ToString()), dt.Rows[column.Index]["Filter_Text"].ToString());
        //                            }
        //                        }

        //                    }
        //                }
        //            }
        //            else
        //            {
        //                UGrid_Rpt.Text = "";
        //                string strQuery = string.Empty;
        //                if (sIsSP == 0)
        //                {
        //                    UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery 'Select * From {0}'", DB.GetSnglValue(string.Format("sp_ExecQuery 'Select QueryName From {0} Where QueryID = {1}'", Db_Detials.tbl_ReportQuery, strSelect[(int)ReportLst.QueryID]))), false);
        //                }
        //                else
        //                {
        //                    switch (strSelect[(int)ReportLst.QueryName])
        //                    {
        //                        case "sp_FetchOverAllYarnStock":
        //                        case "Sp_WeaverYarnStockSummary":
        //                        case "sp_FetchOverAllBeamStock":
        //                        case "sp_FetchOverAllFabStock":
        //                            if (Information.IsDate(dtFrom.Text) & Information.IsDate(dtTo.Text))
        //                            {
        //                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" {3},''{0}'', {1}, {2}", Localization.ToSqlDateString(dtTo.Text), Db_Detials.CompID, Db_Detials.YearID, (cboLedger.SelectedValue == null ? 0 : Localization.ParseNativeDouble(cboLedger.SelectedValue.ToString())))), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                            }

        //                            break;
        //                        case "Sp_FetchYarnWiseStock":
        //                        case "Sp_FetchShadeWiseStock":
        //                        case "Sp_FetchColorWiseStock":
        //                        case "sp_FetchFabricQualityWiseStk":
        //                        case "sp_FetchFabricShadeWiseStk":
        //                        case "Sp_FetchMillwiseYarnStk":
        //                        case "sp_FetchWarpinYarnStk":
        //                        case "sp_FetchProcessYarnStk":
        //                        case "Sp_FetchWeaverYarnStk":
        //                        case "sp_FabricPendingYarnPurchOrderReport":
        //                        case "Sp_FetchYarnProdStock":
        //                        case "sp_FabricTypeWiseStock":
        //                        case "sp_FetchFabricUnitWiseStock":
        //                        case "sp_FetchGodownFabricStk":
        //                        case "sp_FetchProcessFabricStk":
        //                        case "sp_FetchFabricPieceWiseStock":
        //                        case "sp_FabricPendingPuchaseOrderReport":
        //                        case "sp_FabricPendingSalesOrderReport":
        //                        case "sp_SupplierIntCalc":
        //                        case "sp_CustomerIntCalc":
        //                        case "Sp_InHouseYarnStock":
        //                        case "Sp_DyerYarnStock":
        //                        case "Sp_WarpingYarnStock":
        //                        case "Sp_YarnWeaverStock":
        //                        case "Sp_ProcesserYarnStock":
        //                        case "Sp_FetchDeptYarnWiseStock":
        //                        case "sp_FetchProcYrnPOStk":
        //                        case "Sp_InHouseYarnStockBoxWise":
        //                        case "sp_FetchFabricDesignWiseStk1":
        //                        case "sp_FetchFabricDesignWiseStk1_sum":
        //                        case "Sp_WeaverYarnStock":
        //                        case "Sp_WeaverDesignWiseBeamStk":
        //                        case "Sp_StockFabricLedger":
        //                        case "sp_POWiseFinishFabircStock":
        //                        case "sp_WeaverBeamStock_SWMPL":
        //                            if (Information.IsDate(dtFrom.Text) & Information.IsDate(dtTo.Text))
        //                            {
        //                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(dtFrom.Text), Localization.ToSqlDateString(dtTo.Text), (cboLedger.SelectedValue == null ? 0 : Localization.ParseNativeDouble(cboLedger.SelectedValue.ToString())))), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                            }

        //                            break;
        //                        case "sp_FetchFabricDesignWiseStk":
        //                        case "sp_POWiseStockRpt":
        //                        case "sp_POWiseStockRpt1":
        //                            if (Information.IsDate(dtFrom.Text) & Information.IsDate(dtTo.Text))
        //                            {
        //                                if ((cboLedger.SelectedValue != null))
        //                                {
        //                                    UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[(int)ReportLst.QueryName] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(dtFrom.Text), Localization.ToSqlDateString(dtTo.Text), cboLedger.SelectedValue)), false);
        //                                }
        //                                else
        //                                {
        //                                    Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please Select Location");
        //                                }
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                            }

        //                            break;
        //                        case "sp_WeaverLoomwiseStock":
        //                        case "sp_DeptwiseBeamCutBalance":
        //                            if (Information.IsDate(dtTo.Text))
        //                            {
        //                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format("''{0}'', {1}, {2}, {3}", Localization.ToSqlDateString(dtTo.Text), Db_Detials.CompID, Db_Detials.YearID, (cboLedger.SelectedValue == null ? 0 : Localization.ParseNativeDouble(cboLedger.SelectedValue.ToString())))), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please Enter To Date");
        //                            }

        //                            break;
        //                        case "sp_LedgerRptDayWise_Print":
        //                        case "sp_LedgerRptEntyWise_Print":
        //                            if (Information.IsDate(dtFrom.Text) & Information.IsDate(dtTo.Text))
        //                            {
        //                                if ((cboLedger.SelectedValue != null))
        //                                {
        //                                    UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" {4}, {0}, {1}, ''{2}'', ''{3}''", Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(dtFrom.Text), Localization.ToSqlDateString(dtTo.Text), cboLedger.SelectedValue)), false);
        //                                }
        //                                else
        //                                {
        //                                    Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please Select Ledger");
        //                                }
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                            }

        //                            break;
        //                        case "sp_FetchFabricDyingLotStkDtls":
        //                            UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" ''{0}'', {1}, {2}, {3}", Localization.ToSqlDateString(dtTo.Text), (cboLedger.SelectedValue == null ? 0 : cboLedger.SelectedValue), Db_Detials.CompID, Db_Detials.YearID)), false);

        //                            break;
        //                        case "sp_LedgerRptMonthWise_Print":
        //                            if ((cboLedger.SelectedValue != null))
        //                            {
        //                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" {0}, {1}, {2}", cboLedger.SelectedValue, Db_Detials.CompID, Db_Detials.YearID)), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please Select Ledger");
        //                            }

        //                            break;
        //                        case "sp_OutStandingCustomer_Billwise":
        //                        case "sp_OutStandingCustomer_Summary":
        //                        case "sp_OutStandingSupplier_Billwise":
        //                        case "sp_OutStandingSupplier_Summary":
        //                            if (Information.IsDate(dtFrom.Text) & Information.IsDate(dtTo.Text))
        //                            {
        //                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(dtFrom.Text), Localization.ToSqlDateString(dtTo.Text), (cboLedger.SelectedValue == null ? 0 : cboLedger.SelectedValue))), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                            }
        //                            break;
        //                        case "sp_ItemWisePurchase":
        //                        case "sp_ItemWiseSales":
        //                        case "sp_PartyWisePurchase":
        //                        case "sp_PartyWiseSales":
        //                        case "sp_DayBook":
        //                            if (Information.IsDate(dtFrom.Text) & Information.IsDate(dtTo.Text))
        //                            {
        //                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format("''{2}'', ''{3}'', {0}, {1}", Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(dtFrom.Text), Localization.ToSqlDateString(dtTo.Text))), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                            }
        //                            break;
        //                        case "sp_WeaverWiseWeftIsuueReport":
        //                            if (Information.IsDate(dtFrom.Text) & Information.IsDate(dtTo.Text))
        //                            {
        //                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[(int)ReportLst.QueryName] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(dtFrom.Text), Localization.ToSqlDateString(dtTo.Text), (cboLedger.SelectedValue == null ? 0 : cboLedger.SelectedValue))), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                            }

        //                            break;
        //                        case "sp_YarnShadeMAster":
        //                        case "sp_YarnColorMAster":
        //                        case "sp_PODesignrpt":
        //                            UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" {0}, {1}", Db_Detials.CompID, Db_Detials.YearID)), false);

        //                            break;
        //                        case "sp_PoReport":
        //                        case "sp_PoSummeryReport":
        //                        case "sp_PoReport_New":
        //                        case "sp_PoSummeryReport_New":
        //                        case "sp_FabOrderComlCompletionRpt":
        //                            if ((cboLedger.SelectedValue != null))
        //                            {
        //                                UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" ''{0}'', {1}, {2}", cboLedger.SelectedValue, Db_Detials.CompID, Db_Detials.YearID)), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please select PO");
        //                            }

        //                            break;
        //                        case "sp_WeftCompletionRpt":
        //                            UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" ''{0}'', {1}, {2}", (cboLedger.SelectedValue == null ? "" : cboLedger.SelectedValue), Db_Detials.CompID, Db_Detials.YearID)), false);

        //                            break;
        //                        case "sp_DsgnWiseWeftConsuption":
        //                            UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" ''{0}'', {1}, {2}", cboLedger.SelectedValue, Db_Detials.CompID, Db_Detials.YearID)), false);

        //                            break;
        //                        case "sp_LotWiseYarnReport":
        //                        case "sp_FabPendingSalesOrderRpt":
        //                        case "Sp_PendingSubSalesOrderRpt":
        //                        case "sp_OrderStatus":
        //                            UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format(" {0}, {1}", Db_Detials.CompID, Db_Detials.YearID)), false);
        //                            break;

        //                        //case "sp_BookCreation":
        //                        //    if (Information.IsDate(dtFrom.Text) & Information.IsDate(dtTo.Text))
        //                        //    {
        //                        //        UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[(int)ReportLst.QueryName] + string.Format("''{2}'', ''{3}'', {0}, {1}", Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(dtFrom.Text), Localization.ToSqlDateString(dtTo.Text))), false);
        //                        //    }
        //                        //    else
        //                        //    {
        //                        //        Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                        //    }

        //                        //    break;

        //                        //case "sp_BookOpening":
        //                        //case "sp_FetchBookStockRegister":
        //                        //case "sp_BookCreation":
        //                        //    if (Information.IsDate(dtFrom.Text) & Information.IsDate(dtTo.Text))
        //                        //    {
        //                        //        UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[(int)ReportLst.QueryName] + string.Format("''{2}'', ''{3}'', {0}, {1}", Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(dtFrom.Text), Localization.ToSqlDateString(dtTo.Text))), false);
        //                        //    }
        //                        //    else
        //                        //    {
        //                        //        Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                        //    }

        //                        //    break;

        //                        case "sp_BookIssuePartyWise":
        //                        case "sp_BookIssueDesignWise":
        //                            if (Information.IsDate(this.dtFrom.Text) & Information.IsDate(this.dtTo.Text))
        //                            {
        //                                this.UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}'  ", strSelect[2] + string.Format(" {4},''{2}'', ''{3}'', {0}, {1}", new object[] { Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(this.dtFrom.Text), Localization.ToSqlDateString(this.dtTo.Text), RuntimeHelpers.GetObjectValue(Interaction.IIf(this.cboLedger.SelectedValue == null, 0, RuntimeHelpers.GetObjectValue(this.cboLedger.SelectedValue))) })), false);
        //                            }
        //                            else
        //                            {
        //                                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Please enter From Date and To Date");
        //                            }
        //                            break;

        //                        default:
        //                            UGrid_Rpt.DataSource = DB.GetDT(string.Format(" sp_ExecQuery '{0}' ", strSelect[(int)ReportLst.QueryName] + string.Format("''{2}'', ''{3}'', {0}, {1}", Db_Detials.CompID, Db_Detials.YearID, Localization.ToSqlDateString(dtFrom.Text), Localization.ToSqlDateString(dtTo.Text))), false);
        //                            break;
        //                        //UGrid_Rpt.DataSource = DB.GetDT(String.Format(" sp_ExecQuery '{0}' ",strSelect[(int)ReportLst.QueryName] & String.Format(" {0}, {1}", Db_Detials.CompID, Db_Detials.YearID)), False)
        //                    }
        //                }
        //            }

        //            foreach (UltraGridBand band in UGrid_Rpt.DisplayLayout.Bands)
        //            {
        //                fgDtls.Rows.Clear();
        //                foreach (UltraGridColumn column in band.Columns)
        //                {
        //                    fgDtls.Rows.Add();
        //                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[(int)Col.Col_ID].Value = (column.Index + 1);
        //                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[(int)Col.Data_Col].Value = column.Header.Caption.ToString();
        //                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[(int)Col.Data_rmd].Value = column.Header.Caption.ToString();
        //                    fgDtls.Rows[fgDtls.RowCount - 1].Cells[(int)Col.Data_rmdnew].Value = column.Header.Caption.ToString();
        //                    column.CellActivation = Activation.NoEdit;
        //                }
        //                IsLoadGrd = true;
        //            }
        //        }

        //        // AutoSize all of the columns
        //        foreach (UltraGridColumn column in UGrid_Rpt.DisplayLayout.Bands[0].Columns)
        //        {
        //            //column.PerformAutoResize(PerformAutoSizeType.AllRowsInBand)
        //            column.Header.Appearance.BackColor = Color.White;
        //            column.Header.Appearance.BackColor2 = Color.White;
        //            column.Header.Appearance.FontData.Bold = DefaultableBoolean.True;
        //            column.Header.Appearance.BorderAlpha = Alpha.Transparent;

        //            using (SqlDataReader iDr = DB.GetRS("Select * From " + Db_Detials.tbl_ReportConfigDtls + " Where Reportid = " + strSelect[(int)ReportLst.ReportID] + " And Col_Order = " + column.Index))
        //            {
        //                if ((iDr.Read()))
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
        //                    if (string.IsNullOrEmpty(iDr["AlignType"].ToString()))
        //                    {
        //                        fgDtls.Rows[column.Index].Cells[(int)Col.Align].Value = "L";
        //                    }
        //                    else
        //                    {
        //                        fgDtls.Rows[column.Index].Cells[(int)Col.Align].Value = iDr["AlignType"].ToString();
        //                    }
        //                    column.Header.Caption = iDr["Rpt_ColName"].ToString();
        //                    column.Width = Localization.ParseNativeInt(iDr["Col_Width"].ToString());
        //                    fgDtls.Rows[column.Index].Cells[(int)Col.Data_rmdnew].Value = iDr["Rpt_ColName"].ToString();
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

        private void UGrid_Rpt_InitializePrint(object sender, CancelablePrintEventArgs e)
        {
            this.SetupPrint(e);
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
                    strQry = Environment.NewLine + txtReportName.Text == "" ? this.cboRptlst.Text.ToUpper() : txtReportName.Text.ToUpper() + Environment.NewLine;
                    strQry += Environment.NewLine + "From Date : " + Localization.ToVBDateString(this.dtFrom.Text) + " To " + Localization.ToVBDateString(this.dtTo.Text);
                    e.DefaultLogicalPageLayoutInfo.PageHeader = strQry;
                    e.DefaultLogicalPageLayoutInfo.PageFooterBorderStyle = UIElementBorderStyle.Solid;
                    e.DefaultLogicalPageLayoutInfo.PageHeaderBorderStyle = UIElementBorderStyle.Solid;

                }
            }

            e.DefaultLogicalPageLayoutInfo.PageHeaderHeight = 60;
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

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            r_NewRpt r_NewUC = new r_NewRpt();
            Popup pp_NewRpt = new Popup(r_NewUC)
            {
                Resizable = true
            };
            r_NewRpt.sReportName = "";
            r_NewRpt.IsNewRpt = false;
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

        #region For New Report Events
        private void btnSave_Click(object sender, EventArgs e)
        {
            //this.SaveRptSettings(false);
        }

        //private void SaveRptSettings(bool IsNewRpt = false)
        //{
        //    try
        //    {
        //        string strQryDtls = " Insert Into tbl_ReportConfigDtls (ReportID, Qry_ColName, Rpt_ColName, Col_Order, Col_Width, IsShow, IsGroup, Group_lvl, IsAvg, IsSum, IsMax, IsMin, IsCount, Filter_Type, Filter_Text, AlignType) Values((#CodeID#), {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14});" + Environment.NewLine;
        //        string strInsertMain = string.Empty;
        //        string[] strSelect = this.cboRptlst.SelectedValue.ToString().Split(';');
        //        if (strSelect[0] == "-1")
        //        {
        //            strInsertMain = string.Format(" Insert Into {0} (QueryID, ReportName, Font_Name, Font_Size) Values({1}, {2}, {3}, {4});" + Environment.NewLine, new object[] { "tbl_ReportConfigMain", strSelect[1], DB.SQuote(this.cboRptlst.Text), DB.SQuote(this.UGrid_Rpt.Font.FontFamily.Name), this.UGrid_Rpt.Font.Size });
        //            IsNewRpt = true;
        //        }
        //        else
        //        {
        //            strInsertMain = string.Format(" Update {0} Set QueryID = {1}, ReportName = {2}, Font_Name = {3}, Font_Size = {4} Where ReportID = {5};" + Environment.NewLine, new object[] { "tbl_ReportConfigMain", strSelect[1], DB.SQuote(this.cboRptlst.Text), DB.SQuote(this.UGrid_Rpt.Font.FontFamily.Name), this.UGrid_Rpt.Font.Size, strSelect[0] });
        //            IsNewRpt = false;
        //        }

        //        string strInsertQry = string.Empty;
        //        int i = 0;

        //        foreach (UltraGridBand band in UGrid_Rpt.DisplayLayout.Bands)
        //        {
        //            foreach (UltraGridColumn column in band.Columns)
        //            {
        //                string[] strSumry = new string[4];
        //                ColumnFiltersCollection columnFilters = UGrid_Rpt.DisplayLayout.Bands[0].ColumnFilters;

        //                foreach (SummarySettings oSummary in column.Band.Summaries)
        //                {
        //                    if (oSummary.SourceColumn == column)
        //                    {
        //                        strSumry[Localization.ParseNativeInt(oSummary.SummaryType.ToString())] = "'True'";
        //                    }
        //                }

        //                for (int iSumry = 0; i < 4; i++)
        //                {
        //                    if (strSumry[iSumry] == null)
        //                    {
        //                        strSumry[iSumry] = "'False'";
        //                    }
        //                }

        //                string strCmprValue = string.Empty;
        //                string strCmprOptr = string.Empty;
        //                try
        //                {
        //                    strCmprValue = DB.SQuote(columnFilters[column.Index].FilterConditions[0].CompareValue.ToString());
        //                    strCmprOptr = Localization.ParseNativeInt(columnFilters[column.Index].FilterConditions[0].ComparisionOperator.ToString()).ToString();

        //                }
        //                catch (Exception ex)
        //                {
        //                    strCmprValue = "NULL";
        //                    strCmprOptr = "NULL";
        //                }

        //                strInsertQry += string.Format(strQryDtls, DB.SQuote(Conversions.ToString(fgDtls.Rows[i].Cells[1].Value)),
        //                            DB.SQuote(column.Header.Caption), column.Index, column.Width,
        //                            DB.SQuote(Conversions.ToString(column.Hidden)),
        //                            DB.SQuote(Conversions.ToString(column.IsGroupByColumn)),
        //                            Interaction.IIf(column.IsGroupByColumn, column.Level, -1), strSumry[0], strSumry[1],
        //                            strSumry[2], strSumry[3], strSumry[4], strCmprOptr, strCmprValue,
        //                            DB.SQuote(Conversions.ToString(fgDtls.Rows[i].Cells[4].Value)));
        //                i++;


        //            }
        //        }

        //        if (strInsertQry.Length != 0)
        //        {
        //            strInsertQry = string.Format("Delete From {0} Where ReportID = (#CodeID#); ", "tbl_ReportConfigDtls") + strInsertQry;
        //            DB.ExecuteTranscation(strInsertMain, Conversions.ToString(Localization.ParseNativeDouble(strSelect[0])), IsNewRpt, strInsertQry);
        //        }

        //        string StrQuery = string.Format(" sp_ShowReportList '" + Conversions.ToString(iIDentity) + "' ");
        //        string RptNm = this.cboRptlst.Text;

        //        this.cboRptlst.SelectedValueChanged -= new EventHandler(this.cboRptlst_SelectedValueChanged);
        //        Combobox_Setup.Fill_Combo(this.cboRptlst, StrQuery, "ReportName", "ReportID");
        //        CIS_MultiColumnComboBox.CIS_MultiColumnComboBox colFiltr = this.cboRptlst;
        //        colFiltr.ColumnWidths = "0;250";
        //        colFiltr.AutoComplete = true;
        //        colFiltr.AutoDropdown = true;
        //        colFiltr = null;
        //        this.cboRptlst.SelectedValueChanged += new EventHandler(this.cboRptlst_SelectedValueChanged);
        //        this.cboRptlst.Text = RptNm;
        //        Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Report configuration saved successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        Navigate.ShowMessage(CIS_DialogIcon.Information, "", ex.Message);
        //    }
        //}
        #endregion

        #region For Report Print Events
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                object instance = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                int iactionType = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster() Where MiscName='IsReportView'"));
                try
                {
                    DB.ExecuteSQL("INSERT INTO tbl_UserReportLog(MenuID, ReportID, IsCrystalReport, IsBarCode, IsChequePrint, ActionType, UserID, UserDt,StoreID,CompID,BranchID, YearID, IPAddress, MacAddress) VALUES(" + Localization.ParseNativeInt(Conversions.ToString(NewLateBinding.LateGet(instance, null, "iIDentity", new object[0], null, null, null))) + ", " + iReportID + ", 0, 0, 0, " + iactionType + "," + Db_Detials.UserID + ",getdate()" + ","+Db_Detials.StoreID+"," + Db_Detials.CompID + ","+Db_Detials.BranchID+"," + Db_Detials.YearID + "," + DB.SQuote(CommonCls.GetIP()) + "," + DB.SQuote(CommonCls.FetchMacId()) + ")");
                }
                catch { }
                //DB.ExecuteSQL("INSERT INTO tbl_UserReportLog(ReportID,IsView,IsPrint,IsPreview,IsEmailSent,EmailSentIDs,EmailSentFromID,UserID,UserDt) VALUES(" + iReportID + ",0,1,0,0, NULL, NULL, " + Db_Detials.UserID + ",getdate())");
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
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception ex = exception1;
                ProjectData.ClearProjectError();
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


        private void btnFilterClose_Click(object sender, EventArgs e)
        {
        }

        private void btnExpandl_Click(object sender, EventArgs e)
        {
            if (this.chkExpandAll)
            {
                this.UGrid_Rpt.Rows.ExpandAll(true);
                this.btnExpand.Text = "Collapse";
                this.chkExpandAll = false;
            }
            else
            {
                this.chkExpandAll = true;
                this.btnExpand.Text = "Expand";
                this.UGrid_Rpt.Rows.CollapseAll(true);
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
                    DB.ExecuteSQL(string.Format(" delete from {0} where reportid = {1};", "tbl_ReportConfigDtls", strSelect[0].ToString()) + string.Format(" delete from {0} where UserID=" + Db_Detials.UserID + " and CompanyID=" + Db_Detials.CompID + " and reportid = {1};", "tbl_ReportConfigMain", strSelect[0].ToString()));
                    Navigate.ShowMessage(CIS_DialogIcon.SecuritySuccess, "Success", "Record Deleted Successfully.");
                    CIS_MultiColumnComboBox.CIS_MultiColumnComboBox ex = this.cboRptlst;
                    ex.DataSource = DB.GetDT(" sp_ShowReportList '' ", false);
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
                    UltraGridRow activeRow = UGrid_Rpt.ActiveRow;
                    string strColumnValue = activeRow.Cells[0].Value.ToString();
                    //if (cboLedger.SelectedValue != null && cboLedger.SelectedValue.ToString() != "0")
                    //{
                        strNextLevelID = DB.GetSnglValue("select NextLevelId from tbl_ReportLevel where MenuID='" + cboRptlst.SelectedValue + "' and LevlId=" + cboLedger.SelectedValue);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Please Select Ledger Name...");
                    //    return;
                    //}

                    if (strNextLevelID == "")
                    {
                        strNextLevelID = "0";
                    }
                    
                    if (strNextLevelID != "0" && strNextLevelID !="5")
                    {
                        cboLedger.SelectedValue = strNextLevelID;
                        strPreviousVal = strColumnValue;
                        arrlist.Add(strPreviousVal);
                        FillUgrid(strColumnValue);
                    }
                    else
                    {
                        if (this.WindowState != FormWindowState.Minimized)
                        {
                            string strMouleID = DB.GetSnglValue("SELECT MenuID From tbl_MenuMaster Where FormType='T' and Menu_Caption = (select Menu_Caption from tbl_MenuMaster where MenuID= " + cboRptlst.SelectedValue +")");
                            //string strMouleID = DB.GetSnglValue("select MenuID from tbl_MenuMaster where MenuID=" + cboRptlst.SelectedValue);
                           
                            EventHandles.ShowbyFormID(strMouleID, null, null, -1, -1);
                            object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                            Navigate.ShowbyID(objectValue, "[" + ((DataTable)NewLateBinding.LateGet(objectValue, null, "ds", new object[0], null, null, null)).Columns[0].ColumnName + "]", (int)Math.Round(Conversion.Val(activeRow.Cells[0].Text)));
                            
                            #region Collapse MDI Menu Dock
                            object objMDI1 = RuntimeHelpers.GetObjectValue(Navigate.GetForm_byName("MDIMain"));
                            dynamic objfrm = objMDI1;
                            try
                            {
                                objfrm.pnlDockTop.Expand = true;
                            }
                            catch { }
                            #endregion

                            this.WindowState = FormWindowState.Minimized;
                        }
                    }
                }
            }

        }

        //private void SetupPrint(CancelablePrintEventArgs e)
        //{
        //    e.PrintDocument.PrinterSettings.PrintRange = PrintRange.AllPages;
        //    string strQry = "";
        //    using (SqlDataReader iDr = DB.GetRS(string.Format("Select * From {0} Where CompanyID = {1} And YearId = {2}", "Vw_CompanyMaster", Db_Detials.CompID, Db_Detials.YearID)))
        //    {
        //        if (iDr.Read())
        //        {
        //            strQry = iDr["CompanyName"].ToString() + Environment.NewLine;
        //            if (iDr["FactoryAdd"].ToString() != "-")
        //            {
        //                strQry = strQry + Environment.NewLine + "Factory : " + iDr["FactoryAdd"].ToString().Replace("-", "");
        //            }
        //            if (iDr["M_PhoneNo"].ToString() != "-")
        //            {
        //                strQry = strQry + Environment.NewLine + "Tel. No : " + iDr["M_PhoneNo"].ToString().Replace("/", "") + Environment.NewLine;
        //            }
        //            strQry = strQry + Environment.NewLine + this.cboRptlst.Text.ToUpper() + Environment.NewLine;
        //            strQry = strQry + Environment.NewLine + "From Date : " + Localization.ToVBDateString(this.dtFrom.Text) + " To " + Localization.ToVBDateString(this.dtTo.Text);
        //            e.DefaultLogicalPageLayoutInfo.PageHeader = strQry;
        //            e.DefaultLogicalPageLayoutInfo.PageFooterBorderStyle = UIElementBorderStyle.Solid;
        //            e.DefaultLogicalPageLayoutInfo.PageHeaderBorderStyle = UIElementBorderStyle.Solid;
        //        }
        //    }
        //    e.DefaultLogicalPageLayoutInfo.PageHeaderHeight = 140;
        //    Infragistics.Win.Appearance ex = e.DefaultLogicalPageLayoutInfo.PageHeaderAppearance;
        //    ex.FontData.Bold = DefaultableBoolean.True;
        //    ex.TextHAlign = HAlign.Center;
        //    ex.FontData.SizeInPoints = 10f;
        //    ex = null;
        //    strQry = Application.ProductName + " :: " + Application.CompanyName;
        //    e.DefaultLogicalPageLayoutInfo.PageFooter = strQry + Environment.NewLine + Environment.NewLine + "Page <#> of <##> .";
        //    e.DefaultLogicalPageLayoutInfo.PageFooterBorderStyle = UIElementBorderStyle.Solid;
        //    e.DefaultLogicalPageLayoutInfo.PageFooterHeight = 50;
        //    Infragistics.Win.Appearance band = e.DefaultLogicalPageLayoutInfo.PageFooterAppearance;
        //    band.FontData.Bold = DefaultableBoolean.False;
        //    band.ForeColor = Color.Silver;
        //    band.TextHAlign = HAlign.Right;
        //    band.FontData.SizeInPoints = 8f;
        //    band.TextTrimming = TextTrimming.Character;
        //    band = null;
        //    e.DefaultLogicalPageLayoutInfo.ClippingOverride = ClippingOverride.Yes;
        //}

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

        //private void cboRptlst_SelectedValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string FromDt = Localization.ToVBDateString(DB.GetSnglValue(string.Format("Select Yr_From From tbl_YearMaster Where YearID = {0}", Db_Detials.YearID)));
        //        string ToDate = Localization.ToVBDateString(Conversions.ToString(DateAndTime.Now.Date));
        //        Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_Ledger, "");
        //        string[] strSelect = this.cboRptlst.SelectedValue.ToString().Split(new char[] { ';' });
        //        string ReportName = this.cboRptlst.SelectedText;
        //        switch (strSelect[2])
        //        {

        //            case "sp_BookIssueDesignWise":
        //                if (!this.isMultyFilter)
        //                {
        //                    this.lblLedger.Enabled = false;
        //                    //this.cboLedger.Enabled = false;
        //                    this.dtFrom.Enabled = true;
        //                    this.dtTo.Enabled = true;
        //                    this.dtFrom.Text = FromDt;
        //                    this.dtTo.Text = ToDate;
        //                    Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_FabricDesign, "");
        //                }
        //                break;

        //            case "sp_UserLoginStatusReport":
        //                if (!this.isMultyFilter)
        //                {
        //                    this.lblLedger.Enabled = false;
        //                    //this.cboLedger.Enabled = false;
        //                    this.dtFrom.Enabled = true;
        //                    this.dtTo.Enabled = true;
        //                    this.dtFrom.Text = FromDt;
        //                    this.dtTo.Text = ToDate;
        //                    Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_LoginStatus, "");
        //                }
        //                break;

        //            case "sp_DailyWorkInwarRpt":
        //            case "sp_DailyWrkInwrdClientWise":
        //                if (!this.isMultyFilter)
        //                {
        //                    this.lblLedger.Enabled = false;
        //                    //this.cboLedger.Enabled = false;
        //                    this.dtFrom.Enabled = true;
        //                    this.dtTo.Enabled = true;
        //                    this.dtFrom.Text = FromDt;
        //                    this.dtTo.Text = ToDate;
        //                    Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_LedgerAll, "");
        //                }
        //                break;

        //            default:
        //                if (!this.isMultyFilter)
        //                {
        //                    this.lblLedger.Enabled = false;
        //                    //this.cboLedger.Enabled = false;
        //                    this.dtFrom.Enabled = true;
        //                    this.dtTo.Enabled = true;
        //                    this.dtFrom.Text = FromDt;
        //                    this.dtTo.Text = ToDate;
        //                    Combobox_Setup.FillCbo(ref cboLedger, Combobox_Setup.ComboType.Mst_Ledger, "");
        //                }
        //                break;
        //        }
        //    }
        //    catch { }

        //}

        private void frmReportToolNew_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (UGrid_Rpt.Rows.Count > 0)
                {
                    UltraGridRow activeRow = UGrid_Rpt.ActiveRow;
                    ArrayList arlist = arrlist;
                    int icount = arlist.Count;
                    string strColumnValue = string.Empty;

                    //if (icount >= 1)
                    //{
                    //    strColumnValue = arlist[icount - 1].ToString();
                    //}
                    //else
                    //{
                    //    strColumnValue = "0";
                    //}

                    strColumnValue = "0";
                    //if (cboLedger.SelectedValue != null && cboLedger.SelectedValue.ToString() != "0" )
                    //{
                        strPreviousLevelID = DB.GetSnglValue("select PreviousLevelId from tbl_ReportLevel where MenuID='" + cboRptlst.SelectedValue + "' and LevlId=" + cboLedger.SelectedValue);
                    //}
                    //else
                    //{
                        //MessageBox.Show("Please Select Ledger Name...");
                        //return;
                    //}

                    if (strPreviousLevelID != "0")
                    {
                        cboLedger.SelectedValue = strPreviousLevelID;
                        FillUgrid(strColumnValue);
                        arrlist.Remove(strColumnValue);
                        //strColumnValue = activeRow.Cells[0].Value.ToString();
                    }
                    else
                    {
                        CloseForm();
                    }
                }
                else
                {
                    CloseForm();
                }
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

        private void UGrid_Rpt_ClickCell(object sender, ClickCellEventArgs e)
        {

        }

        private void UGrid_Rpt_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void cboRptlst_SelectedValueChanged(object sender, EventArgs e)
        {
            string sqlQuery = string.Format("Select Distinct LevlID,LevelName from {0} where MenuID=" + cboRptlst.SelectedValue + "", "tbl_ReportLevel");
            Combobox_Setup.Fill_Combo(this.cboLedger, sqlQuery, "LevelName", "LevlId");
            cboLedger.ColumnWidths = "0;100";
            cboLedger.AutoComplete = true;
            cboLedger.AutoDropdown = true;
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
            this.UGrid_Rpt.DataSource = DB.GetDT(sQryNM + " " + sQry + "", 240);
            // dt = DB.GetDT(sQryNM + " " + sQry + "", false);
            ultrchrt.Visible = false;
            UGrid_Rpt.Visible = true;
        }
    }
}
