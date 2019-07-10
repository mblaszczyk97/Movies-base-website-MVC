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
    [Authorize]
    public class GameDevelopersController : Controller
    {
        private Model1 db = new Model1();
        ApplicationDbContext context;

        public GameDevelopersController()
        {
            context = new ApplicationDbContext();
        }
        // GET: GameDevelopers
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {


                if (!isAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "AspNetRoles");
            }

            return View(db.GameDeveloper.ToList());
        }

        public Boolean isAdminUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Administrator")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        // GET: GameDevelopers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameDeveloper gameDeveloper = db.GameDeveloper.Find(id);
            if (gameDeveloper == null)
            {
                return HttpNotFound();
            }
            return View(gameDeveloper);
        }


        // GET: GameDevelopers/Create
        public ActionResult Create()
        {
            if (!isAdminUser())
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: GameDevelopers/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,nazwa,data_rozpoczecia")] GameDeveloper gameDeveloper)
        {
            if (ModelState.IsValid)
            {
                db.GameDeveloper.Add(gameDeveloper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gameDeveloper);
        }

        // GET: GameDevelopers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!isAdminUser())
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameDeveloper gameDeveloper = db.GameDeveloper.Find(id);
            if (gameDeveloper == null)
            {
                return HttpNotFound();
            }
            return View(gameDeveloper);
        }

        // POST: GameDevelopers/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,nazwa,data_rozpoczecia")] GameDeveloper gameDeveloper)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gameDeveloper).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gameDeveloper);
        }

        // GET: GameDevelopers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!isAdminUser())
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GameDeveloper gameDeveloper = db.GameDeveloper.Find(id);
            if (gameDeveloper == null)
            {
                return HttpNotFound();
            }
            return View(gameDeveloper);
        }

        // POST: GameDevelopers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GameDeveloper gameDeveloper = db.GameDeveloper.Find(id);
            db.GameDeveloper.Remove(gameDeveloper);
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
