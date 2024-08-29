using AppNetShop.DomainModal;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppNetShop.Controllers
{
    public class C_GetDanhMucController : Controller
    {
        // GET: C_GetDanhMuc
        public ActionResult C_GetDanhMuc()
        {
            return View();
        }
        XEntities _query = new XEntities();

        [HttpGet]
        public JsonResult LoadDanhMuc()
        {
            try
            {
                var data = _query.sp_GetDataDM("").ToList();
                var ngay_gio_hien_tai = String.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Now);

                string htmlThoiKy = "<option value=''>Chọn thời kỳ</option>";
                string htmlBoSuuTap = "<option value=''>Chọn bộ sưu tập</option>";
                string htmlChatLieu = "<option value=''>Chọn chất liệu</option>";
                string htmlMauSac = "<option value=''>Chọn màu sắc</option>";

                var lstThoiky = data.Where(c => c.phan_loai == "Danh mục thời kỳ").ToList();
                var lstBoSuuTap = data.Where(c => c.phan_loai == "Danh mục bộ sưu tập").ToList();
                var lstChatLieu = data.Where(c => c.phan_loai == "Danh mục chất liệu").ToList();
                var lstMauSac = data.Where(c => c.phan_loai == "Danh mục màu sắc").ToList();

                if (lstThoiky != null)
                {
                    foreach (var item in lstThoiky)
                    {
                        htmlThoiKy += string.Format("<option value='{0}'>{0}</option>",item.ten_goi);
                    }
                }
                if (lstBoSuuTap != null)
                {
                    foreach (var item in lstBoSuuTap)
                    {
                        htmlBoSuuTap += string.Format("<option value='{0}'>{0}</option>", item.ten_goi);
                    }
                }
                if (lstChatLieu != null)
                {
                    foreach (var item in lstChatLieu)
                    {
                        htmlChatLieu += string.Format("<option value='{0}'>{0}</option>", item.ten_goi);
                    }
                }
                if (lstMauSac != null)
                {
                    foreach (var item in lstMauSac)
                    {
                        htmlMauSac += string.Format("<option value='{0}'>{0}</option>", item.ten_goi);
                    }
                }

                return Json(new
                {
                    ngay_gio_hien_tai,
                    htmlThoiKy, 
                    htmlBoSuuTap,   
                    htmlChatLieu,
                    htmlMauSac,
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