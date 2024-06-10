using AppNetShop.DTO;
using System.Web.Mvc;

namespace AppNetShop.Controllers
{
    public class BaseSession : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {

            if (User != null)
            {
                var username = User.Identity.Name;

                if (!string.IsNullOrEmpty(username))
                {

                    if (Session["USER_SESSION"] == null)
                    {
                        Session["USER_SESSION"] = SetUserSession.SetUserInFo(username);
                    }
                }
                else
                {
                    Session["USER_SESSION"] = null;
                }

            }
            else
            {
                Session["USER_SESSION"] = null;
            }

            base.OnActionExecuted(filterContext);
        }

    }
}