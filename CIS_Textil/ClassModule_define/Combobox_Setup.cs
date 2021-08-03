using System;
using System.Collections;
using System.Data;
using  CIS_Bussiness;using CIS_DBLayer;
using Microsoft.VisualBasic;

public class Combobox_Setup
{
    public static string FilterId;
   
    #region "Type Of Combo"

    public enum ComboType
    {
        Mst_Company = 1, Mst_DrCr, Mst_Currency, Mst_LedgerGroup, Mst_Ledger, Mst_LedgerCategory, Mst_CashBank, Mst_Customer,
        Mst_Suppliers, Mst_Brokers, Mst_Employees, Mst_ProductionSalary, Mst_OfficeSalary, Mst_PartyGroup, Mst_Department,
        Mst_WeaverNDepartment, Mst_DepartmentNWeaverNSizer, Mst_Transporter, Mst_Warper, Mst_Sizer, Mst_Weaver, Mst_Dyer, Mst_Report, Mst_Level,
        Mst_Twister, Mst_Peacer, Mst_Jobber, Mst_Packer, Mst_Mill, Mst_TaxType, Mst_Gala, Mst_Haste, Mst_DeliverySeries, Mst_Region, Mst_Bank, Mst_PipeNo,
        Mst_FillterType, Mst_Year, Mst_Quater, Mst_Month, Mst_BloodGroup, Mst_AddressGroup, Mst_FormName, Mst_priority, Mst_FabricQuality_Serial, Mst_FormulaType, Mst_EmailID,
        //LBT
        Mst_LBT, Mst_OrderStatus, Mst_ComboBox, Mst_ReportForm, Mst_CrystalForm, Mst_FormType, Mst_CustomerWithVAT, Mst_Form, Mst_User, Mst_MandTForms, Mst_ShowBy,
        //Design Card
        Mst_PayType, Mst_WeftCol,
        //Embroidary
        Mst_EmbIssueType, Mst_EmbDyer,
        //Inventory
        Mst_ItemGroup, Mst_Item, Mst_ItemCategory, Mst_UnitsofMesuremet,
        //Cheque
        Mst_AmountFormat, Mst_AmountInWords, Mst_DateFormat, Mst_WordCase,

        // Payroll
        Religion, Caste, Mst_Designation, Mst_UserType, Mst_District, Mst_Loan, Mst_PayMode, Mst_AdvacneType,
        Mst_Religion,
        Mst_Caste,
        Mst_Title,
        Mst_Gender,
        Mst_Branch,
        Mst_DepartmentPayroll,
        Mst_WorkType,
        Mst_SalaryType,
        Mst_Maritial,
        Mst_NoOfChild,
        Mst_BloodGrp,
        Mst_Identification, Mst_Type, Mst_LeaveType, Mst_Leave, Mst_Cash,

        // Task
        Mst_ChekListType, Mst_TaskType, Mst_TaskName, Mst_TaskEmployee, Mst_Catalogue, mst_Employee, mst_Priority, Mst_LoginStatus, Mst_LedgerAll,
        //Other
        //Mst_UserType,
        Mst_MiscType,
        Mst_Misc,
        Mst_City,
        Mst_State,
        Mst_Country,
        Mst_JobType,
        Mst_Description,
        Mst_Custom,
        Mst_Custom_Fill,
        Mst_VoucherType,
        Mst_VoucherNo,
        Mst_TransportMode,

        //Yarn
        Mst_YarnType,
        Mst_Yarn,
        Mst_YarnColor,
        Mst_YarnShade,

        //Beam/Weaving
        Mst_Formula,
        Mst_BeamPostion,
        Mst_BeamDesign,
        Mst_DesignNo,
        Mst_Pipe,
        Mst_Unit,
        Mst_Loom,
        Mst_LoomType,
        Mst_Shift,
        Mst_BeamNo,

        //Fabric
        Mst_FabricType,
        Mst_FabricQuality,
        Mst_FabricDesign,
        Mst_FabricDesign_Multi,
        Mst_FabricShade,
        Mst_FabricColor,

        //Manufacturing        
        Mst_Warping,
        Mst_Sizing,
        Mst_Weaving,
        Mst_Twisting,
        Mst_Peaceing,
        Mst_JobWork,


        Mst_YarnProcessType,
        Mst_FabricProcessType,
        Mst_IssueType,
        Mst_Grade,

