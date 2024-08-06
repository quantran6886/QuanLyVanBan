using AppNetShop.DomainModal;
using AppNetShop.DTO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace AppNetShop.Controllers
{
    public class C_ChamCongController : Controller
    {
        // GET: C_ChamCong
        public ActionResult C_ChamCong()
        {
            return View();
        }

        XEntities _query = new XEntities();

        [HttpGet]
        public JsonResult LoadData()
        {
            try
            {
                var DateNow = DateTime.Now;

                var ngay_dau_thang = C_GetDate.FirstDayMonth(DateNow);
                var ngay_cuoi_thang = C_GetDate.LastDayMonth(DateNow);
                var tong_so_ngay = C_GetDate.SumMonth(DateNow);

                return Json(new
                {
                    txt_ngay_dau_thang = string.Format("{0:yyyy-MM-dd}", ngay_dau_thang),
                    txt_ngay_cuoi_thang = string.Format("{0:yyyy-MM-dd}", ngay_cuoi_thang),
                    txt_ngay_hien_tai = string.Format("{0:yyyy-MM-dd}", DateNow),
                    tong_so_ngay,
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
        public JsonResult LoadThongKe(DateTime? txt_ngay_dau_thang, DateTime? txt_ngay_cuoi_thang)
        {
            try
            {
                List<DateTime?> dates = C_GetDate.GetAllDates(txt_ngay_dau_thang, txt_ngay_cuoi_thang);
                var lstData = _query.AspChamCongs.Where(c => c.ngay_lam >= txt_ngay_dau_thang && c.ngay_lam <= txt_ngay_cuoi_thang).Select(c => new
                {
                    c.ngay_lam,
                    c.IsLamNuaNgay,
                    c.IsLamCaNgay,
                    c.ghi_chu,
                    cong = c.IsLamCaNgay == true ? 1 : c.IsLamNuaNgay == true ? 0.5 : 0,
                }).ToList();

                List<AspThongKeNgayLam> lstThongKex = new List<AspThongKeNgayLam>();

                foreach (var item in dates)
                {
                    AspThongKeNgayLam thong_ke = new AspThongKeNgayLam()
                    {
                        ngay_cham_cong = item,
                        so_nguoi_lam = lstData.Where(c => c.ngay_lam == item && (c.IsLamCaNgay == true || c.IsLamNuaNgay == true)).Count(),
                        tong_cong = lstData.Where(c => c.ngay_lam == item && (c.IsLamCaNgay == true || c.IsLamNuaNgay == true)).Sum(c => c.cong),
                        trang_thai = lstData.Where(c => c.ngay_lam == item && (c.IsLamCaNgay == true || c.IsLamNuaNgay == true)).Count() > 0 ? "Đã chấm" : "Chưa chấm công",
                    };
                    lstThongKex.Add(thong_ke);
                }
                var lstThongKe = lstThongKex.AsEnumerable().Select(c => new
                {
                    ngay_cham_cong = c.ngay_cham_cong != null ? string.Format("{0:dd-MM-yyyy}", c.ngay_cham_cong) : string.Empty,
                    ngay_lam = c.ngay_cham_cong != null ? string.Format("{0:yyyy-MM-dd}", c.ngay_cham_cong) : string.Empty,
                    c.so_nguoi_lam,
                    c.tong_cong,
                    c.trang_thai,
                    tong_tien_cong = c.tong_cong > 0 ? string.Format("{0:#,##0}", c.tong_cong * 250000) : "0",
                }).ToList();

                return Json(new
                {
                    lstThongKe,
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
        public class AspThongKeNgayLam
        {
            public DateTime? ngay_cham_cong { get; set; }
            public Int32? so_nguoi_lam { get; set; }
            public Double? tong_cong { get; set; }
            public string trang_thai { get; set; }
        }

        [HttpGet]
        public JsonResult LoadTable(DateTime ngay_lam)
        {
            try
            {
                int stt = 1;
                var data = _query.AspHoSoCanBoes.Where(c => ngay_lam <= DateTime.Now).AsEnumerable().Select(c => new
                {
                    stt = stt++,
                    c.IdCanBo,
                    c.so_hieu_giay_to,
                    c.cb_ngan_hang,
                    c.so_tai_khoan,
                    c.chuc_vu,
                    c.ma_bin_ngan_hang,
                    c.cb_trang_thai_lam_viec,
                    c.ho_ten,
                    c.gioi_tinh,
                    ngay_sinh = c.ngay_sinh != null ? string.Format("{0:dd-MM-yyyy}", c.ngay_sinh) : string.Empty,
                    c.so_dien_thoai,
                    c.url_avatar,
                    c.name_avatar,
                    cham_nua_ngay = ChamCongNuaNgay(c.IdCanBo, ngay_lam),
                    cham_ca_ngay = ChamCongNgay(c.IdCanBo, ngay_lam),
                    tong_cong = TongCong(c.IdCanBo),
                }).ToList();

                var lstAccount = data.Select(c => new
                {
                    c.IdCanBo,
                    c.so_hieu_giay_to,
                    c.ho_ten,
                    c.cb_ngan_hang,
                    c.so_tai_khoan,
                    c.ma_bin_ngan_hang,
                    c.cham_ca_ngay,
                    c.cham_nua_ngay,
                    c.tong_cong,
                    so_tien_quy_doi = c.tong_cong > 0 ? string.Format("{0:#,##0}", c.tong_cong * 250000) : "0",
                    so_tien = c.tong_cong > 0 ? c.tong_cong * 250000 : 0,
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

        private string apiBaseUrl = "https://api.vietqr.io/v2";
        private string apiKey = "dbfdf938-9d79-467b-b741-0bf83e1c7a60";
        private string clientId = "b8123d97-17d4-455b-a714-0e427b98410a";
        [HttpGet]
        public async Task<ActionResult> PaymentLoad(Int64 IdCanBo, Double so_tien)
        {
            try
            {
                var lstAccount = _query.AspHoSoCanBoes.Find(IdCanBo);

                if (string.IsNullOrEmpty(lstAccount.so_tai_khoan) || string.IsNullOrEmpty(lstAccount.cb_ngan_hang))
                {
                    return Json(new
                    {
                        message = "Bạn chưa cài đặt ngân hàng",
                        status = false,
                    }, JsonRequestBehavior.AllowGet);
                }

                if (lstAccount != null)
                {
                    var data = new
                    {
                        accountNo = lstAccount.so_tai_khoan,
                        accountName = lstAccount.ho_ten_chu_tai_khoan,
                        acqId = lstAccount.ma_bin_ngan_hang,
                        amount = so_tien,
                        addInfo = "Thanh toán tiền công tháng " + string.Format("{0:yyyy}", DateTime.Now),
                        format = "text",
                        template = "TgXfUW3"
                    };

                    string jsonData = JsonConvert.SerializeObject(data);
                    string apiUrl = $"{apiBaseUrl}/generate";
                    using (HttpClient client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("x-client-id", clientId);
                        client.DefaultRequestHeaders.Add("x-api-key", apiKey);
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                        if (response.IsSuccessStatusCode)
                        {
                            string qrCodeBase64 = await response.Content.ReadAsStringAsync();
                            var jsonObject = JObject.Parse(qrCodeBase64);
                            string qrDataURL = jsonObject["data"]["qrDataURL"].ToString();

                            return Json(new { success = true, images = qrDataURL }, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            // Xử lý khi có lỗi từ API
                            string errorMessage = await response.Content.ReadAsStringAsync();
                            return Json(new { success = false, message = $"Failed to generate QR code. Status code: {response.StatusCode}. Error message: {errorMessage}" }, JsonRequestBehavior.AllowGet);
                        }
                    }
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

        [HttpPost]
        public JsonResult SaveChamCong(Int64? IdCanBo, bool IsLamNuaNgay, bool IsLamCaNgay, DateTime? ngay_lam, Int32 check)
        {
            try
            {
                var data = _query.AspChamCongs.Where(c => c.IdCanBo == IdCanBo && c.ngay_lam == ngay_lam).FirstOrDefault();
                if (data != null)
                {
                    if (check == 1)
                    {
                        IsLamCaNgay = false;
                    }
                    else
                    {
                        IsLamNuaNgay = false;
                    }
                    data.IsLamNuaNgay = IsLamNuaNgay;
                    data.IsLamCaNgay = IsLamCaNgay;
                    data.IdCanBo = IdCanBo;
                    data.ngay_lam = ngay_lam;
                    _query.SaveChanges();
                }
                else
                {
                    if (check == 1)
                    {
                        IsLamCaNgay = false;
                    }
                    else
                    {
                        IsLamNuaNgay = false;
                    }
                    AspChamCong aspChamCong = new AspChamCong();
                    aspChamCong.IsLamNuaNgay = IsLamNuaNgay;
                    aspChamCong.IsLamCaNgay = IsLamCaNgay;
                    aspChamCong.IdCanBo = IdCanBo;
                    aspChamCong.ngay_lam = ngay_lam;
                    _query.AspChamCongs.Add(aspChamCong);
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


        public bool ChamCongNuaNgay(Int64 IdCanBo, DateTime ngay_lam)
        {
            bool is_check = false;
            string ngay_lamx = string.Format("{0:yyyy-MM-dd}", ngay_lam);

            var data = _query.AspChamCongs.Where(c => c.ngay_lam.ToString() == ngay_lamx && c.IdCanBo == IdCanBo).FirstOrDefault();
            if (data != null)
            {
                is_check = data.IsLamNuaNgay == true ? true : false;
            }
            else
            {
                is_check = false;
            }

            return is_check;
        }

        public bool ChamCongNgay(Int64 IdCanBo, DateTime ngay_lam)
        {
            bool is_check = false;
            string ngay_lamx = string.Format("{0:yyyy-MM-dd}", ngay_lam);

            var data = _query.AspChamCongs.Where(c => c.ngay_lam.ToString() == ngay_lamx && c.IdCanBo == IdCanBo).FirstOrDefault();
            if (data != null)
            {
                is_check = data.IsLamCaNgay == true ? true : false;
            }
            else
            {
                is_check = false;
            }

            return is_check;
        }
        public Double TongCong(Int64 IdCanBo)
        {
            var DateNow = DateTime.Now;
            Double cong = 0;

            var ngay_dau_thang = C_GetDate.FirstDayMonth(DateNow);
            var ngay_cuoi_thang = C_GetDate.LastDayMonth(DateNow);

            var data = _query.AspChamCongs.Where(c => c.IdCanBo == IdCanBo && (c.ngay_lam >= ngay_dau_thang) && (c.ngay_lam <= ngay_cuoi_thang)).AsEnumerable().ToList();
            if (data != null)
            {
                foreach (var item in data)
                {
                    var data1 = item.IsLamCaNgay == true ? 1 : 0;
                    var data2 = item.IsLamNuaNgay == true ? 0.5 : 0;
                    cong += data2 + data1;
                }
            }

            return cong;
        }

    }
}