using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DataGridViewEx;
using CIS_Bussiness;
using CIS_DBLayer;
using Microsoft.VisualBasic.CompilerServices;

namespace CIS_Textil
{
    public partial class frmTaskMaster : frmMasterIface
    {
        [AccessedThroughProperty("fgDtls")]
        private DataGridViewEx _fgDtls;
        public frmTaskMaster()
        {
            InitializeComponent();
            fgDtls = new DataGridViewEx();
        }

        #region Form Event

        private void frmTaskMaster_Load(object sender, EventArgs e)
        {
            try
            {
                Combobox_Setup.FillCbo(ref cboTaskType, Combobox_Setup.ComboType.Mst_TaskType, "");
                DetailGrid_Setup.CreateDtlGrid(this, pnlDetail, fgDtls, dt_HasDtls_Grd, dt_AryCalcvalue, dt_AryIsRequired, true, false, true, 0, 0);
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtTaskName.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtTaskName.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtTaskName.Focus();
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

        public bool ValidateForm()
        {
            string str;
            try
            {
                if (fgDtls.Rows[0].Cells[3].Value != null && Localization.ParseNativeDouble(fgDtls.Rows[0].Cells[3].Value.ToString()) > 0)
                {
                    if (!EventHandles.IsValidGridReq(this.fgDtls, base.dt_AryIsRequired))
                    {
                        return true;
                    }
                    if (!EventHandles.IsRequiredInGrid(fgDtls, dt_AryIsRequired, false))
                    {
                        return true;
                    }
                }
                if (cboTaskType.SelectedValue == null || cboTaskType.SelectedValue.ToString() == "" || cboTaskType.SelectedValue.ToString() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Select Task Type");
                    cboTaskType.Focus();
                    return true;
                }
                if (txtTaskName.Text.Trim() == "" || txtTaskName.Text.Trim() == "-" || txtTaskName.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Task Name");
                    txtTaskName.Focus();
                    return true;
                }
                if (DBSp.rtnAction())
                {
                    str = "tbl_TaskMasterMain";
                    if (Navigate.CheckDuplicate(ref str, "TaskName", txtTaskName.Text.Trim(), false, "", 0L, "", "This TaskName is already available"))
                    {
                        txtTaskName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), false, "", 0L, "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtTaskName.Text.Trim(), false, "", 0L, "", "This TaskName is already Used in AliasName"))
                    {
                        txtTaskName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "TaskName", txtAliasName.Text.Trim(), false, "", 0L, "", "This AliasName is already Used in TaskName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                else
                {
                    str = "tbl_TaskMasterMain";
                    if (Navigate.CheckDuplicate(ref str, "TaskName", txtTaskName.Text.Trim(), true, "TaskID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This TaskName is already available"))
                    {
                        txtTaskName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtAliasName.Text.Trim(), true, "TaskID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This Aliasname is already available"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }

                    if (Navigate.CheckDuplicate(ref str, "AliasName", txtTaskName.Text.Trim(), true, "TaskID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This TaskName is already Used in AliasName"))
                    {
                        txtTaskName.Focus();
                        return true;
                    }
                    if (Navigate.CheckDuplicate(ref str, "TaskName", txtAliasName.Text.Trim(), true, "TaskID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", "This AliasName is already Used in TaskName"))
                    {
                        txtAliasName.Focus();
                        return true;
                    }
                }
                if (CommonCls.ValidateMaster(this, txtTaskName, txtAliasName, "tbl_TaskMasterMain", "TaskName"))
                    return true;
                if (CommonCls.ValidateShortCode(this, txtTaskName, txtAliasName, "tbl_TaskMasterMain", "TaskName"))
                    return true;
                return false;
            }
            catch (Exception exception1)
            {
                Navigate.logError(exception1.Message, exception1.StackTrace);
                return false;
            }
        }

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                CommonCls.IncFieldID(this, ref txtCode, "");
                EventHandles.CreateDefault_Rows(fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, false, false);
                ApplyActStatus();
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
                DBValue.Return_DBValue(this, txtCode, "TaskID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboTaskType, "TaskTypeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtTaskName, "TaskName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtAliasName, "AliasName", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
                DetailGrid_Setup.FillGrid(fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "TaskID", Conversions.ToString(Localization.ParseNativeDouble(this.txtCode.Text)), base.dt_HasDtls_Grd);
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
                sComboAddText = txtTaskName.Text;
                ArrayList pArrayData = new ArrayList
                {
                    (cboTaskType.SelectedValue),
                    (txtTaskName.Text.Trim()),
                    (txtAliasName.Text == "" ? null : txtAliasName.Text),
                    (ChkActive.Checked?1: 0)
                };

                if (fgDtls.Rows[0].Cells[3].Value != null && Localization.ParseNativeDouble(fgDtls.Rows[0].Cells[3].Value.ToString()) > 0)
                {
                    DBSp.Transcation_AddEdit(pArrayData, fgDtls, true, "");
                }
                else
                {
                    DBSp.Master_AddEdit(pArrayData, "");
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

        #endregion

        private void fgDtls_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
                {
                    if (Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString()) > 0 && fgDtls.Rows[e.RowIndex].Cells[2].Value != null)
                    {
                        DataTable dt = DB.GetDT("Select * from tbl_CheckListMaster Where CheckListTypeID=" + Localization.ParseNativeInt(fgDtls.Rows[e.RowIndex].Cells[2].Value.ToString()) + "", false);
                        DataGridViewComboBoxColumn cboType = (DataGridViewComboBoxColumn)fgDtls.Columns[3];
                        cboType.DataSource = dt;
                        cboType.DisplayMember = "CheckListName";
                        cboType.ValueMember = "CheckListID";
                        cboType.AutoComplete = true;
                    }
                }

                if (((base.blnFormAction == Enum_Define.ActionType.New_Record) | (base.blnFormAction == Enum_Define.ActionType.Edit_Record)) && (((e.ColumnIndex == 5) | (e.ColumnIndex == 6)) | (e.ColumnIndex == 8)))
                {
                    //CalcVal();
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        public virtual DataGridViewEx fgDtls
        {
            [DebuggerNonUserCode]
            get
            {
                return this._fgDtls;
            }
            [MethodImpl(MethodImplOptions.Synchronized), DebuggerNonUserCode]
            set
            {
                DataGridViewCellEventHandler handler = new DataGridViewCellEventHandler(this.fgDtls_CellValueChanged);
                if (this._fgDtls != null)
                {
                    this._fgDtls.CellValueChanged -= handler;
                }
                this._fgDtls = value;
                if (this._fgDtls != null)
                {
                    this._fgDtls.CellValueChanged += handler;
                }
            }
        }

        private void txtTaskName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateMaster(this, txtTaskName, txtAliasName, "tbl_TaskMasterMain", "TaskName");
        }

        private void txtAliasName_Leave(object sender, EventArgs e)
        {
            CommonCls.ValidateShortCode(this, txtTaskName, txtAliasName, "tbl_TaskMasterMain", "TaskName");
        }
    }
}
