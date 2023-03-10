using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizForge.Data;
using QuizForge.Models.UserModels;
using QuizForge.Models.QuizModels;
using QuizForge.ViewModels;
using QuizForge.Models;
using System.Configuration;

namespace QuizForge.Controllers
{
    public class QuizzesController : Controller
    {
        ApplicationDbContext dbContext;
        SignInManager<ApplicationUser> SignInManager;
        public QuizzesController(ApplicationDbContext dbContext, SignInManager<ApplicationUser> SignInManager)
        {
            this.dbContext = dbContext;
            this.SignInManager = SignInManager;
        }

        public async Task<IActionResult> Index()
        {
           
            if (SignInManager.IsSignedIn(User))
            {
                
                string userEmail = User?.Identity?.Name ?? "";
                //foreach(var item in dbContext.UserPoints.Where(p => (p.UserQuiz.ApplicationUser.Email == userEmail)).ToList()){
                //    Console.WriteLine($"{item.Points} points");
                //}
                return View(new QuizListViewModel()
                {
                    Quizzes = await dbContext.Quizzes.Include(q=>q.UserQuizzes).Include(q=>q.QuizQuestions).ToListAsync(),
                    UserPoints = await dbContext.UserPoints.Where(p => (p.UserQuiz.ApplicationUser.Email == userEmail)).ToListAsync()
            });
            }
            else
            {
                return View(new QuizListViewModel()
                {
                    Quizzes = await dbContext.Quizzes.ToListAsync()
                });
            }
           
        }

        [HttpGet]
        public IActionResult PassTest(int quizId = -1)
        {
            if (quizId != -1)
            {
                Quiz? quizToPass = dbContext.Quizzes.Where(q => q.Id == quizId)
                    .Include(q => q.QuizQuestions).FirstOrDefault();
                
                if (quizToPass == null)
                    return View(new QuizPassViewModel());

                int attempts = dbContext.UserPoints.Where(up => up.UserQuiz == dbContext
                    .UserQuizzes.FirstOrDefault(uq => uq.Quiz == quizToPass))
                    .Count();


               if(attempts >= quizToPass.MaxAttempts)
               {
                    return View(new QuizPassViewModel()
                    {
                        Quiz = quizToPass,
                        CurrentAttempts = attempts,
                        IsAvaible = false
                    });
                }
                
                List<QuizQuestion> questionsToPass = quizToPass.QuizQuestions.ToList();
                List<QuestionAnswer> questionsAnswers = dbContext.QuestionAnswers.Where(q=>q.QuizQuestion.QuizId == quizId).ToList();

                return View(new QuizPassViewModel()
                {
                    Quiz = quizToPass,
                    CurrentAttempts = attempts,
                    IsAvaible = true,
                    QuizQuestions = questionsToPass,
                    QuizQuestionsAnswers = questionsAnswers
                });
            }
            return View(new QuizPassViewModel());
        }

        [HttpPost]
        public IActionResult PassPost()
        {
            /* Short description about how it works
             * We are reciving post request from server and parcing it in results list
             * 
             * elementData contains data in this way:
             *  {QuestionId}, {answ1}, {answ2}, {answ3}, {...}, {...}...
             *  
             *  So, text inputs and select inputs will contain only 2 elements
             *  and multiple choice can contain an endless number of elements
             *  
             *  Because of each section of data is divided and contains QuestionId we can easily
             *  align it with data base elements
             */

            

            int currentI = 0;
            //int currentSw = -1;
            List<List<string>> results = new List<List<string>>();
            var form = HttpContext.Request.Form;

            try
            {

                int quizId = int.Parse(form["quiz-id"]);

                while (true)
                {

                    //If there are no more elements breaking loop
                    if (!form.ContainsKey($"r-{currentI}--1"))
                        break;

                    string[] keys = form.Keys.Where(k => k.StartsWith($"r-{currentI}-")).ToArray();

                    List<string> elementData = new List<string>();
                    //elementData.Add(form[$"r-{currentI}--1"]);

                    foreach(string key in keys)
                    {
                        elementData.Add(form[key]);
                    }

                    results.Add(elementData);
                    currentI++;
                }

                foreach(var item in results)
                {
                    Console.WriteLine("ITEMS: ");
                    foreach(var res in item)
                    {
                        Console.Write($" {res} ");
                    }
                }

                return CalculateResults(quizId, results);

            }
            catch (Exception)
            {
                return View("Error", new ErrorViewModel()
                {
                    RequestId = "400"
                });
            }
        }

        //Helper method for PassPost
        [NonAction]
        private IActionResult CalculateResults(int quizId, List<List<string>> answers)
        {
            Quiz? passedQuiz = dbContext.Quizzes.Where(q => q.Id == quizId).FirstOrDefault();
            if (passedQuiz == null) throw new Exception("Quiz was not found!");
            double userPoints = 0;
            //Checking all answers
            foreach (List<string> answer in answers)
            {
                int questionId = int.Parse(answer[0]);
                QuizQuestion q = dbContext.QuizQuestions.Where(q => q.Id == questionId).Include(q => q.QuizAnswers).FirstOrDefault() ?? new QuizQuestion();

                //Calculation point that will be given for one correct answer
                double pointForOneAnswer;
                int questionCount = (q.QuizAnswers.Where(q => q.IsCorrect == true).Count());
                if (questionCount == 0)
                    pointForOneAnswer = 0;
                else
                    pointForOneAnswer = q.QuestionPoints / questionCount;

                //Calculating points
                for (int i = 0; i < answer.Count; i++)
                {
                    if (q.QuizAnswers.Where(q => q.IsCorrect == true && q.Answer == answer[i]).Count() > 0)
                    {
                        userPoints += pointForOneAnswer;
                    }
                }

               
            }
            //Adding results
            if (SignInManager.IsSignedIn(User))
                {
                string userEmail = User?.Identity?.Name ?? "";
                ApplicationUser? appUser = dbContext.Users.Where(u => u.Email == userEmail).FirstOrDefault();

                if (appUser == null)
                    throw new Exception("User was not found!");

                UserQuiz? uq = dbContext.UserQuizzes.Where(u => u.ApplicationUser == appUser && u.QuizId == quizId).FirstOrDefault();
                if (uq == null)
                {
                   uq = new UserQuiz()
                   {
                      ApplicationUser = appUser,
                      Quiz = passedQuiz
                   };
                    dbContext.Add(uq);
                    dbContext.SaveChanges();
                }
                

                UserPoint userP = new UserPoint()
                {
                    Points = userPoints,
                     UserQuiz = uq
                };

                dbContext.UserPoints.Add(userP);
                dbContext.SaveChanges();
            }
            else
                throw new Exception("User unauthorized!");  
           


            return View("QuizResult", new QuizResultViewModel()
            {
                Quiz = passedQuiz,
                Points = userPoints
            });
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
