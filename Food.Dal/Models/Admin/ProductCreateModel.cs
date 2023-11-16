using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Food.Dal.Models.Admin
{
    public class ProductCreateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(15,2)")]
        public decimal Price { get; set; }
        public IFormFile? MainImage { get; set; }
        public string? MainImages { get; set; }
        public string? GalleryImage { get; set; }
        public decimal? ChippingPrice { get; set; }
      
        public int Code { get; set; }
        public long TimeUnix { get; set; }

        public decimal Total { get; set; }
        public decimal ShippingPrice { get; set; }
    }
}
