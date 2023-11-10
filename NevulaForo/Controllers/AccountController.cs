using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NevulaForo.Models.DB;
using NevulaForo.Models.ViewModels;
using NevulaForo.Resources;
using System.Security.Claims;
using NevulaForo.Services.Contract;

namespace NevulaForo.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private readonly NevulaContext _DBContext;
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IUserService _userService;

        public AccountController(NevulaContext dbContext, IWebHostEnvironment hostingEnvironment, IUserService userService)
        {
            _DBContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index(int IdUser)
        {
            UserProfileVM oUserProfile = new UserProfileVM()
            {
                oUser = _DBContext.Users.Include(u => u.UserRoles).Where(u => u.Id == IdUser && u.DeletedAt == null && u.IsBanned == null).FirstOrDefault(),
                oPublications = _DBContext.Publications
                                        .Include(u => u.IdUserNavigation)
                                            .ThenInclude(u => u.UserRoles)
                                        .Include(c => c.Comments)
                                        .Where(p => p.DeletedAt == null && p.IdUser == IdUser)
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
                                        .ToList()
            };

            if(oUserProfile.oUser != null)
            {
                string profilePicPath = _userService.GetUserProfileImagePath(IdUser);
                ViewBag.ProfilePicPath = profilePicPath;

                return View(oUserProfile);
            } else
            {
                //404
                return NotFound();
            }
        }


        [HttpGet]
        public IActionResult Edit(string type = "General")
        {
            int IdUser = Convert.ToInt32(HttpContext.User.FindFirstValue("Id"));

            if (type == "General")
            {

                User user = _DBContext.Users.Where(u => u.Id == IdUser && u.DeletedAt == null && u.IsBanned == null).FirstOrDefault();

                GeneralEditUserVM viewmodel = new GeneralEditUserVM()
                {
                    Id = IdUser,
                    Email = user.Email,
                    Surname = user.Surname,
                    Username = user.Username,
                    Name = user.Name,
                    Description = user.Description
                };
                return View($"~/Views/Account/Edit/{type}.cshtml", viewmodel);

            } else if (type == "Password")
            {
                ChangePasswordVM viewmodelVacio = new ChangePasswordVM();
                return View($"~/Views/Account/Edit/{type}.cshtml", viewmodelVacio);

            } else if (type == "Avatar")
            {
                string profilePicPath = _userService.GetUserProfileImagePath(IdUser);
                ViewBag.ProfilePicPath = profilePicPath;
            }

            return View($"~/Views/Account/Edit/{type}.cshtml");
        }



        [HttpPost]
        public async Task<JsonResult> EditGeneralApi([FromBody] GeneralEditUserVM viewmodel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(x => x.Value.Errors.Any())
                               .ToDictionary(
                                   kvp => kvp.Key,
                                   kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                               );

                return Json(new { success = false, errors = errors });

            }

            ClaimsPrincipal claimUser = HttpContext.User;
            viewmodel.Id = Convert.ToInt32(claimUser.FindFirstValue("Id")); 


            User? model = _DBContext.Users.Where(u => u.Id == viewmodel.Id && u.DeletedAt == null && u.IsBanned == null).FirstOrDefault();

            if(model != null)
            {
                if(viewmodel.Name != model.Name || viewmodel.Surname != model.Surname || viewmodel.Username != model.Username || viewmodel.Email != model.Email || viewmodel.Description != model.Description)
                {
                    model.Name = viewmodel.Name;
                    model.Surname = viewmodel.Surname;
                    model.Username = viewmodel.Username;
                    model.Email = viewmodel.Email;
                    model.Description = Utilities.ReduceLineBreaks(viewmodel.Description);

                    _DBContext.Update(model);
                    await _DBContext.SaveChangesAsync();

                    //Traigo los Claims que se deben/pueden actualizar
                    var usernameClaim = claimUser.FindFirst(ClaimTypes.NameIdentifier);
                    var emailClaim = claimUser.FindFirst(ClaimTypes.Email);

                    //Creo nuevos Claims con los nuevos valores ingresados
                    Claim newUsernameClaim = new Claim(usernameClaim.Type, model.Username);
                    Claim newEmailClaim = new Claim(emailClaim.Type, model.Email);

                    //Elimino los Claims viejos y creo los Claims nuevos
                    var identity = claimUser.Identity as ClaimsIdentity;
                    identity.RemoveClaim(usernameClaim);
                    identity.RemoveClaim(emailClaim);
                    identity.AddClaim(newUsernameClaim);
                    identity.AddClaim(newEmailClaim);

                    //Creo un nuevo ClaimsPrincipal
                    ClaimsPrincipal newClaimsPrincipal = new ClaimsPrincipal(identity);

                    //Consigo las propiedades de autentificacion, principalmente para saber la fecha de expiracion de las credenciales
                    var authProperties = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = authProperties.Properties;


                    // Actualizo el ClaimsPrincipal en la sesión
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        newClaimsPrincipal,
                        properties);
                }

                return Json(new { success = true });
            } else
            {
                return Json(new { success = false, redirectUrl = "/Access/Logout" });

            }

        }



        [HttpPost]
        public async Task<IActionResult> EditPasswordApi([FromBody] ChangePasswordVM viewmodel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Where(x => x.Value.Errors.Any())
                               .ToDictionary(
                                   kvp => kvp.Key,
                                   kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                               );

                return Json(new { success = false, errors = errors });
            }
            int IdUser = Convert.ToInt32(HttpContext.User.FindFirstValue("Id"));

            User? user = _DBContext.Users.Where(u => u.Id == IdUser && u.DeletedAt == null && u.IsBanned == null).FirstOrDefault();

            if (user != null)
            {
                user.Password = Utilities.EncryptPassword(viewmodel.newPass);
                user.UpdatedAt = DateTime.Now;

                _DBContext.Update(user);
                await _DBContext.SaveChangesAsync();

                return Json(new { success = true });

            } else
            {
                return Json(new { success = false, redirectUrl = "/Access/Logout" });
            }

        }



        [HttpPost]
        public async Task<JsonResult> EditAvatarApi([FromForm] AvatarVM model)
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
            }

            string ruta = Path.Combine(_hostingEnvironment.WebRootPath, $"images/profiles/{IdUser}");
            if (Directory.Exists(ruta))
            {
                Directory.Delete(ruta, true);
            }
            Directory.CreateDirectory(ruta);

            string extension = model.Avatar.FileName.Split('.')[1];
            string filePath = Path.Combine(ruta, $"profile_pic.{extension}");

            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await model.Avatar.CopyToAsync(fileStream);
            }
            
            string pathProfilePicPath = _userService.GetUserProfileImagePath(IdUser, true);

            return Json(new { success = true, pathProfilePic = pathProfilePicPath });
        }


        [HttpGet]
        public IActionResult DeleteAvatarApi()
        {
            int IdUser = Convert.ToInt32(HttpContext.User.FindFirstValue("Id"));

            string ruta = Path.Combine(_hostingEnvironment.WebRootPath, $"images/profiles/{IdUser}");
            if (Directory.Exists(ruta))
            {
                Directory.Delete(ruta, true);
            }

            return Json(new { success = true, pathProfilePic = "/images/profiles/default.jpg" });
        }




        public async Task<IActionResult> Disable()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            int IdUser = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());

            User? user = _DBContext.Users.SingleOrDefault(u => u.Id == IdUser && u.DeletedAt == null && u.IsBanned == null);

            if (user != null)
            {
                user.DeletedAt = DateTime.Now;

                _DBContext.Update(user);
                await _DBContext.SaveChangesAsync();

                // Elimino foto de perfil (si es que tiene)
                string ruta = Path.Combine(_hostingEnvironment.WebRootPath, $"images/profiles/{IdUser}");
                if (Directory.Exists(ruta))
                {
                    Directory.Delete(ruta, true);
                }
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Access");
        }

    }
}
