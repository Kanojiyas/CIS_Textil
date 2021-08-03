using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using  CIS_Bussiness;using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmCompanyMaster : frmMasterIface
    {
        private static string imagepath = "";
        string id = string.Empty;

        private static string imagepathLogo = "";
        string idLogo = string.Empty;

        public frmCompanyMaster()
        {
            base.Load += new EventHandler(this.frmCompanyMaster_Load);
            InitializeComponent();
        }

        private void frmCompanyMaster_Load(object sender, EventArgs e)
        {
            try
            {
                CommonCls.AutoCompleteText(this.strTableName, "CompanyName", ref txtName);
                CommonCls.AutoCompleteText(this.strTableName, "AliasName", ref txtAliasName);
                CommonCls.AutoCompleteText(this.strTableName, "M_PostalCode", ref txtPostal_O);
                CommonCls.AutoCompleteText(this.strTableName, "C_PostalCode", ref txtCorPincode_C);
                CommonCls.AutoCompleteText(this.strTableName, "F_PostalCode", ref txtPostalCode_F);

                Combobox_Setup.FillCbo(ref cboCityName_F, Combobox_Setup.ComboType.Mst_City);
                Combobox_Setup.FillCbo(ref cboStateName_F, Combobox_Setup.ComboType.Mst_State);
                Combobox_Setup.FillCbo(ref cboCity_O, Combobox_Setup.ComboType.Mst_City);
                Combobox_Setup.FillCbo(ref CboState_O, Combobox_Setup.ComboType.Mst_State);
                Combobox_Setup.FillCbo(ref cboCorCity_C, Combobox_Setup.ComboType.Mst_City);
                Combobox_Setup.FillCbo(ref cboCorState_C, Combobox_Setup.ComboType.Mst_State);
                Combobox_Setup.FillCbo(ref cboBank1, Combobox_Setup.ComboType.Mst_Bank);
                Combobox_Setup.FillCbo(ref cboBank2, Combobox_Setup.ComboType.Mst_Bank);
                Combobox_Setup.FillCbo(ref cboBank3, Combobox_Setup.ComboType.Mst_Bank);

                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    FillControls();
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "CompanyID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtName, "CompanyName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtTitle1, "Title1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtTitle2, "Title2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAddress_O, "M_Add1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, CboState_O, "M_StateID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboCity_O, "M_CityID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPostal_O, "M_PostalCode", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPhone_O, "M_ContactNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtFax_F, "M_FaxNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtWebSite_O, "M_Website", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEmailID_O, "M_EmailID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMobileNo_O, "M_MobNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCorAdd_C, "C_Add1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboCorState_C, "C_StateID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboCorCity_C, "C_CityID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCorPincode_C, "C_PostalCode", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCorMob_C, "C_MobNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCorPhnNo_C, "C_ContactNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCorFax_C, "C_FaxNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAddress_F, "F_Add1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboStateName_F, "F_StateID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboCityName_F, "F_CityID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPostalCode_F, "F_PostalCode", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMobileNo_F, "F_MobNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPhoneNo_F, "F_ContactNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtFaxNo_O, "F_FaxNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPanNo, "PANNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtTDSNo, "TDSNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtSrvcTaxNo, "ServiceTaxNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtSrvcTaxDat, "ServiceTaxDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtCstTinNO, "CSTTinNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCSTDat, "CSTDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtVatTinNo, "VatTinNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtVATTINDat, "VatTinDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtEccNO, "EccNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEccDat, "EccDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboBank1, "BankID1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAccNo1, "AccNo1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBank2, "BankID2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAccNo2, "AccNo2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBank3, "BankID3", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAccNo3, "AccNo3", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCINNo, "CINNO", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtIncorporationDate, "IncorporationDt", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, dtBkbegnFrmDt, "BooksBeginningDt", Enum_Define.ValidationType.IsDate);

                imagepath = DBValue.Return_DBValue(this, "ImagePath");
                imagepathLogo = DBValue.Return_DBValue(this, "ImagePathLogo");
                GetImage(txtCode.Text);
                GetImageLogo(txtCode.Text);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        public void SaveRecord()
        {
            try
            {
                System.Collections.ArrayList ArrayData = new System.Collections.ArrayList
                {
                   txtName.Text,
                   txtAliasName.Text,
                   txtTitle1.Text,
                   txtTitle2.Text,
                   txtAddress_O.Text,
                   "",
                   cboCity_O.SelectedValue,
                   CboState_O.SelectedValue,
                   txtPostal_O.Text,
                   txtWebSite_O.Text,
                   txtEmailID_O.Text,
                   txtMobileNo_O.Text,
                   txtPhone_O.Text,
                   txtFaxNo_O.Text,
                   txtCorAdd_C.Text,
                   "",
                   cboCorCity_C.SelectedValue,
                   cboCorState_C.SelectedValue,
                   txtCorPincode_C.Text,
                   "",
                   txtCorMob_C.Text,
                   txtCorPhnNo_C.Text,
                   txtCorFax_C.Text,
                   txtAddress_F.Text,
                   "",
                   cboCityName_F.SelectedValue,
                   cboStateName_F.SelectedValue,
                   txtPostalCode_F.Text,
                   "",
                   txtMobileNo_F.Text,
                   txtPhoneNo_F.Text,
                   txtFax_F.Text,
                   txtPanNo.Text,
                   txtTDSNo.Text,
                   txtSrvcTaxNo.Text,
                   txtSrvcTaxDat.TextFormat(),
                   txtCstTinNO.Text,
                   txtCSTDat.TextFormat(),
                   txtVatTinNo.Text,
                   txtVATTINDat.TextFormat(),
                   txtEccNO.Text,
                   txtEccDat.TextFormat(),
                   cboBank1.SelectedValue,
                   txtAccNo1.Text,
                   cboBank2.SelectedValue,
                   txtAccNo2.Text,
                   cboBank3.SelectedValue,
                   txtAccNo3.Text,
                   null, txtImagePath.Text,
                   null,txtImagePathLogo.Text,
                   txtCINNo.Text,
                   dtIncorporationDate.TextFormat(),
                   dtBkbegnFrmDt.TextFormat()
                };

                string status = string.Empty;
                if (blnFormAction == Enum_Define.ActionType.New_Record)
                    status = "New";
                else if (blnFormAction == Enum_Define.ActionType.Edit_Record)
                    status = "Edit";

                string id = "";
                if (status == "New")
                {
                    id = DB.GetSnglValue("select max(CompanyID) from tbl_CompanyMaster");
                    imagepath = DB.GetSnglValue("select ImagePath from tbl_CompanyMaster where CompanyID=" + id + "");
                    imagepathLogo = DB.GetSnglValue("select ImagePathLogo from tbl_CompanyMaster where CompanyID=" + id + "");
                }
                else
                    id = txtCode.Text;

                string sImgPth = imagepath;
                string sImgPthLogo = imagepathLogo;

                double dblID = DBSp.Master_AddEdit(ArrayData);
                CommonCls.InsertGlobalSetings(Localization.ParseNativeInt(dblID.ToString()));
                //imagepath = DB.GetSnglValue("select ImagePath from tbl_GeneralAddressBook where AddressID=" + id + "");
                if (sImgPth != "")
                {
                    ImageStore(sImgPth);
                }
                if (sImgPthLogo != "")
                {
                    ImageStoreLogo(sImgPthLogo);
                }
                if (status == "Edit")
                {
                    FillControls();
                }
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
                if (txtName.Text.Trim().Length <= 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Party Name");
                    return true;
                }

                if ((txtAddress_O.Text.Trim().Length) <= 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Address");
                    txtAddress_O.Focus();
                    return true;
                }

                if ((CboState_O.SelectedValue == null) || (Localization.ParseNativeInt(CboState_O.SelectedValue.ToString()) <= 0))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select State");
                    CboState_O.Focus();
                    return true;
                }

                if ((cboCity_O.SelectedValue == null) || (Localization.ParseNativeInt(cboCity_O.SelectedValue.ToString()) <= 0))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select City");
                    cboCity_O.Focus();
                    return true;
                }

                if (txtPostal_O.Text.Trim().Length <= 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Postal Code.");
                    txtPostal_O.Focus();
                    return true;
                }

                if (txtPhone_O.Text.Trim().Length <= 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Phone no.");
                    txtPhone_O.Focus();
                    return true;
                }

                return false;

            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                return false;
            }
        }

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

        private void btnBrowseLogo_Click(object sender, EventArgs e)
        {
            //Ask user to select file.
            OpenFileDialog dlg = new OpenFileDialog();
            DialogResult dlgRes = dlg.ShowDialog();
            if (dlgRes != DialogResult.Cancel)
            {
                //Set image in picture box
                pctLogo.ImageLocation = dlg.FileName;

                //Provide file path in txtImagePath text box.
                txtImagePathLogo.Text = dlg.FileName;
                imagepathLogo = txtImagePathLogo.Text;
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                //Read Image Bytes into a byte array
                byte[] imageData = ReadFile(imagepath);

                //Initialize SQL Server Connection
                SqlConnection CN = new SqlConnection(DB.GetDBConn());

                //Set insert query
                string AddressID = string.Empty;
                if (txtCode.Text != "")
                {
                    AddressID = txtCode.Text;
                }
                else
                {
                    AddressID = DB.GetSnglValue("select max(AddressID) from tbl_GeneralAddressBook");
                }
                string qry = "Update tbl_GeneralAddressBook Set Image=@ImageData, Imagepath=@OriginalPath  where AddressID=" + AddressID + " ";

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
                MessageBox.Show(ex.ToString());
            }
        }

        public void ImageStore(string sImagePath)
        {
            try
            {
                //Read Image Bytes into a byte array
                byte[] imageData = ReadFile(sImagePath);

                //Initialize SQL Server Connection
                SqlConnection CN = new SqlConnection(DB.GetDBConn());

                //Set insert query
                string CompanyID = string.Empty;
                if (txtCode.Text != "")
                {
                    CompanyID = txtCode.Text;
                }
                else
                {
                    CompanyID = DB.GetSnglValue("select max(CompanyID) from tbl_CompanyMaster");
                }
                string qry = "Update tbl_CompanyMaster Set Image=@ImageData, Imagepath=@OriginalPath  where CompanyID=" + CompanyID + " ";

                //Initialize SqlCommand object for insert.
                SqlCommand SqlCom = new SqlCommand(qry, CN);

                //We are passing Original Image Path and Image byte data as sql parameters.
                SqlCom.Parameters.Add(new SqlParameter("@OriginalPath", (object)sImagePath));
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

        public void GetImage(string CompanyID)
        {
            try
            {
                DataTable Dt = DB.GetDT("select Image from tbl_CompanyMaster where CompanyID=" + CompanyID, false);
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
            catch { pctImage.Image = CIS_Textil.Properties.Resources.no_image; }
        }

        public void ImageStoreLogo(string sImagePath)
        {
            try
            {
                //Read Image Bytes into a byte array
                byte[] imageData = ReadFile(sImagePath);

                //Initialize SQL Server Connection
                SqlConnection CN = new SqlConnection(DB.GetDBConn());

                //Set insert query
                string CompanyID = string.Empty;
                if (txtCode.Text != "")
                {
                    CompanyID = txtCode.Text;
                }
                else
                {
                    CompanyID = DB.GetSnglValue("select max(CompanyID) from tbl_CompanyMaster");
                }
                string qry = "Update tbl_CompanyMaster Set ImageLogo=@ImageData, ImagepathLogo=@OriginalPath  where CompanyID=" + CompanyID + " ";

                //Initialize SqlCommand object for insert.
                SqlCommand SqlCom = new SqlCommand(qry, CN);

                //We are passing Original Image Path and Image byte data as sql parameters.
                SqlCom.Parameters.Add(new SqlParameter("@OriginalPath", (object)sImagePath));
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

        public void GetImageLogo(string CompanyID)
        {
            try
            {
                DataTable Dt = DB.GetDT("select ImageLogo from tbl_CompanyMaster where CompanyID=" + CompanyID, false);
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
                    pctLogo.Image = newImage;
                }
                else
                {
                    pctLogo.Image = CIS_Textil.Properties.Resources.no_image;
                }
            }
            catch { pctLogo.Image = CIS_Textil.Properties.Resources.no_image; }
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

        private void CboState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((CboState_O.Text != null) && (Localization.ParseNativeDouble(CboState_O.SelectedValue.ToString()) > 0.0))
                {
                    Combobox_Setup.FillCbo(ref cboCity_O, Combobox_Setup.ComboType.Mst_City, " StateID=" + CboState_O.SelectedValue.ToString() + "");
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboStateName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((cboStateName_F.SelectedValue != null) && (Localization.ParseNativeDouble(cboStateName_F.SelectedValue.ToString()) > 0.0))
                {
                    Combobox_Setup.FillCbo(ref cboCityName_F, Combobox_Setup.ComboType.Mst_City, " StateID=" + cboStateName_F.SelectedValue.ToString() + "");
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void txtPanNo_Leave(object sender, EventArgs e)
        {
            try
            {
                System.Text.RegularExpressions.Regex rPan =
                    new System.Text.RegularExpressions.Regex(@"^([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}?$");
                if (txtPanNo.Text.Length > 0)
                {
                    if (!rPan.IsMatch(txtPanNo.Text))
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Invalid PAN Card Number");
                        txtPanNo.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
    }
}
