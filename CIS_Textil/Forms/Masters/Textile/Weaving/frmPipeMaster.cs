using System;
using System.Collections;
using System.Windows.Forms;
using CIS_Bussiness;
using CIS_DBLayer;

namespace CIS_Textil
{
    public partial class frmPipeMaster : frmMasterIface
    {
        public frmPipeMaster()
        {
            InitializeComponent();
        }

        #region Form Event

        private void frmPipeMaster_Load(object sender, EventArgs e)
        {
            try
            {
                if (base.ref_Cbo != null)
                {
                    Form cForm = this;
                    Navigate.NavigateForm(Enum_Define.Navi_form.New_Record, ref cForm, true, false);
                    if (ref_Cbo is CIS_DataGridViewEx.DataGridViewEx)
                    {
                        txtPipeNo.Text = ((CIS_DataGridViewEx.DataGridViewEx)base.ref_Cbo).CurrentCell.EditedFormattedValue.ToString();
                        this.isGridmasterAddText = true;
                    }
                    else
                    {
                        this.txtPipeNo.Text = ((CIS_MultiColumnComboBox.CIS_MultiColumnComboBox)base.ref_Cbo).Text;
                        txtPipeNo.Focus();
                        this.isComboAddText = true;
                        this.isSecondMessage = true;
                    }
                }
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

        public void SaveRecord()
        {
            try
            {
                ArrayList pArrayData = new ArrayList
                {
                    txtPipeNo.Text.Trim(),null,
                    txtPipeSize.Text.Trim(),
                    txtPipeWt.Text.Trim(),
                    (ChkActive.Checked ? 1 : 0),
                    (cboEI1.SelectedValue == null ? 0 : cboEI1.SelectedValue),
                    (cboEI2.SelectedValue == null ? 0 : cboEI2.SelectedValue),
                    (txtET1.Text.Trim()),
                    (txtET2.Text.Trim()),
                    (txtET3.Text.Trim()),
                };
                DBSp.Master_AddEdit(pArrayData, "");
                this.IsMasterAdded = true;
            }
            catch (Exception ex)
            {
                this.IsMasterAdded = false;
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", App_Messages.msg_Save_Error);
            }
        }

        public bool ValidateForm()
        {
            try
            {
                string strTable;
                if (txtPipeNo.Text.Trim() == "" || txtPipeNo.Text.Trim() == "-" || txtPipeNo.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Pipe No");
                    txtPipeNo.Focus();
                    return true;
                }
                if (txtPipeSize.Text.Trim() == "" || txtPipeSize.Text.Trim() == "-" || txtPipeSize.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Pipe Size");
                    txtPipeSize.Focus();
                    return true;
                }
                if (txtPipeWt.Text.Trim() == "" || txtPipeWt.Text.Trim() == "-" || txtPipeWt.Text.Trim() == "0")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Pipe Weight");
                    txtPipeWt.Focus();
                    return true;
                }
                if (DBSp.rtnAction())
                {
                    strTable = "tbl_PipeMaster";
                    if (Navigate.CheckDuplicate(ref strTable, "PipeNo", txtPipeNo.Text.Trim(), false, "", 0L, "", ""))
                    {
                        txtPipeNo.Focus();
                        return true;
                    }
                }
                strTable = "tbl_PipeMaster";
                if (Navigate.CheckDuplicate(ref strTable, "PipeNo", txtPipeNo.Text.Trim(), true, "PipeID", (long)Math.Round(Localization.ParseNativeDouble(txtCode.Text.Trim())), "", ""))
                {
                    txtPipeNo.Focus();
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

        public void FillControls()
        {
            try
            {
                DBValue.Return_DBValue(this, txtCode, "PipeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPipeNo, "PipeNo", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPipeSize, "PipeSize", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtPipeWt, "PipeWt", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, ChkActive, "IsActive", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI1, "EI1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, cboEI2, "EI2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET1, "ET1", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET2, "ET2", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtET3, "ET3", Enum_Define.ValidationType.Text);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
            ApplyActStatus();
        }

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                txtPipeNo.Focus();
                ApplyActStatus();
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
        }

        #endregion

    }
}
