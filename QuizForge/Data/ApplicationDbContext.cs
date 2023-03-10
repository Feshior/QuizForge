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
        //Quizzes models
        public DbSet<Quiz> Quizzes => Set<Quiz>();
        public DbSet<QuizQuestion> QuizQuestions => Set<QuizQuestion>();
        public DbSet<QuestionAnswer> QuestionAnswers => Set<QuestionAnswer>();
        //************

        //UserModels
        public DbSet<UserPoint> UserPoints => Set<UserPoint>();
        public DbSet<UserQuiz> UserQuizzes => Set<UserQuiz>();
    }
}