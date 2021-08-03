using System;
using System.Collections;
using CrystalDecisions.CrystalReports.Engine;
using  CIS_Bussiness;using CIS_DBLayer;

public class Db_Detials
{
   //new System.Drawing.Font("Verdana",11.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
    public static int UserID = 0;
    public static int CompID = 0;
    public static int YearID = 0;
    public static int StoreID = 1;
    public static int BranchID = 1;
    //public static int SeriesTypeID = 0;
    public static int PCS_NO_INCMT = 0;
    public static bool IsSaveClicked = false;
    public static int UserType = 0;
    public static int RowIndex = 0;
    public static string CompanyName = string.Empty;
    public static string IntCompID = string.Empty;
    public static bool IsRuntime = false;
    public static string DbName;
    public static bool IsLedgerEdit = false;
    public static string ActiveTheme;
    public static DateTime FinancialYearFrom;
    public static DateTime FinancialYearTo;

    public static string ActiveFont = "Verdana";
    public static float ActiveFontSize = 8.25F;
    //public static string ActiveFontColor = "Black";
    //public static string DtlsGridForeColor = "Black";
    //public static string DtlsGridBackColor = "White";
    //public static string ButtonForeColor = "Black";
    //public static string ButtonBackColor = "Blue";

    public static string AppVersion = "Tex-til 16.04.18.0";
    public static string _TextBoxCCase = "Capitalise";

    //hashtable to store relation between menuitem position and function to call
    public static Hashtable Hashref = new Hashtable();
    public static ReportDocument objReport;
    public static int iUpdateTimeInterval=1000;

    #region DB BackUp Variables

    public static int iTimeInterval;
    public static int iTimeInterval_folder;

    public static bool IsTypeAutometic;
    public static bool IsTypeAutometic_Folder;
    public static string sAutometicBkpPath;

    public static bool IsOnceADay;
    public static bool IsOnceADay_folder;

    public static bool IsAlwaysNewFile;
    public static bool IsAlwaysNewFile_Fodler;

    public static bool IsBkpDoneForDay;
    public static bool IsBkpDoneForDay_Folder;

    public static string sLastFileNM;
    public static string sLastFileNM_Folder;
    public static DateTime dtBackUpTime;
    public static DateTime dtBackUpTime_Folder;

    #endregion

    #region "All the Tables From Database"

    //--- A
    public const string tbl_AcLedger = "tbl_AcLedger";
    public const string tbl_AccountMain = "tbl_AccountMain";

    public const string tbl_AccountDtls = "tbl_AccountDtls";
    //--- B
    public const string tbl_BeamTypeMaster = "tbl_BeamTypeMaster";
    public const string tbl_BeamDesignMaster = "tbl_BeamDesignMaster";
    public const string tbl_BeamDesignDtls = "tbl_BeamDesignDtls";
    public const string tbl_BeamProductionSizingMain = "tbl_BeamProductionSizingMain";
    public const string tbl_BeamProductionSizingDtls = "tbl_BeamProductionSizingDtls";
    public const string tbl_BeamProductionSizingFooter = "tbl_BeamProductionSizingFooter";
    public const string tbl_BeamPurchaseFooter = "tbl_BeamPurchaseFooter";
    public const string tbl_BeamProductionWarpingMain = "tbl_BeamProductionWarpingMain";
    public const string tbl_BeamProductionWarpingDtls = "tbl_BeamProductionWarpingDtls";
    public const string tbl_BeamProductionWarpingFooter = "tbl_BeamProductionWarpingFooter";
    public const string tbl_BeamLoadingMain = "tbl_BeamLoadingMain";
    public const string tbl_BeamOpeningMain = "tbl_BeamOpeningMain";
    public const string tbl_BeamOpeningDtls = "tbl_BeamOpeningDtls";
    public const string tbl_BeamIssueDtls = "tbl_BeamIssueDtls";
    public const string tbl_BeamIssueMain = "tbl_BeamIssueMain";
    public const string tbl_BeamTransferMain = "tbl_BeamTransferMain";
    public const string tbl_BeamTransferDtls = "tbl_BeamTransferDtls";
    public const string tbl_BeamInwardMain = "tbl_BeamInwardMain";
    public const string tbl_BeamPhysicalStockMain = "tbl_BeamPhysicalStockMain";
    public const string tbl_BeamPurchaseMain = "tbl_BeamPurchaseMain";
    public const string tbl_BeamAdjustmentMain = "tbl_BeamAdjustmentMain";
    public const string tbl_BranchMaster = "tbl_BranchMaster";
    public const string tbl_BeamPhysicalStockDtls = "tbl_BeamPhysicalStockDtls";

