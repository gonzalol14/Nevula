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
        private readonly ILogger<HomeController> _logger;
        private readonly NevulaContext _DBContext;

        public HomeController(ILogger<HomeController> logger, NevulaContext dbContext)
        {
            _logger = logger;
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
                    username = claimUser.FindFirstValue(ClaimTypes.NameIdentifier);
                }
                ViewData["username"] = username;
            }

            return View();
        }

        public IActionResult Community()
        {
            List<Publication> lstPosts = _DBContext.Publications
                                            .Include(u => u.IdUserNavigation)
                                                .ThenInclude(u => u.UserRoles)
                                            .Include(c => c.Comments)
                                            .Where(p => p.DeletedAt == null && p.IdUserNavigation.DeletedAt == null && p.IdUserNavigation.IsBanned == null)
                                            .Select(p => new Publication
                                            {
                                                Id = p.Id,
                                                IdUser = p.IdUser,
                                                Title = p.Title,
                                                Description = p.Description,
                                                CreatedAt = p.CreatedAt,
                                                Comments = p.Comments.Where(comment => comment.DeletedAt == null && comment.IdUserNavigation.DeletedAt == null).ToList(),
                                                IdUserNavigation = p.IdUserNavigation
                                            })
                                            .OrderByDescending(p => p.CreatedAt)
                                            .ToList();
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
        public IActionResult Error(int statusCode)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

            if (statusCode == 404)
            {
                return View("Error404");
            } else
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }


        }
    }
}