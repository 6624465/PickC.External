using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickC.External.Contracts
{
    public class RateCard:IContract
    {
        public RateCard() { }
        public Int16 Category { get; set; }
        public string VehicleCategoryDescription { get; set; }
        public Int16 VehicleType { get; set; }
        public Int16 RateType { get; set; }
        public string VehicleTypeDescription { get; set; }
        public string RateTypeDescription { get; set; }
        public decimal BaseFare { get; set; }
        public decimal BaseKM { get; set; }
        public decimal DistanceFare { get; set; }
        public decimal WaitingFare { get; set; }
        public decimal RideTimeFare { get; set; }
        public decimal CancellationFee { get; set; }
        public decimal OverNightCharges { get; set; }
    }
}