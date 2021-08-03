using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmEmbSampleDesignCard : frmTrnsIface
    {
        ArrayList OrgInGridArray = new ArrayList();

        public DataGridViewEx fgDtls_Yarn;
        public DataGridViewEx fgDtls_Yarn_footer;

        public DataGridViewEx fgDtls_Sequence;
        public DataGridViewEx fgDtls_Sequence_footer;

        private static string imagepath = "";
        string id = string.Empty;

        public frmEmbSampleDesignCard()
        {
            InitializeComponent();

            fgDtls_Yarn = GrdMain.fgDtls;
            fgDtls_Yarn_footer = GrdMain.fgDtls_f;
            fgDtls_Sequence = GrdFooter.fgDtls;
            fgDtls_Sequence_footer = GrdFooter.fgDtls_f;
        }

        #region Form Event
        private void frmSampleDesignCard_Load(object sender, EventArgs e)
        {
            try
            {
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls_Sequence, fgDtls_Sequence_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 1, true);
                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls_Yarn, fgDtls_Yarn_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);
                txtCord.Visible = false;
                lblColCord.Visible = false;
                lblCord.Visible = false;
                rdoMulti.Checked = true;
                tabDesignGrid.TabPages.Remove(tbSequence);
                tabDesignGrid.Refresh();
                tbSequence.Show();
                tbYarn.Show();
                this.fgDtls_Sequence.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_Sequence_CellValueChanged);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region Form Navigation

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "EmbDesignCardID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEmbDesignCardNo, "DesignCardNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEmbDesignname, "EmbDesignName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtTotalstiches, "TotStitches", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtRate, "Rate", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCord, "Cord", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtArea, "Area", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDescription, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEd1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);
                imagepath = DBValue.Return_DBValue(this, "ImagePath");
                GetImage(txtCode.Text);

                string strRdo = string.Empty;
                strRdo = DB.GetSnglValue("Select RdoGroupType From tbl_FabricRateListMain Where IsDeleted=0 and FabricRateListID=" + txtCode.Text + "");
                if (strRdo == "0")
                {
                    rdoMulti.Checked = true;
                }
                else if (strRdo == "1")
                {
                    rdoSequence.Checked = true;
                }
                else if (strRdo == "2")
                {
                    rdoChain.Checked = true;
                }
                else if (strRdo == "3")
                {
                    rdoCord.Checked = true;
                    txtCord.Visible = true;
                    lblColCord.Visible = true;
                    lblCord.Visible = true;
                }

                DetailGrid_Setup.FillGrid(fgDtls_Yarn, this.fgDtls_Yarn.Grid_UID, this.fgDtls_Yarn.Grid_Tbl, "EmbDesignCardID", txtCode.Text, base.dt_HasDtls_Grd);
                DetailGrid_Setup.FillGrid(fgDtls_Sequence, this.fgDtls_Sequence.Grid_UID, this.fgDtls_Sequence.Grid_Tbl, "EmbDesignCardID", txtCode.Text, base.dt_HasDtls_Grd);
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
                sComboAddText = txtEmbDesignCardNo.Text;
                string Rdbtext = string.Empty;
                if (rdoMulti.Checked == true)
                { Rdbtext = "0"; }
                else if (rdoSequence.Checked == true)
                { Rdbtext = "1"; }
                else if (rdoChain.Checked == true)
                { Rdbtext = "2"; }
                else if (rdoCord.Checked == true)
                { Rdbtext = "3"; }

                if (rdoChain.Checked == true || rdoMulti.Checked == true || rdoSequence.Checked == true)
                {
                    txtCord.Text = "";
                }

                ArrayList pArrayData = new ArrayList
                {
                    txtEmbDesignCardNo.Text.Trim().ToString(),
                    txtEmbDesignname.Text.Trim().ToString(),
                    txtArea.Text,
                    txtTotalstiches.Text.ToString(),
                    txtRate.Text.ToString(),
                    Rdbtext,
                    txtCord.Text.Trim(),
                    null,
                    txtImagePath.Text,
                    txtDescription.Text,
                    cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue,
                    cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue,
                    dtEd1.TextFormat(false,true), 
                    txtET1.Text,
                    txtET2.Text,
                    txtET3.Text
                };
                string status = string.Empty;
                if (blnFormAction == Enum_Define.ActionType.New_Record)
                    status = "New";
                else if (blnFormAction == Enum_Define.ActionType.Edit_Record)
                    status = "Edit";
                string strfabType = DB.GetSnglValue("Select FabricQualityID From tbl_FabricQualityMaster Where IsDeleted=0 and FabricQualityName='Our Quality'");
                string strFab = DB.GetSnglValue("Select FabricDesignName from tbl_FabricDesignMaster Where FabricDesignName=" + txtEmbDesignname.Text + " and IsDeleted=0");
                string strQry = string.Empty;
                if (strFab == "") //"Delete From tbl_FabricDesignMaster Where FabricDesignID = (#CodeID#);" + Environment.NewLine;
                {
                    strQry = string.Format("Insert Into tbl_FabricDesignMaster (FabricDesignName,AliasName,FabricDesignNo,ChartNo,FabricQualityID,FabricShadeID,UnitID,Reed,Pick,TotalPicks,Width,Cuts,TapLen,MtrsTaka,WtCuts,WftCal,PayType,Shortage,ProdRate,SalesRate,DesignNoID,LoomTypeID,Wastage,Shrinkage,DyeingTypeID,IsActive,Image,ImagePath,MinPcs,MinMtrs,MaxPcs,MaxMtrs,WtPerMtr,EI1,EI2,ET1,ET2,ET3,IntCompID,StoreID,CompID,YearID,AddedOn,AddedBy,IsModified,ModifiedOn,ModifiedBy,IsDeleted,DeletedOn,DeletedBy,IsCanclled,CancelledOn,CancelledBy,IsApproved,ApprovedOn,ApprovedBy,IsAudited,AuditedOn,AuditedBy) Values ({0},null,{1},null,{2},{3},null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,1,null,null,null,null,null,null,null,0,0,null,null,null,{4},{5},{6},{7},getdate(),{8},0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);", CommonLogic.SQuote(txtEmbDesignname.Text), CommonLogic.SQuote(txtEmbDesignCardNo.Text), strfabType, 1, Db_Detials.CompID, Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
                }
                DBSp.Transcation_AddEdit(pArrayData, this.fgDtls_Yarn, true, strQry, txtEmbDesignCardNo.Text, fgDtls_Sequence);
                if (status == "New")
                {
                    id = DB.GetSnglValue("select max(EmbDesignCardID) from tbl_EmbDesignCardMain");
                    imagepath = DB.GetSnglValue("select ImagePath from tbl_EmbDesignCardMain where EmbDesignCardID=" + id + "");
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
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                string strTable;
                if (txtEmbDesignCardNo.Text.Trim() == "" || txtEmbDesignCardNo.Text.Trim() == "-" || txtEmbDesignCardNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Design Card No");
                    txtEmbDesignCardNo.Focus();
                    return true;
                }
                if (txtEmbDesignname.Text.Trim() == "" || txtEmbDesignname.Text.Trim() == "-" || txtEmbDesignname.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Design Name");
                    txtEmbDesignname.Focus();
                    return true;
                }
                if (txtTotalstiches.Text.Trim() == "" || txtTotalstiches.Text.Trim() == "-" || txtTotalstiches.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Total Stiches");
                    txtTotalstiches.Focus();
                    return true;
                }

                if (txtRate.Text.Trim() == "" || txtRate.Text.Trim() == "-" || txtRate.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Rate");
                    txtRate.Focus();
                    return true;
                }

                if (rdoMulti.Checked == true)
                {
                    if (!EventHandles.IsValidGridReq(this.fgDtls_Yarn, base.dt_AryIsRequired))
                    {
                        return true;
                    }

                    if (!EventHandles.IsRequiredInGrid(fgDtls_Yarn, this.dt_AryIsRequired, false))
                    {
                        return true;
                    }
                }
                if (rdoCord.Checked == true)
                {
                    if (!EventHandles.IsValidGridReq(this.fgDtls_Yarn, base.dt_AryIsRequired))
                    {
                        return true;
                    }

                    if (!EventHandles.IsRequiredInGrid(fgDtls_Yarn, this.dt_AryIsRequired, false))
                    {
                        return true;
                    }

                    if (!EventHandles.IsValidGridReq(this.fgDtls_Sequence, base.dt_AryIsRequired))
                    {
                        return true;
                    }

                    if (!EventHandles.IsRequiredInGrid(fgDtls_Sequence, this.dt_AryIsRequired, false))
                    {
                        return true;
                    }

                    if (txtCord.Text.Trim().Length <= 0)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Cord");
                        txtCord.Focus();
                        return true;
                    }
                }

                if (rdoSequence.Checked == true || rdoChain.Checked == true)
                {
                    if (!EventHandles.IsValidGridReq(this.fgDtls_Yarn, base.dt_AryIsRequired))
                    {
                        return true;
                    }

                    if (!EventHandles.IsRequiredInGrid(fgDtls_Yarn, this.dt_AryIsRequired, false))
                    {
                        return true;
                    }

                    if (!EventHandles.IsValidGridReq(this.fgDtls_Sequence, base.dt_AryIsRequired))
                    {
                        return true;
                    }
                    if (!EventHandles.IsRequiredInGrid(fgDtls_Sequence, this.dt_AryIsRequired, false))
                    {
                        return true;
                    }
                }
                if (DBSp.rtnAction())
                {
                    strTable = "tbl_EmbDesignCardMain";
                    if (Navigate.CheckDuplicate(ref strTable, "DesignCardNo", txtEmbDesignCardNo.Text.Trim(), false, "", 0L, "StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                    {
                        txtEmbDesignCardNo.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "EmbDesignName", txtEmbDesignname.Text.Trim(), false, "", 0L, "StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                    {
                        txtEmbDesignname.Focus();
                        return true;
                    }
                }
                else
                {
                    strTable = "tbl_EmbDesignCardMain";
                    if (Navigate.CheckDuplicate(ref strTable, "DesignCardNo", txtEmbDesignCardNo.Text.Trim(), true, "EmbDesignCardID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                    {
                        txtEmbDesignCardNo.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTable, "EmbDesignName", txtEmbDesignname.Text.Trim(), true, "", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and YearID=" + Db_Detials.YearID + "", ""))
                    {
                        txtEmbDesignname.Focus();
                        return true;
                    }
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
                txtEmbDesignCardNo.Focus();
                EventHandles.CreateDefault_Rows(fgDtls_Yarn, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                EventHandles.CreateDefault_Rows(fgDtls_Sequence, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);

                EventHandles.CalculateFooter_Rows(fgDtls_Yarn, fgDtls_Yarn_footer, fgDtls_Yarn.Grid_ID.ToString(), fgDtls_Yarn.Grid_UID);
                EventHandles.CalculateFooter_Rows(fgDtls_Sequence, fgDtls_Sequence_footer, fgDtls_Sequence.Grid_ID.ToString(), fgDtls_Sequence.Grid_UID);
                pctImage.Image = null;
                pctImage.Image = CIS_Textil.Properties.Resources.no_image;
                imagepath = "";
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

        public void ImageStore(string EmbDesignCardImgID)
        {
            try
            {
                //Read Image Bytes into a byte array
                byte[] imageData = ReadFile(imagepath);

                //Initialize SQL Server Connection
                SqlConnection CN = new SqlConnection(DB.GetDBConn());

                //Set insert query
                string EmbDesignCardID = string.Empty;
                if (txtCode.Text != "")
                {
                    EmbDesignCardID = EmbDesignCardImgID;
                }
                else
                {
                    EmbDesignCardID = DB.GetSnglValue("select max(EmbDesignCardID) from tbl_EmbDesignCardMain");
                }
                string qry = "Update tbl_EmbDesignCardMain Set Image=@ImageData, Imagepath=@OriginalPath  where EmbDesignCardID=" + EmbDesignCardID + " ";

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

        public void GetImage(string EmbDesignCardID)
        {
            try
            {
                DataTable Dt = DB.GetDT("select Image from tbl_EmbDesignCardMain where EmbDesignCardID=" + EmbDesignCardID, false);
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

        #region Grid

        #region Grid Multi
        private void fgDtls_Multi_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

            }
            catch { }
        }

        private void fgDtls_Multi_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
            }
            catch { }
        }
        #endregion

        #region Grid Sequence
        private void fgDtls_Sequence_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

            }
            catch { }
        }
        #endregion

        #region Grid Chain
        private void fgDtls_Chain_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

            }
            catch { }
        }
        #endregion

        #region Grid Cord
        private void fgDtls_Cord_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

            }
            catch { }
        }
        #endregion

        #region CalVal
        private void CalcVal()
        {
            //TxtTotalCuts.Text = string.Format("{0:N0}", CommonCls.GetColSum(this.fgDtls, 12, -1, -1));
            //TxtTotMtrs.Text = string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 13, -1, -1));
            //TxtGrossAmount.Text = string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 16, -1, -1));
            //double dblDedAmt = 0.0;
            //DataGridViewEx ex = this.fgDtls_f;
            //for (int i = 0; i <= (ex.RowCount - 1); i++)
            //{
            //    if (ex.Rows[i].Cells[3].Value != null)
            //    {
            //        if (Operators.ConditionalCompareObjectEqual(ex.Rows[i].Cells[3].FormattedValue, "-", false))
            //        {
            //            dblDedAmt -= Localization.ParseNativeDouble(ex.Rows[i].Cells[5].Value.ToString());
            //        }
            //        else if (Operators.ConditionalCompareObjectEqual(ex.Rows[i].Cells[3].FormattedValue, "+", false))
            //        {
            //            dblDedAmt += Localization.ParseNativeDouble(ex.Rows[i].Cells[5].Value.ToString());
            //        }
            //    }
            //}
            //txtAddLessAmt.Text = string.Format("{0:N2}", Math.Round(Math.Abs(dblDedAmt)));
            //txtNetAmt.Text = string.Format("{0:N2}", Math.Round(Localization.ParseNativeDouble(TxtGrossAmount.Text) + dblDedAmt));
        }
        #endregion

        #endregion

        private void RdoYarn_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoMulti.Checked == true)
            {
                tbYarn.Show();
                tbSequence.Hide();
                txtCord.Visible = false;
                lblColCord.Visible = false;
                lblCord.Visible = false;
                tabDesignGrid.Refresh();
                tabDesignGrid.TabPages.Remove(tbYarn);
                tabDesignGrid.TabPages.Remove(tbSequence);
                tabDesignGrid.TabPages.Add(tbYarn);
            }
        }

        private void rdoSequence_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoSequence.Checked == true)
            {
                tabDesignGrid.Refresh();
                tabDesignGrid.TabPages.Remove(tbYarn);
                tabDesignGrid.TabPages.Remove(tbSequence);

                tabDesignGrid.TabPages.Add(tbYarn);
                tabDesignGrid.TabPages.Add(tbSequence);

                tbYarn.Show();
                tbSequence.Show();

                txtCord.Visible = false;
                lblColCord.Visible = false;
                lblCord.Visible = false;
            }
        }

        private void rdoCord_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCord.Checked == true)
            {
                tabDesignGrid.Refresh();
                tabDesignGrid.TabPages.Remove(tbYarn);
                tabDesignGrid.TabPages.Remove(tbSequence);

                tabDesignGrid.TabPages.Add(tbYarn);
                tabDesignGrid.TabPages.Add(tbSequence);

                tbYarn.Show();
                tbSequence.Show();
                txtCord.Visible = true;
                lblColCord.Visible = true;
                lblCord.Visible = true;
            }
        }

        private void rdoChain_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoChain.Checked == true)
            {
                tabDesignGrid.Refresh();
                tabDesignGrid.TabPages.Remove(tbYarn);
                tabDesignGrid.TabPages.Remove(tbSequence);

                tabDesignGrid.TabPages.Add(tbYarn);
                tabDesignGrid.TabPages.Add(tbSequence);
                tbYarn.Show();
                tbSequence.Show();
                txtCord.Visible = false;
                lblColCord.Visible = false;
                lblCord.Visible = false;
            }
        }

        public void PrintRecord()
        {
            CIS_ReportTool.frmMultiPrint frmMultiPrint = new CIS_ReportTool.frmMultiPrint();
            CIS_ReportTool.frmMultiPrint.MenuID = base.iIDentity;
            CIS_ReportTool.frmMultiPrint.Id = Localization.ParseNativeInt(this.txtCode.Text);
            CIS_ReportTool.frmMultiPrint.TblNm = "tbl_EmbDesignCardMain";
            CIS_ReportTool.frmMultiPrint.IdStr = "EmbDesignCardID";
            CIS_ReportTool.frmMultiPrint.iStoreID = Db_Detials.StoreID;
            CIS_ReportTool.frmMultiPrint.iCompID = Db_Detials.CompID;
            CIS_ReportTool.frmMultiPrint.iBranchID = Db_Detials.BranchID;
            CIS_ReportTool.frmMultiPrint.iYearID = Db_Detials.YearID;
            CIS_ReportTool.frmMultiPrint.iUserID = Db_Detials.UserID;
            CIS_ReportTool.frmMultiPrint.objReport = Db_Detials.objReport;
            CIS_ReportTool.frmMultiPrint.sApplicationName = GetAssemblyInfo.ProductName;

            if (frmMultiPrint.ShowDialog() == DialogResult.Cancel)
            {
                frmMultiPrint.Dispose();
            }
            else
            {
                frmMultiPrint = null;
            }
        }
    }
}
