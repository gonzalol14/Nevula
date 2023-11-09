using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NevulaForo.Controllers
{
    [Authorize]
    [Authorize(Policy = "PolicyAdministrator")]
    public class AdminController : Controller
    {
        //PAGINAS
        public ActionResult Users()
        {
            return View();
        }
        public ActionResult Publications()
        {
            return View();
        }

        public ActionResult Comments()
        {
            return View();
        }


        //Acciones
        [HttpPost]
        //Deben enviar mails al usuario al que se le borra su cuenta, su publicacion o su comentario
        public ActionResult DeleteUser()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult DeletePublication()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteComment()
        {
            return View();
        }
    }
}
