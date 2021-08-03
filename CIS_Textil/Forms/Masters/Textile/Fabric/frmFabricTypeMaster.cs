using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;
using CIS_CLibrary;
using CIS_Utilities;

namespace CIS_Textil
{
    public partial class frmFabricTypeMaster : frmMasterIface
    {
        private static string imagepath = "";
        string id = string.Empty;

        public frmFabricTypeMaster()
        {
            InitializeComponent();
        }

        private void frmFabricTypeMaster_Load(object sender, EventArgs e)
        {
            try
            {
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtFabTypeName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtFabTypeName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtFabTypeName.Focus();
                        this.isComboAddText = true;
                        this.isSecondMessage = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

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
                DBValue.Return_DBValue(this, txtCode, "FabricTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtFabTypeName, "FabricType", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtImagePath, "ImagePath", Enum_Define.ValidationType.Text);
                imagepath = DBValue.Return_DBValue(this, "ImagePath");
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);
                GetImage(txtCode.Text);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
            ApplyActStatus();
        }

        public void SaveRecord()
        {
            try
            {
                sComboAddText = txtFabTypeName.Text;
                ArrayList pArrayData = new ArrayList
                {
                    txtFabTypeName.Text.Trim(),
                    txtAliasName.Text == "" ? null : txtAliasName.Text,
                    (ChkActive.Checked ? 1 : 0),
                    null,
                    txtImagePath.Text,
                    (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                    (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                    (txtET1.Text.Trim()),
                    (txtET2.Text.Trim()),
                    (txtET3.Text.Trim())
                };
                string status = string.Empty;
                if (blnFormAction == Enum_Define.ActionType.New_Record)
                    status = "New";
                else if (blnFormAction == Enum_Define.ActionType.Edit_Record)
                    status = "Edit";
                DBSp.Master_AddEdit(pArrayData, "");
                if (status == "New")
                {
                    id = DB.GetSnglValue("select Max(FabricTypeID) from " + Db_Detials.tbl_FabricTypeMaster + "");

                    imagepath = DB.GetSnglValue("select ImagePath from "  + Db_Detials.tbl_FabricTypeMaster + " where FabricTypeID=" + id + "");
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
            catch (Exception exception1)
            {
                this.IsMasterAdded = false;
                Navigate.logError(exception1.Message, exception1.StackTrace);
                Navigate.ShowMessage(CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                string strTable =  Db_Detials.tbl_FabricTypeMaster;
                if (txtFabTypeName.Text.Trim() == "" || txtFabTypeName.Text.Trim() == "-" || txtFabTypeName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_DialogIcon.Error, "", "Please Enter Fabric Type");
                    txtFabTypeName.Focus();
                    return true;
                }
                if (DBSp.rtnAction())
                {
                    
                    if (Navigate.CheckDuplicate(ref strTable, "FabricType", txtFabTypeName.Text.Trim(), false, "", 0, "", "This FabricType is already available"))
                    {
                        txtFabTypeName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTable, "AliasName", txtAliasName.Text.Trim(), false, "", 0, "", "This AliasName is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "AliasName", txtFabTypeName.Text.Trim(), false, "", 0, "", "This FabricType is already Used in AliasName"))
                    {
                        txtFabTypeName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTable, "FabricType", txtAliasName.Text.Trim(), false, "", 0, "", "This AliasName is already Used in FabricType"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    
                    if (Navigate.CheckDuplicate(ref strTable, "FabricType", txtFabTypeName.Text.Trim(), true, "FabricTypeID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This FabricType is already available"))
                    {
                        txtFabTypeName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTable, "AliasName", txtAliasName.Text.Trim(), true, "FabricTypeID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "AliasName", txtFabTypeName.Text.Trim(), true, "FabricTypeID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This FabricType is already Used in AliasName"))
                    {
                        txtFabTypeName.Focus(); 
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTable, "FabricType", txtAliasName.Text.Trim(), true, "FabricTypeID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This AliasName is already Used in FabricType"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtFabTypeName, txtAliasName, Db_Detials.tbl_FabricTypeMaster, "FabricType"))
                {
                    return true;
                }
                if (CommonCls.ValidateShortCode(this, txtFabTypeName, txtAliasName, Db_Detials.tbl_FabricTypeMaster, "FabricType"))
                {
                    return true;
                }
                return false;
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                return false;
            }
        }

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                txtFabTypeName.Focus();
                pctImage.Image = null;
                pctImage.Image = CIS_Textil.Properties.Resources.no_image;
                imagepath = "";
                ApplyActStatus();
            }

            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
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

        public void ImageStore(string FabricTypeImgID)
        {
            try
            {
                //Read Image Bytes into a byte array
                byte[] imageData = ReadFile(imagepath);

                //Initialize SQL Server Connection
                SqlConnection CN = new SqlConnection(DB.GetDBConn());

                //Set insert query
                string FabricTypeID = string.Empty;
                if (txtCode.Text != "")
                {
                    FabricTypeID = FabricTypeImgID;
                }
                else
                {
                    FabricTypeID = DB.GetSnglValue("select max(FabricTypeID) from "  + Db_Detials.tbl_FabricTypeMaster + "");
                }
                string qry = "Update"  + Db_Detials.tbl_FabricTypeMaster + " Set Image=@ImageData, Imagepath=@OriginalPath  where FabricTypeID=" + FabricTypeID + " ";

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

        public void GetImage(string FabricTypeID)
        {
            try
            {
                DataTable Dt = DB.GetDT("select Image from "  + Db_Detials.tbl_FabricTypeMaster + " where FabricTypeID=" + FabricTypeID, false);
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

        private void txtFabTypeName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtFabTypeName, txtAliasName,  Db_Detials.tbl_FabricTypeMaster , "FabricType");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtFabTypeName, txtAliasName,  Db_Detials.tbl_FabricTypeMaster , "FabricType");
        }
    }
}
