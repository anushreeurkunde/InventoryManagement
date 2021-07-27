using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IProductService : IDisposable
    {
        IQueryable<Product> GetProducts();
        Task<Product> GetProduct(int id);
        Task<Product> PutProduct(int id, Product product);
        Task<Product> PostProduct(Product product);
        Task<Product> DeleteProduct(int id);
        bool ProductExists(int id);
    }
}
