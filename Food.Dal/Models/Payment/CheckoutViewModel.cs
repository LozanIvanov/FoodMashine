using Food.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Dal.Models.Payment
{
    public class CheckoutViewModel
    {
        public CheckoutModel Checkouts { get; set; }
        
        public List<Cart> CartItemList { get; set; }
        public List<Product> Products { get; set; }
    }
}
