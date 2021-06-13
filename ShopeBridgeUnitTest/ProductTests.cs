using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ShopBridge.Controllers;
using ShopBridge.Data;
using ShopBridge.Data.Repository;
using ShopBridge.Models;
using ShopeBridgeUnitTest.Helpers;
using System.Threading.Tasks;

namespace ShopeBridgeUnitTest
{
    [TestClass]
    public class ProductTests
    {
        private InventoryContext _context;
        private ContextSetup _contextSetup;

        [TestInitialize]
        public void testSetup()
        {
            _contextSetup = new ContextSetup();
            _context = _contextSetup.GetContext();
        }

        [TestCleanup]
        public async Task CleanUp()
        {
            await _context.DisposeAsync();
        }

        [TestMethod]
        public void Save_Product_With_Valid_Data()
        {
            var product = new Product
            {
                ProductId = 0,
                ProductName = "Lapddsdtop",
                Description = "LPsds 101",
                Price = 45000,
                NumberOfItems = 122
            };
            _context.Product.Add(product);
            var result = _context.SaveChanges();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Read_Product_With_Valid_Product_id()
        {
            var product = new Product
            {
                ProductId = 1,
                ProductName = "test",
                Description = "LPsds 101",
                Price = 45000,
                NumberOfItems = 122
            };
            _context.Product.Add(product);
            _context.SaveChanges();
            var result = _context.Product.Find(1);
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public async Task Read_Product_With_Valid_Product_Id()
        {
            await _context.Product.AddAsync(new Product
            {
                ProductId = 1,
                ProductName = "test1",
                Description = "ABC test",
                Price = 45000,
                NumberOfItems = 122
            });
            await _context.Product.AddAsync(new Product
            {
                ProductId = 2,
                ProductName = "test2",
                Description = "LPsds 102",
                Price = 45000,
                NumberOfItems = 100
            });
            _context.SaveChanges();
            var inventoryRepo = new ProductRepository(_context);
            var result = inventoryRepo.ReadProduct(1).Result;
            Assert.IsTrue(result != null);
        }

        [TestMethod]
        public async Task Read_Product_With_InValid_Product_Id()
        {
            await _context.Product.AddAsync(new Product
            {
                ProductId = 1,
                ProductName = "test1",
                Description = "ABC test",
                Price = 45000,
                NumberOfItems = 122
            });
            await _context.Product.AddAsync(new Product
            {
                ProductId = 2,
                ProductName = "test2",
                Description = "LPsds 102",
                Price = 45000,
                NumberOfItems = 100
            });
            _context.SaveChanges();
            var inventoryRepo = new ProductRepository(_context);
            var result = inventoryRepo.ReadProduct(5).Result;
            Assert.IsTrue(result == null);
        }

        [TestMethod]
        public async Task Read_Products_With_InValid_Product_Id()
        {
            await _context.Product.AddAsync(new Product
            {
                ProductId = 1,
                ProductName = "test1",
                Description = "ABC test",
                Price = 45000,
                NumberOfItems = 122
            });
            await _context.Product.AddAsync(new Product
            {
                ProductId = 2,
                ProductName = "test2",
                Description = "LPsds 102",
                Price = 45000,
                NumberOfItems = 100
            });
            _context.SaveChanges();
            var inventoryRepo = new ProductRepository(_context);
            var result = inventoryRepo.ReadProducts().Result;
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void Get_Product_By_Id_With_Valid_Data()
        {
            var product = new Product
            {
                ProductId = 1,
                ProductName = "Lapddsdtop",
                Description = "LPsds 101",
                Price = 45000,
                NumberOfItems = 122
            };
            var mock = new Mock<IProductRepository>();
            mock.Setup(x => x.ReadProduct(1)).Returns(Task.FromResult(product));
            ProductController productController = new ProductController(mock.Object);
            var result = productController.ReadProduct(1);
            Assert.AreEqual(product, result.Result);
        }

        [TestMethod]
        public void GetProductByIdWithInvalidData()
        {
            //arrange
            var product = new Product
            {
                ProductId = 1,
                ProductName = "",
                Description = "testing",
                Price = 1000,
                NumberOfItems = null
            };
            var mock = new Mock<IProductRepository>();
            mock.Setup(x => x.ReadProduct(1));
            ProductController productController = new ProductController(mock.Object);
            //act
            var result = productController.ReadProduct(1);

            //assert
            Assert.AreNotEqual(product, result.Result);
        }

        [TestMethod]
        public void Insert_Product_With_Valid_Data()
        {
            //arrange
            var product = new Product
            {
                ProductName = "Unittest1",
                Description = "Unit test description",
                Price = 1200,
                NumberOfItems = 200
            };
            var mock = new Mock<IProductRepository>();
            mock.Setup(x => x.Upsert(product)).Returns(Task.FromResult(1));
            var controller = new ProductController(mock.Object);

            //act
            var existingProduct = controller.UpsertProduct(product).Result;

            //assert
            Assert.AreEqual(1, existingProduct);
        }
    }
}
