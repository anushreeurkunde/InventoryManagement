using BLL;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace DIContainer
{
    public class DependencyMapping
    {
        private readonly IUnityContainer _unityContainer;

        public DependencyMapping(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        public void PopulateContainer()
        {
            _unityContainer.RegisterType<IInventoryContext, InventoryContext>();
            _unityContainer.RegisterType<IProductService, ProductService>();
        }
    }
}
