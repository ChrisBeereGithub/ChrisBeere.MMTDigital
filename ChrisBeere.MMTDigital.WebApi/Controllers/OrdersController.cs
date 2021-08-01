using System;
using System.Threading.Tasks;
using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.CustomExceptions;
using ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Models;
using ChrisBeere.MMTDigital.WebApi.Services.Interfaces;
using ChrisBeere.MMTDigital.WebApi.Validation.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChrisBeere.MMTDigital.WebApi.Controllers
{
    /// <summary>
    /// Web Api controller to marshall http requests related to customer orders.
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OrdersController : ControllerBase
    {
        private const string logComponent = "OrdersController";
        private readonly ILogger<OrdersController> logger;
        private readonly IOrderService orderService;
        private readonly ICustomerRequestValidator customerRequestValidator;

        /// <summary>
        /// Constructor to enable injection of controller dependencies.
        /// </summary>
        /// <param name="logger">Logging component.</param>
        /// <param name="orderService">Order service providing data access.</param>
        /// <param name="customerRequestValidator">Validation service for requests.</param>
        public OrdersController(ILogger<OrdersController> logger, 
                                IOrderService orderService,
                                ICustomerRequestValidator customerRequestValidator)
        {
            this.logger = logger;
            this.orderService = orderService;
            this.customerRequestValidator = customerRequestValidator;
        }

        /// <summary>
        /// Get the most recent order for the supplied customer.
        /// </summary>
        /// <param name="customerRequest">Request specifying the customer details.</param>
        /// <returns>Operation status and serialised Json order data.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMostRecentCustomerOrderAsync(CustomerRequest customerRequest)
        {
            var logMethod = "GetMostRecentCustomerOrder";
            var logPrefix = $"{logComponent}.{logMethod}";
            try
            {
                if (!customerRequestValidator.Validate(customerRequest))
                {
                    logger.LogWarning($"{logPrefix} Bad request.");
                    return BadRequest();
                }

                var customerOrder = await orderService.GetMostRecentOrderForCustomerAsync(customerRequest);

                return Ok(customerOrder);
            }
            catch (CustomerNotFoundException cnfex)
            {
                logger.LogWarning($"{logPrefix} Customer {customerRequest.User} not found.", cnfex);
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError($"{logPrefix} Internal error.", ex);
                return StatusCode(500);
            }
        }
    }
}
