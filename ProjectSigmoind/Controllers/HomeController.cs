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

        [HttpGet]
        public IActionResult Index() {
            return View(new PromntModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(PromntModel model) {
            if(string.IsNullOrWhiteSpace(model.UserPromnt)) {
                ModelState.AddModelError("UserPromnt", "");
                return View(model);
            }

            model.Response = await _mentorGPT.MentorResponse(model.UserPromnt);
            // Заглушка
            model.Links = new Dictionary<string, string> {
                {"Google", "https://www.google.com/search?q=" + System.Net.WebUtility.UrlEncode(model.UserPromnt)},
                {"Wikipedia", "https://en.wikipedia.org/wiki/Special:Search?search=" + System.Net.WebUtility.UrlEncode(model.UserPromnt)}
            };

            return View(model);
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
