using BLL;
using DAL;
using DomainModels;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace InventoryManagement.Tests
{

    public class ProductServiceTest
    {
        Mock<IInventoryContext> _mockInventoryContext;
        ProductDBSet _listProducts = null;

        [SetUp]
        public void SetUp()
        {
            _mockInventoryContext = new Mock<IInventoryContext>();

            _listProducts = new ProductDBSet
            {
            new Product{Id=1, Name= "Test1", Brand="Test1", Description= "Test1", Price= 300},
            new Product{Id=2, Name= "Test2", Brand="Test2", Description= "Test2", Price= 400},
            new Product{Id=3, Name= "Test3", Brand="Test3", Description= "Test3", Price= 500},
            new Product{Id=4, Name= "Test4", Brand="Test4", Description= "Test4", Price= 600}
            };

            _mockInventoryContext.Setup(x => x.Products).Returns(_listProducts);
        }

        [TearDown]
        public void TearDown()
        {
            _listProducts = null;
        }

        [Test]
        public void Test_GetProducts()
        {

            ProductService productService = new ProductService(_mockInventoryContext.Object);
            var result = productService.GetProducts().ToList();
            Assert.AreEqual(_listProducts.Count(), result.Count, $"Expecpected count was {_listProducts.Count()} but got {result.Count}");
        }

        [TestCase("Test1","Test1", 1)]
        [TestCase("Test2","Test2", 2)]
        [TestCase("Test3","Test3", 3)]
        public void Test_GetProductById(string expectedName, string expectedBrand, int id)
        {
            ProductService productService = new ProductService(_mockInventoryContext.Object);
            var result = productService.GetProduct(id).GetAwaiter().GetResult();
            Assert.AreEqual(expectedName, result.Name, $"Expected name was {expectedName} but got {result.Name}");
            Assert.AreEqual(expectedBrand, result.Brand, $"Expected name was {expectedBrand} but got {result.Brand}");
        }

        [Test]
        public void Test_GetProductById_WhenNotFound()
        {
            ProductService productService = new ProductService(_mockInventoryContext.Object);
            var result = productService.GetProduct(789).GetAwaiter().GetResult();
            Assert.IsNull(result, $"Expected null result");
        }

        [Test]
        public void Test_AddProduct()
        {
            Product prod = new Product { Id = 5, Name = "Test5", Brand = "Test5", Description = "Test5", Price = 300 };
            ProductService productService = new ProductService(_mockInventoryContext.Object);
            var result = productService.PostProduct(prod).GetAwaiter().GetResult();
            Assert.IsTrue(_listProducts.Count() > 4, "List not modified");
        }

        [Test]
        public void Test_DeleteProduct()
        {
            ProductService productService = new ProductService(_mockInventoryContext.Object);
            var result = productService.DeleteProduct(1).GetAwaiter().GetResult();

            Assert.IsFalse(_listProducts.Contains(result), $"Failed to delete the record");
            Assert.IsTrue(_listProducts.Count() < 4, $"Failed to delete the record");
        }

        [Test]
        public void Test_DeleteProduct_WhenNotExist()
        {
            ProductService productService = new ProductService(_mockInventoryContext.Object);
            var result = productService.DeleteProduct(876).GetAwaiter().GetResult();

            Assert.IsNull(result, $"Failed to delete the record");
        }

    }
}
