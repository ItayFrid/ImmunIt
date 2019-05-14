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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DataLayer dal = new DataLayer();
            ViewModel vm = new ViewModel();
          //  vm.patients = (from x in dal.patients
          //                 select x).ToList<Patient>();
            return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        public ActionResult WhatIsVac()
        {
            return View();
        }


        public ActionResult WhyVac()
        {
            return View();
        }
        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            Guid guid = new Guid(id);
            DataLayer dal = new DataLayer();
            Medic user = (from x in dal.medics
                          where x.ActivationCode == guid
                          select x).ToList<Medic>()[0];
            if (user != null)
            {
                user.isEmailVerified = true;
                dal.SaveChanges();
            }
            return View(user);
        }
    }
}