        Mst_ReportList,
        //Accounts
        PurchaseAc,
        SalesAc,
        FormType,
        Mst_VAT_TAXClass,
        Mst_DeducteeType,
        Mst_TypeofDutyTax,
        Mst_TDSNatureOfPymt,
        Mst_InterestStyle,
        Mst_MailingAdd,
        Mst_Series,
        Mst_BankAccounts,
        Mst_CheqTemplete,
        Mst_ChequeDtls,
        Mst_GridForms,
        Mst_FabricSerial,
        Mst_ThemeName,
        Mst_ChequeStatus,
        Mst_State_DistrictForm,
        Mst_GType,
        Mst_PackingType,
        Mst_VoucherNumbering,
        Mst_FormName_Voucher,
        Mst_EmbShift,
        Mst_MachineName,
        Mst_MachineType,
        Mst_EmpDepartment,
        Mst_BookSerial,
        Mst_LrUpdForms,
        Mst_Catalog
    }

    #endregion

    #region "Form : Combo Box - for filling."

    /// <summary>
    /// Combo box filling critria with master
    /// </summary>
    //public static void FillCbo(ref CIS_MultiColumnComboBox.CIS_MultiColumnComboBox Cbo, ComboType FillType, string sCustomfilter = "")
    //{
    //    try
    //    {
    //        ArrayList strQuery = new ArrayList();
    //        strQuery = Combobox_Setup.CreateQuery(FillType, sCustomfilter);

    //        if (strQuery.Count != 0)
    //        {
    //            Fill_Combo(Cbo, strQuery[0].ToString(), strQuery[1].ToString(), strQuery[2].ToString());
    //            {
    //                Cbo.ColumnWidths = strQuery[5].ToString();
    //                Cbo.AutoComplete = true;
    //                Cbo.AutoDropdown = true;
    //                Cbo.OpenForm = strQuery[strQuery.Count - 2].ToString();
    //                Cbo.Fill_ComboID = Localization.ParseNativeInt(FillType.ToString());
    //                Cbo.Validating += EventHandles.cboFabType_Validating;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
    //    }

    //}

    public static void FillCbo(ref CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cbo, ComboType FillType, string sCustomfilter = "", string sParent = "")
    {
        try
        {
            DataTable Dt = new DataTable();
            int i = 0;
            try
            {
                Dt = DB.GetDT("exec SP_ComboSetup '" + FillType + "','" + sCustomfilter + "','" + FilterId + "','" + Db_Detials.StoreID + "','" + Db_Detials.CompID + "','" + Db_Detials.BranchID + "','" + Db_Detials.YearID + "'", false);
                i = 1;
            }
            catch
            {
                i = 0;
            }
            if (i == 1)
            {
                try
                {
                    string ValueMember = Dt.Columns[0].ColumnName.ToString();
                    string DisplayMember = Dt.Columns[1].ColumnName.ToString();
                    string isMulti = DB.GetSnglValue("select IsMultiCombo  from  tbl_ComboBoxMaster where ComboType='" + FillType.ToString() + "'");
                    try
                    {
                        DataRow row = Dt.NewRow();
                        row[DisplayMember] = " ";
                        row[ValueMember] = 0;
                        Dt.Rows.InsertAt(row, 0);
                    }
                    catch { }

                    cbo.DataSource = Dt;
                    string[] strDis = DisplayMember.ToString().Split(',');
                    if (cbo.Items.Count > 0)
                    {
                        cbo.DisplayMember = strDis[0];
                        cbo.ValueMember = ValueMember;
                        if (isMulti == "False")
                        {
                            cbo.ColumnWidths = "0;" + cbo.Width + ";0;0;0;0";
                        }
                        else
                        {
                            try
                            {
                                cbo.ColumnWidths = Dt.Rows[1]["ColWidth"].ToString();
                            }
                            catch { cbo.ColumnWidths = "0;" + cbo.Width + ";0;0;0;0"; }
                        }

                        cbo.AutoComplete = true;
                        cbo.AutoDropdown = true;
                        if (cbo.Items.Count > 1)
                        {
                            try
                            {
                                cbo.OpenForm = Dt.Rows[1]["Form"].ToString();
                            }
                            catch { }
                        }
                        else
                        {
                            try
                            {
                                cbo.OpenForm = Dt.Columns[5].ColumnName.ToString();
                            }
                            catch { }
                        }
                        int iComboID = Localization.ParseNativeInt(DB.GetSnglValue("select ComboID from  tbl_ComboBoxMaster where ComboType='" + FillType.ToString() + "'"));
                        cbo.Fill_ComboID = iComboID;
                        try
                        {
                            cbo.SelectedIndex = -1;
                        }
                        catch { }
                        cbo.Validating += EventHandles.cboFabType_Validating;
                    }
                }
                catch (Exception ex)
                {
                    Navigate.logError(ex.Message, ex.StackTrace);
                }
            }
        }
        catch (Exception ex)
        {
            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
        }
    }
    public static void FillFormCloseCbo(ref CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cbo, string FillType, string sCustomfilter = "")
    {
        try
        {
            DataTable Dt = DB.GetDT("exec SP_ComboSetup '" + FillType + "','" + sCustomfilter + "','" + Db_Detials.StoreID + "','" + Db_Detials.CompID + "','" + Db_Detials.BranchID + "','" + Db_Detials.YearID + "'", false);
            string ValueMember = Dt.Columns[0].ColumnName.ToString();
            string DisplayMember = Dt.Columns[1].ColumnName.ToString();
            try
            {
                DataRow row = Dt.NewRow();
                row[DisplayMember] = " ";
                row[ValueMember] = 0;
                Dt.Rows.InsertAt(row, 0);
            }
            catch { }

            cbo.DataSource = Dt;
            string[] strDis = DisplayMember.ToString().Split(',');
            if (cbo.Items.Count > 0)
            {
                cbo.DisplayMember = strDis[0];
                cbo.ValueMember = ValueMember;
                cbo.ColumnWidths = "0;" + cbo.Width + ";0;0;0;0";
                cbo.AutoComplete = true;
                cbo.AutoDropdown = true;
                if (cbo.Items.Count > 1)
                {

                    cbo.OpenForm = Dt.Rows[1][3].ToString();
                }
                else
                {
                    cbo.OpenForm = Dt.Columns[5].ColumnName.ToString();
                }
                int iComboID = Localization.ParseNativeInt(DB.GetSnglValue("select ComboID from  tbl_ComboBoxMaster where ComboType='" + FillType.ToString() + "'"));
                cbo.Fill_ComboID = iComboID;
                cbo.Validating += EventHandles.cboFabType_Validating;
                try
                {
                    cbo.SelectedIndex = -1;
                }
                catch { }
            }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
        }
    }