    public const string tbl_BookCreationDtls = "tbl_BookCreationDtls";
    public const string tbl_BookCreationFooter = "tbl_BookCreationFooter";
    public const string tbl_BookOpeningDtls = "tbl_BookOpeningDtls";

    //--- C
    public const string tbl_CityMaster = "tbl_CityMaster";
    public const string tbl_CheckListTypeMaster = "tbl_CheckListTypeMaster";
    public const string tbl_CountryMaster = "tbl_CountryMaster";
    public const string tbl_CompanyMaster = "tbl_CompanyMaster";
    public const string tbl_ComboBoxMaster = "tbl_ComboBoxMaster";
    public const string tbl_CurrencyMaster = "tbl_CurrencyMaster";
    public const string tbl_ClothOpening = "tbl_ClothOpening";
    public const string tbl_ContraMain = "tbl_ContraMain";
    public const string tbl_CuttingEntrydtls = "tbl_CuttingEntrydtls";
    public const string tbl_CuttingDeliveryChallanDtls = "tbl_CuttingDeliveryChallanDtls";
    public const string tbl_CformDtls = "tbl_CformDtls";

    //--- D
    public const string tbl_DrCrAdj = "tbl_DrCrAdj";
    public const string tbl_DepartmentMaster = "tbl_DepartmentMaster";
    public const string tbl_DesignationMaster = "tbl_DesignationMaster";
    public const string tbl_descriptionMaster = "tbl_descriptionMaster";
    public const string tbl_DistrictMaster = "tbl_DistrictMaster";
    public const string tbl_DailyWorkInwardDtls = "tbl_DailyWorkInwardDtls";
    //--- E

    public const string tbl_EmployeeMaster = "tbl_EmployeeMaster";
    //--- F

