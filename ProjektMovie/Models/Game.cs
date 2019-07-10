namespace ProjektMovie.Models
{
    using ProjektMovie.Validators;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Game")]
    public partial class Game
    {
        public int Id { get; set; }

        [TitleNameAttribute]
        [Display(Name = "Tytu³ Gry")]
        [Required(ErrorMessage = "Podaj tytu³ gry!")]
        [StringLength(100, MinimumLength = 2)]
        [Remote(action: "VerifyTitle", controller: "Game")]
        public string nazwa { get; set; }

        [YearOfReleaseAttribute]
        [Display(Name = "Data Wydania")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime? data_wydania { get; set; }

        public int? GameDeveloperId { get; set; }

        public virtual GameDeveloper GameDeveloper { get; set; }
    }
}
