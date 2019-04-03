using ImmunIt.DAL;
using ImmunIt.Models;
using ImmunIt.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ImmunIt.Controllers
{
    public class MedicController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            Medic medic = new Medic();
            return View(medic);
        }

        public ActionResult MedicLogin(Medic medic)
        {
            DataLayer dal = new DataLayer();
            List<Medic> MedicToCheck = (from x in dal.Medics
                                  where x.UserName == medic.UserName
                                  select x).ToList<Medic>();
            if(MedicToCheck.Count != 0)
            {
                var authTicket = new FormsAuthenticationTicket(
                        1,                                  // version
                        medic.UserName,                      // user name
                        DateTime.Now,                       // created
                        DateTime.Now.AddMinutes(20),        // expires
                        true,       //keep me connected
                        MedicToCheck[0].role                       // store roles
                );
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(authCookie);
            }
            return RedirectToAction("Index", "Medic");
        }

        public ActionResult WatchVaccineInfo()
        {
            return View();
        }

        public ActionResult SearchPatient()
        {
            return View();
        }

        public ActionResult PatientPage()
        {
            return View();
        }

        public ActionResult AddVaccine()
        {
            return View();
        }

    }


}