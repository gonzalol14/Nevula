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
using System.Text.RegularExpressions;
using NevulaForo.Resources;

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
            PublicationDedicatedVM oPublicationDedicated = new PublicationDedicatedVM()
            {
                // No verifico que el Comments (del oPublication) traiga los comentarios validos (sin eliminar ni usuario creador eliminado), ya que para todo lo relacionado a comentarios uso el oComments
                oPublication = _DBContext.Publications
                                .Include(u => u.IdUserNavigation)
                                    .ThenInclude(u => u.UserRoles)
                                .Where(p => p.DeletedAt == null && p.Id == IdPublication)
                                .FirstOrDefault(),
                oComments = _DBContext.Comments
                                .Include(u => u.IdUserNavigation)
                                    .ThenInclude(u => u.UserRoles)
                                .Include(c => c.IdFatherCommentNavigation)
                                .Where(c => c.DeletedAt == null && c.IdPublication == IdPublication && c.IdUserNavigation.DeletedAt == null)
                                .OrderByDescending(c => c.CreatedAt)
                                .ToList()
            };

            if (oPublicationDedicated.oPublication != null)
            {
                ClaimsPrincipal claimUser = HttpContext.User;
                string username = "";

                if (claimUser.Identity.IsAuthenticated)
                {
                    username = claimUser.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).Select(c => c.Value).SingleOrDefault();
                }

                ViewData["username"] = username;

                return View(oPublicationDedicated);

            } else
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
        public async Task<JsonResult> CreateApi([FromBody] CreatePublicationVM viewmodel)
        {
            int IdUser = Convert.ToInt32(HttpContext.User.FindFirstValue("Id"));

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(x => x.Value.Errors.Any())
                               .ToDictionary(
                                   kvp => kvp.Key,
                                   kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                               );

                return Json(new { success = false, errors = errors });
                //return View(viewmodel);
            }


            User user = _DBContext.Users.Where(u => u.DeletedAt == null && u.Id == IdUser).ToList().First();

            Publication model = new Publication()
            {
                IdUser = IdUser,
                Title = viewmodel.Title,
                Description = Utilities.ReduceLineBreaks(viewmodel.Description),
                CreatedAt = DateTime.Now,
                DeletedAt = null,
                IdUserNavigation = user

            };

            _DBContext.Add(model);
            await _DBContext.SaveChangesAsync();

            return Json(new { success = true, redirectUrl = $"/Account/Index?IdUser={IdUser}" }); 
        }


        [HttpGet, Authorize]
        public IActionResult Edit(int IdPublication)
        {
            try
            {
                int IdUser = Convert.ToInt32(HttpContext.User.FindFirstValue("Id"));

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
        public async Task<JsonResult> EditApi([FromBody] CreatePublicationVM viewmodel)
        {
            int IdUser = Convert.ToInt32(HttpContext.User.FindFirstValue("Id"));

            Publication model = _DBContext.Publications.Where(p => p.Id == viewmodel.Id && p.IdUser == IdUser && p.DeletedAt == null).ToList().First();

            if (model != null)
            {

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Where(x => x.Value.Errors.Any())
                               .ToDictionary(
                                   kvp => kvp.Key,
                                   kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                               );

                    return Json(new { success = false, errors = errors });
                    //return View(viewmodel);
                }

                User user = _DBContext.Users.Where(u => u.DeletedAt == null && u.Id == IdUser).ToList().First();

                model.Title = viewmodel.Title;
                model.Description = viewmodel.Description;

                _DBContext.Update(model);
                await _DBContext.SaveChangesAsync();

                return Json(new { success = true, redirectUrl = $"/Publication/Index?IdPublication={model.Id}" });
            }

            //404
            return Json(new { success = false, errors = new List<string> { "Error al intentar editar publicación" } });
        }

        [HttpGet, Authorize]
        public async Task<JsonResult> DeleteApi(int IdPublication)
        {
            int IdUser = Convert.ToInt32(HttpContext.User.FindFirstValue("Id"));

            Publication publication = _DBContext.Publications.FirstOrDefault(p => p.Id == IdPublication && p.IdUser == IdUser && p.DeletedAt == null);
            if (publication != null)
            {
                publication.DeletedAt = DateTime.Now;

                _DBContext.Update(publication);
                await _DBContext.SaveChangesAsync();

                return Json(new { success = true });
            }

            return Json(new { success = false, error = "Error al intentar eliminar la publicacion" });
        }


        [HttpGet]
        public IActionResult Search(SearchVM viewmodel)
        {

            if (viewmodel.For == "posts")
            {
                viewmodel.Publications = _DBContext.Publications
                                                .Include(u => u.IdUserNavigation)
                                                    .ThenInclude(u => u.UserRoles)
                                                .Include(c => c.Comments)
                                                .Where(p => p.DeletedAt == null && p.IdUserNavigation.DeletedAt == null && EF.Functions.Like(p.Title, $"%{viewmodel.Search}%"))
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
            } else
            {
                viewmodel.Users = _DBContext.Users
                                        .Include(u => u.UserRoles)
                                        .Where(u => u.DeletedAt == null && EF.Functions.Like(u.Username, $"%{viewmodel.Search}%"))
                                        .Select(u => new User
                                        {
                                            Id = u.Id,
                                            Name = u.Name,
                                            Surname = u.Surname,
                                            Username = u.Username,
                                            Description = u.Description,
                                            CreatedAt = u.CreatedAt,
                                            Publications = u.Publications.Where(post => post.DeletedAt == null && post.IdUserNavigation.DeletedAt == null).ToList(),
                                            UserRoles = u.UserRoles
                                        })
                                        .ToList();
            }
            

            ViewData["SearchQuery"] = viewmodel.Search;
            return View(viewmodel);
        }

        //Falta like, dislike. Analizar si crear otro controller para comentarios o hacerlo en este.
    }
}
