using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.InteropServices;

public struct Global
{
    //System Setup Function
    public const string SYSTEM_SETUP_FUNCTION_Password = "0E52C1CD2F5B0C54A7AB27632910627A2A78946F";
    public const string TECHNICIAN_ID = "6048086721";
    public const string TECHNICIAN_Password = "0E52C1CD2F5B0C54A7AB27632910627A2A78946F";

    public const string USER_DEFAULT_Password = "0000";
    public const string RESET_PASSWORD_Successful = "Reset password successful!";
    public const string RESET_PASSWORD_Failed = "Reset password failed!";

    public const string HST_12_TAX_ID = "TAX001";
    public const string HST_5_TAX_ID = "TAX005";

    //Tool Tip
    public const string TOOL_TIP_LABEL_CashierName = "Double click to change password.";
    public const string TOOL_TIP_LABEL_VIPName = "Click to to ignore this Customer.";

    //User Activity Log
    public const string USER_ACTIVITY_Login = "Login";
    public const string USER_ACTIVITY_LoginFailed = "Login Failed";
    public const string USER_ACTIVITY_SignOut = "Sign Out";
    public const string USER_ACTIVITY_Exit = "Exit";
    public const string USER_ACTIVITY_ExitFailed = "Exit Failed";
    public const string USER_ACTIVITY_Sell = "Sell";
    public const string USER_ACTIVITY_Return = "Return";
    public const string USER_ACTIVITY_PriceOverride = "Price Override";
    public const string USER_ACTIVITY_ItemDiscount = "Item Discount";
    public const string USER_ACTIVITY_SubtotalDiscount = "Subtotal Discount";
    public const string USER_ACTIVITY_TaxIncluded = "Tax Included";
    public const string USER_ACTIVITY_12HSTIncluded = "12% HST Included";
    public const string USER_ACTIVITY_VIPSubtotalDiscount = "VIP Subtotal Discount";
    

    //Feature
    public const string SYSTEM_FEATURE_Payment= "Feature_Payment";
    public const string SYSTEM_SUB_FEATURE_Payment_CancelPayment = "Feature_Payment_CancelPayment";
    public const string SYSTEM_SUB_FEATURE_Payment_Cash = "Feature_Payment_Cash";
    public const string SYSTEM_SUB_FEATURE_Payment_Debit = "Feature_Payment_Debit";
    public const string SYSTEM_SUB_FEATURE_Payment_Visa = "Feature_Payment_Visa";
    public const string SYSTEM_SUB_FEATURE_Payment_MasterCard = "Feature_Payment_MasterCard";
    public const string SYSTEM_SUB_FEATURE_Payment_AE = "Feature_Payment_AE";
    public const string SYSTEM_SUB_FEATURE_Payment_StoreCredit = "Feature_Payment_StoreCredit";
    public const string SYSTEM_SUB_FEATURE_Payment_JCB = "Feature_Payment_JCB";
    public const string SYSTEM_SUB_FEATURE_Payment_USD = "Feature_Payment_USD";
    public const string SYSTEM_SUB_FEATURE_Payment_AccountReceivable = "Feature_Payment_AccountReceivable";
    public const string SYSTEM_SUB_FEATURE_Payment_ReturnWithoutReceipt = "Feature_Payment_ReturnWithoutReceipt"; 
    public const string SYSTEM_SUB_FEATURE_Payment_EBT = "Feature_Payment_EBT"; 
    public const string SYSTEM_SUB_FEATURE_Payment_Discover = "Feature_Payment_Discover"; 
    public const string SYSTEM_SUB_FEATURE_Payment_Check = "Feature_Payment_Check"; 
    public const string SYSTEM_SUB_FEATURE_Payment_GiftCard = "Feature_Payment_GiftCard";
    public const string SYSTEM_SUB_FEATURE_Payment_UnionPay = "Feature_Payment_UnionPay"; 
    //--
    public const string SYSTEM_FEATURE_Discount = "Feature_Discount";
    public const string SYSTEM_SUB_FEATURE_Discount_ItemDiscount = "Feature_Discount_ItemDiscount";
    public const string SYSTEM_SUB_FEATURE_Discount_OpenItemDiscount = "Feature_Discount_OpenItemDiscount";
    public const string SYSTEM_SUB_FEATURE_Discount_PriceOverride = "Feature_Discount_PriceOverride";
    public const string SYSTEM_SUB_FEATURE_Discount_SubTotalDiscount = "Feature_Discount_SubTotalDiscount";
    public const string SYSTEM_SUB_FEATURE_Discount_AllItemDiscount = "Feature_Discount_AllItemDiscount";
    public const string SYSTEM_SUB_FEATURE_Discount_OpenAllItemDiscount = "Feature_Discount_OpenAllItemDiscount";
    public const string SYSTEM_SUB_FEATURE_Discount_HSTIncluded = "Feature_Discount_HSTIncluded";
    public const string SYSTEM_SUB_FEATURE_Discount_VIPSubTotalDiscount = "Feature_Discount_VIPSubTotalDiscount";
    public const string SYSTEM_SUB_FEATURE_Discount_VIPSubTotalDiscountReminder = "Feature_Discount_VIPSubTotalDiscountReminder";
    public const string SYSTEM_SUB_FEATURE_Discount_DollarDiscount = "Feature_Discount_DollarDiscount";
    public const string SYSTEM_SUB_FEATURE_Discount_TaxExemption = "Feature_Discount_TaxExemption"; 

    //--
    public const string SYSTEM_FEATURE_Management = "Feature_Management";
    public const string SYSTEM_SUB_FEATURE_Management_Report = "Feature_Management_Report";
    public const string SYSTEM_SUB_FEATURE_Management_AllUserReport = "Feature_Management_AllUserReport";
    public const string SYSTEM_SUB_FEATURE_Management_Price = "Feature_Management_Price";
    public const string SYSTEM_SUB_FEATURE_Management_CheckInventory = "Feature_Management_CheckInventory";
    public const string SYSTEM_SUB_FEATURE_Management_ServiceCalling = "Feature_Management_ServiceCalling";
    public const string SYSTEM_SUB_FEATURE_Management_Product = "Feature_Management_Product";
    public const string SYSTEM_SUB_FEATURE_Management_Modifier = "Feature_Management_Modifier";
    public const string SYSTEM_SUB_FEATURE_Management_ModifierSetting = "Feature_Management_ModifierSetting";
    public const string SYSTEM_SUB_FEATURE_Management_ProductModifier = "Feature_Management_ProductModifier";
    public const string SYSTEM_SUB_FEATURE_Management_RePrintReceipt = "Feature_Management_RePrintReceipt";
    public const string SYSTEM_SUB_FEATURE_Management_RePrintKitchen = "Feature_Management_RePrintKitchen";
    public const string SYSTEM_SUB_FEATURE_Management_PriceSet = "Feature_Management_PriceSet";
    public const string SYSTEM_SUB_FEATURE_Management_CustomerSetting = "Feature_Management_CustomerSetting";
    public const string SYSTEM_SUB_FEATURE_Management_CustomerReceipt = "Feature_Management_CustomerReceipt";
    public const string SYSTEM_SUB_FEATURE_Management_MultiItemBarcodePrint = "Feature_Management_MultiItemBarcodePrint";
    public const string SYSTEM_SUB_FEATURE_Management_UserManagement = "Feature_Management_UserManagement";
    public const string SYSTEM_SUB_FEATURE_Management_OpenCashDrawer = "Feature_Management_OpenCashDrawer";
    public const string SYSTEM_SUB_FEATURE_Management_PaymentRecall = "PaymentRecall";
    public const string SYSTEM_SUB_FEATURE_Management_UpdateUSDRate = "Feature_Management_UpdateUSDRate";
    public const string SYSTEM_SUB_FEATURE_Management_InitDB = "Feature_Management_InitDB";
    public const string SYSTEM_SUB_FEATURE_Management_ConsignorManagement = "Feature_Management_ConsignorManagement";
    public const string SYSTEM_SUB_FEATURE_Management_CustomerManagement = "Feature_Management_CustomerManagement";
    public const string SYSTEM_SUB_FEATURE_Management_DeptCateManagement = "Feature_Management_DeptCateManagement";
    public const string SYSTEM_SUB_FEATURE_Management_InventoryManagement = "Feature_Management_InventoryManagement";
    public const string SYSTEM_SUB_FEATURE_Management_QuickButtonsPAD = "Feature_Management_QuickButtonsPAD";
    public const string SYSTEM_SUB_FEATURE_Management_Coupon = "Feature_Management_Coupon";
    public const string SYSTEM_SUB_FEATURE_Management_DeleteButtonPage = "Feature_Management_DeleteButtonPage";
    public const string SYSTEM_SUB_FEATURE_Management_GiftReceipt = "Feature_Management_GiftReceipt";
    public const string SYSTEM_SUB_FEATURE_Management_Calculator = "Feature_Management_Calculator";
    public const string SYSTEM_SUB_FEATURE_Management_Printer = "Feature_Management_Printer";
    public const string SYSTEM_FEATURE_UPDATE_Successful = "Update successful!";
    public const string SYSTEM_SUB_FEATURE_Management_Return = "Feature_Management_Return";
    public const string SYSTEM_SUB_FEATURE_Management_Search = "Feature_Management_Search";
    public const string SYSTEM_SUB_FEATURE_Management_Recall = "Feature_Management_Recall";
    public const string SYSTEM_SUB_FEATURE_Management_EOD = "Feature_Management_EOD";
    public const string SYSTEM_SUB_FEATURE_Management_ModifyProduct = "Feature_Management_ModifyProduct";
    public const string SYSTEM_SUB_FEATURE_Management_DeleteReport = "Feature_Management_DeleteReport";
    public const string SYSTEM_SUB_FEATURE_Management_VIPPassword = "Feature_Management_VIPPassword";
    //
    public const string DBSend_Status_Complete = "Complete";
    public const string DBSend_Status_Pending = "Pending";

