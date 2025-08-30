using Food.Dal.Models.Admin;
using Food.Dal.Models.Payment;
using Food.Dal.Services;
using Food.Database;
using Food.Database.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace FoodMachine.Controllers.Payment
{
    public class CartController : Controller
    {
        private readonly ProductService productService;
        private readonly CartService cartService;
        private ApplicationDbContext _context;

        public CartController(ProductService productService, ApplicationDbContext db, CartService cartService)
        {
            this.productService = productService;
            this._context = db;
            this.cartService = cartService;


        }

        [HttpGet]
        [Route("/Payment/Cart")]

        public async Task<IActionResult> Index()
        {

            int shipping = 10;

            var cartItems = _context.CartItems.ToList();
            var model = new ProductEditViewModel();
            model.Products = new List<ProductCreateModel>();
            model.Shipping = 10;

            foreach (var cartItem in cartItems)
            {
                var product = _context.Products.FirstOrDefault(x => x.Id == cartItem.ProductId);
                if (product == null) continue;

                ProductCreateModel t = new ProductCreateModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    MainImages = product.MainImage,
                    Price = product.Price,
                    Quantity = cartItem.Quantity, // 👈 use quantity from Cart
                    Total = product.Price * cartItem.Quantity, // 👈 Price × Quantity
                   
                };
                ViewBag.Shipping = shipping; 
                model.Products.Add(t);
                // DateTime startTime = DateTime.Now;
                // DateTime duration = startTime.AddMinutes(30);
                // long unix = ((DateTimeOffset)duration).ToUnixTimeSeconds();
                // t.TimeUnix = unix;//todo da promenj da pokazwa ot cart table;


            }

            return View("~/Views/Payment/Cart.cshtml", model);
        }
        [Route("/Payment/Cart/time")]
        public IActionResult Time(CartViewModel model)
        {
            var bas = cartService.GetAll().ToList();

            foreach (var item in bas)
            {

            }

            DateTime currentTime = DateTime.Now;
            Console.WriteLine($"Current Time: {currentTime}");
            Console.WriteLine("30 minutes have passed!");
            long currentUnixTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            Console.WriteLine($"Current Unix timestamp: {currentUnixTimestamp}");
            long futureUnixTimestamp = currentUnixTimestamp + 60; // 1 minute in seconds
            Console.WriteLine($"Future Unix timestamp (1 minute later): {futureUnixTimestamp}");

            // Simulate the passage of time (you can replace this with actual elapsed time logic)
            Console.WriteLine("Waiting for 1 minute...");

            while (DateTimeOffset.UtcNow.ToUnixTimeSeconds() < futureUnixTimestamp)
            {
                // Simulate the passage of time (you can replace this with actual elapsed time logic)
                // For the sake of this example, we use a short delay.
                System.Threading.Thread.Sleep(1000); // 1 second delay
            }

            // The loop exits when 1 minute has passed
            long currentUnixTimestampAfterDelay = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            Console.WriteLine($"Current Unix timestamp after 1 minute: {currentUnixTimestampAfterDelay}");
            Console.WriteLine("1 minute has passed!");
            //DateTime startTime = DateTime.Now;
            //Console.WriteLine($"Start Time: {startTime}");
            //bool isdeleted = true;

            //// Set the duration to 30 minutes
            //TimeSpan duration = TimeSpan.FromMinutes(1);

            //// Calculate the end time
            //DateTime endTime = startTime.Add(duration);
            //Console.WriteLine($"End Time: {endTime}");

            //// Simulate the passage of time (you can replace this with actual elapsed time logic)
            //// For the sake of this example, we use a simple loop with a delay.
            //Console.WriteLine("Waiting for 30 minutes...");

            //while (DateTime.Now <= endTime)
            //{
            //    if (DateTime.Now == endTime)
            //    {
            //        isdeleted = false;
            //    }
            //    // Simulate the passage of time (you can replace this with actual elapsed time logic)
            //    // For the sake of this example, we use a short delay.
            //    System.Threading.Thread.Sleep(1000); // 1 second delay

            //}
            //if (!isdeleted )
            //{
            //    return Redirect("/Admin/Products");
            //}

            //return View("~/Views/Payment/Cart.cshtml");
            return View("~/Views/Admin/Products/Index.cshtml");
        }
        [HttpGet]
        [Route("Payment/Cart/Create/{id}")]
        public IActionResult Create(int id)
        {
            var product = productService.GetProductById(id);
            if (product != null)
            {
                cartService.AddProduct(new Cart
                {
                    ProductId = product.Id,
                    Quantity = 1
                });
            }
            return Redirect("/");

        }
        [HttpGet]
        [Route("/Payment/Cart/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            cartService.Delete(id);
            return Redirect("/Payment/Cart");
        }
        [HttpGet]
        [Route("api/cart/count")]
        public IActionResult GetCartCount()
        {
            int count = cartService.GetCartCount();
            return Ok(new { count }); // returns the current cart count as JSON
        }
        [HttpPost]
        [Route("api/cart/add/{id}")]

        public IActionResult AddToCart(int id)
        {
            var product = productService.GetProductById(id);
            if (product != null)
            {
                cartService.AddProduct(new Cart
                {
                    ProductId = product.Id,
                    Quantity = 1
                });
            }

            // Return the updated count
            return Ok(new { count = cartService.GetCartCount() });
        }

    }

}
