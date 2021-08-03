using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CIS_Bussiness;using CIS_DBLayer;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmEmailAddressBook : Form
    {
        public int iIDentity;
        public bool isEmail;
        public bool isReportTool;
        public bool isSendsms;
        public int iMailingConfigID;

        public int Id;
        public string sFromEmailID;
        public static string IdStr;
        public string sSubject;
        public string sToEmailID;
        CheckBox checkboxHeader = new CheckBox();
        public object refControl;
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        private static Hashtable Hashref = new Hashtable();

        public frmEmailAddressBook()
        {
            InitializeComponent();
        }

        private void frmEmailAddressBook_Load(object sender, EventArgs e)
        {
            if (!isEmail)
            {
                cboFromEmailID_R.Visible = false;
                lblEmailAcc_R.Visible = false;
                btnSendSms.Visible = true;
                lblSubject.Visible = false;
                txtSubject.Visible = false;
                btnSendSms.BringToFront();
                Application.DoEvents();
            }
            else
            {
                cboFromEmailID_R.Visible = true;
                lblEmailAcc_R.Visible = true;
                btnSendSms.Visible = false;
                lblSubject.Visible = true;
                txtSubject.Visible = true;
            }

            string strQry = "";
            string stQry = "";
            Combobox_Setup.FillCbo(ref cboFromEmailID_R, Combobox_Setup.ComboType.Mst_EmailID, "CompID=" + Db_Detials.CompID);
            cboFromEmailID_R.SelectedIndex = 1;
            switch (this.iIDentity)
            {

                case 150:
                    IdStr = "FabPOID";
                    stQry = "SELECT Distinct FabPOID,EntryNo from fn_FetchEntryNoFabricPurchaseOrder() Where CompID=" + Db_Detials.CompID + " and YearID =" + Db_Detials.YearID + "";
                    Combobox_Setup_RT.Fill_Combo(cboFromEntryNo, stQry, "EntryNo", IdStr);
                    Combobox_Setup_RT.Fill_Combo(cboToEntryNo, stQry, "EntryNo", IdStr);
                    cboFromEntryNo.SelectedValue = Id;
                    cboToEntryNo.SelectedValue = Id;

                    strQry = "SELECT Distinct FabPOID,PartyName, MobileNo,FabPONo, EmailID from fn_FetchEntryNoFabricPurchaseOrder() Where CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " and EntryNo between " + cboFromEntryNo.Text + " and " + cboToEntryNo.Text + "";

                    using (DataTable Dt = DB.GetDT(strQry, false))
                    {
                        grdAddressBook.RowCount = Dt.Rows.Count;
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            grdAddressBook.Rows[i].Cells[1].Value = Dt.Rows[i]["PartyName"].ToString();
                            grdAddressBook.Rows[i].Cells[2].Value = Dt.Rows[i]["FabPONo"].ToString();
                            grdAddressBook.Rows[i].Cells[3].Value = Dt.Rows[i]["EmailID"].ToString();
                            grdAddressBook.Rows[i].Cells[4].Value = Dt.Rows[i]["MobileNo"].ToString();

                            if (!isEmail)
                            {
                                grdAddressBook.Columns[4].Visible = true;
                            }
                            else
                            {
                                grdAddressBook.Columns[4].Visible = false;
                            }

                            grdAddressBook.Rows[i].Cells[5].Value = Dt.Rows[i]["FabPOID"].ToString();

                            if (Dt.Rows[i]["EmailID"].ToString() == null || Dt.Rows[i]["EmailID"].ToString() == "")
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = true;
                            }
                            else
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = false;
                            }
                        }
                        cboGroup.Items.Add("PartyName");
                        cboGroup.Items.Add("RefNo");
                        cboGroup.Items.Add("EmailID");
                        cboGroup.Items.Add("MobileNo");
                    }

                    break;




                case 166:
                    IdStr = "FabSOID";
                    stQry = "SELECT Distinct FabSOID,EntryNo from fn_FetchEntryNoFabricSalesOrder() Where CompID=" + Db_Detials.CompID + " and YearID =" + Db_Detials.YearID + "";
                    Combobox_Setup_RT.Fill_Combo(cboFromEntryNo, stQry, "EntryNo", IdStr);
                    Combobox_Setup_RT.Fill_Combo(cboToEntryNo, stQry, "EntryNo", IdStr);
                    cboFromEntryNo.SelectedValue = Id;
                    cboToEntryNo.SelectedValue = Id;

                    strQry = "SELECT Distinct FabSOID,PartyName, MobileNo,FabSONo, EmailID from fn_FetchEntryNoFabricSalesOrder() Where CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " and EntryNo between " + cboFromEntryNo.Text + " and " + cboToEntryNo.Text + "";

                    using (DataTable Dt = DB.GetDT(strQry, false))
                    {
                        grdAddressBook.RowCount = Dt.Rows.Count;
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            grdAddressBook.Rows[i].Cells[1].Value = Dt.Rows[i]["PartyName"].ToString();
                            grdAddressBook.Rows[i].Cells[2].Value = Dt.Rows[i]["FabSONo"].ToString();
                            grdAddressBook.Rows[i].Cells[3].Value = Dt.Rows[i]["EmailID"].ToString();
                            grdAddressBook.Rows[i].Cells[4].Value = Dt.Rows[i]["MobileNo"].ToString();

                            if (!isEmail)
                            {
                                grdAddressBook.Columns[4].Visible = true;
                            }
                            else
                            {
                                grdAddressBook.Columns[4].Visible = false;
                            }

                            grdAddressBook.Rows[i].Cells[5].Value = Dt.Rows[i]["FabSOID"].ToString();

                            if (Dt.Rows[i]["EmailID"].ToString() == null || Dt.Rows[i]["EmailID"].ToString() == "")
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = true;
                            }
                            else
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = false;
                            }
                        }
                        cboGroup.Items.Add("PartyName");
                        cboGroup.Items.Add("RefNo");
                        cboGroup.Items.Add("EmailID");
                        cboGroup.Items.Add("MobileNo");
                    }

                    break;

                case 470:
                    IdStr = "FabSOID";
                    stQry = "SELECT Distinct FabSOID,EntryNo from fn_FetchEntryNoFabricSalesOrderSerial() Where CompID=" + Db_Detials.CompID + " and YearID =" + Db_Detials.YearID + "";
                    Combobox_Setup_RT.Fill_Combo(cboFromEntryNo, stQry, "EntryNo", IdStr);
                    Combobox_Setup_RT.Fill_Combo(cboToEntryNo, stQry, "EntryNo", IdStr);
                    cboFromEntryNo.SelectedValue = Id;
                    cboToEntryNo.SelectedValue = Id;

                    strQry = "SELECT Distinct FabSOID,PartyName, MobileNo,FabSONo, EmailID from fn_FetchEntryNoFabricSalesOrderSerial() Where CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + " and EntryNo between " + cboFromEntryNo.Text + " and " + cboToEntryNo.Text + "";


                    using (DataTable Dt = DB.GetDT(strQry, false))
                    {
                        grdAddressBook.RowCount = Dt.Rows.Count;
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            grdAddressBook.Rows[i].Cells[1].Value = Dt.Rows[i]["PartyName"].ToString();
                            grdAddressBook.Rows[i].Cells[2].Value = Dt.Rows[i]["FabSONo"].ToString();
                            grdAddressBook.Rows[i].Cells[3].Value = Dt.Rows[i]["EmailID"].ToString();
                            grdAddressBook.Rows[i].Cells[4].Value = Dt.Rows[i]["MobileNo"].ToString();
                            grdAddressBook.Rows[i].Cells[5].Value = Dt.Rows[i]["FabSOID"].ToString();
                            if (!isEmail)
                            {
                                grdAddressBook.Columns[4].Visible = true;
                            }
                            else
                            {
                                grdAddressBook.Columns[4].Visible = false;
                            }
                            if (Dt.Rows[i]["EmailID"].ToString() == null || Dt.Rows[i]["EmailID"].ToString() == "")
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = true;
                            }
                            else
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = false;
                            }
                        }
                        cboGroup.Items.Add("PartyName");
                        cboGroup.Items.Add("RefNo");
                        cboGroup.Items.Add("EmailID");
                        cboGroup.Items.Add("MobileNo");

                    }

                    break;

                case 168:
                    IdStr = "FabOutwardID";
                    stQry = "SELECT Distinct FabOutwardID,EntryNo from Fn_FetchEntryNoFabricDeliveryChallan() Where CompID=" + Db_Detials.CompID + " and YearID= " + Db_Detials.YearID + "";
                    Combobox_Setup_RT.Fill_Combo(cboFromEntryNo, stQry, "EntryNo", IdStr);
                    Combobox_Setup_RT.Fill_Combo(cboToEntryNo, stQry, "EntryNo", IdStr);
                    cboFromEntryNo.SelectedValue = Id;
                    cboToEntryNo.SelectedValue = Id;

                    strQry = "SELECT distinct FabOutwardID,PartyName, MobileNo,RefNo , EmailID from Fn_FetchEntryNoFabricDeliveryChallan() Where CompId =" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "  and EntryNo between " + cboFromEntryNo.Text + " and " + cboToEntryNo.Text + "";


                    using (DataTable Dt = DB.GetDT(strQry, false))
                    {
                        grdAddressBook.RowCount = Dt.Rows.Count;
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            grdAddressBook.Rows[i].Cells[1].Value = Dt.Rows[i]["PartyName"].ToString();
                            grdAddressBook.Rows[i].Cells[2].Value = Dt.Rows[i]["RefNo"].ToString();
                            grdAddressBook.Rows[i].Cells[3].Value = Dt.Rows[i]["EmailID"].ToString();
                            grdAddressBook.Rows[i].Cells[4].Value = Dt.Rows[i]["MobileNo"].ToString();
                            grdAddressBook.Rows[i].Cells[5].Value = Dt.Rows[i]["FabOutwardID"].ToString();
                            if (!isEmail)
                            {
                                grdAddressBook.Columns[4].Visible = true;
                            }
                            else
                            {
                                grdAddressBook.Columns[4].Visible = false;
                            }
                            if (Dt.Rows[i]["EmailID"].ToString() == null || Dt.Rows[i]["EmailID"].ToString() == "")
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = true;
                            }
                            else
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = false;
                            }
                        }
                        cboGroup.Items.Add("PartyName");
                        cboGroup.Items.Add("RefNo");
                        cboGroup.Items.Add("EmailID");
                        cboGroup.Items.Add("MobileNo");

                    }

                    break;

                case 171:
                    IdStr = "SalesID";
                    stQry = "SELECT Distinct SalesID,EntryNo from fn_FetchEntryForFabricInvoice() Where CompID=" + Db_Detials.CompID + " and YearID= " + Db_Detials.YearID + "";
                    Combobox_Setup_RT.Fill_Combo(cboFromEntryNo, stQry, "EntryNo", IdStr);
                    Combobox_Setup_RT.Fill_Combo(cboToEntryNo, stQry, "EntryNo", IdStr);
                    cboFromEntryNo.SelectedValue = Id;
                    cboToEntryNo.SelectedValue = Id;

                    strQry = "SELECT distinct SalesID,PartyName, MobileNo,BillNo, EmailID from fn_FetchEntryForFabricInvoice() Where CompId =" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "  and EntryNo between " + cboFromEntryNo.Text + " and " + cboToEntryNo.Text + "";


                    using (DataTable Dt = DB.GetDT(strQry, false))
                    {
                        grdAddressBook.RowCount = Dt.Rows.Count;
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            grdAddressBook.Rows[i].Cells[1].Value = Dt.Rows[i]["PartyName"].ToString();
                            grdAddressBook.Rows[i].Cells[2].Value = Dt.Rows[i]["BillNo"].ToString();
                            grdAddressBook.Rows[i].Cells[3].Value = Dt.Rows[i]["EmailID"].ToString();
                            grdAddressBook.Rows[i].Cells[4].Value = Dt.Rows[i]["MobileNo"].ToString();
                            grdAddressBook.Rows[i].Cells[5].Value = Dt.Rows[i]["SalesID"].ToString();
                            if (!isEmail)
                            {
                                grdAddressBook.Columns[4].Visible = true;
                            }
                            else
                            {
                                grdAddressBook.Columns[4].Visible = false;
                            }
                            if (Dt.Rows[i]["EmailID"].ToString() == null || Dt.Rows[i]["EmailID"].ToString() == "")
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = true;
                            }
                            else
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = false;
                            }
                        }
                        cboGroup.Items.Add("PartyName");
                        cboGroup.Items.Add("RefNo");
                        cboGroup.Items.Add("EmailID");
                        cboGroup.Items.Add("MobileNo");

                    }

                    break;


                case 473:
                    stQry = "SELECT Distinct SalesID,EntryNo from fn_FabricInvoiceMain2(" + Db_Detials.CompID + "," + Db_Detials.YearID + ")";
                    Combobox_Setup_RT.Fill_Combo(cboFromEntryNo, stQry, "EntryNo", "SalesID");
                    Combobox_Setup_RT.Fill_Combo(cboToEntryNo, stQry, "EntryNo", "SalesID");
                    cboFromEntryNo.SelectedValue = Id;
                    cboToEntryNo.SelectedValue = Id;

                    strQry = "SELECT distinct SalesID,PartyName, MobileNo,BillNo, EmailID from fn_FabricInvoiceMain2(" + Db_Detials.CompID + "," + Db_Detials.YearID + ") where  EntryNo between " + cboFromEntryNo.Text + " and " + cboToEntryNo.Text + "";

                    using (DataTable Dt = DB.GetDT(strQry, false))
                    {
                        grdAddressBook.RowCount = Dt.Rows.Count;
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            grdAddressBook.Rows[i].Cells[1].Value = Dt.Rows[i]["PartyName"].ToString();
                            grdAddressBook.Rows[i].Cells[2].Value = Dt.Rows[i]["BillNo"].ToString();
                            grdAddressBook.Rows[i].Cells[3].Value = Dt.Rows[i]["EmailID"].ToString();
                            grdAddressBook.Rows[i].Cells[4].Value = Dt.Rows[i]["MobileNo"].ToString();
                            grdAddressBook.Rows[i].Cells[5].Value = Dt.Rows[i]["SalesID"].ToString();
                            if (!isEmail)
                            {
                                grdAddressBook.Columns[4].Visible = true;
                            }
                            else
                            {
                                grdAddressBook.Columns[4].Visible = false;
                            }
                            if (Dt.Rows[i]["EmailID"].ToString() == null || Dt.Rows[i]["EmailID"].ToString() == "")
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = true;
                            }
                            else
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = false;
                            }
                        }
                        cboGroup.Items.Add("PartyName");
                        cboGroup.Items.Add("RefNo");
                        cboGroup.Items.Add("EmailID");
                        cboGroup.Items.Add("MobileNo");
                    }

                    break;

                case 472:
                    stQry = "SELECT Distinct FabOutwardID,EntryNo from fn_FetchEntryNoForDeliveryChallanSerial() Where CompID=" + Db_Detials.CompID + " and YearID =" + Db_Detials.YearID + "";
                    Combobox_Setup_RT.Fill_Combo(cboFromEntryNo, stQry, "EntryNo", "FabOutwardID");
                    Combobox_Setup_RT.Fill_Combo(cboToEntryNo, stQry, "EntryNo", "FabOutwardID");
                    cboFromEntryNo.SelectedValue = Id;
                    cboToEntryNo.SelectedValue = Id;

                    strQry = "SELECT Distinct FabOutwardID,PartyName, MobileNo,RefNo, EmailID from fn_FetchEntryNoForDeliveryChallanSerial() Where CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "  and EntryNo between " + cboFromEntryNo.Text + " and " + cboToEntryNo.Text + "";
                    using (DataTable Dt = DB.GetDT(strQry, false))
                    {
                        grdAddressBook.RowCount = Dt.Rows.Count;
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            grdAddressBook.Rows[i].Cells[1].Value = Dt.Rows[i]["PartyName"].ToString();
                            grdAddressBook.Rows[i].Cells[2].Value = Dt.Rows[i]["RefNo"].ToString();
                            grdAddressBook.Rows[i].Cells[3].Value = Dt.Rows[i]["EmailID"].ToString();
                            grdAddressBook.Rows[i].Cells[4].Value = Dt.Rows[i]["MobileNo"].ToString();
                            grdAddressBook.Rows[i].Cells[5].Value = Dt.Rows[i]["FabOutwardID"].ToString();
                            if (!isEmail)
                            {
                                grdAddressBook.Columns[4].Visible = true;
                            }
                            else
                            {
                                grdAddressBook.Columns[4].Visible = false;
                            }

                            if (Dt.Rows[i]["EmailID"].ToString() == null || Dt.Rows[i]["EmailID"].ToString() == "")
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = true;
                            }
                            else
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = false;
                            }
                        }

                        cboGroup.Items.Add("PartyName");
                        cboGroup.Items.Add("RefNo");
                        cboGroup.Items.Add("EmailID");
                        cboGroup.Items.Add("MobileNo");
                    }

                    break;
            }

            if (isReportTool)
            {
                lblEmailAcc_R.Visible = true;
                cboFromEmailID_R.Visible = true;
                btnSelect.Visible = true;
                btnSendSms.Visible = false;
            }
            else
            {
                lblEmailAcc_R.Visible = false;
                cboFromEmailID_R.Visible = false;
                btnSelect.Visible = false;
                btnSendSms.Visible = true;
            }

            try
            {
                cboGroup.SelectedIndex = 0;
            }
            catch { }
            isSendsms = false;

            #region Select ALL

            Rectangle rect_DL = grdAddressBook.GetCellDisplayRectangle(0, -1, true);
            rect_DL.Y = 4;
            rect_DL.X = 4;
            checkboxHeader.Name = "chkHeader";
            checkboxHeader.Size = new Size(18, 18);
            checkboxHeader.Location = rect_DL.Location;
            checkboxHeader.CheckedChanged += new EventHandler(chkHeader_CheckedChanged);
            grdAddressBook.Controls.Add(checkboxHeader);

            #endregion
        }

        private void chkHeader_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < grdAddressBook.RowCount; i++)
            {
                grdAddressBook.Rows[i].Cells[0].Value = checkboxHeader.Checked;
            }
            grdAddressBook.EndEdit();
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
            if (CIS_Utilities.CIS_Dialog.Show("Do you want to close this screen ?", GetAssemblyInfo.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Close();
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (isEmail)
            {
                if (cboFromEmailID_R.Text == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "Error", "Please select Email Account which will be used to send email..");
                    return;
                }

                string sEmailIDs = "";

                for (int i = 0; i < grdAddressBook.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)grdAddressBook.Rows[i].Cells[0];
                    if (chk.Value != null)
                    {
                        if (Localization.ParseBoolean(chk.Value.ToString()) == true)
                        {
                            if (isEmail)
                            {
                                if (grdAddressBook.Rows[i].Cells[3].Value.ToString() != "" || grdAddressBook.Rows[i].Cells[2].Value.ToString() != "")
                                    sEmailIDs += grdAddressBook.Rows[i].Cells[3].Value + ":" + grdAddressBook.Rows[i].Cells[5].Value + ":" + grdAddressBook.Rows[i].Cells[2].Value + ";";
                            }

                        }
                    }
                }

                if (sEmailIDs.Length > 0)
                    sEmailIDs = sEmailIDs.Substring(0, sEmailIDs.Length - 1);


                sFromEmailID = cboFromEmailID_R.Text;
                sSubject = txtSubject.Text;
                sToEmailID = sEmailIDs;
                iMailingConfigID = Localization.ParseNativeInt(cboFromEmailID_R.SelectedValue.ToString());
                this.Close();
            }
            else
            {
                string sSms = "";

                for (int i = 0; i < grdAddressBook.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)grdAddressBook.Rows[i].Cells[0];
                    if (chk.Value != null)
                    {
                        if (Localization.ParseBoolean(chk.Value.ToString()) == true)
                        {
                            if (!isEmail)
                            {
                                if (grdAddressBook.Rows[i].Cells[4].Value.ToString() != "" || grdAddressBook.Rows[i].Cells[2].Value.ToString() != "")
                                    sSms += grdAddressBook.Rows[i].Cells[4].Value + ":" + grdAddressBook.Rows[i].Cells[5].Value + ":" + grdAddressBook.Rows[i].Cells[2].Value + ",";
                            }
                        }
                    }
                }
                if (sSms.Length > 0)
                    sSms = sSms.Substring(0, sSms.Length - 1);

                CIS_CLibrary.CIS_Textbox txtToAddress = (CIS_CLibrary.CIS_Textbox)refControl;


                if (!isEmail)
                {
                    txtToAddress.Text = sSms;
                }


            }
        }

        private void txtSearchText_TextChanged(object sender, EventArgs e)
        {
            if (cboGroup.Text != "")
            {
                using (DataTable Dt = DB.GetDT("SELECT FullName, MobileNo, Email, AddressGroup from fn_GeneralAddressBook(" + Db_Detials.CompID + "," + Db_Detials.YearID + ") WHERE Email is not null " + (txtSearchText.Text.Trim().Length > 0 ? "and " + cboGroup.Text + " like '%" + txtSearchText.Text.Trim() + "%'" : ""), false))
                {
                    grdAddressBook.RowCount = Dt.Rows.Count;
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        grdAddressBook.Rows[i].Cells[1].Value = Dt.Rows[i]["FullName"].ToString();
                        grdAddressBook.Rows[i].Cells[2].Value = Dt.Rows[i]["AddressGroup"].ToString();
                        grdAddressBook.Rows[i].Cells[3].Value = Dt.Rows[i]["Email"].ToString();
                        grdAddressBook.Rows[i].Cells[4].Value = Dt.Rows[i]["MobileNo"].ToString();
                    }
                }
            }
        }

        private void lblTop_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(base.Handle, 0xa1, 2, 0);
            }
        }

        private void btnShow_Click(object sender, EventArgs e)
        {
            string strQry = "";
            switch (this.iIDentity)
            {
                case 150:
                    strQry = "SELECT Distinct FabPOID,EntryNo,PartyName, FabPONo,MobileNo, EmailID from [fn_FetchEntryNoFabricPurchaseOrder]() Where CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "  and EntryNo between " + cboFromEntryNo.Text + " and " + cboToEntryNo.Text + "";
                    using (DataTable Dt = DB.GetDT(strQry, false))
                    {
                        grdAddressBook.RowCount = Dt.Rows.Count;
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            grdAddressBook.Rows[i].Cells[1].Value = Dt.Rows[i]["PartyName"].ToString();
                            grdAddressBook.Rows[i].Cells[2].Value = Dt.Rows[i]["BillNo"].ToString(); ;
                            grdAddressBook.Rows[i].Cells[3].Value = Dt.Rows[i]["EmailID"].ToString();
                            grdAddressBook.Rows[i].Cells[4].Value = Dt.Rows[i]["MobileNo"].ToString();
                            grdAddressBook.Rows[i].Cells[5].Value = Dt.Rows[i]["FabPOID"].ToString();
                            if (!isEmail)
                            {
                                grdAddressBook.Columns[4].Visible = true;
                            }
                            else
                            {
                                grdAddressBook.Columns[4].Visible = false;
                            }
                            if (Dt.Rows[i]["EmailID"].ToString() == null || Dt.Rows[i]["EmailID"].ToString() == "")
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = true;
                            }
                            else
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = false;
                            }
                        }
                    }
                    break;

                case 166:
                    strQry = "SELECT Distinct FabSOID,EntryNo,PartyName, FabSONo,MobileNo, EmailID from fn_FetchEntryNoFabricSalesOrder() Where CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "  and EntryNo between " + cboFromEntryNo.Text + " and " + cboToEntryNo.Text + "";
                    using (DataTable Dt = DB.GetDT(strQry, false))
                    {
                        grdAddressBook.RowCount = Dt.Rows.Count;
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            grdAddressBook.Rows[i].Cells[1].Value = Dt.Rows[i]["PartyName"].ToString();
                            grdAddressBook.Rows[i].Cells[2].Value = Dt.Rows[i]["BillNo"].ToString(); ;
                            grdAddressBook.Rows[i].Cells[3].Value = Dt.Rows[i]["EmailID"].ToString();
                            grdAddressBook.Rows[i].Cells[4].Value = Dt.Rows[i]["MobileNo"].ToString();
                            grdAddressBook.Rows[i].Cells[5].Value = Dt.Rows[i]["FabSOID"].ToString();
                            if (!isEmail)
                            {
                                grdAddressBook.Columns[4].Visible = true;
                            }
                            else
                            {
                                grdAddressBook.Columns[4].Visible = false;
                            }
                            if (Dt.Rows[i]["EmailID"].ToString() == null || Dt.Rows[i]["EmailID"].ToString() == "")
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = true;
                            }
                            else
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = false;
                            }
                        }
                    }
                    break;


                case 168:
                    strQry = "SELECT distinct FabOutwardID,PartyName, MobileNo,RefNo, EmailID from Fn_FetchEntryNoFabricDeliveryChallan() Where CompId =" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "  and EntryNo between " + cboFromEntryNo.Text + " and " + cboToEntryNo.Text + "";

                    using (DataTable Dt = DB.GetDT(strQry, false))
                    {
                        grdAddressBook.RowCount = Dt.Rows.Count;
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            grdAddressBook.Rows[i].Cells[1].Value = Dt.Rows[i]["PartyName"].ToString();
                            grdAddressBook.Rows[i].Cells[2].Value = Dt.Rows[i]["RefNo"].ToString();
                            grdAddressBook.Rows[i].Cells[3].Value = Dt.Rows[i]["EmailID"].ToString();
                            grdAddressBook.Rows[i].Cells[4].Value = Dt.Rows[i]["MobileNo"].ToString();
                            grdAddressBook.Rows[i].Cells[5].Value = Dt.Rows[i]["FabOutwardID"].ToString();
                            if (!isEmail)
                            {
                                grdAddressBook.Columns[4].Visible = true;
                            }
                            else
                            {
                                grdAddressBook.Columns[4].Visible = false;
                            }
                            if (Dt.Rows[i]["EmailID"].ToString() == null || Dt.Rows[i]["EmailID"].ToString() == "")
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = true;
                            }
                            else
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = false;
                            }
                        }

                    }

                    break;

                case 470:
                    strQry = "SELECT Distinct FabSOID,EntryNo,PartyName, MobileNo,FabSONo, EmailID from fn_FetchEntryNoFabricSalesOrderSerial() Where CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "  and EntryNo between " + cboFromEntryNo.Text + " and " + cboToEntryNo.Text + "";
                    using (DataTable Dt = DB.GetDT(strQry, false))
                    {
                        grdAddressBook.RowCount = Dt.Rows.Count;
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            grdAddressBook.Rows[i].Cells[1].Value = Dt.Rows[i]["PartyName"].ToString();
                            grdAddressBook.Rows[i].Cells[2].Value = Dt.Rows[i]["FabSONo"].ToString();
                            grdAddressBook.Rows[i].Cells[3].Value = Dt.Rows[i]["EmailID"].ToString();
                            grdAddressBook.Rows[i].Cells[4].Value = Dt.Rows[i]["MobileNo"].ToString();
                            grdAddressBook.Rows[i].Cells[5].Value = Dt.Rows[i]["FabSOID"].ToString();
                            if (!isEmail)
                            {
                                grdAddressBook.Columns[4].Visible = true;
                            }
                            else
                            {
                                grdAddressBook.Columns[4].Visible = false;
                            }
                            if (Dt.Rows[i]["EmailID"].ToString() == null || Dt.Rows[i]["EmailID"].ToString() == "")
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = true;
                            }
                            else
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = false;
                            }
                        }

                    }

                    break;

                case 473:
                    strQry = "SELECT Distinct SalesID,EntryNo,PartyName,BillNo, MobileNo, EmailID from fn_FabricInvoiceMain2(" + Db_Detials.CompID + "," + Db_Detials.YearID + ") where EntryNo BEtween " + cboFromEntryNo.Text + " and " + cboToEntryNo.Text + " ";
                    using (DataTable Dt = DB.GetDT(strQry, false))
                    {
                        grdAddressBook.RowCount = Dt.Rows.Count;
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            grdAddressBook.Rows[i].Cells[1].Value = Dt.Rows[i]["PartyName"].ToString();
                            grdAddressBook.Rows[i].Cells[2].Value = Dt.Rows[i]["BillNo"].ToString();
                            grdAddressBook.Rows[i].Cells[3].Value = Dt.Rows[i]["EmailID"].ToString();
                            grdAddressBook.Rows[i].Cells[4].Value = Dt.Rows[i]["MobileNo"].ToString();
                            grdAddressBook.Rows[i].Cells[5].Value = Dt.Rows[i]["SalesID"].ToString();
                            if (!isEmail)
                            {
                                grdAddressBook.Columns[4].Visible = true;
                            }
                            else
                            {
                                grdAddressBook.Columns[4].Visible = false;
                            }
                            if (Dt.Rows[i]["EmailID"].ToString() == null || Dt.Rows[i]["EmailID"].ToString() == "")
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = true;
                            }
                            else
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = false;
                            }
                        }
                    }

                    break;

                case 171:
                    strQry = "SELECT distinct SalesID,PartyName, MobileNo,BillNo,EmailID from fn_FetchEntryForFabricInvoice() Where CompId =" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "  and EntryNo between " + cboFromEntryNo.Text + " and " + cboToEntryNo.Text + "";

                    using (DataTable Dt = DB.GetDT(strQry, false))
                    {
                        grdAddressBook.RowCount = Dt.Rows.Count;
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            grdAddressBook.Rows[i].Cells[1].Value = Dt.Rows[i]["PartyName"].ToString();
                            grdAddressBook.Rows[i].Cells[2].Value = Dt.Rows[i]["BillNo"].ToString();
                            grdAddressBook.Rows[i].Cells[3].Value = Dt.Rows[i]["EmailID"].ToString();
                            grdAddressBook.Rows[i].Cells[4].Value = Dt.Rows[i]["MobileNo"].ToString();
                            grdAddressBook.Rows[i].Cells[5].Value = Dt.Rows[i]["SalesID"].ToString();
                            if (!isEmail)
                            {
                                grdAddressBook.Columns[4].Visible = true;
                            }
                            else
                            {
                                grdAddressBook.Columns[4].Visible = false;
                            }
                            if (Dt.Rows[i]["EmailID"].ToString() == null || Dt.Rows[i]["EmailID"].ToString() == "")
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = true;
                            }
                            else
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = false;
                            }
                        }

                    }

                    break;

                case 472:
                    strQry = "SELECT Distinct FabOutwardID,EntryNo,RefNo,PartyName, MobileNo, EmailID from fn_FetchEntryNoForDeliveryChallanSerial() Where CompID=" + Db_Detials.CompID + " and YearID=" + Db_Detials.YearID + "  and EntryNo between " + cboFromEntryNo.Text + " and " + cboToEntryNo.Text + "";

                    using (DataTable Dt = DB.GetDT(strQry, false))
                    {
                        grdAddressBook.RowCount = Dt.Rows.Count;
                        for (int i = 0; i < Dt.Rows.Count; i++)
                        {
                            grdAddressBook.Rows[i].Cells[1].Value = Dt.Rows[i]["PartyName"].ToString();
                            grdAddressBook.Rows[i].Cells[2].Value = Dt.Rows[i]["RefNo"].ToString();
                            grdAddressBook.Rows[i].Cells[3].Value = Dt.Rows[i]["EmailID"].ToString();
                            grdAddressBook.Rows[i].Cells[4].Value = Dt.Rows[i]["MobileNo"].ToString();
                            grdAddressBook.Rows[i].Cells[5].Value = Dt.Rows[i]["FabOutwardID"].ToString();
                            if (!isEmail)
                            {
                                grdAddressBook.Columns[4].Visible = true;
                            }
                            else
                            {
                                grdAddressBook.Columns[4].Visible = false;
                            }
                            if (Dt.Rows[i]["EmailID"].ToString() == null || Dt.Rows[i]["EmailID"].ToString() == "")
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = true;
                            }
                            else
                            {
                                grdAddressBook.Rows[i].Cells[0].ReadOnly = false;
                            }
                        }

                    }

                    break;
            }
        }

        private void btnSendSMS_Click(object sender, EventArgs e)
        {
            string sSms = "";

            for (int i = 0; i < grdAddressBook.Rows.Count; i++)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)grdAddressBook.Rows[i].Cells[0];
                if (chk.Value != null)
                {
                    if (Localization.ParseBoolean(chk.Value.ToString()) == true)
                    {
                        if (!isEmail)
                        {
                            if (grdAddressBook.Rows[i].Cells[4].Value.ToString() != "" || grdAddressBook.Rows[i].Cells[2].Value.ToString() != "")
                                sSms += grdAddressBook.Rows[i].Cells[4].Value + ":" + grdAddressBook.Rows[i].Cells[5].Value + ":" + grdAddressBook.Rows[i].Cells[2].Value + ",";
                        }
                    }
                }
            }
            if (sSms.Length > 0)
                sSms = sSms.Substring(0, sSms.Length - 1);

            CIS_CLibrary.CIS_Textbox txtToAddress = (CIS_CLibrary.CIS_Textbox)refControl;

            if (!isEmail)
            {
                txtToAddress.Text = sSms;
            }
            isSendsms = true;
            this.Close();
        }
    }
}
