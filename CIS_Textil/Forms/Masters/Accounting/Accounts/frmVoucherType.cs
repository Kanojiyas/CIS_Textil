using System;
using System.Windows.Forms;
using System.Collections;
using CIS_Bussiness;
using CIS_DBLayer;
using CIS_CLibrary;
using System.Data;

namespace CIS_Textil
{
    public partial class frmVoucherType : frmMasterIface
    {
        CIS_Textbox txtMenu;
        public frmVoucherType()
        {
            InitializeComponent();
            txtMenu = new CIS_CLibrary.CIS_Textbox();
        }

        #region FormEvent

        private void frm_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref CboVoucherType, Combobox_Setup.ComboType.Mst_FormName_Voucher, "", "");
                Combobox_Setup.FillCbo(ref cboVoucherNumber, Combobox_Setup.ComboType.Mst_VoucherNumbering, "", "");
                Combobox_Setup.FillCbo(ref cboPrefixParticular, Combobox_Setup.ComboType.Mst_FillterType, "", "");
                Combobox_Setup.FillCbo(ref cboSuffixParticular, Combobox_Setup.ComboType.Mst_FillterType, "", "");
                Combobox_Setup.FillCbo(ref cboRestartParticular, Combobox_Setup.ComboType.Mst_FillterType, "", "");

                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtVoucherName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtVoucherName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtVoucherName.Focus();
                        this.isComboAddText = true;
                        this.isSecondMessage = true;
                    }
                }

                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    FillControls();
                }
                this.chkAdvanceConfig.CheckedChanged += new System.EventHandler(this.chkAdvanceConfig_CheckedChanged);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
        #endregion FormEvent

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

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                txtVoucherName.Focus();
                ApplyActStatus();
                BindDtls();
                pnlDetails.Visible = false;
                chkAdvanceConfig.Checked = false;

                for (int i = 0; i < chkLstMapping.Items.Count; i++)
                {
                    chkLstMapping.SetItemChecked(i, false);
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void BindDtls()
        {
            try
            {
                DataTable dt_Send = DB.GetDT(string.Format("Select MenuID,Form_Caption From fn_MenuMaster_tbl() Where IsSeparator=0 and Form_Caption<>'-' order by ParentID asc"), false);
                ((ListBox)chkLstMapping).DataSource = dt_Send;
                ((ListBox)chkLstMapping).DisplayMember = "Form_Caption";
                ((ListBox)chkLstMapping).ValueMember = "MenuID";
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
                DBValue.Return_DBValue(this, txtCode, "VoucherTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtVoucherName, "VoucherName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, CboVoucherType, "MenuID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboVoucherNumber, "VoucherNumberingID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMenu, "GenMenuID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, chkAdvanceConfig, "AdvanceConfig", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "ActiveStatus", Enum_Define.ValidationType.Text);
                BindDtls();
                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    chkAdvanceConfig_CheckedChanged(null, null);
                }
                
                string strFind = DB.GetSnglValue("select MappingIDs from tbl_VoucherTypeMaster where isDeleted=0 and VoucherTypeID=" + txtCode.Text + "");
                string[] strMemberGArr = strFind.Split(',');
                if (strFind != "")
                {
                    for (int i = 0; i <= chkLstMapping.Items.Count - 1; i++)
                    {
                        DataRowView dr = (DataRowView)chkLstMapping.Items[i];
                        for (int j = 0; j <= strMemberGArr.Length - 1; j++)
                        {
                            if (dr[0].ToString() == strMemberGArr[j].ToString())
                            {
                                chkLstMapping.SetItemChecked(i, true);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < chkLstMapping.Items.Count; i++)
                    {
                        chkLstMapping.SetItemChecked(i, false);
                    }
                }

                using (IDataReader idr = DB.GetRS(string.Format("Select * from fn_VoucherNumberingMain_tbl() Where MenuID=" + txtMenu.Text + "")))
                {
                    while (idr.Read())
                    {
                        txtStartingNumber.Text = idr["VoucherNumberId"].ToString();
                        txtRestartStartingNumber.Text = idr["RestartNo"].ToString();
                        cboRestartParticular.SelectedValue = Localization.ParseNativeInt(idr["RestartingType"].ToString());
                        cboPrefixParticular.SelectedValue = Localization.ParseNativeInt(idr["PrefixType"].ToString());
                        cboSuffixParticular.SelectedValue = Localization.ParseNativeInt(idr["SuffixType"].ToString());
                        txtSufixText.Text = idr["Suffix"].ToString();
                        txtPrefixText.Text = idr["Prefix"].ToString();
                        txtWidthNumericPart.Text = idr["WidthofNumericPart"].ToString();
                        dtPrefixApplication.Text = Localization.ToVBDateString(idr["PrefixFrom"].ToString());
                        dtRestartApplicationFrom.Text = Localization.ToVBDateString(idr["RestartingFrom"].ToString());
                        dtSuffixApplicationDate.Text = Localization.ToVBDateString(idr["SuffixFrom"].ToString());
                        chkPrefillWithZero.Checked = Localization.ParseBoolean(idr["PrefillwithZero"].ToString());
                    }
                }
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
                string strchkvalue = string.Empty;
                string strHNchkvalue = string.Empty;
                for (int i = 0; i <= chkLstMapping.Items.Count - 1; i++)
                {
                    if (chkLstMapping.GetItemChecked(i) == true)
                    {
                        DataRowView dr = (DataRowView)chkLstMapping.Items[i];
                        strchkvalue = strchkvalue + dr[0].ToString() + ",";
                        strHNchkvalue = strHNchkvalue + dr[1].ToString() + ",";
                    }
                }
                if (strchkvalue.Length > 0)
                {
                    strchkvalue = strchkvalue.Substring(0, strchkvalue.Length - 1);
                }

                sComboAddText = txtVoucherName.Text;
                double MenuID = 0;
                int UserRightsID = 0;
                int ParentID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select ParentId From tbl_MenuMaster Where MenuID=" + CboVoucherType.SelectedValue + "")));
                int OrderBy = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select MAX(IsNull(OrderBy,0))+1 From tbl_MenuMaster Where ParentID=" + ParentID + "")));

                string strQryMain = string.Empty;
                string strQryDelete = string.Empty;
                string strQry = string.Empty;
                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    strQryMain = string.Format("Insert into tbl_MenuMaster (ParentID,VoucherTypeID,OrderBy,Menu_Caption,Form_Caption,ToolTip,TblName_Main,PmryColumn,SearchQry,SearchQry_Dtls,FormCall,FormCall_Web,FormType,MenuType,IsForm,IsSeparator,ApplyYear,IsVisible) Select  ParentID,"+CboVoucherType.SelectedValue+"," + OrderBy + "," + CommonLogic.SQuote(txtVoucherName.Text) + "," + CommonLogic.SQuote(txtVoucherName.Text) + "," + CommonLogic.SQuote(txtVoucherName.Text) + ",TblName_Main,PmryColumn,SearchQry,SearchQry_Dtls,FormCall,FormCall_Web,FormType,MenuType,IsForm,IsSeparator,ApplyYear,0 from tbl_MenuMaster Where MenuID=" + CboVoucherType.SelectedValue + "");
                    if (strQryMain.Length > 0)
                    {
                        MenuID = DB.ExecuteSQL_Trns(strQryMain);
                    }
                    txtMenu.Text = Convert.ToString(MenuID);
                }
                else if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    MenuID = Localization.ParseNativeInt(txtMenu.Text);
                    using (IDataReader idr = DB.GetRS(string.Format("Select * from tbl_MenuMaster Where MenuID=" + CboVoucherType.SelectedValue + "")))
                    {
                        while (idr.Read())
                        {
                            strQryMain = string.Format("update tbl_MenuMaster Set ParentID={0},OrderBy={1},Menu_Caption='{2}',Form_Caption='{3}',ToolTip='{4}',TblName_Main='{5}',PmryColumn='{6}',SearchQry='{7}',SearchQry_Dtls='{8}',FormCall='{9}',FormCall_Web='{10}',FormType='{11}',MenuType='{12}',VoucherTypeID={13},RefMenuID='{14}' where MenuID=" + MenuID + ";",
                                Convert.ToString(idr["parentID"]), Convert.ToString(idr["OrderBy"]),Convert.ToString(txtVoucherName.Text.Trim()),
                                Convert.ToString(txtVoucherName.Text.Trim()), Convert.ToString(txtVoucherName.Text.Trim()), Convert.ToString(idr["TblName_Main"]),
                                Convert.ToString(idr["PmryColumn"]), Convert.ToString(idr["SearchQry"]),
                                Convert.ToString(idr["SearchQry_Dtls"]), Convert.ToString(idr["FormCall"]),
                                Convert.ToString(idr["FormCall_Web"]), Convert.ToString(idr["FormType"]),
                                Convert.ToString(idr["MenuType"]), Convert.ToString(idr["VoucherTypeID"]),
                                Localization.ParseNativeInt(Convert.ToString(idr["RefMenuID"])));
                            DB.ExecuteSQL(strQryMain);
                        }
                    }

                }
                ArrayList pArrayData = new ArrayList
                {
                    txtVoucherName.Text.Trim(),
                    txtAliasName.Text==""?null:txtAliasName.Text,
                    CboVoucherType.SelectedValue,
                    cboVoucherNumber.SelectedValue,
                    txtMenu.Text,
                    chkAdvanceConfig.Checked?1:0,strchkvalue==""?"NULL":strchkvalue,
                    (ChkActive.Checked?1:0)
                };
                if (chkAdvanceConfig.Checked == true)
                {
                    strQry = string.Format("Delete From tbl_VoucherNumberingMain Where MenuID=" + MenuID + ";");
                    strQry += string.Format("Insert Into tbl_VoucherNumberingMain Values({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},getdate(),{16},0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL,0,NULL,NULL);",
                                MenuID, txtStartingNumber.Text, 0, CommonLogic.SQuote(Localization.ToSqlDateString(dtRestartApplicationFrom.Text)),
                                cboRestartParticular.SelectedValue, txtWidthNumericPart.Text, chkPrefillWithZero.Checked ? 1 : 0, CommonLogic.SQuote(txtPrefixText.Text),
                                CommonLogic.SQuote(Localization.ToSqlDateString(dtPrefixApplication.Text)), cboPrefixParticular.SelectedValue,
                                CommonLogic.SQuote(txtSufixText.Text), CommonLogic.SQuote(Localization.ToSqlDateString(dtSuffixApplicationDate.Text)),
                                cboSuffixParticular.SelectedValue, 0, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
                }
                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    //strQryDelete = string.Format("Delete From tbl_MenuMaster Where MenuID=" + txtMenu.Text + ";");
                    strQryDelete += string.Format("Delete From tbl_DeletePro Where MenuID=" + txtMenu.Text + ";");
                    strQryDelete += string.Format("Delete From tbl_MenuMaster_Comp Where MenuID=" + txtMenu.Text + ";");
                    strQryDelete += string.Format("Delete From tbl_GridSettings Where GridID=" + txtMenu.Text + ";");
                    strQryDelete += string.Format("Delete From tbl_GridFields_Mapping Where GridID=" + txtMenu.Text + ";");
                    strQryDelete += string.Format("Delete From tbl_GridSettings_tbls Where GridID=" + txtMenu.Text + ";");
                    strQryDelete += string.Format("Delete From tbl_ReportList Where ModuleID=" + txtMenu.Text + ";");
                    strQryDelete += string.Format("Delete From tbl_ReportQuery Where MenuID=" + txtMenu.Text + ";");
                    DB.ExecuteSQL(strQryDelete);

                    strQryMain = string.Format("Insert into tbl_MenuMaster_Comp Select MenuID,ParentID,OrderBy," + Db_Detials.CompID + "," + Db_Detials.UserType + " From tbl_MenuMaster Where MenuID=" + MenuID + ";");
                    DB.ExecuteSQL(strQryMain);
                }
                else if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                {
                    strQryMain = string.Format("Insert into tbl_MenuMaster_Comp (MenuID,ParentID,OrderBy,CompanyID,UserTypeID) Select MenuID,ParentID,OrderBy," + Db_Detials.CompID + "," + Db_Detials.UserType + " From tbl_MenuMaster Where MenuID=" + MenuID + ";");
                    DB.ExecuteSQL(strQryMain);
                }
                UserRightsID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select UserRightsID From tbl_UserRightsMain Where UserTypeID=" + Db_Detials.UserType + " and CompanyID=" + Db_Detials.CompID + "")));
                strQryMain = string.Format("Update tbl_UserRightsDtls Set View_Rights=1,Add_Rights=1,Edit_Rights=1,Delete_Rights=1,Print_Rights=1,Email_Rights=1,SMS_Rights=1,Settings_Rights=1 Where UserRightsID=" + UserRightsID + " and Form_MenuID=" + MenuID + ";");
                strQryMain += string.Format("Insert into tbl_GridSettings Select " + MenuID + ",SubGridID,ColIndex,VoucherType,ColFields,ColHeading,ColOrder,ColDataType,ColDataFormat,ColMaxLength,ColSize,IsEditable,IsHidden,IsRequired,IsRepeatRow,IsCompulsoryCol,ColCalcValue,NegAllow,BlankIfZero,ColAllignment,SumCols,Custom_Combo,OpenForm,Fill_Table,DisplayMember,ValueMember,whereCondition,ToolTip,Comments,CompID,YearID,AddedOn,AddedBy,IsModified,UpdatedOn,UpdatedBy,IsDeleted,DeletedOn,DeletedBy,IsCanclled,CancelledOn,CancelledBy,IsApproved,ApprovedOn,ApprovedBy,IsAudited,AuditedOn,AuditedBy From tbl_GridSettings  Where GridId=" + CboVoucherType.SelectedValue + ";");
                strQryMain += string.Format("Insert into tbl_GridSettings_tbls Select " + MenuID + ",SubGridID,TblName,TblCategory From tbl_GridSettings_tbls  Where GridId=" + CboVoucherType.SelectedValue + ";");
                strQryMain += string.Format("Insert into tbl_GridFields_Mapping Select " + MenuID + ",ColIndex,GridType,ColFields,ColHeading,ColDataType,IsEditable,IsHidden,IsRequired,IsEqual,IsInQuery,QueryName,LinkID,IfNull_Value,IsUnique,UniqueMsg From tbl_GridFields_Mapping  Where GridId=" + CboVoucherType.SelectedValue + ";");
                strQryMain += string.Format("Insert into tbl_ReportList Select " + MenuID + ",SubReportID,FormName,FormDesc,ReportName,ReportType,VoucherTypeID,QueryString,IsSelectQry,IsInternalRpt,Rows,Cols,PrinterName,PageSize,PaperOrientation,PrintCopies,IsBarcode,IsCheque,IsMulti,Remarks,CompID,YearID,AddedOn,AddedBy,IsModified,UpdatedOn,UpdatedBy,IsDeleted,DeletedOn,DeletedBy,IsCanclled,CancelledOn,CancelledBy,IsApproved,ApprovedOn,ApprovedBy,IsAudited,AuditedOn,AuditedBy From tbl_ReportList  Where ModuleID=" + CboVoucherType.SelectedValue + ";");
                strQryMain += string.Format("Insert into tbl_ReportQuery Select " + MenuID + "," + CboVoucherType.SelectedValue + ",QueryName,ReportName,IsParaDt,LedgerName,ComboBoxEnum,GraphQuery,IsGraph,CompID,YearID,AddedOn,AddedBy,IsModified,UpdatedOn,UpdatedBy,IsDeleted,DeletedOn,DeletedBy,IsCanclled,CancelledOn,CancelledBy,IsApproved,ApprovedOn,ApprovedBy,IsAudited,AuditedOn,AuditedBy From tbl_ReportQuery Where MenuID=" + CboVoucherType.SelectedValue + ";");
                strQryMain += string.Format("Insert into tbl_DeletePro Select  " + MenuID + ",Table_Function_Name,Type,IsValidate,IsMainTable,IsMaster,CompID,YearID,AddedOn,AddedBy,IsModified,UpdatedOn,UpdatedBy,IsDeleted,DeletedOn,DeletedBy,IsCanclled,CancelledOn,CancelledBy,IsApproved,ApprovedOn,ApprovedBy,IsAudited,AuditedOn,AuditedBy From tbl_DeletePro Where MenuID=" + CboVoucherType.SelectedValue + ";");
                DB.ExecuteSQL(strQryMain);
                //ReportListID = Localization.ParseNativeInt(DB.GetSnglValue(string.Format("Select Max(ReportListID) From tbl_ReportList Where IsDeleted=0 and  ModuleID=" + CboVoucherType.SelectedValue + ";")));
                //strQryMain = string.Format("Insert into tbl_ReportList_Parameters Select A.ReportID,A.ParameterFld,A.PrmyCOl,A.OrderNo,A.IsMandetory,A.IsBarcode from tbl_ReportList_Parameters As A Left Join tbl_ReportList AS B On A.ReportID=B.ReportID Where B.ModuleID=" + CboVoucherType.SelectedValue + "");
                double dTransID = 0;
                int RefMenuID = Localization.ParseNativeInt(CboVoucherType.SelectedValue.ToString());
                DBSp.Master_AddEdit_Trns(pArrayData, ref dTransID, strQry);
                strQryMain = "";
                if (base.blnFormAction == Enum_Define.ActionType.New_Record)
                    strQryMain = string.Format("Update tbl_MenuMaster Set MenuType='C',VoucherTypeID={0},RefMenuID='{1}' where MenuID={2};", dTransID, RefMenuID, MenuID);
                else if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                    strQryMain = string.Format("Update tbl_MenuMaster Set MenuType='C',VoucherTypeID={0},RefMenuID='{1}' where MenuID={2};", txtCode.Text, RefMenuID, MenuID);
                DB.ExecuteSQL(strQryMain);
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
            try
            {
               // string str;
                if (txtVoucherName.Text.Trim() == "" || txtVoucherName.Text.Trim() == "-" || txtVoucherName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Currency");
                    txtVoucherName.Focus();
                    return true;
                }

                string strTblName = "tbl_VoucherTypeMaster";
                if (DBSp.rtnAction())
                {
                    strTblName = "tbl_VoucherTypeMaster";
                    if (Navigate.CheckDuplicate(ref strTblName, "VoucherName", txtVoucherName.Text, false, "", 0, "", "This VoucherName is already available"))
                    {
                        txtVoucherName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtAliasName.Text, false, "", 0, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtVoucherName.Text, false, "", 0, "", "This VoucherName is already Used in AliasName"))
                    {
                        txtVoucherName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "VoucherName", txtAliasName.Text, false, "", 0, "", "This AliasName is already Used in VoucherName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    strTblName = "tbl_VoucherTypeMaster";
                    if (Navigate.CheckDuplicate(ref strTblName, "VoucherName", txtVoucherName.Text, true, "VoucherTypeID", Localization.ParseNativeLong(txtCode.Text), "", "This VoucherName is already available"))
                    {
                        txtVoucherName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtAliasName.Text, true, "VoucherTypeID", Localization.ParseNativeLong(txtCode.Text), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtVoucherName.Text, true, "VoucherTypeID", Localization.ParseNativeLong(txtCode.Text), "", "This VoucherName is already Used in AliasName"))
                    {
                        txtVoucherName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "VoucherName", txtAliasName.Text, true, "VoucherTypeID", Localization.ParseNativeLong(txtCode.Text), "", "This AliasName is already Used in VoucherName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                txtVoucherName_Leave(null, null);
                txtAliasName_Leave(null, null);
                return false;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                return false;
            }
        }

        #endregion Navigation

        private void txtVoucherName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtVoucherName, txtAliasName, "tbl_VoucherTypeMaster", "VoucherName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtVoucherName, txtAliasName, "tbl_VoucherTypeMaster", "VoucherName");
        }

        private void chkAdvanceConfig_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAdvanceConfig.Checked)
            {
                pnlDetails.Visible = true;
            }
            else
            {
                pnlDetails.Visible = false;
            }
        }

        private void cboPrefixParticular_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
