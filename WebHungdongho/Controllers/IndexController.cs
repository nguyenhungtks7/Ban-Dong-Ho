using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebHungdongho.Models;

namespace WebHungdongho.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        private HungdonghoEntities db = new HungdonghoEntities();

        // GET: SanPhams
        public ActionResult Index()
        {
            var top12Products = db.SanPhams.OrderByDescending(p => p.Gia).Take(16).ToList();
            return View(top12Products);
        }
        public ActionResult danhmuc(string Danhmuc)
        {
            var danhMuc = db.DanhMucs.FirstOrDefault(dm => dm.TenDanhMuc == Danhmuc);

            if (danhMuc != null)
            {
               
                var sanPhamTrongDanhMuc = danhMuc.SanPhams.ToList();

              
                return View(sanPhamTrongDanhMuc);
            }
            else
            {
              
                return View(new List<SanPham>());
            }
        }
        public ActionResult danhmuc2(string Danhmuc, string NhanHieu)
        {
            var danhMuc = db.DanhMucs.FirstOrDefault(dm => dm.TenDanhMuc == Danhmuc);

            if (danhMuc != null)
            {
                var sanPhamTrongDanhMuc = danhMuc.SanPhams.Where(sp => sp.NhanHieu == NhanHieu).ToList();
                return View(sanPhamTrongDanhMuc);
            }
            else
            {
                return View(new List<SanPham>());
            }
        }
        public static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public ActionResult Dangnhap()
        {    
            
            if (TempData["DangKySuccess"] != null)
            {
                ViewBag.DangKySuccessMessage = TempData["DangKySuccess"].ToString();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dangnhap(LoginModel model)
        {
      

            if (ModelState.IsValid)
            {
               
                string hashedPassword = HashPassword(model.Password);

                var user = db.Users.SingleOrDefault(u => u.TenDangNhap == model.UserName && u.MatKhau == hashedPassword);

                if (user != null)
                {
                    if (user.mod == 1)
                    {
                        Session["tenUser"] = user.Ten;
                        Session["maUser"] = user.MaUser;
                        Session["tentaikhoan"] = user.TenDangNhap;
                        return RedirectToAction("Index", "Home");
                    }
                    else if (user.mod == 2)
                    {
                        Session["tenUser"] = user.Ten;
                        Session["maUser"] = user.MaUser;
                        Session["tentaikhoan"] = user.TenDangNhap;
                        return RedirectToAction("Index", "Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không chính xác.");
                    return View(model);
                }
            }

            return View(model);
        }
        public ActionResult Dangxuat()
        {
            Session.Clear(); 
            Session.Abandon();



            return RedirectToAction("Index", "Index");
        }
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DangKy([Bind(Include = "MaUser,HoTenDem,Ten,TenDangNhap,MatKhau,Email,SoDienThoai,mod")] User user, string NhapLaiMatKhau)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Users.FirstOrDefault(u => u.TenDangNhap == user.TenDangNhap);
                if (existingUser != null)
                {
                    ModelState.AddModelError("TenDangNhap", "Tài khoản đã tồn tại. Vui lòng chọn tên đăng nhập khác.");
                    return View(user);
                }

              
                if (user.MatKhau != NhapLaiMatKhau)
                {
                    ModelState.AddModelError("MatKhau", "Mật khẩu nhập lại không khớp");
                    return View(user);
                }

                // Hash mật khẩu và thêm người dùng mới
                user.MatKhau = HashPassword(user.MatKhau);
                user.mod = 2;
                db.Users.Add(user);
                db.SaveChanges();

                TempData["DangKySuccess"] = "Đăng ký thành công. Vui lòng đăng nhập.";
                return RedirectToAction("Dangnhap", "Index", new { DangKySuccess = "true" });
            }

            return View(user);
        }

        public ActionResult Doimatkhau()
        {
            if (Session["tentaikhoan"] == null)
            {
                return RedirectToAction("Dangnhap", "Index");
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Doimatkhau(Updatepassword model)
        {
            if (ModelState.IsValid)
            {
                string tendn = (string)Session["tentaikhoan"];
                var existingUser = db.Users.FirstOrDefault(u => u.TenDangNhap == tendn);

                if (existingUser != null && existingUser.MatKhau == HashPassword(model.CurrentPassword))
                {
                    if (model.NewPassword != model.ConfirmPassword)
                    {
                        ModelState.AddModelError("", "Mật khẩu mới và xác nhận mật khẩu mới không khớp.");
                        return View(model);
                    }

                    existingUser.MatKhau = HashPassword(model.NewPassword);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu hiện tại không đúng.");
                    return View(model);
                }
            }

            return View(model);
        }
        public ActionResult DoimatkhauUser()
        {
            if (Session["tentaikhoan"] == null)
            {
                return RedirectToAction("Dangnhap", "Index");
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoimatkhauUser(Updatepassword model)
        {
            if (ModelState.IsValid)
            {
                string tendn = (string)Session["tentaikhoan"];
                var existingUser = db.Users.FirstOrDefault(u => u.TenDangNhap == tendn);

                if (existingUser != null && existingUser.MatKhau == HashPassword(model.CurrentPassword))
                {
                    if (model.NewPassword != model.ConfirmPassword)
                    {
                        ModelState.AddModelError("", "Mật khẩu mới và xác nhận mật khẩu mới không khớp.");
                        return View(model);
                    }

                    existingUser.MatKhau = HashPassword(model.NewPassword);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Mật khẩu hiện tại không đúng.");
                    return View(model);
                }
            }

            return View(model);
        }



    }
}