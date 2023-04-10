using Microsoft.AspNetCore.Razor.Language.Extensions;
using QuizForge.Models.QuizModels;

namespace QuizForge.ViewModels
{
    public class QuizPassViewModel
    {
        public Quiz Quiz { get; set; } = new Quiz();
        public List<QuizQuestion> QuizQuestions { get; set; } = new List<QuizQuestion>();
        public List<QuestionAnswer> QuizQuestionsAnswers { get; set; } = new List<QuestionAnswer>();
        public int CurrentAttempts { get; set; }
        public bool IsAvaible { get; set; }

        public Dictionary<int, string> Answers { get; set; } = new Dictionary<int, string>();
    }
}
