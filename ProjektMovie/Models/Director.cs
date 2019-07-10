namespace ProjektMovie.Models
{
    using ProjektMovie.Validators;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Director")]
    public partial class Director
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Director()
        {
            Movie = new HashSet<Movie>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Display(Name = "Imie Re¿ysera")]
        [StringLength(50, MinimumLength = 2)]
        public string imie { get; set; }

        [Display(Name = "Nazwisko Re¿ysera")]
        [StringLength(50, MinimumLength = 2)]
        public string nazwisko { get; set; }

        [IsDateOfBirthValid]
        [Display(Name = "Data Urodzenia")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime? rok_urodzenia { get; set; }

        public string FullName
        {
            get { return imie + " " + nazwisko; }
        }

        [StringLength(50)]
        public string kraj { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Movie> Movie { get; set; }
    }
}
