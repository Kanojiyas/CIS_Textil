using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;


public partial class frmTrnsIface : Form
{
    public bool _Add_Rights;
    private static List<WeakReference> __ENCList = new List<WeakReference>();
    public bool _Delete_Rights;
    public bool _Edit_Rights;
    public double _IRecordCount;
    [AccessedThroughProperty("pnlContent")]
    private Panel _pnlContent;
    public string _SearchStr;
    public string _SortingSrt;
    public bool _View_Rights;
    public Enum_Define.ActionType blnFormAction;
    public DataTable ds;
    public DataTable dt_AryCalcvalue;
    public DataTable dt_AryIsRequired;
    public DataTable dt_HasDtls_Grd;
    public int frmCompID;
    public int frmYearID;
    public int frmVoucherTypeID;
    public int frmStoreID;
    public int frmBranchID;
    public int iIDentity;
    public int IMaxRecord;
    public bool IsFooterGrid;
    public bool IsOpend_form;
    public int IStartRecord;
    public bool IsViewORTable;
    public int RecordNo;
    public object ref_Cbo;
    public int ref_ColID;
    public object ref_ParentForm;
    public int ref_RowID;
    public static int VoucherTypeID;
    public string sSearchQry;
    public string sSearchQry_Dtls;
    public string strTableName;
    public object formNM;
    public Form MdiformNM;
    public string sTextBoxCCase;
    public string strPmryCol;
    private readonly CIS_Textil.DisplaySettings _originalSettings;

    public bool IsMasterAdded;
    public string sComboAddText;

    public string sGridmasterAddText;
    public bool isGridmasterAddText;

    public bool isComboAddText;
    public string sSecondMessage;
    public bool isSecondMessage;

    public bool _Print_Rights;
    public bool _Email_Rights;
    public bool _SMS_Rights;
    public bool _Settings_Rights;


    public frmTrnsIface()
    {
        base.Load += new EventHandler(this.frm_Load);
        __ENCAddToList(this);
        this.RecordNo = 0;
        sGridmasterAddText = "";
        this.IStartRecord = 0;
        this.IMaxRecord = 0;
        this.IsViewORTable = false;
        this._IRecordCount = 0.0;
        this._SearchStr = string.Empty;
        this._SortingSrt = string.Empty;
        this._View_Rights = false;
        this._Add_Rights = false;
        this._Edit_Rights = false;
        this._Delete_Rights = false;
        this._Print_Rights = false;
        this._Email_Rights = false;
        this._SMS_Rights = false;
        this._Settings_Rights = false;
        strTableName = string.Empty;
        sSearchQry = string.Empty;
        sSearchQry_Dtls = string.Empty;
        this.ref_ParentForm = null;
        this.ref_Cbo = null;
        this.IsOpend_form = false;
        this.ref_ColID = -1;
        this.ref_RowID = -1;
        this.IsFooterGrid = false;
        this.dt_HasDtls_Grd = new DataTable();
        this.dt_AryCalcvalue = new DataTable();
        this.dt_AryIsRequired = new DataTable();
        this.frmYearID = -1;
        this.frmCompID = -1;
        this.frmStoreID = -1;
        this.frmBranchID = -1;
        this.frmVoucherTypeID = -1;
        sComboAddText = "";
        this.isSecondMessage = false;
        sSecondMessage = "";
        InitializeComponent();


        //_originalSettings = CIS_Textil.DisplayManager.GetCurrentSettings();
        //if ((_originalSettings.Width == 800) && (_originalSettings.Height == 600))
        //{
        //    this.Height = (this.Height - (-16));
        //    this.Width = (this.Width - (-4));
        //    this.pnlContent.Height = (this.pnlContent.Height - (-16));
        //    this.pnlContent.Width = (this.pnlContent.Width - (-4));
        //}
        //else if ((_originalSettings.Width == 1024) && (_originalSettings.Height == 768))
        //{
        //    this.Height = (this.Height - (142));
        //    this.Width = (this.Width - (198));

        //    this.pnlContent.Height = (this.pnlContent.Height - (142));
        //    this.pnlContent.Width = (this.pnlContent.Width - (198));
        //}
        //else if ((_originalSettings.Width == 1366) && (_originalSettings.Height == 768))
        //{
        //    this.Height = (this.Height - (142));
        //    this.Width = (this.Width - (540));

        //    this.pnlContent.Height = (this.pnlContent.Height - (142));
        //    this.pnlContent.Width = (this.pnlContent.Width - (540));
        //}
    }

