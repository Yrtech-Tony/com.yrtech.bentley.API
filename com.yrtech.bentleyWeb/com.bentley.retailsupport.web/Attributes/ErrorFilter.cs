using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.bentley.retailsupport.web.Attributes
{
    public class ErrorFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            string errorMsg;
            var exception = filterContext.Exception;
            errorMsg = exception.Message;


            if (filterContext.RequestContext.HttpContext.Request.IsAjaxRequest())
            {
                var accept = filterContext.RequestContext.HttpContext.Request.AcceptTypes;
                if (accept.Contains("application/json"))
                {
                    filterContext.Result = new JsonResult() { Data = new { Success = false, Msg = errorMsg } };
                }
                else
                {
                    filterContext.Result = new JavaScriptResult() { Script = "alert( '" + errorMsg + "');" };
                }
            }
            else
            {
                //普通异常
                filterContext.Result = new ViewResult() { ViewName = "/Views/Shared/Error.cshtml", ViewData = new ViewDataDictionary() { { "errorMsg", errorMsg } } };
            }
            filterContext.ExceptionHandled = true;

        }
    }
}