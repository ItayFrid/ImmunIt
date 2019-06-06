
using ImmunIt.Classes;
using ImmunIt.DAL;
using ImmunIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ImmunIt.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        // GET: Patient
        public ActionResult Index()
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
            List<Patient> patients = (from x in dal.patients
                                      select x).ToList<Patient>();
            Patient patient = null;
            foreach (Patient p in patients)
                if (AES.Decrypt(p.Id) == User.Identity.Name)
                    patient = p;
            patient = AES.DecryptPatient(patient);
            //TODO Decrypt Medic Name
            return View(patient);
        }
    }
}