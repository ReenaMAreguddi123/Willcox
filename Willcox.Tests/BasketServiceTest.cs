using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Linq;
using Willcox.Contracts.Models;
using Willcox.Services;
using Xunit;

namespace Willcox.Tests
{
    public class BasketServiceTest
    {
        private readonly Mock<ILogger<BasketService>> _mockLogger;
        private readonly Mock<ILogger<ProductService>> _productServiceMockLogger;
        private readonly Mock<ILogger<DiscountService>> _discountServiceMockLogger;

        public BasketServiceTest()
        {
            _mockLogger = new Mock<ILogger<BasketService>>();
            _productServiceMockLogger = new Mock<ILogger<ProductService>>();
            _discountServiceMockLogger = new Mock<ILogger<DiscountService>>();

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
        public void Basket_Count()
        {
            //Arrange
            var basket = new Basket();

            //Act
            basket.Add(new Product() {ProductId = "1", Name = "Headstone", Price = 120}, 1);
            basket.Add(new Product() {ProductId = "2", Name = "Base", Price = 60}, 1);

            //Assert
            Assert.True(basket.Items.Count == 2);
            Assert.True(basket.Total == 180);
        }

        [Fact]
        public void Basket_ApplyDiscount()
        {
            //Arrange
            var productService = new ProductService(_productServiceMockLogger.Object);
            var discountService = new DiscountService(_discountServiceMockLogger.Object);
            var basketService = new BasketService(discountService, _mockLogger.Object);
            var basket = new Basket();

            //Act
            var products = productService.GetActiveProducts();
            basket.Add(products[0], 1);
            basket.Add(products[1], 1);

            var basketSummary = basketService.ApplyDiscount(basket);

            //Assert
            Assert.NotNull(basketSummary);
            Assert.True(basketSummary.Subtotal == 180);
            Assert.True(basketSummary.Discounts.Count == 1);
            Assert.True(basketSummary.Discounts.First().Amount == 18);
            Assert.True(basketSummary.Total == 162);
        }
    }
}
