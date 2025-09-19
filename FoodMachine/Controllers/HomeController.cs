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

        public IActionResult Index(int? page, List<string>? categories, decimal? minPrice, decimal? maxPrice, string? search)
        {
            var model = new ProductViewModel
            {
                ListProducts = productService.GetProducts(page, minPrice, maxPrice, search, categories),
                TotalPages = productService.GetTotalPages(minPrice, maxPrice, search, categories),
                CurrentPage = page ?? 1,
                CategoryCounts = productService.GetCategoriesWithCounts(),
                SelectedCategories = categories ?? new List<string>(),
                  MinPrice = minPrice,
                MaxPrice = maxPrice
            };

            return View("~/Views/Home/index.cshtml", model);
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