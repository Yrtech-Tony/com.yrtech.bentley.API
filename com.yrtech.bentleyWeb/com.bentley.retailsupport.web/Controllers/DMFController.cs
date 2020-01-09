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
	}
}