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
    public class ProductService : BaseService
    {
        private int perPage = 9;
        public ProductService(IConfiguration configuration) : base(configuration)
        {
        }
        public List<Product> GetProducts(int? pageNullable)
        {
            int page = pageNullable != null ? pageNullable.Value : 1;
            return this.dbContext.Products.Skip((page-1)*this.perPage).Take(this.perPage).ToList();

   
        }
        public int GetTotalPages()
        {
            double count = this.dbContext.Products.Count();
            return (int)Math.Ceiling(count / this.perPage);
        }

        public Product GetProductById(int id)
        {
            return this.dbContext.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public void AddProduct(Product product)
        {
            this.dbContext.Products.Add(product);
            this.dbContext.SaveChanges();
        }

        public void UpdateProduct(int id, Product product)
        {
            var currentProduct = this.dbContext.Products.Where(p => p.Id == id).FirstOrDefault();

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
