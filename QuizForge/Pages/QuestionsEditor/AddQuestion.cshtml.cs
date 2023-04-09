using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuizForge.Data;
using QuizForge.Models.QuizModels;
using QuizForge.ViewModels.QuestionsEditor;
using static Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary;

namespace QuizForge.Pages.QuizzesEditor
{
    public class AddQuestionModel : PageModel
    {
        public Quiz? Quiz { get; set; }
        private ApplicationDbContext _context;
        public AddQuestionModel(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
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

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                ModelStateEntry[] values = ModelState.Values.ToArray();

                for (int i=0; i < values.Count(); i++)
                {
                    if (values[i].Errors.Count > 0)
                    {
                        foreach(ModelError error in values[i].Errors)
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
            if(quizToEdit == null)
            {
                return RedirectToPage("ResultPage",
                new
                {
                    Result = false,
                    Messages = new List<string> { "Quiz was not found!"}
                }
                );
            }

            QuizQuestion newQuestion = new QuizQuestion();
            newQuestion.Quiz = quizToEdit;
            Console.WriteLine($"{quizToEdit.Id}- -----------------------------");
            
            newQuestion.QuestionPoints = AddQuestionMd.Points;
            newQuestion.Question = AddQuestionMd.Question;

            QuestionAnswer answer = new QuestionAnswer()
            {
                Answer = AddQuestionMd.Answer,
                IsCorrect = true,
            };

            newQuestion.QuizAnswers.Add(answer);

            _context.QuizQuestions.Add(newQuestion);

            // Updating quiz points
            int points = (int)(_context.QuizQuestions.Where(q => q.QuizId == quizToEdit.Id).Sum(question => question.QuestionPoints));
            quizToEdit.QuizPoints = points;

            _context.SaveChanges();

            return RedirectToPage("ResultPage",
                new {
                    Result = true,
                    Messages = new List<string>() { $"Question was added successfully with index - {newQuestion.Id}"}
                }
                );
        }

    }
   
}
