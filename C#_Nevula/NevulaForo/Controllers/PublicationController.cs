using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NevulaForo.Controllers
{
    [Authorize]
    public class PublicationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Delete()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        //Falta like, dislike. Analizar si crear otro controller para comentarios o hacerlo en este.
    }
}
