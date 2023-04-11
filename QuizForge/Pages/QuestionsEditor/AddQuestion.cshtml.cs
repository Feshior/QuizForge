using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizForge.Data;
using QuizForge.Models.QuizModels;
using QuizForge.Models.UserModels;
using QuizForge.Services;
using QuizForge.ViewModels.QuestionsEditor;
using static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace QuizForge.Pages.QuizzesEditor
{
    public class AddQuestionModel : PageModel
    {
        public Quiz? Quiz { get; set; }
        private ApplicationDbContext _context;
        private IUserImageUploader _userImageUploader;
        private SignInManager<ApplicationUser> _signInManager;
        public AddQuestionModel(ApplicationDbContext applicationDbContext, IUserImageUploader userImageUploader, SignInManager<ApplicationUser> signInManager)
        {
            _context = applicationDbContext;
            _userImageUploader = userImageUploader;
            _signInManager = signInManager;
        }
        public IActionResult OnGet(int quizId = -1)
        {
            if (quizId == -1)
                return RedirectToPage("/Results/404");

            Quiz = _context.Quizzes.Where(q => q.Id == quizId).FirstOrDefault();
            if(Quiz == null)
                return RedirectToPage("/Results/404");
            return Page();
        }

        [BindProperty]
        public AddQuestionViewModel AddQuestionMd { get; set; } = new AddQuestionViewModel();

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                ModelStateEntry[] values = ModelState.Values.ToArray();

                for (int i = 0; i < values.Count(); i++)
                {
                    if (values[i].Errors.Count > 0)
                    {
                        foreach (ModelError error in values[i].Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }
                }
                return RedirectToPage("ResultPage",
                new
                {
                    Result = false,
                    Messages = errors
                }
                );
            }


            Quiz? quizToEdit = _context.Quizzes.FirstOrDefault(q => q.Id == AddQuestionMd.QuizId);
            if (quizToEdit == null)
            {
                return RedirectToPage("ResultPage",
                new
                {
                    Result = false,
                    Messages = new List<string> { "Quiz was not found!" }
                }
                );
            }
            if (!(_signInManager.IsSignedIn(User) && User?.Identity?.Name == "holo@gmail.com"))
            {
                return RedirectToPage("ResultPage",
                new
                {
                    Result = false,
                    Messages = new List<string> { "Unauthorized!" }
                }
                );
            }


            QuizQuestion newQuestion = new QuizQuestion();
            newQuestion.Quiz = quizToEdit;

            //Loading image
            if (AddQuestionMd.QuestionImage != null)
            {
                string[] result = await _userImageUploader.UploadImage(AddQuestionMd.QuestionImage, User.Identity.Name);
                if (result.Length >= 2 && result[0] != "-1")
                {
                    newQuestion.QuestionImage = result[1];
                }
                else
                {
                    string message = "";
                    if (result.Length > 2)
                        message = result[1];
                    return RedirectToPage("ResultPage",
                                            new
                                            {
                                                Result = false,
                                                Messages = new List<string> { message }
                                            }
                                            );
                }
            }

            newQuestion.QuestionPoints = AddQuestionMd.Points;
            newQuestion.Question = AddQuestionMd.Question;

            QuestionAnswer answer = new QuestionAnswer()
            {
                Answer = AddQuestionMd.Answer,
                IsCorrect = true,
            };

            newQuestion.QuizAnswers.Add(answer);

            _context.QuizQuestions.Add(newQuestion);
            await _context.SaveChangesAsync();

            // Updating quiz points
            Quiz? changedQuiz = _context.Quizzes.FirstOrDefault(q => q.Id == quizToEdit.Id);
            if (changedQuiz != null)
            {
                int points = (int)(_context.QuizQuestions.Where(q => q.QuizId == changedQuiz.Id).Sum(question => question.QuestionPoints));
                changedQuiz.QuizPoints = points;
                await _context.SaveChangesAsync();
            }



            return RedirectToPage("ResultPage",
                new
                {
                    Result = true,
                    Messages = new List<string>() { $"Question was added successfully with index - {newQuestion.Id}" }
                }
                );
        }

    }
   
}
