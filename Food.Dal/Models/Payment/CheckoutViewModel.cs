using Food.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Food.Dal.Models.Payment
{
    public class CheckoutViewModel
    {
        public CheckoutModel Checkouts { get; set; } = new CheckoutModel();
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Cart> CartItemList { get; set; } = new List<Cart>();

        public decimal Subtotal => Products.Sum(p => p.Price);
        public decimal Shipping => Products.Any() ? 10 : 0;
        public decimal Total => Subtotal + Shipping;
    }
}
