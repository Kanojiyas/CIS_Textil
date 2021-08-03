using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using  CIS_Bussiness;using CIS_DBLayer;
using CIS_CLibrary;
using Microsoft.VisualBasic;

namespace CIS_Textil
{
    public partial class frmUserMaster : frmMasterIface
    {
        private static string imagepath = "";
        string id = string.Empty;
        string strOtp = string.Empty;

        public frmUserMaster()
        {
            InitializeComponent();
        }

        #region Events

        private void frm_Load(object sender, EventArgs e)
        {
            try
            {
                CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cboUserType = this.CboUserType;
                Combobox_Setup.FillCbo(ref cboUserType, Combobox_Setup.ComboType.Mst_UserType, "");
               
                this.CboUserType = cboUserType;
                txtMobileNo.Visible = false;
                lblEmail.Visible = false;
                lblMobileNo.Visible = false;
                lblColMobile.Visible = false;
                lblColEmail.Visible = false;
                txtEmailID.Visible = false;
                FillCompGrid();
                FillGrid(Localization.ParseNativeInt(txtCode.Text));
                FillControls();
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #endregion

        #region Navigation

        public void ApplyActStatus()
        {
            if (base.blnFormAction == Enum_Define.ActionType.New_Record)
            {
                ChkActive.Checked = true;
                ChkActive.Visible = false;
            }
            else
            {
                ChkActive.Visible = true;
            }
        }

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "UserId", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtUserName, "UserName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPassword, "Password", Enum_Define.ValidationType.Text);
                this.txtPassword.Text = CommonLogic.UnmungeString(txtPassword.Text);
                DBValue.Return_DBValue(this, txtEmployeeName, "EmployeeName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEmpID, "EmployeeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, CboUserType, "UserType", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtImagePath, "ImagePath", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, chkShowPanelStock, "ShowStockPanel", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtGroupName, "GroupName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, chkAutoGenPwd, "IsUserOtp", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMobileNo, "MobileNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEmailID, "EmailID", Enum_Define.ValidationType.Text);
                GetImage(txtCode.Text);
                FillGrid(Localization.ParseNativeInt(txtCode.Text));
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
            ApplyActStatus();
        }

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
               
                object frm = this;
                pctImage.Image = null;
                pctImage.Image = CIS_Textil.Properties.Resources.no_image;
                imagepath = "";
                chkAutoGenPwd.Checked = false;

                CommonCls.AutoCompleteText(this.strTableName, "UserName", ref txtUserName);
                CommonCls.AutoCompleteText(this.strTableName, "EmployeeName", ref txtEmployeeName);
                ApplyActStatus();
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
            
        }

