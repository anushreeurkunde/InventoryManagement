using DIContainer;
using System.Web.Http;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace InventoryManagement
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // e.g. container.RegisterType<ITestService, TestService>();
            DependencyMapping dependencyMapping = new DependencyMapping(container);
            dependencyMapping.PopulateContainer();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }
    }
}