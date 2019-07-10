namespace ProjektMovie.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Movie")]
    public partial class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Tytu³")]
        [StringLength(60, MinimumLength = 2)]
        public string nazwa { get; set; }

        [Display(Name = "Rodzaj")]
        [StringLength(50, MinimumLength = 4)]
        public string rodzaj { get; set; }

        [Display(Name = "Data Wydania")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime? rok_produkcji { get; set; }

       // public int? director_id { get; set; }

        public virtual Director Director { get; set; }
    }
}
