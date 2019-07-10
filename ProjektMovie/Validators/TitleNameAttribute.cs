using ProjektMovie.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ProjektMovie.Validators
{
    public class TitleNameAttribute : ValidationAttribute
    {
        private Model1 db = new Model1();
        ApplicationDbContext context;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            Game game = (Game)validationContext.ObjectInstance;
            if (game.nazwa != null)
            {
                if (!Regex.Match(game.nazwa, "^([A-Z0-9]).*$").Success)
                {
                    return new ValidationResult(GetErrorMessage()); 
                }

            }

            return ValidationResult.Success;
        }
        private string GetErrorMessage()
        {
            return $"Tytuł musi zaczynać się z dużej litery lub liczby.";
        }
    }
}