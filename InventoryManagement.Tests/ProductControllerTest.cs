using BLL;
using DomainModels;
using InventoryManagement.Controllers;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace InventoryManagement.Tests
{
    class ProductControllerTest
    {
        Mock<IProductService> _productService;
        ProductDBSet _listProducts = null;

        [SetUp]
        public void SetUp()
        {
            _productService = new Mock<IProductService>();
        }

        [TearDown]
        public void TearDown()
        {
            _listProducts = null;
        }

        [Test]
        public void Test_GetAllProducts()
        {
            _listProducts = new ProductDBSet
            {
            new Product{Id=1, Name= "Test1", Brand="Test1", Description= "Test1", Price= 300},
            new Product{Id=2, Name= "Test2", Brand="Test2", Description= "Test2", Price= 400},
            new Product{Id=3, Name= "Test3", Brand="Test3", Description= "Test3", Price= 500},
            new Product{Id=4, Name= "Test4", Brand="Test4", Description= "Test4", Price= 600}
            };

            _productService.Setup(x => x.GetProducts()).Returns(_listProducts);

            var controller = new ProductsController(_productService.Object);
            var result = controller.GetProducts() as ProductDBSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count());
        }

        [Test]
        public void Test_AddProduct()
        {
            var controller = new ProductsController(_productService.Object);

            Product prod = new Product { Id = 5, Name = "Test5", Brand = "Test5", Description = "Test5", Price = 300 };

            var result = (controller.PostProduct(prod).GetAwaiter().GetResult()) as CreatedAtRouteNegotiatedContentResult<Product>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.Id);
            Assert.AreEqual(result.Content.Name, prod.Name);
        }

        [Test]
        public void Test_PutProduct()
        {
            var controller = new ProductsController(_productService.Object);

            var item = new Product { Id = 5, Name = "Test5", Brand = "Test5", Description = "Test5", Price = 300 };

            var result = controller.PutProduct(item.Id, item).GetAwaiter().GetResult() as StatusCodeResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(StatusCodeResult), result);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Test]
        public void Test_Put_WhenDifferentId()
        {
            var controller = new ProductsController(_productService.Object);
            var item = new Product { Id = 5, Name = "Test5", Brand = "Test5", Description = "Test5", Price = 300 };
            
            Assert.DoesNotThrow(
                     () =>
                     {
                         var result = controller.PutProduct(999, item).GetAwaiter().GetResult()
                                                                 as BadRequestResult;
                     });
        }

        [Test]
        public void Test_GetProductById()
        {

            _productService.Setup(x => x.GetProduct(It.IsAny<int>())).Returns(Task.FromResult(new Product { Id = 5, Name = "Test5", Brand = "Test5", Description = "Test5", Price = 300 }));
            var controller = new ProductsController(_productService.Object);
            var result = controller.GetProduct(5).GetAwaiter().GetResult() as OkNegotiatedContentResult<Product>;

            Assert.IsNotNull(result);
            Assert.AreEqual(5, result.Content.Id);
        }

        [Test]
        public void Test_Delete_NotFound()
        {

            _productService.Setup(x => x.DeleteProduct(It.IsAny<int>())).Returns(Task.FromResult<Product>(null));
            var controller = new ProductsController(_productService.Object);
            var result = controller.DeleteProduct(3).GetAwaiter().GetResult() as NotFoundResult;

            Assert.IsNotNull(result);
        }
    }
}
