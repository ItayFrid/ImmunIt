using ImmunIt.DAL;
using ImmunIt.Models;
using ImmunIt.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;

namespace ImmunIt.Controllers
{

    //[Authorize(Roles = "Medic")]
    public class MedicController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult WatchVaccineInfo()
        {
            ViewModel vm = new ViewModel();
            vm.vaccinesJson = getVaccines();
            return View(vm);
        }

        public ActionResult SearchPatient()
        {
            return View();
        }

        public ActionResult getPatient()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            string id = Request.Form["id"];
            List<Patient> patients = (from x in dal.patients
                                      where x.Id == id
                                      select x).ToList<Patient>();
            if (patients.Count != 0)
                return View("PatientPage", patients[0]);
            ViewBag.Search = "Patient Not Found";
            return RedirectToAction("SearchPatient", "Medic");
        }

        public ActionResult PatientPage(Patient patient)
        {
            return View(patient);
        }


        public List<VaccineJson> getVaccines()
        {
            string vaccineJson = new WebClient().DownloadString("https://itayfrid.000webhostapp.com/vaccines.json");
            List<VaccineJson> json = JsonConvert.DeserializeObject<List<VaccineJson>>(vaccineJson);
            return json;
        }

        public ActionResult AddVaccine(Vaccine vacc)
        {
            
            string pId = Request.Form["patientId"], mId = Request.Form["medicId"];
            DataLayer dal = new DataLayer();
            
            List<ImmuneCard> card = (from x in dal.ImmuneCards
                                     where x.patientId == pId
                                     select x).ToList<ImmuneCard>();

            List<Medic> medic = (from x in dal.medics
                                     where x.Id == User.Identity.Name
                                     select x).ToList<Medic>();

            if (ModelState.IsValid)
            {
                vacc.card = card[0];
                vacc.medic = medic[0];

                card[0].Vaccines.Add(vacc);
                ViewBag.message = "Vaccine added succesfully.";
                dal.SaveChanges();
                
            }
            else
            {
                ViewBag.message = "Invalid vaccine information.";
            }

            vacc = new Vaccine();
            return View("AddVaccine", vacc);
        }

    }


}