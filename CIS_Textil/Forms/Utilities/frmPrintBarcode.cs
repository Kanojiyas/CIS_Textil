using System;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DBLayer;
using CIS_Bussiness;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class frmPrintBarcode : frmMasterIface
    {
        private int CheckBoxID;
        public bool IsMultiSelect;
        private bool FO_BOOKDESIGN;
        private int SelChkBox;
        public string sSerialID;

        public frmPrintBarcode()
        {
            this.CheckBoxID = 1;
            this.SelChkBox = 0;
            this.IsMultiSelect = true;
            InitializeComponent();
        }

        private void frm_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref CboDesign, Combobox_Setup.ComboType.Mst_FabricDesign, "");
                Combobox_Setup.FillCbo(ref CboShade, Combobox_Setup.ComboType.Mst_FabricShade, "");
                Combobox_Setup.FillCbo(ref cboQuality, Combobox_Setup.ComboType.Mst_FabricQuality, "");
                Combobox_Setup.FillCbo(ref CboSerial, Combobox_Setup.ComboType.Mst_FabricSerial, "");

                FO_BOOKDESIGN = Localization.ParseBoolean(GlobalVariables.FO_BOOKDESIGN);
                string sqlQuery = string.Format("Select ReportID, FormName From tbl_ReportList Where ModuleID = {0} and FormName<>'-' and IsBarcode = 1", base.iIDentity);
                BandEnumerator enumerator = this.UGrid_Rpt.DisplayLayout.Bands.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ColumnEnumerator enumerator2 = enumerator.Current.Columns.GetEnumerator();
                    while (enumerator2.MoveNext())
                    {
                        UltraGridColumn current = enumerator2.Current;
                        if (current.Index == 2)
                        {
                            current.CellActivation = Activation.AllowEdit;
                        }
                        else
                        {
                            current.CellActivation = Activation.NoEdit;
                        }
                    }
                }
                Combobox_Setup.Fill_Combo(this.cboSelectReport, sqlQuery, "FormName", "ReportID");
                cboSelectReport.ColumnWidths = "0;100";
                cboSelectReport.AutoComplete = true;
                cboSelectReport.AutoDropdown = true;


                if (cboSelectReport.Items.Count > 0)
                {
                    cboSelectReport.SelectedIndex = 0;
                }

                string pkInstalledPrinters = null;
                foreach (string pkInstalledPrinters_loopVariable in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    pkInstalledPrinters = pkInstalledPrinters_loopVariable;
                    cboPrinter.Items.Add(pkInstalledPrinters);
                }

                this.dtToDate.Text = Conversions.ToString(DateAndTime.Now.Date);
                this.dtFromDate.Text = Conversions.ToString(DateAndTime.DateAdd(DateInterval.Day, -15.0, DateAndTime.Now.Date));
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        public bool validate()
        {
            if (cboSelectReport.SelectedValue == null || cboSelectReport.SelectedValue.ToString() == "-" || cboSelectReport.SelectedValue.ToString() == "")
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Report");
                cboSelectReport.Focus();
                return true;
            }
            return false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                bool isPrintDone = false;
                bool IsReportPrint = false;
                if (!validate())
                {
                    int num2;
                    int num = 0;
                    string str = "";
                    UltraGrid grid = this.UGrid_Rpt;
                    if (Localization.ParseBoolean(DB.GetSnglValue(string.Format("Select isBarcode from {0} Where ReportID = {1} ", "tbl_ReportList", RuntimeHelpers.GetObjectValue(this.cboSelectReport.SelectedValue)))))
                    {
                        int num3 = grid.Rows.Count - 1;
                        for (num2 = 0; num2 <= num3; num2++)
                        {
                            if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(grid.Rows[num2].Cells[1].Value, true, false))
                            {
                                EventHandles.PrintBarcode(this.cboSelectReport.Text, Conversions.ToString(grid.Rows[num2].Cells[0].Value), Conversions.ToString(grid.Rows[num2].Cells[3].Value), Conversions.ToString(grid.Rows[num2].Cells[2].Value), Conversions.ToString(grid.Rows[num2].Cells[4].Value), Conversions.ToString(grid.Rows[num2].Cells[6].Value), Conversions.ToDecimal(grid.Rows[num2].Cells[5].Value), 0, decimal.Zero);
                                isPrintDone = true;
                            }
                        }
                    }
                    else
                    {
                        using (IDataReader reader = DB.GetRS(string.Format("Select * From {0} Where ReportID = {1} ", "tbl_ReportList", RuntimeHelpers.GetObjectValue(this.cboSelectReport.SelectedValue))))
                        {
                            while (reader.Read())
                            {
                                this.cboPrinter.SelectedItem = reader["PrinterName"].ToString();
                                str = reader["PageSize"].ToString();
                                num = Conversions.ToInteger(reader["PrintCopies"].ToString());
                            }
                        }
                        CIS_ReportTool.frmReportViewer frmRpt = new CIS_ReportTool.frmReportViewer();
                        frmRpt.PrinterID = this.cboPrinter.SelectedIndex;
                        frmRpt.PrintCopies = num;
                        frmRpt.PaperSize = str;
                        frmRpt.PaperOrientation = true;
                        UltraGrid grid2 = this.UGrid_Rpt;
                        int num4 = grid2.Rows.Count - 1;
                        for (num2 = 0; num2 <= num4; num2++)
                        {
                            if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(grid2.Rows[num2].Cells[1].Value, true, false))
                            {
                                frmRpt.GenerateReport(base.iIDentity, this.dtFromDate.Text, this.dtToDate.Text, 0, Conversions.ToString(grid2.Rows[num2].Cells[0].Value), 0, true, 0, true, Localization.ParseNativeInt(cboSelectReport.SelectedValue.ToString()));
                                IsReportPrint = true;
                            }
                        }
                        grid2 = null;
                        frmRpt = null;
                    }
                    if (isPrintDone)
                    {
                        int iactionType = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster() Where MiscName='IsPrint'"));
                        DBSp.Log_CurrentUser(base.iIDentity, iactionType, 0, 0, 1, 0);
                    }
                    if (IsReportPrint)
                    {
                        int iactionType = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster() Where MiscName='IsPrint'"));
                        DBSp.Log_CurrentUser(base.iIDentity, iactionType, 0, 1, 0, 0);
                    }
                    grid = null;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                string left = string.Empty;
                if (Conversion.Val(RuntimeHelpers.GetObjectValue(this.cboQuality.SelectedValue)) != Conversions.ToDouble("0"))
                {
                    left = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.AddObject(left, Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject("Where FabricQualityID = ", this.cboQuality.SelectedValue)));
                    if (Conversion.Val(RuntimeHelpers.GetObjectValue(this.CboDesign.SelectedValue)) != Conversions.ToDouble("0"))
                    {
                        left = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.AddObject(left, Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(" And FabricDesignID = ", this.CboDesign.SelectedValue)));
                    }
                    if (Conversion.Val(RuntimeHelpers.GetObjectValue(this.CboShade.SelectedValue)) != Conversions.ToDouble("0"))
                    {
                        left = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.AddObject(left, Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(" And FabricShadeID = ", this.CboShade.SelectedValue)));
                    }
                }
                else if (Conversion.Val(RuntimeHelpers.GetObjectValue(this.CboDesign.SelectedValue)) != Conversions.ToDouble("0"))
                {
                    left = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.AddObject(left, Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject("Where FabricDesignID = ", this.CboDesign.SelectedValue)));
                    if (Conversion.Val(RuntimeHelpers.GetObjectValue(this.CboShade.SelectedValue)) != Conversions.ToDouble("0"))
                    {
                        left = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.AddObject(left, Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(" And FabricShadeID = ", this.CboShade.SelectedValue)));
                    }
                    if (Conversion.Val(RuntimeHelpers.GetObjectValue(this.cboQuality.SelectedValue)) != Conversions.ToDouble("0"))
                    {
                        left = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.AddObject(left, Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(" And FabricQualityID = ", this.cboQuality.SelectedValue)));
                    }
                }
                else if (Conversion.Val(RuntimeHelpers.GetObjectValue(this.CboShade.SelectedValue)) != Conversions.ToDouble("0"))
                {
                    left = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.AddObject(left, Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject("Where FabricShadeID = ", this.CboShade.SelectedValue)));
                    if (Conversion.Val(RuntimeHelpers.GetObjectValue(this.CboDesign.SelectedValue)) != Conversions.ToDouble("0"))
                    {
                        left = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.AddObject(left, Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(" And FabricDesignID = ", this.CboDesign.SelectedValue)));
                    }
                    if (Conversion.Val(RuntimeHelpers.GetObjectValue(this.cboQuality.SelectedValue)) != Conversions.ToDouble("0"))
                    {
                        left = Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.AddObject(left, Microsoft.VisualBasic.CompilerServices.Operators.ConcatenateObject(" And FabricQualityID = ", this.cboQuality.SelectedValue)));
                    }
                }
                if ((Strings.Len(this.dtFromDate.Text.ToString()) > 0) & (Strings.Len(this.dtToDate.Text.ToString()) > 0))
                {
                    this.UGrid_Rpt.DataSource = DB.GetDT(string.Format("Select PieceNo,Sel,FabricQualityName,FabricDesignName,FabricShadeName,Mtrs,LotNo from {0}('{1}','{2}',{3},{4}) " + left, new object[] { "dbo.fn_SearchFabricStock_Barcode", Localization.ToSqlDateString(this.dtFromDate.Text), Localization.ToSqlDateString(this.dtToDate.Text), Db_Detials.CompID, Db_Detials.YearID }), false);
                    this.txtTotlRows.Text = Conversions.ToString(this.UGrid_Rpt.Rows.Count);
                }
                BandEnumerator enumerator = this.UGrid_Rpt.DisplayLayout.Bands.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ColumnEnumerator enumerator2 = enumerator.Current.Columns.GetEnumerator();
                    while (enumerator2.MoveNext())
                    {
                        UltraGridColumn current = enumerator2.Current;
                        if (current.Index == 1)
                        {
                            current.CellActivation = Activation.AllowEdit;
                        }
                        else
                        {
                            current.CellActivation = Activation.NoEdit;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }

        }

        private void UGrid_Rpt_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            e.Layout.Override.RowSizing = RowSizing.Free;
            e.Layout.Bands[0].AutoPreviewEnabled = true;
            e.Layout.Override.FilterUIType = FilterUIType.FilterRow;
            e.Layout.Override.FilterEvaluationTrigger = FilterEvaluationTrigger.OnCellValueChange;
            e.Layout.Override.FilterOperatorLocation = FilterOperatorLocation.WithOperand;
            e.Layout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.StartsWith;
            e.Layout.Override.FilterClearButtonLocation = FilterClearButtonLocation.RowAndCell;
            e.Layout.Override.FilterRowAppearance.BackColor = Color.LightYellow;
            e.Layout.Override.FilterRowPromptAppearance.BackColorAlpha = Alpha.Opaque;
            e.Layout.Override.SpecialRowSeparator = SpecialRowSeparator.FilterRow;
            e.Layout.Override.SpecialRowSeparatorAppearance.BackColor = Color.FromArgb(0xe9, 0xf2, 0xc7);
            e.Layout.Override.AllowRowSummaries = AllowRowSummaries.True;
            e.Layout.Override.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
            e.Layout.Override.SummaryDisplayArea |= SummaryDisplayAreas.GroupByRowsFooter;
            e.Layout.Override.SummaryDisplayArea |= SummaryDisplayAreas.InGroupByRows;
            e.Layout.Override.GroupBySummaryDisplayStyle = GroupBySummaryDisplayStyle.SummaryCells;
            e.Layout.Override.SummaryFooterAppearance.BackColor = SystemColors.Info;
            e.Layout.Override.SummaryValueAppearance.BackColor = SystemColors.Window;
            e.Layout.Override.SummaryValueAppearance.FontData.Bold = DefaultableBoolean.True;
            e.Layout.Override.GroupBySummaryValueAppearance.BackColor = SystemColors.Window;
            e.Layout.Override.GroupBySummaryValueAppearance.TextHAlign = HAlign.Right;
            e.Layout.Bands[0].SummaryFooterCaption = "Grand Totals:";
            e.Layout.Override.SummaryFooterCaptionAppearance.FontData.Bold = DefaultableBoolean.True;
            e.Layout.Override.SummaryFooterCaptionVisible = DefaultableBoolean.False;
            e.Layout.Override.SpecialRowSeparator = SpecialRowSeparator.SummaryRow;
            e.Layout.Override.SpecialRowSeparatorAppearance.BackColor = Color.FromArgb(0xda, 0xd9, 0xf1);
            e.Layout.Override.SpecialRowSeparatorHeight = 6;
            e.Layout.Override.BorderStyleSpecialRowSeparator = UIElementBorderStyle.RaisedSoft;
            e.Layout.Override.CellClickAction = CellClickAction.EditAndSelectText;
            e.Layout.Override.SelectTypeRow = SelectType.None;
            e.Layout.ViewStyle = ViewStyle.SingleBand;
            e.Layout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
        }

        private void UGrid_Rpt_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Space)
                {
                    if ((this.UGrid_Rpt.Rows.Count != 0) && (this.UGrid_Rpt.ActiveRow.Index != -1))
                    {
                        if (this.UGrid_Rpt.ActiveCell.Column.Index == this.CheckBoxID)
                        {
                            if (!this.IsMultiSelect)
                            {
                                int num2 = this.UGrid_Rpt.Rows.Count - 1;
                                for (int i = 0; i <= num2; i++)
                                {
                                    this.UGrid_Rpt.Rows[i].Cells[this.CheckBoxID].Value = false;
                                    this.UGrid_Rpt.Rows[i].Cells[this.CheckBoxID].Selected = false;
                                }
                            }
                            if (Microsoft.VisualBasic.CompilerServices.Operators.ConditionalCompareObjectEqual(this.UGrid_Rpt.ActiveRow.Cells[this.CheckBoxID].Value, true, false))
                            {
                                this.UGrid_Rpt.ActiveRow.Cells[this.CheckBoxID].Selected = false;
                                this.UGrid_Rpt.ActiveRow.Cells[this.CheckBoxID].Value = false;
                                this.SelChkBox--;
                            }
                            else
                            {
                                this.UGrid_Rpt.ActiveRow.Cells[this.CheckBoxID].Selected = true;
                                this.UGrid_Rpt.ActiveRow.Cells[this.CheckBoxID].Value = true;
                                this.SelChkBox++;
                            }
                        }
                        else
                        {
                            this.UGrid_Rpt.PerformAction(UltraGridAction.EnterEditMode);
                        }
                    }
                }
                else if (e.KeyCode == Keys.Return)
                {
                    this.UGrid_Rpt.PerformAction(UltraGridAction.NextCell);
                }
                else if (((e.KeyCode == Keys.Right) || (e.KeyCode == Keys.Left)) || (e.KeyCode != Keys.Down))
                {
                }
                this.txtTotlSel.Text = Conversions.ToString(this.SelChkBox);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }

        }

        private enum Col
        {
            Col_ID,
            Data_Col,
            Data_rmd,
            Data_rmdnew,
            Align
        }

        private void cboQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (CboDesign.SelectedValue != null && CboDesign.SelectedValue.ToString() != "0" && CboDesign.SelectedValue.ToString() != "-" && cboQuality.SelectedValue != null && cboQuality.SelectedValue.ToString() != "0" && cboQuality.SelectedValue.ToString() != "-" && CboShade.SelectedValue != null && CboShade.SelectedValue.ToString() != "0" && CboShade.SelectedValue.ToString() != "-")
                {
                    sSerialID = DB.GetSnglValue("Select serialID from fn_BookSerialMaster_Tbl() Where QualityID=" + cboQuality.SelectedValue + " and DesignID=" + CboDesign.SelectedValue + " and ShadeID=" + CboShade.SelectedValue + "");

                    if (sSerialID != null && sSerialID != "")
                    {
                        CboSerial.SelectedValue = sSerialID;
                        CboSerial.Enabled = false;
                    }
                    else
                    {
                        CboSerial.SelectedValue = 0;
                        CboSerial.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }

        }

        private void CboDesign_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboQuality_SelectedIndexChanged(null, null);
        }

        private void CboShade_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboQuality_SelectedIndexChanged(null, null);
        }

    }
}
