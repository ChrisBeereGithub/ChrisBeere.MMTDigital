using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Models;

namespace ChrisBeere.MMTDigital.WebApi.Validation.Interfaces
{
    /// <summary>
    /// Interface defining contracts for a validater for customer http requests.
    /// </summary>
    public interface ICustomerRequestValidator
    {
        /// <summary>
        /// Validate the supplied request object.
        /// </summary>
        /// <param name="customerRequest">Request containing customer details.</param>
        /// <returns>True if request is good, false if its bad.</returns>
        bool Validate(CustomerRequest customerRequest);
    }
}
