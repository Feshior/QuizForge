using QuizForge.Models.QuizModels;

namespace QuizForge.ViewModels
{
    public class QuizResultViewModel
    {
        public Quiz Quiz { get; set; } = new Quiz();
        public double Points { get; set; }
    }
}
