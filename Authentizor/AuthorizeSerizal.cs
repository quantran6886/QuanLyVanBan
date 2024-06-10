using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AppNetShop.Controllers
{
    public class AuthorizeSerizal : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext context)
        {
            if (Session["USER_SESSION"] == null)
            {
                context.Result = new RedirectToRouteResult(
                   new RouteValueDictionary
                   {
                        {"Controller" , "Error" },
                        {"Action" , "E404" }
                   });

            }
            base.OnActionExecuted(context);
        }

    }
}