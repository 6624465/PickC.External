using PickC.External.Contracts;
using PickC.External.DataFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickC.External.BusinessFactory
{
    public class DriverActivityBO
    {
        DriverActivityDAL driverActivityDAL;
        public DriverActivityBO()
        {
            driverActivityDAL = new DriverActivityDAL();
        }

        public DriverMonitorInCustomer GetDriverMonitorInCustomer(DriverMonitorInCustomer item)
        {
            return (DriverMonitorInCustomer)driverActivityDAL.GetDriverMonitorInCustomer<DriverMonitorInCustomer>(item);
        }
    }
}