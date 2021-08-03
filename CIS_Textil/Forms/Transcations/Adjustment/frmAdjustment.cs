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
    public partial class frmAdjustment : Form
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
        public string strPartyName;

        private int NewRefID;
        private int AgainstRefID;
        private int AdvanceID;
        private int OnAccountID;

        private int DrID;
        private int CrID;

        public frmAdjustment()
        {
            InitializeComponent();
            fgDtls = new DataGridViewEx();
            Ref_fgDtls = new DataGridViewEx();
            Ref_Dt_tmp = new DataTable();
        }

        #region FormEvent

        private void frmAdjustment_Load(object sender, EventArgs e)
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
            if (RefModID == 108)
            {
                DetailGrid_Setup.AddColto_GridCombo(ref   view, 30, 0, "tbl_MiscMaster", "Dr/Cr", "DrCrID", false, false, false, "MiscName", "MiscID", "", "TypeID = 1", "", null, 0.0);
                DetailGrid_Setup.AddColto_Grid(ref  view, 1, "Entry ModID", "EntryModID", 120, 10, 0, true, true, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                DetailGrid_Setup.AddColto_GridCombo(ref   view, 80, 2, "tbl_MiscMaster", "Type Of Ref", "TypeOfRef", true, false, false, "MiscName", "MiscID", "", "TypeID = 5", "", null, 0.0);
                DetailGrid_Setup.AddColto_Grid(ref  view, 3, "Entry Type", "EntryType", 100, 10, 0, true, false, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 4, "Ref. Doc.", "RefDoc", 100, 10, 0, true, true, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 5, "Bill No.", "BillNo", 80, 10, 0, false, false, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 6, "Bill Date", "BillDate", 80, 10, 0, false, false, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 7, "Bill Amount", "BillAmount", 80, 10, 2, false, false, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleRight, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 8, "Balance Amount", "BalAmt", 80, 10, 2, true, false, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleRight, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 9, "Adjusted Amount", "AdjAmt", 100, 10, 2, true, false, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleRight, "");
                DetailGrid_Setup.AddChekboxto_Grid(ref  view, 10, "Sel", "Sel", 30, 10, 0, false, false, false, Enum_Define.DataType.pBoolean, DataGridViewContentAlignment.MiddleCenter);
                DetailGrid_Setup.AddColto_Grid(ref  view, 11, "Temp Balance Amount", "TempBalAmt", 120, 10, 2, true, true, false, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleRight, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 12, "Adjust Amount", "AdjAmt", 140, 10, 2, true, false, false, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleRight, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 13, "RowID", "RowID", 120, 10, 0, true, true, false, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleRight, "");
            }
            else
            {
                DetailGrid_Setup.AddColto_GridCombo(ref   view, 30, 0, "tbl_MiscMaster", "Dr/Cr", "DrCrID", false, false, false, "MiscName", "MiscID", "", "TypeID = 1", "", null, 0.0);
                DetailGrid_Setup.AddColto_Grid(ref  view, 1, "Entry ModID", "EntryModID", 120, 10, 0, true, true, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                DetailGrid_Setup.AddColto_GridCombo(ref   view, 80, 2, "tbl_MiscMaster", "Type Of Ref", "TypeOfRef", true, false, false, "MiscName", "MiscID", "", "TypeID = 5", "", null, 0.0);
                DetailGrid_Setup.AddColto_Grid(ref  view, 3, "Entry Type", "EntryType", 100, 10, 0, true, false, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 4, "Ref. Doc.", "RefDoc", 100, 10, 0, true, true, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 5, "Bill No.", "BillNo", 80, 10, 0, true, false, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 6, "Bill Date", "BillDate", 80, 10, 0, true, false, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 7, "Bill Amount", "BillAmount", 80, 10, 2, true, false, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleRight, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 8, "Balance Amount", "BalAmt", 80, 10, 2, true, false, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleRight, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 9, "Adjusted Amount", "AdjAmt", 100, 10, 2, true, true, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleRight, "");
                DetailGrid_Setup.AddChekboxto_Grid(ref  view, 10, "Sel", "Sel", 30, 10, 0, false, true, false, Enum_Define.DataType.pBoolean, DataGridViewContentAlignment.MiddleCenter);
                DetailGrid_Setup.AddColto_Grid(ref  view, 11, "Temp Balance Amount", "TempBalAmt", 120, 10, 2, true, true, false, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleRight, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 12, "Adjust Amount", "AdjAmt", 140, 10, 2, true, false, false, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleRight, "");
                DetailGrid_Setup.AddColto_Grid(ref  view, 13, "RowID", "RowID", 120, 10, 0, true, true, false, Enum_Define.DataType.pDecimal, DataGridViewContentAlignment.MiddleRight, "");
            }
            int iColNo = 14;
            using (IDataReader iDr = DB.GetRS("SELECT FormID, A.LedgerID, B.LedgerName, OrderNo, DrCrID  from  tbl_AdjustmentLedgers AS A LEFT JOIN tbl_LedgerMaster AS B ON A.LedgerID = B.LedgerID WHERE A.IsDeleted=0 and FormID=" + RefModID + " Order BY ORDERNO"))
            {
                while (iDr.Read())
                {
                    DetailGrid_Setup.AddColto_Grid(ref  view, iColNo, iDr["LedgerName"].ToString(), iDr["LedgerName"].ToString(), 80, 10, 2, true, false, false, Enum_Define.DataType.pString, DataGridViewContentAlignment.MiddleLeft, "");
                    iColNo++;
                }
            }

            txtUnAdjustedAnt.Text = string.Format("{0:N2}", dblAmount);
            txtAdjustedAmt.Text = string.Format("{0:N2}", 0);

            using (IDataReader iDr = DB.GetRS("SELECT * from fn_MiscMaster_tbl() WHERE MiscType='Adj. Type'"))
            {
                while (iDr.Read())
                {
                    if (iDr["MiscName"].ToString() == "Advance")
                        AdvanceID = Localization.ParseNativeInt(iDr["MiscID"].ToString());
                    if (iDr["MiscName"].ToString() == "Agst. Ref.")
                        AgainstRefID = Localization.ParseNativeInt(iDr["MiscID"].ToString());
                    if (iDr["MiscName"].ToString() == "New Ref.")
                        NewRefID = Localization.ParseNativeInt(iDr["MiscID"].ToString());
                    if (iDr["MiscName"].ToString() == "On Account")
                        OnAccountID = Localization.ParseNativeInt(iDr["MiscID"].ToString());
                }
            }

            using (IDataReader iDr = DB.GetRS("SELECT * from fn_MiscMaster_tbl() WHERE MiscType='DrCr'"))
            {
                while (iDr.Read())
                {
                    if (iDr["MiscName"].ToString() == "Dr")
                        DrID = Localization.ParseNativeInt(iDr["MiscID"].ToString());
                    if (iDr["MiscName"].ToString() == "Cr")
                        CrID = Localization.ParseNativeInt(iDr["MiscID"].ToString());
                }
            }

            Ref_FormName = DB.GetSnglValue("SELECT Menu_Caption FROM tbl_MenuMaster WHERE MenuID = " + RefModID);
            fgDtls.RowCount = 1;
            Show_Adjustments();
            this.eAdjType = AdjType.Manual;

            for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
            {
                fgDtls.Rows[i].Cells[2].ReadOnly = false;
                if (this.RefDrCr.ToString() == "Debit")
                {
                    fgDtls.Rows[i].Cells[0].Value = DrID;
                }
                else
                {
                    fgDtls.Rows[i].Cells[0].Value = CrID;
                }

                for (int j = 0; j <= fgDtls.Columns.Count - 1; j++)
                {
                    if (j < 9)
                        fgDtls.Rows[i].Cells[j].ReadOnly = true;
                }
            }

            fgDtls.Rows.Add();
            fgDtls.CellValueChanged += new DataGridViewCellEventHandler(fgDtls_CellValueChanged);
            fgDtls.CellEndEdit += new DataGridViewCellEventHandler(fgDtls_CellEndEdit);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
            Dispose();
        }

        private void btnAutoAdj_Click(object sender, EventArgs e)
        {
            try
            {
                eAdjType = AdjType.Auto;
                txtAdjustedAmt.Text = string.Format("{0:N2}", 0);
                txtUnAdjustedAnt.Text = string.Format("{0:N2}", (Localization.ParseNativeDouble(this.txtUnAdjustedAnt.Text) + this.gf_CalculateGridTotal(9)) + this.dblTempAdjAmt);
                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    if ((fgDtls.Rows[i].Cells[8].Value != null) && (fgDtls.Rows[i].Cells[9].Value != null))
                    {
                        ((DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[10]).Selected = false;
                        ((DataGridViewCheckBoxCell)fgDtls.Rows[i].Cells[10]).Value = 0;

                        if (Localization.ParseNativeInt(fgDtls.Rows[i].Cells[2].Value.ToString()) == AgainstRefID)
                        {
                            fgDtls.Rows[i].Cells[8].Value = Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[8].Value.ToString()) + Localization.ParseNativeDecimal(fgDtls.Rows[i].Cells[9].Value.ToString());
                        }
                        fgDtls.Rows[i].Cells[9].Value = string.Format("{0:N2}", 0);
                    }
                }
                for (int j = 0; j <= fgDtls.RowCount - 1; j++)
                {
                    if ((fgDtls.Rows[j].Cells[8].Value != null) && (fgDtls.Rows[j].Cells[9].Value != null))
                    {
                        ((DataGridViewCheckBoxCell)fgDtls.Rows[j].Cells[10]).Selected = true;
                        ((DataGridViewCheckBoxCell)fgDtls.Rows[j].Cells[10]).Value = 1;
                        AdjustAmt(j, AdjType.Auto);
                        if ((Localization.ParseNativeDouble(txtUnAdjustedAnt.Text) + dblTempAdjAmt) <= 0.0)
                        {
                            break;
                        }
                    }
                }
                eAdjType = AdjType.Manual;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            try
            {
                if (Localization.ParseNativeDouble(txtUnAdjustedAnt.Text) < 0.0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Adjustment Amount should be equal to given Amount");
                }
                else
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
                        if (ex.Rows[Ref_intRowNum].Cells[18].Value != null)
                        {
                            if (Localization.ParseNativeInt(ex.Rows[Ref_intRowNum].Cells[18].Value.ToString()) == Ref_intRowNum)
                            {
                                if (ex.Rows[Ref_intRowNum + 1].Cells[14].Value == null || ex.Rows[Ref_intRowNum + 1].Cells[14].Value.ToString() == "" || ex.Rows[Ref_intRowNum + 1].Cells[14].Value.ToString() == "0")
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

                    ex.Rows[Ref_intRowNum].Cells[14].Value = "1";
                    //ex.Rows[Ref_intRowNum].Cells[13].Value = "1"; confirm Please

                    DataTable Dt = DB.GetDT("SELECT OrderNo, DrCrID,LedgerID  from  tbl_AdjustmentLedgers WHERE IsDeleted=0 and FormID=" + RefModID + "", false);
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
                                        row2.Cells[17].Value = OnAccountID;
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
                                    row2.Cells[9].Value = this.fgDtls.Rows[i].Cells[4].Value;
                                else
                                    row2.Cells[9].Value = Ref_TxnCode;

                                row2.Cells[8].Value = "";
                                row2.Cells[10].Value = this.fgDtls.Rows[i].Cells[3].Value;

                                if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == OnAccountID)
                                    row2.Cells[11].Value = RefModID;
                                else
                                    row2.Cells[11].Value = (this.fgDtls.Rows[i].Cells[1].Value != null ? this.fgDtls.Rows[i].Cells[1].Value : 0);

                                if ((Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == AgainstRefID) || (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == NewRefID))
                                    row2.Cells[12].Value = this.fgDtls.Rows[i].Cells[5].Value;
                                else if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == OnAccountID)
                                    row2.Cells[12].Value = this.fgDtls.Rows[i].Cells[2].FormattedValue;
                                else if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == AdvanceID)
                                    row2.Cells[12].Value = Ref_TxnCode;

                                row2.Cells[13].Value = this.fgDtls.Rows[i].Cells[6].Value;
                                row2.Cells[14].Value = "";
                                row2.Cells[15].Value = "";
                                if (this.fgDtls.Rows[i].Cells[5].Value.ToString() == "On Account")
                                    row2.Cells[17].Value = OnAccountID;
                                else
                                    row2.Cells[17].Value = this.fgDtls.Rows[i].Cells[2].Value;

                                row2.Cells[18].Value = Ref_intRowNum;

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
                                            DGMiscLgrs.Cells[8].Value = "";
                                            //DGMiscLgrs.Cells[11].Value = CommonCls.ShowFormCaptionByID(this.RefModID);// this.fgDtls.Rows[i].Cells[3].Value;
                                            DGMiscLgrs.Cells[9].Value = this.fgDtls.Rows[i].Cells[4].Value;
                                            //DGMiscLgrs.Cells[12].Value = RefModID;// (this.fgDtls.Rows[i].Cells[1].Value != null ? this.fgDtls.Rows[i].Cells[1].Value : 0);
                                            //DGMiscLgrs.Cells[13].Value = Ref_TxnCode;// this.fgDtls.Rows[i].Cells[5].Value;

                                            DGMiscLgrs.Cells[10].Value = CommonCls.ShowFormCaptionByID(this.RefModID);
                                            DGMiscLgrs.Cells[11].Value = this.RefModID;
                                            DGMiscLgrs.Cells[12].Value = this.fgDtls.Rows[i].Cells[5].Value;
                                            DGMiscLgrs.Cells[13].Value = Ref_Date;
                                            DGMiscLgrs.Cells[14].Value = "";
                                            DGMiscLgrs.Cells[15].Value = "";
                                            DGMiscLgrs.Cells[16].Value = "0";
                                            DGMiscLgrs.Cells[17].Value = this.fgDtls.Rows[i].Cells[2].Value; /* plz confirm */
                                            DGMiscLgrs.Cells[18].Value = Ref_intRowNum;
                                            intAddRow++;
                                        }
                                    }
                                }
                                row2 = null;
                            }
                        }
                        i++;
                    }
                    if (Localization.ParseNativeDouble(txtUnAdjustedAnt.Text) > 0.0)
                    {

                        ex.Rows.Insert(intAddRow, new DataGridViewRow());
                        DataGridViewRow row3 = ex.Rows[intAddRow];
                        
                        if (this.RefDrCr.ToString() == "Debit")
                        {
                            row3.Cells[2].Value = DrID;
                        }
                        else
                        {
                            row3.Cells[2].Value = CrID;
                        }

                        row3.Cells[3].Value = "ON ACCOUNT";
                        row3.Cells[6].Value = string.Format("{0:N2}", Localization.ParseNativeDouble(txtUnAdjustedAnt.Text));
                        row3.Cells[9].Value = Ref_TxnCode;
                        row3.Cells[12].Value = "ON ACCOUNT";
                        row3.Cells[13].Value = Ref_Date;
                        row3.Cells[10].Value = CommonCls.ShowFormCaptionByID(this.RefModID);
                        row3.Cells[11].Value = this.RefModID;
                        row3.Cells[1].Value = i + 1;
                        row3.Cells[15].Value = "";
                        row3.Cells[14].Value = "";
                        row3.Cells[5].Value = Resources.Img_OnAccount;
                        row3.Cells[16].Value = string.Empty;
                        row3.Cells[17].Value = OnAccountID;
                        row3.Cells[18].Value = Ref_intRowNum;
                        for (i = 0; i <= Ref_fgDtls.ColumnCount - 1; i++)
                        {
                            row3.Cells[i].Style.Font = new Font("Verdana", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
                            row3.Cells[i].Style.BackColor = this.lblOnAc_Clr.BackColor;//Color.FromArgb(170, 225, 225);
                            row3.Cells[i].ReadOnly = true;
                        }
                        row3 = null;
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


                    //CIS_Textil.frmAdjustment_Broker adjust = new CIS_Textil.frmAdjustment_Broker();
                    //adjust.RefModID = RefModID;
                    //adjust.Ref_fgDtls = fgDtls;
                    //adjust.Ref_TxnCode = Ref_TxnCode;
                    ////adjust.Ref_Dt_tmp_Broker = Ref_Dt_tmp;
                    //adjust.dblAmount = dblAmount;
                    //adjust.RefDrCr = RefDrCr;
                    //adjust.Ref_intRowNum = Ref_intRowNum;
                    //adjust.Ref_Date = Ref_Date;
                    //adjust.TransId = TransId;
                    //adjust.pLedgerID = pLedgerID;
                    //adjust.ShowDialog();

                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
        //private void btnDone_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Localization.ParseNativeDouble(txtUnAdjustedAnt.Text) < 0.0)
        //        {
        //            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Adjustment Amount should be equal to given Amount");
        //        }
        //        else
        //        {
        //            int i;
        //            DataGridViewEx ex = Ref_fgDtls;
        //            DataGridViewRow row = ex.Rows[Ref_intRowNum];
        //            row.DefaultCellStyle.BackColor = Color.FromArgb(0, 165, 165);
        //            row.DefaultCellStyle.Font = new Font("Verdana", 8f, FontStyle.Bold, GraphicsUnit.Point, 0);
        //            row.Cells[5].Value = Resources.AdjAmt;

        //            int intAddRow;
        //            int iRowCnts = ex.RowCount - 1;
        //            for (i = Ref_intRowNum + 1; i <= iRowCnts; i++)
        //            {
        //                if (ex.Rows[Ref_intRowNum].Cells[17].Value != null)
        //                {
        //                    if (Localization.ParseNativeInt(ex.Rows[Ref_intRowNum].Cells[17].Value.ToString()) == Ref_intRowNum)
        //                    {
        //                        if (ex.Rows[Ref_intRowNum + 1].Cells[13].Value==null || ex.Rows[Ref_intRowNum + 1].Cells[13].Value.ToString() == "")
        //                        {
        //                            ex.Rows.RemoveAt(Ref_intRowNum + 1);
        //                        }
        //                        else
        //                        {
        //                            break;
        //                        }
        //                    }
        //                }
        //            }

        //            ex.Rows[Ref_intRowNum].Cells[13].Value = "1";

        //            DataTable Dt = DB.GetDT("SELECT OrderNo, DrCrID,LedgerID  from  tbl_AdjustmentLedgers WHERE FormID=" + RefModID + "", false);
        //            intAddRow = Ref_intRowNum + 1;
        //            int j = this.fgDtls.RowCount - 1;
        //            i = 0;
        //            while (i <= j)
        //            {
        //                if (this.fgDtls.Rows[i].Cells[9].Value != null)
        //                {
        //                    if (Localization.ParseNativeDouble(this.fgDtls.Rows[i].Cells[9].Value.ToString()) > 0.0)
        //                    {
        //                        ex.Rows.Insert(intAddRow, new DataGridViewRow());
        //                        DataGridViewRow row2 = ex.Rows[intAddRow];

        //                        row2.Cells[1].Value = i + 1;
        //                        row2.Cells[2].Value = Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[0].Value.ToString());

        //                        if ((Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == AgainstRefID) || (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == NewRefID))
        //                        {
        //                            if (this.fgDtls.Rows[i].Cells[5].Value.ToString() == "On Account")
        //                            {
        //                                row2.Cells[3].Value = "On Account";
        //                                row2.Cells[17].Value = OnAccountID;
        //                            }
        //                            else
        //                                row2.Cells[3].Value = this.fgDtls.Rows[i].Cells[5].Value + "-" + this.fgDtls.Rows[i].Cells[6].Value;

        //                        }
        //                        else if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == OnAccountID)
        //                            row2.Cells[3].Value = this.fgDtls.Rows[i].Cells[2].FormattedValue;
        //                        else if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == AdvanceID)
        //                            row2.Cells[3].Value = this.fgDtls.Rows[i].Cells[5].Value + "-" + this.fgDtls.Rows[i].Cells[6].Value + "(" + this.fgDtls.Rows[i].Cells[2].FormattedValue + ")";
        //                        else
        //                            row2.Cells[3].Value = 0;

        //                        row2.Cells[5].Value = Resources.Img_NoClick;
        //                        row2.Cells[6].Value = string.Format("{0:N2}", Localization.ParseNativeDouble(this.fgDtls.Rows[i].Cells[12].Value.ToString()));

        //                        if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == AgainstRefID)
        //                            row2.Cells[8].Value = this.fgDtls.Rows[i].Cells[4].Value;
        //                        else
        //                            row2.Cells[8].Value = Ref_TxnCode;

        //                        row2.Cells[9].Value = this.fgDtls.Rows[i].Cells[4].Value;

        //                        if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == OnAccountID)
        //                            row2.Cells[10].Value = RefModID;
        //                        else
        //                            row2.Cells[10].Value = (this.fgDtls.Rows[i].Cells[1].Value != null ? this.fgDtls.Rows[i].Cells[1].Value : 0);

        //                        if ((Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == AgainstRefID) || (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == NewRefID))
        //                            row2.Cells[11].Value = this.fgDtls.Rows[i].Cells[5].Value;
        //                        else if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == OnAccountID)
        //                            row2.Cells[11].Value = this.fgDtls.Rows[i].Cells[2].FormattedValue;
        //                        else if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == AdvanceID)
        //                            row2.Cells[11].Value = Ref_TxnCode;

        //                        row2.Cells[12].Value = this.fgDtls.Rows[i].Cells[6].Value;
        //                        row2.Cells[13].Value = string.Empty;
        //                        row2.Cells[14].Value = "1";
        //                        row2.Cells[15].Value = "0";
        //                        if (this.fgDtls.Rows[i].Cells[5].Value.ToString() == "On Account")
        //                            row2.Cells[16].Value = OnAccountID;
        //                        else
        //                            row2.Cells[16].Value = this.fgDtls.Rows[i].Cells[2].Value;

        //                        row2.Cells[17].Value = Ref_intRowNum;

        //                        if (this.fgDtls.Rows[i].Cells[1].Value != null)
        //                        {
        //                            if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[1].Value.ToString()) == RefModID)
        //                            {
        //                                int iBackClr = Ref_fgDtls.ColumnCount - 1;
        //                                for (int n = 0; n <= iBackClr; n++)
        //                                {
        //                                    row2.Cells[n].Style.Font = new Font("Verdana", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);

        //                                    if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == AgainstRefID)
        //                                        row2.Cells[n].Style.BackColor = Color.FromArgb(170, 225, 225);
        //                                    else if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == OnAccountID)
        //                                        row2.Cells[n].Style.BackColor = this.lblOnAc_Clr.BackColor;
        //                                    else if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == NewRefID)
        //                                        row2.Cells[n].Style.BackColor = this.lblNewRef_Clr.BackColor;
        //                                    else if (Localization.ParseNativeInt(this.fgDtls.Rows[i].Cells[2].Value.ToString()) == AdvanceID)
        //                                        row2.Cells[n].Style.BackColor = this.lblAdvance_Clr.BackColor;
        //                                }
        //                            }
        //                        }
        //                        int m = Ref_fgDtls.ColumnCount - 1;
        //                        for (int k = 0; k <= m; k++)
        //                        {
        //                            row2.Cells[k].ReadOnly = true;
        //                        }

        //                        intAddRow++;
        //                        for (int k = 14; k <= this.fgDtls.ColumnCount - 1; k++)
        //                        {
        //                            if (this.fgDtls.Rows[i].Cells[k].Value != null)
        //                            {
        //                                if (Localization.ParseNativeDouble(this.fgDtls.Rows[i].Cells[k].Value.ToString()) > 0)
        //                                {
        //                                    ex.Rows.Insert(intAddRow, new DataGridViewRow());
        //                                    DataGridViewRow DGMiscLgrs = ex.Rows[intAddRow];
        //                                    DGMiscLgrs.Cells[1].Value = i + 1;
        //                                    DataRow[] rst = Dt.Select("OrderNo=" + (k - 13));
        //                                    if (rst.Length > 0)
        //                                    {
        //                                        foreach (DataRow r in rst)
        //                                        {
        //                                            DGMiscLgrs.Cells[2].Value = Localization.ParseNativeInt(r["DrCrID"].ToString());
        //                                            DGMiscLgrs.Cells[3].Value = Localization.ParseNativeInt(r["LedgerID"].ToString());
        //                                            break;
        //                                        }
        //                                    }

        //                                    DGMiscLgrs.Cells[5].Value = Resources.Img_NoClick;
        //                                    DGMiscLgrs.Cells[6].Value = string.Format("{0:N2}", Localization.ParseNativeDouble(this.fgDtls.Rows[i].Cells[k].Value.ToString()));
        //                                    //DGMiscLgrs.Cells[10].Value = Ref_TxnCode;// this.fgDtls.Rows[i].Cells[4].Value;
        //                                    DGMiscLgrs.Cells[8].Value = this.fgDtls.Rows[i].Cells[4].Value;
        //                                    //DGMiscLgrs.Cells[11].Value = CommonCls.ShowFormCaptionByID(this.RefModID);// this.fgDtls.Rows[i].Cells[3].Value;
        //                                    DGMiscLgrs.Cells[9].Value = this.fgDtls.Rows[i].Cells[3].Value;
        //                                    //DGMiscLgrs.Cells[12].Value = RefModID;// (this.fgDtls.Rows[i].Cells[1].Value != null ? this.fgDtls.Rows[i].Cells[1].Value : 0);
        //                                    //DGMiscLgrs.Cells[13].Value = Ref_TxnCode;// this.fgDtls.Rows[i].Cells[5].Value;

        //                                    DGMiscLgrs.Cells[10].Value = (this.fgDtls.Rows[i].Cells[1].Value != null ? this.fgDtls.Rows[i].Cells[1].Value : 0);
        //                                    DGMiscLgrs.Cells[11].Value = this.fgDtls.Rows[i].Cells[5].Value;

        //                                    DGMiscLgrs.Cells[12].Value = Ref_Date;
        //                                    DGMiscLgrs.Cells[12].Value = string.Empty;
        //                                    DGMiscLgrs.Cells[14].Value = "1";
        //                                    DGMiscLgrs.Cells[16].Value = this.fgDtls.Rows[i].Cells[2].Value; /* plz confirm */
        //                                    DGMiscLgrs.Cells[17].Value = Ref_intRowNum;
        //                                    intAddRow++;
        //                                }
        //                            }
        //                        }
        //                        row2 = null;
        //                    }
        //                }
        //                i++;
        //            }
        //            if (Localization.ParseNativeDouble(txtUnAdjustedAnt.Text) > 0.0)
        //            {

        //                ex.Rows.Insert(intAddRow, new DataGridViewRow());
        //                DataGridViewRow row3 = ex.Rows[intAddRow];
        //                row3.Cells[13].Value = "";
        //                row3.Cells[0].Value = DrID;
        //                row3.Cells[3].Value = "ON ACCOUNT";
        //                row3.Cells[6].Value = string.Format("{0:N2}", Localization.ParseNativeDouble(txtUnAdjustedAnt.Text));
        //                row3.Cells[8].Value = Ref_TxnCode;
        //                row3.Cells[11].Value = "ON ACCOUNT";
        //                row3.Cells[12].Value = Ref_Date;
        //                row3.Cells[9].Value = this.RefModID;
        //                row3.Cells[10].Value = RefModID;
        //                row3.Cells[1].Value = i + 1;
        //                row3.Cells[5].Value = Resources.Img_OnAccount;
        //                row3.Cells[13].Value = string.Empty;
        //                row3.Cells[15].Value = "0";
        //                row3.Cells[16].Value = OnAccountID;
        //                row3.Cells[17].Value = Ref_intRowNum;
        //                for (i = 0; i <= Ref_fgDtls.ColumnCount - 1; i++)
        //                {
        //                    row3.Cells[i].Style.Font = new Font("Verdana", 8f, FontStyle.Regular, GraphicsUnit.Point, 0);
        //                    row3.Cells[i].Style.BackColor = this.lblOnAc_Clr.BackColor;//Color.FromArgb(170, 225, 225);
        //                    row3.Cells[i].ReadOnly = true;
        //                }
        //                row3 = null;
        //            }

        //            if (ex.Rows[ex.RowCount - 1].Cells[3].Value == null)
        //            {
        //                row = ex.Rows[ex.RowCount - 1];
        //                row.DefaultCellStyle.BackColor = Color.FromArgb(0, 165, 165);
        //                row.DefaultCellStyle.Font = new Font("Verdana", 8f, FontStyle.Bold, GraphicsUnit.Point, 0);
        //                row.Cells[5].Value = Resources.AdjAmt;
        //                ex.CurrentCell = ex[3, ex.Rows.Count - 1];
        //            }

        //            #region Save to tmpDatatable
        //            for (int r = 0; r <= this.fgDtls.Rows.Count - 1; r++)
        //            {
        //                if (Ref_Dt_tmp.Rows.Count <= r)
        //                    this.Ref_Dt_tmp.Rows.Add();
        //                // if (this.fgDtls.Rows[r].Cells[0].Value != null)
        //                {
        //                    for (int c = 0; c <= this.fgDtls.Columns.Count - 1; c++)
        //                    {
        //                        //if (Localization.ParseNativeDouble(this.fgDtls.Rows[r].Cells[9].Value.ToString()) > 0)
        //                        {
        //                            if (c == 13)
        //                                this.Ref_Dt_tmp.Rows[r][13] = Ref_intRowNum;
        //                            else
        //                                if (this.fgDtls.Rows[r].Cells[c].Value != null)
        //                                    this.Ref_Dt_tmp.Rows[r][c] = this.fgDtls.Rows[r].Cells[c].Value;
        //                                else
        //                                    this.Ref_Dt_tmp.Rows[r][c] = 0;

        //                        }
        //                    }
        //                }
        //            }
        //            #endregion

        //            DataGridViewEx fgDtls = Ref_fgDtls;
        //            Ref_fgDtls.Rows.Add();
        //            CommonCls.SetGridNum(fgDtls);
        //            Ref_fgDtls = fgDtls;
        //            ex = null;
        //            Close();
        //            Dispose();


        //            CIS_Textil.frmAdjustment_Broker adjust = new CIS_Textil.frmAdjustment_Broker();
        //            adjust.RefModID = RefModID;
        //            adjust.Ref_TxnCode = Ref_TxnCode;
        //            adjust.Ref_fgDtls = Ref_fgDtls;
        //           // adjust.Ref_Dt_tmp_Broker = Ref_Dt_tmp;
        //            adjust.dblAmount = dblAmount;
        //            adjust.RefDrCr = RefDrCr;
        //            adjust.Ref_intRowNum = Ref_intRowNum;
        //            adjust.Ref_Date = Ref_Date;
        //            adjust.TransId = TransId;
        //            adjust.pLedgerID = pLedgerID;                    
        //            adjust.ShowDialog();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Navigate.logError(ex.Message, ex.StackTrace);
        //    }
        //}

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

        private void Show_Adjustments()
        {
            try
            {
                string SqlStr = "";
                if (this.FormAction == Enum_Define.ActionType.New_Record)
                {
                    DataRow[] rst = Ref_Dt_tmp.Select("RowID=" + Ref_intRowNum + "and DrCr>0");
                    if (rst.Length > 0)
                    {
                        int iRows = 0;
                        foreach (DataRow r in rst)
                        {
                            fgDtls.Rows.Add();
                            for (int c = 0; c <= Ref_Dt_tmp.Columns.Count - 1; c++)
                            {
                                if (Localization.ParseNativeInt(Ref_Dt_tmp.Rows[iRows][1].ToString()) > 0)
                                {
                                    if (c == 0 || c == 2)
                                        this.fgDtls.Rows[iRows].Cells[c].Value = Localization.ParseNativeInt(Ref_Dt_tmp.Rows[iRows][c].ToString());
                                    else
                                        this.fgDtls.Rows[iRows].Cells[c].Value = Ref_Dt_tmp.Rows[iRows][c].ToString();
                                }
                            }
                            iRows++;
                        }
                    }
                    else
                    {
                        if (this.RefDrCr.ToString() == "Debit")
                        {
                            SqlStr = string.Format("{0} {1}, {2}, {3},{4},{5}", new object[] { "sp_BillAdjustment_New", Db_Detials.CompID, Db_Detials.YearID, this.pLedgerID, CommonLogic.SQuote(Localization.ToSqlDateString(Ref_Date)), 1 });
                        }
                        else
                        {
                            SqlStr = string.Format("{0} {1}, {2}, {3},{4},{5}", new object[] { "sp_BillAdjustment_New", Db_Detials.CompID, Db_Detials.YearID, this.pLedgerID, CommonLogic.SQuote(Localization.ToSqlDateString(Ref_Date)), 0 });
                        }
                    }
                }
                else if ((this.FormAction == Enum_Define.ActionType.View_Record) | (this.FormAction == Enum_Define.ActionType.Edit_Record))
                {
                    DataRow[] rst = Ref_Dt_tmp.Select("RowID=" + Ref_intRowNum + "and DrCr>0");
                    if (rst.Length > 0)
                    {
                        int iRows = 0;
                        foreach (DataRow r in rst)
                        {
                            fgDtls.Rows.Add();
                            for (int c = 0; c <= Ref_Dt_tmp.Columns.Count - 1; c++)
                            {
                                if (Localization.ParseNativeInt(Ref_Dt_tmp.Rows[iRows][1].ToString()) > 0)
                                {
                                    if (c == 0 || c == 2)
                                        this.fgDtls.Rows[iRows].Cells[c].Value = Localization.ParseNativeInt(Ref_Dt_tmp.Rows[iRows][c].ToString());
                                    else
                                        this.fgDtls.Rows[iRows].Cells[c].Value = Ref_Dt_tmp.Rows[iRows][c].ToString();
                                }
                            }
                            iRows++;
                        }
                    }
                    else
                    {
                        if (this.RefDrCr.ToString() == "Debit")
                        {
                            SqlStr = string.Format("{0} {1}, {2}, {3}, {4},{5},{6},{7}", new object[] { "sp_BillAdjustment_Upd", Db_Detials.CompID, Db_Detials.YearID, this.pLedgerID, this.TransId, this.RefModID, CommonLogic.SQuote(Localization.ToSqlDateString(Ref_Date)), 1 });
                        }
                        else
                        {
                            SqlStr = string.Format("{0} {1}, {2}, {3}, {4},{5},{6},{7}", new object[] { "sp_BillAdjustment_Upd", Db_Detials.CompID, Db_Detials.YearID, this.pLedgerID, this.TransId, this.RefModID, CommonLogic.SQuote(Localization.ToSqlDateString(Ref_Date)), 0 });
                        }
                    }
                }

                using (DataTable table = DB.GetDT(SqlStr, false))
                {
                    DataRow[] rowArray = table.Select("");
                    if (rowArray.Length > 0)
                    {
                        DataGridView view = this.fgDtls;
                        view.RowCount = 0;
                        view.Rows.Add(rowArray.Length);
                        for (double k = 0.0; k <= rowArray.Length - 1; k++)
                        {
                            DataGridViewRow row2 = view.Rows[(int)Math.Round(k)];
                            int iDrCr = Localization.ParseNativeInt(rowArray[(int)Math.Round(k)]["DrCrID"].ToString());
                            row2.Cells[0].Value = Localization.ParseNativeInt(rowArray[(int)Math.Round(k)]["DrCrID"].ToString());
                            row2.Cells[0].Style.BackColor = (iDrCr == Localization.ParseNativeInt(this.RefDrCr.ToString()) ? Color.FromArgb(255, 255, 200) : this.lblOnAc_Clr.BackColor);

                            row2.Cells[1].Value = rowArray[(int)Math.Round(k)]["RefTransType"].ToString();
                            row2.Cells[1].Style.BackColor = (iDrCr == Localization.ParseNativeInt(this.RefDrCr.ToString()) ? Color.FromArgb(255, 255, 200) : this.lblOnAc_Clr.BackColor);

                            row2.Cells[2].Value = AgainstRefID;
                            row2.Cells[2].Style.BackColor = (iDrCr == Localization.ParseNativeInt(this.RefDrCr.ToString()) ? Color.FromArgb(255, 255, 200) : this.lblOnAc_Clr.BackColor);

                            row2.Cells[3].Value = CommonCls.ShowFormCaptionByID(Conversions.ToInteger(rowArray[(int)Math.Round(k)]["RefTransType"].ToString()));
                            row2.Cells[3].Style.BackColor = (iDrCr == Localization.ParseNativeInt(this.RefDrCr.ToString()) ? Color.FromArgb(255, 255, 200) : this.lblOnAc_Clr.BackColor);
                            row2.Cells[4].Value = rowArray[(int)Math.Round(k)]["RefID"].ToString();
                            row2.Cells[4].Style.BackColor = (iDrCr == Localization.ParseNativeInt(this.RefDrCr.ToString()) ? Color.FromArgb(255, 255, 200) : this.lblOnAc_Clr.BackColor);
                            row2.Cells[5].Value = rowArray[(int)Math.Round(k)]["RefNo"].ToString();
                            row2.Cells[5].Style.BackColor = (iDrCr == Localization.ParseNativeInt(this.RefDrCr.ToString()) ? Color.FromArgb(255, 255, 200) : this.lblOnAc_Clr.BackColor);
                            row2.Cells[6].Value = Localization.ToVBDateString(rowArray[(int)Math.Round(k)]["RefDate"].ToString());
                            row2.Cells[6].Style.BackColor = (iDrCr == Localization.ParseNativeInt(this.RefDrCr.ToString()) ? Color.FromArgb(255, 255, 200) : this.lblOnAc_Clr.BackColor);
                            row2.Cells[7].Value = string.Format("{0:N2}", RuntimeHelpers.GetObjectValue(Interaction.IIf(rowArray[(int)Math.Round(k)]["Bill_Amt"].ToString() == "", Math.Abs(Localization.ParseNativeDouble(rowArray[(int)Math.Round(k)]["Bal_Amt"].ToString())), rowArray[(int)Math.Round(k)]["Bill_Amt"].ToString())));
                            row2.Cells[7].Style.BackColor = (iDrCr == Localization.ParseNativeInt(this.RefDrCr.ToString()) ? Color.FromArgb(255, 255, 200) : this.lblOnAc_Clr.BackColor);
                            //if (this.RefDrCr == Db_Detials.Ac_DrCr.Credit)
                            {
                                row2.Cells[8].Value = string.Format("{0:N2}", Math.Abs(Localization.ParseNativeDouble(rowArray[(int)Math.Round(k)]["Bal_Amt"].ToString())));
                                row2.Cells[11].Value = string.Format("{0:N2}", Math.Abs(Localization.ParseNativeDouble(rowArray[(int)Math.Round(k)]["Bal_Amt"].ToString())));
                            }
                            row2.Cells[12].Value = "0";
                            row2.Cells[8].Style.BackColor = Color.FromArgb(221, 244, 255);
                            row2.Cells[9].Value = string.Format("{0:N2}", 0);
                            row2.Cells[9].Style.BackColor = Color.FromArgb(255, 207, 183);
                            row2 = null;

                        }
                        view = null;
                    }
                }

                /* */

                for (double i = 0.0; i <= fgDtls.RowCount - 1; i++)
                {
                    DataGridViewRow row3 = fgDtls.Rows[(int)Math.Round(i)];
                    for (int m = 0; m <= Ref_fgDtls.RowCount - 1; m++)
                    {
                        txtOnAccountAmt.Text = DB.GetSnglValue(string.Format("Select SUM(Dr_Amt)-SUM(Cr_Amt) From tbl_AcLedger  Where AdjType=91 and LedgerID=" + this.Ref_fgDtls.Rows[m].Cells[3].Value + ""));
                        if (Ref_fgDtls.Rows[m].Cells[18].Value != null)
                        {
                            if (Localization.ParseNativeInt(Ref_fgDtls.Rows[m].Cells[18].Value.ToString()) == Ref_intRowNum)
                            {
                                if (this.Ref_fgDtls.Rows[m].Cells[14].Value == null)
                                {
                                    this.Ref_fgDtls.Rows[m].Cells[14].Value = "";
                                }

                                if (Operators.ConditionalCompareObjectEqual(this.Ref_fgDtls.Rows[m].Cells[14].Value.ToString(), "", false))
                                {
                                    if (Conversions.ToBoolean(Operators.AndObject(Operators.CompareObjectEqual(row3.Cells[1].Value, this.Ref_fgDtls.Rows[m].Cells[11].Value, false), Operators.CompareObjectEqual(row3.Cells[4].Value, this.Ref_fgDtls.Rows[m].Cells[9].Value, false))))
                                    {
                                        row3.Cells[9].Value = (Localization.ParseNativeDouble(string.Format("{0:N2}", Localization.ParseNativeDouble(Ref_fgDtls.Rows[m].Cells[6].Value.ToString()))));
                                        row3.Cells[12].Value = (Localization.ParseNativeDouble(string.Format("{0:N2}", Localization.ParseNativeDouble(Ref_fgDtls.Rows[m].Cells[6].Value.ToString()))));
                                        row3.Cells[11].Value = string.Format("{0:N2}", Localization.ParseNativeDouble(row3.Cells[11].Value.ToString()) + Localization.ParseNativeDouble(Ref_fgDtls.Rows[m].Cells[6].Value.ToString()));

                                        break;
                                    }
                                }
                            }
                        }
                    }
                    row3 = null;
                }

                if (this.FormAction == Enum_Define.ActionType.Edit_Record)
                {
                    DataTable Dt = DB.GetDT("SELECT Dr_Amt, Cr_Amt,RefNo, LedgerID, AdjType, DrCrID FROM fn_AcLedger() as A WHERE A.CompID = " + Db_Detials.CompID + " and A.YearID = " + Db_Detials.YearID + "  and A.TransID = " + this.TransId + " and TransType = " + this.RefModID + "", false);
                    for (double i = 0.0; i <= fgDtls.RowCount - 1; i++)
                    {
                        DataGridViewRow row3 = fgDtls.Rows[(int)Math.Round(i)];
                        for (int m = 14; m <= fgDtls.ColumnCount - 1; m++)
                        {
                            if (row3.Cells[0].Value != null)
                            {
                                if (row3.Cells[5].Value.ToString() == "ON ACCOUNT")
                                {
                                    row3.Cells[m].Value = "0";
                                }
                                else
                                {
                                    DataRow[] rst = Dt.Select("LedgerID=" + getLedgerIDbyCol(m) + " and RefNo='" + row3.Cells[5].Value + "'");
                                    if (rst.Length > 0)
                                    {
                                        foreach (DataRow r in rst)
                                        {
                                            if (this.RefDrCr.ToString() == "Debit")
                                            {
                                                if (getDrCrIDbyCol(m) == DrID)
                                                    row3.Cells[m].Value = r["Dr_Amt"].ToString();
                                                else
                                                    row3.Cells[m].Value = r["Cr_Amt"].ToString();
                                            }
                                            else
                                            {
                                                if (getDrCrIDbyCol(m) == DrID)
                                                    row3.Cells[m].Value = r["Cr_Amt"].ToString();
                                                else
                                                    row3.Cells[m].Value = r["Dr_Amt"].ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (row3.Cells[12].Value != null && row3.Cells[12].Value.ToString() != "0" && row3.Cells[12].Value.ToString() != "0.00")
                        {
                            row3.Cells[9].Value = (Localization.ParseNativeDouble(row3.Cells[12].Value.ToString()) + gf_CalcExtraLedgers());
                        }
                    }
                }

                double dblextLdrs = gf_CalcExtraLedgers();
                txtAdjustedAmt.Text = string.Format("{0:N2}", gf_CalculateGridTotal(9));
                txtAdjustedAmt.Text = string.Format("{0:N2}", dblTempAdjAmt + dblTempAdjAmt_Cr);
                txtUnAdjustedAnt.Text = string.Format("{0:N2}", dblAmount - Localization.ParseNativeDouble(txtAdjustedAmt.Text));
                DataGridView view3 = this.fgDtls;
                if (view3.Rows.Count >= 1)
                {
                    view3.CurrentCell = view3[8, 0];
                }
                view3 = null;
                this.eAdjType = AdjType.Manual;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }

        }

        private void AdjustAmt(int intI, AdjType eActionType)
        {
            try
            {
                fgDtls.CellValueChanged -= new DataGridViewCellEventHandler(fgDtls_CellValueChanged);
                DataGridViewRow row = this.fgDtls.Rows[intI];
                DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)row.Cells[10];
                this.gf_CalculateGridTotal(9);
                double dblUnAdj = Localization.ParseNativeDouble(txtUnAdjustedAnt.Text);
                double dblAdjAmt = Localization.ParseNativeDouble(row.Cells[9].Value.ToString());
                double RowBalAmt = Localization.ParseNativeDouble(row.Cells[8].Value.ToString());
                double dblCalcAmt = dblUnAdj - RowBalAmt;
                if (eActionType == AdjType.Auto)
                {
                    if (dblAdjAmt > 0.0)
                    {
                        if (Localization.ParseNativeInt(row.Cells[2].Value.ToString()) == AgainstRefID)
                        {
                            row.Cells[8].Value = string.Format("{0:N2}", Localization.ParseNativeDouble(row.Cells[8].Value.ToString()) + Localization.ParseNativeDouble(row.Cells[9].Value.ToString()));
                        }
                        row.Cells[9].Value = string.Format("{0:N2}", 0);
                    }
                    else if (dblAdjAmt < dblCalcAmt)
                    {
                        row.Cells[12].Value = string.Format("{0:N2}", RowBalAmt);
                        row.Cells[9].Value = string.Format("{0:N2}", RowBalAmt);
                        if (Localization.ParseNativeInt(row.Cells[2].Value.ToString()) == AgainstRefID)
                        {
                            row.Cells[8].Value = string.Format("{0:N2}", Localization.ParseNativeDouble(row.Cells[8].Value.ToString()) - RowBalAmt);
                        }
                    }
                    else if (dblAdjAmt >= dblCalcAmt)
                    {
                        row.Cells[12].Value = string.Format("{0:N2}", dblUnAdj);
                        row.Cells[9].Value = string.Format("{0:N2}", dblUnAdj);
                        if (Localization.ParseNativeInt(row.Cells[2].Value.ToString()) == AgainstRefID)
                        {
                            row.Cells[8].Value = string.Format("{0:N2}", Localization.ParseNativeDouble(row.Cells[8].Value.ToString()) - dblUnAdj);
                        }
                    }
                }
                else
                {
                    if (Localization.ParseNativeInt(row.Cells[2].Value.ToString()) == AgainstRefID)
                    {
                        row.Cells[8].Value = string.Format("{0:N2}", Localization.ParseNativeDouble(row.Cells[8].Value.ToString()) - dblAdjAmt);
                    }
                }
                dblCalcAmt = 0.0;
                txtAdjustedAmt.Text = string.Format("{0:N2}", gf_CalculateGridTotal(9));
                txtAdjustedAmt.Text = string.Format("{0:N2}", Localization.ParseNativeDouble(txtAdjustedAmt.Text) + dblTempAdjAmt);
                txtUnAdjustedAnt.Text = string.Format("{0:N2}", dblAmount - Localization.ParseNativeDouble(txtAdjustedAmt.Text));
                if (Localization.ParseNativeDouble(txtUnAdjustedAnt.Text) > 0.0)
                {
                    txtUnAdjustedAnt.BackColor = Color.Maroon;
                }
                else
                {
                    txtUnAdjustedAnt.BackColor = Color.PowderBlue;
                }
                if (fgDtls.Rows.Count > intI)
                {
                    fgDtls.CurrentCell = fgDtls[12, intI + 1];
                }
                row = null;
            }
            catch { }
            fgDtls.CellValueChanged += new DataGridViewCellEventHandler(fgDtls_CellValueChanged);
        }

        private int getLedgerIDbyCol(int iCol)
        {
            return Localization.ParseNativeInt(DB.GetSnglValue("SELECT LedgerID from  tbl_AdjustmentLedgers WHERE FormID=" + RefModID + " and OrderNo=" + (iCol - 13)));
        }

        private int getDrCrIDbyCol(int iCol)
        {
            return Localization.ParseNativeInt(DB.GetSnglValue("SELECT DrCrID from  tbl_AdjustmentLedgers WHERE Isdeleted=0 and FormID=" + RefModID + " and OrderNo=" + (iCol - 13)));
        }

        private double gf_CalculateGridTotal(int mCol)
        {
            double dblTtl = 0;
            try
            {
                dblTempAdjAmt = 0.0;
                dblTempAdjAmt_Cr = 0.0;
                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    if (fgDtls.Rows[i].Cells[0].Value != null)
                    {
                        if (Localization.ParseNativeInt(fgDtls.Rows[i].Cells[0].Value.ToString()) == DrID)
                        {
                            if (fgDtls.Rows[i].Cells[mCol].Value != null && Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[mCol].Value.ToString()) > 0)
                            {
                                dblTempAdjAmt += Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[mCol].Value.ToString());
                            }
                        }
                        else if (Localization.ParseNativeInt(fgDtls.Rows[i].Cells[0].Value.ToString()) == CrID)
                        {
                            if (fgDtls.Rows[i].Cells[mCol].Value != null && Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[mCol].Value.ToString()) > 0)
                            {
                                dblTempAdjAmt_Cr += Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[mCol].Value.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
            return dblTtl;
        }

        private double gf_CalcExtraLedgers()
        {
            double dbltmp_ledgers = 0.0;
            try
            {
                DataTable Dt = DB.GetDT("SELECT OrderNo, DrCrID  from  tbl_AdjustmentLedgers WHERE FormID=" + RefModID + "", false);
                int _AdjType = 0;

                for (int i = 0; i <= fgDtls.RowCount - 1; i++)
                {
                    for (int c = 14; c <= fgDtls.ColumnCount - 1; c++)
                    {
                        DataRow[] rst = Dt.Select("OrderNo=" + (c - 13));
                        if (rst.Length > 0)
                        {
                            foreach (DataRow r in rst)
                            {
                                _AdjType = Localization.ParseNativeInt(r["DrCrID"].ToString());
                            }

                            if (_AdjType == DrID)
                            {
                                if (fgDtls.Rows[i].Cells[c].Value != null)
                                    dbltmp_ledgers += Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[c].Value.ToString());
                            }
                            else if (_AdjType == CrID)
                            {
                                if (fgDtls.Rows[i].Cells[c].Value != null)
                                    dbltmp_ledgers -= Localization.ParseNativeDouble(fgDtls.Rows[i].Cells[c].Value.ToString());
                            }
                        }
                    }
                }
            }
            catch { return 0; }
            return dbltmp_ledgers;
        }

        #endregion Common Functions

        #region GridEvent

        private void fgDtls_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            try
            {
                if (eAdjType == AdjType.Manual)
                {
                    dblTempAdjAmt = Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString());
                    dblPrevAdjAmt = Localization.ParseNativeDouble(fgDtls.Rows[e.RowIndex].Cells[9].Value.ToString());
                }
            }
            catch { }
        }

        private void fgDtls_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    AdjustAmt(fgDtls.CurrentRow.Index, AdjType.Auto);
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            double dblextLdrs = 0;
            try
            {
                //double num;

                DataGridViewRow row = this.fgDtls.Rows[e.RowIndex];
                if ((e.ColumnIndex == 0) || (e.ColumnIndex == 2))
                {
                    if (Localization.ParseNativeDouble(txtUnAdjustedAnt.Text.Trim()) == 0)
                    {
                        row.Cells[0].Value = null;
                        row.Cells[1].Value = null;
                        row.Cells[2].Value = null;
                        row.Cells[9].ReadOnly = false;
                        row.Cells[12].ReadOnly = true;
                        return;
                    }
                    else
                    {
                        row.Cells[9].ReadOnly = false;
                        row.Cells[12].ReadOnly = false;
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
                                if (Localization.ParseNativeDouble(txtUnAdjustedAnt.Text.Trim()) > 0)
                                    fgDtls.Rows.Add();
                            }
                        }
                        catch { fgDtls.Rows.Add(); }
                    }
                }

                if (e.ColumnIndex == 2)
                {
                    if (Localization.ParseNativeDouble(txtUnAdjustedAnt.Text.Trim()) == 0)
                    {
                        row.Cells[2].Value = null;
                        row.Cells[9].ReadOnly = true;
                        row.Cells[12].ReadOnly = true;
                        return;
                    }
                    else
                    {
                        row.Cells[9].ReadOnly = true;
                        row.Cells[12].ReadOnly = false;
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
                            fgDtls.Rows[e.RowIndex].Cells[12].Value = 0;
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
                            fgDtls.Rows[e.RowIndex].Cells[12].Value = 0;

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
                            fgDtls.Rows[e.RowIndex].Cells[12].Value = 0;

                            fgDtls.Rows[e.RowIndex].Cells[5].ReadOnly = false;
                            fgDtls.Rows[e.RowIndex].Cells[6].ReadOnly = false;
                            fgDtls.Rows[e.RowIndex].Cells[7].ReadOnly = false;
                            fgDtls.Rows[e.RowIndex].Cells[8].ReadOnly = false;
                        }

                        if ((Localization.ParseNativeInt(row.Cells[2].Value.ToString()) == OnAccountID) || (Localization.ParseNativeInt(row.Cells[2].Value.ToString()) == AdvanceID))
                        {
                            for (int c = 14; c <= fgDtls.ColumnCount - 1; c++)
                            { row.Cells[c].ReadOnly = true; }
                        }
                        else
                        {
                            for (int c = 14; c <= fgDtls.ColumnCount - 1; c++)
                            { row.Cells[c].ReadOnly = false; }
                        }
                    }
                }

                if (this.eAdjType != AdjType.Manual)
                {
                    return;
                }

                if (e.ColumnIndex > 12)
                {
                    dblextLdrs = gf_CalcExtraLedgers();
                    row.Cells[9].Value = String.Format("{0:N2}", (Localization.ParseNativeDouble(row.Cells[12].Value.ToString()) + dblextLdrs));
                }

                DataGridViewCheckBoxCell cell = row.Cells[10] as DataGridViewCheckBoxCell;
                if (Localization.ParseNativeDouble(row.Cells[9].Value.ToString()) <= 0.0)
                {
                    if (Localization.ParseNativeInt(row.Cells[2].Value.ToString()) == AgainstRefID)
                    {
                        dblextLdrs = gf_CalcExtraLedgers();
                        row.Cells[8].Value = String.Format("{0:N2}", (Localization.ParseNativeDouble(row.Cells[11].Value.ToString()))/* - dblextLdrs*/);
                    }
                }

                if (Localization.ParseNativeDouble(Conversions.ToString(row.Cells[9].Value)) > this.dblAmount)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Their is no more amount to adjust.");
                    row.Cells[9].Value = string.Format("{0:N2}", 0);
                    //dblextLdrs = gf_CalcExtraLedgers();
                    txtAdjustedAmt.Text = string.Format("{0:N2}", dblTempAdjAmt_Cr + dblTempAdjAmt/* + dblextLdrs*/);
                    txtUnAdjustedAnt.Text = string.Format("{0:N2}", dblAmount - Localization.ParseNativeDouble(txtAdjustedAmt.Text)/* - dblextLdrs*/);
                    return;
                }

                txtUnAdjustedAnt.Text = string.Format("{0:N2}", dblAmount - (dblTempAdjAmt_Cr + dblTempAdjAmt) /*- dblextLdrs*/);
                //dblextLdrs = gf_CalcExtraLedgers();
                if (Localization.ParseNativeDouble(Conversions.ToString(row.Cells[9].Value)) > 0.0)
                {
                    if (Localization.ParseNativeInt(row.Cells[2].Value.ToString()) == AgainstRefID)
                    {
                        row.Cells[8].Value = string.Format("{0:N2}", Localization.ParseNativeDouble(Conversions.ToString(row.Cells[11].Value)) - Localization.ParseNativeDouble(Conversions.ToString(row.Cells[9].Value))/* - dblextLdrs*/);
                    }
                }

                gf_CalculateGridTotal(9);

                txtAdjustedAmt.Text = string.Format("{0:N2}", dblTempAdjAmt + dblTempAdjAmt_Cr /*+ dblextLdrs*/);

                if (Localization.ParseNativeDouble(this.txtUnAdjustedAnt.Text) > 0.0)
                {

                    this.txtUnAdjustedAnt.BackColor = Color.Maroon;
                }
                else
                {
                    this.txtUnAdjustedAnt.BackColor = Color.PowderBlue;
                }
            }
            catch
            {

            }
        }

        private void fgDtls_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 12)
            {
                if (Localization.ParseNativeDouble(this.fgDtls.Rows[e.RowIndex].Cells[12].Value.ToString()) >= 0)
                {
                    try
                    {
                        gf_CalculateGridTotal(12);
                        gf_CalcExtraLedgers();
                        this.fgDtls.Rows[e.RowIndex].Cells[9].Value = String.Format("{0:N2}", (Localization.ParseNativeDouble(this.fgDtls.Rows[e.RowIndex].Cells[12].Value.ToString()) + gf_CalcExtraLedgers()));

                        if (fgDtls.Rows[e.RowIndex].Cells[0].Value != null)
                        {
                            if (Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[0].Value.ToString()) == DrID)
                            {
                                txtAdjustedAmt.Text = string.Format("{0:N2}", dblTempAdjAmt + dblTempAdjAmt_Cr /*+ dblextLdrs*/);
                                txtUnAdjustedAnt.Text = string.Format("{0:N2}", dblAmount - Localization.ParseNativeDouble(txtAdjustedAmt.Text)/*- dblextLdrs*/);
                            }
                            else if (Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[0].Value.ToString()) == CrID)
                            {
                                txtAdjustedAmt.Text = string.Format("{0:N2}", dblTempAdjAmt_Cr + dblTempAdjAmt /*+ dblextLdrs*/);
                                txtUnAdjustedAnt.Text = string.Format("{0:N2}", dblAmount - Localization.ParseNativeDouble(txtAdjustedAmt.Text) /*- dblextLdrs*/);
                            }
                        }
                    }
                    catch { }
                }
            }
        }

        #endregion GridEvent

        private void frmAdjustment_KeyUp(object sender, KeyEventArgs e)
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
