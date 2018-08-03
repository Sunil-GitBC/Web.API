using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.API.Entity;
using Web.API.Services;

namespace Web.API.Test
{
    [TestClass]
    public class ProductServiceTest
    {
        private IProductService _productService;
        private ProductDbContext _productDbContext;

        [TestInitialize]
        public void Init()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(databaseName: "Product_Context_Mock")
                .Options;

            _productDbContext = new ProductDbContext(options);

            // TODO: move db helper in Web.API to this project after Web.API have been refactored to use persistence db
            _productDbContext.EnsureDbSeeded();
            _productService = new ProductService(_productDbContext);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _productDbContext.Dispose();
        }

        [TestMethod]
        public async Task GetAll_AllProducts()
        {
            // Act
            var productList = await _productService.GetProducts(null);

            //Assert
            Assert.AreEqual(productList.Count, 3);
        }

        [TestMethod]
        public async Task GivenId_GetById_SpecificProduct()
        {
            // Arrange
            var searchId = "M00001-XBox";

            // Act
            var product = await _productService.GetProductById(searchId);

            //Assert
            Assert.AreEqual(product.Brand, "Microsoft");
            Assert.AreEqual(product.Description, "Console gaming");
        }

        [TestMethod]
        public async Task GivenNonExistenceId_GetById_Null()
        {
            // Arrange
            var searchId = "M00001-0000";

            // Act
            var product = await _productService.GetProductById(searchId);

            //Assert
            Assert.AreEqual(product, null);
        }

        [TestMethod]
        public async Task GivenId_DeleteById_RemoveItem()
        {
            // Arrange
            var searchId = "M00001-XBox";

            // Act
            await _productService.DeleteProductById(searchId);
            var product = await _productDbContext.Products.FindAsync(searchId);

            //Assert
            Assert.AreEqual(product, null);
        }

        [TestMethod]
        public async Task GivenProduct_AddProduct_NewProductAdded()
        {
            // Arrange
            var productId = "M00001-TestProd";
            var productToInsert = new Product
            {
                Id = productId,
                Brand = "Test Brand",
                Description = "Test Description",
                Model = "Test Model"
            };

            // Act
            await _productService.AddProduct(productToInsert);
            var product = await _productDbContext.Products.FindAsync(productId);

            //Assert
            Assert.AreEqual(product.Id, productId);
            Assert.AreEqual(product.Brand, productToInsert.Brand);
            Assert.AreEqual(product.Description, productToInsert.Description);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Product exists")]
        public async Task GivenProductWithSameId_AddProduct_ExceptionThrown()
        {
            // Arrange
            var productId = "M00001-XBox";
            var productToInsert = new Product
            {
                Id = productId,
                Brand = "Test Brand",
                Description = "Test Description",
                Model = "Test Model"
            };

            // Act
            await _productService.AddProduct(productToInsert);
        }

        [TestMethod]
        public async Task GivenProductAndId_UpdateProduct_ProductUpdated()
        {
            // Arrange
            var productId = "M00001-XBox";
            var productToInsert = new Product
            {
                Brand = "Test Brand",
                Description = "Test Description",
                Model = "Test Model"
            };

            // Act
            await _productService.UpdateProductById(productId, productToInsert);
            var product = await _productDbContext.Products.FindAsync(productId);

            //Assert
            Assert.AreEqual(product.Id, productId);
            Assert.AreEqual(product.Brand, productToInsert.Brand);
            Assert.AreEqual(product.Description, productToInsert.Description);
        }
    }
}
