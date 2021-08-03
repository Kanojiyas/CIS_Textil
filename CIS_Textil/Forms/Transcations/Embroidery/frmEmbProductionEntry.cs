using System;
using System.Collections;
using CIS_Bussiness;
using CIS_CLibrary;
using CIS_DBLayer;
using Microsoft.VisualBasic;

namespace CIS_Textil
{
    public partial class frmEmbProductionEntry : frmTrnsIface
    {
        private int MaxId;
        public frmEmbProductionEntry()
        {
            InitializeComponent();
        }

        #region Event
        private void frmEmbProductionEntry_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboMachineNo, Combobox_Setup.ComboType.Mst_MachineName, "");
                Combobox_Setup.FillCbo(ref cboShift, Combobox_Setup.ComboType.Mst_EmbShift, "");
                Combobox_Setup.FillCbo(ref cboOperatorName, Combobox_Setup.ComboType.mst_Employee, "");
                txtPastReading.Enabled = false;
                txtPresentReading.Enabled = false;

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
                DBValue.Return_DBValue(this, txtCode, "ProductionID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtEntryNo, "EntryNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEntryDate, "EntryDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, dtProductionDate, "ProductionDate", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, cboShift, "ShiftID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboMachineNo, "MachineID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPastReading, "PastReading", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPresentReading, "PresentReading", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtTotalStitches, "TotalStitches", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtNoOfFrames, "NoOfFrames", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboOperatorName, "EmployeeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtRemark, "Description", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, dtEd1, "ED1", Enum_Define.ValidationType.IsDate);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);

                MaxId = (int)Math.Round(Localization.ParseNativeDouble(DB.GetSnglValue(string.Format(" Select Isnull(Max(ProductionID),0) From {0}  Where StoreID={1} and CompID = {2} and BranchID={3} and YearID = {4}", "tbl_EmbProductionMain", Db_Detials.StoreID, Db_Detials.CompID, Db_Detials.BranchID, Db_Detials.YearID))));

                if (base.blnFormAction == Enum_Define.ActionType.Edit_Record)
                {
                    dtProductionDate.Focus();
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public void MovetoField()
        {
            try
            {
                txtPastReading.Enabled = false;
                txtPresentReading.Enabled = false;
                txtCode.Text = "";
                dtProductionDate.Focus();
                CommonCls.IncFieldID(this, ref txtEntryNo, "EntryNo");
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
                ArrayList pArrayData = new ArrayList
                {
                    this.frmVoucherTypeID,
                    txtEntryNo.Text,
                    dtEntryDate.TextFormat(false,true),
                    dtProductionDate.TextFormat(false,true),
                    cboShift.SelectedValue,
                    cboMachineNo.SelectedValue,
                    txtPastReading.Text,
                    txtPresentReading.Text,
                    txtTotalStitches.Text,
                    txtNoOfFrames.Text,
                    cboOperatorName.SelectedValue,
                    txtRemark.Text,
                    cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue,
                    cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue,
                    dtEd1.TextFormat(false,true), 
                    txtET1.Text,
                    txtET2.Text,
                    txtET3.Text
                };
                DBSp.Master_AddEdit(pArrayData, "");
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                string strTbl = "";
                if (txtEntryNo.Text.Trim() == "" || txtEntryNo.Text.Trim() == "-" || txtEntryNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry No.");
                    txtEntryNo.Focus();
                    return true;
                }
                if (!Information.IsDate(dtEntryDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Entry Date");
                    dtEntryDate.Focus();
                    return true;
                }
                if (!Information.IsDate(dtProductionDate.Text.ToString()))
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Production Date");
                    dtProductionDate.Focus();
                    return true;
                }

                if (cboShift.SelectedValue == null || cboShift.Text.Trim().ToString() == "-" || cboShift.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Shift");
                    cboShift.Focus();
                    return true;
                }

                if (cboMachineNo.SelectedValue == null || cboMachineNo.Text.Trim().ToString() == "-" || cboMachineNo.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select MachineNo");
                    cboMachineNo.Focus();
                    return true;
                }

                if (cboOperatorName.SelectedValue == null || cboOperatorName.Text.Trim().ToString() == "-" || cboOperatorName.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select OperatorName");
                    cboOperatorName.Focus();
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

        private void txtTotalStitches_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                txtPresentReading.Text = (Localization.ParseNativeInt(txtTotalStitches.Text) + Localization.ParseNativeInt(txtPastReading.Text)).ToString();
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
        }

        private void cboMachineNo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            try
            {
                txtPastReading.Text = Localization.ParseNativeDouble(DB.GetSnglValue(string.Format("Select Top 1 IsNull(PresentReading,0) From tbl_EmbProductionMain Where IsDeleted=0 and StoreID=" + Db_Detials.StoreID + " and CompID=" + Db_Detials.CompID + " and BranchID=" + Db_Detials.BranchID + " and MachineID=" + cboMachineNo.SelectedValue + " Order by ProductionID Desc"))).ToString();
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
        }
    }
}
