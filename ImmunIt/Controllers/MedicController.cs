using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImmunIt.Controllers
{
    public class MedicController : Controller
    {
        // GET: Medic
        public ActionResult Index()
        {
            return View();
        }
    }
}