    /// <summary>
    /// fill Combo
    /// </summary>
    public static void Fill_Combo(CIS_MultiColumnComboBox.CIS_MultiColumnComboBox cbo, string SqlQuery, string DisplayMember, string ValueMember)
    {
        try
        {
            DataTable Dt = DB.GetDT(SqlQuery, false);

            try
            {
                DataRow row = Dt.NewRow();
                row[DisplayMember] = " ";
                row[ValueMember] = 0;
                Dt.Rows.InsertAt(row, 0);
            }
            catch { }

            cbo.DataSource = Dt;
            string[] strDis = DisplayMember.ToString().Split(',');

            if (cbo.Items.Count > 0)
            {
                cbo.DisplayMember = strDis[0];
                cbo.ValueMember = ValueMember;
                try
                {
                    cbo.SelectedIndex = -1;
                }
                catch { }
            }
        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            Navigate.ShowMessage(CIS_Utilities.CIS_DialogIcon.Error, "", ex.Message);
        }
    }

    #endregion

    #region "Form : Create Query - for filling."

    private static ArrayList CreateQueryDB(string ComboType, string whereCondition)
    {
        try
        {
            string sHeaderText = string.Empty;
            string sSelect = string.Empty;
            string sWhere = string.Empty;
            string sForm = string.Empty;

            ArrayList strQuery = new ArrayList();
            using (IDataReader iDr = DB.GetRS("Select * from tbl_ComboMaster WHERE ComboType=" + CommonLogic.SQuote(ComboType)))
            {
                if (iDr.Read())
                {
                    if (string.IsNullOrEmpty(sSelect.ToString()))
                        sSelect = iDr["DisplayMember"].ToString();

                    if (Localization.ParseNativeInt(FilterId) == 0)
                        strQuery.Add(string.Format("Select {0}, {1} From {2} {3} Order BY {4}", iDr["ValueMember"].ToString(), sSelect, iDr["TableName"].ToString(), iDr["WhereCon"].ToString(), iDr["OrderBy"].ToString()));
                    else
                        strQuery.Add(string.Format("Select {0}, {1} From {2} {3} Order BY {4}", iDr["ValueMember"].ToString(), sSelect, iDr["TableName"].ToString(), iDr["FilterCond"].ToString(), iDr["OrderBy"].ToString()));

                    strQuery.Add(iDr["DisplayMember"].ToString());
                    strQuery.Add(iDr["ValueMember"].ToString());
                    strQuery.Add(iDr["HeaderText"].ToString());
                    strQuery.Add(iDr["Form"].ToString());
                    strQuery.Add(iDr["ColWidth"].ToString());
                }
            }

            return strQuery;

        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return null;
        }
    }

