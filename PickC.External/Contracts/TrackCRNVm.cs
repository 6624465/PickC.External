using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickC.External.Contracts
{
    public class TrackCRNVm
    {
        public Booking booking { get; set; }
        public DriverMonitorInCustomer driverMonitorInCustomer { get; set; }
    }
}