    public const string tbl_FabricOrderLedger = "tbl_FabricOrderLedger";
    public const string tbl_FabricTypeMaster = "tbl_FabricTypeMaster";
    public const string tbl_FabricQualityMaster = "tbl_FabricQualityMaster";
    public const string tbl_FabricDesignMaster = "tbl_FabricDesignMaster";
    public const string tbl_FabricShadeMaster = "tbl_FabricShadeMaster";
    public const string tbl_FormulaMaster = "tbl_FormulaMaster";
    public const string tbl_FabricOpeningMain = "tbl_FabricOpeningMain";
    public const string tbl_FabricOpeningDtls = "tbl_FabricOpeningDtls";
    public const string tbl_FabricPurchaseMain = "tbl_FabricPurchaseMain";
    public const string tbl_FabricInwardMain = "tbl_FabricInwardMain";
    public const string tbl_FabricInwardDtls = "tbl_FabricInwardDtls";
    public const string tbl_FabricPurchaseDtls = "tbl_FabricPurchaseDtls";
    public const string tbl_FabricPurchaseFooter = "tbl_FabricPurchaseFooter";
    public const string tbl_FabricPurhaseReturnMain = "tbl_FabricPurhaseReturnMain";
    public const string tbl_FabricPurhaseReturnDtls = "tbl_FabricPurhaseReturnDtls";
    public const string tbl_FabricPurhaseReturnFooter = "tbl_FabricPurhaseReturnFooter";
    public const string tbl_FabricPurchaseOrderDtls = "tbl_FabricPurchaseOrderDtls";
    public const string tbl_FabricPurchaseOrderMain = "tbl_FabricPurchaseOrderMain";
    public const string tbl_FabricIssueMain = "tbl_FabricIssueMain";
    public const string tbl_FabricIssueDtls = "tbl_FabricIssueDtls";
    public const string tbl_FabricReceiptMain = "tbl_FabricReceiptMain";
    public const string tbl_FabricReceiptDtls = "tbl_FabricReceiptDtls";
    public const string tbl_FabricProcessEntryMain = "tbl_FabricProcessEntryMain";
    public const string tbl_FabricProcessEntryDtls = "tbl_FabricProcessEntryDtls";
    public const string tbl_FabricProcessOrderMain = "tbl_FabricProcessOrderMain";
    public const string tbl_FabricProcessOrderDtls = "tbl_FabricProcessOrderDtls";
    public const string tbl_FabricReturnMain = "tbl_FabricReturnMain";
    public const string tbl_FabricReturnDtls = "tbl_FabricReturnDtls";
    public const string tbl_FabricPackingIssueMain = "tbl_FabricPackingIssueMain";
    public const string tbl_FabricPackingIssueDtls = "tbl_FabricPackingIssueDtls";
    public const string tbl_FabricPackingReceiptMain = "tbl_FabricPackingReceiptMain";
    public const string tbl_FabricPackingReceiptDtls = "tbl_FabricPackingReceiptDtls";
    public const string tbl_FabricPackingReturnMain = "tbl_FabricPackingReturnMain";
    public const string tbl_FabricPackingReturnDtls = "tbl_FabricPackingReturnDtls";
    public const string tbl_FabricDeliveryReturnMain = "tbl_FabricDeliveryReturnMain";
    public const string tbl_FabricDeliveryReturnDtls = "tbl_FabricDeliveryReturnDtls";
    public const string tbl_FabricDeliveryReturnFooter = "tbl_FabricDeliveryReturnFooter";
    public const string tbl_FabricOutwardMain = "tbl_FabricOutwardMain";
    public const string tbl_FabricOutwardDtls = "tbl_FabricOutwardDtls";
    public const string tbl_FabricSalesOrderDtls = "tbl_FabricSalesOrderDtls";
    public const string tbl_FabricSalesOrderMain = "tbl_FabricSalesOrderMain";
    public const string tbl_FabricSubSalesOrderDtls = "tbl_FabricSubSalesOrderDtls";
    public const string tbl_FabricSubSalesOrderMain = "tbl_FabricSubSalesOrderMain";
    public const string tbl_FabricInvoiceMain = "tbl_FabricInvoiceMain";
    public const string tbl_FabricInvoiceDtls = "tbl_FabricInvoiceDtls";
    public const string tbl_FabricInvoiceFooter = "tbl_FabricInvoiceFooter";
    public const string tbl_FabricMergingMain = "tbl_FabricMergingMain";
    public const string tbl_FabricMergingDtls = "tbl_FabricMergingDtls";
    public const string tbl_FabricMergingFooter = "tbl_FabricMergingFooter";
    public const string tbl_FabricTransferMain = "tbl_FabricTransferMain";
    public const string tbl_FabricTransferDtls = "tbl_FabricTransferDtls";
    public const string tbl_FabricProductionMain = "tbl_FabricProductionMain";
    public const string tbl_FabricProductionMain2 = "tbl_FabricProductionMain2";
    public const string tbl_FabricProductionDtls2 = "tbl_FabricProductionDtls2";
    public const string tbl_FabricProductionFooter = "tbl_FabricProductionFooter";
    public const string tbl_FabricProductionDtls = "tbl_FabricProductionDtls";
    public const string tbl_FabricCheckingDtls = "tbl_FabricCheckingDtls";
    public const string tbl_FabricCheckingFooter = "tbl_FabricCheckingFooter";
    public const string tbl_FabricCheckingMain = "tbl_FabricCheckingMain";
    public const string tbl_MultiFabProductionMain = "tbl_MultiFabProductionMain ";
    public const string tbl_FabricPhysicalStockMain = "tbl_FabricPhysicalStockMain";
    public const string tbl_FabricProcessInvoiceDtls = "tbl_FabricProcessInvoiceDtls";
    public const string tbl_FabricProcessInvoiceFooter = "tbl_FabricProcessInvoiceFooter";
    public const string tbl_FabricPhysicalStockDtls = "tbl_FabricPhysicalStockDtls";
    //--- G
    public const string tbl_GridSettings = "tbl_GridSettings";
    public const string tbl_GridSettings_tbls = "tbl_GridSettings_tbls";
    public const string tbl_GradeMaster = "tbl_GradeMaster";
    public const string tbl_GalaMaster = "tbl_GalaMaster";
    public const string tbl_GridFields_Mapping = "tbl_GridFields_Mapping";
    public const string tbl_GlobalSettings = "tbl_GlobalSettings";
    public const string tbl_GenPurchaseDeduct = "tbl_GenPurchaseDeduct";

    public const string tbl_GenPurchaseDtls = "tbl_GenPurchaseDtls";
    //--- I 
    public const string tbl_ItemGroupMaster = "tbl_ItemGroupMaster";
    public const string tbl_ItemMaster = "tbl_ItemMaster";

    public const string tbl_ItemCategoryMaster = "tbl_ItemCategoryMaster";
    //--- J
    public const string tbl_JobMaster = "tbl_JobMaster";
    public const string tbl_JournalMain = "tbl_JournalMain";

