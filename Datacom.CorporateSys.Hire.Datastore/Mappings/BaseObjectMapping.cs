using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datacom.CorporateSys.Hire.Domain.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Datacom.CorporateSys.Hire.Datastore.Mappings
{
    public class BaseObjectMapping:EntityTypeConfiguration<BaseObject>
    {
        public BaseObjectMapping()
        {
            
            ToTable("BaseObject", "Hire");

            //must specify this or annotate the property
            HasKey(k => new { k.Id });

            //this is shared with Category and Question
            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            HasOptional<BaseObject>(s => s.ParentBaseObject)
            .WithMany(s => s.BaseObjects).HasForeignKey(s => s.ParentId);
        }
    }
}
