using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using BLL;
using DAL;
using DomainModels;
using InventoryManagement.CustomAttributes;

namespace InventoryManagement.Controllers
{
    [EnableCorsAttribute("*","*","*")]
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;
       
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [AllowAnonymous]
        // GET: api/Products
        public IQueryable<Product> GetProducts()
        {
            return _productService.GetProducts();
        }

        [AllowAnonymous]
        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            Product product = await _productService.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [AdminAuthentication]
        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            try
            {
               await _productService.PutProduct(id, product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_productService.ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [AdminAuthentication]
        // POST: api/Products
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productService.PostProduct(product);

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        [AdminAuthentication]
        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            Product product = await _productService.DeleteProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            
            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}