    public const string tbl_JournalDtls = "tbl_JournalDtls";

    //--- L 
    public const string tbl_LedgerGroupMaster = "tbl_LedgerGroupMaster";
    public const string tbl_LedgerMaster = "tbl_LedgerMaster";
    public const string tbl_LedgerCategoryMaster = "tbl_LedgerCategoryMaster";

    public const string tbl_LoomMaster = "tbl_LoomMaster";

    //--- M
    public const string tbl_MenuMaster = "tbl_MenuMaster";
    public const string tbl_MiscMaster = "tbl_MiscMaster";
    public const string tbl_MiscTypeMaster = "tbl_MiscTypeMaster";

    public const string tbl_MailingConfig = "tbl_MailingConfig";
    //--- P
    public const string tbl_PartyGroupMaster = "tbl_PartyGroupMaster";
    public const string tbl_PipeMaster = "tbl_PipeMaster";
    public const string tbl_PaymentDtls = "tbl_PaymentDtls";
    public const string tbl_PaymentMain = "tbl_PaymentMain";
    public const string tbl_PurchaseMain = "tbl_PurchaseMain";
    public const string tbl_PurchaseDtls = "tbl_PurchaseDtls";
    public const string tbl_PurchaseDeduct = "tbl_PurchaseDeduct";
    public const string tbl_packingreceiptdtls = "tbl_packingreceiptdtls";
    public const string tbl_PerformaInvoiceMain = "tbl_PerformaInvoiceMain";
    public const string tbl_PerformaInvoiceDtls = "tbl_PerformaInvoiceDtls";
    public const string tbl_FabricPackingSlipMain = "tbl_FabricPackingSlipMain";

    public const string tbl_FabricPackingSlipDtls = "tbl_FabricPackingSlipDtls";
    //--- R
    public const string tbl_ReportList = "tbl_ReportList";
    public const string tbl_ReceiptDtls = "tbl_ReceiptDtls";
    public const string tbl_RollProductionMain = "tbl_RollProductionMain";
    public const string tbl_RollProductionDtls = "tbl_RollProductionDtls";
    public const string tbl_RollProductionFooter = "tbl_RollProductionFooter";
    public const string tbl_ItemRateListDtls = "tbl_ItemRateListDtls";

    public const string tbl_ReceiptMain = "tbl_ReceiptMain";
    //--- S

    public const string tbl_StateMaster = "tbl_StateMaster";
    public const string tbl_ShiftMaster = "tbl_ShiftMaster";
    public const string tbl_SalesMain = "tbl_SalesMain";
    public const string tbl_SalesDtls = "tbl_SalesDtls";


    public const string tbl_SalesDeduct = "tbl_SalesDeduct";
    public const string tbl_StockGenralLedger = "tbl_StockGenralLedger";
    public const string tbl_StockYarnLedger = "tbl_StockYarnLedger";
    public const string tbl_StockBeamLedger = "tbl_StockBeamLedger";
    public const string tbl_StockCatalogLedger = "tbl_StockCatalogLedger";
    public const string tbl_StockFabricLedger = "tbl_StockFabricLedger";
    public const string tbl_StockItemLedger = "tbl_StockItemLedger";
    public const string tbl_SecurityMaster = "tbl_SecurityMaster";

    //--- T

    public const string tbl_TransportInvoiceMain = "tbl_TransportInvoiceMain";
    public const string tbl_TransportLedger = "tbl_TransportLedger";
    public const string tbl_TaskMasterMain = "tbl_TaskMasterMain";
    public const string tbl_TaskTypeMaster = "tbl_TaskTypeMaster";
    public const string tbl_TaskMasterDtls = "tbl_TaskMasterDtls";
    public const string tbl_TaskAssignmentDtls = "tbl_TaskAssignmentDtls";
    public const string tbl_TaskAssignmentMain = "tbl_TaskAssignmentMain";

    //--- U
    public const string tbl_UnitsMaster = "tbl_UnitsMaster";
    public const string tbl_UserRightsMain = "tbl_UserRightsMain";
    public const string tbl_UserRightsDtls = "tbl_UserRightsDtls";
    public const string tbl_UserMaster = "tbl_UserMaster";

    public const string tbl_UserLog = "tbl_UserLog";
    //--- V

    public const string tbl_VoucherTypeMaster = "tbl_VoucherTypeMaster";

    //--- W
    public const string tbl_WeftYarnMaster = "tbl_WeftYarnMaster";
    public const string tbl_WeftBeamMaster = "tbl_WeftBeamMaster";

