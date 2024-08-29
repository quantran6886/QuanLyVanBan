using AppNetShop.DomainModal;
using AppNetShop.DTO;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.IO.Compression;
using System.Collections.Generic;
using System.Drawing;

namespace AppNetShop.Controllers
{
    public class ThuMucLuuTruController : Controller
    {
        // GET: ThuMucLuuTru
        public ActionResult ThuMucLuuTru()
        {
            return View();
        }
        XEntities _query = new XEntities();

        [HttpPost]
        public JsonResult SaveFile()
        {
            string message = "";
            bool status = false;
            string duong_dan_tai_lieu = "";
            string ten_file = "";
            string extention = "";
            var user = GetUserSession.getInfo();
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
                        var pathName = "~/FilesUploads/TaiLieuLuuTru_" + String.Format("{0:yyyyMM}", DateTime.Now);
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
        public JsonResult LoadFile()
        {
            try
            {
                var user = GetUserSession.getInfo();

                var data = _query.AspStoreDocument.AsEnumerable().Select(c => new
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
        public JsonResult DeleteFile(Int64? IdDocument)
        {
            try
            {
                var data = _query.AspStoreDocument.Find(IdDocument);
                if (data != null)
                {
                    _query.AspStoreDocument.Remove(data);
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