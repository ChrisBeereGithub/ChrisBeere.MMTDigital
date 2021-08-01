using ChrisBeere.MMTDigital.WebApi.Data.DatabaseContext;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ChrisBeere.MMTDigital.WebApi.Data.Repositories.Interfaces;
using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Models;
using Microsoft.Extensions.Logging;
using System;
using ChrisBeere.MMTDigital.WebApi.Data.DataTransferObjects;

namespace ChrisBeere.MMTDigital.WebApi.Data.Repositories.Implementations
{
    /// <summary>
    /// Data access repository for Customer Order data.
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private const string logComponent = "OrderRepository";
        private readonly SSETestContext dbContext;
        private ILogger<OrderRepository> logger;

        /// <summary>
        /// Constructor enabling injection of dependencies.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        /// <param name="logger">Logging component.</param>
        public OrderRepository(SSETestContext dbContext, ILogger<OrderRepository> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        /// <summary>
        /// Query the database and return the most recent order for the specified customer.
        /// </summary>
        /// <param name="customerRequest">The customer to retrieve order data for.</param>
        /// <returns>The most recent order and its items if one exists.</returns>
        public async Task<List<CustomerOrderItemDto>> GetMostRecentOrderForCustomerAsync(CustomerRequest customerRequest)
        {
            var logMethod = "GetMostRecentOrderForCustomerAsync";
            var logPrefix = $"{logComponent}.{logMethod}";
            try
            {
                var customerOrderItems = await 
                    (from o in dbContext.Orders.AsNoTracking()
                     join oi in dbContext.OrderItems.AsNoTracking()
                     on o.OrderId equals oi.OrderId
                     join p in dbContext.Products.AsNoTracking()
                     on oi.ProductId equals p.ProductId
                     where o.CustomerId == customerRequest.CustomerId
                     && o.OrderDate ==
                        (from o in dbContext.Orders.AsNoTracking()
                        where o.CustomerId == customerRequest.CustomerId
                        select o.OrderDate).Max()
                     select new CustomerOrderItemDto
                     { 
                        Colour = p.Colour,
                        ContainsGift = o.ContainsGift,
                        CustomerId = o.CustomerId,
                        DeliveryExpected = o.DeliveryExpected,
                        OrderDate = o.OrderDate,
                        OrderId = o.OrderId,
                        OrderItemId = oi.OrderItemId,
                        OrderSource = o.OrderSource,
                        PackHeight = p.PackHeight,
                        PackWeight = p.PackWeight,
                        PackWidth = p.PackWidth,
                        Price = oi.Price,
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                        Quantity = oi.Quantity,
                        Returnable = oi.Returnable,
                        ShippingMode = o.ShippingMode,
                        Size = p.Size
                     }).ToListAsync();
                
                return customerOrderItems;
            }
            catch (Exception ex)
            {
                logger.LogError($"{logPrefix}", ex);
                throw;
            }
        }
    }
}
