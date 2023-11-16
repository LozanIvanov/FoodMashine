using Food.Database.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Database
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Cart> CartItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Checkout> Checkouts {get;set;}
        public DbSet<Reservation> Reservations { get; set; }    
        public ApplicationDbContext(DbContextOptions options)
          : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
          
        }
    }
}
