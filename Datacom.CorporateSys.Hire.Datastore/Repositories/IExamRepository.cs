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

        Exam GenerateExam(List<Guid> categoryIds, Guid candidateGuid, string examiner);

        Exam CompleteExam(Guid examId);

        List<Category> GetCategories(List<Guid> categoryIds);
    }
}