using System.Linq;
using Datacom.CorporateSys.Hire.Datastore.Contexts;
using Datacom.CorporateSys.Hire.Domain.Models;

namespace Datacom.CorporateSys.Hire.Datastore.Repositories
{
    public class CandidateRepository : OneHireBaseRepository, ICandidateRepository
    {
        public Candidate GetCandidate(string emailAddress)
        {
            return DbContext.Candidates.FirstOrDefault(x => x.Email == emailAddress);
        }

        public CandidateRepository()
        {
        }

        public CandidateRepository(IOneHireMainContext context)
        {
            base.DbContext = context;
        }
    }
}