    [DebuggerNonUserCode]
    private static void __ENCAddToList(object value)
    {
        List<WeakReference> list = __ENCList;
        lock (list)
        {
            if (__ENCList.Count == __ENCList.Capacity)
            {
                int index = 0;
                int num3 = __ENCList.Count - 1;
                for (int i = 0; i <= num3; i++)
                {
                    WeakReference reference = __ENCList[i];
                    if (reference.IsAlive)
                    {
                        if (i != index)
                        {
                            __ENCList[index] = __ENCList[i];
                        }
                        index++;
                    }
                }
                __ENCList.RemoveRange(index, __ENCList.Count - index);
                __ENCList.Capacity = __ENCList.Count;
            }
            __ENCList.Add(new WeakReference(RuntimeHelpers.GetObjectValue(value)));
        }
    }

    #region "Form Events"

    /// <summary>
    /// Handler For Form Activted.
    /// </summary>

    public static void frm_Activated(object sender, System.EventArgs e)
    {
        try
        {
            Navigate.CheckControl_Active(sender);
            object objMDI1 = RuntimeHelpers.GetObjectValue(Navigate.GetForm_byName("MDIMain"));
            dynamic objfrm = objMDI1;
            objfrm.grdSearch.DataSource = null;
            //NewLateBinding.LateSetComplex(NewLateBinding.LateGet(objMDI1, null, "grdSearch", new object[0], null, null, null), null, "DataSource", new object[] { null }, null, null, false, true);
            object objchld = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
            dynamic objChldfrm = objchld;
            //frmVoucherTypeID = Localization.ParseNativeInt(DB.GetSnglValue("Select VoucherTypeID From tbl_Menumaster Where MenuID=" + objChldfrm.iIDentity + ""));
            objfrm.lblNavigationPath.Text = "You are here: " + DB.GetSnglValue("select Menu_Path from [fn_MenuHierarchey](" + objChldfrm.iIDentity + ")");
            #region Apply Theme
            try
            {
                Form actObj = (Form)RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                Theme oTheme = new Theme();

                if (DB.GetSnglValue(String.Format("Select ThemeName from tbl_ThemeSettings_User Where UserID=" + Db_Detials.UserID + " and IsActive=1")).ToString().ToUpper() != "DEFAULT" && DB.GetSnglValue(String.Format("Select ThemeName from tbl_ThemeSettings_User Where UserID=" + Db_Detials.UserID + " and IsActive=1")).ToString().ToUpper().Trim() != "")
                {
                    if (actObj != null)
                    {
                        oTheme.SetThemeOnControls((Control)actObj);
                    }
                }
            }
            catch (Exception ex) { Navigate.logError(ex.Message,ex.StackTrace); }
            #endregion
        }
        catch (Exception ex)
        {
            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    private void frm_Load(object sender, System.EventArgs e)
    {
        try
        {
            string strID = sender.ToString();//.Replace("" + ".", "");
            strID = strID.Substring(0, strID.IndexOf(","));
            this.IsMasterAdded = false;
            this.sTextBoxCCase = Db_Detials._TextBoxCCase;
            if (Db_Detials.IsRuntime)
            {
                {
                    dt_HasDtls_Grd.Columns.Add("SubGridID", Type.GetType("System.Int32"));
                    dt_HasDtls_Grd.Columns.Add("ColIndex", Type.GetType("System.Int32"));
                    dt_HasDtls_Grd.Columns.Add("ColOrder", Type.GetType("System.Int32"));
                    dt_HasDtls_Grd.Columns.Add("ColDataType", Type.GetType("System.String"));
                    dt_HasDtls_Grd.Columns.Add("ColFields", Type.GetType("System.String"));
                    dt_HasDtls_Grd.Columns.Add("ColDataFormat", Type.GetType("System.String"));
                    dt_HasDtls_Grd.Columns.Add("Openform", Type.GetType("System.Int32"));
                    dt_HasDtls_Grd.Columns.Add("ToolTip", Type.GetType("System.String"));
                }

                {
                    dt_AryCalcvalue.Columns.Add("SubGridID", Type.GetType("System.Int32"));
                    dt_AryCalcvalue.Columns.Add("ColIndex", Type.GetType("System.Int32"));
                    dt_AryCalcvalue.Columns.Add("ColOrder", Type.GetType("System.Int32"));
                    dt_AryCalcvalue.Columns.Add("ColFields", Type.GetType("System.String"));
                    dt_AryCalcvalue.Columns.Add("ColCalcValue", Type.GetType("System.String"));
                }

                {
                    dt_AryIsRequired.Columns.Add("SubGridID", Type.GetType("System.Int32"));
                    dt_AryIsRequired.Columns.Add("ColIndex", Type.GetType("System.Int32"));
                    dt_AryIsRequired.Columns.Add("ColOrder", Type.GetType("System.Int32"));
                    dt_AryIsRequired.Columns.Add("ColFields", Type.GetType("System.String"));
                    dt_AryIsRequired.Columns.Add("IsRequired", Type.GetType("System.String"));
                    dt_AryIsRequired.Columns.Add("IsRepeatRow", Type.GetType("System.String"));
                    dt_AryIsRequired.Columns.Add("IsCompulsoryCol", Type.GetType("System.String"));
                }

                string[] strArray = Db_Detials.Hashref[iIDentity].ToString().Split(';');
                this.strTableName = strArray[0].ToString();
                this.sSearchQry = strArray[1].ToString();
                this.sSearchQry_Dtls = strArray[2].ToString();
                this.Text = strArray[3].ToString();
                this.strPmryCol = strArray[6].ToString();
                lblFormName.Text = strArray[3].ToString();


                string strqry = "";
                strqry = string.Format("Select * From {0} Where UserRightsID  =(Select TOP 1 UserRightsID from tbl_UserRightsMain where IsDeleted=0 and UserTypeID= {1} AND CompanyID={2}) And Form_MenuID = {3} ", Db_Detials.tbl_UserRightsDtls, Db_Detials.UserType, Db_Detials.CompID, iIDentity);
                int iQry = Localization.ParseNativeInt(DB.GetSnglValue("Select SecurityID  from tbl_SecurityMaster Where SecurityLvl='LedgerEdit'"));
                if (Db_Detials.UserType == iQry && Db_Detials.UserType != 1)
                {
                    Db_Detials.IsLedgerEdit = true;
                }
                else
                {
                    Db_Detials.IsLedgerEdit = false;
                }


                using (IDataReader idr = DB.GetRS(strqry))
                {
                    if (idr.Read())
                    {
                        _View_Rights = Localization.ParseBoolean(idr["View_Rights"].ToString());
                        _Add_Rights = Localization.ParseBoolean(idr["Add_Rights"].ToString());
                        _Delete_Rights = Localization.ParseBoolean(idr["Delete_Rights"].ToString());
                        _Edit_Rights = Localization.ParseBoolean(idr["Edit_Rights"].ToString());
                        _Print_Rights = Localization.ParseBoolean(idr["Print_Rights"].ToString());
                        _Email_Rights = Localization.ParseBoolean(idr["Email_Rights"].ToString());
                        _SMS_Rights = Localization.ParseBoolean(idr["SMS_Rights"].ToString());
                        _Settings_Rights = Localization.ParseBoolean(idr["Settings_Rights"].ToString());
                    }
                    else
                    {
                        _View_Rights = false;
                        _Add_Rights = false;
                        _Delete_Rights = false;
                        _Edit_Rights = false;
                        _Print_Rights = false;
                        _Email_Rights = false;
                        _SMS_Rights = false;
                        _Settings_Rights = false;
                    }
                }
                this.Activated += frm_Activated;
                this.FormClosing += EventHandles.frm_FormClosing;

                object objMDI1 = RuntimeHelpers.GetObjectValue(Navigate.GetForm_byName("MDIMain"));
                MdiformNM = (Form)objMDI1;
                dynamic objfrm = objMDI1;

                if (sSearchQry_Dtls == "-")
                {
                    objfrm.btnFindShowDtls.Visible = false;
                }
                else
                {
                    objfrm.btnFindShowDtls.Visible = true;
                }

                if (Localization.ParseBoolean(strArray[4].ToString()))
                {
                    frmYearID = Db_Detials.YearID;
                    frmCompID = Db_Detials.CompID;
                    frmStoreID = Db_Detials.StoreID;
                    frmBranchID = Db_Detials.BranchID;
                    frmVoucherTypeID = Localization.ParseNativeInt(DB.GetSnglValue("Select VoucherTypeID From tbl_Menumaster Where MenuID=" + iIDentity + ""));
                }
                object frm = this;
                Navigate.ApplyColorform(frm);
                Navigate.CheckControl_Active(sender);
                AssignProperties((Control)frm);

                #region Apply Theme
                Theme oTheme = new Theme();
                if (DB.GetSnglValue(String.Format("Select * from tbl_ThemeSettings_User Where UserID=" + Db_Detials.UserID + " and ThemeName='Default' and IsActive=1")).ToString() != "1")
                {
                    if (frm != null)
                    {
                        oTheme.SetThemeOnControls((Control)frm);
                    }
                }
                #endregion
            }
        }
        catch (Exception ex)
        {
            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            Navigate.logError(ex.Message, ex.StackTrace);
        }
    }

    #endregion

    public void AssignProperties(Control Control)
    {
        IniFile ini = new IniFile(Application.StartupPath.ToString() + "\\Others\\FormSetting.ini");
        object frm = this;
        string str = frm.ToString();
        str = str.Substring(0, str.IndexOf(","));
        string[] strVal = str.Split('.');
        foreach (Control cnt in Control.Controls)
        {
            if (cnt is System.Windows.Forms.Panel)
                AssignProperties(cnt);
            else
            {
                if (!string.IsNullOrEmpty(ini.IniReadValue(strVal[1].ToString(), cnt.Name + ".ENABLE")))
                {
                    if (ini.IniReadValue(strVal[1].ToString(), cnt.Name + ".ISMANDETORY") == "1")
                    {
                        if (ini.IniReadValue(strVal[1].ToString(), cnt.Name + ".ENABLE") == "0")
                            cnt.Enabled = false;
                        else
                            cnt.Enabled = true;
                    }
                    else
                    {
                        if (ini.IniReadValue(strVal[1].ToString(), cnt.Name + ".ENABLE") == "0")
                        {
                            cnt.Enabled = false;
                            if (cnt is CIS_CLibrary.CIS_Textbox)
                                cnt.Text = "";
                        }
                        else
                            cnt.Enabled = true;
                    }
                }

                if (!string.IsNullOrEmpty(ini.IniReadValue(strVal[1].ToString(), cnt.Name + ".TABINDEX")))
                {
                    cnt.TabIndex = Localization.ParseNativeInt(ini.IniReadValue(strVal[1].ToString(), cnt.Name + ".TABINDEX"));
                }

                if (!string.IsNullOrEmpty(ini.IniReadValue(strVal[1].ToString(), cnt.Name + ".LOCATION")))
                {
                    string strlocation = ini.IniReadValue(strVal[1].ToString(), cnt.Name + ".LOCATION").Replace("{", "").Replace("}", "");
                    string[] strarr = strlocation.Split(',');
                    int X = Localization.ParseNativeInt(strarr[0].Replace("X", "").Replace("=", ""));
                    int Y = Localization.ParseNativeInt(strarr[1].Replace("Y", "").Replace("=", ""));
                    cnt.Location = new System.Drawing.Point(X, Y);
                }

                if (ini.IniReadValue(strVal[1].ToString(), cnt.Name + ".Moveable") == "1")
                {
                    // CIS_CLibrary.ControlMoverOrResizer.Init(cnt);
                }
                else { CIS_CLibrary.ControlMoverOrResizer.StopDragOrResizing(cnt); }
            }
        }
    }

    private void btnMinimize_Click(object sender, EventArgs e)
    {
        if (this.WindowState != FormWindowState.Minimized)
        {
            try
            {
                this.WindowState = FormWindowState.Minimized;
            }
            catch
            { }
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        EventHandles.Closeform();
    }
}
