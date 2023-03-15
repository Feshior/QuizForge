using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
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
        [MaxLength(100)]
        public string Question { get; set; } = string.Empty;
        
        public List<QuestionAnswer> QuizAnswers { get; set; } = new List<QuestionAnswer>();
        public double QuestionPoints { get; set; } = 0.0F;

        [AllowNull]
        public string? QuestionImage { get; set; } = string.Empty;

#warning Change name to the QuestionType!
        private QuizType quizType { get; set; }
            
        public QuizType QuizType { get {return quizType;}
            set { 
                quizType = value;
            }
        } 

        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; } = new Quiz(); 
        
    }
}
