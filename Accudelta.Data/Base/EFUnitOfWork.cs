using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Accudelta.Data.Interface;

namespace Accudelta.Data.Base
{
    public class EFUnitOfWork : IUnitOfWork, IDisposable
    {
        public DbContext Context { get; private set; }

        public EFUnitOfWork(DbContext context)
        {
            Context = context;
            context.Configuration.LazyLoadingEnabled = true;
            //context.Configuration.LazyLoadingEnabled = true;;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
                Context = null;
            }

            GC.SuppressFinalize(this);
        }
    }
}
