using ImmunIt.DAL;
using ImmunIt.Models;
using ImmunIt.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            ViewBag.Message = "";
            return View(medic);
        }

        public ActionResult MedicLogin(Medic medic)
        {
            DataLayer dal = new DataLayer();
            List<Medic> MedicToCheck()
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