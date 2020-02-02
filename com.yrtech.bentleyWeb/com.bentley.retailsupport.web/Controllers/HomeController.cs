using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.bentley.retailsupport.web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ManageUsers()
        {
            return View();
        }


        public ActionResult ManageAgent()
        {
            return View();
        }

        public ActionResult AgentCreate()
        {
            return View();
        }

        public ActionResult AgentEdit(string Id)
        {
            ViewBag.ShopId = Id;
            return View();
        }

        public ActionResult ManageArea()
        {
            return View();
        }

        public ActionResult AreaCreate()
        {
            return View();
        }

        public ActionResult AreaEdit(string Id)
        {
            ViewBag.AreaId = Id;
            return View();
        }

        public ActionResult ManageActionType()
        {
            return View();
        }

        public ActionResult ActionTypeCreate()
        {
            return View();
        }

        public ActionResult ActionTypeEdit(string Id)
        {
            ViewBag.ActionTypeId = Id;
            return View();
        }
        
    }
}