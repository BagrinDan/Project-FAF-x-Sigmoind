using Microsoft.AspNetCore.Mvc;
using ProjectSigmoind.Models;
using System.Diagnostics;
using ProjectSigmoind.Domain.Models;
using ProjectSigmoind.BussinesLayer.AI.Interface;

namespace ProjectSigmoind.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly IMentorGPT _mentorGPT;

        public HomeController(ILogger<HomeController> logger, IMentorGPT mentorGPT) {
            _logger = logger;
            _mentorGPT = mentorGPT; 
        }

        public IActionResult Index() {
            return View();
        }

        // -- Content --
        public IActionResult CreatePromnt() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PromntModel model) {
            if(ModelState.IsValid) {
                var response = await _mentorGPT.MentorResponse(model.UserPromnt);
                ViewBag.Response = response;
            }

            return View("Index", model); 
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
