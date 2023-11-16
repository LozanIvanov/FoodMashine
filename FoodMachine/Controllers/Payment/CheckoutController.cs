using Food.Dal.Models.Payment;
using Food.Dal.Services;
using Food.Database;
using Food.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodMachine.Controllers.Payment
{
    public class CheckoutController : Controller
    {
        private readonly CartService cartService;
      
        private readonly ApplicationDbContext _context;
        private readonly CheckoutService checkoutService;


        public CheckoutController(CartService cartService, ApplicationDbContext context, CheckoutService checkoutService)
        {
            this.cartService = cartService;
           
            this._context = context;
            this.checkoutService = checkoutService;
        }
        CheckoutViewModel model = new CheckoutViewModel();
        [HttpGet]
        [Route("/Payment/Checkout")]
        public IActionResult Index()
        {
            return View("~/Views/Payment/Checkout.cshtml");
        }

        [HttpGet]
        [Route("/Payment/Checkout/Create")]
        public IActionResult Create()
        {

            List<Product> w = new List<Product>();
            List<Product> pro = _context.CartItems.Select(x => new Product
            {
                Id = x.ProductId
            }).ToList();
            foreach (var pr in pro)
            {
                Product product = _context.Products.Where(x => x.Id == pr.Id).FirstOrDefault();
                w.Add(product);
            }

            model.CartItemList = cartService.GetAll();
        

            model.Products = w;



            return View("~/Views/Payment/Checkout/Create.cshtml", model);
        }
        [HttpPost]
        [Route("/Payment/Checkout/Create")]
        public IActionResult Create(CheckoutViewModel products)
        {
            List<Product> w = new List<Product>();

            List<Product> pro = _context.CartItems.Select(x => new Product
            {
                Id = x.ProductId
            }).ToList();
            foreach (var pr in pro)
            {
                Product product = _context.Products.Where(x => x.Id == pr.Id).FirstOrDefault();
                w.Add(product);
            };
            model.Products = w;

            List<Product> me = new List<Product>();
            List<Cart> sd = cartService.GetAll();



            Checkout m = new Checkout()
            {
                Id = products.Checkouts.Id,
                FirstName = products.Checkouts.FirstName,
                LastName = products.Checkouts.LastName,
                Email = products.Checkouts.Email,
                MobilNumber = products.Checkouts.MobilNumber,
                Address = products.Checkouts.Address,
                City = products.Checkouts.City,
                State = products.Checkouts.State,
                DateTime = products.Checkouts.DateTime,
                Country = products.Checkouts.Country,



            };

            checkoutService.AddCheckout(m);

            return Redirect("/Home");
        }
    }
}
