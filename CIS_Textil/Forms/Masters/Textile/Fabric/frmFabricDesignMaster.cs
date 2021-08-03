using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using PopupControl;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_CLibrary;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmFabricDesignMaster : frmMasterIface
    {
        private static string imagepath = "";
        string id = string.Empty;
        public DataGridViewEx fgDtls;
        public DataGridViewEx fgDtls_footer;

        public frmFabricDesignMaster()
        {
            InitializeComponent();
            fgDtls = GrdMain.fgDtls;
            fgDtls_footer = GrdMain.fgDtls_f;
        }

        private void frmFabricDesignMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboChartNo, Combobox_Setup.ComboType.Mst_DesignNo, "");
                Combobox_Setup.FillCbo(ref cboQuality, Combobox_Setup.ComboType.Mst_FabricQuality, "");
                Combobox_Setup.FillCbo(ref cboShade, Combobox_Setup.ComboType.Mst_FabricShade, "");
                Combobox_Setup.FillCbo(ref CboUnits, Combobox_Setup.ComboType.Mst_UnitsofMesuremet, "");
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                ChkActive.LostFocus += new EventHandler(EventHandles.OnSave_KeyEnter);

                BindComp();

                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtDesignName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtDesignName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtDesignName.Focus();
                        this.isComboAddText = true;
                        this.isSecondMessage = true;
                    }
                }

                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    Combobox_Setup.FillCbo(ref cboChartNo, Combobox_Setup.ComboType.Mst_DesignNo, "");
                    FillControls();
                }
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
                DBValue.Return_DBValue(this, txtCode, "FabricDesignID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDesignName, "FabricDesignName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDesignNo, "FabricDesignNo", Enum_Define.ValidationType.Text);
                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    DBValue.Return_DBValue(this, cboChartNo, "DesignNoID", Enum_Define.ValidationType.Text);
                }
                DBValue.Return_DBValue(this, cboQuality, "FabricQualityID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, CboUnits, "UnitID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboShade, "FabricShadeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtExpectedCuts, "Cuts", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMtrs, "TapLen", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtImagePath, "ImagePath", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMinPcs, "MinPcs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMinMtrs, "MinMtrs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMaxMtrs, "MaxMtrs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtWeightPerMtrs, "WtPerMtr", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMaxPcs, "MaxPcs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                #region Fill Multiple Company
                BindComp();
                string str = DB.GetSnglValue("select IntCompID from fn_FabricDesignMaster_Tbl() Where FabricDesignID=" + txtCode.Text + "");
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
                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "FabricDesignID", txtCode.Text, base.dt_HasDtls_Grd, this.iIDentity);
                GetImage(txtCode.Text);
                
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
            ApplyActStatus();
        }

        public void SaveRecord()
        {
            try
            {
                sComboAddText = txtDesignName.Text;
                ArrayList pArrayData = new ArrayList
                {
                   txtDesignName.Text.Trim(),
                   txtAliasName.Text.Trim(),
                   txtDesignNo.Text.Trim(),
                   null,
                   cboQuality.SelectedValue.ToString(),
                   cboShade.SelectedValue.ToString(),
                   CboUnits.SelectedValue.ToString(),
                   null,
                   null,
                   null,
                   null,
                   txtExpectedCuts.Text.Trim(),
                   txtMtrs.Text.Trim(),
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   cboQuality.SelectedValue.ToString(),
                   null,
                   null,
                   null,
                   null,
                   (ChkActive.Checked == true ? 1 : 0),
                   null,
                   txtImagePath.Text,
                   Localization.ParseNativeDecimal(txtMinPcs.Text),
                   Localization.ParseNativeDecimal(txtMinMtrs.Text),
                   Localization.ParseNativeDecimal(txtMaxPcs.Text),
                   Localization.ParseNativeDecimal(txtMaxMtrs.Text),
                   Localization.ParseNativeDecimal(txtWeightPerMtrs.Text),
                   (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                   (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                   (txtET1.Text.Trim()),
                   (txtET2.Text.Trim()),
                   (txtET3.Text.Trim())
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

                if (fgDtls.Rows.Count > 1)
                {
                    DBSp.Transcation_AddEdit(pArrayData, fgDtls, true);
                }
                else
                {
                    DBSp.Master_AddEdit(pArrayData);
                }

                if (status == "New")
                {
                    id = DB.GetSnglValue("select max(FabricDesignID) from " + Db_Detials.fn_FabricDesignMaster + "");
                    imagepath = DB.GetSnglValue("select ImagePath from" + Db_Detials.fn_FabricDesignMaster + "where FabricDesignID=" + id + "");
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
                this.IsMasterAdded = false;
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                string str = Db_Detials.fn_FabricDesignMaster;
                //if (!EventHandles.IsValidGridReq(this.fgDtls, base.dt_AryIsRequired))
                //{
                //    return true;
                //}
                //if (!EventHandles.IsRequiredInGrid(fgDtls, this.dt_AryIsRequired, false))
                //{
                //    return true;
                //}
                if (txtDesignName.Text.Trim() == "" || txtDesignName.Text.Trim() == "-" || txtDesignName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Design Name");
                    txtDesignName.Focus();
                    return true;
                }
                if (cboQuality.SelectedValue == null || cboQuality.SelectedValue.ToString() == "-" || cboQuality.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Quality");
                    this.cboQuality.Focus();
                    return true;
                }
                if (CboUnits.SelectedValue == null || CboUnits.SelectedValue.ToString() == "-" || CboUnits.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Units");
                    this.CboUnits.Focus();
                    return true;
                }
                if (DBSp.rtnAction())
                {
                    if (Navigate.CheckDuplicate(ref str, "FabricDesignName", txtDesignName.Text.Trim(), false, "", 0L, "", "This DesignName is already available"))
                    {
                        txtDesignName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtDesignName.Text.Trim(), false, "", 0L, "", "This DesignName is already Used in AliasName"))
                    {
                        txtDesignName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "FabricDesignName", txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in DesignName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {

                    if (Navigate.CheckDuplicate(ref str, "FabricDesignName", txtDesignName.Text.Trim(), true, "FabricDesignID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This DesignName is already available"))
                    {
                        this.txtDesignName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), true, "FabricDesignID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtDesignName.Text.Trim(), true, "FabricDesignID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This DesignName is already Used in AliasName"))
                    {
                        this.txtDesignName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "FabricDesignName", txtAliasName.Text.Trim(), true, "FabricDesignID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This AliasName is already Used in DesignName"))
                    {
                        this.txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtDesignName, txtAliasName, Db_Detials.fn_FabricDesignMaster, "FabricDesignName"))
                {
                    return true;
                }
                if (CommonCls.ValidateShortCode(this, txtDesignName, txtAliasName, Db_Detials.fn_FabricDesignMaster, "FabricDesignName"))
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

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                txtDesignName.Focus();
                pctImage.Image = null;
                pctImage.Image = CIS_Textil.Properties.Resources.no_image;
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CalculateFooter_Rows(fgDtls, fgDtls_footer, fgDtls.Grid_ID.ToString(), fgDtls.Grid_UID);
                imagepath = "";
                ApplyActStatus();
                BindComp();
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

        public void ImageStore(string FabricDesignImgID)
        {
            try
            {
                //Read Image Bytes into a byte array
                byte[] imageData = ReadFile(imagepath);

                //Initialize SQL Server Connection
                SqlConnection CN = new SqlConnection(DB.GetDBConn());

                //Set insert query
                string FabricDesignID = string.Empty;
                if (txtCode.Text != "")
                {
                    FabricDesignID = FabricDesignImgID;
                }
                else
                {
                    FabricDesignID = DB.GetSnglValue("select max(FabricDesignID)" + Db_Detials.fn_FabricDesignMaster + "");
                }
                string qry = "Update tbl_FabricDesignMaster Set Image=@ImageData, Imagepath=@OriginalPath  where FabricDesignID=" + FabricDesignID + " ";

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

        public void GetImage(string FabricDesignID)
        {
            try
            {
                DataTable Dt = DB.GetDT("select Image from tbl_FabricDesignMaster where FabricDesignID=" + FabricDesignID, false);
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

        private void txtDesignName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtDesignName, txtAliasName, Db_Detials.fn_FabricDesignMaster, "FabricDesignName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtDesignName, txtAliasName, Db_Detials.fn_FabricDesignMaster, "FabricDesignName");
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
