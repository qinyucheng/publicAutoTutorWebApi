using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace publicAutoTutorWebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        public ActionResult admin_addLesson()
        {
           // ViewBag.Title = "Home Page";
            return View(); 
        }
    }
}
