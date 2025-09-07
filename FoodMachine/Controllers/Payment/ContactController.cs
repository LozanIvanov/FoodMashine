using Microsoft.AspNetCore.Mvc;

namespace FoodMachine.Controllers.Payment
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View("~/Views/Payment/Contact.cshtml");
        }
    }
}
