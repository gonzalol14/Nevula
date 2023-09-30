using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NevulaForo.Models.DB;

namespace NevulaForo.Validations
{
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var username = value.ToString();
                var dbContext = (NevulaContext)validationContext.GetService(typeof(NevulaContext)); // Reemplaza YourDbContext con el nombre de tu DbContext

                // Intentar obtener el ClaimsPrincipal desde el HttpContext
                var httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));

                var claimsPrincipal = httpContextAccessor.HttpContext.User;

                if (claimsPrincipal == null)
                {
                    // Verifica si el nombre de usuario ya existe en la base de datos
                    if (dbContext.Users.Any(u => u.Username == username))
                    {
                        return new ValidationResult("El nombre de usuario ingresado no esta disponible.");
                    }
                } else
                {
                    var userIdClaim = Convert.ToInt32(claimsPrincipal.FindFirst("Id")?.Value);
                    // Verifica si el nombre de usuario ya existe en la base de datos
                    if (dbContext.Users.Any(u => u.Username == username && u.Id != userIdClaim))
                    {
                        return new ValidationResult("El nombre de usuario ingresado no esta disponible.");
                    }
                }

                
            }

            return ValidationResult.Success;
        }
    }
}
