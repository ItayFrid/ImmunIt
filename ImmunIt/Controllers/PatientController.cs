using ImmunIt.DAL;
using ImmunIt.Models;
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
            DataLayer dal = new DataLayer();
            //Getting current patient refrence
            List<Patient> patient = (from x in dal.patients
                                      where x.Id == User.Identity.Name
                                      select x).ToList<Patient>();



            return View(patient);
        }

        //public ActionResult PastVaccines()
   
    }
}