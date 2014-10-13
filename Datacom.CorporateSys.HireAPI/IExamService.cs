using System;
using System.ServiceModel;

namespace Datacom.CorporateSys.HireAPI
{
    [ServiceContract]
    public interface IExamService
    {
       
        [OperationContract]
        Exam GetLatestOpenExamWithQuestionOptions(Guid candidateGuid);
    }
}