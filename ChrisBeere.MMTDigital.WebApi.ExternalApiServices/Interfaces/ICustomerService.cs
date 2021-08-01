using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Models;
using System.Threading.Tasks;

namespace ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Interfaces
{
    /// <summary>
    /// Interface defining contracts for a Customer service providing methods 
    /// that return data sourced from the external Customer Restful Api.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Get specified customer data from the external Customer Restful Api.
        /// </summary>
        /// <param name="customerRequest">Request object detailing the customer.</param>
        /// <returns>Customer object containing full customer attributes.</returns>
        Task<Customer> GetCustomerAsync(CustomerRequest customerRequest);
    }
}
