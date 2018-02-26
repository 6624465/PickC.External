using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using PickC.External.Contracts;
using PickC.External.BusinessFactory;
using PickC.External.ViewModels;
using System.Configuration;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using PickC.External.EmailSMSGenerators;
namespace PickC.External.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index(bool IsTripEstimate = false)
        {
            if(IsTripEstimate)
                ViewData["vdIsTripEstimate"] = true;

            //CustomerInquiry data = TempData["DisplayMessage"] as CustomerInquiry;
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
      
        public ActionResult HelpDesk()
        {
            ViewBag.Customer = new CustomerInquiryBO().GetCustomerSelectList().Select(x => new { Value = x.LookupID, Text = x.LookupCode }).ToList();
            return View();
        }

        public ActionResult HelpMobile()
        {
            return View();
        }
        //public ActionResult ContactUs()
        //{
        //    ViewBag.Customer = new CustomerInquiryBO().GetCustomerSelectList()
        //        .Select(x => new { Value = x.LookupID, Text = x.LookupCode}).ToList();
        //    return View();
        //}
        [HttpPost]
        public ActionResult HelpDeskSave(CustomerInquiry customer)
        {
            customer.InquiryDate = DateTime.Now;

            var result = new CustomerInquiryBO().SaveContactUs(customer);
            if (result) {
                if(customer.InquiryType== 1503)
                TempData["DisplayMessage"] = "Your request is submitted successfully";
                else if (customer.InquiryType == 1504)
                    TempData["DisplayMessage"] = "Thanks for your feedback";
                else
                    TempData["DisplayMessage"] = "Our customer support team will revert  shortly";
            }
            //customersupport:our customer support team will revert  shortly;
            //contactus:your request is submitted succesfully;
            //feedback:Thanks for your feedback;
            var emailResult = SendMessageToPickC(customer);
            return RedirectToAction("HelpDesk", "Dashboard");
        }
        public ActionResult Faqs()
        {
            return View();
        }
        //public ActionResult Feedback()
        //{
        //    ViewBag.Customer = new CustomerInquiryBO().GetCustomerSelectList().Select(x => new { Value = x.LookupID, Text = x.LookupCode }).ToList();
        //    return View();
        //}
        public ActionResult TermsAndConditions()
        {
            return View();
        }
        public ActionResult MobileTermsAndConditions()
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
        public ActionResult GetHelp()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveBooking(Booking booking)
        {
            //JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            try
            {
                booking.BookingDate = DateTime.Now;
                booking.RequiredDate = DateTime.Now;
                booking.LoadingUnLoading = 1373;
                var result = new CustomerInquiryBO().SaveBooking(booking);
                if (result)
                {
                    Booking bookings = new CustomerInquiryBO().GetBooking(new Booking{BookingNo = booking.BookingNo});
                    //var driverList = new CustomerInquiryBO().GetNearTrucksDeviceID(bookings.BookingNo,
                    //    UTILITY.radius,
                    //    bookings.VehicleType,
                    //    bookings.VehicleGroup,
                    //    bookings.Latitude,
                    //    bookings.Longitude);//UTILITY.radius
                    //if (driverList.Count > 0)
                    //{
                    //    PushNotification(driverList.Select(x => x.DeviceId).ToList<string>(),
                    //        booking.BookingNo, UTILITY.NotifyNewBooking);


                    //return Json(new { BookingNo = booking.BookingNo, Status = UTILITY.BOOKINGSUCCESS }, JsonRequestBehavior.AllowGet);
                    //return View(new
                    //{
                    //    BookingNo = booking.BookingNo,
                    //    Status = UTILITY.BOOKINGSUCCESS
                    //});

                    TempData["TD:BookingInfo"] = new BookingStatusVm
                    {
                        BookingNo = booking.BookingNo,
                        Status = UTILITY.BOOKINGSUCCESS
                    };
                    return RedirectToAction("Index");
                    }
                    else
                    {
                        var CancelBooking = new CustomerInquiryBO().DeleteBooking(new Booking { BookingNo = booking.BookingNo });

                    TempData["TD:BookingInfo"] = new BookingStatusVm
                    {
                        BookingNo = "",
                        Status = UTILITY.FAILURESTATUS
                    };
                    return RedirectToAction("Index");
                        //if (CancelBooking)
                        //    //TempData["TD:BookingInfo"] = new BookingStatusVm
                        //    //{
                        //    //    BookingNo = booking.BookingNo,
                        //    //    Status = UTILITY.BOOKINGSUCCESS
                        //    //};
                        //return Json(new { BookingNo = "", Status = UTILITY.NotifyCustomer }, JsonRequestBehavior.AllowGet);
                        ////return View(new { BookingNo = "", Status = UTILITY.NotifyCustomer });
                        //else
                        //    return Json(new { BookingNo = "", Status = UTILITY.FAILURESTATUS }, JsonRequestBehavior.AllowGet);
                    }
                
                //else
                //    return View(result);
            }
            catch (Exception ex)
            {
                return Json(new { BookingNo = "", Status = UTILITY.FAILURESTATUS }, JsonRequestBehavior.AllowGet);
            }
        }
        public void PushNotification(List<string> receipents, string bookingNo, string message)
        {
            try
            {
                string applicationID = ConfigurationManager.AppSettings["appApplicationKey"];
                string senderId = ConfigurationManager.AppSettings["appSenderId"];

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                var data = new
                {
                    registration_ids = receipents,
                    //notification = new
                    //{
                    //    body = message,
                    //    title = "Alert",
                    //    sound = "Enabled"
                    //},
                    data = new
                    {
                        bookingNo = bookingNo,
                        body = message
                    }
                };

                var json = JsonConvert.SerializeObject(data);
                Byte[] byteArray = Encoding.UTF8.GetBytes(json);
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                string str = sResponseFromServer;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
        }
        public bool SendMessageToPickC(CustomerInquiry contactUs)
        {
            bool result = true;
            try
            {
                //contactUs.CreatedBy = UTILITY.DEFAULTUSER;
               // result = new CustomerBO().SaveCustomer(contactUs);
                string fromMail = string.Empty;
                var strBody = string.Empty;
                if (result == true)
                {
                    if (contactUs.InquiryType == 1505)
                    {
                        fromMail = "support@pickcargo.in";
                        strBody = "Namaskar       " + contactUs.CustomerName + ",<BR>Your request has been submitted succesfully, our support team shall contact you at the earliest.<BR><BR>" +
                            "Regards,<BR>" +
                            "Pick - C Support Team.";
                    }
                    else if (contactUs.InquiryType == 1503)
                    {
                        fromMail = "contact@pickcargo.in";
                        strBody = "Namaskar     " + contactUs.CustomerName + ",<BR>Your request has been submitted succesfully, our support team shall contact you at the earliest.<BR><BR>" +
                            "Regards,<BR>" +
                            "Pick - C Support Team.";
                    }
                    else if (contactUs.InquiryType == 1504)
                    {

                        fromMail = "feedback@pickcargo.in";
                        strBody = "Namaskar     " + contactUs.CustomerName + ",<BR>We appreciate your valuable  feedback.<BR><BR>" +
                                "Regards,<BR>" +
                                "Pick - C Support Team.";
                    }
                    //public bool ConfigMail(string to, bool isHtml, string cc, string subject, string body, string[] attachments)
                    //public bool ConfigMail(string to, bool isHtml, string subject, string body)
                    //public bool ConfigMail(string to, bool isHtml, string cc, string subject, string body, string[] attachments)

                    bool sendMailPickC =new  EmailGenerator().ConfigMail(fromMail,true,fromMail, contactUs.Subject, contactUs.Description);

                    if (contactUs.EmailID.Length>0)
                    {
                       

                        //strBody = "Namaskar  " + contactUs.CustomerName + ", <BR>" +
                        //          "Thank you for your valuable request. Our Customer support team will revert soon. <BR> <BR>" +
                        //          "Regards, <BR>" +
                        //          "Thank You,"+
                        //          "Pick-C, Support team.<BR><BR>" +
                        //          "<BR>This is a system generated e - mail please do not reply to this e - mail," +
                        //          "replies to this e - mail are routed to an unmonitored mailbox."+
                        //          "If any queries or clarifications about this invoice, write to us at support@pickcargo.in";



                        bool sendMailCustomer = new EmailGenerator().ConfigMail(contactUs.EmailID, true, contactUs.Subject, strBody);
                    }



                    if (sendMailPickC)
                        return true;
                    else
                        return false;
                }
                else
                    return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
      
    }
}