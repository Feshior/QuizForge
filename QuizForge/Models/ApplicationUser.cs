using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace QuizForge.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string VisibleName { get; set; } = string.Empty;
    }
}
