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
           
            var model = new ContactMessage();
            return View("~/Views/Payment/Contact.cshtml", model); 
        }
    }
}
