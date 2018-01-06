using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PickC.External.Contracts;
using PickC.External.BusinessFactory;

namespace PickC.External.Controllers
{
    public class CustomerInquiryController : Controller
    {
        [HttpPost]
        public ActionResult Help(string mobileNumber)
        {
            var customerInquiryBO = new CustomerInquiryBO();
            customerInquiryBO.Save(new CustomerInquiry {
                InquiryType = UTILITY.CONFIG_INQ_HLP,
                InquiryDate = DateTime.Now,
                MobileNo = mobileNumber,
                EmailID = null,
                CustomerName = null
            });
            return RedirectToAction("Index", "Dashboard");
        }
    }
}