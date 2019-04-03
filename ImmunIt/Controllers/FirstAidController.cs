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
    public class FirstAidController : Controller
    {
        // GET: FirstAid
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PatientPage()
        {
            return View();
        }



        public Patient search(string id,List<Patient> patients)
        {
            foreach (Patient p in patients)
            {
                if (id == p.Id)
                    return p;
            }
            return null;
        }


         public ActionResult SearchPatients(string id)
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
            vm.patients = dal.patients.ToList<Patient>();
            vm.patient = search(id, vm.patients);


            return View("PatientPage", vm);
        }





    }
}