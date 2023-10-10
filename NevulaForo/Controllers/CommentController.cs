using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NevulaForo.Models.DB;
using NevulaForo.Models.ViewModels;
using System.Security.Claims;

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
        public async Task<IActionResult> Create(CreateCommentVM viewmodel)
        {
            Publication publication = _DBContext.Publications.Where(p => p.DeletedAt == null && p.Id == viewmodel.IdPublication).FirstOrDefault();

            if (publication != null) 
            {                
                viewmodel.IdUser = Convert.ToInt32(HttpContext.User.FindFirstValue("Id"));

                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Index", "Publication", new { IdPublication = viewmodel.IdPublication });
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

                return RedirectToAction("Index", "Publication", new { IdPublication = viewmodel.IdPublication });

            }

            //404
            return NotFound();

        }

        /*public IActionResult Edit()
        {
            return View();
        }*/

        [HttpGet]
        public async Task<IActionResult> Delete(int IdComment)
        {
            int IdUser = Convert.ToInt32(HttpContext.User.FindFirstValue("Id"));

            Comment comment = _DBContext.Comments.FirstOrDefault(p => p.Id == IdComment && p.IdUser == IdUser && p.DeletedAt == null);
            if (comment != null)
            {
                comment.DeletedAt = DateTime.Now;

                _DBContext.Update(comment);
                await _DBContext.SaveChangesAsync();

                return RedirectToAction("Index", "Publication", new { IdPublication = comment.IdPublication });
            }

            //404
            return NotFound();
        }
    }
}
