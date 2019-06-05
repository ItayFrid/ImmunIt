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
using ImmunIt.Classes;

namespace ImmunIt.Controllers
{

    [Authorize(Roles = "Medic")]
    public class MedicController : Controller
    {
        public ActionResult Index()
        {
            DataLayer dal = new DataLayer();
            Medic medic = (from x in dal.medics
                           where x.Id == User.Identity.Name
                           select x).ToList<Medic>()[0];
            return View(medic);
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
            AES aes = new AES();
            string vaccineJson = new WebClient().DownloadString("https://immunit.000webhostapp.com/vaccines.json");
            List<VaccineJson> json = JsonConvert.DeserializeObject<List<VaccineJson>>(vaccineJson);
            foreach(VaccineJson vacc in json)
            {
                vacc.Name = aes.Decrypt(vacc.Name);
                vacc.Description = aes.Decrypt(vacc.Description);
            }
            return json;
        }
        public ActionResult Vaccine()
        {
            string pId = Request.Form["patientId"];
            ViewModel vm = new ViewModel();
            DataLayer dal = new DataLayer();
            vm.vaccine = new Vaccine();
            vm.patient = new Patient();
            vm.patient = (from x in dal.patients
                          where x.Id == pId
                          select x).ToList<Patient>()[0];
            return View("AddVaccine", vm);
        }
        public ActionResult AddVaccine(ViewModel vm)
        {
            DataLayer dal = new DataLayer();
            string pId = Request.Form["patientId"], mId = User.Identity.Name;
            List<ImmuneCard> card = (from x in dal.ImmuneCards
                                        where x.patientId == pId
                                        select x).ToList<ImmuneCard>();
            List<Medic> medic = (from x in dal.medics
                                     where x.Id == User.Identity.Name
                                     select x).ToList<Medic>();

            vm.patient = (from x in dal.patients
                          where x.Id == pId
                          select x).ToList<Patient>()[0];
            if (ModelState.IsValid)
            {
                vm.patient.card.Vaccines.Add(vm.vaccine);
                vm.vaccine.card = vm.patient.card;
                vm.vaccine.medic = medic[0];
                ViewBag.message = "Vaccine added succesfully.";
                dal.SaveChanges();
                return View("PatientPage",vm.patient);
            }
            else
            {
                ViewBag.message = "Invalid vaccine information.";
                return View("AddVaccine", vm);
            }
        }

    }


}