    public const string tbl_WeaverInvoiceMain = "tbl_WeaverInvoiceMain";
    //--- Y

    public const string tbl_YarnOrderLedger = "tbl_YarnOrderLedger";
    public const string tbl_YarnOpeningMain = "tbl_YarnOpeningMain";
    public const string tbl_YarnOpeningDtls = "tbl_YarnOpeningDtls";
    public const string tbl_YarnTypeMaster = "tbl_YarnTypeMaster";
    public const string tbl_YarnMaster = "tbl_YarnMaster";
    public const string tbl_YarnMasterDtls = "tbl_YarnMasterDtls";
    public const string tbl_YarnColorMaster = "tbl_YarnColorMaster";
    public const string tbl_YarnShadeMaster = "tbl_YarnShadeMaster";
    public const string tbl_YarnInvoiceFooter = "tbl_YarnInvoiceFooter";
    public const string tbl_YarnInvoiceDtls = "tbl_YarnInvoiceDtls";
    public const string tbl_YarnPurchaseFooter = "tbl_YarnPurchaseFooter";
    public const string tbl_YarnReceiptDtls = "tbl_YarnReceiptDtls";
    public const string tbl_YarnReceiptMain = "tbl_YarnReceiptMain";
    public const string tbl_YarnReturnDtls = "tbl_YarnReturnDtls";
    public const string tbl_YarnReturnMain = "tbl_YarnReturnMain";
    public const string tbl_YarnPurchaseMain = "tbl_YarnPurchaseMain";
    public const string tbl_YarnPurchaseDtls = "tbl_YarnPurchaseDtls";
    public const string tbl_YarnPurchaseReturnMain = "tbl_YarnPurchaseReturnMain";
    public const string tbl_YarnPurchaseReturnDtls = "tbl_YarnPurchaseReturnDtls";
    public const string tbl_YarnPurchaseReturnFooter = "tbl_YarnPurchaseReturnFooter";
    public const string tbl_YarnInwardMain = "tbl_YarnInwardMain";
    public const string tbl_YarnInwardDtls = "tbl_YarnInwardDtls";
    public const string tbl_YarnIssueMain = "tbl_YarnIssueMain";
    public const string tbl_YarnIssueDtls = "tbl_YarnIssueDtls";
    public const string tbl_ReportQuery = "tbl_ReportQuery";
    public const string tbl_ReportConfigMain = "tbl_ReportConfigMain";
    public const string tbl_ReportConfigDtls = "tbl_ReportConfigDtls";
    public const string tbl_YarnPurchaseOrderMain = "tbl_YarnPurchaseOrderMain";
    public const string tbl_YarnPurchaseOrderDtls = "tbl_YarnPurchaseOrderDtls";
    public const string tbl_YarnDo = "tbl_YarnDO";
    public const string tbl_YarnTransferMain = "tbl_YarnTransferMain";
    public const string tbl_YarnTransferDtls = "tbl_YarnTransferDtls";
    public const string tbl_YarnDeliveryChallanMain = "tbl_YarnDeliveryChallanMain";
    public const string tbl_YarnInvoiceMain = "tbl_YarnInvoiceMain";
    public const string tbl_YarnDeliveryChallanDtls = "tbl_YarnDeliveryChallanDtls";
    public const string tbl_YarnProductionMain = "tbl_YarnProductionMain";
    public const string tbl_YarnProductionDtls = "tbl_YarnProductionDtls";
    public const string tbl_YarnProductionDeduct = "tbl_YarnProductionDeduct";
    public const string tbl_YarnProductionFooter = "tbl_YarnProductionFooter";
    public const string tbl_YarnSalesReturnMain = "tbl_YarnSalesReturnMain";
    public const string tbl_YarnSalesReturnDtls = "tbl_YarnSalesReturnDtls";
    public const string tbl_YarnSalesReturnFooter = "tbl_YarnSalesReturnFooter";
    public const string tbl_YarnPhysicalStockMain = "tbl_YarnPhysicalStockMain";
    public const string tbl_YarnPhysicalStockDtls = "tbl_YarnPhysicalStockDtls";
    public const string tbl_YarnProductionOrderMain = "tbl_YarnProductionOrderMain";
    public const string tbl_YarnProcessOrderMain = "tbl_YarnProcessOrderMain";
    public const string tbl_YarnProcessOrderDtls = "tbl_YarnProcessOrderDtls";
    public const string tbl_ReceivableMain = "tbl_ReceivableMain";
    public const string tbl_ReceivableDtls = "tbl_ReceivableDtls";
    #endregion

