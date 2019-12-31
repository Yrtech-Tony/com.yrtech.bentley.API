using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.bentley.retailsupport.web.Controllers
{
    public class MarketingController : BaseController
    {
        //
        // GET: /Marketing/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create(string Id)
        {
            ViewBag.Id = Id;
            return View();
        } 
       
        public ActionResult Before3Weeks(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        public ActionResult Before3Days(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        public ActionResult TheDays(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        public ActionResult After2Days(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        public ActionResult After7Days(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        public ActionResult After1Months(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        public ActionResult After3Months(string Id)
        {
            ViewBag.Id = Id;
            return View();
        }
	}
}