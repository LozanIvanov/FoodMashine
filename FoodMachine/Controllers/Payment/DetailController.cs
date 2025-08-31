using Food.Dal.Models.Admin;
using Food.Dal.Services;
using Microsoft.AspNetCore.Mvc;

namespace Food.Web.Controllers
{
    [Route("payment/detail")]
    public class DetailController : Controller
    {
        private readonly ProductService _productService;

        public DetailController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public IActionResult Index(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            
            var relatedProducts = _productService.GetRelatedProducts(id, 6);

            var model = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Discription,
                Price = product.Price,
                Quantity = product.Quantity,
                MainImage = product.MainImage,
                ListProducts = relatedProducts
            };
            return View("~/Views/Payment/Detail.cshtml", model);
        }
    }
}
