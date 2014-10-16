using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datacom.CorporateSys.Hire.Domain.Models;

namespace Datacom.CorporateSys.Hire.Datastore.Mappings
{
    public class ExamMapping:EntityTypeConfiguration<Exam>
    {
        public ExamMapping()
        {

            ToTable("Exam", "Hire");

            //must specify this or annotate the property
            HasKey(k => new { k.Id });


            //many to many Exam<>Question
            HasMany<Question>(s => s.Questions).WithMany(c => c.Exams).Map(c =>
            {
                c.MapLeftKey("ExamId");
                c.MapRightKey("QuestionId");
                c.ToTable("ExamQuestion","Hire");
            });

            //many to many Exam<>Category
            HasMany<Category>(s => s.Categories).WithMany(c => c.Exams).Map(c =>
            {
                c.MapLeftKey("ExamId");
                c.MapRightKey("CategoryId");
                c.ToTable("ExamCategory","Hire");
            });

            //one to many Candidate>Exam
            HasRequired<Candidate>(s => s.Candidate)
            .WithMany(s => s.Exams).HasForeignKey(s => s.CandidateId);

        }
    }
}
