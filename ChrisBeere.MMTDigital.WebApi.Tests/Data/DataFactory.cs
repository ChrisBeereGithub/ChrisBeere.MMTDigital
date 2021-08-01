using ChrisBeere.MMTDigital.WebApi.Data.DataTransferObjects;
using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Models;
using ChrisBeere.MMTDigital.WebApi.Services.DataTransferObjects;
using System;
using System.Collections.Generic;

namespace ChrisBeere.MMTDigital.WebApi.Tests.Data
{
    /// <summary>
    /// Factory supplying data for unit tests.
    /// </summary>
    public static class DataFactory
    {
        public static List<CustomerOrderItemDto> GetRepositoryData()
        {
            var customerOrderItems = new List<CustomerOrderItemDto>();

            var customerOrderItem = new CustomerOrderItemDto
            {
                OrderId = 1,
                ContainsGift = false,
                OrderDate = new DateTime(2021, 7, 10, 0, 0, 0, 0),
                DeliveryExpected = new DateTime(2021, 7, 12, 0, 0, 0, 0),
                Price = 10,
                Quantity = 2,
                ProductName = "test product",
            };

            customerOrderItems.Add(customerOrderItem);

            return customerOrderItems;
        }

        public static Customer GetExternalApiData()
        {
            var customer = new Customer
            {
                CustomerId = "t1",
                Email = "test@test.com",
                HouseNumber = "1",
                Street = "street",
                Town = "town",
                PostCode = "abc123",
                FirstName = "first",
                LastName = "last"
            };

            return customer;
        }

        public static CustomerOrderDto GetExpectedResponseData()
        {
            var orderItems = new List<OrderItemDto>();

            var orderItem = new OrderItemDto
            {
                PriceEach = 10,
                Product = "test product",
                Quantity = 2
            };

            orderItems.Add(orderItem);

            var order = new OrderDto
            {
                OrderItems = orderItems,
                DeliveryAddress = "1 street, town, abc123",
                OrderNumber = 1,
                DeliveryExpected = "12-July-2021",
                OrderDate = "10-July-2021",
            };

            var customer = new CustomerDto
            {
                FirstName = "first",
                LastName = "last"
            };

            var data = new CustomerOrderDto
            {
                Order = order,
                Customer = customer
            };

            return data;
        }

        public static CustomerRequest GetCustomerRequest()
        {
            var customerRequest = new CustomerRequest
            {
                CustomerId = "c1",
                User = "test@test.com"
            };

            return customerRequest;
        }

    }
}
