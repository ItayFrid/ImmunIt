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
    }
}