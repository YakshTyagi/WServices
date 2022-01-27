using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCExercise.Models;

namespace MVCExercise.Controllers
{
    public class HomeController : Controller
    {
        //[NonAction]
        //[ChildActionOnly]
        public ActionResult Index()
        {
            Employee Emp = new Employee()
            {
                id = 69,
                name = "Yaksh Tyagi"
            };
            return PartialView(Emp);


        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}