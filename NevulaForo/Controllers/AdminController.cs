using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NevulaForo.Models.DB;
using NevulaForo.Models.ViewModels;
using NevulaForo.Services.Contract;
using System.Security.Claims;
using System.Xml.Linq;

namespace NevulaForo.Controllers
{
    [Authorize]
    [Authorize(Policy = "PolicyAdministrator")]
    public class AdminController : Controller
    {
        private readonly NevulaContext _DBContext;
        private readonly IEmailSenderService _emailSenderService;

        public AdminController(NevulaContext dbContext, IEmailSenderService emailService)
        {
            _DBContext = dbContext;
            _emailSenderService = emailService;
        }

        //PAGINAS
        public IActionResult Users(int? IdUser)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            int IdUserAdmin = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());

            List<User> lstaccounts = new List<User>();
            if (IdUser == null || IdUser == 0)
            {
                lstaccounts = _DBContext.Users
                                        .Include(u => u.UserRoles)
                                        .Where(u => u.Id != IdUserAdmin && u.DeletedAt == null)
                                        .Select(u => new User
                                        {
                                            Id = u.Id,
                                            Name = u.Name,
                                            Surname = u.Surname,
                                            Username = u.Username,
                                            Description = u.Description,
                                            CreatedAt = u.CreatedAt,
                                            IsBanned = u.IsBanned,
                                            UserRoles = u.UserRoles
                                        })
                                        .ToList();

            } else
            {
                lstaccounts = _DBContext.Users
                                        .Include(u => u.UserRoles)
                                        .Where(u => u.Id == IdUser && u.DeletedAt == null)
                                        .Select(u => new User
                                        {
                                            Id = u.Id,
                                            Name = u.Name,
                                            Surname = u.Surname,
                                            Username = u.Username,
                                            Description = u.Description,
                                            CreatedAt = u.CreatedAt,
                                            IsBanned = u.IsBanned,
                                            UserRoles = u.UserRoles
                                        })
                                        .ToList();
            }

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
                                            .Where(p => p.DeletedAt == null && p.IsBanned == null && p.IdUser != IdUser && p.IdUserNavigation.DeletedAt == null && p.IdUserNavigation.IsBanned == null)
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

        public IActionResult Comments(int IdPublication)
        {
            PublicationDedicatedVM oPublicationDedicated = new PublicationDedicatedVM()
            {
                // No verifico que el Comments (del oPublication) traiga los comentarios validos (sin eliminar ni usuario creador eliminado), ya que para todo lo relacionado a comentarios uso el oComments
                oPublication = _DBContext.Publications
                                .Include(u => u.IdUserNavigation)
                                    .ThenInclude(u => u.UserRoles)
                                .Where(p => p.Id == IdPublication && p.DeletedAt == null && p.IsBanned == null && p.IdUserNavigation.DeletedAt == null && p.IdUserNavigation.IsBanned == null)
                                .FirstOrDefault(),
                oComments = _DBContext.Comments
                                .Include(u => u.IdUserNavigation)
                                    .ThenInclude(u => u.UserRoles)
                                .Include(c => c.IdFatherCommentNavigation)
                                .Where(c => c.DeletedAt == null && c.IdPublication == IdPublication && c.IdUserNavigation.DeletedAt == null && c.IdUserNavigation.IsBanned == null)
                                .OrderByDescending(c => c.CreatedAt)
                                .ToList()
            };

            if (oPublicationDedicated.oPublication != null)
            {
                return View(oPublicationDedicated);
            }
            else
            {
                //404 para admins
                return View();
            }
        }


