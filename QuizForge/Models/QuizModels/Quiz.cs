using QuizForge.Models.UserModels;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace QuizForge.Models.QuizModels
{

    public class Quiz
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Quiz name")]
        public string QuizName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Quiz points")]
        public int QuizPoints { get; set; } = 0;

        [Display(Name = "Allowed attempts")]
        public int MaxAttempts { get; set; } = 10;
        [Display(Name = "Quiz image")]

        [AllowNull]
        public string QuizImage { get; set; } = "/img/default/quiz_default.png";

        

        public IEnumerable<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();
        public List<UserQuiz> UserQuizzes { get; set; } = new List<UserQuiz>();

    }
}
