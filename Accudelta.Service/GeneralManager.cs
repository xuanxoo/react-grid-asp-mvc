using System;
using Accudelta.Data.Base;

namespace Accudelta.Service
{
    public class GeneralManager
    {
        public static void Commit()
        {
            UnitOfWork.Commit();
        }

        public static void Dispose()
        {
            UnitOfWork.Current.Dispose();
        }
    }
}
