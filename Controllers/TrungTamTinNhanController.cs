using AppNetShop.DomainModal;
using AppNetShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppNetShop.Controllers
{
    public class TrungTamTinNhanController : Controller
    {
        private readonly XEntities _en;

        // GET: TrungTamTinNhan
        public ActionResult TrungTamTinNhan()
        {
            return View();
        }

        public TrungTamTinNhanController()
        {
            _en = new XEntities();
        }

        [HttpGet]
        public JsonResult LoadData()
        {
            try
            {
                var GetIdUser = GetUserSession.getUserLoginSession().UserID;

                var data = _en.AspDetailUsers.Where(c => c.UsersId != GetIdUser).
                    Select(c => new
                    {
                        c.IdDetailUser,
                        c.UsersId,
                        c.UrlImage,
                        c.DateUser,
                        c.SexUser,
                        c.NameImage,
                        c.NameUser,
                    }).ToList();

                return Json(new
                {
                    data,
                    IdSend = GetIdUser,
                    success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}