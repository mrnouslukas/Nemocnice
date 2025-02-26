using Hospital.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Hospital.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private UserManager<AppUser> userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager) {
            _logger = logger;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> Index() {
            //_logger.LogInformation("iNDEX CALLED");
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            string message = $"Hello {user.UserName}";
            return View("Index", message);
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
