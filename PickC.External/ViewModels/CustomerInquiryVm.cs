﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickC.External.ViewModels
{
    public class CustomerInquiryVm
    {
        public Int64 InquiryID { get; set; }
        public Int16 InquiryType { get; set; }
        public DateTime InquiryDate { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string CustomerName { get; set; }
        public decimal? Distance { get; set; }
        public decimal? Duration { get; set; }

        public string frmLatLog { get; set; }
        public string toLatLog { get; set; }

        public string fLoc { get; set; }

        public string tLoc { get; set; }
    }
}