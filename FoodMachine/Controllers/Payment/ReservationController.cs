using Food.Dal.Services;
using Food.Database;
using Food.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodMachine.Controllers.Payment
{
    public class ReservationController : Controller
    {
        private readonly CartService cartService;
        private readonly ReservationService reservationService;
        private readonly ApplicationDbContext context;
        int co = 10;

        public ReservationController(CartService cartService, ApplicationDbContext context, ReservationService reservationService)
        {
            this.cartService = cartService;
            this.context = context;
            this.reservationService = reservationService;
        }
        [HttpGet]
        [Route("/Reservations/Checkout")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("/Payment/Reservation/Create")]
        public IActionResult Create()
        {

            var c = cartService.GetAll();
            int cod = 10;
            cod++;
            DateTime startTime = DateTime.Now;
            DateTime duration = startTime.AddMinutes(30);
            long unix = ((DateTimeOffset)duration).ToUnixTimeSeconds();
            foreach (var item in c)
            {
                Reservation r = new Reservation();
                var product = context.Products.Where(p => p.Id == item.ProductId).FirstOrDefault();

                r.Name = product.Name;
                r.Discription = product.Discription;
                r.Price = product.Price;
                r.MainImage = product.MainImage;
                r.Code = cod;

                r.DateTime = unix;
                reservationService.AddProduct(r);
                cartService.Delete(item.ProductId);

            }
            reservationService.DeleteDate();



            return Redirect("/");
        }
      
        public IActionResult Time()
        {
            var bas = reservationService.GetAll().ToList();

            foreach (var item in bas)
            {
                while (DateTimeOffset.UtcNow.ToUnixTimeSeconds() < item.DateTime)
                {
                    // Simulate the passage of time (you can replace this with actual elapsed time logic)
                    // For the sake of this example, we use a short delay.
                    System.Threading.Thread.Sleep(1000); // 1 second delay
                    reservationService.Delete(item.Id);
                }
            }
            return View();

             
        }


    }
}
