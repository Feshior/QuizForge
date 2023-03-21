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
        public IActionResult Index()
        {
            return View(
                new QuestionsEditorIndexViewModel()
                {
                    QuizQuestions = dbContext.QuizQuestions.Include(q => q.QuizAnswers).ToList()
                }
                );
        }
    }
}
