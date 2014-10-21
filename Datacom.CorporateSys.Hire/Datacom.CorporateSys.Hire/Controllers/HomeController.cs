using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Datacom.CorporateSys.Hire.ViewModels;
using Datacom.CorporateSys.HireAPI;

namespace Datacom.CorporateSys.Hire.Controllers
{
    public class HomeController : ExamController
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome!";

            if (User.Identity.IsAuthenticated && (ViewModel==null||ViewModel.Candidate == null))
                return RedirectToAction("LogOut", "Account");

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About...";

            return View();
        }

        
        public ActionResult Contact()
        {
            ViewBag.Message = "Contact...";

            return View();
        }
    }
}
