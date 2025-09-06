using Food.Dal.Services;
using Food.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
public class CheckoutService : BaseService
{
    public CheckoutService(IConfiguration config) : base(config) { }

    public void AddCheckout(Checkout checkout)  
    {
        dbContext.Checkouts.Add(checkout);
        dbContext.SaveChanges();
    }
}