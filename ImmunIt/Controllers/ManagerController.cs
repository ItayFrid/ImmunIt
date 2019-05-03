﻿using ImmunIt.Classes;
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


        public ActionResult AddPatient()
        {
            Patient patient = new Patient();
            return View("AddPatient", patient);
        }

        public ActionResult PatientRegister(Patient patient)
        {
            DataLayer dal = new DataLayer();
            Encryption enc = new Encryption();
            if (ModelState.IsValid)
            {
                string hashedPassword = enc.CreateHash(patient.Password);      //Encrypting user's password
                if (!userExists(patient.Id))     //Adding user to database
                {
                    ImmuneCard icard = new ImmuneCard();
                    icard.Patient = patient;
                    icard.ImmuneCardId = patient.Id;
                    icard.Vaccines = new List<Vaccine>();
                    
                    patient.role = "Patient";
                    patient.card = icard;
                    patient.Password = hashedPassword;

                    dal.ImmuneCards.Add(icard);
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

        /*This function redirects to medic register page*/
        public ActionResult AddMedic()
        {
            Medic medic = new Medic();
            return View("AddMedic", medic);
        }

        /*This function adds new user to database*/
        /*Given user information from user register form*/
        public ActionResult MedicRegister(Medic medic)
        {
            DataLayer dal = new DataLayer();
            Encryption enc = new Encryption();

            if (ModelState.IsValid)
            {
                string hashedPassword = enc.CreateHash(medic.Password);      //Encrypting user's password
                if (!userExists(medic.Id))     //Adding user to database
                {
                    medic.Password = hashedPassword;
                    dal.medics.Add(medic);
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

        /*This function compares given id with id's in database*/
        private bool userExists(string id)
        {
            DataLayer dal = new DataLayer();
            List<User> users = dal.users.ToList<User>();
            foreach (User user in dal.users)
                if (user.Id.Equals(id))
                    return true;
            return false;
        }

        public ActionResult ViewAllUsers()
        {
            return View();
        }


    }
   
}