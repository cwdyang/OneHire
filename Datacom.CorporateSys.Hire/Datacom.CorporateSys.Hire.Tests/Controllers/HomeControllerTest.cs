using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Datacom.CorporateSys.HireAPI;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Datacom.CorporateSys.Hire;
using Datacom.CorporateSys.Hire.Controllers;

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

            var exam = examService.GetLatestOpenExam(candidateService.GetCandidate("davidy@datacom.co.nz").Id);
            Assert.IsNotNull(exam);
        }

       
    }
}
