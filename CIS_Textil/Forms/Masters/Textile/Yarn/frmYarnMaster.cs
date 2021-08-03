using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;

namespace CIS_Textil
{
    public partial class frmYarnMaster : frmMasterIface
    {
        public bool isWithConsumption;
        [AccessedThroughProperty("fgDtls")]
        private CIS_DataGridViewEx.DataGridViewEx _fgDtls;

        public frmYarnMaster()
        {
            fgDtls = new DataGridViewEx();
            InitializeComponent();
        }

        #region Form Events
        private void frmYarnMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref CboYarnType, Combobox_Setup.ComboType.Mst_YarnType, "");
                Combobox_Setup.FillCbo(ref CboShade, Combobox_Setup.ComboType.Mst_YarnShade, "");
                Combobox_Setup.FillCbo(ref cboColor, Combobox_Setup.ComboType.Mst_YarnColor, "");

                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtYarnName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtYarnName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtYarnName.Focus();
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

        #region Form Navigation

        public void MovetoField()
        {
            try
            {
                if (this.isWithConsumption)
                {
                    EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                }
                txtYarnName.Focus();
                ApplyActStatus();
                BindComp();
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
                DBValue.Return_DBValue(this, txtCode, "YarnID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtYarnName, "YarnName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, CboYarnType, "YarnTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboColor, "ColorID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, CboShade, "ShadeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtlDeniar, "Denier", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCount, "Count", Enum_Define.ValidationType.Text);
                Control ChkActive = this.ChkActive;
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                #region Fill Multiple Company
                BindComp();
                string str = DB.GetSnglValue("select IntCompID from fn_YarnMaster_Tbl() Where YarnID=" + txtCode.Text + "");
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

                if (this.isWithConsumption)
                {
                    DetailGrid_Setup.FillGrid(fgDtls, fgDtls.Grid_UID, fgDtls.Grid_Tbl, "YarnId", Conversions.ToString(Localization.ParseNativeDouble(this.txtCode.Text)), base.dt_HasDtls_Grd);

                    if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                    {
                        EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false);
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
                sComboAddText = txtYarnName.Text;
                this.sGridmasterAddText = txtYarnName.Text.Trim();
                ArrayList pArrayData = new ArrayList
                {
                    (txtYarnName.Text.Trim()),
                    (txtAliasName.Text.Trim()),
                    (CboYarnType.SelectedValue),
                    (cboColor.SelectedValue),
                    (CboShade.SelectedValue),
                    (Localization.ParseNativeDecimal(txtlDeniar.Text.Trim())),
                    (Localization.ParseNativeDecimal(txtCount.Text.Trim())),
                    (ChkActive.Checked ? 1 : 0),
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


                if (!this.isWithConsumption)
                {
                    DBSp.Master_AddEdit(pArrayData, "");
                }
                else
                {
                    DBSp.Transcation_AddEdit(pArrayData, fgDtls, true);
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
            try
            {
                string strTableName = Db_Detials.fn_YarnMaster_Tbl;
                if (txtYarnName.Text.Trim() == "" || txtYarnName.Text.Trim() == "-" || txtYarnName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Yarn Name");
                    txtYarnName.Focus();
                    return true;
                }
                if (CboYarnType.SelectedValue == null || CboYarnType.SelectedValue.ToString() == "-" || CboYarnType.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Yarn Type");
                    CboYarnType.Focus();
                    return true;
                }
                if (!CboYarnType.IsValidSelect)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Valid Yarn Type");
                    this.CboYarnType.Focus();
                    return true;
                }

                if (txtlDeniar.Text.Trim() == "" || txtlDeniar.Text.Trim() == "-" || txtlDeniar.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Deniar");
                    txtlDeniar.Focus();
                    return true;
                }

                if (txtCount.Text.Trim() == "" || txtCount.Text.Trim() == "-" || txtCount.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Count");
                    txtCount.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    if (Navigate.CheckDuplicate(ref strTableName, "YarnName", this.txtYarnName.Text.Trim(), false, "", 0L, "", "This YarnName is already available"))
                    {
                        txtYarnName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", this.txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", this.txtYarnName.Text.Trim(), false, "", 0L, "", "This YarnName is already Used in AliasName"))
                    {
                        txtYarnName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "YarnName", this.txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in YarnName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    if (Navigate.CheckDuplicate(ref strTableName, "YarnName", this.txtYarnName.Text.Trim(), true, "YarnID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This YarnName is already available"))
                    {
                        txtYarnName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", this.txtAliasName.Text.Trim(), true, "YarnID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "AliasName", this.txtYarnName.Text.Trim(), true, "YarnID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This YarnName is already Used in AliasName"))
                    {
                        txtYarnName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTableName, "YarnName", this.txtAliasName.Text.Trim(), true, "YarnID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This AliasName is already Used in YarnName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtYarnName, txtAliasName, "tbl_YarnMaster", "YarnName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtYarnName, txtAliasName, "tbl_YarnMaster", "YarnName"))
                    return true;
                return false;
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                return false;
            }
        }

        #endregion

        private void txtYarnName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtYarnName, txtAliasName, "tbl_YarnMaster", "YarnName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtYarnName, txtAliasName, "tbl_YarnMaster", "YarnName");
        }

        private void BindComp()
        {
            try
            {
                DataTable dt = DB.GetDT(string.Format("Select CompanyID,CompanyName From fn_CompanyMaster_Tbl() order by CompanyName asc"), false);
                ((ListBox)chkCompany).DataSource = null; ((ListBox)chkCompany).DataSource = dt;
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
