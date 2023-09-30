using System.ComponentModel.DataAnnotations;

namespace NevulaForo.Validations
{
    public class AllowedFileExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _allowedExtensions;

        public AllowedFileExtensionsAttribute(params string[] allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (Array.IndexOf(_allowedExtensions, fileExtension) == -1)
                {
                    var allowedExtensions = string.Join(", ", _allowedExtensions);
                    return new ValidationResult($"Se permiten archivos: {allowedExtensions}");
                }
            }

            return ValidationResult.Success;
        }
    }
}
