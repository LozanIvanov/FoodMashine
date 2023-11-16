using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Database.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MainImage { get; set; }
        public string Discription  { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public long? DateTime { get; set; }
        public int? Code { get; set; }
    }
}
