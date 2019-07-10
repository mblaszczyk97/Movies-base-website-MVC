using ProjektMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektMovie.Controllers
{
    public class MovieLINQController : Controller
    {

        private Model1 db = new Model1();
        ApplicationDbContext context;

        public MovieLINQController()
        {
            context = new ApplicationDbContext();
        }

        // GET: MovieLINQ
        public ActionResult Index()
        {
            //var dataContext = new MovieDataContext();
            var movies = from m in db.Movie
                         select m;
            return View(movies);
        }
    }
}