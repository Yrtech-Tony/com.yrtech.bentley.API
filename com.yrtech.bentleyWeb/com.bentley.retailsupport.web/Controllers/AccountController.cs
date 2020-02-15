﻿using com.bentley.retailsupport.web.Common;
using com.yrtech.InventoryAPI.Common;
using com.yrtech.InventoryAPI.DTO;
using System;
using System.Collections.Generic;
using System.Net.Http;
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

        [HttpPost]
        public ActionResult TokenLogin(string token)
        {
            HttpClient client = new HttpClient();
            Uri uri = new Uri("http://" + WebConfigurationManager.AppSettings["APIHost"]);
            client.BaseAddress = uri;
            //添加请求的头文件
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //发送请求并接受返回的值
            String email = TokenHelper.DecryptDES(token);
            string getUserApi = string.Format("bentley/api/Master/UserInfoSearch?userId=&accountId=&accountName=&shopCode=&shopName&email={0}", email);
            HttpResponseMessage message = client.GetAsync(getUserApi).Result;
            string json = message.Content.ReadAsStringAsync().Result;
            APIResult result = CommonHelper.DecodeString<APIResult>(json);
            if (result != null && result.Status)
            {
                List<UserInfoDto> userList = CommonHelper.DecodeString<List<UserInfoDto>>(result.Body);
                if (userList != null && userList.Count > 0)
                {
                   string AccountId = userList[0].AccountId;
                   string password = userList[0].Password;
                   Dictionary<string, string> keyValues = new Dictionary<string, string>();
                   keyValues.Add("accountId", AccountId);
                   keyValues.Add("password", password);
                   HttpContent content = new FormUrlEncodedContent(keyValues);
                   string loginApi = string.Format("bentley/api/Account/Login", AccountId, password);
                   content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
                   message = client.PostAsync(loginApi, content).Result;//改成自己的
                   json = message.Content.ReadAsStringAsync().Result;
                   result = CommonHelper.DecodeString<APIResult>(json);
                   List<AccountDto> accountList = CommonHelper.DecodeString<List<AccountDto>>(result.Body);
                   if (userList != null && userList.Count > 0)
                   {
                       AccountDto user = accountList[0];
                       Session["LoginUser"] = user;
                       FormsAuthentication.SetAuthCookie(user.AccountId, false);
                   }
                }
                else
                {
                    throw new Exception("没有查询到用户信息！");
                }
            }
            else
            {
                throw new Exception("查询用户信息失败！");
            }

            return this.Redirect("~/");
        }
    }
}