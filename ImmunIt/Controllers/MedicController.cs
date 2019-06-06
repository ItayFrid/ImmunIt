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
            List<Medic> medics = (from x in dal.medics
                                    select x).ToList<Medic>();
            Medic medic = null;
            foreach (Medic m in medics)
                if (AES.Decrypt(m.Id) == User.Identity.Name)
                    medic = AES.DecryptMedic(m);
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
                                      select x).ToList<Patient>();
            Patient patient = null;
            foreach (Patient p in patients)
                if (AES.Decrypt(p.Id) == id)
                    patient = AES.DecryptPatient(p);
            if (patients.Count != 0)
                return View("PatientPage", patient);
            ViewBag.Search = "Patient Not Found";
            return RedirectToAction("SearchPatient", "Medic");
        }

        public ActionResult PatientPage(Patient patient)
        {
            return View(patient);
        }


        public List<VaccineJson> getVaccines()
        {
            string vaccineJson = new WebClient().DownloadString("https://immunit.000webhostapp.com/vaccines.json");
            List<VaccineJson> json = JsonConvert.DeserializeObject<List<VaccineJson>>(vaccineJson);
            foreach(VaccineJson vacc in json)
            {
                vacc.Name = AES.Decrypt(vacc.Name);
                vacc.Description = AES.Decrypt(vacc.Description);
            }
            return json;
        }
        public ActionResult Vaccine()
        {
            string pId = Request.Form["patientId"];
            ViewModel vm = new ViewModel();
            DataLayer dal = new DataLayer();
            vm.vaccine = new Vaccine();
            vm.patients = (from x in dal.patients
                           select x).ToList<Patient>();
            foreach (Patient p in vm.patients)
                if (AES.Decrypt(p.Id) == pId)
                    vm.patient = AES.DecryptPatient(p);
            return View("AddVaccine", vm);
        }
        public ActionResult AddVaccine(ViewModel vm)
        {
            DataLayer dal = new DataLayer();
            string pId = Request.Form["patientId"], mId = User.Identity.Name;
            List<ImmuneCard> cards = (from x in dal.ImmuneCards
                                        select x).ToList<ImmuneCard>();
            ImmuneCard card = null;
            foreach (ImmuneCard c in cards)
                if (AES.Decrypt(c.patientId) == pId)
                    card = c;
            List<Medic> medics = (from x in dal.medics
                                     select x).ToList<Medic>();
            Medic medic = null;
            foreach (Medic m in medics)
                if (AES.Decrypt(m.Id) == User.Identity.Name)
                    medic = m;
            vm.patients = (from x in dal.patients
                          select x).ToList<Patient>();
            foreach (Patient p in vm.patients)
                if (AES.Decrypt(p.Id) == pId)
                    vm.patient = p;
            if (ModelState.IsValid)
            {
                vm.vaccine = AES.EncryptVaccine(vm.vaccine);
                vm.patient.card.Vaccines.Add(vm.vaccine);
                vm.vaccine.card = vm.patient.card;
                vm.vaccine.medic = medic;
                ViewBag.message = "Vaccine added succesfully.";
                dal.SaveChanges();
                vm.patient = AES.DecryptPatient(vm.patient);
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