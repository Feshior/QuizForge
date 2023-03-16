using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizForge.Data;
using QuizForge.Models.QuizModels;
using QuizForge.Models.UserModels;
using System.Composition;

namespace QuizForge.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizzesApiController : ControllerBase
    {
        private readonly string adminEmail = "holo@gmail.com";
        ApplicationDbContext context;
        SignInManager<ApplicationUser> SignInManager;
        public QuizzesApiController(ApplicationDbContext context, SignInManager<ApplicationUser> SignInManager)
        {
            this.context = context;
            this.SignInManager = SignInManager;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuiz(int id = -1)
        {
            Quiz? quiz = await context.Quizzes.FindAsync(id);
            if(quiz == null)
            {
                return NotFound();
            }
            return Ok(quiz);
        }
        public List<Quiz> GetQuizzes()
        {
            return context.Quizzes.ToList();
        }


    }
}
