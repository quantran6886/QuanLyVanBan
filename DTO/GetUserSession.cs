using AppNetShop.Authentizor;
using AppNetShop.DomainModal;
using AppNetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppNetShop.DTO
{
    public class GetUserSession
    {

        public static UserLogin getInfo()
        {
            var user = System.Web.HttpContext.Current.Session["USER_SESSION"] as UserLogin;
            return user;
        }

        public static UserLogin GetUserInFo(string username)
        {
            try
            {
                XEntities _sv = new XEntities();

                var _userLoged = _sv.AspNetUsers.Where(c => c.UserName == username).FirstOrDefault();
                var nam_lv = DateTime.Now.Year;

                if (_userLoged != null)
                {
                    var _userDetail = _sv.View_Session.Where(c => c.UsersId == _userLoged.Id).FirstOrDefault();

                    var user = new UserLogin()
                    {
                        UserID = _userLoged.Id,
                        UserName = _userLoged.UserName,
                        IdUser = _userDetail.IdUser,
                        ho_ten = _userDetail.ho_ten,
                        so_dien_thoai = _userDetail.so_dien_thoai,
                        gioi_Tinh = _userDetail.gioi_Tinh,
                        ngay_sinh = _userDetail.ngay_sinh,
                        url_avatar = _userDetail.url_avatar,
                        name_avatar = _userDetail.name_avatar,
                    };

                    return user;
                }
                else
                {
                    var user = new UserLogin()
                    {
                        UserID = _userLoged.Id,
                        UserName = _userLoged.UserName,
                    };
                    return user;

                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}