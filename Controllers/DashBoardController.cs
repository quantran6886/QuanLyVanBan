using AppNetShop.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiamondSubject.Controllers
{
    public class DashBoardController : BaseSession
    {
        [Authorize]
        // GET: DashBoard
        public ActionResult DashBoard()
        {
            return View();
        }
    }
}