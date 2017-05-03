using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accudelta.Model;

namespace Accudelta.Service
{
    public interface IFundService
    {
        IEnumerable<V_FundWithLastValue> GetFunds(int offset, int limit, string query);
        int CountFunds(string query);
    }
}
