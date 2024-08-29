using AppNetShop.DomainModal;
using AppNetShop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppNetShop.Controllers
{
    public class DanhMucHeThongController : Controller
    {
        // GET: DanhMucHeThong
        public ActionResult DanhMucHeThong()
        {
            return View();
        }
        XEntities _query = new XEntities();

        [HttpPost]
        public JsonResult SaveData(Int64? Id, Int32 so_thu_tu, string ten_goi, string phan_loai)
        {
            try
            {
                var check_so_thu_tu = _query.AspDanhMucHeThong.Where(c => Id == 0 ? (c.sap_xep == so_thu_tu && c.phan_loai == phan_loai) : (c.Id != Id && c.sap_xep == so_thu_tu && c.phan_loai == phan_loai)).Count();
                if (check_so_thu_tu > 0)
                {
                    return Json(new
                    {
                        code = false,
                        message = "Số thứ tự bạn nhập đã bị trùng với dữ liệu khác.",
                    }, JsonRequestBehavior.AllowGet);
                }
                if (Id == 0)
                {
                    _query.AspDanhMucHeThong.Add(new AspDanhMucHeThong
                    {
                        sap_xep = so_thu_tu,
                        ten_goi = ten_goi,
                        phan_loai = phan_loai,
                        ngay_tao = DateTime.Now,
                    });
                    _query.SaveChanges();
                }
                else
                {
                    var find_data = _query.AspDanhMucHeThong.Find(Id);
                    if (find_data != null)
                    {
                        find_data.sap_xep = so_thu_tu;
                        find_data.ten_goi = ten_goi;
                        find_data.phan_loai = phan_loai;
                        find_data.ngay_tao = DateTime.Now;
                    }
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
        [HttpPost]
        public JsonResult SaveLock(Int64? Id, Int32 check_lock)
        {
            try
            {
                var find_data = _query.AspDanhMucHeThong.Find(Id);
                if (find_data != null)
                {
                    if (check_lock == 1)
                    {
                        find_data.is_lock = false;
                    }
                    else
                    {
                        find_data.is_lock = true;
                    }

                }
                _query.SaveChanges();
            
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
        [HttpGet]
        public JsonResult LoadTable(string phan_loai)
        {
            try
            {
                var find_data = _query.AspDanhMucHeThong.Where(c => c.phan_loai == phan_loai).Select(c => new
                {
                    c.Id,
                    c.ten_goi,
                    c.sap_xep,
                    is_lock = c.is_lock == true ? "<font class='fa fa-check text-success'></font>" : "0",
                    is_check = c.is_lock,
                    c.ghi_chu,
                }).ToList();

                return Json(new
                {
                    find_data,
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