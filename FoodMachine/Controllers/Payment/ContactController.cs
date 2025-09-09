using Microsoft.AspNetCore.Mvc;
using FoodMachine.Models;

namespace FoodMachine.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet]
        [Route("/Payment/Contact")]
        public IActionResult Index()
        {
            // Create an empty model for the form
            var model = new ContactMessage();
            return View("~/Views/Payment/Contact.cshtml", model); // View: Views/Contact/Index.cshtml
        }
    }
}
