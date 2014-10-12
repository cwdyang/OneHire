using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Datacom.CorporateSys.Hire.Datastore.Mappings;
using Datacom.CorporateSys.Hire.Domain.Models;

namespace Datacom.CorporateSys.Hire.Datastore.Contexts
{
    /// <summary>
    /// To enable database creation:
    /// 
    /// in Package managment console:
    /// 
    /// run
    /// 
    /// Enable-Migrations
    /// Add-Migration "name of the migration, can be anything" > ends up @
    /// c:\development\onehire\Datacom.CorporateSys.Hire.Datastore\Migrations\201410062249468_Hire.cs
    /// 
    /// Add-Migration InitialMigrations 
    /// 
    /// generates a blank up/down() and 
    /// 
    /// Update-Database runs new changess and runs seed method
    /// 
    /// To take down the database:
    /// 
    /// ALTER DATABASE OneHire SET OFFLINE WITH
    /// ROLLBACK IMMEDIATE
    /// drop database OneHire
    /// go
    /// 
    /// 
    /// we are using
    /// Inheritance with EF Code First: Part 3 – Table per Concrete Type (TPC)
    /// 
    /// </summary>
    public class OneHireMainContext : DbContext, IOneHireMainContext
    {

        public string ConnectionString
        {
            get { return Database.Connection.ConnectionString; }
            private set { Database.Connection.ConnectionString = value; }
        }

        public IDbSet<BaseObject> BaseObjects { get; set; }
        public IDbSet<Question> Questions { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<Candidate> Candidates { get; set; }
        public IDbSet<Exam> Exams { get; set; }
        public IDbSet<QuestionOption> QuestionOptions { get; set; }
        public IDbSet<Answer> Answers { get; set; }


        //YOU NEED TO INSTALL EF6 nuget in the host application
        public OneHireMainContext()
            : base("name=dbOneHire")
		{
			Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<OneHireMainContext>(null);

            /*
             * performance 
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.ValidateOnSaveEnabled = false;
            */

            if (!Database.Exists())
            {
                ((IObjectContextAdapter)this).ObjectContext.CreateDatabase();
            }

		}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Note the order of configuration matters
            //http://entityframework.codeplex.com/workitem/1646
            modelBuilder.Configurations.Add(new BaseObjectMapping());
            modelBuilder.Configurations.Add(new CategoryMapping());
            modelBuilder.Configurations.Add(new QuestionMapping());
            modelBuilder.Configurations.Add(new CandidateMapping());

            modelBuilder.Configurations.Add(new ExamMapping());
            modelBuilder.Configurations.Add(new QuestionOptionMapping());
            modelBuilder.Configurations.Add(new AnswerMapping());

            
        }

        

    }

    public static class DbContextExtensions
    {
        public static void DeleteAll<T>(this DbContext context)
            where T : class
        {
            foreach (var p in context.Set<T>())
            {
                context.Entry(p).State = EntityState.Deleted;
            }
        }
    }
}
