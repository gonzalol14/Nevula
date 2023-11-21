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
                                            .Where(p => p.DeletedAt == null && p.IsBanned == null && p.IdUserNavigation.DeletedAt == null && p.IdUserNavigation.IsBanned == null)
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
        public IActionResult Error(int? statusCode = null)
        {
            ViewData["StatusCode"] = statusCode;
            switch (statusCode)
            {
                case 404:
                    ViewData["TitlePage"] = "¡Ups! Página no encontrada.";
                    ViewData["Details"] = "Parece que la página que estás buscando no existe.";
                    break;
                case 403:
                    ViewData["TitlePage"] = "¡Acceso prohibido!";
                    ViewData["Details"] = "Lo sentimos, pero no tienes permisos para acceder a esta página. Si crees que esto es un error, por favor contacta al administrador.";
                    break;
                case 500:
                    ViewData["TitlePage"] = "¡Ups! Algo salió mal en el servidor.";
                    ViewData["Details"] = "Lo sentimos, pero ha ocurrido un error interno en el servidor. Nuestro equipo técnico está trabajando para solucionarlo.";
                    break;
                default:
                    ViewData["TitlePage"] = "¡Ups! Algo salió mal.";
                    ViewData["Details"] = "Ocurrió un error inesperado. Por favor, inténtalo de nuevo más tarde.";
                    break;
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        }
    }
}