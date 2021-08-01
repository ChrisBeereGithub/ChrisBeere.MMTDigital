using System.Net.Http;
using System.Threading.Tasks;

namespace ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Interfaces
{
    /// <summary>
    /// Interface defining contracts for a service providing external Restful Api communication.
    /// </summary>
    public interface IHttpClientService
    {
        /// <summary>
        /// Execute the supplied uri against the remote Rest Api.
        /// </summary>
        /// <param name="uri">Full request including query string.</param>
        /// <returns>The Http Response of the executed request.</returns>
        Task<HttpResponseMessage> GetAsync(string uri);
    }
}
