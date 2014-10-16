using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Datacom.CorporateSys.HireAPI
{
    [ServiceContract]
    public interface IExamService
    {
       
        [OperationContract]
        Exam GetLatestOpenExamWithQuestionOptions(Guid candidateGuid);

        [OperationContract]
        bool HasOpenExams(Guid candidateGuid);

        [OperationContract]
        List<Category> GetCategories(List<Guid> categoryIds);

        [OperationContract]
        Exam GenerateExam(List<Guid> categoryIds, Guid candidateGuid, string examiner);

        [OperationContract]
        Answer AddAnswer(Answer answer);

        [OperationContract]
        Exam CompleteExam(Exam exam,Candidate candidate);


    }
}