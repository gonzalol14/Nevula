using NevulaForo.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NevulaForo.Models.ViewModels
{
    public class AvatarVM
    {
        [Required(ErrorMessage = "Debe ingresar una imagen.")]
        [MaxFileSize(3 * 1024 * 1024)] // 3MB en bytes
        [AllowedFileExtensions(".jpg", ".png", ".webp", ".gif")]
        public IFormFile Avatar { get; set; }

    }
}
