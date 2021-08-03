using System;
using CIS_Bussiness;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmFabricQualityMerging : frmMasterIface
    {
        public frmFabricQualityMerging()
        {
            InitializeComponent();
        }

        private void frmQualityMerging_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref CboQualityFrom, Combobox_Setup.ComboType.Mst_FabricQuality, "");
                Combobox_Setup.FillCbo(ref CboQualityTo, Combobox_Setup.ComboType.Mst_FabricQuality, "");
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
                string strchkvalue = string.Empty;
                string strHNchkvalue = string.Empty;
                string strval = "sp_ReplaceFabricqualityValuesInTrans " + CboQualityFrom.SelectedValue + "," + CboQualityTo.SelectedValue + "";
                DB.ExecuteSQL(strval);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "Ledgers Merged Successfully..");
                CboQualityFrom.SelectedValue = 0;
                CboQualityTo.SelectedValue = 0;
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
                if (CboQualityFrom.Text.Trim().Length <= 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Ledger From");
                    this.CboQualityFrom.Focus();
                    return true;
                }
                if (CboQualityTo.Text.Trim().Length <= 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Ledger To");
                    this.CboQualityTo.Focus();
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
    }
}
