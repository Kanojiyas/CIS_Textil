using System;
using System.Collections;
using System.Data;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using  CIS_Bussiness;using CIS_DBLayer;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using CIS_DataGridViewEx;
using System.Diagnostics;

namespace CIS_Textil
{
    public partial class frmLedgerMaster : frmTrnsIface
    {
        public DataGridViewEx fgDtls = new DataGridViewEx();
        public DataGridViewEx fgDtls_BRS = new DataGridViewEx();
        public DataGridViewEx fgDtls_PYB = new DataGridViewEx();
        public DataGridViewEx fgDtls_BD = new DataGridViewEx();
        private string strBank;
        private string strCapital;
        private string strCr;
        private string strDExps;
        private string strDr;
        private string strIExps;
        private string strLoansLiab;
        private string strPurch;
        private string strSale;
        private string strTaxes;
        private string strIIncm;
        private bool flg_TaskType;
        private bool CT_REQ_LM;
        private bool ST_REQ_LM;
        private bool BR_REQ_LM;
        private bool TR_REQ_LM;
        private bool PS_REQ_LM;
        private bool IsReturn = false;

        public frmLedgerMaster()
        {
            fgDtls = new DataGridViewEx();
            fgDtls_BRS = new DataGridViewEx();
            fgDtls_PYB = new DataGridViewEx();
            fgDtls_BD = new DataGridViewEx();

            strDr = DB.GetSnglValue("select LValues from fn_LedgerGroupDtls(31)");
            strCr = DB.GetSnglValue("select LValues from fn_LedgerGroupDtls(27)");
            strLoansLiab = DB.GetSnglValue("select LValues from fn_LedgerGroupDtls(7)");
            strPurch = DB.GetSnglValue("select LValues from fn_LedgerGroupDtls(16)");
            strSale = DB.GetSnglValue("select LValues from fn_LedgerGroupDtls(15)");
            strBank = DB.GetSnglValue("select LValues from fn_LedgerGroupDtls(33)");
            strCapital = DB.GetSnglValue("select LValues from fn_LedgerGroupDtls(6)");
            strDExps = DB.GetSnglValue("select LValues from fn_LedgerGroupDtls(18)");
            strIIncm = DB.GetSnglValue("select LValues from fn_LedgerGroupDtls(19)");
            strIExps = DB.GetSnglValue("select LValues from fn_LedgerGroupDtls(20)");
            strTaxes = DB.GetSnglValue("select LValues from fn_LedgerGroupDtls(25)");

            InitializeComponent();
        }

        private void frmLedgerMaster_Load(object sender, EventArgs e)
        {
            try
            {
                GetCbo();
                DetailGrid_Setup.CreateDtlGrid(this, pnlBillDtls, fgDtls, dt_HasDtls_Grd, dt_AryCalcvalue, dt_AryIsRequired, true, true, true, 0, 0);
                DetailGrid_Setup.CreateDtlGrid(this, pnlBRSDtls, fgDtls_BRS, dt_HasDtls_Grd, dt_AryCalcvalue, dt_AryIsRequired, true, true, true, 0, 1);
                DetailGrid_Setup.CreateDtlGrid(this, pnlPastBalDtls, fgDtls_PYB, dt_HasDtls_Grd, dt_AryCalcvalue, dt_AryIsRequired, true, true, true, 0, 2);
                DetailGrid_Setup.CreateDtlGrid(this, pnlBankDetails, fgDtls_BD, dt_HasDtls_Grd, dt_AryCalcvalue, dt_AryIsRequired, true, true, true, 0, 3);

                pnlBRSMain.Visible = false;
                pnlBRSMain.Enabled = false;
                PnlPastBalMain.Visible = false;
                PnlPastBalMain.Enabled = false;
                pnlOpeningDtls.Visible = false;
                pnlOpeningDtls.Enabled = false;
                lblFormType.Visible = false;
                cboFormType.Visible = false;
                lblOpBal.Visible = false;
                lblOpCols.Visible = false;
                cboDrCrOp.Visible = false;
                txtOpBal2.Visible = false;
                lblBankBal.Visible = false;
                lblBankCol.Visible = false;
                txtBankBal.Visible = false;
                cboDrCrBank.Visible = false;

                tbLedgerDtls.TabPages.Remove(TbTaskTypeDetails);
                tbLedgerDtls.TabPages.Remove(TbBankDetails);
                flg_TaskType = Localization.ParseBoolean(GlobalVariables.LED_SHOWTYPE);
                CT_REQ_LM = Localization.ParseBoolean(GlobalVariables.CT_REQ_LM);
                ST_REQ_LM = Localization.ParseBoolean(GlobalVariables.ST_REQ_LM);
                BR_REQ_LM = Localization.ParseBoolean(GlobalVariables.BR_REQ_LM);
                TR_REQ_LM = Localization.ParseBoolean(GlobalVariables.TR_REQ_LM);
                PS_REQ_LM = Localization.ParseBoolean(GlobalVariables.PS_REQ_LM);
                if (flg_TaskType)
                {
                    tbLedgerDtls.TabPages.Add(TbTaskTypeDetails);
                    BindDtls();
                }
                else
                {
                    tbLedgerDtls.TabPages.Remove(TbTaskTypeDetails);
                }

                if (ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtLedgerName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtLedgerName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtLedgerName.Focus();
                        this.isComboAddText = true;
                        this.isSecondMessage = true;

                        try
                        {
                            if (Localization.ParseNativeInt(((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).GroupType.ToString()) > 0)
                                cboLedgerGroup.SelectedValue = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).GroupType;
                        }
                        catch { }
                    }
                }

                if (Db_Detials.IsLedgerEdit == true)
                {
                    chkIsBlocked.Enabled = true;
                }
                else
                {
                    chkIsBlocked.Enabled = false;
                }

                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    FillControls();
                }
                this.cboCityName.SelectedValueChanged += new System.EventHandler(this.cboCityName_LostFocus);
                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(fgDtls_CellValueChanged);
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

        public void GetCbo()
        {
            Combobox_Setup.FillCbo(ref cboLedgerGroup, Combobox_Setup.ComboType.Mst_LedgerGroup, "");
            Combobox_Setup.FillCbo(ref cboCityName, Combobox_Setup.ComboType.Mst_City, "");
            Combobox_Setup.FillCbo(ref cboStateName, Combobox_Setup.ComboType.Mst_State, "");
            Combobox_Setup.FillCbo(ref cboPartyGroup, Combobox_Setup.ComboType.Mst_PartyGroup, "");
            Combobox_Setup.FillCbo(ref cboCurrencySymbol, Combobox_Setup.ComboType.Mst_Currency, "");
            Combobox_Setup.FillCbo(ref cboBroker, Combobox_Setup.ComboType.Mst_Brokers, "");
            Combobox_Setup.FillCbo(ref cboVATClass, Combobox_Setup.ComboType.Mst_VAT_TAXClass, "");
            Combobox_Setup.FillCbo(ref cboDeducteeType, Combobox_Setup.ComboType.Mst_DeducteeType, "");
            Combobox_Setup.FillCbo(ref cboTypeofDutyTax, Combobox_Setup.ComboType.Mst_TypeofDutyTax, "");
            Combobox_Setup.FillCbo(ref cboTDSNatureofPymt, Combobox_Setup.ComboType.Mst_TDSNatureOfPymt, "");
            Combobox_Setup.FillCbo(ref cboNatureOfPayment, Combobox_Setup.ComboType.Mst_TDSNatureOfPymt, "");
            Combobox_Setup.FillCbo(ref cboInterestStyle, Combobox_Setup.ComboType.Mst_InterestStyle, "");
            Combobox_Setup.FillCbo(ref cboHaste, Combobox_Setup.ComboType.Mst_Haste, "");
            Combobox_Setup.FillCbo(ref cboFormType, Combobox_Setup.ComboType.FormType, "");
            Combobox_Setup.FillCbo(ref cboTransport, Combobox_Setup.ComboType.Mst_Transporter, "");
        }

