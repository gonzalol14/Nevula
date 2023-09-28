using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

using NevulaForo.Models;
using NevulaForo.Models.DB;
using NevulaForo.Models.ViewModels;
using System.Security.Claims;
using System.Linq;
using NevulaForo.Services.Contract;
using System;


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
            try
            {
                PublicationDedicatedVM oPublicationDedicated = new PublicationDedicatedVM()
                {
                    oPublication = _DBContext.Publications.Include(u => u.IdUserNavigation).ThenInclude(u => u.UserRoles).Where(p => p.DeletedAt == null && p.Id == IdPublication).ToList().First(),
                    oComments = _DBContext.Comments.Include(u => u.IdUserNavigation).ThenInclude(u => u.UserRoles).Where(c => c.DeletedAt == null && c.IdPublication == IdPublication).OrderByDescending(c => c.CreatedAt).ToList()
                };

                ClaimsPrincipal claimUser = HttpContext.User;
                string username = "";

                if (claimUser.Identity.IsAuthenticated)
                {
                    username = claimUser.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
                }

                ViewData["username"] = username;

                return View(oPublicationDedicated);
            } catch(InvalidOperationException ex)
            {
                //404
                return NotFound();
            }
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Create(CreatePublicationVM viewmodel)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            int IdUser = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());

            if (!ModelState.IsValid)
            {
                return View(viewmodel);
            }


            User user = _DBContext.Users.Where(u => u.DeletedAt == null && u.Id == IdUser).ToList().First();

            Publication model = new Publication()
            {
                IdUser = IdUser,
                Title = viewmodel.Title,
                Description = viewmodel.Description,
                CreatedAt = DateTime.Now,
                DeletedAt = null,
                IdUserNavigation = user

            };

            _DBContext.Add(model);
            await _DBContext.SaveChangesAsync();

            return RedirectToAction("Community", "Home");
        }


        [HttpGet, Authorize]
        public IActionResult Edit(int IdPublication)
        {
            try
            {
                ClaimsPrincipal claimUser = HttpContext.User;
                int IdUser = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());

                Publication modelPublication = _DBContext.Publications.FirstOrDefault(p => p.Id == IdPublication && p.IdUser == IdUser);
                
                if (modelPublication != null)
                {
                    CreatePublicationVM viewmodel = new CreatePublicationVM()
                    {
                        Id = modelPublication.Id,
                        Title = modelPublication.Title,
                        Description = modelPublication.Description
                    };

                    return View(viewmodel);

                } else
                {
                    throw new InvalidOperationException("No existe la publicacion o esta eliminada");
                }

            } catch (Exception ex) 
            {
                //404
                return NotFound();
            }
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Edit(CreatePublicationVM viewmodel)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            int IdUser = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());

            Publication model = _DBContext.Publications.Where(p => p.Id == viewmodel.Id && p.IdUser == IdUser && p.DeletedAt == null).ToList().First();

            if (model != null)
            {

                if (!ModelState.IsValid)
                {
                    return View(viewmodel);
                }

                User user = _DBContext.Users.Where(u => u.DeletedAt == null && u.Id == IdUser).ToList().First();

                model.Title = viewmodel.Title;
                model.Description = viewmodel.Description;

                _DBContext.Update(model);
                await _DBContext.SaveChangesAsync();

                return RedirectToAction("Community", "Home");
            }

            //404
            return NotFound();
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Delete(int IdPublication)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            int idUser = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());

            Publication publication = _DBContext.Publications.FirstOrDefault(p => p.Id == IdPublication && p.IdUser == idUser && p.DeletedAt == null);
            if (publication != null)
            {
                publication.DeletedAt = DateTime.Now;

                _DBContext.Update(publication);
                await _DBContext.SaveChangesAsync();

                return RedirectToAction("Index", "Account");
            }

            //404
            return NotFound();
        }
        //Falta like, dislike. Analizar si crear otro controller para comentarios o hacerlo en este.
    }
}
