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
        public Quiz Quiz { get; set; } = new Quiz();
        private ApplicationDbContext _context;
        public AddQuestionModel(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public IActionResult OnGet(int quizId = -1)
        {
            Quiz = _context.Quizzes.Where(q => q.Id == quizId).FirstOrDefault() ?? new Quiz();
            return Page();
        }

        [BindProperty]
        public AddQuestionViewModel? AddQuestionMd { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                ModelStateEntry[] values = ModelState.Values.ToArray();

                string message = "";

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

           
            return RedirectToPage("ResultPage",
                new {
                    Result = true,
                    Message = "Success!"
                }
                );
        }

    }
   
}
