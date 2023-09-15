using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NevulaForo.Models.DB;
using NevulaForo.Models.ViewModels;
using System.Security.Claims;

namespace NevulaForo.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private readonly NevulaContext _DBContext;

        public AccountController(NevulaContext dbContext)
        {
            _DBContext = dbContext;
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
                return RedirectToAction("Community", "Home");
            }
        }


        //Deberian ser 3 edits
        [HttpGet]
        public IActionResult Edit(string type = "General")
        {
            return View($"~/Views/Account/Edit/{type}.cshtml");
        }

        [HttpPost]
        public IActionResult EditGeneral()
        {
            return View($"~/Views/Account/Edit/General.cshtml");
        }

        [HttpPost]
        public IActionResult EditPassword()
        {
            return View($"~/Views/Account/Edit/Password.cshtml");
        }

        [HttpPost]
        public IActionResult EditAvatar()
        {
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
