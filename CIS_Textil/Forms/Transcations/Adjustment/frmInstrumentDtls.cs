using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using  CIS_Bussiness;using CIS_DBLayer;
using CIS_Textil.Properties;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class frmInstrumentDtls : Form
    {
        [AccessedThroughProperty("Ref_fgDtls")]
        private DataGridViewEx _Ref_fgDtls;
        private DataGridViewEx fgDtls;
        public DataTable Ref_Dt_tmp;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        public virtual DataGridViewEx Ref_fgDtls
        {
            [DebuggerNonUserCode]
            get
            {
                return this._Ref_fgDtls;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                this._Ref_fgDtls = value;
            }
        }

        private SplitContainer _spc_Adj;
        public double dblAmount;
        private double dblPAdjAmt;
        private double dblPrevAdjAmt;
        private double dblTempAdjAmt;
        private double dblTempAdjAmt_Cr;
        private AdjType eAdjType;
        public Enum_Define.ActionType FormAction;
        public double pLedgerID;
        public string Ref_Date;
        public int Ref_intRowNum;
        public int Ref_TxnCode;
        private string Ref_FormName;
        public Db_Detials.Ac_DrCr RefDrCr;
        public int RefModID;
        public int TransId;

        private int NewRefID;
        private int AgainstRefID;
        private int AdvanceID;
        private int OnAccountID;

        private int DrID;
        private int CrID;

        public frmInstrumentDtls()
        {
            InitializeComponent();
            fgDtls = new DataGridViewEx();
            Ref_fgDtls = new DataGridViewEx();
            Ref_Dt_tmp = new DataTable();
        }

        #region FormEvent

        private void frmInstrumentDtls_Load(object sender, EventArgs e)
        {
            this.spc_Adj.Panel2.BackColor = Color.FromArgb(49, 132, 155);
            object PNL = this.spc_Adj.Panel1;
            Navigate.SetPropertydtlGrid(PNL, fgDtls, DockStyle.Fill);
            fgDtls.EditMode = DataGridViewEditMode.EditOnKeystroke;

            fgDtls.KeyDown += new KeyEventHandler(EventHandles.fgDtls_KeyDown);
            fgDtls.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(EventHandles.fgDtls_EditingControlShowing);
            fgDtls.DataError += new DataGridViewDataErrorEventHandler(EventHandles.fgDtls_DataError);
            fgDtls.CellEnter += new DataGridViewCellEventHandler(EventHandles.fgDtls_CellEnter);

            DataGridView view = fgDtls;
            DetailGrid_Setup.AddColto_GridCombo(ref   view, 30, 0, "fn_LedgerMastermain()", "LedgerName", "LedgerName", true, false, false, "LedgerName", "LedgerID", "", "", "", null, 0.0);
            DetailGrid_Setup.AddColto_Grid(ref  view, 1, "Recieved From", "RecievedFrom", 100, 10, 0, true, true, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
            DetailGrid_Setup.AddColto_GridCombo(ref   view, 80, 2, "fn_MiscMaster_tbl()", "Transaction Type", "TransactionType", true, false, false, "MiscName", "MiscID", "", "MiscType='PAYMENTTYPE'", "", null, 0.0);
            DetailGrid_Setup.AddColto_Grid(ref  view, 3, "Inst. No", "InstNo", 100, 10, 0, true, false,false, Enum_Define.DataType.pString,DataGridViewContentAlignment.MiddleLeft, "");
            DetailGrid_Setup.AddColto_Grid(ref  view, 4, "Inst. Date.", "InstDate", 100, 10, 0, true, true, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
            DetailGrid_Setup.AddColto_Grid(ref  view, 5, "Bank.", "Bank", 80, 10, 0, false, false, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
            DetailGrid_Setup.AddColto_Grid(ref  view, 6, "Branch", "Branch", 80, 10, 0, false, false, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
            DetailGrid_Setup.AddColto_GridCombo(ref   view, 80, 7, "fn_MiscMaster_tbl()", "Transfer Mode", "TransferMode", true, false, false, "MiscName", "MiscID", "", "MiscType='TransferMode'", "", null, 0.0);
            DetailGrid_Setup.AddColto_Grid(ref  view, 8, "Amount", "Amount", 80, 10, 2, true, false, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleRight, "");
            DetailGrid_Setup.AddColto_Grid(ref  view, 9, "RowID", "RowID", 120, 10, 0, true, true, false, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleRight, "");
            
            Ref_FormName = DB.GetSnglValue("SELECT Menu_Caption FROM tbl_MenuMaster WHERE MenuID = " + RefModID);
            fgDtls.RowCount = 1;
            //Show_Adjustments();
            this.eAdjType = AdjType.Manual;           
            fgDtls.Rows.Add();
            fgDtls.CellValueChanged += new DataGridViewCellEventHandler(fgDtls_CellValueChanged);
            fgDtls.CellEndEdit += new DataGridViewCellEventHandler(fgDtls_CellEndEdit);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                {
                    int i;
                    DataGridViewEx ex = Ref_fgDtls;
                    DataGridViewRow row = ex.Rows[Ref_intRowNum];
                    row.DefaultCellStyle.BackColor = Color.FromArgb(0, 165, 165);
                    row.DefaultCellStyle.Font = new Font("Verdana", 8f, FontStyle.Bold, GraphicsUnit.Point, 0);
                    row.Cells[5].Value = Resources.AdjAmt;
                    int intAddRow;
                    int iRowCnts = ex.RowCount - 1;
                    for (i = Ref_intRowNum + 1; i <= iRowCnts; i++)
                    {
                        if (ex.Rows[Ref_intRowNum].Cells[9].Value != null)
                        {
                            if (Localization.ParseNativeInt(ex.Rows[Ref_intRowNum].Cells[9].Value.ToString()) == Ref_intRowNum)
                            {
                                if (ex.Rows[Ref_intRowNum + 1].Cells[16].Value.ToString() == "")
                                {
                                    ex.Rows.RemoveAt(Ref_intRowNum + 1);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }

                    ex.Rows[Ref_intRowNum].Cells[16].Value = "1";

                    DataTable Dt = DB.GetDT("SELECT OrderNo, DrCrID,LedgerID  from  tbl_AdjustmentLedgers WHERE FormID=" + RefModID + "", false);
                    intAddRow = Ref_intRowNum + 1;
                    int j = this.fgDtls.RowCount - 1;
                    i = 0;
                    while (i <= j)
                    {
                        if (this.fgDtls.Rows[i].Cells[9].Value != null)
                        {
                            if (Localization.ParseNativeDouble(this.fgDtls.Rows[i].Cells[9].Value.ToString()) > 0.0)
                            {
                                ex.Rows.Insert(intAddRow, new DataGridViewRow());
                                DataGridViewRow row2 = ex.Rows[intAddRow];

                                row2.Cells[1].Value = i + 1;
                                row2.Cells[2].Value = Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[0].Value.ToString());

                                if ((Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == AgainstRefID) || (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == NewRefID))
                                {
                                    if (this.fgDtls.Rows[i].Cells[5].Value.ToString() == "On Account")
                                    {
                                        row2.Cells[3].Value = "On Account";
                                        row2.Cells[19].Value = OnAccountID;
                                    }
                                    else
                                        row2.Cells[3].Value = this.fgDtls.Rows[i].Cells[5].Value + "-" + this.fgDtls.Rows[i].Cells[6].Value;

                                }
                                else if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == OnAccountID)
                                    row2.Cells[3].Value = this.fgDtls.Rows[i].Cells[2].FormattedValue;
                                else if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == AdvanceID)
                                    row2.Cells[3].Value = this.fgDtls.Rows[i].Cells[5].Value + "-" + this.fgDtls.Rows[i].Cells[6].Value + "(" + this.fgDtls.Rows[i].Cells[2].FormattedValue + ")";
                                else
                                    row2.Cells[3].Value = 0;

                                row2.Cells[5].Value = Resources.Img_NoClick;
                                row2.Cells[6].Value = string.Format("{0:N2}", Localization.ParseNativeDouble(this.fgDtls.Rows[i].Cells[12].Value.ToString()));

                                if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == AgainstRefID)
                                    row2.Cells[11].Value = this.fgDtls.Rows[i].Cells[4].Value;
                                else
                                    row2.Cells[11].Value = Ref_TxnCode;

                                row2.Cells[12].Value = this.fgDtls.Rows[i].Cells[3].Value;

                                if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == OnAccountID)
                                    row2.Cells[13].Value = RefModID;
                                else
                                    row2.Cells[13].Value = (this.fgDtls.Rows[i].Cells[1].Value != null ? this.fgDtls.Rows[i].Cells[1].Value : 0);

                                if ((Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == AgainstRefID) || (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == NewRefID))
                                    row2.Cells[14].Value = this.fgDtls.Rows[i].Cells[5].Value;
                                else if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == OnAccountID)
                                    row2.Cells[14].Value = this.fgDtls.Rows[i].Cells[2].FormattedValue;
                                else if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == AdvanceID)
                                    row2.Cells[14].Value = Ref_TxnCode;

                                row2.Cells[15].Value = this.fgDtls.Rows[i].Cells[6].Value;
                                row2.Cells[16].Value = string.Empty;
                                row2.Cells[17].Value = "1";
                                row2.Cells[18].Value = "0";
                                if (this.fgDtls.Rows[i].Cells[5].Value.ToString() == "On Account")
                                    row2.Cells[19].Value = OnAccountID;
                                else
                                    row2.Cells[19].Value = this.fgDtls.Rows[i].Cells[2].Value;

                                row2.Cells[20].Value = Ref_intRowNum;

                                if (this.fgDtls.Rows[i].Cells[1].Value != null)
                                {
                                    if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[1].Value.ToString()) == RefModID)
                                    {
                                        int iBackClr = Ref_fgDtls.ColumnCount - 1;
                                        for (int n = 0; n <= iBackClr; n++)
                                        {
                                            row2.Cells[n].Style.Font = new Font("Verdana", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);

                                            if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == AgainstRefID)
                                                row2.Cells[n].Style.BackColor = Color.FromArgb(170, 225, 225);
                                            else if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == OnAccountID)
                                                row2.Cells[n].Style.BackColor = this.lblOnAc_Clr.BackColor;
                                            else if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == NewRefID)
                                                row2.Cells[n].Style.BackColor = this.lblNewRef_Clr.BackColor;
                                            else if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == AdvanceID)
                                                row2.Cells[n].Style.BackColor = this.lblAdvance_Clr.BackColor;
                                        }
                                    }
                                }
                                int m = Ref_fgDtls.ColumnCount - 1;
                                for (int k = 0; k <= m; k++)
                                {
                                    row2.Cells[k].ReadOnly = true;
                                }

                                intAddRow++;
                                for (int k = 14; k <= this.fgDtls.ColumnCount - 1; k++)
                                {
                                    if (this.fgDtls.Rows[i].Cells[k].Value != null)
                                    {
                                        if (Localization.ParseNativeDouble(this.fgDtls.Rows[i].Cells[k].Value.ToString()) > 0)
                                        {
                                            ex.Rows.Insert(intAddRow, new DataGridViewRow());
                                            DataGridViewRow DGMiscLgrs = ex.Rows[intAddRow];
                                            DGMiscLgrs.Cells[1].Value = i + 1;
                                            DataRow[] rst = Dt.Select("OrderNo=" + (k - 13));
                                            if (rst.Length > 0)
                                            {
                                                foreach (DataRow r in rst)
                                                {
                                                    DGMiscLgrs.Cells[2].Value = Localization.ParseNativeInt(r["DrCrID"].ToString());
                                                    DGMiscLgrs.Cells[3].Value = Localization.ParseNativeInt(r["LedgerID"].ToString());
                                                    break;
                                                }
                                            }

                                            DGMiscLgrs.Cells[5].Value = Resources.Img_NoClick;
                                            DGMiscLgrs.Cells[6].Value = string.Format("{0:N2}", Localization.ParseNativeDouble(this.fgDtls.Rows[i].Cells[k].Value.ToString()));
                                            //DGMiscLgrs.Cells[10].Value = Ref_TxnCode;// this.fgDtls.Rows[i].Cells[4].Value;
                                            DGMiscLgrs.Cells[11].Value = this.fgDtls.Rows[i].Cells[4].Value;
                                            //DGMiscLgrs.Cells[11].Value = CommonCls.ShowFormCaptionByID(this.RefModID);// this.fgDtls.Rows[i].Cells[3].Value;
                                            DGMiscLgrs.Cells[12].Value = this.fgDtls.Rows[i].Cells[3].Value;
                                            //DGMiscLgrs.Cells[12].Value = RefModID;// (this.fgDtls.Rows[i].Cells[1].Value != null ? this.fgDtls.Rows[i].Cells[1].Value : 0);
                                            //DGMiscLgrs.Cells[13].Value = Ref_TxnCode;// this.fgDtls.Rows[i].Cells[5].Value;

                                            DGMiscLgrs.Cells[13].Value = (this.fgDtls.Rows[i].Cells[1].Value != null ? this.fgDtls.Rows[i].Cells[1].Value : 0);
                                            DGMiscLgrs.Cells[14].Value = this.fgDtls.Rows[i].Cells[5].Value;

                                            DGMiscLgrs.Cells[15].Value = Ref_Date;
                                            DGMiscLgrs.Cells[16].Value = string.Empty;
                                            DGMiscLgrs.Cells[18].Value = "1";
                                            DGMiscLgrs.Cells[19].Value = this.fgDtls.Rows[i].Cells[2].Value; /* plz confirm */
                                            DGMiscLgrs.Cells[20].Value = Ref_intRowNum;
                                            intAddRow++;
                                        }
                                    }
                                }
                                row2 = null;
                            }
                        }
                        i++;
                    }
                    

                    if (ex.Rows[ex.RowCount - 1].Cells[3].Value == null)
                    {
                        row = ex.Rows[ex.RowCount - 1];
                        row.DefaultCellStyle.BackColor = Color.FromArgb(0, 165, 165);
                        row.DefaultCellStyle.Font = new Font("Verdana", 8f, FontStyle.Bold, GraphicsUnit.Point, 0);
                        row.Cells[5].Value = Resources.AdjAmt;
                        ex.CurrentCell = ex[3, ex.Rows.Count - 1];
                    }

                    #region Save to tmpDatatable
                    for (int r = 0; r <= this.fgDtls.Rows.Count - 1; r++)
                    {
                        if (Ref_Dt_tmp.Rows.Count <= r)
                            this.Ref_Dt_tmp.Rows.Add();

                        // if (this.fgDtls.Rows[r].Cells[0].Value != null)
                        {
                            for (int c = 0; c <= this.fgDtls.Columns.Count - 1; c++)
                            {
                                //if (Localization.ParseNativeDouble(this.fgDtls.Rows[r].Cells[9].Value.ToString()) > 0)
                                {
                                    if (c == 13)
                                        this.Ref_Dt_tmp.Rows[r][13] = Ref_intRowNum;
                                    else
                                        if (this.fgDtls.Rows[r].Cells[c].Value != null)
                                            this.Ref_Dt_tmp.Rows[r][c] = this.fgDtls.Rows[r].Cells[c].Value;
                                        else
                                            this.Ref_Dt_tmp.Rows[r][c] = 0;

                                }
                            }

                        }
                    }
                    #endregion

                    DataGridViewEx fgDtls = Ref_fgDtls;
                    Ref_fgDtls.Rows.Add();
                    CommonCls.SetGridNum(fgDtls);
                    Ref_fgDtls = fgDtls;
                    ex = null;
                    Close();
                    Dispose();

                    CIS_Textil.frmInstrumentDtls adjust = new CIS_Textil.frmInstrumentDtls();
                    adjust.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #endregion FormEvent

        #region Variables

        private enum AdjType
        {
            Auto,
            Manual,
            None
        }

        private enum Col
        {
            EntryDrCr,
            EntryModID,
            EntryType,
            RefDoc,
            pBillNo,
            pRefDate,
            pRefDocAmt,
            pBalAmt,
            pAdjAmt,
            pSel,
            pTempBalAmt
        }

        #endregion Variables

        #region Common Functions


        private int getLedgerIDbyCol(int iCol)
        {
            return Localization.ParseNativeInt(DB.GetSnglValue("SELECT LedgerID from  tbl_AdjustmentLedgers WHERE FormID=" + RefModID + " and OrderNo=" + (iCol - 13)));
        }

        private int getDrCrIDbyCol(int iCol)
        {
            return Localization.ParseNativeInt(DB.GetSnglValue("SELECT DrCrID from  tbl_AdjustmentLedgers WHERE FormID=" + RefModID + " and OrderNo=" + (iCol - 13)));
        }      

        #endregion Common Functions

        #region GridEvent

        private void fgDtls_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (eAdjType == AdjType.Manual)
                {
                    dblTempAdjAmt = Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString());
                    dblPrevAdjAmt = Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[8].Value.ToString());
                }
            }
            catch { }
        }

        private void fgDtls_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //double num;
                DataGridViewRow row = this.fgDtls.Rows[e.RowIndex];
                if ((e.ColumnIndex == 0) || (e.ColumnIndex == 2))
                {
                    {
                        row.Cells[0].Value = null;
                        row.Cells[1].Value = null;
                        row.Cells[2].Value = null;
                       // row.Cells[9].ReadOnly = false;
                       // row.Cells[12].ReadOnly = true;
                        return;
                    }
                    
                }

                if (e.ColumnIndex == 12)
                {
                    if (Localization.ParseNativeDouble(row.Cells[12].Value.ToString()) > 0)
                    {
                        try
                        {
                            if (fgDtls.RowCount == (e.RowIndex + 1))
                            {
                                    fgDtls.Rows.Add();
                            }
                        }
                        catch { fgDtls.Rows.Add(); }
                    }
                }

                if (e.ColumnIndex == 2)
                {
                    if(row.Cells[2].Value == null)
                    {
                        //row.Cells[2].Value = null;
                        row.Cells[9].ReadOnly = true;
                        //row.Cells[12].ReadOnly = true;
                        return;
                    }
                    
                    if (row.Cells[2].Value != null)
                    {
                        fgDtls.Rows[e.RowIndex].Cells[1].Value = RefModID;
                        fgDtls.Rows[e.RowIndex].Cells[3].Value = Ref_FormName;
                        fgDtls.Rows[e.RowIndex].Cells[5].Value = Ref_TxnCode;
                        fgDtls.Rows[e.RowIndex].Cells[6].Value = Ref_Date;
                        if (Localization.ParseNativeInt(row.Cells[2].Value.ToString()) == NewRefID)
                        {
                            if (this.RefDrCr.ToString() == "Debit")
                            {
                                fgDtls.Rows[e.RowIndex].Cells[0].Value = DrID;
                            }
                            else
                            {
                                fgDtls.Rows[e.RowIndex].Cells[0].Value = CrID;
                            }

                            fgDtls.Rows[e.RowIndex].Cells[7].Value = 0;
                            fgDtls.Rows[e.RowIndex].Cells[8].Value = 0;
                            //fgDtls.Rows[e.RowIndex].Cells[12].Value = 0;

                            fgDtls.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                            fgDtls.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                            fgDtls.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                            fgDtls.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                        }
                        else if (Localization.ParseNativeInt(row.Cells[2].Value.ToString()) == AdvanceID)
                        {
                            if (this.RefDrCr.ToString() == "Debit")
                            {
                                fgDtls.Rows[e.RowIndex].Cells[0].Value = DrID;
                            }
                            else
                            {
                                fgDtls.Rows[e.RowIndex].Cells[0].Value = CrID;
                            }
                            fgDtls.Rows[e.RowIndex].Cells[7].Value = 0;
                            fgDtls.Rows[e.RowIndex].Cells[8].Value = 0;
                           // fgDtls.Rows[e.RowIndex].Cells[12].Value = 0;

                            fgDtls.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                            fgDtls.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                            fgDtls.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                            fgDtls.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                        }
                        else
                        {
                            if (this.RefDrCr.ToString() == "Debit")
                            {
                                fgDtls.Rows[e.RowIndex].Cells[0].Value = DrID;
                            }
                            else
                            {
                                fgDtls.Rows[e.RowIndex].Cells[0].Value = CrID;
                            }
                            fgDtls.Rows[e.RowIndex].Cells[7].Value = 0;
                            fgDtls.Rows[e.RowIndex].Cells[8].Value = 0;
                           // fgDtls.Rows[e.RowIndex].Cells[12].Value = 0;

                            fgDtls.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                            fgDtls.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                            fgDtls.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                            fgDtls.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                        }

                        //if ((Localization.ParseNativeInt(row.Cells[2].Value.ToString()) == OnAccountID) || (Localization.ParseNativeInt(row.Cells[2].Value.ToString()) == AdvanceID))
                        //{
                        //    for (int c = 14; c <= fgDtls.ColumnCount - 1; c++)
                        //    { row.Cells[c].ReadOnly = true; }
                        //}
                        //else
                        //{
                        //    for (int c = 14; c <= fgDtls.ColumnCount - 1; c++)
                        //    { row.Cells[c].ReadOnly = false; }
                        //}
                    }
                }

                if (this.eAdjType != AdjType.Manual)
                {
                    return;
                }

               

                

                //if (Localization.ParseNativeDouble(Conversions.ToString(row.Cells[9].Value)) > this.dblAmount)
                //{
                //    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Their is no more amount to adjust.");
                //    row.Cells[9].Value = string.Format("{0:N2}", 0);
                //    //dblextLdrs = gf_CalcExtraLedgers();
                //    txtAdjustedAmt.Text = string.Format("{0:N2}", dblTempAdjAmt_Cr + dblTempAdjAmt/* + dblextLdrs*/);
                //    txtUnAdjustedAnt.Text = string.Format("{0:N2}", dblAmount - Localization.ParseNativeDouble(txtAdjustedAmt.Text)/* - dblextLdrs*/);
                //    return;
                //}
                //txtUnAdjustedAnt.Text = string.Format("{0:N2}", dblAmount - (dblTempAdjAmt_Cr + dblTempAdjAmt) /*- dblextLdrs*/);
                ////dblextLdrs = gf_CalcExtraLedgers();
                //if (Localization.ParseNativeDouble(Conversions.ToString(row.Cells[9].Value)) > 0.0)
                //{
                //    if (Localization.ParseNativeInt(row.Cells[2].Value.ToString()) == AgainstRefID)
                //    {
                //        row.Cells[8].Value = string.Format("{0:N2}", Localization.ParseNativeDouble(Conversions.ToString(row.Cells[11].Value)) - Localization.ParseNativeDouble(Conversions.ToString(row.Cells[9].Value))/* - dblextLdrs*/);
                //    }
                //}               
                //if (Localization.ParseNativeDouble(this.txtUnAdjustedAnt.Text) > 0.0)
                //{

                //    this.txtUnAdjustedAnt.BackColor = Color.Maroon;
                //}
                //else
                //{
                //    this.txtUnAdjustedAnt.BackColor = Color.PowderBlue;
                //}
            }
            catch
            {

            }
        }

        private void fgDtls_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        #endregion GridEvent

        private void frmInstrumentDtls_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void lblDockTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(base.Handle, 0xa1, 2, 0);
            }
        }
    }
}
