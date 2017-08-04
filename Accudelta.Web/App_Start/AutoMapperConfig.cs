using AutoMapper;
using Accudelta.Web.Mappings;

namespace Accudelta.Web
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => {
                cfg.AddProfile<DomainToViewModelMapping>();
            });
        }
    }
}