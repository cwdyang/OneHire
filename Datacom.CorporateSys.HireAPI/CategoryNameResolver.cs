using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;

namespace Datacom.CorporateSys.HireAPI
{
    public class CategoryNameResolver : ValueResolver<Datacom.CorporateSys.Hire.Domain.Models.Question, string>
    {
        protected override string ResolveCore(Hire.Domain.Models.Question source)
        {
            return source.Category.Caption;
        }
    }
}
