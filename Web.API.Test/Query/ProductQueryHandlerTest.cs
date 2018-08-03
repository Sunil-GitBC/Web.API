using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Web.API.Dto;
using Web.API.Entity;
using Web.API.Query;
using Web.API.Services;

namespace Web.API.Test.Query
{
    [TestClass]
    public class ProductQueryHandlerTest
    {
        private ProductQueryHandler _queryHandler;
        private IProductService _productService;

        [TestInitialize]
        public void Init()
        {
            _productService = Substitute.For<IProductService>();
            _productService.GetProducts(Arg.Any<FilterDto>()).Returns(CreateMockProducts());
            _productService.GetProductById(Arg.Any<string>())
                .Returns(new Product
                {
                    Id = "MockId",
                    Brand = "Mock Brand",
                    Description = "Mock Desc",
                    Model = "Mock Model"
                });

            _queryHandler = new ProductQueryHandler(_productService);
        }

        [TestMethod]
        public async Task DispatchProductsQuery_ReturnAllProducts()
        {
            // Act
            var result = await _queryHandler.Handle(new ProductsQuery(), new CancellationToken());

            // Assert 
            Assert.AreEqual(result.Count, 2);
        }

        [TestMethod]
        public async Task DispatchProductQueryWithId_ReturnProduct()
        {
            // Arrange
            var productId = "MockId";
            // Act
            var result = await _queryHandler.Handle(new ProductQuery(productId), new CancellationToken());

            // Assert 
            Assert.AreEqual(result.Brand, "Mock Brand");
            Assert.AreEqual(result.Description, "Mock Desc");
            Assert.AreEqual(result.Model, "Mock Model");
        }

        private IList<Product> CreateMockProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = "S00001-PS4",
                    Brand = "Sony",
                    Description = "Console gaming",
                    Model = "PS4"
                },
                new Product
                {
                    Id = "M00001-XBox",
                    Brand = "Microsoft",
                    Description = "Console gaming",
                    Model = "XBox"
                }
            };
        }
    }
}
