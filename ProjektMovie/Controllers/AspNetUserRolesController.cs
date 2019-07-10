using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektMovie.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProjektMovie.Controllers
{
    [Authorize]
    public class AspNetUserRolesController : Controller
    {

        ApplicationDbContext context;
        private Model3 db = new Model3();

        public AspNetUserRolesController()
        {
            context = new ApplicationDbContext();
        }

        // GET: AspNetUserRoles
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
            return View(db.AspNetUserRoles.ToList());
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

        // GET: AspNetUserRoles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserRoles aspNetUserRoles = db.AspNetUserRoles.Find(id);
            if (aspNetUserRoles == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUserRoles);
        }

        // GET: AspNetUserRoles/Create
        public ActionResult Create()
        {
            if (User.Identity.IsAuthenticated)
            {


                if (!isAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            /*
                var roles = from m in db.AspNetUserRoles
                         select m;
                         */
            ViewBag.UserName = new SelectList(context.Users.Where(u => !u.UserName.Contains("AS"))
                                            .ToList(), "Id", "UserName");

            ViewBag.Name = new SelectList(context.Roles.Where(u => !u.Name.Contains("AS"))
                                            .ToList(), "Id", "Name");

            

            return View();
        }

        // POST: AspNetUserRoles/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,RoleId")] AspNetUserRoles aspNetUserRoles)
        {

            if (ModelState.IsValid)
            {
                db.AspNetUserRoles.Add(aspNetUserRoles);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            

            return View(aspNetUserRoles);
        }

        // GET: AspNetUserRoles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserRoles aspNetUserRoles = db.AspNetUserRoles.Find(id);
            if (aspNetUserRoles == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUserRoles);
        }

        // POST: AspNetUserRoles/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,RoleId")] AspNetUserRoles aspNetUserRoles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUserRoles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aspNetUserRoles);
        }

        // GET: AspNetUserRoles/Delete/5
        public ActionResult Delete(string id1, string id2)
        {
            if (User.Identity.IsAuthenticated)
            {


                if (!isAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserRoles aspNetUserRoles = db.AspNetUserRoles.Find(id1, id2);
            if (aspNetUserRoles == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUserRoles);
        }

        // POST: AspNetUserRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id1, string id2)
        {
            AspNetUserRoles aspNetUserRoles = db.AspNetUserRoles.Find(id1, id2);
            db.AspNetUserRoles.Remove(aspNetUserRoles);
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
