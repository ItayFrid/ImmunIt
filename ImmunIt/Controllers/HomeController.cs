using ImmunIt.Classes;
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
            AddRootAdmin();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
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
            user = AES.DecryptMedic(user);
            return View(user);
        }
        [NonAction]
        private void AddRootAdmin()
        {
            DataLayer dal = new DataLayer();
            TripleDES des = new TripleDES();
            if (dal.users.Count() == 0)
            {
                Manager root = new Manager
                {
                    Name = AES.Encrypt("Manager"),
                    Id = AES.Encrypt("111111111"),
                    Password = des.TripleEncrypt("123"),
                    role = "Manager"
                };
                dal.managers.Add(root);
                dal.SaveChanges();
            }
        }
    }
}