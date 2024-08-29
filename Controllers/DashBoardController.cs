using AppNetShop.Controllers;
using AppNetShop.DomainModal;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiamondSubject.Controllers
{
    public class DashBoardController : BaseSession
    {
        [Authorize]
        // GET: DashBoard
        public ActionResult DashBoard()
        {
            return View();
        }

        XEntities _query = new XEntities();

        [HttpGet]
        public JsonResult LoadData()
        {
            try
            {
                var dataHienVat = _query.AspQuanLyHienVat.ToList();

                var countTHV = dataHienVat.Count();
                var countHVCSDK = dataHienVat.Where(c => !string.IsNullOrEmpty(c.SoDangKy)).Count();
                var countHVST = dataHienVat.Where(c => string.IsNullOrEmpty(c.SoDangKy)).Count();
                var countHVXD = dataHienVat.Where(c => !string.IsNullOrEmpty(c.SoDangKy) && c.IsDuyet == true).Count();
                var countHVCXD = dataHienVat.Where(c => !string.IsNullOrEmpty(c.SoDangKy) && c.IsDuyet != true).Count();
                var countHVDiVat = dataHienVat.Where(c => c.IsDiVat == true).Count();
                var countHVCoVat = dataHienVat.Where(c => c.IsCoVat == true).Count();
                var countHVKhangChien = dataHienVat.Where(c => c.IsHienVatKhangChien == true).Count();
                var countHVBaoVatQG = dataHienVat.Where(c => c.IsBaoVatQG == true).Count();

                return Json(new
                {
                    countTHV,
                    countHVCSDK,
                    countHVST,
                    countHVXD,
                    countHVCXD,
                    countHVDiVat,
                    countHVCoVat,
                    countHVKhangChien,
                    countHVBaoVatQG,
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