using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using Microsoft.VisualBasic;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmCatalogMaster : frmMasterIface
    {
        private DataGridViewEx fgDtls;
        private DataGridViewEx fgDtls_footer;

        public ArrayList OrgInGridArray;

        public frmCatalogMaster()
        {
            InitializeComponent();
            fgDtls = new DataGridViewEx();
            OrgInGridArray = new ArrayList();
        }

        private void frmCatalogMaster_Load(object sender, EventArgs e)
        {
            try
            {
                fgDtls = GrdMain.fgDtls;
                fgDtls_footer = GrdMain.fgDtls_f;

                DetailGrid_Setup.CreateDtlGrid_footer(this, fgDtls, fgDtls_footer, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, true);

                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtCatalogName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtCatalogName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtCatalogName.Focus();
                        this.isComboAddText = true;
                        this.isSecondMessage = true;
                    }
                }

                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    FillControls();
                }
                this.fgDtls.RowsAdded += new DataGridViewRowsAddedEventHandler(this.fgDtls_RowsAdded);
                this.fgDtls.CellValueChanged += new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
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
                DBValue.Return_DBValue(this, txtCode, "CatalogID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCatalogName, "CatalogName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtNoOfDesign, "NoOfDesign", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMtrs, "Mtrs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMinPcs, "MinPcs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMinMtrs, "MinMtrs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMaxMtrs, "MaxMtrs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtMaxPcs, "MaxPcs", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                #region Fill Multiple Company
                BindComp();
                string str = DB.GetSnglValue("select IntCompID from fn_CatalogMaster_Tbl() Where CatalogID=" + txtCode.Text + "");
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

                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "CatalogID", txtCode.Text, base.dt_HasDtls_Grd);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
            ApplyActStatus();
        }

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                ApplyActStatus();
                BindComp();
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
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
                sComboAddText = txtCatalogName.Text;

                ArrayList pArrayData = new ArrayList
                {
                    txtCatalogName.Text.Trim(),
                    txtAliasName.Text.Trim(),
                    txtNoOfDesign.Text.Trim(),
                    Localization.ParseNativeDecimal(txtMtrs.Text.Trim()),
                    (ChkActive.Checked ? 1 : 0),
                    Localization.ParseNativeDecimal(txtMinPcs.Text),
                    Localization.ParseNativeDecimal(txtMinMtrs.Text),
                    Localization.ParseNativeDecimal(txtMaxPcs.Text),
                    Localization.ParseNativeDecimal(txtMaxMtrs.Text),
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
                Db_Detials.IntCompID = strCheckedValue;
                #endregion

                DBSp.Transcation_AddEdit(pArrayData, this.fgDtls, true, "");
                this.IsMasterAdded = true;
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                this.IsMasterAdded = false;
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", exception1.Message);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                string strTable;
                if (txtCatalogName.Text.Trim() == "" || txtCatalogName.Text.Trim() == "-" || txtCatalogName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Book Name");
                    txtCatalogName.Focus();
                    return true;
                }
                if (DBSp.rtnAction())
                {
                    strTable = "tbl_CatalogMaster";
                    if (Navigate.CheckDuplicate(ref strTable, "CatalogName", txtCatalogName.Text.Trim(), false, "", 0, "", "This CatalogName is already available"))
                    {
                        txtCatalogName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "AliasName", txtAliasName.Text.Trim(), false, "", 0, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "AliasName", txtCatalogName.Text.Trim(), false, "", 0, "", "This CatalogName is already Used in AliasName"))
                    {
                        txtCatalogName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "CatalogName", txtAliasName.Text.Trim(), false, "", 0, "", "This AliasName is already Used in CatalogName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    strTable = "tbl_CatalogMaster";
                    if (Navigate.CheckDuplicate(ref strTable, "CatalogName", txtCatalogName.Text.Trim(), true, "CatalogID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This CatalogName is already available"))
                    {
                        txtCatalogName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "AliasName", txtAliasName.Text.Trim(), true, "CatalogID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "AliasName", txtCatalogName.Text.Trim(), true, "CatalogID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This CatalogName is already Used in AliasName"))
                    {
                        txtCatalogName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTable, "CatalogName", txtAliasName.Text.Trim(), true, "CatalogID", (long)Math.Round(Conversion.Val(txtCode.Text.Trim())), "", "This AliasName is already Used in CatalogName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtCatalogName, txtAliasName, "tbl_CatalogMaster", "CatalogName"))
                {
                    return true;
                }

                if (CommonCls.ValidateShortCode(this, txtCatalogName, txtAliasName, "tbl_CatalogMaster", "CatalogName"))
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

        #endregion

        private void txtBook_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtCatalogName, txtAliasName, "tbl_CatalogMaster", "CatalogName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtCatalogName, txtAliasName, "tbl_CatalogMaster", "CatalogName");
        }

        private void CalcVal()
        {
            try
            {
                txtMtrs.Text = string.Format("{0:N2}", CommonCls.GetColSum(this.fgDtls, 3, -1, -1));

                DataTable Dt = new DataTable();
                DataColumn dc = new DataColumn("DesignID", typeof(System.Int32));
                Dt.Columns.Add(dc);
                for (int i = 0; i <= fgDtls.Rows.Count - 1; i++)
                {
                    DataRow r = Dt.NewRow();
                    r["DesignID"] = fgDtls.Rows[i].Cells[2].Value.ToString();
                    Dt.Rows.Add(r);

                }

                DataView view = new DataView(Dt);
                DataTable distinctValues = view.ToTable(true, "DesignID");
                txtNoOfDesign.Text = distinctValues.Rows.Count.ToString();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }

        }

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    CalcVal();
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        private void fgDtls_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try { fgDtls.Rows[e.RowIndex].Cells[0].Value = e.RowIndex + 1; }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
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


