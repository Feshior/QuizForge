using Microsoft.EntityFrameworkCore;
using QuizForge.Data;
using QuizForge.Models.QuizModels;

namespace QuizForge.Models
{
    public static class SeedData
    {
        public static void SeedDatabase(ApplicationDbContext context)
        {
            context.Database.Migrate();
            if(context.Quizzes.Count() == 0
                && context.QuizQuestions.Count() == 0 
                && context.QuestionAnswers.Count() == 0)
            {
                QuizQuestion qz1= new QuizQuestion()
                {
                    Question = "Who is the main character?",
                    QuestionPoints = 20,
                    QuestionImage = "/img/user_images/holo.webp"
                };

                QuizQuestion qz2 = new QuizQuestion()
                {
                    Question = "Where are Holo from?",
                    QuestionPoints = 20,
                    QuestionImage = "/img/user_images/mountains.jpg"
                };

                QuizQuestion qz3 = new QuizQuestion()
                {
                    Question = "Who is on this image?",
                    QuestionPoints = 30,
                    QuestionImage = "/img/user_images/spice_and_wolf_pano.jpeg"
                };

                context.QuizQuestions.AddRange(qz1, qz2, qz3);
                context.SaveChanges();

                List<QuestionAnswer> qu1Answers = new List<QuestionAnswer>()
                {
                    new QuestionAnswer()
                {
                    Answer = "Holo",
                    IsCorrect = true,
                    QuizQuestion = qz1
                },
                        new QuestionAnswer()
                {
                    Answer = "Richten Marlheit",
                    IsCorrect = false,
                    QuizQuestion = qz1
                },
                            new QuestionAnswer()
                {
                    Answer = "ExampleAnswer",
                    IsCorrect = true,
                    QuizQuestion = qz1
                },
                                new QuestionAnswer()
                {
                    Answer = "ExampleAnswer2",
                    IsCorrect = true,
                    QuizQuestion = qz1
                }
            };
                //2--
                QuestionAnswer qu2Answer = new QuestionAnswer()
                {
                    Answer = "Yoitsu",
                    IsCorrect = true,
                    QuizQuestion = qz2
                };

                //3--
                List<QuestionAnswer> qu3Answers = new List<QuestionAnswer>()
                {
                    new QuestionAnswer()
                {
                    Answer = "Holo",
                    IsCorrect = true,
                    QuizQuestion = qz3
                },
                        new QuestionAnswer()
                {
                    Answer = "Kraft Lawrence",
                    IsCorrect = true,
                    QuizQuestion = qz3
                },
                            new QuestionAnswer()
                {
                    Answer = "ExampleAnswer",
                    IsCorrect = false,
                    QuizQuestion = qz3
                },
                                new QuestionAnswer()
                {
                    Answer = "Norah",
                    IsCorrect = true,
                    QuizQuestion = qz3
                }
            };

                foreach (var item in qu1Answers)
                    context.QuestionAnswers.Add(item);

                context.QuestionAnswers.Add(qu2Answer);

                foreach (var item in qu3Answers)
                    context.QuestionAnswers.Add(item);

                context.SaveChanges();

                Quiz quiz1 = new Quiz()
                {
                    QuizName = "Spice and Wolf knoweledge test",
                    QuizPoints = 100,
                    QuizImage = "/img/user_images/holo.webp",
                    MaxAttempts = 10,
                    QuizQuestions = new List<QuizQuestion>() { qz1, qz2, qz3}
                };

                context.Quizzes.Add(quiz1);
                context.SaveChanges();
            }
        }
    }
}
