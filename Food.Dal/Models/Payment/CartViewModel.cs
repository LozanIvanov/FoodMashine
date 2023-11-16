using Food.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Dal.Models.Payment
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public long? DateTime { get; set; }
        public int? Code { get; set; }
    }
}
