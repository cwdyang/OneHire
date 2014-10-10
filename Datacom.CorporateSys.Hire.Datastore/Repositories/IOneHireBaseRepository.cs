using System;
using Datacom.CorporateSys.Hire.Datastore.Contexts;

namespace Datacom.CorporateSys.Hire.Datastore.Repositories
{
    public interface IOneHireBaseRepository:IDisposable
    {
        IOneHireMainContext DbContext { get; set; }
    
    }
}