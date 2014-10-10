using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datacom.CorporateSys.Hire.Domain.Models;

namespace Datacom.CorporateSys.Hire.Datastore.Mappings
{
    public class CandidateMapping:EntityTypeConfiguration<Candidate>
    {
        public CandidateMapping()
        {
            ToTable("Candidate", "Hire");

            HasKey(k => new { k.Id });
        }
    }
}
