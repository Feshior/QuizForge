using Microsoft.AspNetCore.Mvc;
namespace QuizForge.Controllers
{
    public class QuizzesEditorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
