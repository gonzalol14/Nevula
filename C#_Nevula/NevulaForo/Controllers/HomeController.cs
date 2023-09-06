using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

using NevulaForo.Models;
using NevulaForo.Models.DB;

namespace NevulaForo.Controllers
{
    public class HomeController : Controller
    {
        /*private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        private readonly NevulaContext _DBContext;

        public HomeController(NevulaContext dbContext)
        {
            _DBContext = dbContext;
        }

        public IActionResult Index()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            
            if (claimUser.Identity != null)
            {
                string username = "";
                if (claimUser.Identity.IsAuthenticated)
                {
                    username = claimUser.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();

                }
                ViewData["username"] = username;
            }

            return View();
        }

        public IActionResult Community()
        {
            List<Publication> lstPosts = _DBContext.Publications.Include(u => u.IdUserNavigation).ThenInclude(u => u.UserRoles).Include(c => c.Comments).Where(p => p.DeletedAt == null).ToList();
            return View(lstPosts);

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