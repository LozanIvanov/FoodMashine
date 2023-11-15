using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using Food.Dal.Services;
using Food.Database.Models;
using Food.Dal.Models.Admin;

namespace FoodMachine.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ProductService productService;
      
        private readonly IWebHostEnvironment environment;
        public ProductController(ProductService productService,  IWebHostEnvironment environment)
        {
            this.productService = productService;
           
            this.environment = environment;
        }

        [Route("Admin/Products")]
        public IActionResult Index(int? page)
        {
            var result = new ProductViewModel()
            {
                ListProducts = this.productService.GetProducts(page),
                TotalPages = this.productService.GetTotalPages(),
                CurrentPage = page != null ? page.Value : 1
            };


            return View("~/Views/Admin/Products/Index.cshtml", result);
        }

        [Route("Admin/Products/Create")]
        [HttpGet]
        public IActionResult Create()
        {
            ProductViewModel model=new ProductViewModel();
            return View("~/Views/Admin/Products/Create.cshtml",model);
        }

        [Route("Admin/Products/Create")]
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel productModel)
        {
            string filePath = "no-image.png";
            if (productModel.MainImageFile != null)
            {
                filePath = await UploadFile(productModel.MainImageFile);
            }

            Product product = new Product
            {
                Name = productModel.Name,
                Discription = productModel.Description,
                Quantity = productModel.Quantity,
                Price = productModel.Price,
                MainImage = filePath,
               
            };

            this.productService.AddProduct(product);

            return Redirect("/Admin/Products");
        }

        [Route("Admin/Products/Edit/{id}")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
        
            var model = productService.GetProductById(id);
            var newProduct = new ProductViewModel()
            {
                Name = model.Name,
                Description = model.Discription,
                Price = model.Price,
                MainImage = model.MainImage
            };

            return View("~/Views/Admin/Products/Edit.cshtml", newProduct);
        }

        [Route("Admin/Products/Edit/{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductViewModel productModel)
        {
            string filePath = "no-image.png";
            if (productModel.MainImageFile != null)
            {
                filePath = await UploadFile(productModel.MainImageFile);
            }

            Product product = new Product
            {
                Name = productModel.Name,
                Discription = productModel.Description,
                Quantity = productModel.Quantity,
                Price = productModel.Price,
                MainImage = filePath,
              
            };

            this.productService.UpdateProduct(id, product);

            return Redirect("/Admin/Products");
        }
        [HttpGet]
        [Route("/Admin/Products/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            productService.Delete(id);
            return Redirect("/Admin/Products");
        }


        private async Task<string> UploadFile(IFormFile file)
        {
            var uniqueFileName = Guid.NewGuid() + "-" + file.FileName;
            var filePath = Path.Combine("wwwroot", "img", "products", uniqueFileName);

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }

            return uniqueFileName;
        }
    }
}