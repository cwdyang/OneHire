using System;
using Datacom.CorporateSys.Hire.Datastore.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Datacom.CorporateSys.Hire.Datastore.Tests
{
    [TestClass]
    public class OneHireRepoTest
    {
        [TestInitialize]
        public void Setup()
        {
            
        }

        [TestMethod]
        public void GetExams()
        {
            using(var examRepo = new ExamRepository())
            using (var candidateRepo = new CandidateRepository())
            {

                var candidate = candidateRepo.GetCandidate("davidy@datacom.co.nz");

                Assert.IsNotNull(candidate);

                var openExam = examRepo.GetLatestOpenExam(candidate.Id, true, true);

                Assert.IsNotNull(openExam);
            }

        }
    }
}
