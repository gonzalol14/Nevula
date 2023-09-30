using System.ComponentModel.DataAnnotations;

namespace NevulaForo.Validations
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly long _maxFileSize;

        public MaxFileSizeAttribute(long maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult($"El archivo debe ser menor o igual a {_maxFileSize / (1024 * 1024)} MB.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
