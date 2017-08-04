using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Accudelta.Data.Interface;

namespace Accudelta.Data.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
