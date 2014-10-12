using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datacom.CorporateSys.Hire.Domain.Models;

namespace Datacom.CorporateSys.Hire.Datastore.Repositories
{
    public class ExamRepository :OneHireBaseRepository, IExamRepository
    {

        public List<Question> GetSubQuestions(Guid qusetionId, bool loadQuetionOptions = false)
        {
            IQueryable<Question> questionQueryable = null;
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

            //this is a hack, need a better solution
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
    }
}
