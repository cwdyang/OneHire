using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
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

        //http://blog.stevensanderson.com/2010/01/28/editing-a-variable-length-list-aspnet-mvc-2-style/
        public ActionResult CategoryTree(IEnumerable<Guid> categoryIds, Guid? candidateId, bool recurse=false)
        {

            var categories = _examService.GetCategories((categoryIds==null)?Enumerable.Empty<Guid>().ToList(): categoryIds.ToList());

            if (ViewModel == null || ViewModel.Candidate == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewModel.Categories = categories;
            }


            return View(ViewModel);
        }

        public ActionResult GenerateExam(FormCollection items)
        {
            var categoryIds = new List<Guid>();

            foreach (var key in items.AllKeys)
            {
                var value = Request.Form[key];

                if(value.StartsWith("true"))
                    categoryIds.Add(new Guid(key));
            }

            //TODO
            var exam = _examService.GenerateExam(categoryIds, ViewModel.Candidate.Id, "davidy@datacom.co.nz");

            return RedirectToAction("Exam", "Exam");
        }

        //for injection
        public ExamController(ICandidateService candidateService, IExamService examService)
        {
            _candidateService = candidateService;
            _examService = examService;
        }

        readonly ICandidateService _candidateService = new CandidateService();
        readonly IExamService _examService = new ExamService();

        public ExamController()
        {
            _candidateService = new CandidateService();
            _examService = new ExamService();
        }

        public ActionResult Exam(int? questionNumber)
        {

            if (ViewModel == null|| ViewModel.Candidate == null )
                return RedirectToAction("Exam", "Exam");

            if( ViewModel.Exam == null)
            {

                var exam = _examService.GetLatestOpenExamWithQuestionOptions(ViewModel.Candidate.Id);

                if (exam == null)
                    return new HttpStatusCodeResult(404); //return new EmptyResult(); //preferred

                exam.CurrentQuestionId = exam.Questions.First().Id;

                ViewModel.Exam = exam;

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

            

            _examService.AddAnswer(answer);

            parentQuestion.SelectedOption = optionSelected;

            if (ViewModel.Exam.Questions.All(x=>x.SelectedOption!=null))
                ViewModel.Exam = _examService.CompleteExam(ViewModel.Exam,ViewModel.Candidate);

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}