    //Consignor
    public const string SYSTEM_SETTING_VALUEDATATYPE_String = "string";
    public const string SYSTEM_SETTING_VALUEDATATYPE_Decimal = "decimal";
    public const string SYSTEM_SETTING_VALUEDATATYPE_Int = "int";
    public const string SYSTEM_SETTING_VALUEDATATYPE_Boolean = "bool";
    public const string SYSTEM_SETTING_STATUS = "Available";
    public const string SYSTEM_SETTING_True = "True";
    public const string SYSTEM_SETTING_False = "False";
    public const string SYSTEM_SETTING_DESCRIPTION_UserAdd = "USER";
    public const string SYSTEM_SETTING_FUNCTIONNAME_ddlCity = "ddlCity";
    public const string SYSTEM_SETTING_FUNCTIONNAME_ddlProvince = "ddlProvince";
    public const string SYSTEM_SETTING_FUNCTIONNAME_ddlCountry = "ddlCountry";
    public const string SYSTEM_SETTING_FUNCTIONNAME_ddlNotSoldResult = "ddlNotSoldResult";
    public const string SYSTEM_SETTING_FUNCTIONNAME_nudFinalizedItemDay = "nudFinalizedItemDay";
    public const string SYSTEM_SETTING_FUNCTIONNAME_nudSharePercent = "nudSharePercent";
    public const string SYSTEM_SETTING_FUNCTIONNAME_ddlPurchaseMethod = "ddlPurchaseMethod";


    public const string SYSTEM_SETTING_FUNCTIONNAME_dtpReportFromTime = "dtpReportFromTime";
    public const string SYSTEM_SETTING_FUNCTIONNAME_dtpLastReportTime = "dtpLastReportTime"; 
    public const string SYSTEM_SETTING_FUNCTIONNAME_dtSoftwareUpdateDate = "SoftwareUpdateDate"; 
    public const string SYSTEM_SETTING_FUNCTIONNAME_dtpReportToTime = "dtpReportToTime";
    public const string SYSTEM_SETTING_FUNCTIONNAME_nudUSDRate = "nudUSDRate";
    public const string SYSTEM_SETTING_FUNCTIONNAME_HSTTaxIncludeRate = "HSTTaxIncludeRate";
    public const string SYSTEM_SETTING_FUNCTIONNAME_nudRewardPointDollars = "nudRewardPointDollars";
    public const string SYSTEM_SETTING_FUNCTIONNAME_nudSlideShowMinutes = "nudSlideShowMinutes";
    public const string SYSTEM_SETTING_FUNCTIONNAME_rbtEnableSlideShow = "rbtEnableSlideShow";
    public const string SYSTEM_SETTING_FUNCTIONNAME_SlideShowPath = "SlideShowPath";
    public const string SYSTEM_SETTING_FUNCTIONNAME_QuickButtonFilePath = "QuickButtonFilePath";

    public const string SYSTEM_SETTING_FUNCTIONNAME_nudQuickButtonColumn = "nudQuickButtonColumn";
    public const string SYSTEM_SETTING_FUNCTIONNAME_nudQuickButtonRow = "nudQuickButtonRow";

    public const string SYSTEM_SETTING_FUNCTIONNAME_PointDollarRate = "PointDollarRate";
    public const string SYSTEM_SETTING_FUNCTIONNAME_PointDollarRedeemRate = "PointDollarRedeemRate";
    public const string SYSTEM_SETTING_FUNCTIONNAME_LabelGeneratorPath = "LabelGeneratorPath";

    public const string SYSTEM_SETTING_SlideShowLeftPanel = "SYSTEM_SETTING_SlideShowLeftPanel";


    public const string VALIDATION_ERROR_MESSAGE_Warning = "Warning!";
    public const string VALIDATION_ERROR_MESSAGE_Oops = "Oops!";
    public const string VALIDATION_ERROR_MESSAGE_Reminder = "Reminder!";
    public const string VALIDATION_ERROR_MESSAGE_Information = "Information";
    public const string VALIDATION_ERROR_MESSAGE_FailureToSave = "Failure to Save! ";
    public const string VALIDATION_ERROR_MESSAGE_FailureToUpdate = "Failure to Update! ";
    public const string VALIDATION_ERROR_MESSAGE_RequireField = " is required.";
    public const string VALIDATION_ERROR_MESSAGE_CanNotBeDelete = " is required.";
    public const string VALIDATION_ERROR_MESSAGE_ThisFieldValue = "This field Value";
    public const string VALIDATION_ERROR_MESSAGE_DecimalDollarsGreaterThanZero = " must be in numeric , greater than zero and rounded to the nearest cent.";
    public const string VALIDATION_ERROR_MESSAGE_GreaterThanZero = " must be greater than zero.";
    public const string VALIDATION_ERROR_MESSAGE_DecimalQty = " must be in numeric or rounded to the second decimal place.";
    public const string VALIDATION_ERROR_MESSAGE_TheInputTooLong = "The input text is too long.";
    public const string VALIDATION_ERROR_MESSAGE_LoginIDAvailable = "Someone already has that Login ID. Try another?";

    public static string DATABASE_ACCESS_ERROR_MESSAGE = string.Empty;

    //Checkout Infomation
    public const string CHECK_OUT_INFO_TEXT_Tendered = "Tendered:";
    public const string CHECK_OUT_INFO_TEXT_Payout = "Payout:";
    public const string CHECK_OUT_INFO_TEXT_Change = "Change:";
    public const string CHECK_OUT_INFO_TEXT_Due = "Due:";
    public const string CHECK_OUT_INFO_TEXT_RefundDue = "Due:";
    public const string CHECK_OUT_INFO_TEXT_Overpayment = "Overpayment:";


    public const string Consignor_Search_Criteria_Name = "Name";
    public const string Consignor_Search_Criteria_IDNumber = "ID#";
    public const string Consignor_Search_Criteria_Phone = "Phone";
    public const string Consignor_Number_Deleted_Status = "Deleted";

    public const string CustomerCard_STATUS_Active = "Active";
    public const string CustomerCard_STATUS_Inactive = "Inactive";

    public const string SerialNumber_STATUS_Inbound = "Inbound";
    public const string SerialNumber_STATUS_Outbound = "Outbound";

    //Product Description
    public const string PRODUCT_STATUS_Available = "Active";
    public const string PRODUCT_STATUS_Pending = "Inactive";
    public const string PRODUCT_STATUS_Cancel = "Cancel";
    public const string PRODUCT_STATUS_Deleted = "Deleted";
    public const string PRODUCT_Weight_ea = "ea";

    public const string PRODUCT_DESCRIPTION_CONDITION_PreOwned = "Pre-owned";
    public const string PRODUCT_DESCRIPTION_CONDITION_BrandNew = "Brand New";

    public const string PRODUCT_DESCRIPTION_STOCKSTATUS_Available = "Available";
    public const string PRODUCT_DESCRIPTION_STOCKSTATUS_Sold = "Sold";
    public const string PRODUCT_DESCRIPTION_STOCKSTATUS_Deleted = "Deleted";
    public const string PRODUCT_DESCRIPTION_STOCKSTATUS_InTransaction = "InTransaction";
    public const string PRODUCT_DESCRIPTION_STOCKSTATUS_Hold = "HOLD";

    public const string SYSTEM_SETTING_FUNCTIONNAME_ddlUnitName = "ddlUnitName";

