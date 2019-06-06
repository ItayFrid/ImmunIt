using ImmunIt.Classes;
using ImmunIt.DAL;
using ImmunIt.Models;
using ImmunIt.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

//TODO:Add Manager Register
namespace ImmunIt.Controllers
{
    //[Authorize(Roles = "Manager")]
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
            TripleDES des = new TripleDES();
            if (ModelState.IsValid)
            {
                string hashedPassword = des.TripleEncrypt(patient.Password);      //Encrypting user's password
                if (!userExists(patient.Id))     //Adding user to database
                {
                    patient.card = new ImmuneCard
                    {
                        patientId = patient.Id,
                        Vaccines = new List<Vaccine>()
                    };
                    patient.Password = hashedPassword;
                    patient = AES.EncryptPatient(patient);
                    dal.ImmuneCards.Add(patient.card);
                    patient.card.patientId = patient.Id;
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
            TripleDES des = new TripleDES();
            if (ModelState.IsValid)
            {
                string hashedPassword = des.TripleEncrypt(medic.Password);      //Encrypting user's password
                if (!userExists(medic.Id))     //Adding user to database
                {
                    medic.Password = hashedPassword;
                    medic.isEmailVerified = false;
                    medic.ActivationCode = Guid.NewGuid();
                    SendVerification(medic.email,medic.ActivationCode.ToString());
                    medic = AES.EncryptMedic(medic);
                    dal.medics.Add(medic);
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
            users = AES.DecryptUserList(users);
            foreach (User user in dal.users)
                if (user.Id.Equals(id))
                    return true;
            return false;
        }

        public ActionResult ViewAllUsers()
        {
            ViewModel vm = new ViewModel();
            DataLayer dal = new DataLayer();

            vm.patients = AES.DecryptPatientList((from x in dal.patients
                                                  select x).ToList<Patient>());
            vm.medics = AES.DecryptMedicList((from x in dal.medics
                                              select x).ToList<Medic>());
            vm.users = AES.DecryptUserList((from x in dal.users
                                            where x.role == "Manager"
                                            select x).ToList<User>());
            return View(vm);
        }
        [NonAction]
        private void SendVerification(string email,string activationCode)
        {
            var verifyUrl = "/Home/VerifyAccount/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            string subject = "Registration Verification - ImmUnit";
            string body = "To finish the registration please click the link<br/>"+
                "<a href='"+link+"'>"+link+"</a>";
            SendEmail(email, subject, body);
        }
        [NonAction]
        private void SendEmail(string toEmail, string subject, string body)
        {
            string sender = "immunit7@gmail.com";
            string password = "fitay123";
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(sender, password)
            };
            MailMessage msg = new MailMessage(sender, toEmail, subject, body)
            {
                IsBodyHtml = true,
                BodyEncoding = UTF8Encoding.UTF8
            };
            client.Send(msg);
        }
    }
}