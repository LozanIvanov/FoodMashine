using Food.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Food.Dal.Services
{
    public class ProductService : BaseService
    {
        private readonly int perPage = 9;

        public ProductService(IConfiguration configuration) : base(configuration) { }

        // Get products with pagination, filtering by category, price, search
        public List<Product> GetProducts(
            int? pageNullable,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string? search = null,
            List<string>? categories = null)
        {
            int page = pageNullable ?? 1;
            var query = dbContext.Products.AsQueryable();

            // Filter by price
            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);
            if (maxPrice.HasValue && maxPrice.Value > 0)
                query = query.Where(p => p.Price <= maxPrice.Value);

            // Filter by search
            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.Name.Contains(search));

            // Filter by selected categories
            if (categories != null && categories.Count > 0 && !categories.Contains("All"))
                query = query.Where(p => !string.IsNullOrEmpty(p.Category) && categories.Contains(p.Category));

            // Pagination
            return query
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ToList();
        }

        // Get total pages for pagination
        public int GetTotalPages(
            decimal? minPrice = null,
            decimal? maxPrice = null,
            string? search = null,
            List<string>? categories = null)
        {
            var query = dbContext.Products.AsQueryable();

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);
            if (maxPrice.HasValue && maxPrice.Value > 0)
                query = query.Where(p => p.Price <= maxPrice.Value);
            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.Name.Contains(search));
            if (categories != null && categories.Count > 0 && !categories.Contains("All"))
                query = query.Where(p => !string.IsNullOrEmpty(p.Category) && categories.Contains(p.Category));

            double count = query.Count();
            return (int)Math.Ceiling(count / perPage);
        }

        // Get all categories with product counts
        public Dictionary<string, int> GetCategoriesWithCounts()
        {
            return dbContext.Products
                .Where(p => !string.IsNullOrEmpty(p.Category)) // ignore null or empty
                .GroupBy(p => p.Category)
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .ToDictionary(x => x.Category, x => x.Count);
        }

        // Optional: get distinct categories only
        public List<string> GetCategoriesFromProducts()
        {
            return dbContext.Products
                .Where(p => !string.IsNullOrEmpty(p.Category))
                .Select(p => p.Category)
                .Distinct()
                .OrderBy(c => c)
                .ToList();
        }

        // Other existing methods (AddProduct, UpdateProduct, Delete, GetProductById, etc.)
        public Product GetProductById(int id)
        {
            return dbContext.Products.FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
        }

        public void UpdateProduct(int id, Product product)
        {
            var currentProduct = dbContext.Products.FirstOrDefault(p => p.Id == id);
            if (currentProduct != null)
            {
                currentProduct.Name = product.Name;
                currentProduct.Discription = product.Discription;
                currentProduct.Price = product.Price;
                currentProduct.Quantity = product.Quantity;
                currentProduct.Category = product.Category;


                if (!string.IsNullOrEmpty(product.MainImage))
                    currentProduct.MainImage = product.MainImage;

                dbContext.Entry(currentProduct).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var product = GetProductById(id);
            if (product != null)
            {
                dbContext.Entry(product).State = EntityState.Deleted;
                dbContext.SaveChanges();
            }
        }

        public List<Product> GetRelatedProducts(int currentProductId, int count = 6)
        {
            return dbContext.Products
                .Where(p => p.Id != currentProductId)
                .OrderBy(p => Guid.NewGuid())
                .Take(count)
                .ToList();
        }
    }
}

