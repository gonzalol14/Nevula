using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using NevulaForo.Models;
using NevulaForo.Models.DB;
using NevulaForo.Models.ViewModels;
using System.Security.Claims;
using System.Linq;

namespace NevulaForo.Controllers
{
    public class PublicationController : Controller
    {

        private readonly NevulaContext _DBContext;

        public PublicationController(NevulaContext dbContext)
        {
            _DBContext = dbContext;
        }


        [HttpGet]
        public IActionResult Index(int IdPublication)
        {
            //Publication PostAndComments = _DBContext.Publications.Include(u => u.IdUserNavigation).Include(c => c.Comments).Where(p => p.DeletedAt == null && p.Id == IdPublication).ToList().First();
            
            PublicationDedicatedVM oPublicationDedicated = new PublicationDedicatedVM()
            {
                oPublication = _DBContext.Publications.Include(u => u.IdUserNavigation).Where(p => p.DeletedAt == null && p.Id == IdPublication).ToList().First(),
                oComments = _DBContext.Comments.Include(u => u.IdUserNavigation).Where(c => c.DeletedAt == null && c.IdPublication == IdPublication).ToList()
            };

            ClaimsPrincipal claimUser = HttpContext.User;
            string username = "";

            if (claimUser.Identity.IsAuthenticated)
            {
                username = claimUser.Claims.Where(c => c.Type == ClaimTypes.Name).Select(c => c.Value).SingleOrDefault();
            }

            ViewData["username"] = username;

            return View(oPublicationDedicated);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Create(Publication model)
        {
            //INCOMPLETO

            ClaimsPrincipal claimUser = HttpContext.User;
            string id = claimUser.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault();

            model.IdUser = Convert.ToInt32(id);

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            _DBContext.Add(model);
            await _DBContext.SaveChangesAsync();

            return RedirectToAction("Community", "Home");
        }

        
        [HttpGet, Authorize]
        public IActionResult Edit(int IdPublication)
        {
            if(IdPublication != 0)
            {

                ClaimsPrincipal claimUser = HttpContext.User;
                string id = claimUser.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault();

                Publication publication = _DBContext.Publications.FirstOrDefault(p => p.Id == IdPublication && p.IdUser == Convert.ToInt32(id));
                if (publication != null)
                {
                    return View(publication);
                }

            } 

            return RedirectToAction("Index", "Account");
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Edit(Publication model)
        {
            //INCOMPLETO

            ClaimsPrincipal claimUser = HttpContext.User;
            string id = claimUser.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault();

            model.IdUser = Convert.ToInt32(id);

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            _DBContext.Update(model);
            await _DBContext.SaveChangesAsync();

            return RedirectToAction("Community", "Home");
        }


        public IActionResult Delete()
        {
            return View();
        }
        //Falta like, dislike. Analizar si crear otro controller para comentarios o hacerlo en este.
    }
}
