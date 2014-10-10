using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AutoMapper;

namespace Datacom.CorporateSys.HireAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ExamService : IExamService
    {
        static ExamService()
        {
            Mapper.CreateMap<Question, Hire.Domain.Models.Question>()
                .ForMember(o => o.ScorePoint, opt => opt.MapFrom(d => d.ScorePoint))
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id));

            Mapper.CreateMap<Exam, Hire.Domain.Models.Exam>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Examiner, opt => opt.MapFrom(d => d.Examiner))
                .ForMember(o => o.Questions, opt => opt.MapFrom(d => d.Questions));
        }

        public Exam GetLatestOpenExam(Guid candidateGuid)
        {
            return null;
        }
    }
}
