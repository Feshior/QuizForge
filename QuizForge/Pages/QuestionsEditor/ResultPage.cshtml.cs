using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace QuizForge.Pages.QuestionsEditor
{
    public class ResultPageModel : PageModel
    {

        public bool Result { get; set; } = false;
        public IEnumerable<string> Messages { get; set; } = Enumerable.Empty<string>();

        public void OnGet(bool result, IEnumerable<string> messages)
        {
            Result = result;
            Messages = messages;
        }
    }
}
