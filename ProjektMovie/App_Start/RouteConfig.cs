using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjektMovie
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Panel",
                url: "{country}-{lang}/Panel/{action}/{id}",
                defaults: new
                {
                    controller = "Users",
                    action = "index",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "RoleUserow",
                url: "{country}-{lang}/RoleUzytkownikow/{action}/{id}",
                defaults: new
                {
                    controller = "AspNetUserRoles",
                    action = "index",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Role",
                url: "{country}-{lang}/Role/{action}/{id}",
                defaults: new
                {
                    controller = "AspNetRoles",
                    action = "index",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Uzytkownicy",
                url: "{country}-{lang}/Uzytkownicy/{action}/{id}",
                defaults: new
                {
                    controller = "AspNetUsers",
                    action = "index",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "SzukajGry",
                url: "{country}-{lang}/Gry/{action}/{id}",
                defaults: new
                {
                    controller = "Games",
                    action = "index",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "SzukajFilmu",
                url: "{country}-{lang}/Filmy/{action}/{id}",
                defaults: new
                {
                    controller = "Movies",
                    action = "index",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
               name: "Rezyser",
               url: "{country}-{lang}/Rezyserzy/{action}/{id}",
               defaults: new
               {
                   controller = "Directors",
                   action = "index",
                   id = UrlParameter.Optional
               }
           );

            routes.MapRoute(
               name: "Gamedev",
               url: "{country}-{lang}/DeweloperzyGier/{action}/{id}",
               defaults: new
               {
                   controller = "GameDevelopers",
                   action = "index",
                   id = UrlParameter.Optional
               }
           );

            routes.MapRoute(
                name: "Podstawa",
                url: "{country}-{lang}/{controller}/{action}/{id}",
                defaults: new { country = "eu", lang = "pl", controller = "Games", action = "Search", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {  controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
