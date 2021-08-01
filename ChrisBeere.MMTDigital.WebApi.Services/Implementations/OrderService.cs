using ChrisBeere.MMTDigital.WebApi.Services.Interfaces;
using ChrisBeere.MMTDigital.WebApi.Data.Repositories.Interfaces;
using System.Threading.Tasks;
using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Models;
using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Interfaces;
using ChrisBeere.MMTDigital.WebApi.Services.DataTransferObjects;
using ChrisBeere.MMTDigital.WebApi.Data.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System;

namespace ChrisBeere.MMTDigital.WebApi.Services.Implementations
{
    /// <summary>
    /// Order service providing Customer Order and related OrderItem/Product data.
    /// </summary>
    public class OrderService : IOrderService
    {
        private const string logComponent = "OrderService";
        private ILogger<OrderService> logger;
        private readonly IOrderRepository orderRepository;
        private readonly ICustomerService customerService;

        /// <summary>
        /// Constructor enabling injection of dependencies.
        /// </summary>
        /// <param name="orderRepository">The data access repository.</param>
        /// <param name="customerService">The external api customer service.</param>
        /// <param name="logger">Logging component.</param>
        public OrderService(IOrderRepository orderRepository, 
                            ICustomerService customerService,
                            ILogger<OrderService> logger)
        {
            this.customerService = customerService;
            this.orderRepository = orderRepository;
            this.logger = logger;
        }

        /// <summary>
        /// Get the most recent order and customer data for the specified customer.
        /// </summary>
        /// <param name="customerRequest">The customer to retrieve order data for.</param>
        /// <returns>Customer Order data transfer object.</returns>
        public async Task<CustomerOrderDto> GetMostRecentOrderForCustomerAsync(CustomerRequest customerRequest)
        {
            var logMethod = "GetMostRecentOrderForCustomerAsync";
            var logPrefix = $"{logComponent}.{logMethod}";

            try
            {
                var customer = await customerService.GetCustomerAsync(customerRequest);

                var customerOrderItems = await orderRepository.GetMostRecentOrderForCustomerAsync(customerRequest);

                var customerOrderDto = MapCustomerOrderDataToDto(customer, customerOrderItems);

                return customerOrderDto;
            }
            catch (Exception ex)
            { 
                logger.LogError($"{logPrefix}", ex);
                throw;
            }
        }

        /// <summary>
        /// Transpose the supplied collective Customer and Order data into a single data transfer object.
        /// </summary>
        /// <param name="customer">The Customer object.</param>
        /// <param name="customerOrderItems">List of Customer Order Items with all related entity data.</param>
        /// <returns>Object containing customer and most recent order details.</returns>
        private CustomerOrderDto MapCustomerOrderDataToDto(Customer customer, List<CustomerOrderItemDto> customerOrderItems)
        {
            var logMethod = "MapCustomerOrderDataToDto";
            var logPrefix = $"{logComponent}.{logMethod}";

            try
            {
                var customerOrderDto = new CustomerOrderDto();

                if (customer != null)
                {
                    customerOrderDto.Customer.FirstName = customer.FirstName;
                    customerOrderDto.Customer.LastName = customer.LastName;
                }

                if (customerOrderItems != null)
                {
                    customerOrderDto.Order = new OrderDto();

                    var orderData = customerOrderItems.First();

                    customerOrderDto.Order.OrderNumber = orderData.OrderId;
                    customerOrderDto.Order.OrderDate = orderData.OrderDate.ToString("dd-MMMM-yyyy");
                    customerOrderDto.Order.DeliveryAddress =
                        $"{customer.HouseNumber} {customer.Street}, {customer.Town}, {customer.PostCode}";

                    customerOrderDto.Order.DeliveryExpected = orderData.DeliveryExpected.ToString("dd-MMMM-yyyy");

                    foreach (CustomerOrderItemDto customerOrderItem in customerOrderItems)
                    {
                        var giftMask = string.Empty;

                        if (customerOrderItem.ContainsGift)
                        {
                            giftMask = "Gift";
                        }
                        else
                        {
                            giftMask = customerOrderItem.ProductName;
                        }

                        var orderItemDto = new OrderItemDto
                        {
                            PriceEach = customerOrderItem.Price,
                            Product = giftMask,
                            Quantity = customerOrderItem.Quantity
                        };

                        customerOrderDto.Order.OrderItems.Add(orderItemDto);
                    }
                }

                return customerOrderDto;
            }
            catch (Exception ex)
            {
                logger.LogError($"{logPrefix}", ex);
                throw;
            }
        }
    }
}
