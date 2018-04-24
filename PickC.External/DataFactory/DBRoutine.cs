using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickC.External.DataFactory
{
    public static class DBRoutine
    {

        

        /// <summary>
        /// [Config].[LookUp]
        /// </summary>

        public const string SELECTLOOKUP = "[Config].[usp_LookUpSelect]";
        public const string VEHICLEGROUPLIST = "[Master].[usp_VehicleGroupList]";
        public const string CARGOTYPELIST = "[Master].[usp_CargoTypeList]";
        public const string LISTLOOKUP = "[Config].[usp_LookUpList]";
        public const string SAVELOOKUP = "[Config].[usp_LookUpSave]";
        public const string DELETELOOKUP = "[Config].[usp_LookUpDelete]";
        public const string VEHICLETYPELIST = "[Master].[usp_VehicleTypeList]";
        public const string LOADINGUNLOADINGLIST = "[Master].[usp_LoadingUnLoadingList]";
        public const string CUSTOMERSELECTLIST= "[Master].[usp_CustomerInquiryList]";

        /// <summary>
        /// [Operation].[CustomerInquiry]
        /// </summary>
        /// 
      
        public const string SAVECUSTOMERINQUIRY = "[Operation].[usp_CustomerInquirySave]";

        public const string GETAPPROXIMATEFAREWEB = "[Operation].[usp_GetApproximateFareWeb]";
        public const string CUSTOMERSAVE="[Operation].[usp_CustomerInsertExternal]";
        public const string SAVEBOOKING = "[Operation].[usp_BookingSave]";
        public const string SELECTBOOKING = "[Operation].[usp_BookingSelect]";
        public const string NEARTRUCKSDEVICELIST = "[Operation].[usp_NearTrucksDeviceList]";
        public const string DELETEBOOKING = "[Operation].[usp_BookingDelete]";
        public const string LISTRATECARD = "[Master].[usp_RateCardList]";


        public const string BOOKINGBYBOOKINGNO = "[Operation].[usp_BookingListByBookingNo2]";

        public const string SELECTDRIVERMONITORINCUSTOMER = "[Operation].[usp_DriverMonitorInCustomer]";
        public const string DRIVERLATESTFIVELATLONGS = "[Operation].[usp_GetDriverFiveLatLong]";
        public const string SELECTDRIVERACTIVITYBYDRIVERID = "[Operation].[usp_DriverActivitySelectByDriverID]";
    }
}