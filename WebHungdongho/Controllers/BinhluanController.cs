using Antlr.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebHungdongho.Models;

namespace WebHungdongho.Controllers
{
    public class BinhluanController : Controller
    {
        private HungdonghoEntities db = new HungdonghoEntities();
        public ActionResult Index()
        {
            var phanHoiList = db.PhanHois.ToList();
            return View(phanHoiList);
        }
        public ActionResult Binhluan()
        {
            var phanHoiList = db.PhanHois.ToList();
            return View(phanHoiList);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(string NoiDung)
        {
            if (Session["maUser"] == null)
            {
                return RedirectToAction("Dangnhap", "Index");
            }
            if (NoiDung == "")
            {
                ViewBag.checkr = "Bạn chưa nhập nội dung";
                return RedirectToAction("Binhluan");
            }
            PhanHoi phanHoi = new PhanHoi()
            {
                NoiDung = NoiDung,
                MaUser =(int) Session["maUser"],
                NgayPhanHoi = DateTime.Now,
            };
            if (ModelState.IsValid)
            {

                db.PhanHois.Add(phanHoi);
                db.SaveChanges();


                return RedirectToAction("Binhluan");
            }

            return View("Index", db.PhanHois.ToList());

        }
    }
}