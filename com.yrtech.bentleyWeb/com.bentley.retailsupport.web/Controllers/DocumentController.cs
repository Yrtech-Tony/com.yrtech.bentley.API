using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.bentley.retailsupport.web.Controllers
{
    /// <summary>
    /// 文件提交
    /// </summary>
    public class DocumentController : Controller
    {
        //
        // GET: /Document/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DocList(string ShopId, string FileId)
        {
            ViewBag.ShopId = ShopId;
            ViewBag.FileId = FileId;
            return View();
        }
	}
}