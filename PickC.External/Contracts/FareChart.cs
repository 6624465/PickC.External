using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickC.External.Contracts
{
    public class FareChart : IContract
    {
        public Int16 vehicleGroup { get; set; }

        public string vehicleGroupDesc { get; set; }

        public decimal OpenCharge { get; set; }

        public decimal CloseCharge { get; set; }
    }
}