using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datacom.CorporateSys.Hire.Datastore.Contexts;
using Datacom.CorporateSys.Hire.Domain.Models;

namespace Datacom.CorporateSys.Hire.Datastore.Repositories
{
    public class ExamRepository :OneHireBaseRepository, IExamRepository
    {
        public ExamRepository()
        {
        }

        public ExamRepository(IOneHireMainContext context)
        {
            base.DbContext = context;
        }

        public List<Question> GetSubQuestions(Guid qusetionOptionId, bool loadQuetionOptions = false)
        {
            IQueryable<Question> questionQueryable = null;
            var list = Enumerable.Empty<Question>().ToList();

            questionQueryable = loadQuetionOptions ? DbContext.Questions.Include(x => x.QuestionOptions) : DbContext.Questions;

            list = questionQueryable.Where(x => x.QuestionOptionId == qusetionOptionId).ToList();

            list.ForEach(y=>DbContext.Entry(y).Reference(z=>z.Category).Load());

            return list;
        }

        public List<Exam> GetExams(Guid candidateId, bool loadQuestions = false,bool loadQuetionOptions = false)
        {
            //eager loading coz lazy loading turned off
            IQueryable<Candidate> candidateQueryable = null;
            Candidate candidate = null;

            //eager loading
            //http://msdn.microsoft.com/en-nz/data/jj574232.aspx
            //http://stackoverflow.com/questions/19319116/include-vs-load-performance-in-entityframework
            //candidate  = DbContext.Candidates.Include(c => c.Exams).FirstOrDefault(x => x.Id == candidateId);
            //DbContext.Entry(candidate).Collection(x => x.Exams).Load();

            //MULTI LEVEL nested eager loading YAYAY!
            if (loadQuestions && loadQuetionOptions)
                candidateQueryable = DbContext.Candidates.Include(c => c.Exams.Select(s => s.Questions.Select(q => q.QuestionOptions)));
            else if (loadQuestions)
                candidateQueryable = DbContext.Candidates.Include(c => c.Exams.Select(s => s.Questions));
            else
                candidateQueryable = DbContext.Candidates;

            if (candidateQueryable != null)
               candidate = candidateQueryable.FirstOrDefault(x => x.Id == candidateId);

            var exams = (candidate != null) ? candidate.Exams: Enumerable.Empty<Exam>();


            //this is a hack, need a better solution if larger numbers are requrieed
            exams.ToList().ForEach(x=>x.Questions.ToList().ForEach(y=>DbContext.Entry(y).Reference(z=>z.Category).Load()));

            return exams.ToList();
        }

        public Exam GetLatestOpenExam(Guid candidateId, 
            bool loadQuestions = false,
            bool loadQuetionOptions = false)
        {
            var exam = GetExams(candidateId,loadQuestions,loadQuetionOptions).OrderByDescending(x => x.CreatedOn).FirstOrDefault(x => x.CompletedOn == null);

            return exam;
        }

        public Answer AddAnswer(Answer answer)
        {
            //prevents re-insertion

            answer.QuestionOption = DbContext.QuestionOptions.First(x => x.Id == answer.QuestionOption.Id);
            answer.Exam = DbContext.Exams.First(x => x.Id == answer.Exam.Id);

            DbContext.QuestionOptions.Attach(answer.QuestionOption);
            DbContext.Exams.Attach(answer.Exam);

            DbContext.Answers.AddOrUpdate(answer);

            DbContext.SaveChanges();

            return answer;
        }
    }
}
