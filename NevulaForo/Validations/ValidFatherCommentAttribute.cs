using Microsoft.EntityFrameworkCore;
using NevulaForo.Models.DB;
using System.ComponentModel.DataAnnotations;

namespace NevulaForo.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ValidFatherCommentAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _DBContext = (NevulaContext)validationContext.GetService(typeof(NevulaContext));

            if (value != null)  // No es obligatorio un comentario padre, por lo que es válido.
            {
                if (!(value is int parentId)) // Valor del id no es entero
                {
                    return new ValidationResult("Hubo un error al intentar encontrar el comentario citado.");
                }

                // Obtener la publicación actual del contexto de validación
                var currentPublicationId = (int)validationContext.ObjectInstance.GetType()
                    .GetProperty("IdPublication")?.GetValue(validationContext.ObjectInstance);

                // Verificar si el comentario padre pertenece a la misma publicación
                 var isParentCommentValid = _DBContext.Comments
                                            .Include(u => u.IdUserNavigation)
                                            .Where(c => c.Id == parentId && c.DeletedAt == null && c.IdPublication == currentPublicationId && c.IdUserNavigation.DeletedAt == null)
                                            .FirstOrDefault();

                if (isParentCommentValid == null)
                {
                    return new ValidationResult("El comentario citado no existe o no pertenece a la misma publicación.");
                }
            }
            return ValidationResult.Success;
        }

    }
}