        public void SaveRecord()
        {
            try
            {
                ArrayList pArrayData = new ArrayList();
                pArrayData.Add(this.txtUserName.Text.Trim());
                pArrayData.Add(CommonLogic.MungeString(this.txtPassword.Text.Trim()));
                pArrayData.Add(txtEmployeeName.Text);
                pArrayData.Add(txtEmpID.Text);
                pArrayData.Add(CboUserType.SelectedValue);
                pArrayData.Add(this.txtImagePath.Text.Trim());
                pArrayData.Add("");
                pArrayData.Add(txtGroupName.Text);
                pArrayData.Add(RuntimeHelpers.GetObjectValue(Interaction.IIf(this.ChkActive.Checked, 1, 0)));
                pArrayData.Add(RuntimeHelpers.GetObjectValue(Interaction.IIf(this.chkShowPanelStock.Checked, 1, 0)));
                pArrayData.Add(0);
                pArrayData.Add(0);
                pArrayData.Add(this.txtMobileNo.Text);
                pArrayData.Add(this.txtEmailID.Text);
                pArrayData.Add(CommonLogic.MungeString(this.strOtp.Trim()));
                pArrayData.Add(Localization.ToSqlDateString(DateTime.Now.Date.ToString()));
                pArrayData.Add(RuntimeHelpers.GetObjectValue(Interaction.IIf(this.chkAutoGenPwd.Checked, 1, 0)));
                pArrayData.Add(0);
                pArrayData.Add(0);
                pArrayData.Add(0);
                pArrayData.Add(null);
                pArrayData.Add(null);
                pArrayData.Add(null);

                string strDtlQry = string.Empty;
                if (dgvCmpSelection.Rows.Count > 0)
                {
                    strDtlQry += string.Format("Delete From tbl_UserMasterDtls Where UserID={0};", "(#CodeID#)");
                    for (int i = 0; i <= (dgvCmpSelection.Rows.Count - 1); i++)
                    {
                        DataGridViewCheckBoxCell chk = dgvCmpSelection.Rows[i].Cells[2] as DataGridViewCheckBoxCell;
                        if (chk.Value != null)
                        {
                            if (Localization.ParseBoolean(chk.Value.ToString()))
                            {
                                strDtlQry += string.Format("Insert into tbl_UserMasterDtls values({0},{1},{2},{3})", "(#CodeID#)", Db_Detials.StoreID, dgvCmpSelection.Rows[i].Cells[0].Value, Db_Detials.BranchID);
                            }
                        }
                    }
                }
                string status = string.Empty;
                double dblTransID = 0;
                if (blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    status = "New";
                }

                else if (blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    status = "Edit";
                }
                DBSp.Master_AddEdit(pArrayData, strDtlQry);

                if (blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    dblTransID = Localization.ParseNativeDouble(DB.GetSnglValue("Select Top 1 UserID From fn_UserMaster_tbl() Order by UserID Desc"));
                }

                else if (blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    dblTransID = Localization.ParseNativeDouble(txtCode.Text);
                }

                if (status == "New")
                {
                    id = DB.GetSnglValue("select Max(UserID) from tbl_UserMaster");

                    try { CommonCls.SendSms(dblTransID.ToString(), base.iIDentity.ToString(),Localization.ParseNativeInt(dblTransID.ToString()), "0"); }
                    catch { }

                    imagepath = DB.GetSnglValue("select ImagePath from tbl_UserMaster where UserID=" + id + "");
                }
                else
                    id = txtCode.Text;
                if (imagepath != "")
                {
                    ImageStore(id);
                }
                if (status == "Edit")
                {
                    FillControls();
                }
                this.IsMasterAdded = true;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                Control txtUserName = this.txtUserName;
                this.txtUserName = (CIS_Textbox)txtUserName;
                if (Navigate.CheckValid(txtUserName, Enum_Define.ValidationType.Text, "User Name"))
                {
                    return true;
                }
                txtUserName = this.txtPassword;
                this.txtPassword = (CIS_Textbox)txtUserName;
                if (Navigate.CheckValid(txtUserName, Enum_Define.ValidationType.Text, "Password"))
                {
                    return true;
                }
                txtUserName = this.CboUserType;
                if (Navigate.CheckValid(txtUserName, Enum_Define.ValidationType.IsZero, "User Type"))
                {
                    return true;
                }

                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) || (base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    if (txtEmployeeName.Text.Trim() == "NULL" || txtEmployeeName.Text.Trim() == "" || txtEmployeeName.Text.Trim() == "0")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Employee Name");
                        txtEmployeeName.Focus();
                        return true;
                    }
                }
                bool IsValidSelect = false;
                for (int i = 0; i < dgvCmpSelection.Rows.Count; i++)
                {
                    DataGridViewCheckBoxCell chk = dgvCmpSelection.Rows[i].Cells[2] as DataGridViewCheckBoxCell;
                    if (chk.Value != null)
                    {
                        if (chk.Value.ToString() == "True")
                        {
                            IsValidSelect = true;
                            break;
                        }
                    }
                }

                if (DBSp.rtnAction())
                {
                    strTableName = "tbl_UserMaster";
                    if (Navigate.CheckDuplicate(ref strTableName, "UserName", this.txtUserName.Text.Trim(), false, "", 0L, "", ""))
                    {
                        this.txtUserName.Focus();
                        return false;
                    }
                }
                else
                {
                    strTableName = "tbl_UserMaster";
                    if (Navigate.CheckDuplicate(ref strTableName, "UserName", this.txtUserName.Text.Trim(), true, "UserID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", ""))
                    {
                        this.txtUserName.Focus();
                        return false;
                    }
                }

                if (IsValidSelect == false)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
            return false;
        }

