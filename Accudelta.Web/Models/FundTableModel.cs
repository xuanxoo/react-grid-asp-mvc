using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accudelta.Web.Models
{
    public class FundTableModel
    {
        public int TotalCount { get; set; }
        public int Limit { get; set; }

        public IEnumerable<FundModel> Funds { get; set; }
    }
}