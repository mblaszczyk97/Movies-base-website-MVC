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
    public class DirectorsController : Controller
    {
        private readonly IDirectorRepository _directorRepository;

        public DirectorsController()
        {
            _directorRepository = new DirectorRepository();
        }

        public DirectorsController(IDirectorRepository directorRepository)
        {
            _directorRepository = directorRepository;
        }

        // GET: Directors
        public ActionResult Index()
        {
            var movie = from d in _directorRepository.GetDirectors()
                        select d;
            return View(movie);
        }



        // GET: Directors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Director movie = _directorRepository.GetDirectorById(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // GET: Directors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Directors/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,imie,nazwisko,rok_urodzenia,kraj")] Director director)
        {
            if (ModelState.IsValid)
            {
                _directorRepository.AddDirector(director);
                _directorRepository.Save();
                return RedirectToAction("Index");
            }
            return View(director);
        }

        // GET: Directors/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Director movie = _directorRepository.GetDirectorById(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Directors/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,imie,nazwisko,rok_urodzenia,kraj")] Director director)
        {
            if (ModelState.IsValid)
            {
                _directorRepository.UpdateDirector(director);
                _directorRepository.Save();
                return RedirectToAction("Index");
            }
            return View(director);
        }

        // GET: Directors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Director movie = _directorRepository.GetDirectorById(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Directors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Director movie = _directorRepository.GetDirectorById(id);
            _directorRepository.DeleteDirector(id);
            _directorRepository.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            _directorRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
