using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AutoMapper;
using Datacom.CorporateSys.Hire.Datastore.Repositories;
using Datacom.CorporateSys.Hire.Domain.Models;

namespace Datacom.CorporateSys.HireAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ExamService : IExamService
    {
        static ExamService()
        {

            Mapper.CreateMap<Hire.Domain.Models.QuestionOption, Option>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.IsSelected, opt => opt.MapFrom(d => d.IsSelected))
                .ForMember(o => o.Text, opt => opt.MapFrom(d => d.Caption));

            Mapper.CreateMap<Hire.Domain.Models.Question, Question>()
                .ForMember(o => o.DataType, opt => opt.MapFrom(d => d.DataType))
                .ForMember(o => o.ImageUri, opt => opt.MapFrom(d => d.ImageUri))
                .ForMember(o => o.Level, opt => opt.MapFrom(d => d.Level))
                .ForMember(o => o.Questions, opt => opt.Ignore())
                .ForMember(o => o.Text, opt => opt.Ignore())
                .ForMember(o => o.ScorePoint, opt => opt.MapFrom(d => d.ScorePoint))
                .ForMember(o => o.CategoryName, opt => opt.ResolveUsing<CategoryNameResolver>())
                .ForMember(o => o.Options, opt => opt.MapFrom(d => d.QuestionOptions))
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id));

            Mapper.CreateMap<Hire.Domain.Models.Exam,Exam>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.CompletedOn, opt => opt.MapFrom(d => d.CompletedOn))
                .ForMember(o => o.Categories, opt => opt.Ignore())
                 .ForMember(o => o.CreatedOn, opt => opt.MapFrom(d => d.CreatedOn))
                 .ForMember(o => o.StartedOn, opt => opt.MapFrom(d => d.StartedOn))
                .ForMember(o => o.Examiner, opt => opt.MapFrom(d => d.Examiner))
                .ForMember(o => o.Questions, opt => opt.MapFrom(d => d.Questions));
        }

        public Exam GetLatestOpenExam(Guid candidateGuid)
        {
            Exam examToReturn;

            using (var examRepo = new ExamRepository())
            {
                var openExam = examRepo.GetLatestOpenExam(candidateGuid, true, true);

                examToReturn = Mapper.Map<Exam>(openExam);
            }

            return examToReturn;
        }

        public List<Question> GetRelatedQuestions(Guid questionGuid)
        {
            List<Question> list = Enumerable.Empty<Question>().ToList();

            using (var examRepo = new ExamRepository())
            {
            }

            return list;
        }
    }
}
