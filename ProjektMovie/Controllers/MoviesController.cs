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
            Movie movie = _movieRepository.GetMovieById(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
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
                _movieRepository.UpdateMovie(movie);
                _movieRepository.Save();
                return RedirectToAction("Index");
            }
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
            Movie movie = _movieRepository.GetMovieById(id);
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
            Movie movie = _movieRepository.GetMovieById(id);
            _movieRepository.DeleteMovie(id);
            _movieRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _movieRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}

