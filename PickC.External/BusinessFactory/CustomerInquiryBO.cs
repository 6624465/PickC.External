﻿using System;
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
    }
}