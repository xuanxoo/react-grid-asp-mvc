//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Accudelta.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class FundValues
    {
        public int Id { get; set; }
        public int FundId { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Value { get; set; }
    
        public virtual Fund Fund { get; set; }
    }
}
