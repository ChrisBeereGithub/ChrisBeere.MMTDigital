using ChrisBeere.MMTDigital.WebApi.Data.DataTransferObjects;
using ChrisBeere.MMTDigital.WebApi.Data.Models;
using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChrisBeere.MMTDigital.WebApi.Data.Repositories.Interfaces
{
    /// <summary>
    /// Interface defining contracts of a data access repository for Customer Order data.
    /// </summary>
    public interface IOrderRepository 
    {
        /// <summary>
        /// Query the database and return the most recent order for the specified customer.
        /// </summary>
        /// <param name="customerRequest">The customer to retrieve order data for.</param>
        /// <returns>The most recent order object or null if one does not exist.</returns>
        Task<List<CustomerOrderItemDto>> GetMostRecentOrderForCustomerAsync(CustomerRequest customerRequest);
    }
}
