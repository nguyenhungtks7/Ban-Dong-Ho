using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebHungdongho.Models;

namespace WebHungdongho.Controllers
{
    public class PhanHoisController : Controller
    {
        private HungdonghoEntities db = new HungdonghoEntities();

        // GET: PhanHois
        public ActionResult Index()
        {
            var phanHois = db.PhanHois.Include(p => p.User);
            return View(phanHois.ToList());
        }
        [HttpPost]
        public ActionResult Index(string key)
        {
            var tk = db.PhanHois.Where(x => x.NoiDung.Contains(key) || x.NgayPhanHoi.ToString().Contains(key)
            || x.User.HoTenDem.Contains(key) || x.User.Ten.Contains(key) 
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


        // GET: PhanHois/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanHoi phanHoi = db.PhanHois.Find(id);
            if (phanHoi == null)
            {
                return HttpNotFound();
            }
            return View(phanHoi);
        }

        // GET: PhanHois/Create
        public ActionResult Create()
        {
            ViewBag.MaUser = new SelectList(db.Users, "MaUser", "HoTenDem");
            return View();
        }

        // POST: PhanHois/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaPhanHoi,MaUser,NoiDung,NgayPhanHoi")] PhanHoi phanHoi)
        {
            if (ModelState.IsValid)
            {
                db.PhanHois.Add(phanHoi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaUser = new SelectList(db.Users, "MaUser", "HoTenDem", phanHoi.MaUser);
            return View(phanHoi);
        }

        // GET: PhanHois/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanHoi phanHoi = db.PhanHois.Find(id);
            if (phanHoi == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaUser = new SelectList(db.Users, "MaUser", "HoTenDem", phanHoi.MaUser);
            return View(phanHoi);
        }

        // POST: PhanHois/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaPhanHoi,MaUser,NoiDung,NgayPhanHoi")] PhanHoi phanHoi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phanHoi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaUser = new SelectList(db.Users, "MaUser", "HoTenDem", phanHoi.MaUser);
            return View(phanHoi);
        }

        // GET: PhanHois/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanHoi phanHoi = db.PhanHois.Find(id);
            if (phanHoi == null)
            {
                return HttpNotFound();
            }
            return View(phanHoi);
        }

        // POST: PhanHois/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhanHoi phanHoi = db.PhanHois.Find(id);
            db.PhanHois.Remove(phanHoi);
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