        //Acciones
        [HttpGet]
        public async Task<JsonResult> VerifyUser(int IdUser, bool isVerified)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            int IdRoleUser = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault());

            UserRole? userRole = _DBContext.UserRoles.Include(ur => ur.IdUserNavigation).FirstOrDefault(ur => ur.IdUser == IdUser && ur.IdUserNavigation.DeletedAt == null && ur.IdUserNavigation.IsBanned == null);
            if(userRole != null) 
            {
                if (userRole.IdRole == 4)
                {
                    return Json(new { success = false, error = "No es posible cambiar indirectamente un super-admins" });
                }
                if(userRole.IdRole == 3 && IdRoleUser != 4)
                {
                    return Json(new { success = false, error = "Solo los super-admins pueden cambiar el rol admin" });
                }

                MailRequestVM mail = new MailRequestVM() { Email = userRole.IdUserNavigation.Email };
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

                    mail.Subject = "¡Su cuenta de Nevula ha sido verificada!";
                    mail.Body = $"Desde el día de hoy su cuenta de Nevula <b>@{userRole.IdUserNavigation.Username}</b> se encuentra verificada para los demás usuarios. <br> Equipo de Nevula";
                }

                await _emailSenderService.SendEmailAsync(mail);
                _DBContext.Update(userRole);
                await _DBContext.SaveChangesAsync();

                return Json(new { success = true });
            }

            return Json(new { success = false, error = "No se pudo encontrar el usuario (debe encontrarse desbaneado)" });
        }
        [HttpGet]
        public async Task<JsonResult> AdminUser(int IdUser)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            int IdRoleUser = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault());
            if (IdRoleUser != 4)
            {
                return Json(new { success = false, error = "Solo los super-admins pueden dar/sacar el rol admin" });
            }

            UserRole? userRole = _DBContext.UserRoles.Include(ur => ur.IdUserNavigation).FirstOrDefault(ur => ur.IdUser == IdUser && ur.IdUserNavigation.DeletedAt == null && ur.IdUserNavigation.IsBanned == null);
            if (userRole != null)
            {
                if (userRole.IdRole == 4 || IdRoleUser != 4)
                {
                    return Json(new { success = false, error = "No es posible cambiar indirectamente un super-admins" });
                }

                if (userRole.IdRole == 3)
                {
                    //Eliminar el rol admin
                    userRole.IdRole = 1;
                    userRole.ModifiedAt = DateTime.Now;
                } else if(userRole.IdRole < 3) 
                {
                    //Agregar el rol admin
                    userRole.IdRole = 3;
                    userRole.ModifiedAt = DateTime.Now;
                }

                _DBContext.Update(userRole);
                await _DBContext.SaveChangesAsync();

                return Json(new { success = true });
            }

            return Json(new { success = false, error = "No se pudo encontrar el usuario (debe encontrarse desbaneado)" });
        }
        [HttpGet]
        public async Task<JsonResult> SuperAdminUser(int IdUser)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            int IdRoleUser = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault());
            if (IdRoleUser != 4)
            {
                return Json(new { success = false, error = "Solo los super-admins pueden dar/sacar el rol admin" });
            }

            UserRole? userRole = _DBContext.UserRoles.Include(ur => ur.IdUserNavigation).FirstOrDefault(ur => ur.IdUser == IdUser && ur.IdUserNavigation.DeletedAt == null && ur.IdUserNavigation.IsBanned == null);
            if (userRole != null)
            {
                if (userRole.IdRole == 4)
                {
                    //Eliminar el rol admin
                    userRole.IdRole = 1;
                    userRole.ModifiedAt = DateTime.Now;
                }
                else if (userRole.IdRole < 4)
                {
                    //Agregar el rol admin
                    userRole.IdRole = 4;
                    userRole.ModifiedAt = DateTime.Now;
                }

                _DBContext.Update(userRole);
                await _DBContext.SaveChangesAsync();

                return Json(new { success = true });
            }

            return Json(new { success = false, error = "No se pudo encontrar el usuario (debe encontrarse desbaneado)" });
        }

        [HttpGet]
        //Deben enviar mails al usuario al que se le borra su cuenta, su publicacion o su comentario
        public async Task<JsonResult> DeleteUser(int IdUser)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            int IdRoleUser = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault());

            User? user = _DBContext.Users.Include(u => u.UserRoles).Where(u => u.Id == IdUser && u.DeletedAt == null).FirstOrDefault();

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

                string msj;
                MailRequestVM mail = new MailRequestVM() { Email = user.Email };
                if(user.IsBanned == null)
                {
                    user.IsBanned = true;
                    msj = "Se baneo el usuario con éxito";

                    mail.Subject = "Su cuenta de Nevula ha sido baneada";
                    mail.Body = "Hemos baneado su cuenta de Nevula ya que infringe las normas de la comunidad, por lo que, ya no podra utilizarla hasta nuevo aviso. <br> Equipo de Nevula";

                } else
                {
                    user.IsBanned = null;
                    msj = "Se desbaneo el usuario con éxito";

                    mail.Subject = "¡Su cuenta de Nevula ha sido desbaneada!";
                    mail.Body = "El día de hoy hemos decidido desbanear su cuenta de Nevula, por lo que, la podra usar nuevamente sin problemas y seguira teniendo sus publicaciones y comentarios realizados en la comunidad. <br> Equipo de Nevula";
                }
                
                await _emailSenderService.SendEmailAsync(mail);

                _DBContext.Update(user);
                await _DBContext.SaveChangesAsync();

                return Json(new { success = true, msj = msj, isBanned = user.IsBanned });
            }

            return Json(new { success = false, error = "Error al intentar eliminar usuario" });
        } 

        [HttpGet]
        public async Task<JsonResult> DeletePublication(int IdPublication)
        {
            Publication? post = _DBContext.Publications.Include(p => p.IdUserNavigation).Where(p => p.Id == IdPublication && p.DeletedAt == null).FirstOrDefault();
            if (post != null)
            {
                if (post.IsBanned != null)
                {
                    return Json(new { success = false, error = "La publicación ya se encuentra baneada" });

                } else
                {
                    post.IsBanned = true;

                    MailRequestVM mail = new MailRequestVM() { Email = post.IdUserNavigation.Email };
                    mail.Subject = "Se ha baneado una de tus publicaciones";
                    mail.Body = $"Hemos decidido banear la publicación '{post.Title}' ya que infringe con nuestras normas de comunidad, por ende, los usuarios de Nuevula ya no la veran en la pagina. Tenga cuidado ya que podriamos banear su cuenta si sigue desacatando las normas. <br> Equipo de Nevula";
                    await _emailSenderService.SendEmailAsync(mail);

                    _DBContext.Update(post);
                    await _DBContext.SaveChangesAsync();

                    return Json(new { success = true });
                }

            }
            return Json(new { success = false, error = "Error al intentar eliminar publicación" });
        }

        [HttpGet]
        public async Task<JsonResult> DeleteComment(int IdComment)
        {
            Comment? comment = _DBContext.Comments.FirstOrDefault(p => p.Id == IdComment && p.DeletedAt == null);

            if (comment != null)
            {
                comment.DeletedAt = DateTime.Now;

                _DBContext.Update(comment);
                await _DBContext.SaveChangesAsync();

                return Json(new { success = true });
            }

            return Json(new { success = false, error = "Error al intentar eliminar el comentario" });
        }
    }
}
