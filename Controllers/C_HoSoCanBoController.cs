using AppNetShop.DomainModal;
using AppNetShop.DTO;
using Microsoft.Ajax.Utilities;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;

namespace AppNetShop.Controllers
{
    public class C_HoSoCanBoController : Controller
    {
        // GET: C_HoSoCanBo
        public ActionResult C_HoSoCanBo()
        {
            return View();
        }

        XEntities _query = new XEntities();

        [HttpGet]
        public JsonResult LoadData()
        {
            try
            {
                var lstNganHang = _query.AspDanhMucHeThong.Where(c => c.phan_loai.Contains("Danh mục ngân hàng")
                ).Select(c => new
                {
                    c.Id,
                    c.ten_goi,
                }).ToList();

                return Json(new
                {
                    lstNganHang,
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
        public JsonResult LoadDetail(Int64? IdCanBo)
        {
            try
            {
                var listData = _query.AspHoSoCanBo.Where(c => c.IdCanBo == IdCanBo).AsEnumerable().Select(c => new
                {
                    c.IdCanBo,
                    c.so_hieu_giay_to,
                    c.so_dien_thoai,
                    c.ho_ten,
                    c.gioi_tinh,
                    c.dia_chi,
                    c.cb_tinh,
                    c.cb_quan_huyen,
                    c.cb_xa_phuong,
                    c.cb_trang_thai_lam_viec,
                    c.cb_ngan_hang,
                    c.so_tai_khoan,
                    c.ma_bin_ngan_hang,
                    c.ho_ten_chu_tai_khoan,
                    c.chuc_vu,
                    c.email,
                    c.url_avatar,
                    ngay_sinh = c.ngay_sinh != null ? string.Format("{0:yyyy-MM-dd}", c.ngay_sinh) : "",
                    ngay_bat_dau_cong_tac = c.ngay_bat_dau_cong_tac != null ? string.Format("{0:yyyy-MM-dd}", c.ngay_bat_dau_cong_tac) : "",
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
        public JsonResult LoadTable()
        {
            try
            {
                int stt = 1;
                var lstAccount = _query.AspHoSoCanBo.AsEnumerable().Select(c => new
                {
                    stt = stt++,
                    c.IdCanBo,
                    c.so_hieu_giay_to,
                    c.cb_ngan_hang,
                    c.so_tai_khoan,
                    c.chuc_vu,
                    c.cb_trang_thai_lam_viec,
                    c.ho_ten,
                    c.gioi_tinh,
                    ngay_sinh = c.ngay_sinh != null ? string.Format("{0:dd-MM-yyyy}", c.ngay_sinh) : string.Empty,
                    c.so_dien_thoai,
                    c.url_avatar,
                    c.name_avatar
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

        [HttpPost]
        public JsonResult DeleteData(Int64? IdCanBo)
        {
            try
            {
                var data = _query.AspHoSoCanBo.Find(IdCanBo);
                if (data != null)
                {
                    _query.AspHoSoCanBo.Remove(data);
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
        public JsonResult SaveNganHang(Int64? IdCanBo , string cb_ngan_hang, Int64? bin ,string ho_ten_chu_tai_khoan , string so_tai_khoan)
        {
            try
            {
                var data = _query.AspHoSoCanBo.Find(IdCanBo);
                if (data != null)
                {
                    data.cb_ngan_hang = cb_ngan_hang;
                    data.ma_bin_ngan_hang = bin;
                    data.ho_ten_chu_tai_khoan = ho_ten_chu_tai_khoan;
                    data.so_tai_khoan = so_tai_khoan;
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

        private string apiBaseUrl = "https://api.vietqr.io/v2";
        private string apiKey = "dbfdf938-9d79-467b-b741-0bf83e1c7a60";
        private string clientId = "b8123d97-17d4-455b-a714-0e427b98410a";
        [HttpGet]
        public async Task<ActionResult> PaymentLoad(Int64 IdCanBo)
        {
            try
            {
                var lstAccount = _query.AspHoSoCanBo.Find(IdCanBo);

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
                        amount = 0,
                        //addInfo = "",
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
        public class ApiQr
        {
            public string code;
            public string data;

        }


        [HttpPost]
        public JsonResult SaveData(string strData)
        {
            string duong_dan_tai_lieu = "";
            string ten_file = "";
            var ClientData = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<AspHoSoCanBo>(strData);
            try
            {
                var CheckSoHieu = _query.AspHoSoCanBo.Where(a =>
                (ClientData.IdCanBo == 0 ? (a.so_hieu_giay_to == ClientData.so_hieu_giay_to && ClientData.IdCanBo == 0) :
                (a.so_hieu_giay_to == ClientData.so_hieu_giay_to && ClientData.IdCanBo != a.IdCanBo))).Count();
                if (CheckSoHieu > 0)
                {
                    return Json(new
                    {
                        message = "Số hiệu/CMND đã tồn tại",
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
                        var pathName = "~/FilesUploads/" + ClientData.so_hieu_giay_to.Trim() + "_" + String.Format("{0:yyyyMM}", DateTime.Now);
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


                if (ClientData.IdCanBo == 0)
                {
                    ClientData.url_avatar = duong_dan_tai_lieu;
                    ClientData.name_avatar = ten_file;
                    _query.AspHoSoCanBo.Add(ClientData);
                    _query.SaveChanges();
                }
                else
                {
                    var _data = _query.AspHoSoCanBo.Find(ClientData.IdCanBo);
                    if (_data != null)
                    {
                        _data.cb_trang_thai_lam_viec = ClientData.cb_trang_thai_lam_viec;
                        _data.ngay_bat_dau_cong_tac = ClientData.ngay_bat_dau_cong_tac;
                        _data.chuc_vu = ClientData.chuc_vu;
                        _data.so_hieu_giay_to = ClientData.so_hieu_giay_to;
                        _data.ho_ten = ClientData.ho_ten;
                        _data.gioi_tinh = ClientData.gioi_tinh;
                        _data.ngay_sinh = ClientData.ngay_sinh;
                        _data.so_dien_thoai = ClientData.so_dien_thoai;
                        _data.email = ClientData.email;
                        _data.cb_ngan_hang = ClientData.cb_ngan_hang;
                        _data.so_tai_khoan = ClientData.so_tai_khoan;
                        _data.cb_tinh = ClientData.cb_tinh;
                        _data.cb_quan_huyen = ClientData.cb_quan_huyen;
                        _data.cb_xa_phuong = ClientData.cb_xa_phuong;
                        _data.dia_chi = ClientData.dia_chi;
                        if (duong_dan_tai_lieu != "")
                        {
                            _data.url_avatar = duong_dan_tai_lieu;
                            _data.name_avatar = ten_file;
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
    }
}