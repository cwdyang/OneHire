using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Datacom.CorporateSys.Hire.Domain.Models;

namespace Datacom.CorporateSys.Hire.Datastore.Contexts
{
    public interface IOneHireMainContext
    {
        string ConnectionString { get; }


        IDbSet<BaseObject> BaseObjects { get; set; }
        IDbSet<Question> Questions { get; set; }
        IDbSet<Category> Categories { get; set; }
        IDbSet<Candidate> Candidates { get; set; }

        IDbSet<Exam> Exams { get; set; }
        IDbSet<QuestionOption> QuestionOptions { get; set; }
        IDbSet<Answer> Answers { get; set; }

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class ;

        void Dispose();
		int SaveChanges();
    }
}
