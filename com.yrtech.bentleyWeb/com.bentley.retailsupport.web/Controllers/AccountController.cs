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
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            HttpClient client = new HttpClient();

            Uri uri = new Uri("http://" + WebConfigurationManager.AppSettings["APIHost"]);
            client.BaseAddress = uri;
            //添加请求的头文件
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //发送请求并接受返回的值
            string loginApi = string.Format("bentley/api/Account/Login?accountId={0}&password={1}", UserName, Password);
            HttpResponseMessage message = client.GetAsync(loginApi).Result;
            string json = message.Content.ReadAsStringAsync().Result;
            APIResult result = CommonHelper.DecodeString<APIResult>(json);
            if (result.Status)
            {
                AccountDto loginUser = CommonHelper.DecodeString<List<AccountDto>>(result.Body)[0];
                Session["LoginUser"] = loginUser;
                FormsAuthentication.SetAuthCookie(loginUser.AccountId, false);
            }
            else
            {
                throw new Exception("登录失败！" + result.Body);
            }


            return this.Redirect("/Home/Index");
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
        
    }
}