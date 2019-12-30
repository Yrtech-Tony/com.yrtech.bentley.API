using com.bentley.retailsupport.web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace com.bentley.retailsupport.web.Controllers
{
    [AuthenAdminAttribute]
    public class BaseController : Controller
    {
	}
}