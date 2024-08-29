using AppNetShop.DomainModal;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace AppNetShop.Controllers
{
    public class C_PhieuThuController : Controller
    {
        // GET: C_PhieuThu
        public ActionResult C_PhieuThu()
        {
            return View();
        }

        XEntities _query = new XEntities();

        [HttpGet]
        public JsonResult GetData()
        {
            try
            {
                var ngay_tao = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                var don_vi_thu = "Công ty TNHH Chuyển Giao Phần Mềm Diamond";

                return Json(new
                {
                    ngay_tao,
                    don_vi_thu,
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

        [OutputCache(CacheProfile = "Cache24Hours", Location = OutputCacheLocation.Client, NoStore = true)]
        [HttpGet]
        public JsonResult LoadNoiDung(string noi_dung_thu)
        {
            try
            {
                var lstData = _query.AspKeHoachThu.Where(c => c.is_chuyen_thung_rac != true && c.noi_dung_thu.Contains(noi_dung_thu)
                ).AsEnumerable().Select(c => new
                {
                 c.noi_dung_thu,
                }).Take(10).ToList();

                return Json(new
                {
                    lstData,
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
        [HttpGet]
        public JsonResult LoadTable()
        {
            try
            {
                int stt = 1;
                var lstSoTienThu = _query.AspThuThucTe.ToList();
                var Data = _query.AspKeHoachThu.Where(c => c.is_chuyen_thung_rac != true).AsEnumerable().Select(c => new
                {
                    c.IdKeHoach,
                    c.don_vi_thu,
                    c.dia_chi_thu,
                    c.noi_dung_thu,
                    c.ly_do_thu,
                    so_tien_thu_du_kien = c.so_tien_thu_du_kien != null ? string.Format("{0:#,##0}", c.so_tien_thu_du_kien) : "",
                    c.url_minh_chung,
                    ngay_tao_phieu = c.ngay_tao_phieu != null ? string.Format("{0:dd-MM-yyyy}", c.ngay_tao_phieu) : "",
                    so_tien_thu = lstSoTienThu.Where(p => p.IdKeHoach == c.IdKeHoach).Sum(s => s.so_tien_thu),
                }).ToList();

                var listData = Data.AsEnumerable().Select(c => new
                {
                    c.IdKeHoach,
                    stt = stt++,
                    c.don_vi_thu,
                    c.dia_chi_thu,
                    c.noi_dung_thu,
                    c.ly_do_thu,
                    c.so_tien_thu_du_kien,
                    so_tien_thu = c.so_tien_thu != null ? string.Format("{0:#,##0}", c.so_tien_thu) : "",
                    c.url_minh_chung,
                    c.ngay_tao_phieu,
                }).ToList();

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


        [HttpGet]
        public JsonResult LoadSoLieu()
        {
            try
            {
                int stt = 1;
                var lstSoTienThu = _query.AspKeHoachThu.ToList();
                var lstSoTienDaThu = _query.AspThuThucTe.AsEnumerable().Select(c => new
                {
                    c.so_tien_thu,
                    ngay_thu = string.Format("{0:dd-MM-yyyy}", c.ngay_thu),
                }).ToList();
                var ngay_hien_tai = string.Format("{0:dd-MM-yyyy}", DateTime.Now);

                var so_tien_thu_du_kien = lstSoTienThu.Sum(c => c.so_tien_thu_du_kien);
                var so_tien_da_thu = lstSoTienDaThu.Sum(c => c.so_tien_thu);
                var so_tien_da_thu_trong_ngay = lstSoTienDaThu.Where(c => c.ngay_thu == ngay_hien_tai).Sum(c => c.so_tien_thu);
                string so_tien_thu_du_kien_text = string.Format("{0:#,##0}", so_tien_thu_du_kien);
                string so_tien_da_thu_text = string.Format("{0:#,##0}", so_tien_da_thu);
                string so_tien_da_thu_trong_ngay_text = string.Format("{0:#,##0}", so_tien_da_thu_trong_ngay);
                string so_phieu_tao = lstSoTienThu.Count().ToString();
                return Json(new
                {
                    so_tien_thu_du_kien_text,
                    so_tien_da_thu_text,
                    so_tien_da_thu_trong_ngay_text,
                    so_phieu_tao,
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

        [HttpGet]
        public JsonResult GetDataDST(Int64? IdKeHoach)
        {
            try
            {
                int stt = 1;
                int stt1 = 1;
                int stt2 = 1;
                var listData = _query.AspThuThucTe.Where(c => c.IdKeHoach == IdKeHoach).AsEnumerable().Select(c => new
                {
                    c.IdThu,
                    c.IdKeHoach,
                    stt = stt++,
                    ten_cn_tc = c.phan_loai_doi_tuong == "Cá nhân" ? c.ho_ten_ca_nhan : c.ten_don_vi,
                    c.ho_ten_ca_nhan,
                    c.ten_don_vi,
                    c.ma_so_dv,
                    c.so_hieu_giay_to,
                    c.noi_dung_thu,
                    c.phan_loai_doi_tuong,
                    so_tien_thu = c.so_tien_thu != null ? string.Format("{0:#,##0}", c.so_tien_thu) : "",
                    ngay_thu = c.ngay_thu != null ? string.Format("{0:dd-MM-yyyy}", c.ngay_thu) : "",
                }).ToList();

                var lstThuCaNhan = listData.Where(c => c.phan_loai_doi_tuong == "Cá nhân").AsEnumerable()
                    .Select(c => new
                    {
                        c.IdThu,
                        c.IdKeHoach,
                        stt = stt1++,
                        c.ma_so_dv,
                        c.so_hieu_giay_to,
                        c.ho_ten_ca_nhan,
                        c.ten_don_vi,
                        c.noi_dung_thu,
                        c.phan_loai_doi_tuong,
                        so_tien_thu = c.so_tien_thu != null ? string.Format("{0:#,##0}", c.so_tien_thu) : "",
                        ngay_thu = c.ngay_thu != null ? string.Format("{0:dd-MM-yyyy}", c.ngay_thu) : "",
                    }).ToList();

                var lstThuTapThe = listData.Where(c => c.phan_loai_doi_tuong == "Tập thể").AsEnumerable()
                   .Select(c => new
                   {
                       c.IdThu,
                       c.IdKeHoach,
                       stt = stt2++,
                       c.ma_so_dv,
                       c.so_hieu_giay_to,
                       c.ho_ten_ca_nhan,
                       c.ten_don_vi,
                       c.noi_dung_thu,
                       c.phan_loai_doi_tuong,
                       so_tien_thu = c.so_tien_thu != null ? string.Format("{0:#,##0}", c.so_tien_thu) : "",
                       ngay_thu = c.ngay_thu != null ? string.Format("{0:dd-MM-yyyy}", c.ngay_thu) : "",
                   }).ToList();

                return Json(new
                {
                    lstThuCaNhan,
                    lstThuTapThe,
                    lbl_ThuCaNhan = lstThuCaNhan.Count(),
                    lbl_ThuTapThe = lstThuTapThe.Count(),
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

        [HttpGet]
        public JsonResult LoadDetail(Int64? IdKeHoach)
        {
            try
            {
                int stt = 1;
                var listData = _query.AspKeHoachThu.Where(c => c.IdKeHoach == IdKeHoach).AsEnumerable().Select(c => new
                {
                    c.IdKeHoach,
                    stt = stt++,
                    c.don_vi_thu,
                    c.dia_chi_thu,
                    c.noi_dung_thu,
                    c.ly_do_thu,
                    c.thoi_han,
                    c.ho_ten_thu_quy,
                    c.ho_ten_giam_doc,
                    c.ke_toan_truong,
                    c.so_tien_thu_du_kien,
                    ngay_tao_phieu = c.ngay_tao_phieu != null ? string.Format("{0:yyyy-MM-dd}", c.ngay_tao_phieu) : "",
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

        [HttpGet]
        public JsonResult LoadDetailPhieu(Int64? IdThu)
        {
            try
            {
                int stt = 1;
                var listData = _query.AspThuThucTe.Where(c => c.IdThu == IdThu).AsEnumerable().Select(c => new
                {
                    c.IdKeHoach,
                    stt = stt++,
                    c.ten_don_vi,
                    c.dia_chi_don_vi,
                    c.ho_ten_ca_nhan,
                    c.phan_loai_doi_tuong,
                    c.noi_dung_thu,
                    c.ma_so_dv,
                    c.dia_chi_que_quan,
                    c.ke_toan_don_vi,
                    c.so_hieu_giay_to,
                    c.so_tien_thu,
                    ngay_thu = c.ngay_thu != null ? string.Format("{0:yyyy-MM-dd}", c.ngay_thu) : "",
                    ngay_sinh = c.ngay_sinh != null ? string.Format("{0:yyyy-MM-dd}", c.ngay_sinh) : "",
                    c.IsTDSDT,
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
        [HttpGet]
        public JsonResult CheckLuuHeThong(Int64? IdThu, string phan_loai_doi_tuong, string so_hieu_giay_to, string ma_so_dv)
        {
            try
            {
                string noti = "";
                if (phan_loai_doi_tuong == "Cá nhân")
                {
                    var data = _query.AspDSDTThuChi.Where(c => IdThu == 0 && phan_loai_doi_tuong == c.phan_loai_doi_tuong && c.so_hieu_giay_to == so_hieu_giay_to);
                    if (data.Count() > 0)
                    {
                        noti = "Số hiệu đã trùng với số hiệu có trong hệ thống";
                    }
                }
                else
                {
                    var data = _query.AspDSDTThuChi.Where(c => IdThu == 0 && phan_loai_doi_tuong == c.phan_loai_doi_tuong && c.ma_so_dv == ma_so_dv);
                    if (data.Count() > 0)
                    {
                        noti = "Mã đơn vị đã có trong hệ thống";
                    }
                }
                return Json(new
                {
                    noti,
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
        public JsonResult SaveData(string strData)
        {
            try
            {
                Int64 IdKeHoach = 0;

                var ClientData = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<AspKeHoachThu>(strData);
                if (ClientData.IdKeHoach == 0)
                {
                    ClientData.ly_do_thu = HttpUtility.UrlDecode(ClientData.ly_do_thu);
                    _query.AspKeHoachThu.Add(ClientData);
                    _query.SaveChanges();
                    IdKeHoach = ClientData.IdKeHoach;
                }
                else
                {
                    var update = _query.AspKeHoachThu.Find(ClientData.IdKeHoach);
                    if (update != null)
                    {
                        update.noi_dung_thu = ClientData.noi_dung_thu;
                        update.so_tien_thu_du_kien = ClientData.so_tien_thu_du_kien;
                        update.don_vi_thu = ClientData.don_vi_thu;
                        update.ly_do_thu = HttpUtility.UrlDecode(ClientData.ly_do_thu);
                        update.ho_ten_giam_doc = ClientData.ho_ten_giam_doc;
                        update.ke_toan_truong = ClientData.ke_toan_truong;
                        update.ho_ten_thu_quy = ClientData.ho_ten_thu_quy;
                        update.thoi_han = ClientData.thoi_han;
                        IdKeHoach = ClientData.IdKeHoach;
                        _query.SaveChanges();
                    }
                }
                return Json(new
                {
                    IdKeHoach,
                    code = true,
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    message = ex.Message,
                    code = false,
                });
            }
        }

        [HttpPost]
        public JsonResult SavePhieuThu(string strData)
        {
            try
            {
                string noti = "";
                var ClientData = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<AspThuThucTe>(strData);
                if (ClientData.IdThu == 0)
                {
                    _query.AspThuThucTe.Add(ClientData);
                    if (ClientData.IsTDSDT == true)
                    {
                        var data = _query.AspDSDTThuChi.Where(c =>
                        ((c.ma_so_dv == ClientData.ma_so_dv && ClientData.phan_loai_doi_tuong == "Tập thể") ||
                        (c.so_hieu_giay_to == ClientData.so_hieu_giay_to && ClientData.phan_loai_doi_tuong == "Cá nhân")));
                        if (data.Count() > 0)
                        {
                            noti = "Số hiệu/Mã đơn vị đã có trong hệ thống";
                            return Json(new
                            {
                                noti,
                                code = false,
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (noti == "")
                        {
                            _query.SaveChanges();
                            AspDSDTThuChi _ds = new AspDSDTThuChi();
                            _ds.IdThu = ClientData.IdThu;
                            _ds.phan_loai_doi_tuong = ClientData.phan_loai_doi_tuong;
                            _ds.ho_ten_ca_nhan = ClientData.ho_ten_ca_nhan;
                            _ds.gioi_tinh = ClientData.gioi_tinh;
                            _ds.so_hieu_giay_to = ClientData.so_hieu_giay_to;
                            _ds.ngay_sinh = ClientData.ngay_sinh;
                            _ds.dia_chi_don_vi = ClientData.dia_chi_don_vi;
                            _ds.dia_chi_que_quan = ClientData.dia_chi_que_quan;
                            _ds.ke_toan_don_vi = ClientData.dia_chi_que_quan;
                            _ds.ma_so_dv = ClientData.ma_so_dv;
                            _ds.ten_don_vi = ClientData.ten_don_vi;
                            _ds.ngay_tao = DateTime.Now;
                            _ds.so_tien_thu = ClientData.so_tien_thu;
                            _query.AspDSDTThuChi.Add(_ds);
                            _query.SaveChanges();
                        }
                    }
                    else
                    {
                        _query.SaveChanges();
                    }
                }
                else
                {
                    var update = _query.AspThuThucTe.Find(ClientData.IdThu);
                    if (update != null)
                    {
                        update.noi_dung_thu = ClientData.noi_dung_thu;
                        update.so_tien_thu = ClientData.so_tien_thu;
                        update.phan_loai_doi_tuong = ClientData.phan_loai_doi_tuong;
                        update.ho_ten_ca_nhan = ClientData.ho_ten_ca_nhan;
                        update.so_hieu_giay_to = ClientData.so_hieu_giay_to;
                        update.ngay_sinh = ClientData.ngay_sinh;
                        update.dia_chi_que_quan = ClientData.dia_chi_que_quan;
                        update.gioi_tinh = ClientData.gioi_tinh;
                        update.ten_don_vi = ClientData.ten_don_vi;
                        update.ma_so_dv = ClientData.ma_so_dv;
                        update.dia_chi_don_vi = ClientData.dia_chi_don_vi;
                        update.ke_toan_don_vi = ClientData.ke_toan_don_vi;
                        update.ngay_thu = ClientData.ngay_thu;
                        _query.SaveChanges();
                    }
                }
                return Json(new
                {
                    code = true,
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    message = ex.Message,
                    code = false,
                });
            }
        }
        [HttpPost]
        public JsonResult DeleteData(Int64? IdKeHoach)
        {
            try
            {
                var _check = _query.AspThuThucTe.Where(c => c.IdKeHoach == IdKeHoach).Count();

                if (_check > 0)
                {
                    return Json(new
                    {
                        code = false,
                        message = "Đã có bản ghi tồn tại bên trong dữ liệu!không thể xóa",
                    }, JsonRequestBehavior.AllowGet);
                }

                var data = _query.AspKeHoachThu.Find(IdKeHoach);
                if (data != null)
                {
                    _query.AspKeHoachThu.Remove(data);
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


        [HttpPost]
        public JsonResult ChuyenThungRac(Int64? IdKeHoach)
        {
            try
            {

                var data = _query.AspKeHoachThu.Find(IdKeHoach);
                if (data != null)
                {
                    data.is_chuyen_thung_rac = true;
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

        [HttpPost]
        public JsonResult DeletePhieuThu(Int64? IdThu)
        {
            try
            {

                var data = _query.AspDSDTThuChi.Find(IdThu);
                var data2 = _query.AspThuThucTe.Find(IdThu);
                if (data != null)
                {
                    _query.AspDSDTThuChi.Remove(data);
                }
                if (data2 != null)
                {
                    _query.AspThuThucTe.Remove(data2);
                }
                _query.SaveChanges();

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