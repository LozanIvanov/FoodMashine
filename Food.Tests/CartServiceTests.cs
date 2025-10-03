using System;
using System.Linq;
using Food.Dal.Services;
using Food.Database;
using Food.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Xunit;
using FluentAssertions;

namespace Food.Tests
{
    public class CartServiceTests
    {
       
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) 
                .Options;

            return new ApplicationDbContext(options);
        }


        private CartService GetCartService(out ApplicationDbContext context)
        {
            context = GetDbContext();
            var service = new CartService(context);  
            return service;
        }


        [Fact]
        public void AddProduct_Should_Add_New_Item()
        {
            var service = GetCartService(out var context);
            var cartItem = new Cart { ProductId = 1, Quantity = 2 };

            service.AddProduct(cartItem);

            context.CartItems.Count().Should().Be(1);
            context.CartItems.First().Quantity.Should().Be(2);
        }

        [Fact]
        public void AddProduct_Should_Increase_Quantity_If_Exists()
        {
            var service = GetCartService(out var context);
            context.CartItems.Add(new Cart { ProductId = 1, Quantity = 2 });
            context.SaveChanges();

            var cartItem = new Cart { ProductId = 1, Quantity = 3 };
            service.AddProduct(cartItem);

            context.CartItems.Count().Should().Be(1);
            context.CartItems.First().Quantity.Should().Be(5);
        }

        [Fact]
        public void Delete_Should_Remove_Item()
        {
            var service = GetCartService(out var context);
            context.CartItems.Add(new Cart { ProductId = 1, Quantity = 1 });
            context.SaveChanges();

            service.Delete(1);

            context.CartItems.Should().BeEmpty();
        }

        [Fact]
        public void GetCartCount_Should_Return_Total_Quantity()
        {
            var service = GetCartService(out var context);
            context.CartItems.Add(new Cart { ProductId = 1, Quantity = 2 });
            context.CartItems.Add(new Cart { ProductId = 2, Quantity = 3 });
            context.SaveChanges();

            var total = service.GetCartCount();

            total.Should().Be(5);
        }

        [Fact]
        public void ClearCart_Should_Remove_All_Items()
        {
            var service = GetCartService(out var context);
            context.CartItems.Add(new Cart { ProductId = 1, Quantity = 2 });
            context.CartItems.Add(new Cart { ProductId = 2, Quantity = 3 });
            context.SaveChanges();

            service.ClearCart();

            context.CartItems.Should().BeEmpty();
        }
    }
}

