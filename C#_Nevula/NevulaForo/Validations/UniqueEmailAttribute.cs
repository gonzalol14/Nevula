using NevulaForo.Models.DB;
using System.ComponentModel.DataAnnotations;

namespace NevulaForo.Validations
{
    public class UniqueEmailAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var email = value.ToString();
                var dbContext = (NevulaContext)validationContext.GetService(typeof(NevulaContext)); // Reemplaza YourDbContext con el nombre de tu DbContext

                // Intentar obtener el ClaimsPrincipal desde el HttpContext
                var httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));

                var claimsPrincipal = httpContextAccessor.HttpContext.User;

                if (claimsPrincipal == null)
                {
                    // Verifica si el nombre de usuario ya existe en la base de datos
                    if (dbContext.Users.Any(u => u.Email == email))
                    {
                        return new ValidationResult("El ingresado email no esta disponible.");
                    }
                } else
                {
                    var userIdClaim = Convert.ToInt32(claimsPrincipal.FindFirst("Id")?.Value);
                    // Verifica si el nombre de usuario ya existe en la base de datos (sin tener en cuenta el suyo
                    if (dbContext.Users.Any(u => u.Email == email && u.Id != userIdClaim))
                    {
                        return new ValidationResult("El ingresado email no esta disponible.");
                    }
                }

                
            }

            return ValidationResult.Success;
        }

    }
}
