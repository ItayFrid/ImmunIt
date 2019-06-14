
using ImmunIt.Classes;
using ImmunIt.DAL;
using ImmunIt.Models;
using ImmunIt.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            ViewModel vm = new ViewModel();
            vm.vaccinesJson = getVaccines();
            return View(vm);
        }

        public ActionResult WatchPastVaccines()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            //Getting current patient refrence
            List<Patient> patients = (from x in dal.patients
                                      select x).ToList<Patient>();
            Patient patient = null;
            foreach (Patient p in patients)
                if (AES.Decrypt(p.Id) == User.Identity.Name)
                    patient = p;
            vm.patient = AES.DecryptPatient(patient);
            vm.immuneCard = vm.patient.card;
            List<Vaccine> vaccines = vm.immuneCard.Vaccines.ToList<Vaccine>();
            for (int i = 0; i < vaccines.Count; i++)
                if(vaccines[i].medic.Name.Contains(":"))
                    vaccines[i].medic = AES.DecryptMedic(vaccines[i].medic);
            vm.immuneCard.Vaccines = vaccines;
            return View(vm);
        }

        public List<VaccineJson> getVaccines()
        {
            string vaccineJson = new WebClient().DownloadString("https://immunit.000webhostapp.com/vaccines.json");
            List<VaccineJson> json = JsonConvert.DeserializeObject<List<VaccineJson>>(vaccineJson);
            foreach (VaccineJson vacc in json)
            {
                vacc.Name = AES.Decrypt(vacc.Name);
                vacc.Description = AES.Decrypt(vacc.Description);
            }
            return json;
        }
    }
}