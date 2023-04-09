using System.ComponentModel.DataAnnotations;

namespace QuizForge.ViewModels.QuestionsEditor
{
    public class AddQuestionViewModel
    {
        [Required(ErrorMessage = "Quiz is not specified")]
        public int QuizId { get; set; }
        [Required(ErrorMessage = "Question field is empty")]
        public string Question { get; set; } = string.Empty;

        [Required(ErrorMessage = "Message field is empty")]
        public string Answer { get; set; } = string.Empty;

        [Required(ErrorMessage = "Points field is empty")]
        public int Points { get; set; }

        public IFormFile? QuestionImage { get; set; }

    }
}
