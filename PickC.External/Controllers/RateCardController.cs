using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PickC.External.BusinessFactory;
using PickC.External.Contracts;
using PickC.External.DataFactory;


namespace PickC.External.Controllers
{
    public class RateCardController : Controller
    {
        // GET: RateCard
        public ActionResult Index()
        {
            var result = new RateCardBO().getRateCardList();
            return View(result);
        }
        public ActionResult Mobile()
        {
            var result = new RateCardBO().getRateCardList();
            return View(result);
        }
        public ActionResult pickCMobile()
        {
            var result = new RateCardBO().getRateCardList();
            return View(result);
        }
        public ActionResult Ratecard()
        {
            var result = new RateCardBO().getRateCardList();
            return View(result);
        }
    
    
    
    }
}