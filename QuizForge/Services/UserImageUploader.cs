using Microsoft.Extensions.Logging;
using QuizForge.Controllers;

namespace QuizForge.Services
{
    public interface IUserImageUploader
    {

        public Task<string[]> UploadImage(IFormFile file, string userEmail);
        string[] getAllowedExtensions();

    }

    public class UserImageUploader : IUserImageUploader
    {
        private readonly string[] allowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".apng" };
        private readonly IWebHostEnvironment hostingEnvironment;
        private readonly string webRootPath;
        private ILogger<UserImageUploader> logger;


        public UserImageUploader(IWebHostEnvironment hostingEnvironment, ILogger<UserImageUploader> logger)
        {
            this.hostingEnvironment = hostingEnvironment;
            webRootPath = hostingEnvironment.WebRootPath;
            this.logger = logger;
        }

        public string[] getAllowedExtensions()
        {
            return allowedImageExtensions;
        }

        //Returning two values, first is the exit code, 0 ok, -1 error
        //Second value is error message or relative file path
        public async Task<string[]> UploadImage(IFormFile file, string userEmail)//IFormFile file, string userEmail)
        {
            if (!checkImageExtension(file))
            {
                return new string[2] { "-1", "Invalid file name. Please, try again with the different name" };
            }

            string newFileName = GetUniqueFileName(file.FileName);

            //Generating path to the file
            string SystemPathUserUploads = Path.Combine(webRootPath, $"img/user_images/{userEmail}/");
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
                    return new string[2] { "-1", "Invalid file name. Please, try again with the different name" };
                i++;
            }

            //Creating directory with user email if user wasn't uploading any files before
            if (!Directory.Exists(SystemPathUserUploads))
                Directory.CreateDirectory(SystemPathUserUploads);

            //Moving file from system temp directory to our local directory
            using (var stream = new FileStream(fileSystemPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            //Logging data
            //logger.LogInformation($"Created file at {fileSystemPath} from user {userEmail}");

            string localFilePath = Path.Combine($"/img/user_images/{userEmail}/", newFileName);

            return new string[2] { "0", localFilePath };
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
