using Food.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Dal.Models.Admin
{
    public class ProductEditViewModel
    {
        public Product Product { get; set; }
        
      
        public List<ProductCreateModel> Products { get; set; }
        public decimal Shipping { get; set; } // 👈 add this
    }

}
