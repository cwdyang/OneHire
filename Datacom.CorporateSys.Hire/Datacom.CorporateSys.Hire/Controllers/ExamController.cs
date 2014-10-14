using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Datacom.CorporateSys.Hire.Constants;
using Datacom.CorporateSys.Hire.Helpers;
using Datacom.CorporateSys.Hire.ViewModels;
using Datacom.CorporateSys.HireAPI;
using WebGrease.Css.Extensions;

namespace Datacom.CorporateSys.Hire.Controllers
{
    public class ExamController : Controller
    {
        public ActionResult Exam(int? questionNumber)
        {
            var viewModel = Session.GetDataFromSession<ExamViewModel>(SessionConstants.ExamViewModel);

            if (viewModel == null)
            {
                var candidateService = new CandidateService();
                var examService = new ExamService();

                var candidate = candidateService.GetCandidate("davidy@datacom.co.nz");

                if (candidate == null)
                    return new EmptyResult(); //preferred

                var exam = examService.GetLatestOpenExamWithQuestionOptions(candidate.Id);

                if (exam == null)
                    return new EmptyResult(); //preferred

                exam.CurrentQuestionId = exam.Questions.First().Id;
                
                viewModel = new ExamViewModel(candidate, exam);
                Session.SetDataToSession<ExamViewModel>(SessionConstants.ExamViewModel, viewModel);
            }

            viewModel.Exam.CurrentQuestionNumber = (questionNumber) ?? 1;

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AnswerQuestion(Question question)
        {
            return new EmptyResult();
        }
    }
}
