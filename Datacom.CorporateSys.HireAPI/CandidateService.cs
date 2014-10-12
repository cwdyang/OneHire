using AutoMapper;
using Datacom.CorporateSys.Hire.Datastore.Repositories;

namespace Datacom.CorporateSys.HireAPI
{
    public class CandidateService : ICandidateService
    {
        static CandidateService()
        {
            Mapper.CreateMap<Hire.Domain.Models.Candidate, Candidate>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.FirstName, opt => opt.MapFrom(d => d.FirstName))
                .ForMember(o => o.LastName, opt => opt.MapFrom(d => d.LastName))
                .ForMember(o => o.Email, opt => opt.MapFrom(d => d.Email));

        }

        public Candidate GetCandidate(string emailAddress)
        {
            Candidate candidateToReturn = null;

            using (var candidateRepo = new CandidateRepository())
            {
                var candidate = candidateRepo.GetCandidate(emailAddress);

                candidateToReturn = Mapper.Map<Candidate>(candidate);
            }

            return candidateToReturn;
        }
    }
}