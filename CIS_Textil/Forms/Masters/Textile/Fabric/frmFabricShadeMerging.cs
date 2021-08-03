using System;
using CIS_Bussiness;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmFabricShadeMerging : frmMasterIface
    {
        public frmFabricShadeMerging()
        {
            InitializeComponent();
        }

        private void frmShadeMerging_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref CboShadeFrom, Combobox_Setup.ComboType.Mst_FabricShade, "");
                Combobox_Setup.FillCbo(ref CboShadeTo, Combobox_Setup.ComboType.Mst_FabricShade, "");
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
                string strval = "sp_ReplaceFabricShadeValuesInTrans " + CboShadeFrom.SelectedValue + "," + CboShadeTo.SelectedValue + "";
                DB.ExecuteSQL(strval);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "Ledgers Merged Successfully..");
                CboShadeFrom.SelectedValue = 0;
                CboShadeTo.SelectedValue = 0;
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
                if (CboShadeFrom.Text.Trim().Length <= 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Ledger From");
                    this.CboShadeFrom.Focus();
                    return true;
                }
                if (CboShadeTo.Text.Trim().Length <= 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Ledger To");
                    this.CboShadeTo.Focus();
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
