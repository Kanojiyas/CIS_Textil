using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using CIS_Bussiness;
using CIS_CLibrary;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmFabricMaster : frmMasterIface
    {
        private static string imagepath = "";
        string id = string.Empty;
        public int FabricQualityID = 0;
        public int FabricDesignID = 0;
        public int FabricShadeID = 0;

        public frmFabricMaster()
        {
            InitializeComponent();
        }

        #region Event

        private void frmFabricMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboShadeName, Combobox_Setup.ComboType.Mst_FabricShade, "");
                Combobox_Setup.FillCbo(ref cboDesignName, Combobox_Setup.ComboType.Mst_FabricDesign, "");
                Combobox_Setup.FillCbo(ref cboQualityname, Combobox_Setup.ComboType.Mst_FabricQuality, "");
               
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtsrno.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtsrno.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtsrno.Focus();
                        this.isComboAddText = true;
                        this.isSecondMessage = true;
                    }
                }

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

        #endregion

        #region Navigation

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "FabricID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtsrno, "FabricName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDesignName, "FabricDesignID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboQualityname, "FabricQualityID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboShadeName, "FabricShadeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtRate_Maharashtra, "MaharashtraRate", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtRate_Others, "OthersRate", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtImagePath, "ImagePath", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMinPcs, "MinPcs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMinMtrs, "MinMtrs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMaxPcs, "MaxPcs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMaxMtrs, "MaxMtrs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                #region Fill Multiple Company
                BindComp();
                string str = DB.GetSnglValue("select IntCompID from fn_FabricMaster_Tbl() Where FabricID=" + txtCode.Text + "");
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

                imagepath = DBValue.Return_DBValue(this, "ImagePath");
                GetImage(txtCode.Text);
                if (cboQualityname.SelectedValue != null && cboDesignName.SelectedValue != null && cboShadeName.SelectedValue != null)
                {
                    FabricQualityID = Localization.ParseNativeInt(cboQualityname.SelectedValue.ToString());
                    FabricDesignID = Localization.ParseNativeInt(cboDesignName.SelectedValue.ToString());
                    FabricShadeID = Localization.ParseNativeInt(cboShadeName.SelectedValue.ToString());
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
            ApplyActStatus();
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

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                CommonCls.IncFieldID(this, ref txtCode, "");
                CommonCls.AutoCompleteText(this.strTableName, "FabricName", ref txtsrno);
                pctImage.Image = null;
                pctImage.Image = CIS_Textil.Properties.Resources.no_image;
                imagepath = "";
                ApplyActStatus();
                BindComp();
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
                sComboAddText = txtsrno.Text;
                ArrayList pArrayData = new ArrayList
                {
                txtsrno.Text.Trim(),
                txtAliasName.Text.Trim(),
                cboDesignName.SelectedValue,
                cboQualityname.SelectedValue,
                cboShadeName.SelectedValue,
                Localization.ParseNativeDecimal(txtMinPcs.Text),
                Localization.ParseNativeDecimal(txtMinMtrs.Text),
                Localization.ParseNativeDecimal(txtMaxPcs.Text),
                Localization.ParseNativeDecimal(txtMaxMtrs.Text),
                (ChkActive.Checked == true ? 1 : 0),
                null,
                txtImagePath.Text,
                (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                (txtET1.Text.Trim()),
                (txtET2.Text.Trim()),
                (txtET3.Text.Trim())
                };

                #region Multiple Company
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

                DBSp.Master_AddEdit(pArrayData, "");

                string status = string.Empty;

                if (status == "New")
                {
                    id = DB.GetSnglValue("select max(FabricID) from " + Db_Detials.fn_Fabricmaster + "");
                    imagepath = DB.GetSnglValue("select ImagePath from" + Db_Detials.fn_Fabricmaster + "where FabricID =" + id + "");
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
                this.IsMasterAdded = false;
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        public bool ValidateForm()
        {
            //string str;
            try
            {
                if (txtsrno.Text.Trim() == "" || txtsrno.Text.Trim() == "-" || txtsrno.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Serial no");
                    txtsrno.Focus();
                    return true;
                }

                if (cboDesignName.SelectedValue == null || cboDesignName.SelectedValue.ToString() == "-" || cboDesignName.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Design");
                    cboDesignName.Focus();
                    return true;
                }

                if (cboQualityname.SelectedValue == null || cboQualityname.SelectedValue.ToString() == "-" || cboQualityname.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Quality");
                    cboQualityname.Focus();
                    return true;
                }

                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    int intDuplicateComb = Localization.ParseNativeInt(DB.GetSnglValue("select Count(0) from fn_FabricMaster() where FabricDesignID=" + cboDesignName.SelectedValue + " and FabricQualityID=" + cboQualityname.SelectedValue + " and FabricShadeID=" + cboShadeName.SelectedValue + ""));
                    if (intDuplicateComb > 0)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "The Combination Of Design, Quality, Shade is Already Available");
                        return true;
                    }
                }
                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    int intDuplicateComb = Localization.ParseNativeInt(DB.GetSnglValue("select Count(0) from fn_FabricMaster() where FabricDesignID=" + cboDesignName.SelectedValue + " and FabricQualityID=" + cboQualityname.SelectedValue + " and FabricShadeID=" + cboShadeName.SelectedValue + "and FabricID!= " + txtCode.Text + ""));
                    if (intDuplicateComb > 0)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "The Combination Of Design, Quality, Shade is Already Available");
                        return true;
                    }
                }

                string str2 = string.Empty;
                if (DBSp.rtnAction())
                {
                    str2 = "tbl_FabricMaster";
                    if (Navigate.CheckDuplicate(ref str2, "FabricName", this.txtsrno.Text.Trim(), false, "", 0, "", "This FabricName is already available"))
                    {
                        this.txtsrno.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str2, "Aliasname", this.txtAliasName.Text.Trim(), false, "", 0, "", "This Aliasname is already available"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str2, "Aliasname", this.txtsrno.Text.Trim(), false, "", 0, "", "This FabricName is already Used in AliasName"))
                    {
                        this.txtsrno.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str2, "FabricName", this.txtAliasName.Text.Trim(), false, "", 0, "", "This Aliasname is already Used in FabricName"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    str2 = "tbl_FabricMaster";
                    if (Navigate.CheckDuplicate(ref str2, "FabricName", this.txtsrno.Text.Trim(), true, "FabricID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This FabricName is already available"))
                    {
                        txtsrno.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str2, "Aliasname", this.txtAliasName.Text.Trim(), true, "FabricID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str2, "Aliasname", this.txtsrno.Text.Trim(), true, "FabricID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This FabricName is already Used in AliasName"))
                    {
                        txtsrno.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str2, "FabricName", this.txtAliasName.Text.Trim(), true, "FabricID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This Aliasname is already Used in FabricName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }

                if (CommonCls.ValidateMaster(this, txtsrno, txtAliasName, "tbl_FabricMaster", "FabricName"))
                {
                    return true;
                }
                if (CommonCls.ValidateShortCode(this, txtsrno, txtAliasName, "tbl_FabricMaster", "FabricName"))
                {
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

        #endregion

        private void txtFabricName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtsrno, txtAliasName, "tbl_FabricMaster", "FabricName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtsrno, txtAliasName, "tbl_FabricMaster", "FabricName");
        }

        private void cboDesignName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if ((cboDesignName.SelectedValue != null) && cboDesignName.SelectedValue.ToString() != "System.Data.DataRowView" && (Localization.ParseNativeDouble(cboDesignName.SelectedValue.ToString()) > 0.0))
                {
                    Combobox_Setup.Fill_Combo(cboQualityname, "Select FabricQualityID, FabricQualityName From fn_FabricDesignMaster_Tbl() Where FabricDesignID=" + cboDesignName.SelectedValue.ToString() + "", "FabricQualityName", "FabricQualityID");
                }
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
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
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

        public void ImageStore(string SerialImgID)
        {
            try
            {
                //Read Image Bytes into a byte array
                byte[] imageData = ReadFile(imagepath);

                //Initialize SQL Server Connection
                SqlConnection CN = new SqlConnection(DB.GetDBConn());

                //Set insert query
                string FabricID = string.Empty;
                if (txtCode.Text != "")
                {
                    FabricID = SerialImgID;
                }
                else
                {
                    FabricID = DB.GetSnglValue("select max(FabricID) from" + Db_Detials.fn_Fabricmaster + "");
                }
                string qry = "Update tbl_FabricMaster Set Image=@ImageData, Imagepath=@OriginalPath  where FabricID=" + FabricID + " ";

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

        public void GetImage(string FabricID)
        {
            try
            {
                DataTable Dt = DB.GetDT("select Image from tbl_FabricMaster where FabricID=" + FabricID, false);
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
