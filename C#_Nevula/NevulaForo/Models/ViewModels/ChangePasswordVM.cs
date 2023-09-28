using System.ComponentModel.DataAnnotations;

namespace NevulaForo.Models.ViewModels
{
    public class ChangePasswordVM
    {

        [Required(ErrorMessage = "Debe ingresar la contraseña actual")]
        public string currentPass { get; set; } = null!;


        [Required(ErrorMessage = "Debe ingresar una nueva contraseña")]
        [MinLength(3, ErrorMessage = "Debe ingresar una combinación con al menos 3 caracteres")]
        [MaxLength(50, ErrorMessage = "Debe ingresar una combinación con hasta 50 caracteres")]
        public string newPass { get; set; } = null!;

    }
}
