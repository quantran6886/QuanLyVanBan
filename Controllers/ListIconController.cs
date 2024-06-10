using AppNetShop.DomainModal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppNetShop.Controllers
{
    public class ListIconController : Controller
    {
        // GET: ListIcon
        public ActionResult ListIcon()
        {
            return View();
        }
        XEntities _en = new XEntities();

        [HttpPost]
        public JsonResult SaveData(Int64 Id, string NameIcon, string CodeIcon)
        {
            try
            {

                if (Id == 0)
                {
                    lstIcon lst = new lstIcon();
                    lst.Id = 0;
                    lst.NameIcon = NameIcon;
                    lst.CodeIcon = CodeIcon;

                    _en.lstIcons.Add(lst);
                    _en.SaveChanges();
                }
                else
                {
                    var _edit = _en.lstIcons.Find(Id);
                    if (_edit != null)
                    {
                        _edit.NameIcon = NameIcon;
                        _edit.CodeIcon = CodeIcon;
                        _en.SaveChanges();
                    }
                }
                return Json(new
                {
                    status = true,
                    message = "Cập nhật thành công",
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    message = "Có lỗi " + ex.Message,
                });
            }
        }

        [HttpGet]
        public JsonResult LoadTable()
        {

            try
            {
                var lstTable = _en.lstIcons.Select(c => new
                {
                   c.Id,
                   c.NameIcon,
                   c.CodeIcon
                }).ToList();

                return Json(new
                {
                    status = true,
                    lstTable,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new
                {
                    status = false,
                    message = "Có lỗi",
                });
            }


        }

    }
}