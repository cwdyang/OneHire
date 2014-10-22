using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI.WebControls;
using Datacom.CorporateSys.Hire.Constants;
using Datacom.CorporateSys.Hire.Helpers;
using Datacom.CorporateSys.Hire.Models;
using Datacom.CorporateSys.Hire.ViewModels;
using Datacom.CorporateSys.HireAPI;
using Newtonsoft.Json;
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

        protected DateTime FormAuthTimeout 
        {
            get
            {
                try
                {
                    return FormsAuthentication.Decrypt(Request.Cookies[System.Web.Security.FormsAuthentication.FormsCookieName].Value).Expiration;
                }
                catch (Exception)
                {
                    return DateTime.MinValue;
                }
                
            }
        }

        //http://blog.stevensanderson.com/2010/01/28/editing-a-variable-length-list-aspnet-mvc-2-style/
        [SessionCheckFilter]
        public ActionResult CategoryTree(IEnumerable<Guid> categoryIds, Guid? candidateId, bool recurse=false)
        {
           
            ViewModel.Categories = _examService.GetCategories((categoryIds == null) ? Enumerable.Empty<Guid>().ToList() : categoryIds.ToList()); ;

            return View(ViewModel);
        }

        [SessionCheckFilter]
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
            var exam = _examService.GenerateExam(categoryIds, ViewModel.Candidate.Id, Request.Form["ExaminerEmail"]);

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

        [HttpPost]
        public ActionResult Dialog2(DialogModel model)
        {
            return ProcessDialog(model, 2,"xyz");
        }

        public ActionResult Dialog2()
        {
            return PartialView();
        }

        ActionResult ProcessDialog(DialogModel model, int answer, string message)
        {
            if (ModelState.IsValid)
            {
                if (model.Value == answer)
                    return this.DialogResult(message);  // Close dialog via DialogResult call
                else
                    ModelState.AddModelError("", string.Format("Invalid input value. The correct value is {0}", answer));
            }

            return PartialView(model);
        }

        [SessionCheckFilter]
        public ActionResult Exam(int? questionNumber)
        {

            if( ViewModel.Exam == null)
            {

                var exam = _examService.GetLatestOpenExamWithQuestionOptions(ViewModel.Candidate.Id);

                if (exam == null)
                    return RedirectToAction("CategoryTree", "Exam");

                exam.CurrentQuestionId = exam.Questions.First().Id;

                exam.StartedOn = DateTimeOffset.Now;

                ViewModel.Exam = exam;

            }

            ViewModel.Exam.CurrentQuestionNumber = (questionNumber) ?? 1;

            return View(ViewModel);
        }


        [AjaxAuthorize]
        [HttpPost]
        public ActionResult GetSubQuestion(Guid optionId)
        {
            

            if (ViewModel == null || ViewModel.Candidate == null||ViewModel.Exam == null)
                return new EmptyResult();

            
            ViewData["IsLastQuestion"] = false;

            var option = ViewModel.Exam.Questions.SelectMany(x => x.Options).FirstOrDefault(x => x.Id == optionId);
           
            if (option != null)
            {
                var subQuestion = option.Questions.FirstOrDefault() ?? _examService.GetSubQuestions(optionId).FirstOrDefault();
                
                if(subQuestion==null)
                    return new EmptyResult();
                
                var quesiton = ViewModel.Exam.Questions.FirstOrDefault(x => x.Options.Any(y=>y.Id==optionId&&y.IsSelected));

                if (quesiton != null)
                {
                    quesiton.SelectedOptionJSON = new JavaScriptSerializer().Serialize(option);

                    var result = AnswerQuestion(quesiton);

                    if (quesiton.SelectedOption.Questions.FirstOrDefault() == null)
                        quesiton.SelectedOption.Questions.Add(subQuestion);
                }

                if (option.Questions.FirstOrDefault()==null)
                    option.Questions.Add(subQuestion);

                return PartialView("QuestionControl", subQuestion);
            }
            else
                return new EmptyResult();

        }

        [HttpPost]
        [SessionCheckFilter]
        public ActionResult AnswerQuestion(Question question)
        {

            Option optionSelected = null;

            try
            {
                optionSelected = Newtonsoft.Json.JsonConvert.DeserializeObject<Option>(question.SelectedOptionJSON);
            }
            catch (Exception ex)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            
            var parentQuestion = ViewModel.Exam.Questions.FirstOrDefault(x => x.Id == optionSelected.ParentQuestionId) ??
                                 ViewModel.Exam.Questions.SelectMany(t => t.Options)
                .SelectMany(x => x.Questions)
                .FirstOrDefault(x => x.Id == optionSelected.ParentQuestionId);


            var answer = new Answer { AnswerText = optionSelected.Text, Exam = ViewModel.Exam, Id = Guid.NewGuid(), Level = parentQuestion.Level, Option = optionSelected, ScorePoint = parentQuestion.ScorePoint, Text = optionSelected.Text };

            _examService.AddAnswer(answer);

            parentQuestion.SelectedOption = optionSelected;

            if (ViewModel.Exam.Questions.All(x => x.SelectedOption != null))
                return CompleteExamInternal();

            var nextUnanweredQuestion = ViewModel.Exam.Questions.Where(x=>x.SelectedOption==null).OrderBy(x=>x.Sequence).FirstOrDefault();

            ViewModel.Exam.CurrentQuestionNumber = (nextUnanweredQuestion != null) ? nextUnanweredQuestion.Sequence : 1;

            //return Redirect(Request.UrlReferrer.ToString());
            return RedirectToAction("Exam", "Exam", new { questionNumber = ViewModel.Exam.CurrentQuestionNumber});
        }

        [SessionCheckFilter]
        private ActionResult CompleteExamInternal()
        {

            _examService.CompleteExam(ViewModel.Exam, ViewModel.Candidate);
            ViewModel.Exam = null;
            ViewModel.Categories = null;

            return RedirectToAction("CompleteExam", "Exam");
        }

        public ActionResult CompleteExam()
        {

            ViewBag.Message = "Your exam is complete, the examiner/interviewer will be in touch shortly.";

            return View(ViewModel);

        }


    }
}
