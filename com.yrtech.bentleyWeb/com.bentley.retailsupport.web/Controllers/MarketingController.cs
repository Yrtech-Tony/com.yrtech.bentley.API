﻿using System;
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

        public ActionResult Create()
        {
            return View();
        }
	}
}