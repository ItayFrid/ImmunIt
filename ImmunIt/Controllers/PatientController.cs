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
            Patient patient = new Patient();
            ViewBag.PatientLoginMessage = "";
            return View(patient);
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



            return View(patient[0]);
        }

        //public ActionResult PastVaccines()
   
    }
}