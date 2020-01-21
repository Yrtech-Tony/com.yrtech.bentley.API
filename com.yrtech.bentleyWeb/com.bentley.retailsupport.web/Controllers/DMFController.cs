using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.bentley.retailsupport.web.Controllers
{
    public class DMFController : BaseController
    {
        //
        // GET: /DMF/
        public ActionResult DMFItem()
        {
            return View();
        }

        public ActionResult ExpenseAccount()
        {
            return View();
        }

        public ActionResult ExpenseAccountFile(string Id, string FileType)
        {
            ViewBag.Id = Id;
            ViewBag.FileType = FileType;
            return View();
        }
        public ActionResult MonthSale()
        {
            return View();
        }
        public ActionResult DMFDetail(string ShopId)
        {
            ViewBag.ShopId = ShopId;
            return View();
        }
        public ActionResult DMF()
        {
            return View();
        }


    }
}