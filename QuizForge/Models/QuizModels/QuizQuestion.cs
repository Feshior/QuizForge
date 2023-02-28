using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Policy;

namespace QuizForge.Models.QuizModels
{
    public enum QuizType
    {
        OpenAnswers,
        MultipleAnswers,
        SelectAnswer
    }
    public class QuizQuestion
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string Question { get; set; } = string.Empty;
        public List<QuizCorrectAnswer> CorrectAnswers { get; set; } = new List<QuizCorrectAnswer>();
        public double QuestionPoints { get; set; } = 0.0F;
        public string QuestionImage { get; set; } = string.Empty;

        
        private QuizType quizType { get; set; }
            [NotMappedAttribute]
        public QuizType QuizType { get {return quizType;}
            set { 
                quizType = value;
            if(QuestionImage == String.Empty)
                    switch (value)
                    {
                        case QuizType.OpenAnswers:
                            QuestionImage = "openAnswer_default.png";
                            break;
                        case QuizType.MultipleAnswers:
                            QuestionImage = "multipleAnswers_default.png";
                            break;
                        case QuizType.SelectAnswer:
                            QuestionImage = "singleAnswer_default.png";
                            break;
                        default:
                            QuestionImage = "quiz_default.png";
                            break;
                    }
            }
        } 

        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; } = new Quiz(); 
        
    }
}
