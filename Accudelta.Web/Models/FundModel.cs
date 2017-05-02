using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Accudelta.Web.Models
{
    public class FundModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal LastValue { get; set; }
        public string DateValue { get; set; }


    }
}