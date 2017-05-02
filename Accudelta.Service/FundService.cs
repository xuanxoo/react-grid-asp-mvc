using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accudelta.Data.Interface;
using Accudelta.Model;
using System.Data.SqlClient;

namespace Accudelta.Service
{
    public class FundService : IFundService
    {
        private IRepository<V_FundWithLastValue> fundRepository;

        public FundService(IRepository<V_FundWithLastValue> fundRepository)
        {
            this.fundRepository = fundRepository;
        }

        #region Funds

        public IEnumerable<V_FundWithLastValue> GetFunds(int offset, int limit)
        {
            return fundRepository.PaginatedList(f => f.Id, offset, limit);
        }

        public int CountFunds()
        {
            return fundRepository.Counter();
        }

         #endregion

        }
    }
