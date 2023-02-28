using QuizForge.Models.UserModels;
using System.ComponentModel.DataAnnotations;

namespace QuizForge.Models.QuizModels
{

    public class Quiz
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Quiz name")]
        public string QuizName { get; set; } = string.Empty;
        [Display(Name = "Allowed attempts")]
        public int MaxAttempts { get; set; } = 10;
        [Display(Name = "Quiz image")]
        public string QuizImage { get; set; } = string.Empty;

        

        public IEnumerable<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();
        public List<UserQuiz> userQuizzes { get; set; } = new List<UserQuiz>();

    }
}
