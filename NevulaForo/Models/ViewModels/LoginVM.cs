using System.ComponentModel.DataAnnotations;

namespace NevulaForo.Models.ViewModels
{
    public class LoginVM
    {

        [Required(ErrorMessage = "Debe ingresar un correo electronico.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Debe ingresar una contraseña.")]
        public string Password { get; set; } = null!;

        public bool? Remember { get; set; }

    }
}
