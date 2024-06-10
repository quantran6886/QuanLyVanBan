using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AppNetShop.Controllers
{
    public class CheckMenuController : Controller
    {
        public ActionResult CheckMenu() { return View(); }

        // GET: CheckMenu
        [HttpGet]
        public ActionResult CheckMenu(string Id)
        {
            bool _check = false;
            string routeUrl = "not-found";
            // Lấy danh sách các route trong ứng dụng
            var routes = RouteTable.Routes;

            // Lặp qua từng route để kiểm tra
            foreach (var route in routes)
            {
                if (route is Route routeItem)
                {
                    if (routeItem.Defaults != null )
                    {
                        var data = routeItem.Defaults.Values.FirstOrDefault();
                        if (data.ToString() == Id)
                        {
                            routeUrl = routeItem.Url;
                            _check = true;
                            break;
                        }else
                        {
                            _check = false;
                        }
                    }
                }
            }

            // Controller không tồn tại trong danh sách các route
            return RedirectToRoutePermanent(routeUrl);

        }
    }
}