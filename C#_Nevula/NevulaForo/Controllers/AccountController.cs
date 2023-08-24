using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NevulaForo.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //Deberian ser 3 edits
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
