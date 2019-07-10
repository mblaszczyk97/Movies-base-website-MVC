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

namespace ProjektMovie.Controllers
{
    /// <summary>
    /// Walidatory
    /// Authorize
    /// layout nowy
    /// </summary>
    [Authorize]
    public class GamesController : Controller
    {
        private Model1 db = new Model1();
        ApplicationDbContext context;

        public GamesController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Games
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {

            var game = db.Game.Include(g => g.GameDeveloper);
            return View(game.ToList());
        }

        

        public async System.Threading.Tasks.Task<ActionResult> Search(string searchString, string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ForeignSortParm = String.IsNullOrEmpty(sortOrder) ? "foreign_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var games = from m in db.Game
                         select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(s => s.nazwa.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "foreign_desc":
                    games = games.OrderBy(s => s.GameDeveloper.nazwa);
                    break;
                case "name_desc":
                    games = games.OrderByDescending(s => s.nazwa);
                    break;
                case "Date":
                    games = games.OrderBy(s => s.data_wydania);
                    break;
                case "date_desc":
                    games = games.OrderByDescending(s => s.data_wydania);
                    break;
                default:
                    games = games.OrderBy(s => s.nazwa);
                    break;
            }
            return View(await games.ToListAsync());

        }


        // GET: Games/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {

            ViewBag.GameDeveloperId = new SelectList(db.GameDeveloper, "Id", "nazwa");
            return View();
        }

        // POST: Games/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nazwa,data_wydania,GameDeveloperId")] Game game)
        {

            if (ModelState.IsValid)
            {
                db.Game.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GameDeveloperId = new SelectList(db.GameDeveloper, "Id", "nazwa", game.GameDeveloperId);
            return View(game);
        }

        // GET: Games/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.GameDeveloperId = new SelectList(db.GameDeveloper, "Id", "nazwa", game.GameDeveloperId);
            return View(game);
        }

        // POST: Games/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nazwa,data_wydania,GameDeveloperId")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GameDeveloperId = new SelectList(db.GameDeveloper, "Id", "nazwa", game.GameDeveloperId);
            return View(game);
        }

        // GET: Games/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Game.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Game.Find(id);
            db.Game.Remove(game);
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
