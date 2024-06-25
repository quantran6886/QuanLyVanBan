using AppNetShop.DomainModal;
using AppNetShop.DTO;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.IO.Compression;
using System.Collections.Generic;

namespace AppNetShop.Controllers
{
    public class ThuMucLuuTruController : Controller
    {
        // GET: ThuMucLuuTru
        public ActionResult ThuMucLuuTru()
        {
            return View();
        }
        XEntities _en = new XEntities();
        bool is_check = true;

        [HttpPost]
        public JsonResult SaveFile()
        {
            string message = "";
            bool status = false;
            string duong_dan_tai_lieu = "";
            string ten_file = "";
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
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file);
                        var pathName = "~/FilesUploads/" + user.IdUser + "_" + String.Format("{0:yyyyMM}", DateTime.Now);
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
                        document.UrlDocument = duong_dan_tai_lieu;
                        document.NameDocument = ten_file;
                        document.DateNow = DateTime.Now;
                        document.IdUser = user.IdUser;
                        _en.AspStoreDocuments.Add(document);
                    }
                }
                _en.SaveChanges();
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

        public JsonResult LoadFile()
        {
            try
            {
                var user = GetUserSession.getInfo();

                var data = _en.AspStoreDocuments.AsEnumerable().Where(C => C.IdUser == user.IdUser).Select(c => new
                {
                    c.UrlDocument,
                    c.IdDocument,
                    c.NameDocument,
                }).ToList();

                var CountList = data != null ? data.Count() : 0;
                
                return Json(new
                {
                    data,
                    CountList,
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
        public JsonResult DowloadFile(string lstCheckedItem)
        {
            try
            {
                // Tạo tên file ZIP
                string zipFileName = "images.zip";

                // Đường dẫn đến file ZIP tạm thời
                string tempZipFilePath = Path.Combine(Server.MapPath("~/Zips"), zipFileName);

                if (System.IO.File.Exists(tempZipFilePath))
                {
                    System.IO.File.Delete(tempZipFilePath);
                }
                // Tạo một tệp ZIP mới
                using (var archive = ZipFile.Open(tempZipFilePath, ZipArchiveMode.Create))
                {
                    if (lstCheckedItem.EndsWith(","))
                    {
                        var data = lstCheckedItem.Split(',').ToArray();
                        foreach (var item in data)
                        {
                            if (item != "")
                            {
                                var _itemImage = _en.AspStoreDocuments.Where(c => c.IdDocument.ToString() == item.ToString()).FirstOrDefault();
                                string absolutePath = Server.MapPath("~" + _itemImage.UrlDocument);

                                // Thêm tệp ảnh vào tệp ZIP
                                archive.CreateEntryFromFile(absolutePath, Path.GetFileName(absolutePath));
                            }
                        }
                    }
                }

                // Trả về file ZIP để tải xuống
                return Json(new
                {
                    status = true,
                    tempZipFilePath = "Zips/" + zipFileName,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                // Xử lý lỗi nếu có
                return Json(new
                {
                    status = false,
                }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult RemoveFile(string lstCheckedItem)
        {
            try
            {
                if (lstCheckedItem.EndsWith(","))
                {
                    var data = lstCheckedItem.Split(',').ToArray();
                    foreach (var item in data)
                    {
                        if (item != "")
                        {
                            var _itemImage = _en.AspStoreDocuments.Where(c => c.IdDocument.ToString() == item.ToString()).FirstOrDefault();

                            string fullPath = Request.MapPath(_itemImage.UrlDocument);
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }
                            _en.AspStoreDocuments.Remove(_itemImage);
                        }
                    }
                    _en.SaveChanges();
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