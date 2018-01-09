using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PickC.External.Contracts
{
    public class CustomerInquiry
    {
        public Int64 InquiryID { get; set; }
        public Int16 InquiryType { get; set; } 
        public DateTime InquiryDate { get; set; }
        public string MobileNo { get; set; }
        public string EmailID { get; set; }
        public string CustomerName { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }
}