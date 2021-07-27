using DomainModels;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Tests
{
    class ProductDBSet: TestDbSet<Product>
    {
        public override async Task<Product> FindAsync(params object[] keyValues)
        {
            return await Task.FromResult(this.FirstOrDefault(x => x.Id == (int)keyValues.Single()));
        }
    }
}
