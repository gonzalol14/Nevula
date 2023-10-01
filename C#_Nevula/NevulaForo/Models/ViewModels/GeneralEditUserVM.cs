using NevulaForo.Validations;
using System.ComponentModel.DataAnnotations;

namespace NevulaForo.Models.ViewModels
{
    public class GeneralEditUserVM
    {
        public int Id { get; set; }

        [MinLength(3, ErrorMessage = "El nombre debe tener entre 3 y 30 caracteres.")]
        [MaxLength(30, ErrorMessage = "El nombre debe tener entre 3 y 30 caracteres.")]
        public string? Name { get; set; }

        [MinLength(3, ErrorMessage = "El apellido debe tener entre 3 y 30 caracteres.")]
        [MaxLength(30, ErrorMessage = "El apellido debe tener entre 3 y 30 caracteres.")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre de usuario.")]
        [MinLength(3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 30 caracteres.")]
        [MaxLength(30, ErrorMessage = "El nombre de usuario debe tener entre 3 y 30 caracteres.")]
        [UniqueUsername]
        public string Username { get; set; } = null!;


        [Required(ErrorMessage = "Debe ingresar un correo electrónico.")]
        [EmailAddress(ErrorMessage = "Ingrese un corre válido electrónico.")]
        [UniqueEmail]
        public string Email { get; set; } = null!;

        [MaxLength(250, ErrorMessage = "La descripción debe tener hasta 250 caracteres.")]
        public string? Description { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
