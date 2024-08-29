using AppNetShop.DomainModal;
using AppNetShop.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppNetShop.Controllers
{
    public class C_NhapHienVatController : Controller
    {
        // GET: C_NhapHienVat
        public ActionResult C_NhapHienVat()
        {
            return View();
        }

        XEntities _query = new XEntities();

        [HttpGet]
        public JsonResult LoadTable(string txtTimKiem, string cbTrangThaiTimKiem)
        {
            try
            {
                int stt = 1;
                var lstData = _query.AspQuanLyHienVat.Where(c =>
                (!string.IsNullOrEmpty(txtTimKiem) ? c.SoDangKy.ToLower().Contains(txtTimKiem.ToLower()) || c.TenHienVat.ToLower().Contains(cbTrangThaiTimKiem.ToLower()) : true)
                && (!string.IsNullOrEmpty(cbTrangThaiTimKiem) ? cbTrangThaiTimKiem == "Hiện vật sưu tầm" ? c.SoDangKy == "" :
                 cbTrangThaiTimKiem == "Hiện vật bảo tàng" ? c.SoDangKy != "" :
                 cbTrangThaiTimKiem == "Hiện vật chưa duyệt" ? c.IsDuyet != true : c.IsDuyet == true
                : true)
                ).AsEnumerable().Select(c => new
                {
                    stt = stt++,
                    c.IdHienVat,
                    SoDangKy = string.IsNullOrEmpty(c.SoDangKy) ? "Chưa có só đăng ký" : c.SoDangKy,
                    c.TenHienVat,
                    c.cbThoiKy,
                    LoaiHienVat = c.IsDiVat == true ? "Di vật" : c.IsCoVat == true ?
                    "Cổ vật" : c.IsBaoVatQG == true ? "Bảo vật quốc gia" : c.IsHienVatKhangChien == true ? "Hiện vật kháng chiến" : "Chưa phân loại",
                    c.cbNguonGoc,
                    cbTrangThai = c.IsDuyet == true ? "<font class='text-success-600'>Đã duyệt</font>" : c.IsDuyet == false ? "<font class='text-dager-600'>Từ chối</font>"
                    : "<font class='text-muted-600'>Chưa duyệt</font>",
                    dtThoiGianHienVat = c.dtThoiGianHienVat != null ? string.Format("{0:dd-MM-yyyyTHH:ss}", c.dtThoiGianHienVat) : string.Empty,
                }).ToList();

                return Json(new
                {
                    lstData,
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
        public JsonResult LoadTaiLieu(Int64? IdHienVat)
        {
            try
            {
                var user = GetUserSession.getInfo();

                var data = _query.AspStoreDocument.Where(c => c.IdHienVat == IdHienVat).AsEnumerable().Select(c => new
                {
                    c.IdDocument,
                    c.url_file,
                    c.ten_file,
                    c.duoi_file,
                    c.ghi_chu,
                }).ToList();

                return Json(new
                {
                    data,
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
        public JsonResult SaveFile(Int64? IdHienVat)
        {
            string message = "";
            bool status = false;
            string duong_dan_tai_lieu = "";
            string ten_file = "";
            string extention = "";
            var user = GetUserSession.getInfo();
            var listData = _query.AspQuanLyHienVat.Find(IdHienVat);
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {

                        var stream = fileContent.InputStream;
                        ten_file = Path.GetFileName(file);
                        extention = Path.GetExtension(file);
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file);
                        var pathName = "~/FilesUploads/LuuTruHienVat_"+ listData.SoDangKy + "_" + listData.IdHienVat + String.Format("{0:yyyyMM}", DateTime.Now);
                        duong_dan_tai_lieu = pathName.Substring(1) + "/" + fileName;
                        bool folderExists = Directory.Exists(Server.MapPath(pathName));
                        if (!folderExists)
                            Directory.CreateDirectory(Server.MapPath(pathName));

                        var path = Path.Combine(Server.MapPath(pathName), fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }

                        AspStoreDocument document = new AspStoreDocument();
                        document.IdDocument = 0;
                        document.url_file = duong_dan_tai_lieu;
                        document.ten_file = ten_file;
                        document.duoi_file = extention;
                        document.IdHienVat = IdHienVat;
                        document.ngay_tao = DateTime.Now;
                        _query.AspStoreDocument.Add(document);
                    }
                }
                _query.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
                message = ex.Message;
            }
            return Json(new
            {
                status = status,
                message
            });
        }


        [HttpGet]
        public JsonResult LoadDetail(Int64? IdHienVat)
        {
            try
            {
                var listData = _query.AspQuanLyHienVat.Where(c => c.IdHienVat == IdHienVat).AsEnumerable().Select(c => new
                {
                    c.IdHienVat,
                    c.TenHienVat,
                    c.TenKhac,
                    c.TenKhoaHoc,
                    c.TenTiengAnh,
                    c.SoDangKy,
                    c.urlHinhAnh,
                    c.urlHinhAnh3D,
                    c.SoLuongHopThanhHV,
                    c.cbThoiKy,
                    c.cbSuuTam,
                    c.cbNguonGoc,
                    c.cbMauSac,
                    c.IsBaoVatQG,
                    c.IsCoVat,
                    c.IsDiVat,
                    c.IsHienVatKhangChien,
                    c.SoLuongThanhPhan,
                    c.NienDaiTuongDoi,
                    c.NienDaiTuyetDoi,
                    c.cbChatLieu,
                    c.KichThuoc,
                    c.ChieuRong,
                    c.ChieuCao,
                    c.TrongLuong,
                    c.cbTinhTrangHv,
                    c.MieuTa,
                    c.DauTichDacBiet,
                    c.QuyenTacGia,
                    c.DieuKhoanThoaThuan,
                    c.QuyenLienQuan,
                    c.cbChuDe,
                    c.SuKienLienQuan,
                    c.NhanChungLienQuan,
                    c.ChuSoHuuHienVat,
                    c.TacGiaLienQuan,
                    c.NoiSanXuatCheTac,
                    c.GiaTriBaoHiem,
                    c.IsDuyet,
                    c.IdDiTich,
                    c.cbCheDoBaoMat,
                    c.DoAm,
                    c.AnhSang,
                    c.IsLock,
                    dtNgayQuyetDinh = c.dtNgayQuyetDinh != null ? string.Format("{0:yyyy-MM-dd}", c.dtQuyetDinhNhapKho) : "",
                    dtQuyetDinhNhapKho = c.dtQuyetDinhNhapKho != null ? string.Format("{0:yyyy-MM-dd}", c.dtQuyetDinhNhapKho) : "",
                    dtTiepNhanHienVat = c.dtTiepNhanHienVat != null ? string.Format("{0:yyyy-MM-dd}", c.dtTiepNhanHienVat) : "",
                    dtDangKyDVCVBV = c.dtDangKyDVCVBV != null ? string.Format("{0:yyyy-MM-dd}", c.dtDangKyDVCVBV) : "",
                    dtThoiGianHienVat = c.dtThoiGianHienVat != null ? string.Format("{0:yyyy-MM-ddTHH:ss}", c.dtThoiGianHienVat) : "",
                }).FirstOrDefault();

                return Json(new
                {
                    listData,
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

        [HttpPost]
        public JsonResult SaveData(string strData, bool is_nhap_lieu)
        {
            string duong_dan_tai_lieu = "";
            string ten_file = "";
            var ClientData = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<AspQuanLyHienVat>(strData);
            try
            {
                var CheckSoHieu = _query.AspQuanLyHienVat.Where(a =>
                (ClientData.IdHienVat == 0 ? (a.SoDangKy == ClientData.SoDangKy && ClientData.IdHienVat == 0) :
                (a.SoDangKy == ClientData.SoDangKy && ClientData.IdHienVat != a.IdHienVat))).Count();
                if (CheckSoHieu > 0 && is_nhap_lieu == false)
                {
                    return Json(new
                    {
                        message = "Số đăng ký đã tồn tại",
                        code = false,
                    }, JsonRequestBehavior.AllowGet);
                }

                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        var stream = fileContent.InputStream;
                        ten_file = Path.GetFileName(file);
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file);
                        var pathName = "~/FilesUploads/" + ClientData.SoDangKy.Trim() + "_" + String.Format("{0:yyyyMM}", DateTime.Now);
                        duong_dan_tai_lieu = pathName.Substring(1) + "/" + fileName;
                        bool folderExists = Directory.Exists(Server.MapPath(pathName));
                        if (!folderExists)
                        {
                            Directory.CreateDirectory(Server.MapPath(pathName));
                        }

                        var path = Path.Combine(Server.MapPath(pathName), fileName);
                        using (var fileStream = System.IO.File.Create(path))
                        {
                            stream.CopyTo(fileStream);
                        }
                    }
                }

                if (ClientData.IdHienVat == 0)
                {
                    ClientData.urlHinhAnh = duong_dan_tai_lieu;
                    _query.AspQuanLyHienVat.Add(ClientData);
                    _query.SaveChanges();
                }
                else
                {
                    var _data = _query.AspQuanLyHienVat.Find(ClientData.IdHienVat);
                    if (_data != null)
                    {
                        _data.TenHienVat = ClientData.TenHienVat;
                        _data.TenKhac = ClientData.TenKhac;
                        _data.TenKhoaHoc = ClientData.TenKhoaHoc;
                        _data.TenTiengAnh = ClientData.TenTiengAnh;
                        _data.SoDangKy = ClientData.SoDangKy;
                        _data.SoQuyetDinhNhapKho = ClientData.SoQuyetDinhNhapKho;
                        _data.dtQuyetDinhNhapKho = ClientData.dtQuyetDinhNhapKho;
                        _data.dtTiepNhanHienVat = ClientData.dtTiepNhanHienVat;
                        _data.dtDangKyDVCVBV = ClientData.dtDangKyDVCVBV;
                        _data.cbNguonGoc = ClientData.cbNguonGoc;
                        _data.SoLuongHopThanhHV = ClientData.SoLuongHopThanhHV;
                        _data.SoLuongThanhPhan = ClientData.SoLuongThanhPhan;
                        _data.cbThoiKy = ClientData.cbThoiKy;
                        _data.NienDaiTuongDoi = ClientData.NienDaiTuongDoi;
                        _data.IsDiVat = ClientData.IsDiVat;
                        _data.IsCoVat = ClientData.IsCoVat;
                        _data.IsBaoVatQG = ClientData.IsBaoVatQG;
                        _data.IsHienVatKhangChien = ClientData.IsHienVatKhangChien;
                        _data.cbChatLieu = ClientData.cbChatLieu;
                        _data.KichThuoc = ClientData.KichThuoc;
                        _data.ChieuRong = ClientData.ChieuRong;
                        _data.ChieuCao = ClientData.ChieuCao;
                        _data.TrongLuong = ClientData.TrongLuong;
                        _data.cbTinhTrangHv = ClientData.cbTinhTrangHv;
                        _data.MieuTa = ClientData.MieuTa;
                        _data.DauTichDacBiet = ClientData.DauTichDacBiet;
                        _data.QuyenTacGia = ClientData.QuyenTacGia;
                        _data.DieuKhoanThoaThuan = ClientData.DieuKhoanThoaThuan;
                        _data.QuyenLienQuan = ClientData.QuyenLienQuan;
                        _data.cbChuDe = ClientData.cbChuDe;
                        _data.cbSuuTam = ClientData.cbSuuTam;
                        _data.SuKienLienQuan = ClientData.SuKienLienQuan;
                        _data.NhanChungLienQuan = ClientData.NhanChungLienQuan;
                        _data.ChuSoHuuHienVat = ClientData.ChuSoHuuHienVat;
                        _data.TacGiaLienQuan = ClientData.TacGiaLienQuan;
                        _data.NoiSanXuatCheTac = ClientData.NoiSanXuatCheTac;
                        _data.GiaTriBaoHiem = ClientData.GiaTriBaoHiem;
                        _data.QuyetDinhDinhGia = ClientData.QuyetDinhDinhGia;
                        _data.dtNgayQuyetDinh = ClientData.dtNgayQuyetDinh;
                        _data.IsDuyet = ClientData.IsDuyet;
                        _data.IdDiTich = ClientData.IdDiTich;
                        _data.cbCheDoBaoMat = ClientData.cbCheDoBaoMat;
                        _data.KyThuatCheTac = ClientData.KyThuatCheTac;
                        _data.cbMauSac = ClientData.cbMauSac;
                        _data.DiaChiSuuTam = ClientData.DiaChiSuuTam;
                        _data.NhietDo = ClientData.NhietDo;
                        _data.DoAm = ClientData.DoAm;
                        _data.AnhSang = ClientData.AnhSang;
                        _data.dtThoiGianHienVat = ClientData.dtThoiGianHienVat;
                        _data.urlHinhAnh3D = ClientData.urlHinhAnh3D;
                        if (duong_dan_tai_lieu != "")
                        {
                            _data.urlHinhAnh = duong_dan_tai_lieu;
                        }
                    }
                    _query.SaveChanges();
                }

                return Json(new
                {
                    code = true,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    message = ex.Message,
                    code = false,
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult DeleteData(Int64? IdHienVat)
        {
            try
            {
                var data = _query.AspQuanLyHienVat.Find(IdHienVat);
                if (data != null)
                {
                    _query.AspQuanLyHienVat.Remove(data);
                    _query.SaveChanges();
                }

                return Json(new
                {
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