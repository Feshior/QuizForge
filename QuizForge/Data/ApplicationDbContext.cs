using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuizForge.Models.QuizModels;
using QuizForge.Models.UserModels;

namespace QuizForge.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //Quizes models
        DbSet<Quiz> quizzes => Set<Quiz>();
        DbSet<QuizQuestion> quizQuestions => Set<QuizQuestion>();
        DbSet<QuizCorrectAnswer> quizCorrectAnswers => Set<QuizCorrectAnswer>();
        //************

        //UserModels
        DbSet<UserPoint> userPoints => Set<UserPoint>();
        DbSet<UserQuiz> userQuizzes => Set<UserQuiz>();
    }
}