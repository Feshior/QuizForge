using QuizForge.Models.QuizModels;
using QuizForge.Models.UserModels;

namespace QuizForge.ViewModels
{
    public class QuizListViewModel
    {
        public List<Quiz> Quizzes { get; set; } = new List<Quiz>();
        public List<UserPoint> UserPoints { get; set; } = new List<UserPoint>();
    }
}
