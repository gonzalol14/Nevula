using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NevulaForo.Models.ViewModels
{
    public class AvatarVM
    {
        [Required(ErrorMessage = "Debe ingresar una imagen")]
        public IFormFile Avatar { get; set; }

    }
}
