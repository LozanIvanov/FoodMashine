using Food.Dal.Models.Admin;
using Food.Dal.Services;
using FoodMachine.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FoodMachine.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductService productService;

        public HomeController(ILogger<HomeController> logger, ProductService productService)
        {
            _logger = logger;
            this.productService = productService;
        }

        public IActionResult Index(int? page)
        {
            var result = new ProductViewModel()
            {
                ListProducts = this.productService.GetProducts(page),
                TotalPages = this.productService.GetTotalPages(),
                CurrentPage = page != null ? page.Value : 1
            };
            return View("~/Views/Home/index.cshtml", result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}