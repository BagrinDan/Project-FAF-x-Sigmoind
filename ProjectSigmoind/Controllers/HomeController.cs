using Microsoft.AspNetCore.Mvc;
using ProjectSigmoind.Models;
using System.Diagnostics;
using ProjectSigmoind.Domain.Models;

namespace ProjectSigmoind.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {
            return View();
        }

        // -- Content --
        public IActionResult CreatePromnt() {
            return View();
        }

        public IActionResult CreatePromnt(PromntModel promntModel) {
            if(promntModel == null) { 
                return View("Error");
            }

            return RedirectToAction("Succes");
        }
        // --

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
