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
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Login()
        {
            Manager mng = new Manager();
            return View(mng);
        }

        public ActionResult ManagerLogin(Manager mng)
        {
            DataLayer dal = new DataLayer();
            List<Manager> ManagerToCheck = (from x in dal.Managers
                                            where x.UserName == mng.UserName
                                            select x).ToList<Manager>();       //Attempting to get manager information from database
            if (ManagerToCheck.Count != 0)     //In case username was found
            {

                var authTicket = new FormsAuthenticationTicket(
                    1,                                  // version
                    mng.UserName,                      // user name
                    DateTime.Now,                       // created
                    DateTime.Now.AddMinutes(20),        // expires
                    true,       //keep me connected
                    ManagerToCheck[0].role                       // store roles
                    );

                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(authCookie);
                return RedirectToAction("Index", "Manager");

            }
            else
                ViewBag.UserLoginMessage = "Incorrect Username/password";
            return View("ManagerLogin", mng);

        }

   


        public ActionResult AddPatient()
        {
            Patient medic = new Patient();
            return View("AddPatient", medic);
        }

        public ActionResult PatientRegister(Patient patient)
        {
            DataLayer dal = new DataLayer();

            if (ModelState.IsValid)
            {
                //string hashedPassword = enc.CreateHash(medic.password);      //Encrypting user's password
                if (!patientExists(patient.Id))     //Adding user to database
                {
                    ImmunCard immunCard = new ImmunCard();
                    immunCard.Patient = patient;
                    patient.role = "Patient";
                    patient.ImmunCard = immunCard;
                    dal.ImmunCards.Add(immunCard);
                    // medic.password = hashedPassword;
                    dal.patients.Add(patient);
                    dal.SaveChanges();
                    ViewBag.message = "Patient was added succesfully.";
                    patient = new Patient();
                }
                else
                    ViewBag.message = "ID Exists in database.";
            }
            else
                ViewBag.message = "Error in registration.";
            return View("AddPatient", patient);
        }

        /*This function compares given username with usernames in database*/
        private bool patientExists(string Id)
        {
            DataLayer dal = new DataLayer();
            List<Patient> users = dal.patients.ToList<Patient>();
            foreach (Patient user in dal.patients)
                if (user.Id.Equals(Id))
                    return true;
            return false;
        }




        public ActionResult ViewAllUsers()
        {
            return View();
        }


        /*This function redirects to medic register page*/
        public ActionResult AddMedic()
        {
            Medic medic = new Medic();
            return View("AddMedic",medic);
        }

        /*This function adds new user to database*/
        /*Given user information from user register form*/
        public ActionResult MedicRegister(Medic medic)
        {
            DataLayer dal = new DataLayer();

            if (ModelState.IsValid)
            {
                //string hashedPassword = enc.CreateHash(medic.password);      //Encrypting user's password
                if (!medicExists(medic.UserName))     //Adding user to database
                {
                   // medic.password = hashedPassword;
                    dal.Medics.Add(medic);
                    medic.role = "Medic";
                    dal.SaveChanges();
                    ViewBag.message = "Medic was added succesfully.";
                    medic = new Medic();
                }
                else
                    ViewBag.message = "Username Exists in database.";
            }
            else
                ViewBag.message = "Error in registration.";
            return View("AddMedic", medic);
        }



        /*This function compares given username with usernames in database*/
        private bool medicExists(string userName)
        {
            DataLayer dal = new DataLayer();
            List<Medic> users = dal.Medics.ToList<Medic>();
            foreach (Medic user in dal.Medics)
                if (user.UserName.Equals(userName))
                    return true;
            return false;
        }
    }
}