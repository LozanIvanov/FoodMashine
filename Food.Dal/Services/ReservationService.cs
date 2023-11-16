using Food.Database.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Food.Dal.Services
{
    public class ReservationService:BaseService
    {
        public ReservationService(IConfiguration configuration) : base(configuration)
        {
        }

        public List<Reservation> GetAll()
        {


            return this.dbContext.Reservations.ToList();

        }
        public Reservation GetProductById(int id)
        {
            return this.dbContext.Reservations.Where(p => p.Id == id).FirstOrDefault();
        }
        public void AddProduct(Reservation product)
        {

            dbContext.Reservations.Add(product);
            dbContext.SaveChanges();

        }
        public void Check(Reservation code)
        {
            List<Reservation> product = GetAll();
            foreach (var item in product)
            {
                var endTime = item.DateTime;
                var start = DateTime.Now;
                long currentTime = ((DateTimeOffset)start).ToUnixTimeSeconds();
                while (currentTime < endTime)
                {
                    // Simulate the passage of time (you can replace this with actual elapsed time logic)
                    //For the sake of this example, we use a short delay.
                    System.Threading.Thread.Sleep(1000); // 1 second delay
                }
                Delete(item.Id);               
            };                       
        }
        public void Delete(int id)
        {
            var cat = this.dbContext.Reservations.Where(p => p.Id == id)
                .FirstOrDefault();
            dbContext.Entry(cat).State = EntityState.Deleted;
            dbContext.SaveChanges();
        }
        public void DeleteDate() {
           
            var now = DateTime.Now ;
            long df= ((DateTimeOffset)now).ToUnixTimeSeconds();
            var allReservationProduct = GetAll();

           // var f = this.dbContext.Reservations.Where(x => x.DateTime < df).ToList(); ;
            foreach (var item in allReservationProduct)
            {
                if (item.DateTime <df)
                {
                    Delete(item.Id);
                    dbContext.SaveChanges();
                }
            }
        }
           
           

    
    }
}

