using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizForge.Models.QuizModels
{
    public class QuestionAnswers
    {
        public int Id { get; set; }
        [Required]
        public string Answer { get; set; } = string.Empty;
        public bool IsCorrect { get; set; } = false;

        [ForeignKey("QuizQuestion")]
        [Required]
        public int QuizQuestionId { get; set; }
        [Required]
        public QuizQuestion QuizQuestion { get; set; } = new QuizQuestion();
    }
}
