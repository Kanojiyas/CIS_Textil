using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CIS_DBLayer;
using CIS_Bussiness;
using System.Drawing;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections;
using CIS_DataGridViewEx;
using System.Diagnostics;
using System.Reflection;

namespace CIS_Textil
{
    public partial class frmThemeSetting : frmTrnsIface
    {
        [AccessedThroughProperty("fgDtls")]
        private DataGridViewEx _fgDtls;

        public string strTable = "tbl_ThemeSettings";
        string sColor = string.Empty;
        System.Windows.Forms.DataGridViewCellStyle cHead = new System.Windows.Forms.DataGridViewCellStyle();
        System.Windows.Forms.DataGridViewCellStyle altRows = new System.Windows.Forms.DataGridViewCellStyle();

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
                if (this._fgDtls != null)
                {
                }
                this._fgDtls = value;
                if (this._fgDtls != null)
                {
                }
            }
        }

        public frmThemeSetting()
        {
            fgDtls = new CIS_DataGridViewEx.DataGridViewEx();
            fgDtls.ShowFieldChooser = false;
            InitializeComponent();
        }

        #region Form Event
        private void frmThemeSetting_Load(object sender, EventArgs e)
        {
            try
            {
                DetailGrid_Setup.CreateDtlGrid(this, panel1, fgDtls, this.dt_HasDtls_Grd, this.dt_AryCalcvalue, this.dt_AryIsRequired, true, false, true, 0, 0, false);
            }
            catch (Exception ex)
            {
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        #endregion

        #region FormNavigation
        public void FillControls()
        {
            try
            {
                string sColor = string.Empty;
                DBValue.Return_DBValue(this, txtCode, "ThemeID", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtTheme, "ThemeName", Enum_Define.ValidationType.Text);
                sColor = DBValue.Return_DBValue(this, "ThemeColor"); txtThemeColor.Text = sColor.Replace("/", ",");

                DBValue.Return_DBValue(this, txtLabelFont, "LableFont", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLabelFontStyle, "LabelStyle", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtLabelFontSize, "LabelSize", Enum_Define.ValidationType.Text);
                sColor = DBValue.Return_DBValue(this, "LabelForeColor"); txtLabelForeColor.Text = sColor.Replace("/", ",");
                sColor = DBValue.Return_DBValue(this, "LabelBackColor"); txtLabelBackColor.Text = sColor.Replace("/", ",");

                DBValue.Return_DBValue(this, txtTextBoxFont, "TextBoxFont", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtTextBoxFontStyle, "TextBoxStyle", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtTextBoxFontSize, "TextBoxSize", Enum_Define.ValidationType.Text);
                sColor = DBValue.Return_DBValue(this, "TextBoxForeColor"); txtTextBoxForeColor.Text = sColor.Replace("/", ",");
                sColor = DBValue.Return_DBValue(this, "TextBoxBackColor"); txtTextBoxBackColor.Text = sColor.Replace("/", ",");

                DBValue.Return_DBValue(this, txtComboBoxFont, "ComboBoxFont", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtComboBoxFontStyle, "ComboBoxStyle", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtComboBoxFontSize, "ComboBoxSize", Enum_Define.ValidationType.Text);
                sColor = DBValue.Return_DBValue(this, "ComboBoxForeColor"); txtComboBoxForeColor.Text = sColor.Replace("/", ",");
                sColor = DBValue.Return_DBValue(this, "ComboBoxBackColor"); txtComboBoxBackColor.Text = sColor.Replace("/", ",");

                DBValue.Return_DBValue(this, txtButtonFont, "ButtonFont", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtButtonFontStyle, "ButtonStyle", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtButtonFontSize, "ButtonSize", Enum_Define.ValidationType.Text);
                sColor = DBValue.Return_DBValue(this, "ButtonForeColor"); txtButtonForeColor.Text = sColor.Replace("/", ",");
                sColor = DBValue.Return_DBValue(this, "ButtonBackColor"); txtButtonBackColor.Text = sColor.Replace("/", ",");

                DBValue.Return_DBValue(this, txtRadioFont, "RadioFont", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtRadioFontStyle, "RadioStyle", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtRadioFontSize, "RadioSize", Enum_Define.ValidationType.Text);
                sColor = DBValue.Return_DBValue(this, "RadioForeColor"); txtRadioForeColor.Text = sColor.Replace("/", ",");
                sColor = DBValue.Return_DBValue(this, "RadioBackColor"); txtRadioBackColor.Text = sColor.Replace("/", ",");

                DBValue.Return_DBValue(this, txtCheckBoxFont, "CheckBoxFont", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCheckBoxFontStyle, "CheckBoxStyle", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtCheckBoxFontSize, "CheckBoxSize", Enum_Define.ValidationType.Text);
                sColor = DBValue.Return_DBValue(this, "CheckBoxForeColor"); txtCheckBoxForeColor.Text = sColor.Replace("/", ",");
                sColor = DBValue.Return_DBValue(this, "CheckBoxBackColor"); txtCheckBoxBackColor.Text = sColor.Replace("/", ",");

                DBValue.Return_DBValue(this, txtTabControlFont, "TabControlFont", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtTabControlFontStyle, "TabControlStyle", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtTabControlFontSize, "TabControlSize", Enum_Define.ValidationType.Text);
                sColor = DBValue.Return_DBValue(this, "TabControlForeColor"); txtTabControlForeColor.Text = sColor.Replace("/", ",");
                sColor = DBValue.Return_DBValue(this, "TabControlBackColor"); txtTabControlBackColor.Text = sColor.Replace("/", ",");

                DBValue.Return_DBValue(this, txtGroupBoxFont, "GroupBoxFont", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtGroupBoxFontStyle, "GroupBoxStyle", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtGroupBoxFontSize, "GroupBoxSize", Enum_Define.ValidationType.Text);
                sColor = DBValue.Return_DBValue(this, "GroupBoxForeColor"); txtGroupBoxForeColor.Text = sColor.Replace("/", ",");
                sColor = DBValue.Return_DBValue(this, "GroupBoxBackColor"); txtGroupBoxBackColor.Text = sColor.Replace("/", ",");

                DBValue.Return_DBValue(this, txtGridFont, "GridFont", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtGridFontStyle, "GroupBoxStyle", Enum_Define.ValidationType.Text);
                DBValue.Return_DBValue(this, txtGridFontSize, "GroupBoxSize", Enum_Define.ValidationType.Text);
                sColor = DBValue.Return_DBValue(this, "GridFixedForeColor"); txtGridFixedForeColor.Text = sColor.Replace("/", ",");
                sColor = DBValue.Return_DBValue(this, "GridFixedBackColor"); txtGridFixedBackColor.Text = sColor.Replace("/", ",");
                sColor = DBValue.Return_DBValue(this, "GridEntryForeColor"); txtGridEntryForeColor.Text = sColor.Replace("/", ",");
                sColor = DBValue.Return_DBValue(this, "GridEntryBackColor"); txtGridEntryBackColor.Text = sColor.Replace("/", ",");

                sColor = DBValue.Return_DBValue(this, "PanelBackColor"); txtPanelBackColor.Text = sColor.Replace("/", ",");
                //Preview();
                DetailGrid_Setup.FillGrid(this.fgDtls, this.fgDtls.Grid_UID, this.fgDtls.Grid_Tbl, "ThemeID", txtCode.Text, base.dt_HasDtls_Grd);
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
                Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
            }
        }

        public void MovetoField()
        {
            try
            {
                txtCode.Text = "";
                ResetControls();
                ResetPreview();
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
                ("Theme"),
                txtTheme.Text.Trim().ToString(),
                1,
                txtThemeColor.Text.Trim().Replace(",","/"),
                "User Selected Theme",
                txtLabelFont.Text,
                txtLabelFontStyle.Text,
                float.Parse(txtLabelFontSize.Text),
                txtLabelForeColor.Text.Trim().Replace(",","/"),
                txtLabelBackColor.Text.Trim().Replace(",","/"),

                txtTextBoxFont.Text,
                txtTextBoxFontStyle.Text,
                float.Parse(txtTextBoxFontSize.Text),
                txtTextBoxForeColor.Text.Trim().Replace(",","/"),
                txtTextBoxBackColor.Text.Trim().Replace(",","/"),

                txtComboBoxFont.Text,
                txtComboBoxFontStyle.Text,
                float.Parse(txtComboBoxFontSize.Text),
                txtComboBoxForeColor.Text.Trim().Replace(",","/"),
                txtComboBoxBackColor.Text.Trim().Replace(",","/"),

                txtButtonFont.Text,
                txtButtonFontStyle.Text,
                float.Parse(txtButtonFontSize.Text),
                txtButtonForeColor.Text.Trim().Replace(",","/"),
                txtButtonBackColor.Text.Trim().Replace(",","/"),

                txtRadioFont.Text,
                txtRadioFontStyle.Text,
                float.Parse(txtRadioFontSize.Text),
                txtRadioForeColor.Text.Trim().Replace(",","/"),
                txtRadioBackColor.Text.Trim().Replace(",","/"),

                txtCheckBoxFont.Text,
                txtCheckBoxFontStyle.Text,
                float.Parse(txtCheckBoxFontSize.Text),
                txtCheckBoxForeColor.Text.Trim().Replace(",","/"),
                txtCheckBoxBackColor.Text.Trim().Replace(",","/"),

                txtGridFont.Text,
                txtGridFontStyle.Text,
                float.Parse(txtGridFontSize.Text),
                txtGridFixedForeColor.Text.Trim().Replace(",","/"),
                txtGridFixedBackColor.Text.Trim().Replace(",","/"),
                txtGridEntryForeColor.Text.Trim().Replace(",","/"),
                txtGridEntryBackColor.Text.Trim().Replace(",","/"),

                txtTabControlFont.Text,
                txtTabControlFontStyle.Text,
                float.Parse(txtTabControlFontSize.Text),
                txtTabControlForeColor.Text.Trim().Replace(",","/"),
                txtTabControlBackColor.Text.Trim().Replace(",","/"),

                txtGroupBoxFont.Text,
                txtGroupBoxFontStyle.Text,
                float.Parse(txtGroupBoxFontSize.Text),
                txtGroupBoxForeColor.Text.Trim().Replace(",","/"),
                txtGroupBoxBackColor.Text.Trim().Replace(",","/"),

                txtPanelBackColor.Text.Trim().Replace(",","/"),
                0,
                Db_Detials.UserID,
                Db_Detials.CompID,
                Db_Detials.CompID,
                Db_Detials.YearID
                };

                DBSp.Master_AddEdit(pArrayData, "");
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
                if (txtTheme.Text.Trim() == "Enter Theme Name" || txtTheme.Text.Trim() == "-" || txtTheme.Text.Trim() == "0" || txtTheme.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Theme Name");
                    txtTheme.Focus();
                    return true;
                }

                if (txtThemeColor.Text.Trim() == "Theme Color" || txtThemeColor.Text.Trim() == "-" || txtThemeColor.Text.Trim() == "0" || txtThemeColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Please Enter Theme Color");
                    txtThemeColor.Focus();
                    return true;
                }

                if (txtLabelFont.Text.Trim() == "Font Family" || txtLabelFont.Text.Trim() == "-" || txtLabelFont.Text.Trim() == "0" || txtLabelFont.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Font Family");
                    txtLabelFont.Focus();
                    return true;
                }

                if (txtLabelForeColor.Text.Trim() == "Fore Color" || txtLabelForeColor.Text.Trim() == "-" || txtLabelForeColor.Text.Trim() == "0" || txtLabelForeColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Fore Color");
                    txtLabelForeColor.Focus();
                    return true;
                }

                if (txtLabelBackColor.Text.Trim() == "Back Color" || txtLabelBackColor.Text.Trim() == "-" || txtLabelBackColor.Text.Trim() == "0" || txtLabelBackColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Back Color");
                    txtLabelBackColor.Focus();
                    return true;
                }

                if (txtTextBoxFont.Text.Trim() == "Font Family" || txtTextBoxFont.Text.Trim() == "-" || txtTextBoxFont.Text.Trim() == "0" || txtTextBoxFont.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Font Family");
                    txtTextBoxFont.Focus();
                    return true;
                }

                if (txtTextBoxForeColor.Text.Trim() == "Fore Color" || txtTextBoxForeColor.Text.Trim() == "-" || txtTextBoxForeColor.Text.Trim() == "0" || txtTextBoxForeColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Fore Color");
                    txtTextBoxForeColor.Focus();
                    return true;
                }

                if (txtTextBoxBackColor.Text.Trim() == "Back Color" || txtTextBoxBackColor.Text.Trim() == "-" || txtTextBoxBackColor.Text.Trim() == "0" || txtTextBoxBackColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Back Color");
                    txtTextBoxBackColor.Focus();
                    return true;
                }

                if (txtComboBoxFont.Text.Trim() == "Font Family" || txtComboBoxFont.Text.Trim() == "-" || txtComboBoxFont.Text.Trim() == "0" || txtComboBoxFont.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Font Family");
                    txtComboBoxFont.Focus();
                    return true;
                }

                if (txtComboBoxForeColor.Text.Trim() == "Fore Color" || txtComboBoxForeColor.Text.Trim() == "-" || txtComboBoxForeColor.Text.Trim() == "0" || txtComboBoxForeColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Fore Color");
                    txtComboBoxForeColor.Focus();
                    return true;
                }

                if (txtComboBoxBackColor.Text.Trim() == "Back Color" || txtComboBoxBackColor.Text.Trim() == "-" || txtComboBoxBackColor.Text.Trim() == "0" || txtComboBoxBackColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Back Color");
                    txtComboBoxBackColor.Focus();
                    return true;
                }

                if (txtButtonFont.Text.Trim() == "Font Family" || txtButtonFont.Text.Trim() == "-" || txtButtonFont.Text.Trim() == "0" || txtButtonFont.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Font Family");
                    txtButtonFont.Focus();
                    return true;
                }

                if (txtButtonForeColor.Text.Trim() == "Fore Color" || txtButtonForeColor.Text.Trim() == "-" || txtButtonForeColor.Text.Trim() == "0" || txtButtonForeColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Fore Color");
                    txtButtonForeColor.Focus();
                    return true;
                }

                if (txtButtonBackColor.Text.Trim() == "Back Color" || txtButtonBackColor.Text.Trim() == "-" || txtButtonBackColor.Text.Trim() == "0" || txtButtonBackColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Back Color");
                    txtButtonBackColor.Focus();
                    return true;
                }

                if (txtRadioFont.Text.Trim() == "Font Family" || txtRadioFont.Text.Trim() == "-" || txtRadioFont.Text.Trim() == "0" || txtRadioFont.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Font Family");
                    txtRadioFont.Focus();
                    return true;
                }

                if (txtRadioForeColor.Text.Trim() == "Fore Color" || txtRadioForeColor.Text.Trim() == "-" || txtRadioForeColor.Text.Trim() == "0" || txtRadioForeColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Fore Color");
                    txtRadioForeColor.Focus();
                    return true;
                }

                if (txtRadioBackColor.Text.Trim() == "Back Color" || txtRadioBackColor.Text.Trim() == "-" || txtRadioBackColor.Text.Trim() == "0" || txtRadioBackColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Back Color");
                    txtRadioBackColor.Focus();
                    return true;
                }

                if (txtCheckBoxFont.Text.Trim() == "Font Family" || txtCheckBoxFont.Text.Trim() == "-" || txtCheckBoxFont.Text.Trim() == "0" || txtCheckBoxFont.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Font Family");
                    txtCheckBoxFont.Focus();
                    return true;
                }

                if (txtCheckBoxForeColor.Text.Trim() == "Fore Color" || txtCheckBoxForeColor.Text.Trim() == "-" || txtCheckBoxForeColor.Text.Trim() == "0" || txtCheckBoxForeColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Fore Color");
                    txtCheckBoxForeColor.Focus();
                    return true;
                }

                if (txtCheckBoxBackColor.Text.Trim() == "Back Color" || txtCheckBoxBackColor.Text.Trim() == "-" || txtCheckBoxBackColor.Text.Trim() == "0" || txtCheckBoxBackColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Back Color");
                    txtCheckBoxBackColor.Focus();
                    return true;
                }

                if (txtTabControlFont.Text.Trim() == "Font Family" || txtTabControlFont.Text.Trim() == "-" || txtTabControlFont.Text.Trim() == "0" || txtTabControlFont.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Font Family");
                    txtTabControlFont.Focus();
                    return true;
                }

                if (txtTabControlForeColor.Text.Trim() == "Fore Color" || txtTabControlForeColor.Text.Trim() == "-" || txtTabControlForeColor.Text.Trim() == "0" || txtTabControlForeColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Fore Color");
                    txtTabControlForeColor.Focus();
                    return true;
                }

                if (txtTabControlBackColor.Text.Trim() == "Back Color" || txtTabControlBackColor.Text.Trim() == "-" || txtTabControlBackColor.Text.Trim() == "0" || txtTabControlBackColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Back Color");
                    txtTabControlBackColor.Focus();
                    return true;
                }

                if (txtGroupBoxFont.Text.Trim() == "Font Family" || txtGroupBoxFont.Text.Trim() == "-" || txtGroupBoxFont.Text.Trim() == "0" || txtGroupBoxFont.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Font Family");
                    txtGroupBoxFont.Focus();
                    return true;
                }

                if (txtGroupBoxForeColor.Text.Trim() == "Fore Color" || txtGroupBoxForeColor.Text.Trim() == "-" || txtGroupBoxForeColor.Text.Trim() == "0" || txtGroupBoxForeColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Fore Color");
                    txtGroupBoxForeColor.Focus();
                    return true;
                }

                if (txtGroupBoxBackColor.Text.Trim() == "Back Color" || txtGroupBoxBackColor.Text.Trim() == "-" || txtGroupBoxBackColor.Text.Trim() == "0" || txtGroupBoxBackColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Back Color");
                    txtGroupBoxBackColor.Focus();
                    return true;
                }

                if (txtGridFont.Text.Trim() == "Font Family" || txtGridFont.Text.Trim() == "-" || txtGridFont.Text.Trim() == "0" || txtGridFont.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Font Family");
                    txtGridFont.Focus();
                    return true;
                }

                if (txtGridFixedForeColor.Text.Trim() == "Fore Color(Fixed)" || txtGridFixedForeColor.Text.Trim() == "-" || txtGridFixedForeColor.Text.Trim() == "0" || txtGridFixedForeColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Fore Color");
                    txtGridFixedForeColor.Focus();
                    return true;
                }

                if (txtGridFixedBackColor.Text.Trim() == "Back Color(Fixed)" || txtGridFixedBackColor.Text.Trim() == "-" || txtGridFixedBackColor.Text.Trim() == "0" || txtGridFixedBackColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Back Color");
                    txtGridFixedBackColor.Focus();
                    return true;
                }

                if (txtGridEntryForeColor.Text.Trim() == "Fore Color(Entry)" || txtGridEntryForeColor.Text.Trim() == "-" || txtGridEntryForeColor.Text.Trim() == "0" || txtGridEntryForeColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Fore Color");
                    txtGridEntryForeColor.Focus();
                    return true;
                }

                if (txtGridEntryBackColor.Text.Trim() == "Back Color(Entry)" || txtGridEntryBackColor.Text.Trim() == "-" || txtGridEntryBackColor.Text.Trim() == "0" || txtGridEntryBackColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Back Color");
                    txtGridEntryBackColor.Focus();
                    return true;
                }

                if (txtPanelBackColor.Text.Trim() == "Back Color" || txtPanelBackColor.Text.Trim() == "-" || txtPanelBackColor.Text.Trim() == "0" || txtPanelBackColor.Text.Trim() == "")
                {
                    Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", "Double Click To Set Back Color");
                    txtPanelBackColor.Focus();
                    return true;
                }

                if (DBSp.rtnAction())
                {
                    strTableName = "tbl_ThemeSettings";
                    if (Navigate.CheckDuplicate(ref strTableName, "ThemeName", this.txtTheme.Text.Trim(), false, "", 0L, "", ""))
                    {
                        this.txtTheme.Focus();
                        return true;
                    }
                }
                else
                {
                    strTableName = "tbl_ThemeSettings";
                    if (Navigate.CheckDuplicate(ref strTableName, "ThemeName", this.txtTheme.Text.Trim(), true, "ThemeID", (long)Math.Round(Conversion.Val(this.txtCode.Text.Trim())), "", ""))
                    {
                        this.txtTheme.Focus();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Navigate.logError(ex.Message, ex.StackTrace);
            }
            return false;
        }
        #endregion

        #region Making Font
        private Font MakeFont(string family, float size, string style)
        {
            Font result = null;
            try
            {
                result = new Font(family, size, (FontStyle)Enum.Parse(typeof(FontStyle), style));
            }
            catch
            { }
            return result;
        }

        private Font MakeFontStyle(string family, float size, FontStyle style)
        {
            Font result = null;
            try
            {
                result = new Font(family, size, style);
            }
            catch
            { }
            return result;
        }

        #endregion

        #region Color Converter
        private static String RGBConverter(System.Drawing.Color c)
        {
            String rtn = String.Empty;
            try
            {
                rtn = "" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + "";
            }
            catch
            {
            }

            return rtn;
        }

        private static String HexConverter(System.Drawing.Color c)
        {
            String rtn = String.Empty;
            try
            {
                rtn = "" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
            }
            catch
            {
                //doing nothing
            }

            return rtn;
        }
        #endregion

        #region Controls
        #region Theme
        private void txtThemeColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtThemeColor.Text = sColor;
                //btnDemo.BackColor = dlg.Color;
            }
        }
        #endregion

        #region Label
        private void txtLabelFont_DoubleClick(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtLabelFont.Text = dlg.Font.Name;
                txtLabelFontStyle.Text = dlg.Font.Style.ToString();
                decimal dFontSize = (decimal)dlg.Font.Size;
                txtLabelFontSize.Text = Localization.ParseNativeDecimal(dFontSize.ToString()).ToString();
                lblPreview.Font = dlg.Font;
            }
        }

        private void txtLabelForeColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtLabelForeColor.Text = sColor;
                lblPreview.ForeColor = dlg.Color;
            }
        }

        private void txtLabelBackColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtLabelBackColor.Text = sColor;
                lblPreview.BackColor = dlg.Color;
            }
        }
        #endregion

        #region Textbox
        private void txtTextBoxFont_DoubleClick(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtTextBoxFont.Text = dlg.Font.Name;
                txtTextBoxFontStyle.Text = dlg.Font.Style.ToString();
                decimal dFontSize = (decimal)dlg.Font.Size;
                txtTextBoxFontSize.Text = Localization.ParseNativeDecimal(dFontSize.ToString()).ToString();
                txtDemo.Font = dlg.Font;
            }
        }

        private void txtTextBoxForeColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtTextBoxForeColor.Text = sColor;
                txtDemo.ForeColor = dlg.Color;
            }
        }

        private void txtTextBoxBackColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtTextBoxBackColor.Text = sColor;
                txtDemo.BackColor = dlg.Color;
            }
        }
        #endregion

        #region ComboBox
        private void txtComboBoxFont_DoubleClick(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtComboBoxFont.Text = dlg.Font.Name;
                txtComboBoxFontStyle.Text = dlg.Font.Style.ToString();
                decimal dFontSize = (decimal)dlg.Font.Size;
                txtComboBoxFontSize.Text = Localization.ParseNativeDecimal(dFontSize.ToString()).ToString();
                cboDemo.Font = dlg.Font;
            }
        }

        private void txtComboBoxForeColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtComboBoxForeColor.Text = sColor;
                cboDemo.ForeColor = dlg.Color;
            }
        }

        private void txtComboBoxBackColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtComboBoxBackColor.Text = sColor;
                cboDemo.BackColor = dlg.Color;
            }
        }
        #endregion

        #region Button
        private void txtButtonFont_DoubleClick(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtButtonFont.Text = dlg.Font.Name;
                txtButtonFontStyle.Text = dlg.Font.Style.ToString();
                decimal dFontSize = (decimal)dlg.Font.Size;
                txtButtonFontSize.Text = Localization.ParseNativeDecimal(dFontSize.ToString()).ToString();
                btnDemo.Font = dlg.Font;
            }
        }

        private void txtButtonForeColor_DoubleClick_1(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtButtonForeColor.Text = sColor;
                btnDemo.ForeColor = dlg.Color;
            }
        }

        private void txtButtonBackColor_DoubleClick_1(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtButtonBackColor.Text = sColor;
                btnDemo.BackColor = dlg.Color;
            }
        }
        #endregion

        #region Radio Button
        private void txtRadioFont_DoubleClick(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtRadioFont.Text = dlg.Font.Name;
                txtRadioFontStyle.Text = dlg.Font.Style.ToString();
                decimal dFontSize = (decimal)dlg.Font.Size;
                txtRadioFontSize.Text = Localization.ParseNativeDecimal(dFontSize.ToString()).ToString();
                rdoDemo.Font = dlg.Font;
            }
        }

        private void txtRadioForeColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtRadioForeColor.Text = sColor;
                rdoDemo.ForeColor = dlg.Color;
            }
        }

        private void txtRadioBackColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtRadioBackColor.Text = sColor;
                rdoDemo.BackColor = dlg.Color;
            }
        }
        #endregion

        #region Check Box
        private void txtCheckBoxFont_DoubleClick(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtCheckBoxFont.Text = dlg.Font.Name;
                txtCheckBoxFontStyle.Text = dlg.Font.Style.ToString();
                decimal dFontSize = (decimal)dlg.Font.Size;
                txtCheckBoxFontSize.Text = Localization.ParseNativeDecimal(dFontSize.ToString()).ToString();
                ChkDemo.Font = dlg.Font;
            }
        }

        private void txtCheckBoxForeColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtCheckBoxForeColor.Text = sColor;
                ChkDemo.BackColor = dlg.Color;
            }
        }

        private void txtCheckBoxBackColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtCheckBoxBackColor.Text = sColor;
                ChkDemo.BackColor = dlg.Color;
            }
        }
        #endregion

        #region Tab Control
        private void txtTabControlFont_DoubleClick(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtTabControlFont.Text = dlg.Font.Name;
                txtTabControlFontStyle.Text = dlg.Font.Style.ToString();
                decimal dFontSize = (decimal)dlg.Font.Size;
                txtTabControlFontSize.Text = Localization.ParseNativeDecimal(dFontSize.ToString()).ToString();
                TabPage1.Font = dlg.Font;
            }
        }

        private void txtTabControlForeColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtTabControlForeColor.Text = sColor;
                TabPage1.ForeColor = dlg.Color;
            }
        }

        private void txtTabControlBackColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtTabControlBackColor.Text = sColor;
                TabPage1.BackColor = dlg.Color;
            }
        }
        #endregion

        #region GroupBox
        private void txtGroupBoxFont_DoubleClick(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtGroupBoxFont.Text = dlg.Font.Name;
                txtGroupBoxFontStyle.Text = dlg.Font.Style.ToString();
                decimal dFontSize = (decimal)dlg.Font.Size;
                txtGroupBoxFontSize.Text = Localization.ParseNativeDecimal(dFontSize.ToString()).ToString();
                grpPreview.Font = dlg.Font;
            }
        }

        private void txtGroupBoxForeColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtGroupBoxForeColor.Text = sColor;
                grpPreview.ForeColor = dlg.Color;
            }
        }

        private void txtGroupBoxBackColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtGroupBoxBackColor.Text = sColor;
                grpPreview.BackColor = dlg.Color;
            }
        }
        #endregion

        #region Data Grid
        private void txtGridFont_DoubleClick(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                txtGridFont.Text = dlg.Font.Name;
                txtGridFontStyle.Text = dlg.Font.Style.ToString();
                decimal dFontSize = (decimal)dlg.Font.Size;
                txtGridFontSize.Text = Localization.ParseNativeDecimal(dFontSize.ToString()).ToString();

                cHead.Font = dlg.Font;
                altRows.Font = dlg.Font;

                fgDtls.ColumnHeadersDefaultCellStyle = cHead;
                fgDtls.AlternatingRowsDefaultCellStyle = altRows;
                fgDtls.ColumnHeadersHeight = 30;
                foreach (DataGridViewColumn col in fgDtls.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                fgDtls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            }
        }

        private void txtGridFixedForeColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtGridFixedForeColor.Text = sColor;

                cHead.ForeColor = dlg.Color;
                altRows.ForeColor = dlg.Color;

                fgDtls.ColumnHeadersDefaultCellStyle = cHead;
                fgDtls.AlternatingRowsDefaultCellStyle = altRows;
                fgDtls.ColumnHeadersHeight = 30;
                foreach (DataGridViewColumn col in fgDtls.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                fgDtls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            }
        }

        private void txtGridFixedBackColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtGridFixedBackColor.Text = sColor;

                cHead.BackColor = dlg.Color;
                altRows.BackColor = dlg.Color;

                fgDtls.ColumnHeadersDefaultCellStyle = cHead;
                fgDtls.AlternatingRowsDefaultCellStyle = altRows;
                fgDtls.ColumnHeadersHeight = 30;
                foreach (DataGridViewColumn col in fgDtls.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                fgDtls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            }
        }

        private void txtGridEntryForeColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtGridEntryForeColor.Text = sColor;
                cHead.SelectionForeColor = dlg.Color;
                altRows.SelectionForeColor = dlg.Color;

                fgDtls.ColumnHeadersDefaultCellStyle = cHead;
                fgDtls.AlternatingRowsDefaultCellStyle = altRows;
                fgDtls.ColumnHeadersHeight = 30;
                foreach (DataGridViewColumn col in fgDtls.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                fgDtls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            }
        }

        private void txtGridEntryBackColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtGridEntryBackColor.Text = sColor;
                cHead.SelectionBackColor = dlg.Color;
                altRows.SelectionBackColor = dlg.Color;

                fgDtls.ColumnHeadersDefaultCellStyle = cHead;
                fgDtls.AlternatingRowsDefaultCellStyle = altRows;
                fgDtls.ColumnHeadersHeight = 30;
                foreach (DataGridViewColumn col in fgDtls.Columns)
                    col.SortMode = DataGridViewColumnSortMode.NotSortable;
                fgDtls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            }
        }


        #endregion

        #region Panel
        private void txtPanelBackColor_DoubleClick(object sender, EventArgs e)
        {
            ColorDialog dlg = new ColorDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                sColor = RGBConverter(dlg.Color);
                txtPanelBackColor.Text = sColor;
                pnlDetail.BackColor = dlg.Color;
            }
        }
        #endregion
        #endregion

        #region ResetControls and Preview
        private void Preview()
        {
            Font font;
            string[] sColor;

            //Display the font family's name.
            lblPreview.Text = txtLabelFont.Text;
            sColor = txtLabelForeColor.Text.Split(',');
            lblPreview.ForeColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));
            sColor = txtLabelBackColor.Text.Split(',');
            lblPreview.BackColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));

            //Use the font family.

            font = MakeFont(txtLabelFont.Text, float.Parse(txtLabelFontSize.Text), txtLabelFontStyle.Text);
            lblPreview.Font = font;

            //TextBox
            txtDemo.Font = MakeFont(txtTextBoxFont.Text, float.Parse(txtTextBoxFontSize.Text), txtTextBoxFontStyle.Text);
            sColor = txtTextBoxForeColor.Text.Split(',');
            txtDemo.ForeColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));
            sColor = txtTextBoxBackColor.Text.Split(',');
            txtDemo.BackColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));

            //ComboBox
            cboDemo.Font = MakeFont(txtComboBoxFont.Text, float.Parse(txtComboBoxFontSize.Text), txtComboBoxFontStyle.Text);
            sColor = txtComboBoxForeColor.Text.Split(',');
            cboDemo.ForeColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));
            sColor = txtComboBoxBackColor.Text.Split(',');
            cboDemo.BackColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));

            //Button
            btnDemo.Font = MakeFont(txtButtonFont.Text, float.Parse(txtButtonFontSize.Text), txtButtonFontStyle.Text);
            sColor = txtButtonForeColor.Text.Split(',');
            btnDemo.ForeColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));
            sColor = txtButtonBackColor.Text.Split(',');
            btnDemo.BackColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));

            //RadioButton
            rdoDemo.Font = MakeFont(txtRadioFont.Text, float.Parse(txtRadioFontSize.Text), txtRadioFontStyle.Text);
            sColor = txtRadioForeColor.Text.Split(',');
            rdoDemo.ForeColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));
            sColor = txtRadioBackColor.Text.Split(',');
            rdoDemo.BackColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));

            //CheckBox
            ChkDemo.Font = MakeFont(txtCheckBoxFont.Text, float.Parse(txtCheckBoxFontSize.Text), txtCheckBoxFontStyle.Text);
            sColor = txtCheckBoxForeColor.Text.Split(',');
            ChkDemo.ForeColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));
            sColor = txtCheckBoxBackColor.Text.Split(',');
            ChkDemo.BackColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));

            //TabControl
            TabPage1.Font = MakeFont(txtTabControlFont.Text, float.Parse(txtTabControlFontSize.Text), txtTabControlFontStyle.Text);
            tabPage2.Font = MakeFont(txtTabControlFont.Text, float.Parse(txtTabControlFontSize.Text), txtTabControlFontStyle.Text);
            sColor = txtTabControlForeColor.Text.Split(',');
            TabPage1.ForeColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));
            tabPage2.ForeColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));
            sColor = txtTabControlBackColor.Text.Split(',');
            TabPage1.BackColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));
            tabPage2.BackColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));

            //GroupBox
            grpPreview.Font = MakeFont(txtGroupBoxFont.Text, float.Parse(txtGroupBoxFontSize.Text), txtGroupBoxFontStyle.Text);
            sColor = txtGroupBoxForeColor.Text.Split(',');
            grpPreview.ForeColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));
            sColor = txtGroupBoxBackColor.Text.Split(',');
            grpPreview.BackColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));

            //Panel
            sColor = txtPanelBackColor.Text.Split(',');
            pnlDetail.BackColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));

            //Grid
            cHead.Font = MakeFont(txtGridFont.Text, float.Parse(txtGridFontSize.Text), txtGridFontStyle.Text);
            sColor = txtGridFixedBackColor.Text.Split(',');
            cHead.BackColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));
            sColor = txtGridFixedForeColor.Text.Split(',');
            cHead.ForeColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));
            sColor = txtGridEntryBackColor.Text.Split(',');
            cHead.SelectionBackColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));
            sColor = txtGridEntryForeColor.Text.Split(',');
            cHead.SelectionForeColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));

            altRows.Font = MakeFont(txtGridFont.Text, float.Parse(txtGridFontSize.Text), txtGridFontStyle.Text);
            sColor = txtGridFixedBackColor.Text.Split(',');
            altRows.BackColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));
            sColor = txtGridFixedForeColor.Text.Split(',');
            altRows.ForeColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));
            sColor = txtGridEntryBackColor.Text.Split(',');
            altRows.SelectionBackColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));
            sColor = txtGridEntryForeColor.Text.Split(',');
            altRows.SelectionForeColor = Color.FromArgb(Localization.ParseNativeInt(sColor[0]), Localization.ParseNativeInt(sColor[1]), Localization.ParseNativeInt(sColor[2]));

            fgDtls.ColumnHeadersDefaultCellStyle = cHead;
            fgDtls.AlternatingRowsDefaultCellStyle = altRows;
            fgDtls.ColumnHeadersHeight = 30;
            foreach (DataGridViewColumn col in fgDtls.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            fgDtls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        private void ResetControls()
        {
            txtTheme.Text = "Enter Theme Name";
            txtTheme.Focus();
            txtThemeColor.Text = "Theme Color";

            txtLabelFont.Text = "Font Family";
            txtLabelFontStyle.Text = "Style";
            txtLabelFontSize.Text = "Size";
            txtLabelForeColor.Text = "Fore Color";
            txtLabelBackColor.Text = "Back Color";

            txtTextBoxFont.Text = "Font Family";
            txtTextBoxFontStyle.Text = "Style";
            txtTextBoxFontSize.Text = "Size";
            txtTextBoxForeColor.Text = "Fore Color";
            txtTextBoxBackColor.Text = "Back Color";

            txtComboBoxFont.Text = "Font Family";
            txtComboBoxFontStyle.Text = "Style";
            txtComboBoxFontSize.Text = "Size";
            txtComboBoxForeColor.Text = "Fore Color";
            txtComboBoxBackColor.Text = "Back Color";

            txtButtonFont.Text = "Font Family";
            txtButtonFontStyle.Text = "Style";
            txtButtonFontSize.Text = "Size";
            txtButtonForeColor.Text = "Fore Color";
            txtButtonBackColor.Text = "Back Color";

            txtRadioFont.Text = "Font Family";
            txtRadioFontStyle.Text = "Style";
            txtRadioFontSize.Text = "Size";
            txtRadioForeColor.Text = "Fore Color";
            txtRadioBackColor.Text = "Back Color";

            txtCheckBoxFont.Text = "Font Family";
            txtCheckBoxFontStyle.Text = "Style";
            txtCheckBoxFontSize.Text = "Size";
            txtCheckBoxForeColor.Text = "Fore Color";
            txtCheckBoxBackColor.Text = "Back Color";

            txtTabControlFont.Text = "Font Family";
            txtTabControlFontStyle.Text = "Style";
            txtTabControlFontSize.Text = "Size";
            txtTabControlForeColor.Text = "Fore Color";
            txtTabControlBackColor.Text = "Back Color";

            txtGroupBoxFont.Text = "Font Family";
            txtGroupBoxFontStyle.Text = "Style";
            txtGroupBoxFontSize.Text = "Size";
            txtGroupBoxForeColor.Text = "Fore Color";
            txtGroupBoxBackColor.Text = "Back Color";

            txtGridFont.Text = "Font Family";
            txtGridFontStyle.Text = "Style";
            txtGridFontSize.Text = "Size";
            txtGridFixedForeColor.Text = "Fore Color(Fixed)";
            txtGridFixedBackColor.Text = "Back Color(Fixed)";
            txtGridEntryForeColor.Text = "Fore Color(Entry)";
            txtGridEntryBackColor.Text = "Back Color(Entry)";

            txtPanelBackColor.Text = "Back Color";
        }

        public void ResetPreview()
        {
            lblPreview.Font = MakeFontStyle("Verdana", 8, FontStyle.Bold);
            txtDemo.Font = MakeFontStyle("Verdana", 8, FontStyle.Bold);
            cboDemo.Font = MakeFontStyle("Verdana", 8, FontStyle.Bold);
            btnDemo.Font = MakeFontStyle("Verdana", 8, FontStyle.Bold);
            rdoDemo.Font = MakeFontStyle("Verdana", 8, FontStyle.Bold);
            ChkDemo.Font = MakeFontStyle("Verdana", 8, FontStyle.Bold);
            TabPage1.Font = MakeFontStyle("Verdana", 8, FontStyle.Bold);
            tabPage2.Font = MakeFontStyle("Verdana", 8, FontStyle.Bold);
            grpPreview.Font = MakeFontStyle("Verdana", 8, FontStyle.Bold);
            lblPreview.ForeColor = Color.Black;
            txtDemo.ForeColor = Color.Black;
            cboDemo.ForeColor = Color.Black;
            btnDemo.ForeColor = Color.Black;
            rdoDemo.ForeColor = Color.Black;
            ChkDemo.ForeColor = Color.Black;
            TabPage1.ForeColor = Color.Black;
            tabPage2.ForeColor = Color.Black;
            grpPreview.ForeColor = Color.Black;

            lblPreview.BackColor = Color.White;
            txtDemo.BackColor = Color.White;
            cboDemo.BackColor = Color.White;
            btnDemo.BackColor = Color.White;
            rdoDemo.BackColor = Color.White;
            ChkDemo.BackColor = Color.White;
            TabPage1.BackColor = Color.White;
            tabPage2.BackColor = Color.White;
            grpPreview.BackColor = Color.White;



        }
        #endregion

        private void txtThemeColor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Theme oTheme = new Theme();
                Form actObj = (Form)RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());
                object MDI = RuntimeHelpers.GetObjectValue(Navigate.GetForm_byName("MDIMain"));
                if (actObj != null)
                {
                    oTheme.SetThemeOnControls(actObj);
                    oTheme.SetThemeOnMDI((Control)MDI);
                }
                else
                {
                    oTheme.SetThemeOnControls((Control)MDI);
                    oTheme.SetThemeOnMDI((Control)MDI);
                }
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
        }

        private void frmThemeSetting_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Theme oTheme = new Theme();
                //string strTheme = DB.GetSnglValue("select ThemeName from tbl_ThemeSettings where UserID=" + Db_Detials.UserID + " and ThemeType=" + CommonLogic.SQuote("Theme") + "");
                //Db_Detials.ActiveTheme = strTheme;
                object MDI = RuntimeHelpers.GetObjectValue(Navigate.GetForm_byName("MDIMain"));
                Form actObj = (Form)RuntimeHelpers.GetObjectValue(Navigate.GetActiveChild());

                if (actObj != null)
                {
                    oTheme.SetThemeOnControls(actObj);
                    oTheme.SetThemeOnMDI((Control)MDI);
                }
                else
                {
                    oTheme.SetThemeOnControls((Control)MDI);
                    oTheme.SetThemeOnMDI((Control)MDI);
                }
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                Preview();
            }
            catch (Exception ex) { Navigate.logError(ex.Message, ex.StackTrace); }
        }

    }
}
