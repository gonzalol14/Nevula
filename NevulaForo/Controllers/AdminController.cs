using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NevulaForo.Models.DB;
using System.Security.Claims;
using System.Xml.Linq;

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
        [HttpGet]
        public async Task<JsonResult> VerifyUser(int IdUser, bool isVerified)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            int IdRoleUser = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault());

            UserRole? userRole = _DBContext.UserRoles.FirstOrDefault(ur => ur.IdUser == IdUser);
            if(userRole != null) 
            {
                if (userRole.IdRole == 4)
                {
                    return Json(new { success = false, error = "No es posible modificar roles de super-admins" });
                }
                if(userRole.IdRole == 3 && IdRoleUser != 4)
                {
                    return Json(new { success = false, error = "Solo los super-admins pueden cambiar el rol admin" });
                }

                if (isVerified)
                {
                    //Sacar el verificado
                    userRole.IdRole = 1;
                    userRole.ModifiedAt = DateTime.Now;
                } else
                {
                    //Agregar el verificado
                    userRole.IdRole = 2;
                    userRole.ModifiedAt = DateTime.Now;
                }
                _DBContext.Update(userRole);
                await _DBContext.SaveChangesAsync();

                return Json(new { success = true });
            }

            return Json(new { success = false, error = "Error al intentar cambiar el verificado" });
        }
        [HttpGet]
        public async Task<JsonResult> AdminUser(int IdUser, bool isAdmin)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            int IdRoleUser = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault());

            UserRole? userRole = _DBContext.UserRoles.FirstOrDefault(ur => ur.IdUser == IdUser);
            if (userRole != null)
            {
                if (userRole.IdRole == 4)
                {
                    return Json(new { success = false, error = "No es posible modificar roles de super-admins" });
                }
                if (userRole.IdRole == 3 && IdRoleUser != 4)
                {
                    return Json(new { success = false, error = "Solo los super-admins pueden cambiar el rol admin" });
                }

                if (isAdmin)
                {
                    //Eliminar el rol admin
                    userRole.IdRole = 1;
                    userRole.ModifiedAt = DateTime.Now;
                } else
                {
                    //Agregar el rol admin
                    userRole.IdRole = 3;
                    userRole.ModifiedAt = DateTime.Now;
                }

                _DBContext.Update(userRole);
                await _DBContext.SaveChangesAsync();

                return Json(new { success = true });
            }

            return Json(new { success = false, error = "Error al intentar cambiar el rol de admin" });
        }


        [HttpGet]
        //Deben enviar mails al usuario al que se le borra su cuenta, su publicacion o su comentario
        public async Task<JsonResult> DeleteUser(int IdUser)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            int IdRoleUser = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault());

            User? user = _DBContext.Users.Include(u => u.UserRoles).Where(u => u.DeletedAt == null && u.Id == IdUser).FirstOrDefault();

            if (user != null)
            {
                if (user.UserRoles.First().IdRole == 4)
                {
                    return Json(new { success = false, error = "No es posible eliminar un super-admin" });
                }
                if (user.UserRoles.First().IdRole == 3 && IdRoleUser != 4)
                {
                    return Json(new { success = false, error = "Solo los super-admins pueden eliminar usuarios admins" });
                }

                user.DeletedAt = DateTime.Now.AddDays(-31);

                _DBContext.Update(user);
                await _DBContext.SaveChangesAsync();

                return Json(new { success = true });
            }

            return Json(new { success = false, error = "Error al intentar eliminar usuario" });
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
