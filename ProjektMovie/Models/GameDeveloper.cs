namespace ProjektMovie.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GameDeveloper")]
    public partial class GameDeveloper
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GameDeveloper()
        {
            Game = new HashSet<Game>();
        }

        public int Id { get; set; }

        [Display(Name = "Nazwa Firmy")]
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string nazwa { get; set; }

        [Display(Name = "Data Rozpoczêcia Dzia³alnoœci")]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime? data_rozpoczecia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Game> Game { get; set; }
    }
}
