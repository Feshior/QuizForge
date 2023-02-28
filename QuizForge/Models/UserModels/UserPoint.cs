using System.ComponentModel.DataAnnotations.Schema;

namespace QuizForge.Models.UserModels
{
    public class UserPoint
    {
        public int Id { get; set; }
        public double Points { get; set; } = 0.0f;

        [ForeignKey("UserQuiz")]
        public int UserQuizId { get; set; }
        public UserQuiz UserQuiz { get; set; } = new UserQuiz();
    }
}