    public const string SYSTEM_SETTING_FUNCTIONNAME_ddlMadeIn = "ddlMadeIn";
    public const string SYSTEM_SETTING_FUNCTIONNAME_ddlCondition = "ddlCondition";
    public const string SYSTEM_SETTING_FUNCTIONNAME_ddlSubCondition = "ddlSubCondition";
    public const string SYSTEM_SETTING_FUNCTIONNAME_ddlSkin = "ddlSkin";
    public const string SYSTEM_SETTING_FUNCTIONNAME_ddlColor = "ddlColor";

    public const string PRODUCT_DESCRIPTION_SEARCH_FIELD_2 = "ddl_PD_Search_2";
    public const string PRODUCT_DESCRIPTION_SEARCH_FIELD_3 = "ddl_PD_Search_3";
    public const string PRODUCT_DESCRIPTION_SEARCH_FIELD_5 = "ddl_PD_Search_5";
    public const string PRODUCT_DESCRIPTION_SEARCH_FIELD_6 = "ddl_PD_Search_6";

    public const string PRODUCT_DESCRIPTION_TEXT_DDL_FIELD_07 = "ddl_PD_Text_DDL_07";
    public const string PRODUCT_DESCRIPTION_TEXT_DDL_FIELD_08 = "ddl_PD_Text_DDL_08";
    public const string PRODUCT_DESCRIPTION_TEXT_DDL_FIELD_09 = "ddl_PD_Text_DDL_09";
    public const string PRODUCT_DESCRIPTION_TEXT_DDL_FIELD_10 = "ddl_PD_Text_DDL_10";
    public const string PRODUCT_DESCRIPTION_TEXT_DDL_FIELD_11 = "ddl_PD_Text_DDL_11";
    public const string PRODUCT_DESCRIPTION_TEXT_DDL_FIELD_12 = "ddl_PD_Text_DDL_12";
    public const string PRODUCT_DESCRIPTION_STATUS_FIELD= "ddl_PD_Status";

    //Transaction
    public const string TRANSACTION_TYPE_Sale = "Sale";
    public const string TRANSACTION_TYPE_Return = "Return";
    public const string TRANSACTION_TYPE_SaleReturn = "Sale & Return";

    public const string TRANSACTION_STATUS_InTransaction = "InTransaction";
    public const string TRANSACTION_STATUS_Hold = "HOLD";
    public const string TRANSACTION_STATUS_AllVoid = "ALLVOID";
    public const string TRANSACTION_STATUS_Confirmed = "Confirmed";

    public const string TRANSACTION_POSTED = "True";

    public const string TRANSACTION_ITEM_POSTED = "True";

    public const string TRANSACTION_ITEM_STATUS_InTransaction = "InTransaction";
    public const string TRANSACTION_ITEM_STATUS_Hold = "HOLD";
    public const string TRANSACTION_ITEM_STATUS_Void = "VOID";
    public const string TRANSACTION_ITEM_STATUS_Confirmed = "Confirmed";

    public const string TRANSACTION_ITEM_KDSSTATUS_Empty = "";
    public const string TRANSACTION_ITEM_KDSSTATUS_Pending = "Pending";
    public const string TRANSACTION_ITEM_KDSSTATUS_Completed = "Completed";
    public const string TRANSACTION_ITEM_KDSSTATUS_Void = "VOID";

    public const string SPOONITY_DISCOUNT_Type_ItemDiscount = "ItemDiscount%";
    public const string SPOONITY_DISCOUNT_Type_ItemDollarDiscount = "ItemDiscount$";
    public const string SPOONITY_DISCOUNT_Type_SubtotalDiscount = "SubtotalDiscount%";
    public const string SPOONITY_DISCOUNT_Type_SubtotalDollarDiscount = "SubtotalDiscount$";

    //****Don't named the type as the same name*****
    public const string TRANSACTION_ITEM_Type_Item = "Item";
    public const string TRANSACTION_ITEM_Type_TaxFree = "TaxFree";
    public const string TRANSACTION_ITEM_Type_ItemDiscount = "ItemDiscount";
    public const string TRANSACTION_ITEM_Type_SubTotalDiscount = "SubtotalDiscount";
    public const string TRANSACTION_ITEM_Type_VIPDiscount = "VIPDiscount";// kinds of subtotal discount
    public const string TRANSACTION_ITEM_Type_TaxIncluded = "TaxIncluded";
    public const string TRANSACTION_ITEM_Type_Deposit = "Deposit";  //Oct 31, 2013
    public const string TRANSACTION_ITEM_Type_EHF = "EHF";  //Oct 31, 2013
    public const string TRANSACTION_ITEM_Type_CRF = "CRF";  //Nov 4, 2013
    
    public const string TRANSACTION_ITEM_PriceOverrideFlag_True = "True";
    public const string TRANSACTION_ITEM_PriceOverrideFlag_False = "False";

    public const string ITEM_TYPE_Sale = "Sale";
    public const string ITEM_TYPE_Return = "Return";
    public const string ITEM_TYPE_NoReceiptReturn = "NoReceiptReturn";

    //****Don't named the discountItemStatus same with normalItemStatus*****
    //public const string TRANSACTION_ITEM_STATUS_ItemDiscountInTransaction = "ItemDiscountInTransaction";
    //public const string TRANSACTION_ITEM_STATUS_ItemDiscountConfirmed = "ItemDiscountConfirmed";
    //public const string TRANSACTION_ITEM_STATUS_ItemDiscountHold = "ItemDiscountHOLD";

    //Tax
    public const string TAX_Available_FLAG_Yes = "Yes";
    public const string TAX_Available_FLAG_No = "No";

    //Payment
    public const string PAYMENT_TYPE_Payment = "Payment";
    public const string PAYMENT_TYPE_Refund = "Refund";

    public const string PAYMENT_METHOD_Cash = "Cash";
    public const string PAYMENT_METHOD_Debit = "Debit";
    public const string PAYMENT_METHOD_Visa = "Visa";
    public const string PAYMENT_METHOD_MasterCard = "MasterCard";
    public const string PAYMENT_METHOD_AE = "AmericanExpress";
    public const string PAYMENT_METHOD_JCB = "JCB";
    public const string PAYMENT_METHOD_StoreCredit = "StoreCredit";
    public const string PAYMENT_METHOD_USD = "USD";
    public const string PAYMENT_METHOD_VIPPoint = "VIPPoint";
    public const string PAYMENT_METHOD_VIPPayment = "VIPPayment";
    public const string PAYMENT_METHOD_EBT = "EBT Food Stamp"; 
    public const string PAYMENT_METHOD_EBTCash = "EBT Cash Benefit"; 
    public const string PAYMENT_METHOD_Discover = "Discover"; 
    public const string PAYMENT_METHOD_Check = "Check"; 
    public const string PAYMENT_METHOD_PayGiftCard = "GiftCard";
    public const string PAYMENT_METHOD_AccountReceivable = "A/R";
    public const string PAYMENT_METHOD_UnionPay = "UnionPay";
    public const string PAYMENT_METHOD_OtherCreditCard = "OtherCreditCard";
    public const string PAYMENT_METHOD_GiftCard = "GiftCard";
    public const string PAYMENT_METHOD_Diners_Club = "DinersClub";
    public const string PAYMENT_METHOD_Discover_Card = "DiscoverCard";
    public const string PAYMENT_METHOD_Online = "Online";

    public const string PAYMENT_STATUS_Hold = "HOLD";
    public const string PAYMENT_STATUS_InTransaction = "InTransaction";
    public const string PAYMENT_STATUS_Confirmed = "Confirmed";
    public const string PAYMENT_STATUS_OtherConfirmed = "OtherConfirmed";
    public const string PAYMENT_STATUS_Void = "VOID";

    public const string btnHoldAndRecall_TEXT_Hold = "HOLD";
    public const string btnHoldAndRecall_TEXT_Recall = "RE CALL";

    //ReceiptPrint
    public const string RECEIPT_ID_CustomerCopy = "R1";
    public const string RECEIPT_ID_GiftCopy = "R2";
    public const string RECEIPT_TEST_VIEW_TransactionNo = "0123456789212345";

    public const string WARNING_TITLE_TEXT_Reprint = "Duplicate";
    public const string WARNING_TITLE_TEXT_DayEndReport = "Day End Report";
    public const string WARNING_TITLE_TEXT_SoldProductReport = "Item Sold Report";
    public const string WARNING_TITLE_TEXT_ReturnProductReport = "Item Return Report";
    public const string WARNING_TITLE_TEXT_SalesReportByDepartment = "Sales Report By Dept.";
    public const string WARNING_TITLE_TEXT_SalesReportByCategory = "Sales Report By Category.";
    public const string WARNING_TITLE_TEXT_HourlyReport = "Hourly Report";
    public const string WARNING_TITLE_TEXT_Purchase = "Purchase";
    public const string WARNING_TITLE_TEXT_CashInOut = "Cash In / Out";
    public const string WARNING_TITLE_TEXT_CashierCount = "Cashier Count";
    public const string WARNING_TITLE_TEXT_DeleteReport = "Delete Report";
    public const string PRINTER_SETTING_PaperWidthInPixel = "PaperWidthInPixel"; 
    //Customer
    public const string CUSTOMER_STATUS_Available = "Available";
    public const string CUSTOMER_STATUS_Deleted = "Deleted";
    public const string CUSTOMER_NUMBER_Deleted = "Deleted";
    public const string CUSTOMER_Search_Criteria_Name = "Name";
    public const string CUSTOMER_Search_Criteria_IDNumber = "ID#";
    public const string CUSTOMER_Search_Criteria_Phone = "Phone";
    public const string CUSTOMER_Hello = "Hi";

