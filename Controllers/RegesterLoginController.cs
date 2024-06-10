using AppNetShop.DomainModal;
using AppNetShop.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AppNetShop.Controllers
{
    [RequireHttps]
    public class RegesterLoginController : AuthorizeSerizal
    {
        XEntities _en = new XEntities();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public RegesterLoginController()
        {
        }

        public RegesterLoginController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: RegesterLogin
        public ActionResult RegesterLogin()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CreateUserRole(string UserName, string Password, string PasswordConfirm, string Email, bool IsEmail)
        {
            try
            {
                using (XEntities _en = new XEntities())
                {
                    var _data = _en.AspNetUsers.Where(c => c.UserName.ToLower() == UserName.ToLower() || c.Email == Email).Count();

                    if (_data > 0)
                    {
                        return Json(new
                        {
                            message = "Tài khoản hoặc Email đã tồn tại",
                            status = false,
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (Password.Trim() != PasswordConfirm.Trim())
                    {
                        return Json(new
                        {
                            message = "Mật khẩu xác nhận không đúng",
                            status = false,
                        }, JsonRequestBehavior.AllowGet);
                    }

                    var user = new ApplicationUser { UserName = UserName, Email = Email, EmailConfirmed = IsEmail, LockoutEnabled = false };
                    var result = await UserManager.CreateAsync(user, Password);

                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    }

                }

                return Json(new
                {
                    message = "Cập nhập thành công",
                    status = true,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    message = ex.Message,
                    status = false,
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult LoadTable(string txtName)
        {
            try
            {
                var lstAccount = _en.AspNetUsers.AsEnumerable().Where(C => txtName != "" ? C.UserName.ToLower().Contains(txtName.ToLower()) : true).Select(c => new
                {
                    c.UserName,
                    c.Email,
                    Roles = c.AspNetRoles.FirstOrDefault() != null ? c.AspNetRoles.FirstOrDefault().Name : "",
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

        [HttpGet]
        public JsonResult checkHasRoles(string UserName, string Password, string PasswordConfirm, string Email)
        {
            try
            {
                var _data = _en.AspNetUsers.Where(c => c.UserName.ToLower() == UserName.ToLower()).Count();
                var _dataEmail = _en.AspNetUsers.Where(c => c.Email.ToLower() == Email.ToLower()).Count();

                if (_data > 0)
                {
                    return Json(new
                    {
                        message = "Tài khoản đã được đăng ký",
                        status = false,
                    }, JsonRequestBehavior.AllowGet);
                }
                else if (Password.Trim() != PasswordConfirm.Trim())
                {
                    return Json(new
                    {
                        message = "Mật khẩu xác nhận không đúng",
                        status = false,
                    }, JsonRequestBehavior.AllowGet);
                }
                else if (_dataEmail > 0)
                {
                    return Json(new
                    {
                        message = "Email đã được đăng ký",
                        status = false,
                    }, JsonRequestBehavior.AllowGet);
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
                }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}