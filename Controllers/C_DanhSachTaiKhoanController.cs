using AppNetShop.DomainModal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppNetShop.Controllers
{
    public class C_DanhSachTaiKhoanController : Controller
    {
        // GET: C_DanhSachTaiKhoan
        public ActionResult C_DanhSachTaiKhoan()
        {
            return View();
        }
        XEntities _query = new XEntities();

        public JsonResult LoadTable()
        {
            try
            {
                int stt = 1;
                var lstAccount = _query.View_Online.AsEnumerable().Select(c => new
                {
                    stt= stt++,
                    c.IdUser,
                    c.ho_ten,
                    c.gioi_Tinh,
                    c.UserName,
                    ngay_sinh = c.ngay_sinh != null ? string.Format("{0:dd-MM-yyyy}",c.ngay_sinh) : string.Empty,
                    c.so_dien_thoai,
                    c.is_delete,
                    c.url_avatar,
                    c.name_avatar
                }).ToList();

                return Json(new
                {
                    lstAccount,
                    status = true,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new
                {
                    status = false,
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}