using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImmunIt.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult WatchVaccinesInfo()
        {
            return View();
        }

        public ActionResult WatchPastVaccines()
        {
            return View();
        }

   
    }
}