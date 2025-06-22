using Microsoft.AspNetCore.Mvc;
using ProjectSigmoind.BussinesLayer.AI.Entity;
using ProjectSigmoind.Domain.Models;
using ProjectSigmoind.BussinesLayer.AI.Interface;

namespace ProjectSigmoind.Controllers {
    public class MentorController : Controller {
        private readonly IMentorGPT _mentorGPT;

        public MentorController(IMentorGPT mentorGPT) {
            _mentorGPT = mentorGPT;
        }

        [HttpGet]
        public IActionResult Create() {
            return View(new PromntModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(PromntModel model) {
            if(string.IsNullOrWhiteSpace(model.UserPromnt)) {
                ModelState.AddModelError("", "Question is required.");
                return View(model);
            }

            model.Response = await _mentorGPT.MentorResponse(model.UserPromnt);
            //model.Links = new Dictionary<string, string> {
            //    {"Google", "https://www.google.com/search?q=" + System.Net.WebUtility.UrlEncode(model.UserPromnt)},
            //    {"Wikipedia", "https://en.wikipedia.org/wiki/Special:Search?search=" + System.Net.WebUtility.UrlEncode(model.UserPromnt)}
            //};

            return View(model);
        }

    }
}
