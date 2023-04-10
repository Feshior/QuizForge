using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizForge.Data;
using QuizForge.Models.QuizModels;
using QuizForge.Models.UserModels;
using System.Runtime.InteropServices;

namespace QuizForge.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsApiController : ControllerBase
    {
        ApplicationDbContext context;
        SignInManager<ApplicationUser> signInManager;
        public QuestionsApiController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager)
        {
            this.context = context;
            this.signInManager = signInManager;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuizQuestions(int id=-1)
        {
            if (context.Quizzes.Where(q => q.Id == id).Count() == 0)
                return NotFound("No such quiz");

            List<QuizQuestion> result = await context.QuizQuestions.Where(q => q.QuizId == id)
                .Include(q=>q.QuizAnswers).ToListAsync();
            if (result.Count == 0)
                return Ok();

            //Breaking circular reference
            foreach(QuizQuestion question in result)
            {
                foreach(QuestionAnswer questionAnswer in question.QuizAnswers)
                {
                    if (questionAnswer.QuizQuestion != null)
                        questionAnswer.QuizQuestion = new QuizQuestion();
                }
            }

            return Ok(result);
        }
    }

}