        public void FillCompGrid()
        {
            try
            {
                DataTable dt = DB.GetDT("Select CompanyID,CompanyName From tbl_companymaster", false);
                DataGridViewTextBoxColumn txtCompID = new DataGridViewTextBoxColumn();
                txtCompID.Visible = false;
                DataGridViewTextBoxColumn txtCompName = new DataGridViewTextBoxColumn();
                txtCompName.HeaderText = "Company";
                txtCompName.Width = 380;
                DataGridViewCheckBoxColumn chkSelect = new DataGridViewCheckBoxColumn();
                chkSelect.HeaderText = "Select";
                chkSelect.Width = 95;
                dgvCmpSelection.Columns.Clear();
                dgvCmpSelection.Rows.Clear();
                dgvCmpSelection.Columns.Add(txtCompID);
                dgvCmpSelection.Columns.Add(txtCompName);
                dgvCmpSelection.Columns.Add(chkSelect);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dgvCmpSelection.Rows.Add();
                        dgvCmpSelection.Rows[i].Cells[0].Value = dt.Rows[i]["CompanyID"].ToString();
                        dgvCmpSelection.Rows[i].Cells[1].Value = dt.Rows[i]["CompanyName"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public void FillGrid(int PmryID)
        {
            try
            {
                FillCompGrid();
                DataTable dt = DB.GetDT("Select * From tbl_UserMasterDtls where UserID =" + PmryID + "", false);
                if (dgvCmpSelection.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvCmpSelection.Rows.Count; i++)
                    {
                        DataRow[] dr = dt.Select("CompID=" + dgvCmpSelection.Rows[i].Cells[0].Value.ToString());
                        if (dr.Length > 0)
                        {
                            DataGridViewCheckBoxCell chk = dgvCmpSelection.Rows[i].Cells[2] as DataGridViewCheckBoxCell;
                            //if (chk.Value != null)
                            {
                                chk.Value = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #endregion

        #region ImgSaving

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            //Ask user to select file.
            OpenFileDialog dlg = new OpenFileDialog();
            DialogResult dlgRes = dlg.ShowDialog();
            if (dlgRes != DialogResult.Cancel)
            {
                //Set image in picture box
                pctImage.ImageLocation = dlg.FileName;

                //Provide file path in txtImagePath text box.
                txtImagePath.Text = dlg.FileName;
                imagepath = txtImagePath.Text;
            }
        }

        public void ImageStore(string UserMasterImgID)
        {
            try
            {
                //Read Image Bytes into a byte array
                byte[] imageData = ReadFile(imagepath);

                //Initialize SQL Server Connection
                SqlConnection CN = new SqlConnection(DB.GetDBConn());

                //Set insert query
                string UserID = string.Empty;
                if (txtCode.Text != "")
                {
                    UserID = UserMasterImgID;
                }
                else
                {
                    UserID = DB.GetSnglValue("select max(UserID) from tbl_UserMaster");
                }
                string qry = "Update tbl_UserMaster Set Image=@ImageData, Imagepath=@OriginalPath  where UserID=" + UserID + " ";

                //Initialize SqlCommand object for insert.
                SqlCommand SqlCom = new SqlCommand(qry, CN);

                //We are passing Original Image Path and Image byte data as sql parameters.
                SqlCom.Parameters.Add(new SqlParameter("@OriginalPath", (object)imagepath));
                SqlCom.Parameters.Add(new SqlParameter("@ImageData", (object)imageData));

                CN.Open();
                SqlCom.ExecuteNonQuery();
                CN.Close();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public void GetImage(string UserID)
        {
            try
            {
                DataTable Dt = DB.GetDT("select Image from tbl_UserMaster where UserID=" + UserID, false);
                if (Dt.Rows.Count > 0)
                {
                    byte[] imageData = (byte[])Dt.Rows[0][0];
                    //Get image data from gridview column.
                    //Initialize image variable
                    Image newImage;
                    //Read image data into a memory stream
                    using (MemoryStream ms = new MemoryStream(imageData, 0, imageData.Length))
                    {
                        ms.Write(imageData, 0, imageData.Length);

                        //Set image variable value using memory stream.
                        newImage = Image.FromStream(ms, true);
                    }

                    //set picture
                    pctImage.Image = newImage;
                }
                else
                {
                    pctImage.Image = CIS_Textil.Properties.Resources.no_image;
                }
            }
            catch (Exception ex) 
            { 
                Navigate.logError(ex.Message, ex.StackTrace);
                pctImage.Image = CIS_Textil.Properties.Resources.no_image;
            }
        }

        byte[] ReadFile(string sPath)
        {
            byte[] data = null;
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            data = br.ReadBytes((int)numBytes);
            return data;
        }

        #endregion

        private void chkAutoGenPwd_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoGenPwd.Checked == true)
            {
                txtMobileNo.Visible = true;
                lblEmail.Visible = true;
                lblMobileNo.Visible = true;
                lblColMobile.Visible = true;
                lblColEmail.Visible = true;
                txtEmailID.Visible = true;
                GenOtp();
            }
            else
            {
                txtMobileNo.Visible = false;
                lblEmail.Visible = false;
                lblMobileNo.Visible = false;
                lblColMobile.Visible = false;
                lblColEmail.Visible = false;
                txtEmailID.Visible = false;
            }
        }

        public string GenOtp()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz@$";
            Random random = new Random();
            string result = new string(
            Enumerable.Repeat(chars, 10)
                      .Select(s => s[random.Next(s.Length)])
                      .ToArray());
            strOtp = result.ToString();
            return strOtp;
        }
    }
}
