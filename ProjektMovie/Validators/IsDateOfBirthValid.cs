using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektMovie.Validators
{
    public class IsDateOfBirthValid : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(validationContext.DisplayName + " jest wymagane.");
            }
            var dateOfBirth = value;
            if ((DateTime)dateOfBirth <= DateTime.Now)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Data nie może być z przyszłości!");
        }
    }
}