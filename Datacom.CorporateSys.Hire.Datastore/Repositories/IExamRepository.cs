using System;
using System.Collections.Generic;
using Datacom.CorporateSys.Hire.Domain.Models;

namespace Datacom.CorporateSys.Hire.Datastore.Repositories
{
    public interface IExamRepository:IOneHireBaseRepository
    {
        List<Exam> GetExams(Guid candidateId, 
            bool loadQuestions = false, 
            bool loadQuetionOptions = false);
        Exam GetLatestOpenExam(Guid candidateId,
            bool loadQuestions = false,
            bool loadQuetionOptions = false);
    }
}