using System.ComponentModel.DataAnnotations;

namespace NevulaForo.Models.ViewModels
{
    public class CreateCommentVM
    {
        public int Id { get; set; }

        public int IdPublication { get; set; }

        public int IdUser { get; set; }

        public int? IdFatherComment { get; set; }

        [Required(ErrorMessage = "Debe ingresar una descripción.")]
        [MaxLength(300, ErrorMessage = "La descripción debe tener hasta 300 caracteres.")]
        public string Description { get; set; } = null!;
    }
}
