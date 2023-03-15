using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using QuizForge.Data;
using QuizForge.Models.QuizModels;
using QuizForge.Models.UserModels;
using QuizForge.ViewModels;
namespace QuizForge.Controllers
{
    public class QuizzesEditorController : Controller
    {
        SignInManager<ApplicationUser> SignInManager { get; set; }
        ApplicationDbContext context { get; set; }
        ILogger logger { get; set; }
        private readonly string[] allowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".apng" };
        private readonly string defaultQuizImageLocation = "/img/default/quiz_default.jpg";
        private readonly string adminEmail = "holo@gmail.com";
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly string webRootPath;
        public QuizzesEditorController(ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager,
            ILogger<QuizzesEditorController> logger,
            IWebHostEnvironment hostingEnvironment)
        {
            this.context = context;
            this.SignInManager = signInManager;
            this.logger = logger;
            this.hostingEnvironment = hostingEnvironment;
            webRootPath = hostingEnvironment.WebRootPath;
        }

        public IActionResult Index()
        {
            if (SignInManager.IsSignedIn(User) && User?.Identity?.Name == "holo@gmail.com")
            {
                return View(new AddQuizViewModel()
                {
                    //Quizzes = context.Quizzes.ToList()
                });
            }
            return View("404");
        }

        [HttpPost] public IActionResult IndexPost(int id = -1)
        {
            if (!(SignInManager.IsSignedIn(User) && User?.Identity?.Name == "holo@gmail.com"))
                return Forbid("User is not authorized or doesn't have enough permissions");
            try
            {
                Quiz? quizToDelete = context.Quizzes.Where(q => q.Id == id).FirstOrDefault();
                if (quizToDelete == null) return NotFound("Quiz was not founded. If you sure that this quiz exist, try again later");
                string quizImage = quizToDelete.QuizImage;

                List<string> questionsImages = new List<string>();
                List<QuizQuestion> quizQuestions = context.QuizQuestions.Where(qq => qq.QuizId == id).Include(qq => qq.QuizAnswers).ToList();
                List<UserQuiz> userQuizes = context.UserQuizzes.Where(uq => uq.QuizId == id).Include(uq => uq.UserPoints).ToList();
                foreach (var item in quizQuestions)
                {
                    string fileToDelete = Path.Combine(webRootPath, $"{item.QuestionImage}");
                    questionsImages.Add(fileToDelete);
                    context.QuestionAnswers.RemoveRange(item.QuizAnswers);
                    context.Remove(item);
                }
                foreach (var item in userQuizes)
                {
                    context.UserPoints.RemoveRange(item.UserPoints);
                    context.UserQuizzes.Remove(item);
                }
                context.Quizzes.Remove(quizToDelete);
                context.SaveChanges();


                //----------Deleting images----------
                //Deleting quiz image
                if (!quizImage.StartsWith("/img/default/"))
                {
                    string fileToDelete = Path.Combine(webRootPath, $"{quizImage}");
                    if (System.IO.File.Exists(fileToDelete))
                    {
                        System.IO.File.Delete(fileToDelete);
                        logger.LogInformation($"File deleted - {fileToDelete}; User - {User.Identity.Name}");
                    }
                }

                //Deleting questions images
                foreach(string item in questionsImages)
                {
                    if (System.IO.File.Exists(item))
                    {
                        System.IO.File.Delete(item);
                        logger.LogInformation($"File deleted - {item}; User - {User.Identity.Name}");
                    }
                }
                //--------------------

                return Ok("Quiz deleted successfully");
            }catch(IOException io)
            {
                logger.LogError(io.Message);
                return StatusCode(500,"Quiz deleted, but internal server error occured");
            }
            catch (Exception ex)
            {
                return NotFound("Internal server error occured while deleting this Quiz. Try again later " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> QuizzesEditorPost(AddQuizViewModel vm)
        {
            if (User == null || !SignInManager.IsSignedIn(User) ||  User?.Identity?.Name != adminEmail)
                return Forbid("User is not authorized or doesn't have enough permissions");

            logger.LogInformation("POST request");
            if (ModelState.IsValid)
            {
                string imageLocation = defaultQuizImageLocation;
                if(vm.QuizImage != null)
                {
                    if (!checkImageExtension(vm.QuizImage))
                        return BadRequest("File is not an image!");


                    string newFileName = GetUniqueFileName(vm.QuizImage.FileName);

                    //Generating path to the file
                    string SystemPathUserUploads = Path.Combine(webRootPath, $"img/user_images/{User.Identity.Name}/");
                    string fileSystemPath = Path.Combine(SystemPathUserUploads, newFileName);

                    //In case if file with this Guid already exist
                    // (Almoust impossible)
                    int i = 0;
                    while (System.IO.File.Exists(fileSystemPath))
                    {
                        newFileName = GetUniqueFileName(newFileName);
                        fileSystemPath = Path.Combine(SystemPathUserUploads, newFileName);
                        //Max 5 attempts
                        if (i > 5)
                            return BadRequest("Invalid file name");
                        i++;
                    }

                    if (!Directory.Exists(SystemPathUserUploads))
                        Directory.CreateDirectory(SystemPathUserUploads);

                    await vm.QuizImage.CopyToAsync(new FileStream(fileSystemPath, FileMode.Create));
                    logger.LogInformation($"Created file at {fileSystemPath} from user {User.Identity.Name}");

                    string localFilePath = Path.Combine($"img\\user_images\\{User.Identity.Name}\\", newFileName);

                    imageLocation = localFilePath;
                }

                //Creating Quiz object and 
                Quiz newQuiz = new Quiz();
                newQuiz.QuizImage = imageLocation;
                newQuiz.QuizName = vm.QuizName.Trim();
                newQuiz.MaxAttempts = vm.QuizMaxAttempts;
                context.Quizzes.Add(newQuiz);
                context.SaveChanges();
                return Ok("Quiz successfully added!");
            }
            return BadRequest(ModelState);
        }
        private bool checkImageExtension(IFormFile file)
        {
            return allowedImageExtensions.Contains(Path.GetExtension(file.FileName).ToLower());
        }

        private string GetUniqueFileName(string inputFileName)
        {
            string fileName = Path.GetFileName(inputFileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_" + Guid.NewGuid().ToString().Substring(0, 5)
                      + Path.GetExtension(fileName);
        }
    }
}

