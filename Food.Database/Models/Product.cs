using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Database.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Discription { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(15,2)")]
        public decimal Price { get; set; }
        public string? MainImage { get; set; }
        
        public DateTime? Order { get; set; }
     
    }
}
