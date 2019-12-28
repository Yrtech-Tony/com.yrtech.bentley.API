using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace com.bentley.retailsupport.web.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string UserName,string Password)
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
            Session["LoginUser"] = json;

            return this.Redirect("/Home/Index");
        }
	}
}