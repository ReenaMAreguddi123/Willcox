using Microsoft.Extensions.Logging;
using Moq;
using System;
using Willcox.Services;
using Xunit;

namespace Willcox.Tests
{
    public class ProductServiceTest
    {
        private readonly Mock<ILogger<ProductService>> _mockLogger;
        public ProductServiceTest()
        {
            _mockLogger = new Mock<ILogger<ProductService>>();
            _mockLogger.Setup(
                m => m.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.IsAny<object>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()));

            var mockLoggerFactory = new Mock<ILoggerFactory>();

            mockLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(() => _mockLogger.Object);
        }

        [Fact]
        public void GetActiveProducts()
        {
            //Arrange
            var productService = new ProductService(_mockLogger.Object);

            //Act
            var products = productService.GetActiveProducts();

            //Assert
            Assert.NotNull(products);
            Assert.True(products.Count == 4);
        }
    }
}
