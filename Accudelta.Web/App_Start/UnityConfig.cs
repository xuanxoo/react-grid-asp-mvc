using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Accudelta.Model;
using Accudelta.Data.Interface;
using Accudelta.Data.Base;

namespace Accudelta.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<IUnitOfWorkFactory, EFUnitOfWorkFactory>();
            container.RegisterType(typeof(IRepository<>), typeof(EFRepository<>));
            EFUnitOfWorkFactory.SetObjectContext(() => new Entities());
            container.LoadConfiguration();
            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container)); //unity for mvc controllers
        }
    }
}