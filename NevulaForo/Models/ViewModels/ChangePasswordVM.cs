using NevulaForo.Models.DB;
using NevulaForo.Resources;
using NevulaForo.Validations;
using System.ComponentModel.DataAnnotations;

namespace NevulaForo.Models.ViewModels
{
    public class ChangePasswordVM //: IValidatableObject
    {

        [Required(ErrorMessage = "Debe ingresar la contraseña actual.")]
        [CurrentPasswordMatch(ErrorMessage = "La contraseña actual no es correcta.")]
        public string currentPass { get; set; } = null!;


        [Required(ErrorMessage = "Debe ingresar una nueva contraseña.")]
        [MinLength(3, ErrorMessage = "Debe ingresar una combinación con al menos 3 caracteres.")]
        [MaxLength(50, ErrorMessage = "Debe ingresar una combinación con hasta 50 caracteres.")]
        public string newPass { get; set; } = null!;


        /*public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (string.IsNullOrEmpty(currentPass))
            {
                yield return ValidationResult.Success; // No validation if the currentPass is null or empty
            }

            // Intentar obtener el ClaimsPrincipal desde el HttpContext
            var httpContextAccessor = (IHttpContextAccessor)validationContext.GetService(typeof(IHttpContextAccessor));

            var claimsPrincipal = httpContextAccessor.HttpContext.User;

            var userIdClaim = Convert.ToInt32(claimsPrincipal.FindFirst("Id")?.Value);

            var dbContext = (NevulaContext)validationContext.GetService(typeof(NevulaContext));

            // Use userIdClaim to get the user from the database and compare passwords
            var user = dbContext.Users.FirstOrDefault(u => u.Id == userIdClaim);

            if (user == null || !VerifyPassword(currentPass, user.Password))
            {
                yield return new ValidationResult("La contraseña actual no es válida.", new[] { nameof(currentPass) });
            }
        }

        private bool VerifyPassword(string currentPass, string storedPass)
        {
            // Implementa tu lógica de comparación de contraseñas aquí, por ejemplo, hash y comparación
            // Devuelve true si las contraseñas coinciden, de lo contrario, false
            return Utilities.EncryptPassword(currentPass) == storedPass;
        }*/
    }
}
