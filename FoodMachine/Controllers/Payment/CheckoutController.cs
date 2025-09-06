using Food.Dal.Models.Payment;
using Food.Dal.Services;
using Food.Database.Models;
using Microsoft.AspNetCore.Mvc;
namespace Food.Web.Controllers.Payment
{
    [Route("Payment/Checkout")]
    public class CheckoutController : Controller
    {
        private readonly CartService _cartService;
        private readonly CheckoutService _checkoutService;

        public CheckoutController(CartService cartService, CheckoutService checkoutService)
        {
            _cartService = cartService;
            _checkoutService = checkoutService;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var model = new CheckoutViewModel
            {
                CartItemList = _cartService.GetAll(),
                Products = _cartService.GetProducts()
            };
            return View("~/Views/Payment/Checkout.cshtml", model);
        }

        [HttpPost("")]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CheckoutViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.CartItemList = _cartService.GetAll();
                model.Products = _cartService.GetProducts();
                return View("~/Views/Payment/Checkout.cshtml", model);
            } 
            model.Checkouts.DateTime = DateTime.Now;
            //_checkoutService.AddCheckout(model.Checkouts);
            var entity = new Checkout
            {
                FirstName = model.Checkouts.FirstName,
                LastName = model.Checkouts.LastName,
                Email = model.Checkouts.Email,
                MobilNumber = model.Checkouts.MobilNumber,
                Address = model.Checkouts.Address,
                City = model.Checkouts.City,
                Country = model.Checkouts.Country,
                State = model.Checkouts.State,
                PaymentMethod = model.Checkouts.PaymentMethod,
                DateTime = DateTime.Now
            };

            _checkoutService.AddCheckout(entity);

            // ✅ Clear cart after successful checkout
            _cartService.ClearCart();

          /*  foreach (var item in model.CartItemList)
            {
                _cartService.Delete(item.ProductId);
            }
  |*/
            return Redirect("/Home");
        }
    }
}
