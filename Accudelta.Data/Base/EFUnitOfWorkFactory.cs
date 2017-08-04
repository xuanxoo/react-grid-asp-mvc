using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Accudelta.Data.Interface;

namespace Accudelta.Data.Base
{
    public class EFUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private static Func<DbContext> _objectContextDelegate;
        private static readonly Object _lockObject = new object();
        

        public static void SetObjectContext(Func<DbContext> objectContextDelegate)
        {
            _objectContextDelegate = objectContextDelegate;
        }

        public static IUnitOfWork Create()
        {
            DbContext context;

            lock (_lockObject)
            {
                context = _objectContextDelegate();
            }

            return new EFUnitOfWork(context);
        }
    }
}