    //Customer Discount Policy
    public const string DiscountPolicy_STATUS_Available = "Active";
    public const string DiscountPolicy_STATUS_Deleted = "Deleted";
    public const string DiscountPolicy_STATUS_Inactive = "Inactive";


    //Coupon Policy
    public const string CouponPolicy_STATUS_Available = "Active";
    public const string CouponPolicy_STATUS_Deleted = "Deleted";
    public const string CouponPolicy_STATUS_Inactive = "Inactive";

    //Coupon
    public const string Coupon_STATUS_Available = "Active";
    public const string Coupon_STATUS_Deleted = "Deleted";
    public const string Coupon_STATUS_VOID = "VOID";
      
    //USER
    public const string USER_STATUS_Active = "Active";
    public const string USER_STATUS_Deleted = "Deleted";
    public const string USER_STATUS_Inactive = "Inactive";
    public const string USER_Search_Criteria_Name = "Name";
    public const string USER_Search_Criteria_SIN = "SIN";
    public const string USER_Search_Criteria_Phone = "Phone";
    public const string USER_Search_Criteria_LoginID = "Login ID";

    public const string USER_ClockInOut_TYPE_In = "Clock In";
    public const string USER_ClockInOut_TYPE_Out = "Clock Out";

    //TOUR
    public const string TOUR_BOTTON_TEXT_CloseOut = "Close Tour";
    public const string TOUR_BOTTON_TEXT_Tour = "Tour";
    public const string TOUR_STATUS_Active = "Pending";

    //KDS
    public const string KDS_STATUS_Pending = "Pending";
    public const string KDS_STATUS_Completed = "Completed";

    //FireToKitchen
    public const string FireToKitchen_STATUS_Pending = "Pending";
    public const string FireToKitchen_STATUS_Completed = "Completed";

    //CashInOut
    public const string CASH_IN_OUT_TYPE_CashIn = "Cash In";
    public const string CASH_IN_OUT_TYPE_CashOut = "Cash Out";

    //ButtonControl
    public const int BUTTON_PAD_TYPE_DEFAULT_MAX_Column = 5;
    public const int BUTTON_PAD_TYPE_DEFAULT_MAX_Row = 5;
    public const string CONTROL_POSITION_Column = "Column";
    public const string CONTROL_POSITION_Row = "Row";
    public const string BUTTON_PAD_TYPE_QuickButton = "QuickButton";




    //ConsignorReceipt
    public const string CONSIGNOR_RECEIPT_STATUS_Confirmed = "Confirmed";

