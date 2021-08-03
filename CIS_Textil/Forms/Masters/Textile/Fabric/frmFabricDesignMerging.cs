using System;
using CIS_Bussiness;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmFabricDesignMerging : frmMasterIface
    {
        public frmFabricDesignMerging()
        {
            InitializeComponent();
        }

        private void frmDesignMerging_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FilterId = "";
                Combobox_Setup.FillCbo(ref CboDesignFrom, Combobox_Setup.ComboType.Mst_FabricDesign, "");
                Combobox_Setup.FillCbo(ref CboDesignTo, Combobox_Setup.ComboType.Mst_FabricDesign, "");
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
                string strval = "sp_ReplaceFabricDesignValuesInTrans " + CboDesignFrom.SelectedValue + "," + CboDesignTo.SelectedValue + "";
                DB.ExecuteSQL(strval);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Information, "", "Ledgers Merged Successfully..");
                CboDesignFrom.SelectedValue = 0;
                CboDesignTo.SelectedValue = 0;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                if (CboDesignFrom.Text.Trim().Length <= 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Design From");
                    this.CboDesignFrom.Focus();
                    return true;
                }
                if (CboDesignTo.Text.Trim().Length <= 0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Design To");
                    this.CboDesignTo.Focus();
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
