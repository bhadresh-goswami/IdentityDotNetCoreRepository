using Demo_Repo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Demo_Repo.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
           
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("");
            }
            return View();
        }

        public IActionResult Privacy()
        {
            if (User == null)
            {
                return RedirectToAction("");
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            if (User == null)
            {
                return RedirectToAction("");
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}