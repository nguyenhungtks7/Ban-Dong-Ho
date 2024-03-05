using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebHungdongho.Models;

namespace WebHungdongho.Controllers
{
    public class ChitietsanphamController : Controller
    {
        // GET: Chitietsanpham
        private HungdonghoEntities db = new HungdonghoEntities();
        public ActionResult Index(int id)
        {
            var sanpham = db.SanPhams.Find(id);
            return View(sanpham);
        }
      
        public ActionResult AddToCart(int id)
        {
            Console.WriteLine("AddToCart action reached!");
            var sanpham = db.SanPhams.Find(id);

            if (sanpham != null)
            {
                AddProductToCart(sanpham);
            }

         
            return RedirectToAction("Index", "GioHang");
        }

        private void AddProductToCart(SanPham sanpham)
        {
            var cart = Session["cart"] as List<CartItem> ?? new List<CartItem>();

            var existingProduct = cart.Find(item => item.Name == sanpham.TenSP);
            
            if (existingProduct != null)
            {
                existingProduct.Quantity += 1;
            }
            else
            {
                var product = new CartItem
                {
                    id = sanpham.MaSP,
                    Name = sanpham.TenSP,
                    Price = sanpham.Gia ?? 0,
                    Img = sanpham.DuongDanHinh,
                    Quantity = 1
                };
                cart.Add(product);
                
            }

            Session["cart"] = cart;
        }
      
    }
}