using System.ServiceModel;

namespace Datacom.CorporateSys.HireAPI
{
    [ServiceContract]
    public interface ICandidateService
    {

        [OperationContract]
        Candidate GetCandidate(string emailAddress);
    }

    public class CandidateService : ICandidateService
    {
        public Candidate GetCandidate(string emailAddress)
        {
            return new Candidate();
        }
    }
}