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

        public IEnumerable<V_FundWithLastValue> GetFunds(int offset, int limit, string query)
        {
            if(string.IsNullOrEmpty(query))       
                return fundRepository.PaginatedList(f => f.Id, null, offset, limit);
            else
                return fundRepository.PaginatedList(f => f.Id, f => f.Name.Contains(query), offset, limit);
        }

        public int CountFunds(string query)
        {
            if (string.IsNullOrEmpty(query))
                return fundRepository.GetQuery().Count();
            return fundRepository.GetQuery().Where(x => x.Name.Contains(query)).Count();           
        }

         #endregion

        }
    }
