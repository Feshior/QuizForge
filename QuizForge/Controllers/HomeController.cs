using Microsoft.AspNetCore.Mvc;
using QuizForge.Models;
using System.Diagnostics;

namespace QuizForge.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}