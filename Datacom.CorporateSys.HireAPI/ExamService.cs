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
                .ForMember(o => o.Questions, opt => opt.MapFrom(d => d.Questions))
                .ForMember(o => o.ParentQuestionId, opt => opt.MapFrom(d => d.QuestionId))
                .ForMember(o => o.Text, opt => opt.MapFrom(d => d.Caption));

            Mapper.CreateMap<Option, Hire.Domain.Models.QuestionOption>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.IsSelected, opt => opt.MapFrom(d => d.IsSelected))
                .ForMember(o => o.Questions, opt => opt.MapFrom(d => d.Questions))
                .ForMember(o => o.QuestionId, opt => opt.MapFrom(d => d.ParentQuestionId))
                .ForMember(o => o.Caption, opt => opt.MapFrom(d => d.Text));

            Mapper.CreateMap<Hire.Domain.Models.Answer, Answer>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.AnswerText, opt => opt.MapFrom(d => d.AnswerText))
                .ForMember(o => o.Text, opt => opt.MapFrom(d => d.Caption))
                .ForMember(o => o.Option, opt => opt.MapFrom(d => d.QuestionOption))
                .ForMember(o => o.Exam, opt => opt.MapFrom(d => d.Exam))
                .ForMember(o => o.ScorePoint, opt => opt.MapFrom(d => d.ScorePoint));

            Mapper.CreateMap<Answer, Hire.Domain.Models.Answer>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.AnswerText, opt => opt.MapFrom(d => d.AnswerText))
                .ForMember(o => o.Caption, opt => opt.MapFrom(d => d.Text))
                .ForMember(o => o.QuestionOption, opt => opt.MapFrom(d => d.Option))
                .ForMember(o => o.Exam, opt => opt.MapFrom(d => d.Exam))
                .ForMember(o => o.ScorePoint, opt => opt.MapFrom(d => d.ScorePoint));

            Mapper.CreateMap<Hire.Domain.Models.Question, Question>()
                .ForMember(o => o.DataType, opt => opt.MapFrom(d => d.DataType))
                .ForMember(o => o.ImageUri, opt => opt.MapFrom(d => d.ImageUri))
                .ForMember(o => o.Level, opt => opt.MapFrom(d => d.Level))
                .ForMember(o => o.Questions, opt => opt.Ignore())
                .ForMember(o => o.SelectedOption, opt => opt.Ignore())
                .ForMember(o => o.SelectedOptionJSON, opt => opt.Ignore())
                .ForMember(o => o.Sequence, opt => opt.MapFrom(d => d.DisplaySequence))
                .ForMember(o => o.Text, opt => opt.MapFrom(d => d.Caption))
                .ForMember(o => o.ScorePoint, opt => opt.MapFrom(d => d.ScorePoint))
                .ForMember(o => o.CategoryName, opt => opt.ResolveUsing<CategoryNameResolver>())
                .ForMember(o => o.Options, opt => opt.MapFrom(d => d.QuestionOptions))
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id));

            Mapper.CreateMap<Question, Hire.Domain.Models.Question>()
               .ForMember(o => o.DataType, opt => opt.MapFrom(d => d.DataType))
               .ForMember(o => o.ImageUri, opt => opt.MapFrom(d => d.ImageUri))
               .ForMember(o => o.Level, opt => opt.MapFrom(d => d.Level))
               .ForMember(o => o.DisplaySequence, opt => opt.Ignore())
               .ForMember(o => o.Caption, opt => opt.MapFrom(d => d.Text))
               .ForMember(o => o.ScorePoint, opt => opt.MapFrom(d => d.ScorePoint))
               .ForMember(o => o.QuestionOptions, opt => opt.MapFrom(d => d.Options))
               .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id));

            Mapper.CreateMap<Hire.Domain.Models.Candidate, Candidate>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Email, opt => opt.MapFrom(d => d.Email))
                .ForMember(o => o.FirstName, opt => opt.MapFrom(d => d.FirstName))
                .ForMember(o => o.LastName, opt => opt.MapFrom(d => d.LastName))
                .ForMember(o => o.Exams, opt => opt.MapFrom(d => d.Exams));

            Mapper.CreateMap<Candidate, Hire.Domain.Models.Candidate>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Email, opt => opt.MapFrom(d => d.Email))
                .ForMember(o => o.FirstName, opt => opt.MapFrom(d => d.FirstName))
                .ForMember(o => o.LastName, opt => opt.MapFrom(d => d.LastName))
                .ForMember(o => o.Exams, opt => opt.MapFrom(d => d.Exams));

            Mapper.CreateMap<Hire.Domain.Models.Exam,Exam>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.CompletedOn, opt => opt.MapFrom(d => d.CompletedOn))
                .ForMember(o => o.Categories, opt => opt.Ignore())
                .ForMember(o => o.Candidate, opt => opt.MapFrom(d => d.Candidate))
                .ForMember(o => o.Text, opt => opt.MapFrom(d => "Exam"))
                 .ForMember(o => o.CreatedOn, opt => opt.MapFrom(d => d.CreatedOn))
                 .ForMember(o => o.StartedOn, opt => opt.MapFrom(d => d.StartedOn))
                .ForMember(o => o.Examiner, opt => opt.MapFrom(d => d.Examiner))
                .ForMember(o => o.Questions, opt => opt.MapFrom(d => d.Questions));


            Mapper.CreateMap<Exam, Hire.Domain.Models.Exam>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.CompletedOn, opt => opt.MapFrom(d => d.CompletedOn))
                .ForMember(o => o.Categories, opt => opt.Ignore())
                .ForMember(o => o.Candidate, opt => opt.MapFrom(d => d.Candidate))
                 .ForMember(o => o.CreatedOn, opt => opt.MapFrom(d => d.CreatedOn))
                 .ForMember(o => o.StartedOn, opt => opt.MapFrom(d => d.StartedOn))
                .ForMember(o => o.Examiner, opt => opt.MapFrom(d => d.Examiner))
                .ForMember(o => o.Questions, opt => opt.MapFrom(d => d.Questions));

            Mapper.CreateMap<Category, Hire.Domain.Models.Category>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Caption, opt => opt.MapFrom(d => d.Text));

            Mapper.CreateMap<Hire.Domain.Models.BaseObject, Category>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Text, opt => opt.MapFrom(d => d.Caption));

            Mapper.CreateMap<Hire.Domain.Models.Category, Category>()
                .ForMember(o => o.Id, opt => opt.MapFrom(d => d.Id))
                .ForMember(o => o.Categories, opt => opt.MapFrom(d => d.BaseObjects))
                .ForMember(o => o.Text, opt => opt.MapFrom(d => d.Caption));
        }

        public bool HasOpenExams(Guid candidateGuid)
        {
            using (var examRepo = new ExamRepository())
            {
                var openExam = examRepo.GetLatestOpenExam(candidateGuid, false, false);
                return (openExam != null);
            }
        }

        public Exam GetLatestOpenExamWithQuestionOptions(Guid candidateGuid)
        {
            Exam examToReturn;

            using (var examRepo = new ExamRepository())
            {
                var openExam = examRepo.GetLatestOpenExam(candidateGuid, true, true);

                examToReturn = Mapper.Map<Exam>(openExam);
            }

            if (examToReturn != null)
            {
                var i = 1;
                var list = examToReturn.Questions.ToList().OrderBy(x => x.Sequence);
                list.ToList().ForEach(x => { x.Sequence = i++; });
            }

            return examToReturn;
        }

        public IList<Question> GetRelatedQuestions(Guid questionOptionGuid)
        {
            var list = Enumerable.Empty<Question>().ToList();

            using (var examRepo = new ExamRepository())
            {
                var questions = examRepo.GetSubQuestions(questionOptionGuid,true);
                list = Mapper.Map<List<Question>>(questions);
            }

            return list;
        }

        public Answer AddAnswer(Answer answer)
        {
            
            using (var examRepo = new ExamRepository())
            {
                var questions = examRepo.AddAnswer(Mapper.Map<Hire.Domain.Models.Answer>(answer));
            }
            return answer;
        }

        public Exam CompleteExam(Exam exam,Candidate candidate)
        {
            var message = new StringBuilder();

            exam.TotalPossiblePoints = exam.Questions.Sum(x => x.ScorePoint);
            exam.TotalScoredPoints = exam.Questions.Where(x => x.SelectedOption!=null && x.SelectedOption.IsSelected).Sum(x => x.ScorePoint);
            exam.TotalQuestionsAsked = exam.Questions.Count();
            exam.TotalQuestionsAnswered = exam.Questions.Count(x => x.SelectedOption != null);
            exam.TotalQuestionsAnsweredCorrectly = exam.Questions.Count(x => x.SelectedOption != null && x.SelectedOption.IsSelected);

            Exam examReturend = null;

            using (var examRepo = new ExamRepository())
            {
                examReturend = Mapper.Map<Exam>( examRepo.CompleteExam(exam.Id));
            }

            exam.CompletedOn = examReturend.CompletedOn;

            message.Append(string.Format("Interview exam results for: {0} {1} {2}",
                candidate.FirstName, candidate.LastName, "<br/><br/>"));

            message.Append(string.Format("Candidate email: {0}{1}",
                candidate.Email, "<br/><br/>"));

            message.Append("<hr>");

            message.Append(string.Format("Exam {2}Start: {0}{2}Finish{1} {2}{2}",
               exam.StartedOn.GetValueOrDefault().ToString("yyyy-MMM-dd HH:mm:ss"),
               exam.CompletedOn.GetValueOrDefault().ToString("yyyy-MMM-dd HH:mm:ss"),
               "<br/>"));

            message.Append(string.Format("Total Score: {0} / {1} Points. {2}",
               exam.TotalScoredPoints,
               exam.TotalPossiblePoints,
               "<br/><br/>"));

            message.Append(string.Format("Questions Asked: {0}{3} Answered: {1}{3} Correct: {2} {3}{3}{4}",
                exam.TotalQuestionsAsked,
                exam.TotalQuestionsAnswered,
                exam.TotalQuestionsAnsweredCorrectly,
              "<br/>","<hr>"));


            var categoryResults = exam.Questions.GroupBy(x=>x.CategoryName).Select(
                x => new
                {
                    CategoryName = x.Key,
                    TotalQuestionsAsked = x.Count(),
                    TotalQuestionsAnswered = x.Count(y => y.SelectedOption != null),
                    TotalQuestionsAnsweredCorrectly = x.Count(y => y.SelectedOption != null && y.SelectedOption.IsSelected),
                    TotalPossiblePoints = x.Sum(y=>y.ScorePoint),
                    TotalScoredPoints = x.Where(y=>y.SelectedOption!=null && y.SelectedOption.IsSelected).Sum(y=>y.ScorePoint)
                }
            );

            message.Append("<hr>");

            categoryResults.ToList().ForEach(x =>
            {

                message.Append(string.Format("{0} Score: {1} / {2} Points. {3}",
                    x.CategoryName,
                    x.TotalScoredPoints,
                    x.TotalPossiblePoints,
                    "<br/><br/>"));


                message.Append(string.Format("Questions asked: {0}{3} Answered: {1}{3} Correct: {2} {3}{3}",
                    x.TotalQuestionsAsked,
                    x.TotalQuestionsAnswered,
                    x.TotalQuestionsAnsweredCorrectly,
                  "<br/>"));

            });

            message.Append("<hr>");

            sendMail(exam.Examiner, "davidy@datacom.co.nz",
                "Interview exam results for: " + candidate.FirstName + " " + candidate.LastName + " Email: " + candidate.Email, 
                message.ToString());

            return exam;
        }

        public static void sendMail(string to, string from, string subject, string body)
        {
            var msg = new System.Net.Mail.MailMessage(from, to);
            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = body;
            msg.IsBodyHtml = true;
            System.Net.Mail.SmtpClient oSmtpClient = new System.Net.Mail.SmtpClient("dnzakex2.datacom.co.nz");
            //https://social.technet.microsoft.com/Forums/en-US/1a84a06a-f1c8-40b4-ace8-1e264f218aa1/550-571-unable-to-relay-for?forum=exchangesvrsecuremessaginglegacy
            oSmtpClient.UseDefaultCredentials = true;
            oSmtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            //550 5.7.1 Unable to relay for
            oSmtpClient.Send(msg);
        }



        public List<Category> GetCategories(List<Guid> categoryIds)
        {
            var list = Enumerable.Empty<Category>().ToList();

            using (var examRepo = new ExamRepository())
            {
                var categories = examRepo.GetCategories(categoryIds);
                list = Mapper.Map<List<Category>>(categories);
            }

            return list;
        }


        public Exam GenerateExam(List<Guid> categoryIds,Guid candidateGuid,string examiner)
        {
            using (var examRepo = new ExamRepository())
            {
                var exam = examRepo.GenerateExam(categoryIds, candidateGuid, examiner);
                return Mapper.Map<Exam>(exam);
            }
        }


        public List<Question> GetQuestions(List<Guid> questionIds)
        {
            using (var examRepo = new ExamRepository())
            {
                var exams = examRepo.GetQuestions(questionIds);
                return Mapper.Map<List<Question>>(exams);
            }
        }

        public List<Question> GetSubQuestions(Guid questionOptionId)
        {
            using (var examRepo = new ExamRepository())
            {
                var questions = examRepo.GetSubQuestions( questionOptionId,true);

                var quesiontList = Mapper.Map<List<Question>>(questions);

                quesiontList.ForEach(x=>x.IsChildQuestion=true);

                return quesiontList;
            }
        }
    }
}
