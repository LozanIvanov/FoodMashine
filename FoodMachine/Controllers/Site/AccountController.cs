using Food.Dal.Models.Site.Account;
using Food.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodMachine.Controllers.Site
{
    public class AccountController : Controller
    {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [Route("login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View("~/Views/Site/Account/Login.cshtml");
        }
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await signInManager.PasswordSignInAsync(username, password, true, false);
            if (result.Succeeded == false)
            {
                return Redirect("/login");
            }
            var user = await userManager.FindByNameAsync(username);
            var roles = await userManager.GetRolesAsync(user);
            if (roles.Contains("Admin"))//да ме логне като админ
            {
                return Redirect("/Admin/DashBoard");
            }
            if (roles.Contains("Manager"))//да ме логне като мениджър
            {
                return Redirect("/Admin/Products");
            }
            return Redirect("/");
        }
        [Route("register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View("~/Views/Site/Account/Register.cshtml");
        }
        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var User = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
            };
            var result = await userManager.CreateAsync(User, model.Password);

            return RedirectToAction("Login");
        }
        [Route("logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Redirect("/login");
        }
    }
}
