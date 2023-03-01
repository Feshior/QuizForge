using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizForge.Data;
using QuizForge.Models.UserModels;
using QuizForge.Models.QuizModels;
using QuizForge.ViewModels;

namespace QuizForge.Controllers
{
    public class QuizesController : Controller
    {
        ApplicationDbContext dbContext;
        SignInManager<ApplicationUser> SignInManager;
        public QuizesController(ApplicationDbContext dbContext, SignInManager<ApplicationUser> SignInManager)
        {
            this.dbContext = dbContext;
            this.SignInManager = SignInManager;
        }

        public async Task<IActionResult> Index()
        {
           
            if (SignInManager.IsSignedIn(User))
            {
                string userEmail = User?.Identity?.Name ?? "";
                return View(new QuizListViewModel()
                {
                    Quizzes = await dbContext.Quizzes.ToListAsync(),
                    UserPoints = await dbContext.UserPoints.Where(p => (p.UserQuiz.ApplicationUser.Email == userEmail)).ToListAsync()
            });
            }
            else
            {
                return View(new QuizListViewModel()
                {
                    Quizzes = await dbContext.Quizzes.ToListAsync()
                });
            }
           
        }

        [HttpGet]
        public IActionResult PassTest(int quizId = -1)
        {
            Console.WriteLine(quizId);
            if (quizId != -1)
            {
                Quiz? quizToPass = dbContext.Quizzes.Where(q => q.Id == quizId)
                    .Include(q => q.QuizQuestions).FirstOrDefault();
                
                if (quizToPass == null)
                    return View(new QuizPassViewModel());

                    int attempts = dbContext.UserQuizzes.Where(uq => uq.Id == quizToPass.Id).FirstOrDefault()?.UserPoints.Count() ?? 0;

                    if(attempts >= quizToPass.MaxAttempts)
                    {
                    return View(new QuizPassViewModel()
                    {
                        Quiz = quizToPass,
                        CurrentAttempts = attempts,
                        IsAvaible = false
                    });
                }
                
                List<QuizQuestion> questionsToPass = quizToPass.QuizQuestions.ToList();
                List<QuestionAnswers> questionsAnswers = dbContext.QuestionAnswers.Where(q=>q.QuizQuestion.QuizId == quizId).ToList();

                return View(new QuizPassViewModel()
                {
                    Quiz = quizToPass,
                    CurrentAttempts = attempts,
                    IsAvaible = true,
                    QuizQuestions = questionsToPass,
                    QuizQuestionsAnswers = questionsAnswers
                });
            }
            return View(new QuizPassViewModel());
        }
    }
}
