using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NevulaForo.Models.DB;

public partial class Publication
{
    public int Id { get; set; }

    public int IdUser { get; set; }

    [Required(ErrorMessage = "Debe ingresar un título.")]
    [MinLength(5, ErrorMessage = "El título debe tener entre 5 y 250 caracteres.")]
    [MaxLength(250, ErrorMessage = "El título debe tener entre 5 y 250 caracteres.")]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = "Debe ingresar una descripción.")]
    [MinLength(30, ErrorMessage = "El contenido del post debe tener entre 30 y 1750 caracteres.")]
    [MaxLength(1750, ErrorMessage = "El contenido del post debe tener entre 30 y 1750 caracteres.")]
    public string Description { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual User IdUserNavigation { get; set; } = null!;
}
