using QuizForge.Models.QuizModels;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
namespace QuizForge.ViewModels
{
    public class AddQuizViewModel
    {
        
        [Required(ErrorMessage = "You need to choose a quiz name")]

        public string QuizName { get; set; } = String.Empty;
        [Required(ErrorMessage = "You need to select max attempts")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int QuizMaxAttempts { get; set; }

        //[Required]
        public IFormFile? QuizImage { get; set; }
    }
}
