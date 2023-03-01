using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizForge.Data;
using QuizForge.Models.UserModels;
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
    }
}
