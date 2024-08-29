using AppNetShop.DomainModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppNetShop.Controllers
{
    public class C_KhoiPhucDuLieuController : Controller
    {
        // GET: C_KhoiPhucDuLieu
        public ActionResult C_KhoiPhucDuLieu()
        {
            return View();
        }

        XEntities _query = new XEntities();

        [HttpGet]
        public JsonResult LoadTable()
        {
            try
            {
                var lstDSThu = _query.AspKeHoachThu.Where(c => c.is_chuyen_thung_rac == true).AsEnumerable().Select(c => new
                {
                    c.IdKeHoach,
                    c.don_vi_thu,
                    c.dia_chi_thu,
                    c.noi_dung_thu,
                    c.ly_do_thu,
                    so_tien_thu_du_kien = c.so_tien_thu_du_kien != null ? string.Format("{0:#,##0}", c.so_tien_thu_du_kien) : "",
                    c.url_minh_chung,
                    ngay_tao_phieu = c.ngay_tao_phieu != null ? string.Format("{0:dd-MM-yyyy}", c.ngay_tao_phieu) : "",
                }).ToList();

                return Json(new
                {
                    lstDSThu,
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

        
        [HttpPost]
        public JsonResult KhoiPhucDuLieu(Int64? IdKeHoach)
        {
            try
            {

                var data = _query.AspKeHoachThu.Find(IdKeHoach);
                if (data != null)
                {
                    data.is_chuyen_thung_rac = null;
                    _query.SaveChanges();
                }

                return Json(new
                {
                    code = true,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    code = false,
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}