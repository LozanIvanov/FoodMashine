using Food.Database.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Dal.Models.Admin
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Quantity { get; set; }


        public decimal Price { get; set; }
        public string? MainImage { get; set; }
        public string? GalleryImage { get; set; }

        public IFormFile MainImageFile { get; set; }
       
        public List<Product> ListProducts { get; set; }

        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public decimal ShippingPrice { get; set; }

        public ProductViewModel()
        {
           
            ListProducts = new List<Product>();
        }
    }
}
