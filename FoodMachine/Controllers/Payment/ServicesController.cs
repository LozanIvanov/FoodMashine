using Food.Database;
using FoodMachine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace FoodMachine.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ILogger<ServicesController> _logger;
        private readonly ApplicationDbContext _context;

        public ServicesController(ILogger<ServicesController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpPost]
        public IActionResult SubmitMessage(ContactMessage model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "⚠️ Please fill in all required fields.";
                return RedirectToAction("Index", "Contact"); // Back to Contact page
            }

            // Save message to database
            _context.ContactMessages.Add(model);
            _context.SaveChanges();

            TempData["Success"] = "✅ Your message has been received!";
            return RedirectToAction("Index", "Contact");
        }

        public IActionResult Inbox()
        {
            var messages = _context.ContactMessages
                                   .OrderByDescending(m => m.CreatedAt)
                                   .ToList();
            return View(messages); // View: Views/Services/Inbox.cshtml
        }
    }
}
