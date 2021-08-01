using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.CustomExceptions;
using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Interfaces;
using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Implementations
{
    /// <summary>
    /// Customer service providing methods that return data sourced from an external customer Restful Api.
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private const string logComponent = "CustomerService";
        private ILogger<CustomerService> logger;
        private IConfiguration configuration;
        private readonly IHttpClientService httpClientService;

        /// <summary>
        /// Constructor enabling injection of dependencies.
        /// </summary>
        /// <param name="configuration">Configuration object to read appSettings.</param>
        /// <param name="httpClientService">Http client service to handle calls to external Restful Api.</param>
        /// <param name="logger">Logging component.</param>
        public CustomerService(IConfiguration configuration, 
                               IHttpClientService httpClientService,
                               ILogger<CustomerService> logger)
        {
            this.configuration = configuration;
            this.httpClientService = httpClientService;
            this.logger = logger;
        }

        /// <summary>
        /// Get specified customer data from the external Customer Restful Api.
        /// </summary>
        /// <param name="customerRequest">Request object detailing the customer.</param>
        /// <returns>Customer object containing full customer attributes.</returns>
        public async Task<Customer> GetCustomerAsync(CustomerRequest customerRequest)
        {
            Customer customer = null;
            var logMethod = "GetCustomerAsync";
            var logPrefix = $"{logComponent}.{logMethod}";
            HttpResponseMessage response;

            try
            {
                var url = configuration.GetValue<string>("ExternalApi:CustomerApiUrl");
                var key = configuration.GetValue<string>("ExternalApi:CustomerApiKey");

                HttpRequestMessage request = new HttpRequestMessage();

                var requestQuery = $"{url}?email={customerRequest.User}&code={key}";

                response = await httpClientService.GetAsync(requestQuery);

                if (response.IsSuccessStatusCode)
                {
                    var customerJson = await response.Content.ReadAsStringAsync();
                    customer = JsonConvert.DeserializeObject<Customer>(customerJson);
                }

            }
            catch (Exception ex) 
            { 
                logger.LogError($"{logPrefix}", ex);
                throw;
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound ||
                customer.CustomerId != customerRequest.CustomerId)
            {
                throw new CustomerNotFoundException();
            }

            return customer;
        } 
    }
}
