using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NevulaForo.Models.DB;
using NevulaForo.Models.ViewModels;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;
using NevulaForo.Resources;
using NevulaForo.Services.Implementation;

namespace NevulaForo.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly NevulaContext _DBContext;

        public CommentController(NevulaContext dbContext)
        {
            _DBContext = dbContext;
        }


        [HttpPost]
        public async Task<JsonResult> CreateApi([FromBody] CreateCommentVM viewmodel)
        {
            Publication publication = _DBContext.Publications.Where(p => p.DeletedAt == null && p.Id == viewmodel.IdPublication).FirstOrDefault();

            if (publication != null) 
            {                
                viewmodel.IdUser = Convert.ToInt32(HttpContext.User.FindFirstValue("Id"));

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Where(x => x.Value.Errors.Any())
                               .ToDictionary(
                                   kvp => kvp.Key,
                                   kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList()
                               );

                    return Json(new { success = false, errors = errors });
                }

                User user = _DBContext.Users.Where(u => u.DeletedAt == null && u.Id == viewmodel.IdUser).ToList().First();

                Comment model = new Comment()
                {
                    IdUser = viewmodel.IdUser,
                    IdPublication = viewmodel.IdPublication,
                    IdFatherComment = viewmodel.IdFatherComment,
                    Description = viewmodel.Description,
                    CreatedAt = DateTime.Now,
                    DeletedAt = null
                };

                _DBContext.Add(model);
                await _DBContext.SaveChangesAsync();


                return Json(new { success = true, 
                    /*comment = model, 
                    username = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), 
                    userIdRole = HttpContext.User.FindFirstValue(ClaimTypes.Role), 
                    stylizeDate = Utilities.StylizeDate(model.CreatedAt)*/ }, 
                                new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve });

            }

            string[] errorGeneral = { "Error al intentar crear el comentario" };
            return Json(new { success = false, errors = new { errorGeneral = errorGeneral } });

        }

        /*public IActionResult Edit()
        {
            return View();
        }*/

        [HttpGet]
        public async Task<JsonResult> DeleteApi(int IdComment)
        {
            int IdUser = Convert.ToInt32(HttpContext.User.FindFirstValue("Id"));

            Comment comment = _DBContext.Comments.FirstOrDefault(p => p.Id == IdComment && p.IdUser == IdUser && p.DeletedAt == null);
            if (comment != null)
            {
                comment.DeletedAt = DateTime.Now;

                _DBContext.Update(comment);
                await _DBContext.SaveChangesAsync();

                return Json(new { success = true });
            }

            return Json(new { success = false, error = "Error al intentar eliminar el comentario"  });
        }
    }
}
