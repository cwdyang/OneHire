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
        protected ExamViewModel ViewModel
        {
            get { return Session.GetDataFromSession<ExamViewModel>(SessionConstants.ExamViewModel); }
            set { Session.SetDataToSession<ExamViewModel>(SessionConstants.ExamViewModel, value); }
        }

        public ActionResult Exam(int? questionNumber)
        {

            if (ViewModel == null)
            {
                var candidateService = new CandidateService();
                var examService = new ExamService();

                var candidate = candidateService.GetCandidate("davidy@datacom.co.nz");

                if (candidate == null)
                    return new HttpStatusCodeResult(404); //return new EmptyResult(); //preferred

                var exam = examService.GetLatestOpenExamWithQuestionOptions(candidate.Id);

                if (exam == null)
                    return new HttpStatusCodeResult(404); //return new EmptyResult(); //preferred

                exam.CurrentQuestionId = exam.Questions.First().Id;

                ViewModel = new ExamViewModel(candidate, exam);

            }

            ViewModel.Exam.CurrentQuestionNumber = (questionNumber) ?? 1;

            return View(ViewModel);
        }

        [HttpPost]
        public ActionResult AnswerQuestion(Question question)
        {
            if (ViewModel == null)
            {
                return new HttpStatusCodeResult(404);
            }

            Option optionSelected = null;

            try
            {
                optionSelected = Newtonsoft.Json.JsonConvert.DeserializeObject<Option>(question.SelectedOptionJSON);

            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(404);
            }
            
            var parentQuestion = ViewModel.Exam.Questions.First(x => x.Id == optionSelected.ParentQuestionId);

            var answer = new Answer { AnswerText = optionSelected.Text, Exam = ViewModel.Exam, Id = Guid.NewGuid(), Level = parentQuestion.Level, Option = optionSelected, ScorePoint = parentQuestion.ScorePoint, Text = optionSelected.Text };

            var examService = new ExamService();

            examService.AddAnswer(answer);

            parentQuestion.SelectedOption = optionSelected;



            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
