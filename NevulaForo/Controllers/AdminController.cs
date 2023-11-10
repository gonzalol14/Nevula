using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NevulaForo.Models.DB;
using System.Security.Claims;

namespace NevulaForo.Controllers
{
    [Authorize]
    [Authorize(Policy = "PolicyAdministrator")]
    public class AdminController : Controller
    {
        private readonly NevulaContext _DBContext;

        public AdminController(NevulaContext dbContext)
        {
            _DBContext = dbContext;
        }

        //PAGINAS
        public IActionResult Users()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            int IdUser = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());

            List<User> lstaccounts = _DBContext.Users
                                    .Include(u => u.UserRoles)
                                    .Where(u => u.DeletedAt == null && u.Id != IdUser)
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

            return View(lstaccounts);
        }
        public IActionResult Publications()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            int IdUser = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());

            List<Publication> lstPosts = _DBContext.Publications
                                            .Include(u => u.IdUserNavigation)
                                                .ThenInclude(u => u.UserRoles)
                                            .Include(c => c.Comments)
                                            .Where(p => p.DeletedAt == null && p.IdUser != IdUser && p.IdUserNavigation.DeletedAt == null )
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

        public IActionResult Comments()
        {
            return View();
        }


        //Acciones
        [HttpPost]
        //Deben enviar mails al usuario al que se le borra su cuenta, su publicacion o su comentario
        public IActionResult DeleteUser()
        {
            return View();
        } 

        [HttpPost]
        public IActionResult DeletePublication()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DeleteComment()
        {
            return View();
        }
    }
}
