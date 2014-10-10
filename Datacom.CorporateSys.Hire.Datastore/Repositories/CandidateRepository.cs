using System.Linq;
using Datacom.CorporateSys.Hire.Domain.Models;

namespace Datacom.CorporateSys.Hire.Datastore.Repositories
{
    public class CandidateRepository : OneHireBaseRepository, ICandidateRepository
    {
        public Candidate GetCandidate(string emailAddress)
        {
            return DbContext.Candidates.FirstOrDefault(x => x.Email == emailAddress);
        }
    }
}