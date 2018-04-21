using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickC.External.Contracts
{
    public class DriverMonitorInCustomer : IContract
    {
        public DriverMonitorInCustomer() { }

        public string DriverId { get; set; }
        public decimal CurrentLat { get; set; }
        public decimal CurrentLong { get; set; }
    }
}