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

            List<User> accounts = _DBContext.Users
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
            return View(accounts);
        }
        public IActionResult Publications()
        {
            return View();
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
