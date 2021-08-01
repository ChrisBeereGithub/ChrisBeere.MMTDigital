using ChrisBeere.MMTDigital.WebApi.Data.Repositories.Interfaces;
using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Interfaces;
using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Models;
using ChrisBeere.MMTDigital.WebApi.Services.Implementations;
using ChrisBeere.MMTDigital.WebApi.Services.Interfaces;
using ChrisBeere.MMTDigital.WebApi.Tests.Data;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using FluentAssertions;

namespace ChrisBeere.MMTDigital.WebApi.Tests.Services
{
    /// <summary>
    /// Test class providing coverage for the GetMostRecentOrderForCustomerAsync service method.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class OrderServiceTests
    {
        private Mock<ILogger<OrderService>> logger;
        private Mock<IOrderRepository> orderRepository;
        private Mock<ICustomerService> customerService;
        private IOrderService orderService;

        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILogger<OrderService>>();
            orderRepository = new Mock<IOrderRepository>();
            customerService = new Mock<ICustomerService>();
            orderService = new OrderService(orderRepository.Object,
                                            customerService.Object,
                                            logger.Object);
        }

        [Test]
        public async Task GetMostRecentOrderForCustomerAsync_ReturnsCorrectlyMappedCustomerOrderDto_WhenGoodRequest()
        {
            // Arrange
            orderRepository.Setup(x => x.GetMostRecentOrderForCustomerAsync(It.IsAny<CustomerRequest>()))
                .ReturnsAsync(DataFactory.GetRepositoryData());

            customerService.Setup(x => x.GetCustomerAsync(It.IsAny<CustomerRequest>()))
                .ReturnsAsync(DataFactory.GetExternalApiData());

            var expected = DataFactory.GetExpectedResponseData();
            var customerRequest = DataFactory.GetCustomerRequest();

            // Act
            var result = await orderService.GetMostRecentOrderForCustomerAsync(customerRequest);

            // Assert
            expected.Should().BeEquivalentTo(result);
        }
    }
}
