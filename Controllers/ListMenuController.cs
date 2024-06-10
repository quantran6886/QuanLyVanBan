using AppNetShop.DomainModal;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppNetShop.Controllers
{
    public class ListMenuController : Controller
    {
        // GET: ListMenu
        public ActionResult ListMenu()
        {
            return View();
        }

        XEntities _en = new XEntities();

        [HttpGet]
        public JsonResult LoadData()
        {
            try
            {
                var lstIcon = _en.lstIcons.AsEnumerable().Select(c => new
                {
                    c.Id,
                    c.CodeIcon,
                    c.NameIcon,
                }).ToList();


                return Json(new
                {
                    lstIcon,
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
        public JsonResult SaveData(string strData)
        {
            try
            {
                var ClientData = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize <MenuNavbar> (strData);

                if (ClientData.IdMenu == 0)
                {
                    ClientData.IdMenuCha = null;
                    _en.MenuNavbars.Add(ClientData);
                    _en.SaveChanges();
                }
                else
                {
                    var _edit = _en.MenuNavbars.Find(ClientData.IdMenu);
                    if (_edit != null)
                    {
                        _edit.NameMenu = ClientData.NameMenu;
                        _edit.NameColor = ClientData.NameColor;
                        _edit.NameIcon = ClientData.NameIcon;
                        _edit.Controller = ClientData.Controller;
                        _edit.Action = ClientData.Action;
                        _edit.Url = ClientData.Url;
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
                int stt = 1;
                var lstTable = _en.MenuNavbars.AsEnumerable().Select(c => new
                {
                    stt = stt++,
                    c.IdMenu,
                    c.Url,
                    c.NameIcon,
                    c.NameColor,
                    c.NameMenu
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
        
        [HttpGet]
        public JsonResult LoadDetail(Int64 IdMenu)
        {

            try
            {
               var data = _en.MenuNavbars.Where(c => c.IdMenu == IdMenu).Select(c => new
               {
                   c.IdMenu,
                   c.NameMenu,
                   c.NameColor,
                   c.NameIcon,
                   c.Url,
                   c.Action,
                   c.Controller,
               }).FirstOrDefault();

                return Json(new
                {
                    status = true,
                    data,
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

        [HttpPost]
        public JsonResult DeleteMenu(int IdMenu)
        {
            try
            {
                var _data = _en.MenuNavbars.Find(IdMenu);

                if (_data != null)
                {
                    _en.MenuNavbars.Remove(_data);
                    _en.SaveChanges();
                }
                return Json(new
                {
                    status = true,
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