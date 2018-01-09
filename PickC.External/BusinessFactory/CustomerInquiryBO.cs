using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using PickC.External.Contracts;
using PickC.External.DataFactory;

namespace PickC.External.BusinessFactory
{
    public class CustomerInquiryBO
    {
        CustomerInquiryDAL customerInquiryDAL;
        public CustomerInquiryBO()
        {
            customerInquiryDAL = new CustomerInquiryDAL();
        }

        public bool Save(CustomerInquiry customerInquiry)
        {
            return customerInquiryDAL.Save(customerInquiry);
        }

        public List<FareChart> GetApproximateFareWEB(decimal distance, decimal duration)
        {
            return customerInquiryDAL.GetApproximateFareWEB(distance, duration);
        }
        public bool SaveContactUs(CustomerInquiry inquiry)
        {
            return customerInquiryDAL.SaveContactUs(inquiry);
        }
        public List<LookUp> GetCustomerSelectList()
        {
            return customerInquiryDAL.GetCustomerSelectList();
        }
    }
}