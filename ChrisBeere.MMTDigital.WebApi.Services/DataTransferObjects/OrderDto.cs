using System.Collections.Generic;

namespace ChrisBeere.MMTDigital.WebApi.Services.DataTransferObjects
{
    /// <summary>
    /// Data transfer object defining Order model encapsulated by CustomerOrderDto.
    /// </summary>
    public class OrderDto
    {
        public OrderDto()
        {
            OrderItems = new List<OrderItemDto>();
        }

        public int OrderNumber { get; set; }
        public string OrderDate { get; set; } // needs to be a string to control formatting when rendered.
        public string DeliveryAddress { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public string DeliveryExpected { get; set; } // needs to be a string to control formatting when rendered.
    }
}
