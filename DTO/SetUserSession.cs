using AppNetShop.Authentizor;
using AppNetShop.DomainModal;
using AppNetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppNetShop.DTO
{
    public class SetUserSession
    {
        public static UserLogin SetUserInFo(string username)
        {
            try
            {

                var _currentUser = System.Web.HttpContext.Current.Session["USER_SESSION"] as UserLogin;
                if (_currentUser != null)
                {
                    return _currentUser;
                }
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
                        ngay_sinh = _userDetail.ngay_sinh,
                        so_dien_thoai = _userDetail.so_dien_thoai,
                        gioi_Tinh = _userDetail.gioi_Tinh,
                        name_avatar = _userDetail.name_avatar,
                        url_avatar = _userDetail.url_avatar,
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