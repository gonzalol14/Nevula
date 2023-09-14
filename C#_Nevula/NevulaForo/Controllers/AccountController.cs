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
        [Authorize]
        public IActionResult Edit(string type = "General")
        {
            return View($"~/Views/Account/Edit/{type}.cshtml");
        }

        public IActionResult Delete()
        {
            return View();
        }

    }
}
