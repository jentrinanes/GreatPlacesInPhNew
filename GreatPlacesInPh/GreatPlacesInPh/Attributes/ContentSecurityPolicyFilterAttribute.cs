using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GreatPlacesInPh.Attributes
{
    public class ContentSecurityPolicyFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var response = filterContext.HttpContext.Response;
            response.AddHeader("Content-Security-Policy",
                "default-src https:;script-src 'self' https://code.jquery.com https://ajax.aspnetcdn.com; style-src 'self' https://ajax.aspnetcdn.com");

            base.OnActionExecuting(filterContext);           
        }
    }
}