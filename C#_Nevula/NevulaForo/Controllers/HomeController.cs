using Microsoft.AspNetCore.Mvc;
using NevulaForo.Models;
using NevulaForo.Models.DB;
using System.Diagnostics;
using System.Security.Claims;

namespace NevulaForo.Controllers
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
            ClaimsPrincipal claimUser = HttpContext.User;
            string username = "";

            if (claimUser.Identity.IsAuthenticated)
            {
                username = claimUser.Claims.Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault();
            }

            ViewData["username"] = username;

            return View();
        }

        public IActionResult Community()
        {
            // TIENE UN ERROR, ARREGLARLO URGENTEMENTE
            /*List<Publication> lst = new List<Publication>();

            using(var db = new NevulaContext())
            {
                lst = (from d in db.Publications
                       where d.DeletedAt == null
                       select d).ToList();
            }
            return View(lst);*/
            return View();
        }

        public IActionResult News()
        {
            return View();
        }

        public IActionResult Help()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}