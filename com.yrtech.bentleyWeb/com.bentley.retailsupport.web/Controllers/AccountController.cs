using com.bentley.retailsupport.web.Common;
using com.yrtech.InventoryAPI.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace com.bentley.retailsupport.web.Controllers
{
    public class AccountController : BaseController
    {
        //
        // GET: /Account/
        public ActionResult Login(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        public ActionResult AfterLogin(string loginUser)
        {
            List<AccountDto> userList = CommonHelper.DecodeString<List<AccountDto>>(loginUser);
            if (userList != null && userList.Count > 0)
            {
                AccountDto user = userList[0];
                Session["LoginUser"] = user;
                FormsAuthentication.SetAuthCookie(user.AccountId, false);
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["LoginUser"] = null;

            return this.Redirect("~/");
        }

        public ActionResult ResetPassword()
        {
            return View();
        }
        
        public ActionResult UserCreate()
        {
            return View();
        }

        public ActionResult UserEdit(string Id)
        {
            ViewBag.UserId = Id;
            return View();
        }
        
    }
}