    private static ArrayList CreateQuery(ComboType FillType, string whereCondition)
    {
        try
        {
            string sHeaderText = string.Empty;
            string sTableName = string.Empty;
            string sDisplayMember = null;
            string sValueMember = string.Empty;
            string sSelect = string.Empty;
            string sWhere = string.Empty;
            string sForm = string.Empty;
            //bool IsQuery = false;
            string sColWidth = "0;";

            switch (FillType)
            {
                case ComboType.Mst_Company:
                    sDisplayMember = "CompanyName";
                    sValueMember = "CompanyID";
                    sTableName = "tbl_CompanyMaster";
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmCompanyMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_DrCr:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscIdID";
                    sTableName = "tbl_MiscMaster";
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Currency:
                    sDisplayMember = "CurrencyName";
                    sValueMember = "CurrencyID";
                    sTableName = "tbl_CurrencyMaster";
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmCurrencyMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_VoucherType:
                    sDisplayMember = "Menu_Caption";
                    sValueMember = "MenuID";
                    sTableName = "tbl_MenuMaster";
                    if (Strings.Len(FilterId) > 0)
                    {
                        sWhere = string.Format(" where MenuID in {1} Order By {0}", sDisplayMember + " Asc", FilterId);
                        FilterId = "";
                    }
                    else
                    {
                        sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");
                    }

                    break;

                case ComboType.Mst_LedgerGroup:
                    sDisplayMember = "LedgerGroupName";
                    sValueMember = "LedgerGroupId";
                    sTableName = "tbl_LedgerGroupMaster";
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerGroupMaster'");
                    sWhere = string.Format(" Where LedgerGroupTypeId > 0 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Ledger:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = "tbl_LedgerMaster";
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    //if (FilterId.Length > 0)
                    //{
                    //    sWhere = string.Format(" where LedgerGroupId in {1} Order By {0}", sDisplayMember + " Asc", FilterId);
                    //    FilterId = "";
                    //}
                    //else
                    {
                        sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");
                    }
                    sColWidth = "0;300";

                    break;

                case ComboType.Mst_LedgerCategory:
                    sDisplayMember = "LedgerCategory";
                    sValueMember = "LedgerCategoryId";
                    sTableName = "tbl_LedgerGroupMaster";
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerCategoryMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_CashBank:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = "tbl_LedgerMaster";
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId in(32,33) Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Customer:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = "tbl_LedgerMaster";
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId in (31, 34) Order By {0}", sDisplayMember + " Asc");
                    sColWidth = "0;160";

                    break;
                case ComboType.Mst_Suppliers:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = "tbl_LedgerMaster";
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId in (35,27) Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Brokers:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = "tbl_LedgerMaster";
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId = 36 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Employees:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = "tbl_LedgerMaster";
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId = 37 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_ProductionSalary:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = Db_Detials.tbl_LedgerMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId = 38 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_OfficeSalary:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = Db_Detials.tbl_LedgerMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId = 39 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_PartyGroup:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = Db_Detials.tbl_LedgerMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId in (34,35,41) Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Department:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = Db_Detials.tbl_LedgerMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId = 41 Order By {0}", sDisplayMember + " Asc");
                    sColWidth = "0;150;0;";

                    break;
                case ComboType.Mst_WeaverNDepartment:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = Db_Detials.tbl_LedgerMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format("Where LedgerGroupId in (41,45,43) Order By {0}", sDisplayMember + " Asc");
                    sColWidth = "0;260";

                    break;
                case ComboType.Mst_DepartmentNWeaverNSizer:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = Db_Detials.tbl_LedgerMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format("Where LedgerGroupId in (41,45,44,43) Order By {0}", sDisplayMember + " Asc");
                    sColWidth = "0;260";

                    break;
                case ComboType.Mst_Transporter:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = Db_Detials.tbl_LedgerMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId = 42 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Gala:
                    sDisplayMember = "GalaName";
                    sValueMember = "GalaID";
                    sTableName = Db_Detials.tbl_GalaMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_GalaMaster Where FormCall = 'frmGalaMaster'");
                    if (FilterId.Length <= 0)
                    {
                        sWhere = string.Format("  Order By {0}", sDisplayMember + " Asc");

                    }
                    else
                    {
                        sWhere = string.Format(" Where GroupID = {1} Order By {0}", sDisplayMember + " Asc", FilterId);
                        FilterId = "";
                    }

                    break;
                case ComboType.Mst_Warper:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = Db_Detials.tbl_LedgerMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId = 43 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Sizer:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = Db_Detials.tbl_LedgerMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId = 44 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Weaver:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = Db_Detials.tbl_LedgerMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId = 45 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Dyer:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = Db_Detials.tbl_LedgerMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId = 46 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Packer:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = Db_Detials.tbl_LedgerMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId = 53 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Twister:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = Db_Detials.tbl_LedgerMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId = 47 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Peacer:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = Db_Detials.tbl_LedgerMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId = 48 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Jobber:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = Db_Detials.tbl_LedgerMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId = 49 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Mill:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = Db_Detials.tbl_LedgerMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId = 50 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_TaxType:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscIdID";
                    sTableName = Db_Detials.tbl_MiscMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmMiscMaster'");
                    sWhere = string.Format(" Where TypeId = 4 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_ItemGroup:
                    sDisplayMember = "ItemGroupName";
                    sValueMember = "ItemGroupId";
                    sTableName = Db_Detials.tbl_ItemGroupMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmItemGroupMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Item:
                    sDisplayMember = "ItemName";
                    sValueMember = "ItemId";
                    sTableName = Db_Detials.tbl_ItemMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmItemMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_ItemCategory:
                    sDisplayMember = "ItemCategoryName";
                    sValueMember = "ItemCategoryId";
                    sTableName = Db_Detials.tbl_ItemCategoryMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmItemCategoryMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_UnitsofMesuremet:
                    sDisplayMember = "UnitName";
                    sValueMember = "UnitId";
                    sTableName = Db_Detials.tbl_UnitsMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmUnitMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");

                    break;

                case ComboType.Mst_MiscType:
                    sHeaderText = "MiscellaneousType";
                    sDisplayMember = "MiscType";
                    sValueMember = "MiscTypeID";
                    sTableName = Db_Detials.tbl_MiscTypeMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmMiscTypeMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_State:
                    sHeaderText = "State";
                    sDisplayMember = "StateName";
                    sValueMember = "StateID";
                    sTableName = Db_Detials.tbl_StateMaster;
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Country:
                    sHeaderText = "Country";
                    sDisplayMember = "CountryName";
                    sValueMember = "CountryID";
                    sTableName = Db_Detials.tbl_CountryMaster;
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_JobType:
                    sHeaderText = "Job Type";
                    sDisplayMember = "JobName";
                    sValueMember = "JobID";
                    sTableName = Db_Detials.tbl_JobMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmJobMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_YarnType:
                    sHeaderText = "Yarn Type";
                    sDisplayMember = "YarnType";
                    sValueMember = "YarnTypeID";
                    sTableName = Db_Detials.tbl_YarnTypeMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmYarnTypeMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Yarn:
                    sHeaderText = "Yarn";
                    sDisplayMember = "YarnName";
                    sValueMember = "YarnID";
                    sTableName = Db_Detials.tbl_YarnMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmYarnMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_YarnColor:
                    sDisplayMember = "YarnColorName";
                    sValueMember = "YarnColorID";
                    sTableName = Db_Detials.tbl_YarnColorMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmYarnColorMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_YarnShade:
                    sHeaderText = "Yarn Shade";
                    sDisplayMember = "YarnShadeName";
                    sValueMember = "YarnShadeID";
                    sTableName = Db_Detials.tbl_YarnShadeMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmYarnShadeMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Formula:
                    sDisplayMember = "FormulaName";
                    sValueMember = "FormulaId";
                    sTableName = Db_Detials.tbl_FormulaMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmFormulaMaster'");
                    sWhere = string.Format(" Order By {0}", sValueMember + " Asc");

                    break;
                case ComboType.Mst_BeamPostion:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscId";
                    sTableName = Db_Detials.tbl_MiscMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmMiscMaster'");
                    sWhere = string.Format(" where TypeID = 15 Order By {0}", sValueMember + " Asc");
                    break;

                case ComboType.Mst_BeamDesign:
                    sDisplayMember = "BeamDesignName";
                    sValueMember = "BeamDesignID";
                    sTableName = Db_Detials.tbl_BeamDesignMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmBeamDesignMaster'");
                    sWhere = string.Format(" Order By {0}", sValueMember + " Asc");
                    break;

                case ComboType.Mst_DesignNo:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscId";
                    sTableName = Db_Detials.tbl_MiscMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmMiscMaster'");
                    sWhere = string.Format(" where TypeID = 23 Order By {0}", sValueMember + " Asc");
                    break;

                case ComboType.Mst_LoomType:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscId";
                    sTableName = Db_Detials.tbl_MiscMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmMiscMaster'");
                    sWhere = string.Format(" where TypeID = 24 Order By {0}", sValueMember + " Asc");
                    break;

                case ComboType.Mst_BeamNo:
                    sDisplayMember = "BatchNo  as BeamNo";
                    sValueMember = "BatchNo";
                    sTableName = string.Format("fn_FetchForComBeamNo({0},{1})", Db_Detials.CompID, Db_Detials.YearID);
                    //sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmWeftDesignMaster'")
                    if (FilterId.Length > 0)
                    {
                        sWhere = string.Format(" Where LedgerID = {1} Order By {0}", sValueMember + " Asc", FilterId);
                        FilterId = "";
                    }
                    else
                    {
                        sWhere = string.Format("  Order By {0}", sValueMember + " Asc");
                    }
                    break;

                case ComboType.Mst_Pipe:
                    sDisplayMember = "PipeNo";
                    sValueMember = "PipeID";
                    sTableName = Db_Detials.tbl_PipeMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmPipeMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_Loom:
                    sDisplayMember = "LoomName";
                    sValueMember = "LoomID";
                    sTableName = Db_Detials.tbl_LoomMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLoomMaster'");
                    if (Strings.Len(FilterId) > 0)
                    {
                        sWhere = string.Format(" where WeaverID = {1} Order By {0}", sValueMember + " Asc", FilterId);
                        FilterId = "";
                    }
                    else
                    {
                        sWhere = string.Format(" Order By {0}", sValueMember + " Asc");
                    }
                    break;

                case ComboType.Mst_Shift:
                    sDisplayMember = "ShiftName";
                    sValueMember = "ShiftID";
                    sTableName = Db_Detials.tbl_ShiftMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmSiftMaster'");
                    sWhere = string.Format(" Order By {0}", sValueMember + " Asc");
                    break;

                case ComboType.Mst_FabricType:
                    sDisplayMember = "FabricType";
                    sValueMember = "FabricTypeID";
                    sTableName = Db_Detials.tbl_FabricTypeMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmFabricTypeMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_FabricQuality:
                    sDisplayMember = "FabricQualityName";
                    sValueMember = "FabricQualityID";
                    sTableName = Db_Detials.tbl_FabricQualityMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmFabricQualityMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_FabricDesign:
                    sDisplayMember = "FabricDesignName";
                    sValueMember = "FabricDesignID";
                    sTableName = Db_Detials.tbl_FabricDesignMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmFabricDesignMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_FabricDesign_Multi:
                    sDisplayMember = "FabricDesignName, FabricQualityID, FabricQualityName, FabricShadeID, FabricShadeName";
                    sValueMember = "FabricDesignID";
                    sTableName = Db_Detials.vw_Design_ComboSetup;
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmFabricDesignMaster'");
                    sColWidth = "0;160;0;100;0;100";
                    break;

                case ComboType.Mst_FabricShade:
                    sDisplayMember = "FabricShadeName";
                    sValueMember = "FabricShadeId";
                    sTableName = Db_Detials.tbl_FabricShadeMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmFabricShadeMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_FabricColor:
                    sDisplayMember = "FabricColorName";
                    sValueMember = "FabricColorID";
                    sTableName = Db_Detials.tbl_FabricDesignMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmFabricColorMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_YarnProcessType:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.tbl_MiscMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmMiscMaster'");
                    if (FilterId.Length > 0)
                    {
                        sWhere = string.Format(" where TypeId = 11 And MiscID in ({1}) Order By {0}", sValueMember + " Asc", FilterId);
                        FilterId = "";
                    }
                    else
                    {
                        sWhere = string.Format(" where TypeId = 11 Order By {0}", sValueMember + " Asc");
                    }

                    break;
                case ComboType.Mst_IssueType:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.tbl_MiscMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmMiscMaster'");
                    sWhere = string.Format(" Where TypeId =  12 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_FabricProcessType:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.tbl_MiscMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmMiscMaster'");
                    sWhere = string.Format(" Where TypeId =  13 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.Mst_Grade:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.tbl_MiscMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmMiscMaster'");
                    sWhere = string.Format(" Where TypeId =  14 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.PurchaseAc:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerID";
                    sTableName = Db_Detials.tbl_LedgerMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupID = 16 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.SalesAc:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerID";
                    sTableName = Db_Detials.tbl_LedgerMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupID = 15 Order By {0}", sDisplayMember + " Asc");

                    break;
                case ComboType.FormType:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.tbl_MiscMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmMiscMaster'");
                    sWhere = string.Format(" Where TypeId =  19 Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_TransportMode:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.tbl_MiscMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmMiscMaster'");
                    sWhere = string.Format(" Where TypeId =  21 Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_ReportList:
                    sDisplayMember = "FormName";
                    sValueMember = "ReportID";
                    sTableName = Db_Detials.tbl_ReportList;
                    if (FilterId.Length > 0)
                    {
                        sWhere = string.Format(" Where ModuleID = {0}", FilterId);
                        FilterId = "";
                    }

                    break;

                case ComboType.Mst_VAT_TAXClass:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscId";
                    sTableName = Db_Detials.tbl_MiscMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmMiscMaster'");
                    sWhere = string.Format(" where TypeID = 7 Order By {0}", sValueMember + " Asc");

                    break;
                case ComboType.Mst_Description:
                    sDisplayMember = "Description";
                    sValueMember = "DescriptionID";
                    sTableName = Db_Detials.tbl_descriptionMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmDescriptionMaster'");
                    sWhere = string.Format(" ", sValueMember + " Asc");

                    break;
                case ComboType.Mst_Haste:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerID";
                    sTableName = Db_Detials.tbl_LedgerMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format("Where LedgergroupID = 52 ", sValueMember + " Asc");

                    break;
                case ComboType.Mst_DeducteeType:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscId";
                    sTableName = Db_Detials.tbl_MiscMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmMiscMaster'");
                    sWhere = string.Format(" where TypeID = 4 Order By {0}", sValueMember + " Asc");

                    break;
                case ComboType.Mst_TypeofDutyTax:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscId";
                    sTableName = Db_Detials.tbl_MiscMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmMiscMaster'");
                    sWhere = string.Format(" where TypeID = 3 Order By {0}", sValueMember + " Asc");

                    break;
                case ComboType.Mst_TDSNatureOfPymt:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscId";
                    sTableName = Db_Detials.tbl_MiscMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmMiscMaster'");
                    sWhere = string.Format(" where TypeID = 6 Order By {0}", sValueMember + " Asc");

                    break;
                case ComboType.Mst_InterestStyle:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscId";
                    sTableName = Db_Detials.tbl_MiscMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmMiscMaster'");
                    sWhere = string.Format(" where TypeID = 10 Order By {0}", sValueMember + " Asc");

                    break;
                case ComboType.Mst_DeliverySeries:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscId";
                    sTableName = Db_Detials.tbl_MiscMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmMiscMaster'");
                    sWhere = string.Format(" where TypeID = 28 Order By {0}", sValueMember + " Asc");

                    break;
                case ComboType.Mst_PayType:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscId";
                    sTableName = Db_Detials.tbl_MiscMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmMiscMaster'");
                    sWhere = string.Format(" where TypeID = 30 Order By {0}", sValueMember + " Asc");

                    break;
                case ComboType.Mst_WeftCol:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscId";
                    sTableName = Db_Detials.tbl_MiscMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmMiscMaster'");
                    sWhere = string.Format(" where TypeID = 31 Order By {0}", sValueMember + " Asc");

                    break;
                case ComboType.Mst_MailingAdd:
                    sDisplayMember = "MailAdd";
                    sValueMember = "MailingConfigID";
                    sTableName = Db_Detials.tbl_MailingConfig;
                    break;

                case ComboType.Mst_ChekListType:
                    sDisplayMember = "CheckListTypeName";
                    sValueMember = "CheckListTypeID";
                    sTableName = Db_Detials.tbl_CheckListTypeMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmCheckListMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_TaskType:
                    sDisplayMember = "TaskTypeName";
                    sValueMember = "TaskTypeID";
                    sTableName = Db_Detials.tbl_TaskTypeMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmTaskTypeMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_TaskName:
                    sDisplayMember = "TaskName";
                    sValueMember = "TaskID";
                    sTableName = Db_Detials.tbl_TaskMasterMain;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmTaskMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_UserType:
                    sDisplayMember = "SecurityLvl";
                    sValueMember = "SecurityID";
                    sTableName = Db_Detials.tbl_SecurityMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmSecurityMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_Catalogue:
                    sDisplayMember = "CatalogName";
                    sValueMember = "CatalogID";
                    sTableName = "tbl_CatalogMaster";
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmCatalogueMaster'");
                    sWhere = string.Format("Order By {0}", sDisplayMember + " Asc");
                    sColWidth = "0;150;0;";
                    break;

                case ComboType.mst_Employee:
                    sDisplayMember = "EmployeeName";
                    sValueMember = "StaffID";
                    sTableName = "fn_ViewEmployee()";
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmTaskMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.mst_Priority:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.tbl_MiscMaster;
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmTaskMaster'");
                    sWhere = string.Format(" Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_Region:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = "tbl_MiscMaster";
                    sWhere = string.Format("where TypeID=(select MiscTypeID from tbl_MiscTypeMaster where MiscType='Region') Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_Title:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.fn_MiscMasterFind;
                    sWhere = string.Format(" where Type='Name Of Title' Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_Gender:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.fn_MiscMasterFind;
                    sWhere = string.Format(" where Type='Gender' Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_Religion:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.fn_MiscMasterFind;
                    sWhere = string.Format(" where Type='Religion' Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_Caste:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.fn_MiscMasterFind;
                    sWhere = string.Format(" where Type='Caste' Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_WorkType:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.fn_MiscMasterFind;
                    sWhere = string.Format(" where Type='Working Type' Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_SalaryType:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.fn_MiscMasterFind;
                    sWhere = string.Format(" where Type='Salary Type' Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_Maritial:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.fn_MiscMasterFind;
                    sWhere = string.Format(" where Type='Marital Status' Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_NoOfChild:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.fn_MiscMasterFind;
                    sWhere = string.Format(" where Type='No Of Childrean' Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_BloodGrp:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.fn_MiscMasterFind;
                    sWhere = string.Format(" where Type='Blood Group' Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_Identification:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.fn_MiscMasterFind;
                    sWhere = string.Format(" where Type='Identification' Order By {0}", sDisplayMember + " Asc");
                    break;


                case ComboType.Mst_Branch:
                    sDisplayMember = "BranchName";
                    sValueMember = "BranchID";
                    sTableName = Db_Detials.tbl_BranchMaster;
                    sWhere = string.Format("Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_DepartmentPayroll:
                    sDisplayMember = "DepartmentName";
                    sValueMember = "DepartmentID";
                    sTableName = Db_Detials.tbl_DepartmentMaster;
                    sWhere = (whereCondition == "" ? "" : " where BranchID=" + whereCondition + " Order By " + sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_Designation:
                    sDisplayMember = "DesignationName";
                    sValueMember = "DesignationID";
                    sTableName = Db_Detials.tbl_DesignationMaster;
                    sWhere = (whereCondition == "" ? "" : " where DepartmentID=" + whereCondition + " Order By " + sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_City:
                    sDisplayMember = "CityName";
                    sValueMember = "CityId";
                    sTableName = Db_Detials.vw_City;
                    sWhere = (whereCondition == "" ? "" : " where  DistrictID=" + whereCondition + " Order By " + sDisplayMember + " Asc");
                    break;


                case ComboType.Mst_District:
                    sDisplayMember = "DistrictName";
                    sValueMember = "DistrictID";
                    sTableName = Db_Detials.tbl_DistrictMaster;
                    sWhere = (whereCondition == "" ? "" : " where  StateID=" + whereCondition + " Order By " + sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_Type:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.fn_MiscMasterFind;
                    sWhere = string.Format(" where Type='Type' Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_Leave:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.fn_MiscMasterFind;
                    sWhere = string.Format(" where Type='Leave'");
                    break;

                case ComboType.Mst_LeaveType:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = Db_Detials.fn_MiscMasterFind;
                    sWhere = string.Format(" where Type='Leave Type' Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_Cash:
                    sDisplayMember = "LedgerName";
                    sValueMember = "LedgerId";
                    sTableName = "tbl_LedgerMaster";
                    sForm = DB.GetSnglValue("Select MenuID From tbl_MenuMaster Where FormCall = 'frmLedgerMaster'");
                    sWhere = string.Format(" Where LedgerGroupId in(32) Order By {0}", sDisplayMember + " Asc");
                    break;

                case ComboType.Mst_LBT:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = "tbl_MiscMaster";
                    sWhere = string.Format(" where TypeID=(select MiscTypeID from tbl_MiscTypeMaster where MiscType='{0}')  Order By {1}", "LBT Type", sDisplayMember + " Asc");

                    break;

                case ComboType.Mst_LoginStatus:
                    sDisplayMember = "MiscName";
                    sValueMember = "MiscID";
                    sTableName = "tbl_MiscMaster";
                    sWhere = string.Format(" where TypeID=(select MiscTypeID from tbl_MiscTypeMaster where MiscType='{0}')  Order By {1}", "User Login Status", sDisplayMember + " Asc");

                    break;
            }

            ArrayList strQuery = new ArrayList();
            //using (IDataReader iDr = DB.GetRS("Select * from tbl_ComboMaster WHERE ComboType=" + CommonLogic.SQuote() ))
            //{
            //    if (iDr.Read())
            //    {
            //        if (string.IsNullOrEmpty(sSelect.ToString()))
            //            sSelect = iDr["DisplayMember"].ToString();

            //        if(Localization.ParseNativeInt(FilterId)==0)
            //            strQuery.Add(string.Format("Select {0}, {1} From {2} {3} Order BY {4}", iDr["ValueMember"].ToString(), sSelect, iDr["TableName"].ToString(), iDr["WhereCon"].ToString(), iDr["OrderBy"].ToString()));
            //        else
            //            strQuery.Add(string.Format("Select {0}, {1} From {2} {3} Order BY {4}", iDr["ValueMember"].ToString(), sSelect, iDr["TableName"].ToString(), iDr["FilterCond"].ToString(), iDr["OrderBy"].ToString()));

            //        strQuery.Add(iDr["DisplayMember"].ToString());
            //        strQuery.Add(iDr["ValueMember"].ToString());
            //        strQuery.Add(sHeaderText);
            //        strQuery.Add(iDr["Form"].ToString());
            //        strQuery.Add(iDr["ColWidth"].ToString());
            //    }
            //}

            if (!string.IsNullOrEmpty(sValueMember) & !string.IsNullOrEmpty(sDisplayMember) & !string.IsNullOrEmpty(sTableName))
            {
                if (string.IsNullOrEmpty(sSelect.ToString()))
                    sSelect = sDisplayMember;
                strQuery.Add(string.Format("Select {0}, {1} From {2} {3}", sValueMember, sSelect, sTableName, sWhere));
                strQuery.Add(sDisplayMember);
                strQuery.Add(sValueMember);
                strQuery.Add(sHeaderText);
                strQuery.Add(sForm);
                strQuery.Add(sColWidth);
            }
            return strQuery;

        }
        catch (Exception ex)
        {
            Navigate.logError(ex.Message, ex.StackTrace);
            return null;
        }
    }

    #endregion
}

