using System;
using Microsoft.Extensions.Logging;
using Moq;
using Willcox.Contracts;
using Willcox.Services;
using Xunit;

namespace Willcox.Tests
{
    public class DiscountServiceTest
    {
        private readonly Mock<ILogger<DiscountService>> _mockLogger;
        public DiscountServiceTest()
        {
            _mockLogger = new Mock<ILogger<DiscountService>>();
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
        public void GetActiveDiscounts()
        {
            //Arrange
            var discountService = new DiscountService(_mockLogger.Object);

            //Act
            var discounts = discountService.GetActiveDiscounts();

            //Assert
            Assert.NotNull(discounts);
            Assert.True(discounts.Count == 2);
        }
    }
}
