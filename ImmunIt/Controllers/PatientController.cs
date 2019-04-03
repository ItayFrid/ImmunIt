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

        public ActionResult PatientLogin(Patient patient)
        {
            DataLayer dal = new DataLayer();
            Encryption enc = new Encryption();
            List<Patient> patientToCheck = (from x in dal.patients
                                      where x.Id == patient.Id
                                      select x).ToList<Patient>();       //Attempting to get patient information from database
            if (patientToCheck.Count != 0)     //In case username was found
            {
                if (enc.ValidatePassword(patient.Password, patientToCheck[0].Password))   //Correct password
                {
                    var authTicket = new FormsAuthenticationTicket(
                        1,                                  // version
                        patient.Id,                      // user Id
                        DateTime.Now,                       // created
                        DateTime.Now.AddMinutes(20),        // expires
                        true,       //keep me connected
                        patientToCheck[0].role                       // store roles
                        );

                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);
                    return RedirectToAction("Index", "Patient");
                }
                else
                {
                    ViewBag.UserLoginMessage = "Incorrect Id/password";
                }
            }
            else
                ViewBag.UserLoginMessage = "Incorrect Id/password";
            return View("Login", patient);

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