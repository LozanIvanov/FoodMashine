using Food.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Dal.Services
{
    public class CheckoutService:BaseService
    {
        public CheckoutService(IConfiguration configuration) : base(configuration) { }

        public List<Checkout> GetProducts()
        {
            var query = this.dbContext.Checkouts.Include(p => p.Country)
                .Include(p => p.CartItem).ToList();

            return query;


        }


        public void AddCheckout(Checkout product)
        {
            this.dbContext.Checkouts.Add(product);
            this.dbContext.SaveChanges();
        }
        public Product GetProductById(int id)
        {
            return this.dbContext.Products.Where(p => p.Id == id)
                
                .FirstOrDefault();
        }


        public void UpdateProduct(int id, Product product)
        {
            var currentProduct = this.dbContext.Products.Where(p => p.Id == id)
                
                .FirstOrDefault();

            if (currentProduct != null)
            {
                currentProduct.Name = product.Name;
                currentProduct.Discription = product.Discription;
                currentProduct.Price = product.Price;
                currentProduct.Quantity = product.Quantity;
               

                if (!string.IsNullOrEmpty(product.MainImage))
                {
                    currentProduct.MainImage = product.MainImage;
                }

                dbContext.Entry(currentProduct).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }
        public void Delete(int id)
        {
            var cat = GetProductById(id);
            dbContext.Entry(cat).State = EntityState.Deleted;
            dbContext.SaveChanges();
        }
    }
}
