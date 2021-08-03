using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using CIS_Bussiness;using CIS_DBLayer;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CIS_Utilities;

namespace CIS_Textil
{
    public partial class CIS_GridSettings : UserControl
    {
        private static string _mdiFormNM;
        public int SubGridID;
        object frmMdi = Application.OpenForms[_mdiFormNM];

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public CIS_GridSettings()
        {
            InitializeComponent();
            dgvGridSettings.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(EventHandles.fgDtls_EditingControlShowing);
            dgvGridSettings.DataError += new DataGridViewDataErrorEventHandler(EventHandles.fgDtls_DataError);
            dgvGridSettings.CellEnter += new DataGridViewCellEventHandler(EventHandles.fgDtls_CellEnter);
        }

        private void BindGrid()
        {
            object instance = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
            double dbliIDentity = Conversion.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(instance, null, "iIDentity", new object[0], null, null, null)));
            CIS_DataGridViewEx.DataGridViewEx fgDtls = (CIS_DataGridViewEx.DataGridViewEx)(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(instance, null, "fgDtls", new object[0], null, null, null)));

            DataTable dt = DB.GetDT("Select * from fn_FillGridSettings(" + dbliIDentity + "," + SubGridID + ")", false);
            dgvGridSettings.Rows.Clear();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dgvGridSettings.Rows.Add();
                    dgvGridSettings.Rows[i].Cells[0].Value = dt.Rows[i]["GridID"].ToString();
                    dgvGridSettings.Rows[i].Cells[1].Value = dt.Rows[i]["SubGridID"].ToString();
                    dgvGridSettings.Rows[i].Cells[2].Value = dt.Rows[i]["ColHeading"].ToString();
                    dgvGridSettings.Rows[i].Cells[3].Value = dt.Rows[i]["ColOrder"].ToString();

                    if (fgDtls.Columns[i].Width.ToString() != dt.Rows[i]["ColSize"].ToString())
                    {
                        dgvGridSettings.Rows[i].Cells[4].Value = fgDtls.Columns[i].Width;
                    }
                    else
                    {
                        dgvGridSettings.Rows[i].Cells[4].Value = dt.Rows[i]["ColSize"].ToString();
                    }

                    dgvGridSettings.Rows[i].Cells[7].Value = dt.Rows[i]["IsRequired"].ToString();
                    dgvGridSettings.Rows[i].Cells[8].Value = dt.Rows[i]["IsRepeatRow"].ToString();
                    dgvGridSettings.Rows[i].Cells[9].Value = dt.Rows[i]["IsCompulsory"].ToString();
                    dgvGridSettings.Rows[i].Cells[10].Value = dt.Rows[i]["ToolTip"].ToString();
                    dgvGridSettings.Rows[i].Cells[11].Value = dt.Rows[i]["SumCols"].ToString();

                    dgvGridSettings.Rows[i].Cells[2].ReadOnly = false;
                    dgvGridSettings.Rows[i].Cells[10].ReadOnly = false;
                    dgvGridSettings.Rows[i].Cells[11].ReadOnly = false;

                    if (dgvGridSettings.Rows[i].Cells[7].Value.ToString().ToUpper() == "YES")
                    {
                        dgvGridSettings.Rows[i].Cells[5].ReadOnly = true;
                        dgvGridSettings.Rows[i].Cells[6].ReadOnly = true;

                        for (int n = 0; n <= (dgvGridSettings.ColumnCount - 1); n++)
                        {
                            dgvGridSettings.Rows[i].Cells[n].Style.BackColor = Color.LightGray;
                            dgvGridSettings.Rows[i].Cells[n].Style.BackColor = Color.LightGray;
                        }
                    }

                    if (Localization.ParseBoolean(dt.Rows[i]["IsEditable"].ToString()))
                    {
                        DataGridViewCheckBoxCell chk = dgvGridSettings.Rows[i].Cells[5] as DataGridViewCheckBoxCell;
                        chk.Value = true;
                    }

                    if (Localization.ParseBoolean(dt.Rows[i]["IsHidden"].ToString()))
                    {
                        DataGridViewCheckBoxCell chk = dgvGridSettings.Rows[i].Cells[6] as DataGridViewCheckBoxCell;
                        chk.Value = true;
                    }

                    string strDatatype = DB.GetSnglValue(string.Format("Select ColDataType From tbl_GridSettings  Where GridID=" + dgvGridSettings.Rows[i].Cells[0].Value.ToString() + " and SubGridID=" + dgvGridSettings.Rows[i].Cells[1].Value.ToString() + " and ColOrder=" + dgvGridSettings.Rows[i].Cells[3].Value + ""));
                    if (strDatatype == "D" || strDatatype == "I")
                    {
                        dgvGridSettings.Rows[i].Cells[11].ReadOnly = false;
                    }
                    else { dgvGridSettings.Rows[i].Cells[11].ReadOnly = true; }
                }
            }

        }

        private void dgvGridSettings_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

            }
        }

        private void txtClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void CIS_GridSettings_Load(object sender, EventArgs e)
        {
            btnSave.Focus();
            lbl_ScrollHelp.Text = AppMsg.GRIDSETTINGS;
            BindGrid();
        }

        public string SetMDIform
        {
            get { return _mdiFormNM; }
            set { _mdiFormNM = value; }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           
            if (dgvGridSettings.Rows.Count > 0)
            {
                String StrQry = string.Empty;
                for (int i = 0; i < dgvGridSettings.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk_Editable = dgvGridSettings.Rows[i].Cells[5] as DataGridViewCheckBoxCell;
                    DataGridViewCheckBoxCell chk_Hidden = dgvGridSettings.Rows[i].Cells[6] as DataGridViewCheckBoxCell;

                    DataGridViewCheckBoxCell chk_RepeatRow = dgvGridSettings.Rows[i].Cells[8] as DataGridViewCheckBoxCell;

                    DataGridViewTextBoxCell txt_ColSize = dgvGridSettings.Rows[i].Cells[4] as DataGridViewTextBoxCell;
                    DataGridViewTextBoxCell txt_ColOrder = dgvGridSettings.Rows[i].Cells[3] as DataGridViewTextBoxCell;

                    DataGridViewTextBoxCell txt_ColHeading = dgvGridSettings.Rows[i].Cells[2] as DataGridViewTextBoxCell;
                    DataGridViewTextBoxCell txt_ToolTip = dgvGridSettings.Rows[i].Cells[10] as DataGridViewTextBoxCell;

                    DataGridViewCheckBoxCell chk_SumCols = dgvGridSettings.Rows[i].Cells[11] as DataGridViewCheckBoxCell;

                    #region IsEditable
                    if (chk_Editable.Value != null)
                    {
                        if (Localization.ParseBoolean(chk_Editable.Value.ToString()))
                        {
                            StrQry += string.Format("Update tbl_GridSettings Set IsEditable=1 Where GridID=" + dgvGridSettings.Rows[i].Cells[0].Value.ToString() + " and SubGridID=" + dgvGridSettings.Rows[i].Cells[1].Value.ToString() + " and ColOrder=" + dgvGridSettings.Rows[i].Cells[3].Value + "") + Environment.NewLine;
                        }
                        else
                        {
                            StrQry += string.Format("Update tbl_GridSettings Set IsEditable=0 Where GridID=" + dgvGridSettings.Rows[i].Cells[0].Value.ToString() + " and SubGridID=" + dgvGridSettings.Rows[i].Cells[1].Value.ToString() + " and ColOrder=" + dgvGridSettings.Rows[i].Cells[3].Value + "") + Environment.NewLine;
                        }
                    }
                    else
                    {
                        StrQry += string.Format("Update tbl_GridSettings Set IsEditable=0 Where GridID=" + dgvGridSettings.Rows[i].Cells[0].Value.ToString() + " and SubGridID=" + dgvGridSettings.Rows[i].Cells[1].Value.ToString() + " and ColOrder=" + dgvGridSettings.Rows[i].Cells[3].Value + "") + Environment.NewLine;
                    }
                    #endregion

                    #region IsHeaden
                    if (chk_Hidden.Value != null)
                    {
                        if (Localization.ParseBoolean(chk_Hidden.Value.ToString()))
                        {
                            StrQry += string.Format("Update tbl_GridSettings Set IsHidden=1 Where GridID=" + dgvGridSettings.Rows[i].Cells[0].Value.ToString() + " and SubGridID=" + dgvGridSettings.Rows[i].Cells[1].Value.ToString() + " and ColOrder=" + dgvGridSettings.Rows[i].Cells[3].Value + "") + Environment.NewLine;
                        }
                        else
                        {
                            StrQry += string.Format("Update tbl_GridSettings Set IsHidden=0 Where GridID=" + dgvGridSettings.Rows[i].Cells[0].Value.ToString() + " and SubGridID=" + dgvGridSettings.Rows[i].Cells[1].Value.ToString() + " and ColOrder=" + dgvGridSettings.Rows[i].Cells[3].Value + "") + Environment.NewLine;
                        }
                    }
                    else
                    {
                        StrQry += string.Format("Update tbl_GridSettings Set IsHidden=0 Where GridID=" + dgvGridSettings.Rows[i].Cells[0].Value.ToString() + " and SubGridID=" + dgvGridSettings.Rows[i].Cells[1].Value.ToString() + " and ColOrder=" + dgvGridSettings.Rows[i].Cells[3].Value + "") + Environment.NewLine;
                    }
                    #endregion

                    #region IsRepeateRow
                    if (chk_RepeatRow.Value != null)
                    {
                        if (Localization.ParseBoolean(chk_RepeatRow.Value.ToString()))
                        {
                            StrQry += string.Format("Update tbl_GridSettings Set IsRepeatRow=1 Where GridID=" + dgvGridSettings.Rows[i].Cells[0].Value.ToString() + " and SubGridID=" + dgvGridSettings.Rows[i].Cells[1].Value.ToString() + " and ColOrder=" + dgvGridSettings.Rows[i].Cells[3].Value + "") + Environment.NewLine;
                        }
                        else
                        {
                            StrQry += string.Format("Update tbl_GridSettings Set IsRepeatRow=0 Where GridID=" + dgvGridSettings.Rows[i].Cells[0].Value.ToString() + " and SubGridID=" + dgvGridSettings.Rows[i].Cells[1].Value.ToString() + " and ColOrder=" + dgvGridSettings.Rows[i].Cells[3].Value + "") + Environment.NewLine;
                        }
                    }
                    else
                    {
                        StrQry += string.Format("Update tbl_GridSettings Set IsRepeatRow=0 Where GridID=" + dgvGridSettings.Rows[i].Cells[0].Value.ToString() + " and SubGridID=" + dgvGridSettings.Rows[i].Cells[1].Value.ToString() + " and ColOrder=" + dgvGridSettings.Rows[i].Cells[3].Value + "") + Environment.NewLine;
                    }
                    #endregion

                    #region ColSize
                    if (txt_ColSize.Value != null)
                    {
                        if (txt_ColSize.Value.ToString().Length > 0)
                        {
                            StrQry += string.Format("Update tbl_GridSettings Set ColSize=" + txt_ColSize.Value + " Where GridID=" + dgvGridSettings.Rows[i].Cells[0].Value.ToString() + " and SubGridID=" + dgvGridSettings.Rows[i].Cells[1].Value.ToString() + " and ColOrder=" + dgvGridSettings.Rows[i].Cells[3].Value + "") + Environment.NewLine;
                        }
                        else
                        {
                            StrQry += string.Format("Update tbl_GridSettings Set ColSize=50 Where GridID=" + dgvGridSettings.Rows[i].Cells[0].Value.ToString() + " and SubGridID=" + dgvGridSettings.Rows[i].Cells[1].Value.ToString() + " and ColOrder=" + dgvGridSettings.Rows[i].Cells[3].Value + "") + Environment.NewLine;
                        }
                    }
                    else
                    {
                        StrQry += string.Format("Update tbl_GridSettings Set ColSize=50 Where GridID=" + dgvGridSettings.Rows[i].Cells[0].Value.ToString() + " and SubGridID=" + dgvGridSettings.Rows[i].Cells[1].Value.ToString() + " and ColOrder=" + dgvGridSettings.Rows[i].Cells[3].Value + "") + Environment.NewLine;
                    }
                    #endregion

                    #region ColOrder
                    if (txt_ColOrder.Value != null)
                    {
                        if (txt_ColOrder.Value.ToString().Length > 0)
                        {
                            StrQry += string.Format("Update tbl_GridSettings Set ColOrder=" + txt_ColOrder.Value + " Where GridID=" + dgvGridSettings.Rows[i].Cells[0].Value.ToString() + " and SubGridID=" + dgvGridSettings.Rows[i].Cells[1].Value.ToString() + " and ColHeading=" + CommonLogic.SQuote(dgvGridSettings.Rows[i].Cells[2].Value.ToString()) + "") + Environment.NewLine;
                        }
                    }
                    #endregion

                    #region ColHeading
                    if (txt_ColHeading.Value != null)
                    {
                        if (txt_ColHeading.Value.ToString().Length > 0)
                        {
                            StrQry += string.Format("Update tbl_GridSettings Set ColHeading=" + CommonLogic.SQuote(txt_ColHeading.Value.ToString()) + " Where GridID=" + dgvGridSettings.Rows[i].Cells[0].Value.ToString() + " and SubGridID=" + dgvGridSettings.Rows[i].Cells[1].Value.ToString() + " and ColOrder=" + txt_ColOrder.Value + "") + Environment.NewLine;
                        }
                    }
                    #endregion

                    #region ToolTip
                    if (txt_ToolTip.Value != null)
                    {
                        if (txt_ToolTip.Value.ToString().Length > 0)
                        {
                            StrQry += string.Format("Update tbl_GridSettings Set ToolTip=" + CommonLogic.SQuote(txt_ToolTip.Value.ToString()) + " Where GridID=" + dgvGridSettings.Rows[i].Cells[0].Value.ToString() + " and SubGridID=" + dgvGridSettings.Rows[i].Cells[1].Value.ToString() + " and ColOrder=" + txt_ColOrder.Value + "") + Environment.NewLine;
                        }
                    }
                    #endregion

                    #region SumCols
                    

                    if (chk_SumCols.Value != null)
                    {
                        if (Localization.ParseBoolean(chk_SumCols.Value.ToString()))
                        {
                            StrQry += string.Format("Update tbl_GridSettings Set SumCols=1 Where GridID=" + dgvGridSettings.Rows[i].Cells[0].Value.ToString() + " and SubGridID=" + dgvGridSettings.Rows[i].Cells[1].Value.ToString() + " and ColOrder=" + dgvGridSettings.Rows[i].Cells[3].Value + "") + Environment.NewLine;
                        }
                        else
                        {
                            StrQry += string.Format("Update tbl_GridSettings Set SumCols=0 Where GridID=" + dgvGridSettings.Rows[i].Cells[0].Value.ToString() + " and SubGridID=" + dgvGridSettings.Rows[i].Cells[1].Value.ToString() + " and ColOrder=" + dgvGridSettings.Rows[i].Cells[3].Value + "") + Environment.NewLine;
                        }
                    }
                    else
                    {
                        StrQry += string.Format("Update tbl_GridSettings Set SumCols=0 Where GridID=" + dgvGridSettings.Rows[i].Cells[0].Value.ToString() + " and SubGridID=" + dgvGridSettings.Rows[i].Cells[1].Value.ToString() + " and ColOrder=" + dgvGridSettings.Rows[i].Cells[3].Value + "") + Environment.NewLine;
                    }
                    #endregion

                }
                DB.ExecuteSQL(StrQry);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecuritySuccess, "Success", "Grid Settings Saved Successfully");

                object instance = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                double dbliIDentity = Conversion.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(instance, null, "iIDentity", new object[0], null, null, null)));
                DataTable dt_HasDtls_Grd = (DataTable)(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(instance, null, "dt_HasDtls_Grd", new object[0], null, null, null)));
                DataTable dt_AryCalcvalue = (DataTable)(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(instance, null, "dt_AryCalcvalue", new object[0], null, null, null)));
                DataTable dt_AryIsRequired = (DataTable)(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(instance, null, "dt_AryIsRequired", new object[0], null, null, null)));
                CIS_DataGridViewEx.DataGridViewEx fgDtls = (CIS_DataGridViewEx.DataGridViewEx)(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(instance, null, "fgDtls", new object[0], null, null, null)));
                //DetailGrid_Setup.CreateDtlGrid(this, pnlDetail, fgDtls, dt_HasDtls_Grd, dt_AryCalcvalue, dt_AryIsRequired, true, false, true, 0, 0, true);
                DetailGrid_Setup.CreateDtlGrid(instance, fgDtls);
                this.Hide();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void CIS_GridSettings_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Hide();
        }
    }
}
