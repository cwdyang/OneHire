using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Datacom.CorporateSys.Hire.ViewModels;
using Datacom.CorporateSys.HireAPI;

namespace Datacom.CorporateSys.Hire.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Exam()
        {
            var candidateService = new CandidateService();
            var examService = new ExamService();

            var candidate = candidateService.GetCandidate("davidy@datacom.co.nz");
            var exam = examService.GetLatestOpenExamWithQuestionOptions(candidate.Id);

            var viewModel = new ExamViewModel(candidate, exam);

            return View(viewModel);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
