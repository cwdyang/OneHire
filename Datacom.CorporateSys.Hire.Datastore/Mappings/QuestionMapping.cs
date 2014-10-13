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
    public class QuestionMapping:EntityTypeConfiguration<Question>
    {
        public QuestionMapping()
        {

            ToTable("Question", "Hire");

            //one to many Category>Question
            HasRequired<Category>(s => s.Category)
            .WithMany(s => s.Questions).HasForeignKey(s => s.CategoryId);

            //one to many optional QuestionOption>question
            HasOptional<QuestionOption>(e => e.QuestionOption)
            .WithMany(s => s.Questions).HasForeignKey(s => s.QuestionOptionId);

        }
    }
}
