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
    public class QuestionOptionMapping:EntityTypeConfiguration<QuestionOption>
    {
        public QuestionOptionMapping()
        {
            ToTable("QuestionOption", "Hire");

            //must specify this or annotate the property
            HasKey(k => new { k.Id });

            //this is shared with Category and Question
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            //one to many Question>QuestionOption
            HasRequired<Question>(s => s.Question)
            .WithMany(s => s.QuestionOptions).HasForeignKey(s => s.QuestionId);
        }
    }
}
