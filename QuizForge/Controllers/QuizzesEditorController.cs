using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizForge.Models.UserModels;

namespace QuizForge.Controllers
{
    public class QuizzesEditorController : Controller
    {
        SignInManager<ApplicationUser> SignInManager { get; set; }
        public QuizzesEditorController(SignInManager<ApplicationUser> signInManager)
        {
            SignInManager = signInManager;
        }

        public IActionResult Index()
        {
            if (SignInManager.IsSignedIn(User) && User?.Identity?.Name == "holo@gmail.com")
            {
                return View();
            }
            return View("404");
        }
    }
}
