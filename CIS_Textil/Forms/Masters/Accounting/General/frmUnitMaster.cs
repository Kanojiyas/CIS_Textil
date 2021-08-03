using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using System.Data;
using Microsoft.VisualBasic;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmUnitMaster : frmMasterIface
    {
        public frmUnitMaster()
        {
            InitializeComponent();
        }

        #region Form Events

        private void frmUnitMaster_Load(object sender, EventArgs e)
        {
            try
            {
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtFormalName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtFormalName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtFormalName.Focus();
                        this.isComboAddText = true;
                        this.isSecondMessage = true;
                    }
                }
                if (base.blnFormAction == Enum_Define.ActionType.View_Record)
                {
                    FillControls();
                }
                InserDefValues();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
        #endregion

        #region Form Navigation

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
                DBValue.Return_DBValue(this, txtCode, "UnitID", 0);
                DBValue.Return_DBValue(this, txtFormalName, "UnitName", 0);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtSymbol, "Symbol", 0);
                DBValue.Return_DBValue(this, txtDecimal, "Decimal", 0);
                DBValue.Return_DBValue(this, txtRateCalcType, "RateCalcType", 0);
                DBValue.Return_DBValue(this, txtOrderCalcType, "OrderValidationType", 0);
                DBValue.Return_DBValue(this, ChkDefault, "SetDefault", 0);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", 0);
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
                sComboAddText = txtFormalName.Text;
                ArrayList pArrayData = new ArrayList
                {
                    txtFormalName.Text,
                    txtAliasName.Text==""?null:txtAliasName.Text,
                    txtSymbol.Text,
                    txtDecimal.Text,
                    'U',
                    txtRateCalcType.Text,
                    txtOrderCalcType.Text,
                    ChkDefault.Checked ? 1 : 0,
                    ChkActive.Checked ? 1 : 0,
                    (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                    (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                    (txtET1.Text.Trim()),
                    (txtET2.Text.Trim()),
                    (txtET3.Text.Trim())
                };
                DBSp.Master_AddEdit(pArrayData, "");
                this.IsMasterAdded = true;
            }
            catch (Exception exception1)
            {
                this.IsMasterAdded = false;
                Navigate.logError(exception1.Message, exception1.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                CommonCls.IncFieldID(this, ref txtCode, "");
                ApplyActStatus();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                string strTblName;
                strTblName = "tbl_UnitsMaster";

                if (txtFormalName.Text.Trim() == "" || txtFormalName.Text.Trim() == "-" || txtFormalName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Formal Name");
                    txtFormalName.Focus();
                    return true;
                }
                if (txtSymbol.Text.Trim() == "" || txtSymbol.Text.Trim() == "-" || txtSymbol.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Symbol");
                    txtSymbol.Focus();
                    return true;
                }
                if (txtDecimal.Text.Trim() == "" || txtDecimal.Text.Trim() == "-")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Decimal Places");
                    txtDecimal.Focus();
                    return true;
                }
                if (DBSp.rtnAction())
                {
                    if (Navigate.CheckDuplicate(ref strTblName, "UnitName", txtFormalName.Text, false, "", 0, "", "This UnitName is already available"))
                    {
                        txtFormalName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtAliasName.Text, false, "", 0, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtFormalName.Text, false, "", 0, "", "This UnitName is already Used in AliasName"))
                    {
                        txtFormalName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref strTblName, "UnitName", txtAliasName.Text, false, "", 0, "", "This AliasName is already Used in UnitName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                    if (ChkDefault.Checked == true)
                    {
                        bool SetDefault = Localization.ParseBoolean(DB.GetSnglValue("Select SetDefault From " + strTblName + " Where SetDefault = 1 "));
                        if (SetDefault)
                        {
                            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Default Unit Already Activated");
                            ChkDefault.Focus();
                            return true;
                        }
                    }
                }
                else
                {
                    if (Navigate.CheckDuplicate(ref strTblName, "UnitName", txtFormalName.Text.Trim(), true, "UnitID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This UnitName is already available"))
                    {
                        txtFormalName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtAliasName.Text.Trim(), true, "UnitID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "AliasName", txtFormalName.Text.Trim(), true, "UnitID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This UnitName is already Used in AliasName"))
                    {
                        txtFormalName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref strTblName, "UnitName", txtAliasName.Text.Trim(), true, "UnitID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", "This AliasName is already Used in UnitName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(txtRateCalcType.Text, "^[a-zA-Z]"))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Valid Characters");
                    txtRateCalcType.Focus();
                    return true;
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtOrderCalcType.Text, "^[a-zA-Z]"))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Valid Characters");
                    txtOrderCalcType.Focus();
                    return true;
                }
                if (txtRateCalcType.Text.Length <= 0 || txtRateCalcType.Text == "-" || txtOrderCalcType.Text.Length <= 0 || txtOrderCalcType.Text == "-")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Valid Formula Fields");
                    txtRateCalcType.Focus();
                    return true;
                }
                if (txtRateCalcType.Text != "P" && txtRateCalcType.Text != "M" && txtRateCalcType.Text != "W" && txtRateCalcType.Text != "B" || txtRateCalcType.Text.Length > 1)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Valid Formula Fields");
                    txtRateCalcType.Focus();
                    return true;
                }
                if (txtOrderCalcType.Text != "P" && txtOrderCalcType.Text != "M" && txtOrderCalcType.Text != "W" && txtOrderCalcType.Text != "B" || txtOrderCalcType.Text.Length > 1)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Valid Formula Fields");
                    txtOrderCalcType.Focus();
                    return true;
                }

                if (ChkDefault.Checked == true)
                {
                    bool SetDefault = Localization.ParseBoolean(DB.GetSnglValue("Select SetDefault From " + strTblName + " Where SetDefault = 1 And UnitID <> " + Localization.ParseNativeLong(txtCode.Text)));
                    if (SetDefault)
                    {
                        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Default Unit Already Activated");
                        ChkDefault.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtFormalName, txtAliasName, "tbl_UnitsMaster", "UnitName"))
                {
                    return true;
                }
                if (CommonCls.ValidateShortCode(this, txtFormalName, txtAliasName, "tbl_UnitsMaster", "UnitName"))
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

        private void txtFormalName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtFormalName, txtAliasName, "tbl_UnitsMaster", "UnitName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtFormalName, txtAliasName, "tbl_UnitsMaster", "UnitName");
        }

        private void InserDefValues()
        {
            try
            {
                string strQry = "";
                using (DataTable Dt = DB.GetDT("Select * from tbl_Unitsmaster where IsDeleted=0", false))
                {
                    DataRow[] rst_Meters = Dt.Select("UnitName='Meters'");
                    if (rst_Meters.Length == 0)
                    {
                        strQry += string.Format("INSERT INTO tbl_Unitsmaster VALUES('{0}','{1}', '{2}', {3}, '{4}', '{5}', '{6}', {7}, {8}, {9}, {10}, GETDATE(),{11}, 0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                            "Meters", "Mtrs", "Mtrs", "2", "S", "M", "M", 0, 1, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
                    }

                    DataRow[] rst_Pieces = Dt.Select("UnitName='Pieces'");
                    if (rst_Pieces.Length == 0)
                    {
                        strQry += string.Format("INSERT INTO tbl_Unitsmaster VALUES('{0}','{1}', '{2}', {3}, '{4}', '{5}', '{6}', {7}, {8}, {9}, {10}, GETDATE(),{11}, 0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                            "Pieces", "Pcs", "Pcs", "0", "S", "P", "P", 0, 1, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
                    }

                    DataRow[] rst_Set = Dt.Select("UnitName='Set'");
                    if (rst_Set.Length == 0)
                    {
                        strQry += string.Format("INSERT INTO tbl_Unitsmaster VALUES('{0}','{1}', '{2}', {3}, '{4}', '{5}', '{6}', {7},{8},{9},{10}, GETDATE(),{11}, 0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                            "Set", "St", "Set", "0", "S", "M", "M", 0, 1, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
                    }

                    DataRow[] rst_Pair = Dt.Select("UnitName='Pair'");
                    if (rst_Pair.Length == 0)
                    {
                        strQry += string.Format("INSERT INTO tbl_Unitsmaster VALUES('{0}','{1}', '{2}', {3}, '{4}', '{5}', '{6}', {7},{8},{9},{10}, GETDATE(),{11}, 0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                            "Pair", "Pr", "Pair", "0", "S", "M", "M", 0, 1, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
                    }

                    DataRow[] rst_KiloGrams = Dt.Select("UnitName='Kilograms'");
                    if (rst_KiloGrams.Length == 0)
                    {
                        strQry += string.Format("INSERT INTO tbl_Unitsmaster VALUES('{0}','{1}', '{2}', {3}, '{4}', '{5}', '{6}', {7},{8},{9},{10}, GETDATE(),{11}, 0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                            "KiloGrams", "Kgs", "Kgs", "3", "S", "W", "W", 0, 1, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
                    }
                    DataRow[] rst_Yards = Dt.Select("UnitName='Yards'");
                    if (rst_Yards.Length == 0)
                    {
                        strQry += string.Format("INSERT INTO tbl_Unitsmaster VALUES('{0}','{1}', '{2}', {3}, '{4}', '{5}', '{6}', {7},{8},{9},{10}, GETDATE(),{11}, 0,null,null,0,null,null,0,null,null,0,null,null,0,null,null);",
                            "Yards", "Yrds", "Yrds", "2", "S", "M", "M", 0, 1, Db_Detials.CompID, Db_Detials.YearID, Db_Detials.UserID);
                    }
                }
                if (strQry.Length > 0)
                {
                    DB.ExecuteSQL(strQry);
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }
    }
}
