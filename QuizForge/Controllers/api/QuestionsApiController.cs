using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizForge.Data;
using QuizForge.Models.QuizModels;
using QuizForge.Models.UserModels;
using QuizForge.Services;
using System.Runtime.InteropServices;

namespace QuizForge.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsApiController : ControllerBase
    {
        private ApplicationDbContext _context;
        private SignInManager<ApplicationUser> signInManager;
        private string admin_email = "holo@gmail.com";
        private IUserImageUploader userImageUploader;
        private ILogger<QuizzesEditorController> logger;
        public QuestionsApiController(ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager,
            IUserImageUploader userImageUploader,
            ILogger<QuizzesEditorController> logger)
        {
            this._context = context;
            this.signInManager = signInManager;
            this.userImageUploader = userImageUploader;
            this.logger = logger;
        }
        [Route("GetQuizQuestions")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuizQuestions(int id = -1)
        {
            if (_context.Quizzes.Where(q => q.Id == id).Count() == 0)
                return NotFound("No such quiz");

            List<QuizQuestion> result = await _context.QuizQuestions.Where(q => q.QuizId == id)
                .Include(q => q.QuizAnswers).ToListAsync();
            if (result.Count == 0)
                return Ok();

            //Breaking circular reference
            foreach (QuizQuestion question in result)
            {
                foreach (QuestionAnswer questionAnswer in question.QuizAnswers)
                {
                    if (questionAnswer.QuizQuestion != null)
                        questionAnswer.QuizQuestion = new QuizQuestion();
                }
            }

            return Ok(result);
        }

        [Route("DeleteQuestion")]
        [HttpPost]
        public IActionResult DeleteQuestion()
        {
            int id = -1;
            
            try
            {
                string Sid = "";
                if (HttpContext.Request.Form.ContainsKey("id"))
                    Sid = HttpContext.Request.Form["id"];

                if (Sid == "")
                    throw new Exception();

                id = int.Parse(Sid);

            }catch(Exception)
            {
                return BadRequest("Invalid request");
            }
            
            if (id == -1)
                return BadRequest("Invalid request");
            if(!(signInManager.IsSignedIn(User) && User?.Identity?.Name == admin_email))
            {
                return Forbid("User is not authorized or doesn't have enough permissions");
            }
            //User authorized and id is correct

            QuizQuestion? quizToDelete =  _context.QuizQuestions.FirstOrDefault(q => q.Id == id);
            if (quizToDelete == null)
                return BadRequest("Invalid id");
            List<QuestionAnswer> answers = _context.QuestionAnswers.Where(q => q.QuizQuestionId == quizToDelete.Id).ToList();

            //Removing image
            if (quizToDelete.QuestionImage != null)
            {
                if (userImageUploader.DeleteImage(quizToDelete.QuestionImage) != -1)
                    logger.LogInformation($"Deleted image on - {quizToDelete.QuestionImage}; from User - {User.Identity.Name}");
                else
                    logger.LogWarning($"Image deleting error! - - {quizToDelete.QuestionImage}; from User - {User.Identity.Name}");

            }

            _context.QuestionAnswers.RemoveRange(answers);
            _context.QuizQuestions.Remove(quizToDelete);
            _context.SaveChanges();

            //Updating points


            return Ok("Successfully deleted!");
        }


        public class DeleteQuestionModel
        {
            public int Id { get; set; } = -1;
        }
    }


}
