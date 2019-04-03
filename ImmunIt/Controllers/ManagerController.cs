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
        public ActionResult Login()
        {
            Manager mng = new Manager();
            return View(mng);
        }

        public ActionResult ManagerLogin(Manager mng)
        {

            DataLayer dal = new DataLayer();
            //Encryption enc = new Encryption();
            List<Manager> ManagerToCheck = (from x in dal.Managers
                                         where x.UserName == mng.UserName
                                         select x).ToList<Manager>();       //Attempting to get user information from database
            if (ManagerToCheck.Count != 0)     //In case username was found
            {
                // if (enc.ValidatePassword(user.password, userToCheck[0].password))   //Correct password
                //{
                var authTicket = new FormsAuthenticationTicket(
                    1,                                  // version
                    mng.UserName,                      // user name
                    DateTime.Now,                       // created
                    DateTime.Now.AddMinutes(20),        // expires
                    true,       //keep me connected
                    ManagerToCheck[0].role                       // store roles
                    );

                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(authCookie);
                return RedirectToAction("Index","Manager");
                //}

            }
            else
                ViewBag.UserLoginMessage = "Incorrect Username/password";
            return View("ManagerLogin", mng);

        }
        public ActionResult AddMedic()
        {
            return View();
        }
        public ActionResult AddPatient()
        {
            return View();
        }
        public ActionResult ViewAllUsers()
        {
            return View();
        }
    }
}