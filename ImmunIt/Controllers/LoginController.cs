using ImmunIt.Classes;
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
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        /*This function redirects to user login page*/
        public ActionResult UserLogin()
        {
            User user = new User();
            ViewBag.UserLoginMessage = "";
            return View(user);
        }

        /*This function handles user login*/
        /*Given information from user login form*/
        public ActionResult Login(User user)
        {

            DataLayer dal = new DataLayer();
            Encryption enc = new Encryption();
            List<User> userToCheck = (from x in dal.users
                                      where x.Id == user.Id
                                      select x).ToList<User>();       //Attempting to get user information from database
            if (userToCheck.Count != 0)     //In case username was found
            {
                if (enc.ValidatePassword(user.Password, userToCheck[0].Password))   //Correct password
                {
                    var authTicket = new FormsAuthenticationTicket(
                        1,                                  // version
                        user.Id,                      // user id
                        DateTime.Now,                       // created
                        DateTime.Now.AddMinutes(20),        // expires
                        true,       //keep me connected
                        userToCheck[0].role                       // store roles
                        );

                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);

                    if (userToCheck[0].role.Equals("Manager"))
                        return RedirectToAction("Index", "Manager");

                    else if (userToCheck[0].role.Equals("Medic"))
                        return RedirectToAction("Index", "Medic");

                    else
                        return RedirectToAction("Index", "Patient");
                }
                else
                {
                    ViewBag.UserLoginMessage = "Incorrect Username/password";
                }
            }
            else
                ViewBag.UserLoginMessage = "Incorrect Username/password";
            return View("UserLogin", user);
        }

        /*This function handles signing out*/
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToRoute("HomePage");
        }
    }
}