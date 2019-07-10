using ProjektMovie.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjektMovie.Validators
{
    public class YearOfReleaseAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Game game = (Game)validationContext.ObjectInstance;

            DateTime date1 = new DateTime(1947, 1, 1, 0, 0, 0);
            if (game.data_wydania > DateTime.Now || game.data_wydania < date1)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;
        }
        private string GetErrorMessage()
        {
            return $"Nie może być data w przyszłości lub poniżej 1947 roku";
        }


    }
}