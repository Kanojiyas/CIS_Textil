using System;
using CIS_Bussiness;
using CIS_DBLayer;
using CIS_CLibrary;

namespace CIS_Textil
{
    public partial class frmCatalogMerging : frmMasterIface
    {
        public frmCatalogMerging()
        {
            InitializeComponent();
        }

        private void frmCatalogMerging_Load(object sender, System.EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref cboCatalogFrom, Combobox_Setup.ComboType.Mst_Catalogue, "");
                Combobox_Setup.FillCbo(ref cboCatalogTo, Combobox_Setup.ComboType.Mst_Catalogue, "");
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
                string strval = "sp_ReplaceCatalogValuesInTrans " + cboCatalogFrom.SelectedValue + "," + cboCatalogTo.SelectedValue + "";
                DB.ExecuteSQL(strval);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.SecuritySuccess, "", "Ledgers Updated Successfully.");
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
                if (cboCatalogFrom.Text.Trim().Length <= 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Catalog From");
                    this.cboCatalogFrom.Focus();
                    return true;
                }
                if (cboCatalogTo.Text.Trim().Length <= 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Catalog To");
                    this.cboCatalogTo.Focus();
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