    #region "All the Views From Database"

    //--- A


    public const string vw_BillAdjustment = "vw_BillAdjustment";
    //--- C
    public const string vw_City = "vw_City";
    public const string vw_CompanyMaster = "vw_CompanyMaster";

    public const string vw_ColorMaster = "vw_ColorMaster";
    //--- D
    public const string vw_Design_ComboSetup = "vw_Design_ComboSetup";

    public const string vw_WeftDesign_ComboSetup = "vw_WeftDesign_ComboSetup";

    //--- E
    public const string vw_EmployeeMaster = "vw_EmployeeMaster";
    public const string vw_EntityMaster = "vw_EntityMaster";

    public const string vw_EntityTypeMaster = "vw_EntityTypeMaster";
    //--- F


    //--- G

    //--- I

    //--- J

    //--- S

    //--- V

    #endregion

    #region "All the Store Procedure From Database"

    //-- C
    public const string sp_BillAdjustment = "sp_BillAdjustment";
    public const string sp_BillAdjustment_All = "sp_BillAdjustment_All";
    public const string sp_FetchCompSelection = "sp_FetchCompSelection";

    public const string sp_FetchBeamDtls = "Sp_FetchBeamDtls";
    public const string sp_FetchFabPurchesRtnHGrid = "sp_FetchFabPurchesRtnHGrid";
    public const string sp_FetchFabPurchesRtnFGrid = "sp_FetchFabPurchesRtnFGrid";
    public const string sp_FetchPendingLots = "sp_FetchPendingLots";
    public const string sp_FetchWeaverBeamStock = "sp_FetchWeaverBeamStock";
    public const string sp_FetchWeaverYarnStock = "sp_FetchWeaverYarnStock";
    public const string sp_FetchWeaverProductionUnChecked = "sp_FetchWeaverProductionUnChecked";
    public const string sp_FetchWeaverProductionChecked = "sp_FetchWeaverProductionChecked";
    public const string sp_FetchDyingLotStk = "sp_FetchDyingLotStk";

    public const string sp_FetchForBarcodePrint = "sp_FetchForBarcodePrint";

    #endregion

    #region "All the Function From Database"

    public const string fn_BeamsLodedOnLoom = "fn_BeamsLodedOnLoom";
    public const string fn_Fabricmaster = "fn_Fabricmaster()";
    //-- F

    public const string fn_FabricTypeMaster = "fn_FabricTypeMaster()";
    public const string fn_FabricQualityMaster = "fn_FabricQualityMaster()";
    public const string fn_FabricDesignMaster = "fn_FabricDesignMaster()";
    public const string fn_FabricShadeMaster = "fn_FabricShadeMaster()";
    public const string fn_FabricQualityMaster_tbl = "fn_FabricQualityMaster_tbl()";

    public const string fn_FetchFabricStock = "fn_FetchFabricStock";
    public const string fn_FetchFabricStock_PCS = "fn_FetchFabricStock_PCS";
    public const string fn_FetchProcLotDtls = "fn_FetchProcLotDtls";
    public const string fn_FetchYarnPODtls = "fn_FetchYarnPODtls";
    public const string fn_FetchChlnForInvoice = "fn_FetchChlnForInvoice";
    public const string fn_FetchFabSalesOrderDtls = "fn_FetchFabSalesOrderDtls";
    public const string fn_FetchFabPurchaseOrderDtls = "fn_FetchFabPurchaseOrderDtls";
    public const string fn_FetchChlnForYarnInvoice = "fn_FetchChlnForYarnInvoice";
    public const string fn_FetchFabricStockWithLot = "fn_FetchFabricStockWithLot";
    public const string fn_FetchFabSalesOrderForInvoice = "fn_FetchFabSalesOrderForInvoice";
    public const string fn_FetchProcYrnLotStk = "fn_FetchProcYrnLotStk";
    public const string fn_FetchYarnStock = "fn_FetchYarnStock";

    
    public const string fn_ChkDel_LedgerMaster = "dbo.fn_ChkDel_LedgerMaster";
    
