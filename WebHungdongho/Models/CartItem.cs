using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHungdongho.Models
{
    public class CartItem
    {
        public int id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Img { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}