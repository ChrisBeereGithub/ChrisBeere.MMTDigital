using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Models;
using ChrisBeere.MMTDigital.WebApi.Services.DataTransferObjects;
using System.Threading.Tasks;

namespace ChrisBeere.MMTDigital.WebApi.Services.Interfaces
{
    /// <summary>
    /// Interface defining an Order service providing Customer Order and related OrderItem/Product data.
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Get the most recent order and customer data for the specified customer.
        /// </summary>
        /// <param name="customer">The customer to retrieve order data for.</param>
        /// <returns>Customer Order data transfer object.</returns>
        Task<CustomerOrderDto> GetMostRecentOrderForCustomerAsync(CustomerRequest customer);
    }
}
