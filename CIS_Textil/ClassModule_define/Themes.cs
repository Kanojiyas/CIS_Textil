using System.Drawing;
using System.Windows.Forms;
using  CIS_Bussiness;using CIS_DBLayer;
using System.Data;
using System;
using System.Reflection;

public class Theme
{
    System.Windows.Forms.DataGridViewCellStyle cHead = new System.Windows.Forms.DataGridViewCellStyle();
    System.Windows.Forms.DataGridViewCellStyle altRows = new System.Windows.Forms.DataGridViewCellStyle();

    #region New Theme
    string ActTheme = "";
    string ActLabelFont = "", ActTextBoxFont = "",ActComboBoxFont = "",ActButtonFont="",ActRadioFont = "",ActCheckBoxFont= "",ActTabControlFont = "",ActGroupBoxFont = "",ActGridFont="";
    float ActLabelFontSize = 0,ActTextBoxFontSize = 0,ActComboBoxFontSize = 0,ActButtonFontSize = 0,ActRadioFontSize = 0,ActCheckBoxFontSize = 0,ActTabControlFontSize = 0,ActGroupBoxFontSize = 0,ActGridFontSize = 0;
    string ActLabelFontStyle= "",ActTextBoxFontStyle="",ActComboBoxFontStyle = "",ActButtonFontStyle ="",ActRadioFontStyle = "",ActCheckBoxFontStyle= "",ActTabControlFontStyle = "",ActGroupBoxFontStyle = "",ActGridFontStyle="";

    string[] ActThemeColor,ActLabelForeColor, ActTextBoxForeColor, ActComboBoxForeColor, ActButtonForeColor, ActRadioForeColor, ActCheckBoxForeColor, ActTabControlForeColor, ActGroupBoxForeColor;
    string[] ActLabelBackColor,ActTextBoxBackColor, ActComboBoxBackColor, ActButtonBackColor, ActRadioBackColor, ActCheckBoxBackColor, ActTabControlBackColor, ActGroupBoxBackColor;
    string[] ActGridFixedForeColor , ActGridFixedBackColor, ActGridEntryForeColor, ActGridEntryBackColor,ActPanelBackColor;

    private void AssignDataToThemeVariables()
    {
        if (Localization.ParseNativeInt(DB.GetSnglValue("SELECT Count(0) From tbl_ThemeSettings_User Where UserID=" + Db_Detials.UserID)) > 0)
        {
            using (IDataReader dr = DB.GetRS(String.Format("Select * from tbl_ThemeSettings where ThemeID IN (SELECT ThemeID From tbl_ThemeSettings_User Where UserID= "+ Db_Detials.UserID +")")))
            {
                while (dr.Read())
                {
                    ActTheme = dr["ThemeName"].ToString();
                    ActThemeColor = dr["ThemeColor"].ToString().Split('/');

                    ActLabelFont = dr["LableFont"].ToString();
                    ActLabelFontSize = float.Parse(dr["LabelSize"].ToString());
                    ActLabelFontStyle = dr["LabelStyle"].ToString();
                    ActLabelForeColor = dr["LabelForeColor"].ToString().Split('/');
                    ActLabelBackColor = dr["LabelBackColor"].ToString().Split('/');

                    ActTextBoxFont = dr["TextBoxFont"].ToString();
                    ActTextBoxFontSize = float.Parse(dr["TextBoxSize"].ToString());
                    ActTextBoxFontStyle = dr["TextBoxStyle"].ToString();
                    ActTextBoxForeColor = dr["TextBoxForeColor"].ToString().Split('/');
                    ActTextBoxBackColor = dr["TextBoxBackColor"].ToString().Split('/');

                    ActComboBoxFont = dr["ComboBoxFont"].ToString();
                    ActComboBoxFontSize = float.Parse(dr["ComboBoxSize"].ToString());
                    ActComboBoxFontStyle = dr["ComboBoxStyle"].ToString();
                    ActComboBoxForeColor = dr["ComboBoxForeColor"].ToString().Split('/');
                    ActComboBoxBackColor = dr["ComboBoxBackColor"].ToString().Split('/');

                    ActButtonFont = dr["ButtonFont"].ToString();
                    ActButtonFontSize = float.Parse(dr["ButtonSize"].ToString());
                    ActButtonFontStyle = dr["ButtonStyle"].ToString();
                    ActButtonForeColor = dr["ButtonForeColor"].ToString().Split('/');
                    ActButtonBackColor = dr["ButtonBackColor"].ToString().Split('/');

                    ActRadioFont = dr["RadioFont"].ToString();
                    ActRadioFontSize = float.Parse(dr["RadioSize"].ToString());
                    ActRadioFontStyle = dr["RadioStyle"].ToString();
                    ActRadioForeColor = dr["RadioForeColor"].ToString().Split('/');
                    ActRadioBackColor = dr["RadioBackColor"].ToString().Split('/');

                    ActCheckBoxFont = dr["CheckBoxFont"].ToString();
                    ActCheckBoxFontSize = float.Parse(dr["CheckBoxSize"].ToString());
                    ActCheckBoxFontStyle = dr["CheckBoxStyle"].ToString();
                    ActCheckBoxForeColor = dr["CheckBoxForeColor"].ToString().Split('/');
                    ActCheckBoxBackColor = dr["CheckBoxBackColor"].ToString().Split('/');

                    ActTabControlFont = dr["TabControlFont"].ToString();
                    ActTabControlFontSize = float.Parse(dr["TabControlSize"].ToString());
                    ActTabControlFontStyle = dr["TabControlStyle"].ToString();
                    ActTabControlForeColor = dr["TabControlForeColor"].ToString().Split('/');
                    ActTabControlBackColor = dr["TabControlBackColor"].ToString().Split('/');

                    ActGroupBoxFont = dr["GroupBoxFont"].ToString();
                    ActGroupBoxFontSize = float.Parse(dr["GroupBoxSize"].ToString());
                    ActGroupBoxFontStyle = dr["GroupBoxStyle"].ToString();
                    ActGroupBoxForeColor = dr["GroupBoxForeColor"].ToString().Split('/');
                    ActGroupBoxBackColor = dr["GroupBoxBackColor"].ToString().Split('/');

                    ActGridFont = dr["GridFont"].ToString();
                    ActGridFontSize = float.Parse(dr["GridSize"].ToString());
                    ActGridFontStyle = dr["GridStyle"].ToString();
                    ActGridFixedForeColor = dr["GridFixedForeColor"].ToString().Split('/');
                    ActGridFixedBackColor = dr["GridFixedBackColor"].ToString().Split('/');
                    ActGridEntryForeColor = dr["GridEntryForeColor"].ToString().Split('/');
                    ActGridEntryBackColor = dr["GridEntryBackColor"].ToString().Split('/');

                    ActPanelBackColor = dr["PanelBackColor"].ToString().Split('/');
                }
            }
        }
        else
        {
            using (IDataReader dr = DB.GetRS(String.Format("Select * from tbl_ThemeSettings Where ThemeName='Default'")))
            {
                while (dr.Read())
                {
                    ActTheme = dr["ThemeName"].ToString();
                    ActThemeColor = dr["ThemeColor"].ToString().Split('/');

                    ActLabelFont = dr["LableFont"].ToString();
                    ActLabelFontSize = float.Parse(dr["LabelSize"].ToString());
                    ActLabelFontStyle = dr["LabelStyle"].ToString();
                    ActLabelForeColor = dr["LabelForeColor"].ToString().Split('/');
                    ActLabelBackColor = dr["LabelBackColor"].ToString().Split('/');

                    ActTextBoxFont = dr["TextBoxFont"].ToString();
                    ActTextBoxFontSize = float.Parse(dr["TextBoxSize"].ToString());
                    ActTextBoxFontStyle = dr["TextBoxStyle"].ToString();
                    ActTextBoxForeColor = dr["TextBoxForeColor"].ToString().Split('/');
                    ActTextBoxBackColor = dr["TextBoxBackColor"].ToString().Split('/');

                    ActComboBoxFont = dr["ComboBoxFont"].ToString();
                    ActComboBoxFontSize = float.Parse(dr["ComboBoxSize"].ToString());
                    ActComboBoxFontStyle = dr["ComboBoxStyle"].ToString();
                    ActComboBoxForeColor = dr["ComboBoxForeColor"].ToString().Split('/');
                    ActComboBoxBackColor = dr["ComboBoxBackColor"].ToString().Split('/');

                    ActButtonFont = dr["ButtonFont"].ToString();
                    ActButtonFontSize = float.Parse(dr["ButtonSize"].ToString());
                    ActButtonFontStyle = dr["ButtonStyle"].ToString();
                    ActButtonForeColor = dr["ButtonForeColor"].ToString().Split('/');
                    ActButtonBackColor = dr["ButtonBackColor"].ToString().Split('/');

                    ActRadioFont = dr["RadioFont"].ToString();
                    ActRadioFontSize = float.Parse(dr["RadioSize"].ToString());
                    ActRadioFontStyle = dr["RadioStyle"].ToString();
                    ActRadioForeColor = dr["RadioForeColor"].ToString().Split('/');
                    ActRadioBackColor = dr["RadioBackColor"].ToString().Split('/');

                    ActCheckBoxFont = dr["CheckBoxFont"].ToString();
                    ActCheckBoxFontSize = float.Parse(dr["CheckBoxSize"].ToString());
                    ActCheckBoxFontStyle = dr["CheckBoxStyle"].ToString();
                    ActCheckBoxForeColor = dr["CheckBoxForeColor"].ToString().Split('/');
                    ActCheckBoxBackColor = dr["CheckBoxBackColor"].ToString().Split('/');

                    ActTabControlFont = dr["TabControlFont"].ToString();
                    ActTabControlFontSize = float.Parse(dr["TabControlSize"].ToString());
                    ActTabControlFontStyle = dr["TabControlStyle"].ToString();
                    ActTabControlForeColor = dr["TabControlForeColor"].ToString().Split('/');
                    ActTabControlBackColor = dr["TabControlBackColor"].ToString().Split('/');

                    ActGroupBoxFont = dr["GroupBoxFont"].ToString();
                    ActGroupBoxFontSize = float.Parse(dr["GroupBoxSize"].ToString());
                    ActGroupBoxFontStyle = dr["GroupBoxStyle"].ToString();
                    ActGroupBoxForeColor = dr["GroupBoxForeColor"].ToString().Split('/');
                    ActGroupBoxBackColor = dr["GroupBoxBackColor"].ToString().Split('/');

                    ActGridFont = dr["GridFont"].ToString();
                    ActGridFontSize = float.Parse(dr["GridSize"].ToString());
                    ActGridFontStyle = dr["GridStyle"].ToString();
                    ActGridFixedForeColor = dr["GridFixedForeColor"].ToString().Split('/');
                    ActGridFixedBackColor = dr["GridFixedBackColor"].ToString().Split('/');
                    ActGridEntryForeColor = dr["GridEntryForeColor"].ToString().Split('/');
                    ActGridEntryBackColor = dr["GridEntryBackColor"].ToString().Split('/');

                    ActPanelBackColor = dr["PanelBackColor"].ToString().Split('/');
                }
            }
        }
    }

