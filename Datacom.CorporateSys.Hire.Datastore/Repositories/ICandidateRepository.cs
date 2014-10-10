using Datacom.CorporateSys.Hire.Domain.Models;

namespace Datacom.CorporateSys.Hire.Datastore.Repositories
{
    public interface ICandidateRepository : IOneHireBaseRepository
    {
       
        Candidate GetCandidate(string emailAddress);

    }
}