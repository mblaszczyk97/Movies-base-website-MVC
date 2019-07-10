using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using ProjektMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjektMovie.Controllers
{
    public class UsersController : Controller
    {



        // GET: Users
        [Authorize]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {


                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "No";

                if (User.IsInRole("Administrator"))
                {
                    ViewBag.displayMenu = "Yes";
                }

                return View();
            }
            else
            {
                ViewBag.Name = "Not Logged";
            }
            return View();
        }
    }


}