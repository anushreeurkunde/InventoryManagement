using DAL;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductService : IProductService
    {
        
        private readonly IInventoryContext _inventoryContext;

        public ProductService(IInventoryContext inventoryContext)
        {
            _inventoryContext = inventoryContext;
        }

        public IQueryable<Product> GetProducts()
        {
            return _inventoryContext.Products;
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _inventoryContext.Products.FindAsync(id);
        }

        public async Task<Product> PostProduct(Product product)
        {
            _inventoryContext.Products.Add(product);
            await _inventoryContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> PutProduct(int id, Product product)
        {
            _inventoryContext.MarkAsModified(product);
            await _inventoryContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteProduct(int id)
        {
            Product product = await _inventoryContext.Products.FindAsync(id);
            _inventoryContext.Products.Remove(product);
            await _inventoryContext.SaveChangesAsync();
            return product;
        }
        

        public bool ProductExists(int id)
        {
            return _inventoryContext.Products.Count(e => e.Id == id) > 0;
        }
        
        public void Dispose()
        {
            _inventoryContext.Dispose();
        }

    }
}
