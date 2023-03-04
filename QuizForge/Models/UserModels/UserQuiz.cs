using QuizForge.Models.QuizModels;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizForge.Models.UserModels
{
    public class UserQuiz
    {
        public int Id { get; set; }
        //public int UserAttempts = 0;
        
        //Element on 0 index means that it related to 1 attempt
        public List<UserPoint> UserPoints { get; set; } = new List<UserPoint>();

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; } = string.Empty;
        public ApplicationUser ApplicationUser { get; set; } = new ApplicationUser();


        [ForeignKey("Quiz")]
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; } = new Quiz();
    }
}
