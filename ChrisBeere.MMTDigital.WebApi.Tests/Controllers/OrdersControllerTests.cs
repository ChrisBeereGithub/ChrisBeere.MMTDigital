using ChrisBeere.MMTDigital.WebApi.Controllers;
using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.CustomExceptions;
using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Models;
using ChrisBeere.MMTDigital.WebApi.Services.DataTransferObjects;
using ChrisBeere.MMTDigital.WebApi.Services.Interfaces;
using ChrisBeere.MMTDigital.WebApi.Validation.Implementations;
using ChrisBeere.MMTDigital.WebApi.Validation.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace ChrisBeere.MMTDigital.WebApi.Tests
{
    /// <summary>
    /// Test class covering all defined response types for the GetMostRecentCustomerOrderAsync endpoint.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class OrdersControllerTests
    {
        private Mock<IOrderService> orderService;
        private ICustomerRequestValidator customerRequestValidator;
        private Mock<ILogger<OrdersController>> logger;
        private OrdersController ordersController;

        [SetUp]
        public void Setup()
        {
            orderService = new Mock<IOrderService>();
            customerRequestValidator = new CustomerRequestValidator();
            logger = new Mock<ILogger<OrdersController>>();
            ordersController = new OrdersController(logger.Object,
                                                    orderService.Object,
                                                    customerRequestValidator);
        }

        [Test]
        public async Task GetMostRecentCustomerOrder_Returns200OK_WhenGoodRequest()
        {
            // Arrange
            orderService.Setup(x => x.GetMostRecentOrderForCustomerAsync(It.IsAny<CustomerRequest>()))
                .ReturnsAsync(new CustomerOrderDto{});

            // Act
            var request = new CustomerRequest { CustomerId = "test", User = "test" };
            var result = await ordersController.GetMostRecentCustomerOrderAsync(request);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetMostRecentCustomerOrder_Returns404NotFound_WhenCustomerIsNotFound()
        {
            // Arrange
            orderService.Setup(x => x.GetMostRecentOrderForCustomerAsync(It.IsAny<CustomerRequest>()))
                .ThrowsAsync(new CustomerNotFoundException());

            // Act
            var request = new CustomerRequest { CustomerId = "test", User = "test" };
            var result = await ordersController.GetMostRecentCustomerOrderAsync(request);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetMostRecentCustomerOrder_Returns500InternalError_WhenExceptionOccurs()
        {
            // Arrange
            orderService.Setup(x => x.GetMostRecentOrderForCustomerAsync(It.IsAny<CustomerRequest>()))
                .ThrowsAsync(new Exception());

            // Act
            var request = new CustomerRequest { CustomerId = "test", User = "test" };
            var result = await ordersController.GetMostRecentCustomerOrderAsync(request);

            // Assert
            Assert.IsInstanceOf<StatusCodeResult>(result);
            StatusCodeResult httpResult = (StatusCodeResult)result;
            Assert.AreEqual(500, httpResult.StatusCode);
        }

        [Test]
        public async Task GetMostRecentCustomerOrder_Returns400BadRequest_WhenBadRequest()
        {
            // Arrange
            orderService.Setup(x => x.GetMostRecentOrderForCustomerAsync(It.IsAny<CustomerRequest>()))
                .ReturnsAsync(new CustomerOrderDto { });

            // Act
            var request = new CustomerRequest { CustomerId = "", User = "" };
            var result = await ordersController.GetMostRecentCustomerOrderAsync(request);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(result);
        }

    }
}