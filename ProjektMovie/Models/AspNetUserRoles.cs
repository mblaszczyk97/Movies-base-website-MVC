namespace ProjektMovie.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ProjektMovie.Models;

    public partial class AspNetUserRoles
    {
        private Model1 db = new Model1();

        [Key]
        [Column(Order = 0)]
        public string UserId { get; set; }
        public virtual AspNetUsers User { get; set; }

        [Key]
        [Column(Order = 1)]
        public string RoleId { get; set; }
        public virtual AspNetRoles Role { get; set; }


        public async System.Threading.Tasks.Task<String> Search(string searchString)
        {
            

            var movies = from m in db.AspNetUsers
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Id.Equals(UserId));
            }
            return "";

        }
    }
}
