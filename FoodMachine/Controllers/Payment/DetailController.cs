using Food.Dal.Models.Admin;
using Food.Dal.Models.Payment;
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
            if (product == null) return NotFound();

            var relatedProducts = _productService.GetRelatedProducts(id, 6);

            var model = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Discription,
                Price = product.Price,
                MainImage = product.MainImage,
                AverageRating = product.AverageRating,  // add this
                RatingCount = product.RatingCount,      // add this
                ListProducts = relatedProducts
            };

            return View("~/Views/Payment/Detail.cshtml", model);

        }

        [HttpPost("addRating")]
        public IActionResult AddRating([FromBody] RatingRequest request)
        {
            var product = _productService.GetProductById(request.ProductId);
            if (product == null) return NotFound();

            product.RatingCount++;
            product.AverageRating = ((product.AverageRating * (product.RatingCount - 1)) + request.Rating) / product.RatingCount;

            _productService.UpdateProductRating(product);

            return Json(new { success = true });
        }

    }
}
