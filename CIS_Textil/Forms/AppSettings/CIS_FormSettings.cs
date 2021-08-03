using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using  CIS_Bussiness;using CIS_DBLayer;
using CIS_Utilities;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic;
using System.Collections;

namespace CIS_CLibrary
{
    [ToolboxItem(true)]
    public partial class CIS_FormSettings : UserControl
    {
        private static string _mdiFormNM;
        private static string _tbbtnButtom;
        private static string _IniFileNM;

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        object instance = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
        object frmMdi = Application.OpenForms[_mdiFormNM];
        IniFile ini = new IniFile(Application.StartupPath.ToString() + "\\Others\\" + _IniFileNM);
        DataTable dt = new DataTable();

        public object objMDI1;

        public CIS_FormSettings()
        {
            InitializeComponent();
            GetGrid();
            ShowForm();
        }

        private void ShowForm()
        {
            objMDI1 = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
            if (objMDI1 != null)
            {
                AssignProperties();
                dynamic objForm = objMDI1;
                objForm.TopLevel = false;
                pnlDesigner.Controls.Add(objForm);
                objForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                objForm.Dock = DockStyle.Fill;
                objForm.Show();
            }
        }

        private void CIS_FormSettings_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(base.Handle, 0xa1, 2, 0);
            }
        }

        public void AssignProperties()
        {
            dynamic objfrm = RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
            getcontrols(RuntimeHelpers.GetObjectValue(objfrm.pnlContent));
        }

        public void getcontrols(object Container)
        {
            try
            {
                dynamic objCntner = Container;
                if (Conversions.ToBoolean(objCntner.HasChildren))
                {
                    IEnumerator enumerator = null;
                    try
                    {
                        Panel pnl = new Panel();
                        enumerator = ((IEnumerable)objCntner.Controls).GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            Control current = (Control)enumerator.Current;
                            if (current is CIS_CLibrary.CIS_Textbox)
                            {
                                CIS_Textbox box = (CIS_Textbox)current;
                                box.Moveable = true;
                            }
                            else if (current is CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)
                            {
                                try
                                {
                                    CIS_MultiColumnComboBox.CIS_MultiColumnComboBox box2 = (CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)current;
                                    box2.Moveable = true;
                                }
                                catch { }
                            }
                            else if (current is CIS_CLibrary.CIS_TextLabel)
                            {
                                try
                                {
                                    CIS_TextLabel box2 = (CIS_TextLabel)current;
                                    box2.Moveable = true;
                                }
                                catch { }
                            }
                            else if (current is CIS_CLibrary.CIS_Panel)
                            {
                                try
                                {
                                    CIS_Panel box2 = (CIS_Panel)current;
                                    box2.Moveable = true;
                                }
                                catch { }
                            }
                            else if (current is CIS_CLibrary.CIS_Label)
                            {
                                try
                                {

                                    CIS_Label box2 = (CIS_Label)current;
                                    box2.Moveable = true;

                                }
                                catch { }
                            }
                            else if (current is CIS_CLibrary.CIS_CheckBoxList)
                            {
                                try
                                {

                                    CIS_CheckBoxList box2 = (CIS_CheckBoxList)current;
                                    box2.Moveable = true;

                                }
                                catch { }
                            }
                            else if (current is CIS_CLibrary.CIS_Button)
                            {
                                try
                                {

                                    CIS_Button box2 = (CIS_Button)current;
                                    box2.Moveable = true;

                                }
                                catch { }
                            }
                            else if (current is CIS_CLibrary.CIS_RadioButton)
                            {
                                try
                                {

                                    CIS_RadioButton box2 = (CIS_RadioButton)current;
                                    box2.Moveable = true;
                                }
                                catch { }
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
            catch { }
        }

        private void GetGrid()
        {
            try
            {
                if (frmMdi != null)
                {
                    NewLateBinding.LateSetComplex(NewLateBinding.LateGet(frmMdi, null, _tbbtnButtom, new object[0], null, null, null), null, "Enabled", new object[] { false }, null, null, false, true);
                }
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
            try
            {
                string str = instance.ToString();
                string[] strVal1 = str.Split(',');
                string[] strVal2 = strVal1[1].Split(':');
                lblFormName.Text = "Navigation Settings for " + strVal2[1];
                DataColumn col1 = new DataColumn("ControlName_DB");
                DataColumn col2 = new DataColumn("ControlName_Alise");
                DataColumn col3 = new DataColumn("Enabled");
                DataColumn col4 = new DataColumn("TabIndex");
                DataColumn col5 = new DataColumn("IsMandetory");
                DataColumn col6 = new DataColumn("Location");
                DataColumn col7 = new DataColumn("Hidden");

                col1.DataType = System.Type.GetType("System.String");
                col2.DataType = System.Type.GetType("System.String");
                col3.DataType = System.Type.GetType("System.Boolean");
                col4.DataType = System.Type.GetType("System.Double");
                col5.DataType = System.Type.GetType("System.String");
                col6.DataType = System.Type.GetType("System.String");
                col7.DataType = System.Type.GetType("System.Boolean");

                col1.ReadOnly = true;
                col2.ReadOnly = true;
                col5.ReadOnly = true;
                col6.ReadOnly = true;

                dt.Columns.Add(col1);
                dt.Columns.Add(col2);
                dt.Columns.Add(col3);
                dt.Columns.Add(col4);
                dt.Columns.Add(col5);
                dt.Columns.Add(col6);
                dt.Columns.Add(col7);

                Control cntl = new Control();
                cntl = (Control)instance;
                //cntl = (Control)NewLateBinding.LateGet(instance, null, "pnlContent", new object[0], null, null, null);
                BindGrid(cntl, dt);

                DataView dv = dt.DefaultView;
                dv.Sort = "TabIndex asc";
                DataTable sortedDT = dv.ToTable();
                fgDtls.DataSource = sortedDT;

                fgDtls.ClearSelection();
                fgDtls.Rows[0].Cells[0].Selected = true;
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
        }

        private void BindGrid(Control cntl, DataTable dt)
        {
            try
            {
                string str = instance.ToString();
                str = str.Substring(0, str.IndexOf(","));
                string[] strVal = str.Split('.');
                DataRow dr = null;
                if (instance != null)
                {
                    foreach (Control cnt in cntl.Controls)
                    {
                        //if ((!(cnt is System.Windows.Forms.Label)) && (!(cnt is System.Windows.Forms.CheckBox)))
                        {
                            if (cnt is System.Windows.Forms.Panel)
                                BindGrid(cnt, dt);
                            else if (cnt is DataGrid)
                                BindGrid(cnt, dt);
                            else
                            {
                                if ((cnt.Name != "LblUpdateUser") && (cnt.Name != "pnlContent") && (cnt.Name != "lblUUser") && (cnt.Name != "lblHelpText") && (cnt.Name != "lblHelp") && (cnt.Name != "lblFormName") && (cnt.Name != "lblCreatedUser") && (cnt.Name != "lblCUser") && (cnt.Name != "txtCode") && (cnt.Name != "btnMinimize") && (cnt.Name != "btnCancel") && (cnt.Name != ""))
                                {
                                    try
                                    {
                                        dr = dt.NewRow();
                                        dr["ControlName_DB"] = cnt.Name;

                                        if (cnt is TextBox)
                                            dr["ControlName_Alise"] = ((CIS_CLibrary.CIS_Textbox)(cnt)).NameOfControl;
                                        else if (cnt is ComboBox)
                                            dr["ControlName_Alise"] = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)(cnt)).NameOfControl;
                                        else if (cnt is DataGridView)
                                            dr["ControlName_Alise"] = ((CIS_DataGridViewEx.DataGridViewEx)(cnt)).NameOfControl;
                                        else if (cnt is Label)
                                            dr["ControlName_Alise"] = ((CIS_CLibrary.CIS_TextLabel)(cnt)).NameOfControl;
                                        else if (cnt is CheckBox)
                                            dr["ControlName_Alise"] = ((CIS_CLibrary.CIS_CheckBox)(cnt)).NameOfControl;
                                        else if (cnt is Panel)
                                            dr["ControlName_Alise"] = ((CIS_CLibrary.CIS_Panel)(cnt)).NameOfControl;

                                        #region Enable Column
                                        dr["Enabled"] = cnt.Enabled;
                                        #endregion

                                        #region TabIndex Column
                                        dr["TabIndex"] = cnt.TabIndex;
                                        #endregion

                                        #region Mandetory Column
                                        if (cnt is TextBox)
                                            dr["IsMandetory"] = (((CIS_CLibrary.CIS_Textbox)(cnt)).IsMandatory == true ? "YES" : "NO");
                                        else if (cnt is ComboBox)
                                            dr["IsMandetory"] = (((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)(cnt)).IsMandatory == true ? "YES" : "NO");
                                        #endregion

                                        #region Control Location
                                        dr["Location"] = cnt.Location;
                                        #endregion

                                        #region Hidden Control
                                        dr["Hidden"] = cnt.Visible;
                                        #endregion

                                        dt.Rows.Add(dr);
                                    }
                                    catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
        }

        private void txtClose_Click(object sender, EventArgs e)
        {
            try
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
                this.Hide();
                double dbliIDentity = Conversion.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(instance, null, "iIDentity", new object[0], null, null, null)));
                EventHandles.ShowbyFormID(dbliIDentity.ToString(), null, null, -1, -1);
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dt.Rows.Clear();
            try
            {
                string str = instance.ToString();
                string[] strVal1 = str.Split(',');
                string[] strVal2 = strVal1[1].Split(':');
                lblFormName.Text = "Navigation Settings for " + strVal2[1];

                DataColumn col1 = new DataColumn("ControlName_DB");
                DataColumn col2 = new DataColumn("ControlName_Alise");
                DataColumn col3 = new DataColumn("Enabled");
                DataColumn col4 = new DataColumn("TabIndex");
                DataColumn col5 = new DataColumn("IsMandetory");
                DataColumn col6 = new DataColumn("Location");
                DataColumn col7 = new DataColumn("Hidden");

                col1.DataType = System.Type.GetType("System.String");
                col2.DataType = System.Type.GetType("System.String");
                col3.DataType = System.Type.GetType("System.Boolean");
                col4.DataType = System.Type.GetType("System.Double");
                col5.DataType = System.Type.GetType("System.String");
                col6.DataType = System.Type.GetType("System.String");
                col7.DataType = System.Type.GetType("System.Boolean");

                col1.ReadOnly = true;
                col2.ReadOnly = true;
                col5.ReadOnly = true;
                Control cntl = new Control();
                cntl = (Control)instance;
                BindGrid(cntl, dt);
                DataView dv = dt.DefaultView;
                dv.Sort = "TabIndex asc";
                DataTable sortedDT = dv.ToTable();
                fgDtls.DataSource = sortedDT;

                fgDtls.ClearSelection();
                fgDtls.Rows[0].Cells[0].Selected = true;
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }

            try
            {
                string str = instance.ToString();
                str = str.Substring(0, str.IndexOf(","));
                string[] strVal = str.Split('.');

                IniFile ini = new IniFile(Application.StartupPath.ToString() + "\\Others\\" + _IniFileNM);

                for (int i = 0; i <= (this.fgDtls.RowCount - 1); i++)
                {
                    DataGridViewRow row = this.fgDtls.Rows[i];
                    if ((row.Cells[0].Value != null) && (row.Cells[2].Value != null))
                    {
                        if ((row.Cells[0].Value.ToString() != "") && (row.Cells[2].Value.ToString() != ""))
                        {
                            ini.IniWriteValue(strVal[1].ToString(), row.Cells[0].Value + ".ENABLE", (row.Cells[2].Value.ToString() == "True" ? "1" : "0"));
                            ini.IniWriteValue(strVal[1].ToString(), row.Cells[0].Value + ".TABINDEX", row.Cells[3].Value.ToString());
                            ini.IniWriteValue(strVal[1].ToString(), row.Cells[0].Value + ".ISMANDETORY", (row.Cells[4].Value.ToString() == "YES" ? "1" : "0"));
                            ini.IniWriteValue(strVal[1].ToString(), row.Cells[0].Value + ".LOCATION", (row.Cells[5].Value.ToString()));
                            ini.IniWriteValue(strVal[1].ToString(), row.Cells[0].Value + ".HIDDEN", (row.Cells[6].Value.ToString() == "True" ? "1" : "0"));
                        }
                    }
                    row = null;
                }
                Navigate.ShowMessage(CIS_DialogIcon.Information, "", "Settings Saved Successfully..");
                try
                {
                    object[] objArray = new object[] { RuntimeHelpers.GetObjectValue(instance) };
                    bool[] flagArray = new bool[] { true };
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
                    NewLateBinding.LateSetComplex(NewLateBinding.LateGet(frmMdi, null, _tbbtnButtom, new object[0], null, null, null), null, "Enabled", new object[] { true }, null, null, false, true);
                    NewLateBinding.LateCall(instance, null, "AssignProperties", objArray, null, null, flagArray, true);
                }
                catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                this.Hide();
                double dbliIDentity = Conversion.Val(RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(instance, null, "iIDentity", new object[0], null, null, null)));
                EventHandles.ShowbyFormID(dbliIDentity.ToString(), null, null, -1, -1);
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        private void FormSettingControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                try
                {
                    txtClose_Click(null, null);
                }
                catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
                //this.Hide();
            }
        }

        public string SetMDIform
        {
            get { return _mdiFormNM; }
            set { _mdiFormNM = value; }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                txtClose_Click(null, null);
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        public string SetButtonCntrl
        {
            get { return _tbbtnButtom; }
            set { _tbbtnButtom = value; }
        }

        public string SetIniFileNM
        {
            get { return _IniFileNM; }
            set { _IniFileNM = value; }
        }

        private void CIS_FormSettings_Load(object sender, EventArgs e)
        {
            ciS_TabControl1.Selecting += new TabControlCancelEventHandler(ciS_TabControl1_Selecting);
        }

        private void btnDesigner_Click(object sender, EventArgs e)
        {

        }

        private void ciS_TabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            TabPage current = (sender as TabControl).SelectedTab;
            if (current != null)
            {
                dt.Rows.Clear();
                try
                {
                    string str = instance.ToString();
                    string[] strVal1 = str.Split(',');
                    string[] strVal2 = strVal1[1].Split(':');
                    lblFormName.Text = "Navigation Settings for " + strVal2[1];
                    DataColumn col1 = new DataColumn("ControlName_DB");
                    DataColumn col2 = new DataColumn("ControlName_Alise");
                    DataColumn col3 = new DataColumn("Enabled");
                    DataColumn col4 = new DataColumn("TabIndex");
                    DataColumn col5 = new DataColumn("IsMandetory");
                    DataColumn col6 = new DataColumn("Location");
                    DataColumn col7 = new DataColumn("Hidden");


                    col1.DataType = System.Type.GetType("System.String");
                    col2.DataType = System.Type.GetType("System.String");
                    col3.DataType = System.Type.GetType("System.Boolean");
                    col4.DataType = System.Type.GetType("System.Double");
                    col5.DataType = System.Type.GetType("System.String");
                    col6.DataType = System.Type.GetType("System.String");
                    col7.DataType = System.Type.GetType("System.Boolean");

                    col1.ReadOnly = true;
                    col2.ReadOnly = true;
                    col5.ReadOnly = true;
                    Control cntl = new Control();
                    cntl = (Control)instance;
                    //cntl = (Control)NewLateBinding.LateGet(instance, null, "pnlContent", new object[0], null, null, null);
                    BindGrid(cntl, dt);

                    DataView dv = dt.DefaultView;
                    dv.Sort = "TabIndex asc";
                    DataTable sortedDT = dv.ToTable();
                    fgDtls.DataSource = sortedDT;

                    fgDtls.ClearSelection();
                    fgDtls.Rows[0].Cells[0].Selected = true;
                }
                catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
            }
        }
    }
}