    public void SetThemeOnControls(Control frmObj)
    {
        AssignDataToThemeVariables();

        #region Theme

        foreach (Control cntl in frmObj.Controls)
        {
            string[] strVal = cntl.ToString().Split(',');

            #region Control
            switch (strVal[0].ToString())
            {
                case "CIS_CLibrary.CIS_TextLabel":

                    if ((cntl.Name == "lblBorderLeft") || (cntl.Name == "lblBorderRight"))
                    {
                        cntl.BackColor = Color.DodgerBlue;
                    }
                    else
                    {
                        cntl.Font = MakeFont(ActLabelFont, ActLabelFontSize, ActLabelFontStyle);
                        cntl.ForeColor = Color.FromArgb(Localization.ParseNativeInt(ActLabelForeColor[0]), Localization.ParseNativeInt(ActLabelForeColor[1]), Localization.ParseNativeInt(ActLabelForeColor[2]));
                    }
                    break;

                case "CIS_CLibrary.CIS_Textbox":

                    if ((cntl.Name == "lblBorderLeft") || (cntl.Name == "lblBorderRight"))
                    {
                        cntl.BackColor = Color.DodgerBlue;
                    }
                    else
                    {
                        cntl.Font = MakeFont(ActTextBoxFont, ActTextBoxFontSize, ActTextBoxFontStyle);
                        cntl.ForeColor = Color.FromArgb(Localization.ParseNativeInt(ActTextBoxForeColor[0]), Localization.ParseNativeInt(ActTextBoxForeColor[1]), Localization.ParseNativeInt(ActTextBoxForeColor[2]));
                        cntl.BackColor = Color.FromArgb(Localization.ParseNativeInt(ActTextBoxBackColor[0]), Localization.ParseNativeInt(ActTextBoxBackColor[1]), Localization.ParseNativeInt(ActTextBoxBackColor[2]));
                    }
                    break;

                case "CIS_MultiColumnComboBox.CIS_MultiColumnComboBox":

                    if ((cntl.Name == "lblBorderLeft") || (cntl.Name == "lblBorderRight"))
                    {
                        cntl.BackColor = Color.DodgerBlue;
                    }
                    else
                    {
                        cntl.Font = MakeFont(ActComboBoxFont, ActComboBoxFontSize, ActComboBoxFontStyle);
                        cntl.ForeColor = Color.FromArgb(Localization.ParseNativeInt(ActComboBoxForeColor[0]), Localization.ParseNativeInt(ActComboBoxForeColor[1]), Localization.ParseNativeInt(ActComboBoxForeColor[2]));
                        cntl.BackColor = Color.FromArgb(Localization.ParseNativeInt(ActComboBoxBackColor[0]), Localization.ParseNativeInt(ActComboBoxBackColor[1]), Localization.ParseNativeInt(ActComboBoxBackColor[2]));
                    }
                    break;

                case "CIS_CLibrary.CIS_Button":

                    if ((cntl.Name == "lblBorderLeft") || (cntl.Name == "lblBorderRight"))
                    {
                        cntl.BackColor = Color.DodgerBlue;
                    }
                    else
                    {
                        cntl.Font = MakeFont(ActButtonFont, ActButtonFontSize, ActButtonFontStyle);
                        cntl.ForeColor = Color.FromArgb(Localization.ParseNativeInt(ActButtonForeColor[0]), Localization.ParseNativeInt(ActButtonForeColor[1]), Localization.ParseNativeInt(ActButtonForeColor[2]));
                        cntl.BackColor = Color.FromArgb(Localization.ParseNativeInt(ActButtonBackColor[0]), Localization.ParseNativeInt(ActButtonBackColor[1]), Localization.ParseNativeInt(ActButtonBackColor[2]));
                    }
                    break;

                case "CIS_CLibrary.CIS_RadioButton":

                    if ((cntl.Name == "lblBorderLeft") || (cntl.Name == "lblBorderRight"))
                    {
                        cntl.BackColor = Color.DodgerBlue;
                    }
                    else
                    {
                        cntl.Font = MakeFont(ActRadioFont, ActRadioFontSize, ActRadioFontStyle);
                        cntl.ForeColor = Color.FromArgb(Localization.ParseNativeInt(ActRadioForeColor[0]), Localization.ParseNativeInt(ActRadioForeColor[1]), Localization.ParseNativeInt(ActRadioForeColor[2]));
                        cntl.BackColor = Color.FromArgb(Localization.ParseNativeInt(ActRadioBackColor[0]), Localization.ParseNativeInt(ActRadioBackColor[1]), Localization.ParseNativeInt(ActRadioBackColor[2]));
                    }
                    break;

                case "CIS_CLibrary.CIS_CheckBox":

                    if ((cntl.Name == "lblBorderLeft") || (cntl.Name == "lblBorderRight"))
                    {
                        cntl.BackColor = Color.DodgerBlue;
                    }
                    else
                    {
                        cntl.Font = MakeFont(ActCheckBoxFont, ActCheckBoxFontSize, ActCheckBoxFontStyle);
                        cntl.ForeColor = Color.FromArgb(Localization.ParseNativeInt(ActCheckBoxForeColor[0]), Localization.ParseNativeInt(ActCheckBoxForeColor[1]), Localization.ParseNativeInt(ActCheckBoxForeColor[2]));
                        cntl.BackColor = Color.FromArgb(Localization.ParseNativeInt(ActCheckBoxBackColor[0]), Localization.ParseNativeInt(ActCheckBoxBackColor[1]), Localization.ParseNativeInt(ActCheckBoxBackColor[2]));
                    }
                    break;

                case "CIS_CLibrary.CIS_CheckBoxList":

                    if ((cntl.Name == "lblBorderLeft") || (cntl.Name == "lblBorderRight"))
                    {
                        cntl.BackColor = Color.DodgerBlue;
                    }
                    else
                    {
                        cntl.Font = MakeFont(ActCheckBoxFont, ActCheckBoxFontSize, ActCheckBoxFontStyle);
                        cntl.ForeColor = Color.FromArgb(Localization.ParseNativeInt(ActCheckBoxForeColor[0]), Localization.ParseNativeInt(ActCheckBoxForeColor[1]), Localization.ParseNativeInt(ActCheckBoxForeColor[2]));
                        cntl.BackColor = Color.FromArgb(Localization.ParseNativeInt(ActCheckBoxBackColor[0]), Localization.ParseNativeInt(ActCheckBoxBackColor[1]), Localization.ParseNativeInt(ActCheckBoxBackColor[2]));
                    }
                    break;

                case "CIS_CLibrary.CIS_TabControl":

                    if ((cntl.Name == "lblBorderLeft") || (cntl.Name == "lblBorderRight"))
                    {
                        cntl.BackColor = Color.DodgerBlue;
                    }
                    else
                    {
                        cntl.Font = MakeFont(ActTabControlFont, ActTabControlFontSize, ActTabControlFontStyle);
                        cntl.ForeColor = Color.FromArgb(Localization.ParseNativeInt(ActTabControlForeColor[0]), Localization.ParseNativeInt(ActTabControlForeColor[1]), Localization.ParseNativeInt(ActTabControlForeColor[2]));
                        cntl.BackColor = Color.FromArgb(Localization.ParseNativeInt(ActTabControlBackColor[0]), Localization.ParseNativeInt(ActTabControlBackColor[1]), Localization.ParseNativeInt(ActTabControlBackColor[2]));
                    }
                    break;

                case "CIS_GroupBox":
                    if ((cntl.Name == "lblBorderLeft") || (cntl.Name == "lblBorderRight"))
                    {
                        cntl.BackColor = Color.DodgerBlue;
                    }
                    else
                    {
                        cntl.Font = MakeFont(ActGroupBoxFont, ActGroupBoxFontSize, ActGroupBoxFontStyle);
                        cntl.ForeColor = Color.FromArgb(Localization.ParseNativeInt(ActGroupBoxForeColor[0]), Localization.ParseNativeInt(ActGroupBoxForeColor[1]), Localization.ParseNativeInt(ActGroupBoxForeColor[2]));
                        cntl.BackColor = Color.FromArgb(Localization.ParseNativeInt(ActGroupBoxBackColor[0]), Localization.ParseNativeInt(ActGroupBoxBackColor[1]), Localization.ParseNativeInt(ActGroupBoxBackColor[2]));
                    }
                    break;

                case "System.Windows.Forms.Panel":
                    if (cntl.Name == "pnlContent")
                    {
                        cntl.BackColor = Color.MintCream;
                    }
                    else if ((cntl.Name == "pnlCaptionBar") || (cntl.Name == "pnlBottom"))
                        cntl.BackColor = Color.FromArgb(Localization.ParseNativeInt(ActPanelBackColor[0]), Localization.ParseNativeInt(ActPanelBackColor[1]), Localization.ParseNativeInt(ActPanelBackColor[2]));

                    SetThemeOnControls(cntl);
                    break;

                case "CIS_DataGridViewEx.DataGridViewEx":
                    CIS_DataGridViewEx.DataGridViewEx fgdtls = (CIS_DataGridViewEx.DataGridViewEx)cntl;
                    
                    cHead.Font = MakeFont(ActGridFont, ActGridFontSize, ActGridFontStyle);
                    cHead.BackColor = Color.FromArgb(Localization.ParseNativeInt(ActGridFixedBackColor[0]), Localization.ParseNativeInt(ActGridFixedBackColor[1]), Localization.ParseNativeInt(ActGridFixedBackColor[2]));
                    cHead.ForeColor = Color.FromArgb(Localization.ParseNativeInt(ActGridFixedForeColor[0]), Localization.ParseNativeInt(ActGridFixedForeColor[1]), Localization.ParseNativeInt(ActGridFixedForeColor[2]));
                    cHead.SelectionBackColor = Color.FromArgb(Localization.ParseNativeInt(ActGridEntryBackColor[0]), Localization.ParseNativeInt(ActGridEntryBackColor[1]), Localization.ParseNativeInt(ActGridEntryBackColor[2]));
                    cHead.SelectionForeColor = Color.FromArgb(Localization.ParseNativeInt(ActGridEntryForeColor[0]), Localization.ParseNativeInt(ActGridEntryForeColor[1]), Localization.ParseNativeInt(ActGridEntryForeColor[2]));

                    cHead.Font = MakeFont(ActGridFont, ActGridFontSize, ActGridFontStyle);
                    altRows.BackColor = Color.FromArgb(Localization.ParseNativeInt(ActGridFixedBackColor[0]), Localization.ParseNativeInt(ActGridFixedBackColor[1]), Localization.ParseNativeInt(ActGridFixedBackColor[2]));
                    altRows.ForeColor = Color.FromArgb(Localization.ParseNativeInt(ActGridFixedForeColor[0]), Localization.ParseNativeInt(ActGridFixedForeColor[1]), Localization.ParseNativeInt(ActGridFixedForeColor[2]));
                    altRows.SelectionBackColor = Color.FromArgb(Localization.ParseNativeInt(ActGridEntryBackColor[0]), Localization.ParseNativeInt(ActGridEntryBackColor[1]), Localization.ParseNativeInt(ActGridEntryBackColor[2]));
                    altRows.SelectionForeColor = Color.FromArgb(Localization.ParseNativeInt(ActGridEntryForeColor[0]), Localization.ParseNativeInt(ActGridEntryForeColor[1]), Localization.ParseNativeInt(ActGridEntryForeColor[2]));

                    fgdtls.ColumnHeadersDefaultCellStyle = cHead;
                    fgdtls.AlternatingRowsDefaultCellStyle = altRows;
                    fgdtls.ColumnHeadersHeight = 30;
                    foreach (DataGridViewColumn col in fgdtls.Columns)
                        col.SortMode = DataGridViewColumnSortMode.NotSortable;
                    fgdtls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                    break;
            }
            #endregion
        }
        #endregion

    }

