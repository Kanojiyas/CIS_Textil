using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using Microsoft.VisualBasic.CompilerServices;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmPackingRateList : frmMasterIface
    {
        [AccessedThroughProperty("fgDtls")]
        private CIS_DataGridViewEx.DataGridViewEx _fgDtls;

        public virtual CIS_DataGridViewEx.DataGridViewEx fgDtls
        {
            [DebuggerNonUserCode]
            get
            {
                return this._fgDtls;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {

            }
        }

        public frmPackingRateList()
        {
            InitializeComponent();
        }
        private void frm_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboPacker, Combobox_Setup.ComboType.Mst_Packer, "");
                DetailGrid_Setup.CreateDtlGrid(this, pnlDetail, fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0);
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
                DBValue.Return_DBValue(this, txtCode, "PakingRateListID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboPacker, "PackerID", Enum_Define.ValidationType.Text);
                DetailGrid_Setup.FillGrid(this.fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "PakingRateListID", Conversions.ToString(Localization.ParseNativeDouble(this.txtCode.Text)), base.dt_HasDtls_Grd);
                this.fgDtls = fgDtls;
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
                txtCode.Text = "";
                {
                    EventHandles.CreateDefault_Rows(this.fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                }
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
                ArrayList pArrayData = new ArrayList{
                cboPacker.SelectedValue
                };
                DBSp.Transcation_AddEdit(pArrayData, this.fgDtls, true);
                this.IsMasterAdded = true;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                this.IsMasterAdded = false;
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }
        public bool ValidateForm()
        {
            try
            {
                if (!EventHandles.IsValidGridReq(this.fgDtls, base.dt_AryIsRequired))
                {
                    return true;
                }
                DataGridViewEx fgDtls = this.fgDtls;
                this.fgDtls = fgDtls;
                if (!EventHandles.IsRequiredInGrid(this.fgDtls, this.dt_AryIsRequired, false))
                {
                    return true;
                }
                if (cboPacker.Text.Trim().Length <= 0.0)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Packer");
                    this.cboPacker.Focus();
                    return true;
                }
                if (!this.cboPacker.IsValidSelect)
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Valid Packer");
                    this.cboPacker.Focus();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                return true;
            }
        }
    }
}