        private void BindDtls()
        {
            try
            {
                DataTable dt_Send = DB.GetDT(string.Format("Select TaskTypeId,TaskTypeName From fn_TaskType_Tbl()"), false);
                ((ListBox)chklstTaskType).DataSource = dt_Send;
                ((ListBox)chklstTaskType).DisplayMember = "TaskTypeName";
                ((ListBox)chklstTaskType).ValueMember = "TaskTypeID";
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #region Form Navigation

        public void MovetoField()
        {
            try
            {
                GetCbo();
                ApplyActStatus();
                txtCode.Text = "";
                EventHandles.CreateDefault_Rows(this.fgDtls, dt_HasDtls_Grd, dt_AryCalcvalue, dt_AryIsRequired, false, false);
                EventHandles.CreateDefault_Rows(this.fgDtls_BRS, dt_HasDtls_Grd, dt_AryCalcvalue, dt_AryIsRequired, false, false);
                EventHandles.CreateDefault_Rows(this.fgDtls_PYB, dt_HasDtls_Grd, dt_AryCalcvalue, dt_AryIsRequired, false, false);
                EventHandles.CreateDefault_Rows(this.fgDtls_BD, dt_HasDtls_Grd, dt_AryCalcvalue, dt_AryIsRequired, false, false);
                int MaxID = Localization.ParseNativeInt(DB.GetSnglValue("Select Max(TaskTypeID) from fn_TaskType_tbl()"));
                txtLedgerName.Focus();
                chkActiveIntestCalc.Checked = false;
                ChkInventoryValuAffected.Checked = false;
                ChkMainBillByBill.Checked = false;
                chkIsTDSApplicable.Checked = false;
                ChkTDSDeductable.Checked = false;
                chkTCSApplicable.Checked = false;
                chkServiceTaxApplica.Checked = false;
                chkUseAssessableValueCalc.Checked = false;
                chkUsedInVATReturn.Checked = false;
                chkLBTApplicable.Checked = false;
                BindDtls();
                BindComp();
                ChkActive.Checked = true;
                ChkActive.Enabled = true;
                chkActiveIntestCalc.Enabled = true;
                chkIsBlocked.Enabled = true;
                cboLedgerGroup.Enabled = true;
                using (IDataReader reader = DB.GetRS("Select * from fn_TaskType_tbl() where TaskTypeID=" + MaxID + " "))
                {
                    if (reader.Read())
                    {
                        string strMemberGroup = reader["TaskTypeName"].ToString();
                        string[] strMemberGArr = strMemberGroup.Split(',');
                        if (strMemberGroup != "")
                        {
                            for (int i = 0; i <= chklstTaskType.Items.Count - 1; i++)
                            {
                                DataRowView dr = (DataRowView)chklstTaskType.Items[i];
                                for (int j = 0; j <= strMemberGArr.Length - 1; j++)
                                {
                                    if (dr[0].ToString() == strMemberGArr[j].ToString())
                                    {
                                        chklstTaskType.SetItemChecked(i, true);
                                    }
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < chklstTaskType.Items.Count; i++)
                            {
                                chklstTaskType.SetItemChecked(i, false);
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

        public void SaveRecord()
        {
            string strchkvalue = string.Empty;
            string strHNchkvalue = string.Empty;
            for (int i = 0; i <= chklstTaskType.Items.Count - 1; i++)
            {
                if (chklstTaskType.GetItemChecked(i) == true)
                {
                    DataRowView dr = (DataRowView)chklstTaskType.Items[i];
                    strchkvalue = strchkvalue + dr[0].ToString() + ",";
                    strHNchkvalue = strHNchkvalue + dr[1].ToString() + ",";
                }
            }

            if (strchkvalue.Length > 0)
            {
                strchkvalue = strchkvalue.Substring(0, strchkvalue.Length - 1);
            }


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


            try
            {
                sComboAddText = txtLedgerName.Text;
                ArrayList pArrayData = new ArrayList();
                pArrayData.Add(txtLedgerName.Text.Trim().Replace("'", "^"));
                pArrayData.Add(cboLedgerGroup.SelectedValue);
                pArrayData.Add(txtAliasName.Text.Trim());
                pArrayData.Add(ChkMainBillByBill.Checked == true ? 1 : 0);
                pArrayData.Add(txtDefaultCreditPeriod.Text.Trim());
                pArrayData.Add(txtCreditLimit.Text.Trim());
                pArrayData.Add(chkActiveIntestCalc.Checked == true ? 1 : 0);
                pArrayData.Add(txtInterestPer.Text.Trim());
                pArrayData.Add(cboInterestStyle.SelectedText.ToString());
                pArrayData.Add(ChkInventoryValuAffected.Checked == true ? 1 : 0);

                if (cboCurrencySymbol.SelectedValue == null)
                    pArrayData.Add("null");
                else
                    pArrayData.Add(cboCurrencySymbol.SelectedValue);

                pArrayData.Add(ChkTDSDeductable.Checked == true ? 1 : 0);
                if (cboDeducteeType.SelectedValue == null)
                    pArrayData.Add("null");
                else
                    pArrayData.Add(cboDeducteeType.SelectedValue);

                pArrayData.Add(chkIsTDSApplicable.Checked == true ? 1 : 0);
                if (cboNatureOfPayment.SelectedValue == null)
                    pArrayData.Add("null");
                else
                    pArrayData.Add(cboNatureOfPayment.SelectedValue);

                pArrayData.Add(chkTCSApplicable.Checked == true ? 1 : 0);
                pArrayData.Add(chkServiceTaxApplica.Checked == true ? 1 : 0);
                if (cboServiceTaxType.SelectedValue == null)
                    pArrayData.Add("null");
                else
                    pArrayData.Add(cboServiceTaxType.SelectedValue);

                pArrayData.Add(chkUseAssessableValueCalc.Checked == true ? 1 : 0);
                pArrayData.Add(chkUsedInVATReturn.Checked == true ? 1 : 0);
                if (cboVATClass.SelectedValue == null)
                    pArrayData.Add("null");
                else
                    pArrayData.Add(cboVATClass.SelectedValue);

                if (cboTypeofDutyTax.SelectedValue == null)
                    pArrayData.Add("null");
                else
                    pArrayData.Add(cboTypeofDutyTax.SelectedValue);

                if (cboTDSNatureofPymt.SelectedValue == null)
                    pArrayData.Add("null");
                else
                    pArrayData.Add(cboTDSNatureofPymt.SelectedValue);

                if (cboFormType.SelectedValue == null)
                    pArrayData.Add("null");
                else
                    pArrayData.Add(cboFormType.SelectedValue);

                pArrayData.Add(txtPercentofCalc.Text.ToString());
                pArrayData.Add(txtAddress.Text.ToString());
                pArrayData.Add(txtAddress1.Text.ToString());
                if (cboCityName.SelectedValue == null)
                    pArrayData.Add("null");
                else
                    pArrayData.Add(cboCityName.SelectedValue);
                if (cboStateName.SelectedValue == null)
                    pArrayData.Add("null");
                else
                    pArrayData.Add(cboStateName.SelectedValue);
                pArrayData.Add(txtPincode.Text.ToString());
                pArrayData.Add(txtContactPerson.Text.ToString());
                pArrayData.Add(txtPhoneNo.Text.ToString());
                pArrayData.Add(txtMobileNO.Text.ToString());
                pArrayData.Add(txtFaxNO.Text.ToString());
                pArrayData.Add(txtEmail.Text.ToString());
                pArrayData.Add(txtEmail1.Text.ToString());
                pArrayData.Add(txtEmail2.Text.ToString());
                pArrayData.Add(txtLandMark.Text.ToString());
                if (cboPriceLevel.SelectedValue == null)
                    pArrayData.Add("null");
                else
                    pArrayData.Add(cboPriceLevel.SelectedValue);
                if (cboBroker.SelectedValue == null)
                    pArrayData.Add("null");
                else
                    pArrayData.Add(cboBroker.SelectedValue);
                pArrayData.Add(txtBrokerPer.Text.ToString());
                if (cboHaste.SelectedValue == null)
                    pArrayData.Add("null");
                else
                    pArrayData.Add(cboHaste.SelectedValue);
                pArrayData.Add(txtHastePer.Text.ToString());
                if (cboTransport.SelectedValue == null)
                    pArrayData.Add("null");
                else
                    pArrayData.Add(cboTransport.SelectedValue);
                pArrayData.Add(txtAccountNo.Text.ToString());
                pArrayData.Add(txtBranchName.Text.ToString());
                pArrayData.Add(txtBSRCode.Text.ToString());
                if (cboPurchSales.SelectedValue == null)
                    pArrayData.Add("null");
                else
                    pArrayData.Add(cboPurchSales.SelectedValue);

                if (dtEffectiveDataReconcilation.Text == "__/__/____")
                    pArrayData.Add("null");
                else
                    pArrayData.Add(dtEffectiveDataReconcilation.TextFormat(false, true));

                pArrayData.Add(txtBankBal.Text.ToString());
                pArrayData.Add(txtWhatsappNo.Text.ToString());
                pArrayData.Add(txtFaceBook.Text.ToString());
                pArrayData.Add(txtPanNo.Text.ToString());
                pArrayData.Add(txtServiceTaxNo.Text.ToString());
                if (dtSerTaxDate.Text == "__/__/____")
                    pArrayData.Add("null");
                else
                    pArrayData.Add(dtSerTaxDate.TextFormat(false, true));
                pArrayData.Add(txtCstTinNo.Text.ToString());
                if (dtCstDate.Text == "__/__/____")
                    pArrayData.Add("null");
                else
                    pArrayData.Add(dtCstDate.TextFormat(false, true));
                pArrayData.Add(txtVatTinNo.Text.ToString());
                if (dtVatDate.Text == "__/__/____")
                    pArrayData.Add("null");
                else
                    pArrayData.Add(dtVatDate.TextFormat(false, true));
                pArrayData.Add(txtEccNo.Text.ToString());
                if (dtEccDate.Text == "__/__/____")
                    pArrayData.Add("null");
                else
                    pArrayData.Add(dtEccDate.TextFormat(false, true));
                pArrayData.Add(txtPlaNo.Text.ToString());
                pArrayData.Add(txtRange.Text);
                pArrayData.Add(txtDivision.Text);
                pArrayData.Add(txtCommiRate.Text);
                pArrayData.Add(cboSupplierType.SelectedText.ToString());

                if (cboPartyGroup.SelectedValue == null)
                    pArrayData.Add("null");
                else
                    pArrayData.Add(cboPartyGroup.SelectedValue);

                //if (cboDrCr.SelectedItem.ToString() == "Dr")
                //    pArrayData.Add(txtOpeningBal.Text.ToString());
                //else
                //    pArrayData.Add("0.00");

                //if (cboDrCr.SelectedItem.ToString() == "Cr")
                //    pArrayData.Add(txtOpeningBal.Text.ToString());
                //else
                //    pArrayData.Add("0.00");

                //Above Line Commented (Not Saving Opening Bal On Both Conditions Dr And Cr. And Added two line Below By Default 0.00)
                pArrayData.Add("0.00");
                pArrayData.Add("0.00");

                pArrayData.Add("0.00");
                pArrayData.Add("0.00");

                if (CboLocalTaxDealerType.SelectedValue != null)
                    pArrayData.Add("NULL");
                else
                    pArrayData.Add(CboLocalTaxDealerType.SelectedValue);
                pArrayData.Add(txtLocalTaxRegNo.Text.Trim());

                pArrayData.Add(txtRemarks.Text.ToString());
                pArrayData.Add(strchkvalue);

                pArrayData.Add("U");
                pArrayData.Add(ChkActive.Checked ? "1" : "0");
                pArrayData.Add(chkIsBlocked.Checked ? "1" : "0");
                pArrayData.Add(chkCalcDednwithnetamt.Checked ? 1 : 0);
                pArrayData.Add(txtIFSCode.Text.ToString());
                pArrayData.Add(txtMICRCode.Text.ToString());
                pArrayData.Add(txtNameOnPan.Text.ToString());
                pArrayData.Add(chkProvideBankDtls.Checked ? 1 : 0);
                pArrayData.Add(txtPrintingName.Text.ToString());

                pArrayData.Add(cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue);
                pArrayData.Add(cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue);
                pArrayData.Add(cboEI3.SelectedValue == null ? 0 : cboEI3.SelectedValue);
                pArrayData.Add(cboEI4.SelectedValue == null ? 0 : cboEI4.SelectedValue);
                pArrayData.Add(cboEI5.SelectedValue == null ? 0 : cboEI5.SelectedValue);
                pArrayData.Add(cboEI6.SelectedValue == null ? 0 : cboEI6.SelectedValue);

                if (dtED1.Text == "__/__/____")
                    pArrayData.Add("null");
                else
                    pArrayData.Add(dtED1.TextFormat(false, true));

                if (dtED2.Text == "__/__/____")
                    pArrayData.Add("null");
                else
                    pArrayData.Add(dtED2.TextFormat(false, true));

                if (dtED3.Text == "__/__/____")
                    pArrayData.Add("null");
                else
                    pArrayData.Add(dtED3.TextFormat(false, true));

                pArrayData.Add(txtET1.Text.Trim());
                pArrayData.Add(txtET2.Text.Trim());
                pArrayData.Add(txtET3.Text.Trim());
                pArrayData.Add(txtET4.Text.Trim());
                pArrayData.Add(txtET5.Text.Trim());
                pArrayData.Add(txtET6.Text.Trim());

                Db_Detials.IntCompID = strCheckedValue == null ? (Db_Detials.CompID).ToString() : (strCheckedValue == "" ? (Db_Detials.CompID).ToString() : strCheckedValue);

                string strQry = string.Empty + string.Format("Delete From {0} Where LedgerID = {1} And TransType = {2} And CompID = {3} And YearID = {4};", "tbl_AcLedger", "(#CodeID#)", base.iIDentity, Db_Detials.CompID, Db_Detials.YearID) + string.Format("Delete from tbl_LedgerMasterdtls where LedgerId = {0} and CompID = {1} and YearID = {2} ;", "(#CodeID#)", Db_Detials.CompID, Db_Detials.YearID) + string.Format("Delete from tbl_LedgerBlocked where LedgerId = {0} and CompID={1};", "(#CodeID#)", Db_Detials.CompID);
                string entryDate = Localization.ToVBDateString(DB.GetSnglValue(string.Format("select Yr_From from tbl_YearMaster where YearID={0}", Db_Detials.YearID)));
                if (chkIsBlocked.Checked == true)
                {
                    strQry += string.Format("Insert Into tbl_LedgerBlocked values({0},{1}) ", "(#CodeID#)", Db_Detials.CompID);
                }
                if (ChkMainBillByBill.Checked)
                {
                    DataGridViewEx fgDtls = this.fgDtls;
                    int iRowCount = fgDtls.Rows.Count - 1;
                    for (int i = 0; i <= iRowCount; i++)
                    {
                        DataGridViewRow row = fgDtls.Rows[i];
                        if (((!this.strDr.Contains(Conversions.ToString(this.cboLedgerGroup.SelectedValue)) & !this.strCr.Contains(Conversions.ToString(this.cboLedgerGroup.SelectedValue))) && (row.Cells[6].Value == null)) && ((Conversion.Val(this.txtOpeningBal.Text) > 0.0) & (this.cboDrCr.SelectedItem != null)))
                        {
                            if (Operators.ConditionalCompareObjectEqual(this.cboDrCr.SelectedItem, "Dr", false))
                            {
                                strQry += DBSp.InsertInto_AcLedger("(#CodeID#)", Conversions.ToString(i), "0", entryDate, (double)base.iIDentity, "(#CodeID#)", 1, Db_Detials.Ac_AdjType.NewRef, "0", "OP", entryDate, (double)base.iIDentity, Localization.ParseNativeDecimal(this.txtOpeningBal.Text), decimal.Zero, "null",Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID,Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                            }
                            else if (Operators.ConditionalCompareObjectEqual(this.cboDrCr.SelectedItem, "Cr", false))
                            {
                                strQry += DBSp.InsertInto_AcLedger("(#CodeID#)", Conversions.ToString(i), "0", entryDate, (double)base.iIDentity, "(#CodeID#)", 2, Db_Detials.Ac_AdjType.NewRef, "0", "OP", entryDate, (double)base.iIDentity, decimal.Zero, Localization.ParseNativeDecimal(this.txtOpeningBal.Text), "null", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID, Db_Detials.BranchID, DateAndTime.Now.Date);
                            }
                        }
                        if ((this.strDr.Contains(Conversions.ToString(this.cboLedgerGroup.SelectedValue)) && (row.Cells[6].Value != null)) && Operators.ConditionalCompareObjectGreater(row.Cells[6].Value, 0, false))
                        {
                            strQry += DBSp.InsertInto_AcLedger("(#CodeID#)", Conversions.ToString(i), DB.SQuote(Conversions.ToString(row.Cells[3].Value)), Conversions.ToString(row.Cells[4].Value), (double)base.iIDentity, "(#CodeID#)", Conversions.ToInteger(row.Cells[2].Value), Db_Detials.Ac_AdjType.NewRef, "(#CodeID#)", Conversions.ToString(row.Cells[3].Value), Conversions.ToString(row.Cells[4].Value), (double)base.iIDentity, Conversions.ToDecimal(Interaction.IIf(Operators.ConditionalCompareObjectEqual(row.Cells[2].Value, 1, false), Localization.ParseNativeDecimal(Conversions.ToString(row.Cells[6].Value)), 0)), Conversions.ToDecimal(Interaction.IIf(Operators.ConditionalCompareObjectEqual(row.Cells[2].Value, 1, false), 0, Localization.ParseNativeDecimal(Conversions.ToString(row.Cells[6].Value)))), "null", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID,Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                        }
                        if ((this.strCr.Contains(Conversions.ToString(this.cboLedgerGroup.SelectedValue)) && (row.Cells[6].Value != null)) && Operators.ConditionalCompareObjectGreater(row.Cells[6].Value, 0, false))
                        {
                            strQry += DBSp.InsertInto_AcLedger("(#CodeID#)", Conversions.ToString(i), DB.SQuote(Conversions.ToString(row.Cells[3].Value)), Conversions.ToString(row.Cells[4].Value), (double)base.iIDentity, "(#CodeID#)", Conversions.ToInteger(row.Cells[2].Value), Db_Detials.Ac_AdjType.NewRef, "(#CodeID#)", Conversions.ToString(row.Cells[3].Value), Conversions.ToString(row.Cells[4].Value), (double)base.iIDentity, Conversions.ToDecimal(Interaction.IIf(Operators.ConditionalCompareObjectEqual(row.Cells[2].Value, 1, false), Localization.ParseNativeDecimal(Conversions.ToString(row.Cells[6].Value)), 0)), Conversions.ToDecimal(Interaction.IIf(Operators.ConditionalCompareObjectEqual(row.Cells[2].Value, 1, false), 0, Localization.ParseNativeDecimal(Conversions.ToString(row.Cells[6].Value)))), "null", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID,Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                        }
                        //Ledger_Dtls Manual Saving
                        if (row.Cells[2].Value != null && row.Cells[3].Value != null && row.Cells[3].Value.ToString() != "-" && row.Cells[4].Value != null && row.Cells[4].Value.ToString() != "" && row.Cells[6].Value != null && row.Cells[6].Value.ToString() != "" && row.Cells[6].Value != "0")
                        {
                            strQry += string.Format("Insert into tbl_LedgerMasterdtls values ({0},{1},{2},'{3}','{4}',{5},{6},'{7}', {8}, {9})", new object[] { "(#CodeID#)", i + 1, row.Cells[2].Value, row.Cells[3].Value.ToString(), Localization.ToSqlDateString(row.Cells[4].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[5].Value.ToString()), Localization.ParseNativeDecimal(row.Cells[6].Value.ToString()), row.Cells[7].Value == null ? "NULL" : row.Cells[7].Value.ToString(), Db_Detials.CompID, Db_Detials.YearID });
                        }
                        row = null;
                    }
                    strQry += string.Format("Delete from tbl_LedgerMasterdtls where LedgerId = {0} and CompID = {1} and YearID = {2} and BillNo = 'On Account';", "(#CodeID#)", Db_Detials.CompID, Db_Detials.YearID);
                    if (this.strDr.Contains(Conversions.ToString(this.cboLedgerGroup.SelectedValue)) && (decimal.Compare(Localization.ParseNativeDecimal(this.txtOnAcc.Text), decimal.Zero) > 0) && (decimal.Compare(Localization.ParseNativeDecimal(this.txtOpeningBal.Text), decimal.Zero) > 0))
                    {
                        strQry += DBSp.InsertInto_AcLedger("(#CodeID#)", Conversions.ToString(0), "0", entryDate, (double)base.iIDentity, "(#CodeID#)", 1, Db_Detials.Ac_AdjType.NewRef, "(#CodeID#)", "On Account", entryDate, (double)base.iIDentity, Localization.ParseNativeDecimal(this.txtOnAcc.Text), decimal.Zero, "null", Db_Detials.StoreID,Db_Detials.CompID, Db_Detials.YearID,Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date) + string.Format("Insert into tbl_LedgerMasterdtls values ({0},{1},{2},'{3}','{4}',{5},{6},'{7}', {8}, {9})", new object[] { "(#CodeID#)", 0, 1, "On Account", Localization.ToSqlDateString(entryDate), Localization.ParseNativeDecimal(this.txtOnAcc.Text), Localization.ParseNativeDecimal(this.txtOnAcc.Text), "null", Db_Detials.CompID, Db_Detials.YearID });
                    }
                    if (this.strCr.Contains(Conversions.ToString(this.cboLedgerGroup.SelectedValue)) && (decimal.Compare(Localization.ParseNativeDecimal(this.txtOnAcc.Text), decimal.Zero) > 0) && (decimal.Compare(Localization.ParseNativeDecimal(this.txtOpeningBal.Text), decimal.Zero) > 0))
                    {
                        strQry += DBSp.InsertInto_AcLedger("(#CodeID#)", Conversions.ToString(0), "0", entryDate, (double)base.iIDentity, "(#CodeID#)", 2, Db_Detials.Ac_AdjType.NewRef, "(#CodeID#)", "On Account", entryDate, (double)base.iIDentity, decimal.Zero, Localization.ParseNativeDecimal(this.txtOnAcc.Text), "null",Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID,Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date) + string.Format("Insert into tbl_LedgerMasterdtls values ({0},{1},{2},'{3}','{4}',{5},{6},'{7}', {8}, {9})", new object[] { "(#CodeID#)", 0, 2, "On Account", Localization.ToSqlDateString(entryDate), Localization.ParseNativeDecimal(this.txtOnAcc.Text), Localization.ParseNativeDecimal(this.txtOnAcc.Text), "null", Db_Detials.CompID, Db_Detials.YearID });
                    }
                    fgDtls = null;
                }
                else if (Conversions.ToBoolean(Operators.AndObject(Operators.CompareObjectEqual(this.cboDrCr.SelectedItem, "Dr", false), decimal.Compare(Localization.ParseNativeDecimal(this.txtOpeningBal.Text), decimal.Zero) > 0)))
                {
                    strQry += DBSp.InsertInto_AcLedger("(#CodeID#)", Conversions.ToString(0), "0", entryDate, (double)base.iIDentity, "(#CodeID#)", 1, Db_Detials.Ac_AdjType.NewRef, "(#CodeID#)", "On Account", entryDate, (double)base.iIDentity, Localization.ParseNativeDecimal(this.txtOpeningBal.Text), decimal.Zero, "null",Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                }
                else if (Conversions.ToBoolean(Operators.AndObject(Operators.CompareObjectEqual(this.cboDrCr.SelectedItem, "Cr", false), decimal.Compare(Localization.ParseNativeDecimal(this.txtOpeningBal.Text), decimal.Zero) > 0)))
                {
                    strQry += DBSp.InsertInto_AcLedger("(#CodeID#)", Conversions.ToString(0), "0", entryDate, (double)base.iIDentity, "(#CodeID#)", 2, Db_Detials.Ac_AdjType.NewRef, "(#CodeID#)", "On Account", entryDate, (double)base.iIDentity, decimal.Zero, Localization.ParseNativeDecimal(this.txtOpeningBal.Text), "null",Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.YearID,Db_Detials.BranchID, Db_Detials.UserID, DateAndTime.Now.Date);
                }
                strQry = strQry.Replace("'null'", "null").Replace("Nnull", "null").Replace("*", "'");
                if (this.txtCode.Text != "")
                {
                    strQry = strQry.Replace("(#CodeID#)", txtCode.Text);
                }
                DBSp.Transcation_AddEdit(pArrayData, this.fgDtls_BRS, true, strQry, new DataGridViewEx[] { fgDtls_PYB, fgDtls_BD });
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
                DataGridViewEx ex2 = new DataGridViewEx();
                DataGridViewEx fgDtls = this.fgDtls;
                if (fgDtls.RowCount <= 1)
                {
                    int igrdROwCount = fgDtls.RowCount - 1;
                    for (int i = 0; i <= igrdROwCount; i++)
                    {
                        if (fgDtls.Rows[i].Cells[6].Value != null)
                        {
                            if (Localization.ParseNativeInt(fgDtls.Rows[i].Cells[6].Value.ToString()) != 0)
                            {
                                if (!EventHandles.IsValidGridReq(this.fgDtls, base.dt_AryIsRequired))
                                    return true;
                                if (!EventHandles.IsRequiredInGrid(this.fgDtls, this.dt_AryIsRequired, false))
                                    return true;
                            }
                        }
                    }
                }
                fgDtls = null;
                DataGridViewEx ex3 = this.fgDtls_BRS;
                if (ex3.RowCount <= 1)
                {
                    int num4 = ex3.RowCount - 1;
                    for (int j = 0; j <= num4; j++)
                    {
                        if (ex3.Rows[j].Cells[10].Value != null)
                        {
                            if (Localization.ParseNativeInt(ex3.Rows[j].Cells[10].Value.ToString()) != 0)
                            {
                                if (!EventHandles.IsValidGridReq(this.fgDtls_BRS, base.dt_AryIsRequired))
                                    return true;
                                if (!EventHandles.IsRequiredInGrid(this.fgDtls, this.dt_AryIsRequired, false))
                                    return true;
                            }
                        }
                    }
                }
                ex3 = null;
                if (txtLedgerName.Text.Trim() == "" || txtLedgerName.Text.Trim() == "-" || txtLedgerName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Ledger Name");
                    this.txtLedgerName.Focus();
                    return true;
                }
                if (txtPrintingName.Text.Trim() == "" || txtPrintingName.Text.Trim() == "-" || txtPrintingName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Printing Name");
                    this.txtPrintingName.Focus();
                    return true;
                }
                if (cboLedgerGroup.SelectedValue == null || cboLedgerGroup.SelectedValue.ToString() == "-" || cboLedgerGroup.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Ledger Type");
                    this.cboLedgerGroup.Focus();
                    return true;
                }
                if (!this.cboLedgerGroup.IsValidSelect)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Valid Ledger Type");
                    this.cboLedgerGroup.Focus();
                    return true;
                }
                if (Conversion.Val(this.txtOpeningBal.Text) > 0.0)
                {
                    if (this.cboDrCr.SelectedItem == null)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Dr/Cr");
                        this.cboDrCr.Focus();
                        return true;
                    }
                }

                if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT COUNT(0) FROM (Select IDs from split((select LValues from fn_LedgerGroupDtls((Select LedgerGroupID from tbl_LedgerGroupMaster where LedgerGroupName='Sundry Creditors')))))as A WHERE Ids=" + cboLedgerGroup.SelectedValue + "")) > 0)
                {
                    if (CT_REQ_LM)
                    {
                        if (cboCityName.SelectedValue == null || cboCityName.SelectedValue.ToString() == "-" || cboCityName.SelectedValue.ToString() == "0")
                        {
                            tbLedgerDtls.SelectedTab = tbMailingDetails;
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select City");
                            this.cboCityName.Focus();
                            return true;
                        }
                    }
                    if (ST_REQ_LM)
                    {
                        if (cboStateName.SelectedValue == null || cboStateName.SelectedValue.ToString() == "-" || cboStateName.SelectedValue.ToString() == "0")
                        {
                            tbLedgerDtls.SelectedTab = tbMailingDetails;
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select State");
                            this.cboStateName.Focus();
                            return true;
                        }
                    }
                    if (BR_REQ_LM)
                    {
                        if (cboBroker.SelectedValue == null || cboBroker.SelectedValue.ToString() == "-" || cboBroker.SelectedValue.ToString() == "0")
                        {
                            tbLedgerDtls.SelectedTab = tbMailingDetails;
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Broker");
                            this.cboBroker.Focus();
                            return true;
                        }
                    }
                    if (TR_REQ_LM)
                    {
                        if (cboTransport.SelectedValue == null || cboTransport.SelectedValue.ToString() == "-" || cboTransport.SelectedValue.ToString() == "0")
                        {
                            tbLedgerDtls.SelectedTab = tbMailingDetails;
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Transport");
                            this.cboTransport.Focus();
                            return true;
                        }
                    }
                    if (PS_REQ_LM)
                    {
                        if (cboPurchSales.SelectedValue == null || cboPurchSales.SelectedValue.ToString() == "-" || cboPurchSales.SelectedValue.ToString() == "0")
                        {
                            tbLedgerDtls.SelectedTab = tbLedgerDetails;
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Purchase/Sales Ac");
                            this.cboPurchSales.Focus();
                            return true;
                        }
                    }
                }

                if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT COUNT(0) FROM (Select IDs from split((select LValues from fn_LedgerGroupDtls((Select LedgerGroupID from tbl_LedgerGroupMaster where LedgerGroupName='Sundry Debtors')))))as A WHERE Ids=" + cboLedgerGroup.SelectedValue + "")) > 0)
                {
                    if (CT_REQ_LM)
                    {
                        if (cboCityName.SelectedValue == null || cboCityName.SelectedValue.ToString() == "-" || cboCityName.SelectedValue.ToString() == "0")
                        {
                            tbLedgerDtls.SelectedTab = tbMailingDetails;
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select City");
                            this.cboCityName.Focus();
                            return true;
                        }
                    }
                    if (ST_REQ_LM)
                    {
                        if (cboStateName.SelectedValue == null || cboStateName.SelectedValue.ToString() == "-" || cboStateName.SelectedValue.ToString() == "0")
                        {
                            tbLedgerDtls.SelectedTab = tbMailingDetails;
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select State");
                            this.cboStateName.Focus();
                            return true;
                        }
                    }
                    if (BR_REQ_LM)
                    {
                        if (cboBroker.SelectedValue == null || cboBroker.SelectedValue.ToString() == "-" || cboBroker.SelectedValue.ToString() == "0")
                        {
                            tbLedgerDtls.SelectedTab = tbMailingDetails;
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Broker");
                            this.cboBroker.Focus();
                            return true;
                        }
                    }
                    if (TR_REQ_LM)
                    {
                        if (cboTransport.SelectedValue == null || cboTransport.SelectedValue.ToString() == "-" || cboTransport.SelectedValue.ToString() == "0")
                        {
                            tbLedgerDtls.SelectedTab = tbMailingDetails;
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Transport");
                            this.cboTransport.Focus();
                            return true;
                        }
                    }
                    if (PS_REQ_LM)
                    {
                        if (cboPurchSales.SelectedValue == null || cboPurchSales.SelectedValue.ToString() == "-" || cboPurchSales.SelectedValue.ToString() == "0")
                        {
                            tbLedgerDtls.SelectedTab = tbLedgerDetails;
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Purchase/Sales Ac");
                            this.cboPurchSales.Focus();
                            return true;
                        }
                    }
                }
                if (ChkTDSDeductable.Checked == true)
                {
                    if (txtNameOnPan.Text.Trim() == "" || txtNameOnPan.Text.Trim() == "-" || txtNameOnPan.Text.Trim() == "0")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Name On Pan ");
                        this.txtNameOnPan.Focus();
                        return true;
                    }

                    if (txtServiceTaxNo.Text.Trim() == "" || txtServiceTaxNo.Text.Trim() == "-" || txtServiceTaxNo.Text.Trim() == "0")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Service Tax No");
                        this.txtServiceTaxNo.Focus();
                        return true;
                    }

                    if (txtCstTinNo.Text.Trim() == "" || txtCstTinNo.Text.Trim() == "-" || txtCstTinNo.Text.Trim() == "0")
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter CST Tin No");
                        this.txtCstTinNo.Focus();
                        return true;
                    }
                }

                string strTable;
                if (DBSp.rtnAction())
                {
                    strTable = "tbl_LedgerMaster";
                    if (Navigate.CheckDuplicate(ref strTable, "LedgerName", this.txtLedgerName.Text.Trim(), false, "", 0, "", "This LedgerName is already available"))
                    {
                        txtLedgerName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTable, "AliasName", this.txtAliasName.Text.Trim(), false, "", 0, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "AliasName", txtLedgerName.Text.Trim(), false, "", 0, "", "This LedgerName is already Used in AliasName"))
                    {
                        txtLedgerName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTable, "LedgerName", txtAliasName.Text.Trim(), false, "", 0, "", "This AliasName is already Used in LedgerName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    strTable = "tbl_LedgerMaster";
                    if (Navigate.CheckDuplicate(ref strTable, "LedgerName", txtLedgerName.Text.Trim(), true, "LedgerID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This LedgerName is already available"))
                    {
                        txtLedgerName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTable, "AliasName", txtAliasName.Text.Trim(), true, "LedgerID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "AliasName", txtLedgerName.Text.Trim(), true, "LedgerID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This LedgerName is already Used in AliasName"))
                    {
                        txtLedgerName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTable, "LedgerName", txtAliasName.Text.Trim(), true, "LedgerID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This AliasName is already Used in LedgerName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                txtLedgerName_Leave(null, null);
                txtAliasName_Leave(null, null);
                return false;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                return false;
            }
        }

        public void FillControls()
        {
            try
            {
                GetCbo();
                DBValue.Return_DBValue(this, txtCode, "LedgerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLedgerName, "LedgerName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboLedgerGroup, "LedgerGroupId", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPrintingName, "PrintingName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkMainBillByBill, "MBM", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDefaultCreditPeriod, "DCP", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCreditLimit, "CreditLimit", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, chkActiveIntestCalc, "AIC", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtInterestPer, "InterestPer", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboInterestStyle, "InterestStyle", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkInventoryValuAffected, "IVA", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboCurrencySymbol, "CurrencyId", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkTDSDeductable, "ITDSD", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboDeducteeType, "DeducteeType", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, chkIsTDSApplicable, "ITDSA", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboNatureOfPayment, "NatureofPaymentId", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, chkTCSApplicable, "ISTCS", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, chkServiceTaxApplica, "ISA", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboServiceTaxType, "ServiceTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, chkUseAssessableValueCalc, "UAVC", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, chkUsedInVATReturn, "UVR", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboVATClass, "VATTypeId", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTypeofDutyTax, "TaxTypeId", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTDSNatureofPymt, "TaxCategoryId", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboFormType, "FormTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPercentofCalc, "Percentage", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAddress, "Add1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAddress1, "Add2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboCityName, "CityId", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboStateName, "StateId", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPincode, "Pincode", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtContactPerson, "ContactPerson", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPhoneNo, "PhoneNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMobileNO, "MobileNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtFaxNO, "FaxNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEmail, "EmailId", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEmail1, "EmailId1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEmail2, "EmailId2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLandMark, "LandMark", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboPriceLevel, "PriceID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboBroker, "BrokerID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtBrokerPer, "BrokPercenatge", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboHaste, "HasteID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtHastePer, "HastePer", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTransport, "TransportID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAccountNo, "ACNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtBranchName, "BranchName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtBSRCode, "BSRCode", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboPurchSales, "PurchSalesID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEffectiveDataReconcilation, "EDR", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtBankBal, "BankBal", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtWhatsappNo, "WhatsappNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtFaceBook, "FaceBook", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPanNo, "PANNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtServiceTaxNo, "ServiceTaxNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtSerTaxDate, "ServiceTaxDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtCstTinNo, "CSTTinNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtCstDate, "CSTDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtVatTinNo, "VatTinNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtVatDate, "VatTinDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtEccNo, "EccNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEccDate, "EccDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtPlaNo, "PLANo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtRange, "Range", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtDivision, "Division", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCommiRate, "CommiRate", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboPartyGroup, "LedgerCategoryId", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, chkIsBlocked, "Blocked", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, chkCalcDednwithnetamt, "CalcDednWithNetAmt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtIFSCode, "IFSCode", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMICRCode, "MICRCode", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtNameOnPan, "NameOnPan", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, chkProvideBankDtls, "ProvideBankDtls", Enum_Define.ValidationType.Text);

                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI3, "EI3", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI4, "EI4", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI5, "EI5", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI6, "EI6", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtED1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, dtED2, "ED2", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, dtED3, "ED3", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET4, "ET4", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET5, "ET5", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET6, "ET6", Enum_Define.ValidationType.Text);

                //decimal OpDrBal;
                //decimal OpCrBal;

                //OpDrBal = Localization.ParseDBDecimal(DB.GetSnglValue(string.Format("select OpDrBal from tbl_LedgerMaster where LedgerID={0}", this.txtCode.Text)));
                //OpCrBal = Localization.ParseDBDecimal(DB.GetSnglValue(string.Format("select OpCrBal from tbl_LedgerMaster where LedgerID={0}", this.txtCode.Text)));
                //if (OpDrBal > 0)
                //{
                //    DBValue.Return_DBValue(this, txtOpeningBal, "OpDrBal", Enum_Define.ValidationType.Text);
                //    DBValue.Return_DBValue(this, txtOpBal2, "OpDrBal", Enum_Define.ValidationType.Text);
                //    this.cboDrCr.SelectedItem = "Dr";
                //}
                //if (OpCrBal > 0)
                //{
                //    DBValue.Return_DBValue(this, txtOpeningBal, "OpCrBal", Enum_Define.ValidationType.Text);
                //    DBValue.Return_DBValue(this, txtOpBal2, "OpCrBal", Enum_Define.ValidationType.Text);
                //    this.cboDrCr.SelectedItem = "Cr";
                //}

                DBValue.Return_DBValue(this, txtRemarks, "Remarks", Enum_Define.ValidationType.Text);
                //DetailGrid_Setup.FillGrid(this.fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "LedgerId", txtCode.Text, base.dt_HasDtls_Grd,this.iIDentity, GlobalVariables.VALIDATE_EDIT, Db_Detials.CompID.ToString());

                FillGrid_Manual(this.fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "LedgerId", txtCode.Text, base.dt_HasDtls_Grd, this.iIDentity, GlobalVariables.VALIDATE_EDIT, Db_Detials.CompID.ToString(), Db_Detials.YearID.ToString());
                DetailGrid_Setup.FillGrid(this.fgDtls_BRS, this.fgDtls_BRS.Grid_UID, this.fgDtls_BRS.Grid_Tbl, "LedgerId", txtCode.Text, base.dt_HasDtls_Grd);
                DetailGrid_Setup.FillGrid(this.fgDtls_PYB, this.fgDtls_PYB.Grid_UID, this.fgDtls_PYB.Grid_Tbl, "LedgerId", txtCode.Text, base.dt_HasDtls_Grd);
                DetailGrid_Setup.FillGrid(this.fgDtls_BD, this.fgDtls_BD.Grid_UID, this.fgDtls_BD.Grid_Tbl, "LedgerId", txtCode.Text, base.dt_HasDtls_Grd);

                CalcVal();
                try
                {
                    if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                        {
                            if (fgDtls.Rows[i].Cells[3].Value.ToString() == "On Account")
                            {
                                fgDtls.Rows.RemoveAt(i);
                            }
                        }
                        txtOpeningBal_Validated(null, null);
                    }
                }
                catch (Exception ex1)
                {
                    Navigate.logError(ex1.Message, ex1.StackTrace);
                }

                #region Fill Multiple Company
                BindComp();
                string str = DB.GetSnglValue("Select IntCompID from fn_LedgerMaster_Tbl() Where LedgerID=" + txtCode.Text + "");
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

                string strFind = DB.GetSnglValue("select TaskType from tbl_LedgerMaster where isDeleted=0 and  LedgerID=" + txtCode.Text + "");
                string[] strMemberGArr = strFind.Split(',');
                if (strFind != "")
                {
                    for (int i = 0; i <= chklstTaskType.Items.Count - 1; i++)
                    {
                        DataRowView dr = (DataRowView)chklstTaskType.Items[i];
                        for (int j = 0; j <= strMemberGArr.Length - 1; j++)
                        {
                            if (dr[0].ToString() == strMemberGArr[j].ToString())
                            {
                                chklstTaskType.SetItemChecked(i, true);
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < chklstTaskType.Items.Count; i++)
                    {
                        chklstTaskType.SetItemChecked(i, false);
                    }
                }
                string sUserType = string.Empty;
                if (base.blnFormAction == Enum_Define.ActionType.View_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    sUserType = DB.GetSnglValue("Select B.SecurityLvl from tbl_UserMaster AS A left join tbl_Securitymaster as B on A.UserType=B.SecurityID Where A.UserID=" + Db_Detials.UserID);
                    if (sUserType.ToUpper() != "SUPERADMIN" && sUserType.ToUpper() != "ADMIN")
                    {
                        string strLedger_UsedCount = DB.GetSnglValue("Select dbo.fn_ChkUse_LedgerMaster (" + txtCode.Text + ")");
                        if (Localization.ParseBoolean(strLedger_UsedCount) == true)
                        {
                            cboLedgerGroup.Enabled = false;
                        }
                        else
                        {
                            cboLedgerGroup.Enabled = true;
                        }
                    }
                    else
                    {
                        cboLedgerGroup.Enabled = true;
                    }
                }
                else
                {
                    cboLedgerGroup.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
            ApplyActStatus();
        }

        #endregion

        private void CalcVal()
        {
            try
            {
                decimal dblAdjAmtDr_Fill = 0;
                decimal dblAdjAmtCr_Fill = 0;
                decimal dblAdjAmtTotal_Fill = 0;
                DataGridViewEx fgDtls = this.fgDtls;
                //int iChkCompID = Localization.ParseNativeInt(DB.GetSnglValue("Select Distinct(CompID) From tbl_LedgerMasterdtls Where LedgerId= " + txtCode.Text));
                //int iChkYearID = Localization.ParseNativeInt(DB.GetSnglValue("Select Distinct(YearID) From tbl_LedgerMasterdtls Where LedgerId= " + txtCode.Text));

                //if (iChkCompID == Db_Detials.CompID && iChkYearID == Db_Detials.YearID)
                {
                    if (strDr.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)))
                    {
                        int iRowCnt = fgDtls.RowCount - 1;
                        for (int i = 0; i <= iRowCnt; i++)
                        {
                            DataGridViewRow row = fgDtls.Rows[i];
                            if (Operators.ConditionalCompareObjectGreater(row.Cells[6].Value, 0, false))
                            {
                                if (Operators.ConditionalCompareObjectEqual(row.Cells[2].Value, 1, false))
                                {
                                    dblAdjAmtDr_Fill = decimal.Add(dblAdjAmtDr_Fill, Localization.ParseNativeDecimal(Conversions.ToString(row.Cells[6].Value)));
                                }
                                else
                                {
                                    dblAdjAmtCr_Fill = decimal.Add(dblAdjAmtCr_Fill, Localization.ParseNativeDecimal(Conversions.ToString(row.Cells[6].Value)));
                                }
                            }
                            row = null;
                        }
                    }
                    dblAdjAmtTotal_Fill = decimal.Subtract(dblAdjAmtDr_Fill, dblAdjAmtCr_Fill);
                    txtTotalOp.Text = string.Format("{0:N2}", txtOpeningBal.Text.ToString());
                    txtAdjAmt.Text = Conversions.ToString(dblAdjAmtTotal_Fill);
                    txtOnAcc.Text = Conversions.ToString(decimal.Subtract(Localization.ParseNativeDecimal(this.txtTotalOp.Text), Localization.ParseNativeDecimal(this.txtAdjAmt.Text)));
                    if (this.strCr.Contains(Conversions.ToString(this.cboLedgerGroup.SelectedValue)))
                    {
                        int iRowCnt_fill = fgDtls.RowCount - 1;
                        for (int j = 0; j <= iRowCnt_fill; j++)
                        {
                            DataGridViewRow row2 = fgDtls.Rows[j];
                            if (Operators.ConditionalCompareObjectGreater(row2.Cells[6].Value, 0, false))
                            {
                                if (Operators.ConditionalCompareObjectEqual(row2.Cells[2].Value, 2, false))
                                {
                                    dblAdjAmtCr_Fill = decimal.Add(dblAdjAmtCr_Fill, Localization.ParseNativeDecimal(Conversions.ToString(row2.Cells[6].Value)));
                                }
                                else
                                {
                                    dblAdjAmtDr_Fill = decimal.Add(dblAdjAmtDr_Fill, Localization.ParseNativeDecimal(Conversions.ToString(row2.Cells[6].Value)));
                                }
                            }
                            row2 = null;
                        }
                        dblAdjAmtTotal_Fill = decimal.Subtract(dblAdjAmtCr_Fill, dblAdjAmtDr_Fill);
                        txtTotalOp.Text = string.Format("{0:N2}", txtOpeningBal.Text.ToString());
                        txtAdjAmt.Text = Conversions.ToString(dblAdjAmtTotal_Fill);
                    }
                    fgDtls = null;
                    txtOpeningBal.Text = dblAdjAmtTotal_Fill.ToString();
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboLedgerType_SelectedValueChanged(object sender, EventArgs e)
        {
            VisibleCtrls();
        }

        private void cboLedgerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cboLedgerGroup.Text == "Brokers")
            {
                cboBroker.Enabled = false;
                cboBroker.TabStop = false;
            }
            else
            {
                cboBroker.Enabled = true;
                cboBroker.TabStop = true;
            }
        }

        private void cboCityName_LostFocus(object sender, System.EventArgs e)
        {
            if ((cboCityName.SelectedItem != null))
            {
                try
                {
                    string sSTateID = DB.GetSnglValue("SELECT TOP 1 StateID from tbl_CityMaster WHERE CityID=" + cboCityName.SelectedValue);
                    cboStateName.SelectedValue = sSTateID;
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                }
            }
        }

        private void VisibleCtrls()
        {
            try
            {
                if (cboLedgerGroup.SelectedValue != null && cboLedgerGroup.SelectedValue.ToString() != "0")
                {
                    chkCalcDednwithnetamt.Enabled = false;
                    if (strDr.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)))
                    {
                        chkActiveIntestCalc.Enabled = true;
                        ChkInventoryValuAffected.Enabled = true;
                        ChkMainBillByBill.Visible = true;
                        ChkMainBillByBill.Enabled = true;
                        chkIsTDSApplicable.Enabled = false;
                        ChkTDSDeductable.Enabled = true;
                        chkTCSApplicable.Enabled = true;
                        chkLBTApplicable.Enabled = true;
                        chkServiceTaxApplica.Enabled = true;
                        chkUseAssessableValueCalc.Enabled = true;
                        chkUsedInVATReturn.Enabled = true;
                        CboLocalTaxDealerType.Enabled = true;
                        txtLocalTaxRegNo.Enabled = true;
                        txtDefaultCreditPeriod.Enabled = false;
                        txtCreditLimit.Enabled = false;
                        txtInterestPer.Enabled = false;
                        cboInterestStyle.Enabled = false;
                        cboCurrencySymbol.Enabled = true;
                        cboNatureOfPayment.Enabled = false;
                        cboServiceTaxType.Enabled = false;
                        cboVATClass.Enabled = false;
                        lblTypeofDutytax.Visible = false;
                        lblTypeofDutyTaxColon.Visible = false;
                        cboTypeofDutyTax.Visible = false;
                        cboTypeofDutyTax.Enabled = false;
                        cboTDSNatureofPymt.Enabled = false;
                        txtPercentofCalc.Enabled = false;
                        cboDeducteeType.Enabled = false;
                        dtEffectiveDataReconcilation.Enabled = false;
                        cboFormType.Enabled = true;
                        lblOpBal.Visible = false;
                        lblOpCols.Visible = false;
                        cboDrCrOp.Visible = false;
                        txtOpBal2.Visible = false;
                        lblBankBal.Visible = false;
                        lblBankCol.Visible = false;
                        txtBankBal.Visible = false;
                        cboDrCrBank.Visible = false;
                        chkCalcDednwithnetamt.Enabled = false;
                        cboPurchSales.Enabled = true;
                        Combobox_Setup.FillCbo(ref cboPurchSales, Combobox_Setup.ComboType.SalesAc, "");
                        cboDrCr.SelectedItem = "Dr";
                        AddressEnable();
                        TaxDetailsEnable();
                    }
                    if (strCr.Contains((cboLedgerGroup.SelectedValue.ToString())))
                    {
                        chkActiveIntestCalc.Enabled = true;
                        ChkInventoryValuAffected.Enabled = true;
                        ChkMainBillByBill.Visible = true;
                        ChkMainBillByBill.Enabled = true;
                        chkIsTDSApplicable.Enabled = false;
                        ChkTDSDeductable.Enabled = true;
                        chkTCSApplicable.Enabled = true;
                        chkServiceTaxApplica.Enabled = true;
                        chkUseAssessableValueCalc.Enabled = true;
                        chkUsedInVATReturn.Enabled = true;
                        txtDefaultCreditPeriod.Enabled = false;
                        txtCreditLimit.Enabled = false;
                        txtInterestPer.Enabled = false;
                        cboInterestStyle.Enabled = false;
                        cboCurrencySymbol.Enabled = true;
                        cboNatureOfPayment.Enabled = false;
                        cboServiceTaxType.Enabled = false;
                        cboVATClass.Enabled = false;
                        cboTypeofDutyTax.Enabled = false;
                        lblTypeofDutytax.Visible = false;
                        lblTypeofDutyTaxColon.Visible = false;
                        cboTypeofDutyTax.Visible = false;
                        cboTDSNatureofPymt.Enabled = false;
                        txtPercentofCalc.Enabled = false;
                        cboDeducteeType.Enabled = false;
                        dtEffectiveDataReconcilation.Enabled = false;
                        cboFormType.Enabled = true;
                        lblOpBal.Visible = false;
                        lblOpCols.Visible = false;
                        cboDrCrOp.Visible = false;
                        txtOpBal2.Visible = false;
                        lblBankBal.Visible = false;
                        lblBankCol.Visible = false;
                        txtBankBal.Visible = false;
                        cboDrCrBank.Visible = false;
                        cboPurchSales.Enabled = true;
                        chkCalcDednwithnetamt.Enabled = false;
                        Combobox_Setup.FillCbo(ref cboPurchSales, Combobox_Setup.ComboType.PurchaseAc, "");
                        cboDrCr.SelectedItem = "Cr";
                        AddressEnable();
                        TaxDetailsEnable();
                    }
                    if (strLoansLiab.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)))
                    {
                        chkActiveIntestCalc.Enabled = true;
                        ChkInventoryValuAffected.Enabled = false;
                        ChkMainBillByBill.Enabled = false;
                        chkIsTDSApplicable.Enabled = false;
                        ChkTDSDeductable.Enabled = false;
                        chkTCSApplicable.Enabled = false;
                        chkServiceTaxApplica.Enabled = false;
                        chkUseAssessableValueCalc.Enabled = false;
                        chkUsedInVATReturn.Enabled = false;
                        txtDefaultCreditPeriod.Enabled = false;
                        txtCreditLimit.Enabled = false;
                        txtInterestPer.Enabled = false;
                        cboInterestStyle.Enabled = false;
                        cboCurrencySymbol.Enabled = true;
                        cboNatureOfPayment.Enabled = false;
                        cboServiceTaxType.Enabled = false;
                        cboVATClass.Enabled = false;
                        cboTypeofDutyTax.Enabled = false;
                        lblTypeofDutytax.Visible = false;
                        lblTypeofDutyTaxColon.Visible = false;
                        cboTypeofDutyTax.Visible = false;
                        cboTDSNatureofPymt.Enabled = false;
                        chkCalcDednwithnetamt.Enabled = false;
                        txtPercentofCalc.Enabled = false;
                        cboDeducteeType.Enabled = false;
                        dtEffectiveDataReconcilation.Enabled = false;
                        cboPurchSales.Enabled = false;
                        cboFormType.Enabled = false;
                        AddressEnable();
                        TaxDetailsEnable();
                    }
                    if (strPurch.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)))
                    {
                        chkActiveIntestCalc.Enabled = false;
                        ChkInventoryValuAffected.Enabled = true;
                        ChkMainBillByBill.Visible = true;
                        ChkMainBillByBill.Enabled = false;
                        chkIsTDSApplicable.Enabled = false;
                        ChkTDSDeductable.Enabled = false;
                        chkTCSApplicable.Enabled = false;
                        chkServiceTaxApplica.Enabled = false;
                        txtDefaultCreditPeriod.Enabled = false;
                        txtCreditLimit.Enabled = false;
                        txtInterestPer.Enabled = false;
                        cboInterestStyle.Enabled = false;
                        cboCurrencySymbol.Enabled = true;
                        cboNatureOfPayment.Enabled = false;
                        cboServiceTaxType.Enabled = false;
                        cboVATClass.Enabled = false;
                        cboTypeofDutyTax.Enabled = false;
                        lblTypeofDutytax.Visible = false;
                        lblTypeofDutyTaxColon.Visible = false;
                        cboTypeofDutyTax.Visible = false;
                        cboTDSNatureofPymt.Enabled = false;
                        chkCalcDednwithnetamt.Enabled = false;
                        txtPercentofCalc.Enabled = false;
                        cboDeducteeType.Enabled = false;
                        dtEffectiveDataReconcilation.Enabled = false;
                        cboPurchSales.Enabled = false;
                        cboFormType.Enabled = false;
                        chkUseAssessableValueCalc.Enabled = true;
                        chkUsedInVATReturn.Enabled = true;
                        AddressDisable();
                        TaxDetailsDisable();
                    }
                    if (strSale.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)))
                    {
                        chkActiveIntestCalc.Enabled = false;
                        ChkInventoryValuAffected.Enabled = true;
                        ChkMainBillByBill.Enabled = false;
                        ChkMainBillByBill.Visible = true;
                        ChkTDSDeductable.Enabled = false;
                        chkTCSApplicable.Enabled = false;
                        chkServiceTaxApplica.Enabled = false;
                        txtDefaultCreditPeriod.Enabled = false;
                        txtCreditLimit.Enabled = false;
                        txtInterestPer.Enabled = false;
                        cboInterestStyle.Enabled = false;
                        cboCurrencySymbol.Enabled = true;
                        cboNatureOfPayment.Enabled = false;
                        cboServiceTaxType.Enabled = false;
                        cboVATClass.Enabled = false;
                        cboTypeofDutyTax.Enabled = false;
                        lblTypeofDutytax.Visible = false;
                        lblTypeofDutyTaxColon.Visible = false;
                        cboTypeofDutyTax.Visible = false;
                        cboTDSNatureofPymt.Enabled = false;
                        chkCalcDednwithnetamt.Enabled = false;
                        txtPercentofCalc.Enabled = false;
                        cboDeducteeType.Enabled = false;
                        dtEffectiveDataReconcilation.Enabled = false;
                        cboPurchSales.Enabled = false;
                        cboFormType.Enabled = false;
                        chkUseAssessableValueCalc.Enabled = true;
                        chkUsedInVATReturn.Enabled = true;
                        chkIsTDSApplicable.Enabled = true;
                        AddressDisable();
                        TaxDetailsDisable();
                    }
                    if (strBank.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)))
                    {
                        chkActiveIntestCalc.Enabled = false;
                        ChkInventoryValuAffected.Enabled = false;
                        ChkMainBillByBill.Enabled = false;
                        ChkMainBillByBill.Visible = true;
                        chkIsTDSApplicable.Enabled = false;
                        ChkTDSDeductable.Enabled = false;
                        chkTCSApplicable.Enabled = false;
                        chkServiceTaxApplica.Enabled = false;
                        chkUseAssessableValueCalc.Enabled = false;
                        chkUsedInVATReturn.Enabled = false;
                        txtDefaultCreditPeriod.Enabled = false;
                        txtCreditLimit.Enabled = false;
                        txtInterestPer.Enabled = false;
                        cboInterestStyle.Enabled = false;
                        cboCurrencySymbol.Enabled = true;
                        cboNatureOfPayment.Enabled = false;
                        cboServiceTaxType.Enabled = false;
                        cboVATClass.Enabled = false;
                        cboTypeofDutyTax.Enabled = false;
                        lblTypeofDutytax.Visible = false;
                        lblTypeofDutyTaxColon.Visible = false;
                        cboTypeofDutyTax.Visible = false;
                        cboTDSNatureofPymt.Enabled = false;
                        txtPercentofCalc.Enabled = false;
                        cboDeducteeType.Enabled = false;
                        cboPurchSales.Enabled = false;
                        chkCalcDednwithnetamt.Enabled = false;
                        AddressEnable();
                        TaxDetailsDisable();
                        dtEffectiveDataReconcilation.Enabled = true;
                        cboFormType.Enabled = false;
                        lblOpBal.Visible = true;
                        lblOpCols.Visible = true;
                        cboDrCrOp.Visible = true;
                        txtOpBal2.Visible = true;
                        lblBankBal.Visible = true;
                        lblBankCol.Visible = true;
                        txtBankBal.Visible = true;
                        cboDrCrBank.Visible = true;
                    }
                    if (strCapital.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)))
                    {
                        chkActiveIntestCalc.Enabled = false;
                        ChkInventoryValuAffected.Enabled = false;
                        ChkMainBillByBill.Enabled = false;
                        ChkMainBillByBill.Visible = true;
                        chkIsTDSApplicable.Enabled = false;
                        ChkTDSDeductable.Enabled = false;
                        chkTCSApplicable.Enabled = false;
                        chkServiceTaxApplica.Enabled = false;
                        chkUseAssessableValueCalc.Enabled = false;
                        chkUsedInVATReturn.Enabled = false;
                        txtDefaultCreditPeriod.Enabled = false;
                        txtCreditLimit.Enabled = false;
                        txtInterestPer.Enabled = false;
                        cboInterestStyle.Enabled = false;
                        cboCurrencySymbol.Enabled = true;
                        cboNatureOfPayment.Enabled = false;
                        cboServiceTaxType.Enabled = false;
                        cboVATClass.Enabled = false;
                        cboTypeofDutyTax.Enabled = false;
                        lblTypeofDutytax.Visible = false;
                        lblTypeofDutyTaxColon.Visible = false;
                        cboTypeofDutyTax.Visible = false;
                        cboTDSNatureofPymt.Enabled = false;
                        txtPercentofCalc.Enabled = false;
                        cboDeducteeType.Enabled = false;
                        cboPurchSales.Enabled = false;
                        chkCalcDednwithnetamt.Enabled = false;
                        AddressEnable();
                        TaxDetailsEnable();
                        dtEffectiveDataReconcilation.Enabled = false;
                        lblOpBal.Visible = false;
                        lblOpCols.Visible = false;
                        cboDrCrOp.Visible = false;
                        txtOpBal2.Visible = false;
                        lblBankBal.Visible = false;
                        lblBankCol.Visible = false;
                        txtBankBal.Visible = false;
                        cboDrCrBank.Visible = false;
                        cboFormType.Enabled = false;

                    }

                    if (strDExps.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)))
                    {
                        chkActiveIntestCalc.Enabled = false;
                        ChkInventoryValuAffected.Enabled = false;
                        ChkMainBillByBill.Enabled = false;
                        ChkMainBillByBill.Visible = true;
                        ChkTDSDeductable.Enabled = false;
                        chkTCSApplicable.Enabled = false;
                        chkServiceTaxApplica.Enabled = false;
                        chkUseAssessableValueCalc.Enabled = false;
                        chkUsedInVATReturn.Enabled = false;
                        txtDefaultCreditPeriod.Enabled = false;
                        txtCreditLimit.Enabled = false;
                        txtInterestPer.Enabled = false;
                        cboInterestStyle.Enabled = false;
                        cboCurrencySymbol.Enabled = true;
                        cboNatureOfPayment.Enabled = false;
                        cboServiceTaxType.Enabled = false;
                        cboVATClass.Enabled = false;
                        cboTypeofDutyTax.Enabled = false;
                        lblTypeofDutytax.Visible = false;
                        lblTypeofDutyTaxColon.Visible = false;
                        cboTypeofDutyTax.Visible = false;
                        cboTDSNatureofPymt.Enabled = false;
                        txtPercentofCalc.Enabled = false;
                        cboDeducteeType.Enabled = false;
                        dtEffectiveDataReconcilation.Enabled = false;
                        cboPurchSales.Enabled = false;
                        cboFormType.Enabled = false;
                        chkIsTDSApplicable.Enabled = true;
                        chkCalcDednwithnetamt.Enabled = false;
                        AddressDisable();
                        TaxDetailsDisable();
                    }
                    if (strTaxes.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)))
                    {
                        chkActiveIntestCalc.Enabled = false;
                        ChkInventoryValuAffected.Enabled = false;
                        ChkMainBillByBill.Enabled = false;
                        ChkMainBillByBill.Visible = false;
                        ChkTDSDeductable.Enabled = false;
                        chkTCSApplicable.Enabled = false;
                        chkServiceTaxApplica.Enabled = false;
                        chkUseAssessableValueCalc.Enabled = true;
                        chkUsedInVATReturn.Enabled = false;
                        txtDefaultCreditPeriod.Enabled = false;
                        txtCreditLimit.Enabled = false;
                        txtInterestPer.Enabled = false;
                        cboInterestStyle.Enabled = false;
                        cboCurrencySymbol.Enabled = true;
                        cboNatureOfPayment.Enabled = false;
                        cboServiceTaxType.Enabled = false;
                        cboVATClass.Enabled = false;
                        cboTDSNatureofPymt.Enabled = false;
                        txtPercentofCalc.Enabled = true;
                        cboDeducteeType.Enabled = false;
                        dtEffectiveDataReconcilation.Enabled = false;
                        cboPurchSales.Enabled = false;
                        cboFormType.Enabled = false;
                        chkIsTDSApplicable.Enabled = true;
                        cboTypeofDutyTax.Enabled = true;
                        lblTypeofDutytax.Visible = true;
                        lblTypeofDutyTaxColon.Visible = true;
                        cboTypeofDutyTax.Visible = true;
                        AddressDisable();
                        TaxDetailsDisable();
                    }
                    if (strIIncm.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)))
                    {
                        chkActiveIntestCalc.Enabled = false;
                        ChkInventoryValuAffected.Enabled = false;
                        ChkMainBillByBill.Enabled = false;
                        ChkMainBillByBill.Visible = false;
                        ChkTDSDeductable.Enabled = false;
                        chkTCSApplicable.Enabled = false;
                        chkServiceTaxApplica.Enabled = false;
                        chkUseAssessableValueCalc.Enabled = true;
                        chkUsedInVATReturn.Enabled = false;
                        txtDefaultCreditPeriod.Enabled = false;
                        txtCreditLimit.Enabled = false;
                        txtInterestPer.Enabled = false;
                        cboInterestStyle.Enabled = false;
                        cboCurrencySymbol.Enabled = true;
                        cboNatureOfPayment.Enabled = false;
                        cboServiceTaxType.Enabled = false;
                        cboVATClass.Enabled = false;
                        cboTDSNatureofPymt.Enabled = false;
                        txtPercentofCalc.Enabled = true;
                        cboDeducteeType.Enabled = false;
                        dtEffectiveDataReconcilation.Enabled = false;
                        cboPurchSales.Enabled = false;
                        cboFormType.Enabled = false;
                        chkIsTDSApplicable.Enabled = true;
                        cboTypeofDutyTax.Enabled = true;
                        lblTypeofDutytax.Visible = true;
                        lblTypeofDutyTaxColon.Visible = true;
                        cboTypeofDutyTax.Visible = true;
                        AddressDisable();
                        TaxDetailsDisable();
                    }
                    if (strIExps.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)))
                    {
                        chkActiveIntestCalc.Enabled = false;
                        ChkInventoryValuAffected.Enabled = false;
                        ChkMainBillByBill.Enabled = false;
                        ChkMainBillByBill.Visible = false;
                        ChkTDSDeductable.Enabled = false;
                        chkTCSApplicable.Enabled = false;
                        chkServiceTaxApplica.Enabled = false;
                        chkUseAssessableValueCalc.Enabled = true;
                        chkUsedInVATReturn.Enabled = false;
                        txtDefaultCreditPeriod.Enabled = false;
                        txtCreditLimit.Enabled = false;
                        txtInterestPer.Enabled = false;
                        cboInterestStyle.Enabled = false;
                        cboCurrencySymbol.Enabled = true;
                        cboNatureOfPayment.Enabled = false;
                        cboServiceTaxType.Enabled = false;
                        cboVATClass.Enabled = false;
                        cboTDSNatureofPymt.Enabled = false;
                        txtPercentofCalc.Enabled = true;
                        cboDeducteeType.Enabled = false;
                        dtEffectiveDataReconcilation.Enabled = false;
                        cboPurchSales.Enabled = false;
                        cboFormType.Enabled = false;
                        chkIsTDSApplicable.Enabled = true;
                        cboTypeofDutyTax.Enabled = true;
                        lblTypeofDutytax.Visible = true;
                        lblTypeofDutyTaxColon.Visible = true;
                        cboTypeofDutyTax.Visible = true;
                        AddressDisable();
                        TaxDetailsDisable();
                    }

                    object selectedValue = cboLedgerGroup.SelectedValue;

                    switch (Localization.ParseNativeInt(cboLedgerGroup.SelectedValue.ToString()))
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                        case 12:
                        case 13:
                        case 14:
                        case 17:
                        case 19:
                        case 26:
                        case 29:
                        case 32:
                            chkActiveIntestCalc.Enabled = false;
                            ChkInventoryValuAffected.Enabled = false;
                            ChkMainBillByBill.Enabled = false;
                            ChkMainBillByBill.Visible = true;
                            chkIsTDSApplicable.Enabled = false;
                            ChkTDSDeductable.Enabled = false;
                            chkTCSApplicable.Enabled = false;
                            chkServiceTaxApplica.Enabled = false;
                            chkUseAssessableValueCalc.Enabled = false;
                            chkUsedInVATReturn.Enabled = false;
                            chkLBTApplicable.Enabled = false;
                            txtDefaultCreditPeriod.Enabled = false;
                            CboLocalTaxDealerType.Enabled = false;
                            txtLocalTaxRegNo.Enabled = false;
                            txtCreditLimit.Enabled = false;
                            txtInterestPer.Enabled = false;
                            cboInterestStyle.Enabled = false;
                            cboCurrencySymbol.Enabled = true;
                            cboNatureOfPayment.Enabled = false;
                            cboServiceTaxType.Enabled = false;
                            cboVATClass.Enabled = false;
                            cboTypeofDutyTax.Enabled = false;
                            lblTypeofDutytax.Visible = false;
                            lblTypeofDutyTaxColon.Visible = false;
                            cboTypeofDutyTax.Visible = false;
                            cboTDSNatureofPymt.Enabled = false;
                            txtPercentofCalc.Enabled = false;
                            cboDeducteeType.Enabled = false;
                            dtEffectiveDataReconcilation.Enabled = false;
                            cboPurchSales.Enabled = false;
                            cboFormType.Enabled = false;
                            lblOpBal.Visible = false;
                            lblOpCols.Visible = false;
                            cboDrCrOp.Visible = false;
                            txtOpBal2.Visible = false;
                            lblBankBal.Visible = false;
                            lblBankCol.Visible = false;
                            txtBankBal.Visible = false;
                            cboDrCrBank.Visible = false;
                            AddressDisable();
                            TaxDetailsDisable();
                            break;
                    }
                    switch (Localization.ParseNativeInt(cboLedgerGroup.SelectedValue.ToString()))
                    {
                        case 19:
                        case 20:
                        case 25:
                            chkCalcDednwithnetamt.Enabled = true;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void chkActiveIntestCalc_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActiveIntestCalc.Checked)
            {
                txtInterestPer.Enabled = true;
                cboInterestStyle.Enabled = true;
            }
            else
            {
                txtInterestPer.Enabled = false;
                cboInterestStyle.Enabled = false;
            }
        }

        private void ChkTDSDeductable_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkTDSDeductable.Checked)
            {
                cboDeducteeType.Enabled = true;
                chkTCSApplicable.Enabled = false;
                txtNameOnPan.IsMandatory = true;
                txtCstTinNo.IsMandatory = true;
                txtServiceTaxNo.IsMandatory = true;
            }
            else
            {
                cboDeducteeType.Enabled = false;
                chkTCSApplicable.Enabled = true;
                txtNameOnPan.IsMandatory = false;
                txtCstTinNo.IsMandatory = false;
                txtServiceTaxNo.IsMandatory = false;
            }
        }

        private void chkServiceTaxApplica_CheckedChanged(object sender, EventArgs e)
        {
            if (chkServiceTaxApplica.Checked)
            {
                cboServiceTaxType.Enabled = true;
            }
            else
            {
                cboServiceTaxType.Enabled = false;
            }
        }

        private void chkUsedInVATReturn_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUsedInVATReturn.Checked)
            {
                cboVATClass.Enabled = true;
            }
            else
            {
                cboVATClass.Enabled = false;
            }
        }

        private void chkProvideBankDtls_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProvideBankDtls.Checked == true)
            {
                tbLedgerDtls.TabPages.Add(TbBankDetails);
            }
            else
            {
                tbLedgerDtls.TabPages.Remove(TbBankDetails);
            }
        }

        private void AddressDisable()
        {
            txtAddress.Enabled = false;
            txtAddress1.Enabled = false;
            cboCityName.Enabled = false;
            cboStateName.Enabled = false;
            txtPincode.Enabled = false;
            txtContactPerson.Enabled = false;
            txtPhoneNo.Enabled = false;
            txtMobileNO.Enabled = false;
            txtFaxNO.Enabled = false;
            txtEmail.Enabled = false;
            cboPriceLevel.Enabled = false;
            cboBroker.Enabled = false;
            txtBrokerPer.Enabled = false;
            cboHaste.Enabled = false;
            txtHastePer.Enabled = false;
            cboTransport.Enabled = false;
            txtAccountNo.Enabled = false;
            txtBranchName.Enabled = false;
            txtBSRCode.Enabled = false;
            txtIFSCode.Enabled = false;
            txtMICRCode.Enabled = false;
            lblOpBal.Visible = false;
            lblOpCols.Visible = false;
            cboDrCrOp.Visible = false;
            txtOpBal2.Visible = false;
            lblBankBal.Visible = false;
            lblBankCol.Visible = false;
            txtBankBal.Visible = false;
            cboDrCrBank.Visible = false;
        }

        private void AddressEnable()
        {
            txtAddress.Enabled = true;
            txtAddress1.Enabled = true;
            cboCityName.Enabled = true;
            cboStateName.Enabled = true;
            txtPincode.Enabled = true;
            txtContactPerson.Enabled = true;
            txtPhoneNo.Enabled = true;
            txtMobileNO.Enabled = true;
            txtFaxNO.Enabled = true;
            txtEmail.Enabled = true;
            cboPriceLevel.Enabled = true;
            cboBroker.Enabled = true;
            txtBrokerPer.Enabled = true;
            cboHaste.Enabled = true;
            txtHastePer.Enabled = true;
            cboTransport.Enabled = true;
            txtAccountNo.Enabled = true;
            txtBranchName.Enabled = true;
            txtBSRCode.Enabled = true;
            txtIFSCode.Enabled = true;
            txtMICRCode.Enabled = true;
            lblOpBal.Visible = false;
            lblOpCols.Visible = false;
            cboDrCrOp.Visible = false;
            txtOpBal2.Visible = false;
            lblBankBal.Visible = false;
            lblBankCol.Visible = false;
            txtBankBal.Visible = false;
            cboDrCrBank.Visible = false;
        }

        private void TaxDetailsEnable()
        {
            txtPanNo.Enabled = true;
            txtServiceTaxNo.Enabled = true;
            dtSerTaxDate.Enabled = true;
            txtCstTinNo.Enabled = true;
            dtCstDate.Enabled = true;
            txtVatTinNo.Enabled = true;
            dtVatDate.Enabled = true;
            txtEccNo.Enabled = true;
            dtEccDate.Enabled = true;
            txtPlaNo.Enabled = true;
            txtRange.Enabled = true;
            txtDivision.Enabled = true;
            txtCommiRate.Enabled = true;
            cboSupplierType.Enabled = true;

        }

        private void TaxDetailsDisable()
        {
            txtPanNo.Enabled = false;
            txtServiceTaxNo.Enabled = false;
            dtSerTaxDate.Enabled = false;
            txtCstTinNo.Enabled = false;
            dtCstDate.Enabled = false;
            txtVatTinNo.Enabled = false;
            dtVatDate.Enabled = false;
            txtEccNo.Enabled = false;
            dtEccDate.Enabled = false;
            txtPlaNo.Enabled = false;
            txtRange.Enabled = false;
            txtDivision.Enabled = false;
            txtCommiRate.Enabled = false;
            cboSupplierType.Enabled = false;
        }

        private void chkIsTDSApplicable_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIsTDSApplicable.Checked)
            {
                cboDeducteeType.Enabled = true;
            }
            else
            {
                cboDeducteeType.Enabled = false;
            }
        }

        private void chkLBTApplicable_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLBTApplicable.Checked)
            {
                CboLocalTaxDealerType.Enabled = true;
                txtLocalTaxRegNo.Enabled = true;
            }
            else
            {
                CboLocalTaxDealerType.Enabled = false;
                txtLocalTaxRegNo.Enabled = false;
            }
        }

        private void chkServiceTaxApplica_CheckedChanged(object sender, KeyEventArgs e)
        {
            if (chkServiceTaxApplica.Checked)
            {
                cboServiceTaxType.Enabled = true;
            }
            else
            {
                cboServiceTaxType.Enabled = false;
            }
        }

        private void cboTypeofDutyTax_SelectedValueChanged(object sender, EventArgs e)
        {
            switch (cboTypeofDutyTax.SelectedIndex)
            {
                case 0:
                    cboVATClass.Enabled = false;
                    cboTDSNatureofPymt.Enabled = true;
                    txtPercentofCalc.Enabled = false;
                    txtPercentofCalc.Text = "0.00";
                    cboFormType.Enabled = false;
                    lblFormType.Visible = false;
                    cboFormType.Visible = false;
                    lblTaxCategory.Visible = true;
                    cboTDSNatureofPymt.Visible = true;
                    break;

                case 1:
                    cboVATClass.Enabled = true;
                    cboTDSNatureofPymt.Enabled = false;
                    txtPercentofCalc.Enabled = false;
                    txtPercentofCalc.Text = "0.00";
                    cboFormType.Enabled = false;
                    lblFormType.Visible = false;
                    cboFormType.Visible = false;
                    lblTaxCategory.Visible = true;
                    cboTDSNatureofPymt.Visible = true;
                    cboVATClass.Focus();
                    break;

                case 2:
                    cboFormType.Visible = true;
                    cboFormType.Enabled = true;
                    cboVATClass.Enabled = false;
                    cboTDSNatureofPymt.Enabled = false;
                    txtPercentofCalc.Enabled = true;
                    lblFormType.Visible = true;
                    lblTaxCategory.Visible = false;
                    cboTDSNatureofPymt.Visible = false;
                    break;

                case 3:
                    cboTDSNatureofPymt.Enabled = false;
                    cboVATClass.Enabled = false;
                    txtPercentofCalc.Enabled = false;
                    cboVATClass.SelectedText = null;
                    txtPercentofCalc.Text = "0.00";
                    cboFormType.Enabled = false;
                    lblFormType.Visible = false;
                    cboFormType.Visible = false;
                    lblTaxCategory.Visible = true;
                    cboTDSNatureofPymt.Visible = true;
                    break;

                case 4:
                    cboVATClass.Enabled = false;
                    cboTDSNatureofPymt.Enabled = false;
                    txtPercentofCalc.Enabled = false;
                    cboTDSNatureofPymt.SelectedText = null;
                    cboFormType.Visible = false;
                    cboFormType.Enabled = false;
                    lblFormType.Visible = false;
                    lblTaxCategory.Visible = true;
                    cboTDSNatureofPymt.Visible = true;
                    break;
            }
        }

        private void chkTCSApplicable_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTCSApplicable.Checked)
            {
                cboDeducteeType.Enabled = true;
                ChkTDSDeductable.Enabled = false;
            }
            else
            {
                cboDeducteeType.Enabled = false;
                ChkTDSDeductable.Enabled = true;
            }
        }

        private void cboVATClass_SelectedValueChanged(object sender, EventArgs e)
        {
            string snglValue = DB.GetSnglValue(string.Format("Select MiscName From tbl_MiscMaster Where MiscId={0}", RuntimeHelpers.GetObjectValue(cboVATClass.SelectedValue)));
            decimal dper = 0;
            dper = Localization.ParseNativeDecimal(DB.GetSnglValue(string.Format("select substring('{0}', charindex('@','{0}')+1, (charindex('%','{0}')-charindex('@','{0}')-1))", snglValue)));
            txtPercentofCalc.Text = Conversions.ToString(dper);
        }

        private void ChkMainBillByBill_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkMainBillByBill.Checked)
            {
                txtDefaultCreditPeriod.Enabled = true;
                txtCreditLimit.Enabled = true;
            }
            else
            {
                txtDefaultCreditPeriod.Enabled = false;
                txtCreditLimit.Enabled = false;
            }
        }

        private void ChkMainBillByBill_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            { SendKeys.Send("{TAB}"); }
        }

        private void chkActiveIntestCalc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void ChkInventoryValuAffected_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void chkIsTDSApplicable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void chkServiceTaxApplica_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void chkTCSApplicable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void ChkTDSDeductable_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void chkUseAssessableValueCalc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void chkUsedInVATReturn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void chkLBTApplicable_keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void cboDrCr_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if ((Localization.ParseNativeDouble(txtOpeningBal.Text) > 0.0) && (cboDrCr.SelectedItem != null))
                {
                    if (ChkMainBillByBill.Checked)
                    {
                        if (strDr.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)) || strCr.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)))
                        {
                            pnlBillDtls.Visible = true;
                            pnlOpeningDtls.Visible = true;

                            txtTotalOp.Text = string.Format("{0:N2}", txtOpeningBal.Text.ToString());
                            txtOnAcc.Text = Conversions.ToString(Localization.ParseNativeDecimal(txtTotalOp.Text) - Localization.ParseNativeDecimal(txtAdjAmt.Text));
                            fgDtls.Focus();
                            if (cboDrCr.SelectedItem.ToString() == "Dr")
                                fgDtls.Rows[0].Cells[2].Value = 1;
                            else
                                fgDtls.Rows[0].Cells[2].Value = 2;
                        }
                    }
                }
                if (strBank.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)))
                {
                    txtBankBal.Text = string.Format("{0:N2}", txtOpBal2.Text.ToString());
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void btnBRSDone_Click(object sender, EventArgs e)
        {
            if (Localization.ParseNativeDecimal(lblBankDrAmt.Text) > 0)
            {
                if (Localization.ParseNativeDecimal(txtBankBal.Text) != Localization.ParseNativeDecimal(lblBankDrAmt.Text))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Balance as per Bank amount should not be matched with given Bank Balance Amt");
                    fgDtls_BRS.Focus();
                }
            }
            if (Localization.ParseNativeDecimal(lblBankCrAmt.Text) > 0)
            {
                if (Localization.ParseNativeDecimal(txtBankBal.Text) != Localization.ParseNativeDecimal(lblBankCrAmt.Text))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Balance as per Bank amount should not be matched with given Bank Balance Amt");
                    fgDtls_BRS.Focus();
                }
            }
            pnlBRSMain.Visible = false;
            cboPartyGroup.Focus();
        }

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                decimal dblAdjAmtDr = 0;
                decimal dblAdjAmtCr = 0;
                decimal dblAdjAmtTotal = 0;
                if ((base.blnFormAction == Enum_Define.ActionType.New_Record) || (base.blnFormAction == Enum_Define.ActionType.Edit_Record))
                {
                    if (e.ColumnIndex == 6)
                    {
                        DataGridViewEx fgDtls = this.fgDtls;
                        if (strDr.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)))
                        {
                            int iRowCnt = fgDtls.RowCount - 1;
                            for (int i = 0; i <= iRowCnt; i++)
                            {
                                DataGridViewRow row = fgDtls.Rows[i];
                                if (Operators.ConditionalCompareObjectGreater(row.Cells[6].Value, 0, false))
                                {
                                    if (Operators.ConditionalCompareObjectEqual(row.Cells[2].Value, 1, false))
                                    {
                                        dblAdjAmtDr = decimal.Add(dblAdjAmtDr, Localization.ParseNativeDecimal(Conversions.ToString(row.Cells[6].Value)));
                                    }
                                    else
                                    {
                                        dblAdjAmtCr = decimal.Add(dblAdjAmtCr, Localization.ParseNativeDecimal(Conversions.ToString(row.Cells[6].Value)));
                                    }
                                }
                                row = null;
                            }
                            dblAdjAmtTotal = decimal.Subtract(dblAdjAmtDr, dblAdjAmtCr);
                            txtTotalOp.Text = string.Format("{0:N2}", txtOpeningBal.Text.ToString());
                            txtAdjAmt.Text = Conversions.ToString(dblAdjAmtTotal);
                            txtOnAcc.Text = Conversions.ToString(decimal.Subtract(Localization.ParseNativeDecimal(this.txtTotalOp.Text), Localization.ParseNativeDecimal(this.txtAdjAmt.Text)));
                        }
                        if (this.strCr.Contains(Conversions.ToString(this.cboLedgerGroup.SelectedValue)))
                        {
                            int iRowCnt = fgDtls.RowCount - 1;
                            for (int j = 0; j <= iRowCnt; j++)
                            {
                                DataGridViewRow row2 = fgDtls.Rows[j];
                                if (Operators.ConditionalCompareObjectGreater(row2.Cells[6].Value, 0, false))
                                {
                                    if (Operators.ConditionalCompareObjectEqual(row2.Cells[2].Value, 2, false))
                                    {
                                        dblAdjAmtCr = decimal.Add(dblAdjAmtCr, Localization.ParseNativeDecimal(Conversions.ToString(row2.Cells[6].Value)));
                                    }
                                    else
                                    {
                                        dblAdjAmtDr = decimal.Add(dblAdjAmtDr, Localization.ParseNativeDecimal(Conversions.ToString(row2.Cells[6].Value)));
                                    }
                                }
                                row2 = null;
                            }
                            dblAdjAmtTotal = decimal.Subtract(dblAdjAmtCr, dblAdjAmtDr);
                            txtTotalOp.Text = string.Format("{0:N2}", txtOpeningBal.Text.ToString());
                            txtAdjAmt.Text = Conversions.ToString(dblAdjAmtTotal);
                            txtOnAcc.Text = Conversions.ToString(decimal.Subtract(Localization.ParseNativeDecimal(this.txtTotalOp.Text), Localization.ParseNativeDecimal(this.txtAdjAmt.Text)));
                        }
                        fgDtls = null;
                    }
                    if (Localization.ParseNativeDecimal(txtOnAcc.Text) < 0)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Adjust amount should not be greater than Opening Amt");
                        DataGridViewEx ex2 = fgDtls;
                        int iRowCnt = ex2.RowCount - 1;
                        //for (int k = 0; k <= iRowCnt; k++)
                        //{
                        //    ex2.CurrentRow.Cells[6].Value = 0;
                        //}
                        ex2 = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_BRS_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                decimal dblCurrAmtDr = 0;
                decimal dblCurrAmtCr = 0;
                if (cboDrCr.SelectedItem.ToString() == "Dr")
                {
                    lblCompDrBal.Text = txtOpBal2.Text.ToString();
                    lblCompCrBal.Text = "0.00";
                }
                else
                {
                    lblCompCrBal.Text = txtOpBal2.Text.ToString();
                    lblCompDrBal.Text = "0.00";
                }

                if (((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record)) && ((e.ColumnIndex == 10) | (e.ColumnIndex == 11)))
                {
                    DataGridViewEx ex = fgDtls_BRS;
                    if (strBank.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)))
                    {
                        int iRowCnt = ex.RowCount - 1;
                        for (int i = 0; i <= iRowCnt; i++)
                        {
                            DataGridViewRow row = ex.Rows[i];
                            if ((Localization.ParseNativeInt(row.Cells[10].Value.ToString()) > 0) || (Localization.ParseNativeInt(row.Cells[11].Value.ToString()) > 0))
                            {
                                if (Localization.ParseNativeInt(row.Cells[2].Value.ToString()) == 1)
                                    dblCurrAmtDr += Localization.ParseNativeDecimal(Conversions.ToString(row.Cells[10].Value));
                                else
                                    dblCurrAmtCr += Localization.ParseNativeDecimal(Conversions.ToString(row.Cells[11].Value));
                            }
                            lblCurrDrAmt.Text = dblCurrAmtDr.ToString();
                            lblCurrCrAmt.Text = dblCurrAmtCr.ToString();

                            if (Localization.ParseNativeInt(row.Cells[2].Value.ToString()) == 1)
                            {
                                row.Cells[11].ReadOnly = true;
                                row.Cells[11].Value = 0;
                            }
                            else
                            {
                                row.Cells[10].ReadOnly = true;
                                row.Cells[10].Value = 0;
                            }
                            row = null;
                        }
                    }

                    ex = null;
                    decimal BankBal = 0;
                    BankBal = (Localization.ParseNativeDecimal(lblCompDrBal.Text) - Localization.ParseNativeDecimal(lblCompCrBal.Text)) + (Localization.ParseNativeDecimal(lblCurrCrAmt.Text) - Localization.ParseNativeDecimal(lblCurrDrAmt.Text));
                    if (BankBal > 0)
                    {
                        lblBankDrAmt.Text = string.Format("{0:N2}", BankBal.ToString());
                        lblBankCrAmt.Text = "0.00";
                    }
                    else
                    {
                        lblBankCrAmt.Text = string.Format("{0:N2}", Math.Abs(BankBal).ToString());
                        lblBankDrAmt.Text = "0.00";
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void cboDrCrBank_LostFocus(object sender, EventArgs e)
        {
            try
            {
                if (strBank.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)) && (Localization.ParseNativeDecimal(txtBankBal.Text) != Localization.ParseNativeDecimal(txtOpBal2.Text)))
                {
                    pnlBRSMain.Visible = true;
                    pnlBRSDtls.Visible = true;
                    fgDtls_BRS.Focus();
                    if (cboDrCr.SelectedItem.ToString() == "Dr")
                    {
                        lblCompDrBal.Text = txtOpBal2.Text.ToString();
                        lblCompCrBal.Text = "0.00";
                    }
                    else
                    {
                        lblCompCrBal.Text = txtOpBal2.Text.ToString();
                        lblCompDrBal.Text = "0.00";
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void txtOpBal2_LostFocus(object sender, EventArgs e)
        {
            if (strBank.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)))
            {
                txtOpeningBal.Text = txtOpBal2.Text.ToString();
            }
        }

        private void cboDrCrOp_LostFocus(object sender, EventArgs e)
        {
            if (strBank.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)))
            {
                if (Operators.ConditionalCompareObjectEqual(cboDrCrOp.SelectedItem, "Dr", false))
                {
                    cboDrCr.SelectedValue = 1;
                }
                else
                {
                    cboDrCr.SelectedValue = 2;
                }
            }
        }

        private void cboLedgerType_LostFocus(object sender, EventArgs e)
        {
            if (strBank.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)))
            {
                tbLedgerDtls.SelectedTab = tbMailingDetails;
                txtAddress.Focus();
            }
        }

        private void btnLastYrBal_Click(object sender, EventArgs e)
        {
            PnlPastBalMain.Visible = true;
            PnlPastBalMain.Enabled = true;
        }

        private void btnPastBalDone_Click(object sender, EventArgs e)
        {
            PnlPastBalMain.Visible = false;
            PnlPastBalMain.Enabled = false;
        }

        private void fgDtls_PYB_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                txtPastBalTot.Text = string.Format("{0:N0}", CommonCls.GetColSum(fgDtls_PYB, 5, -1, -1));
            }
        }

        private void txtLedgerName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtLedgerName, txtAliasName, "tbl_LedgerMaster", "LedgerName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtLedgerName, txtAliasName, "tbl_LedgerMaster", "LedgerName");
        }

        public void PrintRecord()
        {
            CIS_ReportTool.frmMultiPrint frmMultiPrint = new CIS_ReportTool.frmMultiPrint();
            CIS_ReportTool.frmMultiPrint.MenuID = base.iIDentity;
            CIS_ReportTool.frmMultiPrint.Id = Localization.ParseNativeInt(txtCode.Text);
            CIS_ReportTool.frmMultiPrint.TblNm = "tbl_LedgerMaster";
            CIS_ReportTool.frmMultiPrint.IdStr = "LedgerID";
            CIS_ReportTool.frmMultiPrint.iCompID = Db_Detials.CompID;
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

        private void btnPnlBillDtlsClose_Click(object sender, EventArgs e)
        {
            pnlBillDtls.Visible = false;
            pnlBillDtls.Enabled = false;

        }

        private void btnSave_BillDtls_Click(object sender, EventArgs e)
        {
            btnPnlBillDtlsClose_Click(null, null);
        }

        private void btnClose_BillDtls_Click(object sender, EventArgs e)
        {
            btnPnlBillDtlsClose_Click(null, null);
        }

        private void UpdCompandYearID_fgDtls(object sender, EventArgs e)
        {
            if (ChkMainBillByBill.Checked)
            {
                for (int i = 0; i <= (fgDtls.Rows.Count - 1); i++)
                {
                    if (fgDtls.Rows[i].Cells[3].Value != null && fgDtls.Rows[i].Cells[3].Value.ToString() != "" && fgDtls.Rows[i].Cells[3].Value.ToString() != "-")
                    {
                        fgDtls.Rows[i].Cells[8].Value = Db_Detials.CompID.ToString();
                        fgDtls.Rows[i].Cells[9].Value = Db_Detials.YearID.ToString();
                    }
                }
            }
        }

        #region Fill Grid
        private void FillGrid_Manual(DataGridViewEx piGrid, int pGridUID, string ptbl, string piFld, string pID, DataTable dt_HasDtls_Grd, int iTransType = 0, string sIsValidate = "", string sCompID = "", string sYearID = "", int iStockTable = 0)
        {
            try
            {
                if (pID != "")
                {
                    piGrid.Rows.Clear();
                    string str = string.Empty;
                    if ((piFld.ToString().Length != 0) & (pID.ToString().Length != 0))
                    {
                        str = string.Format("Where {0} = {1} and CompID = {2} and YearID = {3}", piFld, pID, sCompID, sYearID);
                    }
                    int num = Conversions.ToInteger(DB.GetSnglValue("select Count(0) from sysobjects Where xtype = 'P' And [Name] = '" + ptbl + "'"));
                    string sql = string.Empty;
                    if (num == 0)
                    {
                        sql = string.Format(" sp_ExecQuery 'Select * From {0} {1} '", ptbl, str);
                    }
                    else if (!string.IsNullOrEmpty(piFld))
                    {
                        sql = string.Format(" sp_ExecQuery '{0}' ", ptbl + piFld);
                    }
                    else
                    {
                        sql = string.Format(" sp_ExecQuery '{0}' ", ptbl);
                    }

                    if (ptbl != null)
                    {
                        using (DataTable table = DB.GetDT(sql, false))
                        {
                            if (table.Rows.Count > 0)
                            {
                                int num6 = table.Rows.Count - 1;
                                for (int i = 0; i <= num6; i++)
                                {
                                    DataGridViewEx ex = piGrid;
                                    ex.Rows.Add();
                                    ex.Rows[ex.RowCount - 1].Cells[0].Value = ex.Rows.Count - 1;
                                    DataRow[] rowArray = dt_HasDtls_Grd.Select("SubGridID = " + Conversions.ToString(pGridUID));
                                    int num7 = rowArray.Length - 1;
                                    for (int j = 0; j <= num7; j++)
                                    {
                                        string str4 = rowArray[j]["ColDataType"].ToString();
                                        string str5 = rowArray[j]["ColFields"].ToString();
                                        string str3 = rowArray[j]["ColDataFormat"].ToString();
                                        if (str5 != "-")
                                        {
                                            switch (str4)
                                            {
                                                case "T":
                                                case "X":
                                                    {
                                                        ex.Rows[ex.RowCount - 1].Cells[j].Value = table.Rows[i][str5].ToString();
                                                        continue;
                                                    }

                                                case "B":
                                                    {
                                                        DataGridViewCheckBoxCell cell = (DataGridViewCheckBoxCell)ex.Rows[ex.RowCount - 1].Cells[j];
                                                        cell.Value = Localization.ParseBoolean(table.Rows[i][str5].ToString());
                                                        continue;
                                                    }

                                                case "C":
                                                    {
                                                        if (Versioned.IsNumeric(table.Rows[i][str5].ToString()))
                                                        {
                                                            ex.Rows[ex.RowCount - 1].Cells[j].Value = Localization.ParseNativeInt(table.Rows[i][str5].ToString());
                                                        }
                                                        else
                                                        {
                                                            ex.Rows[ex.RowCount - 1].Cells[j].Value = table.Rows[i][str5].ToString();
                                                        }
                                                        continue;
                                                    }

                                                case "S":
                                                    {
                                                        if (table.Rows[i][str5].ToString() != "")
                                                        {
                                                            string str6 = Localization.ToVBDateString(table.Rows[i][str5].ToString());
                                                            ex.Rows[ex.RowCount - 1].Cells[j].Value = str6;
                                                        }
                                                        continue;
                                                    }

                                                case "Z":
                                                    {
                                                        if (table.Rows[i][str5].ToString() != "")
                                                        {
                                                            string str6 = table.Rows[i][str5].ToString();
                                                            string[] dtArray = str6.Split(' ');
                                                            ex.Rows[ex.RowCount - 1].Cells[j].Value = dtArray[1].ToString() + " " + dtArray[2].ToString();
                                                        }
                                                        continue;
                                                    }
                                            }

                                            if (Versioned.IsNumeric(table.Rows[i][str5].ToString()))
                                            {
                                                double num5 = Convert.ToDouble(Localization.ParseNativeDecimal(table.Rows[i][str5].ToString()));
                                                num5 = Conversions.ToDouble(string.Format("{0:N" + str3 + "}", num5));
                                                ex.Rows[ex.RowCount - 1].Cells[j].Value = num5;
                                            }
                                            else
                                            {
                                                ex.Rows[ex.RowCount - 1].Cells[j].Value = table.Rows[i][str5].ToString();
                                            }
                                        }
                                    }
                                    ex = null;
                                }
                            }
                        }
                    }

                    if (piGrid.Rows.Count == 0)
                    {
                        if (piGrid.ColumnCount > 0)
                            piGrid.Rows.Add();
                    }
                }
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                Exception exception = exception1;
                Navigate.logError(exception.Message, exception.StackTrace);
                ProjectData.ClearProjectError();
            }
        }
        #endregion

        private void txtOpeningBal_Validated(object sender, EventArgs e)
        {
            decimal dblAdjAmtDr = 0;
            decimal dblAdjAmtCr = 0;
            decimal dblAdjAmtTotal = 0;
            if (base.blnFormAction == Enum_Define.ActionType.New_Record || base.blnFormAction == Enum_Define.ActionType.Edit_Record)
            {
                DataGridViewEx fgDtls = this.fgDtls;
                if (strDr.Contains(Conversions.ToString(cboLedgerGroup.SelectedValue)))
                {
                    int iRowCnt = fgDtls.RowCount - 1;
                    for (int i = 0; i <= iRowCnt; i++)
                    {
                        DataGridViewRow row = fgDtls.Rows[i];
                        if (Operators.ConditionalCompareObjectGreater(row.Cells[6].Value, 0, false))
                        {
                            if (Operators.ConditionalCompareObjectEqual(row.Cells[2].Value, 1, false))
                            {
                                dblAdjAmtDr = decimal.Add(dblAdjAmtDr, Localization.ParseNativeDecimal(Conversions.ToString(row.Cells[6].Value)));
                            }
                            else
                            {
                                dblAdjAmtCr = decimal.Add(dblAdjAmtCr, Localization.ParseNativeDecimal(Conversions.ToString(row.Cells[6].Value)));
                            }
                        }
                        row = null;
                    }
                    dblAdjAmtTotal = decimal.Subtract(dblAdjAmtDr, dblAdjAmtCr);
                    txtTotalOp.Text = string.Format("{0:N2}", txtOpeningBal.Text.ToString());
                    txtAdjAmt.Text = Conversions.ToString(dblAdjAmtTotal);
                    txtOnAcc.Text = Conversions.ToString(decimal.Subtract(Localization.ParseNativeDecimal(this.txtTotalOp.Text), Localization.ParseNativeDecimal(this.txtAdjAmt.Text)));
                }
                if (this.strCr.Contains(Conversions.ToString(this.cboLedgerGroup.SelectedValue)))
                {
                    int iRowCnt = fgDtls.RowCount - 1;
                    for (int j = 0; j <= iRowCnt; j++)
                    {
                        DataGridViewRow row2 = fgDtls.Rows[j];
                        if (Operators.ConditionalCompareObjectGreater(row2.Cells[6].Value, 0, false))
                        {
                            if (Operators.ConditionalCompareObjectEqual(row2.Cells[2].Value, 2, false))
                            {
                                dblAdjAmtCr = decimal.Add(dblAdjAmtCr, Localization.ParseNativeDecimal(Conversions.ToString(row2.Cells[6].Value)));
                            }
                            else
                            {
                                dblAdjAmtDr = decimal.Add(dblAdjAmtDr, Localization.ParseNativeDecimal(Conversions.ToString(row2.Cells[6].Value)));
                            }
                        }
                        row2 = null;
                    }
                    dblAdjAmtTotal = decimal.Subtract(dblAdjAmtCr, dblAdjAmtDr);
                    txtTotalOp.Text = string.Format("{0:N2}", txtOpeningBal.Text.ToString());
                    txtAdjAmt.Text = Conversions.ToString(dblAdjAmtTotal);
                    txtOnAcc.Text = Conversions.ToString(decimal.Subtract(Localization.ParseNativeDecimal(this.txtTotalOp.Text), Localization.ParseNativeDecimal(this.txtAdjAmt.Text)));
                }
                fgDtls = null;
            }
            if (Localization.ParseNativeDecimal(txtOnAcc.Text) < 0)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Adjust amount should not be greater than Opening Amt");
                DataGridViewEx ex2 = fgDtls;
                int iRowCnt = ex2.RowCount - 1;
                //for (int k = 0; k <= iRowCnt; k++)
                //{
                //    ex2.CurrentRow.Cells[6].Value = 0;
                //}
                ex2 = null;
            }
        }

        private void txtLedgerName_TextChanged(object sender, EventArgs e)
        {
            if (txtLedgerName.Text != null && txtLedgerName.Text != "-")
            {
                txtPrintingName.Text = txtLedgerName.Text.ToUpper();
            }
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


