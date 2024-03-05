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
    public class DonHangsController : Controller
    {
        private HungdonghoEntities db = new HungdonghoEntities();

        // GET: DonHangs
        public ActionResult Index()
        {
            var donHangs = db.DonHangs.Include(d => d.User).OrderByDescending(d=>d.NgayDatHang);
            return View(donHangs.ToList());
        }
        [HttpPost]
        public ActionResult Index(string key)
        {
            var tk = db.DonHangs.Where(x => x.NgayDatHang.ToString().Contains(key)
            || x.TongTien.ToString().Contains(key)
            || x.HoTenDem.Contains(key) || x.Ten.Contains(key)||
            x.Email.Contains(key) || x.SoDienThoai.Contains(key)
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
        public ActionResult DonHangkhachhang()
        {
            if (Session["maUser"] == null)
            {
                return RedirectToAction("Dangnhap", "Index");
            }

            int maUser = (int)Session["maUser"];

            var donHangs = db.DonHangs
         .Where(d => d.MaUser == maUser)
         .Include(d => d.User)
         .OrderByDescending(d => d.NgayDatHang) 
         .ToList();

            return View(donHangs);
        }
        [HttpPost]
        public ActionResult DonHangkhachhang(string key)
        {
            int maUserValue = (int)(Session["maUser"] ?? 0);
            var tk = db.DonHangs
            .Where(x => (x.NgayDatHang.ToString().Contains(key)
                        || x.TongTien.ToString().Contains(key)
                        || x.HoTenDem.Contains(key)
                        || x.Ten.Contains(key)
                        || x.Email.Contains(key)
                        || x.SoDienThoai.Contains(key))
                        && x.MaUser == maUserValue
            )
            .ToList();
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
        public ActionResult ChiTietDonHang(int maDonHang)
        {
            var donHang = db.DonHangs.Find(maDonHang);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            var chiTietDonHang = db.ChiTietDonHangs.Where(ct => ct.MaDonHang == maDonHang).ToList();

            ViewBag.ChiTietDonHang = chiTietDonHang;

            return View(chiTietDonHang);
        }
        // GET: DonHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // GET: DonHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaUser = new SelectList(db.Users, "MaUser", "HoTenDem");
            return View();
        }

        // POST: DonHangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDonHang,NgayDatHang,TongTien,MaUser,HoTenDem,Ten,Email,SoDienThoai")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.DonHangs.Add(donHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaUser = new SelectList(db.Users, "MaUser", "HoTenDem", donHang.MaUser);
            return View(donHang);
        }

        // GET: DonHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaUser = new SelectList(db.Users, "MaUser", "HoTenDem", donHang.MaUser);
            return View(donHang);
        }

        // POST: DonHangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDonHang,NgayDatHang,TongTien,MaUser,HoTenDem,Ten,Email,SoDienThoai")] DonHang donHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaUser = new SelectList(db.Users, "MaUser", "HoTenDem", donHang.MaUser);
            return View(donHang);
        }

        // GET: DonHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonHang donHang = db.DonHangs.Find(id);
            if (donHang == null)
            {
                return HttpNotFound();
            }
            return View(donHang);
        }

        // POST: DonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DonHang donHang = db.DonHangs.Find(id);
            db.DonHangs.Remove(donHang);
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
