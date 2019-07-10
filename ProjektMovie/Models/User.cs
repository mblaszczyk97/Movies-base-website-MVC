namespace ProjektMovie.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string nick { get; set; }

        [Required]
        [StringLength(50)]
        public string haslo { get; set; }

        public int rola_id { get; set; }

        public virtual Roles Roles { get; set; }
    }
}
