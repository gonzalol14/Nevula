using NevulaForo.Models.DB;
using System.ComponentModel.DataAnnotations;

namespace NevulaForo.Models.ViewModels
{
    public class CreatePublicationVM
    {
        public int Id { get; set; }
        public int IdUser { get; set; }

        [Required(ErrorMessage = "Debe ingresar un título"), MinLength(5, ErrorMessage = "El título debe tener entre 5 y 250 caracteres"), MaxLength(250, ErrorMessage = "El título debe tener entre 5 y 250 caracteres")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Debe ingresar una descripción"), MinLength(30, ErrorMessage = "La descripción debe tener entre 30 y 1750 caracteres"), MaxLength(1750, ErrorMessage = "La descripción debe tener entre 30 y 1750 caracteres")]
        public string Description { get; set; } = null!;

    }
}
