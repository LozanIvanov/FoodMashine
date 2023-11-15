using Microsoft.AspNetCore.Identity;
using Food.Database.Models;


namespace Food.Database
{
    public class DBInitializer
    {
        public static void Initialize(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext dbContext)
        {
            var users = userManager.Users.ToList();

            if (users.Count == 0)
            {
                #region Create Users
                // Create Users in the DB
                var password = "123";
                var adminUser = new User()
                {
                    UserName = "Admin",
                    Email = "admin@test.com"
                };
                userManager.CreateAsync(adminUser, password).Wait();

                var user = new User()
                {
                    UserName = "User",
                    Email = "user@test.com"
                };
                userManager.CreateAsync(user, password).Wait();

               
              

                // Add two roles in the DB
                roleManager.CreateAsync(new IdentityRole() { Name = "Admin" }).Wait();
                roleManager.CreateAsync(new IdentityRole() { Name = "User" }).Wait();
             

                // Assign roles to the users
                userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                userManager.AddToRoleAsync(user, "User").Wait();
             
                #endregion



                #region Create Products
                dbContext.Products.Add(new Product() { Name = "Addidas Shoes", Discription = "Brand new model", Price = 120, Quantity = 17, MainImage = "6db95382-ff8f-455a-af11-be9969bc4385-obuvki.webp" });
                dbContext.Products.Add(new Product() { Name = "Addidas Shirt", Discription = "Brand new model", Price = 40, Quantity = 28, MainImage = "product-8.jpg" });
                dbContext.Products.Add(new Product() { Name = "Nike Shirt", Discription = "Brand new model", Price = 50, Quantity = 15, MainImage = "product-4.jpg" });
                dbContext.Products.Add(new Product() { Name = "Addidas Jacket", Discription = "Brand new model", Price = 250, Quantity = 10, MainImage = "product-3.jpg" });
                dbContext.Products.Add(new Product() { Name = "Puma Jacket", Discription = "Brand new model", Price = 190, Quantity = 18, MainImage = "product-6.jpg" });
                dbContext.SaveChanges();
                #endregion


            }
        }
     }
}