    public const string fn_ChkDel_YarnTypeMaster = "dbo.fn_ChkDel_YarnTypeMaster";
    public const string fn_ChkDel_YarnShadeMaster = "dbo.fn_ChkDel_YarnShadeMaster";
    public const string fn_ChkDel_YarnMaster = "dbo.fn_ChkDel_YarnMaster";
    public const string fn_ChkDel_YarnColorMaster = "dbo.fn_ChkDel_YarnColorMaster";
    public const string fn_ChkDel_UnitMaster = "dbo.fn_ChkDel_UnitMaster";
    public const string fn_ChkDel_MiscMaster = "dbo.fn_ChkDel_MiscMaster";
    public const string fn_ChkDel_LoomMaster = "dbo.fn_ChkDel_LoomMaster";
    public const string fn_ChkDel_FabricTypeMaster = "dbo.fn_ChkDel_FabricTypeMaster";
    public const string fn_ChkDel_FabricShadeMaster = "dbo.fn_ChkDel_FabricShadeMaster";
    public const string fn_ChkDel_FabricQualityMaster = "dbo.fn_ChkDel_FabricQualityMaster";
    public const string fn_ChkDel_FabricDesignMaster = "dbo.fn_ChkDel_FabricDesignMaster";
    public const string fn_ChkDel_BeamDesignMaster = "dbo.fn_ChkDel_BeamDesignMaster";
    public const string fn_ChkDel_LedgerGroupMaster = "dbo.fn_ChkDel_LedgerGroupMaster";
    public const string fn_ChkDel_LedgerCategoryMaster = "dbo.fn_ChkDel_LedgerCategoryMaster";
    public const string fn_FetchYarnStockForProduction = "dbo.fn_FetchYarnStockForProduction";
    public const string fn_PendingSalesItemOrder = "dbo.fn_PendingSalesItemOrder";
    public const string fn_FetchPieceNo = "dbo.fn_FetchPieceNo";
    public const string fn_SearchFabricStock = "dbo.fn_SearchFabricStock";
    public const string fn_SearchFabricStock_Barcode = "dbo.fn_SearchFabricStock_Barcode";
    public const string fn_DsgnWiseWeftConsuption = "dbo.fn_DsgnWiseWeftConsuption";
    public const string fn_Chk_DesignInOrder = "dbo.fn_Chk_DesignInOrder";

    //--M
    public const string fn_MiscMasterFind = "dbo.fn_MiscMasterFind()";
    public const string fn_MiscMaster_Tbl = "fn_MiscMaster_Tbl()";

    public const string fn_Chk_DesignOrderLimit = "dbo.fn_Chk_DesignOrderLimit";
    //-- Y
    public const string fn_FormulaMaster_Tbl = "fn_FormulaMaster_Tbl()";
    public const string fn_YarnTypeMaster_Tbl = "fn_YarnTypeMaster_Tbl()";
    public const string fn_YarnMaster_Tbl = "fn_YarnMaster_Tbl()";
    public const string fn_YarnMasterDtls_Tbl = "fn_YarnMasterDtls_Tbl()";
    public const string fn_YarnColorMaster_Tbl = "fn_YarnColorMaster_Tbl()";
    public const string fn_YarnShadeMaster_Tbl = "fn_YarnShadeMaster_Tbl()";


    #endregion
      
    #region "All other supporting enums"

    public enum Ac_DrCr
    {
        Debit = 1,
        Credit = 2
    }

    public enum Ac_AdjType
    {
        OnAccount = 171,
        Advance = 173,
        NewRef = 174,
        AgstRef = 175
    }

    public enum Ac_TrCodes : int
    {
        Contra = 3,
        Payment = 4,
        Receipt = 5,
        Journal = 6,
        Sales = 7,
        Purchase = 8,
        DebitNote = 9,
        CreditNote = 10,
        Memo = 11,
        BankReconciliation = 12,
        PurchaseOrder = 13,
        SalesOrder = 14,
        StockTransferJournal = 15,
        ManufactureStockJournal = 16,
        TDSPayment = 68,
        VATPayment = 69,
        ServiceTaxPayment = 71,
        ExcisePayment = 72,
        Other1 = 73,
        Other2 = 74
    }

    public enum Yarn_TrCodes : int
    {
        YarnPO = 21,
        YarnSO = 22,
        YarnDO = 23,
        YarnPurchase = 24,
        YarnOpening = 25,
        YarnInward = 26,
        YarnIssue = 27,
        YarnReceive = 28,
        YarnReturn = 29,
        YarnProductionOrder = 30,
        YarnProduction = 31,
        YarnDelivery = 32,
        YarnSales = 33

    }

