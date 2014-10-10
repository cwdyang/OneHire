using System;
using Datacom.CorporateSys.Hire.Datastore.Contexts;

namespace Datacom.CorporateSys.Hire.Datastore.Repositories
{
    public class OneHireBaseRepository : IOneHireBaseRepository
    {

        public IOneHireMainContext DbContext { get; set; }

        public OneHireBaseRepository()
        {
            DbContext = new OneHireMainContext();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}