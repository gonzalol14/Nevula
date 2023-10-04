using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NevulaForo.Models.DB;
using NevulaForo.Resources;

namespace NevulaForo.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class CurrentPasswordMatchAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentPass = value?.ToString();
            if (string.IsNullOrEmpty(currentPass))
            {
                return ValidationResult.Success; // No validar si la currentPass es nula o vacia
            }

            // Intentar obtener el ClaimsPrincipal desde el HttpContext
            var httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));
            var claimsPrincipal = httpContextAccessor.HttpContext.User;

            var userIdClaim = Convert.ToInt32(claimsPrincipal.FindFirst("Id")?.Value);

            var dbContext = (NevulaContext)validationContext.GetService(typeof(NevulaContext));

            // Traer el usuario de la BD para comparar la contraseña ingresada con la actual
            var user = dbContext.Users.FirstOrDefault(u => u.Id == userIdClaim);

            if (user == null || !VerifyPassword(currentPass, user.Password))
            {
                return new ValidationResult(ErrorMessage ?? "La contraseña actual no es válida.");
            }

            return ValidationResult.Success;
        }


        private bool VerifyPassword(string currentPass, string storedPass)
        {
            // Devuelve verdadero si las cadenas son iguales y falso si no lo son
            return Utilities.EncryptPassword(currentPass) == storedPass;
        }
    }
}
