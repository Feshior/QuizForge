using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizForge.Data;
using QuizForge.ViewModels.QuestionsEditor;

namespace QuizForge.Controllers
{
    public class QuestionsEditorController : Controller
    {
        ApplicationDbContext dbContext;
        public QuestionsEditorController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }
        [HttpGet]
        public IActionResult Index(int quizId)
        {
            return View(
                new QuestionsEditorIndexViewModel()
                {
                    quizId = quizId
                }
                ) ;
        }
    }
}
