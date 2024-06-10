using AppNetShop.Models;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Security.Claims;
using AppNetShop.DomainModal;
using AppNetShop;
using AppNetShop.DTO;
using AppNetShop.Controllers;

namespace AppNetShop.Controllers
{
    public class LoginController : BaseSession
    {
        //Accounts

        XEntities _sv = new XEntities();
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public LoginController()
        {

        }
        public ActionResult ShowError()
        {
            return View();
        }

        public JsonResult CheckAccount(string username, string emal)
        {
            var hasrow = false;
            var hasmail = false;
            try
            {
                if (username == null)
                {
                    return null;
                }
                XEntities _sv = new XEntities();
                hasrow = _sv.AspNetUsers.Where(c => c.UserName != null && c.UserName.ToLower() == username.ToLower()).Count() == 0 ? false : true;
                if (!string.IsNullOrEmpty(emal))
                {
                    hasmail = _sv.AspNetUsers.Where(c => c.Email != null && c.Email.ToLower() == emal).Count() == 0 ? false : true;
                }
            }
            catch
            {
                hasrow = false;
            }
            return Json(new
            {
                hasrow,
                hasmail
            }, JsonRequestBehavior.AllowGet);

        }

        public LoginController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {

            if (User.Identity.IsAuthenticated)
            {
                var userSesion = GetUserSession.getInfo();


                returnUrl = !String.IsNullOrEmpty(returnUrl) ? returnUrl : "/dang-nhap-he-thong";
                return RedirectToLocal(returnUrl);

            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            // Đăng nhập người dùng
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            try
            {
                var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, /*model.RememberMe*/ true, false);

                using (XEntities _en = new XEntities())
                {
                    var _CheckLogin = _en.AspNetUsers.Where(c => c.UserName == model.UserName).FirstOrDefault();

                    if (_CheckLogin == null)
                    {
                        ModelState.AddModelError("", "Tài khoản không tồn tại.");
                        return View(model);
                    }
                    switch (result)
                    {
                        case SignInStatus.Success:

                            if (_CheckLogin.LockoutEnabled == true)
                            {
                                ModelState.AddModelError("", "Tài khoản của bạn đã bị khóa.");
                                return View(model);
                            }
                            if (_CheckLogin.EmailConfirmed == false)
                            {
                                ModelState.AddModelError("", "Tài khoản của bạn chưa được xác thực.");
                                return View(model);
                            }
                            if (model.Password == "Dotnet@123")
                            {
                                returnUrl = "/t-r-a-n-g-c-h-u";
                            }
                            else if (string.IsNullOrEmpty(returnUrl))
                            {
                                returnUrl = "/t-r-a-n-g-c-h-u";
                            }

                            var identity = new ClaimsIdentity(new[] {
                                new Claim(ClaimTypes.Name, _CheckLogin.UserName),
                                new Claim(ClaimTypes.NameIdentifier,  _CheckLogin.Id),
                            }, DefaultAuthenticationTypes.ApplicationCookie);

                            var context = Request.GetOwinContext();
                            var authenticationManager = context.Authentication;
                            authenticationManager.SignIn(identity);

                            return RedirectToLocal(returnUrl);
                        case SignInStatus.LockedOut:
                            ModelState.AddModelError("", "Tài khoản của bạn đã bị khóa.");
                            return View(model);
                        case SignInStatus.RequiresVerification:
                            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, model.RememberMe });
                        case SignInStatus.Failure:
                        default:
                            ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng.");
                            return View(model);
                    }
                }
            }
            catch (Exception ex)
            {
                var x = ex.Message;
                return View(model);
            }

        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Login", "Login");

        }

        private void AddErrors(Microsoft.AspNet.Identity.IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            Session.Remove("USER_SESSION");
            //return Json(data: 1);
            return RedirectToAction("Login", "Login");
        }
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }



        // GET: Accounts
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }


        //[Authorize]
        //public ActionResult LoadRoles()
        //{
        //    try
        //    {
        //        var data = _sv.AspNetRoles.Select(p => new
        //        {
        //            p.Id,
        //            p.Name,

        //        }).ToList();
        //        var json = JsonConvert.SerializeObject(data);
        //        return Content(json, "application/json");
        //    }
        //    catch (Exception ex)
        //    {

        //        return Json(ex.Message, JsonRequestBehavior.AllowGet);
        //    }
        //}


    }
}