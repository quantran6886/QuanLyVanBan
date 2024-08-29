using AppNetShop.DomainModal;
using AppNetShop.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AppNetShop.Controllers
{
    [Authorize]
    public class RoleSessionController : Controller
    {
        // GET: RoleSession
        public ActionResult RoleSession()
        {
            return View();
        }

        //[HttpGet]
        //public JsonResult LoadMenu()
        //{
        //    try
        //    {
        //        using (XEntities _en = new XEntities())
        //        {
        //            var UserID = GetUserSession.getUserLoginSession().UserID;
                  
        //            if (User.IsInRole("Admin"))
        //            {
        //                var lstMenu = _en.MenuNavbars.Select(c => new
        //                {
        //                    c.IdMenu,
        //                    c.NameMenu,
        //                    c.Url,
        //                    c.NameColor,
        //                    c.Controller,
        //                    c.NameIcon

        //                }).ToList();
        //                return Json(new { status = true, lstMenu}, JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                var lstMenu = _en.MenuUsers.Where(c => c.IdUser == UserID).Select(c => new
        //                {
        //                    c.IdUser,
        //                    NameMenu = c.MenuNavbar != null ? c.MenuNavbar.NameMenu : "",
        //                    Url = c.MenuNavbar != null ? c.MenuNavbar.Url : "",
        //                    NameIcon = c.MenuNavbar != null ? c.MenuNavbar.NameIcon : "",
        //                    NameColor = c.MenuNavbar != null ? c.MenuNavbar.NameColor : "",
        //                    Controller = c.MenuNavbar != null ? c.MenuNavbar.Controller : "",
        //                }).ToList();

        //                return Json(new { status = true, lstMenu }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { status = false }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        public JsonResult SaveRoles(string NameRole)
        {
            try
            {
                using (XEntities _en = new XEntities())
                {
                    AspNetRoles aspNetRole = new AspNetRoles();
                    aspNetRole.Id = Encrypt(NameRole);
                    aspNetRole.Name = NameRole;

                    _en.AspNetRoles.Add(aspNetRole);
                    _en.SaveChanges();
                }

                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            }

        }


        static string key { get; set; } = "A!9HHhi%XjjYY4YP2@Nob009X";

        public static string Encrypt(string text)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                using (var tdes = new TripleDESCryptoServiceProvider())
                {
                    tdes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;

                    using (var transform = tdes.CreateEncryptor())
                    {
                        byte[] textBytes = UTF8Encoding.UTF8.GetBytes(text);
                        byte[] bytes = transform.TransformFinalBlock(textBytes, 0, textBytes.Length);
                        return Convert.ToBase64String(bytes, 0, bytes.Length);
                    }
                }
            }
        }


    }
}