using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebHungdongho.Models;

namespace WebHungdongho.Controllers
{
    public class GioHangController : Controller
    {
        private HungdonghoEntities db = new HungdonghoEntities();
        public ActionResult Index()
        {
            var cart = (List<CartItem>)Session["cart"];
            if (cart != null)
            {

                return View(cart.ToList());
            }
            return View();
        }
        [HttpPost]
        public ActionResult UpdateCart(int itemId, int quantity)
        {
            var cart = (List<CartItem>)Session["cart"];
            if (cart != null)
            {
               
                var itemToUpdate = cart.FirstOrDefault(item => item.id == itemId);
                if (itemToUpdate != null)
                {
                    itemToUpdate.Quantity = quantity;
                    Session["cart"] = cart;
                }
            }

          
            return RedirectToAction("Index");
        }



        public ActionResult RemoveItem(int itemId)
        {
            var cart = (List<CartItem>)Session["cart"];
            if (cart != null)
            {
                var itemToRemove = cart.FirstOrDefault(item => item.id == itemId);
                if (itemToRemove != null)
                {
                    cart.Remove(itemToRemove);
                    Session["cart"] = cart;
                }
            }

          
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Checkout(string HoTenDem, string Ten, string Email, string SoDienThoai, decimal thanhtien)
        {

            var maUser = Session["maUser"];
    
            if (maUser == null)
            {
                ViewBag.CheckDangNhapMessage = "Mời bạn đăng nhập trước khi thanh toán";
                return RedirectToAction("Dangnhap", "Index");
            }
            if (HoTenDem == "")
            {
                ViewBag.CheckHoTenDem = "Bạn chưa nhập họ tên đệm";
                return RedirectToAction("Index", "GioHang");
            }

            if (Ten == "")
            {
                ViewBag.CheckTen = "Bạn chưa nhập tên";
                return RedirectToAction("Index", "GioHang");
            }

            if (Email == "")
            {
                ViewBag.CheckEmail = "Bạn chưa nhập email";
                return RedirectToAction("Index", "GioHang");
            }
            else if (!IsValidEmail(Email))
            {
                ViewBag.CheckEmail = "Email không đúng định dạng";
                return RedirectToAction("Index", "GioHang");
            }

            if (SoDienThoai == "")
            {
                ViewBag.CheckSoDienThoai = "Bạn chưa nhập số điện thoại";
                return RedirectToAction("Index", "GioHang");
            }
            else if (!IsValidPhoneNumber(SoDienThoai))
            {
                ViewBag.CheckSoDienThoai = "Số điện thoại không đúng định dạng";
                return RedirectToAction("Index", "GioHang");
            }
            var order = new DonHang
            {
                NgayDatHang = DateTime.Now,
                TongTien = thanhtien/1000,
                MaUser = (int)maUser,
                HoTenDem = HoTenDem,
                Ten = Ten,
                Email = Email,
                SoDienThoai = SoDienThoai
            };


            db.DonHangs.Add(order);
            db.SaveChanges();


            var cart = (List<CartItem>)Session["cart"];
            if (cart != null)
            {
                foreach (var item in cart)
                {
                    var orderDetail = new ChiTietDonHang
                    {
                        MaDonHang = order.MaDonHang,
                        MaSP = item.id,
                        SoLuong = item.Quantity,
                        GiaSP = item.Price
                    };


                    db.ChiTietDonHangs.Add(orderDetail);
                    db.SaveChanges();
                }


                Session["cart"] = null;
            }

            return RedirectToAction("DonHangkhachhang", "DonHangs");
        }
        private bool IsValidEmail(string email)
        {
            string emailPattern = @"^\S+@\S+\.\S+$";
            return Regex.IsMatch(email, emailPattern);
        }
        private bool IsValidPhoneNumber(string phoneNumber)
        {
            
            string phonePattern = @"^\d{10,}$";
            return Regex.IsMatch(phoneNumber, phonePattern);
        }
    }
}