    public void SetThemeOnMDI(Control frmObj)
    {
        foreach (Control cntl in frmObj.Controls)
        {
            string[] srtVal = cntl.ToString().Split(',');
            switch (srtVal[0].ToString())
            {
                case "CIS_CLibrary.CIS_Panel":
                    #region CIS_Panel
                    {
                        CIS_CLibrary.CIS_Panel cpnl = (CIS_CLibrary.CIS_Panel)cntl;
                        switch (cntl.Name)
                        {
                            case "pnlDockTop":
                                cpnl.CustomColors.CaptionGradientBegin = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));;
                                cpnl.CustomColors.CaptionGradientEnd = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                                cpnl.CustomColors.CaptionGradientMiddle = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                                SetThemeOnMDI(cpnl);
                                break;

                            case "pnlNavigation":
                                cpnl.CustomColors.ContentGradientBegin = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                                cpnl.CustomColors.ContentGradientEnd = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                                break;

                            default:
                                cpnl.CustomColors.CaptionGradientBegin = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                                cpnl.CustomColors.CaptionGradientEnd = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                                cpnl.CustomColors.CaptionGradientMiddle = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                                cpnl.CustomColors.ContentGradientBegin = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                                cpnl.CustomColors.ContentGradientEnd = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                                cpnl.CustomColors.CaptionSelectedGradientBegin = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                                cpnl.CustomColors.CaptionSelectedGradientEnd = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                                cpnl.CustomColors.InnerBorderColor = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                                SetThemeOnMDI(cpnl);
                                break;
                        }
                    }
                    #endregion
                    break;

                case "CIS_CLibrary.CIS_XPanderPanelList":
                    SetThemeOnMDI(cntl);
                    break;

                case "System.Windows.Forms.StatusStrip":
                    System.Windows.Forms.StatusStrip stastrip = (System.Windows.Forms.StatusStrip)cntl;
                    stastrip.BackColor = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                    break;

                case "CIS_CLibrary.CIS_XPanderPanel":
                    CIS_CLibrary.CIS_XPanderPanel xpp = (CIS_CLibrary.CIS_XPanderPanel)cntl;
                    xpp.CustomColors.BackColor = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                    xpp.CustomColors.BorderColor = Color.Black;
                    xpp.CustomColors.CaptionCheckedGradientBegin = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                    xpp.CustomColors.CaptionCheckedGradientEnd = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                    xpp.CustomColors.CaptionCheckedGradientMiddle = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                    xpp.CustomColors.CaptionCloseIcon = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                    xpp.CustomColors.CaptionExpandIcon = Color.White;
                    xpp.CustomColors.CaptionGradientBegin = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                    xpp.CustomColors.CaptionGradientEnd = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                    xpp.CustomColors.CaptionGradientMiddle = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                    xpp.CustomColors.CaptionPressedGradientBegin = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                    xpp.CustomColors.CaptionPressedGradientEnd = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                    xpp.CustomColors.CaptionPressedGradientMiddle = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                    xpp.CustomColors.CaptionSelectedGradientBegin = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                    xpp.CustomColors.CaptionSelectedGradientEnd = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                    xpp.CustomColors.CaptionSelectedGradientMiddle = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                    xpp.CustomColors.CaptionSelectedText = Color.Black;
                    xpp.CustomColors.CaptionText = Color.White;
                    xpp.CustomColors.FlatCaptionGradientBegin = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                    xpp.CustomColors.FlatCaptionGradientEnd = Color.FromArgb(Localization.ParseNativeInt(ActThemeColor[0]), Localization.ParseNativeInt(ActThemeColor[1]), Localization.ParseNativeInt(ActThemeColor[2]));
                    xpp.CustomColors.InnerBorderColor = Color.Black;
                    break;

                case "CIS_CLibrary.CIS_TextLabel":
                    if (cntl.Name == "lblQuickMenu")
                    {
                        CIS_CLibrary.CIS_TextLabel lbl = (CIS_CLibrary.CIS_TextLabel)cntl;
                        lbl.ForeColor = Color.White;
                    }
                    break;

            }
        }
    }

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

    #endregion

    #region Old Theme

    public void SetThemeOnControls(Control frmObj, ThemeName tnm)
    {
        switch (tnm)
        {
            #region BLUE

            case ThemeName.Blue:
                foreach (Control cntl in frmObj.Controls)
                {
                    string[] strVal = cntl.ToString().Split(',');

                    #region Control
                    switch (strVal[0].ToString())
                    {
                        case "CIS_CLibrary.CIS_TextLabel":

                            if ((cntl.Name == "lblBorderLeft") || (cntl.Name == "lblBorderRight"))
                                cntl.BackColor = Color.CornflowerBlue;
                            else
                                cntl.ForeColor = Color.Black;
                            break;

                        case "System.Windows.Forms.Panel":
                            if (cntl.Name == "pnlContent")
                            {
                                cntl.BackColor = Color.LightBlue;
                            }
                            else if ((cntl.Name == "pnlCaptionBar") || (cntl.Name == "pnlBottom"))
                                cntl.BackColor = Color.CornflowerBlue;

                            SetThemeOnControls(cntl, tnm);
                            break;

                        case "CIS_DataGridViewEx.DataGridViewEx":
                            CIS_DataGridViewEx.DataGridViewEx fgdtls = (CIS_DataGridViewEx.DataGridViewEx)cntl;
                            cHead.BackColor = System.Drawing.Color.LightBlue;
                            cHead.ForeColor = System.Drawing.SystemColors.WindowText;
                            cHead.SelectionBackColor = System.Drawing.Color.LightBlue;
                            cHead.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

                            altRows.BackColor = System.Drawing.Color.Lavender;
                            altRows.ForeColor = System.Drawing.SystemColors.WindowText;
                            altRows.SelectionBackColor = System.Drawing.Color.LightBlue;
                            altRows.SelectionForeColor = System.Drawing.Color.Black;

                            fgdtls.ColumnHeadersDefaultCellStyle = cHead;
                            fgdtls.AlternatingRowsDefaultCellStyle = altRows;

                            foreach (DataGridViewColumn col in fgdtls.Columns)
                                col.SortMode = DataGridViewColumnSortMode.NotSortable;

                            fgdtls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                            break;
                    }
                    #endregion
                }
                break;

            #endregion

            #region GREY

            case ThemeName.Gray:
                foreach (Control cntl in frmObj.Controls)
                {
                    string[] strVal = cntl.ToString().Split(',');

                    #region Control
                    switch (strVal[0].ToString())
                    {
                        case "CIS_CLibrary.CIS_TextLabel":

                            if ((cntl.Name == "lblBorderLeft") || (cntl.Name == "lblBorderRight"))
                                cntl.BackColor = Color.DimGray;
                            else
                                cntl.ForeColor = Color.Black;
                            break;

                        case "System.Windows.Forms.Panel":
                            if (cntl.Name == "pnlContent")
                            {
                                cntl.BackColor = Color.WhiteSmoke;
                            }
                            else if ((cntl.Name == "pnlCaptionBar") || (cntl.Name == "pnlBottom"))
                                cntl.BackColor = Color.DimGray;

                            SetThemeOnControls(cntl, tnm);
                            break;

                        case "CIS_DataGridViewEx.DataGridViewEx":
                            CIS_DataGridViewEx.DataGridViewEx fgdtls = (CIS_DataGridViewEx.DataGridViewEx)cntl;
                            cHead.BackColor = System.Drawing.Color.DimGray;
                            cHead.ForeColor = System.Drawing.SystemColors.WindowText;
                            cHead.SelectionBackColor = System.Drawing.Color.DimGray;
                            cHead.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

                            altRows.BackColor = System.Drawing.Color.WhiteSmoke;
                            altRows.ForeColor = System.Drawing.SystemColors.WindowText;
                            altRows.SelectionBackColor = System.Drawing.Color.WhiteSmoke;
                            altRows.SelectionForeColor = System.Drawing.Color.Black;

                            fgdtls.ColumnHeadersDefaultCellStyle = cHead;
                            fgdtls.AlternatingRowsDefaultCellStyle = altRows;
                            foreach (DataGridViewColumn col in fgdtls.Columns)
                                col.SortMode = DataGridViewColumnSortMode.NotSortable;

                            fgdtls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                            break;
                    }
                    #endregion
                }
                break;

            #endregion

            #region ORANGE

            case ThemeName.Orange:
                foreach (Control cntl in frmObj.Controls)
                {
                    string[] strVal = cntl.ToString().Split(',');

                    #region Control
                    switch (strVal[0].ToString())
                    {
                        case "CIS_CLibrary.CIS_TextLabel":

                            if ((cntl.Name == "lblBorderLeft") || (cntl.Name == "lblBorderRight"))
                                cntl.BackColor = Color.Coral;
                            else
                                cntl.ForeColor = Color.Black;
                            break;

                        case "System.Windows.Forms.Panel":
                            if (cntl.Name == "pnlContent")
                            {
                                cntl.BackColor = Color.SeaShell;

                            }
                            else if ((cntl.Name == "pnlCaptionBar") || (cntl.Name == "pnlBottom"))
                                cntl.BackColor = Color.Coral;

                            SetThemeOnControls(cntl, tnm);
                            break;

                        case "CIS_DataGridViewEx.DataGridViewEx":
                            CIS_DataGridViewEx.DataGridViewEx fgdtls = (CIS_DataGridViewEx.DataGridViewEx)cntl;
                            cHead.BackColor = System.Drawing.Color.Coral;
                            cHead.ForeColor = System.Drawing.SystemColors.WindowText;
                            cHead.SelectionBackColor = System.Drawing.Color.Coral;
                            cHead.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

                            altRows.BackColor = System.Drawing.Color.Cornsilk;
                            altRows.ForeColor = System.Drawing.SystemColors.WindowText;
                            altRows.SelectionBackColor = System.Drawing.Color.Coral;
                            altRows.SelectionForeColor = System.Drawing.Color.Black;

                            fgdtls.ColumnHeadersDefaultCellStyle = cHead;
                            fgdtls.AlternatingRowsDefaultCellStyle = altRows;
                            foreach (DataGridViewColumn col in fgdtls.Columns)
                                col.SortMode = DataGridViewColumnSortMode.NotSortable;

                            fgdtls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                            break;
                    }
                    #endregion
                }
                break;

            #endregion

            #region DEFAULT

            case ThemeName.Default:

                foreach (Control cntl in frmObj.Controls)
                {
                    string[] strVal = cntl.ToString().Split(',');

                    #region Control
                    switch (strVal[0].ToString())
                    {
                        case "CIS_CLibrary.CIS_TextLabel":

                            if ((cntl.Name == "lblBorderLeft") || (cntl.Name == "lblBorderRight"))
                                cntl.BackColor = Color.DodgerBlue;
                            else
                                cntl.ForeColor = Color.Black;
                            break;

                        case "System.Windows.Forms.Panel":
                            if (cntl.Name == "pnlContent")
                            {
                                cntl.BackColor = Color.MintCream;

                            }
                            else if ((cntl.Name == "pnlCaptionBar") || (cntl.Name == "pnlBottom"))
                                cntl.BackColor = Color.DodgerBlue;

                            SetThemeOnControls(cntl, tnm);
                            break;

                        case "CIS_DataGridViewEx.DataGridViewEx":
                            CIS_DataGridViewEx.DataGridViewEx fgdtls = (CIS_DataGridViewEx.DataGridViewEx)cntl;
                            cHead.BackColor = System.Drawing.Color.DodgerBlue;
                            cHead.ForeColor = System.Drawing.SystemColors.WindowText;
                            cHead.SelectionBackColor = System.Drawing.Color.Purple;
                            cHead.SelectionForeColor = System.Drawing.SystemColors.HighlightText;

                            altRows.BackColor = System.Drawing.Color.LightSkyBlue;
                            altRows.ForeColor = System.Drawing.SystemColors.WindowText;
                            altRows.SelectionBackColor = System.Drawing.Color.Purple;
                            altRows.SelectionForeColor = System.Drawing.Color.Black;

                            fgdtls.ColumnHeadersDefaultCellStyle = cHead;
                            fgdtls.AlternatingRowsDefaultCellStyle = altRows;
                            fgdtls.ColumnHeadersHeight = 30;
                            foreach (DataGridViewColumn col in fgdtls.Columns)
                                col.SortMode = DataGridViewColumnSortMode.NotSortable;
                            fgdtls.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

                            break;
                    }
                    #endregion
                }
                break;

            #endregion
        }
    }

    public void SetThemeOnMDI(Control frmObj, ThemeName tnm)
    {
        switch (tnm)
        {
            #region BLUE

            case ThemeName.Blue:
                {
                    foreach (Control cntl in frmObj.Controls)
                    {
                        string[] srtVal = cntl.ToString().Split(',');
                        switch (srtVal[0].ToString())
                        {
                            case "CIS_CLibrary.CIS_Panel":
                                #region CIS_Panel
                                {
                                    CIS_CLibrary.CIS_Panel cpnl = (CIS_CLibrary.CIS_Panel)cntl;
                                    switch (cntl.Name)
                                    {
                                        case "pnlDockTop":
                                            cpnl.CustomColors.CaptionGradientBegin = Color.SteelBlue;
                                            cpnl.CustomColors.CaptionGradientEnd = Color.SteelBlue;
                                            cpnl.CustomColors.CaptionGradientMiddle = Color.SteelBlue;
                                            SetThemeOnMDI(cpnl, tnm);
                                            break;

                                        case "pnlNavigation":
                                            cpnl.CustomColors.ContentGradientBegin = Color.Lavender;
                                            cpnl.CustomColors.ContentGradientEnd = Color.Lavender;
                                            break;

                                        default:
                                            cpnl.CustomColors.CaptionGradientBegin = Color.DodgerBlue;
                                            cpnl.CustomColors.CaptionGradientEnd = Color.DodgerBlue;
                                            cpnl.CustomColors.CaptionGradientMiddle = Color.DodgerBlue;
                                            cpnl.CustomColors.ContentGradientBegin = Color.DodgerBlue;
                                            cpnl.CustomColors.ContentGradientEnd = Color.DodgerBlue;
                                            cpnl.CustomColors.CaptionSelectedGradientBegin = Color.DodgerBlue;
                                            cpnl.CustomColors.CaptionSelectedGradientEnd = Color.DodgerBlue;
                                            cpnl.CustomColors.InnerBorderColor = Color.DodgerBlue;
                                            SetThemeOnMDI(cpnl, tnm);
                                            break;
                                    }
                                }
                                #endregion
                                break;

                            case "CIS_CLibrary.CIS_XPanderPanelList":
                                SetThemeOnMDI(cntl, tnm);
                                break;

                            case "System.Windows.Forms.StatusStrip":
                                System.Windows.Forms.StatusStrip stastrip = (System.Windows.Forms.StatusStrip)cntl;
                                stastrip.BackColor = Color.SteelBlue;
                                break;

                            case "CIS_CLibrary.CIS_XPanderPanel":
                                CIS_CLibrary.CIS_XPanderPanel xpp = (CIS_CLibrary.CIS_XPanderPanel)cntl;
                                xpp.CustomColors.BackColor = Color.Lavender;
                                xpp.CustomColors.BorderColor = Color.Black;
                                xpp.CustomColors.CaptionCheckedGradientBegin = Color.CornflowerBlue;
                                xpp.CustomColors.CaptionCheckedGradientEnd = Color.CornflowerBlue;
                                xpp.CustomColors.CaptionCheckedGradientMiddle = Color.CornflowerBlue;
                                xpp.CustomColors.CaptionCloseIcon = Color.RoyalBlue;
                                xpp.CustomColors.CaptionExpandIcon = Color.White;
                                xpp.CustomColors.CaptionGradientBegin = Color.DodgerBlue;
                                xpp.CustomColors.CaptionGradientEnd = Color.DodgerBlue;
                                xpp.CustomColors.CaptionGradientMiddle = Color.DodgerBlue;
                                xpp.CustomColors.CaptionPressedGradientBegin = Color.LightBlue;
                                xpp.CustomColors.CaptionPressedGradientEnd = Color.LightBlue;
                                xpp.CustomColors.CaptionPressedGradientMiddle = Color.LightBlue;
                                xpp.CustomColors.CaptionSelectedGradientBegin = Color.AliceBlue;
                                xpp.CustomColors.CaptionSelectedGradientEnd = Color.AliceBlue;
                                xpp.CustomColors.CaptionSelectedGradientMiddle = Color.AliceBlue;
                                xpp.CustomColors.CaptionSelectedText = Color.Black;
                                xpp.CustomColors.CaptionText = Color.White;
                                xpp.CustomColors.FlatCaptionGradientBegin = Color.CornflowerBlue;
                                xpp.CustomColors.FlatCaptionGradientEnd = Color.CornflowerBlue;
                                xpp.CustomColors.InnerBorderColor = Color.Black;
                                break;

                            case "CIS_CLibrary.CIS_TextLabel":
                                if (cntl.Name == "lblQuickMenu")
                                {
                                    CIS_CLibrary.CIS_TextLabel lbl = (CIS_CLibrary.CIS_TextLabel)cntl;
                                    lbl.ForeColor = Color.White;
                                }
                                break;

                        }
                    }
                }
                break;

            #endregion

            #region GREY

            case ThemeName.Gray:
                {
                    foreach (Control cntl in frmObj.Controls)
                    {
                        string[] strVal = cntl.ToString().Split(',');

                        switch (strVal[0].ToString())
                        {
                            case "CIS_CLibrary.CIS_Panel":
                                #region CIS_Panel
                                CIS_CLibrary.CIS_Panel cpnl = (CIS_CLibrary.CIS_Panel)cntl;
                                switch (cntl.Name)
                                {
                                    case "pnlDockTop":
                                        cpnl.CustomColors.CaptionGradientBegin = Color.Silver;
                                        cpnl.CustomColors.CaptionGradientEnd = Color.Silver;
                                        cpnl.CustomColors.CaptionGradientMiddle = Color.Silver;
                                        SetThemeOnMDI(cpnl, tnm);
                                        break;

                                    case "pnlNavigation":
                                        cpnl.CustomColors.ContentGradientBegin = Color.WhiteSmoke;
                                        cpnl.CustomColors.ContentGradientEnd = Color.WhiteSmoke;
                                        break;

                                    default:
                                        cpnl.CustomColors.CaptionGradientBegin = Color.DimGray;
                                        cpnl.CustomColors.CaptionGradientEnd = Color.DimGray;
                                        cpnl.CustomColors.CaptionGradientMiddle = Color.DimGray;
                                        cpnl.CustomColors.ContentGradientBegin = Color.DimGray;
                                        cpnl.CustomColors.ContentGradientEnd = Color.DimGray;
                                        cpnl.CustomColors.CaptionSelectedGradientBegin = Color.DimGray;
                                        cpnl.CustomColors.CaptionSelectedGradientEnd = Color.DimGray;
                                        cpnl.CustomColors.InnerBorderColor = Color.DimGray;
                                        SetThemeOnMDI(cpnl, tnm);
                                        break;
                                }
                                #endregion
                                break;

                            case "CIS_CLibrary.CIS_XPanderPanelList":
                                SetThemeOnMDI(cntl, tnm);
                                break;

                            case "System.Windows.Forms.StatusStrip":
                                System.Windows.Forms.StatusStrip stastrip = (System.Windows.Forms.StatusStrip)cntl;
                                stastrip.BackColor = Color.Silver;
                                break;

                            case "CIS_CLibrary.CIS_XPanderPanel":
                                CIS_CLibrary.CIS_XPanderPanel xpp = (CIS_CLibrary.CIS_XPanderPanel)cntl;
                                xpp.CustomColors.BackColor = Color.WhiteSmoke;
                                xpp.CustomColors.BorderColor = Color.White;
                                xpp.CustomColors.CaptionCheckedGradientBegin = Color.DimGray;
                                xpp.CustomColors.CaptionCheckedGradientEnd = Color.DimGray;
                                xpp.CustomColors.CaptionCheckedGradientMiddle = Color.DimGray;
                                xpp.CustomColors.CaptionCloseIcon = Color.MistyRose;
                                xpp.CustomColors.CaptionExpandIcon = Color.White;
                                xpp.CustomColors.CaptionGradientBegin = Color.Gray;
                                xpp.CustomColors.CaptionGradientEnd = Color.Gray;
                                xpp.CustomColors.CaptionGradientMiddle = Color.Gray;
                                xpp.CustomColors.CaptionPressedGradientBegin = Color.DarkGray;
                                xpp.CustomColors.CaptionPressedGradientEnd = Color.DarkGray;
                                xpp.CustomColors.CaptionPressedGradientMiddle = Color.DarkGray;
                                xpp.CustomColors.CaptionSelectedGradientBegin = Color.WhiteSmoke;
                                xpp.CustomColors.CaptionSelectedGradientEnd = Color.WhiteSmoke;
                                xpp.CustomColors.CaptionSelectedGradientMiddle = Color.WhiteSmoke;
                                xpp.CustomColors.CaptionSelectedText = Color.Black;
                                xpp.CustomColors.CaptionText = Color.Black;
                                xpp.CustomColors.FlatCaptionGradientBegin = Color.Silver;
                                xpp.CustomColors.FlatCaptionGradientEnd = Color.Silver;
                                xpp.CustomColors.InnerBorderColor = Color.White;
                                break;

                            case "CIS_CLibrary.CIS_TextLabel":
                                if (cntl.Name == "lblQuickMenu")
                                {
                                    CIS_CLibrary.CIS_TextLabel lbl = (CIS_CLibrary.CIS_TextLabel)cntl;
                                    lbl.ForeColor = Color.Black;
                                }
                                break;
                        }
                    }
                }
                break;

            #endregion

            #region ORANGE

            case ThemeName.Orange:
                foreach (Control cntl in frmObj.Controls)
                {
                    string[] strVal = cntl.ToString().Split(',');
                    switch (strVal[0].ToString())
                    {
                        case "CIS_CLibrary.CIS_Panel":
                            CIS_CLibrary.CIS_Panel cpnl = (CIS_CLibrary.CIS_Panel)cntl;
                            #region CIS_Panel
                            switch (cntl.Name)
                            {


                                case "pnlDockTop":
                                    cpnl.CustomColors.CaptionGradientBegin = Color.Coral;
                                    cpnl.CustomColors.CaptionGradientEnd = Color.Coral;
                                    cpnl.CustomColors.CaptionGradientMiddle = Color.Coral;
                                    SetThemeOnMDI(cpnl, tnm);
                                    break;

                                case "pnlNavigation":
                                    cpnl.CustomColors.ContentGradientBegin = Color.Cornsilk;
                                    cpnl.CustomColors.ContentGradientEnd = Color.Cornsilk;
                                    break;

                                default:
                                    cpnl.CustomColors.CaptionGradientBegin = Color.OrangeRed;
                                    cpnl.CustomColors.CaptionGradientEnd = Color.OrangeRed;
                                    cpnl.CustomColors.CaptionGradientMiddle = Color.OrangeRed;
                                    cpnl.CustomColors.ContentGradientBegin = Color.OrangeRed;
                                    cpnl.CustomColors.ContentGradientEnd = Color.OrangeRed;
                                    cpnl.CustomColors.CaptionSelectedGradientBegin = Color.OrangeRed;
                                    cpnl.CustomColors.CaptionSelectedGradientEnd = Color.OrangeRed;
                                    cpnl.CustomColors.InnerBorderColor = Color.OrangeRed;
                                    SetThemeOnMDI(cpnl, tnm);
                                    break;


                            }
                            #endregion
                            break;

                        case "CIS_CLibrary.CIS_XPanderPanelList":
                            SetThemeOnMDI(cntl, tnm);
                            break;

                        case "System.Windows.Forms.StatusStrip":
                            System.Windows.Forms.StatusStrip stastrip = (System.Windows.Forms.StatusStrip)cntl;
                            stastrip.BackColor = Color.Coral;
                            break;

                        case "CIS_CLibrary.CIS_XPanderPanel":
                            CIS_CLibrary.CIS_XPanderPanel xpp = (CIS_CLibrary.CIS_XPanderPanel)cntl;
                            xpp.CustomColors.BackColor = Color.Cornsilk;
                            xpp.CustomColors.BorderColor = Color.Snow;
                            xpp.CustomColors.CaptionCheckedGradientBegin = Color.DarkSalmon;
                            xpp.CustomColors.CaptionCheckedGradientEnd = Color.DarkSalmon;
                            xpp.CustomColors.CaptionCheckedGradientMiddle = Color.DarkSalmon;
                            xpp.CustomColors.CaptionCloseIcon = Color.Gold;
                            xpp.CustomColors.CaptionExpandIcon = Color.White;
                            xpp.CustomColors.CaptionGradientBegin = Color.Coral;
                            xpp.CustomColors.CaptionGradientEnd = Color.Coral;
                            xpp.CustomColors.CaptionGradientMiddle = Color.Coral;
                            xpp.CustomColors.CaptionPressedGradientBegin = Color.MistyRose;
                            xpp.CustomColors.CaptionPressedGradientEnd = Color.MistyRose;
                            xpp.CustomColors.CaptionPressedGradientMiddle = Color.MistyRose;
                            xpp.CustomColors.CaptionSelectedGradientBegin = Color.Snow;
                            xpp.CustomColors.CaptionSelectedGradientEnd = Color.Snow;
                            xpp.CustomColors.CaptionSelectedGradientMiddle = Color.Snow;
                            xpp.CustomColors.CaptionSelectedText = Color.Black;
                            xpp.CustomColors.CaptionText = Color.White;
                            xpp.CustomColors.FlatCaptionGradientBegin = Color.Coral;
                            xpp.CustomColors.FlatCaptionGradientEnd = Color.Coral;
                            xpp.CustomColors.InnerBorderColor = Color.Snow;
                            break;

                        case "CIS_CLibrary.CIS_TextLabel":
                            if (cntl.Name == "lblQuickMenu")
                            {
                                CIS_CLibrary.CIS_TextLabel lbl = (CIS_CLibrary.CIS_TextLabel)cntl;
                                lbl.ForeColor = Color.White;
                            }
                            break;
                    }
                }
                break;

            #endregion

            #region DEFAULT

            case ThemeName.Default:
                foreach (Control cntl in frmObj.Controls)
                {
                    string[] srtVal = cntl.ToString().Split(',');

                    switch (srtVal[0].ToString())
                    {
                        case "CIS_CLibrary.CIS_Panel":
                            #region CIS_Panel
                            CIS_CLibrary.CIS_Panel cpnl = (CIS_CLibrary.CIS_Panel)cntl;
                            switch (cntl.Name)
                            {
                                case "pnlDockTop":
                                    cpnl.CustomColors.CaptionGradientBegin = Color.DarkCyan;
                                    cpnl.CustomColors.CaptionGradientEnd = Color.DarkCyan;
                                    cpnl.CustomColors.CaptionGradientMiddle = Color.DarkCyan;
                                    SetThemeOnMDI(cpnl, tnm);
                                    break;

                                case "pnlDockBottom":
                                    cpnl.CustomColors.CaptionGradientBegin = Color.DimGray;
                                    cpnl.CustomColors.CaptionGradientEnd = Color.DimGray;
                                    cpnl.CustomColors.CaptionGradientMiddle = Color.DimGray;
                                    break;

                                case "pnlNavigation":
                                    cpnl.CustomColors.ContentGradientBegin = Color.LightSkyBlue;
                                    cpnl.CustomColors.ContentGradientEnd = Color.LightSkyBlue;
                                    break;

                                default:
                                    cpnl.CustomColors.CaptionGradientBegin = Color.DarkTurquoise;
                                    cpnl.CustomColors.CaptionGradientEnd = Color.DarkTurquoise;
                                    cpnl.CustomColors.CaptionGradientMiddle = Color.DarkTurquoise;

                                    cpnl.CustomColors.ContentGradientBegin = Color.DarkCyan;
                                    cpnl.CustomColors.ContentGradientEnd = Color.DarkCyan;

                                    cpnl.CustomColors.CaptionSelectedGradientBegin = Color.DarkTurquoise;
                                    cpnl.CustomColors.CaptionSelectedGradientEnd = Color.DarkTurquoise;
                                    cpnl.CustomColors.InnerBorderColor = Color.DarkTurquoise;
                                    SetThemeOnMDI(cpnl, tnm);
                                    break;
                            }
                            #endregion
                            break;

                        case "CIS_CLibrary.CIS_XPanderPanelList":
                            SetThemeOnMDI(cntl, tnm);
                            break;

                        case "System.Windows.Forms.StatusStrip":
                            System.Windows.Forms.StatusStrip stastrip = (System.Windows.Forms.StatusStrip)cntl;
                            stastrip.BackColor = Color.Crimson;
                            break;

                        case "CIS_CLibrary.CIS_XPanderPanel":
                            #region XpanderPanel
                            CIS_CLibrary.CIS_XPanderPanel xpp = (CIS_CLibrary.CIS_XPanderPanel)cntl;
                            switch (cntl.Name)
                            {
                                case "pnlXPanderMenu":
                                    xpp.CustomColors.BorderColor = Color.DarkGray;
                                    xpp.CustomColors.CaptionCheckedGradientBegin = Color.Green;
                                    xpp.CustomColors.CaptionCheckedGradientEnd = Color.Green;
                                    xpp.CustomColors.CaptionCheckedGradientMiddle = Color.Green;
                                    xpp.CustomColors.CaptionCloseIcon = Color.DarkCyan;
                                    xpp.CustomColors.CaptionExpandIcon = Color.White;
                                    xpp.CustomColors.CaptionGradientBegin = Color.DarkSeaGreen;
                                    xpp.CustomColors.CaptionGradientEnd = Color.DarkSeaGreen;
                                    xpp.CustomColors.CaptionGradientMiddle = Color.DarkSeaGreen;
                                    xpp.CustomColors.CaptionPressedGradientBegin = Color.LightSkyBlue;
                                    xpp.CustomColors.CaptionPressedGradientEnd = Color.LightSkyBlue;
                                    xpp.CustomColors.CaptionPressedGradientMiddle = Color.LightSkyBlue;
                                    xpp.CustomColors.CaptionSelectedGradientBegin = Color.LightBlue;
                                    xpp.CustomColors.CaptionSelectedGradientEnd = Color.LightBlue;
                                    xpp.CustomColors.CaptionSelectedGradientMiddle = Color.LightBlue;
                                    xpp.CustomColors.CaptionSelectedText = Color.Black;
                                    xpp.CustomColors.CaptionText = Color.White;
                                    xpp.CustomColors.FlatCaptionGradientBegin = Color.DarkSeaGreen;
                                    xpp.CustomColors.FlatCaptionGradientEnd = Color.DarkSeaGreen;
                                    xpp.CustomColors.InnerBorderColor = Color.White;
                                    break;

                                case "pnlxPanderContacts":
                                    xpp.CustomColors.BorderColor = Color.DarkGray;
                                    xpp.CustomColors.CaptionCheckedGradientBegin = Color.DarkSlateBlue;
                                    xpp.CustomColors.CaptionCheckedGradientEnd = Color.DarkSlateBlue;
                                    xpp.CustomColors.CaptionCheckedGradientMiddle = Color.LightSkyBlue;
                                    xpp.CustomColors.CaptionCloseIcon = Color.Black;
                                    xpp.CustomColors.CaptionExpandIcon = Color.Black;
                                    xpp.CustomColors.CaptionGradientBegin = Color.White;
                                    xpp.CustomColors.CaptionGradientEnd = Color.MintCream;
                                    xpp.CustomColors.CaptionGradientMiddle = Color.White;
                                    xpp.CustomColors.CaptionPressedGradientBegin = Color.LightSkyBlue;
                                    xpp.CustomColors.CaptionPressedGradientEnd = Color.LightSkyBlue;
                                    xpp.CustomColors.CaptionPressedGradientMiddle = Color.LightSkyBlue;
                                    xpp.CustomColors.CaptionSelectedGradientBegin = Color.DarkSlateBlue;
                                    xpp.CustomColors.CaptionSelectedGradientEnd = Color.DarkSlateGray;
                                    xpp.CustomColors.CaptionSelectedGradientMiddle = Color.DarkSlateBlue;
                                    xpp.CustomColors.CaptionSelectedText = Color.Black;
                                    xpp.CustomColors.CaptionText = Color.White;
                                    xpp.CustomColors.FlatCaptionGradientBegin = Color.DarkSlateBlue;
                                    xpp.CustomColors.FlatCaptionGradientEnd = Color.DarkSlateBlue;
                                    xpp.CustomColors.InnerBorderColor = Color.White;
                                    break;

                                case "xPanderCalender":
                                    xpp.CustomColors.BorderColor = Color.DarkGray;
                                    xpp.CustomColors.CaptionCheckedGradientBegin = Color.MediumVioletRed;
                                    xpp.CustomColors.CaptionCheckedGradientEnd = Color.MediumVioletRed;
                                    xpp.CustomColors.CaptionCheckedGradientMiddle = Color.MediumVioletRed;
                                    xpp.CustomColors.CaptionCloseIcon = Color.White;
                                    xpp.CustomColors.CaptionExpandIcon = Color.Black;
                                    xpp.CustomColors.CaptionGradientBegin = Color.White;
                                    xpp.CustomColors.CaptionGradientEnd = Color.MintCream;
                                    xpp.CustomColors.CaptionGradientMiddle = Color.White;
                                    xpp.CustomColors.CaptionPressedGradientBegin = Color.LightSkyBlue;
                                    xpp.CustomColors.CaptionPressedGradientEnd = Color.LightSkyBlue;
                                    xpp.CustomColors.CaptionPressedGradientMiddle = Color.LightSkyBlue;
                                    xpp.CustomColors.CaptionSelectedGradientBegin = Color.LightBlue;
                                    xpp.CustomColors.CaptionSelectedGradientEnd = Color.LightBlue;
                                    xpp.CustomColors.CaptionSelectedGradientMiddle = Color.LightBlue;
                                    xpp.CustomColors.CaptionSelectedText = Color.Black;
                                    xpp.CustomColors.CaptionText = Color.White;
                                    xpp.CustomColors.FlatCaptionGradientBegin = Color.MediumVioletRed;
                                    xpp.CustomColors.FlatCaptionGradientEnd = Color.MediumVioletRed;
                                    xpp.CustomColors.InnerBorderColor = Color.White;
                                    break;

                                case "xPanderToDoList":
                                    xpp.CustomColors.BorderColor = Color.DarkGray;
                                    xpp.CustomColors.CaptionCheckedGradientBegin = Color.Orange;
                                    xpp.CustomColors.CaptionCheckedGradientEnd = Color.Orange;
                                    xpp.CustomColors.CaptionCheckedGradientMiddle = Color.Orange;
                                    xpp.CustomColors.CaptionCloseIcon = Color.Black;
                                    xpp.CustomColors.CaptionExpandIcon = Color.Black;
                                    xpp.CustomColors.CaptionGradientBegin = Color.White;
                                    xpp.CustomColors.CaptionGradientEnd = Color.MintCream;
                                    xpp.CustomColors.CaptionGradientMiddle = Color.White;
                                    xpp.CustomColors.CaptionPressedGradientBegin = Color.LightSkyBlue;
                                    xpp.CustomColors.CaptionPressedGradientEnd = Color.LightSkyBlue;
                                    xpp.CustomColors.CaptionPressedGradientMiddle = Color.LightSkyBlue;
                                    xpp.CustomColors.CaptionSelectedGradientBegin = Color.LightBlue;
                                    xpp.CustomColors.CaptionSelectedGradientEnd = Color.LightBlue;
                                    xpp.CustomColors.CaptionSelectedGradientMiddle = Color.LightBlue;
                                    xpp.CustomColors.CaptionSelectedText = Color.Black;
                                    xpp.CustomColors.CaptionText = Color.White;
                                    xpp.CustomColors.FlatCaptionGradientBegin = Color.Orange;
                                    xpp.CustomColors.FlatCaptionGradientEnd = Color.Orange;
                                    xpp.CustomColors.InnerBorderColor = Color.White;
                                    break;

                                default:
                                    break;
                            }
                            #endregion
                            break;

                        case "CIS_CLibrary.CIS_TextLabel":
                            if (cntl.Name == "lblQuickMenu")
                            {
                                CIS_CLibrary.CIS_TextLabel lbl = (CIS_CLibrary.CIS_TextLabel)cntl;
                                lbl.ForeColor = Color.White;
                            }
                            break;
                    }
                }
                break;

            #endregion
        }
    }

    #endregion

    public void SetColourOnFromState(Control frmObj, Enum_Define.Navi_form NaviID, ThemeName tnm)
    {
        switch (NaviID.ToString())
        {
            #region NewRecord
            case "New_Record":
                foreach (Control cntl in frmObj.Controls)
                {
                    string[] strVal = cntl.ToString().Split(',');

                    #region Control
                    switch (strVal[0].ToString())
                    {
                        case "System.Windows.Forms.Panel":
                            switch (tnm)
                            {
                                case ThemeName.Blue:
                                    if (cntl.Name == "pnlContent")
                                    {
                                        cntl.BackColor = Color.Lavender;
                                    }
                                    break;

                                case ThemeName.Gray:
                                    if (cntl.Name == "pnlContent")
                                    {
                                        cntl.BackColor = Color.White;
                                    }
                                    break;

                                case ThemeName.Orange:
                                    if (cntl.Name == "pnlContent")
                                    {
                                        cntl.BackColor = Color.Cornsilk;
                                    }
                                    break;

                                case ThemeName.Default:
                                    if (cntl.Name == "pnlContent")
                                    {
                                        cntl.BackColor = Color.Cornsilk;
                                    }
                                    break;
                            }
                            break;
                    }
                    #endregion
                }
                break;
            #endregion

            #region EditRecord
            case "Edit_Record":
                foreach (Control cntl in frmObj.Controls)
                {
                    string[] strVal = cntl.ToString().Split(',');

                    #region Control
                    switch (strVal[0].ToString())
                    {
                        case "System.Windows.Forms.Panel":
                            switch (tnm)
                            {
                                case ThemeName.Blue:
                                    if (cntl.Name == "pnlContent")
                                    {
                                        cntl.BackColor = Color.LightSteelBlue;
                                    }
                                    break;

                                case ThemeName.Gray:
                                    if (cntl.Name == "pnlContent")
                                    {
                                        cntl.BackColor = Color.Silver;
                                    }
                                    break;

                                case ThemeName.Orange:
                                    if (cntl.Name == "pnlContent")
                                    {
                                        cntl.BackColor = Color.NavajoWhite;
                                    }
                                    break;

                                case ThemeName.Default:
                                    if (cntl.Name == "pnlContent")
                                    {
                                        cntl.BackColor = Color.MistyRose;
                                    }
                                    break;
                            }
                            break;
                    }
                    #endregion
                }
                break;
            #endregion
        }
    }
    
}

public enum ThemeName
{
    Blue, Gray, Orange, Default
}



