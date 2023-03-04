using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace QuizForge.Models.UserModels
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string VisibleName { get; set; } = string.Empty;

        public List<UserQuiz> completetQuizes = new List<UserQuiz>();
    }
}
