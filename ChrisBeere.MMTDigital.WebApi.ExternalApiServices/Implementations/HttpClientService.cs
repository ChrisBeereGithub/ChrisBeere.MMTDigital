using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Implementations
{
    /// <summary>
    /// Service providing external Restful Api communication.
    /// </summary>
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private const string logComponent = "HttpClientService";
        private ILogger<HttpClientService> logger;

        /// <summary>
        /// Constructor enabling injection of dependencies.
        /// </summary>
        /// <param name="logger">Logging component.</param>
        /// <param name="httpClientFactory">Http client factory provides instances.</param>
        public HttpClientService(ILogger<HttpClientService> logger, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Execute the supplied uri against the remote Rest Api.
        /// </summary>
        /// <param name="uri">Full request including query string.</param>
        /// <returns>The Http Response of the executed request.</returns>
        public async Task<HttpResponseMessage> GetAsync(string uri)
        {
            var logMethod = "GetAsync";
            var logPrefix = $"{logComponent}.{logMethod}";

            try
            {
                var httpClient = httpClientFactory.CreateClient("CustomerApi");
                var response = await httpClient.GetAsync(uri);

                return response;
            }
            catch (Exception ex)
            {
                logger.LogError($"{logPrefix}", ex);
                throw;
            }
        }
    }
}
