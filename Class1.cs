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
using ProjektMovie.Repositories;
using ProjektMovie.Repositories.Interfaces;

namespace ProjektMovie.Controllers
{

    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IMovieRepository _movieRepository;

        public MoviesController()
        {
            _movieRepository = new MovieRepository();
        }

        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        //private Model1 db = new Model1();
        //ApplicationDbContext context;

        /*
        public MoviesController()
        {
            context = new ApplicationDbContext();
        }
        */

        // GET: Movies
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {



            var movie = from d in _movieRepository.GetMovies()
                        select d;
            return View(movie);

        }

        public async System.Threading.Tasks.Task<ActionResult> Search(string searchString, string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.RodzajSortParm = String.IsNullOrEmpty(sortOrder) ? "rodzaj" : "";
            ViewBag.ForeignSortParm = String.IsNullOrEmpty(sortOrder) ? "foreign_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var movies = from m in _movieRepository.GetMovies()
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.nazwa.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "rodzaj":
                    movies = movies.OrderBy(s => s.rodzaj);
                    break;
                case "foreign_desc":
                    movies = movies.OrderBy(s => s.Director.nazwisko);
                    break;
                case "name_desc":
                    movies = movies.OrderByDescending(s => s.nazwa);
                    break;
                case "Date":
                    movies = movies.OrderBy(s => s.rok_produkcji);
                    break;
                case "date_desc":
                    movies = movies.OrderByDescending(s => s.rok_produkcji);
                    break;
                default:
                    movies = movies.OrderBy(s => s.nazwa);
                    break;
            }
            return View(movies);

        }



        // GET: Movies/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = _movieRepository.GetMovieById(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Movies/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {


            ViewBag.director_id = new SelectList(db.Director, "Id", "imie");
            return View();
        }

        // POST: Movies/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nazwa,rodzaj,rok_produkcji,director_id")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _movieRepository.AddMovie(movie);
                _movieRepository.Save();
                return RedirectToAction("Index");
            }

            ViewBag.director_id = new SelectList(db.Director, "Id", "imie", movie.director_id);
            return View(movie);
        }

        // GET: Movies/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            ViewBag.director_id = new SelectList(db.Director, "Id", "imie", movie.director_id);
            return View(movie);
        }

        // POST: Movies/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nazwa,rodzaj,rok_produkcji,director_id")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.director_id = new SelectList(db.Director, "Id", "imie", movie.director_id);
            return View(movie);
        }

        // GET: Movies/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movie.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movie.Find(id);
            db.Movie.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

