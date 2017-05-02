using System;
using AutoMapper;
using Accudelta.Model;
using Accudelta.Web.Models;
using Accudelta.Web.Code;

namespace Accudelta.Web.Mappings
{
    public class DomainToViewModelMapping : Profile
    {
        public DomainToViewModelMapping()
        {
            CreateMap<V_FundWithLastValue, FundModel>()
               .ForMember(vm => vm.Id, map => map.MapFrom(d => d.Id))
               .ForMember(vm => vm.Description, map => map.MapFrom(d => d.Description))
               .ForMember(vm => vm.Name, map => map.MapFrom(d => d.Name))
               .ForMember(vm => vm.LastValue, map => map.MapFrom(d => Utils.StringFromDecimal(d.Value.Value)))
               .ForMember(vm => vm.DateValue, map => map.MapFrom(d => Utils.StringFromDatetime(d.Date)))
               ;


        }
    }
}