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

        public IEnumerable<V_FundWithLastValue> GetFundsPaginated(int offset, int limit, string query)
        {
            return FundQueryResults(query).OrderBy(f => f.Id).Skip(offset).Take(limit);
        }

        public int CountFunds(string query)
        {
            return FundQueryResults(query).Count();           
        }

        private IQueryable<V_FundWithLastValue> FundQueryResults(string query)
        {
            if (string.IsNullOrEmpty(query))
                return fundRepository.GetQuery();

            return fundRepository.GetQuery().Where(f => f.Name.ToLower().Contains(query.ToLower()));            
        }

         #endregion

        }
    }
