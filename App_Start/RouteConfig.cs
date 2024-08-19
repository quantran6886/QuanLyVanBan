using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AppNetShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            
            routes.MapRoute(name: "phong-trung-bay", url: "phong-trung-bay", defaults: new { controller = "C_PhongTrungBay", action = "C_PhongTrungBay", id = UrlParameter.Optional });
            routes.MapRoute(name: "phieu-giao-nhan", url: "phieu-giao-nhan", defaults: new { controller = "C_PhieuGiaoNhan", action = "C_PhieuGiaoNhan", id = UrlParameter.Optional });
            routes.MapRoute(name: "nhap-hien-vat", url: "nhap-hien-vat", defaults: new { controller = "C_NhapHienVat", action = "C_NhapHienVat", id = UrlParameter.Optional });
            routes.MapRoute(name: "cham-cong", url: "cham-cong", defaults: new { controller = "C_ChamCong", action = "C_ChamCong", id = UrlParameter.Optional });
            routes.MapRoute(name: "du-lieu-da-xoa", url: "du-lieu-da-xoa", defaults: new { controller = "C_KhoiPhucDuLieu", action = "C_KhoiPhucDuLieu", id = UrlParameter.Optional });
            routes.MapRoute(name: "quan-ly-phieu-chi", url: "quan-ly-phieu-chi", defaults: new { controller = "C_PhieuChi", action = "C_PhieuChi", id = UrlParameter.Optional });
            routes.MapRoute(name: "quan-ly-phieu-thu", url: "quan-ly-phieu-thu", defaults: new { controller = "C_PhieuThu", action = "C_PhieuThu", id = UrlParameter.Optional });
            routes.MapRoute(name: "danh-sach-khach_hang", url: "danh-sach-khach_hang", defaults: new { controller = "C_DanhSachKhachHang", action = "C_DanhSachKhachHang", id = UrlParameter.Optional });
            routes.MapRoute(name: "danh-sach-tai-khoan", url: "danh-sach-tai-khoan", defaults: new { controller = "C_DanhSachTaiKhoan", action = "C_DanhSachTaiKhoan", id = UrlParameter.Optional });
            routes.MapRoute(name: "ho-so-can-bo", url: "ho-so-can-bo",defaults: new { controller = "C_HoSoCanBo", action = "C_HoSoCanBo", id = UrlParameter.Optional });
            routes.MapRoute( name: "trung-tam-tin-nhan", url: "trung-tam-tin-nhan",defaults: new { controller = "TrungTamTinNhan", action = "TrungTamTinNhan", id = UrlParameter.Optional });
            routes.MapRoute(name: "c-h-a-t",url: "c-h-a-t",defaults: new { controller = "MessageWebSockets", action = "MessageWebSockets", id = UrlParameter.Optional } );
            routes.MapRoute( name: "thu-muc-luu-tru",url: "thu-muc-luu-tru",defaults: new { controller = "ThuMucLuuTru", action = "ThuMucLuuTru", id = UrlParameter.Optional });
            routes.MapRoute(name: "danh-muc-menu",url: "danh-muc-menu",defaults: new { controller = "ListMenu", action = "ListMenu", id = UrlParameter.Optional });
            routes.MapRoute( name: "", url: "c-h-e-c-k-m-e-n-u", defaults: new { controller = "CheckMenu", action = "CheckMenu", id = UrlParameter.Optional });
            routes.MapRoute(name: "",url: "danh-muc-icon",defaults: new { controller = "ListIcon", action = "ListIcon", id = UrlParameter.Optional });
            routes.MapRoute( name: "phan-quyen",url: "phan-quyen",defaults: new { controller = "RolesFunction", action = "RolesFunction", id = UrlParameter.Optional });
            routes.MapRoute( name: "r-o-l-e-s-e-s-i-o-n",url: "r-o-l-e-s-e-s-i-o-n",defaults: new { controller = "RoleSession", action = "RoleSession", id = UrlParameter.Optional });
            routes.MapRoute(name: "dang-ky-tai-khoan",url: "dang-ky-tai-khoan", defaults: new { controller = "RegesterLogin", action = "RegesterLogin", id = UrlParameter.Optional });
            routes.MapRoute( name: "danh-muc-he-thong", url: "danh-muc-he-thong",defaults: new { controller = "DanhMucHeThong", action = "DanhMucHeThong", id = UrlParameter.Optional });
            routes.MapRoute(name: "t-r-a-n-g-c-h-u", url: "t-r-a-n-g-c-h-u",defaults: new { controller = "DashBoard", action = "DashBoard", id = UrlParameter.Optional });
            routes.MapRoute(name: "dang-nhap-he-thong", url: "dang-nhap-he-thong",defaults: new { controller = "Login", action = "Login", id = UrlParameter.Optional });
            routes.MapRoute(name: "not-found",url: "not-found",defaults: new { controller = "Error", action = "E404", id = UrlParameter.Optional });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "DashBoard", action = "DashBoard", id = UrlParameter.Optional }
            );
        }
    }
}