    //Scroll control
    public const int WM_SCROLL = 276; // Horizontal scroll
    public const int WM_VSCROLL = 277; // Vertical scroll
    public const int SB_LINEUP = 0; // Scrolls one line up
    public const int SB_LINELEFT = 0;// Scrolls one cell left
    public const int SB_LINEDOWN = 1; // Scrolls one line down
    public const int SB_LINERIGHT = 1;// Scrolls one cell right
    public const int SB_PAGEUP = 2; // Scrolls one page up
    public const int SB_PAGELEFT = 2;// Scrolls one page left
    public const int SB_PAGEDOWN = 3; // Scrolls one page down
    public const int SB_PAGERIGTH = 3; // Scrolls one page right
    public const int SB_PAGETOP = 6; // Scrolls to the upper left
    public const int SB_LEFT = 6; // Scrolls to the left
    public const int SB_PAGEBOTTOM = 7; // Scrolls to the upper right
    public const int SB_RIGHT = 7; // Scrolls to the right
    public const int SB_ENDSCROLL = 8; // Ends scroll

    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    public static extern int PostMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);


    //System Setting
    public const string SYSTEM_CONFIG_KEY_NAME_VersionType = "VersionType";
    public const string SYSTEM_CONFIG_KEY_NAME_Enterprise = "Enterprise";
    public const string SYSTEM_CONFIG_KEY_NAME_Enterprise_Function_Tour = "Enterprise_Tour";
    public const string SYSTEM_CONFIG_KEY_NAME_PKEnterpriseWebApisUrl = "PKEnterpriseWebApisUrl";
    public const string SYSTEM_CONFIG_KEY_NAME_POSEnterprise_Function_Promotion = "POS_Enterprise_Promotion";
    public const string SYSTEM_CONFIG_KEY_NAME_POSEnterprise_Function_POSProductPriceEdit = "POS_Enterprise_POSProductPriceEdit";
    public const string SYSTEM_CONFIG_KEY_NAME_DepartmentCategory = "POS_Function_DepartmentCategory";
    public const string SYSTEM_CONFIG_KEY_NAME_Weigh = "POS_Function_Weigh";
    public const string SYSTEM_CONFIG_KEY_NAME_LableSize = "LabelSize";
    public const string SYSTEM_CONFIG_KEY_NAME_LablePrintStartPoint = "LablePrintStartPoint";
    public const string SYSTEM_CONFIG_KEY_NAME_LablePrinterSpeed = "LablePrinterSpeed";
    public const string SYSTEM_CONFIG_KEY_NAME_LablePrinterDensity = "LablePrinterDensity";
    public const string SYSTEM_CONFIG_KEY_NAME_LablePrinterSensor = "LablePrinterSensor";
    public const string SYSTEM_CONFIG_KEY_NAME_LablePrinterShiftDistance = "LablePrinterShiftDistance";
    public const string SYSTEM_CONFIG_KEY_NAME_LablePrinterVerticalGap = "LablePrinterVerticalGap";
    public const string SYSTEM_CONFIG_KEY_NAME_KDSTitleFontSize = "KDSTitleFontSize";
    public const string SYSTEM_CONFIG_KEY_NAME_KDSSubTitleFontSize = "KDSSubTitleFontSize";
    public const string SYSTEM_CONFIG_KEY_NAME_SlideBackColor = "SlideBackColor";
    public const string SYSTEM_CONFIG_KEY_NAME_SlideTitleBackColor = "SlideTitleBackColor";
    public const string SYSTEM_CONFIG_KEY_NAME_ServiceCalling = "ServiceCalling";
    public const string SYSTEM_CONFIG_KEY_NAME_ServiceCallingMonitorIndex = "ServiceCallingMonitorIndex";
    public const string SYSTEM_CONFIG_KEY_NAME_ServiceCallingDigits = "ServiceCallingDigits";
    public const string SYSTEM_CONFIG_KEY_NAME_Scale_Serial_Port = "Scale_Serial_Port";
    public const string SYSTEM_CONFIG_KEY_NAME_ServiceCallingColums = "ServiceCallingColums";
    public const string SYSTEM_CONFIG_KEY_NAME_ServiceCallingRows = "ServiceCallingRows";
    public const string SYSTEM_CONFIG_KEY_NAME_Scale_Name = "Scale_Name";
    public const string SYSTEM_CONFIG_KEY_NAME_Spoonity_ApiKey = "SpoonityApiKey";
    public const string SYSTEM_CONFIG_KEY_NAME_Spoonity_EndPointAddress = "SpoonityEndPointAddress";
    public const string SYSTEM_CONFIG_KEY_NAME_Surveillance = "POS_Function_Surveillance";
    public const string SYSTEM_CONFIG_KEY_NAME_Surveillance_Type = "Surveillance_Type";
    public const string SYSTEM_CONFIG_KEY_NAME_Surveillance_IP = "Surveillance_IP";
    public const string SYSTEM_CONFIG_KEY_NAME_Surveillance_Port = "Surveillance_Port";
    public const string SYSTEM_CONFIG_KEY_NAME_Port_Listening_Time = "Port_Listening_Time";
    public const string SYSTEM_CONFIG_KEY_NAME_ClockInOut = "POS_Function_ClockInOut";
    public const string SYSTEM_CONFIG_KEY_NAME_CashierCount = "POS_Function_CashierCount";
    public const string SYSTEM_CONFIG_KEY_NAME_CashierLogin_Without_Checkout = "CashierLogin_Without_Checkout";
    public const string SYSTEM_CONFIG_KEY_NAME_KitchenPrintStartPoint = "KitchenPrintStartPoint";
    public const string SYSTEM_CONFIG_KEY_NAME_FireToKitchenOnHold = "FireToKitchenOnHold";
    public const string SYSTEM_CONFIG_KEY_NAME_FireToKitchenOnHoldWithTimer = "FireToKitchenOnHoldWithTimer";
    public const string SYSTEM_CONFIG_KEY_NAME_NonRevenue = "NonRevenue";
    public const string SYSTEM_CONFIG_KEY_NAME_AutoExitToPasswordScreen = "AutoExitToPasswordScreen";
    public const string SYSTEM_CONFIG_KEY_NAME_AutoTaxIncluded = "POS_Function_AutoTaxIncluded";
    public const string SYSTEM_CONFIG_KEY_NAME_SecondaryMonitorDisplay = "POS_Function_SecondaryMonitorDisplay";
    public const string SYSTEM_CONFIG_KEY_NAME_SecondLanguageSwitch = "POS_Function_SecondLanguageSwitch";
    public const string SYSTEM_CONFIG_VALUE_HoldPrint = "HoldPrint";
    public const string SYSTEM_CONFIG_VALUE_ScaleDefaultUnit = "ScaleDefaultUnit";
    public const string SYSTEM_CONFIG_KEY_NAME_ReceiptType = "ReceiptType";
    public const string SYSTEM_CONFIG_KEY_NAME_Receipt_HideVIPInfo = "Receipt_HideVIPInfo";
    public const string SYSTEM_CONFIG_KEY_NAME_Receipt_HideTourInfo = "Receipt_HideTourInfo";
    public const string SYSTEM_CONFIG_KEY_NAME_VIP_TotalPurchaseAmount = "VIP_TotalPurchaseAmount";
    public const string SYSTEM_CONFIG_KEY_NAME_NoSubtotalDiscount_After_ItemDiscount = "NoSubtotalDiscount_After_ItemDiscount";
    public const string SYSTEM_CONFIG_KEY_NAME_NoTaxIncluded_After_ItemDiscount = "NoTaxIncluded_After_ItemDiscount";
    public const string SYSTEM_CONFIG_KEY_NAME_NoVIPDiscount_After_ItemDiscount = "NoVIPDiscount_After_ItemDiscount";
    public const string SYSTEM_CONFIG_KEY_NAME_ShowPriceOnQB = "POS_Function_ShowPriceOnQB";
    public const string SYSTEM_CONFIG_KEY_NAME_ShowItemAmountOnPrimary = "POS_Function_ShowItemAmountOnPrimay";
    public const string SYSTEM_CONFIG_KEY_NAME_ShowItemPriceOnPrimary = "POS_Function_ShowItemPriceOnPrimay";
    public const string SYSTEM_CONFIG_KEY_NAME_ReportSettlementMode = "ReportSettlementMode";
    public const string SYSTEM_CONFIG_KEY_NAME_Default_Language = "Default_Language";
    public const string SYSTEM_CONFIG_KEY_NAME_Coupon = "POS_Function_Coupon";
    public const string SYSTEM_CONFIG_KEY_NAME_Coupon_TotalPurchaseAmount = "Coupon_TotalPurchaseAmount";
    public const string SYSTEM_CONFIG_KEY_NAME_DollarDiscount = "POS_Function_DollarDiscount";
    public const string SYSTEM_CONFIG_KEY_NAME_ProductsSpecialContorl = "POS_ProductsSpecialContorl"; 
    public const string SYSTEM_CONFIG_KEY_NAME_QuickButtonFontSize = "QuickButtonFontSize"; 
    public const string SYSTEM_CONFIG_KEY_NAME_ProductsCostContorl = "POS_ProductsCostContorl"; 
    public const string SYSTEM_CONFIG_KEY_NAME_ManualWeigh = "ManualWeigh"; 
    public const string SYSTEM_CONFIG_KEY_NAME_SameWeighWarning = "SameWeighWarning";
    public const string SYSTEM_CONFIG_KEY_NAME_ReceiptUserFormat = "ReceiptUserFormat";
    public const string SYSTEM_CONFIG_KEY_NAME_PriceOverrideHighLevelRole = "PriceOverrideHighLevelRole";
    public const string SYSTEM_CONFIG_KEY_NAME_ItemDiscountHighLevelRole = "ItemDiscountHighLevelRole";
    public const string SYSTEM_CONFIG_KEY_NAME_DollarDiscountHighLevelRole = "DollarDiscountHighLevelRole";
    public const string SYSTEM_CONFIG_KEY_NAME_TaxExemptionHighLevelRole = "TaxExemptionHighLevelRole";
    public const string SYSTEM_CONFIG_KEY_NAME_VoidAllHighLevelRole = "VoidAllHighLevelRole";
    public const string SYSTEM_CONFIG_KEY_NAME_VoidHighLevelRole = "VoidHighLevelRole";
    public const string SYSTEM_CONFIG_KEY_NAME_VoidControlAmount = "VoidControlAmount";
    public const string SYSTEM_CONFIG_KEY_NAME_RefuseLogoutWithHold = "RefuseLogoutWithHold";
    public const string SYSTEM_CONFIG_KEY_NAME_ReceiptByDepartment = "ReceiptByDepartment";
    public const string SYSTEM_CONFIG_KEY_NAME_PrintCallingNumber = "PrintCallingNumber";
    public const string SYSTEM_CONFIG_KEY_NAME_KitchenReceipt = "KitchenReceipt";
    public const string SYSTEM_CONFIG_KEY_NAME_VIPAmountPasswordProtection = "VIPAmountPasswordProtection";
    public const string SYSTEM_CONFIG_KEY_NAME_AllowTips = "AllowTips";
    public const string SYSTEM_CONFIG_KEY_NAME_SMTPHOST = "SMTPHostName";
    public const string SYSTEM_CONFIG_KEY_NAME_SMTPPORT = "SMTPPort";
    public const string SYSTEM_CONFIG_KEY_NAME_SMTPUSERNAME = "SMTPUserName";
    public const string SYSTEM_CONFIG_KEY_NAME_SMTPPASSWORD = "SMTPPassword";
    public const string SYSTEM_CONFIG_KEY_NAME_SMTPENABLESSL = "SMTPEnableSSL";
    public const string SYSTEM_CONFIG_KEY_NAME_EMAILSENDER = "EMailSender";
    public const string SYSTEM_CONFIG_KEY_NAME_VIPSENDRECEIPTBYEMAIL = "SendReceipttoVIPEmailAddress";

    public const string SYSTEM_CONFIG_KEY_NAME_ViewAllHoldTransactions = "ViewAllHoldTransactions";
    public const string SYSTEM_CONFIG_KEY_NAME_DeleteHold = "DeleteHold";
    public const string SYSTEM_CONFIG_KEY_NAME_PaymentMethod = "PaymentMethod";
    public const string SYSTEM_CONFIG_KEY_NAME_OldModifier = "OldModifier";
    public const string SYSTEM_CONFIG_VALUE_SingleLine = "SingleLine";
    public const string SYSTEM_CONFIG_VALUE_MultiLine = "MultiLine";
    public const string SYSTEM_CONFIG_KEY_NAME_LabelModifier = "Label_Modifier";
    public const string SYSTEM_CONFIG_KEY_NAME_PrintPriceSetOnInvoice = "PrintPriceSetOnInvoice";
    public const string SYSTEM_CONFIG_KEY_NAME_AllowProductSN = "AllowProductSN";

    public const string SYSTEM_CONFIG_KEY_NAME_TOGOMODE = "ToGoMode";
    public const string SYSTEM_CONFIG_KEY_NAME_TOGODEFAULT = "Default";
    public const string SYSTEM_CONFIG_KEY_NAME_TOGOPROMPT = "Prompt";

    public const string SYSTEM_CONFIG_KEY_NAME_Industry = "Industry";
    public const string SYSTEM_CONFIG_KEY_NAME_Retail = "Retail";
    public const string SYSTEM_CONFIG_KEY_NAME_Foodcourt = "Foodcourt";
    public const string SYSTEM_CONFIG_KEY_NAME_PaymentConfirm = "PaymentConfirm";
    public const string SYSTEM_CONFIG_KEY_NAME_AllowLetterBarcode = "AllowLetterBarcode";
    public const string SYSTEM_CONFIG_KEY_NAME_ProductLanguage = "ProductLanguage";
    public const string SYSTEM_CONFIG_KEY_NAME_MaximumHoldNumber = "MaximumHoldNumber";
    public const string SYSTEM_CONFIG_KEY_NAME_SweetenedTaxFreeCount = "SweetenedTaxFreeCount";
    public const string SYSTEM_CONFIG_KEY_NAME_Inquire_Receipt_Print = "Inquire_Receipt_Print"; 
    public const string SYSTEM_CONFIG_KEY_NAME_Promotion_Receipt_Consolidated = "Promotion_Receipt_Consolidated"; 
    public const string SYSTEM_CONFIG_KEY_NAME_ItemDisc_Receipt_Consolidated = "ItemDiscount_Receipt_Consolidated";
    public const string SYSTEM_CONFIG_KEY_NAME_PennyRounding = "PennyRounding"; 
    public const string SYSTEM_CONFIG_KEY_NAME_ThreeDecimalQty = "Three_DecimalPlace_For_Qty"; 
    public const string SYSTEM_CONFIG_KEY_NAME_VIPFunction = "VIP_Function"; 
    public const string SYSTEM_CONFIG_KEY_NAME_SendDeletedToKitchen = "SendDeletedItemToKitchen";
    public const string SYSTEM_CONFIG_KEY_NAME_ReportOpenCasherDrawer = "ReportOpenCasherDrawer";
    public const string SYSTEM_CONFIG_KEY_NAME_AlwaysOpenCashDrawer = "AlwaysOpenCashDrawer"; 
    public const string SYSTEM_CONFIG_KEY_NAME_CombineWeightItem = "CombineWeightItem";
    public const string SYSTEM_CONFIG_KEY_NAME_SyncAllowLocalQuickButton = "SyncAllowLocalQuickButton"; 
    public const string SYSTEM_CONFIG_KEY_NAME_CombineEAItem = "CombineEAItem";
    public const string SYSTEM_CONFIG_KEY_NAME_ReceiptPrintModifier = "ReceiptPrintModifier";
    public const string SYSTEM_CONFIG_KEY_NAME_ReceiptSingleLineModifier = "ReceiptSingleLineModifier";
    public const string SYSTEM_CONFIG_KEY_NAME_PaymentList = "PaymentList"; 
    public const string SYSTEM_CONFIG_KEY_NAME_CashInOut = "Cash_In_Out";
    public const string SYSTEM_CONFIG_KEY_NAME_ChangePWD = "ChangUserPWD";
    public const string SYSTEM_CONFIG_KEY_NAME_CashInOutReason = "CashInOutReason";
    public const string SYSTEM_CONFIG_KEY_NAME_CallingList = "CallingList";
    public const string SYSTEM_CONFIG_VALUE_NotPrint = "NotPrint";
    public const string SYSTEM_CONFIG_VALUE_PrintLayaway = "PrintLayaway";
    public const string SYSTEM_CONFIG_VALUE_PrintBarcode = "PrintBarcode";
    public const string SYSTEM_CONFIG_KEY_NAME_Resolution = "Resolution"; 
    public const string SYSTEM_CONFIG_KEY_NAME_ReceiptLanguage = "ReceiptLanguage"; 
    public const string SYSTEM_CONFIG_KEY_NAME_KitchenLanguage = "KitchenLanguage";
    public const string SYSTEM_CONFIG_KEY_NAME_ReceiptBarcode = "ReceiptBarcode";
    public const string SYSTEM_CONFIG_KEY_NAME_PSTIDList = "PSTIDList"; 
    public const string SYSTEM_CONFIG_KEY_NAME_AutoCloseChangeWindow = "AutoCloseChangeWindow"; 
    public const string SYSTEM_CONFIG_KEY_NAME_PayGatewaySetting = "PayGatewaySetting";
    public const string SYSTEM_CONFIG_KEY_NAME_PaymentGroup = "PaymentGroup";
    public const string SYSTEM_CONFIG_KEY_NAME_PayGatewayPort = "PayGatewayPort";
    public const string SYSTEM_CONFIG_KEY_NAME_PayGatewayIP = "PayGatewayIP";
    public const string SYSTEM_CONFIG_KEY_NAME_TaxIDExemption = "TaxIDExemption";
    public const string SYSTEM_CONFIG_KEY_NAME_FourDecimalPlaceTax = "Precision_FourthDecimalPlaceTax";
    public const string SYSTEM_CONFIG_KEY_NAME_GIBO = "GIBO";
    public const string SYSTEM_CONFIG_KEY_NAME_QuickSearch = "QuickSearch";
    public const string SYSTEM_CONFIG_KEY_NAME_ReceiptPrinter = "ReceiptPrinter";
    public const string SYSTEM_CONFIG_KEY_NAME_TouchScreenEnterButton = "TouchScreenEnterButton";
    public const string SYSTEM_CONFIG_KEY_NAME_VIPSolution = "VIPSolution";
    public const string SYSTEM_CONFIG_KEY_NAME_ReceiptShowPoints = "ReceiptShowPoints";
    //--nick: Add three settings for auto-backup Database 
    public const string SYSTEM_CONFIG_KEY_NAME_DBBACUPFOLDER = "DBBackupFolder";  
    public const string SYSTEM_CONFIG_KEY_NAME_DBBACUP_HOUR = "DBBackupHour";
    public const string SYSTEM_CONFIG_KEY_NAME_DBBACUP_MINUTE = "DBBackupMinute"; 
    public const string SYSTEM_CONFIG_KEY_NAME_TransactionKeepDays = "TransactionKeepDays";
    public const string SYSTEM_CONFIG_KEY_NAME_SyncAutoStart = "SyncAutoStart";
    public const string SYSTEM_CONFIG_KEY_NAME_SyncAllowUpload = "SyncAllowUpload";
    public const string SYSTEM_CONFIG_KEY_NAME_SyncAllowDownload = "SyncAllowDownload";
    public const string SYSTEM_CONFIG_KEY_NAME_NoBackup = "NoBackup";

    public const string SYSTEM_CONFIG_KEY_NAME_Recall = "AllowRecall";
    public const string SYSTEM_CONFIG_KEY_NAME_EOD = "AllowEOD";
    public const string SYSTEM_CONFIG_KEY_NAME_PADSHOWIMG = "PADShowImage";
    public const string SYSTEM_CONFIG_KEY_NAME_VIPPointPayTax = "AllowVIPPointPayTax";
    public const string SYSTEM_CONFIG_KEY_NAME_LaundryMode = "AllowLaundryMode";
    public const string SYSTEM_CONFIG_KEY_NAME_VIPPointCollectAtCharge = "AllowVIPPointCollectAtCharge";
    public const string SYSTEM_CONFIG_KEY_NAME_LogOutWhileInTransaction = "AllowLogOutWhileInTransaction";

    public const string SYSTEM_CONFIG_KEY_NAME_DefaultOrderNote = "DefaultOrderNote";

    //KDS
    public const string SYSTEM_CONFIG_KEY_NAME_KDSRowCount = "KDSRowCount";
    public const string SYSTEM_CONFIG_KEY_NAME_KDSColCount = "KDSColCount";
    public const string SYSTEM_CONFIG_KEY_NAME_KDSSelectedForeColor = "KDSSelectedForeColor";
    public const string SYSTEM_CONFIG_KEY_NAME_KDSNonSelectedForeColor = "KDSNonSelectedForeColor";
    public const string SYSTEM_CONFIG_KEY_NAME_KDSFiveMinOldForeColor = "KDSFiveMinOldForeColor";
    public const string SYSTEM_CONFIG_KEY_NAME_KDSTenMinOldForeColor = "KDSTenMinOldForeColor";
    public const string SYSTEM_CONFIG_KEY_NAME_KDSRecallForeColor = "KDSRecallForeColor";
    public const string SYSTEM_CONFIG_KEY_NAME_KDSSelectedBackColor = "KDSSelectedBackColor";
    public const string SYSTEM_CONFIG_KEY_NAME_KDSNonSelectedBackColor = "KDSNonSelectedBackColor";
    public const string SYSTEM_CONFIG_KEY_NAME_KDSFiveMinOldBackColor = "KDSFiveMinOldBackColor";
    public const string SYSTEM_CONFIG_KEY_NAME_KDSTenMinOldBackColor = "KDSTenMinOldBackColor";
    public const string SYSTEM_CONFIG_KEY_NAME_KDSRecallBackColor = "KDSRecallBackColor";
    //

    //Online
    public const string SYSTEM_CONFIG_KEY_NAME_EnableOnline = "EnableOnline";
    public const string SYSTEM_CONFIG_KEY_NAME_PrintSummary = "PrintSummary";
    public const string SYSTEM_CONFIG_KEY_NAME_SummaryLanguage = "SummaryLanguage";
    public const string SYSTEM_CONFIG_KEY_NAME_SoundFile = "SoundFile";
    public const string SYSTEM_CONFIG_KEY_NAME_EnableSoundFile = "EnableSoundFile";
    public const string SYSTEM_CONFIG_KEY_NAME_OnlineMerchantId = "MerchantId";
    public const string SYSTEM_CONFIG_KEY_NAME_OnlineEndPointAddress = "OnlineEndPointAddress";
    public const string SYSTEM_CONFIG_KEY_NAME_OnlineTimerInterval = "OnlineTimerInterval";
    public const string SYSTEM_CONFIG_KEY_NAME_OnlineProductZeroPrice = "OnlineProductZeroPrice";
    //

    public const string SYSTEM_CONFIG_KEY_NAME_VoidRecallOnly = "VoidRecallOnly";
    //KIOSK(SelfPOS)
    public const string SYSTEM_CONFIG_KEY_NAME_UIType = "UIType";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskType = "KioskType";
    public const string SYSTEM_CONFIG_KEY_NAME_ShowTransactionSeqDigitsInKiosk = "ShowTransactionSeqDigitsInKiosk";
    public const string SYSTEM_CONFIG_KEY_NAME_TransactionSeqDigitsDescription = "TransactionSeqDigitsDescription";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskTimeOutInterval = "KioskTimeOutInterval";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskPADPriceSize = "KioskPADPriceSize";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskCashPaymentConfirm = "KioskCashPaymentConfirm";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskShowDayEndAfterScan = "KioskShowDayEndAfterScan";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskCallStaffMessage = "KioskCallStaffMessage";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskPadButtonRadius = "KioskPadButtonRadius";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskChangePassword = "KioskChangePassword";
    public const string SYSTEM_CONFIG_KEY_NAME_ReloadUrl = "KioskReloadUrl";

    //Kiosk Modifier(Since not included in database)
    public const string SYSTEM_CONFIG_KEY_NAME_KioskModifierProductNameFontSize = "KioskModifierProductNameFontSize";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskModifierDescriptionComboFontSize = "KioskModifierDescriptionComboFontSize";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskModifierButtonFontSize = "KioskModifierButtonFontSize";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskModifierButtonRadius = "KioskModifierButtonRadius";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskModifierButtonGroupLabelFontSize = "KioskModifierButtonGroupLabelFontSize";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskModifierButtonGroupLabelHeight = "KioskModifierButtonGroupLabelHeight";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskModifierButtonRowHeigt = "KioskModifierButtonRowHeigt";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskModifierButtonMaxCol = "KioskModifierButtonMaxCol";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskModifierCommandButtonFontSize = "KioskModifierCommandButtonFontSize";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskModifierCommandButtonRadius = "KioskModifierCommandButtonRadius";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskModifierCommandButtonColumnCount = "KioskModifierCommandButtonColumnCount";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskModifierCommandButtonRowHeight = "KioskModifierCommandButtonRowHeight";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskModifierComboButtonFontSize = "KioskModifierComboButtonFontSize";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskModifierComboButtonRadius = "KioskModifierComboButtonRadius";
    public const string SYSTEM_CONFIG_KEY_NAME_KioskModifierComboButtonHeight = "KioskModifierComboButtonHeight";
    //public const string SYSTEM_CONFIG_KEY_NAME_KioskModifierControlButtonRadius = "KioskModifierControlButtonRadius";


    public const string SYSTEM_CONFIG_VALUE_VERSION_TYPE_Consignor = "ConsignorVersion";
    public const string SYSTEM_CONFIG_VALUE_VERSION_TYPE_BookStore = "BookStoreVersion";
    public const string SYSTEM_CONFIG_VALUE_Enterprise_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_Enterprise_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_Enterprise_Tour_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_Enterprise_Tour_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_POSEnterprise_Promotion_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_POSEnterprise_Promotion_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_POSEnterprise_POSProductPriceEdit_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_POSEnterprise_POSProductPriceEdit_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_DepartmentCategory_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_DepartmentCategory_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_Surveillance_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_Surveillance_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_Weigh_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_Weigh_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_ClockInOut_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_ClockInOut_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_CashierCount_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_CashierCount_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_CashierLoginWithoutCheckout_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_CashierLoginWithoutCheckout_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_FireToKitchenOnHold_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_FireToKitchenOnHold_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_FireToKitchenOnHoldWithTimer_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_FireToKitchenOnHoldWithTimer_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_NonRevenue_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_NonRevenue_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_AutoTaxIncluded_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_AutoTaxIncluded_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_SecondaryMonitorDisplay_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_SecondaryMonitorDisplay_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_RECEIPT_TYPE_Normal = "Normal";
    public const string SYSTEM_CONFIG_VALUE_RECEIPT_TYPE_Simple = "Simple";
    public const string SYSTEM_CONFIG_VALUE_RECEIPT_TYPE_TaxIncluded = "TaxIncluded";
    public const string SYSTEM_CONFIG_VALUE_RECEIPT_TYPE_LaundryInvoice = "LaundryInvoice";
    public const string SYSTEM_CONFIG_VALUE_Receipt_HideVIPInfo_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_Receipt_HideVIPInfo_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_Receipt_ShowPoints_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_Receipt_ShowPoints_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_Receipt_HideTourInfo_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_Receipt_HideTourInfo_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_VIP_TotalPurcahseAmount_WithoutTax = "WithoutTax";
    public const string SYSTEM_CONFIG_VALUE_VIP_TotalPurcahseAmount_WithTax = "WithTax";
    public const string SYSTEM_CONFIG_VALUE_Multiple_Discount_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_Multiple_Discount_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_ShowPriceOnQB_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_ShowPriceOnQB_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_ShowItemAmountOnPrimary_Enable = "Enable";    
    public const string SYSTEM_CONFIG_VALUE_ShowItemAmountOnPrimary_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_SecondLanguageSwitch_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_SecondLanguageSwitch_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_DefaultLanguage_1 = "1";
    public const string SYSTEM_CONFIG_VALUE_DefaultLanguage_2 = "2";
    public const string SYSTEM_CONFIG_VALUE_COUPON_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_COUPON_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_COUPON_TotalPurcahseAmount_BeforeTax = "BeforeTax";
    public const string SYSTEM_CONFIG_VALUE_COUPON_TotalPurcahseAmount_AfterTax = "AfterTax";
    public const string SYSTEM_CONFIG_VALUE_DollarDiscount_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_DollarDiscount_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_FourthDecimalPlaceTax_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_FourthDecimalPlaceTax_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_ProductsSpecialContorl_Enable = "Enable"; 
    public const string SYSTEM_CONFIG_VALUE_ProductsSpecialContorl_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_ProductsCostContorl_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_ProductsCostContorl_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_ReportByCashier = "ReportByCashier";
    public const string SYSTEM_CONFIG_VALUE_GIBOReport = "GIBOReport";
    public const string SYSTEM_CONFIG_VALUE_MonitorFont = "MonitorFont";
    public const string SYSTEM_CONFIG_VALUE_VIPTypeGroup = "Group";
    //GIBO
    public const string SYSTEM_CONFIG_VALUE_GIBO_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_GIBO_Disable = "Disable";
    //
    public const string SYSTEM_CONFIG_VALUE_TerminalID = "TerminalID";
    public const string SYSTEM_CONFIG_VALUE_TerminalName = "TerminalName";
    public const string SYSTEM_CONFIG_VALUE_KDSName = "KDSName";

    public const string SYSTEM_CONFIG_VALUE_Recall_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_Recall_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_EOD_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_EOD_Disable = "Disable";
    public const string SYSTEM_CONFIG_VALUE_VIPPointPayTax_Enable = "Enable";
    public const string SYSTEM_CONFIG_VALUE_VIPPointPayTax_Disable = "Disable";

    public const string SYSTEM_CONFIG_VALUE_UIMode_KIOSK = "KIOSK";
    public const string SYSTEM_CONFIG_VALUE_UIMode_KIOSKPOS = "KIOSKPOS";

    //Department and Category //****Don't named the same name.*****
    public const string CREATE_NEW_Department = "NewDepartment";
    public const string CREATE_NEW_Category = "NewCategory";
    public const string UPDATE_Department = "UpdateDepartment";
    public const string UPDATE_Category = "UpdateCategory";

    //UI Label Display //****Don't named the same name.*****
    public const string UI_PRODUCT_DESCRIPTION_lbSearch1 = "PRODUCT_DESCRIPTION_lbSearch1";
    public const string UI_PRODUCT_DESCRIPTION_lbSearch2 = "PRODUCT_DESCRIPTION_lbSearch2";
    public const string UI_PRODUCT_DESCRIPTION_lbSearch3 = "PRODUCT_DESCRIPTION_lbSearch3";
    public const string UI_PRODUCT_DESCRIPTION_lbSearch4 = "PRODUCT_DESCRIPTION_lbSearch4";
    public const string UI_PRODUCT_DESCRIPTION_lbSearch5 = "PRODUCT_DESCRIPTION_lbSearch5";
    public const string UI_PRODUCT_DESCRIPTION_lbSearch6 = "PRODUCT_DESCRIPTION_lbSearch6";
    public const string UI_PRODUCT_DESCRIPTION_lbText01 = "PRODUCT_DESCRIPTION_lbText01";
    public const string UI_PRODUCT_DESCRIPTION_lbText02 = "PRODUCT_DESCRIPTION_lbText02";
    public const string UI_PRODUCT_DESCRIPTION_lbText03 = "PRODUCT_DESCRIPTION_lbText03";
    public const string UI_PRODUCT_DESCRIPTION_lbText04 = "PRODUCT_DESCRIPTION_lbText04";
    public const string UI_PRODUCT_DESCRIPTION_lbText05 = "PRODUCT_DESCRIPTION_lbText05";
    public const string UI_PRODUCT_DESCRIPTION_lbText06 = "PRODUCT_DESCRIPTION_lbText06";
    public const string UI_PRODUCT_DESCRIPTION_lbTextDDL07 = "PRODUCT_DESCRIPTION_lbTextDDL07";
    public const string UI_PRODUCT_DESCRIPTION_lbTextDDL08 = "PRODUCT_DESCRIPTION_lbTextDDL08";
    public const string UI_PRODUCT_DESCRIPTION_lbTextDDL09 = "PRODUCT_DESCRIPTION_lbTextDDL09";
    public const string UI_PRODUCT_DESCRIPTION_lbTextDDL10 = "PRODUCT_DESCRIPTION_lbTextDDL10";
    public const string UI_PRODUCT_DESCRIPTION_lbTextDDL11 = "PRODUCT_DESCRIPTION_lbTextDDL11";
    public const string UI_PRODUCT_DESCRIPTION_lbTextDDL12 = "PRODUCT_DESCRIPTION_lbTextDDL12";
    public const string UI_PRODUCT_DESCRIPTION_lbDecimal1 = "PRODUCT_DESCRIPTION_lbDecimal1";
    public const string UI_PRODUCT_DESCRIPTION_lbDecimal2 = "PRODUCT_DESCRIPTION_lbDecimal2";
    public const string UI_PRODUCT_DESCRIPTION_lbDecimal3 = "PRODUCT_DESCRIPTION_lbDecimal3";
    public const string UI_PRODUCT_DESCRIPTION_lbDecimal4 = "PRODUCT_DESCRIPTION_lbDecimal4";
    public const string UI_PRODUCT_DESCRIPTION_lbInt1 = "PRODUCT_DESCRIPTION_lbInt1";
    public const string UI_PRODUCT_DESCRIPTION_lbInt2 = "PRODUCT_DESCRIPTION_lbInt2";
    public const string UI_PRODUCT_DESCRIPTION_lbInt3 = "PRODUCT_DESCRIPTION_lbInt3";
    public const string UI_PRODUCT_DESCRIPTION_lbInt4 = "PRODUCT_DESCRIPTION_lbInt4";
    public const string UI_PRODUCT_DESCRIPTION_lbTF1 = "PRODUCT_DESCRIPTION_lbTF1";
    public const string UI_PRODUCT_DESCRIPTION_lbTF2 = "PRODUCT_DESCRIPTION_lbTF2";
    public const string UI_PRODUCT_DESCRIPTION_lbTF3 = "PRODUCT_DESCRIPTION_lbTF3";
    public const string UI_PRODUCT_DESCRIPTION_lbTF4 = "PRODUCT_DESCRIPTION_lbTF4";
    public const string UI_PRODUCT_DESCRIPTION_lbTF5 = "PRODUCT_DESCRIPTION_lbTF5";
    public const string UI_PRODUCT_DESCRIPTION_lbDateTime1 = "PRODUCT_DESCRIPTION_lbDateTime1";
    public const string UI_PRODUCT_DESCRIPTION_lbDateTime2 = "PRODUCT_DESCRIPTION_lbDateTime2";
    public const string UI_PRODUCT_DESCRIPTION_lbDateTime3 = "PRODUCT_DESCRIPTION_lbDateTime3";
    public const string UI_PRODUCT_DESCRIPTION_lbDateTime4 = "PRODUCT_DESCRIPTION_lbDateTime4";
    public const string UI_PRODUCT_DESCRIPTION_lbStatus = "PRODUCT_DESCRIPTION_lbStatus";
    public const string UI_PRODUCT_DESCRIPTION_lbFilePath = "PRODUCT_DESCRIPTION_lbFilePath";



    //Synchronization
    public const string DOWNLOAD_SERVER_DATA_Successful = "Download server data successful!";
    public const string UPLOAD_LOCAL_DATA_Successful = "Upload local data successful!";
    public const string DOWNLOAD_SERVER_DATA_Fail = "Download server data fail. Please try again.";
    public const string UPLOAD_LOCAL_DATA_Fail = "Upload local data fail. Please try again";
    public const string ADD_CUSTOMER_2_SERVER_ERROR = "Failure to save Customer at server database! Please try again later.";
    public const string UPDATE_CUSTOMER_2_SERVER_ERROR = "Failure to update Customer at server database! Please try again later.";
    public const string DELETE_CUSTOMER_2_SERVER_ERROR = "Failure to delete Customer at server database! Please try again later.";
    public const string SYNCHRONIZATION_CUSTOMER_FROM_SERVER_ERROR = "Failure to get Customer data from server! Please try again later.";
    public const string USER_CHANGE_PASSWORD_2_SERVER_ERROR = "Failure to change password at server database! Please try again later.";
    public const string DOWNLOAD_USER_NEW_PASSWORD_FROM_SERVER_ERROR = "Failure to update POS user password! Please do user synchronization later.";

    //Const string
    public const string String_Manual_Weight = "MAN WT";


    //Surveillance Type
    public const string Surveillance_Type_1 = "File";
    public const string Surveillance_Type_2 = "Network";

    //Scale
    public const string SCALE_NAME_1 = "Avery Berkel 6720-15";
    public const string SCALE_NAME_2 = "Brecknell 67XX";
    public const string SCALE_NAME_3 = "Magellan 8405";
    public const string SCALE_NAME_4 = "Kilotech PD-1";
    public const string SCALE_FUNCTION_IS_DISABLE = "Scale function is disabled.";
    public const string WEIGHT_ZERO_MESSAGE = "Weight is 0";
    public const string PLEASE_TRY_AGAIN = "Please try again!";
    public const string PLEASE_CHECK_ITEM_UNIT_AND_WEIGHT_CHECKBOX = "Please check Item unit name and Weigh-CheckBox.";
    public const string SERIAL_PORT_IS_UNAVAILABLE = "Serial port is unavailable.";
    public const string PRODUCT_UNIT_NAME_LB = "LB";
    public const string PRODUCT_UNIT_NAME_KG = "KG";
    public const string SALES_Qty_IS_NOT_AN_INTEGER = "Sales quantity is not an integer.";

    //User Role
    public const string USER_ROLE_Cashier_1 = "Role_Cashier_1";
    public const string USER_ROLE_Cashier_2 = "Role_Cashier_2";
    public const string USER_ROLE_Cashier_3 = "Role_Cashier_3";
    public const string USER_ROLE_Manager_1 = "Role_Manager_1";
    public const string USER_ROLE_Manager_2 = "Role_Manager_2";
    public const string USER_ROLE_Owner = "Role_Owner";

    //Promotion
    public const string PROMOTION_STATUS_ACTIVE = "Active";
    public const string PROMOTION_STATUS_INACTIVE = "Inactive";

    //Price Set
    public const string SYSTEM_CONFIG_KEY_NAME_PriceA = "Price A";
    public const string SYSTEM_CONFIG_KEY_NAME_PriceB = "Price B";
    public const string SYSTEM_CONFIG_KEY_NAME_PriceC = "Price C";
    public const string SYSTEM_CONFIG_KEY_NAME_PriceD = "Price D";
    public const string SYSTEM_CONFIG_KEY_NAME_PriceE = "Price E";

    public const string SYSTEM_CONFIG_KEY_NAME_PriceF = "Price F";
    public const string SYSTEM_CONFIG_KEY_NAME_PriceG = "Price G";
    public const string SYSTEM_CONFIG_KEY_NAME_PriceH = "Price H";
    public const string SYSTEM_CONFIG_KEY_NAME_PriceI = "Price I";
    public const string SYSTEM_CONFIG_KEY_NAME_PriceJ = "Price J";

    public enum Promotion
    {
        MixMatch = 0,
        Combo = 1,
        Quantity = 2, 
        BOM = 3,
        Weekly = 4,
        Daily = 5,
        TaxFree = 6,
        Qty2=7,
        Combo1 = 8,
        Combo2 = 9,
        VIPPoint = 10
    }


    public enum LogCode
    {
        Debug = 7
    }
    
}



