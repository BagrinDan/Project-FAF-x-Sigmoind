using Microsoft.AspNetCore.Mvc;
using ProjectSigmoind.BussinesLayer.AI.Entity;

namespace ProjectSigmoind.Controllers {
    public class MentorController : Controller {
        private readonly MentorGPT _mentorGPT;

        public MentorController(MentorGPT mentorGPT) {
            _mentorGPT = mentorGPT;
        }

        public async Task<IActionResult> Chat(string userInput) {
            var mentorReply = await _mentorGPT.MentorResponse(userInput);
            return Json(new { response = mentorReply });
        }
    }
}
