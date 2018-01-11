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
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace PickC.External.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index(bool IsTripEstimate = false, CustomerInquiryVm customerInquiryVm = null)
        {
            if(IsTripEstimate)
                ViewData["vdIsTripEstimate"] = true;

            if (customerInquiryVm != null)
                ViewData["vdCustomerInquiryData"] = customerInquiryVm;

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
            ViewBag.Customer = new CustomerInquiryBO().GetCustomerSelectList()
                .Select(x => new { Value = x.LookupID, Text = x.LookupCode}).ToList();
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
        [HttpPost]
        public ActionResult SaveBooking(Booking booking)
        {
            JsonSerializerSettings serializerSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
            try
            {
                booking.BookingDate = DateTime.Now;
                booking.RequiredDate = DateTime.Now;
                booking.LoadingUnLoading = 1373;
                var result = new CustomerInquiryBO().SaveBooking(booking);
                if (result)
                {
                    Booking bookings = new CustomerInquiryBO().GetBooking(new Booking
                    {
                        BookingNo = booking.BookingNo
                    });
                    var driverList = new CustomerInquiryBO().GetNearTrucksDeviceID(bookings.BookingNo,
                        UTILITY.radius,
                        bookings.VehicleType,
                        bookings.VehicleGroup,
                        bookings.Latitude,
                        bookings.Longitude);//UTILITY.radius
                    if (driverList.Count > 0)
                    {
                        PushNotification(driverList.Select(x => x.DeviceId).ToList<string>(),
                            booking.BookingNo, UTILITY.NotifyNewBooking);
                        
                        
                        return Json(new { BookingNo = booking.BookingNo, Status = UTILITY.BOOKINGSUCCESS }, JsonRequestBehavior.AllowGet);
                        //return View(new
                        //{
                        //    BookingNo = booking.BookingNo,
                        //    Status = UTILITY.BOOKINGSUCCESS
                        //});
                    }
                    else
                    {
                        var CancelBooking = new CustomerInquiryBO().DeleteBooking(new Booking { BookingNo = booking.BookingNo });
                        if (CancelBooking)
                            return Json(new { BookingNo = "", Status = UTILITY.NotifyCustomer }, JsonRequestBehavior.AllowGet);
                        //return View(new { BookingNo = "", Status = UTILITY.NotifyCustomer });
                        else
                            return Json(new { BookingNo = "", Status = UTILITY.FAILURESTATUS }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                    return View(result);
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
    }
}