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
            // Валидация если промт был пустым
            if(string.IsNullOrWhiteSpace(model.UserPromnt)) {
                ModelState.AddModelError("", "Question is required.");
                return View(model);
            }

            // Асинхронный ответ
            model.Response = await _mentorGPT.MentorResponse(model.UserPromnt);
            

            return View(model);
        }

    }
}
