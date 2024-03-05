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
    public class ChiTietDonHangs1Controller : Controller
    {
        private HungdonghoEntities db = new HungdonghoEntities();

        // GET: ChiTietDonHangs1
        public ActionResult Index(int maDonHang)
        {
            //var chiTietDonHangs = db.ChiTietDonHangs.Include(c => c.DonHang).Include(c => c.SanPham);
            //return View(chiTietDonHangs.ToList());
            var donHang = db.DonHangs.Find(maDonHang);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            var chiTietDonHang = db.ChiTietDonHangs.Where(ct => ct.MaDonHang == maDonHang).ToList();

            ViewBag.ChiTietDonHang = chiTietDonHang;

            return View(chiTietDonHang);
        }

        // GET: ChiTietDonHangs1/Details/5
        public ActionResult Details(int? id, int? id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id, id2);
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDonHang);
        }

        // GET: ChiTietDonHangs1/Create
        public ActionResult Create()
        {
            ViewBag.MaDonHang = new SelectList(db.DonHangs, "MaDonHang", "HoTenDem");
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP");
            return View();
        }

        // POST: ChiTietDonHangs1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDonHang,MaSP,SoLuong,GiaSP")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietDonHangs.Add(chiTietDonHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDonHang = new SelectList(db.DonHangs, "MaDonHang", "HoTenDem", chiTietDonHang.MaDonHang);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietDonHang.MaSP);
            return View(chiTietDonHang);
        }

        // GET: ChiTietDonHangs1/Edit/5
        public ActionResult Edit(int? id, int? id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id,id2);
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDonHang = new SelectList(db.DonHangs, "MaDonHang", "HoTenDem", chiTietDonHang.MaDonHang);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietDonHang.MaSP);
            return View(chiTietDonHang);
        }

        // POST: ChiTietDonHangs1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDonHang,MaSP,SoLuong,GiaSP")] ChiTietDonHang chiTietDonHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDonHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDonHang = new SelectList(db.DonHangs, "MaDonHang", "HoTenDem", chiTietDonHang.MaDonHang);
            ViewBag.MaSP = new SelectList(db.SanPhams, "MaSP", "TenSP", chiTietDonHang.MaSP);
            return View(chiTietDonHang);
        }

        // GET: ChiTietDonHangs1/Delete/5
        public ActionResult Delete(int? id, int? id2)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id, id2);
            if (chiTietDonHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDonHang);
        }

        // POST: ChiTietDonHangs1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, int id2)
        {
            ChiTietDonHang chiTietDonHang = db.ChiTietDonHangs.Find(id, id2);
            db.ChiTietDonHangs.Remove(chiTietDonHang);
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
