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
    public class CartService:BaseService
    {
        public CartService(IConfiguration configuration) : base(configuration) { }

        public List<Cart> GetAll()
        {


            return this.dbContext.CartItems.ToList();

        }
        public Cart GetProductById(int id)
        {
            return this.dbContext.CartItems.Where(p => p.ProductId == id)
                .FirstOrDefault();
        }
        public void AddProduct(Cart product)
        {
            
            dbContext.CartItems.Add(product);
            dbContext.SaveChanges();

        }



        public void Delete(int id)
        {
            var cat = this.dbContext.CartItems.Where(p => p.ProductId == id)
                .FirstOrDefault();
            dbContext.Entry(cat).State = EntityState.Deleted;
            dbContext.SaveChanges();
        }
    }
}

