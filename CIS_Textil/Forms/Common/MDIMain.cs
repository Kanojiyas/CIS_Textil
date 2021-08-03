using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using CIS_CLibrary;
using CIS_CLibrary.ToolTip;
using CIS_Utilities;
using Infragistics.Win;
using Infragistics.Win.Printing;
using Infragistics.Win.UltraWinGrid;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class MDIMain : Form
    {
        bool IsDemoUser = false;
        Double i = 0.1;
        static int j = 0;
        public static int VoucherTypeID = 0;
        public System.Collections.Hashtable HashFunctions;
        CIS_FormSettings uctrl_Setting;
        private readonly DisplaySettings _originalSettings;
        private int currentMouseOverRow;

        public MDIMain()
        {
            MDIMain.CheckForIllegalCrossThreadCalls = false;
            Db_Detials.IsOnceADay = false;
            Db_Detials.IsOnceADay_folder = false;
            Db_Detials.IsBkpDoneForDay = false;
            Db_Detials.IsBkpDoneForDay_Folder = false;
            Db_Detials.IsAlwaysNewFile = false;
            Db_Detials.IsAlwaysNewFile_Fodler = false;

            ///* GET ORIGINAL SCREEN RESOLUTION */
            _originalSettings = DisplayManager.GetCurrentSettings();
            this.HashFunctions = new System.Collections.Hashtable();
            InitializeComponent();
            //if ((_originalSettings.Width <= 1280) && (_originalSettings.Height <= 768))
            //{
            pnlDockLeft.Expand = false;
            //}

            this.Size = Screen.PrimaryScreen.WorkingArea.Size;
            mnuMain.ForeColor = Color.White;
            BasicToolTipView btv = (tltControls.DataView as BasicToolTipView).Animation(BasicToolTipView.Effect.SlideFade, 200);
            this.Opacity = this.i;
            this.tmrFadeIn.Enabled = true;
            this.tmr3.Enabled = true;
            LoadMenu Loadmnu = new LoadMenu();
            Loadmnu.BuildAutocompleteMenu(cboQuickMenu, Db_Detials.UserType, Db_Detials.CompID);
            this.Text = "";
        }

        private void MDIMain_Load(object sender, EventArgs e)
        {
            pnlDockBottom.Expand = false;
            ExpandCollapsePnl("L");
            this.Text = "";
            txtQuickMenu.Enabled = false;

            txtQuickMenu.Enabled = true;
            //AppStart.IsBool = 1;
            string UserName = DB.GetSnglValue(string.Format("Select UserName from {0} Where UserId = {1}", Db_Detials.tbl_UserMaster, Db_Detials.UserID));
            TS_Userpnl.Text = string.Format("User Name : {0}", UserName);
            TS_Companypnl.Text = GetAssemblyInfo.CompanyName;
            this.Text = Db_Detials.CompanyName + " (Year " + DB.GetSnglValue("Select YrDesc From tbl_YearMaster Where YearID = " + Db_Detials.YearID) + ")";
            string Password = DB.GetSnglValue(string.Format("Select Password from {0} Where UserName = '{1}' and IsDeleted=0", Db_Detials.tbl_UserMaster, UserName));
            string EmployeeName = DB.GetSnglValue(string.Format("Select EmployeeName from {0} Where UserName = '{1}' and IsDeleted=0", Db_Detials.tbl_UserMaster, UserName));
            txtUserName.Text = UserName;
            txtPassword.Text = CommonLogic.UnmungeString(Password);
            txtEmployeeName.Text = EmployeeName;
            //int getadminid = Localization.ParseNativeInt(DB.GetSnglValue("Select UserType from tbl_UserMaster where UserId=" + Db_Detials.UserID));
            bool IsshowStockPanel = Localization.ParseBoolean(DB.GetSnglValue("Select ShowStockPanel from tbl_UserMaster where UserId=" + Db_Detials.UserID));

            if ((IsshowStockPanel == true))
            {
                fgdtls.DataSource = DB.GetDT(string.Format("Select  FabricName,Minimumpcs, BalQtyInStock   from {0}", "fn_MinimumStock()"), false);
                pnlstock.Visible = true;
                fgdtls.Focus();
            }
            try
            {
                grdSearch.InitializeLayout += new InitializeLayoutEventHandler(CommonCls.grdSearch_InitializeLayout);
            }
            catch { }

            try
            {
                IniFile ini = new IniFile(Application.StartupPath.ToString() + "\\Others\\System.ini");
                if (System.IO.File.Exists(ini.IniReadValue("WALLPAPER", "LINK")) == true)
                {
                    this.BackgroundImage = Image.FromFile(ini.IniReadValue("WALLPAPER", "LINK"));
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
            this.BringToFront();

            //Call LoadTreeMenu()

            //-- " Menu Loading..."

            CommonCls.LoadMDIMenu();
            CommonCls.LoadMDIMenu_NoHash();
            ToolStripManager.Renderer = new Office2007Renderer.Office2007Renderer();

            //---- Piece No increment Format


            tmr1.Start();
            tmr2.Start();
            FillCompanies();
            FillUserGrid();

            FillReminder();
            FillTask();
            LoadGlobalVariables();
            GetActiveFont();
            Db_Detials.PCS_NO_INCMT = Localization.ParseNativeInt(GlobalVariables.PCS_NO_INCMT);
            //try
            //{
            //    ClassBroadCast broadCast = new ClassBroadCast();
            //    Thread tBroadCast = new Thread(new ThreadStart(broadCast.BroadCast));
            //    tBroadCast.IsBackground = true;
            //    tBroadCast.Start();
            //}
            //catch { }

            try
            {
                Thread receive = new Thread(new ThreadStart(ReceiveNews));
                receive.IsBackground = true;
                receive.Start();
            }
            catch { }
        }

        private void LoadGlobalVariables()
        {
            try
            {
                GlobalVariables gs = new GlobalVariables();
                using (IDataReader iDr = DB.GetRS("SELECT GSName, GSValue from tbl_GlobalSettings WHERE CompanyForID=" + Db_Detials.CompID))
                {
                    while (iDr.Read())
                    {
                        try
                        {
                            PropertyInfo property = gs.GetType().GetProperty(iDr["GSName"].ToString());
                            if (property != null)
                                property.SetValue(gs, iDr["GsValue"].ToString(), null);
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }

        private void tmr1_Tick(object sender, EventArgs e)
        {
            this.TS_Datetimepnl.Text = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "      ver. " + Db_Detials.AppVersion;
            CheckConnection();
            ApplyVoucherTypeID();
        }
        private void tmr2_Tick(object sender, EventArgs e)
        {
            ToolStripStatusLabel label = this.TS_Companypnl;
            if (label.Text == "")
            {
                label.Text = GetAssemblyInfo.CompanyName; ;
            }
            else
            {
                label.Text = "";
            }
            label = null;
        }
        private void tmr3_Tick(object sender, EventArgs e)
        {
            j = j + 1;
            if (IsDemoUser == true)
                j = 0;
            if (j == 100)
                ////start the Fade Out
                return;
        }
        private void tmrFadeIn_Tick(object sender, EventArgs e)
        {
            i += 0.05;

            //if form is full visible we execute the Fade Out Effect
            if (i >= 1)
            {
                this.Opacity = 1;
                tmrFadeIn.Enabled = false;
                ////stop the Fade In Effect
                tmr3.Enabled = true;
                return;
            }
            this.Opacity = i;
        }

        private void MDIMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                EventHandles.Closeform();
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", exception1.Message);
            }
        }

        private void MDIMain_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                if (e.Control && e.KeyCode == Keys.I)
                {
                    try
                    {
                        frmSrchImg srchFrm = new frmSrchImg();
                        try
                        {
                            if (objectValue != null)
                            {
                                Form frm = (Form)objectValue;
                                Control cntl = frm.ActiveControl;

                                frmTrnsIface frmface = new frmTrnsIface();
                                object activeChild = (Form)Navigate.GetActiveChild();
                                try
                                {
                                    frmface = (frmTrnsIface)activeChild;
                                    if (cntl is CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)
                                    {
                                        try
                                        {
                                            CIS_MultiColumnComboBox.CIS_MultiColumnComboBox sCbo = (CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)cntl;
                                            srchFrm.sSearchText = sCbo.Text;
                                        }
                                        catch { }
                                    }
                                    else if (cntl.Parent.Parent is CIS_DataGridViewEx.DataGridViewEx)
                                    {
                                        try
                                        {
                                            CIS_DataGridViewEx.DataGridViewEx sCbo = (CIS_DataGridViewEx.DataGridViewEx)cntl.Parent.Parent;
                                            srchFrm.sSearchText = sCbo.Rows[sCbo.CurrentRow.Index].Cells[sCbo.CurrentCell.ColumnIndex].FormattedValue.ToString();
                                        }
                                        catch { }
                                    }
                                    else if (cntl is CIS_Textbox)
                                    {
                                        CIS_Textbox txtBox = (CIS_Textbox)cntl;
                                        srchFrm.sSearchText = txtBox.Text;
                                    }
                                }
                                catch { srchFrm.sSearchText = ((CIS_Textil.frmReportTool)(activeChild)).UGrid_Rpt.ActiveCell.Text; }
                            }
                        }
                        catch (Exception ex)
                        {
                            Navigate.logError(ex.Message, ex.StackTrace);
                        }
                        srchFrm.ShowDialog();
                    }
                    catch (Exception ex1)
                    {
                        Navigate.logError(ex1.Message, ex1.StackTrace);
                    }
                }

                if ((objectValue == null) & (pnlstock.Visible == false) & (e.KeyCode == Keys.Escape))
                {
                    EventHandles.Closeform();
                }
                else if ((objectValue != null) & (e.KeyCode == Keys.Escape))
                {
                    try
                    {
                        tbbtnChangeComp.Enabled = true;

                        if (uctrl_Setting != null)
                        {
                            object instance = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                            if (uctrl_Setting.Visible == true)
                            {
                                dynamic objfrm = instance;
                                if (instance != null)
                                {
                                    try
                                    {
                                        Console.WriteLine(RuntimeHelpers.GetObjectValue(objfrm.blnFormAction));
                                        Navigate.EnableNavigate(0, Enum_Define.ActionType.Not_Active, ref instance);
                                        ((DataTable)objfrm.dt_HasDtls_Grd).Clear();
                                        ((DataTable)objfrm.dt_AryCalcvalue).Clear();
                                        ((DataTable)objfrm.dt_AryIsRequired).Clear();
                                    }
                                    catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                                    objfrm.Close();
                                    objfrm.Dispose();
                                }
                                uctrl_Setting.Hide();
                                double dbliIDentity = Conversion.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(instance, null, "iIDentity", new object[0], null, null, null)));
                                EventHandles.ShowbyFormID(dbliIDentity.ToString(), null, null, -1, -1);
                                return;
                            }
                        }

                        if (pnlDockBottom.Expand == true)
                        {
                            pnlDockBottom.Expand = false;
                            btnPrint.Visible = false;
                            return;
                        }
                        else
                            if ((Conversions.ToInteger(NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null)) == 0) | (Conversions.ToInteger(NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null)) == 1))
                            {
                                if (Localization.ParseNativeDouble(Conversions.ToString(NewLateBinding.LateGet(objectValue, null, "_IRecordCount", new object[0], null, null, null))) == 0.0)
                                {
                                    EventHandles.Closeform();
                                }
                                else
                                {
                                    Form cForm = null;
                                    Navigate.NavigateForm(Enum_Define.Navi_form.Cancel_Record, ref cForm, true, false);
                                }
                            }
                            else if (Conversions.ToInteger(NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null)) == 4)
                            {
                                EventHandles.Closeform();
                            }
                    }
                    catch
                    {
                    }
                }
                else
                {
                    if (e.Control && e.Shift && e.KeyCode == Keys.F)
                    {
                        txtQuickMenu.Focus();
                    }

                    if (e.Shift && e.KeyCode == Keys.S)
                    {
                        try
                        {
                            Form frm = this.ActiveMdiChild;
                            object obj = this.ActiveMdiChild;
                            double dbliIDentity = Conversion.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(obj, null, "iIDentity", new object[0], null, null, null)));
                            Control[] CArray = frm.Controls.Find("fgDtls", true);
                            SaveColumnSettings(CArray, dbliIDentity);
                        }
                        catch { }
                    }

                    if (e.KeyCode == Keys.Escape)
                    {
                        if (pnlstock.Visible == true)
                        {
                            if (fgdtls.Focus() == true)
                            {
                                if (CIS_Dialog.Show("Do you want to close this Panel ?", GetAssemblyInfo.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    pnlstock.Visible = false;
                                }
                            }
                        }
                    }


                    if (e.Control && e.KeyCode == Keys.A)
                    {
                        if (objectValue != null)
                        {
                            if ((int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 4)
                            {
                                tbbtnNew_Click(null, null);
                            }
                        }
                    }
                    else if (e.Control && e.KeyCode == Keys.E)
                    {
                        if (objectValue != null)
                        {
                            if ((int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 4)
                            {
                                tbbtnEdit_Click(null, null);
                            }
                        }
                    }
                    else if (e.Control && e.KeyCode == Keys.S)
                    {
                        if (objectValue != null)
                        {
                            if ((int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 0 || (int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 1)
                            {
                                tbbtnSave_Click(null, null);
                            }
                        }
                    }
                    else if (e.Control && e.KeyCode == Keys.L)
                    {
                        if (objectValue != null)
                        {
                            if ((int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 0 || (int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 1)
                            {
                                tbbtnCancel_Click(null, null);
                            }
                        }
                    }
                    else if (e.Control && e.KeyCode == Keys.D)
                    {
                        if (objectValue != null)
                        {
                            if ((int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 4)
                            {
                                tbbtnDelete_Click(null, null);
                            }
                        }
                    }
                    else if (e.Control && e.KeyCode == Keys.F)
                    {
                        if (objectValue != null)
                        {
                            if ((int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 4)
                            {
                                tbbtnFind_Click(null, null);
                            }
                        }
                    }
                    else if (e.Control && e.KeyCode == Keys.R)
                    {
                        if (objectValue != null)
                        {
                            if ((int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 4)
                            {
                                ShowSearchRefList();
                            }
                        }
                    }
                    else if (e.Control && e.KeyCode == Keys.Home)
                    {
                        if (objectValue != null)
                        {
                            if ((int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 4)
                            {
                                tbbtnFirst_Click(null, null);
                            }
                        }
                    }
                    else if (e.Control && e.KeyCode == Keys.Up)
                    {
                        if (objectValue != null)
                        {
                            if ((int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 4)
                            {
                                tbbtnPrevious_Click(null, null);
                            }
                        }
                    }
                    else if (e.Control && e.KeyCode == Keys.Down)
                    {
                        if (objectValue != null)
                        {
                            if ((int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 4)
                            {
                                tbbtnNext_Click(null, null);
                            }
                        }
                    }
                    else if (e.Control && e.KeyCode == Keys.End)
                    {
                        if (objectValue != null)
                        {
                            if ((int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 4)
                            {
                                tbbtnLast_Click(null, null);
                            }
                        }
                    }
                    else if (e.Control && e.KeyCode == Keys.P)
                    {
                        if (objectValue != null)
                        {
                            if ((int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 4)
                            {
                                tbbtnPrint_Click(null, null);
                            }
                        }
                    }
                    else if (e.Control && e.KeyCode == Keys.I)
                    {
                        if (objectValue != null)
                        {
                            if ((int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 4)
                            {
                                tbbtnEMail_Click(null, null);
                            }
                        }
                    }
                    else if (e.Control && e.KeyCode == Keys.M)
                    {
                        if (objectValue != null)
                        {
                            if ((int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 4)
                            {
                                tbbtnSMS_Click(null, null);
                            }
                        }
                    }
                    else if (e.Control && e.KeyCode == Keys.T)
                    {
                        if (objectValue != null)
                        {
                            if ((int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 4)
                            {
                                tbbtnSettings_Click(null, null);
                            }
                        }
                    }
                    else if (e.Control && e.KeyCode == Keys.G)
                    {

                        {
                            tbbtnLogoffUser_Click(null, null);
                        }
                    }
                    else if (e.Control && e.KeyCode == Keys.O)
                    {
                        if (objectValue != null)
                        {
                            tbbtnClose_Click(null, null);
                        }
                    }
                    else if (e.Control && e.KeyCode == Keys.X)
                    {

                        {
                            tbbtnClose_Click(null, null);
                        }
                    }
                    else if (e.Control && e.KeyCode == Keys.F1)
                    {
                        try
                        {
                            if (objectValue != null)
                            {
                                Form frm = (Form)objectValue;
                                Control cntl = frm.ActiveControl;
                                bool isGridCombo = false;
                                try
                                {
                                    CIS_DataGridViewEx.DataGridViewEx sCbo1 = (CIS_DataGridViewEx.DataGridViewEx)cntl.Parent.Parent;
                                    isGridCombo = true;
                                }
                                catch { isGridCombo = false; }

                                frmTrnsIface frmface = new frmTrnsIface();
                                frmMasterIface frmMasterIface = new frmMasterIface();

                                object activeChild = (Form)Navigate.GetActiveChild();
                                try
                                {
                                    frmface = (frmTrnsIface)activeChild;

                                    if (frmface.blnFormAction != Enum_Define.ActionType.View_Record)
                                    {
                                        if (cntl is CIS_MultiColumnComboBox.CIS_MultiColumnComboBox || isGridCombo == true)
                                        {
                                            if (CIS_Dialog.Show("Do you want Add a Recod in Master....?", GetAssemblyInfo.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            {
                                                if ((int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 0 || (int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 1)
                                                {

                                                    if (cntl is CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)
                                                    {
                                                        CIS_MultiColumnComboBox.CIS_MultiColumnComboBox sCbo = (CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)cntl;
                                                        if (Conversion.Val(sCbo.OpenForm.ToString()) != Conversion.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "iIDentity", new object[0], null, null, null))))
                                                        {
                                                            NewLateBinding.LateSet(objectValue, null, "WindowState", new object[] { FormWindowState.Minimized }, null, null);
                                                            if (sCbo.OpenForm != "")
                                                                EventHandles.ShowbyFormID(sCbo.OpenForm, objectValue, sCbo, -1, -1);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        CIS_DataGridViewEx.DataGridViewEx sCbo = (CIS_DataGridViewEx.DataGridViewEx)cntl.Parent.Parent;
                                                        int RowIndex = sCbo.CurrentRow.Index;
                                                        int ColumnIndex = sCbo.CurrentCell.ColumnIndex;
                                                        DataTable dt_HasDtls = (DataTable)NewLateBinding.LateGet(objectValue, null, "dt_HasDtls_Grd", new object[0], null, null, null);
                                                        DataRow[] rowArray = dt_HasDtls.Select("SubGridID = " + Conversions.ToString(sCbo.Grid_UID));
                                                        int num2 = rowArray.Length - 1;
                                                        frmTrnsIface frmTrnsIface = (frmTrnsIface)objectValue; ;

                                                        for (int i = 0; i <= num2; i++)
                                                        {
                                                            if (i == sCbo.CurrentCell.ColumnIndex)
                                                            {
                                                                if (rowArray[i]["ColDataType"].ToString() == "C")
                                                                {
                                                                    string sFormID = rowArray[i]["OpenForm"].ToString();
                                                                    if (Conversion.Val(sFormID) != 0.0)
                                                                    {
                                                                        NewLateBinding.LateSet(objectValue, null, "WindowState", new object[] { FormWindowState.Minimized }, null, null);
                                                                        EventHandles.ShowbyFormID(sFormID, RuntimeHelpers.GetObjectValue(objectValue), sCbo, sCbo.CurrentCell.ColumnIndex, sCbo.CurrentCell.RowIndex);
                                                                    }
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                catch
                                {
                                    frmMasterIface = (frmMasterIface)activeChild;
                                    if (frmMasterIface.blnFormAction != Enum_Define.ActionType.View_Record)
                                    {
                                        if (cntl is CIS_MultiColumnComboBox.CIS_MultiColumnComboBox || isGridCombo == true)
                                        {

                                            if (CIS_Dialog.Show("Do you want Add a Recod in Master....?", GetAssemblyInfo.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            {
                                                if ((int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 0 || (int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 1)
                                                {

                                                    if (cntl is CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)
                                                    {
                                                        CIS_MultiColumnComboBox.CIS_MultiColumnComboBox sCbo = (CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)cntl;
                                                        if (Conversion.Val(sCbo.OpenForm.ToString()) != Conversion.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "iIDentity", new object[0], null, null, null))))
                                                        {
                                                            NewLateBinding.LateSet(objectValue, null, "WindowState", new object[] { FormWindowState.Minimized }, null, null);
                                                            if (sCbo.OpenForm != "")
                                                                EventHandles.ShowbyFormID(sCbo.OpenForm, objectValue, sCbo, -1, -1);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        CIS_DataGridViewEx.DataGridViewEx sCbo = (CIS_DataGridViewEx.DataGridViewEx)cntl.Parent.Parent;
                                                        int RowIndex = sCbo.CurrentRow.Index;
                                                        int ColumnIndex = sCbo.CurrentCell.ColumnIndex;
                                                        DataTable dt_HasDtls = (DataTable)NewLateBinding.LateGet(objectValue, null, "dt_HasDtls_Grd", new object[0], null, null, null);
                                                        DataRow[] rowArray = dt_HasDtls.Select("SubGridID = " + Conversions.ToString(sCbo.Grid_UID));
                                                        int num2 = rowArray.Length - 1;
                                                        frmTrnsIface frmTrnsIface = (frmTrnsIface)objectValue; ;

                                                        for (int i = 0; i <= num2; i++)
                                                        {
                                                            if (i == sCbo.CurrentCell.ColumnIndex)
                                                            {
                                                                if (rowArray[i]["ColDataType"].ToString() == "C")
                                                                {
                                                                    string sFormID = rowArray[i]["OpenForm"].ToString();
                                                                    if (Conversion.Val(sFormID) != 0.0)
                                                                    {
                                                                        NewLateBinding.LateSet(objectValue, null, "WindowState", new object[] { FormWindowState.Minimized }, null, null);
                                                                        EventHandles.ShowbyFormID(sFormID, RuntimeHelpers.GetObjectValue(objectValue), sCbo, sCbo.CurrentCell.ColumnIndex, sCbo.CurrentCell.RowIndex);
                                                                    }
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex3)
                        {
                            Navigate.logError(ex3.Message, ex3.StackTrace);
                        }
                    }

                    else if (e.Shift && e.Control && e.KeyCode == Keys.C)
                    {
                        try
                        {
                            if (objectValue != null)
                            {
                                Form frm = (Form)objectValue;
                                Control cntl = frm.ActiveControl;
                                bool isGridTextBox = false;
                                try
                                {
                                    CIS_DataGridViewEx.DataGridViewEx sCbo1 = (CIS_DataGridViewEx.DataGridViewEx)cntl.Parent.Parent;
                                    isGridTextBox = true;
                                }
                                catch { isGridTextBox = false; }
                                frmTrnsIface frmface = new frmTrnsIface();
                                object activeChild = (Form)Navigate.GetActiveChild();
                                frmface = (frmTrnsIface)activeChild;

                                if (frmface.blnFormAction != Enum_Define.ActionType.View_Record)
                                {
                                    if (cntl is TextBox || isGridTextBox == true)
                                    {
                                        if ((int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 0 || (int)NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null) == 1)
                                        {

                                            if (cntl is CIS_Textbox)
                                            {
                                                frmCalc calc = new frmCalc();
                                                calc.refParentControl = cntl;
                                                calc.isGrid = false;
                                                calc.ShowDialog();
                                            }
                                            else if (cntl is System.Windows.Forms.DataGridViewTextBoxEditingControl)
                                            {
                                                frmCalc calc = new frmCalc();
                                                calc.refParentControl = cntl;
                                                calc.isGrid = true;
                                                calc.ShowDialog();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                    else if (pnlDockBottom.Expand != true)
                    {
                        if ((e.KeyCode == Keys.F3) | (e.KeyCode == (Keys.Control | Keys.S)))
                        {
                            Db_Detials.IsSaveClicked = true;
                        }
                        Navigate.FormKeyCode(e.KeyCode);
                        Db_Detials.IsSaveClicked = false;
                    }
                }
            }
            catch { }
        }

        private void MenuItemOnClick_ShowForm(object sender, EventArgs e)
        {
            try
            {
                ToolStripItem item = (ToolStripItem)sender;
                EventHandles.ShowbyFormID(item.Name.ToString(), null, null, -1, -1);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuItemOnClick_AboutUs(object sender, EventArgs e)
        {
            MessageBox.Show("Crocus IT Solutions Pvt. Ltd.", "Crocus IT Solutions Pvt. Ltd.");
        }

        private void MenuItemOnClick_Exit(object sender, EventArgs e)
        {
            EventHandles.Closeform();
        }

        private void tbbtnNew_Click(object sender, EventArgs e)
        {
            Form cForm = null;
            Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);

        }

        private void tbbtnEdit_Click(object sender, EventArgs e)
        {
            Form cForm = null;
            Navigate.NavigateForm(Enum_Define.Navi_form.Edit_Record, ref cForm, true, false);
            if (pnlDockBottom.Expand == true)
            {
                pnlDockBottom.Expand = false;
                btnPrint.Visible = false;
            }
        }

        private void tbbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Db_Detials.IsSaveClicked = true;
                Form cForm = null;
                Navigate.NavigateForm(Enum_Define.Navi_form.Save_Record, ref cForm, true, false);
                Db_Detials.IsSaveClicked = false;

                frmTrnsIface frmface = new frmTrnsIface();
                object activeChild = (Form)Navigate.GetActiveChild();
                frmface = (frmTrnsIface)activeChild;
                if ((frmface.isGridmasterAddText == true) || (frmface.isComboAddText == true))
                {
                    if (frmface.IsMasterAdded == true)
                    {
                        Form activeForm = (Form)Navigate.GetActiveChild();
                        EventHandles.CloseWithoutMessage();
                    }
                }

                if (pnlDockBottom.Expand == true)
                    ShowSearchList(false);
            }
            catch { }
        }

        private void tbbtnCancel_Click(object sender, EventArgs e)
        {
            Form cForm = null;
            Navigate.NavigateForm(Enum_Define.Navi_form.Cancel_Record, ref cForm, true, false);
        }

        private void tbbtnDelete_Click(object sender, EventArgs e)
        {
            Form cForm = null;
            Navigate.NavigateForm(Enum_Define.Navi_form.Delete_Record, ref cForm, true, false);
        }

        private void tbbtnFind_Click(object sender, EventArgs e)
        {
            ShowSearchList(false);
        }

        private void tbbtnFirst_Click(object sender, EventArgs e)
        {
            Form cForm = null;
            Navigate.NavigateForm(Enum_Define.Navi_form.First_Record, ref cForm, true, false);
        }

        private void tbbtnPrevious_Click(object sender, EventArgs e)
        {
            Form cForm = null;
            Navigate.NavigateForm(Enum_Define.Navi_form.Previous_Record, ref cForm, true, false);
        }

        private void tbbtnNext_Click(object sender, EventArgs e)
        {
            Form cForm = null;
            Navigate.NavigateForm(Enum_Define.Navi_form.Next_Record, ref cForm, true, false);
        }

        private void tbbtnLast_Click(object sender, EventArgs e)
        {
            Form cForm = null;
            Navigate.NavigateForm(Enum_Define.Navi_form.Last_Record, ref cForm, true, false);
        }

        private void tbbtnPrint_Click(object sender, EventArgs e)
        {
            Form cForm = null;
            Navigate.NavigateForm(Enum_Define.Navi_form.Print_Record, ref cForm, true, false);
        }

        private void tbbtnEMail_Click(object sender, EventArgs e)
        {
            if (CommonCls.CheckForInternetConnection() == false)
            {
                Navigate.ShowMessage(CIS_DialogIcon.Warning, "Error", "Internet Not Connected! Email Cannot Be Send.");
                tbbtnEMail.Enabled = false;
                return;
            }
            else
            {
                tbbtnEMail.Enabled = true;
            }

            try
            {
                Form actObj = (Form)RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                if (actObj != null)
                {
                    Cursor = Cursors.WaitCursor;
                    dynamic obj = actObj;
                    string sstartPath = "";
                    string sVal = obj.txtEntryNo.Text;
                    string sID = obj.txtCode.Text;
                    string sReportName = "";
                    //string sReportID = ""; // string.Format("Select ReportID, FormName, SubreportID From tbl_ReportList Where ModuleID = {0} and FormName <> '-' ", obj.iIDentity);
                    //sReportID = DB.GetSnglValue(string.Format("Select ReportID From tbl_ReportList Where ModuleID = {0} and FormName <> '-' ", obj.iIDentity));

                    try
                    {
                        string _filename = string.Empty;
                        string _Password = string.Empty;
                        string _Host = string.Empty;
                        string _PortNo = string.Empty;
                        string _Signature = string.Empty;
                        string _FromEmail = string.Empty;
                        string _Subject = string.Empty;
                        string _ToAddress = string.Empty;
                        string sMailingConfigID = "";

                        DataGridView dgv_AttachFile = new DataGridView();
                        CIS_Textbox txtToAddress = new CIS_Textbox();
                        Cursor = Cursors.Default;

                        frmEmailAddressBook frmEmil = new frmEmailAddressBook();
                        frmEmil.isEmail = true;
                        frmEmil.isReportTool = true;
                        frmEmil.iIDentity = obj.iIDentity;
                        frmEmil.refControl = txtToAddress;
                        frmEmil.Id = Convert.ToInt32(sID);
                        frmEmil.ShowDialog();

                        Cursor = Cursors.WaitCursor;
                        _FromEmail = frmEmil.sFromEmailID;
                        _Subject = frmEmil.sSubject;
                        _ToAddress = frmEmil.sToEmailID;
                        sMailingConfigID = frmEmil.iMailingConfigID.ToString();
                        txtToAddress.Text = _ToAddress;

                        if (txtToAddress.Text.Trim().Length > 0)
                        {
                            using (IDataReader dr = DB.GetRS(string.Format("Select * from {0} Where MailingConfigID = {1} and CompID={2}", Db_Detials.tbl_MailingConfig, sMailingConfigID, Db_Detials.CompID)))
                            {
                                while (dr.Read())
                                {
                                    _Password = CommonLogic.UnmungeString(dr["Password"].ToString());
                                    _Host = dr["Host"].ToString();
                                    _PortNo = dr["PortNo"].ToString();
                                    _Signature = dr["Signature"].ToString();
                                }
                            }

                            //bool blnStatus = false;
                            string sRetVal = string.Empty;
                            string[] sRet = new string[1];
                            try
                            {
                                string[] strEmail = txtToAddress.Text.Split(';');
                                string sEmailID = "";
                                string sTransID = "";
                                string sMenuCaption = DB.GetSnglValue("SELECT Form_Caption from tbl_MenuMaster WHERE MenuID=" + obj.iIDentity);
                                if (strEmail.ToString().Length > 0)
                                {
                                    foreach (var sEmailIDs in strEmail)
                                    {
                                        string[] sEmailAddArr = sEmailIDs.ToString().Split(':');
                                        sEmailID = sEmailAddArr[0].ToString();
                                        sTransID = sEmailAddArr[1].ToString();

                                        string sRetVAl = CommonCls.GetReportDocument(sTransID, sVal, Convert.ToString(obj.iIDentity), 0);
                                        string[] sArrRt = sRetVAl.Split(';');
                                        sstartPath = sArrRt[0].ToString();
                                        sReportName = sArrRt[1].ToString();

                                        dgv_AttachFile.ColumnCount = 3;
                                        dgv_AttachFile.Rows.Clear();
                                        dgv_AttachFile.Rows.Add();

                                        dgv_AttachFile.Rows[0].Cells[0].Value = 1;
                                        dgv_AttachFile.Rows[0].Cells[1].Value = sstartPath + "//" + sMenuCaption + ".pdf";
                                        dgv_AttachFile.Rows[0].Cells[2].Value = Path.GetFileName(sstartPath + "//" + sMenuCaption + ".pdf");

                                        sRetVal = SendMail.sendMail((_Subject.Length > 0 ? _Subject : "Report -" + sReportName.Replace(" ", "")), _Signature, _FromEmail, _Password, sEmailID, "", dgv_AttachFile, _Host, _PortNo, false);
                                        sRet = sRetVal.Split(';');
                                        string sStatus = string.Empty;
                                    }
                                    if (sRet[0].ToString() == "True")
                                        Navigate.ShowMessage(CIS_DialogIcon.SecuritySuccess, "Success", "Mail Sent Successfully..");
                                    else
                                        Navigate.ShowMessage(CIS_DialogIcon.Warning, "Error", "Mail Sending failed");
                                }
                                else
                                {
                                    Navigate.ShowMessage(CIS_DialogIcon.Warning, "Error", "Please Enter To Email Address..");
                                }
                            }
                            catch (Exception ex1)
                            {
                                Navigate.logError(ex1.Message, ex1.StackTrace);
                                Navigate.ShowMessage(CIS_DialogIcon.Warning, "Error", "Mail Sending failed");
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        Navigate.logError(ex.Message, ex.StackTrace);
                        MessageBox.Show(ex.ToString());
                    }
                }

            }
            catch { }
            Application.DoEvents();
            Cursor = Cursors.Default;
        }

        private void tbbtnSMS_Click(object sender, EventArgs e)
        {
            if (CommonCls.CheckForInternetConnection() == false)
            {
                Navigate.ShowMessage(CIS_DialogIcon.Warning, "Error", "Internet Not Connected! SMS Cannot Be Send.");
                tbbtnSMS.Enabled = false;
                return;
            }
            else
            {
                tbbtnSMS.Enabled = true;
            }
            try
            {
                Form actObj = (Form)RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                if (actObj != null)
                {
                    Cursor = Cursors.WaitCursor;
                    dynamic obj = actObj;
                    string sVal = obj.txtEntryNo.Text;
                    string sID = obj.txtCode.Text;
                    try
                    {

                        DataGridView dgv_AttachFile = new DataGridView();
                        CIS_Textbox txtToAddress = new CIS_Textbox();
                        Cursor = Cursors.Default;

                        frmEmailAddressBook frmEmil = new frmEmailAddressBook();
                        frmEmil.isEmail = false;
                        frmEmil.isReportTool = false;
                        frmEmil.iIDentity = obj.iIDentity;
                        frmEmil.refControl = txtToAddress;
                        frmEmil.Id = Convert.ToInt32(sID);
                        frmEmil.isSendsms = false;
                        frmEmil.ShowDialog();

                        Cursor = Cursors.WaitCursor;

                        if (frmEmil.isSendsms == true)
                        {
                            if (frmEmil.isEmail == false)
                            {
                                string sMsg = "";
                                string sMessages = "";
                                string sMobileNo = "";

                                sMsg = DB.GetSnglValue(string.Format("SELECT Message From fn_SMSTemplate_Tbl() Where MenuID=" + Convert.ToString(obj.iIDentity)));
                                string strMessage = CommonCls.GetValueForSms(sID, Convert.ToString(obj.iIDentity));
                                string[] strAp = strMessage.Split(';');
                                sMessages = strAp[0].ToString();
                                sMobileNo = strAp[1].ToString();

                                if (sMsg.Length > 0)
                                {
                                    string sRetVal = SMSServer.SendSMS(sMobileNo, sMessages, Db_Detials.CompID);
                                    string[] Cmsg = sRetVal.Split(';');
                                    if (Cmsg[0] == "True")
                                    {
                                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecuritySuccess, "Success", "Message Sent Successfully");
                                        return;
                                    }
                                    else
                                    {
                                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Error", "Invalid Mobile Number/Numbers or Invalid Credentials or Inactive Account");
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex1)
                    {
                        Navigate.logError(ex1.Message, ex1.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
            Application.DoEvents();
            Cursor = Cursors.Default;
        }

        private void tbbtnSettings_Click(object sender, EventArgs e)
        {
            frmTrnsIface frm = new frmTrnsIface();
            uctrl_Setting = new CIS_FormSettings();
            uctrl_Setting.Dock = DockStyle.None;
            uctrl_Setting.SetMDIform = this.Name;
            uctrl_Setting.SetButtonCntrl = tbbtnChangeComp.Name;
            uctrl_Setting.SetIniFileNM = "FormSetting.ini";
            //this.Enabled = false;
            this.Controls.Add(uctrl_Setting);
        }

        private void tbbtnLogoffUser_Click(object sender, EventArgs e)
        {
            EventHandles.UpDateUserStatus();
            AppStart.IsBool = 1;
            this.Dispose();
        }

        private void tbbtnChangeComp_Click(object sender, EventArgs e)
        {
            try
            {
                int iactionType = Localization.ParseNativeInt(DB.GetSnglValue("select Miscid from fn_MiscMaster() Where MiscName='IsLogOut'"));
                DBSp.Log_CurrentUser(1, iactionType, 0, 0, 0, 0);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
            AppStart.IsBool = 2;
            this.Dispose();
        }

        private void tbbtnClose_Click(object sender, EventArgs e)
        {
            tbbtnChangeComp.Enabled = true;
            if (uctrl_Setting != null)
            {
                object instance = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                if (uctrl_Setting.Visible == true)
                {
                    dynamic objfrm = instance;
                    if (instance != null)
                    {
                        try
                        {
                            Console.WriteLine(RuntimeHelpers.GetObjectValue(objfrm.blnFormAction));
                            Navigate.EnableNavigate(0, Enum_Define.ActionType.Not_Active, ref instance);
                            ((DataTable)objfrm.dt_HasDtls_Grd).Clear();
                            ((DataTable)objfrm.dt_AryCalcvalue).Clear();
                            ((DataTable)objfrm.dt_AryIsRequired).Clear();
                        }
                        catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                        objfrm.Close();
                        objfrm.Dispose();
                    }
                    uctrl_Setting.Hide();
                    double dbliIDentity = Conversion.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(instance, null, "iIDentity", new object[0], null, null, null)));
                    EventHandles.ShowbyFormID(dbliIDentity.ToString(), null, null, -1, -1);
                    return;
                }
            }
            EventHandles.Closeform();
        }

        private void tbbtnExit_Click(object sender, EventArgs e)
        {
            MDIMain_FormClosing(null, null);
        }

        private void tbbtnCloseAll_Click(object sender, EventArgs e)
        {
            foreach (Form form in this.MdiChildren)
            {
                form.Close();
            }
        }

        public void ShowSearchList([Optional, DefaultParameterValue(false)] bool IsDetailsView)
        {
            GrdRef.Visible = false;
            btnFindShowDtls.Visible = true;
            IEnumerator enumerator = null;
            object instance = Application.OpenForms["MDIMain"];
            int num = 0;
            int IsSpOrFunction = 0;
            try
            {
                enumerator = ((IEnumerable)NewLateBinding.LateGet(instance, null, "MdiChildren", new object[0], null, null, null)).GetEnumerator();
                while (enumerator.MoveNext())
                {
                    Form current = (Form)enumerator.Current;
                    num++;
                }
            }
            finally
            {
                if (enumerator is IDisposable)
                {
                    (enumerator as IDisposable).Dispose();
                }
            }
            if (num == 0)
            {
                Label label = new Label();
                Label label2 = label;
                label2.Text = "Their Is No Active Child form(s).";
                label2.TextAlign = ContentAlignment.MiddleCenter;
                label2.Font = new Font("Verdana", 12f, FontStyle.Bold, GraphicsUnit.Point, 0);
                label2.Dock = DockStyle.Fill;
                label2 = null;
                grdSearch.Visible = false;
            }
            else
            {
                int sParaCount = 0;
                grdSearch.Visible = true;
                grdSearch.Name = "dgSearch";
                object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                string sql = string.Empty;
                int num2 = 0;
                sParaCount = Conversions.ToInteger(DB.GetSnglValue(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("SELECT  count(0) FROM INFORMATION_SCHEMA.PARAMETERS Where SPECIFIC_NAME= '", NewLateBinding.LateGet(objectValue, null, "sSearchQry", new object[0], null, null, null)), "'"))));
                if (!IsDetailsView)
                {
                    num2 = Conversions.ToInteger(DB.GetSnglValue(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("select Count(0) from sysobjects Where xtype = 'P' And [Name] = '", NewLateBinding.LateGet(objectValue, null, "sSearchQry", new object[0], null, null, null)), "'"))));
                    if (num2 > 0)
                    {
                        IsSpOrFunction = 1;
                        goto spFuncFound;
                    }
                    num2 = Conversions.ToInteger(DB.GetSnglValue(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("select Count(0) from sysobjects Where xtype = 'IF' And [Name] = '", NewLateBinding.LateGet(objectValue, null, "sSearchQry", new object[0], null, null, null)), "'"))));
                    if (num2 > 0)
                    {
                        IsSpOrFunction = 2;
                        goto spFuncFound;
                    }
                }
                else
                {
                    num2 = Conversions.ToInteger(DB.GetSnglValue(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("select Count(0) from sysobjects Where xtype = 'P' And [Name] = '", NewLateBinding.LateGet(objectValue, null, "sSearchQry_Dtls", new object[0], null, null, null)), "'"))));
                    if (num2 > 0)
                    {
                        IsSpOrFunction = 1;
                        goto spFuncFound;
                    }
                    num2 = Conversions.ToInteger(DB.GetSnglValue(Conversions.ToString(Operators.ConcatenateObject(Operators.ConcatenateObject("select Count(0) from sysobjects Where xtype = 'IF' And [Name] = '", NewLateBinding.LateGet(objectValue, null, "sSearchQry_Dtls", new object[0], null, null, null)), "'"))));
                    if (num2 > 0)
                    {
                        IsSpOrFunction = 2;
                        goto spFuncFound;
                    }
                }
            spFuncFound:
                try
                {

                    if (num2 == 0)
                    {
                        if (!IsDetailsView)
                        {
                            sql = string.Format(" sp_ExecQuery 'Select * From {0} Where IsDeleted=0' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "strTableName", new object[0], null, null, null)));
                        }
                        else
                        {
                            sql = string.Format(" sp_ExecQuery 'Select * From {0}'", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "strTableDtls", new object[0], null, null, null)));
                        }
                    }
                    else if (!IsDetailsView)
                    {
                        if (IsSpOrFunction == 1)
                        {
                            if (sParaCount == 2)
                                sql = string.Format(" sp_ExecQuery '{0} {1}, {2}' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry", new object[0], null, null, null)), Db_Detials.CompID, Db_Detials.YearID);
                            else if (sParaCount == 3)
                                sql = string.Format(" sp_ExecQuery '{0} {1}, {2},{3}' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry", new object[0], null, null, null)), VoucherTypeID, Db_Detials.CompID, Db_Detials.YearID);
                            else if (sParaCount == 4)
                                sql = string.Format(" sp_ExecQuery '{0} {1}, {2},{3},{4}' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry", new object[0], null, null, null)), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID);
                            else if (sParaCount == 6)
                                sql = string.Format(" sp_ExecQuery '{0} {1}, {2},{3},{4},{5},{6}' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry", new object[0], null, null, null)), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, CommonLogic.SQuote(Localization.ToVBDateString(Convert.ToString(Db_Detials.FinancialYearFrom))), CommonLogic.SQuote(Localization.ToVBDateString(Convert.ToString(Db_Detials.FinancialYearTo))));
                            else if (sParaCount == 7)
                                sql = string.Format(" sp_ExecQuery '{0} {1}, {2},{3},{4},{5},{6},{7}' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry", new object[0], null, null, null)), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, CommonLogic.SQuote(Localization.ToVBDateString(Convert.ToString(Db_Detials.FinancialYearFrom))), CommonLogic.SQuote(Localization.ToVBDateString(Convert.ToString(Db_Detials.FinancialYearTo))), VoucherTypeID);
                            else
                                sql = string.Format(" sp_ExecQuery '{0}' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry", new object[0], null, null, null)));
                        }
                        else if (IsSpOrFunction == 2)
                        {
                            if (sParaCount == 2)
                                sql = string.Format(" sp_ExecQuery 'Select * From {0}({1},{2})' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry", new object[0], null, null, null)), Db_Detials.CompID, Db_Detials.YearID);
                            else if (sParaCount == 3)
                                sql = string.Format(" sp_ExecQuery 'Select * From {0}({1},{2},{3})' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry", new object[0], null, null, null)), VoucherTypeID, Db_Detials.CompID, Db_Detials.YearID);
                            else if (sParaCount == 4)
                                sql = string.Format(" sp_ExecQuery 'Select * From {0} ({1}, {2},{3},{4})' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry", new object[0], null, null, null)), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID);
                            else if (sParaCount == 6)
                                sql = string.Format(" sp_ExecQuery 'Select * From {0} ({1}, {2},{3},{4},{5},{6})' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry", new object[0], null, null, null)), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, CommonLogic.SQuote(Localization.ToVBDateString(Convert.ToString(Db_Detials.FinancialYearFrom))), CommonLogic.SQuote(Localization.ToVBDateString(Convert.ToString(Db_Detials.FinancialYearTo))));
                            else if (sParaCount == 7)
                                sql = string.Format(" sp_ExecQuery 'Select * From {0} ({1}, {2},{3},{4},{5},{6},{7})' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry", new object[0], null, null, null)), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, CommonLogic.SQuote(Localization.ToVBDateString(Convert.ToString(Db_Detials.FinancialYearFrom))), CommonLogic.SQuote(Localization.ToVBDateString(Convert.ToString(Db_Detials.FinancialYearTo))), VoucherTypeID);
                            else
                                sql = string.Format(" sp_ExecQuery 'Select * From {0}()' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry", new object[0], null, null, null)));
                        }
                    }
                    else
                    {
                        if (IsSpOrFunction == 1)
                        {
                            if (sParaCount == 2)
                                sql = string.Format(" sp_ExecQuery '{0} {1}, {2}' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry_Dtls", new object[0], null, null, null)), Db_Detials.CompID, Db_Detials.YearID);
                            else if (sParaCount == 3)
                                sql = string.Format(" sp_ExecQuery '{0} {1}, {2},{3}' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry_Dtls", new object[0], null, null, null)), VoucherTypeID, Db_Detials.CompID, Db_Detials.YearID);
                            else if (sParaCount == 4)
                                sql = string.Format(" sp_ExecQuery '{0} {1}, {2},{3},{4}' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry_Dtls", new object[0], null, null, null)), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID);
                            else if (sParaCount == 6)
                                sql = string.Format(" sp_ExecQuery '{0} {1}, {2},{3},{4},{5},{6}' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry_Dtls", new object[0], null, null, null)), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, CommonLogic.SQuote(Localization.ToVBDateString(Convert.ToString(Db_Detials.FinancialYearFrom))), CommonLogic.SQuote(Localization.ToVBDateString(Convert.ToString(Db_Detials.FinancialYearTo))));
                            else if (sParaCount == 7)
                                sql = string.Format(" sp_ExecQuery '{0} {1}, {2},{3},{4},{5},{6},{7}' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry_Dtls", new object[0], null, null, null)), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, CommonLogic.SQuote(Localization.ToVBDateString(Convert.ToString(Db_Detials.FinancialYearFrom))), CommonLogic.SQuote(Localization.ToVBDateString(Convert.ToString(Db_Detials.FinancialYearTo))), VoucherTypeID);
                            else
                                sql = string.Format(" sp_ExecQuery '{0} ' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry_Dtls", new object[0], null, null, null)));
                        }
                        else if (IsSpOrFunction == 2)
                        {
                            if (sParaCount == 2)
                                sql = string.Format(" sp_ExecQuery 'Select * From {0}({1},{2})' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry_Dtls", new object[0], null, null, null)), Db_Detials.CompID, Db_Detials.YearID);
                            else if (sParaCount == 3)
                                sql = string.Format(" sp_ExecQuery 'Select * From {0}({1},{2},{3})' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry_Dtls", new object[0], null, null, null)), VoucherTypeID, Db_Detials.CompID, Db_Detials.YearID);
                            else if (sParaCount == 4)
                                sql = string.Format(" sp_ExecQuery 'Select * From {0} ({1}, {2},{3},{4})' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry_Dtls", new object[0], null, null, null)), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID);
                            else if (sParaCount == 6)
                                sql = string.Format(" sp_ExecQuery 'Select * From {0} ({1}, {2},{3},{4},{5},{6})' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry_Dtls", new object[0], null, null, null)), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, CommonLogic.SQuote(Localization.ToVBDateString(Convert.ToString(Db_Detials.FinancialYearFrom))), CommonLogic.SQuote(Localization.ToVBDateString(Convert.ToString(Db_Detials.FinancialYearTo))));
                            else if (sParaCount == 7)
                                sql = string.Format(" sp_ExecQuery 'Select * From {0} ({1}, {2},{3},{4},{5},{6},{7})' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry_Dtls", new object[0], null, null, null)), Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID, CommonLogic.SQuote(Localization.ToVBDateString(Convert.ToString(Db_Detials.FinancialYearFrom))), CommonLogic.SQuote(Localization.ToVBDateString(Convert.ToString(Db_Detials.FinancialYearTo))), VoucherTypeID);
                            else
                                sql = string.Format(" sp_ExecQuery 'Select * From {0}()' ", RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "sSearchQry_Dtls", new object[0], null, null, null)));
                        }
                    }
                }
                catch (Exception exception1)
                {
                    ProjectData.SetProjectError(exception1);
                    Exception exception = exception1;
                    Navigate.logError(exception1.Message, exception1.StackTrace);
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Error", "Query for find record(s) is missing.");
                    ProjectData.ClearProjectError();
                }
                if (sql.Length > 0)
                {
                    grdSearch.DataSource = DB.GetDT(sql, true);
                }
                bool flag = true;
                BandEnumerator enumerator2 = grdSearch.DisplayLayout.Bands.GetEnumerator();
                while (enumerator2.MoveNext())
                {
                    ColumnEnumerator enumerator3 = enumerator2.Current.Columns.GetEnumerator();
                    while (enumerator3.MoveNext())
                    {
                        UltraGridColumn column = enumerator3.Current;
                        if (flag)
                        {
                            column.Hidden = true;
                            Debug.Print(column.Header.Caption);
                            flag = false;
                        }
                        if (((num2 == 0) & IsDetailsView) & (column.Index == 0))
                        {
                            flag = true;
                        }
                        column.CellActivation = Activation.NoEdit;
                    }
                }
                pnlDockBottom.Expand = true;
            }

            if (grdSearch.Rows.Count > 0)
            {
                grdSearch.Focus();
                grdSearch.Rows[0].Cells[1].Selected = true;
                grdSearch.Rows[0].Cells[1].Activated = true;
                btnPrint.Visible = true;
            }
        }

        public void ShowSearchRefList()
        {
            try
            {
                IEnumerator enumerator = null;
                object instance = Application.OpenForms["MDIMain"];
                int num = 0;
                btnFindShowDtls.Visible = false;
                try
                {
                    enumerator = ((IEnumerable)NewLateBinding.LateGet(instance, null, "MdiChildren", new object[0], null, null, null)).GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        Form current = (Form)enumerator.Current;
                        num++;
                    }
                }
                finally
                {
                    if (enumerator is IDisposable)
                    {
                        (enumerator as IDisposable).Dispose();
                    }
                }

                GrdRef.Visible = true;
                grdSearch.Visible = false;
                GrdRef.Name = "PIECE NO REFERENCE";
                object activeChild = (Form)Navigate.GetActiveChild();
                double iIDentity = Conversion.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(activeChild, null, "iIDentity", new object[0], null, null, null)));
                CIS_DataGridViewEx.DataGridViewEx fgdtls = new CIS_DataGridViewEx.DataGridViewEx();
                fgdtls = ((CIS_DataGridViewEx.DataGridViewEx)NewLateBinding.LateGet(activeChild, null, "fgDtls", new object[0], null, null, null));
                int RowIndex = fgdtls.CurrentRow.Index;
                int rowRef = RowIndex + 1;
                string RefID = string.Empty;

                int icolRef = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select ColIndex from tbl_GridSettings Where Series=" + frmTrnsIface.VoucherTypeID + " and GridId=" + iIDentity + " and SubGridID=" + fgdtls.Grid_UID + " and ColFields='RefID'")));

                if (iIDentity == 163)
                {
                    RefID = fgdtls.Rows[RowIndex].Cells[icolRef].Value.ToString() + "|" + rowRef;
                }
                else
                {
                    RefID = fgdtls.Rows[RowIndex].Cells[icolRef].Value.ToString();
                }
                int iParentID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select ParentID From fn_Menumaster_tbl() Where MenuID=" + iIDentity + "")));
                if (iParentID == 22)
                {
                    GrdRef.DataSource = DB.GetDT("Select * from fn_FetchFabricRefIDRefDtls(" + Db_Detials.CompID + "," + Db_Detials.YearID + "," + CommonLogic.SQuote(RefID) + "," + iIDentity + ")", true);
                }
                else if (iParentID == 19)
                {
                    GrdRef.DataSource = DB.GetDT("Select * from fn_FetchItemRefIDRefDtls(" + Db_Detials.CompID + "," + Db_Detials.YearID + "," + CommonLogic.SQuote(RefID) + "," + iIDentity + ")", true);
                }
                else if (iParentID == 20)
                {
                    GrdRef.DataSource = DB.GetDT("Select * from fn_FetchYarnRefIDRefDtls(" + Db_Detials.CompID + "," + Db_Detials.YearID + "," + CommonLogic.SQuote(RefID) + "," + iIDentity + ")", true);
                }
                else if (iParentID == 21)
                {
                    GrdRef.DataSource = DB.GetDT("Select * from fn_FetchBeamRefIDRefDtls(" + Db_Detials.CompID + "," + Db_Detials.YearID + "," + CommonLogic.SQuote(RefID) + "," + iIDentity + ")", true);
                }
                bool flag = true;
                BandEnumerator enumerator2 = GrdRef.DisplayLayout.Bands.GetEnumerator();
                while (enumerator2.MoveNext())
                {
                    ColumnEnumerator enumerator3 = enumerator2.Current.Columns.GetEnumerator();
                    while (enumerator3.MoveNext())
                    {
                        UltraGridColumn column = enumerator3.Current;
                        if (flag)
                        {
                            column.Hidden = true;
                            Debug.Print(column.Header.Caption);
                            flag = false;
                        }
                        column.CellActivation = Activation.NoEdit;
                    }
                }
                pnlDockBottom.Expand = true;
                if (GrdRef.Rows.Count > 0)
                {
                    GrdRef.Focus();
                    GrdRef.Rows[0].Cells[1].Selected = true;
                    GrdRef.Rows[0].Cells[1].Activated = true;
                    btnPrint.Visible = false;
                }
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
        }

        private void btnFindShowDtls_Click(object sender, EventArgs e)
        {
            if (btnFindShowDtls.Text == "Show Details")
            {
                btnFindShowDtls.Text = "Show Main";
                pnlDockBottom.Expand = true;
                ShowSearchList(true);
            }
            else if (btnFindShowDtls.Text == "Show Main")
            {
                btnFindShowDtls.Text = "Show Details";
                pnlDockBottom.Expand = true;
                ShowSearchList(false);
            }
        }

        private void grdSearch_AfterCellActivate(object sender, EventArgs e)
        {
            try
            {
                object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                if ((objectValue != null) && Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null), Enum_Define.ActionType.View_Record, false))
                {
                    UltraGridRow activeRow = grdSearch.ActiveRow;
                    if (activeRow.Index >= 0)
                    {
                        Navigate.ShowbyID(objectValue, "[" + ((DataTable)NewLateBinding.LateGet(objectValue, null, "ds", new object[0], null, null, null)).Columns[0].ColumnName + "]", (int)Math.Round(Conversion.Val(activeRow.Cells[0].Text)));
                    }
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
                Navigate.logError(exception1.Message, exception1.StackTrace);
            }
        }

        private void grdSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                    if ((objectValue != null) && Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null), Enum_Define.ActionType.View_Record, false))
                    {
                        UltraGridRow activeRow = grdSearch.ActiveRow;
                        if (!grdSearch.Rows.FilterRow.Cells[0].Activated)
                        {
                            grdSearch.Rows.FilterRow.Cells[0].Activate();
                            grdSearch.PerformAction(Infragistics.Win.UltraWinGrid.UltraGridAction.EnterEditMode);
                            grdSearch.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextCell;
                        }
                        else
                        {
                            if (activeRow.Index >= 0)
                            {
                                Navigate.ShowbyID(objectValue, "[" + ((DataTable)NewLateBinding.LateGet(objectValue, null, "ds", new object[0], null, null, null)).Columns[0].ColumnName + "]", (int)Math.Round(Conversion.Val(activeRow.Cells[0].Text)));
                                pnlDockBottom.Expand = false;
                                tblpnl_HelpText.Visible = true;
                            }
                            else
                            {
                                tblpnl_HelpText.Visible = false;
                            }
                        }
                    }
                    else if ((((e.KeyCode == Keys.Right) || (e.KeyCode == Keys.Left)) || (e.KeyCode == Keys.Down)) || (e.KeyCode == Keys.Up))
                    {
                    }

                }
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
        }

        private void grdSearch_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                if ((objectValue != null) && Operators.ConditionalCompareObjectEqual(NewLateBinding.LateGet(objectValue, null, "blnFormAction", new object[0], null, null, null), Enum_Define.ActionType.View_Record, false))
                {
                    UltraGridRow activeRow = grdSearch.ActiveRow;
                    if (activeRow.Index >= 0)
                    {
                        Navigate.ShowbyID(objectValue, "[" + ((DataTable)NewLateBinding.LateGet(objectValue, null, "ds", new object[0], null, null, null)).Columns[0].ColumnName + "]", (int)Math.Round(Conversion.Val(activeRow.Cells[0].Text)));
                        //pnlDockBottom.Expand = false;
                        //tblpnl_HelpText.Visible = true;
                    }
                }
                else
                {
                    tblpnl_HelpText.Visible = false;
                }

            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
        }

        public static bool CheckforEdit(object frm)
        {
            bool flag;
            try
            {
                flag = DeletePro.CheckAndEdit(ref frm);
            }
            catch (Exception exception1)
            {
                Exception exception = exception1;
                flag = true;
                return flag;
            }
            return flag;
        }

        public static void DeleteforModule(object frm)
        {
            try
            {
                DeletePro.CheckAndDelete(ref frm);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                ProjectData.ClearProjectError();
                Navigate.logError(exception1.Message, exception1.StackTrace);
            }
        }

        private void txtQuickMenu_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    picSearchMenu_Click(null, null);
                }

                catch { }
            }
        }

        private void picSearchMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtQuickMenu.Text != "")
                {
                    string sMenuID = DB.GetSnglValue("select MenuID From tbl_MenuMaster  WHERE [Form_Caption]='" + txtQuickMenu.Text.Trim() + "'");
                    if (Localization.ParseNativeInt(sMenuID) != 0)
                    {
                        EventHandles.ShowbyFormID(sMenuID, null, null, -1, -1);
                        txtQuickMenu.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void TS_Datetimepnl_Click(object sender, EventArgs e)
        {
            frmAbout frm = new frmAbout();
            frm.ShowDialog();
        }

        private void pnlDockLeft_ExpandClick(object sender, EventArgs e)
        {
            ExpandCollapsePnl("L");
        }

        private void pnlDockRight_ExpandClick(object sender, EventArgs e)
        {
            ExpandCollapsePnl("R");
        }

        private void ExpandCollapsePnl(string sPanel)
        {
            if ((_originalSettings.Width > 1280) && (_originalSettings.Height >= 768))
            {
                if (sPanel == "L")
                {
                    if (pnlDockLeft.Expand == true)
                    {
                        pnlDockLeft.Width = 280;
                    }
                }
                else
                {
                    pnlDockLeft.Expand = false;
                    pnlDockLeft.Width = 280;
                }
            }
            else
            {
                if (sPanel == "L")
                {
                    if (pnlDockLeft.Expand == true)
                    {
                        pnlDockLeft.Width = 200;
                    }
                }
                else
                {
                    if (pnlDockLeft.Expand == true)
                        pnlDockLeft.Expand = false;
                }
            }
        }

        private void SaveColumnSettings(Control[] arr, double identity)
        {
            string strQry = string.Empty;
            CIS_DataGridViewEx.DataGridViewEx ex = null;
            object obj = this.ActiveMdiChild;
            DataTable dt = (DataTable)RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(obj, null, "dt_AryIsRequired", new object[0], null, null, null));
            foreach (Control c in arr)
            {
                int i = 0;
                ex = (CIS_DataGridViewEx.DataGridViewEx)c;
                if (ex != null)
                {
                    if (ex.Columns.Count > 0)
                    {
                        DataRow[] Drow = dt.Select("SubGridID=" + ex.Grid_UID);

                        foreach (DataRow dr in Drow)
                        {
                            if (Localization.ParseBoolean(dr["IsRequired"].ToString()))
                            {
                                if (ex.Columns[i].HeaderText != "")
                                {
                                    if (ex.Columns[i].Visible)
                                    {
                                        strQry += string.Format("Update tbl_gridsettings Set IsHidden=0 Where GridID={0} and SubGridID={1} and ColHeading={2}" + Environment.NewLine, identity, ex.Grid_UID, CommonLogic.SQuote(ex.Columns[i].HeaderText));
                                    }
                                    else
                                    {
                                        strQry += string.Format("Update tbl_gridsettings Set IsHidden=1 Where GridID={0} and SubGridID={1} and ColHeading={2}" + Environment.NewLine, identity, ex.Grid_UID, CommonLogic.SQuote(ex.Columns[i].HeaderText));
                                    }
                                }
                            }
                            i++;
                        }
                        DB.ExecuteSQL(strQry);
                    }
                }
            }
        }

        private void FillCompanies()
        {
            try
            {
                DataTable dt = DB.GetDT("Select CompanyId,CompanyName From tbl_CompanyMaster Where IsDeleted=0 and CompanyID in (Select CompID from tbl_UserMasterDtls where UserID=" + Db_Detials.UserID + ")", false);
                dgvCompSelect.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgvCompSelect.Rows.Add();
                        dgvCompSelect.Rows[i].Cells[0].Value = dt.Rows[i]["CompanyId"].ToString();
                        dgvCompSelect.Rows[i].Cells[1].Value = dt.Rows[i]["CompanyName"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void FillUserGrid()
        {
            try
            {
                TS_Userpnl.Text = DB.GetSnglValue("Select UserName From fn_UserMaster_tbl() Where UserID=" + Db_Detials.UserID + " and IsLoggedIn=1");

                DataTable dt = DB.GetDT("select UserID,UserName,EmployeeName,IsActive, IPAddress from fn_UserMaster_tbl() WHERE  IsLoggedIn=1", false);
                dgvUserLogin.Rows.Clear();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        dgvUserLogin.Rows.Add();

                        dgvUserLogin.Rows[i].Cells[0].Value = dt.Rows[i]["UserID"].ToString();
                        dgvUserLogin.Rows[i].Cells[1].Value = (dt.Rows[i]["EmployeeName"].ToString() == "" ? dt.Rows[i]["UserName"].ToString() : dt.Rows[i]["EmployeeName"].ToString());
                        dgvUserLogin.Rows[i].Cells[2].Value = dt.Rows[i]["IPAddress"].ToString();
                        //dgvUserLogin.Rows[i].Selected = false;
                        dgvUserLogin.Rows[i].DefaultCellStyle.SelectionBackColor = Color.White;
                        dgvUserLogin.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Black;

                        if (Db_Detials.UserID == Localization.ParseNativeInt(dt.Rows[i]["UserID"].ToString()))
                        {
                            dgvUserLogin.Rows[i].ReadOnly = true;
                            dgvUserLogin.Rows[i].Selected = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void dgvCompSelect_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    if (dgvCompSelect.Rows[e.RowIndex].Cells[1].Value.ToString() != "")
                    {
                        object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                        if (objectValue == null)
                        {
                            if (CIS_Dialog.Show("Are you sure you want to switch the company..?", GetAssemblyInfo.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                try
                                {

                                    Cursor = Cursors.WaitCursor;
                                    Db_Detials.CompID = Localization.ParseNativeInt(dgvCompSelect.Rows[e.RowIndex].Cells[0].Value.ToString());
                                    Db_Detials.CompanyName = dgvCompSelect.Rows[e.RowIndex].Cells[1].Value.ToString();
                                    TS_Companypnl.Text = Db_Detials.CompanyName;
                                    this.Text = Db_Detials.CompanyName + " (Year " + DB.GetSnglValue("Select YrDesc From tbl_YearMaster Where YearID = " + Db_Detials.YearID) + ")";
                                    this.mnuMain.Items.Clear();
                                    CommonCls.LoadMDIMenu();
                                    CommonCls.LoadMDIMenu_NoHash();

                                    LoadMenu Loadmnu = new LoadMenu();
                                    Loadmnu.BuildAutocompleteMenu(cboQuickMenu, Db_Detials.UserType, Db_Detials.CompID);
                                    LoadGlobalVariables();
                                    Cursor = Cursors.Default;
                                }
                                catch { Cursor = Cursors.Default; }
                            }
                        }
                        else
                        {
                            Navigate.ShowMessage(CIS_DialogIcon.Information, "Info", "Please close all the open forms before changing company..");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void VisitWebSite_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("IExplore.exe", @"http://crocusitsolutions.com");
        }

        private void tmronlineuser_Tick(object sender, EventArgs e)
        {
            if (CheckConnection() == true)
            {
                try
                {
                    ApplyVoucherTypeID();
                }
                catch { }
                LoadGlobalVariables();
                bool bIsGlobalVariableUpdated = Localization.ParseBoolean(DB.GetSnglValue(string.Format("Select ConfigValue From tbl_Appconfig Where ConfigName='GlobalVariable_Update'")));
                if (bIsGlobalVariableUpdated)
                {
                    try
                    {
                        DB.ExecuteSQL(string.Format("Update  tbl_AppConfig Set ConfigValue='false' where Name='GlobalVariable_Update'"));
                    }
                    catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                    ultraDesktopAlert1.AutoClose = Infragistics.Win.DefaultableBoolean.False;
                    ultraDesktopAlert1.Show("News!!!", "Global Feature of your software has been changed.");
                }
                FillUserGrid();
                checksession();
                CheckLogOffTime();
            }
        }

        private void tmr_Messanger_Tick(object sender, EventArgs e)
        {
            if (CheckConnection() == true)
            {
                FillUserGrid();
                FillReminder();
                FillTask();
            }
        }

        private void dgvUserLogin_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {

                    DataGridView.HitTestInfo info = this.dgvUserLogin.HitTest(e.X, e.Y);
                    if (Db_Detials.UserID != Localization.ParseNativeInt(this.dgvUserLogin.Rows[info.RowIndex].Cells[0].Value.ToString()))
                    {
                        ContextMenuStrip strip = new ContextMenuStrip();
                        ToolStripMenuItem item = new ToolStripMenuItem("Update");
                        item.Click += new EventHandler(this.mnuUpdate_Click);
                        strip.Items.AddRange(new ToolStripItem[] { item });
                        this.dgvUserLogin.ContextMenuStrip = strip;
                        this.dgvUserLogin.ContextMenuStrip.Enabled = true;
                        if (info.Type == DataGridViewHitTestType.Cell)
                        {
                            this.currentMouseOverRow = info.RowIndex;
                        }
                        else
                        {
                            item.Enabled = false;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Navigate.logError(exception.Message, exception.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", exception.Message);
            }
        }

        private void mnuUpdate_Click(object sender, EventArgs e)
        {
            if (Db_Detials.UserID != Localization.ParseNativeInt(this.dgvUserLogin.Rows[currentMouseOverRow].Cells[0].Value.ToString()))
            {
                frmChat fchat = new frmChat
                {
                    _UserID_To = this.dgvUserLogin.Rows[currentMouseOverRow].Cells[0].Value.ToString(),
                    _ipAddress_To = this.dgvUserLogin.Rows[currentMouseOverRow].Cells[2].Value.ToString(),
                };

                UserID_To = fchat._UserID_To;
                ipAddress_To = fchat._ipAddress_To;

                if ((fchat._UserID_To != "0") && (fchat._ipAddress_To != "0"))
                {
                    fchat.Show();
                }
            }
        }

        public string UserID_To;
        public string ipAddress_To;

        private void dgvUserLogin_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Db_Detials.UserID != Localization.ParseNativeInt(this.dgvUserLogin.Rows[e.RowIndex].Cells[0].Value.ToString()))
            {
                frmChat fchat = new frmChat
                {
                    _UserID_To = this.dgvUserLogin.Rows[e.RowIndex].Cells[0].Value.ToString(),
                    _ipAddress_To = this.dgvUserLogin.Rows[e.RowIndex].Cells[2].Value.ToString(),
                };

                UserID_To = fchat._UserID_To;
                ipAddress_To = fchat._ipAddress_To;

                if ((fchat._UserID_To != "0") && (fchat._ipAddress_To != "0"))
                {
                    fchat.Show();
                }
            }
        }

        public Socket socketReceive = null;
        public Socket socketSent = null;
        public IPEndPoint ipReceive = null;
        public IPEndPoint ipSent = null;
        public Socket chat = null;
        public static Thread tBroadCast;

        private void ReceiveNews()
        {
            try
            {
                ipReceive = new IPEndPoint(
                  Dns.GetHostEntry(Dns.GetHostName()).AddressList[0],
                  8001);

                socketReceive = new Socket(ipReceive.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);

                try
                {
                    socketReceive.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                    socketReceive.Bind(ipReceive);
                    socketReceive.Listen(1024);
                }
                catch { }
            }
            catch (Exception err)
            {
                Navigate.logError(err.Message, err.StackTrace);
                MessageBox.Show(err.Message);
            }

            byte[] buff = new byte[1024];
            while (true)
            {
                try
                {
                    Socket chat = socketReceive.Accept();
                    frmChat message = (frmChat)Navigate.GetForm_byName("frmChat");
                    if (message == null)
                    { Thread newThread = new Thread(new ThreadStart(showBalloonNotification)); this.chat = chat; newThread.Start(); }
                    else
                    {
                        ChatSession cs = new ChatSession(chat);
                        Thread newThread = new Thread(new ThreadStart(cs.StartChat)); newThread.Start();
                    }
                }
                catch { }
            }
        }

        private void showBalloonNotification()
        {
            string sUserName = DB.GetSnglValue("SELECT EmployeeName from tbl_UserMaster WHERE UserID = " + Db_Detials.UserID);
            notifyIcon1.Visible = true;
            notifyIcon1.ShowBalloonTip(5000, "Message Received", "Message from " + sUserName, ToolTipIcon.Info);
        }

        private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
        {
            ChatSession cs = new ChatSession(chat);
            cs.StartChat();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        #region Reminder
        private void FillReminder()
        {
            using (DataTable dt = DB.GetDT("Select * from tbl_Reminder where UserID=" + Db_Detials.UserID + " and CONVERT(DATETIME, ToDt)>=" + CommonLogic.SQuote(Localization.ToSqlDateString(DateTime.Now.Date.ToString())), false))
            {
                dgrd_Reminder.RowCount = dt.Rows.Count;
                dgrd_Reminder.ColumnCount = 3;

                this.dgrd_Reminder.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DateTime FromDate = Localization.ParseNativeDateTime(dt.Rows[i]["FromDt"].ToString());
                    DateTime ToDate = Localization.ParseNativeDateTime(dt.Rows[i]["TODt"].ToString());
                    Image icell; ;

                    if (Localization.ParseNativeDateTime(DateTime.Now.Date.AddDays(1).ToString()) >= ToDate)
                        icell = CIS_Textil.Properties.Resources.ricon;//high
                    else if (Localization.ParseNativeDateTime(DateTime.Now.Date.AddDays(2).ToString()) >= ToDate)
                        icell = CIS_Textil.Properties.Resources.gicon;//medium
                    else
                        icell = CIS_Textil.Properties.Resources.yicon;//low


                    string sdate = "";
                    sdate = Localization.ToVBDateString(dt.Rows[i]["TODt"].ToString());

                    dgrd_Reminder.Rows[i].Cells[0].Value = (System.Drawing.Image)icell;
                    dgrd_Reminder.Rows[i].Cells[1].Value = dt.Rows[i]["Topic"].ToString() + Environment.NewLine + sdate;

                    dgrd_Reminder.Rows[i].Cells[2].Value = dt.Rows[i]["ReminderID"].ToString();
                    dgrd_Reminder.Columns[2].Visible = false;
                }
            }
        }

        private void dgrd_Reminder_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }

        private void lblAddMore_Reminder_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string sMenuID = DB.GetSnglValue("select MenuID From tbl_MenuMaster  WHERE FormCall='frmReminder'");
                if (Localization.ParseNativeInt(sMenuID) != 0)
                {
                    EventHandles.ShowbyFormID(sMenuID, null, null, -1, -1, 0);
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void dgrd_Reminder_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //int iReminderID = Localization.ParseNativeInt(dgrd_Reminder.Rows[e.RowIndex].Cells[2].Value.ToString());
            //try
            //{
            //    string sMenuID = DB.GetSnglValue("select MenuID From tbl_MenuMaster  WHERE FormCall='frmReminder'");
            //    if (Localization.ParseNativeInt(sMenuID) != 0)
            //    {
            //        EventHandles.ShowbyFormID(sMenuID, null, null, -1, -1, iReminderID);
            //    }
            //}
            //catch { }
        }
        #endregion

        #region Task
        private void FillTask()
        {
            using (DataTable dt = DB.GetDT("Select * from fn_ToDoList(" + Db_Detials.CompID + "," + Db_Detials.YearID + ") where PerCompleted<>100 and UserID=" + Db_Detials.UserID + "", false))
            {
                dgrd_Task.RowCount = dt.Rows.Count;
                dgrd_Task.ColumnCount = 3;

                this.dgrd_Task.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Image icell; ;

                    if (dt.Rows[i]["Priority"].ToString() == "High")
                        icell = CIS_Textil.Properties.Resources.ricon;//high
                    else if (dt.Rows[i]["Priority"].ToString() == "Medium")
                        icell = CIS_Textil.Properties.Resources.gicon;//medium
                    else
                        icell = CIS_Textil.Properties.Resources.yicon;//low


                    dgrd_Task.Rows[i].Cells[0].Value = (System.Drawing.Image)icell;
                    dgrd_Task.Rows[i].Cells[1].Value = dt.Rows[i]["TaskName"].ToString();
                    dgrd_Task.Rows[i].Cells[2].Value = dt.Rows[i]["TaskAssignID"].ToString();
                    dgrd_Task.Columns[2].Visible = false;
                }
            }
        }

        private void lbllnk_addmore_Task_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string sMenuID = DB.GetSnglValue("select MenuID From tbl_MenuMaster  WHERE FormCall='frmToDoList'");
                if (Localization.ParseNativeInt(sMenuID) != 0)
                {
                    EventHandles.ShowbyFormID(sMenuID, null, null, -1, -1);
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void dgrd_Task_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.Cancel = true;
        }
        #endregion

        private void btnPrint_Click(object sender, EventArgs e)
        {
            this.PrintReport();
        }

        private void PrintReport()
        {
            this.grdSearch.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.False;
            this.grdSearch.DisplayLayout.Override.AllowRowSummaries = AllowRowSummaries.False;
            this.grdSearch.DisplayLayout.Override.RowSelectorHeaderStyle = RowSelectorHeaderStyle.None;
            this.grdSearch.DisplayLayout.Override.HeaderStyle = HeaderStyle.Standard;
            BandEnumerator ex = this.grdSearch.DisplayLayout.Bands.GetEnumerator();
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
            this.UPPrev.ShowDialog(this.grdSearch);
            BandEnumerator colEnum = this.grdSearch.DisplayLayout.Bands.GetEnumerator();
            while (colEnum.MoveNext())
            {
                ColumnEnumerator colFiltr = colEnum.Current.Columns.GetEnumerator();
                while (colFiltr.MoveNext())
                {
                    colFiltr.Current.Header.Appearance.BackColor = Color.White;
                }
            }

            this.grdSearch.DisplayLayout.Override.AllowRowFiltering = DefaultableBoolean.True;
            this.grdSearch.DisplayLayout.Override.AllowRowSummaries = AllowRowSummaries.BasedOnDataType;
            this.grdSearch.DisplayLayout.Override.RowSelectorHeaderStyle = RowSelectorHeaderStyle.ColumnChooserButton;
            this.grdSearch.DisplayLayout.Override.HeaderStyle = HeaderStyle.Default;
            this.grdSearch.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
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
                        }
                    }
                    catch { }
                }
            }
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

        private void SetupPrint(CancelablePrintEventArgs e)
        {
            object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
            dynamic frmobj = objectValue;
            int formID = frmobj.iIDentity;
            string FormName = DB.GetSnglValue("Select Form_Caption from tbl_MenuMaster Where MenuID=" + formID + "");
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
                    strQry = Environment.NewLine + FormName + " Register" + Environment.NewLine;
                    //strQry += Environment.NewLine + "From Date : " + Localization.ToVBDateString(this.dtFrom.Text) + " To " + Localization.ToVBDateString(this.dtTo.Text);
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

        private void grdSearch_InitializePrint(object sender, CancelablePrintEventArgs e)
        {
            this.SetupPrint(e);
        }

        private void pnlDockBottom_PanelCollapsing(object sender, XPanderStateChangeEventArgs e)
        {
            object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
            if (objectValue != null)
            {
                tblpnl_HelpText.Visible = true;
            }
            else
            {
                tblpnl_HelpText.Visible = false;
            }
        }

        private void pnlDockBottom_PanelExpanding(object sender, XPanderStateChangeEventArgs e)
        {
            tblpnl_HelpText.Visible = false;
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            string strQry = string.Empty;

            strQry += string.Format("Update tbl_UserMaster Set Password='" + CommonLogic.MungeString(txtPassword.Text.Trim()) + "'" + " Where Userid=" + Db_Detials.UserID + ";");
            strQry += string.Format("Update tbl_UserMaster Set EmployeeName='" + txtEmployeeName.Text + "'" + " Where Userid=" + Db_Detials.UserID + ";");
            strQry += string.Format("Update tbl_UserMaster Set IsModified=" + "1" + " Where Userid=" + Db_Detials.UserID + ";");
            strQry += string.Format("Update tbl_UserMaster Set UpdatedOn='" + Localization.ToSqlDateString(DateTime.Now.ToString()) + "'" + " Where Userid=" + Db_Detials.UserID + ";");
            strQry += string.Format("Update tbl_UserMaster Set UpdatedBy='" + Db_Detials.UserID + "'" + " Where Userid=" + Db_Detials.UserID + ";");
            if (strQry != "")
            {
                try
                {
                    DB.ExecuteSQL(strQry);
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "User Updated Successfully");
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Error Occured While Updating User");
                }
            }
        }

        private void TS_Userpnl_Click(object sender, EventArgs e)
        {
            //pnlDockLeft.Expand = true;
            //this.dataGridViewEx1.ColumnHeadersVisible = true;
            //this.dataGridViewEx1.Visible = true;
            //this.dataGridViewEx1.Location = new System.Drawing.Point(610, 317);
        }

        private void fgdtls_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            if (e != null)
            {
                e.Layout.Override.RowSizing = RowSizing.Free;
                e.Layout.Bands[0].AutoPreviewEnabled = true;
                e.Layout.Override.FilterUIType = FilterUIType.FilterRow;
                e.Layout.Override.FilterOperandStyle = FilterOperandStyle.Combo;
                e.Layout.Override.FilterEvaluationTrigger = FilterEvaluationTrigger.OnCellValueChange;
                e.Layout.Override.FilterOperatorLocation = FilterOperatorLocation.WithOperand;
                e.Layout.Override.FilterOperatorDefaultValue = FilterOperatorDefaultValue.StartsWith;
                e.Layout.Override.FilterClearButtonLocation = FilterClearButtonLocation.RowAndCell;
                e.Layout.Override.FilterRowAppearance.BackColor = Color.LightYellow;
                e.Layout.Override.FilterRowPromptAppearance.BackColorAlpha = Infragistics.Win.Alpha.Opaque;
                e.Layout.Override.SpecialRowSeparator = SpecialRowSeparator.FilterRow;
                e.Layout.Override.SpecialRowSeparatorAppearance.BackColor = Color.FromArgb(0xe9, 0xf2, 0xc7);
                e.Layout.Override.AllowRowSummaries = AllowRowSummaries.True;
                e.Layout.Override.SummaryDisplayArea = SummaryDisplayAreas.BottomFixed;
                e.Layout.Override.SummaryDisplayArea |= SummaryDisplayAreas.GroupByRowsFooter;
                e.Layout.Override.SummaryDisplayArea |= SummaryDisplayAreas.InGroupByRows;
                e.Layout.Override.GroupBySummaryDisplayStyle = GroupBySummaryDisplayStyle.SummaryCells;
                e.Layout.Override.SummaryFooterAppearance.BackColor = SystemColors.Info;
                e.Layout.Override.SummaryValueAppearance.BackColor = SystemColors.Window;
                e.Layout.Override.SummaryValueAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                e.Layout.Override.GroupBySummaryValueAppearance.BackColor = SystemColors.Window;
                e.Layout.Override.GroupBySummaryValueAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
                e.Layout.Bands[0].SummaryFooterCaption = "Grand Totals:";
                e.Layout.Override.SummaryFooterCaptionAppearance.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
                e.Layout.Override.SummaryFooterCaptionVisible = Infragistics.Win.DefaultableBoolean.False;
                e.Layout.Override.SpecialRowSeparator = SpecialRowSeparator.SummaryRow;
                e.Layout.Override.SpecialRowSeparatorAppearance.BackColor = Color.FromArgb(0xda, 0xd9, 0xf1);
                e.Layout.Override.SpecialRowSeparatorHeight = 6;
                e.Layout.Override.BorderStyleSpecialRowSeparator = Infragistics.Win.UIElementBorderStyle.RaisedSoft;
                e.Layout.Override.CellClickAction = CellClickAction.EditAndSelectText;
                e.Layout.Override.SelectTypeRow = SelectType.None;
                e.Layout.ViewStyle = ViewStyle.SingleBand;
                e.Layout.ViewStyleBand = ViewStyleBand.OutlookGroupBy;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            pnlstock.Visible = false;
        }

        private void fgdtls_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                foreach (UltraGridBand band in fgdtls.DisplayLayout.Bands)
                {
                    if ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.Enter))
                    {
                        fgdtls.PerformAction(UltraGridAction.BelowCell, true, false);
                        fgdtls.PerformAction(UltraGridAction.EnterEditMode);
                        e.SuppressKeyPress = true;
                    }
                    else if (e.KeyCode == Keys.Up)
                    {
                        fgdtls.PerformAction(UltraGridAction.AboveCell, true, false);
                        fgdtls.PerformAction(UltraGridAction.EnterEditMode);
                        e.SuppressKeyPress = true;
                    }
                }
            }
            catch { }
        }

        private void pnlstock_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblParty1_Click(object sender, EventArgs e)
        {

        }

        public void checksession()
        {
            if (CheckConnection() == true)
            {
                //int isession = Localization.ParseNativeInt(DB.GetSnglValue("Select Sessions From tbl_UserMaster Where IsDeleted=0 and isLoggedIn=1 and UserID=" + Db_Detials.UserID + ""));
                int isession = Localization.ParseNativeInt(DB.GetSnglValue("Select Sessions From tbl_UserMaster Where IsDeleted=0  and UserID=" + Db_Detials.UserID + "and IPaddress=" + "'" + CommonCls.GetIP() + "'"));
                if (isession == 0)
                {
                    ShowinfoMessage(1);
                    AppStart.IsBool = 1;
                    this.Dispose();
                }
            }
        }

        public void ShowinfoMessage(int ShowbyMsg)
        {
            if (ShowbyMsg == 1)
            {
                Thread t = new Thread(new ThreadStart(ThreadForceClose));
                t.Start();
                Thread.Sleep(5000);
                if (t.IsAlive)
                    t.Abort();
            }
            if (ShowbyMsg == 2)
            {
                Thread t = new Thread(new ThreadStart(NetworkCheck));
                t.Start();
                Thread.Sleep(5000);
                if (t.IsAlive)
                    t.Abort();
            }
        }

        public void ThreadForceClose()
        {
            Navigate.ShowMessage(CIS_DialogIcon.Information, "Force Close Application", "Please Save The Entry The Software Will Close After 5 Seconds...");
        }

        public void NetworkCheck()
        {
            Navigate.ShowMessage(CIS_DialogIcon.Information, "Network Down", "Server Disconnected, Please Wait...!!");
        }

        public void CheckLogOffTime()
        {
            try
            {
                string LogOfftime = DB.GetSnglValue("Select UserTime From fn_Chk_CheckUserLogedStatus(" + Db_Detials.UserID + "," + Db_Detials.CompID + ", " + Db_Detials.YearID + ")");
                if (LogOfftime == "1")
                {
                    tbbtnLogoffUser_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void tmr_AppVersion_Tick(object sender, EventArgs e)
        {
            string strAppversion = DB.GetSnglValue(string.Format("Select ConfigValue From tbl_Appconfig Where ConfigName='Appversion'"));
            if (strAppversion != "")
            {
                if (Db_Detials.AppVersion != strAppversion)
                {
                    if (tmr_AppVersion.Interval == 60000)
                    {
                        ultraDesktopAlert1.AutoClose = Infragistics.Win.DefaultableBoolean.False;
                        ultraDesktopAlert1.Show("Good News!!!", "Your Software Got The Newer Version " + strAppversion + ".Please Update Your Software.");
                        Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Newer Version of Software is available..!, Please Update Your Software.");
                    }
                }
            }
        }

        private bool CheckConnection()
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
                ShowinfoMessage(2);
                return false;
            }
        }

        private void GetActiveFont()
        {
            try
            {
                Theme oTheme = new Theme();
                object MDI = RuntimeHelpers.GetObjectValue(Navigate.GetForm_byName("MDIMain"));
                Form actObj = (Form)RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());

                if(DB.GetSnglValue(String.Format("Select ThemeName from tbl_ThemeSettings_User Where UserID=" + Db_Detials.UserID + " and IsActive=1")).ToString().ToUpper() != "DEFAULT" && DB.GetSnglValue(String.Format("Select ThemeName from tbl_ThemeSettings_User Where UserID=" + Db_Detials.UserID + " and IsActive=1")).ToString().ToUpper().Trim() != "")
                {
                    if (actObj != null)
                    {
                        oTheme.SetThemeOnControls(actObj);
                        oTheme.SetThemeOnMDI((Control)MDI);
                    }
                    else
                    {
                        oTheme.SetThemeOnControls((Control)MDI);
                        oTheme.SetThemeOnMDI((Control)MDI);
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void GrdRef_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (GrdRef.Rows.Count > 0)
                {
                    //fgDtls.Rows[i].Cells["FabOutwardID"].Value
                    //GrdRef.
                    //UltraGridRow activeRow = GrdRef.ActiveRow;
                    //string strMouleID = DB.GetSnglValue("select MenuID from tbl_MenuMaster where MenuID=" + cboRptlst.SelectedValue);
                    //EventHandles.ShowbyFormID(strMouleID, null, null, -1, -1);
                    //object objectValue = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                    //Navigate.ShowbyID(objectValue, "[" + ((DataTable)NewLateBinding.LateGet(objectValue, null, "ds", new object[0], null, null, null)).Columns[0].ColumnName + "]", (int)Math.Round(Conversion.Val(activeRow.Cells[0].Text)));
                    //#region Collapse MDI Menu Dock
                    //object objMDI1 = RuntimeHelpers.GetObjectValue(Navigate.GetForm_byName("MDIMain"));
                    //dynamic objfrm = objMDI1;
                    //try
                    //{
                    //    objfrm.pnlDockTop.Expand = true;
                    //}
                    //catch (Exception ex) { Navigate.logError(ex.Message,ex.StackTrace); }
                    //#endregion
                }
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
        }

        public void ApplyVoucherTypeID()
        {
            try
            {
                Form actObj = (Form)RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                if (actObj != null)
                {
                    dynamic obj = actObj;
                    VoucherTypeID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select VoucherTypeID from fn_MenuMaster_Comp() Where MenuID={0}", obj.iIDentity)));
                }
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
        }
    }
}
