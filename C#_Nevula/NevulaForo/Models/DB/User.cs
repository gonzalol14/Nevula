using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NevulaForo.Models.DB;

public partial class User
{
    public int Id { get; set; }

    [MinLength(3, ErrorMessage = "El nombre debe tener entre 3 y 30 caracteres"), MaxLength(30, ErrorMessage = "El nombre debe tener entre 3 y 30 caracteres")]
    public string? Name { get; set; }

    [MinLength(3, ErrorMessage = "El apellido debe tener entre 3 y 30 caracteres"), MaxLength(30, ErrorMessage = "El apellido debe tener entre 3 y 30 caracteres")]
    public string? Surname { get; set; }

    [Required(ErrorMessage = "Debe ingresar un nombre de usuario"), MinLength(3, ErrorMessage = "El nombre de usuario debe tener entre 3 y 30 caracteres"), MaxLength(30, ErrorMessage = "El nombre de usuario debe tener entre 3 y 30 caracteres")]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Debe ingresar un correo electrónico"), EmailAddress(ErrorMessage = "Ingrese un corre válido electrónico")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Debe ingresar una contraseña"), MinLength(3, ErrorMessage = "Debe ingresar una combinación con al menos tres caracteres"), MaxLength(50, ErrorMessage = "Debe ingresar una combinación con al menos tres caracteres")]
    public string Password { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Publication> Publications { get; set; } = new List<Publication>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
