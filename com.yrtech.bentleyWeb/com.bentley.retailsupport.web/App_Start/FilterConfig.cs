using System.Web;
using System.Web.Mvc;

namespace com.bentley.retailsupport.web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