    public enum Fabric_TrCodes : int
    {
        FabricPO = 1,
        FabricSO = 2,
        FabricPurchase = 3,
        FabricOpening = 4,
        FabricInward = 5,
        FabricIssue = 6,
        FabricProcessOrder = 7,
        FabricReceiveFromProcess = 8,
        FabricReceiveFromPacking = 9,
        FabricReceiveFromOther = 10,
        Fabricproduction = 11,
        FabricDelivery = 12,
        FabricSales = 13,
        FabricReturn = 14,
        FabricTransfer = 15,
        FabricPackingSlip = 16
    }

    public enum Beam_TrCodes : int
    {
        BeamOpening = 1,
        BeamProductionFromWarping = 2,
        BeamProductionFromSizing = 3,
        RollProduction = 4,
        BeamTransfer = 5,
        BeamLoading = 6,
        BeamPurchase = 7,
        BeamPurchaseReturn = 8
    }

    //Public Enum pCol As Integer
    //    ParentID = 0
    //    Srno = 1
    //    DrCr = 2
    //    LedgerID = 3
    //    ChequeNo = 4
    //    ChequeDate = 5
    //    RefEntryType = 6
    //    AdjAmount = 7
    //    Amount = 8
    //    Sel = 9
    //    Narration = 10
    //    RefTxnNo = 11
    //    RefBillNo = 12
    //    RefBillDate = 13
    //    IsHead = 14
    //End Enum

    public enum pCol : int
    {
        ParentID = 0,
        Srno = 1,
        DrCr = 2,
        LedgerID = 3,
        Amount = 4,
        Sel = 5,
        AdjAmt = 6,
        ChequeNo = 7,
        ChequeDate = 8,
        Narration = 9,
        RefTxnNo = 10,
        RefEntryType = 11,
        RefModID = 12,
        RefBillNo = 13,
        RefBillDate = 14,
        IsHead = 15,
        IsBillByBill = 16,
        IsProduct = 17
    }


    public enum LedgerGroup : int
    {
        Primary = 1,
        Liability,
        Assets,
        Expenses,
        Income,
        CapitalAccount,
        Loans_Liability,
        CurrentLiabilities,
        FixedAssets,
        Investments,
        CurrentAssets,
        Branch_Divisions,
        Misc_Expenses_ASSET,
        Suspense_Ac,
        SalesAccounts,
        PurchaseAccounts,
        DirectIncomes,
        DirectExpenses,
        IndirectIncomes,
        IndirectExpenses,
        Reserves_Surplus,
        BankODAccount,
        Secured_Loans,
        Unsecured_Loans,
        DutiesAndTaxes,
        Provisions,
        SundryCreditors,
        Stock_in_Hand,
        Deposits_Asset,
        LoansAndAdvances_Asset,
        SundryDebtors,
        Cash_in_Hand,
        BankAccounts
    }

    #endregion

    #region "All BarCode Related Filds"

    public const string Const_PieceNo = "PPPPPPPPPP";
    public const string Const_DesignName = "DDDDDDDDDDDDDDDDDDDD";
    public const string Const_QualityName = "QQQQQQQQQQQQQQQQQQQQ";
    public const string Const_ShadeName = "SSSSSSSSSSSSSSSSSSSS";
    public const string Const_Pcs = "NNNNNNNNNN";
    public const string Const_Meters = "MMMMMMMMMM";
    public const string Const_Weight = "WWWWWWWWWW";
    #endregion

    public const string Const_Grade = "GGGGGGGGGG";
    
}

public class AppMsg
{
    #region APP Messages
    public const string Save_Success = "Record Saved Successfully..";
    public const string Save_Error = "Error Saving Record..";

    public const string FORMS = "Add New Record=>Ctrl+A, Edit Record=> Ctrl+E, Save Record=> Ctrl+S, Cancel Record=Ctrl+L, Delete Record=> Ctrl+D, Find Record=> Ctrl+F,Piece Ref Record=> Ctrl+R, First Record => Home, Previous Record=>Page Up, Next Record=>Page Down, Last Record=>End,  Print Record=> Ctrl+P, Send Email=> Ctrl+I,  Send SMS=> Ctrl+M,  Form Settings=> Ctrl+T,  Logoff=> Ctrl+G,  Close Current Form=> Ctrl+O,  Exit Application=> Ctrl+X";
    public const string REPORTS = "Print=>Alt+P,  Preview=> Alt+V,  Export=> Alt+E,  Email=> Alt+M,  Close=> Alt+C";
    public const string GRIDSETTINGS = "Here You can change the Column Order, Column Size, Hide and Show Columns, Make Column Editable, Make Column Repeate in next row..";
    #endregion
}

public class ConstantVariable 
{
   
}