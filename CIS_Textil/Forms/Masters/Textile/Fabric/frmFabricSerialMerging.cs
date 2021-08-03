using System;
using CIS_Bussiness;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmFabricSerialMerging : frmMasterIface
    {
        public frmFabricSerialMerging()
        {
            InitializeComponent();
        }

        private void frmFabricSerialMerging_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboFabricSerialFrom, Combobox_Setup.ComboType.Mst_FabricSerial, "");
                Combobox_Setup.FillCbo(ref cboFabricSerialTo, Combobox_Setup.ComboType.Mst_FabricSerial, "");
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
                string strval = "sp_ReplaceBookSerialValuesInTrans " + cboFabricSerialFrom.SelectedValue + "," + cboFabricSerialTo.SelectedValue + "";
                DB.ExecuteSQL(strval);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecuritySuccess, "", "Ledgers Merged Successfully.");
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
                if (cboFabricSerialFrom.Text.Trim().Length <= 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Book Serial From");
                    this.cboFabricSerialFrom.Focus();
                    return true;
                }
                if (cboFabricSerialTo.Text.Trim().Length <= 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Book Serial To");
                    this.cboFabricSerialTo.Focus();
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
