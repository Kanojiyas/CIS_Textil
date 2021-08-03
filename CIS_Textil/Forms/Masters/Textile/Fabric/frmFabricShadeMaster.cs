using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CIS_Bussiness;
using Microsoft.VisualBasic;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmFabricShadeMaster : frmMasterIface
    {
        private static string imagepath = "";
        string id = string.Empty;

        public frmFabricShadeMaster()
        {
            InitializeComponent();
        }

        private void frmFabricShadeMaster_Load(object sender, EventArgs e)
        {
            try
            {
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtFabricShadeName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtFabricShadeName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtFabricShadeName.Focus();
                        this.isComboAddText = true;
                        this.isSecondMessage = true;
                    }
                }
                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    FillControls();
                }
                BindComp();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

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
                DBValue.Return_DBValue(this, txtCode, "FabricShadeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtFabricShadeName, "FabricShadeName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
                imagepath = DBValue.Return_DBValue(this, "ImagePath");
                GetImage(txtCode.Text);
                #region Fill Multiple Company
                BindComp();
                string str = DB.GetSnglValue("select IntCompID from fn_FabricShadeMaster_Tbl() Where FabricShadeID=" + txtCode.Text + "");
                string[] strMember = str.Split(',');
                if (str != "")
                {
                    for (int i = 0; i <= chkCompany.Items.Count - 1; i++)
                    {
                        DataRowView dr = (DataRowView)chkCompany.Items[i];
                        for (int j = 0; j <= strMember.Length - 1; j++)
                        {
                            if (dr[0].ToString() == strMember[j].ToString())
                            {
                                chkCompany.SetItemChecked(i, true);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < chkCompany.Items.Count; i++)
                    {
                        chkCompany.SetItemChecked(i, false);
                    }
                }
                #endregion

                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);
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
                sComboAddText = txtFabricShadeName.Text;

                ArrayList pArrayData = new ArrayList
                {
                txtFabricShadeName.Text.Trim(),
                txtAliasName.Text==""?null:txtAliasName.Text,
                (ChkActive.Checked? 1: 0),
                null,
                txtImagePath.Text,
                (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                (txtET1.Text.Trim()),
                (txtET2.Text.Trim()),
                (txtET3.Text.Trim()),
                };

                #region Multiple Company Saving
                string strCheckedValue = string.Empty;
                for (int i = 0; i <= chkCompany.Items.Count - 1; i++)
                {
                    if (chkCompany.GetItemChecked(i) == true)
                    {
                        DataRowView dr = (DataRowView)chkCompany.Items[i];
                        strCheckedValue = strCheckedValue + dr[0].ToString() + ",";
                    }
                }

                if (strCheckedValue.Length > 0)
                {
                    strCheckedValue = strCheckedValue.Substring(0, strCheckedValue.Length - 1);
                }
                Db_Detials.IntCompID = strCheckedValue == null ? (Db_Detials.CompID).ToString() : (strCheckedValue == "" ? (Db_Detials.CompID).ToString() : strCheckedValue);
                #endregion

                string status = string.Empty;
                if (blnFormAction == Enum_Define.ActionType.New_Record)
                    status = "New";
                else if (blnFormAction == Enum_Define.ActionType.Edit_Record)
                    status = "Edit";
                DBSp.Master_AddEdit(pArrayData, "");
                if (status == "New")
                {
                    id = DB.GetSnglValue("select max(FabricShadeID) from" + Db_Detials.fn_FabricShadeMaster + "");
                    imagepath = DB.GetSnglValue("select ImagePath from" + Db_Detials.fn_FabricShadeMaster + "where FabricShadeID=" + id + "");
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
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                string strTable = Db_Detials.fn_FabricShadeMaster ;
                if (txtFabricShadeName.Text.Trim() == "" || txtFabricShadeName.Text.Trim() == "-" || txtFabricShadeName.Text.Trim() == "--" || txtFabricShadeName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Fabric Shade");
                    txtFabricShadeName.Focus();
                    return true;
                }
                if (DBSp.rtnAction())
                {
                    
                    if (Navigate.CheckDuplicate(ref strTable, "FabricShadeName", txtFabricShadeName.Text.Trim(), false, "", 0L, "", "This ShadeName is already available"))
                    {
                        txtFabricShadeName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "AliasName", txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "AliasName", txtFabricShadeName.Text.Trim(), false, "", 0L, "", "This ShadeName is already Used in AliasName"))
                    {
                        txtFabricShadeName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "FabricShadeName", txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in ShadeName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    
                    if (Navigate.CheckDuplicate(ref strTable, "FabricShadeName", txtFabricShadeName.Text.Trim(), true, "FabricShadeId", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This ShadeName is already available"))
                    {
                        txtFabricShadeName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "AliasName", txtAliasName.Text.Trim(), true, "FabricShadeId", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "AliasName", txtFabricShadeName.Text.Trim(), true, "FabricShadeId", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This ShadeName is already Used in AliasName"))
                    {
                        txtFabricShadeName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "FabricShadeName", txtAliasName.Text.Trim(), true, "FabricShadeId", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This AliasName is already Used in ShadeName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtFabricShadeName, txtAliasName, Db_Detials.fn_FabricShadeMaster, "FabricShadeName"))
                {
                    return true;
                }
                if (CommonCls.ValidateMaster(this, txtFabricShadeName, txtAliasName, Db_Detials.fn_FabricShadeMaster, "FabricShadeName"))
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
                txtFabricShadeName.Focus();
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

        #endregion

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

        public void ImageStore(string FabricShadeImgID)
        {
            try
            {
                //Read Image Bytes into a byte array
                byte[] imageData = ReadFile(imagepath);

                //Initialize SQL Server Connection
                SqlConnection CN = new SqlConnection(DB.GetDBConn());

                //Set insert query
                string FabricQualityID = string.Empty;
                if (txtCode.Text != "")
                {
                    FabricQualityID = FabricShadeImgID;
                }
                else
                {
                    FabricQualityID = DB.GetSnglValue("select max(FabricShadeID) from" + Db_Detials.fn_FabricShadeMaster + "");
                }
                string qry = "Update" + Db_Detials.fn_FabricShadeMaster + "Set Image=@ImageData, Imagepath=@OriginalPath  where FabricShadeID=" + FabricQualityID + " ";

                //Initialize SqlCommand object for insert.
                SqlCommand SqlCom = new SqlCommand(qry, CN);

                //We are passing Original Image Path and Image byte data as sql parameters.
                SqlCom.Parameters.Add(new SqlParameter("@OriginalPath", (object)imagepath));
                SqlCom.Parameters.Add(new SqlParameter("@ImageData", (object)imageData));

                CN.Open();
                SqlCom.ExecuteNonQuery();
                CN.Close();
            }
            catch { }
        }

        public void GetImage(string FabricShadeID)
        {
            try
            {
                DataTable Dt = DB.GetDT("select Image from" + Db_Detials.fn_FabricShadeMaster + "where FabricShadeID=" + FabricShadeID, false);
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

        private void txtFabricShadeName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtFabricShadeName, txtAliasName, Db_Detials.fn_FabricShadeMaster , "FabricShadeName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtFabricShadeName, txtAliasName, Db_Detials.fn_FabricShadeMaster , "FabricShadeName");
        }

        private void BindComp()
        {
            try
            {
                DataTable dt = DB.GetDT(string.Format("Select CompanyID,CompanyName From fn_CompanyMaster_Tbl() order by CompanyName asc"), false);
                ((ListBox)chkCompany).DataSource = null;((ListBox)chkCompany).DataSource = dt;
                ((ListBox)chkCompany).DisplayMember = "CompanyName";
                ((ListBox)chkCompany).ValueMember = "CompanyID";
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
    }
}
