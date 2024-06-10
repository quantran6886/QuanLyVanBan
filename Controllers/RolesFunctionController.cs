using AppNetShop.DomainModal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppNetShop.Controllers
{
    [Authorize]
    public class RolesFunctionController : Controller
    {
        // GET: RolesFunction
        public ActionResult RolesFunction()
        {
            return View();
        }
        XEntities _en = new XEntities();

        public JsonResult LoadTable()
        {
            try
            {
                var lstAccount = _en.AspNetUsers.AsEnumerable().Select(c => new
                {
                    c.UserName,
                    c.Email,
                    Lockout = c.LockoutEnabled == true ?  "<span class=\"badge badge-danger\">Lock</span>" : "<span class=\"badge badge-success\">Active</span>",
                    Roles = c.AspNetRoles.FirstOrDefault() != null ? c.AspNetRoles.FirstOrDefault().Name : "Chưa phân quyền",
                }).ToList();

                //var lstMenu = _en.MenuNavbars.AsEnumerable().Select(c => new
                //{
                //    c.IdMenu,
                //    c.Url,
                //    c.NameIcon,
                //    c.NameColor,
                //    c.NameMenu
                //}).ToList();


                return Json(new
                {
                    //lstMenu,
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

        [HttpGet]        
        public JsonResult LoadMenu(string UserName)
        {
            try
            {
                String IdUser = null;
                var data = _en.AspNetUsers.Where(c => c.UserName == UserName).FirstOrDefault();
                if (data != null)
                {
                    IdUser = data.Id;
                }

                //var lstMenu = _en.MenuUsers.AsEnumerable().Where(c => c.IdUser == IdUser).Select(c => new
                //{
                //    c.IdMenu,
                //}).ToList();

                return Json(new
                {
                    //lstMenu,
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
        public JsonResult SaveMenuUser(Int64 IdMenu , string NameUser)
        {
            try
            {
                String IdUser = null;
                var data = _en.AspNetUsers.Where(c => c.UserName == NameUser).FirstOrDefault();
                if (data != null)
                {
                    IdUser = data.Id;
                }
                //var CheckMenuRoles = _en.MenuUsers.Where(c => c.IdUser == IdUser && c.IdMenu == IdMenu).FirstOrDefault();

                //if (CheckMenuRoles != null)
                //{
                //    _en.MenuUsers.Remove(CheckMenuRoles);
                //    _en.SaveChanges();
                //}
                //else
                //{
                //    MenuUser menuUser = new MenuUser();
                //    menuUser.IdRolesMenu = 0;
                //    menuUser.IdMenu = IdMenu;
                //    menuUser.IdUser = IdUser;

                //    _en.MenuUsers.Add(menuUser);
                //    _en.SaveChanges();
                //}
                
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
    }
}