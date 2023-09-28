using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NevulaForo.Models.DB;
using NevulaForo.Models.ViewModels;
using NevulaForo.Resources;
using System.Security.Claims;


using Microsoft.AspNetCore.Http;

namespace NevulaForo.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private readonly NevulaContext _DBContext;
        private IWebHostEnvironment _hostingEnvironment;

        public AccountController(NevulaContext dbContext, IWebHostEnvironment hostingEnvironment)
        {
            _DBContext = dbContext;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index(int IdUser)
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (IdUser == 0 && claimUser.Identity.IsAuthenticated) 
            {
                IdUser = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());
            }

            try
            {
                UserProfileVM oUserProfile = new UserProfileVM()
                {
                    oUser = _DBContext.Users.Include(u => u.UserRoles).Where(u => u.DeletedAt == null && u.Id == IdUser).ToList().First(),
                    oPublications = _DBContext.Publications.Where(p => p.DeletedAt == null && p.IdUser == IdUser).OrderByDescending(p => p.CreatedAt).ToList()
                };

                return View(oUserProfile);
            } catch (InvalidOperationException ex)
            {
                //404
                return NotFound();
            }
        }


        [HttpGet]
        public IActionResult Edit(string type = "General")
        {
            if(type == "General")
            {
                ClaimsPrincipal claimUser = HttpContext.User;
                int IdUser = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());

                User user = _DBContext.Users.Where(u => u.Id == IdUser && u.DeletedAt == null).FirstOrDefault();

                GeneralEditUserVM viewmodel = new GeneralEditUserVM();
                viewmodel.Id = IdUser;
                viewmodel.Email = user.Email;
                viewmodel.Surname = user.Surname;
                viewmodel.Username = user.Username;
                viewmodel.Name = user.Name;
                viewmodel.Description = user.Description;

                return View($"~/Views/Account/Edit/{type}.cshtml", viewmodel);
            } else if (type == "Password")
            {
                ChangePasswordVM viewmodelVacio = new ChangePasswordVM();
                return View($"~/Views/Account/Edit/{type}.cshtml", viewmodelVacio);
            }

            return View($"~/Views/Account/Edit/{type}.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> EditGeneral(GeneralEditUserVM viewmodel)
        {
            if(ModelState.IsValid)
            {
                ClaimsPrincipal claimUser = HttpContext.User;
                viewmodel.Id = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());


                User model = _DBContext.Users.Where(u => u.Id == viewmodel.Id && u.DeletedAt == null).FirstOrDefault();

                if(model != null)
                {
                    if(viewmodel.Name != model.Name || viewmodel.Surname != model.Surname || viewmodel.Username != model.Username || viewmodel.Email != model.Email || viewmodel.Description != model.Description)
                    {
                        model.Name = viewmodel.Name;
                        model.Surname = viewmodel.Surname;
                        model.Username = viewmodel.Username;
                        model.Email = viewmodel.Email;
                        model.Description = viewmodel.Description;

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
                }

            }
            
            return View($"~/Views/Account/Edit/General.cshtml", viewmodel);
        }

        [HttpPost]
        public async Task<IActionResult> EditPassword(ChangePasswordVM viewmodel)
        {

            if (ModelState.IsValid)
            {
                ClaimsPrincipal claimUser = HttpContext.User;
                int IdUser = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());

                try
                {

                    User user = _DBContext.Users.Where(u => u.Id == IdUser && u.DeletedAt == null).FirstOrDefault();

                    if (user != null && user.Password == Utilities.EncryptPassword(viewmodel.currentPass))
                    {
                        user.Password = Utilities.EncryptPassword(viewmodel.newPass);
                        user.UpdatedAt = DateTime.Now;

                        _DBContext.Update(user);
                        await _DBContext.SaveChangesAsync();

                        ViewData["Message"] = "Se actualizo la contraseña";
                        return View($"~/Views/Account/Edit/Password.cshtml", viewmodel);

                    }
                    else
                    {
                        throw new Exception("La contraseña no es correcta");
                    }
                }
                catch (Exception ex)
                {
                    ViewData["MessageErrorPass"] = ex.Message;
                    return View($"~/Views/Account/Edit/Password.cshtml", viewmodel);
                }
            }

            return View($"~/Views/Account/Edit/Password.cshtml", viewmodel);
        }



        [HttpPost]
        public async Task<IActionResult> EditAvatar([FromForm] AvatarVM model)
        {
            // FALTAN AGREGAR VALIDACIONES
            if (!ModelState.IsValid)
            {
                return View($"~/Views/Account/Edit/Avatar.cshtml", model);
            }

            ClaimsPrincipal claimUser = HttpContext.User;
            int IdUser = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());

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

            return View($"~/Views/Account/Edit/Avatar.cshtml");
        }



        public async Task<IActionResult> Delete()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            int IdUser = Convert.ToInt32(claimUser.Claims.Where(c => c.Type == "Id").Select(c => c.Value).SingleOrDefault());

            User user = _DBContext.Users.SingleOrDefault(u => u.Id == IdUser && u.DeletedAt == null);

            if (user != null)
            {
                user.DeletedAt = DateTime.Now;

                _DBContext.Update(user);
                await _DBContext.SaveChangesAsync();
            }

            //Error no existe la cuenta o ya esta eliminada
            return RedirectToAction("Logout", "Access");
        }

    }
}
