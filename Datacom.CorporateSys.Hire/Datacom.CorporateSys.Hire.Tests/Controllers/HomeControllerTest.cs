using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Datacom.CorporateSys.Hire.Domain.Models;
using Datacom.CorporateSys.HireAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Datacom.CorporateSys.Hire;
using Datacom.CorporateSys.Hire.Controllers;
using Answer = Datacom.CorporateSys.HireAPI.Answer;
using Category = Datacom.CorporateSys.HireAPI.Category;
using Exam = Datacom.CorporateSys.HireAPI.Exam;


namespace Datacom.CorporateSys.Hire.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.AreEqual("Modify this template to jump-start your ASP.NET MVC application.", result.ViewBag.Message);
        }

        [TestMethod]
        public void GetExamFromAPI()
        {
            var examService = new ExamService();
            var candidateService = new CandidateService();

            var exam = examService.GetLatestOpenExamWithQuestionOptions(candidateService.GetCandidate("davidy@datacom.co.nz").Id);

            exam.Questions.ToList().ForEach(x => x.Options.ToList().ForEach(y =>
            {
                var relatedQuestions = examService.GetRelatedQuestions(y.Id);

                var answer = new Answer
                {
                    AnswerText = string.Empty,
                    Level = x.Level,
                    ScorePoint = x.ScorePoint,
                    Id = Guid.NewGuid(),
                    
                    //need to use DbContext.QuestionsOption.Attach() if you want to use this
                    Option = new Option {Id = y.Id,IsSelected = y.IsSelected},
                    Exam = new Exam { Id = exam.Id, Text = exam.Text }
                };

                var answerReturned = examService.AddAnswer(answer);

                Debug.WriteLine(relatedQuestions.Count);
            }));

            Assert.IsNotNull(exam);
        }

        [TestMethod]
        public void GetCategories()
        {
            var examService = new ExamService();

            var categories = examService.GetCategories(Enumerable.Empty<Guid>().ToList());
        }
    }


     
}
