using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebHungdongho.Models;

namespace WebHungdongho.Controllers
{
    public class LienHesController : Controller
    {
        private HungdonghoEntities db = new HungdonghoEntities();

        // GET: LienHes
        public ActionResult Index()
        {
            return View(db.LienHes.ToList());
        }
        [HttpPost]
        public ActionResult Index(string key)
        {
            var tk = db.LienHes.Where(x => x.NoiDung.Contains(key) || x.Email.ToString().Contains(key)
            || x.NoiDung.Contains(key) || x.NgayLienHe.ToString().Contains(key)
            ).ToList();
            if (key == "")
            {
                ViewBag.cr = "Bạn chưa nhập thông tin cần tìm kiếm";
            }
            if (tk.Count <= 0)
            {
                ViewBag.check = "Không có thông tin nào liên quan  \"" + key + "\"";
            }
            return View(tk);
        }

        public ActionResult SendMessage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMessage([Bind(Include = "MaLienHe,HoTen,Email,NoiDung,NgayLienHe")] LienHe lienHe)
        {
            if (ModelState.IsValid)
            {
                db.LienHes.Add(lienHe);
                db.SaveChanges();
                ViewBag.SuccessMessage = "Đã gửi thông điệp thành công!";
                return View();
            }

            return View(lienHe);
        }
            // GET: LienHes/Details/5
            public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LienHe lienHe = db.LienHes.Find(id);
            if (lienHe == null)
            {
                return HttpNotFound();
            }
            return View(lienHe);
        }

        // GET: LienHes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LienHes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLienHe,HoTen,Email,NoiDung,NgayLienHe")] LienHe lienHe)
        {
            if (ModelState.IsValid)
            {
                db.LienHes.Add(lienHe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lienHe);
        }

        // GET: LienHes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LienHe lienHe = db.LienHes.Find(id);
            if (lienHe == null)
            {
                return HttpNotFound();
            }
            return View(lienHe);
        }

        // POST: LienHes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLienHe,HoTen,Email,NoiDung,NgayLienHe")] LienHe lienHe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lienHe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lienHe);
        }

        // GET: LienHes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LienHe lienHe = db.LienHes.Find(id);
            if (lienHe == null)
            {
                return HttpNotFound();
            }
            return View(lienHe);
        }

        // POST: LienHes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LienHe lienHe = db.LienHes.Find(id);
            db.LienHes.Remove(lienHe);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
