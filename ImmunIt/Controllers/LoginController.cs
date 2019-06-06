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
            TripleDES des = new TripleDES();
            List<User> userToCheck = (from x in dal.users
                                      select x).ToList<User>();       //Attempting to get user information from database
            User usrToCheck = null;
            for(int i = 0; i < userToCheck.Count; i++)
                if (AES.Decrypt(userToCheck[i].Id) == user.Id)
                    usrToCheck = AES.DecryptUser(userToCheck[i]);

            if (usrToCheck != null)     //In case username was found
            {
                if (des.isValid(usrToCheck.Password, user.Password))   //Correct password
                {
                    var authTicket = new FormsAuthenticationTicket(
                        1,                                  // version
                        user.Id,                            // user id
                        DateTime.Now,                       // created
                        DateTime.Now.AddMinutes(20),        // expires
                        true,                               //keep me connected
                        usrToCheck.role                 // store roles
                        );

                    string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);

                    if (usrToCheck.role.Equals("Manager"))
                        return RedirectToAction("Index", "Manager");

                    else if (usrToCheck.role.Equals("Medic"))
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
            return RedirectToAction("Index", "Home");
        }
    }
}