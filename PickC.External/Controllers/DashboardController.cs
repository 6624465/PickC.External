using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PickC.External.Contracts;
using PickC.External.BusinessFactory;
using PickC.External.ViewModels;

namespace PickC.External.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index(bool IsTripEstimate = false)
        {
            if(IsTripEstimate)
                ViewData["vdIsTripEstimate"] = true;

            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
      
        public ActionResult CustomerSupport()
        {
            ViewBag.Customer = new CustomerInquiryBO().GetCustomerSelectList().Select(x => new { Value = x.LookupID, Text = x.LookupCode }).ToList();
            return View();
        }


        public ActionResult ContactUs()
        {
            ViewBag.Customer = new CustomerInquiryBO().GetCustomerSelectList().Select(x => new { Value = x.LookupID, Text = x.LookupCode }).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult ContactUsSave(CustomerInquiry customer)
        {
            customer.InquiryDate = DateTime.Now;

            var result = new CustomerInquiryBO().SaveContactUs(customer);
            return View("Index");
        }
        public ActionResult Faqs()
        {
            return View();
        }
        public ActionResult Feedback()
        {
            ViewBag.Customer = new CustomerInquiryBO().GetCustomerSelectList().Select(x => new { Value = x.LookupID, Text = x.LookupCode }).ToList();
            return View();
        }
        public ActionResult TermsAndConditions()
        {
            return View();
        }
        public ActionResult Careers()
        {
            return View();
        }
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}