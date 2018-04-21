using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;

using PickC.External.Contracts;
namespace PickC.External.DataFactory
{
    public class DriverActivityDAL
    {
        private Database db;
        private DbTransaction currentTransaction = null;
        private DbConnection connection = null;
        public DriverActivityDAL()
        {
            db = DatabaseFactory.CreateDatabase("PickC");
        }
        public IContract GetDriverMonitorInCustomer<T>(IContract lookupItem) where T : IContract
        {
            var item = ((DriverMonitorInCustomer)lookupItem);
            var driverMonitorInCustomer = db.ExecuteSprocAccessor(DBRoutine.SELECTDRIVERMONITORINCUSTOMER,
                                MapBuilder<DriverMonitorInCustomer>.BuildAllProperties(),
                                item.DriverId).FirstOrDefault();

            if (driverMonitorInCustomer == null) return null;

            return driverMonitorInCustomer;
        }
        public string GetFiveLatLongsforDriver(string DriverID, string Token)
        {
            var recordcommand = db.GetStoredProcCommand(DBRoutine.DRIVERLATESTFIVELATLONGS, DriverID, Token);
            var result = db.ExecuteScalar(recordcommand).ToString();
            return result;
        }
        public IContract GetDriverActivityByDriverID<T>(IContract lookupItem) where T : IContract
        {
            var item = ((DriverActivity)lookupItem);

            var driveractivityItem = db.ExecuteSprocAccessor(DBRoutine.SELECTDRIVERACTIVITYBYDRIVERID,
                                MapBuilder<DriverActivity>.BuildAllProperties(),
                                item.DriverId).FirstOrDefault();

            if (driveractivityItem == null) return null;

            return driveractivityItem;
        }
    }
}