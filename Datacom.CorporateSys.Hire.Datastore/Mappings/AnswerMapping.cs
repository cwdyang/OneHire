using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datacom.CorporateSys.Hire.Domain.Models;

namespace Datacom.CorporateSys.Hire.Datastore.Mappings
{
    public class AnswerMapping:EntityTypeConfiguration<Answer>
    {
        public AnswerMapping()
        {
            ToTable("Answer", "Hire");

            //must specify this or annotate the property
            HasKey(k => new { k.Id });

            //this is shared with Category and Question
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //one to many QuestionOption>Answer
            HasRequired<QuestionOption>(s => s.QuestionOption)
            .WithMany(s => s.Answers).HasForeignKey(s => s.QuestionOptionId);

            //one to many exam>Answer
            HasRequired<Exam>(s => s.Exam)
            .WithMany(s => s.Answers).HasForeignKey(s => s.ExamId);
        }
    }
}
