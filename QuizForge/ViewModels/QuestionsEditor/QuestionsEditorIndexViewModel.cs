using QuizForge.Models.QuizModels;

namespace QuizForge.ViewModels.QuestionsEditor
{
    public class QuestionsEditorIndexViewModel {
        public int quizId { get; set; }
        public AddQuestionViewModel? addQuestionViewModel { get; set; }
    }
}
