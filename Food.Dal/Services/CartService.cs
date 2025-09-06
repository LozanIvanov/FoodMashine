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
        public List<Product> GetProducts()
        {
            // Join CartItems with Products to get full Product info
            return dbContext.CartItems
                .Include(c => c.Product) // include related Product
                .Select(c => c.Product)
                .ToList();
        }
        public void AddProduct(Cart product)
        {
            
            var existing = dbContext.CartItems.FirstOrDefault(c => c.ProductId == product.ProductId);
            if (existing != null)
            {
                existing.Quantity += product.Quantity; // increase quantity
                dbContext.CartItems.Update(existing);
            }
            else
            {
                dbContext.CartItems.Add(product); // add new if not exists
            }

            dbContext.SaveChanges();

        }



        public void Delete(int id)
        {
            var cat = this.dbContext.CartItems.Where(p => p.ProductId == id)
                .FirstOrDefault();
            dbContext.Entry(cat).State = EntityState.Deleted;
            dbContext.SaveChanges();
        }
        public int GetCartCount()
        {
            return dbContext.CartItems.Sum(c => c.Quantity);
        }
        public void ClearCart()
        {
            var allItems = dbContext.CartItems.ToList();
            dbContext.CartItems.RemoveRange(allItems);
            dbContext.SaveChanges();
        }

    }
}

